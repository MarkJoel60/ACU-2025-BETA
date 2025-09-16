// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OperationParam
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.CR;

[Serializable]
public class OperationParam : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CampaignID;

  [PXString]
  [PXDefault]
  [PXUIField(DisplayName = "Campaign ID")]
  [PXSelector(typeof (CRCampaign.campaignID), DescriptionField = typeof (CRCampaign.campaignName))]
  public virtual string CampaignID
  {
    get => this._CampaignID;
    set => this._CampaignID = value;
  }

  [PXString(20)]
  [PXUIField]
  [OperationParam.DataSourceList]
  [PXDefault("Inquiry")]
  public string DataSource { get; set; }

  [PXGuid]
  [PXUIField(DisplayName = "Generic Inquiry")]
  [ContactGISelector]
  public virtual Guid? ContactGI { get; set; }

  [PXInt]
  [PXDBDefault(typeof (CRMarketingList.marketingListID))]
  [PXUIField(DisplayName = "Marketing List")]
  [PXSelector(typeof (Search<CRMarketingList.marketingListID>), SubstituteKey = typeof (CRMarketingList.mailListCode), DescriptionField = typeof (CRMarketingList.name))]
  public virtual int? SourceMarketingListID { get; set; }

  [PXInt]
  [PXDBDefault(typeof (CRMarketingList.marketingListID))]
  [PXUIField(DisplayName = "Marketing List")]
  [PXSelector(typeof (Search<CRMarketingList.marketingListID>), DescriptionField = typeof (CRMarketingList.mailListCode))]
  public virtual int? MarketingListID { get; set; }

  [PXGuid]
  [PXUIField]
  [FilterList(typeof (OperationParam.contactGI), IsSiteMapIdentityScreenID = false, IsSiteMapIdentityGIDesignID = true)]
  [PXFormula(typeof (Default<OperationParam.contactGI>))]
  public virtual Guid? SharedGIFilter { get; set; }

  [PXString(6, IsFixed = true)]
  [OperationParam.ActionList]
  public virtual string Action { get; set; }

  [PXString(6, IsFixed = true)]
  [OperationParam.ActionList]
  public virtual string MarketingListMemberAction { get; set; }

  public abstract class campaignID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OperationParam.campaignID>
  {
  }

  public abstract class dataSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OperationParam.dataSource>
  {
  }

  public class DataSourceList : PXStringListAttribute
  {
    public const string Inquiry = "Inquiry";
    public const string MarketingLists = "MarketingLists";

    public DataSourceList()
      : base(new string[2]
      {
        nameof (Inquiry),
        nameof (MarketingLists)
      }, new string[2]{ nameof (Inquiry), "Marketing List" })
    {
    }

    public class contacts : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      OperationParam.DataSourceList.contacts>
    {
      public contacts()
        : base("Inquiry")
      {
      }
    }

    public class marketingLists : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      OperationParam.DataSourceList.marketingLists>
    {
      public marketingLists()
        : base("MarketingLists")
      {
      }
    }
  }

  public abstract class contactGI : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  OperationParam.contactGI>
  {
  }

  public abstract class sourceMarketingListID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OperationParam.sourceMarketingListID>
  {
  }

  public abstract class marketingListID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OperationParam.marketingListID>
  {
  }

  public abstract class sharedGIFilter : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  OperationParam.sharedGIFilter>
  {
  }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OperationParam.action>
  {
  }

  public class ActionList : PXStringListAttribute
  {
    public const string Add = "Add";
    public const string Delete = "Delete";

    public ActionList()
      : base(new string[2]{ nameof (Add), nameof (Delete) }, new string[2]
      {
        "Add Members",
        "Remove"
      })
    {
    }

    public class add : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    OperationParam.ActionList.add>
    {
      public add()
        : base("Add")
      {
      }
    }

    public class delete : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    OperationParam.ActionList.delete>
    {
      public delete()
        : base("Delete")
      {
      }
    }
  }
}
