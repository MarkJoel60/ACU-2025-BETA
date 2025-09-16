// Decompiled with JetBrains decompiler
// Type: PX.Data.Database.PXDataRecordExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Data.Database;

internal static class PXDataRecordExtension
{
  public static bool? GetBoolean(this PXDataRecord dr, string name)
  {
    return dr.GetBoolean(dr.GetOrdinal(name));
  }

  public static byte? GetByte(this PXDataRecord dr, string name) => dr.GetByte(dr.GetOrdinal(name));

  public static long GetBytes(
    this PXDataRecord dr,
    string name,
    long fieldOffset,
    byte[] buffer,
    int bufferoffset,
    int length)
  {
    return dr.GetBytes(dr.GetOrdinal(name), fieldOffset, buffer, bufferoffset, length);
  }

  public static byte[] GetTimeStamp(this PXDataRecord dr, string name)
  {
    return dr.GetTimeStamp(dr.GetOrdinal(name));
  }

  public static byte[] GetBytes(this PXDataRecord dr, string name)
  {
    return dr.GetBytes(dr.GetOrdinal(name));
  }

  public static char? GetChar(this PXDataRecord dr, string name) => dr.GetChar(dr.GetOrdinal(name));

  public static long GetChars(
    this PXDataRecord dr,
    string name,
    long fieldoffset,
    char[] buffer,
    int bufferoffset,
    int length)
  {
    return dr.GetChars(dr.GetOrdinal(name), fieldoffset, buffer, bufferoffset, length);
  }

  public static System.DateTime? GetDateTime(this PXDataRecord dr, string name)
  {
    return dr.GetDateTime(dr.GetOrdinal(name));
  }

  public static Decimal? GetDecimal(this PXDataRecord dr, string name)
  {
    return dr.GetDecimal(dr.GetOrdinal(name));
  }

  public static double? GetDouble(this PXDataRecord dr, string name)
  {
    return dr.GetDouble(dr.GetOrdinal(name));
  }

  public static float? GetFloat(this PXDataRecord dr, string name)
  {
    return dr.GetFloat(dr.GetOrdinal(name));
  }

  public static Guid? GetGuid(this PXDataRecord dr, string name) => dr.GetGuid(dr.GetOrdinal(name));

  public static short? GetInt16(this PXDataRecord dr, string name)
  {
    return dr.GetInt16(dr.GetOrdinal(name));
  }

  public static int? GetInt32(this PXDataRecord dr, string name)
  {
    return dr.GetInt32(dr.GetOrdinal(name));
  }

  public static long? GetInt64(this PXDataRecord dr, string name)
  {
    return dr.GetInt64(dr.GetOrdinal(name));
  }

  public static string GetString(this PXDataRecord dr, string name)
  {
    return dr.GetString(dr.GetOrdinal(name));
  }

  public static IEnumerable<PXDataRecord> Bind<T>(
    this IEnumerable<PXDataRecord> rows,
    T obj,
    params (Expression<Func<T, object>> PropertyGetter, string Name)[] bindings)
    where T : class
  {
    int bindingsCount = bindings.Length;
    Delegate[] methods = new Delegate[bindingsCount];
    (int, string)[] ordinals = new (int, string)[bindingsCount];
    for (int index = 0; index < bindingsCount; ++index)
    {
      Expression<Func<T, object>> propertyGetter = bindings[index].PropertyGetter;
      MemberExpression memberExpression = propertyGetter.Body.NodeType != ExpressionType.Convert ? propertyGetter.Body as MemberExpression : ((UnaryExpression) propertyGetter.Body).Operand as MemberExpression;
      if (memberExpression == null)
        throw new ArgumentException(nameof (bindings));
      ParameterExpression parameterExpression = Expression.Parameter(typeof (T), "o");
      ParameterExpression row = Expression.Parameter(typeof (PXDataRecord), "row");
      ParameterExpression ordinal = Expression.Parameter(typeof (int), "i");
      Expression valueExpression = PXDataRecordExtension.GetValueExpression(memberExpression.Type, (Expression) row, (Expression) ordinal);
      Delegate @delegate = Expression.Lambda((Expression) Expression.Assign((Expression) Expression.Property((Expression) parameterExpression, memberExpression.Member.Name), valueExpression), parameterExpression, row, ordinal).Compile();
      methods[index] = @delegate;
      ordinals[index] = (-1, bindings[index].Name ?? memberExpression.Member.Name);
    }
    foreach (PXDataRecord row in rows)
    {
      for (int index = 0; index < bindingsCount; ++index)
      {
        (int ordinal, string fieldName) = ordinals[index];
        if (ordinal == -1)
        {
          ordinal = row.GetOrdinal(fieldName);
          ordinals[index] = (ordinal, fieldName);
        }
        methods[index].DynamicInvoke((object) (T) obj, (object) row, (object) ordinal);
      }
      yield return row;
    }
  }

