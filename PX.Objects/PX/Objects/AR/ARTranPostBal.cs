// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTranPostBal
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR.Standalone;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select<ARTranPost>), Persistent = false)]
[PXHidden]
public class ARTranPostBal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlTable = typeof (ARTranPost))]
  public virtual int? ID { get; set; }

  [PXDBString(BqlTable = typeof (ARTranPost))]
  [PXUIField(DisplayName = "Doc. Type")]
  [ARDocType.List]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(BqlTable = typeof (ARTranPost))]
  [PXUIField]
  public virtual string RefNbr { get; set; }

  [PXDBInt(BqlTable = typeof (ARTranPost))]
  [PXUIField(DisplayName = "Line Nbr.", FieldClass = "PaymentsByLines")]
  public virtual int? LineNbr { get; set; }

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

  [PXDBDate(BqlField = typeof (ARTranPost.docDate))]
  [PXUIField]
  public virtual DateTime? ApplicationDate { get; set; }

  [PXDBShort(BqlTable = typeof (ARTranPost))]
  public virtual short? BalanceSign { get; set; }

  [PXDBString(15, IsUnicode = true, BqlTable = typeof (ARTranPost))]
  [PXUIField]
  [PX.Objects.GL.BatchNbr(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAR>>>), IsMigratedRecordField = typeof (ARTranPostBal.isMigratedRecord), BqlTable = typeof (ARTranPost))]
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

  [ARTranPostBal.refNoteID.Note(BqlTable = typeof (ARTranPost))]
  public virtual Guid? RefNoteID { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// in migration mode without affecting GL module.
  /// </summary>
  [PXDBBool(BqlTable = typeof (ARTranPost))]
  public virtual bool? IsMigratedRecord { get; set; }

  [PXDBCurrency(typeof (ARTranPostBal.curyInfoID), typeof (ARTranPostBal.amt), BaseCalc = false, BqlTable = typeof (ARTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? CuryAmt { get; set; }

  [PXDBCurrency(typeof (ARTranPostBal.curyInfoID), typeof (ARTranPostBal.ppdAmt), BaseCalc = false, BqlTable = typeof (ARTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cash Discount Taken")]
  public virtual Decimal? CuryPPDAmt { get; set; }

  [PXDBCurrency(typeof (ARTranPostBal.curyInfoID), typeof (ARTranPostBal.discAmt), BaseCalc = false, BqlTable = typeof (ARTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cash Discount Taken")]
  public virtual Decimal? CuryDiscAmt { get; set; }

  [PXDBCurrency(typeof (ARTranPostBal.curyInfoID), typeof (ARTranPostBal.retainageAmt), BaseCalc = false, BqlTable = typeof (ARTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryRetainageAmt { get; set; }

  [PXDBCurrency(typeof (ARTranPostBal.curyInfoID), typeof (ARTranPostBal.wOAmt), BaseCalc = false, BqlTable = typeof (ARTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Write-Off Amount")]
  public virtual Decimal? CuryWOAmt { get; set; }

  [PXDBCurrency(typeof (ARTranPostBal.curyInfoID), typeof (ARTranPostBal.itemDiscAmt), BaseCalc = false, BqlTable = typeof (ARTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryItemDiscAmt { get; set; }

  [PXDBBaseCury(BqlTable = typeof (ARTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Amt { get; set; }

  [PXDBBaseCury(BqlTable = typeof (ARTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PPDAmt { get; set; }

  [PXDBBaseCury(BqlTable = typeof (ARTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAmt { get; set; }

  [PXDBBaseCury(BqlTable = typeof (ARTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainageAmt { get; set; }

  [PXDBBaseCury(BqlTable = typeof (ARTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? WOAmt { get; set; }

  [PXDBBaseCury(BqlTable = typeof (ARTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ItemDiscAmt { get; set; }

  [PXDBBaseCury(BqlTable = typeof (ARTranPost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RGOLAmt { get; set; }

  [PXCurrency(typeof (ARTranPostBal.curyInfoID), typeof (ARTranPostBal.balanceAmt), BaseCalc = false)]
  [PXUIField(DisplayName = "Balance")]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARRegisterAlias.curyID, IBqlString>.IsEqual<CurrencyInfo2.curyID>>, Switch<Case<Where<BqlOperand<ARRegisterAlias.paymentsByLinesAllowed, IBqlBool>.IsEqual<True>>, ARTran.curyTranBal>, ARRegisterAlias.curyDocBal>>, Round<Mult<Switch<Case<Where<BqlOperand<ARRegisterAlias.paymentsByLinesAllowed, IBqlBool>.IsEqual<True>>, ARTran.tranBal>, ARRegisterAlias.docBal>, Switch<Case<Where<BqlOperand<CurrencyInfo2.curyMultDiv, IBqlString>.IsEqual<PX.Objects.CM.Extensions.CuryMultDivType.mult>>, CurrencyInfo2.recipRate>, CurrencyInfo2.curyRate>>, CurrencyInfo2.curyPrecision>>), typeof (Decimal))]
  public virtual Decimal? CuryBalanceAmt { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARRegisterAlias.paymentsByLinesAllowed, IBqlBool>.IsEqual<True>>, ARTran.tranBal>, ARRegisterAlias.docBal>), typeof (Decimal))]
  public virtual Decimal? BalanceAmt { get; set; }

  [PXCurrency(typeof (ARTranPostBal.curyInfoID), typeof (ARTranPostBal.discBalanceAmt), BaseCalc = false)]
  [PXUIField(DisplayName = "Cash Discount Balance")]
  [PXDBCalced(typeof (Round<Mult<Switch<Case<Where<ARRegisterAlias.paymentsByLinesAllowed, Equal<True>>, ARTran.curyCashDiscBal>, ARRegisterAlias.curyDiscBal>, Switch<Case<Where<BqlOperand<ARRegisterAlias.curyID, IBqlString>.IsEqual<CurrencyInfo2.curyID>>, decimal1>, Div<PX.Objects.CM.Extensions.CurrencyInfo.curyRate, CurrencyInfo2.curyRate>>>, CurrencyInfo2.curyPrecision>), typeof (Decimal))]
  public virtual Decimal? CuryDiscBalanceAmt { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<ARRegisterAlias.paymentsByLinesAllowed, Equal<True>>, ARTran.curyCashDiscBal>, ARRegisterAlias.discBal>), typeof (Decimal))]
  public virtual Decimal? DiscBalanceAmt { get; set; }

  public class PK : PrimaryKeyOf<ARTranPostBal>.By<ARTranPostBal.iD>
  {
    public static ARTranPostBal Find(PXGraph graph, int? id, PKFindOptions options = 0)
    {
      return (ARTranPostBal) PrimaryKeyOf<ARTranPostBal>.By<ARTranPostBal.iD>.FindBy(graph, (object) id, options);
    }
  }

  public static class FK
  {
    public class Document : 
      PrimaryKeyOf<ARRegister>.By<ARRegister.docType, ARRegister.refNbr>.ForeignKeyOf<ARTran>.By<ARTranPostBal.docType, ARTranPostBal.refNbr>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<ARInvoice>.By<ARInvoice.docType, ARInvoice.refNbr>.ForeignKeyOf<ARTran>.By<ARTranPostBal.docType, ARTranPostBal.refNbr>
    {
    }

    public class Payment : 
      PrimaryKeyOf<ARPayment>.By<ARPayment.docType, ARPayment.refNbr>.ForeignKeyOf<ARTran>.By<ARTranPostBal.docType, ARTranPostBal.refNbr>
    {
    }

    public class CashSale : 
      PrimaryKeyOf<ARCashSale>.By<ARCashSale.docType, ARCashSale.refNbr>.ForeignKeyOf<ARTran>.By<ARTranPostBal.docType, ARTranPostBal.refNbr>
    {
    }

    public class SOInvoice : 
      PrimaryKeyOf<PX.Objects.SO.SOInvoice>.By<PX.Objects.SO.SOInvoice.docType, PX.Objects.SO.SOInvoice.refNbr>.ForeignKeyOf<ARTran>.By<ARTranPostBal.docType, ARTranPostBal.refNbr>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARTran>.By<ARTranPostBal.branchID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARTran>.By<ARTranPostBal.curyInfoID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARTran>.By<ARTranPostBal.customerID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ARTran>.By<ARTranPostBal.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ARTran>.By<ARTranPostBal.subID>
    {
    }
  }

  public abstract class iD : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPostBal.iD>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostBal.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostBal.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPostBal.lineNbr>
  {
  }

  public abstract class sourceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTranPostBal.sourceDocType>
  {
  }

  public abstract class sourceRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostBal.sourceRefNbr>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARTranPostBal.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARTranPostBal.curyInfoID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPostBal.branchID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPostBal.customerID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPostBal.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPostBal.subID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostBal.finPeriodID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostBal.tranPeriodID>
  {
  }

  public abstract class applicationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARTranPostBal.applicationDate>
  {
  }

  public abstract class balanceSign : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARTranPostBal.balanceSign>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostBal.batchNbr>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostBal.type>
  {
  }

  public abstract class tranClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostBal.tranClass>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostBal.tranType>
  {
  }

  public abstract class tranRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranPostBal.tranRefNbr>
  {
  }

  public abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranPostBal.referenceID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARTranPostBal.refNoteID>
  {
    public class NoteAttribute : PXNoteAttribute
    {
      public NoteAttribute() => ((PXEventSubscriberAttribute) this).BqlTable = typeof (ARAdjust);

      protected virtual bool IsVirtualTable(System.Type table) => false;
    }
  }

  public abstract class isMigratedRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARTranPostBal.isMigratedRecord>
  {
  }

  public abstract class curyAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostBal.curyAmt>
  {
  }

  public abstract class curyPPDAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostBal.curyPPDAmt>
  {
  }

  public abstract class curyDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostBal.curyDiscAmt>
  {
  }

  public abstract class curyRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostBal.curyRetainageAmt>
  {
  }

  public abstract class curyWOAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostBal.curyWOAmt>
  {
  }

  public abstract class curyItemDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostBal.curyItemDiscAmt>
  {
  }

  public abstract class amt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostBal.amt>
  {
  }

  public abstract class ppdAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostBal.ppdAmt>
  {
  }

  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostBal.discAmt>
  {
  }

  public abstract class retainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostBal.retainageAmt>
  {
  }

  public abstract class wOAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostBal.wOAmt>
  {
  }

  public abstract class itemDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostBal.itemDiscAmt>
  {
  }

  public abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostBal.rGOLAmt>
  {
  }

  public abstract class curyBalanceAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostBal.curyBalanceAmt>
  {
  }

  public abstract class balanceAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranPostBal.balanceAmt>
  {
  }

  public abstract class curyDiscBalanceAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostBal.curyDiscBalanceAmt>
  {
  }

  public abstract class discBalanceAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranPostBal.discBalanceAmt>
  {
  }
}
