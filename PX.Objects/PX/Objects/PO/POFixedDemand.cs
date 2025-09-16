// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POFixedDemand
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

/// <inheritdoc />
[POCreateProjection]
[PXCacheName("PO Fixed Demand")]
public class POFixedDemand : INItemPlan
{
  protected Decimal? _OrderQty;

  /// <inheritdoc cref="P:PX.Objects.IN.INItemPlan.PlanDate" />
  [PXDBDate(BqlField = typeof (INItemPlan.planDate))]
  [PXUIField(DisplayName = "Requested On")]
  public virtual DateTime? RequestedDate { get; set; }

  /// <inheritdoc />
  [PXDBString(2, IsFixed = true)]
  [PXDefault]
  [PXSelector(typeof (Search<INPlanType.planType>), CacheGlobal = true, DescriptionField = typeof (INPlanType.localizedDescr))]
  public override 
  #nullable disable
  string PlanType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INPlanType.Descr" />
  [PXDBString(60, IsUnicode = true, BqlField = typeof (INPlanType.descr))]
  public virtual string PlanDescr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INPlanType.LocalizedDescr" />
  [PXString(60, IsUnicode = true)]
  [INPlanType.LocalizedField(typeof (POFixedDemand.planDescr))]
  [PXUIField(DisplayName = "Plan Type")]
  public virtual string LocalizedPlanDescr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INItemPlan.SourceSiteID" />
  [Site(DisplayName = "Demand Warehouse", DescriptionField = typeof (INSite.descr), BqlField = typeof (INItemPlan.sourceSiteID))]
  [PXFormula(typeof (Default<POFixedDemand.fixedSource>))]
  [PXDefault(typeof (SearchFor<INItemSiteSettings.replenishmentSourceSiteID>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSiteSettings.inventoryID, Equal<BqlField<POFixedDemand.inventoryID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<INItemSiteSettings.siteID, IBqlInt>.IsEqual<BqlField<POFixedDemand.siteID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<POFixedDemand.fixedSource, IBqlString>.IsIn<INReplenishmentSource.transfer, INReplenishmentSource.purchased>>>))]
  public override int? SourceSiteID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INSite.Descr" />
  [PXString(60, IsUnicode = true)]
  [PXFormula(typeof (BqlOperand<INSite.descr, IBqlString>.FromSelectorOf<POFixedDemand.sourceSiteID>))]
  [PXUIField]
  public virtual string SourceSiteDescr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INItemPlan.SiteID" />
  [PXDBInt(BqlField = typeof (INItemPlan.siteID))]
  [PXUIField]
  [PXDimensionSelector("INSITE", typeof (SearchFor<INSite.siteID>.Where<MatchUserFor<INSite>>), typeof (INSite.siteCD), DescriptionField = typeof (INSite.descr), CacheGlobal = true)]
  [PXRestrictor(typeof (Where<INSite.active, Equal<True>>), "Warehouse '{0}' is inactive", new System.Type[] {typeof (INSite.siteCD)})]
  [PXRestrictor(typeof (Where<INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>), "The warehouse cannot be selected; it is used for transit.", new System.Type[] {})]
  public virtual int? POSiteID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.BAccount.BAccountID" />
  [Vendor(typeof (Search2<BAccountR.bAccountID, LeftJoin<BranchAlias, On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.BAccount.isBranch, Equal<True>>>>>.And<BqlOperand<BranchAlias.bAccountID, IBqlInt>.IsEqual<BAccountR.bAccountID>>>>, Where<BqlOperand<PX.Objects.AP.Vendor.type, IBqlString>.IsNotEqual<BAccountType.employeeType>>>))]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.Vendor.vStatus, IsNull>>>>.Or<BqlOperand<PX.Objects.AP.Vendor.vStatus, IBqlString>.IsIn<VendorStatus.active, VendorStatus.oneTime, VendorStatus.holdPayments>>>), "The vendor status is '{0}'.", new System.Type[] {typeof (PX.Objects.AP.Vendor.vStatus)})]
  public override int? VendorID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.LocationID" />
  [PX.Objects.CS.LocationID(typeof (Where<BqlOperand<PX.Objects.CR.Location.bAccountID, IBqlInt>.IsEqual<BqlField<POFixedDemand.vendorID, IBqlInt>.FromCurrent>>))]
  [PXFormula(typeof (Default<POFixedDemand.vendorID>))]
  public override int? VendorLocationID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POVendorInventory.RecordID" />
  [PXInt]
  [PXDBScalar(typeof (POFixedDemand.priceRecordID.SearchBy<INItemPlan.vendorID, INItemPlan.vendorLocationID, INItemPlan.inventoryID, INItemPlan.subItemID>))]
  [PXDefault(typeof (POFixedDemand.priceRecordID.SearchBy<BqlField<POFixedDemand.vendorID, IBqlInt>.FromCurrent, BqlField<POFixedDemand.vendorLocationID, IBqlInt>.FromCurrent, BqlField<POFixedDemand.inventoryID, IBqlInt>.FromCurrent, BqlField<POFixedDemand.subItemID, IBqlInt>.FromCurrent>))]
  [PXFormula(typeof (Default<POFixedDemand.vendorLocationID>))]
  public virtual int? PriceRecordID { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Requested Qty.")]
  public override Decimal? PlanQty { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INUnit.FromUnit" />
  [PXDBString(BqlField = typeof (INUnit.fromUnit))]
  [PXUIField(DisplayName = "UOM")]
  public virtual string DemandUOM { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INUnit.UnitMultDiv" />
  [PXDBString(1, IsFixed = true, BqlField = typeof (INUnit.unitMultDiv))]
  public virtual string UnitMultDiv { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INUnit.UnitRate" />
  [PXDBDecimal(6, BqlField = typeof (INUnit.unitRate))]
  public virtual Decimal? UnitRate { get; set; }

  [PXQuantity]
  [PXDBCalced(typeof (BqlOperand<Mult<INItemPlan.planQty, INUnit.unitRate>, IBqlDecimal>.When<BqlOperand<INUnit.unitMultDiv, IBqlString>.IsEqual<MultDiv.divide>>.Else<BqlOperand<INItemPlan.planQty, IBqlDecimal>.Divide<INUnit.unitRate>>), typeof (Decimal))]
  public virtual Decimal? PlanUnitQty { get; set; }

  [PXQuantity]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? OrderQty
  {
    [PXDependsOnFields(new System.Type[] {typeof (POFixedDemand.planUnitQty)})] get
    {
      return this._OrderQty ?? this.PlanUnitQty;
    }
    set => this._OrderQty = value;
  }

  [PXNote(BqlTable = typeof (PX.Objects.IN.S.INItemSite))]
  public virtual Guid? NoteID { get; set; }

  /// <inheritdoc />
  [PXRefNote]
  [PXUIField(DisplayName = "Reference Nbr.")]
  public override Guid? RefNoteID { get; set; }

  [PXShort]
  [PXUIField(DisplayName = "Add. Lead Time (Days)")]
  public virtual short? AddLeadTimeDays { get; set; }

  [PXPriceCost]
  [PXUIField(DisplayName = "Vendor Price", Enabled = false)]
  public virtual Decimal? EffPrice { get; set; }

  [PXDecimal(6)]
  [PXFormula(typeof (BqlOperand<POFixedDemand.planQty, IBqlDecimal>.Multiply<BqlOperand<PX.Objects.IN.InventoryItem.baseWeight, IBqlDecimal>.FromSelectorOf<POFixedDemand.inventoryID>>))]
  [PXUIField(DisplayName = "Weight")]
  public virtual Decimal? ExtWeight { get; set; }

  [PXDecimal(6)]
  [PXFormula(typeof (BqlOperand<POFixedDemand.planQty, IBqlDecimal>.Multiply<BqlOperand<PX.Objects.IN.InventoryItem.baseVolume, IBqlDecimal>.FromSelectorOf<POFixedDemand.inventoryID>>))]
  [PXUIField(DisplayName = "Volume")]
  public virtual Decimal? ExtVolume { get; set; }

  [PXDecimal(typeof (SearchFor<PX.Objects.CM.Currency.decimalPlaces>.Where<BqlOperand<PX.Objects.CM.Currency.curyID, IBqlString>.IsEqual<BqlOperand<PX.Objects.AP.Vendor.curyID, IBqlString>.FromSelectorOf<BqlField<POCreate.POCreateFilter.vendorID, IBqlInt>.FromCurrent>>>))]
  [PXFormula(typeof (BqlOperand<POFixedDemand.orderQty, IBqlDecimal>.Multiply<POFixedDemand.effPrice>))]
  [PXUIField(DisplayName = "Extended Amt.", Enabled = false)]
  public virtual Decimal? ExtCost { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProject.ProjectID" />
  [PXInt]
  public virtual int? DemandProjectID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INItemClass.ItemClassCD" />
  [PXDBString(30, IsUnicode = true, InputMask = "", BqlField = typeof (INItemClass.itemClassCD))]
  [PXDimensionSelector("INITEMCLASS", typeof (SearchFor<INItemClass.itemClassCD>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemClass.stkItem, Equal<False>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemClass.stkItem, Equal<True>>>>>.And<FeatureInstalled<FeaturesSet.distributionModule>>>>), DescriptionField = typeof (INItemClass.descr))]
  [PXUIField]
  public virtual string ItemClassCD { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CM.Currency.CuryID" />
  [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXFormula(typeof (Default<POFixedDemand.vendorID>))]
  [PXUnboundDefault(typeof (BqlOperand<PX.Objects.AP.Vendor.curyID, IBqlString>.FromSelectorOf<POFixedDemand.vendorID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  [PXUIField]
  public virtual string CuryID { get; set; }

  /// <summary>
  /// Defines the order in which demands are processed upon Purchase Order creation.
  /// </summary>
  [PXString(IsUnicode = true)]
  public string SorterString { get; set; } = string.Empty;

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POFixedDemand.selected>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POFixedDemand.inventoryID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POFixedDemand.siteID>
  {
  }

  public abstract class requestedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POFixedDemand.requestedDate>
  {
  }

  public new abstract class planDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POFixedDemand.planDate>
  {
  }

  public new abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POFixedDemand.planID>
  {
  }

  public new abstract class fixedSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POFixedDemand.fixedSource>
  {
  }

  public new abstract class planType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POFixedDemand.planType>
  {
  }

  public abstract class planDescr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POFixedDemand.planDescr>
  {
  }

  public abstract class localizedPlanDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POFixedDemand.localizedPlanDescr>
  {
  }

  public new abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POFixedDemand.subItemID>
  {
  }

  public new abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POFixedDemand.locationID>
  {
  }

  public new abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POFixedDemand.lotSerialNbr>
  {
  }

  public new abstract class sourceSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POFixedDemand.sourceSiteID>
  {
  }

  public abstract class sourceSiteDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POFixedDemand.sourceSiteDescr>
  {
  }

  public abstract class pOSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POFixedDemand.pOSiteID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POFixedDemand.vendorID>
  {
  }

  public new abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POFixedDemand.vendorLocationID>
  {
  }

  public abstract class priceRecordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POFixedDemand.priceRecordID>
  {
    public class SearchBy<TVendorID, TVendorLocationID, TInventoryID, TSubItemID> : 
      FbqlSelect<SelectFromBase<POVendorInventory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<BAccountR>.On<BqlOperand<
      #nullable enable
      BAccountR.bAccountID, IBqlInt>.IsEqual<
      #nullable disable
      POVendorInventory.vendorID>>>, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<POVendorInventory.FK.InventoryItem>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
      #nullable enable
      POVendorInventory.vendorID, 
      #nullable disable
      Equal<TVendorID>>>>, And<BqlOperand<
      #nullable enable
      POVendorInventory.inventoryID, IBqlInt>.IsEqual<
      #nullable disable
      TInventoryID>>>, And<BqlOperand<
      #nullable enable
      POVendorInventory.active, IBqlBool>.IsEqual<
      #nullable disable
      True>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
      #nullable enable
      POVendorInventory.vendorLocationID, 
      #nullable disable
      Equal<TVendorLocationID>>>>>.Or<BqlOperand<
      #nullable enable
      POVendorInventory.vendorLocationID, IBqlInt>.IsNull>>>>.And<
      #nullable disable
      BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
      #nullable enable
      POVendorInventory.subItemID, 
      #nullable disable
      Equal<TSubItemID>>>>>.Or<BqlOperand<
      #nullable enable
      POVendorInventory.subItemID, IBqlInt>.IsEqual<
      #nullable disable
      PX.Objects.IN.InventoryItem.defaultSubItemID>>>>.Order<By<Desc<TestIf<BqlOperand<
      #nullable enable
      POVendorInventory.vendorLocationID, IBqlInt>.IsEqual<
      #nullable disable
      TVendorLocationID>>>, Desc<TestIf<BqlOperand<
      #nullable enable
      POVendorInventory.subItemID, IBqlInt>.IsEqual<
      #nullable disable
      TSubItemID>>>>>, POVendorInventory>.SearchFor<POVendorInventory.recordID>
      where TVendorID : IBqlOperand, IImplement<IBqlInt>
      where TVendorLocationID : IBqlOperand, IImplement<IBqlInt>
      where TInventoryID : IBqlOperand, IImplement<IBqlInt>
      where TSubItemID : IBqlOperand, IImplement<IBqlInt>
    {
    }
  }

  public new abstract class supplyPlanID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POFixedDemand.supplyPlanID>
  {
  }

  public new abstract class planQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POFixedDemand.planQty>
  {
  }

  public abstract class demandUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POFixedDemand.demandUOM>
  {
  }

  public abstract class unitMultDiv : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POFixedDemand.unitMultDiv>
  {
  }

  public abstract class unitRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POFixedDemand.unitRate>
  {
  }

  public abstract class planUnitQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POFixedDemand.planUnitQty>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POFixedDemand.orderQty>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POFixedDemand.noteID>
  {
  }

  public new abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POFixedDemand.refNoteID>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POFixedDemand.hold>
  {
  }

  public abstract class addLeadTimeDays : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    POFixedDemand.addLeadTimeDays>
  {
  }

  public abstract class effPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POFixedDemand.effPrice>
  {
  }

  public abstract class extWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POFixedDemand.extWeight>
  {
  }

  public abstract class extVolume : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POFixedDemand.extVolume>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POFixedDemand.extCost>
  {
  }

  public abstract class demandProjectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POFixedDemand.demandProjectID>
  {
  }

  public abstract class itemClassCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POFixedDemand.itemClassCD>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POFixedDemand.curyID>
  {
  }

  public abstract class sorterString : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POFixedDemand.sorterString>
  {
  }
}
