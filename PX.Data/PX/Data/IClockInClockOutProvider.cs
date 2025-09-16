// Decompiled with JetBrains decompiler
// Type: PX.Data.IClockInClockOutProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public interface IClockInClockOutProvider
{
  /// <returns>Timer start date and time, document name and status, or <see langword="null" /> if there is no current timer or the current user is not found.</returns>
  (System.DateTime? startDate, int? timeSpent, string summary, string status)? GetCurrentClockInTimerData();
}
