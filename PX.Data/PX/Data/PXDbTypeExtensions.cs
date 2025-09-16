// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDbTypeExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public static class PXDbTypeExtensions
{
  public static bool IsString(this PXDbType dbType)
  {
    switch (dbType)
    {
      case PXDbType.Char:
      case PXDbType.NChar:
      case PXDbType.NText:
      case PXDbType.NVarChar:
      case PXDbType.Text:
      case PXDbType.VarChar:
        return true;
      default:
        return false;
    }
  }

  internal static bool IsUnicodeString(this PXDbType dbType)
  {
    switch (dbType)
    {
      case PXDbType.NChar:
      case PXDbType.NText:
      case PXDbType.NVarChar:
        return true;
      default:
        return false;
    }
  }

  internal static bool IsAsciiString(this PXDbType dbType)
  {
    return dbType == PXDbType.Char || dbType == PXDbType.Text || dbType == PXDbType.VarChar;
  }

  internal static bool IsChar(this PXDbType dbType)
  {
    return dbType == PXDbType.Char || dbType == PXDbType.NChar;
  }

  internal static bool IsDateTime(this PXDbType dbType)
  {
    return dbType == PXDbType.DateTime || dbType == PXDbType.SmallDateTime;
  }

  internal static bool IsInt(this PXDbType dbType)
  {
    if (dbType <= PXDbType.Bit)
    {
      if (dbType != PXDbType.BigInt && dbType != PXDbType.Bit)
        goto label_4;
    }
    else if (dbType != PXDbType.Int && dbType != PXDbType.SmallInt)
      goto label_4;
    return true;
label_4:
    return false;
  }
}
