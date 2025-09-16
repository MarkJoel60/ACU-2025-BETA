// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;
using System;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.SO;

public class SOShipmentEntry_Workflow : PXGraphExtension<SOShipmentEntry>
{
  protected static void DisableWholeScreen(
    BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IContainerFillerFields states)
  {
    states.AddAllFields<SOShipment>((Func<BoundedTo<SOShipmentEntry, SOShipment>.FieldState.INeedAnyConfigField, BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IConfigured>) (state => (BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IConfigured) state.IsDisabled()));
    states.AddField<SOShipment.shipmentNbr>((Func<BoundedTo<SOShipmentEntry, SOShipment>.FieldState.INeedAnyConfigField, BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IConfigured>) null);
    states.AddField<SOShipment.excludeFromIntercompanyProc>((Func<BoundedTo<SOShipmentEntry, SOShipment>.FieldState.INeedAnyConfigField, BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IConfigured>) null);
    states.AddTable<SOShipLine>((Func<BoundedTo<SOShipmentEntry, SOShipment>.FieldState.INeedAnyConfigField, BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IConfigured>) (state => (BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IConfigured) state.IsDisabled()));
    states.AddTable<SOShipLineSplit>((Func<BoundedTo<SOShipmentEntry, SOShipment>.FieldState.INeedAnyConfigField, BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IConfigured>) (state => (BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IConfigured) state.IsDisabled()));
    states.AddTable<SOShipmentAddress>((Func<BoundedTo<SOShipmentEntry, SOShipment>.FieldState.INeedAnyConfigField, BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IConfigured>) (state => (BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IConfigured) state.IsDisabled()));
    states.AddTable<SOShipmentContact>((Func<BoundedTo<SOShipmentEntry, SOShipment>.FieldState.INeedAnyConfigField, BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IConfigured>) (state => (BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IConfigured) state.IsDisabled()));
    states.AddTable<SOOrderShipment>((Func<BoundedTo<SOShipmentEntry, SOShipment>.FieldState.INeedAnyConfigField, BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IConfigured>) (state => (BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IConfigured) state.IsDisabled()));
  }

  public virtual void Configure(PXScreenConfiguration config)
  {
    SOShipmentEntry_Workflow.Configure(config.GetScreenConfigurationContext<SOShipmentEntry, SOShipment>());
  }

