// Decompiled with JetBrains decompiler
// Type: PX.Api.Soap.Screen.ServiceManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Reports;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Hosting;

#nullable disable
namespace PX.Api.Soap.Screen;

/// <summary>
/// Interface manager, contains the list for exposed dynamic types
/// </summary>
public class ServiceManager
{
  private static object _staticLock = new object();
  private static Lazy<ServiceManager.CacheData> _cache = new Lazy<ServiceManager.CacheData>(new Func<ServiceManager.CacheData>(ServiceManager.FindCacheAndGraphTypes));
  private static ServiceManager.GraphDescriptionsCollection _GraphDescriptions;
  private static Dictionary<string, string> _LegacyTableNames;

  private static ServiceManager.NameTypeDictionary Graphs => ServiceManager._cache.Value._graphs;

  private static ServiceManager.NameTypeDictionary HiddenGraphs
  {
    get => ServiceManager._cache.Value._hiddenGraphs;
  }

  private static ServiceManager.NameTypeDictionary TablesData
  {
    get => ServiceManager._cache.Value._tables;
  }

  private static ServiceManager.NameTypeDictionary HiddenTablesData
  {
    get => ServiceManager._cache.Value._hiddenTables;
  }

  private static ServiceManager.GraphDescriptionsCollection GraphDescriptions
  {
    get
    {
      if (ServiceManager._GraphDescriptions == null)
      {
        lock (ServiceManager._staticLock)
        {
          if (ServiceManager._GraphDescriptions == null)
            ServiceManager._GraphDescriptions = ServiceManager.GetGraphDescriptions();
        }
      }
      return ServiceManager._GraphDescriptions;
    }
  }

  internal static IEnumerable<Graph> AllGraphsNotCustomized
  {
    get => (IEnumerable<Graph>) ServiceManager.GraphDescriptions.Values;
  }

  internal static IEnumerable<System.Type> AllHiddenGraphs
  {
    get
    {
      foreach (System.Type hidden in ServiceManager.GraphDescriptions.Hiddens)
        yield return hidden;
    }
  }

  public static IEnumerable<System.Type> AllGraphsTypesNotCustomized
  {
    get
    {
      foreach (ServiceManager.TypeInfo typeInfo in ServiceManager.Graphs.Values)
        yield return typeInfo.Type;
    }
  }

