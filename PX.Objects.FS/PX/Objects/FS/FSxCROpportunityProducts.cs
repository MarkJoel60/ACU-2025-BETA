// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxCROpportunityProducts
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;

#nullable enable
namespace PX.Objects.FS;

public class FSxCROpportunityProducts : PXCacheExtension<
#nullable disable
CROpportunityProducts>
{
  protected int? _VendorLocationID;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>() && PXAccess.FeatureInstalled<FeaturesSet.customerModule>();
  }

  [PXDBString(4, IsFixed = true)]
  [ListField_BillingRule.List]
  [PXDefault("FLRA")]
  [PXUIField(DisplayName = "Billing Rule")]
  public virtual string BillingRule { get; set; }

  [PXDBTimeSpanLong]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Estimated Duration")]
  [PXFormula(typeof (Default<CROpportunityProducts.inventoryID>))]
  public virtual int? EstimatedDuration { get; set; }

  [PXFormula(typeof (Default<CROpportunityProducts.vendorID>))]
  [PXDefault(typeof (Coalesce<Search<INItemSiteSettings.preferredVendorLocationID, Where<INItemSiteSettings.inventoryID, Equal<Current2<CROpportunityProducts.inventoryID>>, And<INItemSiteSettings.preferredVendorID, Equal<Current2<CROpportunityProducts.vendorID>>>>>, Search2<PX.Objects.AP.Vendor.defLocationID, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.locationID, Equal<PX.Objects.AP.Vendor.defLocationID>, And<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.AP.Vendor.bAccountID>>>>, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current2<CROpportunityProducts.vendorID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.vBranchID>>>>>>))]
  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current2<CROpportunityProducts.vendorID>>>))]
  public virtual int? VendorLocationID { get; set; }

  [PXBool]
  [PXUIField(Visible = false)]
  [PXDefault]
  public virtual bool? chkServiceManagement => new bool?(true);

  public abstract class billingRule : ListField_BillingRule
  {
  }

  public abstract class estimatedDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxCROpportunityProducts.estimatedDuration>
  {
  }

  public abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxCROpportunityProducts.vendorLocationID>
  {
  }

  public abstract class ChkServiceManagement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxCROpportunityProducts.ChkServiceManagement>
  {
  }
}
