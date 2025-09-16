// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Standalone.CRLead
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR.Workflows;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CR.Standalone;

/// <summary>
/// A standalone version of the <see cref="T:PX.Objects.CR.CRLead" /> class
/// used to save changes in the <tt>CRLead</tt> table.
/// Represents a marketing lead or a sales lead.</summary>
/// <remarks>A marketing lead is a person or a company
/// that has potential interest in a product your organization offers.
/// A sales lead is a person or a company that expresses interest in products your organization offers.
/// The <see cref="T:PX.Objects.CR.CRLead" /> records are created and edited on the Leads (CR301000) form,
/// which corresponds to the <see cref="T:PX.Objects.CR.LeadMaint" /> graph.
/// </remarks>
[PXCacheName("Lead")]
[PXPrimaryGraph(typeof (LeadMaint))]
[Serializable]
public class CRLead : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The identifier of the lead.
  /// This is the key field. At the same time, it is the identifier of the
  /// <see cref="T:PX.Objects.CR.Contact" /> object.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [PXDBInt(IsKey = true, BqlField = typeof (CRLead.contactID))]
  [PXUIField]
  [PXDBChildIdentity(typeof (CRLead.contactID))]
  public virtual int? ContactID { get; set; }

  /// <summary>The status of the lead.</summary>
  /// <value>
  /// The field values are controlled by the workflow engine.
  /// The possible default values of the field are listed in
  /// the <see cref="T:PX.Objects.CR.Workflows.LeadWorkflow.States" /> class.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [PXUIField]
  [LeadWorkflow.States.List]
  [PXUIEnabled(typeof (Where<EntryStatus, NotEqual<EntryStatus.inserted>>))]
  public virtual 
  #nullable disable
  string Status { get; set; }

  /// <summary>
  /// The reason why the <see cref="P:PX.Objects.CR.Standalone.CRLead.Status" /> field of this lead has been changed.
  /// </summary>
  /// <value>
  /// The field values are controlled by the workflow engine, and the field is not used by the application logic directly.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXUIField]
  [PXStringList(new string[] {}, new string[] {}, BqlTable = typeof (CRLead))]
  public virtual string Resolution { get; set; }

  /// <summary>The identifier of the class.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.CRLeadClass.ClassID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Lead Class")]
  [PXDefault(typeof (Search<CRSetup.defaultLeadClassID>))]
  [PXSelector(typeof (CRLeadClass.classID), DescriptionField = typeof (CRLeadClass.description), CacheGlobal = true)]
  public virtual string ClassID { get; set; }

  /// <summary>
  /// The identifier of the contact that is associated with this lead.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Contact")]
  [PXSelector(typeof (Search<Contact.contactID, Where<Contact.contactType, Equal<ContactTypesAttribute.person>, And<WhereEqualNotNull<Contact.bAccountID, Contact.bAccountID>>>>), DescriptionField = typeof (Contact.displayName), Filterable = true, DirtyRead = true)]
  [PXDBChildIdentity(typeof (Contact.contactID))]
  public virtual int? RefContactID { get; set; }

  /// <summary>
  /// Specifies whether the <see cref="T:PX.Objects.CR.Contact">contact</see>
  /// and <see cref="T:PX.Objects.CR.Address">address</see> information of this lead differs from
  /// the contact and address information
  /// of the <see cref="T:PX.Objects.CR.BAccount">business account</see> associated with this lead.
  /// </summary>
  /// <remarks>
  /// The behavior is controlled by the <see cref="T:PX.Objects.CR.LeadMaint.LeadBAccountSharedAddressOverrideGraphExt" />
  /// graph extension.
  /// </remarks>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? OverrideRefContact { get; set; }

  /// <summary>
  /// An alphanumeric string of up to 255 characters that describes the lead.
  /// This field is used to add any additional information about the lead.
  /// </summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description { get; set; }

  /// <summary>
  /// The date when the lead was converted to the <see cref="T:PX.Objects.CR.Standalone.CROpportunity">opportunity</see>.
  /// </summary>
  /// <value>
  /// The value is filled by the <see cref="T:PX.Objects.CR.LeadMaint.CreateOpportunityAllFromLeadGraphExt" />
  /// graph extension.
  /// </value>
  [PXDBDate(PreserveTime = true)]
  [PXUIField(DisplayName = "Qualification Date")]
  public virtual DateTime? QualificationDate { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.SM.Users">user</see> who converted the lead to the <see cref="T:PX.Objects.CR.Standalone.CROpportunity">opportunity</see>.
  /// </summary>
  /// <value>
  /// The value is filled by the <see cref="T:PX.Objects.CR.LeadMaint.CreateOpportunityAllFromLeadGraphExt" />
  /// graph extension.
  /// </value>
  [PXDBGuid(false)]
  [PXSelector(typeof (Users.pKID), SubstituteKey = typeof (Users.username), DescriptionField = typeof (Users.fullName), CacheGlobal = true, DirtyRead = true, ValidateValue = false)]
  [PXUIField(DisplayName = "Converted By")]
  public virtual Guid? ConvertedBy { get; set; }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRLead.contactID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRLead.status>
  {
  }

  public abstract class resolution : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRLead.resolution>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRLead.classID>
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
}
