// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WZTaskRelation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.WZ;

[PXHidden]
[Serializable]
public class WZTaskRelation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _ScenarioID;
  protected Guid? _TaskID;
  protected Guid? _PredecessorID;
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBGuid(false, IsKey = true)]
  [PXDBDefault(typeof (WZScenario.scenarioID))]
  [PXParent(typeof (Select<WZScenario, Where<WZScenario.scenarioID, Equal<Current<WZTaskRelation.scenarioID>>>>))]
  public virtual Guid? ScenarioID
  {
    get => this._ScenarioID;
    set => this._ScenarioID = value;
  }

  [PXDBGuid(false, IsKey = true)]
  [PXUIField]
  [PXDBDefault(typeof (WZTask.taskID))]
  [PXParent(typeof (Select<WZTask, Where<WZTask.taskID, Equal<Current<WZTaskRelation.taskID>>>>))]
  [PXSelector(typeof (Search<WZTask.taskID, Where<WZTask.scenarioID, Equal<Current<WZTaskRelation.scenarioID>>, And<WZTask.taskID, NotEqual<Current<WZTask.taskID>>>>>), SubstituteKey = typeof (WZTask.name))]
  public virtual Guid? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [PXDBGuid(false, IsKey = true)]
  [PXUIField]
  [PXDefault]
  [PXParent(typeof (Select<WZTask, Where<WZTask.taskID, Equal<Current<WZTaskRelation.predecessorID>>>>))]
  [PXSelector(typeof (Search<WZTask.taskID, Where<WZTask.scenarioID, Equal<Current<WZTaskRelation.scenarioID>>, And<WZTask.taskID, NotEqual<Current<WZTask.taskID>>>>>), SubstituteKey = typeof (WZTask.name))]
  public virtual Guid? PredecessorID
  {
    get => this._PredecessorID;
    set => this._PredecessorID = value;
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
  WZTaskRelation.scenarioID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZTaskRelation.taskID>
  {
  }

  public abstract class predecessorID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZTaskRelation.predecessorID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  WZTaskRelation.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZTaskRelation.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WZTaskRelation.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    WZTaskRelation.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    WZTaskRelation.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WZTaskRelation.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    WZTaskRelation.lastModifiedDateTime>
  {
  }
}
