// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OUSearchEntity
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
[PXHidden]
[Serializable]
public class OUSearchEntity : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private 
  #nullable disable
  string _eMail;
  private string _DisplayName;
  private string _ErrorMessage;
  private string _fullName;
  protected string _CountryID;
  private string _EntityName;
  private string _EntityID;

  [PXUIField]
  [PXDBString]
  [PXDBDefault(typeof (OUSearchEntity.outgoingEmail))]
  public virtual string EMail
  {
    get => this._eMail;
    set => this._eMail = value?.Trim();
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string DisplayName
  {
    get => this._DisplayName;
    set => this._DisplayName = value;
  }

  [PXDependsOnFields(new System.Type[] {typeof (OUSearchEntity.newContactLastName), typeof (OUSearchEntity.newContactFirstName)})]
  [PersonDisplayName(typeof (OUSearchEntity.newContactLastName), typeof (OUSearchEntity.newContactFirstName))]
  [PXUIField]
  public virtual string NewContactDisplayName { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField]
  public virtual string NewContactFirstName { get; set; }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField]
  public virtual string NewContactLastName { get; set; }

  [PXUIField]
  [PXDBString]
  [PXDBDefault(typeof (OUSearchEntity.outgoingEmail))]
  public virtual string NewContactEmail { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXStringList]
  public virtual string OutgoingEmail { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string ErrorMessage
  {
    get => this._ErrorMessage;
    set => this._ErrorMessage = value;
  }

  [PXDBString(IsFixed = true)]
  [PXUIField]
  public virtual string Operation { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search2<Contact.contactID, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>>, Where<Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Contact.contactType, Equal<ContactTypesAttribute.employee>, Or<Contact.contactType, Equal<ContactTypesAttribute.lead>>>>>))]
  public virtual int? ContactID { get; set; }

  [PXDBInt]
  public virtual int? ContactBaccountID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [ContactTypes(BqlTable = typeof (Contact))]
  [PXUIField]
  public virtual string ContactType { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Class ID")]
  [PXSelector(typeof (CRLeadClass.classID), DescriptionField = typeof (CRLeadClass.description), CacheGlobal = true)]
  public virtual string LeadClassID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Source")]
  [CRMSources(BqlTable = typeof (CRLead), BqlField = typeof (CRLead.source))]
  [PXFormula(typeof (Selector<OUSearchEntity.leadClassID, CRLeadClass.defaultSource>))]
  public virtual string LeadSource { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Contact Class")]
  [PXSelector(typeof (CRContactClass.classID), DescriptionField = typeof (CRContactClass.description), CacheGlobal = true)]
  public virtual string ContactClassID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Source")]
  [CRMSources(BqlTable = typeof (Contact), BqlField = typeof (CRLead.source))]
  public virtual string ContactSource { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Salutation { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Account")]
  [CustomerProspectVendor(null, null, null)]
  public virtual int? BAccountID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  [PXFormula(typeof (Selector<OUSearchEntity.bAccountID, BAccount.acctName>))]
  public virtual string FullName
  {
    get => this._fullName;
    set => this._fullName = value;
  }

  [PXDefault(typeof (Search<Branch.countryID, Where<Branch.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXDBString(100)]
  [PXUIField(DisplayName = "Country")]
  [Country]
  public virtual string CountryID
  {
    get => this._CountryID;
    set => this._CountryID = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string EntityName
  {
    get => this._EntityName;
    set => this._EntityName = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string EntityID
  {
    get => this._EntityID;
    set => this._EntityID = value;
  }

  [PXString]
  public virtual string PrevItemId { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Attachment File Names")]
  public virtual string AttachmentNames { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? AttachmentsCount { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Recognition In Progress", Visible = false)]
  public virtual bool? IsRecognitionInProgress { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "", Visible = false)]
  public virtual bool? RecognitionIsNotStarted { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "", Visible = false, Enabled = false)]
  public virtual string NumOfRecognizedDocuments { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "", Visible = false, Enabled = false)]
  public virtual bool? NumOfRecognizedDocumentsCheck { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "", Visible = false, Enabled = false)]
  public virtual string DuplicateFilesMsg { get; set; }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "", Visible = false, Enabled = false)]
  public virtual bool? IsDuplicateDelected { get; set; }

  public abstract class eMail : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUSearchEntity.eMail>
  {
  }

  public abstract class displayName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUSearchEntity.displayName>
  {
  }

  public abstract class newContactDisplayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OUSearchEntity.newContactDisplayName>
  {
  }

  public abstract class newContactFirstName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OUSearchEntity.newContactFirstName>
  {
  }

  public abstract class newContactLastName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OUSearchEntity.newContactLastName>
  {
  }

  public abstract class newContactEmail : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OUSearchEntity.newContactEmail>
  {
  }

  public abstract class outgoingEmail : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OUSearchEntity.outgoingEmail>
  {
  }

  public abstract class errorMessage : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUSearchEntity.errorMessage>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUSearchEntity.operation>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OUSearchEntity.contactID>
  {
  }

  public abstract class contactBaccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OUSearchEntity.contactBaccountID>
  {
  }

  public abstract class contactType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUSearchEntity.contactType>
  {
  }

  public abstract class leadClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUSearchEntity.leadClassID>
  {
  }

  public abstract class leadSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUSearchEntity.leadSource>
  {
  }

  public abstract class contactClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OUSearchEntity.contactClassID>
  {
  }

  public abstract class contactSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OUSearchEntity.contactSource>
  {
  }

  public abstract class salutation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUSearchEntity.salutation>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OUSearchEntity.bAccountID>
  {
  }

  public abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUSearchEntity.fullName>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUSearchEntity.countryID>
  {
  }

  public abstract class entityName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUSearchEntity.entityName>
  {
  }

  public abstract class entityID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUSearchEntity.entityID>
  {
  }

  public abstract class prevItemId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUSearchEntity.prevItemId>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  OUSearchEntity.noteID>
  {
  }

  public abstract class attachmentNames : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OUSearchEntity.attachmentNames>
  {
  }

  public abstract class attachmentsCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OUSearchEntity.attachmentsCount>
  {
  }

  public abstract class isRecognitionInProgress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    OUSearchEntity.isRecognitionInProgress>
  {
  }

  public abstract class recognitionIsNotStarted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    OUSearchEntity.recognitionIsNotStarted>
  {
  }

  public abstract class numOfRecognizedDocuments : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OUSearchEntity.numOfRecognizedDocuments>
  {
  }

  public abstract class numOfRecognizedDocumentsCheck : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    OUSearchEntity.numOfRecognizedDocumentsCheck>
  {
  }

  public abstract class duplicateFilesMsg : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OUSearchEntity.duplicateFilesMsg>
  {
  }

  public abstract class isDuplicateDelected : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    OUSearchEntity.isDuplicateDelected>
  {
  }
}
