// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMContact
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a contact that is specified in the <see cref="T:PX.Objects.PM.PMProject">project</see> and
/// in the <see cref="T:PX.Objects.PM.PMProforma">pro forma invoice</see>.
/// A <see cref="T:PX.Objects.PM.PMContact" /> record is a copy of customer location's
/// <see cref="T:PX.Objects.CR.Contact" /> and can be used to override the contact at the document level.
/// The record is independent of changes to the original <see cref="T:PX.Objects.CR.Contact" /> record.
/// The entities of this type are created and edited on the Projects (PM301000) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.ProjectEntry" /> graph) and on the
/// Pro Forma Invoices (PM307000) form (which corresponds to the <see cref="T:PX.Objects.PM.ProformaEntry" /> graph).
/// </summary>
[PXCacheName("Project Contact")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMContact : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IContact,
  INotable,
  IEmailMessageTarget
{
  protected int? _ContactID;
  protected int? _CustomerID;
  protected int? _CustomerContactID;
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

  /// <summary>
  /// The unique integer identifier of the record.
  /// This field is the primary key field.
  /// </summary>
  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Contact ID", Visible = false)]
  public virtual int? ContactID
  {
    get => this._ContactID;
    set => this._ContactID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="!:Customer" /> record,
  /// which is specified in the document to which the contact belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:Customer.BAccountID" /> field.
  /// </value>
  [PXDBInt]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  /// <summary>
  /// An alias for <see cref="P:PX.Objects.PM.PMContact.CustomerID" />, which exists
  /// for the purpose of implementing the <see cref="T:PX.Objects.CS.IContact" />
  /// interface.
  /// </summary>
  public virtual int? BAccountID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Contact" /> record
  /// from which the contact was originally created.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [PXDBInt]
  public virtual int? CustomerContactID
  {
    get => this._CustomerContactID;
    set => this._CustomerContactID = value;
  }

  /// <summary>
  /// An alias for <see cref="P:PX.Objects.PM.PMContact.CustomerContactID" />,
  /// which exists for the purpose of
  /// implementing the <see cref="T:PX.Objects.CS.IContact" /> interface.
  /// </summary>
  public virtual int? BAccountContactID
  {
    get => this._CustomerContactID;
    set => this._CustomerContactID = value;
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that the contact record
  /// is identical to the original <see cref="T:PX.Objects.CR.Contact" /> record
  /// referenced by the <see cref="P:PX.Objects.PM.PMContact.CustomerContactID" /> field.
  /// </summary>
  [PXDBBool]
  [PXUIField]
  [PXDefault(true)]
  [PXExtraKey]
  public virtual bool? IsDefaultContact
  {
    get => this._IsDefaultContact;
    set => this._IsDefaultContact = value;
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that the contact
  /// overrides the default <see cref="T:PX.Objects.CR.Contact" /> record
  /// referenced by the <see cref="P:PX.Objects.PM.PMContact.CustomerContactID" /> field.
  /// </summary>
  /// <value>
  /// This field is the inverse of <see cref="P:PX.Objects.PM.PMContact.IsDefaultContact" />.
  /// </value>
  [PXBool]
  [PXUIField]
  public virtual bool? OverrideContact
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMContact.isDefaultContact)})] get
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

  /// <summary>
  /// The revision ID of the original <see cref="T:PX.Objects.CR.Contact" />
  /// record from which the record originates.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Contact.RevisionID" /> field.
  /// </value>
  [PXDBInt]
  [PXDefault]
  public virtual int? RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  /// <summary>The contact's title.</summary>
  /// <value>
  /// This field can have one of the values defined
  /// by <see cref="T:PX.Objects.CR.TitlesAttribute" />.
  /// </value>
  [PXDBString(50, IsUnicode = true)]
  [Titles]
  [PXUIField(DisplayName = "Title")]
  public virtual string Title
  {
    get => this._Title;
    set => this._Title = value;
  }

  /// <summary>
  /// The name of the contact person. This field is usually
  /// used in email templates, as shown in the following
  /// example: <c>Dear {Contact.Salutation}!</c>.
  /// </summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string Salutation
  {
    get => this._Salutation;
    set => this._Salutation = value;
  }

  /// <summary>
  /// The department or person to be noted on the attention line.
  /// </summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string Attention { get; set; }

  /// <summary>The business name or the company name of the contact.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Account Name")]
  [PXPersonalDataField]
  public virtual string FullName
  {
    get => this._FullName;
    set => this._FullName = value;
  }

  /// <summary>The e-mail address of the contact.</summary>
  [PXDBEmail]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string Email
  {
    get => this._Email;
    set => this._Email = value;
  }

  /// <summary>The fax number of the contact.</summary>
  [PXDBString(50)]
  [PXUIField(DisplayName = "Fax")]
  [PhoneValidation]
  [PXPersonalDataField]
  public virtual string Fax
  {
    get => this._Fax;
    set => this._Fax = value;
  }

  /// <summary>The type of the fax number of the contact.</summary>
  /// <value>
  /// This field can have one of the values
  /// defined by <see cref="T:PX.Objects.CR.PhoneTypesAttribute" />.
  /// </value>
  [PXDBString(3)]
  [PXDefault("BF")]
  [PXUIField(DisplayName = "Fax")]
  [PhoneTypes]
  public virtual string FaxType
  {
    get => this._FaxType;
    set => this._FaxType = value;
  }

  /// <summary>The first phone number of the contact.</summary>
  [PXDBString(50)]
  [PXUIField]
  [PhoneValidation]
  [PXPersonalDataField]
  public virtual string Phone1
  {
    get => this._Phone1;
    set => this._Phone1 = value;
  }

  /// <summary>The type of the first phone number of the contact.</summary>
  /// <value>
  /// This field can have one of the values defined
  /// by <see cref="T:PX.Objects.CR.PhoneTypesAttribute" />.
  /// </value>
  [PXDBString(3)]
  [PXDefault("B1")]
  [PXUIField(DisplayName = "Phone 1")]
  [PhoneTypes]
  public virtual string Phone1Type
  {
    get => this._Phone1Type;
    set => this._Phone1Type = value;
  }

  /// <summary>The second phone number of the contact.</summary>
  [PXDBString(50)]
  [PXUIField(DisplayName = "Phone 2")]
  [PhoneValidation]
  [PXPersonalDataField]
  public virtual string Phone2
  {
    get => this._Phone2;
    set => this._Phone2 = value;
  }

  /// <summary>The type of the second phone number of the contact.</summary>
  /// <value>
  /// This field can have one of the values defined
  /// by <see cref="T:PX.Objects.CR.PhoneTypesAttribute" />.
  /// </value>
  [PXDBString(3)]
  [PXDefault("B2")]
  [PXUIField(DisplayName = "Phone 2")]
  [PhoneTypes]
  public virtual string Phone2Type
  {
    get => this._Phone2Type;
    set => this._Phone2Type = value;
  }

  /// <summary>The third phone number of the contact.</summary>
  [PXDBString(50)]
  [PXUIField(DisplayName = "Phone 3")]
  [PhoneValidation]
  [PXPersonalDataField]
  public virtual string Phone3
  {
    get => this._Phone3;
    set => this._Phone3 = value;
  }

  /// <summary>The type of the third phone number of the contact.</summary>
  /// <value>
  /// This field can have one of the values defined
  /// by <see cref="T:PX.Objects.CR.PhoneTypesAttribute" />.
  /// </value>
  [PXDBString(3)]
  [PXDefault("H1")]
  [PXUIField(DisplayName = "Phone 3")]
  [PhoneTypes]
  public virtual string Phone3Type
  {
    get => this._Phone3Type;
    set => this._Phone3Type = value;
  }

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

  /// <summary>
  /// An implementation of the <see cref="T:PX.Objects.CR.IEmailMessageTarget" /> interface.
  /// Returns the value of the <see cref="P:PX.Objects.PM.PMContact.Email" /> property.
  /// </summary>
  public string Address => this.Email;

  /// <summary>
  /// An implementation of the <see cref="T:PX.Objects.CR.IEmailMessageTarget" /> interface.
  /// Returns the value of the <see cref="P:PX.Objects.PM.PMContact.FullName" /> property.
  /// </summary>
  public string DisplayName => this.FullName;

  public class PK : PrimaryKeyOf<PMContact>.By<PMContact.contactID>
  {
    public static PMContact Find(PXGraph graph, int? contactID, PKFindOptions options = 0)
    {
      return (PMContact) PrimaryKeyOf<PMContact>.By<PMContact.contactID>.FindBy(graph, (object) contactID, options);
    }
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMContact.contactID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMContact.customerID>
  {
  }

  public abstract class customerContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMContact.customerContactID>
  {
  }

  public abstract class isDefaultContact : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMContact.isDefaultContact>
  {
  }

  public abstract class overrideContact : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMContact.overrideContact>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMContact.revisionID>
  {
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMContact.title>
  {
  }

  public abstract class salutation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMContact.salutation>
  {
  }

  public abstract class attention : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMContact.attention>
  {
  }

  public abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMContact.fullName>
  {
  }

  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMContact.email>
  {
  }

  public abstract class fax : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMContact.fax>
  {
  }

  public abstract class faxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMContact.faxType>
  {
  }

  public abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMContact.phone1>
  {
  }

  public abstract class phone1Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMContact.phone1Type>
  {
  }

  public abstract class phone2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMContact.phone2>
  {
  }

  public abstract class phone2Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMContact.phone2Type>
  {
  }

  public abstract class phone3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMContact.phone3>
  {
  }

  public abstract class phone3Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMContact.phone3Type>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMContact.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMContact.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMContact.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMContact.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMContact.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMContact.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMContact.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMContact.Tstamp>
  {
  }
}
