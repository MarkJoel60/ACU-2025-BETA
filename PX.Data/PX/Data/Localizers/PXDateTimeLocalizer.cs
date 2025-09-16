// Decompiled with JetBrains decompiler
// Type: PX.Data.Localizers.PXDateTimeLocalizer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.LocalizationKeyGenerators;
using PX.Translation;

#nullable disable
namespace PX.Data.Localizers;

/// <exclude />
public class PXDateTimeLocalizer
{
  public void ValidateTranslation(PXDBDateAndTimeAttribute dateTime, PXCache cache)
  {
    this.Localize(dateTime, cache, true);
  }

  public void Localize(PXDBDateAndTimeAttribute dateTime, PXCache cache, bool checkTranslation = false)
  {
    if (dateTime == null || cache == null)
      return;
    IDateTimeLocalizationKeyGenerator localizationKeyGenerator = (IDateTimeLocalizationKeyGenerator) null;
    switch (PXPageRipper.GetFieldSourceType(cache, dateTime.FieldName, false))
    {
      case PX.Translation.FieldSourceType.DacField:
      case PX.Translation.FieldSourceType.Undefined:
        localizationKeyGenerator = (IDateTimeLocalizationKeyGenerator) new DateTimeDacKeyGenerator(cache);
        break;
      case PX.Translation.FieldSourceType.DacFieldCacheAttached:
        localizationKeyGenerator = (IDateTimeLocalizationKeyGenerator) new DateTimeCacheAttachedKeyGenerator(cache);
        break;
      case PX.Translation.FieldSourceType.CacheExtensionField:
        localizationKeyGenerator = (IDateTimeLocalizationKeyGenerator) new DateTimeExtensionKeyGenerator(PXPageRipper.GetExtentionWithProperty(cache.GetExtensionTypes(), cache.GetItemType(), dateTime.FieldName));
        break;
    }
    if (!string.IsNullOrWhiteSpace(dateTime.NeutralDisplayNameDate))
    {
      bool isTranslated;
      string str = PXLocalizer.Localize(dateTime.NeutralDisplayNameDate, localizationKeyGenerator.DateKey, out isTranslated);
      if (checkTranslation)
      {
        if (!isTranslated)
          TranslationValidationManager.AddWarning($"DataField_Date '{cache.Graph.GetType().Name}::{cache.BqlTable.Name}::{dateTime.FieldName + "_Date"}'");
      }
      else
        dateTime.DisplayNameDate = str;
    }
    if (string.IsNullOrWhiteSpace(dateTime.NeutralDisplayNameTime))
      return;
    bool isTranslated1;
    string str1 = PXLocalizer.Localize(dateTime.NeutralDisplayNameTime, localizationKeyGenerator.TimeKey, out isTranslated1);
    if (checkTranslation)
    {
      if (isTranslated1)
        return;
      TranslationValidationManager.AddWarning($"DataField_Time '{cache.Graph.GetType().Name}::{cache.BqlTable.Name}::{dateTime.FieldName + "_Time"}'");
    }
    else
      dateTime.DisplayNameTime = str1;
  }
}
