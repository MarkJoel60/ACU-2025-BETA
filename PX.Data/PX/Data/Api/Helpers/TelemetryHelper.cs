// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Helpers.TelemetryHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text.RegularExpressions;

#nullable disable
namespace PX.Data.Api.Helpers;

public static class TelemetryHelper
{
  public static readonly Regex GiOdataRegex = new Regex("^~/t/[^/]+/api/odata/gi/", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
  public static readonly Regex DacOdataRegex = new Regex("^~/t/[^/]+/api/odata/dac/", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
}
