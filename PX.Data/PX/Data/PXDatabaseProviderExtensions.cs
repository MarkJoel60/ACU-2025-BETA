// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDatabaseProviderExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using PX.DbServices.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace PX.Data;

internal static class PXDatabaseProviderExtensions
{
  internal static T SelectRecord<T>(
    this PXDatabaseProvider provider,
    params PXDataField[] restricts)
    where T : class, IBqlTable, new()
  {
    (PXDataFieldMapping[] dataFieldMappingArray, PXDataField[] pxDataFieldArray) = provider.GetDataFieldsForTable<T>(restricts);
    using (PXDataRecord rec = provider.SelectSingle(typeof (T), ((IEnumerable<PXDataField>) pxDataFieldArray).ToArray<PXDataField>()))
      return rec.GetRow<T>(dataFieldMappingArray);
  }

  private static (PXDataFieldMapping[] Fields, PXDataField[] Selection) GetDataFieldsForTable<T>(
    this PXDatabaseProvider provider,
    params PXDataField[] restricts)
    where T : class, IBqlTable, new()
  {
    PXDataFieldMapping[] array = provider.GetDataFieldsForTable<T>().ToArray<PXDataFieldMapping>();
    return (array, ((IEnumerable<PXDataField>) array).Union<PXDataField>((IEnumerable<PXDataField>) restricts).ToArray<PXDataField>());
  }

  private static T GetRow<T>(this PXDataRecord rec, PXDataFieldMapping[] fields) where T : class, IBqlTable, new()
  {
    if (rec == null)
      return default (T);
    Dictionary<string, System.Type> classFieldTypes = PXCache<T>.GetClassFieldTypes();
    Dictionary<string, object> values = new Dictionary<string, object>();
    for (int i = 0; i < fields.Length; ++i)
    {
      if (!rec.IsDBNull(i))
      {
        string propertyName = fields[i].PropertyName;
        System.Type type = classFieldTypes[propertyName];
        object obj = !(type == typeof (bool)) ? (!(type == typeof (byte)) ? (!(type == typeof (byte[])) ? (!(type == typeof (char)) ? (!(type == typeof (System.DateTime)) ? (!(type == typeof (Decimal)) ? (!(type == typeof (double)) ? (!(type == typeof (float)) ? (!(type == typeof (Guid)) ? (!(type == typeof (short)) ? (!(type == typeof (int)) ? (!(type == typeof (long)) ? (!(type == typeof (string)) ? rec.GetValue(i) : (object) rec.GetString(i)) : (object) rec.GetInt64(i)) : (object) rec.GetInt32(i)) : (object) rec.GetInt16(i)) : (object) rec.GetGuid(i)) : (object) rec.GetFloat(i)) : (object) rec.GetDouble(i)) : (object) rec.GetDecimal(i)) : (object) rec.GetDateTime(i)) : (object) rec.GetChar(i)) : (object) rec.GetBytes(i)) : (object) rec.GetByte(i)) : (object) rec.GetBoolean(i);
        values[propertyName] = obj;
      }
    }
    return PXCache<T>.FillClassFieldValues(values);
  }

  internal static IEnumerable<T> SelectRecords<T>(
    this PXDatabaseProvider provider,
    params PXDataField[] restricts)
    where T : class, IBqlTable, new()
  {
    (PXDataFieldMapping[] dataFieldMappingArray, PXDataField[] pxDataFieldArray) = provider.GetDataFieldsForTable<T>(restricts);
    foreach (PXDataRecord rec in provider.SelectMulti(typeof (T), ((IEnumerable<PXDataField>) pxDataFieldArray).ToArray<PXDataField>()))
    {
      if (rec != null)
        yield return rec.GetRow<T>(dataFieldMappingArray);
    }
  }

  internal static IEnumerable<PXDataFieldMapping> GetDataFieldsForTable<T>(
    this PXDatabaseProvider provider)
    where T : class, IBqlTable, new()
  {
    TableHeader header = provider.GetTableStructure(typeof (T).Name);
    if (header != null)
    {
      foreach (PXEventSubscriberAttribute subscriberAttribute in PXCache<T>.GetAttributesStatic())
      {
        if (subscriberAttribute is PXDBFieldAttribute pxdbFieldAttribute)
        {
          bool flag = false;
          bool isLong = false;
          if (subscriberAttribute is PXDBLocalizableStringAttribute localizableStringAttribute)
          {
            flag = true;
            isLong = localizableStringAttribute.Length <= 0 || localizableStringAttribute.Length > 256 /*0x0100*/;
          }
          string columnName = string.IsNullOrEmpty(pxdbFieldAttribute.DatabaseFieldName) ? pxdbFieldAttribute.FieldName : pxdbFieldAttribute.DatabaseFieldName;
          TableColumn tableColumn = header.Columns.FirstOrDefault<TableColumn>((System.Func<TableColumn, bool>) (c => string.Equals(((TableEntityBase) c).Name, columnName, StringComparison.InvariantCultureIgnoreCase)));
          if (tableColumn != null && tableColumn.Type != SqlDbType.Timestamp)
          {
            SQLExpression fieldName = (SQLExpression) new Column(columnName, (Table) null, (PXDbType) tableColumn.Type);
            if (flag)
              fieldName = PXDBLocalizableStringAttribute.GetValueSelect(typeof (T).Name, pxdbFieldAttribute.FieldName, isLong).Expression;
            yield return new PXDataFieldMapping(fieldName, pxdbFieldAttribute.FieldName);
          }
        }
      }
    }
  }
}
