// Decompiled with JetBrains decompiler
// Type: PX.Data.SitePolicy
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using Microsoft.Extensions.Options;
using PX.Common;
using PX.Common.Cryptography;
using PX.Data.BQL;
using PX.DbServices.QueryObjectModel;
using PX.Hosting.MachineKey;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;

#nullable enable
namespace PX.Data;

public static class SitePolicy
{
  private static 
  #nullable disable
  MapRedirectorProvider mapProvider;
  internal const int DefaultGridFastFilterMaxLength = 100;
  private const string SlotName = "SitePolicy";
  private const string SlotNameCompanyLogo = "SitePolicy$CompanyLogo";
  private const string _defaultLang = "en-US";

  /// <summary>
  /// Determines the period of time (in days) that a password can be used before the system requires the user to change it
  /// </summary>
  public static int PasswordDayAge => SitePolicy.Definitions.passwordDayAge;

  /// <summary>
  /// Determines the least number of characters that a password for a user account may contain
  /// </summary>
  public static int PasswordMinLength => SitePolicy.Definitions.passwordMinLength;

  /// <summary>
  /// Determines whether passwords must meet complexity requirements
  /// </summary>
  /// <remarks>
  /// If this policy is enabled, passwords must meet the following minimum requirements:
  /// Contain characters from three of the following four categories:
  /// - uppercase characters (A through Z)
  /// - lowercase characters (a through z)
  /// - base 10 digits (0 through 9)
  /// - nonalphanumeric characters
  /// </remarks>
  public static bool PasswordComplexity => SitePolicy.Definitions.passwordComplexity;

  /// <summary>Additional password check</summary>
  public static string PasswordRegex => SitePolicy.Definitions.passwordRegex;

  /// <summary>Message for additional password check</summary>
  public static string PasswordRegexMessage => SitePolicy.Definitions.passwordRegexMsg;

  /// <summary>Password security type storage</summary>
  public static MembershipPasswordFormat PasswordSecurityType
  {
    get => SitePolicy.Definitions.passwordSecurityType;
  }

  public static int MultiFactorAuthLevel => SitePolicy.Definitions.multiFactorAuthLevel;

  /// <summary>
  /// Determines the number of failed logon attempts that causes a user account to be locked out
  /// </summary>
  public static int AccountLockoutThreshold => SitePolicy.Definitions.accountLockoutThreshold;

  /// <summary>
  /// Determines the number of minutes a locked-out account remains locked out before automatically becoming unlocked
  /// </summary>
  public static int AccountLockoutDuration => SitePolicy.Definitions.accountLockoutDuration;

  /// <summary>
  /// Determines the time a locked-out account remains locked out before automatically becoming unlocked
  /// </summary>
  public static System.DateTime AccountLockoutTime
  {
    get
    {
      System.DateTime dateTime = System.DateTime.Now;
      dateTime = dateTime.ToUniversalTime();
      return dateTime.AddMinutes((double) -SitePolicy.AccountLockoutDuration);
    }
  }

  /// <summary>
  /// Determines the number of minutes that must elapse after a failed logon attempt before the failed logon attempt counter is reset to 0 bad logon attempts
  /// </summary>
  public static int AccountLockoutReset => SitePolicy.Definitions.accountLockoutReset;

  /// <summary>Determines the operation mask to audit user activity</summary>
  public static int AuditOperationMask => SitePolicy.Definitions.auditOperationMask;

  /// <summary>
  /// Determines months amount to keep audit history when journal cleared
  /// </summary>
  public static int AuditMonthsKeep => SitePolicy.Definitions.auditMonthsKeep;

  /// <summary>
  /// Gets maximum allowed request size (return null if request size is not specified).
  /// </summary>
  public static int MaxRequestSize => SitePolicy.Definitions.maxRequestSize;

  public static int MaxContentLength => SitePolicy.Definitions.maxContentLength;

  public static UploadAllowedFileTypes[] AllowedFileTypes
  {
    get => SitePolicy.Definitions.allowedFileTypes;
  }

