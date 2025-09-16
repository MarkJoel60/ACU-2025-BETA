// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.SalesTerritoryMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CS;

public class SalesTerritoryMaint : PXGraph<
#nullable disable
SalesTerritoryMaint, PX.Objects.CS.SalesTerritory>, ICaptionable
{
  public PXSelect<PX.Objects.CS.SalesTerritory> SalesTerritory;
  [PXHidden]
  public FbqlSelect<SelectFromBase<Country, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<BqlField<
  #nullable enable
  Country.selected, IBqlBool>.Desc, 
  #nullable disable
  BqlField<
  #nullable enable
  Country.countryID, IBqlString>.Asc>>, 
  #nullable disable
  Country>.View Countries;
  [PXHidden]
  public FbqlSelect<SelectFromBase<State, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  State.countryID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.CS.SalesTerritory.countryID, IBqlString>.FromCurrent>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  State.selected, IBqlBool>.Desc, 
  #nullable disable
  BqlField<
  #nullable enable
  State.countryID, IBqlString>.Asc>>, 
  #nullable disable
  State>.View CountryStates;
  [PXHidden]
  public FbqlSelect<SelectFromBase<State, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  int0, IBqlInt>.IsEqual<
  #nullable disable
  int1>>, State>.View EmptyView;

  public string Caption()
  {
    PX.Objects.CS.SalesTerritory current = ((PXSelectBase<PX.Objects.CS.SalesTerritory>) this.SalesTerritory).Current;
    return current == null || current.SalesTerritoryID == null || current.Name == null ? "" : $"{current.SalesTerritoryID} - {current.Name}";
  }

  [PXMergeAttributes]
  [PXFormula(typeof (IIf2<Where<Country.salesTerritoryID, IsNotNull, And<Country.salesTerritoryID, Equal<Current<PX.Objects.CS.SalesTerritory.salesTerritoryID>>>>, True, False>))]
  public virtual void _(Events.CacheAttached<Country.selected> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (IIf2<Where<State.salesTerritoryID, IsNotNull, And<State.salesTerritoryID, Equal<Current<PX.Objects.CS.SalesTerritory.salesTerritoryID>>>>, True, False>))]
  public virtual void _(Events.CacheAttached<State.selected> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Sales Territory ID")]
  public virtual void _(
    Events.CacheAttached<PX.Objects.CS.SalesTerritory.salesTerritoryID> e)
  {
  }

  public virtual void _(Events.RowSelected<PX.Objects.CS.SalesTerritory> e)
  {
    if (e.Row == null)
      return;
    switch (e.Row.SalesTerritoryType)
    {
      case "S":
        ((PXSelectBase) this.Countries).AllowSelect = false;
        ((PXSelectBase) this.CountryStates).AllowSelect = ((PXSelectBase) this.CountryStates).AllowUpdate = true;
        ((PXSelectBase) this.CountryStates).AllowDelete = ((PXSelectBase) this.CountryStates).AllowInsert = false;
        ((PXSelectBase) this.EmptyView).AllowSelect = false;
        break;
      case "C":
        ((PXSelectBase) this.Countries).AllowSelect = ((PXSelectBase) this.Countries).AllowUpdate = true;
        ((PXSelectBase) this.Countries).AllowDelete = ((PXSelectBase) this.Countries).AllowInsert = false;
        ((PXSelectBase) this.CountryStates).AllowSelect = false;
        ((PXSelectBase) this.EmptyView).AllowSelect = false;
        break;
      default:
        ((PXSelectBase) this.Countries).AllowSelect = false;
        ((PXSelectBase) this.CountryStates).AllowSelect = false;
        ((PXSelectBase) this.EmptyView).AllowSelect = true;
        ((PXSelectBase) this.EmptyView).AllowInsert = ((PXSelectBase) this.EmptyView).AllowDelete = ((PXSelectBase) this.EmptyView).AllowUpdate = false;
        break;
    }
  }

  public virtual void _(
    Events.FieldUpdated<PX.Objects.CS.SalesTerritory.salesTerritoryType> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.CS.SalesTerritory row = e.Row as PX.Objects.CS.SalesTerritory;
    if ("S".Equals(((Events.FieldUpdatedBase<Events.FieldUpdated<PX.Objects.CS.SalesTerritory.salesTerritoryType>, object, object>) e).OldValue))
    {
      foreach (PXResult<State> pxResult in PXSelectBase<State, PXViewOf<State>.BasedOn<SelectFromBase<State, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<State.salesTerritoryID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.SalesTerritoryID
      }))
      {
        State state = PXResult<State>.op_Implicit(pxResult);
        state.Selected = new bool?(false);
        ((PXGraph) this).Caches[typeof (State)].Update((object) state);
      }
    }
    else if ("C".Equals(((Events.FieldUpdatedBase<Events.FieldUpdated<PX.Objects.CS.SalesTerritory.salesTerritoryType>, object, object>) e).OldValue))
    {
      foreach (PXResult<Country> pxResult in PXSelectBase<Country, PXViewOf<Country>.BasedOn<SelectFromBase<Country, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Country.salesTerritoryID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.SalesTerritoryID
      }))
      {
        Country country = PXResult<Country>.op_Implicit(pxResult);
        country.Selected = new bool?(false);
        ((PXGraph) this).Caches[typeof (Country)].Update((object) country);
      }
    }
    row.CountryID = (string) null;
  }

  public virtual void _(
    Events.FieldUpdated<PX.Objects.CS.SalesTerritory, PX.Objects.CS.SalesTerritory.countryID> e)
  {
    if (e.Row == null || ((Events.FieldUpdatedBase<Events.FieldUpdated<PX.Objects.CS.SalesTerritory, PX.Objects.CS.SalesTerritory.countryID>, PX.Objects.CS.SalesTerritory, object>) e).OldValue == null)
      return;
    foreach (PXResult<State> pxResult in PXSelectBase<State, PXViewOf<State>.BasedOn<SelectFromBase<State, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<State.salesTerritoryID, Equal<P.AsString>>>>>.And<BqlOperand<State.countryID, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) e.Row.SalesTerritoryID,
      ((Events.FieldUpdatedBase<Events.FieldUpdated<PX.Objects.CS.SalesTerritory, PX.Objects.CS.SalesTerritory.countryID>, PX.Objects.CS.SalesTerritory, object>) e).OldValue
    }))
    {
      State state = PXResult<State>.op_Implicit(pxResult);
      state.Selected = new bool?(false);
      ((PXGraph) this).Caches[typeof (State)].Update((object) state);
    }
  }

  protected virtual void _(Events.RowSelected<Country> e)
  {
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.AdjustUI(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<Country>>) e).Cache, (object) e.Row);
    attributeAdjuster = attributeAdjuster.ForAllFields((Action<PXUIFieldAttribute>) (ui => ui.Enabled = false));
    attributeAdjuster.For<Country.selected>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = true));
  }

  public virtual void _(Events.FieldUpdated<Country, Country.selected> e)
  {
    if (e.Row == null)
      return;
    e.Row.SalesTerritoryID = true.Equals(e.NewValue) ? ((PXSelectBase<PX.Objects.CS.SalesTerritory>) this.SalesTerritory).Current?.SalesTerritoryID : (string) null;
  }

  protected virtual void _(Events.RowSelected<State> e)
  {
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.AdjustUI(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<State>>) e).Cache, (object) e.Row);
    attributeAdjuster = attributeAdjuster.ForAllFields((Action<PXUIFieldAttribute>) (ui => ui.Enabled = false));
    attributeAdjuster.For<State.selected>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = true));
  }

  public virtual void _(Events.FieldUpdated<State, State.selected> e)
  {
    if (e.Row == null)
      return;
    e.Row.SalesTerritoryID = true.Equals(e.NewValue) ? ((PXSelectBase<PX.Objects.CS.SalesTerritory>) this.SalesTerritory).Current?.SalesTerritoryID : (string) null;
  }

  public virtual void Persist()
  {
    if ((((PXGraph) this).Caches[typeof (State)].IsInsertedUpdatedDeleted || ((PXGraph) this).Caches[typeof (Country)].IsInsertedUpdatedDeleted) && this.IsSalesTerritoryChnaged())
    {
      if (EnumerableExtensions.IsIn<WebDialogResult>(((PXSelectBase) this.SalesTerritory).View.Ask((object) ((PXSelectBase<PX.Objects.CS.SalesTerritory>) this.SalesTerritory).Current, "Confirmation", "Some of the selected records are assigned to other sales territories. Do you want to update the territories in these records?", (MessageButtons) 3, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
      {
        [(WebDialogResult) 6] = "Update",
        [(WebDialogResult) 7] = "Skip",
        [(WebDialogResult) 2] = "Cancel"
      }, (MessageIcon) 0), (WebDialogResult) 6, (WebDialogResult) 7))
      {
        if (((PXSelectBase) this.SalesTerritory).View.Answer == 7)
        {
          if (((PXSelectBase<PX.Objects.CS.SalesTerritory>) this.SalesTerritory).Current?.SalesTerritoryType == "S")
            EnumerableExtensions.ForEach<State>(((PXGraph) this).Caches[typeof (State)].Updated.OfType<State>().Where<State>((Func<State, bool>) (v => (((PXGraph) this).Caches[typeof (State)].GetOriginal((object) v) is State original1 ? original1.SalesTerritoryID : (string) null) != null && v.SalesTerritoryID == ((PXSelectBase<PX.Objects.CS.SalesTerritory>) this.SalesTerritory).Current?.SalesTerritoryID)), (Action<State>) (v => ((PXGraph) this).Caches[typeof (State)].Remove((object) v)));
          else if (((PXSelectBase<PX.Objects.CS.SalesTerritory>) this.SalesTerritory).Current?.SalesTerritoryType == "C")
            EnumerableExtensions.ForEach<Country>(((PXGraph) this).Caches[typeof (Country)].Updated.OfType<Country>().Where<Country>((Func<Country, bool>) (v => (((PXGraph) this).Caches[typeof (Country)].GetOriginal((object) v) is Country original2 ? original2.SalesTerritoryID : (string) null) != null && v.SalesTerritoryID == ((PXSelectBase<PX.Objects.CS.SalesTerritory>) this.SalesTerritory).Current?.SalesTerritoryID)), (Action<Country>) (v => ((PXGraph) this).Caches[typeof (Country)].Remove((object) v)));
        }
      }
      else
      {
        ((PXSelectBase) this.SalesTerritory).View.Answer = (WebDialogResult) 0;
        return;
      }
    }
    ((PXGraph) this).Persist();
  }

  protected virtual bool IsSalesTerritoryChnaged()
  {
    return ((PXGraph) this).Caches[typeof (State)].Updated.OfType<State>().Any<State>((Func<State, bool>) (v => v != null && v.SalesTerritoryID != null && (((PXGraph) this).Caches[typeof (State)].GetOriginal((object) v) is State original1 ? original1.SalesTerritoryID : (string) null) != null)) || ((PXGraph) this).Caches[typeof (Country)].Updated.OfType<Country>().Any<Country>((Func<Country, bool>) (v => v != null && v.SalesTerritoryID != null && (((PXGraph) this).Caches[typeof (Country)].GetOriginal((object) v) is Country original2 ? original2.SalesTerritoryID : (string) null) != null));
  }
}
