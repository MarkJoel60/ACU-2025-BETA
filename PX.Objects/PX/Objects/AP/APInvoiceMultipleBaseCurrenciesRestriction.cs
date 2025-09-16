// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceMultipleBaseCurrenciesRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;

#nullable enable
namespace PX.Objects.AP;

public sealed class APInvoiceMultipleBaseCurrenciesRestriction : 
  PXCacheExtension<
  #nullable disable
  APInvoiceVisibilityRestriction, APInvoice>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<APInvoice.branchID>, IsPending>, Null, Case<Where<APInvoice.vendorLocationID, PX.Data.IsNull, And<UnattendedMode, Equal<True>>>, Null, Case<Where<Selector<APInvoice.vendorLocationID, PX.Objects.CR.Location.vBranchID>, PX.Data.IsNotNull>, Selector<APInvoice.vendorLocationID, PX.Objects.CR.Location.vBranchID>, Case<Where<APInvoice.vendorID, PX.Data.IsNotNull, PX.Data.And<Not<Selector<APInvoice.vendorID, Vendor.vOrgBAccountID>, RestrictByBranch<Current2<APInvoice.branchID>>>>>, Null, Case<Where<Current2<APInvoice.branchID>, PX.Data.IsNotNull>, Current2<APInvoice.branchID>, Case<Where<APInvoice.vendorID, PX.Data.IsNotNull, And<Selector<APInvoice.vendorID, Vendor.baseCuryID>, PX.Data.IsNotNull, And<Selector<APInvoice.vendorID, Vendor.baseCuryID>, NotEqual<Current<AccessInfo.baseCuryID>>>>>, Null>>>>>>, Current<AccessInfo.branchID>>))]
  public int? BranchID { get; set; }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXRestrictor(typeof (Where<Current2<APInvoiceMultipleBaseCurrenciesRestriction.branchBaseCuryID>, PX.Data.IsNull, Or<Vendor.baseCuryID, PX.Data.IsNull, Or<Vendor.baseCuryID, Equal<Current2<APInvoiceMultipleBaseCurrenciesRestriction.branchBaseCuryID>>>>>), null, new System.Type[] {})]
  public int? VendorID { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<APInvoice.branchID, PX.Objects.GL.Branch.baseCuryID>))]
  public string BranchBaseCuryID { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<APInvoice.vendorID, Vendor.baseCuryID>))]
  public string VendorBaseCuryID { get; set; }

  public abstract class branchBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoiceMultipleBaseCurrenciesRestriction.branchBaseCuryID>
  {
  }

  public abstract class vendorBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoiceMultipleBaseCurrenciesRestriction.vendorBaseCuryID>
  {
  }
}
