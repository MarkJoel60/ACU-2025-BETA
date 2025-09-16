// Decompiled with JetBrains decompiler
// Type: PX.Data.PXProcessing
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable disable
namespace PX.Data;

/// <summary>A helper class that provides methods for processing pages.</summary>
public static class PXProcessing
{
  public const string ProcessingKey = "PXProcessingState";
  public const string ScheduleIsRunning = "ScheduleIsRunning";

  internal static void SetProcessingInfo(PXProcessingInfo info, object[] processingList)
  {
    PXLongOperation.SetCustomInfo((object) info, "PXProcessingState", processingList);
  }

  internal static void SetProcessingInfoInternal(object key, object info)
  {
    PXLongOperation.SetCustomInfoInternal(key, "PXProcessingState", info);
  }

  internal static PXProcessingInfo GetProcessingInfo()
  {
    return (PXProcessingInfo) PXLongOperation.GetCustomInfoForCurrentThread("PXProcessingState");
  }

  internal static PXProcessingInfo GetProcessingInfo(object key)
  {
    return (PXProcessingInfo) PXLongOperation.GetCustomInfo(key, "PXProcessingState");
  }

  public static PXProcessingInfo GetProcessingInfo(object key, out object[] processingList)
  {
    return (PXProcessingInfo) PXLongOperation.GetCustomInfo(key, "PXProcessingState", out processingList);
  }

  internal static bool IsProcessingKey(string key) => string.Equals(key, "PXProcessingState");

  /// <summary>Initiates parallel processing of the specified list of records with the specified parallel processing options.</summary>
  /// <param name="list">The list of records for parallel processing.</param>
  /// <param name="action">The action to be used for processing.</param>
  /// <param name="parallelOpt">The parallel processing options.</param>
  /// <remarks>This method allows you to use custom error-handling logic.
  /// <inheritdoc cref="T:PX.Data.PXParallelProcessingOptions" path="/remarks" />
  /// </remarks>
  /// <returns>The method returns <see langword="true"></see> if there were errors during the execution.</returns>
  public static bool ProcessItemsParallel<TGraph, TTable>(
    List<TTable> list,
    Action<TGraph, TTable, CancellationToken> action,
    Func<TGraph> factory,
    PXParallelProcessingOptions parallelOpt,
    CancellationToken cancellationToken)
    where TTable : IBqlTable
  {
    PXProcessingInfo<TTable> info = new PXProcessingInfo<TTable>()
    {
      Messages = new PXProcessingMessagesCollection<TTable>(list.Count)
    };
    object[] castList = list.Cast<object>().ToArray<object>();
    PXProcessing.SetProcessingInfo((PXProcessingInfo) info, castList);
    if (WebConfig.ParallelProcessingDisabled || !parallelOpt.IsEnabled)
    {
      TGraph state = factory();
      for (int i = 0; i < list.Count; ++i)
        PXProcessing.ProcessItem<TGraph, TTable>(state, list, i, action, info, cancellationToken);
    }
    else
      PXBatchList.SplitAndProcessAll((IList) list, parallelOpt, (Func<(int, int), System.Action<CancellationToken>>) (range =>
      {
        (int start2, int end2) = range;
        TGraph g = factory();
        return (System.Action<CancellationToken>) (batchCancellationToken =>
        {
          PXProcessing.SetProcessingInfo((PXProcessingInfo) info, castList);
          for (int i = start2; i <= end2; ++i)
            PXProcessing.ProcessItem<TGraph, TTable>(g, list, i, action, info, batchCancellationToken);
        });
      }), cancellationToken);
    PXProcessing.SetProcessingInfo((PXProcessingInfo) info, list.Cast<object>().ToArray<object>());
    return info.Errors > 0;
  }

  private static void ProcessItem<TState, TTable>(
    TState state,
    List<TTable> list,
    int i,
    Action<TState, TTable, CancellationToken> action,
    PXProcessingInfo<TTable> info,
    CancellationToken cancellationToken)
    where TTable : IBqlTable
  {
    PXProcessing.ProcessItem<TTable>(list, i, (Action<TTable, CancellationToken>) ((item, itemCancellationToken) => action(state, item, itemCancellationToken)), info, cancellationToken);
  }

