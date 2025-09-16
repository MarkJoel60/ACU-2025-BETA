// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFirstChanceExceptionLogger
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.WindowsAzure.Storage;
using PX.Common;
using PX.Common.Configuration;
using PX.Data.Api.Mobile;
using PX.Data.Api.Mobile.SignManager;
using PX.Data.DacDescriptorGeneration;
using PX.Logging.TraceProviders;
using PX.SM;
using System;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Hosting;

#nullable enable
namespace PX.Data;

public static class PXFirstChanceExceptionLogger
{
  private const uint HRFileLocked = 2147942432 /*0x80070020*/;
  private const uint HRPortionOfFileLocked = 2147942433;
  internal static readonly 
  #nullable disable
  (System.Type, string, string, int?)[] ExceptionExcludeConditions = new (System.Type, string, string, int?)[14]
  {
    (typeof (PXBaseRedirectException), (string) null, (string) null, new int?()),
    (typeof (PXNotLoggedInException), (string) null, (string) null, new int?()),
    (typeof (PXUndefinedCompanyException), (string) null, (string) null, new int?()),
    (typeof (PXVisibiltyUpdateRequiredException), (string) null, (string) null, new int?()),
    (typeof (DbException), "Incorrect syntax near ')'.", "at PX.Data.PXDBAttributeAttribute.Definition.PX.Data.IPrefetchable<PX.Data.PXDBAttributeAttribute.DefinitionParams>.Prefetch(DefinitionParams parameters)", new int?()),
    (typeof (HttpException), "Invalid file name for file monitoring:", "at System.Web.DirectoryMonitor.AddFileMonitor(String file)", new int?()),
    (typeof (HttpException), (string) null, "at PX.Web.PXApplication.Application_Start(Object sender, EventArgs e)", new int?(-2147467259 /*0x80004005*/)),
    (typeof (ObjectDisposedException), "Cannot access a closed file.", "at Serilog.Sinks.File.SharedFileSink.Serilog.Sinks.File.IFileSink.EmitOrOverflow(LogEvent logEvent)", new int?()),
    (typeof (WebException), "(409)", "at Microsoft.WindowsAzure.Storage.Table.CloudTable.CreateIfNotExists(TableRequestOptions requestOptions, OperationContext operationContext)", new int?()),
    (typeof (StorageException), "(409)", "at Microsoft.WindowsAzure.Storage.Table.CloudTable.CreateIfNotExists(TableRequestOptions requestOptions, OperationContext operationContext)", new int?()),
    (typeof (Exception), "Redirect", "at PX.Web.UI.PXBaseDataSource.RedirectHelper.TryRedirect(PXRedirectRequiredException e)", new int?()),
    (typeof (PXShowDialogException), (string) null, (string) null, new int?()),
    (typeof (PXDialogRequiredException), (string) null, (string) null, new int?()),
    (typeof (PXSignatureRequiredException), (string) null, (string) null, new int?())
  };
  private const string ExceptionExcludeConditionCloudTableExceptionMessage = "(409)";
  private const string ExceptionExcludeConditionCloudTableExceptionStackTrace = "at Microsoft.WindowsAzure.Storage.Table.CloudTable.CreateIfNotExists(TableRequestOptions requestOptions, OperationContext operationContext)";
  [ThreadStatic]
  private static bool IsRecursive;
  [ThreadStatic]
  private static object LastInst;
  private static bool _enabled;
  private static string _logFileName;
  private static bool _isClusterEnabled;
  private static readonly ConcurrentQueue<string> exceptionQueue = new ConcurrentQueue<string>();

  private static bool IsShutdowning => HostingEnvironment.ShutdownReason != 0;

  internal static IHostBuilder UseFirstChanceExceptionLogger(this IHostBuilder hostBuilder)
  {
    return hostBuilder.ConfigureServices((Action<HostBuilderContext, IServiceCollection>) ((context, _) =>
    {
      PXFirstChanceExceptionLogger._enabled = ConfigurationBinder.GetValue<bool>(context.Configuration, "EnableFirstChanceExceptionsLogging", false);
      PXFirstChanceExceptionLogger._logFileName = context.Configuration["FirstChanceExceptionsLogFileName"];
      PXFirstChanceExceptionLogger._isClusterEnabled = context.Configuration.IsClusterEnabled();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      AppDomain.CurrentDomain.FirstChanceException += PXFirstChanceExceptionLogger.\u003C\u003EO.\u003C0\u003E__OnCurrentDomainOnFirstChanceException ?? (PXFirstChanceExceptionLogger.\u003C\u003EO.\u003C0\u003E__OnCurrentDomainOnFirstChanceException = new EventHandler<FirstChanceExceptionEventArgs>(PXFirstChanceExceptionLogger.OnCurrentDomainOnFirstChanceException));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      AppDomain.CurrentDomain.FirstChanceException += PXFirstChanceExceptionLogger.\u003C\u003EO.\u003C1\u003E__ProfilerFirstChanceException ?? (PXFirstChanceExceptionLogger.\u003C\u003EO.\u003C1\u003E__ProfilerFirstChanceException = new EventHandler<FirstChanceExceptionEventArgs>(PXFirstChanceExceptionLogger.ProfilerFirstChanceException));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      PXFirstChanceExceptionLoggerProxy.LogMessage = PXFirstChanceExceptionLogger.\u003C\u003EO.\u003C2\u003E__LogMessage ?? (PXFirstChanceExceptionLogger.\u003C\u003EO.\u003C2\u003E__LogMessage = new System.Action<string>(PXFirstChanceExceptionLogger.LogMessage));
    }));
  }

