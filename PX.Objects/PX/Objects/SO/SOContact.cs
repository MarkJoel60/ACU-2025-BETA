// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOContact
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <summary>
/// Represents a contact that is used in sales order management.
/// </summary>
/// <remarks>
/// An contact state is frozen at the moment of creation of a sales order.
/// Each modification to the original address leads to the generation of a new <see cref="T:PX.Objects.SO.SOContact.revisionID">revision</see> of the contact, which is used in the new sales order or in the overridden contact in an existed sales order.
/// If the <see cref="T:PX.Objects.SO.SOContact.isDefaultContact" /> field is <see langword="false" />, the contact has been overridden or the original contact has been copied with the <see cref="T:PX.Objects.SO.SOContact.revisionID">revision</see> related to the moment of creation.
/// Also this is the base class for the following derived DACs:
/// <list type="bullet">
/// <item><description><see cref="T:PX.Objects.SO.SOShipmentContact" />, which contains the information related to shipping of the ordered items</description></item>
/// <item><description><see cref="T:PX.Objects.SO.SOShippingContact" />, which contains the information related to shipping of the ordered items</description></item>
/// <item><description><see cref="T:PX.Objects.SO.SOBillingContact" />, which contains the information related to billing of the ordered items</description></item>
/// </list>
/// The records of this type are created and edited on the following forms:
/// <list type="bullet">
/// <item><description><i>Invoices (SO303000)</i> form (corresponds to the <see cref="T:PX.Objects.SO.SOInvoiceEntry" /> graph)</description></item>
/// <item><description><i>Shipments (SO302000)</i> form (corresponds to the <see cref="T:PX.Objects.SO.SOShipmentEntry" /> graph)</description></item>
/// <item><description><i>Sales Orders (SO301000)</i> form (corresponds to the <see cref="T:PX.Objects.SO.SOOrderEntry" /> graph)</description></item>
/// </list>
/// </remarks>
[PXCacheName("SO Contact")]
[Serializable]
public class SOContact : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IContact,
  INotable,
  IContactBase,
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

  /// <summary>The identifier of the contact.</summary>
  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Contact ID", Visible = false)]
  public virtual int? ContactID
  {
    get => this._ContactID;
    set => this._ContactID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.Customer">customer</see>.
  /// The field is included in <see cref="T:PX.Objects.SO.SOContact.FK.Customer" />.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.AR.Customer.bAccountID" /> field.
  /// </value>
  [PXDBInt]
  [PXDBDefault(typeof (SOOrder.customerID))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.CustomerID" />
  public virtual int? BAccountID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.ARAddress">customer contact</see>.
  /// The field is included in <see cref="T:PX.Objects.SO.SOContact.FK.CustomerContact" />.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.AR.ARContact.contactID" /> field.
  /// </value>
  [PXDBInt]
  public virtual int? CustomerContactID
  {
    get => this._CustomerContactID;
    set => this._CustomerContactID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.CustomerContactID" />
  public virtual int? BAccountContactID
  {
    get => this._CustomerContactID;
    set => this._CustomerContactID = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the contact is default.
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
  /// If the value of the <see cref="T:PX.Objects.SO.SOContact.isDefaultContact" /> field is <see langword="null" />, the value of this field is <see langword="null" />.
  /// If the value of the <see cref="T:PX.Objects.SO.SOContact.isDefaultContact" /> field is <see langword="true" />, the value of this field is <see langword="false" />.
  /// If the value of the <see cref="T:PX.Objects.SO.SOContact.isDefaultContact" /> field is <see langword="false" />, the value of this field is <see langword="true" />.
  /// </value>
  [PXBool]
  [PXUIField]
  public virtual bool? OverrideContact
  {
    [PXDependsOnFields(new System.Type[] {typeof (SOContact.isDefaultContact)})] get
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
  /// Indicates if  Personally identifiable Information fields
  /// (example Email , Phone1, Fax etc.) are encrypted or not
  /// </summary>
  [PXDBBool]
  public virtual bool? IsEncrypted { get; set; }

  /// <summary>The identifier of the revision contact.</summary>
  /// <remarks>
  /// Each modification to the original contact leads to the generation of a new revision of the contact, which is used in the new sales order or in the overridden contact in an existed sales order.
  /// </remarks>
  [PXDBInt]
  [PXDefault]
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
  /// The field is used in the contacts business letters, which would be used to direct the letter to the proper person if the letter is not addressed to any specific person.
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

  string IContactBase.EMail => this._Email;

  public string Address => this.Email;

  public string DisplayName => this.FullName;

  public class PK : PrimaryKeyOf<SOContact>.By<SOContact.contactID>
  {
    public static SOContact Find(PXGraph graph, int? contactID, PKFindOptions options = 0)
    {
      return (SOContact) PrimaryKeyOf<SOContact>.By<SOContact.contactID>.FindBy(graph, (object) contactID, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<SOContact>.By<SOContact.customerID>
    {
    }

    public class CustomerContact : 
      PrimaryKeyOf<ARContact>.By<ARContact.contactID>.ForeignKeyOf<SOContact>.By<SOContact.customerContactID>
    {
    }
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.ContactID" />
  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOContact.contactID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.CustomerID" />
  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOContact.customerID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.CustomerContactID" />
  public abstract class customerContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOContact.customerContactID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.IsDefaultContact" />
  public abstract class isDefaultContact : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOContact.isDefaultContact>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.OverrideContact" />
  public abstract class overrideContact : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOContact.overrideContact>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.IsEncrypted" />
  public abstract class isEncrypted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOContact.isEncrypted>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.RevisionID" />
  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOContact.revisionID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.Title" />
  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOContact.title>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.Salutation" />
  public abstract class salutation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOContact.salutation>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.Attention" />
  public abstract class attention : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOContact.attention>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.FullName" />
  public abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOContact.fullName>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.Email" />
  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOContact.email>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.Fax" />
  public abstract class fax : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOContact.fax>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.FaxType" />
  public abstract class faxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOContact.faxType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.Phone1" />
  public abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOContact.phone1>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.Phone1Type" />
  public abstract class phone1Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOContact.phone1Type>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.Phone2" />
  public abstract class phone2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOContact.phone2>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.Phone2Type" />
  public abstract class phone2Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOContact.phone2Type>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.Phone3" />
  public abstract class phone3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOContact.phone3>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.Phone3Type" />
  public abstract class phone3Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOContact.phone3Type>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOContact.NoteID" />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOContact.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOContact.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOContact.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOContact.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOContact.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOContact.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOContact.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOContact.Tstamp>
  {
  }
}
