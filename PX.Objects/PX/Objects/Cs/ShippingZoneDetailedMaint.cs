// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.ShippingZoneDetailedMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CS;

public class ShippingZoneDetailedMaint : PXGraph<
#nullable disable
ShippingZoneDetailedMaint, ShippingZone>
{
  [PXImport(typeof (ShippingZone))]
  public PXSelect<ShippingZone> ShippingZones;
  public FbqlSelect<SelectFromBase<ShippingZoneLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  ShippingZoneLine.zoneID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ShippingZone.zoneID, IBqlString>.FromCurrent>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  ShippingZoneLine.lineNbr, IBqlInt>.Asc>>, 
  #nullable disable
  ShippingZoneLine>.View ShippingZoneLines;
  [PXCopyPasteHiddenView]
  public PXFilter<PX.Objects.CS.CountryFilter> CountryFilter;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<State, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  State.countryID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.CS.CountryFilter.countryID, IBqlString>.FromCurrent>>, 
  #nullable disable
  State>.View CountryStatesList;
  public PXAction<ShippingZone> AddCountryStates;
  public PXAction<ShippingZone> AddState;

  public IEnumerable countryStatesList()
  {
    BqlCommand bqlCommand = ((PXSelectBase) this.CountryStatesList).View.BqlSelect;
    List<object> list = ((IEnumerable<object>) GraphHelper.RowCast<ShippingZoneLine>((IEnumerable) ((PXSelectBase<ShippingZoneLine>) this.ShippingZoneLines).Select(Array.Empty<object>())).Where<ShippingZoneLine>((Func<ShippingZoneLine, bool>) (x => x.CountryID != null && x.StateID != null)).Select<ShippingZoneLine, string>((Func<ShippingZoneLine, string>) (x => x.StateID))).ToList<object>();
    if (list.Any<object>())
      bqlCommand = bqlCommand.WhereAnd(NotInHelper<State.stateID>.Create(list.Count));
    return (IEnumerable) new PXView((PXGraph) this, false, bqlCommand).SelectMulti(list.ToArray());
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable addCountryStates(PXAdapter adapter)
  {
    // ISSUE: method pointer
    if (((PXSelectBase<PX.Objects.CS.CountryFilter>) this.CountryFilter).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CaddCountryStates\u003Eb__6_0))) == 1)
      this.addState(adapter);
    else
      ((PXAction) this.Cancel).Press();
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable addState(PXAdapter adapter)
  {
    ShippingZone current = ((PXSelectBase<ShippingZone>) this.ShippingZones).Current;
    if (current == null)
      return adapter.Get();
    List<ShippingZoneLine> list = GraphHelper.RowCast<ShippingZoneLine>((IEnumerable) ((PXSelectBase<ShippingZoneLine>) this.ShippingZoneLines).Select(Array.Empty<object>())).ToList<ShippingZoneLine>();
    foreach (State state1 in ((PXSelectBase) this.CountryStatesList).Cache.Updated)
    {
      State state = state1;
      if (state.Selected.GetValueOrDefault() && (list == null || !list.Any<ShippingZoneLine>((Func<ShippingZoneLine, bool>) (x => x.CountryID == state.CountryID && x.StateID == state.StateID))))
        ((PXSelectBase<ShippingZoneLine>) this.ShippingZoneLines).Insert(new ShippingZoneLine()
        {
          ZoneID = current?.ZoneID,
          CountryID = state.CountryID,
          StateID = state.StateID
        });
    }
    return adapter.Get();
  }

  public void _(Events.RowSelected<ShippingZone> e)
  {
    ShippingZone row = e.Row;
    if (row == null)
      return;
    ((PXAction) this.AddCountryStates).SetEnabled(!string.IsNullOrEmpty(row.ZoneID));
    ((PXSelectBase) this.CountryStatesList).AllowInsert = false;
    ((PXSelectBase) this.CountryStatesList).AllowDelete = false;
  }

  public void _(Events.RowInserting<ShippingZone> e)
  {
    if (e.Row == null)
      return;
    if (PXResultset<ShippingZone>.op_Implicit(PXSelectBase<ShippingZone, PXSelect<ShippingZone, Where<ShippingZone.zoneID, Equal<Required<ShippingZone.zoneID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) e.Row.ZoneID
    })) == null)
      return;
    ((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<ShippingZone>>) e).Cache.RaiseExceptionHandling<ShippingZone.zoneID>((object) e.Row, (object) e.Row.ZoneID, (Exception) new PXException("The record already exists."));
    e.Cancel = true;
  }

  public void _(Events.RowPersisting<ShippingZoneLine> e)
  {
    if (PXDBOperationExt.Command(e.Operation) == 3)
      return;
    ShippingZoneLine shippingZoneLine = PXResultset<ShippingZoneLine>.op_Implicit(PXSelectBase<ShippingZoneLine, PXViewOf<ShippingZoneLine>.BasedOn<SelectFromBase<ShippingZoneLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ShippingZoneLine.countryID, Equal<BqlField<ShippingZoneLine.countryID, IBqlString>.FromCurrent>>>>, And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ShippingZoneLine.stateID, IsNull>>>, Or<BqlOperand<Current<ShippingZoneLine.stateID>, IBqlString>.IsNull>>>.Or<BqlOperand<ShippingZoneLine.stateID, IBqlString>.IsEqual<BqlField<ShippingZoneLine.stateID, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ShippingZoneLine.zoneID, NotEqual<BqlField<ShippingZoneLine.zoneID, IBqlString>.FromCurrent>>>>>.Or<BqlOperand<ShippingZoneLine.lineNbr, IBqlInt>.IsNotEqual<BqlField<ShippingZoneLine.lineNbr, IBqlInt>.FromCurrent>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) new ShippingZoneLine[1]
    {
      e.Row
    }, Array.Empty<object>()));
    if (shippingZoneLine == null)
      return;
    if (e.Row.StateID == null && shippingZoneLine.StateID != null)
    {
      if (((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<ShippingZoneLine>>) e).Cache.RaiseExceptionHandling<ShippingZoneLine.stateID>((object) e.Row, (object) e.Row.StateID, (Exception) new PXSetPropertyException((IBqlTable) e.Row, "A line with the same country and a specific state has already been added to the {0} shipping zone. When adding another line for this country, you need to specify a different state.", (PXErrorLevel) 5, new object[1]
      {
        (object) shippingZoneLine.ZoneID
      })))
        throw new PXRowPersistingException("ShippingZoneLine", (object) e.Row, "A line with the same country and a specific state has already been added to the {0} shipping zone. When adding another line for this country, you need to specify a different state.", new object[1]
        {
          (object) shippingZoneLine.ZoneID
        });
    }
    else if (e.Row.StateID != null && shippingZoneLine.StateID == null)
    {
      if (((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<ShippingZoneLine>>) e).Cache.RaiseExceptionHandling<ShippingZoneLine.stateID>((object) e.Row, (object) e.Row.StateID, (Exception) new PXSetPropertyException((IBqlTable) e.Row, "Another line with the same country and no specific state has already been added to the {0} shipping zone. As a result, all states within that country are automatically included in the shipping zone.", (PXErrorLevel) 5, new object[1]
      {
        (object) shippingZoneLine.ZoneID
      })))
        throw new PXRowPersistingException("ShippingZoneLine", (object) e.Row, "Another line with the same country and no specific state has already been added to the {0} shipping zone. As a result, all states within that country are automatically included in the shipping zone.", new object[1]
        {
          (object) shippingZoneLine.ZoneID
        });
    }
    else if (((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<ShippingZoneLine>>) e).Cache.RaiseExceptionHandling<ShippingZoneLine.countryID>((object) e.Row, (object) e.Row.CountryID, (Exception) new PXSetPropertyException((IBqlTable) e.Row, "The selected combination of the country and state is already assigned to the {0} shipping zone.", (PXErrorLevel) 5, new object[1]
    {
      (object) shippingZoneLine.ZoneID
    })))
      throw new PXRowPersistingException("ShippingZoneLine", (object) e.Row, "The selected combination of the country and state is already assigned to the {0} shipping zone.", new object[1]
      {
        (object) shippingZoneLine.ZoneID
      });
  }

  public void _(Events.RowDeleting<ShippingZone> e)
  {
    if (e.Row == null)
      return;
    List<SOOrchestrationPlan> list = GraphHelper.RowCast<SOOrchestrationPlan>((IEnumerable) PXSelectBase<SOOrchestrationPlan, PXSelect<SOOrchestrationPlan, Where<SOOrchestrationPlan.shippingZoneID, Equal<Required<ShippingZone.zoneID>>, And<SOOrchestrationPlan.isActive, Equal<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.ZoneID
    })).ToList<SOOrchestrationPlan>();
    if (list == null)
      return;
    string str = string.Join(", ", list.Select<SOOrchestrationPlan, string>((Func<SOOrchestrationPlan, string>) (p => p.PlanID)));
    if (str != string.Empty)
      throw new PXSetPropertyException((IBqlTable) e.Row, "The {0} shipping zone cannot be deleted because it is specified in the following orchestration plans: {1}. Deactivate the orchestration plans first.", (PXErrorLevel) 4, new object[2]
      {
        (object) e.Row.ZoneID,
        (object) str
      });
  }
}