  private static void ProcessItem<TTable>(
    List<TTable> list,
    int i,
    Action<TTable, CancellationToken> action,
    PXProcessingInfo<TTable> info,
    CancellationToken cancellationToken)
    where TTable : IBqlTable
  {
    cancellationToken.ThrowIfCancellationRequested();
    PXProcessingMessagesCollection<TTable> messages = info.Messages;
    try
    {
      PXLongOperation.SetCurrentIndex(i);
      action(list[i], cancellationToken);
      if (messages[i] != null)
        return;
      messages[i] = (PXProcessingMessage) new PXProcessingMessage<TTable>(PXErrorLevel.RowInfo, PXMessages.LocalizeNoPrefix("The record has been processed successfully."));
    }
    catch (PXOuterException ex)
    {
      messages[i] = (PXProcessingMessage) new PXProcessingMessage<TTable>(PXErrorLevel.RowError, ex.GetFullMessage(" "));
    }
    catch (PXSetPropertyException ex)
    {
      PXErrorLevel errorLevel = ex.ErrorLevel;
      switch (errorLevel)
      {
        case PXErrorLevel.Warning:
          errorLevel = PXErrorLevel.RowWarning;
          break;
        case PXErrorLevel.Error:
          errorLevel = PXErrorLevel.RowError;
          break;
      }
      messages[i] = (PXProcessingMessage) new PXProcessingMessage<TTable>(errorLevel, errorLevel == PXErrorLevel.RowInfo ? ex.MessageNoPrefix : ex.Message);
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
      messages[i] = (PXProcessingMessage) new PXProcessingMessage<TTable>(PXErrorLevel.RowError, ex.Message);
    }
  }

  [Obsolete("Support CancellationToken")]
  public static int ProcessItems<TTable>(List<TTable> list, System.Action<TTable> action) where TTable : IBqlTable
  {
    return PXProcessing.ProcessItems<TTable>(list, (Action<TTable, CancellationToken>) ((item, _) => action(item)), CancellationToken.None);
  }

  public static int ProcessItems<TTable>(
    List<TTable> list,
    Action<TTable, CancellationToken> action,
    CancellationToken cancellationToken)
    where TTable : IBqlTable
  {
    PXProcessingInfo<TTable> info = new PXProcessingInfo<TTable>()
    {
      Messages = new PXProcessingMessagesCollection<TTable>(list.Count)
    };
    PXProcessing.SetProcessingInfo((PXProcessingInfo) info, list.Cast<object>().ToArray<object>());
    for (int i = 0; i < list.Count; ++i)
      PXProcessing.ProcessItem<TTable>(list, i, action, info, cancellationToken);
    PXProcessing.SetProcessingInfo((PXProcessingInfo) info, list.Cast<object>().ToArray<object>());
    return list.Count - info.Errors;
  }

  private static bool Set(System.Type table, PXErrorLevel level, int index, string message)
  {
    PXProcessingInfo processingInfo = PXProcessing.GetProcessingInfo();
    if (processingInfo != null)
    {
      PXProcessingMessagesCollection messages = processingInfo.Messages;
      if (messages != null && index >= 0 && index < messages.Length)
      {
        messages[index] = new PXProcessingMessage(level, message)
        {
          ItemType = table
        };
        PXLongOperation.SetOperationStateModified();
        return true;
      }
    }
    return false;
  }

  private static bool Set(System.Type table, PXErrorLevel level, int index, Exception e)
  {
    return PXProcessing.Set(table, level, index, e is PXOuterException exception ? exception.GetFullMessage("\r\n") : e.Message);
  }

  /// <summary>Sets the error message on the data record with the specified
  /// index.</summary>
  /// <param name="index">The index of the data record marked with
  /// error.</param>
  /// <param name="message">The error message.</param>
  public static bool SetError(int index, string message)
  {
    return PXProcessing.Set((System.Type) null, PXErrorLevel.RowError, index, message);
  }

  /// <summary>Sets the provided string as the error message of the
  /// processing operation.</summary>
  /// <param name="message">The error message.</param>
  public static bool SetError(string message)
  {
    return PXProcessing.SetError(PXLongOperation.GetCurrentIndex(), message);
  }

  /// <summary>Sets the provided exception as the error on the data record
  /// with the specified index.</summary>
  /// <param name="index">The index of the data record marked with
  /// error.</param>
  /// <param name="e">The exception containing information about the
  /// error.</param>
  public static bool SetError(int index, Exception e)
  {
    return PXProcessing.Set((System.Type) null, PXErrorLevel.RowError, index, e);
  }

