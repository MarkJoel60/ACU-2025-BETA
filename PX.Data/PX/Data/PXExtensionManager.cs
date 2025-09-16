// Decompiled with JetBrains decompiler
// Type: PX.Data.PXExtensionManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Common.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Permissions;
using System.Threading;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

/// <exclude />
internal static class PXExtensionManager
{
  public static ReaderWriterLock _StaticInfoLock = new ReaderWriterLock();
  public static readonly Dictionary<System.Type, Dictionary<PXExtensionManager.ListOfTypes, PXCache.CacheStaticInfoBase>> _CacheStaticInfo = new Dictionary<System.Type, Dictionary<PXExtensionManager.ListOfTypes, PXCache.CacheStaticInfoBase>>();
  public static readonly Dictionary<System.Type, Dictionary<PXExtensionManager.ListOfTypes, PXGraph.GraphStaticInfo>> _GraphStaticInfo = new Dictionary<System.Type, Dictionary<PXExtensionManager.ListOfTypes, PXGraph.GraphStaticInfo>>();
  public static readonly Dictionary<System.Type, Dictionary<System.Type, Dictionary<PXExtensionManager.ListOfTypes, PXCache.AlteredDescriptor>>> _AttributesStaticInfo = new Dictionary<System.Type, Dictionary<System.Type, Dictionary<PXExtensionManager.ListOfTypes, PXCache.AlteredDescriptor>>>();
  private static readonly Dictionary<System.Type, System.Type> _AvailableExtensions = EnumerableExtensions.ToDictionary<System.Type, System.Type>(PXExtensionManager.FindGraphExtensions(PXExtensionManager.FindExtensions()).Where<KeyValuePair<System.Type, System.Type>>((Func<KeyValuePair<System.Type, System.Type>, bool>) (_ => !_.Key.Assembly.FullName.StartsWith("RuntimeCode_"))));
  private static Dictionary<System.Type, System.Type> _DynamicExtensions;
  private static bool _initDynamicExtensionsWatcher;
  internal static readonly Dictionary<System.Type, Dictionary<PXExtensionManager.ListOfTypes, System.Type>> Wrappers = new Dictionary<System.Type, Dictionary<PXExtensionManager.ListOfTypes, System.Type>>();
  private static readonly Dictionary<System.Type, System.Action<object>> InitDelegates = new Dictionary<System.Type, System.Action<object>>();

  internal static IEnumerable<KeyValuePair<System.Type, System.Type>> GetAllExtensions()
  {
    if (!WebConfig.DisableExtensions)
    {
      if (PXExtensionManager._DynamicExtensions == null)
        PXExtensionManager._DynamicExtensions = PXExtensionManager.FindGraphExtensions(PXCodeDirectoryCompiler.GetCompiledTypes<PXGraphExtension>().ToArray());
      Dictionary<System.Type, System.Type> tempExtension = PXExtensionManager._DynamicExtensions;
      if (!PXExtensionManager._initDynamicExtensionsWatcher)
      {
        PXExtensionManager._initDynamicExtensionsWatcher = true;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        PXCodeDirectoryCompiler.NotifyOnChange(PXExtensionManager.\u003C\u003EO.\u003C0\u003E__ClearCaches ?? (PXExtensionManager.\u003C\u003EO.\u003C0\u003E__ClearCaches = new System.Action(PXExtensionManager.ClearCaches)));
      }
      foreach (KeyValuePair<System.Type, System.Type> availableExtension in PXExtensionManager._AvailableExtensions)
        yield return availableExtension;
      if (tempExtension != null)
      {
        foreach (KeyValuePair<System.Type, System.Type> allExtension in tempExtension)
          yield return allExtension;
      }
    }
  }

  public static bool IsGraphExtensionGenericTemplate(System.Type graphExtType)
  {
    return ((IEnumerable<System.Type>) graphExtType.GetGenericArguments()).Any<System.Type>() && ((IEnumerable<System.Type>) graphExtType.GetGenericArguments()).Where<System.Type>((Func<System.Type, bool>) (ga => ((IEnumerable<System.Type>) ga.GetGenericParameterConstraints()).Any<System.Type>((Func<System.Type, bool>) (c =>
    {
      if (!c.IsClass)
        return false;
      if (c.Name == typeof (PXGraphExtension).Name && c.BaseType == typeof (PXGraphExtension).BaseType || c.Name == typeof (PXGraphExtension<>).Name && c.BaseType == typeof (PXGraphExtension<>).BaseType || c.Name == typeof (PXGraph).Name && c.BaseType == typeof (PXGraph).BaseType)
        return true;
      return c.Name == typeof (PXGraph<>).Name && c.BaseType == typeof (PXGraph<>).BaseType;
    })))).All<System.Type>((Func<System.Type, bool>) (a => ((IEnumerable<System.Type>) graphExtType.BaseType.GetGenericArguments()).Any<System.Type>((Func<System.Type, bool>) (ba => ba == a))));
  }

  internal static void ResetFeatures() => PXExtensionManager.ResetFeatures(SlotStore.Instance);

  internal static void ResetFeatures(ISlotStore slots)
  {
    PXDatabase.ResetSlot<PXGraph.GraphStaticInfoDictionary>("GraphStaticInfo", typeof (PXGraph.FeaturesSet));
    TypeKeyedOperationExtensions.Set<PXGraph.GraphStaticInfoDictionary>(slots, (PXGraph.GraphStaticInfoDictionary) null);
    PXDatabase.ResetSlot<PXCache.CacheStaticInfoDictionary>("CacheStaticInfo", typeof (PXGraph.FeaturesSet));
    TypeKeyedOperationExtensions.Set<PXCache.CacheStaticInfoDictionary>(slots, (PXCache.CacheStaticInfoDictionary) null);
  }

  private static void ClearCaches()
  {
    PXExtensionManager._DynamicExtensions = (Dictionary<System.Type, System.Type>) null;
    PXBuildManager.ClearTypeCache();
    lock (PXExtensionManager.Wrappers)
      PXExtensionManager.Wrappers.Clear();
    InterfaceWrapper.ClearCaches();
    PXDatabase.ResetSlotForAllCompanies("GraphStaticInfo", typeof (PXGraph.FeaturesSet));
    lock (PXExtensionManager._StaticInfoLock)
      PXExtensionManager._GraphStaticInfo.Clear();
  }

  private static System.Type[] FindExtensions()
  {
    List<System.Type> ret = new List<System.Type>();
    PXExtensionManager.FindExtensionsInternal(ret);
    return ret.ToArray();
  }

  private static Dictionary<System.Type, System.Type> FindGraphExtensions(System.Type[] extensions)
  {
    Dictionary<System.Type, System.Type> graphExtensions = new Dictionary<System.Type, System.Type>();
    foreach (System.Type extension in extensions)
    {
      System.Type extendedGraphType = PXExtensionManager.GetExtendedGraphType(extension, out bool _);
      if (extendedGraphType != (System.Type) null)
        graphExtensions.Add(extension, extendedGraphType);
    }
    return graphExtensions;
  }

