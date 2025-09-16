// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPhoneValidationAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public class PXPhoneValidationAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  protected System.Type _PhoneValidationField;
  protected System.Type _CountryIdField;
  protected System.Type _ForeignIdField;
  protected string _PhoneMask = "";
  protected PXPhoneValidationAttribute.Definition _Definition;
  private System.Type _CacheType;

  /// <summary>Get, set.</summary>
  public virtual System.Type PhoneValidationField
  {
    get => this._PhoneValidationField;
    set => this._PhoneValidationField = value;
  }

  /// <summary>Get, set.</summary>
  public virtual string PhoneMask
  {
    get => this._PhoneMask;
    set => this._PhoneMask = value;
  }

  /// <summary>Get, set.</summary>
  public virtual System.Type CountryIdField
  {
    get => this._CountryIdField;
    set => this._CountryIdField = value;
  }

  public static void Clear<Table>() where Table : IBqlTable
  {
    PXDatabase.ResetSlot<PXPhoneValidationAttribute.Definition>("PhoneDefinitions", typeof (Table));
  }

  public PXPhoneValidationAttribute(System.Type phoneValidationField)
  {
    if (!(phoneValidationField != (System.Type) null))
      return;
    this._PhoneValidationField = typeof (IBqlField).IsAssignableFrom(phoneValidationField) ? phoneValidationField : throw new PXArgumentException(nameof (PhoneValidationField), "The specified phone validation field is not valid.");
  }

  public void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    string key;
    if (sender.GetItemType().Name == BqlCommand.GetItemType(this._CountryIdField).Name)
    {
      key = (string) sender.GetValue(e.Row, this._CountryIdField.Name);
    }
    else
    {
      PXCache cach = sender.Graph.Caches[BqlCommand.GetItemType(this._CountryIdField)];
      key = (string) cach.GetValue(cach.Current, this._CountryIdField.Name);
    }
    string str = (string) sender.GetValue(e.Row, this._FieldName);
    if (key == null || !this._Definition.CountryIdMask.TryGetValue(key, out this._PhoneMask))
      this._PhoneMask = "+#(###) ###-####";
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), this._FieldName, new bool?(), new int?(), this._PhoneMask, (string[]) null, (string[]) null, new bool?(), (string) null);
  }

  private void countryIDRowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    sender.Graph.Caches[this._CacheType].SetAltered(this._FieldName, true);
  }

  public override void CacheAttached(PXCache sender)
  {
    if (this._CountryIdField != (System.Type) null)
    {
      foreach (PXEventSubscriberAttribute attribute in (!sender.BqlFields.Contains(this._CountryIdField) ? sender.Graph.Caches[BqlCommand.GetItemType(this._CountryIdField)] : sender).GetAttributes(this._CountryIdField.Name))
      {
        if (attribute is PXSelectorAttribute)
        {
          this._ForeignIdField = ((PXSelectorAttribute) attribute).Field;
          break;
        }
      }
    }
    else
    {
      foreach (System.Type bqlField in sender.BqlFields)
      {
        foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes(bqlField.Name))
        {
          if (attribute is PXSelectorAttribute)
          {
            this._ForeignIdField = ((PXSelectorAttribute) attribute).Field;
            this._CountryIdField = bqlField;
            break;
          }
        }
      }
    }
    if (this._ForeignIdField == (System.Type) null)
      throw new PXException("The Country ID field with a selector is not found.");
    this._Definition = PXContext.GetSlot<PXPhoneValidationAttribute.Definition>();
    if (this._Definition == null)
      PXContext.SetSlot<PXPhoneValidationAttribute.Definition>(this._Definition = PXDatabase.GetSlot<PXPhoneValidationAttribute.Definition, PXPhoneValidationAttribute>("PhoneDefinitions", this, BqlCommand.GetItemType(this._ForeignIdField)));
    this._CacheType = sender.GetItemType();
    sender.Graph.Caches[BqlCommand.GetItemType(this._CountryIdField)].RowSelected += new PXRowSelected(this.countryIDRowSelected);
  }

  /// <exclude />
  protected class Definition : IPrefetchable<PXPhoneValidationAttribute>, IPXCompanyDependent
  {
    public Dictionary<string, string> CountryIdMask = new Dictionary<string, string>();

    public void Prefetch(PXPhoneValidationAttribute attr)
    {
      try
      {
        foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti(BqlCommand.GetItemType(attr._PhoneValidationField), new PXDataField(attr._ForeignIdField.Name), new PXDataField(attr._PhoneValidationField.Name)))
          this.CountryIdMask[pxDataRecord.GetString(0)] = pxDataRecord.GetString(1);
      }
      catch
      {
        this.CountryIdMask.Clear();
        throw;
      }
    }
  }
}
