// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Extensions.CacheExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Common.Extensions;

public static class CacheExtensions
{
  public static object GetValue(this PXCache cache, object data, Type bqlField)
  {
    string field = cache.GetField(bqlField);
    return cache.GetValue(data, field);
  }

  public static void Enable(this PXCache cache, bool isEnabled)
  {
    cache.AllowUpdate = isEnabled;
    cache.AllowInsert = isEnabled;
    cache.AllowDelete = isEnabled;
  }

  public static IEnumerable<object> InsertAll(this PXCache cache, IEnumerable<object> items)
  {
    items = (IEnumerable<object>) items.ToList<object>();
    foreach (object obj in items)
      cache.Insert(obj);
    return items;
  }

  public static IEnumerable<object> DeleteAll(this PXCache cache, IEnumerable<object> items)
  {
    items = (IEnumerable<object>) items.ToList<object>();
    foreach (object obj in items)
      cache.Delete(obj);
    return items;
  }

  public static void RaiseException<TField>(
    this PXCache cache,
    object row,
    string message,
    object newValue = null,
    PXErrorLevel errorLevel = 4)
    where TField : IBqlField
  {
    PXSetPropertyException propertyException = new PXSetPropertyException(message, errorLevel);
    cache.RaiseExceptionHandling<TField>(row, newValue, (Exception) propertyException);
  }

  public static void RaiseException(
    this PXCache cache,
    string fieldName,
    object row,
    string message,
    object newValue = null,
    PXErrorLevel errorLevel = 4)
  {
    PXSetPropertyException propertyException = new PXSetPropertyException(message, errorLevel);
    cache.RaiseExceptionHandling(fieldName, row, newValue, (Exception) propertyException);
  }

  public static bool HasError<TField>(this PXCache cache, object row, string errorMessage) where TField : IBqlField
  {
    string field1 = cache.GetField(typeof (TField));
    return cache.GetAttributes(row, field1).OfType<IPXInterfaceField>().Any<IPXInterfaceField>((Func<IPXInterfaceField, bool>) (field => field.ErrorText == errorMessage));
  }

  public static bool GetEnabled<TField>(this PXCache cache, object data) where TField : IBqlField
  {
    return cache.GetAttributesOfType<PXUIFieldAttribute>(data, typeof (TField).Name).Single<PXUIFieldAttribute>((Func<PXUIFieldAttribute, bool>) (attribute => attribute != null)).Enabled;
  }
}
