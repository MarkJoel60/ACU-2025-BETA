// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLongOperation
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Async;
using PX.Async.Internal;
using PX.Common;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Threading;

#nullable enable
namespace PX.Data;

/// <summary>
/// <para>A static class that is used to execute a long-running operation, such as processing data or releasing a document, asynchronously in a separate thread. This
/// class manages the threads created on the Acumatica ERP server to process long-running operations.</para>
/// </summary>
public static class PXLongOperation
{
  [PXInternalUseOnly]
  public static 
  #nullable disable
  ILongOperationManager Manager => (ILongOperationManager) LongOperationManager.Instance;

  /// <summary>Flushes current state.</summary>
  /// <remarks>
  /// <para>If you use this method in <tt>global.asax</tt>, please check for double usage. We recommend that you use this method in
  /// the <tt>Application_EndRequest</tt> method.</para>
  /// </remarks>
  [PXInternalUseOnly]
  public static void FlushState() => LongOperationManager.Instance.FlushState();

  internal static void ClearAbortedProcesses()
  {
    try
    {
      using (PXAccess.GetAdminImpersonator())
        SMLongOperationMaint.ClearAbortedProcesses(WebsiteID.Key);
    }
    catch
    {
    }
  }

  /// <summary>
  /// <para> Starts the delegate method specified in the <paramref name="method" /> parameter
  /// as a long-running operation in a separate thread. The method
  /// also uses the key specified in the <paramref name="key" /> parameter to assign
  /// the long-running operation ID and passes to the delegate the
  /// arguments that are defined by the <paramref name="arguments" /> parameter. </para>
  /// </summary>
  /// <param name="key">A <tt>PXSpecialLongOperationKey</tt> object that contains the ID of the long-running operation,
  /// or the GUID of the long-running operation.</param>
  /// <param name="method">The delegate method to be executed
  /// asynchronously in a separate thread as a long-running operation.</param>
  /// <param name="arguments">The list of arguments of the delegate
  /// method specified in the <paramref name="method" /> parameter.</param>
  [Obsolete("Prefer closing over parameters")]
  public static void StartOperation(object key, PXLongRunDelegate method, object[] arguments)
  {
    PXLongOperation.StartOperation(key, (PXToggleAsyncDelegate) (() => method(arguments)));
  }

  /// <summary>
  /// Starts the delegate specified in the <paramref name="method" /> parameter as a long-running
  /// operation in a separate thread. The method uses the unique identifier (UID)
  /// of the graph specified in the <paramref name="graph" /> parameter to assign the long-running operation ID.
  /// </summary>
  /// <param name="graph">A graph UID that is used as the ID of the long-running operation.</param>
  /// <param name="method">The delegate method to be executed asynchronously in a separate thread as a long-running operation.</param>
  public static void StartOperation(PXGraph graph, PXToggleAsyncDelegate method)
  {
    CancellationIgnorantExtensions.StartOperation(PXLongOperation.Manager, graph, method);
  }

  /// <summary>
  /// Starts the delegate specified in the <paramref name="method" /> parameter as a long-running
  /// operation in a separate thread. The method uses the unique identifier (UID)
  /// of the graph extension specified in the <paramref name="graphExt" /> parameter to assign the long-running operation ID.
  /// </summary>
  /// <param name="graphExt">A graph extension UID that is used as the ID of the long-running operation.</param>
  /// <param name="method">The delegate method to be executed asynchronously in a separate thread as a long-running operation.</param>
  public static void StartOperation<TGraph>(
    PXGraphExtension<TGraph> graphExt,
    PXToggleAsyncDelegate method)
    where TGraph : PXGraph
  {
    PXLongOperation.Manager.StartOperation<TGraph>(graphExt, method);
  }