  private static void ResetTypes()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ServiceManager._cache = new Lazy<ServiceManager.CacheData>(ServiceManager.\u003C\u003EO.\u003C0\u003E__FindCacheAndGraphTypes ?? (ServiceManager.\u003C\u003EO.\u003C0\u003E__FindCacheAndGraphTypes = new Func<ServiceManager.CacheData>(ServiceManager.FindCacheAndGraphTypes)));
    ServiceManager._GraphDescriptions = (ServiceManager.GraphDescriptionsCollection) null;
    ServiceManager.LegacyReportNames.Clear();
  }

  /// <summary>One time intializer</summary>
  static ServiceManager()
  {
    PXCodeDirectoryCompiler.NotifyOnChange(new System.Action(ServiceManager.ResetTypes));
  }

  /// <summary>Registers another type into specified container</summary>
  private static ServiceManager.GraphDescriptionsCollection GetGraphDescriptions()
  {
    ServiceManager.GraphDescriptionsCollection graphDescriptions = new ServiceManager.GraphDescriptionsCollection();
    try
    {
      foreach (System.Type enumTypesInAssembly in PXSubstManager.EnumTypesInAssemblies(nameof (GetGraphDescriptions)))
      {
        if (enumTypesInAssembly != (System.Type) null && typeof (PXGraph).IsAssignableFrom(enumTypesInAssembly) && enumTypesInAssembly != typeof (PXGraph) && !enumTypesInAssembly.IsGenericType)
        {
          if (enumTypesInAssembly.IsDefined(typeof (PXHiddenAttribute), false) && !((PXHiddenAttribute) enumTypesInAssembly.GetCustomAttributes(typeof (PXHiddenAttribute), false)[0]).ServiceVisible)
          {
            graphDescriptions.Hiddens.Add(enumTypesInAssembly);
            PXSubstManager.AddTypeToNamedList(nameof (GetGraphDescriptions), enumTypesInAssembly);
          }
          else
          {
            Graph graph = new Graph()
            {
              GraphName = enumTypesInAssembly.FullName
            };
            PXSubstManager.AddTypeToNamedList(nameof (GetGraphDescriptions), enumTypesInAssembly);
            PXSiteMapNode siteMapNode = PXSiteMap.Provider.FindSiteMapNode(enumTypesInAssembly);
            if (siteMapNode != null)
              graph.Text = siteMapNode.Title;
            graph.IsNamespace = new bool?(false);
            if (!graphDescriptions.ContainsKey(graph.GraphName))
              graphDescriptions.Add(graph.GraphName, graph);
          }
        }
      }
      foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
      {
        if (PXSubstManager.IsSuitableTypeExportAssembly(assembly, true))
        {
          object[] customAttributes = assembly.GetCustomAttributes(typeof (PXHiddenAttribute), true);
          if (customAttributes != null)
          {
            foreach (PXHiddenAttribute pxHiddenAttribute in customAttributes)
            {
              if (pxHiddenAttribute.Target != (System.Type) null && !graphDescriptions.Hiddens.Contains(pxHiddenAttribute.Target))
                graphDescriptions.Hiddens.Add(pxHiddenAttribute.Target);
            }
          }
        }
      }
      PXSubstManager.SaveTypeCache(nameof (GetGraphDescriptions));
    }
    catch
    {
    }
    return graphDescriptions;
  }

  private static ServiceManager.CacheData FindCacheAndGraphTypes()
  {
    ServiceManager.CacheData cacheAndGraphTypes = new ServiceManager.CacheData();
    List<System.Type> typeList1 = new List<System.Type>();
    List<System.Type> typeList2 = new List<System.Type>();
    List<System.Type> typeList3 = new List<System.Type>();
    try
    {
      foreach (System.Type enumTypesInAssembly in PXSubstManager.EnumTypesInAssemblies(nameof (FindCacheAndGraphTypes)))
      {
        try
        {
          ServiceManager.ProcessType(enumTypesInAssembly, typeList1, typeList2, typeList3, false);
        }
        catch
        {
        }
      }
      foreach (System.Type t in typeList1.Union<System.Type>((IEnumerable<System.Type>) typeList2).Union<System.Type>((IEnumerable<System.Type>) typeList3))
        PXSubstManager.AddTypeToNamedList(nameof (FindCacheAndGraphTypes), t);
      PXSubstManager.SaveTypeCache(nameof (FindCacheAndGraphTypes));
    }
    catch
    {
    }
    foreach (System.Type compiledType in PXCodeDirectoryCompiler.GetCompiledTypes<object>())
      ServiceManager.ProcessType(compiledType, typeList1, typeList2, typeList3, true);
    ServiceManager.GenerateShortNames(typeList1, cacheAndGraphTypes._tables);
    ServiceManager.GenerateShortNames(typeList2, cacheAndGraphTypes._hiddenTables);
    ServiceManager.GenerateShortNames(typeList3, cacheAndGraphTypes._graphs);
    ServiceManager.GenerateShortNames(ServiceManager.AllHiddenGraphs.ToList<System.Type>(), cacheAndGraphTypes._hiddenGraphs);
    return cacheAndGraphTypes;
  }

  private static void ProcessType(
    System.Type t,
    List<System.Type> tables,
    List<System.Type> hiddenTables,
    List<System.Type> graphs,
    bool checkDuplicate)
  {
    if (t == (System.Type) null || !t.IsClass || t.IsAbstract || t.IsNotPublic || t.ContainsGenericParameters || t.GetConstructor(System.Type.EmptyTypes) == (ConstructorInfo) null)
      return;
    if (ServiceManager.IsServiceVisible(t))
    {
      if (ServiceManager.IsTable(t) && (!checkDuplicate || !tables.Contains(t)))
      {
        tables.Add(t);
        ServiceManager.LegacyReportNames.AddTable(t);
      }
      else
      {
        if (!ServiceManager.IsGraph(t) || checkDuplicate && graphs.Contains(t) || t.IsGenericType || CustomizedTypeManager.IsCustomizedType(t))
          return;
        graphs.Add(t);
      }
    }
    else
    {
      if (!ServiceManager.IsTable(t) || checkDuplicate && hiddenTables.Contains(t))
        return;
      hiddenTables.Add(t);
    }
  }

  private static string GetModuleName(System.Type t)
  {
    string[] source = (t.Namespace ?? "").Split('.');
    string moduleName = (string) null;
    if (source.Length != 0 && source[0] == "PX")
      moduleName = ((IEnumerable<string>) source).FirstOrDefault<string>((Func<string, bool>) (s => s.Length == 2 && s != "PX"));
    if (string.IsNullOrEmpty(moduleName))
      moduleName = "Core";
    return moduleName;
  }

  private static string GetShortName(System.Type t, bool useModule, bool useSegment)
  {
    if (!useModule && !useSegment)
      return t.Name;
    StringBuilder stringBuilder = new StringBuilder(t.Name);
    string name = BqlCommand.GetItemType(t) == (System.Type) null ? "" : BqlCommand.GetItemType(t).Name;
    string[] source = !Str.IsNullOrEmpty(t.Namespace) ? t.Namespace.Split('.') : throw new PXException("The type {0} is declared out of any namespace.", new object[1]
    {
      (object) t
    });
    string moduleName = ServiceManager.GetModuleName(t);
    if (useModule && !string.IsNullOrEmpty(moduleName) && !t.Name.StartsWith(moduleName) && !name.StartsWith(moduleName))
    {
      stringBuilder.Append("_");
      stringBuilder.Append(moduleName);
    }
    if (!string.IsNullOrEmpty(name))
    {
      string str = name;
      stringBuilder.Append("_");
      stringBuilder.Append(str);
    }
    else if (useSegment)
    {
      string str = ((IEnumerable<string>) source).LastOrDefault<string>();
      if (str == moduleName)
        str = (string) null;
      if (!string.IsNullOrEmpty(str))
      {
        stringBuilder.Append("_");
        stringBuilder.Append(str);
      }
    }
    return stringBuilder.ToString();
  }

  private static bool IsLess(System.Type less, System.Type more)
  {
    if (less.DeclaringType != (System.Type) null || !(more.Namespace ?? "").StartsWith(less.Namespace ?? ""))
      return false;
    return more.DeclaringType != (System.Type) null || (more.Namespace ?? "").Length > (less.Namespace ?? "").Length;
  }

  private static System.Type GetPrefferedType(IEnumerable<System.Type> list)
  {
    System.Type prefferedType = (System.Type) null;
    foreach (System.Type type in list)
    {
      if (prefferedType == (System.Type) null)
        prefferedType = type;
      else if (ServiceManager.IsLess(type, prefferedType))
        prefferedType = type;
      else if (!ServiceManager.IsLess(prefferedType, type))
        return (System.Type) null;
    }
    return prefferedType;
  }

  /// <summary>
  /// group by class name
  /// if more then one, resolve conflict by suffix
  /// extract (module) from type name
  /// if distinct modules exists - include them in suffix, else do not include
  /// if in one module contains several types - include nearest segment, if this not helps - die
  /// </summary>
  /// <param name="list"></param>
  /// <param name="container"></param>
  private static void GenerateShortNames(
    List<System.Type> list,
    ServiceManager.NameTypeDictionary container)
  {
    foreach (IGrouping<string, System.Type> source1 in list.GroupBy<System.Type, string>((Func<System.Type, string>) (t => t.Name)))
    {
      if (source1.Count<System.Type>() == 1)
      {
        container.Add(source1.Key, source1.First<System.Type>());
      }
      else
      {
        IGrouping<string, System.Type>[] array = source1.GroupBy<System.Type, string>((Func<System.Type, string>) (t => ServiceManager.GetModuleName(t))).ToArray<IGrouping<string, System.Type>>();
        bool useModule = array.Length > 1;
        foreach (IGrouping<string, System.Type> grouping in array)
        {
          if (grouping.Count<System.Type>() == 1)
          {
            System.Type t = grouping.First<System.Type>();
            string shortName = ServiceManager.GetShortName(t, useModule, false);
            container.Add(shortName, t);
          }
          else
          {
            System.Type pref = ServiceManager.GetPrefferedType((IEnumerable<System.Type>) grouping);
            foreach (IGrouping<string, System.Type> source2 in grouping.GroupBy<System.Type, string>((Func<System.Type, string>) (t => ServiceManager.GetShortName(t, useModule, t != pref))).ToArray<IGrouping<string, System.Type>>())
            {
              if (source2.Count<System.Type>() == 1)
              {
                string key = source2.Key;
                System.Type t = source2.First<System.Type>();
                container.Add(key, t);
              }
            }
          }
        }
      }
    }
  }

  /// <summary>
  /// Returns true when the specified type is a BqlTable object.
  /// </summary>
  private static bool IsTable(System.Type t)
  {
    return typeof (IBqlTable).IsAssignableFrom(t) && !typeof (PXMappedCacheExtension).IsAssignableFrom(t) && !t.IsGenericType;
  }

  private static bool IsServiceVisible(System.Type t)
  {
    return !t.IsDefined(typeof (PXHiddenAttribute), false) || ((PXHiddenAttribute) t.GetCustomAttributes(typeof (PXHiddenAttribute), false)[0]).ServiceVisible;
  }

  /// <summary>
  /// Returns true when specified type is a subclass of SwGraph
  /// </summary>
  private static bool IsGraph(System.Type t) => t.IsSubclassOf(typeof (PXGraph));

  /// <summary>returns the compiled service type</summary>
  internal static IEnumerable<ServiceManager.TypeInfo> TableList
  {
    get
    {
      List<ServiceManager.TypeInfo> tableList = new List<ServiceManager.TypeInfo>(ServiceManager.TablesData.Values);
      tableList.Sort((Comparison<ServiceManager.TypeInfo>) ((a, b) => Comparer<string>.Default.Compare(a.UniqueName, b.UniqueName)));
      return (IEnumerable<ServiceManager.TypeInfo>) tableList;
    }
  }

  internal static Dictionary<string, string> LegacyTableNames()
  {
    if (ServiceManager._LegacyTableNames == null)
    {
      string path = HostingEnvironment.MapPath("~/App_Data/ReportNames.xml");
      ServiceManager._LegacyTableNames = !File.Exists(path) ? new Dictionary<string, string>() : ((IEnumerable<string>) File.ReadAllLines(path)).Select(line => new
      {
        line = line,
        a = line.Split('=')
      }).Select(_param1 => new
      {
        \u003C\u003Eh__TransparentIdentifier0 = _param1,
        key = ((IEnumerable<string>) _param1.a).First<string>()
      }).Select(_param1 => new
      {
        \u003C\u003Eh__TransparentIdentifier1 = _param1,
        val = ((IEnumerable<string>) _param1.\u003C\u003Eh__TransparentIdentifier0.a).Last<string>()
      }).Select(_param1 => new
      {
        key = _param1.\u003C\u003Eh__TransparentIdentifier1.key,
        val = _param1.val
      }).ToDictionary(_ => _.key, _ => _.val);
    }
    return ServiceManager._LegacyTableNames;
  }

  internal static IEnumerable<ServiceManager.TypeInfo> GraphList
  {
    get
    {
      List<ServiceManager.TypeInfo> graphList = new List<ServiceManager.TypeInfo>(ServiceManager.Graphs.Values);
      graphList.Sort((Comparison<ServiceManager.TypeInfo>) ((a, b) => Comparer<string>.Default.Compare(a.UniqueName, b.UniqueName)));
      return (IEnumerable<ServiceManager.TypeInfo>) graphList;
    }
  }

  /// <summary>Returns the tables list</summary>
  [Obsolete("This property is obsolete and will be removed in the future versions. Use PX.Metadata.IDacRegistry.Visible instead.")]
  public static IEnumerable<System.Type> Tables
  {
    get
    {
      return ServiceManager.TablesData.Values.Select<ServiceManager.TypeInfo, System.Type>((Func<ServiceManager.TypeInfo, System.Type>) (t => t.Type));
    }
  }

  [Obsolete("This property is obsolete and will be removed in the future versions. Use PX.Metadata.IDacRegistry.Hidden instead.")]
  public static IEnumerable<System.Type> HiddenTables
  {
    get
    {
      return ServiceManager.HiddenTablesData.Values.Select<ServiceManager.TypeInfo, System.Type>((Func<ServiceManager.TypeInfo, System.Type>) (_ => _.Type));
    }
  }

  [Obsolete("This property is obsolete and will be removed in the future versions. Use PX.Metadata.IDacRegistry.All instead.")]
  public static IEnumerable<System.Type> AllTables
  {
    get
    {
      foreach (System.Type table in ServiceManager.Tables)
        yield return table;
      foreach (System.Type hiddenTable in ServiceManager.HiddenTables)
        yield return hiddenTable;
    }
  }

  /// <summary>Returns array of table names</summary>
  public static string[] TableNames
  {
    get => ServiceManager.ListToStrings(ServiceManager.TablesData.Names);
  }

  /// <summary>Returns array of graph names</summary>
  internal static string[] GraphNames => ServiceManager.ListToStrings(ServiceManager.Graphs.Names);

  /// <summary>Converts the list of types into the array of string</summary>
  private static string[] ListToStrings(ICollection<string> list)
  {
    List<string> stringList = new List<string>(list.Count);
    foreach (string str in (IEnumerable<string>) list)
      stringList.Add(str);
    stringList.Sort();
    return stringList.ToArray();
  }

  public static System.Type GetGraphTypeByFullName(string fullName)
  {
    return ServiceManager.Graphs.Values.FirstOrDefault<ServiceManager.TypeInfo>((Func<ServiceManager.TypeInfo, bool>) (n => n.Type.FullName == fullName))?.Type;
  }

  /// <summary>Returns the graph type by the name</summary>
  public static System.Type GetGraphType(string GraphName)
  {
    return ServiceManager.Graphs.GetType(GraphName);
  }

  public static System.Type TryGetGraphType(string GraphName)
  {
    return ServiceManager.Graphs.TryGetType(GraphName);
  }

  public static System.Type TryGetHiddenGraphType(string GraphName)
  {
    return ServiceManager.HiddenGraphs.TryGetType(GraphName);
  }

  public static string GetTableNameFromType(System.Type t) => ServiceManager.TablesData.GetName(t);

  public static string GetGraphNameFromType(System.Type t)
  {
    if (t.FullName.StartsWith("PX.Customization."))
      t = t.BaseType;
    return ServiceManager.Graphs.GetName(t);
  }

  /// <summary>Creates the graph by the type specified</summary>
  internal static PXGraph CreateGraphInstance(System.Type GraphType)
  {
    if (GraphType == (System.Type) null)
      throw new ArgumentNullException(nameof (GraphType));
    using (new PXPreserveScope())
      return PXGraph.CreateInstance(GraphType);
  }

  /// <summary>Returns members of SwSelectBase type</summary>
  public static MemberInfo[] GetViews(System.Type graphType)
  {
    return ServiceManager.GetMembers(graphType, new System.Type[1]
    {
      typeof (PXSelectBase)
    });
  }

  /// <summary>Returns members of SwAction type</summary>
  public static string[] GetActions(System.Type graphType)
  {
    MemberInfo[] members = ServiceManager.GetMembers(graphType, new System.Type[1]
    {
      typeof (PXAction)
    });
    List<string> stringList = new List<string>();
    foreach (MemberInfo memberInfo in members)
      stringList.Add(memberInfo.Name);
    return stringList.ToArray();
  }

  /// <summary>Returns filtered members of memberTypes</summary>
  private static MemberInfo[] GetMembers(System.Type graphType, System.Type[] memberTypes)
  {
    List<MemberInfo> memberInfoList = new List<MemberInfo>();
    foreach (System.Reflection.FieldInfo field in graphType.GetFields(BindingFlags.Instance | BindingFlags.Public))
    {
      if (!(field.DeclaringType != graphType) || (object) field == (object) graphType.GetField(field.Name))
      {
        foreach (System.Type memberType in memberTypes)
        {
          if (field.FieldType.IsSubclassOf(memberType))
          {
            memberInfoList.Add((MemberInfo) field);
            break;
          }
        }
      }
    }
    return memberInfoList.ToArray();
  }

  public static System.Type GetTableType(string name) => ServiceManager.TablesData.GetType(name);

  public static System.Type GetHiddenTableType(string name)
  {
    return ServiceManager.HiddenTablesData.GetType(name);
  }

  public static System.Type TryGetTableType(string name)
  {
    return ServiceManager.TablesData.TryGetType(name);
  }

  private class CacheData
  {
    internal ServiceManager.NameTypeDictionary _graphs = new ServiceManager.NameTypeDictionary();
    internal ServiceManager.NameTypeDictionary _hiddenGraphs = new ServiceManager.NameTypeDictionary();
    internal ServiceManager.NameTypeDictionary _tables = new ServiceManager.NameTypeDictionary();
    internal ServiceManager.NameTypeDictionary _hiddenTables = new ServiceManager.NameTypeDictionary();
  }

  internal class TypeInfo
  {
    public System.Type Type;
    public string UniqueName;
  }

  private class GraphDescriptionsCollection : Dictionary<string, Graph>
  {
    private List<System.Type> _hiddens = new List<System.Type>();

    public List<System.Type> Hiddens => this._hiddens;
  }

  internal class ReportNameResolver
  {
    public static System.Type ResolveShortName(string tname)
    {
      tname = ServiceManager.ReportNameResolver.RemovePref(tname);
      System.Type type = (System.Type) null;
      if (ServiceManager.TablesData.ContainsName(tname))
        type = ServiceManager.GetTableType(tname);
      else if (ServiceManager.HiddenTablesData.ContainsName(tname))
        type = ServiceManager.GetHiddenTableType(tname);
      object rowTypes = ServiceManager.LegacyReportNames.GetRowTypesMap()[(object) tname];
      if (rowTypes != null)
        return (System.Type) rowTypes;
      return type != (System.Type) null ? type : (System.Type) null;
    }

    private static string RemovePref(string rowName)
    {
      return !rowName.StartsWith("Row") ? rowName : rowName.Substring(3);
    }

    public static System.Type ResolveTable(string tname, ReportTableCollection reportTables)
    {
      if (reportTables != null)
      {
        bool tableNameContainsTypeAndNamespaceDelimiters = tname.Contains<char>(System.Type.Delimiter) || tname.Contains<char>('+');
        ReportTable t = ((IEnumerable<ReportTable>) reportTables).FirstOrDefault<ReportTable>((Func<ReportTable, bool>) (_ =>
        {
          if (_.FullName == tname)
            return true;
          return !tableNameContainsTypeAndNamespaceDelimiters && _.Name == tname;
        }));
        if (t != null)
          return ServiceManager.ReportNameResolver.ResolveTable(t);
      }
      return ServiceManager.ReportNameResolver.ResolveShortName(tname);
    }

    public static System.Type ResolveTable(ReportTable t)
    {
      if (string.IsNullOrEmpty(t.FullName))
        return ServiceManager.ReportNameResolver.ResolveShortName(t.Name);
      System.Type type = ServiceManager.Tables.Concat<System.Type>(ServiceManager.HiddenTables).FirstOrDefault<System.Type>((Func<System.Type, bool>) (_ => _.FullName == t.FullName));
      return !(type == (System.Type) null) ? type : throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("Cannot resolve the table name: [{0}]", (object) t.FullName));
    }
  }

  private static class LegacyReportNames
  {
    private static readonly Hashtable _tables = new Hashtable();
    private static readonly Hashtable _rowTypesMap = new Hashtable();

    public static void AddTable(System.Type t)
    {
      ServiceManager.LegacyReportNames.AddType(ServiceManager.LegacyReportNames._tables, t);
    }

    private static void AddType(Hashtable container, System.Type t)
    {
      int num = 0;
      string key;
      for (key = t.Name; container.ContainsKey((object) key); key = $"{t.Name}_{num}")
      {
        System.Type type = (System.Type) container[(object) key];
        if (type.FullName.Length > t.FullName.Length)
        {
          container[(object) key] = (object) t;
          t = type;
        }
        ++num;
      }
      container[(object) key] = (object) t;
    }

    /// <summary>Returns unique table name (using namespaces</summary>
    private static string GetUniqueTableName(System.Type t)
    {
      string str1 = t.Name;
      string source = t.FullName.Replace('+', System.Type.Delimiter);
      if (!source.Contains<char>(System.Type.Delimiter))
        return source;
      int length;
      for (string str2 = source.Length - str1.Length - 1 <= 0 ? "" : source.Substring(0, source.Length - str1.Length - 1); !string.IsNullOrEmpty(str2); str2 = length > 0 ? str2.Substring(0, length) : (string) null)
      {
        int num = 0;
        foreach (DictionaryEntry table in ServiceManager.LegacyReportNames._tables)
        {
          System.Type type = (System.Type) table.Value;
          if (type == t && type.Name == (string) table.Key)
          {
            num = 0;
            break;
          }
          char delimiter = System.Type.Delimiter;
          string str3 = delimiter.ToString() + type.FullName.Replace('+', System.Type.Delimiter);
          delimiter = System.Type.Delimiter;
          string str4 = delimiter.ToString() + str1;
          if (str3.EndsWith(str4))
            ++num;
        }
        if (num >= 2)
        {
          length = str2.LastIndexOf(System.Type.Delimiter);
          if (length < 0)
            length = -1;
          str1 = str2.Substring(length + 1) + System.Type.Delimiter.ToString() + str1;
        }
        else
          break;
      }
      return str1.Replace(System.Type.Delimiter.ToString(), "");
    }

    internal static Hashtable GetRowTypesMap()
    {
      if (ServiceManager.LegacyReportNames._rowTypesMap.Count == 0 && ServiceManager.LegacyReportNames._tables.Count > 0)
      {
        foreach (System.Type t in (IEnumerable) ServiceManager.LegacyReportNames._tables.Values)
          ServiceManager.LegacyReportNames._rowTypesMap[(object) ServiceManager.LegacyReportNames.GetUniqueTableName(t)] = (object) t;
      }
      return ServiceManager.LegacyReportNames._rowTypesMap;
    }

    public static void Clear()
    {
      ServiceManager.LegacyReportNames._tables.Clear();
      ServiceManager.LegacyReportNames._rowTypesMap.Clear();
    }
  }

  private class NameTypeDictionary
  {
    private readonly Dictionary<string, ServiceManager.TypeInfo> _nameIndex = new Dictionary<string, ServiceManager.TypeInfo>();
    private readonly Dictionary<System.Type, ServiceManager.TypeInfo> _typeIndex = new Dictionary<System.Type, ServiceManager.TypeInfo>();

    public ICollection<string> Names => (ICollection<string>) this._nameIndex.Keys;

    public IEnumerable<ServiceManager.TypeInfo> Values
    {
      get => (IEnumerable<ServiceManager.TypeInfo>) this._typeIndex.Values;
    }

    public void Add(string name, System.Type t)
    {
      ServiceManager.TypeInfo typeInfo = new ServiceManager.TypeInfo()
      {
        Type = t,
        UniqueName = name
      };
      this._nameIndex.Add(name, typeInfo);
      this._typeIndex.Add(t, typeInfo);
    }

    public System.Type GetType(string name) => this._nameIndex[name].Type;

    public System.Type TryGetType(string name)
    {
      ServiceManager.TypeInfo typeInfo;
      return this._nameIndex.TryGetValue(name, out typeInfo) ? typeInfo.Type : (System.Type) null;
    }

    public string GetName(System.Type t) => this._typeIndex[t].UniqueName;

    public bool ContainsName(string n) => this._nameIndex.ContainsKey(n);
  }
}
