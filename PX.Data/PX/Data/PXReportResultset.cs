// Decompiled with JetBrains decompiler
// Type: PX.Data.PXReportResultset
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
[Serializable]
public class PXReportResultset : IPXResultset, IPXResultsetBase, IEnumerable, IEnumerable<object[]>
{
  protected readonly System.Type[] _ItemTypes;
  protected readonly List<object[]> _Items;

  public System.Type GetItemType(int i)
  {
    if (i < 0 || i >= this._ItemTypes.Length)
      throw new PXArgumentException(nameof (i), "The argument is out of range.");
    return this._ItemTypes[i];
  }

  public object GetItem(int rowNbr, int i)
  {
    if (rowNbr < 0 || rowNbr >= this._Items.Count)
      throw new PXArgumentException(nameof (rowNbr), "The argument is out of range.");
    if (i < 0 || i >= this._Items[rowNbr].Length)
      throw new PXArgumentException(nameof (i), "The argument is out of range.");
    return this._Items[rowNbr][i];
  }

  public int GetTableCount()
  {
    System.Type[] itemTypes = this._ItemTypes;
    return itemTypes == null ? 0 : itemTypes.Length;
  }

  public int GetRowCount()
  {
    List<object[]> items = this._Items;
    // ISSUE: explicit non-virtual call
    return items == null ? 0 : __nonvirtual (items.Count);
  }

  public object GetCollection() => (object) this._Items;

  public System.Type GetCollectionType() => (System.Type) null;

  public PXDelayedQuery GetDelayedQuery() => (PXDelayedQuery) null;

  public PXReportResultset(params System.Type[] itemTypes)
  {
    this._ItemTypes = itemTypes;
    this._Items = new List<object[]>();
  }

  public PXReportResultset(IEnumerable items)
  {
    object[] array = items != null ? items.ToArray<object>() : (object[]) null;
    if (array == null || !((IEnumerable<object>) array).Any<object>())
      return;
    object obj = ((IEnumerable<object>) array).First<object>();
    System.Type[] typeArray;
    if (!(obj is PXResult pxResult1))
      typeArray = new System.Type[1]{ obj.GetType() };
    else
      typeArray = pxResult1.Tables;
    this._ItemTypes = typeArray;
    this._Items = ((IEnumerable<object>) array).Select<object, object[]>((Func<object, object[]>) (item =>
    {
      if (item is PXResult pxResult3)
        return pxResult3.Items;
      return new object[1]{ item };
    })).ToList<object[]>();
  }

  public void Add(params object[] items) => this._Items.Add(items);

  public void Add(PXResult result) => this._Items.Add(result.Items);

  public IEnumerator<object[]> GetEnumerator()
  {
    return (IEnumerator<object[]>) this._Items.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
}
