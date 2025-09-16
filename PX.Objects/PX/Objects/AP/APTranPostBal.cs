// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APTranPostBal
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP.Standalone;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXProjection(typeof (Select<APTranPost>), Persistent = false)]
[PXHidden]
public class APTranPostBal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlTable = typeof (APTranPost))]
  public virtual int? ID { get; set; }

  [PXDBString(BqlTable = typeof (APTranPost))]
  [PXUIField(DisplayName = "Doc. Type")]
  [APDocType.List]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(BqlTable = typeof (APTranPost))]
  [PXUIField(DisplayName = "Ref. Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual string RefNbr { get; set; }

  [PXDBInt(BqlTable = typeof (APTranPost))]
  [PXUIField(DisplayName = "Line Nbr.", FieldClass = "PaymentsByLines")]
  public virtual int? LineNbr { get; set; }

  [PXDBString(BqlTable = typeof (APTranPost))]
  [PXUIField(DisplayName = "Source Doc. Type")]
  [APDocType.List]
  public virtual string SourceDocType { get; set; }

  [PXDBString(BqlTable = typeof (APTranPost))]
  [PXUIField(DisplayName = "Source Ref. Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual string SourceRefNbr { get; set; }

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

  [PXDBDate(BqlField = typeof (APTranPost.docDate))]
  [PXUIField(DisplayName = "Application Date", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual System.DateTime? ApplicationDate { get; set; }

  [PXDBString(15, IsUnicode = true, BqlTable = typeof (APTranPost))]
  [PXUIField(DisplayName = "Batch Number", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PX.Objects.GL.BatchNbr(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAP>>>), IsMigratedRecordField = typeof (APTranPostBal.isMigratedRecord), BqlTable = typeof (APTranPost))]
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

  /// <summAPy>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// in migration mode without affecting GL module.
  /// </summAPy>
  [PXDBBool(BqlTable = typeof (APTranPost))]
  public virtual bool? IsMigratedRecord { get; set; }

  [APTranPostBal.refNoteID.Note(BqlTable = typeof (APTranPost))]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBDate(BqlTable = typeof (APTranPost))]
  [PXDefault(typeof (APRegister.docDate))]
  [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual System.DateTime? DocDate { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTranPostBal.curyInfoID), typeof (APTranPostBal.amt), BaseCalc = false, BqlTable = typeof (APTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? CuryAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTranPostBal.curyInfoID), typeof (APTranPostBal.ppdAmt), BaseCalc = false, BqlTable = typeof (APTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cash Discount Taken")]
  public virtual Decimal? CuryPPDAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTranPostBal.curyInfoID), typeof (APTranPostBal.discAmt), BaseCalc = false, BqlTable = typeof (APTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cash Discount Taken")]
  public virtual Decimal? CuryDiscAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTranPostBal.curyInfoID), typeof (APTranPostBal.retainageAmt), BaseCalc = false, BqlTable = typeof (APTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryRetainageAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTranPostBal.curyInfoID), typeof (APTranPostBal.whTaxAmt), BaseCalc = false, BqlTable = typeof (APTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "With. Tax")]
  public virtual Decimal? CuryWhTaxAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury(BqlTable = typeof (APTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Amt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury(BqlTable = typeof (APTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PPDAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury(BqlTable = typeof (APTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury(BqlTable = typeof (APTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainageAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury(BqlTable = typeof (APTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? WhTaxAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury(BqlTable = typeof (APTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RGOLAmt { get; set; }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APTranPostBal.curyInfoID), typeof (APTranPostBal.balanceAmt), BaseCalc = false)]
  [PXUIField(DisplayName = "Balance")]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<PX.Objects.AP.Standalone.APRegister.curyID, IBqlString>.IsEqual<CurrencyInfo2.curyID>>, Switch<Case<Where<BqlOperand<APRegister.paymentsByLinesAllowed, IBqlBool>.IsEqual<True>>, APTran.curyTranBal>, APRegister.curyDocBal>>, PX.Data.Round<Mult<Switch<Case<Where<BqlOperand<APRegister.paymentsByLinesAllowed, IBqlBool>.IsEqual<True>>, APTran.tranBal>, APRegister.docBal>, Switch<Case<Where<BqlOperand<CurrencyInfo2.curyMultDiv, IBqlString>.IsEqual<PX.Objects.CM.Extensions.CuryMultDivType.mult>>, CurrencyInfo2.recipRate>, CurrencyInfo2.curyRate>>, CurrencyInfo2.curyPrecision>>), typeof (Decimal))]
  public virtual Decimal? CuryBalanceAmt { get; set; }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<APRegister.paymentsByLinesAllowed, IBqlBool>.IsEqual<True>>, APTran.tranBal>, APRegister.docBal>), typeof (Decimal))]
  public virtual Decimal? BalanceAmt { get; set; }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APTranPostBal.curyInfoID), typeof (APTranPostBal.discBalanceAmt), BaseCalc = false)]
  [PXUIField(DisplayName = "Cash Discount Balance")]
  [PXDBCalced(typeof (PX.Data.Round<Mult<Switch<Case<Where<APRegister.paymentsByLinesAllowed, Equal<True>>, APTran.curyCashDiscBal>, PX.Objects.AP.Standalone.APRegister.curyDiscBal>, Switch<Case<Where<BqlOperand<PX.Objects.AP.Standalone.APRegister.curyID, IBqlString>.IsEqual<CurrencyInfo2.curyID>>, decimal1>, Div<PX.Objects.CM.Extensions.CurrencyInfo.curyRate, CurrencyInfo2.curyRate>>>, CurrencyInfo2.curyPrecision>), typeof (Decimal))]
  public virtual Decimal? CuryDiscBalanceAmt { get; set; }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDBCalced(typeof (PX.Objects.AP.Standalone.APRegister.discBal), typeof (Decimal))]
  public virtual Decimal? DiscBalanceAmt { get; set; }

  public class PK : PrimaryKeyOf<APTranPostBal>.By<APTranPostBal.iD>
  {
    public static APTranPostBal Find(PXGraph graph, int? id, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APTranPostBal>.By<APTranPostBal.iD>.FindBy(graph, (object) id, options);
    }
  }

  public static class FK
  {
    public class Document : 
      PrimaryKeyOf<APRegister>.By<APRegister.docType, APRegister.refNbr>.ForeignKeyOf<APTran>.By<APTranPostBal.docType, APTranPostBal.refNbr>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<APInvoice>.By<APInvoice.docType, APInvoice.refNbr>.ForeignKeyOf<APTran>.By<APTranPostBal.docType, APTranPostBal.refNbr>
    {
    }

    public class Payment : 
      PrimaryKeyOf<APPayment>.By<APPayment.docType, APPayment.refNbr>.ForeignKeyOf<APTran>.By<APTranPostBal.docType, APTranPostBal.refNbr>
    {
    }

    public class QuickCheck : 
      PrimaryKeyOf<APQuickCheck>.By<APQuickCheck.docType, APQuickCheck.refNbr>.ForeignKeyOf<APTran>.By<APTranPostBal.docType, APTranPostBal.refNbr>
    {
    }

    public class SOInvoice : 
      PrimaryKeyOf<PX.Objects.SO.SOInvoice>.By<PX.Objects.SO.SOInvoice.docType, PX.Objects.SO.SOInvoice.refNbr>.ForeignKeyOf<APTran>.By<APTranPostBal.docType, APTranPostBal.refNbr>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<APTran>.By<APTranPostBal.branchID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<APTran>.By<APTranPostBal.curyInfoID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APTran>.By<APTranPostBal.vendorID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<APTran>.By<APTranPostBal.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<APTran>.By<APTranPostBal.subID>
    {
    }
  }

  public abstract class iD : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostBal.iD>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostBal.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostBal.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostBal.lineNbr>
  {
  }

  public abstract class sourceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranPostBal.sourceDocType>
  {
  }

  public abstract class sourceRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostBal.sourceRefNbr>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APTranPostBal.curyInfoID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostBal.branchID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostBal.vendorID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostBal.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranPostBal.subID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostBal.finPeriodID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostBal.tranPeriodID>
  {
  }

  public abstract class applicationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APTranPostBal.applicationDate>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostBal.batchNbr>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostBal.type>
  {
  }

  public abstract class tranClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostBal.tranClass>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostBal.tranType>
  {
  }

  public abstract class tranRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranPostBal.tranRefNbr>
  {
  }

  public abstract class isMigratedRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APTranPostBal.isMigratedRecord>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APTranPostBal.refNoteID>
  {
    public class NoteAttribute : PXNoteAttribute
    {
      public NoteAttribute() => this.BqlTable = typeof (APAdjust);

      protected override bool IsVirtualTable(System.Type table) => false;
    }
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APTranPostBal.docDate>
  {
  }

  public abstract class curyAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostBal.curyAmt>
  {
  }

  public abstract class curyPPDAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostBal.curyPPDAmt>
  {
  }

  public abstract class curyDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostBal.curyDiscAmt>
  {
  }

  public abstract class curyRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostBal.curyRetainageAmt>
  {
  }

  public abstract class curyWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostBal.curyWhTaxAmt>
  {
  }

  public abstract class amt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostBal.amt>
  {
  }

  public abstract class ppdAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostBal.ppdAmt>
  {
  }

  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostBal.discAmt>
  {
  }

  public abstract class retainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostBal.retainageAmt>
  {
  }

  public abstract class whTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostBal.whTaxAmt>
  {
  }

  public abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostBal.rGOLAmt>
  {
  }

  public abstract class curyBalanceAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostBal.curyBalanceAmt>
  {
  }

  public abstract class balanceAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTranPostBal.balanceAmt>
  {
  }

  public abstract class curyDiscBalanceAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostBal.curyDiscBalanceAmt>
  {
  }

  public abstract class discBalanceAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranPostBal.discBalanceAmt>
  {
  }
}
