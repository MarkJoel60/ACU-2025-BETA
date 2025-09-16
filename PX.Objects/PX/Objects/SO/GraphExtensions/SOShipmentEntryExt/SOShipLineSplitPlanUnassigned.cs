// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.SOShipLineSplitPlanUnassigned
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class SOShipLineSplitPlanUnassigned : SOShipLineSplitPlanBase<PX.Objects.SO.Unassigned.SOShipLineSplit>
{
  protected override HashSet<long?> CollectShipmentPlans(string shipmentNbr = null)
  {
    object[] objArray;
    if (shipmentNbr != null)
      objArray = new object[1]{ (object) shipmentNbr };
    else
      objArray = new object[0];
    return new HashSet<long?>((IEnumerable<long?>) ((IQueryable<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>>) PXSelectBase<PX.Objects.SO.Unassigned.SOShipLineSplit, PXSelect<PX.Objects.SO.Unassigned.SOShipLineSplit, Where<PX.Objects.SO.Unassigned.SOShipLineSplit.shipmentNbr, Equal<Optional<SOShipment.shipmentNbr>>>>.Config>.Select((PXGraph) this.Base, objArray)).Select<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>, long?>((Expression<Func<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>, long?>>) (s => ((PX.Objects.SO.Unassigned.SOShipLineSplit) s).PlanID)));
  }

  protected override void UpdatePlansOnParentUpdated(SOShipment parent)
  {
  }
}
