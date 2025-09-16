// Decompiled with JetBrains decompiler
// Type: PX.Data.SyScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class SyScope : IDisposable
{
  private bool _Disposed;
  private bool _Export;
  private bool _ContractBasedAPI;

  public SyScope(bool export)
    : this(export, false)
  {
  }

  public SyScope(bool export, bool contractBasedAPI)
  {
    this._Export = export;
    this._ContractBasedAPI = contractBasedAPI;
    PXContext.SetSlot<SyScope>(this);
  }

  public void Dispose()
  {
    if (this._Disposed)
      return;
    PXContext.SetSlot<SyScope>((SyScope) null);
    this._Disposed = true;
  }

  public static bool IsScoped(out bool export) => SyScope.IsScoped(out export, out bool _);

  public static bool IsScoped(out bool export, out bool contractBasedAPI)
  {
    SyScope slot = PXContext.GetSlot<SyScope>();
    if (slot == null)
    {
      export = false;
      contractBasedAPI = false;
      return false;
    }
    export = slot._Export;
    contractBasedAPI = slot._ContractBasedAPI;
    return true;
  }
}
