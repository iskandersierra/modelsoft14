using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using ModelSoft.Framework.Reflection;

namespace ModelSoft.SharpModels
{
    public abstract class ModelElement :
        IModelElement
    {

        #region [ IModelElement ]

        private readonly ITypeInformation _typeInformation;
        private IModelElement _containerElement;
        private IModelProperty _containerProperty;

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

        public ITypeInformation TypeInformation { get { return _typeInformation; } }

        #endregion

        #region [ TypeInformation ]

        public static IModelElement Create(string url, bool throwIfNull = true)
        {
            IModelElement instance;
            if (TryCreate(url, out instance))
                return instance;
            if (throwIfNull)
                throw new ArgumentException(string.Format("Url {0} is invalid as a model class", url));
            return null;
        }

        public static IModelElement Create(Type modelType, bool throwIfNull = true)
        {
            IModelElement instance;
            if (TryCreate(modelType, out instance))
                return instance;
            if (throwIfNull)
                throw new ArgumentException(string.Format("Type {0} is invalid as a model class", modelType.FullName));
            return null;
        }
        public static TModelElement Create<TModelElement>(bool throwIfNull = true)
            where TModelElement : class, IModelElement
        {
            var instance = Create(typeof (TModelElement), throwIfNull);
            return (TModelElement) instance;
        }

        public static bool TryCreate(string url, out IModelElement instance)
        {
            ITypeInformation typeInformation;
            if (TryGetTypeInformation(url, out typeInformation))
            {
                instance = CreateInternal(typeInformation);
                return true;
            }
            instance = null;
            return false;
        }
        public static bool TryCreate(Type modelType, out IModelElement instance)
        {
            ITypeInformation typeInformation;
            if (TryGetTypeInformation(modelType, out typeInformation))
            {
                instance = CreateInternal(typeInformation);
                return true;
            }
            instance = null;
            return false;
        }

        public static bool TryCreate<TModelElement>(out TModelElement instance)
            where TModelElement : class, IModelElement
        {
            IModelElement result;
            if (TryCreate(typeof (TModelElement), out result))
            {
                instance = result as TModelElement;
                return instance != null;
            }
            instance = null;
            return false;
        }

        private static IModelElement CreateInternal(ITypeInformation type)
        {
            var instance = type.CreateInstance();
            return instance;
        }

        public static ITypeInformation GetTypeInformation(Type type)
        {
            ITypeInformation result;
            if (TryGetTypeInformation(type, out result))
                return result;
            throw new ArgumentException(string.Format("Type {0} is invalid as a model class", type.FullName));
        }
        public static ITypeInformation GetTypeInformation(string url)
        {
            ITypeInformation result;
            if (TryGetTypeInformation(url, out result))
                return result;
            throw new ArgumentException(string.Format("Url {0} is invalid as a model class", url));
        }
        public static ITypeInformation GetTypeInformation<TModelElement>()
            where TModelElement : IModelElement
        {
            return GetTypeInformation(typeof(TModelElement));
        }

        public static bool TryGetTypeInformation(Type type, out ITypeInformation typeInformation)
        {
            if (type == null) throw new ArgumentNullException("type");
            TypeInformationImpl impl;
            if (TryGetTypeInformationInternal(type, out impl))
            {
                typeInformation = impl;
                return true;
            }
            typeInformation = null;
            return false;
        }
        public static bool TryGetTypeInformation(string url, out ITypeInformation typeInformation)
        {
            if (url == null) throw new ArgumentNullException("url");
            TypeInformationImpl impl;
            if (TryGetTypeInformationInternal(url, out impl))
            {
                typeInformation = impl;
                return true;
            }
            typeInformation = null;
            return false;
        }

        public static bool TryGetTypeInformation<TModelElement>(out ITypeInformation typeInformation)
            where TModelElement : IModelElement
        {
            var result = TryGetTypeInformation(typeof (TModelElement), out typeInformation);
            return result;
        }

        private static Dictionary<string, TypeInformationImpl> _typesRegistryByUrl = new Dictionary<string,TypeInformationImpl>(StringComparer.OrdinalIgnoreCase);
        private static Dictionary<Type, TypeInformationImpl> _typesRegistry = new Dictionary<Type, TypeInformationImpl>();

        private static bool TryGetTypeInformationInternal(string url, out TypeInformationImpl typeInformation)
        {
            lock (_registryLock)
            {
                if (_typesRegistryByUrl.TryGetValue(url, out typeInformation))
                {
                    return true;
                }
            }

            return false;
        }
        private static bool TryGetTypeInformationInternal(Type type, out TypeInformationImpl typeInformation)
        {
            lock (_registryLock)
            {
                if (!_typesRegistry.TryGetValue(type, out typeInformation))
                {
                    if (!TryCreateTypeInformationInternal(type, out typeInformation))
                        return false;

                    if (_typesRegistryByUrl.ContainsKey(typeInformation.Url))
                        return false;

                    _typesRegistry.Add(typeInformation.Interface, typeInformation);
                    _typesRegistry.Add(typeInformation.ClassType, typeInformation);
                    _typesRegistryByUrl.Add(typeInformation.Url, typeInformation);
                }
            }

            return true;
        }

        private static bool TryCreateTypeInformationInternal(Type type, out TypeInformationImpl typeInformation)
        {
            Type classType;
            Type interfaceType;
            if (type.IsClass)
            {
                interfaceType = ImplementsAttribute.ElementImplements(type);
                if (interfaceType != null)
                {
                    classType = ImplementedByAttribute.ElementIsImplementedBy(interfaceType);
                    if (classType == type)
                    {
                        if (TryCreateTypeInformationInternal(classType, interfaceType, out typeInformation))
                            return true;
                    }
                }
            }
            else if (type.IsInterface)
            {
                classType = ImplementedByAttribute.ElementIsImplementedBy(type);
                if (classType != null)
                {
                    interfaceType = ImplementsAttribute.ElementImplements(classType);
                    if (interfaceType == type)
                    {
                        if (TryCreateTypeInformationInternal(classType, interfaceType, out typeInformation))
                            return true;
                    }
                }
            }

            typeInformation = null;
            return false;
        }

        private static bool TryCreateTypeInformationInternal(Type classType, Type interfaceType,
            out TypeInformationImpl typeInformation)
        {
            var url = ModelElementUrlAttribute.GetElementUrl(interfaceType);
            if (url != null)
            {
                var constructorInfo = classType.GetConstructor(Type.EmptyTypes);
                if (constructorInfo != null)
                {
                    typeInformation = new TypeInformationImpl(classType, interfaceType, url, constructorInfo);
                    return true;
                }
            }

            typeInformation = null;
            return false;
        }

        internal class TypeInformationImpl : ITypeInformation
        {
            private readonly Type _classType;
            private readonly Type _interface;
            private ConcurrentDictionary<string, ModelProperty> _propertiesByName = new ConcurrentDictionary<string, ModelProperty>();
            private string _url;
            private Func<IModelElement> _constructor;

            internal TypeInformationImpl(Type classType, Type interfaceType, string url, ConstructorInfo constructorInfo)
            {
                if (classType == null) throw new ArgumentNullException("classType");
                _classType = classType;
                _interface = interfaceType;
                _url = url;
                _constructor = Expression.Lambda<Func<IModelElement>>(
                    body: Expression.New(constructorInfo)
                    ).Compile();
            }

            public Func<IModelElement> Constructor
            {
                get { return _constructor; }
            }

            public Type Interface
            {
                get { return _interface; }
            }

            public bool IsInstance(IModelElement instance)
            {
                if (instance == null) return false;
                var result = _interface.IsInstanceOfType(instance);
                return result;
            }

            public IModelElement CreateInstance()
            {
                var instance = Constructor();
                return instance;
            }

            public string Url
            {
                get { return _url; }
            }

            public IEnumerable<IModelProperty> Properties
            {
                get
                {
                    return _propertiesByName.Values.AsEnumerable();
                }
            }

            public IModelProperty GetProperty(string propertyName, bool throwIfNotFound = true)
            {
                IModelProperty property;
                if (TryGetProperty(propertyName, out property))
                    return property;
                if (throwIfNotFound)
                    throw new ArgumentException(string.Format("Property {0} not found on type {1}", propertyName, this));
                return null;
            }

            public bool TryGetProperty(string propertyName, out IModelProperty property)
            {
                if (propertyName == null) throw new ArgumentNullException("propertyName");
                ModelProperty value;
                if (_propertiesByName.TryGetValue(propertyName, out value))
                {
                    property = value;
                    return true;
                }
                property = null;
                return false;
            }

            public Type ClassType
            {
                get { return _classType; }
            }

            public ConcurrentDictionary<string, ModelProperty> PropertiesByName
            {
                get { return _propertiesByName; }
            }
        }

        #endregion

        #region [ RegisterProperty ]

        private static readonly object _registryLock = new object();
        private static long _nextPropertyIdentifier = 1;
        private static ConcurrentDictionary<long, ModelProperty> _propertiesById = new ConcurrentDictionary<long, ModelProperty>();
        private static ConcurrentDictionary<PropertyInfo, ModelProperty> _propertiesByPropertyInfo = new ConcurrentDictionary<PropertyInfo, ModelProperty>();

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
                    var typeInfo = (TypeInformationImpl)GetTypeInformation(declaringType);

                    if (typeInfo.GetProperty(property.Name, false) != null)
                        throw new InvalidOperationException(string.Format("Type {0} already define a property with name {1}", declaringType.FullName, property.Name));

                    property._identifier = _nextPropertyIdentifier++;
                    property._typeInformation = typeInfo;

                    _propertiesById.TryAdd(property._identifier, property);
                    _propertiesByPropertyInfo.TryAdd(property._property, property);
                    typeInfo.PropertiesByName.TryAdd(property.Name, property);
                }

