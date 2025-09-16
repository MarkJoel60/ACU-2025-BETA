// Decompiled with JetBrains decompiler
// Type: PX.Api.Soap.Screen.TransformMethodCallExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Linq;
using System.Xml;

#nullable disable
namespace PX.Api.Soap.Screen;

internal class TransformMethodCallExtension : TransformSoapMessageBase
{
  protected override void TransformInputMessage(XmlDocument document)
  {
    if (PXContext.GetSlot<bool>("FORCEUNTYPED"))
      return;
    try
    {
      XmlNodeList elementsByTagName = document.DocumentElement.GetElementsByTagName("SetSchema");
      if (elementsByTagName.Count != 1 || elementsByTagName[0].ChildNodes.Count == 0)
        return;
      if (elementsByTagName[0].ChildNodes[elementsByTagName[0].ChildNodes.Count - 1].Attributes.Count != 0 && !string.IsNullOrEmpty(elementsByTagName[0].ChildNodes[elementsByTagName[0].ChildNodes.Count - 1].Attributes[0].Value) && elementsByTagName[0].ChildNodes[elementsByTagName[0].ChildNodes.Count - 1].Attributes[0].Value.EndsWith("Content"))
        elementsByTagName[0].ChildNodes[elementsByTagName[0].ChildNodes.Count - 1].Attributes[0].Value = "Content";
      XmlNode parent1 = (XmlNode) null;
      foreach (XmlNode xmlNode1 in elementsByTagName[0].ChildNodes[elementsByTagName[0].ChildNodes.Count - 1].ChildNodes.Cast().ToArray<object>())
      {
        if (parent1 == null)
          parent1 = (XmlNode) elementsByTagName[0].ChildNodes[elementsByTagName[0].ChildNodes.Count - 1].AppendChild("Containers");
        if (xmlNode1.LocalName == "Actions")
        {
          foreach (XmlNode oldChild1 in xmlNode1.ChildNodes.Cast().ToArray<object>())
          {
            XmlNode parent2 = (XmlNode) xmlNode1.AppendChild("Action");
            bool flag = false;
            foreach (XmlNode oldChild2 in oldChild1.ChildNodes.Cast().ToArray<object>())
            {
              parent2.AppendChild(oldChild1.RemoveChild(oldChild2));
              flag = flag || oldChild2.LocalName == "Name";
            }
            if (!flag)
              parent2.AppendChild("Name").InnerText = oldChild1.LocalName;
            xmlNode1.RemoveChild(oldChild1);
          }
        }
        else
        {
          XmlNode parent3 = (XmlNode) parent1.AppendChild("Container");
          XmlNode parent4 = (XmlNode) parent3.AppendChild("Fields");
          bool flag1 = false;
          foreach (XmlNode xmlNode2 in xmlNode1.ChildNodes.Cast().ToArray<object>())
          {
            if (xmlNode2.LocalName == "ServiceCommands")
            {
              foreach (XmlNode oldChild3 in xmlNode2.ChildNodes.Cast().ToArray<object>())
              {
                XmlElement parent5 = xmlNode2.AppendChild("Command");
                if (oldChild3.LocalName.StartsWith("Key"))
                {
                  parent5.SetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance", "Key");
                  parent5.Attributes[0].Prefix = "xsi";
                }
                else if (oldChild3.LocalName.StartsWith("Every"))
                {
                  parent5.SetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance", "EveryValue");
                  parent5.Attributes[0].Prefix = "xsi";
                }
                else if (oldChild3.LocalName.StartsWith("Parameter"))
                {
                  parent5.SetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance", "Parameter");
                  parent5.Attributes[0].Prefix = "xsi";
                }
                else if (oldChild3.LocalName.StartsWith("Filter"))
                {
                  parent5.SetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance", "Field");
                  parent5.Attributes[0].Prefix = "xsi";
                }
                else if (oldChild3.LocalName.StartsWith("NewRow"))
                {
                  parent5.SetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance", "NewRow");
                  parent5.Attributes[0].Prefix = "xsi";
                }
                else if (oldChild3.LocalName.StartsWith("DeleteRow"))
                {
                  parent5.SetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance", "DeleteRow");
                  parent5.Attributes[0].Prefix = "xsi";
                }
                else if (oldChild3.LocalName.StartsWith("RowNumber"))
                {
                  parent5.SetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance", "RowNumber");
                  parent5.Attributes[0].Prefix = "xsi";
                }
                else if (oldChild3.LocalName.StartsWith("DialogAnswer"))
                {
                  parent5.SetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance", "Answer");
                  parent5.Attributes[0].Prefix = "xsi";
                }
                else if (oldChild3.LocalName.StartsWith("Attachment"))
                {
                  parent5.SetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance", "Attachment");
                  parent5.Attributes[0].Prefix = "xsi";
                }
                bool flag2 = false;
                foreach (XmlNode oldChild4 in oldChild3.ChildNodes.Cast().ToArray<object>())
                {
                  parent5.AppendChild(oldChild3.RemoveChild(oldChild4));
                  flag2 = flag2 || oldChild4.LocalName == "Name";
                }
                if (!flag2)
                  XML.AppendChild(parent5, "Name").InnerText = oldChild3.LocalName;
                xmlNode2.RemoveChild(oldChild3);
              }
              parent3.AppendChild(xmlNode1.RemoveChild(xmlNode2));
            }
            else if (xmlNode2.LocalName == "Name" && (!xmlNode2.HasChildNodes || xmlNode2.ChildNodes[0].NodeType == XmlNodeType.Text))
            {
              parent3.AppendChild(xmlNode1.RemoveChild(xmlNode2));
              flag1 = true;
            }
            else if (xmlNode2.LocalName == "DisplayName" && (!xmlNode2.HasChildNodes || xmlNode2.ChildNodes[0].NodeType == XmlNodeType.Text))
            {
              parent3.AppendChild(xmlNode1.RemoveChild(xmlNode2));
            }
            else
            {
              XmlNode parent6 = (XmlNode) parent4.AppendChild("Field");
              bool flag3 = false;
              foreach (XmlNode oldChild in xmlNode2.ChildNodes.Cast().ToArray<object>())
              {
                parent6.AppendChild(xmlNode2.RemoveChild(oldChild));
                flag3 = flag3 || oldChild.LocalName == "Name";
              }
              if (!flag3)
                parent6.AppendChild("Name").InnerText = xmlNode2.LocalName;
            }
          }
          if (!flag1)
            parent3.AppendChild("Name").InnerText = xmlNode1.LocalName;
          elementsByTagName[0].ChildNodes[elementsByTagName[0].ChildNodes.Count - 1].RemoveChild(xmlNode1);
        }
      }
      base.TransformInputMessage(document);
    }
    catch
    {
    }
  }
}
