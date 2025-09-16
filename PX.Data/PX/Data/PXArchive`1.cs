// Decompiled with JetBrains decompiler
// Type: PX.Data.PXArchive`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Archiving;
using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXArchive<TNode> : PXArchiveMoveBase<TNode> where TNode : class, IBqlTable, new()
{
  protected override bool MoveToArchive => true;

  /// <inheritdoc />
  public PXArchive(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  /// <inheritdoc />
  public PXArchive(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  [PXUIField(DisplayName = "Archive", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXArchiveButton]
  protected override IEnumerable Handler(PXAdapter adapter) => base.Handler(adapter);

  public override object GetState(object row)
  {
    object state = base.GetState(row);
    if (state is PXButtonState pxButtonState && (pxButtonState.Enabled || pxButtonState.Visible))
    {
      ArchiveInfoHelper instance = ArchiveInfoHelper.Instance;
      if (instance.GetPolicyFor(typeof (TNode)) == null)
      {
        pxButtonState.Enabled = false;
        pxButtonState.Visible = false;
      }
      if (pxButtonState.Enabled)
      {
        PXCache cache = (PXCache) this._Graph.Caches<TNode>();
        System.DateTime? readyDate;
        if (cache != null && cache.Current != null && !instance.IsReadyToBeArchived(cache, cache.Current, out readyDate))
        {
          pxButtonState.Enabled = false;
          if (!readyDate.HasValue)
            pxButtonState.Tooltip = PXMessages.LocalizeFormatNoPrefixNLA("The {0} record cannot be archived due to incomplete processing.", (object) cache.GetName());
          else
            pxButtonState.Tooltip = PXMessages.LocalizeFormatNoPrefixNLA("The {0} record can be archived starting from {1}.", (object) cache.GetName(), (object) readyDate.Value.ToShortDateString());
        }
      }
    }
    return state;
  }

  [PXLocalizable]
  public static class Msg
  {
    public const string NotReady = "The {0} record can be archived starting from {1}.";
    public const string ImproperState = "The {0} record cannot be archived due to incomplete processing.";
  }
}
