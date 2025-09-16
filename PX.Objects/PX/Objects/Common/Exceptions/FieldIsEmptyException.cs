// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Exceptions.FieldIsEmptyException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.Common.Exceptions;

/// <exclude />
public class FieldIsEmptyException : PXException
{
  private const string FieldSeparator = " - ";
  private const string KeysSeparator = " ";

  public Type RowType { get; protected set; }

  public Type FieldType { get; protected set; }

  public static string GetErrorText(
    PXCache cache,
    object row,
    Type fieldType,
    params object[] keys)
  {
    return string.Format(PXMessages.LocalizeNoPrefix("'{0}' cannot be empty."), (object) FieldIsEmptyException.GetFieldDescription(cache, row, fieldType, keys));
  }

  public FieldIsEmptyException(PXCache cache, object row, Type fieldType, params object[] keys)
    : base("'{0}' cannot be empty.", new object[1]
    {
      (object) FieldIsEmptyException.GetFieldDescription(cache, row, fieldType, keys)
    })
  {
    this.RowType = cache.GetItemType();
    this.FieldType = fieldType;
  }

  public FieldIsEmptyException(
    PXCache cache,
    object row,
    Type fieldType,
    bool getKeyValuesFromRow)
    : base("'{0}' cannot be empty.", new object[1]
    {
      (object) FieldIsEmptyException.GetFieldDescription(cache, row, fieldType, (object[]) null, getKeyValuesFromRow)
    })
  {
    this.RowType = cache.GetItemType();
  }

  public FieldIsEmptyException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  private static string GetFieldDescription(
    PXCache cache,
    object row,
    Type fieldType,
    object[] keys,
    bool getKeyValuesFromRow = false)
  {
    string str = cache.DisplayName;
    if (!getKeyValuesFromRow)
    {
      if (((IEnumerable<object>) keys).Any<object>())
        str = $"{str} {string.Join(" ", keys)}";
    }
    else if (cache.BqlKeys.Any<Type>())
      str = $"{str} {string.Join<object>(" ", cache.BqlKeys.Select<Type, object>((Func<Type, object>) (k => cache.GetValue(row, k.Name))))}";
    return str + " - " + ((PXFieldState) cache.GetStateExt(row, fieldType.Name)).DisplayName;
  }
}
