// Decompiled with JetBrains decompiler
// Type: PX.Data.GIDataScreen
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.GenericInquiry;
using PX.Data.Localization;
using PX.Data.Maintenance.GI;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
internal class GIDataScreen(string screenID, PXGraph graph) : DataScreenBase(screenID, graph)
{
  private IEnumerable<InqField> _fields;
  private Dictionary<string, int> _titles;
  private IEnumerable<InqField> _parameters;

  public GIDataScreen(string screenID)
    : this(screenID, (PXGraph) null)
  {
  }

  protected override PXGraph InstantiateGraph()
  {
    using (new PXPreserveScope())
    {
      using (new PXScreenIDScope(this.ScreenID))
        return (PXGraph) PXGenericInqGrph.CreateInstance(this.ScreenID);
    }
  }

  public override string DefaultAction => "EditDetail";

  public override string ViewName => ((PXGenericInqGrph) this.DataGraph).Results.Name;

  public override string ParametersViewName => ((PXGenericInqGrph) this.DataGraph).Filter.Name;

  public override IEnumerable<InqField> GetFields()
  {
    PXGenericInqGrph graph = (PXGenericInqGrph) this.DataGraph;
    if (this._fields == null)
    {
      this._fields = graph.Results.Cache.Fields.Select(f => new
      {
        f = f,
        state = graph.Results.Cache.GetStateExt((object) null, f) as PXFieldState
      }).Where(_param1 => _param1.state != null && !GenericResult.IsAuxiliaryField(_param1.f) && !typeof (GenericResult.row).Name.Equals(_param1.f, StringComparison.OrdinalIgnoreCase) && !graph.NoteFields.Contains(_param1.f) && graph.Columns.Contains(_param1.f)).Select(_param1 => this.GetInquiryField(_param1.state, _param1.f, graph));
      this._fields = (IEnumerable<InqField>) this._fields.ToArray<InqField>();
    }
    return this._fields;
  }

  private InqField GetInquiryField(PXFieldState state, string fieldName, PXGenericInqGrph graph)
  {
    string navigateToActionName = PXGenericInqGrph.GetNavigateToActionName(fieldName);
    InqField field = new InqField()
    {
      Name = fieldName,
      DisplayName = state.DisplayName,
      FieldType = state.DataType,
      Visible = new bool?(state.Visible),
      Enabled = new bool?(state.Enabled),
      LinkCommand = graph.Actions[navigateToActionName] != null ? navigateToActionName : (string) null,
      FieldName = state.Name
    };
    if (PXDBLocalizableStringAttribute.HasMultipleLocales)
      this.SetDisplayNameTranslations(graph, field);
    return field;
  }

  private void SetDisplayNameTranslations(PXGenericInqGrph graph, InqField field)
  {
    (string LocaleName, string Language)[] withUniqueLanguage = PXLocalesHelper.GetLocalesWithUniqueLanguage(graph.CurrentUserInformationProvider.GetUserName());
    if (withUniqueLanguage == null || withUniqueLanguage.Length == 0)
      return;
    GenericResultCache cache = graph.Results.Cache;
    string currentLanguage = PXLocalesHelper.GetCurrentLanguage();
    string defaultLanguage = PXLocalesHelper.GetDefaultLanguage();
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    foreach ((string LocaleName, string Language) tuple in withUniqueLanguage)
    {
      using (new PXLocaleScope(tuple.LocaleName))
      {
        PXFieldState stateExt = cache.GetStateExt((object) null, field.Name) as PXFieldState;
        dictionary[tuple.Language] = PXLocalizer.Localize(stateExt?.DisplayName);
      }
    }
    string defaultDisplayName = dictionary[defaultLanguage];
    if (!defaultLanguage.Equals(currentLanguage, StringComparison.OrdinalIgnoreCase) && defaultDisplayName.Equals(dictionary[currentLanguage], StringComparison.InvariantCultureIgnoreCase))
    {
      dictionary[currentLanguage] = PXLocalizer.Localize(defaultDisplayName);
      if (GenericInquiryHelpers.IsDescriptionField(field.FieldName) && dictionary.Values.All<string>((Func<string, bool>) (v => string.Equals(v, defaultDisplayName))))
      {
        foreach ((string _, string str) in withUniqueLanguage)
        {
          if (!str.Equals(currentLanguage, StringComparison.OrdinalIgnoreCase))
            dictionary[str] = (string) null;
        }
      }
    }
    field.DisplayNameTranslations = dictionary;
    field.DisplayName = dictionary[currentLanguage];
  }

  public virtual bool HasDuplicatedTitles(string title)
  {
    PXGenericInqGrph dataGraph = (PXGenericInqGrph) this.DataGraph;
    if (this._titles == null)
    {
      this._titles = new Dictionary<string, int>();
      foreach (string field in (List<string>) dataGraph.Results.Cache.Fields)
      {
        if (dataGraph.Results.Cache.GetStateExt((object) null, field) is PXFieldState stateExt && stateExt.DisplayName != null)
        {
          int num;
          this._titles[stateExt.DisplayName] = this._titles.TryGetValue(stateExt.DisplayName, out num) ? num + 1 : 1;
        }
      }
    }
    int num1;
    return title != null && this._titles.TryGetValue(title, out num1) && num1 > 1;
  }

  public override IEnumerable<InqField> GetParameters()
  {
    PXGenericInqGrph graph = (PXGenericInqGrph) this.DataGraph;
    if (this._parameters == null)
    {
      this._parameters = graph.Filter.Cache.Fields.Select(f => new
      {
        f = f,
        state = graph.Filter.Cache.GetStateExt((object) null, f) as PXFieldState
      }).Where(_param1 => _param1.state != null).Select(_param1 => new InqField()
      {
        Name = _param1.f,
        DisplayName = _param1.state.DisplayName,
        FieldType = _param1.state.DataType,
        Visible = new bool?(_param1.state.Visible),
        Enabled = new bool?(_param1.state.Enabled),
        FieldName = _param1.state.Name
      });
      this._parameters = (IEnumerable<InqField>) this._parameters.ToArray<InqField>();
    }
    return this._parameters;
  }

  public override string GetStyle(string fieldName, object row)
  {
    string style = (string) null;
    if (row is GenericResult row1)
    {
      PXGenericInqGrph dataGraph = (PXGenericInqGrph) this.DataGraph;
      if (string.IsNullOrEmpty(fieldName))
      {
        if (!string.IsNullOrEmpty(dataGraph.Design?.RowStyleFormula))
          style = GIFormulaParser.Evaluate(dataGraph, row1, dataGraph.Design.RowStyleFormula, true, false) as string;
      }
      else
      {
        GIResult giResult = dataGraph.ResultColumns.FirstOrDefault<GIResult>((Func<GIResult, bool>) (c => string.Equals(c.FieldName, fieldName, StringComparison.OrdinalIgnoreCase)));
        if (!string.IsNullOrEmpty(giResult?.StyleFormula))
          style = GIFormulaParser.Evaluate(dataGraph, row1, giResult.StyleFormula, true, false) as string;
      }
    }
    return style;
  }
}
