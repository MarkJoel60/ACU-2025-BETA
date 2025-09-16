// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.BranchLocationMaint
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
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.FS;

public class BranchLocationMaint : PXGraph<BranchLocationMaint, FSBranchLocation>
{
  public PXSelect<FSBranchLocation> BranchLocationRecords;
  public PXSelect<FSBranchLocation, Where<FSBranchLocation.branchLocationID, Equal<Current<FSBranchLocation.branchLocationID>>>> CurrentBranchLocation;
  [PXViewName("Field Service Address")]
  public PXSelect<FSAddress, Where<FSAddress.addressID, Equal<Current<FSBranchLocation.branchLocationAddressID>>>> BranchLocation_Address;
  [PXViewName("Field Service Contact")]
  public PXSelect<FSContact, Where<FSContact.contactID, Equal<Current<FSBranchLocation.branchLocationContactID>>>> BranchLocation_Contact;
  public PXSelect<FSRoom, Where<FSRoom.branchLocationID, Equal<Current<FSBranchLocation.branchLocationID>>>> RoomRecords;
  public PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.branchID, Equal<Current<FSBranchLocation.branchID>>>> Branch;
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  public PXAction<FSBranchLocation> viewonMap;
  [PXViewDetailsButton(typeof (FSBranchLocation))]
  public PXAction<FSBranchLocation> openRoom;
  public PXAction<FSBranchLocation> validateAddress;

  [PXMergeAttributes]
  [PXSelector(typeof (FSBranchLocation.branchLocationCD))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FSBranchLocation.branchLocationCD> e)
  {
  }

