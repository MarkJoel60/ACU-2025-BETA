// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.UpdateCaseMassProcess
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

public class UpdateCaseMassProcess : 
  CRBaseWorkflowUpdateProcess<
  #nullable disable
  UpdateCaseMassProcess, CRCaseMaint, CRCase, PXMassUpdatableFieldAttribute, CRCase.caseClassID>
{
  [PXHidden]
  public PXSelect<Location> location;
  [PXViewName("Matching Records")]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (CRCase))]
  [PXViewDetailsButton(typeof (CRCase), typeof (Select<BAccountCRM, Where<BAccountCRM.bAccountID, Equal<Current<CRCase.customerID>>>>), ActionName = "Items_BAccount_ViewDetails")]
  [PXViewDetailsButton(typeof (CRCase), typeof (Select2<BAccountCRM, InnerJoin<BAccountParent, On<BAccountParent.parentBAccountID, Equal<BAccountCRM.bAccountID>>>, Where<BAccountParent.bAccountID, Equal<Current<CRCase.customerID>>>>), ActionName = "Items_BAccountParent_ViewDetails")]
  [PXViewDetailsButton(typeof (CRCase), typeof (Select<Contact, Where<Contact.contactID, Equal<Current<CRCase.contactID>>>>))]
  [PXViewDetailsButton(typeof (CRCase), typeof (Select<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.contractID, Equal<Current<CRCase.contractID>>>>))]
  [PXViewDetailsButton(typeof (CRCase), typeof (Select<Location, Where<Location.bAccountID, Equal<Current<CRCase.customerID>>, And<Location.locationID, Equal<Current<CRCase.locationID>>>>>))]
  [PXViewDetailsButton(typeof (CRCase), typeof (Select2<BAccount, InnerJoin<PX.Objects.CT.Contract, On<PX.Objects.CT.Contract.customerID, Equal<BAccount.bAccountID>>>, Where<PX.Objects.CT.Contract.contractID, Equal<Current<CRCase.contractID>>>>), ActionName = "Items_Contract_CustomerID_ViewDetails")]
  public PXFilteredProcessingJoin<CRCase, CRWorkflowMassActionFilter, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRCase.customerID>>, LeftJoin<BAccountParent, On<BAccountParent.bAccountID, Equal<BAccount.parentBAccountID>>, LeftJoin<Contact, On<Contact.contactID, Equal<CRCase.contactID>>, LeftJoin<PX.Objects.CT.Contract, On<PX.Objects.CT.Contract.contractID, Equal<CRCase.contractID>>, LeftJoin<AssignCaseMassProcess.BAccountContract, On<AssignCaseMassProcess.BAccountContract.bAccountID, Equal<PX.Objects.CT.Contract.customerID>>, LeftJoin<Location, On<Location.bAccountID, Equal<CRCase.customerID>, And<Location.locationID, Equal<CRCase.locationID>>>, LeftJoin<CRActivityStatistics, On<CRCase.noteID, Equal<CRActivityStatistics.noteID>>>>>>>>>, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  BAccount.bAccountID, 
  #nullable disable
  IsNull>>>>.Or<Match<BAccount, BqlField<
  #nullable enable
  AccessInfo.userName, IBqlString>.FromCurrent>>>>, 
  #nullable disable
  And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CRCase.released, 
  #nullable disable
  Equal<False>>>>>.Or<BqlOperand<
  #nullable enable
  CRCase.released, IBqlBool>.IsNull>>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  CRWorkflowMassActionFilter.operation>, 
  #nullable disable
  Equal<CRWorkflowMassActionOperation.updateSettings>>>>>.And<Brackets<BqlOperand<
  #nullable enable
  CRCase.isActive, IBqlBool>.IsEqual<
  #nullable disable
  True>>>>>>.Or<WorkflowAction.IsEnabled<CRCase, CRWorkflowMassActionFilter.action>>>>> Items;

  protected override PXFilteredProcessing<CRCase, CRWorkflowMassActionFilter> ProcessingView
  {
    get => (PXFilteredProcessing<CRCase, CRWorkflowMassActionFilter>) this.Items;
  }

  [PXMergeAttributes]
  [PXDBScalar(typeof (Search<CRActivityStatistics.lastActivityDate, Where<CRActivityStatistics.noteID, Equal<CRCase.noteID>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<CRCase.lastActivity> e)
  {
  }
}
