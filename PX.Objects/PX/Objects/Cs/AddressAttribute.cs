// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.AddressAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.CS;

public abstract class AddressAttribute : SharedRecordAttribute
{
  protected 
  #nullable disable
  Dictionary<object, bool> _canceled;

  public AddressAttribute(System.Type AddressIDType, System.Type IsDefaultAddressType, System.Type SelectType)
    : base(AddressIDType, IsDefaultAddressType, SelectType)
  {
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.RowUpdatingEvents rowUpdating = sender.Graph.RowUpdating;
    System.Type recordType1 = this._RecordType;
    AddressAttribute addressAttribute1 = this;
    // ISSUE: virtual method pointer
    PXRowUpdating pxRowUpdating = new PXRowUpdating((object) addressAttribute1, __vmethodptr(addressAttribute1, Record_RowUpdating));
    rowUpdating.AddHandler(recordType1, pxRowUpdating);
    PXGraph.RowInsertingEvents rowInserting = sender.Graph.RowInserting;
    System.Type recordType2 = this._RecordType;
    AddressAttribute addressAttribute2 = this;
    // ISSUE: virtual method pointer
    PXRowInserting pxRowInserting = new PXRowInserting((object) addressAttribute2, __vmethodptr(addressAttribute2, Record_RowInserting));
    rowInserting.AddHandler(recordType2, pxRowInserting);
    this._canceled = new Dictionary<object, bool>();
    sender.Graph.OnBeforeCommit += (Action<PXGraph>) (graph =>
    {
      foreach (object obj in NonGenericIEnumerableExtensions.Concat_(sender.Inserted, sender.Updated))
      {
        int? nullable = (int?) sender.GetValue(obj, this._FieldName);
        int num = 0;
        if (nullable.GetValueOrDefault() < num & nullable.HasValue && graph.Views.Caches.Contains(this._RecordType))
        {
          string displayName = graph.Caches[this._RecordType].DisplayName;
          throw new PXException("The document cannot be saved because the {0} field in the database record that corresponds to this document is corrupted. Please try to save the document again. In case the issue remains, contact your Acumatica support provider for the assistance.", new object[1]
          {
            (object) (!string.IsNullOrEmpty(displayName) ? $"{PXUIFieldAttribute.GetDisplayName(sender, this._FieldName)} ({displayName})" : PXUIFieldAttribute.GetDisplayName(sender, this._FieldName))
          });
        }
      }
    });
  }

