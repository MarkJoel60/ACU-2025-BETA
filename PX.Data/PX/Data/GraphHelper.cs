// Decompiled with JetBrains decompiler
// Type: PX.Data.GraphHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Api;
using PX.Api.Soap.Screen;
using PX.Common;
using PX.Data.Automation;
using PX.Data.Automation.State;
using PX.Data.DacDescriptorGeneration;
using PX.Metadata;
using PX.SM;
using PX.Web.UI;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

#nullable enable
namespace PX.Data;

public static class GraphHelper
{
  private static readonly ConcurrentDictionary<string, System.Type> _typesCache = new ConcurrentDictionary<string, System.Type>();
  public const string IsRedirectFlagQueryParams = "isRedirect";
  public const string RedirectTargetScreenIDParams = "RedirectTargetScreenID";

  static GraphHelper()
  {
    PXCodeDirectoryCompiler.NotifyOnChange(new System.Action(GraphHelper.ResetTypesCache));
  }

  private static void ResetTypesCache() => GraphHelper._typesCache.Clear();

  public static System.Type? GetType(string? typename)
  {
    if (Str.IsNullOrEmpty(typename))
      return (System.Type) null;
    if (CustomizedTypeManager.IsCustomizedType(typename))
      typename = CustomizedTypeManager.GetTypeNotCustomized(typename);
    return ConcurrentDictionaryExtensions.GetOrAddOrUpdate<string, System.Type>(GraphHelper._typesCache, typename, (Func<string, System.Type>) (name => GraphHelper.GetTypeInternal(name)), (Func<string, System.Type, System.Type>) ((name, existing) =>
    {
      System.Type type = existing;
      return (object) type != null ? type : GraphHelper.GetTypeInternal(name);
    }));
  }

  private static System.Type GetTypeInternal(string typename)
  {
    System.Type type1 = PXBuildManager.GetType(typename, false);
    if ((object) type1 == null)
      type1 = System.Type.GetType(typename);
    System.Type typeInternal = type1;
    if (typeInternal == (System.Type) null)
      return (System.Type) null;
    if (typeof (PXGraph).IsAssignableFrom(typeInternal) && !CustomizedTypeManager.IsCustomizedType(typeInternal))
    {
      string customizedTypeFullName = CustomizedTypeManager.GetCustomizedTypeFullName(typeInternal);
      if (customizedTypeFullName != null && customizedTypeFullName != typeInternal.FullName)
      {
        System.Type type2 = PXBuildManager.GetType(customizedTypeFullName, false);
        if ((object) type2 == null)
          type2 = System.Type.GetType(customizedTypeFullName, false) ?? typeInternal;
        typeInternal = type2;
      }
    }
    return typeInternal;
  }

  [PXInternalUseOnly]
  public static List<Graph> GetModules() => GraphHelper.GetModules(false);