  private static Expression GetValueExpression(System.Type valueType, Expression row, Expression ordinal)
  {
    string name = !(valueType == typeof (bool?)) ? (!(valueType == typeof (byte?)) ? (!(valueType == typeof (byte[])) ? (!(valueType == typeof (char?)) ? (!(valueType == typeof (System.DateTime?)) ? (!(valueType == typeof (Decimal?)) ? (!(valueType == typeof (double?)) ? (!(valueType == typeof (float?)) ? (!(valueType == typeof (Guid?)) ? (!(valueType == typeof (short?)) ? (!(valueType == typeof (int?)) ? (!(valueType == typeof (long?)) ? (!(valueType == typeof (string)) ? "GetValue" : "GetString") : "GetInt64") : "GetInt32") : "GetInt16") : "GetGuid") : "GetFloat") : "GetDouble") : "GetDecimal") : "GetDateTime") : "GetChar") : "GetBytes") : "GetByte") : "GetBoolean";
    MethodInfo method = typeof (PXDataRecord).GetMethod(name, new System.Type[1]
    {
      typeof (int)
    });
    Expression expression = (Expression) Expression.Call(row, method, ordinal);
    if (name == "GetValue")
      expression = (Expression) Expression.Convert(expression, valueType);
    return expression;
  }

  /// <summary>
  /// This and other Bind method use convention: names of parameters in <paramref name="creator" /> corresponds to column names (aliases) in SQL query
  /// </summary>
  public static IEnumerable<T> Bind<T, T1>(
    this IEnumerable<PXDataRecord> rows,
    Expression<Func<T1, T>> creator)
  {
    return rows.Bind<T>((LambdaExpression) creator);
  }

  public static IEnumerable<T> Bind<T, T1, T2>(
    this IEnumerable<PXDataRecord> rows,
    Expression<Func<T1, T2, T>> creator)
  {
    return rows.Bind<T>((LambdaExpression) creator);
  }

  public static IEnumerable<T> Bind<T, T1, T2, T3>(
    this IEnumerable<PXDataRecord> rows,
    Expression<Func<T1, T2, T3, T>> creator)
  {
    return rows.Bind<T>((LambdaExpression) creator);
  }

  public static IEnumerable<T> Bind<T, T1, T2, T3, T4>(
    this IEnumerable<PXDataRecord> rows,
    Expression<Func<T1, T2, T3, T4, T>> creator)
  {
    return rows.Bind<T>((LambdaExpression) creator);
  }

  public static IEnumerable<T> Bind<T, T1, T2, T3, T4, T5>(
    this IEnumerable<PXDataRecord> rows,
    Expression<Func<T1, T2, T3, T4, T5, T>> creator)
  {
    return rows.Bind<T>((LambdaExpression) creator);
  }

  public static IEnumerable<T> Bind<T, T1, T2, T3, T4, T5, T6>(
    this IEnumerable<PXDataRecord> rows,
    Expression<Func<T1, T2, T3, T4, T5, T6, T>> creator)
  {
    return rows.Bind<T>((LambdaExpression) creator);
  }

  public static IEnumerable<T> Bind<T, T1, T2, T3, T4, T5, T6, T7>(
    this IEnumerable<PXDataRecord> rows,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T>> creator)
  {
    return rows.Bind<T>((LambdaExpression) creator);
  }

  public static IEnumerable<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8>(
    this IEnumerable<PXDataRecord> rows,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T>> creator)
  {
    return rows.Bind<T>((LambdaExpression) creator);
  }

  private static IEnumerable<T> Bind<T>(
    this IEnumerable<PXDataRecord> rows,
    LambdaExpression lambda)
  {
    ParameterExpression[] array = lambda.Parameters.ToArray<ParameterExpression>();
    int paramsCount = lambda.Parameters.Count;
    (int, string)[] ordinals = new (int, string)[paramsCount];
    ParameterExpression[] parameterExpressionArray = new ParameterExpression[paramsCount + 1];
    Expression[] expressionArray = new Expression[paramsCount];
    ParameterExpression row1 = Expression.Parameter(typeof (PXDataRecord), "row");
    parameterExpressionArray[0] = row1;
    for (int index = 0; index < paramsCount; ++index)
    {
      parameterExpressionArray[index + 1] = Expression.Parameter(typeof (int), "ord" + index.ToString());
      expressionArray[index] = PXDataRecordExtension.GetValueExpression(array[index].Type, (Expression) row1, (Expression) parameterExpressionArray[index + 1]);
      ordinals[index] = (-1, array[index].Name);
    }
    Delegate creator = Expression.Lambda((Expression) Expression.Invoke((Expression) lambda, expressionArray), parameterExpressionArray).Compile();
    object[] values = new object[paramsCount + 1];
    foreach (PXDataRecord row2 in rows)
    {
      values[0] = (object) row2;
      for (int index = 0; index < paramsCount; ++index)
      {
        (int ordinal, string fieldName) = ordinals[index];
        if (ordinal == -1)
        {
          ordinal = row2.GetOrdinal(fieldName);
          ordinals[index] = (ordinal, fieldName);
        }
        values[index + 1] = (object) ordinal;
      }
      yield return (T) creator.DynamicInvoke(values);
    }
  }
}
