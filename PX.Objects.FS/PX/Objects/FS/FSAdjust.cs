// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAdjust
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

public class FSAdjust : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAdjustment
{
  protected bool? _Hold;
  protected int? _CustomerID;
  protected 
  #nullable disable
  string _AdjgDocType;
  public const int AdjgDocTypeLength = 3;
  protected string _AdjgRefNbr;
  public const int AdjgRefNbrLength = 15;
  protected string _AdjdOrderType;
  protected string _AdjdOrderNbr;
  protected Decimal? _CuryAdjgAmt;
  protected Decimal? _AdjAmt;
  protected Decimal? _CuryAdjdAmt;
  protected Decimal? _CuryOrigAdjdAmt;
  protected Decimal? _OrigAdjAmt;
  protected Decimal? _CuryOrigAdjgAmt;
  protected long? _AdjdOrigCuryInfoID;
  protected long? _AdjgCuryInfoID;
  protected long? _AdjdCuryInfoID;
  protected DateTime? _AdjgDocDate;
  protected DateTime? _AdjdOrderDate;
  protected Decimal? _CuryAdjgBilledAmt;
  protected Decimal? _AdjBilledAmt;
  protected Decimal? _CuryAdjdBilledAmt;
  protected Decimal? _CuryDocBal;
  protected Decimal? _DocBal;
  protected bool? _Voided;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected string _AdjdAppRefNbr;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  [PXDBInt]
  [PXDefault(typeof (ARPayment.customerID))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
  [ARPaymentType.List]
  [PXDefault(typeof (ARPayment.docType))]
  [PXUIField(DisplayName = "Doc. Type", Enabled = false, Visible = false)]
  public virtual string AdjgDocType
  {
    get => this._AdjgDocType;
    set => this._AdjgDocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (ARPayment.refNbr), DefaultForUpdate = false)]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false, Visible = false)]
  [PXParent(typeof (Select<ARPayment, Where<ARPayment.docType, Equal<Current<FSAdjust.adjgDocType>>, And<ARPayment.refNbr, Equal<Current<FSAdjust.adjgRefNbr>>>>>))]
  public virtual string AdjgRefNbr
  {
    get => this._AdjgRefNbr;
    set => this._AdjgRefNbr = value;
  }

  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (Coalesce<Search<FSxUserPreferences.dfltSrvOrdType, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>>>, Search<FSSetup.dfltSrvOrdType>>))]
  [PXUIField(DisplayName = "Service Order Type", Enabled = false)]
  [FSSelectorSrvOrdType]
  public virtual string AdjdOrderType
  {
    get => this._AdjdOrderType;
    set => this._AdjdOrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Service Order Nbr.", Enabled = false)]
  [PXParent(typeof (Select<FSServiceOrder, Where<FSServiceOrder.srvOrdType, Equal<Current<FSAdjust.adjdOrderType>>, And<FSServiceOrder.refNbr, Equal<Current<FSAdjust.adjdOrderNbr>>>>>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<FSAdjust.curyAdjdAmt, Greater<decimal0>>, int1>, int0>), typeof (SumCalc<FSServiceOrder.lineCntr>))]
  [PXRestrictor(typeof (Where<FSServiceOrder.canceled, Equal<False>, And<FSServiceOrder.quote, Equal<False>>>), "A Prepayment cannot be created for a Service Order with one of the following statuses: On Hold, Quote, or Canceled.", new System.Type[] {})]
  [PXSelector(typeof (Search2<FSServiceOrder.refNbr, LeftJoin<BAccountSelectorBase, On<BAccountSelectorBase.bAccountID, Equal<FSServiceOrder.customerID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<FSServiceOrder.locationID>>>>, Where<FSServiceOrder.customerID, Equal<Current<FSAdjust.customerID>>, And<FSServiceOrder.srvOrdType, Equal<Optional<FSAdjust.adjdOrderType>>>>, OrderBy<Desc<FSServiceOrder.refNbr>>>), new System.Type[] {typeof (FSServiceOrder.refNbr), typeof (FSServiceOrder.srvOrdType), typeof (BAccountSelectorBase.type), typeof (BAccountSelectorBase.acctCD), typeof (BAccountSelectorBase.acctName), typeof (PX.Objects.CR.Location.locationCD), typeof (FSServiceOrder.status), typeof (FSServiceOrder.priority), typeof (FSServiceOrder.severity), typeof (FSServiceOrder.orderDate), typeof (FSServiceOrder.sLAETA), typeof (FSServiceOrder.assignedEmpID), typeof (FSServiceOrder.sourceType), typeof (FSServiceOrder.sourceRefNbr)}, Filterable = true)]
  public virtual string AdjdOrderNbr
  {
    get => this._AdjdOrderNbr;
    set => this._AdjdOrderNbr = value;
  }

  [PXDBCurrency(typeof (FSAdjust.adjgCuryInfoID), typeof (FSAdjust.adjAmt))]
  [PXFormula(null, typeof (SumCalc<ARPayment.curySOApplAmt>))]
  [PXFormula(typeof (Sub<FSAdjust.curyOrigAdjgAmt, FSAdjust.curyAdjgBilledAmt>))]
  [PXUIField(DisplayName = "Applied To Order")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjgAmt
  {
    get => this._CuryAdjgAmt;
    set => this._CuryAdjgAmt = value;
  }

  [PXDBDecimal(4)]
  [PXFormula(typeof (Sub<FSAdjust.origAdjAmt, FSAdjust.adjBilledAmt>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjAmt
  {
    get => this._AdjAmt;
    set => this._AdjAmt = value;
  }

  [PXDBDecimal(4)]
  [PXFormula(typeof (Sub<FSAdjust.curyOrigAdjdAmt, FSAdjust.curyAdjdBilledAmt>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjdAmt
  {
    get => this._CuryAdjdAmt;
    set => this._CuryAdjdAmt = value;
  }

  [PXDBCalced(typeof (Add<FSAdjust.curyAdjdAmt, FSAdjust.curyAdjdBilledAmt>), typeof (Decimal))]
  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOrigAdjdAmt
  {
    get => this._CuryOrigAdjdAmt;
    set => this._CuryOrigAdjdAmt = value;
  }

  [PXDBCalced(typeof (Add<FSAdjust.adjAmt, FSAdjust.adjBilledAmt>), typeof (Decimal))]
  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigAdjAmt
  {
    get => this._OrigAdjAmt;
    set => this._OrigAdjAmt = value;
  }

  [PXDBCalced(typeof (Add<FSAdjust.curyAdjgAmt, FSAdjust.curyAdjgBilledAmt>), typeof (Decimal))]
  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOrigAdjgAmt
  {
    get => this._CuryOrigAdjgAmt;
    set => this._CuryOrigAdjgAmt = value;
  }

  [PXDecimal(4)]
  public Decimal? CuryAdjgDiscAmt
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  [PXDecimal(4)]
  public Decimal? CuryAdjdDiscAmt
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  [PXDecimal(4)]
  public Decimal? AdjDiscAmt
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  [PXDBLong]
  [PXDefault]
  [CurrencyInfo(ModuleCode = "SO", CuryIDField = "AdjdOrigCuryID")]
  public virtual long? AdjdOrigCuryInfoID
  {
    get => this._AdjdOrigCuryInfoID;
    set => this._AdjdOrigCuryInfoID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (ARPayment.curyInfoID))]
  public virtual long? AdjgCuryInfoID
  {
    get => this._AdjgCuryInfoID;
    set => this._AdjgCuryInfoID = value;
  }

  [PXDBLong]
  [PXDefault]
  [CurrencyInfo(CuryIDField = "AdjdCuryID")]
  public virtual long? AdjdCuryInfoID
  {
    get => this._AdjdCuryInfoID;
    set => this._AdjdCuryInfoID = value;
  }

  [PXDBDate]
  [PXDefault(typeof (ARPayment.adjDate))]
  public virtual DateTime? AdjgDocDate
  {
    get => this._AdjgDocDate;
    set => this._AdjgDocDate = value;
  }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? AdjdOrderDate
  {
    get => this._AdjdOrderDate;
    set => this._AdjdOrderDate = value;
  }

  [PXDBCurrency(typeof (FSAdjust.adjgCuryInfoID), typeof (FSAdjust.adjBilledAmt), BaseCalc = false)]
  [PXUIField(DisplayName = "Transferred to Invoice", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjgBilledAmt
  {
    get => this._CuryAdjgBilledAmt;
    set => this._CuryAdjgBilledAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjBilledAmt
  {
    get => this._AdjBilledAmt;
    set => this._AdjBilledAmt = value;
  }

  [PXDBCurrency(typeof (FSAdjust.adjdCuryInfoID), typeof (FSAdjust.adjBilledAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transferred to Invoice", Enabled = false)]
  public virtual Decimal? CuryAdjdBilledAmt
  {
    get => this._CuryAdjdBilledAmt;
    set => this._CuryAdjdBilledAmt = value;
  }

  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXCurrency(typeof (FSAdjust.adjgCuryInfoID), typeof (FSAdjust.docBal), BaseCalc = false)]
  [PXUIField]
  public virtual Decimal? CuryDocBal
  {
    get => this._CuryDocBal;
    set => this._CuryDocBal = value;
  }

  [PXDecimal(4)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DocBal
  {
    get => this._DocBal;
    set => this._DocBal = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
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

  public Decimal? CuryPayDocBal
  {
    get => this._CuryDocBal;
    set => this._CuryDocBal = value;
  }

  public Decimal? PayDocBal
  {
    get => this._DocBal;
    set => this._DocBal = value;
  }

  public Decimal? CuryPayDiscBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public Decimal? PayDiscBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public Decimal? CuryPayWhTaxBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public Decimal? PayWhTaxBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public DateTime? AdjdDocDate
  {
    get => this._AdjdOrderDate;
    set => this._AdjdOrderDate = value;
  }

  public Decimal? RGOLAmt
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public bool? Released
  {
    get => new bool?(false);
    set
    {
    }
  }

  public bool? ReverseGainLoss
  {
    get => new bool?(false);
    set
    {
    }
  }

  public Decimal? CuryDiscBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public Decimal? DiscBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public Decimal? CuryAdjgWhTaxAmt
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public Decimal? CuryAdjdWhTaxAmt
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public Decimal? AdjWhTaxAmt
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public Decimal? CuryWhTaxBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public Decimal? WhTaxBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  [PXDecimal]
  [PXUIField(DisplayName = "Service Order Billable Total", Enabled = false)]
  public virtual Decimal? SOCuryCompletedBillableTotal { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Source Appointment Nbr.", Enabled = false)]
  public virtual string AdjdAppRefNbr
  {
    get => this._AdjdAppRefNbr;
    set => this._AdjdAppRefNbr = value;
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAdjust.hold>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAdjust.customerID>
  {
  }

  public abstract class adjgDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAdjust.adjgDocType>
  {
  }

  public abstract class adjgRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAdjust.adjgRefNbr>
  {
  }

  public abstract class adjdOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAdjust.adjdOrderType>
  {
  }

  public abstract class adjdOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAdjust.adjdOrderNbr>
  {
  }

  public abstract class curyAdjgAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAdjust.curyAdjgAmt>
  {
  }

  public abstract class adjAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAdjust.adjAmt>
  {
  }

  public abstract class curyAdjdAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAdjust.curyAdjdAmt>
  {
  }

  public abstract class curyOrigAdjdAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAdjust.curyOrigAdjdAmt>
  {
  }

  public abstract class origAdjAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAdjust.origAdjAmt>
  {
  }

  public abstract class curyOrigAdjgAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAdjust.curyOrigAdjgAmt>
  {
  }

  public abstract class curyAdjgDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAdjust.curyAdjgDiscAmt>
  {
  }

  public abstract class curyAdjdDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAdjust.curyAdjdDiscAmt>
  {
  }

  public abstract class adjDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAdjust.adjDiscAmt>
  {
  }

  public abstract class adjdOrigCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    FSAdjust.adjdOrigCuryInfoID>
  {
  }

  public abstract class adjgCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSAdjust.adjgCuryInfoID>
  {
  }

  public abstract class adjdCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSAdjust.adjdCuryInfoID>
  {
  }

  public abstract class adjgDocDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSAdjust.adjgDocDate>
  {
  }

  public abstract class adjdOrderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSAdjust.adjdOrderDate>
  {
  }

  public abstract class curyAdjgBilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAdjust.curyAdjgBilledAmt>
  {
  }

  public abstract class adjBilledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAdjust.adjBilledAmt>
  {
  }

  public abstract class curyAdjdBilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAdjust.curyAdjdBilledAmt>
  {
  }

  public abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAdjust.curyDocBal>
  {
  }

  public abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAdjust.docBal>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAdjust.voided>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSAdjust.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSAdjust.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSAdjust.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAdjust.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAdjust.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSAdjust.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAdjust.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAdjust.lastModifiedDateTime>
  {
  }

  public abstract class sOCuryCompletedBillableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAdjust.sOCuryCompletedBillableTotal>
  {
  }

  public abstract class adjdAppRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAdjust.adjdAppRefNbr>
  {
  }
}
