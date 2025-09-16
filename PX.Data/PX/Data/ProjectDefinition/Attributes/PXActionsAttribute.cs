// Decompiled with JetBrains decompiler
// Type: PX.Data.ProjectDefinition.Attributes.PXActionsAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.ProjectDefinition.Attributes;

/// <exclude />
public class PXActionsAttribute : PXStringListAttribute
{
  private readonly System.Type _ScreenID;
  private readonly System.Type _View;

  public PXActionsAttribute(System.Type srceenID, System.Type view)
    : base(new string[0], new string[0])
  {
    this._ScreenID = srceenID;
    this._View = view;
    this._ExclusiveValues = false;
  }

  [InjectDependencyOnTypeLevel]
  private IPXPageIndexingService PageIndexingService { get; set; }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    string screenID = this.GetValue(sender.Graph, this._ScreenID) as string;
    string str = this.GetValue(sender.Graph, this._View) as string;
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    if (screenID != null)
    {
      string graphTypeByScreenId = this.PageIndexingService.GetGraphTypeByScreenID(screenID);
      if (graphTypeByScreenId != null)
      {
        string primaryView = this.PageIndexingService.GetPrimaryView(graphTypeByScreenId);
        PXViewInfo graphView = GraphHelper.GetGraphView(graphTypeByScreenId, primaryView);
        if (graphView != null)
        {
          foreach (PXActionInfo action in GraphHelper.GetActions(graphTypeByScreenId, graphView.Cache.Name))
          {
            stringList2.Add(action.Name);
            stringList1.Add(action.Name);
          }
        }
      }
    }
    PXStringListAttribute.SetList(sender, e.Row, this._FieldName, stringList2.ToArray(), stringList1.ToArray());
    base.FieldSelecting(sender, e);
  }

  private object GetValue(PXGraph graph, System.Type type)
  {
    PXCache cach = graph.Caches[type.DeclaringType];
    return cach.Current != null ? cach.GetValue(cach.Current, type.Name) : (object) null;
  }
}
