// Decompiled with JetBrains decompiler
// Type: PX.Translation.PXControllRipper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.LocalizationKeyGenerators;
using PX.SM;
using System.Web;

#nullable disable
namespace PX.Translation;

/// <exclude />
public static class PXControllRipper
{
  public const string FORBIDDEN_VALUE_BEGINNING = "<div class='size-xs inline-block'>";

  public static void RipControl(
    string value,
    string pageName,
    string controlId,
    string propName,
    CollectResourceSettings resourceSettings)
  {
    string original = PagePathConverter.ToOriginal(pageName);
    PXControlPropertiesCollector collector = PXPageRipper.GetCollector(HttpContext.Current);
    if (collector == null)
      return;
    string localizationKey = PXControlKeyGenerator.GetLocalizationKey(value, original, controlId, propName);
    collector.Collect(new LocalizationResourceLite(localizationKey, LocalizationResourceType.Control, value), resourceSettings);
  }
}
