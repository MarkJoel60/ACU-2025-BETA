// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.UpdateLeadMassProcess
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

public class UpdateLeadMassProcess : 
  CRBaseWorkflowUpdateProcess<
  #nullable disable
  UpdateLeadMassProcess, LeadMaint, CRLead, PXMassUpdatableFieldAttribute, CRLead.classID>
{
  [PXViewName("Matching Records")]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (CRLead))]
  [PXViewDetailsButton(typeof (CRLead), typeof (Select<BAccountCRM, Where<BAccountCRM.bAccountID, Equal<Current<CRLead.bAccountID>>>>), ActionName = "Items_BAccount_ViewDetails")]
  [PXViewDetailsButton(typeof (CRLead), typeof (Select<BAccountCRM, Where<BAccountCRM.bAccountID, Equal<Current<CRLead.parentBAccountID>>>>), ActionName = "Items_BAccountParent_ViewDetails")]
  public PXFilteredProcessingJoin<CRLead, CRWorkflowMassActionFilter, LeftJoin<Address, On<Address.addressID, Equal<CRLead.defAddressID>>, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRLead.bAccountID>>, LeftJoin<BAccountParent, On<BAccountParent.bAccountID, Equal<CRLead.parentBAccountID>>, LeftJoin<PX.Objects.CS.State, On<PX.Objects.CS.State.countryID, Equal<Address.countryID>, And<PX.Objects.CS.State.stateID, Equal<Address.state>>>, LeftJoin<CRActivityStatistics, On<CRLead.noteID, Equal<CRActivityStatistics.noteID>>>>>>>, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CRLead.contactType, 
  #nullable disable
  Equal<ContactTypesAttribute.lead>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  BAccount.bAccountID, 
  #nullable disable
  IsNull>>>>.Or<Match<BAccount, BqlField<
  #nullable enable
  AccessInfo.userName, IBqlString>.FromCurrent>>>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  CRWorkflowMassActionFilter.operation>, 
  #nullable disable
  Equal<CRWorkflowMassActionOperation.updateSettings>>>>>.And<BqlOperand<
  #nullable enable
  CRLead.isActive, IBqlBool>.IsEqual<
  #nullable disable
  True>>>>>.Or<WorkflowAction.IsEnabled<CRLead, CRWorkflowMassActionFilter.action>>>>, OrderBy<Asc<CRLead.displayName>>> Items;

  [PXMassUpdatableField]
  [PXDefault(typeof (Search<CRLeadClass.defaultSource, Where<CRLeadClass.classID, Equal<Current<CRLead.classID>>>>))]
  [PXMergeAttributes]
  protected virtual void _(Events.CacheAttached<CRLead.source> e)
  {
  }

  [PXMassUpdatableField]
  [PXMergeAttributes]
  protected virtual void _(Events.CacheAttached<CRLead.campaignID> e)
  {
  }

  protected override PXFilteredProcessing<CRLead, CRWorkflowMassActionFilter> ProcessingView
  {
    get => (PXFilteredProcessing<CRLead, CRWorkflowMassActionFilter>) this.Items;
  }
}
