// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APTranPostGL
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP.Standalone;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXProjection(typeof (Select2<APTranPost, LeftJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APTranPost.curyInfoID>>>>), Persistent = false)]
[PXCacheName("AP Document Post GL")]
public class APTranPostGL : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(IsKey = true, BqlTable = typeof (APTranPost))]
  [PXUIField(DisplayName = "Doc. Type")]
  [APDocType.List]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(IsKey = true, BqlTable = typeof (APTranPost))]
  [PXUIField(DisplayName = "Ref. Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual string RefNbr { get; set; }

  [PXDBInt(BqlTable = typeof (APTranPost))]
  [PXUIField(DisplayName = "Line Nbr.")]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(IsKey = true, BqlTable = typeof (APTranPost))]
  public virtual int? ID { get; set; }

  [PXDBString(BqlTable = typeof (APTranPost))]
  [PXUIField(DisplayName = "Source Doc. Type")]
  [APDocType.List]
  public virtual string SourceDocType { get; set; }

  [PXDBString(BqlTable = typeof (APTranPost))]
  [PXUIField(DisplayName = "Source Ref. Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual string SourceRefNbr { get; set; }

  [PXDBString(BqlTable = typeof (PX.Objects.CM.Extensions.CurrencyInfo))]
  public virtual string CuryID { get; set; }

  [PXDBLong(BqlTable = typeof (APTranPost))]
  public virtual long? CuryInfoID { get; set; }

  [Branch(null, null, true, true, true, BqlTable = typeof (APTranPost))]
  public virtual int? BranchID { get; set; }

  [Vendor(BqlTable = typeof (APTranPost))]
  public virtual int? VendorID { get; set; }

  [Account(BqlTable = typeof (APTranPost))]
  public virtual int? AccountID { get; set; }

  [SubAccount(BqlTable = typeof (APTranPost))]
  public virtual int? SubID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlTable = typeof (APTranPost))]
  [PXUIField(DisplayName = "Application Period")]
  public virtual string FinPeriodID { get; set; }

  [PeriodID(null, null, null, true, BqlTable = typeof (APTranPost))]
  public virtual string TranPeriodID { get; set; }

  [PXDBDate(BqlTable = typeof (APTranPost))]
  [PXDefault(typeof (APRegister.docDate))]
  [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual System.DateTime? DocDate { get; set; }

  [PXDBShort(BqlTable = typeof (APTranPost))]
  public virtual short? BalanceSign { get; set; }

  [PXDBString(15, IsUnicode = true, BqlTable = typeof (APTranPost))]
  [PXUIField(DisplayName = "Batch Number", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PX.Objects.GL.BatchNbr(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAP>>>), IsMigratedRecordField = typeof (APTranPostGL.isMigratedRecord), BqlTable = typeof (APTranPost))]
  public virtual string BatchNbr { get; set; }

  [PXDBString(BqlTable = typeof (APTranPost))]
  [APTranPost.type.List]
  public virtual string Type { get; set; }

  [PXDBString(BqlTable = typeof (APTranPost))]
  public virtual string TranClass { get; set; }

  [PXDBString(BqlTable = typeof (APTranPost))]
  public virtual string TranType { get; set; }

  [PXDBString(BqlTable = typeof (APTranPost))]
  public virtual string TranRefNbr { get; set; }

  [PXDBInt(BqlTable = typeof (APTranPost))]
  public virtual int? ReferenceID { get; set; }

  [PXDBGuid(false, BqlTable = typeof (APTranPost))]
  public virtual Guid? RefNoteID { get; set; }

  /// <summAPy>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// in migration mode without affecting GL module.
  /// </summAPy>
  [PXDBBool(BqlTable = typeof (APTranPost))]
  public virtual bool? IsMigratedRecord { get; set; }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APTranPostGL.curyInfoID), typeof (APTranPostGL.balanceAmt), BaseCalc = false)]
  [PXUIField(DisplayName = "Balance")]
  [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.application>>>>>.And<BqlOperand<APTranPost.tranClass, IBqlString>.IsEqual<APTranPost.tranClass.ADRU>>>, Mult<APTranPost.glSign, APTranPost.curyAmt>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.application>>>>>.And<BqlOperand<APTranPost.tranClass, IBqlString>.IsIn<APTranPost.tranClass.QCKN, APTranPost.tranClass.VQCN>>>, BqlFunction<PX.Data.Minus<APTranPost.glSign>, IBqlDecimal>.Multiply<APTranPost.curyAmt>, Case<Where<BqlOperand<APTranPost.type, IBqlString>.IsEqual<APTranPost.type.application>>, BqlFunction<PX.Data.Minus<APTranPost.glSign>, IBqlDecimal>.Multiply<BqlFunction<Add<APTranPost.curyDiscAmt, APTranPost.curyWhTaxAmt>, IBqlDecimal>.Add<APTranPost.curyAmt>>, Case<Where<BqlOperand<APTranPost.type, IBqlString>.IsIn<APTranPost.type.origin, APTranPost.type.adjustment, APTranPost.type.voided, APTranPost.type.installment>>, BqlOperand<APTranPost.glSign, IBqlShort>.Multiply<APTranPost.curyAmt>>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryBalanceAmt { get; set; }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.application>>>>>.And<BqlOperand<APTranPost.tranClass, IBqlString>.IsEqual<APTranPost.tranClass.ADRU>>>, Sub<Mult<APTranPost.glSign, APTranPost.amt>, APTranPost.rGOLAmt>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.application>>>>>.And<BqlOperand<APTranPost.tranClass, IBqlString>.IsIn<APTranPost.tranClass.PPMU, APTranPost.tranClass.CHKU, APTranPost.tranClass.VCKU>>>, BqlFunction<PX.Data.Minus<APTranPost.glSign>, IBqlDecimal>.Multiply<APTranPost.amt>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.application>>>>>.And<BqlOperand<APTranPost.tranClass, IBqlString>.IsIn<APTranPost.tranClass.QCKN, APTranPost.tranClass.VQCN>>>, BqlFunctionMirror<Mult<PX.Data.Minus<APTranPost.glSign>, APTranPost.amt>, IBqlDecimal>.Subtract<APTranPost.rGOLAmt>, Case<Where<BqlOperand<APTranPost.type, IBqlString>.IsEqual<APTranPost.type.application>>, BqlFunctionMirror<Mult<PX.Data.Minus<APTranPost.glSign>, BqlFunction<Add<APTranPost.discAmt, APTranPost.whTaxAmt>, IBqlDecimal>.Add<APTranPost.amt>>, IBqlDecimal>.Subtract<APTranPost.rGOLAmt>, Case<Where<BqlOperand<APTranPost.type, IBqlString>.IsIn<APTranPost.type.origin, APTranPost.type.adjustment, APTranPost.type.voided, APTranPost.type.installment>>, BqlOperand<APTranPost.glSign, IBqlShort>.Multiply<APTranPost.amt>>>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? BalanceAmt { get; set; }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APTranPostGL.curyInfoID), typeof (APTranPostGL.debitAPAmt), BaseCalc = false)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APTranPost.accountID, IBqlInt>.IsNull>, decimal0, Case<Where<BqlOperand<APTranPost.type, IBqlString>.IsEqual<APTranPost.type.voided>>, PX.Data.Minus<APTranPost.curyAmt>, Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.origin>>>>, PX.Data.And<BqlOperand<APTranPost.glSign, IBqlShort>.IsEqual<shortMinus1>>>>.And<BqlOperand<APTranPost.tranClass, IBqlString>.IsIn<APTranPost.tranClass.QCKN, APTranPost.tranClass.VQCN, APTranPost.tranClass.RQCN>>>, BqlFunction<Add<APTranPost.curyAmt, APTranPost.curyDiscAmt>, IBqlDecimal>.Add<APTranPost.curyWhTaxAmt>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.origin>>>>>.And<BqlOperand<APTranPost.glSign, IBqlShort>.IsEqual<shortMinus1>>>, APTranPost.curyAmt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.adjustment>>>>>.And<BqlOperand<APTranPost.glSign, IBqlShort>.IsEqual<shortMinus1>>>, APTranPost.curyAmt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.application>>>>>.And<BqlOperand<APTranPost.glSign, IBqlShort>.IsEqual<short1>>>, BqlFunction<Add<APTranPost.curyAmt, APTranPost.curyDiscAmt>, IBqlDecimal>.Add<APTranPost.curyWhTaxAmt>>>>>>>, decimal0>), typeof (Decimal))]
  [PXUIField(DisplayName = "Debit AP Amt.")]
  public virtual Decimal? CuryDebitAPAmt { get; set; }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APTranPost.accountID, IBqlInt>.IsNull>, decimal0, Case<Where<BqlOperand<APTranPost.type, IBqlString>.IsEqual<APTranPost.type.voided>>, PX.Data.Minus<APTranPost.amt>, Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.origin>>>>, PX.Data.And<BqlOperand<APTranPost.glSign, IBqlShort>.IsEqual<shortMinus1>>>>.And<BqlOperand<APTranPost.tranClass, IBqlString>.IsIn<APTranPost.tranClass.QCKN, APTranPost.tranClass.VQCN, APTranPost.tranClass.RQCN>>>, BqlFunctionMirror<Add<Add<APTranPost.amt, APTranPost.discAmt>, APTranPost.whTaxAmt>, IBqlDecimal>.Add<APTranPost.rGOLAmt>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.origin>>>>>.And<BqlOperand<APTranPost.glSign, IBqlShort>.IsEqual<shortMinus1>>>, APTranPost.amt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.adjustment>>>>>.And<BqlOperand<APTranPost.glSign, IBqlShort>.IsEqual<shortMinus1>>>, APTranPost.amt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.application>>>>>.And<BqlOperand<APTranPost.glSign, IBqlShort>.IsEqual<short1>>>, BqlFunctionMirror<Add<Add<APTranPost.amt, APTranPost.discAmt>, APTranPost.whTaxAmt>, IBqlDecimal>.Add<APTranPost.rGOLAmt>>>>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? DebitAPAmt { get; set; }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APTranPostGL.curyInfoID), typeof (APTranPostGL.creditAPAmt), BaseCalc = false)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APTranPost.accountID, IBqlInt>.IsNull>, decimal0, Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.origin>>>>, PX.Data.And<BqlOperand<APTranPost.glSign, IBqlShort>.IsEqual<short1>>>>.And<BqlOperand<APTranPost.tranClass, IBqlString>.IsIn<APTranPost.tranClass.QCKN, APTranPost.tranClass.VQCN, APTranPost.tranClass.RQCN>>>, BqlFunction<Add<APTranPost.curyAmt, APTranPost.curyDiscAmt>, IBqlDecimal>.Add<APTranPost.curyWhTaxAmt>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.origin>>>>>.And<BqlOperand<APTranPost.glSign, IBqlShort>.IsEqual<short1>>>, APTranPost.curyAmt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.adjustment>>>>>.And<BqlOperand<APTranPost.glSign, IBqlShort>.IsEqual<short1>>>, APTranPost.curyAmt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.application>>>>>.And<BqlOperand<APTranPost.glSign, IBqlShort>.IsEqual<shortMinus1>>>, BqlFunction<Add<APTranPost.curyAmt, APTranPost.curyDiscAmt>, IBqlDecimal>.Add<APTranPost.curyWhTaxAmt>>>>>>, decimal0>), typeof (Decimal))]
  [PXUIField(DisplayName = "Credit AP Amt.")]
  public virtual Decimal? CuryCreditAPAmt { get; set; }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APTranPost.accountID, IBqlInt>.IsNull>, decimal0, Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.origin>>>>, PX.Data.And<BqlOperand<APTranPost.glSign, IBqlShort>.IsEqual<short1>>>>.And<BqlOperand<APTranPost.tranClass, IBqlString>.IsIn<APTranPost.tranClass.QCKN, APTranPost.tranClass.VQCN, APTranPost.tranClass.RQCN>>>, BqlFunctionMirror<Add<Add<APTranPost.amt, APTranPost.discAmt>, APTranPost.whTaxAmt>, IBqlDecimal>.Subtract<APTranPost.rGOLAmt>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.origin>>>>>.And<BqlOperand<APTranPost.glSign, IBqlShort>.IsEqual<short1>>>, APTranPost.amt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.adjustment>>>>>.And<BqlOperand<APTranPost.glSign, IBqlShort>.IsEqual<short1>>>, APTranPost.amt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.application>>>>>.And<BqlOperand<APTranPost.glSign, IBqlShort>.IsEqual<shortMinus1>>>, BqlFunctionMirror<Add<Add<APTranPost.amt, APTranPost.discAmt>, APTranPost.whTaxAmt>, IBqlDecimal>.Subtract<APTranPost.rGOLAmt>>>>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CreditAPAmt { get; set; }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APTranPostGL.curyInfoID), typeof (APTranPostGL.turnDiscAmt), BaseCalc = false)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APTranPost.type, IBqlString>.IsEqual<APTranPost.type.rgol>>, BqlOperand<APTranPost.glSign, IBqlShort>.Multiply<APTranPost.curyDiscAmt>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryTurnDiscAmt { get; set; }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APTranPost.type, IBqlString>.IsEqual<APTranPost.type.rgol>>, BqlOperand<APTranPost.glSign, IBqlShort>.Multiply<APTranPost.discAmt>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? TurnDiscAmt { get; set; }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APTranPostGL.curyInfoID), typeof (APTranPostGL.turnWhTaxAmt), BaseCalc = false)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.rgol>>>>>.And<BqlOperand<APTranPost.tranClass, IBqlString>.IsNotIn<APTranPost.tranClass.REFN, APTranPost.tranClass.VRFN>>>, BqlOperand<APTranPost.glSign, IBqlShort>.Multiply<APTranPost.curyWhTaxAmt>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryTurnWHTaxAmt { get; set; }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.rgol>>>>>.And<BqlOperand<APTranPost.tranClass, IBqlString>.IsNotIn<APTranPost.tranClass.REFN, APTranPost.tranClass.VRFN>>>, BqlOperand<APTranPost.glSign, IBqlShort>.Multiply<APTranPost.whTaxAmt>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? TurnWHTaxAmt { get; set; }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APTranPostGL.curyInfoID), typeof (APTranPostGL.turnRetainageAmt), BaseCalc = false)]
  [PXDBCalced(typeof (BqlOperand<APTranPost.glSign, IBqlShort>.Multiply<APTranPost.curyRetainageAmt>), typeof (Decimal))]
  public virtual Decimal? CuryTurnRetainageAmt { get; set; }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDBCalced(typeof (BqlOperand<APTranPost.glSign, IBqlShort>.Multiply<APTranPost.retainageAmt>), typeof (Decimal))]
  public virtual Decimal? TurnRetainageAmt { get; set; }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APTranPostGL.curyInfoID), typeof (APTranPostGL.retainageReleasedAmt), BaseCalc = false)]
  [PXDBCalced(typeof (IIf<BqlOperand<APTranPost.type, IBqlString>.IsEqual<APTranPost.type.retainage>, BqlFunction<PX.Data.Minus<APTranPost.balanceSign>, IBqlDecimal>.Multiply<BqlOperand<APTranPost.glSign, IBqlShort>.Multiply<APTranPost.curyRetainageAmt>>, Zero>), typeof (Decimal))]
  public virtual Decimal? CuryRetainageReleasedAmt { get; set; }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDBCalced(typeof (IIf<BqlOperand<APTranPost.type, IBqlString>.IsEqual<APTranPost.type.retainage>, BqlFunction<PX.Data.Minus<APTranPost.balanceSign>, IBqlDecimal>.Multiply<BqlOperand<APTranPost.glSign, IBqlShort>.Multiply<APTranPost.retainageAmt>>, Zero>), typeof (Decimal))]
  public virtual Decimal? RetainageReleasedAmt { get; set; }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APTranPostGL.curyInfoID), typeof (APTranPostGL.retainageUnreleasedAmt), BaseCalc = false)]
  [PXDBCalced(typeof (BqlOperand<APTranPost.balanceSign, IBqlShort>.Multiply<BqlOperand<APTranPost.glSign, IBqlShort>.Multiply<APTranPost.curyRetainageAmt>>), typeof (Decimal))]
  public virtual Decimal? CuryRetainageUnreleasedAmt { get; set; }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDBCalced(typeof (BqlOperand<APTranPost.balanceSign, IBqlShort>.Multiply<BqlOperand<APTranPost.glSign, IBqlShort>.Multiply<APTranPost.retainageAmt>>), typeof (Decimal))]
  public virtual Decimal? RetainageUnreleasedAmt { get; set; }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APTranPost.type, IBqlString>.IsEqual<APTranPost.type.rgol>>, APTranPost.rGOLAmt>, decimal0>), typeof (Decimal))]
  public virtual Decimal? TurnRGOLAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury(BqlTable = typeof (APTranPost))]
  public virtual Decimal? RGOLAmt { get; set; }

  /// <summary>
  /// Signed amount of the document or application.
  /// Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />.
  /// </summary>
  [PX.Objects.CM.Extensions.PXCurrency(typeof (APTranPostGL.curyInfoID), typeof (APTranPostGL.turnAmt), BaseCalc = false)]
  [PXDBCalced(typeof (BqlOperand<APTranPost.glSign, IBqlShort>.Multiply<APTranPost.curyAmt>), typeof (Decimal))]
  public virtual Decimal? CuryTurnAmt { get; set; }

  /// <summary>
  /// Signed amount of the document or application.
  /// Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />
  /// </summary>
  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDBCalced(typeof (BqlOperand<APTranPost.glSign, IBqlShort>.Multiply<APTranPost.amt>), typeof (Decimal))]
  public virtual Decimal? TurnAmt { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AP.APTranPost.GLSign" />
  [PXDBShort(BqlField = typeof (APTranPost.glSign))]
  public virtual short? GLSign { get; set; }

  public class PK : 
    PrimaryKeyOf<APTranPostGL>.By<APTranPostGL.docType, APTranPostGL.refNbr, APTranPostGL.lineNbr, APTranPostGL.iD>
  {
    public static APTranPostGL Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      int? id,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APTranPostGL>.By<APTranPostGL.docType, APTranPostGL.refNbr, APTranPostGL.lineNbr, APTranPostGL.iD>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, (object) id, options);
    }
  }

  public static class FK
  {
    public class Document : 
      PrimaryKeyOf<APRegister>.By<APRegister.docType, APRegister.refNbr>.ForeignKeyOf<APTran>.By<APTranPostGL.docType, APTranPostGL.refNbr>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<APInvoice>.By<APInvoice.docType, APInvoice.refNbr>.ForeignKeyOf<APTran>.By<APTranPostGL.docType, APTranPostGL.refNbr>
    {
    }

    public class Payment : 
      PrimaryKeyOf<APPayment>.By<APPayment.docType, APPayment.refNbr>.ForeignKeyOf<APTran>.By<APTranPostGL.docType, APTranPostGL.refNbr>
    {
    }

    public class QuickCheck : 
      PrimaryKeyOf<APQuickCheck>.By<APQuickCheck.docType, APQuickCheck.refNbr>.ForeignKeyOf<APTran>.By<APTranPostGL.docType, APTranPostGL.refNbr>
    {
    }

    public class SOInvoice : 
      PrimaryKeyOf<PX.Objects.SO.SOInvoice>.By<PX.Objects.SO.SOInvoice.docType, PX.Objects.SO.SOInvoice.refNbr>.ForeignKeyOf<APTran>.By<APTranPostGL.docType, APTranPostGL.refNbr>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<APTran>.By<APTranPostGL.branchID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<APTran>.By<APTranPostGL.curyInfoID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APTran>.By<APTranPostGL.vendorID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<APTran>.By<APTranPostGL.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<APTran>.By<APTranPostGL.subID>
    {
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostGL.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostGL.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostGL.lineNbr>
  {
  }

  public abstract class iD : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostGL.iD>
  {
  }

  public abstract class sourceDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostGL.sourceDocType>
  {
  }

  public abstract class sourceRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostGL.sourceRefNbr>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APTranPostGL.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APTranPostGL.curyInfoID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostGL.branchID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostGL.vendorID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostGL.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostGL.subID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostGL.finPeriodID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostGL.tranPeriodID>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APTranPostGL.docDate>
  {
  }

  public abstract class balanceSign : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  APTranPostGL.balanceSign>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostGL.batchNbr>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostGL.type>
  {
  }

  public abstract class tranClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostGL.tranClass>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostGL.tranType>
  {
  }

  public abstract class tranRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostGL.tranRefNbr>
  {
  }

  public abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostGL.referenceID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APTranPostGL.refNoteID>
  {
  }

  public abstract class isMigratedRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APTranPostGL.isMigratedRecord>
  {
  }

  public abstract class curyBalanceAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGL.curyBalanceAmt>
  {
  }

  public abstract class balanceAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostGL.balanceAmt>
  {
  }

  public abstract class curyDebitAPAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGL.curyDebitAPAmt>
  {
  }

  public abstract class debitAPAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostGL.debitAPAmt>
  {
  }

  public abstract class curyCreditAPAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGL.curyCreditAPAmt>
  {
  }

  public abstract class creditAPAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostGL.creditAPAmt>
  {
  }

  public abstract class curyTurnDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGL.curyTurnDiscAmt>
  {
  }

  public abstract class turnDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostGL.turnDiscAmt>
  {
  }

  public abstract class curyTurnWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGL.curyTurnWhTaxAmt>
  {
  }

  public abstract class turnWhTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostGL.turnWhTaxAmt>
  {
  }

  public abstract class curyTurnRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGL.curyTurnRetainageAmt>
  {
  }

  public abstract class turnRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGL.turnRetainageAmt>
  {
  }

  public abstract class curyRetainageReleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGL.curyRetainageReleasedAmt>
  {
  }

  public abstract class retainageReleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGL.retainageReleasedAmt>
  {
  }

  public abstract class curyRetainageUnreleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGL.curyRetainageUnreleasedAmt>
  {
  }

  public abstract class retainageUnreleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostGL.retainageUnreleasedAmt>
  {
  }

  public abstract class turnRGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostGL.turnRGOLAmt>
  {
  }

  public abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostGL.rGOLAmt>
  {
  }

  public abstract class curyTurnAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostGL.curyTurnAmt>
  {
  }

  public abstract class turnAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostGL.turnAmt>
  {
  }

  public abstract class glSign : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  APTranPostGL.glSign>
  {
  }
}
