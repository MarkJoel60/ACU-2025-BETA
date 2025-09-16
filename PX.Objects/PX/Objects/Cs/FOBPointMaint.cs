// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.FOBPointMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CS;

public class FOBPointMaint : PXGraph<FOBPointMaint>
{
  public PXSavePerRow<PX.Objects.CS.FOBPoint> Save;
  public PXCancel<PX.Objects.CS.FOBPoint> Cancel;
  [PXImport(typeof (PX.Objects.CS.FOBPoint))]
  public PXSelect<PX.Objects.CS.FOBPoint> FOBPoint;

  protected virtual void FOBPoint_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    if (PXResultset<PX.Objects.CS.FOBPoint>.op_Implicit(PXSelectBase<PX.Objects.CS.FOBPoint, PXSelect<PX.Objects.CS.FOBPoint, Where<PX.Objects.CS.FOBPoint.fOBPointID, Equal<Required<PX.Objects.CS.FOBPoint.fOBPointID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) ((PX.Objects.CS.FOBPoint) e.Row).FOBPointID
    })) == null)
      return;
    cache.RaiseExceptionHandling<PX.Objects.CS.FOBPoint.fOBPointID>(e.Row, (object) ((PX.Objects.CS.FOBPoint) e.Row).FOBPointID, (Exception) new PXException("Record already exists"));
    ((CancelEventArgs) e).Cancel = true;
  }
}