  /// <summary>
  /// <para>Starts the delegate specified in the <paramref name="method" /> parameter as a long-running
  /// operation in a separate thread. The method also uses the key specified
  /// in the <paramref name="key" /> parameter to assign the long-running operation ID.</para>
  /// </summary>
  /// <param name="key">A <tt>PXGraph</tt> object that contains the ID of the long-running operation,
  /// or the GUID of the long-running operation.</param>
  /// <param name="method">The delegate method to be executed asynchronously in a separate thread as a long-running operation.</param>
  public static void StartOperation(object key, PXToggleAsyncDelegate method)
  {
    PXLongOperation.Manager.StartOperation(key, method);
  }

  internal static void StartOperationWithForceAsync(Guid key, PXToggleAsyncDelegate method)
  {
    LongOperationManager.Instance.StartOperationWithForceAsync(key, method);
  }

  /// <summary>
  /// Clear result of long-running operation after completion.
  /// </summary>
  internal static LongOperationManager ClearOperationResultsOnCompletion()
  {
    return LongOperationManager.Instance.ClearOperationResultsOnCompletion();
  }

  internal static bool IsLongOperationInPendingState(object key)
  {
    return LongOperationManager.Instance.IsLongOperationInPendingState(key);
  }

  /// <exclude />
  public static void AsyncAbort(object key) => LongOperationManager.Instance.AsyncAbort(key);

  /// <summary>Simulates operation for current thread</summary>
  internal static IDisposable SimulateOperation()
  {
    return LongOperationManager.Instance.SimulateOperation();
  }

  /// <summary>
  /// Returns true if long-running operation is in progress.
  /// </summary>
  public static bool IsLongOperationContext() => PXLongOperation.Manager.IsLongOperationContext();

  /// <summary>
  /// <para>Returns the <see cref="T:PX.Data.PXLongRunStatus" /> status of the long-running operation specified by the <paramref name="key" /> parameter.</para>
  /// </summary>
  /// <param name="key">The ID of the long-running operation.</param>
  public static PXLongRunStatus GetStatus(object key) => PXLongOperation.Manager.GetStatus(key);

  /// <summary>
  /// Returns the <see cref="T:PX.Data.PXLongRunStatus" /> status of the long-running operation specified by the <paramref name="key" /> parameter.
  /// The method also returns the duration of the long-running operation and the status message.
  /// </summary>
  /// <param name="key">The ID of the long-running operation.</param>
  /// <param name="timestamp">The out parameter that is used to return the duration of the long-running operation.</param>
  /// <param name="message">The out parameter that is used to return the status message.</param>
  public static PXLongRunStatus GetStatus(
    object key,
    out TimeSpan timestamp,
    out Exception message)
  {
    return PXLongOperation.GetStatus(key, out timestamp, out message, out bool _);
  }

  /// <summary>
  /// Returns the <see cref="T:PX.Data.PXLongRunStatus" /> status of the long-running operation specified by the <paramref name="key" /> parameter.
  /// The method also returns the duration of the long-running operation, the status message, and an indicator
  /// of whether the status message is of the <see cref="T:PX.Data.PXBaseRedirectException" /> exception type.
  /// </summary>
  /// <param name="key">The ID of the long-running operation.</param>
  /// <param name="timestamp">The out parameter that is used to return the duration of the long-running operation.</param>
  /// <param name="message">The out parameter that is used to return the status message.</param>
  /// <param name="isRedirected">The out parameter that is used as an indicator of whether the status
  /// message is of the PXBaseRedirectException exception type.</param>
  public static PXLongRunStatus GetStatus(
    object key,
    out TimeSpan timestamp,
    out Exception message,
    out bool isRedirected)
  {
    (PXLongRunStatus status1, TimeSpan duration, Exception message1, bool isRedirected1) = PXLongOperation.Manager.GetOperationDetails(key);
    int status2 = (int) status1;
    timestamp = duration;
    message = message1;
    isRedirected = isRedirected1;
    return (PXLongRunStatus) status2;
  }

  /// <summary>Removes information about completed task</summary>
  /// <param name="key"></param>
  public static void ClearStatus(object key) => PXLongOperation.Manager.ClearStatus(key);

