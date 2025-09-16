// Decompiled with JetBrains decompiler
// Type: PX.Api.Helpers.ViewHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Description;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Api.Helpers;

internal static class ViewHelper
{
  public static LinkedSelectorViews CollectLinkedSelectorViews(
    HashSet<string> views,
    IEnumerable<PXViewDescription> containers,
    string primaryView)
  {
    Dictionary<string, string> innerDictionary = new Dictionary<string, string>();
    foreach (KeyValuePair<string, string> keyValuePair in ViewHelper.CollectLinkedSelectorViewsImpl(views, containers))
    {
      if ((primaryView == null || !primaryView.Equals(keyValuePair.Value)) && !innerDictionary.ContainsKey(keyValuePair.Key))
        innerDictionary[keyValuePair.Key] = keyValuePair.Value;
    }
    return new LinkedSelectorViews(innerDictionary);
  }

  private static IEnumerable<KeyValuePair<string, string>> CollectLinkedSelectorViewsImpl(
    HashSet<string> views,
    IEnumerable<PXViewDescription> containers)
  {
    foreach (PXViewDescription container in containers)
    {
      FieldInfo[] fields = ((IEnumerable<FieldInfo>) container.Fields).Where<FieldInfo>((Func<FieldInfo, bool>) (fi => fi.IsSelectorField())).ToArray<FieldInfo>();
      foreach (FieldInfo fieldInfo in ((IEnumerable<FieldInfo>) fields).Where<FieldInfo>((Func<FieldInfo, bool>) (field => views.Contains(field.SelectorViewDescription.ViewName))))
        yield return new KeyValuePair<string, string>($"{container.ViewName}%{fieldInfo.FieldName}", fieldInfo.SelectorViewDescription.ViewName);
      foreach (KeyValuePair<string, string> keyValuePair in ViewHelper.CollectLinkedSelectorViewsImpl(views, ((IEnumerable<FieldInfo>) fields).Select<FieldInfo, PXViewDescription>((Func<FieldInfo, PXViewDescription>) (f => f.SelectorViewDescription))))
        yield return keyValuePair;
      fields = (FieldInfo[]) null;
    }
  }
}
