// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTranPostGL
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

[PXProjection(typeof (Select2<ARTranPost, LeftJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARTranPost.curyInfoID>>>>), Persistent = false)]
[PXCacheName("AR Document Post GL")]
public class ARTranPostGL : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(IsKey = true, BqlTable = typeof (ARTranPost))]
  [PXUIField(DisplayName = "Doc. Type")]
  [ARDocType.List]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(IsKey = true, BqlTable = typeof (ARTranPost))]
  [PXUIField]
  public virtual string RefNbr { get; set; }

  [PXDBInt(BqlTable = typeof (ARTranPost))]
  [PXUIField(DisplayName = "Line Nbr.")]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(IsKey = true, BqlTable = typeof (ARTranPost))]
  public virtual int? ID { get; set; }

  [PXDBInt(BqlTable = typeof (ARTranPost))]
  public virtual int? AdjNbr { get; set; }

  [PXDBGuid(false, BqlTable = typeof (ARTranPost))]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBString(BqlTable = typeof (ARTranPost))]
  [PXUIField(DisplayName = "Source Doc. Type")]
  [ARDocType.List]
  public virtual string SourceDocType { get; set; }

  [PXDBString(BqlTable = typeof (ARTranPost))]
  [PXUIField]
  public virtual string SourceRefNbr { get; set; }

  [PXDBString(BqlTable = typeof (PX.Objects.CM.Extensions.CurrencyInfo))]
  public virtual string CuryID { get; set; }

  [PXDBLong(BqlTable = typeof (ARTranPost))]
  public virtual long? CuryInfoID { get; set; }

  [Branch(null, null, true, true, true, BqlTable = typeof (ARTranPost))]
  public virtual int? BranchID { get; set; }

  [PXDBDate(BqlTable = typeof (ARTranPost))]
  [PXDefault(typeof (ARRegister.docDate))]
  [PXUIField]
  public virtual DateTime? DocDate { get; set; }

  [Customer(BqlTable = typeof (ARTranPost))]
  public virtual int? CustomerID { get; set; }

  [Account(BqlTable = typeof (ARTranPost))]
  public virtual int? AccountID { get; set; }

  [SubAccount(BqlTable = typeof (ARTranPost))]
  public virtual int? SubID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlTable = typeof (ARTranPost))]
  [PXUIField(DisplayName = "Application Period")]
  public virtual string FinPeriodID { get; set; }

  [PeriodID(null, null, null, true, BqlTable = typeof (ARTranPost))]
  public virtual string TranPeriodID { get; set; }

  [PXDBShort(BqlTable = typeof (ARTranPost))]
  public virtual short? BalanceSign { get; set; }

  [PXDBString(15, IsUnicode = true, BqlTable = typeof (ARTranPost))]
  [PXUIField]
  [PX.Objects.GL.BatchNbr(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAR>>>), IsMigratedRecordField = typeof (ARTranPostGL.isMigratedRecord), BqlTable = typeof (ARTranPost))]
  public virtual string BatchNbr { get; set; }

  [PXDBString(BqlTable = typeof (ARTranPost))]
  [ARTranPost.type.List]
  public virtual string Type { get; set; }

  [PXDBString(BqlTable = typeof (ARTranPost))]
  public virtual string TranClass { get; set; }

  [PXDBString(BqlTable = typeof (ARTranPost))]
  public virtual string TranType { get; set; }

  [PXDBString(BqlTable = typeof (ARTranPost))]
  public virtual string TranRefNbr { get; set; }

  [PXDBInt(BqlTable = typeof (ARTranPost))]
  public virtual int? ReferenceID { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// in migration mode without affecting GL module.
  /// </summary>
  [PXDBBool(BqlTable = typeof (ARTranPost))]
  public virtual bool? IsMigratedRecord { get; set; }

  [PXDBBaseCury(BqlTable = typeof (ARTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RGOLAmt { get; set; }

  [PXDependsOnFields(new System.Type[] {typeof (ARTranPostGL.rGOLAmt), typeof (ARTranPostGL.sourceDocType)})]
  public virtual Decimal? SignedRGOLAmt
  {
    get
    {
      Decimal? rgolAmt = this.RGOLAmt;
      Decimal? nullable = ARDocType.SignBalance(this.SourceDocType);
      return !(rgolAmt.HasValue & nullable.HasValue) ? new Decimal?() : new Decimal?(rgolAmt.GetValueOrDefault() * nullable.GetValueOrDefault());
    }
  }

  [PXCurrency(typeof (ARTranPostGL.curyInfoID), typeof (ARTranPostGL.balanceAmt), BaseCalc = false)]
  [PXUIField(DisplayName = "Balance")]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPost.type, IBqlString>.IsIn<ARTranPost.type.origin, ARTranPost.type.adjustment, ARTranPost.type.@void, ARTranPost.type.installment>>, BqlOperand<ARTranPost.glSign, IBqlShort>.Multiply<ARTranPost.curyAmt>, Case<Where<BqlOperand<ARTranPost.type, IBqlString>.IsEqual<ARTranPost.type.application>>, BqlFunction<Minus<ARTranPost.glSign>, IBqlDecimal>.Multiply<BqlFunction<Add<ARTranPost.curyDiscAmt, ARTranPost.curyWOAmt>, IBqlDecimal>.Add<ARTranPost.curyAmt>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryBalanceAmt { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPost.type, IBqlString>.IsIn<ARTranPost.type.origin, ARTranPost.type.adjustment, ARTranPost.type.@void, ARTranPost.type.installment>>, BqlOperand<ARTranPost.glSign, IBqlShort>.Multiply<ARTranPost.amt>, Case<Where<BqlOperand<ARTranPost.type, IBqlString>.IsEqual<ARTranPost.type.application>>, BqlFunctionMirror<Mult<Minus<ARTranPost.glSign>, BqlFunction<Add<ARTranPost.amt, ARTranPost.discAmt>, IBqlDecimal>.Add<ARTranPost.wOAmt>>, IBqlDecimal>.Subtract<ARTranPost.rGOLAmt>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? BalanceAmt { get; set; }

  [PXCurrency(typeof (ARTranPostGL.curyInfoID), typeof (ARTranPostGL.debitARAmt), BaseCalc = false)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPost.accountID, IBqlInt>.IsNull>, decimal0, Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.origin>>>>, And<BqlOperand<ARTranPost.glSign, IBqlShort>.IsEqual<short1>>>>.And<BqlOperand<ARTranPost.docType, IBqlString>.IsNotIn<ARDocType.smallCreditWO, ARDocType.smallBalanceWO>>>, ARTranPost.curyAmt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.adjustment>>>>>.And<BqlOperand<ARTranPost.glSign, IBqlShort>.IsEqual<short1>>>, ARTranPost.curyAmt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.application>>>>>.And<BqlOperand<ARTranPost.glSign, IBqlShort>.IsEqual<shortMinus1>>>, BqlFunction<Add<ARTranPost.curyAmt, ARTranPost.curyDiscAmt>, IBqlDecimal>.Add<ARTranPost.curyWOAmt>>>>>, decimal0>), typeof (Decimal))]
  [PXUIField(DisplayName = "Debit AR Amt.")]
  public virtual Decimal? CuryDebitARAmt { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPost.accountID, IBqlInt>.IsNull>, decimal0, Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.origin>>>>, And<BqlOperand<ARTranPost.glSign, IBqlShort>.IsEqual<short1>>>>.And<BqlOperand<ARTranPost.docType, IBqlString>.IsNotIn<ARDocType.smallCreditWO, ARDocType.smallBalanceWO>>>, ARTranPost.amt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.adjustment>>>>>.And<BqlOperand<ARTranPost.glSign, IBqlShort>.IsEqual<short1>>>, ARTranPost.amt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.application>>>>>.And<BqlOperand<ARTranPost.glSign, IBqlShort>.IsEqual<shortMinus1>>>, BqlFunctionMirror<Add<Add<ARTranPost.amt, ARTranPost.discAmt>, ARTranPost.wOAmt>, IBqlDecimal>.Subtract<ARTranPost.rGOLAmt>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.rounding>>>>>.And<BqlOperand<ARTranPost.rGOLAmt, IBqlDecimal>.IsLess<Zero>>>, BqlOperand<ARTranPost.rGOLAmt, IBqlDecimal>.Multiply<shortMinus1>>>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? DebitARAmt { get; set; }

  [PXCurrency(typeof (ARTranPostGL.curyInfoID), typeof (ARTranPostGL.creditARAmt), BaseCalc = false)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPost.accountID, IBqlInt>.IsNull>, decimal0, Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.origin>>>>, And<BqlOperand<ARTranPost.glSign, IBqlShort>.IsEqual<shortMinus1>>>>.And<BqlOperand<ARTranPost.docType, IBqlString>.IsNotIn<ARDocType.smallCreditWO, ARDocType.smallBalanceWO>>>, ARTranPost.curyAmt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.adjustment>>>>>.And<BqlOperand<ARTranPost.glSign, IBqlShort>.IsEqual<shortMinus1>>>, ARTranPost.curyAmt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.@void>>>>>.And<BqlOperand<ARTranPost.docType, IBqlString>.IsNotIn<ARDocType.smallCreditWO, ARDocType.smallBalanceWO>>>, Minus<ARTranPost.curyAmt>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.application>>>>>.And<BqlOperand<ARTranPost.glSign, IBqlShort>.IsEqual<short1>>>, BqlFunction<Add<ARTranPost.curyAmt, ARTranPost.curyDiscAmt>, IBqlDecimal>.Add<ARTranPost.curyWOAmt>>>>>>, decimal0>), typeof (Decimal))]
  [PXUIField(DisplayName = "Credit AR Amt.")]
  public virtual Decimal? CuryCreditARAmt { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPost.accountID, IBqlInt>.IsNull>, decimal0, Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.origin>>>>, And<BqlOperand<ARTranPost.glSign, IBqlShort>.IsEqual<shortMinus1>>>>.And<BqlOperand<ARTranPost.docType, IBqlString>.IsNotIn<ARDocType.smallCreditWO, ARDocType.smallBalanceWO>>>, ARTranPost.amt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.adjustment>>>>>.And<BqlOperand<ARTranPost.glSign, IBqlShort>.IsEqual<shortMinus1>>>, ARTranPost.amt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.@void>>>>>.And<BqlOperand<ARTranPost.docType, IBqlString>.IsNotIn<ARDocType.smallCreditWO, ARDocType.smallBalanceWO>>>, Minus<ARTranPost.amt>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.application>>>>>.And<BqlOperand<ARTranPost.glSign, IBqlShort>.IsEqual<short1>>>, BqlFunctionMirror<Add<Add<ARTranPost.amt, ARTranPost.discAmt>, ARTranPost.wOAmt>, IBqlDecimal>.Add<ARTranPost.rGOLAmt>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.type, Equal<ARTranPost.type.rounding>>>>>.And<BqlOperand<ARTranPost.rGOLAmt, IBqlDecimal>.IsGreater<Zero>>>, ARTranPost.rGOLAmt>>>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CreditARAmt { get; set; }

  [PXCurrency(typeof (ARTranPostGL.curyInfoID), typeof (ARTranPostGL.turnAmt), BaseCalc = false)]
  [PXDBCalced(typeof (BqlOperand<ARTranPost.glSign, IBqlShort>.Multiply<ARTranPost.curyAmt>), typeof (Decimal))]
  public virtual Decimal? CuryTurnAmt { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (BqlOperand<ARTranPost.glSign, IBqlShort>.Multiply<ARTranPost.amt>), typeof (Decimal))]
  public virtual Decimal? TurnAmt { get; set; }

  [PXCurrency(typeof (ARTranPostGL.curyInfoID), typeof (ARTranPostGL.turnDiscAmt), BaseCalc = false)]
  [PXDBCalced(typeof (BqlOperand<ARTranPost.glSign, IBqlShort>.Multiply<ARTranPost.curyDiscAmt>), typeof (Decimal))]
  public virtual Decimal? CuryTurnDiscAmt { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (BqlOperand<ARTranPost.glSign, IBqlShort>.Multiply<ARTranPost.discAmt>), typeof (Decimal))]
  public virtual Decimal? TurnDiscAmt { get; set; }

  [PXCurrency(typeof (ARTranPostGL.curyInfoID), typeof (ARTranPostGL.turnItemDiscAmt), BaseCalc = false)]
  [PXDBCalced(typeof (BqlOperand<ARTranPost.glSign, IBqlShort>.Multiply<ARTranPost.curyItemDiscAmt>), typeof (Decimal))]
  public virtual Decimal? CuryTurnItemDiscAmt { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (BqlOperand<ARTranPost.glSign, IBqlShort>.Multiply<ARTranPost.itemDiscAmt>), typeof (Decimal))]
  public virtual Decimal? TurnItemDiscAmt { get; set; }

  [PXCurrency(typeof (ARTranPostGL.curyInfoID), typeof (ARTranPostGL.turnWOAmt), BaseCalc = false)]
  [PXDBCalced(typeof (BqlOperand<ARTranPost.glSign, IBqlShort>.Multiply<ARTranPost.curyWOAmt>), typeof (Decimal))]
  public virtual Decimal? CuryTurnWOAmt { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (BqlOperand<ARTranPost.glSign, IBqlShort>.Multiply<ARTranPost.wOAmt>), typeof (Decimal))]
  public virtual Decimal? TurnWOAmt { get; set; }

  [PXCurrency(typeof (ARTranPostGL.curyInfoID), typeof (ARTranPostGL.turnRetainageAmt), BaseCalc = false)]
  [PXDBCalced(typeof (BqlOperand<ARTranPost.glSign, IBqlShort>.Multiply<ARTranPost.curyRetainageAmt>), typeof (Decimal))]
  public virtual Decimal? CuryTurnRetainageAmt { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (BqlOperand<ARTranPost.glSign, IBqlShort>.Multiply<ARTranPost.retainageAmt>), typeof (Decimal))]
  public virtual Decimal? TurnRetainageAmt { get; set; }

  [PXCurrency(typeof (ARTranPostGL.curyInfoID), typeof (ARTranPostGL.retainageReleasedAmt), BaseCalc = false)]
  [PXDBCalced(typeof (IIf<BqlOperand<ARTranPost.type, IBqlString>.IsEqual<ARTranPost.type.retainage>, BqlFunction<Minus<ARTranPost.balanceSign>, IBqlDecimal>.Multiply<BqlOperand<ARTranPost.glSign, IBqlShort>.Multiply<ARTranPost.curyRetainageAmt>>, Zero>), typeof (Decimal))]
  public virtual Decimal? CuryRetainageReleasedAmt { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (IIf<BqlOperand<ARTranPost.type, IBqlString>.IsEqual<ARTranPost.type.retainage>, BqlFunction<Minus<ARTranPost.balanceSign>, IBqlDecimal>.Multiply<BqlOperand<ARTranPost.glSign, IBqlShort>.Multiply<ARTranPost.retainageAmt>>, Zero>), typeof (Decimal))]
  public virtual Decimal? RetainageReleasedAmt { get; set; }

  [PXCurrency(typeof (ARTranPostGL.curyInfoID), typeof (ARTranPostGL.retainageUnreleasedAmt), BaseCalc = false)]
  [PXDBCalced(typeof (BqlOperand<ARTranPost.balanceSign, IBqlShort>.Multiply<BqlOperand<ARTranPost.glSign, IBqlShort>.Multiply<ARTranPost.curyRetainageAmt>>), typeof (Decimal))]
  public virtual Decimal? CuryRetainageUnreleasedAmt { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (BqlOperand<ARTranPost.balanceSign, IBqlShort>.Multiply<BqlOperand<ARTranPost.glSign, IBqlShort>.Multiply<ARTranPost.retainageAmt>>), typeof (Decimal))]
  public virtual Decimal? RetainageUnreleasedAmt { get; set; }

  [PXCurrency(typeof (ARTranPostGL.curyInfoID), typeof (ARTranPostGL.retainagePaidTotal), BaseCalc = false)]
  [PXDBCalced(typeof (IIf<BqlOperand<ARTranPost.type, IBqlString>.IsEqual<ARTranPost.type.retainagePayment>, BqlFunction<Mult<ARTranPost.glSign, ARTranPost.balanceSign>, IBqlShort>.Multiply<BqlOperand<ARTranPost.curyAmt, IBqlDecimal>.Add<BqlOperand<ARTranPost.curyDiscAmt, IBqlDecimal>.Add<ARTranPost.curyWOAmt>>>, Zero>), typeof (Decimal))]
  public virtual Decimal? CuryRetainagePaidTotal { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (IIf<BqlOperand<ARTranPost.type, IBqlString>.IsEqual<ARTranPost.type.retainagePayment>, BqlFunction<Mult<ARTranPost.glSign, ARTranPost.balanceSign>, IBqlShort>.Multiply<BqlOperand<ARTranPost.amt, IBqlDecimal>.Add<BqlOperand<ARTranPost.discAmt, IBqlDecimal>.Add<ARTranPost.wOAmt>>>, Zero>), typeof (Decimal))]
  public virtual Decimal? RetainagePaidTotal { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARTranPost.type, IBqlString>.IsEqual<ARTranPost.type.rgol>>, ARTranPost.rGOLAmt>, decimal0>), typeof (Decimal))]
  public virtual Decimal? TurnRGOLAmt { get; set; }

  [PXDBDate(BqlTable = typeof (ARTranPost))]
  public virtual DateTime? StatementDate { get; set; }

  [PXDBInt(BqlTable = typeof (ARTranPost))]
  public virtual int? VoidAdjNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARTranPost.GLSign" />
  [PXDBShort(BqlField = typeof (ARTranPost.glSign))]
  public virtual short? GLSign { get; set; }

  public class PK : 
    PrimaryKeyOf<ARTranPostGL>.By<ARTranPostGL.docType, ARTranPostGL.refNbr, ARTranPostGL.lineNbr, ARTranPostGL.iD>
  {
    public static ARTranPostGL Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      int? id,
      PKFindOptions options = 0)
    {
      return (ARTranPostGL) PrimaryKeyOf<ARTranPostGL>.By<ARTranPostGL.docType, ARTranPostGL.refNbr, ARTranPostGL.lineNbr, ARTranPostGL.iD>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, (object) id, options);
    }
  }

  public static class FK
  {
    public class Document : 
      PrimaryKeyOf<ARRegister>.By<ARRegister.docType, ARRegister.refNbr>.ForeignKeyOf<ARTran>.By<ARTranPostGL.docType, ARTranPostGL.refNbr>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<ARInvoice>.By<ARInvoice.docType, ARInvoice.refNbr>.ForeignKeyOf<ARTran>.By<ARTranPostGL.docType, ARTranPostGL.refNbr>
    {
    }

    public class Payment : 
      PrimaryKeyOf<ARPayment>.By<ARPayment.docType, ARPayment.refNbr>.ForeignKeyOf<ARTran>.By<ARTranPostGL.docType, ARTranPostGL.refNbr>
    {
    }

    public class CashSale : 
      PrimaryKeyOf<ARCashSale>.By<ARCashSale.docType, ARCashSale.refNbr>.ForeignKeyOf<ARTran>.By<ARTranPostGL.docType, ARTranPostGL.refNbr>
    {
    }

    public class SOInvoice : 
      PrimaryKeyOf<PX.Objects.SO.SOInvoice>.By<PX.Objects.SO.SOInvoice.docType, PX.Objects.SO.SOInvoice.refNbr>.ForeignKeyOf<ARTran>.By<ARTranPostGL.docType, ARTranPostGL.refNbr>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARTran>.By<ARTranPostGL.branchID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARTran>.By<ARTranPostGL.curyInfoID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARTran>.By<ARTranPostGL.customerID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ARTran>.By<ARTranPostGL.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ARTran>.By<ARTranPostGL.subID>
    {
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostGL.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostGL.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPostGL.lineNbr>
  {
  }

  public abstract class iD : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPostGL.iD>
  {
  }

  public abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPostGL.adjNbr>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARTranPostGL.refNoteID>
  {
  }

  public abstract class sourceDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostGL.sourceDocType>
  {
  }

  public abstract class sourceRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostGL.sourceRefNbr>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARTranPostGL.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARTranPostGL.curyInfoID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPostGL.branchID>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARTranPostGL.docDate>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPostGL.customerID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPostGL.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPostGL.subID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostGL.finPeriodID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostGL.tranPeriodID>
  {
  }

  public abstract class balanceSign : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARTranPostGL.balanceSign>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostGL.batchNbr>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostGL.type>
  {
  }

  public abstract class tranClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostGL.tranClass>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostGL.tranType>
  {
  }

  public abstract class tranRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostGL.tranRefNbr>
  {
  }

  public abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPostGL.referenceID>
  {
  }

  public abstract class isMigratedRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARTranPostGL.isMigratedRecord>
  {
  }

  public abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostGL.rGOLAmt>
  {
  }

  public abstract class signedRGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostGL.rGOLAmt>
  {
  }

  public abstract class curyBalanceAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostGL.curyBalanceAmt>
  {
  }

  public abstract class balanceAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostGL.balanceAmt>
  {
  }

  public abstract class curyDebitARAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostGL.curyDebitARAmt>
  {
  }

  public abstract class debitARAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostGL.debitARAmt>
  {
  }

  public abstract class curyCreditARAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostGL.curyCreditARAmt>
  {
  }

  public abstract class creditARAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostGL.creditARAmt>
  {
  }

  public abstract class curyTurnAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostGL.curyTurnAmt>
  {
  }

  public abstract class turnAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostGL.turnAmt>
  {
  }

  public abstract class curyTurnDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostGL.curyTurnDiscAmt>
  {
  }

  public abstract class turnDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostGL.turnDiscAmt>
  {
  }

  public abstract class curyTurnItemDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostGL.curyTurnItemDiscAmt>
  {
  }

  public abstract class turnItemDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostGL.turnItemDiscAmt>
  {
  }

  public abstract class curyTurnWOAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostGL.curyTurnWOAmt>
  {
  }

  public abstract class turnWOAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostGL.turnWOAmt>
  {
  }

  public abstract class curyTurnRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostGL.curyTurnRetainageAmt>
  {
  }

  public abstract class turnRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostGL.turnRetainageAmt>
  {
  }

  public abstract class curyRetainageReleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostGL.curyRetainageReleasedAmt>
  {
  }

  public abstract class retainageReleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostGL.retainageReleasedAmt>
  {
  }

  public abstract class curyRetainageUnreleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostGL.curyRetainageUnreleasedAmt>
  {
  }

  public abstract class retainageUnreleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostGL.retainageUnreleasedAmt>
  {
  }

  public abstract class curyRetainagePaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostGL.curyRetainagePaidTotal>
  {
  }

  public abstract class retainagePaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostGL.retainagePaidTotal>
  {
  }

  public abstract class turnRGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostGL.turnRGOLAmt>
  {
  }

  public abstract class statementDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARTranPostGL.statementDate>
  {
  }

  public abstract class voidAdjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPostGL.voidAdjNbr>
  {
  }

  public abstract class glSign : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARTranPostGL.glSign>
  {
  }
}
