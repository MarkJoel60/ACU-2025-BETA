// Decompiled with JetBrains decompiler
// Type: PX.Data.PXVersionInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using Newtonsoft.Json;
using PX.BulkInsert.Tools;
using PX.Common;
using PX.Common.Service;
using PX.DbServices.Model;
using PX.Licensing;
using System;
using System.IO;
using System.Reflection;

#nullable enable
namespace PX.Data;

[Obsolete("Will be replaced by the new PX.Version project in 2023R2")]
public static class PXVersionInfo
{
  private static 
  #nullable disable
  string _oemVersion;
  private static bool _displayBothVersions = false;
  private static string _version;
  private static string _acumaticaBuildVersion;
  private static string _dbversion;
  private static readonly 
  #nullable enable
  Lazy<PXVersionInfo.PatchMetadata?> _patchMetadata = new Lazy<PXVersionInfo.PatchMetadata>(new Func<PXVersionInfo.PatchMetadata>(PXVersionInfo.GetPatchMetadata));

  [PXInternalUseOnly]
  public static 
  #nullable disable
  string OemVersion
  {
    get
    {
      if (!string.IsNullOrEmpty(PXVersionInfo._oemVersion))
        return PXVersionInfo._oemVersion;
      Assembly fromCurrentDomain = VersionHelper.GetAssemblyWithPXOEMVersionAttributeFromCurrentDomain();
      if (fromCurrentDomain == (Assembly) null)
        return "";
      PXVersionInfo._displayBothVersions = ((int) VersionHelper.GetDisplayBothVersionsPropertyFromAssembly(fromCurrentDomain) ?? (PXVersionInfo._displayBothVersions ? 1 : 0)) != 0;
      PXVersionInfo._oemVersion = VersionHelper.GetOEMVersionFromAssembly(fromCurrentDomain);
      return PXVersionInfo._oemVersion;
    }
  }

  [PXInternalUseOnly]
  public static string Version
  {
    get
    {
      if (!string.IsNullOrEmpty(PXVersionInfo._version))
        return PXVersionInfo._version;
      PXVersionInfo._version = PXVersionInfo.AcumaticaBuildVersion;
      if (!string.IsNullOrEmpty(PXVersionInfo.OemVersion))
        PXVersionInfo._version = PXVersionInfo.OemVersion;
      if (PXVersionInfo.DatabaseVersion != null && PXVersionInfo.DatabaseVersion != "1.0.0.0" && PXVersionInfo.DatabaseVersion != PXVersionInfo._version)
        PXVersionInfo._version = $"{PXVersionInfo._version} (DB: {PXVersionInfo.DatabaseVersion})";
      if (PXVersionInfo._displayBothVersions)
        PXVersionInfo._version = $"{PXVersionInfo._version} [{PXVersionInfo.AcumaticaBuildVersion}]";
      return PXVersionInfo._version;
    }
  }

  /// <summary>public because it is required for PageTitle.ascx.cs.</summary>
  [PXInternalUseOnly]
  public static string PatchVersion
  {
    get
    {
      return string.IsNullOrEmpty(PXVersionInfo._patchMetadata.Value?.PatchNumber) ? string.Empty : PXVersionInfo.Version + PXVersionInfo._patchMetadata.Value.PatchNumber;
    }
  }

  /// <summary>public because it is required for PageTitle.ascx.cs.</summary>
  [PXInternalUseOnly]
  public static string PatchNotes => PXVersionInfo._patchMetadata.Value?.PatchNotes ?? string.Empty;

  private static 
  #nullable enable
  PXVersionInfo.PatchMetadata? GetPatchMetadata()
  {
    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bin", "patch.json");
    if (!File.Exists(path))
      return (PXVersionInfo.PatchMetadata) null;
    try
    {
      var data1 = new{ PatchNumber = "", PatchNotes = "" };
      var data2 = JsonConvert.DeserializeAnonymousType(File.ReadAllText(path), data1);
      return new PXVersionInfo.PatchMetadata()
      {
        PatchNotes = data2.PatchNotes,
        PatchNumber = data2.PatchNumber
      };
    }
    catch
    {
      return (PXVersionInfo.PatchMetadata) null;
    }
  }

  [PXInternalUseOnly]
  public static 
  #nullable disable
  string DatabaseVersion
  {
    get
    {
      if (!string.IsNullOrEmpty(PXVersionInfo._dbversion))
        return PXVersionInfo._dbversion;
      PXVersionInfo._dbversion = IEnumerableExtensions.GetPriorityVersion(PXDatabase.SelectVersions())?.Version;
      return PXVersionInfo._dbversion;
    }
  }

  public static string AcumaticaBuildVersion
  {
    get
    {
      if (!string.IsNullOrEmpty(PXVersionInfo._acumaticaBuildVersion))
        return PXVersionInfo._acumaticaBuildVersion;
      PXVersionInfo._acumaticaBuildVersion = "1.0.0.0";
      object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyFileVersionAttribute), false);
      if (customAttributes != null && customAttributes.Length != 0)
        PXVersionInfo._acumaticaBuildVersion = ((AssemblyFileVersionAttribute) customAttributes[0]).Version;
      return PXVersionInfo._acumaticaBuildVersion;
    }
  }

  public static string ProductVersion => "2025 R2";

  [Obsolete("Use ILicensing.PrettyInstallationId")]
  public static string InstallationID => LicensingManager.Instance.PrettyInstallationId;

  public static string Copyright
  {
    get
    {
      string copyright = string.Empty;
      object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
      if (customAttributes != null && customAttributes.Length != 0)
        copyright = ((AssemblyCopyrightAttribute) customAttributes[0]).Copyright;
      return copyright;
    }
  }

  public sealed class VersionService : IVersionService
  {
    private readonly ILicensing _licensing;

    public VersionService() => this._licensing = ServiceLocator.Current.GetInstance<ILicensing>();

    public PX.Common.Service.Version BuildVersion
    {
      get
      {
        string str = "1.0.0.0";
        object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyFileVersionAttribute), false);
        if (customAttributes != null && customAttributes.Length != 0)
          str = ((AssemblyFileVersionAttribute) customAttributes[0]).Version;
        if (str == "1.0.0.0")
          str = IEnumerableExtensions.GetPriorityVersion(PXDatabase.SelectVersions()).Version;
        if (!string.IsNullOrEmpty(str))
        {
          string[] strArray = str.Split('.');
          uint result1;
          uint result2;
          if (strArray.Length > 2 && uint.TryParse(strArray[0], out result1) && uint.TryParse(strArray[2], out result2))
            return new PX.Common.Service.Version(result1, result2);
          if (strArray.Length > 1 && uint.TryParse(strArray[0], out result1) && uint.TryParse(strArray[1], out result2))
            return new PX.Common.Service.Version(result1, result2);
        }
        return PX.Common.Service.Version.Empty;
      }
    }

    public string ApplicationVersion => PXVersionInfo.Version;

    public string ProductVersion => PXVersionInfo.ProductVersion;

    public string ApplicationName
    {
      get => $"Acumatica ERP {PXVersionInfo.Version} {this._licensing.PrettyInstallationId}";
    }

    public string Copyright => PXVersionInfo.Copyright;
  }

  private class PatchMetadata
  {
    public string PatchNumber { get; set; }

    public string PatchNotes { get; set; }
  }
}