                return evaluation;
            }
        }

        #endregion

        #region [ Get/SetValue ]

        //private object _fieldsLock = new object();
        private IDictionary<ModelProperty, FieldValue> _fields;

        protected object GetValue(IModelProperty property)
        {
            var prop = CheckProperty(property, false);
            FieldValue field;
            if (_fields == null || !_fields.TryGetValue(prop, out field))
                field = FieldValue.UnsetValue;

            var result = ((ModelProperty)property).GetValue(this, field);

            return result;
        }

        protected TValue GetValue<TValue>(IModelProperty<TValue> property)
        {
            var result = GetValue((IModelProperty)property);

            return (TValue)result;
        }

        protected void SetValue<TValue>(IModelProperty<TValue> property, TValue value)
        {
            var prop = CheckProperty(property, true);
            FieldValue field;
            if (_fields == null)
                _fields = new Dictionary<ModelProperty, FieldValue>();
            if (!_fields.TryGetValue(prop, out field))
            {
                field = new FieldValue();
                _fields.Add(prop, field);
            }
            prop.SetValue(this, field, value);
        }

        private ModelProperty CheckProperty(IModelProperty property, bool forWrite)
        {
            if (property == null) throw new ArgumentNullException("property");
            var result = property as ModelProperty;
            if (result == null) throw new NotSupportedException(string.Format("property {0} must be an instance of ModelProperty class", property));
            if (result.TypeInformation.ClassType != this.GetType())
                throw new NotSupportedException(string.Format("This object do not support given property {0}", property));
            if (forWrite && result.IsReadOnly)
                throw new NotSupportedException(string.Format("Cannot use read-only property {0} to modify element state", property));

            return result;
        }

        #endregion

        #region [ INotifyPropertyChanged/Changing ]

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;
        public event EventHandler<ModelPropertyChangedEventArgs> ModelPropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException("propertyName");
            var property = this.TypeInformation.GetProperty(propertyName);
            OnPropertyChanged(property);
        }
        protected void OnPropertyChanged(IModelProperty property)
        {
            if (property == null) throw new ArgumentNullException("property");
            if (!property.DeclaringType.IsInstance(this)) throw new ArgumentException(string.Format("Property {0} not accepted for instance {1}", property, this));
            var args = new PropertyChangedEventArgs(property.Name);
            if (PropertyChanged != null)
                PropertyChanged(this, args);
            var modelArgs = new ModelPropertyChangedEventArgs(this, property, args);
            OnModelPropertyChanged(modelArgs);
            OnPropertyChangedOverride(property);
        }
        protected virtual void OnPropertyChangedOverride(IModelProperty property)
        {

        }

        protected void OnPropertyChanging(PropertyChangingEventArgs args)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, args);
        }
        protected void OnPropertyChanging(string propertyName)
        {
            OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
        }
        protected void OnPropertyChanging(IModelProperty property)
        {
            OnPropertyChanging(property.Name);
        }

        private void OnModelPropertyChanged(ModelPropertyChangedEventArgs args)
        {
            if (ModelPropertyChanged != null)
                ModelPropertyChanged(this, args);

            var parent = this.GetContainerElement() as ModelElement;
            if (parent != null)
                parent.OnModelPropertyChanged(args);
        }

        #endregion

        #region [ ModelProperty ]

        internal abstract class ModelProperty : IModelProperty
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
            internal TypeInformationImpl _typeInformation;

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

            public ITypeInformation DeclaringType
            {
                get
                {
                    return TypeInformation;
                }
            }

            public bool IsReadOnly
            {
                get { return _isReadOnly; }
            }

            internal TypeInformationImpl TypeInformation
            {
                get
                {
                    return _underlyingProperty != null
                        ? ((ModelProperty)_underlyingProperty)._typeInformation
                        : _typeInformation;
                }
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

            public object GetValue(ModelElement element, FieldValue field)
            {
                if (field.IsSet)
                    return field.Value;
                var result = GetDefaultValue((ModelElement)element, field);
                return result;
            }

            public void SetValue(ModelElement element, FieldValue field, object value)
            {
                // compare values 
                var oldValue = GetValue(element, field);
                if (Equals(oldValue, value))
                    return;

                element.OnPropertyChanging(this);
                field.Value = value;
                field.IsSet = true;
                element.OnPropertyChanged(this);
            }

            protected abstract object GetDefaultValue(ModelElement element, FieldValue field);
        }
        #endregion

        #region [ FieldValue ]

        protected internal class FieldValue
        {
            internal static readonly FieldValue UnsetValue = new FieldValue();

            public object Value;
            public bool IsSet;
        }

        #endregion [ FieldValue ]
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
            private readonly Type TypeOfValue = typeof(TValue);

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

            protected override object GetDefaultValue(ModelElement element, FieldValue field)
            {
                object result;
                if (DefaultValueFunc != null)
                {
                    result = DefaultValueFunc();
                }
                else if (ComputeValueFunc != null)
                {
                    result = ComputeValueFunc((TElement)element);
                }
                else
                {
                    if (TypeOfValue.IsClass)
                        result = null;
                    else
                        result = Activator.CreateInstance<TValue>();
                }
                return result;
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