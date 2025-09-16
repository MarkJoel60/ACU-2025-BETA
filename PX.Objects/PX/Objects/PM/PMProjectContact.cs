// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProjectContact
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common.Serialization;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>Project's contact list.</summary>
[PXSerializable]
[PXCacheName("Project Contact")]
public class PMProjectContact : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// Gets or sets the contact if it is active in the current project.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Currently Involved")]
  public virtual bool? IsActive { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the contact.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDBDefault(typeof (PMProject.contractID))]
  [Project(IsKey = true)]
  public int? ProjectID { get; set; }

  /// <summary>Business account associated with the contact.</summary>
  [PXDBInt]
  [PXSelector(typeof (Search<BAccountR.bAccountID, Where<BAccountR.status, NotEqual<CustomerStatus.inactive>, And<Where<BAccountR.type, Equal<BAccountType.vendorType>, Or<BAccountR.type, Equal<BAccountType.customerType>, Or<BAccountR.type, Equal<BAccountType.combinedType>>>>>>>), new System.Type[] {typeof (BAccountR.acctCD), typeof (BAccountR.acctName), typeof (BAccountR.type)}, SubstituteKey = typeof (BAccountR.acctCD), DescriptionField = typeof (BAccountR.acctName))]
  [PXUIField(DisplayName = "Business Account")]
  public int? BusinessAccountID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Contact" /> object linked with the current contact.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXRestrictor(typeof (Where<PX.Objects.CR.Contact.isActive, Equal<True>>), "The contact is not active.", new System.Type[] {}, SuppressVerify = true)]
  [PXSelector(typeof (Search<PX.Objects.CR.Contact.contactID, Where<PX.Objects.CR.Contact.contactType, In3<ContactTypesAttribute.person, ContactTypesAttribute.employee>, And<Where<PX.Objects.CR.Contact.bAccountID, Equal<Current<PMProjectContact.businessAccountID>>, Or<Current<PMProjectContact.businessAccountID>, IsNull>>>>>), SubstituteKey = typeof (PX.Objects.CR.Contact.contactID), DescriptionField = typeof (PX.Objects.CR.Contact.displayName), Filterable = true)]
  [PXForeignReference(typeof (PMProjectContact.FK.Contact))]
  public int? ContactID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PMRole" /> object linked with the current contact.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="T:PX.Objects.PMRole.roleID" /> field.
  /// </value>
  [PXSelector(typeof (Search<PMRole.roleID>), DescriptionField = typeof (PMRole.description))]
  [PXDBString(64 /*0x40*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Role")]
  public 
  #nullable disable
  string RoleID { get; set; }

  /// <summary>Role description.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Role Description")]
  public string RoleDescription { get; set; }

  [PXNote]
  [NotePersist(typeof (PMProjectContact.noteID))]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<PMProjectContact>.By<PMProjectContact.projectID, PMProjectContact.contactID>
  {
    public static PMProjectContact Find(
      PXGraph graph,
      int? projectID,
      int? contactID,
      PKFindOptions options = 0)
    {
      return (PMProjectContact) PrimaryKeyOf<PMProjectContact>.By<PMProjectContact.projectID, PMProjectContact.contactID>.FindBy(graph, (object) projectID, (object) contactID, options);
    }
  }

  public static class FK
  {
    public class Contact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<PMProjectContact>.By<PMProjectContact.contactID>
    {
    }
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProjectContact.isActive>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProjectContact.projectID>
  {
  }

  public abstract class businessAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProjectContact.businessAccountID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProjectContact.contactID>
  {
  }

  public abstract class roleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProjectContact.roleID>
  {
  }

  public abstract class roleDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProjectContact.roleDescription>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMProjectContact.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMProjectContact.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMProjectContact.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProjectContact.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMProjectContact.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMProjectContact.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProjectContact.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMProjectContact.lastModifiedDateTime>
  {
  }
}
