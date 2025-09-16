// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.Attributes.MatrixInventoryItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.IN.Matrix.DAC.Unbound;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.Matrix.Attributes;

public class MatrixInventoryItemAttribute : PXEventSubscriberAttribute, IPXRowSelectedSubscriber
{
  protected Lazy<HashSet<string>> inventoryVisibleFields;

  protected virtual Type[] GetInventoryVisibleFields()
  {
    return new Type[10]
    {
      typeof (InventoryItem.selected),
      typeof (InventoryItem.inventoryCD),
      typeof (InventoryItem.descr),
      typeof (InventoryItem.stkItem),
      typeof (InventoryItem.itemClassID),
      typeof (InventoryItem.itemType),
      typeof (InventoryItem.valMethod),
      typeof (InventoryItem.lotSerClassID),
      typeof (MatrixInventoryItem.dfltSiteID),
      typeof (InventoryItem.taxCategoryID)
    };
  }

  public MatrixInventoryItemAttribute()
  {
    this.inventoryVisibleFields = new Lazy<HashSet<string>>((Func<HashSet<string>>) (() => ((IEnumerable<Type>) this.GetInventoryVisibleFields()).Select<Type, string>((Func<Type, string>) (t => t.Name)).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)));
  }

  public virtual void CacheAttached(PXCache sender)
  {
    foreach (string field in (List<string>) sender.Fields)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      MatrixInventoryItemAttribute.\u003C\u003Ec__DisplayClass3_0 cDisplayClass30 = new MatrixInventoryItemAttribute.\u003C\u003Ec__DisplayClass3_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass30.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass30.fieldName = field;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), cDisplayClass30.fieldName, new PXFieldSelecting((object) cDisplayClass30, __methodptr(\u003CCacheAttached\u003Eb__0)));
    }
  }

  protected virtual void AnyFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    string fieldName)
  {
    if (sender.GetFieldOrdinal(fieldName) >= 0)
      return;
    PXFieldSelectingEventArgs selectingEventArgs = e;
    object returnState = e.ReturnState;
    Type type = typeof (string);
    bool? nullable1 = new bool?(false);
    bool? nullable2 = new bool?();
    bool? nullable3 = new bool?();
    int? nullable4 = new int?();
    int? nullable5 = new int?();
    int? nullable6 = new int?();
    bool? nullable7 = new bool?();
    bool? nullable8 = nullable1;
    bool? nullable9 = new bool?();
    PXFieldState instance = PXFieldState.CreateInstance(returnState, type, nullable2, nullable3, nullable4, nullable5, nullable6, (object) null, (string) null, (string) null, (string) null, (string) null, (PXErrorLevel) 0, nullable7, nullable8, nullable9, (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    selectingEventArgs.ReturnState = (object) instance;
  }

  public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    EnumerableExtensions.ForEach<PXUIFieldAttribute>(sender.GetAttributesOfType<PXUIFieldAttribute>((object) null, (string) null), (Action<PXUIFieldAttribute>) (attr =>
    {
      attr.Visible = this.inventoryVisibleFields.Value.Contains(((PXEventSubscriberAttribute) attr).FieldName);
      if (((PXEventSubscriberAttribute) attr).FieldName.Equals(typeof (InventoryItem.selected).Name, StringComparison.OrdinalIgnoreCase))
        return;
      attr.Enabled = false;
    }));
    MatrixInventoryItem row = (MatrixInventoryItem) e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetEnabled<InventoryItem.selected>(sender, (object) row, !row.Exists.GetValueOrDefault() && !row.Duplicate.GetValueOrDefault());
    bool? nullable = row.Exists;
    string str1;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.Duplicate;
      str1 = nullable.GetValueOrDefault() ? "The inventory ID is duplicated. Change segment settings of the inventory ID." : (string) null;
    }
    else
      str1 = "The item with the same inventory ID already exists. Change segment settings of the inventory ID.";
    string str2 = str1;
    PXSetPropertyException propertyException = str2 == null ? (PXSetPropertyException) null : new PXSetPropertyException(str2, (PXErrorLevel) 2);
    sender.RaiseExceptionHandling<InventoryItem.selected>((object) row, (object) false, (Exception) propertyException);
  }
}
