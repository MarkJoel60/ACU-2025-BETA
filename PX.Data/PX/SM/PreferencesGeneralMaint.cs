// Decompiled with JetBrains decompiler
// Type: PX.SM.PreferencesGeneralMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Caching;
using PX.Common;
using PX.Data;
using PX.Data.Maintenance.SM.DAC;
using PX.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

#nullable disable
namespace PX.SM;

public class PreferencesGeneralMaint : PXGraph<PreferencesGeneralMaint>
{
  public PXSelect<WikiPageSimple, Where<WikiPageSimple.pageID, Equal<WikiPageSimple.pageID>>, OrderBy<Desc<WikiPage.articleType, Asc<WikiPage.number>>>> Articles;
  public PXSelect<PreferencesGeneralMaint.WikiPageSimpleForHelp, Where<WikiPageSimple.pageID, Equal<WikiPageSimple.pageID>>, OrderBy<Desc<WikiPage.articleType, Asc<WikiPage.number>>>> ArticlesForHelp;
  public PXSelect<PreferencesGlobal> PrefsGlobal;
  public PXSelect<PreferencesGeneral> Prefs;
  public PXSave<PreferencesGeneral> save;
  public PXCancel<PreferencesGeneral> Cancel;
  public PXAction<PreferencesGeneral> ResetColors;

  public PreferencesGeneralMaint()
  {
    this.Views.Caches.Add(typeof (PXThemeLoader.Branch));
    this.Views.Caches.Add(typeof (PXThemeLoader.Organization));
    string[] array = PXThemeLoader.AvailableThemes.ToArray<string>();
    PXStringListAttribute.SetList<PreferencesGeneral.theme>(this.Prefs.Cache, (object) null, array, array);
  }

  [InjectDependency]
  protected ICacheControl<PageCache> PageCacheControl { get; set; }

