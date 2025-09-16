// Decompiled with JetBrains decompiler
// Type: PX.CS.CSAttributeMaint2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Caching;
using PX.Common;
using PX.Common.Extensions;
using PX.Data;
using PX.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.CS;

public class CSAttributeMaint2 : PXGraph<CSAttributeMaint2>
{
  public const string SessionKey = "AssignAttribute";
  public PXFilter<AttribParams> ScreenSettings;
  public PXSelectJoin<CSAttribute, InnerJoin<CSScreenAttribute, On<CSAttribute.attributeID, Equal<CSScreenAttribute.attributeID>>>, Where<CSScreenAttribute.screenID, Equal<Current<AttribParams.screenID>>>> Attributes;
  public PXSave<AttribParams> Save;
  public PXCancel<AttribParams> Cancel;
  public PXAction<AttribParams> CancelClose;
  public PXSelect<CSScreenAttribute, Where<CSScreenAttribute.screenID, Equal<Current<AttribParams.screenID>>>> AttribList;
  public PXFilter<CSNewAttribute> NewAttrib;
  public PXAction<AttribParams> AddAttrib;
  private bool _alreadyCleared;
  public PXAction<AttribParams> Del;
  public PXAction<AttribParams> Edit;
  public PXAction<AttribParams> ManageAttribs;
  private const string AttributeView = "AttribProxy$";
  private Dictionary<string, PXGraph> graphCache = new Dictionary<string, PXGraph>();

  protected override PXCacheCollection CreateCacheCollection()
  {
    return (PXCacheCollection) new PXCacheUniqueForTypeCollection((PXGraph) this);
  }

