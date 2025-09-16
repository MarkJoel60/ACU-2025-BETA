// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.PXRefNoteSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Reflection;
using System.Web.Compilation;

#nullable disable
namespace PX.Data.EP;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
[Obfuscation(Exclude = true)]
public class PXRefNoteSelectorAttribute : PXViewExtensionAttribute
{
  private const string _VIEW_NAME = "$RefNoteView";
  private const string _SELECT_ACTION_NAME = "$Select_RefNote";
  private const string _NAVIGATE_ACTION_NAME = "$Navigate_ByRefNote";
  private const string _ATTACH_ACTION_NAME = "$Attach_RefNote";
  private readonly System.Type _primaryViewType;
  private readonly System.Type _refNoteIDField;
  private string _viewName;
  private string _navigateActionName;
  private string _selectActionName;
  private string _attachActionName;

  public PXRefNoteSelectorAttribute(System.Type primaryViewType, System.Type refNoteIDField)
  {
    if (primaryViewType == (System.Type) null)
      throw new ArgumentNullException(nameof (primaryViewType));
    if (refNoteIDField == (System.Type) null)
      throw new ArgumentNullException(nameof (refNoteIDField));
    this._primaryViewType = primaryViewType;
    this._refNoteIDField = refNoteIDField;
  }

  public override void ViewCreated(PXGraph graph, string viewName)
  {
    this._viewName = viewName + "$RefNoteView";
    if (!graph.Views.ContainsKey(this._viewName))
    {
      PXFilter<RelatedEntity> pxFilter = new PXFilter<RelatedEntity>(graph);
      graph.Views.Add(this._viewName, pxFilter.View);
      graph.RowUpdated.AddHandler(typeof (RelatedEntity), new PXRowUpdated(this.RelatedEntity_RowUpdated));
      graph.RowSelected.AddHandler(this._primaryViewType, new PXRowSelected(this.PrimaryRow_RowSelected));
      graph.RowPersisted.AddHandler(this._primaryViewType, new PXRowPersisted(this.PrimaryRow_RowPersisted));
      new PXVirtualDACAttribute().ViewCreated(graph, this._viewName);
    }
    this._selectActionName = viewName + "$Select_RefNote";
    if (!graph.Actions.Contains((object) this._selectActionName))
      PXNamedAction.AddAction(graph, this._primaryViewType, this._selectActionName, "Select Related Entity", false, (PXButtonDelegate) (adapter => this.SelectActionHandler(viewName, adapter)));
    this._navigateActionName = viewName + "$Navigate_ByRefNote";
    if (!graph.Actions.Contains((object) this._navigateActionName))
      PXNamedAction.AddAction(graph, this._primaryViewType, this._navigateActionName, "Navigate to Related Entity", false, (PXButtonDelegate) (adapter => this.NavigateActionHandler(viewName, adapter)));
    this._attachActionName = viewName + "$Attach_RefNote";
    if (graph.Actions.Contains((object) this._attachActionName))
      return;
    PXNamedAction.AddAction(graph, this._primaryViewType, this._attachActionName, "Attach to Entity", false, (PXButtonDelegate) (adapter => this.AttachActionHandler(viewName, adapter)));
  }

