// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSelectSiteMapTreeBase`6
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

/// <exclude />
public class PXSelectSiteMapTreeBase<IsSecure, HideEmptyFolder, HideWikiNode, HideNodeThatCantBeAutomated, HideGenericInquiryNode, MapType> : 
  PXSelectBase<MapType>
  where IsSecure : IConstant<bool>, IBqlOperand, new()
  where HideEmptyFolder : IConstant<bool>, IBqlOperand, new()
  where HideWikiNode : IConstant<bool>, IBqlOperand, new()
  where HideNodeThatCantBeAutomated : IConstant<bool>, IBqlOperand, new()
  where HideGenericInquiryNode : IConstant<bool>, IBqlOperand, new()
  where MapType : SiteMap, new()
{
  private readonly PXSiteMapEnumeratorBase siteMapEnumerator;
  private readonly bool isSecure;
  private readonly bool hideEmptyFolder;
  private readonly bool hideWikiNode;
  private readonly bool hideNodeThatCantBeAutomated;
  private readonly bool hideGenericInquiryNode;

  public PXSelectSiteMapTreeBase()
  {
    this.InitializeParemeterFromType<IsSecure>(ref this.isSecure);
    this.InitializeParemeterFromType<HideEmptyFolder>(ref this.hideEmptyFolder);
    this.InitializeParemeterFromType<HideWikiNode>(ref this.hideWikiNode);
    this.InitializeParemeterFromType<HideNodeThatCantBeAutomated>(ref this.hideNodeThatCantBeAutomated);
    this.InitializeParemeterFromType<HideGenericInquiryNode>(ref this.hideGenericInquiryNode);
    this.siteMapEnumerator = (PXSiteMapEnumeratorBase) new PXSiteMapEnumerator(this.isSecure, new PXSiteMapNodeFilter((IEnumerable<Func<PXSiteMapNode, bool>>) this.NodeFilterRules));
  }

  public PXSelectSiteMapTreeBase(PXGraph graph)
    : this()
  {
    this.View = this.CreateView(graph, (Delegate) new PXSelectDelegate<Guid?>(this.sitemap));
  }

  public PXSelectSiteMapTreeBase(PXGraph graph, Delegate handler)
    : this()
  {
    this.View = this.CreateView(graph, handler);
  }

  protected virtual PXView CreateView(PXGraph graph, Delegate handler)
  {
    return new PXView(graph, false, (BqlCommand) new PX.Data.Select<MapType, Where<SiteMap.parentID, Equal<Argument<Guid?>>>, OrderBy<Asc<SiteMap.position>>>(), handler);
  }

  private void InitializeParemeterFromType<SourceType>(ref bool destinationParameter) where SourceType : IConstant<bool>, IBqlOperand, new()
  {
    destinationParameter = new SourceType().Value;
  }

  protected virtual List<Func<PXSiteMapNode, bool>> NodeFilterRules
  {
    get
    {
      List<Func<PXSiteMapNode, bool>> nodeFilterRules = new List<Func<PXSiteMapNode, bool>>();
      if (this.isSecure)
        nodeFilterRules.Add(PXSelectSiteMapTreeBase<IsSecure, HideEmptyFolder, HideWikiNode, HideNodeThatCantBeAutomated, HideGenericInquiryNode, MapType>.\u003C\u003EO.\u003C0\u003E__IsNotAccessible ?? (PXSelectSiteMapTreeBase<IsSecure, HideEmptyFolder, HideWikiNode, HideNodeThatCantBeAutomated, HideGenericInquiryNode, MapType>.\u003C\u003EO.\u003C0\u003E__IsNotAccessible = new Func<PXSiteMapNode, bool>(PXSiteMapFilterRuleStorage.IsNotAccessible)));
      if (this.hideEmptyFolder)
        nodeFilterRules.Add(PXSelectSiteMapTreeBase<IsSecure, HideEmptyFolder, HideWikiNode, HideNodeThatCantBeAutomated, HideGenericInquiryNode, MapType>.\u003C\u003EO.\u003C1\u003E__IsEmptyFolder ?? (PXSelectSiteMapTreeBase<IsSecure, HideEmptyFolder, HideWikiNode, HideNodeThatCantBeAutomated, HideGenericInquiryNode, MapType>.\u003C\u003EO.\u003C1\u003E__IsEmptyFolder = new Func<PXSiteMapNode, bool>(PXSiteMapFilterRuleStorage.IsEmptyFolder)));
      if (this.hideWikiNode)
        nodeFilterRules.Add(PXSelectSiteMapTreeBase<IsSecure, HideEmptyFolder, HideWikiNode, HideNodeThatCantBeAutomated, HideGenericInquiryNode, MapType>.\u003C\u003EO.\u003C2\u003E__IsWiki ?? (PXSelectSiteMapTreeBase<IsSecure, HideEmptyFolder, HideWikiNode, HideNodeThatCantBeAutomated, HideGenericInquiryNode, MapType>.\u003C\u003EO.\u003C2\u003E__IsWiki = new Func<PXSiteMapNode, bool>(PXSiteMapFilterRuleStorage.IsWiki)));
      if (this.hideNodeThatCantBeAutomated)
        nodeFilterRules.Add(PXSelectSiteMapTreeBase<IsSecure, HideEmptyFolder, HideWikiNode, HideNodeThatCantBeAutomated, HideGenericInquiryNode, MapType>.\u003C\u003EO.\u003C3\u003E__CantBeAutomated ?? (PXSelectSiteMapTreeBase<IsSecure, HideEmptyFolder, HideWikiNode, HideNodeThatCantBeAutomated, HideGenericInquiryNode, MapType>.\u003C\u003EO.\u003C3\u003E__CantBeAutomated = new Func<PXSiteMapNode, bool>(PXSiteMapFilterRuleStorage.CantBeAutomated)));
      if (this.hideGenericInquiryNode)
        nodeFilterRules.Add(PXSelectSiteMapTreeBase<IsSecure, HideEmptyFolder, HideWikiNode, HideNodeThatCantBeAutomated, HideGenericInquiryNode, MapType>.\u003C\u003EO.\u003C4\u003E__IsGenericInquiry ?? (PXSelectSiteMapTreeBase<IsSecure, HideEmptyFolder, HideWikiNode, HideNodeThatCantBeAutomated, HideGenericInquiryNode, MapType>.\u003C\u003EO.\u003C4\u003E__IsGenericInquiry = new Func<PXSiteMapNode, bool>(PXSiteMapFilterRuleStorage.IsGenericInquiry)));
      return nodeFilterRules;
    }
  }

  protected virtual IEnumerable sitemap([PXGuid] Guid? NodeID)
  {
    return (IEnumerable) this.siteMapEnumerator.SiteMapNodes(NodeID);
  }
}