  [InjectDependency]
  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }

  [InjectDependency]
  protected ICacheControl<PageCache> PageCacheControl { get; set; }

  [PXCancelCloseButton(Tooltip = "Back", ClosePopup = true)]
  [PXUIField]
  public virtual IEnumerable cancelClose(PXAdapter adapter)
  {
    CSAttributeMaint2 csAttributeMaint2 = this;
    PXGraph.ClearSessionQueryCache();
    foreach (object obj in adapter.Get())
      yield return obj;
    csAttributeMaint2.Actions.PressCancel((PXAction) csAttributeMaint2.CancelClose, adapter);
  }

  [PXUIField(DisplayName = "Add User-Defined Field")]
  [PXButton]
  public virtual IEnumerable addAttrib(PXAdapter adapter)
  {
    if (this.NewAttrib.View.Answer == WebDialogResult.None)
    {
      Tuple<PXFieldState, short, short, string>[] attributeFields = KeyValueHelper.GetAttributeFields(this.ScreenSettings.Current.ScreenID);
      if (this.NewAttrib.Current == null)
        this.NewAttrib.Current = new CSNewAttribute();
      CSNewAttribute current = this.NewAttrib.Current;
      int? column = current.Column;
      if (!column.HasValue)
        current.Column = new int?(1);
      int num1 = 0;
      foreach (Tuple<PXFieldState, short, short, string> tuple in attributeFields)
      {
        int num2 = (int) tuple.Item2;
        column = current.Column;
        int num3 = column.Value;
        if (num2 == num3 && num1 < (int) tuple.Item3)
          num1 = (int) tuple.Item3;
      }
      current.Row = new int?(num1 + 1);
      current.OldAttributeID = current.AttributeID;
    }
    if (this.NewAttrib.AskExt() == WebDialogResult.OK)
    {
      this.CreateSettingsForAttribute();
      this.Persist();
    }
    return adapter.Get();
  }

  private void CreateSettingsForAttribute()
  {
    PXCache cache = this.AttribList.Cache;
    CSScreenAttribute newAttribute = this.CreateNewScreenAttribute();
    newAttribute.Row = this.AssertAttributeParams(newAttribute.Row, newAttribute.Column, (string) null, newAttribute.AttributeID);
    System.Type cacheType = GraphHelper.GetPrimaryCache(PXSiteMap.Provider.FindSiteMapNodeByScreenID(newAttribute.ScreenID).GraphType)?.CacheType;
    KeyValueHelper.Definition def = KeyValueHelper.Def;
    KeyValueHelper.TableAttribute tableAttribute = def != null ? ((IEnumerable<KeyValueHelper.TableAttribute>) def.GetAttributes(cacheType)).FirstOrDefault<KeyValueHelper.TableAttribute>((Func<KeyValueHelper.TableAttribute, bool>) (attribute => attribute.AttributeID == newAttribute.AttributeID)) : (KeyValueHelper.TableAttribute) null;
    if (tableAttribute != null)
    {
      Dictionary<string, string> defaulValues = tableAttribute.DefaulValues;
      Dictionary<string, string>.KeyCollection keys = defaulValues.Keys;
      bool flag = keys.Any<string>((Func<string, bool>) (tv => string.IsNullOrEmpty(tv)));
      foreach (string key in keys)
      {
        CSScreenAttribute copy = (CSScreenAttribute) cache.CreateCopy((object) newAttribute);
        copy.TypeValue = key;
        copy.DefaultValue = defaulValues[key];
        this.AttribList.Insert(copy);
      }
      if (flag)
        return;
      this.AttribList.Insert(newAttribute);
    }
    else
      this.AttribList.Insert(newAttribute);
  }

  private CSScreenAttribute CreateNewScreenAttribute()
  {
    CSScreenAttribute newScreenAttribute = new CSScreenAttribute();
    CSNewAttribute current = this.NewAttrib.Current;
    if (!current.Column.HasValue || !current.Row.HasValue || string.IsNullOrEmpty(current.AttributeID))
      throw new PXArgumentException("params missing");
    newScreenAttribute.Column = new short?((short) this.NewAttrib.Current.Column.Value);
    newScreenAttribute.Row = new short?((short) this.NewAttrib.Current.Row.Value);
    newScreenAttribute.AttributeID = current.AttributeID;
    newScreenAttribute.ScreenID = this.GetAttributeOwnerScreenID();
    return newScreenAttribute;
  }

  private string GetAttributeOwnerScreenID()
  {
    return (this.ScreenSettings.Cache.Current as AttribParams).ScreenID.Replace(".", string.Empty);
  }

  private void ClearAttributeOwnerGraph()
  {
    if (this._alreadyCleared)
      return;
    this._alreadyCleared = true;
    PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(this.GetAttributeOwnerScreenID());
    if (mapNodeByScreenId == null)
      return;
    PXGraph graph = this.GetGraph(mapNodeByScreenId.ScreenID);
    if (graph == null)
      return;
    graph.Load();
    graph.Clear();
    graph.QueryCache.Clear();
    graph.Unload();
  }

  private short? AssertAttributeParams(short? row, short? column, string oldId, string newId)
  {
    foreach (PXResult pxResult in this.Attributes.Select())
    {
      CSScreenAttribute csScreenAttribute = pxResult[1] as CSScreenAttribute;
      if (!(oldId == csScreenAttribute.AttributeID))
      {
        if (newId == csScreenAttribute.AttributeID)
          throw new PXArgumentException(PXLocalizer.LocalizeFormat("The {0} attribute is already in use.", (object) newId));
        short? nullable1 = csScreenAttribute.Row;
        int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
        nullable1 = row;
        int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
        if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
        {
          nullable1 = csScreenAttribute.Column;
          nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
          nullable1 = column;
          nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
          if (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue)
          {
            nullable1 = row;
            nullable1 = this.AssertAttributeParams(row = nullable1.HasValue ? new short?((short) ((int) nullable1.GetValueOrDefault() + 1)) : new short?(), column, oldId, newId);
            return nullable1;
          }
        }
      }
    }
    return row;
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable del(PXAdapter adapter)
  {
    string commandArguments = adapter.CommandArguments;
    PXResultset<CSScreenAttribute> pxResultset = this.AttribList.SearchAll<Asc<CSScreenAttribute.attributeID>>((object[]) new string[1]
    {
      this.NewAttrib.Current.AttributeID = KeyValueHelper.GetAttributeNameWithoutPrefix(StringExtensions.LastSegment(commandArguments, '_'))
    });
    this.AttribList.Cache.Clear();
    foreach (PXResult<CSScreenAttribute> pxResult in pxResultset)
      this.AttribList.Delete((CSScreenAttribute) pxResult);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      base.Persist();
      this.DeleteUdfValues(commandArguments);
      transactionScope.Complete();
    }
    this.ClearAttributeOwnerGraph();
    PXDatabase.SelectTimeStamp();
    return adapter.Get();
  }

  protected void DeleteUdfValues(string attributeName)
  {
    string tableContainingUdf = this.GetNameOfTableContainingUDF();
    if (string.IsNullOrEmpty(tableContainingUdf))
      return;
    (PXDatabase.Provider as PXDatabaseProviderBase).DeleteKeyValueStored(tableContainingUdf, attributeName);
  }

  protected virtual string GetNameOfTableContainingUDF()
  {
    PXView viewFromSession = this.GetViewFromSession();
    return viewFromSession == null ? (string) null : this.GetNameOfTableContainingUDF(viewFromSession.Cache);
  }

  protected string GetNameOfTableContainingUDF(PXCache cache)
  {
    if (string.IsNullOrEmpty(cache._NoteIDName))
      return (string) null;
    return EnumerableExtensions.FirstOrAny<PXNoteAttribute>(cache.GetAttributesReadonly((string) null).OfType<PXNoteAttribute>(), (Func<PXNoteAttribute, bool>) (noteAttribute => noteAttribute.GetType() == typeof (PXNoteAttribute)), (PXNoteAttribute) null)?.BqlTable.Name;
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable edit(PXAdapter adapter)
  {
    if (this.NewAttrib.View.Answer == WebDialogResult.None)
    {
      string attribName = StringExtensions.LastSegment(adapter.CommandArguments, '_');
      this.NewAttrib.Current.AttributeID = KeyValueHelper.GetAttributeNameWithoutPrefix(attribName);
      Tuple<PXFieldState, short, short, string> tuple = ((IEnumerable<Tuple<PXFieldState, short, short, string>>) KeyValueHelper.GetAttributeFields(this.ScreenSettings.Current.ScreenID)).FirstOrDefault<Tuple<PXFieldState, short, short, string>>((Func<Tuple<PXFieldState, short, short, string>, bool>) (_ => _.Item1.Name == attribName));
      if (tuple != null)
      {
        if (this.NewAttrib.Current == null)
          this.NewAttrib.Current = new CSNewAttribute();
        CSNewAttribute current = this.NewAttrib.Current;
        current.AttributeID = KeyValueHelper.GetAttributeNameWithoutPrefix(tuple.Item1.Name);
        current.Row = new int?((int) tuple.Item3);
        current.Column = new int?((int) tuple.Item2);
        current.OldAttributeID = current.AttributeID;
      }
    }
    if (this.NewAttrib.AskExt(true) == WebDialogResult.OK)
    {
      string nameWithoutPrefix = KeyValueHelper.GetAttributeNameWithoutPrefix(this.NewAttrib.Current.OldAttributeID);
      bool flag = false;
      this.AttribList.Cache.Clear();
      foreach (PXResult<CSScreenAttribute> pxResult in this.AttribList.Select())
      {
        CSScreenAttribute csScreenAttribute1 = (CSScreenAttribute) pxResult;
        if (csScreenAttribute1.AttributeID == nameWithoutPrefix)
        {
          CSScreenAttribute csScreenAttribute2 = csScreenAttribute1;
          int? nullable1 = this.NewAttrib.Current.Column;
          short? nullable2 = nullable1.HasValue ? new short?((short) nullable1.GetValueOrDefault()) : new short?();
          csScreenAttribute2.Column = nullable2;
          CSScreenAttribute csScreenAttribute3 = csScreenAttribute1;
          nullable1 = this.NewAttrib.Current.Row;
          short? nullable3 = nullable1.HasValue ? new short?((short) nullable1.GetValueOrDefault()) : new short?();
          csScreenAttribute3.Row = nullable3;
          flag = true;
          if (this.NewAttrib.Current.OldAttributeID == this.NewAttrib.Current.AttributeID)
          {
            csScreenAttribute1.Row = this.AssertAttributeParams(csScreenAttribute1.Row, csScreenAttribute1.Column, this.NewAttrib.Current.OldAttributeID, (string) null);
            this.AttribList.Update(csScreenAttribute1);
          }
          else
          {
            csScreenAttribute1.Row = this.AssertAttributeParams(csScreenAttribute1.Row, csScreenAttribute1.Column, this.NewAttrib.Current.OldAttributeID, this.NewAttrib.Current.OldAttributeID);
            this.AttribList.Delete(csScreenAttribute1);
            this.CreateSettingsForAttribute();
          }
        }
      }
      if (flag)
        this.Persist();
    }
    return adapter.Get();
  }

  public override void Persist()
  {
    base.Persist();
    PXDatabase.SelectTimeStamp();
    this.ClearAttributeOwnerGraph();
  }

  [PXUIField(DisplayName = "Manage Attributes")]
  [PXButton]
  public virtual IEnumerable manageAttribs(PXAdapter adapter)
  {
    throw new PXRedirectByScreenIDException("CS205000", PXBaseRedirectException.WindowMode.NewWindow, PXRedirectByScreenIDException.FramesetBehavior.Supress);
  }

  public static string CreateViewName(string screenId, string viewName)
  {
    return $"AttribProxy${screenId}${viewName}";
  }

  private PXGraph GetGraph(string screenId)
  {
    PXGraph graph = (PXGraph) null;
    if (!this.graphCache.TryGetValue(screenId, out graph))
    {
      graph = PXGraph.CreateInstance(GraphHelper.GetType(PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenId).GraphType));
      this.graphCache.Add(screenId, graph);
    }
    return graph;
  }

  private PXView GetView(string viewName)
  {
    if (viewName.StartsWith("AttribProxy$"))
    {
      string[] strArray = viewName.Split('$');
      if (strArray.Length >= 3)
      {
        string screenId = strArray[1];
        string key = strArray[2];
        PXGraph graph = this.GetGraph(screenId);
        if (graph != null)
          return graph.Views[key];
      }
    }
    return (PXView) null;
  }

  private PXView GetViewFromSession()
  {
    CSAttributeMaint2.ControlParams parametersFromSession = this.GetParametersFromSession();
    if (parametersFromSession == null)
      return (PXView) null;
    PXGraph graph = this.GetGraph(parametersFromSession.ScreenId);
    if (graph == null)
      return (PXView) null;
    string key = parametersFromSession is CSAttributeMaint2.BoundParams boundParams ? Str.NullIfWhitespace(boundParams.ViewName) ?? graph.PrimaryView : graph.PrimaryView;
    return graph.Views[key];
  }

  [PXInternalUseOnly]
  public CSAttributeMaint2.ControlParams GetParametersFromSession()
  {
    return PXContext.Session.PageInfo["AssignAttribute"] as CSAttributeMaint2.ControlParams;
  }

  public override object GetValueExt(string viewName, object data, string fieldName)
  {
    PXView view = this.GetView(viewName);
    if (view == null)
      return base.GetValueExt(viewName, data, fieldName);
    return view.Cache?.GetValueExt(data, fieldName);
  }

  public override IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    PXView view = this.GetView(viewName);
    return view != null ? (IEnumerable) view.Select((object[]) null, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows) : base.ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  public override object GetStateExt(string viewName, object data, string fieldName)
  {
    PXView view = this.GetView(viewName);
    if (view == null)
      return base.GetStateExt(viewName, data, fieldName);
    return view.Cache?.GetStateExt(data, fieldName);
  }

  protected virtual void _(Events.RowPersisted<CSScreenAttribute> e)
  {
    if (e.TranStatus != PXTranStatus.Completed)
      return;
    this.ScreenInfoCacheControl.InvalidateCache(e.Row.ScreenID);
    this.PageCacheControl.InvalidateCache();
  }

  public class ControlParams
  {
    public string ScreenId { get; set; }
  }

  public class BoundParams : CSAttributeMaint2.ControlParams
  {
    public string ViewName { get; set; }

    public string UDFTypeField { get; set; }
  }

  public class ComboBoxParams : CSAttributeMaint2.BoundParams
  {
  }

  public class SelectorParams : CSAttributeMaint2.BoundParams
  {
  }
}
