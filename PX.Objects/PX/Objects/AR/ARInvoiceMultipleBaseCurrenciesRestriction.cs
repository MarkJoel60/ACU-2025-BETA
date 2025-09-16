// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceMultipleBaseCurrenciesRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.AR;

public sealed class ARInvoiceMultipleBaseCurrenciesRestriction : 
  PXCacheExtension<
  #nullable disable
  ARInvoiceVisibilityRestriction, ARInvoice>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<ARInvoice.branchID>, IsPending>, Null, Case<Where<ARInvoice.customerLocationID, IsNull, And<UnattendedMode, Equal<True>>>, Null, Case<Where<Selector<ARInvoice.customerLocationID, PX.Objects.CR.Location.cBranchID>, IsNotNull>, Selector<ARInvoice.customerLocationID, PX.Objects.CR.Location.cBranchID>, Case<Where<ARInvoice.customerID, IsNotNull, And<Not<Selector<ARInvoice.customerID, Customer.cOrgBAccountID>, RestrictByBranch<Current2<ARInvoice.branchID>>>>>, Null, Case<Where<Current2<ARInvoice.branchID>, IsNotNull>, Current2<ARInvoice.branchID>, Case<Where<ARInvoice.customerID, IsNotNull, And<Selector<ARInvoice.customerID, Customer.baseCuryID>, IsNotNull, And<Selector<ARInvoice.customerID, Customer.baseCuryID>, NotEqual<Current<AccessInfo.baseCuryID>>>>>, Null>>>>>>, Current<AccessInfo.branchID>>))]
  public int? BranchID { get; set; }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current2<ARInvoiceMultipleBaseCurrenciesRestriction.branchBaseCuryID>, IsNull, Or<Customer.baseCuryID, IsNull, Or<Customer.baseCuryID, Equal<Current2<ARInvoiceMultipleBaseCurrenciesRestriction.branchBaseCuryID>>>>>), null, new System.Type[] {})]
  public int? CustomerID { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<ARInvoice.branchID, PX.Objects.GL.Branch.baseCuryID>))]
  public string BranchBaseCuryID { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<ARInvoice.customerID, Customer.baseCuryID>))]
  public string CustomerBaseCuryID { get; set; }

  public abstract class branchBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoiceMultipleBaseCurrenciesRestriction.branchBaseCuryID>
  {
  }

  public abstract class customerBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoiceMultipleBaseCurrenciesRestriction.customerBaseCuryID>
  {
  }
}
