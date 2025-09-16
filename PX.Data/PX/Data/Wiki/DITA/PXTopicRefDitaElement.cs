// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.PXTopicRefDitaElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA.Schems;
using System;
using System.Text;
using System.Xml;

#nullable disable
namespace PX.Data.Wiki.DITA;

public class PXTopicRefDitaElement : PXDitaContainer
{
  public override void Write(XmlTextWriter stream, IFileManager filemanager)
  {
    throw new NotImplementedException();
  }

  public override StringBuilder Read(StringBuilder globalContent1, ExportContext _context)
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder globalContent1_1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    string str;
    try
    {
      str = this.Attributs["href"];
    }
    catch
    {
      str = (string) null;
    }
    if (str != null)
    {
      ++_context.Number;
      int num = str.LastIndexOf("/");
      if (num > -1)
        str = str.Remove(0, num + 1);
      int startIndex = str.LastIndexOf(".");
      if (startIndex > -1)
        str = str.Remove(startIndex, str.Length - startIndex);
      StringBuilder ownname = stringBuilder1.Append(str);
      this.SetParent(ownname, _context.parentname, _context.Number);
      StringBuilder parentname = _context.parentname;
      _context.parentname = ownname;
      globalContent1_1 = this.ReadChilds(globalContent1_1, _context);
      _context.parentname = parentname;
    }
    return globalContent1_1;
  }

  public void SetParent(StringBuilder ownname, StringBuilder parent, int Number)
  {
    PXGraph graph = new PXGraph();
    PXSelect<WikiPageDITADac.WikiArticle, Where<WikiPageDITADac.WikiPage.name, Equal<Required<WikiPageDITADac.WikiPage.name>>>> pxSelect1 = new PXSelect<WikiPageDITADac.WikiArticle, Where<WikiPageDITADac.WikiPage.name, Equal<Required<WikiPageDITADac.WikiPage.name>>>>(graph);
    PXSelect<WikiPageDITADac.WikiArticle, Where<WikiPageDITADac.WikiPage.name, Equal<Required<WikiPageDITADac.WikiPage.name>>>> pxSelect2 = new PXSelect<WikiPageDITADac.WikiArticle, Where<WikiPageDITADac.WikiPage.name, Equal<Required<WikiPageDITADac.WikiPage.name>>>>(graph);
    PXSelect<WikiPageDITADac.WikiArticle, Where<WikiPageDITADac.WikiPage.name, Equal<Required<WikiPageDITADac.WikiPage.name>>>> pxSelect3 = new PXSelect<WikiPageDITADac.WikiArticle, Where<WikiPageDITADac.WikiPage.name, Equal<Required<WikiPageDITADac.WikiPage.name>>>>(graph);
    foreach (PXResult pxResult1 in pxSelect1.Select((object) ownname.ToString()))
    {
      WikiPageDITADac.WikiArticle wikiArticle1 = (WikiPageDITADac.WikiArticle) pxResult1[typeof (WikiPageDITADac.WikiArticle)];
      wikiArticle1.Number = new double?((double) Number);
      graph.Caches[typeof (WikiPageDITADac.WikiArticle)].Update((object) wikiArticle1);
      if (parent.Length > 0)
      {
        foreach (PXResult pxResult2 in pxSelect2.Select((object) parent.ToString()))
        {
          WikiPageDITADac.WikiArticle wikiArticle2 = (WikiPageDITADac.WikiArticle) pxResult2[typeof (WikiPageDITADac.WikiArticle)];
          wikiArticle1.ParentUID = wikiArticle2.PageID;
          graph.Caches[typeof (WikiPageDITADac.WikiArticle)].Update((object) wikiArticle1);
          wikiArticle2.Folder = new bool?(true);
          graph.Caches[typeof (WikiPageDITADac.WikiArticle)].Update((object) wikiArticle2);
          graph.Caches[typeof (WikiPageDITADac.WikiArticle)].Persist(PXDBOperation.Update);
        }
      }
      graph.Caches[typeof (WikiPageDITADac.WikiArticle)].Persist(PXDBOperation.Update);
    }
  }
}
