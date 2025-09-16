// Decompiled with JetBrains decompiler
// Type: PX.SM.PreferencesSecurity
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.MultiFactorAuth;
using System;

#nullable enable
namespace PX.SM;

[PXPrimaryGraph(typeof (PreferencesSecurityMaint))]
[Serializable]
public class PreferencesSecurity : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const int DefaultMultifactorProviderTypes = 3;
  protected 
  #nullable disable
  string _DBCertificateName;
  protected string _DBPrevCertificateName;
  protected string _PdfCertificateName;
  protected int? _TraceMonthsKeep;
  protected int? _TraceOperationMask;
  protected bool? _TraceOperationLogin;
  protected bool? _TraceOperationLogout;
  protected bool? _TraceOperationSessionExpired;
  protected bool? _TraceOperationLoginFailed;
  protected bool? _TraceOperationAccessScreen;
  protected bool? _TraceOperationSendMail;
  protected bool? _TraceOperationSendMailFailed;
  protected bool? _TraceOperationLicenseExceeded;
  protected bool? _TraceOperationODataRefresh;
  protected bool? _TraceOperationCustomizationPublished;
  protected int? _PasswordDayAge;
  protected int? _PasswordMinLength;
  protected bool? _PasswordComplexity;
  protected string _PasswordRegexCheck;
  protected string _PasswordRegexCheckMessage;
  protected short? _PasswordSecurityType;
  protected int? _AccountLockoutThreshold;
  protected int? _AccountLockoutDuration;
  protected int? _AccountLockoutReset;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [PXDBString(30)]
  [PXUIField(DisplayName = "DB Encryption Certificate", Enabled = false)]
  [PXSelector(typeof (Certificate.name))]
  public virtual string DBCertificateName
  {
    get => this._DBCertificateName;
    set => this._DBCertificateName = value;
  }

  [PXDBString(30)]
  public virtual string DBPrevCertificateName
  {
    get => this._DBPrevCertificateName;
    set => this._DBPrevCertificateName = value;
  }

  [PXDBString(30)]
  [PXUIField(DisplayName = "PDF Signing Certificate")]
  [PXSelector(typeof (Certificate.name))]
  public virtual string PdfCertificateName
  {
    get => this._PdfCertificateName;
    set => this._PdfCertificateName = value;
  }

  [PXAdministratorRole]
  [PXUIField(DisplayName = "Menu Editor Role", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Roles.rolename))]
  public virtual string DefaultMenuEditorRole { get; set; }

  [PXDBInt]
  [PXDefault(3)]
  [PXUIField(DisplayName = "Audit History Retention Period (Months)")]
  public virtual int? TraceMonthsKeep
  {
    get => this._TraceMonthsKeep;
    set => this._TraceMonthsKeep = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? TraceOperationMask
  {
    get => this._TraceOperationMask;
    set => this._TraceOperationMask = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Login")]
  public virtual bool? TraceOperationLogin
  {
    get
    {
      int? traceOperationMask = this._TraceOperationMask;
      int? nullable = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() & 1) : new int?();
      int num = 0;
      return new bool?(nullable.GetValueOrDefault() > num & nullable.HasValue);
    }
    set
    {
      bool? nullable = value;
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() | 1) : new int?();
      }
      else
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() ^ 1) : new int?();
      }
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "Logout")]
  public virtual bool? TraceOperationLogout
  {
    get
    {
      int? traceOperationMask = this._TraceOperationMask;
      int? nullable = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() & 2) : new int?();
      int num = 0;
      return new bool?(nullable.GetValueOrDefault() > num & nullable.HasValue);
    }
    set
    {
      bool? nullable = value;
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() | 2) : new int?();
      }
      else
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() ^ 2) : new int?();
      }
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "Session Expired")]
  public virtual bool? TraceOperationSessionExpired
  {
    get
    {
      int? traceOperationMask = this._TraceOperationMask;
      int? nullable = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() & 4) : new int?();
      int num = 0;
      return new bool?(nullable.GetValueOrDefault() > num & nullable.HasValue);
    }
    set
    {
      bool? nullable = value;
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() | 4) : new int?();
      }
      else
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() ^ 4) : new int?();
      }
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "Login Failed")]
  public virtual bool? TraceOperationLoginFailed
  {
    get
    {
      int? traceOperationMask = this._TraceOperationMask;
      int? nullable = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() & 8) : new int?();
      int num = 0;
      return new bool?(nullable.GetValueOrDefault() > num & nullable.HasValue);
    }
    set
    {
      bool? nullable = value;
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() | 8) : new int?();
      }
      else
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() ^ 8) : new int?();
      }
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "Screen Accessed")]
  public virtual bool? TraceOperationAccessScreen
  {
    get
    {
      int? traceOperationMask = this._TraceOperationMask;
      int? nullable = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() & 16 /*0x10*/) : new int?();
      int num = 0;
      return new bool?(nullable.GetValueOrDefault() > num & nullable.HasValue);
    }
    set
    {
      bool? nullable = value;
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() | 16 /*0x10*/) : new int?();
      }
      else
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() ^ 16 /*0x10*/) : new int?();
      }
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "Send Email Success")]
  public virtual bool? TraceOperationSendMail
  {
    get
    {
      int? traceOperationMask = this._TraceOperationMask;
      int? nullable = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() & 32 /*0x20*/) : new int?();
      int num = 0;
      return new bool?(nullable.GetValueOrDefault() > num & nullable.HasValue);
    }
    set
    {
      bool? nullable = value;
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() | 32 /*0x20*/) : new int?();
      }
      else
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() ^ 32 /*0x20*/) : new int?();
      }
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "Send Email Error")]
  public virtual bool? TraceOperationSendMailFailed
  {
    get
    {
      int? traceOperationMask = this._TraceOperationMask;
      int? nullable = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() & 64 /*0x40*/) : new int?();
      int num = 0;
      return new bool?(nullable.GetValueOrDefault() > num & nullable.HasValue);
    }
    set
    {
      bool? nullable = value;
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() | 64 /*0x40*/) : new int?();
      }
      else
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() ^ 64 /*0x40*/) : new int?();
      }
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "License Exceeded")]
  public virtual bool? TraceOperationLicenseExceeded
  {
    get
    {
      int? traceOperationMask = this._TraceOperationMask;
      int? nullable = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() & 256 /*0x0100*/) : new int?();
      int num = 0;
      return new bool?(nullable.GetValueOrDefault() > num & nullable.HasValue);
    }
    set
    {
      bool? nullable = value;
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() | 256 /*0x0100*/) : new int?();
      }
      else
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() ^ 256 /*0x0100*/) : new int?();
      }
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "OData Refresh")]
  public virtual bool? TraceOperationODataRefresh
  {
    get
    {
      int? traceOperationMask = this._TraceOperationMask;
      int? nullable = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() & 1024 /*0x0400*/) : new int?();
      int num = 0;
      return new bool?(nullable.GetValueOrDefault() > num & nullable.HasValue);
    }
    set
    {
      bool? nullable = value;
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() | 1024 /*0x0400*/) : new int?();
      }
      else
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() ^ 1024 /*0x0400*/) : new int?();
      }
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "Customization Published")]
  public virtual bool? TraceOperationCustomizationPublished
  {
    get
    {
      int? traceOperationMask = this._TraceOperationMask;
      int? nullable = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() & 128 /*0x80*/) : new int?();
      int num = 0;
      return new bool?(nullable.GetValueOrDefault() > num & nullable.HasValue);
    }
    set
    {
      bool? nullable = value;
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() | 128 /*0x80*/) : new int?();
      }
      else
      {
        int? traceOperationMask = this._TraceOperationMask;
        this._TraceOperationMask = traceOperationMask.HasValue ? new int?(traceOperationMask.GetValueOrDefault() ^ 128 /*0x80*/) : new int?();
      }
    }
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Two-Factor Authentication")]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"None", "Required for Unknown Devices", "Required"})]
  public virtual int? MultiFactorAuthLevel { get; set; }

  [PXDBInt]
  [PXDefault(3)]
  public virtual int? MultiFactorAllowedTypes { get; set; }

  [PXBool]
  [PXDependsOnFields(new System.Type[] {typeof (PreferencesSecurity.multiFactorAllowedTypes)})]
  [PXUIField(DisplayName = "Allow Email")]
  public bool EmailEnabled
  {
    get
    {
      return ((TwoFactorSenderType) this.MultiFactorAllowedTypes.Value).HasFlag((Enum) TwoFactorSenderType.Email);
    }
    set
    {
      if (value)
        this.MultiFactorAllowedTypes = new int?(this.MultiFactorAllowedTypes.Value | 8);
      else
        this.MultiFactorAllowedTypes = new int?(this.MultiFactorAllowedTypes.Value & -9);
    }
  }

  [PXBool]
  [PXDependsOnFields(new System.Type[] {typeof (PreferencesSecurity.multiFactorAllowedTypes)})]
  [PXUIField(DisplayName = "Allow SMS")]
  public bool SmsEnabled
  {
    get
    {
      return ((TwoFactorSenderType) this.MultiFactorAllowedTypes.Value).HasFlag((Enum) TwoFactorSenderType.Sms);
    }
    set
    {
      if (value)
        this.MultiFactorAllowedTypes = new int?(this.MultiFactorAllowedTypes.Value | 4);
      else
        this.MultiFactorAllowedTypes = new int?(this.MultiFactorAllowedTypes.Value & -5);
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "Password Expiry Period in Days:")]
  public virtual bool? IsPasswordDayAge
  {
    get
    {
      int? passwordDayAge = this._PasswordDayAge;
      int num = 0;
      return new bool?(!(passwordDayAge.GetValueOrDefault() == num & passwordDayAge.HasValue));
    }
    set
    {
      bool? nullable1 = value;
      bool flag1 = true;
      if (!(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue))
      {
        int? passwordDayAge = this._PasswordDayAge;
        int num = 0;
        if (!(passwordDayAge.GetValueOrDefault() == num & passwordDayAge.HasValue))
          this._PasswordDayAge = new int?(0);
      }
      bool? nullable2 = value;
      bool flag2 = true;
      if (!(nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue))
        return;
      int? passwordDayAge1 = this._PasswordDayAge;
      int num1 = 0;
      if (!(passwordDayAge1.GetValueOrDefault() == num1 & passwordDayAge1.HasValue))
        return;
      this._PasswordDayAge = new int?(30);
    }
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Days")]
  public virtual int? PasswordDayAge
  {
    get => this._PasswordDayAge;
    set => this._PasswordDayAge = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Minimum Characters in Password:")]
  public virtual bool? IsPasswordMinLength
  {
    get
    {
      int? passwordMinLength = this._PasswordMinLength;
      int num = 0;
      return new bool?(!(passwordMinLength.GetValueOrDefault() == num & passwordMinLength.HasValue));
    }
    set
    {
      bool? nullable1 = value;
      bool flag1 = true;
      if (!(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue))
      {
        int? passwordMinLength = this._PasswordMinLength;
        int num = 0;
        if (!(passwordMinLength.GetValueOrDefault() == num & passwordMinLength.HasValue))
          this._PasswordMinLength = new int?(0);
      }
      bool? nullable2 = value;
      bool flag2 = true;
      if (!(nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue))
        return;
      int? passwordMinLength1 = this._PasswordMinLength;
      int num1 = 0;
      if (!(passwordMinLength1.GetValueOrDefault() == num1 & passwordMinLength1.HasValue))
        return;
      this._PasswordMinLength = new int?(3);
    }
  }

  [PXDBInt]
  [PXDefault(3)]
  [PXUIField(DisplayName = "Characters")]
  public virtual int? PasswordMinLength
  {
    get => this._PasswordMinLength;
    set => this._PasswordMinLength = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Password Must Meet Complexity Requirements")]
  public virtual bool? PasswordComplexity
  {
    get => this._PasswordComplexity;
    set => this._PasswordComplexity = value;
  }

  [PXDBString]
  [PXUIField(DisplayName = "Additional Password Validation Mask")]
  public virtual string PasswordRegexCheck
  {
    get => this._PasswordRegexCheck;
    set => this._PasswordRegexCheck = value;
  }

  [PXDBString]
  [PXUIField(DisplayName = "Incorrect Password Alert")]
  public virtual string PasswordRegexCheckMessage
  {
    get => this._PasswordRegexCheckMessage;
    set => this._PasswordRegexCheckMessage = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "Password Security Type")]
  [PXDefault(1)]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"Clear", "Hash", "Encrypted"})]
  public virtual short? PasswordSecurityType
  {
    get => this._PasswordSecurityType;
    set => this._PasswordSecurityType = value;
  }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Failed Sign-In Attempts Before Account Lockout")]
  public virtual int? AccountLockoutThreshold
  {
    get => this._AccountLockoutThreshold;
    set => this._AccountLockoutThreshold = value;
  }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Account Lockout Duration (Minutes)")]
  public virtual int? AccountLockoutDuration
  {
    get => this._AccountLockoutDuration;
    set => this._AccountLockoutDuration = value;
  }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Reset Interval for Failed Sign-In Attempts (Minutes)")]
  public virtual int? AccountLockoutReset
  {
    get => this._AccountLockoutReset;
    set => this._AccountLockoutReset = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Support to Sign In as Any User")]
  public virtual bool? AllowSupportToLoginAsAnyUser { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public abstract class dBCertificateName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesSecurity.dBCertificateName>
  {
  }

  public abstract class dBPrevCertificateName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesSecurity.dBPrevCertificateName>
  {
  }

  public abstract class pdfCertificateName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesSecurity.pdfCertificateName>
  {
  }

  public abstract class defaultMenuEditorRole : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesSecurity.defaultMenuEditorRole>
  {
  }

  public abstract class traceMonthsKeep : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesSecurity.traceMonthsKeep>
  {
  }

  public abstract class traceOperationMask : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesSecurity.traceOperationMask>
  {
  }

  public abstract class traceOperationLogin : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesSecurity.traceOperationLogin>
  {
  }

  public abstract class traceOperationLogout : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesSecurity.traceOperationLogout>
  {
  }

  public abstract class traceOperationSessionExpired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesSecurity.traceOperationSessionExpired>
  {
  }

  public abstract class traceOperationLoginFailed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesSecurity.traceOperationLoginFailed>
  {
  }

  public abstract class traceOperationAccessScreen : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesSecurity.traceOperationAccessScreen>
  {
  }

  public abstract class traceOperationSendMail : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesSecurity.traceOperationSendMail>
  {
  }

  public abstract class traceOperationSendMailFailed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesSecurity.traceOperationSendMailFailed>
  {
  }

  public abstract class traceOperationLicenseExceeded : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesSecurity.traceOperationLicenseExceeded>
  {
  }

  public abstract class traceOperationODataRefresh : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesSecurity.traceOperationODataRefresh>
  {
  }

  public abstract class traceOperationCustomizationPublished : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesSecurity.traceOperationCustomizationPublished>
  {
  }

  public abstract class multiFactorAuthLevel : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesSecurity.multiFactorAuthLevel>
  {
  }

  public abstract class multiFactorAllowedTypes : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesSecurity.multiFactorAllowedTypes>
  {
  }

  public abstract class emailEnabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesSecurity.emailEnabled>
  {
  }

  public abstract class smsEnabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PreferencesSecurity.smsEnabled>
  {
  }

  public abstract class isPasswordDayAge : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesSecurity.isPasswordDayAge>
  {
  }

  public abstract class passwordDayAge : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesSecurity.passwordDayAge>
  {
  }

  public abstract class isPasswordMinLength : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesSecurity.isPasswordMinLength>
  {
  }

  public abstract class passwordMinLength : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesSecurity.passwordMinLength>
  {
  }

  public abstract class passwordComplexity : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesSecurity.passwordComplexity>
  {
  }

  public abstract class passwordRegexCheck : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesSecurity.passwordRegexCheck>
  {
  }

  public abstract class passwordRegexCheckMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesSecurity.passwordRegexCheckMessage>
  {
  }

  public abstract class passwordSecurityType : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    PreferencesSecurity.passwordSecurityType>
  {
  }

  public abstract class accountLockoutThreshold : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesSecurity.accountLockoutThreshold>
  {
  }

  public abstract class accountLockoutDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesSecurity.accountLockoutDuration>
  {
  }

  public abstract class accountLockoutReset : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesSecurity.accountLockoutReset>
  {
  }

  public abstract class allowSupportToLoginAsAnyUser : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesSecurity.allowSupportToLoginAsAnyUser>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PreferencesSecurity.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesSecurity.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    PreferencesSecurity.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PreferencesSecurity.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesSecurity.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    PreferencesSecurity.lastModifiedDateTime>
  {
  }
}