  private void PrimaryRow_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXActionCollection actions = sender.Graph.Actions;
    PXAction pxAction1 = actions[this._selectActionName];
    pxAction1.SetVisible(true);
    pxAction1.SetEnabled(true);
    PXAction pxAction2 = actions[this._navigateActionName];
    pxAction2.SetVisible(true);
    pxAction2.SetEnabled(true);
    PXAction pxAction3 = actions[this._attachActionName];
    pxAction3.SetVisible(true);
    pxAction3.SetEnabled(true);
  }

  private void PrimaryRow_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    PXGraph graph = sender.Graph;
    object row = e.Row;
    if (row == null || e.TranStatus != PXTranStatus.Open)
      return;
    Note note = (Note) PXSelectBase<Note, PXSelect<Note, Where<Note.noteID, Equal<Required<Note.noteID>>>>.Config>.SelectSingleBound(sender.Graph, (object[]) null, sender.GetValue(row, this._refNoteIDField.Name));
    if (note == null || note.EntityType == null)
      return;
    System.Type type = PXBuildManager.GetType(note.EntityType, false);
    if (!(type != (System.Type) null) || graph.Views.Caches.Contains(type))
      return;
    PXCache cach = graph.Caches[type];
    object entityRow = new EntityHelper(graph).GetEntityRow(type, note.NoteID);
    if (cach.GetStatus(entityRow) != PXEntryStatus.Updated)
      return;
    cach.PersistUpdated(entityRow);
  }

  private void RelatedEntity_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    RelatedEntity row = e.Row as RelatedEntity;
    RelatedEntity oldRow = e.OldRow as RelatedEntity;
    if (row != null && !sender.ObjectsEqual<RelatedEntity.entityCD>((object) row, (object) oldRow) && sender.GetStateExt<RelatedEntity.refNoteID>((object) row) is PXEntityState stateExt && !string.IsNullOrEmpty(stateExt.ViewName) && row.EntityCD != null)
    {
      int startRow = 0;
      int totalRows = 0;
      foreach (object data in sender.Graph.ExecuteSelect(stateExt.ViewName, (object[]) null, new object[1]
      {
        (object) row.EntityCD
      }, new string[1]{ stateExt.TextField }, new bool[1], (PXFilterRow[]) null, ref startRow, 1, ref totalRows))
      {
        object obj = sender.Graph.GetValue(stateExt.ViewName, data, stateExt.ValueField);
        sender.SetValue<RelatedEntity.refNoteID>((object) row, obj);
      }
    }
    if (row == null || oldRow == null)
      return;
    if (row.Type != oldRow.Type)
    {
      Guid? refNoteId1 = row.RefNoteID;
      Guid? refNoteId2 = oldRow.RefNoteID;
      if ((refNoteId1.HasValue == refNoteId2.HasValue ? (refNoteId1.HasValue ? (refNoteId1.GetValueOrDefault() == refNoteId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        row.RefNoteID = new Guid?();
    }
    if (!row.RefNoteID.HasValue || sender.ObjectsEqual<RelatedEntity.refNoteID>((object) row, (object) oldRow))
      return;
    PXGraph graph = sender.Graph;
    if (PXSelectBase<Note, PXSelect<Note, Where<Note.noteID, Equal<Required<Note.noteID>>>>.Config>.SelectSingleBound(sender.Graph, (object[]) null, (object) row.RefNoteID) == null)
      return;
    PXRefNoteSelectorAttribute.EnsureNotePersistence(graph, row.Type, row.RefNoteID);
  }

  public static void SetEnabled(PXView view, bool enabled)
  {
    PXGraph graph = view.Graph;
    string str;
    if (!graph.ViewNames.TryGetValue(view, out str))
      return;
    string name = str + "$Select_RefNote";
    graph.Actions[name]?.SetEnabled(enabled);
  }

  private IEnumerable SelectActionHandler(string viewName, PXAdapter adapter)
  {
    PXGraph graph = adapter.View.Graph;
    if (graph.Views[this._viewName].Cache.Current is RelatedEntity current)
    {
      PXCache cache = graph.Views[viewName].Cache;
      string noteIDFieldName = cache.GetField(this._refNoteIDField);
      Guid? o = cache.Current.With<object, Guid?>((Func<object, Guid?>) (row => cache.GetValue(row, noteIDFieldName) as Guid?));
      object obj = o.With<Guid?, object>((Func<Guid?, object>) (id => new EntityHelper(graph).GetEntityRow(new Guid?(id.Value), true)));
      if (obj != null)
      {
        current.Type = MainTools.GetLongName(obj.GetType());
        current.RefNoteID = o;
      }
      else
      {
        current.Type = (string) null;
        current.RefNoteID = new Guid?();
      }
    }
    return adapter.Get();
  }

  private IEnumerable AttachActionHandler(string viewName, PXAdapter adapter)
  {
    PXGraph graph = adapter.View.Graph;
    RelatedEntity current = graph.Views[this._viewName].Cache.Current as RelatedEntity;
    PXCache cache = graph.Views[viewName].Cache;
    if (current != null)
    {
      PXTrace.WithSourceLocation(nameof (AttachActionHandler), "C:\\build\\code_repo\\NetTools\\PX.Data\\Maintenance\\EP\\Descriptor\\Indexer.cs", 269).Verbose<Guid?, string>("Filter is not null. Filter.RefNoteId = {RefNoteId}; Filter.Type={Type}", current.RefNoteID, current.Type);
      string field = cache.GetField(this._refNoteIDField);
      if (current.RefNoteID.HasValue)
      {
        if (PXSelectBase<Note, PXSelect<Note, Where<Note.noteID, Equal<Required<Note.noteID>>>>.Config>.SelectSingleBound(graph, (object[]) null, (object) current.RefNoteID) != null)
          PXRefNoteSelectorAttribute.EnsureNotePersistence(graph, current.Type, current.RefNoteID);
      }
      cache.SetValueExt(cache.Current, field, (object) current.RefNoteID);
      cache.Update(cache.Current);
    }
    graph.Views[this._viewName].Cache.Update((object) new RelatedEntity());
    if (graph.IsContractBasedAPI)
      graph.Actions.PressSave();
    yield return cache.Current;
  }

  private IEnumerable NavigateActionHandler(string viewName, PXAdapter adapter)
  {
    PXGraph graph = adapter.View.Graph;
    if (graph.Views[this._viewName].Cache.Current is RelatedEntity)
    {
      PXCache cache = graph.Views[viewName].Cache;
      string noteIDFieldName = cache.GetField(this._refNoteIDField);
      Guid? nullable = cache.Current.With<object, Guid?>((Func<object, Guid?>) (row => cache.GetValue(row, noteIDFieldName) as Guid?));
      if (nullable.HasValue)
        new EntityHelper(graph).NavigateToRow(new Guid?(nullable.Value), PXRedirectHelper.WindowMode.NewWindow);
    }
    return adapter.Get();
  }

  public static void EnsureNotePersistence(PXGraph graph, string type, Guid? noteid)
  {
    if (type == null || !noteid.HasValue)
      return;
    System.Type type1 = PXBuildManager.GetType(type, false);
    if (!(type1 != (System.Type) null))
      return;
    PXCache cach = graph.Caches[type1];
    object entityRow = new EntityHelper(graph).GetEntityRow(type1, noteid);
    object data = entityRow;
    string noteField = EntityHelper.GetNoteField(type1);
    PXNoteAttribute.GetNoteID(cach, data, noteField);
    graph.Caches[typeof (Note)].ClearQueryCache();
    graph.EnsureRowPersistence(entityRow);
  }
}
