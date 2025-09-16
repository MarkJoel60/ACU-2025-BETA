// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Argument
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.BQL;

/// <summary>
/// Allows to introduce a query argument, that will be replaced by a value passed from the UI control.
/// Is a strongly typed version of the <see cref="T:PX.Data.Argument`1" />.
/// </summary>
public static class Argument
{
  /// <summary>
  /// Indicates that the argument is of <see cref="T:PX.Data.BQL.IBqlBool" /> type.
  /// </summary>
  public sealed class AsBool : BqlParameter<Argument<bool?>, IBqlBool>
  {
  }

  /// <summary>
  /// Indicates that the argument is of <see cref="T:PX.Data.BQL.IBqlByte" /> type.
  /// </summary>
  public sealed class AsByte : BqlParameter<Argument<byte?>, IBqlByte>
  {
  }

  /// <summary>
  /// Indicates that the argument is of <see cref="T:PX.Data.BQL.IBqlShort" /> type.
  /// </summary>
  public sealed class AsShort : BqlParameter<Argument<short?>, IBqlShort>
  {
  }

  /// <summary>
  /// Indicates that the argument i of <see cref="T:PX.Data.BQL.IBqlInt" /> type.
  /// </summary>
  public sealed class AsInt : BqlParameter<Argument<int?>, IBqlInt>
  {
  }

  /// <summary>
  /// Indicates that the argument is of <see cref="T:PX.Data.BQL.IBqlLong" /> type.
  /// </summary>
  public sealed class AsLong : BqlParameter<Argument<long?>, IBqlLong>
  {
  }

  /// <summary>
  /// Indicates that the argument is of <see cref="T:PX.Data.BQL.IBqlFloat" /> type.
  /// </summary>
  public sealed class AsFloat : BqlParameter<Argument<float?>, IBqlFloat>
  {
  }

  /// <summary>
  /// Indicates that the argument is of <see cref="T:PX.Data.BQL.IBqlDouble" /> type.
  /// </summary>
  public sealed class AsDouble : BqlParameter<Argument<double?>, IBqlDouble>
  {
  }

  /// <summary>
  /// Indicates that the argument is of <see cref="T:PX.Data.BQL.IBqlDecimal" /> type.
  /// </summary>
  public sealed class AsDecimal : BqlParameter<Argument<Decimal?>, IBqlDecimal>
  {
  }

  /// <summary>
  /// Indicates that the argument is of <see cref="T:PX.Data.BQL.IBqlDateTime" /> type.
  /// </summary>
  public sealed class AsDateTime : BqlParameter<Argument<System.DateTime?>, IBqlDateTime>
  {
  }

  /// <summary>
  /// Indicates that the argument is of <see cref="T:PX.Data.BQL.IBqlGuid" /> type.
  /// </summary>
  public sealed class AsGuid : BqlParameter<Argument<Guid?>, IBqlGuid>
  {
  }

  /// <summary>
  /// Indicates that the argument is of <see cref="T:PX.Data.BQL.IBqlString" /> type.
  /// </summary>
  public sealed class AsString : BqlParameter<Argument<string>, IBqlString>
  {
  }
}
