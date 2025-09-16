// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.WebServices.PXExchangeServer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.BulkInsert;
using PX.Common;
using PX.Data.Update.ExchangeService;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.WebServices;

public class PXExchangeServer
{
  private const string Rfc822ContentType = "message/rfc822";
  private const string EchangeUriCacheKey = "EchangeUriCache";
  private ExchangeServiceBinding _gate;
  public PXExchangeEventDelegate Logger;
  protected Dictionary<string, TimeZoneDefinitionType> timeZones = new Dictionary<string, TimeZoneDefinitionType>();
  protected Dictionary<string, string[]> groups = new Dictionary<string, string[]>();
  protected Dictionary<string, string> names = new Dictionary<string, string>();

  public int ProcessPackageSize { get; set; }

  public int SelectPackageSize { get; set; }

  public int UpdatePackageSize { get; set; }

  public int AttachmentSize { get; set; }

  [PXInternalUseOnly]
  public PXExchangeServer(ExchangeServiceBinding gate)
  {
    this._gate = gate;
    this.SelectPackageSize = 75;
    this.UpdatePackageSize = 25;
    this.ProcessPackageSize = 15;
    this.AttachmentSize = 20971520 /*0x01400000*/;
  }

  protected void EnsureTimezoneInitialised()
  {
    if (this._gate == null || this._gate.TimeZoneContext != null)
      return;
    this._gate.TimeZoneContext = new TimeZoneContextType()
    {
      TimeZoneDefinition = this.GetTimeZone()
    };
  }

  protected EmailAddressType PrepareMailbox(string mailbox)
  {
    if (string.IsNullOrEmpty(mailbox))
      return (EmailAddressType) null;
    return new EmailAddressType() { EmailAddress = mailbox };
  }

  protected BaseFolderIdType PrepareFolder(string mailbox, DistinguishedFolderIdNameType parent)
  {
    return (BaseFolderIdType) new DistinguishedFolderIdType()
    {
      Id = parent,
      Mailbox = this.PrepareMailbox(mailbox)
    };
  }

  public static PXExchangeServer GetGate(EMailSyncServer email, PXExchangeEventDelegate logger = null)
  {
    PXExchangeServer pxExchangeServer = ServiceLocator.Current.GetInstance<IPXExchangeServerProvider>().GetPXExchangeServer(email);
    pxExchangeServer.Logger += logger;
    return pxExchangeServer;
  }

  public static PXExchangeServer GetGate(
    string server,
    string login,
    string password,
    PXExchangeEventDelegate logger = null)
  {
    ExchangeServiceBinding gate1 = new ExchangeServiceBinding();
    gate1.Url = PXExchangeServer.ValidateUrl(server, login, password);
    gate1.CookieContainer = new CookieContainer();
    gate1.Credentials = (ICredentials) new NetworkCredential(login, password);
    gate1.RequestServerVersionValue = new RequestServerVersion()
    {
      Version = ExchangeVersionType.Exchange2013_SP1
    };
    gate1.Timeout = (int) new TimeSpan(0, 10, 0).TotalMilliseconds;
    PXExchangeServer gate2 = new PXExchangeServer(gate1);
    gate2.Logger += logger;
    return gate2;
  }

  public static string ValidateUrl(string server, string login, string password)
  {
    if (!string.IsNullOrEmpty(server))
      return server;
    if (string.IsNullOrEmpty(login))
      throw new PXException("Exchange Login is empty.");
    try
    {
      string md5String = PXCriptoHelper.CalculateMD5String(login + password);
      Dictionary<string, string> slot = PXDatabase.GetSlot<Dictionary<string, string>>("EchangeUriCache");
      lock (((ICollection) slot).SyncRoot)
      {
        if (slot.TryGetValue(md5String, out server))
          return server;
      }
      try
      {
        server = PXExchangeServer.AutoDicscover(login, password);
      }
      catch (Exception ex)
      {
        throw new PXException("An attempt to auto-discover the exchange URL by login failed. Please specify a correct exchange server URL.\r\n\r\n" + ex.Message);
      }
      if (string.IsNullOrEmpty(server))
        throw new PXException("An attempt to auto-discover the exchange URL by login failed. Please specify a correct exchange server URL.");
      lock (((ICollection) slot).SyncRoot)
        slot[md5String] = server;
    }
    catch (Exception ex)
    {
      throw new PXException("An exchange URL cannot be auto-discovered.\r\n\r\n" + ex.Message);
    }
    if (string.IsNullOrEmpty(login))
      throw new PXException("An exchange URL cannot be auto-discovered.");
    return server;
  }

  public static string AutoDicscover(string user, string password)
  {
    NetworkCredential networkCredential = new NetworkCredential(user, password);
    string s = $"<Autodiscover xmlns=\"http://schemas.microsoft.com/exchange/autodiscover/outlook/requestschema/2006\"><Request><EMailAddress>{user}</EMailAddress><AcceptableResponseSchema>http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a</AcceptableResponseSchema></Request></Autodiscover>";
    HttpWebRequest httpWebRequest1 = (HttpWebRequest) WebRequest.Create("https://autodiscover-s.outlook.com/autodiscover/autodiscover.xml");
    byte[] bytes = Encoding.UTF8.GetBytes(s);
    httpWebRequest1.ContentLength = (long) bytes.Length;
    httpWebRequest1.ContentType = "text/xml";
    httpWebRequest1.Headers.Add("Translate", "F");
    httpWebRequest1.Method = "POST";
    httpWebRequest1.Credentials = (ICredentials) networkCredential;
    Stream requestStream1 = httpWebRequest1.GetRequestStream();
    requestStream1.Write(bytes, 0, bytes.Length);
    requestStream1.Close();
    httpWebRequest1.AllowAutoRedirect = false;
    WebResponse response = httpWebRequest1.GetResponse();
    string requestUriString = response.Headers.Get("Location");
    if (requestUriString != null)
    {
      HttpWebRequest httpWebRequest2 = (HttpWebRequest) WebRequest.Create(requestUriString);
      httpWebRequest2.ContentLength = (long) bytes.Length;
      httpWebRequest2.ContentType = "text/xml";
      httpWebRequest2.Headers.Add("Translate", "F");
      httpWebRequest2.Method = "POST";
      httpWebRequest2.Credentials = (ICredentials) networkCredential;
      Stream requestStream2 = httpWebRequest2.GetRequestStream();
      requestStream2.Write(bytes, 0, bytes.Length);
      requestStream2.Close();
      response = httpWebRequest2.GetResponse();
    }
    Stream responseStream = response.GetResponseStream();
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.XmlResolver = (XmlResolver) null;
    xmlDocument.Load(responseStream);
    response.Dispose();
    foreach (XmlNode xmlNode in xmlDocument.GetElementsByTagName("Protocol"))
    {
      foreach (XmlNode childNode in xmlNode.ChildNodes)
      {
        if (childNode.Name == "EwsUrl")
          return childNode.InnerText;
      }
    }
    return (string) null;
  }

  public void TestAccount(string mailbox)
  {
    FindFolderType FindFolder1 = new FindFolderType();
    FindFolder1.Traversal = FolderQueryTraversalType.Shallow;
    FindFolder1.ParentFolderIds = new BaseFolderIdType[1]
    {
      (BaseFolderIdType) new DistinguishedFolderIdType()
      {
        Id = DistinguishedFolderIdNameType.inbox,
        Mailbox = this.PrepareMailbox(mailbox)
      }
    };
    FindFolder1.FolderShape = new FolderResponseShapeType()
    {
      BaseShape = DefaultShapeNamesType.IdOnly
    };
    foreach (ResponseMessageType responseMessageType in this._gate.FindFolder(FindFolder1).ResponseMessages.Items)
    {
      if (responseMessageType.ResponseClass != ResponseClassType.Success)
        throw new PXException("An error occurred during a simple test operation. Please check your email address, login, and password. ({0})", new object[1]
        {
          (object) PXExchangeServer.MapErrors(responseMessageType.MessageText)
        });
    }
  }

  public static string MapErrors(string errorText)
  {
    return PXExchangeServer.IsErrorTakesPlace(errorText, "Specified object has not been found in the store.") || PXExchangeServer.IsErrorTakesPlace(errorText, "Specified folder could not be found in the store.") ? "It is likely that the \"Send on behalf\" permission has not been assigned to the selected email account on the Exchange side." : errorText;
  }

  private static bool IsErrorTakesPlace(string errorText, string desiredText)
  {
    return errorText.Contains(desiredText);
  }

  public IEnumerable<Category> GetCategories(string mailbox = null)
  {
    GetUserConfigurationType GetUserConfiguration1 = new GetUserConfigurationType();
    UserConfigurationNameType configurationNameType = new UserConfigurationNameType();
    DistinguishedFolderIdType distinguishedFolderIdType = new DistinguishedFolderIdType();
    distinguishedFolderIdType.Id = DistinguishedFolderIdNameType.calendar;
    EmailAddressType emailAddressType;
    if (mailbox != null)
      emailAddressType = new EmailAddressType()
      {
        EmailAddress = mailbox
      };
    else
      emailAddressType = (EmailAddressType) null;
    distinguishedFolderIdType.Mailbox = emailAddressType;
    configurationNameType.Item = (BaseFolderIdType) distinguishedFolderIdType;
    configurationNameType.Name = "CategoryList";
    GetUserConfiguration1.UserConfigurationName = configurationNameType;
    GetUserConfiguration1.UserConfigurationProperties = UserConfigurationPropertyType.XmlData;
    ResponseMessageType[] items = this._gate.GetUserConfiguration(GetUserConfiguration1).ResponseMessages.Items;
    int index = 0;
    if (index >= items.Length)
      throw new PXException("An invalid response has been obtained from Exchange server.");
    ResponseMessageType responseMessageType = items[index];
    if (responseMessageType.ResponseClass != ResponseClassType.Success)
      throw new Exception(responseMessageType.MessageText);
    return (IEnumerable<Category>) ((MasterCategoryList) new XmlSerializer(typeof (MasterCategoryList)).Deserialize((TextReader) new StreamReader((Stream) new MemoryStream(((GetUserConfigurationResponseMessageType) responseMessageType).UserConfiguration.XmlData), Encoding.UTF8, true))).Categories;
  }

