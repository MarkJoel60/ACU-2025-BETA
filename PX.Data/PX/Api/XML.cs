// Decompiled with JetBrains decompiler
// Type: PX.Api.XML
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.IO;
using System.Text;
using System.Xml;

#nullable enable
namespace PX.Api;

internal static class XML
{
  public static XmlElement AppendChild(this XmlElement parent, string name)
  {
    XmlElement element = parent.OwnerDocument.CreateElement(parent.Prefix, name, parent.NamespaceURI);
    parent.AppendChild((XmlNode) element);
    return element;
  }

  public static XmlElement AppendChild(this XmlDocument doc, string name)
  {
    XmlElement element = doc.CreateElement(name);
    doc.AppendChild((XmlNode) element);
    return element;
  }

  public static XmlElement AppendChild(this XmlNode parent, string name)
  {
    return parent is XmlDocument ? XML.AppendChild((XmlDocument) parent, name) : XML.AppendChild((XmlElement) parent, name);
  }

  public static XmlText AppendText(this XmlElement parent, string text)
  {
    XmlText textNode = parent.OwnerDocument.CreateTextNode(text);
    parent.AppendChild((XmlNode) textNode);
    return textNode;
  }

  public static XmlCDataSection AppendCData(this XmlNode parent, string text)
  {
    XmlCDataSection cdataSection = parent.OwnerDocument.CreateCDataSection(text);
    parent.AppendChild((XmlNode) cdataSection);
    return cdataSection;
  }

  public static void SetAttributeWithDefault(
    this XmlNode element,
    string name,
    object v,
    object def)
  {
    XmlElement xmlElement = (XmlElement) element;
    string str1 = Convert.ToString(v);
    string str2 = Convert.ToString(def);
    int num = str1 == str2 ? 1 : 0;
    if (num != 0 && xmlElement.HasAttribute(name))
      xmlElement.RemoveAttribute(name);
    if (num != 0)
      return;
    xmlElement.SetAttribute(name, str1);
  }

  public static T GetAttribute<T>(XmlNode n, string name, T defVal)
  {
    string attribute = ((XmlElement) n).GetAttribute(name);
    if (string.IsNullOrEmpty(attribute))
      return defVal;
    return typeof (T) == typeof (Guid) ? (T) (ValueType) new Guid(attribute) : (T) Convert.ChangeType((object) attribute, typeof (T));
  }

  public static XmlElement SelectOrCreateChild(this XmlElement parent, string name)
  {
    return parent.SelectChildElement(name) ?? XML.AppendChild(parent, name);
  }

  public static XmlElement SelectChildElement(this XmlElement parent, string name)
  {
    return (XmlElement) parent.SelectSingleNode(name);
  }

  public static string OuterXmlFormatted(this XmlDocument doc)
  {
    StringBuilder sb = new StringBuilder();
    using (XmlTextWriter w = new XmlTextWriter((TextWriter) new StringWriter(sb)))
    {
      w.Formatting = Formatting.Indented;
      w.Indentation = 4;
      doc.Save((XmlWriter) w);
    }
    return sb.ToString();
  }
}
