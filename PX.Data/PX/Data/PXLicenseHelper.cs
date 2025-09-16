// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLicenseHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Configuration;
using PX.BulkInsert.Tools;
using PX.Common;
using PX.Data.Licensing;
using PX.Data.Update;
using PX.DbServices.Model;
using PX.Licensing;
using PX.Logging.Sinks.SystemEventsDbSink;
using PX.SM;
using PX.SP;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Linq;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public static class PXLicenseHelper
{
  private static ILogger Logger = Serilog.Core.Logger.None;
  private static bool? _autoRenewal;

  internal static void InitializeLogging(ILogger logger)
  {
    PXLicenseHelper.Logger = LicensingLog.ForClassContext(logger, typeof (PXLicenseHelper));
  }

  [Obsolete("Use ILicensing.License")]
  public static PXLicense License => LicensingManager.Instance.License;

  [Obsolete("Use ILicensing.PrettyInstallationId")]
  public static string InstallationID => LicensingManager.Instance.PrettyInstallationId;

  public static bool AutoRenewal
  {
    get
    {
      if (!PXLicenseHelper._autoRenewal.HasValue)
      {
        bool result = false;
        string appSetting = ConfigurationManager.AppSettings[nameof (AutoRenewal)];
        PXLicenseHelper._autoRenewal = new bool?(((appSetting == null ? 0 : (bool.TryParse(appSetting, out result) ? 1 : 0)) & (result ? 1 : 0)) != 0);
      }
      return PXLicenseHelper._autoRenewal.GetValueOrDefault();
    }
  }

  internal static bool ForceRequestLicense { get; set; }

  internal static void OnSession(this ILicensingManager licensingManager, ILicensingSession session)
  {
    PXLicenseHelper.Logger.ForMethodContext(nameof (OnSession)).Information<string>("Session {SessionID} expired", session.SessionId);
    licensingManager.RemoveSession(session);
  }

  internal static void OnLogout(this ILicensingManager licensingManager, ILicensingSession session)
  {
    PXLicenseHelper.Logger.ForMethodContext(nameof (OnLogout)).Information<string>("Logging out session {SessionID}", session.SessionId);
    licensingManager.RemoveSession(session);
  }

  internal static string InstallationIdBase
  {
    get => PXLicenseHelper.GetBaseInstallationId((ILogger) null, (LogEventLevel) 0);
  }

  public static string CalcInstallationId(
    string hostName,
    string userName,
    string siteName,
    string virtPath)
  {
    return hostName + userName + siteName + virtPath;
  }

  internal static string GetBaseInstallationId(ILogger logger, LogEventLevel level)
  {
    string hostName = Dns.GetHostName();
    string userOfTheProcess = PXInstanceHelper.UserOfTheProcess;
    string siteName = HostingEnvironment.SiteName;
    string applicationVirtualPath = HostingEnvironment.ApplicationVirtualPath;
    string baseInstallationId = PXLicenseHelper.CalcInstallationId(hostName, userOfTheProcess, siteName, applicationVirtualPath);
    if (logger != null && logger.IsEnabled(level))
      logger.ForContext("DnsHostName", (object) hostName, false).ForContext("WindowsUser", (object) userOfTheProcess, false).ForContext("SiteName", (object) siteName, false).ForContext("ApplicationVirtualPath", (object) applicationVirtualPath, false).Write<string>(level, "Base installation ID is {BaseInstallationId}", baseInstallationId);
    return baseInstallationId;
  }

  internal static string GetLicenseKey(string installationId, bool broadcast)
  {
    string licenseKey1 = (string) null;
    string licenseKey2 = (string) null;
    List<PXDataField> pxDataFieldList = new List<PXDataField>();
    pxDataFieldList.Add((PXDataField) new PXDataField<PX.SM.Licensing.licensingKey>());
    pxDataFieldList.Add((PXDataField) new PXDataField<PX.SM.Licensing.installationID>());
    if (!broadcast)
      pxDataFieldList.Add((PXDataField) new PXDataFieldValue<PX.SM.Licensing.installationID>((object) installationId));
    ILogger ilogger = PXLicenseHelper.Logger.ForMethodContext(nameof (GetLicenseKey));
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PX.SM.Licensing>((PXDataField) new PXDataField<PX.SM.Licensing.licensingKey>(), (PXDataField) new PXDataField<PX.SM.Licensing.installationID>()))
    {
      if (pxDataRecord != null)
      {
        if (licenseKey1 == null)
          licenseKey2 = pxDataRecord.GetString(0);
        licenseKey1 = pxDataRecord.GetString(0);
        if (pxDataRecord.GetString(1) == installationId)
        {
          ilogger.Information<string, string, string>("Found license key {LicenseKey} for installation {InstallationID}: {LicenseKeySelectionReason}", licenseKey1, installationId, "installation id match");
          return licenseKey1;
        }
        if (licenseKey1 != licenseKey2)
          licenseKey2 = (string) null;
      }
    }
    if (licenseKey2 == null)
      ilogger.Information<string, string, string>("Found license key {LicenseKey} for installation {InstallationID}: {LicenseKeySelectionReason}", "NONE", installationId, licenseKey1 != null ? "more than one license key in DB" : "no license keys in DB");
    else
      ilogger.Information<string, string, string>("Found license key {LicenseKey} for installation {InstallationID}: {LicenseKeySelectionReason}", licenseKey1, installationId, "the only license key in DB");
    return licenseKey2;
  }

  private static string GetConfiguration(
    string key,
    string installationId,
    PXLicenseReason reason,
    bool deactivateOldInstance,
    bool throwExceededCountException)
  {
    XDocument xdocument = new XDocument(new object[1]
    {
      (object) new XElement((XName) "instance", new object[2]
      {
        (object) PXLicenseHelper.GetSystemConfiguration(key, installationId, reason, deactivateOldInstance, throwExceededCountException),
        (object) PXLicenseHelper.GetApplicationConfiguration(reason)
      })
    });
    StringBuilder sb = new StringBuilder();
    using (StringWriter stringWriter = new StringWriter(sb))
    {
      xdocument.Save((TextWriter) stringWriter);
      return sb.ToString();
    }
  }

  private static IEnumerable<XElement> GetSystemConfiguration(
    string key,
    string installationId,
    PXLicenseReason reason,
    bool deactivateOldInstance,
    bool throwExceededCountException)
  {
    string str1 = key ?? string.Empty;
    List<XElement> systemConfiguration = new List<XElement>();
    systemConfiguration.Add(new XElement((XName) "generalInfo", new object[9]
    {
      (object) new XAttribute((XName) "version", (object) IEnumerableExtensions.GetPriorityVersion(PXVersionHelper.GetDatabaseVersions()).Version),
      (object) new XAttribute((XName) "type", (object) PXInstanceHelper.CurrentInstanceType.ToString()),
      (object) new XAttribute((XName) "installationID", (object) installationId),
      (object) new XAttribute((XName) "licenseKey", (object) str1),
      (object) new XAttribute((XName) nameof (reason), (object) reason.ToString()),
      (object) new XAttribute((XName) "culture", (object) Thread.CurrentThread.CurrentCulture.ToString()),
      (object) new XAttribute((XName) nameof (deactivateOldInstance), (object) deactivateOldInstance),
      (object) new XAttribute((XName) nameof (throwExceededCountException), (object) throwExceededCountException),
      (object) new XAttribute((XName) "isPortal", (object) PortalHelper.IsPortalContext().ToString())
    }));
    systemConfiguration.Add(new XElement((XName) "environmentInfo", new object[2]
    {
      (object) new XAttribute((XName) "processorCount", (object) Environment.ProcessorCount.ToString()),
      (object) new XAttribute((XName) "osVersion", (object) Environment.OSVersion.ToString())
    }));
    systemConfiguration.Add(new XElement((XName) "instanceInfo", new object[4]
    {
      (object) new XAttribute((XName) "domainName", (object) (HostingEnvironment.SiteName + HostingEnvironment.ApplicationVirtualPath)),
      (object) new XAttribute((XName) "hostName", (object) (Dns.GetHostName() ?? string.Empty)),
      (object) new XAttribute((XName) "ipAddress", (object) (PXInstanceHelper.IPAddress ?? string.Empty)),
      (object) new XAttribute((XName) "databaseName", (object) (PXInstanceHelper.DatabaseName ?? string.Empty))
    }));
    if (!string.IsNullOrEmpty(str1))
    {
      XElement xelement = new XElement((XName) "otherInstances");
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PX.SM.Licensing>((PXDataField) new PXDataField<PX.SM.Licensing.installationID>(), (PXDataField) new PXDataField<PX.SM.Licensing.activity>(), (PXDataField) new PXDataFieldValue<PX.SM.Licensing.licensingKey>((object) str1)))
      {
        string str2 = pxDataRecord.GetString(0);
        System.DateTime? dateTime = pxDataRecord.GetDateTime(1);
        if (dateTime.HasValue)
          xelement.Add((object) new XElement((XName) "instance", new object[2]
          {
            (object) new XAttribute((XName) "id", (object) str2),
            (object) new XAttribute((XName) "activity", (object) dateTime)
          }));
      }
      if (xelement.Elements().Count<XElement>() > 0)
        systemConfiguration.Add(xelement);
    }
    return (IEnumerable<XElement>) systemConfiguration;
  }

  private static IEnumerable<XElement> GetApplicationConfiguration(PXLicenseReason reason)
  {
    if (reason != PXLicenseReason.Installation && reason != PXLicenseReason.Renewal || PXInstanceHelper.CurrentInstanceType == PXInstanceType.Studio)
      return (IEnumerable<XElement>) new XElement[0];
    int? branchId = PXAccess.GetBranchID();
    Guid userId = PXAccess.GetUserID();
    int? companyBaccountId = PXLicenseHelper.GetCompanyBAccountID(branchId);
    XElement[] source = new XElement[2]
    {
      new XElement((XName) "companyInfo", (object) PXLicenseHelper.GetCompanyInfo(companyBaccountId).Select<KeyValuePair<string, string>, XAttribute>((Func<KeyValuePair<string, string>, XAttribute>) (info =>
      {
        KeyValuePair<string, string> keyValuePair = info;
        XName key = (XName) keyValuePair.Key;
        keyValuePair = info;
        string str = keyValuePair.Value ?? string.Empty;
        return new XAttribute(key, (object) str);
      }))),
      new XElement((XName) "userInfo", (object) PXLicenseHelper.GetUserInfo(companyBaccountId, userId).Select<KeyValuePair<string, string>, XAttribute>((Func<KeyValuePair<string, string>, XAttribute>) (info =>
      {
        KeyValuePair<string, string> keyValuePair = info;
        XName key = (XName) keyValuePair.Key;
        keyValuePair = info;
        string str = keyValuePair.Value ?? string.Empty;
        return new XAttribute(key, (object) str);
      })))
    };
    return !((IEnumerable<XElement>) source).Any<XElement>((Func<XElement, bool>) (configuration => configuration.Attributes().Any<XAttribute>((Func<XAttribute, bool>) (attribute => !string.IsNullOrEmpty(attribute.Value))))) ? (IEnumerable<XElement>) new XElement[0] : (IEnumerable<XElement>) source;
  }

  private static int? GetCompanyBAccountID(int? branchID)
  {
    int? companyBaccountId = new int?();
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PX.Data.Licensing.Branch>((PXDataField) new PXDataField<PX.Data.Licensing.Branch.bAccountID>(), (PXDataField) new PXDataFieldValue<PX.Data.Licensing.Branch.branchID>((object) branchID)))
    {
      if (pxDataRecord != null)
        companyBaccountId = pxDataRecord.GetInt32(0);
    }
    return companyBaccountId;
  }

  private static Dictionary<string, string> GetUserInfo(int? companyBAccountID, Guid userID)
  {
    Dictionary<string, string> userInfo = new Dictionary<string, string>();
    if (companyBAccountID.HasValue)
    {
      int? defAddressID;
      int? defContactID;
      PXLicenseHelper.InitializeUserInfoParams(companyBAccountID.Value, userID, out defAddressID, out defContactID);
      if (defAddressID.HasValue)
      {
        foreach (KeyValuePair<string, string> keyValuePair in PXLicenseHelper.GetAddressInfo(defAddressID.Value, companyBAccountID.Value))
          userInfo.Add(keyValuePair.Key, keyValuePair.Value);
      }
      if (defContactID.HasValue)
      {
        foreach (KeyValuePair<string, string> keyValuePair in PXLicenseHelper.GetUserContactInfo(defContactID.Value, companyBAccountID.Value))
          userInfo.Add(keyValuePair.Key, keyValuePair.Value);
      }
    }
    return userInfo;
  }

  private static Dictionary<string, string> GetCompanyInfo(int? companyBAccountID)
  {
    Dictionary<string, string> companyInfo = new Dictionary<string, string>();
    if (companyBAccountID.HasValue)
    {
      int? defAddressID;
      int? defContactID;
      PXLicenseHelper.InitializeCompanyInfoParams(companyBAccountID.Value, out defAddressID, out defContactID);
      if (defAddressID.HasValue)
      {
        foreach (KeyValuePair<string, string> keyValuePair in PXLicenseHelper.GetAddressInfo(defAddressID.Value, companyBAccountID.Value))
          companyInfo.Add(keyValuePair.Key, keyValuePair.Value);
      }
      if (defContactID.HasValue)
      {
        foreach (KeyValuePair<string, string> keyValuePair in PXLicenseHelper.GetContactInfo(defContactID.Value, companyBAccountID.Value))
          companyInfo.Add(keyValuePair.Key, keyValuePair.Value);
      }
    }
    return companyInfo;
  }

  private static Dictionary<string, string> GetUserContactInfo(
    int defContactID,
    int companyBAccountID)
  {
    Dictionary<string, string> contactInfo = PXLicenseHelper.GetContactInfo(defContactID, companyBAccountID);
    PXLicenseHelper.MergeUserInfo(contactInfo, PXLicenseHelper.GetCurrentUserProfileInfo());
    return contactInfo;
  }

  private static void MergeUserInfo(
    Dictionary<string, string> mergeToInfo,
    Dictionary<string, string> mergeFromInfo)
  {
    foreach (string key in mergeToInfo.Keys.Where<string>((Func<string, bool>) (key => mergeFromInfo.ContainsKey(key) && !string.IsNullOrEmpty(mergeFromInfo[key]) && string.IsNullOrEmpty(mergeToInfo[key]))).ToArray<string>())
      mergeToInfo[key] = mergeFromInfo[key];
  }

  private static Dictionary<string, string> GetCurrentUserProfileInfo()
  {
    Dictionary<string, string> currentUserProfileInfo = new Dictionary<string, string>();
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>((PXDataField) new PXDataField<Users.fullName>(), (PXDataField) new PXDataField<Users.email>(), (PXDataField) new PXDataField<Users.phone>(), (PXDataField) new PXDataFieldValue<Users.pKID>((object) PXAccess.GetUserID())))
    {
      if (pxDataRecord != null)
      {
        currentUserProfileInfo["fullName"] = pxDataRecord.GetString(0);
        currentUserProfileInfo["eMail"] = pxDataRecord.GetString(1);
        currentUserProfileInfo["phone1"] = pxDataRecord.GetString(2);
      }
    }
    return currentUserProfileInfo;
  }

  private static Dictionary<string, string> GetContactInfo(int contactID, int bAccountID)
  {
    Dictionary<string, string> contactInfo = new Dictionary<string, string>();
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PX.Data.Licensing.Contact>((PXDataField) new PXDataField<PX.Data.Licensing.Contact.fullName>(), (PXDataField) new PXDataField<PX.Data.Licensing.Contact.eMail>(), (PXDataField) new PXDataField<PX.Data.Licensing.Contact.webSite>(), (PXDataField) new PXDataField<PX.Data.Licensing.Contact.phone1>(), (PXDataField) new PXDataField<PX.Data.Licensing.Contact.phone2>(), (PXDataField) new PXDataFieldValue<PX.Data.Licensing.Contact.bAccountID>((object) bAccountID), (PXDataField) new PXDataFieldValue<PX.Data.Licensing.Contact.contactID>((object) contactID)))
    {
      if (pxDataRecord != null)
      {
        contactInfo["fullName"] = pxDataRecord.GetString(0);
        contactInfo["eMail"] = pxDataRecord.GetString(1);
        contactInfo["webSite"] = pxDataRecord.GetString(2);
        contactInfo["phone1"] = pxDataRecord.GetString(3);
        contactInfo["phone2"] = pxDataRecord.GetString(4);
      }
    }
    return contactInfo;
  }

  private static Dictionary<string, string> GetAddressInfo(int addressID, int bAccountID)
  {
    Dictionary<string, string> addressInfo = new Dictionary<string, string>();
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Address>((PXDataField) new PXDataField<Address.addressLine1>(), (PXDataField) new PXDataField<Address.addressLine2>(), (PXDataField) new PXDataField<Address.city>(), (PXDataField) new PXDataField<Address.countryID>(), (PXDataField) new PXDataField<Address.state>(), (PXDataField) new PXDataField<Address.postalCode>(), (PXDataField) new PXDataFieldValue<Address.bAccountID>((object) bAccountID), (PXDataField) new PXDataFieldValue<Address.addressID>((object) addressID)))
    {
      if (pxDataRecord != null)
      {
        addressInfo["addressLine1"] = pxDataRecord.GetString(0);
        addressInfo["addressLine2"] = pxDataRecord.GetString(1);
        addressInfo["city"] = pxDataRecord.GetString(2);
        addressInfo["countryID"] = pxDataRecord.GetString(3);
        addressInfo["state"] = pxDataRecord.GetString(4);
        addressInfo["postalCode"] = pxDataRecord.GetString(5);
      }
    }
    return addressInfo;
  }

  private static void InitializeUserInfoParams(
    int companyBAccountID,
    Guid userID,
    out int? defAddressID,
    out int? defContactID)
  {
    defAddressID = new int?();
    defContactID = new int?();
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PX.Data.Licensing.Contact>((PXDataField) new PXDataField<PX.Data.Licensing.Contact.contactID>(), (PXDataField) new PXDataFieldValue<PX.Data.Licensing.Contact.userID>((object) userID)))
    {
      if (pxDataRecord != null)
        defContactID = pxDataRecord.GetInt32(0);
    }
    if (!defContactID.HasValue)
      return;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PX.Data.Licensing.BAccount>((PXDataField) new PXDataField<PX.Data.Licensing.BAccount.defAddressID>(), (PXDataField) new PXDataFieldValue<PX.Data.Licensing.BAccount.defContactID>((object) defContactID), (PXDataField) new PXDataFieldValue<PX.Data.Licensing.BAccount.parentBAccountID>((object) companyBAccountID)))
    {
      if (pxDataRecord == null)
        return;
      defAddressID = pxDataRecord.GetInt32(0);
    }
  }

  private static void InitializeCompanyInfoParams(
    int companyBAccountID,
    out int? defAddressID,
    out int? defContactID)
  {
    defAddressID = new int?();
    defContactID = new int?();
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PX.Data.Licensing.BAccount>((PXDataField) new PXDataField<PX.Data.Licensing.BAccount.defAddressID>(), (PXDataField) new PXDataField<PX.Data.Licensing.BAccount.defContactID>(), (PXDataField) new PXDataFieldValue<PX.Data.Licensing.BAccount.bAccountID>((object) companyBAccountID)))
    {
      if (pxDataRecord == null)
        return;
      defAddressID = pxDataRecord.GetInt32(0);
      defContactID = pxDataRecord.GetInt32(1);
    }
  }

  public static PXLicenseDefinition ParceLicense(LicenseBucket bucket)
  {
    if (bucket == null)
    {
      PXLicenseHelper.Logger.ForMethodContext(nameof (ParceLicense)).Warning<string>("Returning null license definition: {BlankLicenseReason}", "bucket is null");
      return (PXLicenseDefinition) null;
    }
    if (string.IsNullOrEmpty(bucket.Restriction))
    {
      PXLicenseHelper.Logger.ForMethodContext(nameof (ParceLicense)).Warning<string>("Returning null license definition: {BlankLicenseReason}", "no restriction");
      return (PXLicenseDefinition) null;
    }
    XmlDocument xmlDocument = new XmlDocument()
    {
      XmlResolver = (XmlResolver) null
    };
    try
    {
      xmlDocument.LoadXml(bucket.Restriction);
    }
    catch (Exception ex)
    {
      PXLicenseHelper.Logger.ForMethodContext(nameof (ParceLicense)).Error(ex, "Error while loading license definition xml");
      throw;
    }
    XmlNode node1 = XMLHelper.GetNode(xmlDocument, "general", false);
    bool attributeValue1 = XMLHelper.TryGetAttributeValue<bool>(node1, "active");
    string attributeValue2 = XMLHelper.TryGetAttributeValue<string>(node1, "licensetype");
    string attributeValue3 = XMLHelper.TryGetAttributeValue<string>(node1, "installationID");
    XmlNode node2 = XMLHelper.GetNode(xmlDocument, "oauth", false);
    string attributeValue4 = XMLHelper.TryGetAttributeValue<string>(node2, "sharedSecret");
    string attributeValue5 = XMLHelper.TryGetAttributeValue<string>(node2, "internalLink");
    string attributeValue6 = XMLHelper.TryGetAttributeValue<string>(node2, "partnerLink");
    XmlNode node3 = XMLHelper.GetNode(xmlDocument, "issuing", false);
    System.DateTime attributeValue7 = XMLHelper.GetAttributeValue<System.DateTime>(node3, "validFrom");
    System.DateTime attributeValue8 = XMLHelper.GetAttributeValue<System.DateTime>(node3, "validTo");
    System.DateTime attributeValue9 = XMLHelper.GetAttributeValue<System.DateTime>(node3, "grasePeriod");
    System.DateTime attributeValue10 = XMLHelper.GetAttributeValue<System.DateTime>(node3, "notificationPeriod");
    System.DateTime attributeValue11 = XMLHelper.GetAttributeValue<System.DateTime>(node3, "nextRequest");
    System.DateTime attributeValue12 = XMLHelper.TryGetAttributeValue<System.DateTime>(node3, "issuedDate");
    string attributeValue13 = XMLHelper.TryGetAttributeValue<string>(node3, "issuedBy");
    string attributeValue14 = XMLHelper.TryGetAttributeValue<string>(node3, "type");
    bool attributeValue15 = XMLHelper.TryGetAttributeValue<bool>(node3, "isPortal");
    XmlNode node4 = XMLHelper.GetNode(xmlDocument, "restriction", false);
    int attributeValue16 = XMLHelper.TryGetAttributeValue<int>(node4, "companies");
    int attributeValue17 = XMLHelper.TryGetAttributeValue<int>(node4, "processors");
    int attributeValue18 = XMLHelper.TryGetAttributeValue<int>(node4, "users");
    string attributeValue19 = XMLHelper.GetAttributeValue<string>(node4, "verison");
    XmlNode node5 = XMLHelper.GetNode(xmlDocument, "application", false);
    string attributeValue20 = XMLHelper.TryGetAttributeValue<string>(node5, "domainName");
    string attributeValue21 = XMLHelper.TryGetAttributeValue<string>(node5, "companyName");
    string attributeValue22 = XMLHelper.TryGetAttributeValue<string>(node5, "customerName");
    List<string> list1 = new List<string>();
    XmlNode node6 = XMLHelper.GetNode(xmlDocument, "features", false);
    if (node6 != null && node6.HasChildNodes)
    {
      foreach (XmlNode childNode in XMLHelper.GetChildNodes(node6))
      {
        if (childNode.Name == "feature")
          list1.Add(XMLHelper.GetAttributeValue<string>(childNode, "name"));
      }
    }
    ReadOnlyCollection<string> features = new ReadOnlyCollection<string>((IList<string>) list1);
    List<LicenseRestriction> restrictions1 = ParseRestrictions(xmlDocument, "restrictions");
    List<LicenseRestriction> restrictions2 = ParseRestrictions(xmlDocument, "resourceRestrictions");
    List<LicenseRestriction> restrictions3 = ParseRestrictions(xmlDocument, "featureRestrictions");
    List<LicenseRestriction> restrictions4 = ParseRestrictions(xmlDocument, "businessRestrictions");
    List<string> list2 = new List<string>();
    XmlNode node7 = XMLHelper.GetNode(xmlDocument, "cloudServices", false);
    if (node7 != null && node7.HasChildNodes)
    {
      foreach (XmlNode childNode in XMLHelper.GetChildNodes(node7))
      {
        if (childNode.Name == "cloudService")
          list2.Add(XMLHelper.GetAttributeValue<string>(childNode, "name"));
      }
    }
    ReadOnlyCollection<string> readOnlyCollection = new ReadOnlyCollection<string>((IList<string>) list2);
    XmlNode xmlNode = XMLHelper.GetChildNodes(node3).Where<XmlNode>((Func<XmlNode, bool>) (_ => _ != null && _.Name == "resourceParams")).FirstOrDefault<XmlNode>();
    string attributeValue23 = XMLHelper.TryGetAttributeValue<string>(xmlNode, "resourceLevel");
    int attributeValue24 = XMLHelper.TryGetAttributeValue<int>(xmlNode, "comTransPerMonth");
    int attributeValue25 = XMLHelper.TryGetAttributeValue<int>(xmlNode, "erpTransPerMonth");
    int attributeValue26 = XMLHelper.TryGetAttributeValue<int>(xmlNode, "comTransPerDay");
    int attributeValue27 = XMLHelper.TryGetAttributeValue<int>(xmlNode, "erpTransPerDay");
    int WebServiceRequestsPerHour = XMLHelper.TryGetAttributeValue<int>(xmlNode, "webServicePerHour");
    if (WebServiceRequestsPerHour == 0)
      WebServiceRequestsPerHour = WebConfig.ApiPerHourLimit;
    float WebServiceProcessingUnits = XMLHelper.TryGetAttributeValue<float>(xmlNode, "cPUCoresForSdk");
    if ((double) WebServiceProcessingUnits == 0.0)
      WebServiceProcessingUnits = (float) WebConfig.ApiProcessingCores;
    int attributeValue28 = XMLHelper.TryGetAttributeValue<int>(xmlNode, "rowsReturnedByApi");
    int attributeValue29 = XMLHelper.TryGetAttributeValue<int>(xmlNode, "dataIncluded");
    int attributeValue30 = XMLHelper.TryGetAttributeValue<int>(xmlNode, "linesPerMasterRecord");
    int attributeValue31 = XMLHelper.TryGetAttributeValue<int>(xmlNode, "serialsPerDoc");
    int attributeValue32 = XMLHelper.TryGetAttributeValue<int>(xmlNode, "fixedAssets");
    int attributeValue33 = XMLHelper.TryGetAttributeValue<int>(xmlNode, "items");
    System.DateTime attributeValue34 = XMLHelper.TryGetAttributeValue<System.DateTime>(xmlNode, "dataLoadModeFrom");
    System.DateTime attributeValue35 = XMLHelper.TryGetAttributeValue<System.DateTime>(xmlNode, "dataLoadModeTo");
    int attributeValue36 = XMLHelper.TryGetAttributeValue<int>(xmlNode, "violationsInGrace");
    int attributeValue37 = XMLHelper.TryGetAttributeValue<int>(xmlNode, "overageInGrace");
    int attributeValue38 = XMLHelper.TryGetAttributeValue<int>(xmlNode, "allowedMonthlyViolations");
    int webServiceApiUsers = XMLHelper.TryGetAttributeValue<int>(xmlNode, "webServiceApiUsers");
    if (webServiceApiUsers == 0)
      webServiceApiUsers = WebConfig.ApiLoginsLimit;
    int attributeValue39 = XMLHelper.TryGetAttributeValue<int>(xmlNode, "daysQuiteFromGrace");
    int attributeValue40 = XMLHelper.TryGetAttributeValue<int>(xmlNode, "baccount");
    System.DateTime attributeValue41 = XMLHelper.TryGetAttributeValue<System.DateTime>(xmlNode != null ? XMLHelper.GetChildNodes(xmlNode).Where<XmlNode>((Func<XmlNode, bool>) (_ => _ != null && _.Name == "environment")).FirstOrDefault<XmlNode>() : (XmlNode) null, "clearViolationsDate");
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    foreach (KeyValuePair<string, string> keyValuePair in PXLicenseHelper.GetCloudServicesConfiguration(node7).Concat<KeyValuePair<string, string>?>(PXLicenseHelper.GetMobileNotificationConfiguration(xmlDocument)).Where<KeyValuePair<string, string>?>((Func<KeyValuePair<string, string>?, bool>) (p => p.HasValue)).Select<KeyValuePair<string, string>?, KeyValuePair<string, string>>((Func<KeyValuePair<string, string>?, KeyValuePair<string, string>>) (p => p.GetValueOrDefault())))
      dictionary.Add(keyValuePair.Key, keyValuePair.Value);
    return new PXLicenseDefinition(attributeValue1, attributeValue3, attributeValue4, attributeValue5, attributeValue6, attributeValue7, attributeValue8, attributeValue9, attributeValue10, attributeValue11, attributeValue12, attributeValue13, attributeValue20, attributeValue21, attributeValue22, attributeValue18, attributeValue16, attributeValue17, attributeValue19, attributeValue14, features, attributeValue23, attributeValue24, attributeValue25, attributeValue26, attributeValue27, WebServiceRequestsPerHour, (int) WebServiceProcessingUnits, attributeValue28, attributeValue29, attributeValue30, attributeValue31, attributeValue32, attributeValue33, attributeValue34, attributeValue35, attributeValue36, attributeValue37, attributeValue38, webServiceApiUsers, attributeValue39, attributeValue41, attributeValue40, attributeValue15, attributeValue2, restrictions1, restrictions2, restrictions3, restrictions4, (IReadOnlyDictionary<string, string>) EnumerableExtensions.AsReadOnly<string, string>((IDictionary<string, string>) dictionary));

    static List<LicenseRestriction> ParseRestrictions(XmlDocument data, string name)
    {
      List<LicenseRestriction> restrictions = new List<LicenseRestriction>();
      XmlNode node = XMLHelper.GetNode(data, name, false);
      if (node != null)
      {
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) node.Attributes)
        {
          try
          {
            restrictions.Add(new LicenseRestriction(attribute.Name, attribute.Value));
          }
          catch
          {
          }
        }
      }
      return restrictions;
    }
  }

  private static IEnumerable<KeyValuePair<string, string>?> GetCloudServicesConfiguration(
    XmlNode cloudServices)
  {
    if (cloudServices != null)
    {
      yield return Element(XMLHelper.TryGetAttributeValue(cloudServices, "discoveryUri"), new string[2]
      {
        "discovery",
        "uri"
      });
      XmlNode cloudServicesAuth = XMLHelper.SelectChildNode(cloudServices, "auth");
      if (cloudServicesAuth != null)
      {
        yield return Element(XMLHelper.TryGetAttributeValue(cloudServicesAuth, "clientID"), new string[2]
        {
          "auth",
          "clientId"
        });
        foreach (KeyValuePair<string, string>? nullable in cloudServicesAuth.ChildNodes.OfType<XmlElement>().Where<XmlElement>((Func<XmlElement, bool>) (el => el.NamespaceURI == cloudServicesAuth.NamespaceURI && el.LocalName == "clientSecret")).Select<XmlElement, KeyValuePair<string, string>?>((Func<XmlElement, int, KeyValuePair<string, string>?>) ((el, i) => Element(el.InnerText, new string[3]
        {
          "auth",
          "clientSecrets",
          i.ToString()
        }))))
          yield return nullable;
      }
    }

    static KeyValuePair<string, string>? Element(string value, string[] keys)
    {
      if (string.IsNullOrWhiteSpace(value))
        return new KeyValuePair<string, string>?();
      return new KeyValuePair<string, string>?(new KeyValuePair<string, string>(ConfigurationPath.Combine(new string[2]
      {
        "cloud-services",
        ConfigurationPath.Combine(keys)
      }), value));
    }
  }

  private static IEnumerable<KeyValuePair<string, string>?> GetMobileNotificationConfiguration(
    XmlDocument xdoc)
  {
    XmlDocument xmlDocument = xdoc;
    XmlNode mobileNotifications = xmlDocument != null ? XMLHelper.GetNode(xmlDocument, "mobileNotifications", false) : (XmlNode) null;
    if (mobileNotifications != null)
    {
      (string, string)[] valueTupleArray = new (string, string)[2]
      {
        ("endpointName", "apikeyserviceurl"),
        ("apiKey", "apikey")
      };
      for (int index = 0; index < valueTupleArray.Length; ++index)
      {
        (string str1, string str2) = valueTupleArray[index];
        string attributeValue = XMLHelper.TryGetAttributeValue<string>(mobileNotifications, str1);
        if (!string.IsNullOrWhiteSpace(attributeValue))
          yield return new KeyValuePair<string, string>?(new KeyValuePair<string, string>(ConfigurationPath.Combine(new string[3]
          {
            "mobile",
            "push-notifications",
            str2
          }), attributeValue));
      }
      valueTupleArray = ((string, string)[]) null;
    }
  }

  internal static LicenseBucket ParseLicenseFile(
    this ILicensingManager licensingManager,
    PX.SM.FileInfo info)
  {
    LicenseBucket licenseFile = PXLicenseHelper.ReadLicenseFile(info);
    if (licensingManager.ValidateLicense(licenseFile))
      return licenseFile;
    ILogger logger = PXLicenseHelper.Logger.ForMethodContext(nameof (ParseLicenseFile));
    if (logger.IsEnabled((LogEventLevel) 1))
      logger = logger.ForContext(licenseFile);
    logger.Error("Invalid license");
    throw new PXException("The received license has an invalid signature.");
  }

  public static LicenseBucket ReadLicenseFile(PX.SM.FileInfo info)
  {
    LicenseBucket licenseBucket = new LicenseBucket()
    {
      Date = new System.DateTime?(System.DateTime.UtcNow)
    };
    using (MemoryStream inStream = new MemoryStream(info.BinData))
    {
      XmlDocument xmlDocument = new XmlDocument()
      {
        XmlResolver = (XmlResolver) null
      };
      try
      {
        xmlDocument.Load((Stream) inStream);
      }
      catch (Exception ex)
      {
        PXLicenseHelper.Logger.ForMethodContext(nameof (ReadLicenseFile)).Error(ex, "Error while loading license definition xml");
        throw new PXException("The received file is not a valid license.", ex);
      }
      XmlAttribute attribute = XMLHelper.TryGetAttribute((XmlNode) xmlDocument.DocumentElement, "signature");
      if (attribute == null)
      {
        PXLicenseHelper.Logger.ForMethodContext(nameof (ReadLicenseFile)).Error("License does not contain signature");
        throw new PXException("The received license is not valid. A digital signature is missing.");
      }
      licenseBucket.Signature = attribute.Value;
      xmlDocument.DocumentElement.Attributes.Remove(attribute);
      XmlNode node = XMLHelper.GetNode(xmlDocument, "general", false);
      licenseBucket.LicenseKey = XMLHelper.GetAttributeValue(node, "licensekey");
      licenseBucket.SkipValidateOnLoadFromFile = XMLHelper.TryGetAttributeValue<bool>(node, "skipvalidateonloadfromfile");
      StringBuilder stringBuilder = new StringBuilder();
      StringBuilder output = stringBuilder;
      using (XmlWriter w = XmlWriter.Create(output, new XmlWriterSettings()
      {
        Indent = true,
        NewLineHandling = NewLineHandling.None
      }))
      {
        xmlDocument.WriteTo(w);
        w.Flush();
        licenseBucket.Restriction = stringBuilder.ToString();
      }
    }
    return licenseBucket;
  }

  internal static LicenseBucket RequestLicense(
    this ILicensingManager licensingManager,
    PXLicenseReason reason,
    bool deactivateOldInstance,
    string installationId = null)
  {
    return licensingManager.RequestLicense(PXLicenseHelper.GetLicenseKey(licensingManager.InstallationId, true), reason, deactivateOldInstance, false, installationId);
  }

  internal static LicenseBucket RequestLicense(
    this ILicensingManager licensingManager,
    string key,
    PXLicenseReason reason,
    bool deactivateOldInstance,
    bool throwExceededCountException,
    string externalInstallationId = null)
  {
    string str = externalInstallationId ?? licensingManager.InstallationId;
    if (string.IsNullOrEmpty(key))
    {
      PXLicenseHelper.Logger.ForMethodContext(nameof (RequestLicense)).Error<string, string>("Can't request license for key {LicenseKey}: {LicenseRequestError}", key, "empty license key");
      throw new PXException("The license key is invalid.");
    }
    if (!PXLicenseHelper.ValidateLicenseKey(key))
    {
      PXLicenseHelper.Logger.ForMethodContext(nameof (RequestLicense)).Error<string, string>("Can't request license for key {LicenseKey}: {LicenseRequestError}", key, "invalid license key format");
      throw new PXException("The license key is invalid.");
    }
    PXLicenseHelper.ForceRequestLicense = false;
    try
    {
      System.IO.File.Delete(Path.Combine(PXInstanceHelper.AppDataFolder, "license.temp.info"));
    }
    catch
    {
    }
    string configuration = PXLicenseHelper.GetConfiguration(key, str, reason, deactivateOldInstance, throwExceededCountException);
    LicenseBucket license = PXLicensingServer.GetLicense(key, str, configuration, licensingManager.Policy.GetStatistics());
    if (!licensingManager.ValidateLicense(license, str))
    {
      ILogger ilogger = PXLicenseHelper.Logger.ForMethodContext(nameof (RequestLicense));
      if (ilogger.IsEnabled((LogEventLevel) 0))
        ilogger = ilogger.ForContext("InstallationID", (object) str, false).ForContext("LicenseConfiguration", (object) configuration, false).ForContext(license);
      ilogger.Error<string>("License for key {LicenseKey} returned by server is invalid", key);
      throw new PXException("The received license has an invalid signature.");
    }
    if (PXLicenseHelper.Logger.IsEnabled((LogEventLevel) 0))
      PXLicenseHelper.Logger.ForMethodContext(nameof (RequestLicense)).ForContext("InstallationID", (object) str, false).ForContext("LicenseConfiguration", (object) configuration, false).ForContext(license).Verbose<string>("Succesfully requested license for key {LicenseKey}", key);
    return license;
  }

  internal static LicenseBucket ReadLicense(
    this ILicensingManager licensingManager,
    string extInstallationId = null)
  {
    ILogger ilogger = PXLicenseHelper.Logger.ForMethodContext(nameof (ReadLicense));
    string str1 = extInstallationId ?? licensingManager.InstallationId;
    LicenseBucket licenseBucket = (LicenseBucket) null;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PX.SM.Licensing>((PXDataField) new PXDataField<PX.SM.Licensing.licensingKey>(), (PXDataField) new PXDataField<PX.SM.Licensing.restriction>(), (PXDataField) new PXDataField<PX.SM.Licensing.signature>(), (PXDataField) new PXDataField<PX.SM.Licensing.date>(), (PXDataField) new PXDataFieldValue<PX.SM.Licensing.installationID>((object) str1)))
    {
      if (pxDataRecord != null)
      {
        string str2 = pxDataRecord.GetString(0);
        string str3 = pxDataRecord.GetString(1);
        string str4 = pxDataRecord.GetString(2);
        System.DateTime? dateTime = pxDataRecord.GetDateTime(3);
        if (ilogger.IsEnabled((LogEventLevel) 0))
          ilogger.ForContext("LicenseKey", (object) str2, false).ForContext("LicenseRestriction", (object) str3, false).ForContext("LicenseSignature", (object) str4, false).ForContext("LicenseDate", (object) dateTime, false).Verbose<string>("Found license for {InstallationID}", str1);
        licenseBucket = new LicenseBucket()
        {
          LicenseKey = str2,
          Restriction = str3,
          Signature = str4,
          Date = dateTime
        };
      }
      else if (ilogger.IsEnabled((LogEventLevel) 3))
      {
        string[] array = PXDatabase.SelectMulti<PX.SM.Licensing>((PXDataField) new PXDataField<PX.SM.Licensing.installationID>()).Select<PXDataRecord, string>((Func<PXDataRecord, string>) (r => r.GetString(0))).ToArray<string>();
        if (array.Length != 0)
          ilogger.ForContext("ExistingLicenses", (object) array, false).Warning<string, int>("License for installation {InstallationID} not found, but {TotalLicenses} other licenses exist in database", str1, array.Length);
        else if (ilogger.IsEnabled((LogEventLevel) 0))
          ilogger.ForContext("InstallationID", (object) str1, false).ForContext("TotalLicenses", (object) 0, false).Verbose("No licenses installed");
      }
    }
    return licenseBucket;
  }

  internal static void WriteLicense(
    this ILicensingManager licensingManager,
    LicenseBucket license,
    string extInstallationId = null)
  {
    ILogger logger = PXLicenseHelper.Logger.ForMethodContext(nameof (WriteLicense));
    string str = extInstallationId ?? licensingManager.InstallationId;
    using (new PXConnectionScope())
    {
      LicenseBucket licenseBucket = licensingManager.ReadLicense(extInstallationId);
      try
      {
        System.DateTime utcNow = System.DateTime.UtcNow;
        if (licenseBucket != null)
        {
          if (logger.IsEnabled((LogEventLevel) 1))
          {
            if (license.LicenseKey == licenseBucket.LicenseKey && license.Restriction == licenseBucket.Restriction && license.Signature == licenseBucket.Signature)
              logger.ForContext("LicenseDate", (object) licenseBucket.Date, false).Debug<string, string>("Updating license for {InstallationID}, license {LicenseChangeState}", str, "not changed");
            else
              logger.ForContext(licenseBucket).Debug<string, string>("Updating license for {InstallationID}, license {LicenseChangeState}", str, "changed");
          }
          PXDatabase.Update<PX.SM.Licensing>((PXDataFieldParam) new PXDataFieldAssign<PX.SM.Licensing.licensingKey>((object) license.LicenseKey), (PXDataFieldParam) new PXDataFieldAssign<PX.SM.Licensing.restriction>((object) license.Restriction), (PXDataFieldParam) new PXDataFieldAssign<PX.SM.Licensing.signature>((object) license.Signature), (PXDataFieldParam) new PXDataFieldAssign<PX.SM.Licensing.date>((object) utcNow), (PXDataFieldParam) new PXDataFieldAssign<PX.SM.Licensing.activity>((object) utcNow), (PXDataFieldParam) new PXDataFieldRestrict<PX.SM.Licensing.installationID>((object) str));
          logger.ForContext(license, new System.DateTime?(utcNow)).ForContext("LicenseActivity", (object) utcNow, false).Information<string>("Updated license for {InstallationID}", str);
        }
        else
        {
          PXDatabase.Insert<PX.SM.Licensing>((PXDataFieldAssign) new PXDataFieldAssign<PX.SM.Licensing.licensingKey>((object) license.LicenseKey), (PXDataFieldAssign) new PXDataFieldAssign<PX.SM.Licensing.restriction>((object) license.Restriction), (PXDataFieldAssign) new PXDataFieldAssign<PX.SM.Licensing.signature>((object) license.Signature), (PXDataFieldAssign) new PXDataFieldAssign<PX.SM.Licensing.date>((object) utcNow), (PXDataFieldAssign) new PXDataFieldAssign<PX.SM.Licensing.activity>((object) utcNow), (PXDataFieldAssign) new PXDataFieldAssign<PX.SM.Licensing.installationID>((object) str));
          logger.ForContext(license, new System.DateTime?(utcNow)).ForContext("LicenseActivity", (object) utcNow, false).Information<string>("Added license for {InstallationID}", str);
        }
      }
      catch (Exception ex)
      {
        logger.Error<string>(ex, "Exception while writing license for {InstallationID}", str);
        throw;
      }
    }
    licensingManager.InvalidateLicense();
    PXPageCacheUtils.InvalidateCachedPages();
    PXDatabase.ResetSlots();
  }

  internal static void DeleteLicense(this ILicensingManager licensingManager)
  {
    string installationId = licensingManager.InstallationId;
    licensingManager.RequestLicense(PXLicenseReason.DeleteLicense, false);
    using (new PXConnectionScope())
    {
      ILogger ilogger = PXLicenseHelper.Logger.ForMethodContext(nameof (DeleteLicense));
      try
      {
        PXDatabase.Delete<PX.SM.Licensing>((PXDataFieldRestrict) new PXDataFieldRestrict<PX.SM.Licensing.installationID>((object) installationId));
        ilogger.Information<string>("Deleted license for {InstallationID}", installationId);
        PXTrace.Logger.ForSystemEvents("License", "License_LicenseDeletedEventId").ForContext("ContextScreenId", (object) "SM201510", false).Information<string>("A license has been deleted {InstallationID}", installationId);
      }
      catch (Exception ex)
      {
        ilogger.Error<string>(ex, "Error while deleting license for {InstallationID}", installationId);
        throw;
      }
    }
    licensingManager.InvalidateLicense();
    PXPageCacheUtils.InvalidateCachedPages();
    PXDatabase.ResetSlots();
  }

  private static bool ValidateLicenseKey(string key)
  {
    string pattern = "^[0-F]{4}\\-?[0-F]{4}\\-?[0-F]{4}\\-?[0-F]{4}\\-?[0-F]{4}$";
    return !string.IsNullOrEmpty(key) && new Regex(pattern).IsMatch(key);
  }

  internal static long GetDbSizeQuota(this ILicensingManager licensingManager)
  {
    PXLicense license = licensingManager.GetLicense();
    return license.DataIncludedGb > 0 ? (long) license.DataIncludedGb * 1024L /*0x0400*/ * 1024L /*0x0400*/ * 1024L /*0x0400*/ : WebConfig.DbSizeQuota;
  }

  internal static LicenseDetail GetLicenseDetail(this ILicensingManager licensingManager)
  {
    LicenseBucket bucket = licensingManager.ReadLicense();
    PXLicense license = licensingManager.GetLicense(bucket);
    PXLicenseDefinition licenseDefinition = PXLicenseHelper.ParceLicense(bucket);
    LicenseDetail licenseDetail1 = new LicenseDetail();
    licenseDetail1.LicenseKey = license?.LicenseKey;
    licenseDetail1.LicenseType = license?.LicenseTypeCD;
    licenseDetail1.InstallationID = license?.InstallationID;
    PXLicenseState? state = license?.State;
    ref PXLicenseState? local = ref state;
    licenseDetail1.Status = local.HasValue ? local.GetValueOrDefault().ToString() : (string) null;
    licenseDetail1.ValidTo = license?.ValidTo;
    licenseDetail1.IssuedBy = license?.IssuedBy;
    licenseDetail1.IsPortal = license?.IsPortal;
    licenseDetail1.ResourceLevel = license?.ResourceLevel;
    LicenseRestriction[] licenseRestrictionArray1;
    if (licenseDefinition == null)
    {
      licenseRestrictionArray1 = (LicenseRestriction[]) null;
    }
    else
    {
      ReadOnlyCollection<LicenseRestriction> restrictions = licenseDefinition.Restrictions;
      licenseRestrictionArray1 = restrictions != null ? restrictions.ToArray<LicenseRestriction>() : (LicenseRestriction[]) null;
    }
    licenseDetail1.Restrictions = licenseRestrictionArray1;
    LicenseRestriction[] licenseRestrictionArray2;
    if (licenseDefinition == null)
    {
      licenseRestrictionArray2 = (LicenseRestriction[]) null;
    }
    else
    {
      ReadOnlyCollection<LicenseRestriction> resourceRestrictions = licenseDefinition.ResourceRestrictions;
      licenseRestrictionArray2 = resourceRestrictions != null ? resourceRestrictions.ToArray<LicenseRestriction>() : (LicenseRestriction[]) null;
    }
    licenseDetail1.ResourceRestrictions = licenseRestrictionArray2;
    LicenseRestriction[] licenseRestrictionArray3;
    if (licenseDefinition == null)
    {
      licenseRestrictionArray3 = (LicenseRestriction[]) null;
    }
    else
    {
      ReadOnlyCollection<LicenseRestriction> featureRestrictions = licenseDefinition.FeatureRestrictions;
      licenseRestrictionArray3 = featureRestrictions != null ? featureRestrictions.ToArray<LicenseRestriction>() : (LicenseRestriction[]) null;
    }
    licenseDetail1.FeatureRestrictions = licenseRestrictionArray3;
    LicenseRestriction[] licenseRestrictionArray4;
    if (licenseDefinition == null)
    {
      licenseRestrictionArray4 = (LicenseRestriction[]) null;
    }
    else
    {
      ReadOnlyCollection<LicenseRestriction> businessRestrictions = licenseDefinition.BusinessRestrictions;
      licenseRestrictionArray4 = businessRestrictions != null ? businessRestrictions.ToArray<LicenseRestriction>() : (LicenseRestriction[]) null;
    }
    licenseDetail1.BusinessRestrictions = licenseRestrictionArray4;
    LicenseDetail licenseDetail2 = licenseDetail1;
    List<LicenseFeatureInfo> licenseFeatureInfoList = new List<LicenseFeatureInfo>();
    foreach (PXResult<LicenseFeature> pxResult in ((LicensingSetup) PXGraph.CreateInstance(typeof (LicensingSetup))).Features.Select())
    {
      LicenseFeature licenseFeature = (LicenseFeature) pxResult;
      licenseFeatureInfoList.Add(new LicenseFeatureInfo(licenseFeature.Id, licenseFeature.Name, licenseFeature.Enabled.GetValueOrDefault()));
    }
    licenseDetail2.Features = licenseFeatureInfoList.ToArray();
    return licenseDetail2;
  }
}
