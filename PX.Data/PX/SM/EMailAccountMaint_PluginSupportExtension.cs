// Decompiled with JetBrains decompiler
// Type: PX.SM.EMailAccountMaint_PluginSupportExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

[PXInternalUseOnly]
public class EMailAccountMaint_PluginSupportExtension : PXGraphExtension<EMailAccountMaint>
{
  public PXSelect<EMailAccountPluginDetail> Details;
  private Lazy<IEmailConnectedService> _service;
  public PXAction<EMailAccount> ReloadSettings;

  protected IEnumerable details() => (IEnumerable) this.FetchPluginSettings();

  [InjectDependency]
  public IPXEmailPluginService PluginService { get; private set; }

  public IEmailConnectedService Service => this._service.Value;

  public override void Initialize()
  {
    this._service = Lazy.By<IEmailConnectedService>((Func<IEmailConnectedService>) (() => this.Base.EMailAccounts.Current?.PluginTypeName != null ? this.PluginService.GetService(this.Base.EMailAccounts.Current, true) : (IEmailConnectedService) null));
  }

  [PXButton]
  [PXUIField(DisplayName = "Reload Settings", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected virtual IEnumerable reloadSettings(PXAdapter adapter)
  {
    this.ReloadParametersInt();
    return adapter.Get();
  }

  protected virtual void ReloadParametersInt()
  {
    if (!this.Service.GetStateFromRemote())
      return;
    PXCache cach = this.Base.Caches[this.Service.SettingsType];
    cach.Update(cach.Locate((object) this.Service.CurrentSettings));
  }

  protected virtual void _(Events.RowSelected<EMailAccount> e)
  {
    if (e.Row == null)
      return;
    bool isInDB = e.Cache.GetOriginal((object) e.Row) != null;
    PXUIFieldAttribute.SetEnabled<EMailAccount.pluginTypeName>(e.Cache, (object) e.Row, !isInDB);
    e.Cache.AdjustUI((object) e.Row).For<EMailAccount.pluginTypeName>((System.Action<PXUIFieldAttribute>) (_ => _.Enabled = !isInDB));
    bool isPlugin = false;
    bool? isOfPluginType = e.Row.IsOfPluginType;
    bool flag = true;
    if (isOfPluginType.GetValueOrDefault() == flag & isOfPluginType.HasValue)
    {
      isPlugin = PXSelectorAttribute.Select<EMailAccount.pluginTypeName>(e.Cache, (object) e.Row, (object) e.Row.PluginTypeName) != null;
      e.Cache.AdjustUI((object) e.Row).For<EMailAccount.isActive>((System.Action<PXUIFieldAttribute>) (_ => _.Enabled = isPlugin));
    }
    this.SetUIVisibility(e, isPlugin);
  }

  protected virtual void SetUIVisibility(Events.RowSelected<EMailAccount> e, bool isPlugin)
  {
    this.Details.AllowSelect = isPlugin;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = e.Cache.AdjustUI((object) e.Row).For<EMailAccount.defaultEmailAssignmentMapID>((System.Action<PXUIFieldAttribute>) (_ => _.Visible = !isPlugin));
    chained = chained.SameFor<EMailAccount.defaultWorkgroupID>();
    chained.SameFor<EMailAccount.defaultOwnerID>();
  }

  protected virtual void _(Events.RowPersisting<EMailAccountPluginDetail> e) => e.Cancel = true;

  protected virtual void _(Events.RowUpdated<EMailAccountPluginDetail> e)
  {
    if (e.Row == null || this.Service == null)
      return;
    PXCache cach = this.Base.Caches[this.Service.SettingsType];
    object data = cach.Locate((object) this.Service.CurrentSettings);
    if (data == null)
      return;
    cach.SetValueExt(data, e.Row.SettingID, (object) e.Row.Value);
    cach.Update(data);
    this.Details.View.RequestRefresh();
  }

  protected virtual void _(
    Events.FieldSelecting<EMailAccountPluginDetail, EMailAccountPluginDetail.settingID> e)
  {
    if (e.Row == null || this.Service == null)
      return;
    PXCache cach = this.Base.Caches[this.Service.SettingsType];
    if (!(cach.GetStateExt(cach.Locate((object) this.Service.CurrentSettings), e.Row.SettingID) is PXFieldState stateExt))
      return;
    switch (stateExt.ErrorLevel)
    {
      case PXErrorLevel.Warning:
      case PXErrorLevel.RowWarning:
        stateExt.ErrorLevel = PXErrorLevel.RowWarning;
        break;
      case PXErrorLevel.Error:
      case PXErrorLevel.RowError:
        stateExt.ErrorLevel = PXErrorLevel.RowError;
        break;
    }
    Events.FieldSelecting<EMailAccountPluginDetail, EMailAccountPluginDetail.settingID> fieldSelecting = e;
    object returnState = e.ReturnState;
    System.Type dataType = typeof (string);
    string error1 = stateExt.Error;
    PXErrorLevel errorLevel1 = stateExt.ErrorLevel;
    bool? isKey = new bool?();
    bool? nullable = new bool?();
    int? required = new int?();
    int? precision = new int?();
    int? length = new int?();
    string error2 = error1;
    int errorLevel2 = (int) errorLevel1;
    bool? enabled = new bool?();
    bool? visible = new bool?();
    bool? readOnly = new bool?();
    PXFieldState instance = PXFieldState.CreateInstance(returnState, dataType, isKey, nullable, required, precision, length, error: error2, errorLevel: (PXErrorLevel) errorLevel2, enabled: enabled, visible: visible, readOnly: readOnly);
    fieldSelecting.ReturnState = (object) instance;
  }

  protected virtual void _(
    Events.FieldSelecting<EMailAccountPluginDetail, EMailAccountPluginDetail.value> e)
  {
    if (e.Row == null || this.Service == null)
      return;
    PXCache cach = this.Base.Caches[this.Service.SettingsType];
    if (!(cach.GetStateExt(cach.Locate((object) this.Service.CurrentSettings), e.Row.SettingID) is PXFieldState stateExt))
      return;
    stateExt.Error = (string) null;
    stateExt.ErrorLevel = PXErrorLevel.Undefined;
    e.ReturnState = (object) stateExt;
  }

  protected virtual void _(
    Events.FieldUpdated<EMailAccount, EMailAccount.pluginTypeName> e)
  {
    if (e.Row == null)
      return;
    this.Details.Cache.Clear();
  }

  [PXOverride]
  public virtual void Persist(System.Action del)
  {
    if (this.Service != null)
    {
      this.Base.EnsureCachePersistence(this.Service.SettingsType);
      if (this.Base.Caches[this.Service.SettingsType].Locate((object) this.Service.CurrentSettings) is IEmailPluginSettings emailPluginSettings)
      {
        bool? isInitialized = emailPluginSettings.IsInitialized;
        bool flag1 = true;
        if (!(isInitialized.GetValueOrDefault() == flag1 & isInitialized.HasValue))
        {
          bool flag2 = this.Service.TryInitializeRemote();
          this.Base.Caches[this.Service.SettingsType].SetValueExt((object) emailPluginSettings, "IsInitialized", (object) flag2);
          this.Base.Caches[this.Service.SettingsType].MarkUpdated((object) emailPluginSettings);
        }
      }
    }
    del();
  }

  public virtual IEnumerable<EMailAccountPluginDetail> FetchPluginSettings()
  {
    EMailAccountMaint_PluginSupportExtension supportExtension = this;
    EMailAccount current = supportExtension.Base.EMailAccounts.Current;
    if (current != null && supportExtension.Service != null)
    {
      foreach (EMailAccountPluginDetail insertDetail in supportExtension.InsertDetails(current, supportExtension.Service))
        yield return insertDetail;
    }
  }

  public virtual IEnumerable<EMailAccountPluginDetail> InsertDetails(
    EMailAccount row,
    IEmailConnectedService service)
  {
    EMailAccountMaint_PluginSupportExtension supportExtension = this;
    PXCache pluginSettingsCache = supportExtension.Base.Caches[service.SettingsType];
    object settings = pluginSettingsCache.Locate((object) supportExtension.Service.CurrentSettings);
    if (settings == null)
      pluginSettingsCache.Hold((object) supportExtension.Service.CurrentSettings);
    settings = (object) supportExtension.Service.CurrentSettings;
    int i = 0;
    foreach (string field in (List<string>) pluginSettingsCache.Fields)
    {
      ++i;
      if (!pluginSettingsCache.Keys.Contains(field) && pluginSettingsCache.GetFieldOrdinal(field) != -1 && pluginSettingsCache.GetAttributesReadonly(field).OfType<PXUIFieldAttribute>().Any<PXUIFieldAttribute>() && pluginSettingsCache.GetStateExt(settings, field) is PXFieldState stateExt && stateExt.Visible)
      {
        EMailAccountPluginDetail row1 = new EMailAccountPluginDetail()
        {
          EmailAccountID = row.EmailAccountID,
          SortOrder = new int?(i),
          SettingID = stateExt.Name,
          Description = stateExt.DisplayName,
          Value = stateExt.Value?.ToString()
        };
        EMailAccountPluginDetail accountPluginDetail = (EMailAccountPluginDetail) supportExtension.Details.Cache.Locate((object) row1);
        if (accountPluginDetail == null)
          supportExtension.Details.Cache.Hold((object) row1);
        yield return accountPluginDetail ?? row1;
      }
    }
  }
}
