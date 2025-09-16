// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.DitaTopicReader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA.Schems;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace PX.Data.Wiki.DITA;

public class DitaTopicReader : IWikiPageMaint
{
  public StringBuilder Context;
  public static Stack<PXDitaElement> DitaplatesStack;
  private static List<PXDitaElement> _elements;
  public ExportContext _exportContext;
  private PXGraph graph;

  public DitaTopicReader()
  {
    this.Context = new StringBuilder();
    this._exportContext = new ExportContext();
    this.graph = new PXGraph();
  }

  public DitaTopicReader(ExportContext exportContext)
  {
    this.Context = new StringBuilder();
    this._exportContext = new ExportContext();
    this._exportContext._ConRef = exportContext._ConRef;
    this._exportContext.WikiID = exportContext.WikiID;
    this.graph = new PXGraph();
  }

  private static PXDitaElement MakeDitaElement(string elementType, string elementValue)
  {
    if (elementType != null)
    {
      switch (elementType.Length)
      {
        case 1:
          switch (elementType[0])
          {
            case 'b':
              return (PXDitaElement) new PXBoldDitaElement();
            case 'i':
              return (PXDitaElement) new PXItalicDitaElement();
            case 'p':
              return (PXDitaElement) new PXParagraphDitaElement();
            case 'q':
              return (PXDitaElement) new PXQDitaElement();
            case 'u':
              return (PXDitaElement) new PXUnderlinedDitaElement();
          }
          break;
        case 2:
          switch (elementType[1])
          {
            case 'd':
              if (elementType == "dd")
                return (PXDitaElement) new PXDDDitaElement();
              break;
            case 'h':
              if (elementType == "ph")
                return (PXDitaElement) new PXPhDitaElement();
              break;
            case 'i':
              if (elementType == "li")
                return (PXDitaElement) new PXLiDitaElement();
              break;
            case 'l':
              switch (elementType)
              {
                case "dl":
                  return (PXDitaElement) new PXDlDitaElement();
                case "ol":
                  return (PXDitaElement) new PXOlDitaElement();
                case "sl":
                  return (PXDitaElement) new PXSlDitaElement();
                case "ul":
                  return (PXDitaElement) new PXUlDitaElement();
              }
              break;
            case 'm':
              if (elementType == "tm")
                return (PXDitaElement) new PXTmDitaElement();
              break;
            case 'n':
              if (elementType == "fn")
                return (PXDitaElement) new PXFnDitaElement();
              break;
            case 'q':
              if (elementType == "lq")
                return (PXDitaElement) new PXLqDitaElement();
              break;
            case 't':
              switch (elementType)
              {
                case "dt":
                  return (PXDitaElement) new PXDtDitaElement();
                case "tt":
                  return (PXDitaElement) new PXttDitaElement();
              }
              break;
          }
          break;
        case 3:
          switch (elementType[2])
          {
            case 'b':
              if (elementType == "sub")
                return (PXDitaElement) new PXSubDitaElement();
              break;
            case 'd':
              if (elementType == "cmd")
                return (PXDitaElement) new PXCmdDitaElement();
              break;
            case 'e':
              if (elementType == "pre")
                return (PXDitaElement) new PXPreDitaElement();
              break;
            case 'g':
              if (elementType == "fig")
                return (PXDitaElement) new PXFigDitaElement();
              break;
            case 'i':
              if (elementType == "sli")
                return (PXDitaElement) new PXSliDitaElement();
              break;
            case 'p':
              if (elementType == "sup")
                return (PXDitaElement) new PXSupDitaElement();
              break;
            case 't':
              if (elementType == "alt")
                return (PXDitaElement) new PXAltDitaElement();
              break;
            case 'w':
              if (elementType == "row")
                return (PXDitaElement) new PXTableRawDitaElement();
              break;
          }
          break;
        case 4:
          switch (elementType[0])
          {
            case 'b':
              if (elementType == "body")
                return (PXDitaElement) new PXBodyDitaElement();
              break;
            case 'c':
              if (elementType == "cite")
                return (PXDitaElement) new PXCiteDitaElement();
              break;
            case 'd':
              switch (elementType)
              {
                case "desc":
                  return (PXDitaElement) new PXDescDitaElement();
                case "ddhd":
                  return (PXDitaElement) new PXDdhdDitaElement();
                case "dthd":
                  return (PXDitaElement) new PXDthdDitaElement();
              }
              break;
            case 'l':
              if (elementType == "link")
                return (PXDitaElement) new PXLinkDitaElement();
              break;
            case 'n':
              if (elementType == "note")
                return (PXDitaElement) new PXNoteDitaElement();
              break;
            case 's':
              if (elementType == "step")
                return (PXDitaElement) new PXStepDitaElement();
              break;
            case 't':
              switch (elementType)
              {
                case "term":
                  return (PXDitaElement) new PXTermDitaElement();
                case "task":
                  return (PXDitaElement) new PXTaskDitaElement();
              }
              break;
            case 'x':
              if (elementType == "xref")
                return (PXDitaElement) new PXXrefDitaElement();
              break;
          }
          break;
        case 5:
          switch (elementType[3])
          {
            case 'a':
              switch (elementType)
              {
                case "param":
                  return (PXDitaElement) new PXParamDitaElement();
                case "thead":
                  return (PXDitaElement) new PXTableTHeadDitaElement();
              }
              break;
            case 'd':
              if (elementType == "tbody")
                return (PXDitaElement) new PXTableTBodyDitaElement();
              break;
            case 'e':
              if (elementType == "lines")
                return (PXDitaElement) new PXLinesDitaElement();
              break;
            case 'g':
              if (elementType == "image")
                return (PXDitaElement) new PXImageDitaElement();
              break;
            case 'i':
              if (elementType == "topic")
                return (PXDitaElement) new PXTopicDitaElement();
              break;
            case 'l':
              switch (elementType)
              {
                case "table":
                  return (PXDitaElement) new PXTableDitaElement();
                case "title":
                  return (PXDitaElement) new PXTitleDitaElement();
              }
              break;
            case 'o':
              if (elementType == "strow")
                return (PXDitaElement) new PXStrowDitaElement();
              break;
            case 'p':
              if (elementType == "steps")
                return (PXDitaElement) new PXStepsDitaElement();
              break;
            case 'r':
              if (elementType == "entry")
                return (PXDitaElement) new PXTableCellDitaElement();
              break;
            case 'x':
              if (elementType == "#text")
              {
                PXTextDitaElement pxTextDitaElement = new PXTextDitaElement();
                pxTextDitaElement.Content = elementValue;
                return (PXDitaElement) pxTextDitaElement;
              }
              break;
          }
          break;
        case 6:
          switch (elementType[0])
          {
            case 'd':
              if (elementType == "dlhead")
                return (PXDitaElement) new PXDlheadDitaElement();
              break;
            case 'o':
              if (elementType == "object")
                return (PXDitaElement) new PXObjectDitaElement();
              break;
            case 's':
              if (elementType == "sthead")
                return (PXDitaElement) new PXStheadDitaElement();
              break;
            case 't':
              if (elementType == "tgroup")
                return (PXDitaElement) new PXTableTGroupDitaElement();
              break;
          }
          break;
        case 7:
          switch (elementType[2])
          {
            case 'a':
              if (elementType == "example")
                return (PXDitaElement) new PXExampleDitaElement();
              break;
            case 'c':
              if (elementType == "section")
                return (PXDitaElement) new PXSectionDitaElement();
              break;
            case 'e':
              switch (elementType)
              {
                case "dlentry":
                  return (PXDitaElement) new PXDlentryDitaElement();
                case "stentry":
                  return (PXDitaElement) new PXStentryDitaElement();
              }
              break;
            case 'f':
              if (elementType == "refbody")
                return (PXDitaElement) new PXRefbodyDitaElement();
              break;
            case 'l':
              if (elementType == "colspec")
                return (PXDitaElement) new PXTableColSpecDitaElement();
              break;
            case 'n':
              switch (elementType)
              {
                case "context":
                  return (PXDitaElement) new PXContextDitaElement();
                case "conbody":
                  return (PXDitaElement) new PXConbodyDitaElement();
                case "concept":
                  return (PXDitaElement) new PXConceptDitaElement();
              }
              break;
            case 's':
              if (elementType == "postreq")
                return (PXDitaElement) new PXPostreqDitaElement();
              break;
            case 'y':
              if (elementType == "keyword")
                return (PXDitaElement) new PXKeywordDitaElement();
              break;
          }
          break;
        case 8:
          switch (elementType[4])
          {
            case 'b':
              if (elementType == "taskbody")
                return (PXDitaElement) new PXTaskbodyDitaElement();
              break;
            case 'l':
              if (elementType == "linklist")
                return (PXDitaElement) new PXLinklistDitaElement();
              break;
            case 'r':
              if (elementType == "figgroup")
                return (PXDitaElement) new PXFiggroupDitaElement();
              break;
            case 't':
              if (elementType == "linktext")
                return (PXDitaElement) new PXLinkTextDitaElement();
              break;
          }
          break;
        case 9:
          switch (elementType[0])
          {
            case 'c':
              if (elementType == "codeblock")
                return (PXDitaElement) new PXCodeBlockDitaElement();
              break;
            case 'r':
              if (elementType == "reference")
                return (PXDitaElement) new PXReferenceDitaElement();
              break;
            case 's':
              if (elementType == "shortdesc")
                return (PXDitaElement) new PXShortdescDitaElement();
              break;
            case 'u':
              if (elementType == "uicontrol")
                return (PXDitaElement) new PXUicontrolDitaElement();
              break;
          }
          break;
        case 10:
          if (elementType == "sectiondiv")
            return (PXDitaElement) new PXSectiondivDitaElement();
          break;
        case 11:
          switch (elementType[1])
          {
            case 'e':
              if (elementType == "menucascade")
                return (PXDitaElement) new PXMenucascadeDitaElement();
              break;
            case 'i':
              if (elementType == "simpletable")
                return (PXDitaElement) new PXSimpletableDitaElement();
              break;
            case 'o':
              if (elementType == "longdescref")
                return (PXDitaElement) new PXLongdescrefDitaElement();
              break;
            case 't':
              if (elementType == "step-result")
                return (PXDitaElement) new PXStep_resultDitaElement();
              break;
          }
          break;
        case 12:
          if (elementType == "longquoteref")
            return (PXDitaElement) new PXLongquoterefDitaElement();
          break;
        case 13:
          switch (elementType[0])
          {
            case 'd':
              if (elementType == "draft-comment")
                return (PXDitaElement) new PXdraftcommentDitaElement();
              break;
            case 'r':
              if (elementType == "related-links")
                return (PXDitaElement) new PXRelated_linksDitaElement();
              break;
          }
          break;
        case 14:
          if (elementType == "steps-informal")
            return (PXDitaElement) new PXSteps_informalDitaElement();
          break;
      }
    }
    return (PXDitaElement) new PXNoneDitaElement();
  }

