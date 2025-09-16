// Decompiled with JetBrains decompiler
// Type: PX.Async.ILongOperationManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace PX.Async;

/// <summary>
/// <para>An interface that is used to execute a long-running operation, such as processing data or releasing a document, asynchronously in a separate thread. This
/// interface manages the threads created on the Acumatica ERP server to process long-running operations.</para>
/// </summary>
public interface ILongOperationManager
{
  /// <summary>
  /// <para>Starts the delegate specified in the <paramref name="method" /> parameter as a long-running
  /// operation in a separate thread. The method uses the unique identifier (UID)
  /// of the graph specified in the <paramref name="graph" /> parameter to assign the long-running operation ID.</para>
  /// </summary>
  /// <param name="graph">A graph UID that is used as the ID of the long-running operation.</param>
  /// <param name="method">The delegate method to be executed asynchronously in a separate thread as a long-running operation.</param>
  void StartOperation(PXGraph graph, System.Action<CancellationToken>? method);

  /// <summary>
  /// <para>Starts the delegate specified in the <paramref name="method" /> parameter as a long-running
  /// operation in a separate thread. The method also uses the key specified
  /// in the <paramref name="key" /> parameter to assign the long-running operation ID.</para>
  /// </summary>
  /// <param name="key">A <tt>PXGraph</tt> object that contains the ID of the long-running operation,
  /// or the GUID of the long-running operation.</param>
  /// <param name="method">The delegate method to be executed asynchronously in a separate thread as a long-running operation.</param>
  void StartOperation(object? key, System.Action<CancellationToken>? method);

  /// <summary>
  /// Starts the delegate specified in the <paramref name="method" /> parameter as a long-running
  /// operation in a separate thread. The method uses the unique identifier (UID)
  /// of the graph specified in the <paramref name="graph" /> parameter to assign the long-running operation ID.
  /// </summary>
  /// <param name="graph">A graph UID that is used as the ID of the long-running operation.</param>
  /// <param name="method">The delegate method to be executed asynchronously in a separate thread as a long-running operation.</param>
  void StartAsyncOperation(PXGraph graph, Func<CancellationToken, Task> method);

  /// <summary>
  /// Starts the delegate specified in the <paramref name="method" /> parameter as a long-running
  /// operation in a separate thread. The method also uses the key specified
  /// in the <paramref name="key" /> parameter to assign the long-running operation ID.
  /// </summary>
  /// <param name="key">A <tt>PXGraph</tt> object that contains the ID of the long-running operation,
  /// or the GUID of the long-running operation.</param>
  /// <param name="method">The delegate method to be executed asynchronously in a separate thread as a long-running operation.</param>
  void StartAsyncOperation(object? key, Func<CancellationToken, Task> method);

  /// <summary>
  /// Returns the details of the long-running operation specified by the <paramref name="key" /> parameter.
  /// </summary>
  /// <param name="key">The ID of the long-running operation.</param>
  LongOperationDetails GetOperationDetails(object key);

  /// <summary>
  /// <para>Makes the current thread wait for the completion of the specified long-running operation.</para>
  /// </summary>
  /// <param name="key">The ID of the long-running operation.</param>
  /// <returns><c>true</c> if long-running operation was found, <c>false</c> otherwise</returns>
  bool WaitCompletion(object key);

  /// <summary>Removes information about completed task</summary>
  /// <param name="key"></param>
  [PXInternalUseOnly]
  void ClearStatus(object key);

  /// <summary>
  /// <para>From the custom information dictionary of the current long-running operation,
  /// returns the data object that is stored under the specified key. If the <paramref name="customInfoKey" />
  /// value is null or empty, the method returns the data object that is stored under the default key.</para>
  /// </summary>
  /// <param name="customInfoKey">The key to access the data object in the dictionary.</param>
  [PXInternalUseOnly]
  object? GetCustomInfo(string? customInfoKey);

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
  [PXInternalUseOnly]
  object? GetCustomInfoFor(object? key, string? customInfoKey, out object[]? processingList);

  /// <summary>
  /// In the custom information dictionary of the current long-running operation, stores the data object,
  /// which is specified in the <paramref name="info" /> parameter, under the key that is defined by the <paramref name="customInfoKey" /> parameter.
  /// </summary>
  /// <param name="info">The data object to be stored in the dictionary.</param>
  /// <param name="customInfoKey">The key to store and access in the dictionary the data object that is specified in the first parameter.</param>
  [PXInternalUseOnly]
  void SetCustomInfo(object? info, string? customInfoKey);

  /// <summary>
  /// <para>Returns the collection of <see cref="T:PX.SM.RowTaskInfo" /> objects, which contain information about
  /// the long-running operations that are in progress. The information includes the name of the user
  /// that started the operation, the form on which the operation was started, the progress of the operation,
  /// the number of errors, and the time the operation has been processing.</para>
  /// </summary>
  [PXInternalUseOnly]
  IEnumerable<RowTaskInfo> GetTaskList();

  /// <summary>
  /// Returns true if long-running operation is in progress.
  /// </summary>
  [PXInternalUseOnly]
  bool IsLongOperationContext();
}
