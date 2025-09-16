// Decompiled with JetBrains decompiler
// Type: PX.SM.UpdateMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Archiver;
using PX.BulkInsert.Installer;
using PX.BulkInsert.Installer.DatabaseSetup;
using PX.Caching;
using PX.Common;
using PX.Data;
using PX.Data.Access.ActiveDirectory;
using PX.Data.Maintenance;
using PX.Data.Process;
using PX.Data.Update;
using PX.Data.Update.Storage;
using PX.DbServices.Model;
using PX.DbServices.Points.DbmsBase;
using PX.Reports;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Xml;

#nullable disable
namespace PX.SM;

public class UpdateMaint : PXGraph<UpdateMaint>
{
  protected static Dictionary<string, UpdateMaint.PXPackageInfo> packages = new Dictionary<string, UpdateMaint.PXPackageInfo>();
  public PXSelectReadonly<Version> VersionRecord;
  public PXFilter<VersionFilter> VersionFilterRecord;
  public PXFilter<UPLogFileFilter> LogFileFilterRecord;
  public PXFilter<LockoutFilter> LockoutFilterRecord;
  public PXSelectOrderBy<UPHistory, OrderBy<Desc<UPHistory.updateID>>> UpdateHistoryRecords;
  public PXSelectOrderBy<UPHistoryComponents, OrderBy<Desc<UPHistoryComponents.updateID>>> UpdateHistoryComponentsRecords;
  public PXSelectJoinOrderBy<UPHistory, InnerJoin<UPHistoryComponents, On<UPHistory.updateID, Equal<UPHistoryComponents.updateID>>>, OrderBy<Desc<UPHistoryComponents.updateComponentID>>> UpdateHistoryFullRecords;
  public PXSelectOrderBy<AvailableBranch, OrderBy<Asc<AvailableBranch.name>>> AvailableBranches;
  public PXSelectOrderBy<AvailableVersion, OrderBy<Desc<AvailableVersion.uploaded, Desc<AvailableVersion.restricted, Desc<AvailableVersion.version>>>>> AvailableVersions;
  public PXSelect<AvailableVersionItem, Where<AvailableVersionItem.version, Equal<Optional<AvailableVersion.version>>>> AvailableVersionItems;
  public PXSelect<AvailableVersionParameter, Where<AvailableVersionParameter.version, Equal<Optional<AvailableVersion.version>>>> AvailableVersionParameters;
  public PXSelect<UPErrors, Where<UPErrors.updateID, Equal<Current<UPHistory.updateID>>>> UpdateErrorRecords;
  public PXCancel<Version> Cancel;
  public PXAction<Version> UploadVersionCommand;
  public PXAction<Version> DownloadVersionCommand;
  public PXAction<Version> ApplyVersionCommand;
  public PXAction<Version> DatabaseUpdateCommand;
  public PXAction<Version> RestartApplicationCommand;
  public PXAction<Version> ResetCachesCommand;
  public PXAction<Version> SkipErrorCommand;
  public PXAction<Version> ShowLogFileCommand;
  public PXAction<Version> ClearLogFileCommand;
  public PXAction<Version> ScheduleLockoutCommand;
  public PXAction<Version> StopLockoutCommand;
  private static PointDbmsBase _dbPoint;
  public PXSelect<Version> UploadVersionPanel;
  public PXAction<Version> ValidateCompatibility;
  public PXAction<Version> UpdateStatisticsCommand;

  private static PointDbmsBase _point
  {
    get
    {
      if (UpdateMaint._dbPoint == null)
        UpdateMaint._dbPoint = PXDatabase.Provider.CreateDbServicesPoint();
      return UpdateMaint._dbPoint;
    }
  }

  [InjectDependency]
  private IEnumerable<ICacheControl> CacheControls { get; set; }

  [InjectDependency]
  private ISessionContextFactory SessionContextFactory { get; set; }

  [InjectDependency]
  private IScheduleProcessorService ScheduleProcessorService { get; set; }

  [InjectDependency]
  private IAppRestartService AppRestartService { get; set; }

  [InjectDependency]
  private IActiveDirectoryProvider ActiveDirectoryProvider { get; set; }

  [InjectDependency]
  private IPXPageIndexingService PageIndexingService { get; set; }

  [InjectDependency]
  private ISiteMapUITypeProvider SiteMapUITypeProvider { get; set; }

  public UpdateMaint()
  {
    this.VersionRecord.Cache.AllowInsert = false;
    this.VersionRecord.Cache.AllowUpdate = false;
    this.UpdateHistoryRecords.Cache.AllowInsert = false;
    this.UpdateHistoryRecords.Cache.AllowUpdate = false;
    this.UpdateHistoryRecords.Cache.AllowDelete = false;
    this.UpdateErrorRecords.Cache.AllowInsert = false;
    this.UpdateErrorRecords.Cache.AllowUpdate = false;
    this.UpdateErrorRecords.Cache.AllowDelete = false;
    this.DatabaseUpdateCommand.SetEnabled(PXUpdateHelper.UpdateSupported);
    if (!PXUpdateLog.HasLog())
    {
      this.ShowLogFileCommand.SetEnabled(false);
      this.ClearLogFileCommand.SetEnabled(false);
    }
    this.ValidateCompatibility.SetVisible(WebConfig.CheckCustomizationCompatibility);
    this.ValidateCompatibility.StateSelectingEvents += new PXFieldSelecting(this.ValidateCompatibilityEnabled);
  }

