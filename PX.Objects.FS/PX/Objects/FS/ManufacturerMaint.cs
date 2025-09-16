// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ManufacturerMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using Autofac;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.Extensions.ContactAddress;
using System;
using System.Collections.Generic;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.FS;

public class ManufacturerMaint : PXGraph<ManufacturerMaint, FSManufacturer>
{
  public PXSelect<FSManufacturer> ManufacturerRecords;
  public PXSelect<FSManufacturer, Where<FSManufacturer.manufacturerID, Equal<Current<FSManufacturer.manufacturerID>>>> CurrentManufacturer;
  [PXViewName("Field Service Contact")]
  public PXSelect<FSContact, Where<FSContact.contactID, Equal<Current<FSManufacturer.manufacturerContactID>>>> Manufacturer_Contact;
  [PXViewName("Field Service Address")]
  public PXSelect<FSAddress, Where<FSAddress.addressID, Equal<Current<FSManufacturer.manufacturerAddressID>>>> Manufacturer_Address;
  public PXAction<FSManufacturer> viewonMap;

  [PXUIField(DisplayName = "View on Map")]
  [PXButton]
  public virtual void ViewonMap()
  {
    FSAddress aAddr = ((PXSelectBase<FSAddress>) this.Manufacturer_Address).SelectSingle(Array.Empty<object>());
    if (aAddr == null)
      return;
    BAccountUtility.ViewOnMap<FSAddress, FSAddress.countryID>((IAddress) aAddr);
  }

  [PXMergeAttributes]
  [PXSelector(typeof (FSManufacturer.manufacturerCD))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FSManufacturer.manufacturerCD> e)
  {
  }

