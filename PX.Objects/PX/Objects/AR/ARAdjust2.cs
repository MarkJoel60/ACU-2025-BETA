// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARAdjust2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.SO;
using PX.Objects.SO.Interfaces;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.AR;

[PXCacheName("Applications")]
[PXBreakInheritance]
[DebuggerDisplay("ARAdjust2: Invoice (Type {AdjdDocType}, Number {AdjdRefNbr}), Payment (Type {AdjgDocType}, Number {AdjgRefNbr}), tstamp = {PX.Data.PXDBTimestampAttribute.ToString(tstamp)}")]
[Serializable]
public class ARAdjust2 : ARAdjust, ICreatePaymentAdjust
{
  [PXDefault]
  [Customer(Visible = false, Enabled = false)]
  public override int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
  [ARPaymentType.List]
  [PXDefault]
  [PXUIField(DisplayName = "Doc. Type")]
  public override 
  #nullable disable
  string AdjgDocType
  {
    get => this._AdjgDocType;
    set => this._AdjgDocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Reference Nbr.")]
  [ARPaymentType.AdjgRefNbr(typeof (Search<ARPayment.refNbr, Where<ARPayment.docType, Equal<Optional<ARAdjust2.adjgDocType>>>>), Filterable = true)]
  [PXParent(typeof (Select<ARPayment, Where<ARPayment.docType, Equal<Current<ARAdjust2.adjgDocType>>, And<ARPayment.refNbr, Equal<Current<ARAdjust2.adjgRefNbr>>>>>), LeaveChildren = true)]
  [PXParent(typeof (Select<ARPaymentTotals, Where<ARPaymentTotals.docType, Equal<Current<ARAdjust2.adjgDocType>>, And<ARPaymentTotals.refNbr, Equal<Current<ARAdjust2.adjgRefNbr>>>>>), ParentCreate = true, LeaveChildren = true)]
  public override string AdjgRefNbr
  {
    get => this._AdjgRefNbr;
    set => this._AdjgRefNbr = value;
  }

  [Branch(null, null, true, true, false, Enabled = false)]
  public override int? AdjgBranchID
  {
    get => this._AdjgBranchID;
    set => this._AdjgBranchID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (PX.Objects.AR.ARInvoice.curyInfoID))]
  public override long? AdjdCuryInfoID
  {
    get => this._AdjdCuryInfoID;
    set => this._AdjdCuryInfoID = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
  [PXDBDefault(typeof (PX.Objects.AR.ARInvoice.docType))]
  [PXUIField]
  public override string AdjdDocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.AR.ARInvoice.refNbr))]
  [PXParent(typeof (Select<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Current<ARAdjust2.adjdDocType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Current<ARAdjust2.adjdRefNbr>>, And<PX.Objects.AR.ARInvoice.docType, NotEqual<ARDocType.creditMemo>>>>>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARAdjust2.curyAdjdAmt, NotEqual<decimal0>, And<ARAdjust2.paymentPendingProcessing, Equal<True>, And<ARAdjust2.paymentCaptureFailed, NotEqual<True>>>>, int1>, int0>), typeof (SumCalc<PX.Objects.AR.ARInvoice.pendingProcessingCntr>), ForceAggregateRecalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARAdjust2.curyAdjdAmt, NotEqual<decimal0>, And<ARAdjust2.paymentCaptureFailed, Equal<True>>>, int1>, int0>), typeof (SumCalc<PX.Objects.AR.ARInvoice.captureFailedCntr>), ForceAggregateRecalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARAdjust.curyAdjdAmt, Greater<decimal0>, And<ARAdjust.isCCAuthorized, Equal<True>, And<ARAdjust.isCCCaptured, Equal<False>>>>, int1>, int0>), typeof (SumCalc<PX.Objects.AR.ARInvoice.authorizedPaymentCntr>), ForceAggregateRecalculation = true)]
  [CopyChildLink(typeof (ARPaymentTotals.invoiceCntr), typeof (ARAdjust2.curyAdjdAmt), new Type[] {typeof (ARAdjust2.adjdDocType), typeof (ARAdjust2.adjdRefNbr)}, new Type[] {typeof (ARPaymentTotals.adjdDocType), typeof (ARPaymentTotals.adjdRefNbr)})]
  [PXUIField]
  public override string AdjdRefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXDefault(typeof (Switch<Case<Where<Selector<ARAdjust2.adjdRefNbr, PX.Objects.AR.ARInvoice.paymentsByLinesAllowed>, NotEqual<True>>, int0>, Null>))]
  [ARInvoiceType.AdjdLineNbr]
  [PXRestrictor(typeof (Where<ARAdjust.ARInvoice.paymentsByLinesAllowed, Equal<True>, And<ARTran.curyTranBal, NotEqual<decimal0>>>), "'{0}' cannot be found in the system.", new Type[] {typeof (ARTran.lineNbr)})]
  public override int? AdjdLineNbr { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Order Type")]
  [PXSelector(typeof (Search<SOOrderType.orderType>))]
  public override string AdjdOrderType
  {
    get => this._AdjdOrderType;
    set => this._AdjdOrderType = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Optional<ARAdjust2.adjdOrderType>>>>), Filterable = true)]
  [PXParent(typeof (Select<SOAdjust, Where<SOAdjust.adjdOrderType, Equal<Current<ARAdjust2.adjdOrderType>>, And<SOAdjust.adjdOrderNbr, Equal<Current<ARAdjust2.adjdOrderNbr>>, And<SOAdjust.adjgDocType, Equal<Current<ARAdjust2.adjgDocType>>, And<SOAdjust.adjgRefNbr, Equal<Current<ARAdjust2.adjgRefNbr>>>>>>>), LeaveChildren = true)]
  public override string AdjdOrderNbr
  {
    get => this._AdjdOrderNbr;
    set => this._AdjdOrderNbr = value;
  }

  [Branch(typeof (PX.Objects.AR.ARInvoice.branchID), null, true, true, true)]
  [PXDBDefault(typeof (PX.Objects.AR.ARInvoice.branchID))]
  public override int? AdjdBranchID
  {
    get => this._AdjdBranchID;
    set => this._AdjdBranchID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXDefault]
  public override int? AdjNbr
  {
    get => this._AdjNbr;
    set => this._AdjNbr = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  public override string AdjBatchNbr
  {
    get => this._AdjBatchNbr;
    set => this._AdjBatchNbr = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (PX.Objects.AR.ARInvoice.curyInfoID))]
  public override long? AdjdOrigCuryInfoID
  {
    get => this._AdjdOrigCuryInfoID;
    set => this._AdjdOrigCuryInfoID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (ARRegister.curyInfoID))]
  public override long? AdjgCuryInfoID
  {
    get => this._AdjgCuryInfoID;
    set => this._AdjgCuryInfoID = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (PX.Objects.AR.ARInvoice.docDate))]
  public override DateTime? AdjgDocDate
  {
    get => this._AdjgDocDate;
    set => this._AdjgDocDate = value;
  }

  [PXDefault]
  [FinPeriodID(null, typeof (ARAdjust2.adjgBranchID), null, null, null, null, true, false, null, typeof (ARAdjust2.adjgTranPeriodID), typeof (PX.Objects.AR.ARInvoice.tranPeriodID), true, true)]
  public override string AdjgFinPeriodID
  {
    get => this._AdjgFinPeriodID;
    set => this._AdjgFinPeriodID = value;
  }

  [PeriodID(null, null, null, true)]
  [PXDBDefault]
  public override string AdjgTranPeriodID
  {
    get => this._AdjgTranPeriodID;
    set => this._AdjgTranPeriodID = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (PX.Objects.AR.ARInvoice.docDate))]
  [PXUIField]
  public override DateTime? AdjdDocDate
  {
    get => this._AdjdDocDate;
    set => this._AdjdDocDate = value;
  }

  [PXDBDefault]
  [FinPeriodID(null, typeof (ARAdjust2.adjdBranchID), null, null, null, null, true, false, null, typeof (ARAdjust2.adjdTranPeriodID), typeof (PX.Objects.AR.ARInvoice.tranPeriodID), true, true)]
  [PXUIField]
  public override string AdjdFinPeriodID
  {
    get => this._AdjdFinPeriodID;
    set => this._AdjdFinPeriodID = value;
  }

  [PXDefault]
  [PeriodID(null, null, null, true)]
  public override string AdjdTranPeriodID
  {
    get => this._AdjdTranPeriodID;
    set => this._AdjdTranPeriodID = value;
  }

  [PXDBCurrency(typeof (ARAdjust2.adjdCuryInfoID), typeof (ARAdjust2.adjDiscAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryAdjdDiscAmt
  {
    get => this._CuryAdjdDiscAmt;
    set => this._CuryAdjdDiscAmt = value;
  }

  [PXDBCurrency(typeof (ARAdjust2.adjdCuryInfoID), typeof (ARAdjust2.adjWOAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARAdjust.voided, Equal<False>>, ARAdjust.curyAdjdWOAmt>, decimal0>), typeof (SumCalc<PX.Objects.AR.ARInvoice.curyBalanceWOTotal>), ForceAggregateRecalculation = true)]
  public override Decimal? CuryAdjdWOAmt { get; set; }

  public override Decimal? CuryAdjdWhTaxAmt
  {
    get => this.CuryAdjdWOAmt;
    set => this.CuryAdjdWOAmt = value;
  }

  [PXDBCurrency(typeof (ARAdjust2.adjdCuryInfoID), typeof (ARAdjust2.adjAmt), BaseCalc = false, MinValue = 0.0)]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARAdjust2.voided, Equal<False>>, ARAdjust2.curyAdjdAmt>, decimal0>), typeof (SumCalc<PX.Objects.AR.ARInvoice.curyPaymentTotal>), ForceAggregateRecalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARAdjust2.voided, Equal<False>, And<ARAdjust2.released, Equal<False>, And<ARAdjust2.paymentReleased, Equal<False>, And<ARAdjust2.isCCAuthorized, Equal<False>>>>>, ARAdjust2.curyAdjdAmt>, decimal0>), typeof (SumCalc<PX.Objects.AR.ARInvoice.curyUnreleasedPaymentAmt>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARAdjust2.voided, Equal<False>, And<ARAdjust2.released, Equal<False>, And<ARAdjust2.paymentReleased, Equal<False>, And<ARAdjust2.isCCAuthorized, Equal<True>>>>>, ARAdjust2.curyAdjdAmt>, decimal0>), typeof (SumCalc<PX.Objects.AR.ARInvoice.curyCCAuthorizedAmt>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARAdjust2.voided, Equal<False>, And<ARAdjust2.released, Equal<False>, And<ARAdjust2.paymentReleased, Equal<True>>>>, ARAdjust2.curyAdjdAmt>, decimal0>), typeof (SumCalc<PX.Objects.AR.ARInvoice.curyPaidAmt>))]
  [PXFormula(null, typeof (SumCalc<SOAdjust.curyAdjdBilledAmt>), ForceAggregateRecalculation = true)]
  public override Decimal? CuryAdjdAmt
  {
    get => this._CuryAdjdAmt;
    set => this._CuryAdjdAmt = value;
  }

  [PXDBCurrency(typeof (ARAdjust2.adjdCuryInfoID), typeof (ARAdjust2.adjAmt), BaseCalc = false)]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? CuryAdjdOrigAmt
  {
    get => this._CuryAdjdOrigAmt;
    set => this._CuryAdjdOrigAmt = value;
  }

  [PXDBGuid(false)]
  public override Guid? InvoiceID { get; set; }

  [PXDBGuid(false)]
  public override Guid? PaymentID { get; set; }

  [PXDBGuid(false)]
  public override Guid? MemoID { get; set; }

  [PXDBDecimal(4)]
  [PXFormula(null, typeof (SumCalc<SOAdjust.adjBilledAmt>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? AdjAmt
  {
    get => this._AdjAmt;
    set => this._AdjAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? AdjWOAmt { get; set; }

  public override Decimal? AdjWhTaxAmt
  {
    get => this.AdjWOAmt;
    set => this.AdjWOAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? AdjDiscAmt
  {
    get => this._AdjDiscAmt;
    set => this._AdjDiscAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryAdjgDiscAmt
  {
    get => this._CuryAdjgDiscAmt;
    set => this._CuryAdjgDiscAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? CuryAdjgWOAmt { get; set; }

  public override Decimal? CuryAdjgWhTaxAmt
  {
    get => this.CuryAdjgWOAmt;
    set => this.CuryAdjgWOAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(null, typeof (SumCalc<SOAdjust.curyAdjgBilledAmt>))]
  public override Decimal? CuryAdjgAmt
  {
    get => this._CuryAdjgAmt;
    set => this._CuryAdjgAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? RGOLAmt
  {
    get => this._RGOLAmt;
    set => this._RGOLAmt = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public override bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public override bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  [PXDBInt]
  public override int? VoidAdjNbr
  {
    get => this._VoidAdjNbr;
    set => this._VoidAdjNbr = value;
  }

  [Account]
  [PXDBDefault(typeof (PX.Objects.AR.ARInvoice.aRAccountID))]
  public override int? AdjdARAcct
  {
    get => this._AdjdARAcct;
    set => this._AdjdARAcct = value;
  }

  [SubAccount]
  [PXDBDefault(typeof (PX.Objects.AR.ARInvoice.aRSubID))]
  public override int? AdjdARSub
  {
    get => this._AdjdARSub;
    set => this._AdjdARSub = value;
  }

  [PXNote]
  public override Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXCurrency(typeof (ARAdjust2.adjdCuryInfoID), typeof (ARAdjust2.docBal), BaseCalc = false)]
  [PXUIField]
  public override Decimal? CuryDocBal
  {
    get => this._CuryDocBal;
    set => this._CuryDocBal = value;
  }

  public override Decimal? CuryPayDocBal
  {
    get => this._CuryDocBal;
    set => this._CuryDocBal = value;
  }

  [PXDecimal(4)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? DocBal
  {
    get => this._DocBal;
    set => this._DocBal = value;
  }

  public override Decimal? PayDocBal
  {
    get => this._DocBal;
    set => this._DocBal = value;
  }

  [PXCurrency(typeof (ARAdjust2.adjdCuryInfoID), typeof (ARAdjust2.discBal), BaseCalc = false)]
  [PXUIField]
  public override Decimal? CuryDiscBal
  {
    get => this._CuryDiscBal;
    set => this._CuryDiscBal = value;
  }

  public override Decimal? CuryPayDiscBal
  {
    get => this._CuryDiscBal;
    set => this._CuryDiscBal = value;
  }

  [PXDecimal(4)]
  [PXUnboundDefault]
  public override Decimal? DiscBal
  {
    get => this._DiscBal;
    set => this._DiscBal = value;
  }

  public override Decimal? PayDiscBal
  {
    get => this._DiscBal;
    set => this._DiscBal = value;
  }

  [PXCurrency(typeof (ARAdjust.adjdCuryInfoID), typeof (ARAdjust.wOBal), BaseCalc = false)]
  [PXUnboundDefault]
  [PXUIField]
  public override Decimal? CuryWOBal
  {
    get => this._CuryWOBal;
    set => this._CuryWOBal = value;
  }

  public override Decimal? CuryWhTaxBal
  {
    get => this._CuryWOBal;
    set => this._CuryWOBal = value;
  }

  [PXDecimal(4)]
  [PXUnboundDefault]
  public override Decimal? WOBal
  {
    get => this._WOBal;
    set => this._WOBal = value;
  }

  public override Decimal? WhTaxBal
  {
    get => this._WOBal;
    set => this._WOBal = value;
  }

  [PXBool]
  [PXUIField]
  [PXDefault(false)]
  public override bool? VoidAppl
  {
    [PXDependsOnFields(new Type[] {typeof (ARAdjust2.adjgDocType)})] get
    {
      return new bool?(ARPaymentType.VoidAppl(this.AdjgDocType));
    }
    set
    {
      if (!value.Value)
        return;
      this.AdjgDocType = ARPaymentType.GetVoidingARDocType(this.AdjgDocType) ?? "RPM";
      this.Voided = new bool?(true);
    }
  }

  [PXBool]
  public override bool? ReverseGainLoss
  {
    [PXDependsOnFields(new Type[] {typeof (ARAdjust2.adjgDocType)})] get
    {
      return new bool?(ARPaymentType.DrCr(this._AdjgDocType) == "C");
    }
    set
    {
    }
  }

  [PXDBBool]
  [DenormalizedFrom(new Type[] {typeof (ARPayment.isCCPayment), typeof (ARPayment.pendingProcessing), typeof (ARPayment.released), typeof (ARPayment.isCCAuthorized), typeof (ARPayment.isCCCaptured), typeof (ARPayment.isCCCaptureFailed)}, new Type[] {typeof (ARAdjust2.isCCPayment), typeof (ARAdjust2.paymentPendingProcessing), typeof (ARAdjust2.paymentReleased), typeof (ARAdjust2.isCCAuthorized), typeof (ARAdjust2.isCCCaptured), typeof (ARAdjust2.paymentCaptureFailed)}, null, typeof (ARAdjust2.adjgRefNbr))]
  public override bool? IsCCPayment { get; set; }

  [PXDBBool]
  public override bool? PaymentPendingProcessing { get; set; }

  [PXDBBool]
  public override bool? PaymentReleased { get; set; }

  [PXDBBool]
  public override bool? IsCCAuthorized { get; set; }

  [PXDBBool]
  public override bool? IsCCCaptured { get; set; }

  [PXDBBool]
  public override bool? PaymentCaptureFailed { get; set; }

  public new class PK : 
    PrimaryKeyOf<ARAdjust2>.By<ARAdjust2.adjgDocType, ARAdjust2.adjgRefNbr, ARAdjust2.adjNbr, ARAdjust2.adjdDocType, ARAdjust2.adjdRefNbr, ARAdjust2.adjdLineNbr>
  {
    public static ARAdjust2 Find(
      PXGraph graph,
      string adjgDocType,
      string adjgRefNbr,
      int? adjNbr,
      string adjdDocType,
      string adjdRefNbr,
      int? adjdLineNbr,
      PKFindOptions options = 0)
    {
      return (ARAdjust2) PrimaryKeyOf<ARAdjust2>.By<ARAdjust2.adjgDocType, ARAdjust2.adjgRefNbr, ARAdjust2.adjNbr, ARAdjust2.adjdDocType, ARAdjust2.adjdRefNbr, ARAdjust2.adjdLineNbr>.FindBy(graph, (object) adjgDocType, (object) adjgRefNbr, (object) adjNbr, (object) adjdDocType, (object) adjdRefNbr, (object) adjdLineNbr, options);
    }
  }

  public new static class FK
  {
    public class PaymentCustomer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARAdjust2>.By<ARAdjust2.customerID>
    {
    }

    public class Payment : 
      PrimaryKeyOf<ARRegister>.By<ARRegister.docType, ARRegister.refNbr>.ForeignKeyOf<ARAdjust2>.By<ARAdjust2.adjgDocType, ARAdjust2.adjgRefNbr>
    {
    }

    public class PaymentCurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARAdjust2>.By<ARAdjust2.adjgCuryInfoID>
    {
    }

    public class PaymentBranch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARAdjust2>.By<ARAdjust2.adjgBranchID>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<ARRegister>.By<ARRegister.docType, ARRegister.refNbr>.ForeignKeyOf<ARAdjust2>.By<ARAdjust2.adjdDocType, ARAdjust2.adjdRefNbr>
    {
    }

    public class InvoiceCurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARAdjust2>.By<ARAdjust2.adjdCuryInfoID>
    {
    }

    public class InvoiceBranch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARAdjust2>.By<ARAdjust2.adjdBranchID>
    {
    }
  }

  public new abstract class selected : IBqlField, IBqlOperand
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust2.customerID>
  {
  }

  public new abstract class adjgDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust2.adjgDocType>
  {
  }

  public new abstract class adjgRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust2.adjgRefNbr>
  {
  }

  public new abstract class adjgBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust2.adjgBranchID>
  {
  }

  public new abstract class adjdCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARAdjust2.adjdCuryInfoID>
  {
  }

  public new abstract class adjdDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust2.adjdDocType>
  {
  }

  public new abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust2.adjdRefNbr>
  {
  }

  public new abstract class adjdLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust2.adjdLineNbr>
  {
  }

  public new abstract class adjdOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust2.adjdOrderType>
  {
  }

  public new abstract class adjdOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust2.adjdOrderNbr>
  {
  }

  public new abstract class adjdBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust2.adjdBranchID>
  {
  }

  public new abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust2.adjNbr>
  {
  }

  public new abstract class adjBatchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust2.adjBatchNbr>
  {
  }

  public new abstract class adjdOrigCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    ARAdjust2.adjdOrigCuryInfoID>
  {
  }

  public new abstract class adjgCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARAdjust2.adjgCuryInfoID>
  {
  }

  public new abstract class adjgDocDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARAdjust2.adjgDocDate>
  {
  }

  public new abstract class adjgFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust2.adjgFinPeriodID>
  {
  }

  public new abstract class adjgTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust2.adjgTranPeriodID>
  {
  }

  public new abstract class adjdDocDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARAdjust2.adjdDocDate>
  {
  }

  public new abstract class adjdFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust2.adjdFinPeriodID>
  {
  }

  public new abstract class adjdTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust2.adjdTranPeriodID>
  {
  }

  public new abstract class curyAdjdDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARAdjust2.curyAdjdDiscAmt>
  {
  }

  public new abstract class curyAdjdWOAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARAdjust2.curyAdjdWOAmt>
  {
  }

  public new abstract class curyAdjdAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust2.curyAdjdAmt>
  {
  }

  public new abstract class curyAdjdOrigAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARAdjust2.curyAdjdOrigAmt>
  {
  }

  public new abstract class invoiceID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARAdjust2.invoiceID>
  {
  }

  public new abstract class paymentID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARAdjust2.paymentID>
  {
  }

  public new abstract class memoID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARAdjust2.memoID>
  {
  }

  public new abstract class adjAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust2.adjAmt>
  {
  }

  public new abstract class adjSignedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARAdjust2.adjSignedAmt>
  {
  }

  public new abstract class adjWOAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust2.adjWOAmt>
  {
  }

  public new abstract class adjDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust2.adjDiscAmt>
  {
  }

  public new abstract class curyAdjgDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARAdjust2.curyAdjgDiscAmt>
  {
  }

  public new abstract class curyAdjgWOAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARAdjust2.curyAdjgWOAmt>
  {
  }

  public new abstract class curyAdjgAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust2.curyAdjgAmt>
  {
  }

  public new abstract class curyAdjgSignedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARAdjust2.curyAdjgSignedAmt>
  {
  }

  public new abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust2.rGOLAmt>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust2.released>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust2.hold>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust2.voided>
  {
  }

  public new abstract class voidAdjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust2.voidAdjNbr>
  {
  }

  public new abstract class adjdARAcct : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust2.adjdARAcct>
  {
  }

  public new abstract class adjdARSub : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust2.adjdARSub>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARAdjust2.noteID>
  {
  }

  public new abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust2.curyDocBal>
  {
  }

  public new abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust2.docBal>
  {
  }

  public new abstract class curyDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust2.curyDiscBal>
  {
  }

  public new abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust2.discBal>
  {
  }

  public new abstract class curyWOBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust2.curyWOBal>
  {
  }

  public new abstract class wOBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust2.wOBal>
  {
  }

  public new abstract class voidAppl : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust2.voidAppl>
  {
  }

  public new abstract class reverseGainLoss : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARAdjust2.reverseGainLoss>
  {
  }

  public new abstract class isCCPayment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust2.isCCPayment>
  {
  }

  public new abstract class paymentPendingProcessing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARAdjust2.paymentPendingProcessing>
  {
  }

  public new abstract class paymentReleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARAdjust2.paymentReleased>
  {
  }

  public new abstract class isCCAuthorized : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust2.isCCAuthorized>
  {
  }

  public new abstract class isCCCaptured : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust2.isCCCaptured>
  {
  }

  public new abstract class paymentCaptureFailed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARAdjust2.paymentCaptureFailed>
  {
  }

  public new abstract class recalculatable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust2.recalculatable>
  {
  }
}
