// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteLotSerial
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction;
using PX.Objects.SO;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;

[PXHidden]
[SiteLotSerial.Accumulator]
public class SiteLotSerial : INSiteLotSerial, IQtyAllocatedBase
{
  [PXDBInt(IsKey = true)]
  [PXForeignSelector(typeof (INTran.inventoryID))]
  [PXSelectorMarker(typeof (SearchFor<PX.Objects.IN.InventoryItem.inventoryID>.Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<SiteLotSerial.inventoryID, IBqlInt>.FromCurrent>>), CacheGlobal = true, ValidateValue = false)]
  [PXDefault]
  public override int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [Site(typeof (Where<True, Equal<True>>), false, false, IsKey = true, ValidateValue = false)]
  [PXRestrictor(typeof (Where<BqlOperand<True, IBqlBool>.IsEqual<True>>), "", new Type[] {}, ReplaceInherited = true)]
  [PXDefault]
  public override int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBString(100, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public override 
  #nullable disable
  string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXGuid]
  public virtual Guid? RefNoteID { get; set; }

  [PXInt]
  [PXFormula(typeof (BqlOperand<PX.Objects.IN.InventoryItem.itemClassID, IBqlInt>.FromSelectorOf<SiteLotSerial.inventoryID>))]
  [PXSelectorMarker(typeof (SearchFor<INItemClass.itemClassID>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<SiteLotSerial.itemClassID, IBqlInt>.FromCurrent>>), CacheGlobal = true)]
  public virtual int? ItemClassID { get; set; }

