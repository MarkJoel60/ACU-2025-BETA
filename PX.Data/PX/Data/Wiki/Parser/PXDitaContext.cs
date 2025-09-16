// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXDitaContext
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

public class PXDitaContext : PXWikiParserContext
{
  public PXDitaContext()
  {
    Stack<PXDitaElement> pxDitaElementStack = new Stack<PXDitaElement>();
    Topic topic = new Topic();
    PXDitaContext.CalcLinkCollection calcLinkCollection = new PXDitaContext.CalcLinkCollection();
    Dictionary<string, object> exchangedata = this.GetExchangedata();
    if (!exchangedata.ContainsKey("calcreflist"))
      exchangedata.Add("calcreflist", (object) calcLinkCollection);
    if (!exchangedata.ContainsKey("CurrentParent"))
      exchangedata.Add("CurrentParent", (object) pxDitaElementStack);
    if (!exchangedata.ContainsKey("topic"))
      exchangedata.Add("topic", (object) topic);
    if (exchangedata.ContainsKey("CellCount"))
      return;
    exchangedata.Add("CellCount", (object) 0);
  }

  public void AddInfoToExchangeField(IEnumerable<Package.MyFileInfo> filelist, Topic topic)
  {
    Stack<PXDitaElement> pxDitaElementStack = new Stack<PXDitaElement>();
    Dictionary<string, object> exchangedata = this.GetExchangedata();
    exchangedata["CurrentParent"] = (object) pxDitaElementStack;
    if (topic != null)
    {
      exchangedata[nameof (topic)] = (object) topic;
      ((PXDitaContext.CalcLinkCollection) exchangedata["calcreflist"]).AddLink(topic.TopicId, (object) topic);
    }
    foreach (Package.MyFileInfo tobject in filelist)
      ((PXDitaContext.CalcLinkCollection) exchangedata["calcreflist"]).AddLink(tobject.Guid, (object) tobject);
  }

  public class CalcLinkCollection
  {
    private Dictionary<Guid, Package.TempCalcLink> _globallinklist;

    public CalcLinkCollection()
    {
      this._globallinklist = new Dictionary<Guid, Package.TempCalcLink>();
    }

    public Package.TempCalcLink GetLink(Guid _guid)
    {
      if (this._globallinklist.ContainsKey(_guid))
        return this._globallinklist[_guid];
      Package.TempCalcLink link = new Package.TempCalcLink();
      this._globallinklist.Add(_guid, link);
      return link;
    }

    public void AddLink(Guid guid, object tobject)
    {
      if (this._globallinklist.ContainsKey(guid))
      {
        this._globallinklist[guid].SetLink(tobject);
      }
      else
      {
        Package.TempCalcLink tempCalcLink = new Package.TempCalcLink();
        tempCalcLink.SetLink(tobject);
        this._globallinklist.Add(guid, tempCalcLink);
      }
    }
  }
}
