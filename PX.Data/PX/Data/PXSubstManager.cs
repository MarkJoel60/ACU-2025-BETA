// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSubstManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using PX.Api;
using PX.Common;
using PX.DbServices.Utils;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

/// <summary>The substitution manager.</summary>
public class PXSubstManager
{
  private static Hashtable graphs_;
  private static Hashtable dynamicGraphs_ = (Hashtable) null;
  private static object dynamicGraphsLock = new object();
  private static PXSubstManager.PXTypeCache _cache = (PXSubstManager.PXTypeCache) null;

  /// <summary>Static ctor that initializes the substitution engine</summary>
  static PXSubstManager()
  {
    PXSubstManager.graphs_ = new Hashtable();
    PXSubstManager.InitTypeCache();
    PXSubstManager.Refresh();
    if (!WebConfig.UseRuntimeCompilation)
      return;
    PXCodeDirectoryCompiler.NotifyOnChange(new System.Action(PXSubstManager.ClearDynamicGraphs));
  }

  private static void InitTypeCache()
  {
    PXSubstManager._cache = new PXSubstManager.PXTypeCache();
    PXSubstManager._cache.Init();
  }

  public static void AddTypeToNamedList(string key, System.Type t)
  {
    PXSubstManager._cache.Add(key, t);
  }

  public static void SaveTypeCache(string key) => PXSubstManager._cache.Save(key);

  public static IEnumerable<System.Type> EnumTypesInAssemblies(
    string key,
    List<Exception> logFailedTypes = null)
  {
    return PXSubstManager.EnumTypesInAssemblies(key, logFailedTypes, false, (Assembly[]) null);
  }

  public static IEnumerable<System.Type> EnumTypesInAssemblies(
    string key,
    bool internalsVisible,
    List<Exception> logFailedTypes = null)
  {
    return PXSubstManager.EnumTypesInAssemblies(key, logFailedTypes, internalsVisible, (Assembly[]) null);
  }

  public static IEnumerable<System.Type> EnumTypesInAssemblies(
    string key,
    List<Exception> logFailedTypes,
    bool internalsVisible,
    params Assembly[] assemblies)
  {
    lock (PXSubstManager.PXTypeCache.cacheLock.GetOrAdd(key, new object()))
    {
      if (!string.IsNullOrEmpty(key))
      {
        if (!PXSubstManager._cache.cachedAssemblies.ContainsKey(key))
        {
          PXSubstManager.PXTypeCache.PXCachedAssemblyTypes cachedAssemblyTypes = PXSubstManager._cache.Load(key);
          if (cachedAssemblyTypes != null)
            PXSubstManager._cache.cachedAssemblies[key] = cachedAssemblyTypes;
        }
      }
    }
    PXSubstManager.PXTypeCache.PXCachedAssemblyTypes cachedAssemblyTypes1;
    return !string.IsNullOrEmpty(key) && PXSubstManager._cache.cachedAssemblies.TryGetValue(key, out cachedAssemblyTypes1) && !cachedAssemblyTypes1.IsNew && cachedAssemblyTypes1.OutdatedAssemblies.Count == 0 ? (IEnumerable<System.Type>) PXSubstManager._cache.Get(key).ToList<System.Type>() : PXSubstManager.GetSubstituteTypesInternal(key, logFailedTypes, internalsVisible, assemblies);
  }

