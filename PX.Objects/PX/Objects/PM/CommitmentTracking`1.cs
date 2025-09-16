// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.CommitmentTracking`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM.Extensions;
using PX.Objects.CM.TemporaryHelpers;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.PM;

public class CommitmentTracking<T> : PXGraphExtension<T> where T : PXGraph
{
  public PXSelect<PMBudgetAccum> Budget;
  public PXSelect<PMCommitment> InternalCommitments;

  [InjectDependency]
  public IBudgetService BudgetService { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  protected virtual void _(Events.RowInserted<PMCommitment> e)
  {
    this.RollUpCommitmentBalance(e.Row, 1);
  }

  protected virtual void _(Events.RowUpdated<PMCommitment> e)
  {
    this.RollUpCommitmentBalance(e.OldRow, -1);
    this.RollUpCommitmentBalance(e.Row, 1);
  }

  protected virtual void _(Events.RowDeleted<PMCommitment> e)
  {
    this.RollUpCommitmentBalance(e.Row, -1);
    this.ClearEmptyRecords();
  }

  public virtual void RollUpCommitmentBalance(PMCommitment row, int sign)
  {
    int? nullable1 = row != null ? row.ProjectID : throw new ArgumentNullException();
    if (!nullable1.HasValue)
      return;
    nullable1 = row.ProjectTaskID;
    if (!nullable1.HasValue)
      return;
    nullable1 = row.AccountGroupID;
    if (!nullable1.HasValue)
      return;
    PMAccountGroup accountGroup = PXSelectorAttribute.Select<PMCommitment.accountGroupID>(this.Base.Caches[typeof (PMCommitment)], (object) row) as PMAccountGroup;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, row.ProjectID);
    PX.Objects.PM.Lite.PMBudget budget1 = this.BudgetService.SelectProjectBalance((IProjectFilter) row, accountGroup, pmProject, out bool _);
    ProjectBalance projectBalance = this.CreateProjectBalance();
    Decimal rollupQty1 = projectBalance.CalculateRollupQty<PMCommitment>(row, (IQuantify) budget1, row.OrigQty);
    Decimal rollupQty2 = projectBalance.CalculateRollupQty<PMCommitment>(row, (IQuantify) budget1, row.Qty);
    Decimal rollupQty3 = projectBalance.CalculateRollupQty<PMCommitment>(row, (IQuantify) budget1, row.OpenQty);
    Decimal rollupQty4 = projectBalance.CalculateRollupQty<PMCommitment>(row, (IQuantify) budget1, row.ReceivedQty);
    Decimal rollupQty5 = projectBalance.CalculateRollupQty<PMCommitment>(row, (IQuantify) budget1, row.InvoicedQty);
    PXSelect<PMBudgetAccum> budget2 = this.Budget;
    PMBudgetAccum pmBudgetAccum1 = new PMBudgetAccum();
    pmBudgetAccum1.ProjectID = budget1.ProjectID;
    pmBudgetAccum1.ProjectTaskID = budget1.ProjectTaskID;
    pmBudgetAccum1.AccountGroupID = budget1.AccountGroupID;
    pmBudgetAccum1.Type = budget1.Type;
    pmBudgetAccum1.InventoryID = budget1.InventoryID;
    pmBudgetAccum1.CostCodeID = budget1.CostCodeID;
    pmBudgetAccum1.UOM = budget1.UOM;
    pmBudgetAccum1.IsProduction = budget1.IsProduction;
    pmBudgetAccum1.Description = budget1.Description;
    pmBudgetAccum1.CuryInfoID = pmProject.CuryInfoID;
    PMBudgetAccum pmBudgetAccum2 = ((PXSelectBase<PMBudgetAccum>) budget2).Insert(pmBudgetAccum1);
    if (budget1.Type == "E")
    {
      PMTask pmTask = PMTask.PK.Find((PXGraph) this.Base, budget1.ProjectID, budget1.TaskID);
      if (pmTask != null && pmTask.Type == "CostRev")
        pmBudgetAccum2.RevenueTaskID = budget1.ProjectTaskID;
    }
    PMBudgetAccum pmBudgetAccum3 = pmBudgetAccum2;
    Decimal? nullable2 = pmBudgetAccum3.CommittedOrigQty;
    Decimal num1 = (Decimal) sign * rollupQty1;
    pmBudgetAccum3.CommittedOrigQty = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num1) : new Decimal?();
    PMBudgetAccum pmBudgetAccum4 = pmBudgetAccum2;
    nullable2 = pmBudgetAccum4.CommittedQty;
    Decimal num2 = (Decimal) sign * rollupQty2;
    pmBudgetAccum4.CommittedQty = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num2) : new Decimal?();
    PMBudgetAccum pmBudgetAccum5 = pmBudgetAccum2;
    nullable2 = pmBudgetAccum5.CommittedOpenQty;
    Decimal num3 = (Decimal) sign * rollupQty3;
    pmBudgetAccum5.CommittedOpenQty = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num3) : new Decimal?();
    PMBudgetAccum pmBudgetAccum6 = pmBudgetAccum2;
    nullable2 = pmBudgetAccum6.CommittedReceivedQty;
    Decimal num4 = (Decimal) sign * rollupQty4;
    pmBudgetAccum6.CommittedReceivedQty = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num4) : new Decimal?();
    PMBudgetAccum pmBudgetAccum7 = pmBudgetAccum2;
    nullable2 = pmBudgetAccum7.CommittedInvoicedQty;
    Decimal num5 = (Decimal) sign * rollupQty5;
    pmBudgetAccum7.CommittedInvoicedQty = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num5) : new Decimal?();
    PMBudgetAccum pmBudgetAccum8 = pmBudgetAccum2;
    nullable2 = pmBudgetAccum8.CuryCommittedOrigAmount;
    Decimal num6 = (Decimal) sign * row.OrigAmount.GetValueOrDefault();
    pmBudgetAccum8.CuryCommittedOrigAmount = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num6) : new Decimal?();
    PMBudgetAccum pmBudgetAccum9 = pmBudgetAccum2;
    nullable2 = pmBudgetAccum9.CuryCommittedAmount;
    Decimal num7 = (Decimal) sign * row.Amount.GetValueOrDefault();
    pmBudgetAccum9.CuryCommittedAmount = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num7) : new Decimal?();
    PMBudgetAccum pmBudgetAccum10 = pmBudgetAccum2;
    nullable2 = pmBudgetAccum10.CuryCommittedOpenAmount;
    Decimal num8 = (Decimal) sign * row.OpenAmount.GetValueOrDefault();
    pmBudgetAccum10.CuryCommittedOpenAmount = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num8) : new Decimal?();
    PMBudgetAccum pmBudgetAccum11 = pmBudgetAccum2;
    nullable2 = pmBudgetAccum11.CuryCommittedInvoicedAmount;
    Decimal num9 = (Decimal) sign * row.InvoicedAmount.GetValueOrDefault();
    pmBudgetAccum11.CuryCommittedInvoicedAmount = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num9) : new Decimal?();
    if (pmProject.CuryID != pmProject.BaseCuryID)
    {
      CurrencyInfo currencyInfo = MultiCurrencyCalculator.GetCurrencyInfo<PMProject.curyInfoID>((PXGraph) this.Base, (object) pmProject);
      PMBudgetAccum pmBudgetAccum12 = pmBudgetAccum2;
      nullable2 = pmBudgetAccum12.CommittedOrigAmount;
      Decimal num10 = (Decimal) sign * currencyInfo.CuryConvBase(row.OrigAmount.GetValueOrDefault());
      pmBudgetAccum12.CommittedOrigAmount = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num10) : new Decimal?();
      PMBudgetAccum pmBudgetAccum13 = pmBudgetAccum2;
      nullable2 = pmBudgetAccum13.CommittedAmount;
      Decimal num11 = (Decimal) sign * currencyInfo.CuryConvBase(row.Amount.GetValueOrDefault());
      pmBudgetAccum13.CommittedAmount = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num11) : new Decimal?();
      PMBudgetAccum pmBudgetAccum14 = pmBudgetAccum2;
      nullable2 = pmBudgetAccum14.CommittedOpenAmount;
      Decimal num12 = (Decimal) sign * currencyInfo.CuryConvBase(row.OpenAmount.GetValueOrDefault());
      pmBudgetAccum14.CommittedOpenAmount = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num12) : new Decimal?();
      PMBudgetAccum pmBudgetAccum15 = pmBudgetAccum2;
      nullable2 = pmBudgetAccum15.CommittedInvoicedAmount;
      Decimal num13 = (Decimal) sign * currencyInfo.CuryConvBase(row.InvoicedAmount.GetValueOrDefault());
      pmBudgetAccum15.CommittedInvoicedAmount = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num13) : new Decimal?();
    }
    else
    {
      PMBudgetAccum pmBudgetAccum16 = pmBudgetAccum2;
      nullable2 = pmBudgetAccum16.CommittedOrigAmount;
      Decimal num14 = (Decimal) sign;
      Decimal? nullable3 = row.OrigAmount;
      Decimal valueOrDefault1 = nullable3.GetValueOrDefault();
      Decimal num15 = num14 * valueOrDefault1;
      Decimal? nullable4;
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable4 = nullable3;
      }
      else
        nullable4 = new Decimal?(nullable2.GetValueOrDefault() + num15);
      pmBudgetAccum16.CommittedOrigAmount = nullable4;
      PMBudgetAccum pmBudgetAccum17 = pmBudgetAccum2;
      nullable2 = pmBudgetAccum17.CommittedAmount;
      Decimal num16 = (Decimal) sign;
      nullable3 = row.Amount;
      Decimal valueOrDefault2 = nullable3.GetValueOrDefault();
      Decimal num17 = num16 * valueOrDefault2;
      Decimal? nullable5;
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable5 = nullable3;
      }
      else
        nullable5 = new Decimal?(nullable2.GetValueOrDefault() + num17);
      pmBudgetAccum17.CommittedAmount = nullable5;
      PMBudgetAccum pmBudgetAccum18 = pmBudgetAccum2;
      nullable2 = pmBudgetAccum18.CommittedOpenAmount;
      Decimal num18 = (Decimal) sign;
      nullable3 = row.OpenAmount;
      Decimal valueOrDefault3 = nullable3.GetValueOrDefault();
      Decimal num19 = num18 * valueOrDefault3;
      Decimal? nullable6;
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable6 = nullable3;
      }
      else
        nullable6 = new Decimal?(nullable2.GetValueOrDefault() + num19);
      pmBudgetAccum18.CommittedOpenAmount = nullable6;
      PMBudgetAccum pmBudgetAccum19 = pmBudgetAccum2;
      nullable2 = pmBudgetAccum19.CommittedInvoicedAmount;
      Decimal num20 = (Decimal) sign;
      nullable3 = row.InvoicedAmount;
      Decimal valueOrDefault4 = nullable3.GetValueOrDefault();
      Decimal num21 = num20 * valueOrDefault4;
      Decimal? nullable7;
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable7 = nullable3;
      }
      else
        nullable7 = new Decimal?(nullable2.GetValueOrDefault() + num21);
      pmBudgetAccum19.CommittedInvoicedAmount = nullable7;
    }
  }

  public virtual ProjectBalance CreateProjectBalance() => new ProjectBalance((PXGraph) this.Base);

  protected virtual void ClearEmptyRecords()
  {
    foreach (PMBudgetAccum pmBudgetAccum in ((PXSelectBase) this.Budget).Cache.Inserted)
    {
      if (this.IsEmptyCommitmentChange(pmBudgetAccum))
        ((PXSelectBase) this.Budget).Cache.Remove((object) pmBudgetAccum);
    }
  }

  private bool IsEmptyCommitmentChange(PMBudgetAccum item)
  {
    return !(item.CommittedOrigQty.GetValueOrDefault() != 0M) && !(item.CommittedQty.GetValueOrDefault() != 0M) && !(item.CommittedOpenQty.GetValueOrDefault() != 0M) && !(item.CommittedReceivedQty.GetValueOrDefault() != 0M) && !(item.CommittedInvoicedQty.GetValueOrDefault() != 0M) && !(item.CuryCommittedOrigAmount.GetValueOrDefault() != 0M) && !(item.CuryCommittedAmount.GetValueOrDefault() != 0M) && !(item.CuryCommittedOpenAmount.GetValueOrDefault() != 0M) && !(item.CuryCommittedInvoicedAmount.GetValueOrDefault() != 0M) && !(item.CommittedOrigAmount.GetValueOrDefault() != 0M) && !(item.CommittedAmount.GetValueOrDefault() != 0M) && !(item.CommittedOpenAmount.GetValueOrDefault() != 0M) && !(item.CommittedInvoicedAmount.GetValueOrDefault() != 0M);
  }
}
