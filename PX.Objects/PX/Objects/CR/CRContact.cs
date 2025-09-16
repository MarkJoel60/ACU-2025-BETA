// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRContact
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GDPR;
using PX.SM;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.CR;

[PXCacheName("Opportunity Contact")]
[DebuggerDisplay("{GetType().Name,nq} ({IsDefaultContact}): ContactID = {ContactID,nq}, DisplayName = {DisplayName}")]
[Serializable]
public class CRContact : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IPersonalContact,
  IContact,
  INotable,
  IContactBase,
  IEmailMessageTarget,
  IConsentable
{
  protected int? _ContactID;
  protected int? _BAccountID;
  protected int? _BAccountContactID;
  protected int? _BAccountLocationID;
  protected bool? _IsDefaultContact;
  protected int? _RevisionID;
  protected 
  #nullable disable
  string _Title;
  protected string _Salutation;
  protected string _FullName;
  protected string _Email;
  protected string _Fax;
  protected string _FaxType;
  protected string _Phone1;
  protected string _Phone1Type;
  protected string _Phone2;
  protected string _Phone2Type;
  protected string _Phone3;
  protected string _Phone3Type;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Contact ID", Visible = false)]
  public virtual int? ContactID
  {
    get => this._ContactID;
    set => this._ContactID = value;
  }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "First Name")]
  [PXPersonalDataField]
  public virtual string FirstName { get; set; }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Last Name")]
  [PXPersonalDataField]
  public virtual string LastName { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Middle Name")]
  [PXPersonalDataField]
  public virtual string MidName { get; set; }

  [PXUIField]
  [PXDependsOnFields(new System.Type[] {typeof (CRContact.lastName), typeof (CRContact.firstName), typeof (CRContact.midName), typeof (CRContact.title)})]
  [PersonDisplayName(typeof (CRContact.lastName), typeof (CRContact.firstName), typeof (CRContact.midName), typeof (CRContact.title))]
  [PXFieldDescription]
  [PXPersonalDataField]
  public virtual string DisplayName { get; set; }

  [PXDBWeblink]
  [PXUIField(DisplayName = "Web")]
  [PXPersonalDataField]
  public virtual string WebSite { get; set; }

  [PXDBInt]
  [PXDBDefault(typeof (CROpportunity.bAccountID))]
  [PXSelector(typeof (BAccount.bAccountID), SubstituteKey = typeof (BAccount.acctCD), DescriptionField = typeof (BAccount.acctName), DirtyRead = true)]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  [PXDBInt]
  public virtual int? BAccountContactID
  {
    get => this._BAccountContactID;
    set => this._BAccountContactID = value;
  }

  [PXDBInt]
  public virtual int? BAccountLocationID
  {
    get => this._BAccountLocationID;
    set => this._BAccountLocationID = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? IsDefaultContact
  {
    get => this._IsDefaultContact;
    set => this._IsDefaultContact = value;
  }

  [PXBool]
  [PXUIField]
  public virtual bool? OverrideContact
  {
    [PXDependsOnFields(new System.Type[] {typeof (CRContact.isDefaultContact)})] get
    {
      if (!this._IsDefaultContact.HasValue)
        return this._IsDefaultContact;
      bool? isDefaultContact = this._IsDefaultContact;
      bool flag = false;
      return new bool?(isDefaultContact.GetValueOrDefault() == flag & isDefaultContact.HasValue);
    }
    set
    {
      bool? nullable1;
      if (value.HasValue)
      {
        bool? nullable2 = value;
        bool flag = false;
        nullable1 = new bool?(nullable2.GetValueOrDefault() == flag & nullable2.HasValue);
      }
      else
        nullable1 = value;
      this._IsDefaultContact = nullable1;
    }
  }

  [PXDBInt]
  public virtual int? RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  [PXDBString(50, IsUnicode = true)]
  [Titles]
  [PXUIField(DisplayName = "Title")]
  public virtual string Title
  {
    get => this._Title;
    set => this._Title = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string Salutation
  {
    get => this._Salutation;
    set => this._Salutation = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string Attention { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Account Name")]
  [PXPersonalDataField]
  public virtual string FullName
  {
    get => this._FullName;
    set => this._FullName = value;
  }

  [PXDBEmail]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string Email
  {
    get => this._Email;
    set => this._Email = value;
  }

  [PXDBString(50)]
  [PXUIField(DisplayName = "Fax")]
  [PhoneValidation]
  [PXPersonalDataField]
  public virtual string Fax
  {
    get => this._Fax;
    set => this._Fax = value;
  }

  [PXDBString(3)]
  [PXDefault("BF")]
  [PXUIField(DisplayName = "Fax")]
  [PhoneTypes]
  public virtual string FaxType
  {
    get => this._FaxType;
    set => this._FaxType = value;
  }

  [PXDBString(50)]
  [PXUIField]
  [PhoneValidation]
  [PXPersonalDataField]
  public virtual string Phone1
  {
    get => this._Phone1;
    set => this._Phone1 = value;
  }

  [PXDBString(3)]
  [PXDefault("B1")]
  [PXUIField(DisplayName = "Phone 1")]
  [PhoneTypes]
  public virtual string Phone1Type
  {
    get => this._Phone1Type;
    set => this._Phone1Type = value;
  }

  [PXDBString(50)]
  [PXUIField(DisplayName = "Phone 2")]
  [PhoneValidation]
  [PXPersonalDataField]
  public virtual string Phone2
  {
    get => this._Phone2;
    set => this._Phone2 = value;
  }

  [PXDBString(3)]
  [PXDefault("B2")]
  [PXUIField(DisplayName = "Phone 2")]
  [PhoneTypes]
  public virtual string Phone2Type
  {
    get => this._Phone2Type;
    set => this._Phone2Type = value;
  }

  [PXDBString(50)]
  [PXUIField(DisplayName = "Phone 3")]
  [PhoneValidation]
  [PXPersonalDataField]
  public virtual string Phone3
  {
    get => this._Phone3;
    set => this._Phone3 = value;
  }

  [PXDBString(3)]
  [PXDefault("H1")]
  [PXUIField(DisplayName = "Phone 3")]
  [PhoneTypes]
  public virtual string Phone3Type
  {
    get => this._Phone3Type;
    set => this._Phone3Type = value;
  }

  [PXConsentAgreementField]
  public virtual bool? ConsentAgreement { get; set; }

  [PXConsentDateField]
  public virtual DateTime? ConsentDate { get; set; }

  [PXConsentExpirationDateField]
  public virtual DateTime? ConsentExpirationDate { get; set; }

  [PXDBGuidNotNull]
  public virtual Guid? NoteID { get; set; }

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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  string IContactBase.EMail => this._Email;

  public string Address => this.Email;

  public class PK : PrimaryKeyOf<CRContact>.By<CRContact.contactID>
  {
    public static CRContact Find(PXGraph graph, int? contactID, PKFindOptions options = 0)
    {
      return (CRContact) PrimaryKeyOf<CRContact>.By<CRContact.contactID>.FindBy(graph, (object) contactID, options);
    }
  }

  public static class FK
  {
    public class BusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<CRContact>.By<CRContact.bAccountID>
    {
    }

    public class Contact : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CRContact>.By<CRContact.bAccountContactID>
    {
    }

    public class Location : 
      PrimaryKeyOf<Location>.By<Location.bAccountID, Location.locationID>.ForeignKeyOf<CRContact>.By<CRContact.bAccountID, CRContact.bAccountLocationID>
    {
    }
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRContact.contactID>
  {
  }

  public abstract class firstName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContact.firstName>
  {
  }

  public abstract class lastName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContact.lastName>
  {
  }

  public abstract class midName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContact.midName>
  {
  }

  public abstract class displayName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContact.displayName>
  {
  }

  public abstract class webSite : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContact.webSite>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRContact.bAccountID>
  {
  }

  public abstract class bAccountContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRContact.bAccountContactID>
  {
  }

  public abstract class bAccountLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRContact.bAccountLocationID>
  {
  }

  public abstract class isDefaultContact : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRContact.isDefaultContact>
  {
  }

  public abstract class overrideContact : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRContact.overrideContact>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRContact.revisionID>
  {
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContact.title>
  {
  }

  public abstract class salutation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContact.salutation>
  {
  }

  public abstract class attention : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContact.attention>
  {
  }

  public abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContact.fullName>
  {
  }

  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContact.email>
  {
  }

  public abstract class fax : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContact.fax>
  {
  }

  public abstract class faxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContact.faxType>
  {
  }

  public abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContact.phone1>
  {
  }

  public abstract class phone1Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContact.phone1Type>
  {
  }

  public abstract class phone2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContact.phone2>
  {
  }

  public abstract class phone2Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContact.phone2Type>
  {
  }

  public abstract class phone3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContact.phone3>
  {
  }

  public abstract class phone3Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRContact.phone3Type>
  {
  }

  public abstract class consentAgreement : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRContact.consentAgreement>
  {
  }

  public abstract class consentDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRContact.consentDate>
  {
  }

  public abstract class consentExpirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRContact.consentExpirationDate>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRContact.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRContact.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRContact.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRContact.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRContact.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRContact.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRContact.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRContact.Tstamp>
  {
  }
}
