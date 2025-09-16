// Decompiled with JetBrains decompiler
// Type: PX.Data.PXZipValidationAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Data;

/// <summary>Implements validation of a value for DAC fields that hold a
/// ZIP postal code.</summary>
/// <example>
/// The code below shows a typical usage of the attribute. The constructor
/// with two parameters, which are set to the fields from the
/// <tt>Country</tt> DAC, is used. The <tt>CountryIdField</tt> property is
/// set to a field from the <tt>ARAddress</tt> DAC where the
/// <tt>PostalCode</tt> is defined.
/// <code>
/// [PXDBString(20)]
/// [PXUIField(DisplayName = "Postal Code")]
/// [PXZipValidation(typeof(Country.zipCodeRegexp),
/// typeof(Country.zipCodeMask),
/// CountryIdField = typeof(ARAddress.countryID))]
/// public virtual string PostalCode { ... }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public class PXZipValidationAttribute : 
  PXAggregateAttribute,
  IPXFieldVerifyingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXRowPersistingSubscriber
{
  protected System.Type _ZipValidationField;
  protected System.Type _ZipMaskField;
  protected System.Type _CountryIdField;
  protected System.Type _ForeignIdField;
  protected PXZipValidationAttribute.DefinitionZip _DefinitionZip;
  protected PXZipValidationAttribute.DefinitionMask _DefinitionMask;

  /// <summary>Gets or sets the DAC field that holds the ZIP validation
  /// information in a country data record.</summary>
  public virtual System.Type ZipValidationField
  {
    get => this._ZipValidationField;
    set => this._ZipValidationField = value;
  }

  /// <summary>
  /// Gets or sets the DAC field that holds the identifier of a
  /// country data record. In case this field is set, zip code verification
  /// will also be performed each time the country ID field is updated.
  /// </summary>
  [Obsolete("The property CountryIdField is deprecated and will be removed in the future versions. Please switch to using constructor overload as soon as possible.", false)]
  public virtual System.Type CountryIdField
  {
    get => this._CountryIdField;
    set
    {
      this._CountryIdField = !(this._CountryIdField != (System.Type) null) ? value : throw new InvalidOperationException("The Country ID field is already assigned and cannot be changed.");
      this._Attributes.Add((PXEventSubscriberAttribute) new PXFormulaAttribute(BqlCommand.Compose(typeof (Validate<>), this._CountryIdField)));
    }
  }

  /// <summary>Clears the internal slots that are used to keep ZIP
  /// definitions and ZIP mask definitions.</summary>
  public static void Clear<Table>() where Table : IBqlTable
  {
    PXDatabase.ResetSlot<PXZipValidationAttribute.DefinitionZip>("ZipDefinitions", typeof (Table));
    PXDatabase.ResetSlot<PXZipValidationAttribute.DefinitionMask>("MaskDefinitions", typeof (Table));
  }

  /// <summary>Initializes a new instance of the attribute that does not
  /// know the field that holds the ZIP mask.</summary>
  /// <param name="zipValidationField">The field that holds country's ZIP validation information.</param>
  public PXZipValidationAttribute(System.Type zipValidationField)
    : this(zipValidationField, (System.Type) null, (System.Type) null)
  {
  }

  /// <summary>Initializes a new instance of the attribute that does not
  /// know the field that holds the ZIP mask.</summary>
  /// <param name="zipValidationField">The field that holds country's ZIP validation information.</param>
  /// <param name="zipMaskField">The field that holds the country's ZIP mask.</param>
  public PXZipValidationAttribute(System.Type zipValidationField, System.Type zipMaskField)
    : this(zipValidationField, zipMaskField, (System.Type) null)
  {
  }

  /// <summary>Initializes a new instance of the attribute that uses the
  /// specified fields to retrieve the ZIP validation information and ZIP
  /// masks per country, and optionally performs re-validation of ZIP code
  /// upon each update of the specified Country ID field.</summary>
  /// <param name="zipMaskField">The field that holds the country's ZIP mask.</param>
  /// <param name="zipValidationField">The field that holds the country's ZIP validation information.</param>
  /// <param name="countryIdField">
  /// Optional field holding the Country ID. If not <c>null</c>, the <see cref="T:PX.Data.PXZipValidationAttribute" />
  /// will re-validate the ZIP code each time the <paramref name="countryIdField" /> is updated.
  /// </param>
  public PXZipValidationAttribute(System.Type zipValidationField, System.Type zipMaskField, System.Type countryIdField = null)
  {
    if (zipValidationField != (System.Type) null)
      this._ZipValidationField = typeof (IBqlField).IsAssignableFrom(zipValidationField) ? zipValidationField : throw new PXArgumentException(nameof (zipValidationField), "The specified Postal Code validation field is not valid.");
    if (zipMaskField != (System.Type) null)
      this._ZipMaskField = typeof (IBqlField).IsAssignableFrom(zipMaskField) ? zipMaskField : throw new PXArgumentException(nameof (zipMaskField), "The specified Postal Code Mask field is not valid.");
    if (!(countryIdField != (System.Type) null))
      return;
    this._CountryIdField = typeof (IBqlField).IsAssignableFrom(countryIdField) ? countryIdField : throw new PXArgumentException(nameof (countryIdField), "The specified Country ID field is not valid.");
    this._Attributes.Add((PXEventSubscriberAttribute) new PXFormulaAttribute(BqlCommand.Compose(typeof (Validate<>), this._CountryIdField)));
  }

  /// <exclude />
  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!sender.Graph.IsContractBasedAPI || !(sender.GetValue(e.Row, this.FieldOrdinal) is string str))
      return;
    string zipCode = str.Trim();
    if (!this.ProcessZipValidation(sender, e.Row, zipCode))
      throw new PXRowPersistingException(this.FieldName, (object) zipCode, "The entered code doesn't match the required expression.");
  }

  /// <exclude />
  public void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (sender.Graph.IsContractBasedAPI || sender.Graph.IsCopyPasteContext)
      return;
    if (!this.ProcessZipValidation(sender, e.Row, e.NewValue as string))
      throw new PXSetPropertyException("The entered code doesn't match the required expression.");
    sender.RaiseExceptionHandling(this._FieldName, e.Row, e.NewValue, (Exception) null);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    string mask;
    if (sender.Graph.IsImport && !sender.Graph.IsMobile || sender.Graph.IsContractBasedAPI || sender.Graph.IsCopyPasteContext || this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered || !this.TryGetInputMask(sender, e.Row, out mask))
      return;
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), this._FieldName, new bool?(), new int?(), mask, (string[]) null, (string[]) null, new bool?(), (string) null);
  }

  protected bool TryGetInputMask(PXCache sender, object row, out string mask)
  {
    if (this._ZipMaskField == (System.Type) null || this._DefinitionMask == null)
    {
      mask = (string) null;
      return false;
    }
    string key;
    if (sender.GetItemType().Name == BqlCommand.GetItemType(this._CountryIdField).Name)
    {
      key = (string) sender.GetValue(row, this._CountryIdField.Name);
    }
    else
    {
      object data = PXSelectorAttribute.Select(sender.Graph.Caches[BqlCommand.GetItemType(this._CountryIdField)], row, this._CountryIdField.Name);
      if (data == null)
      {
        mask = (string) null;
        return false;
      }
      key = (string) sender.Graph.Caches[data.GetType()].GetValue(data, this._CountryIdField.Name);
    }
    if (key != null && this._DefinitionMask.CountryIdMask.TryGetValue(key, out mask) && mask != null)
      return true;
    mask = (string) null;
    return false;
  }

  protected virtual bool ProcessZipValidation(PXCache sender, object row, string zipCode)
  {
    string key;
    if (sender.GetItemType().Name == BqlCommand.GetItemType(this._CountryIdField).Name)
    {
      key = (string) sender.GetValue(row, this._CountryIdField.Name);
    }
    else
    {
      object data = PXSelectorAttribute.Select(sender.Graph.Caches[BqlCommand.GetItemType(this._CountryIdField)], row, this._CountryIdField.Name);
      if (data == null)
        return true;
      key = (string) sender.Graph.Caches[data.GetType()].GetValue(data, this._CountryIdField.Name);
    }
    string regex;
    return key == null || !this._DefinitionZip.CountryIdRegex.TryGetValue(key, out regex) || this.ValidateZip(zipCode, regex);
  }

  protected bool ValidateZip(string val, string regex)
  {
    return string.IsNullOrEmpty(val) || string.IsNullOrEmpty(regex) || new Regex(regex).IsMatch(val);
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
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
    this._DefinitionZip = PXContext.GetSlot<PXZipValidationAttribute.DefinitionZip>();
    if (this._DefinitionZip == null)
      PXContext.SetSlot<PXZipValidationAttribute.DefinitionZip>(this._DefinitionZip = PXDatabase.GetSlot<PXZipValidationAttribute.DefinitionZip, PXZipValidationAttribute>("ZipDefinitions", this, BqlCommand.GetItemType(this._ForeignIdField)));
    if (!(this._ZipMaskField != (System.Type) null))
      return;
    this._DefinitionMask = PXContext.GetSlot<PXZipValidationAttribute.DefinitionMask>();
    if (this._DefinitionMask != null)
      return;
    PXContext.SetSlot<PXZipValidationAttribute.DefinitionMask>(this._DefinitionMask = PXDatabase.GetSlot<PXZipValidationAttribute.DefinitionMask, PXZipValidationAttribute>("MaskDefinitions", this, BqlCommand.GetItemType(this._ForeignIdField)));
  }

  /// <exclude />
  protected class DefinitionZip : IPrefetchable<PXZipValidationAttribute>, IPXCompanyDependent
  {
    public Dictionary<string, string> CountryIdRegex = new Dictionary<string, string>();

    public void Prefetch(PXZipValidationAttribute attr)
    {
      try
      {
        foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti(BqlCommand.GetItemType(attr._ZipValidationField), new PXDataField(attr._ForeignIdField.Name), new PXDataField(attr._ZipValidationField.Name)))
          this.CountryIdRegex[pxDataRecord.GetString(0)] = pxDataRecord.GetString(1);
      }
      catch
      {
        this.CountryIdRegex.Clear();
        throw;
      }
    }
  }

  /// <exclude />
  protected class DefinitionMask : IPrefetchable<PXZipValidationAttribute>, IPXCompanyDependent
  {
    public Dictionary<string, string> CountryIdMask = new Dictionary<string, string>();

    public void Prefetch(PXZipValidationAttribute attr)
    {
      try
      {
        foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti(BqlCommand.GetItemType(attr._ZipValidationField), new PXDataField(attr._ForeignIdField.Name), new PXDataField(attr._ZipMaskField.Name)))
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
