// Decompiled with JetBrains decompiler
// Type: PX.AddressLookup.GoogleAddressLookup
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
using System.Linq;
using System.Xml;

#nullable disable
namespace PX.AddressLookup;

[PXDisplayTypeName("Google Maps", typeof (FeaturesSet.addressLookup))]
public class GoogleAddressLookup : IAddressLookupService, IAddressConnectedService
{
  public static readonly string AddressValidatorID = nameof (GoogleAddressLookup);

  protected string ApiKey { get; set; }

  protected string Countries { get; set; }

  protected string Url { get; set; }

  protected string Path { get; set; }

  public virtual void Initialize(IEnumerable<IAddressValidatorSetting> settings)
  {
    foreach (IAddressValidatorSetting setting in settings)
    {
      if ("API_KEY".Equals(setting.SettingID, StringComparison.InvariantCultureIgnoreCase))
        this.ApiKey = setting.Value;
      if ("COUNTRIES".Equals(setting.SettingID, StringComparison.InvariantCultureIgnoreCase))
        this.Countries = !string.IsNullOrEmpty(setting.Value) ? string.Join(",", ((IEnumerable<string>) setting.Value.Split(CountriesISOListHelper.DelimiterChars, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>) (iso => $"'{iso}'"))) : "";
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
    string filename = $"https://maps.googleapis.com/maps/api/place/autocomplete/xml?input=Acuma&types=address&key={this.ApiKey}";
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load(filename);
    string innerText = xmlDocument.SelectSingleNode("AutocompletionResponse/status")?.InnerText;
    if (string.IsNullOrEmpty(innerText))
    {
      flag = false;
      stringList.Add("The system cannot execute a web request.");
      stringList.Add(filename);
    }
    else if (!innerText.Equals("OK"))
    {
      flag = false;
      stringList.Add("");
      stringList.Add(innerText);
      stringList.Add(xmlDocument.SelectSingleNode("AutocompletionResponse/error_message")?.InnerText);
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
        (IAddressValidatorSetting) new AddressValidatorSetting(GoogleAddressLookup.AddressValidatorID, "API_KEY", 1, "API Key", "", (AddressValidatorSettingControlType) 4),
        (IAddressValidatorSetting) new AddressValidatorSetting(GoogleAddressLookup.AddressValidatorID, "COUNTRIES", 2, "Country restriction", "US, CA, GB, MX", (AddressValidatorSettingControlType) 5),
        (IAddressValidatorSetting) new AddressValidatorSetting(GoogleAddressLookup.AddressValidatorID, "URL", 3, "URL", "https://maps.googleapis.com/maps/api/js?key=${API_KEY}&libraries=places&language=EN", (AddressValidatorSettingControlType) 1),
        (IAddressValidatorSetting) new AddressValidatorSetting(GoogleAddressLookup.AddressValidatorID, "PATH", 4, "PATH", "./map-api/google-api", (AddressValidatorSettingControlType) 1)
      };
    }
  }

  public string GetClientScript(PXGraph graph)
  {
    string str1 = "";
    if (!string.IsNullOrEmpty(graph?.Culture?.TwoLetterISOLanguageName))
      str1 = graph.Culture.TwoLetterISOLanguageName;
    string str2 = "";
    if (!string.IsNullOrEmpty(this.Countries))
      str2 += $"<script type='text/javascript'>  var componentRestrictions_country = [{this.Countries}];  </script>\n";
    return $"{str2}{$"<script type='text/javascript' src='https://maps.googleapis.com/maps/api/js?key={this.ApiKey}&libraries=places&language={str1}' async defer></script>"}<script type='text/javascript' src='..\\..\\Scripts\\AddressLookup\\GooglePlacesAPI.js' async defer></script>";
  }

  /// <summary>
  /// https://developers.google.com/places/web-service/autocomplete#place_autocomplete_status_codes
  /// OK indicates that no errors occurred and at least one result was returned.
  /// ZERO_RESULTS indicates that the search was successful but returned no results.This may occur if the search was passed a bounds in a remote location.
  /// OVER_QUERY_LIMIT indicates that you are over your quota.
  /// REQUEST_DENIED indicates that your request was denied, generally because of lack of a valid key parameter.
  /// INVALID_REQUEST generally indicates that the input parameter is missing.
  /// UNKNOWN_ERROR indicates a server-side error; trying again may be successful
  /// </summary>
  public static class GoogleReponseStatus
  {
    public const string OK = "OK";
    public const string REQUEST_DENIED = "REQUEST_DENIED";
  }
}
