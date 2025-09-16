// Decompiled with JetBrains decompiler
// Type: PX.Data.Localization.XmlUpgrade.Locale_v1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update;
using PX.DbServices.Model.ImportExport.Upgrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace PX.Data.Localization.XmlUpgrade;

public class Locale_v1 : XmlEntityUpgrader
{
  private XElement _root;
  private int? _versionToUpgrade;
  private readonly Dictionary<string, List<XElement>> _translations = new Dictionary<string, List<XElement>>();
  private readonly Dictionary<string, XElement> _values = new Dictionary<string, XElement>();
  private readonly Dictionary<string, List<string>> _idMap = new Dictionary<string, List<string>>();

  public string EntityNameToUpgrade => "Locale";

  public int OrderNumber => 1;

  public int MaxVersionToUpgradeFrom => 20170821;

  public bool Upgrade(XmlEntityBeingUpgraded entity)
  {
    this._root = entity.Data;
    this._versionToUpgrade = new int?(entity.Template.RelationsVersion ?? 1);
    this.ReadTranslations();
    this.UpgradeValues();
    this.RemoveDuplicates();
    return true;
  }

  private IEnumerable<XElement> GetTranslations()
  {
    int? versionToUpgrade = this._versionToUpgrade;
    if (versionToUpgrade.HasValue)
    {
      switch (versionToUpgrade.GetValueOrDefault())
      {
        case 1:
          return this._root.Descendants((XName) "LocalizationTranslation").Elements<XElement>((XName) "row");
      }
    }
    return this._root.Descendants((XName) "LocalizationTranslation");
  }

  private void ReadTranslations()
  {
    foreach (XElement translation in this.GetTranslations())
    {
      string key = translation.Attribute((XName) "IdValue").Value;
      if (!this._translations.ContainsKey(key))
        this._translations[key] = new List<XElement>();
      this._translations[key].Add(translation);
    }
  }

  private void UpgradeValues()
  {
    foreach (XElement element in this._root.Descendants((XName) "LocalizationValue").Elements<XElement>((XName) "row"))
    {
      string str1 = element.Attribute((XName) "NeutralValue").Value;
      string str2 = element.Attribute((XName) "Id").Value;
      string localizationString = PXCriptoHelper.CalculateMD5LocalizationString(str1);
      if (!str2.Equals(localizationString, StringComparison.Ordinal))
      {
        element.Attribute((XName) "Id").Value = localizationString;
        this.UpgradeTranslations(str2, localizationString);
      }
      this._values[str2] = element;
      if (!this._idMap.ContainsKey(localizationString))
        this._idMap[localizationString] = new List<string>();
      this._idMap[localizationString].Add(str2);
    }
  }

  private void UpgradeTranslations(string id, string idNew)
  {
    List<XElement> xelementList;
    if (!this._translations.TryGetValue(id, out xelementList))
      return;
    foreach (XElement xelement in xelementList)
      xelement.Attribute((XName) "IdValue").Value = idNew;
  }

  private void RemoveDuplicates()
  {
    foreach (KeyValuePair<string, List<string>> id in this._idMap)
    {
      if (id.Value.Count > 1)
      {
        foreach (string key in id.Value.Skip<string>(1))
        {
          this._values[key].Remove();
          List<XElement> source;
          if (this._translations.TryGetValue(key, out source))
            source.Remove<XElement>();
        }
      }
    }
  }
}
