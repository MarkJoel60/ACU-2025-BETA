// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.LocationMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.CR;

public abstract class LocationMaint : PXGraph<
#nullable disable
LocationMaint>
{
  public PXSave<PX.Objects.CR.Location> Save;
  public PXAction<PX.Objects.CR.Location> cancel;
  public PXInsert<PX.Objects.CR.Location> Insert;
  public PXDelete<PX.Objects.CR.Location> Delete;
  public PXCopyPasteAction<PX.Objects.CR.Location> CopyPaste;
  public PXFirst<PX.Objects.CR.Location> First;
  public PXPrevious<PX.Objects.CR.Location> previous;
  public PXNext<PX.Objects.CR.Location> next;
  public PXLast<PX.Objects.CR.Location> Last;
  public PXAction<PX.Objects.CR.Location> viewOnMap;
  public PXAction<PX.Objects.CR.Location> validateAddresses;
  public PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Optional<PX.Objects.CR.Location.bAccountID>>>> Location;
  public PXSelect<BAccount> BusinessAccount;
  public PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<PX.Objects.CR.Location.locationID>>>>> LocationCurrent;
  public PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>>> OtherLocations;
  public PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Address.addressID, Equal<Current<PX.Objects.CR.Location.defAddressID>>>>> Address;
  public PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Contact.contactID, Equal<Current<PX.Objects.CR.Location.defContactID>>>>> Contact;
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (PX.Objects.CR.Location))]
  [PXViewDetailsButton(typeof (PX.Objects.CR.Location), typeof (Select<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Current<CROpportunity.contactID>>>>))]
  public PXSelectJoin<CROpportunity, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<CROpportunity.contactID>>, LeftJoin<CROpportunityProbability, On<CROpportunityProbability.stageCode, Equal<CROpportunity.stageID>>>>, Where<CROpportunity.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>, And<CROpportunity.locationID, Equal<Current<PX.Objects.CR.Location.locationID>>>>> Opportunities;
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (PX.Objects.CR.Location))]
  [PXViewDetailsButton(typeof (PX.Objects.CR.Location), typeof (Select<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Current<CRCase.contactID>>>>))]
  public PXSelect<CRCase, Where<CRCase.customerID, Equal<Current<PX.Objects.CR.Location.bAccountID>>, And<CRCase.locationID, Equal<Current<PX.Objects.CR.Location.locationID>>>>> Cases;
  public PXSelect<LocationARAccountSub, Where<LocationARAccountSub.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>, And<LocationARAccountSub.locationID, Equal<Current<PX.Objects.CR.Location.cARAccountLocationID>>>>> ARAccountSubLocation;
  [PXHidden]
  public PXSelect<CustSalesPeople> Salesperson;
  [PXViewName("Address")]
  public FbqlSelect<SelectFromBase<PX.Objects.CR.Address, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PX.Objects.CR.Address.addressID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.CR.Location.defAddressID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  PX.Objects.CR.Address>.View AddressCurrent;
  public PXSetup<PX.Objects.GL.Branch> Company;
  protected LocationValidator LocationValidator;
  private object _KeyToAbort;

  [PXUIField]
  [PXCancelButton]
  protected virtual IEnumerable Cancel(PXAdapter adapter)
  {
    int? nullable1 = ((PXSelectBase<PX.Objects.CR.Location>) this.Location).Current != null ? ((PXSelectBase<PX.Objects.CR.Location>) this.Location).Current.BAccountID : new int?();
    IEnumerator enumerator1 = ((PXAction) new PXCancel<PX.Objects.CR.Location>((PXGraph) this, nameof (Cancel))).Press(adapter).GetEnumerator();
    try
    {
      if (enumerator1.MoveNext())
      {
        PX.Objects.CR.Location location = PXResult.Unwrap<PX.Objects.CR.Location>(enumerator1.Current);
        if (!((PXGraph) this).IsImport && ((PXSelectBase) this.Location).Cache.GetStatus((object) location) == 2)
        {
          int? nullable2 = nullable1;
          int? baccountId = location.BAccountID;
          if (!(nullable2.GetValueOrDefault() == baccountId.GetValueOrDefault() & nullable2.HasValue == baccountId.HasValue) || string.IsNullOrEmpty(location.LocationCD))
          {
            IEnumerator enumerator2 = ((PXAction) this.First).Press(adapter).GetEnumerator();
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
            return (IEnumerable) new object[1]
            {
              (object) location
            };
          }
        }
        return (IEnumerable) new object[1]
        {
          (object) location
        };
      }
    }
    finally
    {
      if (enumerator1 is IDisposable disposable)
        disposable.Dispose();
    }
    return (IEnumerable) new object[0];
  }

  [PXUIField]
  [PXPreviousButton]
  protected virtual IEnumerable Previous(PXAdapter adapter)
  {
    IEnumerator enumerator = ((PXAction) new PXPrevious<PX.Objects.CR.Location>((PXGraph) this, "Prev")).Press(adapter).GetEnumerator();
    try
    {
      if (enumerator.MoveNext())
      {
        object current = enumerator.Current;
        if (((PXSelectBase) this.Location).Cache.GetStatus((object) PXResult.Unwrap<PX.Objects.CR.Location>(current)) == 2)
          return ((PXAction) this.Last).Press(adapter);
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

  [PXUIField]
  [PXNextButton]
  protected virtual IEnumerable Next(PXAdapter adapter)
  {
    IEnumerator enumerator = ((PXAction) new PXNext<PX.Objects.CR.Location>((PXGraph) this, nameof (Next))).Press(adapter).GetEnumerator();
    try
    {
      if (enumerator.MoveNext())
      {
        object current = enumerator.Current;
        if (((PXSelectBase) this.Location).Cache.GetStatus((object) PXResult.Unwrap<PX.Objects.CR.Location>(current)) == 2)
          return ((PXAction) this.First).Press(adapter);
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

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewOnMap(PXAdapter adapter)
  {
    BAccountUtility.ViewOnMap(((PXSelectBase<PX.Objects.CR.Address>) this.Address).Current);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    PX.Objects.CR.Location current1 = ((PXSelectBase<PX.Objects.CR.Location>) this.LocationCurrent).Current;
    if (current1 != null)
    {
      BAccount account = BAccountUtility.FindAccount((PXGraph) this, current1.BAccountID);
      int num;
      if (account != null)
      {
        int? defAddressId1 = account.DefAddressID;
        int? defAddressId2 = current1.DefAddressID;
        num = !(defAddressId1.GetValueOrDefault() == defAddressId2.GetValueOrDefault() & defAddressId1.HasValue == defAddressId2.HasValue) ? 1 : 0;
      }
      else
        num = 0;
      bool flag1 = num != 0;
      PX.Objects.CR.Address current2 = ((PXSelectBase<PX.Objects.CR.Address>) this.Address).Current;
      if (current2 != null && flag1)
      {
        bool? isValidated = current2.IsValidated;
        bool flag2 = false;
        if (isValidated.GetValueOrDefault() == flag2 & isValidated.HasValue)
          PXAddressValidator.Validate<PX.Objects.CR.Address>((PXGraph) this, current2, true, true);
      }
    }
    return adapter.Get();
  }

  public LocationMaint()
  {
    ((PXSelectBase<PX.Objects.CR.Location>) this.Location).Join<LeftJoin<BAccount, On<BAccount.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>>>>();
    ((PXSelectBase<PX.Objects.CR.Location>) this.Location).WhereAnd<Where<BAccount.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<BAccount.bAccountID, IsNotNull, And<Match<BAccount, Current<AccessInfo.userName>>>>>>();
    PXUIFieldAttribute.SetDisplayName<PX.Objects.CR.Contact.salutation>(((PXSelectBase) this.Contact).Cache, "Attention");
    this.LocationValidator = new LocationValidator();
    if (!((PXSelectBase<PX.Objects.GL.Branch>) this.Company).Current.BAccountID.HasValue)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (PX.Objects.GL.Branch), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Company Branches")
      });
    PXUIFieldAttribute.SetDisplayName<PX.Objects.CR.Location.locationCD>(((PXSelectBase) this.Location).Cache, "Location ID");
    PXUIFieldAttribute.SetDisplayName<PX.Objects.CR.Location.cLeadTime>(((PXSelectBase) this.LocationCurrent).Cache, "Lead Time (Days)");
    PXCache cach = ((PXGraph) this).Caches[typeof (BAccount)];
    PXUIFieldAttribute.SetDisplayName<BAccount.acctCD>(cach, "Business Account");
    PXUIFieldAttribute.SetDisplayName<BAccount.acctName>(cach, "Business Account Name");
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (PX.Objects.CR.Location.bAccountID))]
  [PXUIField(DisplayName = "Business Account", TabOrder = 0)]
  [PXDimensionSelector("BIZACCT", typeof (Search2<BAccount.bAccountID, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<BAccount.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<BAccount.defContactID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<BAccount.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<BAccount.defAddressID>>>>>, Where<BAccount.type, Equal<BAccountType.customerType>, Or<BAccount.type, Equal<BAccountType.prospectType>, Or<BAccount.type, Equal<BAccountType.vendorType>, Or<BAccount.type, Equal<BAccountType.combinedType>>>>>>), typeof (BAccount.acctCD), new System.Type[] {typeof (BAccount.acctCD), typeof (BAccount.acctName), typeof (BAccount.type), typeof (BAccount.classID), typeof (BAccount.status), typeof (PX.Objects.CR.Contact.phone1), typeof (PX.Objects.CR.Address.city), typeof (PX.Objects.CR.Address.countryID)}, DescriptionField = typeof (BAccount.acctName))]
  [PXParent(typeof (Select<BAccount, Where<BAccount.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Location.bAccountID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (LocationRawAttribute), "DescriptionField", typeof (PX.Objects.CR.Location.descr))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Location.locationCD> e)
  {
  }

  protected virtual void Location_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.isARAccountSameAsMain>(cache, e.Row, parent != null && !object.Equals((object) parent.DefLocationID, (object) ((PX.Objects.CR.Location) e.Row).LocationID));
    bool flag1 = false;
    bool flag2 = row.LocType == "CU" || row.LocType == "VC";
    bool flag3 = row.LocType == "CP";
    bool? nullable;
    if (row.LocType == "VE" || row.LocType == "VC")
    {
      flag1 = true;
      PX.Objects.AP.Vendor byId = VendorMaint.FindByID((PXGraph) this, row.BAccountID);
      if (byId != null)
      {
        nullable = byId.TaxAgency;
        bool valueOrDefault = nullable.GetValueOrDefault();
        PXUIFieldAttribute.SetRequired<PX.Objects.CR.Location.vExpenseAcctID>(cache, valueOrDefault);
        PXUIFieldAttribute.SetRequired<PX.Objects.CR.Location.vExpenseSubID>(cache, valueOrDefault);
      }
    }
    PXCache pxCache = cache;
    nullable = row.IsDefault;
    int num = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.status>(pxCache, (object) null, num != 0);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.vTaxZoneID>(cache, (object) null, flag1 | flag3);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.vExpenseAcctID>(cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.vExpenseSubID>(cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.vRetainageAcctID>(cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.vRetainageSubID>(cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.vDiscountAcctID>(cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.vDiscountSubID>(cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.vLeadTime>(cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.vBranchID>(cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.vCarrierID>(cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.vFOBPointID>(cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.vShipTermsID>(cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.cTaxZoneID>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.cAvalaraCustomerUsageType>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.cSalesAcctID>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.cSalesSubID>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.cSalesAcctID>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.cRetainageAcctID>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.cRetainageSubID>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.cDiscountAcctID>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.cDiscountSubID>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.cFreightAcctID>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.cFreightSubID>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.cBranchID>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.cCarrierID>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.cFOBPointID>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.cShipTermsID>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.cMPSalesSubID>(cache, (object) null, flag3);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.cMPExpenseSubID>(cache, (object) null, flag3);
    bool? isDefault = cache.GetValueOriginal<PX.Objects.CR.Location.isDefault>((object) row) as bool?;
    PXCacheEx.AdjustUI(cache, (object) row).For<PX.Objects.CR.Location.isDefault>((Action<PXUIFieldAttribute>) (field => field.Enabled = !isDefault.GetValueOrDefault()));
    bool flag4 = false;
    if (row.CCarrierID != null)
    {
      Carrier carrier = Carrier.PK.Find((PXGraph) this, row.CCarrierID);
      if (carrier != null)
      {
        nullable = carrier.IsExternal;
        if (nullable.GetValueOrDefault() && !string.IsNullOrEmpty(carrier.CarrierPluginID))
          flag4 = CarrierPluginMaint.GetCarrierPluginAttributes((PXGraph) this, carrier.CarrierPluginID).Contains("COLLECT");
      }
    }
    PXUIFieldAttribute.SetVisible<PX.Objects.CR.Location.cGroundCollect>(cache, (object) row, flag4);
    this.EstablishCTaxZoneRule((Action<System.Type, bool>) ((field, isRequired) => PXUIFieldAttribute.SetRequired(((PXSelectBase) this.Location).Cache, field.Name, isRequired)));
    this.EstablishVTaxZoneRule((Action<System.Type, bool>) ((field, isRequired) => PXUIFieldAttribute.SetRequired(((PXSelectBase) this.Location).Cache, field.Name, isRequired)));
  }

  protected virtual void Location_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    PX.Objects.CR.Location row = e.Row as PX.Objects.CR.Location;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    int? defLocationId = parent.DefLocationID;
    int? locationId = row.LocationID;
    if (defLocationId.GetValueOrDefault() == locationId.GetValueOrDefault() & defLocationId.HasValue == locationId.HasValue)
      throw new PXException("Default Business Account Location cannot be deleted.");
  }

  protected virtual void Location_LocType_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    switch (parent.Type)
    {
      case "VE":
        e.NewValue = (object) "VE";
        break;
      case "CU":
      case "EC":
        e.NewValue = (object) "CU";
        break;
      case "VC":
        e.NewValue = (object) "VC";
        break;
      default:
        e.NewValue = (object) "CP";
        break;
    }
  }

  protected virtual void Location_DefAddressID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    e.NewValue = (object) parent.DefAddressID;
  }

  protected virtual void Location_DefContactID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    e.NewValue = (object) parent.DefContactID;
  }

  protected virtual void Location_CARAccountLocationID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    e.NewValue = (object) parent.DefLocationID;
  }

  protected virtual void Location_VAPAccountLocationID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    e.NewValue = (object) parent.DefLocationID;
  }

  protected virtual void Location_VPaymentInfoLocationID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    e.NewValue = (object) parent.DefLocationID;
  }

  protected virtual void Location_VRemitAddressID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    e.NewValue = (object) parent.DefAddressID;
  }

  protected virtual void Location_VRemitContactID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    e.NewValue = (object) parent.DefContactID;
  }

  protected virtual void Location_VExpenseAcctID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    int? nullable1 = row.BAccountID;
    if (!nullable1.HasValue)
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    nullable1 = parent.DefLocationID;
    if (!nullable1.HasValue || !(parent.Type == "VE") && !(parent.Type == "VC"))
      return;
    nullable1 = row.LocationID;
    int? nullable2 = parent.DefLocationID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location>.Config>.Search<PX.Objects.CR.Location.locationID, PX.Objects.CR.Location.bAccountID>((PXGraph) this, (object) parent.DefLocationID, (object) parent.BAccountID, Array.Empty<object>()));
    if (location == null)
      return;
    nullable2 = location.VExpenseAcctID;
    if (!nullable2.HasValue)
      return;
    e.NewValue = (object) location.VExpenseAcctID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void Location_VExpenseSubID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    int? nullable1 = row.BAccountID;
    if (!nullable1.HasValue)
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    nullable1 = parent.DefLocationID;
    if (!nullable1.HasValue || !(parent.Type == "VE") && !(parent.Type == "VC"))
      return;
    nullable1 = row.LocationID;
    int? nullable2 = parent.DefLocationID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location>.Config>.Search<PX.Objects.CR.Location.locationID, PX.Objects.CR.Location.bAccountID>((PXGraph) this, (object) parent.DefLocationID, (object) parent.BAccountID, Array.Empty<object>()));
    if (location == null)
      return;
    nullable2 = location.VExpenseSubID;
    if (!nullable2.HasValue)
      return;
    e.NewValue = (object) location.VExpenseSubID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.vDiscountAcctID> e)
  {
    PX.Objects.CR.Location defaultLocation = this.GetDefaultLocation(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.vDiscountAcctID>>) e).Cache, e.Row);
    if (defaultLocation == null || !defaultLocation.VDiscountAcctID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.vDiscountAcctID>, PX.Objects.CR.Location, object>) e).NewValue = (object) defaultLocation.VDiscountAcctID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.vDiscountAcctID>>) e).Cancel = true;
  }

  protected void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.vDiscountSubID> e)
  {
    PX.Objects.CR.Location defaultLocation = this.GetDefaultLocation(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.vDiscountSubID>>) e).Cache, e.Row);
    if (defaultLocation == null || !defaultLocation.VDiscountSubID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.vDiscountSubID>, PX.Objects.CR.Location, object>) e).NewValue = (object) defaultLocation.VDiscountSubID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.vDiscountSubID>>) e).Cancel = true;
  }

  protected virtual void Location_CSalesAcctID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    int? nullable1 = row.BAccountID;
    if (!nullable1.HasValue)
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    nullable1 = parent.DefLocationID;
    if (!nullable1.HasValue || !(parent.Type == "CU") && !(parent.Type == "VC"))
      return;
    nullable1 = row.LocationID;
    int? nullable2 = parent.DefLocationID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location>.Config>.Search<PX.Objects.CR.Location.locationID, PX.Objects.CR.Location.bAccountID>((PXGraph) this, (object) parent.DefLocationID, (object) parent.BAccountID, Array.Empty<object>()));
    if (location == null)
      return;
    nullable2 = location.CSalesAcctID;
    if (!nullable2.HasValue)
      return;
    e.NewValue = (object) location.CSalesAcctID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void Location_CSalesSubID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    int? nullable1 = row.BAccountID;
    if (!nullable1.HasValue)
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    nullable1 = parent.DefLocationID;
    if (!nullable1.HasValue || !(parent.Type == "CU") && !(parent.Type == "VC"))
      return;
    nullable1 = row.LocationID;
    int? nullable2 = parent.DefLocationID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location>.Config>.Search<PX.Objects.CR.Location.locationID, PX.Objects.CR.Location.bAccountID>((PXGraph) this, (object) parent.DefLocationID, (object) parent.BAccountID, Array.Empty<object>()));
    if (location == null)
      return;
    nullable2 = location.CSalesSubID;
    if (!nullable2.HasValue)
      return;
    e.NewValue = (object) location.CSalesSubID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cARAccountID> e)
  {
    PX.Objects.CR.Location defaultLocation = this.GetDefaultLocation(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cARAccountID>>) e).Cache, e.Row);
    if (defaultLocation == null || !defaultLocation.CARAccountID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cARAccountID>, PX.Objects.CR.Location, object>) e).NewValue = (object) defaultLocation.CARAccountID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cARAccountID>>) e).Cancel = true;
  }

  protected void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cARSubID> e)
  {
    PX.Objects.CR.Location defaultLocation = this.GetDefaultLocation(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cARSubID>>) e).Cache, e.Row);
    if (defaultLocation == null || !defaultLocation.CARSubID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cARSubID>, PX.Objects.CR.Location, object>) e).NewValue = (object) defaultLocation.CARSubID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cARSubID>>) e).Cancel = true;
  }

  protected void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cDiscountAcctID> e)
  {
    PX.Objects.CR.Location defaultLocation = this.GetDefaultLocation(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cDiscountAcctID>>) e).Cache, e.Row);
    if (defaultLocation == null || !defaultLocation.CDiscountAcctID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cDiscountAcctID>, PX.Objects.CR.Location, object>) e).NewValue = (object) defaultLocation.CDiscountAcctID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cDiscountAcctID>>) e).Cancel = true;
  }

  protected void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cDiscountSubID> e)
  {
    PX.Objects.CR.Location defaultLocation = this.GetDefaultLocation(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cDiscountSubID>>) e).Cache, e.Row);
    if (defaultLocation == null || !defaultLocation.CDiscountSubID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cDiscountSubID>, PX.Objects.CR.Location, object>) e).NewValue = (object) defaultLocation.CDiscountSubID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cDiscountSubID>>) e).Cancel = true;
  }

  protected void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cFreightAcctID> e)
  {
    PX.Objects.CR.Location defaultLocation = this.GetDefaultLocation(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cFreightAcctID>>) e).Cache, e.Row);
    if (defaultLocation == null || !defaultLocation.CFreightAcctID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cFreightAcctID>, PX.Objects.CR.Location, object>) e).NewValue = (object) defaultLocation.CFreightAcctID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cFreightAcctID>>) e).Cancel = true;
  }

  protected void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cFreightSubID> e)
  {
    PX.Objects.CR.Location defaultLocation = this.GetDefaultLocation(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cFreightSubID>>) e).Cache, e.Row);
    if (defaultLocation == null || !defaultLocation.CFreightSubID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cFreightSubID>, PX.Objects.CR.Location, object>) e).NewValue = (object) defaultLocation.CFreightSubID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Location, PX.Objects.CR.Location.cFreightSubID>>) e).Cancel = true;
  }

  protected virtual void Location_CPriceClassID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    int? nullable = row.BAccountID;
    if (!nullable.HasValue)
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    nullable = parent.DefLocationID;
    if (!nullable.HasValue || !(parent.Type == "CU") && !(parent.Type == "VC"))
      return;
    nullable = row.LocationID;
    int? defLocationId = parent.DefLocationID;
    if (nullable.GetValueOrDefault() == defLocationId.GetValueOrDefault() & nullable.HasValue == defLocationId.HasValue)
      return;
    PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location>.Config>.Search<PX.Objects.CR.Location.locationID, PX.Objects.CR.Location.bAccountID>((PXGraph) this, (object) parent.DefLocationID, (object) parent.BAccountID, Array.Empty<object>()));
    if (location == null || location.CPriceClassID == null)
      return;
    e.NewValue = (object) location.CPriceClassID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void Location_VTaxZoneID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    int? nullable = row.BAccountID;
    if (!nullable.HasValue)
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    nullable = parent.DefLocationID;
    if (!nullable.HasValue || !(parent.Type == "VE") && !(parent.Type == "VC"))
      return;
    nullable = row.LocationID;
    int? defLocationId = parent.DefLocationID;
    if (nullable.GetValueOrDefault() == defLocationId.GetValueOrDefault() & nullable.HasValue == defLocationId.HasValue)
      return;
    VendorClass vendorClass = PXResultset<VendorClass>.op_Implicit(PXSelectBase<VendorClass, PXSelect<VendorClass>.Config>.Search<VendorClass.vendorClassID>((PXGraph) this, (object) PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor>.Config>.Search<PX.Objects.AP.Vendor.bAccountID>((PXGraph) this, (object) parent.BAccountID, Array.Empty<object>())).VendorClassID, Array.Empty<object>()));
    if (vendorClass == null || vendorClass.TaxZoneID == null)
      return;
    e.NewValue = (object) vendorClass.TaxZoneID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void Location_VTaxCalcMode_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    int? nullable = row.BAccountID;
    if (!nullable.HasValue)
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    nullable = parent.DefLocationID;
    if (!nullable.HasValue || !(parent.Type == "VE") && !(parent.Type == "VC"))
      return;
    nullable = row.LocationID;
    int? defLocationId = parent.DefLocationID;
    if (nullable.GetValueOrDefault() == defLocationId.GetValueOrDefault() & nullable.HasValue == defLocationId.HasValue)
      return;
    VendorClass vendorClass = PXResultset<VendorClass>.op_Implicit(PXSelectBase<VendorClass, PXSelect<VendorClass>.Config>.Search<VendorClass.vendorClassID>((PXGraph) this, (object) PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor>.Config>.Search<PX.Objects.AP.Vendor.bAccountID>((PXGraph) this, (object) parent.BAccountID, Array.Empty<object>())).VendorClassID, Array.Empty<object>()));
    if (vendorClass == null || vendorClass.TaxCalcMode == null)
      return;
    e.NewValue = (object) vendorClass.TaxCalcMode;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void Location_CTaxZoneID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    int? nullable = row.BAccountID;
    if (!nullable.HasValue)
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    nullable = parent.DefLocationID;
    if (!nullable.HasValue || !(parent.Type == "CU") && !(parent.Type == "VC"))
      return;
    nullable = row.LocationID;
    int? defLocationId = parent.DefLocationID;
    if (nullable.GetValueOrDefault() == defLocationId.GetValueOrDefault() & nullable.HasValue == defLocationId.HasValue)
      return;
    CustomerClass customerClass = PXResultset<CustomerClass>.op_Implicit(PXSelectBase<CustomerClass, PXSelect<CustomerClass>.Config>.Search<CustomerClass.customerClassID>((PXGraph) this, (object) PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer>.Config>.Search<Customer.bAccountID>((PXGraph) this, (object) parent.BAccountID, Array.Empty<object>())).CustomerClassID, Array.Empty<object>()));
    if (customerClass == null || customerClass.TaxZoneID == null)
      return;
    e.NewValue = (object) customerClass.TaxZoneID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.CR.Location.cTaxZoneID> e)
  {
    PX.Objects.CR.Location row = (PX.Objects.CR.Location) e.Row;
    if (row == null || !(row.LocType == "VE") && !(row.LocType == "VC") || row.VTaxZoneID != null && !((string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.CR.Location.cTaxZoneID>, object, object>) e).OldValue == row.VTaxZoneID) || !((bool?) PXResultset<VendorClass>.op_Implicit(PXSelectBase<VendorClass, PXSelect<VendorClass>.Config>.Search<VendorClass.vendorClassID>((PXGraph) this, (object) PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor>.Config>.Search<PX.Objects.AP.Vendor.bAccountID>((PXGraph) this, (object) row.BAccountID, Array.Empty<object>())).VendorClassID, Array.Empty<object>()))?.RequireTaxZone).GetValueOrDefault())
      return;
    ((PXSelectBase) this.LocationCurrent).Cache.SetValue<PX.Objects.CR.Location.vTaxZoneID>((object) row, (object) row.CTaxZoneID);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.CR.Location.vTaxZoneID> e)
  {
    PX.Objects.CR.Location row = (PX.Objects.CR.Location) e.Row;
    if (row == null || !(row.LocType == "CU") && !(row.LocType == "VC") || row.CTaxZoneID != null && !((string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.CR.Location.vTaxZoneID>, object, object>) e).OldValue == row.CTaxZoneID) || !((bool?) PXResultset<CustomerClass>.op_Implicit(PXSelectBase<CustomerClass, PXSelect<CustomerClass>.Config>.Search<CustomerClass.customerClassID>((PXGraph) this, (object) PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer>.Config>.Search<Customer.bAccountID>((PXGraph) this, (object) row.BAccountID, Array.Empty<object>())).CustomerClassID, Array.Empty<object>()))?.RequireTaxZone).GetValueOrDefault())
      return;
    ((PXSelectBase) this.LocationCurrent).Cache.SetValue<PX.Objects.CR.Location.cTaxZoneID>((object) row, (object) row.VTaxZoneID);
  }

  protected virtual void Location_CAvalaraCustomerUsageType_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    int? nullable = row.BAccountID;
    if (!nullable.HasValue)
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent != null)
    {
      nullable = parent.DefLocationID;
      if (nullable.HasValue && (parent.Type == "CU" || parent.Type == "VC"))
      {
        nullable = row.LocationID;
        int? defLocationId = parent.DefLocationID;
        if (!(nullable.GetValueOrDefault() == defLocationId.GetValueOrDefault() & nullable.HasValue == defLocationId.HasValue))
        {
          CustomerClass customerClass = PXResultset<CustomerClass>.op_Implicit(PXSelectBase<CustomerClass, PXSelect<CustomerClass>.Config>.Search<CustomerClass.customerClassID>((PXGraph) this, (object) PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer>.Config>.Search<Customer.bAccountID>((PXGraph) this, (object) parent.BAccountID, Array.Empty<object>())).CustomerClassID, Array.Empty<object>()));
          if (customerClass != null && customerClass.AvalaraCustomerUsageType != null)
          {
            e.NewValue = (object) customerClass.AvalaraCustomerUsageType;
            ((CancelEventArgs) e).Cancel = true;
            return;
          }
        }
      }
    }
    e.NewValue = (object) "0";
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void Location_CShipComplete_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    PX.Objects.CR.Standalone.Location location = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelect<PX.Objects.CR.Standalone.Location, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Required<BAccount.bAccountID>>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Required<BAccount.defLocationID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) parent.BAccountID,
      (object) parent.DefLocationID
    }));
    if (location == null || location.CShipComplete == null)
      return;
    e.NewValue = (object) location.CShipComplete;
  }

  protected virtual void Location_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    PX.Objects.CR.Location loc = e.Row as PX.Objects.CR.Location;
    if (loc == null || !loc.BAccountID.HasValue)
      return;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) loc, (PKFindOptions) 0);
    if (parent == null)
      return;
    bool? valueOriginal = cache.GetValueOriginal<PX.Objects.CR.Standalone.Location.isDefault>((object) loc) as bool?;
    bool? nullable = loc.IsDefault;
    if (nullable.GetValueOrDefault())
    {
      nullable = valueOriginal;
      bool? isDefault = loc.IsDefault;
      if (!(nullable.GetValueOrDefault() == isDefault.GetValueOrDefault() & nullable.HasValue == isDefault.HasValue))
        this.SetDefaultLocation(loc, parent);
    }
    this.EstablishCTaxZoneRule((Action<System.Type, bool>) ((field, isRequired) => PXDefaultAttribute.SetPersistingCheck(cache, field.Name, (object) loc, isRequired ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2)));
    this.EstablishVTaxZoneRule((Action<System.Type, bool>) ((field, isRequired) => PXDefaultAttribute.SetPersistingCheck(cache, field.Name, (object) loc, isRequired ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2)));
    if (parent.Type == "CU" || parent.Type == "VC")
    {
      this.LocationValidator.ValidateCustomerLocation(cache, parent, (ILocation) loc);
      this.VerifyAvalaraUsageType(loc);
    }
    if (!(parent.Type == "VE") && !(parent.Type == "VC"))
      return;
    PX.Objects.AP.Vendor byId = VendorMaint.GetByID((PXGraph) this, parent.BAccountID);
    this.LocationValidator.ValidateVendorLocation(cache, byId, (ILocation) loc);
  }

  protected virtual void Location_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.Operation != 2)
      return;
    if (e.TranStatus == null)
    {
      int? nullable = (int?) sender.GetValue<PX.Objects.CR.Location.vAPAccountLocationID>(e.Row);
      int num1 = 0;
      if (nullable.GetValueOrDefault() < num1 & nullable.HasValue)
      {
        this._KeyToAbort = sender.GetValue<PX.Objects.CR.Location.locationID>(e.Row);
        PXDatabase.Update<PX.Objects.CR.Location>(new PXDataFieldParam[3]
        {
          (PXDataFieldParam) new PXDataFieldAssign("VAPAccountLocationID", this._KeyToAbort),
          (PXDataFieldParam) new PXDataFieldRestrict("LocationID", this._KeyToAbort),
          (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed
        });
        sender.SetValue<PX.Objects.CR.Location.vAPAccountLocationID>(e.Row, this._KeyToAbort);
      }
      nullable = (int?) sender.GetValue<PX.Objects.CR.Location.vPaymentInfoLocationID>(e.Row);
      int num2 = 0;
      if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
      {
        this._KeyToAbort = sender.GetValue<PX.Objects.CR.Location.locationID>(e.Row);
        PXDatabase.Update<PX.Objects.CR.Location>(new PXDataFieldParam[3]
        {
          (PXDataFieldParam) new PXDataFieldAssign("VPaymentInfoLocationID", this._KeyToAbort),
          (PXDataFieldParam) new PXDataFieldRestrict("LocationID", this._KeyToAbort),
          (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed
        });
        sender.SetValue<PX.Objects.CR.Location.vPaymentInfoLocationID>(e.Row, this._KeyToAbort);
      }
      nullable = (int?) sender.GetValue<PX.Objects.CR.Location.cARAccountLocationID>(e.Row);
      int num3 = 0;
      if (!(nullable.GetValueOrDefault() < num3 & nullable.HasValue))
        return;
      this._KeyToAbort = sender.GetValue<PX.Objects.CR.Location.locationID>(e.Row);
      PXDatabase.Update<PX.Objects.CR.Location>(new PXDataFieldParam[3]
      {
        (PXDataFieldParam) new PXDataFieldAssign("CARAccountLocationID", this._KeyToAbort),
        (PXDataFieldParam) new PXDataFieldRestrict("LocationID", this._KeyToAbort),
        (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed
      });
      sender.SetValue<PX.Objects.CR.Location.cARAccountLocationID>(e.Row, this._KeyToAbort);
    }
    else
    {
      if (e.TranStatus == 2)
      {
        if (object.Equals(this._KeyToAbort, sender.GetValue<PX.Objects.CR.Location.vAPAccountLocationID>(e.Row)))
        {
          object obj = sender.GetValue<PX.Objects.CR.Location.locationID>(e.Row);
          sender.SetValue<PX.Objects.CR.Location.vAPAccountLocationID>(e.Row, obj);
        }
        if (object.Equals(this._KeyToAbort, sender.GetValue<PX.Objects.CR.Location.vPaymentInfoLocationID>(e.Row)))
        {
          object obj = sender.GetValue<PX.Objects.CR.Location.locationID>(e.Row);
          sender.SetValue<PX.Objects.CR.Location.vPaymentInfoLocationID>(e.Row, obj);
        }
        if (object.Equals(this._KeyToAbort, sender.GetValue<PX.Objects.CR.Location.cARAccountLocationID>(e.Row)))
        {
          object obj = sender.GetValue<PX.Objects.CR.Location.locationID>(e.Row);
          sender.SetValue<PX.Objects.CR.Location.cARAccountLocationID>(e.Row, obj);
        }
      }
      this._KeyToAbort = (object) null;
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.CR.Location, PX.Objects.CR.Location.status> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.Status == "A")
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.CR.Location, PX.Objects.CR.Location.status>>) e).Cache.SetValue<PX.Objects.CR.Location.isActive>((object) e.Row, (object) true);
    else
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.CR.Location, PX.Objects.CR.Location.status>>) e).Cache.SetValue<PX.Objects.CR.Location.isActive>((object) e.Row, (object) false);
  }

  protected virtual void Location_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Location row))
      return;
    PX.Objects.CR.Address address = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.DefAddressID
    }));
    if (address == null || ((PXSelectBase) this.Address).Cache.GetStatus((object) address) != null)
      return;
    ((PXSelectBase) this.Address).Cache.SetStatus((object) address, (PXEntryStatus) 1);
  }

  protected virtual void Location_IsARAccountSameAsMain_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PX.Objects.CR.Location row = (PX.Objects.CR.Location) e.Row;
    BAccount parent = (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID>.FindParent((PXGraph) this, (PX.Objects.CR.Location.bAccountID) row, (PKFindOptions) 0);
    bool? accountSameAsMain = row.IsARAccountSameAsMain;
    bool flag = false;
    if (accountSameAsMain.GetValueOrDefault() == flag & accountSameAsMain.HasValue)
    {
      LocationARAccountSub locationArAccountSub1 = PXResultset<LocationARAccountSub>.op_Implicit(((PXSelectBase<LocationARAccountSub>) this.ARAccountSubLocation).Select(Array.Empty<object>()));
      row.CARAccountID = locationArAccountSub1.CARAccountID;
      row.CARSubID = locationArAccountSub1.CARSubID;
      row.CRetainageAcctID = locationArAccountSub1.CRetainageAcctID;
      row.CRetainageSubID = locationArAccountSub1.CRetainageSubID;
      row.CARAccountLocationID = row.LocationID;
      LocationARAccountSub locationArAccountSub2 = new LocationARAccountSub();
      locationArAccountSub2.BAccountID = row.BAccountID;
      locationArAccountSub2.LocationID = row.LocationID;
      locationArAccountSub2.CARAccountID = row.CARAccountID;
      locationArAccountSub2.CARSubID = row.CARSubID;
      locationArAccountSub2.CRetainageAcctID = row.CRetainageAcctID;
      locationArAccountSub2.CRetainageSubID = row.CRetainageSubID;
      ((PXSelectBase) this.BusinessAccount).Cache.Current = (object) parent;
      ((PXSelectBase<LocationARAccountSub>) this.ARAccountSubLocation).Insert(locationArAccountSub2);
    }
    if (!row.IsARAccountSameAsMain.GetValueOrDefault())
      return;
    int? defLocationId = parent.DefLocationID;
    int? nullable1 = row.LocationID;
    if (defLocationId.GetValueOrDefault() == nullable1.GetValueOrDefault() & defLocationId.HasValue == nullable1.HasValue)
      return;
    PX.Objects.CR.Location location1 = row;
    nullable1 = new int?();
    int? nullable2 = nullable1;
    location1.CARAccountID = nullable2;
    PX.Objects.CR.Location location2 = row;
    nullable1 = new int?();
    int? nullable3 = nullable1;
    location2.CARSubID = nullable3;
    PX.Objects.CR.Location location3 = row;
    nullable1 = new int?();
    int? nullable4 = nullable1;
    location3.CRetainageAcctID = nullable4;
    PX.Objects.CR.Location location4 = row;
    nullable1 = new int?();
    int? nullable5 = nullable1;
    location4.CRetainageSubID = nullable5;
    if (parent == null)
      return;
    row.CARAccountLocationID = parent.DefLocationID;
  }

  protected virtual void Location_VBranchID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void Address_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Address row))
      return;
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Address.isValidated>(cache, (object) row, false);
  }

  protected virtual void LocationARAccountSub_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (((PXSelectBase<PX.Objects.CR.Location>) this.Location).Current == null)
      return;
    PXUIFieldAttribute.SetEnabled(sender, e.Row, object.Equals((object) ((PXSelectBase<PX.Objects.CR.Location>) this.Location).Current.LocationID, (object) ((PXSelectBase<PX.Objects.CR.Location>) this.Location).Current.CARAccountLocationID));
  }

  protected virtual void LocationARAccountSub_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    LocationARAccountSub row = (LocationARAccountSub) e.Row;
    if (!sender.ObjectsEqual<LocationARAccountSub.cARAccountID, LocationARAccountSub.cARSubID>(e.Row, e.OldRow))
    {
      PX.Objects.CR.Location current = ((PXSelectBase<PX.Objects.CR.Location>) this.Location).Current;
      if (((PXFieldState) sender.GetStateExt<LocationARAccountSub.cARAccountID>(e.Row)).ErrorLevel < 4)
        current.CARAccountID = row.CARAccountID;
      current.CARSubID = row.CARSubID;
      GraphHelper.MarkUpdated(((PXSelectBase) this.Location).Cache, (object) current);
      PX.Objects.CR.Address address = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) current.DefAddressID
      }));
      if (address != null && ((PXSelectBase) this.Address).Cache.GetStatus((object) address) == null)
        ((PXSelectBase) this.Address).Cache.SetStatus((object) address, (PXEntryStatus) 1);
    }
    if (sender.ObjectsEqual<LocationARAccountSub.cRetainageAcctID, LocationARAccountSub.cRetainageSubID>(e.Row, e.OldRow))
      return;
    PX.Objects.CR.Location current1 = ((PXSelectBase<PX.Objects.CR.Location>) this.Location).Current;
    if (((PXFieldState) sender.GetStateExt<LocationARAccountSub.cRetainageAcctID>(e.Row)).ErrorLevel < 4)
      current1.CRetainageAcctID = row.CRetainageAcctID;
    current1.CRetainageSubID = row.CRetainageSubID;
    if (((PXSelectBase) this.Location).Cache.GetStatus((object) current1) != null)
      return;
    ((PXSelectBase) this.Location).Cache.SetStatus((object) current1, (PXEntryStatus) 1);
  }

  public virtual void Persist()
  {
    LocationARAccountSub current = ((PXSelectBase<LocationARAccountSub>) this.ARAccountSubLocation).Current;
    if (current != null)
    {
      ValidationHelper<ValidationHelper>.SetErrorEmptyIfNull<LocationARAccountSub.cARAccountID>(((PXSelectBase) this.ARAccountSubLocation).Cache, (object) current, (object) current.CARAccountID);
      ValidationHelper<ValidationHelper>.SetErrorEmptyIfNull<LocationARAccountSub.cARSubID>(((PXSelectBase) this.ARAccountSubLocation).Cache, (object) current, (object) current.CARSubID);
    }
    ((PXGraph) this).Persist();
    ((PXSelectBase) this.ARAccountSubLocation).Cache.Clear();
  }

  private CustomerClass ReadCustomerClass()
  {
    return PXResult<Customer, CustomerClass>.op_Implicit((PXResult<Customer, CustomerClass>) PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelectJoin<Customer, InnerJoin<CustomerClass, On<Customer.customerClassID, Equal<CustomerClass.customerClassID>>>, Where<Customer.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())));
  }

  private VendorClass ReadVendorClass()
  {
    return PXResult<PX.Objects.AP.Vendor, VendorClass>.op_Implicit((PXResult<PX.Objects.AP.Vendor, VendorClass>) PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelectJoin<PX.Objects.AP.Vendor, InnerJoin<VendorClass, On<PX.Objects.AP.Vendor.vendorClassID, Equal<VendorClass.vendorClassID>>>, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())));
  }

  protected virtual void VerifyAvalaraUsageType(PX.Objects.CR.Location loc)
  {
    CustomerClass customerClass = this.ReadCustomerClass();
    if (customerClass != null && customerClass.RequireAvalaraCustomerUsageType.GetValueOrDefault() && loc.CAvalaraCustomerUsageType == "0")
      throw new PXRowPersistingException(typeof (PX.Objects.CR.Location.cAvalaraCustomerUsageType).Name, (object) loc.CAvalaraCustomerUsageType, "Select the entity usage type other than Default.");
  }

  protected virtual void EstablishCTaxZoneRule(Action<System.Type, bool> setCheck)
  {
    CustomerClass customerClass = this.ReadCustomerClass();
    VendorClass vendorClass = this.ReadVendorClass();
    bool? nullable1 = (bool?) customerClass?.RequireTaxZone;
    int num;
    if (!nullable1.GetValueOrDefault())
    {
      bool? nullable2;
      if (vendorClass == null)
      {
        nullable1 = new bool?();
        nullable2 = nullable1;
      }
      else
        nullable2 = vendorClass.RequireTaxZone;
      nullable1 = nullable2;
      num = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 1;
    bool flag = num != 0;
    setCheck(typeof (PX.Objects.CR.Location.cTaxZoneID), flag);
  }

  protected virtual void EstablishVTaxZoneRule(Action<System.Type, bool> setCheck)
  {
    VendorClass vendorClass = this.ReadVendorClass();
    bool flag = vendorClass != null && vendorClass.RequireTaxZone.GetValueOrDefault();
    setCheck(typeof (PX.Objects.CR.Location.vTaxZoneID), flag);
  }

  protected virtual void SetDefaultLocation(PX.Objects.CR.Location row, BAccount account)
  {
    if (account == null || !account.BAccountID.HasValue || row == null || !row.LocationID.HasValue)
      return;
    if (!row.IsActive.GetValueOrDefault())
      throw new Exception("Default location can not be inactive.");
    PX.Objects.CR.Location previuosDefault = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<BAccount.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<BAccount.defLocationID>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) account.BAccountID,
      (object) account.DefLocationID
    }));
    int? defLocationId = account.DefLocationID;
    int? locationId = row.LocationID;
    if (!(defLocationId.GetValueOrDefault() == locationId.GetValueOrDefault() & defLocationId.HasValue == locationId.HasValue))
      this.SetLocationAsDefault(row, previuosDefault);
    account.DefLocationID = row.LocationID;
    ((PXSelectBase) this.BusinessAccount).Cache.Update((object) account);
  }

  public virtual void SetLocationAsDefault(PX.Objects.CR.Location newDefault, PX.Objects.CR.Location previuosDefault)
  {
    int? locationId = newDefault.LocationID;
    foreach (PXResult<PX.Objects.CR.Location> pxResult in ((PXSelectBase<PX.Objects.CR.Location>) this.OtherLocations).Select(Array.Empty<object>()))
    {
      PX.Objects.CR.Location location = PXResult<PX.Objects.CR.Location>.op_Implicit(pxResult);
      if (!object.Equals((object) location.LocationID, (object) location.VAPAccountLocationID))
      {
        location.VAPAccountLocationID = locationId;
        if (object.Equals((object) location.LocationID, (object) newDefault.LocationID) && previuosDefault != null)
        {
          location.VAPAccountID = previuosDefault.VAPAccountID;
          location.VAPSubID = previuosDefault.VAPSubID;
          location.VRetainageAcctID = previuosDefault.VRetainageAcctID;
          location.VRetainageSubID = previuosDefault.VRetainageSubID;
        }
        GraphHelper.MarkUpdated(((PXSelectBase) this.Location).Cache, (object) location);
      }
      if (!object.Equals((object) location.LocationID, (object) location.VPaymentInfoLocationID))
      {
        location.VPaymentInfoLocationID = locationId;
        if (object.Equals((object) location.LocationID, (object) newDefault.LocationID) && previuosDefault != null)
        {
          location.VCashAccountID = previuosDefault.VCashAccountID;
          location.VPaymentMethodID = previuosDefault.VPaymentMethodID;
          location.VPaymentLeadTime = previuosDefault.VPaymentLeadTime;
          location.VSeparateCheck = previuosDefault.VSeparateCheck;
          location.VPaymentByType = previuosDefault.VPaymentByType;
          location.VRemitAddressID = previuosDefault.VRemitAddressID;
          location.VRemitContactID = previuosDefault.VRemitContactID;
        }
        GraphHelper.MarkUpdated(((PXSelectBase) this.Location).Cache, (object) location);
      }
      if (!object.Equals((object) location.LocationID, (object) location.CARAccountLocationID))
      {
        location.CARAccountLocationID = locationId;
        if (object.Equals((object) location.LocationID, (object) newDefault.LocationID) && previuosDefault != null)
        {
          location.CARAccountID = previuosDefault.CARAccountID;
          location.CARSubID = previuosDefault.CARSubID;
          location.CRetainageAcctID = previuosDefault.CRetainageAcctID;
          location.CRetainageSubID = previuosDefault.CRetainageSubID;
        }
        GraphHelper.MarkUpdated(((PXSelectBase) this.Location).Cache, (object) location);
      }
    }
  }

  private PX.Objects.CR.Location GetDefaultLocation(PXCache cache, PX.Objects.CR.Location location)
  {
    if (location != null && location.BAccountID.HasValue)
    {
      BAccount baccount = (BAccount) PXParentAttribute.SelectParent(cache, (object) location, typeof (BAccount));
      if (baccount != null && baccount.DefLocationID.HasValue && (baccount.Type == "VE" || baccount.Type == "CU" || baccount.Type == "VC"))
      {
        int? locationId = location.LocationID;
        int? defLocationId = baccount.DefLocationID;
        if (!(locationId.GetValueOrDefault() == defLocationId.GetValueOrDefault() & locationId.HasValue == defLocationId.HasValue))
          return PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location>.Config>.Search<PX.Objects.CR.Location.locationID, PX.Objects.CR.Location.bAccountID>((PXGraph) this, (object) baccount.DefLocationID, (object) baccount.BAccountID, Array.Empty<object>()));
      }
    }
    return (PX.Objects.CR.Location) null;
  }

  /// <exclude />
  public class LocationMaintAddressLookupExtension : 
    AddressLookupExtension<LocationMaint, PX.Objects.CR.Location, PX.Objects.CR.Address>
  {
    protected override string AddressView => "Address";

    protected override string ViewOnMap => "viewOnMap";
  }
}
