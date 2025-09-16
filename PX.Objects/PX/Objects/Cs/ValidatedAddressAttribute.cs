// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.ValidatedAddressAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

/// <summary>
/// This atribute should be placed on the IsValidated field of the Address-based DAC
/// It clears an IsValidated flag, if the essential fields of address (Line1, Line2, City, State, Country, PostalCode) are modified
/// It is rely on the predefined fields naming.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ValidatedAddressAttribute : 
  PXEventSubscriberAttribute,
  IPXRowUpdatedSubscriber,
  IPXRowSelectedSubscriber
{
  protected string _AddressLine1Field = "AddressLine1";
  protected string _AddressLine2Field = "AddressLine2";
  protected string _AddressLine31Field = "AddressLine3";
  protected string _CityField = "City";
  protected string _CountryField = "Country";
  protected string _StateField = "State";
  protected string _PostalCodeField = "PostalCode";
  protected string _IsValidatedField = "IsValidated";
  protected string _BAccountAddressID = "BAccountAddressID";

  public virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    string[] strArray = new string[7]
    {
      this._AddressLine1Field,
      this._AddressLine2Field,
      this._AddressLine31Field,
      this._CityField,
      this._CountryField,
      this._StateField,
      this._PostalCodeField
    };
    bool flag = false;
    bool? nullable1 = (bool?) sender.GetValue(e.OldRow, this.FieldOrdinal);
    int num = nullable1.GetValueOrDefault() ? 1 : 0;
    nullable1 = (bool?) sender.GetValue(e.Row, this.FieldOrdinal);
    bool valueOrDefault = nullable1.GetValueOrDefault();
    int? nullable2 = (int?) sender.GetValue(e.OldRow, this._BAccountAddressID);
    int? nullable3 = (int?) sender.GetValue(e.Row, this._BAccountAddressID);
    if (num != 0 && valueOrDefault)
    {
      int? nullable4 = nullable2;
      int? nullable5 = nullable3;
      if (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue)
      {
        foreach (string str in strArray)
        {
          int fieldOrdinal = sender.GetFieldOrdinal(str);
          if (!object.Equals(sender.GetValue(e.OldRow, fieldOrdinal), sender.GetValue(e.Row, fieldOrdinal)))
          {
            flag = true;
            break;
          }
        }
      }
    }
    if (!flag)
      return;
    sender.SetValue(e.Row, this.FieldOrdinal, (object) false);
  }

  public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null || !PXAccess.FeatureInstalled<FeaturesSet.addressValidation>())
      return;
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (!((bool?) sender.GetValue(e.Row, this.FieldOrdinal)).GetValueOrDefault() && PXAddressValidator.IsValidateRequired<IAddressBase>(sender.Graph, (IAddressBase) e.Row))
      propertyException = new PXSetPropertyException("Address is not validated.", (PXErrorLevel) 2);
    sender.RaiseExceptionHandling(this.FieldName, e.Row, (object) false, (Exception) propertyException);
  }
}
