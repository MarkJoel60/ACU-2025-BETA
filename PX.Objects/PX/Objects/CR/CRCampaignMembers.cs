// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCampaignMembers
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
[PXCacheName("Campaign Members")]
[Serializable]
public class CRCampaignMembers : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  byte[] _tstamp;
  protected string _CreatedByScreenID;
  protected Guid? _CreatedByID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDBDefault(typeof (CRCampaign.campaignID))]
  [PXUIField(DisplayName = "Campaign ID")]
  [PXParent(typeof (Select<CRCampaign, Where<CRCampaign.campaignID, Equal<Current<CRCampaignMembers.campaignID>>>>))]
  [PXSelector(typeof (Search<CRCampaign.campaignID, Where<CRCampaign.isActive, Equal<True>>>))]
  public virtual string CampaignID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Member Name")]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<BAccount>.On<BqlOperand<BAccount.bAccountID, IBqlInt>.IsEqual<Contact.bAccountID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.bAccountID, IsNull>>>>.Or<Brackets<BqlChainableConditionLite<MatchUserFor<BAccount>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.contactType, NotEqual<ContactTypesAttribute.bAccountProperty>>>>>.Or<BqlOperand<BAccount.defContactID, IBqlInt>.IsEqual<Contact.contactID>>>>>>>, Contact>.SearchFor<Contact.contactID>), new System.Type[] {typeof (Contact.contactType), typeof (Contact.memberName), typeof (Contact.salutation), typeof (Contact.bAccountID), typeof (Contact.eMail), typeof (Contact.phone1), typeof (Contact.isActive)})]
  [PXParent(typeof (Select<Contact, Where<Contact.contactID, Equal<Current<CRCampaignMembers.contactID>>>>))]
  public virtual int? ContactID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Opportunities Created", Enabled = false)]
  [PXDBScalar(typeof (Search5<CROpportunity.noteID, InnerJoin<Contact, On<True, Equal<True>>, LeftJoin<BAccount, On<BAccount.defContactID, Equal<Contact.contactID>>, InnerJoin<CRCampaign, On<CROpportunity.campaignSourceID, Equal<CRCampaign.campaignID>>>>>, Where<CRCampaign.campaignID, Equal<CRCampaignMembers.campaignID>, And<Contact.contactID, Equal<CRCampaignMembers.contactID>, And<Where<CROpportunity.contactID, Equal<CRCampaignMembers.contactID>, And<Contact.contactType, NotEqual<ContactTypesAttribute.lead>, Or<CROpportunity.leadID, Equal<Contact.noteID>, And<Contact.contactType, Equal<ContactTypesAttribute.lead>, Or<CROpportunity.bAccountID, Equal<BAccount.bAccountID>, And<Contact.contactType, Equal<ContactTypesAttribute.bAccountProperty>>>>>>>>>>, Aggregate<Count>>))]
  public virtual int? OpportunityCreatedCount { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Incoming Activities Logged", Enabled = false)]
  [PXDBScalar(typeof (Search5<CRActivity.noteID, InnerJoin<CRCampaign, On<CRActivity.documentNoteID, Equal<CRCampaign.noteID>>, InnerJoin<Contact, On<BqlOperand<True, IBqlBool>.IsEqual<True>>, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRActivity.bAccountID>>, LeftJoin<Contact2, On<Contact2.contactID, Equal<BAccount.defContactID>>>>>>, Where<CRCampaign.campaignID, Equal<CRCampaignMembers.campaignID>, And2<Where<CRActivity.contactID, Equal<CRCampaignMembers.contactID>, And<Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Contact2.contactID, Equal<CRCampaignMembers.contactID>, And<Contact2.contactType, Equal<ContactTypesAttribute.bAccountProperty>, Or<CRActivity.refNoteID, Equal<Contact.noteID>, And<Contact.contactType, Equal<ContactTypesAttribute.lead>>>>>>>, And<Contact.contactID, Equal<CRCampaignMembers.contactID>, And<CRActivity.incoming, Equal<True>, And<Where<Not<CRActivity.type, IsNull, And<CRActivity.classID, Equal<CRActivityClass.task>>>>>>>>>, Aggregate<Count>>))]
  public virtual int? IncomingActivitiesLogged { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Outgoing Activities Logged", Enabled = false)]
  [PXDBScalar(typeof (Search5<CRActivity.noteID, InnerJoin<CRCampaign, On<CRActivity.documentNoteID, Equal<CRCampaign.noteID>>, InnerJoin<Contact, On<BqlOperand<True, IBqlBool>.IsEqual<True>>, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRActivity.bAccountID>>, LeftJoin<Contact2, On<Contact2.contactID, Equal<BAccount.defContactID>>>>>>, Where<CRCampaign.campaignID, Equal<CRCampaignMembers.campaignID>, And2<Where<CRActivity.contactID, Equal<CRCampaignMembers.contactID>, And<Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Contact2.contactID, Equal<CRCampaignMembers.contactID>, And<Contact2.contactType, Equal<ContactTypesAttribute.bAccountProperty>, Or<CRActivity.refNoteID, Equal<Contact.noteID>, And<Contact.contactType, Equal<ContactTypesAttribute.lead>>>>>>>, And<Contact.contactID, Equal<CRCampaignMembers.contactID>, And<CRActivity.outgoing, Equal<True>, And<Where<Not<CRActivity.type, IsNull, And<CRActivity.classID, Equal<CRActivityClass.task>>>>>>>>>, Aggregate<Count>>))]
  public virtual int? OutgoingActivitiesLogged { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Activities Logged", Enabled = false)]
  [PXDBScalar(typeof (Search5<CRActivity.noteID, InnerJoin<Contact, On<True, Equal<True>>, InnerJoin<CRCampaign, On<True, Equal<True>>>>, Where<CRCampaign.campaignID, Equal<CRCampaignMembers.campaignID>, And<Contact.contactID, Equal<CRCampaignMembers.contactID>, And<CRActivity.documentNoteID, Equal<CRCampaign.noteID>, And2<Where<CRActivity.contactID, Equal<CRCampaignMembers.contactID>, And<Contact.contactType, NotEqual<ContactTypesAttribute.lead>, Or<CRActivity.refNoteID, Equal<Contact.noteID>, And<Contact.contactType, Equal<ContactTypesAttribute.lead>>>>>, And<CRActivity.classID, NotEqual<CRActivityClass.email>, And<CRActivity.classID, NotEqual<CRActivityClass.task>>>>>>>, Aggregate<Count>>))]
  public virtual int? ActivitiesLogged { get; set; }

  [PXDBScalar(typeof (Search5<CRActivity.noteID, InnerJoin<Contact, On<True, Equal<True>>, InnerJoin<CRCampaign, On<True, Equal<True>>>>, Where<CRCampaign.campaignID, Equal<CRCampaignMembers.campaignID>, And<Contact.contactID, Equal<CRCampaignMembers.contactID>, And<CRActivity.documentNoteID, Equal<CRCampaign.noteID>, And2<Where<CRActivity.contactID, Equal<CRCampaignMembers.contactID>, And<Contact.contactType, NotEqual<ContactTypesAttribute.lead>, Or<CRActivity.refNoteID, Equal<Contact.noteID>, And<Contact.contactType, Equal<ContactTypesAttribute.lead>>>>>, And<CRActivity.classID, Equal<CRActivityClass.email>>>>>>, Aggregate<Count>>))]
  [PXInt]
  [PXUIField(DisplayName = "Emails Sent", Enabled = false)]
  public virtual int? EmailSendCount { get; set; }

  /// <summary>
  /// A reference to the <see cref="T:PX.Objects.CR.CRMarketingList.marketingListID" /> from which the member has been added.
  /// </summary>
  [PXDBInt]
  [PXUIField(Visible = false)]
  public virtual int? MarketingListID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
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

  public class PK : 
    PrimaryKeyOf<CRCampaignMembers>.By<CRCampaignMembers.campaignID, CRCampaignMembers.contactID>
  {
    public static CRCampaignMembers Find(
      PXGraph graph,
      string campaignID,
      int? contactID,
      PKFindOptions options = 0)
    {
      return (CRCampaignMembers) PrimaryKeyOf<CRCampaignMembers>.By<CRCampaignMembers.campaignID, CRCampaignMembers.contactID>.FindBy(graph, (object) campaignID, (object) contactID, options);
    }
  }

  public static class FK
  {
    public class Campaign : 
      PrimaryKeyOf<CRCampaign>.By<CRCampaign.campaignID>.ForeignKeyOf<CRCampaignMembers>.By<CRCampaignMembers.campaignID>
    {
    }

    public class MarketingList : 
      PrimaryKeyOf<CRMarketingList>.By<CRMarketingList.marketingListID>.ForeignKeyOf<CRCampaignMembers>.By<CRCampaignMembers.marketingListID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRCampaignMembers.selected>
  {
  }

  public abstract class campaignID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCampaignMembers.campaignID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCampaignMembers.contactID>
  {
  }

  public abstract class opportunityCreatedCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCampaignMembers.opportunityCreatedCount>
  {
  }

  public abstract class incomingActivitiesLogged : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCampaignMembers.incomingActivitiesLogged>
  {
  }

  public abstract class outgoingActivitiesLogged : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCampaignMembers.outgoingActivitiesLogged>
  {
  }

  public abstract class activitiesLogged : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCampaignMembers.activitiesLogged>
  {
  }

  public abstract class emailSendCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCampaignMembers.emailSendCount>
  {
  }

  public abstract class marketingListID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCampaignMembers.marketingListID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRCampaignMembers.Tstamp>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCampaignMembers.createdByScreenID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRCampaignMembers.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCampaignMembers.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRCampaignMembers.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCampaignMembers.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCampaignMembers.lastModifiedDateTime>
  {
  }
}
