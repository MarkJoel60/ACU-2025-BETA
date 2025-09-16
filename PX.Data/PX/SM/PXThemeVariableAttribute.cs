// Decompiled with JetBrains decompiler
// Type: PX.SM.PXThemeVariableAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.LocalizationKeyGenerators;
using PX.Data.Maintenance.SM.DAC;
using System;
using System.Linq;

#nullable enable
namespace PX.SM;

[PXInternalUseOnly]
public class PXThemeVariableAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldSelectingSubscriber,
  IPXFieldUpdatedSubscriber,
  IPXRowSelectedSubscriber
{
  protected const 
  #nullable disable
  string ViewName = "ThemeVariables";
  protected const string DependentVariablesViewName = "DependentThemeVariables";
  protected readonly string _variableName;
  protected PXView _variablesView;
  protected PXView _dependentVariablesView;
  private System.Type _persistDefaultValue;

  public PXThemeVariableAttribute(string variableName) => this._variableName = variableName;

  public System.Type PersistDefaultValue
  {
    get => this._persistDefaultValue;
    set
    {
      this._persistDefaultValue = typeof (IBqlField).IsAssignableFrom(value) ? value : throw new PXArgumentException("The type {0} must inherit the PX.Data.IBqlField interface.", value.Name);
    }
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._dependentVariablesView = this.CreateView(sender.Graph, "DependentThemeVariables", PXSelectBase<ThemeVariables, PXSelect<ThemeVariables, Where<ThemeVariables.entityNoteID, In<Required<ThemeVariables.entityNoteID>>, And<ThemeVariables.theme, Equal<Required<ThemeVariables.theme>>, And<ThemeVariables.variableName, Equal<Required<ThemeVariables.variableName>>>>>>.Config>.GetCommand());
    this._variablesView = this.CreateView(sender.Graph, "ThemeVariables", PXSelectBase<ThemeVariables, PXSelect<ThemeVariables, Where<ThemeVariables.entityNoteID, Equal<Required<ThemeVariables.entityNoteID>>, And<ThemeVariables.theme, Equal<Required<ThemeVariables.theme>>, And<ThemeVariables.variableName, Equal<Required<ThemeVariables.variableName>>>>>>.Config>.GetCommand());
    if (!(this.PersistDefaultValue != (System.Type) null))
      return;
    sender.Graph.FieldUpdated.AddHandler(sender.GetItemType(), this.PersistDefaultValue.Name, new PXFieldUpdated(this.PersistDefaultValueFieldUpdated));
  }

  public void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    Guid? noteIdIfExists = PXNoteAttribute.GetNoteIDIfExists(sender, e.Row);
    if (!noteIdIfExists.HasValue)
      return;
    string currentTheme = this.GetCurrentTheme(sender, e.Row);
    this._variablesView.SelectSingle((object) noteIdIfExists, (object) currentTheme, (object) this._variableName);
    ThemeVariables themeVariables = this._variablesView.Cache.Locate((object) new ThemeVariables()
    {
      EntityNoteID = noteIdIfExists,
      Theme = currentTheme,
      VariableName = this._variableName
    }) as ThemeVariables;
    PXEntryStatus status = this._variablesView.Cache.GetStatus((object) themeVariables);
    if (themeVariables == null || status == PXEntryStatus.Deleted || status == PXEntryStatus.InsertedDeleted)
    {
      string defaultVariableValue = this.GetDefaultVariableValue(sender, e.Row);
      e.ReturnValue = (object) defaultVariableValue;
      PXThemeLoader.CssVariable cssVariable = PXThemeLoader.GetCssVariables(currentTheme).FirstOrDefault<PXThemeLoader.CssVariable>((Func<PXThemeLoader.CssVariable, bool>) (v => string.Equals(v.VariableName, this._variableName, StringComparison.OrdinalIgnoreCase)));
      if (themeVariables != null || cssVariable == null || !cssVariable.Enabled)
        return;
      this._variablesView.Cache.SetStatus((object) new ThemeVariables()
      {
        EntityNoteID = noteIdIfExists,
        Theme = currentTheme,
        VariableName = this._variableName,
        Value = defaultVariableValue
      }, PXEntryStatus.Held);
    }
    else
      e.ReturnValue = (object) themeVariables.Value;
  }

  public void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    Guid? noteIdIfExists = PXNoteAttribute.GetNoteIDIfExists(sender, e.Row);
    if (!noteIdIfExists.HasValue)
      return;
    string currentTheme = this.GetCurrentTheme(sender, e.Row);
    string newValue = sender.GetValue(e.Row, this._FieldName) as string;
    if (!this.IsValueChanged(noteIdIfExists.Value, currentTheme, newValue))
      return;
    this._variablesView.SelectSingle((object) noteIdIfExists, (object) currentTheme, (object) this._variableName);
    ThemeVariables themeVariables1 = new ThemeVariables()
    {
      EntityNoteID = noteIdIfExists,
      Theme = currentTheme,
      VariableName = this._variableName
    };
    if (!(this._variablesView.Cache.Locate((object) themeVariables1) is ThemeVariables themeVariables2))
      themeVariables2 = this._variablesView.Cache.Insert((object) themeVariables1) as ThemeVariables;
    else if (this._variablesView.Cache.GetStatus((object) themeVariables2) == PXEntryStatus.Held)
      this._variablesView.Cache.SetStatus((object) themeVariables2, PXEntryStatus.Inserted);
    themeVariables2.Value = newValue;
    this._variablesView.Cache.Update((object) themeVariables2);
    this.UpdateDependentVariables(noteIdIfExists.Value, currentTheme);
  }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXThemeLoader.CssVariable cssVariable = PXThemeLoader.GetCssVariables(this.GetCurrentTheme(sender, e.Row)).FirstOrDefault<PXThemeLoader.CssVariable>((Func<PXThemeLoader.CssVariable, bool>) (v => string.Equals(v.VariableName, this._variableName, StringComparison.OrdinalIgnoreCase)));
    PXUIFieldAttribute.SetVisible(sender, this._FieldName, cssVariable != null && cssVariable.Enabled);
    if (cssVariable == null)
      return;
    string displayName = PXLocalizer.Localize(cssVariable.DisplayName, PXSpecialKeyGenerator.GetThemeVariableLocalizationKey((this.GetCurrentTheme(sender, e.Row), cssVariable.DisplayName)));
    PXUIFieldAttribute.SetDisplayNameLocalized(sender, this._FieldName, displayName);
  }

  public void PersistDefaultValueFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    Guid? noteIdIfExists = PXNoteAttribute.GetNoteIDIfExists(sender, e.Row);
    if (!noteIdIfExists.HasValue)
      return;
    string currentTheme = this.GetCurrentTheme(sender, e.Row);
    this._variablesView.SelectSingle((object) noteIdIfExists, (object) currentTheme, (object) this._variableName);
    if (!(this._variablesView.Cache.Locate((object) new ThemeVariables()
    {
      EntityNoteID = noteIdIfExists,
      Theme = currentTheme,
      VariableName = this._variableName
    }) is ThemeVariables themeVariables))
      return;
    bool valueOrDefault = (sender.GetValue(e.Row, this._persistDefaultValue.Name) as bool?).GetValueOrDefault();
    PXEntryStatus status = this._variablesView.Cache.GetStatus((object) themeVariables);
    if (status == PXEntryStatus.Held & valueOrDefault)
    {
      this._variablesView.Cache.SetStatus((object) themeVariables, PXEntryStatus.Inserted);
    }
    else
    {
      if (status != PXEntryStatus.Inserted || valueOrDefault)
        return;
      this._variablesView.Cache.SetStatus((object) themeVariables, PXEntryStatus.Held);
    }
  }

  private PXView CreateView(PXGraph graph, string viewName, BqlCommand command)
  {
    PXView view1;
    if (graph.Views.TryGetValue(viewName, out view1))
      return view1;
    PXView view2 = new PXView(graph, false, command);
    graph.Views.Add(viewName, view2);
    graph.Views.Caches.Add(typeof (ThemeVariables));
    return view2;
  }

  protected virtual string GetCurrentTheme(PXCache cache, object row) => PXThemeLoader.ThemeName;

  protected virtual string GetDefaultVariableValue(PXCache cache, object row)
  {
    Guid? noteIdIfExists = PXNoteAttribute.GetNoteIDIfExists(cache, row);
    return !noteIdIfExists.HasValue ? (string) null : PXThemeLoader.GetParentVariableValue(new Guid?(noteIdIfExists.Value), this._variableName);
  }

  private bool IsValueChanged(Guid noteId, string theme, string newValue)
  {
    return !string.Equals(this._variablesView.Cache.GetOriginal((object) new ThemeVariables()
    {
      EntityNoteID = new Guid?(noteId),
      Theme = theme,
      VariableName = this._variableName
    }) is ThemeVariables original ? original.Value : (string) null, newValue, StringComparison.OrdinalIgnoreCase);
  }

  private void UpdateDependentVariables(Guid noteId, string theme)
  {
    Guid[] array = PXThemeLoader.GetNotUsedDependantsNoteIds(noteId).ToArray<Guid>();
    if (array.Length == 0)
      return;
    foreach (object obj in this._dependentVariablesView.SelectMulti((object) array, (object) theme, (object) this._variableName))
      this._dependentVariablesView.Cache.SetStatus(obj, PXEntryStatus.Deleted);
  }

  public class ThemeHasVariables : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Constant<
    #nullable disable
    PXThemeVariableAttribute.ThemeHasVariables>
  {
    public ThemeHasVariables()
      : base(PXThemeLoader.GetCssVariables(PXThemeLoader.ThemeName).Count > 0)
    {
    }
  }
}
