// Decompiled with JetBrains decompiler
// Type: PX.Translation.PXDllRipper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using Microsoft.Extensions.Options;
using Namotion.Reflection;
using PX.Common;
using PX.Data;
using PX.Data.LocalizationKeyGenerators;
using PX.Metadata;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Translation;

/// <exclude />
internal class PXDllRipper : PXRipper
{
  private readonly DllResourceTypesToCollect dllResourceTypesToCollect;

  public PXDllRipper()
    : this(DllResourceTypesToCollect.All)
  {
  }

  public PXDllRipper(
    DllResourceTypesToCollect dllResourceTypesToCollect)
  {
    if (dllResourceTypesToCollect.Includes(DllResourceTypesToCollect.XmlComment))
    {
      StringCollectingOptions collectingOptions = ServiceLocator.Current.GetInstance<IOptions<StringCollectingOptions>>().Value;
      if ((collectingOptions != null ? (!collectingOptions.CollectXmlComments ? 1 : 0) : 0) != 0)
        dllResourceTypesToCollect &= ~DllResourceTypesToCollect.XmlComment;
    }
    this.dllResourceTypesToCollect = dllResourceTypesToCollect;
  }

  protected override void OnRipStart(List<string> processed, ResourceCollection result)
  {
    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
    {
      string withoutExtension;
      try
      {
        withoutExtension = Path.GetFileNameWithoutExtension(assembly.Location);
      }
      catch
      {
        continue;
      }
      if (!processed.Contains(withoutExtension))
      {
        this.RipNormalAssembly(assembly, result);
        processed.Add(Path.GetFileNameWithoutExtension(assembly.Location));
      }
    }
  }

  protected override void Rip(
    System.IO.FileInfo file,
    List<string> processed,
    ResourceCollection result,
    LocalizationTranslationSetItem item = null,
    TranslationSetMaint graph = null,
    bool standalone = false)
  {
    string withoutExtension = Path.GetFileNameWithoutExtension(file.FullName);
    if (withoutExtension.IndexOf(".resources") != -1 || file.FullName.Contains("x64") || file.FullName.Contains("x86") || processed.Contains(withoutExtension))
      return;
    this.RipDll(file.FullName, result);
    processed.Add(withoutExtension);
  }

  private void RipDll(string dllPath, ResourceCollection result)
  {
    if (dllPath.IndexOf(".resources") != -1 || dllPath.IndexOf("Customization\\SourceControl") != -1)
      return;
    AppDomain domain = (AppDomain) null;
    ResolveEventHandler resolveEventHandler = (ResolveEventHandler) null;
    try
    {
      byte[] dllBytes = File.ReadAllBytes(dllPath);
      domain = AppDomain.CreateDomain("TranslationSecondaryDomain");
      resolveEventHandler = (ResolveEventHandler) ((sender, args) => args.Name.StartsWith(Path.GetFileNameWithoutExtension(dllPath), StringComparison.OrdinalIgnoreCase) ? Assembly.Load(dllBytes) : (Assembly) null);
      AppDomain.CurrentDomain.AssemblyResolve += resolveEventHandler;
      Assembly a;
      try
      {
        a = domain.Load(dllBytes);
      }
      catch (BadImageFormatException ex)
      {
        return;
      }
      this.RipNormalAssembly(a, result);
    }
    finally
    {
      if (resolveEventHandler != null)
        AppDomain.CurrentDomain.AssemblyResolve -= resolveEventHandler;
      if (domain != null)
        AppDomain.Unload(domain);
    }
  }

  private void RipNormalAssembly(Assembly a, ResourceCollection result)
  {
    System.Type[] types;
    try
    {
      types = a.GetTypes();
    }
    catch
    {
      return;
    }
    using (new RipAssemblyScope())
    {
      foreach (System.Type type in types)
      {
        try
        {
          this.RipType(type, types, result);
        }
        catch
        {
        }
      }
    }
  }

