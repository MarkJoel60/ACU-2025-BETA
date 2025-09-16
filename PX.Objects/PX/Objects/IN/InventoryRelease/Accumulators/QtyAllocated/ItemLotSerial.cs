// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial
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
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;

[PXHidden]
[ItemLotSerial.Accumulator]
public class ItemLotSerial : INItemLotSerial, IQtyAllocatedSeparateReceipts, IQtyAllocatedBase
{
  [PXDBInt(IsKey = true)]
  [PXForeignSelector(typeof (INTran.inventoryID))]
  [PXSelectorMarker(typeof (SearchFor<PX.Objects.IN.InventoryItem.inventoryID>.Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<ItemLotSerial.inventoryID, IBqlInt>.FromCurrent>>), CacheGlobal = true, ValidateValue = false)]
  [PXDefault]
  public override int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
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
  [PXFormula(typeof (BqlOperand<PX.Objects.IN.InventoryItem.itemClassID, IBqlInt>.FromSelectorOf<ItemLotSerial.inventoryID>))]
  [PXSelectorMarker(typeof (SearchFor<INItemClass.itemClassID>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<ItemLotSerial.itemClassID, IBqlInt>.FromCurrent>>), CacheGlobal = true)]
  public virtual int? ItemClassID { get; set; }

  [PXString(10, IsUnicode = true)]
  [PXDefault(typeof (SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<ItemLotSerial.inventoryID, IBqlInt>.FromCurrent>>))]
  public virtual string LotSerClassID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (SelectFromBase<INLotSerClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INLotSerClass.lotSerClassID, IBqlString>.IsEqual<BqlField<ItemLotSerial.lotSerClassID, IBqlString>.FromCurrent>>))]
  public override string LotSerTrack
  {
    get => this._LotSerTrack;
    set => this._LotSerTrack = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (SelectFromBase<INLotSerClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INLotSerClass.lotSerClassID, IBqlString>.IsEqual<BqlField<ItemLotSerial.lotSerClassID, IBqlString>.FromCurrent>>))]
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

  [PXDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyNotAvail { get; set; }

  [PXBool]
  [PXUnboundDefault(typeof (Selector<ItemLotSerial.itemClassID, INItemClass.negQty>))]
  public virtual bool? NegQty { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyAvail { get; set; }

  [PXString(10, IsUnicode = true)]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<ItemLotSerial.itemClassID, IBqlInt>.FromCurrent>>))]
  public virtual string AvailabilitySchemeID { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyINIssues { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyINReceipts { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyINAssemblyDemand { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyINAssemblySupply { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyInTransit { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtySOReverse { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtySOBackOrdered { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtySOPrepared { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtySOBooked { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtySOShipped { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtySOShipping { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyPOReceipts { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyPOPrepared { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyPOOrders { get; set; }

  [AvailabilityFlag(false)]
  public virtual bool? InclQtyFixedSOPO { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyPOFixedReceipt { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyProductionDemandPrepared { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyProductionDemand { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyProductionAllocated { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyProductionSupplyPrepared { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyProductionSupply { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyFSSrvOrdPrepared { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyFSSrvOrdBooked { get; set; }

  [AvailabilityFlag(true)]
  public virtual bool? InclQtyFSSrvOrdAllocated { get; set; }

  [PXBool]
  public virtual bool? IsIntercompany { get; set; }

  public new class PK : 
    PrimaryKeyOf<ItemLotSerial>.By<ItemLotSerial.inventoryID, ItemLotSerial.lotSerialNbr>
  {
    public static ItemLotSerial Find(
      PXGraph graph,
      int? inventoryID,
      string lotSerialNbr,
      PKFindOptions options = 0)
    {
      return (ItemLotSerial) PrimaryKeyOf<ItemLotSerial>.By<ItemLotSerial.inventoryID, ItemLotSerial.lotSerialNbr>.FindBy(graph, (object) inventoryID, (object) lotSerialNbr, options);
    }
  }

  public new static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<ItemLotSerial>.By<ItemLotSerial.inventoryID>
    {
    }
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemLotSerial.inventoryID>
  {
  }

  public new abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ItemLotSerial.lotSerialNbr>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ItemLotSerial.refNoteID>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemLotSerial.itemClassID>
  {
  }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ItemLotSerial.lotSerClassID>
  {
  }

  public new abstract class qtyOnHand : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ItemLotSerial.qtyOnHand>
  {
  }

  public new abstract class lotSerTrack : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ItemLotSerial.lotSerTrack>
  {
  }

  public new abstract class lotSerAssign : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ItemLotSerial.lotSerAssign>
  {
  }

  public new abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ItemLotSerial.expireDate>
  {
  }

  public abstract class qtyNotAvail : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ItemLotSerial.qtyNotAvail>
  {
  }

  public abstract class negQty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ItemLotSerial.negQty>
  {
  }

  public abstract class inclQtyAvail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ItemLotSerial.inclQtyAvail>
  {
  }

  public abstract class availabilitySchemeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ItemLotSerial.availabilitySchemeID>
  {
  }

  public abstract class inclQtyINIssues : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtyINIssues>
  {
  }

  public abstract class inclQtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtyINReceipts>
  {
  }

  public abstract class inclQtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtyINAssemblyDemand>
  {
  }

  public abstract class inclQtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtyINAssemblySupply>
  {
  }

  public abstract class inclQtyInTransit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtyInTransit>
  {
  }

  public abstract class inclQtySOReverse : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtySOReverse>
  {
  }

  public abstract class inclQtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtySOBackOrdered>
  {
  }

  public abstract class inclQtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtySOPrepared>
  {
  }

  public abstract class inclQtySOBooked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtySOBooked>
  {
  }

  public abstract class inclQtySOShipped : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtySOShipped>
  {
  }

  public abstract class inclQtySOShipping : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtySOShipping>
  {
  }

  public abstract class inclQtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtyPOReceipts>
  {
  }

  public abstract class inclQtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtyPOPrepared>
  {
  }

  public abstract class inclQtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtyPOOrders>
  {
  }

  public abstract class inclQtyFixedSOPO : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtyFixedSOPO>
  {
  }

  public abstract class inclQtyPOFixedReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtyPOFixedReceipt>
  {
  }

  public abstract class inclQtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtyProductionDemandPrepared>
  {
  }

  public abstract class inclQtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtyProductionDemand>
  {
  }

  public abstract class inclQtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtyProductionAllocated>
  {
  }

  public abstract class inclQtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtyProductionSupplyPrepared>
  {
  }

  public abstract class inclQtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtyProductionSupply>
  {
  }

  public abstract class inclQtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtyFSSrvOrdPrepared>
  {
  }

  public abstract class inclQtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtyFSSrvOrdBooked>
  {
  }

  public abstract class inclQtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ItemLotSerial.inclQtyFSSrvOrdAllocated>
  {
  }

  public abstract class isIntercompany : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ItemLotSerial.isIntercompany>
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
      graph.Caches.SubscribeCacheCreated<ItemLotSerial>((Action) (() =>
      {
        if (!(((PXCache) GraphHelper.Caches<ItemLotSerial>(graph)).Interceptor is ItemLotSerial.AccumulatorAttribute interceptor2))
          return;
        interceptor2.forceValidateAvailQty = val;
      }));
    }

    public static void SuppressValidateDuplicatesOnReceipt(PXGraph graph, bool val)
    {
      graph.Caches.SubscribeCacheCreated<ItemLotSerial>((Action) (() =>
      {
        if (!(((PXCache) GraphHelper.Caches<ItemLotSerial>(graph)).Interceptor is ItemLotSerial.AccumulatorAttribute interceptor2))
          return;
        interceptor2.suppressValidateDuplicatesOnReceipt = val;
      }));
    }

    public AccumulatorAttribute() => this.SingleRecord = true;

    protected virtual void PrepareSingleField<TQtyField>(
      PXCache cache,
      ItemLotSerial diff,
      PXAccumulatorCollection columns)
      where TQtyField : IBqlField, IImplement<IBqlDecimal>
    {
      Decimal? nullable1 = (Decimal?) cache.GetValue<TQtyField>((object) diff);
      if (diff.LotSerTrack == "S" && EnumerableExtensions.IsNotIn<Decimal?>(nullable1, new Decimal?(), new Decimal?(0M), new Decimal?(-1M), new Decimal?(1M)))
        throw new PXException("Duplicate serial number '{1}' for item '{0}' is found in document.", new object[2]
        {
          PXForeignSelectorAttribute.GetValueExt<ItemLotSerial.inventoryID>(cache, (object) diff),
          PXForeignSelectorAttribute.GetValueExt<ItemLotSerial.lotSerialNbr>(cache, (object) diff)
        });
      if (diff.LotSerTrack == "S" && diff.LotSerAssign == "R")
      {
        PXAccumulatorCollection accumulatorCollection1 = columns;
        Decimal num1 = 1M;
        Decimal? nullable2 = nullable1;
        // ISSUE: variable of a boxed type
        __Boxed<Decimal?> local1 = (ValueType) (nullable2.HasValue ? new Decimal?(num1 - nullable2.GetValueOrDefault()) : new Decimal?());
        accumulatorCollection1.Restrict<TQtyField>((PXComp) 5, (object) local1);
        PXAccumulatorCollection accumulatorCollection2 = columns;
        nullable2 = nullable1;
        // ISSUE: variable of a boxed type
        __Boxed<Decimal?> local2 = (ValueType) (nullable2.HasValue ? new Decimal?(0M - nullable2.GetValueOrDefault()) : new Decimal?());
        accumulatorCollection2.Restrict<TQtyField>((PXComp) 3, (object) local2);
        nullable2 = diff.QtyOnHand;
        Decimal num2 = 1M;
        if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
          columns.AppendException(string.Empty, new PXAccumulatorRestriction[1]
          {
            (PXAccumulatorRestriction) new PXAccumulatorRestriction<INItemLotSerial.qtyInTransit>((PXComp) 1, (object) 1M)
          });
      }
      if (!(diff.LotSerTrack == "S") || !(diff.LotSerAssign == "U"))
        return;
      columns.AppendException(string.Empty, new PXAccumulatorRestriction[1]
      {
        (PXAccumulatorRestriction) new PXAccumulatorRestriction<TQtyField>((PXComp) 5, (object) 1M)
      });
      columns.AppendException(string.Empty, new PXAccumulatorRestriction[1]
      {
        (PXAccumulatorRestriction) new PXAccumulatorRestriction<TQtyField>((PXComp) 3, (object) -1M)
      });
      columns.AppendException(string.Empty, new PXAccumulatorRestriction[3]
      {
        (PXAccumulatorRestriction) new PXAccumulatorRestriction<INItemLotSerial.qtyOrig>((PXComp) 6, (object) null),
        (PXAccumulatorRestriction) new PXAccumulatorRestriction<INItemLotSerial.qtyOrig>((PXComp) 1, (object) -1M),
        (PXAccumulatorRestriction) new PXAccumulatorRestriction<TQtyField>((PXComp) 4, (object) 1M)
      });
      columns.AppendException(string.Empty, new PXAccumulatorRestriction[3]
      {
        (PXAccumulatorRestriction) new PXAccumulatorRestriction<INItemLotSerial.qtyOrig>((PXComp) 6, (object) null),
        (PXAccumulatorRestriction) new PXAccumulatorRestriction<INItemLotSerial.qtyOrig>((PXComp) 1, (object) 1M),
        (PXAccumulatorRestriction) new PXAccumulatorRestriction<TQtyField>((PXComp) 2, (object) -1M)
      });
    }

    protected virtual void ValidateSingleField<TQtyField>(
      PXCache cache,
      ItemLotSerial aggr,
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
      ItemLotSerial itemLotSerial = (ItemLotSerial) row;
      columns.Update<ItemLotSerial.lotSerTrack>((PXDataFieldAssign.AssignBehavior) 4);
      columns.Update<ItemLotSerial.lotSerAssign>((PXDataFieldAssign.AssignBehavior) 4);
      columns.Update<ItemLotSerial.expireDate>(itemLotSerial.UpdateExpireDate.GetValueOrDefault() ? (PXDataFieldAssign.AssignBehavior) 0 : (PXDataFieldAssign.AssignBehavior) 4);
      columns.Update<ItemLotSerial.qtyOnHand>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemLotSerial.qtyAvail>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemLotSerial.qtyHardAvail>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemLotSerial.qtyActual>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemLotSerial.qtyInTransit>((PXDataFieldAssign.AssignBehavior) 1);
      columns.InitializeWith<INItemLotSerial.qtyOrig>((object) itemLotSerial.QtyOrig);
      columns.InitializeWith<INItemLotSerial.noteID>((object) itemLotSerial.NoteID);
      bool flag1 = itemLotSerial.LotSerTrack == "S" && itemLotSerial.LotSerAssign == "U";
      bool flag2 = itemLotSerial.LotSerTrack == "S" && itemLotSerial.LotSerAssign == "R";
      Decimal? nullable = itemLotSerial.QtyOnHand;
      Decimal num1 = 0M;
      if (!(nullable.GetValueOrDefault() == num1 & nullable.HasValue))
      {
        if (flag1)
        {
          nullable = itemLotSerial.QtyOnHand;
          if (nullable.HasValue)
            columns.Update<INItemLotSerial.qtyOrig>((object) itemLotSerial.QtyOnHand, (PXDataFieldAssign.AssignBehavior) 4);
        }
        this.PrepareSingleField<ItemLotSerial.qtyOnHand>(cache, itemLotSerial, columns);
      }
      else if (this.ValidateAvailQty(cache.Graph))
      {
        nullable = itemLotSerial.QtyHardAvail;
        Decimal num2 = 0M;
        if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
          this.PrepareSingleField<INItemLotSerial.qtyHardAvail>(cache, itemLotSerial, columns);
        nullable = itemLotSerial.QtyAvail;
        Decimal num3 = 0M;
        if (!(nullable.GetValueOrDefault() == num3 & nullable.HasValue))
          this.PrepareSingleField<INItemLotSerial.qtyAvail>(cache, itemLotSerial, columns);
        nullable = itemLotSerial.QtyOnReceipt;
        Decimal num4 = 0M;
        if (!(nullable.GetValueOrDefault() == num4 & nullable.HasValue) & flag2)
        {
          this.PrepareSingleField<INItemLotSerial.qtyOnReceipt>(cache, itemLotSerial, columns);
          nullable = itemLotSerial.QtyOnReceipt;
          Decimal num5 = 0M;
          if (nullable.GetValueOrDefault() > num5 & nullable.HasValue && !this.suppressValidateDuplicatesOnReceipt)
          {
            PXAccumulatorCollection accumulatorCollection = columns;
            Decimal num6 = 1M;
            nullable = itemLotSerial.QtyOnReceipt;
            // ISSUE: variable of a boxed type
            __Boxed<Decimal?> local = (ValueType) (nullable.HasValue ? new Decimal?(num6 - nullable.GetValueOrDefault()) : new Decimal?());
            accumulatorCollection.Restrict<ItemLotSerial.qtyOnHand>((PXComp) 5, (object) local);
          }
        }
      }
      if (cache.GetStatus(row) != 2 || !this.IsZero(itemLotSerial))
        return true;
      cache.SetStatus(row, (PXEntryStatus) 4);
      return false;
    }

    public override bool PersistInserted(PXCache cache, object row)
    {
      ItemLotSerial b = (ItemLotSerial) row;
      try
      {
        return base.PersistInserted(cache, row);
      }
      catch (PXLockViolationException ex)
      {
        ItemLotSerial a = (ItemLotSerial) PrimaryKeyOf<ItemLotSerial>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<ItemLotSerial.inventoryID, ItemLotSerial.lotSerialNbr>>.Find(cache.Graph, (TypeArrayOf<IBqlField>.IFilledWith<ItemLotSerial.inventoryID, ItemLotSerial.lotSerialNbr>) b, (PKFindOptions) 0);
        ItemLotSerial itemLotSerial = this.Aggregate<ItemLotSerial>(cache, a, b);
        itemLotSerial.LotSerTrack = b.LotSerTrack;
        itemLotSerial.LotSerAssign = b.LotSerAssign;
        bool flag = b.LotSerTrack == "S" && b.LotSerAssign == "R";
        Decimal? nullable;
        if (b.IsIntercompany.GetValueOrDefault() & flag)
        {
          nullable = b.QtyOnHand;
          Decimal num1 = 0M;
          if (nullable.GetValueOrDefault() > num1 & nullable.HasValue)
          {
            nullable = itemLotSerial.QtyOnHand;
            Decimal num2 = 1M;
            if (nullable.GetValueOrDefault() > num2 & nullable.HasValue && GraphHelper.RowCast<INItemPlan>((IEnumerable) this.GetItemPlans(cache, b.InventoryID, b.LotSerialNbr)).Any<INItemPlan>((Func<INItemPlan, bool>) (p => p.PlanType == "62")))
              throw new PXIntercompanyReceivedNotIssuedException(cache, (IBqlTable) itemLotSerial);
          }
        }
        bool isDuplicated;
        string refRowID;
        Guid? refNoteID = this.LookupDocumentsByLotSerialNumber(cache, b.InventoryID, b.LotSerialNbr, out isDuplicated, out refRowID);
        string message = (string) null;
        if (b.LotSerTrack == "S" & isDuplicated && !this.suppressValidateDuplicatesOnReceipt)
          message = "Duplicate serial number '{1}' for item '{0}' is found in document.";
        nullable = b.QtyOnHand;
        Decimal num3 = 0M;
        if (!(nullable.GetValueOrDefault() == num3 & nullable.HasValue))
          this.ValidateSingleField<ItemLotSerial.qtyOnHand>(cache, itemLotSerial, new Guid?(), ref message);
        else if (this.ValidateAvailQty(cache.Graph))
        {
          this.ValidateSingleField<INItemLotSerial.qtyAvail>(cache, itemLotSerial, refNoteID, ref message);
          this.ValidateSingleField<INItemLotSerial.qtyHardAvail>(cache, itemLotSerial, refNoteID, ref message);
          if (flag)
          {
            this.ValidateSingleField<INItemLotSerial.qtyOnReceipt>(cache, itemLotSerial, refNoteID, ref message);
            if (message == null)
            {
              Decimal? qtyOnHand = itemLotSerial.QtyOnHand;
              Decimal? qtyOnReceipt = b.QtyOnReceipt;
              nullable = qtyOnHand.HasValue & qtyOnReceipt.HasValue ? new Decimal?(qtyOnHand.GetValueOrDefault() + qtyOnReceipt.GetValueOrDefault()) : new Decimal?();
              Decimal num4 = 1M;
              if (nullable.GetValueOrDefault() > num4 & nullable.HasValue && this.suppressValidateDuplicatesOnReceipt)
                message = "Serial Number '{1}' for item '{0}' is already received.";
            }
          }
        }
        if (message != null)
          throw new PXException(message, new object[3]
          {
            PXForeignSelectorAttribute.GetValueExt<ItemLotSerial.inventoryID>(cache, row),
            PXForeignSelectorAttribute.GetValueExt<ItemLotSerial.lotSerialNbr>(cache, row),
            (object) refRowID
          });
        throw;
      }
      catch (PXRestrictionViolationException ex)
      {
        bool isDuplicated;
        string refRowID;
        Guid? nullable = this.LookupDocumentsByLotSerialNumber(cache, b.InventoryID, b.LotSerialNbr, out isDuplicated, out refRowID);
        bool flag = ex.Index % 2 == 0;
        throw new PXException(!(b.LotSerTrack == "S" & isDuplicated) || this.suppressValidateDuplicatesOnReceipt ? (flag ? (nullable.HasValue ? "Serial Number '{1}' for item '{0}' is already received in '{2}'." : "Serial Number '{1}' for item '{0}' is already received.") : (nullable.HasValue ? "Serial Number '{1}' for item '{0}' already issued in '{2}'." : "Serial Number '{1}' for item '{0}' already issued.")) : "Duplicate serial number '{1}' for item '{0}' is found in document.", new object[3]
        {
          PXForeignSelectorAttribute.GetValueExt<ItemLotSerial.inventoryID>(cache, row),
          PXForeignSelectorAttribute.GetValueExt<ItemLotSerial.lotSerialNbr>(cache, row),
          (object) refRowID
        });
      }
    }

    protected virtual Guid? LookupDocumentsByLotSerialNumber(
      PXCache cache,
      int? inventoryID,
      string lotSerialNbr,
      out bool isDuplicated,
      out string refRowID)
    {
      isDuplicated = false;
      refRowID = (string) null;
      PXResultset<INItemPlan> itemPlans = this.GetItemPlans(cache, inventoryID, lotSerialNbr);
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
      refRowID = new ItemLotSerial.AccumulatorAttribute.AutoNumberedEntityHelper(cache.Graph).GetEntityRowID(nullableList[0], ", ");
      return nullableList[0];
    }

    protected virtual PXResultset<INItemPlan> GetItemPlans(
      PXCache cache,
      int? inventoryID,
      string lotSerialNbr)
    {
      return PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.inventoryID, Equal<Required<INItemPlan.inventoryID>>, And<INItemPlan.lotSerialNbr, Equal<Required<INItemPlan.lotSerialNbr>>>>>.Config>.SelectWindowed(cache.Graph, 0, 10, new object[2]
      {
        (object) inventoryID,
        (object) lotSerialNbr
      });
    }

    public virtual bool IsZero(ItemLotSerial a) => a.IsZero();

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
