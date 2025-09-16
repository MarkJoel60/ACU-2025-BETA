// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPaymentMultipleBaseCurrenciesRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;

#nullable enable
namespace PX.Objects.AP;

public sealed class APPaymentMultipleBaseCurrenciesRestriction : 
  PXCacheExtension<
  #nullable disable
  APPaymentVisibilityRestriction, APPayment>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<APPayment.branchID>, IsPending>, Null, Case<Where<APPayment.vendorLocationID, PX.Data.IsNull, And<UnattendedMode, Equal<True>>>, Null, Case<Where<Selector<APPayment.vendorLocationID, PX.Objects.CR.Location.vBranchID>, PX.Data.IsNotNull>, Selector<APPayment.vendorLocationID, PX.Objects.CR.Location.vBranchID>, Case<Where<APPayment.vendorID, PX.Data.IsNotNull, PX.Data.And<Not<Selector<APPayment.vendorID, Vendor.vOrgBAccountID>, RestrictByBranch<Current2<APPayment.branchID>>>>>, Null, Case<Where<Current2<APPayment.branchID>, PX.Data.IsNotNull>, Current2<APPayment.branchID>, Case<Where<APPayment.vendorID, PX.Data.IsNotNull, And<Selector<APPayment.vendorID, Vendor.baseCuryID>, PX.Data.IsNotNull, And<Selector<APPayment.vendorID, Vendor.baseCuryID>, NotEqual<Current<AccessInfo.baseCuryID>>>>>, Null>>>>>>, Current<AccessInfo.branchID>>))]
  public int? BranchID { get; set; }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXRestrictor(typeof (Where<Current2<APPaymentMultipleBaseCurrenciesRestriction.branchBaseCuryID>, PX.Data.IsNull, Or<Vendor.baseCuryID, PX.Data.IsNull, Or<Vendor.baseCuryID, Equal<Current2<APPaymentMultipleBaseCurrenciesRestriction.branchBaseCuryID>>>>>), null, new System.Type[] {})]
  public int? VendorID { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<APPayment.branchID, PX.Objects.GL.Branch.baseCuryID>))]
  public string BranchBaseCuryID { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<APPayment.vendorID, Vendor.baseCuryID>))]
  public string VendorBaseCuryID { get; set; }

  public abstract class branchBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPaymentMultipleBaseCurrenciesRestriction.branchBaseCuryID>
  {
  }

  public abstract class vendorBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPaymentMultipleBaseCurrenciesRestriction.vendorBaseCuryID>
  {
  }
}
