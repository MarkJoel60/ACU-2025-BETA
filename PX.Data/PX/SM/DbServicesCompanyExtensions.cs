// Decompiled with JetBrains decompiler
// Type: PX.SM.DbServicesCompanyExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.DbServices.Model;

#nullable disable
namespace PX.SM;

internal static class DbServicesCompanyExtensions
{
  public static string GetDescription(this CompanyHeader c)
  {
    string str = (string) null;
    if (!string.IsNullOrEmpty(c.Cd) && !int.TryParse(c.Cd, out int _))
      str = c.Cd;
    if (string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(c.Key))
      str = c.Key;
    return !string.IsNullOrEmpty(str) ? $"{str} ({c.Id.ToString()})" : c.Id.ToString();
  }
}
