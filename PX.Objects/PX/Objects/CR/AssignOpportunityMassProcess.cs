// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.AssignOpportunityMassProcess
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

public class AssignOpportunityMassProcess : 
  CRBaseAssignProcess<
  #nullable disable
  AssignOpportunityMassProcess, CROpportunity, CRSetup.defaultOpportunityAssignmentMapID>
{
  [PXViewName("Matching Records")]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (CROpportunity))]
  [PXViewDetailsButton(typeof (CROpportunity), typeof (Select<BAccountCRM, Where<BAccountCRM.bAccountID, Equal<Current<CROpportunity.bAccountID>>>>), ActionName = "Items_BAccount_ViewDetails")]
  [PXViewDetailsButton(typeof (CROpportunity), typeof (Select<BAccountCRM, Where<BAccountCRM.bAccountID, Equal<Current<CROpportunity.parentBAccountID>>>>), ActionName = "Items_BAccountParent_ViewDetails")]
  public PXProcessingJoin<CROpportunity, LeftJoin<CROpportunityProbability, On<CROpportunityProbability.stageCode, Equal<CROpportunity.stageID>>, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CROpportunity.bAccountID>>, LeftJoin<BAccountParent, On<BAccountParent.bAccountID, Equal<CROpportunity.parentBAccountID>>, LeftJoin<CRActivityStatistics, On<CROpportunity.noteID, Equal<CRActivityStatistics.noteID>>>>>>, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CROpportunity.isActive, 
  #nullable disable
  NotEqual<False>>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  BAccount.bAccountID, 
  #nullable disable
  IsNull>>>>.Or<Match<BAccount, Current<AccessInfo.userName>>>>>>> Items;
}
