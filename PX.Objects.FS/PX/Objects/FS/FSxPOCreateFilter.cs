// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxPOCreateFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.CS;
using PX.Objects.PO;

#nullable enable
namespace PX.Objects.FS;

public class FSxPOCreateFilter : PXCacheExtension<
#nullable disable
POCreate.POCreateFilter>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXString(4, IsFixed = true, InputMask = ">AAAA")]
  [PXUIField]
  [FSSelectorSrvOrdType]
  [PXFieldDescription]
  [PXDefault]
  public virtual string SrvOrdType { get; set; }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search2<FSServiceOrder.refNbr, LeftJoin<BAccountSelectorBase, On<BAccountSelectorBase.bAccountID, Equal<FSServiceOrder.customerID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<FSServiceOrder.locationID>>>>, Where<FSServiceOrder.srvOrdType, Equal<Current<FSxPOCreateFilter.srvOrdType>>>, OrderBy<Desc<FSServiceOrder.refNbr>>>))]
  [PXDefault]
  public virtual string ServiceOrderRefNbr { get; set; }

  [PXString(20, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXDefault]
  public virtual string AppointmentRefNbr { get; set; }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSxPOCreateFilter.srvOrdType>
  {
  }

  public abstract class serviceOrderRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxPOCreateFilter.serviceOrderRefNbr>
  {
  }

  public abstract class appointmentRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxPOCreateFilter.appointmentRefNbr>
  {
  }
}
