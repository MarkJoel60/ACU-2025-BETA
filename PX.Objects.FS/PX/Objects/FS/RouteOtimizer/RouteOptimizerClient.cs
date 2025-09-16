// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteOtimizer.RouteOptimizerClient
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using Newtonsoft.Json;
using PX.Data;
using System;
using System.IO;
using System.Net;
using System.Text;

#nullable disable
namespace PX.Objects.FS.RouteOtimizer;

public class RouteOptimizerClient
{
  /// <summary>
  /// Optimizes a set of waypoints so that they are best allocated to a set of vehicles taking into
  /// account the following complex constraints:
  /// 1 - vehicle limited capacity and working time-window;
  /// 2 - waypoint service time, delivery time-window and priority;
  /// </summary>
  /// <param name="requestBody">Set of waypoints and vechicle config</param>
  /// <param name="ApiKey">The API key to use for the client connection</param>
  /// <returns></returns>
  public SingleDayOptimizationOutput getSingleDayOptimization(
    string url,
    string apiKey,
    SingleDayOptimizationInput requestBody)
  {
    SingleDayOptimizationOutput singleDayOptimization = new SingleDayOptimizationOutput();
    if (url != null)
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create($"{url.Trim()}?key={apiKey}");
      string str1 = JsonConvert.SerializeObject((object) requestBody);
      httpWebRequest.Method = "POST";
      httpWebRequest.ContentType = "application/json";
      httpWebRequest.ContentLength = (long) str1.Length;
      using (Stream requestStream = httpWebRequest.GetRequestStream())
      {
        using (StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.ASCII))
          streamWriter.Write(str1);
      }
      try
      {
        using (Stream stream = httpWebRequest.GetResponse().GetResponseStream() ?? Stream.Null)
        {
          using (StreamReader streamReader = new StreamReader(stream))
            singleDayOptimization = JsonConvert.DeserializeObject<SingleDayOptimizationOutput>(streamReader.ReadToEnd());
        }
      }
      catch (WebException ex)
      {
        using (Stream stream = ex.Response.GetResponseStream() ?? Stream.Null)
        {
          using (StreamReader streamReader = new StreamReader(stream))
          {
            Error error = JsonConvert.DeserializeObject<Error>(streamReader.ReadToEnd());
            string errorCode = error.errorCode;
            string str2;
            if (errorCode != null)
            {
              switch (errorCode.Length)
              {
                case 4:
                  switch (errorCode[1])
                  {
                    case '1':
                      if (errorCode == "-100")
                      {
                        str2 = "WorkWave Internal Server Error. Please try again later and also contact WorkWave support team so that they may investigate.";
                        goto label_40;
                      }
                      break;
                    case '2':
                      if (errorCode == "-200")
                      {
                        str2 = "Wrong authentication key for WorkWave API.";
                        goto label_40;
                      }
                      break;
                    case '4':
                      if (errorCode == "-400")
                      {
                        str2 = "One or more constraints associated with the WorkWave authentication key, such as maximum number of Drivers or Waypoints, have been violated.";
                        goto label_40;
                      }
                      break;
                    case '5':
                      if (errorCode == "-500")
                      {
                        str2 = "The maximum daily amount of requests has been reached. Further requests must wait until the next day.";
                        goto label_40;
                      }
                      break;
                    case '6':
                      if (errorCode == "-600")
                      {
                        str2 = "The WorkWave authentication key is expired.";
                        goto label_40;
                      }
                      break;
                    case '9':
                      switch (errorCode)
                      {
                        case "-900":
                          str2 = "The given WorkWave key has submitted too many requests in a short period of time. Insert a pause between requests to avoid getting this error.";
                          goto label_40;
                        case "-901":
                          str2 = "The given WorkWave key has submitted too many concurrent requests. Wait for previously submitted requests to complete to avoid getting this error.";
                          goto label_40;
                      }
                      break;
                  }
                  break;
                case 5:
                  if (errorCode == "-1000")
                  {
                    str2 = "Malformed Request made to WorkWave. Something is wrong with the input. Either a query-string parameter is missing or malformed or the JSON data in the request body is malformed (typically due to a missing or overabundant parenthesis or comma or a missing mandatory field).";
                    goto label_40;
                  }
                  break;
              }
            }
            str2 = error.errorDescription;
label_40:
            throw new PXException(PXMessages.LocalizeFormatNoPrefix(str2, Array.Empty<object>()));
          }
        }
      }
    }
    return singleDayOptimization;
  }
}
