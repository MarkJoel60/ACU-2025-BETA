// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.UpdateBAccountMassProcess
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

public class UpdateBAccountMassProcess : 
  CRBaseUpdateProcess<
  #nullable disable
  UpdateBAccountMassProcess, BAccount, PXMassUpdatableFieldAttribute, BAccount.classID>
{
  [PXViewName("Matching Records")]
  [PXViewDetailsButton(typeof (BAccount))]
  [PXFilterable(new System.Type[] {})]
  public PXProcessingJoin<BAccount, LeftJoin<Contact, On<Contact.bAccountID, Equal<BAccount.bAccountID>, And<Contact.contactID, Equal<BAccount.defContactID>>>, LeftJoin<Address, On<Address.bAccountID, Equal<BAccount.bAccountID>, And<Address.addressID, Equal<BAccount.defAddressID>>>, LeftJoin<BAccountParent, On<BAccountParent.bAccountID, Equal<BAccount.parentBAccountID>>, LeftJoin<Location, On<Location.bAccountID, Equal<BAccount.bAccountID>, And<Location.locationID, Equal<BAccount.defLocationID>>>, LeftJoin<PX.Objects.CS.State, On<PX.Objects.CS.State.countryID, Equal<Address.countryID>, And<PX.Objects.CS.State.stateID, Equal<Address.state>>>>>>>>, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  BAccount.type, 
  #nullable disable
  Equal<BAccountType.prospectType>>>>, Or<BqlOperand<
  #nullable enable
  BAccount.type, IBqlString>.IsEqual<
  #nullable disable
  BAccountType.customerType>>>>.Or<BqlOperand<
  #nullable enable
  BAccount.type, IBqlString>.IsEqual<
  #nullable disable
  BAccountType.combinedType>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  CRWorkflowMassActionFilter.operation>, 
  #nullable disable
  Equal<CRWorkflowMassActionOperation.updateSettings>>>>>.Or<WorkflowAction.IsEnabled<BAccount, CRWorkflowMassActionFilter.action>>>>>>.And<Match<Current<AccessInfo.userName>>>>, OrderBy<Asc<BAccount.acctName>>> Items;

  protected override PXGraph GetPrimaryGraph(BAccount item)
  {
    return (PXGraph) PXGraph.CreateInstance<BusinessAccountMaint>();
  }
}
