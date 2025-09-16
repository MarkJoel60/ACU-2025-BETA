// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDatabaseSiteMapProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PX.Common;
using PX.Data.Access;
using PX.Data.Update;
using PX.Security;
using PX.SM;
using PX.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

/// <summary>
/// Represents site map provider based on the data stored in the DB
/// </summary>
public class PXDatabaseSiteMapProvider : PXSiteMapProvider
{
  protected static readonly System.Type[] Tables = new System.Type[5]
  {
    typeof (PX.SM.SiteMap),
    typeof (PX.SM.PortalMap),
    typeof (PX.SM.RolesInGraph),
    typeof (LocalizationTranslation),
    typeof (PreferencesGeneral)
  };
  /// <summary>Occurs when a new site map node is being added.</summary>
  public EventHandler<AddingNewSiteMapNodeEventArgs> AddingNew;
  protected internal string Table;

  [PXInternalUseOnly]
  public PXDatabaseSiteMapProvider(
    IOptions<PXSiteMapOptions> options,
    IHttpContextAccessor httpContextAccessor,
    IRoleManagementService roleManagementService)
    : base(options, httpContextAccessor, roleManagementService)
  {
    this.Table = WebAppType.Current.IsPortal() ? typeof (PX.SM.PortalMap).FullName : typeof (PX.SM.SiteMap).FullName;
  }

  protected override PXSiteMapProvider.Definition GetSlot(string slotName)
  {
    return (PXSiteMapProvider.Definition) PXDatabase.GetSlot<PXDatabaseSiteMapProvider.DatabaseDefinition, PXDatabaseSiteMapProvider>(slotName + Thread.CurrentThread.CurrentUICulture.Name, this, PXDatabaseSiteMapProvider.Tables);
  }

  protected override void ResetSlot(string slotName)
  {
    PXDatabase.ResetSlot<PXDatabaseSiteMapProvider.DatabaseDefinition>(slotName + Thread.CurrentThread.CurrentUICulture.Name, PXDatabaseSiteMapProvider.Tables);
  }

  protected void FireAddingNew(AddingNewSiteMapNodeEventArgs e) => this.OnAddingNew(e);

  protected virtual void OnAddingNew(AddingNewSiteMapNodeEventArgs e)
  {
    EventHandler<AddingNewSiteMapNodeEventArgs> addingNew = this.AddingNew;
    if (addingNew == null)
      return;
    addingNew((object) this, e);
  }

