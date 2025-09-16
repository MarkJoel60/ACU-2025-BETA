// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.P
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.BQL;

/// <summary>
/// Allows to introduce a query parameter, that will be replaced by a value passed to the <see cref="M:PX.Data.PXSelectBase`1.Select(System.Object[])" /> method.
/// Is a strongly typed version of the <see cref="T:PX.Data.Required`1" /> that uses references to the predefined fields of the <see cref="T:PX.Data.BQL.Parameter" /> virtual BQL table.
/// </summary>
public static class P
{
  /// <summary>
  /// Indicates that the parameter has the <see cref="T:PX.Data.BQL.IBqlBool" /> type.
  /// </summary>
  public sealed class AsBool : BqlParameter<Required<Parameter.ofBool>, IBqlBool>
  {
  }

  /// <summary>
  /// Indicates that the parameter has the <see cref="T:PX.Data.BQL.IBqlByte" /> type.
  /// </summary>
  public sealed class AsByte : BqlParameter<Required<Parameter.ofByte>, IBqlByte>
  {
  }

  /// <summary>
  /// Indicates that the parameter has the <see cref="T:PX.Data.BQL.IBqlShort" /> type.
  /// </summary>
  public sealed class AsShort : BqlParameter<Required<Parameter.ofShort>, IBqlShort>
  {
  }

  /// <summary>
  /// Indicates that the parameter has the <see cref="T:PX.Data.BQL.IBqlInt" /> type.
  /// </summary>
  public sealed class AsInt : BqlParameter<Required<Parameter.ofInt>, IBqlInt>
  {
  }

  /// <summary>
  /// Indicates that the parameter has the <see cref="T:PX.Data.BQL.IBqlLong" /> type.
  /// </summary>
  public sealed class AsLong : BqlParameter<Required<Parameter.ofLong>, IBqlLong>
  {
  }

  /// <summary>
  /// Indicates that the parameter has the <see cref="T:PX.Data.BQL.IBqlFloat" /> type.
  /// </summary>
  public sealed class AsFloat : BqlParameter<Required<Parameter.ofFloat>, IBqlFloat>
  {
  }

  /// <summary>
  /// Indicates that the parameter has the <see cref="T:PX.Data.BQL.IBqlDouble" /> type.
  /// </summary>
  public sealed class AsDouble : BqlParameter<Required<Parameter.ofDouble>, IBqlDouble>
  {
  }

  /// <summary>
  /// Indicates that the parameter has the <see cref="T:PX.Data.BQL.IBqlDecimal" /> type.
  /// </summary>
  public sealed class AsDecimal : BqlParameter<Required<Parameter.ofDecimal>, IBqlDecimal>
  {
  }

  /// <summary>
  /// Indicates that the parameter has the <see cref="T:PX.Data.BQL.IBqlDateTime" /> type.
  /// </summary>
  /// <remarks>The class is a full equivalent of <see cref="T:PX.Data.BQL.P.AsDateTime.UTC" />. However, we recommend that you use the <see cref="T:PX.Data.BQL.P.AsDateTime.UTC" /> option explicitly to indicate that the date and time are in UTC.</remarks>
  public sealed class AsDateTime : BqlParameter<Required<Parameter.ofDateTimeUTC>, IBqlDateTime>
  {
    /// <summary>
    /// Indicates that the parameter has the <see cref="T:PX.Data.BQL.IBqlDateTime" /> type and is presented in UTC.
    /// </summary>
    public sealed class UTC : BqlParameter<Required<Parameter.ofDateTimeUTC>, IBqlDateTime>
    {
    }

    /// <summary>
    /// Indicates that the parameter has the <see cref="T:PX.Data.BQL.IBqlDateTime" /> type and is presented in local time zone.
    /// </summary>
    public sealed class WithTimeZone : BqlParameter<Required<Parameter.ofDateTime>, IBqlDateTime>
    {
    }
  }

  /// <summary>
  /// Indicates that the parameter has the <see cref="T:PX.Data.BQL.IBqlGuid" /> type.
  /// </summary>
  public sealed class AsGuid : BqlParameter<Required<Parameter.ofGuid>, IBqlGuid>
  {
  }

  /// <summary>
  /// Indicates that the parameter has the <see cref="T:PX.Data.BQL.IBqlString" /> type (NVARCHAR).
  /// </summary>
  public sealed class AsString : BqlParameter<Required<Parameter.ofString>, IBqlString>
  {
    /// <summary>
    /// Indicates that the parameter has the <see cref="T:PX.Data.BQL.IBqlString" /> type (VARCHAR).
    /// </summary>
    public sealed class ASCII : BqlParameter<Required<Parameter.ofAsciiString>, IBqlString>
    {
      /// <summary>
      /// Indicates that the parameter has the <see cref="T:PX.Data.BQL.IBqlString" /> type (CHAR).
      /// </summary>
      public sealed class Fixed : BqlParameter<Required<Parameter.ofFixedAsciiString>, IBqlString>
      {
      }
    }

    /// <summary>
    /// Indicates that the parameter has the <see cref="T:PX.Data.BQL.IBqlString" /> type (NCHAR).
    /// </summary>
    public sealed class Fixed : BqlParameter<Required<Parameter.ofFixedString>, IBqlString>
    {
      /// <summary>
      /// Indicates that the parameter has the <see cref="T:PX.Data.BQL.IBqlString" /> type (CHAR).
      /// </summary>
      public sealed class ASCII : BqlParameter<Required<Parameter.ofFixedAsciiString>, IBqlString>
      {
      }
    }
  }

  /// <summary>
  /// Indicates that the parameter has the <see cref="T:PX.Data.BQL.IBqlByteArray" /> type.
  /// </summary>
  public sealed class AsByteArray : BqlParameter<Required<Parameter.ofByteArray>, IBqlByteArray>
  {
  }
}
