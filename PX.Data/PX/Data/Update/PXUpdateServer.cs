// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXUpdateServer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update.Storage;
using PX.Data.Update.UpdateService;
using PX.SM;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace PX.Data.Update;

internal static class PXUpdateServer
{
  private static string GetServerUrl()
  {
    return !string.IsNullOrEmpty(InstallationPolicy.UpdateServer) ? InstallationPolicy.UpdateServer : throw new PXException("Acumatica Update Server is not set up.");
  }

  private static UpdateServiceGate GetGate()
  {
    return PXUpdateServer.GetGate(PXUpdateServer.GetServerUrl());
  }

  private static UpdateServiceGate GetGate(string server)
  {
    server = PXUpdateServer.ValidateUrl(server);
    UpdateServiceGate gate = (UpdateServiceGate) null;
    if (gate == null || gate.Url != server)
    {
      gate = new UpdateServiceGate(server);
      gate.Timeout = (int) new TimeSpan(0, 30, 0).TotalMilliseconds;
    }
    return gate;
  }

  private static string ValidateUrl(string server)
  {
    server = server.ToLower();
    if (!server.StartsWithCollation("http://") && !server.StartsWith("https://"))
      server = "https://" + server;
    if (!server.EndsWithCollation("update.asmx"))
    {
      if (!server.EndsWithCollation("/"))
        server += "/";
      server += "update.asmx";
    }
    return server;
  }

  private static VersionInfo ToVersionInfo(this System.Version version)
  {
    return new VersionInfo()
    {
      Type = PXInstanceHelper.CurrentInstanceType.ToApplicationType(),
      Major = version.Major,
      Minor = version.Minor,
      Build = version.Build
    };
  }

  private static ApplicationType ToApplicationType(this PXInstanceType type)
  {
    return (ApplicationType) Enum.Parse(typeof (ApplicationType), type.ToString());
  }

  public static void CheckConnection(string server)
  {
    using (UpdateServiceGate gate = PXUpdateServer.GetGate(server))
    {
      try
      {
        gate.GetBranchesFiltered(new VersionInfo()
        {
          Major = 0,
          Minor = 0,
          Build = 0,
          Type = PXInstanceHelper.CurrentInstanceType.ToApplicationType()
        }, (string) null);
      }
      catch (Exception ex)
      {
        throw PXException.ExtractInner(ex);
      }
    }
  }

  public static bool CheckUpdates(string currentVersion)
  {
    if (!InstallationPolicy.UpdateEnabled || !InstallationPolicy.UpdateNotification)
      return false;
    using (UpdateServiceGate gate = PXUpdateServer.GetGate())
    {
      try
      {
        return gate.CheckUpdates(PXVersionHelper.Convert(currentVersion).ToVersionInfo());
      }
      catch (Exception ex)
      {
        PXTrace.WriteError(ex);
        return false;
      }
    }
  }

  public static IEnumerable<AvailableVersion> GetVersions(string currentVersion, string key)
  {
    List<AvailableVersion> versions = new List<AvailableVersion>();
    if (!InstallationPolicy.UpdateEnabled)
      return (IEnumerable<AvailableVersion>) versions;
    using (UpdateServiceGate gate = PXUpdateServer.GetGate())
    {
      foreach (BuildInfo build in gate.GetBuilds(PXVersionHelper.Convert(currentVersion).ToVersionInfo(), key))
        versions.Add(new AvailableVersion()
        {
          Version = build.Name,
          Date = build.Date,
          Restricted = new bool?(build.Restricted),
          Description = build.Description,
          Notes = build.Notes
        });
    }
    return (IEnumerable<AvailableVersion>) versions;
  }

  public static void DownloadVersion(string id, string version, string key)
  {
    if (!InstallationPolicy.UpdateEnabled)
      return;
    VersionInfo versionInfo = PXVersionHelper.Convert(version).ToVersionInfo();
    using (Stream stream = PXStorageHelper.GetAppDataProvider().OpenWrite(id))
    {
      using (UpdateServiceGate gate = PXUpdateServer.GetGate())
      {
        int position = 0;
        while (true)
        {
          byte[] buffer = PXUpdateServer.DownloadPart(gate, versionInfo, key, position);
          if (buffer != null)
          {
            if (buffer.Length != 0)
            {
              stream.Write(buffer, 0, buffer.Length);
              ++position;
            }
            else
              break;
          }
          else
            break;
        }
      }
      stream.Flush();
    }
    GC.Collect();
  }

  private static byte[] DownloadPart(
    UpdateServiceGate gate,
    VersionInfo version,
    string key,
    int position)
  {
    short num1 = 3;
    int num2 = 0;
    while (true)
    {
      try
      {
        return gate.DownloadBuildPartially(version, key, position);
      }
      catch
      {
        if (num2 == (int) num1 - 1)
          throw;
      }
      ++num2;
    }
  }
}
