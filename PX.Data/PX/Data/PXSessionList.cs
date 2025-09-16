// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSessionList
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

internal static class PXSessionList
{
  public static readonly Dictionary<string, PXSessionListItem> Items = new Dictionary<string, PXSessionListItem>();

  public static PXSessionListItem Add(PXSessionListItem it)
  {
    lock (PXSessionList.Items)
    {
      PXSessionListItem pxSessionListItem;
      if (PXSessionList.Items.TryGetValue(it.ItemID, out pxSessionListItem))
      {
        pxSessionListItem.DateModified = new System.DateTime?(System.DateTime.Now);
        pxSessionListItem.User = it.User;
        return pxSessionListItem;
      }
      PXSessionList.Items.Add(it.ItemID, it);
      return it;
    }
  }
}
