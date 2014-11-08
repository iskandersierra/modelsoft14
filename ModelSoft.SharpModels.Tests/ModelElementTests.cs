using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelSoft.Framework;

namespace ModelSoft.SharpModels.Tests
{
    [TestClass]
    public class ModelElementTests
    {
        [TestMethod]
        public void ModelElement_SampleModel_Initialization()
        {
            CheckPropertyAsserts(SampleModel.FirstNameProperty, "FirstName", "First name", ModelPropertyType.Property, ModelPropertyMultiplicity.Single, null, false);
            CheckPropertyAsserts(SampleModel.BirthDateProperty, "BirthDate", "Birth date", ModelPropertyType.Property, ModelPropertyMultiplicity.Single, null, false);
            CheckPropertyAsserts(SampleModel.AgePropertyKey, "Age", "Age", ModelPropertyType.Property, ModelPropertyMultiplicity.Single, null, false);
            CheckPropertyAsserts(SampleModel.AgeProperty, "Age", "Age", ModelPropertyType.Property, ModelPropertyMultiplicity.Single, null, true);
            CheckPropertyAsserts(SampleModel.SexProperty, "Sex", "Gender", ModelPropertyType.Property, ModelPropertyMultiplicity.Single, null, false);
            CheckPropertyAsserts(SampleModel.FatherProperty, "Father", "Father", ModelPropertyType.Container, ModelPropertyMultiplicity.Single, SampleModel.ChildrenProperty, false);
            CheckPropertyAsserts(SampleModel.ChildrenProperty, "Children", "Children", ModelPropertyType.Content, ModelPropertyMultiplicity.OrderedCollection, SampleModel.FatherProperty, false);
            CheckPropertyAsserts(SampleModel.BestFriendProperty, "BestFriend", "Best friend", ModelPropertyType.Reference, ModelPropertyMultiplicity.Single, SampleModel.BestFriendProperty, false);
            CheckPropertyAsserts(SampleModel.FriendsProperty, "Friends", "Friends", ModelPropertyType.Reference, ModelPropertyMultiplicity.Collection, SampleModel.FriendsProperty, false);
            CheckPropertyAsserts(SampleModel.SocialMediaPropertyKey, "SocialMedia", "Social media", ModelPropertyType.Reference, ModelPropertyMultiplicity.Collection, null, false);
            CheckPropertyAsserts(SampleModel.SocialMediaProperty, "SocialMedia", "Social media", ModelPropertyType.Reference, ModelPropertyMultiplicity.Collection, null, true);

            var typeInformation = ModelElement.GetTypeInformation(typeof(ISampleModel));
            var typeInformation2 = ModelElement.GetTypeInformation<ISampleModel>();

            Assert.IsNotNull(typeInformation);
            Assert.IsNotNull(typeInformation2);
            Assert.AreSame(typeInformation, typeInformation2);
        }

        private static void CheckPropertyAsserts(IModelProperty property, string propertyName, string friendlyName, ModelPropertyType type, ModelPropertyMultiplicity multiplicity, IModelProperty opposite, bool isReadOnly)
        {
            Assert.IsNotNull(property, propertyName);
            Assert.AreNotEqual(0L, property.Identifier);
            Assert.AreEqual(propertyName, property.Name);
            Assert.AreEqual(friendlyName ?? propertyName, property.FriendlyName);
            Assert.AreEqual(type, property.ModelPropertyType);
            Assert.AreEqual(multiplicity, property.ModelPropertyMultiplicity);
            Assert.AreEqual(opposite, property.Opposite);
            Assert.AreEqual(isReadOnly, property.IsReadOnly);
        }

        [TestMethod]
        public void ModelElement_SampleModel_NewInstance()
        {
            var model = ModelElement.Create<ISampleModel>();
            Assert.IsNull(model.FirstName, "FirstName");

            model.FirstName = "Foo";
            Assert.AreEqual("Foo", model.FirstName, "FirstName");

        }

        public enum Sex { NotSet, Male, Female }

        [ModelElementUrl("SampleModel")]
        [ImplementedBy(typeof(SampleModel))]
        [IsAbstract(false)]
        public interface ISampleModel : IModelElement
        {
            string FirstName { get; set; }
            DateTime? BirthDate { get; set; }
            int? Age { get; set; }
            Sex Sex { get; set; }
            SampleModel Father { get; set; }
            IList<SampleModel> Children { get; set; }
            SampleModel BestFriend { get; set; }
            ICollection<SampleModel> Friends { get; set; }
            IEnumerable<SampleModel> SocialMedia { get; set; }
        }

