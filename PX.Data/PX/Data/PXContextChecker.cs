// Decompiled with JetBrains decompiler
// Type: PX.Data.PXContextChecker
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Context;
using System;

#nullable disable
namespace PX.Data;

internal sealed class PXContextChecker : IDisposable
{
  private readonly string _contextCheckKey;

  public PXContextChecker(string prefix)
  {
    this._contextCheckKey = $"{nameof (PXContextChecker)}:{prefix}:{Guid.NewGuid():N}";
    SlotStore.Instance.Set(this._contextCheckKey, (object) this);
  }

  internal void CheckContext()
  {
    if (SlotStore.Instance.Get<PXContextChecker>(this._contextCheckKey) != this)
      throw new InvalidOperationException("PXContext changed");
  }

  public void Dispose() => SlotStore.Instance.Remove(this._contextCheckKey);
}
