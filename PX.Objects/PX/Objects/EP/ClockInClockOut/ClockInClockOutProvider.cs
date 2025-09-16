// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ClockInClockOut.ClockInClockOutProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.EP.ClockInClockOut;

[PXInternalUseOnly]
internal sealed class ClockInClockOutProvider : IClockInClockOutProvider
{
  private readonly ICurrentUserInformationProvider _currentUserInformationProvider;

  public ClockInClockOutProvider(
    ICurrentUserInformationProvider currentUserInformationProvider)
  {
    this._currentUserInformationProvider = currentUserInformationProvider;
  }

  public (System.DateTime? startDate, int? timeSpent, string summary, string status)? GetCurrentClockInTimerData()
  {
    string screenIdByGraphType = PXSiteMap.Provider.GetScreenIdByGraphType(typeof (EPClockInTimerMaint));
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.clockInClockOut>() || PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenIdByGraphType) == null)
      return new (System.DateTime?, int?, string, string)?((new System.DateTime?(), new int?(), (string) null, "U"));
    if (this._currentUserInformationProvider.GetUserIdOrDefault() == Guid.NewGuid())
      return new (System.DateTime?, int?, string, string)?((new System.DateTime?(), new int?(), (string) null, (string) null));
    EPClockInTimerData clockInTimerData = PXGraph.CreateInstance<EPClockInTimerMaint>().CurrentTimer.SelectSingle();
    return clockInTimerData == null || !clockInTimerData.StartDate.HasValue ? new (System.DateTime?, int?, string, string)?((new System.DateTime?(), new int?(), (string) null, (string) null)) : new (System.DateTime?, int?, string, string)?((clockInTimerData.StartDate, clockInTimerData.TimeSpent, $"{clockInTimerData.EntityName} {clockInTimerData.DocumentNbr}", clockInTimerData.Status));
  }
}