  public static UploadAllowedFileTypes[] AllowedImageTypes
  {
    get => SitePolicy.Definitions.allowedImageTypes;
  }

  public static string[] AllowedFileTypesExt => SitePolicy.Definitions.allowedFileTypesExt;

  public static string[] AllowedImageTypesExt => SitePolicy.Definitions.allowedImageTypesExt;

  public static string PdfCertificateName => SitePolicy.Definitions.pdfCertificateName;

  internal static bool ValidateCertificate(string name)
  {
    X509Certificate2 certificate = SitePolicy.CreateCertificate(name);
    if (certificate == null)
      return false;
    PX.Common.Cryptography.RSACryptoProvider rsaCryptoProvider = new PX.Common.Cryptography.RSACryptoProvider(certificate, Array.Empty<X509Certificate2>());
    try
    {
      return string.CompareOrdinal(Encoding.Unicode.GetString(((CryptoProvider) rsaCryptoProvider).Decrypt(((CryptoProvider) rsaCryptoProvider).Encrypt(Encoding.Unicode.GetBytes("a123w")))), "a123w") == 0;
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
      return false;
    }
  }

  internal static CryptoProvider RSACryptoProvider
  {
    get
    {
      CryptoProvider rsaCryptoProvider1 = SitePolicy.Definitions.RSACryptoProvider;
      if (rsaCryptoProvider1 != null)
        return rsaCryptoProvider1;
      SitePolicy.Definition definitions = SitePolicy.Definitions;
      X509Certificate2 certificate = SitePolicy.CreateCertificate(SitePolicy.Definitions.dbCertificateName);
      X509Certificate2[] x509Certificate2Array = new X509Certificate2[1]
      {
        SitePolicy.CreateCertificate(SitePolicy.Definitions.dbPrevCertificateName)
      };
      PX.Common.Cryptography.RSACryptoProvider rsaCryptoProvider2;
      CryptoProvider rsaCryptoProvider3 = (CryptoProvider) (rsaCryptoProvider2 = new PX.Common.Cryptography.RSACryptoProvider(certificate, x509Certificate2Array));
      definitions.RSACryptoProvider = (CryptoProvider) rsaCryptoProvider2;
      return rsaCryptoProvider3;
    }
  }

  internal static CryptoProvider TripleDESCryptoProvider
  {
    get => (CryptoProvider) SitePolicy.CreateSymetricProvider();
  }

  internal static bool IsCertificateUsage(string name)
  {
    return name == SitePolicy.Definitions.pdfCertificateName || name == SitePolicy.Definitions.dbCertificateName || name == SitePolicy.Definitions.dbPrevCertificateName;
  }

  private static X509Certificate2 CreateCertificate(string name)
  {
    if (string.IsNullOrEmpty(name))
      return (X509Certificate2) null;
    try
    {
      using (new PXConnectionScope())
      {
        X509Certificate2 certificate;
        try
        {
          certificate = CertificateRepository.GetCertificate(name);
        }
        catch (CryptographicException ex)
        {
          throw new PXException("The current certificate is not valid.");
        }
        if (certificate == null)
          throw new PXException("An encryption certificate cannot be created. Make sure that the certificate has been uploaded to the site.");
        return certificate.PrivateKey != null ? certificate : throw new PXException("An encryption certificate cannot be created. There is no private key.");
      }
    }
    catch (PXException ex)
    {
      PXTrace.WriteError((Exception) ex);
      return (X509Certificate2) null;
    }
  }

  private static SymmetricCryptoProvider CreateSymetricProvider()
  {
    MachineKeyOptions machineKeyOptions = ServiceLocator.Current.GetInstance<IOptions<MachineKeyOptions>>().Value;
    TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
    byte[] numArray1 = new byte[cryptoServiceProvider.IV.Length];
    Array.Copy((Array) CryptoProvider.HexStringToByteArray(machineKeyOptions.DecryptionKey), (Array) numArray1, numArray1.Length);
    byte[] numArray2 = new byte[cryptoServiceProvider.Key.Length];
    Array.Copy((Array) CryptoProvider.HexStringToByteArray(machineKeyOptions.ValidationKey), (Array) numArray2, numArray2.Length);
    return new SymmetricCryptoProvider(cryptoServiceProvider.CreateEncryptor(numArray2, numArray1), cryptoServiceProvider.CreateDecryptor(numArray2, numArray1));
  }

