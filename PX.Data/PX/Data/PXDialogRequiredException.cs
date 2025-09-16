// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDialogRequiredException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

public sealed class PXDialogRequiredException : PXBaseUnhandledRedirectException
{
  private readonly MessageButtons _buttons;
  private readonly MessageIcon _icon;
  private readonly string _header;
  private readonly object _row;
  private readonly string _viewName;
  private readonly string _key;
  private readonly PXGraph _graph;
  private readonly IReadOnlyDictionary<WebDialogResult, string> _customButtonNames;

  public PXDialogRequiredException(
    string viewName,
    object row,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon)
    : this((PXGraph) null, viewName, (string) null, row, header, message, buttons, icon, false)
  {
  }

  public PXDialogRequiredException(
    PXGraph graph,
    string viewName,
    string key,
    object row,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon)
    : this(graph, viewName, key, row, header, message, buttons, icon, false)
  {
  }

  public PXDialogRequiredException(
    PXGraph graph,
    string viewName,
    string key,
    object row,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon,
    bool repaintControls)
    : this(graph, viewName, key, row, header, message, buttons, (IReadOnlyDictionary<WebDialogResult, string>) null, icon, repaintControls)
  {
  }

  public PXDialogRequiredException(
    PXGraph graph,
    string viewName,
    string key,
    object row,
    string header,
    string message,
    MessageButtons buttons,
    IReadOnlyDictionary<WebDialogResult, string> customButtonNames,
    MessageIcon icon,
    bool repaintControls)
    : base(message)
  {
    this._graph = graph;
    this._viewName = viewName;
    this._key = key;
    this._row = row;
    this._buttons = buttons;
    this._customButtonNames = customButtonNames ?? (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>();
    this._icon = icon;
    this._header = header;
    this.RepaintControls = repaintControls;
  }

  public override string Message => PXMessages.LocalizeNoPrefix(this._Message);

  public string Key => this._key;

  public PXGraph Graph => this._graph;

  public string ViewName => this._viewName;

  public object Row => this._row;

  public MessageButtons Buttons => this._buttons;

  public IReadOnlyDictionary<WebDialogResult, string> CustomButtonNames
  {
    get
    {
      Dictionary<WebDialogResult, string> customButtonNames = new Dictionary<WebDialogResult, string>();
      foreach (KeyValuePair<WebDialogResult, string> customButtonName in (IEnumerable<KeyValuePair<WebDialogResult, string>>) this._customButtonNames)
        customButtonNames[customButtonName.Key] = PXLocalizer.Localize(customButtonName.Value);
      return (IReadOnlyDictionary<WebDialogResult, string>) customButtonNames;
    }
  }

  public MessageIcon Icon => this._icon;

  public string Header => PXMessages.LocalizeNoPrefix(this._header);

  public PXDialogRequiredException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.HResult = -2147024809;
    ReflectionSerializer.RestoreObjectProps<PXDialogRequiredException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXDialogRequiredException>(this, info);
    base.GetObjectData(info, context);
  }

  public List<string> ForceCommit { get; set; }
}
