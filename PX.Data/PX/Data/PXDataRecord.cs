// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDataRecord
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Database;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Wraps a single record of a result set obtained by executing a BQL statement. A record includes data fields of all joined tables.</summary>
public class PXDataRecord : IDisposable
{
  protected internal List<System.Action> _postponedRowSelecting;
  protected IDataReader _Reader;
  protected IDbCommand _Command;
  protected PXDatabaseProvider _provider;
  protected StringTable _stringTable;
  internal Stopwatch _Timer;
  internal bool _isTableChanging;
  internal PXDataRecord.SqlTimerDisposable _sqlTimerDisposable;

  protected PXDataRecord()
  {
  }

  public PXDataRecord(int e)
  {
  }

  protected internal void AddRowSelecting(System.Action del)
  {
    if (this._postponedRowSelecting == null)
      this._postponedRowSelecting = new List<System.Action>();
    this._postponedRowSelecting.Add(del);
  }

  protected internal void SetRowSelecting(List<System.Action> list)
  {
    this._postponedRowSelecting = list;
  }

  protected void ExecuteRowSelecting()
  {
    if (this._postponedRowSelecting == null)
      return;
    for (int index = 0; index < this._postponedRowSelecting.Count; ++index)
      this._postponedRowSelecting[index]();
    this._postponedRowSelecting.Clear();
  }

  /// <summary>
  /// Initializes a new instance of the <tt>PXDataRecord</tt> class.
  /// </summary>
  /// <param name="reader">The reader object.</param>
  /// <param name="command">The database command object.</param>
  /// <param name="provider">The database provider object.</param>
  /// <param name="stringTable">Cache for string objects.</param>
  public PXDataRecord(
    IDataReader reader,
    IDbCommand command,
    PXDatabaseProvider provider,
    StringTable stringTable = null)
  {
    PXDataRecord pxDataRecord = this;
    this._Reader = reader;
    this._Command = command;
    this._provider = provider;
    this._stringTable = stringTable ?? (StringTable) new StringTableFixedSize();
    if (!PXPerformanceMonitor.IsEnabled || reader == null)
      return;
    PXPerformanceInfo sample = PXPerformanceMonitor.CurrentSample;
    if (sample == null)
      return;
    PXProfilerSqlSample sqlSample = PXPerformanceInfo.FindLastSample(sample.SqlSamples, reader);
    this._sqlTimerDisposable = new PXDataRecord.SqlTimerDisposable()
    {
      _sqlTimerStart = (System.Action) (() =>
      {
        sample.SqlTimer.Start();
        sqlSample?.SqlTimer.Start();
        pxDataRecord._Timer?.Start();
      }),
      _sqlTimerEnd = (System.Action) (() =>
      {
        sample.SqlTimer.Stop();
        sqlSample?.SqlTimer.Stop();
        pxDataRecord._Timer?.Stop();
      })
    };
  }

  void IDisposable.Dispose()
  {
    if (this._Command != null)
    {
      if (this._Reader != null)
      {
        this._Reader.Dispose();
        this._Reader = (IDataReader) null;
      }
      this._provider.LeaveConnection(this._Command.Connection);
      this._Command.Dispose();
      this._Command = (IDbCommand) null;
    }
    this.ExecuteRowSelecting();
  }

  public virtual bool IsFieldRestricted(PXCache cache, string field) => true;

  /// <summary>Gets the number of columns in the current data record. If the
  /// <tt>PXDataRecord</tt> instance is not positioned in a valid data
  /// record, the value is <tt>0</tt>. The default value is
  /// <tt>-1</tt>.</summary>
  public virtual int FieldCount => this._Reader.FieldCount;

  /// <summary>
  /// Returns the value of the specified field.
  /// Can cause performace issue because of <paramref name="name" /> finding.
  /// </summary>
  /// <param name="name">The name of the column to find.</param>
  /// <returns>Field value.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// No column with the specified name was found.
  /// </exception>
  public object this[string name] => this.GetValue(this.GetOrdinal(name));

  /// <summary>Gets the value of the specified column as a Boolean.</summary>
  /// <param name="i">The zero-based column ordinal.</param>
  /// <returns>The value of the column.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual bool? GetBoolean(int i)
  {
    using (this.SqlTimer)
      return this._Reader.IsDBNull(i) ? new bool?() : new bool?(this._Reader.GetBoolean(i));
  }

  /// <summary>
  /// Gets the 8-bit unsigned integer value of the specified column.
  /// </summary>
  /// <param name="i">The zero-based column ordinal.</param>
  /// <returns>The 8-bit unsigned integer value of the specified column.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual byte? GetByte(int i)
  {
    using (this.SqlTimer)
      return this._Reader.IsDBNull(i) ? new byte?() : new byte?(this._Reader.GetByte(i));
  }