  internal static void FindExtensionsInternal(List<System.Type> ret, List<Exception> logFailedTypes = null)
  {
    foreach (System.Type enumTypesInAssembly in PXSubstManager.EnumTypesInAssemblies(nameof (FindExtensionsInternal), logFailedTypes))
    {
      if (PXExtensionManager.GetExtendedGraphType(enumTypesInAssembly, out bool _) != (System.Type) null && (!enumTypesInAssembly.IsGenericType || enumTypesInAssembly.IsConstructedGenericType))
      {
        PXSubstManager.AddTypeToNamedList(nameof (FindExtensionsInternal), enumTypesInAssembly);
        ret.Add(enumTypesInAssembly);
      }
    }
    PXSubstManager.SaveTypeCache(nameof (FindExtensionsInternal));
  }

  public static System.Type GetDeepestGenericBase(System.Type t)
  {
    System.Type baseType = t.BaseType;
    while (baseType.BaseType != (System.Type) null && baseType.BaseType.IsGenericType)
      baseType = baseType.BaseType;
    return baseType;
  }

  /// <summary>
  /// For <see cref="T:PX.Data.PXGraphExtension" /> or <see cref="T:PX.Data.PXCacheExtension" />, returns the original type they are extending.
  /// </summary>
  /// <param name="extension">Extension type.</param>
  /// <returns>The original type being extended; <see langword="null" /> otherwise.
  /// If <paramref name="extension" /> is not an extension type, <see langword="null" /> is returned.</returns>
  public static System.Type GetExtendedType(System.Type extension)
  {
    System.Type firstExtensionParent = PXExtensionManager.GetFirstExtensionParent(extension);
    return !firstExtensionParent.IsGenericType || firstExtensionParent.IsGenericTypeDefinition ? (System.Type) null : ((IEnumerable<System.Type>) firstExtensionParent.GetGenericArguments()).Last<System.Type>();
  }

  public static System.Type GetFirstExtensionParent(System.Type extension)
  {
    System.Type firstExtensionParent;
    for (firstExtensionParent = extension; firstExtensionParent.BaseType != (System.Type) null; firstExtensionParent = firstExtensionParent.BaseType)
    {
      string str = firstExtensionParent.Name;
      int length = str.IndexOf('`');
      if (length != -1)
        str = str.Substring(0, length);
      if (str == typeof (PXGraphExtension).Name || str == typeof (PXCacheExtension).Name)
        break;
    }
    return firstExtensionParent;
  }

  public static System.Type GetExtendedGraphType(System.Type extension, out bool secondLevel)
  {
    secondLevel = false;
    if (extension.BaseType == (System.Type) null || !extension.BaseType.IsSubclassOf(typeof (PXGraphExtension)))
      return (System.Type) null;
    if (extension.IsAbstract && !extension.IsDefined(typeof (PXProtectedAccessAttribute)))
      return (System.Type) null;
    System.Type firstExtensionParent = PXExtensionManager.GetFirstExtensionParent(extension);
    secondLevel = firstExtensionParent != extension.BaseType;
    if (!firstExtensionParent.IsGenericType)
      return (System.Type) null;
    System.Type[] genericArguments = firstExtensionParent.GetGenericArguments();
    return genericArguments[genericArguments.Length - 1];
  }

  public static List<System.Type> GetExtensions(System.Type tgraph, bool checkActive)
  {
    return PXExtensionManager.GetExtensions(tgraph, checkActive, out Dictionary<string, System.Type> _, out Dictionary<string, string> _);
  }

  private static bool IsDisabled(System.Type graphType, System.Type extType)
  {
    MethodInfo isActiveMethod1 = GetIsActiveMethod("IsActive");
    if (isActiveMethod1 != (MethodInfo) null && isActiveMethod1.ReturnType == typeof (bool) && isActiveMethod1.Invoke((object) null, new object[0]) is bool flag1 && !flag1)
      return true;
    MethodInfo isActiveMethod2 = GetIsActiveMethod("IsActiveForGraph");
    if (isActiveMethod2 != (MethodInfo) null && isActiveMethod2.ReturnType == typeof (bool) && isActiveMethod2.IsGenericMethod && isActiveMethod2.GetGenericArguments().Length == 1)
    {
      System.Type typeNotCustomized = CustomizedTypeManager.GetTypeNotCustomized(graphType);
      if (typeNotCustomized == (System.Type) null)
        return false;
      if (isActiveMethod2.MakeGenericMethod(typeNotCustomized).Invoke((object) null, new object[0]) is bool flag2 && !flag2)
        return true;
    }
    return false;

    MethodInfo GetIsActiveMethod(string name)
    {
      return extType.GetMethod(name, BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public, (Binder) null, new System.Type[0], (ParameterModifier[]) null);
    }
  }

  internal static List<System.Type> GetExtensions(
    System.Type tgraph,
    bool checkActive,
    out Dictionary<string, System.Type> inactiveViews,
    out Dictionary<string, string> inactiveActions)
  {
    inactiveViews = new Dictionary<string, System.Type>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    inactiveActions = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    List<System.Type> list = new List<System.Type>();
    foreach (KeyValuePair<System.Type, System.Type> allExtension in PXExtensionManager.GetAllExtensions())
    {
      System.Type key = allExtension.Key;
      if (allExtension.Value.IsAssignableFrom(tgraph))
      {
        if (checkActive)
        {
          try
          {
            if (PXExtensionManager.IsDisabled(tgraph, key))
            {
              foreach (System.Reflection.FieldInfo field in key.GetFields(BindingFlags.Instance | BindingFlags.Public))
              {
                if (typeof (PXSelectBase).IsAssignableFrom(field.FieldType))
                  inactiveViews[field.Name] = field.FieldType;
                else if (typeof (PXAction).IsAssignableFrom(field.FieldType))
                  inactiveActions[field.Name] = new PXActionInfo(field).DisplayName;
              }
              continue;
            }
            if (PXCache._mapping.IsDisabled(key))
              continue;
          }
          catch
          {
          }
        }
        list.Add(key);
      }
    }
    return PXExtensionManager.Sort(list);
  }

  public static List<System.Type> Sort(List<System.Type> list)
  {
    list.Sort((Comparison<System.Type>) ((a, b) => string.Compare(a.FullName, b.FullName, StringComparison.InvariantCulture)));
    PXBuildManager.SortExts(list);
    HashSet<System.Type> visited = new HashSet<System.Type>();
    List<System.Type> parent = new List<System.Type>();
    List<System.Type> sorted = new List<System.Type>();
    Dictionary<System.Type, List<System.Type>> ascendants = new Dictionary<System.Type, List<System.Type>>();
    foreach (System.Type extension in list)
    {
      System.Type[] genericArguments = PXExtensionManager.GetFirstExtensionParent(extension).GetGenericArguments();
      if (genericArguments.Length != 0)
      {
        System.Type key = genericArguments[genericArguments.Length - 1];
        List<System.Type> typeList;
        if (!ascendants.TryGetValue(key, out typeList))
          ascendants[key] = typeList = new List<System.Type>();
        typeList.Add(extension);
      }
    }
    foreach (System.Type type in list)
      PXExtensionManager.visit(type, visited, parent, sorted, ascendants);
    return sorted;
  }

