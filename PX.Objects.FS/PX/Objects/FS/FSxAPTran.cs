// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxAPTran
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.FS;

public class FSxAPTran : PXCacheExtension<
#nullable disable
APTran>, IFSRelatedDoc
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

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

  [PXDBString(40)]
  [PXDefault(typeof (CreateAPFilter.relatedEntityType))]
  [PXStringList(new string[] {"PX.Objects.FS.FSServiceOrder", "PX.Objects.FS.FSAppointment"}, new string[] {"Service Order", "Appointment"})]
  [PXUIField(DisplayName = "Related Svc. Doc. Type", Visible = true)]
  public virtual string RelatedEntityType { get; set; }

  [FSEntityIDAPInvoiceSelector(typeof (FSxAPTran.relatedEntityType))]
  [PXDBGuid(false)]
  [PXDefault(typeof (CreateAPFilter.relatedDocNoteID))]
  [PXUIField(DisplayName = "Related Svc. Doc. Nbr.", Visible = false)]
  [PXFormula(typeof (Default<FSxAPTran.relatedEntityType>))]
  public virtual Guid? RelatedDocNoteID { get; set; }

  [PXInt]
  [PXDefault]
  public virtual int? Mem_PreviousPostID { get; set; }

  [PXString]
  [PXDefault]
  public virtual string Mem_TableSource { get; set; }

  [PXInt]
  [PXDefault]
  public int? ServiceContractPeriodID => new int?();

  [PXString]
  [PXDefault]
  public string ServiceContractRefNbr => string.Empty;

  [PXBool]
  [PXDefault]
  public virtual bool? IsDocBilledOrClosed { get; set; }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSxAPTran.srvOrdType>
  {
  }

  public abstract class appointmentRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxAPTran.appointmentRefNbr>
  {
  }

  public abstract class appointmentLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxAPTran.appointmentLineNbr>
  {
  }

  public abstract class serviceOrderRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxAPTran.serviceOrderRefNbr>
  {
  }

  public abstract class serviceOrderLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxAPTran.serviceOrderLineNbr>
  {
  }

  public abstract class relatedEntityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxAPTran.relatedEntityType>
  {
  }

  public abstract class relatedDocNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSxAPTran.relatedDocNoteID>
  {
  }

  public abstract class mem_PreviousPostID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxAPTran.mem_PreviousPostID>
  {
  }

  public abstract class mem_TableSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxAPTran.mem_TableSource>
  {
  }

  public abstract class serviceContractPeriodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxAPTran.serviceContractPeriodID>
  {
  }

  public abstract class serviceContractRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxAPTran.serviceContractRefNbr>
  {
  }

  public abstract class isDocBilledOrClosed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxAPTran.isDocBilledOrClosed>
  {
  }
}
