// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDataRecordMap
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDataRecordMap : PXDataRecord
{
  protected PXDataRecord _innerRecord;

  public PXDataRecordMap(List<PXDataRecordMap.FieldEntry> map)
    : base((IDataReader) null, (IDbCommand) null, (PXDatabaseProvider) null)
  {
    this.FieldMap = map;
  }

  internal List<PXDataRecordMap.FieldEntry> FieldMap { get; }

  public void SetRow(PXDataRecord row)
  {
    this._innerRecord = row;
    this._isTableChanging = row._isTableChanging;
    this._postponedRowSelecting = row._postponedRowSelecting;
  }

  private bool Remap(ref int i, out PXDataRecordMap.FieldEntry field)
  {
    field = this.FieldMap[i];
    if (field.PositionInQuery < 0)
      return true;
    i = field.PositionInQuery;
    return false;
  }

  private bool Remap(ref int i) => this.Remap(ref i, out PXDataRecordMap.FieldEntry _);

  public override bool? GetBoolean(int i)
  {
    return this.Remap(ref i) ? new bool?() : this._innerRecord.GetBoolean(i);
  }

  public override byte? GetByte(int i)
  {
    return this.Remap(ref i) ? new byte?() : this._innerRecord.GetByte(i);
  }

  public override byte[] GetBytes(int i)
  {
    return this.Remap(ref i) ? (byte[]) null : this._innerRecord.GetBytes(i);
  }

  public override long GetBytes(
    int i,
    long fieldOffset,
    byte[] buffer,
    int bufferoffset,
    int length)
  {
    return this.Remap(ref i) ? 0L : this._innerRecord.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
  }

  public override byte[] GetTimeStamp(int i)
  {
    return this.Remap(ref i) ? (byte[]) null : this._innerRecord.GetTimeStamp(i);
  }

  public override char? GetChar(int i)
  {
    return this.Remap(ref i) ? new char?() : this._innerRecord.GetChar(i);
  }

  public override long GetChars(
    int i,
    long fieldoffset,
    char[] buffer,
    int bufferoffset,
    int length)
  {
    return this.Remap(ref i) ? 0L : this._innerRecord.GetChars(i, fieldoffset, buffer, bufferoffset, length);
  }

  public override string GetDataTypeName(int i)
  {
    return this.Remap(ref i) ? (string) null : this._innerRecord.GetDataTypeName(i);
  }

  public override System.DateTime? GetDateTime(int i)
  {
    return this.Remap(ref i) ? new System.DateTime?() : this._innerRecord.GetDateTime(i);
  }

  public override Decimal? GetDecimal(int i)
  {
    return this.Remap(ref i) ? new Decimal?() : this._innerRecord.GetDecimal(i);
  }

  public override double? GetDouble(int i)
  {
    return this.Remap(ref i) ? new double?() : this._innerRecord.GetDouble(i);
  }

  public override System.Type GetFieldType(int i)
  {
    return this.Remap(ref i) ? (System.Type) null : this._innerRecord.GetFieldType(i);
  }

  public override float? GetFloat(int i)
  {
    return this.Remap(ref i) ? new float?() : this._innerRecord.GetFloat(i);
  }

  public override Guid? GetGuid(int i)
  {
    return this.Remap(ref i) ? new Guid?() : this._innerRecord.GetGuid(i);
  }

  public override short? GetInt16(int i)
  {
    return this.Remap(ref i) ? new short?() : this._innerRecord.GetInt16(i);
  }

  public override int? GetInt32(int i)
  {
    return this.Remap(ref i) ? new int?() : this._innerRecord.GetInt32(i);
  }

  public override long? GetInt64(int i)
  {
    return this.Remap(ref i) ? new long?() : this._innerRecord.GetInt64(i);
  }

  public override string GetString(int i)
  {
    int i1 = i;
    return this.Remap(ref i1) ? (string) null : this._innerRecord.GetString(i1);
  }

  public override object GetValue(int i)
  {
    return this.Remap(ref i) ? (object) null : this._innerRecord.GetValue(i);
  }

  public override XContainer GetXmlContainer(int i)
  {
    return this.Remap(ref i) ? (XContainer) null : this._innerRecord.GetXmlContainer(i);
  }

  public override bool IsDBNull(int i) => this.Remap(ref i) || this._innerRecord.IsDBNull(i);

  public override string GetName(int i)
  {
    PXDataRecordMap.FieldEntry field;
    return this.Remap(ref i, out field) ? (string) null : field.FieldName;
  }

  public override int FieldCount => this.FieldMap.Count;

  /// <exclude />
  public class FieldEntry
  {
    public int PositionInResult;
    public int PositionInQuery;
    public string FieldName;
    public System.Type TableType;

    public FieldEntry(string field, System.Type table, int positionInResult)
    {
      this.FieldName = field;
      this.TableType = table;
      this.PositionInResult = positionInResult;
      this.PositionInQuery = -1;
    }

    public override bool Equals(object obj)
    {
      PXDataRecordMap.FieldEntry fieldEntry = (PXDataRecordMap.FieldEntry) obj;
      return this.FieldName == fieldEntry.FieldName && this.TableType == fieldEntry.TableType;
    }

    public override int GetHashCode()
    {
      return this.FieldName.GetHashCode() ^ this.TableType.GetHashCode();
    }

    public override string ToString() => $"{this.TableType.Name}/{this.FieldName}";
  }
}
