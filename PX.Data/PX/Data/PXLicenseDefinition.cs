// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLicenseDefinition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public class PXLicenseDefinition
{
  public readonly bool Active;
  public readonly bool IsPortal;
  public readonly string InstallationID;
  public readonly string LicenseTypeCD;
  public readonly string SharedSecret;
  public readonly string InternalLink;
  public readonly string PartnerLink;
  public readonly System.DateTime ValidFrom;
  public readonly System.DateTime ValidTo;
  public readonly System.DateTime GrasePeriod;
  public readonly System.DateTime NotificationPeriod;
  public readonly System.DateTime NextRequest;
  public readonly System.DateTime IssuedDate;
  public readonly string IssuedBy;
  public readonly string DomainName;
  public readonly string CompanyName;
  public readonly string CustomerName;
  public readonly int Users;
  public readonly int Companies;
  public readonly int Processors;
  public readonly string Version;
  public readonly string Type;
  public readonly ReadOnlyCollection<string> Features;
  public ReadOnlyCollection<LicenseRestriction> Restrictions;
  public ReadOnlyCollection<LicenseRestriction> ResourceRestrictions;
  public ReadOnlyCollection<LicenseRestriction> FeatureRestrictions;
  public ReadOnlyCollection<LicenseRestriction> BusinessRestrictions;
  public readonly string ResourceLevel;
  public readonly int CommerceTransactionsPerMonth;
  public readonly int ErpTransactionsPerMonth;
  public readonly int CommerceTransactionsPerDay;
  public readonly int ErpTransactionsPerDay;
  public readonly int WebServiceRequestsPerHour;
  public readonly int WebServiceRequestsPerMinute;
  public readonly int WebServiceProcessingUnits;
  public readonly int MaxDataRowsReturnedByAPI;
  public readonly int DataIncludedGb;
  public readonly int LinesPerMasterRecord;
  public readonly int SerialsPerDocument;
  public readonly int NumberOfFixedAssets;
  public readonly int NumberOfItems;
  public readonly System.DateTime? ClearViolations;
  public readonly System.DateTime DataLoadModeFrom;
  public readonly System.DateTime DataLoadModeTo;
  public readonly int ViolationsInGrace;
  public readonly int OverageInGrace;
  public readonly int AllowedMonthlyViolations;
  public readonly int WebServiceApiUsers;
  public readonly int DaysQuiteFromGrace;
  public readonly int BAccount;

  public int PayrollEmployee
  {
    get
    {
      return LicenseRestriction.TryGetRestrictionValue<int>((IEnumerable<LicenseRestriction>) this.FeatureRestrictions, "payrollEmployee");
    }
  }

  public int MaximumNumberOfStaffMembersAndVehicles
  {
    get
    {
      return LicenseRestriction.TryGetRestrictionValue<int>((IEnumerable<LicenseRestriction>) this.FeatureRestrictions, "fsStaffVehicle");
    }
  }

  public int MaximumNumberOfAppointmentsPerMonth
  {
    get
    {
      return LicenseRestriction.TryGetRestrictionValue<int>((IEnumerable<LicenseRestriction>) this.FeatureRestrictions, "fsAppointments");
    }
  }

  public int MaximumNumberOfExpenseReceiptsRecognizedPerMonth
  {
    get
    {
      return LicenseRestriction.TryGetRestrictionValue<int>((IEnumerable<LicenseRestriction>) this.FeatureRestrictions, "expenseRcptPerMonth");
    }
  }

  public int MaximumNumberOfBusinessCardsRecognizedPerMonth
  {
    get
    {
      return LicenseRestriction.TryGetRestrictionValue<int>((IEnumerable<LicenseRestriction>) this.FeatureRestrictions, "businessCardsPerMonth");
    }
  }

  public int MaximumNumberOfDocumentsRecognizedPerMonth
  {
    get
    {
      return LicenseRestriction.TryGetRestrictionValue<int>((IEnumerable<LicenseRestriction>) this.FeatureRestrictions, "apDocPagesPerMonth");
    }
  }

  public int MaximumNumberOfBankFeedAccounts
  {
    get
    {
      return LicenseRestriction.TryGetRestrictionValue<int>((IEnumerable<LicenseRestriction>) this.FeatureRestrictions, "bankFeedAccounts");
    }
  }

  [PXInternalUseOnly]
  public IReadOnlyDictionary<string, string> Configuration { get; }

  protected PXLicenseDefinition()
  {
  }

  protected PXLicenseDefinition(PXLicenseDefinition definition)
  {
    if (definition == null)
    {
      GetLogger().Warning<string>("Skipping license definition initialization: {BlankLicenseReason}", "source is null");
    }
    else
    {
      foreach (System.Reflection.FieldInfo field in typeof (PXLicenseDefinition).GetFields())
      {
        object obj = field.GetValue((object) definition);
        if (obj != null)
        {
          try
          {
            field.SetValue((object) this, obj);
          }
          catch (Exception ex)
          {
            GetLogger().Error<string, object>(ex, "Error while setting license definition field {Field} to {Value}", field.Name, obj);
          }
        }
      }
      this.Configuration = definition.Configuration;
    }

    static ILogger GetLogger() => LicensingLog.ForClassContext(typeof (PXLicenseDefinition));
  }

  public PXLicenseDefinition(
    bool active,
    string installationID,
    System.DateTime validFrom,
    System.DateTime validTo,
    System.DateTime grasePeriod,
    System.DateTime notificationPeriod,
    System.DateTime nextRequest,
    System.DateTime issuedDate,
    string issuedBy,
    string domainName,
    string companyName,
    string customerName,
    int users,
    int companies,
    int processors,
    string version,
    string type,
    ReadOnlyCollection<string> features)
    : this(active, installationID, (string) null, (string) null, (string) null, validFrom, validTo, grasePeriod, notificationPeriod, nextRequest, issuedDate, issuedBy, domainName, companyName, customerName, users, companies, processors, version, type, features)
  {
  }

  public PXLicenseDefinition(
    bool active,
    string installationID,
    string sharedSecret,
    string internalLink,
    string partnerLink,
    System.DateTime validFrom,
    System.DateTime validTo,
    System.DateTime grasePeriod,
    System.DateTime notificationPeriod,
    System.DateTime nextRequest,
    System.DateTime issuedDate,
    string issuedBy,
    string domainName,
    string companyName,
    string customerName,
    int users,
    int companies,
    int processors,
    string version,
    string type,
    ReadOnlyCollection<string> features)
  {
    this.Active = active;
    this.InstallationID = installationID;
    this.SharedSecret = sharedSecret;
    this.InternalLink = internalLink;
    this.PartnerLink = partnerLink;
    this.ValidFrom = validFrom;
    this.ValidTo = validTo;
    this.GrasePeriod = grasePeriod;
    this.NotificationPeriod = notificationPeriod;
    this.NextRequest = nextRequest;
    this.IssuedDate = issuedDate;
    this.IssuedBy = issuedBy;
    this.Type = type;
    this.DomainName = domainName;
    this.CompanyName = companyName;
    this.CustomerName = customerName;
    this.Users = users;
    this.Companies = companies;
    this.Processors = processors;
    this.Version = version;
    this.Features = features;
  }

  public PXLicenseDefinition(
    bool active,
    string installationID,
    System.DateTime validFrom,
    System.DateTime validTo,
    System.DateTime grasePeriod,
    System.DateTime notificationPeriod,
    System.DateTime nextRequest,
    System.DateTime issuedDate,
    string issuedBy,
    string domainName,
    string companyName,
    string customerName,
    int users,
    int companies,
    int processors,
    string version,
    string type,
    ReadOnlyCollection<string> features,
    string ResourceLevel,
    int CommerceTransactionsPerMonth,
    int ErpTransactionsPerMonth,
    int CommerceTransactionsPerDay,
    int ErpTransactionsPerDay,
    int WebServiceRequestsPerHour,
    int WebServiceProcessingUnits,
    int MaxDataRowsReturnedByAPI,
    int DataIncludedGb,
    int LinesPerMasterRecord,
    int SerialsPerDocument,
    int NumberOfFixedAssets,
    int NumberOfItems,
    System.DateTime dataLoadModeFrom,
    System.DateTime dataLoadModeTo,
    int violationsInGrace,
    int overageInGrace,
    int allowedMonthlyViolations,
    int webServiceApiUsers,
    int daysQuiteFromGrace,
    System.DateTime clearViolationsDate,
    int bAccount,
    bool isPortal,
    string licenseTypeCD,
    List<LicenseRestriction> restrictions,
    List<LicenseRestriction> resourceRestrictions,
    List<LicenseRestriction> featureRestrictions,
    List<LicenseRestriction> businessRestrictions,
    IReadOnlyDictionary<string, string> configuration)
    : this(active, installationID, (string) null, (string) null, (string) null, validFrom, validTo, grasePeriod, notificationPeriod, nextRequest, issuedDate, issuedBy, domainName, companyName, customerName, users, companies, processors, version, type, features, ResourceLevel, CommerceTransactionsPerMonth, ErpTransactionsPerMonth, CommerceTransactionsPerDay, ErpTransactionsPerDay, WebServiceRequestsPerHour, WebServiceProcessingUnits, MaxDataRowsReturnedByAPI, DataIncludedGb, LinesPerMasterRecord, SerialsPerDocument, NumberOfFixedAssets, NumberOfItems, dataLoadModeFrom, dataLoadModeTo, violationsInGrace, overageInGrace, allowedMonthlyViolations, webServiceApiUsers, daysQuiteFromGrace, clearViolationsDate, bAccount, isPortal, licenseTypeCD, restrictions, resourceRestrictions, featureRestrictions, businessRestrictions, configuration)
  {
  }

  public PXLicenseDefinition(
    bool active,
    string installationID,
    string sharedSecret,
    string internalLink,
    string partnerLink,
    System.DateTime validFrom,
    System.DateTime validTo,
    System.DateTime grasePeriod,
    System.DateTime notificationPeriod,
    System.DateTime nextRequest,
    System.DateTime issuedDate,
    string issuedBy,
    string domainName,
    string companyName,
    string customerName,
    int users,
    int companies,
    int processors,
    string version,
    string type,
    ReadOnlyCollection<string> features,
    string ResourceLevel,
    int CommerceTransactionsPerMonth,
    int ErpTransactionsPerMonth,
    int CommerceTransactionsPerDay,
    int ErpTransactionsPerDay,
    int WebServiceRequestsPerHour,
    int WebServiceProcessingUnits,
    int MaxDataRowsReturnedByAPI,
    int DataIncludedGb,
    int LinesPerMasterRecord,
    int SerialsPerDocument,
    int NumberOfFixedAssets,
    int NumberOfItems,
    System.DateTime dataLoadModeFrom,
    System.DateTime dataLoadModeTo,
    int violationsInGrace,
    int overageInGrace,
    int allowedMonthlyViolations,
    int webServiceApiUsers,
    int daysQuiteFromGrace,
    System.DateTime clearViolationsDate,
    int bAccount,
    bool isPortal,
    string licenseTypeCD,
    List<LicenseRestriction> restrictions,
    List<LicenseRestriction> resourceRestrictions,
    List<LicenseRestriction> featureRestrictions,
    List<LicenseRestriction> businessRestrictions,
    IReadOnlyDictionary<string, string> configuration)
  {
    this.Active = active;
    this.InstallationID = installationID;
    this.SharedSecret = sharedSecret;
    this.InternalLink = internalLink;
    this.PartnerLink = partnerLink;
    this.ValidFrom = validFrom;
    this.ValidTo = validTo;
    this.GrasePeriod = grasePeriod;
    this.NotificationPeriod = notificationPeriod;
    this.NextRequest = nextRequest;
    this.IssuedDate = issuedDate;
    this.IssuedBy = issuedBy;
    this.Type = type;
    this.DomainName = domainName;
    this.CompanyName = companyName;
    this.CustomerName = customerName;
    this.Users = users;
    this.Companies = companies;
    this.Processors = processors;
    this.Version = version;
    this.Features = features;
    this.ResourceLevel = ResourceLevel;
    this.CommerceTransactionsPerMonth = CommerceTransactionsPerMonth;
    this.ErpTransactionsPerMonth = ErpTransactionsPerMonth;
    this.CommerceTransactionsPerDay = CommerceTransactionsPerDay;
    this.ErpTransactionsPerDay = ErpTransactionsPerDay;
    this.WebServiceRequestsPerHour = WebServiceRequestsPerHour;
    this.WebServiceRequestsPerMinute = WebServiceRequestsPerHour == 0 ? 0 : WebServiceRequestsPerHour / 60 + 1;
    this.WebServiceProcessingUnits = WebServiceProcessingUnits;
    this.MaxDataRowsReturnedByAPI = MaxDataRowsReturnedByAPI;
    this.DataIncludedGb = DataIncludedGb;
    this.LinesPerMasterRecord = LinesPerMasterRecord;
    this.SerialsPerDocument = SerialsPerDocument;
    this.NumberOfFixedAssets = NumberOfFixedAssets;
    this.NumberOfItems = NumberOfItems;
    this.DataLoadModeFrom = dataLoadModeFrom;
    this.DataLoadModeTo = dataLoadModeTo;
    this.ViolationsInGrace = violationsInGrace;
    this.OverageInGrace = overageInGrace;
    this.AllowedMonthlyViolations = allowedMonthlyViolations;
    this.WebServiceApiUsers = webServiceApiUsers;
    this.DaysQuiteFromGrace = daysQuiteFromGrace;
    this.ClearViolations = new System.DateTime?(clearViolationsDate);
    this.BAccount = bAccount;
    this.IsPortal = isPortal;
    this.LicenseTypeCD = licenseTypeCD;
    this.Restrictions = new ReadOnlyCollection<LicenseRestriction>((IList<LicenseRestriction>) restrictions);
    this.ResourceRestrictions = new ReadOnlyCollection<LicenseRestriction>((IList<LicenseRestriction>) resourceRestrictions);
    this.FeatureRestrictions = new ReadOnlyCollection<LicenseRestriction>((IList<LicenseRestriction>) featureRestrictions);
    this.BusinessRestrictions = new ReadOnlyCollection<LicenseRestriction>((IList<LicenseRestriction>) businessRestrictions);
    this.Configuration = configuration;
  }
}