  protected virtual void Record_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!e.ExternalCall || PXContext.GetSlot<bool>("ForceInternalCall"))
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void Record_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (this._canceled.ContainsKey(e.NewRow))
    {
      ((CancelEventArgs) e).Cancel = true;
      if (sender.GetStatus(e.NewRow) == 1)
        sender.SetStatus(e.NewRow, (PXEntryStatus) 0);
    }
    object obj1 = sender.GetValue(e.NewRow, this._RecordID);
    object obj2 = sender.GetValue(e.NewRow, this._IsDefault);
    if (Convert.ToInt32(obj1) <= 0 || !Convert.ToBoolean(obj2) || sender.GetStatus(e.NewRow) != 1)
      return;
    sender.SetStatus(e.NewRow, (PXEntryStatus) 0);
  }

  protected override void Record_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    base.Record_RowUpdated(sender, e);
    if (!((bool?) sender.GetValue(e.Row, this._IsDefault)).GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetError(sender, e.Row, "City", (string) null, (string) null);
    PXUIFieldAttribute.SetError(sender, e.Row, "State", (string) null, (string) null);
    PXUIFieldAttribute.SetError(sender, e.Row, "PostalCode", (string) null, (string) null);
    PXUIFieldAttribute.SetError(sender, e.Row, "CountryID", (string) null, (string) null);
  }

  public void Address_IsDefaultAddress_FieldVerifying<TAddress>(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
    where TAddress : class, IBqlTable, IAddress, new()
  {
    PXCache cach = sender.Graph.Caches[this._ItemType];
    int? nullable1 = (int?) cach.GetValue(cach.Current, this._FieldOrdinal);
    int? addressId = ((TAddress) e.Row).AddressID;
    int? nullable2 = nullable1;
    if (!(addressId.GetValueOrDefault() == nullable2.GetValueOrDefault() & addressId.HasValue == nullable2.HasValue))
      return;
    bool? newValue = (bool?) e.NewValue;
    bool flag = false;
    if (newValue.GetValueOrDefault() == flag & newValue.HasValue)
    {
      int? nullable3 = ((TAddress) e.Row).AddressID;
      int num = 0;
      if (nullable3.GetValueOrDefault() > num & nullable3.HasValue)
      {
        ((CancelEventArgs) e).Cancel = true;
        e.NewValue = (object) true;
        this._canceled[e.Row] = true;
        TAddress copy = (TAddress) sender.CreateCopy((object) (TAddress) e.Row);
        // ISSUE: variable of a boxed type
        __Boxed<TAddress> local = (object) copy;
        nullable3 = new int?();
        int? nullable4 = nullable3;
        local.AddressID = nullable4;
        ((INotable) (object) copy).NoteID = new Guid?();
        copy.IsDefaultAddress = new bool?(false);
        PXContext.SetSlot<bool>("ForceInternalCall", true);
        try
        {
          sender.Insert((IDictionary) sender.ToDictionary((object) copy));
        }
        finally
        {
          PXContext.SetSlot<bool>("ForceInternalCall", false);
        }
        TAddress current = (TAddress) sender.Current;
        cach.SetValue(cach.Current, this._FieldOrdinal, (object) current.AddressID);
        GraphHelper.MarkUpdated(cach, cach.Current);
        return;
      }
    }
    if (e.NewValue == null || !(bool) e.NewValue)
      return;
    newValue = (bool?) e.NewValue;
    if (!newValue.GetValueOrDefault())
      return;
    ((CancelEventArgs) e).Cancel = true;
    e.NewValue = (object) false;
    this.DefaultRecord(cach, cach.Current, e.Row);
    if (e.ExternalCall)
    {
      string[] strArray1 = new string[7]
      {
        "AddressLine1",
        "AddressLine2",
        "AddressLine3",
        "City",
        "State",
        "PostalCode",
        "CountryID"
      };
      foreach (string str in strArray1)
        sender.SetValuePending(e.Row, str, PXCache.NotSetValue);
      if (typeof (IAddressISO20022).IsAssignableFrom(typeof (TAddress)))
      {
        string[] strArray2 = new string[11]
        {
          "Department",
          "SubDepartment",
          "StreetName",
          "BuildingNumber",
          "BuildingName",
          "Floor",
          "UnitNumber",
          "PostBox",
          "Room",
          "TownLocationName",
          "DistrictName"
        };
        foreach (string str in strArray2)
          sender.SetValuePending(e.Row, str, PXCache.NotSetValue);
      }
    }
    GraphHelper.MarkUpdated(cach, cach.Current);
  }

  public virtual void DefaultAddress<TAddress, TAddressID>(
    PXCache sender,
    object DocumentRow,
    object AddressRow)
    where TAddress : class, IBqlTable, IAddress, new()
    where TAddressID : IBqlField
  {
    PXView view = sender.Graph.TypedViews.GetView(this._Select, false);
    int num1 = -1;
    int num2 = 0;
    bool flag = false;
    object[] objArray = new object[1]{ DocumentRow };
    ref int local1 = ref num1;
    ref int local2 = ref num2;
    using (List<object>.Enumerator enumerator = view.Select(objArray, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref local1, 1, ref local2).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXResult current = (PXResult) enumerator.Current;
        flag = AddressAttribute.DefaultAddress<TAddress, TAddressID>(sender, this.FieldName, DocumentRow, AddressRow, (object) current);
      }
    }
    if (flag || this._Required)
      return;
    this.ClearRecord(sender, DocumentRow);
  }

  public static bool DefaultAddress<TAddress, TAddressID>(
    PXCache sender,
    string FieldName,
    object DocumentRow,
    object AddressRow,
    object SourceRow)
    where TAddress : class, IBqlTable, IAddress, new()
    where TAddressID : IBqlField
  {
    bool flag = false;
    if (SourceRow != null)
    {
      if (!(AddressRow is TAddress address1))
        address1 = PXResultset<TAddress>.op_Implicit(PXSelectBase<TAddress, PXSelect<TAddress, Where<TAddressID, Equal<PX.Data.Required<TAddressID>>>>.Config>.Select(sender.Graph, new object[1]
        {
          sender.GetValue(DocumentRow, FieldName)
        }));
      if (!PXResult.Unwrap<TAddress>(SourceRow).AddressID.HasValue || sender.GetValue(DocumentRow, FieldName) == null)
      {
        int? nullable;
        if ((object) address1 != null)
        {
          nullable = address1.AddressID;
          int num = 0;
          if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
            goto label_7;
        }
        address1 = new TAddress();
label_7:
        Address address2 = PXResult.Unwrap<Address>(SourceRow);
        address1.BAccountAddressID = address2.AddressID;
        address1.BAccountID = address2.BAccountID;
        address1.RevisionID = address2.RevisionID;
        address1.IsDefaultAddress = new bool?(true);
        ((IAddressBase) (object) address1).AddressLine1 = address2.AddressLine1;
        ((IAddressBase) (object) address1).AddressLine2 = address2.AddressLine2;
        ((IAddressBase) (object) address1).AddressLine3 = address2.AddressLine3;
        ((IAddressBase) (object) address1).City = address2.City;
        ((IAddressBase) (object) address1).State = address2.State;
        ((IAddressBase) (object) address1).PostalCode = address2.PostalCode;
        ((IAddressBase) (object) address1).CountryID = address2.CountryID;
        address1.IsValidated = address2.IsValidated;
        if (address1 is IAddressISO20022 iaddressIsO20022)
        {
          iaddressIsO20022.Department = address2.Department;
          iaddressIsO20022.SubDepartment = address2.SubDepartment;
          iaddressIsO20022.StreetName = address2.StreetName;
          iaddressIsO20022.BuildingNumber = address2.BuildingNumber;
          iaddressIsO20022.BuildingName = address2.BuildingName;
          iaddressIsO20022.Floor = address2.Floor;
          iaddressIsO20022.UnitNumber = address2.UnitNumber;
          iaddressIsO20022.PostBox = address2.PostBox;
          iaddressIsO20022.Room = address2.Room;
          iaddressIsO20022.TownLocationName = address2.TownLocationName;
          iaddressIsO20022.DistrictName = address2.DistrictName;
        }
        if (address1 is IAddressLocation iaddressLocation)
        {
          iaddressLocation.Latitude = address2.Latitude;
          iaddressLocation.Longitude = address2.Longitude;
        }
        nullable = address1.BAccountAddressID;
        int num1;
        if (nullable.HasValue)
        {
          nullable = address1.BAccountID;
          if (nullable.HasValue)
          {
            nullable = address1.RevisionID;
            num1 = nullable.HasValue ? 1 : 0;
            goto label_15;
          }
        }
        num1 = 0;
label_15:
        flag = num1 != 0;
        nullable = address1.AddressID;
        if (!nullable.HasValue)
        {
          TAddress address3 = (TAddress) sender.Graph.Caches[typeof (TAddress)].Insert((object) address1);
          sender.SetValue(DocumentRow, FieldName, (object) address3.AddressID);
        }
        else if (AddressRow == null)
          sender.Graph.Caches[typeof (TAddress)].Update((object) address1);
      }
      else
      {
        int? addressId;
        if ((object) address1 != null)
        {
          addressId = address1.AddressID;
          int num = 0;
          if (addressId.GetValueOrDefault() < num & addressId.HasValue)
            sender.Graph.Caches[typeof (TAddress)].Delete((object) address1);
        }
        sender.SetValue(DocumentRow, FieldName, (object) PXResult.Unwrap<TAddress>(SourceRow).AddressID);
        addressId = PXResult.Unwrap<TAddress>(SourceRow).AddressID;
        flag = addressId.HasValue;
      }
    }
    return flag;
  }

  public static void Copy(IAddress dest, IAddress source)
  {
    dest.BAccountID = source.BAccountID;
    dest.BAccountAddressID = source.BAccountAddressID;
    dest.RevisionID = source.RevisionID;
    dest.IsDefaultAddress = source.IsDefaultAddress;
    ((IAddressBase) dest).AddressLine1 = ((IAddressBase) source).AddressLine1;
    ((IAddressBase) dest).AddressLine2 = ((IAddressBase) source).AddressLine2;
    ((IAddressBase) dest).AddressLine3 = ((IAddressBase) source).AddressLine3;
    ((IAddressBase) dest).City = ((IAddressBase) source).City;
    ((IAddressBase) dest).CountryID = ((IAddressBase) source).CountryID;
    ((IAddressBase) dest).State = ((IAddressBase) source).State;
    ((IAddressBase) dest).PostalCode = ((IAddressBase) source).PostalCode;
    if (dest is IAddressISO20022 iaddressIsO20022_1 && source is IAddressISO20022 iaddressIsO20022_2)
    {
      iaddressIsO20022_1.Department = iaddressIsO20022_2.Department;
      iaddressIsO20022_1.SubDepartment = iaddressIsO20022_2.SubDepartment;
      iaddressIsO20022_1.StreetName = iaddressIsO20022_2.StreetName;
      iaddressIsO20022_1.BuildingNumber = iaddressIsO20022_2.BuildingNumber;
      iaddressIsO20022_1.BuildingName = iaddressIsO20022_2.BuildingName;
      iaddressIsO20022_1.Floor = iaddressIsO20022_2.Floor;
      iaddressIsO20022_1.UnitNumber = iaddressIsO20022_2.UnitNumber;
      iaddressIsO20022_1.PostBox = iaddressIsO20022_2.PostBox;
      iaddressIsO20022_1.Room = iaddressIsO20022_2.Room;
      iaddressIsO20022_1.TownLocationName = iaddressIsO20022_2.TownLocationName;
      iaddressIsO20022_1.DistrictName = iaddressIsO20022_2.DistrictName;
    }
    if (dest is IAddressLocation iaddressLocation1 && source is IAddressLocation iaddressLocation2)
    {
      iaddressLocation1.Latitude = iaddressLocation2.Latitude;
      iaddressLocation1.Longitude = iaddressLocation2.Longitude;
    }
    IValidatedAddress validatedAddress1 = (IValidatedAddress) dest;
    if (validatedAddress1 == null)
      return;
    IValidatedAddress validatedAddress2 = (IValidatedAddress) source;
    if (validatedAddress2 == null)
      return;
    validatedAddress1.IsValidated = validatedAddress2.IsValidated;
  }

  protected void CopyAddress<TAddress, TAddressID>(
    PXCache sender,
    object DocumentRow,
    object SourceRow,
    bool clone)
    where TAddress : class, IBqlTable, IAddress, new()
    where TAddressID : IBqlField
  {
    if (!(SourceRow is IAddress address1))
      address1 = (IAddress) PXResultset<TAddress>.op_Implicit(PXSelectBase<TAddress, PXSelect<TAddress, Where<TAddressID, Equal<PX.Data.Required<TAddressID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        sender.GetValue(SourceRow, this._FieldOrdinal)
      }));
    IAddress source = address1;
    if (source != null && (clone || !source.IsDefaultAddress.GetValueOrDefault()))
    {
      int? nullable = (int?) sender.GetValue(DocumentRow, this._FieldOrdinal);
      int num = 0;
      if (nullable.GetValueOrDefault() < num & nullable.HasValue)
      {
        TAddress address2 = new TAddress();
        address2.AddressID = (int?) sender.GetValue(DocumentRow, this._FieldOrdinal);
        AddressAttribute.Copy((IAddress) sender.Graph.Caches[typeof (TAddress)].Locate((object) address2), source);
      }
      else
      {
        TAddress dest = new TAddress();
        AddressAttribute.Copy((IAddress) dest, source);
        TAddress address3 = (TAddress) sender.Graph.Caches[typeof (TAddress)].Insert((object) dest);
        if ((object) address3 == null)
          return;
        sender.SetValue(DocumentRow, this.FieldOrdinal, (object) address3.AddressID);
      }
    }
    else
      this.DefaultAddress<TAddress, TAddressID>(sender, DocumentRow, (object) null);
  }

  [Obsolete]
  public abstract class addressLine1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddressAttribute.addressLine1>
  {
  }

  [Obsolete]
  public abstract class addressLine2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddressAttribute.addressLine2>
  {
  }

  [Obsolete]
  public abstract class addressLine3 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddressAttribute.addressLine3>
  {
  }

  [Obsolete]
  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddressAttribute.city>
  {
  }

  [Obsolete]
  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddressAttribute.countryID>
  {
  }

  [Obsolete]
  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddressAttribute.state>
  {
  }

  [Obsolete]
  public abstract class postalCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddressAttribute.postalCode>
  {
  }
}
