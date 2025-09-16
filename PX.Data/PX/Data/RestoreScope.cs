// Decompiled with JetBrains decompiler
// Type: PX.Data.RestoreScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public class RestoreScope : IDisposable
{
  private List<RestoreScope.IRestorable> _ro = new List<RestoreScope.IRestorable>();

  public RestoreScope RestoreTo<T>(System.Action<T> set, T to)
  {
    this._ro.Add((RestoreScope.IRestorable) new RestoreScope.Restorable<T>()
    {
      setter = set,
      restoreTo = to
    });
    return this;
  }

  void IDisposable.Dispose()
  {
    foreach (RestoreScope.IRestorable restorable in this._ro)
      restorable.Restore();
  }

  private interface IRestorable
  {
    void Restore();
  }

  private class Restorable<T> : RestoreScope.IRestorable
  {
    public T restoreTo;
    public System.Action<T> setter;

    public void Restore() => this.setter(this.restoreTo);
  }
}
