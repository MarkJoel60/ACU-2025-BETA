// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.DAC.FSxARTran
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.FS.DAC;

public sealed class FSxARTran : PXCacheExtension<
#nullable disable
ARTran>, IFSRelatedDoc
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXDBString(4, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Service Order Type", FieldClass = "SERVICEMANAGEMENT")]
  public string SrvOrdType { get; set; }

  /// <summary>Appointment ref nbr</summary>
  [PXDBString(20, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public string AppointmentRefNbr { get; set; }

  /// <summary>Appointment line nbr</summary>
  [PXDBInt]
  [PXDefault]
  [PXUIField]
  public int? AppointmentLineNbr { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public string ServiceOrderRefNbr { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField]
  public int? ServiceOrderLineNbr { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public string ServiceContractRefNbr { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField]
  public int? ServiceContractPeriodID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [ListField_EquipmentAction.ListAtrribute]
  [PXDefault]
  [PXUIField(DisplayName = "Equipment Action", Visible = false, FieldClass = "EQUIPMENTMANAGEMENT", Enabled = false)]
  public string EquipmentAction { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Suspended Target Equipment ID", FieldClass = "EQUIPMENTMANAGEMENT", Enabled = false)]
  [PXDefault]
  [PXSelector(typeof (Search<FSEquipment.SMequipmentID>), SubstituteKey = typeof (FSEquipment.refNbr))]
  public int? ReplaceSMEquipmentID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Target Equipment ID", Visible = false, FieldClass = "EQUIPMENTMANAGEMENT", Enabled = false)]
  [PXDefault]
  [FSSelectorExtensionMaintenanceEquipment(typeof (ARTran.customerID))]
  public int? SMEquipmentID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Model Equipment Line Nbr.", Visible = false, FieldClass = "EQUIPMENTMANAGEMENT", Enabled = false)]
  [PXDefault]
  [FSSelectorNewTargetEquipmentSOInvoice(ValidateValue = false)]
  public int? NewEquipmentLineNbr { get; set; }

  /// <summary>Component ID</summary>
  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Component ID", FieldClass = "EQUIPMENTMANAGEMENT", Enabled = false)]
  [PXSelector(typeof (Search<FSModelTemplateComponent.componentID>), SubstituteKey = typeof (FSModelTemplateComponent.componentCD))]
  public int? ComponentID { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Component Line Nbr.", Visible = false, FieldClass = "EQUIPMENTMANAGEMENT", Enabled = false)]
  [FSSelectorEquipmentLineRefSOInvoice]
  public int? EquipmentComponentLineNbr { get; set; }

  [PXDBString(2147483647 /*0x7FFFFFFF*/, IsUnicode = false)]
  [PXUIField(DisplayName = "Equipment Action Comment", FieldClass = "EQUIPMENTMANAGEMENT", Visible = false, Enabled = false)]
  [SkipSetExtensionVisibleInvisible]
  public string Comment { get; set; }

  [FSRelatedDocument(typeof (ARTran))]
  [PXUIField(DisplayName = "Related Svc. Doc. Nbr.", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public string RelatedDocument { get; set; }

  /// <summary>Is FS related</summary>
  [PXBool]
  [PXDefault]
  public bool? IsFSRelated
  {
    get
    {
      return new bool?(!string.IsNullOrEmpty(this.AppointmentRefNbr) || !string.IsNullOrEmpty(this.ServiceOrderRefNbr) || !string.IsNullOrEmpty(this.ServiceContractRefNbr));
    }
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSxARTran.srvOrdType>
  {
  }

  public abstract class appointmentRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxARTran.appointmentRefNbr>
  {
  }

  public abstract class appointmentLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxARTran.appointmentLineNbr>
  {
  }

  public abstract class serviceOrderRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxARTran.serviceOrderRefNbr>
  {
  }

  public abstract class serviceOrderLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxARTran.serviceOrderLineNbr>
  {
  }

  public abstract class serviceContractRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxARTran.serviceContractRefNbr>
  {
  }

  public abstract class serviceContractPeriodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxARTran.serviceContractPeriodID>
  {
  }

  public abstract class equipmentAction : ListField_EquipmentAction
  {
  }

  public abstract class replaceSMEquipmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxARTran.replaceSMEquipmentID>
  {
  }

  public abstract class sMEquipmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxARTran.sMEquipmentID>
  {
  }

  public abstract class newEquipmentLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxARTran.newEquipmentLineNbr>
  {
  }

  public abstract class componentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxARTran.componentID>
  {
  }

  public abstract class equipmentComponentLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxARTran.equipmentComponentLineNbr>
  {
  }

  public abstract class comment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSxARTran.comment>
  {
  }

  public abstract class relatedDocument : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxARTran.relatedDocument>
  {
  }

  public abstract class isFSRelated : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSxARTran.isFSRelated>
  {
  }
}
