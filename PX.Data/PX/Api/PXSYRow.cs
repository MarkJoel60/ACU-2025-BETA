// Decompiled with JetBrains decompiler
// Type: PX.Api.PXSYRow
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Monads;

#nullable disable
namespace PX.Api;

public class PXSYRow : IEnumerable<string>, IEnumerable
{
  private readonly PXSYTable _Parent;
  private readonly PXSYItem[] _Items;

  [PXInternalUseOnly]
  public PXSYItem[] Items => this._Items;

  public Dictionary<string, string> Keys { get; set; }

  public bool IsProcessed { get; set; }

  public PXSYTable Parent => this._Parent;

  public string this[int index]
  {
    get
    {
      PXSYItem pxsyItem = this.GetItem(index);
      return pxsyItem != null ? pxsyItem.Value : string.Empty;
    }
    set => this.SetItem(index, new PXSYItem(value));
  }

  public string this[string column]
  {
    get
    {
      PXSYItem pxsyItem = this.GetItem(column);
      return pxsyItem != null ? pxsyItem.Value : string.Empty;
    }
    set => this.SetItem(column, new PXSYItem(value));
  }

  public IEnumerator<string> GetEnumerator()
  {
    PXSYItem[] pxsyItemArray = this._Items;
    for (int index = 0; index < pxsyItemArray.Length; ++index)
    {
      PXSYItem pxsyItem = pxsyItemArray[index];
      yield return pxsyItem == null ? (string) null : pxsyItem.Value;
    }
    pxsyItemArray = (PXSYItem[]) null;
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  public PXSYItem GetItem(int index) => this._Items[index];

  public void SetItem(int index, PXSYItem item) => this._Items[index] = item;

  public PXSYItem GetItem(string column)
  {
    int index = this._Parent.IndexOfColumn(column);
    return index >= 0 ? this._Items[index] : throw new PXException("The column '{0}' is not found in the data set.", new object[1]
    {
      (object) column
    });
  }

  public void SetItem(string column, PXSYItem item)
  {
    this._Items[this._Parent.IndexOfColumn(column)] = item;
  }

  public PXSYRow(PXSYTable table)
  {
    this._Parent = table;
    this._Items = new PXSYItem[this._Parent.ColumnsCount];
  }

  public int Count => this._Items.Length;

  public string[] ToArray()
  {
    string[] array = new string[this.Count];
    for (int index = 0; index < this.Count; ++index)
      array[index] = MaybeObjects.Return<PXSYItem, string>(this._Items[index], (Func<PXSYItem, string>) (c =>
      {
        PXFieldState state = c.State;
        return (state != null ? (state.Visibility == PXUIVisibility.HiddenByAccessRights ? 1 : 0) : 0) == 0 ? c.Value : string.Empty;
      }), string.Empty);
    return array;
  }
}
