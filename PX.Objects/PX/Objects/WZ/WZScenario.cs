// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WZScenario
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.SM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.WZ;

[PXPrimaryGraph(typeof (WZScenarioEntry))]
[PXCacheName("Wizard Scenario")]
[TableAndChartDashboardType]
[PXHidden]
[Serializable]
public class WZScenario : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssign
{
  protected Guid? _ScenarioID;
  protected 
  #nullable disable
  string _Name;
  protected string _Status;
  protected DateTime? _ExecutionDate;
  protected string _ExecutionPeriodID;
  protected string _Rolename;
  protected Guid? _NodeID;
  protected string _ScheduleID;
  protected bool? _Scheduled;
  protected int? _ScenarioOrder;
  protected int? _AssignmentMapID;
  protected int? _WorkgroupID;
  protected int? _OwnerID;
  protected string _TasksCompleted;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBGuid(false, IsKey = true)]
  [PXUIField]
  public virtual Guid? ScenarioID
  {
    get => this._ScenarioID;
    set => this._ScenarioID = value;
  }

  [PXDBString(50, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXDBString(2, IsFixed = true)]
  [WizardScenarioStatuses]
  [PXDefault("PN")]
  [PXUIField]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBDateAndTime]
  [PXUIField]
  public virtual DateTime? ExecutionDate
  {
    get => this._ExecutionDate;
    set => this._ExecutionDate = value;
  }

  [FinPeriodID(typeof (WZScenario.executionDate), null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXDefault]
  [PXUIField(DisplayName = "Execution Period", Enabled = false)]
  public virtual string ExecutionPeriodID
  {
    get => this._ExecutionPeriodID;
    set => this._ExecutionPeriodID = value;
  }

  [PXDBString(64 /*0x40*/, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<Roles.rolename, Where<Roles.guest, Equal<False>>>), DescriptionField = typeof (Roles.descr))]
  public virtual string Rolename
  {
    get => this._Rolename;
    set => this._Rolename = value;
  }

  [PXDBGuid(false)]
  [PXDefault]
  [PXUIField]
  public virtual Guid? NodeID
  {
    get => this._NodeID;
    set => this._NodeID = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXParent(typeof (Select<Schedule, Where<Schedule.scheduleID, Equal<Current<WZScenario.scheduleID>>>>), LeaveChildren = true)]
  public virtual string ScheduleID
  {
    get => this._ScheduleID;
    set => this._ScheduleID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Scheduled
  {
    get => this._Scheduled;
    set => this._Scheduled = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Order")]
  public virtual int? ScenarioOrder
  {
    get => this._ScenarioOrder;
    set => this._ScenarioOrder = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search<EPAssignmentMap.assignmentMapID, Where<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeImplementationScenario>>>), SubstituteKey = typeof (EPAssignmentMap.name))]
  [PXUIField(DisplayName = "Assignment Map")]
  public virtual int? AssignmentMapID
  {
    get => this._AssignmentMapID;
    set => this._AssignmentMapID = value;
  }

  [PXInt]
  [PXSelector(typeof (Search<EPCompanyTree.workGroupID>), SubstituteKey = typeof (EPCompanyTree.description))]
  [PXUIField(DisplayName = "Workgroup ID", Enabled = false)]
  public virtual int? WorkgroupID
  {
    get => this._WorkgroupID;
    set => this._WorkgroupID = value;
  }

  [Owner(IsDBField = false, DisplayName = "Assignee", Enabled = false)]
  public virtual int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Tasks Completed")]
  public virtual string TasksCompleted
  {
    get => this._TasksCompleted;
    set => this._TasksCompleted = value;
  }

  int? IAssign.WorkgroupID
  {
    get => this.WorkgroupID;
    set => this.WorkgroupID = value;
  }

  int? IAssign.OwnerID
  {
    get => this.OwnerID;
    set => this.OwnerID = value;
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

  public abstract class scenarioID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZScenario.scenarioID>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WZScenario.name>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WZScenario.status>
  {
  }

  public abstract class executionDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    WZScenario.executionDate>
  {
  }

  public abstract class executionPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WZScenario.executionPeriodID>
  {
  }

  public abstract class rolename : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WZScenario.rolename>
  {
  }

  public abstract class nodeID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZScenario.nodeID>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WZScenario.scheduleID>
  {
  }

  public abstract class scheduled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WZScenario.scheduled>
  {
  }

  public abstract class scenarioOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WZScenario.scenarioOrder>
  {
  }

  public abstract class assignmentMapID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WZScenario.assignmentMapID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WZScenario.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WZScenario.ownerID>
  {
  }

  public abstract class tasksCompleted : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WZScenario.tasksCompleted>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  WZScenario.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZScenario.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WZScenario.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    WZScenario.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZScenario.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WZScenario.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    WZScenario.lastModifiedDateTime>
  {
  }
}
