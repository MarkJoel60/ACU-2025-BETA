// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXProjection(typeof (Select2<APInvoice, InnerJoin<APRegister, On<APRegister.docType, Equal<APInvoice.docType>, And<APRegister.refNbr, Equal<APInvoice.refNbr>>>, LeftJoin<APTran, On<APRegister.paymentsByLinesAllowed, Equal<True>, And<APTran.tranType, Equal<APInvoice.docType>, And<APTran.refNbr, Equal<APInvoice.refNbr>, And<APTran.curyRetainageBal, NotEqual<decimal0>, And<APTran.curyRetainageAmt, NotEqual<decimal0>>>>>>>>, Where2<Where<CurrentValue<APRetainageFilter.vendorID>, PX.Data.IsNull, Or<APRegister.vendorID, Equal<CurrentValue<APRetainageFilter.vendorID>>>>, And2<Where<CurrentValue<APRetainageFilter.projectID>, PX.Data.IsNull, Or<APRegister.projectID, Equal<CurrentValue<APRetainageFilter.projectID>>>>, And2<Where<CurrentValue<APRetainageFilter.showBillsWithOpenBalance>, Equal<True>, PX.Data.Or<Where<APRegister.curyDocBal, Equal<decimal0>, And<CurrentValue<APRetainageFilter.showBillsWithOpenBalance>, NotEqual<True>>>>>, And<APRegister.retainageApply, Equal<True>, And<APRegister.released, Equal<True>, And<APRegister.docDate, LessEqual<CurrentValue<APRetainageFilter.docDate>>, And2<Where<APRegister.refNbr, Equal<CurrentValue<APRetainageFilter.refNbr>>, Or<CurrentValue<APRetainageFilter.refNbr>, PX.Data.IsNull>>, PX.Data.And<Where<APRegister.paymentsByLinesAllowed, NotEqual<True>, And<APRegister.curyRetainageUnreleasedAmt, Greater<decimal0>, Or<APTran.refNbr, PX.Data.IsNotNull>>>>>>>>>>>, OrderBy<Asc<APRegister.refNbr>>>))]
[Serializable]
public class APInvoiceExt : APInvoice
{
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (APInvoice.docType))]
  [APInvoiceType.List]
  [PXUIField(DisplayName = "Type")]
  public override 
  #nullable disable
  string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (APInvoice.refNbr))]
  [PXUIField(DisplayName = "Reference Nbr.")]
  [PXSelector(typeof (APInvoiceExt.refNbr))]
  public override string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXInt(IsKey = true)]
  [PXUIField(DisplayName = "Line Nbr.", Enabled = false, FieldClass = "PaymentsByLines")]
  [PXFormula(typeof (IsNull<APInvoiceExt.aPTranLineNbr, PX.Objects.CS.int0>))]
  public virtual int? LineNbr { get; set; }

  /// <summary>
  /// Code of the <see cref="T:PX.Objects.CM.Currency">Currency</see> of the document.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">company's base currency</see>.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (APRegister.curyID))]
  [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible, FieldClass = "Multicurrency")]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public override string CuryID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Project", Enabled = false)]
  [PXSelector(typeof (PMProject.contractID), SubstituteKey = typeof (PMProject.contractCD), ValidateValue = false)]
  [PXFormula(typeof (Switch<IBqlInt, TypeArrayOf<IBqlCase>.Empty, Case<Where<BqlOperand<APInvoice.paymentsByLinesAllowed, IBqlBool>.IsEqual<True>>, APInvoiceExt.aPTranProjectID>, APInvoice.projectID>.When<BqlOperand<APInvoiceExt.hasMultipleProjects, IBqlBool>.IsNotEqual<True>>.ElseNull))]
  public virtual int? DisplayProjectID { get; set; }

  [PXCurrency(typeof (APInvoice.curyInfoID), typeof (APInvoiceExt.retainageBal), BaseCalc = false)]
  [PXFormula(typeof (Mult<IsNull<APInvoiceExt.aPTranCuryRetainageBal, APRegister.curyRetainageUnreleasedAmt>, PX.Objects.AP.BQL.SignAmount<APInvoiceExt.docType>>))]
  public virtual Decimal? CuryRetainageBal { get; set; }

  [PXBaseCury]
  [PXFormula(typeof (Mult<IsNull<APInvoiceExt.aPTranRetainageBal, APInvoice.retainageUnreleasedAmt>, PX.Objects.AP.BQL.SignAmount<APInvoiceExt.docType>>))]
  public virtual Decimal? RetainageBal { get; set; }

  [PXCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.origDocAmtWithRetainageTotal), BaseCalc = false)]
  [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXFormula(typeof (Mult<IsNull<Add<APInvoiceExt.aPTranCuryOrigRetainageAmt, APInvoiceExt.aPTranCuryOrigTranAmt>, Add<APRegister.curyOrigDocAmt, APRegister.curyRetainageTotal>>, PX.Objects.AP.BQL.SignAmount<APInvoiceExt.docType>>))]
  public override Decimal? CuryOrigDocAmtWithRetainageTotal { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
  [PXFormula(typeof (Mult<IsNull<Add<APInvoiceExt.aPTranOrigRetainageAmt, APInvoiceExt.aPTranOrigTranAmt>, Add<APRegister.curyOrigDocAmt, APRegister.curyRetainageTotal>>, PX.Objects.AP.BQL.SignAmount<APInvoiceExt.docType>>))]
  public override Decimal? OrigDocAmtWithRetainageTotal { get; set; }

  [UnboundRetainagePercent(typeof (True), typeof (decimal100), typeof (APInvoiceExt.curyRetainageBal), typeof (APInvoiceExt.curyRetainageReleasedAmt), typeof (APInvoiceExt.retainageReleasePct), DisplayName = "Percent to Release")]
  public virtual Decimal? RetainageReleasePct { get; set; }

  [UnboundRetainageAmount(typeof (APInvoice.curyInfoID), typeof (APInvoiceExt.curyRetainageBal), typeof (APInvoiceExt.curyRetainageReleasedAmt), typeof (APInvoiceExt.retainageReleasedAmt), typeof (APInvoiceExt.retainageReleasePct), DisplayName = "Retainage to Release")]
  public virtual Decimal? CuryRetainageReleasedAmt { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? RetainageReleasedAmt { get; set; }

  [PXCurrency(typeof (APInvoice.curyInfoID), typeof (APInvoiceExt.retainageUnreleasedCalcAmt))]
  [PXUIField(DisplayName = "Unreleased Retainage")]
  [PXFormula(typeof (Sub<APInvoiceExt.curyRetainageBal, APInvoiceExt.curyRetainageReleasedAmt>))]
  public virtual Decimal? CuryRetainageUnreleasedCalcAmt { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? RetainageUnreleasedCalcAmt { get; set; }

  [PXDBInt(BqlField = typeof (APTran.lineNbr))]
  public virtual int? APTranLineNbr { get; set; }

  [PXDBInt(BqlField = typeof (APTran.inventoryID))]
  [PXUIField(DisplayName = "Inventory ID", Enabled = false, FieldClass = "PaymentsByLines")]
  [PXSelector(typeof (PX.Objects.IN.InventoryItem.inventoryID), SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD), ValidateValue = false)]
  public virtual int? APTranInventoryID { get; set; }

  [PXDBInt(BqlField = typeof (APTran.projectID))]
  [PXUIField(DisplayName = "Project", Enabled = false, FieldClass = "PaymentsByLines")]
  [PXSelector(typeof (PMProject.contractID), SubstituteKey = typeof (PMProject.contractCD), ValidateValue = false)]
  public virtual int? APTranProjectID { get; set; }

  [PXDBInt(BqlField = typeof (APTran.taskID))]
  [PXUIField(DisplayName = "Project Task", Enabled = false, FieldClass = "PaymentsByLines")]
  [PXSelector(typeof (PMTask.taskID), SubstituteKey = typeof (PMTask.taskCD), ValidateValue = false)]
  public virtual int? APTranTaskID { get; set; }

  [PXDBInt(BqlField = typeof (APTran.costCodeID))]
  [PXUIField(DisplayName = "Cost Code", Enabled = false, FieldClass = "PaymentsByLines")]
  [PXSelector(typeof (PMCostCode.costCodeID), SubstituteKey = typeof (PMCostCode.costCodeCD), ValidateValue = false)]
  public virtual int? APTranCostCodeID { get; set; }

  [PXDBInt(BqlField = typeof (APTran.accountID))]
  [PXUIField(DisplayName = "Account", Enabled = false, FieldClass = "PaymentsByLines")]
  [PXSelector(typeof (PX.Objects.GL.Account.accountID), SubstituteKey = typeof (PX.Objects.GL.Account.accountCD), ValidateValue = false)]
  public virtual int? APTranAccountID { get; set; }

  [PXDBDecimal(BqlField = typeof (APTran.curyOrigRetainageAmt))]
  public virtual Decimal? APTranCuryOrigRetainageAmt { get; set; }

  [PXDBDecimal(BqlField = typeof (APTran.origRetainageAmt))]
  public virtual Decimal? APTranOrigRetainageAmt { get; set; }

  [PXDBDecimal(BqlField = typeof (APTran.curyRetainageBal))]
  public virtual Decimal? APTranCuryRetainageBal { get; set; }

  [PXDBDecimal(BqlField = typeof (APTran.retainageBal))]
  public virtual Decimal? APTranRetainageBal { get; set; }

  [PXDBDecimal(BqlField = typeof (APTran.curyOrigTranAmt))]
  public virtual Decimal? APTranCuryOrigTranAmt { get; set; }

  [PXDBDecimal(BqlField = typeof (APTran.origTranAmt))]
  public virtual Decimal? APTranOrigTranAmt { get; set; }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoiceExt.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoiceExt.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoiceExt.lineNbr>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoiceExt.curyID>
  {
  }

  public abstract class displayProjectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoiceExt.displayProjectID>
  {
  }

  public abstract class curyRetainageBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceExt.curyRetainageBal>
  {
  }

  public abstract class retainageBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoiceExt.retainageBal>
  {
  }

  public new abstract class curyOrigDocAmtWithRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceExt.curyOrigDocAmtWithRetainageTotal>
  {
  }

  public new abstract class origDocAmtWithRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceExt.origDocAmtWithRetainageTotal>
  {
  }

  public abstract class retainageReleasePct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceExt.retainageReleasePct>
  {
  }

  public abstract class curyRetainageReleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceExt.curyRetainageReleasedAmt>
  {
  }

  public abstract class retainageReleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceExt.retainageReleasedAmt>
  {
  }

  public abstract class curyRetainageUnreleasedCalcAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceExt.curyRetainageUnreleasedCalcAmt>
  {
  }

  public abstract class retainageUnreleasedCalcAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceExt.retainageUnreleasedCalcAmt>
  {
  }

  public abstract class aPTranLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoiceExt.aPTranLineNbr>
  {
  }

  public abstract class aPTranInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APInvoiceExt.aPTranInventoryID>
  {
  }

  public abstract class aPTranProjectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoiceExt.aPTranProjectID>
  {
  }

  public abstract class aPTranTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoiceExt.aPTranTaskID>
  {
  }

  public abstract class aPTranCostCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoiceExt.aPTranCostCodeID>
  {
  }

  public abstract class aPTranAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoiceExt.aPTranAccountID>
  {
  }

  public abstract class aPTranCuryOrigRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceExt.aPTranCuryOrigRetainageAmt>
  {
  }

  public abstract class aPTranOrigRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceExt.aPTranOrigRetainageAmt>
  {
  }

  public abstract class aPTranCuryRetainageBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceExt.aPTranCuryRetainageBal>
  {
  }

  public abstract class aPTranRetainageBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceExt.aPTranRetainageBal>
  {
  }

  public abstract class aPTranCuryOrigTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceExt.aPTranCuryOrigTranAmt>
  {
  }

  public abstract class aPTranOrigTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceExt.aPTranOrigTranAmt>
  {
  }

  public new abstract class hasMultipleProjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APInvoiceExt.hasMultipleProjects>
  {
  }
}
