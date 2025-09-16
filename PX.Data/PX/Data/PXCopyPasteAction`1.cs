// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCopyPasteAction`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.DbServices.Model.DataSet;
using PX.DbServices.Model.ImportExport;
using PX.DbServices.Model.ImportExport.Serialization;
using PX.DbServices.QueryObjectModel;
using PX.Metadata;
using PX.SM;
using PX.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

#nullable disable
namespace PX.Data;

/// <summary>Implements the copy-paste functionality and creates the Edit button on the page datasource.</summary>
/// <typeparam name="TNode"></typeparam>
public class PXCopyPasteAction<TNode> : PXAction<TNode>, IPXCopyPasteAction where TNode : class, IBqlTable, new()
{
  private readonly string actionExceptionKey = "CopyPasteActionException";
  protected readonly ButtonMenu bmCopy = new ButtonMenu("CopyDocument", PXMessages.LocalizeNoPrefix("Copy"), (string) null)
  {
    Enabled = true,
    ImageKey = "Copy"
  };
  protected readonly ButtonMenu bmPaste = new ButtonMenu("PasteDocument", PXMessages.LocalizeNoPrefix("Paste"), (string) null)
  {
    Enabled = PXCopyPasteAction<TNode>.IsPasteAllowed(),
    ImageKey = "Paste"
  };
  protected readonly ButtonMenu bmSaveTemplate = new ButtonMenu("SaveTemplate", PXMessages.LocalizeNoPrefix("Save as Template..."), (string) null)
  {
    ImageKey = "Save"
  };
  protected readonly ButtonMenu bmExportXml = new ButtonMenu("ExportXml", PXMessages.LocalizeNoPrefix("Export as XML"), (string) null)
  {
    Enabled = true
  };
  protected readonly ButtonMenu bmImportXml = new ButtonMenu("ImportXml", PXMessages.LocalizeNoPrefix("Import from XML"), (string) null)
  {
    Enabled = true
  };
  protected readonly ButtonMenu bmResetToParent = new ButtonMenu("ResetToParent", PXMessages.LocalizeNoPrefix("Reset to Default"), (string) null)
  {
    Enabled = true,
    ImageKey = "Cancel"
  };
  protected readonly List<ButtonMenu> pasteFromTemplateButtons;
  public readonly string contextScreenId;
  private DataLoaderChecker loaderChecker;
  /// <exclude />
  public System.Action ImportXMLPostProcessor;
  /// <exclude />
  public System.Action<DataSetXmReader> ImportXMLValidator;
  protected static readonly object[] EmptyArray = new object[0];

