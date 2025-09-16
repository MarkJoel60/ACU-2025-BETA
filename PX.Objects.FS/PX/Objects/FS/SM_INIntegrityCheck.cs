// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_INIntegrityCheck
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

[Serializable]
public class SM_INIntegrityCheck : PXGraphExtension<INIntegrityCheck>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXOverride]
  public virtual void DeleteOrphanedItemPlans(
    INItemSiteSummary itemsite,
    SM_INIntegrityCheck.DeleteOrphanedItemPlansDelegate del)
  {
    if (del != null)
      del(itemsite);
    foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXSelectReadonly2<INItemPlan, InnerJoin<FSServiceOrder, On<FSServiceOrder.noteID, Equal<INItemPlan.refNoteID>>, LeftJoin<FSSODetSplit, On<FSSODetSplit.srvOrdType, Equal<FSServiceOrder.srvOrdType>, And<FSSODetSplit.sOOrderNbr, Equal<FSServiceOrder.refNbr>, And<FSSODetSplit.planID, Equal<INItemPlan.planID>>>>>>, Where<FSSODetSplit.sOOrderNbr, IsNull, And<INItemPlan.refEntityType, Equal<Constants.DACName<FSServiceOrder>>, And<INItemPlan.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INItemPlan.siteID, Equal<Current<INItemSiteSummary.siteID>>>>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
    {
      (object) itemsite
    }, Array.Empty<object>()))
      PXDatabase.Delete<INItemPlan>(new PXDataFieldRestrict[1]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<INItemPlan.planID>((PXDbType) 0, new int?(8), (object) PXResult<INItemPlan>.op_Implicit(pxResult).PlanID, (PXComp) 0)
      });
    foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXSelectReadonly2<INItemPlan, InnerJoin<FSAppointment, On<FSAppointment.noteID, Equal<INItemPlan.refNoteID>>, LeftJoin<FSApptLineSplit, On<FSApptLineSplit.srvOrdType, Equal<FSAppointment.srvOrdType>, And<FSApptLineSplit.apptNbr, Equal<FSAppointment.refNbr>, And<FSApptLineSplit.planID, Equal<INItemPlan.planID>>>>>>, Where<FSApptLineSplit.apptNbr, IsNull, And<INItemPlan.refEntityType, Equal<Constants.DACName<FSAppointment>>, And<INItemPlan.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INItemPlan.siteID, Equal<Current<INItemSiteSummary.siteID>>>>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
    {
      (object) itemsite
    }, Array.Empty<object>()))
      PXDatabase.Delete<INItemPlan>(new PXDataFieldRestrict[1]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<INItemPlan.planID>((PXDbType) 0, new int?(8), (object) PXResult<INItemPlan>.op_Implicit(pxResult).PlanID, (PXComp) 0)
      });
  }

  [PXOverride]
  public virtual Type[] GetParentDocumentsNoteFields(Func<Type[]> del)
  {
    Type[] source = del != null ? del() : (Type[]) null;
    if (source == null)
      return source;
    List<Type> list = ((IEnumerable<Type>) source).ToList<Type>();
    list.Add(typeof (FSServiceOrder.noteID));
    list.Add(typeof (FSAppointment.noteID));
    return list.ToArray();
  }

  public delegate void DeleteOrphanedItemPlansDelegate(INItemSiteSummary itemsite);
}
