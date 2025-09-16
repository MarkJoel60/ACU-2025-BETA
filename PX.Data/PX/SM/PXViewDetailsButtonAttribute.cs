// Decompiled with JetBrains decompiler
// Type: PX.SM.PXViewDetailsButtonAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.SM;

/// <exclude />
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class PXViewDetailsButtonAttribute : PXViewExtensionAttribute
{
  public const string ACTION_SUFFIX = "_ViewDetails";
  private PXSpecialButtonType _OnClosingPopup;
  private System.Type _primaryType;
  private readonly BqlCommand _select;
  private readonly System.Type _targetItemType;
  private readonly string _fieldName;
  private System.Type _itemType;
  private object _windowMode;

  public PXSpecialButtonType OnClosingPopup
  {
    get => this._OnClosingPopup;
    set => this._OnClosingPopup = value;
  }

  public object WindowMode
  {
    get => this._windowMode;
    set
    {
      switch (value)
      {
        case null:
        case PXRedirectHelper.WindowMode _:
          this._windowMode = value;
          break;
        default:
          throw new ArgumentException("The argument must be a value from the PXRedirectHelper.WindowMode enum.");
      }
    }
  }

  private PXRedirectHelper.WindowMode? WindowModeInternal
  {
    get => this.WindowMode as PXRedirectHelper.WindowMode?;
  }

  public PXViewDetailsButtonAttribute()
  {
  }

  public PXViewDetailsButtonAttribute(System.Type primaryType)
  {
    if (primaryType == (System.Type) null)
      throw new ArgumentNullException();
    if (primaryType.IsNested && typeof (IBqlField).IsAssignableFrom(primaryType))
    {
      this._fieldName = primaryType.Name;
      this._primaryType = BqlCommand.GetItemType(primaryType);
    }
    else if (typeof (IBqlSearch).IsAssignableFrom(primaryType))
    {
      this._select = BqlCommand.CreateInstance(primaryType);
      System.Type[] tables = this._select.GetTables();
      this._targetItemType = tables != null && tables.Length != 0 ? tables[0] : throw new ArgumentException("The target DAC for the '*_ViewDetails' action cannot be found.", "select");
      this._fieldName = ((IBqlSearch) this._select).GetField().Name;
    }
    else if (typeof (IBqlSelect).IsAssignableFrom(primaryType))
    {
      this._select = BqlCommand.CreateInstance(primaryType);
      System.Type[] tables = this._select.GetTables();
      this._targetItemType = tables != null && tables.Length != 0 ? tables[0] : throw new ArgumentException("The target DAC for the '*_ViewDetails' action cannot be found.", "select");
    }
    else
      this._primaryType = primaryType;
  }

  public PXViewDetailsButtonAttribute(System.Type primaryType, System.Type select)
    : this(primaryType)
  {
    if (select == (System.Type) null)
      throw new ArgumentNullException(nameof (select));
    this._select = typeof (IBqlSelect).IsAssignableFrom(select) ? BqlCommand.CreateInstance(select) : throw new ArgumentException($"Inheritor of the '{typeof (IBqlSelect)}' type is expected.", nameof (select));
    System.Type[] tables = this._select.GetTables();
    this._targetItemType = tables != null && tables.Length != 0 ? tables[0] : throw new ArgumentException("The target DAC for the '*_ViewDetails' action cannot be found.", nameof (select));
  }

  public string ActionName { get; set; }

  public override void ViewCreated(PXGraph graph, string viewName)
  {
    if (this._primaryType == (System.Type) null)
      this._primaryType = graph.PrimaryItemType;
    string str1 = this.ActionName;
    if (string.IsNullOrEmpty(str1))
    {
      string str2 = viewName;
      if (this._targetItemType != (System.Type) null)
      {
        if (this._fieldName != null)
          str2 = $"{str2}_{this._targetItemType.Name}__{this._fieldName}";
        else
          str2 = $"{str2}_{this._targetItemType.Name}";
      }
      else if (this._fieldName != null)
        str2 = $"{str2}_{this._fieldName}";
      str1 = str2 + "_ViewDetails";
    }
    this._itemType = graph.Views[viewName].CacheGetItemType();
    if (this._fieldName != null && this._itemType == this._primaryType && (this._targetItemType == (System.Type) null || this._targetItemType == this._primaryType))
    {
      PXUIFieldAttribute pxuiFieldAttribute = new PXUIFieldAttribute()
      {
        DisplayName = "",
        MapEnableRights = PXCacheRights.Select
      };
      PXEditDetailButtonAttribute detailButtonAttribute1 = new PXEditDetailButtonAttribute();
      detailButtonAttribute1.OnClosingPopup = this.OnClosingPopup;
      detailButtonAttribute1.CommitChanges = false;
      PXEditDetailButtonAttribute detailButtonAttribute2 = detailButtonAttribute1;
      PXAction instance = (PXAction) Activator.CreateInstance(typeof (PXNamedAction<>).MakeGenericType(this._primaryType), (object) graph, (object) str1, (object) new PXButtonDelegate(this.Handler), (object) pxuiFieldAttribute, (object) detailButtonAttribute2);
      bool flag = false;
      int num = 0;
      for (int index = 0; index < graph.Actions.Count; ++index)
      {
        PXAction action = (PXAction) graph.Actions[index];
        if (action != null && action.GetState((object) null) is PXButtonState state && (state.SpecialType == PXSpecialButtonType.Cancel || state.SpecialType == PXSpecialButtonType.Insert))
        {
          num = index;
          flag = true;
        }
      }
      if (flag)
        graph.Actions.Insert(num + 1, (object) str1, (object) instance);
      else
        graph.Actions[str1] = instance;
    }
    else
    {
      PXButtonAttribute pxButtonAttribute = new PXButtonAttribute()
      {
        OnClosingPopup = this.OnClosingPopup
      };
      PXNamedAction.AddAction(graph, this._primaryType, str1, "View Details", (string) null, false, new PXButtonDelegate(this.Handler), (PXEventSubscriberAttribute) pxButtonAttribute);
    }
  }

  protected virtual IEnumerable Handler(PXAdapter adapter)
  {
    PXGraph graph = adapter.View.Cache.Graph;
    object record = graph.Caches[this._itemType].Current;
    if (this._select != null)
      record = new PXView(graph, false, this._select).SelectSingle();
    if (record is PXResult)
      record = ((PXResult) record)[0];
    if (record != null)
      this.Redirect(adapter, record);
    return adapter.Get();
  }

  protected virtual void Redirect(PXAdapter adapter, object record)
  {
    PXRedirectHelper.WindowMode windowMode = this._fieldName == null || !(this._itemType == this._primaryType) || !(this._targetItemType == (System.Type) null) && !(this._targetItemType == this._primaryType) ? PXRedirectHelper.WindowMode.NewWindow : PXRedirectHelper.WindowMode.InlineWindow;
    PXRedirectHelper.TryRedirect(adapter.View.Cache.Graph, record, (PXRedirectHelper.WindowMode) ((int) this.WindowModeInternal ?? (int) windowMode));
  }
}
