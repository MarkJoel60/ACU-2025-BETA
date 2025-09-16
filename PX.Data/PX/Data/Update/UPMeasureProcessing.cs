// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.UPMeasureProcessing
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Update;

[PXHidden]
public class UPMeasureProcessing : PXGraph<UPMeasureProcessing>
{
  public PXCancel<UPSelectedEndpoint> Cancel;
  public PXProcessing<UPSelectedEndpoint> SelectedEndpoints;

  public UPMeasureProcessing()
  {
    this.SelectedEndpoints.SetProcessDelegate((PXProcessingBase<UPSelectedEndpoint>.ProcessItemDelegate) (ep => UPMeasureMaintenance.MeasureProcess((UPMeasureEndpoint) ep)));
    this.SelectedEndpoints.SetProcessCaption("Measure");
    this.SelectedEndpoints.SetProcessAllCaption("Measure All");
    this.SelectedEndpoints.SetSelected<UPSelectedEndpoint.selected>();
  }

  protected virtual void UPSelectedEndpoint_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is UPMeasureEndpoint row) || row.Screen == null)
      return;
    PXSiteMap.ScreenInfo screenInfo = UPMeasureMaintenance.EnshureScreen(row.Screen);
    if (screenInfo == null)
      return;
    PXSiteMap.ScreenInfo.Action[] actions = screenInfo.Actions;
    PXStringListAttribute.SetList<UPMeasureEndpoint.action>(sender, (object) row, ((IEnumerable<PXSiteMap.ScreenInfo.Action>) actions).Select<PXSiteMap.ScreenInfo.Action, string>((Func<PXSiteMap.ScreenInfo.Action, string>) (a => a.Name)).ToArray<string>(), ((IEnumerable<PXSiteMap.ScreenInfo.Action>) actions).Select<PXSiteMap.ScreenInfo.Action, string>((Func<PXSiteMap.ScreenInfo.Action, string>) (a => a.DisplayName)).ToArray<string>());
  }
}
