// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxPMTimeActivity
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.FS;

public class FSxPMTimeActivity : PXCacheExtension<
#nullable disable
PMTimeActivity>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>() && PXAccess.FeatureInstalled<FeaturesSet.timeReportingModule>();
  }

  [Service(null)]
  [PXUIField(DisplayName = "Service")]
  [PXDefault]
  public virtual int? ServiceID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Customer ID")]
  [FSSelectorCustomer]
  [PXDefault]
  public virtual int? AppointmentCustomerID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Appointment Nbr.")]
  [PXSelector(typeof (Search<FSAppointment.appointmentID>), SubstituteKey = typeof (FSAppointment.refNbr))]
  [PXDefault]
  public virtual int? AppointmentID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Log Ref. Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<FSAppointmentLog.lineNbr, Where<FSAppointmentLog.docID, Equal<Current<FSxPMTimeActivity.appointmentID>>>>), SubstituteKey = typeof (FSAppointmentLog.lineRef))]
  [PXDefault]
  public virtual int? LogLineNbr { get; set; }

  [PXBool]
  [PXUIField(Visible = false)]
  [PXDefault]
  public virtual bool? LastBillable { get; set; }

  public abstract class serviceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxPMTimeActivity.serviceID>
  {
  }

  public abstract class appointmentCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxPMTimeActivity.appointmentCustomerID>
  {
  }

  public abstract class appointmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxPMTimeActivity.appointmentID>
  {
  }

  public abstract class logLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxPMTimeActivity.logLineNbr>
  {
  }

  public abstract class lastBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSxPMTimeActivity.lastBillable>
  {
  }
}
