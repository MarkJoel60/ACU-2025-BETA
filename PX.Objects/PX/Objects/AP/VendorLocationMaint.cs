// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorLocationMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CR.Extensions.Relational;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP;

public class VendorLocationMaint : LocationMaint
{
  public PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.AP.Vendor.bAccountID, IsNotNull, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>> Vendor;
  public PXSelect<LocationAPAccountSub, Where<LocationAPAccountSub.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>, And<LocationAPAccountSub.locationID, Equal<Current<PX.Objects.CR.Location.vAPAccountLocationID>>>>> APAccountSubLocation;
  public PXSelect<LocationAPPaymentInfo, Where<LocationAPPaymentInfo.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>, And<LocationAPPaymentInfo.locationID, Equal<Current<PX.Objects.CR.Location.vPaymentInfoLocationID>>>>> APPaymentInfoLocation;
  public PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Address.addressID, Equal<Current<PX.Objects.CR.Location.vRemitAddressID>>>>> RemitAddress;
  public PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Contact.contactID, Equal<Current<PX.Objects.CR.Location.vRemitContactID>>>>> RemitContact;
  public PXSelectJoin<VendorPaymentMethodDetail, InnerJoin<PX.Objects.CA.PaymentMethod, On<VendorPaymentMethodDetail.paymentMethodID, Equal<PX.Objects.CA.PaymentMethod.paymentMethodID>>, InnerJoin<PaymentMethodDetail, On<PaymentMethodDetail.paymentMethodID, Equal<VendorPaymentMethodDetail.paymentMethodID>, And<PaymentMethodDetail.detailID, Equal<VendorPaymentMethodDetail.detailID>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>>>, Where<VendorPaymentMethodDetail.bAccountID, Equal<Optional<LocationAPPaymentInfo.bAccountID>>, And<VendorPaymentMethodDetail.locationID, Equal<Optional<LocationAPPaymentInfo.locationID>>, And<VendorPaymentMethodDetail.paymentMethodID, Equal<Optional<LocationAPPaymentInfo.vPaymentMethodID>>>>>, OrderBy<Asc<PaymentMethodDetail.orderIndex>>> PaymentDetails;
  public PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Optional<LocationAPPaymentInfo.vPaymentMethodID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>> PaymentTypeDetails;
  public PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>>> Addresses;
  public PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>>> Contacts;
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.CR.LocationBranchSettings, Where<PX.Objects.CR.LocationBranchSettings.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.LocationBranchSettings.locationID, Equal<Current<PX.Objects.CR.Location.locationID>>, And<PX.Objects.CR.LocationBranchSettings.branchID, Equal<Current<AccessInfo.branchID>>>>>> LocationBranchSettings;
  public PXAction<PX.Objects.CR.Location> viewRemitOnMap;

