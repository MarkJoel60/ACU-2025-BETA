// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.VersionService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.BulkInsert.Tools;
using PX.Data.Update;
using System;
using System.Reflection;

#nullable disable
namespace PX.Data.Services;

/// <exclude />
[Obsolete("Will be replaced by the new PX.Version project in 2023R2")]
public class VersionService : IVersionService
{
  private const string ZERO = "00.000.0000";
  private const string MIN = "24.100.0139";
  private const string DEF = "1.00.0000";
  private const string REGEXP = "^[0-9]{2}\\.[0-9]{3}\\.[0-9]{2,4}\\.?[0-9]{2,4}$";
  private string _oemVersion;

  public string ZeroVersion => "00.000.0000";

  public string MinimumVersion => "24.100.0139";

  public string DefaultVersion => "1.00.0000";

  public string VersionRegExp => "^[0-9]{2}\\.[0-9]{3}\\.[0-9]{2,4}\\.?[0-9]{2,4}$";

  public Version AssemblyVersion
  {
    get
    {
      if (!string.IsNullOrEmpty(this.OemVersion))
        return new Version(this.OemVersion);
      object[] customAttributes = typeof (PXView).Assembly.GetCustomAttributes(typeof (AssemblyFileVersionAttribute), false);
      return customAttributes != null && customAttributes.Length != 0 ? PXVersionHelper.Convert(((AssemblyFileVersionAttribute) customAttributes[0]).Version) : new Version();
    }
  }

  public string GetVersionString(Version vers, bool pxFormat)
  {
    string versionString;
    if (!pxFormat)
      versionString = vers.ToString();
    else if (vers.Revision > 0)
      versionString = $"{vers.Major}.{vers.Minor.ToString().PadLeft(2, '0')}.{vers.Build.ToString().PadLeft(4, '0')}.{vers.Revision.ToString().PadLeft(4, '0')}";
    else
      versionString = vers.Build <= 0 ? (vers.Minor <= 0 ? (vers.Major <= 0 ? $"{"0"}.{"00"}.{"0000"}" : $"{vers.Major}.{"00"}.{"0000"}") : $"{vers.Major}.{vers.Minor.ToString().PadLeft(2, '0')}.{"00"}") : $"{vers.Major}.{vers.Minor.ToString().PadLeft(2, '0')}.{vers.Build.ToString().PadLeft(4, '0')}";
    return versionString;
  }

  private string OemVersion
  {
    get
    {
      if (!string.IsNullOrEmpty(this._oemVersion))
        return this._oemVersion;
      Assembly fromCurrentDomain = VersionHelper.GetAssemblyWithPXOEMVersionAttributeFromCurrentDomain();
      if (fromCurrentDomain == (Assembly) null)
        return "";
      this._oemVersion = VersionHelper.GetOEMVersionFromAssembly(fromCurrentDomain);
      return this._oemVersion;
    }
  }
}
