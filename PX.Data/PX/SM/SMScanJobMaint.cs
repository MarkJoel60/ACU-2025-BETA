// Decompiled with JetBrains decompiler
// Type: PX.SM.SMScanJobMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Owin.DeviceHub;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.SM;

public class SMScanJobMaint : PXGraph<SMScanJobMaint, SMScanJob>
{
  public PXFilter<SMScanJobFilter> Filter;
  public PXSelect<SMScanJob, Where<Where2<Where<SMScanJob.createdDateTime, GreaterEqual<Current<SMScanJobFilter.startDate>>, Or<Current<SMScanJobFilter.startDate>, IsNull>>, And2<Where<SMScanJob.createdDateTime, Less<Current<SMScanJobFilter.endDatePlusOne>>, Or<Current<SMScanJobFilter.endDate>, IsNull>>, And<Where2<Where<Current<SMScanJobFilter.hideProcessed>, Equal<PX.Data.True>, And<SMScanJob.status, NotEqual<ScanJobStatus.processed>>>, Or<Current<SMScanJobFilter.hideProcessed>, NotEqual<PX.Data.True>>>>>>>, OrderBy<Desc<SMScanJob.createdDateTime>>> ScanJob;
  public PXSelect<SMScanJobParameter, Where<SMScanJobParameter.scanJobID, Equal<Current<SMScanJob.scanJobID>>>> Parameters;
  private const string DeviceHubFeatureName = "PX.Objects.CS.FeaturesSet+deviceHub";

  [InjectDependency]
  internal IDeviceHubService DeviceHubService { get; set; }

  public SMScanJobMaint()
  {
    this.Insert.SetVisible(false);
    this.Delete.SetVisible(false);
    this.First.SetVisible(false);
    this.Previous.SetVisible(false);
    this.Next.SetVisible(false);
    this.Last.SetVisible(false);
    this.CopyPaste.SetVisible(false);
    if (this.IsImport)
      this.Views[nameof (ScanJob)] = this.ScanJob.View = new PXView((PXGraph) this, this.ScanJob.View.IsReadOnly, (BqlCommand) new PX.Data.Select<SMScanJob>());
    this.ScanJob.Cache.AllowInsert = false;
    this.ScanJob.AllowUpdate = this.IsContractBasedAPI;
    this.Parameters.Cache.AllowInsert = false;
    this.Parameters.AllowUpdate = false;
    this.ScanJob.Cache.AllowDelete = PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub");
  }

