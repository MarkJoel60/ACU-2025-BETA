// Decompiled with JetBrains decompiler
// Type: PX.Translation.PXSpecialRipper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.LocalizationKeyGenerators;
using System;

#nullable disable
namespace PX.Translation;

/// <exclude />
public class PXSpecialRipper : IPXRipper
{
  public void CollectResources(ResourceCollection result)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.CollectSpecialResource<string>(PXSpecialResources.ColorNames, PXSpecialRipper.\u003C\u003EO.\u003C0\u003E__GetColorNameLocalizationKey ?? (PXSpecialRipper.\u003C\u003EO.\u003C0\u003E__GetColorNameLocalizationKey = new Func<string, string>(PXSpecialKeyGenerator.GetColorNameLocalizationKey)), (Func<string, string>) (r => r), LocalizationResourceType.ColorName, result);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.CollectSpecialResource<string>(PXSpecialResources.FontNames, PXSpecialRipper.\u003C\u003EO.\u003C1\u003E__GetFontNameLocalizationKey ?? (PXSpecialRipper.\u003C\u003EO.\u003C1\u003E__GetFontNameLocalizationKey = new Func<string, string>(PXSpecialKeyGenerator.GetFontNameLocalizationKey)), (Func<string, string>) (r => r), LocalizationResourceType.FontName, result);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.CollectSpecialResource<string>(PXSpecialResources.FontFamilyNames, PXSpecialRipper.\u003C\u003EO.\u003C2\u003E__GetFontFamilyLocalizationKey ?? (PXSpecialRipper.\u003C\u003EO.\u003C2\u003E__GetFontFamilyLocalizationKey = new Func<string, string>(PXSpecialKeyGenerator.GetFontFamilyLocalizationKey)), (Func<string, string>) (r => r), LocalizationResourceType.FontFamily, result);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.CollectSpecialResource<(string, string)>(PXSpecialResources.ThemeVariables, PXSpecialRipper.\u003C\u003EO.\u003C3\u003E__GetThemeVariableLocalizationKey ?? (PXSpecialRipper.\u003C\u003EO.\u003C3\u003E__GetThemeVariableLocalizationKey = new Func<(string, string), string>(PXSpecialKeyGenerator.GetThemeVariableLocalizationKey)), (Func<(string, string), string>) (r => r.VariableDisplayName), LocalizationResourceType.ThemeVariable, result);
  }

  private void CollectSpecialResource<T>(
    T[] resourceSource,
    Func<T, string> KeyGenerator,
    Func<T, string> ValueGenerator,
    LocalizationResourceType resourceType,
    ResourceCollection result)
  {
    foreach (T obj in resourceSource)
    {
      string resKey = KeyGenerator(obj);
      string neutralValue = ValueGenerator(obj);
      result.AddResource(new LocalizationResourceLite(resKey, resourceType, neutralValue));
    }
  }
}