        [Implements(typeof(ISampleModel))]
        public class SampleModel : ModelElement<SampleModel>, ISampleModel
        {
            public static readonly IModelProperty<string> FirstNameProperty;
            public static readonly IModelProperty<DateTime?> BirthDateProperty;
            internal static readonly IModelProperty<int?> AgePropertyKey;
            public static readonly IModelProperty<int?> AgeProperty;
            public static readonly IModelProperty<Sex> SexProperty;
            public static readonly IModelProperty<SampleModel> FatherProperty;
            public static readonly IModelProperty<IList<SampleModel>> ChildrenProperty;
            public static readonly IModelProperty<SampleModel> BestFriendProperty;
            public static readonly IModelProperty<ICollection<SampleModel>> FriendsProperty;
            internal static readonly IModelProperty<IEnumerable<SampleModel>> SocialMediaPropertyKey;
            public static readonly IModelProperty<IEnumerable<SampleModel>> SocialMediaProperty;

            static SampleModel()
            {
                FirstNameProperty = RegisterProperty(e => e.FirstName)
                    .WithFriendlyName("First name")
                    .Build();

                BirthDateProperty = RegisterProperty(e => e.BirthDate)
                    .WithFriendlyName(() => "Birth date")
                    .Build();

                AgePropertyKey = RegisterProperty(e => e.Age)
                    .WithFriendlyName(() => "Age")
                    .WithComputedValue(ComputeAge)
                    .Build();
                AgeProperty = RegisterReadOnly(AgePropertyKey);

                SexProperty = RegisterProperty(e => e.Sex)
                    .WithFriendlyName("Gender")
                    .WithDefaultValue(Sex.NotSet)
                    .Build();

                FatherProperty = RegisterContainer(e => e.Father)
                    .Build();

                ChildrenProperty = RegisterOrderedContents(e => e.Children)
                    .WithOpposite(FatherProperty)
                    .Build();

                BestFriendProperty = RegisterReference(e => e.BestFriend)
                    .WithFriendlyName("Best friend")
                    .IsSelfOpposite()
                    .Build();

                FriendsProperty = RegisterReferences(e => e.Friends)
                    .IsSelfOpposite()
                    .Build();

                SocialMediaPropertyKey = RegisterComputedReferences(e => e.SocialMedia, ComputeSocialMedia)
                    .WithFriendlyName("Social media")
                    .Build();
                SocialMediaProperty = RegisterReadOnly(SocialMediaPropertyKey);

            }

            public string FirstName { get { return GetValue(FirstNameProperty); } set { SetValue(FirstNameProperty, value); } }

            public DateTime? BirthDate { get { return GetValue(BirthDateProperty); } set { SetValue(BirthDateProperty, value); } }
            public int? Age { get { return GetValue(AgeProperty); } set { SetValue(AgeProperty, value); } }
            public Sex Sex { get { return GetValue(SexProperty); } set { SetValue(SexProperty, value); } }

            public SampleModel Father { get { return GetValue(FatherProperty); } set { SetValue(FatherProperty, value); } }
            public IList<SampleModel> Children { get { return GetValue(ChildrenProperty); } set { SetValue(ChildrenProperty, value); } }
            public SampleModel BestFriend { get { return GetValue(BestFriendProperty); } set { SetValue(BestFriendProperty, value); } }
            public ICollection<SampleModel> Friends { get { return GetValue(FriendsProperty); } set { SetValue(FriendsProperty, value); } }
            public IEnumerable<SampleModel> SocialMedia { get { return GetValue(SocialMediaProperty); } set { SetValue(SocialMediaProperty, value); } }


            private static int? ComputeAge(SampleModel element)
            {
                if (element.BirthDate == null) return null;
                var birthDate = element.BirthDate.Value;
                var today = DateTime.Today;
                var yearDiff = today.Year - birthDate.Year;
                today = today.AddYears(-yearDiff);
                if (today < birthDate)
                    yearDiff--;
                return yearDiff;
            }

            private static IEnumerable<SampleModel> ComputeSocialMedia(SampleModel element)
            {
                return element.DepthFirstSearch(e => e.Friends);
            }
        }
    }
}
