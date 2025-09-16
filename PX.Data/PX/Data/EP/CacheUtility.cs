// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.CacheUtility
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

#nullable disable
namespace PX.Data.EP;

public static class CacheUtility
{
  public static string GetErrorDescription(string error)
  {
    return error != null && !((error = error.Trim()) == string.Empty) ? $"<b><font color=\"Red\" >{error}</font></b><br />" : string.Empty;
  }

  public static string GetDescription(EntityHelper helper, object entity, System.Type entityType)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<b>");
    stringBuilder.Append(HttpUtility.HtmlEncode(EntityHelper.GetFriendlyEntityName(entityType)));
    stringBuilder.Append("</b>");
    bool flag = true;
    foreach (KeyValuePair<string, string> fieldValuePair in helper.GetFieldValuePairs(entity, entityType))
    {
      if (flag)
      {
        stringBuilder.Append("<table width=\"100%\" cellspacing=\"0\">");
        flag = false;
      }
      stringBuilder.AppendFormat("<tr><td style=\"vertical-align: top;\">{0}:</td><td style=\"vertical-align: top;\">{1}</td></tr>", (object) HttpUtility.HtmlEncode(fieldValuePair.Key), (object) HttpUtility.HtmlEncode(fieldValuePair.Value));
    }
    if (!flag)
      stringBuilder.Append("</table>");
    return stringBuilder.ToString();
  }

  public static IEnumerable<TNode> Extract<TNode>(this PXResultset<TNode> set) where TNode : class, IBqlTable, new()
  {
    foreach (PXResult<TNode> pxResult in set)
      yield return (TNode) pxResult;
  }

  public static void SynchronizeByItemType(this PXGraph graph, PXCache cache)
  {
    if (graph == null)
      throw new ArgumentNullException(nameof (graph));
    System.Type objB = cache != null ? cache.GetItemType() : throw new ArgumentNullException(nameof (cache));
    List<System.Type> typeList = new List<System.Type>();
    foreach (KeyValuePair<System.Type, PXCache> cach in (Dictionary<System.Type, PXCache>) graph.Caches)
    {
      if (cach.Value != cache && object.Equals((object) cach.Value.GetItemType(), (object) objB))
        typeList.Add(cach.Key);
    }
    foreach (System.Type key in typeList)
      graph.Caches[key] = cache;
    PXView[] array = new PXView[graph.Views.Values.Count];
    graph.Views.Values.CopyTo(array, 0);
    foreach (PXView pxView in array)
    {
      if (object.Equals((object) pxView.GetItemType(), (object) objB))
        pxView._Cache = (PXCache) null;
    }
  }
}
