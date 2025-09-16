// Decompiled with JetBrains decompiler
// Type: PX.SM.PreferencesEmail
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM.Email;
using Serilog.Events;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
[PXPrimaryGraph(typeof (PreferencesEmailMaint))]
[Serializable]
public class PreferencesEmail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _SupportEMailAccount;
  protected string _EmailTagPrefix;
  protected string _EmailTagSuffix;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [EmailAccountRaw(EmailAccountsToShowOptions.OnlySystem, null, DisplayName = "Default Email Account")]
  public virtual int? DefaultEMailAccountID { get; set; }

  [PXDBString(100)]
  [PXUIField(DisplayName = "Support Email Account")]
  public virtual string SupportEMailAccount
  {
    get => this._SupportEMailAccount;
    set => this._SupportEMailAccount = value;
  }

  [PXDBString(30)]
  [PXDefault("[")]
  [PXUIField(DisplayName = "Email Tag Prefix")]
  public virtual string EmailTagPrefix
  {
    get => this._EmailTagPrefix;
    set => this._EmailTagPrefix = value;
  }

  [PXDBString(30)]
  [PXDefault("]")]
  [PXUIField(DisplayName = "Email Tag Suffix")]
  public virtual string EmailTagSuffix
  {
    get => this._EmailTagSuffix;
    set => this._EmailTagSuffix = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Archive Emails")]
  [PXIntList(new int[] {0, 3, 6, 9, 12}, new string[] {"None", "Older than 3 Months", "Older than 6 Months", "Older than 9 Months", "Older than 12 Months"})]
  public virtual int? ArchiveEmailsOlderThan { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 10)]
  [PXUIField(DisplayName = "Automatic Resend Attempts")]
  [PXDefault(3)]
  public virtual int? RepeatOnErrorSending { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Suspend Email Processing")]
  public virtual bool? SuspendEmailProcessing { get; set; }

  /// <summary>
  /// If True: The system not only move email to Pending processing status, but also automatically apply Process action to move email activity into Processing status.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Send User Emails Immediately")]
  [PXDefault(false)]
  [PXFormula(typeof (Switch<Case<Where<PreferencesEmail.suspendEmailProcessing, Equal<False>>, False>, PreferencesEmail.sendUserEmailsImmediately>))]
  public virtual bool? SendUserEmailsImmediately { get; set; }

  /// <summary>The email processing logging configuration.</summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Email Processing Logging")]
  [PXDefault(1)]
  [PreferencesEmail.emailProcessingLogging.List]
  public virtual int? EmailProcessingLogging { get; set; }

  /// <summary>The retention period for email logs.</summary>
  /// <remarks>
  /// All email logs older than the specified period will be deleted on each email receive.
  /// </remarks>
  [PXDBString(2)]
  [PXUIField(DisplayName = "Keep Email Logs For")]
  [PXDefault("1W")]
  [PreferencesEmail.emailProcessingLoggingRetentionPeriod.List]
  public virtual string EmailProcessingLoggingRetentionPeriod { get; set; }

  [EmailNotification(DisplayName = "New User Welcome Email Template")]
  public virtual int? UserWelcomeNotificationId { get; set; }

  [EmailNotification(DisplayName = "Welcome Email Template (New Portal User)")]
  public virtual int? PortalUserWelcomeNotificationId { get; set; }

  [EmailNotification(DisplayName = "Password Changed Email Template")]
  public virtual int? PasswordChangedNotificationId { get; set; }

  [EmailNotification(DisplayName = "Login Recovery Email Template")]
  public virtual int? LoginRecoveryNotificationId { get; set; }

  [EmailNotification(DisplayName = "Password Recovery Email Template")]
  public virtual int? PasswordRecoveryNotificationId { get; set; }

  [EmailNotification(DisplayName = "Password Recovery Email Template (Portal)")]
  public virtual int? PortalPasswordRecoveryNotificationId { get; set; }

  [EmailNotification(DisplayName = "New Device Code Email Template")]
  public virtual int? TwoFactorNewDeviceNotificationId { get; set; }

  [EmailNotification(DisplayName = "Two-Factor Access Code Email Template")]
  public virtual int? TwoFactorCodeByNotificationId { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "URL to be used in Notifications")]
  public virtual string NotificationSiteUrl { get; set; }

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

  public static class FK
  {
    public class DefaultEmailAccount : 
      PrimaryKeyOf<EMailAccount>.By<EMailAccount.emailAccountID>.ForeignKeyOf<PreferencesEmail>.By<PreferencesEmail.defaultEMailAccountID>
    {
    }
  }

  /// <exclude />
  public abstract class defaultEMailAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesEmail.defaultEMailAccountID>
  {
    public class EmailAccountRule : 
      EMailAccount.userID.PreventMakingPersonalIfUsedAsSystem<Select<PreferencesEmail, Where<KeysRelation<PX.Data.ReferentialIntegrity.Attributes.Field<PreferencesEmail.defaultEMailAccountID>.IsRelatedTo<EMailAccount.emailAccountID>.AsSimpleKey.WithTablesOf<EMailAccount, PreferencesEmail>, EMailAccount, PreferencesEmail>.SameAsCurrent>>>
    {
    }
  }

  public abstract class supportEMailAccount : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesEmail.supportEMailAccount>
  {
  }

  /// <exclude />
  public abstract class emailTagPrefix : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesEmail.emailTagPrefix>
  {
  }

  /// <exclude />
  public abstract class emailTagSuffix : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesEmail.emailTagSuffix>
  {
  }

  /// <exclude />
  public abstract class archiveEmailsOlderThan : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesEmail.archiveEmailsOlderThan>
  {
  }

  public abstract class repeatOnErrorSending : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesEmail.repeatOnErrorSending>
  {
  }

  public abstract class suspendEmailProcessing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesEmail.suspendEmailProcessing>
  {
  }

  public abstract class sendUserEmailsImmediately : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesEmail.sendUserEmailsImmediately>
  {
  }

  /// <inheritdoc cref="P:PX.SM.PreferencesEmail.EmailProcessingLogging" />
  public abstract class emailProcessingLogging : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesEmail.emailProcessingLogging>
  {
    public const int Disabled = 0;
    public const int EnabledForFailedEmails = 1;
    public const int EnabledForAllEmails = 2;

    public static LogEventLevel ToEventLevel(int? emailProcessingLogging)
    {
      LogEventLevel eventLevel;
      if (emailProcessingLogging.HasValue)
      {
        switch (emailProcessingLogging.GetValueOrDefault())
        {
          case 1:
            eventLevel = (LogEventLevel) 3;
            goto label_5;
          case 2:
            eventLevel = (LogEventLevel) 2;
            goto label_5;
        }
      }
      eventLevel = (LogEventLevel) 5;
label_5:
      return eventLevel;
    }

    public class disabled : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      PreferencesEmail.emailProcessingLogging.disabled>
    {
      public disabled()
        : base(0)
      {
      }
    }

    public class enabledForFailedEmails : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      PreferencesEmail.emailProcessingLogging.enabledForFailedEmails>
    {
      public enabledForFailedEmails()
        : base(1)
      {
      }
    }

    public class enabledForAllEmails : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      PreferencesEmail.emailProcessingLogging.enabledForAllEmails>
    {
      public enabledForAllEmails()
        : base(2)
      {
      }
    }

    public class ListAttribute : PXIntListAttribute
    {
      public ListAttribute()
        : base((0, "Disabled"), (1, "Enabled for Failed Emails"), (2, "Enabled for All Emails"))
      {
      }
    }
  }

  /// <inheritdoc cref="P:PX.SM.PreferencesEmail.EmailProcessingLoggingRetentionPeriod" />
  public abstract class emailProcessingLoggingRetentionPeriod : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesEmail.emailProcessingLoggingRetentionPeriod>
  {
    public const string Day1 = "1D";
    public const string Week1 = "1W";
    public const string Month1 = "1M";
    public const string Month3 = "3M";

    public static TimeSpan ToTimeSpan(string value)
    {
      TimeSpan timeSpan;
      switch (value)
      {
        case "1D":
          timeSpan = TimeSpan.FromDays(1.0);
          break;
        case "1W":
          timeSpan = TimeSpan.FromDays(7.0);
          break;
        case "1M":
          timeSpan = TimeSpan.FromDays(30.0);
          break;
        case "3M":
          timeSpan = TimeSpan.FromDays(90.0);
          break;
        default:
          timeSpan = TimeSpan.Zero;
          break;
      }
      return timeSpan;
    }

    public class day1 : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      PreferencesEmail.emailProcessingLoggingRetentionPeriod.day1>
    {
      public day1()
        : base("1D")
      {
      }
    }

    public class week1 : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      PreferencesEmail.emailProcessingLoggingRetentionPeriod.week1>
    {
      public week1()
        : base("1W")
      {
      }
    }

    public class month1 : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      PreferencesEmail.emailProcessingLoggingRetentionPeriod.month1>
    {
      public month1()
        : base("1M")
      {
      }
    }

    public class month3 : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      PreferencesEmail.emailProcessingLoggingRetentionPeriod.month3>
    {
      public month3()
        : base("3M")
      {
      }
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(("1D", "1 Day"), ("1W", "1 Week"), ("1M", "1 Month"), ("3M", "3 Months"))
      {
      }
    }
  }

  public abstract class userWelcomeNotificationId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesEmail.userWelcomeNotificationId>
  {
  }

  public abstract class portalUserWelcomeNotificationId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesEmail.portalUserWelcomeNotificationId>
  {
  }

  public abstract class passwordChangedNotificationId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesEmail.passwordChangedNotificationId>
  {
  }

  public abstract class loginRecoveryNotificationId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesEmail.loginRecoveryNotificationId>
  {
  }

  public abstract class passwordRecoveryNotificationId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesEmail.passwordRecoveryNotificationId>
  {
  }

  public abstract class portalPasswordRecoveryNotificationId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesEmail.portalPasswordRecoveryNotificationId>
  {
  }

  public abstract class twoFactorNewDeviceNotificationId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesEmail.twoFactorNewDeviceNotificationId>
  {
  }

  public abstract class twoFactorCodeByNotificationId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesEmail.twoFactorCodeByNotificationId>
  {
  }

  public abstract class notificationSiteUrl : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesEmail.notificationSiteUrl>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PreferencesEmail.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesEmail.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    PreferencesEmail.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PreferencesEmail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesEmail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    PreferencesEmail.lastModifiedDateTime>
  {
  }

  public class MonthPeriod
  {
    public const int Never = 0;
    public const int Three = 3;
    public const int Six = 6;
    public const int Nine = 9;
    public const int Twelve = 12;

    public class UI
    {
      public const string Never = "None";
      public const string Three = "Older than 3 Months";
      public const string Six = "Older than 6 Months";
      public const string Nine = "Older than 9 Months";
      public const string Twelve = "Older than 12 Months";
    }
  }
}
