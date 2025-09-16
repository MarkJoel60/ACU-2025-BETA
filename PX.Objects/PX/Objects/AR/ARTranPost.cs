// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTranPost
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR.Standalone;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXCacheName("AR Document transaction")]
public class ARTranPost : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Branch(typeof (ARRegister.branchID), null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [PXDBString(IsKey = true)]
  [PXUIField(DisplayName = "Doc. Type")]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(IsKey = true)]
  [PXParent(typeof (Select<ARRegister, Where<ARRegister.docType, Equal<Current<ARTranPost.docType>>, And<ARRegister.refNbr, Equal<Current<ARTranPost.refNbr>>>>>))]
  [PXUIField]
  public virtual string RefNbr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField]
  public virtual int? LineNbr { get; set; }

  [PXDBIdentity(IsKey = true)]
  public virtual int? ID { get; set; }

  [PXDBInt]
  public virtual int? AdjNbr { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBString(3, IsFixed = true)]
  [ARDocType.List]
  [PXUIField(DisplayName = "Source Doc. Type")]
  public virtual string SourceDocType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField]
  public virtual string SourceRefNbr { get; set; }

  [PXDBDate]
  [PXDefault(typeof (ARRegister.docDate))]
  [PXUIField]
  public virtual DateTime? DocDate { get; set; }

  [Customer]
  [PXDefault]
  public virtual int? CustomerID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, typeof (ARTranPost.branchID), null, null, null, null, true, false, null, typeof (ARTranPost.tranPeriodID), null, true, true)]
  [PXUIField(DisplayName = "Application Period")]
  public virtual string FinPeriodID { get; set; }

  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (ARRegister.curyInfoID), Required = true)]
  public virtual long? CuryInfoID { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PX.Objects.GL.BatchNbr(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAR>>>), IsMigratedRecordField = typeof (ARTranPost.isMigratedRecord))]
  [PXUIField]
  public virtual string BatchNbr { get; set; }

  [Account(typeof (ARTranPost.branchID))]
  public virtual int? AccountID { get; set; }

  [SubAccount(typeof (ARTranPost.accountID), typeof (ARTranPost.branchID), true)]
  public virtual int? SubID { get; set; }

  [PXDBCurrency(typeof (ARTranPost.curyInfoID), typeof (ARTranPost.amt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? CuryAmt { get; set; }

  [PXDBCurrency(typeof (ARTranPost.curyInfoID), typeof (ARTranPost.ppdAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cash Discount Taken")]
  public virtual Decimal? CuryPPDAmt { get; set; }

  [PXDBCurrency(typeof (ARTranPost.curyInfoID), typeof (ARTranPost.discAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cash Discount Taken")]
  public virtual Decimal? CuryDiscAmt { get; set; }

  [PXDBCurrency(typeof (ARTranPost.curyInfoID), typeof (ARTranPost.retainageAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryRetainageAmt { get; set; }

  [PXDBCurrency(typeof (ARTranPost.curyInfoID), typeof (ARTranPost.wOAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Write-Off Amount")]
  public virtual Decimal? CuryWOAmt { get; set; }

  [PXDBCurrency(typeof (ARTranPost.curyInfoID), typeof (ARTranPost.itemDiscAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryItemDiscAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Amt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PPDAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainageAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? WOAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ItemDiscAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RGOLAmt { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// in migration mode without affecting GL module.
  /// </summary>
  [PXDBBool]
  public virtual bool? IsMigratedRecord { get; set; }

  [PXBool]
  public virtual bool? IsVoidPrepayment { get; set; }

  [PXDBString]
  [ARTranPost.type.List]
  [PXUIField(DisplayName = "Transaction type")]
  public virtual string Type { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Tran. Type")]
  public virtual string TranType { get; set; }

  [PXDBString]
  [PXUIField]
  public virtual string TranRefNbr { get; set; }

  [Customer]
  public virtual int? ReferenceID { get; set; }

  [PXDBShort]
  [PXFormula(typeof (Switch<Case<Where<BqlOperand<ARTranPost.docType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.cashSale, ARDocType.smallCreditWO, ARDocType.prepaymentInvoice>>, short1>, shortMinus1>))]
  public virtual short? BalanceSign { get; set; }

  [PXDBShort]
  [PXFormula(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.@void>>>>>.And<BqlOperand<ARTranPost.tranType, IBqlString>.IsEqual<ARDocType.voidRefund>>>, shortMinus1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.retainageReverse>>>>>.And<BqlOperand<ARTranPost.docType, IBqlString>.IsIn<ARDocType.invoice, ARDocType.debitMemo>>>, shortMinus1, Case<Where<BqlOperand<ARTranPost.type, IBqlString>.IsEqual<ARTranPost.type.retainageReverse>>, short1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.retainage>>>>>.And<BqlOperand<ARTranPost.tranType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, shortMinus1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.retainage>>>>>.And<BqlOperand<ARTranPost.tranType, IBqlString>.IsIn<ARDocType.invoice, ARDocType.debitMemo>>>, short1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.application>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARTranPost.sourceDocType, IBqlString>.IsEqual<ARDocType.refund>>>>, shortMinus1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.adjustment>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.docType, Equal<ARDocType.refund>>>>>.And<BqlOperand<ARTranPost.sourceDocType, IBqlString>.IsEqual<ARDocType.prepaymentInvoice>>>>, shortMinus1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, NotIn3<ARTranPost.type.adjustment, ARTranPost.type.rgol>>>>>.And<BqlOperand<ARTranPost.docType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.cashSale, ARDocType.smallCreditWO, ARDocType.prepaymentInvoice>>>, short1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.adjustment>>>>>.And<BqlOperand<ARTranPost.sourceDocType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.cashSale, ARDocType.smallCreditWO, ARDocType.prepaymentInvoice>>>, short1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.rgol>>>>>.And<BqlOperand<ARTranPost.docType, IBqlString>.IsEqual<ARDocType.cashReturn>>>, short1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.@void>>>>>.And<BqlOperand<ARTranPost.docType, IBqlString>.IsEqual<ARDocType.smallBalanceWO>>>, shortMinus1, Case<Where<BqlOperand<ARTranPost.type, IBqlString>.IsEqual<ARTranPost.type.@void>>, short1>>>>>>>>>>>>, shortMinus1>))]
  public virtual short? GLSign { get; set; }

  [PXDBString]
  [PXFormula(typeof (Add<Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.application>>>>>.And<BqlOperand<ARTranPost.isMigratedRecord, IBqlBool>.IsEqual<True>>>, ARDocType.creditMemo, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.application>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.docType, Equal<ARDocType.smallCreditWO>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.sourceDocType, Equal<ARDocType.voidPayment>>>>>.And<BqlOperand<ARTranPost.isVoidPrepayment, IBqlBool>.IsEqual<True>>>>>, ARDocType.smallCreditWO, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.adjustment>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.sourceDocType, Equal<ARDocType.smallCreditWO>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.docType, Equal<ARDocType.voidPayment>>>>>.And<BqlOperand<ARTranPost.isVoidPrepayment, IBqlBool>.IsEqual<True>>>>>, ARDocType.prepayment, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.application>>>>>.And<Not<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.docType, Equal<ARDocType.smallCreditWO>>>>>.And<BqlOperand<ARTranPost.adjNbr, IBqlInt>.IsEqual<int_1>>>>>, ARTranPost.sourceDocType, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.adjustment>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.sourceDocType, Equal<ARDocType.smallCreditWO>>>>>.And<BqlOperand<ARTranPost.adjNbr, IBqlInt>.IsEqual<int_1>>>>, ARTranPost.sourceDocType, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.rgol>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.sourceDocType, Equal<ARDocType.smallCreditWO>>>>>.And<BqlOperand<ARTranPost.adjNbr, IBqlInt>.IsEqual<int_1>>>>, ARTranPost.docType>>>>>>, ARTranPost.docType>, Switch<Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.adjustment>>>>, And<BqlOperand<ARTranPost.sourceDocType, IBqlString>.IsEqual<ARDocType.smallCreditWO>>>>.And<BqlOperand<ARTranPost.docType, IBqlString>.IsEqual<ARDocType.prepayment>>>, PX.Objects.GL.GLTran.tranClass.writeoff, Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.adjustment>>>>, And<BqlOperand<ARTranPost.sourceDocType, IBqlString>.IsEqual<ARDocType.smallCreditWO>>>>.And<BqlOperand<ARTranPost.adjNbr, IBqlInt>.IsEqual<int_1>>>, PX.Objects.GL.GLTran.tranClass.normal, Case<Where<BqlOperand<ARTranPost.type, IBqlString>.IsEqual<ARTranPost.type.adjustment>>, PX.Objects.GL.GLTran.tranClass.payment, Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.application>>>>, And<BqlOperand<ARTranPost.docType, IBqlString>.IsEqual<ARDocType.smallCreditWO>>>>.And<BqlOperand<ARTranPost.adjNbr, IBqlInt>.IsEqual<int_1>>>, PX.Objects.GL.GLTran.tranClass.payment, Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.application>>>>, And<BqlOperand<ARTranPost.docType, IBqlString>.IsEqual<ARDocType.smallCreditWO>>>>.And<BqlOperand<ARTranPost.sourceDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, Space, Case<Where<BqlOperand<ARTranPost.type, IBqlString>.IsEqual<ARTranPost.type.rgol>>, PX.Objects.GL.GLTran.tranClass.rgol, Case<Where<BqlOperand<ARTranPost.type, IBqlString>.IsEqual<ARTranPost.type.rounding>>, PX.Objects.GL.GLTran.tranClass.rounding, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, In3<ARTranPost.type.origin, ARTranPost.type.@void>>>>>.And<BqlOperand<ARTranPost.docType, IBqlString>.IsEqual<ARDocType.prepayment>>>, PX.Objects.GL.GLTran.tranClass.payment, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.@void>>>>>.And<BqlOperand<ARTranPost.docType, IBqlString>.IsEqual<ARDocType.smallCreditWO>>>, PX.Objects.GL.GLTran.tranClass.@void, Case<Where<BqlOperand<ARTranPost.docType, IBqlString>.IsIn<ARDocType.invoice, ARDocType.debitMemo, ARDocType.creditMemo, ARDocType.finCharge, ARDocType.cashSale, ARDocType.cashReturn, ARDocType.prepaymentInvoice>>, PX.Objects.GL.GLTran.tranClass.normal, Case<Where<BqlOperand<ARTranPost.docType, IBqlString>.IsIn<ARDocType.payment, ARDocType.voidPayment, ARDocType.refund, ARDocType.voidRefund>>, PX.Objects.GL.GLTran.tranClass.payment, Case<Where<BqlOperand<ARTranPost.docType, IBqlString>.IsIn<ARDocType.smallBalanceWO, ARDocType.smallCreditWO, ARDocType.prepayment>>, PX.Objects.GL.GLTran.tranClass.charge>>>>>>>>>>>>>>))]
  public virtual string TranClass { get; set; }

  [PXDBDate]
  public virtual DateTime? StatementDate { get; set; }

  [PXDBInt]
  public virtual int? VoidAdjNbr { get; set; }

  public class PK : 
    PrimaryKeyOf<ARTranPost>.By<ARTranPost.docType, ARTranPost.refNbr, ARTranPost.lineNbr, ARTranPost.iD>
  {
    public static ARTranPost Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      int? id,
      PKFindOptions options = 0)
    {
      return (ARTranPost) PrimaryKeyOf<ARTranPost>.By<ARTranPost.docType, ARTranPost.refNbr, ARTranPost.lineNbr, ARTranPost.iD>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, (object) id, options);
    }
  }

  public static class FK
  {
    public class Document : 
      PrimaryKeyOf<ARRegister>.By<ARRegister.docType, ARRegister.refNbr>.ForeignKeyOf<ARTranPost>.By<ARTranPost.docType, ARTranPost.refNbr>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<ARInvoice>.By<ARInvoice.docType, ARInvoice.refNbr>.ForeignKeyOf<ARTranPost>.By<ARTranPost.docType, ARTranPost.refNbr>
    {
    }

    public class Payment : 
      PrimaryKeyOf<ARPayment>.By<ARPayment.docType, ARPayment.refNbr>.ForeignKeyOf<ARTranPost>.By<ARTranPost.docType, ARTranPost.refNbr>
    {
    }

    public class CashSale : 
      PrimaryKeyOf<ARCashSale>.By<ARCashSale.docType, ARCashSale.refNbr>.ForeignKeyOf<ARTranPost>.By<ARTranPost.docType, ARTranPost.refNbr>
    {
    }

    public class SOInvoice : 
      PrimaryKeyOf<PX.Objects.SO.SOInvoice>.By<PX.Objects.SO.SOInvoice.docType, PX.Objects.SO.SOInvoice.refNbr>.ForeignKeyOf<ARTranPost>.By<ARTranPost.docType, ARTranPost.refNbr>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARTranPost>.By<ARTranPost.branchID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARTranPost>.By<ARTranPost.curyInfoID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARTranPost>.By<ARTranPost.customerID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ARTranPost>.By<ARTranPost.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ARTranPost>.By<ARTranPost.subID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPost.branchID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPost.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPost.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPost.lineNbr>
  {
  }

  public abstract class iD : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPost.iD>
  {
  }

  public abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPost.adjNbr>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARTranPost.refNoteID>
  {
  }

  public abstract class sourceDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPost.sourceDocType>
  {
  }

  public abstract class sourceRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPost.sourceRefNbr>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARTranPost.docDate>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPost.customerID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPost.finPeriodID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPost.tranPeriodID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARTranPost.curyInfoID>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPost.batchNbr>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPost.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPost.subID>
  {
  }

  public abstract class curyAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPost.curyAmt>
  {
  }

  public abstract class curyPPDAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPost.curyPPDAmt>
  {
  }

  public abstract class curyDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPost.curyDiscAmt>
  {
  }

  public abstract class curyRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPost.curyRetainageAmt>
  {
  }

  public abstract class curyWOAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPost.curyWOAmt>
  {
  }

  public abstract class curyItemDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPost.curyItemDiscAmt>
  {
  }

  public abstract class amt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPost.amt>
  {
  }

  public abstract class ppdAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPost.ppdAmt>
  {
  }

  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPost.discAmt>
  {
  }

  public abstract class retainageAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPost.retainageAmt>
  {
  }

  public abstract class wOAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPost.wOAmt>
  {
  }

  public abstract class itemDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPost.itemDiscAmt>
  {
  }

  public abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPost.rGOLAmt>
  {
  }

  public abstract class isMigratedRecord : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTranPost.isMigratedRecord>
  {
  }

  public abstract class isVoidPrepayment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTranPost.isVoidPrepayment>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPost.type>
  {
    public const string Origin = "S";
    public const string Application = "D";
    public const string Adjustment = "G";
    public const string Retainage = "F";
    public const string RetainagePayment = "T";
    public const string RetainageReverse = "U";
    public const string RGOL = "R";
    public const string Rounding = "X";
    public const string Voided = "V";
    public const string Installment = "I";

    public class origin : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.type.origin>
    {
      public origin()
        : base("S")
      {
      }
    }

    public class application : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.type.application>
    {
      public application()
        : base("D")
      {
      }
    }

    public class adjustment : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.type.adjustment>
    {
      public adjustment()
        : base("G")
      {
      }
    }

    public class retainage : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.type.retainage>
    {
      public retainage()
        : base("F")
      {
      }
    }

    public class retainageReverse : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ARTranPost.type.retainageReverse>
    {
      public retainageReverse()
        : base("U")
      {
      }
    }

    public class retainagePayment : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ARTranPost.type.retainagePayment>
    {
      public retainagePayment()
        : base("T")
      {
      }
    }

    public class rgol : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.type.rgol>
    {
      public rgol()
        : base("R")
      {
      }
    }

    public class rounding : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.type.rounding>
    {
      public rounding()
        : base("X")
      {
      }
    }

    public class @void : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.type.@void>
    {
      public @void()
        : base("V")
      {
      }
    }

    public class installment : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.type.installment>
    {
      public installment()
        : base("I")
      {
      }
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[9]
        {
          "S",
          "D",
          "G",
          "F",
          "U",
          "V",
          "I",
          "R",
          "X"
        }, new string[9]
        {
          "Original document",
          "Application",
          "Adjusted",
          "Release Retainage",
          "Reverse Retainage",
          "Voided",
          "Installment",
          "Realized and Rounding GOL",
          "Realized and Rounding GOL"
        })
      {
      }
    }
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPost.tranType>
  {
  }

  public abstract class tranRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPost.tranRefNbr>
  {
  }

  public abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPost.referenceID>
  {
  }

  public abstract class balanceSign : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARTranPost.balanceSign>
  {
  }

  public abstract class glSign : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARTranPost.glSign>
  {
  }

  public abstract class tranClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPost.tranClass>
  {
    public class CRMN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.CRMN>
    {
      public CRMN()
        : base(nameof (CRMN))
      {
      }
    }

    public class CRMP : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.CRMP>
    {
      public CRMP()
        : base(nameof (CRMP))
      {
      }
    }

    public class CRMR : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.CRMR>
    {
      public CRMR()
        : base(nameof (CRMR))
      {
      }
    }

    public class CRMU : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.CRMU>
    {
      public CRMU()
        : base(nameof (CRMU))
      {
      }
    }

    public class CRMX : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.CRMX>
    {
      public CRMX()
        : base(nameof (CRMX))
      {
      }
    }

    public class DRMN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.DRMN>
    {
      public DRMN()
        : base(nameof (DRMN))
      {
      }
    }

    public class FCHN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.FCHN>
    {
      public FCHN()
        : base(nameof (FCHN))
      {
      }
    }

    public class INVN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.INVN>
    {
      public INVN()
        : base(nameof (INVN))
      {
      }
    }

    public class PMTN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.PMTN>
    {
      public PMTN()
        : base(nameof (PMTN))
      {
      }
    }

    public class PMTP : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.PMTP>
    {
      public PMTP()
        : base(nameof (PMTP))
      {
      }
    }

    public class PMTR : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.PMTR>
    {
      public PMTR()
        : base(nameof (PMTR))
      {
      }
    }

    public class PMTU : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.PMTU>
    {
      public PMTU()
        : base(nameof (PMTU))
      {
      }
    }

    public class PMTX : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.PMTX>
    {
      public PMTX()
        : base(nameof (PMTX))
      {
      }
    }

    public class PPMB : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.PPMB>
    {
      public PPMB()
        : base(nameof (PPMB))
      {
      }
    }

    public class PPMN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.PPMN>
    {
      public PPMN()
        : base(nameof (PPMN))
      {
      }
    }

    public class PPMP : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.PPMP>
    {
      public PPMP()
        : base(nameof (PPMP))
      {
      }
    }

    public class PPMR : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.PPMR>
    {
      public PPMR()
        : base(nameof (PPMR))
      {
      }
    }

    public class PPMU : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.PPMU>
    {
      public PPMU()
        : base(nameof (PPMU))
      {
      }
    }

    public class REFN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.REFN>
    {
      public REFN()
        : base(nameof (REFN))
      {
      }
    }

    public class REFP : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.REFP>
    {
      public REFP()
        : base(nameof (REFP))
      {
      }
    }

    public class REFU : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.REFU>
    {
      public REFU()
        : base(nameof (REFU))
      {
      }
    }

    public class REFR : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.REFR>
    {
      public REFR()
        : base(nameof (REFR))
      {
      }
    }

    public class RPMN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.RPMN>
    {
      public RPMN()
        : base(nameof (RPMN))
      {
      }
    }

    public class RPMP : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.RPMP>
    {
      public RPMP()
        : base(nameof (RPMP))
      {
      }
    }

    public class RPMR : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.RPMR>
    {
      public RPMR()
        : base(nameof (RPMR))
      {
      }
    }

    public class RPMU : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.RPMU>
    {
      public RPMU()
        : base(nameof (RPMU))
      {
      }
    }

    public class SMBN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.SMBN>
    {
      public SMBN()
        : base(nameof (SMBN))
      {
      }
    }

    public class SMBP : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.SMBP>
    {
      public SMBP()
        : base(nameof (SMBP))
      {
      }
    }

    public class SMBR : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.SMBR>
    {
      public SMBR()
        : base(nameof (SMBR))
      {
      }
    }

    public class SMBU : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.SMBU>
    {
      public SMBU()
        : base(nameof (SMBU))
      {
      }
    }

    public class SMCB : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.SMCB>
    {
      public SMCB()
        : base(nameof (SMCB))
      {
      }
    }

    public class SMCN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.SMCN>
    {
      public SMCN()
        : base(nameof (SMCN))
      {
      }
    }

    public class SMCP : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.SMCP>
    {
      public SMCP()
        : base(nameof (SMCP))
      {
      }
    }

    public class SMCU : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.SMCU>
    {
      public SMCU()
        : base(nameof (SMCU))
      {
      }
    }

    public class VRFN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.VRFN>
    {
      public VRFN()
        : base(nameof (VRFN))
      {
      }
    }

    public class VRFP : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.VRFP>
    {
      public VRFP()
        : base(nameof (VRFP))
      {
      }
    }

    public class VRFR : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.VRFR>
    {
      public VRFR()
        : base(nameof (VRFR))
      {
      }
    }

    public class VRFU : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.VRFU>
    {
      public VRFU()
        : base(nameof (VRFU))
      {
      }
    }

    public class CSLN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.CSLN>
    {
      public CSLN()
        : base(nameof (CSLN))
      {
      }
    }

    public class CSLR : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.CSLR>
    {
      public CSLR()
        : base(nameof (CSLR))
      {
      }
    }

    public class RCSN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.RCSN>
    {
      public RCSN()
        : base(nameof (RCSN))
      {
      }
    }

    public class RCSR : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.RCSR>
    {
      public RCSR()
        : base(nameof (RCSR))
      {
      }
    }

    public class PPIN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.PPIN>
    {
      public PPIN()
        : base(nameof (PPIN))
      {
      }
    }

    public class PPIP : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.PPIP>
    {
      public PPIP()
        : base(nameof (PPIP))
      {
      }
    }

    public class PPIY : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.PPIY>
    {
      public PPIY()
        : base(nameof (PPIY))
      {
      }
    }

    public class CRMY : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARTranPost.tranClass.CRMY>
    {
      public CRMY()
        : base(nameof (CRMY))
      {
      }
    }
  }

  public abstract class statementDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARTranPost.statementDate>
  {
  }

  public abstract class voidAdjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPost.voidAdjNbr>
  {
  }
}
