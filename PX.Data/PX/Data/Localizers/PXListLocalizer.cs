// Decompiled with JetBrains decompiler
// Type: PX.Data.Localizers.PXListLocalizer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.LocalizationKeyGenerators;
using PX.Translation;

#nullable disable
namespace PX.Data.Localizers;

/// <exclude />
public class PXListLocalizer : IPXObjectLocalizer
{
  public void ValidateTranslation(
    string fieldName,
    PXCache listCache,
    string[] neutralAllowedLabels,
    string[] allowedLabels)
  {
    this.Localize(fieldName, listCache, neutralAllowedLabels, allowedLabels, true);
  }

  public void Localize(
    string fieldName,
    PXCache listCache,
    string[] neutralAllowedLabels,
    string[] allowedLabels,
    bool checkTranslation = false)
  {
    if (string.IsNullOrEmpty(fieldName) || neutralAllowedLabels == null || allowedLabels == null || listCache == null)
      return;
    string neutralDisplayName = PXUIFieldAttribute.GetNeutralDisplayName(listCache, fieldName);
    switch (PXPageRipper.GetFieldSourceType(listCache, fieldName, false))
    {
      case PX.Translation.FieldSourceType.DacField:
        this.LocalizeDacList(fieldName, allowedLabels, neutralAllowedLabels, neutralDisplayName, listCache, checkTranslation);
        break;
      case PX.Translation.FieldSourceType.DacFieldCacheAttached:
        this.LocalizeDacListCacheAttached(fieldName, allowedLabels, neutralAllowedLabels, neutralDisplayName, listCache, checkTranslation);
        break;
      case PX.Translation.FieldSourceType.CacheExtensionField:
        this.LocalizeCacheExtensionList(fieldName, allowedLabels, neutralAllowedLabels, neutralDisplayName, listCache, checkTranslation);
        break;
    }
  }

  private bool LocalizedValueIsValid(string value, string localizedValue)
  {
    return !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(localizedValue) && value != localizedValue;
  }

  private void LocalizeDacList(
    string fieldName,
    string[] allowedLabels,
    string[] neutralAllowedLabels,
    string neutralDisplayName,
    PXCache cache,
    bool checkTranslation)
  {
    string listLocalizationKey = PXListKeyGenerator.GetDacListLocalizationKey(cache.BqlTable.FullName);
    for (int currentIndex = 0; currentIndex < allowedLabels.Length; ++currentIndex)
    {
      if (neutralAllowedLabels.Length > currentIndex && !string.IsNullOrEmpty(neutralAllowedLabels[currentIndex]))
      {
        string localizationValue = PXListKeyGenerator.GetLocalizationValue(neutralDisplayName, neutralAllowedLabels[currentIndex]);
        bool isTranslated;
        bool isNotLocalizable;
        string localizedValue = PXLocalizer.Localize(localizationValue, listLocalizationKey, false, out isTranslated, out isNotLocalizable);
        if (checkTranslation)
        {
          if (!isTranslated)
            TranslationValidationManager.AddWarning($"DACList '{cache.Graph.GetType().Name}::{cache.BqlTable.Name}::{fieldName}'");
        }
        else
        {
          if (!isTranslated && !isNotLocalizable)
            localizedValue = PXMessages.LocalizeNoPrefix(neutralAllowedLabels[currentIndex]);
          if (this.LocalizedValueIsValid(localizationValue, localizedValue))
            allowedLabels[currentIndex] = localizedValue;
          else
            this.NormalizeLabels(neutralAllowedLabels, allowedLabels, currentIndex);
        }
      }
    }
  }

  private void LocalizeDacListCacheAttached(
    string fieldName,
    string[] allowedLabels,
    string[] neutralAllowedLabels,
    string neutralDisplayName,
    PXCache cache,
    bool checkTranslation)
  {
    string attachedLocalizationKey = PXListKeyGenerator.GetDacListCacheAttachedLocalizationKey(cache.BqlTable.FullName, cache.Graph.GetType().FullName);
    for (int currentIndex = 0; currentIndex < allowedLabels.Length; ++currentIndex)
    {
      if (neutralAllowedLabels.Length > currentIndex && !string.IsNullOrEmpty(neutralAllowedLabels[currentIndex]))
      {
        string localizationValue = PXListKeyGenerator.GetLocalizationValue(neutralDisplayName, neutralAllowedLabels[currentIndex]);
        bool isTranslated;
        string localizedValue = PXLocalizer.Localize(localizationValue, attachedLocalizationKey, out isTranslated);
        if (checkTranslation)
        {
          if (!isTranslated)
            TranslationValidationManager.AddWarning($"DACListCacheAttached '{cache.Graph.GetType().Name}::{cache.BqlTable.Name}::{fieldName}'");
        }
        else if (this.LocalizedValueIsValid(localizationValue, localizedValue))
          allowedLabels[currentIndex] = localizedValue;
        else
          this.NormalizeLabels(neutralAllowedLabels, allowedLabels, currentIndex);
      }
    }
  }

  private void LocalizeCacheExtensionList(
    string fieldName,
    string[] allowedLabels,
    string[] neutralAllowedLabels,
    string neutralDisplayName,
    PXCache cache,
    bool checkTranslation)
  {
    System.Type extentionWithProperty = PXPageRipper.GetExtentionWithProperty(cache.GetExtensionTypes(), cache.GetItemType(), fieldName);
    if (!(extentionWithProperty != (System.Type) null))
      return;
    string listLocalizationKey = PXListKeyGenerator.GetCacheExtensionListLocalizationKey(extentionWithProperty.FullName);
    for (int currentIndex = 0; currentIndex < allowedLabels.Length; ++currentIndex)
    {
      if (neutralAllowedLabels.Length > currentIndex && !string.IsNullOrEmpty(neutralAllowedLabels[currentIndex]))
      {
        string localizationValue = PXListKeyGenerator.GetLocalizationValue(neutralDisplayName, neutralAllowedLabels[currentIndex]);
        bool isTranslated;
        string localizedValue = PXLocalizer.Localize(localizationValue, listLocalizationKey, out isTranslated);
        if (checkTranslation)
        {
          if (!isTranslated)
            TranslationValidationManager.AddWarning($"DACListExtension '{cache.Graph.GetType().Name}::{cache.BqlTable.Name}::{fieldName}'");
        }
        else if (this.LocalizedValueIsValid(localizationValue, localizedValue))
          allowedLabels[currentIndex] = localizedValue;
        else
          this.NormalizeLabels(neutralAllowedLabels, allowedLabels, currentIndex);
      }
    }
  }

  private void NormalizeLabels(
    string[] neutralAllowedLabels,
    string[] allowedLabels,
    int currentIndex)
  {
    if (string.CompareOrdinal(neutralAllowedLabels[currentIndex], allowedLabels[currentIndex]) == 0)
      return;
    allowedLabels[currentIndex] = neutralAllowedLabels[currentIndex];
  }
}
