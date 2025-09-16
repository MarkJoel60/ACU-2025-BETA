// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBListAttributeHelper`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class PXDBListAttributeHelper<TValue> : IPXDBListAttributeHelper, ILocalizableValues
{
  private PXDBListAttributeHelper<TValue>.ListParameters _parameters;
  private readonly string _slotKey;
  private readonly string _locKey;

  protected PXDBListAttributeHelper(System.Type table, System.Type valueField, System.Type descriptionField)
  {
    this._parameters = new PXDBListAttributeHelper<TValue>.ListParameters(table, descriptionField, valueField);
    this._slotKey = $"_{this.GetType()}_slotKey";
    this._locKey = table.FullName;
  }

  public System.Type DefaultValueField
  {
    get => this._parameters.DefaultValueField;
    set => this._parameters = this._parameters.ChangeDefaultValueField(value);
  }

  public string EmptyLabel
  {
    get => this._parameters.EmptyLabel.Key;
    set => this._parameters = this._parameters.ChangeEmptyLabel(value, this.EmptyLabelValue);
  }

  protected virtual TValue EmptyLabelValue => default (TValue);

  public object DefaultValue
  {
    get
    {
      PXDBListAttributeHelper<TValue>.ValueLabelPairs data = this.Data;
      return (object) (data == null ? default (TValue) : data.DefaultValue);
    }
  }

  private PXDBListAttributeHelper<TValue>.ValueLabelPairs Data
  {
    get
    {
      return PXDatabase.GetSlot<PXDBListAttributeHelper<TValue>.ValueLabelPairs, PXDBListAttributeHelper<TValue>.ListParameters>(this._slotKey, this._parameters, this._parameters.Table);
    }
  }

  public void FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    PXAttributeLevel attributeLevel,
    string fieldName)
  {
    if (attributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    PXDBListAttributeHelper<TValue>.ValueLabelPairs data = this.Data;
    e.ReturnState = this.CreateState(sender, e, data.Values, this.Localize(data.DescriptionFieldName, data.Labels), fieldName, data.DefaultValue);
  }

  protected string[] Localize(string descriptionFieldName, string[] labels)
  {
    if (string.IsNullOrEmpty(descriptionFieldName) || labels == null)
      return (string[]) null;
    string[] strArray = new string[labels.Length];
    for (int index = 0; index < labels.Length; ++index)
      strArray[index] = PXLocalizer.Localize(labels[index], this.Key);
    return strArray;
  }

  protected abstract object CreateState(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    TValue[] values,
    string[] labels,
    string fieldName,
    TValue defaultValue);

  public Dictionary<object, string> ValueLabelDic(PXGraph graph)
  {
    PXDBListAttributeHelper<TValue>.ValueLabelPairs data = this.Data;
    Dictionary<object, string> dictionary = new Dictionary<object, string>(data.Values.Length);
    for (int index = 0; index < data.Values.Length; ++index)
      dictionary.Add((object) data.Values[index], data.Labels[index]);
    return dictionary;
  }

  public string Key => this._locKey;

  public string[] Values => this.Localize(this.Data.DescriptionFieldName, this.Data.Labels);

  /// <exclude />
  private struct ListParameters(
    System.Type table,
    System.Type descriptionField,
    System.Type valueField,
    System.Type defaultValueField,
    KeyValuePair<string, TValue> emptyLabel)
  {
    public readonly System.Type DescriptionField = descriptionField;
    public readonly System.Type ValueField = valueField;
    public readonly System.Type DefaultValueField = defaultValueField;
    public readonly System.Type Table = table;
    public readonly KeyValuePair<string, TValue> EmptyLabel = emptyLabel;

    public ListParameters(
      System.Type table,
      System.Type descriptionField,
      System.Type valueField,
      KeyValuePair<string, TValue> emptyLabel)
      : this(table, descriptionField, valueField, (System.Type) null, emptyLabel)
    {
    }

    public ListParameters(System.Type table, System.Type descriptionField, System.Type valueField)
      : this(table, descriptionField, valueField, (System.Type) null, new KeyValuePair<string, TValue>((string) null, default (TValue)))
    {
    }

    public PXDBListAttributeHelper<TValue>.ListParameters ChangeDefaultValueField(
      System.Type defaultValueField)
    {
      return new PXDBListAttributeHelper<TValue>.ListParameters(this.Table, this.DescriptionField, this.ValueField, defaultValueField, this.EmptyLabel);
    }

    public PXDBListAttributeHelper<TValue>.ListParameters ChangeEmptyLabel(
      string label,
      TValue value)
    {
      return new PXDBListAttributeHelper<TValue>.ListParameters(this.Table, this.DescriptionField, this.ValueField, this.DefaultValueField, new KeyValuePair<string, TValue>(label, value));
    }
  }

  /// <exclude />
  private class ValueLabelPairs : 
    IPrefetchable<PXDBListAttributeHelper<TValue>.ListParameters>,
    IPXCompanyDependent
  {
    private TValue[] _values;
    private string[] _labels;
    private string _descriptionFieldName;
    private TValue _defaultValue;

    public void Prefetch(
      PXDBListAttributeHelper<TValue>.ListParameters parameter)
    {
      System.Type table = parameter.Table;
      System.Type valueField = parameter.ValueField;
      System.Type descriptionField = parameter.DescriptionField;
      System.Type defaultValueField = parameter.DefaultValueField;
      this._defaultValue = default (TValue);
      List<TValue> objList1 = new List<TValue>();
      List<string> stringList1 = new List<string>();
      if (parameter.EmptyLabel.Key != null)
      {
        List<TValue> objList2 = objList1;
        KeyValuePair<string, TValue> emptyLabel = parameter.EmptyLabel;
        TValue obj = emptyLabel.Value;
        objList2.Add(obj);
        List<string> stringList2 = stringList1;
        emptyLabel = parameter.EmptyLabel;
        string key = emptyLabel.Key;
        stringList2.Add(key);
      }
      List<PXDataField> pxDataFieldList = new List<PXDataField>(3);
      pxDataFieldList.Add(new PXDataField(valueField.Name));
      pxDataFieldList.Add(new PXDataField(descriptionField.Name));
      if (defaultValueField != (System.Type) null)
        pxDataFieldList.Add(new PXDataField(defaultValueField.Name));
      PXDataField[] array = pxDataFieldList.ToArray();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti(table, array))
      {
        TValue obj = (TValue) pxDataRecord.GetValue(0);
        objList1.Add(obj);
        stringList1.Add(pxDataRecord.GetString(1));
        if (defaultValueField != (System.Type) null)
        {
          bool? boolean = pxDataRecord.GetBoolean(2);
          if (boolean.HasValue && boolean.Value)
            this._defaultValue = obj;
        }
      }
      this._values = objList1.ToArray();
      this._labels = stringList1.ToArray();
      this._descriptionFieldName = descriptionField.Name;
    }

    public TValue DefaultValue => this._defaultValue;

    public TValue[] Values => this._values;

    public string[] Labels => this._labels;

    public string DescriptionFieldName => this._descriptionFieldName;
  }
}
