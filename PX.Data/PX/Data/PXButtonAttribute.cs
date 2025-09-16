// Decompiled with JetBrains decompiler
// Type: PX.Data.PXButtonAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.WorkflowAPI;
using System;

#nullable disable
namespace PX.Data;

/// <summary>Sets up a button that is used to initiate the action in the
/// user interface.</summary>
/// <remarks>
/// <para>This attribute should be placed on the declaration of the method
/// that implements the action.</para>
/// <para>Through attribute's parameters, you can configure some
/// properties of the button, such <tt>ImageUrl</tt>,
/// <tt>ShortcutChar</tt>, and <tt>Tooltip</tt>. To configure other layout
/// properties, such as <tt>DisplayName</tt>, <tt>Visible</tt>, or
/// <tt>Enabled</tt>, use the <see cref="T:PX.Data.PXUIFieldAttribute">PXUIField</see>
/// attribute. Still some other properties can be set only on an
/// ASPX page.</para>
/// <para>A number of other attributes derive from the <tt>PXButton</tt>
/// attributes. These attribute do not implement additional logic and only
/// set certain properties to specific values.</para>
/// </remarks>
/// <example><para></para>
/// <code title="Example" lang="CS">
/// // An example of using the attribute without parameters is given below.
/// // Action declaration in a graph
/// public PXAction&lt;ApproveBillsFilter&gt; ViewDocument;
/// 
/// // Action implementation in a graph
/// [PXUIField(DisplayName = "View Document",
///            MapEnableRights = PXCacheRights.Update,
///            MapViewRights = PXCacheRights.Update)]
/// [PXButton]
/// public virtual IEnumerable viewDocument(PXAdapter adapter) { ... }</code>
/// <code title="Example2" lang="CS">
/// // In the example below the button is disabled by default (it can be enabled in code).
/// // Also, the ImageKey property sets a specific image to be displayed on the button.
/// public PXAction&lt;VendorR&gt; viewCustomer;
/// 
/// [PXUIField(DisplayName = Messages.ViewCustomer,
///            Enabled = false, Visible = true,
///            MapEnableRights = PXCacheRights.Select,
///            MapViewRights = PXCacheRights.Select)]
/// [PXButton(ImageKey = PX.Web.UI.Sprite.Main.Process)]
/// public virtual IEnumerable ViewCustomer(PXAdapter adapter) { ... }</code>
/// <code title="Example3" groupname="Example2" lang="CS">
/// // In the example below, the attribute provides specific URLs of the images
/// // displayed on the button by default (ImageUrl) when it is disabled (DisabledImageUrl).
/// // The tooltip is also specified.
/// public PXAction&lt;EPActivity&gt; CancelSending;
/// 
/// [PXUIField(DisplayName = EP.Messages.CancelSending, MapEnableRights = PXCacheRights.Select)]
/// [PXButton(ImageUrl = "~/Icons/Cancel_Active.gif",
///           DisabledImageUrl = "~/Icons/Cancel_NotActive.gif",
///           Tooltip = EP.Messages.CancelSendingTooltip)]
/// public virtual void cancelSending() { ... }</code>
/// </example>
public class PXButtonAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  protected string _ImageSet;
  protected string _ImageKey;
  protected string _ImageUrl;
  protected string _DisabledImageUrl;
  protected string _HoverImageUrl;
  protected string _Tooltip;
  protected PXConfirmationType _ConfirmationType = PXConfirmationType.Unspecified;
  protected string _ConfirmationMessage;
  protected string _Category;
  protected char _ShortcutChar;
  protected bool _ShortcutCtrl;
  protected bool _ClosePopup;
  protected bool _PopupVisible;
  protected bool _CommitChanges = true;
  protected bool _MenuAutoOpen;
  protected bool _DynamicText;
  private bool? _visibilityOnMainToolbar;
  protected bool _IsLockedOnToolbar;
  protected ActionConnotation _Connotation;

  /// <summary>
  /// Initializes a new instance of the <tt>PXButton</tt> attribute.
  /// </summary>
  public PXButtonAttribute()
  {
    this._PopupVisible = true;
    this.SpecialType = PXSpecialButtonType.Default;
  }

  /// <summary>Gets or sets the value that indicates whether the keyboard
  /// shortcut for the button includes the <i>Ctrl</i> key.</summary>
  public bool ShortcutCtrl
  {
    get => this._ShortcutCtrl;
    set => this._ShortcutCtrl = value;
  }

  /// <summary>Gets or sets the value that indicates whether the keyboard
  /// shortcut for the button includes the <i>Shift</i> key.</summary>
  public bool ShortcutShift { get; set; }

  /// <summary>Gets or sets the <see cref="T:PX.Data.PXSpecialButtonType">PXSpecialButtonType</see>
  /// value that indicates whether a button has a special type, such as
  /// <tt>Save</tt>, <tt>Cancel</tt>, or <tt>Refresh</tt>, or does not have.
  /// A button of a special type may be searched, for instance, by graph
  /// methods in special occassions (the <tt>PressSave()</tt> method
  /// searches visible buttons of <tt>Save</tt> type and selects the first
  /// of them). By default, the property is set to
  /// <tt>PXSpecialButtonType.Default</tt>.</summary>
  public PXSpecialButtonType SpecialType { get; set; }

  /// <summary>Gets or sets the special type of the button that will be
  /// triggerred on closing of an application webpage that is opened in
  /// popup mode.</summary>
  public PXSpecialButtonType OnClosingPopup { get; set; }

  public string PopupCommand { get; set; }

  /// <summary>Gets or sets the value that indicates whether the enclosing
  /// popup is closed once the button logic is executed.</summary>
  public bool ClosePopup
  {
    get => this._ClosePopup;
    set => this._ClosePopup = value;
  }

  /// <summary>Gets or sets the value that indicates whether the button is
  /// visible when the enclosing webpage is opened in popup mode.</summary>
  public bool PopupVisible
  {
    get => this._PopupVisible;
    set => this._PopupVisible = value;
  }

  /// <summary>Gets or sets the value that indicates whether a button press
  /// posts modifications to the server.</summary>
  public bool CommitChanges
  {
    get => this._CommitChanges;
    set => this._CommitChanges = value;
  }

  /// <summary>Gets or sets the character that is used as the keyboard
  /// shorcut for the button. Setting additionally the <tt>ShortcutCtrl</tt>
  /// and <tt>ShortcutShift</tt> properties adds or removes <i>Ctrl</i> and
  /// <i>Shift</i> keys to and from the shortcut.</summary>
  public char ShortcutChar
  {
    get => this._ShortcutChar;
    set => this._ShortcutChar = value;
  }

  /// <summary>Gets or sets the value that identifies the image set. Forms
  /// the first part of the button image URL.</summary>
  public string ImageSet
  {
    get => this._ImageSet;
    set => this._ImageSet = value;
  }

  /// <summary>Gets or sets the value that identifies the button image
  /// within the set specified by <tt>ImageSet</tt>. Forms the second part
  /// of the button image URL.</summary>
  public string ImageKey
  {
    get => this._ImageKey;
    set => this._ImageKey = value;
  }

  /// <summary>Gets or sets the URL of the image displayed on the button
  /// when it is enabled.</summary>
  /// <example>In the example below, the attribute provides specific URLs
  /// of the images displayed on the button by default (<tt>ImageUrl</tt>)
  /// and when it is disabled (<tt>DisabledImageUrl</tt>).
  /// <code>
  /// public PXAction&lt;EPActivity&gt; CancelSending;
  /// 
  /// [PXUIField(DisplayName = EP.Messages.CancelSending, MapEnableRights = PXCacheRights.Select)]
  /// [PXButton(ImageUrl = "~/Icons/Cancel_Active.gif",
  ///           DisabledImageUrl = "~/Icons/Cancel_NotActive.gif",
  ///           Tooltip = EP.Messages.CancelSendingTooltip)]
  /// public virtual void cancelSending() { ... }
  /// </code>
  /// </example>
  public string ImageUrl
  {
    get => this._ImageUrl;
    set => this._ImageUrl = value;
  }

  /// <summary>Gets or sets the URL of the image displayed on the button
  /// when it is disabled.</summary>
  public string DisabledImageUrl
  {
    get => this._DisabledImageUrl;
    set => this._DisabledImageUrl = value;
  }

  /// <summary>Gets or sets the URL of the image displayed on the enabled
  /// button on hover.</summary>
  public string HoverImageUrl
  {
    get => this._HoverImageUrl;
    set => this._HoverImageUrl = value;
  }

  /// <summary>Gets or sets the string displayed as a tooltip for the
  /// button.</summary>
  public string Tooltip
  {
    get => this._Tooltip;
    set => this._Tooltip = PXMessages.Localize(value, out string _);
  }

  /// <summary>Gets or sets the <see cref="T:PX.Data.PXConfirmationType">PXConfirmationType</see> value
  /// that indicates in what cases the confirmation message is shown to a
  /// user on a button press. By default, the property is set to
  /// <tt>PXConfirmationType.Unspecified</tt>.</summary>
  public PXConfirmationType ConfirmationType
  {
    get => this._ConfirmationType;
    set => this._ConfirmationType = value;
  }

  /// <summary>Gets or sets the confirmation message that can be shown to a
  /// user on a button press. The cases when the configramtion message is
  /// shown depend on <tt>ConfirmationType</tt>.</summary>
  public string ConfirmationMessage
  {
    get => this._ConfirmationMessage;
    set => this._ConfirmationMessage = PXMessages.Localize(value, out string _);
  }

  /// <summary>Gets or sets the value that indicates (if set to <tt>true</tt>) that a button click
  /// only expands the menu with other buttons. </summary>
  /// <value>If the value is <tt>true</tt>, the button
  /// click opens the menu and does not trigger a button's action.</value>
  public bool MenuAutoOpen
  {
    get => this._MenuAutoOpen;
    set => this._MenuAutoOpen = value;
  }

  public bool DynamicText
  {
    get => this._DynamicText;
    set => this._DynamicText = value;
  }

  [Obsolete("This field is obsolete and will be removed in the future versions. Use DisplayOnMainToolbar instead.")]
  public bool VisibleOnDataSource
  {
    get => this.DisplayOnMainToolbar;
    set => this.DisplayOnMainToolbar = value;
  }

  public bool DisplayOnMainToolbar
  {
    get => this._visibilityOnMainToolbar ?? true;
    set => this._visibilityOnMainToolbar = new bool?(value);
  }

  public bool IsLockedOnToolbar
  {
    get => this._IsLockedOnToolbar;
    set => this._IsLockedOnToolbar = value;
  }

  /// <summary>
  /// Gets or sets the value that indicates that the button will not be forced to be disabled when a main entity of the screen is archived.
  /// </summary>
  public bool IgnoresArchiveDisabling { get; set; }

  public string Category
  {
    get => this._Category;
    set => this._Category = PXMessages.Localize(value, out string _);
  }

  public ActionConnotation Connotation
  {
    get => this._Connotation;
    set => this._Connotation = value;
  }

  /// <summary>
  /// Gets or sets the value that indicates (if set to <tt>true</tt>) that the button appears in the <b>Processing</b> dialog box of a processing page.
  /// </summary>
  /// <value>By default, the value is <tt>false</tt> (the button does not appear in the <b>Processing</b> dialog box).</value>
  public bool VisibleOnProcessingResults { get; set; }

  void IPXFieldSelectingSubscriber.FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    PXButtonState instance = PXButtonState.CreateInstance(e.ReturnState, this._FieldName, this._ImageUrl, this._DisabledImageUrl, this._HoverImageUrl, this._Tooltip, new bool?(), this._ConfirmationType, this._ConfirmationMessage, this._ShortcutChar != char.MinValue ? new char?(this._ShortcutChar) : new char?(), new bool?(this._ShortcutCtrl), new bool?(this.ShortcutShift), (ButtonMenu[]) null, new bool?(this._CommitChanges), new bool?(this._ClosePopup), new bool?(this._PopupVisible), this._ImageSet, this._ImageKey, (PXShortCutAttribute) null, (System.Type) null);
    instance.SpecialType = this.SpecialType;
    instance.OnClosingPopup = this.OnClosingPopup;
    instance.VisibleOnDataSource = this._visibilityOnMainToolbar;
    instance.VisibleOnProcessingResults = this.VisibleOnProcessingResults;
    instance.DynamicText = this.DynamicText;
    instance.Callback.PopupCommand = this.PopupCommand;
    instance.IsLockedOnToolbar = this._IsLockedOnToolbar;
    instance.Category = this._Category;
    instance.Connotation = this._Connotation;
    e.ReturnState = (object) instance;
  }
}
