// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GIActionsListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Automation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Maintenance.GI;

/// <summary>
/// Implements PXStringList attribute with values from screen's actions list.
/// </summary>
public class GIActionsListAttribute(System.Type nodeIdFieldType) : ScreenInfoListAttribute(nodeIdFieldType)
{
  private bool _enabledOnly;
  private bool _visibleOnly;

  [InjectDependencyOnTypeLevel]
  public new IWorkflowService WorkflowService { get; set; }

  /// <summary>Allowed action types.</summary>
  public PXSpecialButtonType[] AllowedActionTypes { get; set; }

  public bool EnabledOnly
  {
    get => this._enabledOnly;
    set => this._enabledOnly = value;
  }

  public bool VisibleOnly
  {
    get => this._visibleOnly;
    set => this._visibleOnly = value;
  }

  private void NormalizeArray(string[] array)
  {
    if (array != null && array.Length != 0)
      return;
    array = new string[1]{ "" };
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    PXSiteMap.ScreenInfo screenInfo = this.GetScreenInfo(sender);
    if (screenInfo != null)
    {
      IEnumerable<PXSiteMap.ScreenInfo.Action> source = ((IEnumerable<PXSiteMap.ScreenInfo.Action>) screenInfo.Actions).Where<PXSiteMap.ScreenInfo.Action>((Func<PXSiteMap.ScreenInfo.Action, bool>) (a => a.IsMass));
      if (this.AllowedActionTypes != null)
        source = source.Where<PXSiteMap.ScreenInfo.Action>((Func<PXSiteMap.ScreenInfo.Action, bool>) (a => ((IEnumerable<PXSpecialButtonType>) this.AllowedActionTypes).Contains<PXSpecialButtonType>(a.ButtonType)));
      if (this.EnabledOnly)
        source = source.Where<PXSiteMap.ScreenInfo.Action>((Func<PXSiteMap.ScreenInfo.Action, bool>) (a => a.Enabled));
      if (this.VisibleOnly)
        source = source.Where<PXSiteMap.ScreenInfo.Action>((Func<PXSiteMap.ScreenInfo.Action, bool>) (a => a.Visible));
      string screenId = this.GetScreenID(sender.Graph);
      if (screenId != null)
      {
        List<string> addedFromWorkflowActions = this.WorkflowService.GetWorkflowAddedActions(screenId).ToList<string>();
        if (addedFromWorkflowActions.Any<string>())
          source = source.Where<PXSiteMap.ScreenInfo.Action>((Func<PXSiteMap.ScreenInfo.Action, bool>) (a => !addedFromWorkflowActions.Contains<string>(a.ShortName, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)));
      }
      this._AllowedValues = source.Select<PXSiteMap.ScreenInfo.Action, string>((Func<PXSiteMap.ScreenInfo.Action, string>) (a => a.Name)).ToArray<string>();
      this._AllowedLabels = source.Select(action => new
      {
        action = action,
        splitted = action.Name.Split('@')
      }).Select(_param1 => _param1.splitted.Length < 2 ? _param1.action.DisplayName : $"{_param1.action.DisplayName} [{_param1.splitted[1]}]").ToArray<string>();
      this.NormalizeArray(this._AllowedValues);
      this.NormalizeArray(this._AllowedLabels);
    }
    base.FieldSelecting(sender, e);
  }
}
