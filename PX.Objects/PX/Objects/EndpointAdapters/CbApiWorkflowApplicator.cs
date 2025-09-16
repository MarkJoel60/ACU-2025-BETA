// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.CbApiWorkflowApplicator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.ContractBased.Automation;
using PX.Objects.CR;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.EndpointAdapters;

internal static class CbApiWorkflowApplicator
{
  internal class OpportunityApplicator(IWorkflowServiceWrapper workflowService) : 
    CbApiWorkflowApplicator<CROpportunity, CROpportunity.status>(workflowService, "Opportunity")
  {
  }

  internal class CaseApplicator(IWorkflowServiceWrapper workflowService) : 
    CbApiWorkflowApplicator<CRCase, CRCase.status>(workflowService, "Case")
  {
  }

  internal class LeadApplicator(IWorkflowServiceWrapper workflowService) : 
    CbApiWorkflowApplicator<CRLead, CRLead.status>(workflowService, "Lead")
  {
  }

  internal class ProjectTemplateApplicator(IWorkflowServiceWrapper workflowService) : 
    CbApiWorkflowApplicator<PMProject, PMProject.status>(workflowService, "ProjectTemplate")
  {
  }

  internal class ProjectTaskApplicator(IWorkflowServiceWrapper workflowService) : 
    CbApiWorkflowApplicator<PMTask, PMTask.status>(workflowService, "ProjectTask")
  {
  }
}
