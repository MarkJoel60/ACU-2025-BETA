// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.DenormalizedFromAttribute
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
namespace PX.Objects.Common.Attributes;

/// <summary>
/// The attribute handles the fields which need to be denormalized from parent to children.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
public class DenormalizedFromAttribute : PXEventSubscriberAttribute
{
  protected Type[] _parentFieldTypes;
  protected string[] _childFieldNames;
  protected object[] _defaultValues;
  protected Type _childToParentLinkField;

  protected string GetChildFieldName(int i) => this._childFieldNames[i] ?? this.FieldName;

  /// <summary>Constructor of the attribute.</summary>
  /// <param name="parentFieldType">The field from parent which needs to be denormalized.</param>
  /// <param name="childToParentLinkField">The field which is the link from child to parent.</param>
  public DenormalizedFromAttribute(Type parentFieldType, Type childToParentLinkField = null)
  {
    this._parentFieldTypes = typeof (IBqlField).IsAssignableFrom(parentFieldType) && typeof (IBqlTable).IsAssignableFrom(BqlCommand.GetItemType(parentFieldType)) ? new Type[1]
    {
      parentFieldType
    } : throw new PXArgumentException(nameof (parentFieldType));
    this._childFieldNames = new string[1];
    this._childToParentLinkField = childToParentLinkField;
  }

  /// <summary>Constructor of the attribute.</summary>
  /// <param name="parentTypes">The collection of the fields from parent which needs to be denormalized.</param>
  /// <param name="childTypes">The corresponding collection of the fields in children.</param>
  /// <param name="defaultValues">Default values for children in case if parent is not found.</param>
  /// <param name="childToParentLinkField">The field which is the link from child to parent.</param>
  public DenormalizedFromAttribute(
    Type[] parentTypes,
    Type[] childTypes,
    object[] defaultValues = null,
    Type childToParentLinkField = null)
  {
    if (parentTypes == null || parentTypes.Length == 0)
      throw new PXArgumentException(nameof (parentTypes));
    if (childTypes == null || childTypes.Length != parentTypes.Length)
      throw new PXArgumentException(nameof (childTypes));
    if (defaultValues != null && defaultValues.Length != parentTypes.Length)
      throw new PXArgumentException(nameof (defaultValues));
    this._parentFieldTypes = parentTypes;
    this._childFieldNames = ((IEnumerable<Type>) childTypes).Select<Type, string>((Func<Type, string>) (t => t.Name)).ToArray<string>();
    this._defaultValues = defaultValues;
    this._childToParentLinkField = childToParentLinkField;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    Type parentFieldType = this._parentFieldTypes[0];
    PXGraph.RowUpdatedEvents rowUpdated = sender.Graph.RowUpdated;
    Type itemType1 = BqlCommand.GetItemType(parentFieldType);
    DenormalizedFromAttribute denormalizedFromAttribute1 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) denormalizedFromAttribute1, __vmethodptr(denormalizedFromAttribute1, Parent_RowUpdated));
    rowUpdated.AddHandler(itemType1, pxRowUpdated);
    for (int index = 0; index < this._childFieldNames.Length; ++index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      DenormalizedFromAttribute.\u003C\u003Ec__DisplayClass7_0 cDisplayClass70 = new DenormalizedFromAttribute.\u003C\u003Ec__DisplayClass7_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass70.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass70.index = index;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      sender.Graph.FieldDefaulting.AddHandler(sender.GetItemType(), this.GetChildFieldName(cDisplayClass70.index), new PXFieldDefaulting((object) cDisplayClass70, __methodptr(\u003CCacheAttached\u003Eb__0)));
    }
    if (!(this._childToParentLinkField != (Type) null))
      return;
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType2 = BqlCommand.GetItemType(this._childToParentLinkField);
    string name = this._childToParentLinkField.Name;
    DenormalizedFromAttribute denormalizedFromAttribute2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) denormalizedFromAttribute2, __vmethodptr(denormalizedFromAttribute2, Child_ParentLinkUpdated));
    fieldUpdated.AddHandler(itemType2, name, pxFieldUpdated);
  }

  protected virtual object GetParentValue(Type parentField, PXCache sender, object child)
  {
    Type itemType = BqlCommand.GetItemType(parentField);
    object obj = PXParentAttribute.SelectParent(sender, child, itemType);
    return sender.Graph.Caches[itemType].GetValue(obj, parentField.Name);
  }

  public virtual void Child_ParentLinkUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    for (int i = 0; i < this._parentFieldTypes.Length; ++i)
      sender.SetValueExt(e.Row, this.GetChildFieldName(i), this.GetParentValue(this._parentFieldTypes[i], sender, e.Row));
  }

  public virtual void Child_FieldDefaulting(
    int index,
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    Type parentFieldType = this._parentFieldTypes[index];
    e.NewValue = this.GetParentValue(parentFieldType, sender, e.Row) ?? this._defaultValues?[index];
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void Parent_RowUpdated(PXCache parentCache, PXRowUpdatedEventArgs e)
  {
    bool flag = false;
    for (int index = 0; index < this._parentFieldTypes.Length; ++index)
    {
      if (!object.Equals(parentCache.GetValue(e.Row, this._parentFieldTypes[index].Name), parentCache.GetValue(e.OldRow, this._parentFieldTypes[index].Name)))
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      return;
    PXCache cach = parentCache.Graph.Caches[this.BqlTable];
    foreach (object selectChild in PXParentAttribute.SelectChildren(cach, e.Row, parentCache.GetItemType()))
    {
      object copy = cach.CreateCopy(selectChild);
      for (int i = 0; i < this._parentFieldTypes.Length; ++i)
      {
        object obj = parentCache.GetValue(e.Row, this._parentFieldTypes[i].Name);
        cach.SetValue(copy, this.GetChildFieldName(i), obj);
      }
      cach.Update(copy);
    }
  }
}
