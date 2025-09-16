// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDummyDataRecord
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Extensions;
using System;

#nullable disable
namespace PX.Data;

/// <exclude />
internal class PXDummyDataRecord : PXDataRecord
{
  protected readonly string[] _Reader;

  public PXDummyDataRecord(string[] values) => this._Reader = values;

  public override int FieldCount => throw new NotImplementedException();

  public override bool? GetBoolean(int i)
  {
    return this.IsDBNull(i) ? new bool?() : new bool?(Convert.ToBoolean((object) this.GetInt16(i)));
  }

  public override byte? GetByte(int i)
  {
    return this.IsDBNull(i) ? new byte?() : new byte?(Convert.ToByte(this._Reader[i]));
  }

  public override long GetBytes(
    int i,
    long fieldOffset,
    byte[] buffer,
    int bufferoffset,
    int length)
  {
    if (this.IsDBNull(i))
      return 0;
    char[] charArray = this._Reader[i].ToCharArray();
    byte[] numArray = Convert.FromBase64CharArray(charArray, 0, charArray.Length);
    if (buffer != null)
    {
      int num = 0;
      while ((long) num + fieldOffset < (long) numArray.Length && num + bufferoffset < buffer.Length && num < length)
      {
        buffer[num + bufferoffset] = numArray[(long) num + fieldOffset];
        ++i;
      }
    }
    return (long) numArray.Length - fieldOffset < 0L ? 0L : (long) numArray.Length - fieldOffset;
  }

  public override byte[] GetTimeStamp(int i)
  {
    byte[] buffer = new byte[8];
    this.GetBytes(i, 0L, buffer, 0, 8);
    return buffer;
  }

  public override byte[] GetBytes(int i)
  {
    if (this.IsDBNull(i))
      return (byte[]) null;
    byte[] buffer = new byte[this.GetBytes(i, 0L, (byte[]) null, 0, 0)];
    this.GetBytes(i, 0L, buffer, 0, buffer.Length);
    return buffer;
  }

  public override char? GetChar(int i)
  {
    return this.IsDBNull(i) ? new char?() : new char?(Convert.ToChar(this._Reader[i]));
  }

  public override long GetChars(
    int i,
    long fieldoffset,
    char[] buffer,
    int bufferoffset,
    int length)
  {
    if (this.IsDBNull(i))
      return 0;
    char[] charArray = this._Reader[i].ToCharArray();
    if (buffer != null)
    {
      int num = 0;
      while ((long) num + fieldoffset < (long) charArray.Length && num + bufferoffset < buffer.Length && num < length)
      {
        buffer[num + bufferoffset] = charArray[(long) num + fieldoffset];
        ++i;
      }
    }
    return (long) charArray.Length - fieldoffset < 0L ? 0L : (long) charArray.Length - fieldoffset;
  }

  public override string GetDataTypeName(int i) => throw new NotImplementedException();

  public override System.DateTime? GetDateTime(int i)
  {
    return this.IsDBNull(i) ? new System.DateTime?() : new System.DateTime?(Convert.ToDateTime(this._Reader[i]));
  }

  public override Decimal? GetDecimal(int i)
  {
    return this.IsDBNull(i) ? new Decimal?() : new Decimal?(Convert.ToDecimal(this._Reader[i]));
  }

  public override double? GetDouble(int i)
  {
    return this.IsDBNull(i) ? new double?() : new double?(Convert.ToDouble(this._Reader[i]));
  }

  public override System.Type GetFieldType(int i) => throw new NotImplementedException();

  public override float? GetFloat(int i)
  {
    return this.IsDBNull(i) ? new float?() : new float?(Convert.ToSingle(this._Reader[i]));
  }

  public override Guid? GetGuid(int i)
  {
    return this.IsDBNull(i) ? new Guid?() : new Guid?(new Guid(this._Reader[i]));
  }

  public override short? GetInt16(int i)
  {
    return this.IsDBNull(i) ? new short?() : new short?(Convert.ToInt16(StringExtensions.FirstSegment(this._Reader[i], '.')));
  }

  public override int? GetInt32(int i)
  {
    return this.IsDBNull(i) ? new int?() : new int?(Convert.ToInt32(StringExtensions.FirstSegment(this._Reader[i], '.')));
  }

  public override long? GetInt64(int i)
  {
    return this.IsDBNull(i) ? new long?() : new long?(Convert.ToInt64(StringExtensions.FirstSegment(this._Reader[i], '.')));
  }

  public override string GetName(int i) => throw new NotImplementedException();

  public override string GetString(int i) => this.IsDBNull(i) ? (string) null : this._Reader[i];

  public override object GetValue(int i)
  {
    return this.IsDBNull(i) ? (object) null : (object) this._Reader[i];
  }

  public override bool IsDBNull(int i)
  {
    return i > this._Reader.Length || string.IsNullOrWhiteSpace(this._Reader[i]) || this._Reader[i][0] == char.MinValue;
  }
}
