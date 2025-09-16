// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select2<ARInvoice, InnerJoin<ARRegister, On<ARRegister.docType, Equal<ARInvoice.docType>, And<ARRegister.refNbr, Equal<ARInvoice.refNbr>>>, LeftJoin<ARTran, On<ARRegister.paymentsByLinesAllowed, Equal<True>, And<ARTran.tranType, Equal<ARInvoice.docType>, And<ARTran.refNbr, Equal<ARInvoice.refNbr>, And<ARTran.curyRetainageBal, NotEqual<decimal0>, And<ARTran.curyRetainageAmt, NotEqual<decimal0>>>>>>, LeftJoin<PX.Objects.GL.Account, On<ARRegister.paymentsByLinesAllowed, Equal<True>, And<PX.Objects.GL.Account.accountID, Equal<ARTran.accountID>>>>>>, Where2<Where<CurrentValue<ARRetainageFilter.customerID>, IsNull, Or<ARRegister.customerID, Equal<CurrentValue<ARRetainageFilter.customerID>>>>, And2<Where<CurrentValue<ARRetainageFilter.projectID>, IsNull, Or<ARInvoice.projectID, Equal<CurrentValue<ARRetainageFilter.projectID>>>>, And2<Where<CurrentValue<ARRetainageFilter.showBillsWithOpenBalance>, Equal<True>, Or<Where<ARRegister.curyDocBal, Equal<decimal0>, And<CurrentValue<ARRetainageFilter.showBillsWithOpenBalance>, NotEqual<True>>>>>, And<ARRegister.retainageApply, Equal<True>, And<ARRegister.released, Equal<True>, And<ARRegister.docDate, LessEqual<CurrentValue<ARRetainageFilter.docDate>>, And2<Where<ARRegister.refNbr, Equal<CurrentValue<ARRetainageFilter.refNbr>>, Or<CurrentValue<ARRetainageFilter.refNbr>, IsNull>>, And<Where<ARRegister.paymentsByLinesAllowed, NotEqual<True>, And<ARRegister.curyRetainageUnreleasedAmt, Greater<decimal0>, Or<ARTran.refNbr, IsNotNull, And2<Where<CurrentValue<ARRetainageFilter.projectTaskID>, IsNull, Or<ARTran.taskID, Equal<CurrentValue<ARRetainageFilter.projectTaskID>>>>, And2<Where<CurrentValue<ARRetainageFilter.accountGroupID>, IsNull, Or<PX.Objects.GL.Account.accountGroupID, Equal<CurrentValue<ARRetainageFilter.accountGroupID>>>>, And2<Where<CurrentValue<ARRetainageFilter.costCodeID>, IsNull, Or<ARTran.costCodeID, Equal<CurrentValue<ARRetainageFilter.costCodeID>>>>, And<Where<CurrentValue<ARRetainageFilter.inventoryID>, IsNull, Or<ARTran.inventoryID, Equal<CurrentValue<ARRetainageFilter.inventoryID>>>>>>>>>>>>>>>>>>>, OrderBy<Asc<ARRegister.refNbr>>>))]
[Serializable]
public class ARInvoiceExt : ARInvoice
{
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (ARInvoice.docType))]
  [ARInvoiceType.List]
  [PXUIField(DisplayName = "Type")]
  public override 
  #nullable disable
  string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (ARInvoice.refNbr))]
  [PXUIField(DisplayName = "Reference Nbr.")]
  [PXSelector(typeof (ARInvoiceExt.refNbr))]
  public override string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXInt(IsKey = true)]
  [PXUIField(DisplayName = "Line Nbr.", Enabled = false, FieldClass = "PaymentsByLines")]
  [PXFormula(typeof (IsNull<ARInvoiceExt.aRTranLineNbr, int0>))]
  public virtual int? LineNbr { get; set; }

  /// <summary>
  /// Code of the <see cref="T:PX.Objects.CM.Currency">Currency</see> of the document.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">company's base currency</see>.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (ARRegister.curyID))]
  [PXUIField]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public override string CuryID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Project", Enabled = false)]
  [PXSelector(typeof (PMProject.contractID), SubstituteKey = typeof (PMProject.contractCD), ValidateValue = false)]
  [PXFormula(typeof (Switch<Case<Where<ARInvoice.paymentsByLinesAllowed, Equal<True>>, ARInvoiceExt.aRTranProjectID>, ARInvoice.projectID>))]
  public virtual int? DisplayProjectID { get; set; }

  [PXCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoiceExt.retainageBal), BaseCalc = false)]
  [PXFormula(typeof (Mult<IsNull<ARInvoiceExt.aRTranCuryRetainageBal, ARInvoice.curyRetainageUnreleasedAmt>, PX.Objects.AR.BQL.SignAmount<ARInvoiceExt.docType>>))]
  public virtual Decimal? CuryRetainageBal { get; set; }

  [PXBaseCury]
  [PXFormula(typeof (Mult<IsNull<ARInvoiceExt.aRTranRetainageBal, ARRegister.retainageUnreleasedAmt>, PX.Objects.AR.BQL.SignAmount<ARInvoiceExt.docType>>))]
  public virtual Decimal? RetainageBal { get; set; }

  [PXCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.origDocAmtWithRetainageTotal), BaseCalc = false)]
  [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Mult<IsNull<Add<ARInvoiceExt.aRTranCuryOrigRetainageAmt, ARInvoiceExt.aRTranCuryOrigTranAmt>, Add<ARRegister.curyOrigDocAmt, ARRegister.curyRetainageTotal>>, PX.Objects.AR.BQL.SignAmount<ARInvoiceExt.docType>>))]
  public override Decimal? CuryOrigDocAmtWithRetainageTotal { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
  [PXFormula(typeof (Mult<IsNull<Add<ARInvoiceExt.aRTranOrigRetainageAmt, ARInvoiceExt.aRTranOrigTranAmt>, Add<ARRegister.curyOrigDocAmt, ARRegister.curyRetainageTotal>>, PX.Objects.AR.BQL.SignAmount<ARInvoiceExt.docType>>))]
  public override Decimal? OrigDocAmtWithRetainageTotal { get; set; }

  [UnboundRetainagePercent(typeof (True), typeof (ARRetainageFilter.retainageReleasePct), typeof (ARInvoiceExt.curyRetainageBal), typeof (ARInvoiceExt.curyRetainageReleasedAmt), typeof (ARInvoiceExt.retainageReleasePct), DisplayName = "Percent to Release")]
  public virtual Decimal? RetainageReleasePct { get; set; }

  [UnboundRetainageAmount(typeof (ARInvoice.curyInfoID), typeof (ARInvoiceExt.curyRetainageBal), typeof (ARInvoiceExt.curyRetainageReleasedAmt), typeof (ARInvoiceExt.retainageReleasedAmt), typeof (ARInvoiceExt.retainageReleasePct), DisplayName = "Retainage to Release")]
  [PXParent(typeof (Select<ARRetainageFilter>), UseCurrent = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARInvoice.selected, Equal<True>>, ARInvoiceExt.curyRetainageReleasedAmt>, decimal0>), typeof (SumCalc<ARRetainageFilter.curyRetainageReleasedAmt>))]
  public virtual Decimal? CuryRetainageReleasedAmt { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainageReleasedAmt { get; set; }

  [PXCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoiceExt.retainageUnreleasedCalcAmt))]
  [PXUIField(DisplayName = "Unreleased Retainage")]
  [PXFormula(typeof (Sub<ARInvoiceExt.curyRetainageBal, ARInvoiceExt.curyRetainageReleasedAmt>))]
  public virtual Decimal? CuryRetainageUnreleasedCalcAmt { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainageUnreleasedCalcAmt { get; set; }

  [PXDBInt(BqlField = typeof (ARTran.lineNbr))]
  public virtual int? ARTranLineNbr { get; set; }

  /// <summary>The sort order of the detail line.</summary>
  [PXDBInt(BqlField = typeof (ARTran.sortOrder))]
  [PXUIField(DisplayName = "Line Nbr.", Enabled = false, FieldClass = "PaymentsByLines")]
  public virtual int? ARTranSortOrder { get; set; }

  [PXDBInt(BqlField = typeof (ARTran.inventoryID))]
  [PXUIField(DisplayName = "Inventory ID", Enabled = false, FieldClass = "PaymentsByLines")]
  [PXSelector(typeof (PX.Objects.IN.InventoryItem.inventoryID), SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD), ValidateValue = false)]
  public virtual int? ARTranInventoryID { get; set; }

  [PXDBInt(BqlField = typeof (ARTran.projectID))]
  [PXUIField(DisplayName = "Project", Enabled = false, FieldClass = "PaymentsByLines")]
  [PXSelector(typeof (PMProject.contractID), SubstituteKey = typeof (PMProject.contractCD), ValidateValue = false)]
  public virtual int? ARTranProjectID { get; set; }

  [PXDBInt(BqlField = typeof (ARTran.taskID))]
  [PXUIField(DisplayName = "Project Task", Enabled = false, FieldClass = "PaymentsByLines")]
  [PXSelector(typeof (PMTask.taskID), SubstituteKey = typeof (PMTask.taskCD), ValidateValue = false)]
  public virtual int? ARTranTaskID { get; set; }

  [PXDBInt(BqlField = typeof (ARTran.costCodeID))]
  [PXUIField(DisplayName = "Cost Code", Enabled = false, FieldClass = "PaymentsByLines")]
  [PXSelector(typeof (PMCostCode.costCodeID), SubstituteKey = typeof (PMCostCode.costCodeCD), ValidateValue = false)]
  public virtual int? ARTranCostCodeID { get; set; }

  [PXDBInt(BqlField = typeof (ARTran.accountID))]
  [PXUIField(DisplayName = "Account", Enabled = false, FieldClass = "PaymentsByLines")]
  [PXSelector(typeof (PX.Objects.GL.Account.accountID), SubstituteKey = typeof (PX.Objects.GL.Account.accountCD), ValidateValue = false)]
  public virtual int? ARTranAccountID { get; set; }

  [PXDBDecimal(BqlField = typeof (ARTran.curyOrigRetainageAmt))]
  public virtual Decimal? ARTranCuryOrigRetainageAmt { get; set; }

  [PXDBDecimal(BqlField = typeof (ARTran.origRetainageAmt))]
  public virtual Decimal? ARTranOrigRetainageAmt { get; set; }

  [PXDBDecimal(BqlField = typeof (ARTran.curyRetainageBal))]
  public virtual Decimal? ARTranCuryRetainageBal { get; set; }

  [PXDBDecimal(BqlField = typeof (ARTran.retainageBal))]
  public virtual Decimal? ARTranRetainageBal { get; set; }

  [PXDBDecimal(BqlField = typeof (ARTran.curyOrigTranAmt))]
  public virtual Decimal? ARTranCuryOrigTranAmt { get; set; }

  [PXDBDecimal(BqlField = typeof (ARTran.origTranAmt))]
  public virtual Decimal? ARTranOrigTranAmt { get; set; }

  public new abstract class docType : IBqlField, IBqlOperand
  {
  }

  public new abstract class refNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class lineNbr : IBqlField, IBqlOperand
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoiceExt.curyID>
  {
  }

  public abstract class displayProjectID : IBqlField, IBqlOperand
  {
  }

  public abstract class curyRetainageBal : IBqlField, IBqlOperand
  {
  }

  public abstract class retainageBal : IBqlField, IBqlOperand
  {
  }

  public new abstract class curyOrigDocAmtWithRetainageTotal : IBqlField, IBqlOperand
  {
  }

  public new abstract class origDocAmtWithRetainageTotal : IBqlField, IBqlOperand
  {
  }

  public abstract class retainageReleasePct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceExt.retainageReleasePct>
  {
  }

  public abstract class curyRetainageReleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceExt.curyRetainageReleasedAmt>
  {
  }

  public abstract class retainageReleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceExt.retainageReleasedAmt>
  {
  }

  public abstract class curyRetainageUnreleasedCalcAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceExt.curyRetainageUnreleasedCalcAmt>
  {
  }

  public abstract class retainageUnreleasedCalcAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceExt.retainageUnreleasedCalcAmt>
  {
  }

  public abstract class aRTranLineNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class aRTranSortOrder : IBqlField, IBqlOperand
  {
  }

  public abstract class aRTranInventoryID : IBqlField, IBqlOperand
  {
  }

  public abstract class aRTranProjectID : IBqlField, IBqlOperand
  {
  }

  public abstract class aRTranTaskID : IBqlField, IBqlOperand
  {
  }

  public abstract class aRTranCostCodeID : IBqlField, IBqlOperand
  {
  }

  public abstract class aRTranAccountID : IBqlField, IBqlOperand
  {
  }

  public abstract class aRTranCuryOrigRetainageAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class aRTranOrigRetainageAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class aRTranCuryRetainageBal : IBqlField, IBqlOperand
  {
  }

  public abstract class aRTranRetainageBal : IBqlField, IBqlOperand
  {
  }

  public abstract class aRTranCuryOrigTranAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class aRTranOrigTranAmt : IBqlField, IBqlOperand
  {
  }
}
