// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSDetailFSLogAction
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXProjection(typeof (Select<FSAppointmentDet, Where<FSAppointmentDet.lineType, Equal<FSLineType.Service>, And<FSAppointmentDet.isCanceledNotPerformed, NotEqual<True>>>>))]
[Serializable]
public class FSDetailFSLogAction : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Start>, And<Current<FSLogActionFilter.type>, Equal<FSLogTypeAction.SrvBasedOnAssignment>>>))]
  public virtual bool? Selected { get; set; }

  [PXDBString(4, IsKey = true, IsFixed = true, BqlField = typeof (FSAppointmentDet.srvOrdType))]
  [PXUIField(DisplayName = "Service Order Type", Visible = false, Enabled = false)]
  [PXDefault(typeof (FSAppointment.srvOrdType))]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType>), CacheGlobal = true)]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "", BqlField = typeof (FSAppointmentDet.refNbr))]
  [PXUIField(DisplayName = "Appointment Nbr.", Visible = false, Enabled = false)]
  [PXDBDefault(typeof (FSAppointment.refNbr), DefaultForUpdate = false)]
  [PXParent(typeof (FSDetailFSLogAction.FK.Appointment))]
  public virtual string RefNbr { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointmentDet.appointmentID))]
  [PXDBDefault(typeof (FSAppointment.appointmentID))]
  [PXUIField(DisplayName = "Appointment Nbr.")]
  public virtual int? AppointmentID { get; set; }

  [PXDBIdentity(BqlField = typeof (FSAppointmentDet.appDetID))]
  public virtual int? AppDetID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (FSAppointmentDet.lineNbr))]
  [PXLineNbr(typeof (FSAppointment.lineCntr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointmentDet.sODetID))]
  [PXCheckUnique(new Type[] {}, Where = typeof (Where<FSDetailFSLogAction.appointmentID, Equal<Current<FSAppointment.appointmentID>>>))]
  [PXUIField(DisplayName = "Service Order Detail Ref. Nbr.", Visible = false)]
  [FSSelectorSODetID]
  public virtual int? SODetID { get; set; }

  [PXDBString(4, IsFixed = true, BqlField = typeof (FSAppointmentDet.lineRef))]
  [PXUIField(DisplayName = "Detail Ref. Nbr.", Enabled = false)]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Start>, And<Current<FSLogActionFilter.type>, Equal<FSLogTypeAction.SrvBasedOnAssignment>>>))]
  public virtual string LineRef { get; set; }

  [PXDefault]
  [PXDBInt(BqlField = typeof (FSAppointmentDet.inventoryID))]
  [PXSelector(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID>), SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXUIField(DisplayName = "Inventory ID", Enabled = false)]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Start>, And<Current<FSLogActionFilter.type>, Equal<FSLogTypeAction.SrvBasedOnAssignment>>>))]
  public virtual int? InventoryID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (FSAppointmentDet.tranDesc))]
  [PXDefault]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Start>, And<Current<FSLogActionFilter.type>, Equal<FSLogTypeAction.SrvBasedOnAssignment>>>))]
  public virtual string Descr { get; set; }

  [FSDBTimeSpanLong(BqlField = typeof (FSAppointmentDet.estimatedDuration))]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Estimated Duration", Enabled = false)]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Start>, And<Current<FSLogActionFilter.type>, Equal<FSLogTypeAction.SrvBasedOnAssignment>>>))]
  public virtual int? EstimatedDuration { get; set; }

  [PXDBBool(BqlField = typeof (FSAppointmentDet.isTravelItem))]
  [PXUIField]
  public virtual bool? IsTravelItem { get; set; }

  public class PK : 
    PrimaryKeyOf<FSDetailFSLogAction>.By<FSDetailFSLogAction.srvOrdType, FSDetailFSLogAction.refNbr, FSDetailFSLogAction.lineNbr>
  {
    public static FSDetailFSLogAction Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (FSDetailFSLogAction) PrimaryKeyOf<FSDetailFSLogAction>.By<FSDetailFSLogAction.srvOrdType, FSDetailFSLogAction.refNbr, FSDetailFSLogAction.lineNbr>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public class UK : PrimaryKeyOf<FSDetailFSLogAction>.By<FSDetailFSLogAction.appDetID>
  {
    public static FSDetailFSLogAction Find(PXGraph graph, int? appDetID, PKFindOptions options = 0)
    {
      return (FSDetailFSLogAction) PrimaryKeyOf<FSDetailFSLogAction>.By<FSDetailFSLogAction.appDetID>.FindBy(graph, (object) appDetID, options);
    }
  }

  public static class FK
  {
    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSDetailFSLogAction>.By<FSDetailFSLogAction.srvOrdType>
    {
    }

    public class Appointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>.ForeignKeyOf<FSDetailFSLogAction>.By<FSDetailFSLogAction.srvOrdType, FSDetailFSLogAction.refNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSDetailFSLogAction>.By<FSDetailFSLogAction.inventoryID>
    {
    }

    public class ServiceOrderLine : 
      PrimaryKeyOf<FSSODet>.By<FSSODet.sODetID>.ForeignKeyOf<FSDetailFSLogAction>.By<FSDetailFSLogAction.sODetID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSDetailFSLogAction.selected>
  {
  }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSDetailFSLogAction.srvOrdType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSDetailFSLogAction.refNbr>
  {
  }

  public abstract class appointmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSDetailFSLogAction.appointmentID>
  {
  }

  public abstract class appDetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSDetailFSLogAction.appDetID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSDetailFSLogAction.lineNbr>
  {
  }

  public abstract class sODetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSDetailFSLogAction.sODetID>
  {
  }

  public abstract class lineRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSDetailFSLogAction.lineRef>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSDetailFSLogAction.inventoryID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSDetailFSLogAction.descr>
  {
  }

  public abstract class estimatedDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSDetailFSLogAction.estimatedDuration>
  {
  }

  public abstract class isTravelItem : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSDetailFSLogAction.isTravelItem>
  {
  }
}
