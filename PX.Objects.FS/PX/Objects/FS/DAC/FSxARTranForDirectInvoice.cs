// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.DAC.FSxARTranForDirectInvoice
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.SO.DAC.Projections;

#nullable enable
namespace PX.Objects.FS.DAC;

public sealed class FSxARTranForDirectInvoice : 
  PXCacheExtension<
  #nullable disable
  ARTranForDirectInvoice>,
  IFSRelatedDoc
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  /// <inheritdoc cref="P:PX.Objects.FS.DAC.FSxARTran.RelatedDocument" />
  [FSRelatedDocument(typeof (ARTran))]
  [PXUIField(DisplayName = "Related Document", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public string RelatedDocument { get; set; }

  /// <inheritdoc cref="P:PX.Objects.FS.DAC.FSxARTran.SrvOrdType" />
  [PXDBString(4, IsFixed = true, BqlField = typeof (FSxARTran.srvOrdType))]
  [PXDefault]
  [PXUIField(DisplayName = "Service Order Type", FieldClass = "SERVICEMANAGEMENT")]
  public string SrvOrdType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.FS.DAC.FSxARTran.ServiceOrderRefNbr" />
  [PXDBString(15, IsUnicode = true, BqlField = typeof (FSxARTran.serviceOrderRefNbr))]
  [PXUIField]
  public string ServiceOrderRefNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.FS.DAC.FSxARTran.ServiceOrderLineNbr" />
  [PXDBInt]
  [PXDefault]
  [PXUIField]
  public int? ServiceOrderLineNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.FS.DAC.FSxARTran.AppointmentRefNbr" />
  [PXDBString(20, IsUnicode = true, BqlField = typeof (FSxARTran.appointmentRefNbr))]
  [PXDefault]
  [PXUIField]
  public string AppointmentRefNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.FS.DAC.FSxARTran.AppointmentLineNbr" />
  [PXDBInt]
  [PXDefault]
  [PXUIField]
  public int? AppointmentLineNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.FS.DAC.FSxARTran.ServiceContractRefNbr" />
  [PXDBString(15, IsUnicode = true, BqlField = typeof (FSxARTran.serviceContractRefNbr))]
  [PXUIField]
  public string ServiceContractRefNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.FS.DAC.FSxARTran.ServiceContractPeriodID" />
  [PXDBInt]
  [PXDefault]
  [PXUIField]
  public int? ServiceContractPeriodID { get; set; }

  public abstract class relatedDocument : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxARTranForDirectInvoice.relatedDocument>
  {
  }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxARTranForDirectInvoice.srvOrdType>
  {
  }

  public abstract class serviceOrderRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxARTranForDirectInvoice.serviceOrderRefNbr>
  {
  }

  public abstract class serviceOrderLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxARTranForDirectInvoice.serviceOrderLineNbr>
  {
  }

  public abstract class appointmentRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxARTranForDirectInvoice.appointmentRefNbr>
  {
  }

  public abstract class appointmentLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxARTranForDirectInvoice.appointmentLineNbr>
  {
  }

  public abstract class serviceContractRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxARTranForDirectInvoice.serviceContractRefNbr>
  {
  }

  public abstract class serviceContractPeriodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxARTranForDirectInvoice.serviceContractPeriodID>
  {
  }
}
