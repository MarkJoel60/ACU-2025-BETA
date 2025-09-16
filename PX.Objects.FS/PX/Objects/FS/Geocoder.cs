// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Geocoder
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

#nullable disable
namespace PX.Objects.FS;

/// <summary>Wrapper round the Google Maps geocoding service.</summary>
public static class Geocoder
{
  /// <summary>Reverses geocode the specified location.</summary>
  /// <param name="location">The location.</param>
  /// <returns>Returns the address of the location.</returns>
  public static string ReverseGeocode(LatLng location, string apiKey)
  {
    string xml;
    try
    {
      xml = HttpWebService.MakeRequest($"{"https://dev.virtualearth.net/REST/v1/"}Locations/{location.Latitude},{location.Longitude}?o=xml&key={apiKey}");
    }
    catch
    {
      throw new Exception("The system failed to connect to Bing API. Check your connection.");
    }
    XmlDocument xmlDocument = new XmlDocument()
    {
      XmlResolver = (XmlResolver) null
    };
    xmlDocument.LoadXml(xml);
    XmlNamespaceManager nameSpace = new XmlNamespaceManager(xmlDocument.NameTable);
    SharedFunctions.GenerateXmlNameSpace(ref nameSpace);
    string str1 = string.Empty;
    string[] strArray = new string[2]
    {
      "Address",
      "PopulatedPlace"
    };
    foreach (string str2 in strArray)
    {
      XmlNode xmlNode = xmlDocument.SelectSingleNode(string.Format("//{0}Location[{0}EntityType = '{1}']//{0}FormattedAddress", (object) "bingSchema:", (object) str2), nameSpace);
      if (xmlNode != null)
      {
        str1 = xmlNode.InnerText;
        break;
      }
    }
    return str1;
  }

  /// <summary>Geocodes the specified address.</summary>
  /// <param name="address">The address.</param>
  /// <returns>An array of possible locations.</returns>
  public static GLocation[] Geocode(string rAddress, string apiKey)
  {
    XmlDocument xmlDocument = new XmlDocument()
    {
      XmlResolver = (XmlResolver) null
    };
    try
    {
      string xml = HttpWebService.MakeRequest($"{"https://dev.virtualearth.net/REST/v1/"}Locations/{Regex.Replace(rAddress, "[^a-zA-Z0-9_. ]+", "", RegexOptions.Compiled)}?o=xml&key={apiKey}");
      xmlDocument.LoadXml(xml);
    }
    catch
    {
      throw new Exception("The system failed to connect to Bing API. Check your connection.");
    }
    XmlNamespaceManager nameSpace = new XmlNamespaceManager(xmlDocument.NameTable);
    SharedFunctions.GenerateXmlNameSpace(ref nameSpace);
    XmlNodeList xmlNodeList = xmlDocument.SelectNodes($"//{"bingSchema:"}Location", nameSpace);
    List<GLocation> glocationList = new List<GLocation>();
    foreach (XmlElement xmlElement in xmlNodeList)
    {
      string innerText = xmlElement.SelectSingleNode($".//{"bingSchema:"}FormattedAddress", nameSpace).InnerText;
      GLocation glocation = new GLocation(new LatLng((XmlElement) xmlElement.SelectSingleNode($".//{"bingSchema:"}Point", nameSpace), nameSpace), innerText);
      glocationList.Add(glocation);
    }
    return glocationList.ToArray();
  }

  public static string GetStatus(string rAddress, string apiKey)
  {
    string xml;
    try
    {
      xml = HttpWebService.MakeRequest($"{"https://dev.virtualearth.net/REST/v1/"}Locations/{Regex.Replace(rAddress, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled)}?o=xml&key={apiKey}");
    }
    catch
    {
      throw new Exception("The system failed to connect to Bing API. Check your connection.");
    }
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.XmlResolver = (XmlResolver) null;
    xmlDocument.LoadXml(xml);
    XmlNamespaceManager nameSpace = new XmlNamespaceManager(xmlDocument.NameTable);
    SharedFunctions.GenerateXmlNameSpace(ref nameSpace);
    return xmlDocument.SelectSingleNode($"//{"bingSchema:"}StatusDescription", nameSpace).InnerText;
  }
}
