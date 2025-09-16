// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPickingWorksheetShipment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName]
[PXProjection(typeof (SelectFrom<SOPickingWorksheetShipment, TypeArrayOf<IFbqlJoin>.Empty>.InnerJoin<SOShipment>.On<SOPickingWorksheetShipment.FK.Shipment>), new Type[] {typeof (SOPickingWorksheetShipment)})]
public class SOPickingWorksheetShipment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXDBDefault(typeof (SOPickingWorksheet.worksheetNbr))]
  [PXParent(typeof (SOPickingWorksheetShipment.FK.Worksheet))]
  public virtual 
  #nullable disable
  string WorksheetNbr { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXSelector(typeof (SearchFor<SOShipment.shipmentNbr>))]
  [PXParent(typeof (SOPickingWorksheetShipment.FK.Shipment))]
  public virtual string ShipmentNbr { get; set; }

  [PXBool]
  [PXDBCalced(typeof (BqlOperand<True, IBqlBool>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.currentWorksheetNbr, IsNull>>>>.Or<BqlOperand<SOShipment.currentWorksheetNbr, IBqlString>.IsNotEqual<SOPickingWorksheetShipment.worksheetNbr>>>.Else<False>), typeof (bool))]
  [PXUIField(DisplayName = "Unlinked", Enabled = false)]
  public virtual bool? Unlinked { get; set; }

  [PXDBString(1, IsFixed = true, BqlTable = typeof (SOShipment))]
  [PXUIField]
  [SOShipmentStatus.List]
  public virtual string Status { get; set; }

  [PXDBBool(BqlTable = typeof (SOShipment))]
  [PXUIField(DisplayName = "Picked", Enabled = false)]
  public virtual bool? Picked { get; set; }

  [PXDBBool(BqlTable = typeof (SOShipment))]
  [PXUIField(DisplayName = "Picked via Worksheet", Enabled = false)]
  public virtual bool? PickedViaWorksheet { get; set; }

  [PXDBQuantity(BqlTable = typeof (SOShipment))]
  [PXUIField]
  public virtual Decimal? PickedQty { get; set; }

  [PXDBQuantity(BqlTable = typeof (SOShipment))]
  [PXUIField]
  public virtual Decimal? ShipmentQty { get; set; }

  [PXDBDecimal(6, BqlTable = typeof (SOShipment))]
  [PXUIField(DisplayName = "Shipped Weight", Enabled = false)]
  public virtual Decimal? ShipmentWeight { get; set; }

  [PXDBDecimal(6, BqlTable = typeof (SOShipment))]
  [PXUIField(DisplayName = "Shipped Volume", Enabled = false)]
  public virtual Decimal? ShipmentVolume { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "Created At", Enabled = false, IsReadOnly = true)]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By", Enabled = false, IsReadOnly = true)]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "Last Modified At", Enabled = false, IsReadOnly = true)]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<SOPickingWorksheetShipment>.By<SOPickingWorksheetShipment.worksheetNbr, SOPickingWorksheetShipment.shipmentNbr>
  {
    public static SOPickingWorksheetShipment Find(
      PXGraph graph,
      string worksheetNbr,
      string shipmentNbr,
      PKFindOptions options = 0)
    {
      return (SOPickingWorksheetShipment) PrimaryKeyOf<SOPickingWorksheetShipment>.By<SOPickingWorksheetShipment.worksheetNbr, SOPickingWorksheetShipment.shipmentNbr>.FindBy(graph, (object) worksheetNbr, (object) shipmentNbr, options);
    }
  }

  public static class FK
  {
    public class Worksheet : 
      PrimaryKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.worksheetNbr>.ForeignKeyOf<SOPickingWorksheetShipment>.By<SOPickingWorksheetShipment.worksheetNbr>
    {
    }

    public class Shipment : 
      PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentNbr>.ForeignKeyOf<SOPickingWorksheetShipment>.By<SOPickingWorksheetShipment.shipmentNbr>
    {
    }
  }

  public abstract class worksheetNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickingWorksheetShipment.worksheetNbr>
  {
  }

  public abstract class shipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickingWorksheetShipment.shipmentNbr>
  {
  }

  public abstract class unlinked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOPickingWorksheetShipment.unlinked>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPickingWorksheetShipment.status>
  {
  }

  public abstract class picked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOPickingWorksheetShipment.picked>
  {
  }

  public abstract class pickedViaWorksheet : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickingWorksheetShipment.pickedViaWorksheet>
  {
  }

  public abstract class pickedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPickingWorksheetShipment.pickedQty>
  {
  }

  public abstract class shipmentQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPickingWorksheetShipment.shipmentQty>
  {
  }

  public abstract class shipmentWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPickingWorksheetShipment.shipmentWeight>
  {
  }

  public abstract class shipmentVolume : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPickingWorksheetShipment.shipmentVolume>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    SOPickingWorksheetShipment.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOPickingWorksheetShipment.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickingWorksheetShipment.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickingWorksheetShipment.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOPickingWorksheetShipment.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickingWorksheetShipment.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickingWorksheetShipment.lastModifiedDateTime>
  {
  }
}
