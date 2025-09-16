// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSrvOrdAddressAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.CS;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class FSSrvOrdAddressAttribute(System.Type selectType) : FSDocumentAddressAttribute(selectType)
{
  public override void DefaultAddress<TAddress, TAddressID>(
    PXCache sender,
    object documentRow,
    object addressRow)
  {
    PXView pxView = (PXView) null;
    object obj1 = (object) null;
    FSSrvOrdType fsSrvOrdType = (FSSrvOrdType) null;
    bool flag1 = false;
    AppointmentEntry appointmentEntry = (AppointmentEntry) null;
    if (sender.Graph is ServiceOrderEntry)
      fsSrvOrdType = ((PXSelectBase<FSSrvOrdType>) ((ServiceOrderEntry) sender.Graph).ServiceOrderTypeSelected).Current;
    else if (sender.Graph is AppointmentEntry)
    {
      appointmentEntry = (AppointmentEntry) sender.Graph;
      fsSrvOrdType = ((PXSelectBase<FSSrvOrdType>) appointmentEntry.ServiceOrderTypeSelected).Current;
    }
    if (fsSrvOrdType != null && fsSrvOrdType.AppAddressSource == "BL" && sender.GetValue<FSServiceOrder.branchLocationID>(documentRow) != null)
    {
      obj1 = sender.GetValue<FSServiceOrder.branchLocationID>(documentRow);
      BqlCommand instance = BqlCommand.CreateInstance(new System.Type[1]
      {
        typeof (Select2<FSBLOCAddress, InnerJoin<FSBranchLocation, On<FSBranchLocation.branchLocationAddressID, Equal<FSBLOCAddress.addressID>>>, Where<FSBranchLocation.branchLocationID, Equal<PX.Data.Required<FSBranchLocation.branchLocationID>>>>)
      });
      pxView = sender.Graph.TypedViews.GetView(instance, false);
      flag1 = true;
    }
    else if (fsSrvOrdType != null && fsSrvOrdType.AppAddressSource == "CC" && sender.GetValue<FSServiceOrder.contactID>(documentRow) != null)
    {
      obj1 = sender.GetValue<FSServiceOrder.contactID>(documentRow);
      BqlCommand instance = BqlCommand.CreateInstance(new System.Type[1]
      {
        typeof (Select2<PX.Objects.CR.Address, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.defAddressID, Equal<PX.Objects.CR.Address.addressID>>, LeftJoin<FSAddress, On<FSAddress.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<FSAddress.bAccountAddressID, Equal<PX.Objects.CR.Address.addressID>, And<FSAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<FSAddress.isDefaultAddress, Equal<boolTrue>>>>>>>, Where<PX.Objects.CR.Contact.contactID, Equal<PX.Data.Required<FSServiceOrder.contactID>>>>)
      });
      pxView = sender.Graph.TypedViews.GetView(instance, false);
    }
    else if (fsSrvOrdType != null && fsSrvOrdType.AppAddressSource == "BA" && sender.GetValue<FSServiceOrder.locationID>(documentRow) != null)
    {
      obj1 = sender.GetValue<FSServiceOrder.locationID>(documentRow);
      BqlCommand instance = BqlCommand.CreateInstance(new System.Type[1]
      {
        typeof (Select2<PX.Objects.CR.Address, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.defAddressID, Equal<PX.Objects.CR.Address.addressID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<PX.Data.Required<FSManufacturer.locationID>>>, LeftJoin<FSAddress, On<FSAddress.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<FSAddress.bAccountAddressID, Equal<PX.Objects.CR.Address.addressID>, And<FSAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<FSAddress.isDefaultAddress, Equal<boolTrue>>>>>>>>, Where<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.defAddressID>>>)
      });
      pxView = sender.Graph.TypedViews.GetView(instance, false);
    }
    if (pxView != null)
    {
      int num1 = -1;
      int num2 = 0;
      bool flag2 = false;
      using (List<object>.Enumerator enumerator = pxView.Select(new object[1]
      {
        documentRow
      }, new object[1]{ obj1 }, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref num1, 1, ref num2).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXResult current1 = (PXResult) enumerator.Current;
          flag2 = !flag1 ? AddressAttribute.DefaultAddress<TAddress, TAddressID>(sender, this.FieldName, documentRow, addressRow, (object) current1) : this.DefaultBLOCAddress<FSBLOCAddress, FSBLOCAddress.addressID>(sender, this.FieldName, documentRow, addressRow, (object) current1);
          sender.SetDefaultExt<FSServiceOrder.taxZoneID>(documentRow);
          if (appointmentEntry != null)
          {
            FSAppointment current2 = ((PXSelectBase<FSAppointment>) appointmentEntry.AppointmentSelected).Current;
            if (current2 != null)
              ((PXSelectBase) appointmentEntry.AppointmentSelected).Cache.SetDefaultExt<FSAppointment.taxZoneID>((object) current2);
          }
        }
      }
      if (flag2 || this._Required)
        return;
      this.ClearRecord(sender, documentRow);
    }
    else
    {
      this.ClearRecord(sender, documentRow);
      if (!this._Required || sender.GetValue(documentRow, this._FieldOrdinal) != null)
        return;
      using (new ReadOnlyScope(new PXCache[1]
      {
        sender.Graph.Caches[this._RecordType]
      }))
      {
        object obj2 = sender.Graph.Caches[this._RecordType].Insert();
        object obj3 = sender.Graph.Caches[this._RecordType].GetValue(obj2, this._RecordID);
        sender.SetValue(documentRow, this._FieldOrdinal, obj3);
      }
    }
  }

  public virtual bool DefaultBLOCAddress<TAddress, TAddressID>(
    PXCache sender,
    string fieldName,
    object documentRow,
    object addressRow,
    object sourceRow)
    where TAddress : class, IBqlTable, IAddress, IAddressLocation, new()
    where TAddressID : IBqlField
  {
    bool flag = false;
    if (sourceRow != null)
    {
      if (!(addressRow is FSAddress fsAddress1))
        fsAddress1 = PXResultset<FSAddress>.op_Implicit(PXSelectBase<FSAddress, PXSelect<FSAddress, Where<FSAddress.addressID, Equal<PX.Data.Required<TAddressID>>>>.Config>.Select(sender.Graph, new object[1]
        {
          sender.GetValue(documentRow, fieldName)
        }));
      FSAddress fsAddress2 = PXResult.Unwrap<FSAddress>(sourceRow);
      if ((fsAddress2 != null ? (!fsAddress2.AddressID.HasValue ? 1 : 0) : 1) != 0 || sender.GetValue(documentRow, fieldName) == null)
      {
        int? nullable;
        if (fsAddress1 != null)
        {
          nullable = fsAddress1.AddressID;
          int num = 0;
          if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
            goto label_7;
        }
        fsAddress1 = new FSAddress();
label_7:
        fsAddress1.BAccountAddressID = PXResult.Unwrap<TAddress>(sourceRow).AddressID;
        fsAddress1.BAccountID = PXResult.Unwrap<TAddress>(sourceRow).BAccountID;
        fsAddress1.RevisionID = PXResult.Unwrap<TAddress>(sourceRow).RevisionID;
        fsAddress1.IsDefaultAddress = new bool?(true);
        fsAddress1.AddressLine1 = ((IAddressBase) (object) PXResult.Unwrap<TAddress>(sourceRow)).AddressLine1;
        fsAddress1.AddressLine2 = ((IAddressBase) (object) PXResult.Unwrap<TAddress>(sourceRow)).AddressLine2;
        fsAddress1.AddressLine3 = ((IAddressBase) (object) PXResult.Unwrap<TAddress>(sourceRow)).AddressLine3;
        fsAddress1.City = ((IAddressBase) (object) PXResult.Unwrap<TAddress>(sourceRow)).City;
        fsAddress1.State = ((IAddressBase) (object) PXResult.Unwrap<TAddress>(sourceRow)).State;
        fsAddress1.PostalCode = ((IAddressBase) (object) PXResult.Unwrap<TAddress>(sourceRow)).PostalCode;
        fsAddress1.CountryID = ((IAddressBase) (object) PXResult.Unwrap<TAddress>(sourceRow)).CountryID;
        fsAddress1.IsValidated = PXResult.Unwrap<TAddress>(sourceRow).IsValidated;
        fsAddress1.Latitude = PXResult.Unwrap<TAddress>(sourceRow).Latitude;
        fsAddress1.Longitude = PXResult.Unwrap<TAddress>(sourceRow).Longitude;
        nullable = fsAddress1.BAccountAddressID;
        int num1;
        if (nullable.HasValue)
        {
          nullable = fsAddress1.BAccountID;
          if (nullable.HasValue)
          {
            nullable = fsAddress1.RevisionID;
            num1 = nullable.HasValue ? 1 : 0;
            goto label_11;
          }
        }
        num1 = 0;
label_11:
        flag = num1 != 0;
        nullable = fsAddress1.AddressID;
        if (!nullable.HasValue)
        {
          FSAddress fsAddress3 = (FSAddress) sender.Graph.Caches[typeof (FSAddress)].Insert((object) fsAddress1);
          sender.SetValue(documentRow, fieldName, (object) fsAddress3.AddressID);
        }
        else if (addressRow == null)
          sender.Graph.Caches[typeof (FSAddress)].Update((object) fsAddress1);
      }
      else
      {
        int? addressId;
        if (fsAddress1 != null)
        {
          addressId = fsAddress1.AddressID;
          int num = 0;
          if (addressId.GetValueOrDefault() < num & addressId.HasValue)
            sender.Graph.Caches[typeof (FSAddress)].Delete((object) fsAddress1);
        }
        sender.SetValue(documentRow, fieldName, (object) PXResult.Unwrap<TAddress>(sourceRow).AddressID);
        addressId = PXResult.Unwrap<FSAddress>(sourceRow).AddressID;
        flag = addressId.HasValue;
      }
    }
    return flag;
  }
}