  public static Guid? GetLinkTemplate => SitePolicy.Definitions.getLinkTemplate;

  public static Guid? HomePage => SitePolicy.Definitions.homePage;

  public static Guid? PortalHomePage => SitePolicy.Definitions.portalHomePage;

  public static bool UseMLSearch => SitePolicy.Definitions.useMLSearch;

  public static bool UseSpellCheck => SitePolicy.Definitions.useSpellCheck;

  public static MapRedirectorProvider MapProvider
  {
    get
    {
      if (SitePolicy.mapProvider == null)
        SitePolicy.mapProvider = new MapRedirectorProvider();
      return SitePolicy.mapProvider;
    }
    set => SitePolicy.mapProvider = value;
  }

  public static MapRedirector CurrentMapRedirector
  {
    get
    {
      return !SitePolicy.Definitions.mapViewID.HasValue ? (MapRedirector) null : SitePolicy.MapProvider.MapRedirectors[SitePolicy.Definitions.mapViewID.Value];
    }
  }

  /// <summary>
  /// Gets a value indicating whether text whould be visible for action buttons in grids.
  /// </summary>
  public static bool GridActionsText => SitePolicy.Definitions.gridActionsText;

  public static PXCondition GridFastFilterCondition
  {
    get => SitePolicy.Definitions.gridFastFilterCondition;
  }

  public static int? GridFastFilterMaxLength => SitePolicy.Definitions.gridFastFilterMaxLength;

  public static Guid? GetCompanyLogoFileID(string branchCD)
  {
    branchCD = branchCD?.Trim();
    if (branchCD != null)
    {
      Guid? companyLogoFileId;
      if (PXDatabase.GetSlot<SitePolicy.CompanyLogoDefinition>("SitePolicy$CompanyLogo", typeof (PX.SM.Branch), typeof (UploadFile), typeof (SitePolicy.CompanyLogoDefinition.Organization)).FileIDs.TryGetValue(branchCD, out companyLogoFileId))
        return companyLogoFileId;
    }
    return new Guid?();
  }

  /// <summary>
  /// Gets a value indicating which UI is selected by default for forms.
  /// </summary>
  public static string DefaultUI => SitePolicy.Definitions.DefaultUI;

  public static Guid? GetOrganizationLogoFileID(int organizationId)
  {
    Guid? nullable;
    return PXDatabase.GetSlot<SitePolicy.CompanyLogoDefinition>("SitePolicy$CompanyLogo", typeof (PX.SM.Branch), typeof (UploadFile)).OrgFileIDs.TryGetValue(organizationId.ToString(), out nullable) ? nullable : new Guid?();
  }

  public static string MenuEditorRole => SitePolicy.Definitions.menuEditorRole;

  private static SitePolicy.Definition Definitions
  {
    get
    {
      return PXDatabase.GetSlot<SitePolicy.Definition>(nameof (SitePolicy), typeof (PreferencesGeneral), typeof (PreferencesEmail), typeof (PreferencesSecurity), typeof (UploadAllowedFileTypes)) ?? SitePolicy.Definition.Default;
    }
  }

  public static void Clear()
  {
    PXDatabase.ResetSlot<SitePolicy.Definition>(nameof (SitePolicy), typeof (PreferencesGeneral), typeof (PreferencesEmail), typeof (PreferencesSecurity), typeof (UploadAllowedFileTypes));
    PXDatabase.ResetSlot<SitePolicy.CompanyLogoDefinition>("SitePolicy$CompanyLogo", typeof (PX.SM.Branch), typeof (UploadFile));
  }

