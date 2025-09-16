// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.EfficiencyFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXHidden]
public class EfficiencyFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Site]
  public virtual int? SiteID { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Start Date", Required = true)]
  public virtual DateTime? StartDate { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "End Date")]
  public virtual DateTime? EndDate { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Expand by User")]
  public bool? ExpandByUser { get; set; }

  [PXGuid]
  [PXUIField(DisplayName = "User")]
  [PXUIEnabled(typeof (EfficiencyFilter.expandByUser))]
  [PXFormula(typeof (BqlOperand<Null, IBqlNull>.When<BqlOperand<EfficiencyFilter.expandByUser, IBqlBool>.IsEqual<False>>.Else<EfficiencyFilter.userID>))]
  [PXSelector(typeof (SearchFor<Users.pKID>.Where<BqlOperand<Users.isHidden, IBqlBool>.IsEqual<False>>), SubstituteKey = typeof (Users.username))]
  public virtual Guid? UserID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Expand by Pick List")]
  public bool? ExpandByShipment { get; set; }

  [PXDBString(4, IsFixed = true, IsUnicode = false)]
  [PXDefault("SHPT")]
  [EfficiencyFilter.docType.List]
  [PXFormula(typeof (Default<EfficiencyFilter.expandByShipment>))]
  [PXUIEnabled(typeof (EfficiencyFilter.expandByShipment))]
  [PXUIVisible(typeof (Where<FeatureInstalled<FeaturesSet.wMSAdvancedPicking>>))]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXFormula(typeof (BqlOperand<Null, IBqlNull>.When<BqlOperand<EfficiencyFilter.expandByShipment, IBqlBool>.IsEqual<False>>.Else<EfficiencyFilter.standaloneShipmentNbr>))]
  [PXFormula(typeof (Default<EfficiencyFilter.siteID>))]
  [PXUIField(DisplayName = "Shipment Nbr.")]
  [PXUIEnabled(typeof (EfficiencyFilter.expandByShipment))]
  [PXUIVisible(typeof (Where<Not<FeatureInstalled<FeaturesSet.wMSAdvancedPicking>>>))]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<EfficiencyFilter.siteID>, IsNull>>>>.Or<BqlOperand<SOShipment.siteID, IBqlInt>.IsEqual<BqlField<EfficiencyFilter.siteID, IBqlInt>.FromCurrent.NoDefault>>>.Order<By<BqlField<SOShipment.shipmentNbr, IBqlString>.Desc>>, SOShipment>.SearchFor<SOShipment.shipmentNbr>))]
  [PXRestrictor(typeof (Where<BqlOperand<SOShipment.confirmed, IBqlBool>.IsEqual<True>>), "The shipment is not confirmed.", new Type[] {})]
  [PXDependsOnFields(new Type[] {typeof (EfficiencyFilter.shipmentNbr)})]
  public virtual string StandaloneShipmentNbr
  {
    get => this.ShipmentNbr;
    set => this.ShipmentNbr = value;
  }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIEnabled(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EfficiencyFilter.expandByShipment, Equal<True>>>>>.And<BqlOperand<EfficiencyFilter.docType, IBqlString>.IsEqual<SOShipmentProcessedByUser.docType.shipment>>))]
  [PXFormula(typeof (BqlOperand<Null, IBqlNull>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EfficiencyFilter.expandByShipment, Equal<False>>>>>.Or<BqlOperand<EfficiencyFilter.docType, IBqlString>.IsNotEqual<SOShipmentProcessedByUser.docType.shipment>>>.Else<EfficiencyFilter.shipmentNbr>))]
  [PXFormula(typeof (Default<EfficiencyFilter.siteID>))]
  [PXUIField(DisplayName = "Shipment Nbr.")]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<EfficiencyFilter.siteID>, IsNull>>>>.Or<BqlOperand<SOShipment.siteID, IBqlInt>.IsEqual<BqlField<EfficiencyFilter.siteID, IBqlInt>.FromCurrent.NoDefault>>>.Order<By<BqlField<SOShipment.shipmentNbr, IBqlString>.Desc>>, SOShipment>.SearchFor<SOShipment.shipmentNbr>))]
  [PXRestrictor(typeof (Where<BqlOperand<SOShipment.confirmed, IBqlBool>.IsEqual<True>>), "The shipment is not confirmed.", new Type[] {})]
  public virtual string ShipmentNbr { get; set; }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIEnabled(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EfficiencyFilter.expandByShipment, Equal<True>>>>>.And<BqlOperand<EfficiencyFilter.docType, IBqlString>.IsEqual<SOShipmentProcessedByUser.docType.pickList>>))]
  [PXFormula(typeof (BqlOperand<Null, IBqlNull>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EfficiencyFilter.expandByShipment, Equal<False>>>>>.Or<BqlOperand<EfficiencyFilter.docType, IBqlString>.IsNotEqual<SOShipmentProcessedByUser.docType.pickList>>>.Else<EfficiencyFilter.worksheetNbr>))]
  [PXFormula(typeof (Default<EfficiencyFilter.siteID>))]
  [PXUIField]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<SOPickingWorksheet, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingWorksheet.worksheetType, NotEqual<SOPickingWorksheet.worksheetType.single>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<EfficiencyFilter.siteID>, IsNull>>>>.Or<BqlOperand<SOPickingWorksheet.siteID, IBqlInt>.IsEqual<BqlField<EfficiencyFilter.siteID, IBqlInt>.FromCurrent.NoDefault>>>>.Order<By<BqlField<SOPickingWorksheet.worksheetNbr, IBqlString>.Desc>>, SOPickingWorksheet>.SearchFor<SOPickingWorksheet.worksheetNbr>))]
  [PXRestrictor(typeof (Where<Exists<SelectFromBase<SOPicker, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<SOPicker.worksheetNbr>.IsRelatedTo<SOPickingWorksheet.worksheetNbr>.AsSimpleKey.WithTablesOf<SOPickingWorksheet, SOPicker>, SOPickingWorksheet, SOPicker>.And<BqlOperand<SOPicker.confirmed, IBqlBool>.IsEqual<True>>>>>), "The picking worksheet has no confirmed pick lists.", new Type[] {})]
  public virtual string WorksheetNbr { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Expand by Day")]
  public bool? ExpandByDay { get; set; }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EfficiencyFilter.siteID>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EfficiencyFilter.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EfficiencyFilter.endDate>
  {
  }

  public abstract class expandByUser : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EfficiencyFilter.expandByUser>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EfficiencyFilter.userID>
  {
  }

  public abstract class expandByShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EfficiencyFilter.expandByShipment>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EfficiencyFilter.docType>
  {
    public const string PickList = "PLST";
    public const string Shipment = "SHPT";

    [PXLocalizable]
    public static class DisplayNames
    {
      public const string PickList = "Worksheet Nbr.";
      public const string Shipment = "Shipment Nbr.";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair("SHPT", "Shipment Nbr."),
          PXStringListAttribute.Pair("PLST", "Worksheet Nbr.")
        })
      {
      }
    }

    public class pickList : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EfficiencyFilter.docType.pickList>
    {
      public pickList()
        : base("PLST")
      {
      }
    }

    public class shipment : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EfficiencyFilter.docType.shipment>
    {
      public shipment()
        : base("SHPT")
      {
      }
    }
  }

  public abstract class standaloneShipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EfficiencyFilter.standaloneShipmentNbr>
  {
  }

  public abstract class shipmentNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EfficiencyFilter.shipmentNbr>
  {
  }

  public abstract class worksheetNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EfficiencyFilter.worksheetNbr>
  {
  }

  public abstract class expandByDay : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EfficiencyFilter.expandByDay>
  {
  }

  [PXLocalizable]
  public class Msg
  {
    public const string ShipmentIsNotConfirmed = "The shipment is not confirmed.";
    public const string WorksheetHasNoConfirmedPickLists = "The picking worksheet has no confirmed pick lists.";
  }
}