  private static void OnCurrentDomainOnFirstChanceException(
    object o,
    FirstChanceExceptionEventArgs args)
  {
    if (PXFirstChanceExceptionLogger.IsRecursive || PXFirstChanceExceptionLogger.IsShutdowning)
      return;
    if (args.Exception is IOException)
      return;
    try
    {
      PXFirstChanceExceptionLogger.IsRecursive = true;
      if (!PXFirstChanceExceptionLogger._enabled)
        return;
      PXFirstChanceExceptionLogger.LogException(args.Exception);
    }
    catch
    {
    }
    finally
    {
      PXFirstChanceExceptionLogger.IsRecursive = false;
    }
  }

  private static void ProfilerFirstChanceException(object o, FirstChanceExceptionEventArgs args)
  {
    if (PXFirstChanceExceptionLogger.IsRecursive)
      return;
    if (PXFirstChanceExceptionLogger.IsShutdowning)
      return;
    try
    {
      PXFirstChanceExceptionLogger.IsRecursive = true;
      if (PXFirstChanceExceptionLogger.LastInst != null && PXFirstChanceExceptionLogger.LastInst == args.Exception)
        return;
      PXFirstChanceExceptionLogger.LastInst = (object) args.Exception;
      string stackTrace = args.Exception is InsufficientExecutionStackException ? args.Exception.StackTrace : PXStackTrace.GetStackTrace(1).ToString();
      if (args.Exception.IsExcluded(stackTrace, PXFirstChanceExceptionLogger.ExceptionExcludeConditions))
        return;
      DacDescriptor? attachedDacDescriptor = args.Exception.GetAttachedDacDescriptor();
      PXPerformanceMonitor.AddTrace(args.Exception.Source ?? PXTrace.TryGetAspNetCallbackInformation(), "FirstChanceException", args.Exception.Message, stackTrace, args.Exception.GetType().FullName, (string) null, (string) null, attachedDacDescriptor?.ToString());
    }
    catch
    {
    }
    finally
    {
      PXFirstChanceExceptionLogger.IsRecursive = false;
    }
  }

  [Obsolete("This object is obsolete and will be removed. Rewrite your code without this object or contact your partner for assistance.")]
  public static void LogMessage(string m)
  {
    if (!PXFirstChanceExceptionLogger._enabled)
      return;
    try
    {
      throw new Exception(m);
    }
    catch
    {
    }
  }

  private static void LogException(Exception exception)
  {
    string path = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), PXFirstChanceExceptionLogger._logFileName);
    string stackTrace = exception is InsufficientExecutionStackException ? exception.StackTrace : PXStackTrace.GetStackTrace(2, true).ToString();
    if (exception.IsExcluded(stackTrace, PXFirstChanceExceptionLogger.ExceptionExcludeConditions))
      return;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine("\n================");
    stringBuilder.Append(System.DateTime.UtcNow.ToString("MMM/dd HH:mm:ss.ffff  ", (IFormatProvider) CultureInfo.InvariantCulture));
    if (PXFirstChanceExceptionLogger._isClusterEnabled)
      stringBuilder.Append(WebsiteID.Key);
    try
    {
      if (HttpContext.Current != null)
      {
        stringBuilder.Append(" ");
        stringBuilder.Append((object) HttpContext.Current.Request.Url);
      }
    }
    catch (HttpException ex)
    {
    }
    stringBuilder.AppendLine();
    stringBuilder.AppendLine(exception.ToString());
    if (exception is ReflectionTypeLoadException typeLoadException)
    {
      Exception[] loaderExceptions = typeLoadException.LoaderExceptions;
      if ((loaderExceptions != null ? (loaderExceptions.Length != 0 ? 1 : 0) : 0) != 0)
      {
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("Loader exceptions:");
        stringBuilder.AppendLine();
        foreach (Exception loaderException in typeLoadException.LoaderExceptions)
        {
          stringBuilder.AppendLine(loaderException.ToString());
          stringBuilder.AppendLine();
        }
      }
    }
    stringBuilder.AppendLine("Stack trace: " + stackTrace);
    if (PXFirstChanceExceptionLogger.exceptionQueue.Count < 1000)
      PXFirstChanceExceptionLogger.exceptionQueue.Enqueue(stringBuilder.ToString());
    Stream stream;
    if (PXFirstChanceExceptionLogger.exceptionQueue.IsEmpty || !PXFirstChanceExceptionLogger.TryOpen(path, out stream))
      return;
    using (stream)
    {
      StreamWriter streamWriter = new StreamWriter(stream);
      while (!PXFirstChanceExceptionLogger.exceptionQueue.IsEmpty)
      {
        string result;
        PXFirstChanceExceptionLogger.exceptionQueue.TryDequeue(out result);
        if (!string.IsNullOrEmpty(result))
          streamWriter.Write(result);
      }
      streamWriter.Close();
    }
  }

  private static bool FileIsLocked(IOException ioException)
  {
    uint hrForException = (uint) Marshal.GetHRForException((Exception) ioException);
    return hrForException == 2147942432U /*0x80070020*/ || hrForException == 2147942433U;
  }

  private static bool TryOpen(string path, out Stream stream)
  {
    try
    {
      stream = (Stream) System.IO.File.Open(path, FileMode.Append);
      return true;
    }
    catch (IOException ex)
    {
      if (!PXFirstChanceExceptionLogger.FileIsLocked(ex))
        throw;
      stream = (Stream) null;
      return false;
    }
  }
}
