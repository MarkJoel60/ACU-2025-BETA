// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXVersionHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Data.Services;
using PX.DbServices.Model;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Data.Update;

public static class PXVersionHelper
{
  private static IVersionService VersionService
  {
    get => ServiceLocator.Current.GetInstance<IVersionService>();
  }

  public static string ZeroVersion => PXVersionHelper.VersionService.ZeroVersion;

  public static string MinimumVersion => PXVersionHelper.VersionService.MinimumVersion;

  public static string DefaultVersion => PXVersionHelper.VersionService.DefaultVersion;

  public static Version GetAssemblyVersion() => PXVersionHelper.VersionService.AssemblyVersion;

  internal static IEnumerable<DataVersion> GetDatabaseVersions()
  {
    using (new PXImpersonationContext(PXInstanceHelper.ScopeUser))
      return PXDatabase.SelectVersions();
  }

  [Obsolete]
  internal static Version GetDatabaseVersion()
  {
    return new Version(IEnumerableExtensions.GetPriorityVersion(PXVersionHelper.GetDatabaseVersions()).Version);
  }

  public static Version Convert(string version)
  {
    if (version == null)
      return (Version) null;
    version = version.Replace("_", ".");
    string[] strArray = version.Split(new char[1]{ '.' }, StringSplitOptions.RemoveEmptyEntries);
    Version version1 = new Version();
    int major = strArray.Length != 0 ? int.Parse(strArray[0]) : 0;
    int num1 = strArray.Length > 1 ? int.Parse(strArray[1]) : 0;
    int num2 = strArray.Length > 2 ? int.Parse(strArray[2]) : 0;
    int num3 = strArray.Length > 3 ? int.Parse(strArray[3]) : 0;
    int minor = num1;
    int build = num2;
    int revision = num3;
    return new Version(major, minor, build, revision);
  }

  public static bool Validate(string version)
  {
    return !string.IsNullOrEmpty(version) && new Regex(PXVersionHelper.VersionService.VersionRegExp).IsMatch(version);
  }

  public static int Compare(string version1, string version2)
  {
    return PXVersionHelper.Convert(version1).CompareTo(PXVersionHelper.Convert(version2));
  }

  public static string ToString(this Version vers, bool pxFormat)
  {
    return PXVersionHelper.VersionService.GetVersionString(vers, pxFormat);
  }
}
