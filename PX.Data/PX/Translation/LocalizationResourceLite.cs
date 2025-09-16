// Decompiled with JetBrains decompiler
// Type: PX.Translation.LocalizationResourceLite
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Translation;

/// <exclude />
public class LocalizationResourceLite
{
  private string v1;
  private string displayName;
  private bool v2;

  public static string CurrentScreenID { get; set; }

  public string ResKey { get; private set; }

  public string NeutralValue { get; private set; }

  public string ScreenId { get; private set; }

  public int Type { get; private set; }

  public LocalizationResourceLite(
    string resKey,
    LocalizationResourceType resourceType,
    string neutralValue)
  {
    this.ResKey = resKey;
    this.NeutralValue = neutralValue == null ? (string) null : PXLocalizer.UnescapeString(neutralValue.Trim());
    this.Type = (int) resourceType;
    this.ScreenId = LocalizationResourceLite.CurrentScreenID;
  }

  public LocalizationResourceLite(string v1, string displayName, string _language, bool v2)
  {
    this.v1 = v1;
    this.displayName = displayName;
    this.v2 = v2;
  }

  public override bool Equals(object obj)
  {
    return obj is LocalizationResourceLite localizationResourceLite && string.Compare(this.ResKey, localizationResourceLite.ResKey, StringComparison.InvariantCultureIgnoreCase) == 0 && string.Compare(this.NeutralValue, localizationResourceLite.NeutralValue, StringComparison.InvariantCulture) == 0;
  }

  public override int GetHashCode() => $"{this.ResKey}_{this.NeutralValue}".GetHashCode();
}