  /// <summary>
  /// removes information about task attached to the <paramref name="graph" />
  /// </summary>
  internal static void ForceClearStatus(PXGraph graph)
  {
    LongOperationManager.Instance.ForceClearStatus(OperationKey.For(graph));
  }

  /// <exclude />
  [PXInternalUseOnly]
  public static object GetCurrentItem() => LongOperationManager.Instance.GetCurrentItem();

  internal static void SetCurrentItem(object currentItem)
  {
    LongOperationManager.Instance.SetCurrentItem(currentItem);
  }

  internal static void SetOperationStateModified()
  {
    LongOperationManager.Instance.SetOperationStateModified();
  }

  internal static int GetCurrentIndex() => LongOperationManager.Instance.GetCurrentIndex();

  internal static void SetCurrentIndex(int CurrentIndex)
  {
    LongOperationManager.Instance.SetCurrentIndex(CurrentIndex);
  }

  /// <summary>
  /// <para>From the custom information dictionary of the current long-running operation,
  /// returns the data object stored under the default key. </para>
  /// </summary>
  public static object GetCustomInfo() => PXLongOperation.Manager.GetCustomInfo();

  /// <summary>
  /// <para>From the custom information dictionary of the current long-running operation,
  /// returns the data object that is stored under the specified key. If the <paramref name="customInfoKey" />
  /// value is null or empty, the method returns the data object that is stored under the default key.</para>
  /// </summary>
  /// <param name="customInfoKey">The key to access the data object in the dictionary.</param>
  public static object GetCustomInfoForCurrentThread(string customInfoKey)
  {
    return PXLongOperation.Manager.GetCustomInfo(customInfoKey);
  }

  /// <summary>
  /// <para>From the custom information dictionary of the long-running operation specified by the <paramref name="key" />,
  /// returns the data object that is stored under the default key.</para>
  /// </summary>
  /// <param name="key">The ID of the long-running operation.</param>
  public static object GetCustomInfo(object key) => PXLongOperation.Manager.GetCustomInfoFor(key);

  /// <summary>
  /// <para>From the custom information dictionary of the long-running operation specified
  /// by the <paramref name="key" /> parameter, returns the data object that is stored under the key that
  /// is defined by the <paramref name="customInfoKey" /> parameter.
  /// If the <see cref="T:PX.Data.PXLongOperation" /> static class does not contain a long-running operation with the
  /// key specified in the <paramref name="key" /> parameter, the method returns null. Otherwise, the method does the following:</para>
  /// 	<list type="bullet">
  /// 		<item><description>If the <paramref name="customInfoKey" /> parameter is null or empty:
  /// 		Returns the data object that is stored under the default key</description></item>
  /// 		<item><description>Otherwise: Returns the data object that is stored under the key specified
  /// 		in the <paramref name="customInfoKey" /> parameter</description></item>
  /// 	</list>
  /// </summary>
  /// <param name="key">The ID of the long-running operation.</param>
  /// <param name="customInfoKey">The key to access the data object in the custom information dictionary of
  /// the long-running operation specified by the first parameter.</param>
  public static object GetCustomInfo(object key, string customInfoKey)
  {
    return PXLongOperation.Manager.GetCustomInfoFor(key, customInfoKey);
  }

  /// <summary>
  /// <para>From the custom information dictionary of the long-running operation specified by the <paramref name="key" /> parameter,
  /// returns the data object that is stored under the default key.
  /// In the <paramref name="processingList" /> parameter, this method returns the list of the records processed
  /// by the delegate of the long-running operation.</para>
  /// </summary>
  /// <param name="key">The ID of the long-running operation.</param>
  /// <param name="processingList">The out parameter that is used to return the array of the data records that must
  /// be processed by the delegate of the long-running operation.</param>
  public static object GetCustomInfo(object key, out object[] processingList)
  {
    return PXLongOperation.Manager.GetCustomInfoFor(key, out processingList);
  }

