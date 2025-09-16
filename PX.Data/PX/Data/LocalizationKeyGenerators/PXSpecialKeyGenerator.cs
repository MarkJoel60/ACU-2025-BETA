// Decompiled with JetBrains decompiler
// Type: PX.Data.LocalizationKeyGenerators.PXSpecialKeyGenerator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.LocalizationKeyGenerators;

/// <exclude />
public static class PXSpecialKeyGenerator
{
  private const string COLORNAME_PREFIX = "ColorName:";
  private const string FONTNAME_PREFIX = "FontName:";
  private const string FONTFAMILY_PREFIX = "FontFamily";
  private const string THEMEVARIABLE_PREFIX = "ThemeVariable";

  public static string GetColorNameLocalizationKey(string colorName)
  {
    return $"{"ColorName:"} {colorName}";
  }

  public static string GetFontNameLocalizationKey(string fontName) => $"{"FontName:"} {fontName}";

  public static string GetFontFamilyLocalizationKey(string fontFamily)
  {
    return $"{"FontFamily"} {fontFamily}";
  }

  public static string GetThemeVariableLocalizationKey(
    (string Theme, string VariableDisplayName) variable)
  {
    return $"ThemeVariable {variable.Theme} {variable.VariableDisplayName}";
  }

  public static string GetMessageLocalizationKey(string messageContainerFullName)
  {
    return messageContainerFullName;
  }
}