  [InjectDependency]
  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }

  [InjectDependency]
  private PXSiteMapProvider SiteMapProvider { get; set; }

  [InjectDependency]
  private PXWikiProvider WikiProvider { get; set; }

  protected virtual IEnumerable articles(string PageID)
  {
    Guid parentID = GUID.CreateGuid(PageID) ?? Guid.Empty;
    foreach (PXSiteMapNode childNode in (IEnumerable<PXSiteMapNode>) this.WikiProvider.FindSiteMapNodeFromKey(parentID).ChildNodes)
    {
      if (this.WikiProvider.GetAccessRights(childNode.NodeID) >= PXWikiRights.Select)
      {
        WikiPageSimple wikiPageSimple = new WikiPageSimple();
        wikiPageSimple.PageID = new Guid?(childNode.NodeID);
        wikiPageSimple.Title = childNode.Title;
        wikiPageSimple.ParentUID = new Guid?(parentID);
        yield return (object) wikiPageSimple;
      }
    }
  }

  protected virtual IEnumerable articlesForHelp(string PageID)
  {
    Guid parentID = GUID.CreateGuid(PageID) ?? Guid.Empty;
    foreach (PXSiteMapNode childNode in (IEnumerable<PXSiteMapNode>) this.WikiProvider.FindSiteMapNodeFromKey(parentID).ChildNodes)
    {
      if (this.WikiProvider.GetAccessRights(childNode.NodeID) >= PXWikiRights.Select)
      {
        PreferencesGeneralMaint.WikiPageSimpleForHelp pageSimpleForHelp = new PreferencesGeneralMaint.WikiPageSimpleForHelp();
        pageSimpleForHelp.PageID = new Guid?(childNode.NodeID);
        pageSimpleForHelp.Title = childNode.Title;
        pageSimpleForHelp.ParentUID = new Guid?(parentID);
        yield return (object) pageSimpleForHelp;
      }
    }
  }

  [PXUIField(DisplayName = "Reset to default colors")]
  [PXButton]
  public virtual void resetColors()
  {
    if (this.Prefs.Ask("Confirm Colors Reset", "Colors will be reset to default colors of the current theme for all companies and branches after you save your changes. Do you want to proceed?", MessageButtons.OKCancel) != WebDialogResult.OK)
      return;
    PXResultset<ThemeVariables> pxResultset1 = PXSelectBase<ThemeVariables, PXSelect<ThemeVariables, Where<ThemeVariables.theme, Equal<Current<PreferencesGeneral.theme>>>>.Config>.Select((PXGraph) this);
    PXCache cach1 = this.Caches[typeof (ThemeVariables)];
    foreach (PXResult<ThemeVariables> pxResult in pxResultset1)
      cach1.Delete((object) pxResult);
    PXResultset<PXThemeLoader.Branch> pxResultset2 = PXSelectBase<PXThemeLoader.Branch, PXSelect<PXThemeLoader.Branch, Where<PXThemeLoader.Branch.overrideThemeVariables, Equal<PX.Data.True>>>.Config>.Select((PXGraph) this);
    PXCache cach2 = this.Caches[typeof (PXThemeLoader.Branch)];
    foreach (PXResult<PXThemeLoader.Branch> pxResult in pxResultset2)
    {
      PXThemeLoader.Branch branch = (PXThemeLoader.Branch) pxResult;
      branch.OverrideThemeVariables = new bool?(false);
      cach2.Update((object) branch);
    }
    PXResultset<PXThemeLoader.Organization> pxResultset3 = PXSelectBase<PXThemeLoader.Organization, PXSelect<PXThemeLoader.Organization, Where<PXThemeLoader.Organization.overrideThemeVariables, Equal<PX.Data.True>>>.Config>.Select((PXGraph) this);
    PXCache cach3 = this.Caches[typeof (PXThemeLoader.Organization)];
    foreach (PXResult<PXThemeLoader.Organization> pxResult in pxResultset3)
    {
      PXThemeLoader.Organization organization = (PXThemeLoader.Organization) pxResult;
      organization.OverrideThemeVariables = new bool?(false);
      cach3.Update((object) organization);
    }
  }

  internal virtual IEnumerable sitemap([PXGuid] Guid? NodeID)
  {
    bool needRoot = false;
    if (!NodeID.HasValue)
    {
      NodeID = new Guid?(Guid.Empty);
      needRoot = true;
    }
    return this.EnumNodes(NodeID.Value, needRoot);
  }

  protected virtual IEnumerable EnumNodes(Guid nodeId, bool needRoot)
  {
    PXSiteMapNode siteMapNodeFromKey = this.SiteMapProvider.FindSiteMapNodeFromKey(nodeId);
    if (needRoot)
    {
      yield return (object) this.CreateSiteMap(siteMapNodeFromKey);
    }
    else
    {
      foreach (PXSiteMapNode childNode in (IEnumerable<PXSiteMapNode>) this.SiteMapProvider.GetChildNodes(siteMapNodeFromKey))
        yield return (object) this.CreateSiteMap(childNode);
    }
  }

  protected virtual SiteMap CreateSiteMap(PXSiteMapNode node)
  {
    return new SiteMap()
    {
      NodeID = new Guid?(node.NodeID),
      ParentID = new Guid?(node.ParentID),
      ScreenID = node.ScreenID,
      Title = node.Title,
      Url = node.Url
    };
  }

  public static string GetDefaultTimeZoneId(string companyId)
  {
    string userName = PXContext.PXIdentity.User.Identity.Name;
    if (!PXContext.PXIdentity.IsAuthenticatedAndNotEmpty())
      userName = $"admin{(string.IsNullOrEmpty(companyId) ? string.Empty : "@")}{companyId}";
    using (new PXLoginScope(userName, PXAccess.GetAdministratorRoles()))
    {
      PreferencesGeneral preferencesGeneral = (PreferencesGeneral) PXGraph.CreateInstance<PreferencesGeneralMaint>().Prefs.SelectWindowed(0, 1);
      return preferencesGeneral == null ? (string) null : preferencesGeneral.TimeZone;
    }
  }

  [PXUIField(DisplayName = "Save", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXSaveButton]
  protected virtual IEnumerable Save(PXAdapter adapter)
  {
    int num = this.Caches[typeof (ThemeVariables)].IsDirty || this.Caches[typeof (PXThemeLoader.Branch)].IsDirty || this.Caches[typeof (PXThemeLoader.Organization)].IsDirty ? 1 : (this.Caches[typeof (PreferencesGeneral)].IsDirty ? 1 : 0);
    this.Persist();
    if (num == 0 || HttpContext.Current == null)
      return adapter.Get();
    this.PageCacheControl.InvalidateCache();
    throw new PXRefreshException();
  }

  public override void Persist()
  {
    PXCache cache = this.Prefs.Cache;
    PreferencesGeneral data = cache.Updated.Cast<PreferencesGeneral>().FirstOrDefault<PreferencesGeneral>();
    bool flag = false;
    if (data != null)
      flag = !string.Equals((string) cache.GetValueOriginal<PreferencesGeneral.defaultUI>((object) data), data.DefaultUI, StringComparison.OrdinalIgnoreCase);
    base.Persist();
    if (!flag)
      return;
    this.ScreenInfoCacheControl.InvalidateCache();
  }

  protected void _(Events.RowSelecting<WikiPage> e)
  {
    WikiPage row = e.Row;
    if (!row.PageID.HasValue)
      return;
    PXSiteMapNode siteMapNodeFromKey = this.WikiProvider.FindSiteMapNodeFromKey(row.PageID.Value);
    row.Title = siteMapNodeFromKey == null ? row.Name : siteMapNodeFromKey.Title;
    PXUIFieldAttribute.SetDisplayName<WikiPage.title>(e.Cache, "Primary Page");
  }

  protected virtual void PreferencesGeneral_PortalExternalAccessLink_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if ((PreferencesGeneral) e.Row == null)
      return;
    string pattern = "^\\~\\/[a-zA-Z0-9]";
    string newValue = (string) e.NewValue;
    Regex regex1 = new Regex("[a-zA-Z]://[a-zA-Z0-9]");
    Regex regex2 = new Regex(pattern);
    if (string.IsNullOrEmpty(newValue))
      return;
    Match match1 = regex1.Match(newValue);
    Match match2 = regex2.Match(newValue);
    if (!match1.Success && !match2.Success)
      throw new PXException("The URL {0} is not valid. This is an example of a valid URL: http://www.acumatica.com.", new object[1]
      {
        (object) newValue
      });
  }

  protected virtual void PreferencesGeneral_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    string[] array = PXThemeLoader.AvailableThemes.ToArray<string>();
    string theme1 = ((PreferencesGeneral) e.Row).Theme;
    PXStringListAttribute.SetList<PreferencesGeneral.theme>(cache, (object) null, array, array);
    if (!(e.Row is PreferencesGeneral row1))
      return;
    PXCache cache1 = cache;
    PreferencesGeneral data = row1;
    int? deletingMlEventsMode = row1.DeletingMLEventsMode;
    int num1 = 0;
    int num2 = deletingMlEventsMode.GetValueOrDefault() == num1 & deletingMlEventsMode.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PreferencesGeneral.mLEventsRetentionAge>(cache1, (object) data, num2 != 0);
    PXCache pxCache = cache;
    PreferencesGeneral row2 = row1;
    string theme2 = row1.Theme;
    PXSetPropertyException propertyException;
    if (!((IEnumerable<string>) array).Contains<string>(theme1))
      propertyException = new PXSetPropertyException("The '{0}' theme not found in the ~\\App_Themes\\ folder. The default theme will be used.", new object[1]
      {
        (object) theme1
      });
    else
      propertyException = (PXSetPropertyException) null;
    pxCache.RaiseExceptionHandling<PreferencesGeneral.theme>((object) row2, (object) theme2, (Exception) propertyException);
    bool isVisible = !string.IsNullOrEmpty(SitePolicy.GetHelpApiHref(false)) && !string.IsNullOrEmpty(SitePolicy.GetHelpApiKey());
    PXUIFieldAttribute.SetVisible<PreferencesGeneral.useMLSearch>(cache, (object) row1, isVisible);
  }

  protected virtual void PreferencesGeneral_RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    if (e.Row == null)
      return;
    if (e.TranStatus == PXTranStatus.Open)
    {
      PreferencesGeneral row = (PreferencesGeneral) e.Row;
      PreferencesGeneral original = (PreferencesGeneral) cache.GetOriginal((object) row);
      if (!(row.PersonNameFormat != original.PersonNameFormat))
        return;
      row.NeedUpdatePersonNames = new bool?(true);
    }
    else
    {
      if (e.TranStatus != PXTranStatus.Completed)
        return;
      PreferencesGeneral prefs = e.Row as PreferencesGeneral;
      bool? updatePersonNames = prefs.NeedUpdatePersonNames;
      bool flag = true;
      if (!(updatePersonNames.GetValueOrDefault() == flag & updatePersonNames.HasValue))
        return;
      PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() =>
      {
        PreferencesGeneralMaint instance = PXGraph.CreateInstance<PreferencesGeneralMaint>();
        instance.Prefs.Current = prefs;
        instance.UpdatePersonDisplayNames(prefs.PersonNameFormat);
      }));
    }
  }

  protected virtual void UpdatePersonDisplayNames(string PersonDisplayNameFormat)
  {
    switch (PersonDisplayNameFormat)
    {
      case "WESTERN":
        PXDatabase.Update(new PXGraph(), (IBqlUpdate) new Update<Set<Users.fullName, Add<IsNull<Add<Users.firstName, Space>, PX.Data.Empty>, Users.lastName>>, PX.Data.Select<Users, Where<Users.lastName, IsNotNull>>>());
        break;
      case "LEGACY":
      case "EASTERN":
      case "EASTERN_WITH_TITLE":
        PXDatabase.Update(new PXGraph(), (IBqlUpdate) new Update<Set<Users.fullName, Add<Users.lastName, IsNull<Add<CommaSpace, Users.firstName>, PX.Data.Empty>>>, PX.Data.Select<Users, Where<Users.lastName, IsNotNull>>>());
        break;
    }
    PXDatabase.ClearCompanyCache();
  }

  protected virtual void PreferencesGeneral_PersonNameFormat_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PreferencesGeneral row))
      return;
    sender.RaiseExceptionHandling<PreferencesGeneral.personNameFormat>((object) row, (object) null, (Exception) new PXSetPropertyException("The display name order will be changed for all contacts, employees, and users. This may take up to a few minutes, depending on the number of records to be processed.", PXErrorLevel.Warning));
  }

  protected virtual void _(Events.RowUpdated<PreferencesGeneral> e)
  {
    this.CheckAndWarnIfDefaultUiChanged(e);
  }

  private void CheckAndWarnIfDefaultUiChanged(Events.RowUpdated<PreferencesGeneral> e)
  {
    PreferencesGeneral row = e.Row;
    PreferencesGeneral oldRow = e.OldRow;
    if (row.DefaultUI == oldRow.DefaultUI || oldRow.DefaultUI == null && row.DefaultUI == "E")
      return;
    int num = (int) this.Prefs.Ask("Change Default UI", PXLocalizer.LocalizeFormat("All forms with Default in the UI column of the Site Map (SM200520) form and that support the {0} UI will be switched to it for all users of the current tenant.", (object) (row.DefaultUI == "T" ? "Modern" : "Classic")), MessageButtons.OK);
  }

  protected virtual void _(
    Events.FieldSelecting<PreferencesGlobal, PreferencesGlobal.enableTelemetry> e)
  {
    if (PXTelemetryInvoker.AllowDisableTelemetry())
      PXUIFieldAttribute.SetVisible<PreferencesGlobal.enableTelemetry>(e.Cache, (object) null, true);
    if (!(e.ReturnValue is bool returnValue) || returnValue)
      return;
    PXUIFieldAttribute.SetWarning<PreferencesGlobal.enableTelemetry>(e.Cache, (object) e.Row, "Disabling the sending of diagnostic information to Acumatica prevents Acumatica Support from using it for troubleshooting.");
  }

  public class WikiPageSimpleForHelp : WikiPageSimple
  {
  }
}
