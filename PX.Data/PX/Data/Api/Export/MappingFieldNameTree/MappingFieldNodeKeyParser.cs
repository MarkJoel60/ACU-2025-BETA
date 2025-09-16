// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Export.MappingFieldNameTree.MappingFieldNodeKeyParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

#nullable disable
namespace PX.Data.Api.Export.MappingFieldNameTree;

internal class MappingFieldNodeKeyParser
{
  private readonly ImmutableHashSet<string> _views;

  internal MappingFieldNodeKeyParser(IEnumerable<string> viewsCollection)
  {
    this._views = ImmutableHashSet.ToImmutableHashSet<string>(viewsCollection);
  }

  internal (string ViewName, string CommandName) ParseNodeKey(string nodeKey, bool validateKey = true)
  {
    if (string.IsNullOrEmpty(nodeKey))
      return ((string) null, (string) null);
    string[] source = nodeKey.Split('.');
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return source.Length != 2 || ((IEnumerable<string>) source).Any<string>(MappingFieldNodeKeyParser.\u003C\u003EO.\u003C0\u003E__IsNullOrEmpty ?? (MappingFieldNodeKeyParser.\u003C\u003EO.\u003C0\u003E__IsNullOrEmpty = new Func<string, bool>(string.IsNullOrEmpty))) || validateKey && !this._views.Contains(ScreenUtils.NormalizeViewName(source[0])) ? ((string) null, (string) null) : (source[0], source[1]);
  }

  internal static bool IsViewNodeKey(string nodeKey, out string viewName)
  {
    if (!nodeKey.StartsWith("*", StringComparison.Ordinal))
    {
      viewName = (string) null;
      return false;
    }
    viewName = nodeKey.Substring("*".Length);
    return true;
  }

  internal static (RootNodeType? Type, string ViewName) GetRootNodeInfo(string rootNodeKey)
  {
    string nodeKey = rootNodeKey;
    string viewName;
    return !string.IsNullOrEmpty(nodeKey) ? (nodeKey == "<ActionsNode>" ? (new RootNodeType?(RootNodeType.Actions), (string) null) : (!MappingFieldNodeKeyParser.IsViewNodeKey(nodeKey, out viewName) ? (new RootNodeType?(), (string) null) : (new RootNodeType?(RootNodeType.View), viewName))) : (new RootNodeType?(RootNodeType.Root), (string) null);
  }
}
