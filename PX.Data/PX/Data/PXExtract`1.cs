// Decompiled with JetBrains decompiler
// Type: PX.Data.PXExtract`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXExtract<TNode> : PXArchiveMoveBase<TNode> where TNode : class, IBqlTable, new()
{
  protected override bool MoveToArchive => false;

  /// <inheritdoc />
  public PXExtract(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  /// <inheritdoc />
  public PXExtract(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  [PXUIField(DisplayName = "Extract", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXExtractButton]
  protected override IEnumerable Handler(PXAdapter adapter) => base.Handler(adapter);
}
