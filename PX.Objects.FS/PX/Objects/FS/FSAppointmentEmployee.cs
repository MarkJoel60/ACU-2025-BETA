// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentEmployee
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("FSAppointmentEmployee")]
[Serializable]
public class FSAppointmentEmployee : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type", Visible = false, Enabled = false)]
  [PXDefault(typeof (FSAppointment.srvOrdType))]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType>), CacheGlobal = true)]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Appointment Nbr.", Visible = false, Enabled = false)]
  [PXDBDefault(typeof (FSAppointment.refNbr), DefaultForUpdate = false)]
  [PXParent(typeof (Select<FSAppointment, Where<FSAppointment.srvOrdType, Equal<Current<FSAppointmentEmployee.srvOrdType>>, And<FSAppointment.refNbr, Equal<Current<FSAppointmentEmployee.refNbr>>>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBInt]
  [PXDBDefault(typeof (FSAppointment.appointmentID))]
  [PXUIField(DisplayName = "Appointment Ref. Nbr.")]
  public virtual int? AppointmentID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (FSAppointment.employeeLineCntr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  [PXUnboundFormula(typeof (int1), typeof (SumCalc<FSAppointment.staffCntr>))]
  public virtual int? LineNbr { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXUIField]
  public virtual string LineRef { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXParent(typeof (Select<FSAppointmentDet, Where<FSAppointmentDet.lineRef, Equal<Current<FSAppointmentEmployee.serviceLineRef>>, And<FSAppointmentDet.appointmentID, Equal<Current<FSAppointmentEmployee.appointmentID>>>>>))]
  [PXUIField(DisplayName = "Detail Ref. Nbr.")]
  [FSSelectorAppointmentSODetID]
  [PXCheckUnique(new System.Type[] {}, Where = typeof (Where<FSAppointmentEmployee.appointmentID, Equal<Current<FSAppointment.appointmentID>>, And<FSAppointmentEmployee.employeeID, Equal<Current<FSAppointmentEmployee.employeeID>>>>), IgnoreNulls = false, ClearOnDuplicate = false)]
  public virtual string ServiceLineRef { get; set; }

  [PXDBInt]
  [PXDefault]
  [FSSelector_StaffMember_ServiceOrderProjectID]
  [PXUIField(DisplayName = "Staff Member", TabOrder = 0)]
  public virtual int? EmployeeID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Primary Driver")]
  public virtual bool? PrimaryDriver { get; set; }

  [PXNote]
  [PXUIField(DisplayName = "NoteID", Enabled = false)]
  public virtual Guid? NoteID { get; set; }

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

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Route Driver", Enabled = false, Visible = false)]
  public virtual bool? IsDriver { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField]
  [EmployeeType.List]
  public virtual string Type { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXFormula(typeof (Default<FSAppointmentEmployee.serviceLineRef>))]
  [PXSelector(typeof (EPEarningType.typeCD))]
  [PXUIField(DisplayName = "Earning Type")]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>>))]
  [PXRestrictor(typeof (Where<EPEarningType.isActive, Equal<True>>), "The earning type {0} selected on the Time & Expenses Preferences (EP101000) form is inactive. Inactive earning types are not available for data entry in new activities and time entries.", new System.Type[] {typeof (EPEarningType.typeCD)})]
  public virtual string EarningType { get; set; }

  [PXDBBool]
  [PXDefault(typeof (Switch<Case<Where<Current<FSAppointmentEmployee.type>, Equal<BAccountType.employeeType>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>>, Current<FSSrvOrdType.createTimeActivitiesFromAppointment>>, False>))]
  [PXUIField(DisplayName = "Track Time")]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>>))]
  [PXUIEnabled(typeof (Where<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>>))]
  public virtual bool? TrackTime { get; set; }

  [PXDefault(typeof (Switch<Case<Where<Selector<FSAppointmentEmployee.dfltProjectID, PMProject.nonProject>, Equal<False>>, Current<FSSrvOrdType.dfltCostCodeID>>>))]
  [SMCostCode(typeof (FSAppointmentEmployee.skipCostCodeValidation), null, typeof (FSAppointmentEmployee.dfltProjectTaskID))]
  public virtual int? CostCodeID { get; set; }

  [PXBool]
  [PXFormula(typeof (IIf<Where<Current<FSSrvOrdType.createTimeActivitiesFromAppointment>, Equal<True>, And<Current<FSSetup.enableEmpTimeCardIntegration>, Equal<True>>>, False, True>))]
  public virtual bool? SkipCostCodeValidation { get; set; }

  [PXDefault(typeof (FSAppointment.projectID))]
  [ProjectBase(typeof (FSServiceOrder.billCustomerID), Visible = false)]
  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.isCancelled, Equal<False>>), "The {0} project or contract is canceled.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXForeignReference(typeof (FSAppointmentEmployee.FK.DefaultProject))]
  public virtual int? DfltProjectID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Project Task", FieldClass = "PROJECT")]
  [FSSelectorActive_AR_SO_ProjectTask(typeof (Where<PMTask.projectID, Equal<Current<FSAppointmentEmployee.dfltProjectID>>>))]
  [PXForeignReference(typeof (FSAppointmentEmployee.FK.DefaultTask))]
  public virtual int? DfltProjectTaskID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.laborItem>>>), SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
  [PXUIField(DisplayName = "Labor Item")]
  [PXDefault(typeof (Search<EPEmployee.labourItemID, Where<EPEmployee.bAccountID, Equal<Current<FSAppointmentEmployee.employeeID>>>>))]
  [PXFormula(typeof (Default<FSAppointmentEmployee.employeeID>))]
  public virtual int? LaborItemID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Mem_Selected { get; set; }

  [PXBool]
  [PXUIField(Enabled = false, Visible = false)]
  public virtual bool? IsStaffCalendar { get; set; }

  public class PK : 
    PrimaryKeyOf<FSAppointmentEmployee>.By<FSAppointmentEmployee.srvOrdType, FSAppointmentEmployee.refNbr, FSAppointmentEmployee.lineNbr>
  {
    public static FSAppointmentEmployee Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (FSAppointmentEmployee) PrimaryKeyOf<FSAppointmentEmployee>.By<FSAppointmentEmployee.srvOrdType, FSAppointmentEmployee.refNbr, FSAppointmentEmployee.lineNbr>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSAppointmentEmployee>.By<FSAppointmentEmployee.srvOrdType>
    {
    }

    public class Appointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>.ForeignKeyOf<FSAppointmentEmployee>.By<FSAppointmentEmployee.srvOrdType, FSAppointmentEmployee.refNbr>
    {
    }

    public class Staff : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<FSAppointmentEmployee>.By<FSAppointmentEmployee.employeeID>
    {
    }

    public class EarningType : 
      PrimaryKeyOf<EPEarningType>.By<EPEarningType.typeCD>.ForeignKeyOf<FSAppointmentEmployee>.By<FSAppointmentEmployee.earningType>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<FSAppointmentEmployee>.By<FSAppointmentEmployee.costCodeID>
    {
    }

    public class DefaultProject : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<FSAppointmentEmployee>.By<FSAppointmentEmployee.dfltProjectID>
    {
    }

    public class DefaultTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<FSAppointmentEmployee>.By<FSAppointmentEmployee.dfltProjectID, FSAppointmentEmployee.dfltProjectTaskID>
    {
    }

    public class LaborItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSAppointmentEmployee>.By<FSAppointmentEmployee.laborItemID>
    {
    }
  }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentEmployee.srvOrdType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentEmployee.refNbr>
  {
  }

  public abstract class appointmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentEmployee.appointmentID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentEmployee.lineNbr>
  {
  }

  public abstract class lineRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentEmployee.lineRef>
  {
  }

  public abstract class serviceLineRef : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentEmployee.serviceLineRef>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentEmployee.employeeID>
  {
  }

  public abstract class primaryDriver : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentEmployee.primaryDriver>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSAppointmentEmployee.noteID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSAppointmentEmployee.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentEmployee.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentEmployee.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSAppointmentEmployee.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentEmployee.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentEmployee.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSAppointmentEmployee.Tstamp>
  {
  }

  public abstract class isDriver : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentEmployee.isDriver>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentEmployee.type>
  {
  }

  public abstract class earningType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentEmployee.earningType>
  {
  }

  public abstract class trackTime : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentEmployee.trackTime>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentEmployee.costCodeID>
  {
  }

  public abstract class skipCostCodeValidation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentEmployee.skipCostCodeValidation>
  {
  }

  public abstract class dfltProjectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentEmployee.dfltProjectID>
  {
  }

  public abstract class dfltProjectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentEmployee.dfltProjectTaskID>
  {
  }

  public abstract class laborItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentEmployee.laborItemID>
  {
  }

  public abstract class mem_Selected : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentEmployee.mem_Selected>
  {
  }

  public abstract class isStaffCalendar : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentEmployee.isStaffCalendar>
  {
  }
}
