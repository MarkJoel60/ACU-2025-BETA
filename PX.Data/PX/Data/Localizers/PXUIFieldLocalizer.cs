// Decompiled with JetBrains decompiler
// Type: PX.Data.Localizers.PXUIFieldLocalizer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.LocalizationKeyGenerators;
using PX.Translation;

#nullable disable
namespace PX.Data.Localizers;

/// <exclude />
public class PXUIFieldLocalizer : IPXObjectLocalizer
{
  public const char AUTOMATION_BUTTON_SYMBOL = '@';

  public void ValidateTranslation(
    PXUIFieldAttribute uiField,
    string bqlTableFullName,
    PXCache fieldCache,
    UIFieldSourceType fieldSourceType)
  {
    this.Localize(uiField, bqlTableFullName, fieldCache, fieldSourceType, true);
  }

  public void Localize(
    PXUIFieldAttribute uiField,
    string bqlTableFullName,
    PXCache fieldCache,
    UIFieldSourceType fieldSourceType,
    bool checkTranslation = false)
  {
    if (uiField == null || string.IsNullOrEmpty(bqlTableFullName) || fieldCache == null || string.IsNullOrEmpty(uiField.NeutralDisplayName.Trim()))
      return;
    switch (fieldSourceType)
    {
      case UIFieldSourceType.ActionName:
        this.LocalizeActionName(uiField, fieldCache, checkTranslation);
        break;
      case UIFieldSourceType.AutomationButtonName:
        this.LocalizeAutomationButtonName(uiField, fieldCache, checkTranslation);
        break;
      case UIFieldSourceType.DacFieldName:
      case UIFieldSourceType.Undefined:
        this.LocalizeDacFieldName(uiField, fieldCache, checkTranslation);
        break;
      case UIFieldSourceType.ParentDacFieldName:
        this.LocalizeParentDacFieldName(uiField, fieldCache, checkTranslation);
        break;
      case UIFieldSourceType.CacheExtensionFieldName:
        this.LocalizeCacheExtensionFieldName(uiField, fieldCache, checkTranslation);
        break;
    }
  }

  private void LocalizeActionName(PXUIFieldAttribute uiField, PXCache cache, bool checkTranslation)
  {
    if (uiField == null || cache == null)
      return;
    string nameLocalizationKey = PXUIFieldKeyGenerator.GetActionNameLocalizationKey(cache.Graph.GetType().FullName);
    bool isTranslated;
    string str = PXLocalizer.Localize(uiField.NeutralDisplayName, nameLocalizationKey, out isTranslated);
    if (checkTranslation)
    {
      if (isTranslated)
        return;
      TranslationValidationManager.AddWarning($"ActionName '{cache.Graph.GetType().Name}::{uiField.FieldName}'");
    }
    else
      uiField.DisplayName = str;
  }

  private void LocalizeAutomationButtonName(
    PXUIFieldAttribute uiField,
    PXCache cache,
    bool checkTranslation)
  {
    if (uiField == null || cache == null)
      return;
    string nameLocalizationKey = PXUIFieldKeyGenerator.GetAutomationButtonNameLocalizationKey(cache.Graph.GetType().FullName);
    bool isTranslated;
    string str = PXLocalizer.Localize(uiField.NeutralDisplayName, nameLocalizationKey, out isTranslated);
    if (checkTranslation)
    {
      if (isTranslated)
        return;
      TranslationValidationManager.AddWarning($"AutomationButton '{cache.Graph.GetType().Name}::{uiField.FieldName}'");
    }
    else
      uiField.DisplayName = str;
  }

  private void LocalizeDacFieldName(
    PXUIFieldAttribute uiField,
    PXCache cache,
    bool checkTranslation)
  {
    if (uiField == null || cache == null)
      return;
    string nameLocalizationKey = PXUIFieldKeyGenerator.GetDacFieldNameLocalizationKey(cache.GetItemType().FullName);
    bool isTranslated;
    string str = PXLocalizer.Localize(uiField.NeutralDisplayName, nameLocalizationKey, false, out isTranslated);
    if (checkTranslation)
    {
      if (isTranslated)
        return;
      TranslationValidationManager.AddWarning($"DataField '{cache.Graph.GetType().Name}::{cache.BqlTable.Name}::{uiField.FieldName}'");
    }
    else
      uiField.DisplayName = str;
  }

  private void LocalizeParentDacFieldName(
    PXUIFieldAttribute uiField,
    PXCache cache,
    bool checkTranslation)
  {
    if (uiField == null || cache == null || !(cache.GetItemType().BaseType != (System.Type) null))
      return;
    string nameLocalizationKey = PXUIFieldKeyGenerator.GetDacFieldNameLocalizationKey(cache.GetItemType().BaseType.FullName);
    bool isTranslated;
    string str = PXLocalizer.Localize(uiField.NeutralDisplayName, nameLocalizationKey, out isTranslated);
    if (checkTranslation)
    {
      if (isTranslated)
        return;
      TranslationValidationManager.AddWarning($"DataField '{cache.Graph.GetType().Name}::{cache.BqlTable.Name}::{uiField.FieldName}'");
    }
    else
      uiField.DisplayName = str;
  }

  private void LocalizeCacheExtensionFieldName(
    PXUIFieldAttribute uiField,
    PXCache cache,
    bool checkTranslation)
  {
    if (uiField == null || cache == null)
      return;
    System.Type extentionWithProperty = PXPageRipper.GetExtentionWithProperty(cache.GetExtensionTypes(), cache.GetItemType(), uiField.FieldName);
    if (!(extentionWithProperty != (System.Type) null))
      return;
    string nameLocalizationKey = PXUIFieldKeyGenerator.GetCacheExtensionNameLocalizationKey(extentionWithProperty.FullName);
    bool isTranslated;
    string str = PXLocalizer.Localize(uiField.NeutralDisplayName, nameLocalizationKey, out isTranslated);
    if (checkTranslation)
    {
      if (isTranslated)
        return;
      TranslationValidationManager.AddWarning($"DataField '{cache.Graph.GetType().Name}::{extentionWithProperty.Name}::{uiField.FieldName}'");
    }
    else
      uiField.DisplayName = str;
  }
}
