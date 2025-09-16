// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.LSMasterDummy
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN.WMS;

[PXHidden]
public class LSMasterDummy : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILSMaster,
  IItemPlanMaster
{
  [Site]
  public int? SiteID { get; set; }

  [Location(typeof (LSMasterDummy.siteID))]
  public virtual int? LocationID { get; set; }

  [StockItem]
  public virtual int? InventoryID { get; set; }

  [SubItem(typeof (LSMasterDummy.inventoryID))]
  public virtual int? SubItemID { get; set; }

  [INLotSerialNbr(typeof (LSMasterDummy.inventoryID), typeof (LSMasterDummy.subItemID), typeof (LSMasterDummy.locationID), typeof (CostCenter.freeStock))]
  public virtual 
  #nullable disable
  string LotSerialNbr { get; set; }

  [INExpireDate(typeof (LSMasterDummy.inventoryID))]
  public virtual DateTime? ExpireDate { get; set; }

  [INUnit(typeof (LSMasterDummy.inventoryID))]
  public virtual string UOM { get; set; }

  [PXQuantity(typeof (LSMasterDummy.uOM), typeof (LSMasterDummy.baseQty), HandleEmptyKey = true)]
  public virtual Decimal? Qty { get; set; }

  [PXDecimal(6)]
  public virtual Decimal? BaseQty { get; set; }

  [PXDate]
  public virtual DateTime? TranDate { get; set; }

  [PXString]
  public string TranType { get; set; }

  [PXShort]
  public virtual short? InvtMult { get; set; }

  public int? ProjectID { get; set; }

  public int? TaskID { get; set; }

  public bool? IsIntercompany => new bool?(false);

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LSMasterDummy.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LSMasterDummy.locationID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LSMasterDummy.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LSMasterDummy.subItemID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LSMasterDummy.lotSerialNbr>
  {
  }

  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  LSMasterDummy.expireDate>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LSMasterDummy.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  LSMasterDummy.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  LSMasterDummy.baseQty>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  LSMasterDummy.tranDate>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LSMasterDummy.tranType>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  LSMasterDummy.invtMult>
  {
  }
}
