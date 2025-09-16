// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.POCommitmentAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.PM;

public class POCommitmentAttribute : PMCommitmentAttribute
{
  public POCommitmentAttribute()
    : base(typeof (PX.Objects.PO.POOrder))
  {
  }

  public override void DocumentRowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    PX.Objects.PO.POOrder row = e.Row as PX.Objects.PO.POOrder;
    PX.Objects.PO.POOrder oldRow = e.OldRow as PX.Objects.PO.POOrder;
    if (!this.IsCommitmentSyncRequired(sender, row, oldRow))
      return;
    foreach (ICommitmentSource selectChild in PXParentAttribute.SelectChildren(sender.Graph.Caches[this.detailEntity], (object) row, this.primaryEntity))
      this.SyncCommitment(sender, (object) selectChild);
  }

  protected override bool EraseCommitment(PXCache sender, object row)
  {
    ICommitmentSource commitmentSource = (ICommitmentSource) row;
    PX.Objects.PO.POOrder poOrder = (PX.Objects.PO.POOrder) PXParentAttribute.SelectParent(sender.Graph.Caches[this.detailEntity], row, typeof (PX.Objects.PO.POOrder));
    if (!commitmentSource.TaskID.HasValue || poOrder.OrderType == "BL" || poOrder.OrderType == "SB")
      return true;
    bool? nullable1 = commitmentSource.Cancelled;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = poOrder.Cancelled;
      if (!nullable1.GetValueOrDefault())
        goto label_8;
    }
    Decimal? nullable2 = commitmentSource.ReceivedQty;
    Decimal num1 = 0M;
    if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
    {
      nullable2 = commitmentSource.BilledQty;
      Decimal num2 = 0M;
      if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
      {
        nullable2 = commitmentSource.BilledAmt;
        Decimal num3 = 0M;
        if (nullable2.GetValueOrDefault() == num3 & nullable2.HasValue)
          return true;
      }
    }
