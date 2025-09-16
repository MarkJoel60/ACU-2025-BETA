// Decompiled with JetBrains decompiler
// Type: PX.AddressLookup.AzureAddressLookup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.AddressValidator;
using PX.CCProcessingBase.Attributes;
using PX.Data;
using PX.Objects.CR.Services;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Net;

#nullable disable
namespace PX.AddressLookup;

[PXDisplayTypeName("Azure Maps", typeof (FeaturesSet.addressLookup))]
public class AzureAddressLookup : IAddressLookupService, IAddressConnectedService
{
  public static readonly string AddressValidatorID = nameof (AzureAddressLookup);

  protected string ApiKey { get; set; }

  protected string Country { get; set; }

  protected string Url { get; set; }

  protected string Path { get; set; }

  public virtual void Initialize(IEnumerable<IAddressValidatorSetting> settings)
  {
    foreach (IAddressValidatorSetting setting in settings)
    {
      if ("API_KEY".Equals(setting.SettingID, StringComparison.InvariantCultureIgnoreCase))
        this.ApiKey = setting.Value;
      if ("COUNTRY".Equals(setting.SettingID, StringComparison.InvariantCultureIgnoreCase))
      {
        if (string.IsNullOrEmpty(setting.Value))
        {
          this.Country = "''";
        }
        else
        {
          char[] chArray = new char[3]{ ' ', '"', '\'' };
          this.Country = $"'{setting.Value.Trim(chArray)}'";
        }
      }
      if ("URL".Equals(setting.SettingID, StringComparison.InvariantCultureIgnoreCase))
        this.Url = setting.Value;
      if ("PATH".Equals(setting.SettingID, StringComparison.InvariantCultureIgnoreCase))
        this.Path = setting.Value;
    }
  }

  public virtual PingResult Ping()
  {
    List<string> stringList = new List<string>();
    bool flag = true;
    string requestUriString = $"https://atlas.microsoft.com/search/fuzzy/xml?&query=Acumatica&subscription-key={this.ApiKey}";
    if (!(WebRequest.Create(requestUriString).GetResponse() is HttpWebResponse response))
    {
      flag = false;
      stringList.Add("");
      stringList.Add("The system cannot execute a web request.");
      stringList.Add(requestUriString);
    }
    else if (response.StatusCode.ToString() != "OK")
    {
      flag = false;
      stringList.Add("");
      stringList.Add($"{response.StatusCode}-{response.StatusDescription}");
    }
    PingResult pingResult = new PingResult();
    ((ResultBase) pingResult).IsSuccess = flag;
    ((ResultBase) pingResult).Messages = stringList.ToArray();
    return pingResult;
  }

  public virtual IAddressValidatorSetting[] DefaultSettings
  {
    get
    {
      return new IAddressValidatorSetting[4]
      {
        (IAddressValidatorSetting) new AddressValidatorSetting(AzureAddressLookup.AddressValidatorID, "API_KEY", 1, "API Key", "", (AddressValidatorSettingControlType) 4),
        (IAddressValidatorSetting) new AddressValidatorSetting(AzureAddressLookup.AddressValidatorID, "COUNTRY", 2, "Country restriction", "US", (AddressValidatorSettingControlType) 6),
        (IAddressValidatorSetting) new AddressValidatorSetting(AzureAddressLookup.AddressValidatorID, "URL", 3, "URL", "https://atlas.microsoft.com/sdk/javascript/service/2/atlas-service.min.js", (AddressValidatorSettingControlType) 1),
        (IAddressValidatorSetting) new AddressValidatorSetting(AzureAddressLookup.AddressValidatorID, "PATH", 4, "PATH", "./map-api/azure-api", (AddressValidatorSettingControlType) 1)
      };
    }
  }

  public string GetClientScript(PXGraph graph)
  {
    string str = "";
    if (!string.IsNullOrEmpty(graph?.Culture?.TwoLetterISOLanguageName))
      str = graph.Culture.TwoLetterISOLanguageName;
    return $"<script type='text/javascript'>  var countryCodeSettings = {this.Country}; var subscriptionKey = '{this.ApiKey}'; var language = '{str}'; </script>\n" + "<script src=\"https://atlas.microsoft.com/sdk/javascript/service/2/atlas-service.min.js\"></script><link href=\"../../Scripts/AddressLookup/jquery-ui.min.css\" rel=\"stylesheet\"><script src=\"../../Scripts/AddressLookup/jquery-ui.min.js\"></script><link href=\"https://atlas.microsoft.com/sdk/javascript/mapcontrol/3/atlas.min.css\" rel=\"stylesheet\" /><script src=\"https://atlas.microsoft.com/sdk/javascript/mapcontrol/3/atlas.min.js\"></script><script type=\"text/javascript\" src=\"../../Scripts/AddressLookup/AzureMapsAPI.js\" async defer></script>";
  }

  public static class AzureAutosuggestReponseStatus
  {
    public const string OK = "OK";
  }
}
