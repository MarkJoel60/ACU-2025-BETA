// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.DAC.Standalone.CRCampaign
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.CR.Workflows;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.CR.DAC.Standalone;

/// <exclude />
[PXCacheName("Campaign Statistics")]
public class CRCampaign : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _CampaignID;
  protected string _CampaignName;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [AutoNumber(typeof (CRSetup.campaignNumberingID), typeof (AccessInfo.businessDate))]
  [PXSelector(typeof (CRCampaign.campaignID), DescriptionField = typeof (CRCampaign.campaignName))]
  [PXFieldDescription]
  public virtual string CampaignID
  {
    get => this._CampaignID;
    set => this._CampaignID = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault("")]
  [PXUIField]
  [PXNavigateSelector(typeof (CRCampaign.campaignName))]
  [PXFieldDescription]
  public virtual string CampaignName
  {
    get => this._CampaignName;
    set => this._CampaignName = value;
  }

  [PXInt]
  [PXUIField(DisplayName = "Leads Generated", Enabled = false)]
  [PXDBScalar(typeof (Search4<Contact.contactID, Where<Contact.campaignID, Equal<CRCampaign.campaignID>, And<Contact.contactType, Equal<ContactTypesAttribute.lead>>>, Aggregate<Count<Contact.contactID>>>))]
  public virtual int? LeadsGenerated { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Leads Converted", Enabled = false)]
  [PXDBScalar(typeof (Search4<CRLead.contactID, Where<CRLead.campaignID, Equal<CRCampaign.campaignID>, And<CRLead.status, Equal<LeadWorkflow.States.converted>>>, Aggregate<Count<CRLead.contactID>>>))]
  public virtual int? LeadsConverted { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Total Members", Enabled = false)]
  [PXDBScalar(typeof (Search5<CRCampaignMembers.contactID, InnerJoin<Contact, On<Contact.contactID, Equal<CRCampaignMembers.contactID>>>, Where<CRCampaignMembers.campaignID, Equal<CRCampaign.campaignID>>, Aggregate<Count<CRCampaignMembers.contactID>>>))]
  public virtual int? Contacts { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Members Contacted", Enabled = false)]
  [PXDBScalar(typeof (Search4<CRActivity.contactID, Where<CRActivity.outgoing, Equal<True>, And<CRActivity.documentNoteID, Equal<CRCampaign.noteID>, And<Where<Not<CRActivity.type, IsNull, And<CRActivity.classID, Equal<CRActivityClass.task>>>>>>>, Aggregate<Count<CRActivity.contactID>>>))]
  public virtual int? MembersContacted { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Members Responded", Enabled = false)]
  [PXDBScalar(typeof (Search4<CRActivity.contactID, Where<CRActivity.incoming, Equal<True>, And<CRActivity.documentNoteID, Equal<CRCampaign.noteID>, And<Where<Not<CRActivity.type, IsNull, And<CRActivity.classID, Equal<CRActivityClass.task>>>>>>>, Aggregate<Count<CRActivity.contactID>>>))]
  public virtual int? MembersResponded { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Opportunities", Enabled = false)]
  [PXDBScalar(typeof (Search4<CROpportunity.opportunityID, Where<CROpportunity.campaignSourceID, Equal<CRCampaign.campaignID>>, Aggregate<Count<CROpportunity.opportunityID>>>))]
  public virtual int? Opportunities { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Won Opportunities", Enabled = false)]
  [PXDBScalar(typeof (Search4<CROpportunity.opportunityID, Where<CROpportunity.campaignSourceID, Equal<CRCampaign.campaignID>, And<CROpportunity.status, Equal<OpportunityStatus.won>>>, Aggregate<Count<CROpportunity.opportunityID>>>))]
  public virtual int? ClosedOpportunities { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Opportunities Value", Enabled = false)]
  [PXDBScalar(typeof (Search4<CROpportunity.productsAmount, Where<CROpportunity.campaignSourceID, Equal<CRCampaign.campaignID>>, Aggregate<Sum<CROpportunity.amount, Sum<CROpportunity.productsAmount, Sum<CROpportunity.discTot>>>>>))]
  public virtual Decimal? OpportunitiesValue { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Won Opportunities Value", Enabled = false)]
  [PXDBScalar(typeof (Search4<CROpportunity.productsAmount, Where<CROpportunity.campaignSourceID, Equal<CRCampaign.campaignID>, And<CROpportunity.status, Equal<OpportunityStatus.won>>>, Aggregate<Sum<CROpportunity.amount, Sum<CROpportunity.productsAmount, Sum<CROpportunity.discTot>>>>>))]
  public virtual Decimal? ClosedOpportunitiesValue { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Generated Leads Converted to Won Opportunities", Enabled = false)]
  [PXDBScalar(typeof (Search5<Contact.contactID, InnerJoin<CROpportunity, On<CROpportunity.leadID, Equal<Contact.noteID>, And<CROpportunity.status, Equal<OpportunityStatus.won>>>>, Where<Contact.campaignID, Equal<CRCampaign.campaignID>>, Aggregate<Count<Contact.contactID>>>))]
  public virtual int? LeadsGeneratedClosedOpportunities { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRCampaign.selected>
  {
  }

  public abstract class campaignID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCampaign.campaignID>
  {
  }

  public abstract class campaignName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCampaign.campaignName>
  {
  }

  public abstract class leadsGenerated : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCampaign.leadsGenerated>
  {
  }

  public abstract class leadsConverted : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCampaign.leadsConverted>
  {
  }

  public abstract class contacts : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCampaign.contacts>
  {
  }

  public abstract class membersContacted : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCampaign.membersContacted>
  {
  }

  public abstract class membersResponded : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCampaign.membersResponded>
  {
  }

  public abstract class opportunities : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCampaign.opportunities>
  {
  }

  public abstract class closedOpportunities : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCampaign.closedOpportunities>
  {
  }

  public abstract class opportunitiesValue : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRCampaign.opportunitiesValue>
  {
  }

  public abstract class closedOpportunitiesValue : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRCampaign.closedOpportunitiesValue>
  {
  }

  public abstract class leadsGeneratedClosedOpportunities : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCampaign.leadsGeneratedClosedOpportunities>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRCampaign.noteID>
  {
  }
}
