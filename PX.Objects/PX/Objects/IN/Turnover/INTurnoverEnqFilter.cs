// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Turnover.INTurnoverEnqFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN.Turnover;

[PXCacheName("Inventory Turnover Filter")]
public class INTurnoverEnqFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Organization(false, Required = false)]
  public virtual int? OrganizationID { get; set; }

  [BranchOfOrganization(typeof (INTurnoverEnqFilter.organizationID), false, null, null)]
  public virtual int? BranchID { get; set; }

  [OrganizationTree(typeof (INTurnoverEnqFilter.organizationID), typeof (INTurnoverEnqFilter.branchID), null, false, Required = true)]
  public virtual int? OrgBAccountID { get; set; }

  [PXDBBool]
  public bool? UseMasterCalendar { get; set; }

  [AnyPeriodFilterable(null, null, typeof (INTurnoverEnqFilter.branchID), null, typeof (INTurnoverEnqFilter.organizationID), typeof (INTurnoverEnqFilter.useMasterCalendar), null, false, null, null)]
  [PXUIField(DisplayName = "From Period", Required = true)]
  public virtual 
  #nullable disable
  string FromPeriodID { get; set; }

  [AnyPeriodFilterable(null, null, typeof (INTurnoverEnqFilter.branchID), null, typeof (INTurnoverEnqFilter.organizationID), typeof (INTurnoverEnqFilter.useMasterCalendar), null, false, null, null)]
  [PXUIField(DisplayName = "To Period", Required = true)]
  public virtual string ToPeriodID { get; set; }

  [Site(typeof (Where<BqlOperand<INSite.branchID, IBqlInt>.Is<Inside<BqlField<INTurnoverEnqFilter.orgBAccountID, IBqlInt>.FromCurrent>>>), false)]
  public virtual int? SiteID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Item Class")]
  [PXDimensionSelector("INITEMCLASS", typeof (Search<INItemClass.itemClassID, Where<BqlOperand<INItemClass.stkItem, IBqlBool>.IsEqual<True>>>), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), CacheGlobal = true)]
  public virtual int? ItemClassID { get; set; }

  [AnyInventory(typeof (FbqlSelect<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Match<BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>, And<BqlOperand<PX.Objects.IN.InventoryItem.stkItem, IBqlBool>.IsEqual<True>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<INTurnoverEnqFilter.itemClassID>, IsNull>>>>.Or<BqlOperand<PX.Objects.IN.InventoryItem.itemClassID, IBqlInt>.IsEqual<BqlField<INTurnoverEnqFilter.itemClassID, IBqlInt>.FromCurrent>>>>, PX.Objects.IN.InventoryItem>.SearchFor<PX.Objects.IN.InventoryItem.inventoryID>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr))]
  [PXRestrictor(typeof (Where<BqlOperand<PX.Objects.IN.InventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.markedForDeletion>>), "The inventory item is {0}.", new Type[] {typeof (PX.Objects.IN.InventoryItem.itemStatus)}, ShowWarning = true)]
  public virtual int? InventoryID { get; set; }

  [PXDBBool]
  public virtual bool? IsSuitableCalcsFound { get; set; }

  [PXDBBool]
  public virtual bool? IsPartialSuitableCalcs { get; set; }

  [PXDBBool]
  public virtual bool? IsMixedSuitableCalcs { get; set; }

  [Site]
  public virtual int? SuitableCalcsSiteID { get; set; }

  [PXDBInt]
  [PXDimensionSelector("INITEMCLASS", typeof (Search<INItemClass.itemClassID, Where<BqlOperand<INItemClass.stkItem, IBqlBool>.IsEqual<True>>>), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), CacheGlobal = true)]
  public virtual int? SuitableCalcsItemClassID { get; set; }

  [AnyInventory]
  public virtual int? SuitableCalcsInventoryID { get; set; }

  [PXDBBool]
  public virtual bool? IsInventoryListCalc { get; set; }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTurnoverEnqFilter.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTurnoverEnqFilter.branchID>
  {
  }

  public abstract class orgBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTurnoverEnqFilter.orgBAccountID>
  {
  }

  public abstract class useMasterCalendar : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INTurnoverEnqFilter.useMasterCalendar>
  {
  }

  public abstract class fromPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTurnoverEnqFilter.fromPeriodID>
  {
  }

  public abstract class toPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTurnoverEnqFilter.toPeriodID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTurnoverEnqFilter.siteID>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTurnoverEnqFilter.itemClassID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTurnoverEnqFilter.inventoryID>
  {
  }

  public abstract class isSuitableCalcsFound : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INTurnoverEnqFilter.isSuitableCalcsFound>
  {
  }

  public abstract class isPartialSuitableCalcs : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INTurnoverEnqFilter.isPartialSuitableCalcs>
  {
  }

  public abstract class isMixedSuitableCalcs : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INTurnoverEnqFilter.isMixedSuitableCalcs>
  {
  }

  public abstract class suitableCalcsSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTurnoverEnqFilter.suitableCalcsSiteID>
  {
  }

  public abstract class suitableCalcsItemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTurnoverEnqFilter.suitableCalcsItemClassID>
  {
  }

  public abstract class suitableCalcsInventoryID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INTurnoverEnqFilter.suitableCalcsInventoryID>
  {
  }

  public abstract class isInventoryListCalc : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INTurnoverEnqFilter.isInventoryListCalc>
  {
  }
}
