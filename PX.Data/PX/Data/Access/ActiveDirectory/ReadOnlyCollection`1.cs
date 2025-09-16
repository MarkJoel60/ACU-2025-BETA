// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.ReadOnlyCollection`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

[PXInternalUseOnly]
[Serializable]
public sealed class ReadOnlyCollection<T> : ReadOnlyCollectionBase, IEnumerable<T>, IEnumerable
{
  public ReadOnlyCollection(IEnumerable<T> data)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    foreach (T obj in data)
      this.InnerList.Add((object) obj);
  }

  public string ToString(string separator)
  {
    if (this.InnerList.Count == 0)
      return string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    foreach (object inner in this.InnerList)
    {
      if (stringBuilder.Length > 0)
        stringBuilder.Append(separator);
      stringBuilder.Append(inner.ToString());
    }
    return stringBuilder.ToString();
  }

  public T this[int index]
  {
    get => index < 0 || index >= this.InnerList.Count ? default (T) : (T) this.InnerList[index];
  }

  public IEnumerator<T> GetEnumerator()
  {
    foreach (T inner in this.InnerList)
      yield return inner;
  }
}