  [PXUIField(DisplayName = "Validate Addresses", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = true, FieldClass = "Validate Address")]
  [PXButton]
  public override IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    PX.Objects.CR.Location current = this.Location.Current;
    if (current != null)
    {
      PX.Objects.CR.Address address1 = this.FindAddress(current.DefAddressID);
      bool? isValidated;
      if (address1 != null)
      {
        isValidated = address1.IsValidated;
        bool flag = false;
        if (isValidated.GetValueOrDefault() == flag & isValidated.HasValue)
          PXAddressValidator.Validate<PX.Objects.CR.Address>((PXGraph) this, address1, true, true);
      }
      PX.Objects.CR.Address address2 = this.FindAddress(current.VRemitAddressID);
      if (address2 != null)
      {
        isValidated = address2.IsValidated;
        bool flag = false;
        if (isValidated.GetValueOrDefault() == flag & isValidated.HasValue)
        {
          int? addressId1 = address2.AddressID;
          int? addressId2 = address1.AddressID;
          if (!(addressId1.GetValueOrDefault() == addressId2.GetValueOrDefault() & addressId1.HasValue == addressId2.HasValue))
            PXAddressValidator.Validate<PX.Objects.CR.Address>((PXGraph) this, address2, true, true);
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Cancel", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXCancelButton]
  protected override IEnumerable Cancel(PXAdapter adapter)
  {
    int? nullable1 = this.Location.Current != null ? this.Location.Current.BAccountID : new int?();
    IEnumerator enumerator1 = new PXCancel<PX.Objects.CR.Location>((PXGraph) this, nameof (Cancel)).Press(adapter).GetEnumerator();
    try
    {
      if (enumerator1.MoveNext())
      {
        object current = enumerator1.Current;
        PX.Objects.CR.Location location = PXResult.Unwrap<PX.Objects.CR.Location>(current);
        if (!this.IsImport && this.Location.Cache.GetStatus((object) location) == PXEntryStatus.Inserted)
        {
          int? nullable2 = nullable1;
          int? baccountId = location.BAccountID;
          if (!(nullable2.GetValueOrDefault() == baccountId.GetValueOrDefault() & nullable2.HasValue == baccountId.HasValue) || string.IsNullOrEmpty(location.LocationCD))
          {
            IEnumerator enumerator2 = this.First.Press(adapter).GetEnumerator();
            try
            {
              if (enumerator2.MoveNext())
                return (IEnumerable) new object[1]
                {
                  enumerator2.Current
                };
            }
            finally
            {
              if (enumerator2 is IDisposable disposable)
                disposable.Dispose();
            }
            location.LocationCD = (string) null;
            return (IEnumerable) new object[1]{ current };
          }
        }
        return (IEnumerable) new object[1]{ current };
      }
    }
    finally
    {
      if (enumerator1 is IDisposable disposable)
        disposable.Dispose();
    }
    return (IEnumerable) new object[0];
  }

  [PXUIField(DisplayName = "Prev", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXPreviousButton]
  protected override IEnumerable Previous(PXAdapter adapter)
  {
    IEnumerator enumerator = new PXPrevious<PX.Objects.CR.Location>((PXGraph) this, "Prev").Press(adapter).GetEnumerator();
    try
    {
      if (enumerator.MoveNext())
      {
        object current = enumerator.Current;
        if (this.Location.Cache.GetStatus((object) PXResult.Unwrap<PX.Objects.CR.Location>(current)) == PXEntryStatus.Inserted)
          return this.Last.Press(adapter);
        return (IEnumerable) new object[1]{ current };
      }
    }
    finally
    {
      if (enumerator is IDisposable disposable)
        disposable.Dispose();
    }
    return (IEnumerable) new object[0];
  }

  [PXUIField(DisplayName = "Next", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXNextButton]
  protected override IEnumerable Next(PXAdapter adapter)
  {
    IEnumerator enumerator = new PXNext<PX.Objects.CR.Location>((PXGraph) this, nameof (Next)).Press(adapter).GetEnumerator();
    try
    {
      if (enumerator.MoveNext())
      {
        object current = enumerator.Current;
        if (this.Location.Cache.GetStatus((object) PXResult.Unwrap<PX.Objects.CR.Location>(current)) == PXEntryStatus.Inserted)
          return this.First.Press(adapter);
        return (IEnumerable) new object[1]{ current };
      }
    }
    finally
    {
      if (enumerator is IDisposable disposable)
        disposable.Dispose();
    }
    return (IEnumerable) new object[0];
  }

  public VendorLocationMaint()
  {
    this.Location.Join<LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>>>>();
    this.Location.WhereAnd<Where<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.AP.Vendor.bAccountID, IsNotNull, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>();
    this.Views.Caches.Remove(typeof (LocationAPPaymentInfo));
    this.FieldDefaulting.AddHandler<BAccountR.type>((PXFieldDefaulting) ((sender, e) =>
    {
      if (e.Row == null)
        return;
      e.NewValue = (object) "VE";
    }));
  }

  public override void Persist()
  {
    LocationAPAccountSub current = this.APAccountSubLocation.Current;
    if (current != null)
    {
      ValidationHelper<ValidationHelper>.SetErrorEmptyIfNull<LocationAPAccountSub.vAPAccountID>(this.APAccountSubLocation.Cache, (object) current, (object) current.VAPAccountID);
      ValidationHelper<ValidationHelper>.SetErrorEmptyIfNull<LocationAPAccountSub.vAPSubID>(this.APAccountSubLocation.Cache, (object) current, (object) current.VAPSubID);
    }
    base.Persist();
    this.APPaymentInfoLocation.Cache.Clear();
    this.APAccountSubLocation.Cache.Clear();
  }

  [PXUIField(DisplayName = "View on Map", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable ViewRemitOnMap(PXAdapter adapter)
  {
    BAccountUtility.ViewOnMap(this.RemitAddress.Current);
    return adapter.Get();
  }

  [PXDefault(typeof (PX.Objects.CR.Location.bAccountID))]
  [PX.Objects.AP.Vendor(typeof (Search<PX.Objects.AP.Vendor.bAccountID, Where<Where<PX.Objects.AP.Vendor.type, Equal<BAccountType.vendorType>, Or<PX.Objects.AP.Vendor.type, Equal<BAccountType.combinedType>>>>>), IsKey = true, TabOrder = 0)]
  [PXParent(typeof (PX.Data.Select<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>>>))]
  protected override void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Location.bAccountID> e)
  {
  }

  [PXUIField(DisplayName = "Default Branch")]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Location.vBranchID> e)
  {
  }

  [LocationRaw(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>>), null, IsKey = true, Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Location ID")]
  [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
  protected new virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Location.locationCD> e)
  {
  }

  protected override void Location_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PX.Objects.CR.BAccount baccount = (PX.Objects.CR.BAccount) PXParentAttribute.SelectParent(sender, e.Row, typeof (PX.Objects.CR.BAccount));
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.isAPAccountSameAsMain>(sender, e.Row, baccount != null && !object.Equals((object) baccount.DefLocationID, (object) ((PX.Objects.CR.Location) e.Row).LocationID));
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.isAPPaymentInfoSameAsMain>(sender, e.Row, baccount != null && !object.Equals((object) baccount.DefLocationID, (object) ((PX.Objects.CR.Location) e.Row).LocationID));
    PXUIFieldAttribute.SetVisible<PX.Objects.CR.Location.vSiteID>(sender, e.Row, baccount == null || !baccount.IsBranch.GetValueOrDefault());
    PXUIFieldAttribute.SetVisible<PX.Objects.CR.LocationBranchSettings.vSiteID>(this.LocationBranchSettings.Cache, (object) null, baccount != null && baccount.IsBranch.GetValueOrDefault());
    base.Location_RowSelected(sender, e);
  }

  protected virtual void Location_VTaxZoneID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    PX.Objects.CR.Location row = (PX.Objects.CR.Location) e.Row;
    if (row == null || !(row.LocType == "VE") || row.CTaxZoneID != null && !((string) e.OldValue == row.CTaxZoneID))
      return;
    this.LocationCurrent.Cache.SetValue<PX.Objects.CR.Location.cTaxZoneID>((object) row, (object) row.VTaxZoneID);
  }

  protected virtual void Location_IsAPAccountSameAsMain_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PX.Objects.CR.Location row = (PX.Objects.CR.Location) e.Row;
    bool? accountSameAsMain = row.IsAPAccountSameAsMain;
    bool flag = false;
    if (accountSameAsMain.GetValueOrDefault() == flag & accountSameAsMain.HasValue)
    {
      LocationAPAccountSub locationApAccountSub1 = (LocationAPAccountSub) this.APAccountSubLocation.Select();
      row.VAPAccountID = locationApAccountSub1.VAPAccountID;
      row.VAPSubID = locationApAccountSub1.VAPSubID;
      row.VRetainageAcctID = locationApAccountSub1.VRetainageAcctID;
      row.VRetainageSubID = locationApAccountSub1.VRetainageSubID;
      row.VAPAccountLocationID = row.LocationID;
      LocationAPAccountSub locationApAccountSub2 = new LocationAPAccountSub();
      locationApAccountSub2.BAccountID = row.BAccountID;
      locationApAccountSub2.LocationID = row.LocationID;
      locationApAccountSub2.VAPAccountID = row.VAPAccountID;
      locationApAccountSub2.VAPSubID = row.VAPSubID;
      locationApAccountSub2.VRetainageAcctID = row.VRetainageAcctID;
      locationApAccountSub2.VRetainageSubID = row.VRetainageSubID;
      this.BusinessAccount.Cache.Current = (object) (PX.Objects.CR.BAccount) PXParentAttribute.SelectParent(sender, e.Row, typeof (PX.Objects.CR.BAccount));
      this.APAccountSubLocation.Insert(locationApAccountSub2);
    }
    if (!row.IsAPAccountSameAsMain.GetValueOrDefault())
      return;
    row.VAPAccountID = new int?();
    row.VAPSubID = new int?();
    row.VRetainageAcctID = new int?();
    row.VRetainageSubID = new int?();
    PX.Objects.CR.BAccount baccount = (PX.Objects.CR.BAccount) PXParentAttribute.SelectParent(sender, e.Row, typeof (PX.Objects.CR.BAccount));
    if (baccount == null)
      return;
    row.VAPAccountLocationID = baccount.DefLocationID;
  }

  protected virtual void Location_IsAPPaymentInfoSameAsMain_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PX.Objects.CR.Location row = (PX.Objects.CR.Location) e.Row;
    bool? paymentInfoSameAsMain = row.IsAPPaymentInfoSameAsMain;
    bool flag = false;
    if (paymentInfoSameAsMain.GetValueOrDefault() == flag & paymentInfoSameAsMain.HasValue)
    {
      LocationAPPaymentInfo locationApPaymentInfo1 = (LocationAPPaymentInfo) this.APPaymentInfoLocation.Select();
      row.VPaymentInfoLocationID = row.LocationID;
      row.VPaymentMethodID = locationApPaymentInfo1.VPaymentMethodID;
      row.VCashAccountID = locationApPaymentInfo1.VCashAccountID;
      row.VPaymentByType = locationApPaymentInfo1.VPaymentByType;
      row.VPaymentLeadTime = locationApPaymentInfo1.VPaymentLeadTime;
      row.VSeparateCheck = locationApPaymentInfo1.VSeparateCheck;
      LocationAPPaymentInfo locationApPaymentInfo2 = new LocationAPPaymentInfo();
      locationApPaymentInfo2.BAccountID = row.BAccountID;
      locationApPaymentInfo2.LocationID = row.LocationID;
      locationApPaymentInfo2.VPaymentMethodID = row.VPaymentMethodID;
      locationApPaymentInfo2.VCashAccountID = row.VCashAccountID;
      locationApPaymentInfo2.VPaymentByType = row.VPaymentByType;
      locationApPaymentInfo2.VPaymentLeadTime = row.VPaymentLeadTime;
      locationApPaymentInfo2.VSeparateCheck = row.VSeparateCheck;
      foreach (PXResult<VendorPaymentMethodDetail> pxResult in this.PaymentDetails.Select((object) locationApPaymentInfo1.BAccountID, (object) locationApPaymentInfo1.LocationID, (object) locationApPaymentInfo1.VPaymentMethodID))
      {
        VendorPaymentMethodDetail copy = PXCache<VendorPaymentMethodDetail>.CreateCopy((VendorPaymentMethodDetail) pxResult);
        copy.LocationID = locationApPaymentInfo2.LocationID;
        this.PaymentDetails.Insert(copy);
      }
      this.BusinessAccount.Cache.Current = (object) (PX.Objects.CR.BAccount) PXParentAttribute.SelectParent(sender, e.Row, typeof (PX.Objects.CR.BAccount));
      this.APPaymentInfoLocation.Insert(locationApPaymentInfo2);
    }
    else
    {
      if (!row.IsAPPaymentInfoSameAsMain.GetValueOrDefault())
        return;
      foreach (PXResult<VendorPaymentMethodDetail> pxResult in this.PaymentDetails.Select((object) row.BAccountID, (object) row.LocationID, (object) row.VPaymentMethodID))
        this.PaymentDetails.Delete((VendorPaymentMethodDetail) pxResult);
      PX.Objects.CR.BAccount baccount = (PX.Objects.CR.BAccount) PXParentAttribute.SelectParent(sender, e.Row, typeof (PX.Objects.CR.BAccount));
      if (baccount == null)
        return;
      row.VPaymentInfoLocationID = baccount.DefLocationID;
      PX.Objects.CR.Location location = (PX.Objects.CR.Location) PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select(sender.Graph, (object) baccount.BAccountID, (object) baccount.DefLocationID);
      row.VPaymentMethodID = (string) null;
      row.VCashAccountID = new int?();
      row.VPaymentByType = new int?(0);
      row.VPaymentLeadTime = new short?((short) 0);
      row.VSeparateCheck = new bool?(false);
    }
  }

  protected virtual void Location_CBranchID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) null;
    e.Cancel = true;
  }