  protected class DatabaseDefinition : 
    PXSiteMapProvider.Definition,
    IPrefetchable<PXDatabaseSiteMapProvider>,
    IPXCompanyDependent
  {
    [NonSerialized]
    private readonly ISiteMapNodeFactory _siteMapNodeCreator;
    [NonSerialized]
    private readonly ISiteMapUrlTransformer _siteMapUrlTransformer;
    [NonSerialized]
    private readonly ScreenUiSwitchingChecker _screenSwitchingChecker;

    public DatabaseDefinition()
    {
      if (!ServiceLocator.IsLocationProviderSet)
        return;
      this._siteMapNodeCreator = ServiceLocator.Current.GetInstance<ISiteMapNodeFactory>();
      this._siteMapUrlTransformer = ServiceLocator.Current.GetInstance<ISiteMapUrlTransformer>();
      this._screenSwitchingChecker = ServiceLocator.Current.GetInstance<ScreenUiSwitchingChecker>();
    }

    void IPrefetchable<PXDatabaseSiteMapProvider>.Prefetch(PXDatabaseSiteMapProvider provider)
    {
      System.Type type = (System.Type) null;
      if (!string.IsNullOrWhiteSpace(provider.Table))
        type = PXBuildManager.GetType(provider.Table, false);
      if (type == (System.Type) null)
        type = typeof (PX.SM.SiteMap);
      bool isTestTenant = PXCompanyHelper.IsTestTenant();
      System.Type table = type;
      PXDataField[] pxDataFieldArray = new PXDataField[7]
      {
        (PXDataField) new PXDataField<PX.SM.SiteMap.nodeID>(),
        (PXDataField) new PXDataField<PX.SM.SiteMap.title>(),
        (PXDataField) new PXDataField<PX.SM.SiteMap.url>(),
        (PXDataField) new PXDataField<PX.SM.SiteMap.screenID>(),
        (PXDataField) new PXDataField<PX.SM.SiteMap.parentID>(),
        (PXDataField) new PXDataFieldOrder<PX.SM.SiteMap.position>(),
        (PXDataField) new PXDataField<PX.SM.SiteMap.selectedUI>()
      };
      foreach (var data in PXDatabase.SelectMulti(table, pxDataFieldArray).Select(r =>
      {
        Guid? guid1 = r.GetGuid(0);
        Guid guid2 = guid1.Value;
        string str1 = r.GetString(1);
        string str2 = r.GetString(2);
        string str3 = r.GetString(3);
        guid1 = r.GetGuid(4);
        Guid guid3 = guid1.Value;
        string str4 = r.GetString(5) ?? "E";
        return new
        {
          NodeID = guid2,
          Title = str1,
          Url = str2,
          ScreenID = str3,
          ParentID = guid3,
          SelectedUi = str4
        };
      }).Where(r => r.NodeID != Guid.Empty).Prepend(new
      {
        NodeID = Guid.Empty,
        Title = "Root Node",
        Url = "~/Frames/Default.aspx",
        ScreenID = "00000000",
        ParentID = Guid.Empty,
        SelectedUi = "E"
      }).ToArray())
      {
        string str = this._siteMapUrlTransformer.TransformUrl(this._screenSwitchingChecker.UiSwitchingIsDisabled(new bool?(isTestTenant)) ? "E" : data.SelectedUi, data.ScreenID, data.Url);
        PXSiteMapNode node = this.CreateNode(provider, data.NodeID, data.Title, str, PXPageIndexingService.GetGraphType(str), data.ScreenID, data.SelectedUi, isTestTenant);
        AddingNewSiteMapNodeEventArgs e = new AddingNewSiteMapNodeEventArgs(node);
        provider.FireAddingNew(e);
        if (!e.Cancel)
          this.AddNode(node, data.ParentID);
      }
    }

    protected virtual void Prefetch(PXDatabaseSiteMapProvider provider)
    {
      ((IPrefetchable<PXDatabaseSiteMapProvider>) this).Prefetch(provider);
    }

    protected PXSiteMapNode CreateNode(
      PXDatabaseSiteMapProvider provider,
      Guid nodeID,
      string title,
      string aspxUrl,
      string graphType,
      string screenID,
      string selectedUi)
    {
      return this.CreateNode(provider, nodeID, title, aspxUrl, graphType, screenID, selectedUi, false);
    }

    protected PXSiteMapNode CreateNode(
      PXDatabaseSiteMapProvider provider,
      Guid nodeID,
      string title,
      string aspxUrl,
      string graphType,
      string screenID,
      string selectedUi,
      bool isTestTenant)
    {
      PXRoleList roleList = (PXRoleList) null;
      if (!string.IsNullOrEmpty(screenID))
        roleList = PXAccess.RegisterGraphType(graphType, screenID);
      if (aspxUrl != null && aspxUrl.StartsWith("~/Frames/Default.aspx", StringComparison.OrdinalIgnoreCase) && screenID != "00000000" && ((IEnumerable<string>) PXUrl.GetParameters(aspxUrl.ToLower()).AllKeys).All<string>((Func<string, bool>) (x => x != "scrid")))
        aspxUrl = PXUrl.AppendUrlParameter(aspxUrl, "scrID", screenID);
      else if (aspxUrl == null)
        aspxUrl = string.Empty;
      PXSiteMapNode siteMapNode = this.CreateSiteMapNode(provider, nodeID, title, aspxUrl, graphType, screenID, selectedUi, roleList, isTestTenant);
      PXLocalizerRepository.SiteMapLocalizer.Localize(siteMapNode);
      return siteMapNode;
    }

    private PXSiteMapNode CreateSiteMapNode(
      PXDatabaseSiteMapProvider provider,
      Guid nodeID,
      string title,
      string aspxUrl,
      string graphType,
      string screenID,
      string selectedUi,
      PXRoleList roleList,
      bool isTestTenant)
    {
      PXSiteMapNode siteMapNode;
      switch (selectedUi)
      {
        case "E":
          siteMapNode = (PXSiteMapNode) new PXOldUiSiteMapNode((PXSiteMapProvider) provider, nodeID, aspxUrl, title, roleList, graphType, screenID);
          break;
        case "T":
          siteMapNode = this._siteMapNodeCreator.CreateNode(selectedUi, (PXSiteMapProvider) provider, nodeID, aspxUrl, title, roleList, graphType, screenID, isTestTenant);
          break;
        default:
          siteMapNode = this._siteMapNodeCreator.CreateNode(selectedUi, (PXSiteMapProvider) provider, nodeID, aspxUrl, title, roleList, graphType, screenID, isTestTenant);
          break;
      }
      return siteMapNode;
    }
  }
}
