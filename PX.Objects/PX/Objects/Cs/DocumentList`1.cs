// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.DocumentList`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

public class DocumentList<T>(PXGraph Graph) : DocumentListBase<T>(Graph) where T : class, IBqlTable
{
  public bool Consolidate = true;

  protected override object GetValue(object data, Type field)
  {
    return !this.Consolidate ? (object) null : this._Graph.Caches[typeof (T)].GetValue(data, field.Name);
  }

  public override T Find(object item)
  {
    PXCache cache = this._Graph.Caches[typeof (T)];
    return this.Find((Predicate<T>) (data => cache.ObjectsEqual((object) data, item)));
  }

  public virtual void AddOrReplace(T item)
  {
    PXCache cache = this._Graph.Caches[typeof (T)];
    int index = this.FindIndex((Predicate<T>) (data => cache.ObjectsEqual((object) data, (object) item)));
    if (index < 0)
      this.Add(item);
    else
      this[index] = item;
  }
}