  private static IEnumerable<System.Type> GetSubstituteTypesInternal(
    string key,
    List<Exception> logFailedTypes,
    bool internalsVisible,
    params Assembly[] assemblies)
  {
    List<Assembly> processed = new List<Assembly>();
    Assembly[] asm = new Assembly[0];
    if (!string.IsNullOrEmpty(key))
      PXSubstManager._cache.Add(key);
    while (asm.Length < (asm = assemblies ?? AppDomain.CurrentDomain.GetAssemblies()).Length)
    {
      for (int i = 0; i < asm.Length; ++i)
      {
        Assembly assembly = asm[i];
        if (PXSubstManager.IsSuitableTypeExportAssembly(assembly) && !processed.Contains(assembly))
        {
          PXSubstManager.PXTypeCache.PXCachedAssemblyTypes cachedAssemblyTypes;
          if (!string.IsNullOrEmpty(key) && PXSubstManager._cache.assembliesHash.ContainsKey(assembly.FullName) && PXSubstManager._cache.cachedAssemblies.TryGetValue(key, out cachedAssemblyTypes) && !cachedAssemblyTypes.IsNew && !cachedAssemblyTypes.OutdatedAssemblies.Contains(assembly))
          {
            processed.Add(assembly);
            if (cachedAssemblyTypes.ContainsKey(assembly.FullName))
            {
              foreach (KeyValuePair<System.Type, int> keyValuePair in (IEnumerable<KeyValuePair<System.Type, int>>) cachedAssemblyTypes[assembly.FullName].OrderBy<KeyValuePair<System.Type, int>, int>((Func<KeyValuePair<System.Type, int>, int>) (t => t.Value)))
                yield return keyValuePair.Key;
            }
          }
          else
          {
            List<System.Type> typeList = new List<System.Type>();
            try
            {
              System.Type[] typeArray = (System.Type[]) null;
              try
              {
                if (!assembly.IsDynamic)
                  typeArray = !internalsVisible ? assembly.GetExportedTypes() : TypeLoaderExtensions.GetLoadableTypes(assembly).ToArray<System.Type>();
              }
              catch (ReflectionTypeLoadException ex)
              {
                typeArray = ex.Types;
              }
              if (typeArray != null)
              {
                foreach (System.Type type in typeArray)
                {
                  if (type != (System.Type) null)
                    typeList.Add(type);
                }
              }
            }
            catch (Exception ex)
            {
              logFailedTypes?.Add((Exception) new PXException($"Failed to load types from assembly {assembly.FullName} with message: {ex.Message}", ex));
            }
            if (!string.IsNullOrEmpty(key))
              PXSubstManager._cache.Add(key, assembly);
            processed.Add(assembly);
            foreach (System.Type type in typeList)
              yield return type;
          }
        }
      }
    }
  }

  /// <summary>Returns TRUE when assembly could export some types.</summary>
  public static bool IsSuitableTypeExportAssembly(Assembly a, bool ignoreCustomizationTypes)
  {
    return ReflectionUtils.IsSuitableTypeExportAssembly(a, ignoreCustomizationTypes);
  }

  /// <summary>Returns TRUE when assembly could export some types.</summary>
  public static bool IsSuitableTypeExportAssembly(Assembly a)
  {
    return PXSubstManager.IsSuitableTypeExportAssembly(a, false);
  }

  /// <summary>Refreshes the substitution classes (design time only).</summary>
  public static void Refresh()
  {
    PXSubstManager.graphs_.Clear();
    bool flag1 = false;
    bool flag2 = false;
    List<System.Type> typeList = new List<System.Type>();
    try
    {
      bool flag3 = false;
      foreach (System.Type enumTypesInAssembly in PXSubstManager.EnumTypesInAssemblies("PXSubstManager.Refresh"))
      {
        if (enumTypesInAssembly != (System.Type) null && enumTypesInAssembly.IsClass && !enumTypesInAssembly.IsAbstract && enumTypesInAssembly.IsPublic && typeof (IBqlTable).IsAssignableFrom(enumTypesInAssembly))
        {
          if (!flag3)
          {
            System.Type type = System.Type.GetType(enumTypesInAssembly.FullName);
            if (type == (System.Type) null)
            {
              if (!flag1)
              {
                try
                {
                  flag1 = true;
                  flag2 = (bool) typeof (BuildManager).GetField("_theBuildManagerInitialized", BindingFlags.Static | BindingFlags.NonPublic).GetValue((object) null);
                }
                catch
                {
                  flag2 = true;
                }
              }
              if (flag2)
                type = PXBuildManager.GetType(enumTypesInAssembly.FullName, false);
            }
            if (!(type != enumTypesInAssembly))
              flag3 = true;
            else
              break;
          }
          typeList.Add(enumTypesInAssembly);
          PXSubstManager.AddTypeToNamedList("PXSubstManager.Refresh", enumTypesInAssembly);
        }
      }
      PXSubstManager.SaveTypeCache("PXSubstManager.Refresh");
    }
    catch
    {
    }
    foreach (System.Type t in typeList)
      PXSubstManager.Register(t, PXSubstManager.graphs_);
  }

