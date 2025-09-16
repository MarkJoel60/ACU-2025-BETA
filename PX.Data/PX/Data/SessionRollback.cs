// Decompiled with JetBrains decompiler
// Type: PX.Data.SessionRollback
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;

#nullable disable
namespace PX.Data;

internal class SessionRollback
{
  private PXGraph graph;
  private int inActionDepth;
  private bool resetOnUnload;

  public SessionRollback(PXGraph aGraph) => this.graph = aGraph;

  internal bool IsTransactionInsidePersist { get; set; }

  public void OnUnload()
  {
    if (!this.resetOnUnload)
      return;
    this.ResetToUnmodifiedState(SessionRollback.FakeState.UI | SessionRollback.FakeState.Dirty);
  }

  public void OnActionStart()
  {
    if (this.inActionDepth++ == 0)
    {
      this.ClearSessionUnmodified();
      this.SaveUnmodifiedState();
    }
    this.IsTransactionInsidePersist = false;
  }

  public void OnActionEnd() => --this.inActionDepth;

  public void DropSavedState() => this.ClearSessionUnmodified();

  public void ProcessDeleteException<ExceptionType>(ExceptionType ex) where ExceptionType : Exception
  {
    this.HandleIgnoredExceptions((Exception) ex);
    this.ResetToUnmodifiedState();
  }

  public void ProcessInsertException<ExceptionType>(ExceptionType ex) where ExceptionType : Exception
  {
    this.HandleIgnoredExceptions((Exception) ex);
    this.ResetToUnmodifiedState();
  }

  public void ProcessActionException<ExceptionType>(ExceptionType ex, string name) where ExceptionType : Exception
  {
    if (this.inActionDepth != 0)
      return;
    this.HandleIgnoredExceptions((Exception) ex);
    if (this.IsTransactionInsidePersist && (object) ex != null && !this.graph.IsDirty && !PXTransactionScope.IsScoped)
    {
      this.ResetToInitialState();
      this.IsTransactionInsidePersist = false;
    }
    else if (name == "Delete")
      this.ResetToUnmodifiedState(SessionRollback.FakeState.UI);
    else
      this.ResetToUnmodifiedState(SessionRollback.FakeState.UI | SessionRollback.FakeState.Dirty);
  }

  public void ProcessUpdateException<ExceptionType>(
    ExceptionType ex,
    PXCache cache,
    IDictionary keys,
    IDictionary values)
    where ExceptionType : Exception
  {
    this.HandleIgnoredExceptions((Exception) ex);
    this.ResetToUnmodifiedState();
    if ((object) ex is ThreadAbortException || this.graph.IsImport)
      throw (object) ex;
    SessionRollback.ProcessRollbackException((Exception) ex, cache, keys, values);
  }

  private void HandleIgnoredExceptions(Exception ex)
  {
    if (!(ex is SessionRollback.IgnoreRollbackException rollbackException))
      return;
    rollbackException.ThrowSavedDispatchInfo();
  }

  private static void ProcessRollbackException(
    Exception ex,
    PXCache cache,
    IDictionary keys,
    IDictionary values)
  {
    bool flag = false;
    SessionRollback.PXSessionRollbackException rollbackException = new SessionRollback.PXSessionRollbackException(ex);
    try
    {
      SessionRollback.PopulateFieldsState(cache, keys, values);
    }
    catch
    {
      flag = true;
    }
    if (flag)
      throw rollbackException;
    IEnumerable<PXFieldState> states = values.Values.OfType<PXFieldState>();
    IEnumerable<string> fieldsToRaiseError = SessionRollback.GetFieldsToRaiseError(cache, keys, states);
    PXFieldState stateToSetError = SessionRollback.GetStateToSetError(states);
    if (!fieldsToRaiseError.Any<string>() && stateToSetError == null)
      throw rollbackException;
    object obj = cache.FillItem(keys);
    object data = cache.Locate(obj);
    foreach (string name in fieldsToRaiseError)
      PXUIFieldAttribute.SetError(cache, data, name, rollbackException.Message, (string) null, false, PXErrorLevel.RowError);
    if (stateToSetError == null)
      return;
    stateToSetError.Error = ex.Message;
    stateToSetError.ErrorLevel = PXErrorLevel.RowError;
  }

