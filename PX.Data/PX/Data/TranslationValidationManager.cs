// Decompiled with JetBrains decompiler
// Type: PX.Data.TranslationValidationManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace PX.Data;

/// <exclude />
/// <exclude />
public static class TranslationValidationManager
{
  private const string WARNINGS_KEY = "TranslationValidationWarnings";
  private const string DEFINITION_KEY = "TranslationValidationDefinition";

  private static SortedSet<string> Warnings
  {
    get
    {
      if (!(PXContext.Session["TranslationValidationWarnings"] is SortedSet<string> warnings))
      {
        warnings = new SortedSet<string>();
        PXContext.Session["TranslationValidationWarnings"] = (object) warnings;
      }
      return warnings;
    }
  }

  public static void InitializePageValidation() => TranslationValidationManager.Warnings.Clear();

  public static void AddWarning(string warning)
  {
    if (string.IsNullOrEmpty(warning) || !TranslationValidationManager.ValidateCurrentLocale())
      return;
    TranslationValidationManager.Warnings.Add(warning);
  }

  public static IEnumerable<string> GetWarnings()
  {
    return (IEnumerable<string>) TranslationValidationManager.Warnings;
  }

  public static void ValidatePage(Control form)
  {
    if (!TranslationValidationManager.ValidateCurrentLocale() || TranslationValidationManager.Warnings.Count <= 0 || form == null)
      return;
    TranslationValidationManager.RenderWarnings(form);
  }

  public static bool ValidateCurrentLocale()
  {
    bool flag = false;
    TranslationValidationManager.Definition definition = PXContext.GetSlot<TranslationValidationManager.Definition>();
    if (definition == null)
      definition = PXContext.SetSlot<TranslationValidationManager.Definition>(PXDatabase.GetSlot<TranslationValidationManager.Definition>("TranslationValidationDefinition", typeof (Locale)));
    if (definition != null)
      flag = definition.LocalesToValidate.Contains(CultureInfo.CurrentCulture.Name);
    return flag;
  }

  private static string GetCurrentLocaleLink()
  {
    return $"<a href=\"{$"{new HyperLink().ResolveUrl(HttpUtility.UrlPathEncode("~/Main"))}?ScreenId={"SM200540"}&{"Language"}={PXLocalesProvider.GetCurrentLocale()}"}\" target=\"_blank\">{CultureInfo.CurrentCulture.NativeName}</a>";
  }

  private static void RenderWarnings(Control form)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("<div class='ValidationBanner' onclick='javascript:document.getElementById(\"TranslationTableContent\").style.display=\"\";'>\r\n\t<div class='ValidationMessage'>\r\n\t\tTranslation Validation: {0} Warnings\r\n\t</div>\r\n\t<iframe name='VSTargetFrame' style='width:1px;height:1px;border:0px;display:inline-block;'>\r\n\t</iframe>\r\n</div>\r\n<table class='ValidationTable' style='display:none' id='TranslationTableContent'>\r\n\t<colgroup>\r\n\t\t<col width='100px' />\r\n\t\t<col width='100%' />\r\n\t</colgroup>", (object) TranslationValidationManager.Warnings.Count);
    foreach (string warning in TranslationValidationManager.Warnings)
      stringBuilder.AppendFormat("<tr><td>{0}</td><td>{1} is not translated in current locale {2}</td></tr>", (object) "Warning: ", (object) warning, (object) TranslationValidationManager.GetCurrentLocaleLink());
    stringBuilder.AppendLine("</table>");
    form.Controls.AddAt(0, (Control) new LiteralControl(stringBuilder.ToString()));
  }

  private class Definition : IPrefetchable, IPXCompanyDependent
  {
    public HashSet<string> LocalesToValidate { get; private set; }

    public void Prefetch()
    {
      this.LocalesToValidate = new HashSet<string>(((IEnumerable<PXLocale>) PXLocalesProvider.GetLocales()).Where<PXLocale>((Func<PXLocale, bool>) (locale =>
      {
        bool? validationWarnings = locale.ShowValidationWarnings;
        bool flag = true;
        return validationWarnings.GetValueOrDefault() == flag & validationWarnings.HasValue;
      })).Select<PXLocale, string>((Func<PXLocale, string>) (locale => locale.Name)));
    }
  }
}
