// Decompiled with JetBrains decompiler
// Type: PX.Data.TracePageUiStrings
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

[PXLocalizable]
public class TracePageUiStrings
{
  public const string LogsSubmitted = "Logs Submitted";
  public const string SubmitLogsButtonText = "SUBMIT LOGS";
  public const string LogsSubmittedBody = "Technical details about the last actions have been submitted to Acumatica Inc. If you want to contact your Acumatica support provider about this log, save the following trace log ID:";
  public const string UserLabel = "User";
  public const string LocalTimeLabel = "Local Time";
  public const string UtcTimeLabel = "UTC Time";
  public const string VersionLabel = "Version";
  public const string CustomizationsLabel = "Customizations";
  public const string ReportButtonDisabledHint = "Sending diagnostic and usage data is not enabled on the Site Preferences (SM200505) form.";
  public const string RequestHeader = "Request";
  public const string ScreenFirstRowTitle = "Screen";
  public const string TypeFirstRowTitle = "Type";
  public const string TimeFirstRowTitle = "Time";
  public const string CommandDetailHeader = "Command";
  public const string CompanyIdDetailHeader = "CompanyID";
  public const string KeysDetailHeader = "Keys";
  public const string PerformanceHeader = "Performance";
  public const string ServerTimeFirstRowTitle = "Server Time";
  public const string CpuTimeFirstRowTitle = "CPU Time";
  public const string SqlTimeFirstRowTitle = "SQL Time";
  public const string WaitTimeFirstRowTitle = "Wait Time";
  public const string SqlCountFirstRowTitle = "SQL Count";
  public const string SqlRowsFirstRowTitle = "SQL Rows";
  public const string MessagesTabHeader = "Messages";
  public const string MessageFirstRowTitle = "Message";
  public const string SqlTabHeader = "SQL";
  public const string ExceptionsTabHeader = "Exceptions";
  public const string PropertyName_Category = "Category";
  public const string PropertyName_StackTrace = "Stack Trace";
  public const string PropertyName_ExceptionType = "Exception Type";
  public const string PropertyName_DacDescriptorInfo = "DAC Details";
  public const string PropertyName_Message = "Message";
  public const string TablesHeader = "Tables";
  public const string TotalTimeHeader = "Total Time";
  public const string ExecutionsHeader = "Executions";
  public const string RowsHeader = "Rows";

  public static string GetLocalized(string message)
  {
    return PXLocalizer.Localize(message, typeof (TracePageUiStrings).FullName);
  }
}
