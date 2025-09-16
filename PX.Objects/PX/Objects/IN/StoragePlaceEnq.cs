// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.StoragePlaceEnq
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN;

public class StoragePlaceEnq : PXGraph<
#nullable disable
StoragePlaceEnq>
{
  public PXFilter<StoragePlaceEnq.StoragePlaceFilter> Filter;
  [PXFilterable(new Type[] {})]
  public FbqlSelect<SelectFromBase<StoragePlaceStatus, TypeArrayOf<IFbqlJoin>.Empty>, StoragePlaceStatus>.View storages;

  public virtual IEnumerable Storages()
  {
    if (((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) this.Filter).Current.StorageType == "L")
    {
      if (((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) this.Filter).Current.ExpandByLotSerialNbr.GetValueOrDefault())
      {
        FbqlSelect<SelectFromBase<INLocation, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLocationStatus>.On<INLocationStatus.FK.Location>>, FbqlJoins.Left<INLotSerialStatus>.On<KeysRelation<CompositeKey<Field<INLotSerialStatus.inventoryID>.IsRelatedTo<INLocationStatus.inventoryID>, Field<INLotSerialStatus.subItemID>.IsRelatedTo<INLocationStatus.subItemID>, Field<INLotSerialStatus.siteID>.IsRelatedTo<INLocationStatus.siteID>, Field<INLotSerialStatus.locationID>.IsRelatedTo<INLocationStatus.locationID>>.WithTablesOf<INLocationStatus, INLotSerialStatus>, INLocationStatus, INLotSerialStatus>.And<BqlOperand<INLotSerialStatus.qtyOnHand, IBqlDecimal>.IsGreater<Zero>>>>, FbqlJoins.Inner<INSite>.On<INLocation.FK.Site>>, FbqlJoins.Inner<InventoryItem>.On<INLocationStatus.FK.InventoryItem>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<StoragePlaceEnq.StoragePlaceFilter.siteID>, Equal<INSite.siteID>>>>, And<BqlOperand<INLocationStatus.qtyOnHand, IBqlDecimal>.IsGreater<Zero>>>>.And<BqlOperand<INLocation.active, IBqlBool>.IsEqual<True>>>.Order<By<BqlField<INSite.siteCD, IBqlString>.Asc, BqlField<INLocation.locationCD, IBqlString>.Asc, BqlField<InventoryItem.inventoryCD, IBqlString>.Asc, BqlField<INLocationStatus.subItemID, IBqlInt>.Asc, BqlField<INLotSerialStatus.lotSerialNbr, IBqlString>.Asc, BqlField<INLocationStatus.qtyOnHand, IBqlDecimal>.Desc, BqlField<INLotSerialStatus.qtyOnHand, IBqlDecimal>.Desc>>, INLocation>.View select = new FbqlSelect<SelectFromBase<INLocation, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLocationStatus>.On<INLocationStatus.FK.Location>>, FbqlJoins.Left<INLotSerialStatus>.On<KeysRelation<CompositeKey<Field<INLotSerialStatus.inventoryID>.IsRelatedTo<INLocationStatus.inventoryID>, Field<INLotSerialStatus.subItemID>.IsRelatedTo<INLocationStatus.subItemID>, Field<INLotSerialStatus.siteID>.IsRelatedTo<INLocationStatus.siteID>, Field<INLotSerialStatus.locationID>.IsRelatedTo<INLocationStatus.locationID>>.WithTablesOf<INLocationStatus, INLotSerialStatus>, INLocationStatus, INLotSerialStatus>.And<BqlOperand<INLotSerialStatus.qtyOnHand, IBqlDecimal>.IsGreater<Zero>>>>, FbqlJoins.Inner<INSite>.On<INLocation.FK.Site>>, FbqlJoins.Inner<InventoryItem>.On<INLocationStatus.FK.InventoryItem>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<StoragePlaceEnq.StoragePlaceFilter.siteID>, Equal<INSite.siteID>>>>, And<BqlOperand<INLocationStatus.qtyOnHand, IBqlDecimal>.IsGreater<Zero>>>>.And<BqlOperand<INLocation.active, IBqlBool>.IsEqual<True>>>.Order<By<BqlField<INSite.siteCD, IBqlString>.Asc, BqlField<INLocation.locationCD, IBqlString>.Asc, BqlField<InventoryItem.inventoryCD, IBqlString>.Asc, BqlField<INLocationStatus.subItemID, IBqlInt>.Asc, BqlField<INLotSerialStatus.lotSerialNbr, IBqlString>.Asc, BqlField<INLocationStatus.qtyOnHand, IBqlDecimal>.Desc, BqlField<INLotSerialStatus.qtyOnHand, IBqlDecimal>.Desc>>, INLocation>.View((PXGraph) this);
        if (((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) this.Filter).Current.LocationID.HasValue)
          ((PXSelectBase<INLocation>) select).WhereAnd<Where<BqlOperand<Current<StoragePlaceEnq.StoragePlaceFilter.locationID>, IBqlInt>.IsEqual<INLocation.locationID>>>();
        if (((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) this.Filter).Current.InventoryID.HasValue)
          ((PXSelectBase<INLocation>) select).WhereAnd<Where<BqlOperand<Current<StoragePlaceEnq.StoragePlaceFilter.inventoryID>, IBqlInt>.IsEqual<InventoryItem.inventoryID>>>();
        if (PXAccess.FeatureInstalled<FeaturesSet.subItem>() && ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) this.Filter).Current.SubItemID.HasValue)
          ((PXSelectBase<INLocation>) select).WhereAnd<Where<BqlOperand<Current<StoragePlaceEnq.StoragePlaceFilter.subItemID>, IBqlInt>.IsEqual<INLocationStatus.subItemID>>>();
        if (!string.IsNullOrEmpty(((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) this.Filter).Current.LotSerialNbr))
          ((PXSelectBase<INLocation>) select).WhereAnd<Where<BqlOperand<Current<StoragePlaceEnq.StoragePlaceFilter.lotSerialNbr>, IBqlString>.IsEqual<INLotSerialStatus.lotSerialNbr>>>();
        bool cartsEnabled = this.AreCartsInUse();
        if (cartsEnabled)
        {
          ((PXSelectBase<INLocation>) select).Join<LeftJoin<INCartContentByLocation, On<INCartContentByLocation.FK.LocationStatus>>>();
          ((PXSelectBase<INLocation>) select).Join<LeftJoin<INCartContentByLotSerial, On<INCartContentByLotSerial.FK.LotSerialStatus>>>();
        }
        return Execute<PXResult<INLocation, INLocationStatus, INLotSerialStatus, INSite, InventoryItem>>((PXSelectBase) select, (Func<PXResult<INLocation, INLocationStatus, INLotSerialStatus, INSite, InventoryItem>, StoragePlaceStatus>) (record =>
        {
          INLocation inLocation1;
          INLocationStatus inLocationStatus1;
          INLotSerialStatus inLotSerialStatus1;
          INSite inSite1;
          InventoryItem inventoryItem1;
          record.Deconstruct(ref inLocation1, ref inLocationStatus1, ref inLotSerialStatus1, ref inSite1, ref inventoryItem1);
          INLocation inLocation2 = inLocation1;
          INLocationStatus inLocationStatus2 = inLocationStatus1;
          INLotSerialStatus inLotSerialStatus2 = inLotSerialStatus1;
          INSite inSite2 = inSite1;
          InventoryItem inventoryItem2 = inventoryItem1;
          INCartContentByLocation contentByLocation = cartsEnabled ? ((PXResult) record).GetItem<INCartContentByLocation>() : (INCartContentByLocation) null;
          INCartContentByLotSerial contentByLotSerial = cartsEnabled ? ((PXResult) record).GetItem<INCartContentByLotSerial>() : (INCartContentByLotSerial) null;
          StoragePlaceStatus storagePlaceStatus1 = new StoragePlaceStatus();
          storagePlaceStatus1.SiteID = inSite2.SiteID;
          storagePlaceStatus1.StorageID = inLocation2.LocationID;
          storagePlaceStatus1.StorageCD = inLocation2.LocationCD;
          storagePlaceStatus1.Descr = inLocation2.Descr;
          storagePlaceStatus1.InventoryID = inventoryItem2.InventoryID;
          storagePlaceStatus1.InventoryDescr = inventoryItem2.Descr;
          storagePlaceStatus1.SubItemID = inLocationStatus2.SubItemID;
          storagePlaceStatus1.LotSerialNbr = inLotSerialStatus2.LotSerialNbr;
          storagePlaceStatus1.ExpireDate = inLotSerialStatus2.ExpireDate;
          Decimal? nullable1;
          Decimal? nullable2;
          if (inLotSerialStatus2.LotSerialNbr == null)
          {
            nullable1 = inLocationStatus2.QtyOnHand;
            Decimal valueOrDefault = ((Decimal?) contentByLocation?.BaseQty).GetValueOrDefault();
            nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - valueOrDefault) : new Decimal?();
          }
          else
          {
            nullable1 = inLotSerialStatus2.QtyOnHand;
            Decimal valueOrDefault = ((Decimal?) contentByLotSerial?.BaseQty).GetValueOrDefault();
            nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - valueOrDefault) : new Decimal?();
          }
          storagePlaceStatus1.Qty = nullable2;
          Decimal valueOrDefault1;
          if (inLotSerialStatus2.LotSerialNbr == null)
          {
            Decimal? nullable3;
            if (contentByLocation == null)
            {
              nullable1 = new Decimal?();
              nullable3 = nullable1;
            }
            else
              nullable3 = contentByLocation.BaseQty;
            nullable1 = nullable3;
            valueOrDefault1 = nullable1.GetValueOrDefault();
          }
          else
          {
            Decimal? nullable4;
            if (contentByLotSerial == null)
            {
              nullable1 = new Decimal?();
              nullable4 = nullable1;
            }
            else
              nullable4 = contentByLotSerial.BaseQty;
            nullable1 = nullable4;
            valueOrDefault1 = nullable1.GetValueOrDefault();
          }
          storagePlaceStatus1.QtyPickedToCart = new Decimal?(valueOrDefault1);
          storagePlaceStatus1.BaseUnit = inventoryItem2.BaseUnit;
          storagePlaceStatus1.NoteID = inventoryItem2.NoteID;
          StoragePlaceStatus storagePlaceStatus2 = storagePlaceStatus1;
          PXDBLocalizableStringAttribute.CopyTranslations<InventoryItem.descr, StoragePlaceStatus.inventoryDescr>((PXGraph) this, (object) inventoryItem2, (object) storagePlaceStatus2);
          return storagePlaceStatus2;
        }));
      }
      FbqlSelect<SelectFromBase<INLocation, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLocationStatus>.On<INLocationStatus.FK.Location>>, FbqlJoins.Inner<INSite>.On<INLocation.FK.Site>>, FbqlJoins.Inner<InventoryItem>.On<INLocationStatus.FK.InventoryItem>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<StoragePlaceEnq.StoragePlaceFilter.siteID>, Equal<INSite.siteID>>>>, And<BqlOperand<INLocationStatus.qtyOnHand, IBqlDecimal>.IsGreater<Zero>>>>.And<BqlOperand<INLocation.active, IBqlBool>.IsEqual<True>>>.Order<By<BqlField<INSite.siteCD, IBqlString>.Asc, BqlField<INLocation.locationCD, IBqlString>.Asc, BqlField<InventoryItem.inventoryCD, IBqlString>.Asc, BqlField<INLocationStatus.subItemID, IBqlInt>.Asc, BqlField<INLocationStatus.qtyOnHand, IBqlDecimal>.Desc>>, INLocation>.View select1 = new FbqlSelect<SelectFromBase<INLocation, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLocationStatus>.On<INLocationStatus.FK.Location>>, FbqlJoins.Inner<INSite>.On<INLocation.FK.Site>>, FbqlJoins.Inner<InventoryItem>.On<INLocationStatus.FK.InventoryItem>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<StoragePlaceEnq.StoragePlaceFilter.siteID>, Equal<INSite.siteID>>>>, And<BqlOperand<INLocationStatus.qtyOnHand, IBqlDecimal>.IsGreater<Zero>>>>.And<BqlOperand<INLocation.active, IBqlBool>.IsEqual<True>>>.Order<By<BqlField<INSite.siteCD, IBqlString>.Asc, BqlField<INLocation.locationCD, IBqlString>.Asc, BqlField<InventoryItem.inventoryCD, IBqlString>.Asc, BqlField<INLocationStatus.subItemID, IBqlInt>.Asc, BqlField<INLocationStatus.qtyOnHand, IBqlDecimal>.Desc>>, INLocation>.View((PXGraph) this);
      if (((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) this.Filter).Current.LocationID.HasValue)
        ((PXSelectBase<INLocation>) select1).WhereAnd<Where<BqlOperand<Current<StoragePlaceEnq.StoragePlaceFilter.locationID>, IBqlInt>.IsEqual<INLocation.locationID>>>();
      if (((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) this.Filter).Current.InventoryID.HasValue)
        ((PXSelectBase<INLocation>) select1).WhereAnd<Where<BqlOperand<Current<StoragePlaceEnq.StoragePlaceFilter.inventoryID>, IBqlInt>.IsEqual<InventoryItem.inventoryID>>>();
      if (PXAccess.FeatureInstalled<FeaturesSet.subItem>() && ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) this.Filter).Current.SubItemID.HasValue)
        ((PXSelectBase<INLocation>) select1).WhereAnd<Where<BqlOperand<Current<StoragePlaceEnq.StoragePlaceFilter.subItemID>, IBqlInt>.IsEqual<INLocationStatus.subItemID>>>();
      bool areCartsInUse = this.AreCartsInUse();
      if (areCartsInUse)
        ((PXSelectBase<INLocation>) select1).Join<LeftJoin<INCartContentByLocation, On<INCartContentByLocation.FK.LocationStatus>>>();
      return Execute<PXResult<INLocation, INLocationStatus, INSite, InventoryItem>>((PXSelectBase) select1, (Func<PXResult<INLocation, INLocationStatus, INSite, InventoryItem>, StoragePlaceStatus>) (record =>
      {
        INLocation inLocation3;
        INLocationStatus inLocationStatus3;
        INSite inSite3;
        InventoryItem inventoryItem3;
        record.Deconstruct(ref inLocation3, ref inLocationStatus3, ref inSite3, ref inventoryItem3);
        INLocation inLocation4 = inLocation3;
        INLocationStatus inLocationStatus4 = inLocationStatus3;
        INSite inSite4 = inSite3;
        InventoryItem inventoryItem4 = inventoryItem3;
        INCartContentByLocation contentByLocation = areCartsInUse ? ((PXResult) record).GetItem<INCartContentByLocation>() : (INCartContentByLocation) null;
        StoragePlaceStatus storagePlaceStatus3 = new StoragePlaceStatus();
        storagePlaceStatus3.SiteID = inSite4.SiteID;
        storagePlaceStatus3.StorageID = inLocation4.LocationID;
        storagePlaceStatus3.StorageCD = inLocation4.LocationCD;
        storagePlaceStatus3.Descr = inLocation4.Descr;
        storagePlaceStatus3.InventoryID = inventoryItem4.InventoryID;
        storagePlaceStatus3.InventoryDescr = inventoryItem4.Descr;
        storagePlaceStatus3.SubItemID = inLocationStatus4.SubItemID;
        Decimal? nullable5 = inLocationStatus4.QtyOnHand;
        Decimal valueOrDefault = ((Decimal?) contentByLocation?.BaseQty).GetValueOrDefault();
        storagePlaceStatus3.Qty = nullable5.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - valueOrDefault) : new Decimal?();
        Decimal? nullable6;
        if (contentByLocation == null)
        {
          nullable5 = new Decimal?();
          nullable6 = nullable5;
        }
        else
          nullable6 = contentByLocation.BaseQty;
        nullable5 = nullable6;
        storagePlaceStatus3.QtyPickedToCart = new Decimal?(nullable5.GetValueOrDefault());
        storagePlaceStatus3.BaseUnit = inventoryItem4.BaseUnit;
        storagePlaceStatus3.NoteID = inventoryItem4.NoteID;
        StoragePlaceStatus storagePlaceStatus4 = storagePlaceStatus3;
        PXDBLocalizableStringAttribute.CopyTranslations<InventoryItem.descr, StoragePlaceStatus.inventoryDescr>((PXGraph) this, (object) inventoryItem4, (object) storagePlaceStatus4);
        return storagePlaceStatus4;
      }));
    }
    if (!(((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) this.Filter).Current.StorageType == "C"))
      throw new NotImplementedException();
    if (((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) this.Filter).Current.ExpandByLotSerialNbr.GetValueOrDefault())
    {
      FbqlSelect<SelectFromBase<INCart, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCartSplit>.On<INCartSplit.FK.Cart>>, FbqlJoins.Inner<INSite>.On<INCart.FK.Site>>, FbqlJoins.Inner<InventoryItem>.On<INCartSplit.FK.InventoryItem>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<StoragePlaceEnq.StoragePlaceFilter.siteID>, Equal<INSite.siteID>>>>, And<BqlOperand<INCartSplit.baseQty, IBqlDecimal>.IsGreater<Zero>>>>.And<BqlOperand<INCart.active, IBqlBool>.IsEqual<True>>>.Order<By<BqlField<INSite.siteCD, IBqlString>.Asc, BqlField<INCart.cartCD, IBqlString>.Asc, BqlField<InventoryItem.inventoryCD, IBqlString>.Asc, BqlField<INCartSplit.subItemID, IBqlInt>.Asc, BqlField<INCartSplit.lotSerialNbr, IBqlString>.Asc, BqlField<INCartSplit.baseQty, IBqlDecimal>.Desc>>, INCart>.View select = new FbqlSelect<SelectFromBase<INCart, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCartSplit>.On<INCartSplit.FK.Cart>>, FbqlJoins.Inner<INSite>.On<INCart.FK.Site>>, FbqlJoins.Inner<InventoryItem>.On<INCartSplit.FK.InventoryItem>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<StoragePlaceEnq.StoragePlaceFilter.siteID>, Equal<INSite.siteID>>>>, And<BqlOperand<INCartSplit.baseQty, IBqlDecimal>.IsGreater<Zero>>>>.And<BqlOperand<INCart.active, IBqlBool>.IsEqual<True>>>.Order<By<BqlField<INSite.siteCD, IBqlString>.Asc, BqlField<INCart.cartCD, IBqlString>.Asc, BqlField<InventoryItem.inventoryCD, IBqlString>.Asc, BqlField<INCartSplit.subItemID, IBqlInt>.Asc, BqlField<INCartSplit.lotSerialNbr, IBqlString>.Asc, BqlField<INCartSplit.baseQty, IBqlDecimal>.Desc>>, INCart>.View((PXGraph) this);
      int? nullable = ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) this.Filter).Current.CartID;
      if (nullable.HasValue)
        ((PXSelectBase<INCart>) select).WhereAnd<Where<BqlOperand<Current<StoragePlaceEnq.StoragePlaceFilter.cartID>, IBqlInt>.IsEqual<INCart.cartID>>>();
      nullable = ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) this.Filter).Current.InventoryID;
      if (nullable.HasValue)
        ((PXSelectBase<INCart>) select).WhereAnd<Where<BqlOperand<Current<StoragePlaceEnq.StoragePlaceFilter.inventoryID>, IBqlInt>.IsEqual<InventoryItem.inventoryID>>>();
      if (PXAccess.FeatureInstalled<FeaturesSet.subItem>())
      {
        nullable = ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) this.Filter).Current.SubItemID;
        if (nullable.HasValue)
          ((PXSelectBase<INCart>) select).WhereAnd<Where<BqlOperand<Current<StoragePlaceEnq.StoragePlaceFilter.subItemID>, IBqlInt>.IsEqual<INCartSplit.subItemID>>>();
      }
      if (!string.IsNullOrEmpty(((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) this.Filter).Current.LotSerialNbr))
        ((PXSelectBase<INCart>) select).WhereAnd<Where<BqlOperand<Current<StoragePlaceEnq.StoragePlaceFilter.lotSerialNbr>, IBqlString>.IsEqual<INCartSplit.lotSerialNbr>>>();
      return Execute<PXResult<INCart, INCartSplit, INSite, InventoryItem>>((PXSelectBase) select, (Func<PXResult<INCart, INCartSplit, INSite, InventoryItem>, StoragePlaceStatus>) (record =>
      {
        INCart inCart1;
        INCartSplit inCartSplit1;
        INSite inSite5;
        InventoryItem inventoryItem5;
        record.Deconstruct(ref inCart1, ref inCartSplit1, ref inSite5, ref inventoryItem5);
        INCart inCart2 = inCart1;
        INCartSplit inCartSplit2 = inCartSplit1;
        INSite inSite6 = inSite5;
        InventoryItem inventoryItem6 = inventoryItem5;
        StoragePlaceStatus storagePlaceStatus = new StoragePlaceStatus()
        {
          SiteID = inSite6.SiteID,
          StorageID = inCart2.CartID,
          StorageCD = inCart2.CartCD,
          Descr = inCart2.Descr,
          InventoryID = inventoryItem6.InventoryID,
          InventoryDescr = inventoryItem6.Descr,
          SubItemID = inCartSplit2.SubItemID,
          LotSerialNbr = inCartSplit2.LotSerialNbr,
          ExpireDate = inCartSplit2.ExpireDate,
          Qty = inCartSplit2.BaseQty,
          BaseUnit = inventoryItem6.BaseUnit,
          NoteID = inventoryItem6.NoteID
        };
        PXDBLocalizableStringAttribute.CopyTranslations<InventoryItem.descr, StoragePlaceStatus.inventoryDescr>((PXGraph) this, (object) inventoryItem6, (object) storagePlaceStatus);
        return storagePlaceStatus;
      }));
    }
    FbqlSelect<SelectFromBase<INCart, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCartSplit>.On<INCartSplit.FK.Cart>>, FbqlJoins.Inner<INSite>.On<INCart.FK.Site>>, FbqlJoins.Inner<InventoryItem>.On<INCartSplit.FK.InventoryItem>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<StoragePlaceEnq.StoragePlaceFilter.siteID>, Equal<INSite.siteID>>>>, And<BqlOperand<INCartSplit.baseQty, IBqlDecimal>.IsGreater<Zero>>>>.And<BqlOperand<INCart.active, IBqlBool>.IsEqual<True>>>.Aggregate<To<GroupBy<INSite.siteCD>, GroupBy<INCart.cartCD>, GroupBy<INCartSplit.inventoryID>, GroupBy<INCartSplit.subItemID>, Sum<INCartSplit.baseQty>>>.Having<BqlAggregatedOperand<Sum<INCartSplit.qty>, IBqlDecimal>.IsGreater<Zero>>.Order<By<BqlField<INSite.siteCD, IBqlString>.Asc, BqlField<INCart.cartCD, IBqlString>.Asc, BqlField<InventoryItem.inventoryCD, IBqlString>.Asc, BqlField<INCartSplit.subItemID, IBqlInt>.Asc, BqlField<INCartSplit.baseQty, IBqlDecimal>.Desc>>, INCart>.View select2 = new FbqlSelect<SelectFromBase<INCart, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCartSplit>.On<INCartSplit.FK.Cart>>, FbqlJoins.Inner<INSite>.On<INCart.FK.Site>>, FbqlJoins.Inner<InventoryItem>.On<INCartSplit.FK.InventoryItem>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<StoragePlaceEnq.StoragePlaceFilter.siteID>, Equal<INSite.siteID>>>>, And<BqlOperand<INCartSplit.baseQty, IBqlDecimal>.IsGreater<Zero>>>>.And<BqlOperand<INCart.active, IBqlBool>.IsEqual<True>>>.Aggregate<To<GroupBy<INSite.siteCD>, GroupBy<INCart.cartCD>, GroupBy<INCartSplit.inventoryID>, GroupBy<INCartSplit.subItemID>, Sum<INCartSplit.baseQty>>>.Having<BqlAggregatedOperand<Sum<INCartSplit.qty>, IBqlDecimal>.IsGreater<Zero>>.Order<By<BqlField<INSite.siteCD, IBqlString>.Asc, BqlField<INCart.cartCD, IBqlString>.Asc, BqlField<InventoryItem.inventoryCD, IBqlString>.Asc, BqlField<INCartSplit.subItemID, IBqlInt>.Asc, BqlField<INCartSplit.baseQty, IBqlDecimal>.Desc>>, INCart>.View((PXGraph) this);
    int? nullable7 = ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) this.Filter).Current.CartID;
    if (nullable7.HasValue)
      ((PXSelectBase<INCart>) select2).WhereAnd<Where<BqlOperand<Current<StoragePlaceEnq.StoragePlaceFilter.cartID>, IBqlInt>.IsEqual<INCart.cartID>>>();
    nullable7 = ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) this.Filter).Current.InventoryID;
    if (nullable7.HasValue)
      ((PXSelectBase<INCart>) select2).WhereAnd<Where<BqlOperand<Current<StoragePlaceEnq.StoragePlaceFilter.inventoryID>, IBqlInt>.IsEqual<InventoryItem.inventoryID>>>();
    if (PXAccess.FeatureInstalled<FeaturesSet.subItem>())
    {
      nullable7 = ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) this.Filter).Current.SubItemID;
      if (nullable7.HasValue)
        ((PXSelectBase<INCart>) select2).WhereAnd<Where<BqlOperand<Current<StoragePlaceEnq.StoragePlaceFilter.subItemID>, IBqlInt>.IsEqual<INCartSplit.subItemID>>>();
    }
    return Execute<PXResult<INCart, INCartSplit, INSite, InventoryItem>>((PXSelectBase) select2, (Func<PXResult<INCart, INCartSplit, INSite, InventoryItem>, StoragePlaceStatus>) (record =>
    {
      INCart inCart3;
      INCartSplit inCartSplit3;
      INSite inSite7;
      InventoryItem inventoryItem7;
      record.Deconstruct(ref inCart3, ref inCartSplit3, ref inSite7, ref inventoryItem7);
      INCart inCart4 = inCart3;
      INCartSplit inCartSplit4 = inCartSplit3;
      INSite inSite8 = inSite7;
      InventoryItem inventoryItem8 = inventoryItem7;
      StoragePlaceStatus storagePlaceStatus = new StoragePlaceStatus()
      {
        SiteID = inSite8.SiteID,
        StorageID = inCart4.CartID,
        StorageCD = inCart4.CartCD,
        Descr = inCart4.Descr,
        InventoryID = inventoryItem8.InventoryID,
        InventoryDescr = inventoryItem8.Descr,
        SubItemID = inCartSplit4.SubItemID,
        Qty = inCartSplit4.BaseQty,
        BaseUnit = inventoryItem8.BaseUnit,
        NoteID = inventoryItem8.NoteID
      };
      PXDBLocalizableStringAttribute.CopyTranslations<InventoryItem.descr, StoragePlaceStatus.inventoryDescr>((PXGraph) this, (object) inventoryItem8, (object) storagePlaceStatus);
      return storagePlaceStatus;
    }));

    IEnumerable Execute<TResult>(PXSelectBase select, Func<TResult, StoragePlaceStatus> convertor) where TResult : PXResult
    {
      // ISSUE: method pointer
      PXFilterRow[] array = ((IEnumerable) PXView.Filters).Cast<PXFilterRow>().Select<PXFilterRow, PXFilterRow>(new Func<PXFilterRow, PXFilterRow>((object) this, __methodptr(\u003CStorages\u003Eg__RemapFilterField\u007C2_1))).Where<PXFilterRow>((Func<PXFilterRow, bool>) (fr => fr != null)).ToArray<PXFilterRow>();
      bool flag = array.Length < PXView.Filters.Length;
      PXDelegateResult pxDelegateResult = new PXDelegateResult()
      {
        IsResultSorted = true,
        IsResultTruncated = !flag
      };
      int startRow = pxDelegateResult.IsResultTruncated ? PXView.StartRow : 0;
      int maximumRows = pxDelegateResult.IsResultTruncated ? PXView.MaximumRows : 0;
      int num = 0;
      foreach (TResult result in select.View.Select(PXView.Currents, PXView.Parameters, new object[0], new string[0], new bool[0], array, ref startRow, maximumRows, ref num))
      {
        StoragePlaceStatus storagePlaceStatus = convertor(result);
        ((List<object>) pxDelegateResult).Add((object) storagePlaceStatus);
        GraphHelper.Hold(((PXSelectBase) this.storages).Cache, (object) storagePlaceStatus);
      }
      if (pxDelegateResult.IsResultTruncated)
        PXView.StartRow = 0;
      else if (PXView.ReverseOrder)
        ((List<object>) pxDelegateResult).Reverse();
      return (IEnumerable) pxDelegateResult;
    }
  }

  protected virtual IEnumerable<Type> GridVirtualFields
  {
    get
    {
      return (IEnumerable<Type>) new Type[3]
      {
        typeof (StoragePlaceStatus.qty),
        typeof (StoragePlaceStatus.qtyPickedToCart),
        typeof (StoragePlaceStatus.expireDate)
      };
    }
  }

  protected virtual bool AreCartsInUse()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.wMSCartTracking>() && ((IQueryable<PXResult<INCartSplit>>) PXSelectBase<INCartSplit, PXViewOf<INCartSplit>.BasedOn<SelectFromBase<INCartSplit, TypeArrayOf<IFbqlJoin>.Empty>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Any<PXResult<INCartSplit>>();
  }

  protected virtual void _(
    Events.RowUpdated<StoragePlaceEnq.StoragePlaceFilter> e)
  {
    ((PXSelectBase) this.storages).Cache.Clear();
  }

  protected virtual void _(
    Events.RowInserted<StoragePlaceEnq.StoragePlaceFilter> e)
  {
    ((PXSelectBase) this.storages).Cache.Clear();
  }

  protected virtual void _(
    Events.RowSelected<StoragePlaceEnq.StoragePlaceFilter> e)
  {
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.AdjustUI(((PXSelectBase) this.storages).Cache, (object) null).For<StoragePlaceStatus.qtyPickedToCart>((Action<PXUIFieldAttribute>) (a => a.Visible = e.Row?.StorageType == "L"));
    chained = chained.For<StoragePlaceStatus.lotSerialNbr>((Action<PXUIFieldAttribute>) (a => a.Visible = ((bool?) e.Row?.ExpandByLotSerialNbr).GetValueOrDefault()));
    chained.SameFor<StoragePlaceStatus.expireDate>();
  }

  public virtual bool IsDirty => false;

  [PXHidden]
  public class StoragePlaceFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [Site(Required = true)]
    public int? SiteID { get; set; }

    [PXBool]
    [PXUnboundDefault(false)]
    [PXUIField]
    public virtual bool? ExpandByLotSerialNbr { get; set; }

    [Location(typeof (StoragePlaceEnq.StoragePlaceFilter.siteID))]
    [PXUIVisible(typeof (Where<Not<FeatureInstalled<FeaturesSet.wMSCartTracking>>>))]
    [PXFormula(typeof (Default<StoragePlaceEnq.StoragePlaceFilter.siteID>))]
    public int? StorageID
    {
      get => this.LocationID;
      set => this.LocationID = value;
    }

    [Location(typeof (StoragePlaceEnq.StoragePlaceFilter.siteID))]
    [PXUIEnabled(typeof (BqlOperand<StoragePlaceEnq.StoragePlaceFilter.storageType, IBqlString>.IsEqual<StoragePlaceEnq.StoragePlaceFilter.storageType.locations>))]
    [PXUIVisible(typeof (Where<FeatureInstalled<FeaturesSet.wMSCartTracking>>))]
    [PXFormula(typeof (Default<StoragePlaceEnq.StoragePlaceFilter.siteID, StoragePlaceEnq.StoragePlaceFilter.storageType>))]
    public virtual int? LocationID { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "Cart ID")]
    [PXUIEnabled(typeof (BqlOperand<StoragePlaceEnq.StoragePlaceFilter.storageType, IBqlString>.IsEqual<StoragePlaceEnq.StoragePlaceFilter.storageType.carts>))]
    [PXUIVisible(typeof (Where<FeatureInstalled<FeaturesSet.wMSCartTracking>>))]
    [PXSelector(typeof (SearchFor<INCart.cartID>.Where<BqlOperand<INCart.active, IBqlBool>.IsEqual<True>>), SubstituteKey = typeof (INCart.cartCD), DescriptionField = typeof (INCart.descr))]
    [PXFormula(typeof (Default<StoragePlaceEnq.StoragePlaceFilter.siteID, StoragePlaceEnq.StoragePlaceFilter.storageType>))]
    public int? CartID { get; set; }

    [StockItem]
    public virtual int? InventoryID { get; set; }

    [SubItem(typeof (StoragePlaceEnq.StoragePlaceFilter.inventoryID))]
    [PXFormula(typeof (Default<StoragePlaceEnq.StoragePlaceFilter.inventoryID>))]
    public virtual int? SubItemID { get; set; }

    [LotSerialNbr]
    [PXUIEnabled(typeof (StoragePlaceEnq.StoragePlaceFilter.expandByLotSerialNbr))]
    [PXFormula(typeof (Default<StoragePlaceEnq.StoragePlaceFilter.inventoryID>))]
    public virtual string LotSerialNbr { get; set; }

    [PXString]
    [StoragePlaceEnq.StoragePlaceFilter.storageType.List]
    [PXUnboundDefault("L")]
    [PXUIField(DisplayName = "Show Storages", FieldClass = "Carts")]
    [PXUIVisible(typeof (Where<FeatureInstalled<FeaturesSet.wMSCartTracking>>))]
    public virtual string StorageType { get; set; }

    [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
    [PXBool]
    [PXUnboundDefault(true)]
    [PXUIField(DisplayName = "Show Locations", FieldClass = "Carts")]
    public virtual bool? ShowLocations { get; set; }

    [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
    [PXBool]
    [PXUnboundDefault(typeof (FeatureInstalled<FeaturesSet.wMSCartTracking>))]
    [PXUIField(DisplayName = "Show Carts", FieldClass = "Carts")]
    public virtual bool? ShowCarts { get; set; }

    public abstract class siteID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      StoragePlaceEnq.StoragePlaceFilter.siteID>
    {
    }

    public abstract class expandByLotSerialNbr : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      StoragePlaceEnq.StoragePlaceFilter.expandByLotSerialNbr>
    {
    }

    public abstract class storageID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      StoragePlaceEnq.StoragePlaceFilter.storageID>
    {
    }

    public abstract class locationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      StoragePlaceEnq.StoragePlaceFilter.locationID>
    {
    }

    public abstract class cartID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      StoragePlaceEnq.StoragePlaceFilter.cartID>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      StoragePlaceEnq.StoragePlaceFilter.inventoryID>
    {
    }

    public abstract class subItemID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      StoragePlaceEnq.StoragePlaceFilter.subItemID>
    {
    }

    public abstract class lotSerialNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      StoragePlaceEnq.StoragePlaceFilter.lotSerialNbr>
    {
    }

    public abstract class storageType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      StoragePlaceEnq.StoragePlaceFilter.storageType>
    {
      public const string Carts = "C";
      public const string Locations = "L";

      [PXLocalizable]
      public static class DisplayNames
      {
        public const string Carts = "Cart";
        public const string Locations = "Location";
      }

      public class carts : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        StoragePlaceEnq.StoragePlaceFilter.storageType.carts>
      {
        public carts()
          : base("C")
        {
        }
      }

      public class locations : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        StoragePlaceEnq.StoragePlaceFilter.storageType.locations>
      {
        public locations()
          : base("L")
        {
        }
      }

      public class List : PXStringListAttribute
      {
        public List()
          : base(new Tuple<string, string>[2]
          {
            PXStringListAttribute.Pair("L", "Location"),
            PXStringListAttribute.Pair("C", "Cart")
          })
        {
        }
      }
    }

    public abstract class showLocations : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      StoragePlaceEnq.StoragePlaceFilter.showLocations>
    {
    }

    public abstract class showCarts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      StoragePlaceEnq.StoragePlaceFilter.showCarts>
    {
    }
  }
}