  public void EnsureCategory(
    string mailbox,
    string name,
    CategoryColor color = CategoryColor.None,
    CategoryKeyboardShortcut shortcut = CategoryKeyboardShortcut.None)
  {
    Category category = new Category(name, color, CategoryKeyboardShortcut.None);
    this.LogVerbose(mailbox, "The category {0} in mailbox {1} is being searched for.", (object) name, (object) mailbox);
    GetUserConfigurationType GetUserConfiguration1 = new GetUserConfigurationType();
    UserConfigurationNameType configurationNameType1 = new UserConfigurationNameType();
    DistinguishedFolderIdType distinguishedFolderIdType1 = new DistinguishedFolderIdType();
    distinguishedFolderIdType1.Id = DistinguishedFolderIdNameType.calendar;
    EmailAddressType emailAddressType1;
    if (mailbox != null)
      emailAddressType1 = new EmailAddressType()
      {
        EmailAddress = mailbox
      };
    else
      emailAddressType1 = (EmailAddressType) null;
    distinguishedFolderIdType1.Mailbox = emailAddressType1;
    configurationNameType1.Item = (BaseFolderIdType) distinguishedFolderIdType1;
    configurationNameType1.Name = "CategoryList";
    GetUserConfiguration1.UserConfigurationName = configurationNameType1;
    GetUserConfiguration1.UserConfigurationProperties = UserConfigurationPropertyType.XmlData;
    GetUserConfigurationResponseType userConfiguration = this._gate.GetUserConfiguration(GetUserConfiguration1);
    MasterCategoryList o = (MasterCategoryList) null;
    UserConfigurationType configurationType1 = (UserConfigurationType) null;
    foreach (ResponseMessageType responseMessageType in userConfiguration.ResponseMessages.Items)
    {
      if (responseMessageType.ResponseClass == ResponseClassType.Success)
      {
        configurationType1 = ((GetUserConfigurationResponseMessageType) responseMessageType).UserConfiguration;
        using (StreamReader streamReader = new StreamReader((Stream) new MemoryStream(configurationType1.XmlData), Encoding.UTF8, true))
          o = (MasterCategoryList) new XmlSerializer(typeof (MasterCategoryList)).Deserialize((TextReader) streamReader);
      }
      else if (responseMessageType.ResponseCode != ResponseCodeType.ErrorItemNotFound)
        throw new Exception(PXExchangeServer.MapErrors(responseMessageType.MessageText));
    }
    if (o == null)
      o = new MasterCategoryList()
      {
        Categories = new List<Category>()
      };
    o.Categories.RemoveAll((Predicate<Category>) (_ => _.Name == category.Name));
    for (int index = 0; index < 2; ++index)
    {
      bool flag = false;
      if (configurationType1 == null)
      {
        flag = true;
        UserConfigurationType configurationType2 = new UserConfigurationType();
        UserConfigurationNameType configurationNameType2 = new UserConfigurationNameType();
        DistinguishedFolderIdType distinguishedFolderIdType2 = new DistinguishedFolderIdType();
        distinguishedFolderIdType2.Id = DistinguishedFolderIdNameType.calendar;
        EmailAddressType emailAddressType2;
        if (mailbox != null)
          emailAddressType2 = new EmailAddressType()
          {
            EmailAddress = mailbox
          };
        else
          emailAddressType2 = (EmailAddressType) null;
        distinguishedFolderIdType2.Mailbox = emailAddressType2;
        configurationNameType2.Item = (BaseFolderIdType) distinguishedFolderIdType2;
        configurationNameType2.Name = "CategoryList";
        configurationType2.UserConfigurationName = configurationNameType2;
        configurationType1 = configurationType2;
      }
      using (MemoryStream memoryStream = new MemoryStream())
      {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof (MasterCategoryList));
        MemoryStream output = memoryStream;
        using (XmlWriter xmlWriter = XmlWriter.Create((Stream) output, new XmlWriterSettings()
        {
          Encoding = Encoding.UTF8
        }))
          xmlSerializer.Serialize(xmlWriter, (object) o);
        configurationType1.XmlData = memoryStream.ToArray();
      }
      BaseResponseMessageType responseMessageType1;
      if (flag)
      {
        this.LogInfo(mailbox, "The category '{0}' is not found in mailbox {1}. The category is being created.", (object) name, (object) mailbox);
        responseMessageType1 = (BaseResponseMessageType) this._gate.CreateUserConfiguration(new CreateUserConfigurationType()
        {
          UserConfiguration = configurationType1
        });
      }
      else
        responseMessageType1 = (BaseResponseMessageType) this._gate.UpdateUserConfiguration(new UpdateUserConfigurationType()
        {
          UserConfiguration = configurationType1
        });
      foreach (ResponseMessageType responseMessageType2 in responseMessageType1.ResponseMessages.Items)
      {
        if (responseMessageType2.ResponseClass != ResponseClassType.Success)
          throw new Exception(PXExchangeServer.MapErrors(responseMessageType2.MessageText));
      }
      o.Categories.Add(category);
    }
  }

  public IEnumerable<PXOperationResult<int>> GetUsersTimeZonesBiases(IEnumerable<string> addresses)
  {
    GetUserAvailabilityRequestType request = new GetUserAvailabilityRequestType()
    {
      TimeZone = new SerializableTimeZone() { Bias = 480 },
      MailboxDataArray = addresses.Select<string, MailboxData>((Func<string, MailboxData>) (address => new MailboxData()
      {
        AttendeeType = MeetingAttendeeType.Required,
        Email = new EmailAddress() { Address = address },
        ExcludeConflicts = false,
        ExcludeConflictsSpecified = false
      })).ToArray<MailboxData>(),
      FreeBusyViewOptions = new FreeBusyViewOptionsType()
      {
        TimeWindow = new Duration()
        {
          StartTime = System.DateTime.Now,
          EndTime = System.DateTime.Now.AddDays(1.0)
        },
        RequestedView = FreeBusyViewType.FreeBusy,
        RequestedViewSpecified = true
      }
    };
    GetUserAvailabilityResponseType response = this._gate.GetUserAvailability(request);
    for (int i = 0; i < response.FreeBusyResponseArray.Length; ++i)
    {
      FreeBusyResponseType freeBusyResponse = response.FreeBusyResponseArray[i];
      string address = request.MailboxDataArray[i].Email.Address;
      WorkingHours workingHours = freeBusyResponse.FreeBusyView.WorkingHours;
      Exception error = (Exception) null;
      try
      {
        if (freeBusyResponse.ResponseMessage.ResponseClass == ResponseClassType.Error)
          throw new PXException("Cannot get the user's availability status for the mailbox: {0}. The following error occurred during the synchronization of emails: '{1}'.", new object[2]
          {
            (object) address,
            (object) freeBusyResponse.ResponseMessage.MessageText
          });
        if (workingHours == null)
          throw new PXException(PXMessages.LocalizeFormatNoPrefix("It seems that '{0}' mailbox hasn't been initialized. You must initialize this mailbox on Outlook Web Access.", (object) address));
      }
      catch (Exception ex)
      {
        error = ex;
      }
      yield return error != null ? new PXOperationResult<int>(error) : new PXOperationResult<int>(workingHours.TimeZone.Bias);
    }
  }

  [Obsolete("Use GetUsersTimeZonesBiases instead.")]
  public IEnumerable<int> GetUsersTimeZones(IEnumerable<string> addresses)
  {
    return this.GetUsersTimeZonesBiases(addresses).Where<PXOperationResult<int>>((Func<PXOperationResult<int>, bool>) (a => a.Success)).Select<PXOperationResult<int>, int>((Func<PXOperationResult<int>, int>) (a => a.Result));
  }

  public TimeZoneDefinitionType GetTimeZone(string name = null)
  {
    if (this.timeZones == null || this.timeZones.Count <= 0)
    {
      this.LogVerbose((string) null, "Time zones from Exchange server are being evaluated.");
      this.timeZones = new Dictionary<string, TimeZoneDefinitionType>();
      foreach (ResponseMessageType responseMessageType in this._gate.GetServerTimeZones(new GetServerTimeZonesType()
      {
        ReturnFullTimeZoneData = true,
        ReturnFullTimeZoneDataSpecified = true
      }).ResponseMessages.Items)
      {
        if (responseMessageType.ResponseClass != ResponseClassType.Success)
          throw new PXException(responseMessageType.MessageText);
        foreach (TimeZoneDefinitionType zoneDefinitionType in ((GetServerTimeZonesResponseMessageType) responseMessageType).TimeZoneDefinitions.TimeZoneDefinition)
          this.timeZones[zoneDefinitionType.Id] = zoneDefinitionType;
      }
    }
    TimeZoneDefinitionType zoneDefinitionType1;
    return this.timeZones.TryGetValue(name ?? "UTC", out zoneDefinitionType1) ? zoneDefinitionType1 : (TimeZoneDefinitionType) null;
  }

  public string ResolveName(string entry)
  {
    string str = (string) null;
    if (this.names.TryGetValue(entry, out str))
      return str;
    ResolveNamesResponseType namesResponseType = this._gate.ResolveNames(new ResolveNamesType()
    {
      ReturnFullContactData = false,
      ContactDataShape = DefaultShapeNamesType.IdOnly,
      ContactDataShapeSpecified = true,
      SearchScope = ResolveNamesSearchScopeType.ActiveDirectory,
      UnresolvedEntry = entry
    });
    for (int index = 0; index < namesResponseType.ResponseMessages.Items.Length; ++index)
    {
      ResponseMessageType responseMessageType = namesResponseType.ResponseMessages.Items[index];
      if (responseMessageType.ResponseClass == ResponseClassType.Success)
      {
        foreach (ResolutionType resolutionType in (responseMessageType as ResolveNamesResponseMessageType).ResolutionSet.Resolution)
          this.names[entry] = str = resolutionType.Mailbox.EmailAddress;
      }
    }
    return str;
  }

  public string[] ExpandGroup(string group)
  {
    string[] strArray = (string[]) null;
    if (this.groups.TryGetValue(group, out strArray))
      return strArray;
    ExpandDLResponseType expandDlResponseType = this._gate.ExpandDL(new ExpandDLType()
    {
      Mailbox = new EmailAddressType()
      {
        EmailAddress = group
      }
    });
    for (int index = 0; index < expandDlResponseType.ResponseMessages.Items.Length; ++index)
    {
      ResponseMessageType responseMessageType1 = expandDlResponseType.ResponseMessages.Items[index];
      if (responseMessageType1.ResponseClass == ResponseClassType.Success)
      {
        ExpandDLResponseMessageType responseMessageType2 = responseMessageType1 as ExpandDLResponseMessageType;
        List<string> stringList = new List<string>();
        foreach (EmailAddressType emailAddressType in responseMessageType2.DLExpansion.Mailbox)
          stringList.Add(emailAddressType.EmailAddress);
        this.groups[group] = strArray = stringList.ToArray();
      }
    }
    return strArray;
  }

  public IEnumerable<BaseFolderType> FindPublicFolders(string mailbox, System.Type folderType = null)
  {
    return this.FindFolders(mailbox, DistinguishedFolderIdNameType.publicfoldersroot, folderType: folderType);
  }

  public IEnumerable<BaseFolderType> FindRootFolders(string mailbox, System.Type folderType = null)
  {
    return this.FindFolders(mailbox, DistinguishedFolderIdNameType.root, folderType: folderType);
  }

  public IEnumerable<BaseFolderType> FindFolders(
    string mailbox,
    DistinguishedFolderIdNameType folder,
    string filter = null,
    System.Type folderType = null)
  {
    return this.FindSubFolders(FolderQueryTraversalType.Shallow, DefaultShapeNamesType.AllProperties, filter, folderType, (BaseFolderIdType) new DistinguishedFolderIdType()
    {
      Id = folder,
      Mailbox = this.PrepareMailbox(mailbox)
    });
  }

  protected IEnumerable<BaseFolderType> FindSubFolders(
    FolderQueryTraversalType traversal,
    DefaultShapeNamesType shape,
    string filter,
    System.Type folderType,
    params BaseFolderIdType[] folders)
  {
    FindFolderType FindFolder1 = new FindFolderType()
    {
      Traversal = traversal,
      ParentFolderIds = ((IEnumerable<BaseFolderIdType>) folders).ToArray<BaseFolderIdType>(),
      FolderShape = new FolderResponseShapeType()
      {
        BaseShape = shape
      }
    };
    if (!string.IsNullOrEmpty(filter))
    {
      RestrictionType restrictionType1 = new RestrictionType();
      IsEqualToType isEqualToType = new IsEqualToType();
      isEqualToType.Item = (BasePathToElementType) new PathToUnindexedFieldType()
      {
        FieldURI = UnindexedFieldURIType.folderDisplayName
      };
      isEqualToType.FieldURIOrConstant = new FieldURIOrConstantType()
      {
        Item = (object) new ConstantValueType()
        {
          Value = filter
        }
      };
      restrictionType1.Item = (SearchExpressionType) isEqualToType;
      RestrictionType restrictionType2 = restrictionType1;
      FindFolder1.Restriction = restrictionType2;
    }
    ResponseMessageType[] responseMessageTypeArray = this._gate.FindFolder(FindFolder1).ResponseMessages.Items;
    for (int index1 = 0; index1 < responseMessageTypeArray.Length; ++index1)
    {
      ResponseMessageType responseMessageType1 = responseMessageTypeArray[index1];
      FindFolderResponseMessageType responseMessageType2 = responseMessageType1.ResponseClass == ResponseClassType.Success ? (FindFolderResponseMessageType) responseMessageType1 : throw new Exception(responseMessageType1.MessageText);
      if (responseMessageType2.RootFolder.TotalItemsInView > 0)
      {
        BaseFolderType[] baseFolderTypeArray = responseMessageType2.RootFolder.Folders;
        for (int index2 = 0; index2 < baseFolderTypeArray.Length; ++index2)
        {
          BaseFolderType subFolder = baseFolderTypeArray[index2];
          if (!(folderType != (System.Type) null) || folderType.IsAssignableFrom(subFolder.GetType()))
            yield return subFolder;
        }
        baseFolderTypeArray = (BaseFolderType[]) null;
      }
      yield return (BaseFolderType) null;
    }
    responseMessageTypeArray = (ResponseMessageType[]) null;
  }

  public PXOperationResult<PXExchangeFolderID> CreateFolder<T>(PXExchangeFolderDefinition definition) where T : BaseFolderType, new()
  {
    CreateFolderType CreateFolder1 = new CreateFolderType();
    CreateFolder1.ParentFolderId = new TargetFolderIdType()
    {
      Item = (BaseFolderIdType) new DistinguishedFolderIdType()
      {
        Id = definition.Parent,
        Mailbox = this.PrepareMailbox(definition.Address)
      }
    };
    CreateFolderType createFolderType = CreateFolder1;
    BaseFolderType[] baseFolderTypeArray = new BaseFolderType[1];
    T obj = new T();
    obj.DisplayName = definition.Name;
    baseFolderTypeArray[0] = (BaseFolderType) obj;
    createFolderType.Folders = baseFolderTypeArray;
    foreach (ResponseMessageType responseMessageType1 in this._gate.CreateFolder(CreateFolder1).ResponseMessages.Items)
    {
      if (responseMessageType1.ResponseClass != ResponseClassType.Success)
        return new PXOperationResult<PXExchangeFolderID>(new Exception(responseMessageType1.MessageText), definition.Tag);
      FolderInfoResponseMessageType responseMessageType2 = (FolderInfoResponseMessageType) responseMessageType1;
      if (((IEnumerable<BaseFolderType>) responseMessageType2.Folders).Any<BaseFolderType>())
      {
        BaseFolderType[] folders = responseMessageType2.Folders;
        int index = 0;
        if (index < folders.Length)
        {
          BaseFolderType baseFolderType = folders[index];
          return new PXOperationResult<PXExchangeFolderID>(new PXExchangeFolderID(definition.Address, (BaseFolderIdType) baseFolderType.FolderId), definition.Tag);
        }
      }
    }
    throw new PXException("Create folder error");
  }

  public IEnumerable<PXOperationResult<PXExchangeFolderID>> EnsureFolders<T>(
    params PXExchangeFolderDefinition[] folders)
    where T : BaseFolderType, new()
  {
    return folders == null || folders.Length == 0 ? (IEnumerable<PXOperationResult<PXExchangeFolderID>>) new PXOperationResult<PXExchangeFolderID>[0] : this.EnsureFolders<T>((IEnumerable<PXExchangeFolderDefinition>) folders);
  }

  public IEnumerable<PXOperationResult<PXExchangeFolderID>> EnsureFolders<T>(
    IEnumerable<PXExchangeFolderDefinition> data)
    where T : BaseFolderType, new()
  {
    PXExchangeServer pxExchangeServer1 = this;
    Dictionary<string, List<PXExchangeFolderDefinition>> buffer = new Dictionary<string, List<PXExchangeFolderDefinition>>();
    foreach (PXExchangeFolderDefinition folderDefinition in data)
    {
      if (string.IsNullOrEmpty(folderDefinition.Name))
        yield return new PXOperationResult<PXExchangeFolderID>(new PXExchangeFolderID(folderDefinition.Address, (BaseFolderIdType) new DistinguishedFolderIdType()
        {
          Id = folderDefinition.Parent,
          Mailbox = pxExchangeServer1.PrepareMailbox(folderDefinition.Address)
        }), folderDefinition.Tag);
      else if (!string.IsNullOrEmpty(folderDefinition.CachedID))
      {
        yield return new PXOperationResult<PXExchangeFolderID>(new PXExchangeFolderID(folderDefinition.Address, (BaseFolderIdType) new FolderIdType()
        {
          Id = folderDefinition.CachedID
        }), folderDefinition.Tag);
      }
      else
      {
        List<PXExchangeFolderDefinition> folderDefinitionList;
        if (!buffer.TryGetValue(folderDefinition.Name ?? string.Empty, out folderDefinitionList))
          buffer[folderDefinition.Name] = folderDefinitionList = new List<PXExchangeFolderDefinition>();
        folderDefinitionList.Add(folderDefinition);
      }
    }
    foreach (string key in buffer.Keys)
    {
      string name = key;
      List<PXExchangeFolderDefinition> folders = buffer[name];
      if (folders.Count > 0)
      {
        int procRows = pxExchangeServer1.ProcessPackageSize;
        pxExchangeServer1.LogVerbose((string) null, "IDs for the folder '{0}' are being evaluated. Searching is occurring in {1} mailboxes.", (object) name, (object) folders.Select<PXExchangeFolderDefinition, string>((Func<PXExchangeFolderDefinition, string>) (f => f.Address)).Distinct<string>().Count<string>());
        for (int i = 0; i < folders.Count; i += procRows)
        {
          PXExchangeServer pxExchangeServer = pxExchangeServer1;
          PXExchangeFolderDefinition[] batch = folders.Skip<PXExchangeFolderDefinition>(i).Take<PXExchangeFolderDefinition>(procRows).ToArray<PXExchangeFolderDefinition>();
          FindFolderType request = new FindFolderType();
          request.Traversal = FolderQueryTraversalType.Shallow;
          request.FolderShape = new FolderResponseShapeType()
          {
            BaseShape = DefaultShapeNamesType.IdOnly
          };
          FindFolderType findFolderType = request;
          RestrictionType restrictionType = new RestrictionType();
          IsEqualToType isEqualToType = new IsEqualToType();
          isEqualToType.Item = (BasePathToElementType) new PathToUnindexedFieldType()
          {
            FieldURI = UnindexedFieldURIType.folderDisplayName
          };
          isEqualToType.FieldURIOrConstant = new FieldURIOrConstantType()
          {
            Item = (object) new ConstantValueType()
            {
              Value = name
            }
          };
          restrictionType.Item = (SearchExpressionType) isEqualToType;
          findFolderType.Restriction = restrictionType;
          // ISSUE: reference to a compiler-generated method
          request.ParentFolderIds = ((IEnumerable<PXExchangeFolderDefinition>) batch).Select<PXExchangeFolderDefinition, BaseFolderIdType>(new Func<PXExchangeFolderDefinition, BaseFolderIdType>(pxExchangeServer1.\u003CEnsureFolders\u003Eb__47_1<T>)).ToArray<BaseFolderIdType>();
          FindFolderResponseType response = (FindFolderResponseType) null;
          RetryableOperation.DoOperation((System.Action) (() => response = pxExchangeServer._gate.FindFolder(request)), 3, (Action<int, Exception>) null);
          for (int j = 0; j < response.ResponseMessages.Items.Length; ++j)
          {
            PXExchangeFolderDefinition definition = batch[j];
            ResponseMessageType responseMessageType1 = response.ResponseMessages.Items[j];
            if (responseMessageType1.ResponseClass == ResponseClassType.Success)
            {
              FindFolderResponseMessageType responseMessageType2 = (FindFolderResponseMessageType) responseMessageType1;
              if (responseMessageType2.RootFolder.TotalItemsInView > 0)
              {
                BaseFolderType[] folders1 = responseMessageType2.RootFolder.Folders;
                int index = 0;
                if (index < folders1.Length)
                {
                  BaseFolderType baseFolderType = folders1[index];
                  yield return new PXOperationResult<PXExchangeFolderID>(new PXExchangeFolderID(definition.Address, (BaseFolderIdType) baseFolderType.FolderId, true), definition.Tag);
                }
              }
              else
              {
                pxExchangeServer1.LogInfo(definition.Address, "The folder '{0}' is not found in mailbox {1}. The system is creating the folder.", (object) name, (object) definition.Address);
                yield return pxExchangeServer1.CreateFolder<T>(definition);
              }
            }
            else
              yield return new PXOperationResult<PXExchangeFolderID>(new Exception(responseMessageType1.MessageText), definition.Tag);
          }
          batch = (PXExchangeFolderDefinition[]) null;
        }
        folders = (List<PXExchangeFolderDefinition>) null;
        name = (string) null;
      }
    }
  }

  public IEnumerable<PXExchangeItem<T>> FindItems<T>(
    PXExchangeFolderID[] folders,
    PXExchangeFindOptions options,
    string category = null,
    System.DateTime? date = null,
    BasePathToElementType[] extFields = null,
    HashSet<string> skip = null)
    where T : ItemType, new()
  {
    PXExchangeServer pxExchangeServer = this;
    pxExchangeServer.EnsureTimezoneInitialised();
    pxExchangeServer.LogVerbose((string) null, "The system is searching for items. (Category: {0}, Date: {1}, Options: {2}).", (object) (category ?? "Note"), (object) (date ?? new System.DateTime(1901, 1, 1)), (object) options);
    bool detailed = (options & PXExchangeFindOptions.IncludeDetails) == PXExchangeFindOptions.IncludeDetails;
    bool getAttachments = (options & PXExchangeFindOptions.IncludeAttachments) == PXExchangeFindOptions.IncludeAttachments;
    bool getHtml = (options & PXExchangeFindOptions.PlainText) != PXExchangeFindOptions.PlainText;
    bool flag1 = (options & PXExchangeFindOptions.IncludePrivate) != PXExchangeFindOptions.IncludePrivate;
    bool flag2 = (options & PXExchangeFindOptions.IncludeDraft) != PXExchangeFindOptions.IncludeDraft;
    bool headersOnly = (options & PXExchangeFindOptions.HeadersOnly) == PXExchangeFindOptions.HeadersOnly;
    if (extFields != null && extFields.Length != 0)
      detailed = false;
    FindItemType request = new FindItemType();
    request.ItemShape = new ItemResponseShapeType()
    {
      BaseShape = detailed ? DefaultShapeNamesType.AllProperties : DefaultShapeNamesType.IdOnly
    };
    request.ItemShape.AdditionalProperties = new BasePathToElementType[2]
    {
      (BasePathToElementType) new PathToUnindexedFieldType()
      {
        FieldURI = UnindexedFieldURIType.itemDateTimeReceived
      },
      (BasePathToElementType) new PathToUnindexedFieldType()
      {
        FieldURI = UnindexedFieldURIType.itemLastModifiedTime
      }
    };
    request.Traversal = ItemQueryTraversalType.Shallow;
    FindItemType findItemType1 = request;
    IndexedPageViewType indexedPageViewType = new IndexedPageViewType();
    indexedPageViewType.BasePoint = IndexBasePointType.Beginning;
    indexedPageViewType.Offset = 0;
    indexedPageViewType.MaxEntriesReturned = pxExchangeServer.SelectPackageSize;
    indexedPageViewType.MaxEntriesReturnedSpecified = true;
    findItemType1.Item = (BasePagingType) indexedPageViewType;
    request.SortOrder = new FieldOrderType[1]
    {
      new FieldOrderType()
      {
        Order = SortDirectionType.Ascending,
        Item = (BasePathToElementType) new PathToUnindexedFieldType()
        {
          FieldURI = UnindexedFieldURIType.itemDateTimeReceived
        }
      }
    };
    SearchExpressionType searchExpressionType1 = (SearchExpressionType) null;
    if (!string.IsNullOrEmpty(category))
    {
      IsEqualToType isEqualToType = new IsEqualToType();
      isEqualToType.Item = (BasePathToElementType) new PathToUnindexedFieldType()
      {
        FieldURI = UnindexedFieldURIType.itemCategories
      };
      isEqualToType.FieldURIOrConstant = new FieldURIOrConstantType()
      {
        Item = (object) new ConstantValueType()
        {
          Value = category
        }
      };
      searchExpressionType1 = (SearchExpressionType) isEqualToType;
    }
    SearchExpressionType searchExpressionType2 = (SearchExpressionType) null;
    if (!flag1)
    {
      IsEqualToType isEqualToType = new IsEqualToType();
      isEqualToType.Item = (BasePathToElementType) new PathToUnindexedFieldType()
      {
        FieldURI = UnindexedFieldURIType.itemSensitivity
      };
      isEqualToType.FieldURIOrConstant = new FieldURIOrConstantType()
      {
        Item = (object) new ConstantValueType()
        {
          Value = SensitivityChoicesType.Normal.ToString()
        }
      };
      searchExpressionType2 = (SearchExpressionType) isEqualToType;
    }
    SearchExpressionType searchExpressionType3 = (SearchExpressionType) null;
    if (!flag2)
    {
      IsEqualToType isEqualToType = new IsEqualToType();
      isEqualToType.Item = (BasePathToElementType) new PathToUnindexedFieldType()
      {
        FieldURI = UnindexedFieldURIType.itemIsDraft
      };
      isEqualToType.FieldURIOrConstant = new FieldURIOrConstantType()
      {
        Item = (object) new ConstantValueType()
        {
          Value = bool.FalseString
        }
      };
      searchExpressionType3 = (SearchExpressionType) isEqualToType;
    }
    SearchExpressionType searchExpressionType4 = (SearchExpressionType) null;
    if (date.HasValue)
    {
      bool flag3 = (options & PXExchangeFindOptions.Created) == PXExchangeFindOptions.Created;
      int num = (options & PXExchangeFindOptions.Modified) == PXExchangeFindOptions.Modified ? 1 : 0;
      IsGreaterThanOrEqualToType thanOrEqualToType1 = new IsGreaterThanOrEqualToType();
      thanOrEqualToType1.Item = (BasePathToElementType) new PathToUnindexedFieldType()
      {
        FieldURI = UnindexedFieldURIType.itemLastModifiedTime
      };
      thanOrEqualToType1.FieldURIOrConstant = new FieldURIOrConstantType()
      {
        Item = (object) new ConstantValueType()
        {
          Value = date.Value.ToString("yyyy-MM-ddTHH:mm:ssZ")
        }
      };
      IsGreaterThanOrEqualToType thanOrEqualToType2 = thanOrEqualToType1;
      IsGreaterThanOrEqualToType thanOrEqualToType3 = new IsGreaterThanOrEqualToType();
      thanOrEqualToType3.Item = (BasePathToElementType) new PathToUnindexedFieldType()
      {
        FieldURI = UnindexedFieldURIType.itemDateTimeCreated
      };
      thanOrEqualToType3.FieldURIOrConstant = new FieldURIOrConstantType()
      {
        Item = (object) new ConstantValueType()
        {
          Value = date.Value.ToString("yyyy-MM-ddTHH:mm:ssZ")
        }
      };
      IsGreaterThanOrEqualToType thanOrEqualToType4 = thanOrEqualToType3;
      IsLessThanOrEqualToType thanOrEqualToType5 = new IsLessThanOrEqualToType();
      thanOrEqualToType5.Item = (BasePathToElementType) new PathToUnindexedFieldType()
      {
        FieldURI = UnindexedFieldURIType.itemDateTimeCreated
      };
      thanOrEqualToType5.FieldURIOrConstant = new FieldURIOrConstantType()
      {
        Item = (object) new ConstantValueType()
        {
          Value = date.Value.ToString("yyyy-MM-ddTHH:mm:ssZ")
        }
      };
      IsLessThanOrEqualToType thanOrEqualToType6 = thanOrEqualToType5;
      if (num != 0)
        searchExpressionType4 = (SearchExpressionType) thanOrEqualToType2;
      else if (flag3)
      {
        OrType orType1 = new OrType();
        OrType orType2 = orType1;
        SearchExpressionType[] searchExpressionTypeArray = new SearchExpressionType[2]
        {
          (SearchExpressionType) thanOrEqualToType4,
          null
        };
        AndType andType = new AndType();
        andType.Items = new SearchExpressionType[2]
        {
          (SearchExpressionType) thanOrEqualToType2,
          (SearchExpressionType) thanOrEqualToType6
        };
        searchExpressionTypeArray[1] = (SearchExpressionType) andType;
        orType2.Items = searchExpressionTypeArray;
        searchExpressionType4 = (SearchExpressionType) orType1;
      }
    }
    List<SearchExpressionType> searchExpressionTypeList = new List<SearchExpressionType>();
    if (searchExpressionType4 != null)
      searchExpressionTypeList.Add(searchExpressionType4);
    if (searchExpressionType1 != null)
      searchExpressionTypeList.Add(searchExpressionType1);
    if (searchExpressionType2 != null)
      searchExpressionTypeList.Add(searchExpressionType2);
    if (searchExpressionType3 != null)
      searchExpressionTypeList.Add(searchExpressionType3);
    if (searchExpressionTypeList.Count > 0)
    {
      if (searchExpressionTypeList.Count == 1)
      {
        request.Restriction = new RestrictionType()
        {
          Item = searchExpressionTypeList[0]
        };
      }
      else
      {
        FindItemType findItemType2 = request;
        RestrictionType restrictionType = new RestrictionType();
        AndType andType = new AndType();
        andType.Items = searchExpressionTypeList.ToArray();
        restrictionType.Item = (SearchExpressionType) andType;
        findItemType2.Restriction = restrictionType;
      }
    }
    int procRows = pxExchangeServer.ProcessPackageSize;
    for (int i = 0; i < folders.Length; i += procRows)
    {
      PXExchangeFolderID[] batch = ((IEnumerable<PXExchangeFolderID>) folders).Skip<PXExchangeFolderID>(i).Take<PXExchangeFolderID>(procRows).ToArray<PXExchangeFolderID>();
      request.ParentFolderIds = ((IEnumerable<PXExchangeFolderID>) batch).Select<PXExchangeFolderID, BaseFolderIdType>((Func<PXExchangeFolderID, BaseFolderIdType>) (f => f.FolderID)).ToArray<BaseFolderIdType>();
      int position = 0;
      int maxBatch = pxExchangeServer.SelectPackageSize * (detailed ? 1 : 2);
      int maxRows = (int) System.Math.Ceiling((double) maxBatch / (double) batch.Length);
      if (maxRows < procRows)
        maxRows = procRows;
      bool more = true;
      bool found = true;
      while (more & found)
      {
        request.Item.MaxEntriesReturned = maxRows;
        ((IndexedPageViewType) request.Item).Offset = position;
        FindItemResponseType response = (FindItemResponseType) null;
        RetryableOperation.DoOperation((System.Action) (() => response = this._gate.FindItem(request)), 5, (Action<int, Exception>) ((atempt, ex) =>
        {
          if (atempt < 3 || ex == null || !ex.Message.ToLower().Contains("try again later"))
            return;
          maxRows /= 2;
          if (maxRows < 2)
            maxRows = 2;
          Thread.Sleep(10000 * atempt);
        }));
        more = false;
        found = false;
        int index1 = 0;
        int counter = 0;
        List<PXExchangeItemID> needDetails = new List<PXExchangeItemID>();
        ResponseMessageType[] responseMessageTypeArray = response.ResponseMessages.Items;
        for (int index2 = 0; index2 < responseMessageTypeArray.Length; ++index2)
        {
          ResponseMessageType responseMessageType1 = responseMessageTypeArray[index2];
          FindItemResponseMessageType responseMessageType2 = responseMessageType1.ResponseClass == ResponseClassType.Success ? (FindItemResponseMessageType) responseMessageType1 : throw new PXExchangeSyncFatalException(batch[index1].Address, responseMessageType1.MessageText);
          if (responseMessageType2.RootFolder.TotalItemsInView > 0)
          {
            if (!(responseMessageType2.RootFolder.Item is ArrayOfRealItemsType arrayOfRealItemsType) || arrayOfRealItemsType.Items == null)
            {
              ++index1;
              continue;
            }
            PXExchangeFolderID folder = batch[index1];
            if (responseMessageType2.RootFolder.TotalItemsInView > position + maxRows)
              more = true;
            ItemType[] itemTypeArray = arrayOfRealItemsType.Items;
            for (int index3 = 0; index3 < itemTypeArray.Length; ++index3)
            {
              ItemType itemType = itemTypeArray[index3];
              ++counter;
              found = true;
              if (typeof (T).IsAssignableFrom(itemType.GetType()) && (skip == null || !skip.Contains(itemType.ItemId.Id)))
              {
                if (!detailed && !headersOnly)
                  needDetails.Add(new PXExchangeItemID(folder.Address, itemType.ItemId, new System.DateTime?(itemType.DateTimeReceived)));
                else
                  yield return new PXExchangeItem<T>(folder.Address, (T) itemType);
              }
            }
            itemTypeArray = (ItemType[]) null;
            folder = (PXExchangeFolderID) null;
          }
          ++index1;
        }
        responseMessageTypeArray = (ResponseMessageType[]) null;
        if (needDetails.Count > 0)
        {
          needDetails.Sort((Comparison<PXExchangeItemID>) ((x, y) =>
          {
            if (!x.Date.HasValue && !y.Date.HasValue)
              return 0;
            if (!x.Date.HasValue)
              return -1;
            return !y.Date.HasValue ? 1 : x.Date.Value.CompareTo(y.Date.Value);
          }));
          foreach (PXExchangeItem<T> pxExchangeItem in pxExchangeServer.GetItems<T>(getHtml, getAttachments, extFields, needDetails.ToArray()))
            yield return pxExchangeItem;
        }
        position += maxRows;
        if (counter < maxBatch)
        {
          int num = (int) System.Math.Ceiling((double) (maxBatch - counter) / (double) batch.Length);
          if (num > 1)
            maxRows += num;
        }
        needDetails = (List<PXExchangeItemID>) null;
      }
      batch = (PXExchangeFolderID[]) null;
    }
  }

  public IEnumerable<PXExchangeItem<T>> GetItems<T>(
    bool getHtml,
    bool getAttachments,
    BasePathToElementType[] extFields,
    params PXExchangeItemID[] items)
    where T : ItemType, new()
  {
    PXExchangeServer pxExchangeServer1 = this;
    if (items.Length != 0)
    {
      pxExchangeServer1.EnsureTimezoneInitialised();
      int maxRows = pxExchangeServer1.UpdatePackageSize * 2;
      int counter = 0;
      for (int i = 0; i < items.Length; i += maxRows)
      {
        PXExchangeServer pxExchangeServer = pxExchangeServer1;
        GetItemType request = new GetItemType();
        request.ItemIds = (BaseItemIdType[]) ((IEnumerable<PXExchangeItemID>) items).Skip<PXExchangeItemID>(i).Take<PXExchangeItemID>(maxRows).Select<PXExchangeItemID, ItemIdType>((Func<PXExchangeItemID, ItemIdType>) (id => id.ItemID)).ToArray<ItemIdType>();
        request.ItemShape = new ItemResponseShapeType()
        {
          AdditionalProperties = extFields,
          BaseShape = DefaultShapeNamesType.AllProperties,
          BodyType = getHtml ? BodyTypeResponseType.HTML : BodyTypeResponseType.Text,
          BodyTypeSpecified = true
        };
        if (request.ItemIds.Length != 0)
        {
          GetItemResponseType response = (GetItemResponseType) null;
          RetryableOperation.DoOperation((System.Action) (() => response = pxExchangeServer._gate.GetItem(request)), 2, (Action<int, Exception>) null);
          ResponseMessageType[] responseMessageTypeArray = response.ResponseMessages.Items;
          for (int index1 = 0; index1 < responseMessageTypeArray.Length; ++index1)
          {
            ResponseMessageType responseMessageType = responseMessageTypeArray[index1];
            if (responseMessageType.ResponseClass == ResponseClassType.Success)
            {
              ItemType[] itemTypeArray = ((ItemInfoResponseMessageType) responseMessageType).Items.Items;
              for (int index2 = 0; index2 < itemTypeArray.Length; ++index2)
              {
                ItemType itemType = itemTypeArray[index2];
                yield return new PXExchangeItem<T>(items[counter].Address, (T) itemType, getAttachments ? pxExchangeServer1.GetAttachments(items[counter].Address, itemType) : (AttachmentType[]) null);
              }
              itemTypeArray = (ItemType[]) null;
              ++counter;
            }
            else if (responseMessageType.ResponseCode != ResponseCodeType.ErrorItemNotFound)
            {
              IEnumerable<string> values = responseMessageType.MessageXml != null ? ((IEnumerable<XmlElement>) responseMessageType.MessageXml.Any).Select<XmlElement, string>((Func<XmlElement, string>) (x => x.OuterXml)) : (IEnumerable<string>) null;
              throw new PXException(values == null ? responseMessageType.MessageText : responseMessageType.MessageText + Environment.NewLine + string.Join(Environment.NewLine, values));
            }
          }
          responseMessageTypeArray = (ResponseMessageType[]) null;
        }
      }
    }
  }

  public IEnumerable<PXExchangeResponce<T>> CreateItems<T>(
    params PXExchangeRequest<T, ItemType>[] data)
    where T : ItemType, new()
  {
    return data == null || data.Length == 0 ? (IEnumerable<PXExchangeResponce<T>>) new PXExchangeResponce<T>[0] : this.CreateItems<T>((IEnumerable<PXExchangeRequest<T, ItemType>>) data);
  }

  public IEnumerable<PXExchangeResponce<T>> CreateItems<T>(
    IEnumerable<PXExchangeRequest<T, ItemType>> data)
    where T : ItemType, new()
  {
    if (data != null)
    {
      this.EnsureTimezoneInitialised();
      int maxRows = this.UpdatePackageSize;
      foreach (Tuple<string, BaseFolderIdType, PXExchangeRequest<T, ItemType>[]> mailbox in this.BatchItems<T, ItemType>(maxRows, data))
      {
        PXExchangeRequest<T, ItemType>[] items = mailbox.Item3;
        for (int i = 0; i < items.Length; i += maxRows)
        {
          PXExchangeRequest<T, ItemType>[] batch = ((IEnumerable<PXExchangeRequest<T, ItemType>>) items).Skip<PXExchangeRequest<T, ItemType>>(i).Take<PXExchangeRequest<T, ItemType>>(maxRows).ToArray<PXExchangeRequest<T, ItemType>>();
          bool flag = ((IEnumerable<PXExchangeRequest<T, ItemType>>) batch).Any<PXExchangeRequest<T, ItemType>>((Func<PXExchangeRequest<T, ItemType>, bool>) (b => b.SendRequired));
          bool sendSeparateRequired = ((IEnumerable<PXExchangeRequest<T, ItemType>>) batch).Any<PXExchangeRequest<T, ItemType>>((Func<PXExchangeRequest<T, ItemType>, bool>) (b => b.SendSeparateRequired));
          bool detailsRequired = ((IEnumerable<PXExchangeRequest<T, ItemType>>) batch).Any<PXExchangeRequest<T, ItemType>>((Func<PXExchangeRequest<T, ItemType>, bool>) (b => b.DetailsRequired));
          if (sendSeparateRequired)
            flag = false;
          CreateItemType CreateItem1 = new CreateItemType();
          CreateItem1.Items = new NonEmptyArrayOfAllItemsType()
          {
            Items = ((IEnumerable<PXExchangeRequest<T, ItemType>>) batch).Select<PXExchangeRequest<T, ItemType>, ItemType>((Func<PXExchangeRequest<T, ItemType>, ItemType>) (b => b.Request)).ToArray<ItemType>()
          };
          CreateItem1.SendMeetingInvitations = flag ? CalendarItemCreateOrDeleteOperationType.SendToAllAndSaveCopy : CalendarItemCreateOrDeleteOperationType.SendToNone;
          CreateItem1.SendMeetingInvitationsSpecified = true;
          CreateItem1.MessageDisposition = flag ? MessageDispositionType.SendAndSaveCopy : MessageDispositionType.SaveOnly;
          CreateItem1.MessageDispositionSpecified = true;
          if (sendSeparateRequired)
            CreateItem1.SavedItemFolderId = new TargetFolderIdType()
            {
              Item = (BaseFolderIdType) new DistinguishedFolderIdType()
              {
                Id = DistinguishedFolderIdNameType.drafts,
                Mailbox = this.PrepareMailbox(mailbox.Item1)
              }
            };
          else
            CreateItem1.SavedItemFolderId = new TargetFolderIdType()
            {
              Item = mailbox.Item2
            };
          if (CreateItem1.Items.Items.Length != 0)
          {
            CreateItemResponseType response = (CreateItemResponseType) null;
            response = this._gate.CreateItem(CreateItem1);
            List<PXExchangeRequest<T, ItemType>> toSendSeparately = new List<PXExchangeRequest<T, ItemType>>();
            List<Tuple<PXExchangeItemID, PXExchangeReBase<T>>> needDetails = new List<Tuple<PXExchangeItemID, PXExchangeReBase<T>>>();
            for (int j = 0; j < response.ResponseMessages.Items.Length; ++j)
            {
              PXExchangeRequest<T, ItemType> current = batch[j];
              ResponseMessageType responseMessageType = response.ResponseMessages.Items[j];
              if (responseMessageType.ResponseClass == ResponseClassType.Success)
              {
                ItemType[] itemTypeArray = ((ItemInfoResponseMessageType) responseMessageType).Items.Items;
                for (int index = 0; index < itemTypeArray.Length; ++index)
                {
                  ItemType item = itemTypeArray[index];
                  PXOperationResult<ItemIdType> attachment = this.CreateAttachment(mailbox.Item1, item, batch[j].Attachments, false);
                  if (attachment != null)
                  {
                    if (attachment.Success)
                      item.ItemId = attachment.Result;
                    else
                      yield return new PXExchangeResponce<T>((PXExchangeReBase<T>) current, item as T, error: attachment.Error.Message);
                  }
                  if (sendSeparateRequired)
                    toSendSeparately.Add(new PXExchangeRequest<T, ItemType>(current.Folder, item, current.UID, current.Tag));
                  else if (detailsRequired)
                    needDetails.Add(Tuple.Create<PXExchangeItemID, PXExchangeReBase<T>>(new PXExchangeItemID(current.Folder.Address, item.ItemId), (PXExchangeReBase<T>) current));
                  else
                    yield return new PXExchangeResponce<T>((PXExchangeReBase<T>) current, item as T);
                  item = (ItemType) null;
                }
                itemTypeArray = (ItemType[]) null;
              }
              else
              {
                IEnumerable<string> messages = responseMessageType.MessageXml != null ? ((IEnumerable<XmlElement>) responseMessageType.MessageXml.Any).Select<XmlElement, string>((Func<XmlElement, string>) (x => x.OuterXml)) : (IEnumerable<string>) null;
                yield return new PXExchangeResponce<T>((PXExchangeReBase<T>) current, code: responseMessageType.ResponseCode, error: responseMessageType.MessageText, messages: messages);
              }
              current = (PXExchangeRequest<T, ItemType>) null;
            }
            if (toSendSeparately.Count > 0)
            {
              foreach (PXExchangeResponce<T> sendItem in this.SendItems<T>(toSendSeparately.ToArray()))
              {
                if (detailsRequired && sendItem.Success)
                  needDetails.Add(Tuple.Create<PXExchangeItemID, PXExchangeReBase<T>>(new PXExchangeItemID(sendItem.Address, sendItem.Item.ItemId), (PXExchangeReBase<T>) sendItem));
                else
                  yield return sendItem;
              }
            }
            if (needDetails.Count > 0)
            {
              foreach (PXExchangeItem<T> pxExchangeItem in this.GetItems<T>(true, false, (BasePathToElementType[]) null, needDetails.Select<Tuple<PXExchangeItemID, PXExchangeReBase<T>>, PXExchangeItemID>((Func<Tuple<PXExchangeItemID, PXExchangeReBase<T>>, PXExchangeItemID>) (d => d.Item1)).ToArray<PXExchangeItemID>()))
              {
                PXExchangeItem<T> item = pxExchangeItem;
                yield return new PXExchangeResponce<T>(needDetails.First<Tuple<PXExchangeItemID, PXExchangeReBase<T>>>((Func<Tuple<PXExchangeItemID, PXExchangeReBase<T>>, bool>) (r => r.Item1.ItemID.Id == item.Item.ItemId.Id)).Item2, item as T);
              }
            }
            batch = (PXExchangeRequest<T, ItemType>[]) null;
            response = (CreateItemResponseType) null;
            toSendSeparately = (List<PXExchangeRequest<T, ItemType>>) null;
            needDetails = (List<Tuple<PXExchangeItemID, PXExchangeReBase<T>>>) null;
          }
        }
        items = (PXExchangeRequest<T, ItemType>[]) null;
      }
    }
  }

  public IEnumerable<PXExchangeResponce<T>> UpdateItems<T>(
    params PXExchangeRequest<T, ItemChangeType>[] data)
    where T : ItemType, new()
  {
    return data == null || data.Length == 0 ? (IEnumerable<PXExchangeResponce<T>>) new PXExchangeResponce<T>[0] : this.UpdateItems<T>((IEnumerable<PXExchangeRequest<T, ItemChangeType>>) data);
  }

  public IEnumerable<PXExchangeResponce<T>> UpdateItems<T>(
    IEnumerable<PXExchangeRequest<T, ItemChangeType>> data)
    where T : ItemType, new()
  {
    PXExchangeServer pxExchangeServer = this;
    if (data != null)
    {
      pxExchangeServer.EnsureTimezoneInitialised();
      int maxRows = pxExchangeServer.UpdatePackageSize;
      foreach (Tuple<string, BaseFolderIdType, PXExchangeRequest<T, ItemChangeType>[]> mailbox in pxExchangeServer.BatchItems<T, ItemChangeType>(maxRows, data))
      {
        PXExchangeRequest<T, ItemChangeType>[] items = mailbox.Item3;
        for (int i = 0; i < items.Length; i += maxRows)
        {
          PXExchangeRequest<T, ItemChangeType>[] batch = ((IEnumerable<PXExchangeRequest<T, ItemChangeType>>) items).Skip<PXExchangeRequest<T, ItemChangeType>>(i).Take<PXExchangeRequest<T, ItemChangeType>>(maxRows).ToArray<PXExchangeRequest<T, ItemChangeType>>();
          bool flag = ((IEnumerable<PXExchangeRequest<T, ItemChangeType>>) batch).Any<PXExchangeRequest<T, ItemChangeType>>((Func<PXExchangeRequest<T, ItemChangeType>, bool>) (b => b.SendRequired));
          bool sendSeparateRequired = ((IEnumerable<PXExchangeRequest<T, ItemChangeType>>) batch).Any<PXExchangeRequest<T, ItemChangeType>>((Func<PXExchangeRequest<T, ItemChangeType>, bool>) (b => b.SendSeparateRequired));
          bool detailsRequired = ((IEnumerable<PXExchangeRequest<T, ItemChangeType>>) batch).Any<PXExchangeRequest<T, ItemChangeType>>((Func<PXExchangeRequest<T, ItemChangeType>, bool>) (b => b.DetailsRequired));
          if (sendSeparateRequired)
            flag = false;
          UpdateItemType UpdateItem1 = new UpdateItemType();
          UpdateItem1.ConflictResolution = ConflictResolutionType.AlwaysOverwrite;
          UpdateItem1.ItemChanges = ((IEnumerable<PXExchangeRequest<T, ItemChangeType>>) batch).Select<PXExchangeRequest<T, ItemChangeType>, ItemChangeType>((Func<PXExchangeRequest<T, ItemChangeType>, ItemChangeType>) (b => b.Request)).ToArray<ItemChangeType>();
          UpdateItem1.SendMeetingInvitationsOrCancellations = flag ? CalendarItemUpdateOperationType.SendToChangedAndSaveCopy : CalendarItemUpdateOperationType.SendToNone;
          UpdateItem1.SendMeetingInvitationsOrCancellationsSpecified = true;
          UpdateItem1.MessageDisposition = flag ? MessageDispositionType.SendAndSaveCopy : MessageDispositionType.SaveOnly;
          UpdateItem1.MessageDispositionSpecified = true;
          if (sendSeparateRequired)
            UpdateItem1.SavedItemFolderId = new TargetFolderIdType()
            {
              Item = (BaseFolderIdType) new DistinguishedFolderIdType()
              {
                Id = DistinguishedFolderIdNameType.drafts,
                Mailbox = pxExchangeServer.PrepareMailbox(mailbox.Item1)
              }
            };
          if (UpdateItem1.ItemChanges.Length != 0)
          {
            UpdateItemResponseType response = (UpdateItemResponseType) null;
            response = pxExchangeServer._gate.UpdateItem(UpdateItem1);
            List<PXExchangeRequest<T, ItemType>> toSendSeparately = new List<PXExchangeRequest<T, ItemType>>();
            List<Tuple<PXExchangeItemID, PXExchangeReBase<T>>> needDetails = new List<Tuple<PXExchangeItemID, PXExchangeReBase<T>>>();
            for (int j = 0; j < response.ResponseMessages.Items.Length; ++j)
            {
              PXExchangeRequest<T, ItemChangeType> current = batch[j];
              ResponseMessageType responseMessageType1 = response.ResponseMessages.Items[j];
              if (responseMessageType1.ResponseClass == ResponseClassType.Success)
              {
                UpdateItemResponseMessageType responseMessageType2 = (UpdateItemResponseMessageType) responseMessageType1;
                if (responseMessageType2.ConflictResults != null && responseMessageType2.ConflictResults.Count > 0)
                  pxExchangeServer.LogWarning(current.Address, "{1} conflict(s) was(were) overridden during the update of the item '{0}'.", (object) responseMessageType2.ConflictResults.Count, (object) current.Item.Subject);
                ItemType[] itemTypeArray = responseMessageType2.Items.Items;
                for (int index = 0; index < itemTypeArray.Length; ++index)
                {
                  ItemType item = itemTypeArray[index];
                  PXOperationResult<ItemIdType> attachment = pxExchangeServer.CreateAttachment(mailbox.Item1, item, batch[j].Attachments, true);
                  if (attachment != null)
                  {
                    if (attachment.Success)
                      item.ItemId = attachment.Result;
                    else
                      yield return new PXExchangeResponce<T>((PXExchangeReBase<T>) current, item as T, error: attachment.Error.Message);
                  }
                  if (sendSeparateRequired)
                    toSendSeparately.Add(new PXExchangeRequest<T, ItemType>(current.Folder, item, current.UID, current.Tag));
                  else if (detailsRequired)
                    needDetails.Add(Tuple.Create<PXExchangeItemID, PXExchangeReBase<T>>(new PXExchangeItemID(current.Folder.Address, item.ItemId), (PXExchangeReBase<T>) current));
                  else
                    yield return new PXExchangeResponce<T>((PXExchangeReBase<T>) batch[j], item as T);
                  item = (ItemType) null;
                }
                itemTypeArray = (ItemType[]) null;
              }
              else
              {
                IEnumerable<string> messages = responseMessageType1.MessageXml != null ? ((IEnumerable<XmlElement>) responseMessageType1.MessageXml.Any).Select<XmlElement, string>((Func<XmlElement, string>) (x => x.OuterXml)) : (IEnumerable<string>) null;
                yield return new PXExchangeResponce<T>((PXExchangeReBase<T>) batch[j], code: responseMessageType1.ResponseCode, error: responseMessageType1.MessageText, messages: messages);
              }
              current = (PXExchangeRequest<T, ItemChangeType>) null;
            }
            if (toSendSeparately.Count > 0)
            {
              foreach (PXExchangeResponce<T> sendItem in pxExchangeServer.SendItems<T>(toSendSeparately.ToArray()))
              {
                if (detailsRequired && sendItem.Success)
                  needDetails.Add(Tuple.Create<PXExchangeItemID, PXExchangeReBase<T>>(new PXExchangeItemID(sendItem.Address, sendItem.Item.ItemId), (PXExchangeReBase<T>) sendItem));
                else
                  yield return sendItem;
              }
            }
            if (needDetails.Count > 0)
            {
              foreach (PXExchangeItem<T> pxExchangeItem in pxExchangeServer.GetItems<T>(true, false, (BasePathToElementType[]) null, needDetails.Select<Tuple<PXExchangeItemID, PXExchangeReBase<T>>, PXExchangeItemID>((Func<Tuple<PXExchangeItemID, PXExchangeReBase<T>>, PXExchangeItemID>) (d => d.Item1)).ToArray<PXExchangeItemID>()))
              {
                PXExchangeItem<T> item = pxExchangeItem;
                yield return new PXExchangeResponce<T>(needDetails.First<Tuple<PXExchangeItemID, PXExchangeReBase<T>>>((Func<Tuple<PXExchangeItemID, PXExchangeReBase<T>>, bool>) (r => r.Item1.ItemID.Id == item.Item.ItemId.Id)).Item2, item as T);
              }
            }
            batch = (PXExchangeRequest<T, ItemChangeType>[]) null;
            response = (UpdateItemResponseType) null;
            toSendSeparately = (List<PXExchangeRequest<T, ItemType>>) null;
            needDetails = (List<Tuple<PXExchangeItemID, PXExchangeReBase<T>>>) null;
          }
        }
        items = (PXExchangeRequest<T, ItemChangeType>[]) null;
      }
    }
  }

  public IEnumerable<PXExchangeResponce<T>> DeleteItems<T>(
    params PXExchangeRequest<T, ItemType>[] data)
    where T : ItemType, new()
  {
    return data == null || data.Length == 0 ? (IEnumerable<PXExchangeResponce<T>>) new PXExchangeResponce<T>[0] : this.DeleteItems<T>((IEnumerable<PXExchangeRequest<T, ItemType>>) data);
  }

  public IEnumerable<PXExchangeResponce<T>> DeleteItems<T>(
    IEnumerable<PXExchangeRequest<T, ItemType>> data)
    where T : ItemType, new()
  {
    PXExchangeServer pxExchangeServer1 = this;
    if (data != null)
    {
      pxExchangeServer1.EnsureTimezoneInitialised();
      int maxRows = pxExchangeServer1.UpdatePackageSize;
      foreach (Tuple<string, BaseFolderIdType, PXExchangeRequest<T, ItemType>[]> batchItem in pxExchangeServer1.BatchItems<T, ItemType>(maxRows, data))
      {
        PXExchangeRequest<T, ItemType>[] items = batchItem.Item3;
        for (int i = 0; i < items.Length; i += maxRows)
        {
          PXExchangeServer pxExchangeServer = pxExchangeServer1;
          PXExchangeRequest<T, ItemType>[] batch = ((IEnumerable<PXExchangeRequest<T, ItemType>>) items).Skip<PXExchangeRequest<T, ItemType>>(i).Take<PXExchangeRequest<T, ItemType>>(maxRows).ToArray<PXExchangeRequest<T, ItemType>>();
          DeleteItemType deleteItem = new DeleteItemType();
          deleteItem.DeleteType = DisposalType.MoveToDeletedItems;
          deleteItem.AffectedTaskOccurrences = AffectedTaskOccurrencesType.AllOccurrences;
          deleteItem.AffectedTaskOccurrencesSpecified = true;
          deleteItem.SendMeetingCancellations = CalendarItemCreateOrDeleteOperationType.SendOnlyToAll;
          deleteItem.SendMeetingCancellationsSpecified = true;
          deleteItem.SuppressReadReceipts = true;
          deleteItem.SuppressReadReceiptsSpecified = true;
          deleteItem.ItemIds = ((IEnumerable<PXExchangeRequest<T, ItemType>>) batch).Select<PXExchangeRequest<T, ItemType>, BaseItemIdType>((Func<PXExchangeRequest<T, ItemType>, BaseItemIdType>) (t => (BaseItemIdType) t.Request.ItemId)).ToArray<BaseItemIdType>();
          if (deleteItem.ItemIds.Length != 0)
          {
            DeleteItemResponseType response = (DeleteItemResponseType) null;
            RetryableOperation.DoOperation((System.Action) (() => response = pxExchangeServer._gate.DeleteItem(deleteItem)), 2, (Action<int, Exception>) null);
            for (int j = 0; j < response.ResponseMessages.Items.Length; ++j)
            {
              ResponseMessageType responseMessageType = response.ResponseMessages.Items[j];
              if (responseMessageType.ResponseClass == ResponseClassType.Success)
                yield return new PXExchangeResponce<T>((PXExchangeReBase<T>) batch[j]);
              else if (responseMessageType.ResponseCode == ResponseCodeType.ErrorItemNotFound)
              {
                yield return new PXExchangeResponce<T>((PXExchangeReBase<T>) batch[j]);
              }
              else
              {
                IEnumerable<string> messages = responseMessageType.MessageXml != null ? ((IEnumerable<XmlElement>) responseMessageType.MessageXml.Any).Select<XmlElement, string>((Func<XmlElement, string>) (x => x.OuterXml)) : (IEnumerable<string>) null;
                yield return new PXExchangeResponce<T>((PXExchangeReBase<T>) batch[j], code: responseMessageType.ResponseCode, error: responseMessageType.MessageText, messages: messages);
              }
            }
            batch = (PXExchangeRequest<T, ItemType>[]) null;
          }
        }
        items = (PXExchangeRequest<T, ItemType>[]) null;
      }
    }
  }

  public IEnumerable<PXExchangeResponce<T>> CancelItems<T>(
    params PXExchangeRequest<T, ItemType>[] data)
    where T : ItemType, new()
  {
    return data == null || data.Length == 0 ? (IEnumerable<PXExchangeResponce<T>>) new PXExchangeResponce<T>[0] : this.CancelItems<T>((IEnumerable<PXExchangeRequest<T, ItemType>>) data);
  }

  public IEnumerable<PXExchangeResponce<T>> CancelItems<T>(
    IEnumerable<PXExchangeRequest<T, ItemType>> data)
    where T : ItemType, new()
  {
    if (data != null)
    {
      this.EnsureTimezoneInitialised();
      int maxRows = this.UpdatePackageSize;
      foreach (Tuple<string, BaseFolderIdType, PXExchangeRequest<T, ItemType>[]> mailbox in this.BatchItems<T, ItemType>(maxRows, data))
      {
        PXExchangeRequest<T, ItemType>[] items = mailbox.Item3;
        for (int i = 0; i < items.Length; i += maxRows)
        {
          PXExchangeRequest<T, ItemType>[] batch = ((IEnumerable<PXExchangeRequest<T, ItemType>>) items).Skip<PXExchangeRequest<T, ItemType>>(i).Take<PXExchangeRequest<T, ItemType>>(maxRows).ToArray<PXExchangeRequest<T, ItemType>>();
          CreateItemType CreateItem1 = new CreateItemType();
          CreateItem1.MessageDisposition = MessageDispositionType.SendAndSaveCopy;
          CreateItem1.MessageDispositionSpecified = true;
          CreateItem1.SendMeetingInvitations = CalendarItemCreateOrDeleteOperationType.SendOnlyToAll;
          CreateItem1.SendMeetingInvitationsSpecified = true;
          CreateItem1.SavedItemFolderId = new TargetFolderIdType()
          {
            Item = mailbox.Item2
          };
          CreateItem1.Items = new NonEmptyArrayOfAllItemsType()
          {
            Items = (ItemType[]) ((IEnumerable<PXExchangeRequest<T, ItemType>>) batch).Select<PXExchangeRequest<T, ItemType>, CancelCalendarItemType>((Func<PXExchangeRequest<T, ItemType>, CancelCalendarItemType>) (t =>
            {
              return new CancelCalendarItemType()
              {
                ReferenceItemId = t.Request.ItemId
              };
            })).ToArray<CancelCalendarItemType>()
          };
          if (CreateItem1.Items.Items.Length != 0)
          {
            CreateItemResponseType response = (CreateItemResponseType) null;
            response = this._gate.CreateItem(CreateItem1);
            for (int j = 0; j < response.ResponseMessages.Items.Length; ++j)
            {
              ResponseMessageType responseMessageType = response.ResponseMessages.Items[j];
              if (responseMessageType.ResponseClass == ResponseClassType.Success)
                yield return new PXExchangeResponce<T>((PXExchangeReBase<T>) batch[j]);
              else if (responseMessageType.ResponseCode == ResponseCodeType.ErrorItemNotFound)
              {
                yield return new PXExchangeResponce<T>((PXExchangeReBase<T>) batch[j]);
              }
              else
              {
                IEnumerable<string> messages = responseMessageType.MessageXml != null ? ((IEnumerable<XmlElement>) responseMessageType.MessageXml.Any).Select<XmlElement, string>((Func<XmlElement, string>) (x => x.OuterXml)) : (IEnumerable<string>) null;
                yield return new PXExchangeResponce<T>((PXExchangeReBase<T>) batch[j], code: responseMessageType.ResponseCode, error: responseMessageType.MessageText, messages: messages);
              }
            }
            batch = (PXExchangeRequest<T, ItemType>[]) null;
            response = (CreateItemResponseType) null;
          }
        }
        items = (PXExchangeRequest<T, ItemType>[]) null;
      }
    }
  }

  public IEnumerable<PXExchangeResponce<T>> SendItems<T>(
    params PXExchangeRequest<T, ItemType>[] data)
    where T : ItemType, new()
  {
    return data == null || data.Length == 0 ? (IEnumerable<PXExchangeResponce<T>>) new PXExchangeResponce<T>[0] : this.SendItems<T>((IEnumerable<PXExchangeRequest<T, ItemType>>) data);
  }

  public IEnumerable<PXExchangeResponce<T>> SendItems<T>(
    IEnumerable<PXExchangeRequest<T, ItemType>> data)
    where T : ItemType, new()
  {
    if (data != null)
    {
      this.EnsureTimezoneInitialised();
      int maxRows = this.UpdatePackageSize;
      foreach (Tuple<string, BaseFolderIdType, PXExchangeRequest<T, ItemType>[]> mailbox in this.BatchItems<T, ItemType>(maxRows, data))
      {
        PXExchangeRequest<T, ItemType>[] items = mailbox.Item3;
        for (int i = 0; i < items.Length; i += maxRows)
        {
          PXExchangeRequest<T, ItemType>[] batch = ((IEnumerable<PXExchangeRequest<T, ItemType>>) items).Skip<PXExchangeRequest<T, ItemType>>(i).Take<PXExchangeRequest<T, ItemType>>(maxRows).ToArray<PXExchangeRequest<T, ItemType>>();
          SendItemType SendItem1 = new SendItemType();
          SendItem1.SavedItemFolderId = new TargetFolderIdType()
          {
            Item = mailbox.Item2
          };
          SendItem1.ItemIds = ((IEnumerable<PXExchangeRequest<T, ItemType>>) batch).Select<PXExchangeRequest<T, ItemType>, BaseItemIdType>((Func<PXExchangeRequest<T, ItemType>, BaseItemIdType>) (t => (BaseItemIdType) t.Request.ItemId)).ToArray<BaseItemIdType>();
          SendItem1.SaveItemToFolder = true;
          if (SendItem1.ItemIds.Length != 0)
          {
            SendItemResponseType response = (SendItemResponseType) null;
            response = this._gate.SendItem(SendItem1);
            for (int j = 0; j < response.ResponseMessages.Items.Length; ++j)
            {
              ResponseMessageType responseMessageType = response.ResponseMessages.Items[j];
              if (responseMessageType.ResponseClass == ResponseClassType.Success)
              {
                yield return new PXExchangeResponce<T>((PXExchangeReBase<T>) batch[j]);
              }
              else
              {
                IEnumerable<string> messages = responseMessageType.MessageXml != null ? ((IEnumerable<XmlElement>) responseMessageType.MessageXml.Any).Select<XmlElement, string>((Func<XmlElement, string>) (x => x.OuterXml)) : (IEnumerable<string>) null;
                yield return new PXExchangeResponce<T>((PXExchangeReBase<T>) batch[j], code: responseMessageType.ResponseCode, error: responseMessageType.MessageText, messages: messages);
              }
            }
            batch = (PXExchangeRequest<T, ItemType>[]) null;
            response = (SendItemResponseType) null;
          }
        }
        items = (PXExchangeRequest<T, ItemType>[]) null;
      }
    }
  }

  public IEnumerable<PXExchangeResponce<T>> CategorizeItems<T>(
    string category,
    params PXExchangeRequest<T, T>[] data)
    where T : ItemType, new()
  {
    return data == null || data.Length == 0 ? (IEnumerable<PXExchangeResponce<T>>) new PXExchangeResponce<T>[0] : this.CategorizeItems<T>(category, (IEnumerable<PXExchangeRequest<T, T>>) data);
  }

  public IEnumerable<PXExchangeResponce<T>> CategorizeItems<T>(
    string category,
    IEnumerable<PXExchangeRequest<T, T>> data)
    where T : ItemType, new()
  {
    if (data == null)
      return (IEnumerable<PXExchangeResponce<T>>) new PXExchangeResponce<T>[0];
    Func<ItemType, ItemChangeType> convert = (Func<ItemType, ItemChangeType>) (item =>
    {
      string[] first = new string[1]{ category };
      if (item.Categories != null)
        first = ((IEnumerable<string>) first).Concat<string>((IEnumerable<string>) item.Categories).ToArray<string>();
      ItemChangeType itemChangeType1 = new ItemChangeType();
      itemChangeType1.Item = (BaseItemIdType) item.ItemId;
      ItemChangeType itemChangeType2 = itemChangeType1;
      ItemChangeDescriptionType[] changeDescriptionTypeArray = new ItemChangeDescriptionType[1]
      {
        (ItemChangeDescriptionType) new SetItemFieldType()
        {
          Item = (BasePathToElementType) new PathToUnindexedFieldType()
          {
            FieldURI = UnindexedFieldURIType.itemCategories
          },
          Item1 = (ItemType) new T() { Categories = first }
        }
      };
      itemChangeType2.Updates = changeDescriptionTypeArray;
      return itemChangeType1;
    });
    return this.UpdateItems<T>(data.Select<PXExchangeRequest<T, T>, PXExchangeRequest<T, ItemChangeType>>((Func<PXExchangeRequest<T, T>, PXExchangeRequest<T, ItemChangeType>>) (r => new PXExchangeRequest<T, ItemChangeType>(r.Folder, convert((ItemType) r.Item), r.UID, r.Tag, r.Attachments)
    {
      SendRequired = r.SendRequired,
      SendSeparateRequired = r.SendSeparateRequired
    })).ToArray<PXExchangeRequest<T, ItemChangeType>>());
  }

  public IEnumerable<PXExchangeResponce<T>> MoveItems<T>(
    params PXExchangeRequest<T, ItemIdType>[] data)
    where T : ItemType, new()
  {
    return data == null || data.Length == 0 ? (IEnumerable<PXExchangeResponce<T>>) new PXExchangeResponce<T>[0] : this.MoveItems<T>((IEnumerable<PXExchangeRequest<T, ItemIdType>>) data);
  }

  public IEnumerable<PXExchangeResponce<T>> MoveItems<T>(
    IEnumerable<PXExchangeRequest<T, ItemIdType>> data)
    where T : ItemType, new()
  {
    if (data != null)
    {
      this.EnsureTimezoneInitialised();
      int maxRows = this.UpdatePackageSize;
      foreach (Tuple<string, BaseFolderIdType, PXExchangeRequest<T, ItemIdType>[]> mailbox in this.BatchItems<T, ItemIdType>(maxRows, data))
      {
        PXExchangeRequest<T, ItemIdType>[] items = mailbox.Item3;
        for (int i = 0; i < items.Length; i += maxRows)
        {
          PXExchangeRequest<T, ItemIdType>[] batch = ((IEnumerable<PXExchangeRequest<T, ItemIdType>>) items).Skip<PXExchangeRequest<T, ItemIdType>>(i).Take<PXExchangeRequest<T, ItemIdType>>(maxRows).ToArray<PXExchangeRequest<T, ItemIdType>>();
          MoveItemType MoveItem1 = new MoveItemType();
          MoveItem1.ReturnNewItemIds = true;
          MoveItem1.ReturnNewItemIdsSpecified = true;
          MoveItem1.ToFolderId = new TargetFolderIdType()
          {
            Item = mailbox.Item2
          };
          MoveItem1.ItemIds = ((IEnumerable<PXExchangeRequest<T, ItemIdType>>) batch).Select<PXExchangeRequest<T, ItemIdType>, BaseItemIdType>((Func<PXExchangeRequest<T, ItemIdType>, BaseItemIdType>) (t => (BaseItemIdType) t.Request)).ToArray<BaseItemIdType>();
          if (MoveItem1.ItemIds.Length != 0)
          {
            MoveItemResponseType response = (MoveItemResponseType) null;
            response = this._gate.MoveItem(MoveItem1);
            for (int j = 0; j < response.ResponseMessages.Items.Length; ++j)
            {
              ResponseMessageType responseMessageType = response.ResponseMessages.Items[j];
              if (responseMessageType.ResponseClass == ResponseClassType.Success)
              {
                ItemType[] itemTypeArray = ((ItemInfoResponseMessageType) responseMessageType).Items.Items;
                for (int index = 0; index < itemTypeArray.Length; ++index)
                  yield return new PXExchangeResponce<T>((PXExchangeReBase<T>) batch[j], itemTypeArray[index] as T);
                itemTypeArray = (ItemType[]) null;
              }
              else
              {
                IEnumerable<string> messages = responseMessageType.MessageXml != null ? ((IEnumerable<XmlElement>) responseMessageType.MessageXml.Any).Select<XmlElement, string>((Func<XmlElement, string>) (x => x.OuterXml)) : (IEnumerable<string>) null;
                yield return new PXExchangeResponce<T>((PXExchangeReBase<T>) batch[j], code: responseMessageType.ResponseCode, error: responseMessageType.MessageText, messages: messages);
              }
            }
            batch = (PXExchangeRequest<T, ItemIdType>[]) null;
            response = (MoveItemResponseType) null;
          }
        }
        items = (PXExchangeRequest<T, ItemIdType>[]) null;
      }
    }
  }

  public IEnumerable<AttachmentType> GetAttachments(params AttachmentIdType[] attachments)
  {
    return this.GetAttachments(false, attachments);
  }

  public IEnumerable<AttachmentType> GetAttachments(
    bool includeMimeContent,
    params AttachmentIdType[] attachments)
  {
    PXExchangeServer pxExchangeServer1 = this;
    if (attachments.Length != 0)
    {
      int maxRows = pxExchangeServer1.UpdatePackageSize;
      for (int i = 0; i < attachments.Length; i += maxRows)
      {
        PXExchangeServer pxExchangeServer = pxExchangeServer1;
        GetAttachmentType request = new GetAttachmentType();
        request.AttachmentShape = new AttachmentResponseShapeType()
        {
          BodyType = BodyTypeResponseType.Best,
          BodyTypeSpecified = true,
          IncludeMimeContent = includeMimeContent,
          IncludeMimeContentSpecified = includeMimeContent
        };
        request.AttachmentIds = (RequestAttachmentIdType[]) ((IEnumerable<AttachmentIdType>) attachments).Skip<AttachmentIdType>(i).Take<AttachmentIdType>(maxRows).ToArray<AttachmentIdType>();
        if (request.AttachmentIds.Length != 0)
        {
          GetAttachmentResponseType response = (GetAttachmentResponseType) null;
          RetryableOperation.DoOperation((System.Action) (() => response = pxExchangeServer._gate.GetAttachment(request)));
          ResponseMessageType[] responseMessageTypeArray = response.ResponseMessages.Items;
          for (int index1 = 0; index1 < responseMessageTypeArray.Length; ++index1)
          {
            ResponseMessageType responseMessageType = responseMessageTypeArray[index1];
            if (responseMessageType.ResponseClass == ResponseClassType.Success)
            {
              AttachmentType[] attachmentTypeArray = ((AttachmentInfoResponseMessageType) responseMessageType).Attachments;
              for (int index2 = 0; index2 < attachmentTypeArray.Length; ++index2)
                yield return attachmentTypeArray[index2];
              attachmentTypeArray = (AttachmentType[]) null;
            }
            else
            {
              IEnumerable<string> values = responseMessageType.MessageXml != null ? ((IEnumerable<XmlElement>) responseMessageType.MessageXml.Any).Select<XmlElement, string>((Func<XmlElement, string>) (x => x.OuterXml)) : (IEnumerable<string>) null;
              throw new PXException(values == null ? responseMessageType.MessageText : responseMessageType.MessageText + Environment.NewLine + string.Join(Environment.NewLine, values));
            }
          }
          responseMessageTypeArray = (ResponseMessageType[]) null;
        }
      }
    }
  }

  public IEnumerable<AttachmentType> CreateAttachment(
    params Tuple<ItemIdType, AttachmentType[]>[] attachments)
  {
    Tuple<ItemIdType, AttachmentType[]>[] tupleArray = attachments;
    for (int index1 = 0; index1 < tupleArray.Length; ++index1)
    {
      Tuple<ItemIdType, AttachmentType[]> mailbox = tupleArray[index1];
      List<AttachmentType> batch = new List<AttachmentType>();
      for (int i = 0; i < mailbox.Item2.Length; ++i)
      {
        batch.Add(mailbox.Item2[i]);
        if (batch.Sum<AttachmentType>((Func<AttachmentType, int>) (a => a.Size)) > this.AttachmentSize || i + 1 >= mailbox.Item2.Length)
        {
          CreateAttachmentType CreateAttachment1 = new CreateAttachmentType();
          CreateAttachment1.ParentItemId = mailbox.Item1;
          CreateAttachment1.Attachments = batch.ToArray();
          if (CreateAttachment1.Attachments.Length != 0)
          {
            CreateAttachmentResponseType response = (CreateAttachmentResponseType) null;
            response = this._gate.CreateAttachment(CreateAttachment1);
            for (int j = 0; j < response.ResponseMessages.Items.Length; ++j)
            {
              ResponseMessageType responseMessageType = response.ResponseMessages.Items[j];
              if (responseMessageType.ResponseClass == ResponseClassType.Success)
              {
                AttachmentType[] attachmentTypeArray = ((AttachmentInfoResponseMessageType) responseMessageType).Attachments;
                for (int index2 = 0; index2 < attachmentTypeArray.Length; ++index2)
                  yield return attachmentTypeArray[index2];
                attachmentTypeArray = (AttachmentType[]) null;
              }
              else
              {
                IEnumerable<string> values = responseMessageType.MessageXml != null ? ((IEnumerable<XmlElement>) responseMessageType.MessageXml.Any).Select<XmlElement, string>((Func<XmlElement, string>) (x => x.OuterXml)) : (IEnumerable<string>) null;
                throw new PXException(values == null ? responseMessageType.MessageText : responseMessageType.MessageText + Environment.NewLine + string.Join(Environment.NewLine, values));
              }
            }
            batch.Clear();
            response = (CreateAttachmentResponseType) null;
          }
        }
      }
      batch = (List<AttachmentType>) null;
      mailbox = (Tuple<ItemIdType, AttachmentType[]>) null;
    }
    tupleArray = (Tuple<ItemIdType, AttachmentType[]>[]) null;
  }

  public void DeleteAttachment(params AttachmentIdType[] attachments)
  {
    if (attachments.Length == 0)
      return;
    int updatePackageSize = this.UpdatePackageSize;
    for (int count = 0; count < attachments.Length; count += updatePackageSize)
    {
      DeleteAttachmentType request = new DeleteAttachmentType();
      request.AttachmentIds = (RequestAttachmentIdType[]) ((IEnumerable<AttachmentIdType>) attachments).Skip<AttachmentIdType>(count).Take<AttachmentIdType>(updatePackageSize).ToArray<AttachmentIdType>();
      if (request.AttachmentIds.Length != 0)
      {
        DeleteAttachmentResponseType response = (DeleteAttachmentResponseType) null;
        RetryableOperation.DoOperation((System.Action) (() => response = this._gate.DeleteAttachment(request)));
        foreach (ResponseMessageType responseMessageType in response.ResponseMessages.Items)
        {
          if (responseMessageType.ResponseClass != ResponseClassType.Success)
          {
            IEnumerable<string> values = responseMessageType.MessageXml != null ? ((IEnumerable<XmlElement>) responseMessageType.MessageXml.Any).Select<XmlElement, string>((Func<XmlElement, string>) (x => x.OuterXml)) : (IEnumerable<string>) null;
            throw new PXException(values == null ? responseMessageType.MessageText : responseMessageType.MessageText + Environment.NewLine + string.Join(Environment.NewLine, values));
          }
        }
      }
    }
  }

  protected AttachmentType[] GetAttachments(string mailbox, ItemType item)
  {
    try
    {
      if (item.Attachments == null || item.Attachments.Length == 0)
        return (AttachmentType[]) null;
      List<AttachmentIdType> attachmentIdTypeList = new List<AttachmentIdType>();
      foreach (AttachmentType attachment in item.Attachments)
      {
        if (attachment.Size > this.AttachmentSize)
          this.LogWarning(mailbox, "The size of the attachment '{0}' is bigger than '{1}' bytes; the attachment will be skipped.", (object) attachment.Name, (object) this.AttachmentSize);
        else
          attachmentIdTypeList.Add(attachment.AttachmentId);
      }
      if (!this.ContainsRfc822Attachment((IEnumerable<AttachmentType>) item.Attachments))
        return this.GetAttachments(attachmentIdTypeList.ToArray()).ToArray<AttachmentType>();
      List<AttachmentType> list = this.GetAttachments(true, attachmentIdTypeList.ToArray()).ToList<AttachmentType>();
      return list.Where<AttachmentType>((Func<AttachmentType, bool>) (a => a is FileAttachmentType)).ToList<AttachmentType>().Concat<AttachmentType>((IEnumerable<AttachmentType>) this.PrepareRfc822Attachments((IEnumerable<AttachmentType>) list)).ToArray<AttachmentType>();
    }
    catch (Exception ex)
    {
      this.LogError(mailbox, ex);
      return (AttachmentType[]) null;
    }
  }

  protected PXOperationResult<ItemIdType> CreateAttachment(
    string mailbox,
    ItemType item,
    AttachmentType[] attachments,
    bool validateExisting)
  {
    if (attachments == null)
      attachments = new AttachmentType[0];
    List<AttachmentType> attachmentTypeList1 = new List<AttachmentType>((IEnumerable<AttachmentType>) attachments);
    List<AttachmentType> attachmentTypeList2 = new List<AttachmentType>();
    if (validateExisting)
    {
      GetItemType GetItem1 = new GetItemType();
      GetItem1.ItemIds = new BaseItemIdType[1]
      {
        (BaseItemIdType) item.ItemId
      };
      GetItemType getItemType = GetItem1;
      ItemResponseShapeType responseShapeType1 = new ItemResponseShapeType();
      responseShapeType1.BaseShape = DefaultShapeNamesType.IdOnly;
      responseShapeType1.AdditionalProperties = new BasePathToElementType[2]
      {
        (BasePathToElementType) new PathToUnindexedFieldType()
        {
          FieldURI = UnindexedFieldURIType.itemAttachments
        },
        (BasePathToElementType) new PathToUnindexedFieldType()
        {
          FieldURI = UnindexedFieldURIType.itemHasAttachments
        }
      };
      ItemResponseShapeType responseShapeType2 = responseShapeType1;
      getItemType.ItemShape = responseShapeType2;
      foreach (ResponseMessageType responseMessageType in this._gate.GetItem(GetItem1).ResponseMessages.Items)
      {
        if (responseMessageType.ResponseClass == ResponseClassType.Success)
        {
          foreach (ItemType itemType in ((ItemInfoResponseMessageType) responseMessageType).Items.Items)
          {
            if (itemType.Attachments != null && itemType.Attachments.Length != 0)
            {
              foreach (AttachmentType attachment in itemType.Attachments)
              {
                AttachmentType att = attachment;
                AttachmentType attachmentType = ((IEnumerable<AttachmentType>) attachments).FirstOrDefault<AttachmentType>((Func<AttachmentType, bool>) (a => string.Equals(a.Name, att.Name, StringComparison.InvariantCultureIgnoreCase) || string.Equals(this.GetShortName(a.Name), this.GetShortName(att.Name), StringComparison.InvariantCultureIgnoreCase) || string.Equals(a.ContentId, att.ContentId, StringComparison.InvariantCultureIgnoreCase)));
                if (attachmentType != null)
                  attachmentTypeList2.Add(attachmentType);
                if (attachmentType != null)
                  attachmentTypeList1.Remove(attachmentType);
              }
            }
          }
        }
      }
    }
    try
    {
      ItemIdType result = item.ItemId;
      if (attachmentTypeList1.Count > 0)
      {
        foreach (AttachmentType attachmentType in this.CreateAttachment(Tuple.Create<ItemIdType, AttachmentType[]>(item.ItemId, attachmentTypeList1.ToArray())))
          result = new ItemIdType()
          {
            Id = attachmentType.AttachmentId.RootItemId,
            ChangeKey = attachmentType.AttachmentId.RootItemChangeKey
          };
      }
      return new PXOperationResult<ItemIdType>(result);
    }
    catch (Exception ex)
    {
      this.LogError(mailbox, ex);
      return new PXOperationResult<ItemIdType>(ex);
    }
  }

  protected void SerialiseObjectToText(object request)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      new XmlSerializer(request.GetType()).Serialize((Stream) memoryStream, request);
      memoryStream.Flush();
      memoryStream.Seek(0L, SeekOrigin.Begin);
      using (StreamReader streamReader = new StreamReader((Stream) memoryStream))
      {
        string end = streamReader.ReadToEnd();
        if (System.DateTime.Now.Ticks >= 0L)
          return;
        Console.WriteLine(end);
      }
    }
  }

  protected IEnumerable<Tuple<string, BaseFolderIdType, PXExchangeRequest<T, ReqT>[]>> BatchItems<T, ReqT>(
    int size,
    IEnumerable<PXExchangeRequest<T, ReqT>> items)
    where T : ItemType, new()
  {
    Dictionary<string, List<PXExchangeRequest<T, ReqT>>> result = new Dictionary<string, List<PXExchangeRequest<T, ReqT>>>();
    foreach (PXExchangeRequest<T, ReqT> pxExchangeRequest in items)
    {
      string str = string.Empty;
      if (pxExchangeRequest.Folder.FolderID is FolderIdType)
        str = ((FolderIdType) pxExchangeRequest.Folder.FolderID).Id;
      if (pxExchangeRequest.Folder.FolderID is DistinguishedFolderIdType)
        str = ((DistinguishedFolderIdType) pxExchangeRequest.Folder.FolderID).Id.ToString();
      string key = $"{pxExchangeRequest.Folder.Address}|{str}|{(object) pxExchangeRequest.SendRequired}";
      List<PXExchangeRequest<T, ReqT>> list;
      if (!result.TryGetValue(key, out list))
        result[key] = list = new List<PXExchangeRequest<T, ReqT>>();
      list.Add(pxExchangeRequest);
      if (list.Count >= size)
      {
        yield return Tuple.Create<string, BaseFolderIdType, PXExchangeRequest<T, ReqT>[]>(pxExchangeRequest.Folder.Address, pxExchangeRequest.Folder.FolderID, list.ToArray());
        list.Clear();
      }
      list = (List<PXExchangeRequest<T, ReqT>>) null;
    }
    foreach (KeyValuePair<string, List<PXExchangeRequest<T, ReqT>>> keyValuePair in result)
    {
      if (keyValuePair.Value.Count > 0)
        yield return Tuple.Create<string, BaseFolderIdType, PXExchangeRequest<T, ReqT>[]>(keyValuePair.Value.First<PXExchangeRequest<T, ReqT>>().Address, keyValuePair.Value.First<PXExchangeRequest<T, ReqT>>().Folder.FolderID, keyValuePair.Value.ToArray());
    }
  }

  protected string GetShortName(string name)
  {
    if (string.IsNullOrEmpty(name))
      return name;
    if (name.Contains("\\"))
      name = name.Substring(name.IndexOf("\\") + 1);
    if (name.Contains("/"))
      name = name.Substring(name.IndexOf("/") + 1);
    return name;
  }

  protected void LogVerbose(string mailbox, string message, params object[] args)
  {
    if (this.Logger == null)
      return;
    this.Logger(new PXExchangeEvent(mailbox, EventLevel.Verbose, string.Format(message, args), (Exception) null));
  }

  protected void LogInfo(string mailbox, string message, params object[] args)
  {
    if (this.Logger == null)
      return;
    this.Logger(new PXExchangeEvent(mailbox, EventLevel.Informational, string.Format(message, args), (Exception) null));
  }

  protected void LogWarning(string mailbox, string message, params object[] args)
  {
    if (this.Logger == null)
      return;
    this.Logger(new PXExchangeEvent(mailbox, EventLevel.Warning, string.Format(message, args), (Exception) null));
  }

  protected void LogError(string mailbox, Exception error, string message = null)
  {
    if (this.Logger == null)
      return;
    this.Logger(new PXExchangeEvent(mailbox, EventLevel.Error, message, error));
  }

  protected bool ContainsRfc822Attachment(IEnumerable<AttachmentType> attachments)
  {
    return attachments != null && attachments.Any<AttachmentType>((Func<AttachmentType, bool>) (a => a is ItemAttachmentType && a.ContentType == "message/rfc822"));
  }

  protected IEnumerable<FileAttachmentType> PrepareRfc822Attachments(
    IEnumerable<AttachmentType> attachments)
  {
    List<FileAttachmentType> fileAttachmentTypeList = new List<FileAttachmentType>();
    Dictionary<string, int> dictionary = new Dictionary<string, int>();
    foreach (ItemAttachmentType itemAttachmentType in attachments.Where<AttachmentType>((Func<AttachmentType, bool>) (a => a is ItemAttachmentType && a.ContentType.ToLower() == "message/rfc822")))
    {
      string key = string.Join("_", (string.IsNullOrEmpty(itemAttachmentType.Name) ? "(empty subject)" : itemAttachmentType.Name).Split(Path.GetInvalidFileNameChars()));
      if (dictionary.ContainsKey(key))
        key = $"{key} ({dictionary[key]++})";
      else
        dictionary.Add(key, 1);
      string str = key + ".eml";
      byte[] numArray = string.IsNullOrEmpty(itemAttachmentType?.Item?.MimeContent?.Value) ? (byte[]) null : Convert.FromBase64String(itemAttachmentType.Item.MimeContent.Value);
      FileAttachmentType fileAttachmentType1 = new FileAttachmentType();
      fileAttachmentType1.AttachmentId = itemAttachmentType.AttachmentId;
      fileAttachmentType1.Content = numArray;
      fileAttachmentType1.ContentId = itemAttachmentType.ContentId;
      fileAttachmentType1.ContentLocation = itemAttachmentType.ContentLocation;
      fileAttachmentType1.ContentType = "text/plain";
      fileAttachmentType1.IsContactPhoto = false;
      fileAttachmentType1.IsContactPhotoSpecified = false;
      fileAttachmentType1.IsInline = false;
      fileAttachmentType1.IsInlineSpecified = false;
      fileAttachmentType1.LastModifiedTime = itemAttachmentType.LastModifiedTime;
      fileAttachmentType1.LastModifiedTimeSpecified = false;
      fileAttachmentType1.Name = str;
      fileAttachmentType1.Size = 0;
      fileAttachmentType1.SizeSpecified = false;
      FileAttachmentType fileAttachmentType2 = fileAttachmentType1;
      fileAttachmentTypeList.Add(fileAttachmentType2);
    }
    return (IEnumerable<FileAttachmentType>) fileAttachmentTypeList;
  }
}
