// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFPopupProcessing`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data;

public class PXFPopupProcessing<Primary, FilterTable, Table> : 
  PXFilteredProcessing<FilterTable, Table>
  where Primary : class, IBqlTable, new()
  where FilterTable : class, IBqlTable, new()
  where Table : class, IBqlTable, new()
{
  protected const string _PopupCancelActionKey = "PopupCancel";
  protected PXAction _PopupCancel;
  protected const string _PopupOkActionKey = "PopupOk";
  protected PXAction _PopupOk;

  public PXFPopupProcessing(PXGraph graph)
    : base(graph)
  {
  }

  public PXFPopupProcessing(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  protected override void _PrepareGraph<PrimaryType>()
  {
    base._PrepareGraph<Primary>();
    this._ScheduleButton.SetVisible(false);
    PXGraph graph = this._OuterView.Cache.Graph;
    this._PopupCancel = (PXAction) new PXAction<Primary>(graph, (Delegate) new PXButtonDelegate(this.PopupCancel));
    PXProcessingBase<FilterTable>.AddAction(graph, "PopupCancel", this._PopupCancel);
    this._PopupCancel.StateSelectingEvents += (PXFieldSelecting) ((sender, e) =>
    {
      if (PXLongOperation.GetStatus(graph.UID) != PXLongRunStatus.Completed)
        return;
      e.ReturnState = (object) PXButtonState.CreateInstance(e.ReturnState, (string) null, (string) null, (string) null, (string) null, (string) null, new bool?(false), PXConfirmationType.Unspecified, (string) null, new char?(), new bool?(), new bool?(), (ButtonMenu[]) null, new bool?(), new bool?(), new bool?(), (string) null, (string) null, (PXShortCutAttribute) null, typeof (Table));
      ((PXFieldState) e.ReturnState).Enabled = false;
    });
    this._PopupOk = (PXAction) new PXAction<Primary>(graph, (Delegate) new PXButtonDelegate(this.PopupOk));
    PXProcessingBase<FilterTable>.AddAction(graph, "PopupOk", this._PopupOk);
    this._PopupOk.StateSelectingEvents += (PXFieldSelecting) ((sender, e) =>
    {
      e.ReturnState = (object) PXButtonState.CreateInstance(e.ReturnState, (string) null, (string) null, (string) null, (string) null, (string) null, new bool?(false), PXConfirmationType.Unspecified, (string) null, new char?(), new bool?(), new bool?(), (ButtonMenu[]) null, new bool?(), new bool?(), new bool?(), (string) null, (string) null, (PXShortCutAttribute) null, typeof (Table));
      PXLongRunStatus status = PXLongOperation.GetStatus(graph.UID);
      ((PXFieldState) e.ReturnState).Visible = status == PXLongRunStatus.Completed;
      ((PXFieldState) e.ReturnState).Enabled = status == PXLongRunStatus.Completed;
    });
    this._ProcessAllButton.StateSelectingEvents += (PXFieldSelecting) ((sender, e) =>
    {
      e.ReturnState = (object) PXButtonState.CreateInstance(e.ReturnState, (string) null, (string) null, (string) null, (string) null, (string) null, new bool?(false), PXConfirmationType.Unspecified, (string) null, new char?(), new bool?(), new bool?(), (ButtonMenu[]) null, new bool?(), new bool?(), new bool?(), (string) null, (string) null, (PXShortCutAttribute) null, typeof (Table));
      PXLongRunStatus status = PXLongOperation.GetStatus(graph.UID);
      ((PXFieldState) e.ReturnState).Visible = status == PXLongRunStatus.NotExists;
      ((PXFieldState) e.ReturnState).Enabled = status == PXLongRunStatus.NotExists;
    });
    this._ProcessButton.StateSelectingEvents += (PXFieldSelecting) ((sender, e) =>
    {
      e.ReturnState = (object) PXButtonState.CreateInstance(e.ReturnState, (string) null, (string) null, (string) null, (string) null, (string) null, new bool?(false), PXConfirmationType.Unspecified, (string) null, new char?(), new bool?(), new bool?(), (ButtonMenu[]) null, new bool?(), new bool?(), new bool?(), (string) null, (string) null, (PXShortCutAttribute) null, typeof (Table));
      PXLongRunStatus status = PXLongOperation.GetStatus(graph.UID);
      ((PXFieldState) e.ReturnState).Visible = status == PXLongRunStatus.NotExists;
      ((PXFieldState) e.ReturnState).Enabled = status == PXLongRunStatus.NotExists;
    });
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Cancel", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected virtual IEnumerable PopupCancel(PXAdapter adapter)
  {
    if (PXLongOperation.GetStatus(this._Graph.UID) == PXLongRunStatus.InProcess)
    {
      PXLongOperation.AsyncAbort(this._Graph.UID);
      PXLongOperation.WaitCompletion(this._Graph.UID);
    }
    PXLongOperation.ClearStatus(this._Graph.UID);
    this._OuterView.Cache.Clear();
    return adapter.Get();
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Ok", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected virtual IEnumerable PopupOk(PXAdapter adapter)
  {
    PXLongOperation.ClearStatus(this._Graph.UID);
    this._OuterView.Cache.Clear();
    return adapter.Get();
  }

  /// <summary>Sets the display name of the button that abort process.</summary>
  /// <param name="caption">The string used as the display name.</param>
  public virtual void SetPopupCancelCaption(string caption)
  {
    this._PopupCancel.SetCaption(caption);
  }

  /// <summary>Sets the tooltip for the button that abort process.</summary>
  /// <param name="tooltip">The string used as the tooltip.</param>
  public virtual void SetPopupCancelTooltip(string tooltip)
  {
    this._PopupCancel.SetTooltip(tooltip);
  }

  /// <summary>Sets the display name of the button that closes process result.</summary>
  /// <param name="caption">The string used as the display name.</param>
  public virtual void SetPopupOkCaption(string caption) => this._PopupOk.SetCaption(caption);

  /// <summary>Sets the tooltip for the button that closes process result.</summary>
  /// <param name="tooltip">The string used as the tooltip.</param>
  public virtual void SetPopupOkTooltip(string tooltip) => this._PopupOk.SetTooltip(tooltip);
}