  [PXDBString(4, IsFixed = true)]
  [PXDefault("MNFC")]
  [PXUIField(DisplayName = "Entity Type", Visible = false, Enabled = false)]
  protected virtual void FSContact_EntityType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(4, IsFixed = true)]
  [PXDefault("MNFC")]
  [PXUIField(DisplayName = "Entity Type", Visible = false, Enabled = false)]
  protected virtual void FSAddress_EntityType_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSManufacturer.allowOverrideContactAddress> e)
  {
    if (e.Row == null)
      return;
    FSContact fsContact = ((PXSelectBase<FSContact>) this.Manufacturer_Contact).SelectSingle(Array.Empty<object>());
    FSAddress fsAddress = ((PXSelectBase<FSAddress>) this.Manufacturer_Address).SelectSingle(Array.Empty<object>());
    ((PXSelectBase) this.Manufacturer_Contact).Cache.SetValueExt<FSContact.overrideContact>((object) fsContact, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSManufacturer.allowOverrideContactAddress>, object, object>) e).NewValue);
    ((PXSelectBase) this.Manufacturer_Address).Cache.SetValueExt<FSAddress.overrideAddress>((object) fsAddress, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSManufacturer.allowOverrideContactAddress>, object, object>) e).NewValue);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSManufacturer> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSManufacturer> e)
  {
    if (e.Row == null)
      return;
    FSManufacturer row = e.Row;
    ((PXGraph) this).Caches[typeof (FSContact)].AllowUpdate = row.AllowOverrideContactAddress.GetValueOrDefault();
    ((PXGraph) this).Caches[typeof (FSAddress)].AllowUpdate = row.AllowOverrideContactAddress.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<FSManufacturer.allowOverrideContactAddress>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSManufacturer>>) e).Cache, (object) row, row.ContactID.HasValue);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSManufacturer> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSManufacturer> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSManufacturer> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSManufacturer> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSManufacturer> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSManufacturer> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSManufacturer> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSManufacturer> e)
  {
  }

  public class ContactAddress : ContactAddressGraph<ManufacturerMaint>
  {
    protected override ContactAddressGraph<ManufacturerMaint>.DocumentMapping GetDocumentMapping()
    {
      return new ContactAddressGraph<ManufacturerMaint>.DocumentMapping(typeof (FSManufacturer))
      {
        DocumentAddressID = typeof (FSManufacturer.manufacturerAddressID),
        DocumentContactID = typeof (FSManufacturer.manufacturerContactID)
      };
    }

    protected override ContactAddressGraph<ManufacturerMaint>.DocumentContactMapping GetDocumentContactMapping()
    {
      return new ContactAddressGraph<ManufacturerMaint>.DocumentContactMapping(typeof (FSContact))
      {
        EMail = typeof (FSContact.email)
      };
    }

    protected override ContactAddressGraph<ManufacturerMaint>.DocumentAddressMapping GetDocumentAddressMapping()
    {
      return new ContactAddressGraph<ManufacturerMaint>.DocumentAddressMapping(typeof (FSAddress));
    }

    protected override PXCache GetContactCache()
    {
      return ((PXSelectBase) this.Base.Manufacturer_Contact).Cache;
    }

    protected override PXCache GetAddressCache()
    {
      return ((PXSelectBase) this.Base.Manufacturer_Address).Cache;
    }

    protected override IPersonalContact GetCurrentContact()
    {
      return (IPersonalContact) ((PXSelectBase<FSContact>) this.Base.Manufacturer_Contact).SelectSingle(Array.Empty<object>());
    }

    protected override IPersonalContact GetEtalonContact()
    {
      bool isDirty = ((PXSelectBase) this.Base.Manufacturer_Contact).Cache.IsDirty;
      FSContact etalonContact = ((PXSelectBase<FSContact>) this.Base.Manufacturer_Contact).Insert();
      ((PXSelectBase) this.Base.Manufacturer_Contact).Cache.SetStatus((object) etalonContact, (PXEntryStatus) 5);
      ((PXSelectBase) this.Base.Manufacturer_Contact).Cache.IsDirty = isDirty;
      return (IPersonalContact) etalonContact;
    }

    protected override IAddress GetCurrentAddress()
    {
      return (IAddress) ((PXSelectBase<FSAddress>) this.Base.Manufacturer_Address).SelectSingle(Array.Empty<object>());
    }

    protected override IAddress GetEtalonAddress()
    {
      bool isDirty = ((PXSelectBase) this.Base.Manufacturer_Address).Cache.IsDirty;
      FSAddress etalonAddress = ((PXSelectBase<FSAddress>) this.Base.Manufacturer_Address).Insert();
      ((PXSelectBase) this.Base.Manufacturer_Address).Cache.SetStatus((object) etalonAddress, (PXEntryStatus) 5);
      ((PXSelectBase) this.Base.Manufacturer_Address).Cache.IsDirty = isDirty;
      return (IAddress) etalonAddress;
    }

    protected override IPersonalContact GetCurrentShippingContact() => (IPersonalContact) null;

    protected override IPersonalContact GetEtalonShippingContact() => (IPersonalContact) null;

    protected override IAddress GetCurrentShippingAddress() => (IAddress) null;

    protected override IAddress GetEtalonShippingAddress() => (IAddress) null;

    protected override PXCache GetShippingContactCache() => (PXCache) null;

    protected override PXCache GetShippingAddressCache() => (PXCache) null;
  }

  public class ExtensionSorting : Module
  {
    private static readonly Dictionary<System.Type, int> _order = new Dictionary<System.Type, int>()
    {
      {
        typeof (ManufacturerMaint.ContactAddress),
        1
      }
    };

    protected virtual void Load(ContainerBuilder builder)
    {
      ApplicationStartActivation.RunOnApplicationStart(builder, (System.Action) (() => PXBuildManager.SortExtensions += (Action<List<System.Type>>) (list => PXBuildManager.PartialSort(list, ManufacturerMaint.ExtensionSorting._order))), (string) null);
    }
  }

  /// <exclude />
  public class ManufacturerMaintAddressLookupExtension : 
    AddressLookupExtension<ManufacturerMaint, FSManufacturer, FSAddress>
  {
    protected override string AddressView => "Manufacturer_Address";

    protected override string ViewOnMap => "viewonMap";
  }
}
