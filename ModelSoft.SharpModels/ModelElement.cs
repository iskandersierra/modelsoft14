using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using ModelSoft.Framework.Reflection;

namespace ModelSoft.SharpModels
{
    public enum ModelPropertyType { Property, Container, Content, Reference }
    public enum ModelPropertyMultiplicity { Single, Collection, OrderedCollection }

    public abstract class ModelElement :
        IModelElement
    {
        internal ModelElement()
        {
        }

        #region [ IModelElement ]

        private IModelElement _containerElement;

        IModelElement IModelElement.ContainerElement
        {
            get { return GetContainerElement(); }
        }

        protected IModelElement GetContainerElement()
        {
            return _containerElement;
        }

        public IEnumerable<IModelElement> ContainedElements
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IModel ContainerModel
        {
            get
            {
                if (this is IModel) return (IModel)this;
                if (_containerElement == null) return null;
                return _containerElement.ContainerModel;
            }
        }

        #endregion

        #region [ RegisterProperty ]

        private static readonly object _registryLock = new object();
        private static long _nextPropertyIdentifier = 1;
        private static ConcurrentDictionary<long, ModelProperty> _propertiesById = new ConcurrentDictionary<long, ModelProperty>();
        private static ConcurrentDictionary<PropertyInfo, ModelProperty> _propertiesByPropertyInfo = new ConcurrentDictionary<PropertyInfo, ModelProperty>();
        private static ConcurrentDictionary<Type, TypeInformation> _typesRegistry = new ConcurrentDictionary<Type, TypeInformation>();

        protected internal static IModelProperty<TValue> RegisterProperty<TValue>(Func<IModelProperty<TValue>> propertyFunc)
        {
            if (propertyFunc == null) throw new ArgumentNullException("propertyFunc");
            lock (_registryLock)
            {
                var evaluation = propertyFunc();
                if (evaluation == null)
                    throw new InvalidOperationException(string.Format("Unknown property type: {0}", "null"));

                var property = evaluation as ModelProperty;

                if (property == null)
                    throw new InvalidOperationException(string.Format("Unknown property type: {0}", evaluation.GetType().Name));
                if (property._identifier != 0L)
                    throw new InvalidOperationException(string.Format("Property {0} already has identifier {1}", property._property, property.Identifier));

                if (property._underlyingProperty != null)
                {
                    // is a read-only property
                    if (property._underlyingProperty.Identifier == 0L)
                        throw new ArgumentException(string.Format("Cannot register read-only property before its underliying property {0}", property), "propertyFunc");
                    property._identifier = property._underlyingProperty.Identifier;
                }
                else
                {
                    if (property.Opposite != null)
                    {
                        var modelProperty = property.Opposite as ModelProperty;
                        if (modelProperty == null || modelProperty.Opposite != null && modelProperty.Opposite != property)
                            throw new ArgumentException(string.Format("Opposite property {0} is already bound for property {1}", property.Opposite, property));
                        modelProperty._opposite = property;
                    }

                    if (_propertiesByPropertyInfo.ContainsKey(property._property))
                        throw new InvalidOperationException(string.Format("Property {0} is already defined", property._property));

                    var declaringType = property._property.DeclaringType;
                    var typeInfo = GetTypeInformationInternal(declaringType);

                    if (typeInfo.PropertiesByName.ContainsKey(property.Name))
                        throw new InvalidOperationException(string.Format("Type {0} already define a property with name {1}", declaringType.FullName, property.Name));

                    property._identifier = _nextPropertyIdentifier++;

                    _propertiesById.TryAdd(property._identifier, property);
                    _propertiesByPropertyInfo.TryAdd(property._property, property);
                    typeInfo.PropertiesByName.TryAdd(property.Name, property);
                }

                return evaluation;
            }
        }

        private static TypeInformation GetTypeInformationInternal(Type type)
        {
            return _typesRegistry.GetOrAdd(type, CreateTypeInformationInternal);
        }

        private static TypeInformation CreateTypeInformationInternal(Type type)
        {
            return new TypeInformation(type);
        }

        #endregion

        #region [ Get/SetValue ]

        protected TValue GetValue<TValue>(IModelProperty<TValue> property)
        {
            throw new NotImplementedException();
        }

        protected void SetValue<TValue>(IModelProperty<TValue> firstNameProperty, TValue value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region [ ModelProperty ]

        internal class ModelProperty : IModelProperty
        {
            internal long _identifier;
            internal readonly PropertyInfo _property;
            private readonly string _name;
            private readonly ModelPropertyType _modelPropertyType;
            private readonly ModelPropertyMultiplicity _modelPropertyMultiplicity;
            private readonly Func<string> _friendlyNameFunc;
            internal IModelProperty _opposite;
            private readonly bool _isReadOnly;
            internal readonly IModelProperty _underlyingProperty;

            internal ModelProperty(PropertyInfo property, ModelPropertyType modelPropertyType, ModelPropertyMultiplicity modelPropertyMultiplicity, Func<string> friendlyNameFunc, IModelProperty opposite)
            {
                _property = property;
                _modelPropertyType = modelPropertyType;
                _modelPropertyMultiplicity = modelPropertyMultiplicity;
                _friendlyNameFunc = friendlyNameFunc;
                _opposite = opposite;
                _name = property.Name;
            }
            internal ModelProperty(IModelProperty underlyingProperty)
            {
                _isReadOnly = true;
                _underlyingProperty = underlyingProperty;
            }

            public string Name
            {
                get
                {
                    return _underlyingProperty != null
                        ? _underlyingProperty.Name
                        : _name;
                }
            }

            public string FriendlyName
            {
                get
                {
                    return _underlyingProperty != null
                        ? _underlyingProperty.FriendlyName
                        : (_friendlyNameFunc == null ? Name : _friendlyNameFunc());
                }
            }

            public long Identifier
            {
                get { return _identifier; }
            }

            public ModelPropertyType ModelPropertyType
            {
                get
                {
                    return _underlyingProperty != null
                        ? _underlyingProperty.ModelPropertyType
                        : _modelPropertyType;
                }
            }

            public ModelPropertyMultiplicity ModelPropertyMultiplicity
            {
                get
                {
                    return _underlyingProperty != null
                      ? _underlyingProperty.ModelPropertyMultiplicity
                      : _modelPropertyMultiplicity;
                }
            }

            public IModelProperty Opposite
            {
                get
                {
                    return _underlyingProperty != null
                        ? _underlyingProperty.Opposite
                        : _opposite;
                }
            }

            public bool IsReadOnly
            {
                get { return _isReadOnly; }
            }

            protected bool Equals(ModelProperty other)
            {
                if (_identifier == 0) throw new InvalidOperationException(string.Format("Property {0} is not initialized", _property));
                return _identifier == other._identifier;
            }

            public override bool Equals(object obj)
            {
                if (_identifier == 0) throw new InvalidOperationException(string.Format("Property {0} is not initialized", _property));
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((ModelProperty)obj);
            }

            public override int GetHashCode()
            {
                if (_identifier == 0) throw new InvalidOperationException(string.Format("Property {0} is not initialized", _property));
                return _identifier.GetHashCode();
            }

            public override string ToString()
            {
                return _underlyingProperty != null
                        ? _underlyingProperty.ToString()
                        : string.Format("[{0}] {1}.{2}", _identifier, _property.DeclaringType.Name, Name);
            }
        }
        #endregion

        #region [ TypeInformation ]
        class TypeInformation
        {
            private readonly Type _type;
            private readonly Type _interface;
            private ConcurrentDictionary<string, ModelProperty> _propertiesByName = new ConcurrentDictionary<string, ModelProperty>();
            private string _url;
            private Func<object> _constructor;

            public TypeInformation(Type type)
            {
                if (type == null) throw new ArgumentNullException("type");
                _type = type;
                _interface = ImplementsAttribute.ElementImplements(type);
                if (_interface == null)
                    throw new InvalidOperationException(string.Format("Type {0} do not implement a model interface", type.FullName));

                _url = ModelElementUrlAttribute.GetElementUrl(_interface);
                if (_url == null)
                    throw new InvalidOperationException(string.Format("Type {0} has no url associated with it", type.FullName));

                var constructorInfo = type.GetConstructor(Type.EmptyTypes);
                if (constructorInfo == null)
                    throw new InvalidOperationException(string.Format("Type {0} has no parameterless constructor therefore is non-instantiable", type.FullName));
                _constructor = Expression.Lambda<Func<object>>(
                    body: Expression.New(constructorInfo)
                    ).Compile();
            }

            public Func<object> Constructor
            {
                get { return _constructor; }
            }

            public string Url
            {
                get { return _url; }
            }

            public Type Type
            {
                get { return _type; }
            }

            public ConcurrentDictionary<string, ModelProperty> PropertiesByName
            {
                get { return _propertiesByName; }
            }
        }
        #endregion
    }

    public abstract class ModelElement<TElement> :
        ModelElement
        where TElement : ModelElement<TElement>
    {

        protected static ModelPropertyBuilder<TValue> RegisterProperty<TValue>(Expression<Func<TElement, TValue>> property)
        {
            if (property == null) throw new ArgumentNullException("property");

            var pInfo = property.GetParameterPropertyInfo();
            return new ModelPropertyBuilder<TValue>(pInfo, ModelPropertyType.Property, ModelPropertyMultiplicity.Single);
        }
        protected static ModelPropertyBuilder<TValue> RegisterProperty<TValue>(PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException("property");

            return new ModelPropertyBuilder<TValue>(property, ModelPropertyType.Property, ModelPropertyMultiplicity.Single);
        }

        protected static ModelPropertyBuilder<TValue> RegisterContainer<TValue>(Expression<Func<TElement, TValue>> property)
        {
            if (property == null) throw new ArgumentNullException("property");

            var pInfo = property.GetParameterPropertyInfo();
            return new ModelPropertyBuilder<TValue>(pInfo, ModelPropertyType.Container, ModelPropertyMultiplicity.Single);
        }
        protected static ModelPropertyBuilder<TValue> RegisterContainer<TValue>(PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException("property");

            return new ModelPropertyBuilder<TValue>(property, ModelPropertyType.Container, ModelPropertyMultiplicity.Single);
        }

        protected static ModelPropertyBuilder<TValue> RegisterContent<TValue>(Expression<Func<TElement, TValue>> property)
        {
            if (property == null) throw new ArgumentNullException("property");

            var pInfo = property.GetParameterPropertyInfo();
            return new ModelPropertyBuilder<TValue>(pInfo, ModelPropertyType.Content, ModelPropertyMultiplicity.Single);
        }
        protected static ModelPropertyBuilder<TValue> RegisterContent<TValue>(PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException("property");

            return new ModelPropertyBuilder<TValue>(property, ModelPropertyType.Content, ModelPropertyMultiplicity.Single);
        }
        protected static ModelPropertyBuilder<ICollection<TValue>> RegisterContents<TValue>(Expression<Func<TElement, ICollection<TValue>>> property)
        {
            if (property == null) throw new ArgumentNullException("property");

            var pInfo = property.GetParameterPropertyInfo();
            return new ModelPropertyBuilder<ICollection<TValue>>(pInfo, ModelPropertyType.Content, ModelPropertyMultiplicity.Collection);
        }
        protected static ModelPropertyBuilder<ICollection<TValue>> RegisterContents<TValue>(PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException("property");

            return new ModelPropertyBuilder<ICollection<TValue>>(property, ModelPropertyType.Content, ModelPropertyMultiplicity.Collection);
        }
        protected static ModelPropertyBuilder<IList<TValue>> RegisterOrderedContents<TValue>(Expression<Func<TElement, IList<TValue>>> property)
        {
            if (property == null) throw new ArgumentNullException("property");

            var pInfo = property.GetParameterPropertyInfo();
            return new ModelPropertyBuilder<IList<TValue>>(pInfo, ModelPropertyType.Content, ModelPropertyMultiplicity.OrderedCollection);
        }
        protected static ModelPropertyBuilder<IList<TValue>> RegisterOrderedContents<TValue>(PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException("property");

            return new ModelPropertyBuilder<IList<TValue>>(property, ModelPropertyType.Content, ModelPropertyMultiplicity.OrderedCollection);
        }

        protected static ModelPropertyBuilder<TValue> RegisterReference<TValue>(Expression<Func<TElement, TValue>> property)
        {
            if (property == null) throw new ArgumentNullException("property");

            var pInfo = property.GetParameterPropertyInfo();
            return new ModelPropertyBuilder<TValue>(pInfo, ModelPropertyType.Reference, ModelPropertyMultiplicity.Single);
        }
        protected static ModelPropertyBuilder<TValue> RegisterReference<TValue>(PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException("property");

            return new ModelPropertyBuilder<TValue>(property, ModelPropertyType.Reference, ModelPropertyMultiplicity.Single);
        }
        protected static ModelPropertyBuilder<ICollection<TValue>> RegisterReferences<TValue>(Expression<Func<TElement, ICollection<TValue>>> property)
        {
            if (property == null) throw new ArgumentNullException("property");

            var pInfo = property.GetParameterPropertyInfo();
            return new ModelPropertyBuilder<ICollection<TValue>>(pInfo, ModelPropertyType.Reference, ModelPropertyMultiplicity.Collection);
        }
        protected static ModelPropertyBuilder<ICollection<TValue>> RegisterReferences<TValue>(PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException("property");

            return new ModelPropertyBuilder<ICollection<TValue>>(property, ModelPropertyType.Reference, ModelPropertyMultiplicity.Collection);
        }
        protected static ModelPropertyBuilder<IList<TValue>> RegisterOrderedReferences<TValue>(Expression<Func<TElement, IList<TValue>>> property)
        {
            if (property == null) throw new ArgumentNullException("property");

            var pInfo = property.GetParameterPropertyInfo();
            return new ModelPropertyBuilder<IList<TValue>>(pInfo, ModelPropertyType.Reference, ModelPropertyMultiplicity.OrderedCollection);
        }
        protected static ModelPropertyBuilder<IList<TValue>> RegisterOrderedReferences<TValue>(PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException("property");

            return new ModelPropertyBuilder<IList<TValue>>(property, ModelPropertyType.Reference, ModelPropertyMultiplicity.OrderedCollection);
        }
        protected static ModelPropertyBuilder<IEnumerable<TValue>> RegisterComputedReferences<TValue>(Expression<Func<TElement, IEnumerable<TValue>>> property, Func<TElement, IEnumerable<TValue>> computedValue)
        {
            if (property == null) throw new ArgumentNullException("property");
            if (computedValue == null) throw new ArgumentNullException("computedValue");

            var pInfo = property.GetParameterPropertyInfo();
            return new ModelPropertyBuilder<IEnumerable<TValue>>(pInfo, ModelPropertyType.Reference, ModelPropertyMultiplicity.Collection)
                .WithComputedValue(computedValue);
        }
        protected static ModelPropertyBuilder<IEnumerable<TValue>> RegisterComputedReferences<TValue>(PropertyInfo property, Func<TElement, IEnumerable<TValue>> computedValue)
        {
            if (property == null) throw new ArgumentNullException("property");
            if (computedValue == null) throw new ArgumentNullException("computedValue");

            return new ModelPropertyBuilder<IEnumerable<TValue>>(property, ModelPropertyType.Reference, ModelPropertyMultiplicity.Collection)
                .WithComputedValue(computedValue);
        }

        protected static IModelProperty<TValue> RegisterReadOnly<TValue>(IModelProperty<TValue> property)
        {
            if (property == null) throw new ArgumentNullException("property");
            if (property.IsReadOnly) throw new ArgumentException("property");

            return RegisterProperty(() => new ModelProperty<TValue>(property));
        }
        
        #region [ ModelPropertyBuilder ]

        internal sealed class ModelProperty<TValue> : ModelProperty, IModelProperty<TValue>
        {
            private readonly Func<TValue> _defaultValueFunc;
            private readonly Func<TElement, TValue> _computeValueFunc;

            internal ModelProperty(PropertyInfo property, ModelPropertyType modelPropertyType, ModelPropertyMultiplicity modelPropertyMultiplicity, Func<string> friendlyNameFunc, IModelProperty opposite, Func<TValue> defaultValueFunc, Func<TElement, TValue> computeValueFunc)
                : base(property, modelPropertyType, modelPropertyMultiplicity, friendlyNameFunc, opposite)
            {
                _defaultValueFunc = defaultValueFunc;
                _computeValueFunc = computeValueFunc;
            }

            internal ModelProperty(IModelProperty<TValue> underlyingProperty) 
                : base(underlyingProperty)
            {
            }

            public Func<TValue> DefaultValueFunc
            {
                get { return _defaultValueFunc; }
            }

            public Func<TElement, TValue> ComputeValueFunc
            {
                get { return _computeValueFunc; }
            }
        }

        protected class ModelPropertyBuilder<TValue>
        {
            internal readonly PropertyInfo _property;
            internal readonly ModelPropertyType _modelPropertyType;
            internal readonly ModelPropertyMultiplicity _modelPropertyMultiplicity;
            internal Func<string> _friendlyNameFunc;
            internal IModelProperty _opposite;
            internal bool _oppositeSelf;
            internal Func<TValue> _defaultValueFunc;
            internal Func<TElement, TValue> _computeValueFunc;
            //internal bool _isReadOnly;
            //internal IModelProperty _underlyingProperty;

            internal ModelPropertyBuilder(PropertyInfo property, ModelPropertyType modelPropertyType, ModelPropertyMultiplicity modelPropertyMultiplicity)
            {
                if (property.GetGetMethod(false).IsStatic)
                    throw new ArgumentException(string.Format("Cannot register static property {0}", property), "property");
                if (!property.CanRead || property.GetGetMethod(false) == null)
                    throw new ArgumentException(string.Format("Cannot register property {0} without read/get method", property), "property");
                if (property.DeclaringType != typeof(TElement))
                    throw new ArgumentException(string.Format("Cannot register property {0} on a different element type {1}", property, typeof(TElement).FullName), "property");

                var genericTypes = property.PropertyType.GetGenericArgsFor(typeof(IEnumerable<>), true);
                switch (modelPropertyType)
                {
                    case ModelPropertyType.Property:
                        if (typeof(IModelElement).IsAssignableFrom(property.PropertyType) ||
                            genericTypes != null && typeof(IModelElement).IsAssignableFrom(genericTypes[0]))
                            throw new ArgumentException(string.Format("Plain property {0} cannot reference model elements. Use other Register method appropriate for this relationship.", property), "property");
                        break;
                    default:
                        if (!typeof(IModelElement).IsAssignableFrom(property.PropertyType) &&
                           !(genericTypes != null && typeof(IModelElement).IsAssignableFrom(genericTypes[0])))
                            throw new ArgumentException(string.Format("Model property {0} must reference model elements. Use other Register method appropriate for this relationship.", property), "property");
                        break;
                }

                switch (modelPropertyType)
                {
                    case ModelPropertyType.Property:
                        if (modelPropertyMultiplicity != ModelPropertyMultiplicity.Single)
                            throw new ArgumentException(string.Format("Property {0} must have single multiplicity", property), "modelPropertyMultiplicity");
                        break;
                    case ModelPropertyType.Container:
                        if (modelPropertyMultiplicity != ModelPropertyMultiplicity.Single)
                            throw new ArgumentException(string.Format("Container property {0} must have single multiplicity", property), "modelPropertyMultiplicity");
                        break;
                }

                _property = property;
                _modelPropertyType = modelPropertyType;
                _modelPropertyMultiplicity = modelPropertyMultiplicity;
            }

            public IModelProperty<TValue> Build()
            {
                var result = ModelElement.RegisterProperty(() =>
                {
                    var property = new ModelProperty<TValue>(
                        _property, _modelPropertyType, _modelPropertyMultiplicity,
                        _friendlyNameFunc, _opposite,
                        _defaultValueFunc, _computeValueFunc);
                    if (_oppositeSelf)
                        property._opposite = property;

                    return property;
                });

                return result;
            }

            public ModelPropertyBuilder<TValue> WithFriendlyName(Func<string> friendlyNameFunc)
            {
                if (friendlyNameFunc == null) throw new ArgumentNullException("friendlyNameFunc");
                if (_friendlyNameFunc != null) throw new InvalidOperationException(string.Format("FriendlyName is already set for property {0}", _property));

                _friendlyNameFunc = friendlyNameFunc;

                return this;
            }
            public ModelPropertyBuilder<TValue> WithFriendlyName(string friendlyName)
            {
                if (friendlyName == null) throw new ArgumentNullException("friendlyName");
                if (_friendlyNameFunc != null) throw new InvalidOperationException(string.Format("FriendlyName is already set for property {0}", _property));

                _friendlyNameFunc = () => friendlyName;

                return this;
            }

            public ModelPropertyBuilder<TValue> WithOpposite(IModelProperty<TElement> opposite)
            {
                WithOppositeInternal(opposite);

                return this;
            }
            public ModelPropertyBuilder<TValue> WithOpposite(IModelProperty<ICollection<TElement>> opposite)
            {
                WithOppositeInternal(opposite);

                return this;
            }
            public ModelPropertyBuilder<TValue> WithOpposite(IModelProperty<IList<TElement>> opposite)
            {
                WithOppositeInternal(opposite);

                return this;
            }
            public ModelPropertyBuilder<TValue> IsSelfOpposite()
            {
                if (_opposite != null || _oppositeSelf)
                    throw new ArgumentException(string.Format("Property {0} is already bound", _property));
                if (_modelPropertyMultiplicity == ModelPropertyMultiplicity.OrderedCollection)
                    throw new ArgumentException(string.Format("Ordered collection property {0} cannot be self opposite", _property));
                if (_modelPropertyType != ModelPropertyType.Reference)
                    throw new ArgumentException(string.Format("Only reference properties can be self opposite: {0}", _property));

                _oppositeSelf = true;

                return this;
            }
            private void WithOppositeInternal(IModelProperty opposite)
            {
                if (opposite == null) throw new ArgumentNullException("opposite");
                if (_opposite != null || _oppositeSelf)
                    throw new ArgumentException(string.Format("Property {0} is already bound", _property), "opposite");
                var prop = opposite as ModelProperty;
                if (prop.Opposite != null) // Recheck this condition when building property
                    throw new ArgumentException(string.Format("Opposite property {0} is already bound for property {1}", prop, _property), "opposite");
                if (_modelPropertyMultiplicity == ModelPropertyMultiplicity.OrderedCollection && opposite.ModelPropertyMultiplicity == ModelPropertyMultiplicity.OrderedCollection)
                    throw new ArgumentException(string.Format("Ordered collection properties {0} and {1} cannot be opposites", _property, opposite), "opposite");
                switch (_modelPropertyType)
                {
                    case ModelPropertyType.Property:
                        throw new ArgumentException(string.Format("Plain property {0} cannot have opposite", _property), "opposite");
                    case ModelPropertyType.Content:
                        if (opposite.ModelPropertyType != ModelPropertyType.Container)
                            throw new ArgumentException(string.Format("Content property's {0} opposite {1} must be a container property", _property, opposite), "opposite");
                        break;
                    case ModelPropertyType.Container:
                        if (opposite.ModelPropertyType != ModelPropertyType.Content)
                            throw new ArgumentException(string.Format("Container property's {0} opposite {1} must be a content property", _property, opposite), "opposite");
                        break;
                    case ModelPropertyType.Reference:
                        if (opposite.ModelPropertyType != ModelPropertyType.Content)
                            throw new ArgumentException(string.Format("Reference property's {0} opposite {1} must be a reference property", _property, opposite), "opposite");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _opposite = opposite;
            }

            public ModelPropertyBuilder<TValue> WithDefaultValue(TValue defaultValue)
            {
                return WithDefaultValue(() => defaultValue);
            }
            public ModelPropertyBuilder<TValue> WithDefaultValue(Func<TValue> defaultValue)
            {
                if (defaultValue == null) throw new ArgumentNullException("defaultValue");
                if (_defaultValueFunc != null)
                    throw new ArgumentException(string.Format("Property {0} already have default value", _property), "defaultValue");
                if (_modelPropertyType != ModelPropertyType.Property)
                    throw new ArgumentException(string.Format("Property {0} is not plain so it cannot have default value", _property), "defaultValue");

                _defaultValueFunc = defaultValue;

                return this;
            }

            public ModelPropertyBuilder<TValue> WithComputedValue(Func<TElement, TValue> computedValue)
            {
                if (computedValue == null) throw new ArgumentNullException("computedValue");
                if (_defaultValueFunc != null)
                    throw new ArgumentException(string.Format("Property {0} already have computed value", _property), "computedValue");
                if (_modelPropertyType != ModelPropertyType.Property && _modelPropertyType != ModelPropertyType.Reference)
                    throw new ArgumentException(string.Format("Property {0} is not plain or reference (single or collection) so it cannot have computed value", _property));
                if (_modelPropertyType == ModelPropertyType.Reference && _modelPropertyMultiplicity == ModelPropertyMultiplicity.OrderedCollection)
                    throw new ArgumentException(string.Format("Property {0} is not plain or reference (single or collection) so it cannot have computed value", _property));

                _computeValueFunc = computedValue;

                return this;
            }

        }

        #endregion

    }

}