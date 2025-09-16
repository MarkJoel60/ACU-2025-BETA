// Decompiled with JetBrains decompiler
// Type: PX.SM.SMScannerMaint
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

public class SMScannerMaint : PXGraph<SMScannerMaint>
{
  public PXSave<SMScanner> Save;
  public PXCancel<SMScanner> Cancel;
  public PXSelect<SMScanner> Scanners;
  private const string DeviceHubFeatureName = "PX.Objects.CS.FeaturesSet+deviceHub";
  public PXAction<SMScanner> updateScannerList;

  [InjectDependency]
  internal IDeviceHubService DeviceHubService { get; set; }

  public SMScannerMaint()
  {
    this.Scanners.Cache.AllowInsert = PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub");
    this.Scanners.AllowInsert = false;
    this.Scanners.Cache.AllowDelete = PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub");
    this.updateScannerList.SetEnabled(PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub"));
  }

  protected virtual void _(Events.RowSelected<SMScanner> e)
  {
    PXCache cache = e.Cache;
    SMScanner row = e.Row;
    if (cache == null || row == null || string.IsNullOrEmpty(row.ScannerName))
      return;
    ComboBoxUtils.PopulateFileTypes<SMScanner.paperSourceDefValue>(cache, row.PaperSourceComboValues, (object) row);
    ComboBoxUtils.PopulateFileTypes<SMScanner.pixelTypeDefValue>(cache, row.PixelTypeComboValues, (object) row);
    ComboBoxUtils.PopulateFileTypes<SMScanner.resolutionDefValue>(cache, row.ResolutionComboValues, (object) row);
    ComboBoxUtils.PopulateFileTypes<SMScanner.fileTypeDefValue>(cache, row.FileTypeComboValues, (object) row);
  }

  [PXUIField(DisplayName = "Update Scanner List", MapViewRights = PXCacheRights.Select, MapEnableRights = PXCacheRights.Update)]
  [PXButton]
  protected virtual IEnumerable UpdateScannerList(PXAdapter adapter)
  {
    this.Save.Press();
    this.LongOperationManager.StartAsyncOperation(new Func<CancellationToken, Task>(this.DeviceHubService.UpdateScannerList));
    return adapter.Get();
  }

  protected virtual void SMScanner_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (e.Row == null || ((SMScanner) e.Row).ScannerName != null)
      return;
    e.Cancel = true;
  }
}
