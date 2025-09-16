// Decompiled with JetBrains decompiler
// Type: PX.Data.Localization.ILocalizationFeaturesService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Localization;

/// <summary>
/// The service provides the information about localization features
/// that aggregate data and business logic specific to a region
/// and makes Acumatica ERP compliant to local market requirements.
/// Localization features can include custom generic inquiries, forms, reports, dashboards,
/// modifications to existing forms, business logic, etc.
/// </summary>
public interface ILocalizationFeaturesService
{
  /// <summary>Returns localizations available on the website.</summary>
  /// <returns>A list of localizations represented by unique IDs that consist of capital letters.</returns>
  IEnumerable<string> GetEnabledLocalizations();

  /// <summary>
  ///  Returns an ID of the localization that has been enabled by using the corresponding check box on the Enable/Disable Features (CS100000) form in the current tenant.
  /// </summary>
  /// <returns>The unique ID that consists of capital letters used by ILocalizationFeaturesService
  /// and specified as a country code following by ISO 3166-1 alpha-2.
  /// Returns null if no localization features are enabled for a tenant.</returns>
  string GetEnabledLocalization();

  /// <summary>
  /// Returns the localization that corresponds to the requested feature.
  /// </summary>
  /// <param name="feature">The name of the feature according to <see cref="P:PX.Data.PXAccessProvider.AllFeatures">PXAccessProvider</see></param>
  /// <returns>The unique ID of the localization that consists of capital letters</returns>
  string GetLocalizationCodeForFeature(string feature);
}
