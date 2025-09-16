// Decompiled with JetBrains decompiler
// Type: PX.AddressLookup.AddressValidatorPluginMaintGoogleAddressLookupGraphExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.AddressValidator;
using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.AddressLookup;

public class AddressValidatorPluginMaintGoogleAddressLookupGraphExt : 
  PXGraphExtension<AddressValidatorPluginMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.addressLookup>();

  protected virtual void _(
    Events.FieldVerifying<AddressValidatorPluginDetail, AddressValidatorPluginDetail.value> e)
  {
    AddressValidatorPluginDetail row = e.Row;
    if (row == null || string.IsNullOrEmpty(((Events.FieldVerifyingBase<Events.FieldVerifying<AddressValidatorPluginDetail, AddressValidatorPluginDetail.value>, AddressValidatorPluginDetail, object>) e)?.NewValue?.ToString()))
      return;
    AddressValidatorPlugin addressValidatorPlugin = PXResultset<AddressValidatorPlugin>.op_Implicit(PXSelectBase<AddressValidatorPlugin, PXSelect<AddressValidatorPlugin, Where<AddressValidatorPlugin.addressValidatorPluginID, Equal<Required<AddressValidatorPlugin.addressValidatorPluginID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
    {
      (object) row.AddressValidatorPluginID
    }));
    if (addressValidatorPlugin != null && addressValidatorPlugin.PluginTypeName.Contains(GoogleAddressLookup.AddressValidatorID) && row.ControlTypeValue.GetValueOrDefault() == 5 && ((Events.FieldVerifyingBase<Events.FieldVerifying<AddressValidatorPluginDetail, AddressValidatorPluginDetail.value>, AddressValidatorPluginDetail, object>) e).NewValue.ToString().Split(CountriesISOListHelper.DelimiterChars, StringSplitOptions.RemoveEmptyEntries).Length > 5)
      throw new PXSetPropertyException("The Google API allows filtering by up to 5 countries.");
  }
}