  protected static void Configure(
    WorkflowContext<SOShipmentEntry, SOShipment> context)
  {
    SOShipmentEntry_Workflow.Conditions conditions = context.Conditions.GetPack<SOShipmentEntry_Workflow.Conditions>();
    CommonActionCategories.Categories<SOShipmentEntry, SOShipment> categories1 = CommonActionCategories.Get<SOShipmentEntry, SOShipment>(context);
    BoundedTo<SOShipmentEntry, SOShipment>.ActionCategory.IConfigured processingCategory = categories1.Processing;
    BoundedTo<SOShipmentEntry, SOShipment>.ActionCategory.IConfigured intercompanyCategory = categories1.Intercompany;
    BoundedTo<SOShipmentEntry, SOShipment>.ActionCategory.IConfigured printingEmailingCategory = categories1.PrintingAndEmailing;
    BoundedTo<SOShipmentEntry, SOShipment>.ActionCategory.IConfigured labelsCategory = context.Categories.CreateNew("Labels Category", (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<SOShipmentEntry, SOShipment>.ActionCategory.IConfigured>) (category => (BoundedTo<SOShipmentEntry, SOShipment>.ActionCategory.IConfigured) category.DisplayName("Labels")));
    BoundedTo<SOShipmentEntry, SOShipment>.ActionCategory.IConfigured otherCategory = categories1.Other;
    context.AddScreenConfigurationFor((Func<BoundedTo<SOShipmentEntry, SOShipment>.ScreenConfiguration.IStartConfigScreen, BoundedTo<SOShipmentEntry, SOShipment>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<SOShipmentEntry, SOShipment>.ScreenConfiguration.IConfigured) ((BoundedTo<SOShipmentEntry, SOShipment>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<SOShipment.status>().AddDefaultFlow((Func<BoundedTo<SOShipmentEntry, SOShipment>.Workflow.INeedStatesFlow, BoundedTo<SOShipmentEntry, SOShipment>.Workflow.IConfigured>) (flow => (BoundedTo<SOShipmentEntry, SOShipment>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<SOShipmentEntry, SOShipment>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOShipmentEntry, SOShipment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOShipmentEntry, SOShipment>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<SOShipmentEntry, PXAutoAction<SOShipment>>>) (g => g.initializeState))));
      fss.Add<SOShipmentStatus.hold>((Func<BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOShipmentEntry, SOShipment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOShipmentEntry, SOShipment>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.releaseFromHold), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.validateAddresses), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.printPickListAction), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
      }))));
      fss.Add<SOShipmentStatus.open>((Func<BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOShipmentEntry, SOShipment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOShipmentEntry, SOShipment>.BaseFlowStep.IConfigured) ((BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add<ConfirmShipmentExtension>((Expression<Func<ConfirmShipmentExtension, PXAction<SOShipment>>>) (g => g.confirmShipmentAction), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.putOnHold), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.printShipmentConfirmation), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.validateAddresses), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
        actions.Add<LabelsPrinting>((Expression<Func<LabelsPrinting, PXAction<SOShipment>>>) (g => g.getReturnLabelsAction), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.printPickListAction), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add<ConfirmShipmentExtension>((Expression<Func<ConfirmShipmentExtension, PXWorkflowEventHandler<SOShipment>>>) (g => g.OnShipmentConfirmed))))));
      fss.Add<SOShipmentStatus.confirmed>((Func<BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOShipmentEntry, SOShipment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOShipmentEntry, SOShipment>.BaseFlowStep.IConfigured) ((BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add<InvoiceExtension>((Expression<Func<InvoiceExtension, PXAction<SOShipment>>>) (g => g.createInvoice), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add<UpdateInventoryExtension>((Expression<Func<UpdateInventoryExtension, PXAction<SOShipment>>>) (g => g.updateIN), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.printShipmentConfirmation), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.correctShipmentAction), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
        actions.Add<LabelsPrinting>((Expression<Func<LabelsPrinting, PXAction<SOShipment>>>) (g => g.printLabels), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
        actions.Add<LabelsPrinting>((Expression<Func<LabelsPrinting, PXAction<SOShipment>>>) (g => g.printCommercialInvoices), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.validateAddresses), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.emailShipment), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
        actions.Add<Intercompany>((Expression<Func<Intercompany, PXAction<SOShipment>>>) (e => e.generatePOReceipt), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: field reference
        handlers.Add<SOOrderShipment, SOInvoice>(Expression.Lambda<Func<SOShipmentEntry, PXWorkflowEventHandler<SOShipment, SOOrderShipment, SOInvoice>>>((Expression) Expression.Field((Expression) Expression.Call(g, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXGraph.GetExtension)), Array.Empty<Expression>()), FieldInfo.GetFieldFromHandle(__fieldref (SOOrderExtension.OnInvoiceLinked))), parameterExpression));
        handlers.Add((Expression<Func<SOShipmentEntry, PXWorkflowEventHandler<SOShipment>>>) (g => g.OnShipmentCorrected));
      }))).WithFieldStates(new Action<BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IContainerFillerFields>(SOShipmentEntry_Workflow.DisableWholeScreen))));
      fss.Add<SOShipmentStatus.partiallyInvoiced>((Func<BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOShipmentEntry, SOShipment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOShipmentEntry, SOShipment>.BaseFlowStep.IConfigured) ((BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add<InvoiceExtension>((Expression<Func<InvoiceExtension, PXAction<SOShipment>>>) (g => g.createInvoice), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.printShipmentConfirmation), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
        actions.Add<LabelsPrinting>((Expression<Func<LabelsPrinting, PXAction<SOShipment>>>) (g => g.printLabels), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
        actions.Add<LabelsPrinting>((Expression<Func<LabelsPrinting, PXAction<SOShipment>>>) (g => g.printCommercialInvoices), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.validateAddresses), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
        actions.Add<Intercompany>((Expression<Func<Intercompany, PXAction<SOShipment>>>) (e => e.generatePOReceipt), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        ParameterExpression parameterExpression3;
        // ISSUE: method reference
        // ISSUE: field reference
        handlers.Add<SOOrderShipment, SOInvoice>(Expression.Lambda<Func<SOShipmentEntry, PXWorkflowEventHandler<SOShipment, SOOrderShipment, SOInvoice>>>((Expression) Expression.Field((Expression) Expression.Call(g, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXGraph.GetExtension)), Array.Empty<Expression>()), FieldInfo.GetFieldFromHandle(__fieldref (SOOrderExtension.OnInvoiceLinked))), parameterExpression3));
        ParameterExpression parameterExpression4;
        // ISSUE: method reference
        // ISSUE: field reference
        handlers.Add<SOOrderShipment, SOInvoice>(Expression.Lambda<Func<SOShipmentEntry, PXWorkflowEventHandler<SOShipment, SOOrderShipment, SOInvoice>>>((Expression) Expression.Field((Expression) Expression.Call(g, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXGraph.GetExtension)), Array.Empty<Expression>()), FieldInfo.GetFieldFromHandle(__fieldref (SOOrderExtension.OnInvoiceUnlinked))), parameterExpression4));
      }))).WithFieldStates(new Action<BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IContainerFillerFields>(SOShipmentEntry_Workflow.DisableWholeScreen))));
      fss.Add<SOShipmentStatus.invoiced>((Func<BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOShipmentEntry, SOShipment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOShipmentEntry, SOShipment>.BaseFlowStep.IConfigured) ((BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.validateAddresses), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
        actions.Add<Intercompany>((Expression<Func<Intercompany, PXAction<SOShipment>>>) (e => e.generatePOReceipt), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: field reference
        handlers.Add<SOOrderShipment, SOInvoice>(Expression.Lambda<Func<SOShipmentEntry, PXWorkflowEventHandler<SOShipment, SOOrderShipment, SOInvoice>>>((Expression) Expression.Field((Expression) Expression.Call(g, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXGraph.GetExtension)), Array.Empty<Expression>()), FieldInfo.GetFieldFromHandle(__fieldref (SOOrderExtension.OnInvoiceUnlinked))), parameterExpression));
        handlers.Add<SOInvoice>((Expression<Func<SOShipmentEntry, PXWorkflowEventHandler<SOShipment, SOInvoice>>>) (g => g.OnInvoiceReleased));
        handlers.Add<SOInvoice>((Expression<Func<SOShipmentEntry, PXWorkflowEventHandler<SOShipment, SOInvoice>>>) (g => g.OnInvoiceCancelled));
      }))).WithFieldStates(new Action<BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IContainerFillerFields>(SOShipmentEntry_Workflow.DisableWholeScreen))));
      fss.Add<SOShipmentStatus.completed>((Func<BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOShipmentEntry, SOShipment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOShipmentEntry, SOShipment>.BaseFlowStep.IConfigured) ((BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.printShipmentConfirmation), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
        actions.Add<Intercompany>((Expression<Func<Intercompany, PXAction<SOShipment>>>) (e => e.generatePOReceipt), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: field reference
        handlers.Add<SOOrderShipment, SOInvoice>(Expression.Lambda<Func<SOShipmentEntry, PXWorkflowEventHandler<SOShipment, SOOrderShipment, SOInvoice>>>((Expression) Expression.Field((Expression) Expression.Call(g, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXGraph.GetExtension)), Array.Empty<Expression>()), FieldInfo.GetFieldFromHandle(__fieldref (SOOrderExtension.OnInvoiceUnlinked))), parameterExpression));
        handlers.Add<SOInvoice>((Expression<Func<SOShipmentEntry, PXWorkflowEventHandler<SOShipment, SOInvoice>>>) (g => g.OnInvoiceCancelled));
      }))).WithFieldStates(new Action<BoundedTo<SOShipmentEntry, SOShipment>.FieldState.IContainerFillerFields>(SOShipmentEntry_Workflow.DisableWholeScreen))).WithOnEnterAssignments((Action<BoundedTo<SOShipmentEntry, SOShipment>.Assignment.IContainerFillerFields>) (eass => eass.Add<SOShipment.gotReadyForArchiveAt>((Func<BoundedTo<SOShipmentEntry, SOShipment>.Assignment.INeedRightOperand, BoundedTo<SOShipmentEntry, SOShipment>.Assignment.IConfigured>) (v => (BoundedTo<SOShipmentEntry, SOShipment>.Assignment.IConfigured) v.SetFromToday()))))).WithOnLeaveAssignments((Action<BoundedTo<SOShipmentEntry, SOShipment>.Assignment.IContainerFillerFields>) (lass => lass.Add<SOShipment.gotReadyForArchiveAt>((Func<BoundedTo<SOShipmentEntry, SOShipment>.Assignment.INeedRightOperand, BoundedTo<SOShipmentEntry, SOShipment>.Assignment.IConfigured>) (v => (BoundedTo<SOShipmentEntry, SOShipment>.Assignment.IConfigured) v.SetFromValue((object) null)))))));
      fss.Add<SOShipmentStatus.receipted>((Func<BoundedTo<SOShipmentEntry, SOShipment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOShipmentEntry, SOShipment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOShipmentEntry, SOShipment>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IContainerFillerActions>) (actions => actions.Add<InvoiceExtension>((Expression<Func<InvoiceExtension, PXAction<SOShipment>>>) (g => g.createDropshipInvoice), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IAllowOptionalConfig, BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionState.IConfigured) a.IsDuplicatedInToolbar()))))));
    })).WithTransitions((Action<BoundedTo<SOShipmentEntry, SOShipment>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<SOShipmentEntry, SOShipment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.hold>().IsTriggeredOn((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.initializeState)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.confirmed>().IsTriggeredOn((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.initializeState)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsConfirmed)));
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.partiallyInvoiced>().IsTriggeredOn((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.initializeState)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsPartiallyInvoiced)));
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.invoiced>().IsTriggeredOn((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.initializeState)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsInvoiced)));
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.completed>().IsTriggeredOn((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.initializeState)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsCompleted)));
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.open>().IsTriggeredOn((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.initializeState))));
      }));
      transitions.AddGroupFrom<SOShipmentStatus.hold>((Action<BoundedTo<SOShipmentEntry, SOShipment>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.open>().IsTriggeredOn((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.releaseFromHold)).WithFieldAssignments((Action<BoundedTo<SOShipmentEntry, SOShipment>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOShipment.hold>(new bool?(false))))))));
      transitions.AddGroupFrom<SOShipmentStatus.open>((Action<BoundedTo<SOShipmentEntry, SOShipment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.hold>().IsTriggeredOn((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<SOShipmentEntry, SOShipment>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOShipment.hold>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.confirmed>().IsTriggeredOn<ConfirmShipmentExtension>((Expression<Func<ConfirmShipmentExtension, PXWorkflowEventHandlerBase<SOShipment>>>) (g => g.OnShipmentConfirmed)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsConfirmed)));
      }));
      transitions.AddGroupFrom<SOShipmentStatus.confirmed>((Action<BoundedTo<SOShipmentEntry, SOShipment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.open>().IsTriggeredOn((Expression<Func<SOShipmentEntry, PXWorkflowEventHandlerBase<SOShipment>>>) (g => g.OnShipmentCorrected)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) BoundedTo<SOShipmentEntry, SOShipment>.Condition.op_LogicalNot(conditions.IsConfirmed))));
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.completed>().IsTriggeredOn<UpdateInventoryExtension>((Expression<Func<UpdateInventoryExtension, PXAction<SOShipment>>>) (g => g.updateIN)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsNotBillableAndReleased)));
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.invoiced>().IsTriggeredOn<SOOrderExtension>((Expression<Func<SOOrderExtension, PXWorkflowEventHandlerBase<SOShipment>>>) (ge => ge.OnInvoiceLinked)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsInvoiced)));
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.partiallyInvoiced>().IsTriggeredOn<SOOrderExtension>((Expression<Func<SOOrderExtension, PXWorkflowEventHandlerBase<SOShipment>>>) (ge => ge.OnInvoiceLinked)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsPartiallyInvoiced)));
      }));
      transitions.AddGroupFrom<SOShipmentStatus.partiallyInvoiced>((Action<BoundedTo<SOShipmentEntry, SOShipment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.confirmed>().IsTriggeredOn<SOOrderExtension>((Expression<Func<SOOrderExtension, PXWorkflowEventHandlerBase<SOShipment>>>) (ge => ge.OnInvoiceUnlinked)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsConfirmed)));
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.invoiced>().IsTriggeredOn<SOOrderExtension>((Expression<Func<SOOrderExtension, PXWorkflowEventHandlerBase<SOShipment>>>) (ge => ge.OnInvoiceLinked)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsInvoiced)));
      }));
      transitions.AddGroupFrom<SOShipmentStatus.invoiced>((Action<BoundedTo<SOShipmentEntry, SOShipment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.confirmed>().IsTriggeredOn<SOOrderExtension>((Expression<Func<SOOrderExtension, PXWorkflowEventHandlerBase<SOShipment>>>) (ge => ge.OnInvoiceUnlinked)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsConfirmed)));
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.partiallyInvoiced>().IsTriggeredOn<SOOrderExtension>((Expression<Func<SOOrderExtension, PXWorkflowEventHandlerBase<SOShipment>>>) (ge => ge.OnInvoiceUnlinked)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsPartiallyInvoiced)));
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.completed>().IsTriggeredOn((Expression<Func<SOShipmentEntry, PXWorkflowEventHandlerBase<SOShipment>>>) (g => g.OnInvoiceReleased)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsCompleted)));
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.partiallyInvoiced>().IsTriggeredOn((Expression<Func<SOShipmentEntry, PXWorkflowEventHandlerBase<SOShipment>>>) (g => g.OnInvoiceCancelled)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsPartiallyInvoiced)));
      }));
      transitions.AddGroupFrom<SOShipmentStatus.completed>((Action<BoundedTo<SOShipmentEntry, SOShipment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.confirmed>().IsTriggeredOn<SOOrderExtension>((Expression<Func<SOOrderExtension, PXWorkflowEventHandlerBase<SOShipment>>>) (ge => ge.OnInvoiceUnlinked)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsConfirmed)));
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.partiallyInvoiced>().IsTriggeredOn<SOOrderExtension>((Expression<Func<SOOrderExtension, PXWorkflowEventHandlerBase<SOShipment>>>) (ge => ge.OnInvoiceUnlinked)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsPartiallyInvoiced)));
        ts.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.Transition.INeedTarget, BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured>) (t => (BoundedTo<SOShipmentEntry, SOShipment>.Transition.IConfigured) t.To<SOShipmentStatus.invoiced>().IsTriggeredOn((Expression<Func<SOShipmentEntry, PXWorkflowEventHandlerBase<SOShipment>>>) (g => g.OnInvoiceCancelled)).When((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsInvoiced)));
      }));
      transitions.AddGroupFrom<SOShipmentStatus.receipted>((Action<BoundedTo<SOShipmentEntry, SOShipment>.Transition.ISourceContainerFillerTransitions>) (ts => { }));
    })))).WithActions((Action<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.initializeState), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.releaseFromHold), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured) a.WithCategory(processingCategory)));
      actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.putOnHold), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured) a.WithCategory(processingCategory, (Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.releaseFromHold)).PlaceAfter("confirmShipmentAction")));
      actions.Add<ConfirmShipmentExtension>((Expression<Func<ConfirmShipmentExtension, PXAction<SOShipment>>>) (g => g.confirmShipmentAction), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).IsDisabledWhen((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) BoundedTo<SOShipmentEntry, SOShipment>.Condition.op_LogicalNot(conditions.IsNotHeldByPicking)).MassProcessingScreen<SOInvoiceShipment>().InBatchMode()));
      actions.Add<InvoiceExtension>((Expression<Func<InvoiceExtension, PXAction<SOShipment>>>) (g => g.createInvoice), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).IsDisabledWhen((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsNotBillable).MassProcessingScreen<SOInvoiceShipment>().InBatchMode()));
      actions.Add<InvoiceExtension>((Expression<Func<InvoiceExtension, PXAction<SOShipment>>>) (g => g.createDropshipInvoice), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).MassProcessingScreen<SOInvoiceShipment>().InBatchMode()));
      actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.correctShipmentAction), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).IsDisabledWhen((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsIntercompanyReceiptGenerated)));
      actions.Add<Intercompany>((Expression<Func<Intercompany, PXAction<SOShipment>>>) (e => e.generatePOReceipt), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured) a.WithCategory(intercompanyCategory).IsHiddenWhen((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsNotIntercompanyIssue).IsDisabledWhen((BoundedTo<SOShipmentEntry, SOShipment>.ISharedCondition) conditions.IsIntercompanyReceiptGenerated)));
      actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.printPickListAction), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured) a.WithCategory(printingEmailingCategory).MassProcessingScreen<SOInvoiceShipment>().InBatchMode()));
      actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.printShipmentConfirmation), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured) a.WithCategory(printingEmailingCategory).WithFieldAssignments((Action<BoundedTo<SOShipmentEntry, SOShipment>.Assignment.IContainerFillerFields>) (fass => fass.Add<SOShipment.confirmationPrinted>(new bool?(true)))).MassProcessingScreen<SOInvoiceShipment>().InBatchMode()));
      actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.emailShipment), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured) a.WithCategory(printingEmailingCategory).MassProcessingScreen<SOInvoiceShipment>()));
      actions.Add<LabelsPrinting>((Expression<Func<LabelsPrinting, PXAction<SOShipment>>>) (g => g.printLabels), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured) a.WithCategory(labelsCategory).MassProcessingScreen<SOInvoiceShipment>().InBatchMode()));
      actions.Add<LabelsPrinting>((Expression<Func<LabelsPrinting, PXAction<SOShipment>>>) (g => g.printCommercialInvoices), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured) a.WithCategory(labelsCategory).MassProcessingScreen<SOInvoiceShipment>().InBatchMode()));
      actions.Add<LabelsPrinting>((Expression<Func<LabelsPrinting, PXAction<SOShipment>>>) (g => g.getReturnLabelsAction), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured) a.WithCategory(labelsCategory)));
      actions.Add<UpdateInventoryExtension>((Expression<Func<UpdateInventoryExtension, PXAction<SOShipment>>>) (g => g.updateIN), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured) a.WithCategory(otherCategory).MassProcessingScreen<SOInvoiceShipment>().InBatchMode()));
      actions.Add((Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.validateAddresses), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
    })).WithCategories((Action<BoundedTo<SOShipmentEntry, SOShipment>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(intercompanyCategory);
      categories.Add(printingEmailingCategory);
      categories.Add(labelsCategory);
      categories.Add(otherCategory);
    })).WithHandlers((Action<BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<SOShipment>) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<SOShipment>) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedSubscriber<SOShipment>) handler.WithTargetOf<SOShipment>().OfEntityEvent<SOShipment.Events>((Expression<Func<SOShipment.Events, PXEntityEvent<SOShipment>>>) (e => e.ShipmentConfirmed))).Is<ConfirmShipmentExtension>((Expression<Func<ConfirmShipmentExtension, PXWorkflowEventHandler<SOShipment, SOShipment>>>) (g => g.OnShipmentConfirmed))).UsesTargetAsPrimaryEntity()).DisplayName("Shipment Confirmed")));
      handlers.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<SOShipment>) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<SOShipment>) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedSubscriber<SOShipment>) handler.WithTargetOf<SOShipment>().OfEntityEvent<SOShipment.Events>((Expression<Func<SOShipment.Events, PXEntityEvent<SOShipment>>>) (e => e.ShipmentCorrected))).Is((Expression<Func<SOShipment, PXWorkflowEventHandler<SOShipment, SOShipment>>>) (g => g.OnShipmentCorrected))).UsesTargetAsPrimaryEntity()).DisplayName("Shipment Corrected")));
      handlers.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<SOOrderShipment, SOInvoice>) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<SOOrderShipment, SOInvoice>) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedSubscriber<SOOrderShipment, SOInvoice>) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedEventContainer<SOOrderShipment, SOInvoice>) handler.WithTargetOf<SOOrderShipment>().WithParametersOf<SOInvoice>()).OfEntityEvent<SOOrderShipment.Events>((Expression<Func<SOOrderShipment.Events, PXEntityEvent<SOOrderShipment, SOInvoice>>>) (e => e.InvoiceLinked))).Is<SOOrderExtension>((Expression<Func<SOOrderExtension, PXWorkflowEventHandler<SOInvoice, SOOrderShipment, SOInvoice>>>) (ge => ge.OnInvoiceLinked))).UsesPrimaryEntityGetter<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.shipmentType, Equal<BqlField<SOOrderShipment.shipmentType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<SOShipment.shipmentNbr, IBqlString>.IsEqual<BqlField<SOOrderShipment.shipmentNbr, IBqlString>.FromCurrent>>>>(false)).DisplayName("Invoice Linked")));
      handlers.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<SOOrderShipment, SOInvoice>) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<SOOrderShipment, SOInvoice>) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedSubscriber<SOOrderShipment, SOInvoice>) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedEventContainer<SOOrderShipment, SOInvoice>) handler.WithTargetOf<SOOrderShipment>().WithParametersOf<SOInvoice>()).OfEntityEvent<SOOrderShipment.Events>((Expression<Func<SOOrderShipment.Events, PXEntityEvent<SOOrderShipment, SOInvoice>>>) (e => e.InvoiceUnlinked))).Is<SOOrderExtension>((Expression<Func<SOOrderExtension, PXWorkflowEventHandler<SOInvoice, SOOrderShipment, SOInvoice>>>) (ge => ge.OnInvoiceUnlinked))).UsesPrimaryEntityGetter<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.shipmentType, Equal<BqlField<SOOrderShipment.shipmentType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<SOShipment.shipmentNbr, IBqlString>.IsEqual<BqlField<SOOrderShipment.shipmentNbr, IBqlString>.FromCurrent>>>>(false)).DisplayName("Invoice Unlinked")));
      handlers.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<SOInvoice>) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<SOInvoice>) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedSubscriber<SOInvoice>) handler.WithTargetOf<SOInvoice>().OfEntityEvent<SOInvoice.Events>((Expression<Func<SOInvoice.Events, PXEntityEvent<SOInvoice>>>) (e => e.InvoiceReleased))).Is((Expression<Func<SOInvoice, PXWorkflowEventHandler<SOShipment, SOInvoice>>>) (g => g.OnInvoiceReleased))).UsesPrimaryEntityGetter<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOOrderShipment>.On<BqlOperand<SOOrderShipment.shipmentNbr, IBqlString>.IsEqual<SOShipment.shipmentNbr>>>>.Where<KeysRelation<CompositeKey<Field<SOOrderShipment.invoiceType>.IsRelatedTo<SOInvoice.docType>, Field<SOOrderShipment.invoiceNbr>.IsRelatedTo<SOInvoice.refNbr>>.WithTablesOf<SOInvoice, SOOrderShipment>, SOInvoice, SOOrderShipment>.SameAsCurrent>>(true)).DisplayName("Invoice Released")));
      handlers.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<SOInvoice>) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<SOInvoice>) ((BoundedTo<SOShipmentEntry, SOShipment>.WorkflowEventHandlerDefinition.INeedSubscriber<SOInvoice>) handler.WithTargetOf<SOInvoice>().OfEntityEvent<SOInvoice.Events>((Expression<Func<SOInvoice.Events, PXEntityEvent<SOInvoice>>>) (e => e.InvoiceCancelled))).Is((Expression<Func<SOInvoice, PXWorkflowEventHandler<SOShipment, SOInvoice>>>) (g => g.OnInvoiceCancelled))).UsesPrimaryEntityGetter<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOOrderShipment>.On<BqlOperand<SOOrderShipment.shipmentNbr, IBqlString>.IsEqual<SOShipment.shipmentNbr>>>>.Where<KeysRelation<CompositeKey<Field<SOOrderShipment.invoiceType>.IsRelatedTo<SOInvoice.docType>, Field<SOOrderShipment.invoiceNbr>.IsRelatedTo<SOInvoice.refNbr>>.WithTablesOf<SOInvoice, SOOrderShipment>, SOInvoice, SOOrderShipment>.SameAsCurrent>>(true)).DisplayName("Invoice Cancelled")));
    })).WithArchivingRules((Action<BoundedTo<SOShipmentEntry, SOShipment>.ArchivingRule.IContainerFillerRules>) (rules =>
    {
      rules.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.ArchivingRule.INeedTable, BoundedTo<SOShipmentEntry, SOShipment>.ArchivingRule.IConfigured>) (r => (BoundedTo<SOShipmentEntry, SOShipment>.ArchivingRule.IConfigured) r.Archive<SOShipLine>().UsingItsParentAttribute()));
      rules.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.ArchivingRule.INeedTable, BoundedTo<SOShipmentEntry, SOShipment>.ArchivingRule.IConfigured>) (r => (BoundedTo<SOShipmentEntry, SOShipment>.ArchivingRule.IConfigured) r.Archive<SOShipLineSplit>().UsingItsParentAttribute()));
      rules.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.ArchivingRule.INeedTable, BoundedTo<SOShipmentEntry, SOShipment>.ArchivingRule.IConfigured>) (r => (BoundedTo<SOShipmentEntry, SOShipment>.ArchivingRule.IConfigured) r.Archive<SOPackageDetail>().UsingItsParentAttribute()));
      rules.Add((Func<BoundedTo<SOShipmentEntry, SOShipment>.ArchivingRule.INeedTable, BoundedTo<SOShipmentEntry, SOShipment>.ArchivingRule.IConfigured>) (r => (BoundedTo<SOShipmentEntry, SOShipment>.ArchivingRule.IConfigured) r.Archive<SOShipLineSplitPackage>().UsingItsFK<SOShipLineSplitPackage.FK.Shipment>()));
    }))));
  }

  public class Conditions : BoundedTo<SOShipmentEntry, SOShipment>.Condition.Pack
  {
    public BoundedTo<SOShipmentEntry, SOShipment>.Condition IsOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOShipmentEntry, SOShipment>.Condition.ConditionBuilder, BoundedTo<SOShipmentEntry, SOShipment>.Condition>) (b => b.FromBql<BqlOperand<SOShipment.hold, IBqlBool>.IsEqual<True>>()), nameof (IsOnHold));
      }
    }

    public BoundedTo<SOShipmentEntry, SOShipment>.Condition IsNotBillable
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOShipmentEntry, SOShipment>.Condition.ConditionBuilder, BoundedTo<SOShipmentEntry, SOShipment>.Condition>) (b => b.FromBql<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.unbilledOrderCntr, Equal<Zero>>>>, And<BqlOperand<SOShipment.billedOrderCntr, IBqlInt>.IsEqual<Zero>>>>.And<BqlOperand<SOShipment.releasedOrderCntr, IBqlInt>.IsEqual<Zero>>>()), nameof (IsNotBillable));
      }
    }

    public BoundedTo<SOShipmentEntry, SOShipment>.Condition IsConfirmed
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOShipmentEntry, SOShipment>.Condition.ConditionBuilder, BoundedTo<SOShipmentEntry, SOShipment>.Condition>) (b => b.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.confirmed, Equal<True>>>>, And<BqlOperand<SOShipment.billedOrderCntr, IBqlInt>.IsEqual<Zero>>>, And<BqlOperand<SOShipment.releasedOrderCntr, IBqlInt>.IsEqual<Zero>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.unbilledOrderCntr, Greater<Zero>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.unbilledOrderCntr, Equal<Zero>>>>>.And<BqlOperand<SOShipment.released, IBqlBool>.IsNotEqual<True>>>>>()), nameof (IsConfirmed));
      }
    }

    public BoundedTo<SOShipmentEntry, SOShipment>.Condition IsPartiallyInvoiced
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOShipmentEntry, SOShipment>.Condition.ConditionBuilder, BoundedTo<SOShipmentEntry, SOShipment>.Condition>) (b => b.FromBql<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.confirmed, Equal<True>>>>, And<BqlOperand<SOShipment.unbilledOrderCntr, IBqlInt>.IsGreater<Zero>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.billedOrderCntr, Greater<Zero>>>>>.Or<BqlOperand<SOShipment.releasedOrderCntr, IBqlInt>.IsGreater<Zero>>>>()), nameof (IsPartiallyInvoiced));
      }
    }

    public BoundedTo<SOShipmentEntry, SOShipment>.Condition IsInvoiced
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOShipmentEntry, SOShipment>.Condition.ConditionBuilder, BoundedTo<SOShipmentEntry, SOShipment>.Condition>) (b => b.FromBql<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.confirmed, Equal<True>>>>, And<BqlOperand<SOShipment.unbilledOrderCntr, IBqlInt>.IsEqual<Zero>>>>.And<BqlOperand<SOShipment.billedOrderCntr, IBqlInt>.IsGreater<Zero>>>()), nameof (IsInvoiced));
      }
    }

    public BoundedTo<SOShipmentEntry, SOShipment>.Condition IsCompleted
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOShipmentEntry, SOShipment>.Condition.ConditionBuilder, BoundedTo<SOShipmentEntry, SOShipment>.Condition>) (b => b.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.confirmed, Equal<True>>>>, And<BqlOperand<SOShipment.unbilledOrderCntr, IBqlInt>.IsEqual<Zero>>>, And<BqlOperand<SOShipment.billedOrderCntr, IBqlInt>.IsEqual<Zero>>>>.And<BqlOperand<SOShipment.releasedOrderCntr, IBqlInt>.IsGreater<Zero>>>()), nameof (IsCompleted));
      }
    }

    public BoundedTo<SOShipmentEntry, SOShipment>.Condition IsNotIntercompanyIssue
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOShipmentEntry, SOShipment>.Condition.ConditionBuilder, BoundedTo<SOShipmentEntry, SOShipment>.Condition>) (b => b.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.isIntercompany, Equal<False>>>>>.Or<BqlOperand<SOShipment.operation, IBqlString>.IsNotEqual<SOOperation.issue>>>()), nameof (IsNotIntercompanyIssue));
      }
    }

    public BoundedTo<SOShipmentEntry, SOShipment>.Condition IsIntercompanyReceiptGenerated
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOShipmentEntry, SOShipment>.Condition.ConditionBuilder, BoundedTo<SOShipmentEntry, SOShipment>.Condition>) (b => b.FromBql<BqlOperand<SOShipment.intercompanyPOReceiptNbr, IBqlString>.IsNotNull>()), nameof (IsIntercompanyReceiptGenerated));
      }
    }

    public BoundedTo<SOShipmentEntry, SOShipment>.Condition IsNotBillableAndReleased
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOShipmentEntry, SOShipment>.Condition.ConditionBuilder, BoundedTo<SOShipmentEntry, SOShipment>.Condition>) (b => b.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.unbilledOrderCntr, Equal<Zero>>>>, And<BqlOperand<SOShipment.billedOrderCntr, IBqlInt>.IsEqual<Zero>>>, And<BqlOperand<SOShipment.releasedOrderCntr, IBqlInt>.IsEqual<Zero>>>>.And<BqlOperand<SOShipment.released, IBqlBool>.IsEqual<True>>>()), nameof (IsNotBillableAndReleased));
      }
    }

    public BoundedTo<SOShipmentEntry, SOShipment>.Condition IsNotHeldByPicking
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOShipmentEntry, SOShipment>.Condition.ConditionBuilder, BoundedTo<SOShipmentEntry, SOShipment>.Condition>) (b => b.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.currentWorksheetNbr, IsNull>>>, Or<BqlOperand<SOShipment.picked, IBqlBool>.IsEqual<True>>>, Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Selector<SOShipment.currentWorksheetNbr, SOPickingWorksheet.worksheetType>, In3<SOPickingWorksheet.worksheetType.wave, SOPickingWorksheet.worksheetType.batch>>>>>.And<BqlOperand<Selector<SOShipment.currentWorksheetNbr, SOPickingWorksheet.status>, IBqlString>.IsIn<SOPickingWorksheet.status.picked, SOPickingWorksheet.status.completed>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Selector<SOShipment.currentWorksheetNbr, SOPickingWorksheet.worksheetType>, Equal<SOPickingWorksheet.worksheetType.single>>>>>.And<BqlOperand<Selector<SOShipment.currentWorksheetNbr, SOPickingWorksheet.status>, IBqlString>.IsNotEqual<SOPickingWorksheet.status.picking>>>>()), nameof (IsNotHeldByPicking));
      }
    }
  }

  public static class ActionCategories
  {
    public const string LabelsCategoryID = "Labels Category";

    [PXLocalizable]
    public static class DisplayNames
    {
      public const string Labels = "Labels";
    }
  }
}
