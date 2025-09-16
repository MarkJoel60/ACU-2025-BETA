// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSelectSiteMapTree`5
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

/// <exclude />
public class PXSelectSiteMapTree<IsSecure, HideEmptyFolder, HideWikiNode, HideNodeThatCantBeAutomated, HideGenericInquiryNode>(
  PXGraph g) : 
  PXSelectSiteMapTreeBase<IsSecure, HideEmptyFolder, HideWikiNode, HideNodeThatCantBeAutomated, HideGenericInquiryNode, SiteMap>(g)
  where IsSecure : IConstant<bool>, IBqlOperand, new()
  where HideEmptyFolder : IConstant<bool>, IBqlOperand, new()
  where HideWikiNode : IConstant<bool>, IBqlOperand, new()
  where HideNodeThatCantBeAutomated : IConstant<bool>, IBqlOperand, new()
  where HideGenericInquiryNode : IConstant<bool>, IBqlOperand, new()
{
  protected override List<Func<PXSiteMapNode, bool>> NodeFilterRules
  {
    get
    {
      List<Func<PXSiteMapNode, bool>> nodeFilterRules = base.NodeFilterRules;
      nodeFilterRules.Add((Func<PXSiteMapNode, bool>) (_ => !string.IsNullOrWhiteSpace(_.ScreenID) && !_.ScreenID.EndsWith("000000")));
      return nodeFilterRules;
    }
  }
}
