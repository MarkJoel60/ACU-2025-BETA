// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMCommitmentAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.PM;

public abstract class PMCommitmentAttribute : 
  PXEventSubscriberAttribute,
  IPXRowInsertedSubscriber,
  IPXRowUpdatedSubscriber,
  IPXRowDeletedSubscriber
{
  protected Type primaryEntity;
  protected Type detailEntity;

  public PMCommitmentAttribute(Type primaryEntity) => this.primaryEntity = primaryEntity;

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.detailEntity = sender.GetItemType();
    if (!PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
      return;
    PXGraph.RowUpdatedEvents rowUpdated = sender.Graph.RowUpdated;
    Type primaryEntity = this.primaryEntity;
    PMCommitmentAttribute commitmentAttribute = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) commitmentAttribute, __vmethodptr(commitmentAttribute, DocumentRowUpdated));
    rowUpdated.AddHandler(primaryEntity, pxRowUpdated);
  }

  public void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    Guid? commitmentID = (Guid?) sender.GetValue(e.Row, this.FieldOrdinal);
    this.DeleteCommitment(sender, commitmentID);
  }

  public void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    this.SyncCommitment(sender, e.Row);
  }

  public void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!this.IsCommitmentSyncRequired(sender, e.Row, e.OldRow))
      return;
    this.SyncCommitment(sender, e.Row);
  }

  public abstract void DocumentRowUpdated(PXCache sender, PXRowUpdatedEventArgs e);

  protected abstract bool IsCommitmentSyncRequired(PXCache sender, object row, object oldRow);

  protected abstract bool EraseCommitment(PXCache sender, object row);

  protected abstract int? GetAccountGroup(PXCache sender, object row);

  protected abstract PMCommitment FromRecord(PXCache sender, object row);

  protected virtual void SyncCommitment(PXCache sender, object row)
  {
    this.SyncCommitment(sender, row, false);
  }

  protected virtual void SyncCommitment(PXCache sender, object row, bool skipCommitmentSelect)
  {
    if (!this.IsCommitmentTrackingEnabled(sender))
      return;
    PXCache cach = sender.Graph.Caches[this.detailEntity];
    Guid? commitmentID = (Guid?) cach.GetValue(row, this.FieldOrdinal);
    if (this.EraseCommitment(sender, row))
    {
      this.DeleteCommitment(sender, commitmentID);
      cach.SetValue(row, this.FieldOrdinal, (object) null);
      GraphHelper.MarkUpdated(cach, row, true);
    }
    else
    {
      int? accountGroup = this.GetAccountGroup(sender, row);
      PMCommitment pmCommitment1 = (PMCommitment) null;
      if (!skipCommitmentSelect && commitmentID.HasValue)
        pmCommitment1 = PXResultset<PMCommitment>.op_Implicit(PXSelectBase<PMCommitment, PXSelect<PMCommitment, Where<PMCommitment.commitmentID, Equal<Required<PMCommitment.commitmentID>>>>.Config>.Select(sender.Graph, new object[1]
        {
          (object) commitmentID
        }));
      if (pmCommitment1 == null)
      {
        PMCommitment pmCommitment2 = this.FromRecord(sender, row);
        sender.Graph.Caches[typeof (PMCommitment)].Insert((object) pmCommitment2);
        Guid? commitmentId = pmCommitment2.CommitmentID;
        Guid? nullable = commitmentID;
        if ((commitmentId.HasValue == nullable.HasValue ? (commitmentId.HasValue ? (commitmentId.GetValueOrDefault() != nullable.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
          return;
        cach.SetValue(row, this.FieldOrdinal, (object) pmCommitment2.CommitmentID);
        GraphHelper.MarkUpdated(cach, row, true);
      }
      else
      {
        PMCommitment pmCommitment3 = this.FromRecord(sender, row);
        pmCommitment1.AccountGroupID = accountGroup;
        pmCommitment1.Status = pmCommitment3.Status;
        pmCommitment1.ProjectID = pmCommitment3.ProjectID;
        pmCommitment1.ProjectTaskID = pmCommitment3.TaskID;
        pmCommitment1.UOM = pmCommitment3.UOM;
        pmCommitment1.OrigQty = pmCommitment3.OrigQty;
        pmCommitment1.OrigAmount = pmCommitment3.OrigAmount;
        pmCommitment1.Qty = pmCommitment3.Qty;
        pmCommitment1.Amount = pmCommitment3.Amount;
        pmCommitment1.ReceivedQty = pmCommitment3.ReceivedQty;
        pmCommitment1.OpenQty = pmCommitment3.OpenQty;
        pmCommitment1.OpenAmount = pmCommitment3.OpenAmount;
        pmCommitment1.InvoicedQty = pmCommitment3.InvoicedQty;
        pmCommitment1.InvoicedAmount = pmCommitment3.InvoicedAmount;
        pmCommitment1.RefNoteID = pmCommitment3.RefNoteID;
        pmCommitment1.InventoryID = pmCommitment3.InventoryID;
        pmCommitment1.CostCodeID = pmCommitment3.CostCodeID;
        pmCommitment1.BranchID = pmCommitment3.BranchID;
        sender.Graph.Caches[typeof (PMCommitment)].Update((object) pmCommitment1);
      }
    }
  }

  protected virtual void DeleteCommitment(PXCache sender, Guid? commitmentID)
  {
    if (!commitmentID.HasValue)
      return;
    PMCommitment pmCommitment = PXResultset<PMCommitment>.op_Implicit(PXSelectBase<PMCommitment, PXSelect<PMCommitment, Where<PMCommitment.commitmentID, Equal<Required<PMCommitment.commitmentID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) commitmentID
    }));
    if (pmCommitment == null)
      return;
    sender.Graph.Caches[typeof (PMCommitment)].Delete((object) pmCommitment);
  }

  protected virtual bool IsCommitmentTrackingEnabled(PXCache sender)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
      return false;
    PMSetup pmSetup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select(sender.Graph, Array.Empty<object>()));
    return pmSetup != null && pmSetup.CostCommitmentTracking.GetValueOrDefault();
  }

  public static Decimal CuryConvCury(IPXCurrencyRate foundRate, Decimal baseval, int? precision)
  {
    if (baseval == 0M)
      return 0M;
    if (foundRate == null)
      throw new ArgumentNullException(nameof (foundRate));
    Decimal num;
    try
    {
      num = foundRate.CuryRate.Value;
    }
    catch (InvalidOperationException ex)
    {
      throw new PXRateNotFoundException();
    }
    if (num == 0.0M)
      num = 1.0M;
    Decimal d = foundRate.CuryMultDiv != "D" ? baseval * num : baseval / num;
    if (precision.HasValue)
      d = Decimal.Round(d, precision.Value, MidpointRounding.AwayFromZero);
    return d;
  }

  public static void Sync(PXCache sender, object data)
  {
    PMCommitmentAttribute commitmentAttribute = (PMCommitmentAttribute) null;
    foreach (PXEventSubscriberAttribute subscriberAttribute in sender.GetAttributesReadonly("commitmentID"))
    {
      if (subscriberAttribute is PMCommitmentAttribute)
      {
        commitmentAttribute = (PMCommitmentAttribute) subscriberAttribute;
        break;
      }
    }
    commitmentAttribute?.SyncCommitment(sender, data, true);
  }
}
