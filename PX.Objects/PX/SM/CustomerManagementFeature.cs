// Decompiled with JetBrains decompiler
// Type: PX.SM.CustomerManagementFeature
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;

#nullable enable
namespace PX.SM;

[PXHidden]
public class CustomerManagementFeature : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  public virtual bool? IsInstalled
  {
    get => new bool?(PXAccess.FeatureInstalled<FeaturesSet.customerModule>());
  }

  [PXBool]
  public virtual bool? IsOutlookIntegrationInstalled
  {
    get => new bool?(PXAccess.FeatureInstalled<FeaturesSet.outlookIntegration>());
  }

  [PXBool]
  public virtual bool? IsOpenIDConnectInstalled
  {
    get => new bool?(PXAccess.FeatureInstalled<FeaturesSet.openIDConnect>());
  }

  [PXBool]
  public virtual bool? IsOutlookOidcEnabled { get; set; }

  public abstract class isInstalled : 
    BqlType<IBqlBool, bool>.Field<
    #nullable disable
    CustomerManagementFeature.isInstalled>
  {
  }

  public abstract class isOutlookIntegrationInstalled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerManagementFeature.isOutlookIntegrationInstalled>
  {
  }

  public abstract class isOpenIDConnectInstalled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerManagementFeature.isOpenIDConnectInstalled>
  {
  }

  public abstract class isOutlookOidcEnabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerManagementFeature.isOutlookOidcEnabled>
  {
  }
}
