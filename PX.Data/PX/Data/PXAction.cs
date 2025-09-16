// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAction
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.DependencyInjection;
using PX.Data.WorkflowAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>The base abstract class for graph actions.</summary>
public abstract class PXAction
{
  private bool _clearAnswerAfterPress = true;
  internal PXGraphExtension[] Extensions;
  protected PXGraph _Graph;
  protected PXEventSubscriberAttribute[] _Attributes;
  protected bool _Pressed;
  internal ButtonMenu[] _Menus;
  /// <summary>The value that indicates whether a button click
  /// only expands the menu with other buttons. If the value is <see langword="true" />,
  /// the button click opens the menu and does not trigger the button's action.</summary>
  public bool MenuAutoOpen;
  protected PXEventSubscriberAttribute[] _MenuAttributes;
  protected bool _Altered;
  protected bool _DynamicVisibility;
  internal Action<PXAction, PXAdapter> BeforeRunHandler;
  internal Action<PXAction, PXAdapter, Exception> AfterRunHandler;
  protected bool? _isMass;
  protected PXFieldSelecting _StateSelectingEvents;

  internal static bool IsInterruptedByActionLogic
  {
    get => PXContext.GetSlot<bool>("WorkflowAction.IsInterruptedByActionLogic");
    set => PXContext.SetSlot<bool>("WorkflowAction.IsInterruptedByActionLogic", value);
  }

  public bool ClearAnswerAfterPress
  {
    get => this._clearAnswerAfterPress;
    set => this._clearAnswerAfterPress = value;
  }

  protected PXGraphExtension[] GraphExtensions => this.Extensions;

  internal IEnumerable<PXEventSubscriberAttribute> Attributes
  {
    get => (IEnumerable<PXEventSubscriberAttribute>) this._Attributes;
  }

  protected internal bool AutomationDisabled { get; set; }

  protected internal bool AutomationHidden { get; set; }

  protected internal bool WorkflowHiddenOnMainToolbar { get; set; }

  protected internal string AutomationConnotation { get; set; }

  protected internal string AutomationCategory { get; set; }

  public PXAction(PXGraph graph)
  {
    this._Graph = graph != null ? graph : throw new PXArgumentException(nameof (graph), "The argument cannot be null.");
    InjectMethods.InjectDependencies(this);
    PXExtensionManager.InitExtensions((object) this);
  }

  public abstract IEnumerable Press(PXAdapter adapter);

  public abstract void Press();

  public abstract object GetState(object row);

  public PXGraph Graph => this._Graph;

  public virtual bool GetEnabled()
  {
    bool enabled = true;
    foreach (PXEventSubscriberAttribute attribute in this._Attributes)
    {
      if (attribute is IPXInterfaceField)
        enabled &= ((IPXInterfaceField) attribute).Enabled;
    }
    return enabled;
  }

  public virtual bool GetEnabled(string menu)
  {
    bool enabled = true;
    if (this._Menus == null)
    {
      string str = (string) null;
      foreach (PXEventSubscriberAttribute attribute in this._Attributes)
      {
        if (attribute is PXUIFieldAttribute)
        {
          str = ((PXUIFieldAttribute) attribute).DisplayName;
          break;
        }
      }
      if (string.IsNullOrEmpty(str))
        throw new PXException("Menu item '{0}' cannot be enabled or disabled for an action because its menus are not defined.", new object[1]
        {
          (object) menu
        });
      throw new PXException("Menu item '{0}' cannot be enabled or disabled for the action '{1}' because its menus are not defined.", new object[2]
      {
        (object) menu,
        (object) str
      });
    }
    for (int index = 0; index < this._Menus.Length; ++index)
    {
      ButtonMenu menu1 = this._Menus[index];
      if (menu1.Command == menu)
      {
        if (this._MenuAttributes != null)
          enabled &= ((IPXInterfaceField) this._MenuAttributes[index]).Enabled;
        enabled &= menu1.GetEnabled();
        break;
      }
    }
    return enabled;
  }