  private void RipType(System.Type type, System.Type[] alltypes, ResourceCollection result)
  {
    if (this.IsDAC(type))
    {
      bool flag = this.dllResourceTypesToCollect.Includes(DllResourceTypesToCollect.XmlComment) && !this.IsMappedCacheExtension(type);
      if (((this.dllResourceTypesToCollect.Includes(DllResourceTypesToCollect.DisplayName) ? 1 : (this.dllResourceTypesToCollect.Includes(DllResourceTypesToCollect.ListAttribute) ? 1 : 0)) | (flag ? 1 : 0)) == 0)
        return;
      System.Type concreteType = this.GetConcreteType(type, (IEnumerable<System.Type>) alltypes);
      PXCache cacheByTypeSafely = this.GetCacheByTypeSafely(concreteType);
      if (cacheByTypeSafely == null)
        return;
      this.ReadDisplayNamesAndLabels(cacheByTypeSafely, concreteType, result);
      if (!flag)
        return;
      this.ReadXmlComments(cacheByTypeSafely, concreteType, result);
    }
    else if (this.IsMessagesContainer(type))
    {
      this.ReadMessages(type, result);
    }
    else
    {
      if (!this.IsGraph(type))
        return;
      this.ReadDisplayNamesFromGraph(type, result);
    }
  }

  private bool IsGraph(System.Type type)
  {
    return type.IsClass && !type.IsAbstract && !type.IsGenericType && type != typeof (PXGraph) && typeof (PXGraph).IsAssignableFrom(type);
  }

  private bool IsDAC(System.Type type) => this.IsBqlTable(type) || this.IsCacheExtension(type);

  private bool IsBqlTable(System.Type type)
  {
    return type.IsClass && !type.IsAbstract && !type.ContainsGenericParameters && type.HasDefaultConstructor() && typeof (IBqlTable).IsAssignableFrom(type) && !typeof (PXMappedCacheExtension).IsAssignableFrom(type);
  }

  private bool IsCacheExtension(System.Type type)
  {
    return type.IsClass && !type.ContainsGenericParameters && type.IsSubclassOf(typeof (PXCacheExtension)) && type.BaseType.IsGenericType && !typeof (PXMappedCacheExtension).IsAssignableFrom(type);
  }

  private bool IsMappedCacheExtension(System.Type type)
  {
    return type == typeof (PXMappedCacheExtension) || type.IsSubclassOf(typeof (PXMappedCacheExtension));
  }

  private bool IsMessagesContainer(System.Type type)
  {
    return type.IsClass && Attribute.IsDefined((MemberInfo) type, typeof (PXLocalizableAttribute));
  }

  private System.Type GetConcreteType(System.Type type, IEnumerable<System.Type> availableTypes)
  {
    if (type.IsGenericTypeDefinition)
    {
      if (type.IsNested)
      {
        try
        {
          System.Type type1 = availableTypes.FirstOrDefault<System.Type>((Func<System.Type, bool>) (t => !t.IsGenericTypeDefinition && type.DeclaringType.GenericIsAssignableFrom(t)));
          if (type1 != (System.Type) null)
          {
            System.Type type2 = type1;
            while (type2 != (System.Type) null && type2 != typeof (object) && (!type2.IsGenericType || type2.GetGenericTypeDefinition() != type.DeclaringType))
              type2 = type2.BaseType;
            if (type2 != (System.Type) null)
              return type.MakeGenericType(type2.GetGenericArguments());
          }
        }
        catch
        {
        }
      }
    }
    return type;
  }

  private PXCache GetCacheByTypeSafely(System.Type type)
  {
    try
    {
      return new PXGraph().Caches[type];
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
      return (PXCache) null;
    }
  }

