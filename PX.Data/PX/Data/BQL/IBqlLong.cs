// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.IBqlLong
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable enable
namespace PX.Data.BQL;

public interface IBqlLong : 
  IBqlDataType,
  IBqlInteger,
  IBqlNumeric,
  IBqlComparable,
  IBqlEquitable,
  IBqlCorrespondsTo<long?>,
  IBqlCastableTo<IBqlByte>,
  IBqlCastableTo<IBqlShort>,
  IBqlCastableTo<IBqlInt>,
  IBqlCastableTo<IBqlLong>,
  IBqlCastableTo<IBqlDecimal>,
  IBqlCastableTo<IBqlFloat>,
  IBqlCastableTo<IBqlDouble>,
  IBqlCastableTo<IBqlNull>,
  IBqlCastableTo<BqlPlaceholder.IBqlAny>
{
}
