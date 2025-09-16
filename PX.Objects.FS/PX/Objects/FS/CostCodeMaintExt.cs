// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CostCodeMaintExt
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.FS;

public class CostCodeMaintExt : PXGraphExtension<CostCodeMaint>
{
  public PXSetup<PX.Objects.FS.FSSetup> FSSetup;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  protected virtual void _(PX.Data.Events.RowSelected<FSSrvOrdType> e)
  {
    FSSrvOrdType row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSrvOrdType>>) e).Cache;
    if (row == null)
      return;
    bool? nullable;
    int num1;
    if (((PXSelectBase<PX.Objects.FS.FSSetup>) this.FSSetup).Current != null)
    {
      nullable = ((PXSelectBase<PX.Objects.FS.FSSetup>) this.FSSetup).Current.EnableEmpTimeCardIntegration;
      if (nullable.GetValueOrDefault())
      {
        num1 = row.Behavior != "QT" ? 1 : 0;
        goto label_5;
      }
    }
    num1 = 0;
label_5:
    int num2;
    if (num1 != 0)
    {
      nullable = row.CreateTimeActivitiesFromAppointment;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    bool flag = num2 != 0 && row.Behavior != "QT";
    PXUIFieldAttribute.SetRequired<FSSrvOrdType.dfltEarningType>(cache, flag);
    PXDefaultAttribute.SetPersistingCheck<FSSrvOrdType.dfltEarningType>(cache, (object) row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMCostCode> e)
  {
    if (e.Operation != 3 || e.Row == null)
      return;
    FSSrvOrdType fsSrvOrdType = PXResultset<FSSrvOrdType>.op_Implicit(PXSelectBase<FSSrvOrdType, PXViewOf<FSSrvOrdType>.BasedOn<SelectFromBase<FSSrvOrdType, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSrvOrdType.active, Equal<True>>>>>.And<BqlOperand<FSSrvOrdType.dfltCostCodeID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
    {
      (object) e.Row.CostCodeID
    }));
    if (fsSrvOrdType != null)
      throw new PXException("{0} cannot be deleted because it is referenced in the following record: {1}.", new object[2]
      {
        (object) EntityHelper.GetFriendlyEntityName(typeof (PMCostCode), (object) e.Row),
        (object) EntityHelper.GetFriendlyEntityName(typeof (FSSrvOrdType), (object) fsSrvOrdType)
      });
    PXUpdate<Set<FSSrvOrdType.dfltCostCodeID, Null>, FSSrvOrdType, Where<FSSrvOrdType.dfltCostCodeID, Equal<Required<FSSrvOrdType.dfltCostCodeID>>>>.Update((PXGraph) this.Base, new object[1]
    {
      (object) e.Row.CostCodeID
    });
  }
}
