// Decompiled with JetBrains decompiler
// Type: PX.Translation.RipAssemblyScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Translation;

public class RipAssemblyScope : IDisposable
{
  private readonly bool _isScoped;

  public static bool IsScoped
  {
    get
    {
      RipAssemblyScope slot = PXContext.GetSlot<RipAssemblyScope>();
      return slot != null && slot._isScoped;
    }
  }

  public RipAssemblyScope()
  {
    this._isScoped = true;
    PXContext.SetSlot<RipAssemblyScope>(this);
  }

  public void Dispose() => PXContext.SetSlot<RipAssemblyScope>((RipAssemblyScope) null);
}
