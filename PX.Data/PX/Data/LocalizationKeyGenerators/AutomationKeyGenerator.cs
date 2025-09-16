// Decompiled with JetBrains decompiler
// Type: PX.Data.LocalizationKeyGenerators.AutomationKeyGenerator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;

#nullable disable
namespace PX.Data.LocalizationKeyGenerators;

/// <exclude />
public static class AutomationKeyGenerator
{
  private const string KEYBASE = "AutomationButton";

  public static string GetLocalizationKey(string graphTypeFullName)
  {
    return $"{"AutomationButton"} {CustomizedTypeManager.GetTypeNotCustomized(graphTypeFullName)}";
  }
}
