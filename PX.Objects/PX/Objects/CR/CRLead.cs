// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRLead
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR.MassProcess;
using PX.Objects.CR.Workflows;
using PX.SM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <summary>Represents a marketing lead or a sales lead.</summary>
/// <remarks>
/// A marketing lead is a person or a company that has potential interest in a product your organization offers.
/// A sales lead is a person or a company that expresses interest in products your organization offers.
/// The records of this type are created and edited on the Leads (CR301000) form,
/// which corresponds to the <see cref="T:PX.Objects.CR.LeadMaint" /> graph.
/// Note that this class is a projection of the <see cref="T:PX.Objects.CR.Contact" /> and <see cref="T:PX.Objects.CR.Standalone.CRLead" /> classes.
/// </remarks>
[PXBreakInheritance]
[PXCacheName("Lead")]
[PXTable(new System.Type[] {typeof (Contact.contactID)})]
[CRCacheIndependentPrimaryGraph(typeof (LeadMaint), typeof (Select<CRLead, Where<CRLead.contactID, Equal<Current<CRLead.contactID>>>>))]
[PXGroupMask(typeof (LeftJoinSingleTable<BAccount, On<BAccount.bAccountID, Equal<CRLead.bAccountID>>>), WhereRestriction = typeof (Where<BAccount.bAccountID, IsNull, Or<Match<BAccount, Current<AccessInfo.userName>>>>))]
[Serializable]
public class CRLead : Contact
{
  [PXDBString(10, IsUnicode = true, BqlTable = typeof (CRLead))]
  [PXUIField(DisplayName = "Lead Class")]
  [PXDefault(typeof (Search<CRSetup.defaultLeadClassID>))]
  [PXSelector(typeof (CRLeadClass.classID), DescriptionField = typeof (CRLeadClass.description), CacheGlobal = true)]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXMassUpdatableField]
  public override 
  #nullable disable
  string ClassID { get; set; }

  [CRLeadSelector(typeof (FbqlSelect<SelectFromBase<CRLead, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<BAccount>.On<BqlOperand<BAccount.bAccountID, IBqlInt>.IsEqual<CRLead.bAccountID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.bAccountID, IsNull>>>>.Or<Match<BAccount, Current<AccessInfo.userName>>>>, CRLead>.SearchFor<CRLead.contactID>), new System.Type[] {typeof (CRLead.memberName), typeof (CRLead.fullName), typeof (CRLead.salutation), typeof (Contact.eMail), typeof (Contact.phone1), typeof (CRLead.status), typeof (CRLead.duplicateStatus)}, Headers = new string[] {"Contact", "Account Name", "Job Title", "Email", "Phone 1", "Status", "Duplicate"}, DescriptionField = typeof (CRLead.memberName), Filterable = true)]
  [PXDBIdentity(IsKey = true)]
  [PXUIField]
  [PXPersonalDataWarning]
  [LeadLastNameOrCompanyNameRequired]
  public override int? ContactID { get; set; }

  /// <summary>The type of the lead.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.ContactTypesAttribute" /> class.
  /// The default value is <see cref="F:PX.Objects.CR.ContactTypesAttribute.Lead" />.
  /// This field must be specified at the initialization stage and not be changed afterwards.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXDefault("LD")]
  [ContactTypes]
  [PXUIField]
  public override string ContactType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Contact.BAccountID" />
  [CRContactBAccountDefault]
  [PXFormula(typeof (Default<CRLead.refContactID>))]
  [PXDefault(typeof (IIf<Where<CRLead.refContactID, IsNotNull>, Selector<CRLead.refContactID, Contact.bAccountID>, CRLead.bAccountID>))]
  [CRMBAccount(new System.Type[] {typeof (BAccountType.prospectType), typeof (BAccountType.customerType), typeof (BAccountType.combinedType), typeof (BAccountType.vendorType)}, null, null, null)]
  [PXMassUpdatableField]
  public override int? BAccountID { get; set; }

  /// <summary>The name of the company the contact works for.</summary>
  /// <value>
  /// Either this field or the <see cref="P:PX.Objects.CR.Contact.LastName" /> field must be specified to create the lead.
  /// </value>
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  [PXPersonalDataField]
  [PXContactInfoField]
  public override string FullName { get; set; }

  /// <summary>The source of the lead.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.CRMSourcesAttribute" /> class.
  /// The value of the field is automatically changed when the <see cref="P:PX.Objects.CR.CRLead.ClassID" /> property is changed.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Source")]
  [CRMSources(BqlTable = typeof (CRLead))]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXFormula(typeof (BqlOperand<Selector<CRLead.classID, CRLeadClass.defaultSource>, IBqlString>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<False, Equal<Use<IsImport>.AsBool>>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EntryStatus, Equal<EntryStatus.inserted>>>>>.Or<BqlOperand<Current<CRLead.source>, IBqlString>.IsNull>>>>.Else<CRLead.source>))]
  public override string Source { get; set; }

  /// <inheritdoc />
  [Owner(typeof (CRLead.workgroupID))]
  [PXMassUpdatableField]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  public override int? OwnerID { get; set; }

  /// <inheritdoc />
  [PXSearchable(1024 /*0x0400*/, "{0}: {1}", new System.Type[] {typeof (CRLead.contactType), typeof (CRLead.displayName)}, new System.Type[] {typeof (CRLead.fullName), typeof (Contact.eMail), typeof (Contact.phone1), typeof (Contact.phone2), typeof (Contact.phone3), typeof (Contact.webSite)}, WhereConstraint = typeof (Where<BqlOperand<CRLead.contactType, IBqlString>.IsNotIn<ContactTypesAttribute.bAccountProperty, ContactTypesAttribute.employee>>), MatchWithJoin = typeof (LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRLead.bAccountID>>>), Line1Format = "{0}{1}{2}{3}", Line1Fields = new System.Type[] {typeof (CRLead.fullName), typeof (CRLead.salutation), typeof (Contact.phone1), typeof (Contact.eMail)}, Line2Format = "{1}{2}{3}", Line2Fields = new System.Type[] {typeof (CRLead.defAddressID), typeof (PX.Objects.CR.Address.displayName), typeof (PX.Objects.CR.Address.city), typeof (PX.Objects.CR.Address.state), typeof (PX.Objects.CR.Address.countryID)})]
  [PXUniqueNote(DescriptionField = typeof (CRLead.memberName), Selector = typeof (CRLead.contactID), ShowInReferenceSelector = true)]
  [PXUIField]
  public override Guid? NoteID { get; set; }

  /// <inheritdoc />
  [PXUIField]
  [PXDependsOnFields(new System.Type[] {typeof (Contact.lastName), typeof (Contact.firstName), typeof (Contact.midName), typeof (Contact.title)})]
  [PersonDisplayName(typeof (Contact.lastName), typeof (Contact.firstName), typeof (Contact.midName), typeof (Contact.title))]
  [PXDefault]
  [PXNavigateSelector(typeof (Search2<Contact.displayName, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.contactID>>>, Where2<Where<Contact.contactType, Equal<ContactTypesAttribute.lead>, Or<Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Contact.contactType, Equal<ContactTypesAttribute.employee>>>>, And<Where<BAccount.bAccountID, IsNull, Or<Match<BAccount, Current<AccessInfo.userName>>>>>>>))]
  [PXPersonalDataField]
  [PXContactInfoField]
  public override string DisplayName { get; set; }

  /// <inheritdoc />
  [PXDBCalced(typeof (Switch<Case<Where<Contact.displayName, Equal<Empty>>, Contact.fullName>, Contact.displayName>), typeof (string))]
  [PXUIField]
  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXFieldDescription]
  public override string MemberName { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CRLead.Status" />
  [PXDBString(1, IsFixed = true, BqlTable = typeof (CRLead))]
  [PXDefault]
  [PXUIField]
  [LeadWorkflow.States.List(BqlTable = typeof (CRLead))]
  [PXUIEnabled(typeof (Where<EntryStatus, NotEqual<EntryStatus.inserted>, Or<IsImport, Equal<True>>>))]
  public override string Status { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CRLead.Resolution" />
  [PXDBString(2, IsFixed = true, BqlTable = typeof (CRLead))]
  [PXUIField]
  [PXStringList(new string[] {}, new string[] {}, BqlTable = typeof (CRLead))]
  public override string Resolution { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CRLead.RefContactID" />
  [ContactRaw(typeof (CRLead.bAccountID))]
  [PXDBChildIdentity(typeof (Contact.contactID))]
  public virtual int? RefContactID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CRLead.OverrideRefContact" />
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? OverrideRefContact { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CRLead.Description" />
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CRLead.QualificationDate" />
  [PXDBDate(PreserveTime = true)]
  [PXUIField(DisplayName = "Qualification Date")]
  public virtual DateTime? QualificationDate { get; set; }

  /// <exclude />
  [PXDBGuid(false)]
  [PXSelector(typeof (Users.pKID), SubstituteKey = typeof (Users.username), DescriptionField = typeof (Users.fullName), CacheGlobal = true, DirtyRead = true, ValidateValue = false)]
  [PXUIField(DisplayName = "Converted By")]
  public virtual Guid? ConvertedBy { get; set; }

  /// <summary>
  /// The attributes available for the current contact.
  /// The field is preserved for internal use.
  /// </summary>
  [CRAttributesField(typeof (CRLead.classID), typeof (Contact.noteID))]
  public override string[] Attributes { get; set; }

  public new class PK : PrimaryKeyOf<CRLead>.By<CRLead.contactID>
  {
    public static CRLead Find(PXGraph graph, int? contactID, PKFindOptions options = 0)
    {
      return (CRLead) PrimaryKeyOf<CRLead>.By<CRLead.contactID>.FindBy(graph, (object) contactID, options);
    }
  }

  public new static class FK
  {
    public class Class : 
      PrimaryKeyOf<CRLeadClass>.By<CRLeadClass.classID>.ForeignKeyOf<CRLead>.By<CRLead.classID>
    {
    }

    public class Contact : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CRLead>.By<CRLead.refContactID>
    {
    }

    public class BusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<CRLead>.By<CRLead.bAccountID>
    {
    }

    public class ParentBusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<CRLead>.By<CRLead.parentBAccountID>
    {
    }

    public class Address : 
      PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<CRLead>.By<CRLead.defAddressID>
    {
    }

    public class Owner : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CRLead>.By<CRLead.ownerID>
    {
    }

    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<CRLead>.By<CRLead.workgroupID>
    {
    }

    public class SalesTerritory : 
      PrimaryKeyOf<PX.Objects.CS.SalesTerritory>.By<PX.Objects.CS.SalesTerritory.salesTerritoryID>.ForeignKeyOf<CRLead>.By<Contact.salesTerritoryID>
    {
    }
  }

  public new abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRLead.classID>
  {
  }

  public new abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRLead.contactID>
  {
  }

  public new abstract class contactType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRLead.contactType>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRLead.bAccountID>
  {
  }

  public new abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRLead.fullName>
  {
  }

  public new abstract class source : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRLead.source>
  {
  }

  public new abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRLead.ownerID>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRLead.noteID>
  {
  }

  public new abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRLead.isActive>
  {
  }

  public new abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRLead.workgroupID>
  {
  }

  public new abstract class lastName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRLead.lastName>
  {
  }

  public new abstract class displayName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRLead.displayName>
  {
  }

  public new abstract class memberName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRLead.memberName>
  {
  }

  public new abstract class overrideAddress : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRLead.overrideAddress>
  {
  }

  public new abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRLead.defAddressID>
  {
  }

  public new abstract class duplicateFound : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRLead.duplicateFound>
  {
  }

  public new abstract class duplicateStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRLead.duplicateStatus>
  {
  }

  public new abstract class campaignID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRLead.campaignID>
  {
  }

  public new abstract class parentBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRLead.parentBAccountID>
  {
  }

  public new abstract class salutation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRLead.salutation>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRLead.createdDateTime>
  {
  }

  public new abstract class overrideSalesTerritory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRLead.overrideSalesTerritory>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRLead.status>
  {
  }

  public new abstract class resolution : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRLead.resolution>
  {
  }

  public abstract class refContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRLead.refContactID>
  {
  }

  public abstract class overrideRefContact : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRLead.overrideRefContact>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRLead.description>
  {
  }

  public abstract class qualificationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRLead.qualificationDate>
  {
  }

  public abstract class convertedBy : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRLead.convertedBy>
  {
  }

  public new abstract class attributes : BqlType<IBqlAttributes, string[]>.Field<CRLead.attributes>
  {
  }
}
