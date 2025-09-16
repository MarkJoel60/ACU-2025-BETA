// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BackwardCompatibility.OpportunityActivities
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR.Standalone;
using System;

#nullable disable
namespace PX.Objects.CR.BackwardCompatibility;

/// <exclude />
[Obsolete]
public sealed class OpportunityActivities(PXGraph graph) : CRActivityList<PX.Objects.CR.CROpportunity>(graph)
{
  protected override void SetCommandCondition(Delegate handler = null)
  {
    SelectFromBase<CRPMTimeActivity, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<CROpportunityRevision>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CROpportunityRevision.noteID, Equal<CRPMTimeActivity.refNoteID>>>>>.And<BqlOperand<CROpportunityRevision.opportunityID, IBqlString>.IsEqual<BqlField<PX.Objects.CR.CROpportunity.opportunityID, IBqlString>.FromCurrent>>>>, FbqlJoins.Left<CRReminder>.On<BqlOperand<CRReminder.refNoteID, IBqlGuid>.IsEqual<CRPMTimeActivity.noteID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRPMTimeActivity.refNoteID, Equal<BqlField<PX.Objects.CR.CROpportunity.noteID, IBqlGuid>.FromCurrent>>>>, Or<BqlOperand<CRPMTimeActivity.refNoteID, IBqlGuid>.IsEqual<BqlField<PX.Objects.CR.CROpportunity.quoteNoteID, IBqlGuid>.FromCurrent>>>>.Or<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CROpportunityClass.showContactActivities>, Equal<True>>>>>.And<BqlOperand<CRPMTimeActivity.refNoteID, IBqlGuid>.IsEqual<BqlField<PX.Objects.CR.CROpportunity.leadID, IBqlGuid>.FromCurrent>>>>>.OrderBy<BqlField<CRPMTimeActivity.createdDateTime, IBqlDateTime>.Desc> orderBy = new SelectFromBase<CRPMTimeActivity, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<CROpportunityRevision>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CROpportunityRevision.noteID, Equal<CRPMTimeActivity.refNoteID>>>>>.And<BqlOperand<CROpportunityRevision.opportunityID, IBqlString>.IsEqual<BqlField<PX.Objects.CR.CROpportunity.opportunityID, IBqlString>.FromCurrent>>>>, FbqlJoins.Left<CRReminder>.On<BqlOperand<CRReminder.refNoteID, IBqlGuid>.IsEqual<CRPMTimeActivity.noteID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRPMTimeActivity.refNoteID, Equal<BqlField<PX.Objects.CR.CROpportunity.noteID, IBqlGuid>.FromCurrent>>>>, Or<BqlOperand<CRPMTimeActivity.refNoteID, IBqlGuid>.IsEqual<BqlField<PX.Objects.CR.CROpportunity.quoteNoteID, IBqlGuid>.FromCurrent>>>>.Or<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CROpportunityClass.showContactActivities>, Equal<True>>>>>.And<BqlOperand<CRPMTimeActivity.refNoteID, IBqlGuid>.IsEqual<BqlField<PX.Objects.CR.CROpportunity.leadID, IBqlGuid>.FromCurrent>>>>>.OrderBy<BqlField<CRPMTimeActivity.createdDateTime, IBqlDateTime>.Desc>();
    if ((object) handler == null)
      ((PXSelectBase) this).View = new PXView(((PXSelectBase) this).View.Graph, ((PXSelectBase) this).View.IsReadOnly, (BqlCommand) orderBy);
    else
      ((PXSelectBase) this).View = new PXView(((PXSelectBase) this).View.Graph, ((PXSelectBase) this).View.IsReadOnly, (BqlCommand) orderBy, handler);
  }
}
