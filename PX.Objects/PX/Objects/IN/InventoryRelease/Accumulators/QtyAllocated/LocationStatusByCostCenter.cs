// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter
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
using PX.Objects.CS;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction;
using PX.Objects.IN.PhysicalInventory;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;

[PXHidden]
[LocationStatusByCostCenter.Accumulator(BqlTable = typeof (INLocationStatusByCostCenter))]
public class LocationStatusByCostCenter : 
  INLocationStatusByCostCenter,
  IQtyAllocated,
  IQtyAllocatedBase
{
  [PXDBInt(IsKey = true)]
  [PXForeignSelector(typeof (INTran.inventoryID))]
  [PXSelectorMarker(typeof (SearchFor<PX.Objects.IN.InventoryItem.inventoryID>.Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<LocationStatusByCostCenter.inventoryID, IBqlInt>.FromCurrent>>), CacheGlobal = true, ValidateValue = false)]
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

  [Location(typeof (LocationStatusByCostCenter.siteID), IsKey = true, ValidateValue = false)]
  [PXDefault]
  [PXForeignReference(typeof (INLocationStatusByCostCenter.FK.Location))]
  public override int? LocationID
  {
    get => base.LocationID;
    set => base.LocationID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? CostCenterID
  {
    get => base.CostCenterID;
    set => base.CostCenterID = value;
  }

  [PXInt]
  [PXFormula(typeof (BqlOperand<PX.Objects.IN.InventoryItem.itemClassID, IBqlInt>.FromSelectorOf<LocationStatusByCostCenter.inventoryID>))]
  [PXSelectorMarker(typeof (SearchFor<INItemClass.itemClassID>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<LocationStatusByCostCenter.itemClassID, IBqlInt>.FromCurrent>>), CacheGlobal = true)]
  public virtual int? ItemClassID { get; set; }

  [PXBool]
  [PXUnboundDefault(typeof (Selector<LocationStatusByCostCenter.itemClassID, INItemClass.negQty>))]
  public virtual bool? NegQty { get; set; }

  [PXBool]
  [PXDefault(typeof (SelectFromBase<INLocation, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INLocation.locationID, IBqlInt>.IsEqual<BqlField<LocationStatusByCostCenter.locationID, IBqlInt>.FromCurrent>>))]
  public virtual bool? InclQtyAvail { get; set; }

  [PXString(10, IsUnicode = true)]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<LocationStatusByCostCenter.itemClassID, IBqlInt>.FromCurrent>>))]
  public virtual 
  #nullable disable
  string AvailabilitySchemeID { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyINIssues))]
  public virtual bool? InclQtyINIssues { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyINReceipts))]
  public virtual bool? InclQtyINReceipts { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyINAssemblyDemand))]
  public virtual bool? InclQtyINAssemblyDemand { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyINAssemblySupply))]
  public virtual bool? InclQtyINAssemblySupply { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyInTransit))]
  public virtual bool? InclQtyInTransit { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOReverse))]
  public virtual bool? InclQtySOReverse { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOBackOrdered))]
  public virtual bool? InclQtySOBackOrdered { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOPrepared))]
  public virtual bool? InclQtySOPrepared { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOBooked))]
  public virtual bool? InclQtySOBooked { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOShipped))]
  public virtual bool? InclQtySOShipped { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOShipping))]
  public virtual bool? InclQtySOShipping { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyPOReceipts))]
  public virtual bool? InclQtyPOReceipts { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyPOPrepared))]
  public virtual bool? InclQtyPOPrepared { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyPOOrders))]
  public virtual bool? InclQtyPOOrders { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyFixedSOPO))]
  public virtual bool? InclQtyFixedSOPO { get; set; }

  [AvailabilityFlag(false)]
  public virtual bool? InclQtyPOFixedReceipt { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionDemandPrepared))]
  public virtual bool? InclQtyProductionDemandPrepared { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionDemand))]
  public virtual bool? InclQtyProductionDemand { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionAllocated))]
  public virtual bool? InclQtyProductionAllocated { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionSupplyPrepared))]
  public virtual bool? InclQtyProductionSupplyPrepared { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionSupply))]
  public virtual bool? InclQtyProductionSupply { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyFSSrvOrdPrepared))]
  public virtual bool? InclQtyFSSrvOrdPrepared { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyFSSrvOrdBooked))]
  public virtual bool? InclQtyFSSrvOrdBooked { get; set; }

  [AvailabilityFlag(typeof (LocationStatusByCostCenter.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyFSSrvOrdAllocated))]
  public virtual bool? InclQtyFSSrvOrdAllocated { get; set; }

  [PXString(IsUnicode = true)]
  public virtual string RelatedPIID { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? SkipQtyValidation { get; set; }

  public new class PK : 
    PrimaryKeyOf<LocationStatusByCostCenter>.By<LocationStatusByCostCenter.inventoryID, LocationStatusByCostCenter.subItemID, LocationStatusByCostCenter.siteID, LocationStatusByCostCenter.locationID, LocationStatusByCostCenter.costCenterID>
  {
    public static LocationStatusByCostCenter Find(
      PXGraph graph,
      int? inventoryID,
      int? subItemID,
      int? siteID,
      int? locationID,
      int? costCenterID,
      PKFindOptions options = 0)
    {
      return (LocationStatusByCostCenter) PrimaryKeyOf<LocationStatusByCostCenter>.By<LocationStatusByCostCenter.inventoryID, LocationStatusByCostCenter.subItemID, LocationStatusByCostCenter.siteID, LocationStatusByCostCenter.locationID, LocationStatusByCostCenter.costCenterID>.FindBy(graph, (object) inventoryID, (object) subItemID, (object) siteID, (object) locationID, (object) costCenterID, options);
    }
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationStatusByCostCenter.inventoryID>
  {
  }

  public new abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationStatusByCostCenter.subItemID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationStatusByCostCenter.siteID>
  {
  }

  public new abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationStatusByCostCenter.locationID>
  {
  }

  public new abstract class costCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationStatusByCostCenter.costCenterID>
  {
  }

  public abstract class itemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationStatusByCostCenter.itemClassID>
  {
  }

  public new abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusByCostCenter.qtyOnHand>
  {
  }

  public abstract class negQty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocationStatusByCostCenter.negQty>
  {
  }

  public abstract class inclQtyAvail : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyAvail>
  {
  }

  public abstract class availabilitySchemeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationStatusByCostCenter.availabilitySchemeID>
  {
  }

  public abstract class inclQtyINIssues : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyINIssues>
  {
  }

  public abstract class inclQtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyINReceipts>
  {
  }

  public abstract class inclQtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyINAssemblyDemand>
  {
  }

  public abstract class inclQtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyINAssemblySupply>
  {
  }

  public abstract class inclQtyInTransit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyInTransit>
  {
  }

  public abstract class inclQtySOReverse : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtySOReverse>
  {
  }

  public abstract class inclQtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtySOBackOrdered>
  {
  }

  public abstract class inclQtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtySOPrepared>
  {
  }

  public abstract class inclQtySOBooked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtySOBooked>
  {
  }

  public abstract class inclQtySOShipped : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtySOShipped>
  {
  }

  public abstract class inclQtySOShipping : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtySOShipping>
  {
  }

  public abstract class inclQtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyPOReceipts>
  {
  }

  public abstract class inclQtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyPOPrepared>
  {
  }

  public abstract class inclQtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyPOOrders>
  {
  }

  public abstract class inclQtyFixedSOPO : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyFixedSOPO>
  {
  }

  public abstract class inclQtyPOFixedReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyPOFixedReceipt>
  {
  }

  public abstract class inclQtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyProductionDemandPrepared>
  {
  }

  public abstract class inclQtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyProductionDemand>
  {
  }

  public abstract class inclQtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyProductionAllocated>
  {
  }

  public abstract class inclQtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyProductionSupplyPrepared>
  {
  }

  public abstract class inclQtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyProductionSupply>
  {
  }

  public abstract class inclQtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyFSSrvOrdPrepared>
  {
  }

  public abstract class inclQtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyFSSrvOrdBooked>
  {
  }

  public abstract class inclQtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.inclQtyFSSrvOrdAllocated>
  {
  }

  public abstract class relatedPIID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationStatusByCostCenter.relatedPIID>
  {
  }

  public abstract class skipQtyValidation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusByCostCenter.skipQtyValidation>
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
      cache.Graph.CommandPreparing.AddHandler(typeof (LocationStatusByCostCenter), "SiteIDToVerify", new PXCommandPreparing((object) this, __methodptr(SiteIDToVerifyCommandPreparing)));
    }

    private void SiteIDToVerifyCommandPreparing(PXCache cache, PXCommandPreparingEventArgs e)
    {
      if (PXDBOperationExt.Command(e.Operation) != 1)
        return;
      e.DataType = (PXDbType) 200;
      e.BqlTable = this._BqlTable;
      Type type = e.Table == (Type) null ? this._BqlTable : e.Table;
      e.Expr = (SQLExpression) new Column("SiteID", (Table) new SimpleTable(type, (string) null), (PXDbType) 8);
      e.IsRestriction = true;
      string str = ((Table) new Query().Select<INLocation.siteID>().From<INLocation>().Where(SQLExpressionExt.EQ((SQLExpression) new Column<INLocation.locationID>((Table) null), (SQLExpression) new SQLConst(e.Value)).And(SQLExpressionExt.EQ((SQLExpression) new Column("CompanyID", typeof (INLocation), (PXDbType) 8), (SQLExpression) new SQLConst((object) PXInstanceHelper.CurrentCompany))))).SQLQuery(cache.Graph.SqlDialect.GetConnection()).ToString();
      e.Value = e.DataValue = (object) $"({str})";
    }

    protected override bool PrepareInsert(
      PXCache cache,
      object row,
      PXAccumulatorCollection columns)
    {
      if (!base.PrepareInsert(cache, row, columns))
        return false;
      columns.Update<LocationStatusByCostCenter.qtyOnHand>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyAvail>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyHardAvail>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyActual>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyINIssues>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyINReceipts>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyINAssemblyDemand>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyINAssemblySupply>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyInTransit>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtySOPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtySOBooked>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtySOShipped>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtySOShipping>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyPOReceipts>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyPOPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyPOOrders>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyInTransitToProduction>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyProductionSupplyPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyProductionSupply>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyPOFixedProductionPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyPOFixedProductionOrders>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyProductionDemandPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyProductionDemand>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyProductionAllocated>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtySOFixedProduction>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyProdFixedPurchase>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyProdFixedProduction>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyProdFixedProdOrdersPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyProdFixedProdOrders>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyProdFixedSalesOrdersPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyProdFixedSalesOrders>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyFSSrvOrdPrepared>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyFSSrvOrdBooked>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INLocationStatusByCostCenter.qtyFSSrvOrdAllocated>((PXDataFieldAssign.AssignBehavior) 1);
      LocationStatusByCostCenter statusByCostCenter = (LocationStatusByCostCenter) row;
      Decimal? qtyOnHand = statusByCostCenter.QtyOnHand;
      Decimal num1 = 0M;
      Decimal? nullable1;
      if (qtyOnHand.GetValueOrDefault() >= num1 & qtyOnHand.HasValue)
      {
        nullable1 = statusByCostCenter.QtyActual;
        Decimal num2 = 0M;
        if (nullable1.GetValueOrDefault() >= num2 & nullable1.HasValue)
          statusByCostCenter.NegQty = new bool?(true);
      }
      bool? nullable2 = statusByCostCenter.NegQty;
      bool flag = false;
      if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
      {
        nullable2 = statusByCostCenter.SkipQtyValidation;
        if (!nullable2.GetValueOrDefault())
        {
          nullable1 = statusByCostCenter.QtyOnHand;
          Decimal num3 = 0M;
          if (nullable1.GetValueOrDefault() < num3 & nullable1.HasValue)
          {
            PXAccumulatorCollection accumulatorCollection = columns;
            nullable1 = statusByCostCenter.QtyOnHand;
            // ISSUE: variable of a boxed type
            __Boxed<Decimal?> local = (ValueType) (nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?());
            accumulatorCollection.Restrict<LocationStatusByCostCenter.qtyOnHand>((PXComp) 3, (object) local);
          }
          nullable1 = statusByCostCenter.QtyActual;
          Decimal num4 = 0M;
          if (nullable1.GetValueOrDefault() < num4 & nullable1.HasValue)
          {
            PXAccumulatorCollection accumulatorCollection = columns;
            nullable1 = statusByCostCenter.QtyActual;
            // ISSUE: variable of a boxed type
            __Boxed<Decimal?> local = (ValueType) (nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?());
            accumulatorCollection.Restrict<INLocationStatusByCostCenter.qtyActual>((PXComp) 3, (object) local);
          }
        }
      }
      if (!this._InternalCall)
      {
        nullable1 = statusByCostCenter.QtyOnHand;
        Decimal num5 = 0M;
        if (!(nullable1.GetValueOrDefault() < num5 & nullable1.HasValue))
        {
          nullable1 = statusByCostCenter.QtySOShipped;
          Decimal num6 = 0M;
          if (!(nullable1.GetValueOrDefault() < num6 & nullable1.HasValue))
          {
            nullable1 = statusByCostCenter.QtyOnHand;
            Decimal num7 = 0M;
            if (!(nullable1.GetValueOrDefault() > num7 & nullable1.HasValue))
            {
              nullable1 = statusByCostCenter.QtySOShipped;
              Decimal num8 = 0M;
              if (!(nullable1.GetValueOrDefault() > num8 & nullable1.HasValue))
                goto label_18;
            }
          }
        }
        string PIID;
        if (this.CreateLocksInspector(statusByCostCenter.SiteID.Value).IsInventoryLocationLocked(statusByCostCenter.InventoryID, statusByCostCenter.LocationID, statusByCostCenter.RelatedPIID, out PIID))
          throw new PXException("You cannot change the quantity of the {0} item in the {1} location of the {2} warehouse because this item and location are used in the {3} physical inventory document. To review all locked items, see the Physical Inventory Locked Items (IN409000) inquiry.", new object[4]
          {
            PXForeignSelectorAttribute.GetValueExt<LocationStatusByCostCenter.inventoryID>(cache, (object) statusByCostCenter),
            PXForeignSelectorAttribute.GetValueExt<LocationStatusByCostCenter.locationID>(cache, (object) statusByCostCenter),
            PXForeignSelectorAttribute.GetValueExt<LocationStatusByCostCenter.siteID>(cache, (object) statusByCostCenter),
            (object) PIID
          });
      }
label_18:
      if (!this._InternalCall)
      {
        int? siteId = statusByCostCenter.SiteID;
        int num9 = new SiteAnyAttribute.transitSiteID().Value;
        if (!(siteId.GetValueOrDefault() == num9 & siteId.HasValue))
          columns.AppendException("The warehouse of the status table does not coincide with the warehouse of the location. Please contact your Acumatica support service.", new PXAccumulatorRestriction[1]
          {
            new PXAccumulatorRestriction("SiteIDToVerify", (PXComp) 0, (object) statusByCostCenter.LocationID)
          });
      }
      if (cache.GetStatus(row) != 2 || !this.IsZero((IStatus) statusByCostCenter))
        return true;
      cache.SetStatus(row, (PXEntryStatus) 4);
      return false;
    }

    public override bool PersistInserted(PXCache cache, object row)
    {
      try
      {
        return base.PersistInserted(cache, row);
      }
      catch (PXLockViolationException ex)
      {
        LocationStatusByCostCenter statusByCostCenter = (LocationStatusByCostCenter) row;
        LocationStatusByCostCenter a = (LocationStatusByCostCenter) PrimaryKeyOf<LocationStatusByCostCenter>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<LocationStatusByCostCenter.inventoryID, LocationStatusByCostCenter.subItemID, LocationStatusByCostCenter.siteID, LocationStatusByCostCenter.locationID, LocationStatusByCostCenter.costCenterID>>.Find(cache.Graph, (TypeArrayOf<IBqlField>.IFilledWith<LocationStatusByCostCenter.inventoryID, LocationStatusByCostCenter.subItemID, LocationStatusByCostCenter.siteID, LocationStatusByCostCenter.locationID, LocationStatusByCostCenter.costCenterID>) statusByCostCenter, (PKFindOptions) 0);
        LocationStatusByCostCenter sumStatus = this.Aggregate<LocationStatusByCostCenter>(cache, a, statusByCostCenter);
        string errorMessage = this.GetErrorMessage(cache, statusByCostCenter, sumStatus, false);
        if (errorMessage != null)
          throw new PXException(errorMessage, new object[4]
          {
            PXForeignSelectorAttribute.GetValueExt<LocationStatusByCostCenter.inventoryID>(cache, row),
            PXForeignSelectorAttribute.GetValueExt<LocationStatusByCostCenter.subItemID>(cache, row),
            PXForeignSelectorAttribute.GetValueExt<LocationStatusByCostCenter.siteID>(cache, row),
            PXForeignSelectorAttribute.GetValueExt<LocationStatusByCostCenter.locationID>(cache, row)
          });
        throw;
      }
    }

    public override void RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
    {
      if (PXDBOperationExt.Command(e.Operation) == 2 && e.TranStatus == null)
      {
        LocationStatusByCostCenter row = (LocationStatusByCostCenter) e.Row;
        string errorMessage = this.GetErrorMessage(cache, row, (LocationStatusByCostCenter) null, true);
        if (errorMessage != null)
          throw new PXException(errorMessage, new object[4]
          {
            PXForeignSelectorAttribute.GetValueExt<LocationStatusByCostCenter.inventoryID>(cache, e.Row),
            PXForeignSelectorAttribute.GetValueExt<LocationStatusByCostCenter.subItemID>(cache, e.Row),
            PXForeignSelectorAttribute.GetValueExt<LocationStatusByCostCenter.siteID>(cache, e.Row),
            PXForeignSelectorAttribute.GetValueExt<LocationStatusByCostCenter.locationID>(cache, e.Row)
          });
      }
      base.RowPersisted(cache, e);
    }

    protected virtual string GetErrorMessage(
      PXCache cache,
      LocationStatusByCostCenter bal,
      LocationStatusByCostCenter sumStatus,
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
              goto label_5;
          }
          errorMessage = GetCostLayerType(bal.CostCenterID) == "S" ? "The on-hand quantity of the {0} special-order item in the {3} location of the {2} warehouse is not sufficient to process the document." : "Updating item '{0} {1}' in warehouse '{2} {3}' quantity on hand will go negative.";
        }
      }
label_5:
      if (errorMessage == null)
      {
        bool? nullable2 = bal.NegQty;
        bool flag2 = false;
        if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
        {
          nullable2 = bal.SkipQtyValidation;
          if (!nullable2.GetValueOrDefault())
          {
            nullable1 = bal.QtyActual;
            Decimal num3 = 0M;
            if (nullable1.GetValueOrDefault() < num3 & nullable1.HasValue)
            {
              if (!insert)
              {
                nullable1 = sumStatus.QtyActual;
                Decimal num4 = 0M;
                if (!(nullable1.GetValueOrDefault() < num4 & nullable1.HasValue))
                  goto label_12;
              }
              errorMessage = GetCostLayerType(bal.CostCenterID) == "S" ? "The available-for-issue quantity of the {0} special-order item in the {3} location of the {2} warehouse is not sufficient to release the document." : "The document cannot be released because the available-for-issue quantity would become negative for the {0} {1} item. This item is located in the {3} location of the {2} warehouse.";
            }
          }
        }
      }
label_12:
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

    protected virtual PILocksInspector CreateLocksInspector(int siteID)
    {
      return new PILocksInspector(siteID);
    }
  }
}
