// Decompiled with JetBrains decompiler
// Type: PX.Data.PXEntityAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// This is a generic attribute that aggregates other attributes and exposes some of their public properties.
/// The attribute is usually attached to a field that stores a reference to some entity.
/// </summary>
/// <remarks>
/// Aggregated attributes can be of the following types:
/// <list type="bullet">
/// <item><description><see cref="T:PX.Data.PXDBFieldAttribute" />, such as <see cref="T:PX.Data.PXDBIntAttribute" />, <see cref="T:PX.Data.PXDBStringAttribute" />, and their unbound analogs;</description></item>
/// <item><description><see cref="T:PX.Data.PXUIFieldAttribute" />;</description></item>
/// <item><description><see cref="T:PX.Data.PXSelectorAttribute" /> or <see cref="T:PX.Data.PXDimensionSelectorAttribute" />;</description></item>
/// <item><description><see cref="T:PX.Data.PXDefaultAttribute" />.</description></item>
/// </list>
/// </remarks>
public abstract class PXEntityAttribute : 
  PXAggregateAttribute,
  IPXInterfaceField,
  IPXCommandPreparingSubscriber,
  IPXRowSelectingSubscriber
{
  protected int _DBAttrIndex = -1;
  protected int _NonDBAttrIndex = -1;
  protected int _UIAttrIndex = -1;
  protected int _SelAttrIndex = -1;
  protected int _DefAttrIndex = -1;

  protected PXEntityAttribute()
  {
    this.Initialize();
    this.Filterable = true;
  }

  protected virtual void Initialize()
  {
    this._DBAttrIndex = -1;
    this._NonDBAttrIndex = -1;
    this._UIAttrIndex = -1;
    this._SelAttrIndex = -1;
    this._DefAttrIndex = -1;
    foreach (PXEventSubscriberAttribute attribute1 in (List<PXEventSubscriberAttribute>) this._Attributes)
    {
      if (attribute1 is PXDBFieldAttribute)
      {
        this._DBAttrIndex = this._Attributes.IndexOf(attribute1);
        foreach (PXEventSubscriberAttribute attribute2 in (List<PXEventSubscriberAttribute>) this._Attributes)
        {
          if (attribute1 != attribute2 && PXAttributeFamilyAttribute.IsSameFamily(attribute1.GetType(), attribute2.GetType()))
          {
            this._NonDBAttrIndex = this._Attributes.IndexOf(attribute2);
            break;
          }
        }
      }
      if (attribute1 is PXUIFieldAttribute)
        this._UIAttrIndex = this._Attributes.IndexOf(attribute1);
      if (attribute1 is PXDimensionSelectorAttribute)
        this._SelAttrIndex = this._Attributes.IndexOf(attribute1);
      if (attribute1 is PXSelectorAttribute && this._SelAttrIndex < 0)
        this._SelAttrIndex = this._Attributes.IndexOf(attribute1);
      if (attribute1 is PXDefaultAttribute)
        this._DefAttrIndex = this._Attributes.IndexOf(attribute1);
    }
  }

  public override void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers)
  {
    if (typeof (ISubscriber) == typeof (IPXCommandPreparingSubscriber) || typeof (ISubscriber) == typeof (IPXRowSelectingSubscriber))
    {
      if (!this.IsDBField)
      {
        if (this.NonDBAttribute == null)
          subscribers.Add(this as ISubscriber);
        else if (typeof (ISubscriber) == typeof (IPXRowSelectingSubscriber))
          subscribers.Add(this as ISubscriber);
        else
          this.NonDBAttribute.GetSubscriber<ISubscriber>(subscribers);
        for (int index = 0; index < this._Attributes.Count; ++index)
        {
          if (index != this._DBAttrIndex && index != this._NonDBAttrIndex)
            this._Attributes[index].GetSubscriber<ISubscriber>(subscribers);
        }
      }
      else
      {
        base.GetSubscriber<ISubscriber>(subscribers);
        if (this.NonDBAttribute != null)
          subscribers.Remove(this.NonDBAttribute as ISubscriber);
        subscribers.Remove(this as ISubscriber);
      }
    }
    else
    {
      base.GetSubscriber<ISubscriber>(subscribers);
      if (this.NonDBAttribute == null)
        return;
      subscribers.Remove(this.NonDBAttribute as ISubscriber);
    }
  }

  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    e.Expr = (SQLExpression) new SQLConst((object) string.Empty);
    e.Cancel = true;
  }

  public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (!this.IsDBField)
      return;
    sender.SetValue(e.Row, this._FieldOrdinal, (object) null);
  }

  public bool IsDBField { get; set; } = true;

  protected PXDBFieldAttribute DBAttribute
  {
    get
    {
      return this._DBAttrIndex != -1 ? (PXDBFieldAttribute) this._Attributes[this._DBAttrIndex] : (PXDBFieldAttribute) null;
    }
  }

  protected PXEventSubscriberAttribute NonDBAttribute
  {
    get
    {
      return this._NonDBAttrIndex != -1 ? this._Attributes[this._NonDBAttrIndex] : (PXEventSubscriberAttribute) null;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXEventSubscriberAttribute.FieldName" />
  public new string FieldName
  {
    get => this.DBAttribute?.FieldName;
    set => this.DBAttribute.FieldName = value;
  }

  /// <inheritdoc cref="P:PX.Data.PXDBFieldAttribute.IsKey" />
  public bool IsKey
  {
    get
    {
      PXDBFieldAttribute dbAttribute = this.DBAttribute;
      return dbAttribute != null && dbAttribute.IsKey;
    }
    set => this.DBAttribute.IsKey = value;
  }

  /// <inheritdoc cref="P:PX.Data.PXDBStringAttribute.IsFixed" />
  public bool IsFixed
  {
    get
    {
      PXDBStringAttribute dbAttribute = (PXDBStringAttribute) this.DBAttribute;
      return dbAttribute != null && dbAttribute.IsFixed;
    }
    set
    {
      ((PXDBStringAttribute) this.DBAttribute).IsFixed = value;
      if (this.NonDBAttribute == null)
        return;
      ((PXStringAttribute) this.NonDBAttribute).IsFixed = value;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXDBFieldAttribute.BqlField" />
  public System.Type BqlField
  {
    get => this.DBAttribute?.BqlField;
    set
    {
      this.DBAttribute.BqlField = value;
      this.BqlTable = this.DBAttribute.BqlTable;
    }
  }

  protected PXUIFieldAttribute UIAttribute
  {
    get
    {
      return this._UIAttrIndex != -1 ? (PXUIFieldAttribute) this._Attributes[this._UIAttrIndex] : (PXUIFieldAttribute) null;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXUIFieldAttribute.Visibility" />
  public PXUIVisibility Visibility
  {
    get
    {
      PXUIFieldAttribute uiAttribute = this.UIAttribute;
      return uiAttribute == null ? PXUIVisibility.Undefined : uiAttribute.Visibility;
    }
    set
    {
      if (this.UIAttribute == null)
        return;
      this.UIAttribute.Visibility = value;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXUIFieldAttribute.Visible" />
  public bool Visible
  {
    get
    {
      PXUIFieldAttribute uiAttribute = this.UIAttribute;
      return uiAttribute == null || uiAttribute.Visible;
    }
    set
    {
      if (this.UIAttribute == null)
        return;
      this.UIAttribute.Visible = value;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXUIFieldAttribute.Enabled" />
  public bool Enabled
  {
    get
    {
      PXUIFieldAttribute uiAttribute = this.UIAttribute;
      return uiAttribute == null || uiAttribute.Enabled;
    }
    set
    {
      if (this.UIAttribute == null)
        return;
      this.UIAttribute.Enabled = value;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXUIFieldAttribute.DisplayName" />
  public string DisplayName
  {
    get => this.UIAttribute?.DisplayName;
    set
    {
      if (this.UIAttribute == null)
        return;
      this.UIAttribute.DisplayName = value;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXUIFieldAttribute.FieldClass" />
  public string FieldClass
  {
    get => this.UIAttribute?.FieldClass;
    set
    {
      if (this.UIAttribute == null)
        return;
      this.UIAttribute.FieldClass = value;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXUIFieldAttribute.Required" />
  public bool Required
  {
    get
    {
      PXUIFieldAttribute uiAttribute = this.UIAttribute;
      return uiAttribute != null && uiAttribute.Required;
    }
    set
    {
      if (this.UIAttribute == null)
        return;
      this.UIAttribute.Required = value;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXUIFieldAttribute.TabOrder" />
  public virtual int TabOrder
  {
    get
    {
      PXUIFieldAttribute uiAttribute = this.UIAttribute;
      return uiAttribute == null ? this._FieldOrdinal : uiAttribute.TabOrder;
    }
    set
    {
      if (this.UIAttribute == null)
        return;
      this.UIAttribute.TabOrder = value;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXUIFieldAttribute.ErrorHandling" />
  public virtual PXErrorHandling ErrorHandling
  {
    get
    {
      PXUIFieldAttribute uiAttribute = this.UIAttribute;
      return uiAttribute == null ? PXErrorHandling.WhenVisible : uiAttribute.ErrorHandling;
    }
    set
    {
      if (this.UIAttribute == null)
        return;
      this.UIAttribute.ErrorHandling = value;
    }
  }

  private IPXInterfaceField PXInterfaceField => (IPXInterfaceField) this.UIAttribute;

  /// <inheritdoc cref="P:PX.Data.IPXInterfaceField.ErrorText" />
  public string ErrorText
  {
    get => this.PXInterfaceField?.ErrorText;
    set
    {
      if (this.PXInterfaceField == null)
        return;
      this.PXInterfaceField.ErrorText = value;
    }
  }

  /// <inheritdoc cref="P:PX.Data.IPXInterfaceField.ErrorValue" />
  public object ErrorValue
  {
    get => this.PXInterfaceField?.ErrorValue;
    set
    {
      if (this.PXInterfaceField == null)
        return;
      this.PXInterfaceField.ErrorValue = value;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXUIFieldAttribute.ErrorLevel" />
  [PXInternalUseOnly]
  public PXErrorLevel ErrorLevel
  {
    get
    {
      IPXInterfaceField pxInterfaceField = this.PXInterfaceField;
      return pxInterfaceField == null ? PXErrorLevel.Undefined : pxInterfaceField.ErrorLevel;
    }
    set
    {
      if (this.PXInterfaceField == null)
        return;
      this.PXInterfaceField.ErrorLevel = value;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXUIFieldAttribute.MapEnableRights" />
  public PXCacheRights MapEnableRights
  {
    get
    {
      IPXInterfaceField pxInterfaceField = this.PXInterfaceField;
      return pxInterfaceField == null ? PXCacheRights.Select : pxInterfaceField.MapEnableRights;
    }
    set
    {
      if (this.PXInterfaceField == null)
        return;
      this.PXInterfaceField.MapEnableRights = value;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXUIFieldAttribute.MapViewRights" />
  public PXCacheRights MapViewRights
  {
    get
    {
      IPXInterfaceField pxInterfaceField = this.PXInterfaceField;
      return pxInterfaceField == null ? PXCacheRights.Select : pxInterfaceField.MapViewRights;
    }
    set
    {
      if (this.PXInterfaceField == null)
        return;
      this.PXInterfaceField.MapViewRights = value;
    }
  }

  /// <inheritdoc cref="P:PX.Data.IPXInterfaceField.ViewRights" />
  public bool ViewRights
  {
    get
    {
      IPXInterfaceField pxInterfaceField = this.PXInterfaceField;
      return pxInterfaceField == null || pxInterfaceField.ViewRights;
    }
  }

  /// <inheritdoc cref="M:PX.Data.IPXInterfaceField.ForceEnabled" />
  public void ForceEnabled() => this.PXInterfaceField?.ForceEnabled();

  protected PXSelectorAttribute NonDimensionSelectorAttribute
  {
    get
    {
      return this._SelAttrIndex != -1 ? this._Attributes[this._SelAttrIndex] as PXSelectorAttribute : (PXSelectorAttribute) null;
    }
  }

  protected PXDimensionSelectorAttribute SelectorAttribute
  {
    get
    {
      return this._SelAttrIndex != -1 ? this._Attributes[this._SelAttrIndex] as PXDimensionSelectorAttribute : (PXDimensionSelectorAttribute) null;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXSelectorAttribute.DescriptionField" />
  public virtual System.Type DescriptionField
  {
    get
    {
      System.Type descriptionField = this.NonDimensionSelectorAttribute?.DescriptionField;
      if ((object) descriptionField != null)
        return descriptionField;
      return this.SelectorAttribute?.DescriptionField;
    }
    set
    {
      if (this.NonDimensionSelectorAttribute != null)
        this.NonDimensionSelectorAttribute.DescriptionField = value;
      if (this.SelectorAttribute == null)
        return;
      this.SelectorAttribute.DescriptionField = value;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXSelectorAttribute.DirtyRead" />
  public virtual bool DirtyRead
  {
    get
    {
      PXSelectorAttribute selectorAttribute1 = this.NonDimensionSelectorAttribute;
      if (selectorAttribute1 != null)
        return selectorAttribute1.DirtyRead;
      PXDimensionSelectorAttribute selectorAttribute2 = this.SelectorAttribute;
      return selectorAttribute2 != null && selectorAttribute2.DirtyRead;
    }
    set
    {
      if (this.NonDimensionSelectorAttribute != null)
        this.NonDimensionSelectorAttribute.DirtyRead = value;
      if (this.SelectorAttribute == null)
        return;
      this.SelectorAttribute.DirtyRead = value;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXSelectorAttribute.CacheGlobal" />
  public virtual bool CacheGlobal
  {
    get
    {
      PXSelectorAttribute selectorAttribute1 = this.NonDimensionSelectorAttribute;
      if (selectorAttribute1 != null)
        return selectorAttribute1.CacheGlobal;
      PXDimensionSelectorAttribute selectorAttribute2 = this.SelectorAttribute;
      return selectorAttribute2 != null && selectorAttribute2.CacheGlobal;
    }
    set
    {
      if (this.NonDimensionSelectorAttribute != null)
        this.NonDimensionSelectorAttribute.CacheGlobal = value;
      if (this.SelectorAttribute == null)
        return;
      this.SelectorAttribute.CacheGlobal = value;
    }
  }

  /// <inheritdoc cref="F:PX.Data.PXSelectorAttribute.ValidateValue" />
  public virtual bool ValidateValue
  {
    get
    {
      PXSelectorAttribute selectorAttribute1 = this.NonDimensionSelectorAttribute;
      if (selectorAttribute1 != null)
        return selectorAttribute1.ValidateValue;
      PXDimensionSelectorAttribute selectorAttribute2 = this.SelectorAttribute;
      return selectorAttribute2 != null && selectorAttribute2.ValidateValue;
    }
    set
    {
      if (this.NonDimensionSelectorAttribute != null)
        this.NonDimensionSelectorAttribute.ValidateValue = value;
      if (this.SelectorAttribute == null)
        return;
      this.SelectorAttribute.ValidateValue = value;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXSelectorAttribute.Filterable" />
  public virtual bool Filterable
  {
    get
    {
      PXSelectorAttribute selectorAttribute1 = this.NonDimensionSelectorAttribute;
      if (selectorAttribute1 != null)
        return selectorAttribute1.Filterable;
      PXDimensionSelectorAttribute selectorAttribute2 = this.SelectorAttribute;
      return selectorAttribute2 != null && selectorAttribute2.Filterable;
    }
    set
    {
      if (this.NonDimensionSelectorAttribute != null)
        this.NonDimensionSelectorAttribute.Filterable = value;
      if (this.SelectorAttribute == null)
        return;
      this.SelectorAttribute.Filterable = value;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXDimensionSelectorAttribute.ValidComboRequired" />
  public virtual bool ValidComboRequired
  {
    get
    {
      PXDimensionSelectorAttribute selectorAttribute = this.SelectorAttribute;
      return selectorAttribute != null && selectorAttribute.ValidComboRequired;
    }
    set
    {
      if (this.SelectorAttribute == null)
        return;
      this.SelectorAttribute.ValidComboRequired = value;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXDimensionSelectorAttribute.SupportNewValues" />
  public virtual bool SupportNewValues
  {
    get
    {
      PXDimensionSelectorAttribute selectorAttribute = this.SelectorAttribute;
      return selectorAttribute != null && selectorAttribute.SupportNewValues;
    }
    set
    {
      if (this.SelectorAttribute == null)
        return;
      this.SelectorAttribute.SupportNewValues = value;
    }
  }

  protected PXDefaultAttribute DefaultAttribute
  {
    get
    {
      return this._DefAttrIndex != -1 ? (PXDefaultAttribute) this._Attributes[this._DefAttrIndex] : (PXDefaultAttribute) null;
    }
  }

  /// <inheritdoc cref="P:PX.Data.PXDefaultAttribute.PersistingCheck" />
  public virtual PXPersistingCheck PersistingCheck
  {
    get
    {
      PXDefaultAttribute defaultAttribute = this.DefaultAttribute;
      return defaultAttribute == null ? PXPersistingCheck.Nothing : defaultAttribute.PersistingCheck;
    }
    set
    {
      if (this.DefaultAttribute == null)
        return;
      this.DefaultAttribute.PersistingCheck = value;
    }
  }
}
