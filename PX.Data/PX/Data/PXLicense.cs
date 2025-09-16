// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLicense
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Update;
using PX.Licensing;
using PX.SM;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public sealed class PXLicense : PXLicenseDefinition
{
  private const int USERS_BYPASS_NUMBER = 2147483647 /*0x7FFFFFFF*/;
  private const int COMPANIES_BYPASS_NUMBER = 2147483647 /*0x7FFFFFFF*/;
  private const int PROCESSORS_BYPASS_NUMBER = 2147483647 /*0x7FFFFFFF*/;
  private readonly bool _validated;
  private readonly bool _configured;
  public readonly string LicenseKey;
  public readonly string Restriction;
  public readonly string Signature;
  public readonly System.DateTime? Date;
  public readonly ReadOnlyCollection<string> Trials;
  private readonly bool IsVerified;

  public bool Valid
  {
    get
    {
      return this.State == PXLicenseState.Valid || this.State == PXLicenseState.GracePeriod || this.State == PXLicenseState.NotifyPeriod || this.State == PXLicenseState.Bypass;
    }
  }

  public bool Licensed => this.Validated && this.Valid;

  public bool Validated => PXAccess.BypassLicense || this._validated;

  public bool Configured => this._configured;

  public bool IsUnlimitedUser() => PXAccess.IsUnlimitedUser() && this.IsVerified;

  public PXLicenseState State
  {
    get
    {
      if ((!this.Configured || !this.Validated) && PXAccess.BypassLicense && this.IsVerified)
        return PXLicenseState.Bypass;
      if (!this.Configured)
        return PXLicenseState.Invalid;
      System.DateTime dateTime = System.DateTime.UtcNow;
      System.DateTime date1 = dateTime.Date;
      if (!this.Active)
        return PXLicenseState.Rejected;
      dateTime = this.ValidFrom;
      if (dateTime.Date > date1)
        return PXLicenseState.Invalid;
      dateTime = this.ValidTo;
      if (dateTime.Date < date1)
      {
        dateTime = this.GrasePeriod;
        if (dateTime.Date < date1)
          return PXLicenseState.Expired;
      }
      dateTime = this.ValidTo;
      if (dateTime.Date < date1)
      {
        dateTime = this.GrasePeriod;
        if (dateTime.Date > date1)
          return PXLicenseState.GracePeriod;
      }
      if (this.Date.HasValue)
      {
        System.DateTime? date2 = this.Date;
        dateTime = this.NextRequest;
        if ((date2.HasValue ? (date2.GetValueOrDefault() < dateTime ? 1 : 0) : 0) != 0)
        {
          dateTime = this.NotificationPeriod;
          if (dateTime.Date < date1)
          {
            dateTime = this.ValidTo;
            if (dateTime.Date > date1)
              return PXLicenseState.NotifyPeriod;
          }
        }
      }
      return PXLicenseState.Valid;
    }
  }

  public int UsersAllowed
  {
    get
    {
      if (this.State == PXLicenseState.Bypass)
        return int.MaxValue;
      return !this.Licensed ? 2 : this.Users;
    }
  }

  public int MaxApiUsersAllowed => !this.Licensed ? 2 : this.WebServiceApiUsers;

  public int CompaniesAllowed
  {
    get
    {
      if (this.State == PXLicenseState.Bypass)
        return int.MaxValue;
      return !this.Licensed ? 10 : this.Companies;
    }
  }

  public int ProcessorsAllowed
  {
    get
    {
      if (this.State == PXLicenseState.Bypass)
        return int.MaxValue;
      return !this.Licensed ? 4 : this.Processors;
    }
  }

  internal PXLicense(ICodeSigningManager codeSigningManager)
  {
    this._configured = false;
    if (codeSigningManager == null)
      return;
    this.IsVerified = codeSigningManager.VerifyAssemblyCodeSign(PXAccess.Provider.GetType().Assembly);
  }

  internal PXLicense(
    ILicensingManager licensingManager,
    ICodeSigningManager codeSigningManager,
    LicenseBucket bucket)
    : base(PXLicenseHelper.ParceLicense(bucket))
  {
    this.IsVerified = codeSigningManager.VerifyAssemblyCodeSign(PXAccess.Provider.GetType().Assembly);
    this.Date = bucket.Date;
    this._configured = true;
    this._validated = licensingManager.ValidateLicense(bucket);
    ILogger ilogger = LicensingLog.ForClassContext(typeof (PXLicense));
    ILogger logger = ilogger.IsEnabled((LogEventLevel) 0) ? ilogger : Logger.None;
    this.Trials = new ReadOnlyCollection<string>((IList<string>) PXCompanyHelper.SelectCompanies(PXCompanySelectOptions.Positive).ToList<UPCompany>().Where<UPCompany>((Func<UPCompany, bool>) (c =>
    {
      if (c.System.GetValueOrDefault())
      {
        logger.Write<string, string, string>((LogEventLevel) 0, "Company {CompanyName} set to {TrialStatus} because {TrialStatusReason}", c.LoginName, "non-trial", "is system");
        return false;
      }
      if (c.DataType == PXDataTypesHelper.DemoCompany.Name)
      {
        logger.ForContext("CompanyDataType", (object) c.DataType, false).Write<string, string, string>((LogEventLevel) 0, "Company {CompanyName} set to {TrialStatus} because {TrialStatusReason}", c.LoginName, "trial", "data type is demo");
        return true;
      }
      if (c.DataType == PXDataTypesHelper.TrialCompany.Name)
      {
        logger.ForContext("CompanyDataType", (object) c.DataType, false).Write<string, string, string>((LogEventLevel) 0, "Company {CompanyName} set to {TrialStatus} because {TrialStatusReason}", c.LoginName, "trial", "data type is trial");
        return true;
      }
      logger.ForContext("CompanyDataType", (object) c.DataType, false).Write<string, string, string>((LogEventLevel) 0, "Company {CompanyName} set to {TrialStatus} because {TrialStatusReason}", c.LoginName, "non-trial", "by default");
      return false;
    })).Select<UPCompany, string>((Func<UPCompany, string>) (c => c.LoginName)).ToList<string>());
    if (!this._validated)
      return;
    this.LicenseKey = bucket.LicenseKey;
    this.Restriction = bucket.Restriction;
    this.Signature = bucket.Signature;
  }

  public PXLicense Clone() => this.MemberwiseClone() as PXLicense;

  public string GetHash()
  {
    if (!this.Valid)
      return (string) null;
    return PXCriptoHelper.CalculateSHAString(this.ValidFrom.ToString((IFormatProvider) CultureInfo.InvariantCulture) + this.ValidTo.ToString((IFormatProvider) CultureInfo.InvariantCulture) + this.GrasePeriod.ToString((IFormatProvider) CultureInfo.InvariantCulture) + this.Users.ToString((IFormatProvider) CultureInfo.InvariantCulture) + this.Processors.ToString((IFormatProvider) CultureInfo.InvariantCulture) + string.Join(",", this.Features.ToArray<string>()));
  }

  public bool ValidFeature(string feature)
  {
    if (this.State == PXLicenseState.Bypass)
      return true;
    return this.Validated ? this.Features.Contains<string>(feature, (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase) : !this.Configured;
  }

  internal object GetData()
  {
    return (object) new
    {
      State = this.State,
      Companies = this.Companies,
      Configured = this.Configured,
      Date = this.Date,
      LicenseKey = this.LicenseKey,
      Processors = this.Processors,
      Trials = this.Trials,
      Users = this.Users,
      Validated = this.Validated,
      Active = this.Active,
      CompanyName = this.CompanyName,
      CustomerName = this.CustomerName,
      DomainName = this.DomainName,
      Features = this.Features,
      GrasePeriod = this.GrasePeriod,
      InstallationID = this.InstallationID,
      IssuedBy = this.IssuedBy,
      IssuedDate = this.IssuedDate,
      NextRequest = this.NextRequest,
      NotificationPeriod = this.NotificationPeriod,
      Type = this.Type,
      ValidFrom = this.ValidFrom,
      ValidTo = this.ValidTo,
      Version = this.Version,
      ResourceLevel = this.ResourceLevel,
      CommerceTransactionsPerMonth = this.CommerceTransactionsPerMonth,
      ErpTransactionsPerMonth = this.ErpTransactionsPerMonth,
      CommerceTransactionsPerDay = this.CommerceTransactionsPerDay,
      ErpTransactionsPerDay = this.ErpTransactionsPerDay,
      WebServiceRequestsPerHour = this.WebServiceRequestsPerHour,
      WebServiceProcessingUnits = this.WebServiceProcessingUnits,
      MaxDataRowsReturnedByAPI = this.MaxDataRowsReturnedByAPI,
      DataIncludedGb = this.DataIncludedGb,
      LinesPerMasterRecord = this.LinesPerMasterRecord,
      SerialsPerDocument = this.SerialsPerDocument,
      NumberOfFixedAssets = this.NumberOfFixedAssets,
      NumberOfItems = this.NumberOfItems,
      ClearViolations = this.ClearViolations,
      IsPortal = this.IsPortal
    };
  }

  internal bool HasChangedFrom(PXLicense old)
  {
    if (old == null || this.Companies != old.Companies || this.Configured != old.Configured)
      return true;
    System.DateTime? date = this.Date;
    System.DateTime? nullable = old.Date;
    if ((date.HasValue == nullable.HasValue ? (date.HasValue ? (date.GetValueOrDefault() != nullable.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0 && !(this.LicenseKey != old.LicenseKey) && this.Processors == old.Processors && this.Users == old.Users && this.Validated == old.Validated && this.Active == old.Active && !(this.CompanyName != old.CompanyName) && !(this.CustomerName != old.CustomerName) && !(this.DomainName != old.DomainName) && !(this.GrasePeriod != old.GrasePeriod) && !(this.InstallationID != old.InstallationID) && !(this.IssuedBy != old.IssuedBy) && !(this.IssuedDate != old.IssuedDate) && !(this.NextRequest != old.NextRequest) && !(this.NotificationPeriod != old.NotificationPeriod) && !(this.Type != old.Type) && !(this.ValidFrom != old.ValidFrom) && !(this.ValidTo != old.ValidTo) && !(this.Version != old.Version) && !(this.ResourceLevel != old.ResourceLevel) && this.CommerceTransactionsPerMonth == old.CommerceTransactionsPerMonth && this.ErpTransactionsPerMonth == old.ErpTransactionsPerMonth && this.CommerceTransactionsPerDay == old.CommerceTransactionsPerDay && this.ErpTransactionsPerDay == old.ErpTransactionsPerDay && this.WebServiceRequestsPerMinute == old.WebServiceRequestsPerMinute && this.WebServiceProcessingUnits == old.WebServiceProcessingUnits && this.MaxDataRowsReturnedByAPI == old.MaxDataRowsReturnedByAPI && this.DataIncludedGb == old.DataIncludedGb && this.LinesPerMasterRecord == old.LinesPerMasterRecord && this.SerialsPerDocument == old.SerialsPerDocument && this.NumberOfFixedAssets == old.NumberOfFixedAssets && this.NumberOfItems == old.NumberOfItems)
    {
      nullable = this.ClearViolations;
      System.DateTime? clearViolations = old.ClearViolations;
      if ((nullable.HasValue == clearViolations.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != clearViolations.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0 && this.IsPortal == old.IsPortal && this.State == old.State && new HashSet<string>((IEnumerable<string>) this.Trials).SetEquals((IEnumerable<string>) new HashSet<string>((IEnumerable<string>) old.Trials)))
        return !new HashSet<string>((IEnumerable<string>) this.Features).SetEquals((IEnumerable<string>) new HashSet<string>((IEnumerable<string>) old.Features));
    }
    return true;
  }
}