  protected override void Location_VBranchID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
  }

  protected virtual void LocationAPAccountSub_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null || this.Location.Current == null)
      return;
    PXUIFieldAttribute.SetEnabled(sender, e.Row, object.Equals((object) this.Location.Current.LocationID, (object) this.Location.Current.VAPAccountLocationID));
  }

  protected virtual void LocationAPAccountSub_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    LocationAPAccountSub row = (LocationAPAccountSub) e.Row;
    if (!sender.ObjectsEqual<LocationAPAccountSub.vAPAccountID, LocationAPAccountSub.vAPSubID>(e.Row, e.OldRow))
    {
      PX.Objects.CR.Location current = this.Location.Current;
      if (((PXFieldState) sender.GetStateExt<LocationAPAccountSub.vAPAccountID>(e.Row)).ErrorLevel < PXErrorLevel.Error)
        current.VAPAccountID = row.VAPAccountID;
      current.VAPSubID = row.VAPSubID;
      this.Location.Cache.MarkUpdated((object) current);
    }
    if (sender.ObjectsEqual<LocationAPAccountSub.vRetainageAcctID, LocationAPAccountSub.vRetainageSubID>(e.Row, e.OldRow))
      return;
    PX.Objects.CR.Location current1 = this.Location.Current;
    if (((PXFieldState) sender.GetStateExt<LocationAPAccountSub.vRetainageAcctID>(e.Row)).ErrorLevel < PXErrorLevel.Error)
      current1.VRetainageAcctID = row.VRetainageAcctID;
    current1.VRetainageSubID = row.VRetainageSubID;
    if (this.Location.Cache.GetStatus((object) current1) != PXEntryStatus.Notchanged)
      return;
    this.Location.Cache.SetStatus((object) current1, PXEntryStatus.Updated);
  }

  protected virtual void LocationAPPaymentInfo_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    LocationAPPaymentInfo row = (LocationAPPaymentInfo) e.Row;
    if (this.Location.Current == null)
      return;
    bool isEnabled = object.Equals((object) this.Location.Current.LocationID, (object) this.Location.Current.VPaymentInfoLocationID);
    bool flag = row != null && !string.IsNullOrEmpty(row.VPaymentMethodID);
    PXUIFieldAttribute.SetEnabled(sender, e.Row, isEnabled);
    PXUIFieldAttribute.SetEnabled<LocationAPPaymentInfo.vCashAccountID>(sender, e.Row, isEnabled & flag);
  }

  protected virtual void LocationAPPaymentInfo_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    LocationAPPaymentInfo row = (LocationAPPaymentInfo) e.Row;
    if (sender.ObjectsEqual<LocationAPPaymentInfo.vPaymentMethodID, LocationAPPaymentInfo.vCashAccountID, LocationAPPaymentInfo.vPaymentByType, LocationAPPaymentInfo.vPaymentLeadTime, LocationAPPaymentInfo.vSeparateCheck>(e.Row, e.OldRow))
      return;
    PX.Objects.CR.Location current = this.Location.Current;
    current.VPaymentMethodID = row.VPaymentMethodID;
    current.VCashAccountID = row.VCashAccountID;
    current.VPaymentByType = row.VPaymentByType;
    current.VPaymentLeadTime = row.VPaymentLeadTime;
    current.VSeparateCheck = row.VSeparateCheck;
    this.Location.Cache.MarkUpdated((object) current);
    sender.Graph.Caches[typeof (PX.Objects.CR.Location)].IsDirty = true;
  }

  protected virtual void LocationAPPaymentInfo_VPaymentMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    string oldValue = (string) e.OldValue;
    LocationAPPaymentInfo row = (LocationAPPaymentInfo) e.Row;
    if (!string.IsNullOrEmpty(oldValue))
      this.ClearPaymentDetails((IPaymentTypeDetailMaster) e.Row, oldValue, true);
    this.FillPaymentDetails((IPaymentTypeDetailMaster) e.Row);
    sender.SetDefaultExt<LocationAPPaymentInfo.vCashAccountID>(e.Row);
  }

  protected virtual void LocationAPPaymentInfo_VCashAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) null;
    e.Cancel = true;
  }

  protected virtual void VendorPaymentMethodDetail_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    if (e.Row == null || this.Location.Current == null)
      return;
    PXUIFieldAttribute.SetEnabled(sender, e.Row, object.Equals((object) this.Location.Current.LocationID, (object) this.Location.Current.VPaymentInfoLocationID));
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<VendorPaymentMethodDetail, VendorPaymentMethodDetail.detailValue> e)
  {
    PaymentMethodDetailHelper.VendorDetailValueFieldSelecting((PXGraph) this, e);
  }

  protected virtual void Contact_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Contact row))
      return;
    PX.Objects.CR.BAccount account = BAccountUtility.FindAccount((PXGraph) this, row.BAccountID);
    bool flag = false;
    if (account != null)
    {
      int? contactId = row.ContactID;
      int? defContactId = account.DefContactID;
      flag = contactId.GetValueOrDefault() == defContactId.GetValueOrDefault() & contactId.HasValue == defContactId.HasValue;
    }
    if (flag || this.Location.Current == null || !object.Equals((object) this.Location.Current.VRemitContactID, (object) row.ContactID))
      return;
    PXUIFieldAttribute.SetEnabled(sender, e.Row, object.Equals((object) this.Location.Current.LocationID, (object) this.Location.Current.VPaymentInfoLocationID));
  }

  protected override void Address_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Address row))
      return;
    PX.Objects.CR.BAccount account = BAccountUtility.FindAccount((PXGraph) this, row.BAccountID);
    bool flag = false;
    if (account != null)
    {
      int? addressId = row.AddressID;
      int? defAddressId = account.DefAddressID;
      flag = addressId.GetValueOrDefault() == defAddressId.GetValueOrDefault() & addressId.HasValue == defAddressId.HasValue;
    }
    if (!flag && this.Location.Current != null && object.Equals((object) this.Location.Current.VRemitAddressID, (object) row.AddressID))
      PXUIFieldAttribute.SetEnabled(sender, e.Row, object.Equals((object) this.Location.Current.LocationID, (object) this.Location.Current.VPaymentInfoLocationID));
    else
      base.Address_RowSelected(sender, e);
  }

  protected virtual void Address_CountryID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    PX.Objects.CR.Address row = (PX.Objects.CR.Address) e.Row;
    if (!((string) e.OldValue != row.CountryID))
      return;
    row.State = (string) null;
  }

  protected virtual PX.Objects.CR.Address FindAddress(int? aId)
  {
    if (aId.HasValue)
    {
      foreach (PXResult<PX.Objects.CR.Address> pxResult in this.Addresses.Select())
      {
        PX.Objects.CR.Address address = (PX.Objects.CR.Address) pxResult;
        int? addressId = address.AddressID;
        int? nullable = aId;
        if (addressId.GetValueOrDefault() == nullable.GetValueOrDefault() & addressId.HasValue == nullable.HasValue)
          return address;
      }
    }
    return (PX.Objects.CR.Address) null;
  }

  protected virtual PX.Objects.CR.Contact FindContact(int? aId)
  {
    if (aId.HasValue)
    {
      foreach (PXResult<PX.Objects.CR.Contact> pxResult in this.Contacts.Select())
      {
        PX.Objects.CR.Contact contact = (PX.Objects.CR.Contact) pxResult;
        int? contactId = contact.ContactID;
        int? nullable = aId;
        if (contactId.GetValueOrDefault() == nullable.GetValueOrDefault() & contactId.HasValue == nullable.HasValue)
          return contact;
      }
    }
    return (PX.Objects.CR.Contact) null;
  }

  protected virtual void FillPaymentDetails(IPaymentTypeDetailMaster account)
  {
    if (account == null || string.IsNullOrEmpty(account.VPaymentMethodID))
      return;
    if ((PX.Objects.CA.PaymentMethod) PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, (object) account.VPaymentMethodID) == null)
      return;
    List<PaymentMethodDetail> paymentMethodDetailList = new List<PaymentMethodDetail>();
    foreach (PXResult<PaymentMethodDetail> pxResult1 in this.PaymentTypeDetails.Select((object) account.VPaymentMethodID))
    {
      PaymentMethodDetail paymentMethodDetail1 = (PaymentMethodDetail) pxResult1;
      VendorPaymentMethodDetail paymentMethodDetail2 = (VendorPaymentMethodDetail) null;
      foreach (PXResult<VendorPaymentMethodDetail> pxResult2 in this.PaymentDetails.Select((object) account.BAccountID, (object) account.LocationID, (object) account.VPaymentMethodID))
      {
        VendorPaymentMethodDetail paymentMethodDetail3 = (VendorPaymentMethodDetail) pxResult2;
        if (paymentMethodDetail3.DetailID == paymentMethodDetail1.DetailID)
        {
          paymentMethodDetail2 = paymentMethodDetail3;
          break;
        }
      }
      if (paymentMethodDetail2 == null)
        paymentMethodDetailList.Add(paymentMethodDetail1);
    }
    using (new ReadOnlyScope(new PXCache[1]
    {
      this.PaymentDetails.Cache
    }))
    {
      foreach (PaymentMethodDetail paymentMethodDetail in paymentMethodDetailList)
        this.PaymentDetails.Insert(new VendorPaymentMethodDetail()
        {
          BAccountID = account.BAccountID,
          LocationID = account.LocationID,
          PaymentMethodID = account.VPaymentMethodID,
          DetailID = paymentMethodDetail.DetailID
        });
      if (paymentMethodDetailList.Count <= 0)
        return;
      this.PaymentDetails.View.RequestRefresh();
    }
  }

  protected virtual void ClearPaymentDetails(
    IPaymentTypeDetailMaster account,
    string paymentTypeID,
    bool clearNewOnly)
  {
    foreach (PXResult<VendorPaymentMethodDetail> pxResult in this.PaymentDetails.Select((object) account.BAccountID, (object) account.LocationID, (object) paymentTypeID))
    {
      VendorPaymentMethodDetail paymentMethodDetail = (VendorPaymentMethodDetail) pxResult;
      bool flag = true;
      if (clearNewOnly)
        flag = this.PaymentDetails.Cache.GetStatus((object) paymentMethodDetail) == PXEntryStatus.Inserted;
      if (flag)
        this.PaymentDetails.Delete(paymentMethodDetail);
    }
  }

  protected override void EstablishCTaxZoneRule(Action<System.Type, bool> setCheck)
  {
    setCheck(typeof (PX.Objects.CR.Location.cTaxZoneID), false);
  }

  /// <exclude />
  public class LocationBAccountSharedContactOverrideGraphExt : 
    SharedChildOverrideGraphExt<VendorLocationMaint, VendorLocationMaint.LocationBAccountSharedContactOverrideGraphExt>
  {
    protected override CRParentChild<VendorLocationMaint, VendorLocationMaint.LocationBAccountSharedContactOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<VendorLocationMaint, VendorLocationMaint.LocationBAccountSharedContactOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Location.defContactID),
        IsOverrideRelated = typeof (PX.Objects.CR.Location.overrideContact)
      };
    }

    protected override SharedChildOverrideGraphExt<VendorLocationMaint, VendorLocationMaint.LocationBAccountSharedContactOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<VendorLocationMaint, VendorLocationMaint.LocationBAccountSharedContactOverrideGraphExt>.RelatedMapping(typeof (PX.Objects.AP.Vendor))
      {
        RelatedID = typeof (PX.Objects.AP.Vendor.bAccountID),
        ChildID = typeof (PX.Objects.AP.Vendor.defContactID)
      };
    }

    protected override CRParentChild<VendorLocationMaint, VendorLocationMaint.LocationBAccountSharedContactOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<VendorLocationMaint, VendorLocationMaint.LocationBAccountSharedContactOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Contact))
      {
        ChildID = typeof (PX.Objects.CR.Contact.contactID),
        RelatedID = typeof (PX.Objects.CR.Contact.bAccountID)
      };
    }
  }

  /// <exclude />
  public class LocationBAccountSharedAddressOverrideGraphExt : 
    SharedChildOverrideGraphExt<VendorLocationMaint, VendorLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>
  {
    protected override CRParentChild<VendorLocationMaint, VendorLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<VendorLocationMaint, VendorLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Location.defAddressID),
        IsOverrideRelated = typeof (PX.Objects.CR.Location.overrideAddress)
      };
    }

    protected override SharedChildOverrideGraphExt<VendorLocationMaint, VendorLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<VendorLocationMaint, VendorLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>.RelatedMapping(typeof (PX.Objects.AP.Vendor))
      {
        RelatedID = typeof (PX.Objects.AP.Vendor.bAccountID),
        ChildID = typeof (PX.Objects.AP.Vendor.defAddressID)
      };
    }

    protected override CRParentChild<VendorLocationMaint, VendorLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<VendorLocationMaint, VendorLocationMaint.LocationBAccountSharedAddressOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Address))
      {
        ChildID = typeof (PX.Objects.CR.Address.addressID),
        RelatedID = typeof (PX.Objects.CR.Address.bAccountID)
      };
    }
  }

  /// <exclude />
  public class LocationRemitSharedContactOverrideGraphExt : 
    SharedChildOverrideGraphExt<VendorLocationMaint, VendorLocationMaint.LocationRemitSharedContactOverrideGraphExt>
  {
    protected override CRParentChild<VendorLocationMaint, VendorLocationMaint.LocationRemitSharedContactOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<VendorLocationMaint, VendorLocationMaint.LocationRemitSharedContactOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Location.vRemitContactID),
        IsOverrideRelated = typeof (PX.Objects.CR.Location.overrideRemitContact)
      };
    }

    protected override SharedChildOverrideGraphExt<VendorLocationMaint, VendorLocationMaint.LocationRemitSharedContactOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<VendorLocationMaint, VendorLocationMaint.LocationRemitSharedContactOverrideGraphExt>.RelatedMapping(typeof (PX.Objects.AP.Vendor))
      {
        RelatedID = typeof (PX.Objects.AP.Vendor.bAccountID),
        ChildID = typeof (PX.Objects.AP.Vendor.defContactID)
      };
    }

    protected override CRParentChild<VendorLocationMaint, VendorLocationMaint.LocationRemitSharedContactOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<VendorLocationMaint, VendorLocationMaint.LocationRemitSharedContactOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Contact))
      {
        ChildID = typeof (PX.Objects.CR.Contact.contactID),
        RelatedID = typeof (PX.Objects.CR.Contact.bAccountID)
      };
    }
  }

  /// <exclude />
  public class LocationRemitSharedAddressOverrideGraphExt : 
    SharedChildOverrideGraphExt<VendorLocationMaint, VendorLocationMaint.LocationRemitSharedAddressOverrideGraphExt>
  {
    protected override CRParentChild<VendorLocationMaint, VendorLocationMaint.LocationRemitSharedAddressOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<VendorLocationMaint, VendorLocationMaint.LocationRemitSharedAddressOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Location.vRemitAddressID),
        IsOverrideRelated = typeof (PX.Objects.CR.Location.overrideRemitAddress)
      };
    }

    protected override SharedChildOverrideGraphExt<VendorLocationMaint, VendorLocationMaint.LocationRemitSharedAddressOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<VendorLocationMaint, VendorLocationMaint.LocationRemitSharedAddressOverrideGraphExt>.RelatedMapping(typeof (PX.Objects.AP.Vendor))
      {
        RelatedID = typeof (PX.Objects.AP.Vendor.bAccountID),
        ChildID = typeof (PX.Objects.AP.Vendor.defAddressID)
      };
    }

    protected override CRParentChild<VendorLocationMaint, VendorLocationMaint.LocationRemitSharedAddressOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<VendorLocationMaint, VendorLocationMaint.LocationRemitSharedAddressOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Address))
      {
        ChildID = typeof (PX.Objects.CR.Address.addressID),
        RelatedID = typeof (PX.Objects.CR.Address.bAccountID)
      };
    }
  }

  /// <exclude />
  public class VendorLocationMaintRemitAddressLookupExtension : 
    AddressLookupExtension<VendorLocationMaint, PX.Objects.CR.Location, PX.Objects.CR.Address>
  {
    protected override string AddressView => "RemitAddress";

    protected override string ViewOnMap => "viewRemitOnMap";
  }
}
