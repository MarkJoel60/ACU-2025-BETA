// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.CostBudgetFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <exclude />
[PXCacheName("Cost Budget Filter")]
[ExcludeFromCodeCoverage]
[Serializable]
public class CostBudgetFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ProjectTaskID;

  [ProjectTask(typeof (PMProject.contractID), AlwaysEnabled = true, DirtyRead = true)]
  public virtual int? ProjectTaskID
  {
    get => this._ProjectTaskID;
    set => this._ProjectTaskID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Group by Task")]
  public virtual bool? GroupByTask { get; set; }

  public abstract class projectTaskID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  CostBudgetFilter.projectTaskID>
  {
  }

  public abstract class groupByTask : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CostBudgetFilter.groupByTask>
  {
  }
}
