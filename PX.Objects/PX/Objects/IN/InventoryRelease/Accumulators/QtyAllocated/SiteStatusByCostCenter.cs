// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.SQLTree;
using PX.Data.Update;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction;
using PX.Objects.IN.InventoryRelease.Exceptions;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;

[PXHidden]
[SiteStatusByCostCenter.Accumulator(BqlTable = typeof (INSiteStatusByCostCenter))]
public class SiteStatusByCostCenter : INSiteStatusByCostCenter, IQtyAllocated, IQtyAllocatedBase
{
  [PXDBInt(IsKey = true)]
  [PXForeignSelector(typeof (INTran.inventoryID))]
  [PXSelectorMarker(typeof (SearchFor<PX.Objects.IN.InventoryItem.inventoryID>.Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<SiteStatusByCostCenter.inventoryID, IBqlInt>.FromCurrent>>), CacheGlobal = true, ValidateValue = false)]
  [PXDefault]
  public override int? InventoryID { get; set; }

  [Site(typeof (Where<True, Equal<True>>), false, false, IsKey = true, ValidateValue = false)]
  [PXRestrictor(typeof (Where<True, Equal<True>>), "", new Type[] {}, ReplaceInherited = true)]
  [PXDefault]
  public override int? SiteID
  {
    get => base.SiteID;
    set => base.SiteID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? CostCenterID
  {
    get => base.CostCenterID;
    set => base.CostCenterID = value;
  }

  [PXInt]
  [PXFormula(typeof (Selector<SiteStatusByCostCenter.inventoryID, PX.Objects.IN.InventoryItem.itemClassID>))]
  [PXSelectorMarker(typeof (SearchFor<INItemClass.itemClassID>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<SiteStatusByCostCenter.itemClassID, IBqlInt>.FromCurrent>>), CacheGlobal = true)]
  public virtual int? ItemClassID { get; set; }

  [PXString(10, IsUnicode = true)]
  [PXDefault(typeof (SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<SiteStatusByCostCenter.inventoryID, IBqlInt>.FromCurrent>>))]
  public virtual 
  #nullable disable
  string LotSerClassID { get; set; }

  [PXBool]
  [PXUnboundDefault(typeof (Selector<SiteStatusByCostCenter.itemClassID, INItemClass.negQty>))]
  public virtual bool? NegQty { get; set; }

  [PXBool]
  [PXUnboundDefault(typeof (Selector<SiteStatusByCostCenter.itemClassID, INItemClass.negQty>))]
  public virtual bool? NegAvailQty { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyAvail { get; set; }

  [PXString(10, IsUnicode = true)]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<SiteStatusByCostCenter.itemClassID, IBqlInt>.FromCurrent>>))]
  public virtual string AvailabilitySchemeID { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyINIssues))]
  public virtual bool? InclQtyINIssues { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyINReceipts))]
  public virtual bool? InclQtyINReceipts { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyINAssemblyDemand))]
  public virtual bool? InclQtyINAssemblyDemand { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyINAssemblySupply))]
  public virtual bool? InclQtyINAssemblySupply { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyInTransit))]
  public virtual bool? InclQtyInTransit { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOReverse))]
  public virtual bool? InclQtySOReverse { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOBackOrdered))]
  public virtual bool? InclQtySOBackOrdered { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOPrepared))]
  public virtual bool? InclQtySOPrepared { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOBooked))]
  public virtual bool? InclQtySOBooked { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOShipped))]
  public virtual bool? InclQtySOShipped { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOShipping))]
  public virtual bool? InclQtySOShipping { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyPOReceipts))]
  public virtual bool? InclQtyPOReceipts { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyPOPrepared))]
  public virtual bool? InclQtyPOPrepared { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyPOOrders))]
  public virtual bool? InclQtyPOOrders { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyFixedSOPO))]
  public virtual bool? InclQtyFixedSOPO { get; set; }

  [AvailabilityFlag(false)]
  public virtual bool? InclQtyPOFixedReceipt { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionDemandPrepared))]
  public virtual bool? InclQtyProductionDemandPrepared { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionDemand))]
  public virtual bool? InclQtyProductionDemand { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionAllocated))]
  public virtual bool? InclQtyProductionAllocated { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionSupplyPrepared))]
  public virtual bool? InclQtyProductionSupplyPrepared { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionSupply))]
  public virtual bool? InclQtyProductionSupply { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyFSSrvOrdPrepared))]
  public virtual bool? InclQtyFSSrvOrdPrepared { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyFSSrvOrdBooked))]
  public virtual bool? InclQtyFSSrvOrdBooked { get; set; }

  [AvailabilityFlag(typeof (SiteStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyFSSrvOrdAllocated))]
  public virtual bool? InclQtyFSSrvOrdAllocated { get; set; }

  [PXBool]
  public virtual bool? InitSiteStatus { get; set; }

  [PXBool]
  public virtual bool? PersistEvenZero { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? SkipQtyValidation { get; set; }

  /// <exclude />
  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? ValidateHardAvailQtyForAdjustments { get; set; }

  public new class PK : 
    PrimaryKeyOf<SiteStatusByCostCenter>.By<SiteStatusByCostCenter.inventoryID, SiteStatusByCostCenter.subItemID, SiteStatusByCostCenter.siteID, SiteStatusByCostCenter.costCenterID>
  {
    public static SiteStatusByCostCenter Find(
      PXGraph graph,
      int? inventoryID,
      int? subItemID,
      int? siteID,
      int? costCenterID,
      PKFindOptions options = 0)
    {
      return (SiteStatusByCostCenter) PrimaryKeyOf<SiteStatusByCostCenter>.By<SiteStatusByCostCenter.inventoryID, SiteStatusByCostCenter.subItemID, SiteStatusByCostCenter.siteID, SiteStatusByCostCenter.costCenterID>.FindBy(graph, (object) inventoryID, (object) subItemID, (object) siteID, (object) costCenterID, options);
    }
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SiteStatusByCostCenter.inventoryID>
  {
  }

  public new abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SiteStatusByCostCenter.subItemID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SiteStatusByCostCenter.siteID>
  {
  }

  public new abstract class costCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SiteStatusByCostCenter.costCenterID>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SiteStatusByCostCenter.itemClassID>
  {
  }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SiteStatusByCostCenter.lotSerClassID>
  {
  }

  public new abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusByCostCenter.qtyOnHand>
  {
  }

  public abstract class negQty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SiteStatusByCostCenter.negQty>
  {
  }

  public abstract class negAvailQty : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.negAvailQty>
  {
  }

  public abstract class inclQtyAvail : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyAvail>
  {
  }

  public abstract class availabilitySchemeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SiteStatusByCostCenter.availabilitySchemeID>
  {
  }

  public abstract class inclQtyINIssues : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyINIssues>
  {
  }

  public abstract class inclQtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyINReceipts>
  {
  }

  public abstract class inclQtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyINAssemblyDemand>
  {
  }

  public abstract class inclQtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyINAssemblySupply>
  {
  }

  public abstract class inclQtyInTransit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyInTransit>
  {
  }

  public abstract class inclQtySOReverse : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtySOReverse>
  {
  }

  public abstract class inclQtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtySOBackOrdered>
  {
  }

  public abstract class inclQtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtySOPrepared>
  {
  }

  public abstract class inclQtySOBooked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtySOBooked>
  {
  }

  public abstract class inclQtySOShipped : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtySOShipped>
  {
  }

  public abstract class inclQtySOShipping : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtySOShipping>
  {
  }

  public abstract class inclQtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyPOReceipts>
  {
  }

  public abstract class inclQtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyPOPrepared>
  {
  }

  public abstract class inclQtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyPOOrders>
  {
  }

  public abstract class inclQtyFixedSOPO : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyFixedSOPO>
  {
  }

  public abstract class inclQtyPOFixedReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyPOFixedReceipt>
  {
  }

  public abstract class inclQtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyProductionDemandPrepared>
  {
  }

  public abstract class inclQtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyProductionDemand>
  {
  }

  public abstract class inclQtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyProductionAllocated>
  {
  }

  public abstract class inclQtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyProductionSupplyPrepared>
  {
  }

  public abstract class inclQtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyProductionSupply>
  {
  }

  public abstract class inclQtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyFSSrvOrdPrepared>
  {
  }

  public abstract class inclQtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyFSSrvOrdBooked>
  {
  }

  public abstract class inclQtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.inclQtyFSSrvOrdAllocated>
  {
  }

  public abstract class initSiteStatus : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.initSiteStatus>
  {
  }

  public abstract class persistEvenZero : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.persistEvenZero>
  {
  }

  public abstract class skipQtyValidation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.skipQtyValidation>
  {
  }

  public abstract class validateHardAvailQtyForAdjustments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusByCostCenter.validateHardAvailQtyForAdjustments>
  {
  }

  public class AccumulatorAttribute : StatusAccumulatorAttribute
  {
    private const string SiteIDToVerifyField = "SiteIDToVerify";

    public AccumulatorAttribute() => this.SingleRecord = true;

    public override void CacheAttached(PXCache cache)
    {
      base.CacheAttached(cache);
      if (cache.Fields.Contains("SiteIDToVerify"))
        return;
      cache.Fields.Add("SiteIDToVerify");
      // ISSUE: method pointer
      cache.Graph.CommandPreparing.AddHandler(typeof (SiteStatusByCostCenter), "SiteIDToVerify", new PXCommandPreparing((object) this, __methodptr(SiteIDToVerifyCommandPreparing)));
    }

    private void SiteIDToVerifyCommandPreparing(PXCache cache, PXCommandPreparingEventArgs e)
    {
      if (PXDBOperationExt.Command(e.Operation) != 1)
        return;
      e.DataType = (PXDbType) 200;
      e.BqlTable = this._BqlTable;
      PXCommandPreparingEventArgs preparingEventArgs = e;
      Type type = e.Table;
      if ((object) type == null)
        type = this._BqlTable;
      Column column = new Column("SiteID", (Table) new SimpleTable(type, (string) null), (PXDbType) 8);
      preparingEventArgs.Expr = (SQLExpression) column;
      e.IsRestriction = true;
      string str = ((Table) new Query().Select<INCostCenter.siteID>().From<INCostCenter>().Where(SQLExpressionExt.EQ((SQLExpression) new Column<INCostCenter.costCenterID>((Table) null), (SQLExpression) new SQLConst(e.Value)).And(SQLExpressionExt.EQ((SQLExpression) new Column("CompanyID", typeof (INCostCenter), (PXDbType) 8), (SQLExpression) new SQLConst((object) PXInstanceHelper.CurrentCompany))))).SQLQuery(cache.Graph.SqlDialect.GetConnection()).ToString();
      e.Value = e.DataValue = (object) $"({str})";
    }

    protected override bool PrepareInsert(
      PXCache cache,
      object row,
      PXAccumulatorCollection columns)
    {
      if (!base.PrepareInsert(cache, row, columns))
        return false;
      columns.Update<SiteStatusByCostCenter.qtyOnHand>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyAvail>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyHardAvail>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyActual>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyINIssues>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyINReceipts>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyINAssemblyDemand>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyINAssemblySupply>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyInTransit>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtySOPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtySOBooked>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtySOShipped>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtySOShipping>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyPOReceipts>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyPOPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyPOOrders>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyInTransitToProduction>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyProductionSupplyPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyProductionSupply>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyPOFixedProductionPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyPOFixedProductionOrders>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyProductionDemandPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyProductionDemand>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyProductionAllocated>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtySOFixedProduction>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyProdFixedPurchase>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyProdFixedProduction>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyProdFixedProdOrdersPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyProdFixedProdOrders>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyProdFixedSalesOrdersPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyProdFixedSalesOrders>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyFSSrvOrdPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyFSSrvOrdBooked>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteStatusByCostCenter.qtyFSSrvOrdAllocated>((PXDataFieldAssign.AssignBehavior) 1);
      SiteStatusByCostCenter a = (SiteStatusByCostCenter) row;
      bool? negQty = a.NegQty;
      bool flag1 = false;
      Decimal? nullable1;
      if (negQty.GetValueOrDefault() == flag1 & negQty.HasValue && !a.SkipQtyValidation.GetValueOrDefault())
      {
        nullable1 = a.QtyOnHand;
        Decimal num = 0M;
        if (nullable1.GetValueOrDefault() < num & nullable1.HasValue)
        {
          PXAccumulatorCollection accumulatorCollection = columns;
          nullable1 = a.QtyOnHand;
          // ISSUE: variable of a boxed type
          __Boxed<Decimal?> local = (ValueType) (nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?());
          accumulatorCollection.Restrict<SiteStatusByCostCenter.qtyOnHand>((PXComp) 3, (object) local);
          goto label_9;
        }
      }
      bool? nullable2 = a.NegAvailQty;
      bool flag2 = false;
      if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
      {
        nullable2 = a.SkipQtyValidation;
        if (!nullable2.GetValueOrDefault())
        {
          nullable1 = a.QtyHardAvail;
          Decimal num = 0M;
          if (nullable1.GetValueOrDefault() < num & nullable1.HasValue)
          {
            PXAccumulatorCollection accumulatorCollection = columns;
            nullable1 = a.QtyHardAvail;
            // ISSUE: variable of a boxed type
            __Boxed<Decimal?> local = (ValueType) (nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?());
            accumulatorCollection.Restrict<INSiteStatusByCostCenter.qtyHardAvail>((PXComp) 3, (object) local);
          }
        }
      }
label_9:
      nullable2 = a.NegQty;
      bool flag3 = false;
      if (nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue)
      {
        nullable2 = a.SkipQtyValidation;
        if (!nullable2.GetValueOrDefault())
        {
          nullable1 = a.QtyActual;
          Decimal num = 0M;
          if (nullable1.GetValueOrDefault() < num & nullable1.HasValue)
          {
            PXAccumulatorCollection accumulatorCollection = columns;
            nullable1 = a.QtyActual;
            // ISSUE: variable of a boxed type
            __Boxed<Decimal?> local = (ValueType) (nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?());
            accumulatorCollection.Restrict<INSiteStatusByCostCenter.qtyActual>((PXComp) 3, (object) local);
          }
        }
      }
      if (!this._InternalCall)
      {
        int? nullable3 = a.CostCenterID;
        int num1 = 0;
        if (!(nullable3.GetValueOrDefault() == num1 & nullable3.HasValue))
        {
          nullable3 = a.SiteID;
          int num2 = new SiteAnyAttribute.transitSiteID().Value;
          if (!(nullable3.GetValueOrDefault() == num2 & nullable3.HasValue))
            columns.AppendException("The warehouse of the status table does not coincide with the warehouse of the cost center. Please contact your Acumatica support provider.", new PXAccumulatorRestriction[1]
            {
              new PXAccumulatorRestriction("SiteIDToVerify", (PXComp) 0, (object) a.CostCenterID)
            });
        }
      }
      nullable2 = a.NegQty;
      bool flag4 = false;
      if (nullable2.GetValueOrDefault() == flag4 & nullable2.HasValue)
      {
        nullable2 = a.SkipQtyValidation;
        if (!nullable2.GetValueOrDefault())
        {
          nullable2 = a.ValidateHardAvailQtyForAdjustments;
          if (nullable2.GetValueOrDefault())
          {
            nullable1 = a.QtyHardAvail;
            Decimal num = 0M;
            if (nullable1.GetValueOrDefault() < num & nullable1.HasValue)
              columns.AppendException(string.Empty, new PXAccumulatorRestriction[1]
              {
                (PXAccumulatorRestriction) new PXAccumulatorRestriction<INSiteStatusByCostCenter.qtyHardAvail>((PXComp) 3, (object) 0M)
              });
          }
        }
      }
      if (cache.GetStatus(row) == 2 && this.IsZero((IStatus) a))
      {
        nullable2 = a.PersistEvenZero;
        if (!nullable2.GetValueOrDefault() && cache.Locate(row) is SiteStatusByCostCenter statusByCostCenter && statusByCostCenter == row)
        {
          cache.SetStatus(row, (PXEntryStatus) 4);
          return false;
        }
      }
      return true;
    }

    public override bool PersistInserted(PXCache cache, object row)
    {
      try
      {
        return base.PersistInserted(cache, row);
      }
      catch (PXLockViolationException ex)
      {
        SiteStatusByCostCenter statusByCostCenter = (SiteStatusByCostCenter) row;
        SiteStatusByCostCenter a = (SiteStatusByCostCenter) PrimaryKeyOf<SiteStatusByCostCenter>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<SiteStatusByCostCenter.inventoryID, SiteStatusByCostCenter.subItemID, SiteStatusByCostCenter.siteID, SiteStatusByCostCenter.costCenterID>>.Find(cache.Graph, (TypeArrayOf<IBqlField>.IFilledWith<SiteStatusByCostCenter.inventoryID, SiteStatusByCostCenter.subItemID, SiteStatusByCostCenter.siteID, SiteStatusByCostCenter.costCenterID>) statusByCostCenter, (PKFindOptions) 0);
        SiteStatusByCostCenter sumStatus = this.Aggregate<SiteStatusByCostCenter>(cache, a, statusByCostCenter);
        string errorMessage = this.GetErrorMessage(cache, statusByCostCenter, sumStatus, false);
        if (errorMessage != null)
          throw new PXSiteStatusByCostCenterPersistInsertedException(statusByCostCenter.InventoryID, statusByCostCenter.SubItemID, statusByCostCenter.SiteID, statusByCostCenter.CostCenterID, errorMessage, new object[3]
          {
            PXForeignSelectorAttribute.GetValueExt<SiteStatusByCostCenter.inventoryID>(cache, row),
            PXForeignSelectorAttribute.GetValueExt<SiteStatusByCostCenter.subItemID>(cache, row),
            PXForeignSelectorAttribute.GetValueExt<SiteStatusByCostCenter.siteID>(cache, row)
          });
        throw;
      }
      catch (PXRestrictionViolationException ex)
      {
        SiteStatusByCostCenter statusByCostCenter1 = (SiteStatusByCostCenter) row;
        INSiteStatusByCostCenter statusByCostCenter2 = INSiteStatusByCostCenter.PK.Find(cache.Graph, statusByCostCenter1.InventoryID, statusByCostCenter1.SubItemID, statusByCostCenter1.SiteID, statusByCostCenter1.CostCenterID);
        bool? negQty = statusByCostCenter1.NegQty;
        bool flag = false;
        if (negQty.GetValueOrDefault() == flag & negQty.HasValue && !statusByCostCenter1.SkipQtyValidation.GetValueOrDefault() && statusByCostCenter1.ValidateHardAvailQtyForAdjustments.GetValueOrDefault() && statusByCostCenter2.QtyHardAvail.Value < 0M)
        {
          PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(cache.Graph, statusByCostCenter1.InventoryID);
          object[] objArray = new object[3]
          {
            (object) inventoryItem.InventoryCD,
            null,
            null
          };
          Decimal? qtyHardAvail = statusByCostCenter2.QtyHardAvail;
          objArray[1] = (object) (qtyHardAvail.HasValue ? new Decimal?(-qtyHardAvail.GetValueOrDefault()) : new Decimal?()).ToFormattedString();
          objArray[2] = (object) inventoryItem.BaseUnit;
          throw new PXException("Due to inventory item allocations, the available-for-shipping quantity of the {0} item will become negative. Reduce the allocated quantity by {1} {2} before releasing the adjustment. For details, see the Allocations Affected by Inventory Adjustments report.", objArray);
        }
        throw;
      }
    }

    public override void RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
    {
      if (PXDBOperationExt.Command(e.Operation) == 2 && e.TranStatus == null)
      {
        SiteStatusByCostCenter row = (SiteStatusByCostCenter) e.Row;
        string errorMessage = this.GetErrorMessage(cache, row, (SiteStatusByCostCenter) null, true);
        if (errorMessage != null)
          throw new PXException(errorMessage, new object[3]
          {
            PXForeignSelectorAttribute.GetValueExt<SiteStatusByCostCenter.inventoryID>(cache, e.Row),
            PXForeignSelectorAttribute.GetValueExt<SiteStatusByCostCenter.subItemID>(cache, e.Row),
            PXForeignSelectorAttribute.GetValueExt<SiteStatusByCostCenter.siteID>(cache, e.Row)
          });
      }
      base.RowPersisted(cache, e);
    }

    protected virtual string GetErrorMessage(
      PXCache cache,
      SiteStatusByCostCenter bal,
      SiteStatusByCostCenter sumStatus,
      bool insert)
    {
      string errorMessage = (string) null;
      bool? negQty = bal.NegQty;
      bool flag1 = false;
      Decimal? nullable1;
      if (negQty.GetValueOrDefault() == flag1 & negQty.HasValue && !bal.SkipQtyValidation.GetValueOrDefault())
      {
        nullable1 = bal.QtyOnHand;
        Decimal num1 = 0M;
        if (nullable1.GetValueOrDefault() < num1 & nullable1.HasValue)
        {
          if (!insert)
          {
            nullable1 = sumStatus.QtyOnHand;
            Decimal num2 = 0M;
            if (!(nullable1.GetValueOrDefault() < num2 & nullable1.HasValue))
              goto label_11;
          }
          errorMessage = GetCostLayerType(bal.CostCenterID) == "S" ? "The on-hand quantity of the {0} special-order item in the {2} warehouse is not sufficient to process the document." : "Updating item '{0} {1}' in warehouse '{2}' quantity on hand will go negative.";
          goto label_11;
        }
      }
      bool? nullable2 = bal.NegAvailQty;
      bool flag2 = false;
      if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
      {
        nullable2 = bal.SkipQtyValidation;
        if (!nullable2.GetValueOrDefault())
        {
          nullable1 = bal.QtyHardAvail;
          Decimal num3 = 0M;
          if (nullable1.GetValueOrDefault() < num3 & nullable1.HasValue)
          {
            if (!insert)
            {
              nullable1 = sumStatus.QtyHardAvail;
              Decimal num4 = 0M;
              if (!(nullable1.GetValueOrDefault() < num4 & nullable1.HasValue))
                goto label_11;
            }
            errorMessage = GetCostLayerType(bal.CostCenterID) == "S" ? "The available quantity of the {0} special-order item in the {2} warehouse is not sufficient to process the document." : "Updating item '{0} {1}' in warehouse '{2}' quantity available for shipment will go negative.";
          }
        }
      }
label_11:
      nullable2 = bal.NegQty;
      bool flag3 = false;
      if (nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue)
      {
        nullable2 = bal.SkipQtyValidation;
        if (!nullable2.GetValueOrDefault())
        {
          nullable1 = bal.QtyActual;
          Decimal num5 = 0M;
          if (nullable1.GetValueOrDefault() < num5 & nullable1.HasValue)
          {
            if (!insert)
            {
              nullable1 = sumStatus.QtyActual;
              Decimal num6 = 0M;
              if (!(nullable1.GetValueOrDefault() < num6 & nullable1.HasValue))
                goto label_17;
            }
            errorMessage = GetCostLayerType(bal.CostCenterID) == "S" ? "The available-for-issue quantity of the {0} special-order item in the {2} warehouse is not sufficient to release the document." : "The document cannot be released because the available-for-issue quantity would become negative for the {0} {1} item. This item is located in the {2} warehouse.";
          }
        }
      }
label_17:
      return errorMessage;

      string GetCostLayerType(int? costCenterID)
      {
        int? nullable = costCenterID;
        int num = 0;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          return "N";
        return INCostCenter.PK.Find(cache.Graph, costCenterID)?.CostLayerType;
      }
    }
  }
}
