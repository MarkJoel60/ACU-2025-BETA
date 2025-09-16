// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEquipmentTimeCard
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXPrimaryGraph(typeof (EquipmentTimeCardMaint))]
[PXCacheName("Equipment Time Card")]
[Serializable]
public class EPEquipmentTimeCard : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssign
{
  protected int? _EquipmentID;
  protected int? _WeekID;
  protected int? _SummaryLineCntr;
  protected int? _DetailLineCntr;
  protected Guid? _NoteID;
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [AutoNumber(typeof (EPSetup.equipmentTimeCardNumberingID), typeof (AccessInfo.businessDate))]
  [PXSelector(typeof (EPEquipmentTimeCard.timeCardCD), new Type[] {typeof (EPEquipmentTimeCard.timeCardCD), typeof (EPEquipmentTimeCard.equipmentID), typeof (EPEquipmentTimeCard.weekDescription), typeof (EPEquipmentTimeCard.status)})]
  [PXFieldDescription]
  public virtual string TimeCardCD { get; set; }

  [PXDefault]
  [PXDBInt]
  [PXUIField(DisplayName = "Equipment ID")]
  [PXSelector(typeof (Search<EPEquipment.equipmentID, Where<EPEquipment.isActive, Equal<True>>>), SubstituteKey = typeof (EPEquipment.equipmentCD), DescriptionField = typeof (EPEquipment.description))]
  [PXFieldDescription]
  public virtual int? EquipmentID
  {
    get => this._EquipmentID;
    set => this._EquipmentID = value;
  }

  [PXDBString(1)]
  [PXDefault("H")]
  [EPEquipmentTimeCardStatus]
  [PXUIField(DisplayName = "Status")]
  public virtual string Status { get; set; }

