// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGenerateAfterAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <summary>
/// When we generate controls based on the DAC fields we can use this attribute to establish controls order.
/// Fields marked with PXGenerateAfterAttribute will come right after the field passed as an argument to PXGenerateAfterAttribute
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class PXGenerateAfterAttribute : Attribute
{
  public readonly string FieldToFollow;

  public PXGenerateAfterAttribute(string fieldName)
  {
    this.FieldToFollow = !string.IsNullOrEmpty(fieldName) ? fieldName : throw new PXArgumentException(fieldName, "PXGenerateAfterAttribute fieldName must be set.");
  }

  public static List<string> GetSortedFieldList(PXCache cache)
  {
    List<string> stringList = new List<string>();
    List<Tuple<string, string>> tupleList1 = new List<Tuple<string, string>>();
    System.Type itemType = cache.GetItemType();
    List<string> sortedFieldList = new List<string>();
    foreach (string field in (List<string>) cache.Fields)
    {
      PropertyInfo property = itemType.GetProperty(field);
      string fieldToFollow = (object) property != null ? property.GetCustomAttribute<PXGenerateAfterAttribute>()?.FieldToFollow : (string) null;
      if (string.IsNullOrEmpty(fieldToFollow))
      {
        sortedFieldList.Add(field);
      }
      else
      {
        if (!cache.Fields.Contains(fieldToFollow))
          throw new PXArgumentException(field, "Invalid PXGenerateAfterAttribute parameter: No such DAC field has been found.");
        tupleList1.Add(Tuple.Create<string, string>(fieldToFollow, field));
      }
    }
    List<Tuple<string, string>> tupleList2;
    for (; tupleList1.Count > 0; tupleList1 = tupleList2)
    {
      tupleList2 = new List<Tuple<string, string>>();
      for (int index = tupleList1.Count - 1; index >= 0; --index)
      {
        Tuple<string, string> tuple = tupleList1[index];
        int num = sortedFieldList.IndexOf(tuple.Item1);
        if (num > -1)
          sortedFieldList.Insert(num + 1, tuple.Item2);
        else
          tupleList2.Add(tuple);
      }
    }
    return sortedFieldList;
  }
}
