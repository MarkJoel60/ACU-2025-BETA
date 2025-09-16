// Decompiled with JetBrains decompiler
// Type: PX.Data.UserRecords.RecordCachedContentBuilder
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

#nullable disable
namespace PX.Data.UserRecords;

[PXInternalUseOnly]
public class RecordCachedContentBuilder : IRecordCachedContentBuilder
{
  public string BuildCachedContent(PXGraph screenGraph, IBqlTable entity)
  {
    ExceptionExtensions.ThrowOnNull<PXGraph>(screenGraph, nameof (screenGraph), (string) null);
    if (entity == null)
      return (string) null;
    string friendlyEntityName = EntityHelper.GetFriendlyEntityName(entity.GetType(), (object) entity);
    if (string.IsNullOrWhiteSpace(friendlyEntityName))
      return (string) null;
    string entityDescription = this.GetEntityDescription(screenGraph, entity);
    if (string.IsNullOrWhiteSpace(entityDescription))
      return (string) null;
    XElement cachedRecordXml = new XElement((XName) "record", new object[2]
    {
      (object) new XAttribute((XName) "Name", (object) friendlyEntityName),
      (object) new XAttribute((XName) "Description", (object) entityDescription)
    });
    IEnumerable<XElement> content = this.GetSearchableInfo(screenGraph, entity, friendlyEntityName).Select<KeyValuePair<string, string>, XElement>((Func<KeyValuePair<string, string>, XElement>) (kvp => new XElement((XName) "pair", new object[2]
    {
      (object) new XAttribute((XName) "Name", (object) kvp.Key),
      (object) new XAttribute((XName) "Value", (object) kvp.Value)
    })));
    cachedRecordXml.Add((object) content);
    return this.WriteXmlToStringWithUnicodeSymbolsSupport(cachedRecordXml);
  }

  protected string GetEntityDescription(PXGraph screenGraph, IBqlTable entity)
  {
    string entityDescription = EntityHelper.GetEntityDescription(screenGraph, (object) entity);
    return !string.IsNullOrWhiteSpace(entityDescription) ? entityDescription : new EntityHelper(screenGraph).GetEntityKeysDescription(entity);
  }

  protected IEnumerable<KeyValuePair<string, string>> GetSearchableInfo(
    PXGraph screenGraph,
    IBqlTable entity,
    string friendlyEntityName)
  {
    System.Type entityType = entity.GetType();
    PXCache cach = screenGraph.Caches[entityType];
    PXSearchableAttribute searchableAttribute = PXSearchableAttribute.GetSearchableAttribute(cach);
    if (searchableAttribute == null)
      return Enumerable.Empty<KeyValuePair<string, string>>();
    IReadOnlyCollection<System.Type> fieldsForUserRecords = searchableAttribute.GetSearchableFieldsForUserRecords();
    if (fieldsForUserRecords == null || fieldsForUserRecords.Count == 0)
      return Enumerable.Empty<KeyValuePair<string, string>>();
    Dictionary<System.Type, object> values = searchableAttribute.ExtractValues(cach, (object) entity, (PXResult) null, (IEnumerable<System.Type>) fieldsForUserRecords);
    if (values == null || values.Count == 0)
      return Enumerable.Empty<KeyValuePair<string, string>>();
    foreach (System.Type key in fieldsForUserRecords.Where<System.Type>((Func<System.Type, bool>) (fieldType => !this.IsValidEntityKeyField(screenGraph, entityType, fieldType))))
      values.Remove(key);
    return values.Count == 0 ? Enumerable.Empty<KeyValuePair<string, string>>() : this.ProcessSearchableFieldValues(cach, entityType, entity, friendlyEntityName, values);
  }

