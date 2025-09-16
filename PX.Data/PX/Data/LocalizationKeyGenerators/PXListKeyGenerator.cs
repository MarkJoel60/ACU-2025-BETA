// Decompiled with JetBrains decompiler
// Type: PX.Data.LocalizationKeyGenerators.PXListKeyGenerator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.LocalizationKeyGenerators;

/// <exclude />
public static class PXListKeyGenerator
{
  private const string LOCALIZATIONVALUE_BASE = "->";

  public static string GetLocalizationValue(string neutralDisplayName, string allowedLabel)
  {
    return $"{neutralDisplayName} {"->"} {allowedLabel}";
  }

  public static string GetDacListLocalizationKey(string bqlTableFullName) => bqlTableFullName;

  public static string GetDbListLocalizationKey(string bqlTableFullName) => bqlTableFullName;

  public static string GetDacListCacheAttachedLocalizationKey(
    string bqlTableFullName,
    string graphFullName)
  {
    return $"{bqlTableFullName} {graphFullName}";
  }

  public static string GetCacheExtensionListLocalizationKey(string extentionTypeFullName)
  {
    return extentionTypeFullName;
  }
}
