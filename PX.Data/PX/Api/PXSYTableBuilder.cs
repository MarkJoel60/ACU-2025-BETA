// Decompiled with JetBrains decompiler
// Type: PX.Api.PXSYTableBuilder
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PX.Api;

internal class PXSYTableBuilder : ISYTableAddGet
{
  private readonly List<object> PrimaryViewRows = new List<object>();
  private readonly RowList Rows = new RowList();
  private readonly string[] Headers;
  private readonly string Locale;

  public PXSYTableBuilder(string[] headers, string locale)
  {
    this.Headers = headers;
    this.Locale = locale;
  }

  public PXSYTableBuilder(string[] headers)
    : this(headers, (string) null)
  {
  }

  public void AddRow(object primaryViewRow = null)
  {
    this.Rows.Add(new Dictionary<string, RowField>());
    this.PrimaryViewRows.Add(primaryViewRow);
  }

  public void RemoveRow()
  {
    int index = this.Rows.Count - 1;
    this.Rows.RemoveAt(index);
    this.PrimaryViewRows.RemoveAt(index);
  }

  public void AddValue(
    string hdr,
    object v,
    PXFieldState state,
    PXDBLocalizableStringAttribute.Translations translations = null)
  {
    if (!((IEnumerable<string>) this.Headers).Contains<string>(hdr))
      throw new PXException("Invalid hdr; hdr = " + hdr);
    this.Rows.Last<Dictionary<string, RowField>>()[hdr] = new RowField(v, state, translations);
  }

  public object GetValue(string hdr)
  {
    if (!((IEnumerable<string>) this.Headers).Contains<string>(hdr))
      throw new PXException("Invalid hdr");
    RowField rowField;
    return this.Rows.Last<Dictionary<string, RowField>>().TryGetValue(hdr, out rowField) ? rowField.Value : (object) null;
  }

  public PXSYTablePr GetTable(bool keepNullValues = false)
  {
    PXSYTablePr table = new PXSYTablePr(((IEnumerable<string>) this.Headers).Distinct<string>(), (IEnumerable<object>) this.PrimaryViewRows);
    foreach (Dictionary<string, RowField> row1 in (List<Dictionary<string, RowField>>) this.Rows)
    {
      PXSYRow row2 = table.CreateRow();
      table.Add(row2);
      foreach (string header in this.Headers)
      {
        RowField rowField;
        if (row1.TryGetValue(header, out rowField))
        {
          object nativeValue = rowField.Value;
          string valueFromNativeValue = nativeValue == null & keepNullValues ? (string) null : PXSYTableBuilder.GetStringValueFromNativeValue(nativeValue, this.Locale);
          row2.SetItem(header, new PXSYItem(valueFromNativeValue, nativeValue, rowField.State, false, rowField.Translations));
        }
        else
          row2.SetItem(header, new PXSYItem(keepNullValues ? (string) null : "", (object) null, (PXFieldState) null, true, (PXDBLocalizableStringAttribute.Translations) null));
      }
    }
    return table;
  }

  public static string GetStringValueFromNativeValue(
    object nativeValue,
    string locale,
    bool useInvariantCulture = false)
  {
    CultureInfo provider = useInvariantCulture ? CultureInfo.InvariantCulture : (string.IsNullOrEmpty(locale) ? CultureInfo.CurrentCulture : CultureInfo.GetCultureInfo(locale));
    return Convert.ToString(nativeValue, (IFormatProvider) provider);
  }
}
