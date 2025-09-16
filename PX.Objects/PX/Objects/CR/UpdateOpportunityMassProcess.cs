// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.UpdateOpportunityMassProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR.MassProcess;
using PX.SM;

#nullable enable
namespace PX.Objects.CR;

public class UpdateOpportunityMassProcess : 
  CRBaseWorkflowUpdateProcess<
  #nullable disable
  UpdateOpportunityMassProcess, OpportunityMaint, CROpportunity, PXMassUpdatableFieldAttribute, CROpportunity.classID>
{
  [PXViewName("Matching Records")]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (CROpportunity))]
  [PXViewDetailsButton(typeof (CROpportunity), typeof (Select<BAccountCRM, Where<BAccountCRM.bAccountID, Equal<Current<CROpportunity.bAccountID>>>>), ActionName = "Items_BAccount_ViewDetails")]
  [PXViewDetailsButton(typeof (CROpportunity), typeof (Select<BAccountCRM, Where<BAccountCRM.bAccountID, Equal<Current<CROpportunity.parentBAccountID>>>>), ActionName = "Items_BAccountParent_ViewDetails")]
  public PXFilteredProcessingJoin<CROpportunity, CRWorkflowMassActionFilter, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CROpportunity.bAccountID>>, LeftJoin<BAccountParent, On<BAccountParent.bAccountID, Equal<CROpportunity.parentBAccountID>>, LeftJoin<CROpportunityProbability, On<CROpportunityProbability.stageCode, Equal<CROpportunity.stageID>>, LeftJoin<CRActivityStatistics, On<CROpportunity.noteID, Equal<CRActivityStatistics.noteID>>>>>>, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  BAccount.bAccountID, 
  #nullable disable
  IsNull>>>>.Or<Match<BAccount, BqlField<
  #nullable enable
  AccessInfo.userName, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  CRWorkflowMassActionFilter.operation>, 
  #nullable disable
  Equal<CRWorkflowMassActionOperation.updateSettings>>>>>.And<BqlOperand<
  #nullable enable
  CROpportunity.isActive, IBqlBool>.IsEqual<
  #nullable disable
  True>>>>>.Or<WorkflowAction.IsEnabled<CROpportunity, CRWorkflowMassActionFilter.action>>>>> Items;

  protected override PXFilteredProcessing<CROpportunity, CRWorkflowMassActionFilter> ProcessingView
  {
    get => (PXFilteredProcessing<CROpportunity, CRWorkflowMassActionFilter>) this.Items;
  }
}