  public static async Task<int?> CreateScanJob(
    ScanJobInfo scanJobInfo,
    CancellationToken cancellationToken)
  {
    int? scanJob = new int?();
    if (PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub"))
    {
      if (scanJobInfo.ScannerID.HasValue)
      {
        int? nullable = scanJobInfo.PaperSource;
        if (nullable.HasValue)
        {
          nullable = scanJobInfo.PixelType;
          if (nullable.HasValue)
          {
            nullable = scanJobInfo.Resolution;
            if (nullable.HasValue)
            {
              nullable = scanJobInfo.FileType;
              if (nullable.HasValue)
              {
                using (PXTransactionScope ts = new PXTransactionScope())
                {
                  scanJob = await PXGraph.CreateInstance<SMScanJobMaint>().AddScanJob(scanJobInfo, cancellationToken);
                  ts.Complete();
                }
                goto label_14;
              }
            }
          }
        }
      }
      throw new PXException("Select a scanner and specify its parameters.");
    }
label_14:
    return scanJob;
  }

  private async Task<int?> AddScanJob(ScanJobInfo scanJobInfo, CancellationToken cancellationToken)
  {
    SMScanJobMaint graph = this;
    int? ScanJobID = new int?();
    if (PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub"))
    {
      SMScanner smScanner = (SMScanner) PXSelectBase<SMScanner, PXSelect<SMScanner, Where<SMScanner.scannerID, Equal<Required<SMScanner.scannerID>>>>.Config>.Select((PXGraph) graph, (object) scanJobInfo.ScannerID);
      if (smScanner == null)
        throw new PXException("The {0} scanner is not found.", new object[1]
        {
          (object) scanJobInfo.ScannerID
        });
      SMScanJob instance1 = (SMScanJob) graph.ScanJob.Cache.CreateInstance();
      instance1.DeviceHubID = smScanner.DeviceHubID;
      instance1.ScannerName = smScanner.ScannerName;
      instance1.EntityScreenID = scanJobInfo.ScanScreenID;
      instance1.EntityNoteID = scanJobInfo.ScanEntityNoteID;
      instance1.EntityPrimaryViewName = scanJobInfo.ScanEntityPrimaryViewName;
      instance1.Status = "S";
      instance1.PaperSource = scanJobInfo.PaperSource;
      instance1.PixelType = scanJobInfo.PixelType;
      instance1.Resolution = scanJobInfo.Resolution;
      instance1.FileType = scanJobInfo.FileType;
      instance1.FileName = graph.CorrectedFileName(scanJobInfo.FileName);
      SMScanJob scanJob = graph.ScanJob.Insert(instance1);
      string primaryParameters = scanJobInfo.ScanEntityPrimaryParameters;
      char[] separator1 = new char[1]{ ';' };
      foreach (KeyValuePair<string, string> keyValuePair in ((IEnumerable<string>) primaryParameters.Split(separator1, StringSplitOptions.RemoveEmptyEntries)).Select<string, string[]>((Func<string, string[]>) (part => part.Split('|'))).ToDictionary<string[], string, string>((Func<string[], string>) (split => split[0]), (Func<string[], string>) (split => split[1])))
      {
        if (keyValuePair.Value != null)
        {
          SMScanJobParameter instance2 = (SMScanJobParameter) graph.Parameters.Cache.CreateInstance();
          instance2.ViewName = scanJobInfo.ScanEntityPrimaryViewName;
          instance2.ParameterName = keyValuePair.Key;
          instance2.ParameterValue = keyValuePair.Value;
          graph.Parameters.Insert(instance2);
        }
      }
      string entityParameters = scanJobInfo.ScanEntityParameters;
      char[] separator2 = new char[1]{ ';' };
      foreach (KeyValuePair<string, string> keyValuePair in ((IEnumerable<string>) entityParameters.Split(separator2, StringSplitOptions.RemoveEmptyEntries)).Select<string, string[]>((Func<string, string[]>) (part => part.Split('|'))).ToDictionary<string[], string, string>((Func<string[], string>) (split => split[0]), (Func<string[], string>) (split => split[1])))
      {
        if (keyValuePair.Value != null)
        {
          SMScanJobParameter instance3 = (SMScanJobParameter) graph.Parameters.Cache.CreateInstance();
          instance3.ViewName = scanJobInfo.ScanViewName;
          instance3.ParameterName = keyValuePair.Key;
          instance3.ParameterValue = keyValuePair.Value;
          graph.Parameters.Insert(instance3);
        }
      }
      graph.Actions.PressSave();
      ScanJobID = (int?) graph.ScanJob.Current?.ScanJobID;
      await graph.DeviceHubService.SendScanJobs(scanJob.DeviceHubID, new PX.Owin.DeviceHub.ScanJob[1]
      {
        graph.ResolveScanJob(scanJob)
      }, cancellationToken);
    }
    return ScanJobID;
  }

  protected virtual PX.Owin.DeviceHub.ScanJob ResolveScanJob(SMScanJob scanJob)
  {
    PX.Owin.DeviceHub.ScanJob scanJob1 = new PX.Owin.DeviceHub.ScanJob();
    scanJob1.JobID = scanJob.ScanJobID.ToString();
    scanJob1.DeviceHubID = scanJob.DeviceHubID;
    scanJob1.Status = scanJob.Status;
    scanJob1.Version = scanJob.LastModifiedDateTime ?? System.DateTime.Now;
    scanJob1.ScannerName = scanJob.ScannerName;
    scanJob1.EntityScreenID = scanJob.EntityScreenID;
    scanJob1.EntityNoteID = scanJob.EntityNoteID ?? Guid.Empty;
    scanJob1.EntityPrimaryViewName = scanJob.EntityPrimaryViewName;
    scanJob1.PaperSource = scanJob.PaperSource.GetValueOrDefault();
    int? nullable = scanJob.PixelType;
    scanJob1.PixelType = nullable.GetValueOrDefault();
    nullable = scanJob.Resolution;
    scanJob1.Resolution = nullable.GetValueOrDefault();
    nullable = scanJob.FileType;
    scanJob1.FileType = nullable.GetValueOrDefault();
    scanJob1.FileName = scanJob.FileName;
    scanJob1.RequestingUserName = scanJob.RequestingUserName;
    PX.Owin.DeviceHub.ScanJob scanJob2 = scanJob1;
    foreach (SMScanJobParameter scanJobParameter in this.Parameters.View.SelectMultiBound((object[]) new SMScanJob[1]
    {
      scanJob
    }).RowCast<SMScanJobParameter>())
      scanJob2.AddParameter(scanJobParameter.ViewName, scanJobParameter.ParameterName, scanJobParameter.ParameterValue);
    return scanJob2;
  }

  protected virtual string CorrectedFileName(string sFileName)
  {
    if (string.IsNullOrEmpty(sFileName))
      return (string) null;
    string str = sFileName;
    foreach (char invalidFileNameChar in Path.GetInvalidFileNameChars())
      str = str.Replace(invalidFileNameChar, '_');
    return str;
  }

  protected virtual void _(Events.RowSelecting<SMScanJob> e)
  {
    using (new PXConnectionScope())
    {
      PXCache cache = e.Cache;
      SMScanJob row = e.Row;
      if (cache == null || row == null || string.IsNullOrEmpty(row.DeviceHubID) || string.IsNullOrEmpty(row.ScannerName))
        return;
      SMScanner smScanner = (SMScanner) PXSelectBase<SMScanner, PXSelect<SMScanner, Where<SMScanner.deviceHubID, Equal<Required<SMScanner.deviceHubID>>, And<SMScanner.scannerName, Equal<Required<SMScanner.scannerName>>>>>.Config>.Select((PXGraph) this, (object) row.DeviceHubID, (object) row.ScannerName);
      if (smScanner == null)
        return;
      row.PaperSourceList = smScanner.PaperSourceComboValues;
      row.PixelTypeList = smScanner.PixelTypeComboValues;
      row.ResolutionList = smScanner.ResolutionComboValues;
      row.FileTypeList = smScanner.FileTypeComboValues;
    }
  }

  protected virtual void _(Events.RowSelected<SMScanJob> e)
  {
    PXCache cache = e.Cache;
    SMScanJob row = e.Row;
    if (cache == null || row == null || string.IsNullOrEmpty(row.DeviceHubID) || string.IsNullOrEmpty(row.ScannerName))
      return;
    ComboBoxUtils.PopulateFileTypes<SMScanJob.paperSource>(cache, row.PaperSourceList, (object) row);
    ComboBoxUtils.PopulateFileTypes<SMScanJob.pixelType>(cache, row.PixelTypeList, (object) row);
    ComboBoxUtils.PopulateFileTypes<SMScanJob.resolution>(cache, row.ResolutionList, (object) row);
    ComboBoxUtils.PopulateFileTypes<SMScanJob.fileType>(cache, row.FileTypeList, (object) row);
  }
}
