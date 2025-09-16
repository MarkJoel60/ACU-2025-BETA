// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMBudgetAccumAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.PM;

public class PMBudgetAccumAttribute : PXAccumulatorAttribute
{
  public PMBudgetAccumAttribute() => this._SingleRecord = true;

  protected virtual bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  {
    if (!base.PrepareInsert(sender, row, columns))
      return false;
    PMBudget pmBudget = (PMBudget) row;
    columns.Update<PMBudget.curyDraftChangeOrderAmount>((object) pmBudget.CuryDraftChangeOrderAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.draftChangeOrderAmount>((object) pmBudget.DraftChangeOrderAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.draftChangeOrderQty>((object) pmBudget.DraftChangeOrderQty, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.curyChangeOrderAmount>((object) pmBudget.CuryChangeOrderAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.changeOrderAmount>((object) pmBudget.ChangeOrderAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.changeOrderQty>((object) pmBudget.ChangeOrderQty, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.curyInvoicedAmount>((object) pmBudget.CuryInvoicedAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.invoicedQty>((object) pmBudget.InvoicedQty, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.invoicedAmount>((object) pmBudget.InvoicedAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.actualQty>((object) pmBudget.ActualQty, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.curyActualAmount>((object) pmBudget.CuryActualAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.actualAmount>((object) pmBudget.ActualAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.curyInclTaxAmount>((object) pmBudget.CuryInclTaxAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.inclTaxAmount>((object) pmBudget.InclTaxAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.committedQty>((object) pmBudget.CommittedQty, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.curyCommittedAmount>((object) pmBudget.CuryCommittedAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.committedAmount>((object) pmBudget.CommittedAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.committedOrigQty>((object) pmBudget.CommittedOrigQty, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.curyCommittedOrigAmount>((object) pmBudget.CuryCommittedOrigAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.committedOrigAmount>((object) pmBudget.CommittedOrigAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.committedCOQty>((object) pmBudget.CommittedCOQty, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.curyCommittedCOAmount>((object) pmBudget.CuryCommittedCOAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.committedCOAmount>((object) pmBudget.CommittedCOAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.committedOpenQty>((object) pmBudget.CommittedOpenQty, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.curyCommittedOpenAmount>((object) pmBudget.CuryCommittedOpenAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.committedOpenAmount>((object) pmBudget.CommittedOpenAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.committedReceivedQty>((object) pmBudget.CommittedReceivedQty, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.committedInvoicedQty>((object) pmBudget.CommittedInvoicedQty, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.curyCommittedInvoicedAmount>((object) pmBudget.CuryCommittedInvoicedAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.committedInvoicedAmount>((object) pmBudget.CommittedInvoicedAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.curyRetainedAmount>((object) pmBudget.CuryRetainedAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.retainedAmount>((object) pmBudget.RetainedAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.curyDraftRetainedAmount>((object) pmBudget.CuryDraftRetainedAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.draftRetainedAmount>((object) pmBudget.DraftRetainedAmount, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<PMBudget.description>((object) pmBudget.Description, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<PMBudget.type>((object) pmBudget.Type, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<PMBudget.uOM>((object) pmBudget.UOM, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<PMBudget.productivityTracking>((object) pmBudget.ProductivityTracking, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<PMBudget.curyUnitRate>((object) pmBudget.CuryUnitRate, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<PMBudget.rate>((object) (pmBudget.Rate ?? pmBudget.CuryUnitRate.GetValueOrDefault()), (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<PMBudget.curyInfoID>((object) pmBudget.CuryInfoID, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<PMBudget.retainagePct>((object) pmBudget.RetainagePct, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<PMBudget.progressBillingBase>((object) (pmBudget.ProgressBillingBase ?? "A"), (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<PMBudget.revenueTaskID>((object) pmBudget.RevenueTaskID, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<PMBudget.taxCategoryID>((object) pmBudget.TaxCategoryID, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<PMBudget.mode>((object) "A", (PXDataFieldAssign.AssignBehavior) 0);
    return true;
  }
}
