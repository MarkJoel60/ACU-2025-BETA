// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WZTaskTreeItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.WZ;

[PXVirtual]
[PXHidden]
[Serializable]
public class WZTaskTreeItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _ScenarioID;
  protected Guid? _TaskID;
  protected Guid? _ParentTaskID;
  protected 
  #nullable disable
  string _Name;
  protected int? _Position;

  [PXDBGuid(false)]
  public virtual Guid? ScenarioID
  {
    get => this._ScenarioID;
    set => this._ScenarioID = value;
  }

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [PXDBGuid(false)]
  public virtual Guid? ParentTaskID
  {
    get => this._ParentTaskID;
    set => this._ParentTaskID = value;
  }

  [PXDBString(100, IsUnicode = true)]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXDBInt]
  public virtual int? Position
  {
    get => this._Position;
    set => this._Position = value;
  }

  public abstract class scenarioID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZTaskTreeItem.scenarioID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZTaskTreeItem.taskID>
  {
  }

  public abstract class parentTaskID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZTaskTreeItem.parentTaskID>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WZTaskTreeItem.name>
  {
  }

  public abstract class position : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WZTaskTreeItem.position>
  {
  }
}
