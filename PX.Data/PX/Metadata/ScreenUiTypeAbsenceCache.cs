// Decompiled with JetBrains decompiler
// Type: PX.Metadata.ScreenUiTypeAbsenceCache
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Caching;
using System;
using System.Collections.Concurrent;

#nullable disable
namespace PX.Metadata;

/// <summary>
/// A cache containing information about the absence of certain UI types of screens.
/// It’s used in the mobile app to support screens implemented only in the modern UI.
/// </summary>
internal class ScreenUiTypeAbsenceCache : ICacheControlledBy<ScreenUiTypeAbsence>, ICacheControl
{
  private readonly ConcurrentDictionary<string, ScreenUiTypeAbsence> _absences = new ConcurrentDictionary<string, ScreenUiTypeAbsence>();

  public void SetAbsence(string screenId, ScreenUiTypeAbsence absence)
  {
    screenId = screenId.ToUpperInvariant();
    int num = (int) this._absences.AddOrUpdate(screenId, absence, (Func<string, ScreenUiTypeAbsence, ScreenUiTypeAbsence>) ((_1, _2) => absence));
  }

  public ScreenUiTypeAbsence GetAbsence(string screenId)
  {
    screenId = screenId.ToUpperInvariant();
    ScreenUiTypeAbsence screenUiTypeAbsence;
    return !this._absences.TryGetValue(screenId, out screenUiTypeAbsence) ? ScreenUiTypeAbsence.Unknown : screenUiTypeAbsence;
  }

  public void InvalidateCache() => this._absences.Clear();
}
