// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POContact
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

#nullable enable
namespace PX.Objects.PO;

/// <summary>
/// Represents a contact that is used in purchase order management.
/// </summary>
/// <remarks>
/// An contact state is frozen at the moment of creation of a purchase order.
/// Each modification to the original address leads to the generation of a new <see cref="T:PX.Objects.PO.POContact.revisionID">revision</see> of the contact, which is used in the new purchase order or in the overridden contact in an existed purchase order.
/// If the <see cref="T:PX.Objects.PO.POContact.isDefaultContact" /> field is <see langword="false" />, the contact has been overridden or the original contact has been copied with the <see cref="T:PX.Objects.PO.POContact.revisionID">revision</see> related to the moment of creation.
/// Also this is the base class for the following derived DACs:
/// <list type="bullet">
/// <item><description><see cref="T:PX.Objects.PO.POShipContact" />, which contains the information related to the shipping of the ordered items</description></item>
/// <item><description>`<see cref="T:PX.Objects.PO.PORemitContact" />, which contains the information about the vendor to supply the ordered items</description></item>
/// </list>
/// The records of this type are created and edited on the <i>Purchase Orders (PO301000)</i> form
/// (which corresponds to the <see cref="T:PX.Objects.PO.POOrderEntry" /> graph).
/// </remarks>
[PXCacheName("PO Contact")]
[Serializable]
public class POContact : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IContact,
  INotable,
  IEmailMessageTarget
{
  protected int? _ContactID;
  protected int? _BAccountID;
  protected int? _BAccountContactID;
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

  /// <summary>The identifier of the contact.</summary>
  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Contact ID", Visible = false)]
  public virtual int? ContactID
  {
    get => this._ContactID;
    set => this._ContactID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Data.Licensing.BAccount">business account</see>.
  /// The field is included in <see cref="T:PX.Objects.PO.POContact.FK.BAccount" />.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Data.Licensing.BAccount.bAccountID" /> field.
  /// </value>
  [PXDBInt]
  [PXDefault]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Data.Licensing.Contact">business contact</see>.
  /// The field is included in <see cref="T:PX.Objects.PO.POContact.FK.BAccountContact" />.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Data.Licensing.Contact.contactID" /> field.
  /// </value>
  [PXDBInt]
  public virtual int? BAccountContactID
  {
    get => this._BAccountContactID;
    set => this._BAccountContactID = value;
  }

  /// <summary>
  /// If the value is <see langword="false" />, the contact has been overridden or the original contact has been copied with the revision related to the moment of creation.
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
  /// Specifies (if set to <see langword="true" />) that the contact is overriden.
  /// </summary>
  /// <value>
  /// If the value of the <see cref="T:PX.Objects.PO.POContact.isDefaultContact" /> field is <see langword="null" />, the value of this field is <see langword="null" />.
  /// If the value of the <see cref="T:PX.Objects.PO.POContact.isDefaultContact" /> field is <see langword="true" />, the value of this field is <see langword="false" />.
  /// If the value of the <see cref="T:PX.Objects.PO.POContact.isDefaultContact" /> field is <see langword="false" />, the value of this field is <see langword="true" />.
  /// </value>
  [PXBool]
  [PXUIField]
  public virtual bool? OverrideContact
  {
    [PXDependsOnFields(new System.Type[] {typeof (POContact.isDefaultContact)})] get
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

  /// <summary>The identifier of the revision contact.</summary>
  /// <remarks>
  /// Each modification to the original contact leads to the generation of a new revision of the contact, which is used in the new purchase order or in the overridden contact in an existed purchase order.
  /// </remarks>
  [PXDefault]
  [PXDBInt]
  [PXUIField]
  public virtual int? RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  /// <summary>The name title of the contact.</summary>
  [PXDBString(50, IsUnicode = true)]
  [Titles]
  [PXUIField(DisplayName = "Title")]
  public virtual string Title
  {
    get => this._Title;
    set => this._Title = value;
  }

  /// <summary>The job title of the contact.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string Salutation
  {
    get => this._Salutation;
    set => this._Salutation = value;
  }

  /// <summary>
  /// The field that is used in the contact's business letters to direct the letter to the proper person if the letter is not addressed to any specific person.
  /// </summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string Attention { get; set; }

  /// <summary>The account name of the contact.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Account Name")]
  [PXPersonalDataField]
  public virtual string FullName
  {
    get => this._FullName;
    set => this._FullName = value;
  }

  /// <summary>The email of the contact.</summary>
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
  /// The field can have one of the values described in <see cref="T:PX.Objects.CR.PhoneTypesAttribute" />.
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
  /// The field can have one of the values described in <see cref="T:PX.Objects.CR.PhoneTypesAttribute" />.
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
  /// The field can have one of the values described in <see cref="T:PX.Objects.CR.PhoneTypesAttribute" />.
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
  /// The field can have one of the values described in <see cref="T:PX.Objects.CR.PhoneTypesAttribute" />.
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

  public string Address => this.Email;

  public string DisplayName => this.FullName;

  public class PK : PrimaryKeyOf<POContact>.By<POContact.contactID>
  {
    public static POContact Find(PXGraph graph, int? contactID, PKFindOptions options = 0)
    {
      return (POContact) PrimaryKeyOf<POContact>.By<POContact.contactID>.FindBy(graph, (object) contactID, options);
    }
  }

  public static class FK
  {
    public class BAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<POContact>.By<POContact.bAccountID>
    {
    }

    public class BAccountContact : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<POContact>.By<POContact.bAccountContactID>
    {
    }
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.ContactID" />
  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POContact.contactID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.BAccountID" />
  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POContact.bAccountID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.BAccountContactID" />
  public abstract class bAccountContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POContact.bAccountContactID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.IsDefaultContact" />
  public abstract class isDefaultContact : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POContact.isDefaultContact>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.OverrideContact" />
  public abstract class overrideContact : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POContact.overrideContact>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.RevisionID" />
  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POContact.revisionID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.Title" />
  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POContact.title>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.Salutation" />
  public abstract class salutation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POContact.salutation>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.Attention" />
  public abstract class attention : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POContact.attention>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.FullName" />
  public abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POContact.fullName>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.Email" />
  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POContact.email>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.Fax" />
  public abstract class fax : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POContact.fax>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.FaxType" />
  public abstract class faxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POContact.faxType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.Phone1" />
  public abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POContact.phone1>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.Phone1Type" />
  public abstract class phone1Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POContact.phone1Type>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.Phone2" />
  public abstract class phone2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POContact.phone2>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.Phone2Type" />
  public abstract class phone2Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POContact.phone2Type>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.Phone3" />
  public abstract class phone3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POContact.phone3>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.Phone3Type" />
  public abstract class phone3Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POContact.phone3Type>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POContact.NoteID" />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POContact.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POContact.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POContact.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POContact.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POContact.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POContact.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POContact.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POContact.Tstamp>
  {
  }
}
