// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.PreferencesGeneralAddressLookupExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CR.Extensions;

[Serializable]
public sealed class PreferencesGeneralAddressLookupExtension : PXCacheExtension<
#nullable disable
PreferencesGeneral>
{
  /// <value>
  /// <see cref="T:PX.Objects.CS.AddressValidatorPlugin.addressValidatorPluginID" /> of a Address Lookup which will be used.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Lookup Plug-In", FieldClass = "Address Lookup")]
  [PXSelector(typeof (AddressValidatorPlugin.addressValidatorPluginID), DescriptionField = typeof (AddressValidatorPlugin.description))]
  [PXRestrictor(typeof (Where<AddressValidatorPlugin.isActive, Equal<True>, And<AddressValidatorPlugin.pluginTypeName, Contains<AddressLookupNamespaceName>>>), "The address lookup plug-in that you have specified is not active. Click the magnifier icon and select a plug-in from the list.", new System.Type[] {})]
  public string AddressLookupPluginID { get; set; }

  public abstract class addressLookupPluginID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesGeneralAddressLookupExtension.addressLookupPluginID>
  {
  }
}
