// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Extensions.PXCacheExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.Common.Extensions;

public static class PXCacheExtensions
{
  public static IEqualityComparer<TDAC> GetKeyComparer<TDAC>(this PXCache<TDAC> cache) where TDAC : class, IBqlTable, new()
  {
    return (IEqualityComparer<TDAC>) new CustomComparer<TDAC>(new Func<object, int>(((PXCache) cache).GetObjectHashCode), new Func<object, object, bool>(((PXCache) cache).ObjectsEqual));
  }

  public static void ClearFieldErrors<TField>(this PXCache cache, object record) where TField : IBqlField
  {
    cache.RaiseExceptionHandling<TField>(record, (object) null, (Exception) null);
  }

  public static void ClearFieldSpecificError<TField>(
    this PXCache cache,
    object record,
    string errorMsg,
    params object[] errorMessageArguments)
    where TField : IBqlField
  {
    if (!string.Equals(PXUIFieldAttribute.GetError<TField>(cache, record), PXMessages.LocalizeFormatNoPrefix(errorMsg, errorMessageArguments), StringComparison.CurrentCulture))
      return;
    cache.ClearFieldErrors<TField>(record);
  }

  public static void DisplayFieldError<TField>(
    this PXCache cache,
    object record,
    PXErrorLevel errorLevel,
    string message,
    params object[] errorMessageArguments)
    where TField : IBqlField
  {
    PXSetPropertyException<TField> propertyException = new PXSetPropertyException<TField>(message, errorLevel, errorMessageArguments);
    if (cache.RaiseExceptionHandling<TField>(record, (object) null, (Exception) propertyException))
      throw propertyException;
  }

  public static void DisplayFieldError<TField>(
    this PXCache cache,
    object record,
    string message,
    params object[] errorMessageArguments)
    where TField : IBqlField
  {
    cache.DisplayFieldError<TField>(record, (PXErrorLevel) 4, message, errorMessageArguments);
  }

  public static void DisplayFieldWarning<TField>(
    this PXCache cache,
    object record,
    object newValue,
    string message,
    params object[] errorMessageArguments)
    where TField : IBqlField
  {
    PXSetPropertyException<TField> propertyException = new PXSetPropertyException<TField>(message, (PXErrorLevel) 2, errorMessageArguments);
    if (cache.RaiseExceptionHandling<TField>(record, newValue, (Exception) propertyException))
      throw propertyException;
  }

  public static string GetFullDescription(this PXCache cache, object record)
  {
    StringBuilder stringBuilder = cache != null ? new StringBuilder(cache.GetItemType().Name + ".") : throw new ArgumentNullException(nameof (cache));
    if (record == null)
    {
      stringBuilder.Append(" [NULL]");
      return stringBuilder.ToString();
    }
    foreach (Type bqlField in cache.BqlFields)
      stringBuilder.Append($" {bqlField.Name}: {cache.GetValue(record, bqlField.Name) ?? (object) "[NULL]"};");
    return stringBuilder.ToString();
  }

  /// <summary>
  /// A PXCache extension method that sets all cache permissions to edit data: <see cref="P:PX.Data.PXCache.AllowInsert" />, <see cref="P:PX.Data.PXCache.AllowDelete" />, <see cref="P:PX.Data.PXCache.AllowUpdate" />.
  /// </summary>
  /// <param name="cache">The cache to set permissions.</param>
  /// <param name="allowEdit">True to set all permissions to allow edit of data, false to prohibit edit.</param>
  public static void SetAllEditPermissions(this PXCache cache, bool allowEdit)
  {
    if (cache == null)
      throw new ArgumentNullException(nameof (cache));
    cache.AllowInsert = allowEdit;
    cache.AllowDelete = allowEdit;
    cache.AllowUpdate = allowEdit;
  }

  /// <summary>
  /// Checks if the value of a field has been updated during the round-trip.
  /// </summary>
  public static bool IsValueUpdated<TValue, TField>(
    this PXCache cache,
    object row,
    IEqualityComparer<TValue> comparer = null)
    where TField : IBqlField
  {
    if (cache.GetStatus(row) != 1)
      throw new ArgumentException("Row is not in Updated status", nameof (row));
    if (comparer == null)
      comparer = (IEqualityComparer<TValue>) EqualityComparer<TValue>.Default;
    TValue x = (TValue) cache.GetValue<TField>(row);
    TValue valueOriginal = (TValue) cache.GetValueOriginal<TField>(row);
    return !comparer.Equals(x, valueOriginal);
  }

  /// <summary>
  /// Collects all the non-key fields of the specified cache which are decorated with <see cref="T:PX.Data.PXDBDecimalAttribute" />.
  /// </summary>
  public static IEnumerable<string> GetAllDBDecimalFields(
    this PXCache cache,
    params string[] excludeFields)
  {
    IEnumerable<string> source = ((IEnumerable<string>) cache.Fields).Where<string>((Func<string, bool>) (fld => cache.GetAttributesReadonly(fld).OfType<PXDBDecimalAttribute>().Any<PXDBDecimalAttribute>((Func<PXDBDecimalAttribute, bool>) (a => !((PXDBFieldAttribute) a).IsKey))));
    string[] strArray = excludeFields;
    if ((strArray != null ? (strArray.Length != 0 ? 1 : 0) : 0) != 0)
      source = source.Where<string>((Func<string, bool>) (fld => !((IEnumerable<string>) excludeFields).Any<string>((Func<string, bool>) (efld => string.Equals(fld, efld, StringComparison.OrdinalIgnoreCase)))));
    return source;
  }

