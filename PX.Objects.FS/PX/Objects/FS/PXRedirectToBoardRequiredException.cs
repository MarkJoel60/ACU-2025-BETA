// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.PXRedirectToBoardRequiredException
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

#nullable disable
namespace PX.Objects.FS;

public class PXRedirectToBoardRequiredException : PXRedirectToUrlException
{
  private static string BuildUrl(string baseBoardUrl, KeyValuePair<string, string>[] args)
  {
    StringBuilder stringBuilder = new StringBuilder("~\\");
    stringBuilder.Append(baseBoardUrl);
    if (args != null && args.Length != 0)
    {
      stringBuilder.Append("?");
      for (int index = 0; index < args.Length; ++index)
      {
        KeyValuePair<string, string> keyValuePair = args[index];
        stringBuilder.Append(keyValuePair.Key);
        stringBuilder.Append("=");
        stringBuilder.Append(keyValuePair.Value);
        if (index != args.Length - 1)
          stringBuilder.Append("&");
      }
    }
    return stringBuilder.ToString();
  }

  public PXRedirectToBoardRequiredException(
    string baseBoardUrl,
    KeyValuePair<string, string>[] parameters,
    PXBaseRedirectException.WindowMode windowMode = 3,
    bool supressFrameset = true)
    : base(PXRedirectToBoardRequiredException.BuildUrl(baseBoardUrl, parameters), windowMode, supressFrameset, (string) null)
  {
  }

  protected PXRedirectToBoardRequiredException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public static PXBaseRedirectException GenerateMultiEmployeeRedirect(
    PXSiteMapProvider siteMapProvide,
    KeyValuePair<string, string>[] parameters,
    MainAppointmentFilter calendarFilter,
    PXBaseRedirectException.WindowMode windowMode = 3)
  {
    if (siteMapProvide.FindSiteMapNodeByScreenID("FS300300").Url.IndexOf("pages/fs/calendars/MultiEmpDispatch/FS300300.aspx", StringComparison.OrdinalIgnoreCase) >= 0)
      return (PXBaseRedirectException) new PXRedirectToBoardRequiredException("pages/fs/calendars/MultiEmpDispatch/FS300300.aspx", parameters, windowMode);
    SchedulerMaint instance = PXGraph.CreateInstance<SchedulerMaint>();
    if (instance == null)
      return (PXBaseRedirectException) null;
    ((PXSelectBase<MainAppointmentFilter>) instance.MainAppointmentFilter).Current = calendarFilter;
    PXRedirectRequiredException employeeRedirect = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) employeeRedirect).Mode = windowMode;
    return (PXBaseRedirectException) employeeRedirect;
  }
}
