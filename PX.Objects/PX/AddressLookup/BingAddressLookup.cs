// Decompiled with JetBrains decompiler
// Type: PX.AddressLookup.BingAddressLookup
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
using System.Xml;

#nullable disable
namespace PX.AddressLookup;

[PXDisplayTypeName("Bing Maps", typeof (FeaturesSet.addressLookup))]
public class BingAddressLookup : IAddressLookupService, IAddressConnectedService
{
  public static readonly string AddressValidatorID = nameof (BingAddressLookup);

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
    string filename = $"https://dev.virtualearth.net/REST/v1/Locations/?q=ttt&o=xml&key={this.ApiKey}";
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load(filename);
    string innerText = xmlDocument.DocumentElement?.GetElementsByTagName("StatusCode")?.Item(0)?.InnerText;
    if (string.IsNullOrEmpty(innerText))
    {
      flag = false;
      stringList.Add("");
      stringList.Add("The system cannot execute a web request.");
      stringList.Add(filename);
    }
    else if (!innerText.Equals("200"))
    {
      flag = false;
      stringList.Add("");
      stringList.Add($"{innerText}-{xmlDocument.DocumentElement?.GetElementsByTagName("StatusDescription")?.Item(0)?.InnerText}");
      stringList.Add(xmlDocument.DocumentElement?.GetElementsByTagName("ErrorDetails")?.Item(0)?.InnerText);
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
        (IAddressValidatorSetting) new AddressValidatorSetting(BingAddressLookup.AddressValidatorID, "API_KEY", 1, "API Key", "", (AddressValidatorSettingControlType) 4),
        (IAddressValidatorSetting) new AddressValidatorSetting(BingAddressLookup.AddressValidatorID, "COUNTRY", 2, "Country restriction", "US", (AddressValidatorSettingControlType) 6),
        (IAddressValidatorSetting) new AddressValidatorSetting(BingAddressLookup.AddressValidatorID, "URL", 3, "URL", "https://www.bing.com/api/maps/mapcontrol?key=${API_KEY}&setLang=en&includeEntityTypes=Address,Business", (AddressValidatorSettingControlType) 1),
        (IAddressValidatorSetting) new AddressValidatorSetting(BingAddressLookup.AddressValidatorID, "PATH", 4, "PATH", "./map-api/bing-api", (AddressValidatorSettingControlType) 1)
      };
    }
  }

  public string GetClientScript(PXGraph graph)
  {
    string str1 = "";
    if (!string.IsNullOrEmpty(graph?.Culture?.TwoLetterISOLanguageName))
      str1 = graph.Culture.TwoLetterISOLanguageName;
    string str2 = $"<script type='text/javascript'>  var countryCodeSettings = {this.Country};  </script>\n";
    string str3 = "Address,Business";
    string str4 = $"<script type='text/javascript' src='https://www.bing.com/api/maps/mapcontrol?key={this.ApiKey}&setLang={str1}&includeEntityTypes={str3}' async defer></script>";
    return $"{str2}{str4}<script type='text/javascript' src='..\\..\\Scripts\\AddressLookup\\BingMapsAPI.js' async defer></script>";
  }

  public static class BingAutosuggestReponseStatus
  {
    public const string OK = "200";
  }
}
