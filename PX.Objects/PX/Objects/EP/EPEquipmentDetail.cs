// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEquipmentDetail
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

[PXCacheName("Equipment Time Card Detail")]
[Serializable]
public class EPEquipmentDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _Date;
  protected 
  #nullable disable
  string _Description;
  protected int? _ProjectID;
  protected int? _RunTime;
  protected int? _SetupTime;
  protected int? _SuspendTime;
  protected bool? _IsBillable;
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
  [PXParent(typeof (Select<EPEquipmentTimeCard, Where<EPEquipmentTimeCard.timeCardCD, Equal<Current<EPEquipmentDetail.timeCardCD>>>>))]
  public virtual string TimeCardCD { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (EPEquipmentTimeCard.detailLineCntr))]
  [PXUIField(Visible = false)]
  public virtual int? LineNbr { get; set; }

  [PXDBInt]
  public virtual int? SetupSummaryLineNbr { get; set; }

  [PXDBInt]
  public virtual int? RunSummaryLineNbr { get; set; }

  [PXDBInt]
  public virtual int? SuspendSummaryLineNbr { get; set; }

  [PXDBInt]
  public virtual int? OrigLineNbr { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Date")]
  public virtual DateTime? Date
  {
    get => this._Date;
    set => this._Date = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDefault(typeof (PMProject.contractID))]
  [EPEquipmentActiveProject]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<EPEquipmentDetail.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [EPTimecardProjectTask(typeof (EPEquipmentDetail.projectID), "TA", DisplayName = "Project Task")]
  [PXForeignReference(typeof (CompositeKey<Field<EPEquipmentDetail.projectID>.IsRelatedTo<PMTask.projectID>, Field<EPEquipmentDetail.projectTaskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? ProjectTaskID { get; set; }

  [CostCode(null, typeof (EPEquipmentDetail.projectTaskID), "E", DescriptionField = typeof (PMCostCode.description))]
  public virtual int? CostCodeID { get; set; }

  [PXDBInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Run Time")]
  public virtual int? RunTime
  {
    get => this._RunTime;
    set => this._RunTime = value;
  }

  [PXDBInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Setup Time")]
  public virtual int? SetupTime
  {
    get => this._SetupTime;
    set => this._SetupTime = value;
  }

  [PXDBInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Suspend Time")]
  public virtual int? SuspendTime
  {
    get => this._SuspendTime;
    set => this._SuspendTime = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Billable")]
  public virtual bool? IsBillable
  {
    get => this._IsBillable;
    set => this._IsBillable = value;
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

  public abstract class timeCardCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEquipmentDetail.timeCardCD>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentDetail.lineNbr>
  {
  }

  public abstract class setupSummarylineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEquipmentDetail.setupSummarylineNbr>
  {
  }

  public abstract class runSummarylineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEquipmentDetail.runSummarylineNbr>
  {
  }

  public abstract class suspendSummarylineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEquipmentDetail.suspendSummarylineNbr>
  {
  }

  public abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentDetail.origLineNbr>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EPEquipmentDetail.date>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEquipmentDetail.description>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentDetail.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentDetail.projectTaskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentDetail.costCodeID>
  {
  }

  public abstract class runTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentDetail.runTime>
  {
  }

  public abstract class setupTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentDetail.setupTime>
  {
  }

  public abstract class suspendTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentDetail.suspendTime>
  {
  }

  public abstract class isBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPEquipmentDetail.isBillable>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEquipmentDetail.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPEquipmentDetail.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEquipmentDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEquipmentDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEquipmentDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPEquipmentDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEquipmentDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEquipmentDetail.lastModifiedDateTime>
  {
  }
}
