// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ContactExtAddress
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXProjection(typeof (Select2<Contact, LeftJoin<Address, On<Contact.bAccountID, Equal<Address.bAccountID>, And<Contact.defAddressID, Equal<Address.addressID>>>>>), Persistent = true)]
[PXCacheName("Contact with Address")]
[Serializable]
public class ContactExtAddress : Address, IDefAddressAccessor
{
  protected int? _ContactBAccountID;
  protected int? _ContactID;
  protected int? _DefAddressID;
  protected 
  #nullable disable
  string _Title;
  protected string _Salutation;
  protected string _FirstName;
  protected string _MidName;
  protected string _LastName;
  protected string _FullName;
  protected string _EMail;
  protected string _WebSite;
  protected string _Fax;
  protected string _FaxType;
  protected string _Phone1;
  protected string _Phone1Type;
  protected string _Phone2;
  protected string _Phone2Type;
  protected string _Phone3;
  protected string _Phone3Type;
  protected DateTime? _DateOfBirth;
  protected string _ContactType;
  protected bool? _IsActive;
  protected Guid? _ContactNoteID;
  protected Guid? _ContactCreatedByID;
  protected string _ContactCreatedByScreenID;
  protected DateTime? _ContactCreatedDateTime;
  protected Guid? _ContactLastModifiedByID;
  protected string _ContactLastModifiedByScreenID;
  protected DateTime? _ContactLastModifiedDateTime;
  protected bool? _IsDefault;
  protected bool? _IsAddressSameAsMain;
  protected string _ContactDisplayName;

