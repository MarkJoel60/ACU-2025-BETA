// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ExchangeServiceBinding
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[GeneratedCode("System.Web.Services", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[WebServiceBinding(Name = "ExchangeServiceBinding", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
[XmlInclude(typeof (AttendeeConflictData))]
[XmlInclude(typeof (ServiceConfiguration))]
[XmlInclude(typeof (DirectoryEntryType))]
[XmlInclude(typeof (BaseResponseMessageType))]
[XmlInclude(typeof (BaseCalendarItemStateDefinitionType))]
[XmlInclude(typeof (RuleOperationType))]
[XmlInclude(typeof (BaseSubscriptionRequestType))]
[XmlInclude(typeof (MailboxLocatorType))]
[XmlInclude(typeof (BaseGroupByType))]
[XmlInclude(typeof (RecurrenceRangeBaseType))]
[XmlInclude(typeof (RecurrencePatternBaseType))]
[XmlInclude(typeof (AttachmentType))]
[XmlInclude(typeof (ChangeDescriptionType))]
[XmlInclude(typeof (BasePagingType))]
[XmlInclude(typeof (BasePermissionType))]
[XmlInclude(typeof (BaseFolderType))]
[XmlInclude(typeof (BaseItemIdType))]
[XmlInclude(typeof (BaseEmailAddressType))]
[XmlInclude(typeof (BaseFolderIdType))]
[XmlInclude(typeof (BaseRequestType))]
public class ExchangeServiceBinding : SoapHttpClientProtocol
{
  private ExchangeImpersonationType exchangeImpersonationField;
  private MailboxCultureType mailboxCultureField;
  private RequestServerVersion requestServerVersionValueField;
  private ServerVersionInfo serverVersionInfoValueField;
  private SendOrPostCallback ResolveNamesOperationCompleted;
  private SendOrPostCallback ExpandDLOperationCompleted;
  private SendOrPostCallback GetServerTimeZonesOperationCompleted;
  private TimeZoneContextType timeZoneContextField;
  private ManagementRoleType managementRoleField;
  private SendOrPostCallback FindFolderOperationCompleted;
  private DateTimePrecisionType dateTimePrecisionField;
  private SendOrPostCallback FindItemOperationCompleted;
  private SendOrPostCallback GetFolderOperationCompleted;
  private SendOrPostCallback UploadItemsOperationCompleted;
  private SendOrPostCallback ExportItemsOperationCompleted;
  private SendOrPostCallback ConvertIdOperationCompleted;
  private SendOrPostCallback CreateFolderOperationCompleted;
  private SendOrPostCallback CreateFolderPathOperationCompleted;
  private SendOrPostCallback DeleteFolderOperationCompleted;
  private SendOrPostCallback EmptyFolderOperationCompleted;
  private SendOrPostCallback UpdateFolderOperationCompleted;
  private SendOrPostCallback MoveFolderOperationCompleted;
  private SendOrPostCallback CopyFolderOperationCompleted;
  private SendOrPostCallback SubscribeOperationCompleted;
  private SendOrPostCallback UnsubscribeOperationCompleted;
  private SendOrPostCallback GetEventsOperationCompleted;
  private SendOrPostCallback GetStreamingEventsOperationCompleted;
  private SendOrPostCallback SyncFolderHierarchyOperationCompleted;
  private SendOrPostCallback SyncFolderItemsOperationCompleted;
  private SendOrPostCallback CreateManagedFolderOperationCompleted;
  private SendOrPostCallback GetItemOperationCompleted;
  private SendOrPostCallback CreateItemOperationCompleted;
  private SendOrPostCallback DeleteItemOperationCompleted;
  private SendOrPostCallback UpdateItemOperationCompleted;
  private SendOrPostCallback UpdateItemInRecoverableItemsOperationCompleted;
  private SendOrPostCallback SendItemOperationCompleted;
  private SendOrPostCallback MoveItemOperationCompleted;
  private SendOrPostCallback CopyItemOperationCompleted;
  private SendOrPostCallback ArchiveItemOperationCompleted;
  private SendOrPostCallback CreateAttachmentOperationCompleted;
  private SendOrPostCallback DeleteAttachmentOperationCompleted;
  private SendOrPostCallback GetAttachmentOperationCompleted;
  private SendOrPostCallback GetClientAccessTokenOperationCompleted;
  private SendOrPostCallback GetDelegateOperationCompleted;
  private SendOrPostCallback AddDelegateOperationCompleted;
  private SendOrPostCallback RemoveDelegateOperationCompleted;
  private SendOrPostCallback UpdateDelegateOperationCompleted;
  private SendOrPostCallback CreateUserConfigurationOperationCompleted;
  private SendOrPostCallback DeleteUserConfigurationOperationCompleted;
  private SendOrPostCallback GetUserConfigurationOperationCompleted;
  private SendOrPostCallback UpdateUserConfigurationOperationCompleted;
  private SendOrPostCallback GetUserAvailabilityOperationCompleted;
  private SendOrPostCallback GetUserOofSettingsOperationCompleted;
  private SendOrPostCallback SetUserOofSettingsOperationCompleted;
  private SendOrPostCallback GetServiceConfigurationOperationCompleted;
  private SendOrPostCallback GetMailTipsOperationCompleted;
  private SendOrPostCallback PlayOnPhoneOperationCompleted;
  private SendOrPostCallback GetPhoneCallInformationOperationCompleted;
  private SendOrPostCallback DisconnectPhoneCallOperationCompleted;
  private SendOrPostCallback GetSharingMetadataOperationCompleted;
  private SendOrPostCallback RefreshSharingFolderOperationCompleted;
  private SendOrPostCallback GetSharingFolderOperationCompleted;
  private SendOrPostCallback SetTeamMailboxOperationCompleted;
  private SendOrPostCallback UnpinTeamMailboxOperationCompleted;
  private SendOrPostCallback GetRoomListsOperationCompleted;
  private SendOrPostCallback GetRoomsOperationCompleted;
  private SendOrPostCallback FindMessageTrackingReportOperationCompleted;
  private SendOrPostCallback GetMessageTrackingReportOperationCompleted;
  private SendOrPostCallback FindConversationOperationCompleted;
  private SendOrPostCallback ApplyConversationActionOperationCompleted;
  private SendOrPostCallback GetConversationItemsOperationCompleted;
  private SendOrPostCallback FindPeopleOperationCompleted;
  private SendOrPostCallback GetPersonaOperationCompleted;
  private SendOrPostCallback GetInboxRulesOperationCompleted;
  private SendOrPostCallback UpdateInboxRulesOperationCompleted;
  private SendOrPostCallback GetPasswordExpirationDateOperationCompleted;
  private SendOrPostCallback GetSearchableMailboxesOperationCompleted;
  private SendOrPostCallback SearchMailboxesOperationCompleted;
  private SendOrPostCallback GetDiscoverySearchConfigurationOperationCompleted;
  private SendOrPostCallback GetHoldOnMailboxesOperationCompleted;
  private SendOrPostCallback SetHoldOnMailboxesOperationCompleted;
  private SendOrPostCallback GetNonIndexableItemStatisticsOperationCompleted;
  private SendOrPostCallback GetNonIndexableItemDetailsOperationCompleted;
  private SendOrPostCallback MarkAllItemsAsReadOperationCompleted;
  private SendOrPostCallback MarkAsJunkOperationCompleted;
  private SendOrPostCallback GetAppManifestsOperationCompleted;
  private SendOrPostCallback AddNewImContactToGroupOperationCompleted;
  private SendOrPostCallback AddNewTelUriContactToGroupOperationCompleted;
  private SendOrPostCallback AddImContactToGroupOperationCompleted;
  private SendOrPostCallback RemoveImContactFromGroupOperationCompleted;
  private SendOrPostCallback AddImGroupOperationCompleted;
  private SendOrPostCallback AddDistributionGroupToImListOperationCompleted;
  private SendOrPostCallback GetImItemListOperationCompleted;
  private SendOrPostCallback GetImItemsOperationCompleted;
  private SendOrPostCallback RemoveContactFromImListOperationCompleted;
  private SendOrPostCallback RemoveDistributionGroupFromImListOperationCompleted;
  private SendOrPostCallback RemoveImGroupOperationCompleted;
  private SendOrPostCallback SetImGroupOperationCompleted;
  private SendOrPostCallback SetImListMigrationCompletedOperationCompleted;
  private SendOrPostCallback GetUserRetentionPolicyTagsOperationCompleted;
  private SendOrPostCallback InstallAppOperationCompleted;
  private SendOrPostCallback UninstallAppOperationCompleted;
  private SendOrPostCallback DisableAppOperationCompleted;
  private SendOrPostCallback GetAppMarketplaceUrlOperationCompleted;
  private SendOrPostCallback GetUserPhotoOperationCompleted;
  private bool useDefaultCredentialsSetExplicitly;

  /// <remarks />
  public ExchangeServiceBinding()
  {
    if (this.IsLocalFileSystemWebService(this.Url))
    {
      this.UseDefaultCredentials = true;
      this.useDefaultCredentialsSetExplicitly = false;
    }
    else
      this.useDefaultCredentialsSetExplicitly = true;
  }

  public ExchangeImpersonationType ExchangeImpersonation
  {
    get => this.exchangeImpersonationField;
    set => this.exchangeImpersonationField = value;
  }

  public MailboxCultureType MailboxCulture
  {
    get => this.mailboxCultureField;
    set => this.mailboxCultureField = value;
  }

  public RequestServerVersion RequestServerVersionValue
  {
    get => this.requestServerVersionValueField;
    set => this.requestServerVersionValueField = value;
  }

  public ServerVersionInfo ServerVersionInfoValue
  {
    get => this.serverVersionInfoValueField;
    set => this.serverVersionInfoValueField = value;
  }

  public TimeZoneContextType TimeZoneContext
  {
    get => this.timeZoneContextField;
    set => this.timeZoneContextField = value;
  }

  public ManagementRoleType ManagementRole
  {
    get => this.managementRoleField;
    set => this.managementRoleField = value;
  }

  public DateTimePrecisionType DateTimePrecision
  {
    get => this.dateTimePrecisionField;
    set => this.dateTimePrecisionField = value;
  }

  public new string Url
  {
    get => base.Url;
    set
    {
      if (this.IsLocalFileSystemWebService(base.Url) && !this.useDefaultCredentialsSetExplicitly && !this.IsLocalFileSystemWebService(value))
        base.UseDefaultCredentials = false;
      base.Url = value;
    }
  }

  public new bool UseDefaultCredentials
  {
    get => base.UseDefaultCredentials;
    set
    {
      base.UseDefaultCredentials = value;
      this.useDefaultCredentialsSetExplicitly = true;
    }
  }

  /// <remarks />
  public event ResolveNamesCompletedEventHandler ResolveNamesCompleted;

  /// <remarks />
  public event ExpandDLCompletedEventHandler ExpandDLCompleted;

  /// <remarks />
  public event GetServerTimeZonesCompletedEventHandler GetServerTimeZonesCompleted;

  /// <remarks />
  public event FindFolderCompletedEventHandler FindFolderCompleted;

  /// <remarks />
  public event FindItemCompletedEventHandler FindItemCompleted;

  /// <remarks />
  public event GetFolderCompletedEventHandler GetFolderCompleted;

  /// <remarks />
  public event UploadItemsCompletedEventHandler UploadItemsCompleted;

  /// <remarks />
  public event ExportItemsCompletedEventHandler ExportItemsCompleted;

  /// <remarks />
  public event ConvertIdCompletedEventHandler ConvertIdCompleted;

  /// <remarks />
  public event CreateFolderCompletedEventHandler CreateFolderCompleted;

  /// <remarks />
  public event CreateFolderPathCompletedEventHandler CreateFolderPathCompleted;

  /// <remarks />
  public event DeleteFolderCompletedEventHandler DeleteFolderCompleted;

  /// <remarks />
  public event EmptyFolderCompletedEventHandler EmptyFolderCompleted;

  /// <remarks />
  public event UpdateFolderCompletedEventHandler UpdateFolderCompleted;

  /// <remarks />
  public event MoveFolderCompletedEventHandler MoveFolderCompleted;

  /// <remarks />
  public event CopyFolderCompletedEventHandler CopyFolderCompleted;

  /// <remarks />
  public event SubscribeCompletedEventHandler SubscribeCompleted;

  /// <remarks />
  public event UnsubscribeCompletedEventHandler UnsubscribeCompleted;

  /// <remarks />
  public event GetEventsCompletedEventHandler GetEventsCompleted;

  /// <remarks />
  public event GetStreamingEventsCompletedEventHandler GetStreamingEventsCompleted;

  /// <remarks />
  public event SyncFolderHierarchyCompletedEventHandler SyncFolderHierarchyCompleted;

  /// <remarks />
  public event SyncFolderItemsCompletedEventHandler SyncFolderItemsCompleted;

  /// <remarks />
  public event CreateManagedFolderCompletedEventHandler CreateManagedFolderCompleted;

  /// <remarks />
  public event GetItemCompletedEventHandler GetItemCompleted;

  /// <remarks />
  public event CreateItemCompletedEventHandler CreateItemCompleted;

  /// <remarks />
  public event DeleteItemCompletedEventHandler DeleteItemCompleted;

  /// <remarks />
  public event UpdateItemCompletedEventHandler UpdateItemCompleted;

  /// <remarks />
  public event UpdateItemInRecoverableItemsCompletedEventHandler UpdateItemInRecoverableItemsCompleted;

  /// <remarks />
  public event SendItemCompletedEventHandler SendItemCompleted;

  /// <remarks />
  public event MoveItemCompletedEventHandler MoveItemCompleted;

  /// <remarks />
  public event CopyItemCompletedEventHandler CopyItemCompleted;

  /// <remarks />
  public event ArchiveItemCompletedEventHandler ArchiveItemCompleted;

  /// <remarks />
  public event CreateAttachmentCompletedEventHandler CreateAttachmentCompleted;

  /// <remarks />
  public event DeleteAttachmentCompletedEventHandler DeleteAttachmentCompleted;

  /// <remarks />
  public event GetAttachmentCompletedEventHandler GetAttachmentCompleted;

  /// <remarks />
  public event GetClientAccessTokenCompletedEventHandler GetClientAccessTokenCompleted;

  /// <remarks />
  public event GetDelegateCompletedEventHandler GetDelegateCompleted;

  /// <remarks />
  public event AddDelegateCompletedEventHandler AddDelegateCompleted;

  /// <remarks />
  public event RemoveDelegateCompletedEventHandler RemoveDelegateCompleted;

  /// <remarks />
  public event UpdateDelegateCompletedEventHandler UpdateDelegateCompleted;

  /// <remarks />
  public event CreateUserConfigurationCompletedEventHandler CreateUserConfigurationCompleted;

  /// <remarks />
  public event DeleteUserConfigurationCompletedEventHandler DeleteUserConfigurationCompleted;

  /// <remarks />
  public event GetUserConfigurationCompletedEventHandler GetUserConfigurationCompleted;

  /// <remarks />
  public event UpdateUserConfigurationCompletedEventHandler UpdateUserConfigurationCompleted;

  /// <remarks />
  public event GetUserAvailabilityCompletedEventHandler GetUserAvailabilityCompleted;

  /// <remarks />
  public event GetUserOofSettingsCompletedEventHandler GetUserOofSettingsCompleted;

  /// <remarks />
  public event SetUserOofSettingsCompletedEventHandler SetUserOofSettingsCompleted;

  /// <remarks />
  public event GetServiceConfigurationCompletedEventHandler GetServiceConfigurationCompleted;

  /// <remarks />
  public event GetMailTipsCompletedEventHandler GetMailTipsCompleted;

  /// <remarks />
  public event PlayOnPhoneCompletedEventHandler PlayOnPhoneCompleted;

  /// <remarks />
  public event GetPhoneCallInformationCompletedEventHandler GetPhoneCallInformationCompleted;

  /// <remarks />
  public event DisconnectPhoneCallCompletedEventHandler DisconnectPhoneCallCompleted;

  /// <remarks />
  public event GetSharingMetadataCompletedEventHandler GetSharingMetadataCompleted;

  /// <remarks />
  public event RefreshSharingFolderCompletedEventHandler RefreshSharingFolderCompleted;

  /// <remarks />
  public event GetSharingFolderCompletedEventHandler GetSharingFolderCompleted;

  /// <remarks />
  public event SetTeamMailboxCompletedEventHandler SetTeamMailboxCompleted;

  /// <remarks />
  public event UnpinTeamMailboxCompletedEventHandler UnpinTeamMailboxCompleted;

  /// <remarks />
  public event GetRoomListsCompletedEventHandler GetRoomListsCompleted;

  /// <remarks />
  public event GetRoomsCompletedEventHandler GetRoomsCompleted;

  /// <remarks />
  public event FindMessageTrackingReportCompletedEventHandler FindMessageTrackingReportCompleted;

  /// <remarks />
  public event GetMessageTrackingReportCompletedEventHandler GetMessageTrackingReportCompleted;

  /// <remarks />
  public event FindConversationCompletedEventHandler FindConversationCompleted;

  /// <remarks />
  public event ApplyConversationActionCompletedEventHandler ApplyConversationActionCompleted;

  /// <remarks />
  public event GetConversationItemsCompletedEventHandler GetConversationItemsCompleted;

  /// <remarks />
  public event FindPeopleCompletedEventHandler FindPeopleCompleted;

  /// <remarks />
  public event GetPersonaCompletedEventHandler GetPersonaCompleted;

  /// <remarks />
  public event GetInboxRulesCompletedEventHandler GetInboxRulesCompleted;

  /// <remarks />
  public event UpdateInboxRulesCompletedEventHandler UpdateInboxRulesCompleted;

  /// <remarks />
  public event GetPasswordExpirationDateCompletedEventHandler GetPasswordExpirationDateCompleted;

  /// <remarks />
  public event GetSearchableMailboxesCompletedEventHandler GetSearchableMailboxesCompleted;

  /// <remarks />
  public event SearchMailboxesCompletedEventHandler SearchMailboxesCompleted;

  /// <remarks />
  public event GetDiscoverySearchConfigurationCompletedEventHandler GetDiscoverySearchConfigurationCompleted;

  /// <remarks />
  public event GetHoldOnMailboxesCompletedEventHandler GetHoldOnMailboxesCompleted;

  /// <remarks />
  public event SetHoldOnMailboxesCompletedEventHandler SetHoldOnMailboxesCompleted;

  /// <remarks />
  public event GetNonIndexableItemStatisticsCompletedEventHandler GetNonIndexableItemStatisticsCompleted;

  /// <remarks />
  public event GetNonIndexableItemDetailsCompletedEventHandler GetNonIndexableItemDetailsCompleted;

  /// <remarks />
  public event MarkAllItemsAsReadCompletedEventHandler MarkAllItemsAsReadCompleted;

  /// <remarks />
  public event MarkAsJunkCompletedEventHandler MarkAsJunkCompleted;

  /// <remarks />
  public event GetAppManifestsCompletedEventHandler GetAppManifestsCompleted;

  /// <remarks />
  public event AddNewImContactToGroupCompletedEventHandler AddNewImContactToGroupCompleted;

  /// <remarks />
  public event AddNewTelUriContactToGroupCompletedEventHandler AddNewTelUriContactToGroupCompleted;

  /// <remarks />
  public event AddImContactToGroupCompletedEventHandler AddImContactToGroupCompleted;

  /// <remarks />
  public event RemoveImContactFromGroupCompletedEventHandler RemoveImContactFromGroupCompleted;

  /// <remarks />
  public event AddImGroupCompletedEventHandler AddImGroupCompleted;

  /// <remarks />
  public event AddDistributionGroupToImListCompletedEventHandler AddDistributionGroupToImListCompleted;

  /// <remarks />
  public event GetImItemListCompletedEventHandler GetImItemListCompleted;

  /// <remarks />
  public event GetImItemsCompletedEventHandler GetImItemsCompleted;

  /// <remarks />
  public event RemoveContactFromImListCompletedEventHandler RemoveContactFromImListCompleted;

  /// <remarks />
  public event RemoveDistributionGroupFromImListCompletedEventHandler RemoveDistributionGroupFromImListCompleted;

  /// <remarks />
  public event RemoveImGroupCompletedEventHandler RemoveImGroupCompleted;

  /// <remarks />
  public event SetImGroupCompletedEventHandler SetImGroupCompleted;

  /// <remarks />
  public event SetImListMigrationCompletedCompletedEventHandler SetImListMigrationCompletedCompleted;

  /// <remarks />
  public event GetUserRetentionPolicyTagsCompletedEventHandler GetUserRetentionPolicyTagsCompleted;

  /// <remarks />
  public event InstallAppCompletedEventHandler InstallAppCompleted;

  /// <remarks />
  public event UninstallAppCompletedEventHandler UninstallAppCompleted;

  /// <remarks />
  public event DisableAppCompletedEventHandler DisableAppCompleted;

  /// <remarks />
  public event GetAppMarketplaceUrlCompletedEventHandler GetAppMarketplaceUrlCompleted;

  /// <remarks />
  public event GetUserPhotoCompletedEventHandler GetUserPhotoCompleted;

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ResolveNames", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("ResolveNamesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public ResolveNamesResponseType ResolveNames([XmlElement("ResolveNames", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ResolveNamesType ResolveNames1)
  {
    return (ResolveNamesResponseType) this.Invoke(nameof (ResolveNames), new object[1]
    {
      (object) ResolveNames1
    })[0];
  }

  /// <remarks />
  public void ResolveNamesAsync(ResolveNamesType ResolveNames1)
  {
    this.ResolveNamesAsync(ResolveNames1, (object) null);
  }

  /// <remarks />
  public void ResolveNamesAsync(ResolveNamesType ResolveNames1, object userState)
  {
    if (this.ResolveNamesOperationCompleted == null)
      this.ResolveNamesOperationCompleted = new SendOrPostCallback(this.OnResolveNamesOperationCompleted);
    this.InvokeAsync("ResolveNames", new object[1]
    {
      (object) ResolveNames1
    }, this.ResolveNamesOperationCompleted, userState);
  }

  private void OnResolveNamesOperationCompleted(object arg)
  {
    if (this.ResolveNamesCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.ResolveNamesCompleted((object) this, new ResolveNamesCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ExpandDL", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("ExpandDLResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public ExpandDLResponseType ExpandDL([XmlElement("ExpandDL", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ExpandDLType ExpandDL1)
  {
    return (ExpandDLResponseType) this.Invoke(nameof (ExpandDL), new object[1]
    {
      (object) ExpandDL1
    })[0];
  }

  /// <remarks />
  public void ExpandDLAsync(ExpandDLType ExpandDL1) => this.ExpandDLAsync(ExpandDL1, (object) null);

  /// <remarks />
  public void ExpandDLAsync(ExpandDLType ExpandDL1, object userState)
  {
    if (this.ExpandDLOperationCompleted == null)
      this.ExpandDLOperationCompleted = new SendOrPostCallback(this.OnExpandDLOperationCompleted);
    this.InvokeAsync("ExpandDL", new object[1]
    {
      (object) ExpandDL1
    }, this.ExpandDLOperationCompleted, userState);
  }

  private void OnExpandDLOperationCompleted(object arg)
  {
    if (this.ExpandDLCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.ExpandDLCompleted((object) this, new ExpandDLCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ExchangeImpersonation")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetServerTimeZones", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetServerTimeZonesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetServerTimeZonesResponseType GetServerTimeZones(
    [XmlElement("GetServerTimeZones", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetServerTimeZonesType GetServerTimeZones1)
  {
    return (GetServerTimeZonesResponseType) this.Invoke(nameof (GetServerTimeZones), new object[1]
    {
      (object) GetServerTimeZones1
    })[0];
  }

  /// <remarks />
  public void GetServerTimeZonesAsync(GetServerTimeZonesType GetServerTimeZones1)
  {
    this.GetServerTimeZonesAsync(GetServerTimeZones1, (object) null);
  }

  /// <remarks />
  public void GetServerTimeZonesAsync(GetServerTimeZonesType GetServerTimeZones1, object userState)
  {
    if (this.GetServerTimeZonesOperationCompleted == null)
      this.GetServerTimeZonesOperationCompleted = new SendOrPostCallback(this.OnGetServerTimeZonesOperationCompleted);
    this.InvokeAsync("GetServerTimeZones", new object[1]
    {
      (object) GetServerTimeZones1
    }, this.GetServerTimeZonesOperationCompleted, userState);
  }

  private void OnGetServerTimeZonesOperationCompleted(object arg)
  {
    if (this.GetServerTimeZonesCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetServerTimeZonesCompleted((object) this, new GetServerTimeZonesCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("TimeZoneContext")]
  [SoapHeader("ExchangeImpersonation")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ManagementRole")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/FindFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("FindFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public FindFolderResponseType FindFolder([XmlElement("FindFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] FindFolderType FindFolder1)
  {
    return (FindFolderResponseType) this.Invoke(nameof (FindFolder), new object[1]
    {
      (object) FindFolder1
    })[0];
  }

  /// <remarks />
  public void FindFolderAsync(FindFolderType FindFolder1)
  {
    this.FindFolderAsync(FindFolder1, (object) null);
  }

  /// <remarks />
  public void FindFolderAsync(FindFolderType FindFolder1, object userState)
  {
    if (this.FindFolderOperationCompleted == null)
      this.FindFolderOperationCompleted = new SendOrPostCallback(this.OnFindFolderOperationCompleted);
    this.InvokeAsync("FindFolder", new object[1]
    {
      (object) FindFolder1
    }, this.FindFolderOperationCompleted, userState);
  }

  private void OnFindFolderOperationCompleted(object arg)
  {
    if (this.FindFolderCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.FindFolderCompleted((object) this, new FindFolderCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("TimeZoneContext")]
  [SoapHeader("ExchangeImpersonation")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ManagementRole")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("DateTimePrecision")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/FindItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("FindItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public FindItemResponseType FindItem([XmlElement("FindItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] FindItemType FindItem1)
  {
    return (FindItemResponseType) this.Invoke(nameof (FindItem), new object[1]
    {
      (object) FindItem1
    })[0];
  }

  /// <remarks />
  public void FindItemAsync(FindItemType FindItem1) => this.FindItemAsync(FindItem1, (object) null);

  /// <remarks />
  public void FindItemAsync(FindItemType FindItem1, object userState)
  {
    if (this.FindItemOperationCompleted == null)
      this.FindItemOperationCompleted = new SendOrPostCallback(this.OnFindItemOperationCompleted);
    this.InvokeAsync("FindItem", new object[1]
    {
      (object) FindItem1
    }, this.FindItemOperationCompleted, userState);
  }

  private void OnFindItemOperationCompleted(object arg)
  {
    if (this.FindItemCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.FindItemCompleted((object) this, new FindItemCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("TimeZoneContext")]
  [SoapHeader("ExchangeImpersonation")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ManagementRole")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetFolderResponseType GetFolder([XmlElement("GetFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetFolderType GetFolder1)
  {
    return (GetFolderResponseType) this.Invoke(nameof (GetFolder), new object[1]
    {
      (object) GetFolder1
    })[0];
  }

  /// <remarks />
  public void GetFolderAsync(GetFolderType GetFolder1)
  {
    this.GetFolderAsync(GetFolder1, (object) null);
  }

  /// <remarks />
  public void GetFolderAsync(GetFolderType GetFolder1, object userState)
  {
    if (this.GetFolderOperationCompleted == null)
      this.GetFolderOperationCompleted = new SendOrPostCallback(this.OnGetFolderOperationCompleted);
    this.InvokeAsync("GetFolder", new object[1]
    {
      (object) GetFolder1
    }, this.GetFolderOperationCompleted, userState);
  }

  private void OnGetFolderOperationCompleted(object arg)
  {
    if (this.GetFolderCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetFolderCompleted((object) this, new GetFolderCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UploadItems", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("UploadItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public UploadItemsResponseType UploadItems([XmlElement("UploadItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UploadItemsType UploadItems1)
  {
    return (UploadItemsResponseType) this.Invoke(nameof (UploadItems), new object[1]
    {
      (object) UploadItems1
    })[0];
  }

  /// <remarks />
  public void UploadItemsAsync(UploadItemsType UploadItems1)
  {
    this.UploadItemsAsync(UploadItems1, (object) null);
  }

  /// <remarks />
  public void UploadItemsAsync(UploadItemsType UploadItems1, object userState)
  {
    if (this.UploadItemsOperationCompleted == null)
      this.UploadItemsOperationCompleted = new SendOrPostCallback(this.OnUploadItemsOperationCompleted);
    this.InvokeAsync("UploadItems", new object[1]
    {
      (object) UploadItems1
    }, this.UploadItemsOperationCompleted, userState);
  }

  private void OnUploadItemsOperationCompleted(object arg)
  {
    if (this.UploadItemsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.UploadItemsCompleted((object) this, new UploadItemsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ManagementRole")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ExportItems", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("ExportItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public ExportItemsResponseType ExportItems([XmlElement("ExportItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ExportItemsType ExportItems1)
  {
    return (ExportItemsResponseType) this.Invoke(nameof (ExportItems), new object[1]
    {
      (object) ExportItems1
    })[0];
  }

  /// <remarks />
  public void ExportItemsAsync(ExportItemsType ExportItems1)
  {
    this.ExportItemsAsync(ExportItems1, (object) null);
  }

  /// <remarks />
  public void ExportItemsAsync(ExportItemsType ExportItems1, object userState)
  {
    if (this.ExportItemsOperationCompleted == null)
      this.ExportItemsOperationCompleted = new SendOrPostCallback(this.OnExportItemsOperationCompleted);
    this.InvokeAsync("ExportItems", new object[1]
    {
      (object) ExportItems1
    }, this.ExportItemsOperationCompleted, userState);
  }

  private void OnExportItemsOperationCompleted(object arg)
  {
    if (this.ExportItemsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.ExportItemsCompleted((object) this, new ExportItemsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ConvertId", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("ConvertIdResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public ConvertIdResponseType ConvertId([XmlElement("ConvertId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ConvertIdType ConvertId1)
  {
    return (ConvertIdResponseType) this.Invoke(nameof (ConvertId), new object[1]
    {
      (object) ConvertId1
    })[0];
  }

  /// <remarks />
  public void ConvertIdAsync(ConvertIdType ConvertId1)
  {
    this.ConvertIdAsync(ConvertId1, (object) null);
  }

  /// <remarks />
  public void ConvertIdAsync(ConvertIdType ConvertId1, object userState)
  {
    if (this.ConvertIdOperationCompleted == null)
      this.ConvertIdOperationCompleted = new SendOrPostCallback(this.OnConvertIdOperationCompleted);
    this.InvokeAsync("ConvertId", new object[1]
    {
      (object) ConvertId1
    }, this.ConvertIdOperationCompleted, userState);
  }

  private void OnConvertIdOperationCompleted(object arg)
  {
    if (this.ConvertIdCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.ConvertIdCompleted((object) this, new ConvertIdCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("TimeZoneContext")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("CreateFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public CreateFolderResponseType CreateFolder([XmlElement("CreateFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateFolderType CreateFolder1)
  {
    return (CreateFolderResponseType) this.Invoke(nameof (CreateFolder), new object[1]
    {
      (object) CreateFolder1
    })[0];
  }

  /// <remarks />
  public void CreateFolderAsync(CreateFolderType CreateFolder1)
  {
    this.CreateFolderAsync(CreateFolder1, (object) null);
  }

  /// <remarks />
  public void CreateFolderAsync(CreateFolderType CreateFolder1, object userState)
  {
    if (this.CreateFolderOperationCompleted == null)
      this.CreateFolderOperationCompleted = new SendOrPostCallback(this.OnCreateFolderOperationCompleted);
    this.InvokeAsync("CreateFolder", new object[1]
    {
      (object) CreateFolder1
    }, this.CreateFolderOperationCompleted, userState);
  }

  private void OnCreateFolderOperationCompleted(object arg)
  {
    if (this.CreateFolderCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.CreateFolderCompleted((object) this, new CreateFolderCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("TimeZoneContext")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateFolderPath", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("CreateFolderPathResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public CreateFolderPathResponseType CreateFolderPath([XmlElement("CreateFolderPath", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateFolderPathType CreateFolderPath1)
  {
    return (CreateFolderPathResponseType) this.Invoke(nameof (CreateFolderPath), new object[1]
    {
      (object) CreateFolderPath1
    })[0];
  }

  /// <remarks />
  public void CreateFolderPathAsync(CreateFolderPathType CreateFolderPath1)
  {
    this.CreateFolderPathAsync(CreateFolderPath1, (object) null);
  }

  /// <remarks />
  public void CreateFolderPathAsync(CreateFolderPathType CreateFolderPath1, object userState)
  {
    if (this.CreateFolderPathOperationCompleted == null)
      this.CreateFolderPathOperationCompleted = new SendOrPostCallback(this.OnCreateFolderPathOperationCompleted);
    this.InvokeAsync("CreateFolderPath", new object[1]
    {
      (object) CreateFolderPath1
    }, this.CreateFolderPathOperationCompleted, userState);
  }

  private void OnCreateFolderPathOperationCompleted(object arg)
  {
    if (this.CreateFolderPathCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.CreateFolderPathCompleted((object) this, new CreateFolderPathCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DeleteFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("DeleteFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public DeleteFolderResponseType DeleteFolder([XmlElement("DeleteFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DeleteFolderType DeleteFolder1)
  {
    return (DeleteFolderResponseType) this.Invoke(nameof (DeleteFolder), new object[1]
    {
      (object) DeleteFolder1
    })[0];
  }

  /// <remarks />
  public void DeleteFolderAsync(DeleteFolderType DeleteFolder1)
  {
    this.DeleteFolderAsync(DeleteFolder1, (object) null);
  }

  /// <remarks />
  public void DeleteFolderAsync(DeleteFolderType DeleteFolder1, object userState)
  {
    if (this.DeleteFolderOperationCompleted == null)
      this.DeleteFolderOperationCompleted = new SendOrPostCallback(this.OnDeleteFolderOperationCompleted);
    this.InvokeAsync("DeleteFolder", new object[1]
    {
      (object) DeleteFolder1
    }, this.DeleteFolderOperationCompleted, userState);
  }

  private void OnDeleteFolderOperationCompleted(object arg)
  {
    if (this.DeleteFolderCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.DeleteFolderCompleted((object) this, new DeleteFolderCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/EmptyFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("EmptyFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public EmptyFolderResponseType EmptyFolder([XmlElement("EmptyFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] EmptyFolderType EmptyFolder1)
  {
    return (EmptyFolderResponseType) this.Invoke(nameof (EmptyFolder), new object[1]
    {
      (object) EmptyFolder1
    })[0];
  }

  /// <remarks />
  public void EmptyFolderAsync(EmptyFolderType EmptyFolder1)
  {
    this.EmptyFolderAsync(EmptyFolder1, (object) null);
  }

  /// <remarks />
  public void EmptyFolderAsync(EmptyFolderType EmptyFolder1, object userState)
  {
    if (this.EmptyFolderOperationCompleted == null)
      this.EmptyFolderOperationCompleted = new SendOrPostCallback(this.OnEmptyFolderOperationCompleted);
    this.InvokeAsync("EmptyFolder", new object[1]
    {
      (object) EmptyFolder1
    }, this.EmptyFolderOperationCompleted, userState);
  }

  private void OnEmptyFolderOperationCompleted(object arg)
  {
    if (this.EmptyFolderCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.EmptyFolderCompleted((object) this, new EmptyFolderCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("TimeZoneContext")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("UpdateFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public UpdateFolderResponseType UpdateFolder([XmlElement("UpdateFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateFolderType UpdateFolder1)
  {
    return (UpdateFolderResponseType) this.Invoke(nameof (UpdateFolder), new object[1]
    {
      (object) UpdateFolder1
    })[0];
  }

  /// <remarks />
  public void UpdateFolderAsync(UpdateFolderType UpdateFolder1)
  {
    this.UpdateFolderAsync(UpdateFolder1, (object) null);
  }

  /// <remarks />
  public void UpdateFolderAsync(UpdateFolderType UpdateFolder1, object userState)
  {
    if (this.UpdateFolderOperationCompleted == null)
      this.UpdateFolderOperationCompleted = new SendOrPostCallback(this.OnUpdateFolderOperationCompleted);
    this.InvokeAsync("UpdateFolder", new object[1]
    {
      (object) UpdateFolder1
    }, this.UpdateFolderOperationCompleted, userState);
  }

  private void OnUpdateFolderOperationCompleted(object arg)
  {
    if (this.UpdateFolderCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.UpdateFolderCompleted((object) this, new UpdateFolderCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/MoveFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("MoveFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public MoveFolderResponseType MoveFolder([XmlElement("MoveFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] MoveFolderType MoveFolder1)
  {
    return (MoveFolderResponseType) this.Invoke(nameof (MoveFolder), new object[1]
    {
      (object) MoveFolder1
    })[0];
  }

  /// <remarks />
  public void MoveFolderAsync(MoveFolderType MoveFolder1)
  {
    this.MoveFolderAsync(MoveFolder1, (object) null);
  }

  /// <remarks />
  public void MoveFolderAsync(MoveFolderType MoveFolder1, object userState)
  {
    if (this.MoveFolderOperationCompleted == null)
      this.MoveFolderOperationCompleted = new SendOrPostCallback(this.OnMoveFolderOperationCompleted);
    this.InvokeAsync("MoveFolder", new object[1]
    {
      (object) MoveFolder1
    }, this.MoveFolderOperationCompleted, userState);
  }

  private void OnMoveFolderOperationCompleted(object arg)
  {
    if (this.MoveFolderCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.MoveFolderCompleted((object) this, new MoveFolderCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CopyFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("CopyFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public CopyFolderResponseType CopyFolder([XmlElement("CopyFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CopyFolderType CopyFolder1)
  {
    return (CopyFolderResponseType) this.Invoke(nameof (CopyFolder), new object[1]
    {
      (object) CopyFolder1
    })[0];
  }

  /// <remarks />
  public void CopyFolderAsync(CopyFolderType CopyFolder1)
  {
    this.CopyFolderAsync(CopyFolder1, (object) null);
  }

  /// <remarks />
  public void CopyFolderAsync(CopyFolderType CopyFolder1, object userState)
  {
    if (this.CopyFolderOperationCompleted == null)
      this.CopyFolderOperationCompleted = new SendOrPostCallback(this.OnCopyFolderOperationCompleted);
    this.InvokeAsync("CopyFolder", new object[1]
    {
      (object) CopyFolder1
    }, this.CopyFolderOperationCompleted, userState);
  }

  private void OnCopyFolderOperationCompleted(object arg)
  {
    if (this.CopyFolderCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.CopyFolderCompleted((object) this, new CopyFolderCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/Subscribe", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("SubscribeResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public SubscribeResponseType Subscribe([XmlElement("Subscribe", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SubscribeType Subscribe1)
  {
    return (SubscribeResponseType) this.Invoke(nameof (Subscribe), new object[1]
    {
      (object) Subscribe1
    })[0];
  }

  /// <remarks />
  public void SubscribeAsync(SubscribeType Subscribe1)
  {
    this.SubscribeAsync(Subscribe1, (object) null);
  }

  /// <remarks />
  public void SubscribeAsync(SubscribeType Subscribe1, object userState)
  {
    if (this.SubscribeOperationCompleted == null)
      this.SubscribeOperationCompleted = new SendOrPostCallback(this.OnSubscribeOperationCompleted);
    this.InvokeAsync("Subscribe", new object[1]
    {
      (object) Subscribe1
    }, this.SubscribeOperationCompleted, userState);
  }

  private void OnSubscribeOperationCompleted(object arg)
  {
    if (this.SubscribeCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.SubscribeCompleted((object) this, new SubscribeCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/Unsubscribe", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("UnsubscribeResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public UnsubscribeResponseType Unsubscribe([XmlElement("Unsubscribe", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UnsubscribeType Unsubscribe1)
  {
    return (UnsubscribeResponseType) this.Invoke(nameof (Unsubscribe), new object[1]
    {
      (object) Unsubscribe1
    })[0];
  }

  /// <remarks />
  public void UnsubscribeAsync(UnsubscribeType Unsubscribe1)
  {
    this.UnsubscribeAsync(Unsubscribe1, (object) null);
  }

  /// <remarks />
  public void UnsubscribeAsync(UnsubscribeType Unsubscribe1, object userState)
  {
    if (this.UnsubscribeOperationCompleted == null)
      this.UnsubscribeOperationCompleted = new SendOrPostCallback(this.OnUnsubscribeOperationCompleted);
    this.InvokeAsync("Unsubscribe", new object[1]
    {
      (object) Unsubscribe1
    }, this.UnsubscribeOperationCompleted, userState);
  }

  private void OnUnsubscribeOperationCompleted(object arg)
  {
    if (this.UnsubscribeCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.UnsubscribeCompleted((object) this, new UnsubscribeCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetEvents", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetEventsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetEventsResponseType GetEvents([XmlElement("GetEvents", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetEventsType GetEvents1)
  {
    return (GetEventsResponseType) this.Invoke(nameof (GetEvents), new object[1]
    {
      (object) GetEvents1
    })[0];
  }

  /// <remarks />
  public void GetEventsAsync(GetEventsType GetEvents1)
  {
    this.GetEventsAsync(GetEvents1, (object) null);
  }

  /// <remarks />
  public void GetEventsAsync(GetEventsType GetEvents1, object userState)
  {
    if (this.GetEventsOperationCompleted == null)
      this.GetEventsOperationCompleted = new SendOrPostCallback(this.OnGetEventsOperationCompleted);
    this.InvokeAsync("GetEvents", new object[1]
    {
      (object) GetEvents1
    }, this.GetEventsOperationCompleted, userState);
  }

  private void OnGetEventsOperationCompleted(object arg)
  {
    if (this.GetEventsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetEventsCompleted((object) this, new GetEventsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetEvents", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetStreamingEventsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetStreamingEventsResponseType GetStreamingEvents(
    [XmlElement("GetStreamingEvents", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetStreamingEventsType GetStreamingEvents1)
  {
    return (GetStreamingEventsResponseType) this.Invoke(nameof (GetStreamingEvents), new object[1]
    {
      (object) GetStreamingEvents1
    })[0];
  }

  /// <remarks />
  public void GetStreamingEventsAsync(GetStreamingEventsType GetStreamingEvents1)
  {
    this.GetStreamingEventsAsync(GetStreamingEvents1, (object) null);
  }

  /// <remarks />
  public void GetStreamingEventsAsync(GetStreamingEventsType GetStreamingEvents1, object userState)
  {
    if (this.GetStreamingEventsOperationCompleted == null)
      this.GetStreamingEventsOperationCompleted = new SendOrPostCallback(this.OnGetStreamingEventsOperationCompleted);
    this.InvokeAsync("GetStreamingEvents", new object[1]
    {
      (object) GetStreamingEvents1
    }, this.GetStreamingEventsOperationCompleted, userState);
  }

  private void OnGetStreamingEventsOperationCompleted(object arg)
  {
    if (this.GetStreamingEventsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetStreamingEventsCompleted((object) this, new GetStreamingEventsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SyncFolderHierarchy", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("SyncFolderHierarchyResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public SyncFolderHierarchyResponseType SyncFolderHierarchy(
    [XmlElement("SyncFolderHierarchy", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SyncFolderHierarchyType SyncFolderHierarchy1)
  {
    return (SyncFolderHierarchyResponseType) this.Invoke(nameof (SyncFolderHierarchy), new object[1]
    {
      (object) SyncFolderHierarchy1
    })[0];
  }

  /// <remarks />
  public void SyncFolderHierarchyAsync(SyncFolderHierarchyType SyncFolderHierarchy1)
  {
    this.SyncFolderHierarchyAsync(SyncFolderHierarchy1, (object) null);
  }

  /// <remarks />
  public void SyncFolderHierarchyAsync(
    SyncFolderHierarchyType SyncFolderHierarchy1,
    object userState)
  {
    if (this.SyncFolderHierarchyOperationCompleted == null)
      this.SyncFolderHierarchyOperationCompleted = new SendOrPostCallback(this.OnSyncFolderHierarchyOperationCompleted);
    this.InvokeAsync("SyncFolderHierarchy", new object[1]
    {
      (object) SyncFolderHierarchy1
    }, this.SyncFolderHierarchyOperationCompleted, userState);
  }

  private void OnSyncFolderHierarchyOperationCompleted(object arg)
  {
    if (this.SyncFolderHierarchyCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.SyncFolderHierarchyCompleted((object) this, new SyncFolderHierarchyCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SyncFolderItems", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("SyncFolderItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public SyncFolderItemsResponseType SyncFolderItems([XmlElement("SyncFolderItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SyncFolderItemsType SyncFolderItems1)
  {
    return (SyncFolderItemsResponseType) this.Invoke(nameof (SyncFolderItems), new object[1]
    {
      (object) SyncFolderItems1
    })[0];
  }

  /// <remarks />
  public void SyncFolderItemsAsync(SyncFolderItemsType SyncFolderItems1)
  {
    this.SyncFolderItemsAsync(SyncFolderItems1, (object) null);
  }

  /// <remarks />
  public void SyncFolderItemsAsync(SyncFolderItemsType SyncFolderItems1, object userState)
  {
    if (this.SyncFolderItemsOperationCompleted == null)
      this.SyncFolderItemsOperationCompleted = new SendOrPostCallback(this.OnSyncFolderItemsOperationCompleted);
    this.InvokeAsync("SyncFolderItems", new object[1]
    {
      (object) SyncFolderItems1
    }, this.SyncFolderItemsOperationCompleted, userState);
  }

  private void OnSyncFolderItemsOperationCompleted(object arg)
  {
    if (this.SyncFolderItemsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.SyncFolderItemsCompleted((object) this, new SyncFolderItemsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateManagedFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("CreateManagedFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public CreateManagedFolderResponseType CreateManagedFolder(
    [XmlElement("CreateManagedFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateManagedFolderRequestType CreateManagedFolder1)
  {
    return (CreateManagedFolderResponseType) this.Invoke(nameof (CreateManagedFolder), new object[1]
    {
      (object) CreateManagedFolder1
    })[0];
  }

  /// <remarks />
  public void CreateManagedFolderAsync(
    CreateManagedFolderRequestType CreateManagedFolder1)
  {
    this.CreateManagedFolderAsync(CreateManagedFolder1, (object) null);
  }

  /// <remarks />
  public void CreateManagedFolderAsync(
    CreateManagedFolderRequestType CreateManagedFolder1,
    object userState)
  {
    if (this.CreateManagedFolderOperationCompleted == null)
      this.CreateManagedFolderOperationCompleted = new SendOrPostCallback(this.OnCreateManagedFolderOperationCompleted);
    this.InvokeAsync("CreateManagedFolder", new object[1]
    {
      (object) CreateManagedFolder1
    }, this.CreateManagedFolderOperationCompleted, userState);
  }

  private void OnCreateManagedFolderOperationCompleted(object arg)
  {
    if (this.CreateManagedFolderCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.CreateManagedFolderCompleted((object) this, new CreateManagedFolderCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("TimeZoneContext")]
  [SoapHeader("ExchangeImpersonation")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ManagementRole")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("DateTimePrecision")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetItemResponseType GetItem([XmlElement("GetItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetItemType GetItem1)
  {
    return (GetItemResponseType) this.Invoke(nameof (GetItem), new object[1]
    {
      (object) GetItem1
    })[0];
  }

  /// <remarks />
  public void GetItemAsync(GetItemType GetItem1) => this.GetItemAsync(GetItem1, (object) null);

  /// <remarks />
  public void GetItemAsync(GetItemType GetItem1, object userState)
  {
    if (this.GetItemOperationCompleted == null)
      this.GetItemOperationCompleted = new SendOrPostCallback(this.OnGetItemOperationCompleted);
    this.InvokeAsync("GetItem", new object[1]
    {
      (object) GetItem1
    }, this.GetItemOperationCompleted, userState);
  }

  private void OnGetItemOperationCompleted(object arg)
  {
    if (this.GetItemCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetItemCompleted((object) this, new GetItemCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("TimeZoneContext")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("CreateItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public CreateItemResponseType CreateItem([XmlElement("CreateItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateItemType CreateItem1)
  {
    return (CreateItemResponseType) this.Invoke(nameof (CreateItem), new object[1]
    {
      (object) CreateItem1
    })[0];
  }

  /// <remarks />
  public void CreateItemAsync(CreateItemType CreateItem1)
  {
    this.CreateItemAsync(CreateItem1, (object) null);
  }

  /// <remarks />
  public void CreateItemAsync(CreateItemType CreateItem1, object userState)
  {
    if (this.CreateItemOperationCompleted == null)
      this.CreateItemOperationCompleted = new SendOrPostCallback(this.OnCreateItemOperationCompleted);
    this.InvokeAsync("CreateItem", new object[1]
    {
      (object) CreateItem1
    }, this.CreateItemOperationCompleted, userState);
  }

  private void OnCreateItemOperationCompleted(object arg)
  {
    if (this.CreateItemCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.CreateItemCompleted((object) this, new CreateItemCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DeleteItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("DeleteItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public DeleteItemResponseType DeleteItem([XmlElement("DeleteItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DeleteItemType DeleteItem1)
  {
    return (DeleteItemResponseType) this.Invoke(nameof (DeleteItem), new object[1]
    {
      (object) DeleteItem1
    })[0];
  }

  /// <remarks />
  public void DeleteItemAsync(DeleteItemType DeleteItem1)
  {
    this.DeleteItemAsync(DeleteItem1, (object) null);
  }

  /// <remarks />
  public void DeleteItemAsync(DeleteItemType DeleteItem1, object userState)
  {
    if (this.DeleteItemOperationCompleted == null)
      this.DeleteItemOperationCompleted = new SendOrPostCallback(this.OnDeleteItemOperationCompleted);
    this.InvokeAsync("DeleteItem", new object[1]
    {
      (object) DeleteItem1
    }, this.DeleteItemOperationCompleted, userState);
  }

  private void OnDeleteItemOperationCompleted(object arg)
  {
    if (this.DeleteItemCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.DeleteItemCompleted((object) this, new DeleteItemCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("TimeZoneContext")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("UpdateItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public UpdateItemResponseType UpdateItem([XmlElement("UpdateItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateItemType UpdateItem1)
  {
    return (UpdateItemResponseType) this.Invoke(nameof (UpdateItem), new object[1]
    {
      (object) UpdateItem1
    })[0];
  }

  /// <remarks />
  public void UpdateItemAsync(UpdateItemType UpdateItem1)
  {
    this.UpdateItemAsync(UpdateItem1, (object) null);
  }

  /// <remarks />
  public void UpdateItemAsync(UpdateItemType UpdateItem1, object userState)
  {
    if (this.UpdateItemOperationCompleted == null)
      this.UpdateItemOperationCompleted = new SendOrPostCallback(this.OnUpdateItemOperationCompleted);
    this.InvokeAsync("UpdateItem", new object[1]
    {
      (object) UpdateItem1
    }, this.UpdateItemOperationCompleted, userState);
  }

  private void OnUpdateItemOperationCompleted(object arg)
  {
    if (this.UpdateItemCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.UpdateItemCompleted((object) this, new UpdateItemCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("TimeZoneContext")]
  [SoapHeader("ExchangeImpersonation")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ManagementRole")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateItemInRecoverableItems", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("UpdateItemInRecoverableItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public UpdateItemInRecoverableItemsResponseType UpdateItemInRecoverableItems(
    [XmlElement("UpdateItemInRecoverableItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateItemInRecoverableItemsType UpdateItemInRecoverableItems1)
  {
    return (UpdateItemInRecoverableItemsResponseType) this.Invoke(nameof (UpdateItemInRecoverableItems), new object[1]
    {
      (object) UpdateItemInRecoverableItems1
    })[0];
  }

  /// <remarks />
  public void UpdateItemInRecoverableItemsAsync(
    UpdateItemInRecoverableItemsType UpdateItemInRecoverableItems1)
  {
    this.UpdateItemInRecoverableItemsAsync(UpdateItemInRecoverableItems1, (object) null);
  }

  /// <remarks />
  public void UpdateItemInRecoverableItemsAsync(
    UpdateItemInRecoverableItemsType UpdateItemInRecoverableItems1,
    object userState)
  {
    if (this.UpdateItemInRecoverableItemsOperationCompleted == null)
      this.UpdateItemInRecoverableItemsOperationCompleted = new SendOrPostCallback(this.OnUpdateItemInRecoverableItemsOperationCompleted);
    this.InvokeAsync("UpdateItemInRecoverableItems", new object[1]
    {
      (object) UpdateItemInRecoverableItems1
    }, this.UpdateItemInRecoverableItemsOperationCompleted, userState);
  }

  private void OnUpdateItemInRecoverableItemsOperationCompleted(object arg)
  {
    if (this.UpdateItemInRecoverableItemsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.UpdateItemInRecoverableItemsCompleted((object) this, new UpdateItemInRecoverableItemsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SendItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("SendItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public SendItemResponseType SendItem([XmlElement("SendItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SendItemType SendItem1)
  {
    return (SendItemResponseType) this.Invoke(nameof (SendItem), new object[1]
    {
      (object) SendItem1
    })[0];
  }

  /// <remarks />
  public void SendItemAsync(SendItemType SendItem1) => this.SendItemAsync(SendItem1, (object) null);

  /// <remarks />
  public void SendItemAsync(SendItemType SendItem1, object userState)
  {
    if (this.SendItemOperationCompleted == null)
      this.SendItemOperationCompleted = new SendOrPostCallback(this.OnSendItemOperationCompleted);
    this.InvokeAsync("SendItem", new object[1]
    {
      (object) SendItem1
    }, this.SendItemOperationCompleted, userState);
  }

  private void OnSendItemOperationCompleted(object arg)
  {
    if (this.SendItemCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.SendItemCompleted((object) this, new SendItemCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/MoveItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("MoveItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public MoveItemResponseType MoveItem([XmlElement("MoveItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] MoveItemType MoveItem1)
  {
    return (MoveItemResponseType) this.Invoke(nameof (MoveItem), new object[1]
    {
      (object) MoveItem1
    })[0];
  }

  /// <remarks />
  public void MoveItemAsync(MoveItemType MoveItem1) => this.MoveItemAsync(MoveItem1, (object) null);

  /// <remarks />
  public void MoveItemAsync(MoveItemType MoveItem1, object userState)
  {
    if (this.MoveItemOperationCompleted == null)
      this.MoveItemOperationCompleted = new SendOrPostCallback(this.OnMoveItemOperationCompleted);
    this.InvokeAsync("MoveItem", new object[1]
    {
      (object) MoveItem1
    }, this.MoveItemOperationCompleted, userState);
  }

  private void OnMoveItemOperationCompleted(object arg)
  {
    if (this.MoveItemCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.MoveItemCompleted((object) this, new MoveItemCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CopyItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("CopyItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public CopyItemResponseType CopyItem([XmlElement("CopyItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CopyItemType CopyItem1)
  {
    return (CopyItemResponseType) this.Invoke(nameof (CopyItem), new object[1]
    {
      (object) CopyItem1
    })[0];
  }

  /// <remarks />
  public void CopyItemAsync(CopyItemType CopyItem1) => this.CopyItemAsync(CopyItem1, (object) null);

  /// <remarks />
  public void CopyItemAsync(CopyItemType CopyItem1, object userState)
  {
    if (this.CopyItemOperationCompleted == null)
      this.CopyItemOperationCompleted = new SendOrPostCallback(this.OnCopyItemOperationCompleted);
    this.InvokeAsync("CopyItem", new object[1]
    {
      (object) CopyItem1
    }, this.CopyItemOperationCompleted, userState);
  }

  private void OnCopyItemOperationCompleted(object arg)
  {
    if (this.CopyItemCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.CopyItemCompleted((object) this, new CopyItemCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ArchiveItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("ArchiveItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public ArchiveItemResponseType ArchiveItem([XmlElement("ArchiveItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ArchiveItemType ArchiveItem1)
  {
    return (ArchiveItemResponseType) this.Invoke(nameof (ArchiveItem), new object[1]
    {
      (object) ArchiveItem1
    })[0];
  }

  /// <remarks />
  public void ArchiveItemAsync(ArchiveItemType ArchiveItem1)
  {
    this.ArchiveItemAsync(ArchiveItem1, (object) null);
  }

  /// <remarks />
  public void ArchiveItemAsync(ArchiveItemType ArchiveItem1, object userState)
  {
    if (this.ArchiveItemOperationCompleted == null)
      this.ArchiveItemOperationCompleted = new SendOrPostCallback(this.OnArchiveItemOperationCompleted);
    this.InvokeAsync("ArchiveItem", new object[1]
    {
      (object) ArchiveItem1
    }, this.ArchiveItemOperationCompleted, userState);
  }

  private void OnArchiveItemOperationCompleted(object arg)
  {
    if (this.ArchiveItemCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.ArchiveItemCompleted((object) this, new ArchiveItemCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("TimeZoneContext")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateAttachment", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("CreateAttachmentResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public CreateAttachmentResponseType CreateAttachment([XmlElement("CreateAttachment", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateAttachmentType CreateAttachment1)
  {
    return (CreateAttachmentResponseType) this.Invoke(nameof (CreateAttachment), new object[1]
    {
      (object) CreateAttachment1
    })[0];
  }

  /// <remarks />
  public void CreateAttachmentAsync(CreateAttachmentType CreateAttachment1)
  {
    this.CreateAttachmentAsync(CreateAttachment1, (object) null);
  }

  /// <remarks />
  public void CreateAttachmentAsync(CreateAttachmentType CreateAttachment1, object userState)
  {
    if (this.CreateAttachmentOperationCompleted == null)
      this.CreateAttachmentOperationCompleted = new SendOrPostCallback(this.OnCreateAttachmentOperationCompleted);
    this.InvokeAsync("CreateAttachment", new object[1]
    {
      (object) CreateAttachment1
    }, this.CreateAttachmentOperationCompleted, userState);
  }

  private void OnCreateAttachmentOperationCompleted(object arg)
  {
    if (this.CreateAttachmentCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.CreateAttachmentCompleted((object) this, new CreateAttachmentCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DeleteAttachment", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("DeleteAttachmentResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public DeleteAttachmentResponseType DeleteAttachment([XmlElement("DeleteAttachment", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DeleteAttachmentType DeleteAttachment1)
  {
    return (DeleteAttachmentResponseType) this.Invoke(nameof (DeleteAttachment), new object[1]
    {
      (object) DeleteAttachment1
    })[0];
  }

  /// <remarks />
  public void DeleteAttachmentAsync(DeleteAttachmentType DeleteAttachment1)
  {
    this.DeleteAttachmentAsync(DeleteAttachment1, (object) null);
  }

  /// <remarks />
  public void DeleteAttachmentAsync(DeleteAttachmentType DeleteAttachment1, object userState)
  {
    if (this.DeleteAttachmentOperationCompleted == null)
      this.DeleteAttachmentOperationCompleted = new SendOrPostCallback(this.OnDeleteAttachmentOperationCompleted);
    this.InvokeAsync("DeleteAttachment", new object[1]
    {
      (object) DeleteAttachment1
    }, this.DeleteAttachmentOperationCompleted, userState);
  }

  private void OnDeleteAttachmentOperationCompleted(object arg)
  {
    if (this.DeleteAttachmentCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.DeleteAttachmentCompleted((object) this, new DeleteAttachmentCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("TimeZoneContext")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetAttachment", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetAttachmentResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetAttachmentResponseType GetAttachment([XmlElement("GetAttachment", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetAttachmentType GetAttachment1)
  {
    return (GetAttachmentResponseType) this.Invoke(nameof (GetAttachment), new object[1]
    {
      (object) GetAttachment1
    })[0];
  }

  /// <remarks />
  public void GetAttachmentAsync(GetAttachmentType GetAttachment1)
  {
    this.GetAttachmentAsync(GetAttachment1, (object) null);
  }

  /// <remarks />
  public void GetAttachmentAsync(GetAttachmentType GetAttachment1, object userState)
  {
    if (this.GetAttachmentOperationCompleted == null)
      this.GetAttachmentOperationCompleted = new SendOrPostCallback(this.OnGetAttachmentOperationCompleted);
    this.InvokeAsync("GetAttachment", new object[1]
    {
      (object) GetAttachment1
    }, this.GetAttachmentOperationCompleted, userState);
  }

  private void OnGetAttachmentOperationCompleted(object arg)
  {
    if (this.GetAttachmentCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetAttachmentCompleted((object) this, new GetAttachmentCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetClientAccessToken", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetClientAccessTokenResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetClientAccessTokenResponseType GetClientAccessToken(
    [XmlElement("GetClientAccessToken", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetClientAccessTokenType GetClientAccessToken1)
  {
    return (GetClientAccessTokenResponseType) this.Invoke(nameof (GetClientAccessToken), new object[1]
    {
      (object) GetClientAccessToken1
    })[0];
  }

  /// <remarks />
  public void GetClientAccessTokenAsync(GetClientAccessTokenType GetClientAccessToken1)
  {
    this.GetClientAccessTokenAsync(GetClientAccessToken1, (object) null);
  }

  /// <remarks />
  public void GetClientAccessTokenAsync(
    GetClientAccessTokenType GetClientAccessToken1,
    object userState)
  {
    if (this.GetClientAccessTokenOperationCompleted == null)
      this.GetClientAccessTokenOperationCompleted = new SendOrPostCallback(this.OnGetClientAccessTokenOperationCompleted);
    this.InvokeAsync("GetClientAccessToken", new object[1]
    {
      (object) GetClientAccessToken1
    }, this.GetClientAccessTokenOperationCompleted, userState);
  }

  private void OnGetClientAccessTokenOperationCompleted(object arg)
  {
    if (this.GetClientAccessTokenCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetClientAccessTokenCompleted((object) this, new GetClientAccessTokenCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetDelegate", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetDelegateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetDelegateResponseMessageType GetDelegate([XmlElement("GetDelegate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetDelegateType GetDelegate1)
  {
    return (GetDelegateResponseMessageType) this.Invoke(nameof (GetDelegate), new object[1]
    {
      (object) GetDelegate1
    })[0];
  }

  /// <remarks />
  public void GetDelegateAsync(GetDelegateType GetDelegate1)
  {
    this.GetDelegateAsync(GetDelegate1, (object) null);
  }

  /// <remarks />
  public void GetDelegateAsync(GetDelegateType GetDelegate1, object userState)
  {
    if (this.GetDelegateOperationCompleted == null)
      this.GetDelegateOperationCompleted = new SendOrPostCallback(this.OnGetDelegateOperationCompleted);
    this.InvokeAsync("GetDelegate", new object[1]
    {
      (object) GetDelegate1
    }, this.GetDelegateOperationCompleted, userState);
  }

  private void OnGetDelegateOperationCompleted(object arg)
  {
    if (this.GetDelegateCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetDelegateCompleted((object) this, new GetDelegateCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/AddDelegate", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("AddDelegateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public AddDelegateResponseMessageType AddDelegate([XmlElement("AddDelegate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] AddDelegateType AddDelegate1)
  {
    return (AddDelegateResponseMessageType) this.Invoke(nameof (AddDelegate), new object[1]
    {
      (object) AddDelegate1
    })[0];
  }

  /// <remarks />
  public void AddDelegateAsync(AddDelegateType AddDelegate1)
  {
    this.AddDelegateAsync(AddDelegate1, (object) null);
  }

  /// <remarks />
  public void AddDelegateAsync(AddDelegateType AddDelegate1, object userState)
  {
    if (this.AddDelegateOperationCompleted == null)
      this.AddDelegateOperationCompleted = new SendOrPostCallback(this.OnAddDelegateOperationCompleted);
    this.InvokeAsync("AddDelegate", new object[1]
    {
      (object) AddDelegate1
    }, this.AddDelegateOperationCompleted, userState);
  }

  private void OnAddDelegateOperationCompleted(object arg)
  {
    if (this.AddDelegateCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.AddDelegateCompleted((object) this, new AddDelegateCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/RemoveDelegate", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("RemoveDelegateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public RemoveDelegateResponseMessageType RemoveDelegate([XmlElement("RemoveDelegate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] RemoveDelegateType RemoveDelegate1)
  {
    return (RemoveDelegateResponseMessageType) this.Invoke(nameof (RemoveDelegate), new object[1]
    {
      (object) RemoveDelegate1
    })[0];
  }

  /// <remarks />
  public void RemoveDelegateAsync(RemoveDelegateType RemoveDelegate1)
  {
    this.RemoveDelegateAsync(RemoveDelegate1, (object) null);
  }

  /// <remarks />
  public void RemoveDelegateAsync(RemoveDelegateType RemoveDelegate1, object userState)
  {
    if (this.RemoveDelegateOperationCompleted == null)
      this.RemoveDelegateOperationCompleted = new SendOrPostCallback(this.OnRemoveDelegateOperationCompleted);
    this.InvokeAsync("RemoveDelegate", new object[1]
    {
      (object) RemoveDelegate1
    }, this.RemoveDelegateOperationCompleted, userState);
  }

  private void OnRemoveDelegateOperationCompleted(object arg)
  {
    if (this.RemoveDelegateCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.RemoveDelegateCompleted((object) this, new RemoveDelegateCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateDelegate", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("UpdateDelegateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public UpdateDelegateResponseMessageType UpdateDelegate([XmlElement("UpdateDelegate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateDelegateType UpdateDelegate1)
  {
    return (UpdateDelegateResponseMessageType) this.Invoke(nameof (UpdateDelegate), new object[1]
    {
      (object) UpdateDelegate1
    })[0];
  }

  /// <remarks />
  public void UpdateDelegateAsync(UpdateDelegateType UpdateDelegate1)
  {
    this.UpdateDelegateAsync(UpdateDelegate1, (object) null);
  }

  /// <remarks />
  public void UpdateDelegateAsync(UpdateDelegateType UpdateDelegate1, object userState)
  {
    if (this.UpdateDelegateOperationCompleted == null)
      this.UpdateDelegateOperationCompleted = new SendOrPostCallback(this.OnUpdateDelegateOperationCompleted);
    this.InvokeAsync("UpdateDelegate", new object[1]
    {
      (object) UpdateDelegate1
    }, this.UpdateDelegateOperationCompleted, userState);
  }

  private void OnUpdateDelegateOperationCompleted(object arg)
  {
    if (this.UpdateDelegateCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.UpdateDelegateCompleted((object) this, new UpdateDelegateCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateUserConfiguration", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("CreateUserConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public CreateUserConfigurationResponseType CreateUserConfiguration(
    [XmlElement("CreateUserConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateUserConfigurationType CreateUserConfiguration1)
  {
    return (CreateUserConfigurationResponseType) this.Invoke(nameof (CreateUserConfiguration), new object[1]
    {
      (object) CreateUserConfiguration1
    })[0];
  }

  /// <remarks />
  public void CreateUserConfigurationAsync(
    CreateUserConfigurationType CreateUserConfiguration1)
  {
    this.CreateUserConfigurationAsync(CreateUserConfiguration1, (object) null);
  }

  /// <remarks />
  public void CreateUserConfigurationAsync(
    CreateUserConfigurationType CreateUserConfiguration1,
    object userState)
  {
    if (this.CreateUserConfigurationOperationCompleted == null)
      this.CreateUserConfigurationOperationCompleted = new SendOrPostCallback(this.OnCreateUserConfigurationOperationCompleted);
    this.InvokeAsync("CreateUserConfiguration", new object[1]
    {
      (object) CreateUserConfiguration1
    }, this.CreateUserConfigurationOperationCompleted, userState);
  }

  private void OnCreateUserConfigurationOperationCompleted(object arg)
  {
    if (this.CreateUserConfigurationCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.CreateUserConfigurationCompleted((object) this, new CreateUserConfigurationCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DeleteUserConfiguration", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("DeleteUserConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public DeleteUserConfigurationResponseType DeleteUserConfiguration(
    [XmlElement("DeleteUserConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DeleteUserConfigurationType DeleteUserConfiguration1)
  {
    return (DeleteUserConfigurationResponseType) this.Invoke(nameof (DeleteUserConfiguration), new object[1]
    {
      (object) DeleteUserConfiguration1
    })[0];
  }

  /// <remarks />
  public void DeleteUserConfigurationAsync(
    DeleteUserConfigurationType DeleteUserConfiguration1)
  {
    this.DeleteUserConfigurationAsync(DeleteUserConfiguration1, (object) null);
  }

  /// <remarks />
  public void DeleteUserConfigurationAsync(
    DeleteUserConfigurationType DeleteUserConfiguration1,
    object userState)
  {
    if (this.DeleteUserConfigurationOperationCompleted == null)
      this.DeleteUserConfigurationOperationCompleted = new SendOrPostCallback(this.OnDeleteUserConfigurationOperationCompleted);
    this.InvokeAsync("DeleteUserConfiguration", new object[1]
    {
      (object) DeleteUserConfiguration1
    }, this.DeleteUserConfigurationOperationCompleted, userState);
  }

  private void OnDeleteUserConfigurationOperationCompleted(object arg)
  {
    if (this.DeleteUserConfigurationCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.DeleteUserConfigurationCompleted((object) this, new DeleteUserConfigurationCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUserConfiguration", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetUserConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetUserConfigurationResponseType GetUserConfiguration(
    [XmlElement("GetUserConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUserConfigurationType GetUserConfiguration1)
  {
    return (GetUserConfigurationResponseType) this.Invoke(nameof (GetUserConfiguration), new object[1]
    {
      (object) GetUserConfiguration1
    })[0];
  }

  /// <remarks />
  public void GetUserConfigurationAsync(GetUserConfigurationType GetUserConfiguration1)
  {
    this.GetUserConfigurationAsync(GetUserConfiguration1, (object) null);
  }

  /// <remarks />
  public void GetUserConfigurationAsync(
    GetUserConfigurationType GetUserConfiguration1,
    object userState)
  {
    if (this.GetUserConfigurationOperationCompleted == null)
      this.GetUserConfigurationOperationCompleted = new SendOrPostCallback(this.OnGetUserConfigurationOperationCompleted);
    this.InvokeAsync("GetUserConfiguration", new object[1]
    {
      (object) GetUserConfiguration1
    }, this.GetUserConfigurationOperationCompleted, userState);
  }

  private void OnGetUserConfigurationOperationCompleted(object arg)
  {
    if (this.GetUserConfigurationCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetUserConfigurationCompleted((object) this, new GetUserConfigurationCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateUserConfiguration", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("UpdateUserConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public UpdateUserConfigurationResponseType UpdateUserConfiguration(
    [XmlElement("UpdateUserConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateUserConfigurationType UpdateUserConfiguration1)
  {
    return (UpdateUserConfigurationResponseType) this.Invoke(nameof (UpdateUserConfiguration), new object[1]
    {
      (object) UpdateUserConfiguration1
    })[0];
  }

  /// <remarks />
  public void UpdateUserConfigurationAsync(
    UpdateUserConfigurationType UpdateUserConfiguration1)
  {
    this.UpdateUserConfigurationAsync(UpdateUserConfiguration1, (object) null);
  }

  /// <remarks />
  public void UpdateUserConfigurationAsync(
    UpdateUserConfigurationType UpdateUserConfiguration1,
    object userState)
  {
    if (this.UpdateUserConfigurationOperationCompleted == null)
      this.UpdateUserConfigurationOperationCompleted = new SendOrPostCallback(this.OnUpdateUserConfigurationOperationCompleted);
    this.InvokeAsync("UpdateUserConfiguration", new object[1]
    {
      (object) UpdateUserConfiguration1
    }, this.UpdateUserConfigurationOperationCompleted, userState);
  }

  private void OnUpdateUserConfigurationOperationCompleted(object arg)
  {
    if (this.UpdateUserConfigurationCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.UpdateUserConfigurationCompleted((object) this, new UpdateUserConfigurationCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("TimeZoneContext")]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUserAvailability", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetUserAvailabilityResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetUserAvailabilityResponseType GetUserAvailability(
    [XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUserAvailabilityRequestType GetUserAvailabilityRequest)
  {
    return (GetUserAvailabilityResponseType) this.Invoke(nameof (GetUserAvailability), new object[1]
    {
      (object) GetUserAvailabilityRequest
    })[0];
  }

  /// <remarks />
  public void GetUserAvailabilityAsync(
    GetUserAvailabilityRequestType GetUserAvailabilityRequest)
  {
    this.GetUserAvailabilityAsync(GetUserAvailabilityRequest, (object) null);
  }

  /// <remarks />
  public void GetUserAvailabilityAsync(
    GetUserAvailabilityRequestType GetUserAvailabilityRequest,
    object userState)
  {
    if (this.GetUserAvailabilityOperationCompleted == null)
      this.GetUserAvailabilityOperationCompleted = new SendOrPostCallback(this.OnGetUserAvailabilityOperationCompleted);
    this.InvokeAsync("GetUserAvailability", new object[1]
    {
      (object) GetUserAvailabilityRequest
    }, this.GetUserAvailabilityOperationCompleted, userState);
  }

  private void OnGetUserAvailabilityOperationCompleted(object arg)
  {
    if (this.GetUserAvailabilityCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetUserAvailabilityCompleted((object) this, new GetUserAvailabilityCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("ExchangeImpersonation")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUserOofSettings", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetUserOofSettingsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetUserOofSettingsResponse GetUserOofSettings(
    [XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUserOofSettingsRequest GetUserOofSettingsRequest)
  {
    return (GetUserOofSettingsResponse) this.Invoke(nameof (GetUserOofSettings), new object[1]
    {
      (object) GetUserOofSettingsRequest
    })[0];
  }

  /// <remarks />
  public void GetUserOofSettingsAsync(
    GetUserOofSettingsRequest GetUserOofSettingsRequest)
  {
    this.GetUserOofSettingsAsync(GetUserOofSettingsRequest, (object) null);
  }

  /// <remarks />
  public void GetUserOofSettingsAsync(
    GetUserOofSettingsRequest GetUserOofSettingsRequest,
    object userState)
  {
    if (this.GetUserOofSettingsOperationCompleted == null)
      this.GetUserOofSettingsOperationCompleted = new SendOrPostCallback(this.OnGetUserOofSettingsOperationCompleted);
    this.InvokeAsync("GetUserOofSettings", new object[1]
    {
      (object) GetUserOofSettingsRequest
    }, this.GetUserOofSettingsOperationCompleted, userState);
  }

  private void OnGetUserOofSettingsOperationCompleted(object arg)
  {
    if (this.GetUserOofSettingsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetUserOofSettingsCompleted((object) this, new GetUserOofSettingsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("ExchangeImpersonation")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SetUserOofSettings", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("SetUserOofSettingsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public SetUserOofSettingsResponse SetUserOofSettings(
    [XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SetUserOofSettingsRequest SetUserOofSettingsRequest)
  {
    return (SetUserOofSettingsResponse) this.Invoke(nameof (SetUserOofSettings), new object[1]
    {
      (object) SetUserOofSettingsRequest
    })[0];
  }

  /// <remarks />
  public void SetUserOofSettingsAsync(
    SetUserOofSettingsRequest SetUserOofSettingsRequest)
  {
    this.SetUserOofSettingsAsync(SetUserOofSettingsRequest, (object) null);
  }

  /// <remarks />
  public void SetUserOofSettingsAsync(
    SetUserOofSettingsRequest SetUserOofSettingsRequest,
    object userState)
  {
    if (this.SetUserOofSettingsOperationCompleted == null)
      this.SetUserOofSettingsOperationCompleted = new SendOrPostCallback(this.OnSetUserOofSettingsOperationCompleted);
    this.InvokeAsync("SetUserOofSettings", new object[1]
    {
      (object) SetUserOofSettingsRequest
    }, this.SetUserOofSettingsOperationCompleted, userState);
  }

  private void OnSetUserOofSettingsOperationCompleted(object arg)
  {
    if (this.SetUserOofSettingsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.SetUserOofSettingsCompleted((object) this, new SetUserOofSettingsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetServiceConfiguration", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetServiceConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetServiceConfigurationResponseMessageType GetServiceConfiguration(
    [XmlElement("GetServiceConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetServiceConfigurationType GetServiceConfiguration1)
  {
    return (GetServiceConfigurationResponseMessageType) this.Invoke(nameof (GetServiceConfiguration), new object[1]
    {
      (object) GetServiceConfiguration1
    })[0];
  }

  /// <remarks />
  public void GetServiceConfigurationAsync(
    GetServiceConfigurationType GetServiceConfiguration1)
  {
    this.GetServiceConfigurationAsync(GetServiceConfiguration1, (object) null);
  }

  /// <remarks />
  public void GetServiceConfigurationAsync(
    GetServiceConfigurationType GetServiceConfiguration1,
    object userState)
  {
    if (this.GetServiceConfigurationOperationCompleted == null)
      this.GetServiceConfigurationOperationCompleted = new SendOrPostCallback(this.OnGetServiceConfigurationOperationCompleted);
    this.InvokeAsync("GetServiceConfiguration", new object[1]
    {
      (object) GetServiceConfiguration1
    }, this.GetServiceConfigurationOperationCompleted, userState);
  }

  private void OnGetServiceConfigurationOperationCompleted(object arg)
  {
    if (this.GetServiceConfigurationCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetServiceConfigurationCompleted((object) this, new GetServiceConfigurationCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetMailTips", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetMailTipsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetMailTipsResponseMessageType GetMailTips([XmlElement("GetMailTips", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetMailTipsType GetMailTips1)
  {
    return (GetMailTipsResponseMessageType) this.Invoke(nameof (GetMailTips), new object[1]
    {
      (object) GetMailTips1
    })[0];
  }

  /// <remarks />
  public void GetMailTipsAsync(GetMailTipsType GetMailTips1)
  {
    this.GetMailTipsAsync(GetMailTips1, (object) null);
  }

  /// <remarks />
  public void GetMailTipsAsync(GetMailTipsType GetMailTips1, object userState)
  {
    if (this.GetMailTipsOperationCompleted == null)
      this.GetMailTipsOperationCompleted = new SendOrPostCallback(this.OnGetMailTipsOperationCompleted);
    this.InvokeAsync("GetMailTips", new object[1]
    {
      (object) GetMailTips1
    }, this.GetMailTipsOperationCompleted, userState);
  }

  private void OnGetMailTipsOperationCompleted(object arg)
  {
    if (this.GetMailTipsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetMailTipsCompleted((object) this, new GetMailTipsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/PlayOnPhone", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("PlayOnPhoneResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public PlayOnPhoneResponseMessageType PlayOnPhone([XmlElement("PlayOnPhone", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] PlayOnPhoneType PlayOnPhone1)
  {
    return (PlayOnPhoneResponseMessageType) this.Invoke(nameof (PlayOnPhone), new object[1]
    {
      (object) PlayOnPhone1
    })[0];
  }

  /// <remarks />
  public void PlayOnPhoneAsync(PlayOnPhoneType PlayOnPhone1)
  {
    this.PlayOnPhoneAsync(PlayOnPhone1, (object) null);
  }

  /// <remarks />
  public void PlayOnPhoneAsync(PlayOnPhoneType PlayOnPhone1, object userState)
  {
    if (this.PlayOnPhoneOperationCompleted == null)
      this.PlayOnPhoneOperationCompleted = new SendOrPostCallback(this.OnPlayOnPhoneOperationCompleted);
    this.InvokeAsync("PlayOnPhone", new object[1]
    {
      (object) PlayOnPhone1
    }, this.PlayOnPhoneOperationCompleted, userState);
  }

  private void OnPlayOnPhoneOperationCompleted(object arg)
  {
    if (this.PlayOnPhoneCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.PlayOnPhoneCompleted((object) this, new PlayOnPhoneCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetPhoneCallInformation", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetPhoneCallInformationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetPhoneCallInformationResponseMessageType GetPhoneCallInformation(
    [XmlElement("GetPhoneCallInformation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetPhoneCallInformationType GetPhoneCallInformation1)
  {
    return (GetPhoneCallInformationResponseMessageType) this.Invoke(nameof (GetPhoneCallInformation), new object[1]
    {
      (object) GetPhoneCallInformation1
    })[0];
  }

  /// <remarks />
  public void GetPhoneCallInformationAsync(
    GetPhoneCallInformationType GetPhoneCallInformation1)
  {
    this.GetPhoneCallInformationAsync(GetPhoneCallInformation1, (object) null);
  }

  /// <remarks />
  public void GetPhoneCallInformationAsync(
    GetPhoneCallInformationType GetPhoneCallInformation1,
    object userState)
  {
    if (this.GetPhoneCallInformationOperationCompleted == null)
      this.GetPhoneCallInformationOperationCompleted = new SendOrPostCallback(this.OnGetPhoneCallInformationOperationCompleted);
    this.InvokeAsync("GetPhoneCallInformation", new object[1]
    {
      (object) GetPhoneCallInformation1
    }, this.GetPhoneCallInformationOperationCompleted, userState);
  }

  private void OnGetPhoneCallInformationOperationCompleted(object arg)
  {
    if (this.GetPhoneCallInformationCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetPhoneCallInformationCompleted((object) this, new GetPhoneCallInformationCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DisconnectPhoneCall", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("DisconnectPhoneCallResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public DisconnectPhoneCallResponseMessageType DisconnectPhoneCall(
    [XmlElement("DisconnectPhoneCall", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DisconnectPhoneCallType DisconnectPhoneCall1)
  {
    return (DisconnectPhoneCallResponseMessageType) this.Invoke(nameof (DisconnectPhoneCall), new object[1]
    {
      (object) DisconnectPhoneCall1
    })[0];
  }

  /// <remarks />
  public void DisconnectPhoneCallAsync(DisconnectPhoneCallType DisconnectPhoneCall1)
  {
    this.DisconnectPhoneCallAsync(DisconnectPhoneCall1, (object) null);
  }

  /// <remarks />
  public void DisconnectPhoneCallAsync(
    DisconnectPhoneCallType DisconnectPhoneCall1,
    object userState)
  {
    if (this.DisconnectPhoneCallOperationCompleted == null)
      this.DisconnectPhoneCallOperationCompleted = new SendOrPostCallback(this.OnDisconnectPhoneCallOperationCompleted);
    this.InvokeAsync("DisconnectPhoneCall", new object[1]
    {
      (object) DisconnectPhoneCall1
    }, this.DisconnectPhoneCallOperationCompleted, userState);
  }

  private void OnDisconnectPhoneCallOperationCompleted(object arg)
  {
    if (this.DisconnectPhoneCallCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.DisconnectPhoneCallCompleted((object) this, new DisconnectPhoneCallCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetSharingMetadata", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetSharingMetadataResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetSharingMetadataResponseMessageType GetSharingMetadata(
    [XmlElement("GetSharingMetadata", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetSharingMetadataType GetSharingMetadata1)
  {
    return (GetSharingMetadataResponseMessageType) this.Invoke(nameof (GetSharingMetadata), new object[1]
    {
      (object) GetSharingMetadata1
    })[0];
  }

  /// <remarks />
  public void GetSharingMetadataAsync(GetSharingMetadataType GetSharingMetadata1)
  {
    this.GetSharingMetadataAsync(GetSharingMetadata1, (object) null);
  }

  /// <remarks />
  public void GetSharingMetadataAsync(GetSharingMetadataType GetSharingMetadata1, object userState)
  {
    if (this.GetSharingMetadataOperationCompleted == null)
      this.GetSharingMetadataOperationCompleted = new SendOrPostCallback(this.OnGetSharingMetadataOperationCompleted);
    this.InvokeAsync("GetSharingMetadata", new object[1]
    {
      (object) GetSharingMetadata1
    }, this.GetSharingMetadataOperationCompleted, userState);
  }

  private void OnGetSharingMetadataOperationCompleted(object arg)
  {
    if (this.GetSharingMetadataCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetSharingMetadataCompleted((object) this, new GetSharingMetadataCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/RefreshSharingFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("RefreshSharingFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public RefreshSharingFolderResponseMessageType RefreshSharingFolder(
    [XmlElement("RefreshSharingFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] RefreshSharingFolderType RefreshSharingFolder1)
  {
    return (RefreshSharingFolderResponseMessageType) this.Invoke(nameof (RefreshSharingFolder), new object[1]
    {
      (object) RefreshSharingFolder1
    })[0];
  }

  /// <remarks />
  public void RefreshSharingFolderAsync(RefreshSharingFolderType RefreshSharingFolder1)
  {
    this.RefreshSharingFolderAsync(RefreshSharingFolder1, (object) null);
  }

  /// <remarks />
  public void RefreshSharingFolderAsync(
    RefreshSharingFolderType RefreshSharingFolder1,
    object userState)
  {
    if (this.RefreshSharingFolderOperationCompleted == null)
      this.RefreshSharingFolderOperationCompleted = new SendOrPostCallback(this.OnRefreshSharingFolderOperationCompleted);
    this.InvokeAsync("RefreshSharingFolder", new object[1]
    {
      (object) RefreshSharingFolder1
    }, this.RefreshSharingFolderOperationCompleted, userState);
  }

  private void OnRefreshSharingFolderOperationCompleted(object arg)
  {
    if (this.RefreshSharingFolderCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.RefreshSharingFolderCompleted((object) this, new RefreshSharingFolderCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetSharingFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetSharingFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetSharingFolderResponseMessageType GetSharingFolder([XmlElement("GetSharingFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetSharingFolderType GetSharingFolder1)
  {
    return (GetSharingFolderResponseMessageType) this.Invoke(nameof (GetSharingFolder), new object[1]
    {
      (object) GetSharingFolder1
    })[0];
  }

  /// <remarks />
  public void GetSharingFolderAsync(GetSharingFolderType GetSharingFolder1)
  {
    this.GetSharingFolderAsync(GetSharingFolder1, (object) null);
  }

  /// <remarks />
  public void GetSharingFolderAsync(GetSharingFolderType GetSharingFolder1, object userState)
  {
    if (this.GetSharingFolderOperationCompleted == null)
      this.GetSharingFolderOperationCompleted = new SendOrPostCallback(this.OnGetSharingFolderOperationCompleted);
    this.InvokeAsync("GetSharingFolder", new object[1]
    {
      (object) GetSharingFolder1
    }, this.GetSharingFolderOperationCompleted, userState);
  }

  private void OnGetSharingFolderOperationCompleted(object arg)
  {
    if (this.GetSharingFolderCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetSharingFolderCompleted((object) this, new GetSharingFolderCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ManagementRole")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SetTeamMailbox", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("SetTeamMailboxResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public SetTeamMailboxResponseMessageType SetTeamMailbox([XmlElement("SetTeamMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SetTeamMailboxRequestType SetTeamMailbox1)
  {
    return (SetTeamMailboxResponseMessageType) this.Invoke(nameof (SetTeamMailbox), new object[1]
    {
      (object) SetTeamMailbox1
    })[0];
  }

  /// <remarks />
  public void SetTeamMailboxAsync(SetTeamMailboxRequestType SetTeamMailbox1)
  {
    this.SetTeamMailboxAsync(SetTeamMailbox1, (object) null);
  }

  /// <remarks />
  public void SetTeamMailboxAsync(SetTeamMailboxRequestType SetTeamMailbox1, object userState)
  {
    if (this.SetTeamMailboxOperationCompleted == null)
      this.SetTeamMailboxOperationCompleted = new SendOrPostCallback(this.OnSetTeamMailboxOperationCompleted);
    this.InvokeAsync("SetTeamMailbox", new object[1]
    {
      (object) SetTeamMailbox1
    }, this.SetTeamMailboxOperationCompleted, userState);
  }

  private void OnSetTeamMailboxOperationCompleted(object arg)
  {
    if (this.SetTeamMailboxCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.SetTeamMailboxCompleted((object) this, new SetTeamMailboxCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UnpinTeamMailbox", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("UnpinTeamMailboxResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public UnpinTeamMailboxResponseMessageType UnpinTeamMailbox(
    [XmlElement("UnpinTeamMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UnpinTeamMailboxRequestType UnpinTeamMailbox1)
  {
    return (UnpinTeamMailboxResponseMessageType) this.Invoke(nameof (UnpinTeamMailbox), new object[1]
    {
      (object) UnpinTeamMailbox1
    })[0];
  }

  /// <remarks />
  public void UnpinTeamMailboxAsync(UnpinTeamMailboxRequestType UnpinTeamMailbox1)
  {
    this.UnpinTeamMailboxAsync(UnpinTeamMailbox1, (object) null);
  }

  /// <remarks />
  public void UnpinTeamMailboxAsync(UnpinTeamMailboxRequestType UnpinTeamMailbox1, object userState)
  {
    if (this.UnpinTeamMailboxOperationCompleted == null)
      this.UnpinTeamMailboxOperationCompleted = new SendOrPostCallback(this.OnUnpinTeamMailboxOperationCompleted);
    this.InvokeAsync("UnpinTeamMailbox", new object[1]
    {
      (object) UnpinTeamMailbox1
    }, this.UnpinTeamMailboxOperationCompleted, userState);
  }

  private void OnUnpinTeamMailboxOperationCompleted(object arg)
  {
    if (this.UnpinTeamMailboxCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.UnpinTeamMailboxCompleted((object) this, new UnpinTeamMailboxCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetRoomLists", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetRoomListsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetRoomListsResponseMessageType GetRoomLists([XmlElement("GetRoomLists", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetRoomListsType GetRoomLists1)
  {
    return (GetRoomListsResponseMessageType) this.Invoke(nameof (GetRoomLists), new object[1]
    {
      (object) GetRoomLists1
    })[0];
  }

  /// <remarks />
  public void GetRoomListsAsync(GetRoomListsType GetRoomLists1)
  {
    this.GetRoomListsAsync(GetRoomLists1, (object) null);
  }

  /// <remarks />
  public void GetRoomListsAsync(GetRoomListsType GetRoomLists1, object userState)
  {
    if (this.GetRoomListsOperationCompleted == null)
      this.GetRoomListsOperationCompleted = new SendOrPostCallback(this.OnGetRoomListsOperationCompleted);
    this.InvokeAsync("GetRoomLists", new object[1]
    {
      (object) GetRoomLists1
    }, this.GetRoomListsOperationCompleted, userState);
  }

  private void OnGetRoomListsOperationCompleted(object arg)
  {
    if (this.GetRoomListsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetRoomListsCompleted((object) this, new GetRoomListsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetRooms", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetRoomsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetRoomsResponseMessageType GetRooms([XmlElement("GetRooms", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetRoomsType GetRooms1)
  {
    return (GetRoomsResponseMessageType) this.Invoke(nameof (GetRooms), new object[1]
    {
      (object) GetRooms1
    })[0];
  }

  /// <remarks />
  public void GetRoomsAsync(GetRoomsType GetRooms1) => this.GetRoomsAsync(GetRooms1, (object) null);

  /// <remarks />
  public void GetRoomsAsync(GetRoomsType GetRooms1, object userState)
  {
    if (this.GetRoomsOperationCompleted == null)
      this.GetRoomsOperationCompleted = new SendOrPostCallback(this.OnGetRoomsOperationCompleted);
    this.InvokeAsync("GetRooms", new object[1]
    {
      (object) GetRooms1
    }, this.GetRoomsOperationCompleted, userState);
  }

  private void OnGetRoomsOperationCompleted(object arg)
  {
    if (this.GetRoomsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetRoomsCompleted((object) this, new GetRoomsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/FindMessageTrackingReport", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("FindMessageTrackingReportResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public FindMessageTrackingReportResponseMessageType FindMessageTrackingReport(
    [XmlElement("FindMessageTrackingReport", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] FindMessageTrackingReportRequestType FindMessageTrackingReport1)
  {
    return (FindMessageTrackingReportResponseMessageType) this.Invoke(nameof (FindMessageTrackingReport), new object[1]
    {
      (object) FindMessageTrackingReport1
    })[0];
  }

  /// <remarks />
  public void FindMessageTrackingReportAsync(
    FindMessageTrackingReportRequestType FindMessageTrackingReport1)
  {
    this.FindMessageTrackingReportAsync(FindMessageTrackingReport1, (object) null);
  }

  /// <remarks />
  public void FindMessageTrackingReportAsync(
    FindMessageTrackingReportRequestType FindMessageTrackingReport1,
    object userState)
  {
    if (this.FindMessageTrackingReportOperationCompleted == null)
      this.FindMessageTrackingReportOperationCompleted = new SendOrPostCallback(this.OnFindMessageTrackingReportOperationCompleted);
    this.InvokeAsync("FindMessageTrackingReport", new object[1]
    {
      (object) FindMessageTrackingReport1
    }, this.FindMessageTrackingReportOperationCompleted, userState);
  }

  private void OnFindMessageTrackingReportOperationCompleted(object arg)
  {
    if (this.FindMessageTrackingReportCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.FindMessageTrackingReportCompleted((object) this, new FindMessageTrackingReportCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetMessageTrackingReport", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetMessageTrackingReportResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetMessageTrackingReportResponseMessageType GetMessageTrackingReport(
    [XmlElement("GetMessageTrackingReport", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetMessageTrackingReportRequestType GetMessageTrackingReport1)
  {
    return (GetMessageTrackingReportResponseMessageType) this.Invoke(nameof (GetMessageTrackingReport), new object[1]
    {
      (object) GetMessageTrackingReport1
    })[0];
  }

  /// <remarks />
  public void GetMessageTrackingReportAsync(
    GetMessageTrackingReportRequestType GetMessageTrackingReport1)
  {
    this.GetMessageTrackingReportAsync(GetMessageTrackingReport1, (object) null);
  }

  /// <remarks />
  public void GetMessageTrackingReportAsync(
    GetMessageTrackingReportRequestType GetMessageTrackingReport1,
    object userState)
  {
    if (this.GetMessageTrackingReportOperationCompleted == null)
      this.GetMessageTrackingReportOperationCompleted = new SendOrPostCallback(this.OnGetMessageTrackingReportOperationCompleted);
    this.InvokeAsync("GetMessageTrackingReport", new object[1]
    {
      (object) GetMessageTrackingReport1
    }, this.GetMessageTrackingReportOperationCompleted, userState);
  }

  private void OnGetMessageTrackingReportOperationCompleted(object arg)
  {
    if (this.GetMessageTrackingReportCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetMessageTrackingReportCompleted((object) this, new GetMessageTrackingReportCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/FindConversation", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("FindConversationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public FindConversationResponseMessageType FindConversation([XmlElement("FindConversation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] FindConversationType FindConversation1)
  {
    return (FindConversationResponseMessageType) this.Invoke(nameof (FindConversation), new object[1]
    {
      (object) FindConversation1
    })[0];
  }

  /// <remarks />
  public void FindConversationAsync(FindConversationType FindConversation1)
  {
    this.FindConversationAsync(FindConversation1, (object) null);
  }

  /// <remarks />
  public void FindConversationAsync(FindConversationType FindConversation1, object userState)
  {
    if (this.FindConversationOperationCompleted == null)
      this.FindConversationOperationCompleted = new SendOrPostCallback(this.OnFindConversationOperationCompleted);
    this.InvokeAsync("FindConversation", new object[1]
    {
      (object) FindConversation1
    }, this.FindConversationOperationCompleted, userState);
  }

  private void OnFindConversationOperationCompleted(object arg)
  {
    if (this.FindConversationCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.FindConversationCompleted((object) this, new FindConversationCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ApplyConversationAction", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("ApplyConversationActionResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public ApplyConversationActionResponseType ApplyConversationAction(
    [XmlElement("ApplyConversationAction", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ApplyConversationActionType ApplyConversationAction1)
  {
    return (ApplyConversationActionResponseType) this.Invoke(nameof (ApplyConversationAction), new object[1]
    {
      (object) ApplyConversationAction1
    })[0];
  }

  /// <remarks />
  public void ApplyConversationActionAsync(
    ApplyConversationActionType ApplyConversationAction1)
  {
    this.ApplyConversationActionAsync(ApplyConversationAction1, (object) null);
  }

  /// <remarks />
  public void ApplyConversationActionAsync(
    ApplyConversationActionType ApplyConversationAction1,
    object userState)
  {
    if (this.ApplyConversationActionOperationCompleted == null)
      this.ApplyConversationActionOperationCompleted = new SendOrPostCallback(this.OnApplyConversationActionOperationCompleted);
    this.InvokeAsync("ApplyConversationAction", new object[1]
    {
      (object) ApplyConversationAction1
    }, this.ApplyConversationActionOperationCompleted, userState);
  }

  private void OnApplyConversationActionOperationCompleted(object arg)
  {
    if (this.ApplyConversationActionCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.ApplyConversationActionCompleted((object) this, new ApplyConversationActionCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetConversationItems", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetConversationItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetConversationItemsResponseType GetConversationItems(
    [XmlElement("GetConversationItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetConversationItemsType GetConversationItems1)
  {
    return (GetConversationItemsResponseType) this.Invoke(nameof (GetConversationItems), new object[1]
    {
      (object) GetConversationItems1
    })[0];
  }

  /// <remarks />
  public void GetConversationItemsAsync(GetConversationItemsType GetConversationItems1)
  {
    this.GetConversationItemsAsync(GetConversationItems1, (object) null);
  }

  /// <remarks />
  public void GetConversationItemsAsync(
    GetConversationItemsType GetConversationItems1,
    object userState)
  {
    if (this.GetConversationItemsOperationCompleted == null)
      this.GetConversationItemsOperationCompleted = new SendOrPostCallback(this.OnGetConversationItemsOperationCompleted);
    this.InvokeAsync("GetConversationItems", new object[1]
    {
      (object) GetConversationItems1
    }, this.GetConversationItemsOperationCompleted, userState);
  }

  private void OnGetConversationItemsOperationCompleted(object arg)
  {
    if (this.GetConversationItemsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetConversationItemsCompleted((object) this, new GetConversationItemsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/FindPeople", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("FindPeopleResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public FindPeopleResponseMessageType FindPeople([XmlElement("FindPeople", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] FindPeopleType FindPeople1)
  {
    return (FindPeopleResponseMessageType) this.Invoke(nameof (FindPeople), new object[1]
    {
      (object) FindPeople1
    })[0];
  }

  /// <remarks />
  public void FindPeopleAsync(FindPeopleType FindPeople1)
  {
    this.FindPeopleAsync(FindPeople1, (object) null);
  }

  /// <remarks />
  public void FindPeopleAsync(FindPeopleType FindPeople1, object userState)
  {
    if (this.FindPeopleOperationCompleted == null)
      this.FindPeopleOperationCompleted = new SendOrPostCallback(this.OnFindPeopleOperationCompleted);
    this.InvokeAsync("FindPeople", new object[1]
    {
      (object) FindPeople1
    }, this.FindPeopleOperationCompleted, userState);
  }

  private void OnFindPeopleOperationCompleted(object arg)
  {
    if (this.FindPeopleCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.FindPeopleCompleted((object) this, new FindPeopleCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetPersona", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetPersonaResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetPersonaResponseMessageType GetPersona([XmlElement("GetPersona", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetPersonaType GetPersona1)
  {
    return (GetPersonaResponseMessageType) this.Invoke(nameof (GetPersona), new object[1]
    {
      (object) GetPersona1
    })[0];
  }

  /// <remarks />
  public void GetPersonaAsync(GetPersonaType GetPersona1)
  {
    this.GetPersonaAsync(GetPersona1, (object) null);
  }

  /// <remarks />
  public void GetPersonaAsync(GetPersonaType GetPersona1, object userState)
  {
    if (this.GetPersonaOperationCompleted == null)
      this.GetPersonaOperationCompleted = new SendOrPostCallback(this.OnGetPersonaOperationCompleted);
    this.InvokeAsync("GetPersona", new object[1]
    {
      (object) GetPersona1
    }, this.GetPersonaOperationCompleted, userState);
  }

  private void OnGetPersonaOperationCompleted(object arg)
  {
    if (this.GetPersonaCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetPersonaCompleted((object) this, new GetPersonaCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("TimeZoneContext")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetInboxRules", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetInboxRulesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetInboxRulesResponseType GetInboxRules([XmlElement("GetInboxRules", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetInboxRulesRequestType GetInboxRules1)
  {
    return (GetInboxRulesResponseType) this.Invoke(nameof (GetInboxRules), new object[1]
    {
      (object) GetInboxRules1
    })[0];
  }

  /// <remarks />
  public void GetInboxRulesAsync(GetInboxRulesRequestType GetInboxRules1)
  {
    this.GetInboxRulesAsync(GetInboxRules1, (object) null);
  }

  /// <remarks />
  public void GetInboxRulesAsync(GetInboxRulesRequestType GetInboxRules1, object userState)
  {
    if (this.GetInboxRulesOperationCompleted == null)
      this.GetInboxRulesOperationCompleted = new SendOrPostCallback(this.OnGetInboxRulesOperationCompleted);
    this.InvokeAsync("GetInboxRules", new object[1]
    {
      (object) GetInboxRules1
    }, this.GetInboxRulesOperationCompleted, userState);
  }

  private void OnGetInboxRulesOperationCompleted(object arg)
  {
    if (this.GetInboxRulesCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetInboxRulesCompleted((object) this, new GetInboxRulesCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("TimeZoneContext")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateInboxRules", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("UpdateInboxRulesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public UpdateInboxRulesResponseType UpdateInboxRules([XmlElement("UpdateInboxRules", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateInboxRulesRequestType UpdateInboxRules1)
  {
    return (UpdateInboxRulesResponseType) this.Invoke(nameof (UpdateInboxRules), new object[1]
    {
      (object) UpdateInboxRules1
    })[0];
  }

  /// <remarks />
  public void UpdateInboxRulesAsync(UpdateInboxRulesRequestType UpdateInboxRules1)
  {
    this.UpdateInboxRulesAsync(UpdateInboxRules1, (object) null);
  }

  /// <remarks />
  public void UpdateInboxRulesAsync(UpdateInboxRulesRequestType UpdateInboxRules1, object userState)
  {
    if (this.UpdateInboxRulesOperationCompleted == null)
      this.UpdateInboxRulesOperationCompleted = new SendOrPostCallback(this.OnUpdateInboxRulesOperationCompleted);
    this.InvokeAsync("UpdateInboxRules", new object[1]
    {
      (object) UpdateInboxRules1
    }, this.UpdateInboxRulesOperationCompleted, userState);
  }

  private void OnUpdateInboxRulesOperationCompleted(object arg)
  {
    if (this.UpdateInboxRulesCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.UpdateInboxRulesCompleted((object) this, new UpdateInboxRulesCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetPasswordExpirationDate", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetPasswordExpirationDateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetPasswordExpirationDateResponseMessageType GetPasswordExpirationDate(
    [XmlElement("GetPasswordExpirationDate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetPasswordExpirationDateType GetPasswordExpirationDate1)
  {
    return (GetPasswordExpirationDateResponseMessageType) this.Invoke(nameof (GetPasswordExpirationDate), new object[1]
    {
      (object) GetPasswordExpirationDate1
    })[0];
  }

  /// <remarks />
  public void GetPasswordExpirationDateAsync(
    GetPasswordExpirationDateType GetPasswordExpirationDate1)
  {
    this.GetPasswordExpirationDateAsync(GetPasswordExpirationDate1, (object) null);
  }

  /// <remarks />
  public void GetPasswordExpirationDateAsync(
    GetPasswordExpirationDateType GetPasswordExpirationDate1,
    object userState)
  {
    if (this.GetPasswordExpirationDateOperationCompleted == null)
      this.GetPasswordExpirationDateOperationCompleted = new SendOrPostCallback(this.OnGetPasswordExpirationDateOperationCompleted);
    this.InvokeAsync("GetPasswordExpirationDate", new object[1]
    {
      (object) GetPasswordExpirationDate1
    }, this.GetPasswordExpirationDateOperationCompleted, userState);
  }

  private void OnGetPasswordExpirationDateOperationCompleted(object arg)
  {
    if (this.GetPasswordExpirationDateCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetPasswordExpirationDateCompleted((object) this, new GetPasswordExpirationDateCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ManagementRole")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetSearchableMailboxes", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetSearchableMailboxesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetSearchableMailboxesResponseMessageType GetSearchableMailboxes(
    [XmlElement("GetSearchableMailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetSearchableMailboxesType GetSearchableMailboxes1)
  {
    return (GetSearchableMailboxesResponseMessageType) this.Invoke(nameof (GetSearchableMailboxes), new object[1]
    {
      (object) GetSearchableMailboxes1
    })[0];
  }

  /// <remarks />
  public void GetSearchableMailboxesAsync(GetSearchableMailboxesType GetSearchableMailboxes1)
  {
    this.GetSearchableMailboxesAsync(GetSearchableMailboxes1, (object) null);
  }

  /// <remarks />
  public void GetSearchableMailboxesAsync(
    GetSearchableMailboxesType GetSearchableMailboxes1,
    object userState)
  {
    if (this.GetSearchableMailboxesOperationCompleted == null)
      this.GetSearchableMailboxesOperationCompleted = new SendOrPostCallback(this.OnGetSearchableMailboxesOperationCompleted);
    this.InvokeAsync("GetSearchableMailboxes", new object[1]
    {
      (object) GetSearchableMailboxes1
    }, this.GetSearchableMailboxesOperationCompleted, userState);
  }

  private void OnGetSearchableMailboxesOperationCompleted(object arg)
  {
    if (this.GetSearchableMailboxesCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetSearchableMailboxesCompleted((object) this, new GetSearchableMailboxesCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ManagementRole")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SearchMailboxes", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("SearchMailboxesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public SearchMailboxesResponseType SearchMailboxes([XmlElement("SearchMailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SearchMailboxesType SearchMailboxes1)
  {
    return (SearchMailboxesResponseType) this.Invoke(nameof (SearchMailboxes), new object[1]
    {
      (object) SearchMailboxes1
    })[0];
  }

  /// <remarks />
  public void SearchMailboxesAsync(SearchMailboxesType SearchMailboxes1)
  {
    this.SearchMailboxesAsync(SearchMailboxes1, (object) null);
  }

  /// <remarks />
  public void SearchMailboxesAsync(SearchMailboxesType SearchMailboxes1, object userState)
  {
    if (this.SearchMailboxesOperationCompleted == null)
      this.SearchMailboxesOperationCompleted = new SendOrPostCallback(this.OnSearchMailboxesOperationCompleted);
    this.InvokeAsync("SearchMailboxes", new object[1]
    {
      (object) SearchMailboxes1
    }, this.SearchMailboxesOperationCompleted, userState);
  }

  private void OnSearchMailboxesOperationCompleted(object arg)
  {
    if (this.SearchMailboxesCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.SearchMailboxesCompleted((object) this, new SearchMailboxesCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ManagementRole")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetDiscoverySearchConfiguration", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetDiscoverySearchConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetDiscoverySearchConfigurationResponseMessageType GetDiscoverySearchConfiguration(
    [XmlElement("GetDiscoverySearchConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetDiscoverySearchConfigurationType GetDiscoverySearchConfiguration1)
  {
    return (GetDiscoverySearchConfigurationResponseMessageType) this.Invoke(nameof (GetDiscoverySearchConfiguration), new object[1]
    {
      (object) GetDiscoverySearchConfiguration1
    })[0];
  }

  /// <remarks />
  public void GetDiscoverySearchConfigurationAsync(
    GetDiscoverySearchConfigurationType GetDiscoverySearchConfiguration1)
  {
    this.GetDiscoverySearchConfigurationAsync(GetDiscoverySearchConfiguration1, (object) null);
  }

  /// <remarks />
  public void GetDiscoverySearchConfigurationAsync(
    GetDiscoverySearchConfigurationType GetDiscoverySearchConfiguration1,
    object userState)
  {
    if (this.GetDiscoverySearchConfigurationOperationCompleted == null)
      this.GetDiscoverySearchConfigurationOperationCompleted = new SendOrPostCallback(this.OnGetDiscoverySearchConfigurationOperationCompleted);
    this.InvokeAsync("GetDiscoverySearchConfiguration", new object[1]
    {
      (object) GetDiscoverySearchConfiguration1
    }, this.GetDiscoverySearchConfigurationOperationCompleted, userState);
  }

  private void OnGetDiscoverySearchConfigurationOperationCompleted(object arg)
  {
    if (this.GetDiscoverySearchConfigurationCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetDiscoverySearchConfigurationCompleted((object) this, new GetDiscoverySearchConfigurationCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ManagementRole")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetHoldOnMailboxes", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetHoldOnMailboxesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetHoldOnMailboxesResponseMessageType GetHoldOnMailboxes(
    [XmlElement("GetHoldOnMailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetHoldOnMailboxesType GetHoldOnMailboxes1)
  {
    return (GetHoldOnMailboxesResponseMessageType) this.Invoke(nameof (GetHoldOnMailboxes), new object[1]
    {
      (object) GetHoldOnMailboxes1
    })[0];
  }

  /// <remarks />
  public void GetHoldOnMailboxesAsync(GetHoldOnMailboxesType GetHoldOnMailboxes1)
  {
    this.GetHoldOnMailboxesAsync(GetHoldOnMailboxes1, (object) null);
  }

  /// <remarks />
  public void GetHoldOnMailboxesAsync(GetHoldOnMailboxesType GetHoldOnMailboxes1, object userState)
  {
    if (this.GetHoldOnMailboxesOperationCompleted == null)
      this.GetHoldOnMailboxesOperationCompleted = new SendOrPostCallback(this.OnGetHoldOnMailboxesOperationCompleted);
    this.InvokeAsync("GetHoldOnMailboxes", new object[1]
    {
      (object) GetHoldOnMailboxes1
    }, this.GetHoldOnMailboxesOperationCompleted, userState);
  }

  private void OnGetHoldOnMailboxesOperationCompleted(object arg)
  {
    if (this.GetHoldOnMailboxesCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetHoldOnMailboxesCompleted((object) this, new GetHoldOnMailboxesCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ManagementRole")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SetHoldOnMailboxes", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("SetHoldOnMailboxesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public SetHoldOnMailboxesResponseMessageType SetHoldOnMailboxes(
    [XmlElement("SetHoldOnMailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SetHoldOnMailboxesType SetHoldOnMailboxes1)
  {
    return (SetHoldOnMailboxesResponseMessageType) this.Invoke(nameof (SetHoldOnMailboxes), new object[1]
    {
      (object) SetHoldOnMailboxes1
    })[0];
  }

  /// <remarks />
  public void SetHoldOnMailboxesAsync(SetHoldOnMailboxesType SetHoldOnMailboxes1)
  {
    this.SetHoldOnMailboxesAsync(SetHoldOnMailboxes1, (object) null);
  }

  /// <remarks />
  public void SetHoldOnMailboxesAsync(SetHoldOnMailboxesType SetHoldOnMailboxes1, object userState)
  {
    if (this.SetHoldOnMailboxesOperationCompleted == null)
      this.SetHoldOnMailboxesOperationCompleted = new SendOrPostCallback(this.OnSetHoldOnMailboxesOperationCompleted);
    this.InvokeAsync("SetHoldOnMailboxes", new object[1]
    {
      (object) SetHoldOnMailboxes1
    }, this.SetHoldOnMailboxesOperationCompleted, userState);
  }

  private void OnSetHoldOnMailboxesOperationCompleted(object arg)
  {
    if (this.SetHoldOnMailboxesCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.SetHoldOnMailboxesCompleted((object) this, new SetHoldOnMailboxesCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ManagementRole")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetNonIndexableItemStatistics", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetNonIndexableItemStatisticsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetNonIndexableItemStatisticsResponseMessageType GetNonIndexableItemStatistics(
    [XmlElement("GetNonIndexableItemStatistics", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetNonIndexableItemStatisticsType GetNonIndexableItemStatistics1)
  {
    return (GetNonIndexableItemStatisticsResponseMessageType) this.Invoke(nameof (GetNonIndexableItemStatistics), new object[1]
    {
      (object) GetNonIndexableItemStatistics1
    })[0];
  }

  /// <remarks />
  public void GetNonIndexableItemStatisticsAsync(
    GetNonIndexableItemStatisticsType GetNonIndexableItemStatistics1)
  {
    this.GetNonIndexableItemStatisticsAsync(GetNonIndexableItemStatistics1, (object) null);
  }

  /// <remarks />
  public void GetNonIndexableItemStatisticsAsync(
    GetNonIndexableItemStatisticsType GetNonIndexableItemStatistics1,
    object userState)
  {
    if (this.GetNonIndexableItemStatisticsOperationCompleted == null)
      this.GetNonIndexableItemStatisticsOperationCompleted = new SendOrPostCallback(this.OnGetNonIndexableItemStatisticsOperationCompleted);
    this.InvokeAsync("GetNonIndexableItemStatistics", new object[1]
    {
      (object) GetNonIndexableItemStatistics1
    }, this.GetNonIndexableItemStatisticsOperationCompleted, userState);
  }

  private void OnGetNonIndexableItemStatisticsOperationCompleted(object arg)
  {
    if (this.GetNonIndexableItemStatisticsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetNonIndexableItemStatisticsCompleted((object) this, new GetNonIndexableItemStatisticsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ManagementRole")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetNonIndexableItemDetails", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetNonIndexableItemDetailsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetNonIndexableItemDetailsResponseMessageType GetNonIndexableItemDetails(
    [XmlElement("GetNonIndexableItemDetails", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetNonIndexableItemDetailsType GetNonIndexableItemDetails1)
  {
    return (GetNonIndexableItemDetailsResponseMessageType) this.Invoke(nameof (GetNonIndexableItemDetails), new object[1]
    {
      (object) GetNonIndexableItemDetails1
    })[0];
  }

  /// <remarks />
  public void GetNonIndexableItemDetailsAsync(
    GetNonIndexableItemDetailsType GetNonIndexableItemDetails1)
  {
    this.GetNonIndexableItemDetailsAsync(GetNonIndexableItemDetails1, (object) null);
  }

  /// <remarks />
  public void GetNonIndexableItemDetailsAsync(
    GetNonIndexableItemDetailsType GetNonIndexableItemDetails1,
    object userState)
  {
    if (this.GetNonIndexableItemDetailsOperationCompleted == null)
      this.GetNonIndexableItemDetailsOperationCompleted = new SendOrPostCallback(this.OnGetNonIndexableItemDetailsOperationCompleted);
    this.InvokeAsync("GetNonIndexableItemDetails", new object[1]
    {
      (object) GetNonIndexableItemDetails1
    }, this.GetNonIndexableItemDetailsOperationCompleted, userState);
  }

  private void OnGetNonIndexableItemDetailsOperationCompleted(object arg)
  {
    if (this.GetNonIndexableItemDetailsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetNonIndexableItemDetailsCompleted((object) this, new GetNonIndexableItemDetailsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/MarkAllItemsAsRead", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("MarkAllItemsAsReadResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public MarkAllItemsAsReadResponseType MarkAllItemsAsRead(
    [XmlElement("MarkAllItemsAsRead", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] MarkAllItemsAsReadType MarkAllItemsAsRead1)
  {
    return (MarkAllItemsAsReadResponseType) this.Invoke(nameof (MarkAllItemsAsRead), new object[1]
    {
      (object) MarkAllItemsAsRead1
    })[0];
  }

  /// <remarks />
  public void MarkAllItemsAsReadAsync(MarkAllItemsAsReadType MarkAllItemsAsRead1)
  {
    this.MarkAllItemsAsReadAsync(MarkAllItemsAsRead1, (object) null);
  }

  /// <remarks />
  public void MarkAllItemsAsReadAsync(MarkAllItemsAsReadType MarkAllItemsAsRead1, object userState)
  {
    if (this.MarkAllItemsAsReadOperationCompleted == null)
      this.MarkAllItemsAsReadOperationCompleted = new SendOrPostCallback(this.OnMarkAllItemsAsReadOperationCompleted);
    this.InvokeAsync("MarkAllItemsAsRead", new object[1]
    {
      (object) MarkAllItemsAsRead1
    }, this.MarkAllItemsAsReadOperationCompleted, userState);
  }

  private void OnMarkAllItemsAsReadOperationCompleted(object arg)
  {
    if (this.MarkAllItemsAsReadCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.MarkAllItemsAsReadCompleted((object) this, new MarkAllItemsAsReadCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/MarkAsJunk", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("MarkAsJunkResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public MarkAsJunkResponseType MarkAsJunk([XmlElement("MarkAsJunk", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] MarkAsJunkType MarkAsJunk1)
  {
    return (MarkAsJunkResponseType) this.Invoke(nameof (MarkAsJunk), new object[1]
    {
      (object) MarkAsJunk1
    })[0];
  }

  /// <remarks />
  public void MarkAsJunkAsync(MarkAsJunkType MarkAsJunk1)
  {
    this.MarkAsJunkAsync(MarkAsJunk1, (object) null);
  }

  /// <remarks />
  public void MarkAsJunkAsync(MarkAsJunkType MarkAsJunk1, object userState)
  {
    if (this.MarkAsJunkOperationCompleted == null)
      this.MarkAsJunkOperationCompleted = new SendOrPostCallback(this.OnMarkAsJunkOperationCompleted);
    this.InvokeAsync("MarkAsJunk", new object[1]
    {
      (object) MarkAsJunk1
    }, this.MarkAsJunkOperationCompleted, userState);
  }

  private void OnMarkAsJunkOperationCompleted(object arg)
  {
    if (this.MarkAsJunkCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.MarkAsJunkCompleted((object) this, new MarkAsJunkCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetAppManifests", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetAppManifestsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetAppManifestsResponseType GetAppManifests([XmlElement("GetAppManifests", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetAppManifestsType GetAppManifests1)
  {
    return (GetAppManifestsResponseType) this.Invoke(nameof (GetAppManifests), new object[1]
    {
      (object) GetAppManifests1
    })[0];
  }

  /// <remarks />
  public void GetAppManifestsAsync(GetAppManifestsType GetAppManifests1)
  {
    this.GetAppManifestsAsync(GetAppManifests1, (object) null);
  }

  /// <remarks />
  public void GetAppManifestsAsync(GetAppManifestsType GetAppManifests1, object userState)
  {
    if (this.GetAppManifestsOperationCompleted == null)
      this.GetAppManifestsOperationCompleted = new SendOrPostCallback(this.OnGetAppManifestsOperationCompleted);
    this.InvokeAsync("GetAppManifests", new object[1]
    {
      (object) GetAppManifests1
    }, this.GetAppManifestsOperationCompleted, userState);
  }

  private void OnGetAppManifestsOperationCompleted(object arg)
  {
    if (this.GetAppManifestsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetAppManifestsCompleted((object) this, new GetAppManifestsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/AddNewImContactToGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("AddNewImContactToGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public AddNewImContactToGroupResponseMessageType AddNewImContactToGroup(
    [XmlElement("AddNewImContactToGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] AddNewImContactToGroupType AddNewImContactToGroup1)
  {
    return (AddNewImContactToGroupResponseMessageType) this.Invoke(nameof (AddNewImContactToGroup), new object[1]
    {
      (object) AddNewImContactToGroup1
    })[0];
  }

  /// <remarks />
  public void AddNewImContactToGroupAsync(AddNewImContactToGroupType AddNewImContactToGroup1)
  {
    this.AddNewImContactToGroupAsync(AddNewImContactToGroup1, (object) null);
  }

  /// <remarks />
  public void AddNewImContactToGroupAsync(
    AddNewImContactToGroupType AddNewImContactToGroup1,
    object userState)
  {
    if (this.AddNewImContactToGroupOperationCompleted == null)
      this.AddNewImContactToGroupOperationCompleted = new SendOrPostCallback(this.OnAddNewImContactToGroupOperationCompleted);
    this.InvokeAsync("AddNewImContactToGroup", new object[1]
    {
      (object) AddNewImContactToGroup1
    }, this.AddNewImContactToGroupOperationCompleted, userState);
  }

  private void OnAddNewImContactToGroupOperationCompleted(object arg)
  {
    if (this.AddNewImContactToGroupCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.AddNewImContactToGroupCompleted((object) this, new AddNewImContactToGroupCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/AddNewTelUriContactToGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("AddNewTelUriContactToGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public AddNewTelUriContactToGroupResponseMessageType AddNewTelUriContactToGroup(
    [XmlElement("AddNewTelUriContactToGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] AddNewTelUriContactToGroupType AddNewTelUriContactToGroup1)
  {
    return (AddNewTelUriContactToGroupResponseMessageType) this.Invoke(nameof (AddNewTelUriContactToGroup), new object[1]
    {
      (object) AddNewTelUriContactToGroup1
    })[0];
  }

  /// <remarks />
  public void AddNewTelUriContactToGroupAsync(
    AddNewTelUriContactToGroupType AddNewTelUriContactToGroup1)
  {
    this.AddNewTelUriContactToGroupAsync(AddNewTelUriContactToGroup1, (object) null);
  }

  /// <remarks />
  public void AddNewTelUriContactToGroupAsync(
    AddNewTelUriContactToGroupType AddNewTelUriContactToGroup1,
    object userState)
  {
    if (this.AddNewTelUriContactToGroupOperationCompleted == null)
      this.AddNewTelUriContactToGroupOperationCompleted = new SendOrPostCallback(this.OnAddNewTelUriContactToGroupOperationCompleted);
    this.InvokeAsync("AddNewTelUriContactToGroup", new object[1]
    {
      (object) AddNewTelUriContactToGroup1
    }, this.AddNewTelUriContactToGroupOperationCompleted, userState);
  }

  private void OnAddNewTelUriContactToGroupOperationCompleted(object arg)
  {
    if (this.AddNewTelUriContactToGroupCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.AddNewTelUriContactToGroupCompleted((object) this, new AddNewTelUriContactToGroupCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/AddImContactToGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("AddImContactToGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public AddImContactToGroupResponseMessageType AddImContactToGroup(
    [XmlElement("AddImContactToGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] AddImContactToGroupType AddImContactToGroup1)
  {
    return (AddImContactToGroupResponseMessageType) this.Invoke(nameof (AddImContactToGroup), new object[1]
    {
      (object) AddImContactToGroup1
    })[0];
  }

  /// <remarks />
  public void AddImContactToGroupAsync(AddImContactToGroupType AddImContactToGroup1)
  {
    this.AddImContactToGroupAsync(AddImContactToGroup1, (object) null);
  }

  /// <remarks />
  public void AddImContactToGroupAsync(
    AddImContactToGroupType AddImContactToGroup1,
    object userState)
  {
    if (this.AddImContactToGroupOperationCompleted == null)
      this.AddImContactToGroupOperationCompleted = new SendOrPostCallback(this.OnAddImContactToGroupOperationCompleted);
    this.InvokeAsync("AddImContactToGroup", new object[1]
    {
      (object) AddImContactToGroup1
    }, this.AddImContactToGroupOperationCompleted, userState);
  }

  private void OnAddImContactToGroupOperationCompleted(object arg)
  {
    if (this.AddImContactToGroupCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.AddImContactToGroupCompleted((object) this, new AddImContactToGroupCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/RemoveImContactFromGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("RemoveImContactFromGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public RemoveImContactFromGroupResponseMessageType RemoveImContactFromGroup(
    [XmlElement("RemoveImContactFromGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] RemoveImContactFromGroupType RemoveImContactFromGroup1)
  {
    return (RemoveImContactFromGroupResponseMessageType) this.Invoke(nameof (RemoveImContactFromGroup), new object[1]
    {
      (object) RemoveImContactFromGroup1
    })[0];
  }

  /// <remarks />
  public void RemoveImContactFromGroupAsync(
    RemoveImContactFromGroupType RemoveImContactFromGroup1)
  {
    this.RemoveImContactFromGroupAsync(RemoveImContactFromGroup1, (object) null);
  }

  /// <remarks />
  public void RemoveImContactFromGroupAsync(
    RemoveImContactFromGroupType RemoveImContactFromGroup1,
    object userState)
  {
    if (this.RemoveImContactFromGroupOperationCompleted == null)
      this.RemoveImContactFromGroupOperationCompleted = new SendOrPostCallback(this.OnRemoveImContactFromGroupOperationCompleted);
    this.InvokeAsync("RemoveImContactFromGroup", new object[1]
    {
      (object) RemoveImContactFromGroup1
    }, this.RemoveImContactFromGroupOperationCompleted, userState);
  }

  private void OnRemoveImContactFromGroupOperationCompleted(object arg)
  {
    if (this.RemoveImContactFromGroupCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.RemoveImContactFromGroupCompleted((object) this, new RemoveImContactFromGroupCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/AddImGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("AddImGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public AddImGroupResponseMessageType AddImGroup([XmlElement("AddImGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] AddImGroupType AddImGroup1)
  {
    return (AddImGroupResponseMessageType) this.Invoke(nameof (AddImGroup), new object[1]
    {
      (object) AddImGroup1
    })[0];
  }

  /// <remarks />
  public void AddImGroupAsync(AddImGroupType AddImGroup1)
  {
    this.AddImGroupAsync(AddImGroup1, (object) null);
  }

  /// <remarks />
  public void AddImGroupAsync(AddImGroupType AddImGroup1, object userState)
  {
    if (this.AddImGroupOperationCompleted == null)
      this.AddImGroupOperationCompleted = new SendOrPostCallback(this.OnAddImGroupOperationCompleted);
    this.InvokeAsync("AddImGroup", new object[1]
    {
      (object) AddImGroup1
    }, this.AddImGroupOperationCompleted, userState);
  }

  private void OnAddImGroupOperationCompleted(object arg)
  {
    if (this.AddImGroupCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.AddImGroupCompleted((object) this, new AddImGroupCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/AddDistributionGroupToImList", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("AddDistributionGroupToImListResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public AddDistributionGroupToImListResponseMessageType AddDistributionGroupToImList(
    [XmlElement("AddDistributionGroupToImList", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] AddDistributionGroupToImListType AddDistributionGroupToImList1)
  {
    return (AddDistributionGroupToImListResponseMessageType) this.Invoke(nameof (AddDistributionGroupToImList), new object[1]
    {
      (object) AddDistributionGroupToImList1
    })[0];
  }

  /// <remarks />
  public void AddDistributionGroupToImListAsync(
    AddDistributionGroupToImListType AddDistributionGroupToImList1)
  {
    this.AddDistributionGroupToImListAsync(AddDistributionGroupToImList1, (object) null);
  }

  /// <remarks />
  public void AddDistributionGroupToImListAsync(
    AddDistributionGroupToImListType AddDistributionGroupToImList1,
    object userState)
  {
    if (this.AddDistributionGroupToImListOperationCompleted == null)
      this.AddDistributionGroupToImListOperationCompleted = new SendOrPostCallback(this.OnAddDistributionGroupToImListOperationCompleted);
    this.InvokeAsync("AddDistributionGroupToImList", new object[1]
    {
      (object) AddDistributionGroupToImList1
    }, this.AddDistributionGroupToImListOperationCompleted, userState);
  }

  private void OnAddDistributionGroupToImListOperationCompleted(object arg)
  {
    if (this.AddDistributionGroupToImListCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.AddDistributionGroupToImListCompleted((object) this, new AddDistributionGroupToImListCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetImItemList", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetImItemListResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetImItemListResponseMessageType GetImItemList([XmlElement("GetImItemList", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetImItemListType GetImItemList1)
  {
    return (GetImItemListResponseMessageType) this.Invoke(nameof (GetImItemList), new object[1]
    {
      (object) GetImItemList1
    })[0];
  }

  /// <remarks />
  public void GetImItemListAsync(GetImItemListType GetImItemList1)
  {
    this.GetImItemListAsync(GetImItemList1, (object) null);
  }

  /// <remarks />
  public void GetImItemListAsync(GetImItemListType GetImItemList1, object userState)
  {
    if (this.GetImItemListOperationCompleted == null)
      this.GetImItemListOperationCompleted = new SendOrPostCallback(this.OnGetImItemListOperationCompleted);
    this.InvokeAsync("GetImItemList", new object[1]
    {
      (object) GetImItemList1
    }, this.GetImItemListOperationCompleted, userState);
  }

  private void OnGetImItemListOperationCompleted(object arg)
  {
    if (this.GetImItemListCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetImItemListCompleted((object) this, new GetImItemListCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetImItems", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetImItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetImItemsResponseMessageType GetImItems([XmlElement("GetImItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetImItemsType GetImItems1)
  {
    return (GetImItemsResponseMessageType) this.Invoke(nameof (GetImItems), new object[1]
    {
      (object) GetImItems1
    })[0];
  }

  /// <remarks />
  public void GetImItemsAsync(GetImItemsType GetImItems1)
  {
    this.GetImItemsAsync(GetImItems1, (object) null);
  }

  /// <remarks />
  public void GetImItemsAsync(GetImItemsType GetImItems1, object userState)
  {
    if (this.GetImItemsOperationCompleted == null)
      this.GetImItemsOperationCompleted = new SendOrPostCallback(this.OnGetImItemsOperationCompleted);
    this.InvokeAsync("GetImItems", new object[1]
    {
      (object) GetImItems1
    }, this.GetImItemsOperationCompleted, userState);
  }

  private void OnGetImItemsOperationCompleted(object arg)
  {
    if (this.GetImItemsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetImItemsCompleted((object) this, new GetImItemsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/RemoveContactFromImList", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("RemoveContactFromImListResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public RemoveContactFromImListResponseMessageType RemoveContactFromImList(
    [XmlElement("RemoveContactFromImList", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] RemoveContactFromImListType RemoveContactFromImList1)
  {
    return (RemoveContactFromImListResponseMessageType) this.Invoke(nameof (RemoveContactFromImList), new object[1]
    {
      (object) RemoveContactFromImList1
    })[0];
  }

  /// <remarks />
  public void RemoveContactFromImListAsync(
    RemoveContactFromImListType RemoveContactFromImList1)
  {
    this.RemoveContactFromImListAsync(RemoveContactFromImList1, (object) null);
  }

  /// <remarks />
  public void RemoveContactFromImListAsync(
    RemoveContactFromImListType RemoveContactFromImList1,
    object userState)
  {
    if (this.RemoveContactFromImListOperationCompleted == null)
      this.RemoveContactFromImListOperationCompleted = new SendOrPostCallback(this.OnRemoveContactFromImListOperationCompleted);
    this.InvokeAsync("RemoveContactFromImList", new object[1]
    {
      (object) RemoveContactFromImList1
    }, this.RemoveContactFromImListOperationCompleted, userState);
  }

  private void OnRemoveContactFromImListOperationCompleted(object arg)
  {
    if (this.RemoveContactFromImListCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.RemoveContactFromImListCompleted((object) this, new RemoveContactFromImListCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/RemoveDistributionGroupFromImList", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("RemoveDistributionGroupFromImListResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public RemoveDistributionGroupFromImListResponseMessageType RemoveDistributionGroupFromImList(
    [XmlElement("RemoveDistributionGroupFromImList", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] RemoveDistributionGroupFromImListType RemoveDistributionGroupFromImList1)
  {
    return (RemoveDistributionGroupFromImListResponseMessageType) this.Invoke(nameof (RemoveDistributionGroupFromImList), new object[1]
    {
      (object) RemoveDistributionGroupFromImList1
    })[0];
  }

  /// <remarks />
  public void RemoveDistributionGroupFromImListAsync(
    RemoveDistributionGroupFromImListType RemoveDistributionGroupFromImList1)
  {
    this.RemoveDistributionGroupFromImListAsync(RemoveDistributionGroupFromImList1, (object) null);
  }

  /// <remarks />
  public void RemoveDistributionGroupFromImListAsync(
    RemoveDistributionGroupFromImListType RemoveDistributionGroupFromImList1,
    object userState)
  {
    if (this.RemoveDistributionGroupFromImListOperationCompleted == null)
      this.RemoveDistributionGroupFromImListOperationCompleted = new SendOrPostCallback(this.OnRemoveDistributionGroupFromImListOperationCompleted);
    this.InvokeAsync("RemoveDistributionGroupFromImList", new object[1]
    {
      (object) RemoveDistributionGroupFromImList1
    }, this.RemoveDistributionGroupFromImListOperationCompleted, userState);
  }

  private void OnRemoveDistributionGroupFromImListOperationCompleted(object arg)
  {
    if (this.RemoveDistributionGroupFromImListCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.RemoveDistributionGroupFromImListCompleted((object) this, new RemoveDistributionGroupFromImListCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/RemoveImGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("RemoveImGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public RemoveImGroupResponseMessageType RemoveImGroup([XmlElement("RemoveImGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] RemoveImGroupType RemoveImGroup1)
  {
    return (RemoveImGroupResponseMessageType) this.Invoke(nameof (RemoveImGroup), new object[1]
    {
      (object) RemoveImGroup1
    })[0];
  }

  /// <remarks />
  public void RemoveImGroupAsync(RemoveImGroupType RemoveImGroup1)
  {
    this.RemoveImGroupAsync(RemoveImGroup1, (object) null);
  }

  /// <remarks />
  public void RemoveImGroupAsync(RemoveImGroupType RemoveImGroup1, object userState)
  {
    if (this.RemoveImGroupOperationCompleted == null)
      this.RemoveImGroupOperationCompleted = new SendOrPostCallback(this.OnRemoveImGroupOperationCompleted);
    this.InvokeAsync("RemoveImGroup", new object[1]
    {
      (object) RemoveImGroup1
    }, this.RemoveImGroupOperationCompleted, userState);
  }

  private void OnRemoveImGroupOperationCompleted(object arg)
  {
    if (this.RemoveImGroupCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.RemoveImGroupCompleted((object) this, new RemoveImGroupCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SetImGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("SetImGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public SetImGroupResponseMessageType SetImGroup([XmlElement("SetImGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SetImGroupType SetImGroup1)
  {
    return (SetImGroupResponseMessageType) this.Invoke(nameof (SetImGroup), new object[1]
    {
      (object) SetImGroup1
    })[0];
  }

  /// <remarks />
  public void SetImGroupAsync(SetImGroupType SetImGroup1)
  {
    this.SetImGroupAsync(SetImGroup1, (object) null);
  }

  /// <remarks />
  public void SetImGroupAsync(SetImGroupType SetImGroup1, object userState)
  {
    if (this.SetImGroupOperationCompleted == null)
      this.SetImGroupOperationCompleted = new SendOrPostCallback(this.OnSetImGroupOperationCompleted);
    this.InvokeAsync("SetImGroup", new object[1]
    {
      (object) SetImGroup1
    }, this.SetImGroupOperationCompleted, userState);
  }

  private void OnSetImGroupOperationCompleted(object arg)
  {
    if (this.SetImGroupCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.SetImGroupCompleted((object) this, new SetImGroupCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("MailboxCulture")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapHeader("ExchangeImpersonation")]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SetImListMigrationCompleted", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("SetImListMigrationCompletedResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public SetImListMigrationCompletedResponseMessageType SetImListMigrationCompleted(
    [XmlElement("SetImListMigrationCompleted", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SetImListMigrationCompletedType SetImListMigrationCompleted1)
  {
    return (SetImListMigrationCompletedResponseMessageType) this.Invoke(nameof (SetImListMigrationCompleted), new object[1]
    {
      (object) SetImListMigrationCompleted1
    })[0];
  }

  /// <remarks />
  public void SetImListMigrationCompletedAsync(
    SetImListMigrationCompletedType SetImListMigrationCompleted1)
  {
    this.SetImListMigrationCompletedAsync(SetImListMigrationCompleted1, (object) null);
  }

  /// <remarks />
  public void SetImListMigrationCompletedAsync(
    SetImListMigrationCompletedType SetImListMigrationCompleted1,
    object userState)
  {
    if (this.SetImListMigrationCompletedOperationCompleted == null)
      this.SetImListMigrationCompletedOperationCompleted = new SendOrPostCallback(this.OnSetImListMigrationCompletedOperationCompleted);
    this.InvokeAsync("SetImListMigrationCompleted", new object[1]
    {
      (object) SetImListMigrationCompleted1
    }, this.SetImListMigrationCompletedOperationCompleted, userState);
  }

  private void OnSetImListMigrationCompletedOperationCompleted(object arg)
  {
    if (this.SetImListMigrationCompletedCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.SetImListMigrationCompletedCompleted((object) this, new SetImListMigrationCompletedCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUserRetentionPolicyTags", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetUserRetentionPolicyTagsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetUserRetentionPolicyTagsResponseMessageType GetUserRetentionPolicyTags(
    [XmlElement("GetUserRetentionPolicyTags", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUserRetentionPolicyTagsType GetUserRetentionPolicyTags1)
  {
    return (GetUserRetentionPolicyTagsResponseMessageType) this.Invoke(nameof (GetUserRetentionPolicyTags), new object[1]
    {
      (object) GetUserRetentionPolicyTags1
    })[0];
  }

  /// <remarks />
  public void GetUserRetentionPolicyTagsAsync(
    GetUserRetentionPolicyTagsType GetUserRetentionPolicyTags1)
  {
    this.GetUserRetentionPolicyTagsAsync(GetUserRetentionPolicyTags1, (object) null);
  }

  /// <remarks />
  public void GetUserRetentionPolicyTagsAsync(
    GetUserRetentionPolicyTagsType GetUserRetentionPolicyTags1,
    object userState)
  {
    if (this.GetUserRetentionPolicyTagsOperationCompleted == null)
      this.GetUserRetentionPolicyTagsOperationCompleted = new SendOrPostCallback(this.OnGetUserRetentionPolicyTagsOperationCompleted);
    this.InvokeAsync("GetUserRetentionPolicyTags", new object[1]
    {
      (object) GetUserRetentionPolicyTags1
    }, this.GetUserRetentionPolicyTagsOperationCompleted, userState);
  }

  private void OnGetUserRetentionPolicyTagsOperationCompleted(object arg)
  {
    if (this.GetUserRetentionPolicyTagsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetUserRetentionPolicyTagsCompleted((object) this, new GetUserRetentionPolicyTagsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/InstallApp", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("InstallAppResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public InstallAppResponseType InstallApp([XmlElement("InstallApp", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] InstallAppType InstallApp1)
  {
    return (InstallAppResponseType) this.Invoke(nameof (InstallApp), new object[1]
    {
      (object) InstallApp1
    })[0];
  }

  /// <remarks />
  public void InstallAppAsync(InstallAppType InstallApp1)
  {
    this.InstallAppAsync(InstallApp1, (object) null);
  }

  /// <remarks />
  public void InstallAppAsync(InstallAppType InstallApp1, object userState)
  {
    if (this.InstallAppOperationCompleted == null)
      this.InstallAppOperationCompleted = new SendOrPostCallback(this.OnInstallAppOperationCompleted);
    this.InvokeAsync("InstallApp", new object[1]
    {
      (object) InstallApp1
    }, this.InstallAppOperationCompleted, userState);
  }

  private void OnInstallAppOperationCompleted(object arg)
  {
    if (this.InstallAppCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.InstallAppCompleted((object) this, new InstallAppCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UninstallApp", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("UninstallAppResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public UninstallAppResponseType UninstallApp([XmlElement("UninstallApp", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UninstallAppType UninstallApp1)
  {
    return (UninstallAppResponseType) this.Invoke(nameof (UninstallApp), new object[1]
    {
      (object) UninstallApp1
    })[0];
  }

  /// <remarks />
  public void UninstallAppAsync(UninstallAppType UninstallApp1)
  {
    this.UninstallAppAsync(UninstallApp1, (object) null);
  }

  /// <remarks />
  public void UninstallAppAsync(UninstallAppType UninstallApp1, object userState)
  {
    if (this.UninstallAppOperationCompleted == null)
      this.UninstallAppOperationCompleted = new SendOrPostCallback(this.OnUninstallAppOperationCompleted);
    this.InvokeAsync("UninstallApp", new object[1]
    {
      (object) UninstallApp1
    }, this.UninstallAppOperationCompleted, userState);
  }

  private void OnUninstallAppOperationCompleted(object arg)
  {
    if (this.UninstallAppCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.UninstallAppCompleted((object) this, new UninstallAppCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DisableApp", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("DisableAppResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public DisableAppResponseType DisableApp([XmlElement("DisableApp", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DisableAppType DisableApp1)
  {
    return (DisableAppResponseType) this.Invoke(nameof (DisableApp), new object[1]
    {
      (object) DisableApp1
    })[0];
  }

  /// <remarks />
  public void DisableAppAsync(DisableAppType DisableApp1)
  {
    this.DisableAppAsync(DisableApp1, (object) null);
  }

  /// <remarks />
  public void DisableAppAsync(DisableAppType DisableApp1, object userState)
  {
    if (this.DisableAppOperationCompleted == null)
      this.DisableAppOperationCompleted = new SendOrPostCallback(this.OnDisableAppOperationCompleted);
    this.InvokeAsync("DisableApp", new object[1]
    {
      (object) DisableApp1
    }, this.DisableAppOperationCompleted, userState);
  }

  private void OnDisableAppOperationCompleted(object arg)
  {
    if (this.DisableAppCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.DisableAppCompleted((object) this, new DisableAppCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetAppMarketplaceUrl", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetAppMarketplaceUrlResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetAppMarketplaceUrlResponseMessageType GetAppMarketplaceUrl(
    [XmlElement("GetAppMarketplaceUrl", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetAppMarketplaceUrlType GetAppMarketplaceUrl1)
  {
    return (GetAppMarketplaceUrlResponseMessageType) this.Invoke(nameof (GetAppMarketplaceUrl), new object[1]
    {
      (object) GetAppMarketplaceUrl1
    })[0];
  }

  /// <remarks />
  public void GetAppMarketplaceUrlAsync(GetAppMarketplaceUrlType GetAppMarketplaceUrl1)
  {
    this.GetAppMarketplaceUrlAsync(GetAppMarketplaceUrl1, (object) null);
  }

  /// <remarks />
  public void GetAppMarketplaceUrlAsync(
    GetAppMarketplaceUrlType GetAppMarketplaceUrl1,
    object userState)
  {
    if (this.GetAppMarketplaceUrlOperationCompleted == null)
      this.GetAppMarketplaceUrlOperationCompleted = new SendOrPostCallback(this.OnGetAppMarketplaceUrlOperationCompleted);
    this.InvokeAsync("GetAppMarketplaceUrl", new object[1]
    {
      (object) GetAppMarketplaceUrl1
    }, this.GetAppMarketplaceUrlOperationCompleted, userState);
  }

  private void OnGetAppMarketplaceUrlOperationCompleted(object arg)
  {
    if (this.GetAppMarketplaceUrlCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetAppMarketplaceUrlCompleted((object) this, new GetAppMarketplaceUrlCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  [SoapHeader("RequestServerVersionValue")]
  [SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
  [SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUserPhoto", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
  [return: XmlElement("GetUserPhotoResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
  public GetUserPhotoResponseMessageType GetUserPhoto([XmlElement("GetUserPhoto", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUserPhotoType GetUserPhoto1)
  {
    return (GetUserPhotoResponseMessageType) this.Invoke(nameof (GetUserPhoto), new object[1]
    {
      (object) GetUserPhoto1
    })[0];
  }

  /// <remarks />
  public void GetUserPhotoAsync(GetUserPhotoType GetUserPhoto1)
  {
    this.GetUserPhotoAsync(GetUserPhoto1, (object) null);
  }

  /// <remarks />
  public void GetUserPhotoAsync(GetUserPhotoType GetUserPhoto1, object userState)
  {
    if (this.GetUserPhotoOperationCompleted == null)
      this.GetUserPhotoOperationCompleted = new SendOrPostCallback(this.OnGetUserPhotoOperationCompleted);
    this.InvokeAsync("GetUserPhoto", new object[1]
    {
      (object) GetUserPhoto1
    }, this.GetUserPhotoOperationCompleted, userState);
  }

  private void OnGetUserPhotoOperationCompleted(object arg)
  {
    if (this.GetUserPhotoCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetUserPhotoCompleted((object) this, new GetUserPhotoCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  /// <remarks />
  public new void CancelAsync(object userState) => base.CancelAsync(userState);

  private bool IsLocalFileSystemWebService(string url)
  {
    if (url == null || url == string.Empty)
      return false;
    Uri uri = new Uri(url);
    return uri.Port >= 1024 /*0x0400*/ && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0;
  }

  protected new virtual object[] Invoke(string methodName, object[] parameters)
  {
    return base.Invoke(methodName, parameters);
  }
}