  public static bool ObjectsEqualExceptFields<Field1>(this PXCache cache, object a, object b) where Field1 : IBqlField
  {
    return cache.ObjectsEqualExceptFields(a, b, typeof (Field1));
  }

  public static bool ObjectsEqualExceptFields<Field1, Field2>(
    this PXCache cache,
    object a,
    object b)
    where Field1 : IBqlField
    where Field2 : IBqlField
  {
    return cache.ObjectsEqualExceptFields(a, b, typeof (Field1), typeof (Field2));
  }

  public static bool ObjectsEqualExceptFields<Field1, Field2, Field3>(
    this PXCache cache,
    object a,
    object b)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
  {
    return cache.ObjectsEqualExceptFields(a, b, typeof (Field1), typeof (Field2), typeof (Field3));
  }

  public static bool ObjectsEqualExceptFields<Field1, Field2, Field3, Field4>(
    this PXCache cache,
    object a,
    object b)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
  {
    return cache.ObjectsEqualExceptFields(a, b, typeof (Field1), typeof (Field2), typeof (Field3), typeof (Field4));
  }

  public static bool ObjectsEqualExceptFields<Field1, Field2, Field3, Field4, Field5>(
    this PXCache cache,
    object a,
    object b)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
  {
    return cache.ObjectsEqualExceptFields(a, b, typeof (Field1), typeof (Field2), typeof (Field3), typeof (Field4), typeof (Field5));
  }

  public static bool ObjectsEqualExceptFields<Field1, Field2, Field3, Field4, Field5, Field6>(
    this PXCache cache,
    object a,
    object b)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
  {
    return cache.ObjectsEqualExceptFields(a, b, typeof (Field1), typeof (Field2), typeof (Field3), typeof (Field4), typeof (Field5), typeof (Field6));
  }

  public static bool ObjectsEqualExceptFields<Field1, Field2, Field3, Field4, Field5, Field6, Field7>(
    this PXCache cache,
    object a,
    object b)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
  {
    return cache.ObjectsEqualExceptFields(a, b, typeof (Field1), typeof (Field2), typeof (Field3), typeof (Field4), typeof (Field5), typeof (Field6), typeof (Field7));
  }

  public static bool ObjectsEqualExceptFields<Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8>(
    this PXCache cache,
    object a,
    object b)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
    where Field8 : IBqlField
  {
    return cache.ObjectsEqualExceptFields(a, b, typeof (Field1), typeof (Field2), typeof (Field3), typeof (Field4), typeof (Field5), typeof (Field6), typeof (Field7), typeof (Field8));
  }

  public static bool ObjectsEqualExceptFields(
    this PXCache cache,
    object a,
    object b,
    params Type[] exceptFields)
  {
    HashSet<string> exceptFieldsHashSet = ((IEnumerable<Type>) exceptFields).Select<Type, string>((Func<Type, string>) (efld => efld.Name)).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (string str in ((IEnumerable<string>) cache.Fields).Where<string>((Func<string, bool>) (fld => !exceptFieldsHashSet.Contains(fld) && cache.GetAttributesReadonly(fld, false).Any<PXEventSubscriberAttribute>())))
    {
      if (!object.Equals(cache.GetValue(a, str), cache.GetValue(b, str)))
        return false;
    }
    return true;
  }

  public static void VerifyFieldAndRaiseException<T>(
    this PXCache cache,
    object row,
    bool throwError = false)
    where T : IBqlField
  {
    object obj = cache.GetValue<T>(row);
    try
    {
      cache.RaiseFieldVerifying<T>(row, ref obj);
    }
    catch (PXSetPropertyException ex)
    {
      cache.RaiseExceptionHandling<T>(row, obj, (Exception) ex);
      if (!throwError)
        return;
      throw;
    }
  }

  public static void VerifyFieldAndRaiseException(
    this PXCache cache,
    object row,
    string fieldName,
    bool throwError = false)
  {
    object obj = cache.GetValue(row, fieldName);
    try
    {
      cache.RaiseFieldVerifying(fieldName, row, ref obj);
    }
    catch (PXSetPropertyException ex)
    {
      cache.RaiseExceptionHandling(fieldName, row, obj, (Exception) ex);
      if (!throwError)
        return;
      throw;
    }
  }

