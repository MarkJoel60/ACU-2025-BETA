// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APContact
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
namespace PX.Objects.AP;

/// <summary>A payment-specific remittance contact information.</summary>
[PXCacheName("AP Contact")]
[Serializable]
public class APContact : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IContact,
  INotable,
  IEmailMessageTarget
{
  protected int? _ContactID;
  protected int? _VendorID;
  protected int? _VendorContactID;
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
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Contact ID", Visible = false)]
  public virtual int? ContactID
  {
    get => this._ContactID;
    set => this._ContactID = value;
  }

  [PXDBInt]
  [PXDBDefault(typeof (APRegister.vendorID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  public virtual int? BAccountID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBInt]
  public virtual int? VendorContactID
  {
    get => this._VendorContactID;
    set => this._VendorContactID = value;
  }

  public virtual int? BAccountContactID
  {
    get => this._VendorContactID;
    set => this._VendorContactID = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Default Vendor Contact", Visibility = PXUIVisibility.Visible)]
  [PXDefault(true)]
  [PXExtraKey]
  public virtual bool? IsDefaultContact
  {
    get => this._IsDefaultContact;
    set => this._IsDefaultContact = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Override Contact", Visibility = PXUIVisibility.Visible)]
  public virtual bool? OverrideContact
  {
    [PXDependsOnFields(new System.Type[] {typeof (APContact.isDefaultContact)})] get
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
  [PXDefault(0)]
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
  [PXUIField(DisplayName = "Job Title", Visibility = PXUIVisibility.SelectorVisible)]
  [PXPersonalDataField]
  public virtual string Salutation
  {
    get => this._Salutation;
    set => this._Salutation = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Attention", Visibility = PXUIVisibility.SelectorVisible)]
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
  [PXUIField(DisplayName = "Email", Visibility = PXUIVisibility.SelectorVisible)]
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
  [PXDefault("BF", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Fax")]
  [PhoneTypes]
  public virtual string FaxType
  {
    get => this._FaxType;
    set => this._FaxType = value;
  }

  [PXDBString(50)]
  [PXUIField(DisplayName = "Phone 1", Visibility = PXUIVisibility.SelectorVisible)]
  [PhoneValidation]
  [PXPersonalDataField]
  public virtual string Phone1
  {
    get => this._Phone1;
    set => this._Phone1 = value;
  }

  [PXDBString(3)]
  [PXDefault("B1", PersistingCheck = PXPersistingCheck.Nothing)]
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
  [PXDefault("B2", PersistingCheck = PXPersistingCheck.Nothing)]
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
  [PXDefault("H1", PersistingCheck = PXPersistingCheck.Nothing)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual System.DateTime? LastModifiedDateTime
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

  public class PK : PrimaryKeyOf<APContact>.By<APContact.contactID>
  {
    public static APContact Find(PXGraph graph, int? addressID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APContact>.By<APContact.contactID>.FindBy(graph, (object) addressID, options);
    }
  }

  public static class FK
  {
    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APContact>.By<APContact.vendorID>
    {
    }

    public class VendorContact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<APContact>.By<APContact.vendorContactID>
    {
    }
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APContact.contactID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APContact.vendorID>
  {
  }

  public abstract class vendorContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APContact.vendorContactID>
  {
  }

  public abstract class isDefaultContact : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APContact.isDefaultContact>
  {
  }

  public abstract class overrideContact : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APContact.overrideContact>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APContact.revisionID>
  {
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APContact.title>
  {
  }

  public abstract class salutation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APContact.salutation>
  {
  }

  public abstract class attention : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APContact.attention>
  {
  }

  public abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APContact.fullName>
  {
  }

  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APContact.email>
  {
  }

  public abstract class fax : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APContact.fax>
  {
  }

  public abstract class faxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APContact.faxType>
  {
  }

  public abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APContact.phone1>
  {
  }

  public abstract class phone1Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APContact.phone1Type>
  {
  }

  public abstract class phone2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APContact.phone2>
  {
  }

  public abstract class phone2Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APContact.phone2Type>
  {
  }

  public abstract class phone3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APContact.phone3>
  {
  }

  public abstract class phone3Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APContact.phone3Type>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APContact.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APContact.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APContact.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APContact.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APContact.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APContact.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APContact.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APContact.Tstamp>
  {
  }
}
