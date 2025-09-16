// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.PXDBDefaultBankTaxZoneAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common.Attributes;

public class PXDBDefaultBankTaxZoneAttribute(Type sourceType) : PXDBDefaultAttribute(sourceType)
{
  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (((e.Operation & 3) == 2 && this._DefaultForInsert || (e.Operation & 3) == 1 && this._DefaultForUpdate) && this._SourceType != (Type) null)
    {
      this.EnsureIsRestriction(sender);
      if (this._IsRestriction.Value.GetValueOrDefault())
      {
        object key1 = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
        if ((e.Operation & 3) == 2 && !this._DoubleDefaultAttribute && (key1 is string && ((string) key1).StartsWith(" ", StringComparison.InvariantCultureIgnoreCase) || key1 is int num1 && num1 < 0 || key1 is long num2 && num2 < 0L))
          sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) null);
        object obj;
        if (this._IsRestriction.Persisted != null && key1 != null && this._IsRestriction.Persisted.TryGetValue(key1, out obj))
        {
          object key2 = sender.Graph.Caches[this._SourceType].GetValue(obj, this._SourceField ?? ((PXEventSubscriberAttribute) this)._FieldName);
          sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, key2);
          if (key2 != null)
            this._IsRestriction.Persisted[key2] = obj;
        }
      }
      else
      {
        sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) null);
        if (this._Select != null)
        {
          List<object> objectList = sender.Graph.TypedViews.GetView(this._Select, false).SelectMultiBound(new object[1]
          {
            e.Row
          }, Array.Empty<object>());
          if (objectList != null && objectList.Count > 0)
          {
            object obj = objectList[objectList.Count - 1];
            if (obj is PXResult)
              obj = ((PXResult) obj)[0];
            sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, sender.Graph.Caches[this._SourceType].GetValue(obj, this._SourceField ?? ((PXEventSubscriberAttribute) this)._FieldName));
          }
        }
        else if (this._SourceType != (Type) null)
        {
          object obj = PXParentAttribute.SelectParent(sender, e.Row, this._SourceType);
          PXCache cach = sender.Graph.Caches[this._SourceType];
          if (obj != null)
            sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, cach.GetValue(obj, this._SourceField ?? ((PXEventSubscriberAttribute) this)._FieldName));
        }
      }
    }
    if (this.PersistingCheck == 2 || ((e.Operation & 3) != 2 || !this._DefaultForInsert) && ((e.Operation & 3) != 1 || !this._DefaultForUpdate) || sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal) != null)
      return;
    if (sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, (object) null, (Exception) new PXSetPropertyKeepPreviousException(PXMessages.LocalizeFormat("'{0}' cannot be empty.", new object[1]
    {
      (object) $"[{((PXEventSubscriberAttribute) this)._FieldName}]"
    }))))
      throw new PXRowPersistingException(((PXEventSubscriberAttribute) this)._FieldName, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) ((PXEventSubscriberAttribute) this)._FieldName
      });
  }
}