  /// <summary>
  /// The method returns: display cache name; display field name and user friendly value for each key from DAC.
  /// </summary>
  public static string GetRowDescription(
    this PXCache cache,
    object row,
    string cacheNameSeparator = ": ",
    string keysSeparator = "; ")
  {
    if (cache == null)
      throw new PXArgumentException(nameof (cache));
    if (row == null)
      return (string) null;
    List<object> values = new List<object>();
    foreach (string key in (IEnumerable<string>) cache.Keys)
    {
      object obj = cache.GetValue(row, key);
      if (obj != null)
        cache.RaiseFieldSelecting(key, row, ref obj, true);
      string displayName = obj is PXFieldState pxFieldState ? pxFieldState.DisplayName : (string) null;
      if (!string.IsNullOrEmpty(displayName))
        values.Add((object) $"{displayName} = {obj}");
      else if (obj != null)
        values.Add(obj);
    }
    return cache.DisplayName + cacheNameSeparator + string.Join<object>(keysSeparator, (IEnumerable<object>) values);
  }

  /// <summary>
  /// The method retrievs a mask from the PXStringState, applies it to the value and return user friendly value that can be used in UI
  /// </summary>
  /// <typeparam name="TDac"></typeparam>
  /// <param name="cache"></param>
  /// <param name="dac"></param>
  /// <returns></returns>
  public static string GetFormatedMaskField<TField>(this PXCache cache, IBqlTable dac) where TField : IBqlField
  {
    ExceptionExtensions.ThrowOnNull<PXCache>(cache, nameof (cache), (string) null);
    ExceptionExtensions.ThrowOnNull<IBqlTable>(dac, nameof (dac), (string) null);
    if (!(cache.GetStateExt<TField>((object) dac) is PXStringState stateExt))
      return string.Empty;
    string str = cache.GetValue<TField>((object) dac)?.ToString();
    return string.IsNullOrWhiteSpace(str) || string.IsNullOrWhiteSpace(stateExt.InputMask) ? str : Mask.Format(stateExt.InputMask, str);
  }

  /// <summary>
  /// The method sets the specified value to the field of the record with raising all events (FieldVerifying, FieldUpdated, RowUpdated)
  /// and restores the status of the record in the cache to original.
  /// </summary>
  public static void RaiseEventsOnFieldChanging<TField>(
    this PXCache cache,
    object row,
    object value)
    where TField : IBqlField
  {
    PXEntryStatus status = cache.GetStatus(row);
    try
    {
      object copy = cache.CreateCopy(row);
      cache.SetValueExt<TField>(row, value);
      cache.RaiseRowUpdated(row, copy);
    }
    finally
    {
      cache.SetStatus(row, status);
    }
  }

  public static TTable LiteUpdate<TTable>(
    this PXCache cache,
    TTable row,
    Action<PXCache, TTable> update,
    bool skipRowUpdatedEvent = false)
    where TTable : class, IBqlTable, new()
  {
    ExceptionExtensions.ThrowOnNull<PXCache>(cache, nameof (cache), (string) null);
    ExceptionExtensions.ThrowOnNull<TTable>(row, nameof (row), (string) null);
    ExceptionExtensions.ThrowOnNull<Action<PXCache, TTable>>(update, nameof (update), (string) null);
    object copy = skipRowUpdatedEvent ? (object) null : cache.CreateCopy((object) row);
    update(cache, row);
    GraphHelper.MarkUpdated(cache, (object) row);
    cache.IsDirty = true;
    if (!skipRowUpdatedEvent)
      cache.RaiseRowUpdated((object) row, copy);
    return row;
  }

  public static TTable LiteUpdate<TTable>(
    this PXCache cache,
    TTable row,
    Action<ValueSetter<TTable>> update,
    bool skipRowUpdatedEvent = false)
    where TTable : class, IBqlTable, new()
  {
    ExceptionExtensions.ThrowOnNull<Action<ValueSetter<TTable>>>(update, nameof (update), (string) null);
    return cache.LiteUpdate<TTable>(row, (Action<PXCache, TTable>) ((c, r) => update(PXCacheEx.GetSetterFor<TTable>(c, r))), skipRowUpdatedEvent);
  }

  public static TTable LiteUpdate<TTable>(
    this PXCache cache,
    TTable row,
    Action<TTable> update,
    bool skipRowUpdatedEvent = false)
    where TTable : class, IBqlTable, new()
  {
    ExceptionExtensions.ThrowOnNull<Action<TTable>>(update, nameof (update), (string) null);
    return cache.LiteUpdate<TTable>(row, (Action<PXCache, TTable>) ((c, r) => update(r)), skipRowUpdatedEvent);
  }

  public static void HoldRows<TEntity>(this PXCache cache, IEnumerable<TEntity> rows) where TEntity : IBqlTable
  {
    foreach (TEntity row in rows)
      GraphHelper.Hold(cache, (object) row);
  }

  public static void UnHoldRows<TEntity>(this PXCache cache, IEnumerable<TEntity> rows)
  {
    bool flag = false;
    foreach (TEntity row in rows)
    {
      if (cache.GetStatus((object) row) == 5)
      {
        cache.SetStatus((object) row, (PXEntryStatus) 0);
        cache.Remove((object) row);
        flag = true;
      }
    }
    if (!flag)
      return;
    cache.ClearQueryCache();
  }
}