  [PXDBInt(IsKey = true, BqlField = typeof (Contact.bAccountID))]
  [PXDBDefault(typeof (BAccount.bAccountID))]
  [PXUIField]
  [PXParent(typeof (Select<BAccount, Where<BAccount.bAccountID, Equal<Current<ContactExtAddress.bAccountID>>, And<BAccount.type, NotEqual<BAccountType.combinedType>>>>), LeaveChildren = true)]
  public virtual int? ContactBAccountID
  {
    get => this._ContactBAccountID;
    set => this._ContactBAccountID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (Contact.contactID))]
  [PXDBDefault(typeof (Contact.contactID))]
  [PXUIField]
  public virtual int? ContactID
  {
    get => this._ContactID;
    set => this._ContactID = value;
  }

  [PXDBInt(BqlField = typeof (Contact.defAddressID))]
  [PXDBDefault(typeof (Address.addressID))]
  [PXUIField]
  public virtual int? DefAddressID
  {
    get => this._DefAddressID;
    set => this._DefAddressID = value;
  }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (Contact.title))]
  [Titles]
  [PXUIField]
  public virtual string Title
  {
    get => this._Title;
    set => this._Title = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (Contact.salutation))]
  [PXDefault]
  [PXUIField(DisplayName = "Job Title")]
  public virtual string Salutation
  {
    get => this._Salutation;
    set => this._Salutation = value;
  }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (Contact.firstName))]
  [PXUIField]
  public virtual string FirstName
  {
    get => this._FirstName;
    set => this._FirstName = value;
  }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (Contact.midName))]
  [PXUIField(DisplayName = "Middle Name")]
  public virtual string MidName
  {
    get => this._MidName;
    set => this._MidName = value;
  }

  [PXDBString(100, IsUnicode = true, BqlField = typeof (Contact.lastName))]
  [PXUIField]
  public virtual string LastName
  {
    get => this._LastName;
    set => this._LastName = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (Contact.fullName))]
  public virtual string FullName
  {
    get => this._FullName;
    set => this._FullName = value;
  }

  [PXDBEmail(BqlField = typeof (Contact.eMail))]
  [PXUIField(DisplayName = "Email")]
  public virtual string EMail
  {
    get => this._EMail;
    set => this._EMail = value;
  }

  [PXDBWeblink(BqlField = typeof (Contact.webSite))]
  [PXUIField(DisplayName = "Web")]
  public virtual string WebSite
  {
    get => this._WebSite;
    set => this._WebSite = value;
  }

  [PXDBString(50, BqlField = typeof (Contact.fax))]
  [PXUIField(DisplayName = "Fax")]
  [PhoneValidation]
  public virtual string Fax
  {
    get => this._Fax;
    set => this._Fax = value;
  }

  [PXDBString(3, BqlField = typeof (Contact.faxType))]
  [PXDefault("BF")]
  [PXUIField(DisplayName = "Fax")]
  [PhoneTypes]
  public virtual string FaxType
  {
    get => this._FaxType;
    set => this._FaxType = value;
  }

  [PXDBString(50, BqlField = typeof (Contact.phone1))]
  [PXUIField(DisplayName = "Phone 1")]
  [PhoneValidation]
  public virtual string Phone1
  {
    get => this._Phone1;
    set => this._Phone1 = value;
  }

  [PXDBString(3, BqlField = typeof (Contact.phone1Type))]
  [PXDefault("B1")]
  [PXUIField(DisplayName = "Phone 1")]
  [PhoneTypes]
  public virtual string Phone1Type
  {
    get => this._Phone1Type;
    set => this._Phone1Type = value;
  }

  [PXDBString(50, BqlField = typeof (Contact.phone2))]
  [PXUIField(DisplayName = "Phone 2")]
  [PhoneValidation]
  public virtual string Phone2
  {
    get => this._Phone2;
    set => this._Phone2 = value;
  }

  [PXDBString(3, BqlField = typeof (Contact.phone2Type))]
  [PXDefault("B2")]
  [PXUIField(DisplayName = "Phone 2")]
  [PhoneTypes]
  public virtual string Phone2Type
  {
    get => this._Phone2Type;
    set => this._Phone2Type = value;
  }

  [PXDBString(50, BqlField = typeof (Contact.phone3))]
  [PXUIField(DisplayName = "Phone 3")]
  [PhoneValidation]
  public virtual string Phone3
  {
    get => this._Phone3;
    set => this._Phone3 = value;
  }

  [PXDBString(3, BqlField = typeof (Contact.phone3Type))]
  [PXDefault("B3")]
  [PXUIField(DisplayName = "Phone 3")]
  [PhoneTypes]
  public virtual string Phone3Type
  {
    get => this._Phone3Type;
    set => this._Phone3Type = value;
  }

  [PXDBDate(BqlField = typeof (Contact.dateOfBirth))]
  [PXUIField(DisplayName = "Date Of Birth")]
  public virtual DateTime? DateOfBirth
  {
    get => this._DateOfBirth;
    set => this._DateOfBirth = value;
  }

  [PXDBString(2, IsFixed = true, BqlField = typeof (Contact.contactType))]
  [PXDefault("PN")]
  public virtual string ContactType
  {
    get => this._ContactType;
    set => this._ContactType = value;
  }

  [PXDBBool(BqlField = typeof (Contact.isActive))]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXNote(BqlField = typeof (Contact.noteID))]
  public virtual Guid? ContactNoteID
  {
    get => this._ContactNoteID;
    set => this._ContactNoteID = value;
  }

  [PXDBCreatedByID(BqlField = typeof (Contact.createdByID))]
  public virtual Guid? ContactCreatedByID
  {
    get => this._ContactCreatedByID;
    set => this._ContactCreatedByID = value;
  }

  [PXDBCreatedByScreenID(BqlField = typeof (Contact.createdByScreenID))]
  public virtual string ContactCreatedByScreenID
  {
    get => this._ContactCreatedByScreenID;
    set => this._ContactCreatedByScreenID = value;
  }

  [PXDBCreatedDateTime(BqlField = typeof (Contact.createdDateTime))]
  public virtual DateTime? ContactCreatedDateTime
  {
    get => this._ContactCreatedDateTime;
    set => this._ContactCreatedDateTime = value;
  }

  [PXDBLastModifiedByID(BqlField = typeof (Contact.lastModifiedByID))]
  public virtual Guid? ContactLastModifiedByID
  {
    get => this._ContactLastModifiedByID;
    set => this._ContactLastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID(BqlField = typeof (Contact.lastModifiedByScreenID))]
  public virtual string ContactLastModifiedByScreenID
  {
    get => this._ContactLastModifiedByScreenID;
    set => this._ContactLastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime(BqlField = typeof (Contact.lastModifiedDateTime))]
  public virtual DateTime? ContactLastModifiedDateTime
  {
    get => this._ContactLastModifiedDateTime;
    set => this._ContactLastModifiedDateTime = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Is Default", Visible = true)]
  public virtual bool? IsDefault
  {
    get => this._IsDefault;
    set => this._IsDefault = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Same as Main")]
  public virtual bool? IsAddressSameAsMain
  {
    get => this._IsAddressSameAsMain;
    set => this._IsAddressSameAsMain = value;
  }

  [PXExtraKey]
  [PXDBInt]
  [PXDBDefault(typeof (BAccount.bAccountID))]
  [PXUIField]
  public override int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  [PXExtraKey]
  [PXDBInt]
  [PXUIField]
  public override int? AddressID
  {
    get => this._AddressID;
    set => this._AddressID = value;
  }

  [PXDBGuid(false)]
  public override Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXUIField]
  [PX.Objects.CR.ContactDisplayName(typeof (ContactExtAddress.lastName), typeof (ContactExtAddress.firstName), typeof (ContactExtAddress.midName), typeof (ContactExtAddress.title), true, BqlField = typeof (Contact.displayName))]
  [PXNavigateSelector(typeof (Contact.displayName))]
  public virtual string ContactDisplayName
  {
    get => this._ContactDisplayName;
    set => this._ContactDisplayName = value;
  }

  public abstract class contactBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContactExtAddress.contactBAccountID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContactExtAddress.contactID>
  {
  }

  public abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContactExtAddress.defAddressID>
  {
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactExtAddress.title>
  {
  }

  public abstract class salutation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactExtAddress.salutation>
  {
  }

  public abstract class firstName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactExtAddress.firstName>
  {
  }

  public abstract class midName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactExtAddress.midName>
  {
  }

  public abstract class lastName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactExtAddress.lastName>
  {
  }

  public abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactExtAddress.fullName>
  {
  }

  public abstract class eMail : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactExtAddress.eMail>
  {
  }

  public abstract class webSite : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactExtAddress.webSite>
  {
  }

  public abstract class fax : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactExtAddress.fax>
  {
  }

  public abstract class faxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactExtAddress.faxType>
  {
  }

  public abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactExtAddress.phone1>
  {
  }

  public abstract class phone1Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactExtAddress.phone1Type>
  {
  }

  public abstract class phone2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactExtAddress.phone2>
  {
  }

  public abstract class phone2Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactExtAddress.phone2Type>
  {
  }

  public abstract class phone3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactExtAddress.phone3>
  {
  }

  public abstract class phone3Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContactExtAddress.phone3Type>
  {
  }

  public abstract class dateOfBirth : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContactExtAddress.dateOfBirth>
  {
  }

  public abstract class contactType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContactExtAddress.contactType>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContactExtAddress.isActive>
  {
  }

  public abstract class contactNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ContactExtAddress.contactNoteID>
  {
  }

  public abstract class contactCreatedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ContactExtAddress.contactCreatedByID>
  {
  }

  public abstract class contactCreatedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContactExtAddress.contactCreatedByScreenID>
  {
  }

  public abstract class contactCreatedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContactExtAddress.contactCreatedDateTime>
  {
  }

  public abstract class contactLastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ContactExtAddress.contactLastModifiedByID>
  {
  }

  public abstract class contactLastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContactExtAddress.contactLastModifiedByScreenID>
  {
  }

  public abstract class contactLastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContactExtAddress.contactLastModifiedDateTime>
  {
  }

  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContactExtAddress.isDefault>
  {
  }

  public abstract class isAddressSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ContactExtAddress.isAddressSameAsMain>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContactExtAddress.bAccountID>
  {
  }

  public new abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContactExtAddress.addressID>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ContactExtAddress.noteID>
  {
  }

  public abstract class contactDisplayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContactExtAddress.contactDisplayName>
  {
  }
}
