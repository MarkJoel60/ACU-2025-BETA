// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.PXSyncItemSet
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CS.Email;

public sealed class PXSyncItemSet
{
  private Dictionary<int, PXSyncItemID> byItem = new Dictionary<int, PXSyncItemID>();
  private Dictionary<int, List<PXSyncItemID>> byNote = new Dictionary<int, List<PXSyncItemID>>();

  public int Count => this.byItem.Count;

  public PXSyncItemID this[string id]
  {
    get
    {
      PXSyncItemID pxSyncItemId = (PXSyncItemID) null;
      return !string.IsNullOrEmpty(id) && this.byItem.TryGetValue(id.GetHashCode(), out pxSyncItemId) ? pxSyncItemId : (PXSyncItemID) null;
    }
    set
    {
      if (id == null)
        return;
      this.byItem[id.GetHashCode()] = value;
    }
  }

  public List<PXSyncItemID> this[Guid? id]
  {
    get
    {
      List<PXSyncItemID> pxSyncItemIdList = (List<PXSyncItemID>) null;
      return id.HasValue && this.byNote.TryGetValue(id.GetHashCode(), out pxSyncItemIdList) ? pxSyncItemIdList : (List<PXSyncItemID>) null;
    }
    set
    {
      if (!id.HasValue)
        return;
      List<PXSyncItemID> pxSyncItemIdList = (List<PXSyncItemID>) null;
      if (!this.byNote.TryGetValue(id.GetHashCode(), out pxSyncItemIdList))
        this.byNote[id.GetHashCode()] = pxSyncItemIdList = new List<PXSyncItemID>();
      pxSyncItemIdList.AddRange((IEnumerable<PXSyncItemID>) value);
    }
  }

  public PXSyncItemSet(IEnumerable<PXSyncItemID> collection)
  {
    if (collection == null)
      return;
    foreach (PXSyncItemID pxSyncItemId in collection)
      this.Add(pxSyncItemId);
  }

  public bool NeedProcessItem(string address, string itemid, Guid? noteid)
  {
    if (itemid == null || !noteid.HasValue)
      return false;
    List<PXSyncItemID> source = (List<PXSyncItemID>) null;
    return this.byNote.TryGetValue(noteid.GetHashCode(), out source) && source != null && source.Count > 0 && (source.Any<PXSyncItemID>((Func<PXSyncItemID, bool>) (i => i.NeedProcess())) || !(source.First<PXSyncItemID>().Address == address));
  }

  public void Add(PXSyncItemID item)
  {
    if (item == null || item.ItemID == null || this.byItem.ContainsKey(item.ItemID.GetHashCode()))
      return;
    this.byItem[item.ItemID.GetHashCode()] = item;
    List<PXSyncItemID> pxSyncItemIdList1 = (List<PXSyncItemID>) null;
    Dictionary<int, List<PXSyncItemID>> byNote1 = this.byNote;
    Guid? noteId = item.NoteID;
    int hashCode1 = noteId.GetHashCode();
    ref List<PXSyncItemID> local = ref pxSyncItemIdList1;
    if (!byNote1.TryGetValue(hashCode1, out local))
    {
      Dictionary<int, List<PXSyncItemID>> byNote2 = this.byNote;
      noteId = item.NoteID;
      int hashCode2 = noteId.GetHashCode();
      List<PXSyncItemID> pxSyncItemIdList2;
      pxSyncItemIdList1 = pxSyncItemIdList2 = new List<PXSyncItemID>();
      byNote2[hashCode2] = pxSyncItemIdList2;
    }
    pxSyncItemIdList1.Add(item);
  }

  public void Clear()
  {
    this.byItem.Clear();
    this.byNote.Clear();
  }

  public bool Contains(PXSyncItemID item)
  {
    return this.Contains(item.ItemID) || item.NoteID.HasValue && this.Contains(new Guid?(item.NoteID.Value));
  }

  public bool Contains(string code)
  {
    return !string.IsNullOrEmpty(code) && this.byItem.ContainsKey(code.GetHashCode());
  }

  public bool Contains(Guid? code) => code.HasValue && this.byNote.ContainsKey(code.GetHashCode());

  public HashSet<string> ToHashSet()
  {
    return new HashSet<string>(this.byItem.Values.Select<PXSyncItemID, string>((Func<PXSyncItemID, string>) (i => i.ItemID)));
  }
}
