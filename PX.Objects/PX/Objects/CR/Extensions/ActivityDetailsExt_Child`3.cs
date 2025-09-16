// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.ActivityDetailsExt_Child`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.SP;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.Extensions;

public abstract class ActivityDetailsExt_Child<TGraph, TPrimaryEntity, TPrimaryEntity_NoteID> : 
  ActivityDetailsExt<TGraph, TPrimaryEntity, CRChildActivity, CRChildActivity.noteID>
  where TGraph : PXGraph, new()
  where TPrimaryEntity : CRActivity, IBqlTable, INotable, new()
  where TPrimaryEntity_NoteID : IBqlField, IImplement<IBqlCastableTo<IBqlGuid>>
{
  public override System.Type GetLinkConditionClause()
  {
    return typeof (Where<CRChildActivity.parentNoteID, Equal<Current<TPrimaryEntity_NoteID>>, And<Where<CRChildActivity.isCorrected, NotEqual<True>, Or<CRChildActivity.isCorrected, IsNull>>>>);
  }

  public override System.Type GetOrderByClause()
  {
    return typeof (OrderBy<Desc<CRChildActivity.createdDateTime>>);
  }

  public override System.Type GetClassConditionClause()
  {
    return typeof (Where<CRChildActivity.classID, GreaterEqual<Zero>>);
  }

  public override System.Type GetPrivateConditionClause()
  {
    return !PortalHelper.IsPortalContext((PortalContexts) 3) ? (System.Type) null : typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRChildActivity.isPrivate, IsNull>>>>.Or<BqlOperand<CRChildActivity.isPrivate, IBqlBool>.IsEqual<False>>>);
  }

  public override void CreateTimeActivity(PXGraph targetGraph, int classID, string activityType)
  {
    base.CreateTimeActivity(targetGraph, classID, activityType);
    PMTimeActivity current1 = this.Base.Caches[typeof (PMTimeActivity)]?.Current as PMTimeActivity;
    PXCache cach = targetGraph.Caches[typeof (PMTimeActivity)];
    PMTimeActivity current2 = cach?.Current as PMTimeActivity;
    if (current1 == null || current2 == null)
      return;
    cach.SetValueExt<PMTimeActivity.projectID>((object) current2, (object) current1.ProjectID);
    cach.SetValueExt<PMTimeActivity.projectTaskID>((object) current2, (object) current1.ProjectTaskID);
    cach.SetValueExt<PMTimeActivity.costCodeID>((object) current2, (object) current1.CostCodeID);
  }

  public override void InitializeActivity(CRActivity row)
  {
    base.InitializeActivity(row);
    CRActivity crActivity = row;
    if (crActivity.ParentNoteID.HasValue)
      return;
    Guid? noteId;
    crActivity.ParentNoteID = noteId = (Guid?) (this.Base.Caches[typeof (TPrimaryEntity)]?.Current as TPrimaryEntity)?.NoteID;
  }

  public override Guid? GetRefNoteID()
  {
    return !(this.Base.Caches[typeof (TPrimaryEntity)].Current is TPrimaryEntity current) ? new Guid?() : current.RefNoteID;
  }

  /// <summary>
  /// Check is any billable child time activity exist or not
  /// </summary>
  /// <param name="parentNoteId"></param>
  /// <returns>true if exists, otherwise false</returns>
  public bool AnyBillableChildExists(object parentNoteId)
  {
    return ((IQueryable<PXResult<CRChildActivity>>) PXSelectBase<CRChildActivity, PXViewOf<CRChildActivity>.BasedOn<SelectFromBase<CRChildActivity, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRPMTimeActivity.isBillable, Equal<True>>>>>.And<BqlOperand<CRChildActivity.parentNoteID, IBqlGuid>.IsEqual<P.AsGuid>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      parentNoteId
    })).Any<PXResult<CRChildActivity>>();
  }

  /// <summary>Return time totals of children activities</summary>
  public (int timeSpent, int overtimeSpent, int timeBillable, int overtimeBillable) GetChildrenTimeTotals(
    object parentNoteId)
  {
    BqlCommand bqlCommand = ((PXSelectBase) this.Activities).View.BqlSelect.OrderByNew<BqlNone>().AggregateNew(BqlTemplate.FromType(typeof (Aggregate<Sum<CRChildActivity.timeSpent, Sum<CRChildActivity.overtimeSpent, Sum<CRChildActivity.timeBillable, Sum<CRChildActivity.overtimeBillable, GroupBy<CRChildActivity.parentNoteID>>>>>>)).ToType());
    List<System.Type> typeList = new List<System.Type>()
    {
      typeof (CRChildActivity.timeSpent),
      typeof (CRChildActivity.overtimeSpent),
      typeof (CRChildActivity.timeBillable),
      typeof (CRChildActivity.overtimeBillable)
    };
    PXView pxView = new PXView((PXGraph) this.Base, true, bqlCommand);
    using (new PXFieldScope(pxView, (IEnumerable<System.Type>) typeList, false))
    {
      CRChildActivity crChildActivity = PXResult<CRChildActivity>.op_Implicit(pxView.SelectSingle(new object[1]
      {
        parentNoteId
      }) as PXResult<CRChildActivity>);
      int? nullable1 = (int?) crChildActivity?.TimeSpent;
      int valueOrDefault1 = nullable1.GetValueOrDefault();
      int? nullable2;
      if (crChildActivity == null)
      {
        nullable1 = new int?();
        nullable2 = nullable1;
      }
      else
        nullable2 = crChildActivity.OvertimeSpent;
      nullable1 = nullable2;
      int valueOrDefault2 = nullable1.GetValueOrDefault();
      int? nullable3;
      if (crChildActivity == null)
      {
        nullable1 = new int?();
        nullable3 = nullable1;
      }
      else
        nullable3 = crChildActivity.TimeBillable;
      nullable1 = nullable3;
      int valueOrDefault3 = nullable1.GetValueOrDefault();
      int? nullable4;
      if (crChildActivity == null)
      {
        nullable1 = new int?();
        nullable4 = nullable1;
      }
      else
        nullable4 = crChildActivity.OvertimeBillable;
      nullable1 = nullable4;
      int valueOrDefault4 = nullable1.GetValueOrDefault();
      return (valueOrDefault1, valueOrDefault2, valueOrDefault3, valueOrDefault4);
    }
  }
}
