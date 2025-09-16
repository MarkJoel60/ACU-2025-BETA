// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Turnover.ManageTurnover
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN.Turnover;

public class ManageTurnover : PXGraph<
#nullable disable
ManageTurnover>
{
  public PXAction<INTurnoverCalcFilter> refresh;
  public PXCancel<INTurnoverCalcFilter> Cancel;
  public PXFilter<INTurnoverCalcFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<INTurnoverCalc, INTurnoverCalcFilter, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INTurnoverCalc.branchID, 
  #nullable disable
  Inside<BqlField<
  #nullable enable
  INTurnoverCalcFilter.orgBAccountID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  INTurnoverCalcFilter.fromPeriodID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  INTurnoverCalc.fromPeriodID, IBqlString>.IsGreaterEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INTurnoverCalcFilter.fromPeriodID, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  INTurnoverCalcFilter.toPeriodID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  INTurnoverCalc.toPeriodID, IBqlString>.IsLessEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INTurnoverCalcFilter.toPeriodID, IBqlString>.FromCurrent>>>>> TurnoverCalcs;
  public 
  #nullable disable
  PXSetup<INSetup> insetup;

  [PXUIField]
  [PXButton(ImageKey = "Refresh")]
  public virtual IEnumerable Refresh(PXAdapter adapter)
  {
    ((PXSelectBase) this.TurnoverCalcs).Cache.Clear();
    ((PXSelectBase) this.TurnoverCalcs).Cache.ClearQueryCache();
    return adapter.Get();
  }

  public ManageTurnover()
  {
    INSetup current = ((PXSelectBase<INSetup>) this.insetup).Current;
  }

  protected virtual IEnumerable turnoverCalcs()
  {
    INTurnoverCalcFilter current = ((PXSelectBase<INTurnoverCalcFilter>) this.Filter).Current;
    if (!current.OrgBAccountID.HasValue)
      return (IEnumerable) Array<INTurnoverCalc>.Empty;
    switch (current.Action)
    {
      case "DEL":
        return (IEnumerable) null;
      case "CALC":
        return (IEnumerable) this.calculateTurnoverCalcs(current);
      default:
        return (IEnumerable) Array<INTurnoverCalc>.Empty;
    }
  }

  protected virtual IEnumerable<INTurnoverCalc> calculateTurnoverCalcs(INTurnoverCalcFilter filter)
  {
    if (filter.FromPeriodID == null && filter.NumberOfPeriods.GetValueOrDefault() <= 0 || filter.ToPeriodID == null || filter.CalculateBy == null || filter.CalculateBy == "NONE")
      return (IEnumerable<INTurnoverCalc>) Array<INTurnoverCalc>.Empty;
    PXCache cache = ((PXSelectBase) this.TurnoverCalcs).Cache;
    List<INTurnoverCalc> list1 = GraphHelper.RowCast<INTurnoverCalc>(cache.Inserted).ToList<INTurnoverCalc>();
    if (list1.Any<INTurnoverCalc>())
      return (IEnumerable<INTurnoverCalc>) list1;
    List<INTurnoverCalc> list2 = ((IEnumerable<INTurnoverCalc>) this.CreateTurnoverCalcRows(filter)).ToList<INTurnoverCalc>();
    foreach (INTurnoverCalc inTurnoverCalc in list2)
    {
      cache.SetDefaultExt<INTurnoverCalc.noteID>((object) inTurnoverCalc);
      cache.SetStatus((object) inTurnoverCalc, (PXEntryStatus) 2);
    }
    return (IEnumerable<INTurnoverCalc>) list2;
  }

  protected virtual void _(PX.Data.Events.RowSelected<INTurnoverCalcFilter> e)
  {
    INTurnoverCalcFilter row = e.Row;
    if (row == null)
      return;
    if (PXContext.GetSlot<AUSchedule>() == null)
      this.SetFieldsEnabled(row);
    (bool Calculate, bool Delete) actionsAvailability = this.GetActionsAvailability(row);
    ((PXProcessing<INTurnoverCalc>) this.TurnoverCalcs).SetProcessEnabled(actionsAvailability.Calculate || actionsAvailability.Delete);
    ((PXProcessing<INTurnoverCalc>) this.TurnoverCalcs).SetProcessAllEnabled(actionsAvailability.Calculate || actionsAvailability.Delete);
    if (actionsAvailability.Calculate)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((PXProcessingBase<INTurnoverCalc>) this.TurnoverCalcs).SetProcessDelegate<TurnoverCalcMaint>(ManageTurnover.\u003C\u003Ec.\u003C\u003E9__9_0 ?? (ManageTurnover.\u003C\u003Ec.\u003C\u003E9__9_0 = new PXProcessingBase<INTurnoverCalc>.ProcessItemDelegate<TurnoverCalcMaint>((object) ManageTurnover.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C_\u003Eb__9_0))));
    }
    else
    {
      if (!actionsAvailability.Delete)
        return;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((PXProcessingBase<INTurnoverCalc>) this.TurnoverCalcs).SetProcessDelegate<TurnoverCalcMaint>(ManageTurnover.\u003C\u003Ec.\u003C\u003E9__9_1 ?? (ManageTurnover.\u003C\u003Ec.\u003C\u003E9__9_1 = new PXProcessingBase<INTurnoverCalc>.ProcessItemDelegate<TurnoverCalcMaint>((object) ManageTurnover.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C_\u003Eb__9_1))));
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<INTurnoverCalcFilter> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INTurnoverCalcFilter>>) e).Cache.ObjectsEqual<INTurnoverCalcFilter.action, INTurnoverCalcFilter.orgBAccountID, INTurnoverCalcFilter.fromPeriodID, INTurnoverCalcFilter.toPeriodID, INTurnoverCalcFilter.calculateBy>((object) e.OldRow, (object) e.Row))
      return;
    ((PXSelectBase) this.TurnoverCalcs).Cache.Clear();
    ((PXSelectBase) this.TurnoverCalcs).Cache.ClearQueryCache();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INTurnoverCalcFilter, INTurnoverCalcFilter.action> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTurnoverCalcFilter, INTurnoverCalcFilter.action>>) e).Cache.SetDefaultExt<INTurnoverCalcFilter.calculateBy>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INTurnoverCalcFilter, INTurnoverCalcFilter.fromPeriodID> e)
  {
    if (e.Row.ToPeriodID == null || string.CompareOrdinal(e.Row.FromPeriodID, e.Row.ToPeriodID) <= 0)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTurnoverCalcFilter, INTurnoverCalcFilter.fromPeriodID>>) e).Cache.SetValue<INTurnoverCalcFilter.toPeriodID>((object) e.Row, (object) e.Row.FromPeriodID);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INTurnoverCalcFilter, INTurnoverCalcFilter.toPeriodID> e)
  {
    if (e.Row.FromPeriodID == null || string.CompareOrdinal(e.Row.FromPeriodID, e.Row.ToPeriodID) <= 0)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTurnoverCalcFilter, INTurnoverCalcFilter.toPeriodID>>) e).Cache.SetValue<INTurnoverCalcFilter.fromPeriodID>((object) e.Row, (object) e.Row.ToPeriodID);
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<INTurnoverCalc, INTurnoverCalc.inventoryID> e)
  {
    INTurnoverCalc row = e.Row;
    if ((row != null ? (row.IsInventoryListCalc.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<INTurnoverCalc, INTurnoverCalc.inventoryID>>) e).ReturnValue = (object) PXMessages.LocalizeNoPrefix("<LIST>");
  }

  protected virtual INTurnoverCalc[] CreateTurnoverCalcRows(INTurnoverCalcFilter filter)
  {
    List<INTurnoverCalc> inTurnoverCalcList = new List<INTurnoverCalc>();
    List<INTurnoverCalc> source1 = new List<INTurnoverCalc>();
    int[] branches = this.GetBranches(filter);
    MasterFinPeriod[] source2 = (MasterFinPeriod[]) null;
    string toPeriodId = filter.ToPeriodID;
    string fromPeriodID1 = filter.FromPeriodID;
    if (fromPeriodID1 == null)
    {
      source2 = GraphHelper.RowCast<MasterFinPeriod>((IEnumerable) ((PXSelectBase<MasterFinPeriod>) new PXViewOf<MasterFinPeriod>.BasedOn<SelectFromBase<MasterFinPeriod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<MasterFinPeriod.finPeriodID, IBqlString>.IsLessEqual<P.AsString.ASCII>>.Order<By<Desc<MasterFinPeriod.finPeriodID>>>>.ReadOnly((PXGraph) this)).SelectWindowed(0, filter.NumberOfPeriods ?? 1, new object[1]
      {
        (object) toPeriodId
      })).OrderBy<MasterFinPeriod, string>((Func<MasterFinPeriod, string>) (x => x.FinPeriodID)).ToArray<MasterFinPeriod>();
      fromPeriodID1 = ((IEnumerable<MasterFinPeriod>) source2).First<MasterFinPeriod>().FinPeriodID;
    }
    PXViewOf<INTurnoverCalc>.BasedOn<SelectFromBase<INTurnoverCalc, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTurnoverCalc.branchID, In<P.AsInt>>>>, And<BqlOperand<INTurnoverCalc.isFullCalc, IBqlBool>.IsEqual<True>>>, And<BqlOperand<INTurnoverCalc.fromPeriodID, IBqlString>.IsEqual<P.AsString.ASCII>>>>.And<BqlOperand<INTurnoverCalc.toPeriodID, IBqlString>.IsEqual<P.AsString.ASCII>>>>.ReadOnly readOnly = new PXViewOf<INTurnoverCalc>.BasedOn<SelectFromBase<INTurnoverCalc, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTurnoverCalc.branchID, In<P.AsInt>>>>, And<BqlOperand<INTurnoverCalc.isFullCalc, IBqlBool>.IsEqual<True>>>, And<BqlOperand<INTurnoverCalc.fromPeriodID, IBqlString>.IsEqual<P.AsString.ASCII>>>>.And<BqlOperand<INTurnoverCalc.toPeriodID, IBqlString>.IsEqual<P.AsString.ASCII>>>>.ReadOnly((PXGraph) this);
    switch (filter.CalculateBy)
    {
      case "RANGE":
        inTurnoverCalcList.AddRange(createCalcs(fromPeriodID1, toPeriodId));
        source1.AddRange((IEnumerable<INTurnoverCalc>) ((PXSelectBase<INTurnoverCalc>) readOnly).SelectMain(new object[3]
        {
          (object) branches,
          (object) fromPeriodID1,
          (object) toPeriodId
        }));
        break;
      case "PERIOD":
        if (source2 == null)
          source2 = ((PXSelectBase<MasterFinPeriod>) new PXViewOf<MasterFinPeriod>.BasedOn<SelectFromBase<MasterFinPeriod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<MasterFinPeriod.finPeriodID, GreaterEqual<P.AsString.ASCII>>>>>.And<BqlOperand<MasterFinPeriod.finPeriodID, IBqlString>.IsLessEqual<P.AsString.ASCII>>>>.ReadOnly((PXGraph) this)).SelectMain(new object[2]
          {
            (object) fromPeriodID1,
            (object) toPeriodId
          });
        foreach (MasterFinPeriod masterFinPeriod in source2)
          inTurnoverCalcList.AddRange(createCalcs(masterFinPeriod.FinPeriodID, masterFinPeriod.FinPeriodID));
        source1.AddRange((IEnumerable<INTurnoverCalc>) ((PXSelectBase<INTurnoverCalc>) new PXViewOf<INTurnoverCalc>.BasedOn<SelectFromBase<INTurnoverCalc, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTurnoverCalc.branchID, In<P.AsInt>>>>, And<BqlOperand<INTurnoverCalc.isFullCalc, IBqlBool>.IsEqual<True>>>, And<BqlOperand<INTurnoverCalc.fromPeriodID, IBqlString>.IsEqual<INTurnoverCalc.toPeriodID>>>, And<BqlOperand<INTurnoverCalc.fromPeriodID, IBqlString>.IsGreaterEqual<P.AsString.ASCII>>>>.And<BqlOperand<INTurnoverCalc.fromPeriodID, IBqlString>.IsLessEqual<P.AsString.ASCII>>>>.ReadOnly((PXGraph) this)).SelectMain(new object[3]
        {
          (object) branches,
          (object) fromPeriodID1,
          (object) toPeriodId
        }));
        break;
      case "YEAR":
        MasterFinPeriod[] masterFinPeriodArray = ((PXSelectBase<MasterFinPeriod>) new PXViewOf<MasterFinPeriod>.BasedOn<SelectFromBase<MasterFinPeriod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<MasterFinPeriod.finPeriodID, GreaterEqual<P.AsString.ASCII>>>>>.And<BqlOperand<MasterFinPeriod.finPeriodID, IBqlString>.IsLessEqual<P.AsString.ASCII>>>.Aggregate<To<GroupBy<MasterFinPeriod.finYear>, Max<MasterFinPeriod.finPeriodID>>>.Order<By<Asc<MasterFinPeriod.finYear>>>>.ReadOnly((PXGraph) this)).SelectMain(new object[2]
        {
          (object) fromPeriodID1,
          (object) toPeriodId
        });
        string str1 = fromPeriodID1;
        foreach (MasterFinPeriod masterFinPeriod in masterFinPeriodArray)
        {
          string str2 = str1;
          if (str2 == null)
            str2 = FinPeriodUtils.GetFirstFinPeriodIDOfYear((IYear) new MasterFinYear()
            {
              Year = masterFinPeriod.FinYear
            });
          string fromPeriodID2 = str2;
          inTurnoverCalcList.AddRange(createCalcs(fromPeriodID2, masterFinPeriod.FinPeriodID));
          source1.AddRange((IEnumerable<INTurnoverCalc>) ((PXSelectBase<INTurnoverCalc>) readOnly).SelectMain(new object[3]
          {
            (object) branches,
            (object) fromPeriodID2,
            (object) masterFinPeriod.FinPeriodID
          }));
          str1 = (string) null;
        }
        break;
    }
    Dictionary<(int?, string, string), INTurnoverCalc> dictionary = source1.ToDictionary<INTurnoverCalc, (int?, string, string)>((Func<INTurnoverCalc, (int?, string, string)>) (x => (x.BranchID, x.FromPeriodID, x.ToPeriodID)));
    foreach (INTurnoverCalc inTurnoverCalc1 in inTurnoverCalcList)
    {
      INTurnoverCalc inTurnoverCalc2;
      if (dictionary.TryGetValue((inTurnoverCalc1.BranchID, inTurnoverCalc1.FromPeriodID, inTurnoverCalc1.ToPeriodID), out inTurnoverCalc2))
        inTurnoverCalc1.CreatedDateTime = inTurnoverCalc2.CreatedDateTime;
    }
    return inTurnoverCalcList.ToArray();

    IEnumerable<INTurnoverCalc> createCalcs(string fromPeriodID, string toPeriodID)
    {
      int[] numArray = branches;
      for (int index = 0; index < numArray.Length; ++index)
        yield return this.CreateTurnoverCalc(filter, new int?(numArray[index]), fromPeriodID, toPeriodID);
      numArray = (int[]) null;
    }
  }

  protected virtual int[] GetBranches(INTurnoverCalcFilter filter)
  {
    if (!filter.BranchID.HasValue)
      return PXAccess.GetChildBranchIDs(filter.OrganizationID.HasValue ? filter.OrganizationID : ((PXAccess.Organization) PXAccess.GetOrganizationByBAccountID(filter.OrgBAccountID))?.OrganizationID, true);
    return new int[1]{ filter.BranchID.Value };
  }

  protected virtual INTurnoverCalc CreateTurnoverCalc(
    INTurnoverCalcFilter filter,
    int? branchID,
    string fromPeriod = null,
    string toPeriod = null)
  {
    return new INTurnoverCalc()
    {
      BranchID = branchID,
      FromPeriodID = fromPeriod ?? filter.FromPeriodID,
      ToPeriodID = toPeriod ?? filter.ToPeriodID
    };
  }

  protected (bool Calculate, bool Delete) GetActionsAvailability(INTurnoverCalcFilter filter)
  {
    int num1;
    if (filter.Action == "CALC" && filter.OrgBAccountID.HasValue && !string.IsNullOrEmpty(filter.CalculateBy) && filter.CalculateBy != "NONE")
    {
      if (string.IsNullOrEmpty(filter.FromPeriodID))
      {
        int? numberOfPeriods = filter.NumberOfPeriods;
        int num2 = 0;
        if (!(numberOfPeriods.GetValueOrDefault() > num2 & numberOfPeriods.HasValue))
          goto label_4;
      }
      num1 = !string.IsNullOrEmpty(filter.ToPeriodID) ? 1 : 0;
      goto label_5;
    }
label_4:
    num1 = 0;
label_5:
    int num3 = !(filter.Action == "DEL") ? (false ? 1 : 0) : (filter.OrgBAccountID.HasValue ? 1 : 0);
    return (num1 != 0, num3 != 0);
  }

  protected virtual void SetFieldsEnabled(INTurnoverCalcFilter filter)
  {
    bool isCalcAction = filter.Action == "CALC";
    bool isDeleteAction = filter.Action == "DEL";
    PXUIFieldAttribute.SetEnabled<INTurnoverCalcFilter.orgBAccountID>(((PXSelectBase) this.Filter).Cache, (object) filter, isCalcAction | isDeleteAction);
    PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Filter).Cache, (object) filter).For<INTurnoverCalcFilter.fromPeriodID>((Action<PXUIFieldAttribute>) (fa =>
    {
      fa.Required = isCalcAction;
      fa.Enabled = isCalcAction | isDeleteAction;
    })).SameFor<INTurnoverCalcFilter.toPeriodID>();
    PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Filter).Cache, (object) filter).For<INTurnoverCalcFilter.calculateBy>((Action<PXUIFieldAttribute>) (fa =>
    {
      fa.Required = isCalcAction;
      fa.Enabled = isCalcAction;
    }));
  }
}
