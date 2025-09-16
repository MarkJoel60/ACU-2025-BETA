// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSManufacturer
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Manufacturer")]
[PXPrimaryGraph(typeof (ManufacturerMaint))]
[Serializable]
public class FSManufacturer : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ManufacturerAddressID;
  protected int? _ManufacturerContactID;
  protected bool? _AllowOverrideContactAddress;

  [PXDBIdentity]
  [PXUIField(Enabled = false)]
  public virtual int? ManufacturerID { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
  [PXDefault]
  [NormalizeWhiteSpace]
  [PXUIField]
  [PXSelector(typeof (FSManufacturer.manufacturerCD), DescriptionField = typeof (FSManufacturer.descr))]
  public virtual 
  #nullable disable
  string ManufacturerCD { get; set; }

  [PXDBInt]
  [FSDocumentAddress(typeof (Select<PX.Objects.CR.Address, Where<True, Equal<False>>>))]
  public virtual int? ManufacturerAddressID
  {
    get => this._ManufacturerAddressID;
    set => this._ManufacturerAddressID = value;
  }

  [PXDBInt]
  [FSDocumentContact(typeof (Select<PX.Objects.CR.Contact, Where<True, Equal<False>>>))]
  public virtual int? ManufacturerContactID
  {
    get => this._ManufacturerContactID;
    set => this._ManufacturerContactID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? AllowOverrideContactAddress
  {
    get => this._AllowOverrideContactAddress;
    set => this._AllowOverrideContactAddress = value;
  }

  [PXDefault]
  [PXInt]
  public virtual int? LocationID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Contact")]
  [PXSelector(typeof (Search2<PX.Objects.CR.Contact.contactID, InnerJoin<BAccount, On<BAccount.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>>>, Where<PX.Objects.CR.Contact.contactType, NotIn3<ContactTypesAttribute.bAccountProperty, ContactTypesAttribute.broker>, And<Where<BAccount.type, Equal<BAccountType.customerType>, Or<BAccount.type, Equal<BAccountType.prospectType>, Or<BAccount.type, Equal<BAccountType.combinedType>, Or<BAccount.type, Equal<BAccountType.vendorType>>>>>>>>), new System.Type[] {typeof (PX.Objects.CR.Contact.displayName), typeof (PX.Objects.CR.Contact.salutation), typeof (PX.Objects.CR.Contact.fullName), typeof (PX.Objects.CR.Contact.eMail), typeof (PX.Objects.CR.Contact.phone1), typeof (BAccount.type)}, DescriptionField = typeof (PX.Objects.CR.Contact.displayName))]
  public virtual int? ContactID { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Manufacturer")]
  public virtual string ManufacturerGICD { get; set; }

  public class PK : PrimaryKeyOf<FSManufacturer>.By<FSManufacturer.manufacturerID>
  {
    public static FSManufacturer Find(PXGraph graph, int? manufacturerID, PKFindOptions options = 0)
    {
      return (FSManufacturer) PrimaryKeyOf<FSManufacturer>.By<FSManufacturer.manufacturerID>.FindBy(graph, (object) manufacturerID, options);
    }
  }

  public class UK : PrimaryKeyOf<FSManufacturer>.By<FSManufacturer.manufacturerCD>
  {
    public static FSManufacturer Find(PXGraph graph, string manufacturerCD, PKFindOptions options = 0)
    {
      return (FSManufacturer) PrimaryKeyOf<FSManufacturer>.By<FSManufacturer.manufacturerCD>.FindBy(graph, (object) manufacturerCD, options);
    }
  }

  public static class FK
  {
    public class Address : 
      PrimaryKeyOf<FSAddress>.By<FSAddress.addressID>.ForeignKeyOf<FSManufacturer>.By<FSManufacturer.manufacturerAddressID>
    {
    }

    public class Contact : 
      PrimaryKeyOf<FSContact>.By<FSContact.contactID>.ForeignKeyOf<FSManufacturer>.By<FSManufacturer.manufacturerContactID>
    {
    }

    public class CRContact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<FSManufacturer>.By<FSManufacturer.contactID>
    {
    }
  }

  public abstract class manufacturerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSManufacturer.manufacturerID>
  {
  }

  public abstract class manufacturerCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSManufacturer.manufacturerCD>
  {
  }

  public abstract class manufacturerAddressID : IBqlField, IBqlOperand
  {
  }

  public abstract class manufacturerContactID : IBqlField, IBqlOperand
  {
  }

  public abstract class allowOverrideContactAddress : IBqlField, IBqlOperand
  {
  }

  public abstract class locationID : IBqlField, IBqlOperand
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSManufacturer.contactID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSManufacturer.descr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSManufacturer.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSManufacturer.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSManufacturer.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSManufacturer.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSManufacturer.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSManufacturer.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSManufacturer.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSManufacturer.Tstamp>
  {
  }

  public abstract class manufacturerGICD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSManufacturer.manufacturerGICD>
  {
  }
}
