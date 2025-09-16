// Decompiled with JetBrains decompiler
// Type: PX.Data.PXEnumDescriptionAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Translation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;

#nullable disable
namespace PX.Data;

/// <exclude />
[AttributeUsage(AttributeTargets.Field)]
public sealed class PXEnumDescriptionAttribute : Attribute
{
  private string _displayName;
  private string _displayNameKey;
  private System.Type _enumType;
  private string _field;
  private string _category;
  private static object _syncObj = new object();
  private static Hashtable _enumsInfoNotLocalizable = new Hashtable();

  private static Hashtable GetEnumsInfo(bool localize)
  {
    if (!localize)
      return PXEnumDescriptionAttribute._enumsInfoNotLocalizable;
    return PXDatabase.GetSlot<Hashtable>("PXEnumDescriptionAttribute$EnumsInfo$" + Thread.CurrentThread.CurrentUICulture.Name, typeof (LocalizationTranslation));
  }

  public PXEnumDescriptionAttribute(string displayName, System.Type keyType)
  {
    this._displayName = displayName;
    this._displayNameKey = keyType.ToString();
  }

  /// <summary>Get, set.</summary>
  public string Category
  {
    get => this._category;
    set => this._category = value;
  }

  /// <summary>Get, set.</summary>
  public System.Type EnumType
  {
    get => this._enumType;
    set
    {
      this._enumType = !(this._enumType == (System.Type) null) && typeof (Enum).IsAssignableFrom(this._enumType) ? value : throw new PXException("Not enum type");
    }
  }

  /// <summary>Get, set.</summary>
  public string Field
  {
    get => this._field;
    set
    {
      this._field = !string.IsNullOrEmpty(this._field) ? value : throw new PXException("Bad enum field name");
    }
  }

  /// <summary>Get.</summary>
  public string DisplayName
  {
    get
    {
      string str = (string) null;
      if (this._enumType != (System.Type) null && !string.IsNullOrEmpty(this._field) && !CultureInfo.InvariantCulture.Equals((object) Thread.CurrentThread.CurrentCulture))
        str = PXLocalizer.Localize(this._displayName, this._displayNameKey);
      return !string.IsNullOrEmpty(str) ? str : this._displayName;
    }
  }

  public static string[] GetNames(System.Type @enum)
  {
    return new List<string>((IEnumerable<string>) PXEnumDescriptionAttribute.GetValueNamePairs(@enum).Values).ToArray();
  }

  public static IDictionary<object, string> GetValueNamePairs(System.Type @enum, bool localize = true)
  {
    return PXEnumDescriptionAttribute.GetValueNamePairs(@enum, (string) null, localize);
  }

  public static IDictionary<object, string> GetValueNamePairs(
    System.Type @enum,
    string categoryName,
    bool localize = true)
  {
    string key1 = $"{@enum.FullName}__{categoryName}";
    Hashtable enumsInfo = PXEnumDescriptionAttribute.GetEnumsInfo(localize);
    lock (PXEnumDescriptionAttribute._syncObj)
    {
      if (enumsInfo.ContainsKey((object) key1))
        return enumsInfo[(object) key1] as IDictionary<object, string>;
      bool flag = string.IsNullOrEmpty(categoryName);
      Dictionary<object, string> valueNamePairs = new Dictionary<object, string>();
      foreach (KeyValuePair<object, KeyValuePair<string, string>> keyValuePair1 in (IEnumerable<KeyValuePair<object, KeyValuePair<string, string>>>) PXEnumDescriptionAttribute.GetFullInfoUnSafelly(@enum, localize))
      {
        KeyValuePair<string, string> keyValuePair2 = keyValuePair1.Value;
        string[] array = keyValuePair2.Key.Split(',');
        if (flag || Array.Find<string>(array, (Predicate<string>) (s => string.Compare(s, categoryName, true) == 0)) != null)
        {
          Dictionary<object, string> dictionary = valueNamePairs;
          object key2 = keyValuePair1.Key;
          keyValuePair2 = keyValuePair1.Value;
          string str = keyValuePair2.Value;
          dictionary.Add(key2, str);
        }
      }
      enumsInfo.Add((object) key1, (object) valueNamePairs);
      return (IDictionary<object, string>) valueNamePairs;
    }
  }

  public static IDictionary<object, KeyValuePair<string, string>> GetFullInfo(
    System.Type @enum,
    bool localize = false)
  {
    lock (PXEnumDescriptionAttribute._syncObj)
      return PXEnumDescriptionAttribute.GetFullInfoUnSafelly(@enum, localize);
  }

  private static IDictionary<object, KeyValuePair<string, string>> GetFullInfoUnSafelly(
    System.Type @enum,
    bool localize = true)
  {
    string key1 = @enum.FullName + "_";
    Hashtable enumsInfo = PXEnumDescriptionAttribute.GetEnumsInfo(localize);
    if (enumsInfo.ContainsKey((object) key1))
      return enumsInfo[(object) key1] as IDictionary<object, KeyValuePair<string, string>>;
    Dictionary<object, KeyValuePair<string, string>> fullInfoUnSafelly = new Dictionary<object, KeyValuePair<string, string>>();
    foreach (System.Reflection.FieldInfo field in @enum.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField))
    {
      object key2 = Enum.Parse(@enum, field.Name);
      KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>((string) null, field.Name);
      if (Attribute.GetCustomAttribute((MemberInfo) field, typeof (PXEnumDescriptionAttribute)) is PXEnumDescriptionAttribute customAttribute)
      {
        customAttribute._enumType = @enum;
        customAttribute._field = field.Name;
        keyValuePair = new KeyValuePair<string, string>(customAttribute._category, localize ? customAttribute.DisplayName : customAttribute._displayName);
      }
      fullInfoUnSafelly.Add(key2, keyValuePair);
    }
    enumsInfo.Add((object) key1, (object) fullInfoUnSafelly);
    return (IDictionary<object, KeyValuePair<string, string>>) fullInfoUnSafelly;
  }

  public static KeyValuePair<string, string> GetInfo(System.Type @enum, object value)
  {
    string name = Enum.GetName(@enum, value);
    return !string.IsNullOrEmpty(name) && Attribute.GetCustomAttribute((MemberInfo) @enum.GetField(name), typeof (PXEnumDescriptionAttribute)) is PXEnumDescriptionAttribute customAttribute ? new KeyValuePair<string, string>(customAttribute.Category, customAttribute.DisplayName) : new KeyValuePair<string, string>();
  }
}
