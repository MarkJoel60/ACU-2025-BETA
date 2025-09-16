// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.PXAddressLookup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.AddressValidator;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR.Services;
using PX.SM;
using PX.SP;
using PX.Web.UI;
using System;
using System.Collections.Generic;
using System.Web.Compilation;
using System.Web.UI;

#nullable enable
namespace PX.Objects.CS;

public static class PXAddressLookup
{
  public static bool IsEnabled(
  #nullable disable
  PXGraph graph)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.addressLookup>() && PXAddressLookup.IsAddressLookupActive(graph);
  }

  public static AddressValidatorPlugin GetAddressLookupPlugin(PXGraph graph)
  {
    string addressValidatorPluginID = "";
    if (PXSiteMap.IsPortal)
    {
      PXAddressLookup.PortalSetup portalSetup = PXResultset<PXAddressLookup.PortalSetup>.op_Implicit(PXSelectBase<PXAddressLookup.PortalSetup, PXSelectReadonly<PXAddressLookup.PortalSetup>.Config>.SelectSingleBound(graph, (object[]) null, (object[]) null));
      if (!string.IsNullOrEmpty(portalSetup?.AddressLookupPluginID))
        addressValidatorPluginID = portalSetup.AddressLookupPluginID;
    }
    else if (PortalHelper.IsPortalContext((PortalContexts) 2))
    {
      addressValidatorPluginID = PortalHelper.GetPortal()?.AddressLookupPluginID ?? string.Empty;
    }
    else
    {
      PreferencesGeneral preferencesGeneral = PXResultset<PreferencesGeneral>.op_Implicit(PXSelectBase<PreferencesGeneral, PXSelectReadonly<PreferencesGeneral>.Config>.SelectSingleBound(graph, (object[]) null, (object[]) null));
      if (!string.IsNullOrEmpty(preferencesGeneral?.AddressLookupPluginID))
        addressValidatorPluginID = preferencesGeneral.AddressLookupPluginID;
    }
    return string.IsNullOrEmpty(addressValidatorPluginID) ? (AddressValidatorPlugin) null : AddressValidatorPlugin.PK.Find(graph, addressValidatorPluginID);
  }

  public static bool IsAddressLookupActive(PXGraph graph)
  {
    return ((bool?) PXAddressLookup.GetAddressLookupPlugin(graph)?.IsActive).GetValueOrDefault();
  }

  public static IAddressLookupService CreateAddressLookup(PXGraph graph)
  {
    AddressValidatorPlugin addressLookupPlugin = PXAddressLookup.GetAddressLookupPlugin(graph);
    if (addressLookupPlugin == null)
      throw new PXException("The address lookup plug-in has not been found. Contact your system administrator.");
    IAddressLookupService addressLookup = (IAddressLookupService) null;
    if (!string.IsNullOrEmpty(addressLookupPlugin.PluginTypeName))
    {
      try
      {
        addressLookup = (IAddressLookupService) Activator.CreateInstance(PXBuildManager.GetType(addressLookupPlugin.PluginTypeName, true));
        PXResultset<AddressValidatorPluginDetail> pxResultset = ((PXSelectBase<AddressValidatorPluginDetail>) new PXSelect<AddressValidatorPluginDetail, Where<AddressValidatorPluginDetail.addressValidatorPluginID, Equal<Required<AddressValidatorPluginDetail.addressValidatorPluginID>>>>(graph)).Select(new object[1]
        {
          (object) addressLookupPlugin.AddressValidatorPluginID
        });
        List<IAddressValidatorSetting> validatorSettingList = new List<IAddressValidatorSetting>(pxResultset.Count);
        foreach (PXResult<AddressValidatorPluginDetail> pxResult in pxResultset)
        {
          AddressValidatorPluginDetail validatorPluginDetail = PXResult<AddressValidatorPluginDetail>.op_Implicit(pxResult);
          validatorSettingList.Add((IAddressValidatorSetting) validatorPluginDetail);
        }
        ((IAddressConnectedService) addressLookup).Initialize((IEnumerable<IAddressValidatorSetting>) validatorSettingList);
      }
      catch (Exception ex)
      {
        throw new PXException("The system cannot create an address lookup plug-in with the specified settings. Contact your system administrator.", new object[1]
        {
          (object) ex.Message
        });
      }
    }
    return addressLookup;
  }

  public static void RegisterClientScript(PXPage page, PXGraph graph)
  {
    if (!PXAddressLookup.IsAddressLookupActive(graph))
      return;
    IAddressLookupService addressLookup = PXAddressLookup.CreateAddressLookup(graph);
    if (addressLookup == null)
      return;
    string clientScript = addressLookup.GetClientScript(graph);
    if (string.IsNullOrEmpty(clientScript))
      return;
    ((Page) page).ClientScript.RegisterStartupScript(page.GetType(), "addressLookup", clientScript);
  }

  [PXHidden]
  public class PortalSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(32 /*0x20*/, IsKey = true)]
    public virtual string PortalSetupID { get; set; }

    [PXDBString(15, IsUnicode = true)]
    public string AddressLookupPluginID { get; set; }

    public abstract class portalSetupID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXAddressLookup.PortalSetup.portalSetupID>
    {
    }

    public abstract class addressLookupPluginID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXAddressLookup.PortalSetup.addressLookupPluginID>
    {
    }
  }

  [PXHidden]
  public class SPPortal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(15, IsUnicode = true)]
    public string AddressLookupPluginID { get; set; }

    public abstract class addressLookupPluginID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXAddressLookup.SPPortal.addressLookupPluginID>
    {
    }
  }
}
