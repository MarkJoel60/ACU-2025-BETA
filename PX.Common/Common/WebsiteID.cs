// Decompiled with JetBrains decompiler
// Type: PX.Common.WebsiteID
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.IO;
using System.Security.Principal;
using System.Web.Hosting;

#nullable disable
namespace PX.Common;

public static class WebsiteID
{
  private static string \u0002;
  public static string RuntimeKey = Guid.NewGuid().ToString();

  public static string Key
  {
    get
    {
      if (WebsiteID.\u0002 == null)
        WebsiteID.\u0002 = WebsiteID.\u0002().Replace("-", "");
      return WebsiteID.\u0002;
    }
  }

  private static string \u0002()
  {
    string location = typeof (WebsiteID).Assembly.Location;
    string path = HostingEnvironment.MapPath($"~/App_Data/WebsiteID{((uint) PXReflectionSerializer.GetStableHash(location.Substring(0, location.LastIndexOf("\\assembly\\", StringComparison.InvariantCultureIgnoreCase)))).ToString()}.txt");
    if (File.Exists(path))
      return File.ReadAllText(path);
    try
    {
      string contents = Guid.NewGuid().ToString();
      File.WriteAllText(path, contents);
      return contents;
    }
    catch (UnauthorizedAccessException ex)
    {
      WindowsIdentity current = WindowsIdentity.GetCurrent();
      if (current != null)
        throw new Exception("Failed to write WebsiteID from the user: " + current.Name, (Exception) ex);
      throw;
    }
  }
}
