// Decompiled with JetBrains decompiler
// Type: PX.Data.Localizers.PXSpecialLocalizer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.LocalizationKeyGenerators;
using System.Web;

#nullable disable
namespace PX.Data.Localizers;

/// <exclude />
public class PXSpecialLocalizer
{
  public void LocalizeColorName(ref string colorName)
  {
    string nameLocalizationKey = PXSpecialKeyGenerator.GetColorNameLocalizationKey(colorName);
    colorName = PXLocalizer.Localize(colorName, nameLocalizationKey);
  }

  public void LocalizeFontName(ref string fontName)
  {
    string nameLocalizationKey = PXSpecialKeyGenerator.GetFontNameLocalizationKey(fontName);
    fontName = PXLocalizer.Localize(fontName, nameLocalizationKey);
  }

  public void LocalizeFontFamilyName(ref string familyName)
  {
    string familyLocalizationKey = PXSpecialKeyGenerator.GetFontFamilyLocalizationKey(familyName);
    familyName = PXLocalizer.Localize(familyName, familyLocalizationKey);
  }

  public string LocalizeWorkflowField(string fieldName, string displayName)
  {
    string str = displayName;
    if (!string.IsNullOrEmpty(fieldName))
    {
      string nameLocalizationKey = PXUIFieldKeyGenerator.GetDacFieldNameLocalizationKey(fieldName);
      str = PXLocalizer.Localize(displayName, nameLocalizationKey, false, out bool _);
    }
    return str;
  }

  public string LocalizeWorkflowForm(string formName, string displayName)
  {
    string str = displayName;
    if (!string.IsNullOrEmpty(formName) && HttpContext.Current != null)
    {
      string executionFilePath = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath;
      string localizationKey = PXControlKeyGenerator.GetLocalizationKey(displayName, executionFilePath, formName, "FormName");
      str = PXLocalizer.Localize(displayName, localizationKey, false, out bool _);
    }
    return str;
  }
}