  /// <summary>
  /// <para>From the custom information dictionary of the long-running operation specified by the <paramref name="key" /> parameter,
  /// returns the data object that is stored under the key specified in the <paramref name="customInfoKey" /> parameter.
  /// In the <paramref name="processingList" /> parameter, this method returns the list of the records processed by the
  /// delegate of the long-running operation.
  /// If the PXLongOperation static class does not contain a long-running operation with the key specified
  /// in the <paramref name="key" /> parameter, the method returns null. Otherwise, the method does the following:</para>
  /// 	<list type="bullet">
  /// 		<item><description>If the <paramref name="customInfoKey" /> parameter is null or empty:
  /// 		Returns the data object that is stored under the default key</description></item>
  /// 		<item><description>Otherwise: Returns the data object that is stored under the key specified
  /// 		in the <paramref name="customInfoKey" /> parameter</description></item>
  /// 	</list>
  /// </summary>
  /// <param name="key">The ID of the long-running operation.</param>
  /// <param name="customInfoKey">The key to access the data object in the custom information dictionary
  /// of the long-running operation specified by the first parameter.</param>
  /// <param name="processingList">The out parameter that is used to return the array of the data records
  /// that must be processed by the delegate of the long-running operation.</param>
  public static object GetCustomInfo(object key, string customInfoKey, out object[] processingList)
  {
    return PXLongOperation.Manager.GetCustomInfoFor(key, customInfoKey, out processingList);
  }

  internal static Dictionary<string, object> GetAllCustomInfo(object key)
  {
    return LongOperationManager.Instance.GetAllCustomInfo(key);
  }

  internal static bool IsDefaultInfo(string key) => PXLongOperationState.IsDefaultInfo(key);

  /// <exclude />
  [PXInternalUseOnly]
  public static bool Exists(PXGraph graph)
  {
    return !graph.stateLoading && PXLongOperation.Exists(graph.UID);
  }

  /// <exclude />
  [PXInternalUseOnly]
  public static bool Exists(object key) => LongOperationManager.Instance.Exists(key);

  /// <exclude />
  [PXInternalUseOnly]
  public static object GetCustomInfoPersistent(object key, bool completedOnly = true)
  {
    if (key == null)
      return (object) null;
    object customInfoPersistent = (object) null;
    if (PXContext.Session.IsSessionEnabled)
      customInfoPersistent = PXContext.Session.LongOpCustomInfo["CustomInfo:" + key.ToString()];
    if (customInfoPersistent == null && !completedOnly)
      customInfoPersistent = PXLongOperation.GetCustomInfo((object) ("CustomInfo:" + key.ToString()));
    return customInfoPersistent;
  }

  /// <exclude />
  [PXInternalUseOnly]
  public static void SetCustomInfoPersistent(object info)
  {
    PXLongOperation.SetCustomInfo(info, "CustomInfo:" + PXLongOperation.GetOperationKey().ToString(), (object[]) null);
  }

  /// <exclude />
  [PXInternalUseOnly]
  public static void RemoveCustomInfoPersistent(object key)
  {
    if (key == null || !PXContext.Session.IsSessionEnabled)
      return;
    PXContext.Session.Remove("CustomInfo:" + key.ToString());
  }

  /// <summary>
  /// In the custom information dictionary of the current long-running operation, stores the specified data object under the default key.
  /// </summary>
  /// <param name="info">The data object to be stored in the dictionary.</param>
  public static void SetCustomInfo(object info) => PXLongOperation.Manager.SetCustomInfo(info);

  /// <summary>
  /// In the custom information dictionary of the current long-running operation, stores the data object,
  /// which is specified in the <paramref name="info" /> parameter, under the key that is defined by the <paramref name="key" /> parameter.
  /// </summary>
  /// <param name="info">The data object to be stored in the dictionary.</param>
  /// <param name="key">The key to store and access in the dictionary the data object that is specified in the first parameter.</param>
  public static void SetCustomInfo(object info, string key)
  {
    PXLongOperation.Manager.SetCustomInfo(info, key);
  }

