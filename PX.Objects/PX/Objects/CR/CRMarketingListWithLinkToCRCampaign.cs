// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMarketingListWithLinkToCRCampaign
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
[PXHidden]
[PXProjection(typeof (Select2<CRMarketingList, LeftJoin<CRCampaignToCRMarketingListLink, On<CRMarketingList.marketingListID, Equal<CRCampaignToCRMarketingListLink.marketingListID>, And<CRCampaignToCRMarketingListLink.campaignID, Equal<CurrentValue<CRCampaign.campaignID>>>>>>), Persistent = false)]
public class CRMarketingListWithLinkToCRCampaign : CRMarketingList
{
  [PXDBBool(BqlField = typeof (CRCampaignToCRMarketingListLink.selectedForCampaign))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? SelectedForCampaign { get; set; }

  /// <summary>
  /// A reference to the <see cref="T:PX.Objects.CR.CRCampaign.campaignID" />.
  /// </summary>
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (CRCampaignToCRMarketingListLink.campaignID))]
  [PXDefault(typeof (Current<CRCampaign.campaignID>))]
  [PXUIField]
  public virtual 
  #nullable disable
  string CampaignID { get; set; }

  /// <summary>
  /// The date of the last update of the marketing campaign members from the current list.
  /// </summary>
  [PXDBDate(PreserveTime = true, InputMask = "g", BqlField = typeof (CRCampaignToCRMarketingListLink.lastUpdateDate))]
  [PXUIField(DisplayName = "Last Updated On", Visible = true, Enabled = false)]
  public virtual DateTime? LastUpdateDate { get; set; }

  public abstract class selectedForCampaign : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRMarketingListWithLinkToCRCampaign.selectedForCampaign>
  {
  }

  public abstract class campaignID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingListWithLinkToCRCampaign.campaignID>
  {
  }

  public abstract class lastUpdateDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRMarketingListWithLinkToCRCampaign.lastUpdateDate>
  {
  }
}