  /// <summary>Sets the provided exception as the error of the processing
  /// operation.</summary>
  /// <param name="e">The exception containing information about the
  /// error.</param>
  public static bool SetError(Exception e)
  {
    return PXProcessing.SetError(PXLongOperation.GetCurrentIndex(), e);
  }

  /// <summary>Sets the warning message on the data record with the
  /// specified index.</summary>
  /// <param name="index">The index of the data record to which the message
  /// is attached.</param>
  /// <param name="message">The warning message.</param>
  public static bool SetWarning(int index, string message)
  {
    return PXProcessing.Set((System.Type) null, PXErrorLevel.RowWarning, index, message);
  }

  /// <summary>Sets the warning message for the processing
  /// operation.</summary>
  /// <param name="message">The warning message.</param>
  public static bool SetWarning(string message)
  {
    return PXProcessing.SetWarning(PXLongOperation.GetCurrentIndex(), message);
  }

  /// <summary>Attaches the provided exception as the warning-level error to
  /// the data record with the specified index.</summary>
  /// <param name="index">The index of the data record to which the
  /// exception is attached.</param>
  /// <param name="e">The exception containing warning information.</param>
  public static bool SetWarning(int index, Exception e)
  {
    return PXProcessing.Set((System.Type) null, PXErrorLevel.RowWarning, index, e);
  }

  /// <summary>Sets the provided exceptiona as the warning-level error of
  /// the processing operation.</summary>
  /// <param name="e">The exception containing warning information.</param>
  public static bool SetWarning(Exception e)
  {
    return PXProcessing.Set((System.Type) null, PXErrorLevel.RowWarning, PXLongOperation.GetCurrentIndex(), e);
  }

  /// <summary>Attaches the provided information message to the data record
  /// with the specified index.</summary>
  /// <param name="index">The index of the data record to which the message
  /// is attached.</param>
  /// <param name="message">The information message.</param>
  public static bool SetInfo(int index, string message)
  {
    return PXProcessing.Set((System.Type) null, PXErrorLevel.RowInfo, index, message);
  }

  /// <summary>Sets the information message for the processing
  /// operation.</summary>
  /// <param name="message">The information message.</param>
  public static bool SetInfo(string message)
  {
    return PXProcessing.SetInfo(PXLongOperation.GetCurrentIndex(), message);
  }

  /// <summary>Attaches the provided exception as the information-level
  /// error to the data record with the specified index.</summary>
  /// <param name="index">The index of the data record that is marked with
  /// the exception.</param>
  /// <param name="e">The exception containing information.</param>
  public static bool SetInfo(int index, Exception e)
  {
    return PXProcessing.Set((System.Type) null, PXErrorLevel.RowInfo, PXLongOperation.GetCurrentIndex(), e);
  }

  /// <summary>Sets the provided exception as the information-level error
  /// for the processing operation.</summary>
  /// <param name="e">The exception containing information.</param>
  public static bool SetInfo(Exception e)
  {
    return PXProcessing.SetInfo(PXLongOperation.GetCurrentIndex(), e);
  }

  /// <summary>Sets the information message confirming that a data record
  /// has been processed successfully</summary>
  public static bool SetProcessed()
  {
    return PXProcessing.SetInfo("The record has been processed successfully.");
  }

  /// <summary>Sets the error message on the data record with the specified
  /// index and specified type.</summary>
  /// <param name="index">The index of the data record marked with
  /// error.</param>
  /// <param name="message">The error message.</param>
  public static bool SetError<TTable>(int index, string message) where TTable : IBqlTable
  {
    return PXProcessing.Set(typeof (TTable), PXErrorLevel.RowError, index, message);
  }

  /// <summary>Sets the provided string as the error message of the
  /// processing operation for the specified type.</summary>
  /// <param name="message">The error message.</param>
  public static bool SetError<TTable>(string message) where TTable : IBqlTable
  {
    return PXProcessing.SetError<TTable>(PXLongOperation.GetCurrentIndex(), message);
  }

  /// <summary>Sets the provided exception as the error on the data record
  /// with the specified index and specified type.</summary>
  /// <param name="index">The index of the data record marked with
  /// error.</param>
  /// <param name="e">The exception containing information about the
  /// error.</param>
  public static bool SetError<TTable>(int index, Exception e) where TTable : IBqlTable
  {
    return PXProcessing.Set(typeof (TTable), PXErrorLevel.RowError, index, e);
  }

