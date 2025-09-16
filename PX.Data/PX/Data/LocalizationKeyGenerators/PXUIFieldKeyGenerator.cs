// Decompiled with JetBrains decompiler
// Type: PX.Data.LocalizationKeyGenerators.PXUIFieldKeyGenerator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;

#nullable disable
namespace PX.Data.LocalizationKeyGenerators;

/// <exclude />
public static class PXUIFieldKeyGenerator
{
  private const string AUTOMATIONBUTTON_PREFIX = "AutomationButton";

  public static string GetActionNameLocalizationKey(string fieldGraphFullName)
  {
    return CustomizedTypeManager.GetTypeNotCustomized(fieldGraphFullName);
  }

  public static string GetAutomationButtonNameLocalizationKey(string fieldGraphFullName)
  {
    return $"{"AutomationButton"} {CustomizedTypeManager.GetTypeNotCustomized(fieldGraphFullName)}";
  }

  public static string GetCacheExtensionNameLocalizationKey(string extentionTypeFullName)
  {
    return extentionTypeFullName;
  }

  public static string GetDacFieldNameLocalizationKey(string dacTypeFullName) => dacTypeFullName;
}
