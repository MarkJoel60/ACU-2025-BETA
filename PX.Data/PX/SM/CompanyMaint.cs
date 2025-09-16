// Decompiled with JetBrains decompiler
// Type: PX.SM.CompanyMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Async;
using PX.BulkInsert;
using PX.Caching;
using PX.CloudServices.Tenants;
using PX.Common;
using PX.Common.Context;
using PX.Data;
using PX.Data.Api.Export;
using PX.Data.DependencyInjection;
using PX.Data.Localizers;
using PX.Data.Maintenance;
using PX.Data.Maintenance.TenantShapshotDeletion;
using PX.Data.Maintenance.TenantShapshotDeletion.DAC;
using PX.Data.PushNotifications;
using PX.Data.Services;
using PX.Data.Update;
using PX.DbServices.Model;
using PX.DbServices.Model.Entities;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.Points.FileSystem;
using PX.DbServices.QueryObjectModel;
using PX.Licensing;
using PX.Logging.Sinks.SystemEventsDbSink;
using PX.Metadata;
using PX.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;

#nullable disable
namespace PX.SM;

[NonOptimizable(IgnoreOptimizationBehavior = true)]
public class CompanyMaint : PXGraph<CompanyMaint>, IGraphWithInitialization
{
  [PXVirtualDAC]
  public PXSelect<UPCompany> Companies;
  [PXVirtualDAC]
  public PXSelect<UPCompany, Where<UPCompany.companyID, Equal<Current<UPCompany.companyID>>>> CompanyCurrent;
  public PXSelect<UPSnapshot, Where2<Where<UPSnapshot.sourceCompany, Equal<Current<UPCompany.companyID>>, Or<UPSnapshot.sourceCompany, IsNull>>, And<Where<UPSnapshot.isUnderDeletion, IsNull, Or<UPSnapshot.isUnderDeletion, Equal<False>>>>>, OrderBy<Desc<UPSnapshot.date>>> Snapshots;
  public PXSelectJoin<UPSnapshotHistory, InnerJoin<UPSnapshot, On<UPSnapshotHistory.snapshotID, Equal<UPSnapshot.snapshotID>>>, Where<UPSnapshotHistory.targetCompany, Equal<Current<UPCompany.companyID>>>, OrderBy<Desc<UPSnapshotHistory.historyID>>> SnapshotsHistory;
  public PXSelect<PX.SM.Users> Users;
  public PXAction<UPCompany> SaveCompanyCommand;
  public PXCancel<UPCompany> Cancel;
  public PXAction<UPCompany> InsertCompanyCommand;
  public PXAction<UPCompany> DeleteSnapshotCommand;
  public PXPrevious<UPCompany> Previous;
  public PXNext<UPCompany> Next;
  public PXAction<UPCompany> CopyCompanyCommand;
  [PXVirtualDAC]
  public PXFilter<CopyCompanySettings> CopyCompanyPanel;
  public PXAction<UPCompany> ExportSnapshotCommand;
  [PXVirtualDAC]
  public PXFilter<ExportSnapshotSettings> ExportSnapshotPanel;
  public PXAction<UPCompany> ReloadSnapshotCommand;
  [PXVirtualDAC]
  public PXFilter<ImportSnapshotSettings> ReloadSnapshotPanel;
  public PXAction<UPCompany> PrepareXmlSnapshotCommand;
  public PXAction<UPCompany> PrepareAdbSnapshotCommand;
  public PXAction<UPCompany> ImportSnapshotCommand;
  [PXVirtualDAC]
  public PXFilter<ImportSnapshotSettings> ImportSnapshotPanel;
  public PXAction<UPCompany> DeleteCompanyCommand;
  public PXAction<UPCompany> DownloadSnapshotCommand;
  public PXAction<UPCompany> UploadSnapshotCommand;
  public PXSelect<UPCompany> UploadDialogPanel;
  public PXAction<UPCompany> ManageUsersCommand;
  public PXAction<UPCompany> ChangeVisibilityCommand;
  public PXAction<UPCompany> TrialCompanyCommand;
  public PXAction<UPCompany> DeleteOrphanedRows;
  public PXAction<UPCompany> DismissedUnsafeSnapshotCommand;
  public PXAction<UPCompany> SpaceUsageCommand;
  private bool persisting;
  private static Regex _tenantNameValidationRegex = new Regex("[,;<>*&%:\\\\?]");
  private static readonly IReadOnlyCollection<System.Type> _prefetchedTablesWithCompanyMask = (IReadOnlyCollection<System.Type>) new List<System.Type>(EnumerableExtensions.Except<System.Type>((IEnumerable<System.Type>) PXGenericInqGrph.Definition.UsedTables, typeof (PXGraph.FeaturesSet))).AddRange<System.Type>((IEnumerable<System.Type>) PXDimensionAttribute.UsedTables).Add<System.Type>(typeof (SiteMap));

  [InjectDependency]
  private ICloudTenantManagementService _cloudTenantManagementService { get; set; }

  [InjectDependency]
  private ISnapshotService _snapshotService { get; set; }

  [InjectDependency]
  private ICacheControl<PageCache> _pageCacheControl { get; set; }

  [InjectDependency]
  private IScreenInfoCacheControl _screenInfoCacheControl { get; set; }

  [InjectDependency]
  private ILicensingManager _licensingManager { get; set; }

  [InjectDependency]
  private IPXLogin _pxLogin { get; set; }

  [InjectDependency]
  private ISessionContextFactory _sessionContextFactory { get; set; }

  [InjectDependency]
  private IRoleManagementService _roleManagementService { get; set; }

  [InjectDependency]
  private ILegacyCompanyService _legacyCompanyService { get; set; }

  [InjectDependency]
  private ILongOperationTaskManager _longOperationTaskManager { get; set; }

  protected IEnumerable companies()
  {
    if (PXView.NeedDefaultPrimaryViewObject)
    {
      UPCompany row = this.Companies.Cache.Cached.Cast<UPCompany>().Where<UPCompany>((System.Func<UPCompany, bool>) (c =>
      {
        int? companyId = c.CompanyID;
        int currentCompany = PXInstanceHelper.CurrentCompany;
        return companyId.GetValueOrDefault() == currentCompany & companyId.HasValue;
      })).FirstOrDefault<UPCompany>() ?? PXCompanyHelper.SelectCompanies().Where<UPCompany>((System.Func<UPCompany, bool>) (c =>
      {
        int? companyId = c.CompanyID;
        int currentCompany = PXInstanceHelper.CurrentCompany;
        return companyId.GetValueOrDefault() == currentCompany & companyId.HasValue;
      })).FirstOrDefault<UPCompany>();
      PXView.StartRow = 0;
      this.Companies.Cache.Hold((object) row);
      yield return (object) row;
    }
    else
    {
      List<int> keys = new List<int>();
      foreach (UPCompany upCompany in this.Companies.Cache.Cached)
      {
        switch (this.Companies.Cache.GetStatus((object) upCompany))
        {
          case PXEntryStatus.Updated:
          case PXEntryStatus.Inserted:
          case PXEntryStatus.Held:
            keys.Add(upCompany.CompanyID.GetValueOrDefault());
            yield return (object) upCompany;
            continue;
          default:
            continue;
        }
      }
      foreach (UPCompany includeWithoutUser in PXCompanyHelper.SelectVisibleCompaniesIncludeWithoutUsers())
      {
        if (!keys.Contains(includeWithoutUser.CompanyID.Value))
        {
          this.Companies.Cache.Hold((object) includeWithoutUser);
          yield return (object) includeWithoutUser;
        }
      }
    }
  }

  protected IEnumerable companyCurrent()
  {
    UPCompany current = this.Companies.Current;
    if (current != null)
      yield return (object) current;
  }

  protected IEnumerable snapshots()
  {
    UPCompany current = this.Companies.Current;
    PXEntryStatus status = this.Companies.Cache.GetStatus((object) current);
    if (current == null || status != PXEntryStatus.Held && status != PXEntryStatus.Notchanged && status != PXEntryStatus.Updated)
      return (IEnumerable) new List<UPCompany>();
    PXView pxView = new PXView((PXGraph) this, true, this.Snapshots.View.BqlSelect);
    pxView.Clear();
    return (IEnumerable) pxView.SelectMulti();
  }

  protected IEnumerable snapshotsHistory()
  {
    using (new PXReadDeletedScope())
    {
      PXView pxView = new PXView((PXGraph) this, true, this.SnapshotsHistory.View.BqlSelect);
      pxView.Clear();
      return (IEnumerable) pxView.SelectMulti();
    }
  }

  protected IEnumerable users()
  {
    return (IEnumerable) this.WithAnotherCompany<PX.SM.Users>((PXSelectBase<PX.SM.Users>) this.Users);
  }

  protected PXLoginScope GetLoginScope()
  {
    return CompanyMaint.GetLoginScope((this.Companies.Current ?? throw new PXException("A tenant is not selected.")).LoginName);
  }

