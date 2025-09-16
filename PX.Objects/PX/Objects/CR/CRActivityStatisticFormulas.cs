// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRActivityStatisticFormulas
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.BQL.ActivityCalc;

#nullable disable
namespace PX.Objects.CR;

/// <summary>
/// The formulas that are used to calculate <see cref="T:PX.Objects.CR.CRActivityStatistics">activity statistics</see> for <see cref="T:PX.Objects.CR.CRActivity" />.
/// </summary>
[PXUnboundFormula(typeof (Switch<Case<Where<CRActivity.incoming, Equal<True>, And<CRActivity.completedDate, IsNotNull, And<CRActivity.uistatus, Equal<ActivityStatusListAttribute.completed>>>>, True>, False>), typeof (LastActivityCalc<CRActivityStatistics.lastIncomingActivityNoteID, CRActivity.noteID>))]
[PXUnboundFormula(typeof (Switch<Case<Where<CRActivity.incoming, Equal<True>, And<CRActivity.completedDate, IsNotNull, And<CRActivity.uistatus, Equal<ActivityStatusListAttribute.completed>>>>, True>, False>), typeof (LastActivityCalc<CRActivityStatistics.lastIncomingActivityDate, CRActivity.completedDate>))]
[PXUnboundFormula(typeof (Switch<Case<Where<CRActivity.outgoing, Equal<True>, And<CRActivity.completedDate, IsNotNull, And<CRActivity.uistatus, Equal<ActivityStatusListAttribute.completed>>>>, True>, False>), typeof (LastActivityCalc<CRActivityStatistics.lastOutgoingActivityNoteID, CRActivity.noteID>))]
[PXUnboundFormula(typeof (Switch<Case<Where<CRActivity.outgoing, Equal<True>, And<CRActivity.completedDate, IsNotNull, And<CRActivity.uistatus, Equal<ActivityStatusListAttribute.completed>>>>, True>, False>), typeof (LastActivityCalc<CRActivityStatistics.lastOutgoingActivityDate, CRActivity.completedDate>))]
[PXUnboundFormula(typeof (Switch<Case<Where<CRActivity.outgoing, Equal<True>, And<CRActivity.completedDate, IsNotNull, And<CRActivity.uistatus, Equal<ActivityStatusListAttribute.completed>>>>, True>, False>), typeof (FirstActivityCalc<CRActivityStatistics.initialOutgoingActivityCompletedAtDate, CRActivity.completedDate>))]
[PXUnboundFormula(typeof (Switch<Case<Where<CRActivity.outgoing, Equal<True>, And<CRActivity.completedDate, IsNotNull, And<CRActivity.uistatus, Equal<ActivityStatusListAttribute.completed>>>>, True>, False>), typeof (FirstActivityCalc<CRActivityStatistics.initialOutgoingActivityCompletedAtNoteID, CRActivity.noteID>))]
[PXUnboundFormula(typeof (Switch<Case<Where<CRActivity.incoming, Equal<True>, And<CRActivity.completedDate, IsNotNull, And<CRActivity.uistatus, Equal<ActivityStatusListAttribute.completed>>>>, True>, False>), typeof (FirstActivityCalc<CRActivityStatistics.initialIncomingActivityNoteID, CRActivity.noteID>))]
[PXUnboundFormula(typeof (Switch<Case<Where<CRActivity.incoming, Equal<True>, And<CRActivity.completedDate, IsNotNull, And<CRActivity.uistatus, Equal<ActivityStatusListAttribute.completed>>>>, True>, False>), typeof (FirstActivityCalc<CRActivityStatistics.initialIncomingActivityDate, CRActivity.completedDate>))]
public sealed class CRActivityStatisticFormulas : PXAggregateAttribute
{
}
