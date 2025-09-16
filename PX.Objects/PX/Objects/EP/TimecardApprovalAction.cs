// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.TimecardApprovalAction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.PM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

public class TimecardApprovalAction : 
  EPApprovalAutomation<EPTimeCard, EPTimeCard.isApproved, EPTimeCard.isRejected, EPTimeCard.isHold, EPSetupTimeCardApproval>
{
  public TimecardApprovalAction(PXGraph graph)
    : base(graph)
  {
  }

  public TimecardApprovalAction(PXGraph graph, Delegate @delegate)
    : base(graph, @delegate)
  {
  }

  public override bool Approve(EPTimeCard source)
  {
    this.OnBeforeApproval(source);
    return base.Approve(source);
  }

  public override bool IsApprover(EPTimeCard source)
  {
    List<object> source1 = this._Pending.SelectMulti((object) this.GetSourceNoteID((object) source));
    if (!source1.Any<object>())
      return true;
    foreach (EPApproval epApproval in source1)
    {
      if (this.ValidateApprovalAccess(epApproval) || this.IsTaskApprover(epApproval.RefNoteID))
        return true;
    }
    return false;
  }

  public virtual bool IsTaskApprover(Guid? refNoteID)
  {
    PX.Objects.CR.BAccount baccount = (PX.Objects.CR.BAccount) new FbqlSelect<SelectFromBase<PX.Objects.CR.BAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.CR.BAccount.defContactID, IBqlInt>.IsEqual<P.AsInt>>, PX.Objects.CR.BAccount>.View(this._Graph).Select((object) PXAccess.GetContactID()).FirstOrDefault<PXResult<PX.Objects.CR.BAccount>>();
    if (baccount == null)
      return false;
    return new FbqlSelect<SelectFromBase<EPTimeCard, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<EPTimeCardSummary>.On<EPTimeCardSummary.FK.Timecard>>, FbqlJoins.Inner<PMTask>.On<EPTimeCardSummary.FK.ProjectTask>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<EPTimeCard.noteID, Equal<P.AsGuid>>>>>.And<BqlOperand<PMTask.approverID, IBqlInt>.IsEqual<P.AsInt>>>, EPTimeCard>.View(this._Graph).Select((object) refNoteID, (object) baccount.BAccountID).Any<PXResult<EPTimeCard>>();
  }

  protected virtual void OnBeforeApproval(EPTimeCard row)
  {
    if (row == null)
      return;
    bool flag = false;
    foreach (PXResult activity in ((TimeCardMaint) this._Graph).activities())
    {
      TimeCardMaint.EPTimecardDetail row1 = (TimeCardMaint.EPTimecardDetail) activity[typeof (TimeCardMaint.EPTimecardDetail)];
      if (!row1.Released.GetValueOrDefault() && (row1.TimeSpent.GetValueOrDefault() != 0 || row1.TimeBillable.GetValueOrDefault() != 0) && row1.ApprovalStatus != "CD" && row1.ApprovalStatus != "AP" && row1.ApprovalStatus != "CL")
      {
        ((TimeCardMaint) this._Graph).Activities.Cache.RaiseExceptionHandling<TimeCardMaint.EPTimecardDetail.timeSpent>((object) row1, (object) row1.TimeSpent, (Exception) new PXSetPropertyException("The activity is not approved.", PXErrorLevel.RowError));
        flag = true;
      }
    }
    if (flag)
      throw new PXException("At least one activity of the time card is not approved. The time card can be approved only when all its activities are approved.");
  }

  [Obsolete]
  public virtual bool IsTaskApprover(Guid? refNoteID, int? ownerID)
  {
    return this.IsTaskApprover(refNoteID);
  }
}
