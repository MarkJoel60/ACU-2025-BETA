// Decompiled with JetBrains decompiler
// Type: PX.Api.Soap.Screen.TransformClassExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.IO;
using System.Linq;
using System.Web.Services.Protocols;
using System.Xml;

#nullable disable
namespace PX.Api.Soap.Screen;

public class TransformClassExtension : TransformSoapMessageBase
{
  private bool needTransform;

  public override object GetInitializer(Type serviceType) => (object) serviceType;

  public override void Initialize(object initializer)
  {
    this.needTransform = initializer == (object) typeof (ScreenGeneric);
  }

  public override void ProcessMessage(SoapMessage message)
  {
    if (!this.needTransform)
      return;
    base.ProcessMessage(message);
  }

  public override Stream ChainStream(Stream stream)
  {
    return this.needTransform ? base.ChainStream(stream) : stream;
  }

  protected override void TransformInputMessage(XmlDocument document)
  {
    try
    {
      XmlNode firstChild1 = document.DocumentElement.FirstChild;
      if (!firstChild1.HasChildNodes)
        return;
      XmlNode firstChild2 = firstChild1.FirstChild;
      XmlNode xmlNode = (XmlNode) null;
      string screenId = PXContext.GetScreenID();
      if (!string.IsNullOrEmpty(screenId))
      {
        string str = screenId.Replace(".", "");
        if (firstChild2.LocalName.StartsWith(str) && firstChild2.LocalName.Length > str.Length)
          xmlNode = firstChild1.AppendChild((XmlNode) document.CreateElement(firstChild2.Prefix, firstChild2.LocalName.Substring(str.Length), firstChild2.NamespaceURI));
      }
      else if (firstChild2.LocalName.StartsWith("Untyped") && firstChild2.LocalName.Length > 7)
      {
        xmlNode = firstChild1.AppendChild((XmlNode) document.CreateElement(firstChild2.Prefix, firstChild2.LocalName.Substring(7), firstChild2.NamespaceURI));
        PXContext.SetSlot<bool>("FORCEUNTYPED", true);
      }
      if (xmlNode == null)
        return;
      foreach (XmlAttribute node in firstChild2.Attributes.Cast().ToArray<object>())
        xmlNode.Attributes.Append(node);
      foreach (XmlNode newChild in firstChild2.ChildNodes.Cast().ToArray<object>())
        xmlNode.AppendChild(newChild);
      firstChild2.ParentNode.RemoveChild(firstChild2);
    }
    catch
    {
    }
  }

  protected override void TransformResultMessage(XmlDocument document)
  {
    try
    {
      string str1 = (PXContext.GetScreenID() ?? "").Replace(".", "");
      if (string.IsNullOrEmpty(str1))
        return;
      XmlNode firstChild1 = document.DocumentElement.FirstChild;
      XmlNode firstChild2 = firstChild1.FirstChild;
      if (!(firstChild2.LocalName != "Fault"))
        return;
      if (PXContext.GetSlot<bool>("FORCEUNTYPED"))
      {
        XmlNode xmlNode = firstChild1.AppendChild((XmlNode) document.CreateElement(firstChild2.Prefix, "Untyped" + firstChild2.LocalName, firstChild2.NamespaceURI));
        foreach (XmlAttribute node in firstChild2.Attributes.Cast().ToArray<object>())
          xmlNode.Attributes.Append(node);
        foreach (XmlNode newChild in firstChild2.ChildNodes.Cast().ToArray<object>())
          xmlNode.AppendChild(newChild);
        firstChild2.ParentNode.RemoveChild(firstChild2);
      }
      else if (PXContext.GetSlot<bool>("FORCEGENERIC"))
      {
        string str2 = "Content";
        if (firstChild2.HasChildNodes)
        {
          if (firstChild2.LocalName == "SubmitResponse")
            firstChild2 = firstChild2.FirstChild;
          else if (firstChild2.LocalName == "ImportResponse")
          {
            firstChild2 = firstChild2.FirstChild;
            str2 = "ImportResult";
          }
        }
        foreach (XmlNode childNode in firstChild2.ChildNodes)
        {
          XmlAttribute attribute = document.CreateAttribute("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance");
          attribute.Value = str1 + str2;
          childNode.Attributes.Append(attribute);
        }
      }
      else
      {
        XmlNode xmlNode = firstChild1.AppendChild((XmlNode) document.CreateElement(firstChild2.Prefix, str1 + firstChild2.LocalName, firstChild2.NamespaceURI));
        foreach (XmlAttribute node in firstChild2.Attributes.Cast().ToArray<object>())
          xmlNode.Attributes.Append(node);
        foreach (XmlNode newChild in firstChild2.ChildNodes.Cast().ToArray<object>())
          xmlNode.AppendChild(newChild);
        firstChild2.ParentNode.RemoveChild(firstChild2);
      }
    }
    catch
    {
    }
  }
}
