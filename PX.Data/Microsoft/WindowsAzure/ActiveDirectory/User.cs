// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.ActiveDirectory.User
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Data.Services.Common;

#nullable disable
namespace Microsoft.WindowsAzure.ActiveDirectory;

[DataServiceKey("objectId")]
public class User : DirectoryObject
{
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private bool? _accountEnabled;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<AssignedLicense> _assignedLicenses = new Collection<AssignedLicense>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<AssignedPlan> _assignedPlans = new Collection<AssignedPlan>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _city;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _country;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _department;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private bool? _dirSyncEnabled;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _displayName;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _facsimileTelephoneNumber;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _givenName;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _immutableId;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _jobTitle;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private DateTime? _lastDirSyncTime;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _mail;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _mailNickname;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _mobile;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<string> _otherMails = new Collection<string>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _passwordPolicies;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private PasswordProfile _passwordProfile;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private bool _passwordProfileInitialized;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _physicalDeliveryOfficeName;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _postalCode;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _preferredLanguage;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<ProvisionedPlan> _provisionedPlans = new Collection<ProvisionedPlan>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<ProvisioningError> _provisioningErrors = new Collection<ProvisioningError>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<string> _proxyAddresses = new Collection<string>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _state;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _streetAddress;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _surname;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _telephoneNumber;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private DataServiceStreamLink _thumbnailPhoto;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _usageLocation;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _userPrincipalName;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _userType;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<Permission> _permissions = new Collection<Permission>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<DirectoryObject> _registeredDevices = new Collection<DirectoryObject>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<DirectoryObject> _ownedDevices = new Collection<DirectoryObject>();