  protected static PXLoginScope GetLoginScope(string companyLoginName)
  {
    return new PXLoginScope($"{PXAccess.GetUserName()}@{companyLoginName}", PXAccess.GetAdministratorRoles());
  }

  protected IEnumerable<T> WithAnotherCompany<T>(PXSelectBase<T> select) where T : class, IBqlTable, new()
  {
    UPCompany current = this.Companies.Current;
    PXLoginScope pxLoginScope = (PXLoginScope) null;
    int? companyId = current.CompanyID;
    int currentCompany = PXInstanceHelper.CurrentCompany;
    if (!(companyId.GetValueOrDefault() == currentCompany & companyId.HasValue))
    {
      PXDatabase.ResetCredentials();
      pxLoginScope = this.GetLoginScope();
    }
    List<T> objList = new List<T>();
    try
    {
      if (this.Companies.Cache.GetStatus((object) current) == PXEntryStatus.Inserted)
        return (IEnumerable<T>) objList;
      foreach (T obj in new PXView((PXGraph) this, true, select.View.BqlSelect).SelectMulti())
        objList.Add(obj);
    }
    catch
    {
    }
    finally
    {
      if (pxLoginScope != null)
      {
        ((IDisposable) pxLoginScope).Dispose();
        PXDatabase.ResetCredentials();
      }
    }
    return (IEnumerable<T>) objList;
  }

  protected UPCompany GetCurrentCompany()
  {
    return this.companies().Cast<UPCompany>().FirstOrDefault<UPCompany>((System.Func<UPCompany, bool>) (c =>
    {
      int? companyId = c.CompanyID;
      int currentCompany = PXInstanceHelper.CurrentCompany;
      return companyId.GetValueOrDefault() == currentCompany & companyId.HasValue;
    }));
  }

  public CompanyMaint()
  {
    this.Users.Cache.AllowDelete = false;
    this.Users.Cache.AllowInsert = false;
    this.Users.Cache.AllowUpdate = false;
    this.Snapshots.Cache.AllowUpdate = false;
    this.Snapshots.Cache.AllowDelete = false;
    this.SnapshotsHistory.Cache.AllowDelete = false;
    this.SnapshotsHistory.Cache.AllowInsert = false;
    this.SnapshotsHistory.Cache.AllowUpdate = false;
  }

  void IGraphWithInitialization.Initialize()
  {
    if (!PXDatabase.Provider.DatabaseDefinedCompanies || PXDatabase.Provider.GetLoginCompanies().Any<KeyValuePair<int, string>>())
    {
      this.InsertCompanyCommand.SetEnabled(false);
      this.InsertCompanyCommand.SetTooltip("The web configuration does not allow you to create tenants.");
    }
    if (PXDatabase.AvailableCompanies.Length >= this._licensingManager.License.CompaniesAllowed)
    {
      this.InsertCompanyCommand.SetEnabled(false);
      this.InsertCompanyCommand.SetTooltip("The list of tenants will be restricted by the license.");
    }
    UPCompany current = this.Companies.Current;
    this.Companies.Select();
    if (current == null)
      return;
    this.Companies.Current = (UPCompany) this.Companies.Cache.Locate((object) current) ?? this.Companies.Current;
  }

  private bool IsNewUI()
  {
    return "T".Equals(PXSiteMap.Provider.FindSiteMapNode(this.GetType())?.SelectedUI, StringComparison.OrdinalIgnoreCase);
  }

