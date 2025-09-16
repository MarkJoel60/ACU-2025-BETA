// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDbTypeConverter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.DbServices.Points.DbmsBase;
using System;
using System.Data;

#nullable disable
namespace PX.Data;

public static class PXDbTypeConverter
{
  internal static PXDbType TypeCodeToPXDbType(this TypeCode typeCode)
  {
    return PXDbTypeConverter.SqlDbTypeToPXDbType(DbmsTableAdapter.getDbTypeFromTypeCode(typeCode));
  }

  public static PXDbType SqlDbTypeToPXDbType(SqlDbType sqlType)
  {
    switch (sqlType)
    {
      case SqlDbType.BigInt:
        return PXDbType.BigInt;
      case SqlDbType.Binary:
        return PXDbType.Binary;
      case SqlDbType.Bit:
        return PXDbType.Bit;
      case SqlDbType.Char:
        return PXDbType.Char;
      case SqlDbType.DateTime:
        return PXDbType.DateTime;
      case SqlDbType.Decimal:
        return PXDbType.Decimal;
      case SqlDbType.Float:
        return PXDbType.Float;
      case SqlDbType.Image:
        return PXDbType.Image;
      case SqlDbType.Int:
        return PXDbType.Int;
      case SqlDbType.Money:
        return PXDbType.Money;
      case SqlDbType.NChar:
        return PXDbType.NChar;
      case SqlDbType.NText:
        return PXDbType.NText;
      case SqlDbType.NVarChar:
        return PXDbType.NVarChar;
      case SqlDbType.Real:
        return PXDbType.Real;
      case SqlDbType.UniqueIdentifier:
        return PXDbType.UniqueIdentifier;
      case SqlDbType.SmallDateTime:
      case SqlDbType.DateTime2:
        return PXDbType.SmallDateTime;
      case SqlDbType.SmallInt:
        return PXDbType.SmallInt;
      case SqlDbType.SmallMoney:
        return PXDbType.SmallMoney;
      case SqlDbType.Text:
        return PXDbType.Text;
      case SqlDbType.Timestamp:
        return PXDbType.Timestamp;
      case SqlDbType.TinyInt:
        return PXDbType.TinyInt;
      case SqlDbType.VarBinary:
        return PXDbType.VarBinary;
      case SqlDbType.VarChar:
        return PXDbType.VarChar;
      case SqlDbType.Variant:
        return PXDbType.Variant;
      case SqlDbType.Xml:
        return PXDbType.Xml;
      case SqlDbType.Udt:
        return PXDbType.Udt;
      default:
        throw new PXException("Unknown SqlDbType");
    }
  }
}
