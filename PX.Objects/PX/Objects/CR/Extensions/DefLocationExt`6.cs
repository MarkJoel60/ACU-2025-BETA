// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.DefLocationExt`6
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Export;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CR.Extensions;

/// <summary>
/// Extension that is used for selecting and defaulting the Default Address and Default Contact of the Business Account and it's inheritors.
/// No Inserting of Contact and Address is implemented, as the Inserting is handled inside the <see cref="!:SharedChildOverrideGraphExt" /> graph extension.
/// </summary>
public abstract class DefLocationExt<TGraph, TDefContactAddress, TLocationDetails, TMaster, TBAccountID, TDefLocationID> : 
  PXGraphExtension<TGraph>,
  IAddressValidationHelper
  where TGraph : PXGraph
  where TDefContactAddress : PXGraphExtension<TGraph>
  where TLocationDetails : LocationDetailsExt<TGraph, TMaster, TBAccountID>
  where TMaster : PX.Objects.CR.BAccount, IBqlTable, new()
  where TBAccountID : class, IBqlField
  where TDefLocationID : class, IBqlField
{
  protected LocationValidator locationValidator;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Location> BaseLocationDummy;
  [PXViewName("Delivery Settings")]
  public PXSelect<PX.Objects.CR.Standalone.Location, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<TBAccountID>>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<TDefLocationID>>>>> DefLocation;
  [PXViewName("Shipping Contact")]
  public PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.bAccountID, Equal<Current<PX.Objects.CR.Standalone.Location.bAccountID>>, And<PX.Objects.CR.Contact.contactID, Equal<Current<PX.Objects.CR.Standalone.Location.defContactID>>>>> DefLocationContact;
  [PXViewName("Shipping Address")]
  public PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.bAccountID, Equal<Current<PX.Objects.CR.Standalone.Location.bAccountID>>, And<PX.Objects.CR.Address.addressID, Equal<Current<PX.Objects.CR.Standalone.Location.defAddressID>>>>> DefLocationAddress;
  public PXAction<TMaster> ViewDefLocationAddressOnMap;
  public PXAction<TMaster> SetDefaultLocation;
  private object _KeyToAbort;

  public virtual List<System.Type> InitLocationFields => new List<System.Type>();

  public TLocationDetails LocationDetailsExtension { get; private set; }

  public TDefContactAddress DefContactAddressExtension { get; private set; }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.LocationDetailsExtension = this.Base.GetExtension<TLocationDetails>() ?? throw new PXException("The graph does not have defined extension: {0}.", new object[1]
    {
      (object) typeof (TLocationDetails).Name
    });
    this.DefContactAddressExtension = this.Base.GetExtension<TDefContactAddress>() ?? throw new PXException("The graph does not have defined extension: {0}.", new object[1]
    {
      (object) typeof (TDefContactAddress).Name
    });
    this.SubstituteBaseLocationsView();
    this.locationValidator = new LocationValidator();
  }

  protected virtual void SubstituteBaseLocationsView()
  {
    if (this.BaseLocationsViewName == null || !this.Base.IsContractBasedAPI || !((Dictionary<string, PXView>) this.Base.Views).ContainsKey(this.BaseLocationsViewName))
      return;
    this.Base.Views[this.BaseLocationsViewName] = new PXView((PXGraph) this.Base, false, ((PXSelectBase) this.DefLocation).View.BqlSelect);
  }

  protected virtual string BaseLocationsViewName => "BaseLocations";

  [PXOptimizationBehavior(IgnoreBqlDelegate = true)]
  public virtual IEnumerable defLocationContact()
  {
    return this.SelectEntityByKey<PX.Objects.CR.Contact, PX.Objects.CR.Contact.contactID, PX.Objects.CR.Standalone.Location.defContactID, PX.Objects.CR.Standalone.Location.overrideContact>();
  }

  [PXOptimizationBehavior(IgnoreBqlDelegate = true)]
  public virtual IEnumerable defLocationAddress()
  {
    return this.SelectEntityByKey<PX.Objects.CR.Address, PX.Objects.CR.Address.addressID, PX.Objects.CR.Standalone.Location.defAddressID, PX.Objects.CR.Standalone.Location.overrideAddress>();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable viewDefLocationAddressOnMap(PXAdapter adapter)
  {
    BAccountUtility.ViewOnMap(((PXSelectBase<PX.Objects.CR.Address>) this.DefLocationAddress).SelectSingle(Array.Empty<object>()));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Set as Default")]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual void setDefaultLocation()
  {
    if (!(this.Base.Caches[typeof (TMaster)].Current is TMaster current1) || !current1.BAccountID.HasValue)
      return;
    PX.Objects.CR.Standalone.Location current2 = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.LocationDetailsExtension.Locations).Current;
    if (current2 == null)
      return;
    int? nullable = current2.LocationID;
    if (!nullable.HasValue)
      return;
    if (!current2.IsActive.GetValueOrDefault())
      throw new Exception("Default location can not be inactive.");
    PX.Objects.CR.Standalone.Location location = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelect<PX.Objects.CR.Standalone.Location, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Required<PX.Objects.CR.BAccount.defLocationID>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[2]
    {
      (object) current1.BAccountID,
      (object) current1.DefLocationID
    }));
    if (location != null && ((PXSelectBase) this.DefLocation).Cache.GetStatus((object) location) == 2)
      ((PXSelectBase) this.DefLocation).Cache.Delete((object) location);
    nullable = current1.DefLocationID;
    int? locationId = current2.LocationID;
    if (!(nullable.GetValueOrDefault() == locationId.GetValueOrDefault() & nullable.HasValue == locationId.HasValue))
      this.SetLocationAsDefault(current2);
    current1.DefLocationID = current2.LocationID;
    GraphHelper.MarkUpdated(((PXSelectBase) this.DefLocation).Cache, (object) current2);
    this.Base.Caches[typeof (TMaster)].Update((object) current1);
  }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.CR.BAccount.bAccountID))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.bAccountID> e)
  {
  }

  [PXDBChildIdentity(typeof (PX.Objects.CR.Contact.contactID))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.defContactID> e)
  {
  }

  [PXDBChildIdentity(typeof (PX.Objects.CR.Address.addressID))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.defAddressID> e)
  {
  }

  [PXDBInt]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cARAccountLocationID> e)
  {
  }

  [PXDBInt]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vAPAccountLocationID> e)
  {
  }

  [PXDBInt]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vPaymentInfoLocationID> e)
  {
  }

  [PXShort]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vSiteIDIsNull> e)
  {
  }

  [PXFormula(typeof (Switch<Case<Where<CurrentValue<PX.Objects.CR.BAccount.defAddressID>, IsNull>, Null, Case<Where<PX.Objects.CR.Standalone.Location.defAddressID, Equal<CurrentValue<PX.Objects.CR.BAccount.defAddressID>>>, False>>, True>))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.overrideAddress> e)
  {
  }

  [PXFormula(typeof (Switch<Case<Where<CurrentValue<PX.Objects.CR.BAccount.defContactID>, IsNull>, Null, Case<Where<PX.Objects.CR.Standalone.Location.defContactID, Equal<CurrentValue<PX.Objects.CR.BAccount.defContactID>>>, False>>, True>))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.overrideContact> e)
  {
  }

  [PXFormula(typeof (Switch<Case<Where<CurrentValue<PX.Objects.CR.BAccount.defAddressID>, IsNull>, Null, Case<Where<PX.Objects.CR.Standalone.Location.defAddressID, Equal<CurrentValue<PX.Objects.CR.BAccount.defAddressID>>>, True>>, False>))]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.isAddressSameAsMain> e)
  {
  }

  [PXFormula(typeof (Switch<Case<Where<CurrentValue<PX.Objects.CR.BAccount.defContactID>, IsNull>, Null, Case<Where<PX.Objects.CR.Standalone.Location.defContactID, Equal<CurrentValue<PX.Objects.CR.BAccount.defContactID>>>, True>>, False>))]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.isContactSameAsMain> e)
  {
  }

  [PXDBInt]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Standalone.Location.locationID))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<TDefLocationID> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vBranchID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vBranchID>, PX.Objects.CR.Standalone.Location, object>) e).NewValue = (object) null;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vBranchID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vAPAccountLocationID> e)
  {
    this.DefaultFromSame<PX.Objects.CR.Standalone.Location.locationID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vAPAccountLocationID>>) e).Args, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vAPAccountLocationID>>) e).Cache);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cARAccountLocationID> e)
  {
    this.DefaultFromSame<PX.Objects.CR.Standalone.Location.locationID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cARAccountLocationID>>) e).Args, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cARAccountLocationID>>) e).Cache);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vPaymentInfoLocationID> e)
  {
    this.DefaultFromSame<PX.Objects.CR.Standalone.Location.locationID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vPaymentInfoLocationID>>) e).Args, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vPaymentInfoLocationID>>) e).Cache);
  }

  protected virtual void _(PX.Data.Events.RowSelected<TMaster> e)
  {
    TMaster row = e.Row;
    if ((object) row == null)
      return;
    ((PXAction) this.SetDefaultLocation).SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<TMaster>>) e).Cache.GetStatus((object) row) != 2);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.CR.Standalone.Location> e)
  {
    this.PopulateIsDefault(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PX.Objects.CR.Standalone.Location> e)
  {
    this.PopulateIsDefault(e.Row);
  }

  protected virtual void PopulateIsDefault(PX.Objects.CR.Standalone.Location row)
  {
    if (row == null)
      return;
    row.IsDefault = new bool?(false);
    if (!(this.Base.Caches[typeof (TMaster)].Current is TMaster current))
      return;
    int? locationId = row.LocationID;
    int? defLocationId = current.DefLocationID;
    if (!(locationId.GetValueOrDefault() == defLocationId.GetValueOrDefault() & locationId.HasValue == defLocationId.HasValue))
      return;
    row.IsDefault = new bool?(true);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location> e)
  {
    if (e.Operation != 2)
      return;
    if (e.TranStatus == null)
    {
      int? nullable = (int?) ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location>>) e).Cache.GetValue<PX.Objects.CR.Standalone.Location.vAPAccountLocationID>((object) e.Row);
      if (nullable.HasValue)
      {
        nullable = (int?) ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location>>) e).Cache.GetValue<PX.Objects.CR.Standalone.Location.vAPAccountLocationID>((object) e.Row);
        int num = 0;
        if (!(nullable.GetValueOrDefault() < num & nullable.HasValue))
          goto label_5;
      }
      this._KeyToAbort = ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location>>) e).Cache.GetValue<PX.Objects.CR.Standalone.Location.locationID>((object) e.Row);
      PXDatabase.Update<PX.Objects.CR.Location>(new PXDataFieldParam[3]
      {
        (PXDataFieldParam) new PXDataFieldAssign("VAPAccountLocationID", this._KeyToAbort),
        (PXDataFieldParam) new PXDataFieldRestrict("LocationID", this._KeyToAbort),
        (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed
      });
      ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location>>) e).Cache.SetValue<PX.Objects.CR.Standalone.Location.vAPAccountLocationID>((object) e.Row, this._KeyToAbort);