  public void MakeDitaTree(Stream source)
  {
    XmlDocument xmlDocument = new XmlDocument()
    {
      XmlResolver = (XmlResolver) null
    };
    xmlDocument.XmlResolver = (XmlResolver) null;
    xmlDocument.Load(source);
    XmlNodeList xmlNodeList1 = xmlDocument.SelectNodes("topic");
    XmlNodeList xmlNodeList2 = xmlDocument.SelectNodes("concept");
    XmlNodeList xmlNodeList3 = xmlDocument.SelectNodes("task");
    XmlNodeList xmlNodeList4 = xmlDocument.SelectNodes("reference");
    List<XmlNode> xmlNodeList5 = new List<XmlNode>();
    if (xmlNodeList1 != null && xmlNodeList1.Count > 0)
      xmlNodeList5.Add(xmlNodeList1[0]);
    if (xmlNodeList2 != null && xmlNodeList2.Count > 0)
      xmlNodeList5.Add(xmlNodeList2[0]);
    if (xmlNodeList3 != null && xmlNodeList3.Count > 0)
      xmlNodeList5.Add(xmlNodeList3[0]);
    if (xmlNodeList4 != null && xmlNodeList4.Count > 0)
      xmlNodeList5.Add(xmlNodeList4[0]);
    Stack<DitaTopicReader.NodeTreeLevel> nodeTreeLevelStack = new Stack<DitaTopicReader.NodeTreeLevel>();
    DitaTopicReader.DitaplatesStack = new Stack<PXDitaElement>();
    DitaTopicReader._elements = new List<PXDitaElement>();
    ((IEnumerator) xmlNodeList5.GetEnumerator()).MoveNext();
    foreach (XmlNode parent in (IEnumerable<XmlNode>) xmlNodeList5)
    {
      PXDitaElement pxDitaElement1 = DitaTopicReader.MakeDitaElement(parent.Name.ToString((IFormatProvider) CultureInfo.InvariantCulture), "");
      if (parent.Attributes != null)
      {
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) parent.Attributes)
          pxDitaElement1.AddAttribute(attribute.Name, attribute.Value);
      }
      DitaTopicReader.DitaplatesStack.Push(pxDitaElement1);
      DitaTopicReader._elements.Add(pxDitaElement1);
      List<XmlNode> xmlNodeList6 = new List<XmlNode>();
      for (int i = 0; i < parent.ChildNodes.Count; ++i)
        xmlNodeList6.Add(parent.ChildNodes[i]);
      IEnumerable<XmlNode> xmlNodes1 = (IEnumerable<XmlNode>) xmlNodeList6;
      DitaTopicReader.NodeTreeLevel nodeTreeLevel1 = new DitaTopicReader.NodeTreeLevel(parent, xmlNodes1.GetEnumerator());
      nodeTreeLevelStack.Push(nodeTreeLevel1);
      while (nodeTreeLevelStack.Count > 0)
      {
        DitaTopicReader.NodeTreeLevel nodeTreeLevel2 = nodeTreeLevelStack.Pop();
        PXDitaElement pxDitaElement2 = DitaTopicReader.DitaplatesStack.Pop();
        if (nodeTreeLevel2.ChildsIterator.MoveNext())
        {
          XmlNode current = nodeTreeLevel2.ChildsIterator.Current;
          if (current != null)
          {
            nodeTreeLevelStack.Push(nodeTreeLevel2);
            DitaTopicReader.DitaplatesStack.Push(pxDitaElement2);
            PXDitaElement pxditaelement = DitaTopicReader.MakeDitaElement(current.Name.ToString((IFormatProvider) CultureInfo.InvariantCulture), current.Value);
            if (current.Attributes != null)
            {
              foreach (XmlAttribute attribute in (XmlNamedNodeMap) current.Attributes)
                pxditaelement.AddAttribute(attribute.Name, attribute.Value);
            }
            pxDitaElement2.AddChild(pxditaelement);
            List<XmlNode> xmlNodeList7 = new List<XmlNode>();
            for (int i = 0; i < current.ChildNodes.Count; ++i)
              xmlNodeList7.Add(current.ChildNodes[i]);
            IEnumerable<XmlNode> xmlNodes2 = (IEnumerable<XmlNode>) xmlNodeList7;
            if (xmlNodes2 != null)
            {
              DitaTopicReader.NodeTreeLevel nodeTreeLevel3 = new DitaTopicReader.NodeTreeLevel(current, xmlNodes2.GetEnumerator());
              nodeTreeLevelStack.Push(nodeTreeLevel3);
              DitaTopicReader.DitaplatesStack.Push(pxditaelement);
            }
          }
        }
      }
    }
  }

  public void RenderDitaTree()
  {
    foreach (PXDitaElement element in DitaTopicReader._elements)
      this.Context = element.Read(this.Context, this._exportContext);
  }

  public void WriteWiki(string fileName, int number)
  {
    PXSelectJoin<WikiPageDITADac.WikiPage, InnerJoin<WikiPageDITADac.WikiPageLanguage, On<PX.SM.WikiPageLanguage.pageID, Equal<WikiPageDITADac.WikiPage.pageID>>, InnerJoin<WikiPageDITADac.WikiRevision, On<WikiPageDITADac.WikiRevision.pageID, Equal<WikiPageDITADac.WikiPage.pageID>>, InnerJoin<WikiPageDITADac.WikiArticle, On<WikiPageDITADac.WikiArticle.pageID, Equal<WikiPageDITADac.WikiPage.pageID>>>>>, Where<WikiPageDITADac.WikiPage.pageID, Equal<Required<WikiPageDITADac.WikiPage.pageID>>>> pxSelectJoin = new PXSelectJoin<WikiPageDITADac.WikiPage, InnerJoin<WikiPageDITADac.WikiPageLanguage, On<PX.SM.WikiPageLanguage.pageID, Equal<WikiPageDITADac.WikiPage.pageID>>, InnerJoin<WikiPageDITADac.WikiRevision, On<WikiPageDITADac.WikiRevision.pageID, Equal<WikiPageDITADac.WikiPage.pageID>>, InnerJoin<WikiPageDITADac.WikiArticle, On<WikiPageDITADac.WikiArticle.pageID, Equal<WikiPageDITADac.WikiPage.pageID>>>>>, Where<WikiPageDITADac.WikiPage.pageID, Equal<Required<WikiPageDITADac.WikiPage.pageID>>>>(this.graph);
    string text = this.Context.ToString();
    int startIndex = fileName.IndexOf(".");
    if (startIndex > -1)
      fileName = fileName.Remove(startIndex, fileName.Length - startIndex);
    object[] objArray = new object[1]
    {
      (object) this._exportContext.topicid
    };
    using (IEnumerator<PXResult<WikiPageDITADac.WikiPage>> enumerator = pxSelectJoin.Select(objArray).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXResult<WikiPageDITADac.WikiPage, WikiPageDITADac.WikiPageLanguage, WikiPageDITADac.WikiRevision, WikiPageDITADac.WikiArticle> current = (PXResult<WikiPageDITADac.WikiPage, WikiPageDITADac.WikiPageLanguage, WikiPageDITADac.WikiRevision, WikiPageDITADac.WikiArticle>) enumerator.Current;
        WikiPageDITADac.WikiPage wikiPage = (WikiPageDITADac.WikiPage) current[typeof (WikiPageDITADac.WikiPage)];
        WikiPageDITADac.WikiPageLanguage wikiPageLanguage = (WikiPageDITADac.WikiPageLanguage) current[typeof (WikiPageDITADac.WikiPageLanguage)];
        WikiPageDITADac.WikiRevision wikiRevision = (WikiPageDITADac.WikiRevision) current[typeof (WikiPageDITADac.WikiRevision)];
        WikiPageDITADac.WikiArticle wikiArticle = (WikiPageDITADac.WikiArticle) current[typeof (WikiPageDITADac.WikiArticle)];
        this.UpdateFile(text, fileName, this.graph, this._exportContext.WikiPageTitle.ToString(), wikiPage, wikiPageLanguage, wikiRevision, wikiArticle, number);
        return;
      }
    }
    this.InsertFile(this._exportContext.topicid, text, fileName, this.graph, this._exportContext.WikiPageTitle.ToString(), number);
  }

  public void Read(Stream source, string fileName, int number)
  {
    this.MakeDitaTree(source);
    this.RenderDitaTree();
    this.WriteWiki(fileName, number);
  }

  public void InsertFile(
    Guid guid,
    string text,
    string fileName,
    PXGraph graph,
    string title,
    int number)
  {
    WikiPageDITADac.WikiArticle wikiArticle1 = new WikiPageDITADac.WikiArticle();
    wikiArticle1.PageID = new Guid?(guid);
    wikiArticle1.Name = fileName;
    wikiArticle1.WikiID = new Guid?(this._exportContext.WikiID);
    wikiArticle1.Folder = new bool?(false);
    wikiArticle1.ArticleType = new int?(10);
    wikiArticle1.Versioned = new bool?(true);
    wikiArticle1.ParentUID = new Guid?(this._exportContext.WikiID);
    wikiArticle1.StatusID = new int?(3);
    wikiArticle1.Number = new double?((double) number);
    wikiArticle1.CreatedByID = new Guid?(guid);
    wikiArticle1.CreatedDateTime = new System.DateTime?(System.DateTime.Now);
    wikiArticle1.CategoryID = new int?(6);
    wikiArticle1.LastModifiedByID = new Guid?(guid);
    WikiPageDITADac.WikiArticle wikiArticle2 = wikiArticle1;
    graph.Caches[typeof (WikiPageDITADac.WikiArticle)].Insert((object) wikiArticle2);
    graph.Caches[typeof (WikiPageDITADac.WikiArticle)].Persist(PXDBOperation.Insert);
    WikiPageDITADac.WikiPageLanguage wikiPageLanguage = new WikiPageDITADac.WikiPageLanguage()
    {
      PageID = new Guid?(guid),
      Language = "en-US",
      Title = title,
      LastRevisionID = new int?(1),
      LastPublishedID = new int?(1)
    };
    graph.Caches[typeof (WikiPageDITADac.WikiPageLanguage)].Insert((object) wikiPageLanguage);
    graph.Caches[typeof (WikiPageDITADac.WikiPageLanguage)].Persist(PXDBOperation.Insert);
    WikiPageDITADac.WikiRevision wikiRevision = new WikiPageDITADac.WikiRevision()
    {
      PageID = new Guid?(guid),
      Language = "en-US",
      PageRevisionID = new int?(1),
      Content = text,
      CreatedByID = new Guid?(guid),
      CreatedDateTime = new System.DateTime?(System.DateTime.Now),
      UID = new Guid?(guid)
    };
    graph.Caches[typeof (WikiPageDITADac.WikiRevision)].Insert((object) wikiRevision);
    graph.Caches[typeof (WikiPageDITADac.WikiRevision)].Persist(PXDBOperation.Insert);
  }

  public void UpdateFile(
    string text,
    string fileName,
    PXGraph graph,
    string title,
    WikiPageDITADac.WikiPage wikiPage,
    WikiPageDITADac.WikiPageLanguage wikiPageLanguage,
    WikiPageDITADac.WikiRevision wikiRevision,
    WikiPageDITADac.WikiArticle wikiArticle,
    int number)
  {
    wikiArticle.Folder = new bool?(false);
    wikiArticle.Name = fileName;
    graph.Caches[typeof (WikiPageDITADac.WikiArticle)].Update((object) wikiArticle);
    graph.Caches[typeof (WikiPageDITADac.WikiArticle)].Persist(PXDBOperation.Update);
    wikiPageLanguage.Title = title;
    graph.Caches[typeof (WikiPageDITADac.WikiPageLanguage)].Update((object) wikiPageLanguage);
    graph.Caches[typeof (WikiPageDITADac.WikiPageLanguage)].Persist(PXDBOperation.Update);
    PX.SM.WikiRevision wikiRevision1 = (PX.SM.WikiRevision) new PXSelect<PX.SM.WikiRevision, Where<PX.SM.WikiRevision.pageID, Equal<Required<PX.SM.WikiRevision.pageID>>>, OrderBy<Desc<PX.SM.WikiRevision.pageRevisionID>>>(graph).SelectWindowed(0, 1, (object) wikiRevision.PageID);
    WikiPageDITADac.WikiRevision wikiRevision2 = new WikiPageDITADac.WikiRevision()
    {
      PageID = wikiRevision1.PageID,
      Language = "en-US",
      PageRevisionID = wikiRevision1.PageRevisionID,
      Content = text,
      CreatedByID = wikiRevision1.PageID,
      CreatedDateTime = new System.DateTime?(System.DateTime.Now),
      UID = wikiRevision1.UID
    };
    graph.Caches[typeof (WikiPageDITADac.WikiRevision)].Update((object) wikiRevision2);
    graph.Caches[typeof (WikiPageDITADac.WikiRevision)].Persist(PXDBOperation.Update);
  }

  public void InitNew(Guid wikiid) => throw new NotImplementedException();

  public void InitNew(Guid? wikiid, Guid? parentid, string name)
  {
    throw new NotImplementedException();
  }

  public string GetFileUrl
  {
    get => throw new NotImplementedException();
    set => throw new NotImplementedException();
  }

  public Guid CurrentAttachmentGuid
  {
    get => throw new NotImplementedException();
    set => throw new NotImplementedException();
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
