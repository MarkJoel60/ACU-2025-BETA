// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR.Extensions.CRDuplicateEntities;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXPrimaryGraph(typeof (CRSetupMaint))]
[PXCacheName("Customer Management Preferences")]
[Serializable]
public class CRSetup : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  PXNoteAttribute.IPXCopySettings
{
  protected 
  #nullable disable
  string _CampaignNumberingID;
  protected string _OpportunityNumberingID;
  protected string _QuoteNumberingID;
  protected string _CaseNumberingID;
  protected string _MassMailNumberingID;
  protected string _DefaultCaseClassID;
  protected string _DefaultOpportunityClassID;
  protected string _DefaultRateTypeID;
  protected bool? _AllowOverrideRate;
  protected string _DefaultCustomerClassID;
  protected bool? _CopyNotes;
  protected bool? _CopyFiles;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("CAMPAIGN")]
  [PXUIField(DisplayName = "Campaign Numbering Sequence")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string CampaignNumberingID
  {
    get => this._CampaignNumberingID;
    set => this._CampaignNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("OPPORTUNTY")]
  [PXUIField(DisplayName = "Opportunity Numbering Sequence")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string OpportunityNumberingID
  {
    get => this._OpportunityNumberingID;
    set => this._OpportunityNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("CRQUOTE")]
  [PXUIField(DisplayName = "Quote Numbering Sequence")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string QuoteNumberingID
  {
    get => this._QuoteNumberingID;
    set => this._QuoteNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault("CASE")]
  [PXUIField(DisplayName = "Case Numbering Sequence")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string CaseNumberingID
  {
    get => this._CaseNumberingID;
    set => this._CaseNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault("MMAIL")]
  [PXUIField(DisplayName = "Mass Mail Numbering Sequence")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string MassMailNumberingID
  {
    get => this._MassMailNumberingID;
    set => this._MassMailNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Default Case Class")]
  [PXSelector(typeof (CRCaseClass.caseClassID), DescriptionField = typeof (CRCaseClass.description), CacheGlobal = true)]
  public virtual string DefaultCaseClassID
  {
    get => this._DefaultCaseClassID;
    set => this._DefaultCaseClassID = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Default Opportunity Class")]
  [PXSelector(typeof (CROpportunityClass.cROpportunityClassID), DescriptionField = typeof (CROpportunityClass.description), CacheGlobal = true)]
  public virtual string DefaultOpportunityClassID
  {
    get => this._DefaultOpportunityClassID;
    set => this._DefaultOpportunityClassID = value;
  }

  [PXDBString(6, IsUnicode = true)]
  [PXSelector(typeof (PX.Objects.CM.CurrencyRateType.curyRateTypeID), DescriptionField = typeof (PX.Objects.CM.CurrencyRateType.descr))]
  [PXForeignReference(typeof (Field<CRSetup.defaultRateTypeID>.IsRelatedTo<PX.Objects.CM.CurrencyRateType.curyRateTypeID>))]
  [PXUIField(DisplayName = "Default Rate Type ")]
  public virtual string DefaultRateTypeID
  {
    get => this._DefaultRateTypeID;
    set => this._DefaultRateTypeID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Rate Override")]
  public virtual bool? AllowOverrideRate
  {
    get => this._AllowOverrideRate;
    set => this._AllowOverrideRate = value;
  }

  [AssignmentMap(typeof (AssignmentMapType.AssignmentMapTypeLead), DisplayName = "Lead Assignment Map")]
  public virtual int? LeadDefaultAssignmentMapID { get; set; }

  [AssignmentMap(typeof (AssignmentMapType.AssignmentMapTypeContact), DisplayName = "Contact Assignment Map")]
  public virtual int? ContactDefaultAssignmentMapID { get; set; }

  [AssignmentMap(typeof (AssignmentMapType.AssignmentMapTypeCase), DisplayName = "Case Assignment Map")]
  public virtual int? DefaultCaseAssignmentMapID { get; set; }

  [AssignmentMap(typeof (AssignmentMapType.AssignmentMapTypeProspect), DisplayName = "Business Account Assignment Map")]
  public virtual int? DefaultBAccountAssignmentMapID { get; set; }

  [AssignmentMap(typeof (AssignmentMapType.AssignmentMapTypeOpportunity), DisplayName = "Opportunity Assignment Map")]
  public virtual int? DefaultOpportunityAssignmentMapID { get; set; }

  [ApprovalMap(typeof (AssignmentMapType.AssignmentMapTypeQuotes))]
  public virtual int? QuoteApprovalMapID { get; set; }

  [EmailNotification(DisplayName = "Pending Approval Notification")]
  public virtual int? QuoteApprovalNotificationID { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Default Lead Class")]
  [PXSelector(typeof (CRLeadClass.classID), DescriptionField = typeof (CRLeadClass.description), CacheGlobal = true)]
  public virtual string DefaultLeadClassID { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Default Contact Class")]
  [PXSelector(typeof (CRContactClass.classID), DescriptionField = typeof (CRContactClass.description), CacheGlobal = true)]
  public virtual string DefaultContactClassID { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Default Business Account Class")]
  [PXSelector(typeof (CRCustomerClass.cRCustomerClassID), DescriptionField = typeof (CRCustomerClass.description), CacheGlobal = true)]
  public virtual string DefaultCustomerClassID
  {
    get => this._DefaultCustomerClassID;
    set => this._DefaultCustomerClassID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Copy Notes")]
  public virtual bool? CopyNotes
  {
    get => this._CopyNotes;
    set => this._CopyNotes = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Copy Attachments")]
  public virtual bool? CopyFiles
  {
    get => this._CopyFiles;
    set => this._CopyFiles = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Normalize Validation Scores", FieldClass = "DUPLICATE")]
  public virtual bool? DuplicateScoresNormalization { get; set; }

  /// <summary>
  /// Delimiters used for option: <see cref="F:PX.Objects.CR.TransformationRulesAttribute.SplitWords" /> during grams calculation.
  /// For delimiters different from default
  /// (<see cref="F:PX.Objects.CR.Extensions.CRDuplicateEntities.CRCharsDelimitersAttribute.DefaultDelimiters" />
  /// attach custom <see cref="T:PX.Objects.CR.Extensions.CRDuplicateEntities.CRCharsDelimitersAttribute" /> with required pattern.
  /// For instance, " ,:^" will use 4 delimiters: ' ', ',', ':', '^'.
  /// </summary>
  [PXString]
  [CRCharsDelimiters]
  [PXUIField(DisplayName = "Delimiters", FieldClass = "DUPLICATE")]
  public virtual string DuplicateCharsDelimiters { get; set; }

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

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public abstract class campaignNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSetup.campaignNumberingID>
  {
  }

  public abstract class opportunityNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSetup.opportunityNumberingID>
  {
  }

  public abstract class quoteNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSetup.quoteNumberingID>
  {
  }

  public abstract class caseNumberingID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRSetup.caseNumberingID>
  {
  }

  public abstract class massMailNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSetup.massMailNumberingID>
  {
  }

  public abstract class defaultCaseClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSetup.defaultCaseClassID>
  {
  }

  public abstract class defaultOpportunityClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSetup.defaultOpportunityClassID>
  {
  }

  public abstract class defaultRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSetup.defaultRateTypeID>
  {
  }

  public abstract class allowOverrideRate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRSetup.allowOverrideRate>
  {
  }

  public abstract class leaddefaultAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRSetup.leaddefaultAssignmentMapID>
  {
  }

  public abstract class contactdefaultAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRSetup.contactdefaultAssignmentMapID>
  {
  }

  public abstract class defaultCaseAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRSetup.defaultCaseAssignmentMapID>
  {
  }

  public abstract class defaultBAccountAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRSetup.defaultBAccountAssignmentMapID>
  {
  }

  public abstract class defaultOpportunityAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRSetup.defaultOpportunityAssignmentMapID>
  {
  }

  public abstract class quoteApprovalMapID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRSetup.quoteApprovalMapID>
  {
  }

  public abstract class quoteApprovalNotificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRSetup.quoteApprovalNotificationID>
  {
  }

  public abstract class defaultLeadClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSetup.defaultLeadClassID>
  {
  }

  public abstract class defaultContactClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSetup.defaultContactClassID>
  {
  }

  public abstract class defaultCustomerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSetup.defaultCustomerClassID>
  {
  }

  public abstract class copyNotes : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRSetup.copyNotes>
  {
  }

  public abstract class copyFiles : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRSetup.copyFiles>
  {
  }

  public abstract class duplicateScoresNormalization : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRSetup.duplicateScoresNormalization>
  {
  }

  public abstract class duplicateCharsDelimiters : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSetup.duplicateCharsDelimiters>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRSetup.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRSetup.Tstamp>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRSetup.noteID>
  {
  }
}
