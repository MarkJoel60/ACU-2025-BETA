// Decompiled with JetBrains decompiler
// Type: PX.SM.ScanJobInfoMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace PX.SM;

public class ScanJobInfoMaint : PXGraph<
#nullable disable
ScanJobInfoMaint>
{
  public PXFilter<ScanJobInfo> ScanJobInfoRecord;
  public PXAction<ScanJobInfo> Cancel;
  public PXAction<ScanJobInfo> AttachFromScanner;

  protected virtual void _(Events.RowSelected<ScanJobInfo> e)
  {
    PXCache cache = e.Cache;
    ScanJobInfo row = e.Row;
    if (cache == null || row == null || !row.ScannerID.HasValue)
      return;
    this.ScanJobInfoRecord.AllowUpdate = !(row.ProcessStarted ?? false);
    this.AttachFromScanner.SetEnabled(this.ScanJobInfoRecord.AllowUpdate);
    ComboBoxUtils.PopulateFileTypes<ScanJobInfo.paperSource>(cache, row.PaperSourceList, (object) row);
    ComboBoxUtils.PopulateFileTypes<ScanJobInfo.pixelType>(cache, row.PixelTypeList, (object) row);
    ComboBoxUtils.PopulateFileTypes<ScanJobInfo.resolution>(cache, row.ResolutionList, (object) row);
    ComboBoxUtils.PopulateFileTypes<ScanJobInfo.fileType>(cache, row.FileTypeList, (object) row);
  }

  protected virtual void _(Events.RowUpdated<ScanJobInfo> e)
  {
    PXCache cache = e.Cache;
    if (cache.ObjectsEqual<ScanJobInfo.scannerID>((object) e.Row, (object) e.OldRow))
      return;
    SMScanner smScanner = PXSelectorAttribute.Select<ScanJobInfo.scannerID>(cache, (object) e.Row) as SMScanner;
    e.Row.PaperSourceList = smScanner.PaperSourceComboValues;
    e.Row.PixelTypeList = smScanner.PixelTypeComboValues;
    e.Row.ResolutionList = smScanner.ResolutionComboValues;
    e.Row.FileTypeList = smScanner.FileTypeComboValues;
    e.Row.PaperSource = smScanner.PaperSourceDefValue;
    e.Row.PixelType = smScanner.PixelTypeDefValue;
    e.Row.Resolution = smScanner.ResolutionDefValue;
    e.Row.FileType = smScanner.FileTypeDefValue;
  }

  [PXUIField(DisplayName = "Cancel")]
  [PXButton(CommitChanges = true, ImageKey = "Cancel", Tooltip = "Cancel (Esc)")]
  public virtual IEnumerable cancel(PXAdapter adapter)
  {
    ScanJobInfo current = this.ScanJobInfoRecord.Current;
    current.ProcessStarted = new bool?(false);
    this.ScanJobInfoRecord.Update(current);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Scan", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton(ClosePopup = true, CommitChanges = true)]
  public virtual IEnumerable attachFromScanner(PXAdapter adapter)
  {
    ScanJobInfo data = this.ScanJobInfoRecord.Current;
    data.ProcessStarted = new bool?(true);
    this.ScanJobInfoRecord.Update(data);
    this.LongOperationManager.StartAsyncOperation((Func<CancellationToken, Task>) (async ct =>
    {
      int? scanJob = await SMScanJobMaint.CreateScanJob(data, ct);
      System.DateTime dateTime = System.DateTime.Now.AddSeconds(300.0);
      bool flag1 = false;
      try
      {
        while (!ct.IsCancellationRequested)
        {
          using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<SMScanJob>(new PXDataField("Status"), new PXDataField("Error"), (PXDataField) new PXDataFieldValue("ScanJobID", PXDbType.Int, (object) scanJob)))
          {
            flag1 = !(pxDataRecord?.GetString(0) == "D") && !(pxDataRecord?.GetString(0) == "F");
            if (pxDataRecord?.GetString(0) == "F")
              throw new PXException($"Scan failed with the following error: {pxDataRecord?.GetString(1)}");
          }
          if (!flag1)
            break;
          if (System.DateTime.Now > dateTime)
            throw new PXException($"Could not scan the {data.FileName} document. The request timed out.");
          Thread.Sleep(250);
        }
      }
      finally
      {
        bool? processStarted = data.ProcessStarted;
        bool flag2 = true;
        if (processStarted.GetValueOrDefault() == flag2 & processStarted.HasValue)
        {
          data.ProcessStarted = new bool?(false);
          this.ScanJobInfoRecord.Update(data);
        }
      }
    }));
    return adapter.Get();
  }

  public static void AddScanAction(PXGraph Graph)
  {
    if (!PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub") || Graph is PXGenericInqGrph || string.IsNullOrEmpty(Graph.PrimaryView) || Graph.PrimaryItemType == (System.Type) null)
      return;
    PXButtonDelegate handler = (PXButtonDelegate) (adapter =>
    {
      string str1 = string.IsNullOrEmpty(adapter.CommandArguments) ? Graph.PrimaryView : adapter.CommandArguments;
      string primaryView = Graph.PrimaryView;
      object current1 = Graph.Views[primaryView].Cache?.Current;
      if (PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub") && current1 != null && !string.IsNullOrEmpty(str1))
      {
        object current2 = Graph.Views[str1].Cache.Current;
        string str2 = Graph.Accessinfo.ScreenID.Replace(".", "");
        Dictionary<string, string> source1 = new Dictionary<string, string>();
        foreach (string keyName in Graph.GetKeyNames(primaryView))
          source1.Add(keyName, Convert.ToString(Graph.GetValue(primaryView, current1, keyName)));
        Dictionary<string, string> source2 = new Dictionary<string, string>();
        if (primaryView.ToUpper() != str1.ToUpper())
        {
          foreach (string keyName in Graph.GetKeyNames(str1))
            source2.Add(keyName, Convert.ToString(Graph.GetValue(str1, current2, keyName)));
        }
        ScanJobInfoMaint instance = PXGraph.CreateInstance<ScanJobInfoMaint>();
        ScanJobInfo current3 = instance.ScanJobInfoRecord.Current;
        ScanJobInfo copy = PXCache<ScanJobInfo>.CreateCopy(current3);
        current3.ScanScreenID = str2;
        current3.ScanEntityNoteID = (Guid?) Graph.GetValue(str1, current2, "NoteID");
        current3.ScanEntityPrimaryViewName = primaryView;
        current3.ScanViewName = str1;
        current3.ScanEntityPrimaryParameters = string.Join(";", source1.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (k => $"{k.Key}|{k.Value}")));
        current3.ScanEntityParameters = string.Join(";", source2.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (k => $"{k.Key}|{k.Value}")));
        current3.FileName = source2.Count > 0 ? $"{string.Join("-", source2.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (x => x.Value.Trim())))}-{System.DateTime.Now.ToString("yyyyMMddHHmmssfff")}" : $"{string.Join("-", source1.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (x => x.Value.Trim())))}-{System.DateTime.Now.ToString("yyyyMMddHHmmssfff")}";
        instance.ScanJobInfoRecord.Cache.SetValueExt<ScanJobInfo.scannerID>((object) current3, (object) instance.GetDefaultScanner());
        instance.ScanJobInfoRecord.Cache.RaiseRowUpdated((object) instance.ScanJobInfoRecord.Current, (object) copy);
        throw new PXPopupRedirectException((PXGraph) instance, "ScanJob", true);
      }
      return adapter.Get();
    });
    PXButtonAttribute instance1 = PXEventSubscriberAttribute.CreateInstance<PXButtonAttribute>();
    instance1.DisplayOnMainToolbar = false;
    instance1.PopupVisible = false;
    PXUIFieldAttribute instance2 = PXEventSubscriberAttribute.CreateInstance<PXUIFieldAttribute>();
    instance2.DisplayName = "Scan";
    instance2.Visible = false;
    instance2.MapEnableRights = PXCacheRights.Select;
    instance2.MapViewRights = PXCacheRights.Select;
    PXNamedAction.AddAction(Graph, Graph.PrimaryItemType, "Scan", "Scan", handler, new List<PXEventSubscriberAttribute>()
    {
      (PXEventSubscriberAttribute) instance1,
      (PXEventSubscriberAttribute) instance2
    }.ToArray());
  }

  private Guid? GetDefaultScanner()
  {
    return SMScanner.PK.Find((PXGraph) this, (Guid?) ((UserPreferences) PXSelectBase<UserPreferences, PXSelect<UserPreferences, Where<UserPreferences.userID, Equal<Required<UserPreferences.userID>>>>.Config>.Select((PXGraph) this, (object) this.Accessinfo.UserID))?.DefaultScannerID)?.ScannerID;
  }
}