  /// <summary>Returns all types substituted for t.</summary>
  public static IEnumerable<System.Type> GetSubstitutedTypes(System.Type t, System.Type graphType)
  {
    foreach (Hashtable substitutionSource in PXSubstManager.GetSubstitutionSources(graphType))
    {
      List<System.Type> substitutedTypes = new List<System.Type>();
      foreach (DictionaryEntry dictionaryEntry in substitutionSource)
      {
        System.Type type = dictionaryEntry.Value as System.Type;
        if ((object) type != null && type == t)
        {
          System.Type key = dictionaryEntry.Key as System.Type;
          if ((object) key != null)
            substitutedTypes.Add(key);
        }
      }
      if (substitutedTypes.Count > 0)
        return (IEnumerable<System.Type>) substitutedTypes;
    }
    return (IEnumerable<System.Type>) new List<System.Type>() { t };
  }

  /// <summary>Returns the substitution class.</summary>
  public static System.Type Substitute(System.Type t, System.Type graphType)
  {
    foreach (Hashtable substitutionSource in PXSubstManager.GetSubstitutionSources(graphType))
    {
      System.Type type = (System.Type) substitutionSource[(object) t];
      if (type != (System.Type) null)
        return type;
    }
    return t;
  }

  /// <summary>Returns substituion tables for specific graph, root graph and dynamic</summary>
  private static IEnumerable<Hashtable> GetSubstitutionSources(System.Type graphType)
  {
    Hashtable subst = (Hashtable) PXSubstManager.graphs_[(object) graphType];
    if (subst == null && CustomizedTypeManager.IsCustomizedType(graphType))
      subst = (Hashtable) PXSubstManager.graphs_[(object) graphType.BaseType];
    if (subst != null)
      yield return subst;
    if (graphType != typeof (object))
      subst = (Hashtable) PXSubstManager.graphs_[(object) typeof (object)];
    if (subst != null)
      yield return subst;
    if (WebConfig.UseRuntimeCompilation)
    {
      Hashtable graphs = PXSubstManager.dynamicGraphs_;
      if (graphs == null)
      {
        lock (PXSubstManager.dynamicGraphsLock)
        {
          System.Type[] array = PXCodeDirectoryCompiler.GetCompiledTypes<IBqlTable>().ToArray();
          graphs = new Hashtable();
          foreach (System.Type t in array)
            PXSubstManager.Register(t, graphs);
          PXSubstManager.dynamicGraphs_ = graphs;
        }
      }
      Hashtable substitutionSource = (Hashtable) graphs[(object) graphType];
      if (substitutionSource != null)
        yield return substitutionSource;
    }
  }

  private static void ClearDynamicGraphs()
  {
    lock (PXSubstManager.dynamicGraphsLock)
      PXSubstManager.dynamicGraphs_ = (Hashtable) null;
  }

