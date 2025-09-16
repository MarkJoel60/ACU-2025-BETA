// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.AddressLookupPreferencesGeneralMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.SM;

#nullable disable
namespace PX.Objects.CR.Extensions;

public class AddressLookupPreferencesGeneralMaintExt : PXGraphExtension<PreferencesGeneralMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.addressLookup>();

  public virtual void _(
    Events.FieldVerifying<PreferencesGeneral.addressLookupPluginID> e)
  {
    if (!(e.Row is PreferencesGeneral) || string.IsNullOrEmpty(((Events.FieldVerifyingBase<Events.FieldVerifying<PreferencesGeneral.addressLookupPluginID>, object, object>) e)?.NewValue?.ToString()))
      return;
    AddressValidatorPlugin addressValidatorPlugin = PXResultset<AddressValidatorPlugin>.op_Implicit(PXSelectBase<AddressValidatorPlugin, PXSelect<AddressValidatorPlugin, Where<AddressValidatorPlugin.addressValidatorPluginID, Equal<Required<AddressValidatorPlugin.addressValidatorPluginID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
    {
      (object) ((Events.FieldVerifyingBase<Events.FieldVerifying<PreferencesGeneral.addressLookupPluginID>, object, object>) e).NewValue.ToString()
    }));
    if (addressValidatorPlugin != null && !addressValidatorPlugin.PluginTypeName.Contains("AddressLookup"))
      throw new PXSetPropertyException("An incorrect plug-in type has been selected for the address lookup plug-in.");
  }
}
