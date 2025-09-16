// Decompiled with JetBrains decompiler
// Type: PX.SM.SelectedTreeNode
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
[Serializable]
public class SelectedTreeNode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _NodeID;

  [PXDBGuid(false)]
  public virtual Guid? NodeID
  {
    get => this._NodeID;
    set => this._NodeID = value;
  }

  /// <exclude />
  public abstract class nodeID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  SelectedTreeNode.nodeID>
  {
  }
}