  /// <summary>
  /// Registers specified class into the substitution engine
  /// </summary>
  private static void Register(System.Type t, Hashtable graphs)
  {
    foreach (PXSubstituteAttribute customAttribute in t.GetCustomAttributes(typeof (PXSubstituteAttribute), false))
    {
      System.Type key = customAttribute.GraphType;
      System.Type parentType = customAttribute.ParentType;
      if (key == (System.Type) null)
        key = typeof (object);
      for (System.Type type = t; type != (System.Type) null && type != parentType && typeof (IBqlTable).IsAssignableFrom(type); type = type.BaseType)
      {
        Hashtable hashtable = (Hashtable) graphs[(object) key];
        if (hashtable == null)
          graphs[(object) key] = (object) (hashtable = new Hashtable());
        else if (type == t && hashtable[(object) type] != null)
          break;
        hashtable[(object) type] = (object) t;
      }
    }
  }

  internal class PXTypeCache
  {
    internal ConcurrentDictionary<string, PXSubstManager.PXTypeCache.PXCachedAssemblyTypes> cachedAssemblies;
    internal ConcurrentDictionary<string, string> assembliesHash;
    private string filePath;
    private string binPath;
    private static bool debugVersion;
    internal static ConcurrentDictionary<string, object> cacheLock = new ConcurrentDictionary<string, object>();

    internal void Init()
    {
      object data = AppDomain.CurrentDomain.GetData("DataDirectory");
      this.filePath = data == null ? AppDomain.CurrentDomain.BaseDirectory : data.ToString();
      this.filePath = Path.Combine(this.filePath, "TypeCache");
      this.binPath = data == null ? AppDomain.CurrentDomain.BaseDirectory : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
      if (!Directory.Exists(this.filePath))
        Directory.CreateDirectory(this.filePath);
      this.assembliesHash = new ConcurrentDictionary<string, string>();
      this.cachedAssemblies = new ConcurrentDictionary<string, PXSubstManager.PXTypeCache.PXCachedAssemblyTypes>();
      if (!WebConfig.UseTypeListDebugMode)
        return;
      string path = Path.Combine(this.filePath, "debug_types_cache.xml");
      if (File.Exists(path))
        PXSubstManager.PXTypeCache.debugVersion = true;
      else
        File.Create(path);
    }

    internal PXSubstManager.PXTypeCache.PXCachedAssemblyTypes Load(string key)
    {
      foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
      {
        if (PXSubstManager.IsSuitableTypeExportAssembly(assembly) && !this.assembliesHash.ContainsKey(assembly.FullName))
          this.assembliesHash[assembly.FullName] = this.GetAssemblyHash(assembly);
      }
      string path = Path.Combine(this.filePath, this.ConvertFilePath(key) + ".cache");
      if (File.Exists(path))
      {
        try
        {
          PXSubstManager.PXTypeCache.PXCachedAssemblyTypesStorage[] source = JsonConvert.DeserializeObject<PXSubstManager.PXTypeCache.PXCachedAssemblyTypesStorage[]>(File.ReadAllText(path));
          HashSet<Assembly> assemblySet = new HashSet<Assembly>();
          HashSet<string> tempOutdatedAssemblies = new HashSet<string>();
          if (!PXSubstManager.PXTypeCache.debugVersion)
          {
            HashSet<string> cachedAssembliesHash = ((IEnumerable<PXSubstManager.PXTypeCache.PXCachedAssemblyTypesStorage>) source).Select<PXSubstManager.PXTypeCache.PXCachedAssemblyTypesStorage, string>((Func<PXSubstManager.PXTypeCache.PXCachedAssemblyTypesStorage, string>) (_ => _.Name)).ToHashSet<string>();
            tempOutdatedAssemblies = ((IEnumerable<PXSubstManager.PXTypeCache.PXCachedAssemblyTypesStorage>) source).Where<PXSubstManager.PXTypeCache.PXCachedAssemblyTypesStorage>((Func<PXSubstManager.PXTypeCache.PXCachedAssemblyTypesStorage, bool>) (_ => !this.assembliesHash.ContainsKey(_.Name) || !this.assembliesHash[_.Name].Equals(_.Hash))).Select<PXSubstManager.PXTypeCache.PXCachedAssemblyTypesStorage, string>((Func<PXSubstManager.PXTypeCache.PXCachedAssemblyTypesStorage, string>) (_ => _.Name)).ToHashSet<string>();
            HashSet<string> hashSet = this.assembliesHash.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (_ => !cachedAssembliesHash.Contains(_.Key))).Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (_ => _.Key)).ToHashSet<string>();
            tempOutdatedAssemblies = tempOutdatedAssemblies.Union<string>((IEnumerable<string>) hashSet).ToHashSet<string>();
            assemblySet = ((IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies()).Where<Assembly>((Func<Assembly, bool>) (_ => tempOutdatedAssemblies.Contains(_.FullName))).ToHashSet<Assembly>();
          }
          PXSubstManager.PXTypeCache.PXCachedAssemblyTypes cachedAssemblyTypes = new PXSubstManager.PXTypeCache.PXCachedAssemblyTypes(key)
          {
            OutdatedAssemblies = assemblySet
          };
          foreach (PXSubstManager.PXTypeCache.PXCachedAssemblyTypesStorage assemblyTypesStorage in source)
          {
            if (tempOutdatedAssemblies.Contains(assemblyTypesStorage.Name))
            {
              cachedAssemblyTypes.IsDirty = true;
            }
            else
            {
              cachedAssemblyTypes[assemblyTypesStorage.Name] = new ConcurrentDictionary<System.Type, int>();
              if (assemblyTypesStorage.Types != null && assemblyTypesStorage.Types.Length != 0)
              {
                for (int index = 0; index < assemblyTypesStorage.Types.Length; ++index)
                {
                  System.Type type = System.Type.GetType(assemblyTypesStorage.Types[index], false);
                  if (type != (System.Type) null)
                    cachedAssemblyTypes[assemblyTypesStorage.Name].TryAdd(type, index);
                }
              }
            }
          }
          return cachedAssemblyTypes;
        }
        catch
        {
        }
      }
      return (PXSubstManager.PXTypeCache.PXCachedAssemblyTypes) null;
    }

