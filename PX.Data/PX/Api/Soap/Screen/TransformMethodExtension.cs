// Decompiled with JetBrains decompiler
// Type: PX.Api.Soap.Screen.TransformMethodExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

#nullable enable
namespace PX.Api.Soap.Screen;

internal class TransformMethodExtension : TransformSoapMessageBase
{
  protected override void TransformResultMessage(
  #nullable disable
  XmlDocument document)
  {
    if (PXContext.GetSlot<bool>("FORCEUNTYPED"))
      return;
    try
    {
      XmlElement[] array = document.DocumentElement.EnumDescendantsOrSelf<XmlElement>((Func<XmlElement, IEnumerable<XmlElement>>) (n => n.ChildNodes.Select<XmlElement>())).Where<XmlElement>((Func<XmlElement, bool>) (e => e.LocalName == "Name")).ToArray<XmlElement>();
      HashSet<XmlNode> xmlNodeSet = new HashSet<XmlNode>();
      bool flag = array.Length != 0 && string.Equals(array[0].NamespaceURI, "http://www.acumatica.com/generic/", StringComparison.OrdinalIgnoreCase);
      foreach (XmlElement xmlElement1 in array)
      {
        XmlNode parentNode1 = xmlElement1.ParentNode;
        XmlElement parentNode2 = (XmlElement) parentNode1.ParentNode;
        XmlNode parent;
        if (parentNode2.LocalName == "Actions")
          parent = (XmlNode) parentNode2;
        else if (parentNode2.LocalName == "ServiceCommands" || parentNode2.LocalName == "Keys")
        {
          XmlNode parentNode3 = parentNode2.ParentNode;
          parent = parentNode3.AppendChild(parentNode3.RemoveChild((XmlNode) parentNode2));
        }
        else
        {
          if (parentNode2.LocalName == "Descriptor")
          {
            xmlNodeSet.Add(parentNode1);
            continue;
          }
          parent = parentNode2.ParentNode;
        }
        XmlElement xmlElement2 = parent.AppendChild(xmlElement1.InnerText);
        foreach (XmlNode newChild in parentNode1.ChildNodes.Cast().ToArray<object>())
          xmlElement2.AppendChild(newChild);
        foreach (XmlAttribute node in parentNode1.Attributes.Cast().ToArray<object>())
          xmlElement2.Attributes.Append(node);
        if (flag && parentNode2.LocalName != "Containers")
          xmlElement2.AppendChild((XmlNode) document.CreateElement(xmlElement1.Prefix, xmlElement1.LocalName, xmlElement1.NamespaceURI)).InnerText = xmlElement1.InnerText;
        if (parent != parentNode2)
        {
          if (!xmlNodeSet.Contains((XmlNode) parentNode2))
            xmlNodeSet.Add((XmlNode) parentNode2);
        }
        else if (!xmlNodeSet.Contains(parentNode1))
          xmlNodeSet.Add(parentNode1);
        xmlNodeSet.Add((XmlNode) xmlElement1);
      }
      foreach (XmlNode oldChild in xmlNodeSet)
        oldChild.ParentNode.RemoveChild(oldChild);
      base.TransformResultMessage(document);
    }
    catch
    {
    }
  }
}
