// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.WebServices.PXExchangeReflectionHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update.ExchangeService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.WebServices;

public static class PXExchangeReflectionHelper
{
  private static readonly string[] AutochangeEllements = new string[6]
  {
    "ItemId",
    "MimeContent",
    "DateTimeSent",
    "DateTimeCreated",
    "DateTimeReceived",
    "LastModifiedTime"
  };

  public static string GetItemHash<T>(this PXExchangeItem<T> item) where T : ItemType, new()
  {
    string str = PXExchangeReflectionHelper.GetItemText((ItemType) item.Item);
    if (item.Attachments != null)
    {
      foreach (AttachmentType attachment in item.Attachments)
        str = $"{str}|{attachment.Name}|{attachment.Size.ToString()}";
    }
    return PXCriptoHelper.CalculateMD5String(str);
  }

  private static string GetItemText(ItemType item)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      new XmlSerializer(item.GetType()).Serialize((Stream) memoryStream, (object) item);
      memoryStream.Flush();
      memoryStream.Seek(0L, SeekOrigin.Begin);
      XmlDocument xmlDocument = new XmlDocument()
      {
        XmlResolver = (XmlResolver) null
      };
      xmlDocument.Load((Stream) memoryStream);
      List<XmlNode> xmlNodeList = new List<XmlNode>();
      foreach (XmlNode childNode in xmlDocument.DocumentElement.ChildNodes)
      {
        if (childNode.NodeType != XmlNodeType.Comment && ((IEnumerable<string>) PXExchangeReflectionHelper.AutochangeEllements).Contains<string>(childNode.Name))
          xmlNodeList.Add(childNode);
      }
      foreach (XmlNode oldChild in xmlNodeList)
        xmlDocument.DocumentElement.RemoveChild(oldChild);
      memoryStream.Flush();
      memoryStream.Seek(0L, SeekOrigin.Begin);
      memoryStream.SetLength(0L);
      using (XmlWriter w = XmlWriter.Create((Stream) memoryStream, new XmlWriterSettings()
      {
        Indent = true,
        NewLineHandling = NewLineHandling.None
      }))
        xmlDocument.Save(w);
      memoryStream.Flush();
      memoryStream.Seek(0L, SeekOrigin.Begin);
      using (StreamReader streamReader = new StreamReader((Stream) memoryStream))
        return streamReader.ReadToEnd();
    }
  }

  public static bool IsObjectModified<T>(
    PXCache cache,
    object row,
    bool compareVirtuals,
    params string[] ignoreList)
    where T : IBqlTable
  {
    if (row == null)
      return false;
    foreach (string str in compareVirtuals ? cache.Fields.ToArray() : cache.BqlFields.Select<System.Type, string>((Func<System.Type, string>) (f => char.ToUpper(f.Name[0]).ToString() + f.Name.Substring(1))).ToArray<string>())
    {
      if (!((IEnumerable<string>) ignoreList).Contains<string>(str))
      {
        if (!compareVirtuals)
        {
          bool flag = false;
          foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(row, str))
          {
            if (subscriberAttribute is PXDBFieldAttribute)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
            continue;
        }
        System.Type fieldType = cache.GetFieldType(str);
        if (!PXExchangeReflectionHelper.AreValuesEquals(cache.GetValue(row, str), cache.GetValueOriginal(row, str), fieldType, ignoreList))
          return true;
      }
    }
    return false;
  }

  private static bool AreObjectsEqual(object objectA, object objectB, params string[] ignoreList)
  {
    if (objectA == null || objectB == null)
      return object.Equals(objectA, objectB);
    if (objectA.GetType() != objectB.GetType())
      return false;
    foreach (PropertyInfo propertyInfo in ((IEnumerable<PropertyInfo>) objectA.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)).Where<PropertyInfo>((Func<PropertyInfo, bool>) (p => p.CanRead && !((IEnumerable<string>) ignoreList).Contains<string>(p.Name))))
    {
      if (!PXExchangeReflectionHelper.AreValuesEquals(propertyInfo.GetValue(objectA, (object[]) null), propertyInfo.GetValue(objectB, (object[]) null), propertyInfo.PropertyType, ignoreList))
        return false;
    }
    return true;
  }

  private static bool AreValuesEquals(
    object valueA,
    object valueB,
    System.Type type,
    params string[] ignoreList)
  {
    if (PXExchangeReflectionHelper.CanDirectlyCompare(type))
    {
      if (!PXExchangeReflectionHelper.AreValuesEqual(valueA, valueB))
        return false;
    }
    else if (typeof (IEnumerable).IsAssignableFrom(type))
    {
      if (valueA == null && valueB != null || valueA != null && valueB == null)
        return false;
      if (valueA != null && valueB != null)
      {
        IEnumerable<object> source1 = ((IEnumerable) valueA).Cast<object>();
        IEnumerable<object> source2 = ((IEnumerable) valueB).Cast<object>();
        int num1 = source1.Count<object>();
        int num2 = source2.Count<object>();
        if (num1 != num2)
          return false;
        for (int index = 0; index < num1; ++index)
        {
          object obj1 = source1.ElementAt<object>(index);
          object obj2 = source2.ElementAt<object>(index);
          if (obj1 == null || obj2 == null || PXExchangeReflectionHelper.CanDirectlyCompare(obj1.GetType()))
          {
            if (!PXExchangeReflectionHelper.AreValuesEqual(obj1, obj2))
              return false;
          }
          else if (!PXExchangeReflectionHelper.AreObjectsEqual(obj1, obj2, ignoreList))
            return false;
        }
      }
    }
    else if (!type.IsClass || !PXExchangeReflectionHelper.AreObjectsEqual(valueA, valueB, ignoreList))
      return false;
    return true;
  }

  private static bool CanDirectlyCompare(System.Type type)
  {
    return typeof (IComparable).IsAssignableFrom(type) || type.IsPrimitive || type.IsValueType;
  }

  private static bool AreValuesEqual(object valueA, object valueB)
  {
    IComparable comparable = valueA as IComparable;
    if (valueA == null && valueB != null || valueA != null && valueB == null)
      return false;
    if (valueA == null && valueB == null)
      return true;
    if (valueA.GetType() != valueB.GetType())
      return false;
    if (valueA is string && valueB is string)
      return string.Equals(((string) valueA).Trim(), ((string) valueB).Trim());
    return (comparable == null || comparable.CompareTo(valueB) == 0) && object.Equals(valueA, valueB);
  }
}
