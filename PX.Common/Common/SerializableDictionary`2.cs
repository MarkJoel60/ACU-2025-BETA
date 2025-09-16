// Decompiled with JetBrains decompiler
// Type: PX.Common.SerializableDictionary`2
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#nullable enable
namespace PX.Common;

[XmlRoot("dictionary")]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
{
  public XmlSchema? GetSchema() => (XmlSchema) null;

  public void ReadXml(XmlReader reader)
  {
    XmlSerializer xmlSerializer1 = new XmlSerializer(typeof (TKey));
    XmlSerializer xmlSerializer2 = new XmlSerializer(typeof (TValue));
    int num = reader.IsEmptyElement ? 1 : 0;
    reader.Read();
    if (num != 0)
      return;
    while (reader.NodeType != XmlNodeType.EndElement)
    {
      reader.ReadStartElement("item");
      reader.ReadStartElement("key");
      TKey key = (TKey) xmlSerializer1.Deserialize(reader);
      reader.ReadEndElement();
      reader.ReadStartElement("value");
      TValue obj = (TValue) xmlSerializer2.Deserialize(reader);
      reader.ReadEndElement();
      this.Add(key, obj);
      reader.ReadEndElement();
      int content = (int) reader.MoveToContent();
    }
    reader.ReadEndElement();
  }

  public void WriteXml(XmlWriter writer)
  {
    XmlSerializer xmlSerializer1 = new XmlSerializer(typeof (TKey));
    XmlSerializer xmlSerializer2 = new XmlSerializer(typeof (TValue));
    foreach (TKey key in this.Keys)
    {
      writer.WriteStartElement("item");
      writer.WriteStartElement("key");
      xmlSerializer1.Serialize(writer, (object) key);
      writer.WriteEndElement();
      writer.WriteStartElement("value");
      TValue o = this[key];
      xmlSerializer2.Serialize(writer, (object) o);
      writer.WriteEndElement();
      writer.WriteEndElement();
    }
  }
}
