// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Extensions.PXGraphClassExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common.Extensions;

public static class PXGraphClassExtensions
{
  public static void DisableCaches(this PXGraph graph)
  {
    foreach (PXCache pxCache in ((Dictionary<Type, PXCache>) graph.Caches).Values)
    {
      int num1;
      bool flag1 = (num1 = 0) != 0;
      pxCache.AllowDelete = num1 != 0;
      int num2;
      bool flag2 = (num2 = flag1 ? 1 : 0) != 0;
      pxCache.AllowUpdate = num2 != 0;
      pxCache.AllowInsert = flag2;
    }
  }

  internal static void EnableCaches(this PXGraph graph)
  {
    foreach (PXCache pxCache in ((Dictionary<Type, PXCache>) graph.Caches).Values)
    {
      int num1;
      bool flag1 = (num1 = 1) != 0;
      pxCache.AllowDelete = num1 != 0;
      int num2;
      bool flag2 = (num2 = flag1 ? 1 : 0) != 0;
      pxCache.AllowUpdate = num2 != 0;
      pxCache.AllowInsert = flag2;
    }
  }

  internal static Dictionary<Type, CachePermission> SaveCachesPermissions(
    this PXGraph graph,
    bool saveEnabledOnly = false)
  {
    Dictionary<Type, CachePermission> dictionary = new Dictionary<Type, CachePermission>();
    foreach (PXCache pxCache in ((Dictionary<Type, PXCache>) graph.Caches).Values)
    {
      if (!saveEnabledOnly || pxCache.AllowInsert || pxCache.AllowUpdate || pxCache.AllowDelete)
      {
        Type type = pxCache.GetType();
        if (!dictionary.ContainsKey(type))
          dictionary.Add(type, new CachePermission()
          {
            AllowSelect = pxCache.AllowSelect,
            AllowInsert = pxCache.AllowInsert,
            AllowUpdate = pxCache.AllowUpdate,
            AllowDelete = pxCache.AllowDelete
          });
      }
    }
    return dictionary;
  }

  internal static void LoadCachesPermissions(
    this PXGraph graph,
    Dictionary<Type, CachePermission> allows)
  {
    foreach (PXCache pxCache in ((Dictionary<Type, PXCache>) graph.Caches).Values)
    {
      CachePermission cachePermission;
      if (allows.TryGetValue(pxCache.GetType(), out cachePermission))
      {
        pxCache.AllowSelect = cachePermission.AllowSelect;
        pxCache.AllowInsert = cachePermission.AllowInsert;
        pxCache.AllowUpdate = cachePermission.AllowUpdate;
        pxCache.AllowDelete = cachePermission.AllowDelete;
      }
    }
  }

  internal static TService GetService<TService>(this PXGraph graph)
  {
    return ((Func<PXGraph, TService>) ((IServiceProvider) ServiceLocator.Current).GetService(typeof (Func<PXGraph, TService>)))(graph);
  }

  public static PXGraphClassExtensions.DescriptionMaker GetLocalizedDescriptionMaker(
    this string formatTemplate)
  {
    return (PXGraphClassExtensions.DescriptionMaker) (originalDescription => PXMessages.LocalizeFormatNoPrefix(formatTemplate, new object[1]
    {
      (object) originalDescription
    }));
  }

  public static PXGraphClassExtensions.DescriptionMaker GetDescriptionMaker(
    this string formatTemplate)
  {
    return (PXGraphClassExtensions.DescriptionMaker) (originalDescription => string.Format(formatTemplate, (object) originalDescription));
  }

  public static string MakeDescription<TField>(
    this PXGraph graph,
    string originalDescription,
    PXGraphClassExtensions.DescriptionMaker maker)
    where TField : IBqlField
  {
    string str = maker(originalDescription);
    int maxDescriptionLength = 0;
    PXCacheEx.Adjust<PXDBStringAttribute>(graph.Caches[BqlCommand.GetItemType<TField>()], (object) null).For<TField>((Action<PXDBStringAttribute>) (attribute => maxDescriptionLength = attribute != null ? attribute.Length : -1));
    if (maxDescriptionLength == -1)
      return str;
    int num = str.Length - maxDescriptionLength;
    if (num > 0)
    {
      if (num > originalDescription.Length)
        throw new PXException("A description of maximum available length ({0}) cannot be correctly generated for the {1} field. The original description is \"{2}\". The extra long generated description is \"{3}\".", new object[4]
        {
          (object) maxDescriptionLength,
          (object) typeof (TField).FullName,
          (object) originalDescription,
          (object) str
        });
      str = maker(originalDescription.Substring(0, originalDescription.Length - num));
    }
    return str;
  }

  public static string MakeDescription<TField>(
    this PXGraph graph,
    string formatTemplate,
    string originalDescription)
    where TField : IBqlField
  {
    return graph.MakeDescription<TField>(originalDescription, formatTemplate.GetDescriptionMaker());
  }

  public static string MakeLocalizedDescription<TField>(
    this PXGraph graph,
    string formatTemplate,
    string originalDescription)
    where TField : IBqlField
  {
    return graph.MakeDescription<TField>(originalDescription, formatTemplate.GetLocalizedDescriptionMaker());
  }

  public static TTable LiteUpdate<TTable>(
    this PXGraph graph,
    TTable row,
    Action<PXCache, TTable> update,
    bool skipRowUpdatedEvent = false)
    where TTable : class, IBqlTable, new()
  {
    return ((PXCache) GraphHelper.Caches<TTable>(graph)).LiteUpdate<TTable>(row, update, skipRowUpdatedEvent);
  }

  public static TTable LiteUpdate<TTable>(
    this PXGraph graph,
    TTable row,
    Action<ValueSetter<TTable>> update,
    bool skipRowUpdatedEvent = false)
    where TTable : class, IBqlTable, new()
  {
    return ((PXCache) GraphHelper.Caches<TTable>(graph)).LiteUpdate<TTable>(row, update, skipRowUpdatedEvent);
  }

  public static TTable LiteUpdate<TTable>(
    this PXGraph graph,
    TTable row,
    Action<TTable> update,
    bool skipRowUpdatedEvent = false)
    where TTable : class, IBqlTable, new()
  {
    return ((PXCache) GraphHelper.Caches<TTable>(graph)).LiteUpdate<TTable>(row, update, skipRowUpdatedEvent);
  }

  public static bool TryGetScreenIdFor<T>(this PXGraph graph, out string screenID) where T : PXGraph
  {
    PXSiteMapNode siteMapNode = PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, typeof (T));
    screenID = siteMapNode?.ScreenID;
    return !string.IsNullOrEmpty(screenID);
  }

  public delegate string DescriptionMaker(string originalDescription);
}
