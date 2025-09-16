// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXInstanceHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Update.LicensingService;
using PX.SM;
using PX.SM.Alias;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Xml;

#nullable disable
namespace PX.Data.Update;

public static class PXInstanceHelper
{
  private static readonly string customizationPath = HostingEnvironment.MapPath("~/App_Data/CustomizationPublishedInfo.xml");
  private const string defScopeUser = "admin";
  private const string defAppdataUrl = "~/App_Data";
  private const string defUpdateDataUrl = "~/App_Data/Database";
  private static bool throwExceptions;
  private static string scopeUser;
  private static string updateDataFolder;

  internal static string UserOfTheProcess { get; private set; }

  public static bool ThrowExceptions
  {
    get => PXInstanceHelper.throwExceptions;
    set => PXInstanceHelper.throwExceptions = value;
  }

  public static string ScopeUser
  {
    get
    {
      string login = string.IsNullOrEmpty(PXInstanceHelper.scopeUser) ? "admin" : PXInstanceHelper.scopeUser;
      try
      {
        string username;
        string company;
        LegacyCompanyService.ParseLogin(login, out username, out company, out string _);
        if (PXDatabase.Companies.Length != 0)
        {
          if (string.IsNullOrEmpty(company))
            login = $"{login}@{PXDatabase.Companies[0]}";
        }
        else
          login = username;
      }
      catch
      {
      }
      return login;
    }
    private set
    {
      if (string.IsNullOrEmpty(value))
        return;
      PXInstanceHelper.scopeUser = value;
    }
  }

  public static string RootFolder => HostingEnvironment.MapPath("~/");

  public static string AppDataFolder => HostingEnvironment.MapPath("~/App_Data");

  public static string UpdateDataFolder
  {
    get
    {
      PXInstanceHelper.updateDataFolder = ConfigurationManager.AppSettings["UpdateDataPath"];
      return !string.IsNullOrEmpty(PXInstanceHelper.updateDataFolder) ? PXInstanceHelper.updateDataFolder : HostingEnvironment.MapPath("~/App_Data/Database");
    }
    private set
    {
      if (string.IsNullOrEmpty(value))
        return;
      PXInstanceHelper.updateDataFolder = value;
    }
  }

  public static int IISVersion
  {
    get
    {
      if (HttpContext.Current == null)
        throw new ArgumentException("HttpContext.Current");
      string str = HttpContext.Current.Request.ServerVariables["SERVER_SOFTWARE"];
      if (str.Contains<char>('.'))
        str = str.Substring(0, str.LastIndexOf('.'));
      if (str.Contains<char>('/'))
        str = str.Substring(str.LastIndexOf('/') + 1);
      return string.IsNullOrEmpty(str) ? -1 : int.Parse(str);
    }
  }

  public static bool Impersonation
  {
    get
    {
      IdentitySection section = (IdentitySection) WebConfigurationManager.GetSection("system.web/identity");
      return section != null && section.Impersonate;
    }
  }

  public static string HostName => PXInstanceHelper.GetHostName(HttpContext.Current?.Server);

  public static string GetHostName(HttpServerUtility server)
  {
    if (server == null)
      return (string) null;
    string machineName = server.MachineName;
    string applicationVirtualPath = HostingEnvironment.ApplicationVirtualPath;
    string str;
    if (applicationVirtualPath == null)
      str = (string) null;
    else
      str = applicationVirtualPath.Trim('/');
    return $"{machineName}/{str}";
  }

  public static string InstanceID
  {
    get
    {
      return PXCriptoHelper.ConvertBytes(PXCriptoHelper.CalculateMD5(HostingEnvironment.ApplicationID));
    }
  }

  public static string DatabaseName
  {
    get
    {
      using (IDbConnection connection = (IDbConnection) PXDatabase.CreateConnection())
      {
        if (connection != null)
          return connection.Database;
      }
      return (string) null;
    }
  }

  public static int DatabaseId
  {
    get
    {
      if (PXAccess.IsMultiDbMode)
      {
        using (IDbConnection connection = (IDbConnection) PXDatabase.CreateConnection())
        {
          if (connection != null)
            return connection.ConnectionString.GetHashCode();
        }
      }
      return 0;
    }
  }

  public static DatabaseInfo DatabaseInfo => PXDatabase.Provider.SelectDatabaseInfo();

  public static string IPAddress
  {
    get
    {
      string ipAddress = (string) null;
      foreach (System.Net.IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
      {
        if (address.AddressFamily == AddressFamily.InterNetwork)
          ipAddress = ipAddress == null ? address.ToString() : $"{ipAddress},{address.ToString()}";
      }
      return ipAddress;
    }
  }

  public static PXInstanceType CurrentInstanceType
  {
    get
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle(typeof (PX.SM.Version), new PXDataField(typeof (UPHistoryComponents.componentName).Name), (PXDataField) new PXDataFieldValue(typeof (UPHistoryComponents.componentType).Name, (object) 'P')))
      {
        switch (pxDataRecord.GetString(0))
        {
          case "Application":
            return PXInstanceType.Erp;
          case "Studio":
            return PXInstanceType.Studio;
        }
      }
      throw new PXException("Unknown instance type");
    }
  }

  public static string PublishedCustomizations
  {
    get
    {
      XmlDocument xmlDocument = new XmlDocument();
      if (!System.IO.File.Exists(PXInstanceHelper.customizationPath))
        return (string) null;
      xmlDocument.Load(PXInstanceHelper.customizationPath);
      return xmlDocument.DocumentElement?.Attributes["project"]?.Value;
    }
  }

  public static LicenseCustInfo[] GetCustProjects()
  {
    try
    {
      List<LicenseCustInfo> licenseCustInfoList = new List<LicenseCustInfo>();
      string publishedCustomizations = PXInstanceHelper.PublishedCustomizations;
      string[] strArray;
      if (publishedCustomizations == null)
        strArray = (string[]) null;
      else
        strArray = publishedCustomizations.Split(new char[1]
        {
          ','
        }, StringSplitOptions.RemoveEmptyEntries);
      string[] source = strArray;
      if (source != null && source.Length != 0)
      {
        foreach (CustProject custProject in (IEnumerable<CustProject>) PXDatabase.Select<CustProject>())
        {
          if (((IEnumerable<string>) source).Contains<string>(custProject.Name))
            licenseCustInfoList.Add(new LicenseCustInfo()
            {
              Id = custProject.ProjID,
              Name = custProject.Name,
              Description = custProject.Description
            });
        }
      }
      return licenseCustInfoList.ToArray();
    }
    catch
    {
    }
    return (LicenseCustInfo[]) null;
  }

  public static int CurrentCompany
  {
    get => PXDatabase.Provider.getCompanyID("Company", out companySetting _);
  }

  public static string GetTempFolder(string prefix)
  {
    string str = Path.GetTempPath();
    if (string.IsNullOrEmpty(str))
      throw new PXException("The Temp directory is not defined.");
    if (!Directory.Exists(str))
      throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("The Temp directory does not exist: {0}", (object) str));
    if (!string.IsNullOrEmpty(prefix))
      str = Path.Combine(str, prefix);
    return Path.Combine(str, Path.GetFileNameWithoutExtension(Path.GetTempFileName()));
  }

  /// <summary>Store user to use it on installation id generating</summary>
  [PXInternalUseOnly]
  public static void CaptureProcessUser()
  {
    PXInstanceHelper.UserOfTheProcess = WindowsIdentity.GetCurrent().User.Value;
  }
}
