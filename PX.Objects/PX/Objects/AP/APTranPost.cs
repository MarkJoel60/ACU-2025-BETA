// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APTranPost
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

[PXCacheName("AP Document transaction")]
public class APTranPost : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Branch(typeof (APRegister.branchID), null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [PXDBString(IsKey = true)]
  [PXUIField(DisplayName = "Doc. Type")]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(IsKey = true)]
  [PXParent(typeof (Select<APRegister, Where<APRegister.docType, Equal<Current<APTranPost.docType>>, And<APRegister.refNbr, Equal<Current<APTranPost.refNbr>>>>>))]
  [PXUIField(DisplayName = "Ref. Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual string RefNbr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Line Nbr.", Visibility = PXUIVisibility.Visible, FieldClass = "PaymentsByLines")]
  public virtual int? LineNbr { get; set; }

  [PXDBIdentity(IsKey = true)]
  public virtual int? ID { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBString(3, IsFixed = true)]
  [APDocType.List]
  [PXUIField(DisplayName = "Source Doc. Type")]
  public virtual string SourceDocType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Source Ref. Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual string SourceRefNbr { get; set; }

  [PXDBDate]
  [PXDefault(typeof (APRegister.docDate))]
  [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual System.DateTime? DocDate { get; set; }

  [Vendor]
  [PXDefault]
  public virtual int? VendorID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, typeof (APTranPost.branchID), null, null, null, null, true, false, null, typeof (APTranPost.tranPeriodID), null, true, true)]
  [PXUIField(DisplayName = "Application Period")]
  public virtual string FinPeriodID { get; set; }

  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  [PXDBLong]
  [PX.Objects.CM.Extensions.CurrencyInfo(typeof (APRegister.curyInfoID), Required = true)]
  public virtual long? CuryInfoID { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Batch Number", Visibility = PXUIVisibility.Visible, Visible = true, Enabled = false)]
  [PX.Objects.GL.BatchNbr(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAP>>>), IsMigratedRecordField = typeof (APTranPost.isMigratedRecord))]
  public virtual string BatchNbr { get; set; }

  [Account(typeof (APTranPost.branchID), DisplayName = "Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description))]
  public virtual int? AccountID { get; set; }

  [SubAccount(typeof (APTranPost.accountID), typeof (APTranPost.branchID), true, DisplayName = "Subaccount", Visibility = PXUIVisibility.Visible)]
  public virtual int? SubID { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTranPost.curyInfoID), typeof (APTranPost.amt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? CuryAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTranPost.curyInfoID), typeof (APTranPost.ppdAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cash Discount Taken")]
  public virtual Decimal? CuryPPDAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTranPost.curyInfoID), typeof (APTranPost.discAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cash Discount Taken")]
  public virtual Decimal? CuryDiscAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTranPost.curyInfoID), typeof (APTranPost.retainageAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryRetainageAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTranPost.curyInfoID), typeof (APTranPost.whTaxAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "With. Tax")]
  public virtual Decimal? CuryWhTaxAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Amt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PPDAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainageAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? WhTaxAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RGOLAmt { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// in migration mode without affecting GL module.
  /// </summary>
  [PXDBBool]
  public virtual bool? IsMigratedRecord { get; set; }

  [PXDBString]
  [APTranPost.type.List]
  [PXUIField(DisplayName = "Transaction type")]
  public virtual string Type { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Tran. Type")]
  public virtual string TranType { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Tran. Ref. Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual string TranRefNbr { get; set; }

  [PXDBShort]
  [PXFormula(typeof (Switch<Case<Where<BqlOperand<APTranPost.docType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj, APDocType.quickCheck>>, short1>, shortMinus1>))]
  public virtual short? BalanceSign { get; set; }

  [PXDBShort]
  [PXFormula(typeof (Switch<Case<Where<BqlOperand<APTranPost.type, IBqlString>.IsEqual<APTranPost.type.voided>>, short1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.retainageReverse>>>>>.And<BqlOperand<APTranPost.docType, IBqlString>.IsIn<APDocType.invoice, APDocType.creditAdj>>>, shortMinus1, Case<Where<BqlOperand<APTranPost.type, IBqlString>.IsEqual<APTranPost.type.retainageReverse>>, short1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.retainage>>>>>.And<BqlOperand<APTranPost.tranType, IBqlString>.IsEqual<APDocType.debitAdj>>>, shortMinus1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.retainage>>>>>.And<BqlOperand<APTranPost.tranType, IBqlString>.IsIn<APDocType.invoice, APDocType.creditAdj>>>, short1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.application>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.docType, Equal<APDocType.prepayment>>>>>.And<BqlOperand<APTranPost.sourceDocType, IBqlString>.IsIn<APDocType.prepayment, APDocType.check, APDocType.voidCheck>>>>, short1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.adjustment>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.sourceDocType, Equal<APDocType.prepayment>>>>>.And<BqlOperand<APTranPost.docType, IBqlString>.IsIn<APDocType.prepayment, APDocType.check, APDocType.voidCheck>>>>, short1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, NotEqual<APTranPost.type.adjustment>>>>>.And<BqlOperand<APTranPost.docType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj, APDocType.quickCheck, APDocType.prepaymentInvoice>>>, short1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.adjustment>>>>>.And<BqlOperand<APTranPost.sourceDocType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj, APDocType.quickCheck, APDocType.prepaymentInvoice>>>, short1>>>>>>>>>, shortMinus1>))]
  public virtual short? GLSign { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// in migration mode without affecting GL module.
  /// </summary>
  [PXBool]
  public virtual bool? IsVoidPrepayment { get; set; }

  [PXDBString]
  [PXFormula(typeof (Switch<Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.application>>>>, PX.Data.And<BqlOperand<APTranPost.isMigratedRecord, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<APTranPost.docType, IBqlString>.IsIn<APDocType.invoice, APDocType.debitAdj, APDocType.creditAdj, APDocType.quickCheck, APDocType.voidQuickCheck>>>, BqlOperand<APDocType.debitAdj, IBqlString>.Concat<PX.Objects.GL.GLTran.tranClass.normal>, Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.application>>>>, PX.Data.And<BqlOperand<APTranPost.isMigratedRecord, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<APTranPost.docType, IBqlString>.IsIn<APDocType.check, APDocType.voidCheck, APDocType.refund, APDocType.voidRefund>>>, BqlOperand<APDocType.debitAdj, IBqlString>.Concat<PX.Objects.GL.GLTran.tranClass.payment>, Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, Equal<APTranPost.type.application>>>>, PX.Data.And<BqlOperand<APTranPost.isMigratedRecord, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<APTranPost.docType, IBqlString>.IsEqual<APDocType.prepayment>>>, BqlOperand<APDocType.debitAdj, IBqlString>.Concat<PX.Objects.GL.GLTran.tranClass.charge>, Case<Where<BqlOperand<APTranPost.type, IBqlString>.IsEqual<APTranPost.type.rgol>>, BqlOperand<APTranPost.sourceDocType, IBqlString>.Concat<PX.Objects.GL.GLTran.tranClass.rgol>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, In3<APTranPost.type.application, APTranPost.type.rgol>>>>>.And<BqlOperand<APTranPost.docType, IBqlString>.IsIn<APDocType.invoice, APDocType.debitAdj, APDocType.creditAdj, APDocType.quickCheck, APDocType.voidQuickCheck>>>, BqlOperand<APTranPost.sourceDocType, IBqlString>.Concat<PX.Objects.GL.GLTran.tranClass.normal>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, In3<APTranPost.type.application, APTranPost.type.rgol>>>>>.And<BqlOperand<APTranPost.docType, IBqlString>.IsIn<APDocType.check, APDocType.voidCheck, APDocType.refund, APDocType.voidRefund>>>, BqlOperand<APTranPost.sourceDocType, IBqlString>.Concat<PX.Objects.GL.GLTran.tranClass.payment>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, In3<APTranPost.type.application, APTranPost.type.rgol>>>>>.And<BqlOperand<APTranPost.docType, IBqlString>.IsEqual<APDocType.prepayment>>>, BqlOperand<APTranPost.sourceDocType, IBqlString>.Concat<PX.Objects.GL.GLTran.tranClass.charge>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.type, In3<APTranPost.type.origin, APTranPost.type.voided, APTranPost.type.adjustment>>>>, PX.Data.And<BqlOperand<APTranPost.isMigratedRecord, IBqlBool>.IsEqual<False>>>, PX.Data.And<BqlOperand<APTranPost.docType, IBqlString>.IsEqual<APDocType.voidCheck>>>>.And<BqlOperand<APTranPost.isVoidPrepayment, IBqlBool>.IsEqual<True>>>, BqlOperand<APTranPost.docType, IBqlString>.Concat<PX.Objects.GL.GLTran.tranClass.charge>, Case<Where<BqlOperand<APTranPost.type, IBqlString>.IsEqual<APTranPost.type.adjustment>>, BqlOperand<APTranPost.docType, IBqlString>.Concat<PX.Objects.GL.GLTran.tranClass.payment>, Case<Where<BqlOperand<APTranPost.type, IBqlString>.IsEqual<APTranPost.type.rgol>>, BqlOperand<APTranPost.docType, IBqlString>.Concat<PX.Objects.GL.GLTran.tranClass.rgol>, Case<Where<BqlOperand<APTranPost.docType, IBqlString>.IsIn<APDocType.invoice, APDocType.debitAdj, APDocType.creditAdj, APDocType.quickCheck, APDocType.voidQuickCheck, APDocType.cashReturn, APDocType.prepaymentInvoice>>, BqlOperand<APTranPost.docType, IBqlString>.Concat<PX.Objects.GL.GLTran.tranClass.normal>, Case<Where<BqlOperand<APTranPost.docType, IBqlString>.IsIn<APDocType.check, APDocType.voidCheck, APDocType.refund, APDocType.voidRefund>>, BqlOperand<APTranPost.docType, IBqlString>.Concat<PX.Objects.GL.GLTran.tranClass.payment>, Case<Where<BqlOperand<APTranPost.docType, IBqlString>.IsEqual<APDocType.prepayment>>, BqlOperand<APTranPost.docType, IBqlString>.Concat<PX.Objects.GL.GLTran.tranClass.charge>>>>>>>>>>>>>>>))]
  public virtual string TranClass { get; set; }

  public class PK : 
    PrimaryKeyOf<APTranPost>.By<APTranPost.docType, APTranPost.refNbr, APTranPost.lineNbr, APTranPost.iD>
  {
    public static APTranPost Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      int? id,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APTranPost>.By<APTranPost.docType, APTranPost.refNbr, APTranPost.lineNbr, APTranPost.iD>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, (object) id, options);
    }
  }

  public static class FK
  {
    public class Document : 
      PrimaryKeyOf<APRegister>.By<APRegister.docType, APRegister.refNbr>.ForeignKeyOf<APTran>.By<APTranPost.docType, APTranPost.refNbr>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<APInvoice>.By<APInvoice.docType, APInvoice.refNbr>.ForeignKeyOf<APTran>.By<APTranPost.docType, APTranPost.refNbr>
    {
    }

    public class Payment : 
      PrimaryKeyOf<APPayment>.By<APPayment.docType, APPayment.refNbr>.ForeignKeyOf<APTran>.By<APTranPost.docType, APTranPost.refNbr>
    {
    }

    public class QuickCheck : 
      PrimaryKeyOf<APQuickCheck>.By<APQuickCheck.docType, APQuickCheck.refNbr>.ForeignKeyOf<APTran>.By<APTranPost.docType, APTranPost.refNbr>
    {
    }

    public class SOInvoice : 
      PrimaryKeyOf<PX.Objects.SO.SOInvoice>.By<PX.Objects.SO.SOInvoice.docType, PX.Objects.SO.SOInvoice.refNbr>.ForeignKeyOf<APTran>.By<APTranPost.docType, APTranPost.refNbr>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<APTran>.By<APTranPost.branchID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<APTran>.By<APTranPost.curyInfoID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APTran>.By<APTranPost.vendorID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<APTran>.By<APTranPost.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<APTran>.By<APTranPost.subID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPost.branchID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPost.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPost.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPost.lineNbr>
  {
  }

  public abstract class iD : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPost.iD>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APTranPost.refNoteID>
  {
  }

  public abstract class sourceDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPost.sourceDocType>
  {
  }

  public abstract class sourceRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPost.sourceRefNbr>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APTranPost.docDate>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPost.vendorID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPost.finPeriodID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPost.tranPeriodID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APTranPost.curyInfoID>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPost.batchNbr>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPost.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPost.subID>
  {
  }

  public abstract class curyAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPost.curyAmt>
  {
  }

  public abstract class curyPPDAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPost.curyPPDAmt>
  {
  }

  public abstract class curyDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPost.curyDiscAmt>
  {
  }

  public abstract class curyRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPost.curyRetainageAmt>
  {
  }

  public abstract class curyWhTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPost.curyWhTaxAmt>
  {
  }

  public abstract class amt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPost.amt>
  {
  }

  public abstract class ppdAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPost.ppdAmt>
  {
  }

  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPost.discAmt>
  {
  }

  public abstract class retainageAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPost.retainageAmt>
  {
  }

  public abstract class whTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPost.whTaxAmt>
  {
  }

  public abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPost.rGOLAmt>
  {
  }

  public abstract class isMigratedRecord : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTranPost.isMigratedRecord>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPost.type>
  {
    public const string Origin = "S";
    public const string Application = "D";
    public const string Adjustment = "G";
    public const string Retainage = "F";
    public const string RetainageReverse = "U";
    public const string Installment = "I";
    public const string Voided = "V";
    public const string RGOL = "R";

    public class origin : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.type.origin>
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
    APTranPost.type.application>
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
    APTranPost.type.adjustment>
    {
      public adjustment()
        : base("G")
      {
      }
    }

    public class rgol : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.type.rgol>
    {
      public rgol()
        : base("R")
      {
      }
    }

    public class retainage : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.type.retainage>
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
      APTranPost.type.retainageReverse>
    {
      public retainageReverse()
        : base("U")
      {
      }
    }

    public class voided : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.type.voided>
    {
      public voided()
        : base("V")
      {
      }
    }

    public class installment : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.type.installment>
    {
      public installment()
        : base("I")
      {
      }
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[6]{ "S", "D", "G", "F", "U", "V" }, new string[6]
        {
          "Original document",
          "Application",
          "Adjustment",
          "Retainage Release",
          "Reverse Retainage",
          "Voided"
        })
      {
      }
    }
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPost.tranType>
  {
  }

  public abstract class tranRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPost.tranRefNbr>
  {
  }

  public abstract class balanceSign : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  APTranPost.balanceSign>
  {
  }

  public abstract class glSign : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  APTranPost.glSign>
  {
  }

  public abstract class isVoidPrepayment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTranPost.isVoidPrepayment>
  {
  }

  public abstract class tranClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPost.tranClass>
  {
    public class ACRN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.ADRN>
    {
      public ACRN()
        : base(nameof (ACRN))
      {
      }
    }

    public class ADRN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.ADRN>
    {
      public ADRN()
        : base(nameof (ADRN))
      {
      }
    }

    public class ADRP : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.ADRP>
    {
      public ADRP()
        : base(nameof (ADRP))
      {
      }
    }

    public class ADRR : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.ADRR>
    {
      public ADRR()
        : base(nameof (ADRR))
      {
      }
    }

    public class ADRU : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.ADRU>
    {
      public ADRU()
        : base(nameof (ADRU))
      {
      }
    }

    public class INVN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.INVN>
    {
      public INVN()
        : base(nameof (INVN))
      {
      }
    }

    public class PPMB : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.PPMB>
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
    APTranPost.tranClass.PPMN>
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
    APTranPost.tranClass.PPMP>
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
    APTranPost.tranClass.PPMR>
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
    APTranPost.tranClass.PPMU>
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
    APTranPost.tranClass.REFN>
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
    APTranPost.tranClass.REFP>
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
    APTranPost.tranClass.REFU>
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
    APTranPost.tranClass.REFR>
    {
      public REFR()
        : base(nameof (REFR))
      {
      }
    }

    public class VRFN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.VRFN>
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
    APTranPost.tranClass.VRFP>
    {
      public VRFP()
        : base(nameof (VRFP))
      {
      }
    }

    public class VRFU : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.VRFU>
    {
      public VRFU()
        : base(nameof (VRFU))
      {
      }
    }

    public class VRFR : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.VRFR>
    {
      public VRFR()
        : base(nameof (VRFR))
      {
      }
    }

    public class VCKN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.VCKN>
    {
      public VCKN()
        : base(nameof (VCKN))
      {
      }
    }

    public class VCKP : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.VCKP>
    {
      public VCKP()
        : base(nameof (VCKP))
      {
      }
    }

    public class VCKR : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.VCKR>
    {
      public VCKR()
        : base(nameof (VCKR))
      {
      }
    }

    public class VCKU : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.VCKU>
    {
      public VCKU()
        : base(nameof (VCKU))
      {
      }
    }

    public class CHKN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.CHKN>
    {
      public CHKN()
        : base(nameof (CHKN))
      {
      }
    }

    public class CHKP : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.CHKP>
    {
      public CHKP()
        : base(nameof (CHKP))
      {
      }
    }

    public class CHKU : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.CHKU>
    {
      public CHKU()
        : base(nameof (CHKU))
      {
      }
    }

    public class CHKR : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.CHKR>
    {
      public CHKR()
        : base(nameof (CHKR))
      {
      }
    }

    public class QCKN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.QCKN>
    {
      public QCKN()
        : base(nameof (QCKN))
      {
      }
    }

    public class QCKR : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.QCKR>
    {
      public QCKR()
        : base(nameof (QCKR))
      {
      }
    }

    public class VQCN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.VQCN>
    {
      public VQCN()
        : base(nameof (VQCN))
      {
      }
    }

    public class VQCR : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.VQCR>
    {
      public VQCR()
        : base(nameof (VQCR))
      {
      }
    }

    public class RQCN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.RQCN>
    {
      public RQCN()
        : base(nameof (RQCN))
      {
      }
    }

    public class RQCR : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APTranPost.tranClass.RQCR>
    {
      public RQCR()
        : base(nameof (RQCR))
      {
      }
    }
  }
}