  /// <exclude />
  [PXInternalUseOnly]
  public static void SetCustomInfo(object info, object[] processingList)
  {
    PXLongOperation.SetCustomInfo(info, (string) null, processingList);
  }

  /// <exclude />
  [PXInternalUseOnly]
  public static void SetCustomInfo(object info, string key, object[] processingList)
  {
    LongOperationManager.Instance.SetCustomInfo(info, key, processingList);
  }

  internal static void SetCustomInfoInternal(object key, object info)
  {
    PXLongOperation.SetCustomInfoInternal(key, (string) null, info);
  }

  internal static void SetCustomInfoInternal(object key, string customInfoKey, object info)
  {
    LongOperationManager.Instance.SetCustomInfoInternal(key, customInfoKey, info);
  }

  internal static Result AsyncExecute<Result>(
    string uniqueKey,
    Func<Result> method,
    long waitTimeout)
  {
    return LongOperationManager.Instance.AsyncExecute<Result>(uniqueKey, method, waitTimeout);
  }

  internal static Result AsyncExecute<Result>(
    this LongOperationManager managerImplementation,
    string uniqueKey,
    Func<Result> method,
    long waitTimeout)
  {
    if (uniqueKey == null)
      throw new ArgumentNullException(nameof (uniqueKey));
    ILongOperationManager manager = (ILongOperationManager) managerImplementation;
    System.DateTime utcNow = System.DateTime.UtcNow;
    Exception message;
    while (true)
    {
      TimeSpan duration;
      PXLongRunStatus status;
      (status, duration, message, _) = manager.GetOperationDetails((object) uniqueKey);
      switch (status)
      {
        case PXLongRunStatus.NotExists:
          manager.StartOperation((object) uniqueKey, (PXToggleAsyncDelegate) (() =>
          {
            PX.Common.Async.SetStatusSetter((System.Action<string>) (info => manager.SetCustomInfo((object) info)));
            manager.SetCustomInfo((object) method());
          }));
          break;
        case PXLongRunStatus.Completed:
          goto label_5;
        case PXLongRunStatus.Aborted:
          goto label_6;
      }
      if (waitTimeout > 0L)
      {
        duration = System.DateTime.UtcNow.Subtract(utcNow);
        if (duration.TotalMilliseconds + 100.0 > (double) waitTimeout)
          goto label_11;
      }
      Thread.Sleep(100);
    }
label_5:
    Result customInfoFor = (Result) manager.GetCustomInfoFor((object) uniqueKey);
    managerImplementation.ForceClearStatus((object) uniqueKey);
    return customInfoFor;
label_6:
    managerImplementation.ForceClearStatus((object) uniqueKey);
    if (message != null)
      throw message;
    throw new PXException("An unhandled exception has occurred.");
label_11:
    return default (Result);
  }

  internal static object GetOperationKey() => LongOperationManager.Instance.GetOperationKey();

  internal static bool IsLongRunOperation => LongOperationManager.Instance.IsLongRunOperation;

  /// <summary>
  /// <para>Makes the current thread wait for the completion of the specified long-running operation.</para>
  /// </summary>
  /// <param name="key">The ID of the long-running operation.</param>
  /// <returns><c>true</c> if long-running operation was found, <c>false</c> otherwise</returns>
  public static bool WaitCompletion(object key) => PXLongOperation.Manager.WaitCompletion(key);

  /// <summary>
  /// <para>Returns the collection of <see cref="T:PX.SM.RowTaskInfo" /> objects, which contain information about
  /// the long-running operations that are in progress. The information includes the name of the user
  /// that started the operation, the form on which the operation was started, the progress of the operation,
  /// the number of errors, and the time the operation has been processing.</para>
  /// </summary>
  public static IEnumerable<RowTaskInfo> GetTaskList() => PXLongOperation.Manager.GetTaskList();
}