    internal void Save(string key)
    {
      lock (PXSubstManager.PXTypeCache.cacheLock.GetOrAdd(key, new object()))
      {
        PXSubstManager.PXTypeCache.PXCachedAssemblyTypes cachedTypes = (PXSubstManager.PXTypeCache.PXCachedAssemblyTypes) null;
        if (!this.cachedAssemblies.TryGetValue(key, out cachedTypes) || !cachedTypes.IsDirty)
          return;
        cachedTypes.IsDirty = false;
        cachedTypes.IsNew = false;
        this.SaveInternal(cachedTypes);
      }
    }

    private void SaveInternal(
      PXSubstManager.PXTypeCache.PXCachedAssemblyTypes cachedTypes)
    {
      try
      {
        File.WriteAllText(Path.Combine(this.filePath, this.ConvertFilePath(cachedTypes.Key) + ".cache"), JsonConvert.SerializeObject((object) cachedTypes.Select<KeyValuePair<string, ConcurrentDictionary<System.Type, int>>, PXSubstManager.PXTypeCache.PXCachedAssemblyTypesStorage>((Func<KeyValuePair<string, ConcurrentDictionary<System.Type, int>>, PXSubstManager.PXTypeCache.PXCachedAssemblyTypesStorage>) (_ => new PXSubstManager.PXTypeCache.PXCachedAssemblyTypesStorage()
        {
          Name = _.Key,
          Hash = this.assembliesHash[_.Key],
          Types = _.Value.Count == 0 ? (string[]) null : _.Value.OrderBy<KeyValuePair<System.Type, int>, int>((Func<KeyValuePair<System.Type, int>, int>) (t => t.Value)).Select<KeyValuePair<System.Type, int>, string>((Func<KeyValuePair<System.Type, int>, string>) (t => t.Key.AssemblyQualifiedName)).ToArray<string>()
        })).OrderByDescending<PXSubstManager.PXTypeCache.PXCachedAssemblyTypesStorage, int>((Func<PXSubstManager.PXTypeCache.PXCachedAssemblyTypesStorage, int>) (_ => _.Types != null ? _.Types.Length : 0)).ToArray<PXSubstManager.PXTypeCache.PXCachedAssemblyTypesStorage>(), (Formatting) 1));
      }
      catch
      {
      }
    }

