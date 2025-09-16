// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Attributes.FSDBEarningTypeDefaultAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.FS.Attributes;

public class FSDBEarningTypeDefaultAttribute : PXDefaultAttribute
{
  public FSDBEarningTypeDefaultAttribute() => this.PersistingCheck = (PXPersistingCheck) 2;

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    FSAppointmentLog row = (FSAppointmentLog) e.Row;
    FSAppointmentEmployee appointmentEmployee = PXResultset<FSAppointmentEmployee>.op_Implicit(PXSelectBase<FSAppointmentEmployee, PXViewOf<FSAppointmentEmployee>.BasedOn<SelectFromBase<FSAppointmentEmployee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSAppointmentEmployee.appointmentID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSAppointmentEmployee.employeeID, Equal<P.AsInt>>>>>.And<BqlOperand<FSAppointmentEmployee.serviceLineRef, IBqlString>.IsEqual<P.AsString>>>>>.Config>.SelectSingleBound(sender.Graph, (object[]) null, new object[3]
    {
      (object) row.DocID,
      (object) row.BAccountID,
      (object) row.DetLineRef
    }));
    if (appointmentEmployee != null && appointmentEmployee.EarningType != null)
    {
      e.NewValue = (object) appointmentEmployee.EarningType;
    }
    else
    {
      FSAppointmentDet fsAppointmentDet = PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXViewOf<FSAppointmentDet>.BasedOn<SelectFromBase<FSAppointmentDet, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSAppointmentDet.lineRef, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSAppointmentDet.appointmentID, Equal<P.AsInt>>>>>.And<BqlOperand<BAccountType.employeeType, IBqlString>.IsEqual<P.AsString>>>>>.Config>.SelectSingleBound(sender.Graph, (object[]) null, new object[3]
      {
        (object) row.DetLineRef,
        (object) row.DocID,
        (object) row.BAccountType
      }));
      if (fsAppointmentDet != null)
      {
        FSxService extension = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxService>(PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, fsAppointmentDet.InventoryID));
        e.NewValue = (object) extension.DfltEarningType;
      }
      else
      {
        FSSrvOrdType fsSrvOrdType = PXResultset<FSSrvOrdType>.op_Implicit(PXSelectBase<FSSrvOrdType, PXViewOf<FSSrvOrdType>.BasedOn<SelectFromBase<FSSrvOrdType, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSrvOrdType.srvOrdType, Equal<P.AsString>>>>>.And<BqlOperand<BAccountType.employeeType, IBqlString>.IsEqual<P.AsString>>>>.Config>.SelectSingleBound(sender.Graph, (object[]) null, new object[2]
        {
          (object) row.DocType,
          (object) row.BAccountType
        }));
        if (fsSrvOrdType == null)
          return;
        e.NewValue = (object) fsSrvOrdType.DfltEarningType;
      }
    }
  }
}
