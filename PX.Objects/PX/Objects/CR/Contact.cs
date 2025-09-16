// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Contact
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR.MassProcess;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GDPR;
using PX.SM;
using PX.TM;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.CR;

/// <summary>
/// Depending on the type, represents a real person or the contact information of other entities like <see cref="T:PX.Objects.CR.CRLead" />,
/// <see cref="T:PX.Objects.EP.EPEmployee" />, <see cref="T:PX.Objects.CR.BAccount" />, an so on.
/// The records of this type are mostly created and edited on the Contacts (CR302000) form,
/// which corresponds to the <see cref="T:PX.Objects.CR.ContactMaint" /> graph.
/// Also, records of the derived class <see cref="T:PX.Objects.CR.CRLead" /> are mostly created and edited on the Leads (CR301000) form,
/// which corresponds to the <see cref="T:PX.Objects.CR.LeadMaint" /> graph.
/// </summary>
[CRCacheIndependentPrimaryGraph(typeof (EmployeeMaint), typeof (Where<Contact.contactType, Equal<ContactTypesAttribute.employee>, And<Contact.contactID, Less<int0>>>))]
[CRCacheIndependentPrimaryGraph(typeof (EmployeeMaint), typeof (Select<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Current<Contact.contactID>>>>))]
[CRCacheIndependentPrimaryGraph(typeof (BusinessAccountMaint), typeof (Select<BAccount, Where<BAccount.defContactID, Equal<Current<Contact.contactID>>, And<BAccount.type, NotEqual<ContactTypesAttribute.employee>>>>))]
[CRCacheIndependentPrimaryGraph(typeof (ContactMaint), typeof (Select<Contact, Where<Contact.contactID, IsNull, And<Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Where<Contact.contactID, Equal<Current<Contact.contactID>>, And<Where<Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Contact.contactType, Equal<ContactTypesAttribute.bAccountProperty>>>>>>>>>))]
[PXODataDocumentTypesRestriction(typeof (ContactMaint))]
[CRCacheIndependentPrimaryGraph(typeof (LeadMaint), typeof (Select<CRLead, Where<CRLead.contactID, Equal<Current<Contact.contactID>>>>))]
[CRContactCacheName("Contact")]
[CREmailContactsView(typeof (Select2<Contact, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>>, Where<Contact.contactID, Equal<Optional<Contact.contactID>>, Or2<Where<Optional<Contact.bAccountID>, IsNotNull, And<BAccount.bAccountID, Equal<Optional<Contact.bAccountID>>>>, Or<Contact.contactType, Equal<ContactTypesAttribute.employee>>>>>))]
[PXGroupMask(typeof (LeftJoinSingleTable<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>>), WhereRestriction = typeof (Where<BAccount.bAccountID, IsNull, Or<Match<BAccount, Current<AccessInfo.userName>>>>))]
[ERPTransactions(Suppress = true)]
[DebuggerDisplay("{GetType().Name,nq}: ContactID = {ContactID,nq}, ContactType = {ContactType}, MemberName = {MemberName}")]
[Serializable]
public class Contact : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IContactBase,
  IPersonalContact,
  IContact,
  INotable,
  IAssign,
  IPXSelectable,
  IEmailMessageTarget,
  IConsentable
{
  protected int? _defAddressID;
  private 
  #nullable disable
  string _fullName;
  private string _eMail;
  protected string _DuplicateStatus;
  private DateTime? _assignDate;
  protected string _CampaignID;
  protected DateTime? _GrammValidationDateTime;
  protected string _SearchSuggestion;
  protected string _ExtRefNbr;
  protected string _LanguageID;

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Selected { get; set; }

  /// <summary>
  /// The display name of the contact.
  /// Its value is made up of the <see cref="P:PX.Objects.CR.Contact.LastName" />, <see cref="P:PX.Objects.CR.Contact.FirstName" />, <see cref="P:PX.Objects.CR.Contact.MidName" />, and
  /// <see cref="P:PX.Objects.CR.Contact.Title" /> values. The format depends on the <see cref="P:PX.SM.PreferencesGeneral.PersonNameFormat" /> site setting.
  /// </summary>
  /// <remarks>
  /// This field is changed when the fields it depends on are changed.
  /// </remarks>
  [PXUIField]
  [PXDependsOnFields(new System.Type[] {typeof (Contact.lastName), typeof (Contact.firstName), typeof (Contact.midName), typeof (Contact.title)})]
  [PersonDisplayName(typeof (Contact.lastName), typeof (Contact.firstName), typeof (Contact.midName), typeof (Contact.title))]
  [PXFieldDescription]
  [PXDefault]
  [PXUIRequired(typeof (Where<Contact.contactType, Equal<ContactTypesAttribute.person>>))]
  [PXNavigateSelector(typeof (Search2<Contact.displayName, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.contactID>>>, Where2<Where<Contact.contactType, Equal<ContactTypesAttribute.lead>, Or<Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Contact.contactType, Equal<ContactTypesAttribute.employee>>>>, And<Where<BAccount.bAccountID, IsNull, Or<Match<BAccount, Current<AccessInfo.userName>>>>>>>))]
  [PXPersonalDataField]
  [PXContactInfoField]
  public virtual string DisplayName { get; set; }

  /// <summary>
  /// Similar to <see cref="P:PX.Objects.CR.Contact.DisplayName" />, but unlike <see cref="P:PX.Objects.CR.Contact.DisplayName" />, it is a calculated field.
  /// It is equal to <see cref="P:PX.Objects.CR.Contact.DisplayName" /> if the latter is not <tt>null</tt>, and to <see cref="P:PX.Objects.CR.Contact.FullName" /> otherwise.
  /// </summary>
  [PXDBCalced(typeof (Switch<Case<Where<Contact.displayName, Equal<Empty>>, Contact.fullName>, Contact.displayName>), typeof (string))]
  [PXUIField]
  [PXString(255 /*0xFF*/, IsUnicode = true)]
  public virtual string MemberName { get; set; }

  /// <summary>
  /// The identifier of the contact.
  /// This field is the key field.
  /// </summary>
  /// <value>
  /// This field is an identity auto-increment database field that in normal conditions cannot be duplicated in different tenants.
  /// This value is present in the URI of the open document: "ScreenId=CR302000&amp;ContactID=100044".
  /// </value>
  [PXDBIdentity(IsKey = true, BqlField = typeof (Contact.contactID))]
  [PXUIField]
  [ContactSelector(true, new System.Type[] {typeof (ContactTypesAttribute.person), typeof (ContactTypesAttribute.employee)})]
  [PXPersonalDataWarning]
  public virtual int? ContactID { get; set; }

  /// <summary>The identifier of the revision of the contact.</summary>
  /// <value>
  /// It is increased at each update of the contact.
  /// This field is used to check whether the original contact was changed.
  /// Tables in <tt>PX.Objects</tt> that rely on this field:
  /// <see cref="T:PX.Objects.AP.APContact" />,
  /// <see cref="T:PX.Objects.AR.ARContact" />,
  /// <see cref="T:PX.Objects.CR.CRContact" />,
  /// <see cref="T:PX.Objects.PM.PMContact" />,
  /// <see cref="T:PX.Objects.PO.POContact" />,
  /// <see cref="T:PX.Objects.SO.SOContact" />,
  /// </value>
  [PXDBInt]
  [PXDefault(0)]
  [AddressRevisionID]
  public virtual int? RevisionID { get; set; }

  [Obsolete("Use OverrideAddress instead")]
  [PXBool]
  [PXUIField(DisplayName = "Same as in Account")]
  public virtual bool? IsAddressSameAsMain { get; set; }

  /// <summary>
  /// Specifies whether the <see cref="T:PX.Objects.CR.Address">address</see>
  /// information of this contact differs from the <see cref="T:PX.Objects.CR.Address">address</see> information
  /// of the <see cref="T:PX.Objects.CR.BAccount">business account</see> associated with this contact.
  /// IF it is so, the <see cref="T:PX.Objects.CR.Address">address</see> information is synchronized with the associated
  /// <see cref="T:PX.Objects.CR.BAccount">business account</see>.
  /// </summary>
  /// <remarks>
  /// The behavior is controlled by the <see cref="T:PX.Objects.CR.ContactMaint.ContactBAccountSharedAddressOverrideGraphExt" />
  /// graph extension.
  /// </remarks>
  [PXBool]
  [PXUIField(DisplayName = "Override Address")]
  public virtual bool? OverrideAddress { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Address" /> object linked with the current document.
  /// The field can be empty for the contacts of the following types:
  /// <see cref="F:PX.Objects.CR.ContactTypesAttribute.BAccountProperty" /> and <see cref="F:PX.Objects.CR.ContactTypesAttribute.Employee" />.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Address.AddressID" /> field.
  /// </value>
  [PXDBInt]
  [PXSelector(typeof (PX.Objects.CR.Address.addressID), DirtyRead = true)]
  [PXUIField(DisplayName = "Address")]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Address.addressID))]
  public virtual int? DefAddressID
  {
    get => this._defAddressID;
    set => this._defAddressID = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the <see cref="T:PX.Objects.CR.Contact">contact</see>,
  /// which the current <see cref="T:PX.Objects.CR.ContactAccount" /> represents, is primary for the <see cref="T:PX.Objects.CR.BAccount">business account</see>,
  /// which the current <see cref="T:PX.Objects.CR.ContactAccount" /> also represents.
  /// </summary>
  /// <remarks>
  /// It is used only by the <see cref="T:PX.Objects.CR.BusinessAccountMaint" /> graph.
  /// </remarks>
  [PXBool]
  [PXUIField(DisplayName = "Primary", Enabled = false)]
  public virtual bool? IsPrimary { get; set; }

  /// <summary>The title of the person.</summary>
  /// <value>
  /// The predefined values are listed in the <see cref="T:PX.Objects.CR.TitlesAttribute" /> class,
  /// but this field can have any value.
  /// </value>
  [PXDBString(50, IsUnicode = true)]
  [Titles]
  [PXUIField(DisplayName = "Title")]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  public virtual string Title { get; set; }

  /// <summary>The first name of the person.</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "First Name")]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXPersonalDataField]
  [PXContactInfoField]
  public virtual string FirstName { get; set; }

  /// <summary>The middle name of the person.</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Middle Name")]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXPersonalDataField]
  public virtual string MidName { get; set; }

  /// <summary>The last name of the person.</summary>
  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Last Name")]
  [CRLastNameDefault]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXPersonalDataField]
  [PXContactInfoField]
  public virtual string LastName { get; set; }

  /// <summary>The job title of the person.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXMassUpdatableField]
  [PXPersonalDataField]
  [PXContactInfoField]
  public virtual string Salutation { get; set; }

  /// <summary>
  /// The name of the document recipient (a person or team) used in the documents.
  /// </summary>
  /// <remarks>
  /// Not used in primary graph, only in documents, for instance, <see cref="T:PX.Objects.CR.CROpportunity" />, <see cref="T:PX.Objects.SO.SOOrder" />, and so on.
  /// </remarks>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Attention")]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXMassUpdatableField]
  [PXPersonalDataField]
  public virtual string Attention { get; set; }

  /// <summary>
  /// The identifier of the related <see cref="T:PX.Objects.CR.BAccount">business account</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [PXDBInt]
  [CRContactBAccountDefault]
  [PXUIField(DisplayName = "Business Account")]
  [PXSelector(typeof (BAccount.bAccountID), SubstituteKey = typeof (BAccount.acctCD), DescriptionField = typeof (BAccount.acctName), DirtyRead = true)]
  [PXMassUpdatableField]
  public virtual int? BAccountID { get; set; }

  /// <summary>The name of the company the contact works for.</summary>
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  [PXPersonalDataField]
  [PXContactInfoField]
  public virtual string FullName
  {
    get => this._fullName;
    set => this._fullName = value;
  }

  /// <summary>
  /// The identifier of the account that is considered as parent for the current account (<see cref="P:PX.Objects.CR.Contact.BAccountID" />).
  /// </summary>
  /// <remarks>There is no business logic in the application for this field.</remarks>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [ParentBAccount(typeof (Contact.bAccountID), null, null, null, null)]
  [PXMassUpdatableField]
  public virtual int? ParentBAccountID { get; set; }

  /// <summary>The email address of the contact.</summary>
  /// <value>
  /// The field should be a valid email address, or a list of email addresses separated by semicolons.
  /// The email addresses will be validated with the <see cref="M:PX.Common.Mail.EmailParser.ParseAddresses(System.String)" /> method.
  /// </value>
  [PXDBEmail]
  [PXUIField]
  [PXMassMergableField]
  [PXDeduplicationSearchEmailField]
  [PXDefault]
  [PXPersonalDataField]
  [PXContactInfoField]
  public virtual string EMail
  {
    get => this._eMail;
    set => this._eMail = value?.Trim();
  }

  string IContact.Email
  {
    get => this.EMail;
    set => this.EMail = value;
  }

  /// <summary>The URL of the contact website.</summary>
  /// <value>The field should contain a valid URL.</value>
  [PXDBWeblink]
  [PXUIField(DisplayName = "Web")]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXPersonalDataField]
  [PXContactInfoField]
  public virtual string WebSite { get; set; }

  /// <summary>The fax number.</summary>
  /// <value>
  /// The type of the number can be set by the <see cref="P:PX.Objects.CR.Contact.FaxType" /> field.
  /// The value should match the phone validation pattern specified for the current company.
  /// See <see cref="P:PX.Objects.GL.Company.PhoneMask" /> for details.
  /// </value>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Fax")]
  [PhoneValidation]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXPersonalDataField]
  [PXContactInfoField]
  public virtual string Fax { get; set; }

  /// <summary>
  /// The phone type for the <see cref="P:PX.Objects.CR.Contact.Fax" /> field.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.PhoneTypesAttribute" /> class.
  /// </value>
  [PXDBString(3)]
  [PXDefault("BF")]
  [PXUIField(DisplayName = "Fax Type")]
  [PhoneTypes]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXContactInfoField]
  public virtual string FaxType { get; set; }

  /// <summary>The phone number.</summary>
  /// <value>
  /// The type of the number can be set by the <see cref="P:PX.Objects.CR.Contact.Phone1Type" /> field.
  /// The value should match the phone validation pattern specified for the current company.
  /// See <see cref="P:PX.Objects.GL.Company.PhoneMask" /> for details.
  /// </value>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField]
  [PhoneValidation]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXPersonalDataField]
  [PXPhone]
  [PXContactInfoField]
  public virtual string Phone1 { get; set; }

  /// <summary>
  /// The phone type for the <see cref="P:PX.Objects.CR.Contact.Phone1" /> field.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.PhoneTypesAttribute" /> class.
  /// </value>
  [PXDBString(3)]
  [PXDefault("B1")]
  [PXUIField(DisplayName = "Phone 1 Type")]
  [PhoneTypes]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXContactInfoField]
  public virtual string Phone1Type { get; set; }

  /// <summary>The second phone number.</summary>
  /// <value>
  /// The type of the number can be set by the <see cref="P:PX.Objects.CR.Contact.Phone2Type" /> field.
  /// The value should match the phone validation pattern specified for the current company.
  /// See <see cref="P:PX.Objects.GL.Company.PhoneMask" /> for details.
  /// </value>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Phone 2")]
  [PhoneValidation]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXPersonalDataField]
  [PXPhone]
  [PXContactInfoField]
  public virtual string Phone2 { get; set; }

  /// <summary>
  /// The phone type for the <see cref="P:PX.Objects.CR.Contact.Phone2" /> field.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.PhoneTypesAttribute" /> class.
  /// </value>
  [PXDBString(3)]
  [PXDefault("C")]
  [PXUIField(DisplayName = "Phone 2 Type")]
  [PhoneTypes]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXContactInfoField]
  public virtual string Phone2Type { get; set; }

  /// <summary>The third phone number.</summary>
  /// <value>
  /// The type of the number can be set by the <see cref="P:PX.Objects.CR.Contact.Phone3Type" /> field.
  /// The value should match the phone validation pattern specified for the current company.
  /// See <see cref="P:PX.Objects.GL.Company.PhoneMask" /> for details.
  /// </value>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Phone 3")]
  [PhoneValidation]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXPersonalDataField]
  [PXPhone]
  [PXContactInfoField]
  public virtual string Phone3 { get; set; }

  /// <summary>
  /// The phone type for the <see cref="P:PX.Objects.CR.Contact.Phone3" /> field.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.PhoneTypesAttribute" /> class.
  /// </value>
  [PXDBString(3)]
  [PXDefault("H1")]
  [PXUIField(DisplayName = "Phone 3 Type")]
  [PhoneTypes]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXContactInfoField]
  public virtual string Phone3Type { get; set; }

  /// <summary>The date of birth.</summary>
  [PXDefault]
  [PXDBDate]
  [PXUIField(DisplayName = "Date Of Birth")]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXPersonalDataField]
  public virtual DateTime? DateOfBirth { get; set; }

  /// <inheritdoc />
  [PXSearchable(1024 /*0x0400*/, "{0}: {1}", new System.Type[] {typeof (Contact.contactType), typeof (Contact.displayName)}, new System.Type[] {typeof (Contact.fullName), typeof (Contact.eMail), typeof (Contact.phone1), typeof (Contact.phone2), typeof (Contact.phone3), typeof (Contact.webSite)}, WhereConstraint = typeof (Where<BqlOperand<Contact.contactType, IBqlString>.IsNotIn<ContactTypesAttribute.bAccountProperty, ContactTypesAttribute.employee>>), MatchWithJoin = typeof (LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>>), Line1Format = "{0}{1}{2}{3}", Line1Fields = new System.Type[] {typeof (Contact.fullName), typeof (Contact.salutation), typeof (Contact.phone1), typeof (Contact.eMail)}, Line2Format = "{1}{2}{3}", Line2Fields = new System.Type[] {typeof (Contact.defAddressID), typeof (PX.Objects.CR.Address.displayName), typeof (PX.Objects.CR.Address.city), typeof (PX.Objects.CR.Address.state), typeof (PX.Objects.CR.Address.countryID)})]
  [PXUniqueNote(DescriptionField = typeof (Contact.displayName), Selector = typeof (FbqlSelect<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<BAccount>.On<BqlOperand<BAccount.bAccountID, IBqlInt>.IsEqual<Contact.bAccountID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.contactType, Equal<ContactTypesAttribute.person>>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.bAccountID, IsNull>>>>.Or<Match<BAccount, Current<AccessInfo.userName>>>>>>, Contact>.SearchFor<Contact.contactID>), ShowInReferenceSelector = true)]
  [PXTimeTag(typeof (Contact.noteID))]
  [PXUIField]
  public virtual Guid? NoteID { get; set; }

  /// <summary>
  /// Specifies whether the current contact is active and can be specified in documents.
  /// </summary>
  /// <remarks>
  /// Only active contacts can be specified in such documents as
  /// <see cref="T:PX.Objects.CR.CROpportunity" />, <see cref="T:PX.Objects.CR.CRCase" />, <see cref="T:PX.Objects.CR.CRQuote" />, <see cref="T:PX.Objects.PM.PMQuote" />.
  /// The duplicate validation feature <see cref="P:PX.Objects.CS.FeaturesSet.ContactDuplicate" /> works only with active contacts.
  /// </remarks>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active", Enabled = false)]
  public virtual bool? IsActive { get; set; }

  /// <summary>
  /// Specifies whether the current contact's type is not the <see cref="F:PX.Objects.CR.ContactTypesAttribute.Employee">employee type</see>.
  /// </summary>
  /// <value>
  /// The value is <see langword="true" /> when the value of <see cref="P:PX.Objects.CR.Contact.ContactType" />
  /// is not equal to <see cref="F:PX.Objects.CR.ContactTypesAttribute.Employee" />.
  /// </value>
  [PXBool]
  [PXUIField]
  public virtual bool? IsNotEmployee => new bool?(!(this.ContactType == "EP"));

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that no fax should be sent to the contact.
  /// </summary>
  /// <value>
  /// The system does not rely on this field, but the value can be used by an external system or as an indicator.
  /// </value>
  [PXDBBool]
  [PXUIField(DisplayName = "Do Not Fax", FieldClass = "CRM")]
  [PXMassUpdatableField]
  [PXDefault(false)]
  [PXPersonalDataField(DefaultValue = false)]
  public virtual bool? NoFax { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that no mail should be sent to the contact.
  /// </summary>
  /// <value>
  /// The system does not rely on this field, but the value can be used by an external system or as an indicator.
  /// </value>
  [PXDBBool]
  [PXUIField(DisplayName = "Do Not Mail", FieldClass = "CRM")]
  [PXMassUpdatableField]
  [PXDefault(false)]
  [PXPersonalDataField(DefaultValue = false)]
  public virtual bool? NoMail { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the email of the contact will not be involved in the mass email process.
  /// </summary>
  /// <value>
  /// Contacts with this field set to <see langword="true" /> will not receive mass emails
  /// sent by the <see cref="T:PX.Objects.CR.CRMassMailMaint" /> graph (the Mass Emails (CR308000) form).
  /// </value>
  [PXDBBool]
  [PXUIField(DisplayName = "No Marketing", FieldClass = "CRM")]
  [PXMassUpdatableField]
  [PXDefault(false)]
  [PXPersonalDataField(DefaultValue = false)]
  public virtual bool? NoMarketing { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the contact should not be called.
  /// </summary>
  /// <value>
  /// The system does not rely on this field, but the value can be used by an external system or as an indicator.
  /// </value>
  [PXDBBool]
  [PXUIField(DisplayName = "Do Not Call", FieldClass = "CRM")]
  [PXMassUpdatableField]
  [PXDefault(false)]
  [PXPersonalDataField(DefaultValue = false)]
  public virtual bool? NoCall { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the email of the contact will not be involved in the mass email process.
  /// This contact will not receive any notification emails.
  /// </summary>
  /// <value>
  /// Contacts with this field set to <see langword="true" /> will not receive mass emails
  /// sent by the <see cref="T:PX.Objects.CR.CRMassMailMaint" /> graph (the Mass Emails (CR308000) form).
  /// </value>
  [PXDBBool]
  [PXUIField(DisplayName = "Do Not Email", FieldClass = "CRM")]
  [PXMassUpdatableField]
  [PXDefault(false)]
  [PXPersonalDataField(DefaultValue = false)]
  public virtual bool? NoEMail { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the email of the contact will not be involved in the mass email process.
  /// </summary>
  /// <value>
  /// Contacts with this field set to <see langword="true" /> will not receive mass emails
  /// sent by the <see cref="T:PX.Objects.CR.CRMassMailMaint" /> graph (the Mass Emails (CR308000) form).
  /// </value>
  [PXDBBool]
  [PXUIField(DisplayName = "No Mass Mail", FieldClass = "CRM")]
  [PXMassUpdatableField]
  [PXDefault(false)]
  [PXPersonalDataField(DefaultValue = false)]
  public virtual bool? NoMassMail { get; set; }

  /// <summary>The gender of the contact.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.GendersAttribute" /> class,
  /// and a default value can be set depending on the value of <see cref="P:PX.Objects.CR.Contact.Title" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Gender")]
  [Genders(typeof (Contact.title))]
  [PXMassMergableField]
  [PXPersonalDataField]
  public virtual string Gender { get; set; }

  /// <summary>The marital status of the contact.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.MaritalStatusesAttribute" /> class.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Marital Status")]
  [MaritalStatuses]
  [PXMassMergableField]
  [PXPersonalDataField]
  public virtual string MaritalStatus { get; set; }

  /// <summary>The wedding date of the contact.</summary>
  [Obsolete("The field is not used anymore")]
  [PXDBDate]
  [PXUIField(DisplayName = "Wedding Date")]
  public virtual DateTime? Anniversary { get; set; }

  /// <summary>The name of the spouse or partner of the contact.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Spouse/Partner Name")]
  [PXMassMergableField]
  [PXPersonalDataField]
  public virtual string Spouse { get; set; }

  /// <summary>The image attached to the contact.</summary>
  /// <value>
  /// The value can be fetched from the exchange server during contacts synchronization (see <see cref="P:PX.Objects.CS.FeaturesSet.ExchangeIntegration" />).
  /// </value>
  [PXUIField]
  [PXDBString(IsUnicode = true)]
  public string Img { get; set; }

  /// <summary>
  /// Specifies whether the contact should be included in exchange synchronization.
  /// </summary>
  /// <value>
  /// The value is used in the exchange integration (see <see cref="P:PX.Objects.CS.FeaturesSet.ExchangeIntegration" />).
  /// The default value is <see langword="true" />.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Synchronize with Exchange", FieldClass = "ExchangeIntegration")]
  public virtual bool? Synchronize { get; set; }

  /// <summary>The type of the contact.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.ContactTypesAttribute" /> class.
  /// The default value is <see cref="F:PX.Objects.CR.ContactTypesAttribute.Person" />.
  /// This field must be specified at the initialization stage and not be changed afterwards.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXDefault("PN")]
  [ContactTypes]
  [PXUIField]
  public virtual string ContactType { get; set; }

  /// <summary>
  /// The numeric representation of <see cref="P:PX.Objects.CR.Contact.ContactType" /> used to sort a grid containing contacts of different types.
  /// </summary>
  /// <value>
  /// The value is used in the following graphs: <see cref="T:PX.Objects.CR.OUSearchMaint" />, <see cref="T:PX.Objects.CR.CampaignMaint" />, <see cref="T:PX.Objects.CR.CRMarketingListMaint" />.
  /// The possible values match <see cref="P:PX.Objects.CR.Contact.ContactType" /> values as follows:
  /// <list type="table">
  ///   <listheader>
  ///     <term>ContactType</term>
  ///     <description>Priority</description>
  ///   </listheader>
  ///   <item>
  ///     <term><see cref="F:PX.Objects.CR.ContactTypesAttribute.BAccountProperty" /></term>
  ///     <description><see cref="!:ContactTypesAttribute.bAccountPriority" /></description>
  ///   </item>
  ///   <item>
  ///     <term><see cref="F:PX.Objects.CR.ContactTypesAttribute.SalesPerson" /></term>
  ///     <description><see cref="!:ContactTypesAttribute.salesPersonPriority" /></description>
  ///   </item>
  ///   <item>
  ///     <term><see cref="F:PX.Objects.CR.ContactTypesAttribute.Employee" /></term>
  ///     <description><see cref="!:ContactTypesAttribute.employeePriority" /></description>
  ///   </item>
  ///   <item>
  ///     <term><see cref="F:PX.Objects.CR.ContactTypesAttribute.Person" /></term>
  ///     <description><see cref="!:ContactTypesAttribute.personPriority" /></description>
  ///   </item>
  ///   <item>
  ///     <term><see cref="F:PX.Objects.CR.ContactTypesAttribute.Lead" /></term>
  ///     <description><see cref="!:ContactTypesAttribute.leadPriority" /></description>
  ///   </item>
  /// </list>
  /// </value>
  [PXDBCalced(typeof (Switch<Case<Where<Contact.contactType, Equal<ContactTypesAttribute.bAccountProperty>>, ContactTypesAttribute.Priority.bAccountPriority, Case<Where<Where<Contact.contactType, Equal<ContactTypesAttribute.salesPerson>>>, ContactTypesAttribute.Priority.salesPersonPriority, Case<Where<Where<Contact.contactType, Equal<ContactTypesAttribute.employee>>>, ContactTypesAttribute.Priority.employeePriority, Case<Where<Where<Contact.contactType, Equal<ContactTypesAttribute.person>>>, ContactTypesAttribute.Priority.personPriority, Case<Where<Where<Contact.contactType, Equal<ContactTypesAttribute.lead>>>, ContactTypesAttribute.Priority.leadPriority>>>>>>), typeof (int))]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  public virtual int? ContactPriority { get; set; }

  /// <summary>The duplicate status of the contact.</summary>
  /// <value>
  /// The field indicates whether the contact was validated by the duplicate validation (see <see cref="P:PX.Objects.CS.FeaturesSet.ContactDuplicate" />).
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.DuplicateStatusAttribute" /> class.
  /// The default value is <see cref="F:PX.Objects.CR.DuplicateStatusAttribute.NotValidated" />.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PX.Objects.CR.DuplicateStatus]
  [PXDefault("NV")]
  [PXUIField(DisplayName = "Duplicate", FieldClass = "DUPLICATE")]
  public virtual string DuplicateStatus
  {
    get => this._DuplicateStatus;
    set => this._DuplicateStatus = value;
  }

  /// <summary>
  /// Specifies whether <see cref="P:PX.Objects.CR.Contact.DuplicateStatus" /> is equal to <see cref="F:PX.Objects.CR.DuplicateStatusAttribute.PossibleDuplicated" />
  /// when the <see cref="P:PX.Objects.CS.FeaturesSet.ContactDuplicate" /> feature is enabled.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Duplicate Found")]
  [PXDBCalced(typeof (Switch<Case<Where<Contact.duplicateStatus, Equal<DuplicateStatusAttribute.possibleDuplicated>, And<FeatureInstalled<FeaturesSet.contactDuplicate>>>, True>, False>), typeof (bool))]
  public virtual bool? DuplicateFound { get; set; }

  /// <summary>The status of the contact.</summary>
  /// <value>
  /// The field values are controlled by the workflow engine, and the field is not used by the application logic directly.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Status")]
  [ContactStatus.List]
  [PXMassUpdatableField]
  public virtual string Status { get; set; }

  /// <summary>
  /// The reason why the <see cref="P:PX.Objects.CR.Contact.Status" /> of this contact has been changed.
  /// </summary>
  /// <value>
  /// The property is used by the <see cref="T:PX.Objects.CR.LeadMaint" /> graph, displayed on the Leads (CR301000)
  /// form for <see cref="T:PX.Objects.CR.CRLead" /> objects, and controlled by the workflow engine.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXStringList(new string[] {}, new string[] {})]
  [PXUIField(DisplayName = "Reason")]
  public virtual string Resolution { get; set; }

  /// <summary>
  /// The date and time when <see cref="P:PX.Objects.CR.Contact.OwnerID" /> or <see cref="P:PX.Objects.CR.Contact.WorkgroupID" /> were last changed.
  /// </summary>
  [PXUIField(DisplayName = "Assignment Date")]
  [AssignedDate(typeof (Contact.workgroupID), typeof (Contact.ownerID))]
  public virtual DateTime? AssignDate
  {
    get => this._assignDate ?? this.CreatedDateTime;
    set => this._assignDate = value;
  }

  /// <summary>The identifier of the class.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.CRContactClass.ClassID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Contact Class")]
  [PXSelector(typeof (CRContactClass.classID), DescriptionField = typeof (CRContactClass.description), CacheGlobal = true)]
  [PXDefault(typeof (Search<CRSetup.defaultContactClassID, Where<Current<Contact.contactType>, Equal<ContactTypesAttribute.person>>>))]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXMassUpdatableField]
  public virtual string ClassID { get; set; }

  /// <summary>
  /// The source of the contact. If a contact was created from a <see cref="T:PX.Objects.CR.CRLead">lead</see>,
  /// the value is copied from the lead related to the contact.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.CRMSourcesAttribute" /> class.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Source")]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [CRMSources]
  public virtual string Source { get; set; }

  /// <inheritdoc />
  [PXDBInt]
  [PXUIField(DisplayName = "Workgroup")]
  [PXCompanyTreeSelector]
  [PXMassUpdatableField]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  public virtual int? WorkgroupID { get; set; }

  /// <inheritdoc />
  [Owner(typeof (Contact.workgroupID))]
  [PXMassUpdatableField]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  public virtual int? OwnerID { get; set; }

  /// <summary>
  /// The identifier of the user associated with the contact.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.SM.Users.PKID">Users.PKID</see> field.
  /// </value>
  [PXDBGuid(false)]
  [PXForeignReference]
  [PXUser]
  public virtual Guid? UserID { get; set; }

  /// <summary>
  /// The identifier of the campaign that resulted in creation of the contact.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.CRCampaign.CampaignID">CRCampaign.campaignID</see> field.
  /// The property is used by the <see cref="T:PX.Objects.CR.LeadMaint" /> graph and displayed on the Leads (CR301000) form.
  /// </value>
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Source Campaign")]
  [PXSelector(typeof (CRCampaign.campaignID), DescriptionField = typeof (CRCampaign.campaignName))]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  public virtual string CampaignID
  {
    get => this._CampaignID;
    set => this._CampaignID = value;
  }

  /// <summary>The person's preferred method of contact.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.CRContactMethodsAttribute" /> class.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [CRContactMethods]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Contact Method")]
  [PXMassUpdatableField]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  public virtual string Method { get; set; }

  [Obsolete("The field is not used anymore.")]
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Can Be Converted", Visible = false)]
  public virtual bool? IsConvertable { get; set; }

  /// <summary>
  /// The date and time of the last gramm validation.
  /// The field is preserved for internal use.
  /// </summary>
  [CRValidateDate]
  public virtual DateTime? GrammValidationDateTime
  {
    get => this._GrammValidationDateTime;
    set => this._GrammValidationDateTime = value;
  }

  /// <inheritdoc />
  [PXConsentAgreementField]
  public virtual bool? ConsentAgreement { get; set; }

  /// <inheritdoc />
  [PXConsentDateField]
  public virtual DateTime? ConsentDate { get; set; }

  /// <inheritdoc />
  [PXConsentExpirationDateField]
  public virtual DateTime? ConsentExpirationDate { get; set; }

  /// <summary>
  /// The attributes list available for the current contact.
  /// The field is preserved for internal use.
  /// </summary>
  [CRAttributesField(typeof (Contact.classID))]
  public virtual string[] Attributes { get; set; }

  /// <summary>
  /// This field is used to implement the <see cref="P:PX.Objects.CR.IEmailMessageTarget.Address" />
  /// interface only, it always returns <see cref="P:PX.Objects.CR.Contact.EMail" />.
  /// </summary>
  public string Address => this.EMail;

  /// <summary>
  /// The flag identified that the <see cref="T:PX.Objects.CR.Contact.salesTerritoryID" /> is filled automatically
  /// based on <see cref="T:PX.Objects.CR.Address.state" /> and <see cref="T:PX.Objects.CR.Address.countryID" /> or can be assigned manually.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Territory", FieldClass = "SalesTerritoryManagement")]
  public virtual bool? OverrideSalesTerritory { get; set; }

  /// <summary>
  /// The reference to <see cref="T:PX.Objects.CS.SalesTerritory.salesTerritoryID" />. If <see cref="T:PX.Objects.CR.Contact.overrideSalesTerritory" />
  /// is <see langword="false" /> then it's filled automatically
  /// based on <see cref="T:PX.Objects.CR.Address.state" /> and <see cref="T:PX.Objects.CR.Address.countryID" />
  /// otherwise it's assigned by user.
  /// </summary>
  [SalesTerritoryField]
  [PXUIEnabled(typeof (Where<BqlOperand<Contact.overrideSalesTerritory, IBqlBool>.IsEqual<True>>))]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXForeignReference(typeof (Contact.FK.SalesTerritory))]
  public virtual string SalesTerritoryID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [Obsolete("The field is not used anymore")]
  [PXString(150, IsUnicode = true)]
  [PXUIField(DisplayName = "Search Suggestion")]
  [PXDBCalced(typeof (BqlOperand<Quotes, IBqlString>.Concat<BqlOperand<Contact.displayName, IBqlString>.Concat<BqlOperand<Quotes, IBqlString>.Concat<BqlOperand<Space, IBqlString>.Concat<BqlOperand<OpenBracket, IBqlString>.Concat<BqlOperand<Contact.eMail, IBqlString>.Concat<CloseBracket>>>>>>), typeof (string))]
  public virtual string SearchSuggestion
  {
    get => this._SearchSuggestion;
    set => this._SearchSuggestion = value;
  }

  /// <summary>
  /// The external reference number of the contact.
  /// It can be an additional number of the contact used in external integration.
  /// </summary>
  [PXDBString(40, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXMassMergableField]
  [PXDeduplicationSearchField(true, true)]
  public virtual string ExtRefNbr
  {
    get => this._ExtRefNbr;
    set => this._ExtRefNbr = value;
  }

  /// <summary>
  /// The language in which the contact prefers to communicate.
  /// </summary>
  /// <value>
  /// By default, the system fills in the box with the locale specified for the contact's country.
  /// This field is displayed on the form only if there are multiple active locales
  /// configured on the System Locales (SM200550) form
  /// (corresponds to the <see cref="T:PX.SM.LocaleMaintenance" /> graph).
  /// </value>
  [PXDBString(10, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Language/Locale")]
  [PXSelector(typeof (Search<Locale.localeName, Where<Locale.isActive, Equal<True>>>), DescriptionField = typeof (Locale.description))]
  [ContacLanguageDefault(typeof (PX.Objects.CR.Address.countryID))]
  public virtual string LanguageID
  {
    get => this._LanguageID;
    set => this._LanguageID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  /// <exclude />
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DeletedDatabaseRecord { get; set; }

  bool? IContact.IsDefaultContact
  {
    get => new bool?();
    set
    {
    }
  }

  int? IContact.BAccountContactID
  {
    get => new int?();
    set
    {
    }
  }

  public class PK : PrimaryKeyOf<Contact>.By<Contact.contactID>
  {
    public static Contact Find(PXGraph graph, int? contactID, PKFindOptions options = 0)
    {
      return (Contact) PrimaryKeyOf<Contact>.By<Contact.contactID>.FindBy(graph, (object) contactID, options);
    }
  }

  public static class FK
  {
    public class Class : 
      PrimaryKeyOf<CRContactClass>.By<CRContactClass.classID>.ForeignKeyOf<Contact>.By<Contact.classID>
    {
    }

    public class BusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<Contact>.By<Contact.bAccountID>
    {
    }

    public class ParentBusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<Contact>.By<Contact.parentBAccountID>
    {
    }

    public class Address : 
      PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<Contact>.By<Contact.defAddressID>
    {
    }

    public class Owner : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<Contact>.By<Contact.ownerID>
    {
    }

    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<Contact>.By<Contact.workgroupID>
    {
    }

    public class User : PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<Contact>.By<Contact.userID>
    {
    }

    public class SalesTerritory : 
      PrimaryKeyOf<PX.Objects.CS.SalesTerritory>.By<PX.Objects.CS.SalesTerritory.salesTerritoryID>.ForeignKeyOf<Contact>.By<Contact.salesTerritoryID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contact.selected>
  {
  }

  public abstract class displayName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.displayName>
  {
  }

  public abstract class memberName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.memberName>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contact.contactID>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contact.revisionID>
  {
  }

  [Obsolete("Use OverrideAddress instead")]
  public abstract class isAddressSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Contact.isAddressSameAsMain>
  {
  }

  public abstract class overrideAddress : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contact.overrideAddress>
  {
  }

  public abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contact.defAddressID>
  {
  }

  public abstract class isPrimary : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contact.isPrimary>
  {
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.title>
  {
  }

  public abstract class firstName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.firstName>
  {
  }

  public abstract class midName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.midName>
  {
  }

  public abstract class lastName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.lastName>
  {
  }

  public abstract class salutation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.salutation>
  {
  }

  public abstract class attention : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.attention>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contact.bAccountID>
  {
  }

  public abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.fullName>
  {
  }

  public abstract class parentBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contact.parentBAccountID>
  {
  }

  public abstract class eMail : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.eMail>
  {
  }

  public abstract class webSite : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.webSite>
  {
  }

  public abstract class fax : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.fax>
  {
  }

  public abstract class faxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.faxType>
  {
  }

  public abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.phone1>
  {
  }

  public abstract class phone1Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.phone1Type>
  {
  }

  public abstract class phone2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.phone2>
  {
  }

  public abstract class phone2Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.phone2Type>
  {
  }

  public abstract class phone3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.phone3>
  {
  }

  public abstract class phone3Type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.phone3Type>
  {
  }

  public abstract class dateOfBirth : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Contact.dateOfBirth>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Contact.noteID>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contact.isActive>
  {
  }

  public abstract class isNotEmployee : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contact.isNotEmployee>
  {
  }

  public abstract class noFax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contact.noFax>
  {
  }

  public abstract class noMail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contact.noMail>
  {
  }

  public abstract class noMarketing : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contact.noMarketing>
  {
  }

  public abstract class noCall : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contact.noCall>
  {
  }

  public abstract class noEMail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contact.noEMail>
  {
  }

  public abstract class noMassMail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contact.noMassMail>
  {
  }

  public abstract class gender : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.gender>
  {
  }

  public abstract class maritalStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.maritalStatus>
  {
  }

  [Obsolete("The field is not used anymore")]
  public abstract class anniversary : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Contact.anniversary>
  {
  }

  public abstract class spouse : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.spouse>
  {
  }

  public abstract class img : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.img>
  {
  }

  public abstract class synchronize : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contact.synchronize>
  {
  }

  public abstract class contactType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.contactType>
  {
  }

  public abstract class contactPriority : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contact.contactPriority>
  {
  }

  public abstract class duplicateStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.duplicateStatus>
  {
  }

  public abstract class duplicateFound : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contact.duplicateFound>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.status>
  {
  }

  public abstract class resolution : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.resolution>
  {
  }

  public abstract class assignDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Contact.assignDate>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.classID>
  {
  }

  public abstract class source : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.source>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contact.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contact.ownerID>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Contact.userID>
  {
  }

  public abstract class campaignID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.campaignID>
  {
  }

  public abstract class method : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.method>
  {
  }

  [Obsolete("The field is not used anymore.")]
  public abstract class isConvertable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contact.isConvertable>
  {
  }

  public abstract class grammValidationDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Contact.grammValidationDateTime>
  {
  }

  public abstract class consentAgreement : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contact.consentAgreement>
  {
  }

  public abstract class consentDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Contact.consentDate>
  {
  }

  public abstract class consentExpirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Contact.consentExpirationDate>
  {
  }

  public abstract class attributes : BqlType<IBqlAttributes, string[]>.Field<Contact.attributes>
  {
  }

  public abstract class overrideSalesTerritory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Contact.overrideSalesTerritory>
  {
  }

  public abstract class salesTerritoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Contact.salesTerritoryID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Contact.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Contact.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Contact.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Contact.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Contact.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Contact.lastModifiedDateTime>
  {
  }

  [Obsolete("The field is not used anymore")]
  public abstract class searchSuggestion : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Contact.searchSuggestion>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.extRefNbr>
  {
  }

  public abstract class languageID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact.languageID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Contact.Tstamp>
  {
  }

  public abstract class deletedDatabaseRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Contact.deletedDatabaseRecord>
  {
  }
}