    internal PXSubstManager.PXTypeCache.PXCachedAssemblyTypes Add(string key)
    {
      return this.cachedAssemblies.GetOrAdd(key, (Func<string, PXSubstManager.PXTypeCache.PXCachedAssemblyTypes>) (_ => new PXSubstManager.PXTypeCache.PXCachedAssemblyTypes(key)
      {
        IsDirty = true,
        IsNew = true
      }));
    }

    internal ConcurrentDictionary<System.Type, int> Add(string key, Assembly assembly)
    {
      PXSubstManager.PXTypeCache.PXCachedAssemblyTypes assemblies = this.Add(key);
      return this.assembliesHash.ContainsKey(assembly.FullName) ? assemblies.GetOrAdd(assembly.FullName, (Func<string, ConcurrentDictionary<System.Type, int>>) (_ =>
      {
        ConcurrentDictionary<System.Type, int> concurrentDictionary = new ConcurrentDictionary<System.Type, int>();
        assemblies.IsDirty = true;
        return concurrentDictionary;
      })) : (ConcurrentDictionary<System.Type, int>) null;
    }

    internal void Add(string key, System.Type type)
    {
      ConcurrentDictionary<System.Type, int> concurrentDictionary = this.Add(key, type.Assembly);
      if (concurrentDictionary == null || !concurrentDictionary.TryAdd(type, concurrentDictionary.Count))
        return;
      this.cachedAssemblies[key].IsDirty = true;
    }

    internal IEnumerable<System.Type> Get(string key)
    {
      if (this.cachedAssemblies.ContainsKey(key))
      {
        foreach (KeyValuePair<string, ConcurrentDictionary<System.Type, int>> keyValuePair1 in (ConcurrentDictionary<string, ConcurrentDictionary<System.Type, int>>) this.cachedAssemblies[key])
        {
          foreach (KeyValuePair<System.Type, int> keyValuePair2 in (IEnumerable<KeyValuePair<System.Type, int>>) keyValuePair1.Value.OrderBy<KeyValuePair<System.Type, int>, int>((Func<KeyValuePair<System.Type, int>, int>) (t => t.Value)))
            yield return keyValuePair2.Key;
        }
      }
    }

    private string GetAssemblyHash(Assembly assembly)
    {
      string fileStamp = this.GetFileStamp(assembly.Location);
      string str = Path.Combine(this.binPath, Path.GetFileName(assembly.Location));
      if (File.Exists(str))
        fileStamp = this.GetFileStamp(str);
      return fileStamp;
    }

    private string GetFileStamp(string filePath)
    {
      if (!File.Exists(filePath))
        return string.Empty;
      System.DateTime creationTimeUtc = File.GetCreationTimeUtc(filePath);
      System.DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(filePath);
      return $"{creationTimeUtc.Ticks.ToString()}|{lastWriteTimeUtc.Ticks.ToString()}";
    }

    private string ConvertFilePath(string key)
    {
      return MainTools.Replace(key, new char[2]{ '<', '>' }, '_');
    }

    public class PXCachedAssemblyTypesStorage
    {
      public string Name;
      public string Hash;
      public string[] Types;
    }

    internal class PXCachedAssemblyTypes : 
      ConcurrentDictionary<string, ConcurrentDictionary<System.Type, int>>
    {
      internal bool IsDirty;
      internal bool IsNew;
      internal string Key;
      internal HashSet<Assembly> OutdatedAssemblies = new HashSet<Assembly>();

      internal PXCachedAssemblyTypes(string key) => this.Key = key;
    }
  }
}