  [PXUIField(DisplayName = "Save", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXSaveButton(SpecialType = PXSpecialButtonType.SaveNotClose)]
  protected IEnumerable saveCompanyCommand(PXAdapter adapter)
  {
    CompanyMaint companyMaint = this;
    int currentCompanyId = PXInstanceHelper.CurrentCompany;
    if (!companyMaint.Companies.Cache.AllowUpdate)
      throw new PXException("The record cannot be saved.");
    foreach (object obj in adapter.Get())
      yield return obj;
    List<UPCompany> inserted = companyMaint.Companies.Cache.Inserted.Cast<UPCompany>().ToList<UPCompany>();
    List<UPCompany> list = companyMaint.Companies.Cache.Updated.Cast<UPCompany>().ToList<UPCompany>();
    companyMaint.Persist();
    companyMaint.SelectTimeStamp();
    if (list.Any<UPCompany>())
    {
      CompanyMaint.PersistUpdated((IEnumerable<UPCompany>) list, companyMaint._roleManagementService, companyMaint._legacyCompanyService);
      UPCompany upCompany = list.FirstOrDefault<UPCompany>((System.Func<UPCompany, bool>) (u => u.Altered.GetValueOrDefault()));
      if (upCompany != null)
      {
        int? companyId = upCompany.CompanyID;
        int num = currentCompanyId;
        if (companyId.GetValueOrDefault() == num & companyId.HasValue)
          companyMaint.SetDefaultLoginCompanyCookie(upCompany.LoginName);
        Redirector.Refresh(HttpContext.Current);
      }
    }
    if (inserted.Any<UPCompany>())
    {
      PXLongOperation.StartOperation(companyMaint.UID, (PXToggleAsyncDelegate) (() => CompanyMaint.PersistInserted(this.GetCurrentCompany(), inserted)));
      companyMaint.SetDefaultLoginCompanyCookie(inserted.Last<UPCompany>().LoginName);
    }
  }

  private void SetDefaultLoginCompanyCookie(string loginName)
  {
    if (string.IsNullOrEmpty(loginName) || HttpContext.Current == null)
      return;
    HttpContext.Current.Response.Cookies["CompanyID"].Value = loginName;
    HttpContext.Current.Response.Cookies["CompanyID"].Expires = System.DateTime.Now.AddDays(3.0);
  }

  [PXUIField(DisplayName = "Insert", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  [PXInsertButton]
  protected IEnumerable insertCompanyCommand(PXAdapter adapter)
  {
    return new PXInsert<UPCompany>((PXGraph) this, nameof (insertCompanyCommand)).Press(adapter);
  }

  [PXButton]
  [PXUIField(DisplayName = "Delete Snapshot", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable deleteSnapshotCommand(PXAdapter adapter)
  {
    TenantSnapshotDeletionProcess instance = PXGraph.CreateInstance<TenantSnapshotDeletionProcess>();
    instance.Filter.Cache.SetValueExt<DeletionAction.name>((object) instance.Filter.Current, (object) "S");
    throw new PXRedirectRequiredException((PXGraph) instance, (string) null);
  }

  [PXButton(Tooltip = "Copy the current tenant into another tenant.", Category = "Actions", PopupVisible = true)]
  [PXUIField(DisplayName = "Copy Tenant", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  public IEnumerable copyCompanyCommand(PXAdapter adapter)
  {
    if (PXSiteLockout.GetStatus(true) != PXSiteLockout.Status.Locked)
    {
      int num;
      if (!this.IsNewUI())
        num = (int) this.CompanyCurrent.View.Ask((object) null, PXMessages.LocalizeNoPrefix("Copy Tenant"), PXMessages.LocalizeNoPrefix("The system is not in maintenance mode. Copying tenants can lead to data corruption. Before copying the tenant, activate maintenance mode for all sites on the Apply Updates (SM203510) form. Do you want to open the form?"), MessageButtons.OKCancel, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
        {
          {
            WebDialogResult.OK,
            PXMessages.LocalizeNoPrefix("Open")
          },
          {
            WebDialogResult.Cancel,
            PXMessages.LocalizeNoPrefix("Cancel")
          }
        }, MessageIcon.Warning);
      else
        num = (int) this.CompanyCurrent.AskExt();
      if (num == 1)
        throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<UpdateMaint>(), true, string.Empty);
      return adapter.Get();
    }
    switch (this.CopyCompanyPanel.AskExt(true))
    {
      case WebDialogResult.OK:
        CopyCompanySettings options = this.CopyCompanyPanel.Current;
        int? nullable1 = options != null ? options.CompanyID : throw new PXException("The settings for copying the tenant are not specified.");
        if (!nullable1.HasValue)
          throw new PXException("The target tenant is not selected.");
        UPCompany sourceCompany = this.Companies.Current;
        PointDbmsBase point = PXDatabase.Provider.CreateDbServicesPoint();
        point.SchemaReader.ClearCache();
        point.SchemaReader.OmitTriggersAndFks = false;
        List<CompanyHeader> companies = point.getCompanies(true);
        CompanyHeader targetCompany = companies.FirstOrDefault<CompanyHeader>((System.Func<CompanyHeader, bool>) (c =>
        {
          int id = c.Id;
          int? companyId = options.CompanyID;
          int valueOrDefault = companyId.GetValueOrDefault();
          return id == valueOrDefault & companyId.HasValue;
        }));
        string str;
        if (targetCompany != null)
        {
          if (!(targetCompany.Type == "System") && targetCompany.Id != 1)
          {
            int id1 = targetCompany.Id;
            nullable1 = sourceCompany.CompanyID;
            int valueOrDefault1 = nullable1.GetValueOrDefault();
            str = id1 == valueOrDefault1 & nullable1.HasValue ? "Please select two different companies." : (companies.Any<CompanyHeader>((System.Func<CompanyHeader, bool>) (c =>
            {
              int id2 = targetCompany.Id;
              int? parentId = c.ParentId;
              int valueOrDefault2 = parentId.GetValueOrDefault();
              return id2 == valueOrDefault2 & parentId.HasValue;
            })) ? "A tenant that is used as a parent for another tenant can’t be deleted." : (string) null);
          }
          else
            str = "The system tenant can’t be modified.";
        }
        else
          str = "The target tenant is not found.";
        string message = str;
        if (message != null)
          throw new PXException(message);
        if (!this.IsContractBasedAPI)
        {
          if (this.Companies.Ask("Warning", PXMessages.LocalizeFormatNoPrefix("Please confirm that data should be copied from ~'{0}'~to~'{1}'.", (object) sourceCompany.Description, (object) targetCompany.GetDescription()), MessageButtons.OKCancel, MessageIcon.Warning, true) != WebDialogResult.OK)
            return adapter.Get();
        }
        PXLongOperation.StartOperation(this.UID, (PXToggleAsyncDelegate) (() =>
        {
          SpaceUsageCalculationHistory calculationHistory = (SpaceUsageCalculationHistory) null;
          if (SpaceUsageMaint.CalculateSpaceUsage())
            calculationHistory = SpaceUsageMaint.GetCalculatedSpaceUsage();
          long? size1 = sourceCompany.Size;
          long? size2 = PXCompanyHelper.FindCompany(PXCompanySelectOptions.All, targetCompany.Id).Size;
          if (calculationHistory != null)
          {
            long? freeSpace = calculationHistory.FreeSpace;
            long? nullable2 = size1;
            long? nullable3 = size2;
            long? nullable4 = nullable2.HasValue & nullable3.HasValue ? new long?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new long?();
            if (freeSpace.GetValueOrDefault() < nullable4.GetValueOrDefault() & freeSpace.HasValue & nullable4.HasValue)
              throw new PXException("The system cannot copy the tenant because the database size will exceed your limit. You can view used database size and limit on the Space Usage (SM203525) form.");
          }
          try
          {
            PXDatabase.Provider.GetMaintenance(point).CopyCompanyOnDatabaseServer(sourceCompany.CompanyID.Value, targetCompany, true, false);
            this._cloudTenantManagementService.OnCopyCompany(point, targetCompany.Id);
            PXDatabase.ClearCompanyCache();
            using (CompanyMaint.GetLoginScope(targetCompany.Key))
            {
              CompanyMaint.DisableSchedulers();
              PXDBLocalizableStringAttribute.EnsureTranslations((System.Func<string, bool>) (tableName => true));
            }
            PXTrace.Logger.ForSystemEvents("System", "System_CompanyCopyCreatedEventId").ForCurrentCompanyContext().Information<string, string>("A tenant copy has been created TargetTenant:{TargetTenant}, SourceTenant:{SourceTenant}", targetCompany?.Key, sourceCompany?.LoginName);
          }
          catch (Exception ex)
          {
            PXTrace.Logger.ForSystemEvents("System", "System_CompanyCopyCreationFailedEventId").Error<string, string>("Creating of a tenant copy has failed TargetTenant:{TargetTenant}, SourceTenant:{SourceTenant}", targetCompany?.Key, sourceCompany?.LoginName);
            throw;
          }
        }));
        return adapter.Get();
      case WebDialogResult.Yes:
        if (this.IsContractBasedAPI)
          goto case WebDialogResult.OK;
        break;
    }
    return adapter.Get();
  }

  protected IEnumerable copyCompanyPanel()
  {
    if (this.CopyCompanyPanel.Current == null)
    {
      CopyCompanySettings copyCompanySettings = new CopyCompanySettings();
      UPCompany current = this.GetCurrentCompany();
      copyCompanySettings.CompanyID = PXCompanyHelper.SelectCompanies().FirstOrDefault<UPCompany>((System.Func<UPCompany, bool>) (c =>
      {
        int? companyId1 = c.CompanyID;
        int? companyId2 = current.CompanyID;
        return !(companyId1.GetValueOrDefault() == companyId2.GetValueOrDefault() & companyId1.HasValue == companyId2.HasValue);
      })).CompanyID;
      yield return (object) this.CopyCompanyPanel.Insert(copyCompanySettings);
    }
    else
      yield return (object) this.CopyCompanyPanel.Current;
  }

  [PXButton(Tooltip = "Create a snapshot of the current tenant.", Category = "Actions", PopupVisible = true, DisplayOnMainToolbar = true, IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "Create Snapshot", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable exportSnapshotCommand(PXAdapter adapter)
  {
    UPCompany company = this.Companies.Current;
    bool flag = PXSiteLockout.GetStatus(true) == PXSiteLockout.Status.Locked;
    if (!flag)
    {
      int num;
      if (!this.IsNewUI())
        num = (int) this.Snapshots.View.Ask((object) null, PXMessages.LocalizeNoPrefix("Create Snapshot"), PXMessages.LocalizeNoPrefix("The system is not in maintenance mode. Creating snapshots can lead to data corruption. Before creating the snapshot, activate maintenance mode for all sites on the Apply Updates (SM203510) form. Do you want to open the form?"), MessageButtons.OKCancel, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
        {
          {
            WebDialogResult.OK,
            PXMessages.LocalizeNoPrefix("Open")
          },
          {
            WebDialogResult.Cancel,
            PXMessages.LocalizeNoPrefix("Cancel")
          }
        }, MessageIcon.Warning);
      else
        num = (int) this.Snapshots.AskExt();
      if (num == 1)
        throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<UpdateMaint>(), true, string.Empty);
      return adapter.Get();
    }
    UPSnapshotHistory restoredSnapshot = CompanyMaint.GetLastRestoredSnapshot();
    int num1;
    if (restoredSnapshot != null)
    {
      bool? isSafe = restoredSnapshot.IsSafe;
      if (isSafe.HasValue)
      {
        isSafe = restoredSnapshot.IsSafe;
        num1 = !isSafe.Value ? 1 : 0;
        goto label_11;
      }
    }
    num1 = 0;
label_11:
    int num2 = flag ? 1 : 0;
    if ((num1 & num2) != 0 && this.Companies.Ask("Warning", PXMessages.LocalizeFormatNoPrefix("The snapshot may contain corrupted data because the last restored snapshot was unsafe."), MessageButtons.OKCancel, MessageIcon.Warning, true) != WebDialogResult.OK || this.ExportSnapshotPanel.AskExt(true) != WebDialogResult.OK)
      return adapter.Get();
    ExportSnapshotSettings options = this.ExportSnapshotPanel.Current;
    if (options == null || company == null)
      throw new PXException("The settings of snapshot creation are not specified.");
    PXLongOperation.StartOperation(this.UID, (PXToggleAsyncDelegate) (() => new PXSnapshotCreator(company, options).Start()));
    return adapter.Get();
  }

  protected IEnumerable exportSnapshotPanel()
  {
    if (this.ExportSnapshotPanel.Current != null)
    {
      this.ExportSnapshotPanel.Current.Name = this.SelectedCompanyName;
      this.ExportSnapshotPanel.Current.Company = this.Companies.Current != null ? this.Companies.Current.Description : PXInstanceHelper.CurrentCompany.ToString();
      yield return (object) this.ExportSnapshotPanel.Current;
    }
    else
      yield return (object) this.ExportSnapshotPanel.Insert(new ExportSnapshotSettings());
  }

  [PXButton(ImageKey = "Refresh", Tooltip = "Reset the data of the current tenant.")]
  [PXUIField(Visible = false, DisplayName = "Reset Data", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable reloadSnapshotCommand(PXAdapter adapter)
  {
    int num = (int) this.ReloadSnapshotPanel.AskExt();
    return adapter.Get();
  }

  protected IEnumerable reloadSnapshotPanel()
  {
    if (this.ReloadSnapshotPanel.Current != null)
      yield return (object) this.ReloadSnapshotPanel.Current;
    else
      yield return (object) this.ReloadSnapshotPanel.Insert(new ImportSnapshotSettings());
  }

  [PXButton(ImageKey = "Process", Tooltip = "Prepare the selected snapshot for export in XML Format.")]
  [PXUIField(DisplayName = "Prepare in XML Format", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable prepareXmlSnapshotCommand(PXAdapter adapter)
  {
    this.prepareSnapshotCommon((FileFormat) 2);
    return adapter.Get();
  }

  [PXButton(ImageKey = "Process", Tooltip = "Prepare the selected snapshot for export in Binary Format.")]
  [PXUIField(DisplayName = "Prepare in Binary Format", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable prepareAdbSnapshotCommand(PXAdapter adapter)
  {
    this.prepareSnapshotCommon((FileFormat) 1);
    return adapter.Get();
  }

  protected void prepareSnapshotCommon(FileFormat format)
  {
    UPCompany company = this.Companies.Current;
    UPSnapshot snapshot = this.Snapshots.Current;
    if (company == null || snapshot == null)
      throw new PXException("A snapshot is not selected.");
    if (!snapshot.LinkedCompany.HasValue || !PXCompanyHelper.SelectCompanies(PXCompanySelectOptions.Negative).Any<UPCompany>((System.Func<UPCompany, bool>) (c =>
    {
      int? companyId = c.CompanyID;
      int num = snapshot.LinkedCompany.Value;
      return companyId.GetValueOrDefault() == num & companyId.HasValue;
    })))
      return;
    PXLongOperation.StartOperation(this.UID, (PXToggleAsyncDelegate) (() => new PXSnapshotCreator(company, snapshot).Start(format)));
  }

  [PXButton(Tooltip = "Restore the selected snapshot to the current tenant.", Category = "Actions", PopupVisible = true, DisplayOnMainToolbar = true, IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "Restore Snapshot", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable importSnapshotCommand(PXAdapter adapter)
  {
    UPCompany company = this.Companies.Current;
    UPSnapshot snapshot = this.Snapshots.Current;
    if (snapshot == null || !snapshot.SnapshotID.HasValue || company == null)
      throw new PXException("A snapshot is not selected.");
    if (PXSiteLockout.GetStatus(true) != PXSiteLockout.Status.Locked)
    {
      int num;
      if (!this.IsNewUI())
        num = (int) this.SnapshotsHistory.View.Ask((object) null, PXMessages.LocalizeNoPrefix("Restore Snapshot"), PXMessages.LocalizeNoPrefix("The system is not in maintenance mode. Restoring the snapshot can lead to data corruption. Before restoring the snapshot, activate maintenance mode for all sites on the Apply Updates (SM203510) form. Do you want to open the form?"), MessageButtons.OKCancel, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
        {
          {
            WebDialogResult.OK,
            PXMessages.LocalizeNoPrefix("Open")
          },
          {
            WebDialogResult.Cancel,
            PXMessages.LocalizeNoPrefix("Cancel")
          }
        }, MessageIcon.Warning);
      else
        num = (int) this.SnapshotsHistory.AskExt();
      if (num == 1)
        throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<UpdateMaint>(), true, string.Empty);
      return adapter.Get();
    }
    if (!snapshot.IsSafe.Value && this.Companies.Ask("Warning", PXMessages.LocalizeFormatNoPrefix("This snapshot was not taken in maintenance mode and could contain corrupted data. We do not recommend that you restore the snapshot for production use. Are you sure you want to restore this snapshot?"), MessageButtons.YesNo, MessageIcon.Warning, true) != WebDialogResult.Yes)
      return adapter.Get();
    CompanyInfo companyInfo = ((IEnumerable<CompanyInfo>) PXDatabase.Provider.SelectCompanies(false)).FirstOrDefault<CompanyInfo>((System.Func<CompanyInfo, bool>) (ci =>
    {
      int companyId = ci.CompanyID;
      int? parentId = company.ParentID;
      int valueOrDefault = parentId.GetValueOrDefault();
      return companyId == valueOrDefault & parentId.HasValue;
    }));
    if (!string.IsNullOrWhiteSpace(snapshot.MasterCompany) && companyInfo != null && snapshot.MasterCompany.Trim() != PXCompanyHelper.FindCompany(PXCompanySelectOptions.All, company.ParentID.Value).Description.Trim())
    {
      if (this.CompanyCurrent.Ask("Warning", PXMessages.LocalizeFormatNoPrefix("WARNING: The snapshot you are about to restore is based on a different parent tenant, '{0}'. Restoring this snapshot might render your instance inoperable. Are you sure you want to continue?", (object) snapshot.MasterCompany), MessageButtons.OKCancel, MessageIcon.Warning, true) != WebDialogResult.OK)
        return adapter.Get();
    }
    ImportSnapshotSettings settings = this.importSnapshotPanel().Cast<ImportSnapshotSettings>().First<ImportSnapshotSettings>();
    if (this.ImportSnapshotPanel.View.Answer != WebDialogResult.OK)
    {
      settings.Name = snapshot.Name;
      settings.Company = company.Description;
      settings.Description = snapshot.Description;
      settings.Customization = new bool?(!string.IsNullOrEmpty(snapshot.Customization));
    }
    if (this.ImportSnapshotPanel.AskExt(true) != WebDialogResult.OK)
      return adapter.Get();
    settings = this.ImportSnapshotPanel.Current;
    if (settings == null)
      throw new PXException("The settings of snapshot restoration are not specified.");
    ISnapshotService snapshotService = this._snapshotService;
    ICacheControl<PageCache> pageCacheControl = this._pageCacheControl;
    IScreenInfoCacheControl screenInfoCacheControl = this._screenInfoCacheControl;
    ILicensingManager licensingManager = this._licensingManager;
    IPXLogin pxLogin = this._pxLogin;
    ISessionContextFactory sessionContextFactory = this._sessionContextFactory;
    ILegacyCompanyService legacyCompanyService = this._legacyCompanyService;
    PXLongOperation.StartOperation(this.UID, (PXToggleAsyncDelegate) (() => CompanyMaint.PersistSnapshot(company, settings, snapshot, snapshotService, pageCacheControl, screenInfoCacheControl, licensingManager, pxLogin, sessionContextFactory, legacyCompanyService)));
    return adapter.Get();
  }

  protected IEnumerable importSnapshotPanel()
  {
    if (this.ImportSnapshotPanel.Current != null)
      yield return (object) this.ImportSnapshotPanel.Current;
    else
      yield return (object) this.ImportSnapshotPanel.Insert(new ImportSnapshotSettings());
  }

  [PXUIField(DisplayName = "Delete Tenant", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  [PXDeleteButton(ConfirmationMessage = null, ImageKey = null)]
  protected IEnumerable deleteCompanyCommand(PXAdapter adapter)
  {
    TenantSnapshotDeletionProcess instance = PXGraph.CreateInstance<TenantSnapshotDeletionProcess>();
    instance.Filter.Cache.SetValueExt<DeletionAction.name>((object) instance.Filter.Current, (object) "T");
    throw new PXRedirectRequiredException((PXGraph) instance, (string) null);
  }

  [PXButton(ImageKey = "GetFile", Tooltip = "Export the selected snapshot.")]
  [PXUIField(DisplayName = "Export Snapshot", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable downloadSnapshotCommand(PXAdapter adapter)
  {
    UPSnapshot current = this.Snapshots.Current;
    if (current != null)
    {
      Guid? nullable = current.SnapshotID;
      if (nullable.HasValue)
      {
        nullable = current.Prepared.GetValueOrDefault() ? current.SnapshotID : throw new PXException("The snapshot is not prepared for downloading.");
        SnapshotDownloader.Redirect(nullable.Value, (current.Name ?? current.Description ?? "Snapshot").Replace(" ", ""));
        return adapter.Get();
      }
    }
    throw new PXException("A snapshot is not selected.");
  }

  [PXButton(ImageKey = "CheckOut", Tooltip = "Import a snapshot.")]
  [PXUIField(DisplayName = "Import Snapshot", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable uploadSnapshotCommand(PXAdapter adapter)
  {
    if (this.UploadDialogPanel.AskExt() == WebDialogResult.OK)
    {
      FileInfo fileInfo = PXContext.SessionTyped<PXSessionStatePXData>().FileInfo.Pop<FileInfo>("UploadedSnapshotKey");
      if (fileInfo == null)
        throw new PXException("The file is not found, or you don't have enough rights to see the file.");
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate(((PXSnapshotBase) new PXSnapshotUploader(this.GetCurrentCompany(), fileInfo.BinData, fileInfo.ImportIncludeData, fileInfo.TestPassBeforeImport)).Start));
    }
    return adapter.Get();
  }

  [PXButton(ImageKey = "Settings", Tooltip = "Manage users of the current tenant.")]
  [PXUIField(DisplayName = "Manage Users", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete, Visible = false)]
  protected IEnumerable manageUsersCommand(PXAdapter adapter)
  {
    UPCompany current1 = this.Companies.Current;
    PX.SM.Users current2 = this.Users.Current;
    if (current1 == null)
      throw new PXException("A tenant is not selected.");
    throw new PXRedirectByScreenIDException("SM201010", PXBaseRedirectException.WindowMode.New, true, (object) new
    {
      Username = current2.Username,
      cpid = current1.CompanyID
    });
  }

  [PXButton(ImageKey = "CheckIn", Tooltip = "Change the visibility of the selected snapshot.")]
  [PXUIField(DisplayName = "Change Visibility", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable changeVisibilityCommand(PXAdapter adapter)
  {
    UPCompany current1 = this.Companies.Current;
    UPSnapshot current2 = this.Snapshots.Current;
    if (current1 == null || current2 == null)
      return adapter.Get();
    UPSnapshot upSnapshot = current2;
    int? nullable1 = current2.SourceCompany;
    int? nullable2;
    if (nullable1.HasValue)
    {
      nullable1 = new int?();
      nullable2 = nullable1;
    }
    else
      nullable2 = current1.CompanyID;
    upSnapshot.SourceCompany = nullable2;
    this.Snapshots.Cache.Persist((object) current2, PXDBOperation.Update);
    this.Snapshots.View.Clear();
    return adapter.Get();
  }

  [PXButton(Tooltip = "Change to the test tenant.", CommitChanges = true, Category = "Actions")]
  [PXUIField(DisplayName = "Change to Test Tenant", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable trialCompanyCommand(PXAdapter adapter)
  {
    UPCompany current = this.Companies.Current;
    if (current == null)
      throw new PXException("A snapshot is not selected.");
    if (this.Companies.Ask("Warning", PXMessages.LocalizeFormatNoPrefix("Please confirm that you want to make '{0}' a test tenant. Test tenants are intended for training and demonstrating Acumatica, and are not intended for production use. This operation cannot be reverted.", (object) current.Description), MessageButtons.OKCancel, MessageIcon.Warning, true) != WebDialogResult.OK)
      return adapter.Get();
    PXDatabase.Provider.GetMaintenance().InsertCompanyRecord(current.CompanyID.Value, current.CompanyCD, new int?(current.ParentID.Value), PXDataTypesHelper.TrialCompany.Name, current.Hidden.Value, current.LoginName);
    PXDatabase.ResetSlots();
    this._licensingManager.InvalidateLicense();
    Redirector.Refresh(HttpContext.Current);
    return adapter.Get();
  }

  private int longRunsHere()
  {
    return this._longOperationTaskManager.GetTasks(screenID: PXContext.GetScreenID()).Count<RowTaskInfo>();
  }

  [PXButton(Tooltip = "Validate the database and delete orphaned records.", Category = "Actions", PopupVisible = true)]
  [PXUIField(DisplayName = "Optimize Database", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected IEnumerable deleteOrphanedRows(PXAdapter adapter)
  {
    TenantSnapshotDeletionProcess instance = PXGraph.CreateInstance<TenantSnapshotDeletionProcess>();
    instance.Filter.Cache.SetValueExt<DeletionAction.name>((object) instance.Filter.Current, (object) "O");
    foreach (PXResult<TenantSnapshotDeletion> pxResult in instance.Records.Select())
    {
      TenantSnapshotDeletion snapshotDeletion = (TenantSnapshotDeletion) pxResult;
      snapshotDeletion.Selected = new bool?(true);
      instance.Records.Update(snapshotDeletion);
    }
    throw new PXRedirectRequiredException((PXGraph) instance, (string) null);
  }

  [PXButton(Tooltip = "Hide the warning about the unsafe snapshot in the Help > About window.", Category = "Actions", PopupVisible = true)]
  [PXUIField(DisplayName = "Hide Warning")]
  protected void dismissedUnsafeSnapshotCommand()
  {
    UPSnapshotHistory restoredSnapshot = CompanyMaint.GetLastRestoredSnapshot();
    if (restoredSnapshot == null)
      return;
    UPSnapshotHistory row = this.SnapshotsHistory.Locate(restoredSnapshot) ?? restoredSnapshot;
    if (row == null)
      return;
    bool? dismissed = row.Dismissed;
    if (!dismissed.HasValue)
      return;
    dismissed = row.Dismissed;
    if (dismissed.Value)
      return;
    row.Dismissed = new bool?(true);
    this.SnapshotsHistory.Update(row);
    this.SnapshotsHistory.Cache.PersistUpdated((object) row);
    this.SnapshotsHistory.Cache.IsDirty = false;
  }

  [PXButton(Tooltip = "View Space Usage", Category = "Actions")]
  [PXUIField(DisplayName = "View Space Usage")]
  protected void spaceUsageCommand()
  {
    throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<SpaceUsageMaint>(), false, string.Empty);
  }

  [PXMergeAttributes(Method = MergeMethod.Replace)]
  [PXInt(IsKey = true)]
  [PXUIField(DisplayName = "Tenant ID", Visibility = PXUIVisibility.SelectorVisible)]
  [CompanyIncludeWithoutUsersSelector]
  protected virtual void _(Events.CacheAttached<UPCompany.companyID> e)
  {
  }

  protected virtual void UPCompany_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    UPCompany row = e.Row as UPCompany;
    PXEntryStatus status = sender.GetStatus((object) row);
    bool flag1 = PXLongOperation.Exists(this.UID);
    bool? nullable;
    int num1;
    if (status == PXEntryStatus.Inserted && PXDatabase.Provider is PXSqlDatabaseProvider)
    {
      nullable = row.System;
      num1 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag2 = num1 != 0;
    PXCache cache1 = sender;
    UPCompany data1 = row;
    nullable = row.Hidden;
    int num2 = nullable.GetValueOrDefault() ? 0 : (PXDatabase.Provider.DatabaseDefinedCompanies ? 1 : (!PXDatabase.Provider.GetLoginCompanies().Any<KeyValuePair<int, string>>() ? 1 : 0));
    PXUIFieldAttribute.SetEnabled<UPCompany.loginName>(cache1, (object) data1, num2 != 0);
    PXCache cache2 = sender;
    UPCompany data2 = row;
    int num3;
    if (status == PXEntryStatus.Inserted)
    {
      nullable = row.System;
      num3 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetEnabled<UPCompany.parentID>(cache2, (object) data2, num3 != 0);
    bool isEnabled = !flag2 && !flag1;
    this.ExportSnapshotCommand.SetEnabled(isEnabled);
    this.ReloadSnapshotCommand.SetEnabled(isEnabled);
    this.ImportSnapshotCommand.SetEnabled(isEnabled);
    this.UploadSnapshotCommand.SetEnabled(isEnabled);
    this.DownloadSnapshotCommand.SetEnabled(isEnabled);
    this.ManageUsersCommand.SetEnabled(isEnabled);
    this.PrepareAdbSnapshotCommand.SetEnabled(isEnabled);
    this.PrepareXmlSnapshotCommand.SetEnabled(isEnabled);
    this.DeleteOrphanedRows.SetEnabled(!flag1);
    this.ChangeVisibilityCommand.SetEnabled(!flag1);
    this.SaveCompanyCommand.SetEnabled(!flag1);
    this.TrialCompanyCommand.SetEnabled(!flag2 && !flag1 && this._licensingManager.License.Licensed && row.Status != CompanyLicenseStatus.Trial.ToString());
    this.CopyCompanyCommand.SetEnabled(!flag2 && !flag1 && PXCompanyHelper.SelectCompanies().Count<UPCompany>() > 1);
    this.InsertCompanyCommand.SetEnabled(!flag1 && sender.AllowInsert);
    this.Next.SetEnabled(!flag1);
    this.Previous.SetEnabled(!flag1);
    this.Cancel.SetEnabled(!flag1);
    this.DeleteCompanyCommand.SetEnabled(!flag2 && !flag1);
    YaqlVectorQuery orphanedCompaniesQuery = AcumaticaDbKeeperBase.getSelectOrphanedCompaniesQuery(false);
    int count = PXDatabase.Provider.CreateDbServicesPoint().selectVector<int>(orphanedCompaniesQuery, false).Count;
    if (count > 0 && !flag1 || count > 1 & flag1)
    {
      int num4 = this.longRunsHere();
      string error = string.Format(num4 > 0 ? PXLocalizer.Localize("The number of possibly orphaned snapshots in the database is {0}, which is an approximate value because tenant management operations are running. (The number of running operations is {1}.)") : PXLocalizer.Localize("The number of orphaned snapshots in the database is {0}. Click Optimize Database to fix."), (object) count, (object) num4);
      PXUIFieldAttribute.SetWarning<UPCompany.companyID>(this.Companies.Cache, e.Row, error);
    }
    PXUIFieldAttribute.SetVisible<UPSnapshotHistory.createdByID>(sender, e.Row);
    UPSnapshotHistory upSnapshotHistory = this.SnapshotsHistory.SelectSingle();
    if (upSnapshotHistory != null)
    {
      if (upSnapshotHistory != null)
      {
        nullable = upSnapshotHistory.Dismissed;
        if (nullable.HasValue)
        {
          nullable = upSnapshotHistory.Dismissed;
          if (nullable.Value)
          {
            nullable = upSnapshotHistory.IsSafe;
            if (nullable.HasValue)
            {
              nullable = upSnapshotHistory.IsSafe;
              if (!nullable.Value)
                goto label_17;
            }
          }
        }
      }
      if (upSnapshotHistory != null)
      {
        nullable = upSnapshotHistory.IsSafe;
        if (nullable.HasValue)
        {
          nullable = upSnapshotHistory.IsSafe;
          if (nullable.Value)
            goto label_17;
        }
      }
      this.DismissedUnsafeSnapshotCommand.SetEnabled(true);
      goto label_19;
    }
label_17:
    this.DismissedUnsafeSnapshotCommand.SetEnabled(false);
label_19:
    if (status == PXEntryStatus.Inserted || !PXCompanyHelper.IsNotVisibleCompany(row.CompanyID))
      return;
    this.ExportSnapshotCommand.SetEnabled(false);
    this.ImportSnapshotCommand.SetEnabled(false);
    this.CopyCompanyCommand.SetEnabled(false);
    this.TrialCompanyCommand.SetEnabled(false);
    this.UploadSnapshotCommand.SetEnabled(false);
    this.DownloadSnapshotCommand.SetEnabled(false);
    this.PrepareXmlSnapshotCommand.SetEnabled(false);
    this.PrepareAdbSnapshotCommand.SetEnabled(false);
    this.ChangeVisibilityCommand.SetEnabled(false);
    this.DeleteSnapshotCommand.SetEnabled(false);
    this.DeleteCompanyCommand.SetEnabled(true);
    PXUIFieldAttribute.SetWarning<UPCompany.companyID>(this.Companies.Cache, e.Row, PXMessages.LocalizeFormatNoPrefix("The data of the {0} tenant was corrupted and cannot be restored. You need to delete this tenant.", (object) row.Description));
  }

  protected virtual void UPCompany_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    UPCompany row = e.Row as UPCompany;
    int num1 = 0;
    int num2 = 0;
    foreach (int num3 in (IEnumerable<int>) PXCompanyHelper.SelectCompanies(PXCompanySelectOptions.Positive, true).Where<UPCompany>((System.Func<UPCompany, bool>) (c => c.CompanyID.HasValue)).Select<UPCompany, int>((System.Func<UPCompany, int>) (c => c.CompanyID.Value)).OrderBy<int, int>((System.Func<int, int>) (c => c)))
    {
      num2 = num3;
      ++num1;
      if (num2 > num1)
        break;
    }
    if (num2 == num1)
      ++num1;
    row.CompanyID = new int?(num1);
  }

  protected virtual void UPCompany_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (this.persisting)
      return;
    UPCompany row = e.Row as UPCompany;
    try
    {
      this.persisting = true;
      sender.RaiseRowPersisting((object) row, e.Operation);
      if (PXUIFieldAttribute.GetErrors(sender, (object) row, PXErrorLevel.Error).Count > 0)
        throw new PXException(DacDescriptorUtils.GetNonEmptyDacDescriptor(sender, (IBqlTable) row), "{0} '{1}' record raised at least one error. Please review the errors.", new object[2]
        {
          (object) "",
          (object) "Company"
        });
      if (row != null && CompanyMaint._tenantNameValidationRegex.IsMatch(row.CompanyCD ?? ""))
        throw new PXSetPropertyException<UPCompany.companyCD>("The tenant Name cannot contain any of the following characters: ',' ';' '<' '>' '*' '%' '&' ':' '\\' '?'. ");
      if (row != null)
      {
        if (CompanyMaint._tenantNameValidationRegex.IsMatch(row.LoginName ?? ""))
          throw new PXSetPropertyException<UPCompany.loginName>("The tenant Login Name cannot contain any of the following characters: ',' ';' '<' '>' '*' '%' '&' ':' '\\' '?'. ");
      }
    }
    finally
    {
      this.persisting = false;
    }
    e.Cancel = true;
  }

  protected virtual void UPCompany_ParentID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    UPCompany row = e.Row as UPCompany;
    int? companyId = row.CompanyID;
    int? parentId = row.ParentID;
    if (companyId.GetValueOrDefault() == parentId.GetValueOrDefault() & companyId.HasValue == parentId.HasValue)
      throw new PXSetPropertyException("The base tenant must be different from this tenant.");
  }

  protected virtual void UPCompany_ParentID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    UPCompany row = e.Row as UPCompany;
    if (row.ParentID.HasValue)
      return;
    UPCompany currentCompany = this.GetCurrentCompany();
    row.ParentID = currentCompany != null ? currentCompany.ParentID : new int?(1);
  }

  protected virtual void UPCompany_CompanyCD_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    string newValue = e.NewValue as string;
    if (!string.IsNullOrEmpty(newValue) && newValue.Contains<char>('@'))
      throw new PXSetPropertyException("The tenant name cannot contain \"@\".");
  }

  protected virtual void UPCompany_LoginName_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    string value = e.NewValue as string;
    if (value == null || value == string.Empty)
      return;
    UPCompany row = e.Row as UPCompany;
    if (row != null && PXCompanyHelper.SelectCompanies(PXCompanySelectOptions.Positive).Any<UPCompany>((System.Func<UPCompany, bool>) (c =>
    {
      int? companyId1 = c.CompanyID;
      int? companyId2 = row.CompanyID;
      return !(companyId1.GetValueOrDefault() == companyId2.GetValueOrDefault() & companyId1.HasValue == companyId2.HasValue) && PXLocalesProvider.CollationComparer.Equals(c.LoginName, value);
    })))
      throw new PXSetPropertyException("A tenant with the same name already exists in the system.");
    if (value.Contains<char>(',') || value.Contains<char>(';') || value.Contains<char>('<') || value.Contains<char>('>') || value.Contains<char>('@') || value.Contains<char>('+') || value.Contains<char>(':') || value.Contains<char>('[') || value.Contains<char>(']') || value.Contains<char>('#') || value.Contains<char>('=') || value.Contains<char>('|') || value.Contains<char>('{') || value.Contains<char>('}') || value.Contains<char>('$') || value.Contains<char>('^') || value.Contains<char>('?') || value.Contains<char>('/') || value.Contains<char>('\\') || value.Contains<char>('%') || value.Contains<char>('&'))
      throw new PXSetPropertyException("The login name cannot contain \",\", \";\", \":\", \"+\", \"=\", \"?\", \"^\", \"<\", \">\", \"/\", \"\\\", \"{\", \"}\", \"[\", \"]\", \"|\", \"#\", \"$\", \"%\", \"&\" and \"@\".");
  }

  protected virtual void UPCompany_LoginName_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    (e.Row as UPCompany).Altered = new bool?(true);
  }

  protected virtual void UPSnapshot_Version_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (e.Row == null || e.ReturnValue != null)
      return;
    e.ReturnValue = (object) IEnumerableExtensions.GetPriorityVersion(PXVersionHelper.GetDatabaseVersions()).Version;
  }

  protected virtual void _(Events.FieldSelecting<UPSnapshot.exportMode> e)
  {
    if (e.ReturnValue == null)
      return;
    string returnValue = (string) e.ReturnValue;
    PXListLocalizer pxListLocalizer = new PXListLocalizer();
    string[] strArray = new string[1]{ returnValue };
    pxListLocalizer.Localize("ExportMode", e.Cache, strArray, strArray);
    e.ReturnValue = (object) strArray[0];
  }

  protected virtual void ExportSnapshotSettings_Name_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ExportSnapshotSettings row = (ExportSnapshotSettings) e.Row;
    UPCompany current = this.Companies.Current;
    if (row == null || current == null || string.IsNullOrEmpty(current.CompanyCD))
      return;
    e.NewValue = (object) this.SelectedCompanyName;
  }

  protected virtual void ExportSnapshotSettings_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    PXStringListAttribute.SetList<ExportSnapshotSettings.exportMode>(sender, e.Row, PXSnapshotBase.GetExportModes(), PXSnapshotBase.GetExportModes(true));
  }

  protected string SelectedCompanyName
  {
    get
    {
      UPCompany current = this.Companies.Current;
      return current != null && !string.IsNullOrEmpty(current.CompanyCD) ? current.CompanyCD : string.Empty;
    }
  }

  protected static void PersistInserted(UPCompany master, List<UPCompany> inserted)
  {
    if (!inserted.Any<UPCompany>())
      return;
    if (string.IsNullOrEmpty(master.LoginName))
      master.LoginName = master.CompanyCD;
    ICompanyService instance = ServiceLocator.Current.GetInstance<ICompanyService>();
    IEnumerable<int> companies = inserted.Select<UPCompany, int>((System.Func<UPCompany, int>) (i => i.CompanyID.Value));
    instance.PrepareGeneralInfo(companies);
    foreach (UPCompany company in inserted)
    {
      instance.PrepareCompanyInfo();
      using (CompanyMaint.GetLoginScope(company.LoginName))
      {
        try
        {
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            PointDbmsBase pointWithOpenTransaction = PXDatabase.Provider.CreateDbServicesPoint((IDbTransaction) PXTransactionScope.GetTransaction());
            DbmsMaintenance maintenance = PXDatabase.Provider.GetMaintenance(pointWithOpenTransaction);
            bool flag = company.Hidden ?? false;
            maintenance.InsertCompanyRecord(company.CompanyID.Value, company.CompanyCD, new int?(company.ParentID.Value), PXDataTypesHelper.UserCompany.Name, flag, flag ? (string) null : company.LoginName);
            maintenance.InsertCompanyRecord(master.CompanyID.Value, master.CompanyCD, new int?(master.ParentID.Value), master.DataType, false, master.LoginName);
            maintenance.CorrectCompanyMask(CompanyMaint._prefetchedTablesWithCompanyMask.Select<System.Type, TableHeader>((System.Func<System.Type, TableHeader>) (t => pointWithOpenTransaction.Schema.GetTable(t.Name))).ToList<TableHeader>(), company.CompanyID);
            ProviderKeySuffixSlot.Set(Guid.NewGuid());
            instance.PersistCompanyInfo(company, false);
            transactionScope.Complete();
            PXTrace.Logger.ForSystemEvents("System", "System_TenantCreatedEventId").ForContext("ContextScreenId", (object) "SM203520", false).Information<string>("A new tenant has been created {TargetTenant}", company.LoginName);
          }
        }
        finally
        {
          ProviderKeySuffixSlot.Clear();
        }
      }
    }
    instance.AfterCompaniesPersist();
  }

  protected static void PersistUpdated(
    IEnumerable<UPCompany> updated,
    IRoleManagementService roleManagementService,
    ILegacyCompanyService legacyCompanyService)
  {
    string username;
    string company;
    legacyCompanyService.ParseLogin(PXContext.PXIdentity.UserName, out username, out company, out string _);
    string[] rolesForUser = roleManagementService.GetRolesForUser(username);
    ISlotStore instance = SlotStore.Instance;
    int? singleCompanyId = instance.GetSingleCompanyId();
    foreach (UPCompany upCompany in updated)
    {
      DbmsMaintenance maintenance = PXDatabase.Provider.GetMaintenance();
      int? nullable1 = upCompany.CompanyID;
      int num1 = nullable1.Value;
      string companyCd = upCompany.CompanyCD;
      nullable1 = upCompany.ParentID;
      int? nullable2 = new int?(nullable1.Value);
      string dataType = upCompany.DataType;
      bool? nullable3 = upCompany.Hidden;
      int num2 = nullable3.GetValueOrDefault() ? 1 : 0;
      string loginName = upCompany.LoginName;
      maintenance.InsertCompanyRecord(num1, companyCd, nullable2, dataType, num2 != 0, loginName);
      nullable3 = upCompany.Current;
      if (nullable3.GetValueOrDefault() && username != null && company != null)
        PXContext.PXIdentity.SetUser((IPrincipal) new GenericPrincipal((IIdentity) new GenericIdentity($"{username}@{upCompany.LoginName}"), rolesForUser));
    }
    if (singleCompanyId.HasValue)
      instance.SetSingleCompanyId(singleCompanyId.GetValueOrDefault());
    PXDatabase.ResetSlots();
    PXDatabase.Provider.GetMaintenance().ReinitialiseCompanies();
    PXDatabase.ClearCompanyCache();
  }

  private protected static void PersistSnapshot(
    UPCompany company,
    ImportSnapshotSettings settings,
    UPSnapshot snapshot,
    ISnapshotService snapshotService,
    ICacheControl<PageCache> pageCacheControl,
    IScreenInfoCacheControl screenInfoCacheControl,
    ILicensingManager licensingManager,
    IPXLogin pxLogin,
    ISessionContextFactory sessionContextFactory,
    ILegacyCompanyService legacyCompanyService)
  {
    string str1 = (string) null;
    string str2 = (string) null;
    bool flag;
    try
    {
      int? companyId = company.CompanyID;
      int currentCompany = PXInstanceHelper.CurrentCompany;
      flag = companyId.GetValueOrDefault() == currentCompany & companyId.HasValue;
      str1 = legacyCompanyService.ExtractUsername(PXContext.PXIdentity.UserName);
      str2 = flag ? legacyCompanyService.ExtractCompany(PXContext.PXIdentity.UserName) : company.LoginName;
      PX.SM.Users user = (PX.SM.Users) PXSelectBase<PX.SM.Users, PXSelectReadonly<PX.SM.Users, Where<PX.SM.Users.pKID, Equal<Required<PX.SM.Users.pKID>>>>.Config>.Select(new PXGraph(), (object) PXAccess.GetUserID());
      PreferencesSecurity preferences = (PreferencesSecurity) PXSelectBase<PreferencesSecurity, PXSelectReadonly<PreferencesSecurity>.Config>.Select(new PXGraph());
      try
      {
        new PXSnapshotRestorator(company, settings, snapshot).Start();
      }
      finally
      {
        CompanyMaint.PersistUser(company, user, preferences, true);
      }
      snapshotService.ApplyDataAfterSnapshotRestoration(company);
      PXTrace.Logger.ForSystemEvents("System", "System_SnapshotRestoredEventId").ForContext("ContextUserIdentity", (object) str1, false).ForContext("CurrentCompany", (object) str2, false).ForContext("ContextScreenId", (object) "SM203520", false).ForCurrentCompanyContext().Information<string, int?, string>("A snapshot has been successfully restored TargetTenant:{TargetTenant}, SourceTenant:{SourceTenant}, Snapshot:{Snapshot}", company?.LoginName, (int?) snapshot?.SourceCompany, snapshot?.Name);
    }
    catch (Exception ex)
    {
      PXTrace.Logger.ForSystemEvents("System", "System_SnapshotRestorationFailedEventId").ForContext("ContextUserIdentity", (object) str1, false).ForContext("CurrentCompany", (object) str2, false).ForContext("ContextScreenId", (object) "SM203520", false).Error<string, int?, string>(ex, "Snapshot restoration failed TargetTenant:{TargetTenant}, SourceTenant:{SourceTenant}, Snapshot:{Snapshot}", company?.LoginName, (int?) snapshot?.SourceCompany, snapshot?.Name);
      throw;
    }
    if (flag)
    {
      PXExtensionManager.ResetFeatures();
      pxLogin.InitBranch(str1, str2);
      pxLogin.SwitchCulture(str1, str2);
      sessionContextFactory.Abandon();
    }
    pageCacheControl.InvalidateCache();
    screenInfoCacheControl.InvalidateCache();
    licensingManager.RequestLogOut(str2);
  }

  protected internal static void PersistUser(
    UPCompany company,
    PX.SM.Users user,
    PreferencesSecurity preferences,
    bool disableSchedulers,
    DbmsMaintenance maint = null,
    Dictionary<string, List<RoleClaims>> claims = null,
    Dictionary<string, List<RoleActiveDirectory>> directories = null)
  {
    if (maint == null)
      maint = PXDatabase.Provider.GetMaintenance();
    if (user == null)
      user = (PX.SM.Users) PXSelectBase<PX.SM.Users, PXSelectReadonly<PX.SM.Users, Where<PX.SM.Users.pKID, Equal<Required<PX.SM.Users.pKID>>>>.Config>.Select(new PXGraph(), (object) PXAccess.GetUserID());
    if (preferences == null)
      preferences = (PreferencesSecurity) PXSelectBase<PreferencesSecurity, PXSelectReadonly<PreferencesSecurity>.Config>.Select(new PXGraph());
    int? nullable1 = user.Source;
    int num = 0;
    bool flag1 = !(nullable1.GetValueOrDefault() == num & nullable1.HasValue);
    if (flag1)
    {
      if (claims == null)
      {
        claims = new Dictionary<string, List<RoleClaims>>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
        foreach (PXResult<RoleClaims> pxResult in PXSelectBase<RoleClaims, PXSelectReadonly<RoleClaims>.Config>.Select(new PXGraph()))
        {
          RoleClaims roleClaims = (RoleClaims) pxResult;
          if (!string.IsNullOrEmpty(roleClaims.Role))
          {
            List<RoleClaims> roleClaimsList;
            if (!claims.TryGetValue(roleClaims.Role, out roleClaimsList))
              claims[roleClaims.Role] = roleClaimsList = new List<RoleClaims>();
            roleClaimsList.Add(roleClaims);
          }
        }
      }
      if (directories == null)
      {
        directories = new Dictionary<string, List<RoleActiveDirectory>>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
        foreach (PXResult<RoleActiveDirectory> pxResult in PXSelectBase<RoleActiveDirectory, PXSelectReadonly<RoleActiveDirectory>.Config>.Select(new PXGraph()))
        {
          RoleActiveDirectory roleActiveDirectory = (RoleActiveDirectory) pxResult;
          if (!string.IsNullOrEmpty(roleActiveDirectory.Role))
          {
            List<RoleActiveDirectory> roleActiveDirectoryList;
            if (!directories.TryGetValue(roleActiveDirectory.Role, out roleActiveDirectoryList))
              directories[roleActiveDirectory.Role] = roleActiveDirectoryList = new List<RoleActiveDirectory>();
            roleActiveDirectoryList.Add(roleActiveDirectory);
          }
        }
      }
    }
    string password = user.Password;
    IUserService instance1 = ServiceLocator.Current.GetInstance<IUserService>();
    instance1.BeforeSaveUser();
    try
    {
      maint.ReinitialiseCompanies();
      PXDatabase.ClearCompanyCache();
      string[] companies = PXDatabase.Companies;
      PXDatabase.ResetCredentials();
      using (CompanyMaint.GetLoginScope(company.LoginName))
      {
        Access instance2 = PXGraph.CreateInstance<Access>();
        if (!flag1)
        {
          PX.SM.Users users1 = (PX.SM.Users) PXSelectBase<PX.SM.Users, PXSelectReadonly<PX.SM.Users, Where<PX.SM.Users.username, Equal<Required<PX.SM.Users.username>>>>.Config>.Select((PXGraph) instance2, (object) user.Username).FirstOrDefault<PXResult<PX.SM.Users>>();
          if (users1 == null)
          {
            PX.SM.Users users2 = user;
            nullable1 = new int?();
            int? nullable2 = nullable1;
            users2.ContactID = nullable2;
            user = instance2.UserList.Insert(user);
          }
          user.Password = password;
          user.PasswordChangeOnNextLogin = new bool?(false);
          user.GeneratePassword = new bool?(false);
          user.OverrideADRoles = new bool?(true);
          PX.SM.Users users3 = user;
          nullable1 = new int?();
          int? nullable3 = nullable1;
          users3.ContactID = nullable3;
          if (users1 != null)
          {
            user.PKID = users1.PKID;
            user.GroupMask = users1.GroupMask;
          }
          user = instance2.UserList.Update(user);
        }
        if (flag1)
        {
          bool? overrideAdRoles = user.OverrideADRoles;
          bool flag2 = true;
          if (!(overrideAdRoles.GetValueOrDefault() == flag2 & overrideAdRoles.HasValue))
            goto label_63;
        }
        if (flag1)
        {
          instance2.FieldVerifying.AddHandler<UsersInRoles.username>((PXFieldVerifying) ((s, e) => e.Cancel = true));
          using (new SuppressPushNotificationsScope())
            PXAccess.GetTrueUserID();
          if (instance2.UserList.Cache.Locate((IDictionary) new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
          {
            {
              "Username",
              (object) user.Username
            }
          }) > 0)
          {
            instance2.UserList.Current.OverrideADRoles = user.OverrideADRoles;
            instance2.UserList.Update(instance2.UserList.Current);
          }
        }
        bool flag3 = false;
        foreach (PXResult<UsersInRoles> pxResult in instance2.RoleList.Select())
        {
          UsersInRoles usersInRoles = (UsersInRoles) pxResult;
          if (((IEnumerable<string>) PXAccess.GetAdministratorRoles()).First<string>() == usersInRoles.Rolename)
          {
            flag3 = true;
            break;
          }
        }
        instance2.RoleList.View.Clear();
        if (!flag3)
        {
          foreach (string str in instance1.GetRolesToCopy())
          {
            UsersInRoles usersInRoles = new UsersInRoles()
            {
              Rolename = str,
              Username = user.Username
            };
            instance2.RoleList.Insert(usersInRoles);
          }
        }
label_63:
        if (flag1 && PXSelectBase<RoleClaims, PXSelectReadonly<RoleClaims>.Config>.Select(new PXGraph()).Count == 0 && PXSelectBase<RoleActiveDirectory, PXSelectReadonly<RoleActiveDirectory>.Config>.Select(new PXGraph()).Count == 0)
        {
          foreach (string key in instance1.GetRolesToCopy())
          {
            if (!string.IsNullOrEmpty(key))
            {
              List<RoleClaims> roleClaimsList;
              if (claims != null && claims.TryGetValue(key, out roleClaimsList))
              {
                foreach (RoleClaims roleClaims in roleClaimsList)
                  instance2.ClaimsMap.Insert(roleClaims);
              }
              List<RoleActiveDirectory> roleActiveDirectoryList;
              if (directories != null && directories.TryGetValue(key, out roleActiveDirectoryList))
              {
                foreach (RoleActiveDirectory roleActiveDirectory in roleActiveDirectoryList)
                  instance2.ActiveDirectoryMap.Insert(roleActiveDirectory);
              }
            }
          }
        }
        instance2.Save.Press();
        PXResult<PreferencesSecurity> pxResult1 = PXSelectBase<PreferencesSecurity, PXSelectReadonly<PreferencesSecurity>.Config>.Select((PXGraph) instance2, (object) user.Username).FirstOrDefault<PXResult<PreferencesSecurity>>();
        PreferencesSecurity preferencesSecurity = pxResult1 != null ? (PreferencesSecurity) pxResult1 : new PreferencesSecurity();
        if (preferences != null)
        {
          preferences.DBCertificateName = preferencesSecurity.DBCertificateName;
          preferences.DBPrevCertificateName = preferencesSecurity.DBPrevCertificateName;
          preferences.PdfCertificateName = preferencesSecurity.PdfCertificateName;
        }
        PreferencesSecurityMaint instance3 = PXGraph.CreateInstance<PreferencesSecurityMaint>();
        instance3.SkipMultiFactorSwitchOnValidation = true;
        instance3.Prefs.Update(preferences);
        instance3.Save.Press();
        if (!flag1)
        {
          user.OldPassword = password;
          user.NewPassword = password;
          user.ConfirmPassword = password;
          instance2.UserManagementService.UpdateUserPassword(user.Username, password);
        }
        if (disableSchedulers)
          CompanyMaint.DisableSchedulers();
        PXDBLocalizableStringAttribute.EnsureTranslations((System.Func<string, bool>) (tableName => true));
        PXDatabase.ResetSlots();
        PXDatabase.ClearCompanyCache();
      }
    }
    finally
    {
      PXDatabase.ResetCredentials();
    }
  }

  protected static void DisableSchedulers()
  {
    try
    {
      foreach (PXDataFieldAssign[] source in PXDatabase.SelectDataFields<AUSchedule>().ToArray<PXDataFieldAssign[]>())
      {
        PXDataFieldAssign pxDataFieldAssign1 = ((IEnumerable<PXDataFieldAssign>) source).First<PXDataFieldAssign>((System.Func<PXDataFieldAssign, bool>) (f => string.Equals(f.Column.Name, typeof (AUSchedule.screenID).Name, StringComparison.InvariantCultureIgnoreCase)));
        PXDataFieldAssign pxDataFieldAssign2 = ((IEnumerable<PXDataFieldAssign>) source).First<PXDataFieldAssign>((System.Func<PXDataFieldAssign, bool>) (f => string.Equals(f.Column.Name, typeof (AUSchedule.scheduleID).Name, StringComparison.InvariantCultureIgnoreCase)));
        ((IEnumerable<PXDataFieldAssign>) source).First<PXDataFieldAssign>((System.Func<PXDataFieldAssign, bool>) (f => string.Equals(f.Column.Name, typeof (AUSchedule.isActive).Name, StringComparison.InvariantCultureIgnoreCase))).Value = (object) false;
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>((IEnumerable<PXDataFieldParam>) ((IEnumerable<PXDataFieldAssign>) source).Where<PXDataFieldAssign>((System.Func<PXDataFieldAssign, bool>) (f => !string.Equals(f.Column.Name, typeof (AUSchedule.scheduleID).Name, StringComparison.InvariantCultureIgnoreCase))));
          pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<AUSchedule.screenID>(pxDataFieldAssign1.Value));
          pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<AUSchedule.scheduleID>(pxDataFieldAssign2.Value));
          pxDataFieldParamList.Add((PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed);
          try
          {
            PXDatabase.Update<AUSchedule>(pxDataFieldParamList.ToArray());
            transactionScope.Complete();
          }
          catch (PXDbOperationSwitchRequiredException ex)
          {
            PXDatabase.Insert<AUSchedule>(((IEnumerable<PXDataFieldAssign>) source).ToArray<PXDataFieldAssign>());
            transactionScope.Complete();
          }
        }
        PXDatabase.SelectTimeStamp();
        PXDatabase.ResetSlots();
      }
    }
    catch
    {
    }
  }

  public void OnPackageUploaded(string fileName, string password, byte[] content)
  {
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => new PXSnapshotUploader(this.GetCurrentCompany(), content).Start()));
  }

  public static UPSnapshotHistory GetLastRestoredSnapshot()
  {
    CompanyMaint instance = PXGraph.CreateInstance<CompanyMaint>();
    int? singleCompanyId = SlotStore.Instance.GetSingleCompanyId();
    if (!singleCompanyId.HasValue)
      return (UPSnapshotHistory) null;
    return new PXSelectJoin<UPSnapshotHistory, InnerJoin<UPSnapshot, On<UPSnapshotHistory.snapshotID, Equal<UPSnapshot.snapshotID>>>, Where<UPSnapshotHistory.targetCompany, Equal<Optional<UPCompany.companyID>>>, OrderBy<Desc<UPSnapshotHistory.historyID>>>((PXGraph) instance).SelectSingle((object) singleCompanyId.Value);
  }
}
