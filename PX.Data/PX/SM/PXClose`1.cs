// Decompiled with JetBrains decompiler
// Type: PX.SM.PXClose`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections;

#nullable disable
namespace PX.SM;

public class PXClose<TNode>(PXGraph graph, string name) : PXAction<TNode>(graph, name) where TNode : class, IBqlTable, new()
{
  [PXUIField(DisplayName = "Close", MapEnableRights = PXCacheRights.Select)]
  [PXCancelButton(Tooltip = "Discard changes and close (Esc).")]
  protected override IEnumerable Handler(PXAdapter adapter) => base.Handler(adapter);
}
