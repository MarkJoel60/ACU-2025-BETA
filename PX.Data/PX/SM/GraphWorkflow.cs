// Decompiled with JetBrains decompiler
// Type: PX.SM.GraphWorkflow
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

public class GraphWorkflow : PXGraph<GraphWorkflow>
{
  public PXSelect<AUWorkflowForm> Forms;
  public PXSelect<AUWorkflowFormField> FormFields;
  public PXSelect<AUWorkflowDefinition> Definition;
  public PXSelect<AUWorkflow> Workflow;
  public PXSelect<AUWorkflowState> WorkflowState;
  public PXSelect<AUWorkflowStateProperty> WorkflowStateProperty;
  public PXSelect<AUWorkflowStateAction> WorkflowStateActions;
  public PXSelect<AUWorkflowStateEventHandler> WorkflowStateEventHandlers;
  public PXSelect<AUWorkflowStateActionField> WorkflowStateActionField;
  public PXSelect<AUWorkflowStateActionParam> WorkflowStateActionParam;
  public PXSelect<AUWorkflowTransition> WorkflowTransition;
  public PXSelect<AUWorkflowTransitionField> WorkflowTransitionField;
  public PXSelect<AUWorkflowOnEnterStateField> WorkflowOnEnterStateField;
  public PXSelect<AUWorkflowOnLeaveStateField> WorkflowOnLeaveStateField;
}
