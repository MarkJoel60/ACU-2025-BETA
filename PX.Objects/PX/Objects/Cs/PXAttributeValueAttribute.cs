// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.PXAttributeValueAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PX.Objects.CS;

internal class PXAttributeValueAttribute : PXDBStringAttribute
{
  private Dictionary<string, Dictionary<string, short>> AttributesWithValues
  {
    get
    {
      return PXDatabase.GetSlot<PXAttributeValueAttribute.ValuesWithSortOrder>(nameof (PXAttributeValueAttribute), new Type[2]
      {
        typeof (CSAttribute),
        typeof (CSAttributeDetail)
      }).AllValues;
    }
  }

  private List<string> MultiSelectAttributes
  {
    get
    {
      return PXDatabase.GetSlot<PXAttributeValueAttribute.ValuesWithSortOrder>(nameof (PXAttributeValueAttribute), new Type[1]
      {
        typeof (CSAttribute)
      }).MultiSelectAttributes;
    }
  }

  public PXAttributeValueAttribute()
    : base((int) byte.MaxValue)
  {
    this.IsUnicode = true;
  }

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue is DateTime)
      e.NewValue = (object) Convert.ToString(e.NewValue, (IFormatProvider) CultureInfo.InvariantCulture);
    string attributeId = e.Row is CSAnswers row ? row.AttributeID : (string) null;
    if (!string.IsNullOrWhiteSpace(attributeId) && e.NewValue != null && this.MultiSelectAttributes.Contains(attributeId))
    {
      Dictionary<string, short> attributesWithValue = this.AttributesWithValues[attributeId];
      IEnumerable<string> realValues = ((IEnumerable<string>) (e.NewValue as string).Split(',')).Where<string>((Func<string, bool>) (val => !string.IsNullOrEmpty(val)));
      e.NewValue = (object) string.Join(",", attributesWithValue.Where<KeyValuePair<string, short>>((Func<KeyValuePair<string, short>, bool>) (val => realValues.Contains<string>(val.Key))).OrderBy<KeyValuePair<string, short>, short>((Func<KeyValuePair<string, short>, short>) (val => val.Value)).ThenBy<KeyValuePair<string, short>, string>((Func<KeyValuePair<string, short>, string>) (val => val.Key)).Select<KeyValuePair<string, short>, string>((Func<KeyValuePair<string, short>, string>) (val => val.Key)));
    }
    base.FieldUpdating(sender, e);
  }

  private class ValuesWithSortOrder : IPrefetchable, IPXCompanyDependent
  {
    public Dictionary<string, Dictionary<string, short>> AllValues { get; private set; }

    public List<string> MultiSelectAttributes { get; private set; }

    public void Prefetch()
    {
      this.MultiSelectAttributes = PXDatabase.SelectMulti<CSAttribute>(new PXDataField[2]
      {
        (PXDataField) new PXDataField<CSAttribute.attributeID>(),
        (PXDataField) new PXDataField<CSAttribute.controlType>()
      }).Where<PXDataRecord>((Func<PXDataRecord, bool>) (r => r.GetInt32(1).GetValueOrDefault() == 6)).Select<PXDataRecord, string>((Func<PXDataRecord, string>) (r => r.GetString(0))).ToList<string>();
      List<CSAttributeDetail> list = PXDatabase.SelectMulti<CSAttributeDetail>(new PXDataField[3]
      {
        (PXDataField) new PXDataField<CSAttributeDetail.attributeID>(),
        (PXDataField) new PXDataField<CSAttributeDetail.valueID>(),
        (PXDataField) new PXDataField<CSAttributeDetail.sortOrder>()
      }).Where<PXDataRecord>((Func<PXDataRecord, bool>) (r => this.MultiSelectAttributes.Contains(r.GetString(0)))).Select<PXDataRecord, CSAttributeDetail>((Func<PXDataRecord, CSAttributeDetail>) (r => new CSAttributeDetail()
      {
        AttributeID = r.GetString(0),
        ValueID = r.GetString(1),
        SortOrder = r.GetInt16(2)
      })).ToList<CSAttributeDetail>();
      this.AllValues = new Dictionary<string, Dictionary<string, short>>();
      foreach (string multiSelectAttribute in this.MultiSelectAttributes)
      {
        string attribute = multiSelectAttribute;
        Dictionary<string, short> dictionary = new Dictionary<string, short>();
        foreach (CSAttributeDetail csAttributeDetail in list.Where<CSAttributeDetail>((Func<CSAttributeDetail, bool>) (a => a.AttributeID == attribute)))
          dictionary[csAttributeDetail.ValueID] = csAttributeDetail.SortOrder.GetValueOrDefault();
        this.AllValues[attribute] = dictionary;
      }
    }
  }
}