label_8:
    nullable1 = poOrder.Hold;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = poOrder.Approved;
      if (nullable1.GetValueOrDefault())
        return !this.GetAccountGroup(sender, row).HasValue;
    }
    nullable1 = poOrder.LockCommitment;
    return !nullable1.GetValueOrDefault();
  }

  protected override PMCommitment FromRecord(PXCache sender, object row)
  {
    ICommitmentSource poline = (ICommitmentSource) row;
    PX.Objects.PO.POOrder poOrder = (PX.Objects.PO.POOrder) PXParentAttribute.SelectParent(sender.Graph.Caches[this.detailEntity], row, typeof (PX.Objects.PO.POOrder));
    PMCommitment commitment = new PMCommitment();
    commitment.Type = "I";
    commitment.Status = this.CommitmentStatusFromSource(poline);
    commitment.CommitmentID = new Guid?(poline.CommitmentID ?? Guid.NewGuid());
    commitment.AccountGroupID = this.GetAccountGroup(sender, row);
    commitment.ProjectID = poline.ProjectID;
    commitment.ProjectTaskID = poline.TaskID;
    commitment.UOM = poline.UOM;
    Decimal? nullable1;
    if (!poline.OrigExtCost.HasValue)
    {
      PMCommitment pmCommitment = commitment;
      nullable1 = poline.OrderQty;
      Decimal? nullable2 = new Decimal?(nullable1.GetValueOrDefault());
      pmCommitment.OrigQty = nullable2;
    }
    else
    {
      PMCommitment pmCommitment = commitment;
      nullable1 = poline.OrigOrderQty;
      Decimal? nullable3 = new Decimal?(nullable1.GetValueOrDefault());
      pmCommitment.OrigQty = nullable3;
    }
    commitment.Qty = poline.OrderQty;
    IProjectMultiCurrency instance = ServiceLocator.Current.GetInstance<IProjectMultiCurrency>();
    PMProject project = PMProject.PK.Find(sender.Graph, commitment.ProjectID);
    nullable1 = poline.OrigExtCost;
    Decimal? nullable4;
    if (!nullable1.HasValue)
    {
      nullable1 = poline.CuryExtCost;
      Decimal valueOrDefault = poline.CuryRetainageAmt.GetValueOrDefault();
      nullable4 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault) : new Decimal?();
    }
    else
      nullable4 = poline.OrigExtCost;
    nullable1 = poline.CuryExtCost;
    Decimal valueOrDefault1 = poline.CuryRetainageAmt.GetValueOrDefault();
    Decimal? nullable5 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
    Decimal? curyBilledAmt = poline.CuryBilledAmt;
    commitment.OrigAmount = new Decimal?(instance.GetValueInProjectCurrency(sender.Graph, project, poOrder.CuryID, poOrder.OrderDate, nullable4));
    commitment.Amount = new Decimal?(instance.GetValueInProjectCurrency(sender.Graph, project, poOrder.CuryID, poOrder.OrderDate, nullable5));
    commitment.InvoicedAmount = new Decimal?(instance.GetValueInProjectCurrency(sender.Graph, project, poOrder.CuryID, poOrder.OrderDate, curyBilledAmt));
    commitment.ReceivedQty = poline.CompletedQty;
    commitment.InvoicedQty = poline.BilledQty;
    commitment.OpenQty = new Decimal?(commitment.Status == "O" ? this.CalculateOpenQty(commitment, poline.CompletePOLine) : 0M);
    commitment.OpenAmount = new Decimal?(commitment.Status == "O" ? this.CalculateOpenAmount(commitment, poline.CompletePOLine) : 0M);
    commitment.RefNoteID = poOrder.NoteID;
    PMCommitment pmCommitment1 = commitment;
    int? nullable6 = poline.InventoryID;
    int? nullable7 = new int?(nullable6 ?? PMInventorySelectorAttribute.EmptyInventoryID);
    pmCommitment1.InventoryID = nullable7;
    PMCommitment pmCommitment2 = commitment;
    nullable6 = poline.CostCodeID;
    int? nullable8 = new int?(nullable6 ?? CostCodeAttribute.GetDefaultCostCode());
    pmCommitment2.CostCodeID = nullable8;
    commitment.BranchID = poline.BranchID;
    return commitment;
  }

  private string CommitmentStatusFromSource(ICommitmentSource poline)
  {
    if (poline.Closed.GetValueOrDefault())
      return "C";
    return poline.Cancelled.GetValueOrDefault() ? "X" : "O";
  }

  protected virtual Decimal CalculateReceivedAmount(PMCommitment commitment)
  {
    if (commitment.Qty.GetValueOrDefault() == 0M)
      return 0M;
    Decimal? nullable = commitment.Amount;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = commitment.Qty;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    return valueOrDefault1 / valueOrDefault2 * commitment.ReceivedQty.GetValueOrDefault();
  }

  protected virtual Decimal CalculateUnbilledAmount(PMCommitment commitment)
  {
    Decimal? nullable = commitment.Amount;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = commitment.InvoicedAmount;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    return valueOrDefault1 - valueOrDefault2;
  }

  protected virtual Decimal CalculateOpenQty(PMCommitment commitment, string completeMethod)
  {
    return completeMethod == "A" ? this.CalculateOpenQtyByAmount(commitment) : this.CalculateOpenQtyByQuantity(commitment);
  }

  protected virtual Decimal CalculateOpenQtyByAmount(PMCommitment commitment)
  {
    Decimal? nullable = commitment.Qty;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = commitment.ReceivedQty;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    Decimal val1 = valueOrDefault1 - valueOrDefault2;
    nullable = commitment.Qty;
    Decimal valueOrDefault3 = nullable.GetValueOrDefault();
    nullable = commitment.InvoicedQty;
    Decimal valueOrDefault4 = nullable.GetValueOrDefault();
    Decimal val2 = valueOrDefault3 - valueOrDefault4;
    return Math.Min(val1, val2);
  }

  protected virtual Decimal CalculateOpenQtyByQuantity(PMCommitment commitment)
  {
    return this.CalculateOpenQtyByAmount(commitment);
  }

  protected virtual Decimal CalculateOpenAmount(PMCommitment commitment, string completeMethod)
  {
    return completeMethod == "A" ? this.CalculateOpenAmountByAmount(commitment) : this.CalculateOpenAmountByQuantity(commitment);
  }

  protected Decimal CalculateOpenAmountByAmount(PMCommitment commitment)
  {
    Decimal receivedAmount = this.CalculateReceivedAmount(commitment);
    Decimal unbilledAmount = this.CalculateUnbilledAmount(commitment);
    Decimal val1 = commitment.Amount.GetValueOrDefault() - receivedAmount;
    return val1 < 0M && unbilledAmount <= 0M ? Math.Max(val1, unbilledAmount) : Math.Min(val1, unbilledAmount);
  }

  protected Decimal CalculateOpenAmountByQuantity(PMCommitment commitment)
  {
    if (commitment.Qty.GetValueOrDefault() == 0M)
      return Math.Min(0M, this.CalculateUnbilledAmount(commitment));
    Decimal? nullable1 = commitment.Amount;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = commitment.Qty;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal num1 = valueOrDefault1 / valueOrDefault2;
    Decimal valueOrDefault3 = commitment.Qty.GetValueOrDefault();
    Decimal? nullable2 = commitment.ReceivedQty;
    Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
    Decimal val1 = valueOrDefault3 - valueOrDefault4;
    nullable2 = commitment.Qty;
    Decimal valueOrDefault5 = nullable2.GetValueOrDefault();
    nullable2 = commitment.InvoicedQty;
    Decimal valueOrDefault6 = nullable2.GetValueOrDefault();
    Decimal val2 = valueOrDefault5 - valueOrDefault6;
    Decimal num2 = Math.Min(val1, val2);
    return num1 * num2;
  }

  protected override int? GetAccountGroup(PXCache sender, object row)
  {
    ICommitmentSource line = (ICommitmentSource) row;
    return line.LineType == "PG" && line.ExpenseAcctID.HasValue ? POCommitmentAttribute.GetAccountGroupFromAccountID(sender.Graph, line.ExpenseAcctID) : POCommitmentAttribute.GetAccountGroupID(sender.Graph, line);
  }

  public static int? GetAccountGroupID(PXGraph graph, PX.Objects.PO.POLine line)
  {
    return POCommitmentAttribute.GetAccountGroupID(graph, (ICommitmentSource) line);
  }

  private static int? GetAccountGroupID(PXGraph graph, ICommitmentSource line)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(graph, line.InventoryID);
    return inventoryItem != null && inventoryItem.StkItem.GetValueOrDefault() && inventoryItem.COGSAcctID.HasValue ? POCommitmentAttribute.GetAccountGroupFromAccountID(graph, inventoryItem.COGSAcctID) : POCommitmentAttribute.GetAccountGroupFromAccountID(graph, line.ExpenseAcctID);
  }

  private static int? GetAccountGroupFromAccountID(PXGraph graph, int? accountID)
  {
    PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find(graph, accountID);
    return account != null && account.AccountGroupID.HasValue ? account.AccountGroupID : new int?();
  }

  protected override bool IsCommitmentSyncRequired(PXCache sender, object row, object oldRow)
  {
    return this.IsCommitmentSyncRequired((ICommitmentSource) row, (ICommitmentSource) oldRow);
  }

  private bool IsCommitmentSyncRequired(ICommitmentSource row, ICommitmentSource oldRow)
  {
    Decimal? orderQty1 = row.OrderQty;
    Decimal? orderQty2 = oldRow.OrderQty;
    if (orderQty1.GetValueOrDefault() == orderQty2.GetValueOrDefault() & orderQty1.HasValue == orderQty2.HasValue)
    {
      Decimal? extCost1 = row.ExtCost;
      Decimal? extCost2 = oldRow.ExtCost;
      if (extCost1.GetValueOrDefault() == extCost2.GetValueOrDefault() & extCost1.HasValue == extCost2.HasValue)
      {
        Decimal? billedQty1 = row.BilledQty;
        Decimal? billedQty2 = oldRow.BilledQty;
        if (billedQty1.GetValueOrDefault() == billedQty2.GetValueOrDefault() & billedQty1.HasValue == billedQty2.HasValue)
        {
          Decimal? receivedQty1 = row.ReceivedQty;
          Decimal? receivedQty2 = oldRow.ReceivedQty;
          if (receivedQty1.GetValueOrDefault() == receivedQty2.GetValueOrDefault() & receivedQty1.HasValue == receivedQty2.HasValue)
          {
            Decimal? completedQty1 = row.CompletedQty;
            Decimal? completedQty2 = oldRow.CompletedQty;
            if (completedQty1.GetValueOrDefault() == completedQty2.GetValueOrDefault() & completedQty1.HasValue == completedQty2.HasValue)
            {
              Decimal? billedQty3 = row.BilledQty;
              Decimal? billedQty4 = oldRow.BilledQty;
              if (billedQty3.GetValueOrDefault() == billedQty4.GetValueOrDefault() & billedQty3.HasValue == billedQty4.HasValue)
              {
                Decimal? billedAmt1 = row.BilledAmt;
                Decimal? billedAmt2 = oldRow.BilledAmt;
                if (billedAmt1.GetValueOrDefault() == billedAmt2.GetValueOrDefault() & billedAmt1.HasValue == billedAmt2.HasValue)
                {
                  int? projectId1 = row.ProjectID;
                  int? projectId2 = oldRow.ProjectID;
                  if (projectId1.GetValueOrDefault() == projectId2.GetValueOrDefault() & projectId1.HasValue == projectId2.HasValue)
                  {
                    int? taskId1 = row.TaskID;
                    int? taskId2 = oldRow.TaskID;
                    if (taskId1.GetValueOrDefault() == taskId2.GetValueOrDefault() & taskId1.HasValue == taskId2.HasValue)
                    {
                      int? expenseAcctId1 = row.ExpenseAcctID;
                      int? expenseAcctId2 = oldRow.ExpenseAcctID;
                      if (expenseAcctId1.GetValueOrDefault() == expenseAcctId2.GetValueOrDefault() & expenseAcctId1.HasValue == expenseAcctId2.HasValue)
                      {
                        int? inventoryId1 = row.InventoryID;
                        int? inventoryId2 = oldRow.InventoryID;
                        if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
                        {
                          int? costCodeId1 = row.CostCodeID;
                          int? costCodeId2 = oldRow.CostCodeID;
                          if (costCodeId1.GetValueOrDefault() == costCodeId2.GetValueOrDefault() & costCodeId1.HasValue == costCodeId2.HasValue && !(row.UOM != oldRow.UOM))
                          {
                            bool? completed1 = row.Completed;
                            bool? completed2 = oldRow.Completed;
                            if (completed1.GetValueOrDefault() == completed2.GetValueOrDefault() & completed1.HasValue == completed2.HasValue)
                            {
                              bool? cancelled1 = row.Cancelled;
                              bool? cancelled2 = oldRow.Cancelled;
                              if (cancelled1.GetValueOrDefault() == cancelled2.GetValueOrDefault() & cancelled1.HasValue == cancelled2.HasValue)
                              {
                                bool? closed1 = row.Closed;
                                bool? closed2 = oldRow.Closed;
                                return !(closed1.GetValueOrDefault() == closed2.GetValueOrDefault() & closed1.HasValue == closed2.HasValue);
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
    return true;
  }

  protected virtual bool IsCommitmentSyncRequired(PXCache sender, PX.Objects.PO.POOrder row, PX.Objects.PO.POOrder oldRow)
  {
    bool? valueOriginal = (bool?) sender.GetValueOriginal<PX.Objects.PO.POOrder.approved>((object) row);
    bool? hold1 = row.Hold;
    bool? hold2 = oldRow.Hold;
    if (hold1.GetValueOrDefault() == hold2.GetValueOrDefault() & hold1.HasValue == hold2.HasValue)
    {
      bool? nullable = row.Cancelled;
      bool? cancelled = oldRow.Cancelled;
      if (nullable.GetValueOrDefault() == cancelled.GetValueOrDefault() & nullable.HasValue == cancelled.HasValue)
      {
        bool? approved = row.Approved;
        nullable = oldRow.Approved;
        if (approved.GetValueOrDefault() == nullable.GetValueOrDefault() & approved.HasValue == nullable.HasValue)
        {
          nullable = row.Approved;
          return nullable.GetValueOrDefault() && !valueOriginal.GetValueOrDefault();
        }
      }
    }
    return true;
  }

  protected override bool IsCommitmentTrackingEnabled(PXCache sender)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
      return false;
    PMSetup pmSetup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select(sender.Graph, Array.Empty<object>()));
    return pmSetup != null && pmSetup.CostCommitmentTracking.GetValueOrDefault();
  }
}
