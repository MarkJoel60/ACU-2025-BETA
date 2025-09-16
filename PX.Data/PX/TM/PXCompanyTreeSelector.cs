// Decompiled with JetBrains decompiler
// Type: PX.TM.PXCompanyTreeSelector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.TM;

/// <summary>Shows a company tree.</summary>
/// <example>
/// <code title="" description="" lang="neutral"></code>
/// </example>
public class PXCompanyTreeSelector : PXSelectorAttribute
{
  public const string TreeViewName = "_EPCompanyTree_Tree_";

  public PXCompanyTreeSelector()
    : base(typeof (EPCompanyTree.workGroupID))
  {
    this.DescriptionField = typeof (EPCompanyTree.description);
    this.SubstituteKey = typeof (EPCompanyTree.description);
    this.SelectorMode = PXSelectorMode.DisplayModeValue;
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (sender.Graph.Views.ContainsKey("_EPCompanyTree_Tree_"))
      return;
    PXSelectCompanyTree selectCompanyTree = new PXSelectCompanyTree(sender.Graph);
    sender.Graph.Views.Add("_EPCompanyTree_Tree_", selectCompanyTree.View);
  }
}
