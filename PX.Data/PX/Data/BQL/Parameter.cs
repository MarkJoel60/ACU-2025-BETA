// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Parameter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable enable
namespace PX.Data.BQL;

/// <exclude />
public sealed class Parameter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <exclude />
  [PXBool]
  public bool? OfBool { get; set; }

  /// <exclude />
  [PXByte]
  public byte? OfByte { get; set; }

  /// <exclude />
  [PXShort]
  public short? OfShort { get; set; }

  /// <exclude />
  [PXInt]
  public int? OfInt { get; set; }

  /// <exclude />
  [PXLong]
  public long? OfLong { get; set; }

  /// <exclude />
  [PXFloat]
  public float? OfFloat { get; set; }

  /// <exclude />
  [PXDouble]
  public double? OfDouble { get; set; }

  /// <exclude />
  [PXDecimal]
  public Decimal? OfDecimal { get; set; }

  /// <exclude />
  [PXDate(UseTimeZone = true)]
  public System.DateTime? OfDateTime { get; set; }

  /// <exclude />
  [PXDate(UseTimeZone = false)]
  public System.DateTime? OfDateTimeUTC { get; set; }

  /// <exclude />
  [PXGuid]
  public Guid? OfGuid { get; set; }

  /// <exclude />
  [PXString(IsUnicode = true, IsFixed = false)]
  public 
  #nullable disable
  string OfString { get; set; }

  /// <exclude />
  [PXString(IsUnicode = false, IsFixed = false)]
  public string OfAsciiString { get; set; }

  /// <exclude />
  [PXString(IsUnicode = true, IsFixed = true)]
  public string OfFixedString { get; set; }

  /// <exclude />
  [PXString(IsUnicode = false, IsFixed = true)]
  public string OfFixedAsciiString { get; set; }

  /// <exclude />
  [PXDBBinary]
  public byte[] OfByteArray { get; set; }

  public abstract class ofBool : BqlType<
  #nullable enable
  IBqlByte, byte>.Field<
  #nullable disable
  Parameter.ofBool>
  {
  }

  public abstract class ofByte : BqlType<
  #nullable enable
  IBqlByte, byte>.Field<
  #nullable disable
  Parameter.ofByte>
  {
  }

  public abstract class ofShort : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Parameter.ofShort>
  {
  }

  public abstract class ofInt : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Parameter.ofInt>
  {
  }

  public abstract class ofLong : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  Parameter.ofLong>
  {
  }

  public abstract class ofFloat : BqlType<
  #nullable enable
  IBqlFloat, float>.Field<
  #nullable disable
  Parameter.ofFloat>
  {
  }

  public abstract class ofDouble : BqlType<
  #nullable enable
  IBqlDouble, double>.Field<
  #nullable disable
  Parameter.ofDouble>
  {
  }

  public abstract class ofDecimal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Parameter.ofDecimal>
  {
  }

  public abstract class ofDateTime : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  Parameter.ofDateTime>
  {
  }

  public abstract class ofDateTimeUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    Parameter.ofDateTimeUTC>
  {
  }

  public abstract class ofGuid : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Parameter.ofGuid>
  {
  }

  public abstract class ofString : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Parameter.ofString>
  {
  }

  public abstract class ofAsciiString : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Parameter.ofAsciiString>
  {
  }

  public abstract class ofFixedString : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Parameter.ofFixedString>
  {
  }

  public abstract class ofFixedAsciiString : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Parameter.ofFixedAsciiString>
  {
  }

  public abstract class ofByteArray : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Parameter.ofByteArray>
  {
  }
}