  /// <summary>Create a new User object.</summary>
  /// <param name="objectId">Initial value of objectId.</param>
  /// <param name="assignedLicenses">Initial value of assignedLicenses.</param>
  /// <param name="assignedPlans">Initial value of assignedPlans.</param>
  /// <param name="otherMails">Initial value of otherMails.</param>
  /// <param name="provisionedPlans">Initial value of provisionedPlans.</param>
  /// <param name="provisioningErrors">Initial value of provisioningErrors.</param>
  /// <param name="proxyAddresses">Initial value of proxyAddresses.</param>
  /// <param name="thumbnailPhoto">Initial value of thumbnailPhoto.</param>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public static User CreateUser(
    string objectId,
    Collection<AssignedLicense> assignedLicenses,
    Collection<AssignedPlan> assignedPlans,
    Collection<string> otherMails,
    Collection<ProvisionedPlan> provisionedPlans,
    Collection<ProvisioningError> provisioningErrors,
    Collection<string> proxyAddresses,
    DataServiceStreamLink thumbnailPhoto)
  {
    User user = new User();
    user.objectId = objectId;
    user.assignedLicenses = assignedLicenses != null ? assignedLicenses : throw new ArgumentNullException(nameof (assignedLicenses));
    user.assignedPlans = assignedPlans != null ? assignedPlans : throw new ArgumentNullException(nameof (assignedPlans));
    user.otherMails = otherMails != null ? otherMails : throw new ArgumentNullException(nameof (otherMails));
    user.provisionedPlans = provisionedPlans != null ? provisionedPlans : throw new ArgumentNullException(nameof (provisionedPlans));
    user.provisioningErrors = provisioningErrors != null ? provisioningErrors : throw new ArgumentNullException(nameof (provisioningErrors));
    user.proxyAddresses = proxyAddresses != null ? proxyAddresses : throw new ArgumentNullException(nameof (proxyAddresses));
    user.thumbnailPhoto = thumbnailPhoto;
    return user;
  }

  /// <summary>
  /// There are no comments for Property accountEnabled in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public bool? accountEnabled
  {
    get => this._accountEnabled;
    set => this._accountEnabled = value;
  }

  /// <summary>
  /// There are no comments for Property assignedLicenses in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<AssignedLicense> assignedLicenses
  {
    get => this._assignedLicenses;
    set => this._assignedLicenses = value;
  }

  /// <summary>
  /// There are no comments for Property assignedPlans in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<AssignedPlan> assignedPlans
  {
    get => this._assignedPlans;
    set => this._assignedPlans = value;
  }

  /// <summary>
  /// There are no comments for Property city in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string city
  {
    get => this._city;
    set => this._city = value;
  }

  /// <summary>
  /// There are no comments for Property country in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string country
  {
    get => this._country;
    set => this._country = value;
  }

  /// <summary>
  /// There are no comments for Property department in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string department
  {
    get => this._department;
    set => this._department = value;
  }

  /// <summary>
  /// There are no comments for Property dirSyncEnabled in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public bool? dirSyncEnabled
  {
    get => this._dirSyncEnabled;
    set => this._dirSyncEnabled = value;
  }

  /// <summary>
  /// There are no comments for Property displayName in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string displayName
  {
    get => this._displayName;
    set => this._displayName = value;
  }

  /// <summary>
  /// There are no comments for Property facsimileTelephoneNumber in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string facsimileTelephoneNumber
  {
    get => this._facsimileTelephoneNumber;
    set => this._facsimileTelephoneNumber = value;
  }

  /// <summary>
  /// There are no comments for Property givenName in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string givenName
  {
    get => this._givenName;
    set => this._givenName = value;
  }

  /// <summary>
  /// There are no comments for Property immutableId in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string immutableId
  {
    get => this._immutableId;
    set => this._immutableId = value;
  }

  /// <summary>
  /// There are no comments for Property jobTitle in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string jobTitle
  {
    get => this._jobTitle;
    set => this._jobTitle = value;
  }

  /// <summary>
  /// There are no comments for Property lastDirSyncTime in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public DateTime? lastDirSyncTime
  {
    get => this._lastDirSyncTime;
    set => this._lastDirSyncTime = value;
  }

  /// <summary>
  /// There are no comments for Property mail in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string mail
  {
    get => this._mail;
    set => this._mail = value;
  }

  /// <summary>
  /// There are no comments for Property mailNickname in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string mailNickname
  {
    get => this._mailNickname;
    set => this._mailNickname = value;
  }

  /// <summary>
  /// There are no comments for Property mobile in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string mobile
  {
    get => this._mobile;
    set => this._mobile = value;
  }

  /// <summary>
  /// There are no comments for Property otherMails in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<string> otherMails
  {
    get => this._otherMails;
    set => this._otherMails = value;
  }

  /// <summary>
  /// There are no comments for Property passwordPolicies in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string passwordPolicies
  {
    get => this._passwordPolicies;
    set => this._passwordPolicies = value;
  }

  /// <summary>
  /// There are no comments for Property passwordProfile in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public PasswordProfile passwordProfile
  {
    get
    {
      if (this._passwordProfile == null && !this._passwordProfileInitialized)
      {
        this._passwordProfile = new PasswordProfile();
        this._passwordProfileInitialized = true;
      }
      return this._passwordProfile;
    }
    set
    {
      this._passwordProfile = value;
      this._passwordProfileInitialized = true;
    }
  }

  /// <summary>
  /// There are no comments for Property physicalDeliveryOfficeName in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string physicalDeliveryOfficeName
  {
    get => this._physicalDeliveryOfficeName;
    set => this._physicalDeliveryOfficeName = value;
  }

  /// <summary>
  /// There are no comments for Property postalCode in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string postalCode
  {
    get => this._postalCode;
    set => this._postalCode = value;
  }

  /// <summary>
  /// There are no comments for Property preferredLanguage in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string preferredLanguage
  {
    get => this._preferredLanguage;
    set => this._preferredLanguage = value;
  }

  /// <summary>
  /// There are no comments for Property provisionedPlans in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<ProvisionedPlan> provisionedPlans
  {
    get => this._provisionedPlans;
    set => this._provisionedPlans = value;
  }

  /// <summary>
  /// There are no comments for Property provisioningErrors in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<ProvisioningError> provisioningErrors
  {
    get => this._provisioningErrors;
    set => this._provisioningErrors = value;
  }

  /// <summary>
  /// There are no comments for Property proxyAddresses in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<string> proxyAddresses
  {
    get => this._proxyAddresses;
    set => this._proxyAddresses = value;
  }

  /// <summary>
  /// There are no comments for Property state in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string state
  {
    get => this._state;
    set => this._state = value;
  }

  /// <summary>
  /// There are no comments for Property streetAddress in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string streetAddress
  {
    get => this._streetAddress;
    set => this._streetAddress = value;
  }

  /// <summary>
  /// There are no comments for Property surname in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string surname
  {
    get => this._surname;
    set => this._surname = value;
  }

  /// <summary>
  /// There are no comments for Property telephoneNumber in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string telephoneNumber
  {
    get => this._telephoneNumber;
    set => this._telephoneNumber = value;
  }

  /// <summary>
  /// There are no comments for Property thumbnailPhoto in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public DataServiceStreamLink thumbnailPhoto
  {
    get => this._thumbnailPhoto;
    set => this._thumbnailPhoto = value;
  }

  /// <summary>
  /// There are no comments for Property usageLocation in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string usageLocation
  {
    get => this._usageLocation;
    set => this._usageLocation = value;
  }

  /// <summary>
  /// There are no comments for Property userPrincipalName in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string userPrincipalName
  {
    get => this._userPrincipalName;
    set => this._userPrincipalName = value;
  }

  /// <summary>
  /// There are no comments for Property userType in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string userType
  {
    get => this._userType;
    set => this._userType = value;
  }

  /// <summary>There are no comments for permissions in the schema.</summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<Permission> permissions
  {
    get => this._permissions;
    set
    {
      if (value == null)
        return;
      this._permissions = value;
    }
  }

  /// <summary>
  /// There are no comments for registeredDevices in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<DirectoryObject> registeredDevices
  {
    get => this._registeredDevices;
    set
    {
      if (value == null)
        return;
      this._registeredDevices = value;
    }
  }

  /// <summary>There are no comments for ownedDevices in the schema.</summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<DirectoryObject> ownedDevices
  {
    get => this._ownedDevices;
    set
    {
      if (value == null)
        return;
      this._ownedDevices = value;
    }
  }
}
