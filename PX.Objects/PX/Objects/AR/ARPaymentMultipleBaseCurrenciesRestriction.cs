// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPaymentMultipleBaseCurrenciesRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;

#nullable enable
namespace PX.Objects.AR;

public sealed class ARPaymentMultipleBaseCurrenciesRestriction : 
  PXCacheExtension<
  #nullable disable
  ARPaymentVisibilityRestriction, ARPayment>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, TabOrder = 0)]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<ARPayment.branchID>, IsPending>, Null, Case<Where<ARPayment.customerLocationID, IsNull, And<UnattendedMode, Equal<True>>>, Null, Case<Where<Selector<ARPayment.customerLocationID, PX.Objects.CR.Location.cBranchID>, IsNotNull>, Selector<ARPayment.customerLocationID, PX.Objects.CR.Location.cBranchID>, Case<Where<ARPayment.customerID, IsNotNull, And<Not<Selector<ARPayment.customerID, Customer.cOrgBAccountID>, RestrictByBranch<Current2<ARPayment.branchID>>>>>, Null, Case<Where<Current2<ARPayment.branchID>, IsNotNull>, Current2<ARPayment.branchID>, Case<Where<ARPayment.customerID, IsNotNull, And<Selector<ARPayment.customerID, Customer.baseCuryID>, IsNotNull, And<Selector<ARPayment.customerID, Customer.baseCuryID>, NotEqual<Current<AccessInfo.baseCuryID>>>>>, Null>>>>>>, Current<AccessInfo.branchID>>))]
  public int? BranchID { get; set; }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current2<ARPaymentMultipleBaseCurrenciesRestriction.branchBaseCuryID>, IsNull, Or<Customer.baseCuryID, IsNull, Or<Customer.baseCuryID, Equal<Current2<ARPaymentMultipleBaseCurrenciesRestriction.branchBaseCuryID>>>>>), null, new System.Type[] {})]
  public int? CustomerID { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<ARPayment.branchID, PX.Objects.GL.Branch.baseCuryID>))]
  public string BranchBaseCuryID { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<ARPayment.customerID, Customer.baseCuryID>))]
  public string CustomerBaseCuryID { get; set; }

  public abstract class branchBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPaymentMultipleBaseCurrenciesRestriction.branchBaseCuryID>
  {
  }

  public abstract class customerBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPaymentMultipleBaseCurrenciesRestriction.customerBaseCuryID>
  {
  }
}
