// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCampaignToCRMarketingListLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <summary>CRMarketing list included into CRCampaign</summary>
[PXCacheName("CRCampaign To CRMarketingList Link")]
public class CRCampaignToCRMarketingListLink : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? SelectedForCampaign { get; set; }

  /// <summary>
  /// A reference to the <see cref="T:PX.Objects.CR.CRCampaign.campaignID" />.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault(typeof (Current<CRCampaign.campaignID>))]
  [PXUIField]
  [PXParent(typeof (CRCampaignToCRMarketingListLink.FK.Campaign))]
  public virtual 
  #nullable disable
  string CampaignID { get; set; }

  /// <summary>
  /// A reference to the <see cref="T:PX.Objects.CR.CRMarketingList.marketingListID" />.
  /// </summary>
  [PXDBInt(IsKey = true)]
  [PXForeignReference]
  [PXUIField(Visible = true)]
  public virtual int? MarketingListID { get; set; }

  /// <summary>
  /// The date of the last update of the marketing campaign members from the current list.
  /// </summary>
  [PXDBDate(PreserveTime = true, InputMask = "g")]
  [PXUIField(DisplayName = "Last Updated On", Visible = true, Enabled = false)]
  public virtual DateTime? LastUpdateDate { get; set; }

  /// <summary>Primary Key</summary>
  public class PK : 
    PrimaryKeyOf<CRCampaignToCRMarketingListLink>.By<CRCampaignToCRMarketingListLink.campaignID, CRCampaignToCRMarketingListLink.marketingListID>
  {
    public static CRCampaignToCRMarketingListLink Find(
      PXGraph graph,
      string campaignID,
      int marketingListID,
      PKFindOptions options = 0)
    {
      return (CRCampaignToCRMarketingListLink) PrimaryKeyOf<CRCampaignToCRMarketingListLink>.By<CRCampaignToCRMarketingListLink.campaignID, CRCampaignToCRMarketingListLink.marketingListID>.FindBy(graph, (object) campaignID, (object) marketingListID, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Campaign FK</summary>
    public class Campaign : 
      PrimaryKeyOf<CRCampaign>.By<CRCampaign.campaignID>.ForeignKeyOf<CRCampaign>.By<CRCampaignToCRMarketingListLink.campaignID>
    {
    }

    /// <summary>MarketingList FK</summary>
    public class MarketingList : 
      PrimaryKeyOf<CRMarketingList>.By<CRMarketingList.marketingListID>.ForeignKeyOf<CRMarketingList>.By<CRCampaignToCRMarketingListLink.marketingListID>
    {
    }
  }

  public abstract class selectedForCampaign : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRCampaignToCRMarketingListLink.selectedForCampaign>
  {
  }

  public abstract class campaignID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCampaignToCRMarketingListLink.campaignID>
  {
  }

  public abstract class marketingListID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRCampaignToCRMarketingListLink.marketingListID>
  {
  }

  public abstract class lastUpdateDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCampaignToCRMarketingListLink.lastUpdateDate>
  {
  }
}
