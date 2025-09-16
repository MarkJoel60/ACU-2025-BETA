// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteDirections
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;
using System.Threading;
using System.Xml;

#nullable disable
namespace PX.Objects.FS;

/// <summary>
/// Static class providing methods to retrieve directions between locations.
/// </summary>
public static class RouteDirections
{
  /// <summary>
  /// Gets a route from the Google Maps Directions web service.
  /// </summary>
  /// <param name="optimize">If set to <c>true</c> optimize the route by re-ordering the locations to minimize the
  /// time to complete the route.</param>
  public static Route GetRoute(string optimize, string apiKey, params GLocation[] locations)
  {
    if (locations.Length < 2)
      throw new ArgumentException("At least one origin and one destination have to be included.");
    string reqStr = (string) null;
    for (int index = 0; index < locations.Length; ++index)
      reqStr = $"{$"{reqStr}wp.{index.ToString()}="}{locations[index].ToString()}&";
    if (!string.IsNullOrEmpty(optimize))
      reqStr = $"{reqStr}optimize={optimize}&";
    string str1 = RouteDirections.RemoveSpecialCharacters(reqStr);
    string response = (string) null;
    int num = 0;
    string str2 = (string) null;
    try
    {
      while (num < 3)
      {
        response = HttpWebService.MakeRequest($"https://dev.virtualearth.net/REST/v1/Routes/Driving?distanceUnit=mi&o=xml&{str1}key={apiKey}");
        string status = RouteDirections.GetStatus(response);
        if (!string.IsNullOrEmpty(status))
        {
          if (!string.Equals(status, "Too Many Requests"))
            break;
        }
        ++num;
        str2 = (string) null;
        Thread.Sleep(2000);
      }
    }
    catch
    {
      return (Route) null;
    }
    return RouteDirections.ParseResponse(response);
  }

  private static string RemoveSpecialCharacters(string reqStr) => reqStr.Replace("#", string.Empty);

  private static Route ParseResponse(string response)
  {
    XmlDocument route = new XmlDocument();
    route.XmlResolver = (XmlResolver) null;
    route.LoadXml(response);
    XmlNamespaceManager nameSpace = new XmlNamespaceManager(route.NameTable);
    SharedFunctions.GenerateXmlNameSpace(ref nameSpace);
    string innerText = route.SelectSingleNode($".//{"bingSchema:"}StatusDescription", nameSpace).InnerText;
    if (innerText != "OK")
      throw new RoutingException(RouteDirections.GetStatusMessage(innerText));
    return new Route(route, nameSpace);
  }

  /// <summary>Returns the status code from a google map response.</summary>
  /// <param name="response">The google map response.</param>
  private static string GetStatus(string response)
  {
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.XmlResolver = (XmlResolver) null;
    xmlDocument.LoadXml(response);
    XmlNamespaceManager nameSpace = new XmlNamespaceManager(xmlDocument.NameTable);
    SharedFunctions.GenerateXmlNameSpace(ref nameSpace);
    return xmlDocument.SelectSingleNode($".//{"bingSchema:"}StatusDescription", nameSpace).InnerText;
  }

  private static string GetStatusMessage(string status)
  {
    if (status != null)
    {
      switch (status.Length)
      {
        case 2:
          if (status == "OK")
            return "The request was successful.";
          break;
        case 7:
          if (status == "Created")
            return "A new resource has been created.";
          break;
        case 8:
          if (status == "Accepted")
            return "The request has been accepted for processing.";
          break;
        case 9:
          switch (status[0])
          {
            case 'F':
              if (status == "Forbidden")
                return "The request is for something forbidden. Authorization will not help.";
              break;
            case 'N':
              if (status == "Not Found")
                return "The requested resource was not found.";
              break;
          }
          break;
        case 11:
          if (status == "Bad Request")
            return "The request contains an error.";
          break;
        case 12:
          if (status == "Unauthorized")
            return "Access was denied. You may have entered your credentials incorrectly, or you might not have access to the requested resource or operation.";
          break;
        case 17:
          if (status == "Too Many Requests")
            return "The user has sent too many requests in a given amount of time. The user account is rate-limited.";
          break;
        case 21:
          if (status == "Internal Server Error")
            return "Your request could not be completed because there was a problem with the service.";
          break;
      }
    }
    return "There is a problem with the service right now. Please try again later.";
  }
}
