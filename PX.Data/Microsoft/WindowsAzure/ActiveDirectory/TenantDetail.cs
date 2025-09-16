// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.ActiveDirectory.TenantDetail
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;

#nullable disable
namespace Microsoft.WindowsAzure.ActiveDirectory;

public class TenantDetail : DirectoryObject
{
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<AssignedPlan> _assignedPlans = new Collection<AssignedPlan>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _city;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private DateTime? _companyLastDirSyncTime;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _country;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _countryLetterCode;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private bool? _dirSyncEnabled;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _displayName;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<string> _marketingNotificationEmails = new Collection<string>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _postalCode;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _preferredLanguage;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<ProvisionedPlan> _provisionedPlans = new Collection<ProvisionedPlan>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<ProvisioningError> _provisioningErrors = new Collection<ProvisioningError>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _state;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _street;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<string> _technicalNotificationMails = new Collection<string>();
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _telephoneNumber;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private string _tenantType;
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  private Collection<VerifiedDomain> _verifiedDomains = new Collection<VerifiedDomain>();

  /// <summary>Create a new TenantDetail object.</summary>
  /// <param name="objectId">Initial value of objectId.</param>
  /// <param name="assignedPlans">Initial value of assignedPlans.</param>
  /// <param name="marketingNotificationEmails">Initial value of marketingNotificationEmails.</param>
  /// <param name="provisionedPlans">Initial value of provisionedPlans.</param>
  /// <param name="provisioningErrors">Initial value of provisioningErrors.</param>
  /// <param name="technicalNotificationMails">Initial value of technicalNotificationMails.</param>
  /// <param name="verifiedDomains">Initial value of verifiedDomains.</param>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public static TenantDetail CreateTenantDetail(
    string objectId,
    Collection<AssignedPlan> assignedPlans,
    Collection<string> marketingNotificationEmails,
    Collection<ProvisionedPlan> provisionedPlans,
    Collection<ProvisioningError> provisioningErrors,
    Collection<string> technicalNotificationMails,
    Collection<VerifiedDomain> verifiedDomains)
  {
    TenantDetail tenantDetail = new TenantDetail();
    tenantDetail.objectId = objectId;
    tenantDetail.assignedPlans = assignedPlans != null ? assignedPlans : throw new ArgumentNullException(nameof (assignedPlans));
    tenantDetail.marketingNotificationEmails = marketingNotificationEmails != null ? marketingNotificationEmails : throw new ArgumentNullException(nameof (marketingNotificationEmails));
    tenantDetail.provisionedPlans = provisionedPlans != null ? provisionedPlans : throw new ArgumentNullException(nameof (provisionedPlans));
    tenantDetail.provisioningErrors = provisioningErrors != null ? provisioningErrors : throw new ArgumentNullException(nameof (provisioningErrors));
    tenantDetail.technicalNotificationMails = technicalNotificationMails != null ? technicalNotificationMails : throw new ArgumentNullException(nameof (technicalNotificationMails));
    tenantDetail.verifiedDomains = verifiedDomains != null ? verifiedDomains : throw new ArgumentNullException(nameof (verifiedDomains));
    return tenantDetail;
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
  /// There are no comments for Property companyLastDirSyncTime in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public DateTime? companyLastDirSyncTime
  {
    get => this._companyLastDirSyncTime;
    set => this._companyLastDirSyncTime = value;
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
  /// There are no comments for Property countryLetterCode in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string countryLetterCode
  {
    get => this._countryLetterCode;
    set => this._countryLetterCode = value;
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
  /// There are no comments for Property marketingNotificationEmails in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<string> marketingNotificationEmails
  {
    get => this._marketingNotificationEmails;
    set => this._marketingNotificationEmails = value;
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
  /// There are no comments for Property state in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string state
  {
    get => this._state;
    set => this._state = value;
  }

  /// <summary>
  /// There are no comments for Property street in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string street
  {
    get => this._street;
    set => this._street = value;
  }

  /// <summary>
  /// There are no comments for Property technicalNotificationMails in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<string> technicalNotificationMails
  {
    get => this._technicalNotificationMails;
    set => this._technicalNotificationMails = value;
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
  /// There are no comments for Property tenantType in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public string tenantType
  {
    get => this._tenantType;
    set => this._tenantType = value;
  }

  /// <summary>
  /// There are no comments for Property verifiedDomains in the schema.
  /// </summary>
  [GeneratedCode("System.Data.Services.Design", "1.0.0")]
  public Collection<VerifiedDomain> verifiedDomains
  {
    get => this._verifiedDomains;
    set => this._verifiedDomains = value;
  }
}
