// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCampaign
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.Search;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.PM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXCacheName("Campaign")]
[PXPrimaryGraph(typeof (CampaignMaint))]
[PXCopyPasteHiddenFields(new System.Type[] {typeof (CRCampaign.projectID), typeof (CRCampaign.projectTaskID)})]
[Serializable]
public class CRCampaign : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, INotable
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _CampaignID;
  protected string _CampaignName;
  protected string _Description;
  private string _plainText;
  protected string _CampaignType;
  protected string _Status;
  protected bool? _IsActive;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected Decimal? _ExpectedRevenue;
  protected Decimal? _PlannedBudget;
  protected int? _ExpectedResponse;
  protected int? _MailsSent;
  protected string _PromoCodeID;
  protected string _SendFilter = "N";
  protected byte[] _tstamp;
  protected DateTime? _CreatedDateTime;
  protected string _CreatedByScreenID;
  protected Guid? _CreatedByID;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected Guid? _NoteID;

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

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set
    {
      this._Description = value;
      this._plainText = (string) null;
    }
  }

  [PXString(IsUnicode = true)]
  [PXUIField(Visible = false)]
  public virtual string DescriptionAsPlainText
  {
    get => this._plainText ?? (this._plainText = SearchService.Html2PlainText(this.Description));
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("")]
  [PXUIField]
  [PXSelector(typeof (CRCampaignType.typeID), DescriptionField = typeof (CRCampaignType.description))]
  public virtual string CampaignType
  {
    get => this._CampaignType;
    set => this._CampaignType = value;
  }

  [CRAttributesField(typeof (CRCampaign.campaignType))]
  public virtual string[] Attributes { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [PXUIField]
  [PXStringList(new string[] {}, new string[] {})]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "End Date")]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Expected Return")]
  public virtual Decimal? ExpectedRevenue
  {
    get => this._ExpectedRevenue;
    set => this._ExpectedRevenue = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Planned Budget")]
  public virtual Decimal? PlannedBudget
  {
    get => this._PlannedBudget;
    set => this._PlannedBudget = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Expected Response")]
  public virtual int? ExpectedResponse
  {
    get => this._ExpectedResponse;
    set => this._ExpectedResponse = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Mails Sent")]
  public virtual int? MailsSent
  {
    get => this._MailsSent;
    set => this._MailsSent = value;
  }

  [Owner(typeof (CRCampaign.workgroupID))]
  public int? OwnerID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Workgroup")]
  [PXCompanyTreeSelector]
  public int? WorkgroupID { get; set; }

  [PXDBString(30, IsUnicode = true)]
  [PXDefault("")]
  [PXUIField(DisplayName = "Promo Code", Required = false)]
  public virtual string PromoCodeID
  {
    get => this._PromoCodeID;
    set => this._PromoCodeID = value;
  }

  [PXString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Sent Emails Filter")]
  [PXDefault("N")]
  [SendFilterSources]
  public virtual string SendFilter
  {
    get => this._SendFilter;
    set => this._SendFilter = value;
  }

  [PXRestrictor(typeof (Where<PMProject.visibleInCR, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase]
  [PXUIField]
  public virtual int? ProjectID { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXDefault(typeof (PX.Data.Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<CRCampaign.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [PXDimensionSelector("PROTASK", typeof (Search2<PMTask.taskID, LeftJoin<CRCampaign, On<CRCampaign.projectID, Equal<PMTask.projectID>, And<CRCampaign.projectTaskID, Equal<PMTask.taskID>>>>, Where<PMTask.projectID, Equal<Current<CRCampaign.projectID>>, And<CRCampaign.campaignID, IsNull, Or<PMTask.taskID, Equal<Current<CRCampaign.projectTaskID>>>>>>), typeof (PMTask.taskCD), new System.Type[] {typeof (PMTask.taskCD), typeof (PMTask.description), typeof (PMTask.status)}, DescriptionField = typeof (PMTask.description))]
  public virtual int? ProjectTaskID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXNote(DescriptionField = typeof (CRCampaign.campaignID), Selector = typeof (CRCampaign.campaignID), ShowInReferenceSelector = true)]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  public class PK : PrimaryKeyOf<CRCampaign>.By<CRCampaign.campaignID>
  {
    public static CRCampaign Find(PXGraph graph, string campaignID, PKFindOptions options = 0)
    {
      return (CRCampaign) PrimaryKeyOf<CRCampaign>.By<CRCampaign.campaignID>.FindBy(graph, (object) campaignID, options);
    }
  }

  public static class FK
  {
    public class Type : 
      PrimaryKeyOf<CRCampaignType>.By<CRCampaignType.typeID>.ForeignKeyOf<CRCampaign>.By<CRCampaign.campaignType>
    {
    }

    public class Owner : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CRCampaign>.By<CRCampaign.ownerID>
    {
    }

    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<CRCampaign>.By<CRCampaign.workgroupID>
    {
    }
  }

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

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCampaign.description>
  {
  }

  public abstract class descriptionAsPlainText : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCampaign.descriptionAsPlainText>
  {
  }

  public abstract class campaignType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCampaign.campaignType>
  {
  }

  public abstract class attributes : BqlType<IBqlAttributes, string[]>.Field<CRCampaign.attributes>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCampaign.status>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRCampaign.isActive>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRCampaign.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRCampaign.endDate>
  {
  }

  public abstract class expectedRevenue : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRCampaign.expectedRevenue>
  {
  }

  public abstract class plannedBudget : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRCampaign.plannedBudget>
  {
  }

  public abstract class expectedResponse : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCampaign.expectedResponse>
  {
  }

  public abstract class mailsSent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCampaign.mailsSent>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCampaign.ownerID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCampaign.workgroupID>
  {
  }

  public abstract class promoCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCampaign.promoCodeID>
  {
  }

  public abstract class sendFilter : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCampaign.sendFilter>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCampaign.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRCampaign.projectTaskID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRCampaign.Tstamp>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCampaign.createdDateTime>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCampaign.createdByScreenID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRCampaign.createdByID>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRCampaign.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCampaign.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCampaign.lastModifiedDateTime>
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
