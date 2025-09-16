// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CountryAddressLookupExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CR.Extensions;

[Serializable]
public sealed class CountryAddressLookupExtension : PXCacheExtension<PX.Objects.CS.Country>
{
  [PXRestrictor(typeof (Where<AddressValidatorPlugin.isActive, Equal<True>, And<AddressValidatorPlugin.pluginTypeName, Contains<AddressValidatorNamespaceName>>>), "The address validation plug-in you have typed is not active. Click the magnifier icon to view the list of active plug-ins.", new System.Type[] {})]
  [PXMergeAttributes]
  public string AddressValidatorPluginID { get; set; }
}
