// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.EP;

#nullable enable
namespace PX.Objects.AP;

public sealed class VendorVisibilityRestriction : PXCacheExtension<
#nullable disable
Vendor>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXDefault(typeof (Search2<APSetup.dfltVendorClassID, InnerJoin<VendorClass, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>, Where<VendorClass.orgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>))]
  [PXSelector(typeof (Search2<VendorClass.vendorClassID, LeftJoin<EPEmployeeClass, On<EPEmployeeClass.vendorClassID, Equal<VendorClass.vendorClassID>>>, Where<VendorClass.orgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>, PX.Data.And<Where<EPEmployeeClass.vendorClassID, PX.Data.IsNull, PX.Data.And<MatchUser>>>>>), DescriptionField = typeof (VendorClass.descr), CacheGlobal = true)]
  public string VendorClassID { get; set; }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXDefault(0, typeof (Search<VendorClass.orgBAccountID, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>))]
  public int? VOrgBAccountID { get; set; }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [RestrictCustomerByUserBranches(typeof (BAccountR.cOrgBAccountID))]
  [RestrictVendorByUserBranches(typeof (BAccountR.vOrgBAccountID))]
  public int? ParentBAccountID { get; set; }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [RestrictVendorByUserBranches(typeof (BAccountR.vOrgBAccountID))]
  public int? PayToVendorID { get; set; }

  public abstract class vOrgBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    VendorVisibilityRestriction.vOrgBAccountID>
  {
  }
}
