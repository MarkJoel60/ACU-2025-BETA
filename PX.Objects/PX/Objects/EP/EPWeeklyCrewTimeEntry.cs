// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPWeeklyCrewTimeEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.PM;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.EP;

public class EPWeeklyCrewTimeEntry : 
  PXGraph<
  #nullable disable
  EPWeeklyCrewTimeEntry, EPWeeklyCrewTimeActivity>,
  PXImportAttribute.IPXPrepareItems
{
  public FbqlSelect<SelectFromBase<EPWeeklyCrewTimeActivity, TypeArrayOf<IFbqlJoin>.Empty>, EPWeeklyCrewTimeActivity>.View Document;
  public SelectFrom<PMTimeActivity> PMTimeActivityDummyView;
  public FbqlSelect<SelectFromBase<EPCompanyTreeMember, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  EPCompanyTreeMember.workGroupID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  EPWeeklyCrewTimeActivity.workgroupID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  EPCompanyTreeMember.active, IBqlBool>.IsEqual<
  #nullable disable
  True>>>, EPCompanyTreeMember>.View OriginalGroupMembers;
  [PXImport]
  public FbqlSelect<SelectFromBase<EPActivityApprove, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<EPEmployee>.On<BqlOperand<
  #nullable enable
  EPEmployee.defContactID, IBqlInt>.IsEqual<
  #nullable disable
  EPActivityApprove.ownerID>>>, FbqlJoins.Inner<EPEarningType>.On<BqlOperand<
  #nullable enable
  EPEarningType.typeCD, IBqlString>.IsEqual<
  #nullable disable
  PMTimeActivity.earningTypeID>>>>.Where<BqlOperand<
  #nullable enable
  EPActivityApprove.workgroupID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  EPWeeklyCrewTimeActivity.workgroupID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  EPActivityApprove>.View TimeActivities;
  public FbqlSelect<SelectFromBase<EPTimeActivitiesSummary, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  EPTimeActivitiesSummary.workgroupID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  EPWeeklyCrewTimeActivity.workgroupID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  EPTimeActivitiesSummary.week, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  EPWeeklyCrewTimeActivity.week, IBqlInt>.FromCurrent>>>, 
  #nullable disable
  EPTimeActivitiesSummary>.View WorkgroupTimeSummary;
  public FbqlSelect<SelectFromBase<EPTimeActivitiesSummary, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<EPCompanyTreeMember>.On<BqlOperand<
  #nullable enable
  EPTimeActivitiesSummary.contactID, IBqlInt>.IsEqual<
  #nullable disable
  EPCompanyTreeMember.contactID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  EPTimeActivitiesSummary.workgroupID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  EPWeeklyCrewTimeActivity.workgroupID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  EPTimeActivitiesSummary.week, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  EPWeeklyCrewTimeActivity.week, IBqlInt>.FromCurrent>>>, 
  #nullable disable
  EPTimeActivitiesSummary>.View CompanyTreeMembers;
  public PXFilter<EPWeeklyCrewTimeActivityFilter> Filter;
  /// Using EPActivityApprove2 to have a different cache than <see cref="F:PX.Objects.EP.EPWeeklyCrewTimeEntry.TimeActivities" />
  ///  data view.
  public FbqlSelect<SelectFromBase<EPActivityApprove2, TypeArrayOf<IFbqlJoin>.Empty>, EPActivityApprove2>.View BulkEntryTimeActivities;
  public PXAction<EPWeeklyCrewTimeActivity> EnterBulkTime;
  public PXAction<EPWeeklyCrewTimeActivity> InsertForBulkTimeEntry;
  public PXAction<EPWeeklyCrewTimeActivity> CopySelectedActivity;
  public PXAction<EPWeeklyCrewTimeActivity> LoadLastWeekActivities;
  public PXAction<EPWeeklyCrewTimeActivity> LoadLastWeekMembers;
  public PXAction<EPWeeklyCrewTimeActivity> DeleteMember;
  public PXAction<EPWeeklyCrewTimeActivity> CompleteAllActivities;

  public virtual IEnumerable timeActivities()
  {
    IEnumerable<object> objects = (IEnumerable<object>) new List<object>();
    if (!((PXSelectBase<EPWeeklyCrewTimeActivity>) this.Document).Current.WorkgroupID.HasValue || !((PXSelectBase<EPWeeklyCrewTimeActivity>) this.Document).Current.Week.HasValue)
      return (IEnumerable) objects;
    BqlCommand bqlCommand1 = ((PXSelectBase) this.TimeActivities).View.BqlSelect;
    int? nullable1 = ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.ProjectID;
    if (nullable1.HasValue)
      bqlCommand1 = bqlCommand1.WhereAnd<Where<EPActivityApprove.projectID, Equal<Current<EPWeeklyCrewTimeActivityFilter.projectID>>>>();
    nullable1 = ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.ProjectTaskID;
    if (nullable1.HasValue)
      bqlCommand1 = bqlCommand1.WhereAnd<Where<EPActivityApprove.projectTaskID, Equal<Current<EPWeeklyCrewTimeActivityFilter.projectTaskID>>>>();
    object[] dateParam = this.GetDateParam();
    System.Type type1 = BqlCommand.Compose(new System.Type[8]
    {
      typeof (Where<,,>),
      typeof (EPActivityApprove.date),
      typeof (GreaterEqual<>),
      typeof (P.AsDateTime),
      typeof (And<,>),
      typeof (EPActivityApprove.date),
      typeof (LessEqual<>),
      typeof (P.AsDateTime)
    });
    System.Type type2 = BqlCommand.Compose(new System.Type[3]
    {
      typeof (Where<,>),
      typeof (EPActivityApprove.date),
      typeof (IsNull)
    });
    BqlCommand bqlCommand2 = bqlCommand1.WhereAnd(BqlCommand.Compose(new System.Type[4]
    {
      typeof (Where2<,>),
      type1,
      typeof (Or<>),
      type2
    }));
    IEnumerable<object> source;
    if (((PXSelectBase) this.TimeActivities).View.GetExternalFilters() != null)
    {
      int num1 = 0;
      int num2 = 0;
      source = (IEnumerable<object>) new PXView((PXGraph) this, false, bqlCommand2).Select(PXView.Currents, dateParam, PXView.Searches, PXView.SortColumns, PXView.Descendings, ((PXSelectBase) this.TimeActivities).View.GetExternalFilters(), ref num1, 0, ref num2);
    }
    else
      source = (IEnumerable<object>) new PXView((PXGraph) this, false, bqlCommand2).SelectMulti(dateParam);
    nullable1 = ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.Day;
    if (nullable1.HasValue)
      source = (IEnumerable<object>) source.Cast<PXResult<EPActivityApprove, EPEmployee, EPEarningType>>().Where<PXResult<EPActivityApprove, EPEmployee, EPEarningType>>((Func<PXResult<EPActivityApprove, EPEmployee, EPEarningType>, bool>) (x =>
      {
        int? dayOfWeek = PXResult<EPActivityApprove, EPEmployee, EPEarningType>.op_Implicit(x).DayOfWeek;
        int? day = ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.Day;
        return dayOfWeek.GetValueOrDefault() == day.GetValueOrDefault() & dayOfWeek.HasValue == day.HasValue;
      }));
    ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.RegularTime = new int?(0);
    ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.Overtime = new int?(0);
    ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.BillableTime = new int?(0);
    ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.BillableOvertime = new int?(0);
    foreach (PXResult<EPActivityApprove, EPEmployee, EPEarningType> pxResult in source)
    {
      EPActivityApprove epActivityApprove = PXResult<EPActivityApprove, EPEmployee, EPEarningType>.op_Implicit(pxResult);
      EPEarningType epEarningType = PXResult<EPActivityApprove, EPEmployee, EPEarningType>.op_Implicit(pxResult);
      EPWeeklyCrewTimeActivityFilter current1 = ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current;
      nullable1 = current1.RegularTime;
      bool? isOvertime = epEarningType.IsOvertime;
      int? nullable2;
      int num3;
      if (!isOvertime.GetValueOrDefault())
      {
        nullable2 = epActivityApprove.TimeSpent;
        num3 = nullable2.GetValueOrDefault();
      }
      else
        num3 = 0;
      int num4 = num3;
      int? nullable3;
      if (!nullable1.HasValue)
      {
        nullable2 = new int?();
        nullable3 = nullable2;
      }
      else
        nullable3 = new int?(nullable1.GetValueOrDefault() + num4);
      current1.RegularTime = nullable3;
      EPWeeklyCrewTimeActivityFilter current2 = ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current;
      nullable1 = current2.BillableTime;
      isOvertime = epEarningType.IsOvertime;
      int num5;
      if (!isOvertime.GetValueOrDefault())
      {
        nullable2 = epActivityApprove.TimeBillable;
        num5 = nullable2.GetValueOrDefault();
      }
      else
        num5 = 0;
      int num6 = num5;
      int? nullable4;
      if (!nullable1.HasValue)
      {
        nullable2 = new int?();
        nullable4 = nullable2;
      }
      else
        nullable4 = new int?(nullable1.GetValueOrDefault() + num6);
      current2.BillableTime = nullable4;
      EPWeeklyCrewTimeActivityFilter current3 = ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current;
      nullable1 = current3.Overtime;
      nullable2 = epActivityApprove.OvertimeSpent;
      int valueOrDefault1 = nullable2.GetValueOrDefault();
      int? nullable5;
      if (!nullable1.HasValue)
      {
        nullable2 = new int?();
        nullable5 = nullable2;
      }
      else
        nullable5 = new int?(nullable1.GetValueOrDefault() + valueOrDefault1);
      current3.Overtime = nullable5;
      EPWeeklyCrewTimeActivityFilter current4 = ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current;
      nullable1 = current4.BillableOvertime;
      nullable2 = epActivityApprove.OvertimeBillable;
      int valueOrDefault2 = nullable2.GetValueOrDefault();
      int? nullable6;
      if (!nullable1.HasValue)
      {
        nullable2 = new int?();
        nullable6 = nullable2;
      }
      else
        nullable6 = new int?(nullable1.GetValueOrDefault() + valueOrDefault2);
      current4.BillableOvertime = nullable6;
    }
    return (IEnumerable) source;
  }

  public virtual IEnumerable workgroupTimeSummary()
  {
    IEnumerable<EPTimeActivitiesSummary> returnedList = (IEnumerable<EPTimeActivitiesSummary>) new List<EPTimeActivitiesSummary>();
    if (!((PXSelectBase<EPWeeklyCrewTimeActivity>) this.Document).Current.WorkgroupID.HasValue || !((PXSelectBase<EPWeeklyCrewTimeActivity>) this.Document).Current.Week.HasValue)
    {
      ((PXAction) this.DeleteMember).SetEnabled(false);
      return (IEnumerable) returnedList;
    }
    returnedList = new PXView((PXGraph) this, false, ((PXSelectBase) this.WorkgroupTimeSummary).View.BqlSelect).SelectMulti(Array.Empty<object>()).Select<object, EPTimeActivitiesSummary>((Func<object, EPTimeActivitiesSummary>) (x => (EPTimeActivitiesSummary) x));
    IEnumerable<EPCompanyTreeMember> firstTableItems = ((PXSelectBase<EPCompanyTreeMember>) this.OriginalGroupMembers).Select(Array.Empty<object>()).FirstTableItems;
    int num1 = returnedList.Count<EPTimeActivitiesSummary>((Func<EPTimeActivitiesSummary, bool>) (x => x.IsWithoutActivities.GetValueOrDefault()));
    bool isDirty = ((PXSelectBase) this.WorkgroupTimeSummary).Cache.IsDirty;
    Func<EPCompanyTreeMember, bool> predicate = (Func<EPCompanyTreeMember, bool>) (x => !returnedList.Any<EPTimeActivitiesSummary>((Func<EPTimeActivitiesSummary, bool>) (y =>
    {
      int? contactId1 = x.ContactID;
      int? contactId2 = y.ContactID;
      return contactId1.GetValueOrDefault() == contactId2.GetValueOrDefault() & contactId1.HasValue == contactId2.HasValue;
    })));
    foreach (EPCompanyTreeMember companyTreeMember in firstTableItems.Where<EPCompanyTreeMember>(predicate))
    {
      EPTimeActivitiesSummary element = ((PXSelectBase<EPTimeActivitiesSummary>) this.WorkgroupTimeSummary).Insert(new EPTimeActivitiesSummary()
      {
        ContactID = companyTreeMember.ContactID
      });
      returnedList = returnedList.Append<EPTimeActivitiesSummary>(element);
      ++num1;
    }
    ((PXSelectBase) this.WorkgroupTimeSummary).Cache.IsDirty = isDirty;
    ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.TotalWorkgroupMembers = new int?(returnedList.Count<EPTimeActivitiesSummary>());
    EPWeeklyCrewTimeActivityFilter current = ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current;
    int? workgroupMembers = ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.TotalWorkgroupMembers;
    int num2 = num1;
    int? nullable = workgroupMembers.HasValue ? new int?(workgroupMembers.GetValueOrDefault() - num2) : new int?();
    current.TotalWorkgroupMembersWithActivities = nullable;
    ((PXAction) this.DeleteMember).SetEnabled(returnedList.Any<EPTimeActivitiesSummary>());
    return (IEnumerable) returnedList;
  }

  public virtual IEnumerable companyTreeMembers()
  {
    BqlCommand bqlCommand = ((PXSelectBase) this.CompanyTreeMembers).View.BqlSelect;
    bool? showAllMembers = ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.ShowAllMembers;
    bool flag = false;
    if (showAllMembers.GetValueOrDefault() == flag & showAllMembers.HasValue)
      bqlCommand = bqlCommand.WhereAnd<Where<EPTimeActivitiesSummary.status, Equal<WorkgroupMemberStatusAttribute.permanentActive>, Or<EPTimeActivitiesSummary.status, Equal<WorkgroupMemberStatusAttribute.temporaryActive>>>>();
    List<object> source = new PXView((PXGraph) this, false, bqlCommand).SelectMulti(Array.Empty<object>());
    return !source.Any<object>() ? (IEnumerable) ((PXSelectBase<EPTimeActivitiesSummary>) this.WorkgroupTimeSummary).Select(Array.Empty<object>()) : (IEnumerable) EnumerableExtensions.Distinct<PXResult<EPTimeActivitiesSummary, EPCompanyTreeMember>, int?>(source.Cast<PXResult<EPTimeActivitiesSummary, EPCompanyTreeMember>>(), (Func<PXResult<EPTimeActivitiesSummary, EPCompanyTreeMember>, int?>) (x => PXResult<EPTimeActivitiesSummary, EPCompanyTreeMember>.op_Implicit(x).ContactID));
  }

  public IEnumerable bulkEntryTimeActivities()
  {
    return ((PXGraph) this).Caches[typeof (EPActivityApprove2)].Inserted;
  }

  [PXButton]
  [PXUIField(DisplayName = "Mass Enter Time", Enabled = true)]
  public virtual void enterBulkTime()
  {
    if (((PXSelectBase<EPTimeActivitiesSummary>) this.CompanyTreeMembers).AskExt() != 1)
      return;
    this.insertForBulkTimeEntry();
  }

  [PXButton]
  [PXUIField(DisplayName = "Add")]
  public virtual void insertForBulkTimeEntry()
  {
    IEnumerable<EPTimeActivitiesSummary> activitiesSummaries = ((PXSelectBase<EPTimeActivitiesSummary>) this.CompanyTreeMembers).Select(Array.Empty<object>()).FirstTableItems.Where<EPTimeActivitiesSummary>((Func<EPTimeActivitiesSummary, bool>) (x => x.Selected.GetValueOrDefault()));
    foreach (PXResult<EPActivityApprove2> pxResult in ((PXSelectBase<EPActivityApprove2>) this.BulkEntryTimeActivities).Select(Array.Empty<object>()))
    {
      EPActivityApprove original = (EPActivityApprove) PXResult<EPActivityApprove2>.op_Implicit(pxResult);
      foreach (EPTimeActivitiesSummary activitiesSummary in activitiesSummaries)
      {
        EPActivityApprove copy = ((PXSelectBase<EPActivityApprove>) this.TimeActivities).Insert();
        this.RestoreCopy(copy, original);
        ((PXSelectBase) this.TimeActivities).Cache.SetValueExt<EPActivityApprove.ownerID>((object) copy, (object) activitiesSummary.ContactID);
        if (copy.UnionID == null)
          ((PXSelectBase) this.TimeActivities).Cache.SetDefaultExt<PMTimeActivity.unionID>((object) copy);
        ((PXSelectBase<EPActivityApprove>) this.TimeActivities).Update(copy);
        int? nullable = original.ProjectID;
        if (nullable.HasValue)
        {
          nullable = original.ProjectTaskID;
          if (nullable.HasValue)
          {
            nullable = original.ProjectTaskID;
            int? projectTaskId = copy.ProjectTaskID;
            if (!(nullable.GetValueOrDefault() == projectTaskId.GetValueOrDefault() & nullable.HasValue == projectTaskId.HasValue))
              ((PXSelectBase<EPActivityApprove>) this.TimeActivities).SetValueExt<EPActivityApprove.projectTaskID>(copy, (object) original.ProjectTaskID.Value);
          }
        }
      }
    }
    ((PXGraph) this).Caches[typeof (EPActivityApprove2)].Clear();
  }

  [PXButton]
  [PXUIField(DisplayName = "Copy Selected Line")]
  public virtual void copySelectedActivity()
  {
    EPActivityApprove current = ((PXSelectBase<EPActivityApprove>) this.TimeActivities).Current;
    EPActivityApprove copy = ((PXSelectBase<EPActivityApprove>) this.TimeActivities).Insert();
    this.RestoreCopy(copy, current);
    ((PXSelectBase<EPActivityApprove>) this.TimeActivities).Update(copy);
  }

  [PXButton]
  [PXUIField(DisplayName = "Load Activities from Previous Week")]
  public virtual void loadLastWeekActivities()
  {
    if (!((PXSelectBase<EPWeeklyCrewTimeActivity>) this.Document).Current.WorkgroupID.HasValue || !((PXSelectBase<EPWeeklyCrewTimeActivity>) this.Document).Current.Week.HasValue)
      return;
    foreach (PXResult<EPActivityApprove> pxResult in PXSelectBase<EPActivityApprove, PXViewOf<EPActivityApprove>.BasedOn<SelectFromBase<EPActivityApprove, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPActivityApprove.workgroupID, Equal<BqlField<EPWeeklyCrewTimeActivity.workgroupID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<EPActivityApprove.weekID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) this.GetLastWeekID()
    }))
    {
      EPActivityApprove original = PXResult<EPActivityApprove>.op_Implicit(pxResult);
      EPActivityApprove copy = ((PXSelectBase<EPActivityApprove>) this.TimeActivities).Insert();
      this.RestoreCopy(copy, original);
      copy.Date = new DateTime?(original.Date.Value.AddDays(7.0));
      copy.WeekID = new int?(((PXSelectBase<EPWeeklyCrewTimeActivity>) this.Document).Current.Week.Value);
      copy.Hold = new bool?(true);
      copy.TimeCardCD = (string) null;
      ((PXSelectBase<EPActivityApprove>) this.TimeActivities).Update(copy);
    }
  }

  [PXButton]
  [PXUIField(DisplayName = "Load Workgroup from Previous Week")]
  public virtual void loadLastWeekMembers()
  {
    if (!((PXSelectBase<EPWeeklyCrewTimeActivity>) this.Document).Current.WorkgroupID.HasValue || !((PXSelectBase<EPWeeklyCrewTimeActivity>) this.Document).Current.Week.HasValue)
      return;
    HashSet<int?> hashSet = ((PXSelectBase<EPTimeActivitiesSummary>) this.WorkgroupTimeSummary).Select(Array.Empty<object>()).FirstTableItems.Select<EPTimeActivitiesSummary, int?>((Func<EPTimeActivitiesSummary, int?>) (x => x.ContactID)).ToHashSet<int?>();
    foreach (PXResult<EPTimeActivitiesSummary> pxResult in PXSelectBase<EPTimeActivitiesSummary, PXViewOf<EPTimeActivitiesSummary>.BasedOn<SelectFromBase<EPTimeActivitiesSummary, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPTimeActivitiesSummary.workgroupID, Equal<BqlField<EPWeeklyCrewTimeActivity.workgroupID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<EPTimeActivitiesSummary.week, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) this.GetLastWeekID()
    }))
    {
      EPTimeActivitiesSummary activitiesSummary1 = PXResult<EPTimeActivitiesSummary>.op_Implicit(pxResult);
      if (!hashSet.Contains(activitiesSummary1.ContactID))
      {
        EPTimeActivitiesSummary activitiesSummary2 = new EPTimeActivitiesSummary();
        activitiesSummary2.ContactID = activitiesSummary1.ContactID;
        EPTimeActivitiesSummary activitiesSummary3 = activitiesSummary2;
        int? nullable1 = ((PXSelectBase<EPWeeklyCrewTimeActivity>) this.Document).Current.Week;
        int? nullable2 = new int?(nullable1.Value);
        activitiesSummary3.Week = nullable2;
        EPTimeActivitiesSummary activitiesSummary4 = activitiesSummary2;
        nullable1 = ((PXSelectBase<EPWeeklyCrewTimeActivity>) this.Document).Current.WorkgroupID;
        int? nullable3 = new int?(nullable1.Value);
        activitiesSummary4.WorkgroupID = nullable3;
        ((PXSelectBase<EPTimeActivitiesSummary>) this.WorkgroupTimeSummary).Insert(activitiesSummary2);
      }
    }
  }

  [PXButton]
  [PXUIField]
  public virtual void deleteMember()
  {
    if (!((PXSelectBase<EPTimeActivitiesSummary>) this.WorkgroupTimeSummary).Current.IsWithoutActivities.GetValueOrDefault())
      throw new PXException("This employee has time activities reported for the selected week and cannot be removed from the list.");
    ((PXSelectBase<EPTimeActivitiesSummary>) this.WorkgroupTimeSummary).Delete(((PXSelectBase<EPTimeActivitiesSummary>) this.WorkgroupTimeSummary).Current);
  }

  [PXButton]
  [PXUIField(DisplayName = "Complete Activities")]
  public virtual void completeAllActivities()
  {
    foreach (EPActivityApprove epActivityApprove in ((PXSelectBase<EPActivityApprove>) this.TimeActivities).Select(Array.Empty<object>()).FirstTableItems.Where<EPActivityApprove>((Func<EPActivityApprove, bool>) (x => x.ApprovalStatus == "OP")))
    {
      epActivityApprove.Hold = new bool?(false);
      epActivityApprove.ApprovalStatus = "CD";
      ((PXSelectBase<EPActivityApprove>) this.TimeActivities).Update(epActivityApprove);
    }
  }

  [PXMergeAttributes]
  [PXDefault]
  [PXUIField(DisplayName = "Employee")]
  public virtual void _(PX.Data.Events.CacheAttached<EPActivityApprove.ownerID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (EPWeeklyCrewTimeActivity.workgroupID))]
  public virtual void _(
    PX.Data.Events.CacheAttached<EPActivityApprove.workgroupID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (EPWeeklyCrewTimeActivity.workgroupID))]
  public virtual void _(
    PX.Data.Events.CacheAttached<EPActivityApprove2.workgroupID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (Search<PMProject.contractID, Where<PMProject.nonProject, Equal<True>>>))]
  public virtual void _(
    PX.Data.Events.CacheAttached<EPActivityApprove.projectID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (EPActivityDefaultWeekAttribute))]
  [PXDefault(typeof (EPWeeklyCrewTimeActivity.week))]
  public virtual void _(PX.Data.Events.CacheAttached<EPActivityApprove.weekID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  public virtual void _(PX.Data.Events.CacheAttached<EPActivityApprove.date> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  public virtual void _(PX.Data.Events.CacheAttached<EPActivityApprove.summary> e)
  {
  }

  [PXMergeAttributes]
  [PXUIEnabled(typeof (PMTimeActivity.isBillable))]
  public virtual void _(
    PX.Data.Events.CacheAttached<EPActivityApprove.timeBillable> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<EPActivityApprove> e)
  {
    this.UpdateReportedInTimeZoneIDIfNeeded(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<EPActivityApprove>>) e).Cache, e.Row, new DateTime?(), e.Row.Date);
  }

  public virtual void _(PX.Data.Events.FieldVerifying<EPActivityApprove.date> e)
  {
    EPActivityApprove row = e.Row as EPActivityApprove;
    if (PXWeekSelector2Attribute.GetWeekInfo((PXGraph) this, ((PXSelectBase<EPWeeklyCrewTimeActivity>) this.Document).Current.Week.Value).IsValid((DateTime) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<EPActivityApprove.date>, object, object>) e).NewValue))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<EPActivityApprove.date>, object, object>) e).NewValue = (object) null;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<EPActivityApprove.date>>) e).Cache.RaiseExceptionHandling<EPActivityApprove.date>((object) row, (object) row.Date, (Exception) new PXSetPropertyException("The specified date is outside the selected week.", (PXErrorLevel) 4));
  }

  public virtual void _(PX.Data.Events.RowSelected<EPActivityApprove> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetError<EPActivityApprove.date>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPActivityApprove>>) e).Cache, (object) e.Row, !e.Row.Date.HasValue ? $"'{"Date/Time"}' cannot be empty." : (string) null);
    PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPActivityApprove>>) e).Cache, (object) e.Row, e.Row.ApprovalStatus == "OP");
    PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPActivityApprove>>) e).Cache, (object) e.Row).ForAllFields((Action<PXUIFieldAttribute>) (field => field.Enabled = e.Row.ApprovalStatus == "OP")).For<EPActivityApprove.approvalStatus>((Action<PXUIFieldAttribute>) (field => field.Enabled = false)).For<EPActivityApprove.hold>((Action<PXUIFieldAttribute>) (field => field.Enabled = e.Row.ApprovalStatus == "OP" || e.Row.ApprovalStatus == "CD"));
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.reportedInTimeZoneID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPActivityApprove>>) e).Cache, (object) e.Row, false);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<EPActivityApprove> e)
  {
    if (e.Row.ApprovalStatus == "AP" || e.Row.Released.GetValueOrDefault())
    {
      ((PXSelectBase) this.TimeActivities).View.Ask(PXMessages.LocalizeFormatNoPrefix("Activity in status \"{0}\" cannot be deleted.", new object[1]
      {
        ((PX.Data.Events.Event<PXRowDeletingEventArgs, PX.Data.Events.RowDeleting<EPActivityApprove>>) e).Cache.GetValueExt<PMTimeActivity.approvalStatus>((object) e.Row)
      }), (MessageButtons) 0);
      e.Cancel = true;
    }
    else
    {
      if (e.Row.TimeCardCD == null)
        return;
      ((PXSelectBase) this.TimeActivities).View.Ask("This Activity assigned to the Time Card. You may do changes only in a Time Card screen.", (MessageButtons) 0);
      e.Cancel = true;
    }
  }

  public virtual void _(
    PX.Data.Events.FieldDefaulting<EPActivityApprove.projectID> e)
  {
    if (!((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.ProjectID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<EPActivityApprove.projectID>, object, object>) e).NewValue = (object) ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.ProjectID;
  }

  public virtual void _(
    PX.Data.Events.FieldDefaulting<EPActivityApprove.projectTaskID> e)
  {
    if (!((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.ProjectTaskID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<EPActivityApprove.projectTaskID>, object, object>) e).NewValue = (object) ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.ProjectTaskID;
  }

  public virtual void _(
    PX.Data.Events.FieldDefaulting<EPActivityApprove2.projectID> e)
  {
    if (!((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.ProjectID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<EPActivityApprove2.projectID>, object, object>) e).NewValue = (object) ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.ProjectID;
  }

  public virtual void _(
    PX.Data.Events.FieldDefaulting<EPActivityApprove2.projectTaskID> e)
  {
    if (!((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.ProjectTaskID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<EPActivityApprove2.projectTaskID>, object, object>) e).NewValue = (object) ((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.ProjectTaskID;
  }

  public virtual void _(PX.Data.Events.FieldDefaulting<EPActivityApprove2.date> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<EPActivityApprove2.date>, object, object>) e).NewValue = (object) this.GetWeekDateFromDay(((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.Day);
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<EPActivityApprove2, EPActivityApprove2.date> e)
  {
    EPActivityApprove2 row = e.Row;
    if (PXWeekSelector2Attribute.GetWeekInfo((PXGraph) this, ((PXSelectBase<EPWeeklyCrewTimeActivity>) this.Document).Current.Week.Value).IsValid((DateTime) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<EPActivityApprove2, EPActivityApprove2.date>, EPActivityApprove2, object>) e).NewValue))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<EPActivityApprove2, EPActivityApprove2.date>, EPActivityApprove2, object>) e).NewValue = (object) null;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<EPActivityApprove2, EPActivityApprove2.date>>) e).Cache.RaiseExceptionHandling<EPActivityApprove2.date>((object) row, (object) row.Date, (Exception) new PXSetPropertyException("The specified date is outside the selected week.", (PXErrorLevel) 4));
  }

  public virtual void _(PX.Data.Events.FieldSelecting<EPActivityApprove.hold> e)
  {
    EPActivityApprove row = (EPActivityApprove) e.Row;
    if (row == null)
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<EPActivityApprove.hold>>) e).ReturnValue = (object) (row.ApprovalStatus == "OP");
  }

  public virtual void _(
    PX.Data.Events.FieldDefaulting<EPWeeklyCrewTimeActivity.week> e)
  {
    int? nullable;
    int num;
    if (!(e.Row is EPWeeklyCrewTimeActivity row))
    {
      num = 1;
    }
    else
    {
      nullable = row.WorkgroupID;
      num = !nullable.HasValue ? 1 : 0;
    }
    if (num != 0)
      return;
    EPWeeklyCrewTimeActivity topFirst = PXSelectBase<EPWeeklyCrewTimeActivity, PXViewOf<EPWeeklyCrewTimeActivity>.BasedOn<SelectFromBase<EPWeeklyCrewTimeActivity, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<EPWeeklyCrewTimeActivity.workgroupID, IBqlInt>.IsEqual<P.AsInt>>.Aggregate<To<Max<EPWeeklyCrewTimeActivity.week>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.WorkgroupID
    }).TopFirst;
    if (topFirst != null)
    {
      nullable = topFirst.Week;
      if (nullable.HasValue)
      {
        PX.Data.Events.FieldDefaulting<EPWeeklyCrewTimeActivity.week> fieldDefaulting = e;
        nullable = topFirst.Week;
        // ISSUE: variable of a boxed type
        __Boxed<int> nextWeekId = (ValueType) PXWeekSelector2Attribute.GetNextWeekID((PXGraph) this, nullable.Value);
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<EPWeeklyCrewTimeActivity.week>, object, object>) fieldDefaulting).NewValue = (object) nextWeekId;
        return;
      }
    }
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<EPWeeklyCrewTimeActivity.week>, object, object>) e).NewValue = (object) PXWeekSelector2Attribute.GetWeekID((PXGraph) this, ((PXGraph) this).Accessinfo.BusinessDate.Value);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPTimeActivitiesSummary, EPTimeActivitiesSummary.contactID> e)
  {
    EPTimeActivitiesSummary row = e.Row;
    if (row == null || !row.ContactID.HasValue)
      return;
    BAccount baccount = PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXSelect<BAccount>.Config>.Search<BAccount.defContactID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPTimeActivitiesSummary, EPTimeActivitiesSummary.contactID>>) e).Cache.Graph, e.NewValue, Array.Empty<object>()));
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPTimeActivitiesSummary, EPTimeActivitiesSummary.contactID>>) e).Cache.SetValue<EPTimeActivitiesSummary.employeeStatus>((object) row, (object) baccount.VStatus);
    PXUIFieldAttribute.SetWarning<EPTimeActivitiesSummary.contactID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPTimeActivitiesSummary, EPTimeActivitiesSummary.contactID>>) e).Cache, (object) row, baccount.VStatus == "I" ? "This employee is inactive." : (string) null);
  }

  public virtual void _(PX.Data.Events.RowSelected<EPTimeActivitiesSummary> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetWarning<EPTimeActivitiesSummary.contactID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPTimeActivitiesSummary>>) e).Cache, (object) e.Row, e.Row.EmployeeStatus == "I" ? "This employee is inactive." : (string) null);
    PXUIFieldAttribute.SetWarning<EPTimeActivitiesSummary.totalRegularTime>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPTimeActivitiesSummary>>) e).Cache, (object) e.Row, e.Row.IsWithoutActivities.GetValueOrDefault() ? "At least one of the listed employees has no time activities reported for the selected period." : (string) null);
  }

  public virtual void _(PX.Data.Events.RowSelected<EPWeeklyCrewTimeActivity> e)
  {
    if (e.Row == null)
      return;
    bool flag = e.Row.WorkgroupID.HasValue && e.Row.Week.HasValue;
    ((PXAction) this.CompleteAllActivities).SetEnabled(flag);
    ((PXSelectBase) this.TimeActivities).AllowInsert = flag;
    ((PXSelectBase) this.TimeActivities).AllowUpdate = flag;
    ((PXAction) this.EnterBulkTime).SetEnabled(flag);
    ((PXAction) this.LoadLastWeekActivities).SetEnabled(flag);
    ((PXSelectBase) this.WorkgroupTimeSummary).AllowInsert = flag;
    ((PXSelectBase) this.WorkgroupTimeSummary).AllowUpdate = flag;
    ((PXAction) this.LoadLastWeekMembers).SetEnabled(flag);
  }

  public virtual void _(
    PX.Data.Events.FieldDefaulting<EPActivityApprove, EPActivityApprove.date> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<EPActivityApprove, EPActivityApprove.date>, EPActivityApprove, object>) e).NewValue = (object) this.GetWeekDateFromDay(((PXSelectBase<EPWeeklyCrewTimeActivityFilter>) this.Filter).Current.Day);
  }

  public virtual void _(
    PX.Data.Events.FieldUpdated<PMTimeActivity.earningTypeID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTimeActivity.earningTypeID>>) e).Cache.SetDefaultExt<PMTimeActivity.labourItemID>(e.Row);
  }

  public virtual void _(PX.Data.Events.FieldUpdated<EPActivityApprove.date> e)
  {
    EPActivityApprove row = (EPActivityApprove) e.Row;
    if (row == null)
      return;
    this.UpdateReportedInTimeZoneIDIfNeeded(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove.date>>) e).Cache, row, (DateTime?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<EPActivityApprove.date>, object, object>) e).OldValue, row.Date);
  }

  public virtual void _(PX.Data.Events.FieldUpdated<EPActivityApprove.projectID> e)
  {
    if (((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove.projectID>>) e).Cache.GetStatus(e.Row) == 2 || ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove.projectID>>) e).Cache.GetStatus(e.Row) == 1)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove.projectID>>) e).Cache.SetDefaultExt<PMTimeActivity.certifiedJob>(e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove.projectID>>) e).Cache.SetDefaultExt<PMTimeActivity.labourItemID>(e.Row);
  }

  public virtual void _(
    PX.Data.Events.FieldUpdated<EPActivityApprove2.projectID> e)
  {
    if (((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove2.projectID>>) e).Cache.GetStatus(e.Row) != 2 && ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove2.projectID>>) e).Cache.GetStatus(e.Row) != 1)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove2.projectID>>) e).Cache.SetDefaultExt<PMTimeActivity.certifiedJob>(e.Row);
  }

  public virtual void _(PX.Data.Events.FieldUpdated<EPActivityApprove.ownerID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove.ownerID>>) e).Cache.Current = e.Row;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove.ownerID>>) e).Cache.SetDefaultExt<PMTimeActivity.labourItemID>(e.Row);
  }

  public void _(PX.Data.Events.RowPersisting<EPActivityApprove2> e) => e.Cancel = true;

  private void UpdateReportedInTimeZoneIDIfNeeded(
    PXCache cache,
    EPActivityApprove row,
    DateTime? oldValue,
    DateTime? newValue)
  {
    DateTime? nullable1 = oldValue;
    DateTime? nullable2 = newValue;
    if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0 && newValue.HasValue)
      return;
    string id = LocaleInfo.GetTimeZone()?.Id;
    cache.SetValueExt<PMTimeActivity.reportedInTimeZoneID>((object) row, (object) id);
  }

  private EPActivityApprove CopyActivity(EPActivityApprove activity)
  {
    EPActivityApprove copy = ((PXSelectBase) this.TimeActivities).Cache.CreateCopy((object) activity) as EPActivityApprove;
    copy.NoteID = new Guid?();
    return copy;
  }

  private int GetLastWeekID()
  {
    return PXWeekSelector2Attribute.GetWeekID((PXGraph) this, PXWeekSelector2Attribute.GetWeekStartDate((PXGraph) this, ((PXSelectBase<EPWeeklyCrewTimeActivity>) this.Document).Current.Week.Value).AddDays(-1.0));
  }

  public virtual DateTime? GetWeekDateFromDay(int? dayOfWeek)
  {
    return !dayOfWeek.HasValue ? new DateTime?() : PRWeekDaySelector.GetWeekDates((PXGraph) this, ((PXSelectBase<EPWeeklyCrewTimeActivity>) this.Document).Current.Week.Value).SingleOrDefault<PRWeekDaySelector.WeekDate>((Func<PRWeekDaySelector.WeekDate, bool>) (x =>
    {
      int dayOfWeek1 = (int) x.Date.Value.DayOfWeek;
      int? nullable = dayOfWeek;
      int valueOrDefault = nullable.GetValueOrDefault();
      return dayOfWeek1 == valueOrDefault & nullable.HasValue;
    })).Date;
  }

  public virtual void RestoreCopy(EPActivityApprove copy, EPActivityApprove original)
  {
    Guid? noteId = copy.NoteID;
    Guid? refNoteId = copy.RefNoteID;
    ((PXSelectBase) this.TimeActivities).Cache.RestoreCopy((object) copy, (object) original);
    copy.NoteID = noteId;
    copy.RefNoteID = refNoteId;
    copy.ApprovalStatus = "OP";
    copy.Released = new bool?(false);
  }

  private object[] GetDateParam()
  {
    PXTimeZoneInfo timeZone = LocaleInfo.GetTimeZone();
    DateTime weekStartDate = PXWeekSelector2Attribute.GetWeekStartDate((PXGraph) this, ((PXSelectBase<EPWeeklyCrewTimeActivity>) this.Document).Current.Week.Value);
    DateTime weekEndDateTime = PXWeekSelector2Attribute.GetWeekEndDateTime((PXGraph) this, ((PXSelectBase<EPWeeklyCrewTimeActivity>) this.Document).Current.Week.Value);
    return new object[2]
    {
      (object) PXTimeZoneInfo.ConvertTimeToUtc(weekStartDate, timeZone),
      (object) PXTimeZoneInfo.ConvertTimeToUtc(weekEndDateTime, timeZone)
    };
  }

  public virtual void InitCacheMapping(Dictionary<System.Type, System.Type> map)
  {
    ((PXGraph) this).InitCacheMapping(map);
    ((PXGraph) this).Caches.AddCacheMapping(typeof (BAccount), typeof (BAccount));
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (viewName == "TimeActivities" && values[(object) "OwnerID"] != null)
    {
      BAccount topFirst = PXSelectBase<BAccount, PXViewOf<BAccount>.BasedOn<SelectFromBase<BAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccount.acctCD, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) values[(object) "OwnerID"].ToString()
      }).TopFirst;
      values[(object) "OwnerID"] = (object) topFirst.DefContactID;
    }
    return true;
  }

  public virtual bool RowImporting(string viewName, object row) => true;

  public virtual bool RowImported(string viewName, object row, object oldRow) => true;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2022 R2.")]
  public virtual void _(PX.Data.Events.RowInserted<EPWeeklyCrewTimeActivity> e)
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public virtual void _(PX.Data.Events.RowInserting<EPActivityApprove> e)
  {
  }
}