  private void ReadXmlComments(PXCache cache, System.Type dacOrDacExtType, ResourceCollection result)
  {
    string xmlDocsSummary1 = XmlDocsExtensions.GetXmlDocsSummary(dacOrDacExtType, true);
    if (!string.IsNullOrWhiteSpace(xmlDocsSummary1))
    {
      string summaryLocalizationKey = XmlCommentKeyGenerator.GetDacOrDacExtensionXmlCommentSummaryLocalizationKey(dacOrDacExtType);
      result.AddResource(new LocalizationResourceLite(summaryLocalizationKey, LocalizationResourceType.XmlComment, xmlDocsSummary1));
    }
    string xmlDocsRemarks1 = XmlDocsExtensions.GetXmlDocsRemarks(dacOrDacExtType, true);
    if (!string.IsNullOrWhiteSpace(xmlDocsRemarks1))
    {
      string remarkLocalizationKey = XmlCommentKeyGenerator.GetDacOrDacExtensionXmlCommentRemarkLocalizationKey(dacOrDacExtType);
      result.AddResource(new LocalizationResourceLite(remarkLocalizationKey, LocalizationResourceType.XmlComment, xmlDocsRemarks1));
    }
    foreach (PropertyInfo member in ((IEnumerable<PropertyInfo>) dacOrDacExtType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)).Where<PropertyInfo>((Func<PropertyInfo, bool>) (property => cache.Fields.Contains(property.Name))))
    {
      string xmlDocsSummary2 = XmlDocsExtensions.GetXmlDocsSummary((MemberInfo) member, true);
      string xmlDocsRemarks2 = XmlDocsExtensions.GetXmlDocsRemarks((MemberInfo) member, true);
      string xmlDocsValue = member.GetXmlDocsValue();
      string neutralValue1 = xmlDocsRemarks2;
      string neutralValue2 = xmlDocsSummary2;
      if (!string.IsNullOrWhiteSpace(neutralValue2))
      {
        string summaryLocalizationKey = XmlCommentKeyGenerator.GetDacPropertyXmlCommentSummaryLocalizationKey(dacOrDacExtType, member.Name);
        result.AddResource(new LocalizationResourceLite(summaryLocalizationKey, LocalizationResourceType.XmlComment, neutralValue2));
      }
      if (!string.IsNullOrWhiteSpace(neutralValue1))
      {
        string remarksLocalizationKey = XmlCommentKeyGenerator.GetDacPropertyXmlCommentRemarksLocalizationKey(dacOrDacExtType, member.Name);
        result.AddResource(new LocalizationResourceLite(remarksLocalizationKey, LocalizationResourceType.XmlComment, neutralValue1));
      }
      if (!string.IsNullOrWhiteSpace(xmlDocsValue))
      {
        string valueLocalizationKey = XmlCommentKeyGenerator.GetDacPropertyXmlCommentValueLocalizationKey(dacOrDacExtType, member.Name);
        result.AddResource(new LocalizationResourceLite(valueLocalizationKey, LocalizationResourceType.XmlComment, xmlDocsValue));
      }
    }
  }

  private void ReadDisplayNamesAndLabels(PXCache cache, System.Type type, ResourceCollection result)
  {
    if (!this.dllResourceTypesToCollect.Includes(DllResourceTypesToCollect.DisplayName) && !this.dllResourceTypesToCollect.Includes(DllResourceTypesToCollect.ListAttribute))
      return;
    try
    {
      if (type.IsAbstract)
        return;
      List<System.Type> dacHierarchy = this.GetDacHierarchy(type);
      PropertyInfo prop = (PropertyInfo) null;
      System.Type propType = (System.Type) null;
      foreach (string field1 in (List<string>) cache.Fields)
      {
        string field = field1;
        System.Type cacheType = type;
        foreach (System.Type type1 in dacHierarchy)
        {
          prop = type1.GetProperty(field, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
          if (prop != (PropertyInfo) null)
          {
            cacheType = type1;
            break;
          }
          propType = ((IEnumerable<System.Type>) type1.GetNestedTypes()).FirstOrDefault<System.Type>((Func<System.Type, bool>) (nt => string.Equals(nt.Name, field, StringComparison.InvariantCultureIgnoreCase)));
          if (propType != (System.Type) null)
          {
            cacheType = type1;
            break;
          }
        }
        if ((prop != (PropertyInfo) null || propType != (System.Type) null) && !this.TryGetLocalizableValues(cacheType, (MemberInfo) prop, propType, result, field))
        {
          bool localizeLists = this.dllResourceTypesToCollect.Includes(DllResourceTypesToCollect.ListAttribute) && PXDllRipper.GetAttributesForField((MemberInfo) prop, propType, false).OfType<IPXLocalizableList>().All<IPXLocalizableList>((Func<IPXLocalizableList, bool>) (localizableList => localizableList.IsLocalizable));
          this.GetLocalizableValuesFromState(cache, cache.GetItemType(), field, localizeLists, cacheType.FullName, result);
          this.GetLocalizableValuesFromAttributes(prop, propType, cacheType.FullName, result, PXDllRipper.UIAttrs.Labels);
        }
      }
    }
    catch
    {
    }
  }

  private List<System.Type> GetDacHierarchy(System.Type type)
  {
    List<System.Type> dacHierarchy = new List<System.Type>()
    {
      type
    };
    if (this.IsCacheExtension(type))
      return dacHierarchy;
    for (System.Type baseType = type.BaseType; PXDllRipper.ShouldCollectStringsFromDacBaseType(baseType); baseType = baseType.BaseType)
      dacHierarchy.Add(baseType);
    return dacHierarchy;
  }

  private static bool ShouldCollectStringsFromDacBaseType(System.Type dacBaseType)
  {
    if (dacBaseType == (System.Type) null || !dacBaseType.IsBqlTable())
      return false;
    bool flag = typeof (IBqlTable).IsAssignableFrom(dacBaseType);
    if (!flag)
      return true;
    return flag && dacBaseType.IsAbstract;
  }

  private void GetLocalizableValuesFromAttributes(
    PropertyInfo prop,
    System.Type propType,
    string typeNameForKey,
    ResourceCollection result,
    PXDllRipper.UIAttrs uiAttrs)
  {
    bool flag = this.dllResourceTypesToCollect.Includes(DllResourceTypesToCollect.DisplayName);
    this.dllResourceTypesToCollect.Includes(DllResourceTypesToCollect.LabelAttribute);
    string nameLocalizationKey = PXUIFieldKeyGenerator.GetDacFieldNameLocalizationKey(typeNameForKey);
    foreach (object obj in PXDllRipper.GetAttributesForField((MemberInfo) prop, propType, false))
    {
      if (flag && (uiAttrs & PXDllRipper.UIAttrs.DisplayName) != (PXDllRipper.UIAttrs) 0 && obj is PXUIFieldAttribute pxuiFieldAttribute)
        result.AddResource(new LocalizationResourceLite(nameLocalizationKey, LocalizationResourceType.DisplayName, pxuiFieldAttribute.DisplayName));
    }
  }

  private void ReadLists(
    PXFieldState state,
    string typeNameForKey,
    string field,
    ResourceCollection result)
  {
    switch (state)
    {
      case PXStringState _ when ((PXStringState) state).AllowedValues != null:
        this.ReadStringList(result, typeNameForKey, field, state as PXStringState);
        break;
      case PXIntState _ when ((PXIntState) state).AllowedValues != null:
        this.ReadIntList(result, typeNameForKey, field, state as PXIntState);
        break;
    }
  }

  private void ReadStringList(
    ResourceCollection result,
    string assName,
    string field,
    PXStringState state)
  {
    if (state.AllowedValues.Length != state.AllowedLabels.Length)
      return;
    string listLocalizationKey = PXListKeyGenerator.GetDacListLocalizationKey(assName);
    for (int index = 0; index < state.AllowedValues.Length; ++index)
    {
      string localizationValue = PXListKeyGenerator.GetLocalizationValue(state.DisplayName, state.AllowedLabels[index]);
      result.AddResource(new LocalizationResourceLite(listLocalizationKey, LocalizationResourceType.StringListItem, localizationValue));
    }
  }

  private void ReadIntList(
    ResourceCollection result,
    string assName,
    string field,
    PXIntState state)
  {
    if (state.AllowedValues.Length != state.AllowedLabels.Length)
      return;
    string listLocalizationKey = PXListKeyGenerator.GetDacListLocalizationKey(assName);
    for (int index = 0; index < state.AllowedValues.Length; ++index)
    {
      string localizationValue = PXListKeyGenerator.GetLocalizationValue(state.DisplayName, state.AllowedLabels[index]);
      result.AddResource(new LocalizationResourceLite(listLocalizationKey, LocalizationResourceType.IntListItem, localizationValue));
    }
  }

  private void ReadDbStringList(
    ResourceCollection result,
    string assName,
    string field,
    string[] values)
  {
    if (values == null)
      return;
    for (int index = 0; index < values.Length; ++index)
    {
      string listLocalizationKey = PXListKeyGenerator.GetDbListLocalizationKey(assName);
      result.AddResource(new LocalizationResourceLite(listLocalizationKey, LocalizationResourceType.DbStringListItem, values[index]));
    }
  }

  private void ReadMessages(System.Type type, ResourceCollection result)
  {
    if (!this.dllResourceTypesToCollect.Includes(DllResourceTypesToCollect.Message))
      return;
    foreach (System.Reflection.FieldInfo fieldInfo in ((IEnumerable<System.Reflection.FieldInfo>) type.GetFields(BindingFlags.Static | BindingFlags.Public)).Where<System.Reflection.FieldInfo>((Func<System.Reflection.FieldInfo, bool>) (_ => _.FieldType == typeof (string))))
    {
      string neutralValue = (string) fieldInfo.GetValue((object) null);
      string messageLocalizationKey = PXSpecialKeyGenerator.GetMessageLocalizationKey(type.FullName);
      result.AddResource(new LocalizationResourceLite(messageLocalizationKey, LocalizationResourceType.Message, neutralValue));
    }
  }

  private void ReadDisplayNamesFromGraph(System.Type graphType, ResourceCollection result)
  {
    if (!this.dllResourceTypesToCollect.Includes(DllResourceTypesToCollect.DisplayName) && !this.dllResourceTypesToCollect.Includes(DllResourceTypesToCollect.ListAttribute))
      return;
    PXGraph graph = (PXGraph) null;
    try
    {
      graph = PXGraph.CreateInstance(graphType);
    }
    catch
    {
    }
    if (graph == null)
      return;
    this.ReadActions(graph, graphType, result);
    this.ReadDisplayNamesFromCacheAttached(graph, graphType, result);
  }

  private void ReadActions(PXGraph graph, System.Type graphType, ResourceCollection result)
  {
    try
    {
      foreach (DictionaryEntry action in (OrderedDictionary) graph.Actions)
      {
        string key = action.Key as string;
        PXAction pxAction = action.Value as PXAction;
        if (!string.IsNullOrWhiteSpace(key) && pxAction != null)
        {
          System.Reflection.FieldInfo field = graphType.GetField(key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
          if ((field == (System.Reflection.FieldInfo) null || !this.TryGetLocalizableValues(graphType, (MemberInfo) field, (System.Type) null, result, key)) && this.dllResourceTypesToCollect.Includes(DllResourceTypesToCollect.DisplayName) && !string.IsNullOrWhiteSpace(pxAction.GetState((object) null) is PXButtonState state ? state.DisplayName : (string) null) && (field != (System.Reflection.FieldInfo) null || state.IsDisplayNameSpecified))
          {
            string nameLocalizationKey = PXUIFieldKeyGenerator.GetActionNameLocalizationKey(graphType.FullName);
            result.AddResource(new LocalizationResourceLite(nameLocalizationKey, LocalizationResourceType.ActionDisplayName, state.DisplayName));
          }
        }
      }
    }
    catch
    {
    }
  }

  private void ReadDisplayNamesFromCacheAttached(
    PXGraph graph,
    System.Type graphType,
    ResourceCollection result)
  {
    try
    {
      Dictionary<PXExtensionManager.ListOfTypes, PXGraph.GraphStaticInfo> dictionary;
      if (!PXExtensionManager._GraphStaticInfo.TryGetValue(graphType, out dictionary))
        return;
      foreach (PXGraph.GraphStaticInfo graphStaticInfo in dictionary.Values)
      {
        if (graphStaticInfo.AlteredSource != null)
        {
          foreach (PXCache.AlteredSource alteredSource in graphStaticInfo.AlteredSource.Values)
          {
            if (alteredSource.Attributes != null && alteredSource.Attributes.Length != 0)
            {
              PXCache cach = graph.Caches[alteredSource.CacheType];
              foreach (IGrouping<string, PXEventSubscriberAttribute> grouping in ((IEnumerable<PXEventSubscriberAttribute>) alteredSource.Attributes).GroupBy<PXEventSubscriberAttribute, string, PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, string>) (a => a.FieldName), (Func<PXEventSubscriberAttribute, PXEventSubscriberAttribute>) (a => a), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
              {
                if (!this.TryGetLocalizableValues((IEnumerable<PXEventSubscriberAttribute>) grouping, result, grouping.Key))
                {
                  bool localizeLists = this.dllResourceTypesToCollect.Includes(DllResourceTypesToCollect.ListAttribute) && grouping.All<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => !(a is IPXLocalizableList pxLocalizableList) || pxLocalizableList.IsLocalizable));
                  this.GetLocalizableValuesFromState(cach, alteredSource.CacheType, grouping.Key, localizeLists, alteredSource.CacheType.FullName, result);
                }
              }
            }
          }
        }
      }
    }
    catch
    {
    }
  }

  private void GetLocalizableValuesFromState(
    PXCache cache,
    System.Type cacheType,
    string field,
    bool localizeLists,
    string typeNameForKey,
    ResourceCollection result)
  {
    PXFieldState state = (PXFieldState) null;
    try
    {
      state = cache.GetStateExt((object) null, field) as PXFieldState;
    }
    catch
    {
    }
    if (string.IsNullOrWhiteSpace(state?.DisplayName) || (state.Visibility & PXUIVisibility.Visible) != PXUIVisibility.Visible)
      return;
    if (this.dllResourceTypesToCollect.Includes(DllResourceTypesToCollect.DisplayName))
    {
      LocalizationResourceType resourceType = cache.BqlTable == cacheType ? LocalizationResourceType.DisplayName : LocalizationResourceType.VirtualTableDisplayName;
      string nameLocalizationKey = PXUIFieldKeyGenerator.GetDacFieldNameLocalizationKey(typeNameForKey);
      result.AddResource(new LocalizationResourceLite(nameLocalizationKey, resourceType, state.DisplayName));
    }
    if (!localizeLists)
      return;
    this.ReadLists(state, typeNameForKey, field, result);
  }

  private static IEnumerable<object> GetAttributesForField(
    MemberInfo prop,
    System.Type propType,
    bool inherit)
  {
    if (prop != (MemberInfo) null && propType != (System.Type) null)
      return ((IEnumerable<object>) prop.GetCustomAttributes(inherit)).Concat<object>((IEnumerable<object>) propType.GetCustomAttributes(inherit));
    if (prop != (MemberInfo) null)
      return (IEnumerable<object>) prop.GetCustomAttributes(inherit);
    return propType != (System.Type) null ? (IEnumerable<object>) propType.GetCustomAttributes(inherit) : Enumerable.Empty<object>();
  }

  private bool TryGetLocalizableValues(
    System.Type cacheType,
    MemberInfo prop,
    System.Type propType,
    ResourceCollection result,
    string field)
  {
    if (!this.dllResourceTypesToCollect.Includes(DllResourceTypesToCollect.ListAttribute))
      return false;
    bool localizableValues1 = false;
    foreach (object obj in PXDllRipper.GetAttributesForField(prop, propType, true))
    {
      if (obj.GetType().GetInterface("ILocalizableValues") != (System.Type) null)
      {
        ILocalizableValues localizableValues2 = (ILocalizableValues) obj;
        this.ReadDbStringList(result, localizableValues2.Key, field, localizableValues2.Values);
        localizableValues1 = true;
      }
    }
    return localizableValues1;
  }

  private bool TryGetLocalizableValues(
    IEnumerable<PXEventSubscriberAttribute> attributes,
    ResourceCollection result,
    string field)
  {
    if (!this.dllResourceTypesToCollect.Includes(DllResourceTypesToCollect.ListAttribute))
      return false;
    foreach (PXEventSubscriberAttribute attribute in attributes)
    {
      if (attribute is ILocalizableValues localizableValues)
      {
        this.ReadDbStringList(result, localizableValues.Key, field, localizableValues.Values);
        return true;
      }
    }
    return false;
  }

  [Flags]
  private enum UIAttrs
  {
    DisplayName = 1,
    Labels = 2,
    All = Labels | DisplayName, // 0x00000003
    AllExceptDisplayName = Labels, // 0x00000002
  }
}
