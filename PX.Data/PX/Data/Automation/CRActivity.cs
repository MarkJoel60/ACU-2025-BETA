// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.CRActivity
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.EP;
using System;

#nullable enable
namespace PX.Data.Automation;

[PXHidden]
[Serializable]
public class CRActivity : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssign
{
  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXSequentialNote(SuppressActivitiesCount = true, IsKey = true)]
  [PXUIField(DisplayName = "ID")]
  [PXSelector(typeof (CRActivity.noteID), new System.Type[] {typeof (CRActivity.noteID)})]
  public virtual Guid? NoteID { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Task")]
  [PXSelector(typeof (Search<CRActivity.noteID>), new System.Type[] {typeof (CRActivity.subject), typeof (CRActivity.priority), typeof (CRActivity.startDate), typeof (CRActivity.endDate)})]
  public virtual Guid? ParentNoteID { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "References Nbr.")]
  public virtual Guid? RefNoteID { get; set; }

  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Related Entity Description", Enabled = false)]
  public virtual 
  #nullable disable
  string Source { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Class", Visibility = PXUIVisibility.SelectorVisible)]
  [PXFieldDescription]
  public virtual int? ClassID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  public virtual string ClassInfo { get; set; }

  [PXDBString(5, IsFixed = true, IsUnicode = false)]
  [PXUIField(DisplayName = "Type", Required = true)]
  public virtual string Type { get; set; }

  [PXDBString(255 /*0xFF*/, InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Summary", Visibility = PXUIVisibility.SelectorVisible)]
  [PXFieldDescription]
  public virtual string Subject { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Location")]
  public virtual string Location { get; set; }

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Activity Details")]
  public virtual string Body { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Priority")]
  [PXDefault(1, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"Low", "Normal", "High"})]
  public virtual int? Priority { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Status")]
  public virtual string UIStatus { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Category")]
  public virtual int? CategoryID { get; set; }

  [PXUIField(DisplayName = "All Day")]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? AllDay { get; set; }

  [PXDefault]
  [PXDBDate(InputMask = "g", PreserveTime = true, UseTimeZone = true)]
  [PXUIField(DisplayName = "Start Time")]
  public virtual System.DateTime? StartDate { get; set; }

  [PXDBDate(InputMask = "g", PreserveTime = true, UseTimeZone = true)]
  [PXUIField(DisplayName = "End Time")]
  public virtual System.DateTime? EndDate { get; set; }

  [PXDBDate(InputMask = "g", PreserveTime = true)]
  [PXUIField(DisplayName = "Completed At", Enabled = false)]
  public virtual System.DateTime? CompletedDate { get; set; }

  [PXInt]
  public virtual int? DayOfWeek
  {
    [PXDependsOnFields(new System.Type[] {typeof (CRActivity.startDate)})] get
    {
      System.DateTime? startDate = this.StartDate;
      return !startDate.HasValue ? new int?() : new int?((int) startDate.Value.DayOfWeek);
    }
  }

  [PXDBInt(MinValue = 0, MaxValue = 100)]
  [PXDefault(0, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Completion (%)")]
  public virtual int? PercentCompletion { get; set; }

  [PXDBGuid(false)]
  [PXChildUpdatable(AutoRefresh = true)]
  [PXUIField(DisplayName = "Owner")]
  [PXFormula(typeof (Default<CRActivity.workgroupID>))]
  public virtual int? OwnerID { get; set; }

  [PXDBInt]
  [PXChildUpdatable(UpdateRequest = true)]
  [PXUIField(DisplayName = "Workgroup")]
  public virtual int? WorkgroupID { get; set; }

  [PXDBBool]
  [PXUIField(Visible = false)]
  public virtual bool? IsExternal { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Internal")]
  public virtual bool? IsPrivate { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Incoming")]
  public virtual bool? Incoming { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Outgoing")]
  public virtual bool? Outgoing { get; set; }

  [PXDBBool]
  [PXDefault(true, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Synchronize")]
  public virtual bool? Synchronize { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Business Account")]
  public virtual int? BAccountID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Contact")]
  public virtual int? ContactID { get; set; }

  [PXString(InputMask = "")]
  [PXUIField(DisplayName = "Entity", Visibility = PXUIVisibility.SelectorVisible, Enabled = false, IsReadOnly = true)]
  public virtual string EntityDescription { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Show As")]
  public virtual int? ShowAsID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Locked")]
  public virtual bool? IsLocked { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DeletedDatabaseRecord { get; set; }

  [PXDBCreatedByID(DontOverrideValue = true)]
  [PXUIField(DisplayName = "Created By", Enabled = false)]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Created At", Enabled = false)]
  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp(RecordComesFirst = true)]
  public virtual byte[] tstamp { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRActivity.selected>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRActivity.noteID>
  {
  }

  public abstract class parentNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRActivity.parentNoteID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRActivity.refNoteID>
  {
  }

  public abstract class source : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.source>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.classID>
  {
  }

  public abstract class classInfo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.classInfo>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.type>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.subject>
  {
  }

  public abstract class location : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.location>
  {
  }

  public abstract class body : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.body>
  {
  }

  public abstract class priority : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.priority>
  {
  }

  public abstract class uistatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRActivity.uistatus>
  {
  }

  public abstract class categoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.categoryID>
  {
  }

  public abstract class allDay : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRActivity.allDay>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  CRActivity.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  CRActivity.endDate>
  {
  }

  public abstract class completedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    CRActivity.completedDate>
  {
  }

  public abstract class dayOfWeek : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.dayOfWeek>
  {
  }

  public abstract class percentCompletion : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.percentCompletion>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.ownerID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.workgroupID>
  {
  }

  public abstract class isExternal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRActivity.isExternal>
  {
  }

  public abstract class isPrivate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRActivity.isPrivate>
  {
  }

  public abstract class incoming : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRActivity.incoming>
  {
  }

  public abstract class outgoing : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRActivity.outgoing>
  {
  }

  public abstract class synchronize : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRActivity.synchronize>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.bAccountID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.contactID>
  {
  }

  public abstract class entityDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRActivity.entityDescription>
  {
  }

  public abstract class showAsID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRActivity.showAsID>
  {
  }

  public abstract class isLocked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRActivity.isLocked>
  {
  }

  public abstract class deletedDatabaseRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRActivity.deletedDatabaseRecord>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRActivity.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRActivity.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    CRActivity.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRActivity.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRActivity.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    CRActivity.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRActivity.Tstamp>
  {
  }
}
