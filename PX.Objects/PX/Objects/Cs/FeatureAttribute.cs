// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.FeatureAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.CS;

[PXDefault(false)]
[PXDBBool]
[PXUIField]
public class FeatureAttribute : 
  FeatureRestrictorAttribute,
  IPXFieldSelectingSubscriber,
  IPXFieldDefaultingSubscriber
{
  protected bool _defValue;

  public FeatureAttribute(bool defValue)
    : this(defValue, (Type) null, (Type) null)
  {
  }

  public FeatureAttribute(bool defValue, Type parent)
    : this(defValue, parent, (Type) null)
  {
  }

  public FeatureAttribute(Type parent)
    : this(parent, (Type) null)
  {
  }

  public FeatureAttribute(Type parent, Type checkUsage)
    : base(checkUsage)
  {
    this.Parent = parent;
  }

  public FeatureAttribute(bool defValue, Type parent, Type checkUsage)
    : this(parent, checkUsage)
  {
    this.GetAttribute<PXDefaultAttribute>().Constant = (object) defValue;
  }

  public Type Parent { get; set; }

  public string DisplayName
  {
    get => this.GetAttribute<PXUIFieldAttribute>().DisplayName;
    set => this.GetAttribute<PXUIFieldAttribute>().DisplayName = value;
  }

  public bool Enabled
  {
    get => this.GetAttribute<PXUIFieldAttribute>().Enabled;
    set => this.GetAttribute<PXUIFieldAttribute>().Enabled = value;
  }

  public bool Visible
  {
    get => this.GetAttribute<PXUIFieldAttribute>().Visible;
    set => this.GetAttribute<PXUIFieldAttribute>().Visible = value;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!(this.Parent != (Type) null) || !(this.Parent.DeclaringType != (Type) null))
      return;
    Type c = this.Parent.DeclaringType;
    if (typeof (PXCacheExtension).IsAssignableFrom(c) && c.BaseType.IsGenericType)
      c = c.BaseType.GetGenericArguments()[c.BaseType.GetGenericArguments().Length - 1];
    PXGraph.RowUpdatedEvents rowUpdated = sender.Graph.RowUpdated;
    Type type = c;
    FeatureAttribute featureAttribute = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) featureAttribute, __vmethodptr(featureAttribute, RowUpdated));
    rowUpdated.AddHandler(type, pxRowUpdated);
  }

  public bool Top { get; set; }

  public bool SyncToParent { get; set; }

  protected virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    bool? nullable1 = (bool?) sender.GetValue(e.Row, this.Parent.Name);
    bool? nullable2 = (bool?) sender.GetValue(e.OldRow, this.Parent.Name);
    object obj1 = sender.GetValue(e.Row, ((IEnumerable<string>) sender.Keys).Last<string>());
    if (obj1 != null && obj1 is int? && (int) obj1 != 3)
      return;
    bool? nullable3 = nullable1;
    bool? nullable4 = nullable2;
    if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
      return;
    if (nullable1.GetValueOrDefault())
    {
      object obj2 = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
      if (obj2 == null || !(obj2 is bool? nullable5) || !nullable5.GetValueOrDefault())
        sender.SetDefaultExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
      if (!this.SyncToParent)
        return;
      sender.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) true);
    }
    else
      sender.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) false);
  }

  public void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    bool flag1 = !this.SyncToParent;
    bool? nullable1;
    if (sender.AllowUpdate)
    {
      if (this.Parent != (Type) null)
      {
        nullable1 = (bool?) sender.GetValue(e.Row, this.Parent.Name);
        bool flag2 = false;
        if (!(nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue))
          goto label_4;
      }
      else
        goto label_4;
    }
    flag1 = false;
label_4:
    PXFieldSelectingEventArgs selectingEventArgs = e;
    object returnState = e.ReturnState;
    Type type = typeof (bool);
    nullable1 = new bool?();
    bool? nullable2 = nullable1;
    nullable1 = new bool?();
    bool? nullable3 = nullable1;
    int? nullable4 = new int?(-1);
    int? nullable5 = new int?();
    int? nullable6 = new int?();
    string fieldName = ((PXEventSubscriberAttribute) this)._FieldName;
    string displayName = this.DisplayName;
    bool? nullable7 = new bool?(flag1);
    nullable1 = new bool?();
    bool? nullable8 = nullable1;
    nullable1 = new bool?();
    bool? nullable9 = nullable1;
    PXFieldState instance = PXFieldState.CreateInstance(returnState, type, nullable2, nullable3, nullable4, nullable5, nullable6, (object) null, fieldName, (string) null, displayName, (string) null, (PXErrorLevel) 0, nullable7, nullable8, nullable9, (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    selectingEventArgs.ReturnState = (object) instance;
  }

  public void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (!(this.Parent != (Type) null) || ((bool?) sender.GetValue(e.Row, this.Parent.Name)).GetValueOrDefault())
      return;
    ((CancelEventArgs) e).Cancel = true;
    e.NewValue = (object) false;
  }
}
