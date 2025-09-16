// Decompiled with JetBrains decompiler
// Type: PX.Api.WsdlTypeBuilder
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#nullable disable
namespace PX.Api;

internal class WsdlTypeBuilder
{
  private readonly List<WsdlTypeBuilder.Field> _fields = new List<WsdlTypeBuilder.Field>();
  public string BaseName;
  public string Documentation;
  public string Name;
  public bool IsExisting;

  public WsdlTypeBuilder.Field AddField(string n, Type t)
  {
    WsdlTypeBuilder.Field field = new WsdlTypeBuilder.Field()
    {
      Name = n,
      Type = t
    };
    this._fields.Add(field);
    return field;
  }

  private static XmlQualifiedName GetName(string s)
  {
    return new XmlQualifiedName(s, "http://acumatica.com");
  }

  public void Save(XmlSchema s)
  {
    if (this.IsExisting)
    {
      this.SaveExisting(s);
    }
    else
    {
      XmlSchemaComplexType schemaComplexType1 = new XmlSchemaComplexType();
      schemaComplexType1.Name = this.Name;
      XmlSchemaComplexType schemaComplexType2 = schemaComplexType1;
      if (!string.IsNullOrEmpty(this.Documentation))
        schemaComplexType2.Annotation = WsdlTypeBuilder.CreateDoc(this.Documentation);
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      if (string.IsNullOrEmpty(this.BaseName))
      {
        schemaComplexType2.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      }
      else
      {
        XmlSchemaComplexContent schemaComplexContent = new XmlSchemaComplexContent()
        {
          IsMixed = false
        };
        schemaComplexType2.ContentModel = (XmlSchemaContentModel) schemaComplexContent;
        XmlSchemaComplexContentExtension contentExtension = new XmlSchemaComplexContentExtension()
        {
          BaseTypeName = WsdlTypeBuilder.GetName(this.BaseName)
        };
        schemaComplexContent.Content = (XmlSchemaContent) contentExtension;
        contentExtension.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      }
      foreach (WsdlTypeBuilder.Field field in this._fields)
        xmlSchemaSequence.Items.Add((XmlSchemaObject) field.GetElement());
      s.Items.Add((XmlSchemaObject) schemaComplexType2);
    }
  }

  private void SaveExisting(XmlSchema schema)
  {
    XmlSchemaComplexType schemaComplexType = schema.Items.Select<XmlSchemaComplexType>().First<XmlSchemaComplexType>((Func<XmlSchemaComplexType, bool>) (t => t.Name == this.Name));
    XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
    if (string.IsNullOrEmpty(this.BaseName))
    {
      schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
    }
    else
    {
      XmlSchemaComplexContent schemaComplexContent = new XmlSchemaComplexContent()
      {
        IsMixed = false
      };
      schemaComplexType.ContentModel = (XmlSchemaContentModel) schemaComplexContent;
      XmlSchemaComplexContentExtension contentExtension = new XmlSchemaComplexContentExtension()
      {
        BaseTypeName = WsdlTypeBuilder.GetName(this.BaseName)
      };
      schemaComplexContent.Content = (XmlSchemaContent) contentExtension;
      contentExtension.Particle = (XmlSchemaParticle) xmlSchemaSequence;
    }
    foreach (WsdlTypeBuilder.Field field in this._fields)
      xmlSchemaSequence.Items.Add((XmlSchemaObject) field.GetElement());
  }

  public static XmlQualifiedName GetPrimitiveTypeName(Type type)
  {
    string ns = "http://www.w3.org/2001/XMLSchema";
    string name;
    switch (Type.GetTypeCode(type))
    {
      case TypeCode.Boolean:
        name = "boolean";
        break;
      case TypeCode.Char:
        name = "char";
        ns = "http://microsoft.com/wsdl/types/";
        break;
      case TypeCode.SByte:
        name = "byte";
        break;
      case TypeCode.Byte:
        name = "unsignedByte";
        break;
      case TypeCode.Int16:
        name = "short";
        break;
      case TypeCode.UInt16:
        name = "unsignedShort";
        break;
      case TypeCode.Int32:
        name = "int";
        break;
      case TypeCode.UInt32:
        name = "unsignedInt";
        break;
      case TypeCode.Int64:
        name = "long";
        break;
      case TypeCode.UInt64:
        name = "unsignedLong";
        break;
      case TypeCode.Single:
        name = "float";
        break;
      case TypeCode.Double:
        name = "double";
        break;
      case TypeCode.Decimal:
        name = "decimal";
        break;
      case TypeCode.DateTime:
        name = "dateTime";
        break;
      case TypeCode.String:
        name = "string";
        break;
      default:
        if (type == typeof (Guid))
        {
          name = "guid";
          ns = "http://microsoft.com/wsdl/types/";
          break;
        }
        if (type == typeof (byte[]))
        {
          name = "base64Binary";
          break;
        }
        XmlTypeMapping xmlTypeMapping = new XmlReflectionImporter("http://acumatica.com").ImportTypeMapping(type);
        return new XmlQualifiedName(xmlTypeMapping.XsdTypeName, xmlTypeMapping.XsdTypeNamespace);
    }
    return new XmlQualifiedName(name, ns);
  }

  private static XmlSchemaAnnotation CreateDoc(string text)
  {
    return new XmlSchemaAnnotation()
    {
      Items = {
        (XmlSchemaObject) new XmlSchemaDocumentation()
        {
          Markup = WsdlTypeBuilder.TextToNodeArray(text)
        }
      }
    };
  }

  private static XmlNode[] TextToNodeArray(string text)
  {
    XmlDocument xmlDocument = new XmlDocument()
    {
      XmlResolver = (XmlResolver) null
    };
    xmlDocument.XmlResolver = (XmlResolver) null;
    return new XmlNode[1]
    {
      (XmlNode) xmlDocument.CreateTextNode(text)
    };
  }

  public class Field
  {
    public string DefaultValue;
    public string Documentation;
    public bool IsNillable;
    public bool IsValueType;
    public string Name;
    public Type Type;
    public XmlQualifiedName SchemaTypeName;

    public XmlSchemaElement GetElement()
    {
      XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
      xmlSchemaElement.Name = this.Name;
      XmlQualifiedName xmlQualifiedName = this.SchemaTypeName;
      if ((object) xmlQualifiedName == null)
        xmlQualifiedName = WsdlTypeBuilder.GetPrimitiveTypeName(this.Type);
      xmlSchemaElement.SchemaTypeName = xmlQualifiedName;
      XmlSchemaElement element = xmlSchemaElement;
      if (this.IsNillable)
      {
        element.IsNillable = true;
      }
      else
      {
        element.MaxOccurs = 1M;
        element.MinOccurs = (Decimal) (this.IsValueType ? 1 : 0);
      }
      element.DefaultValue = this.DefaultValue;
      if (!string.IsNullOrEmpty(this.Documentation))
        element.Annotation = WsdlTypeBuilder.CreateDoc(this.Documentation);
      return element;
    }
  }
}
