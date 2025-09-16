// Decompiled with JetBrains decompiler
// Type: PX.Data.GIDataRecordMap
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Description.GI;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public class GIDataRecordMap : PXDataRecordMap
{
  internal PXDataRecord InnerRecord => this._innerRecord;

  internal RestrictedFieldsSet RestrictedFields { get; }

  public GIDataRecordMap(List<PXDataRecordMap.FieldEntry> map, RestrictedFieldsSet restrictedFields)
    : base(map)
  {
    this.RestrictedFields = restrictedFields;
  }

  public override bool IsFieldRestricted(PXCache cache, string field)
  {
    return this.RestrictedFields == null || this.RestrictedFields.Contains(new RestrictedField(cache.GetItemType(), field)) || this.RestrictedFields.Contains(new RestrictedField(field));
  }

  internal class GIFieldEntry : PXDataRecordMap.FieldEntry
  {
    public GIFieldEntry(
      string field,
      PXTable table,
      int positionInResult,
      int positionInQuery,
      PXGraph owner)
      : base(field, table?.BqlTable, positionInResult)
    {
      this.Owner = owner;
      this.Table = table;
      this.PositionInQuery = positionInQuery;
    }

    public PXGraph Owner { get; }

    public PXTable Table { get; }
  }
}
