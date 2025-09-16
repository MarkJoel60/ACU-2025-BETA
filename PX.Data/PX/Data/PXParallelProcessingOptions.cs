// Decompiled with JetBrains decompiler
// Type: PX.Data.PXParallelProcessingOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>The parallel processing options that override the default ones and the ones from the <tt>web.config</tt> file.</summary>
/// <remarks>
///   <para>To enable parallel processing in the system, you need to set the <see cref="F:PX.Common.WebConfig.ParallelProcessingDisabled">ParallelProcessingDisabled</see> key to <see langword="false" /> in the <tt>appSettings</tt>
/// section of the <tt>web.config</tt> file. You can additionally set the values of the following keys:</para>
///   <list type="bullet">
///     <item><description>
///       <see cref="F:PX.Common.WebConfig.ParallelProcessingBatchSize">ParallelProcessingBatchSize</see>: You specify the number of records that are included in a batch for parallel processing. By default, the value is
///     10. You can override this value in code.</description></item>
///     <item><description>
///       <see cref="F:PX.Common.WebConfig.EnableAutoNumberingInSeparateConnection">EnableAutoNumberingInSeparateConnection</see>: You set the key to <tt>true</tt> to make the system issue new document numbers in a separate
///         connection to the <tt>NumberingSequence</tt> table, which means that the numbering sequence does not blocks other threads that are running in parallel.
///         You cannot override this value in code.
///         <para><b>Note:</b> If a thread fails to process a batch, the numbering sequence may have gaps in the numbering sequence.</para></description></item>
///     <item><description>
///       <see cref="F:PX.Common.WebConfig.ParallelProcessingMaxThreads">ParallelProcessingMaxThreads</see>: Specify the maximum number of parallel threads that are used by the processing delegates. You cannot override
///         this value in code.
///         <para><b>Tip:</b> The system calculates the number of cores that are used for parallel processing depending on the license parameters and the actual
///         number of cores available on the machine. The value of the key can additionally decrease the number of threads available for parallel processing.</para></description></item>
///     <item><description>
///       <see cref="F:PX.Common.WebConfig.IsParallelProcessingSkipExceptions">IsParallelProcessingSkipBatchExceptions</see>: Set the key to <tt>true</tt> to make the system continue with processing of other batches if an
///     exception occurs during processing of a batch. The error is still recorded and, if you use a parallel processing via a processing view, the
///     <see cref="T:PX.Data.PXOperationCompletedWithErrorException" /> exception is thrown after the processing of all batches. If you use the
///     <see cref="M:PX.Data.PXProcessing.ProcessItemsParallel``2(System.Collections.Generic.List{``1},System.Action{``0,``1,System.Threading.CancellationToken},System.Func{``0},PX.Data.PXParallelProcessingOptions,System.Threading.CancellationToken)">PXProcessing.ProcessItemsParallel</see> method directly, you can handle the errors by yourself. However, if the initial exception is
///     <see cref="T:PX.Data.PXOperationCompletedWithErrorException" /> exception, which is a system exception, the batch is marked as processed incorrectly regardless of the
///     value of the key. You cannot override the value of this key in code.</description></item>
///   </list>
/// </remarks>
/// <example><para>To configure the parallel execution for a particular processing view, you need to set ParallelProcessingOptions of the processing view, as shown in the example below.</para>
///   <code title="Example" lang="CS">
/// MyProcessingView.ParallelProcessingOptions =
///    settings =&gt;
///    {
///       settings.IsEnabled = true;
///       settings.BatchSize = 10;
///    };</code>
/// </example>
public class PXParallelProcessingOptions
{
  /// <summary>A field that specifies (if set to <see langword="true" />) that parallel processing is enabled for a particular processing.</summary>
  public bool IsEnabled;
  public bool AutoBatchSize;
  /// <summary>The batch size for parallel processing.</summary>
  /// <remarks>You override this field if you need to use a custom batch size for this parallel processing.</remarks>
  public int BatchSize = WebConfig.ParallelProcessingBatchSize;
  internal int? ParallelThreadsCount;
  /// <summary>A delegate for custom batch splitting.</summary>
  /// <remarks>If the <strong>SplitToBatches</strong> delegate is not <see langword="null"></see>, it is used instead of the algorithm of the original
  /// <strong>PX.Data.PXBatchList.SplitToBatches</strong> function.</remarks>
  /// <example>
  ///   <code title="Example" lang="CS">
  /// // The following code shows an example of the SplitToBatches delegate.
  /// // The ScheduledTran rows are ordered by the key, which consists of the following fields:
  /// // ScheduleID, ComponentID, and DetailLineNbr.
  /// // The customized SplitToBatches delegate corrects the batch size
  /// // if the ScheduledTran rows with the same parent key are split to multiple batches.
  /// // The correction of the batch size is necessary because the process updates
  /// // the parent object of the rows (which is DRScheduleDetail) and has to do it only once.
  /// private IEnumerable&lt;(int, int)&gt; SplitToBatches(IList list, PXParallelProcessingOptions options)
  /// {
  ///     ScheduledTran tEnd, tNext;
  ///     int start = 0, end = 0;
  /// 
  ///     while (start &lt; list.Count)
  ///     {
  ///         end = Math.Min(start + options.BatchSize - 1, list.Count - 1) - 1;
  ///         do
  ///         {
  ///             end++;
  ///             tEnd = (ScheduledTran)list[end];
  ///             tNext = (end + 1) &lt; list.Count ? ( )list[end + 1] : null;
  ///         }
  ///         while (tEnd.ScheduleID == tNext?.ScheduleID &amp;&amp; tEnd.ComponentID == tNext?.ComponentID &amp;&amp; tEnd.DetailLineNbr == tNext?.DetailLineNbr);
  /// 
  ///         yield return (start, end);
  ///         start = end + 1;
  ///     }
  /// }
  /// 
  /// protected virtual void ScheduleRecognitionFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  /// {
  ///     ScheduleRecognitionFilter filter = Filter.Current;
  ///     DateTime? filterDate = filter.RecDate;
  /// 
  ///     if (PX.Common.WebConfig.ParallelProcessingDisabled == false)
  ///     {
  ///         Items.ParallelProcessingOptions = settings =&gt; {
  ///             settings.IsEnabled = true;
  ///             settings.BatchSize = 1000;
  ///             settings.SplitToBatches = SplitToBatches;
  ///         };
  ///     }
  ///     Items.SetProcessDelegate(delegate (List&lt;ScheduledTran&gt; items)
  ///     {
  ///         RunRecognition(items, filterDate);
  ///     });
  /// }</code>
  /// </example>
  public Func<IList, PXParallelProcessingOptions, IEnumerable<(int start, int end)>> SplitToBatches;
}
