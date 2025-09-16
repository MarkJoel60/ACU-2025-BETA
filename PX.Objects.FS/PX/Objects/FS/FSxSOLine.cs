// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxSOLine
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.SO;

#nullable enable
namespace PX.Objects.FS;

public class FSxSOLine : PXCacheExtension<
#nullable disable
SOLine>, IFSRelatedDoc
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? SDPosted { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Appointment", Visible = false)]
  public virtual bool? SDSelected { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Service Order Type", FieldClass = "SERVICEMANAGEMENT")]
  public virtual string SrvOrdType { get; set; }

  [PXDBString(20, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string AppointmentRefNbr { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField]
  public virtual int? AppointmentLineNbr { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string ServiceOrderRefNbr { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField]
  public virtual int? ServiceOrderLineNbr { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string ServiceContractRefNbr { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField]
  public virtual int? ServiceContractPeriodID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Target Equipment ID", Visible = false, FieldClass = "EQUIPMENTMANAGEMENT")]
  [PXDefault]
  [FSSelectorExtensionMaintenanceEquipment(typeof (PX.Objects.SO.SOOrder.customerID))]
  public virtual int? SMEquipmentID { get; set; }

  [PXDBInt]
  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.inventory>))]
  [PXUIField(DisplayName = "Model Equipment Line Nbr.", Visible = false, FieldClass = "EQUIPMENTMANAGEMENT")]
  [PXDefault]
  [FSSelectorNewTargetEquipmentSalesOrder]
  public virtual int? NewEquipmentLineNbr { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Component ID", Visible = false, FieldClass = "EQUIPMENTMANAGEMENT")]
  [FSSelectorComponentIDSalesOrder]
  public virtual int? ComponentID { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Component Line Nbr.", Visible = false, FieldClass = "EQUIPMENTMANAGEMENT")]
  [FSSelectorEquipmentLineRefSalesOrder]
  public virtual int? EquipmentComponentLineNbr { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.inventory>))]
  [ListField_EquipmentAction.ListAtrribute]
  [PXDefault]
  [PXUIField(DisplayName = "Equipment Action", Visible = false, FieldClass = "EQUIPMENTMANAGEMENT")]
  public virtual string EquipmentAction { get; set; }

  [PXDBString(2147483647 /*0x7FFFFFFF*/, IsUnicode = true)]
  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.inventory>))]
  [PXDefault]
  [PXUIField(DisplayName = "Equipment Action Comment", FieldClass = "EQUIPMENTMANAGEMENT", Visible = false)]
  [SkipSetExtensionVisibleInvisible]
  public virtual string Comment { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault]
  public virtual string EquipmentItemClass { get; set; }

  [FSRelatedDocument(typeof (SOLine))]
  [PXDefault]
  [PXUIField(DisplayName = "Related Svc. Doc. Nbr.", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual string RelatedDocument { get; set; }

  [PXInt]
  [PXDefault]
  public virtual int? Mem_PreviousPostID { get; set; }

  [PXString]
  [PXDefault]
  public virtual string Mem_TableSource { get; set; }

  public abstract class sDPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSxSOLine.sDPosted>
  {
  }

  public abstract class sDSelected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSxSOLine.sDSelected>
  {
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSxSOLine.srvOrdType>
  {
  }

  public abstract class appointmentRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxSOLine.appointmentRefNbr>
  {
  }

  public abstract class appointmentLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxSOLine.appointmentLineNbr>
  {
  }

  public abstract class serviceOrderRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxSOLine.serviceOrderRefNbr>
  {
  }

  public abstract class serviceOrderLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxSOLine.serviceOrderLineNbr>
  {
  }

  public abstract class serviceContractRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxSOLine.serviceContractRefNbr>
  {
  }

  public abstract class serviceContractPeriodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxSOLine.serviceContractPeriodID>
  {
  }

  public abstract class sMEquipmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxSOLine.sMEquipmentID>
  {
  }

  public abstract class newEquipmentLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxSOLine.newEquipmentLineNbr>
  {
  }

  public abstract class componentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxSOLine.componentID>
  {
  }

  public abstract class equipmentComponentLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxSOLine.equipmentComponentLineNbr>
  {
  }

  public abstract class equipmentAction : ListField_EquipmentAction
  {
  }

  public abstract class comment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSxSOLine.comment>
  {
  }

  public abstract class equipmentItemClass : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxSOLine.equipmentItemClass>
  {
  }

  public abstract class relatedDocument : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxSOLine.relatedDocument>
  {
  }

  public abstract class mem_PreviousPostID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxSOLine.mem_PreviousPostID>
  {
  }

  public abstract class mem_TableSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxSOLine.mem_TableSource>
  {
  }
}
