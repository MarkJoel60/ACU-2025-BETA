// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXLicensingServer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update.LicensingService;
using Serilog;
using Serilog.Events;
using System;
using System.Text;

#nullable disable
namespace PX.Data.Update;

internal static class PXLicensingServer
{
  private static ILogger Logger = Serilog.Core.Logger.None;

  internal static void InitializeLogging(ILogger logger)
  {
    PXLicensingServer.Logger = LicensingLog.ForClassContext(logger, typeof (PXLicensingServer));
  }

  private static string GetServerUrl()
  {
    if (string.IsNullOrEmpty(InstallationPolicy.LicensingServer))
    {
      PXException pxException = new PXException("The Acumatica Licensing Server is not set up.");
      PXLicensingServer.Logger.Error((Exception) pxException, "Licensing server url not defined");
      throw pxException;
    }
    return InstallationPolicy.LicensingServer;
  }

  private static LicensingServiceGate GetGate()
  {
    return PXLicensingServer.GetGate(PXLicensingServer.GetServerUrl());
  }

  private static LicensingServiceGate GetGate(string server)
  {
    server = PXLicensingServer.ValidateUrl(server);
    LicensingServiceGate gate = (LicensingServiceGate) null;
    if (gate == null || gate.Url != server)
    {
      gate = new LicensingServiceGate(server);
      gate.Timeout = (int) new TimeSpan(0, 30, 0).TotalMilliseconds;
    }
    PXLicensingServer.Logger.Verbose<string>("Using licensing server url {LicensingServerUrl}", gate.Url);
    return gate;
  }

  private static string ValidateUrl(string server)
  {
    server = server.ToLower();
    if (!server.StartsWith("http://") && !server.StartsWith("https://"))
      server = "https://" + server;
    if (!server.EndsWith("api/licensing.asmx"))
    {
      if (!server.EndsWith("/"))
        server += "/";
      server += "api/licensing.asmx";
    }
    return server;
  }

  public static void CheckConnection(string server)
  {
    using (LicensingServiceGate gate = PXLicensingServer.GetGate(server))
    {
      try
      {
        gate.GetLicense((InstanceInfo) null);
      }
      catch (Exception ex)
      {
        PXLicensingServer.Logger.Warning<string>(ex, "Error while checking connection to license server {LicensingServerUrl}", gate.Url);
        throw PXException.ExtractInner(ex);
      }
    }
  }

  public static LicenseBucket GetLicense(
    string key,
    string id,
    string configuration,
    InstanceStatistics instanceStatistics)
  {
    string base64String = Convert.ToBase64String(Encoding.Unicode.GetBytes(configuration));
    InstanceInfo info = new InstanceInfo()
    {
      LicenseKey = key,
      InstallationID = id,
      Configuration = base64String,
      Statistic = instanceStatistics,
      CustProjects = PXInstanceHelper.GetCustProjects()
    };
    using (LicensingServiceGate gate = PXLicensingServer.GetGate())
    {
      ILogger logger = PXLicensingServer.Logger.ForContext("InstallationID", (object) id, false).ForContext("LicenseKey", (object) key, false).ForContext("LicensingServerUrl", (object) gate.Url, false);
      if (logger.IsEnabled((LogEventLevel) 1))
        logger = logger.ForContext("LicenseConfiguration", (object) configuration, false);
      LicenseInfo license;
      try
      {
        license = gate.GetLicense(info);
      }
      catch (Exception ex)
      {
        logger.Error(ex, "Error while getting license from licensing server");
        throw new PXException(ex, "The license server returned an error. For detailed information, see the trace.", Array.Empty<object>());
      }
      if (license != null)
      {
        logger.ForContext("LicenseServerResponse", (object) license, true).Verbose("Got response from licensing server");
        if (!license.Valid)
        {
          logger.Error<string, string>("Got improper ({ImproperLicenseType}) license from licensing server: {LicensingServerError}", "Invalid", license.Error);
          throw new PXException(license.Error);
        }
        if (!string.IsNullOrEmpty(license.Error))
        {
          if (license.Error == "InstanceCountWarningException")
          {
            logger.Warning<string, string>("Got improper ({ImproperLicenseType}) license from licensing server: {LicensingServerError}", "Instance count warning", license.Error);
            throw new InstanceCountWarningException();
          }
          logger.Error<string, string>("Got improper ({ImproperLicenseType}) license from licensing server: {LicensingServerError}", "With errors", license.Error);
          throw new PXException(license.Error);
        }
        LicenseBucket licenseBucket = new LicenseBucket()
        {
          LicenseKey = key,
          Restriction = PXLicensingServer.TransformValue(license.Restriction),
          Signature = license.Signature,
          Date = new System.DateTime?(System.DateTime.UtcNow)
        };
        logger.ForContext(licenseBucket).Information("Created license bucket from licensing server response");
        return licenseBucket;
      }
      logger.Error("Null response from licensing server");
      throw new PXException("The license cannot be obtained. An unexpected response has been returned from the license server.");
    }
  }

  private static string TransformValue(string str)
  {
    if (str.EndsWith("=") || str.Replace(" ", "").Length % 4 == 0)
    {
      try
      {
        return Encoding.Unicode.GetString(Convert.FromBase64String(str));
      }
      catch (Exception ex)
      {
        PXLicensingServer.Logger.ForContext("InputString", (object) str, false).Verbose(ex, "Error while converting string from BASE64");
      }
    }
    else
      PXLicensingServer.Logger.ForContext("InputString", (object) str, false).Verbose("Input string is not BASE64-encoded");
    return str.Contains("\n") && !str.Contains("\r") ? str.Replace("\n", "\r\n") : str;
  }
}
