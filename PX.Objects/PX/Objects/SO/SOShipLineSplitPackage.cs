// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipLineSplitPackage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName]
public class SOShipLineSplitPackage : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXDBDefault(typeof (SOShipment.shipmentNbr))]
  public virtual 
  #nullable disable
  string ShipmentNbr { get; set; }

  [PXDBInt]
  [PXFormula(typeof (Selector<SOShipLineSplitPackage.shipmentSplitLineNbr, SOShipLineSplit.lineNbr>))]
  public virtual int? ShipmentLineNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Shipment Split Line Nbr.")]
  [PXParent(typeof (SOShipLineSplitPackage.FK.ShipmentLineSplit))]
  [PXSelector(typeof (Search<SOShipLineSplit.splitLineNbr, Where<SOShipLineSplit.shipmentNbr, Equal<Current<SOPackageDetail.shipmentNbr>>, And<SOShipLineSplit.packedQty, Less<SOShipLineSplit.qty>>>>), new Type[] {typeof (SOShipLineSplit.lineNbr), typeof (SOShipLineSplit.splitLineNbr), typeof (SOShipLineSplit.origOrderType), typeof (SOShipLineSplit.origOrderNbr), typeof (SOShipLineSplit.inventoryID), typeof (SOShipLineSplit.lotSerialNbr), typeof (SOShipLineSplit.qty), typeof (SOShipLineSplit.packedQty), typeof (SOShipLineSplit.uOM)}, DirtyRead = true)]
  public virtual int? ShipmentSplitLineNbr { get; set; }

  [PXDBInt]
  [PXDBDefault(typeof (SOPackageDetail.lineNbr))]
  [PXParent(typeof (SOShipLineSplitPackage.FK.PackageDetail))]
  public virtual int? PackageLineNbr { get; set; }

  [Inventory(Enabled = false)]
  [PXFormula(typeof (Selector<SOShipLineSplitPackage.shipmentSplitLineNbr, SOShipLineSplit.inventoryID>))]
  public virtual int? InventoryID { get; set; }

  [SubItem(typeof (SOShipLineSplitPackage.inventoryID), Enabled = false)]
  [PXFormula(typeof (Selector<SOShipLineSplitPackage.shipmentSplitLineNbr, SOShipLineSplit.subItemID>))]
  public virtual int? SubItemID { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Lot/Serial Nbr.", FieldClass = "LotSerial", Enabled = false)]
  [PXFormula(typeof (Selector<SOShipLineSplitPackage.shipmentSplitLineNbr, SOShipLineSplit.lotSerialNbr>))]
  public virtual string LotSerialNbr { get; set; }

  [INUnit(typeof (SOShipLineSplitPackage.inventoryID), DisplayName = "UOM", Enabled = false)]
  [PXFormula(typeof (Selector<SOShipLineSplitPackage.shipmentSplitLineNbr, SOShipLineSplit.uOM>))]
  public virtual string UOM { get; set; }

  [PXDBQuantity(typeof (SOShipLineSplitPackage.uOM), typeof (SOShipLineSplitPackage.basePackedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? PackedQty { get; set; }

  [PXDBDecimal(6)]
  public virtual Decimal? BasePackedQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnitPriceFactor { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? WeightFactor { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<SOShipLineSplitPackage>.By<SOShipLineSplitPackage.shipmentNbr, SOShipLineSplitPackage.shipmentLineNbr, SOShipLineSplitPackage.shipmentSplitLineNbr, SOShipLineSplitPackage.packageLineNbr>
  {
    public static SOShipLineSplitPackage Find(
      PXGraph graph,
      string shipmentNbr,
      int? shipmentLineNbr,
      int? shipmentSplitLineNbr,
      int? packageLineNbr,
      PKFindOptions options = 0)
    {
      return (SOShipLineSplitPackage) PrimaryKeyOf<SOShipLineSplitPackage>.By<SOShipLineSplitPackage.shipmentNbr, SOShipLineSplitPackage.shipmentLineNbr, SOShipLineSplitPackage.shipmentSplitLineNbr, SOShipLineSplitPackage.packageLineNbr>.FindBy(graph, (object) shipmentNbr, (object) shipmentLineNbr, (object) shipmentSplitLineNbr, (object) packageLineNbr, options);
    }
  }

  public static class FK
  {
    public class Shipment : 
      PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentNbr>.ForeignKeyOf<SOShipLineSplitPackage>.By<SOShipLineSplitPackage.shipmentNbr>
    {
    }

    public class ShipmentLine : 
      PrimaryKeyOf<SOShipLine>.By<SOShipLine.shipmentNbr, SOShipLine.lineNbr>.ForeignKeyOf<SOShipLineSplitPackage>.By<SOShipLineSplitPackage.shipmentNbr, SOShipLineSplitPackage.shipmentLineNbr>
    {
    }

    public class ShipmentLineSplit : 
      PrimaryKeyOf<SOShipLineSplit>.By<SOShipLineSplit.shipmentNbr, SOShipLineSplit.lineNbr, SOShipLineSplit.splitLineNbr>.ForeignKeyOf<SOShipLineSplitPackage>.By<SOShipLineSplitPackage.shipmentNbr, SOShipLineSplitPackage.shipmentLineNbr, SOShipLineSplitPackage.shipmentSplitLineNbr>
    {
    }

    public class PackageDetail : 
      PrimaryKeyOf<SOPackageDetail>.By<SOPackageDetail.shipmentNbr, SOPackageDetail.lineNbr>.ForeignKeyOf<SOShipLineSplitPackage>.By<SOShipLineSplitPackage.shipmentNbr, SOShipLineSplitPackage.packageLineNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<SOShipLineSplitPackage>.By<SOShipLineSplitPackage.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<SOShipLineSplitPackage>.By<SOShipLineSplitPackage.subItemID>
    {
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLineSplitPackage.recordID>
  {
  }

  public abstract class shipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplitPackage.shipmentNbr>
  {
  }

  public abstract class shipmentLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipLineSplitPackage.shipmentLineNbr>
  {
  }

  public abstract class shipmentSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipLineSplitPackage.shipmentSplitLineNbr>
  {
  }

  public abstract class packageLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipLineSplitPackage.packageLineNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLineSplitPackage.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLineSplitPackage.subItemID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplitPackage.lotSerialNbr>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLineSplitPackage.uOM>
  {
  }

  public abstract class packedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipLineSplitPackage.packedQty>
  {
  }

  public abstract class basePackedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipLineSplitPackage.basePackedQty>
  {
  }

  public abstract class unitPriceFactor : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipLineSplitPackage.unitPriceFactor>
  {
  }

  public abstract class weightFactor : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipLineSplitPackage.weightFactor>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOShipLineSplitPackage.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplitPackage.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipLineSplitPackage.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOShipLineSplitPackage.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplitPackage.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipLineSplitPackage.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOShipLineSplitPackage.Tstamp>
  {
  }
}
