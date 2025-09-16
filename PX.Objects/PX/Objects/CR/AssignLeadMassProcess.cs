// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.AssignLeadMassProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.SM;

#nullable enable
namespace PX.Objects.CR;

public class AssignLeadMassProcess : 
  CRBaseAssignProcess<
  #nullable disable
  AssignLeadMassProcess, CRLead, CRSetup.leaddefaultAssignmentMapID>
{
  [PXViewName("Matching Records")]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (CRLead))]
  [PXViewDetailsButton(typeof (CRLead), typeof (Select<BAccountCRM, Where<BAccountCRM.bAccountID, Equal<Current<CRLead.bAccountID>>>>), ActionName = "Items_BAccount_ViewDetails")]
  [PXViewDetailsButton(typeof (CRLead), typeof (Select<BAccountCRM, Where<BAccountCRM.bAccountID, Equal<Current<CRLead.parentBAccountID>>>>), ActionName = "Items_BAccountParent_ViewDetails")]
  public PXProcessingJoin<CRLead, LeftJoin<Address, On<Address.addressID, Equal<CRLead.defAddressID>>, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRLead.bAccountID>>, LeftJoin<BAccountParent, On<BAccountParent.bAccountID, Equal<CRLead.parentBAccountID>>, LeftJoin<PX.Objects.CS.State, On<PX.Objects.CS.State.countryID, Equal<Address.countryID>, And<PX.Objects.CS.State.stateID, Equal<Address.state>>>, LeftJoin<CRActivityStatistics, On<CRLead.noteID, Equal<CRActivityStatistics.noteID>>>>>>>, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CRLead.isActive, 
  #nullable disable
  Equal<True>>>>, And<BqlOperand<
  #nullable enable
  CRLead.contactType, IBqlString>.IsEqual<
  #nullable disable
  ContactTypesAttribute.lead>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  BAccount.bAccountID, 
  #nullable disable
  IsNull>>>>.Or<Match<BAccount, BqlField<
  #nullable enable
  AccessInfo.userName, IBqlString>.FromCurrent>>>>, 
  #nullable disable
  OrderBy<Asc<CRLead.displayName>>> Items;

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Contact")]
  protected virtual void _(Events.CacheAttached<CRLead.memberName> e)
  {
  }
}
