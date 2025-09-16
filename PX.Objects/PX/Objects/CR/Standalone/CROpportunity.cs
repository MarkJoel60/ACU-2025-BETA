// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Standalone.CROpportunity
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.CR.Standalone;

/// <exclude />
public class CROpportunity : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const int OpportunityIDLength = 15;

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Selected { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [AutoNumber(typeof (CRSetup.opportunityNumberingID), typeof (AccessInfo.businessDate))]
  [PXSelector(typeof (Search3<CROpportunity.opportunityID, OrderBy<Desc<CROpportunity.opportunityID>>>), new System.Type[] {typeof (CROpportunity.opportunityID), typeof (CROpportunity.status), typeof (CROpportunity.closeDate), typeof (CROpportunity.stageID), typeof (CROpportunity.classID)}, Filterable = true)]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string OpportunityID { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Source Lead", Enabled = false)]
  [PXSelector(typeof (Search<Contact.noteID, Where<BqlOperand<Contact.contactType, IBqlString>.IsEqual<ContactTypesAttribute.lead>>>), DescriptionField = typeof (Contact.displayName))]
  public virtual Guid? LeadID { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Opportunity Class")]
  [PXDefault]
  [PXSelector(typeof (CROpportunityClass.cROpportunityClassID), DescriptionField = typeof (CROpportunityClass.description), CacheGlobal = true)]
  public virtual string ClassID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Subject { get; set; }

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Details")]
  public virtual string Details { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? CloseDate { get; set; }

  [PXDBDate(PreserveTime = true)]
  [PXUIField]
  public virtual DateTime? StageChangedDate { get; set; }

  [PXDBString(2)]
  [PXUIField(DisplayName = "Stage")]
  [CROpportunityStages(typeof (CROpportunity.classID), typeof (CROpportunity.stageChangedDate), OnlyActiveStages = true)]
  [PXDefault]
  public virtual string StageID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [OpportunityStatus.List]
  [PXDefault]
  public virtual string Status { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDBString(2, IsFixed = true)]
  [OpportunityReason.List]
  [PXUIField(DisplayName = "Reason")]
  public virtual string Resolution { get; set; }

  [PXDBDate(PreserveTime = true, InputMask = "g")]
  [PXUIField(DisplayName = "Assignment Date")]
  public virtual DateTime? AssignDate { get; set; }

  [PXDBDate(PreserveTime = true, InputMask = "g")]
  [PXUIField(DisplayName = "Actual Close Date")]
  public virtual DateTime? ClosingDate { get; set; }

  [PXNote(DescriptionField = typeof (CROpportunity.opportunityID), Selector = typeof (CROpportunity.opportunityID))]
  public virtual Guid? NoteID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [CRMSources]
  public virtual string Source { get; set; }

  [PXDBString(255 /*0xFF*/, IsFixed = true)]
  [PXUIField(DisplayName = "External Ref.")]
  public virtual string ExternalRef { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Date Created", Enabled = false)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified Date", Enabled = false)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBGuid(false)]
  [PXDefault]
  public virtual Guid? DefQuoteID { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunity.selected>
  {
  }

  public abstract class opportunityID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunity.opportunityID>
  {
  }

  public abstract class leadID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CROpportunity.leadID>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.classID>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.subject>
  {
  }

  public abstract class details : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.details>
  {
  }

  public abstract class closeDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CROpportunity.closeDate>
  {
  }

  public abstract class stageChangedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunity.stageChangedDate>
  {
  }

  public abstract class stageID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.stageID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.status>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunity.isActive>
  {
  }

  public abstract class resolution : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.resolution>
  {
  }

  public abstract class assignDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CROpportunity.assignDate>
  {
  }

  public abstract class closingDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunity.closingDate>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CROpportunity.noteID>
  {
  }

  public abstract class source : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.source>
  {
  }

  public abstract class externalRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.externalRef>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CROpportunity.Tstamp>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunity.createdByScreenID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CROpportunity.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunity.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CROpportunity.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunity.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunity.lastModifiedDateTime>
  {
  }

  public abstract class defQuoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CROpportunity.defQuoteID>
  {
  }
}