  private bool IsValidEntityKeyField(PXGraph screenGraph, System.Type entityType, System.Type fieldType)
  {
    System.Type declaringType = fieldType.DeclaringType;
    if (declaringType == (System.Type) null)
      return false;
    PXCache cach = screenGraph.Caches[declaringType];
    if (entityType != declaringType && cach.Keys.Contains(fieldType.Name) || !(cach.GetStateExt((object) null, fieldType.Name) is PXFieldState stateExt) || !stateExt.Visible || stateExt.Visibility == PXUIVisibility.Invisible || stateExt.Visibility == PXUIVisibility.HiddenByAccessRights)
      return false;
    PXSelectorAttribute selectorAttribute = cach.GetAttributesReadonly(fieldType.Name, true).OfType<PXSelectorAttribute>().FirstOrDefault<PXSelectorAttribute>();
    if (selectorAttribute?.SubstituteKey != (System.Type) null)
      return true;
    try
    {
      PropertyInfo property = declaringType.GetProperty(fieldType.Name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
      return selectorAttribute == null || property?.PropertyType == typeof (string);
    }
    catch (AmbiguousMatchException ex)
    {
      return false;
    }
  }

  private IEnumerable<KeyValuePair<string, string>> ProcessSearchableFieldValues(
    PXCache entityCache,
    System.Type entityType,
    IBqlTable entity,
    string friendlyEntityName,
    Dictionary<System.Type, object> extractedValues)
  {
    foreach (List<(System.Type, string, string)> valueTupleList in extractedValues.Where<KeyValuePair<System.Type, object>>((Func<KeyValuePair<System.Type, object>, bool>) (typeAndValue => typeAndValue.Value != null)).Select<KeyValuePair<System.Type, object>, (System.Type, string, string)>((Func<KeyValuePair<System.Type, object>, (System.Type, string, string)>) (typeAndValue =>
    {
      KeyValuePair<System.Type, object> keyValuePair = typeAndValue;
      System.Type key = keyValuePair.Key;
      keyValuePair = typeAndValue;
      string searchFieldName = this.GetSearchFieldName(keyValuePair.Key, entityCache, entityType, entity);
      keyValuePair = typeAndValue;
      string str = keyValuePair.Value.ToString();
      return (key, searchFieldName, str);
    })).Where<(System.Type, string, string)>((Func<(System.Type, string, string), bool>) (fieldInfo => !string.IsNullOrWhiteSpace(fieldInfo.Value))).GroupBy<(System.Type, string, string), string>((Func<(System.Type, string, string), string>) (fieldInfo => fieldInfo.FieldName)).Select<IGrouping<string, (System.Type, string, string)>, List<(System.Type, string, string)>>((Func<IGrouping<string, (System.Type, string, string)>, List<(System.Type, string, string)>>) (group => group.ToList<(System.Type, string, string)>())))
    {
      if (valueTupleList.Count == 1)
      {
        (System.Type, string, string) valueTuple = valueTupleList[0];
        yield return new KeyValuePair<string, string>(valueTuple.Item2, valueTuple.Item3);
      }
      else
      {
        foreach ((System.Type type, string str1, string str2) in valueTupleList)
        {
          System.Type declaringType = type.DeclaringType;
          string str3 = declaringType == entityType ? friendlyEntityName : EntityHelper.GetFriendlyEntityName(declaringType);
          if (!string.IsNullOrWhiteSpace(str3))
            yield return new KeyValuePair<string, string>($"{str3} {str1}", str2);
        }
      }
    }
  }

  private string GetSearchFieldName(
    System.Type searchFieldType,
    PXCache entityCache,
    System.Type entityType,
    IBqlTable entity)
  {
    System.Type declaringType = searchFieldType.DeclaringType;
    if (declaringType == (System.Type) null)
      return searchFieldType.Name;
    return entityType == declaringType ? entityCache.GetAttributesOfType<PXUIFieldAttribute>((object) entity, searchFieldType.Name).FirstOrDefault<PXUIFieldAttribute>()?.DisplayName ?? searchFieldType.Name : entityCache.Graph.Caches[declaringType].GetAttributesReadonly(searchFieldType.Name).OfType<PXUIFieldAttribute>().FirstOrDefault<PXUIFieldAttribute>()?.DisplayName ?? searchFieldType.Name;
  }

  protected string WriteXmlToStringWithUnicodeSymbolsSupport(XElement cachedRecordXml)
  {
    XmlWriterSettings settings = new XmlWriterSettings()
    {
      CheckCharacters = false,
      Indent = false
    };
    StringBuilder output = new StringBuilder();
    using (XmlWriter writer = XmlWriter.Create(output, settings))
      cachedRecordXml.WriteTo(writer);
    return output.ToString();
  }
}
