// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPreserveScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class PXPreserveScope : IDisposable
{
  private bool _Disposed;
  private bool _DoNotLoad;
  private bool _PageGeneratorRequest;
  private PXPreserveScope _Previous;

  public PXPreserveScope()
  {
    this._Previous = PXContext.GetSlot<PXPreserveScope>();
    if (this._Previous != null)
    {
      this._DoNotLoad = this._Previous._DoNotLoad;
      this._PageGeneratorRequest = this._Previous._PageGeneratorRequest;
    }
    PXContext.SetSlot<PXPreserveScope>(this);
  }

  public PXPreserveScope(bool doNotLoad)
    : this()
  {
    this._DoNotLoad = doNotLoad;
  }

  public PXPreserveScope(bool pageGeneratorRequest, bool doNotLoad)
    : this(pageGeneratorRequest | doNotLoad)
  {
    this._PageGeneratorRequest = pageGeneratorRequest;
  }

  public void Dispose()
  {
    if (this._Disposed)
      return;
    PXContext.SetSlot<PXPreserveScope>(this._Previous);
    this._Disposed = true;
  }

  public static bool IsScoped()
  {
    PXPreserveScope slot = PXContext.GetSlot<PXPreserveScope>();
    return slot != null && !slot._DoNotLoad;
  }

  public static bool IsPageGeneratorRequest()
  {
    PXPreserveScope slot = PXContext.GetSlot<PXPreserveScope>();
    return slot != null && slot._PageGeneratorRequest;
  }

  public static bool IsFullTrust() => PXContext.GetSlot<PXPreserveScope>() == null;
}
