// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.UnitOfMeasureMaintExt
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

#nullable disable
namespace PX.Objects.PM;

public class UnitOfMeasureMaintExt : PXGraphExtension<UnitOfMeasureMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  protected virtual void _(PX.Data.Events.RowDeleting<UnitOfMeasure> e)
  {
    UnitOfMeasure row = e.Row;
    if (row == null)
      return;
    if (PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXViewOf<PMSetup>.BasedOn<SelectFromBase<PMSetup, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMSetup.emptyItemUOM, IBqlString>.IsEqual<P.AsString>>>.ReadOnly.Config>.Select(((PX.Data.Events.Event<PXRowDeletingEventArgs, PX.Data.Events.RowDeleting<UnitOfMeasure>>) e).Cache.Graph, new object[1]
    {
      (object) row.Unit
    })) != null)
      throw new PXSetPropertyException((IBqlTable) row, "The {0} unit of measure cannot be deleted because it is specified as the empty item UOM on the Projects Preferences (PM101000) form.", (PXErrorLevel) 4, new object[1]
      {
        (object) row.Unit
      });
  }
}
