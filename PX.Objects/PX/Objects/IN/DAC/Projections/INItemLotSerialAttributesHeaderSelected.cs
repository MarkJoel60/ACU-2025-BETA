// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.Projections.INItemLotSerialAttributesHeaderSelected
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Interfaces;
using PX.Objects.CS;
using PX.Objects.IN.DAC.Unbound;
using System;

#nullable enable
namespace PX.Objects.IN.DAC.Projections;

/// <exclude />
[PXCacheName("INItemLotSerialAttributesHeaderSelected")]
[PXProjection(typeof (SelectFromBase<INItemLotSerialAttributesHeader, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.INItemLotSerial>.On<INItemLotSerialAttributesHeader.FK.INItemLotSerial>>, FbqlJoins.Inner<INSiteLotSerial>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteLotSerial.inventoryID, Equal<INItemLotSerialAttributesHeader.inventoryID>>>>, And<BqlOperand<INSiteLotSerial.lotSerialNbr, IBqlString>.IsEqual<INItemLotSerialAttributesHeader.lotSerialNbr>>>>.And<BqlOperand<INSiteLotSerial.siteID, IBqlInt>.IsNotEqual<SiteAnyAttribute.transitSiteID>>>>, FbqlJoins.Inner<INSite>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteLotSerial.siteID, Equal<INSite.siteID>>>>>.And<Where<INSite.baseCuryID, EqualBaseCuryID<Current2<AddLotSerialHeader.branchID>>>>>>, FbqlJoins.Left<INLotSerialStatusByCostCenter>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLotSerialStatusByCostCenter.inventoryID, Equal<INSiteLotSerial.inventoryID>>>>, And<BqlOperand<INLotSerialStatusByCostCenter.siteID, IBqlInt>.IsEqual<INSiteLotSerial.siteID>>>, And<BqlOperand<INLotSerialStatusByCostCenter.lotSerialNbr, IBqlString>.IsEqual<INSiteLotSerial.lotSerialNbr>>>, And<BqlOperand<INLotSerialStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<CostCenter.freeStock>>>>.And<BqlOperand<CurrentValue<AddLotSerialHeader.showLocation>, IBqlBool>.IsEqual<True>>>>, FbqlJoins.Left<INLocation>.On<BqlOperand<INLotSerialStatusByCostCenter.locationID, IBqlInt>.IsEqual<INLocation.locationID>>>, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.InventoryItem.inventoryID, Equal<INSiteLotSerial.inventoryID>>>>>.And<BqlOperand<PX.Objects.IN.InventoryItem.stkItem, IBqlBool>.IsEqual<True>>>>>.Where<BqlChainableConditionLite<CurrentMatch<INSite, AccessInfo.userName>>.And<BqlChainableConditionLite<FeatureInstalled<FeaturesSet.interBranch>>.Or<SameOrganizationBranch<INSite.branchID, Current<AddLotSerialHeader.branchID>>>>>), Persistent = false)]
public class INItemLotSerialAttributesHeaderSelected : 
  PXBqlTable,
  IQtySelectable,
  IPXSelectable,
  IBqlTable,
  IBqlTableSystemDataStorage
{
  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.IN.InventoryItem.lotSerClassID))]
  public virtual 
  #nullable disable
  string LotSerClassID { get; set; }

  [StockItem(BqlField = typeof (INItemLotSerialAttributesHeader.inventoryID), IsKey = true)]
  [PXDefault]
  public virtual int? InventoryID { get; set; }

  [PXDefault]
  [PX.Objects.IN.LotSerialNbr(BqlField = typeof (INItemLotSerialAttributesHeader.lotSerialNbr), IsKey = true)]
  public virtual string LotSerialNbr { get; set; }

  [PXDBString(100, IsUnicode = true, BqlField = typeof (INItemLotSerialAttributesHeader.mfgLotSerialNbr))]
  [PXUIField(DisplayName = "Manufacturer Lot/Serial Nbr.")]
  public virtual string MfgLotSerialNbr { get; set; }

  [PXDBLocalizableString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (INItemLotSerialAttributesHeader.descr), IsProjection = true)]
  [PXUIField]
  public virtual string Descr { get; set; }

  [Site(BqlField = typeof (INSiteLotSerial.siteID), IsKey = true)]
  public virtual int? SiteID { get; set; }

  [PXDBCalced(typeof (IsNull<INLotSerialStatusByCostCenter.locationID, int0>), typeof (int))]
  [PXInt(IsKey = true)]
  public virtual int? LocationID { get; set; }

  [PXUIVisible(typeof (Where<BqlOperand<Current<AddLotSerialHeader.showLocation>, IBqlBool>.IsEqual<True>>))]
  [LocationRaw(BqlField = typeof (INLocation.locationCD), DisplayName = "Location")]
  public virtual string LocationCD { get; set; }

  [INUnit]
  public virtual string BaseUnit { get; set; }

  [PXDBCalced(typeof (IIf<Where<INLotSerialStatusByCostCenter.qtyOnHand, IsNull>, INSiteLotSerial.qtyOnHand, Minimum<INLotSerialStatusByCostCenter.qtyOnHand, INSiteLotSerial.qtyOnHand>>), typeof (Decimal))]
  [PXQuantity]
  [PXUIField(DisplayName = "Qty. On Hand")]
  public virtual Decimal? QtyOnHand { get; set; }

  [PXDBCalced(typeof (IIf<Where<INLotSerialStatusByCostCenter.qtyAvail, IsNull>, INSiteLotSerial.qtyAvail, Minimum<INLotSerialStatusByCostCenter.qtyAvail, INSiteLotSerial.qtyAvail>>), typeof (Decimal))]
  [PXQuantity]
  [PXUIField(DisplayName = "Qty. Available")]
  public virtual Decimal? QtyAvail { get; set; }

  [PXDBCalced(typeof (IIf<Where<INLotSerialStatusByCostCenter.qtyHardAvail, IsNull>, INSiteLotSerial.qtyHardAvail, Minimum<INLotSerialStatusByCostCenter.qtyHardAvail, INSiteLotSerial.qtyHardAvail>>), typeof (Decimal))]
  [PXQuantity]
  [PXUIField(DisplayName = "Qty. Available For Shipping")]
  public virtual Decimal? QtyHardAvail { get; set; }

  [PXQuantity]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Selected")]
  public virtual Decimal? QtySelected { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.IN.INItemLotSerial.expireDate))]
  public virtual DateTime? ExpireDate { get; set; }

  [BorrowedNote(typeof (INItemLotSerialAttributesHeader), typeof (INItemLotSerialAttributesMaint), BqlField = typeof (INItemLotSerialAttributesHeader.noteID))]
  [PXReplaceUDFConfiguration(typeof (INItemLotSerialAttributesHeader))]
  public virtual Guid? NoteID { get; set; }

  public abstract class selected : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemLotSerialAttributesHeaderSelected.selected>
  {
  }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerialAttributesHeaderSelected.lotSerClassID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemLotSerialAttributesHeaderSelected.inventoryID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerialAttributesHeaderSelected.lotSerialNbr>
  {
  }

  public abstract class mfgLotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerialAttributesHeaderSelected.mfgLotSerialNbr>
  {
  }

  public abstract class descr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerialAttributesHeaderSelected.descr>
  {
  }

  public abstract class siteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemLotSerialAttributesHeaderSelected.siteID>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemLotSerialAttributesHeaderSelected.locationID>
  {
  }

  public abstract class locationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerialAttributesHeaderSelected.locationCD>
  {
  }

  public abstract class baseUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerialAttributesHeaderSelected.baseUnit>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemLotSerialAttributesHeaderSelected.qtyOnHand>
  {
  }

  public abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemLotSerialAttributesHeaderSelected.qtyAvail>
  {
  }

  public abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemLotSerialAttributesHeaderSelected.qtyHardAvail>
  {
  }

  public abstract class qtySelected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemLotSerialAttributesHeaderSelected.qtySelected>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemLotSerialAttributesHeaderSelected.expireDate>
  {
  }

  public abstract class noteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INItemLotSerialAttributesHeaderSelected.noteID>
  {
  }
}
