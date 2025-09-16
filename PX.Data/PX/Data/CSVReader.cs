// Decompiled with JetBrains decompiler
// Type: PX.Data.CSVReader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

#nullable disable
namespace PX.Data;

/// <summary>Reads CSV format</summary>
public class CSVReader : IContentReader, IDisposable
{
  private const string _SEPARATOR = ",";
  private readonly string _content;
  private readonly Dictionary<int, string> _header;
  private readonly Dictionary<int, string> _current;
  private StringReader _reader;
  private CsvReader _helper;
  private string _separator;

  public CSVReader(byte[] content, int codePage)
  {
    this._content = Encoding.GetEncoding(codePage).GetString(content);
    this._header = new Dictionary<int, string>();
    this._current = new Dictionary<int, string>();
    this.Separator = ",";
  }

  public string Separator
  {
    get => this._separator;
    set => this._separator = CSVReader.ReplaceTabChars(string.IsNullOrEmpty(value) ? "," : value);
  }

  public bool MoveNext()
  {
    string[] strArray = this.ReadNextLine();
    if (strArray == null)
      return false;
    foreach (int key in this._header.Keys)
      this._current[key] = key >= strArray.Length ? string.Empty : strArray[key];
    return true;
  }

  public string GetValue(int index)
  {
    return !this._current.ContainsKey(index) ? (string) null : this._current[index];
  }

  public void Reset()
  {
    this.DisposeReader();
    this._reader = new StringReader(this._content);
    this._header.Clear();
    this._helper = new CsvReader((TextReader) this._reader, (IReaderConfiguration) new CsvConfiguration(CultureInfo.InvariantCulture, (System.Type) null)
    {
      BadDataFound = (BadDataFound) null,
      Delimiter = this._separator
    }, false);
    this._helper.Read();
    this._helper.ReadHeader();
    int num = 0;
    foreach (string str in ((IReaderRow) this._helper.Context.Reader).HeaderRecord)
      this._header.Add(num++, str);
  }

  public void Reset(IEnumerable<string> header)
  {
    this.DisposeReader();
    this._reader = new StringReader(this._content);
    this._helper = new CsvReader((TextReader) this._reader, (IReaderConfiguration) new CsvConfiguration(CultureInfo.InvariantCulture, (System.Type) null)
    {
      BadDataFound = (BadDataFound) null,
      Delimiter = this._separator
    }, false);
    this._header.Clear();
    int num = 0;
    foreach (string str in header)
      this._header.Add(num++, str);
  }

  public IDictionary<int, string> IndexKeyPairs => (IDictionary<int, string>) this._header;

  public void Dispose() => this.DisposeReader();

  private void DisposeReader()
  {
    if (this._helper != null)
      this._helper.Dispose();
    if (this._reader == null)
      return;
    this._reader.Dispose();
  }

  private string[] ReadNextLine()
  {
    if (this._reader == null)
      return (string[]) null;
    if (this._helper == null)
      return (string[]) null;
    return this._helper.Read() ? this._helper.Context.Parser.Record : (string[]) null;
  }

  private static string ReplaceTabChars(string str)
  {
    StringBuilder stringBuilder = new StringBuilder();
    int num = str.IndexOf("\\t", 0);
    if (num > -1)
    {
      int startIndex = 0;
      do
      {
        stringBuilder.Append(str.Substring(startIndex, num - startIndex));
        if (num == startIndex || str[num - 1] != '\\')
          stringBuilder.Append("\t");
        else
          stringBuilder.Append("\\t");
        startIndex = num + 2;
      }
      while ((num = str.IndexOf("\\t", startIndex)) > -1);
      stringBuilder.Append(str.Substring(startIndex));
    }
    else
      stringBuilder.Append(str);
    return stringBuilder.ToString();
  }
}