  [PXDefault]
  [PXDBString(10, IsUnicode = true, InputMask = ">AAAAAAAAAA")]
  [PXUIField]
  protected virtual void FSRoom_RoomID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(4, IsFixed = true)]
  [PXDefault("BLOC")]
  [PXUIField(DisplayName = "Entity Type", Visible = false, Enabled = false)]
  protected virtual void FSContact_EntityType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(4, IsFixed = true)]
  [PXDefault("BLOC")]
  [PXUIField(DisplayName = "Entity Type", Visible = false, Enabled = false)]
  protected virtual void FSAddress_EntityType_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "View on Map")]
  [PXButton]
  public virtual void ViewonMap()
  {
    FSAddress aAddr = ((PXSelectBase<FSAddress>) this.BranchLocation_Address).SelectSingle(Array.Empty<object>());
    if (aAddr == null)
      return;
    BAccountUtility.ViewOnMap<FSAddress, FSAddress.countryID>((IAddress) aAddr);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable OpenRoom(PXAdapter adapter)
  {
    FSRoom current = ((PXSelectBase<FSRoom>) this.RoomRecords).Current;
    if (current != null)
    {
      RoomMaint instance = PXGraph.CreateInstance<RoomMaint>();
      ((PXSelectBase<FSRoom>) instance.RoomRecords).Current = PXResultset<FSRoom>.op_Implicit(((PXSelectBase<FSRoom>) instance.RoomRecords).Search<FSRoom.roomID>((object) current.RoomID, new object[1]
      {
        (object) current.BranchLocationID
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ValidateAddress(PXAdapter adapter)
  {
    BranchLocationMaint aGraph = this;
    foreach (FSBranchLocation fsBranchLocation in adapter.Get<FSBranchLocation>())
    {
      if (fsBranchLocation != null)
      {
        FSAddress aAddress = PXResultset<FSAddress>.op_Implicit(((PXSelectBase<FSAddress>) aGraph.BranchLocation_Address).Select(Array.Empty<object>()));
        if (aAddress != null)
        {
          bool? nullable = aAddress.IsDefaultAddress;
          bool flag1 = false;
          if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
          {
            nullable = aAddress.IsValidated;
            bool flag2 = false;
            if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
              PXAddressValidator.Validate<FSAddress>((PXGraph) aGraph, aAddress, true, true);
          }
        }
      }
      yield return (object) fsBranchLocation;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSBranchLocation> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSBranchLocation> e)
  {
    if (e.Row == null)
      return;
    this.EnableDisable_ActionButtons(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSBranchLocation>>) e).Cache, e.Row);
    PXDefaultAttribute.SetPersistingCheck<FSBranchLocation.dfltSiteID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSBranchLocation>>) e).Cache, (object) e.Row, this.GetPersistingCheckValueForDfltSiteID(PXAccess.FeatureInstalled<FeaturesSet.warehouse>() || PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>()));
    PXUIFieldAttribute.SetVisible<FSBranchLocation.dfltUOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSBranchLocation>>) e).Cache, (object) e.Row, PXAccess.FeatureInstalled<FeaturesSet.inventory>() && PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>());
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSBranchLocation> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSBranchLocation> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSBranchLocation> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSBranchLocation> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSBranchLocation> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSBranchLocation> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSBranchLocation> e)
  {
    if (e.Row == null)
      return;
    FSBranchLocation row = e.Row;
    if (PXAccess.FeatureInstalled<FeaturesSet.warehouse>() || PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>())
      return;
    row.DfltSiteID = new int?();
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSBranchLocation> e)
  {
  }

  protected virtual void _(PX.Data.Events.FieldUpdating<FSRoom, FSRoom.roomID> e)
  {
    if (((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSRoom, FSRoom.roomID>>) e).NewValue == null)
      return;
    FSRoom row = e.Row;
    if (PXResultset<FSRoom>.op_Implicit(PXSelectBase<FSRoom, PXSelect<FSRoom, Where<FSRoom.branchLocationID, Equal<Required<FSRoom.branchLocationID>>, And<FSRoom.roomID, Equal<Required<FSRoom.roomID>>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldUpdatingEventArgs, PX.Data.Events.FieldUpdating<FSRoom, FSRoom.roomID>>) e).Cache.Graph, new object[2]
    {
      (object) row.BranchLocationID,
      ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSRoom, FSRoom.roomID>>) e).NewValue
    })) == null)
      return;
    ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSRoom, FSRoom.roomID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSRoom> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSRoom> e)
  {
    if (e.Row == null)
      return;
    FSRoom row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSRoom>>) e).Cache;
    if (row.RoomID == null || string.IsNullOrEmpty(row.Descr))
      return;
    PXUIFieldAttribute.SetEnabled<FSRoom.roomID>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<FSRoom.descr>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<FSRoom.floorNbr>(cache, (object) row, false);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSRoom> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSRoom> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSRoom> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSRoom> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSRoom> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSRoom> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSRoom> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSRoom> e)
  {
  }

  /// <summary>The Action buttons get enabled or disabled.</summary>
  public virtual void EnableDisable_ActionButtons(
    PXCache cache,
    FSBranchLocation fsBranchLocationRow)
  {
    if (((PXSelectBase<FSAddress>) this.BranchLocation_Address).Current == null)
      return;
    if (((PXSelectBase<FSAddress>) this.BranchLocation_Address).Current.CountryID != null && ((PXSelectBase<FSAddress>) this.BranchLocation_Address).Current.City != null && ((PXSelectBase<FSAddress>) this.BranchLocation_Address).Current.State != null && ((PXSelectBase<FSAddress>) this.BranchLocation_Address).Current.PostalCode != null)
    {
      bool? isValidated = ((PXSelectBase<FSAddress>) this.BranchLocation_Address).Current.IsValidated;
      bool flag = false;
      if (isValidated.GetValueOrDefault() == flag & isValidated.HasValue)
      {
        ((PXAction) this.validateAddress).SetEnabled(true);
        return;
      }
    }
    ((PXAction) this.validateAddress).SetEnabled(false);
  }

  /// <summary>
  /// Checks if the distribution module is enable and return the corresponding PersistingCheck value.
  /// </summary>
  /// <returns>PXPersistingCheck.NullOrBlank is the distribution module is enabled otherwise returns PXPersistingCheck.Nothing.</returns>
  public virtual PXPersistingCheck GetPersistingCheckValueForDfltSiteID(
    bool isDistributionModuleEnabled)
  {
    return !isDistributionModuleEnabled ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1;
  }

  public class ContactAddress : ContactAddressGraph<BranchLocationMaint>
  {
    protected override ContactAddressGraph<BranchLocationMaint>.DocumentMapping GetDocumentMapping()
    {
      return new ContactAddressGraph<BranchLocationMaint>.DocumentMapping(typeof (FSBranchLocation))
      {
        DocumentAddressID = typeof (FSBranchLocation.branchLocationAddressID),
        DocumentContactID = typeof (FSBranchLocation.branchLocationContactID)
      };
    }

    protected override ContactAddressGraph<BranchLocationMaint>.DocumentContactMapping GetDocumentContactMapping()
    {
      return new ContactAddressGraph<BranchLocationMaint>.DocumentContactMapping(typeof (FSContact))
      {
        EMail = typeof (FSContact.email)
      };
    }

    protected override ContactAddressGraph<BranchLocationMaint>.DocumentAddressMapping GetDocumentAddressMapping()
    {
      return new ContactAddressGraph<BranchLocationMaint>.DocumentAddressMapping(typeof (FSAddress));
    }

    protected override PXCache GetContactCache()
    {
      return ((PXSelectBase) this.Base.BranchLocation_Contact).Cache;
    }

    protected override PXCache GetAddressCache()
    {
      return ((PXSelectBase) this.Base.BranchLocation_Address).Cache;
    }

    protected override IPersonalContact GetCurrentContact()
    {
      return (IPersonalContact) ((PXSelectBase<FSContact>) this.Base.BranchLocation_Contact).SelectSingle(Array.Empty<object>());
    }

    protected override IPersonalContact GetEtalonContact()
    {
      bool isDirty = ((PXSelectBase) this.Base.BranchLocation_Contact).Cache.IsDirty;
      FSContact etalonContact = ((PXSelectBase<FSContact>) this.Base.BranchLocation_Contact).Insert();
      ((PXSelectBase) this.Base.BranchLocation_Contact).Cache.SetStatus((object) etalonContact, (PXEntryStatus) 5);
      ((PXSelectBase) this.Base.BranchLocation_Contact).Cache.IsDirty = isDirty;
      return (IPersonalContact) etalonContact;
    }

    protected override IAddress GetCurrentAddress()
    {
      return (IAddress) ((PXSelectBase<FSAddress>) this.Base.BranchLocation_Address).SelectSingle(Array.Empty<object>());
    }

    protected override IAddress GetEtalonAddress()
    {
      bool isDirty = ((PXSelectBase) this.Base.BranchLocation_Address).Cache.IsDirty;
      FSAddress etalonAddress = ((PXSelectBase<FSAddress>) this.Base.BranchLocation_Address).Insert();
      ((PXSelectBase) this.Base.BranchLocation_Address).Cache.SetStatus((object) etalonAddress, (PXEntryStatus) 5);
      ((PXSelectBase) this.Base.BranchLocation_Address).Cache.IsDirty = isDirty;
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
        typeof (BranchLocationMaint.ContactAddress),
        1
      }
    };

    protected virtual void Load(ContainerBuilder builder)
    {
      ApplicationStartActivation.RunOnApplicationStart(builder, (System.Action) (() => PXBuildManager.SortExtensions += (Action<List<System.Type>>) (list => PXBuildManager.PartialSort(list, BranchLocationMaint.ExtensionSorting._order))), (string) null);
    }
  }

  /// <exclude />
  public class BranchLocationMaintAddressLookupExtension : 
    AddressLookupExtension<BranchLocationMaint, FSBranchLocation, FSAddress>
  {
    protected override string AddressView => "BranchLocation_Address";

    protected override string ViewOnMap => "viewonMap";
  }
}