  [PXInternalUseOnly]
  public static List<Graph> GetModules(bool skipReport)
  {
    List<Graph> modules = new List<Graph>();
    foreach (Graph graph in GraphHelper.GetGraphAll(skipReport))
    {
      bool? nullable = graph.IsNamespace;
      bool flag1 = true;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        if (skipReport)
        {
          nullable = graph.IsReport;
          bool flag2 = true;
          if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
            continue;
        }
        modules.Add(graph);
      }
    }
    return modules;
  }

  [PXInternalUseOnly]
  public static List<Graph> GetGraphAll() => GraphHelper.GetGraphAll(false);

  [PXInternalUseOnly]
  public static List<Graph> GetGraphAll(bool skipReport)
  {
    List<Graph> source = new List<Graph>();
    List<System.Type> typeList = new List<System.Type>();
    foreach (System.Type allHiddenGraph in ServiceManager.AllHiddenGraphs)
    {
      if (!typeList.Contains(allHiddenGraph))
        typeList.Add(allHiddenGraph);
    }
    foreach (Graph graph in ServiceManager.AllGraphsNotCustomized)
    {
      System.Type graphTypeByFullName = ServiceManager.GetGraphTypeByFullName(graph.GraphName);
      if (!(graphTypeByFullName == (System.Type) null) && !graphTypeByFullName.IsDefined(typeof (PXHiddenAttribute), false) && !typeList.Contains(graphTypeByFullName))
        source.Add(new Graph()
        {
          GraphName = graph.GraphName,
          Text = graph.Text,
          IsNamespace = new bool?(false)
        });
    }
    if (!skipReport)
    {
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PX.SM.SiteMap>(new PXDataField("ScreenID"), new PXDataField("Title"), (PXDataField) new PXDataFieldValue("ScreenID", PXDbType.VarChar, new int?(8), (object) null, PXComp.ISNOTNULL), (PXDataField) new PXDataFieldValue("Title", PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) null, PXComp.ISNOTNULL), (PXDataField) new PXDataFieldValue("Url", PXDbType.VarChar, new int?(512 /*0x0200*/), (object) "%.rpx", PXComp.LIKE)))
        source.Add(new Graph()
        {
          GraphName = pxDataRecord.GetString(0),
          Text = pxDataRecord.GetString(1),
          IsNamespace = new bool?(false),
          IsReport = new bool?(true)
        });
    }
    return GraphHelper.FillNamespaces(source);
  }

  internal static List<Graph> FillNamespaces(List<Graph> source)
  {
    source.Sort((Comparison<Graph>) ((g1, g2) => string.Compare(g1.GraphName, g2.GraphName, StringComparison.OrdinalIgnoreCase)));
    List<Graph> graphList = new List<Graph>();
    string str1 = "null";
    string str2 = "";
    for (int index1 = 0; index1 < source.Count; ++index1)
    {
      string[] strArray = source[index1].GraphName.Split(new char[1]
      {
        '.'
      }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length >= 2)
      {
        int num = source[index1].GraphName.Contains(".Reports.") ? 3 : 2;
        for (int index2 = strArray.Length - num; index2 < strArray.Length - num + 1; ++index2)
        {
          string str3 = string.Join(".", strArray, 0, index2 + 1);
          if (!str2.StartsWith(str3))
          {
            Graph graph = new Graph();
            graph.GraphName = str3;
            graph.IsNamespace = new bool?(true);
            graph.Icon = Sprite.Tree.GetFullUrl("Folder");
            str1 = strArray[index2];
            graphList.Add(graph);
            graphList.Add(new Graph()
            {
              GraphName = str3 + ".Reports",
              IsNamespace = new bool?(true),
              Icon = Sprite.Tree.GetFullUrl("Folder"),
              Text = graph.Text + ".Reports",
              IsReport = new bool?(true)
            });
          }
        }
        str2 = source[index1].GraphName;
        source[index1].Text = $"{str1}.{source[index1].Text}";
        graphList.Add(source[index1]);
      }
    }
    graphList.Sort((Comparison<Graph>) ((g1, g2) => string.Compare(g1.Text, g2.Text, StringComparison.OrdinalIgnoreCase)));
    return graphList;
  }

  [PXInternalUseOnly]
  public static PXViewInfo? GetGraphView(string? graphName, string? view)
  {
    return GraphHelper.GetGraphView(graphName, view, false);
  }

  [PXInternalUseOnly]
  internal static PXViewInfo? GetGraphView(string? graphName, string? view, bool showHidden)
  {
    return GraphHelper.GetGraphView(GraphHelper.GetType(graphName), view, showHidden);
  }

  [PXInternalUseOnly]
  public static PXViewInfo? GetGraphView(System.Type? type, string? view)
  {
    return GraphHelper.GetGraphView(type, view, false);
  }

  [PXInternalUseOnly]
  internal static PXViewInfo? GetGraphView(System.Type? type, string? view, bool showHidden)
  {
    PXViewInfo graphView = (PXViewInfo) null;
    if (!Str.IsNullOrEmpty(view) && type != (System.Type) null)
    {
      List<System.Type> source1 = (List<System.Type>) null;
      if (typeof (PXGraph).IsAssignableFrom(type))
        source1 = new List<System.Type>() { type };
      else if (typeof (PXGraphExtension).IsAssignableFrom(type))
      {
        System.Type[] source2 = new System.Type[10]
        {
          typeof (PXGraphExtension<>),
          typeof (PXGraphExtension<,>),
          typeof (PXGraphExtension<,,>),
          typeof (PXGraphExtension<,,,>),
          typeof (PXGraphExtension<,,,,>),
          typeof (PXGraphExtension<,,,,,>),
          typeof (PXGraphExtension<,,,,,,>),
          typeof (PXGraphExtension<,,,,,,,>),
          typeof (PXGraphExtension<,,,,,,,,>),
          typeof (PXGraphExtension<,,,,,,,,,>)
        };
        System.Type type1 = type;
        bool flag = false;
        while (!flag && type1 != typeof (object))
        {
          if (type1.IsGenericType && ((IEnumerable<System.Type>) source2).Contains<System.Type>(type1.GetGenericTypeDefinition()))
            flag = true;
          else
            type1 = type1.BaseType;
        }
        if (flag)
        {
          source1 = new List<System.Type>() { type };
          IEnumerable<System.Type> collection = ((IEnumerable<System.Type>) type1.GetGenericArguments()).Where<System.Type>((Func<System.Type, bool>) (a => typeof (PXGraph).IsAssignableFrom(a) || typeof (PXGraphExtension).IsAssignableFrom(a)));
          source1.AddRange(collection);
        }
      }
      if (source1 != null)
        graphView = source1.Select<System.Type, PXViewInfo>((Func<System.Type, PXViewInfo>) (c => GraphHelper.GetViewFromType(c, view, showHidden))).Where<PXViewInfo>((Func<PXViewInfo, bool>) (info => info != null)).FirstOrDefault<PXViewInfo>();
    }
    return graphView;
  }

  private static PXViewInfo? GetViewFromType(System.Type type, string view, bool showHidden)
  {
    PXViewInfo viewFromType = (PXViewInfo) null;
    System.Reflection.FieldInfo field = type.GetField(view);
    if (field != (System.Reflection.FieldInfo) null && field.FieldType.IsSubclassOf(typeof (PXSelectBase)))
    {
      string displayName = (string) null;
      if (field.IsDefined(typeof (PXViewNameAttribute), true))
        displayName = ((PXNameAttribute) field.GetCustomAttributes(typeof (PXViewNameAttribute), true)[0]).Name;
      System.Type type1 = field.FieldType;
      BqlCommand viewCommand = GraphHelper.GetViewCommand(field);
      PXCacheInfo cache = (PXCacheInfo) null;
      for (; type1 != typeof (object); type1 = type1.BaseType)
      {
        if (type1.IsGenericType && type1.GetGenericTypeDefinition() == typeof (PXSelectBase<>))
        {
          System.Type genericArgument = type1.GetGenericArguments()[0];
          if (!(genericArgument == typeof (AccessInfo)) && (showHidden || !genericArgument.IsDefined(typeof (PXHiddenAttribute), false)) || !(genericArgument == typeof (AccessInfo)) && !genericArgument.IsDefined(typeof (PXHiddenAttribute), false))
            cache = new PXCacheInfo(genericArgument);
          else
            break;
        }
      }
      if (cache != null)
        viewFromType = new PXViewInfo(field.Name, displayName, cache, viewCommand);
    }
    return viewFromType;
  }

  [PXInternalUseOnly]
  public static List<PXViewInfo> GetGraphViews(string? graphName, bool isNamedOnly)
  {
    return GraphHelper.GetGraphViews(GraphHelper.GetType(graphName), isNamedOnly, false);
  }

  [PXInternalUseOnly]
  public static List<PXViewInfo> GetGraphViews(string? graphName, bool isNamedOnly, bool showHidden)
  {
    return GraphHelper.GetGraphViews(GraphHelper.GetType(graphName), isNamedOnly, showHidden);
  }

  [PXInternalUseOnly]
  public static List<PXViewInfo> GetGraphViews(System.Type type, bool isNamedOnly)
  {
    return GraphHelper.GetGraphViews(type, isNamedOnly, false);
  }

  [PXInternalUseOnly]
  public static List<PXViewInfo> GetGraphViews(System.Type? type, bool isNamedOnly, bool showHidden)
  {
    List<PXViewInfo> graphViews = new List<PXViewInfo>();
    if (type == (System.Type) null || !typeof (PXGraph).IsAssignableFrom(type))
      return graphViews;
    List<System.Reflection.FieldInfo> fieldInfoList = new List<System.Reflection.FieldInfo>((IEnumerable<System.Reflection.FieldInfo>) type.GetFields());
    foreach (System.Type extension in PXGraph._GetExtensions(type))
      fieldInfoList.AddRange((IEnumerable<System.Reflection.FieldInfo>) extension.GetFields());
    foreach (System.Reflection.FieldInfo field in fieldInfoList)
    {
      if (field.FieldType.IsSubclassOf(typeof (PXSelectBase)))
      {
        string displayName = (string) null;
        if (field.IsDefined(typeof (PXViewNameAttribute), true))
          displayName = ((PXNameAttribute) field.GetCustomAttributes(typeof (PXViewNameAttribute), true)[0]).Name;
        else if (isNamedOnly)
          continue;
        PXCacheInfo viewCache = GraphHelper.GetViewCache(field, showHidden);
        BqlCommand viewCommand = GraphHelper.GetViewCommand(field);
        if (viewCache != null)
          graphViews.Add(new PXViewInfo(field.Name, displayName, viewCache, viewCommand));
      }
    }
    if (isNamedOnly && graphViews.Count == 0)
    {
      PXSiteMapNode siteMapNode = PXSiteMap.Provider.FindSiteMapNode(type);
      if (siteMapNode != null)
      {
        foreach (KeyValuePair<string, string[]> view in ScreenUtils.ScreenInfo.TryGet(siteMapNode.ScreenID).Views)
        {
          PXCacheInfo viewCache = GraphHelper.GetViewCache(type.GetField(view.Key), showHidden);
          if (viewCache != null)
            graphViews.Add(new PXViewInfo(view.Key, view.Key, viewCache));
        }
      }
    }
    return graphViews;
  }

  private static BqlCommand? GetViewCommand(System.Reflection.FieldInfo field)
  {
    System.Type type1 = field.FieldType;
    if (!type1.IsGenericType)
      return (BqlCommand) null;
    System.Type type2 = (System.Type) null;
    for (; type1 != typeof (object); type1 = type1.BaseType)
    {
      type2 = type1.GetNestedType("Config", BindingFlags.Instance | BindingFlags.Public);
      if (type2 != (System.Type) null)
      {
        type2 = type2.MakeGenericType(type1.GetGenericArguments());
        break;
      }
      if (type1.IsGenericType && type1.GetGenericTypeDefinition() == typeof (PXSelectBase<>))
        return Activator.CreateInstance(typeof (Select<>).MakeGenericType(type1.GetGenericArguments())) as BqlCommand;
    }
    return type2 == (System.Type) null ? (BqlCommand) null : ((IViewConfigBase) Activator.CreateInstance(type2)).GetCommand();
  }

  private static PXCacheInfo? GetViewCache(System.Reflection.FieldInfo? field)
  {
    return GraphHelper.GetViewCache(field, false);
  }

  private static PXCacheInfo? GetViewCache(System.Reflection.FieldInfo? field, bool showHidden)
  {
    if (field == (System.Reflection.FieldInfo) null)
      return (PXCacheInfo) null;
    System.Type type1 = field.FieldType;
    if (field.IsDefined(typeof (PXHiddenAttribute), false) && !showHidden)
      return (PXCacheInfo) null;
    for (; type1 != typeof (object) && type1 != (System.Type) null; type1 = type1.BaseType)
    {
      System.Type type2 = !type1.IsGenericType || !(type1.GetGenericTypeDefinition() == typeof (PXSelectBase<>)) ? type1.GetInterface(typeof (ICacheType<>).Name) : type1;
      if (type2 != (System.Type) null)
      {
        System.Type genericArgument = type2.GetGenericArguments()[0];
        if (!(genericArgument == typeof (AccessInfo)) && (!genericArgument.IsDefined(typeof (PXHiddenAttribute), false) || showHidden))
          return new PXCacheInfo(genericArgument);
        break;
      }
    }
    return (PXCacheInfo) null;
  }

  [PXInternalUseOnly]
  public static PXCacheInfo? GetPrimaryCache(string graphName)
  {
    string name = PXPageIndexingService.GetPrimaryView(graphName) ?? PXPageIndexingService.GetPrimaryView(CustomizedTypeManager.GetTypeNotCustomized(graphName));
    if (!Str.IsNullOrEmpty(name))
    {
      System.Type type = GraphHelper.GetType(graphName);
      if (type != (System.Type) null)
      {
        System.Reflection.FieldInfo field1 = type.GetField(name);
        if (field1 != (System.Reflection.FieldInfo) null)
          return GraphHelper.GetViewCache(field1);
        foreach (System.Type extension in PXExtensionManager.GetExtensions(type, false))
        {
          System.Reflection.FieldInfo field2 = extension.GetField(name);
          if (field2 != (System.Reflection.FieldInfo) null)
            return GraphHelper.GetViewCache(field2);
        }
      }
    }
    return (PXCacheInfo) null;
  }

  [PXInternalUseOnly]
  public static List<PXCacheInfo> GetGraphCaches(string? graphName)
  {
    return GraphHelper.GetGraphCaches(GraphHelper.GetType(graphName));
  }

  [PXInternalUseOnly]
  public static List<PXCacheInfo> GetGraphCaches(System.Type? type)
  {
    List<PXCacheInfo> source1 = new List<PXCacheInfo>();
    if (type != (System.Type) null && typeof (PXGraph).IsAssignableFrom(type))
    {
      string[] source2 = PXPageIndexingService.GetDataMembers(type.FullName) ?? PXPageIndexingService.GetDataMembers(CustomizedTypeManager.GetTypeNotCustomized(type.FullName));
      List<System.Reflection.FieldInfo> fieldInfoList = new List<System.Reflection.FieldInfo>((IEnumerable<System.Reflection.FieldInfo>) type.GetFields());
      foreach (System.Type extension in PXGraph._GetExtensions(type))
        fieldInfoList.AddRange((IEnumerable<System.Reflection.FieldInfo>) extension.GetFields());
      Dictionary<System.Type, string> source3 = new Dictionary<System.Type, string>();
      foreach (System.Reflection.FieldInfo fieldInfo in fieldInfoList)
      {
        int num = source2 == null ? 0 : (!((IEnumerable<string>) source2).Contains<string>(fieldInfo.Name, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase) ? 1 : 0);
        bool flag1 = fieldInfo.IsDefined(typeof (PXHiddenAttribute), false);
        bool flag2 = fieldInfo.FieldType.IsSubclassOf(typeof (PXSelectBase));
        bool flag3 = fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof (PXSelectExtension<>);
        if (((num != 0 ? 0 : (!flag1 ? 1 : 0)) & (flag2 ? 1 : 0)) != 0 && !flag3)
        {
          System.Type fieldType = fieldInfo.FieldType;
          foreach (System.Type type1 in GraphHelper.GetCacheTypesFromMember(type, fieldType))
          {
            string name;
            if (type1.IsDefined(typeof (PXCacheNameAttribute), true))
              name = ((PXNameAttribute) type1.GetCustomAttributes(typeof (PXCacheNameAttribute), true)[0]).GetName();
            else if (type1.IsDefined(typeof (PXProjectionAttribute), true))
              name = PXCache.GetBqlTable(BqlCommand.CreateInstance(((PXProjectionAttribute) type1.GetCustomAttributes(typeof (PXProjectionAttribute), true)[0]).Select).GetTables()[0]).Name;
            else
              name = PXCache.GetBqlTable(type1).Name;
            source3[type1] = name;
          }
        }
      }
      source1.AddRange(source3.Select<KeyValuePair<System.Type, string>, PXCacheInfo>((Func<KeyValuePair<System.Type, string>, PXCacheInfo>) (p => new PXCacheInfo(p.Key, p.Value))));
    }
    return source1.OrderBy<PXCacheInfo, string>((Func<PXCacheInfo, string>) (c => c.DisplayName)).ToList<PXCacheInfo>();
  }

  private static bool IsExceptionalType(System.Type type)
  {
    return type.FullName.StartsWith("PX.Objects.CR.CRActivityListBase`") && ((IEnumerable<System.Type>) type.GetGenericArguments()).Count<System.Type>() == 2;
  }

  private static IEnumerable<System.Type> GetCacheTypesFromMember(System.Type graphType, System.Type memberType)
  {
    List<System.Type> typeList = new List<System.Type>();
    for (; memberType != typeof (object); memberType = memberType.BaseType)
    {
      if (memberType.IsGenericType)
      {
        if (GraphHelper.IsExceptionalType(memberType))
          typeList.Add(memberType.GetGenericArguments()[1]);
        else if (memberType.GetGenericTypeDefinition() == typeof (PXSelectBase<>))
        {
          typeList.Add(memberType.GetGenericArguments()[0]);
        }
        else
        {
          for (System.Type type1 = memberType; type1 != (System.Type) null; type1 = type1.BaseType)
          {
            using (IEnumerator<System.Type> enumerator = ((IEnumerable<System.Type>) type1.GetGenericArguments()).Where<System.Type>((Func<System.Type, bool>) (a => a.IsGenericType)).GetEnumerator())
            {
label_17:
              while (enumerator.MoveNext())
              {
                System.Type type2 = enumerator.Current;
                while (true)
                {
                  if (type2 != (System.Type) null && type2 != typeof (BqlNone))
                  {
                    if (type2.IsGenericType && type2.GetGenericTypeDefinition() == typeof (JoinBase<,,>))
                    {
                      typeList.Add(type2.GetGenericArguments()[0]);
                      type2 = type2.GetGenericArguments()[2];
                    }
                    else
                      type2 = type2.BaseType;
                  }
                  else
                    goto label_17;
                }
              }
            }
          }
        }
      }
    }
    foreach (System.Type t in typeList)
    {
      if (!(t == typeof (AccessInfo)) && !t.IsDefined(typeof (PXHiddenAttribute), false))
        yield return PXSubstManager.Substitute(t, graphType);
    }
  }

  [PXInternalUseOnly]
  public static List<PXActionInfo> GetActions(string graphType, string cacheType)
  {
    return GraphHelper.GetActions(GraphHelper.GetType(graphType), GraphHelper.GetType(cacheType));
  }

  [PXInternalUseOnly]
  public static List<PXActionInfo> GetActions(System.Type? graphType, System.Type? cacheType)
  {
    List<PXActionInfo> source = new List<PXActionInfo>();
    if (graphType != (System.Type) null)
    {
      List<System.Reflection.FieldInfo> fieldInfoList = new List<System.Reflection.FieldInfo>((IEnumerable<System.Reflection.FieldInfo>) graphType.GetFields());
      foreach (System.Type extension in PXGraph._GetExtensions(graphType))
        fieldInfoList.AddRange((IEnumerable<System.Reflection.FieldInfo>) extension.GetFields());
      foreach (System.Reflection.FieldInfo fieldInfo in fieldInfoList)
      {
        if (IsActionOfType(fieldInfo.FieldType, cacheType))
        {
          source.Add(new PXActionInfo(fieldInfo));
        }
        else
        {
          System.Type type1;
          for (type1 = fieldInfo.FieldType; type1 != (System.Type) null && type1 != typeof (object) && (!type1.IsGenericType || type1.GetGenericTypeDefinition() != typeof (PXProcessing<>)); type1 = type1.BaseType)
          {
            if (type1.IsGenericType && type1.GetGenericTypeDefinition() == typeof (PXFilteredProcessing<,>) && type1.GetGenericArguments().Length > 1 && type1.GetGenericArguments()[1] != cacheType && PXSubstManager.Substitute(type1.GetGenericArguments()[1], graphType) != cacheType)
            {
              type1 = (System.Type) null;
              break;
            }
          }
          if (type1 != (System.Type) null && type1.IsGenericType && type1.GetGenericTypeDefinition() == typeof (PXProcessing<>))
          {
            source.Add(new PXActionInfo("Process", PXMessages.LocalizeNoPrefix("Process"))
            {
              Icon = Sprite.Main.GetFullUrl("Release")
            });
            source.Add(new PXActionInfo("ProcessAll", PXMessages.LocalizeNoPrefix("Process All"))
            {
              Icon = Sprite.Main.GetFullUrl("Release")
            });
            string display = PXMessages.LocalizeNoPrefix("");
            source.Add(new PXActionInfo("Schedule", display)
            {
              Icon = Sprite.Main.GetFullUrl("Shedule")
            });
            source.Add(new PXActionInfo("_ScheduleAdd_", $"{display} - {PXMessages.LocalizeNoPrefix("Add")}")
            {
              Icon = Sprite.Main.GetFullUrl("AddNew")
            });
            source.Add(new PXActionInfo("_ScheduleView_", $"{display} - {PXMessages.LocalizeNoPrefix("View")}")
            {
              Icon = Sprite.Main.GetFullUrl("DataEntry")
            });
            source.Add(new PXActionInfo("_ScheduleHistory_", $"{display} - {PXMessages.LocalizeNoPrefix("History")}")
            {
              Icon = Sprite.Main.GetFullUrl("Inquiry")
            });
          }
          if (typeof (PXSelectBase).IsAssignableFrom(fieldInfo.FieldType))
          {
            if (Attribute.IsDefined((MemberInfo) fieldInfo, typeof (PXImportAttribute)))
            {
              PXActionInfo pxActionInfo = new PXActionInfo(fieldInfo.Name + "$Upload", PXMessages.LocalizeNoPrefix("Upload"))
              {
                Icon = Sprite.Main.GetFullUrl("Process")
              };
              source.Add(pxActionInfo);
            }
            System.Type type2 = fieldInfo.FieldType;
            while (type2 != (System.Type) null && (!type2.IsGenericType || type2.GetGenericArguments()[0] != cacheType && PXSubstManager.Substitute(type2.GetGenericArguments()[0], graphType) != cacheType))
              type2 = type2.BaseType;
            if (type2 != (System.Type) null)
            {
              PXActionInfo pxActionInfo = new PXActionInfo(fieldInfo.Name + "$ExportExcel", PXMessages.LocalizeNoPrefix("Export"))
              {
                Icon = Sprite.Main.GetFullUrl("Excel")
              };
              source.Add(pxActionInfo);
              foreach (System.Reflection.FieldInfo field in fieldInfo.FieldType.GetFields())
              {
                System.Reflection.FieldInfo subField = field;
                if (subField.FieldType.IsSubclassOf(typeof (PXAction)) && subField.FieldType.IsGenericType && (subField.FieldType.GetGenericArguments()[0] == cacheType || PXSubstManager.Substitute(subField.FieldType.GetGenericArguments()[0], graphType) == cacheType) && !source.Any<PXActionInfo>((Func<PXActionInfo, bool>) (a => a.Name == subField.Name)))
                  source.Add(new PXActionInfo(subField));
              }
            }
            if (fieldInfo.FieldType.IsDefined(typeof (PXDynamicButtonAttribute), true))
            {
              foreach (object customAttribute in fieldInfo.FieldType.GetCustomAttributes(typeof (PXDynamicButtonAttribute), true))
              {
                List<PXActionInfo> dynamicActions = customAttribute is PXDynamicButtonAttribute dynamicButtonAttribute ? dynamicButtonAttribute.GetDynamicActions(graphType, fieldInfo.FieldType) : (List<PXActionInfo>) null;
                if (dynamicActions != null)
                  source.AddRange((IEnumerable<PXActionInfo>) dynamicActions);
              }
            }
          }
        }
      }
      if (ServiceLocator.IsLocationProviderSet)
      {
        PXWorkflowService instance = ServiceLocator.Current.GetInstance<PXWorkflowService>();
        source.AddRange(((IEnumerable<ScreenActionBase>) instance.GetAutomationActions(graphType.FullName, ExceptionExtensions.CheckIfNull<System.Type>(cacheType, nameof (cacheType), (string) null).FullName)).Union<ScreenActionBase>((IEnumerable<ScreenActionBase>) instance.GetExtraActions(graphType.FullName)).Select<ScreenActionBase, PXActionInfo>((Func<ScreenActionBase, PXActionInfo>) (action => new PXActionInfo(action.ActionId, action.ActionName))));
      }
    }
    return source;

    bool IsActionOfType(System.Type actionType, System.Type? dacType)
    {
      if (actionType.IsSubclassOf(typeof (PXAction)))
      {
        for (; actionType != typeof (PXAction) && actionType != (System.Type) null; actionType = actionType.BaseType)
        {
          if (actionType.IsGenericType && (actionType.GetGenericArguments()[0] == dacType || PXSubstManager.Substitute(actionType.GetGenericArguments()[0], graphType) == dacType))
            return true;
        }
      }
      return false;
    }
  }

  [PXInternalUseOnly]
  public static List<PXActionInfo> GetActionsFiltered(System.Type graphType, System.Type type)
  {
    List<PXActionInfo> source = new List<PXActionInfo>();
    List<PXActionInfo> pxActionInfoList = new List<PXActionInfo>();
    foreach (PXActionInfo action1 in GraphHelper.GetActions(graphType, type))
    {
      PXActionInfo action = action1;
      PXActionInfo pxActionInfo = source.Find((Predicate<PXActionInfo>) (actionCollectionItem => actionCollectionItem.DisplayName == action.DisplayName));
      if (pxActionInfo != null)
      {
        if (!pxActionInfoList.Contains(pxActionInfo))
          pxActionInfoList.Add(pxActionInfo);
        action.DisplayName = $"{action.DisplayName} ({action.Name})";
      }
      source.Add(action);
    }
    foreach (PXActionInfo pxActionInfo in pxActionInfoList)
      pxActionInfo.DisplayName = $"{pxActionInfo.DisplayName} ({pxActionInfo.Name})";
    return source.OrderBy<PXActionInfo, string>((Func<PXActionInfo, string>) (a => !string.IsNullOrEmpty(a.DisplayName) ? a.DisplayName : a.Name)).ToList<PXActionInfo>();
  }

  [PXInternalUseOnly]
  public static List<PXFieldInfo> GetCacheFields(PXCache cache)
  {
    List<int> intList = new List<int>();
    List<PXFieldInfo> source = new List<PXFieldInfo>();
    HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes((string) null))
    {
      if (attribute is IPXInterfaceField)
      {
        if (!stringSet.Contains(attribute.FieldName))
        {
          string displayName = string.IsNullOrEmpty(((IPXInterfaceField) attribute).DisplayName) ? attribute.FieldName : ((IPXInterfaceField) attribute).DisplayName;
          int index = source.FindIndex((Predicate<PXFieldInfo>) (i => i.DisplayName == displayName));
          if (index >= 0)
          {
            intList.Add(source.Count);
            if (!intList.Contains(index))
              intList.Add(index);
          }
          source.Add(new PXFieldInfo(attribute.FieldName, displayName));
          stringSet.Add(attribute.FieldName);
        }
      }
    }
    foreach (int index in intList)
    {
      PXFieldInfo pxFieldInfo = source[index];
      if (pxFieldInfo.Name != pxFieldInfo.DisplayName)
        pxFieldInfo.DisplayName = $"{pxFieldInfo.DisplayName} ({pxFieldInfo.Name})";
    }
    return source.OrderBy<PXFieldInfo, string>((Func<PXFieldInfo, string>) (r => r.DisplayName)).ToList<PXFieldInfo>();
  }

  [PXInternalUseOnly]
  public static List<PXFieldInfo> GetCacheFieldsIncludingNonUi(PXCache cache, bool includeNoteText)
  {
    List<PXFieldInfo> source = new List<PXFieldInfo>();
    Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
    foreach (string field in (List<string>) cache.Fields)
    {
      if (!cache.IsKvExtAttribute(field))
      {
        string str = (string) null;
        foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(field))
        {
          if (attribute is IPXInterfaceField pxInterfaceField)
            str = string.IsNullOrEmpty(pxInterfaceField.DisplayName) ? attribute.FieldName : pxInterfaceField.DisplayName;
        }
        if ((!field.Contains("_") || !Str.IsNullOrEmpty(str)) && (!Str.IsNullOrEmpty(str) || !field.StartsWith("Note") || string.Equals(field, "NoteText", StringComparison.InvariantCultureIgnoreCase) && includeNoteText))
        {
          string displayName = str ?? field;
          source.Add(new PXFieldInfo(field, displayName));
        }
      }
    }
    Dictionary<string, List<int>> dictionary2 = new Dictionary<string, List<int>>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
    for (int index = 0; index < source.Count; ++index)
    {
      PXFieldInfo pxFieldInfo = source[index];
      if (pxFieldInfo.DisplayName != null)
      {
        if (!dictionary2.ContainsKey(pxFieldInfo.DisplayName))
          dictionary2[pxFieldInfo.DisplayName] = new List<int>();
        dictionary2[pxFieldInfo.DisplayName].Add(index);
      }
    }
    foreach (KeyValuePair<string, List<int>> keyValuePair in dictionary2)
    {
      if (keyValuePair.Value.Count > 1)
      {
        foreach (int index in keyValuePair.Value)
        {
          PXFieldInfo pxFieldInfo = source[index];
          if (pxFieldInfo.Name != pxFieldInfo.DisplayName)
            pxFieldInfo.DisplayName = $"{pxFieldInfo.DisplayName} ({pxFieldInfo.Name})";
        }
      }
    }
    return source.OrderBy<PXFieldInfo, string>((Func<PXFieldInfo, string>) (r => r.DisplayName)).ToList<PXFieldInfo>();
  }

  [PXInternalUseOnly]
  public static List<PXFieldInfo> GetUserDefinedFields(PXCache cache)
  {
    List<PXFieldInfo> userDefinedFields = new List<PXFieldInfo>();
    foreach (string str in cache.Fields.Where<string>(new Func<string, bool>(cache.IsKvExtAttribute)))
    {
      PXFieldState stateExt = (PXFieldState) cache.GetStateExt((object) null, str);
      userDefinedFields.Add(new PXFieldInfo(str, stateExt.DisplayName ?? str));
    }
    return userDefinedFields;
  }

  [PXInternalUseOnly]
  public static Tuple<string, string?>[] GetFieldsForView(
    PXGraph graph,
    string viewName,
    bool includeAuditColumns = false,
    Func<string, PXCache, bool>? filter = null,
    GraphHelper.IncludeFields includeFields = GraphHelper.IncludeFields.WithDisplayNameOnly)
  {
    PXView view = graph.Views[viewName];
    IEnumerable<PXFieldInfo> source = EnumerableExtensions.Distinct<PXFieldInfo, string>((includeFields == GraphHelper.IncludeFields.WithDisplayNameOnly ? (IEnumerable<PXFieldInfo>) GraphHelper.GetCacheFields(view.Cache) : (IEnumerable<PXFieldInfo>) GraphHelper.GetCacheFieldsIncludingNonUi(view.Cache, includeFields == GraphHelper.IncludeFields.All)).Where<PXFieldInfo>((Func<PXFieldInfo, bool>) (it => includeAuditColumns || !GraphHelper.IsAuditFieldName(it.Name))).Union<PXFieldInfo>((IEnumerable<PXFieldInfo>) GraphHelper.GetUserDefinedFields(view.Cache)), (Func<PXFieldInfo, string>) (it => it.Name));
    if (filter != null)
      source = source.Where<PXFieldInfo>((Func<PXFieldInfo, bool>) (it => filter(it.Name, view.Cache)));
    return source.Select<PXFieldInfo, Tuple<string, string>>((Func<PXFieldInfo, Tuple<string, string>>) (it => new Tuple<string, string>(it.Name, it.DisplayName))).OrderBy<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (it => it.Item2)).ToArray<Tuple<string, string>>();
  }

  public static Tuple<string, string?>[] GetFieldsForView(
    PXGraph graph,
    System.Type fieldType,
    string viewName,
    bool includeAuditColumns = false)
  {
    PXView view = graph.Views[viewName];
    return EnumerableExtensions.Distinct<PXFieldInfo, string>(GraphHelper.GetCacheFields(view.Cache).Where<PXFieldInfo>((Func<PXFieldInfo, bool>) (it => includeAuditColumns || !GraphHelper.IsAuditFieldName(it.Name))).Where<PXFieldInfo>((Func<PXFieldInfo, bool>) (it => view.Cache.GetFieldType(it.Name) == fieldType)).Union<PXFieldInfo>((IEnumerable<PXFieldInfo>) GraphHelper.GetUserDefinedFields(view.Cache)), (Func<PXFieldInfo, string>) (it => it.Name)).Select<PXFieldInfo, Tuple<string, string>>((Func<PXFieldInfo, Tuple<string, string>>) (it => new Tuple<string, string>(it.Name, it.DisplayName))).OrderBy<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (it => it.Item2)).ToArray<Tuple<string, string>>();
  }

  [PXInternalUseOnly]
  public static Tuple<string, string?>[] GetFieldsForType(
    PXGraph graph,
    System.Type type,
    bool includeAuditColumns = false,
    GraphHelper.IncludeFields includeFields = GraphHelper.IncludeFields.WithDisplayNameOnly)
  {
    PXCache cach = graph.Caches[type];
    return EnumerableExtensions.Distinct<PXFieldInfo, string>((includeFields == GraphHelper.IncludeFields.WithDisplayNameOnly ? (IEnumerable<PXFieldInfo>) GraphHelper.GetCacheFields(cach) : (IEnumerable<PXFieldInfo>) GraphHelper.GetCacheFieldsIncludingNonUi(cach, includeFields == GraphHelper.IncludeFields.All)).Where<PXFieldInfo>((Func<PXFieldInfo, bool>) (it => includeAuditColumns || !GraphHelper.IsAuditFieldName(it.Name))).Union<PXFieldInfo>((IEnumerable<PXFieldInfo>) GraphHelper.GetUserDefinedFields(cach)), (Func<PXFieldInfo, string>) (it => it.Name)).Select<PXFieldInfo, Tuple<string, string>>((Func<PXFieldInfo, Tuple<string, string>>) (it => new Tuple<string, string>(it.Name, it.DisplayName))).OrderBy<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (it => it.Item2)).ToArray<Tuple<string, string>>();
  }

  public static bool IsAuditFieldName(string name)
  {
    return name.Equals("NoteID", StringComparison.OrdinalIgnoreCase) || name.Equals("CreatedByID", StringComparison.OrdinalIgnoreCase) || name.Equals("CreatedByScreenID", StringComparison.OrdinalIgnoreCase) || name.Equals("CreatedDateTime", StringComparison.OrdinalIgnoreCase) || name.Equals("LastModifiedByID", StringComparison.OrdinalIgnoreCase) || name.Equals("LastModifiedByScreenID", StringComparison.OrdinalIgnoreCase) || name.Equals("LastModifiedDateTime", StringComparison.OrdinalIgnoreCase) || name.Equals("tstamp", StringComparison.OrdinalIgnoreCase);
  }

  public static IEnumerable QuickSelect(this PXView view)
  {
    return view.Graph.QuickSelect(view.BqlSelect, (object[]) null, (PXFilterRow[]) null, view.IsReadOnly);
  }

  public static IEnumerable QuickSelect(this PXView view, object[]? parameters)
  {
    return view.Graph.QuickSelect(view.BqlSelect, parameters, (PXFilterRow[]) null, view.IsReadOnly);
  }

  public static IEnumerable QuickSelect(this PXGraph graph, System.Type bqlCommand)
  {
    return graph.QuickSelect(BqlCommand.CreateInstance(bqlCommand));
  }

  public static IEnumerable QuickSelect(this PXGraph graph, BqlCommand bqlCommand)
  {
    return GraphHelper.QuickSelect(graph, bqlCommand, (PXFilterRow[]) null);
  }

  public static IEnumerable QuickSelect(
    this PXGraph graph,
    BqlCommand bqlCommand,
    PXFilterRow[]? filters)
  {
    return graph.QuickSelect(bqlCommand, (object[]) null, filters);
  }

  public static IEnumerable QuickSelect(
    this PXGraph graph,
    BqlCommand bqlCommand,
    object[]? parameters)
  {
    return graph.QuickSelect(bqlCommand, parameters, (PXFilterRow[]) null);
  }

  public static IEnumerable QuickSelect(
    this PXGraph graph,
    BqlCommand bqlCommand,
    object[]? parameters,
    PXFilterRow[]? filters)
  {
    return graph.QuickSelect(bqlCommand, parameters, filters, true);
  }

  public static IEnumerable QuickSelect(
    this PXGraph graph,
    BqlCommand bqlCommand,
    object[]? parameters,
    PXFilterRow[]? filters,
    bool isReadOnly)
  {
    PXView pxView = new PXView(graph, isReadOnly, bqlCommand);
    int startRow = PXView.StartRow;
    int totalRows = 0;
    if (PXView.CurrentRestrictedFields.Any())
      pxView.RestrictedFields = PXView.CurrentRestrictedFields;
    List<object> objectList = pxView.Select(PXView.Currents, parameters ?? PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, filters ?? (PXFilterRow[]) PXView.Filters, ref startRow, PXView.MaximumRows, ref totalRows);
    PXView.StartRow = 0;
    return (IEnumerable) objectList;
  }

  [PXInternalUseOnly]
  public static PXSelectBase? GetDataMember(this PXGraph graph, string name)
  {
    System.Reflection.FieldInfo field = graph.GetType().GetField(name);
    return !(field == (System.Reflection.FieldInfo) null) ? field.GetValue((object) graph) as PXSelectBase : throw new Exception($"Cannot find a data member '{name}' in the graph '{graph.GetType().Name}'.");
  }

  [Obsolete("Do not use. Will be removed in future version.")]
  public static PXResultset<Table>? SoftSelect<Table>(
    this PXSelectBase<Table> select,
    params object[] arguments)
    where Table : class, IBqlTable, new()
  {
    PXResultset<Table> pxResultset = (PXResultset<Table>) null;
    if (select != null)
    {
      try
      {
        pxResultset = select.Select(arguments);
      }
      catch (PXSetPropertyException ex)
      {
      }
    }
    return pxResultset;
  }

  public static string GetName(this PXCache cache)
  {
    return Str.IsNullOrEmpty(cache.DisplayName) ? cache.DisplayName : PXMessages.Localize(cache.DisplayName, out string _);
  }

  public static object NonDirtyInsert(this PXCache cache)
  {
    bool isDirty = cache.IsDirty;
    object obj = cache.Insert();
    cache.IsDirty = isDirty;
    return obj;
  }

  public static TNode? NonDirtyInsert<TNode>(this PXCache cache, System.Action<TNode?> handler) where TNode : class
  {
    bool isDirty = cache.IsDirty;
    object instance = cache.CreateInstance();
    if (handler != null)
      handler(instance as TNode);
    object obj = cache.Insert(instance);
    cache.IsDirty = isDirty;
    return obj as TNode;
  }

  public static TNode? InitNewRow<TNode>(this PXCache cache, TNode? node = null) where TNode : class, IBqlTable, new()
  {
    return GraphHelper.InitNewRow<TNode>((PXCache<TNode>) cache, node);
  }

  public static TNode? InitNewRow<TNode>(this PXCache<TNode> cache, TNode? node = null) where TNode : class, IBqlTable, new()
  {
    TNode result = default (TNode);
    PXRowInserting handler = (PXRowInserting) ((sender, e) =>
    {
      result = (TNode) e.Row;
      e.Cancel = true;
    });
    cache.Graph.RowInserting.AddHandler(typeof (TNode), handler);
    try
    {
      if ((object) node != null)
        cache.Insert(node);
      else
        cache.Insert();
    }
    finally
    {
      cache.Graph.RowInserting.RemoveHandler(typeof (TNode), handler);
    }
    return result;
  }

  public static IEnumerable RowCast(this IEnumerable resultSet, System.Type nodeType)
  {
    foreach (object result in resultSet)
    {
      if (result is PXResult pxResult)
        yield return pxResult[nodeType];
      else
        yield return Convert.ChangeType(result, nodeType);
    }
  }

  public static IEnumerable<TNode> RowCast<TNode>(this IEnumerable resultSet) where TNode : IBqlTable
  {
    foreach (object result in resultSet)
    {
      if (result is PXResult pxResult)
        yield return (TNode) pxResult[typeof (TNode)];
      else
        yield return (TNode) result;
    }
  }

  private static IEnumerable SelectMyDacs() => (IEnumerable) new object[0];

  public static void PressButton(this PXAction action, PXAdapter? adapter)
  {
    ExceptionExtensions.ThrowOnNull<PXAction>(action, nameof (action), (string) null);
    if (adapter == null)
    {
      BqlCommand instance = BqlCommand.CreateInstance(typeof (Select<>), action.GetRowType());
      PXSelectDelegate handler = new PXSelectDelegate(GraphHelper.SelectMyDacs);
      adapter = new PXAdapter(new PXView(action.Graph, true, instance, (Delegate) handler));
    }
    IEnumerator enumerator = action.Press(adapter).GetEnumerator();
    do
      ;
    while (enumerator.MoveNext());
  }

  public static void PressButton(this PXAction action) => action.PressButton((PXAdapter) null);

  public static void EnsureCachePersistence<TNode>(this PXGraph graph) where TNode : IBqlTable
  {
    graph.EnsureCachePersistence<TNode>(false);
  }

  public static void EnsureCachePersistence<TNode>(this PXGraph graph, bool makeLast) where TNode : IBqlTable
  {
    graph.EnsureCachePersistence(typeof (TNode), makeLast);
  }

  public static void EnsureCachePersistence(this PXGraph? graph, System.Type? dacType)
  {
    graph.EnsureCachePersistence(dacType, false);
  }

  public static void EnsureCachePersistence(this PXGraph? graph, System.Type? dacType, bool makeLast)
  {
    if (graph == null || dacType == (System.Type) null)
      return;
    List<System.Type> caches = graph.Views.Caches;
    int index = caches.IndexOf(dacType);
    if (index == -1)
    {
      caches.Add(dacType);
    }
    else
    {
      if (!makeLast)
        return;
      caches.RemoveAt(index);
      caches.Add(dacType);
    }
  }

  public static TGraph Clone<TGraph>(this TGraph graph) where TGraph : PXGraph, new()
  {
    return graph.Clone<TGraph>(false);
  }

  internal static TGraph Clone<TGraph>(this TGraph graph, bool loadGraph) where TGraph : PXGraph, new()
  {
    graph.ReuseRestricted = true;
    DialogAnswer current = graph.Caches<DialogAnswer>().Current as DialogAnswer;
    graph.Unload();
    TGraph graph1;
    using (new PXPreserveScope())
    {
      if (graph is PXGenericInqGrph pxGenericInqGrph)
      {
        Guid? designId = pxGenericInqGrph.Design.DesignID;
        PXGenericInqGrph instance;
        if (designId.HasValue)
        {
          Guid? nullable = designId;
          Guid empty = Guid.Empty;
          if ((nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == empty ? 1 : 0) : 1) : 0) == 0)
          {
            instance = PXGenericInqGrph.CreateInstance(designId.Value, false);
            goto label_6;
          }
        }
        instance = PXGenericInqGrph.CreateInstance(pxGenericInqGrph.Description);
label_6:
        graph1 = instance as TGraph;
      }
      else
      {
        System.Type type = graph.GetType();
        graph1 = graph.IsMobile ? (TGraph) PXGraph.CreateInstance(type, graph.StatePrefix) : (TGraph) PXGraph.CreateInstance(type);
      }
      graph1.IsMobile = graph.IsMobile;
      graph1.Accessinfo = graph.Accessinfo;
      if (current != null)
      {
        graph.Caches<DialogAnswer>().Insert(current);
        graph1.Caches<DialogAnswer>().Insert(current);
      }
      if (loadGraph)
      {
        graph1.Load();
      }
      else
      {
        foreach (System.Type key in graph.Caches.Keys)
          graph1.Caches[key]?.LoadFromSession();
      }
    }
    return graph1;
  }

  public static PXGraph TypelessClone(this PXGraph graph)
  {
    graph.Unload();
    using (new PXPreserveScope())
      return PXGraph.CreateInstance(graph.GetType());
  }

  public static PXCache<T> Caches<T>(this PXGraph graph) where T : class, IBqlTable, new()
  {
    return graph.Caches<T>(false);
  }

  public static PXCache<T> Caches<T>(this PXGraph graph, bool ensurePersistence) where T : class, IBqlTable, new()
  {
    PXCache<T> cach = (PXCache<T>) graph.Caches[typeof (T)];
    if (ensurePersistence)
    {
      System.Type itemType = cach.GetItemType();
      if ((object) itemType != null && !graph.Views.Caches.Contains(itemType))
        graph.Views.Caches.Add(itemType);
    }
    return cach;
  }

  [Obsolete("Use MarkUpdated and Hold methods instead")]
  public static void SmartSetStatus(
    this PXCache cache,
    object row,
    PXEntryStatus dst,
    PXEntryStatus src = PXEntryStatus.Notchanged)
  {
    if (cache.GetStatus(row) != src)
      return;
    cache.SetStatus(row, dst);
  }

  /// <summary>Hold entity in the cache</summary>
  public static void Hold(this PXCache cache, object row)
  {
    if (cache.GetStatus(row) != PXEntryStatus.Notchanged)
      return;
    cache.SetStatus(row, PXEntryStatus.Held);
  }

  /// <summary>Mark entity as updated without firing any event</summary>
  public static void MarkUpdated(this PXCache cache, object row) => cache.MarkUpdated(row, true);

  /// <summary>Mark entity as updated without firing any event</summary>
  public static void MarkUpdated(this PXCache cache, object row, bool assertError)
  {
    if (assertError && row != null)
    {
      object obj = cache.Locate(row);
      if (obj != null && obj != row)
      {
        DacDescriptor? emptyDacDescriptor = cache.GetNonEmptyDacDescriptor(row);
        throw new InvalidOperationException(PXMessages.LocalizeNoPrefix("Cannot mark the record as updated because another record with the same key exists in the cache. Contact your Acumatica support provider for the assistance.")).AddDacDescriptor<InvalidOperationException>(emptyDacDescriptor);
      }
    }
    PXEntryStatus status = cache.GetStatus(row);
    if (!EnumerableExtensions.IsIn<PXEntryStatus>(status, PXEntryStatus.Inserted, PXEntryStatus.InsertedDeleted, PXEntryStatus.Deleted, PXEntryStatus.Updated))
      cache.SetStatus(row, PXEntryStatus.Updated);
    else
      cache.EnsureUnmodified(row, new PXEntryStatus?(status), new PXEntryStatus?(status));
  }

  /// <summary>Mark entity as deleted without firing any event</summary>
  public static void MarkDeleted(this PXCache cache, object row)
  {
    PXEntryStatus status = cache.GetStatus(row);
    if (EnumerableExtensions.IsIn<PXEntryStatus>(status, PXEntryStatus.InsertedDeleted, PXEntryStatus.Deleted))
      return;
    cache.SetStatus(row, status == PXEntryStatus.Inserted ? PXEntryStatus.InsertedDeleted : PXEntryStatus.Deleted);
  }

  public static void EnsureRowPersistence(this PXGraph graph, object row)
  {
    graph.EnsureCachePersistence(row.GetType());
    graph.Caches[row.GetType()].MarkUpdated(row);
  }

  public static System.Type GetBqlField(this PXCache cache, string fieldName)
  {
    System.Type bqlField = cache.GetBqlField(fieldName);
    return !(bqlField == (System.Type) null) ? bqlField : throw new PXException($"The field '{fieldName}' is not found in the cache '{cache.GetItemType().FullName}'.");
  }

  public static System.Type GetBqlField<FieldType>(this PXCache cache) where FieldType : IBqlField
  {
    return cache.GetBqlField(typeof (FieldType).Name);
  }

  public static PXCache? GetPrimaryCache(this PXGraph graph)
  {
    if (Str.IsNullOrEmpty(graph.PrimaryView))
    {
      PXAction pxAction = graph != null ? ((IEnumerable<PXAction>) graph.Actions.Values.ToArray<PXAction>()).FirstOrDefault<PXAction>((Func<PXAction, bool>) (action => action.GetRowType() != (System.Type) null)) : (PXAction) null;
      if (pxAction == null)
        return (PXCache) null;
      return graph?.Caches[pxAction.GetRowType()];
    }
    return graph?.Views[graph.PrimaryView]?.Cache;
  }

  public static object? GetCurrentPrimaryObject(this PXGraph graph)
  {
    if (graph == null)
      return (object) null;
    return graph.GetPrimaryCache()?.Current;
  }

  public static bool IsPrimaryObjectInserted(this PXGraph graph)
  {
    PXCache primaryCache = graph.GetPrimaryCache();
    return primaryCache != null && primaryCache.GetStatus(primaryCache.Current) == PXEntryStatus.Inserted;
  }

  [PXInternalUseOnly]
  internal static unsafe void ClearBqlTableSystemData(this PXGraph? graph)
  {
    if (graph == null)
      return;
    foreach (PXCache cach in graph.Caches.Caches)
    {
      foreach (object obj in cach.Cached)
      {
        if (obj is IBqlTableSystemDataStorage systemDataStorage)
          *(PXBqlTableSystemData*) ref systemDataStorage.GetBqlTableSystemData() = new PXBqlTableSystemData();
      }
    }
    foreach (KeyValuePair<ViewKey, PXViewQueryCollection> keyValuePair in (Dictionary<ViewKey, PXViewQueryCollection>) graph.QueryCache)
    {
      foreach (PXQueryResult pxQueryResult in keyValuePair.Value.Values)
      {
        if (pxQueryResult.Items is PXView.VersionedList items && items.MergedList != null)
        {
          foreach (object merged in (List<object>) items.MergedList)
          {
            if (merged is IBqlTableSystemDataStorage systemDataStorage)
              *(PXBqlTableSystemData*) ref systemDataStorage.GetBqlTableSystemData() = new PXBqlTableSystemData();
          }
        }
        foreach (object obj in pxQueryResult.Items)
        {
          if (obj is IBqlTableSystemDataStorage systemDataStorage)
            *(PXBqlTableSystemData*) ref systemDataStorage.GetBqlTableSystemData() = new PXBqlTableSystemData();
        }
      }
    }
  }

  internal static PXGraph? CreateGraph(string graphName)
  {
    System.Type type1 = PXBuildManager.GetType(graphName, false);
    if (type1 == (System.Type) null)
      type1 = System.Type.GetType(graphName);
    if (!(type1 != (System.Type) null))
      return (PXGraph) null;
    System.Type type2 = PXBuildManager.GetType(CustomizedTypeManager.GetCustomizedTypeFullName(type1), false);
    if ((object) type2 == null)
      type2 = type1;
    System.Type graphType = type2;
    using (new PXPreserveScope())
    {
      try
      {
        return PXGraph.CreateInstance(graphType);
      }
      catch (TargetInvocationException ex)
      {
        throw PXException.ExtractInner((Exception) ex);
      }
    }
  }

  private class NoDac : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }

  public enum IncludeFields
  {
    WithDisplayNameOnly,
    All,
    AllExceptNoteText,
  }
}