  /// <summary>
  /// Currently there is only one way to show an error box without showing an error on specific field:
  /// it should be visible field on the primary view. As keys are most likely on primary view, we try to
  /// return them first if they are visible.
  /// </summary>
  private static IEnumerable<string> GetFieldsToRaiseError(
    PXCache cache,
    IDictionary keys,
    IEnumerable<PXFieldState> states)
  {
    List<string> list = keys.Keys.OfType<string>().Where<string>((Func<string, bool>) (x => IsVisible(x))).ToList<string>();
    if (list.Any<string>())
      return (IEnumerable<string>) list;
    if (!states.Any<PXFieldState>())
      return (IEnumerable<string>) Array<string>.Empty;
    return (IEnumerable<string>) new string[1]
    {
      (states.FirstOrDefault<PXFieldState>((Func<PXFieldState, bool>) (x => x.Visible)) ?? states.First<PXFieldState>()).Name
    };

    bool IsVisible(string fieldName)
    {
      return cache.Current != null && cache.Fields.Contains(fieldName) && cache.GetStateExt(cache.Current, fieldName) is PXFieldState stateExt && stateExt.Visible;
    }
  }

  private static PXFieldState GetStateToSetError(IEnumerable<PXFieldState> states)
  {
    return states.FirstOrDefault<PXFieldState>((Func<PXFieldState, bool>) (x => x.Visible)) ?? states.First<PXFieldState>();
  }

  private void ClearSessionUnmodified()
  {
    foreach (KeyValuePair<System.Type, PXCache> cach in (Dictionary<System.Type, PXCache>) this.graph.Caches)
      cach.Value?.ClearSessionUnmodified();
  }

  private void ResetToUnmodifiedState(SessionRollback.FakeState fakeState = SessionRollback.FakeState.None)
  {
    foreach (KeyValuePair<System.Type, PXCache> cach in (Dictionary<System.Type, PXCache>) this.graph.Caches)
      cach.Value.ResetToUnmodified(fakeState);
  }

  private void SaveUnmodifiedState()
  {
    foreach (KeyValuePair<System.Type, PXCache> cach in (Dictionary<System.Type, PXCache>) this.graph.Caches)
      cach.Value.SaveUnmodifiedState();
  }

  private void ResetToInitialState()
  {
    foreach (KeyValuePair<System.Type, PXCache> keyValuePair in this.graph.Caches.ToList<KeyValuePair<System.Type, PXCache>>())
      keyValuePair.Value.ResetToInitialState();
  }

  private static void PopulateFieldsState(PXCache cache, IDictionary keys, IDictionary values)
  {
    object obj = cache.FillItem(keys);
    object data = cache.Locate(obj);
    foreach (string str in values.Keys.ToArray<string>())
    {
      if (cache.Fields.Contains(str))
        values[(object) str] = cache.GetStateExt(data, str);
    }
  }

  public class PXSessionRollbackException(Exception inner) : PXException(inner, "Unhandled exception has occurred, your changes have been rolled back: {0}", (object) inner.Message)
  {
  }

  public class IgnoreRollbackException : PXException
  {
    private ExceptionDispatchInfo info;

    public IgnoreRollbackException(Exception inner)
      : base(inner.Message, inner)
    {
      this.info = ExceptionDispatchInfo.Capture(inner);
    }

    public void ThrowSavedDispatchInfo() => this.info.Throw();
  }

  [Flags]
  public enum FakeState
  {
    None = 0,
    UI = 1,
    Dirty = 2,
  }
}
