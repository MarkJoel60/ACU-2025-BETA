// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.SC.Graphs.SubcontractEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.PO;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CN.Subcontracts.SC.Graphs;

public class SubcontractEntry_Workflow : PXGraphExtension<SubcontractEntry>
{
  public const string CreatePrepaymentActionName = "createPrepayment";
  public const string UnlinkFromSOActionName = "unlinkFromSO";
  public const string ConvertToNormalActionName = "convertToNormal";
  public const string CreateSalesOrderActionName = "createSalesOrder";
  public const string GenerateSalesOrderActionName = "generateSalesOrder";

  public virtual void Configure(PXScreenConfiguration config)
  {
    SubcontractEntry_Workflow.Configure(config.GetScreenConfigurationContext<SubcontractEntry, POOrder>());
  }

  protected static void Configure(WorkflowContext<SubcontractEntry, POOrder> context)
  {
    SubcontractEntry_Workflow.Conditions conditions = context.Conditions.GetPack<SubcontractEntry_Workflow.Conditions>();
    BoundedTo<SubcontractEntry, POOrder>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<SubcontractEntry, POOrder>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<SubcontractEntry, POOrder>.ActionCategory.IConfigured>) (category => (BoundedTo<SubcontractEntry, POOrder>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<SubcontractEntry, POOrder>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval", (Func<BoundedTo<SubcontractEntry, POOrder>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<SubcontractEntry, POOrder>.ActionCategory.IConfigured>) (category => (BoundedTo<SubcontractEntry, POOrder>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<SubcontractEntry, POOrder>.ActionCategory.IConfigured printingAndEmailingCategory = context.Categories.CreateNew("Printing and Emailing", (Func<BoundedTo<SubcontractEntry, POOrder>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<SubcontractEntry, POOrder>.ActionCategory.IConfigured>) (category => (BoundedTo<SubcontractEntry, POOrder>.ActionCategory.IConfigured) category.DisplayName("Printing and Emailing")));
    BoundedTo<SubcontractEntry, POOrder>.ActionCategory.IConfigured otherCategory = context.Categories.CreateNew("OtherCategory", (Func<BoundedTo<SubcontractEntry, POOrder>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<SubcontractEntry, POOrder>.ActionCategory.IConfigured>) (category => (BoundedTo<SubcontractEntry, POOrder>.ActionCategory.IConfigured) category.DisplayName("Other")));
    context.AddScreenConfigurationFor((Func<BoundedTo<SubcontractEntry, POOrder>.ScreenConfiguration.IStartConfigScreen, BoundedTo<SubcontractEntry, POOrder>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<SubcontractEntry, POOrder>.ScreenConfiguration.IConfigured) ((BoundedTo<SubcontractEntry, POOrder>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<POOrder.status>().FlowTypeIdentifierIs<POOrder.orderType>(true).WithFlows((Action<BoundedTo<SubcontractEntry, POOrder>.Workflow.IContainerFillerFlows>) (flows => flows.Add<POOrderType.regularSubcontract>((Func<BoundedTo<SubcontractEntry, POOrder>.Workflow.INeedStatesFlow, BoundedTo<SubcontractEntry, POOrder>.Workflow.IConfigured>) (flow => (BoundedTo<SubcontractEntry, POOrder>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IContainerFillerStates>) (states =>
    {
      states.Add("_", (Func<BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured) state.IsInitial((Expression<Func<SubcontractEntry, PXAutoAction<POOrder>>>) (g => g.initializeState))));
      states.Add<POOrderStatus.hold>((Func<BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<SubcontractEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.releaseFromHold), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.subcontractAuditReport), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.printSubcontract), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
      }))).WithFieldStates((Action<BoundedTo<SubcontractEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddAllFields<POOrder>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddTable<POLine>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddTable<POTax>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddAllFields<POShipAddress>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddAllFields<POShipContact>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.approved>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddField<POOrder.ownerID>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.workgroupID>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
      }))));
      states.Add<POOrderStatus.pendingPrint>((Func<BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<SubcontractEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.markAsDontPrint), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.printSubcontract), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.subcontractAuditReport), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
      }))).WithFieldStates((Action<BoundedTo<SubcontractEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddAllFields<POOrder>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddField<POOrder.orderType>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.orderNbr>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.controlTotal>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.dontPrint>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddTable<POLine>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddField<POLine.cancelled>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.completed>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.promisedDate>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.closed>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddTable<POTax>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<POShipAddress>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<POShipContact>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddTable<PX.Objects.CM.Extensions.CurrencyInfo>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
      }))).WithEventHandlers((Action<BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<SubcontractEntry, PXWorkflowEventHandler<POOrder>>>) (g => g.OnLinesCompleted));
        handlers.Add((Expression<Func<SubcontractEntry, PXWorkflowEventHandler<POOrder>>>) (g => g.OnLinesClosed));
        handlers.Add((Expression<Func<SubcontractEntry, PXWorkflowEventHandler<POOrder>>>) (g => g.OnPrinted));
        handlers.Add((Expression<Func<SubcontractEntry, PXWorkflowEventHandler<POOrder>>>) (g => g.OnDoNotPrintChecked));
      }))));
      states.Add<POOrderStatus.pendingEmail>((Func<BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<SubcontractEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.emailSubcontract), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.markAsDontEmail), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.printSubcontract), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.subcontractAuditReport), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
      }))).WithFieldStates((Action<BoundedTo<SubcontractEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddAllFields<POOrder>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddField<POOrder.orderType>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.orderNbr>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.controlTotal>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.dontEmail>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddTable<POLine>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddField<POLine.cancelled>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.completed>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.promisedDate>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.closed>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddTable<POTax>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<POShipAddress>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<POShipContact>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddTable<PX.Objects.CM.Extensions.CurrencyInfo>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
      }))).WithEventHandlers((Action<BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<SubcontractEntry, PXWorkflowEventHandler<POOrder>>>) (g => g.OnLinesCompleted));
        handlers.Add((Expression<Func<SubcontractEntry, PXWorkflowEventHandler<POOrder>>>) (g => g.OnLinesClosed));
        handlers.Add((Expression<Func<SubcontractEntry, PXWorkflowEventHandler<POOrder>>>) (g => g.OnDoNotEmailChecked));
      }))));
      states.Add<POOrderStatus.open>((Func<BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<SubcontractEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.complete), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.emailSubcontract), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.createPOReceipt), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.createAPInvoice), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add("createPrepayment", (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.subcontractAuditReport), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.printSubcontract), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
      }))).WithFieldStates((Action<BoundedTo<SubcontractEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddAllFields<POOrder>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddField<POOrder.orderType>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.orderNbr>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.controlTotal>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddTable<POLine>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddField<POLine.cancelled>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.completed>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.promisedDate>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.closed>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddTable<POTax>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<POShipAddress>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<POShipContact>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddTable<PX.Objects.CM.Extensions.CurrencyInfo>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
      }))).WithEventHandlers((Action<BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<SubcontractEntry, PXWorkflowEventHandler<POOrder>>>) (g => g.OnLinesCompleted));
        handlers.Add((Expression<Func<SubcontractEntry, PXWorkflowEventHandler<POOrder>>>) (g => g.OnLinesClosed));
        handlers.Add((Expression<Func<SubcontractEntry, PXWorkflowEventHandler<POOrder>>>) (g => g.OnReleaseChangeOrder));
      }))));
      states.Add<POOrderStatus.completed>((Func<BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<SubcontractEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.reopenOrder), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.createAPInvoice), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add("createPrepayment", (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.subcontractAuditReport), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.printSubcontract), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
      }))).WithFieldStates((Action<BoundedTo<SubcontractEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddTable<POOrder>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddField<POOrder.orderType>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.orderNbr>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddTable<POLine>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddField<POLine.cancelled>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.completed>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.promisedDate>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.closed>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddTable<POTax>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<PORemitAddress>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<PORemitContact>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<POShipAddress>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<POShipContact>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddTable<PX.Objects.CM.Extensions.CurrencyInfo>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
      }))).WithEventHandlers((Action<BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<SubcontractEntry, PXWorkflowEventHandler<POOrder>>>) (g => g.OnLinesClosed));
        handlers.Add((Expression<Func<SubcontractEntry, PXWorkflowEventHandler<POOrder>>>) (g => g.OnLinesReopened));
        handlers.Add((Expression<Func<SubcontractEntry, PXWorkflowEventHandler<POOrder>>>) (g => g.OnReleaseChangeOrder));
      }))));
      states.Add<POOrderStatus.cancelled>((Func<BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<SubcontractEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.reopenOrder), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.subcontractAuditReport), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.printSubcontract), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
      }))).WithFieldStates((Action<BoundedTo<SubcontractEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddTable<POOrder>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddField<POOrder.orderType>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.orderNbr>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddTable<POLine>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddField<POLine.cancelled>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.completed>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.promisedDate>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.closed>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddAllFields<PORemitAddress>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<PORemitContact>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<POShipAddress>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<POShipContact>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddTable<POTax>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddTable<PX.Objects.CM.Extensions.CurrencyInfo>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
      }))).WithEventHandlers((Action<BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<SubcontractEntry, PXWorkflowEventHandler<POOrder>>>) (g => g.OnLinesReopened));
        handlers.Add((Expression<Func<SubcontractEntry, PXWorkflowEventHandler<POOrder>>>) (g => g.OnReleaseChangeOrder));
      }))));
      states.Add<POOrderStatus.closed>((Func<BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<SubcontractEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.reopenOrder), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.subcontractAuditReport), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.printSubcontract), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
      }))).WithFieldStates((Action<BoundedTo<SubcontractEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddTable<POOrder>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddField<POOrder.orderType>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.orderNbr>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddTable<POLine>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddField<POLine.cancelled>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.completed>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.promisedDate>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.closed>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddAllFields<PORemitAddress>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<PORemitContact>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<POShipAddress>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<POShipContact>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddTable<POTax>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddTable<PX.Objects.CM.Extensions.CurrencyInfo>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
      }))).WithEventHandlers((Action<BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<SubcontractEntry, PXWorkflowEventHandler<POOrder>>>) (g => g.OnLinesReopened));
        handlers.Add((Expression<Func<SubcontractEntry, PXWorkflowEventHandler<POOrder>>>) (g => g.OnLinesCompleted));
        handlers.Add((Expression<Func<SubcontractEntry, PXWorkflowEventHandler<POOrder>>>) (g => g.OnReleaseChangeOrder));
      }))));
    })).WithTransitions((Action<BoundedTo<SubcontractEntry, POOrder>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<SubcontractEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.initializeState)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingPrint>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.initializeState)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsPrinted))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingEmail>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.initializeState)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsEmailed))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.cancelled>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.initializeState)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.IsCancelled)));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.closed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.initializeState)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.HasAllLinesClosed)));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.completed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.initializeState)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.HasAllLinesCompleted)));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.open>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.initializeState))));
      }));
      transitions.AddGroupFrom<POOrderStatus.hold>((Action<BoundedTo<SubcontractEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t =>
        {
          BoundedTo<SubcontractEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.pendingPrint>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.releaseFromHold));
          BoundedTo<SubcontractEntry, POOrder>.Condition condition9 = BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsOnHold);
          BoundedTo<SubcontractEntry, POOrder>.Condition condition10 = BoundedTo<SubcontractEntry, POOrder>.Condition.op_False(condition9) ? condition9 : BoundedTo<SubcontractEntry, POOrder>.Condition.op_BitwiseAnd(condition9, BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsPrinted));
          return (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) condition10);
        }));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t =>
        {
          BoundedTo<SubcontractEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.pendingEmail>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.releaseFromHold));
          BoundedTo<SubcontractEntry, POOrder>.Condition condition11 = BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsOnHold);
          BoundedTo<SubcontractEntry, POOrder>.Condition condition12 = BoundedTo<SubcontractEntry, POOrder>.Condition.op_False(condition11) ? condition11 : BoundedTo<SubcontractEntry, POOrder>.Condition.op_BitwiseAnd(condition11, BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsEmailed));
          return (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) condition12);
        }));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t =>
        {
          BoundedTo<SubcontractEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.closed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.releaseFromHold));
          BoundedTo<SubcontractEntry, POOrder>.Condition condition13 = BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsOnHold);
          BoundedTo<SubcontractEntry, POOrder>.Condition condition14 = BoundedTo<SubcontractEntry, POOrder>.Condition.op_False(condition13) ? condition13 : BoundedTo<SubcontractEntry, POOrder>.Condition.op_BitwiseAnd(condition13, conditions.HasAllLinesClosed);
          return (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) condition14);
        }));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t =>
        {
          BoundedTo<SubcontractEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.completed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.releaseFromHold));
          BoundedTo<SubcontractEntry, POOrder>.Condition condition15 = BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsOnHold);
          BoundedTo<SubcontractEntry, POOrder>.Condition condition16 = BoundedTo<SubcontractEntry, POOrder>.Condition.op_False(condition15) ? condition15 : BoundedTo<SubcontractEntry, POOrder>.Condition.op_BitwiseAnd(condition15, conditions.HasAllLinesCompleted);
          return (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) condition16);
        }));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.open>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.releaseFromHold)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsOnHold))));
      }));
      transitions.AddGroupFrom<POOrderStatus.pendingPrint>((Action<BoundedTo<SubcontractEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.putOnHold)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.cancelled>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.cancelOrder)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.IsCancelled)));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingEmail>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnPrinted)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsEmailed)).WithFieldAssignments((Action<BoundedTo<SubcontractEntry, POOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<POOrder.printed>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.closed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnPrinted)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.HasAllLinesClosed).WithFieldAssignments((Action<BoundedTo<SubcontractEntry, POOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<POOrder.printed>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.completed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnPrinted)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.HasAllLinesCompleted).WithFieldAssignments((Action<BoundedTo<SubcontractEntry, POOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<POOrder.printed>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.open>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnPrinted)).WithFieldAssignments((Action<BoundedTo<SubcontractEntry, POOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<POOrder.printed>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingEmail>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnDoNotPrintChecked)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsEmailed)).DoesNotPersist()));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.closed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnDoNotPrintChecked)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.HasAllLinesClosed).DoesNotPersist()));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.completed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnDoNotPrintChecked)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.HasAllLinesCompleted).DoesNotPersist()));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.open>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnDoNotPrintChecked)).DoesNotPersist()));
      }));
      transitions.AddGroupFrom<POOrderStatus.pendingEmail>((Action<BoundedTo<SubcontractEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.putOnHold)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.cancelled>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.cancelOrder)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.IsCancelled)));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.closed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.emailSubcontract)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.HasAllLinesClosed).WithFieldAssignments((Action<BoundedTo<SubcontractEntry, POOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<POOrder.emailed>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.completed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.emailSubcontract)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.HasAllLinesCompleted).WithFieldAssignments((Action<BoundedTo<SubcontractEntry, POOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<POOrder.emailed>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.open>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.emailSubcontract)).WithFieldAssignments((Action<BoundedTo<SubcontractEntry, POOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<POOrder.emailed>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.closed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnDoNotEmailChecked)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.HasAllLinesClosed)));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.completed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnDoNotEmailChecked)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.HasAllLinesCompleted)));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.open>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnDoNotEmailChecked))));
      }));
      transitions.AddGroupFrom<POOrderStatus.open>((Action<BoundedTo<SubcontractEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.putOnHold)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.cancelled>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.cancelOrder)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.IsCancelled)));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.completed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnLinesCompleted)).DoesNotPersist()));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.completed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.complete)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.HasAllLinesCompleted)));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.closed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnLinesClosed))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingPrint>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnReleaseChangeOrder)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsPrinted))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingEmail>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnReleaseChangeOrder)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsEmailed))));
      }));
      transitions.AddGroupFrom<POOrderStatus.cancelled>((Action<BoundedTo<SubcontractEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.reopenOrder)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.open>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnLinesReopened))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingPrint>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnReleaseChangeOrder)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsPrinted))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingEmail>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnReleaseChangeOrder)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsEmailed))));
      }));
      transitions.AddGroupFrom<POOrderStatus.completed>((Action<BoundedTo<SubcontractEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.reopenOrder)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.open>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnLinesReopened))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.closed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnLinesClosed))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingPrint>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnReleaseChangeOrder)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsPrinted))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingEmail>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnReleaseChangeOrder)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsEmailed))));
      }));
      transitions.AddGroupFrom<POOrderStatus.closed>((Action<BoundedTo<SubcontractEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.completed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnLinesCompleted))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.open>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnLinesReopened))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.reopenOrder)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingPrint>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnReleaseChangeOrder)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsPrinted))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingEmail>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnReleaseChangeOrder)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsEmailed))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.open>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXWorkflowEventHandlerBase<POOrder>>>) (g => g.OnReleaseChangeOrder))));
      }));
    })))))).WithActions((Action<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.initializeState), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) null);
      actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.releaseFromHold), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.DisplayName("Remove Hold").InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<SubcontractEntry, POOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<POOrder.hold>(new bool?(false))))));
      actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.DisplayName("Hold").InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<SubcontractEntry, POOrder>.Assignment.IContainerFillerFields>) (fas =>
      {
        fas.Add<POOrder.hold>(new bool?(true));
        fas.Add<POOrder.printed>(new bool?(false));
        fas.Add<POOrder.emailed>(new bool?(false));
        fas.Add<POOrder.cancelled>(new bool?(false));
      })).IsDisabledWhen((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.IsChangeOrder)));
      actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.emailSubcontract), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.InFolder(printingAndEmailingCategory).WithPersistOptions((ActionPersistOptions) 2).MassProcessingScreen<PrintSubcontract>().InBatchMode()));
      actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.markAsDontEmail), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.InFolder(printingAndEmailingCategory).MassProcessingScreen<PrintSubcontract>().InBatchMode().WithFieldAssignments((Action<BoundedTo<SubcontractEntry, POOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<POOrder.dontEmail>(new bool?(true))))));
      actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.createAPInvoice), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.complete), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.DisplayName("Complete").InFolder(processingCategory)));
      actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.DisplayName("Cancel").InFolder(processingCategory).WithFieldAssignments((Action<BoundedTo<SubcontractEntry, POOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<POOrder.cancelled>(new bool?(true))))));
      actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.reopenOrder), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.DisplayName("Reopen").InFolder(processingCategory).WithFieldAssignments((Action<BoundedTo<SubcontractEntry, POOrder>.Assignment.IContainerFillerFields>) (fas =>
      {
        fas.Add<POOrder.hold>(new bool?(true));
        fas.Add<POOrder.printed>(new bool?(false));
        fas.Add<POOrder.emailed>(new bool?(false));
        fas.Add<POOrder.cancelled>(new bool?(false));
      })).IsDisabledWhen((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.IsChangeOrder)));
      actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.InFolder(otherCategory)));
      actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.InFolder(otherCategory)));
      actions.Add("createPrepayment", (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.InFolder((FolderType) 1).WithPersistOptions((ActionPersistOptions) 2)));
      actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.subcontractAuditReport), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.InFolder((FolderType) 2)));
      actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.printSubcontract), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.InFolder(printingAndEmailingCategory).WithPersistOptions((ActionPersistOptions) 2).MassProcessingScreen<PrintSubcontract>().InBatchMode()));
      actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.markAsDontPrint), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.InFolder(printingAndEmailingCategory).MassProcessingScreen<PrintSubcontract>().InBatchMode().WithFieldAssignments((Action<BoundedTo<SubcontractEntry, POOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<POOrder.dontPrint>(new bool?(true))))));
      actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.emailPurchaseOrder), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.IsHiddenAlways()));
      actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.printPurchaseOrder), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.IsHiddenAlways()));
      actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.viewPurchaseOrderReceipt), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.IsHiddenAlways()));
      actions.Add("unlinkFromSO", (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.IsHiddenAlways()));
      actions.Add("convertToNormal", (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.IsHiddenAlways()));
      actions.Add("createSalesOrder", (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.IsHiddenAlways()));
      actions.Add("generateSalesOrder", (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) c.IsHiddenAlways()));
    })).WithHandlers((Action<BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Func<BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<POOrder>) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<POOrder>) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedSubscriber<POOrder>) handler.WithTargetOf<POOrder>().OfEntityEvent<POOrder.Events>((Expression<Func<POOrder.Events, PXEntityEvent<POOrder>>>) (e => e.LinesCompleted))).Is((Expression<Func<POOrder, PXWorkflowEventHandler<POOrder, POOrder>>>) (g => g.OnLinesCompleted))).UsesTargetAsPrimaryEntity()).DisplayName("PO Lines Completed")));
      handlers.Add((Func<BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<POOrder>) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<POOrder>) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedSubscriber<POOrder>) handler.WithTargetOf<POOrder>().OfEntityEvent<POOrder.Events>((Expression<Func<POOrder.Events, PXEntityEvent<POOrder>>>) (e => e.LinesClosed))).Is((Expression<Func<POOrder, PXWorkflowEventHandler<POOrder, POOrder>>>) (g => g.OnLinesClosed))).UsesTargetAsPrimaryEntity()).DisplayName("PO Lines Closed")));
      handlers.Add((Func<BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<POOrder>) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<POOrder>) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedSubscriber<POOrder>) handler.WithTargetOf<POOrder>().OfEntityEvent<POOrder.Events>((Expression<Func<POOrder.Events, PXEntityEvent<POOrder>>>) (e => e.LinesReopened))).Is((Expression<Func<POOrder, PXWorkflowEventHandler<POOrder, POOrder>>>) (g => g.OnLinesReopened))).UsesTargetAsPrimaryEntity()).DisplayName("PO Lines Reopened")));
      handlers.Add((Func<BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<POOrder>) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<POOrder>) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedSubscriber<POOrder>) handler.WithTargetOf<POOrder>().OfEntityEvent<POOrder.Events>((Expression<Func<POOrder.Events, PXEntityEvent<POOrder>>>) (e => e.Printed))).Is((Expression<Func<POOrder, PXWorkflowEventHandler<POOrder, POOrder>>>) (g => g.OnPrinted))).UsesTargetAsPrimaryEntity()).DisplayName("Printed")));
      handlers.Add((Func<BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<POOrder>) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<POOrder>) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedSubscriber<POOrder>) handler.WithTargetOf<POOrder>().OfEntityEvent<POOrder.Events>((Expression<Func<POOrder.Events, PXEntityEvent<POOrder>>>) (e => e.DoNotPrintChecked))).Is((Expression<Func<POOrder, PXWorkflowEventHandler<POOrder, POOrder>>>) (g => g.OnDoNotPrintChecked))).UsesTargetAsPrimaryEntity()).DisplayName("Do Not Print Selected")));
      handlers.Add((Func<BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<POOrder>) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<POOrder>) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedSubscriber<POOrder>) handler.WithTargetOf<POOrder>().OfEntityEvent<POOrder.Events>((Expression<Func<POOrder.Events, PXEntityEvent<POOrder>>>) (e => e.DoNotEmailChecked))).Is((Expression<Func<POOrder, PXWorkflowEventHandler<POOrder, POOrder>>>) (g => g.OnDoNotEmailChecked))).UsesTargetAsPrimaryEntity()).DisplayName("Do Not Email Selected")));
      handlers.Add((Func<BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<POOrder>) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<POOrder>) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<POOrder>) ((BoundedTo<SubcontractEntry, POOrder>.WorkflowEventHandlerDefinition.INeedSubscriber<POOrder>) handler.WithTargetOf<POOrder>().OfEntityEvent<POOrder.Events>((Expression<Func<POOrder.Events, PXEntityEvent<POOrder>>>) (e => e.ReleaseChangeOrder))).Is((Expression<Func<POOrder, PXWorkflowEventHandler<POOrder, POOrder>>>) (g => g.OnReleaseChangeOrder))).UsesTargetAsPrimaryEntity()).WithFieldAssignments((Action<BoundedTo<POOrder, POOrder>.Assignment.IContainerFillerFields>) (fas =>
      {
        fas.Add<POOrder.printed>(new bool?(false));
        fas.Add<POOrder.emailed>(new bool?(false));
        fas.Add<POOrder.cancelled>(new bool?(false));
      }))).DisplayName("Change Order Released")));
    })).WithCategories((Action<BoundedTo<SubcontractEntry, POOrder>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(approvalCategory);
      categories.Add(printingAndEmailingCategory);
      categories.Add(otherCategory);
      categories.Update((FolderType) 1, (Func<BoundedTo<SubcontractEntry, POOrder>.ActionCategory.ConfiguratorCategory, BoundedTo<SubcontractEntry, POOrder>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(printingAndEmailingCategory)));
      categories.Update((FolderType) 2, (Func<BoundedTo<SubcontractEntry, POOrder>.ActionCategory.ConfiguratorCategory, BoundedTo<SubcontractEntry, POOrder>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get((FolderType) 1))));
    }))));
  }

  public class Conditions : BoundedTo<SubcontractEntry, POOrder>.Condition.Pack
  {
    public BoundedTo<SubcontractEntry, POOrder>.Condition IsOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SubcontractEntry, POOrder>.Condition.ConditionBuilder, BoundedTo<SubcontractEntry, POOrder>.Condition>) (b => b.FromBql<BqlOperand<POOrder.hold, IBqlBool>.IsEqual<True>>()), nameof (IsOnHold));
      }
    }

    public BoundedTo<SubcontractEntry, POOrder>.Condition IsCancelled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SubcontractEntry, POOrder>.Condition.ConditionBuilder, BoundedTo<SubcontractEntry, POOrder>.Condition>) (b => b.FromBql<BqlOperand<POOrder.cancelled, IBqlBool>.IsEqual<True>>()), nameof (IsCancelled));
      }
    }

    public BoundedTo<SubcontractEntry, POOrder>.Condition IsPrinted
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SubcontractEntry, POOrder>.Condition.ConditionBuilder, BoundedTo<SubcontractEntry, POOrder>.Condition>) (b => b.FromBql<BqlOperand<POOrder.printedExt, IBqlBool>.IsEqual<True>>()), nameof (IsPrinted));
      }
    }

    public BoundedTo<SubcontractEntry, POOrder>.Condition IsEmailed
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SubcontractEntry, POOrder>.Condition.ConditionBuilder, BoundedTo<SubcontractEntry, POOrder>.Condition>) (b => b.FromBql<BqlOperand<POOrder.emailedExt, IBqlBool>.IsEqual<True>>()), nameof (IsEmailed));
      }
    }

    public BoundedTo<SubcontractEntry, POOrder>.Condition IsChangeOrder
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SubcontractEntry, POOrder>.Condition.ConditionBuilder, BoundedTo<SubcontractEntry, POOrder>.Condition>) (b => b.FromBql<BqlOperand<POOrder.behavior, IBqlString>.IsEqual<POBehavior.changeOrder>>()), nameof (IsChangeOrder));
      }
    }

    public BoundedTo<SubcontractEntry, POOrder>.Condition HasAllLinesClosed
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SubcontractEntry, POOrder>.Condition.ConditionBuilder, BoundedTo<SubcontractEntry, POOrder>.Condition>) (b => b.FromBql<BqlOperand<POOrder.linesToCloseCntr, IBqlInt>.IsEqual<Zero>>()), nameof (HasAllLinesClosed));
      }
    }

    public BoundedTo<SubcontractEntry, POOrder>.Condition HasAllLinesCompleted
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SubcontractEntry, POOrder>.Condition.ConditionBuilder, BoundedTo<SubcontractEntry, POOrder>.Condition>) (b => b.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POOrder.linesToCloseCntr, NotEqual<Zero>>>>>.And<BqlOperand<POOrder.linesToCompleteCntr, IBqlInt>.IsEqual<Zero>>>()), nameof (HasAllLinesCompleted));
      }
    }
  }
}
