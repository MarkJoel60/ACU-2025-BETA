// Decompiled with JetBrains decompiler
// Type: PX.Data.PXXmlDataRecord
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

#nullable disable
namespace PX.Data;

internal class PXXmlDataRecord : PXDataRecord
{
  private readonly XElement[] _fields;
  private static readonly XNamespace _ns = (XNamespace) "http://www.w3.org/2001/XMLSchema-instance";

  public PXXmlDataRecord(XElement row) => this._fields = row.Elements().ToArray<XElement>();

  protected static bool IsNull(XElement field)
  {
    return field.Attribute(PXXmlDataRecord._ns + "nil")?.Value == "true";
  }

  public override int FieldCount => this._fields.Length;

  public override bool? GetBoolean(int i)
  {
    XElement field = this._fields[i];
    return PXXmlDataRecord.IsNull(field) ? new bool?() : new bool?(Convert.ToByte(field.Value) > (byte) 0);
  }

  public override byte? GetByte(int i)
  {
    XElement field = this._fields[i];
    return PXXmlDataRecord.IsNull(field) ? new byte?() : new byte?(Convert.ToByte(field.Value));
  }

  public override long GetBytes(
    int i,
    long fieldOffset,
    byte[] buffer,
    int bufferoffset,
    int length)
  {
    if (PXXmlDataRecord.IsNull(this._fields[i]))
      return 0;
    throw new NotImplementedException();
  }

  public override byte[] GetTimeStamp(int i)
  {
    XElement field = this._fields[i];
    return PXXmlDataRecord.IsNull(field) ? (byte[]) null : Convert.FromBase64String(field.Value);
  }

  public override byte[] GetBytes(int i)
  {
    XElement field = this._fields[i];
    if (PXXmlDataRecord.IsNull(field))
      return (byte[]) null;
    return string.IsNullOrEmpty(field.Value) ? new byte[0] : Convert.FromBase64String(field.Value);
  }

  public override char? GetChar(int i)
  {
    XElement field = this._fields[i];
    return PXXmlDataRecord.IsNull(field) ? new char?() : new char?(Convert.ToChar(field.Value));
  }

  public override long GetChars(
    int i,
    long fieldoffset,
    char[] buffer,
    int bufferoffset,
    int length)
  {
    if (PXXmlDataRecord.IsNull(this._fields[i]))
      return 0;
    throw new NotImplementedException();
  }

  public override string GetDataTypeName(int i) => throw new NotImplementedException();

  public override System.DateTime? GetDateTime(int i)
  {
    XElement field = this._fields[i];
    return PXXmlDataRecord.IsNull(field) ? new System.DateTime?() : new System.DateTime?(Convert.ToDateTime(field.Value));
  }

  public override Decimal? GetDecimal(int i)
  {
    XElement field = this._fields[i];
    return PXXmlDataRecord.IsNull(field) ? new Decimal?() : new Decimal?(Convert.ToDecimal(field.Value, (IFormatProvider) CultureInfo.InvariantCulture));
  }

  public override double? GetDouble(int i)
  {
    XElement field = this._fields[i];
    return PXXmlDataRecord.IsNull(field) ? new double?() : new double?(Convert.ToDouble(field.Value, (IFormatProvider) CultureInfo.InvariantCulture));
  }

  public override System.Type GetFieldType(int i) => throw new NotImplementedException();

  public override float? GetFloat(int i)
  {
    XElement field = this._fields[i];
    return PXXmlDataRecord.IsNull(field) ? new float?() : new float?(Convert.ToSingle(field.Value, (IFormatProvider) CultureInfo.InvariantCulture));
  }

  public override Guid? GetGuid(int i)
  {
    XElement field = this._fields[i];
    return PXXmlDataRecord.IsNull(field) ? new Guid?() : new Guid?(Guid.Parse(field.Value));
  }

  public override short? GetInt16(int i)
  {
    XElement field = this._fields[i];
    return PXXmlDataRecord.IsNull(field) ? new short?() : new short?(Convert.ToInt16(field.Value));
  }

  public override int? GetInt32(int i)
  {
    XElement field = this._fields[i];
    return PXXmlDataRecord.IsNull(field) ? new int?() : new int?(Convert.ToInt32(field.Value));
  }

  public override long? GetInt64(int i)
  {
    XElement field = this._fields[i];
    return PXXmlDataRecord.IsNull(field) ? new long?() : new long?(Convert.ToInt64(field.Value));
  }

  public override string GetName(int i) => this._fields[i].Name.LocalName;

  public override string GetString(int i)
  {
    XElement field = this._fields[i];
    return PXXmlDataRecord.IsNull(field) ? (string) null : field.Value;
  }

  public override object GetValue(int i)
  {
    XElement field = this._fields[i];
    return PXXmlDataRecord.IsNull(field) ? (object) null : (object) field.Value;
  }

  public override XContainer GetXmlContainer(int i)
  {
    XElement field = this._fields[i];
    if (PXXmlDataRecord.IsNull(field))
      return (XContainer) null;
    XNode firstNode = field.FirstNode;
    if ((firstNode != null ? (firstNode.NodeType == XmlNodeType.Text ? 1 : 0) : 0) == 0)
      return (XContainer) field;
    using (XmlReader reader = XmlReader.Create((TextReader) new StringReader($"<{field.Name}>{field.Value}</{field.Name}>")))
      return (XContainer) XDocument.Load(reader).Root;
  }

  public override bool IsDBNull(int i) => PXXmlDataRecord.IsNull(this._fields[i]);

  public virtual XElement GetXElement(int i) => this._fields[i];
}
