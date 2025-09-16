// Decompiled with JetBrains decompiler
// Type: PX.Data.Localizers.PXControlLocalizer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.LocalizationKeyGenerators;
using System;

#nullable disable
namespace PX.Data.Localizers;

/// <exclude />
public class PXControlLocalizer
{
  public string Localize(
    string value,
    string pageName,
    string controlId,
    string propName,
    bool bypassTranslationWarning = false)
  {
    string original = PagePathConverter.ToOriginal(pageName);
    string localizationKey = PXControlKeyGenerator.GetLocalizationKey(value, original, controlId, propName);
    bool isTranslated;
    string str = PXLocalizer.Localize(value, localizationKey, out isTranslated);
    if (!bypassTranslationWarning && !isTranslated && !string.IsNullOrEmpty(pageName) && !string.IsNullOrEmpty(value))
    {
      string strA = (string) null;
      try
      {
        strA = PXSiteMap.CurrentScreenID;
      }
      catch (PXNotLoggedInException ex)
      {
      }
      catch (PXUndefinedCompanyException ex)
      {
      }
      int num = pageName.IndexOf(".aspx");
      if (!string.IsNullOrEmpty(strA) && num >= strA.Length)
      {
        string strB = pageName.Substring(num - strA.Length, strA.Length);
        if (string.Compare(strA, strB, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(pageName, "~/GenericInquiry/GenericInquiry.aspx", StringComparison.OrdinalIgnoreCase) == 0)
          TranslationValidationManager.AddWarning($"ControlProperty '{pageName}::{controlId}::{propName}' with value '{value}'");
      }
    }
    return str;
  }
}