  [PXString(10, IsUnicode = true)]
  [PXDefault(typeof (SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<SiteLotSerial.inventoryID, IBqlInt>.FromCurrent>>))]
  public virtual string LotSerClassID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (SelectFromBase<INLotSerClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INLotSerClass.lotSerClassID, IBqlString>.IsEqual<BqlField<SiteLotSerial.lotSerClassID, IBqlString>.FromCurrent>>))]
  public override string LotSerTrack
  {
    get => this._LotSerTrack;
    set => this._LotSerTrack = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (SelectFromBase<INLotSerClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INLotSerClass.lotSerClassID, IBqlString>.IsEqual<BqlField<SiteLotSerial.lotSerClassID, IBqlString>.FromCurrent>>))]
  public override string LotSerAssign
  {
    get => this._LotSerAssign;
    set => this._LotSerAssign = value;
  }

  [PXDBDate]
  public override DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? QtyNotAvail
  {
    get => base.QtyNotAvail;
    set => base.QtyNotAvail = value;
  }

  [PXBool]
  [PXUnboundDefault(typeof (Selector<SiteLotSerial.itemClassID, INItemClass.negQty>))]
  public virtual bool? NegQty { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? NegActualQty { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyAvail { get; set; }

  [PXString(10, IsUnicode = true)]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<SiteLotSerial.itemClassID, IBqlInt>.FromCurrent>>))]
  public virtual string AvailabilitySchemeID { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyINIssues))]
  public virtual bool? InclQtyINIssues { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyINReceipts))]
  public virtual bool? InclQtyINReceipts { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyINAssemblyDemand))]
  public virtual bool? InclQtyINAssemblyDemand { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyINAssemblySupply))]
  public virtual bool? InclQtyINAssemblySupply { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyInTransit))]
  public virtual bool? InclQtyInTransit { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOReverse))]
  public virtual bool? InclQtySOReverse { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOBackOrdered))]
  public virtual bool? InclQtySOBackOrdered { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOPrepared))]
  public virtual bool? InclQtySOPrepared { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOBooked))]
  public virtual bool? InclQtySOBooked { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOShipped))]
  public virtual bool? InclQtySOShipped { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtySOShipping))]
  public virtual bool? InclQtySOShipping { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyPOReceipts))]
  public virtual bool? InclQtyPOReceipts { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyPOPrepared))]
  public virtual bool? InclQtyPOPrepared { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyPOOrders))]
  public virtual bool? InclQtyPOOrders { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyFixedSOPO))]
  public virtual bool? InclQtyFixedSOPO { get; set; }

  [AvailabilityFlag(false)]
  public virtual bool? InclQtyPOFixedReceipt { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionDemandPrepared))]
  public virtual bool? InclQtyProductionDemandPrepared { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionDemand))]
  public virtual bool? InclQtyProductionDemand { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionAllocated))]
  public virtual bool? InclQtyProductionAllocated { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionSupplyPrepared))]
  public virtual bool? InclQtyProductionSupplyPrepared { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyProductionSupply))]
  public virtual bool? InclQtyProductionSupply { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyFSSrvOrdPrepared))]
  public virtual bool? InclQtyFSSrvOrdPrepared { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyFSSrvOrdBooked))]
  public virtual bool? InclQtyFSSrvOrdBooked { get; set; }

  [AvailabilityFlag(typeof (SiteLotSerial.availabilitySchemeID), typeof (INAvailabilityScheme.inclQtyFSSrvOrdAllocated))]
  public virtual bool? InclQtyFSSrvOrdAllocated { get; set; }

  /// <exclude />
  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? ValidateHardAvailQtyForAdjustments { get; set; }

  public new class PK : 
    PrimaryKeyOf<SiteLotSerial>.By<SiteLotSerial.inventoryID, SiteLotSerial.siteID, SiteLotSerial.lotSerialNbr>
  {
    public static SiteLotSerial Find(
      PXGraph graph,
      int? inventoryID,
      int? siteID,
      string lotSerialNbr,
      PKFindOptions options = 0)
    {
      return (SiteLotSerial) PrimaryKeyOf<SiteLotSerial>.By<SiteLotSerial.inventoryID, SiteLotSerial.siteID, SiteLotSerial.lotSerialNbr>.FindBy(graph, (object) inventoryID, (object) siteID, (object) lotSerialNbr, options);
    }
  }

  public new static class FK
  {
    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SiteLotSerial>.By<SiteLotSerial.siteID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<SiteLotSerial>.By<SiteLotSerial.inventoryID>
    {
    }
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SiteLotSerial.inventoryID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SiteLotSerial.siteID>
  {
  }

  public new abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SiteLotSerial.lotSerialNbr>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SiteLotSerial.refNoteID>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SiteLotSerial.itemClassID>
  {
  }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SiteLotSerial.lotSerClassID>
  {
  }

  public new abstract class qtyOnHand : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SiteLotSerial.qtyOnHand>
  {
  }

  public new abstract class lotSerTrack : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SiteLotSerial.lotSerTrack>
  {
  }

  public new abstract class lotSerAssign : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SiteLotSerial.lotSerAssign>
  {
  }

  public new abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SiteLotSerial.expireDate>
  {
  }

  public new abstract class qtyNotAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteLotSerial.qtyNotAvail>
  {
  }

  public abstract class negQty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SiteLotSerial.negQty>
  {
  }

  public abstract class negActualQty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SiteLotSerial.negActualQty>
  {
  }

  public abstract class inclQtyAvail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SiteLotSerial.inclQtyAvail>
  {
  }

  public abstract class availabilitySchemeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SiteLotSerial.availabilitySchemeID>
  {
  }

  public abstract class inclQtyINIssues : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtyINIssues>
  {
  }

  public abstract class inclQtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtyINReceipts>
  {
  }

  public abstract class inclQtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtyINAssemblyDemand>
  {
  }

  public abstract class inclQtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtyINAssemblySupply>
  {
  }

  public abstract class inclQtyInTransit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtyInTransit>
  {
  }

  public abstract class inclQtySOReverse : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtySOReverse>
  {
  }

  public abstract class inclQtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtySOBackOrdered>
  {
  }

  public abstract class inclQtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtySOPrepared>
  {
  }

  public abstract class inclQtySOBooked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtySOBooked>
  {
  }

  public abstract class inclQtySOShipped : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtySOShipped>
  {
  }

  public abstract class inclQtySOShipping : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtySOShipping>
  {
  }

  public abstract class inclQtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtyPOReceipts>
  {
  }

  public abstract class inclQtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtyPOPrepared>
  {
  }

  public abstract class inclQtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtyPOOrders>
  {
  }

  public abstract class inclQtyFixedSOPO : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtyFixedSOPO>
  {
  }

  public abstract class inclQtyPOFixedReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtyPOFixedReceipt>
  {
  }

  public abstract class inclQtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtyProductionDemandPrepared>
  {
  }

  public abstract class inclQtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtyProductionDemand>
  {
  }

  public abstract class inclQtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtyProductionAllocated>
  {
  }

  public abstract class inclQtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtyProductionSupplyPrepared>
  {
  }

  public abstract class inclQtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtyProductionSupply>
  {
  }

  public abstract class inclQtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtyFSSrvOrdPrepared>
  {
  }

  public abstract class inclQtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtyFSSrvOrdBooked>
  {
  }

  public abstract class inclQtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.inclQtyFSSrvOrdAllocated>
  {
  }

  public abstract class validateHardAvailQtyForAdjustments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteLotSerial.validateHardAvailQtyForAdjustments>
  {
  }

  public class AccumulatorAttribute : StatusAccumulatorAttribute
  {
    protected bool forceValidateAvailQty;
    protected bool suppressValidateDuplicatesOnReceipt;

    public virtual bool ValidateAvailQty(PXGraph graph)
    {
      return this.forceValidateAvailQty || !graph.UnattendedMode;
    }

    public static void ForceAvailQtyValidation(PXGraph graph, bool val)
    {
      graph.Caches.SubscribeCacheCreated<SiteLotSerial>((Action) (() =>
      {
        if (!(((PXCache) GraphHelper.Caches<SiteLotSerial>(graph)).Interceptor is SiteLotSerial.AccumulatorAttribute interceptor2))
          return;
        interceptor2.forceValidateAvailQty = val;
      }));
    }

    public static void SuppressValidateDuplicatesOnReceipt(PXGraph graph, bool val)
    {
      graph.Caches.SubscribeCacheCreated<SiteLotSerial>((Action) (() =>
      {
        if (!(((PXCache) GraphHelper.Caches<SiteLotSerial>(graph)).Interceptor is SiteLotSerial.AccumulatorAttribute interceptor2))
          return;
        interceptor2.suppressValidateDuplicatesOnReceipt = val;
      }));
    }

    public AccumulatorAttribute() => this.SingleRecord = true;

    protected virtual void PrepareSingleField<TQtyField>(
      PXCache cache,
      SiteLotSerial diff,
      PXAccumulatorCollection columns)
      where TQtyField : IBqlField, IImplement<IBqlDecimal>
    {
      Decimal? nullable1 = (Decimal?) cache.GetValue<TQtyField>((object) diff);
      if (diff.LotSerTrack == "S" && EnumerableExtensions.IsNotIn<Decimal?>(nullable1, new Decimal?(), new Decimal?(0M), new Decimal?(-1M), new Decimal?(1M)))
        throw new PXException("Duplicate serial number '{1}' for item '{0}' is found in document.", new object[2]
        {
          PXForeignSelectorAttribute.GetValueExt<SiteLotSerial.inventoryID>(cache, (object) diff),
          PXForeignSelectorAttribute.GetValueExt<SiteLotSerial.lotSerialNbr>(cache, (object) diff)
        });
      if (!(diff.LotSerTrack == "S") || !(diff.LotSerAssign == "R"))
        return;
      PXAccumulatorCollection accumulatorCollection1 = columns;
      Decimal num = 1M;
      Decimal? nullable2 = nullable1;
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> local1 = (ValueType) (nullable2.HasValue ? new Decimal?(num - nullable2.GetValueOrDefault()) : new Decimal?());
      accumulatorCollection1.Restrict<TQtyField>((PXComp) 5, (object) local1);
      PXAccumulatorCollection accumulatorCollection2 = columns;
      nullable2 = nullable1;
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> local2 = (ValueType) (nullable2.HasValue ? new Decimal?(0M - nullable2.GetValueOrDefault()) : new Decimal?());
      accumulatorCollection2.Restrict<TQtyField>((PXComp) 3, (object) local2);
    }

    protected virtual void ValidateSingleField<TQtyField>(
      PXCache cache,
      SiteLotSerial aggr,
      Guid? refNoteID,
      ref string message)
      where TQtyField : IBqlField, IImplement<IBqlDecimal>
    {
      Decimal? nullable1 = (Decimal?) cache.GetValue<TQtyField>((object) aggr);
      if (!(aggr.LotSerTrack == "S") || !(aggr.LotSerAssign == "R"))
        return;
      Decimal? nullable2 = nullable1;
      Decimal num1 = 0M;
      if (nullable2.GetValueOrDefault() < num1 & nullable2.HasValue)
      {
        message = !refNoteID.HasValue ? "Serial Number '{1}' for item '{0}' already issued." : "Serial Number '{1}' for item '{0}' already issued in '{2}'.";
      }
      else
      {
        nullable2 = nullable1;
        Decimal num2 = 1M;
        if (!(nullable2.GetValueOrDefault() > num2 & nullable2.HasValue))
          return;
        message = !refNoteID.HasValue ? "Serial Number '{1}' for item '{0}' is already received." : "Serial Number '{1}' for item '{0}' is already received in '{2}'.";
      }
    }

    protected override bool PrepareInsert(
      PXCache cache,
      object row,
      PXAccumulatorCollection columns)
    {
      if (!base.PrepareInsert(cache, row, columns))
        return false;
      SiteLotSerial siteLotSerial = (SiteLotSerial) row;
      if (siteLotSerial.LotSerTrack == "S" && siteLotSerial.LotSerAssign == "U")
        return false;
      columns.Update<SiteLotSerial.lotSerTrack>((PXDataFieldAssign.AssignBehavior) 4);
      columns.Update<SiteLotSerial.lotSerAssign>((PXDataFieldAssign.AssignBehavior) 4);
      PXAccumulatorCollection accumulatorCollection1 = columns;
      bool? nullable1 = siteLotSerial.UpdateExpireDate;
      int num1 = nullable1.GetValueOrDefault() ? 0 : 4;
      accumulatorCollection1.Update<SiteLotSerial.expireDate>((PXDataFieldAssign.AssignBehavior) num1);
      columns.Update<SiteLotSerial.qtyOnHand>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteLotSerial.qtyAvail>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<SiteLotSerial.qtyNotAvail>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteLotSerial.qtyHardAvail>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteLotSerial.qtyActual>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INSiteLotSerial.qtyInTransit>((PXDataFieldAssign.AssignBehavior) 1);
      Decimal? nullable2 = siteLotSerial.QtyOnHand;
      Decimal num2 = 0M;
      if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue))
        this.PrepareSingleField<SiteLotSerial.qtyOnHand>(cache, siteLotSerial, columns);
      else if (this.ValidateAvailQty(cache.Graph))
      {
        nullable2 = siteLotSerial.QtyHardAvail;
        Decimal num3 = 0M;
        if (nullable2.GetValueOrDefault() < num3 & nullable2.HasValue)
          this.PrepareSingleField<INSiteLotSerial.qtyHardAvail>(cache, siteLotSerial, columns);
        nullable2 = siteLotSerial.QtyAvail;
        Decimal num4 = 0M;
        if (!(nullable2.GetValueOrDefault() == num4 & nullable2.HasValue))
          this.PrepareSingleField<INSiteLotSerial.qtyAvail>(cache, siteLotSerial, columns);
        nullable2 = siteLotSerial.QtyHardAvail;
        Decimal num5 = 0M;
        if (nullable2.GetValueOrDefault() < num5 & nullable2.HasValue && siteLotSerial.LotSerTrack == "L" && siteLotSerial.LotSerAssign == "R")
        {
          PXAccumulatorCollection accumulatorCollection2 = columns;
          nullable2 = siteLotSerial.QtyHardAvail;
          // ISSUE: variable of a boxed type
          __Boxed<Decimal?> local = (ValueType) (nullable2.HasValue ? new Decimal?(-nullable2.GetValueOrDefault()) : new Decimal?());
          accumulatorCollection2.Restrict<INSiteLotSerial.qtyHardAvail>((PXComp) 3, (object) local);
        }
      }
      nullable1 = siteLotSerial.NegActualQty;
      if (!nullable1.GetValueOrDefault())
      {
        nullable2 = siteLotSerial.QtyActual;
        Decimal num6 = 0M;
        if (nullable2.GetValueOrDefault() < num6 & nullable2.HasValue && siteLotSerial.LotSerAssign == "R")
        {
          PXAccumulatorCollection accumulatorCollection3 = columns;
          nullable2 = siteLotSerial.QtyActual;
          // ISSUE: variable of a boxed type
          __Boxed<Decimal?> local = (ValueType) (nullable2.HasValue ? new Decimal?(-nullable2.GetValueOrDefault()) : new Decimal?());
          accumulatorCollection3.Restrict<INSiteLotSerial.qtyActual>((PXComp) 3, (object) local);
        }
      }
      nullable1 = siteLotSerial.ValidateHardAvailQtyForAdjustments;
      if (nullable1.GetValueOrDefault())
      {
        nullable2 = siteLotSerial.QtyHardAvail;
        Decimal num7 = 0M;
        if (nullable2.GetValueOrDefault() < num7 & nullable2.HasValue)
          columns.AppendException(string.Empty, new PXAccumulatorRestriction[1]
          {
            (PXAccumulatorRestriction) new PXAccumulatorRestriction<INSiteLotSerial.qtyHardAvail>((PXComp) 3, (object) 0M)
          });
      }
      if (cache.GetStatus(row) != 2 || !this.IsZero(siteLotSerial))
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
        SiteLotSerial siteLotSerial = (SiteLotSerial) row;
        SiteLotSerial a = (SiteLotSerial) PrimaryKeyOf<SiteLotSerial>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<SiteLotSerial.inventoryID, SiteLotSerial.siteID, SiteLotSerial.lotSerialNbr>>.Find(cache.Graph, (TypeArrayOf<IBqlField>.IFilledWith<SiteLotSerial.inventoryID, SiteLotSerial.siteID, SiteLotSerial.lotSerialNbr>) siteLotSerial, (PKFindOptions) 0);
        SiteLotSerial aggr = this.Aggregate<SiteLotSerial>(cache, a, siteLotSerial);
        aggr.LotSerTrack = siteLotSerial.LotSerTrack;
        aggr.LotSerAssign = siteLotSerial.LotSerAssign;
        bool isDuplicated;
        string refRowID;
        Guid? refNoteID = this.LookupDocumentsByLotSerialNumber(cache, siteLotSerial, out isDuplicated, out refRowID);
        string message = (string) null;
        if (siteLotSerial.LotSerTrack == "S" & isDuplicated && !this.suppressValidateDuplicatesOnReceipt)
          message = "Duplicate serial number '{1}' for item '{0}' is found in document.";
        Decimal? nullable = siteLotSerial.QtyOnHand;
        Decimal num1 = 0M;
        if (!(nullable.GetValueOrDefault() == num1 & nullable.HasValue))
          this.ValidateSingleField<SiteLotSerial.qtyOnHand>(cache, aggr, new Guid?(), ref message);
        else if (this.ValidateAvailQty(cache.Graph))
        {
          this.ValidateSingleField<INSiteLotSerial.qtyAvail>(cache, aggr, refNoteID, ref message);
          nullable = siteLotSerial.QtyHardAvail;
          Decimal num2 = 0M;
          if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
          {
            nullable = aggr.QtyHardAvail;
            Decimal num3 = 0M;
            if (nullable.GetValueOrDefault() < num3 & nullable.HasValue && siteLotSerial.LotSerTrack == "L" && siteLotSerial.LotSerAssign == "R")
              throw new PXException(!(cache.Graph is INRegisterEntryBase graph) || !((PXSelectBase<INSetup>) graph.insetup).Current.AllocateDocumentsOnHold.GetValueOrDefault() ? "The document cannot be saved because the quantity of the {0} item with the {2} lot number is not sufficient for shipping in the {1} warehouse." : "The document cannot be saved because the quantity of the {0} item with the {2} lot number is not sufficient for shipping in the {1} warehouse. To be able to save the document with the On Hold status, clear the Allocate Items in Documents on Hold check box on the General tab of the Inventory Preferences (IN101000) form.", new object[3]
              {
                PXForeignSelectorAttribute.GetValueExt<SiteLotSerial.inventoryID>(cache, row),
                PXForeignSelectorAttribute.GetValueExt<SiteLotSerial.siteID>(cache, row),
                PXForeignSelectorAttribute.GetValueExt<SiteLotSerial.lotSerialNbr>(cache, row)
              });
          }
        }
        if (!siteLotSerial.NegActualQty.GetValueOrDefault())
        {
          nullable = siteLotSerial.QtyActual;
          Decimal num4 = 0M;
          if (nullable.GetValueOrDefault() < num4 & nullable.HasValue)
          {
            nullable = aggr.QtyActual;
            Decimal num5 = 0M;
            if (nullable.GetValueOrDefault() < num5 & nullable.HasValue && siteLotSerial.LotSerAssign == "R")
              throw new PXException("The document cannot be released because the available-for-issue quantity would become negative for the {0} item. This item has the {2} lot/serial number and is located in the {1} warehouse.", new object[3]
              {
                PXForeignSelectorAttribute.GetValueExt<SiteLotSerial.inventoryID>(cache, row),
                PXForeignSelectorAttribute.GetValueExt<SiteLotSerial.siteID>(cache, row),
                PXForeignSelectorAttribute.GetValueExt<SiteLotSerial.lotSerialNbr>(cache, row)
              });
          }
        }
        if (message != null)
          throw new PXException(message, new object[3]
          {
            PXForeignSelectorAttribute.GetValueExt<SiteLotSerial.inventoryID>(cache, row),
            PXForeignSelectorAttribute.GetValueExt<SiteLotSerial.lotSerialNbr>(cache, row),
            (object) refRowID
          });
        throw;
      }
      catch (PXRestrictionViolationException ex)
      {
        SiteLotSerial siteLotSerial = (SiteLotSerial) row;
        INSiteLotSerial inSiteLotSerial = INSiteLotSerial.PK.Find(cache.Graph, siteLotSerial.InventoryID, siteLotSerial.SiteID, siteLotSerial.LotSerialNbr);
        if (siteLotSerial.ValidateHardAvailQtyForAdjustments.GetValueOrDefault() && inSiteLotSerial.QtyHardAvail.Value < 0M)
        {
          PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(cache.Graph, siteLotSerial.InventoryID);
          object[] objArray = new object[4]
          {
            (object) siteLotSerial.LotSerialNbr,
            (object) inventoryItem.InventoryCD,
            null,
            null
          };
          Decimal? qtyHardAvail = inSiteLotSerial.QtyHardAvail;
          objArray[2] = (object) (qtyHardAvail.HasValue ? new Decimal?(-qtyHardAvail.GetValueOrDefault()) : new Decimal?()).ToFormattedString();
          objArray[3] = (object) inventoryItem.BaseUnit;
          throw new PXException("Due to inventory item allocations, the available-for-shipping quantity of the {0} lot/serial number of the {1} item will become negative. Reduce the allocated quantity by {2} {3} before releasing the adjustment. For details, see the Allocations Affected by Inventory Adjustments report.", objArray);
        }
        throw;
      }
    }

    private Guid? LookupDocumentsByLotSerialNumber(
      PXCache cache,
      SiteLotSerial row,
      out bool isDuplicated,
      out string refRowID)
    {
      isDuplicated = false;
      refRowID = (string) null;
      PXResultset<INItemPlan> itemPlans = this.GetItemPlans(cache, row.InventoryID, row.SiteID, row.LotSerialNbr);
      if (itemPlans.Count <= 1)
        return new Guid?();
      List<Guid?> nullableList = new List<Guid?>();
      Dictionary<Guid?, int> dictionary = new Dictionary<Guid?, int>();
      PXCache<INItemPlan> pxCache = GraphHelper.Caches<INItemPlan>(cache.Graph);
      for (int index = 0; index < itemPlans.Count; ++index)
      {
        INItemPlan inItemPlan = ((PXResult) itemPlans[index]).GetItem<INItemPlan>();
        Guid? refNoteId = inItemPlan.RefNoteID;
        if (pxCache.GetStatus(inItemPlan) == null)
          nullableList.Insert(0, refNoteId);
        else
          nullableList.Add(refNoteId);
        if (dictionary.ContainsKey(refNoteId))
        {
          dictionary[refNoteId]++;
          isDuplicated = true;
        }
        else
          dictionary[refNoteId] = 1;
      }
      refRowID = new SiteLotSerial.AccumulatorAttribute.AutoNumberedEntityHelper(cache.Graph).GetEntityRowID(nullableList[0], ", ");
      return nullableList[0];
    }

    protected virtual PXResultset<INItemPlan> GetItemPlans(
      PXCache cache,
      int? inventoryID,
      int? siteID,
      string lotSerialNbr)
    {
      return PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.inventoryID, Equal<Required<INItemPlan.inventoryID>>, And<INItemPlan.siteID, Equal<Required<INItemPlan.siteID>>, And<INItemPlan.lotSerialNbr, Equal<Required<INItemPlan.lotSerialNbr>>>>>>.Config>.SelectWindowed(cache.Graph, 0, 10, new object[3]
      {
        (object) inventoryID,
        (object) siteID,
        (object) lotSerialNbr
      });
    }

    public virtual bool IsZero(SiteLotSerial a) => a.IsZero();

    protected class AutoNumberedEntityHelper(PXGraph graph) : EntityHelper(graph)
    {
      public virtual string GetFieldString(
        object row,
        Type entityType,
        string fieldName,
        bool preferDescription)
      {
        PXCache cach = this.graph.Caches[entityType];
        if (cach.GetStatus(row) == 2)
        {
          object row1 = cach.Locate(row);
          string keyToAbort = AutoNumberAttribute.GetKeyToAbort(cach, row1, fieldName);
          if (keyToAbort != null)
            return keyToAbort;
        }
        return base.GetFieldString(row, entityType, fieldName, preferDescription);
      }
    }
  }
}
