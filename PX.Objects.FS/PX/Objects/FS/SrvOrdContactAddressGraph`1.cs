// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SrvOrdContactAddressGraph`1
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.Extensions.ContactAddress;
using PX.Objects.PM;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class SrvOrdContactAddressGraph<TGraph> : ContactAddressGraph<TGraph> where TGraph : PXGraph
{
  protected override ContactAddressGraph<TGraph>.DocumentMapping GetDocumentMapping()
  {
    return new ContactAddressGraph<TGraph>.DocumentMapping(typeof (FSServiceOrder))
    {
      DocumentAddressID = typeof (FSServiceOrder.serviceOrderAddressID),
      DocumentContactID = typeof (FSServiceOrder.serviceOrderContactID)
    };
  }

  protected override ContactAddressGraph<TGraph>.DocumentContactMapping GetDocumentContactMapping()
  {
    return new ContactAddressGraph<TGraph>.DocumentContactMapping(typeof (FSContact))
    {
      EMail = typeof (FSContact.email)
    };
  }

  protected override ContactAddressGraph<TGraph>.DocumentAddressMapping GetDocumentAddressMapping()
  {
    return new ContactAddressGraph<TGraph>.DocumentAddressMapping(typeof (FSAddress));
  }

  protected virtual PXSelectBase<FSContact> GetContactView()
  {
    if ((object) this.Base is ServiceOrderEntry)
      return (PXSelectBase<FSContact>) ((ServiceOrderEntry) (object) this.Base).ServiceOrder_Contact;
    return (object) this.Base is AppointmentEntry ? (PXSelectBase<FSContact>) ((AppointmentEntry) (object) this.Base).ServiceOrder_Contact : (PXSelectBase<FSContact>) null;
  }

  protected virtual PXSelectBase<FSAddress> GetAddressView()
  {
    if ((object) this.Base is ServiceOrderEntry)
      return (PXSelectBase<FSAddress>) ((ServiceOrderEntry) (object) this.Base).ServiceOrder_Address;
    return (object) this.Base is AppointmentEntry ? (PXSelectBase<FSAddress>) ((AppointmentEntry) (object) this.Base).ServiceOrder_Address : (PXSelectBase<FSAddress>) null;
  }

  protected virtual PXSelectBase<FSSrvOrdType> GetSrvOrdTypeView()
  {
    if ((object) this.Base is ServiceOrderEntry)
      return (PXSelectBase<FSSrvOrdType>) ((ServiceOrderEntry) (object) this.Base).ServiceOrderTypeSelected;
    return (object) this.Base is AppointmentEntry ? (PXSelectBase<FSSrvOrdType>) ((AppointmentEntry) (object) this.Base).ServiceOrderTypeSelected : (PXSelectBase<FSSrvOrdType>) null;
  }

  protected override PXCache GetContactCache() => ((PXSelectBase) this.GetContactView()).Cache;

  protected override PXCache GetAddressCache() => ((PXSelectBase) this.GetAddressView()).Cache;

  protected override IPersonalContact GetCurrentContact()
  {
    return (IPersonalContact) this.GetContactView().SelectSingle(Array.Empty<object>());
  }

  protected override IPersonalContact GetEtalonContact()
  {
    bool isDirty = ((PXSelectBase) this.GetContactView()).Cache.IsDirty;
    FSContact etalonContact = this.GetContactView().Insert();
    ((PXSelectBase) this.GetContactView()).Cache.SetStatus((object) etalonContact, (PXEntryStatus) 5);
    ((PXSelectBase) this.GetContactView()).Cache.IsDirty = isDirty;
    return (IPersonalContact) etalonContact;
  }

  protected override IAddress GetCurrentAddress()
  {
    return (IAddress) this.GetAddressView().SelectSingle(Array.Empty<object>());
  }

  protected override IAddress GetEtalonAddress()
  {
    bool isDirty = ((PXSelectBase) this.GetAddressView()).Cache.IsDirty;
    FSAddress etalonAddress = this.GetAddressView().Insert();
    ((PXSelectBase) this.GetAddressView()).Cache.SetStatus((object) etalonAddress, (PXEntryStatus) 5);
    ((PXSelectBase) this.GetAddressView()).Cache.IsDirty = isDirty;
    return (IAddress) etalonAddress;
  }

  protected override IPersonalContact GetCurrentShippingContact() => (IPersonalContact) null;

  protected override IPersonalContact GetEtalonShippingContact() => (IPersonalContact) null;

  protected override IAddress GetCurrentShippingAddress() => (IAddress) null;

  protected override IAddress GetEtalonShippingAddress() => (IAddress) null;

  protected override PXCache GetShippingContactCache() => (PXCache) null;

  protected override PXCache GetShippingAddressCache() => (PXCache) null;

  public virtual PX.Objects.CR.Contact GetContact(IContact source)
  {
    if (source == null)
      return (PX.Objects.CR.Contact) null;
    return new PX.Objects.CR.Contact()
    {
      BAccountID = source.BAccountID,
      RevisionID = source.RevisionID,
      FullName = source.FullName,
      Salutation = source.Salutation,
      Title = source.Title,
      Phone1 = source.Phone1,
      Phone1Type = source.Phone1Type,
      Phone2 = source.Phone2,
      Phone2Type = source.Phone2Type,
      Phone3 = source.Phone3,
      Phone3Type = source.Phone3Type,
      Fax = source.Fax,
      FaxType = source.FaxType,
      EMail = source.Email,
      NoteID = new Guid?(),
      Attention = source.Attention
    };
  }

  public virtual PX.Objects.CR.Address GetAddress(IAddress source)
  {
    if (source == null)
      return (PX.Objects.CR.Address) null;
    PX.Objects.CR.Address address = new PX.Objects.CR.Address();
    address.BAccountID = source.BAccountID;
    address.RevisionID = source.RevisionID;
    address.AddressLine1 = ((IAddressBase) source).AddressLine1;
    address.AddressLine2 = ((IAddressBase) source).AddressLine2;
    address.AddressLine3 = ((IAddressBase) source).AddressLine3;
    address.City = ((IAddressBase) source).City;
    address.CountryID = ((IAddressBase) source).CountryID;
    address.State = ((IAddressBase) source).State;
    address.PostalCode = ((IAddressBase) source).PostalCode;
    IAddressLocation iaddressLocation1 = (IAddressLocation) address;
    if (iaddressLocation1 != null && source is IAddressLocation iaddressLocation2)
    {
      iaddressLocation1.Latitude = iaddressLocation2.Latitude;
      iaddressLocation1.Longitude = iaddressLocation2.Longitude;
    }
    address.IsValidated = source.IsValidated;
    return address;
  }

  protected override bool AskForConfirmationForAddress(
    PX.Objects.Extensions.ContactAddress.Document row,
    ContactAddressGraph<TGraph>.ChangedData data)
  {
    return false;
  }

  protected override bool IsThereSomeContactAddressSourceValue(PXCache cache, PX.Objects.Extensions.ContactAddress.Document row)
  {
    FSSrvOrdType current = this.GetSrvOrdTypeView().Current;
    FSServiceOrder main = (FSServiceOrder) cache.GetMain<PX.Objects.Extensions.ContactAddress.Document>(row);
    if (current == null || main == null)
      return false;
    int? nullable;
    if (current.AppAddressSource == "BA")
    {
      nullable = row.LocationID;
      if (nullable.HasValue)
        goto label_9;
    }
    if (current.AppAddressSource == "CC")
    {
      nullable = row.ContactID;
      if (nullable.HasValue)
        goto label_9;
    }
    if (!(current.AppAddressSource == "BL"))
      return false;
    nullable = main.BranchLocationID;
    return nullable.HasValue;
label_9:
    return true;
  }

  protected void SetDefaultContactAndAddress(
    PXCache cache,
    object Row,
    int? oldContactID,
    int? oldLocationID,
    int? oldBranchLocationID,
    ContacAddressCallerEnum callerID)
  {
    PX.Objects.Extensions.ContactAddress.Document row;
    FSServiceOrder fsServiceOrderRow;
    if (Row is FSServiceOrder)
    {
      row = cache.GetExtension<PX.Objects.Extensions.ContactAddress.Document>(Row);
      fsServiceOrderRow = (FSServiceOrder) Row;
    }
    else
    {
      row = Row as PX.Objects.Extensions.ContactAddress.Document;
      fsServiceOrderRow = (FSServiceOrder) cache.GetMain<PX.Objects.Extensions.ContactAddress.Document>(row);
    }
    if (row == null)
      return;
    this.SetServiceOrderDefaultContactAndAddress(cache, row, fsServiceOrderRow, oldContactID, oldLocationID, oldBranchLocationID, fsServiceOrderRow.ProjectID, callerID);
  }

  protected void SetServiceOrderDefaultContactAndAddress(
    PXCache cache,
    PX.Objects.Extensions.ContactAddress.Document row,
    FSServiceOrder fsServiceOrderRow,
    int? oldContactID,
    int? oldLocationID,
    int? oldBranchLocationID,
    int? oldProjectID,
    ContacAddressCallerEnum callerID)
  {
    PX.Objects.CR.Contact contact = (PX.Objects.CR.Contact) null;
    PX.Objects.CR.Address address = (PX.Objects.CR.Address) null;
    FSSrvOrdType fsSrvOrdType = (FSSrvOrdType) null;
    if (cache.Graph is ServiceOrderEntry)
      fsSrvOrdType = ((PXSelectBase<FSSrvOrdType>) ((ServiceOrderEntry) cache.Graph).ServiceOrderTypeSelected).Current;
    else if (cache.Graph is AppointmentEntry)
      fsSrvOrdType = ((PXSelectBase<FSSrvOrdType>) ((AppointmentEntry) cache.Graph).ServiceOrderTypeSelected).Current;
    if (fsSrvOrdType != null && fsSrvOrdType.AppAddressSource == "BL" && oldBranchLocationID.HasValue)
    {
      contact = this.GetContact((IContact) PXResultset<FSContact>.op_Implicit(PXSelectBase<FSContact, PXSelectJoin<FSContact, InnerJoin<FSBranchLocation, On<FSBranchLocation.branchLocationContactID, Equal<FSContact.contactID>>>, Where<FSBranchLocation.branchLocationID, Equal<Required<FSBranchLocation.branchLocationID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) oldBranchLocationID
      })));
      address = this.GetAddress((IAddress) PXResultset<FSAddress>.op_Implicit(PXSelectBase<FSAddress, PXSelectJoin<FSAddress, InnerJoin<FSBranchLocation, On<FSBranchLocation.branchLocationAddressID, Equal<FSAddress.addressID>>>, Where<FSBranchLocation.branchLocationID, Equal<Required<FSBranchLocation.branchLocationID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) oldBranchLocationID
      })));
    }
    else if (fsSrvOrdType != null && fsSrvOrdType.AppAddressSource == "CC" && oldContactID.HasValue)
    {
      contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.Extensions.ContactAddress.Document.contactID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) oldContactID
      }));
      address = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelectJoin<PX.Objects.CR.Address, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.defAddressID, Equal<PX.Objects.CR.Address.addressID>>>, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.Extensions.ContactAddress.Document.contactID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) oldContactID
      }));
    }
    else if (fsSrvOrdType != null && fsSrvOrdType.AppAddressSource == "BA" && oldLocationID.HasValue)
    {
      contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.Extensions.ContactAddress.Document.locationID>>>>, Where<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.defContactID>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) oldLocationID
      }));
      address = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelectJoin<PX.Objects.CR.Address, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.defAddressID, Equal<PX.Objects.CR.Address.addressID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.Extensions.ContactAddress.Document.locationID>>>>>, Where<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.defAddressID>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) oldLocationID
      }));
    }
    int? nullable;
    if (fsSrvOrdType != null && callerID == ContacAddressCallerEnum.ProjectID)
    {
      nullable = fsServiceOrderRow.ProjectID;
      if (nullable.HasValue)
      {
        nullable = fsServiceOrderRow.ProjectID;
        int num1 = 0;
        if (!(nullable.GetValueOrDefault() == num1 & nullable.HasValue))
        {
          int num2 = -1;
          int num3 = 0;
          PMProject pmProject = PMProject.PK.Find(cache.Graph, oldProjectID);
          object[] objArray1 = new object[1]
          {
            (object) pmProject.BillAddressID
          };
          PMAddress pmAddress = (PMAddress) this.Base.TypedViews.GetView(BqlCommand.CreateInstance(new System.Type[1]
          {
            typeof (SelectFromBase<PMAddress, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMAddress.addressID, IBqlInt>.IsEqual<P.AsInt>>)
          }), false).Select((object[]) null, objArray1, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref num2, 1, ref num3).FirstOrDefault<object>();
          object[] objArray2 = new object[1]
          {
            (object) pmProject.SiteAddressID
          };
          PMAddress source = (PMAddress) this.Base.TypedViews.GetView(BqlCommand.CreateInstance(new System.Type[1]
          {
            typeof (SelectFromBase<PMAddress, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMAddress.addressID, IBqlInt>.IsEqual<P.AsInt>>)
          }), false).Select((object[]) null, objArray2, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref num2, 1, ref num3).FirstOrDefault<object>();
          if (source != null)
          {
            address = PropertyTransfer.Transfer<PMAddress, PX.Objects.CR.Address>(source, new PX.Objects.CR.Address());
            address.AddressID = source.AddressID;
            address.IsValidated = source.IsValidated;
            address.BAccountID = pmAddress.BAccountID;
          }
        }
      }
    }
    bool flag = false;
    switch (callerID)
    {
      case ContacAddressCallerEnum.Insert:
        flag = true;
        break;
      case ContacAddressCallerEnum.BranchLocationID:
        int num4;
        if (fsSrvOrdType?.AppAddressSource == "BL")
        {
          nullable = fsServiceOrderRow.BranchLocationID;
          num4 = nullable.HasValue ? 1 : 0;
        }
        else
          num4 = 0;
        flag = num4 != 0;
        break;
      case ContacAddressCallerEnum.BAccountID:
        int num5;
        if (fsSrvOrdType?.AppAddressSource != "BL")
        {
          nullable = fsServiceOrderRow.CustomerID;
          num5 = nullable.HasValue ? 1 : 0;
        }
        else
          num5 = 0;
        flag = num5 != 0;
        break;
      case ContacAddressCallerEnum.ContactID:
        int num6;
        if (fsSrvOrdType?.AppAddressSource == "CC")
        {
          nullable = fsServiceOrderRow.ContactID;
          num6 = nullable.HasValue ? 1 : 0;
        }
        else
          num6 = 0;
        flag = num6 != 0;
        break;
      case ContacAddressCallerEnum.LocationID:
        int num7;
        if (fsSrvOrdType?.AppAddressSource == "BA")
        {
          nullable = fsServiceOrderRow.LocationID;
          num7 = nullable.HasValue ? 1 : 0;
        }
        else
          num7 = 0;
        flag = num7 != 0;
        break;
      case ContacAddressCallerEnum.ProjectID:
        flag = true;
        break;
    }
    if (!flag)
      return;
    ContactAddressGraph<TGraph>.ChangedData changedForContactInfo = new ContactAddressGraph<TGraph>.ChangedData()
    {
      OldContact = contact,
      OldAddress = address
    };
    this.DefaultRecords(row, changedForContactInfo, new ContactAddressGraph<TGraph>.ChangedData(false));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.branchLocationID> e)
  {
    FSServiceOrder row = e.Row;
    if (row == null)
      return;
    this.SetDefaultContactAndAddress(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.branchLocationID>>) e).Cache, (object) e.Row, row.ContactID, row.LocationID, (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.branchLocationID>, FSServiceOrder, object>) e).OldValue, ContacAddressCallerEnum.BranchLocationID);
  }

  protected override void _(
    PX.Data.Events.FieldSelecting<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.allowOverrideContactAddress> e)
  {
    PX.Objects.Extensions.ContactAddress.Document row = e.Row;
    if (row == null)
      return;
    FSSrvOrdType current = this.GetSrvOrdTypeView().Current;
    FSServiceOrder main = (FSServiceOrder) ((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.allowOverrideContactAddress>>) e).Cache.GetMain<PX.Objects.Extensions.ContactAddress.Document>(row);
    if (current == null || main == null || (!(current.AppAddressSource == "BA") || row.LocationID.HasValue) && (!(current.AppAddressSource == "CC") || row.ContactID.HasValue) && (!(current.AppAddressSource == "BL") || main.BranchLocationID.HasValue))
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.allowOverrideContactAddress>>) e).ReturnValue = (object) false;
  }

  protected override void _(
    PX.Data.Events.FieldUpdated<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.contactID> e)
  {
    PX.Objects.Extensions.ContactAddress.Document row = e.Row;
    if (row == null)
      return;
    int? oldBranchLocationID = (int?) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.contactID>>) e).Cache.GetValue<FSServiceOrder.branchLocationID>((object) row);
    this.SetDefaultContactAndAddress(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.contactID>>) e).Cache, (object) e.Row, (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.contactID>, PX.Objects.Extensions.ContactAddress.Document, object>) e).OldValue, row.LocationID, oldBranchLocationID, ContacAddressCallerEnum.ContactID);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.Extensions.ContactAddress.Document> e)
  {
    if ((object) this.Base is ServiceOrderEntry)
    {
      ((PXAction) ((ServiceOrderEntry) (object) this.Base).viewDirectionOnMap).SetEnabled(true);
    }
    else
    {
      if (!((object) this.Base is AppointmentEntry))
        return;
      ((PXAction) ((AppointmentEntry) (object) this.Base).viewDirectionOnMap).SetEnabled(true);
    }
  }

  protected override void _(PX.Data.Events.RowInserted<PX.Objects.Extensions.ContactAddress.Document> e)
  {
    PX.Objects.Extensions.ContactAddress.Document row = e.Row;
    if (row == null)
      return;
    bool isDirty1 = this.GetContactCache().IsDirty;
    bool isDirty2 = this.GetAddressCache().IsDirty;
    int? oldBranchLocationID = (int?) ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<PX.Objects.Extensions.ContactAddress.Document>>) e).Cache.GetValue<FSServiceOrder.branchLocationID>((object) row);
    this.SetDefaultContactAndAddress(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<PX.Objects.Extensions.ContactAddress.Document>>) e).Cache, (object) e.Row, row.ContactID, row.LocationID, oldBranchLocationID, ContacAddressCallerEnum.Insert);
    this.GetContactCache().IsDirty = isDirty1;
    this.GetAddressCache().IsDirty = isDirty2;
  }

  protected override void _(
    PX.Data.Events.FieldUpdated<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.locationID> e)
  {
    PX.Objects.Extensions.ContactAddress.Document row = e.Row;
    if (row == null)
      return;
    int? oldBranchLocationID = (int?) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.locationID>>) e).Cache.GetValue<FSServiceOrder.branchLocationID>((object) row);
    this.SetDefaultContactAndAddress(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.locationID>>) e).Cache, (object) e.Row, row.ContactID, (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.locationID>, PX.Objects.Extensions.ContactAddress.Document, object>) e).OldValue, oldBranchLocationID, ContacAddressCallerEnum.LocationID);
  }

  protected override void _(
    PX.Data.Events.FieldUpdated<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.bAccountID> e)
  {
    PX.Objects.Extensions.ContactAddress.Document row = e.Row;
    if (row == null)
      return;
    int? oldBranchLocationID = (int?) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.bAccountID>>) e).Cache.GetValue<FSServiceOrder.branchLocationID>((object) row);
    this.SetDefaultContactAndAddress(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.bAccountID>>) e).Cache, (object) e.Row, row.ContactID, row.LocationID, oldBranchLocationID, ContacAddressCallerEnum.BAccountID);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.projectID> e)
  {
    PX.Objects.Extensions.ContactAddress.Document row = e.Row;
    if (row == null || !PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() || !new ProjectSettingsManager().CalculateProjectSpecificTaxes)
      return;
    int? oldBranchLocationID = (int?) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.projectID>>) e).Cache.GetValue<FSServiceOrder.branchLocationID>((object) row);
    FSServiceOrder main = (FSServiceOrder) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.projectID>>) e).Cache.GetMain<PX.Objects.Extensions.ContactAddress.Document>(row);
    this.SetServiceOrderDefaultContactAndAddress(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.projectID>>) e).Cache, row, main, row.ContactID, row.LocationID, oldBranchLocationID, (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.Extensions.ContactAddress.Document, PX.Objects.Extensions.ContactAddress.Document.projectID>, PX.Objects.Extensions.ContactAddress.Document, object>) e).OldValue, ContacAddressCallerEnum.ProjectID);
  }
}
