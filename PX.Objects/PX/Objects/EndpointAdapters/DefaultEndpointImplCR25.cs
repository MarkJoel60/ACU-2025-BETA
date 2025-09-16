// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.DefaultEndpointImplCR25
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;

#nullable disable
namespace PX.Objects.EndpointAdapters;

[PXVersion("25.200.001", "Default")]
internal class DefaultEndpointImplCR25(
  CbApiWorkflowApplicator.CaseApplicator caseApplicator,
  CbApiWorkflowApplicator.OpportunityApplicator opportunityApplicator,
  CbApiWorkflowApplicator.LeadApplicator leadApplicator) : DefaultEndpointImplCR24(caseApplicator, opportunityApplicator, leadApplicator)
{
}