  [InjectDependency]
  private IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }

  public PXCopyPasteAction(PXGraph graph, string name)
    : base(graph, name)
  {
    if (HttpContext.Current == null || !PXCopyPasteData<PXGraph>.IsCurrentUserClipboardAvailable || PXContext.GetScreenID() == null)
    {
      this.SetVisible(false);
    }
    else
    {
      this.contextScreenId = PXContext.GetScreenID().ToUpperInvariant().Replace(".", "");
      bool flag1 = PXCopyPasteAction<TNode>.CanExportEntitiesOnScreen(this.contextScreenId);
      bool flag2 = graph.CanClipboardCopyPaste();
      this.ImportEnabled = flag1;
      AUTemplate[] array = ((IEnumerable<AUTemplate>) PXDatabase.GetSlot<AUTemplateCache>("AUTemplateCache", typeof (AUTemplate)).Items).Where<AUTemplate>((Func<AUTemplate, bool>) (_ => _.ScreenID == this.contextScreenId)).ToArray<AUTemplate>();
      if (!flag1 && !flag2 && array.Length == 0)
      {
        this.SetVisible(false);
      }
      else
      {
        graph.RowSelected.AddHandler<TNode>(new PXRowSelected(this.RowSelected));
        if (flag1)
        {
          try
          {
            this.loaderChecker = new DataLoaderChecker(PXDatabase.Provider.CreateDbServicesPoint(), PXCopyPasteAction<TNode>.GetExportTemplateForScreen(this.contextScreenId));
          }
          catch (Exception ex)
          {
          }
        }
        List<ButtonMenu> buttonMenuList1;
        if (!flag1)
          buttonMenuList1 = new List<ButtonMenu>()
          {
            this.bmCopy,
            this.bmPaste,
            this.bmSaveTemplate
          };
        else if (!flag2)
        {
          buttonMenuList1 = new List<ButtonMenu>();
        }
        else
        {
          buttonMenuList1 = new List<ButtonMenu>();
          buttonMenuList1.Add(this.bmCopy);
          buttonMenuList1.Add(this.bmPaste);
          buttonMenuList1.Add(this.bmSaveTemplate);
        }
        List<ButtonMenu> buttonMenuList2 = buttonMenuList1;
        if (flag1)
        {
          DataLoaderChecker loaderChecker = this.loaderChecker;
          if ((loaderChecker != null ? (loaderChecker.Relations.IsStableSharing ? 1 : 0) : 0) != 0)
            buttonMenuList2.Add(this.bmResetToParent);
          buttonMenuList2.Add(this.bmExportXml);
          buttonMenuList2.Add(this.bmImportXml);
        }
        this.pasteFromTemplateButtons = ((IEnumerable<AUTemplate>) array).Select<AUTemplate, ButtonMenu>((Func<AUTemplate, ButtonMenu>) (item => new ButtonMenu("#" + item.TemplateID.ToString(), string.Format(PXMessages.LocalizeNoPrefix("Paste from [{0}]"), (object) item.Description), Sprite.Tree.GetFullUrl("Leaf")))).ToList<ButtonMenu>();
        buttonMenuList2.AddRange((IEnumerable<ButtonMenu>) this.pasteFromTemplateButtons);
        this.SetMenu(buttonMenuList2.ToArray());
        this.MenuAutoOpen = true;
      }
    }
  }

  public bool ImportEnabled { get; set; }

  protected static bool IsPasteAllowed()
  {
    if (HttpContext.Current == null || !PXCopyPasteData<PXGraph>.IsCurrentUserClipboardAvailable || PXContext.GetScreenID() == null)
      return true;
    return !PXCopyPasteData<PXGraph>.CurrentUserClipboard.IsEmpty() && PXCopyPasteData<PXGraph>.CurrentUserClipboard.ScreenId == PXContext.GetScreenID().ToUpperInvariant().Replace(".", "");
  }

  protected virtual void RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    this.bmPaste.Enabled = (cache.GetExtensionTypes().Length != 0 && this._Graph.stateLoading || cache.IsKeysFilled(e.Row)) && PXCopyPasteAction<TNode>.IsPasteAllowed();
    this.bmExportXml.Enabled = e.Row != null && cache.GetStatus(e.Row) == PXEntryStatus.Notchanged;
    this.bmResetToParent.Enabled = false;
    if (e.Row == null || this.loaderChecker == null || cache.Graph.UnattendedMode)
      return;
    object currentObj = cache.Current;
    this.bmResetToParent.Enabled = this.loaderChecker.DoHaveParent(Yaql.and(cache.Keys.ToDictionary<string, string, object>((Func<string, string>) (a => a), (Func<string, object>) (k => cache.GetValue(currentObj, k))).Select<KeyValuePair<string, object>, YaqlCondition>((Func<KeyValuePair<string, object>, YaqlCondition>) (kv => Yaql.eq<object>((YaqlScalar) Yaql.column(kv.Key, (string) null), kv.Value)))));
  }

  public static ExportTemplate GetExportTemplateForType(System.Type tGraph)
  {
    PXSiteMapNode siteMapNodeUnsecure = PXSiteMap.Provider.FindSiteMapNodeUnsecure(tGraph);
    return siteMapNodeUnsecure != null ? PXCopyPasteAction<TNode>.GetExportTemplateForScreen(siteMapNodeUnsecure.ScreenID) : (ExportTemplate) null;
  }

  private static string getXmlExportRulesFileForScreen(string screenId)
  {
    return HostingEnvironment.MapPath($"~/App_Data/XmlExportDefinitions/{screenId}.xml");
  }

  public static bool CanExportEntitiesOnScreen(string screenId)
  {
    return File.Exists(PXCopyPasteAction<TNode>.getXmlExportRulesFileForScreen(screenId));
  }

  public static ExportTemplate GetExportTemplateForScreen(string screenId)
  {
    string rulesFileForScreen = PXCopyPasteAction<TNode>.getXmlExportRulesFileForScreen(screenId);
    if (!File.Exists(rulesFileForScreen))
      throw new ArgumentException("There is no file with rules of export to XML for the screen " + screenId, nameof (screenId));
    return PXCopyPasteAction<TNode>.CanExportEntitiesOnScreen(screenId) ? ExportTemplate.XmlSerializer.Read(rulesFileForScreen) : (ExportTemplate) null;
  }

  private void ProcessSiteMapChanges(PxDataSet dataSet)
  {
    if (!(ProcessSiteMapChanges("SiteMap") | ProcessSiteMapChanges("PortalMap")))
      return;
    PXSiteMap.Provider.Clear();

    bool ProcessSiteMapChanges(string tableName)
    {
      PxDataTable table = dataSet.GetTable(tableName);
      if (table == null || !((PxDataRows) table).Rows.Any<object[]>())
        return false;
      int idx = ((PxDataRows) table).IndexOfColumn("ScreenID");
      foreach (string screenId in ((PxDataRows) table).Rows.Select<object[], string>((Func<object[], string>) (row => (string) row[idx])).Where<string>((Func<string, bool>) (s => !string.IsNullOrEmpty(s))).Distinct<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
        this.ScreenInfoCacheControl.InvalidateCache(screenId);
      return true;
    }
  }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select)]
  [PXButton(ImageKey = "Copy", Tooltip = "Clipboard", SpecialType = PXSpecialButtonType.CopyPaste, CommitChanges = false, IsLockedOnToolbar = true)]
  protected override IEnumerable Handler(PXAdapter adapter)
  {
    if (DialogManager.GetAnswer(adapter.View, this.actionExceptionKey) == WebDialogResult.OK || PXGraph.ProxyIsActive)
      return adapter.Get();
    PXCopyPasteData<PXGraph> currentUserClipboard = PXCopyPasteData<PXGraph>.CurrentUserClipboard;
    PXCopyPasteData<PXGraph>.SaveClipboard(currentUserClipboard);
    if (adapter.Menu != null && adapter.Menu.StartsWith("#"))
    {
      string str = adapter.Menu.TrimStart('#');
      currentUserClipboard.LoadTemplateFromDb(Convert.ToInt32(str));
      currentUserClipboard.PasteTo(adapter.View.Graph);
    }
    if (adapter.Menu == this.bmResetToParent.Command && this.loaderChecker != null)
    {
      PxDataSet parent = adapter.View.Graph.ResetEntitiesToParent(this.loaderChecker.Relations);
      System.Action xmlPostProcessor = this.ImportXMLPostProcessor;
      if (xmlPostProcessor != null)
        xmlPostProcessor();
      this.ProcessSiteMapChanges(parent);
      Dictionary<string, object> navigationParams = parent.GetNavigationParams(adapter.View.Cache.BqlTable.Name, adapter.View.Cache.Keys.ToArray());
      if (navigationParams.Count <= 0 || adapter.View.Cache.Locate((IDictionary) navigationParams) != 1 || adapter.View.Cache.Current == null)
        return (IEnumerable) PXCopyPasteAction<TNode>.EmptyArray;
      adapter.View.Graph.Clear(PXClearOption.ClearAll);
      adapter.View.Graph.Clear(PXClearOption.ClearQueriesOnly);
      adapter.View.Graph.SelectTimeStamp();
      adapter.View.Cache.Locate((IDictionary) navigationParams);
      return (IEnumerable) new object[1]
      {
        adapter.View.Cache.Current
      };
    }
    if (adapter.Menu == this.bmExportXml.Command)
    {
      if (this.loaderChecker != null)
      {
        try
        {
          PX.SM.FileInfo currentEntityAsXml = adapter.View.Graph.GetCurrentEntityAsXml(this.loaderChecker.Relations);
          if (currentEntityAsXml != null)
            throw new PXRedirectToFileException(currentEntityAsXml, true);
        }
        catch (XMLUniqueKeysInconsistentException ex)
        {
          this.ThrowActionExceptionPopup(PXMessages.LocalizeFormatNoPrefix("The unique key of the {0} table has the following different values in the export template: {1}. Specify the same unique key value for this table.", (object) ex.Table, (object) string.Join("; ", ex.Keys)), adapter, "Export");
        }
        return (IEnumerable) PXCopyPasteAction<TNode>.EmptyArray;
      }
    }
    if (adapter.Menu == this.bmImportXml.Command)
    {
      if (DialogManager.AskExt(adapter.View.Graph, "UploadImportedXml", (string) null, (DialogManager.InitializePanel) null, false) != WebDialogResult.OK)
        return (IEnumerable) PXCopyPasteAction<TNode>.EmptyArray;
      PX.SM.FileInfo info = PXContext.SessionTyped<PXSessionStatePXData>().FileInfo["XmlUploadEntity"];
      if (info == null)
        throw new PXException("The file is not found, or you don't have enough rights to see the file.");
      bool flag = true;
      try
      {
        object[] objArray;
        if (!this.ImportEntityFromXml(adapter.View, info))
          objArray = PXCopyPasteAction<TNode>.EmptyArray;
        else
          objArray = new object[1]
          {
            adapter.View.Cache.Current
          };
        return (IEnumerable) objArray;
      }
      catch (PXDialogRequiredException ex)
      {
        flag = false;
        throw;
      }
      catch (XMLUniqueKeysInconsistentException ex)
      {
        this.ThrowActionExceptionPopup(PXMessages.LocalizeFormatNoPrefix("The unique key of the {0} table has the following different values in the export template: {1}. Specify the same unique key value for this table.", (object) ex.Table, (object) string.Join("; ", ex.Keys)), adapter, "Import");
      }
      catch (InvalidPrimaryKeyInsertException ex)
      {
        this.ThrowActionExceptionPopup(PXMessages.LocalizeFormatNoPrefix("The import will lead to the insertion of a {0} record with the {1} ID. This action is prohibited by the relation model. Check if your destination is set up correctly.", (object[]) ex.Vals), adapter, "Import");
      }
      finally
      {
        if (flag)
          PXContext.Session.Remove("XmlUploadEntity");
      }
    }
    if (adapter.Menu == this.bmPaste.Command)
      currentUserClipboard.PasteTo(adapter.View.Graph);
    object[] array = adapter.Get().Cast().ToArray<object>();
    if (adapter.Menu == this.bmCopy.Command)
      currentUserClipboard.CopyFrom(adapter.View.Graph, true, MessageButtons.OK);
    if (!(adapter.Menu == this.bmSaveTemplate.Command))
      return (IEnumerable) array;
    currentUserClipboard.CopyFrom(adapter.View.Graph, true, MessageButtons.OKCancel);
    throw new PXRedirectRequiredException((PXGraph) currentUserClipboard.CreateTemplate((string) null), true, "");
  }

  private void ThrowActionExceptionPopup(string message, PXAdapter adapter, string headerOperation)
  {
    throw new PXDialogRequiredException(adapter.View.Graph, adapter.View.Name, this.actionExceptionKey, (object) null, headerOperation + " error", message, MessageButtons.OK, MessageIcon.None);
  }

  internal bool ImportEntityFromXml(PXView view, PX.SM.FileInfo info)
  {
    PXGraph graph = view.Graph;
    PXCache cache = view.Cache;
    DataSetXmReader dataSetXmReader = new DataSetXmReader(info.BinData, PXDatabase.Provider.CreateDbServicesPoint().Schema, true);
    System.Action<DataSetXmReader> importXmlValidator = this.ImportXMLValidator;
    if (importXmlValidator != null)
      importXmlValidator(dataSetXmReader);
    DataUploader dl;
    Dictionary<string, PerTableReport> dictionary;
    try
    {
      dictionary = graph.ImportEntitiesFromXml(info.BinData, (RecordImportMode) 4, out dl);
    }
    catch (InvalidImportOperationException ex)
    {
      throw new PXException(PXLocalizer.LocalizeFormat("Impossible to import selected file. Records in the file and in the database table {0} don't match.", (object) ex.TableName));
    }
    System.Action xmlPostProcessor = this.ImportXMLPostProcessor;
    if (xmlPostProcessor != null)
      xmlPostProcessor();
    PerTableReport perTableReport1;
    PerTableReport perTableReport2;
    if (dictionary.TryGetValue("SiteMap", out perTableReport1) && perTableReport1.AnyChanges || dictionary.TryGetValue("PortalMap", out perTableReport2) && perTableReport2.AnyChanges)
      this.ProcessSiteMapChanges(dataSetXmReader.DataSet);
    Dictionary<string, object> navigationParams = dl.GetNavigationParams(cache.BqlTable.Name, cache.Keys.ToArray());
    if (navigationParams.Count > 0)
    {
      Dictionary<string, object> keys1 = EnumerableExtensions.Select<string, object>(navigationParams, (Func<string, object, object>) ((k, v) => (v is PxDataReferenceIdentity referenceIdentity1 ? (object) new int?(((PxDataReference<int>) referenceIdentity1).OldValue) : (object) new int?()) ?? v));
      if (cache.Locate((IDictionary) keys1) == 1 && cache.Current != null)
      {
        Dictionary<string, object> keys2 = EnumerableExtensions.Select<string, object>(navigationParams, (Func<string, object, object>) ((k, v) => (v is PxDataReferenceIdentity referenceIdentity2 ? (object) new int?(((PxDataReference<int>) referenceIdentity2).NewValue) : (object) new int?()) ?? v));
        graph.Clear(PXClearOption.ClearAll);
        graph.Clear(PXClearOption.ClearQueriesOnly);
        graph.SelectTimeStamp();
        cache.Locate((IDictionary) keys2);
        return true;
      }
    }
    return false;
  }
}
