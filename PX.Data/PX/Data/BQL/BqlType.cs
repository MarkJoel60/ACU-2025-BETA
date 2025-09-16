// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable enable
namespace PX.Data.BQL;

public static class BqlType
{
  public static bool IsStronglyTyped(System.Type? operand)
  {
    return operand != (System.Type) null && typeof (IImplement<IBqlDataType>).IsAssignableFrom(operand);
  }

  public static System.Type? GetCorrespondingDotNetType(System.Type operand)
  {
    if (operand == (System.Type) null)
      return (System.Type) null;
    if (typeof (IImplement<IBqlBool>).IsAssignableFrom(operand))
      return typeof (bool);
    if (typeof (IImplement<IBqlByte>).IsAssignableFrom(operand))
      return typeof (byte);
    if (typeof (IImplement<IBqlShort>).IsAssignableFrom(operand))
      return typeof (short);
    if (typeof (IImplement<IBqlInt>).IsAssignableFrom(operand))
      return typeof (int);
    if (typeof (IImplement<IBqlLong>).IsAssignableFrom(operand))
      return typeof (long);
    if (typeof (IImplement<IBqlFloat>).IsAssignableFrom(operand))
      return typeof (float);
    if (typeof (IImplement<IBqlDouble>).IsAssignableFrom(operand))
      return typeof (double);
    if (typeof (IImplement<IBqlDecimal>).IsAssignableFrom(operand))
      return typeof (Decimal);
    if (typeof (IImplement<IBqlGuid>).IsAssignableFrom(operand))
      return typeof (Guid);
    if (typeof (IImplement<IBqlDateTime>).IsAssignableFrom(operand))
      return typeof (System.DateTime);
    if (typeof (IImplement<IBqlString>).IsAssignableFrom(operand))
      return typeof (string);
    if (typeof (IImplement<IBqlByteArray>).IsAssignableFrom(operand))
      return typeof (byte[]);
    return typeof (IImplement<BqlPlaceholder.IBqlAny>).IsAssignableFrom(operand) ? typeof (object) : (System.Type) null;
  }
}
