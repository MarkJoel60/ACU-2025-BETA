// Decompiled with JetBrains decompiler
// Type: PX.Data.PXButtonState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.WorkflowAPI;
using System.Web.UI.WebControls;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXButtonState : PXFieldState
{
  protected string _ImageSet;
  protected string _ImageKey;
  protected string _ImageUrl;
  protected string _DisabledImageUrl;
  protected string _HoverImageUrl;
  protected string _Tooltip;
  protected bool _Pressed;
  protected ButtonMenu[] _Menus;
  protected PXConfirmationType _ConfirmationType = PXConfirmationType.IfDirty;
  protected string _ConfirmationMessage;
  protected char _ShortcutChar;
  protected bool _ShortcutCtrl;
  protected bool _ShortcutShift;
  protected bool _CommitChanges;
  protected bool _ClosePopup;
  protected bool _PopupVisible;
  protected PXSpecialButtonType _SpecialType;
  protected PXShortCutAttribute _ExtShortcut;
  protected System.Type _ItemType;
  protected bool _ViewRights;
  protected PXSpecialButtonType _OnClosingPopup;
  protected bool _DynamicVisibility;
  protected bool _DynamicText;

  protected PXButtonState(object value)
    : base(value)
  {
    this.Callback = new PXButtonState.CallbackData();
    if (!(value is PXButtonState pxButtonState))
      return;
    this._ImageSet = pxButtonState._ImageSet;
    this._ImageKey = pxButtonState._ImageKey;
    this._ImageUrl = pxButtonState._ImageUrl;
    this._DisabledImageUrl = pxButtonState._DisabledImageUrl;
    this._HoverImageUrl = pxButtonState._HoverImageUrl;
    this._Tooltip = pxButtonState._Tooltip;
    this._Pressed = pxButtonState._Pressed;
    this._ConfirmationType = pxButtonState._ConfirmationType;
    this._ConfirmationMessage = pxButtonState._ConfirmationMessage;
    this._ShortcutChar = pxButtonState._ShortcutChar;
    this._ShortcutCtrl = pxButtonState._ShortcutCtrl;
    this._ShortcutShift = pxButtonState._ShortcutShift;
    this._CommitChanges = pxButtonState._CommitChanges;
    this._Menus = pxButtonState._Menus;
    this._ExtShortcut = pxButtonState._ExtShortcut;
    this._ItemType = pxButtonState._ItemType;
    this._ViewRights = pxButtonState._ViewRights;
    this._OnClosingPopup = pxButtonState._OnClosingPopup;
  }

  public virtual string ImageSet => this._ImageSet;

  public virtual string ImageKey => this._ImageKey;

  public virtual string ImageUrl => this._ImageUrl;

  public virtual string DisabledImageUrl => this._DisabledImageUrl;

  public virtual string HoverImageUrl => this._HoverImageUrl;

  public virtual string Tooltip
  {
    get => this._Tooltip;
    set => this._Tooltip = value;
  }

  public virtual bool Pressed => this._Pressed;

  public virtual PXConfirmationType ConfirmationType => this._ConfirmationType;

  public virtual string ConfirmationMessage
  {
    get => this._ConfirmationMessage;
    set => this._ConfirmationMessage = value;
  }

  public virtual char ShortcutChar => this._ShortcutChar;

  public virtual bool ShortcutCtrl => this._ShortcutCtrl;

  public virtual bool ShortcutShift => this._ShortcutShift;

  public virtual PXSpecialButtonType SpecialType
  {
    get => this._SpecialType;
    set => this._SpecialType = value;
  }

  public virtual PXShortCutAttribute ExtShortcut => this._ExtShortcut;

  public virtual bool CommitChanges => this._CommitChanges;

  public virtual bool ClosePopup => this._ClosePopup;

  public virtual bool PopupVisible
  {
    get => this._PopupVisible;
    set => this._PopupVisible = value;
  }

  public ButtonMenu[] Menus
  {
    get => this._Menus;
    set => this._Menus = value;
  }

  public System.Type ItemType => this._ItemType;

  public bool ViewRights
  {
    get => this._ViewRights;
    set => this._ViewRights = value;
  }

  public virtual PXSpecialButtonType OnClosingPopup
  {
    get => this._OnClosingPopup;
    set => this._OnClosingPopup = value;
  }

  public virtual bool DynamicVisibility
  {
    get => this._DynamicVisibility;
    set => this._DynamicVisibility = value;
  }

  public virtual bool DynamicText
  {
    get => this._DynamicText;
    set => this._DynamicText = value;
  }

  public PXButtonState.CallbackData Callback { get; }

  public bool? VisibleOnDataSource { get; set; }

  public bool VisibleOnProcessingResults { get; set; }

  public virtual bool IsLockedOnToolbar { get; set; }

  public virtual string Category { get; set; }

  public virtual ActionConnotation Connotation { get; set; }

  public static PXButtonState CreateInstance(
    object value,
    string fieldName,
    string imageUrl,
    string disabledImageUrl,
    string hoverImageUrl,
    string tooltip,
    bool? pressed,
    PXConfirmationType confirmationType,
    string confirmationMessage,
    char? shortcutChar,
    bool? shortcutCtrl,
    bool? shortcutShift,
    ButtonMenu[] menus,
    bool? commitChanges,
    bool? closePopup,
    bool? popupVisible,
    string imageSet,
    string imageKey,
    PXShortCutAttribute extShortcut,
    System.Type itemType)
  {
    if (!(value is PXButtonState instance))
      instance = new PXButtonState(value);
    instance._DataType = typeof (Button);
    if (fieldName != null)
      instance._FieldName = fieldName;
    if (imageSet != null)
      instance._ImageSet = imageSet;
    if (imageKey != null)
      instance._ImageKey = imageKey;
    if (imageUrl != null)
      instance._ImageUrl = imageUrl;
    if (disabledImageUrl != null)
      instance._DisabledImageUrl = disabledImageUrl;
    if (hoverImageUrl != null)
      instance._HoverImageUrl = hoverImageUrl;
    if (tooltip != null)
      instance._Tooltip = tooltip;
    if (pressed.HasValue)
      instance._Pressed = pressed.Value;
    if (confirmationType != PXConfirmationType.Unspecified)
      instance._ConfirmationType = confirmationType;
    if (confirmationMessage != null)
      instance._ConfirmationMessage = confirmationMessage;
    if (shortcutChar.HasValue)
      instance._ShortcutChar = shortcutChar.Value;
    if (shortcutCtrl.HasValue)
      instance._ShortcutCtrl = shortcutCtrl.Value;
    if (shortcutShift.HasValue)
      instance._ShortcutShift = shortcutShift.Value;
    if (menus != null)
      instance._Menus = menus;
    if (commitChanges.HasValue)
      instance._CommitChanges = commitChanges.Value;
    if (closePopup.HasValue)
      instance._ClosePopup = closePopup.Value;
    if (popupVisible.HasValue)
      instance._PopupVisible = popupVisible.Value;
    instance._ExtShortcut = extShortcut;
    if (itemType != (System.Type) null)
      instance._ItemType = itemType;
    return instance;
  }

  public static PXButtonState CreateDefaultState<TPrimaryDAC>(object returnState)
  {
    return PXButtonState.CreateInstance(returnState, (string) null, (string) null, (string) null, (string) null, (string) null, new bool?(false), PXConfirmationType.Unspecified, (string) null, new char?(), new bool?(), new bool?(), (ButtonMenu[]) null, new bool?(), new bool?(), new bool?(), (string) null, (string) null, (PXShortCutAttribute) null, typeof (TPrimaryDAC));
  }

  public void ForceCommit(bool commit = true) => this._CommitChanges = commit;

  public class CallbackData
  {
    protected PXButtonState.PostDataMode _PostData = PXButtonState.PostDataMode.Page;
    protected int _RepaintControls;
    protected string _DependsOnView;
    protected string _PopupCommand;
    protected string _PopupCommandTarget;
    protected string _PopupPanel;
    protected string _SelectControlsIDs;
    protected string _RepaintControlsIDs;

    public virtual PXButtonState.PostDataMode PostData
    {
      get => this._PostData;
      internal set => this._PostData = value;
    }

    public virtual int RepaintControls => this._RepaintControls;

    public string DependsOnView
    {
      get => this._DependsOnView;
      internal set => this._DependsOnView = value;
    }

    public string PopupCommand
    {
      get => this._PopupCommand;
      internal set => this._PopupCommand = value;
    }

    public string PopupCommandTarget => this._PopupCommandTarget;

    public string PopupPanel => this._PopupPanel;

    public string SelectControlsIDs => this._SelectControlsIDs;

    public string RepaintControlsIDs => this._RepaintControlsIDs;

    public System.Type ActionOf { get; set; }
  }

  /// <summary>Defines identifiers for the callback post data mode.</summary>
  public enum PostDataMode
  {
    /// <summary>Control State only</summary>
    Self,
    /// <summary>Content of contol that initiated callback</summary>
    Content,
    /// <summary>
    /// Content of parent container for contol that initiated callback
    /// </summary>
    Container,
    /// <summary>Entire Page content</summary>
    Page,
  }
}