  /// <summary>Sets the provided exception as the error of the processing
  /// operation for the specified type.</summary>
  /// <param name="e">The exception containing information about the
  /// error.</param>
  public static bool SetError<TTable>(Exception e) where TTable : IBqlTable
  {
    return PXProcessing.SetError<TTable>(PXLongOperation.GetCurrentIndex(), e);
  }

  /// <summary>Sets the warning message on the data record with the
  /// specified index and specified type.</summary>
  /// <param name="index">The index of the data record to which the message
  /// is attached.</param>
  /// <param name="message">The warning message.</param>
  public static bool SetWarning<TTable>(int index, string message) where TTable : IBqlTable
  {
    return PXProcessing.Set(typeof (TTable), PXErrorLevel.RowWarning, index, message);
  }

  /// <summary>Sets the warning message for the processing
  /// operation for the specified type.</summary>
  /// <param name="message">The warning message.</param>
  public static bool SetWarning<TTable>(string message) where TTable : IBqlTable
  {
    return PXProcessing.SetWarning<TTable>(PXLongOperation.GetCurrentIndex(), message);
  }

  /// <summary>Attaches the provided exception as the warning-level error to
  /// the data record with the specified index and specified type.</summary>
  /// <param name="index">The index of the data record to which the
  /// exception is attached.</param>
  /// <param name="e">The exception containing warning information.</param>
  public static bool SetWarning<TTable>(int index, Exception e) where TTable : IBqlTable
  {
    return PXProcessing.Set(typeof (TTable), PXErrorLevel.RowWarning, index, e);
  }

  /// <summary>Sets the provided exceptiona as the warning-level error of
  /// the processing operation for the specified type.</summary>
  /// <param name="e">The exception containing warning information.</param>
  public static bool SetWarning<TTable>(Exception e) where TTable : IBqlTable
  {
    return PXProcessing.Set(typeof (TTable), PXErrorLevel.RowWarning, PXLongOperation.GetCurrentIndex(), e);
  }

  /// <summary>Attaches the provided information message to the data record
  /// with the specified index and specified type.</summary>
  /// <param name="index">The index of the data record to which the message
  /// is attached.</param>
  /// <param name="message">The information message.</param>
  public static bool SetInfo<TTable>(int index, string message) where TTable : IBqlTable
  {
    return PXProcessing.Set(typeof (TTable), PXErrorLevel.RowInfo, index, message);
  }

  /// <summary>Sets the information message for the processing
  /// operation for the specified type.</summary>
  /// <param name="message">The information message.</param>
  public static bool SetInfo<TTable>(string message) where TTable : IBqlTable
  {
    return PXProcessing.SetInfo<TTable>(PXLongOperation.GetCurrentIndex(), message);
  }

  /// <summary>Attaches the provided exception as the information-level
  /// error to the data record with the specified index and specified type.</summary>
  /// <param name="index">The index of the data record that is marked with
  /// the exception.</param>
  /// <param name="e">The exception containing information.</param>
  public static bool SetInfo<TTable>(int index, Exception e) where TTable : IBqlTable
  {
    return PXProcessing.Set(typeof (TTable), PXErrorLevel.RowInfo, PXLongOperation.GetCurrentIndex(), e);
  }

  /// <summary>Sets the provided exception as the information-level error
  /// for the processing operation for the specified type.</summary>
  /// <param name="e">The exception containing information.</param>
  public static bool SetInfo<TTable>(Exception e) where TTable : IBqlTable
  {
    return PXProcessing.SetInfo<TTable>(PXLongOperation.GetCurrentIndex(), e);
  }

  /// <summary>Sets the information message confirming that a data record for the specified type
  /// has been processed successfully</summary>
  public static bool SetProcessed<TTable>() where TTable : IBqlTable
  {
    return PXProcessing.SetInfo<TTable>("The record has been processed successfully.");
  }

  /// <summary>Sets the current data record to process.</summary>
  /// <param name="currentItem">The data record to be set as the
  /// current.</param>
  public static void SetCurrentItem(object currentItem)
  {
    PXLongOperation.SetCurrentItem(currentItem);
  }

  public static PXProcessingMessage GetItemMessage<TTable>() where TTable : IBqlTable
  {
    PXProcessingInfo processingInfo = PXProcessing.GetProcessingInfo();
    return processingInfo == null || !typeof (PXProcessingInfo<TTable>).IsAssignableFrom(processingInfo.GetType()) ? (PXProcessingMessage) null : ((PXProcessingInfo<TTable>) processingInfo).Messages[PXLongOperation.GetCurrentIndex()];
  }
}