  [PXUIField(DisplayName = "Orig. Ref. Nbr.", Enabled = false)]
  [PXDBString(10, IsUnicode = true)]
  public virtual string OrigTimeCardCD { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Week")]
  [PXWeekSelector2(DescriptionField = typeof (EPWeekRaw.shortDescription))]
  public virtual int? WeekID
  {
    get => this._WeekID;
    set => this._WeekID = value;
  }

  [PXDBBool]
  [PXUIField(Visible = false)]
  [PXDefault(true)]
  public virtual bool? IsHold { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsApproved { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsRejected { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? IsReleased { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Workgroup ID", Visible = false)]
  [PXSelector(typeof (EPCompanyTreeOwner.workGroupID), SubstituteKey = typeof (EPCompanyTreeOwner.description))]
  public virtual int? WorkgroupID { get; set; }

  [PXInt]
  [PXUIField(Visible = false)]
  public virtual int? OwnerID { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? SummaryLineCntr
  {
    get => this._SummaryLineCntr;
    set => this._SummaryLineCntr = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? DetailLineCntr
  {
    get => this._DetailLineCntr;
    set => this._DetailLineCntr = value;
  }

  [PXNote(ShowInReferenceSelector = true, DescriptionField = typeof (EPEquipmentTimeCard.timeCardCD), Selector = typeof (EPEquipmentTimeCard.timeCardCD))]
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

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [TimecardWeekStartDate(typeof (EPEquipmentTimeCard.weekId))]
  [PXUIField(DisplayName = "Week Start Date")]
  public virtual DateTime? WeekStartDate { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Week")]
  [PXFormula(typeof (Selector<EPEquipmentTimeCard.weekId, EPWeekRaw.description>))]
  public virtual string WeekDescription { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Week")]
  [PXFieldDescription]
  [PXFormula(typeof (Selector<EPEquipmentTimeCard.weekId, EPWeekRaw.shortDescription>))]
  public virtual string WeekShortDescription { get; set; }

  [PXInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Time Spent", Enabled = false)]
  public virtual int? TimeSetupCalc { get; set; }

  [PXInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Run", Enabled = false)]
  public virtual int? TimeRunCalc { get; set; }

  [PXInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Suspend", Enabled = false)]
  public virtual int? TimeSuspendCalc { get; set; }

  [PXInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Total", Enabled = false)]
  public virtual int? TimeTotalCalc
  {
    get
    {
      int? nullable = this.TimeSetupCalc;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.TimeRunCalc;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      int num = valueOrDefault1 + valueOrDefault2;
      nullable = this.TimeSuspendCalc;
      int valueOrDefault3 = nullable.GetValueOrDefault();
      return new int?(num + valueOrDefault3);
    }
  }

  [PXInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Billable", Enabled = false)]
  public virtual int? TimeBillableSetupCalc { get; set; }

  [PXInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Billable Run", Enabled = false)]
  public virtual int? TimeBillableRunCalc { get; set; }

  [PXInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Billable Suspend", Enabled = false)]
  public virtual int? TimeBillableSuspendCalc { get; set; }

  [PXInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Billable Total", Enabled = false)]
  public virtual int? TimeBillableTotalCalc
  {
    get
    {
      int? nullable = this.TimeBillableSetupCalc;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.TimeBillableRunCalc;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      int num = valueOrDefault1 + valueOrDefault2;
      nullable = this.TimeBillableSuspendCalc;
      int valueOrDefault3 = nullable.GetValueOrDefault();
      return new int?(num + valueOrDefault3);
    }
  }

  [PXString]
  [PXStringList(new string[] {"N", "C"}, new string[] {"Normal", "Correction"})]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  public virtual string TimecardType => !string.IsNullOrEmpty(this.OrigTimeCardCD) ? "C" : "N";

  /// <summary>The time spent total for Sunday.</summary>
  [PXInt]
  public virtual int? SunTotal { get; set; }

  /// <summary>The time spent total for Monday.</summary>
  [PXInt]
  public virtual int? MonTotal { get; set; }

  /// <summary>The time spent total for Tuesday.</summary>
  [PXInt]
  public virtual int? TueTotal { get; set; }

  /// <summary>The time spent total for Wednesday.</summary>
  [PXInt]
  public virtual int? WedTotal { get; set; }

  /// <summary>The time spent total for Thursday.</summary>
  [PXInt]
  public virtual int? ThuTotal { get; set; }

  /// <summary>The time spent total for Friday.</summary>
  [PXInt]
  public virtual int? FriTotal { get; set; }

  /// <summary>The time spent total for Saturday.</summary>
  [PXInt]
  public virtual int? SatTotal { get; set; }

  /// <summary>The time spent total for the whole week.</summary>
  [PXInt]
  [PXDependsOnFields(new Type[] {typeof (EPEquipmentTimeCard.monTotal), typeof (EPEquipmentTimeCard.tueTotal), typeof (EPEquipmentTimeCard.wedTotal), typeof (EPEquipmentTimeCard.thuTotal), typeof (EPEquipmentTimeCard.friTotal), typeof (EPEquipmentTimeCard.satTotal), typeof (EPEquipmentTimeCard.sunTotal)})]
  public virtual int? WeekTotal
  {
    get
    {
      int? nullable = this.SunTotal;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.MonTotal;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      int num1 = valueOrDefault1 + valueOrDefault2;
      nullable = this.TueTotal;
      int valueOrDefault3 = nullable.GetValueOrDefault();
      int num2 = num1 + valueOrDefault3;
      nullable = this.WedTotal;
      int valueOrDefault4 = nullable.GetValueOrDefault();
      int num3 = num2 + valueOrDefault4;
      nullable = this.ThuTotal;
      int valueOrDefault5 = nullable.GetValueOrDefault();
      int num4 = num3 + valueOrDefault5;
      nullable = this.FriTotal;
      int valueOrDefault6 = nullable.GetValueOrDefault();
      int num5 = num4 + valueOrDefault6;
      nullable = this.SatTotal;
      int valueOrDefault7 = nullable.GetValueOrDefault();
      return new int?(num5 + valueOrDefault7);
    }
  }

  public class Events : PXEntityEventBase<EPEquipmentTimeCard>.Container<EPEquipmentTimeCard.Events>
  {
    public PXEntityEvent<EPEquipmentTimeCard> UpdateStatus;
  }

  public abstract class timeCardCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEquipmentTimeCard.timeCardCD>
  {
  }

  public abstract class equipmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentTimeCard.equipmentID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEquipmentTimeCard.status>
  {
  }

  public abstract class origTimeCardCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEquipmentTimeCard.origTimeCardCD>
  {
  }

  public abstract class weekId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentTimeCard.weekId>
  {
  }

  public abstract class isHold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPEquipmentTimeCard.isHold>
  {
  }

  public abstract class isApproved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPEquipmentTimeCard.isApproved>
  {
  }

  public abstract class isRejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPEquipmentTimeCard.isRejected>
  {
  }

  public abstract class isReleased : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPEquipmentTimeCard.isReleased>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentTimeCard.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentTimeCard.ownerID>
  {
  }

  public abstract class summaryLineCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEquipmentTimeCard.summaryLineCntr>
  {
  }

  public abstract class detailLineCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEquipmentTimeCard.detailLineCntr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEquipmentTimeCard.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPEquipmentTimeCard.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEquipmentTimeCard.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEquipmentTimeCard.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEquipmentTimeCard.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPEquipmentTimeCard.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEquipmentTimeCard.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEquipmentTimeCard.lastModifiedDateTime>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPEquipmentTimeCard.selected>
  {
  }

  public abstract class weekStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEquipmentTimeCard.weekStartDate>
  {
  }

  public abstract class weekDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEquipmentTimeCard.weekDescription>
  {
  }

  public abstract class weekShortDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEquipmentTimeCard.weekShortDescription>
  {
  }

  public abstract class timeSetupCalc : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEquipmentTimeCard.timeSetupCalc>
  {
  }

  public abstract class timeRunCalc : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentTimeCard.timeRunCalc>
  {
  }

  public abstract class timeSuspendCalc : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEquipmentTimeCard.timeSuspendCalc>
  {
  }

  public abstract class timeTotalCalc : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEquipmentTimeCard.timeTotalCalc>
  {
  }

  public abstract class timeBillableSetupCalc : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEquipmentTimeCard.timeBillableSetupCalc>
  {
  }

  public abstract class timeBillableRunCalc : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEquipmentTimeCard.timeBillableRunCalc>
  {
  }

  public abstract class timeBillableSuspendCalc : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEquipmentTimeCard.timeBillableSuspendCalc>
  {
  }

  public abstract class timeBillableTotalCalc : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEquipmentTimeCard.timeBillableTotalCalc>
  {
  }

  public abstract class timecardType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEquipmentTimeCard.timecardType>
  {
  }

  public abstract class sunTotal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentTimeCard.sunTotal>
  {
  }

  public abstract class monTotal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentTimeCard.monTotal>
  {
  }

  public abstract class tueTotal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentTimeCard.tueTotal>
  {
  }

  public abstract class wedTotal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentTimeCard.wedTotal>
  {
  }

  public abstract class thuTotal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentTimeCard.thuTotal>
  {
  }

  public abstract class friTotal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentTimeCard.friTotal>
  {
  }

  public abstract class satTotal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentTimeCard.satTotal>
  {
  }

  public abstract class weekTotal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentTimeCard.weekTotal>
  {
  }
}