  protected virtual IEnumerable availableVersions()
  {
    VersionFilter versionFilter = this.EnshureFilter();
    if (((int) versionFilter.RefreshBuildsRequired ?? 1) == 0)
    {
      this.CheckDownloadedStatus();
      foreach (AvailableVersion availableVersion in this.AvailableVersions.Cache.Inserted)
        yield return (object) availableVersion;
    }
    else
    {
      IEnumerable<AvailableVersion> uploaded = this.CheckUploaded();
      this.AvailableVersions.Cache.Clear();
      this.AvailableVersionItems.Cache.Clear();
      this.AvailableVersionParameters.Cache.Clear();
      foreach (AvailableVersion availableVersion in uploaded.Union<AvailableVersion>(this.GetVersions(versionFilter == null ? (string) null : versionFilter.Key).Where<AvailableVersion>((Func<AvailableVersion, bool>) (v => !uploaded.Any<AvailableVersion>((Func<AvailableVersion, bool>) (u => this.AvailableVersions.Cache.ObjectsEqual((object) v, (object) u)))))))
      {
        this.AvailableVersions.Insert(availableVersion);
        yield return (object) availableVersion;
      }
      this.EnshureFilter().RefreshBuildsRequired = new bool?(false);
      this.AvailableVersions.Cache.IsDirty = false;
    }
  }

  protected virtual void CheckDownloadedStatus()
  {
    if (!(PXLongOperation.GetCustomInfo(this.UID) is UpdateMaint.PXPackageInfo customInfo))
      return;
    foreach (AvailableVersion availableVersion in this.AvailableVersions.Cache.Inserted)
    {
      if (availableVersion.Version == customInfo.Version)
      {
        availableVersion.PackageKey = customInfo.PackageKey;
        availableVersion.Type = customInfo.Type.ToString();
        availableVersion.Launcher = customInfo.Launcher;
        availableVersion.Notes = customInfo.Notes;
        availableVersion.Description = customInfo.Description;
        availableVersion.Restricted = new bool?(customInfo.Restricted);
        this.FillAvailableVersionDetails(customInfo);
        break;
      }
    }
    PXLongOperation.ClearStatus(this.UID);
  }

  protected virtual IEnumerable<AvailableVersion> CheckUploaded()
  {
    foreach (UpdateMaint.PXPackageInfo info in UpdateMaint.packages.Values)
    {
      AvailableVersion availableVersion = new AvailableVersion();
      availableVersion.PackageKey = info.PackageKey;
      availableVersion.Version = info.Version;
      availableVersion.Date = new System.DateTime?(info.Date);
      availableVersion.Type = info.Type.ToString();
      availableVersion.Launcher = info.Launcher;
      availableVersion.Restricted = new bool?(info.Restricted);
      availableVersion.Description = info.Description;
      availableVersion.Notes = info.Notes;
      this.FillAvailableVersionDetails(info);
      yield return availableVersion;
    }
  }

  protected virtual void FillAvailableVersionDetails(UpdateMaint.PXPackageInfo info)
  {
    foreach (UpdateMaint.PXPackageItemInfo pxPackageItemInfo in info.Items)
    {
      AvailableVersionItem availableVersionItem = new AvailableVersionItem()
      {
        Version = info.Version,
        Name = pxPackageItemInfo.Name,
        Path = pxPackageItemInfo.Key ?? pxPackageItemInfo.Path
      };
      if (this.AvailableVersionItems.Insert(availableVersionItem) == null)
        this.AvailableVersionItems.Update(availableVersionItem);
    }
    foreach (UpdateMaint.PXPackageParameterInfo parameter in info.Parameters)
    {
      AvailableVersionParameter versionParameter = new AvailableVersionParameter()
      {
        Version = info.Version,
        Name = parameter.Name,
        Value = parameter.Value
      };
      if (this.AvailableVersionParameters.Insert(versionParameter) == null)
        this.AvailableVersionParameters.Update(versionParameter);
    }
    this.AvailableVersions.Cache.IsDirty = false;
    this.AvailableVersionItems.Cache.IsDirty = false;
    this.AvailableVersionParameters.Cache.IsDirty = false;
  }

