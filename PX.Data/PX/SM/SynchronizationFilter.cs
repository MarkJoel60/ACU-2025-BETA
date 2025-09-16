// Decompiled with JetBrains decompiler
// Type: PX.SM.SynchronizationFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
[PXPrimaryGraph(typeof (SynchronizationProcess))]
[Serializable]
public class SynchronizationFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Operation;

  [PXDBString(1, IsFixed = true)]
  [PXDefault("D")]
  [PXUIField(DisplayName = "Operation")]
  [FileSyncOperations.List]
  public virtual string Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  /// <exclude />
  public abstract class operation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SynchronizationFilter.operation>
  {
  }
}
