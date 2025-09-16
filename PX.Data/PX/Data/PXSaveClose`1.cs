// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSaveClose`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXSaveClose<TNode>(PXGraph graph, string name) : PXAction<TNode>(graph, name) where TNode : class, IBqlTable, new()
{
  [PXUIField(DisplayName = "Save & Close", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXSaveCloseButton(ClosePopup = true)]
  protected override IEnumerable Handler(PXAdapter adapter)
  {
    PXSaveClose<TNode> caller = this;
    if (!adapter.InternalCall && !adapter.View.Cache.AllowUpdate)
      throw new PXException("The record cannot be saved.");
    foreach (object obj in adapter.Get())
      yield return obj;
    caller._Graph.Actions.PressSave((PXAction) caller);
  }
}
