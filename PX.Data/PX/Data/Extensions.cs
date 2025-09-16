// Decompiled with JetBrains decompiler
// Type: PX.Data.Extensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;

#nullable disable
namespace PX.Data;

public static class Extensions
{
  public static object GetValue(this PXDataRecord record, int i, System.Type type)
  {
    if (record == null)
      throw new ArgumentNullException(nameof (record));
    System.Type type1 = !(type == (System.Type) null) ? Nullable.GetUnderlyingType(type) : throw new ArgumentNullException(nameof (type));
    if ((object) type1 == null)
      type1 = type;
    type = type1;
    switch (System.Type.GetTypeCode(type))
    {
      case TypeCode.Object:
        if (type == typeof (Guid))
          return (object) record.GetGuid(i);
        if (type == typeof (byte[]))
          return (object) record.GetBytes(i);
        break;
      case TypeCode.Boolean:
        return (object) record.GetBoolean(i);
      case TypeCode.Char:
        return (object) record.GetChar(i);
      case TypeCode.Byte:
        return (object) record.GetByte(i);
      case TypeCode.Int16:
        return (object) record.GetInt16(i);
      case TypeCode.Int32:
        return (object) record.GetInt32(i);
      case TypeCode.Int64:
        return (object) record.GetInt64(i);
      case TypeCode.Single:
        return (object) record.GetFloat(i);
      case TypeCode.Double:
        return (object) record.GetDouble(i);
      case TypeCode.Decimal:
        return (object) record.GetDecimal(i);
      case TypeCode.DateTime:
        return (object) record.GetDateTime(i);
      case TypeCode.String:
        return (object) record.GetString(i);
    }
    return record.GetValue(i);
  }

  internal static Query GetQuery(
    this BqlCommand command,
    PXGraph graph,
    long topLimit,
    long skipOffset = 0)
  {
    return command.GetQuery(graph, (PXView) null, topLimit, skipOffset);
  }

  internal static Query GetQuery(
    this BqlCommand command,
    PXGraph graph,
    PXView view,
    long topCount,
    long skip = 0)
  {
    Query query = command.GetQuery(graph, view);
    if (skip > 0L)
      query.Offset((int) skip);
    if (topCount > 0L)
      query.Limit((int) topCount);
    return query;
  }
}
