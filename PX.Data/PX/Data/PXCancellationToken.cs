// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCancellationToken
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Threading;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXCancellationToken
{
  public ManualResetEventSlim Handle = new ManualResetEventSlim(false);
  public volatile bool IsCancelled;
  public System.Action SyncCancelAction;
  public System.Action AsyncCancelAction;
  public System.Action OnCanBeCancelled;

  public static void CheckCancellation()
  {
    PXCancellationToken slot = PXContext.GetSlot<PXCancellationToken>();
    if (slot != null && slot.OnCanBeCancelled != null)
    {
      slot.OnCanBeCancelled();
      slot.OnCanBeCancelled = (System.Action) null;
    }
    if (slot == null)
      return;
    if (!slot.IsCancelled)
      return;
    try
    {
      if (slot.SyncCancelAction == null)
        throw new OperationCanceledException("Operation Cancelled");
      slot.SyncCancelAction();
    }
    finally
    {
      slot.Handle.Set();
    }
  }

  public bool Cancel(TimeSpan timeout)
  {
    this.IsCancelled = true;
    System.Action asyncCancelAction = this.AsyncCancelAction;
    if (asyncCancelAction != null)
      asyncCancelAction();
    return this.Handle.Wait(timeout);
  }

  public void CancelNoWait()
  {
    this.IsCancelled = true;
    System.Action asyncCancelAction = this.AsyncCancelAction;
    if (asyncCancelAction == null)
      return;
    asyncCancelAction();
  }

  public static PXCancellationToken Ensure(bool bCreate = true)
  {
    PXCancellationToken cancellationToken = PXContext.GetSlot<PXCancellationToken>();
    if (cancellationToken == null & bCreate)
    {
      cancellationToken = new PXCancellationToken();
      PXContext.SetSlot<PXCancellationToken>(cancellationToken);
    }
    return cancellationToken;
  }
}
