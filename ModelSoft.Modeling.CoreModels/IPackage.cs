using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using ModelSoft.Framework.Collections;
using ModelSoft.Framework.DomainObjects;

namespace ModelSoft.Core
{
    public interface IPackage : IUriSpace, IPackageable
    {
        [Content]
        IIndexedList<string, IPackageable> PackagedElements { get; }
    }

    //public class Package : ModelElementBase, IPackage
    //{
    //    static Package()
    //    {
    //        UriModelProperty = RegisterProperty<Package, IWithUri, Uri>(p => p.Uri);
    //        AbsoluteUriModelProperty = RegisterComputedProperty<Package, IWithUri, Uri>(p => p.AbsolteUri, WithUriImplementation.Compute_AbsolteUri);
    //        ContainerSpaceModelProperty = RegisterContainerProperty<Package, IWithUri, IUriSpace>("ContainerSpace");
    //        NamedElementsModelProperty = RegisterChildrenProperty<Package, IUriSpace, string, IWithUri>("NamedElements");
    //        ContainerPackageModelProperty = RegisterContainerProperty<Package, IPackageable, IPackage>(p => p.ContainerPackage);
    //        PackagedElementsModelProperty = RegisterChildrenProperty<Package, IPackage, string, IPackageable>(p => p.PackagedElements);
    //    }

    //    public static readonly IModelProperty<Uri> UriModelProperty;
    //    public Uri Uri
    //    {
    //        get
    //        {
    //            return GetValue(UriModelProperty);
    //        }
    //        set
    //        {
    //            SetValue(UriModelProperty, value);
    //        }
    //    }

    //    public static readonly IModelProperty<Uri> AbsoluteUriModelProperty;
    //    public Uri AbsolteUri
    //    {
    //        get
    //        {
    //            return GetValue(AbsoluteUriModelProperty);
    //        }
    //    }

    //    public static readonly IModelProperty<IUriSpace> ContainerSpaceModelProperty;
    //    [Container]
    //    IUriSpace IWithUri.ContainerSpace
    //    {
    //        get
    //        {
    //            return GetValue(ContainerSpaceModelProperty);
    //        }
    //    }

    //    public static readonly IModelProperty<IIndexedList<string, IWithUri>> NamedElementsModelProperty;
    //    [Content]
    //    IIndexedList<string, IWithUri> IUriSpace.NamedElements
    //    {
    //        get
    //        {
    //            return GetValue(NamedElementsModelProperty);
    //        }
    //    }

    //    public static readonly IModelProperty<IPackage> ContainerPackageModelProperty;
    //    [Container]
    //    public IPackage ContainerPackage
    //    {
    //        get
    //        {
    //            return GetValue(ContainerPackageModelProperty);
    //        }
    //    }

    //    public static readonly IModelProperty<IIndexedList<string, IPackageable>> PackagedElementsModelProperty;
    //    [Content]
    //    public IIndexedList<string, IPackageable> PackagedElements
    //    {
    //        get
    //        {
    //            return GetValue(PackagedElementsModelProperty);
    //        }
    //    }
    //}
}
