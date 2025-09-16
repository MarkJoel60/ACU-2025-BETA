// Decompiled with JetBrains decompiler
// Type: PX.Data.LocalizationKeyGenerators.XmlCommentKeyGenerator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.LocalizationKeyGenerators;

/// <summary>
/// A generator of localization resource keys for XML comments.
/// </summary>
internal static class XmlCommentKeyGenerator
{
  private const string XmlCommentSummaryKeyPrefix = "XmlCommentSummary";
  private const string XmlCommentRemarksKeyPrefix = "XmlCommentRemarks";
  private const string XmlCommentValueKeyPrefix = "XmlCommentValue";

  public static string GetDacOrDacExtensionXmlCommentSummaryLocalizationKey(
    System.Type dacOrDacExtensionType)
  {
    return XmlCommentKeyGenerator.GetDacOrDacExtensionLocalizationKey(dacOrDacExtensionType, "XmlCommentSummary");
  }

  public static string GetDacOrDacExtensionXmlCommentRemarkLocalizationKey(
    System.Type dacOrDacExtensionType)
  {
    return XmlCommentKeyGenerator.GetDacOrDacExtensionLocalizationKey(dacOrDacExtensionType, "XmlCommentRemarks");
  }

  private static string GetDacOrDacExtensionLocalizationKey(
    System.Type dacOrDacExtensionType,
    string prefix)
  {
    ExceptionExtensions.ThrowOnNull<System.Type>(dacOrDacExtensionType, nameof (dacOrDacExtensionType), (string) null);
    return $"{prefix} {dacOrDacExtensionType.FullName}";
  }

  public static string GetDacPropertyXmlCommentSummaryLocalizationKey(
    System.Type dacOrDacExtensionType,
    string dacPropertyName)
  {
    return XmlCommentKeyGenerator.GetPropetyLocalizationKey(dacOrDacExtensionType, dacPropertyName, "XmlCommentSummary");
  }

  public static string GetDacPropertyXmlCommentRemarksLocalizationKey(
    System.Type dacOrDacExtensionType,
    string dacPropertyName)
  {
    return XmlCommentKeyGenerator.GetPropetyLocalizationKey(dacOrDacExtensionType, dacPropertyName, "XmlCommentRemarks");
  }

  public static string GetDacPropertyXmlCommentValueLocalizationKey(
    System.Type dacOrDacExtensionType,
    string dacPropertyName)
  {
    return XmlCommentKeyGenerator.GetPropetyLocalizationKey(dacOrDacExtensionType, dacPropertyName, "XmlCommentValue");
  }

  private static string GetPropetyLocalizationKey(
    System.Type dacOrDacExtensionType,
    string dacPropertyName,
    string prefix)
  {
    ExceptionExtensions.ThrowOnNull<System.Type>(dacOrDacExtensionType, nameof (dacOrDacExtensionType), (string) null);
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(dacPropertyName, nameof (dacOrDacExtensionType), (string) null);
    return $"{prefix} {dacOrDacExtensionType.FullName} {dacPropertyName}";
  }
}