  protected virtual void VersionFilter_Key_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.EnshureFilter().RefreshBuildsRequired = new bool?(true);
  }

  protected virtual string CurrentVersion
  {
    get
    {
      System.Version vers = new System.Version(IEnumerableExtensions.GetPrimaryVersion(PXVersionHelper.GetDatabaseVersions()).Version);
      System.Version assemblyVersion = PXVersionHelper.GetAssemblyVersion();
      return assemblyVersion == new System.Version(0, 0, 0, 0) || assemblyVersion == new System.Version(1, 0, 0, 0) || assemblyVersion.CompareTo(vers) >= 0 ? vers.ToString(true) : assemblyVersion.ToString(true);
    }
  }

  protected virtual VersionFilter EnshureFilter()
  {
    VersionFilter current = this.VersionFilterRecord.Current;
    if (current != null)
      return current;
    return this.VersionFilterRecord.Insert(new VersionFilter()
    {
      RefreshBuildsRequired = new bool?(true)
    });
  }

  protected void SetupLockoutButtons()
  {
    if (PXContext.PXIdentity.User.IsInRole(((IEnumerable<string>) PXAccess.GetAdministratorRoles()).First<string>()))
    {
      bool flag = PXSiteLockout.GetStatus() != 0;
      this.ScheduleLockoutCommand.SetVisible(!flag);
      this.ScheduleLockoutCommand.SetEnabled(!flag);
      this.StopLockoutCommand.SetVisible(flag);
      this.StopLockoutCommand.SetEnabled(flag);
    }
    else
    {
      this.ScheduleLockoutCommand.SetVisible(false);
      this.ScheduleLockoutCommand.SetEnabled(false);
      this.StopLockoutCommand.SetVisible(false);
      this.StopLockoutCommand.SetEnabled(false);
    }
  }

  protected virtual void LockoutFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    this.SetupLockoutButtons();
  }

  protected virtual void UPHistory_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    this.SkipErrorCommand.SetEnabled(this.UpdateErrorRecords.SelectSingle() != null);
  }

  protected virtual void VersionFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    AvailableVersion current = this.AvailableVersions.Current;
    int num = current == null ? 0 : (current.Uploaded.GetValueOrDefault() ? 1 : 0);
    if (!(e.Row is VersionFilter row))
      return;
    this.SetupLockoutButtons();
    if (!WebConfig.RestrictUpdates)
      return;
    sender.RaiseExceptionHandling<VersionFilter.key>(e.Row, (object) row.Key, (Exception) new PXSetPropertyException("Application updates have been disabled in web.config.", PXErrorLevel.Warning));
    this.UploadVersionCommand.SetEnabled(false);
    this.DownloadVersionCommand.SetEnabled(false);
    this.ApplyVersionCommand.SetEnabled(false);
  }

  protected void AvailableVersion_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    AvailableVersion row = e.Row as AvailableVersion;
  }

  [PXButton(ImageKey = "CheckIn", Tooltip = "Display the dialog box to upload a new version of the application.")]
  [PXUIField(DisplayName = "Upload Custom Package", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable uploadVersionCommand(PXAdapter adapter)
  {
    if (this.IsNewUi())
    {
      if (DialogManager.AskExt(adapter.View.Graph, this.AvailableVersions.Name, (string) null, (DialogManager.InitializePanel) null, false) == WebDialogResult.OK)
        this.OnPackageUploaded(PXContext.SessionTyped<PXSessionStatePXData>().FileInfo["UploadFileSessionKey"].BinData);
    }
    else
    {
      int num = (int) this.UploadVersionPanel.AskExt();
    }
    return adapter.Get();
  }

  private bool IsNewUi()
  {
    return string.Equals(this.SiteMapUITypeProvider.GetUIByScreenId(PXSiteMap.Provider.FindSiteMapNodeByGraphTypeUnsecure(typeof (UpdateMaint).FullName).ScreenID), "T", StringComparison.InvariantCultureIgnoreCase);
  }

  [PXButton(ImageKey = "GetFile", Tooltip = "Download the selected version.")]
  [PXUIField(DisplayName = "Download Package", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable downloadVersionCommand(PXAdapter adapter)
  {
    VersionFilter filter = this.EnshureFilter();
    AvailableVersion version = this.AvailableVersions.Current;
    if (version == null || string.IsNullOrEmpty(version.Version))
      throw new PXException("Select the version you want to download.");
    if (version.Uploaded.GetValueOrDefault())
      throw new PXException("The package has already been uploaded.");
    PXLongOperation.StartOperation(this.UID, (PXToggleAsyncDelegate) (() => UpdateMaint.DownloadBuild(version, filter.Key)));
    return adapter.Get();
  }

  protected static void DownloadBuild(AvailableVersion version, string key)
  {
    string str = Guid.NewGuid().ToString() + ".aup";
    PXUpdateServer.DownloadVersion(str, version.Version, key);
    PXLongOperation.SetCustomInfo((object) UpdateMaint.ValidatePackage(str, version));
  }

  [PXButton(Tooltip = "Repair the current database.", Category = "Actions")]
  [PXUIField(DisplayName = "Repair Database", Enabled = false, MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable databaseUpdateCommand(PXAdapter adapter)
  {
    if (this.VersionRecord.Ask("Update Confirmation", "It is highly recommended that you do a full backup of the database before you continue with upgrade. Click Yes if you want to continue.", MessageButtons.YesNo) != WebDialogResult.Yes)
      return adapter.Get();
    if (!PXUpdateHelper.UpdateSupported)
      throw new PXException("The update is not supported.");
    PXUpdateHelper.Update(true);
    throw new PXRedirectToUrlException("~/frames/Maintenance.aspx", PXBaseRedirectException.WindowMode.Base, "Upgrade Complete");
  }

  [PXButton(Tooltip = "Restart the current application.", Category = "Actions", DisplayOnMainToolbar = true)]
  [PXUIField(DisplayName = "Restart Application", Enabled = false, MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable restartApplicationCommand(PXAdapter adapter)
  {
    if (this.VersionRecord.Ask("Update Confirmation", "During this operation, all running processes will be stopped and all unsaved data will be discarded. Click Yes if you want to continue.", MessageButtons.YesNo) != WebDialogResult.Yes)
      return adapter.Get();
    this.AppRestartService.RequestRestart();
    throw new PXRedirectToUrlException("~/frames/Maintenance.aspx", PXBaseRedirectException.WindowMode.Base, "Upgrade Complete");
  }

  [PXButton(Tooltip = "Reset all caches in the system.", Category = "Actions")]
  [PXUIField(DisplayName = "Reset Caches", Enabled = false, MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable resetCachesCommand(PXAdapter adapter)
  {
    this.ActiveDirectoryProvider.Reset();
    PXAccess.ForEachTenantAsAdmin(new System.Action(this.ResetCachesForCompany));
    PXDatabase.ClearCompanyCache();
    this.PageIndexingService.Clear();
    PXMessages.ClearMessagePrefixes();
    this.SessionContextFactory.Abandon();
    ReportStorageHelper.Clear();
    PXBuildManager.ClearTypeCache();
    throw new PXRefreshException();
  }

  [PXInternalUseOnly]
  protected virtual void ResetCachesForCompany()
  {
    PXAccess.Clear();
    PXSiteMap.Provider.Clear();
    PXDatabase.ResetSlots();
    foreach (ICacheControl cacheControl in this.CacheControls)
      cacheControl.InvalidateCache();
  }

  [PXButton(ImageKey = "Save", Tooltip = "Update your site to the selected version.")]
  [PXUIField(DisplayName = "Install Update", Enabled = false, MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable applyVersionCommand(PXAdapter adapter)
  {
    this.EnshureFilter();
    AvailableVersion version = this.AvailableVersions.Current;
    if (version == null)
      throw new PXException("Select the version to which you want to upgrade.");
    if (!version.Uploaded.GetValueOrDefault() || string.IsNullOrEmpty(version.Version) || string.IsNullOrEmpty(version.Type))
      throw new PXException("You should download the version before applying it.");
    if (PXVersionHelper.Compare(this.CurrentVersion, version.Version) > 0)
      throw new PXException("It is not possible to upgrade to an earlier version.");
    if ((PXInstanceType) Enum.Parse(typeof (PXInstanceType), version.Type) != PXInstanceHelper.CurrentInstanceType)
      throw new PXException("The package has an incorrect application type.");
    this.CheckVersionForAbilityToUpgrade();
    if (WebConfig.CheckCustomizationCompatibility)
    {
      int? validationStatus = version.ValidationStatus;
      if (!validationStatus.HasValue)
      {
        object obj = PXContext.Session.LongOpCustomInfo[version.PackageKey];
        if (obj != null)
          version.ValidationStatus = new int?((int) obj);
      }
      validationStatus = version.ValidationStatus;
      int error = AvailableVersion.validationStatus.Error;
      if (validationStatus.GetValueOrDefault() == error & validationStatus.HasValue)
        throw new PXException("The selected update is not compatible with the customization published on the website.");
      validationStatus = version.ValidationStatus;
      int ok = AvailableVersion.validationStatus.OK;
      if (!(validationStatus.GetValueOrDefault() == ok & validationStatus.HasValue) && UpdateMaint.HasPublishedCustomization())
        throw new PXException("The website contains a published customization. Click Validate Customization to validate the compatibility of the selected update with the customization.");
    }
    if (this.VersionRecord.Ask("Update Confirmation", "The site will be out of service during the update. Please confirm that you want to proceed.", MessageButtons.YesNo) != WebDialogResult.Yes)
      return adapter.Get();
    IEnumerable<AvailableVersionItem> source1 = this.AvailableVersionItems.Cache.Inserted.Cast<AvailableVersionItem>().Where<AvailableVersionItem>((Func<AvailableVersionItem, bool>) (i => i.Version == version.Version));
    IEnumerable<AvailableVersionParameter> source2 = this.AvailableVersionParameters.Cache.Inserted.Cast<AvailableVersionParameter>().Where<AvailableVersionParameter>((Func<AvailableVersionParameter, bool>) (i => i.Version == version.Version));
    PXUpgradeSpec spec = new PXUpgradeSpec();
    spec.Launcher = version.Launcher;
    spec.CurrentVersion = new System.Version(IEnumerableExtensions.GetPrimaryVersion(PXVersionHelper.GetDatabaseVersions()).Version);
    spec.DestinationVersion = PXVersionHelper.Convert(version.Version);
    spec.ZipPath = PXStorageHelper.GetAppDataProvider().GetPath(version.PackageKey);
    spec.ZipFiles = source1.ToDictionary<AvailableVersionItem, string, string>((Func<AvailableVersionItem, string>) (k => k.Name), (Func<AvailableVersionItem, string>) (v => v.Path));
    spec.Parameters = source2.ToDictionary<AvailableVersionParameter, string, string>((Func<AvailableVersionParameter, string>) (k => k.Name), (Func<AvailableVersionParameter, string>) (v => v.Value));
    PXUpgradeHelper.ValidateUpdate(spec);
    PXUpgradeHelper.LaunchUpgrade(spec);
    Thread.Sleep(10000);
    throw new PXRedirectToUrlException("~/frames/Maintenance.aspx", PXBaseRedirectException.WindowMode.Base, "Upgrade Complete");
  }

  private void CheckVersionForAbilityToUpgrade()
  {
    AvailableVersion current = this.AvailableVersions.Current;
    string path = PXStorageHelper.GetAppDataProvider().GetPath(current.PackageKey);
    string str = Path.Combine(HttpContext.Current.Server.MapPath("~/"), "CheckVersionTemp");
    using (ZipArchiveWrapper zipArchiveWrapper1 = new ZipArchiveWrapper(path, FileMode.Open, FileAccess.Read))
    {
      using (ZipArchiveWrapper zipArchiveWrapper2 = new ZipArchiveWrapper(zipArchiveWrapper1.GetStream("Database.zip")))
      {
        if (Directory.Exists(str))
          Directory.Delete(str, true);
        zipArchiveWrapper2.Decompress(str, (IEnumerable<string>) null);
      }
    }
    DatabasePayloadReader databasePayloadReader = new DatabasePayloadReader(str, "ERPDatabaseSetup.adc", Path.Combine(str, "Data"));
    try
    {
      InstallationCommon.IsAllowToUpgrade<PXException>(this.CurrentVersion, current.Version, databasePayloadReader.KnownVersions);
    }
    finally
    {
      Directory.Delete(str, true);
    }
  }

  [PXButton(Tooltip = "Validate that the published customization is compatible with the selected update version.")]
  [PXUIField(DisplayName = "Validate Customization", Enabled = true)]
  protected void validateCompatibility()
  {
    this.EnshureFilter();
    AvailableVersion version = this.AvailableVersions.Current;
    if (version == null)
      throw new PXException("Select the version to which you want to upgrade.");
    if (!version.Uploaded.GetValueOrDefault() || string.IsNullOrEmpty(version.Version) || string.IsNullOrEmpty(version.Type))
      throw new PXException("You should download the version before applying it.");
    if (PXVersionHelper.Compare(this.CurrentVersion, version.Version) > 0)
      throw new PXException("It is not possible to upgrade to an earlier version.");
    if ((PXInstanceType) Enum.Parse(typeof (PXInstanceType), version.Type) != PXInstanceHelper.CurrentInstanceType)
      throw new PXException("The package has an incorrect application type.");
    IEnumerable<AvailableVersionItem> source1 = this.AvailableVersionItems.Cache.Inserted.Cast<AvailableVersionItem>().Where<AvailableVersionItem>((Func<AvailableVersionItem, bool>) (i => i.Version == version.Version));
    IEnumerable<AvailableVersionParameter> source2 = this.AvailableVersionParameters.Cache.Inserted.Cast<AvailableVersionParameter>().Where<AvailableVersionParameter>((Func<AvailableVersionParameter, bool>) (i => i.Version == version.Version));
    string zipPath = new PXUpgradeSpec()
    {
      Launcher = version.Launcher,
      CurrentVersion = new System.Version(IEnumerableExtensions.GetPrimaryVersion(PXVersionHelper.GetDatabaseVersions()).Version),
      DestinationVersion = PXVersionHelper.Convert(version.Version),
      ZipPath = PXStorageHelper.GetAppDataProvider().GetPath(version.PackageKey),
      ZipFiles = source1.ToDictionary<AvailableVersionItem, string, string>((Func<AvailableVersionItem, string>) (k => k.Name), (Func<AvailableVersionItem, string>) (v => v.Path)),
      Parameters = source2.ToDictionary<AvailableVersionParameter, string, string>((Func<AvailableVersionParameter, string>) (k => k.Name), (Func<AvailableVersionParameter, string>) (v => v.Value))
    }.ZipPath;
    string key = version.PackageKey;
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() =>
    {
      try
      {
        UpdateMaint.CheckCustomizationCompatibility(zipPath);
        PXLongOperation.SetCustomInfo((object) AvailableVersion.validationStatus.OK, key);
      }
      catch (Exception ex)
      {
        PXLongOperation.SetCustomInfo((object) AvailableVersion.validationStatus.Error, key);
        PXLongOperation.SetCustomInfo((object) new UpdateMaint.ValidationFailed()
        {
          Text = ex.Message
        });
        throw new PXException("Validation Failed");
      }
    }));
  }

  private void ValidateCompatibilityEnabled(PXCache sender, PXFieldSelectingEventArgs args)
  {
    this.ValidateCompatibility.SetEnabled(UpdateMaint.HasPublishedCustomization());
  }

  private static bool HasPublishedCustomization()
  {
    return File.Exists(HostingEnvironment.MapPath("~/app_data/CustomizationPublishedDoc.zip"));
  }

  private static void CheckCustomizationCompatibility(string zipPath)
  {
    if (!WebConfig.CheckCustomizationCompatibility || !File.Exists(HostingEnvironment.MapPath("~/app_data/CustomizationPublishedDoc.zip")))
      return;
    string str1 = Path.Combine(WebConfig.CustomizationTempFilesPath, "PackageFiles");
    using (ZipArchiveWrapper zipArchiveWrapper1 = new ZipArchiveWrapper(zipPath, FileMode.Open, FileAccess.Read))
    {
      using (ZipArchiveWrapper zipArchiveWrapper2 = new ZipArchiveWrapper(zipArchiveWrapper1.GetStream("Files.zip")))
      {
        if (Directory.Exists(str1))
          Directory.Delete(str1, true);
        zipArchiveWrapper2.Decompress(str1, (IEnumerable<string>) null);
      }
    }
    string arguments = $"/method ValidateCustomization /src \"{HostingEnvironment.ApplicationPhysicalPath.TrimEnd('\\')}\" /website \"{str1.TrimEnd('\\')}\" /CustomizationTempFilesPath \"{WebConfig.CustomizationTempFilesPath.TrimEnd('\\')}\"";
    string str2 = Path.Combine(str1, "PX.CommandLine.exe");
    if (!File.Exists(str2))
      str2 = HostingEnvironment.MapPath("~/bin/PX.CommandLine.exe");
    ProcessStartInfo startInfo = new ProcessStartInfo(str2, arguments)
    {
      UseShellExecute = false,
      RedirectStandardOutput = true
    };
    WindowsImpersonationContext impersonationContext = WindowsIdentity.Impersonate(IntPtr.Zero);
    try
    {
      using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(startInfo))
      {
        string end = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        if (process.ExitCode != 0)
          throw new PXException(end);
      }
      Directory.Delete(str1, true);
    }
    finally
    {
      impersonationContext.Undo();
    }
  }

  [PXButton(ImageKey = "Remove", Tooltip = "Mark this exception as skipped, so that it will not appear on the login screen.")]
  [PXUIField(DisplayName = "Skip Error", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable skipErrorCommand(PXAdapter adapter)
  {
    UPErrors current = this.UpdateErrorRecords.Current;
    if (current != null)
    {
      current.Skip = new bool?(true);
      this.UpdateErrorRecords.Update(current);
      this.Persist(typeof (UPErrors), PXDBOperation.Update);
      this.UpdateErrorRecords.Cache.IsDirty = false;
    }
    PXFileStatusWriter.ClearUpdateStatus();
    return adapter.Get();
  }

  [PXButton(ImageKey = "Note", Tooltip = "Show the log of upgrade operations.")]
  [PXUIField(DisplayName = "Show Log File", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable showLogFileCommand(PXAdapter adapter)
  {
    UPLogFileFilter current = this.LogFileFilterRecord.Current;
    if (current == null)
      this.LogFileFilterRecord.Insert(new UPLogFileFilter());
    current.Text = PXUpdateLog.GetDefaultLog();
    this.LogFileFilterRecord.Update(current);
    int num = (int) this.LogFileFilterRecord.AskExt();
    return adapter.Get();
  }

  [PXButton(Tooltip = "Schedule the lockout.", Category = "Actions", DisplayOnMainToolbar = true)]
  [PXUIField(DisplayName = "Schedule Maintenance", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable scheduleLockoutCommand(PXAdapter adapter)
  {
    LockoutFilter current = this.LockoutFilterRecord.Current;
    if (PXSiteLockout.GetStatus(true) != PXSiteLockout.Status.Free)
    {
      int num = (int) this.LockoutFilterRecord.Ask(current, string.Empty, "The scheduled lockout already exists.", MessageButtons.OK);
      return adapter.Get();
    }
    System.DateTime? dateTime = current.DateTime;
    if (!dateTime.HasValue)
      current.DateTime = new System.DateTime?(PXTimeZoneInfo.Now);
    if (current.Reason == null)
      current.Reason = PXMessages.LocalizeNoPrefix("Maintenance");
    if (this.LockoutFilterRecord.AskExt().IsPositive())
    {
      dateTime = current.DateTime;
      PXSiteLockout.Lock(dateTime.Value, current.Reason, current.LockoutAll.Value);
    }
    return adapter.Get();
  }

  [PXButton(Tooltip = "Stop the lockout.", Category = "Actions", DisplayOnMainToolbar = true)]
  [PXUIField(DisplayName = "Stop Maintenance", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable stopLockoutCommand(PXAdapter adapter)
  {
    PXSiteLockout.LockoutType lockoutType = PXSiteLockout.LockoutType.Instance | PXSiteLockout.LockoutType.Global;
    if ((PXSiteLockout.GetLockoutType(true) & lockoutType) == lockoutType)
    {
      if (this.LockoutFilterRecord.Ask("The instance lockout has been stopped, but a global lockout has been scheduled from another instance. Do you want to cancel it?", MessageButtons.YesNo) == WebDialogResult.Yes)
        PXSiteLockout.Unlock(true);
      else
        PXSiteLockout.Unlock(false);
    }
    else
      PXSiteLockout.Unlock(true);
    this.ScheduleProcessorService.ClearLoggedSkippedSchedules();
    return adapter.Get();
  }

  [PXButton(ImageKey = "Note", Tooltip = "Clear the log of upgrade operations.")]
  [PXUIField(DisplayName = "Clear Log File", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable clearLogFileCommand(PXAdapter adapter)
  {
    PXUpdateLog.ClearDefaultLog();
    return adapter.Get();
  }

  [PXButton(Tooltip = "Update Statistics", Category = "Actions")]
  [PXUIField(DisplayName = "Update Statistics", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  public IEnumerable updateStatisticsCommand(PXAdapter adapter)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    PXLongOperation.StartOperation((PXGraph) this, UpdateMaint.\u003C\u003EO.\u003C0\u003E__UpdateStatistics ?? (UpdateMaint.\u003C\u003EO.\u003C0\u003E__UpdateStatistics = new PXToggleAsyncDelegate(UpdateMaint.UpdateStatistics)));
    return adapter.Get();
  }

  private static void UpdateStatistics() => UpdateMaint._point.getDbKeeper().UpdateStatistic();

  public void OnPackageUploaded(string fileName, string password, byte[] content)
  {
    this.OnPackageUploaded(content);
  }

  public void OnPackageUploaded(byte[] content)
  {
    string str = Guid.NewGuid().ToString() + ".aup";
    PXStorageHelper.GetAppDataProvider()[str] = content;
    UpdateMaint.PXPackageInfo info = UpdateMaint.ValidatePackage(str);
    if (info == null)
    {
      PXStorageHelper.GetAppDataProvider().Delete(str);
    }
    else
    {
      AvailableVersion availableVersion = new AvailableVersion();
      availableVersion.PackageKey = info.PackageKey;
      availableVersion.Version = info.Version;
      availableVersion.Date = new System.DateTime?(info.Date);
      availableVersion.Type = info.Type.ToString();
      availableVersion.Launcher = info.Launcher;
      if (this.AvailableVersions.Insert(availableVersion) == null)
        this.AvailableVersions.Update(availableVersion);
      this.FillAvailableVersionDetails(info);
    }
  }

  private static UpdateMaint.PXPackageInfo ValidatePackage(string key)
  {
    if (!PXStorageHelper.GetAppDataProvider().Exists(key))
      return (UpdateMaint.PXPackageInfo) null;
    UpdateMaint.PXPackageInfo pxPackageInfo = new UpdateMaint.PXPackageInfo();
    using (Stream stream = PXStorageHelper.GetAppDataProvider().OpenRead(key))
    {
      using (ZipArchiveWrapper archive = new ZipArchiveWrapper(stream))
      {
        pxPackageInfo.Manifest = archive["Manifest.xml"];
        using (MemoryStream inStream = new MemoryStream(pxPackageInfo.Manifest))
        {
          XmlDocument xmlDocument = new XmlDocument();
          xmlDocument.Load((Stream) inStream);
          XmlNode xmlNode1 = xmlDocument.SelectSingleNode("//pachageManifest/generalInfo");
          XmlAttribute attribute1 = xmlNode1 == null ? (XmlAttribute) null : xmlNode1.Attributes["type"];
          pxPackageInfo.Type = attribute1 != null && !string.IsNullOrEmpty(attribute1.Value) && ((IEnumerable<string>) Enum.GetNames(typeof (PXInstanceType))).Contains<string>(attribute1.Value) ? (PXInstanceType) Enum.Parse(typeof (PXInstanceType), attribute1.Value) : throw new PXException("The package has an incorrect application type.");
          if (pxPackageInfo.Type != PXInstanceHelper.CurrentInstanceType)
            throw new PXException("The package has an incorrect application type.");
          XmlAttribute attribute2 = xmlNode1 == null ? (XmlAttribute) null : xmlNode1.Attributes["version"];
          pxPackageInfo.Version = attribute2 != null && !string.IsNullOrEmpty(attribute2.Value) && PXVersionHelper.Validate(attribute2.Value) ? attribute2.Value : throw new PXException("The package version is not valid.");
          XmlAttribute attribute3 = xmlNode1 == null ? (XmlAttribute) null : xmlNode1.Attributes["dotNet"];
          if (attribute3 != null && !string.IsNullOrEmpty(attribute3.Value))
            pxPackageInfo.Parameters.Add(new UpdateMaint.PXPackageParameterInfo()
            {
              Name = "dotNet",
              Value = attribute3.Value
            });
          XmlNode xmlNode2 = xmlDocument.SelectSingleNode("//pachageManifest/items");
          if (xmlNode2 == null)
            throw new Exception("The package validation has failed.");
          foreach (XmlElement childNode in xmlNode2.ChildNodes)
          {
            XmlAttribute attribute4 = childNode.Attributes["name"];
            XmlAttribute attribute5 = childNode.Attributes["hash"];
            XmlAttribute attribute6 = childNode.Attributes["size"];
            if (attribute4 == null || string.IsNullOrEmpty(attribute4.Value) || attribute5 == null || string.IsNullOrEmpty(attribute5.Value))
              throw new Exception("The package validation has failed.");
            string id = (string) null;
            long result;
            byte[] calculatedHash;
            if (attribute6 != null && attribute6.Value != null && long.TryParse(attribute6.Value, out result) && result > 104857600L /*0x06400000*/)
            {
              id = UpdateMaint.ExtractItem(archive, attribute4.Value);
              calculatedHash = id != null ? new SHA1CryptoServiceProvider().ComputeHash(PXStorageHelper.GetAppDataProvider().OpenRead(id)) : throw new Exception("The package validation has failed.");
            }
            else
            {
              byte[] buffer = archive[attribute4.Value];
              calculatedHash = buffer != null && buffer.Length != 0 ? new SHA1CryptoServiceProvider().ComputeHash(buffer) : throw new Exception("The package validation has failed.");
            }
            if (!PXCriptoHelper.ValidateHash(calculatedHash, Convert.FromBase64String(attribute5.Value)))
              throw new Exception("The package validation has failed.");
            GC.Collect();
            pxPackageInfo.Items.Add(new UpdateMaint.PXPackageItemInfo()
            {
              Name = attribute4.Value,
              Path = attribute4.Value,
              Key = id
            });
          }
          XmlNode xmlNode3 = xmlDocument.SelectSingleNode("//pachageManifest/parameters");
          if (xmlNode3 != null)
          {
            foreach (XmlElement childNode in xmlNode3.ChildNodes)
            {
              XmlAttribute attribute7 = childNode.Attributes["name"];
              XmlAttribute attribute8 = childNode.Attributes["value"];
              if (attribute7 == null || string.IsNullOrEmpty(attribute7.Value) || attribute8 == null)
                throw new Exception("The package validation has failed.");
              pxPackageInfo.Parameters.Add(new UpdateMaint.PXPackageParameterInfo()
              {
                Name = attribute7.Value,
                Value = attribute8.Value
              });
            }
          }
          XmlNode xmlNode4 = xmlDocument.SelectSingleNode("//pachageManifest/localizations");
          if (xmlNode4 != null)
          {
            IEnumerable<MarketAdaptationManager> marketAdaptation = MarketAdaptationManager.GetMarketAdaptation(UpdateMaint._point);
            List<string> source = new List<string>();
            foreach (XmlNode childNode in xmlNode4.ChildNodes)
            {
              XmlAttribute attribute9 = childNode.Attributes["name"];
              source.Add(attribute9.Value);
            }
            foreach (MarketAdaptationManager adaptationManager in marketAdaptation)
            {
              MarketAdaptationManager locale = adaptationManager;
              if (!source.Any<string>((Func<string, bool>) (x => x.StartsWith(locale.Region.Name, StringComparison.OrdinalIgnoreCase))))
                throw new Exception($"The package validation has failed. The package does not contain the {locale.Region.Name} localization required to update the site.");
            }
          }
          UpdateMaint.PXPackageItemInfo pxPackageItemInfo = pxPackageInfo.Items.FirstOrDefault<UpdateMaint.PXPackageItemInfo>((Func<UpdateMaint.PXPackageItemInfo, bool>) (i => i.Name == "PX.Launcher.dll"));
          if (pxPackageItemInfo == null)
            throw new Exception("The package validation has failed.");
          byte[] array;
          using (MemoryStream destination = new MemoryStream())
          {
            archive.GetStream(pxPackageItemInfo.Path).CopyTo((Stream) destination);
            array = destination.ToArray();
          }
          pxPackageInfo.Launcher = array;
          pxPackageInfo.PackageKey = key;
          pxPackageInfo.Date = System.DateTime.Now;
        }
      }
    }
    return UpdateMaint.packages[pxPackageInfo.Version] = pxPackageInfo;
  }

  private static UpdateMaint.PXPackageInfo ValidatePackage(string key, AvailableVersion version)
  {
    UpdateMaint.PXPackageInfo pxPackageInfo = UpdateMaint.ValidatePackage(key);
    if (pxPackageInfo != null && version != null)
    {
      pxPackageInfo.Date = version.Date.GetValueOrDefault();
      pxPackageInfo.Restricted = version.Restricted.GetValueOrDefault();
      pxPackageInfo.Description = version.Description;
      pxPackageInfo.Notes = version.Notes;
    }
    return pxPackageInfo;
  }

  private static string ExtractItem(ZipArchiveWrapper archive, string name)
  {
    string id = Guid.NewGuid().ToString() + ".aup";
    using (Stream stream1 = archive.GetStream(name))
    {
      using (Stream stream2 = PXStorageHelper.GetAppDataProvider().OpenWrite(id))
      {
        int count1 = 1048576 /*0x100000*/;
        byte[] buffer = new byte[count1];
        int count2;
        do
        {
          count2 = stream1.Read(buffer, 0, count1);
          if (count2 > 0)
            stream2.Write(buffer, 0, count2);
        }
        while (count2 >= count1);
      }
    }
    return id;
  }

  protected IEnumerable<AvailableVersion> GetVersions(string key)
  {
    try
    {
      return ((int) this.EnshureFilter().ServerAvailable ?? 1) == 0 ? (IEnumerable<AvailableVersion>) new List<AvailableVersion>() : PXUpdateServer.GetVersions(this.CurrentVersion, key);
    }
    catch
    {
      this.VersionFilterRecord.Cache.RaiseExceptionHandling<VersionFilter.key>((object) this.VersionFilterRecord.Current, (object) null, (Exception) new PXSetPropertyException("The connection to Acumatica Update Server failed."));
      this.EnshureFilter().ServerAvailable = new bool?(false);
      return (IEnumerable<AvailableVersion>) new List<AvailableVersion>();
    }
  }

  public static bool CheckForUpdates()
  {
    return PXUpdateServer.CheckUpdates(PXGraph.CreateInstance<UpdateMaint>().CurrentVersion);
  }

  protected class PXPackageInfo
  {
    public string PackageKey;
    public PXInstanceType Type;
    public string Version;
    public System.DateTime Date;
    public bool Restricted;
    public string Description;
    public string Notes;
    public byte[] Manifest;
    public byte[] Launcher;
    public List<UpdateMaint.PXPackageItemInfo> Items = new List<UpdateMaint.PXPackageItemInfo>();
    public List<UpdateMaint.PXPackageParameterInfo> Parameters = new List<UpdateMaint.PXPackageParameterInfo>();
  }

  protected class PXPackageItemInfo
  {
    public string Name;
    public string Path;
    public string Key;
  }

  protected class PXPackageParameterInfo
  {
    public string Name;
    public string Value;
  }

  private class ValidationFailed : IPXCustomInfo
  {
    public string Text;

    public void Complete(PXLongRunStatus status, PXGraph graph)
    {
      UpdateMaint updateMaint = (UpdateMaint) graph;
      updateMaint.ApplyVersionCommand.SetEnabled(false);
      int num = (int) updateMaint.VersionRecord.Ask("Validation error: The update is not compatible with the published customization.", "The selected update is not compatible with the customization published on the website.\n\n" + this.Text, MessageButtons.OK);
    }
  }
}
