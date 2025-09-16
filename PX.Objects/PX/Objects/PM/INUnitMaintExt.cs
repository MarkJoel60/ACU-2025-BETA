// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.INUnitMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.PM;

public class INUnitMaintExt : PXGraphExtension<INUnitMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  protected virtual void _(PX.Data.Events.RowDeleting<INUnit> e)
  {
    this.ValidateUnitOfMeasue(e.Row, ((PX.Data.Events.Event<PXRowDeletingEventArgs, PX.Data.Events.RowDeleting<INUnit>>) e).Cache.Graph);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<INUnit.fromUnit> e)
  {
    if (!(e.Row is INUnit row) || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INUnit.fromUnit>, object, object>) e).NewValue == null)
      return;
    this.ValidateUnitOfMeasue(row, ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<INUnit.fromUnit>>) e).Cache.Graph);
  }

  private void ValidateUnitOfMeasue(INUnit inUnit, PXGraph graph)
  {
    if (inUnit == null || !(inUnit.FromUnit == inUnit.ToUnit))
      return;
    Decimal? unitRate = inUnit.UnitRate;
    Decimal num = (Decimal) 1;
    if (!(unitRate.GetValueOrDefault() == num & unitRate.HasValue))
      return;
    if (PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXViewOf<PMSetup>.BasedOn<SelectFromBase<PMSetup, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMSetup.emptyItemUOM, IBqlString>.IsEqual<P.AsString>>>.ReadOnly.Config>.Select(graph, new object[1]
    {
      (object) inUnit.FromUnit
    })) != null)
      throw new PXSetPropertyException((IBqlTable) inUnit, "The {0} unit of measure cannot be deleted because it is specified as the empty item UOM on the Projects Preferences (PM101000) form.", (PXErrorLevel) 4, new object[1]
      {
        (object) inUnit.FromUnit
      });
  }
}
