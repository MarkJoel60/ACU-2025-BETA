// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.ActionTagProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class ActionTagProcessor : PXHtmlParser.TagProcessor
{
  private static readonly ActionTagProcessor instance = new ActionTagProcessor();

  public static ActionTagProcessor Instance => ActionTagProcessor.instance;

  protected ActionTagProcessor()
  {
  }

  public PXElement Process(
    string tagName,
    string content,
    List<PXHtmlAttribute> attributes,
    WikiArticle result,
    PXWikiParserContext settings)
  {
    string name = (string) null;
    List<object> objectList = new List<object>();
    foreach (PXHtmlAttribute attribute in attributes)
    {
      if (attribute.name == "name")
        name = attribute.value;
      else
        objectList.Add((object) attribute.value);
    }
    PXGraph templateGraph = result.TypedContext.TemplateGraph;
    object currentRow = result.TypedContext.CurrentRow;
    if (name == null || templateGraph == null)
      return (PXElement) null;
    EntityHelper entityHelper = new EntityHelper(templateGraph);
    PXAction action = templateGraph.Actions[name];
    if (action == null)
      return (PXElement) null;
    PXAdapter adapter = new PXAdapter(templateGraph.Views[entityHelper.GetViewName(currentRow.GetType())]);
    if (objectList.Count > 0)
      adapter.Parameters = objectList.ToArray();
    adapter.StartRow = 0;
    adapter.MaximumRows = 1;
    adapter.TotalRequired = false;
    foreach (object obj in action.Press(adapter))
      ;
    return (PXElement) null;
  }
}
