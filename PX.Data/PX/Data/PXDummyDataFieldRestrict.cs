// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDummyDataFieldRestrict
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data;

/// <summary>
/// Special class to pass old values to pxdatabase.delete for push notifications. This restricts is not passed to database.
/// </summary>
internal class PXDummyDataFieldRestrict(
  Column column,
  PXDbType valueType,
  int? valueLength,
  object value) : PXDataFieldRestrict(column, valueType, valueLength, value)
{
  public PXDummyDataFieldRestrict(
    string fieldName,
    PXDbType valueType,
    int? valueLength,
    object value)
    : this(new Column(fieldName), valueType, valueLength, value)
  {
  }
}
