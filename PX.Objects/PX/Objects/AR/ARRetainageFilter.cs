// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARRetainageFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.Standalone;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <exclude />
[Serializable]
public class ARRetainageFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Organization(false)]
  public int? OrganizationID { get; set; }

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [OrganizationTree(typeof (ARRetainageFilter.organizationID), typeof (ARRetainageFilter.branchID), null, false)]
  [PXUIRequired(typeof (Where<FeatureInstalled<FeaturesSet.multipleBaseCurrencies>>))]
  public int? OrgBAccountID { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DocDate { get; set; }

  [AROpenPeriod(typeof (ARRetainageFilter.docDate), null, null, typeof (ARRetainageFilter.organizationID), null, null, true, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, IsFilterMode = true)]
  [PXDefault]
  [PXUIField]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  [Customer]
  public virtual int? CustomerID { get; set; }

  [NonProjectBase]
  public virtual int? ProjectID { get; set; }

  [ProjectTask(typeof (ARRetainageFilter.projectID), FieldClass = "PaymentsByLines")]
  public virtual int? ProjectTaskID { get; set; }

  [AccountGroup(FieldClass = "PaymentsByLines")]
  public virtual int? AccountGroupID { get; set; }

  [CostCode(Filterable = false, SkipVerification = true, FieldClass = "PaymentsByLines")]
  public virtual int? CostCodeID { get; set; }

  [PXDBInt]
  [PXUIField]
  [PMInventorySelector(Filterable = true)]
  public virtual int? InventoryID { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [ARInvoiceType.RefNbr(typeof (Search5<ARRegisterAlias.refNbr, InnerJoinSingleTable<ARInvoice, On<ARInvoice.docType, Equal<ARRegisterAlias.docType>, And<ARInvoice.refNbr, Equal<ARRegisterAlias.refNbr>>>, InnerJoinSingleTable<Customer, On<ARRegisterAlias.customerID, Equal<Customer.bAccountID>>, LeftJoin<ARTran, On<ARRegisterAlias.paymentsByLinesAllowed, Equal<True>, And<ARTran.tranType, Equal<ARInvoice.docType>, And<ARTran.refNbr, Equal<ARInvoice.refNbr>, And<ARTran.curyRetainageBal, NotEqual<decimal0>, And<ARTran.curyRetainageAmt, NotEqual<decimal0>>>>>>>>>, Where<ARRegisterAlias.retainageApply, Equal<True>, And<ARRegisterAlias.released, Equal<True>, And2<Where<ARRegisterAlias.paymentsByLinesAllowed, NotEqual<True>, And<ARRegisterAlias.curyRetainageUnreleasedAmt, Greater<decimal0>, And<ARRegisterAlias.curyRetainageTotal, Greater<decimal0>>>>, Or<ARRegisterAlias.paymentsByLinesAllowed, Equal<True>, And<ARTran.refNbr, IsNotNull>>>>>, Aggregate<GroupBy<ARRegisterAlias.docType, GroupBy<ARRegisterAlias.refNbr>>>, OrderBy<Desc<ARRegisterAlias.refNbr>>>))]
  [PXUIField]
  public virtual string RefNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? ShowBillsWithOpenBalance { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "100.0")]
  [PXUIField]
  public virtual Decimal? RetainageReleasePct { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage to Release", FieldClass = "Retainage", Enabled = false)]
  public virtual Decimal? CuryRetainageReleasedAmt { get; set; }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRetainageFilter.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRetainageFilter.branchID>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARRetainageFilter.docDate>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRetainageFilter.finPeriodID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRetainageFilter.customerID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRetainageFilter.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRetainageFilter.projectTaskID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRetainageFilter.accountGroupID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRetainageFilter.costCodeID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRetainageFilter.inventoryID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRetainageFilter.refNbr>
  {
  }

  public abstract class showBillsWithOpenBalance : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRetainageFilter.showBillsWithOpenBalance>
  {
  }

  public abstract class retainageReleasePct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRetainageFilter.retainageReleasePct>
  {
  }

  public abstract class curyRetainageReleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRetainageFilter.curyRetainageReleasedAmt>
  {
  }
}
