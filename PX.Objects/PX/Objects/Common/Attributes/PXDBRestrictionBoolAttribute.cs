// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.PXDBRestrictionBoolAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.SQLTree;
using System;

#nullable disable
namespace PX.Objects.Common.Attributes;

/// <exclude />
public class PXDBRestrictionBoolAttribute : PXBoolAttribute, IPXRowPersistedSubscriber
{
  protected string _RelatedDatabaseFieldName;
  protected Type _RelatedBqlField;

  public PXDBRestrictionBoolAttribute(Type relatedBqlField)
  {
    this.RelatedBqlField = relatedBqlField;
  }

  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    base.CommandPreparing(sender, e);
    int num = (e.Operation & 3) == 2 ? 1 : 0;
    bool flag = (e.Operation & 3) == 1;
    if (num != 0)
    {
      e.ExcludeFromInsertUpdate();
    }
    else
    {
      if (!flag)
        return;
      e.DataType = (PXDbType) 2;
      e.DataLength = new int?(1);
      e.BqlTable = ((PXEventSubscriberAttribute) this)._BqlTable;
      Type type = e.Table == (Type) null ? ((PXEventSubscriberAttribute) this)._BqlTable : e.Table;
      e.Expr = (SQLExpression) new Column(this._RelatedDatabaseFieldName, (Table) new SimpleTable(type, (string) null), e.DataType);
      e.IsRestriction = true;
      e.Value = e.DataValue = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) ?? sender.GetValue(e.Row, this.RelatedBqlField.Name);
    }
  }

  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.Row == null || !EnumerableExtensions.IsIn<PXTranStatus>(e.TranStatus, (PXTranStatus) 1, (PXTranStatus) 2))
      return;
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
  }

  protected virtual Type RelatedBqlField
  {
    get => this._RelatedBqlField;
    set
    {
      this._RelatedBqlField = value;
      this._RelatedDatabaseFieldName = char.ToUpper(value.Name[0]).ToString() + value.Name.Substring(1);
      if (!value.IsNested)
        return;
      if (value.DeclaringType.IsDefined(typeof (PXTableAttribute), true))
        ((PXEventSubscriberAttribute) this).BqlTable = value.DeclaringType;
      else
        ((PXEventSubscriberAttribute) this).BqlTable = BqlCommand.GetItemType(value);
    }
  }
}
