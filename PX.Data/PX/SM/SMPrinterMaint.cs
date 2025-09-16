// Decompiled with JetBrains decompiler
// Type: PX.SM.SMPrinterMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Owin.DeviceHub;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.SM;

public class SMPrinterMaint : PXGraph<SMPrinterMaint>
{
  public PXSave<SMPrinter> Save;
  public PXCancel<SMPrinter> Cancel;
  public PXSelect<SMPrinter, Where<Match<Current<AccessInfo.userName>>>, OrderBy<Asc<SMPrinter.deviceHubID, Asc<SMPrinter.printerName>>>> Printers;
  public PXAction<SMPrinter> updatePrinterList;

  [InjectDependency]
  internal IDeviceHubService DeviceHubService { get; set; }

  [PXUIField(DisplayName = "Update Printer List", MapViewRights = PXCacheRights.Select, MapEnableRights = PXCacheRights.Update)]
  [PXButton]
  protected virtual IEnumerable UpdatePrinterList(PXAdapter adapter)
  {
    this.Save.Press();
    this.LongOperationManager.StartAsyncOperation(new Func<CancellationToken, Task>(this.DeviceHubService.UpdatePrinterList));
    return adapter.Get();
  }

  protected virtual void SMPrinter_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (e.Row == null || ((SMPrinter) e.Row).PrinterName != null)
      return;
    e.Cancel = true;
  }
}
