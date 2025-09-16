// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.DitaMapReader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace PX.Data.Wiki.DITA;

internal class DitaMapReader
{
  public static StringBuilder Context;
  public static Stack<PXDitaElement> DitaplatesStack;
  private static List<PXDitaElement> _elements;
  private static ExportContext _exportContext;

  public DitaMapReader() => DitaMapReader._exportContext = new ExportContext();

  private static PXDitaElement MakeDitaMapElement(string elementType, string elementValue)
  {
    return elementType == "topicref" ? (PXDitaElement) new PXTopicRefDitaElement() : (PXDitaElement) new PXMapDitaElement();
  }

  public void Read(Stream source, string fileName)
  {
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.XmlResolver = (XmlResolver) null;
    xmlDocument.Load(source);
    XmlNodeList xmlNodeList1 = xmlDocument.SelectNodes("map");
    List<XmlNode> xmlNodeList2 = new List<XmlNode>();
    if (xmlNodeList1 != null && xmlNodeList1.Count > 0)
      xmlNodeList2.Add(xmlNodeList1[0]);
    Stack<DitaMapReader.NodeTreeLevel> nodeTreeLevelStack = new Stack<DitaMapReader.NodeTreeLevel>();
    DitaMapReader.DitaplatesStack = new Stack<PXDitaElement>();
    DitaMapReader._elements = new List<PXDitaElement>();
    ((IEnumerator) xmlNodeList2.GetEnumerator()).MoveNext();
    foreach (XmlNode parent in (IEnumerable<XmlNode>) xmlNodeList2)
    {
      PXDitaElement pxDitaElement1 = DitaMapReader.MakeDitaMapElement(parent.Name.ToString((IFormatProvider) CultureInfo.InvariantCulture), "");
      if (parent.Attributes != null)
      {
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) parent.Attributes)
          pxDitaElement1.AddAttribute(attribute.Name, attribute.Value);
      }
      DitaMapReader.DitaplatesStack.Push(pxDitaElement1);
      DitaMapReader._elements.Add(pxDitaElement1);
      List<XmlNode> xmlNodeList3 = new List<XmlNode>();
      for (int i = 0; i < parent.ChildNodes.Count; ++i)
        xmlNodeList3.Add(parent.ChildNodes[i]);
      IEnumerable<XmlNode> xmlNodes1 = (IEnumerable<XmlNode>) xmlNodeList3;
      DitaMapReader.NodeTreeLevel nodeTreeLevel1 = new DitaMapReader.NodeTreeLevel(parent, xmlNodes1.GetEnumerator());
      nodeTreeLevelStack.Push(nodeTreeLevel1);
      while (nodeTreeLevelStack.Count > 0)
      {
        DitaMapReader.NodeTreeLevel nodeTreeLevel2 = nodeTreeLevelStack.Pop();
        PXDitaElement pxDitaElement2 = DitaMapReader.DitaplatesStack.Pop();
        if (nodeTreeLevel2.ChildsIterator.MoveNext())
        {
          XmlNode current = nodeTreeLevel2.ChildsIterator.Current;
          if (current != null)
          {
            nodeTreeLevelStack.Push(nodeTreeLevel2);
            DitaMapReader.DitaplatesStack.Push(pxDitaElement2);
            PXDitaElement pxditaelement = DitaMapReader.MakeDitaMapElement(current.Name.ToString((IFormatProvider) CultureInfo.InvariantCulture), current.Value);
            if (current.Attributes != null)
            {
              foreach (XmlAttribute attribute in (XmlNamedNodeMap) current.Attributes)
                pxditaelement.AddAttribute(attribute.Name, attribute.Value);
            }
            pxDitaElement2.AddChild(pxditaelement);
            List<XmlNode> xmlNodeList4 = new List<XmlNode>();
            for (int i = 0; i < current.ChildNodes.Count; ++i)
              xmlNodeList4.Add(current.ChildNodes[i]);
            IEnumerable<XmlNode> xmlNodes2 = (IEnumerable<XmlNode>) xmlNodeList4;
            if (xmlNodes2 != null)
            {
              DitaMapReader.NodeTreeLevel nodeTreeLevel3 = new DitaMapReader.NodeTreeLevel(current, xmlNodes2.GetEnumerator());
              nodeTreeLevelStack.Push(nodeTreeLevel3);
              DitaMapReader.DitaplatesStack.Push(pxditaelement);
            }
          }
        }
      }
    }
    foreach (PXDitaElement element in DitaMapReader._elements)
      DitaMapReader.Context = element.Read(DitaMapReader.Context, DitaMapReader._exportContext);
  }

  private class NodeTreeLevel
  {
    private readonly IEnumerator<XmlNode> _childsIterator;

    public NodeTreeLevel(XmlNode parent, IEnumerator<XmlNode> childsIterator)
    {
      if (parent == null)
        throw new ArgumentNullException(nameof (parent));
      this._childsIterator = childsIterator != null ? childsIterator : throw new ArgumentNullException(nameof (childsIterator));
    }

    public IEnumerator<XmlNode> ChildsIterator => this._childsIterator;
  }
}
