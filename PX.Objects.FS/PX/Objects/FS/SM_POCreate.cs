// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_POCreate
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.PO;
using PX.Objects.SO.POCreateExt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class SM_POCreate : PXGraphExtension<POCreateSOExtension, POCreate>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase<POFixedDemand>) ((PXGraphExtension<POCreate>) this).Base.FixedDemand).Join<LeftJoin<FSServiceOrder, On<BqlOperand<FSServiceOrder.noteID, IBqlGuid>.IsEqual<POFixedDemand.refNoteID>>, LeftJoin<FSSODetFSSODetSplit, On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODetFSSODetSplit.planID, Equal<POFixedDemand.planID>>>>, And<BqlOperand<FSSODetFSSODetSplit.srvOrdType, IBqlString>.IsEqual<FSServiceOrder.srvOrdType>>>>.And<BqlOperand<FSSODetFSSODetSplit.refNbr, IBqlString>.IsEqual<FSServiceOrder.refNbr>>>>>>();
    ((PXSelectBase<POFixedDemand>) ((PXGraphExtension<POCreate>) this).Base.FixedDemand).WhereAnd<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSServiceOrder.customerID, Equal<BqlField<SOxPOCreateFilter.customerID, IBqlInt>.FromCurrent>>>>, Or<BqlOperand<Current<SOxPOCreateFilter.customerID>, IBqlInt>.IsNull>>>.Or<BqlOperand<FSServiceOrder.refNbr, IBqlString>.IsNull>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSServiceOrder.srvOrdType, Equal<BqlField<FSxPOCreateFilter.srvOrdType, IBqlString>.FromCurrent>>>>>.Or<BqlOperand<Current<FSxPOCreateFilter.srvOrdType>, IBqlString>.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSServiceOrder.refNbr, Equal<BqlField<FSxPOCreateFilter.serviceOrderRefNbr, IBqlString>.FromCurrent>>>>>.Or<BqlOperand<Current<FSxPOCreateFilter.serviceOrderRefNbr>, IBqlString>.IsNull>>>>();
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Service Order Nbr.", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<FSServiceOrder.refNbr> e)
  {
  }

  /// Overrides <see cref="M:PX.Objects.PO.POCreate.GetFixedDemandFieldScope" />
  [PXOverride]
  public IEnumerable<Type> GetFixedDemandFieldScope(
    Func<IEnumerable<Type>> base_GetFixedDemandFieldScope)
  {
    return base_GetFixedDemandFieldScope().Concat<Type>((IEnumerable<Type>) new Type[6]
    {
      typeof (FSServiceOrder.srvOrdType),
      typeof (FSServiceOrder.refNbr),
      typeof (FSServiceOrder.customerID),
      typeof (FSServiceOrder.projectID),
      typeof (FSServiceOrder.noteID),
      typeof (FSSODetFSSODetSplit)
    });
  }

  [PXOverride]
  public IEnumerable EnumerateAndPrepareFixedDemands(
    PXResultset<POFixedDemand> fixedDemands,
    Func<PXResultset<POFixedDemand>, IEnumerable> base_EnumerateAndPrepareFixedDemands)
  {
    HashSet<long> planIDList = this.GetServicePlanIDs();
    foreach (PXResult<POFixedDemand> pxResult in base_EnumerateAndPrepareFixedDemands(fixedDemands))
    {
      POFixedDemand poFixedDemand = PXResult.Unwrap<POFixedDemand>((object) pxResult);
      HashSet<long> longSet = planIDList;
      // ISSUE: explicit non-virtual call
      if ((longSet != null ? (__nonvirtual (longSet.Contains(poFixedDemand.PlanID.Value)) ? 1 : 0) : 0) != 0)
        poFixedDemand.Selected = new bool?(true);
      yield return (object) pxResult;
    }
  }

  [PXOverride]
  public void EnumerateAndPrepareFixedDemandRow(
    PXResult<POFixedDemand> record,
    Action<PXResult<POFixedDemand>> base_EnumerateAndPrepareFixedDemandRow)
  {
    base_EnumerateAndPrepareFixedDemandRow(record);
    POFixedDemand poFixedDemand = PXResult.Unwrap<POFixedDemand>((object) record);
    FSServiceOrder fsServiceOrder = PXResult.Unwrap<FSServiceOrder>((object) record);
    if (fsServiceOrder == null || string.IsNullOrEmpty(fsServiceOrder.RefNbr))
      return;
    PXCacheEx.GetExtension<POFixedDemand, SOxPOFixedDemand>(poFixedDemand).SalesCustomerID = fsServiceOrder.CustomerID;
    FSSODetFSSODetSplit fssoDetFssoDetSplit = PXResult.Unwrap<FSSODetFSSODetSplit>((object) record);
    if (fssoDetFssoDetSplit != null && !string.IsNullOrEmpty(fssoDetFssoDetSplit.RefNbr))
    {
      PX.Objects.SO.SOLine soLine = PXResult.Unwrap<PX.Objects.SO.SOLine>((object) record);
      soLine.UnitPrice = fssoDetFssoDetSplit.UnitPrice;
      soLine.UOM = fssoDetFssoDetSplit.UOM;
    }
    poFixedDemand.DemandProjectID = fsServiceOrder.ProjectID;
    poFixedDemand.NoteID = fsServiceOrder.NoteID;
  }

  [PXOverride]
  public string GetSorterString(
    PXResult<POFixedDemand> record,
    Func<PXResult<POFixedDemand>, string> base_GetSorterString)
  {
    POFixedDemand poFixedDemand = PXResult.Unwrap<POFixedDemand>((object) record);
    if (!(poFixedDemand.PlanType == "F6"))
      return base_GetSorterString(record);
    FSSODet fssoDet = PXResultset<FSSODet>.op_Implicit(PXSelectBase<FSSODet, PXViewOf<FSSODet>.BasedOn<SelectFromBase<FSSODet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FSSODetSplit>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODet.lineNbr, Equal<FSSODetSplit.lineNbr>>>>, And<BqlOperand<FSSODet.srvOrdType, IBqlString>.IsEqual<FSSODetSplit.srvOrdType>>>>.And<BqlOperand<FSSODet.refNbr, IBqlString>.IsEqual<FSSODetSplit.refNbr>>>>>.Where<BqlOperand<FSSODetSplit.planID, IBqlLong>.IsEqual<P.AsLong>>>.Config>.Select((PXGraph) ((PXGraphExtension<POCreate>) this).Base, new object[1]
    {
      (object) poFixedDemand.PlanID
    }));
    return fssoDet != null ? $"{fssoDet.SrvOrdType}.{fssoDet.RefNbr}.{fssoDet.SortOrder.GetValueOrDefault():D7}" : string.Empty;
  }

  public HashSet<long> GetServicePlanIDs()
  {
    if (((PXSelectBase<POCreate.POCreateFilter>) ((PXGraphExtension<POCreate>) this).Base.Filter).Current != null)
    {
      FSxPOCreateFilter extension = ((PXSelectBase) ((PXGraphExtension<POCreate>) this).Base.Filter).Cache.GetExtension<FSxPOCreateFilter>((object) ((PXSelectBase<POCreate.POCreateFilter>) ((PXGraphExtension<POCreate>) this).Base.Filter).Current);
      if (!string.IsNullOrEmpty(extension.AppointmentRefNbr) && !string.IsNullOrEmpty(extension.SrvOrdType))
        return GraphHelper.RowCast<FSSODetSplit>((IEnumerable) PXSelectBase<FSSODetSplit, PXViewOf<FSSODetSplit>.BasedOn<SelectFromBase<FSSODetSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FSAppointmentDet>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSAppointmentDet.srvOrdType, Equal<FSSODetSplit.srvOrdType>>>>, And<BqlOperand<FSAppointmentDet.origSrvOrdNbr, IBqlString>.IsEqual<FSSODetSplit.refNbr>>>>.And<BqlOperand<FSAppointmentDet.origLineNbr, IBqlInt>.IsEqual<FSSODetSplit.lineNbr>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSAppointmentDet.srvOrdType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<FSAppointmentDet.refNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<FSSODetSplit.completed, IBqlBool>.IsEqual<False>>>>.Config>.Select((PXGraph) ((PXGraphExtension<POCreate>) this).Base, new object[2]
        {
          (object) extension.SrvOrdType,
          (object) extension.AppointmentRefNbr
        })).Select<FSSODetSplit, long>((Func<FSSODetSplit, long>) (x => x.PlanID.Value)).ToHashSet<long>();
    }
    return (HashSet<long>) null;
  }

  /// Overrides <see cref="M:PX.Objects.PO.POCreate.TryRedirectToRelatedDocument(System.Nullable{System.Guid})" />
  [PXOverride]
  public void TryRedirectToRelatedDocument(
    Guid? refNoteID,
    Action<Guid?> base_TryRedirectToRelatedDocument)
  {
    FSServiceOrder fsServiceOrder = PXResultset<FSServiceOrder>.op_Implicit(PXSelectBase<FSServiceOrder, PXViewOf<FSServiceOrder>.BasedOn<SelectFromBase<FSServiceOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FSServiceOrder.noteID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.Select((PXGraph) ((PXGraphExtension<POCreate>) this).Base, new object[1]
    {
      (object) refNoteID
    }));
    if (fsServiceOrder != null)
    {
      ServiceOrderEntry instance = PXGraph.CreateInstance<ServiceOrderEntry>();
      ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) fsServiceOrder.RefNbr, new object[1]
      {
        (object) fsServiceOrder.SrvOrdType
      }));
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
    }
    base_TryRedirectToRelatedDocument(refNoteID);
  }
}
