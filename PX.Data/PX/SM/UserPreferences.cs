// Decompiled with JetBrains decompiler
// Type: PX.SM.UserPreferences
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM.Email;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("User Preferences and Email Settings")]
public class UserPreferences : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _UserID;
  protected int? _DefBranchID;
  protected 
  #nullable disable
  byte[] _tstamp;
  protected string _PdfCertificateName;
  protected bool? _signatureToReplyAndForward;
  protected bool? _signatureToNewEmail;
  protected string _MailSignature;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected string _EntityDefaultSearch;
  protected string _TimeZone;
  protected Guid? _NoteID;
  protected bool? _DisableSuggest;
  protected bool? _EnableSmartSuggest;

  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  [PXUIField]
  public virtual Guid? UserID
  {
    get => this._UserID;
    set => this._UserID = value;
  }

  [EmailAccountRaw(EmailAccountsToShowOptions.MineAndSystem, null, DisplayName = "Default Email Account")]
  public virtual int? DefaultEMailAccountID { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Home Page")]
  public virtual Guid? HomePage { get; set; }

  [PXDBInt]
  [PXDimensionSelector("BRANCH", typeof (Search<Branch.branchID>), typeof (Branch.branchCD), ValidComboRequired = true)]
  [PXUIField(DisplayName = "Default Branch")]
  public virtual int? DefBranchID
  {
    get => this._DefBranchID;
    set => this._DefBranchID = value;
  }

  [PXPrinterSelector(DisplayName = "Default Printer")]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<UserPreferences.defaultPrinterID>.IsRelatedTo<SMPrinter.printerID>))]
  public virtual Guid? DefaultPrinterID { get; set; }

  [PXScaleSelector(DisplayName = "Default Scale")]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<UserPreferences.defaultScalesID>.IsRelatedTo<SMScale.scaleDeviceID>))]
  public virtual Guid? DefaultScalesID { get; set; }

  [PXScannerSelector(DisplayName = "Default Scanner")]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<UserPreferences.defaultScannerID>.IsRelatedTo<SMScanner.scannerID>))]
  public virtual Guid? DefaultScannerID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [Obsolete("This field is obsolete and will be removed in the future versions of Acumatica. It is ignored by the system. Please use PreferencesSecurity.PdfCertificateName")]
  [PXDBString(30)]
  [PXUIField(DisplayName = "PDF Signing Certificate", Visible = false)]
  [PXSelector(typeof (Certificate.name))]
  public virtual string PdfCertificateName
  {
    get => this._PdfCertificateName;
    set => this._PdfCertificateName = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include in Replies and Forwarded Emails")]
  public virtual bool? SignatureToReplyAndForward
  {
    get => this._signatureToReplyAndForward;
    set => this._signatureToReplyAndForward = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Include in New Emails")]
  public virtual bool? SignatureToNewEmail
  {
    get => this._signatureToNewEmail;
    set => this._signatureToNewEmail = value;
  }

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Signature")]
  public virtual string MailSignature
  {
    get => this._MailSignature;
    set => this._MailSignature = value;
  }

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

  [PXDBText(IsUnicode = true)]
  public virtual string EntityDefaultSearch
  {
    get => this._EntityDefaultSearch;
    set => this._EntityDefaultSearch = value;
  }

  [PXDBString(32 /*0x20*/)]
  [PXUIField(DisplayName = "Time Zone")]
  [PXTimeZone(true)]
  public virtual string TimeZone
  {
    get => this._TimeZone;
    set => this._TimeZone = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBBoolInvert]
  [PXUIField(DisplayName = "Lookup Box Suggestions")]
  public virtual bool? DisableSuggest
  {
    get => this._DisableSuggest;
    set => this._DisableSuggest = value;
  }

  [UserPreferences.enableSmartSuggest.PXDBBoolDefaultTrue]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Intelligent Text Completion")]
  public virtual bool? EnableSmartSuggest
  {
    get => this._EnableSmartSuggest;
    set => this._EnableSmartSuggest = value;
  }

  public class PK : PrimaryKeyOf<UserPreferences>.By<UserPreferences.userID>
  {
    public static UserPreferences Find(PXGraph graph, Guid? userID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<UserPreferences>.By<UserPreferences.userID>.FindBy(graph, (object) userID, options);
    }
  }

  public static class FK
  {
    public class DefaultBranch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<UserPreferences>.By<UserPreferences.defBranchID>
    {
    }

    public class DefaultPrinter : 
      PrimaryKeyOf<SMPrinter>.By<SMPrinter.printerID>.ForeignKeyOf<UserPreferences>.By<UserPreferences.defaultPrinterID>
    {
    }

    public class DefaultScale : 
      PrimaryKeyOf<SMScale>.By<SMScale.scaleDeviceID>.ForeignKeyOf<UserPreferences>.By<UserPreferences.defaultScalesID>
    {
    }

    public class DefaultScanner : 
      PrimaryKeyOf<SMScanner>.By<SMScanner.scannerID>.ForeignKeyOf<UserPreferences>.By<UserPreferences.defaultScannerID>
    {
    }

    public class Certificate : 
      PrimaryKeyOf<Certificate>.By<Certificate.name>.ForeignKeyOf<UserPreferences>.By<UserPreferences.pdfCertificateName>
    {
    }

    public class User : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<UserPreferences>.By<UserPreferences.userID>
    {
    }
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UserPreferences.userID>
  {
  }

  public abstract class defaultEMailAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    UserPreferences.defaultEMailAccountID>
  {
  }

  public abstract class homePage : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UserPreferences.homePage>
  {
  }

  public abstract class defBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UserPreferences.defBranchID>
  {
  }

  public abstract class defaultPrinterID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    UserPreferences.defaultPrinterID>
  {
  }

  public abstract class defaultScalesID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    UserPreferences.defaultScalesID>
  {
  }

  public abstract class defaultScannerID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    UserPreferences.defaultScannerID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  UserPreferences.Tstamp>
  {
  }

  public abstract class pdfCertificateName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UserPreferences.pdfCertificateName>
  {
  }

  public abstract class signatureToReplyAndForward : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    UserPreferences.signatureToReplyAndForward>
  {
  }

  public abstract class signatureToNewEmail : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    UserPreferences.signatureToNewEmail>
  {
  }

  public abstract class mailSignature : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UserPreferences.mailSignature>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UserPreferences.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UserPreferences.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    UserPreferences.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    UserPreferences.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UserPreferences.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    UserPreferences.lastModifiedDateTime>
  {
  }

  public abstract class entityDefaultSearch : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UserPreferences.entityDefaultSearch>
  {
  }

  public abstract class timeZone : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UserPreferences.timeZone>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UserPreferences.noteID>
  {
  }

  public abstract class disableSuggest : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    UserPreferences.disableSuggest>
  {
  }

  public abstract class enableSmartSuggest : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    UserPreferences.enableSmartSuggest>
  {
    internal class PXDBBoolDefaultTrueAttribute : PXDBBoolAttribute
    {
      public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
      {
        if (e.ReturnValue == null)
          e.ReturnValue = (object) true;
        base.FieldSelecting(sender, e);
      }
    }
  }
}
