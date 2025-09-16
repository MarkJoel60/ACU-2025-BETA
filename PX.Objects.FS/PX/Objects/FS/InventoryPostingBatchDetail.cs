// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.InventoryPostingBatchDetail
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXProjection(typeof (Select2<FSAppointmentDet, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentDet.appointmentID>>, InnerJoin<FSPostInfo, On<FSAppointmentDet.postID, Equal<FSPostInfo.postID>>, InnerJoin<FSPostDet, On<FSPostDet.postID, Equal<FSPostInfo.postID>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.billCustomerID>>, InnerJoin<FSAddress, On<FSAddress.addressID, Equal<FSServiceOrder.serviceOrderAddressID>>, LeftJoin<FSGeoZonePostalCode, On<FSGeoZonePostalCode.postalCode, Equal<FSAddress.postalCode>>, LeftJoin<FSGeoZone, On<FSGeoZone.geoZoneID, Equal<FSGeoZonePostalCode.geoZoneID>>>>>>>>>>, Where<FSAppointmentDet.lineType, Equal<ListField_LineType_Pickup_Delivery.Pickup_Delivery>>>))]
[Serializable]
public class InventoryPostingBatchDetail : PostingBatchDetail
{
  [PXDBInt(BqlField = typeof (FSAppointmentDet.appDetID))]
  public virtual int? AppointmentInventoryItemID { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointmentDet.sODetID))]
  [PXUIField(DisplayName = "Service Order Detail Ref. Nbr.")]
  [PXSelector(typeof (Search<FSSODet.sODetID, Where<FSSODet.sOID, Equal<Current<PostingBatchDetail.appointmentID>>, And<FSSODet.status, NotEqual<FSSODet.ListField_Status_SODet.Canceled>>>>), SubstituteKey = typeof (FSSODet.lineRef))]
  public virtual int? SODetID { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointmentDet.inventoryID))]
  [PXUIField(DisplayName = "Inventory ID")]
  [PXSelector(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID>), SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  public virtual int? InventoryID { get; set; }

  public abstract class appointmentInventoryItemID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    InventoryPostingBatchDetail.appointmentInventoryItemID>
  {
  }

  public abstract class sODetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryPostingBatchDetail.sODetID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryPostingBatchDetail.inventoryID>
  {
  }
}