  private static void visit(
    System.Type item,
    HashSet<System.Type> visited,
    List<System.Type> parent,
    List<System.Type> sorted,
    Dictionary<System.Type, List<System.Type>> ascendants)
  {
    int start = parent.LastIndexOf(item);
    if (start >= 0)
      throw new PXException($"When extending a cache, cross references are not permitted: {string.Join<System.Type>(" -> ", Enumerable.Range(start, parent.Count - start).Select<int, System.Type>((Func<int, System.Type>) (i => parent[i])).Concat<System.Type>((IEnumerable<System.Type>) new System.Type[1]
      {
        item
      }))}.");
    parent.Add(item);
    bool flag = false;
    if (!visited.Contains(item))
    {
      visited.Add(item);
      flag = true;
    }
    System.Type[] genericArguments = PXExtensionManager.GetFirstExtensionParent(item).GetGenericArguments();
    for (System.Type key = genericArguments.Length != 0 ? genericArguments[genericArguments.Length - 1].BaseType : typeof (object); key != typeof (object); key = key.BaseType)
    {
      List<System.Type> typeList;
      if (ascendants.TryGetValue(key, out typeList))
      {
        foreach (System.Type type in typeList)
          PXExtensionManager.visit(type, visited, parent, sorted, ascendants);
      }
    }
    for (int index = 0; index < genericArguments.Length - 1; ++index)
      PXExtensionManager.visit(genericArguments[index], visited, parent, sorted, ascendants);
    if (flag)
      sorted.Add(item);
    parent.RemoveAt(parent.Count - 1);
  }

  private static Dictionary<System.Type, HashSet<System.Type>> GetDependencyDict(
    List<System.Type> extensions)
  {
    Dictionary<System.Type, HashSet<System.Type>> dependencyDict = new Dictionary<System.Type, HashSet<System.Type>>();
    List<System.Type> typeList;
    for (; extensions.Count > 0; extensions = typeList)
    {
      typeList = new List<System.Type>();
      foreach (System.Type extension in extensions)
      {
        System.Type[] genericArguments = PXExtensionManager.GetFirstExtensionParent(extension).GetGenericArguments();
        string str = string.Join<System.Type>(", ", ((IEnumerable<System.Type>) genericArguments).Take<System.Type>(genericArguments.Length - 1).Where<System.Type>((Func<System.Type, bool>) (mainExtension => mainExtension.IsAbstract && !mainExtension.IsDefined(typeof (PXProtectedAccessAttribute)))));
        if (!string.IsNullOrEmpty(str))
          throw new PXException($"Graph extension {extension} cannot refer to the following abstract graph extensions that are not marked as [PXProtectedAccess]: {str}.");
        foreach (System.Type key in genericArguments)
        {
          HashSet<System.Type> typeSet;
          if (dependencyDict.TryGetValue(key, out typeSet))
          {
            typeSet.Add(extension);
          }
          else
          {
            dependencyDict[key] = new HashSet<System.Type>()
            {
              extension
            };
            typeList.Add(key);
          }
        }
      }
    }
    return dependencyDict;
  }

  internal static System.Type CreateWrapper(System.Type tgraph, List<System.Type> extensions)
  {
    if (!extensions.Any<System.Type>())
      return (System.Type) null;
    if (tgraph.IsSealed)
      return (System.Type) null;
    Dictionary<System.Type, (System.Type, System.Type)> interfaceWrappers = InterfaceWrapper.GetInterfaceWrappers(tgraph, extensions);
    IEnumerable<IGrouping<MethodInfo, MethodInfo>> groupByBaseMethod = PXExtensionManager.GetExtMethodsGroupByBaseMethod(tgraph, extensions);
    return PXExtensionManager.CreateWrapperInternal(tgraph, extensions, groupByBaseMethod, interfaceWrappers);
  }

  internal static System.Type CreateExtensionWrapper(
    System.Type textension,
    List<System.Type> extensions,
    ILookup<MethodInfo, MethodInfo> extMethods)
  {
    if (!extensions.Any<System.Type>())
      return (System.Type) null;
    if (textension.IsSealed)
      return (System.Type) null;
    HashSet<MethodInfo> virtualMethods = ((IEnumerable<MethodInfo>) textension.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)).Where<MethodInfo>((Func<MethodInfo, bool>) (m => m.IsVirtual)).ToHashSet<MethodInfo>();
    Dictionary<System.Type, (System.Type, System.Type)> interfaceWrappers = InterfaceWrapper.GetInterfaceWrappers(textension, extensions);
    IEnumerable<IGrouping<MethodInfo, MethodInfo>> extMethods1 = extMethods.Where<IGrouping<MethodInfo, MethodInfo>>((Func<IGrouping<MethodInfo, MethodInfo>, bool>) (m => virtualMethods.Contains(m.Key)));
    lock (PXExtensionManager.Wrappers)
    {
      PXExtensionManager.ListOfTypes key = new PXExtensionManager.ListOfTypes(extensions);
      Dictionary<PXExtensionManager.ListOfTypes, System.Type> dictionary;
      if (!PXExtensionManager.Wrappers.TryGetValue(textension, out dictionary))
        PXExtensionManager.Wrappers[textension] = dictionary = new Dictionary<PXExtensionManager.ListOfTypes, System.Type>();
      if (dictionary == null)
        return (System.Type) null;
      System.Type extensionWrapper;
      if (dictionary.TryGetValue(key, out extensionWrapper))
        return extensionWrapper;
      System.Type wrapperInternal = PXExtensionManager.CreateWrapperInternal(textension, extensions, extMethods1, interfaceWrappers);
      dictionary.Add(key, wrapperInternal);
      return wrapperInternal;
    }
  }

  internal static System.Type CreateInterfaceWrapper(System.Type textension, List<System.Type> extensions)
  {
    if (!extensions.Any<System.Type>() || textension.IsSealed)
      return (System.Type) null;
    Dictionary<System.Type, (System.Type, System.Type)> interfaceWrappers = InterfaceWrapper.GetInterfaceWrappers(textension, extensions);
    return PXExtensionManager.CreateWrapperInternal(textension, extensions, (IEnumerable<IGrouping<MethodInfo, MethodInfo>>) null, interfaceWrappers);
  }

  private static System.Type CreateWrapperInternal(
    System.Type tunderlying,
    List<System.Type> extensions,
    IEnumerable<IGrouping<MethodInfo, MethodInfo>> extMethods,
    Dictionary<System.Type, (System.Type, System.Type)> interfaces)
  {
    if (interfaces == null && (!tunderlying.IsAbstract || !tunderlying.IsDefined(typeof (PXProtectedAccessAttribute))) && (extMethods == null || extMethods.Count<IGrouping<MethodInfo, MethodInfo>>() == 0))
      return (System.Type) null;
    AssemblyName name = new AssemblyName()
    {
      Name = tunderlying.Name + "_Container"
    };
    ModuleBuilder moduleBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run).DefineDynamicModule(name.Name);
    string wrapperTypeName = CustomizedTypeManager.GetWrapperTypeName(tunderlying);
    TypeBuilder typeBuilder = moduleBuilder.DefineType(wrapperTypeName, TypeAttributes.Public | TypeAttributes.Sealed, tunderlying);
    foreach (ConstructorInfo constructor in tunderlying.GetConstructors())
    {
      System.Type[] array = ((IEnumerable<ParameterInfo>) constructor.GetParameters()).Select<ParameterInfo, System.Type>((Func<ParameterInfo, System.Type>) (_ => _.ParameterType)).ToArray<System.Type>();
      ILGenerator ilGenerator = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, array).GetILGenerator();
      for (int index = 0; index <= array.Length; ++index)
        ilGenerator.Emit(OpCodes.Ldarg, index);
      ilGenerator.Emit(OpCodes.Call, constructor);
      ilGenerator.Emit(OpCodes.Ret);
    }
    if (extMethods == null || extMethods.Count<IGrouping<MethodInfo, MethodInfo>>() == 0)
    {
      if (interfaces != null)
        InterfaceWrapper.Create(typeBuilder, tunderlying, extensions, interfaces);
      return typeBuilder.CreateType();
    }
    foreach (IGrouping<MethodInfo, MethodInfo> extMethod in extMethods)
    {
      MethodInfo key = extMethod.Key;
      System.Type[] array = ((IEnumerable<System.Type>) new System.Type[1]
      {
        tunderlying
      }).Concat<System.Type>(((IEnumerable<ParameterInfo>) key.GetParameters()).Select<ParameterInfo, System.Type>((Func<ParameterInfo, System.Type>) (info => info.ParameterType))).ToArray<System.Type>();
      MethodBuilder method = typeBuilder.DefineMethod(key.Name + "GeneratedWrapper", MethodAttributes.Private | MethodAttributes.Static, key.ReturnType, array);
      (typeof (PXGraph).IsAssignableFrom(tunderlying) ? PXExtensionManager.BuildGraphMethod(tunderlying, extMethod.Key, extensions, (IEnumerable<MethodInfo>) extMethod) : PXExtensionManager.BuildExtensionMethod(tunderlying, extMethod.Key, extensions, (IEnumerable<MethodInfo>) extMethod)).CompileToMethod(method);
      MethodBuilder methodBuilder = typeBuilder.DefineMethod(key.Name, MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig, key.ReturnType, ((IEnumerable<ParameterInfo>) key.GetParameters()).Select<ParameterInfo, System.Type>((Func<ParameterInfo, System.Type>) (info => info.ParameterType)).ToArray<System.Type>());
      foreach (ParameterInfo parameter in key.GetParameters())
      {
        ParameterBuilder parameterBuilder = methodBuilder.DefineParameter(parameter.Position + 1, parameter.Attributes, parameter.Name);
        foreach (CustomAttributeData data in (IEnumerable<CustomAttributeData>) parameter.GetCustomAttributesData())
        {
          CustomAttributeBuilder attributeBuilder = data.ToAttributeBuilder();
          if (attributeBuilder != null)
            parameterBuilder.SetCustomAttribute(attributeBuilder);
        }
      }
      foreach (CustomAttributeData data in (IEnumerable<CustomAttributeData>) key.GetCustomAttributesData())
      {
        CustomAttributeBuilder attributeBuilder = data.ToAttributeBuilder();
        if (attributeBuilder != null)
          methodBuilder.SetCustomAttribute(attributeBuilder);
      }
      methodBuilder.SetCustomAttribute(new CustomAttributeBuilder(typeof (PXExtensionManager.PXCstOverridedAttribute).GetConstructor(System.Type.EmptyTypes), new object[0], new System.Reflection.FieldInfo[0], new object[0]));
      ILGenerator ilGenerator = methodBuilder.GetILGenerator();
      for (int index = 0; index < array.Length; ++index)
        ilGenerator.Emit(OpCodes.Ldarg, index);
      ilGenerator.EmitCall(OpCodes.Call, (MethodInfo) method, (System.Type[]) null);
      ilGenerator.Emit(OpCodes.Ret);
    }
    foreach (MethodBuilder methodBuilder in (List<MethodBuilder>) typeBuilder.GetType().GetField("m_listMethods", BindingFlags.Instance | BindingFlags.NonPublic).GetValue((object) typeBuilder))
    {
      ILGenerator ilGenerator = methodBuilder.GetILGenerator();
      byte[] source = (byte[]) ilGenerator.GetType().GetField("m_ILStream", BindingFlags.Instance | BindingFlags.NonPublic).GetValue((object) ilGenerator);
      int count = (int) ilGenerator.GetType().GetField("m_length", BindingFlags.Instance | BindingFlags.NonPublic).GetValue((object) ilGenerator);
      foreach (ILInstruction readInstruction in new ILReader()
      {
        module = ((Module) moduleBuilder)
      }.ReadInstructions(((IEnumerable<byte>) source).Take<byte>(count).ToArray<byte>()))
      {
        if (!(readInstruction.Code != OpCodes.Callvirt))
        {
          MethodInfo operand = readInstruction.Operand as MethodInfo;
          if (!(operand == (MethodInfo) null) && operand.IsVirtual)
            source[readInstruction.Pos] = (byte) OpCodes.Call.Value;
        }
      }
    }
    if (interfaces != null)
      InterfaceWrapper.Create(typeBuilder, tunderlying, extensions, interfaces);
    return typeBuilder.CreateType();
  }

  internal static IEnumerable<IGrouping<MethodInfo, MethodInfo>> GetExtMethodsGroupByBaseMethod(
    System.Type troot,
    List<System.Type> extensions)
  {
    HashSet<MethodInfo> rootVirtualMethods = ((IEnumerable<MethodInfo>) troot.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)).Where<MethodInfo>((Func<MethodInfo, bool>) (m => m.IsVirtual)).ToHashSet<MethodInfo>();
    ILookup<MethodInfo, MethodInfo> validExtMethods = PXExtensionManager.GetValidExtMethods((IEnumerable<MethodInfo>) rootVirtualMethods, troot, extensions);
    return validExtMethods == null ? (IEnumerable<IGrouping<MethodInfo, MethodInfo>>) null : validExtMethods.Where<IGrouping<MethodInfo, MethodInfo>>((Func<IGrouping<MethodInfo, MethodInfo>, bool>) (m => rootVirtualMethods.Contains(m.Key)));
  }

  internal static ILookup<MethodInfo, MethodInfo> GetValidExtMethods(
    System.Type troot,
    List<System.Type> extensions)
  {
    IEnumerable<MethodInfo> availableMethods = ((IEnumerable<MethodInfo>) troot.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)).Where<MethodInfo>((Func<MethodInfo, bool>) (m => m.IsVirtual));
    Dictionary<System.Type, HashSet<System.Type>> dependencyDict = PXExtensionManager.GetDependencyDict(extensions);
    System.Type tnode = troot;
    Dictionary<System.Type, HashSet<System.Type>> extensionsDict = dependencyDict;
    return PXExtensionManager.GetValidExtMethods(availableMethods, tnode, extensionsDict);
  }

  internal static ILookup<MethodInfo, MethodInfo> GetValidExtMethods(
    IEnumerable<MethodInfo> rootVirtualMethods,
    System.Type troot,
    List<System.Type> extensions)
  {
    Dictionary<System.Type, HashSet<System.Type>> dependencyDict = PXExtensionManager.GetDependencyDict(extensions);
    return PXExtensionManager.GetValidExtMethods(rootVirtualMethods, troot, dependencyDict);
  }

  private static ILookup<MethodInfo, MethodInfo> GetValidExtMethods(
    IEnumerable<MethodInfo> availableMethods,
    System.Type tnode,
    Dictionary<System.Type, HashSet<System.Type>> extensionsDict)
  {
    (ILookup<MethodInfo, MethodInfo> validMethods, List<MethodInfo> unprocessedMethods) validExtMethodsRec = PXExtensionManager.GetValidExtMethodsRec(availableMethods, tnode, extensionsDict);
    ILookup<MethodInfo, MethodInfo> validMethods = validExtMethodsRec.validMethods;
    MethodInfo methodInfo = validExtMethodsRec.unprocessedMethods.FirstOrDefault<MethodInfo>();
    if ((object) methodInfo != null)
      throw new PXException("The {0} method in the {1} graph extension is marked as [PXOverride], but no original method with this name has been found in PXGraph.", new object[2]
      {
        (object) methodInfo,
        (object) methodInfo.DeclaringType
      });
    return validMethods;
  }

  private static (ILookup<MethodInfo, MethodInfo> validMethods, List<MethodInfo> unprocessedMethods) GetValidExtMethodsRec(
    IEnumerable<MethodInfo> availableMethods,
    System.Type tnode,
    Dictionary<System.Type, HashSet<System.Type>> extensionsDict)
  {
    List<System.Type> source1 = new List<System.Type>();
    for (; tnode.BaseType != (System.Type) null; tnode = tnode.BaseType)
    {
      HashSet<System.Type> collection;
      if (extensionsDict.TryGetValue(tnode, out collection))
        source1.AddRange((IEnumerable<System.Type>) collection);
    }
    if (source1.Count == 0)
      return (Enumerable.Empty<MethodInfo>().ToLookup<MethodInfo, MethodInfo>((Func<MethodInfo, MethodInfo>) (_ => _)), new List<MethodInfo>(0));
    IEnumerable<MethodInfo> source2 = source1.SelectMany<System.Type, MethodInfo>((Func<System.Type, IEnumerable<MethodInfo>>) (ext => (IEnumerable<MethodInfo>) ext.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))).Where<MethodInfo>((Func<MethodInfo, bool>) (m => Attribute.IsDefined((MemberInfo) m, typeof (PXOverrideAttribute))));
    ILookup<string, MethodInfo> availableLookup = availableMethods.ToLookup<MethodInfo, string>((Func<MethodInfo, string>) (m => m.Name));
    List<MethodInfo> unprocMethods = new List<MethodInfo>();
    ILookup<MethodInfo, MethodInfo> validMethods = source2.Where<MethodInfo>((Func<MethodInfo, bool>) (ext =>
    {
      int num = availableLookup.Contains(ext.Name) ? 1 : 0;
      if (num != 0)
        return num != 0;
      unprocMethods.Add(ext);
      return num != 0;
    })).ToLookup<MethodInfo, MethodInfo>((Func<MethodInfo, MethodInfo>) (ext => PXExtensionManager.GetMethodBySig(ext, availableLookup)));
    foreach (System.Type tnode1 in source1)
    {
      (ILookup<MethodInfo, MethodInfo> lookup1, List<MethodInfo> methodInfoList) = PXExtensionManager.GetValidExtMethodsRec(availableMethods.Concat<MethodInfo>(((IEnumerable<MethodInfo>) tnode1.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)).Where<MethodInfo>((Func<MethodInfo, bool>) (m => m.IsVirtual))), tnode1, extensionsDict);
      if (lookup1 != null)
        validMethods = validMethods.Concat<IGrouping<MethodInfo, MethodInfo>>((IEnumerable<IGrouping<MethodInfo, MethodInfo>>) lookup1).SelectMany(lookup => lookup.Select(value => new
        {
          Key = lookup.Key,
          value = value
        })).Distinct().ToLookup(x => x.Key, x => x.value);
      unprocMethods.AddRange((IEnumerable<MethodInfo>) methodInfoList);
    }
    unprocMethods.RemoveAll((Predicate<MethodInfo>) (un => validMethods.Any<IGrouping<MethodInfo, MethodInfo>>((Func<IGrouping<MethodInfo, MethodInfo>, bool>) (lk => lk.AsEnumerable<MethodInfo>().Contains<MethodInfo>(un)))));
    return (validMethods, unprocMethods);
  }

  public static CustomAttributeBuilder ToAttributeBuilder(this CustomAttributeData data)
  {
    if (data == null || data.AttributeType == typeof (SecurityPermissionAttribute))
      return (CustomAttributeBuilder) null;
    object[] values = (object[]) PXExtensionManager.ExtractValues(typeof (object[]), data.ConstructorArguments);
    List<PropertyInfo> propertyInfoList = new List<PropertyInfo>();
    List<object> objectList1 = new List<object>();
    List<System.Reflection.FieldInfo> fieldInfoList = new List<System.Reflection.FieldInfo>();
    List<object> objectList2 = new List<object>();
    foreach (CustomAttributeNamedArgument namedArgument in (IEnumerable<CustomAttributeNamedArgument>) data.NamedArguments)
    {
      System.Reflection.FieldInfo memberInfo1 = namedArgument.MemberInfo as System.Reflection.FieldInfo;
      PropertyInfo memberInfo2 = namedArgument.MemberInfo as PropertyInfo;
      CustomAttributeTypedArgument typedValue;
      if (memberInfo1 != (System.Reflection.FieldInfo) null)
      {
        fieldInfoList.Add(memberInfo1);
        List<object> objectList3 = objectList2;
        typedValue = namedArgument.TypedValue;
        object obj = typedValue.Value;
        objectList3.Add(obj);
      }
      else if (memberInfo2 != (PropertyInfo) null)
      {
        propertyInfoList.Add(memberInfo2);
        List<object> objectList4 = objectList1;
        typedValue = namedArgument.TypedValue;
        object obj = typedValue.Value;
        objectList4.Add(obj);
      }
    }
    return new CustomAttributeBuilder(data.Constructor, values, propertyInfoList.ToArray(), objectList1.ToArray(), fieldInfoList.ToArray(), objectList2.ToArray());
  }

  public static System.Type GetWrapperType(System.Type t)
  {
    System.Type wrapper;
    lock (PXExtensionManager.Wrappers)
    {
      Dictionary<PXExtensionManager.ListOfTypes, System.Type> dictionary;
      if (!PXExtensionManager.Wrappers.TryGetValue(t, out dictionary))
        PXExtensionManager.Wrappers[t] = dictionary = new Dictionary<PXExtensionManager.ListOfTypes, System.Type>();
      if (dictionary == null)
        return t;
      PXExtensionManager.ListOfTypes key = new PXExtensionManager.ListOfTypes(PXExtensionManager.GetExtensions(t, false));
      if (!dictionary.TryGetValue(key, out wrapper))
      {
        wrapper = PXExtensionManager.CreateWrapper(t, PXExtensionManager.GetExtensions(t, false));
        dictionary.Add(key, wrapper);
        if (wrapper == (System.Type) null)
          PXExtensionManager.Wrappers[t] = (Dictionary<PXExtensionManager.ListOfTypes, System.Type>) null;
      }
    }
    System.Type type = wrapper;
    return (object) type != null ? type : t;
  }

  public static Attribute[] GetCustomAttributes(CustomAttributeData[] data)
  {
    List<Attribute> attributeList = new List<Attribute>(data.Length);
    foreach (CustomAttributeData customAttributeData in data)
    {
      Attribute attribute = (Attribute) PXExtensionManager.GetWrapperType(customAttributeData.Constructor.DeclaringType).GetConstructor(customAttributeData.ConstructorArguments.Select<CustomAttributeTypedArgument, System.Type>((Func<CustomAttributeTypedArgument, System.Type>) (_ => _.ArgumentType)).ToArray<System.Type>()).Invoke((object[]) PXExtensionManager.ExtractValues(typeof (object[]), customAttributeData.ConstructorArguments));
      foreach (CustomAttributeNamedArgument namedArgument in (IEnumerable<CustomAttributeNamedArgument>) customAttributeData.NamedArguments)
      {
        object obj = PXExtensionManager.ExtractValue(namedArgument.TypedValue);
        PropertyInfo memberInfo1 = namedArgument.MemberInfo as PropertyInfo;
        if (memberInfo1 != (PropertyInfo) null)
        {
          memberInfo1.SetValue((object) attribute, obj, (object[]) null);
        }
        else
        {
          System.Reflection.FieldInfo memberInfo2 = namedArgument.MemberInfo as System.Reflection.FieldInfo;
          if (memberInfo2 == (System.Reflection.FieldInfo) null)
            throw new PXException("Unknown property {0}", new object[1]
            {
              (object) namedArgument.MemberInfo
            });
          memberInfo2.SetValue((object) attribute, obj);
        }
      }
      attributeList.Add(attribute);
    }
    return attributeList.ToArray();
  }

  private static Array ExtractValues(System.Type tresult, IList<CustomAttributeTypedArgument> lst)
  {
    Array instance = Array.CreateInstance(tresult.GetElementType(), lst.Count);
    for (int index = 0; index < lst.Count; ++index)
    {
      object obj = PXExtensionManager.ExtractValue(lst[index]);
      instance.SetValue(obj, index);
    }
    return instance;
  }

  private static object ExtractValue(CustomAttributeTypedArgument typedArgument)
  {
    object values = typedArgument.Value;
    if (values is IList<CustomAttributeTypedArgument> lst)
      values = (object) PXExtensionManager.ExtractValues(typedArgument.ArgumentType, lst);
    return values;
  }

  private static LambdaExpression BuildGraphMethod(
    System.Type tgraph,
    MethodInfo original,
    List<System.Type> graphextensions,
    IEnumerable<MethodInfo> methods)
  {
    ParameterExpression g = Expression.Parameter(tgraph, "g");
    return PXExtensionManager.BuildMethod(g, original, methods, (Func<MethodInfo, Expression>) (methodInfo =>
    {
      int num = -1;
      for (int index = 0; index < graphextensions.Count; ++index)
      {
        for (System.Type type = graphextensions[index]; type != (System.Type) null; type = type.BaseType)
        {
          if (methodInfo.DeclaringType == type)
          {
            num = index;
            break;
          }
        }
      }
      return num >= 0 ? (Expression) Expression.Convert((Expression) Expression.ArrayIndex((Expression) Expression.PropertyOrField((Expression) g, "GraphExtensions"), (Expression) Expression.Constant((object) num)), methodInfo.DeclaringType) : (Expression) g;
    }));
  }

  private static LambdaExpression BuildExtensionMethod(
    System.Type textension,
    MethodInfo original,
    List<System.Type> graphextensions,
    IEnumerable<MethodInfo> methods)
  {
    ParameterExpression ext = Expression.Parameter(textension, "ext");
    Expression graph = (Expression) Expression.Convert((Expression) Expression.Property((Expression) ext, "Base"), typeof (PXGraph));
    return PXExtensionManager.BuildMethod(ext, original, methods, (Func<MethodInfo, Expression>) (methodInfo =>
    {
      int num = -1;
      for (int index = 0; index < graphextensions.Count; ++index)
      {
        for (System.Type type = graphextensions[index]; type != (System.Type) null; type = type.BaseType)
        {
          if (methodInfo.DeclaringType == type)
          {
            num = index;
            break;
          }
        }
      }
      if (num < 0)
        return (Expression) ext;
      return (Expression) Expression.Convert((Expression) Expression.Call(graph, "GetExtension", new System.Type[0], (Expression) Expression.Constant((object) num)), methodInfo.DeclaringType);
    }));
  }

  private static LambdaExpression BuildMethod(
    ParameterExpression callee,
    MethodInfo method,
    IEnumerable<MethodInfo> extensionsMethods,
    Func<MethodInfo, Expression> targetFunctor)
  {
    MethodInfo[] array = ((IEnumerable<MethodInfo>) new MethodInfo[1]
    {
      method
    }).Concat<MethodInfo>(extensionsMethods).ToArray<MethodInfo>();
    ParameterExpression[] arguments = ((IEnumerable<ParameterInfo>) method.GetParameters()).Select<ParameterInfo, ParameterExpression>((Func<ParameterInfo, ParameterExpression>) (_ => Expression.Parameter(_.ParameterType))).ToArray<ParameterExpression>();
    Func<MethodInfo, bool> predicate = (Func<MethodInfo, bool>) (_ => _.GetParameters().Length == arguments.Length);
    List<MethodInfo> list1 = ((IEnumerable<MethodInfo>) array).Where<MethodInfo>(predicate).ToList<MethodInfo>();
    List<MethodInfo> list2 = extensionsMethods.Where<MethodInfo>((Func<MethodInfo, bool>) (_ => _.GetParameters().Length == arguments.Length + 1)).ToList<MethodInfo>();
    if (method.ReturnType != typeof (void))
    {
      ParameterExpression left = Expression.Variable(method.ReturnType, "ret");
      List<Expression> expressionList = new List<Expression>();
      foreach (MethodInfo method1 in list1)
      {
        int num = method1.ReturnType != typeof (void) ? 1 : 0;
        Expression right = (Expression) Expression.Call(targetFunctor(method1), method1, (Expression[]) arguments);
        if (num != 0)
          right = (Expression) Expression.Assign((Expression) left, right);
        expressionList.Add(right);
      }
      expressionList.Add((Expression) left);
      Expression body = (Expression) Expression.Block((IEnumerable<ParameterExpression>) new ParameterExpression[1]
      {
        left
      }, (IEnumerable<Expression>) expressionList);
      foreach (MethodInfo method2 in list2)
      {
        Expression expression = (Expression) Expression.Lambda(((IEnumerable<ParameterInfo>) method2.GetParameters()).Last<ParameterInfo>().ParameterType, body, arguments);
        body = (Expression) Expression.Call(targetFunctor(method2), method2, EnumerableExtensions.Append<Expression>((Expression[]) arguments, expression));
      }
      return Expression.Lambda(body, ((IEnumerable<ParameterExpression>) new ParameterExpression[1]
      {
        callee
      }).Concat<ParameterExpression>((IEnumerable<ParameterExpression>) arguments));
    }
    Expression body1 = (Expression) Expression.Block((IEnumerable<Expression>) list1.Cast<MethodInfo>().Select<MethodInfo, MethodCallExpression>((Func<MethodInfo, MethodCallExpression>) (simpleMethod => Expression.Call(targetFunctor(simpleMethod), simpleMethod, (Expression[]) arguments))));
    foreach (MethodInfo method3 in list2)
    {
      Expression expression = (Expression) Expression.Lambda(((IEnumerable<ParameterInfo>) method3.GetParameters()).Last<ParameterInfo>().ParameterType, body1, arguments);
      body1 = (Expression) Expression.Call(targetFunctor(method3), method3, EnumerableExtensions.Append<Expression>((Expression[]) arguments, expression));
    }
    return Expression.Lambda(body1, ((IEnumerable<ParameterExpression>) new ParameterExpression[1]
    {
      callee
    }).Concat<ParameterExpression>((IEnumerable<ParameterExpression>) arguments));
  }

  private static bool IsCompatibleMethod(MethodInfo src, MethodInfo ext)
  {
    ParameterInfo[] parameters = src.GetParameters();
    ParameterInfo[] extParams = ext.GetParameters();
    int num = parameters.Length == extParams.Length ? 1 : 0;
    bool flag1 = parameters.Length + 1 == extParams.Length;
    if (num == 0 && !flag1)
      return false;
    bool flag2 = src.ReturnType != typeof (void);
    if (src.ReturnType != ext.ReturnType && ext.ReturnType != typeof (void) || ((IEnumerable<ParameterInfo>) parameters).Where<ParameterInfo>((Func<ParameterInfo, int, bool>) ((p, i) => p.ParameterType != extParams[i].ParameterType)).Any<ParameterInfo>())
      return false;
    if (!flag1)
      return true;
    System.Type parameterType = ((IEnumerable<ParameterInfo>) extParams).Last<ParameterInfo>().ParameterType;
    MethodInfo method = parameterType.GetMethod("Invoke");
    if (flag2)
    {
      if (ext.ReturnType != src.ReturnType)
        return false;
      if (method.ReturnType != src.ReturnType)
        throw new PXException("Return type of the {0} delegate does not match the return type of the overridden method.", new object[1]
        {
          (object) parameterType.Name
        });
    }
    if (parameters.Length == 0)
      return true;
    ParameterInfo[] prms = method.GetParameters();
    if (prms.Length < parameters.Length)
      throw new PXException("The number of declared parameters of the {0} delegate does not match the number of parameters of the overridden method.", new object[1]
      {
        (object) parameterType.Name
      });
    return parameters.Length == ((IEnumerable<ParameterInfo>) parameters).Where<ParameterInfo>((Func<ParameterInfo, int, bool>) ((p, i) => p.ParameterType == prms[i].ParameterType)).Count<ParameterInfo>();
  }

  private static MethodInfo GetMethodBySig(
    MethodInfo methodInfo,
    ILookup<string, MethodInfo> virtualMethods)
  {
    MethodInfo methodInfo1 = virtualMethods[methodInfo.Name].FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>) (m => PXExtensionManager.IsCompatibleMethod(m, methodInfo)));
    return !(methodInfo1 == (MethodInfo) null) ? methodInfo1 : throw new PXException("The {0} method in the {1} graph extension is marked as [PXOverride], but its signature is not compatible with the original method.", new object[2]
    {
      (object) methodInfo,
      (object) methodInfo.DeclaringType
    });
  }

  public static object[] GetCustomAttributesEx(this MemberInfo t, System.Type filter, bool inherit)
  {
    object[] customAttributes1 = t.GetCustomAttributes(filter, inherit);
    if (!((IEnumerable<object>) customAttributes1).Any<object>((Func<object, bool>) (_ => PXExtensionManager.GetWrapperType(_.GetType()) != _.GetType())))
      return customAttributes1;
    if (inherit)
      throw new PXException("Inherit not supported");
    Attribute[] customAttributes2 = PXExtensionManager.GetCustomAttributes(t.GetCustomAttributesData().Where<CustomAttributeData>((Func<CustomAttributeData, bool>) (d => filter == (System.Type) null || filter.IsAssignableFrom(d.Constructor.DeclaringType))).ToArray<CustomAttributeData>());
    if (filter == (System.Type) null)
      return (object[]) customAttributes2;
    Array instance = Array.CreateInstance(filter, customAttributes2.Length);
    Array.Copy((Array) customAttributes2, instance, customAttributes2.Length);
    return (object[]) instance;
  }

  public static void EmitExtensionsCreation(System.Type tgraph, List<System.Type> extensions, ILGenerator il)
  {
    if (extensions.Count <= 0)
      return;
    System.Reflection.FieldInfo field = tgraph.GetField("Extensions", BindingFlags.Instance | BindingFlags.NonPublic);
    System.Type elementType = field.FieldType.GetElementType();
    il.Emit(OpCodes.Ldarg_0);
    il.Emit(OpCodes.Castclass, tgraph);
    il.Emit(OpCodes.Ldc_I4, extensions.Count);
    il.Emit(OpCodes.Newarr, elementType);
    il.Emit(OpCodes.Stfld, field);
    LocalBuilder local = il.DeclareLocal(elementType);
    Dictionary<System.Type, HashSet<System.Type>> dependencyDict = PXExtensionManager.GetDependencyDict(extensions);
    List<System.Type> interfaceWrappers = InterfaceWrapper.GetExtensionInterfaceWrappers(tgraph, extensions);
    ILookup<MethodInfo, MethodInfo> validExtMethods = PXExtensionManager.GetValidExtMethods(CustomizedTypeManager.GetTypeNotCustomized(tgraph), extensions);
    for (int index1 = 0; index1 < extensions.Count; ++index1)
    {
      System.Type type1;
      if (dependencyDict.ContainsKey(extensions[index1]))
      {
        System.Type type2 = PXExtensionManager.CreateExtensionWrapper(extensions[index1], extensions, validExtMethods);
        if ((object) type2 == null)
          type2 = extensions[index1];
        type1 = type2;
      }
      else if (extensions[index1].IsAbstract || interfaceWrappers != null && interfaceWrappers.Contains(extensions[index1]))
      {
        System.Type type3 = PXExtensionManager.CreateExtensionWrapper(extensions[index1], extensions, validExtMethods);
        if ((object) type3 == null)
          type3 = extensions[index1];
        type1 = type3;
      }
      else
        type1 = extensions[index1];
      if (type1.IsAbstract)
        throw new PXException("Cannot instantiate graph extension {0} for use in {1} because class {0} is abstract.", new object[2]
        {
          (object) type1,
          (object) string.Join(", ", (IEnumerable<string>) dependencyDict[type1].Select<System.Type, string>((Func<System.Type, string>) (dependentExtension => dependentExtension.FullName)).OrderBy<string, string>((Func<string, string>) (name => name)))
        });
      il.Emit(OpCodes.Ldarg_0);
      il.Emit(OpCodes.Castclass, tgraph);
      il.Emit(OpCodes.Ldfld, field);
      il.Emit(OpCodes.Ldc_I4, index1);
      ILGenerator ilGenerator = il;
      OpCode newobj = OpCodes.Newobj;
      ilGenerator.Emit(newobj, type1.GetConstructor(new System.Type[0]) ?? throw new PXException("Graph extension {0} must have a default constructor.", new object[1]
      {
        (object) type1
      }));
      il.Emit(OpCodes.Dup);
      il.Emit(OpCodes.Stloc, local);
      il.Emit(OpCodes.Stelem_Ref);
      il.Emit(OpCodes.Ldloc, local);
      il.Emit(OpCodes.Castclass, type1);
      il.Emit(OpCodes.Ldarg_0);
      il.Emit(OpCodes.Castclass, tgraph);
      il.Emit(OpCodes.Stfld, type1.GetField("_Base", BindingFlags.Instance | BindingFlags.NonPublic));
      System.Type firstExtensionParent = PXExtensionManager.GetFirstExtensionParent(type1);
      int num1;
      for (int index2 = 0; index2 < (num1 = firstExtensionParent.GetGenericArguments().Length - 1); ++index2)
      {
        System.Type genericArgument = firstExtensionParent.GetGenericArguments()[index2];
        int num2;
        if ((num2 = extensions.IndexOf(genericArgument)) == -1)
          throw new PXException("Dependant extension does not belong to the same graph.");
        il.Emit(OpCodes.Ldloc, local);
        il.Emit(OpCodes.Castclass, type1);
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Castclass, tgraph);
        il.Emit(OpCodes.Ldfld, field);
        il.Emit(OpCodes.Ldc_I4, num2);
        il.Emit(OpCodes.Ldelem_Ref);
        il.Emit(OpCodes.Castclass, genericArgument);
        il.Emit(OpCodes.Stfld, type1.GetField("_Base" + (num1 - index2).ToString(), BindingFlags.Instance | BindingFlags.NonPublic));
      }
    }
  }

  public static void InitExtensions(object instance)
  {
    System.Type type = instance.GetType();
    System.Action<object> action;
    bool flag;
    lock (((ICollection) PXExtensionManager.InitDelegates).SyncRoot)
      flag = PXExtensionManager.InitDelegates.TryGetValue(type, out action);
    if (!flag)
    {
      List<System.Type> extensions = PXExtensionManager.GetExtensions(type, false);
      if (extensions.Count != 0)
      {
        DynamicMethod dynamicMethod = new DynamicMethod("_Initialize", (System.Type) null, new System.Type[1]
        {
          typeof (object)
        }, type, true);
        ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
        PXExtensionManager.EmitExtensionsCreation(type, extensions, ilGenerator);
        System.Reflection.FieldInfo field = type.GetField("Extensions", BindingFlags.Instance | BindingFlags.NonPublic);
        for (int index = 0; index < extensions.Count; ++index)
        {
          ilGenerator.Emit(OpCodes.Ldarg_0);
          ilGenerator.Emit(OpCodes.Castclass, type);
          ilGenerator.Emit(OpCodes.Ldfld, field);
          ilGenerator.Emit(OpCodes.Ldc_I4, index);
          ilGenerator.Emit(OpCodes.Ldelem_Ref);
          MethodInfo method = typeof (PXGraphExtension).GetMethod("Initialize");
          ilGenerator.EmitCall(OpCodes.Callvirt, method, (System.Type[]) null);
        }
        ilGenerator.Emit(OpCodes.Ret);
        action = (System.Action<object>) dynamicMethod.CreateDelegate(typeof (System.Action<object>));
      }
      lock (((ICollection) PXExtensionManager.InitDelegates).SyncRoot)
      {
        if (!PXExtensionManager.InitDelegates.ContainsKey(type))
          PXExtensionManager.InitDelegates.Add(type, action);
      }
    }
    if (action == null)
      return;
    action(instance);
  }

  public static PXEventSubscriberAttribute[] MergeExtensionAttributes(
    IEnumerable<MemberInfo> memberList,
    List<PXEventSubscriberAttribute> existingList = null)
  {
    List<PXEventSubscriberAttribute> result = existingList ?? new List<PXEventSubscriberAttribute>();
    foreach (MemberInfo member in memberList)
    {
      IEnumerable<PXCustomizeBaseAttributeAttribute> customAttributes1 = member.GetCustomAttributes<PXCustomizeBaseAttributeAttribute>();
      foreach (PXCustomizeBaseAttributeAttribute attributeAttribute in customAttributes1)
        attributeAttribute.Apply(result);
      IEnumerable<PXRemoveBaseAttributeAttribute> customAttributes2 = member.GetCustomAttributes<PXRemoveBaseAttributeAttribute>();
      foreach (PXRemoveBaseAttributeAttribute attributeAttribute in customAttributes2)
        attributeAttribute.Apply(result);
      PXEventSubscriberAttribute[] customAttributesEx = (PXEventSubscriberAttribute[]) member.GetCustomAttributesEx(typeof (PXEventSubscriberAttribute), false);
      PXMergeAttributesAttribute customAttribute = member.GetCustomAttribute<PXMergeAttributesAttribute>();
      bool flag = customAttributes1.Any<PXCustomizeBaseAttributeAttribute>() || customAttributes2.Any<PXRemoveBaseAttributeAttribute>();
      if (customAttribute != null)
      {
        if (customAttribute.Method == MergeMethod.Replace & flag)
          throw new PXException("MergeMethod.Replace is Invalid {0}", new object[1]
          {
            (object) member
          });
        customAttribute.Apply(result, customAttributesEx);
      }
      else if (flag)
        result.AddRange((IEnumerable<PXEventSubscriberAttribute>) customAttributesEx);
      else if (!typeof (PXMappedCacheExtension).IsAssignableFrom(member.DeclaringType) || ((IEnumerable<PXEventSubscriberAttribute>) customAttributesEx).Count<PXEventSubscriberAttribute>() != 0)
      {
        result.Clear();
        result.AddRange((IEnumerable<PXEventSubscriberAttribute>) customAttributesEx);
      }
    }
    return result.ToArray();
  }

  /// <exclude />
  internal sealed class GraphKind
  {
    public readonly System.Type _Type;

    public GraphKind(System.Type type) => this._Type = type;

    public override int GetHashCode()
    {
      System.Type type = this._Type;
      return (object) type == null ? 0 : type.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return this._Type == (System.Type) null;
      return obj is PXExtensionManager.GraphKind graphKind && this._Type == (System.Type) null == (graphKind._Type == (System.Type) null) && (!(this._Type != (System.Type) null) || !(graphKind._Type != this._Type));
    }
  }

  /// <exclude />
  internal sealed class ListOfTypes
  {
    public readonly List<System.Type> _Types;

    public ListOfTypes(List<System.Type> types) => this._Types = types;

    public override int GetHashCode() => this._Types == null ? 100 : this._Types.Count + 100;

    public override bool Equals(object obj)
    {
      if (obj == null)
        return this._Types == null || this._Types.Count == 0;
      if (!(obj is PXExtensionManager.ListOfTypes listOfTypes) || this._Types == null != (listOfTypes._Types == null))
        return false;
      if (this._Types != null)
      {
        if (this._Types.Count != listOfTypes._Types.Count)
          return false;
        for (int index = 0; index < this._Types.Count; ++index)
        {
          if (listOfTypes._Types[index] != this._Types[index])
            return false;
        }
      }
      return true;
    }
  }

  internal class PXCstOverridedAttribute : Attribute
  {
  }
}
