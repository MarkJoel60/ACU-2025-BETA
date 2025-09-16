// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPaymentChargeTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CA;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// Represents a charge or a fee applied by the bank for processing
/// of an <see cref="T:PX.Objects.AR.ARPayment">Accounts Receivable payment</see>.
/// Entities of this type can be edited on the Finance Charges tab
/// of the Payments and Applications (AR302000) form, which corresponds
/// to the <see cref="T:PX.Objects.AR.ARPaymentEntry" /> graph.
/// </summary>
[PXCacheName("AR Payment Charge Transaction")]
[Serializable]
public class ARPaymentChargeTran : PXBqlTable, IPaymentCharge, IBqlTable, IBqlTableSystemDataStorage
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
  protected DateTime? _TranDate;
  protected string _FinPeriodID;
  protected string _TranPeriodID;
  protected string _TranDesc;
  protected long? _CuryInfoID;
  protected long? _CashTranID;
  protected Decimal? _CuryTranAmt;
  protected Decimal? _TranAmt;
  protected bool? _Released;
  protected bool? _Cleared;
  protected DateTime? _ClearDate;
  protected bool? _Consolidate;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (ARRegister.docType))]
  [PXUIField(DisplayName = "DocType")]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (ARRegister.refNbr))]
  [PXParent(typeof (Select<ARRegister, Where<ARRegister.docType, Equal<Current<ARPaymentChargeTran.docType>>, And<ARRegister.refNbr, Equal<Current<ARPaymentChargeTran.refNbr>>>>>))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXLineNbr(typeof (ARPayment.chargeCntr), DecrementOnDelete = false)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt]
  [PXDBDefault(typeof (ARPayment.cashAccountID))]
  [PXSelector(typeof (Search<PX.Objects.CA.CashAccount.cashAccountID>), ValidateValue = false)]
  [PXUIField(DisplayName = "Cash Account ID")]
  public virtual int? CashAccountID
  {
    get => this._CashAccountID;
    set => this._CashAccountID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXFormula(typeof (Selector<ARPaymentChargeTran.entryTypeID, CAEntryType.drCr>))]
  [CADrCr.List]
  [PXUIField]
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
  [PXSelector(typeof (Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>, Where<CashAccountETDetail.cashAccountID, Equal<Current<ARPayment.cashAccountID>>, And<CAEntryType.drCr, Equal<CADrCr.cACredit>, And<CAEntryType.module, Equal<BatchModule.moduleCA>>>>>))]
  [PXUIField(DisplayName = "Entry Type")]
  public virtual string EntryTypeID
  {
    get => this._EntryTypeID;
    set => this._EntryTypeID = value;
  }

  [PXDefault]
  [PXFormula(typeof (Selector<ARPaymentChargeTran.entryTypeID, Selector<CAEntryType.accountID, PX.Objects.GL.Account.accountCD>>))]
  [Account(DisplayName = "Offset Account", Required = true, AvoidControlAccounts = true)]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [PXDefault]
  [PXFormula(typeof (Selector<ARPaymentChargeTran.entryTypeID, CAEntryType.subID>))]
  [SubAccount(typeof (ARPaymentChargeTran.accountID), DisplayName = "Offset Subaccount", Required = true)]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBDate]
  [PXDefault(typeof (ARPayment.docDate))]
  [PXUIField(DisplayName = "TranDate")]
  public virtual DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PX.Objects.GL.FinPeriodID(null, typeof (ARPaymentChargeTran.cashAccountID), typeof (Selector<ARPaymentChargeTran.cashAccountID, PX.Objects.CA.CashAccount.branchID>), null, null, null, true, false, null, typeof (ARPaymentChargeTran.tranPeriodID), typeof (ARPayment.tranPeriodID), true, true)]
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
  [PXFormula(typeof (Selector<ARPaymentChargeTran.entryTypeID, CAEntryType.descr>))]
  [PXUIField(DisplayName = "Description")]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (ARRegister.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBLong]
  [ARPaymentChargeCashTranID]
  public virtual long? CashTranID
  {
    get => this._CashTranID;
    set => this._CashTranID = value;
  }

  [PXDBCurrency(typeof (ARPaymentChargeTran.curyInfoID), typeof (ARPaymentChargeTran.tranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  [PXFormula(null, typeof (SumCalc<ARRegister.curyChargeAmt>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARPaymentChargeTran.consolidate, Equal<True>>, ARPaymentChargeTran.curyTranAmt>, decimal0>), typeof (SumCalc<ARPayment.curyConsolidateChargeTotal>))]
  public virtual Decimal? CuryTranAmt
  {
    get => this._CuryTranAmt;
    set => this._CuryTranAmt = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the charge
  /// or the <see cref="P:PX.Objects.PM.PMSetup.NonProjectCode">non-project code</see>, which indicates that the charge is not related to any particular project.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDefault(typeof (ARPayment.projectID))]
  [ARActiveProject(ValidateValue = false)]
  [PXForeignReference(typeof (Field<ARPaymentChargeTran.projectID>.IsRelatedTo<PMProject.contractID>))]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// Identifier of the particular <see cref="T:PX.Objects.PM.PMTask">task</see> associated with the charge. The task belongs to the <see cref="P:PX.Objects.AR.ARPaymentChargeTran.ProjectID">selected project</see>
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMTask.TaskID">PMTask.TaskID</see> field.
  /// </value>
  [ActiveProjectTask(typeof (ARPaymentChargeTran.projectID), "AR", DisplayName = "Project Task", Required = true)]
  [PXForeignReference(typeof (Field<ARPaymentChargeTran.taskID>.IsRelatedTo<PMTask.taskID>))]
  public virtual int? TaskID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">Cost Code</see> associated with the charge.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMCostCode.costCodeID" /> field.
  /// </value>
  [CostCode(typeof (ARPaymentChargeTran.accountID), typeof (ARPaymentChargeTran.taskID), null)]
  [PXForeignReference(typeof (Field<ARPaymentChargeTran.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  public virtual int? CostCodeID { get; set; }

  [PXDBBaseCury]
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
  public virtual DateTime? ClearDate
  {
    get => this._ClearDate;
    set => this._ClearDate = value;
  }

  [PXDBBool]
  [PXFormula(typeof (Selector<ARPaymentChargeTran.entryTypeID, CAEntryType.consolidate>))]
  public bool? Consolidate
  {
    get => this._Consolidate;
    set => this._Consolidate = value;
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
  public virtual DateTime? CreatedDateTime
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
  public virtual DateTime? LastModifiedDateTime
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
    return (ARPaymentType.DrCr(this.DocType) == "D" ? 1 : -1) * (this.DrCr == "D" ? 1 : -1) * (this.DocType == "REF" || this.DocType == "VRF" ? -1 : 1);
  }

  public class PK : 
    PrimaryKeyOf<ARPaymentChargeTran>.By<ARPaymentChargeTran.docType, ARPaymentChargeTran.refNbr, ARPaymentChargeTran.lineNbr>
  {
    public static ARPaymentChargeTran Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (ARPaymentChargeTran) PrimaryKeyOf<ARPaymentChargeTran>.By<ARPaymentChargeTran.docType, ARPaymentChargeTran.refNbr, ARPaymentChargeTran.lineNbr>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARPaymentChargeTran>.By<ARPaymentChargeTran.cashAccountID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ARPaymentChargeTran>.By<ARPaymentChargeTran.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ARPaymentChargeTran>.By<ARPaymentChargeTran.subID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARPaymentChargeTran>.By<ARPaymentChargeTran.curyInfoID>
    {
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPaymentChargeTran.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPaymentChargeTran.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPaymentChargeTran.lineNbr>
  {
  }

  public abstract class cashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARPaymentChargeTran.cashAccountID>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPaymentChargeTran.drCr>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPaymentChargeTran.extRefNbr>
  {
  }

  public abstract class entryTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPaymentChargeTran.entryTypeID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPaymentChargeTran.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPaymentChargeTran.subID>
  {
  }

  public abstract class tranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARPaymentChargeTran.tranDate>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPaymentChargeTran.finPeriodID>
  {
  }

  public abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPaymentChargeTran.tranPeriodID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPaymentChargeTran.tranDesc>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARPaymentChargeTran.curyInfoID>
  {
  }

  public abstract class cashTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARPaymentChargeTran.cashTranID>
  {
  }

  public abstract class curyTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARPaymentChargeTran.curyTranAmt>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPaymentChargeTran.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPaymentChargeTran.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPaymentChargeTran.costCodeID>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARPaymentChargeTran.tranAmt>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPaymentChargeTran.released>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPaymentChargeTran.cleared>
  {
  }

  public abstract class clearDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARPaymentChargeTran.clearDate>
  {
  }

  public abstract class consolidate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPaymentChargeTran.consolidate>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARPaymentChargeTran.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPaymentChargeTran.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARPaymentChargeTran.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARPaymentChargeTran.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPaymentChargeTran.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARPaymentChargeTran.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARPaymentChargeTran.Tstamp>
  {
  }
}