  /// <summary>
  /// Reads a stream of bytes from the specified column offset into the buffer
  /// as an array, starting at the given buffer offset.
  /// </summary>
  /// <param name="i">The zero-based column ordinal.</param>
  /// <param name="fieldOffset">The index within the field from which to start the read operation.</param>
  /// <param name="buffer">The buffer into which to read the stream of bytes.</param>
  /// <param name="bufferoffset">The index for buffer to start the read operation.</param>
  /// <param name="length">The number of bytes to read.</param>
  /// <returns>The actual number of bytes read.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual long GetBytes(
    int i,
    long fieldOffset,
    byte[] buffer,
    int bufferoffset,
    int length)
  {
    using (this.SqlTimer)
      return this._Reader.IsDBNull(i) ? 0L : this._Reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
  }

  /// <summary>
  /// Reads 8 bytes from the specified column offset as an array of bytes.
  /// </summary>
  /// <param name="i">The zero-based column ordinal.</param>
  /// <returns>The array of 8 bytes.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual byte[] GetTimeStamp(int i)
  {
    byte[] buffer = new byte[8];
    this.GetBytes(i, 0L, buffer, 0, 8);
    return buffer;
  }

  public virtual byte[] GetBytes(int i)
  {
    using (this.SqlTimer)
    {
      if (this._Reader.IsDBNull(i))
        return (byte[]) null;
      byte[] buffer = new byte[this._Reader.GetBytes(i, 0L, (byte[]) null, 0, 0)];
      this._Reader.GetBytes(i, 0L, buffer, 0, buffer.Length);
      return buffer;
    }
  }

  /// <summary>Gets the character value of the specified column.</summary>
  /// <param name="i">The zero-based column ordinal.</param>
  /// <returns>The character value of the specified column.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual char? GetChar(int i)
  {
    using (this.SqlTimer)
      return this._Reader.IsDBNull(i) ? new char?() : new char?(this._Reader.GetChar(i));
  }

  /// <summary>
  /// Reads a stream of characters from the specified column offset into the buffer
  /// as an array, starting at the given buffer offset.
  /// </summary>
  /// <param name="i">The zero-based column ordinal.</param>
  /// <param name="fieldoffset">The index within the row from which to start the read operation.</param>
  /// <param name="buffer">The buffer into which to read the stream of bytes.</param>
  /// <param name="bufferoffset">The index for buffer to start the read operation.</param>
  /// <param name="length">The number of bytes to read.</param>
  /// <returns>The actual number of characters read.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual long GetChars(
    int i,
    long fieldoffset,
    char[] buffer,
    int bufferoffset,
    int length)
  {
    using (this.SqlTimer)
      return this._Reader.IsDBNull(i) ? 0L : this._Reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
  }

  /// <summary>
  /// Gets the data type information for the specified field.
  /// </summary>
  /// <param name="i">The index of the field to find.</param>
  /// <returns>The data type information for the specified field.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual string GetDataTypeName(int i) => this._Reader.GetDataTypeName(i);

  /// <summary>
  /// Gets the date and time data value of the specified field.
  /// </summary>
  /// <param name="i">The index of the field to find.</param>
  /// <returns>The date and time data value of the spcified field.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual System.DateTime? GetDateTime(int i)
  {
    using (this.SqlTimer)
      return this._Reader.IsDBNull(i) ? new System.DateTime?() : new System.DateTime?(this._Reader.GetDateTime(i));
  }

  /// <summary>
  /// Gets the fixed-position numeric value of the specified field.
  /// </summary>
  /// <param name="i">The index of the field to find.</param>
  /// <returns>The fixed-position numeric value of the specified field.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual Decimal? GetDecimal(int i)
  {
    using (this.SqlTimer)
      return this._Reader.IsDBNull(i) ? new Decimal?() : new Decimal?(this._Reader.GetDecimal(i));
  }

  /// <summary>
  /// Gets the double-precision floating point number of the specified field.
  /// </summary>
  /// <param name="i">The index of the field to find.</param>
  /// <returns>The double-precision floating point number of the specified field.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual double? GetDouble(int i)
  {
    using (this.SqlTimer)
      return this._Reader.IsDBNull(i) ? new double?() : new double?(this._Reader.GetDouble(i));
  }

  /// <summary>
  /// Gets the <tt>System.Type</tt> information corresponding to the type of <tt>System.Object</tt>
  /// that would be returned from <tt>System.Data.IDataRecord.GetValue(System.Int32)</tt>.
  /// </summary>
  /// <param name="i">The index of the field to find.</param>
  /// <returns>The <tt>System.Type</tt> information corresponding to the type of <tt>System.Object</tt>
  /// that would be returned from <tt>System.Data.IDataRecord.GetValue(System.Int32)</tt>.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual System.Type GetFieldType(int i) => this._Reader.GetFieldType(i);

  /// <summary>
  /// Gets the single-precision floating point number of the specified field.
  /// </summary>
  /// <param name="i">The index of the field to find.</param>
  /// <returns>The single-precision floating point number of the specified field.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual float? GetFloat(int i)
  {
    using (this.SqlTimer)
      return this._Reader.IsDBNull(i) ? new float?() : new float?(this._Reader.GetFloat(i));
  }

  /// <summary>Returns the GUID value of the specified field.</summary>
  /// <param name="i">The index of the field to find.</param>
  /// <returns>The GUID value of the specified field.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual Guid? GetGuid(int i)
  {
    using (this.SqlTimer)
      return this._Reader.IsDBNull(i) ? new Guid?() : new Guid?(this._Reader.GetGuid(i));
  }

  /// <summary>
  /// Gets the 16-bit signed integer value of the specified field.
  /// </summary>
  /// <param name="i">The index of the field to find.</param>
  /// <returns>The 16-bit signed integer value of the specified field.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual short? GetInt16(int i)
  {
    using (this.SqlTimer)
      return this._Reader.IsDBNull(i) ? new short?() : new short?(this._Reader.GetInt16(i));
  }

  /// <summary>
  /// Gets the 32-bit signed integer value of the specified field.
  /// </summary>
  /// <param name="i">The index of the field to find.</param>
  /// <returns>The 32-bit signed integer value of the specified field.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual int? GetInt32(int i)
  {
    using (this.SqlTimer)
      return this._Reader.IsDBNull(i) ? new int?() : new int?(this._Reader.GetInt32(i));
  }

  /// <summary>
  /// Gets the 64-bit signed integer value of the specified field.
  /// </summary>
  /// <param name="i">The index of the field to find.</param>
  /// <returns>The 64-bit signed integer value of the specified field.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual long? GetInt64(int i)
  {
    using (this.SqlTimer)
      return this._Reader.IsDBNull(i) ? new long?() : new long?(this._Reader.GetInt64(i));
  }

  /// <summary>Gets the name for the field to find.</summary>
  /// <param name="i">The index of the field to find.</param>
  /// <returns> The name of the field or the empty string (""),
  /// if there is no value to return.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual string GetName(int i) => this._Reader.GetName(i);

  /// <summary>Gets the string value of the specified field.</summary>
  /// <param name="i">The index of the field to find.</param>
  /// <returns>The string value of the specified field.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual string GetString(int i)
  {
    using (this.SqlTimer)
    {
      if (this._Reader.IsDBNull(i))
        return (string) null;
      string s = this._Reader.GetString(i);
      if (this._stringTable != null && s != null)
        s = this._stringTable.Add(s);
      return s;
    }
  }

  /// <summary>Returns the value of the specified field.</summary>
  /// <param name="i">The index of the field to find.</param>
  /// <returns>The System.Object which will contain the field value.</returns>
  /// <exception cref="T:System.IndexOutOfRangeException">
  /// The index passed was outside the range from 0 to
  /// <tt>System.Data.IDataRecord.FieldCount</tt>.
  /// </exception>
  public virtual object GetValue(int i)
  {
    using (this.SqlTimer)
    {
      if (this._Reader.IsDBNull(i))
        return (object) null;
      object obj = this._Reader.GetValue(i);
      if (!(obj is string))
        return obj;
      string s = (string) obj;
      if (this._stringTable != null)
        s = this._stringTable.Add(s);
      return (object) s;
    }
  }

  public virtual XContainer GetXmlContainer(int i)
  {
    using (this.SqlTimer)
    {
      if (this._Reader.IsDBNull(i))
        return (XContainer) null;
      string s = this._Reader.GetString(i);
      if (string.IsNullOrEmpty(s))
        return (XContainer) null;
      using (XmlReader reader = XmlReader.Create((TextReader) new PXXmlStringReader(s)))
        return (XContainer) XDocument.Load(reader);
    }
  }

  /// <summary>
  /// Returns the value that indicates whether the specified field
  /// is set to <tt>null</tt>.
  /// </summary>
  /// <param name="i">The index of the field to find.</param>
  /// <returns><tt>true</tt> if the specified field is set to <tt>null</tt>.
  /// Otherwise, <tt>false</tt>.</returns>
  public virtual bool IsDBNull(int i)
  {
    using (this.SqlTimer)
      return this._Reader.IsDBNull(i);
  }

  public int GetOrdinal(string fieldName) => this._Reader.GetOrdinal(fieldName);

  internal IDisposable SqlTimer
  {
    get
    {
      PXDataRecord.SqlTimerDisposable sqlTimerDisposable = this._sqlTimerDisposable;
      if (sqlTimerDisposable != null)
        sqlTimerDisposable._sqlTimerStart();
      return (IDisposable) this._sqlTimerDisposable;
    }
  }

  /// <exclude />
  internal class SqlTimerDisposable : IDisposable
  {
    public System.Action _sqlTimerStart;
    public System.Action _sqlTimerEnd;

    public void Dispose() => this._sqlTimerEnd();
  }
}