  internal static string GetHelpApiHref(bool checkSitePolicy)
  {
    if (checkSitePolicy && !SitePolicy.UseMLSearch)
      return string.Empty;
    string empty = WebConfig.GetString("help:search:service:baseUri", string.Empty);
    if (!string.IsNullOrEmpty(empty))
    {
      string name = CultureInfo.CurrentUICulture.Name;
      if (name != "en-US")
      {
        PXResultset<WikiRevision> pxResultset = PXSelectBase<WikiRevision, PXSelect<WikiRevision, Where<WikiRevision.language, Equal<Required<WikiRevision.language>>>>.Config>.SelectSingleBound(new PXGraph(), (object[]) null, (object) name);
        // ISSUE: explicit non-virtual call
        if ((pxResultset != null ? (__nonvirtual (pxResultset.Count) > 0 ? 1 : 0) : 0) != 0)
          empty = string.Empty;
      }
    }
    return empty;
  }

  internal static bool IsOnlineHelpOn()
  {
    return !string.IsNullOrWhiteSpace(SitePolicy.GetHelpApiHref(true));
  }

  internal static string GetHelpApiKey()
  {
    return WebConfig.GetString("help:search:service:x-api-key", string.Empty);
  }

  internal static bool IsHelpPortal()
  {
    return WebConfig.GetBool("HelpPortal", false) || WebConfig.GetString("DefaultRoute", string.Empty).ToLower() == "help";
  }

  private class Definition : IPrefetchable, IPXCompanyDependent
  {
    private const int MaxAccountLockoutDuration = 52560000;
    public int auditMonthsKeep;
    public int auditOperationMask;
    public int passwordDayAge;
    public int passwordMinLength;
    public bool passwordComplexity;
    public string passwordRegex;
    public string passwordRegexMsg;
    public int accountLockoutThreshold;
    public int accountLockoutDuration;
    public int accountLockoutReset;
    public MembershipPasswordFormat passwordSecurityType;
    public int maxRequestSize;
    public int maxContentLength;
    public string pdfCertificateName;
    public string menuEditorRole;
    public string dbCertificateName;
    public string dbPrevCertificateName;
    public int? mapViewID;
    public string[] allowedFileTypesExt;
    public string[] allowedImageTypesExt;
    public UploadAllowedFileTypes[] allowedFileTypes;
    public UploadAllowedFileTypes[] allowedImageTypes;
    public bool gridActionsText;
    public PXCondition gridFastFilterCondition;
    public int? gridFastFilterMaxLength;
    public Guid? getLinkTemplate;
    public CryptoProvider TripleDESCryptoProvider;
    public CryptoProvider RSACryptoProvider;
    public Guid? homePage;
    public Guid? portalHomePage;
    public bool useMLSearch;
    public int multiFactorAuthLevel;
    public bool useSpellCheck;
    public string DefaultUI;
    public static readonly SitePolicy.Definition Default = new SitePolicy.Definition();

    public Definition()
    {
      this.auditMonthsKeep = 0;
      this.auditOperationMask = 0;
      this.passwordDayAge = 0;
      this.passwordMinLength = 0;
      this.passwordComplexity = false;
      this.passwordSecurityType = MembershipPasswordFormat.Clear;
      this.accountLockoutThreshold = 3;
      this.accountLockoutDuration = 15;
      this.accountLockoutReset = 10;
      this.gridActionsText = false;
      this.gridFastFilterCondition = PXCondition.RLIKE;
      this.gridFastFilterMaxLength = new int?(100);
      this.TripleDESCryptoProvider = (CryptoProvider) null;
      this.RSACryptoProvider = (CryptoProvider) null;
    }

