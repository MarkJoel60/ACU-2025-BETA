// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PaymentTransaction.AfterProcessingManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.Extensions.PaymentTransaction;

public abstract class AfterProcessingManager
{
  public bool IsMassProcess { get; set; }

  public virtual void RunAuthorizeActions(IBqlTable table, bool success)
  {
  }

  public virtual void RunIncreaseAuthorizedAmountActions(IBqlTable table, bool success)
  {
  }

  public virtual void RunCaptureActions(IBqlTable table, bool success)
  {
  }

  public virtual void RunPriorAuthorizedCaptureActions(IBqlTable table, bool success)
  {
  }

  public virtual void RunVoidActions(IBqlTable table, bool success)
  {
  }

  public virtual void RunCreditActions(IBqlTable table, bool success)
  {
  }

  public virtual void RunCaptureOnlyActions(IBqlTable table, bool success)
  {
  }

  public virtual void RunUnknownActions(IBqlTable table, bool success)
  {
  }

  public virtual bool CheckDocStateConsistency(IBqlTable table) => true;

  public abstract PXGraph GetGraph();

  public abstract void PersistData();
}
