// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GISelectedOnlyScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.Maintenance.GI;

/// <summary>
/// This scope defines that only records with "Selected" property set to true must be selected.
/// </summary>
public sealed class GISelectedOnlyScope : IDisposable
{
  private const string _key = "GISelectedOnly";
  private bool _disposed;

  public GISelectedOnlyScope() => PXContext.SetSlot<bool>("GISelectedOnly", true);

  public static bool IsDefined => PXContext.GetSlot<bool>("GISelectedOnly");

  public void Dispose()
  {
    if (this._disposed)
      return;
    PXContext.SetSlot<bool>("GISelectedOnly", false);
    this._disposed = true;
  }
}
