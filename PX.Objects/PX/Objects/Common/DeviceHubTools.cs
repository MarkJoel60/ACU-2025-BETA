// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DeviceHubTools
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CR;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.Common;

public static class DeviceHubTools
{
  public static async Task PrintReportViaDeviceHub<TBAccount>(
    PXGraph graph,
    string reportID,
    Dictionary<string, string> reportParameters,
    string notificationSource,
    TBAccount baccount,
    CancellationToken cancellationToken)
    where TBAccount : BAccount, new()
  {
    NotificationUtility notificationUtility = new NotificationUtility(graph);
    Dictionary<PrintSettings, PXReportRequiredException> dictionary1 = new Dictionary<PrintSettings, PXReportRequiredException>();
    Dictionary<string, string> dictionary2 = reportParameters;
    PrintSettings printSettings = new PrintSettings();
    printSettings.PrintWithDeviceHub = new bool?(true);
    printSettings.DefinePrinterManually = new bool?(false);
    Func<string, string, int?, Guid?> func = new Func<string, string, int?, Guid?>(notificationUtility.SearchPrinter);
    string str1 = notificationSource;
    string str2 = reportID;
    string str3 = (object) (TBAccount) baccount == null ? reportID : notificationUtility.SearchReport<TBAccount>(reportID, ((TBAccount) baccount).BAccountID, graph.Accessinfo.BranchID);
    int? branchId = graph.Accessinfo.BranchID;
    int num = await SMPrintJobMaint.CreatePrintJobGroups(SMPrintJobMaint.AssignPrintJobToPrinter(dictionary1, dictionary2, (IPrintable) printSettings, func, str1, str2, str3, branchId, (CurrentLocalization) null), cancellationToken) ? 1 : 0;
  }
}
