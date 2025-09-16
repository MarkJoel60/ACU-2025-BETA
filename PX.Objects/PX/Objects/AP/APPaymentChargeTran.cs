// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPaymentChargeTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// Represents a charge or a fee applied by the bank for processing
/// of an <see cref="T:PX.Objects.AP.APPayment">Accounts Payable payment</see>.
/// Entities of this type can be edited on the Finance Charges tab
/// of the Checks and Payments (AP302000) form, which corresponds
/// to the <see cref="T:PX.Objects.AP.APPaymentEntry" /> graph.
/// </summary>
[PXCacheName("AP Financial Charge Transaction")]
[Serializable]
public class APPaymentChargeTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IPaymentCharge
{
  protected 
  #nullable disable
  string _DocType;
  protected string _RefNbr;
  protected int? _LineNbr;
  protected int? _CashAccountID;
  protected string _DrCr;
  protected string _ExtRefNbr;
  protected string _EntryTypeID;
  protected int? _AccountID;
  protected int? _SubID;
  protected System.DateTime? _TranDate;
  protected string _FinPeriodID;
  protected string _TranPeriodID;
  protected string _TranDesc;
  protected long? _CuryInfoID;
  protected Decimal? _CuryTranAmt;
  protected Decimal? _TranAmt;
  protected bool? _Released;
  protected bool? _Cleared;
  protected System.DateTime? _ClearDate;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (APRegister.docType))]
  [PXUIField(DisplayName = "DocType")]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (APRegister.refNbr))]
  [PXParent(typeof (Select<APRegister, Where<APRegister.docType, Equal<Current<APPaymentChargeTran.docType>>, And<APRegister.refNbr, Equal<Current<APPaymentChargeTran.refNbr>>>>>))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Line Nbr.", Visibility = PXUIVisibility.Visible, Visible = false)]
  [PXLineNbr(typeof (APPayment.chargeCntr), DecrementOnDelete = false)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt]
  [PXDefault(typeof (APPayment.cashAccountID))]
  [PXUIField(DisplayName = "Cash Account ID")]
  [PXSelector(typeof (Search<PX.Objects.CA.CashAccount.cashAccountID>), ValidateValue = false)]
  public virtual int? CashAccountID
  {
    get => this._CashAccountID;
    set => this._CashAccountID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXFormula(typeof (Selector<APPaymentChargeTran.entryTypeID, CAEntryType.drCr>))]
  [CADrCr.List]
  [PXUIField(DisplayName = "Disb./Receipt", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string DrCr
  {
    get => this._DrCr;
    set => this._DrCr = value;
  }

  [PXDBString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "ExtRefNbr")]
  public virtual string ExtRefNbr
  {
    get => this._ExtRefNbr;
    set => this._ExtRefNbr = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXSelector(typeof (Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>, Where<CashAccountETDetail.cashAccountID, Equal<Current<APPayment.cashAccountID>>, And<CAEntryType.drCr, Equal<CADrCr.cACredit>>>>))]
  [PXUIField(DisplayName = "Entry Type")]
  public virtual string EntryTypeID
  {
    get => this._EntryTypeID;
    set => this._EntryTypeID = value;
  }

  [PXDefault]
  [PXFormula(typeof (Selector<APPaymentChargeTran.entryTypeID, Selector<CAEntryType.accountID, PX.Objects.GL.Account.accountCD>>))]
  [Account(DisplayName = "Offset Account", AvoidControlAccounts = true)]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [PXDefault]
  [PXFormula(typeof (Selector<APPaymentChargeTran.entryTypeID, CAEntryType.subID>))]
  [SubAccount(typeof (APPaymentChargeTran.accountID), DisplayName = "Offset Subaccount")]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBDate]
  [PXDefault(typeof (APPayment.docDate))]
  [PXUIField(DisplayName = "TranDate")]
  public virtual System.DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PX.Objects.GL.FinPeriodID(null, typeof (APPaymentChargeTran.cashAccountID), typeof (Selector<APPaymentChargeTran.cashAccountID, PX.Objects.CA.CashAccount.branchID>), null, null, null, true, false, null, typeof (APPaymentChargeTran.tranPeriodID), typeof (APPayment.tranPeriodID), true, true)]
  [PXUIField(DisplayName = "FinPeriodID")]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PeriodID(null, null, null, true)]
  [PXUIField(DisplayName = "TranPeriodID")]
  public virtual string TranPeriodID
  {
    get => this._TranPeriodID;
    set => this._TranPeriodID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXFormula(typeof (Selector<APPaymentChargeTran.entryTypeID, CAEntryType.descr>))]
  [PXUIField(DisplayName = "Description")]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  [PXDBLong]
  [PX.Objects.CM.Extensions.CurrencyInfo(typeof (APRegister.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBLong]
  [APPaymentChargeCashTranID]
  public virtual long? CashTranID { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APPaymentChargeTran.curyInfoID), typeof (APPaymentChargeTran.tranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  [PXFormula(null, typeof (SumCalc<APRegister.curyChargeAmt>))]
  public virtual Decimal? CuryTranAmt
  {
    get => this._CuryTranAmt;
    set => this._CuryTranAmt = value;
  }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranAmt
  {
    get => this._TranAmt;
    set => this._TranAmt = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released")]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Cleared")]
  public virtual bool? Cleared
  {
    get => this._Cleared;
    set => this._Cleared = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "ClearDate")]
  public virtual System.DateTime? ClearDate
  {
    get => this._ClearDate;
    set => this._ClearDate = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public int GetCASign()
  {
    return (APPaymentType.DrCr(this.DocType) == "D" ? 1 : -1) * (this.DrCr == "D" ? 1 : -1) * -1;
  }

  public class PK : 
    PrimaryKeyOf<APPaymentChargeTran>.By<APPaymentChargeTran.docType, APPaymentChargeTran.refNbr, APPaymentChargeTran.lineNbr>
  {
    public static APPaymentChargeTran Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APPaymentChargeTran>.By<APPaymentChargeTran.docType, APPaymentChargeTran.refNbr, APPaymentChargeTran.lineNbr>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<APPaymentChargeTran>.By<APPaymentChargeTran.curyInfoID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<APPaymentChargeTran>.By<APPaymentChargeTran.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<APPaymentChargeTran>.By<APPaymentChargeTran.subID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<PX.Objects.CA.CashAccount>.By<PX.Objects.CA.CashAccount.cashAccountID>.ForeignKeyOf<APPaymentChargeTran>.By<APPaymentChargeTran.cashAccountID>
    {
    }

    public class CashAccountTransaction : 
      PrimaryKeyOf<CATran>.By<CATran.cashAccountID, CATran.tranID>.ForeignKeyOf<APPaymentChargeTran>.By<APPaymentChargeTran.cashAccountID, APPaymentChargeTran.cashTranID>
    {
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPaymentChargeTran.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPaymentChargeTran.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPaymentChargeTran.lineNbr>
  {
  }

  public abstract class cashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APPaymentChargeTran.cashAccountID>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPaymentChargeTran.drCr>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPaymentChargeTran.extRefNbr>
  {
  }

  public abstract class entryTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPaymentChargeTran.entryTypeID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPaymentChargeTran.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPaymentChargeTran.subID>
  {
  }

  public abstract class tranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPaymentChargeTran.tranDate>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPaymentChargeTran.finPeriodID>
  {
  }

  public abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPaymentChargeTran.tranPeriodID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPaymentChargeTran.tranDesc>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APPaymentChargeTran.curyInfoID>
  {
  }

  public abstract class cashTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APPaymentChargeTran.cashTranID>
  {
  }

  public abstract class curyTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPaymentChargeTran.curyTranAmt>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APPaymentChargeTran.tranAmt>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPaymentChargeTran.released>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPaymentChargeTran.cleared>
  {
  }

  public abstract class clearDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPaymentChargeTran.clearDate>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APPaymentChargeTran.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPaymentChargeTran.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPaymentChargeTran.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APPaymentChargeTran.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPaymentChargeTran.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPaymentChargeTran.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APPaymentChargeTran.Tstamp>
  {
  }
}
