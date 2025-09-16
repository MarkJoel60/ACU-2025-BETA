// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.DocumentList`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

public class DocumentList<T0, T1>(PXGraph Graph) : DocumentListBase<PXResult<T0, T1>>(Graph)
  where T0 : class, IBqlTable, new()
  where T1 : class, IBqlTable, new()
{
  public virtual void Add(T0 item0, T1 item1) => this.Add(new PXResult<T0, T1>(item0, item1));

  protected override object GetValue(object data, Type field)
  {
    Type type = BqlCommand.GetItemType(field);
    if (type.IsAssignableFrom(typeof (T0)))
      type = typeof (T0);
    if (type.IsAssignableFrom(typeof (T1)))
      type = typeof (T1);
    return this._Graph.Caches[type].GetValue(((PXResult) data)[type], field.Name);
  }

  public override PXResult<T0, T1> Find(object item)
  {
    if (item is T0)
    {
      PXCache cache = this._Graph.Caches[typeof (T0)];
      return this.Find((Predicate<PXResult<T0, T1>>) (data => cache.ObjectsEqual((object) PXResult<T0, T1>.op_Implicit(data), item)));
    }
    if (!(item is T1))
      throw new PXArgumentException();
    PXCache cache1 = this._Graph.Caches[typeof (T1)];
    return this.Find((Predicate<PXResult<T0, T1>>) (data => cache1.ObjectsEqual((object) PXResult<T0, T1>.op_Implicit(data), item)));
  }
}
