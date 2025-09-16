// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Standalone.APPayment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.AP.Standalone;

[PXHidden(ServiceVisible = false)]
[Serializable]
public class APPayment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DocType;
  protected string _RefNbr;
  protected int? _RemitAddressID;
  protected int? _RemitContactID;
  protected string _PaymentMethodID;
  protected int? _CashAccountID;
  protected string _ExtRefNbr;
  protected System.DateTime? _AdjDate;
  protected string _AdjFinPeriodID;
  protected string _AdjTranPeriodID;
  protected int? _StubCntr;
  protected int? _BillCntr;
  protected int? _ChargeCntr;
  protected bool? _Cleared;
  protected System.DateTime? _ClearDate;
  protected long? _CATranID;
  protected bool? _DepositAsBatch;
  protected System.DateTime? _DepositAfter;
  protected bool? _Deposited;
  protected System.DateTime? _DepositDate;
  protected string _DepositType;
  protected string _DepositNbr;

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt]
  public virtual int? RemitAddressID
  {
    get => this._RemitAddressID;
    set => this._RemitAddressID = value;
  }

  [PXDBInt]
  public virtual int? RemitContactID
  {
    get => this._RemitContactID;
    set => this._RemitContactID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  public virtual string PaymentMethodID
  {
    get => this._PaymentMethodID;
    set => this._PaymentMethodID = value;
  }

  [PXDBInt]
  public virtual int? CashAccountID
  {
    get => this._CashAccountID;
    set => this._CashAccountID = value;
  }

  [PXDBString(40, IsUnicode = true)]
  public virtual string ExtRefNbr
  {
    get => this._ExtRefNbr;
    set => this._ExtRefNbr = value;
  }

  [PXDBDate]
  [PXDefault]
  public virtual System.DateTime? AdjDate
  {
    get => this._AdjDate;
    set => this._AdjDate = value;
  }

  [PXDBString(6, IsFixed = true)]
  public virtual string AdjFinPeriodID
  {
    get => this._AdjFinPeriodID;
    set => this._AdjFinPeriodID = value;
  }

  [PXDBString(6, IsFixed = true)]
  public virtual string AdjTranPeriodID
  {
    get => this._AdjTranPeriodID;
    set => this._AdjTranPeriodID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? StubCntr
  {
    get => this._StubCntr;
    set => this._StubCntr = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? BillCntr
  {
    get => this._BillCntr;
    set => this._BillCntr = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ChargeCntr
  {
    get => this._ChargeCntr;
    set => this._ChargeCntr = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Cleared
  {
    get => this._Cleared;
    set => this._Cleared = value;
  }

  [PXDBDate]
  public virtual System.DateTime? ClearDate
  {
    get => this._ClearDate;
    set => this._ClearDate = value;
  }

  [PXDBLong]
  public virtual long? CATranID
  {
    get => this._CATranID;
    set => this._CATranID = value;
  }

  [PXDBBool]
  [PXDefault(typeof (Search<PX.Objects.CA.PaymentMethod.printOrExport, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Current<APQuickCheck.paymentMethodID>>>>))]
  public virtual bool? PrintCheck { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal]
  public virtual Decimal? CuryOrigTaxDiscAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal]
  public virtual Decimal? OrigTaxDiscAmt { get; set; }

  [PXDBBool]
  public virtual bool? DepositAsBatch
  {
    get => this._DepositAsBatch;
    set => this._DepositAsBatch = value;
  }

  [PXDBDate]
  public virtual System.DateTime? DepositAfter
  {
    get => this._DepositAfter;
    set => this._DepositAfter = value;
  }

  [PXDBBool]
  public virtual bool? Deposited
  {
    get => this._Deposited;
    set => this._Deposited = value;
  }

  [PXDate]
  public virtual System.DateTime? DepositDate
  {
    get => this._DepositDate;
    set => this._DepositDate = value;
  }

  [PXDBString(3, IsFixed = true)]
  public virtual string DepositType
  {
    get => this._DepositType;
    set => this._DepositType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  public virtual string DepositNbr
  {
    get => this._DepositNbr;
    set => this._DepositNbr = value;
  }

  /// <summary>External payment ID</summary>
  [PXDBString(50)]
  [PXUIField(DisplayName = "External Payment ID", IsReadOnly = true)]
  public virtual string ExternalPaymentID { get; set; }

  /// <summary>External payment status</summary>
  [PXDBString(50)]
  [PXUIField(DisplayName = "External Payment Status", IsReadOnly = true)]
  public virtual string ExternalPaymentStatus { get; set; }

  public class PK : PrimaryKeyOf<APPayment>.By<APPayment.docType, APPayment.refNbr>
  {
    public static APPayment Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APPayment>.By<APPayment.docType, APPayment.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.refNbr>
  {
  }

  public abstract class remitAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.remitAddressID>
  {
  }

  public abstract class remitContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.remitContactID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPayment.paymentMethodID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.cashAccountID>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.extRefNbr>
  {
  }

  public abstract class adjDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APPayment.adjDate>
  {
  }

  public abstract class adjFinPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.adjFinPeriodID>
  {
  }

  public abstract class adjTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPayment.adjTranPeriodID>
  {
  }

  public abstract class stubCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.stubCntr>
  {
  }

  public abstract class billCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.billCntr>
  {
  }

  public abstract class chargeCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.chargeCntr>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.cleared>
  {
  }

  public abstract class clearDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APPayment.clearDate>
  {
  }

  public abstract class cATranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APPayment.cATranID>
  {
  }

  public abstract class printCheck : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.printCheck>
  {
  }

  public abstract class curyOrigTaxDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPayment.curyOrigTaxDiscAmt>
  {
  }

  public abstract class origTaxDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPayment.origTaxDiscAmt>
  {
  }

  public abstract class depositAsBatch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.depositAsBatch>
  {
  }

  public abstract class depositAfter : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APPayment.depositAfter>
  {
  }

  public abstract class deposited : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.deposited>
  {
  }

  public abstract class depositDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APPayment.depositDate>
  {
  }

  public abstract class depositType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.depositType>
  {
  }

  public abstract class depositNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.depositNbr>
  {
  }

  public abstract class externalPaymentID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPayment.externalPaymentID>
  {
  }

  public abstract class externalPaymentStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPayment.externalPaymentStatus>
  {
  }
}
