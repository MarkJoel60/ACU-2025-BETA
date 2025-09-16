// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorSORefNbrAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorSORefNbrAttribute : PXSelectorAttribute
{
  public FSSelectorSORefNbrAttribute()
    : base(typeof (Search2<FSServiceOrder.refNbr, LeftJoin<BAccountSelectorBase, On<BAccountSelectorBase.bAccountID, Equal<FSServiceOrder.customerID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<FSServiceOrder.customerID>, And<PX.Objects.CR.Location.locationID, Equal<FSServiceOrder.locationID>>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>>>, Where2<Where<FSServiceOrder.srvOrdType, Equal<Optional<FSServiceOrder.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>, OrderBy<Desc<FSServiceOrder.refNbr>>>), new System.Type[14]
    {
      typeof (FSServiceOrder.refNbr),
      typeof (FSServiceOrder.srvOrdType),
      typeof (BAccountSelectorBase.type),
      typeof (BAccountSelectorBase.acctCD),
      typeof (BAccountSelectorBase.acctName),
      typeof (PX.Objects.CR.Location.locationCD),
      typeof (FSServiceOrder.status),
      typeof (FSServiceOrder.priority),
      typeof (FSServiceOrder.severity),
      typeof (FSServiceOrder.orderDate),
      typeof (FSServiceOrder.sLAETA),
      typeof (FSServiceOrder.assignedEmpID),
      typeof (FSServiceOrder.sourceType),
      typeof (FSServiceOrder.sourceRefNbr)
    })
  {
    this.Filterable = true;
  }
}
