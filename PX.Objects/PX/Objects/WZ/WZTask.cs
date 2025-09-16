// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WZTask
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.EP;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.WZ;

[PXHidden]
[Serializable]
public class WZTask : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _ScenarioID;
  protected Guid? _TaskID;
  protected Guid? _ParentTaskID;
  protected 
  #nullable disable
  string _Name;
  protected int? _Position;
  protected int? _Order;
  protected int? _Offset;
  protected string _Type;
  protected string _Status;
  protected bool? _IsOptional;
  protected string _Details;
  protected string _ScreenID;
  protected Guid? _ImportScenarioID;
  protected int? _AssignedTo;
  protected DateTime? _AssignedDate;
  protected DateTime? _StartedDate;
  protected int? _CompletedBy;
  protected DateTime? _CompletedDate;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected Guid? _NoteID;

  [PXDBGuid(false)]
  [PXSelector(typeof (WZScenario.scenarioID), SubstituteKey = typeof (WZScenario.name))]
  [PXParent(typeof (Select<WZScenario, Where<WZScenario.scenarioID, Equal<Current<WZTask.scenarioID>>>>))]
  public virtual Guid? ScenarioID
  {
    get => this._ScenarioID;
    set => this._ScenarioID = value;
  }

  [PXDBGuid(false, IsKey = true)]
  [PXUIField]
  public virtual Guid? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [PXDBGuid(false)]
  [PXUIField]
  public virtual Guid? ParentTaskID
  {
    get => this._ParentTaskID;
    set => this._ParentTaskID = value;
  }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXDefault]
  [PXDBInt]
  public virtual int? Position
  {
    get => this._Position;
    set => this._Position = value;
  }

  [PXInt]
  public virtual int? Order
  {
    get => this._Order;
    set => this._Order = value;
  }

  [PXInt]
  public virtual int? Offset
  {
    get => this._Offset;
    set => this._Offset = value;
  }

  [PXDBString(2, IsFixed = true)]
  [WizardTaskTypes]
  [PXDefault("AR")]
  [PXUIField(DisplayName = "Type")]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXDBString(2, IsFixed = true)]
  [WizardTaskStatuses]
  [PXDefault("PN")]
  [PXUIField(DisplayName = "Status")]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsOptional
  {
    get => this._IsOptional;
    set => this._IsOptional = value;
  }

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Details")]
  public virtual string Details
  {
    get => this._Details;
    set => this._Details = value;
  }

  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField]
  [PXSelector(typeof (Search<SiteMap.screenID, Where<SiteMap.screenID, IsNotNull>>), new System.Type[] {typeof (SiteMap.nodeID), typeof (SiteMap.screenID)}, DescriptionField = typeof (SiteMap.title))]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Import Scenario")]
  [PXSelector(typeof (Search<SYMapping.mappingID, Where<SYMapping.screenID, Equal<Current<WZTask.screenID>>, And<SYMapping.mappingType, Equal<SYMapping.mappingType.typeImport>, And<SYMapping.isActive, Equal<True>>>>>), SubstituteKey = typeof (SYMapping.name))]
  public Guid? ImportScenarioID
  {
    get => this._ImportScenarioID;
    set => this._ImportScenarioID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Assigned To")]
  [PXSelector(typeof (Search2<PX.Objects.CR.Contact.contactID, LeftJoin<EPEmployee, On<EPEmployee.defContactID, Equal<PX.Objects.CR.Contact.contactID>>, LeftJoin<Users, On<Users.pKID, Equal<EPEmployee.userID>>>>, Where<Users.isHidden, Equal<False>, And<Users.isApproved, Equal<True>, And<Users.guest, NotEqual<True>>>>>), new System.Type[] {typeof (Users.username), typeof (PX.Objects.CR.Contact.displayName), typeof (Users.fullName), typeof (Users.state), typeof (EPEmployee.acctCD), typeof (EPEmployee.acctName)}, DescriptionField = typeof (PX.Objects.CR.Contact.displayName))]
  public virtual int? AssignedTo
  {
    get => this._AssignedTo;
    set => this._AssignedTo = value;
  }

  [PXDBDateAndTime]
  public virtual DateTime? AssignedDate
  {
    get => this._AssignedDate;
    set => this._AssignedDate = value;
  }

  [PXDBDateAndTime]
  [PXUIField(DisplayName = "Started", Enabled = false)]
  public virtual DateTime? StartedDate
  {
    get => this._StartedDate;
    set => this._StartedDate = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Completed By", Enabled = false)]
  [PXSelector(typeof (Search2<PX.Objects.CR.Contact.contactID, LeftJoin<EPEmployee, On<EPEmployee.defContactID, Equal<PX.Objects.CR.Contact.contactID>>, LeftJoin<Users, On<Users.pKID, Equal<EPEmployee.userID>>>>, Where<Users.isHidden, Equal<False>, And<Users.isApproved, Equal<True>, And<Users.guest, NotEqual<True>>>>>), new System.Type[] {typeof (Users.username), typeof (PX.Objects.CR.Contact.displayName), typeof (Users.fullName), typeof (Users.state), typeof (EPEmployee.acctCD), typeof (EPEmployee.acctName)}, DescriptionField = typeof (PX.Objects.CR.Contact.displayName))]
  public virtual int? CompletedBy
  {
    get => this._CompletedBy;
    set => this._CompletedBy = value;
  }

  [PXDBDateAndTime]
  [PXUIField(DisplayName = "Completed", Enabled = false)]
  public virtual DateTime? CompletedDate
  {
    get => this._CompletedDate;
    set => this._CompletedDate = value;
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

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  public abstract class scenarioID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZTask.scenarioID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZTask.taskID>
  {
  }

  public abstract class parentTaskID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZTask.parentTaskID>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WZTask.name>
  {
  }

  public abstract class position : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WZTask.position>
  {
  }

  public abstract class order : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WZTask.order>
  {
  }

  public abstract class offset : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WZTask.offset>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WZTask.type>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WZTask.status>
  {
  }

  public abstract class isOptional : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WZTask.isOptional>
  {
  }

  public abstract class details : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WZTask.details>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WZTask.screenID>
  {
  }

  public abstract class importScenarioID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZTask.importScenarioID>
  {
  }

  public abstract class assignedTo : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WZTask.assignedTo>
  {
  }

  public abstract class assignedDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  WZTask.assignedDate>
  {
  }

  public abstract class startedDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  WZTask.startedDate>
  {
  }

  public abstract class completedBy : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WZTask.completedBy>
  {
  }

  public abstract class completedDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  WZTask.completedDate>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  WZTask.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZTask.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WZTask.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    WZTask.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZTask.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WZTask.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    WZTask.lastModifiedDateTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZTask.noteID>
  {
  }
}
