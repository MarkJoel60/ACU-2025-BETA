// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBBaseIDAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>Base class for PXDB***ByID attributes</summary>
[PXDBGuid(false)]
[Serializable]
public abstract class PXDBBaseIDAttribute : PXAggregateAttribute, IPXFieldVerifyingSubscriber
{
  protected int _DBGuidAttrIndex = -1;
  protected int _DisplSelAttrIndex = -1;
  protected int _UIFldAttrIndex = -1;

  /// <summary>Returns <tt>null</tt> on get. Sets the BQL field representing
  /// the field in BQL queries.</summary>
  public System.Type BqlField
  {
    get => (System.Type) null;
    set
    {
      if (this._DBGuidAttrIndex < 0)
        return;
      ((PXDBFieldAttribute) this._Attributes[this._DBGuidAttrIndex]).BqlField = value;
      this.BqlTable = this._Attributes[this._DBGuidAttrIndex].BqlTable;
    }
  }

  public string DisplayName
  {
    get
    {
      return this._UIFldAttrIndex != -1 ? ((PXUIFieldAttribute) this._Attributes[this._UIFldAttrIndex]).DisplayName : (string) null;
    }
    set
    {
      if (this._UIFldAttrIndex == -1)
        return;
      ((PXUIFieldAttribute) this._Attributes[this._UIFldAttrIndex]).DisplayName = value;
    }
  }

  public PXUIVisibility Visibility
  {
    get
    {
      return this._UIFldAttrIndex != -1 ? ((PXUIFieldAttribute) this._Attributes[this._UIFldAttrIndex]).Visibility : PXUIVisibility.Undefined;
    }
    set
    {
      if (this._UIFldAttrIndex == -1)
        return;
      ((PXUIFieldAttribute) this._Attributes[this._UIFldAttrIndex]).Visibility = value;
    }
  }

  public bool Visible
  {
    get
    {
      return this._UIFldAttrIndex == -1 || ((PXUIFieldAttribute) this._Attributes[this._UIFldAttrIndex]).Visible;
    }
    set
    {
      if (this._UIFldAttrIndex == -1)
        return;
      ((PXUIFieldAttribute) this._Attributes[this._UIFldAttrIndex]).Visible = value;
    }
  }

  protected Guid GetUserID(PXCache sender) => PXAccess.GetTrueUserID();

  internal PXDBBaseIDAttribute(
    System.Type search,
    System.Type substituteKey,
    System.Type descriptionField,
    params System.Type[] fields)
  {
    PXAggregateAttribute.AggregatedAttributesCollection attributes = this._Attributes;
    PXDBBaseIDAttribute.AuditUserSelectorAttribute selectorAttribute = new PXDBBaseIDAttribute.AuditUserSelectorAttribute(search, fields);
    selectorAttribute.DescriptionField = descriptionField;
    selectorAttribute.SubstituteKey = substituteKey;
    selectorAttribute.CacheGlobal = true;
    attributes.Add((PXEventSubscriberAttribute) selectorAttribute);
    this._DisplSelAttrIndex = this._Attributes.Count - 1;
    this._DBGuidAttrIndex = this._Attributes.FindIndex((Predicate<PXEventSubscriberAttribute>) (attr => attr is PXDBGuidAttribute));
    this._UIFldAttrIndex = this._Attributes.FindIndex((Predicate<PXEventSubscriberAttribute>) (attr => attr is PXUIFieldAttribute));
  }

  /// <summary>Gets or sets the value that indicates whether a field update
  /// is allowed after the field value is set for the first time.</summary>
  public bool DontOverrideValue { get; set; }

  void IPXFieldVerifyingSubscriber.FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    try
    {
      if (this._DisplSelAttrIndex < 0)
        return;
      ((IPXFieldVerifyingSubscriber) this._Attributes[this._DisplSelAttrIndex]).FieldVerifying(sender, e);
    }
    catch (PXSetPropertyException ex)
    {
      e.NewValue = (object) this.GetUserID(sender);
    }
  }

  /// <exclude />
  public override void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers)
  {
    base.GetSubscriber<ISubscriber>(subscribers);
    if (!(typeof (ISubscriber) == typeof (IPXFieldVerifyingSubscriber)) || this._DisplSelAttrIndex < 0)
      return;
    subscribers.Remove(this._Attributes[this._DisplSelAttrIndex] as ISubscriber);
  }

  public void AddUIFieldAttributeIfNeeded(string displayName)
  {
    if (this._UIFldAttrIndex < 0)
    {
      this._Attributes.Add((PXEventSubscriberAttribute) new PXUIFieldAttribute()
      {
        Enabled = false,
        Visible = true,
        IsReadOnly = true
      });
      this._UIFldAttrIndex = this._Attributes.Count - 1;
    }
    (this._Attributes[this._UIFldAttrIndex] as PXUIFieldAttribute).DisplayName = displayName;
  }

  internal class AuditUserSelectorAttribute : PXDisplaySelectorAttribute
  {
    public AuditUserSelectorAttribute(System.Type type)
      : base(type)
    {
    }

    public AuditUserSelectorAttribute(System.Type type, params System.Type[] fieldList)
      : base(type, fieldList)
    {
    }

    protected override void EmitColumnForDescriptionField(PXCache sender)
    {
      base.EmitColumnForDescriptionField(sender);
      this.EmitDescriptionFieldAlias(sender, $"{this._FieldName}_{this._Type.Name}_Username");
    }

    public override void DescriptionFieldCommandPreparing(
      PXCache sender,
      PXCommandPreparingEventArgs e)
    {
      base.DescriptionFieldCommandPreparing(sender, e);
      Query q = e.Expr is SubQuery expr ? expr.Query() : (Query) null;
      if (q != null && q.GetSelection().Count > 0 && (q.GetSelection()[0] is Column column ? (column.Name.Equals("displayName", StringComparison.OrdinalIgnoreCase) ? 1 : 0) : 0) != 0)
      {
        SimpleTable t = new SimpleTable(this._Type.Name + "Ext");
        SQLSwitch field = new SQLSwitch().Case(new Column("FirstName", (Table) t).IsNull(), (SQLExpression) new SQLSwitch().Case(new Column("LastName", (Table) t).IsNull(), (SQLExpression) new Column("UserName", (Table) t)).Default((SQLExpression) new Column("LastName", (Table) t))).Default((SQLExpression) new SQLSwitch().Case(new Column("LastName", (Table) t).IsNull(), (SQLExpression) new Column("FirstName", (Table) t)).Default(new Column("FirstName", (Table) t).Concat((SQLExpression) new SQLConst((object) " ")).Concat((SQLExpression) new Column("LastName", (Table) t))));
        q.ClearSelection();
        q.Field((SQLExpression) field);
        e.Expr = (SQLExpression) new SubQuery(q);
      }
      if (q == null || e.Operation != (PXDBOperation.External | PXDBOperation.WhereClause))
        return;
      q.IncludeRemovedRecords = true;
    }
  }
}
