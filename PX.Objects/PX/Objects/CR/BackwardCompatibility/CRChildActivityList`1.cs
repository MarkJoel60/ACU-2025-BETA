// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BackwardCompatibility.CRChildActivityList`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.BackwardCompatibility;

/// <exclude />
[Obsolete]
public class CRChildActivityList<TParentActivity> : 
  CRActivityListBase<TParentActivity, CRChildActivity>
  where TParentActivity : CRActivity, new()
{
  public CRChildActivityList(PXGraph graph)
    : base(graph)
  {
    // ISSUE: method pointer
    ((PXSelectBase) this)._Graph.FieldDefaulting.AddHandler<CRActivity.parentNoteID>(new PXFieldDefaulting((object) this, __methodptr(ParentNoteID_FieldDefaulting)));
    // ISSUE: method pointer
    ((PXSelectBase) this)._Graph.FieldDefaulting.AddHandler<CRChildActivity.parentNoteID>(new PXFieldDefaulting((object) this, __methodptr(ParentNoteID_FieldDefaulting)));
    // ISSUE: method pointer
    ((PXSelectBase) this)._Graph.FieldDefaulting.AddHandler<CRSMEmail.parentNoteID>(new PXFieldDefaulting((object) this, __methodptr(ParentNoteID_FieldDefaulting)));
    // ISSUE: method pointer
    ((PXSelectBase) this)._Graph.FieldDefaulting.AddHandler<PMTimeActivity.projectID>(new PXFieldDefaulting((object) this, __methodptr(ProjectID_FieldDefaulting)));
    // ISSUE: method pointer
    ((PXSelectBase) this)._Graph.FieldDefaulting.AddHandler<PMTimeActivity.projectTaskID>(new PXFieldDefaulting((object) this, __methodptr(ProjectTaskID_FieldDefaulting)));
    // ISSUE: method pointer
    ((PXSelectBase) this)._Graph.FieldDefaulting.AddHandler<PMTimeActivity.costCodeID>(new PXFieldDefaulting((object) this, __methodptr(ProjectCostCodeID_FieldDefaulting)));
  }

  private void ProjectID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    PXCache cach = sender.Graph.Caches[typeof (CRPMTimeActivity)];
    if (cach.Current == null)
      return;
    e.NewValue = (object) ((CRPMTimeActivity) cach.Current).ProjectID;
  }

  private void ProjectTaskID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    PXCache cach = sender.Graph.Caches[typeof (CRPMTimeActivity)];
    if (cach.Current == null)
      return;
    e.NewValue = (object) ((CRPMTimeActivity) cach.Current).ProjectTaskID;
  }

  private void ProjectCostCodeID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    PXCache cach = sender.Graph.Caches[typeof (CRPMTimeActivity)];
    if (cach.Current == null)
      return;
    e.NewValue = (object) ((CRPMTimeActivity) cach.Current).CostCodeID;
  }

  private void ParentNoteID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    PXCache cach = sender.Graph.Caches[typeof (TParentActivity)];
    if (cach.Current == null)
      return;
    e.NewValue = (object) ((TParentActivity) cach.Current).NoteID;
  }

  [Obsolete]
  public IEnumerable SelectByParentNoteID(object parentNoteId)
  {
    return (IEnumerable) GraphHelper.RowCast<CRChildActivity>((IEnumerable) PXSelectBase<CRChildActivity, PXSelect<CRChildActivity, Where<CRChildActivity.parentNoteID, Equal<Required<CRChildActivity.parentNoteID>>>>.Config>.Select(((PXSelectBase) this)._Graph, new object[1]
    {
      parentNoteId
    }));
  }

  /// <summary>
  /// Check is any billable child time activity exist or not
  /// </summary>
  /// <param name="parentNoteId"></param>
  /// <returns>true if exists, otherwise false</returns>
  public bool AnyBillableChildExists(object parentNoteId)
  {
    return ((IQueryable<PXResult<CRChildActivity>>) PXSelectBase<CRChildActivity, PXViewOf<CRChildActivity>.BasedOn<SelectFromBase<CRChildActivity, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRPMTimeActivity.isBillable, Equal<True>>>>>.And<BqlOperand<CRChildActivity.parentNoteID, IBqlGuid>.IsEqual<P.AsGuid>>>>.Config>.Select(((PXSelectBase) this)._Graph, new object[1]
    {
      parentNoteId
    })).Any<PXResult<CRChildActivity>>();
  }

  /// <summary>Return time totals of children activities</summary>
  public (int timeSpent, int overtimeSpent, int timeBillable, int overtimeBillable) GetChildrenTimeTotals(
    object parentNoteId)
  {
    BqlCommand bqlCommand = ((PXSelectBase) this).View.BqlSelect.OrderByNew<BqlNone>().AggregateNew(BqlTemplate.FromType(typeof (Aggregate<Sum<CRChildActivity.timeSpent, Sum<CRChildActivity.overtimeSpent, Sum<CRChildActivity.timeBillable, Sum<CRChildActivity.overtimeBillable, GroupBy<CRChildActivity.parentNoteID>>>>>>)).ToType());
    List<System.Type> typeList = new List<System.Type>()
    {
      typeof (CRChildActivity.timeSpent),
      typeof (CRChildActivity.overtimeSpent),
      typeof (CRChildActivity.timeBillable),
      typeof (CRChildActivity.overtimeBillable)
    };
    PXView pxView = new PXView(((PXSelectBase) this)._Graph, true, bqlCommand);
    using (new PXFieldScope(pxView, (IEnumerable<System.Type>) typeList, false))
    {
      CRChildActivity crChildActivity = PXResult<CRChildActivity>.op_Implicit(pxView.SelectSingle(new object[1]
      {
        parentNoteId
      }) as PXResult<CRChildActivity>);
      return (((int?) crChildActivity?.TimeSpent).GetValueOrDefault(), ((int?) crChildActivity?.OvertimeSpent).GetValueOrDefault(), ((int?) crChildActivity?.TimeBillable).GetValueOrDefault(), ((int?) crChildActivity?.OvertimeBillable).GetValueOrDefault());
    }
  }

  protected override void ReadNoteIDFieldInfo(out string noteField, out System.Type noteBqlField)
  {
    noteField = typeof (CRActivity.refNoteID).Name;
    noteBqlField = ((PXSelectBase) this)._Graph.Caches[typeof (TParentActivity)].GetBqlField(noteField);
  }

  protected override void SetCommandCondition(Delegate handler = null)
  {
    BqlCommand bqlCommand = this.OriginalCommand.WhereAnd(BqlCommand.Compose(new System.Type[12]
    {
      typeof (Where<,,>),
      typeof (CRChildActivity.parentNoteID),
      typeof (Equal<>),
      typeof (Optional<>),
      typeof (TParentActivity).GetNestedType(typeof (CRActivity.noteID).Name),
      typeof (And<>),
      typeof (Where<,,>),
      typeof (CRChildActivity.isCorrected),
      typeof (NotEqual<True>),
      typeof (Or<,>),
      typeof (CRChildActivity.isCorrected),
      typeof (IsNull)
    }));
    if ((object) handler == null)
      ((PXSelectBase) this).View = new PXView(((PXSelectBase) this).View.Graph, ((PXSelectBase) this).View.IsReadOnly, bqlCommand);
    else
      ((PXSelectBase) this).View = new PXView(((PXSelectBase) this).View.Graph, ((PXSelectBase) this).View.IsReadOnly, bqlCommand, handler);
  }
}