  public virtual void SetEnabled(bool isEnabled)
  {
    foreach (PXEventSubscriberAttribute attribute in this._Attributes)
    {
      if (attribute is IPXInterfaceField)
      {
        ((IPXInterfaceField) attribute).Enabled = isEnabled;
        this._Altered = true;
      }
    }
  }

  public virtual void SetEnabled(string menu, bool isEnabled)
  {
    this.SetEnabledInternal(menu, isEnabled);
  }

  internal void SetEnabledInternal(string menu, bool isEnabled, bool caseSensitive = true)
  {
    if (this._Menus == null)
    {
      string str = (string) null;
      foreach (PXEventSubscriberAttribute attribute in this._Attributes)
      {
        if (attribute is PXUIFieldAttribute)
        {
          str = ((PXUIFieldAttribute) attribute).DisplayName;
          break;
        }
      }
      if (string.IsNullOrEmpty(str))
        throw new PXException("Menu item '{0}' cannot be enabled or disabled for an action because its menus are not defined.", new object[1]
        {
          (object) menu
        });
    }
    else
    {
      for (int index = 0; index < this._Menus.Length; ++index)
      {
        ButtonMenu menu1 = this._Menus[index];
        if (string.Equals(menu1.Command, menu, caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
        {
          if (this._MenuAttributes != null)
          {
            ((IPXInterfaceField) this._MenuAttributes[index]).Enabled = isEnabled;
            isEnabled = ((IPXInterfaceField) this._MenuAttributes[index]).Enabled;
          }
          menu1.Enabled = isEnabled;
          break;
        }
      }
    }
  }

  public virtual bool GetVisible()
  {
    bool visible = true;
    foreach (PXEventSubscriberAttribute attribute in this._Attributes)
    {
      if (attribute is IPXInterfaceField)
        visible &= ((IPXInterfaceField) attribute).Visible;
    }
    return visible;
  }

  public virtual void SetVisible(string menu, bool isVisible)
  {
    this.SetVisibleInternal(menu, isVisible);
  }

  internal void SetVisibleInternal(string menu, bool isVisible, bool caseSensitive = true)
  {
    if (this._Menus == null)
    {
      string str = (string) null;
      foreach (PXEventSubscriberAttribute attribute in this._Attributes)
      {
        if (attribute is PXUIFieldAttribute)
        {
          str = ((PXUIFieldAttribute) attribute).DisplayName;
          break;
        }
      }
      if (string.IsNullOrEmpty(str))
        throw new PXException("Menu item '{0}' cannot be enabled or disabled for an action because its menus are not defined.", new object[1]
        {
          (object) menu
        });
    }
    else
    {
      for (int index = 0; index < this._Menus.Length; ++index)
      {
        ButtonMenu menu1 = this._Menus[index];
        if (string.Equals(menu1.Command, menu, caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
        {
          if (this._MenuAttributes != null)
          {
            ((IPXInterfaceField) this._MenuAttributes[index]).Visible = isVisible;
            isVisible = ((IPXInterfaceField) this._MenuAttributes[index]).Visible;
          }
          menu1.Visible = isVisible;
          break;
        }
      }
    }
  }

  /// <summary>Displays or hides the action button.</summary>
  /// <param name="isVisible">If the value is <see langword="true" />, the button is displayed.</param>
  public virtual void SetVisible(bool isVisible)
  {
    foreach (PXEventSubscriberAttribute attribute in this._Attributes)
    {
      if (attribute is IPXInterfaceField)
      {
        ((IPXInterfaceField) attribute).Visible = isVisible;
        this._DynamicVisibility = true;
      }
    }
  }

  public virtual void SetDynamicText(bool isDynamicText)
  {
    foreach (PXEventSubscriberAttribute attribute in this._Attributes)
    {
      if (attribute is PXButtonAttribute)
        ((PXButtonAttribute) attribute).DynamicText = isDynamicText;
    }
  }

  public virtual void SetCaption(string caption)
  {
    foreach (PXEventSubscriberAttribute attribute in this._Attributes)
    {
      if (attribute is IPXInterfaceField)
        ((IPXInterfaceField) attribute).DisplayName = PXMessages.Localize(caption, out string _);
    }
  }

  public virtual void SetImageKey(string imageKey)
  {
    foreach (PXEventSubscriberAttribute attribute in this._Attributes)
    {
      if (attribute is PXButtonAttribute)
        ((PXButtonAttribute) attribute).ImageKey = imageKey;
    }
  }

  public virtual string GetCaption()
  {
    foreach (PXEventSubscriberAttribute attribute in this._Attributes)
    {
      if (attribute is IPXInterfaceField pxInterfaceField)
        return pxInterfaceField.DisplayName;
    }
    return (string) null;
  }

  public virtual void SetTooltip(string tooltip)
  {
    foreach (PXEventSubscriberAttribute attribute in this._Attributes)
    {
      if (attribute is PXButtonAttribute)
        ((PXButtonAttribute) attribute).Tooltip = PXMessages.Localize(tooltip, out string _);
    }
  }

  public virtual void SetConfirmationMessage(string message)
  {
    PXButtonAttribute pxButtonAttribute = this.Attributes.OfType<PXButtonAttribute>().FirstOrDefault<PXButtonAttribute>();
    if (pxButtonAttribute == null)
      return;
    pxButtonAttribute.ConfirmationMessage = PXMessages.Localize(message, out string _);
  }

  public void SetPressed(bool isPressed) => this._Pressed = isPressed;

  [Obsolete("This field is obsolete and will be removed in the future versions. Use SetDisplayOnMainToolbar instead.")]
  public virtual void SetVisibleOnDataSource(bool isVisibleOnDataSource)
  {
    PXButtonAttribute pxButtonAttribute = this.Attributes.OfType<PXButtonAttribute>().FirstOrDefault<PXButtonAttribute>();
    if (pxButtonAttribute == null)
      return;
    pxButtonAttribute.VisibleOnDataSource = isVisibleOnDataSource;
  }

  public virtual void SetDisplayOnMainToolbar(bool visible)
  {
    PXButtonAttribute pxButtonAttribute = this.Attributes.OfType<PXButtonAttribute>().FirstOrDefault<PXButtonAttribute>();
    if (pxButtonAttribute == null)
      return;
    pxButtonAttribute.DisplayOnMainToolbar = visible;
  }

  public virtual bool? GetDisplayOnMainToolbar()
  {
    return this.Attributes.OfType<PXButtonAttribute>().FirstOrDefault<PXButtonAttribute>()?.DisplayOnMainToolbar;
  }

  public virtual void SetCategory(string category)
  {
    PXButtonAttribute pxButtonAttribute = this.Attributes.OfType<PXButtonAttribute>().FirstOrDefault<PXButtonAttribute>();
    if (pxButtonAttribute == null)
      return;
    pxButtonAttribute.Category = category;
  }

  public virtual string GetCategory()
  {
    return this.Attributes.OfType<PXButtonAttribute>().FirstOrDefault<PXButtonAttribute>()?.Category;
  }

  public virtual PXSpecialButtonType? GetSpecialType()
  {
    return this.Attributes.OfType<PXButtonAttribute>().FirstOrDefault<PXButtonAttribute>()?.SpecialType;
  }

  public virtual void SetIsLockedOnToolbar(bool locked)
  {
    PXButtonAttribute pxButtonAttribute = this.Attributes.OfType<PXButtonAttribute>().FirstOrDefault<PXButtonAttribute>();
    if (pxButtonAttribute == null)
      return;
    pxButtonAttribute.IsLockedOnToolbar = locked;
  }

  public virtual bool? GetIsLockedOnToolbar()
  {
    return this.Attributes.OfType<PXButtonAttribute>().FirstOrDefault<PXButtonAttribute>()?.IsLockedOnToolbar;
  }

  public virtual void SetIgnoresArchiveDisabling(bool value)
  {
    PXButtonAttribute pxButtonAttribute = this.Attributes.OfType<PXButtonAttribute>().FirstOrDefault<PXButtonAttribute>();
    if (pxButtonAttribute == null)
      return;
    pxButtonAttribute.IgnoresArchiveDisabling = value;
  }

  public virtual bool? GetIgnoresArchiveDisabling()
  {
    return this.Attributes.OfType<PXButtonAttribute>().FirstOrDefault<PXButtonAttribute>()?.IgnoresArchiveDisabling;
  }

  public virtual void SetConnotation(ActionConnotation connotation)
  {
    PXButtonAttribute pxButtonAttribute = this.Attributes.OfType<PXButtonAttribute>().FirstOrDefault<PXButtonAttribute>();
    if (pxButtonAttribute == null)
      return;
    pxButtonAttribute.Connotation = connotation;
  }

  public virtual ActionConnotation? GetConnotation()
  {
    return this.Attributes.OfType<PXButtonAttribute>().FirstOrDefault<PXButtonAttribute>()?.Connotation;
  }

  public virtual void SetCommitChanges(bool commitChanges)
  {
    PXButtonAttribute pxButtonAttribute = this.Attributes.OfType<PXButtonAttribute>().FirstOrDefault<PXButtonAttribute>();
    if (pxButtonAttribute == null)
      return;
    pxButtonAttribute.CommitChanges = commitChanges;
  }

  /// <summary>
  /// Appends child menu items to the action button.
  /// When a child menu item is clicked by a user, the same action handler is executed
  /// with a distinct PXAdapter.Menu parameter.
  /// </summary>
  /// <param name="menus"></param>
  public abstract void SetMenu(ButtonMenu[] menus);

  public virtual void SetMapEnableRights(PXCacheRights mapping)
  {
    foreach (PXEventSubscriberAttribute attribute in this._Attributes)
    {
      if (attribute is IPXInterfaceField)
        ((IPXInterfaceField) attribute).MapEnableRights = mapping;
    }
  }

  public virtual void SetMapViewRights(PXCacheRights mapping)
  {
    foreach (PXEventSubscriberAttribute attribute in this._Attributes)
    {
      if (attribute is IPXInterfaceField)
        ((IPXInterfaceField) attribute).MapViewRights = mapping;
    }
  }

  public virtual void AddMenuAction(PXAction action)
  {
    this.AddMenuAction(action, (string) null, false);
  }

  public abstract void AddMenuAction(PXAction action, string prevAction, bool insertAfter);

  internal abstract void RemoveMenuAction(PXAction action);

  public virtual string[] GetParameterNames() => new string[0];

  public virtual PXFieldState GetParameterExt(string name) => (PXFieldState) null;

  public virtual object SetParameterExt(string name, object value) => (object) null;

  public abstract System.Type GetRowType();

  /// <summary>
  /// An indicator of whether the action is available for mass-processing operations.
  /// </summary>
  /// <value>The value is <see langword="false" /> by default.</value>
  public virtual bool IsMass
  {
    get
    {
      bool? isMass = this._isMass;
      bool flag = true;
      return isMass.GetValueOrDefault() == flag & isMass.HasValue;
    }
    set => this._isMass = new bool?(value);
  }

  /// <summary>Determines if action has full PXAdapter support.</summary>
  /// <remarks>This property is used in Mass Process (GI).</remarks>
  internal virtual bool HasAdapterSupport => false;

  /// <exclude />
  public event PXFieldSelecting StateSelectingEvents
  {
    add => this._StateSelectingEvents = value + this._StateSelectingEvents;
    remove => this._StateSelectingEvents -= value;
  }
}
