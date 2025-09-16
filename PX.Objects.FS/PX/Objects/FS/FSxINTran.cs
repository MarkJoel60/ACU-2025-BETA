// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxINTran
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;

#nullable enable
namespace PX.Objects.FS;

public class FSxINTran : PXCacheExtension<
#nullable disable
INTran>, IFSRelatedDoc
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>() && PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();
  }

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type", FieldClass = "SERVICEMANAGEMENT")]
  [PXDefault]
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

  [PXString]
  [PXDefault]
  public string ServiceContractRefNbr => string.Empty;

  [PXInt]
  [PXDefault]
  public int? ServiceContractPeriodID => new int?();

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSxINTran.srvOrdType>
  {
  }

  public abstract class appointmentRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxINTran.appointmentRefNbr>
  {
  }

  public abstract class appointmentLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxINTran.appointmentLineNbr>
  {
  }

  public abstract class serviceOrderRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxINTran.serviceOrderRefNbr>
  {
  }

  public abstract class serviceOrderLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxINTran.serviceOrderLineNbr>
  {
  }

  public abstract class serviceContractRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxINTran.serviceContractRefNbr>
  {
  }

  public abstract class serviceContractPeriodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxINTran.serviceContractPeriodID>
  {
  }
}
