// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEquipmentSummary
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXCacheName("Equipment Time Card Summary")]
[Serializable]
public class EPEquipmentSummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const 
  #nullable disable
  string Setup = "ST";
  public const string Run = "RU";
  public const string Suspend = "SD";
  protected bool? _IsBillable;
  protected string _Description;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBDefault(typeof (EPEquipmentTimeCard.timeCardCD))]
  [PXDBString(10, IsKey = true)]
  [PXUIField(Visible = false)]
  [PXParent(typeof (Select<EPEquipmentTimeCard, Where<EPEquipmentTimeCard.timeCardCD, Equal<Current<EPEquipmentSummary.timeCardCD>>>>))]
  public virtual string TimeCardCD { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (EPEquipmentTimeCard.summaryLineCntr))]
  [PXUIField(Visible = false)]
  public virtual int? LineNbr { get; set; }

  [PXStringList(new string[] {"ST", "RU", "SD"}, new string[] {"Setup", "Run", "Suspend"})]
  [PXDBString(2, IsFixed = true, IsUnicode = false, InputMask = ">LL")]
  [PXDefault("RU")]
  [PXUIField(DisplayName = "Rate Type")]
  public virtual string RateType { get; set; }

  [ProjectDefault("TA")]
  [EPEquipmentActiveProject]
  [PXForeignReference(typeof (Field<EPEquipmentSummary.projectID>.IsRelatedTo<PMProject.contractID>))]
  public virtual int? ProjectID { get; set; }

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<EPEquipmentSummary.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [EPTimecardProjectTask(typeof (EPEquipmentSummary.projectID), "TA", DisplayName = "Project Task")]
  [PXForeignReference(typeof (CompositeKey<Field<EPEquipmentSummary.projectID>.IsRelatedTo<PMTask.projectID>, Field<EPEquipmentSummary.projectTaskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? ProjectTaskID { get; set; }

  [CostCode(null, typeof (EPEquipmentSummary.projectTaskID), "E", DescriptionField = typeof (PMCostCode.description))]
  [PXForeignReference(typeof (Field<EPEquipmentSummary.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  public virtual int? CostCodeID { get; set; }

  [PXTimeList]
  [PXInt]
  [PXUIField(DisplayName = "Time Spent", Enabled = false)]
  public virtual int? TimeSpent
  {
    get
    {
      int? nullable = this.Mon;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.Tue;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      int num1 = valueOrDefault1 + valueOrDefault2;
      nullable = this.Wed;
      int valueOrDefault3 = nullable.GetValueOrDefault();
      int num2 = num1 + valueOrDefault3;
      nullable = this.Thu;
      int valueOrDefault4 = nullable.GetValueOrDefault();
      int num3 = num2 + valueOrDefault4;
      nullable = this.Fri;
      int valueOrDefault5 = nullable.GetValueOrDefault();
      int num4 = num3 + valueOrDefault5;
      nullable = this.Sat;
      int valueOrDefault6 = nullable.GetValueOrDefault();
      int num5 = num4 + valueOrDefault6;
      nullable = this.Sun;
      int valueOrDefault7 = nullable.GetValueOrDefault();
      return new int?(num5 + valueOrDefault7);
    }
  }

  [PXTimeList]
  [PXDBInt]
  [PXUIField(DisplayName = "Sun")]
  public virtual int? Sun { get; set; }

  [PXTimeList]
  [PXDBInt]
  [PXUIField(DisplayName = "Mon")]
  public virtual int? Mon { get; set; }

  [PXTimeList]
  [PXDBInt]
  [PXUIField(DisplayName = "Tue")]
  public virtual int? Tue { get; set; }

  [PXTimeList]
  [PXDBInt]
  [PXUIField(DisplayName = "Wed")]
  public virtual int? Wed { get; set; }

  [PXTimeList]
  [PXDBInt]
  [PXUIField(DisplayName = "Thu")]
  public virtual int? Thu { get; set; }

  [PXTimeList]
  [PXDBInt]
  [PXUIField(DisplayName = "Fri")]
  public virtual int? Fri { get; set; }

  [PXTimeList]
  [PXDBInt]
  [PXUIField(DisplayName = "Sat")]
  public virtual int? Sat { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Billable")]
  public virtual bool? IsBillable
  {
    get => this._IsBillable;
    set => this._IsBillable = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
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

  [PXInt]
  [PXUIField(DisplayName = "Labor Item", Enabled = false)]
  [PXSelector(typeof (PX.Objects.IN.InventoryItem.inventoryID), SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  public virtual int? LabourClassCalc { get; set; }

  public int? GetTimeTotal(DayOfWeek day)
  {
    switch (day)
    {
      case DayOfWeek.Sunday:
        return this.Sun;
      case DayOfWeek.Monday:
        return this.Mon;
      case DayOfWeek.Tuesday:
        return this.Tue;
      case DayOfWeek.Wednesday:
        return this.Wed;
      case DayOfWeek.Thursday:
        return this.Thu;
      case DayOfWeek.Friday:
        return this.Fri;
      case DayOfWeek.Saturday:
        return this.Sat;
      default:
        return new int?();
    }
  }

  public int? GetTimeTotal()
  {
    int? nullable = this.Mon;
    int valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = this.Tue;
    int valueOrDefault2 = nullable.GetValueOrDefault();
    int num1 = valueOrDefault1 + valueOrDefault2;
    nullable = this.Wed;
    int valueOrDefault3 = nullable.GetValueOrDefault();
    int num2 = num1 + valueOrDefault3;
    nullable = this.Thu;
    int valueOrDefault4 = nullable.GetValueOrDefault();
    int num3 = num2 + valueOrDefault4;
    nullable = this.Fri;
    int valueOrDefault5 = nullable.GetValueOrDefault();
    int num4 = num3 + valueOrDefault5;
    nullable = this.Sat;
    int valueOrDefault6 = nullable.GetValueOrDefault();
    int num5 = num4 + valueOrDefault6;
    nullable = this.Sun;
    int valueOrDefault7 = nullable.GetValueOrDefault();
    return new int?(num5 + valueOrDefault7);
  }

  public virtual string ToString()
  {
    return $"{this.RateType} {this.Mon} {this.Tue} {this.Wed} {this.Thu} {this.Fri} {this.Sat} {this.Sun}";
  }

  public abstract class timeCardCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEquipmentSummary.timeCardCD>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentSummary.lineNbr>
  {
  }

  public abstract class rateType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEquipmentSummary.rateType>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentSummary.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentSummary.projectTaskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentSummary.costCodeID>
  {
  }

  public abstract class timeSpent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentSummary.timeSpent>
  {
  }

  public abstract class sun : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentSummary.sun>
  {
  }

  public abstract class mon : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentSummary.mon>
  {
  }

  public abstract class tue : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentSummary.tue>
  {
  }

  public abstract class wed : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentSummary.wed>
  {
  }

  public abstract class thu : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentSummary.thu>
  {
  }

  public abstract class fri : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentSummary.fri>
  {
  }

  public abstract class sat : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentSummary.sat>
  {
  }

  public abstract class isBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPEquipmentSummary.isBillable>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEquipmentSummary.description>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEquipmentSummary.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPEquipmentSummary.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEquipmentSummary.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEquipmentSummary.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEquipmentSummary.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPEquipmentSummary.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEquipmentSummary.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEquipmentSummary.lastModifiedDateTime>
  {
  }

  public abstract class labourClassCalc : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEquipmentSummary.labourClassCalc>
  {
  }
}