label_5:
      nullable = (int?) ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location>>) e).Cache.GetValue<PX.Objects.CR.Standalone.Location.vPaymentInfoLocationID>((object) e.Row);
      if (nullable.HasValue)
      {
        nullable = (int?) ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location>>) e).Cache.GetValue<PX.Objects.CR.Standalone.Location.vPaymentInfoLocationID>((object) e.Row);
        int num = 0;
        if (!(nullable.GetValueOrDefault() < num & nullable.HasValue))
          goto label_8;
      }
      this._KeyToAbort = ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location>>) e).Cache.GetValue<PX.Objects.CR.Standalone.Location.locationID>((object) e.Row);
      PXDatabase.Update<PX.Objects.CR.Standalone.Location>(new PXDataFieldParam[3]
      {
        (PXDataFieldParam) new PXDataFieldAssign("VPaymentInfoLocationID", this._KeyToAbort),
        (PXDataFieldParam) new PXDataFieldRestrict("LocationID", this._KeyToAbort),
        (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed
      });
      ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location>>) e).Cache.SetValue<PX.Objects.CR.Standalone.Location.vPaymentInfoLocationID>((object) e.Row, this._KeyToAbort);
label_8:
      nullable = (int?) ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location>>) e).Cache.GetValue<PX.Objects.CR.Standalone.Location.cARAccountLocationID>((object) e.Row);
      if (nullable.HasValue)
      {
        nullable = (int?) ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location>>) e).Cache.GetValue<PX.Objects.CR.Standalone.Location.cARAccountLocationID>((object) e.Row);
        int num = 0;
        if (!(nullable.GetValueOrDefault() < num & nullable.HasValue))
          return;
      }
      this._KeyToAbort = ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location>>) e).Cache.GetValue<PX.Objects.CR.Standalone.Location.locationID>((object) e.Row);
      PXDatabase.Update<PX.Objects.CR.Standalone.Location>(new PXDataFieldParam[3]
      {
        (PXDataFieldParam) new PXDataFieldAssign("CARAccountLocationID", this._KeyToAbort),
        (PXDataFieldParam) new PXDataFieldRestrict("LocationID", this._KeyToAbort),
        (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed
      });
      ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location>>) e).Cache.SetValue<PX.Objects.CR.Standalone.Location.cARAccountLocationID>((object) e.Row, this._KeyToAbort);
    }
    else
    {
      if (e.TranStatus == 2)
      {
        if (object.Equals(this._KeyToAbort, ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location>>) e).Cache.GetValue<PX.Objects.CR.Standalone.Location.vAPAccountLocationID>((object) e.Row)))
          ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location>>) e).Cache.SetValue<PX.Objects.CR.Standalone.Location.vAPAccountLocationID>((object) e.Row, (object) null);
        if (object.Equals(this._KeyToAbort, ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location>>) e).Cache.GetValue<PX.Objects.CR.Standalone.Location.vPaymentInfoLocationID>((object) e.Row)))
          ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location>>) e).Cache.SetValue<PX.Objects.CR.Standalone.Location.vPaymentInfoLocationID>((object) e.Row, (object) null);
        if (object.Equals(this._KeyToAbort, ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location>>) e).Cache.GetValue<PX.Objects.CR.Standalone.Location.cARAccountLocationID>((object) e.Row)))
          ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location>>) e).Cache.SetValue<PX.Objects.CR.Standalone.Location.cARAccountLocationID>((object) e.Row, (object) null);
      }
      this._KeyToAbort = (object) null;
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<TMaster> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<TMaster>>) e).Cache.ObjectsEqual<PX.Objects.CR.BAccount.acctCD>((object) e.Row, (object) e.OldRow) || e.OldRow.AcctCD != null)
      return;
    this.InsertLocation(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<TMaster>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserted<TMaster> e, PXRowInserted del)
  {
    TMaster row = e.Row;
    if ((object) row == null)
      return;
    this.InsertLocation(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<TMaster>>) e).Cache, row);
    del?.Invoke(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<TMaster>>) e).Cache, ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<TMaster>>) e).Args);
  }

  [PXOverride]
  public virtual void Persist(System.Action del)
  {
    if (this.Base.Caches[typeof (TMaster)].Current is TMaster current && this.Base.Caches[typeof (TMaster)].GetStatus((object) current) == 1)
    {
      bool flag = false;
      foreach (PXResult<PX.Objects.CR.Standalone.Location> pxResult in ((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.LocationDetailsExtension.Locations).Select(Array.Empty<object>()))
      {
        PX.Objects.CR.Standalone.Location location = PXResult<PX.Objects.CR.Standalone.Location>.op_Implicit(pxResult);
        PXEntryStatus status = ((PXSelectBase) this.DefLocation).Cache.GetStatus((object) location);
        if (status != 1 && status != 2)
          flag |= !this.ValidateLocation(((PXSelectBase) this.DefLocation).Cache, location);
      }
      if (flag)
        throw new PXException("The record cannot be saved because at least one error has occurred. Please review the errors.");
    }
    del();
  }

  public virtual bool CurrentAddressRequiresValidation => true;

  public virtual void ValidateAddress()
  {
    PX.Objects.CR.Address aAddress = ((PXSelectBase<PX.Objects.CR.Address>) this.DefLocationAddress).SelectSingle(Array.Empty<object>());
    if (aAddress == null)
      return;
    bool? isValidated = aAddress.IsValidated;
    bool flag = false;
    if (!(isValidated.GetValueOrDefault() == flag & isValidated.HasValue))
      return;
    PXAddressValidator.Validate<PX.Objects.CR.Address>((PXGraph) this.Base, aAddress, true, true);
  }

  public virtual void InsertLocation(PXCache cache, TMaster row)
  {
    if (row.DefLocationID.HasValue)
      return;
    bool isDirty = ((PXSelectBase) this.DefLocation).Cache.IsDirty;
    PX.Objects.CR.Standalone.Location instance = (PX.Objects.CR.Standalone.Location) ((PXSelectBase) this.DefLocation).Cache.CreateInstance();
    instance.BAccountID = row.BAccountID;
    object obj = (object) PXMessages.LocalizeNoPrefix("MAIN");
    ((PXSelectBase) this.DefLocation).Cache.RaiseFieldUpdating<PX.Objects.CR.Standalone.Location.locationCD>((object) instance, ref obj);
    instance.LocationCD = (string) obj;
    instance.LocType = "CP";
    switch (row.Type)
    {
      case "VE":
        instance.LocType = "VE";
        break;
      case "CU":
        instance.LocType = "CU";
        break;
      case "VC":
        instance.LocType = "VC";
        break;
      case "EC":
        instance.LocType = "CU";
        break;
      default:
        instance.LocType = "CP";
        break;
    }
    instance.Descr = PXMessages.LocalizeNoPrefix("Primary Location");
    instance.IsDefault = new bool?(true);
    instance.DefAddressID = row.DefAddressID;
    instance.OverrideAddress = new bool?(false);
    instance.DefContactID = row.DefContactID;
    instance.OverrideContact = new bool?(false);
    PX.Objects.CR.Standalone.Location location1 = (PX.Objects.CR.Standalone.Location) ((PXSelectBase) this.DefLocation).Cache.Insert((object) instance);
    this.InitLocation((IBqlTable) location1, location1.LocType, false);
    PX.Objects.CR.Standalone.Location location2 = (PX.Objects.CR.Standalone.Location) ((PXSelectBase) this.DefLocation).Cache.Update((object) location1);
    cache.SetValue<PX.Objects.CR.BAccount.defLocationID>((object) row, (object) location2.LocationID);
    ((PXSelectBase) this.DefLocation).Cache.IsDirty = isDirty;
  }

  public virtual void InitLocation(IBqlTable location, string aLocationType, bool onlySetDefault)
  {
    if (location == null)
      return;
    PXCache cach = this.Base.Caches[location.GetType()];
    foreach (System.Type initLocationField in this.InitLocationFields)
    {
      if (onlySetDefault || cach.GetValue((object) location, initLocationField.Name) == null)
        cach.SetDefaultExt((object) location, initLocationField.Name, (object) null);
    }
    cach.SetValue<PX.Objects.CR.Standalone.Location.locType>((object) location, (object) aLocationType);
  }

  public virtual bool ValidateLocation(PXCache cache, PX.Objects.CR.Standalone.Location location)
  {
    return true;
  }

  public virtual void SetLocationAsDefault(PX.Objects.CR.Standalone.Location row)
  {
    int? locationId = row.LocationID;
    foreach (PXResult<PX.Objects.CR.Standalone.Location> pxResult in ((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.LocationDetailsExtension.Locations).Select(Array.Empty<object>()))
    {
      PX.Objects.CR.Standalone.Location objA = PXResult<PX.Objects.CR.Standalone.Location>.op_Implicit(pxResult);
      if (!object.Equals((object) objA.LocationID, (object) objA.VAPAccountLocationID))
      {
        PX.Objects.CR.Standalone.Location location = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.DefLocation).SelectSingle(Array.Empty<object>());
        objA.VAPAccountLocationID = locationId;
        if (object.Equals((object) objA, (object) row) && location != null)
        {
          objA.VAPAccountID = location.VAPAccountID;
          objA.VAPSubID = location.VAPSubID;
          objA.VRetainageAcctID = location.VRetainageAcctID;
          objA.VRetainageSubID = location.VRetainageSubID;
        }
        GraphHelper.MarkUpdated(((PXSelectBase) this.DefLocation).Cache, (object) objA);
        ((PXSelectBase) this.DefLocation).Cache.IsDirty = true;
      }
      if (!object.Equals((object) objA.LocationID, (object) objA.VPaymentInfoLocationID))
      {
        PX.Objects.CR.Standalone.Location location = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.DefLocation).SelectSingle(Array.Empty<object>());
        objA.VPaymentInfoLocationID = locationId;
        if (object.Equals((object) objA, (object) row) && location != null)
        {
          objA.VCashAccountID = location.VCashAccountID;
          objA.VPaymentMethodID = location.VPaymentMethodID;
          objA.VPaymentLeadTime = location.VPaymentLeadTime;
          objA.VSeparateCheck = location.VSeparateCheck;
          objA.VPaymentByType = location.VPaymentByType;
          objA.VRemitAddressID = location.VRemitAddressID;
          objA.VRemitContactID = location.VRemitContactID;
        }
        GraphHelper.MarkUpdated(((PXSelectBase) this.DefLocation).Cache, (object) objA);
        ((PXSelectBase) this.DefLocation).Cache.IsDirty = true;
      }
      if (!object.Equals((object) objA.LocationID, (object) objA.CARAccountLocationID))
      {
        PX.Objects.CR.Standalone.Location location = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.DefLocation).SelectSingle(Array.Empty<object>());
        objA.CARAccountLocationID = locationId;
        if (object.Equals((object) objA, (object) row) && location != null)
        {
          objA.CARAccountID = location.CARAccountID;
          objA.CARSubID = location.CARSubID;
          objA.CRetainageAcctID = location.CRetainageAcctID;
          objA.CRetainageSubID = location.CRetainageSubID;
        }
        GraphHelper.MarkUpdated(((PXSelectBase) this.DefLocation).Cache, (object) objA);
        ((PXSelectBase) this.DefLocation).Cache.IsDirty = true;
      }
    }
  }

  public virtual void DefaultFrom<TSourceField>(
    PXFieldDefaultingEventArgs e,
    PXCache source,
    object sourceObject,
    bool allowNull = true,
    object nullValue = null)
    where TSourceField : IBqlField
  {
    if (sourceObject == null || !allowNull && source.GetValue<TSourceField>(sourceObject) == null)
      return;
    e.NewValue = source.GetValue<TSourceField>(sourceObject) ?? nullValue;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void DefaultFrom<TSourceField>(
    PXFieldDefaultingEventArgs e,
    PXCache source,
    bool allowNull = true,
    object nullValue = null)
    where TSourceField : IBqlField
  {
    this.DefaultFrom<TSourceField>(e, source, source.Current, allowNull, nullValue);
  }

  public virtual void DefaultFromSame<TSourceField>(
    PXFieldDefaultingEventArgs e,
    PXCache source,
    bool allowNull = true,
    object nullValue = null)
    where TSourceField : IBqlField
  {
    if (e.Row == null || !allowNull && source.GetValue<TSourceField>(e.Row) == null)
      return;
    e.NewValue = source.GetValue<TSourceField>(e.Row) ?? nullValue;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual IEnumerable SelectEntityByKey<TEntity, TEntityKey, TValueField, TOverrideField>()
    where TEntity : class, IBqlTable, new()
    where TEntityKey : IBqlField
    where TValueField : IBqlField
    where TOverrideField : IBqlField
  {
    PX.Objects.CR.Standalone.Location valueObject = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.DefLocation).Current;
    if (valueObject == null)
    {
      valueObject = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.DefLocation).Current = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.DefLocation).SelectSingle(Array.Empty<object>());
      ((PXSelectBase) this.DefLocation).Cache.RaiseRowSelected((object) valueObject);
    }
    if (valueObject != null)
    {
      foreach (object obj in this.SelectEntityByKey<TEntity, TEntityKey, TValueField, TOverrideField>((object) valueObject))
        yield return obj;
    }
  }

  public virtual IEnumerable SelectEntityByKey<TEntity, TEntityKey, TValueField, TOverrideField>(
    object valueObject)
    where TEntity : class, IBqlTable, new()
    where TEntityKey : IBqlField
    where TValueField : IBqlField
    where TOverrideField : IBqlField
  {
    DefLocationExt<TGraph, TDefContactAddress, TLocationDetails, TMaster, TBAccountID, TDefLocationID> defLocationExt = this;
    if (valueObject != null)
    {
      TEntity entity = ((PXSelectBase<TEntity>) new PXSelect<TEntity, Where<TEntityKey, Equal<Required<TValueField>>>>((PXGraph) defLocationExt.Base)).SelectSingle(new object[1]
      {
        defLocationExt.Base.Caches[typeof (TValueField).DeclaringType].GetValue<TValueField>(valueObject)
      });
      if ((defLocationExt.Base.Caches[typeof (TOverrideField).DeclaringType].GetValue<TOverrideField>(valueObject) as bool?).GetValueOrDefault())
      {
        PXUIFieldAttribute.SetEnabled((PXCache) GraphHelper.Caches<TEntity>((PXGraph) defLocationExt.Base), (object) entity, true);
      }
      else
      {
        entity = PXCache<TEntity>.CreateCopy(entity);
        PXUIFieldAttribute.SetEnabled((PXCache) GraphHelper.Caches<TEntity>((PXGraph) defLocationExt.Base), (object) entity, false);
      }
      yield return (object) entity;
    }
  }

  public abstract class WithUIExtension : 
    DefLocationExt<TGraph, TDefContactAddress, TLocationDetails, TMaster, TBAccountID, TDefLocationID>
  {
    [PXDBDefault(typeof (PX.Objects.CR.BAccount.bAccountID))]
    [PXMergeAttributes]
    protected new virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.bAccountID> e)
    {
    }

    [LocationRaw(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<PX.Objects.CR.Standalone.Location.bAccountID>>>), typeof (PX.Objects.CR.Standalone.Location.locationCD))]
    [PXDefault]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.locationCD> e)
    {
    }

    [PXSelector(typeof (Search<PX.Objects.TX.TaxZone.taxZoneID>), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), CacheGlobal = true)]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cTaxZoneID> e)
    {
    }

    [PXSelector(typeof (Search<Carrier.carrierID>), new System.Type[] {typeof (Carrier.carrierID), typeof (Carrier.description), typeof (Carrier.isExternal), typeof (Carrier.confirmationRequired)}, CacheGlobal = true, DescriptionField = typeof (Carrier.description))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cCarrierID> e)
    {
    }

    [PXSelector(typeof (Search<ShipTerms.shipTermsID>), CacheGlobal = true, DescriptionField = typeof (ShipTerms.description))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cShipTermsID> e)
    {
    }

    [PXSelector(typeof (ShippingZone.zoneID), CacheGlobal = true, DescriptionField = typeof (ShippingZone.description))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cShipZoneID> e)
    {
    }

    [PXSelector(typeof (FOBPoint.fOBPointID), CacheGlobal = true, DescriptionField = typeof (FOBPoint.description))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cFOBPointID> e)
    {
    }

    [Branch(null, null, true, true, false)]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cBranchID> e)
    {
    }

    [Account]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cSalesAcctID> e)
    {
    }

    [SubAccount(typeof (PX.Objects.CR.Standalone.Location.cSalesAcctID))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cSalesSubID> e)
    {
    }

    [PXSelector(typeof (ARPriceClass.priceClassID), DescriptionField = typeof (ARPriceClass.description))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cPriceClassID> e)
    {
    }

    [PXDimensionSelector("INSITE", typeof (INSite.siteID), typeof (INSite.siteCD), DescriptionField = typeof (INSite.descr))]
    [PXRestrictor(typeof (Where<INSite.active, Equal<True>>), "Warehouse '{0}' is inactive", new System.Type[] {typeof (INSite.siteCD)})]
    [PXRestrictor(typeof (Where<INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>), "The warehouse cannot be selected; it is used for transit.", new System.Type[] {})]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cSiteID> e)
    {
    }

    [Account]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cDiscountAcctID> e)
    {
    }

    [SubAccount(typeof (PX.Objects.CR.Standalone.Location.cDiscountAcctID))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cDiscountSubID> e)
    {
    }

    [Account]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cRetainageAcctID> e)
    {
    }

    [SubAccount(typeof (PX.Objects.CR.Standalone.Location.cRetainageAcctID))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cRetainageSubID> e)
    {
    }

    [Account]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cFreightAcctID> e)
    {
    }

    [SubAccount(typeof (PX.Objects.CR.Standalone.Location.cFreightAcctID))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cFreightSubID> e)
    {
    }

    [PXSelector(typeof (Search<CSCalendar.calendarID>), DescriptionField = typeof (CSCalendar.description))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cCalendarID> e)
    {
    }

    [Project(typeof (Where<PMProject.customerID, Equal<Current<PX.Objects.CR.Standalone.Location.bAccountID>>>), DisplayName = "Default Project")]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cDefProjectID> e)
    {
    }

    [Account(null, typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "AR Account", Required = true, ControlAccountForModule = "AR")]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cARAccountID> e)
    {
    }

    [SubAccount(typeof (PX.Objects.CR.Standalone.Location.cARAccountID), DisplayName = "AR Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description), Required = true)]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cARSubID> e)
    {
    }

    [PXSelector(typeof (Search<PX.Objects.TX.TaxZone.taxZoneID>), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), CacheGlobal = true)]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vTaxZoneID> e)
    {
    }

    [PXSelector(typeof (Search<Carrier.carrierID>), new System.Type[] {typeof (Carrier.carrierID), typeof (Carrier.description), typeof (Carrier.isExternal), typeof (Carrier.confirmationRequired)}, CacheGlobal = true, DescriptionField = typeof (Carrier.description))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vCarrierID> e)
    {
    }

    [PXSelector(typeof (Search<ShipTerms.shipTermsID>), CacheGlobal = true, DescriptionField = typeof (ShipTerms.description))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vShipTermsID> e)
    {
    }

    [PXSelector(typeof (FOBPoint.fOBPointID), CacheGlobal = true, DescriptionField = typeof (FOBPoint.description))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vFOBPointID> e)
    {
    }

    [Branch(null, null, true, true, false)]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vBranchID> e)
    {
    }

    [Account]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vExpenseAcctID> e)
    {
    }

    [SubAccount(typeof (PX.Objects.CR.Standalone.Location.vExpenseAcctID))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vExpenseSubID> e)
    {
    }

    [Account]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vRetainageAcctID> e)
    {
    }

    [SubAccount(typeof (PX.Objects.CR.Standalone.Location.vRetainageAcctID))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vRetainageSubID> e)
    {
    }

    [Account]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vFreightAcctID> e)
    {
    }

    [SubAccount(typeof (PX.Objects.CR.Standalone.Location.vFreightAcctID))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vFreightSubID> e)
    {
    }

    [Account]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vDiscountAcctID> e)
    {
    }

    [SubAccount(typeof (PX.Objects.CR.Standalone.Location.vDiscountAcctID))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vDiscountSubID> e)
    {
    }

    [PXDimensionSelector("INSITE", typeof (INSite.siteID), typeof (INSite.siteCD), DescriptionField = typeof (INSite.descr))]
    [PXRestrictor(typeof (Where<INSite.active, Equal<True>>), "Warehouse '{0}' is inactive", new System.Type[] {typeof (INSite.siteCD)})]
    [PXRestrictor(typeof (Where<INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>), "The warehouse cannot be selected; it is used for transit.", new System.Type[] {})]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vSiteID> e)
    {
    }

    [Project(DisplayName = "Default Project")]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vDefProjectID> e)
    {
    }

    [Account(null, typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "AP Account", Required = true, ControlAccountForModule = "AP")]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vAPAccountID> e)
    {
    }

    [SubAccount(typeof (PX.Objects.CR.Standalone.Location.vAPAccountID), DisplayName = "AP Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description), Required = true)]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vAPSubID> e)
    {
    }

    [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.useForAP, Equal<True>, And<PX.Objects.CA.PaymentMethod.isActive, Equal<True>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vPaymentMethodID> e)
    {
    }

    [CashAccount(typeof (PX.Objects.CR.Location.vBranchID), typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>>, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.CA.CashAccount.clearingAccount, Equal<False>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<PX.Objects.CR.Location.vPaymentMethodID>>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>>))]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vCashAccountID> e)
    {
    }

    [SubAccount]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cMPSalesSubID> e)
    {
    }

    [SubAccount]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cMPExpenseSubID> e)
    {
    }

    [SubAccount]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cMPFreightSubID> e)
    {
    }

    [SubAccount]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cMPDiscountSubID> e)
    {
    }

    [SubAccount]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cMPGainLossSubID> e)
    {
    }

    [PXDimensionSelector("INSITE", typeof (INSite.siteID), typeof (INSite.siteCD), DescriptionField = typeof (INSite.descr))]
    [PXRestrictor(typeof (Where<INSite.active, Equal<True>>), "Warehouse '{0}' is inactive", new System.Type[] {typeof (INSite.siteCD)})]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cMPSiteID> e)
    {
    }
  }

  public abstract class WithCombinedTypeValidation : 
    DefLocationExt<TGraph, TDefContactAddress, TLocationDetails, TMaster, TBAccountID, TDefLocationID>.WithUIExtension
  {
    protected virtual void _(PX.Data.Events.RowDeleting<TMaster> e)
    {
      TMaster row = e.Row;
      if ((object) row == null || !(row.Type == "VC") && !row.IsBranch.GetValueOrDefault())
        return;
      PXParentAttribute.SetLeaveChildren<PX.Objects.CR.Location.bAccountID>(((PXSelectBase) this.BaseLocationDummy).Cache, (object) null, true);
    }
  }
}
