// Decompiled with JetBrains decompiler
// Type: PX.Data.RichTextEdit.WikiRichText
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Data.RichTextEdit;

[Serializable]
public class WikiRichText : PXGraph<WikiRichText>
{
  public PXSelectJoin<WikiPage2, LeftJoin<WikiPageParent, On<WikiPage2.parentUID, Equal<WikiPageParent.pageID>>>> Pages;
  public PXSelect<WikiPage2> PagesSimple;
  public PXSelect<WikiPage2> Subarticles;

  public IEnumerable subarticles()
  {
    Guid? nullable = new Guid?();
    foreach (PXFilterRow filter in PXView.Filters)
    {
      if (filter.DataField == "ParentUID")
      {
        if (filter.Value is Guid guid)
        {
          nullable = new Guid?(guid);
          break;
        }
        if (filter.Value is string input)
        {
          Guid result;
          if (Guid.TryParse(input, out result))
          {
            nullable = new Guid?(result);
            break;
          }
          break;
        }
        break;
      }
    }
    if (nullable.HasValue)
    {
      Guid guid = nullable.Value;
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      Dictionary<Guid, List<WikiPage2>> groups = this.PagesSimple.Select().Select<PXResult<WikiPage2>, WikiPage2>(Expression.Lambda<Func<PXResult<WikiPage2>, WikiPage2>>((Expression) Expression.Call(result, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression)).Where<WikiPage2>((Expression<Func<WikiPage2, bool>>) (page => page.ParentUID.HasValue && page.PageID.HasValue)).GroupBy<WikiPage2, Guid>((Expression<Func<WikiPage2, Guid>>) (page => page.ParentUID.Value)).ToDictionary<IGrouping<Guid, WikiPage2>, Guid, List<WikiPage2>>((Func<IGrouping<Guid, WikiPage2>, Guid>) (group => group.Key), (Func<IGrouping<Guid, WikiPage2>, List<WikiPage2>>) (group => group.ToList<WikiPage2>()));
      Queue<Guid> queue = new Queue<Guid>();
      queue.Enqueue(nullable.Value);
      while (queue.Count > 0)
      {
        List<WikiPage2> wikiPage2List;
        if (groups.TryGetValue(queue.Dequeue(), out wikiPage2List))
        {
          foreach (WikiPage2 wikiPage2 in wikiPage2List)
          {
            queue.Enqueue(wikiPage2.PageID.Value);
            yield return (object) wikiPage2;
          }
        }
      }
      groups = (Dictionary<Guid, List<WikiPage2>>) null;
      queue = (Queue<Guid>) null;
    }
  }
}