    public void Prefetch()
    {
      using (new PXConnectionScope())
      {
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PreferencesGeneral>(new PXDataField(typeof (PreferencesGeneral.maxUploadSize).Name), new PXDataField(typeof (PreferencesGeneral.mapViewer).Name), new PXDataField(typeof (PreferencesGeneral.gridActionsText).Name), (PXDataField) new PXDataField<PreferencesGeneral.gridFastFilterCondition>(), new PXDataField(typeof (PreferencesGeneral.getLinkTemplate).Name), (PXDataField) new PXDataField<PreferencesGeneral.homePage>(), (PXDataField) new PXDataField<PreferencesGeneral.portalHomePage>(), new PXDataField(typeof (PreferencesGeneral.useMLSearch).Name), (PXDataField) new PXDataField<PreferencesGeneral.gridFastFilterMaxLength>(), (PXDataField) new PXDataField<PreferencesGeneral.spellCheck>(), (PXDataField) new PXDataField<PreferencesGeneral.defaultUI>()))
        {
          if (pxDataRecord != null)
          {
            this.maxRequestSize = pxDataRecord.GetInt32(0).Value;
            this.mapViewID = pxDataRecord.GetInt32(1);
            this.gridActionsText = pxDataRecord.GetBoolean(2).GetValueOrDefault();
            this.gridFastFilterCondition = (PXCondition) pxDataRecord.GetInt32(3).Value;
            this.getLinkTemplate = pxDataRecord.GetGuid(4);
            this.homePage = pxDataRecord.GetGuid(5);
            this.portalHomePage = pxDataRecord.GetGuid(6);
            this.useMLSearch = pxDataRecord.GetBoolean(7).GetValueOrDefault();
            this.gridFastFilterMaxLength = pxDataRecord.GetInt32(8);
            this.useSpellCheck = pxDataRecord.GetBoolean(9).GetValueOrDefault();
            this.DefaultUI = pxDataRecord.GetString(10) ?? "E";
          }
        }
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PreferencesSecurity>(new PXDataField(typeof (PreferencesSecurity.traceMonthsKeep).Name), new PXDataField(typeof (PreferencesSecurity.traceOperationMask).Name), new PXDataField(typeof (PreferencesSecurity.passwordDayAge).Name), new PXDataField(typeof (PreferencesSecurity.passwordMinLength).Name), new PXDataField(typeof (PreferencesSecurity.passwordComplexity).Name), new PXDataField(typeof (PreferencesSecurity.passwordRegexCheck).Name), new PXDataField(typeof (PreferencesSecurity.passwordRegexCheckMessage).Name), new PXDataField(typeof (PreferencesSecurity.accountLockoutThreshold).Name), new PXDataField(typeof (PreferencesSecurity.accountLockoutDuration).Name), new PXDataField(typeof (PreferencesSecurity.accountLockoutReset).Name), new PXDataField(typeof (PreferencesSecurity.passwordSecurityType).Name), new PXDataField(typeof (PreferencesSecurity.dBCertificateName).Name), new PXDataField(typeof (PreferencesSecurity.dBPrevCertificateName).Name), new PXDataField(typeof (PreferencesSecurity.pdfCertificateName).Name), new PXDataField(typeof (PreferencesSecurity.defaultMenuEditorRole).Name), (PXDataField) new PXDataField<PreferencesSecurity.multiFactorAuthLevel>()))
        {
          if (pxDataRecord != null)
          {
            this.auditMonthsKeep = pxDataRecord.GetInt32(0).Value;
            this.auditOperationMask = pxDataRecord.GetInt32(1).Value;
            this.passwordDayAge = pxDataRecord.GetInt32(2).Value;
            this.passwordMinLength = pxDataRecord.GetInt32(3).Value;
            this.passwordComplexity = pxDataRecord.GetBoolean(4).Value;
            this.passwordRegex = pxDataRecord.GetString(5);
            this.passwordRegexMsg = pxDataRecord.GetString(6);
            this.accountLockoutThreshold = pxDataRecord.GetInt32(7).Value;
            this.accountLockoutDuration = System.Math.Min(pxDataRecord.GetInt32(8).Value, 52560000);
            this.accountLockoutReset = pxDataRecord.GetInt32(9).Value;
            this.passwordSecurityType = (MembershipPasswordFormat) pxDataRecord.GetInt16(10).Value;
            this.dbCertificateName = pxDataRecord.GetString(11);
            this.dbPrevCertificateName = pxDataRecord.GetString(12);
            this.pdfCertificateName = pxDataRecord.GetString(13);
            this.menuEditorRole = pxDataRecord.GetString(14);
            this.multiFactorAuthLevel = pxDataRecord.GetInt32(15).GetValueOrDefault();
          }
        }
        List<UploadAllowedFileTypes> source = new List<UploadAllowedFileTypes>();
        foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<UploadAllowedFileTypes>(new PXDataField("FileExt"), new PXDataField("IsImage"), new PXDataField("IconUrl"), (PXDataField) new PXDataFieldValue("Forbidden", PXDbType.Bit, (object) false)))
        {
          UploadAllowedFileTypes allowedFileTypes = new UploadAllowedFileTypes()
          {
            FileExt = pxDataRecord.GetString(0),
            IsImage = pxDataRecord.GetBoolean(1),
            IconUrl = pxDataRecord.GetString(2),
            Forbidden = new bool?(false)
          };
          source.Add(allowedFileTypes);
        }
        this.allowedFileTypes = source.ToArray();
        this.allowedImageTypes = source.Where<UploadAllowedFileTypes>((Func<UploadAllowedFileTypes, bool>) (f =>
        {
          bool? isImage = f.IsImage;
          bool flag = true;
          return isImage.GetValueOrDefault() == flag & isImage.HasValue;
        })).ToArray<UploadAllowedFileTypes>();
        this.allowedFileTypesExt = ((IEnumerable<UploadAllowedFileTypes>) this.allowedFileTypes).Select<UploadAllowedFileTypes, string>((Func<UploadAllowedFileTypes, string>) (f => f.FileExt)).ToArray<string>();
        this.allowedImageTypesExt = ((IEnumerable<UploadAllowedFileTypes>) this.allowedImageTypes).Select<UploadAllowedFileTypes, string>((Func<UploadAllowedFileTypes, string>) (f => f.FileExt)).ToArray<string>();
        if (!(WebConfigurationManager.GetSection("system.web/httpRuntime") is HttpRuntimeSection section))
          return;
        this.maxContentLength = section.MaxRequestLength;
        if (this.maxRequestSize <= this.maxContentLength)
          return;
        this.maxRequestSize = this.maxContentLength;
      }
    }
  }

  private class CompanyLogoDefinition : IPrefetchable, IPXCompanyDependent
  {
    private readonly Dictionary<string, Guid?> _fileIDs = new Dictionary<string, Guid?>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, Guid?> _orgFileIDs = new Dictionary<string, Guid?>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

    public IReadOnlyDictionary<string, Guid?> FileIDs
    {
      get => (IReadOnlyDictionary<string, Guid?>) this._fileIDs;
    }

    public IReadOnlyDictionary<string, Guid?> OrgFileIDs
    {
      get => (IReadOnlyDictionary<string, Guid?>) this._orgFileIDs;
    }

    public void Prefetch()
    {
      this._fileIDs.Clear();
      this._orgFileIDs.Clear();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<SitePolicy.CompanyLogoDefinition.Branch>((IEnumerable<YaqlJoin>) new YaqlJoin[3]
      {
        Yaql.join<UploadFile>("BranchLogo", Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<SitePolicy.CompanyLogoDefinition.Branch.logoName>((string) null), Yaql.column<UploadFile.name>("BranchLogo")), (YaqlJoinType) 1),
        Yaql.join<SitePolicy.CompanyLogoDefinition.Organization>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<SitePolicy.CompanyLogoDefinition.Branch.organizationID>((string) null), Yaql.column<SitePolicy.CompanyLogoDefinition.Organization.organizationID>((string) null)), (YaqlJoinType) 0),
        Yaql.join<UploadFile>("OrganizationLogo", Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<SitePolicy.CompanyLogoDefinition.Organization.logoName>((string) null), Yaql.column<UploadFile.name>("OrganizationLogo")), (YaqlJoinType) 1)
      }, (PXDataField) new PXDataField<SitePolicy.CompanyLogoDefinition.Branch.branchCD>(), (PXDataField) new PXDataField<UploadFile.fileID>("BranchLogo"), (PXDataField) new PXDataField<UploadFile.fileID>("OrganizationLogo"), (PXDataField) new PXDataField<SitePolicy.CompanyLogoDefinition.Organization.organizationID>("Organization")))
      {
        string key = pxDataRecord.GetString(0)?.Trim();
        if (key != null)
        {
          Guid? guid1 = pxDataRecord.GetGuid(1);
          if (guid1.HasValue)
          {
            this._fileIDs[key] = new Guid?(guid1.Value);
          }
          else
          {
            Guid? guid2 = pxDataRecord.GetGuid(2);
            if (guid2.HasValue)
              this._fileIDs[key] = new Guid?(guid2.Value);
          }
        }
        int? int32 = pxDataRecord.GetInt32(3);
        if (int32.HasValue)
        {
          Guid? guid = pxDataRecord.GetGuid(2);
          if (guid.HasValue)
            this._orgFileIDs[int32.Value.ToString()] = new Guid?(guid.Value);
        }
      }
    }

    public class Branch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
    {
      protected int? _BranchID;
      protected string _BranchCD;
      private string logoname1;

      /// <summary>
      /// Database identity.
      /// Unique identifier of the Branch.
      /// </summary>
      [PXDBIdentity]
      public virtual int? BranchID
      {
        get => this._BranchID;
        set => this._BranchID = value;
      }

      /// <summary>
      /// Key field.
      /// User-friendly unique identifier of the Branch.
      /// </summary>
      [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
      public virtual string BranchCD
      {
        get => this._BranchCD;
        set => this._BranchCD = value;
      }

      /// <summary>
      /// Reference to <see cref="T:PX.Data.SitePolicy.CompanyLogoDefinition.Organization" /> record to which the Branch belongs.
      /// </summary>
      public virtual int? OrganizationID { get; set; }

      /// <summary>The name of the logo image file.</summary>
      [PXDBString(IsUnicode = true, InputMask = "")]
      [PXUIField(DisplayName = "Logo File")]
      public string LogoName
      {
        get => this.logoname1;
        set => this.logoname1 = value;
      }

      /// <summary>The name of the report logo image file.</summary>
      [PXDBString(IsUnicode = true, InputMask = "")]
      [PXUIField(DisplayName = "Report Logo File")]
      public string LogoNameReport { get; set; }

      public abstract class branchID : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Field<
        #nullable disable
        SitePolicy.CompanyLogoDefinition.Branch.branchID>
      {
      }

      public abstract class branchCD : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        SitePolicy.CompanyLogoDefinition.Branch.branchCD>
      {
      }

      public abstract class organizationID : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Field<
        #nullable disable
        SitePolicy.CompanyLogoDefinition.Branch.organizationID>
      {
      }

      public abstract class logoName : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        SitePolicy.CompanyLogoDefinition.Branch.logoName>
      {
      }

      public abstract class logoNameReport : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        SitePolicy.CompanyLogoDefinition.Branch.logoNameReport>
      {
      }
    }

    public class Organization : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
    {
      [PXDBIdentity]
      public virtual int? OrganizationID { get; set; }

      /// <summary>The name of the logo image file.</summary>
      [PXDBString(IsUnicode = true, InputMask = "")]
      [PXUIField(DisplayName = "Logo File")]
      public string LogoName { get; set; }

      /// <summary>The name of the report logo image file.</summary>
      [PXDBString(IsUnicode = true, InputMask = "")]
      [PXUIField(DisplayName = "Report Logo File")]
      public string LogoNameReport { get; set; }

      public abstract class organizationID : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Field<
        #nullable disable
        SitePolicy.CompanyLogoDefinition.Organization.organizationID>
      {
      }

      public abstract class logoName : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        SitePolicy.CompanyLogoDefinition.Organization.logoName>
      {
      }

      public abstract class logoNameReport : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        SitePolicy.CompanyLogoDefinition.Organization.logoNameReport>
      {
      }
    }
  }
}
