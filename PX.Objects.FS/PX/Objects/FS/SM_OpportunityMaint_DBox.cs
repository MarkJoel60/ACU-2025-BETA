// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_OpportunityMaint_DBox
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.FS;

public class SM_OpportunityMaint_DBox : 
  DialogBoxSOApptCreation<SM_OpportunityMaint, OpportunityMaint, CROpportunity>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  public virtual void Configure(PXScreenConfiguration configuration)
  {
    SM_OpportunityMaint_DBox.Configure(configuration.GetScreenConfigurationContext<OpportunityMaint, CROpportunity>());
  }

  protected static void Configure(
    WorkflowContext<OpportunityMaint, CROpportunity> context)
  {
    BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured categoryServices = context.Categories.CreateNew("Services", (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured>) (c => (BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured) c.DisplayName("Services")));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<OpportunityMaint, CROpportunity>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<OpportunityMaint, CROpportunity>.ScreenConfiguration.ConfiguratorScreen>) (config => config.WithActions((Action<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.ContainerAdjusterActions>) (a =>
    {
      a.Add<SM_OpportunityMaint_DBox>((Expression<Func<SM_OpportunityMaint_DBox, PXAction<CROpportunity>>>) (e => e.CreateSrvOrdDocument), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured>) (c => (BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured) c.WithCategory(categoryServices)));
      a.Add<SM_OpportunityMaint_DBox>((Expression<Func<SM_OpportunityMaint_DBox, PXAction<CROpportunity>>>) (e => e.CreateApptDocument), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured>) (c => (BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured) c.WithCategory(categoryServices)));
    })).UpdateDefaultFlow((Func<BoundedTo<OpportunityMaint, CROpportunity>.Workflow.ConfiguratorFlow, BoundedTo<OpportunityMaint, CROpportunity>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<OpportunityMaint, CROpportunity>.BaseFlowStep.ContainerAdjusterStates>) (states =>
    {
      states.Update("N", (Func<BoundedTo<OpportunityMaint, CROpportunity>.FlowState.ConfiguratorState, BoundedTo<OpportunityMaint, CROpportunity>.FlowState.ConfiguratorState>) (state => state.WithActions((Action<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.ContainerAdjusterActions>) (actions =>
      {
        actions.Add<SM_OpportunityMaint_DBox>((Expression<Func<SM_OpportunityMaint_DBox, PXAction<CROpportunity>>>) (e => e.CreateSrvOrdDocument), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
        actions.Add<SM_OpportunityMaint_DBox>((Expression<Func<SM_OpportunityMaint_DBox, PXAction<CROpportunity>>>) (e => e.CreateApptDocument), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
      }))));
      states.Update("O", (Func<BoundedTo<OpportunityMaint, CROpportunity>.FlowState.ConfiguratorState, BoundedTo<OpportunityMaint, CROpportunity>.FlowState.ConfiguratorState>) (state => state.WithActions((Action<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.ContainerAdjusterActions>) (actions =>
      {
        actions.Add<SM_OpportunityMaint_DBox>((Expression<Func<SM_OpportunityMaint_DBox, PXAction<CROpportunity>>>) (e => e.CreateSrvOrdDocument), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
        actions.Add<SM_OpportunityMaint_DBox>((Expression<Func<SM_OpportunityMaint_DBox, PXAction<CROpportunity>>>) (e => e.CreateApptDocument), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
      }))));
      states.Update("W", (Func<BoundedTo<OpportunityMaint, CROpportunity>.FlowState.ConfiguratorState, BoundedTo<OpportunityMaint, CROpportunity>.FlowState.ConfiguratorState>) (state => state.WithActions((Action<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.ContainerAdjusterActions>) (actions =>
      {
        actions.Add<SM_OpportunityMaint_DBox>((Expression<Func<SM_OpportunityMaint_DBox, PXAction<CROpportunity>>>) (e => e.CreateSrvOrdDocument), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
        actions.Add<SM_OpportunityMaint_DBox>((Expression<Func<SM_OpportunityMaint_DBox, PXAction<CROpportunity>>>) (e => e.CreateApptDocument), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
      }))));
    }))))));
  }

  protected virtual void _(PX.Data.Events.RowSelected<CROpportunity> e)
  {
    if (e.Row == null)
      return;
    int num = this.Base1.GetFSSetup() != null ? 1 : 0;
    bool flag1 = ((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Cache.GetStatus((object) ((PXSelectBase<CROpportunity>) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Current) == 2;
    FSxCROpportunity extension = ((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Cache.GetExtension<FSxCROpportunity>((object) e.Row);
    bool flag2 = num != 0 && e.Row != null && !flag1 && extension?.ServiceOrderRefNbr == null;
    if (flag2)
    {
      CRQuote crQuote = ((PXSelectBase<CRQuote>) ((PXGraphExtension<OpportunityMaint>) this).Base.PrimaryQuoteQuery).SelectSingle(Array.Empty<object>());
      bool flag3 = crQuote != null;
      flag2 = (!flag3 || crQuote.Status == "A" || crQuote.Status == "S" || crQuote.Status == "T" || crQuote.Status == "D") && (!flag3 || crQuote.QuoteType == "D") && e.Row.BAccountID.HasValue;
    }
    this.ProjectSelectorEnabled = ProjectDefaultAttribute.IsNonProject(e.Row.ProjectID);
    ((PXAction) this.CreateSrvOrdDocument).SetEnabled(flag2);
    ((PXAction) this.CreateApptDocument).SetEnabled(flag2);
  }

  public override void PrepareDBoxDefaults()
  {
    CROpportunity current = ((PXSelectBase<CROpportunity>) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Current;
    ((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Cache.GetExtension<FSxCROpportunity>((object) current);
    if (GraphHelper.RowCast<CROpportunityProducts>((IEnumerable) ((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Products).View.SelectMultiBound(new object[1]
    {
      (object) current
    }, Array.Empty<object>())).Any<CROpportunityProducts>((Func<CROpportunityProducts, bool>) (_ => !_.InventoryID.HasValue)) && ((PXSelectBase<CROpportunity>) ((PXGraphExtension<OpportunityMaint>) this).Base.OpportunityCurrent).Ask("The opportunity contains one or multiple product lines with no inventory item specified. Click OK if you want to ignore these product lines and proceed with creating a service order. Click Cancel if you want to review the product lines and specify inventory items where necessary.", (MessageButtons) 1) == 2)
      return;
    ((PXSelectBase<DBoxDocSettings>) this.DocumentSettings).Current.CustomerID = current.BAccountID;
    ((PXSelectBase) this.DocumentSettings).Cache.SetValueExt<DBoxDocSettings.branchID>((object) ((PXSelectBase<DBoxDocSettings>) this.DocumentSettings).Current, (object) current.BranchID);
    ((PXSelectBase) this.DocumentSettings).Cache.SetValueExt<DBoxDocSettings.projectID>((object) ((PXSelectBase<DBoxDocSettings>) this.DocumentSettings).Current, (object) current.ProjectID);
    ((PXSelectBase<DBoxDocSettings>) this.DocumentSettings).Current.OrderDate = current.CloseDate;
    ((PXSelectBase<DBoxDocSettings>) this.DocumentSettings).Current.Description = current.Subject;
    ((PXSelectBase<DBoxDocSettings>) this.DocumentSettings).Current.LongDescr = current.Details;
    ((PXSelectBase<DBoxDocSettings>) this.DocumentSettings).Current.ScheduledDateTimeBegin = current.CloseDate;
  }

  public override void PrepareHeaderAndDetails(DBoxHeader header, List<DBoxDetails> details)
  {
    if (header == null || ((PXSelectBase<DBoxDocSettings>) this.DocumentSettings).Current == null)
      return;
    CROpportunity current1 = ((PXSelectBase<CROpportunity>) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Current;
    FSxCROpportunity extension1 = ((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Cache.GetExtension<FSxCROpportunity>((object) current1);
    PX.Objects.CR.CRContact current2 = ((PXSelectBase<PX.Objects.CR.CRContact>) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity_Contact).Current;
    CRShippingAddress current3 = ((PXSelectBase<CRShippingAddress>) ((PXGraphExtension<OpportunityMaint>) this).Base.Shipping_Address).Current;
    CRSetup current4 = ((PXSelectBase<CRSetup>) this.Base1.CRSetupRecord).Current;
    extension1.SDEnabled = new bool?(true);
    extension1.BranchLocationID = header.BranchLocationID;
    extension1.SrvOrdType = header.SrvOrdType;
    header.LocationID = current1.LocationID;
    header.CuryID = current1.CuryID;
    header.ContactID = current1.ContactID;
    int? salesPersonId = CRExtensionHelper.GetSalesPersonID((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base, current1.OwnerID);
    if (salesPersonId.HasValue)
      header.SalesPersonID = salesPersonId;
    header.TaxZoneID = current1.TaxZoneID;
    header.Contact = (DBoxHeaderContact) current2;
    header.Address = (DBoxHeaderAddress) (CRAddress) current3;
    header.CopyFiles = current4.CopyFiles;
    header.CopyNotes = current4.CopyNotes;
    header.sourceDocument = (object) current1;
    foreach (PXResult<CROpportunityProducts> pxResult in ((PXSelectBase<CROpportunityProducts>) ((PXGraphExtension<OpportunityMaint>) this).Base.Products).Select(Array.Empty<object>()))
    {
      CROpportunityProducts opportunityProducts = PXResult<CROpportunityProducts>.op_Implicit(pxResult);
      if (opportunityProducts.InventoryID.HasValue)
      {
        DBoxDetails dboxDetails = (DBoxDetails) opportunityProducts;
        PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow((PXGraph) ((PXGraphExtension<OpportunityMaint>) this).Base, dboxDetails.InventoryID);
        dboxDetails.LineType = this.GetLineTypeFromInventoryItem(inventoryItemRow);
        FSxCROpportunityProducts extension2 = ((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Products).Cache.GetExtension<FSxCROpportunityProducts>((object) opportunityProducts);
        dboxDetails.BillingRule = extension2.BillingRule;
        dboxDetails.EstimatedDuration = extension2.EstimatedDuration;
        dboxDetails.POVendorLocationID = extension2.VendorLocationID;
        details.Add(dboxDetails);
      }
    }
  }

  public override void CreateDocument(
    ServiceOrderEntry srvOrdGraph,
    AppointmentEntry apptGraph,
    DBoxHeader header,
    List<DBoxDetails> details)
  {
    this.CreateDocument(srvOrdGraph, apptGraph, "OP", (string) null, ((PXSelectBase<CROpportunity>) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Current?.OpportunityID, new int?(), ((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Opportunity).Cache, ((PXSelectBase) ((PXGraphExtension<OpportunityMaint>) this).Base.Products).Cache, header, details, header.CreateAppointment.GetValueOrDefault());
  }

  public virtual string GetLineTypeFromInventoryItem(PX.Objects.IN.InventoryItem inventoryItemRow)
  {
    if (inventoryItemRow.StkItem.GetValueOrDefault())
      return "SLPRO";
    return !(inventoryItemRow.ItemType == "S") ? "NSTKI" : "SERVI";
  }
}
