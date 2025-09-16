// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.AddressLookupExtension`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace PX.Objects.CR.Extensions;

public abstract class AddressLookupExtension<TGraph, TMaster, TAddress> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TMaster : class, IBqlTable, new()
  where TAddress : class, IBqlTable, IAddressLocation, new()
{
  public PXFilter<PX.Objects.CR.Extensions.AddressLookupFilter> addresslookup_mapdialog;
  public string ActionName;
  public const string AddressLookupSelectActionName = "AddressLookupSelectAction";
  private const string _VIEWONMAP_ACTION = "ViewOnMap";
  public PXFilter<PX.Objects.CR.Extensions.AddressLookupFilter> AddressLookupFilter;

  public virtual void _(Events.FieldUpdating<TAddress, CRAddress.state> e)
  {
    if (((PXSelectBase) this.addresslookup_mapdialog).View.Answer != 1 || !(((Events.FieldUpdatingBase<Events.FieldUpdating<TAddress, CRAddress.state>>) e).NewValue is string newValue) || string.IsNullOrEmpty(newValue))
      return;
    PX.Objects.CS.State state = PXResultset<PX.Objects.CS.State>.op_Implicit(PXSelectBase<PX.Objects.CS.State, PXSelectReadonly<PX.Objects.CS.State, Where<PX.Objects.CS.State.name, Equal<Required<PX.Objects.CS.State.name>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
    {
      (object) newValue
    }));
    if (state == null)
      return;
    ((Events.FieldUpdatingBase<Events.FieldUpdating<TAddress, CRAddress.state>>) e).NewValue = (object) state.StateID;
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.ActionName = this.ActionName ?? ((object) this).GetType().Name;
    if (this.ActionName.StartsWith(typeof (TGraph).Name))
      this.ActionName = this.ActionName.Substring(typeof (TGraph).Name.Length);
    if (this.ActionName.EndsWith("Extension"))
      this.ActionName = this.ActionName.Substring(0, this.ActionName.Length - "Extension".Length);
    string actionName = this.ActionName;
    AddressLookupExtension<TGraph, TMaster, TAddress> addressLookupExtension1 = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate handler = new PXButtonDelegate((object) addressLookupExtension1, __vmethodptr(addressLookupExtension1, AddressLookup));
    this.AddAction(actionName, "Address Lookup", handler, false);
    AddressLookupExtension<TGraph, TMaster, TAddress> addressLookupExtension2 = this;
    // ISSUE: virtual method pointer
    this.AddAction("AddressLookupSelectAction", "Select", new PXButtonDelegate((object) addressLookupExtension2, __vmethodptr(addressLookupExtension2, AddressLookupSelectAction)), true);
  }

  protected void AddAction(
    string name,
    string displayName,
    PXButtonDelegate handler,
    bool visible,
    bool displayOnMainToolbar = false)
  {
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>()
    {
      (PXEventSubscriberAttribute) new PXUIFieldAttribute()
      {
        DisplayName = PXMessages.LocalizeNoPrefix(displayName),
        MapEnableRights = (PXCacheRights) 2,
        MapViewRights = (PXCacheRights) 1,
        Visible = visible,
        Enabled = true
      },
      (PXEventSubscriberAttribute) new PXButtonAttribute()
      {
        DisplayOnMainToolbar = displayOnMainToolbar
      }
    };
    this.Base.Actions[name] = (PXAction) new PXNamedAction<TMaster>((PXGraph) this.Base, name, handler, subscriberAttributeList.ToArray());
  }

  protected abstract string AddressView { get; }

  protected virtual string ViewOnMap => (string) null;

  protected virtual string ViewOnMapActionName => "ViewOnMap";

  protected virtual IAddressLocation CurrentAddress
  {
    get
    {
      object obj = this.Base.Views[this.AddressView].SelectSingle(Array.Empty<object>());
      return obj is PXResult pxResult ? (IAddressLocation) pxResult[0] : (IAddressLocation) obj;
    }
  }

  protected virtual IEnumerable AddressLookupSelectAction(PXAdapter adapter)
  {
    PX.Objects.CR.Extensions.AddressLookupFilter current = ((PXSelectBase<PX.Objects.CR.Extensions.AddressLookupFilter>) this.AddressLookupFilter)?.Current;
    string str = current.ViewName ?? this.AddressView;
    if (string.IsNullOrEmpty(str))
      return adapter.Get();
    PXView view = this.Base.Views[str];
    object obj = view.SelectSingle(Array.Empty<object>());
    IAddressLocation iaddressLocation = obj is PXResult pxResult ? (IAddressLocation) pxResult[0] : (IAddressLocation) obj;
    if (current != null && iaddressLocation != null && !string.IsNullOrEmpty(current.CountryID) && !string.IsNullOrEmpty(current.City))
    {
      iaddressLocation.Longitude = current.Longitude;
      iaddressLocation.Latitude = current.Latitude;
      ((IAddressBase) iaddressLocation).AddressLine1 = current.AddressLine1 ?? "";
      ((IAddressBase) iaddressLocation).AddressLine2 = current.AddressLine2 ?? "";
      ((IAddressBase) iaddressLocation).AddressLine3 = current.AddressLine3 ?? "";
      ((IAddressBase) iaddressLocation).CountryID = current.CountryID ?? ((IAddressBase) iaddressLocation).CountryID;
      ((IAddressBase) iaddressLocation).City = current.City ?? "";
      ((IAddressBase) iaddressLocation).PostalCode = current.PostalCode ?? "";
      ((IAddressBase) iaddressLocation).State = current?.State ?? "";
      if (!string.IsNullOrEmpty(((IAddressBase) iaddressLocation).State))
      {
        PX.Objects.CS.State state = PXResultset<PX.Objects.CS.State>.op_Implicit(PXSelectBase<PX.Objects.CS.State, PXSelectReadonly<PX.Objects.CS.State, Where<PX.Objects.CS.State.name, Equal<Required<PX.Objects.CS.State.name>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
        {
          (object) ((IAddressBase) iaddressLocation).State
        }));
        if (state != null)
          ((IAddressBase) iaddressLocation).State = state.StateID;
      }
      view.Cache.Update((object) iaddressLocation);
    }
    ((PXSelectBase) this.AddressLookupFilter).Cache.Clear();
    ((PXSelectBase) this.AddressLookupFilter).Cache.IsDirty = false;
    return adapter.Get();
  }

  protected virtual IEnumerable AddressLookup(PXAdapter adapter)
  {
    DialogAnswerType answerType = WebDialogResultExtension.GetAnswerType(((PXSelectBase) this.AddressLookupFilter).View.Answer);
    if (answerType != null)
    {
      if (answerType - 1 <= 2)
        ;
    }
    else
    {
      this.Base.Actions["AddressLookupSelectAction"].SetVisible(true);
      this.Base.Actions["AddressLookupSelectAction"].SetEnabled(this.GetIsEnabled());
      this.AddressLookupFilter.Reset();
      PX.Objects.CR.Extensions.AddressLookupFilter addressLookupFilter = new PX.Objects.CR.Extensions.AddressLookupFilter()
      {
        SearchAddress = "",
        ViewName = this.AddressView,
        Longitude = (Decimal?) this.CurrentAddress?.Longitude,
        Latitude = (Decimal?) this.CurrentAddress?.Latitude,
        AddressLine1 = ((IAddressBase) this.CurrentAddress)?.AddressLine1,
        AddressLine2 = ((IAddressBase) this.CurrentAddress)?.AddressLine2,
        AddressLine3 = ((IAddressBase) this.CurrentAddress)?.AddressLine3,
        CountryID = ((IAddressBase) this.CurrentAddress)?.CountryID,
        City = ((IAddressBase) this.CurrentAddress)?.City,
        PostalCode = ((IAddressBase) this.CurrentAddress)?.PostalCode,
        State = ((IAddressBase) this.CurrentAddress)?.State
      };
      ((PXCache) GraphHelper.Caches<PX.Objects.CR.Extensions.AddressLookupFilter>((PXGraph) this.Base)).Clear();
      GraphHelper.Caches<PX.Objects.CR.Extensions.AddressLookupFilter>((PXGraph) this.Base).Insert(addressLookupFilter);
      ((PXCache) GraphHelper.Caches<PX.Objects.CR.Extensions.AddressLookupFilter>((PXGraph) this.Base)).IsDirty = false;
      ((PXSelectBase<PX.Objects.CR.Extensions.AddressLookupFilter>) this.AddressLookupFilter).SelectSingle(Array.Empty<object>());
      ((PXSelectBase<PX.Objects.CR.Extensions.AddressLookupFilter>) this.AddressLookupFilter).AskExt(true);
    }
    PXView pxView;
    if (((Dictionary<string, PXView>) this.Base.Views).TryGetValue(this.AddressView, out pxView))
    {
      pxView.RequestRefresh();
      this.Base.Views[this.Base.PrimaryView].RequestRefresh();
    }
    return adapter.Get();
  }

  public virtual void _(Events.RowSelected<TMaster> e)
  {
    bool flag1 = false;
    if (!string.IsNullOrEmpty(this.ActionName) && ((OrderedDictionary) this.Base.Actions).Contains((object) this.ActionName))
    {
      flag1 = PXAddressLookup.IsEnabled((PXGraph) this.Base);
      this.Base.Actions[this.ActionName].SetVisible(flag1);
      this.Base.Actions[this.ActionName].SetEnabled(true);
    }
    bool flag2 = this.Base.IsMobile || !flag1;
    if (!string.IsNullOrEmpty(this.ViewOnMap) && ((OrderedDictionary) this.Base.Actions).Contains((object) this.ViewOnMap))
    {
      this.Base.Actions[this.ViewOnMap].SetVisible(flag2);
    }
    else
    {
      if (string.IsNullOrEmpty(this.ViewOnMapActionName) || !((OrderedDictionary) this.Base.Actions).Contains((object) this.ViewOnMapActionName))
        return;
      this.Base.Actions[this.ViewOnMapActionName].SetVisible(flag2);
    }
  }

  protected virtual bool GetIsEnabled()
  {
    PXView view = this.Base.Views[this.AddressView];
    object obj = view.SelectSingle(Array.Empty<object>());
    TAddress address = (TAddress) (obj is PXResult pxResult ? pxResult[0] : obj);
    if ((object) address == null)
    {
      bool isDirty = view.Cache.IsDirty;
      address = (TAddress) view.Cache.Insert();
      view.Cache.IsDirty = isDirty;
    }
    if (!(view.Cache.GetStateExt<Address.addressLine1>((object) address) as PXFieldState).Enabled || !(view.Cache.GetStateExt<Address.addressLine2>((object) address) as PXFieldState).Enabled || !(view.Cache.GetStateExt<Address.addressLine3>((object) address) as PXFieldState).Enabled || !(view.Cache.GetStateExt<Address.city>((object) address) as PXFieldState).Enabled || !(view.Cache.GetStateExt<Address.state>((object) address) as PXFieldState).Enabled || !(view.Cache.GetStateExt<Address.postalCode>((object) address) as PXFieldState).Enabled || !(view.Cache.GetStateExt<Address.countryID>((object) address) as PXFieldState).Enabled)
      return false;
    return view.AllowInsert || view.AllowUpdate;
  }
}
