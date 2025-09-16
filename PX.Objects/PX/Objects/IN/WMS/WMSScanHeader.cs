// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.WMSScanHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN.WMS;

public sealed class WMSScanHeader : PXCacheExtension<
#nullable disable
QtyScanHeader, ScanHeader>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.advancedFulfillment>();

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
  public string RefNbr { get; set; }

  [Site(Enabled = false)]
  public int? SiteID { get; set; }

  [Location(typeof (WMSScanHeader.siteID), Enabled = false)]
  public int? LocationID { get; set; }

  [Inventory(Enabled = false)]
  public int? InventoryID { get; set; }

  [SubItem(typeof (WMSScanHeader.inventoryID), Enabled = false)]
  public int? SubItemID { get; set; }

  [INUnit(typeof (WMSScanHeader.inventoryID), Enabled = false)]
  public string UOM { get; set; }

  [PXQuantity(typeof (WMSScanHeader.uOM), typeof (WMSScanHeader.baseQty), HandleEmptyKey = true)]
  [PXUnboundDefault(TypeCode.Decimal, "1")]
  public Decimal? Qty { get; set; }

  [PXDecimal(6)]
  public Decimal? BaseQty { get; set; }

  [PXString]
  public string LotSerialNbr { get; set; }

  [PXDate]
  public DateTime? ExpireDate { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Remove Mode", Enabled = false)]
  [PXUIVisible(typeof (WMSScanHeader.remove))]
  public bool? Remove { get; set; }

  [PXDate]
  [PXUnboundDefault(typeof (AccessInfo.businessDate))]
  public DateTime? TranDate { get; set; }

  /// <exclude />
  [PXString]
  public string TranType { get; set; }

  [PXShort]
  public short? InventoryMultiplicator { get; set; }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WMSScanHeader.refNbr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WMSScanHeader.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WMSScanHeader.locationID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WMSScanHeader.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WMSScanHeader.subItemID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WMSScanHeader.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  WMSScanHeader.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  WMSScanHeader.baseQty>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WMSScanHeader.lotSerialNbr>
  {
  }

  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  WMSScanHeader.expireDate>
  {
  }

  public abstract class remove : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WMSScanHeader.remove>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  WMSScanHeader.tranDate>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WMSScanHeader.tranType>
  {
  }

  public abstract class inventoryMultiplicator : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    WMSScanHeader.inventoryMultiplicator>
  {
  }
}
