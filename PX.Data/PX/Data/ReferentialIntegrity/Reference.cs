// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Reference
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PX.Data.ReferentialIntegrity;

/// <summary>
/// Represents a referential relationship (or foreign key constraint) between two <see cref="T:PX.Data.IBqlTable" />s,
/// used to annotate data model and perform referential integrity check.
/// </summary>
[ImmutableObject(true)]
[DebuggerDisplay("{ToString()}")]
[PXInternalUseOnly]
public class Reference : IEquatable<Reference>
{
  private static System.Type BuildChildrenSelect(
    TableWithKeys parent,
    TableWithKeys child,
    System.Type parameterType)
  {
    return Reference.BuildSelectByKeysCommand(child.Table, child.KeyFields.Zip<System.Type, System.Type, FieldAndParameter>((IEnumerable<System.Type>) parent.KeyFields, (Func<System.Type, System.Type, FieldAndParameter>) ((ch, p) => new FieldAndParameter(ch, p, parameterType))).ToArray<FieldAndParameter>());
  }

  private static System.Type BuildParentSelect(
    TableWithKeys parent,
    TableWithKeys child,
    System.Type parameterType)
  {
    return Reference.BuildSelectByKeysCommand(parent.Table, child.KeyFields.Zip<System.Type, System.Type, FieldAndParameter>((IEnumerable<System.Type>) parent.KeyFields, (Func<System.Type, System.Type, FieldAndParameter>) ((ch, p) => new FieldAndParameter(p, ch, parameterType))).ToArray<FieldAndParameter>());
  }

  private static System.Type BuildSelectByKeysCommand(
    System.Type table,
    FieldAndParameter[] fieldsAndParameters)
  {
    return BqlCommand.Compose(typeof (Select<,>), table, fieldsAndParameters.ToWhere());
  }

  internal static Reference.ReferenceKeys[] GetReferenceKeys(
    BqlCommand selectCommand,
    System.Type parentTable,
    System.Type childTable)
  {
    return selectCommand.GetParameterPairs().Select(p => new
    {
      p = p,
      refField = p.Key
    }).Select(_param1 => new
    {
      \u003C\u003Eh__TransparentIdentifier0 = _param1,
      paramField = _param1.p.Value
    }).Select(_param1 =>
    {
      var data = _param1;
      int num;
      if (!(childTable == (System.Type) null))
      {
        System.Type itemType = BqlCommand.GetItemType(_param1.\u003C\u003Eh__TransparentIdentifier0.refField);
        num = (object) itemType != null ? (itemType.IsAssignableFrom(childTable) ? 1 : 0) : 0;
      }
      else
        num = 1;
      return new
      {
        \u003C\u003Eh__TransparentIdentifier1 = data,
        refFieldFromChild = num != 0
      };
    }).Select(_param1 =>
    {
      var data = _param1;
      System.Type itemType = BqlCommand.GetItemType(_param1.\u003C\u003Eh__TransparentIdentifier1.\u003C\u003Eh__TransparentIdentifier0.refField);
      int num = (object) itemType != null ? (itemType.IsAssignableFrom(parentTable) ? 1 : 0) : 0;
      return new
      {
        \u003C\u003Eh__TransparentIdentifier2 = data,
        refFieldFromParent = num != 0
      };
    }).Select(_param1 =>
    {
      var data = _param1;
      int num;
      if (!(childTable == (System.Type) null))
      {
        System.Type itemType = BqlCommand.GetItemType(_param1.\u003C\u003Eh__TransparentIdentifier2.\u003C\u003Eh__TransparentIdentifier1.paramField);
        num = (object) itemType != null ? (itemType.IsAssignableFrom(childTable) ? 1 : 0) : 0;
      }
      else
        num = 1;
      return new
      {
        \u003C\u003Eh__TransparentIdentifier3 = data,
        paramFieldFromChild = num != 0
      };
    }).Select(_param1 =>
    {
      var data = _param1;
      System.Type itemType = BqlCommand.GetItemType(_param1.\u003C\u003Eh__TransparentIdentifier3.\u003C\u003Eh__TransparentIdentifier2.\u003C\u003Eh__TransparentIdentifier1.paramField);
      int num = (object) itemType != null ? (itemType.IsAssignableFrom(parentTable) ? 1 : 0) : 0;
      return new
      {
        \u003C\u003Eh__TransparentIdentifier4 = data,
        paramFieldFromParent = num != 0
      };
    }).Select(_param1 => new
    {
      \u003C\u003Eh__TransparentIdentifier5 = _param1,
      isIndirect = (!_param1.\u003C\u003Eh__TransparentIdentifier4.\u003C\u003Eh__TransparentIdentifier3.\u003C\u003Eh__TransparentIdentifier2.refFieldFromChild || !_param1.paramFieldFromParent) && (!_param1.\u003C\u003Eh__TransparentIdentifier4.\u003C\u003Eh__TransparentIdentifier3.refFieldFromParent || !_param1.\u003C\u003Eh__TransparentIdentifier4.paramFieldFromChild)
    }).Where(_param1 => !_param1.isIndirect).Select(_param1 => new Reference.ReferenceKeys()
    {
      ParentField = _param1.\u003C\u003Eh__TransparentIdentifier5.\u003C\u003Eh__TransparentIdentifier4.\u003C\u003Eh__TransparentIdentifier3.\u003C\u003Eh__TransparentIdentifier2.\u003C\u003Eh__TransparentIdentifier1.\u003C\u003Eh__TransparentIdentifier0.refField,
      ChildField = _param1.\u003C\u003Eh__TransparentIdentifier5.\u003C\u003Eh__TransparentIdentifier4.\u003C\u003Eh__TransparentIdentifier3.\u003C\u003Eh__TransparentIdentifier2.\u003C\u003Eh__TransparentIdentifier1.paramField
    }).Distinct<Reference.ReferenceKeys>().ToArray<Reference.ReferenceKeys>();
  }

  /// <summary>
  /// Create <see cref="T:PX.Data.ReferentialIntegrity.Reference" /> from a Select-<see cref="T:PX.Data.BqlCommand" />
  /// </summary>
  /// <param name="selectCommand"><see cref="T:PX.Data.IBqlSelect" /> or <see cref="T:PX.Data.IBqlSearch" /> where first table suppose to be parent table</param>
  /// <param name="childTable">Child <see cref="T:PX.Data.IBqlTable" /></param>
  [PXInternalUseOnly]
  public static Reference FromParentSelect(
    BqlCommand selectCommand,
    ReferenceOrigin referenceOrigin,
    ReferenceBehavior referenceBehavior,
    System.Type childTable = null,
    System.Type parameterType = null)
  {
    System.Type firstTable = selectCommand.GetFirstTable();
    Reference.ReferenceKeys[] referenceKeys = Reference.GetReferenceKeys(selectCommand, firstTable, childTable);
    if (referenceKeys.Length == 0)
      throw new PXArgumentException(nameof (selectCommand), "ParentSelect doesn't contain parameter pairs, or its predicate starts not with a parameter pair: " + selectCommand.GetType().ToCodeString());
    System.Type type1 = childTable;
    if ((object) type1 == null)
      type1 = BqlCommand.GetItemType(((IEnumerable<Reference.ReferenceKeys>) referenceKeys).First<Reference.ReferenceKeys>().ChildField);
    childTable = type1;
    System.Type type2 = selectCommand.GetType();
    TableWithKeys parent = new TableWithKeys(firstTable, (IEnumerable<System.Type>) ((IEnumerable<Reference.ReferenceKeys>) referenceKeys).Select<Reference.ReferenceKeys, System.Type>((Func<Reference.ReferenceKeys, System.Type>) (k => k.ParentField)).ToArray<System.Type>());
    TableWithKeys child = new TableWithKeys(childTable, (IEnumerable<System.Type>) ((IEnumerable<Reference.ReferenceKeys>) referenceKeys).Select<Reference.ReferenceKeys, System.Type>((Func<Reference.ReferenceKeys, System.Type>) (k => k.ChildField)).ToArray<System.Type>());
    int num1 = (int) referenceOrigin;
    int num2 = (int) referenceBehavior;
    System.Type parameterType1 = parameterType;
    if ((object) parameterType1 == null)
      parameterType1 = typeof (Required<>);
    return new Reference(type2, parent, child, (ReferenceOrigin) num1, (ReferenceBehavior) num2, parameterType1);
  }

  public static Reference FromFieldsRelations(
    System.Type fieldsRelationsContainer,
    ReferenceOrigin origin,
    ReferenceBehavior behavior = ReferenceBehavior.NoAction,
    System.Type parameterType = null)
  {
    fieldsRelationsContainer = !(fieldsRelationsContainer == (System.Type) null) ? TypeArrayOf<IFieldsRelation>.EmptyOrSingleOrSelf(fieldsRelationsContainer) : throw new PXArgumentException(nameof (fieldsRelationsContainer), "The argument cannot be null.");
    System.Type[] source = TypeArrayOf<IFieldsRelation>.CheckAndExtract(fieldsRelationsContainer, nameof (fieldsRelationsContainer));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    IFieldsRelation[] fieldsRelationArray = ((IEnumerable<System.Type>) source).Any<System.Type>() ? ((IEnumerable<System.Type>) source).Select<System.Type, object>(Reference.\u003C\u003EO.\u003C0\u003E__CreateInstance ?? (Reference.\u003C\u003EO.\u003C0\u003E__CreateInstance = new Func<System.Type, object>(Activator.CreateInstance))).Cast<IFieldsRelation>().ToArray<IFieldsRelation>() : throw new PXArgumentException(nameof (fieldsRelationsContainer), "The passed array must contains at least one element.");
    if (((IEnumerable<IFieldsRelation>) fieldsRelationArray).Select<IFieldsRelation, System.Type>((Func<IFieldsRelation, System.Type>) (r => BqlCommand.GetItemType(r.FieldOfChildTable))).Distinct<System.Type>().Count<System.Type>() > 1)
      throw new PXInvalidFieldsRelationException("All fields from child side must belong to the same table.", origin, (IEnumerable<IFieldsRelation>) fieldsRelationArray, Array.Empty<object>());
    if (((IEnumerable<IFieldsRelation>) fieldsRelationArray).Select<IFieldsRelation, System.Type>((Func<IFieldsRelation, System.Type>) (r => BqlCommand.GetItemType(r.FieldOfParentTable))).Distinct<System.Type>().Count<System.Type>() > 1)
      throw new PXInvalidFieldsRelationException("All fields from parent side must belong to the same table.", origin, (IEnumerable<IFieldsRelation>) fieldsRelationArray, Array.Empty<object>());
    System.Type itemType1 = BqlCommand.GetItemType(((IEnumerable<IFieldsRelation>) fieldsRelationArray).First<IFieldsRelation>().FieldOfParentTable);
    System.Type itemType2 = BqlCommand.GetItemType(((IEnumerable<IFieldsRelation>) fieldsRelationArray).First<IFieldsRelation>().FieldOfChildTable);
    IEnumerable<System.Type> keyFields = ((IEnumerable<IFieldsRelation>) fieldsRelationArray).Select<IFieldsRelation, System.Type>((Func<IFieldsRelation, System.Type>) (r => r.FieldOfParentTable));
    return new Reference(new TableWithKeys(itemType1, keyFields), new TableWithKeys(itemType2, ((IEnumerable<IFieldsRelation>) fieldsRelationArray).Select<IFieldsRelation, System.Type>((Func<IFieldsRelation, System.Type>) (r => r.FieldOfChildTable))), origin, behavior, parameterType);
  }

  /// <summary>
  /// Instantiate <see cref="T:PX.Data.ReferentialIntegrity.Reference" /> between parent and child tables
  /// </summary>
  internal Reference(
    TableWithKeys parent,
    TableWithKeys child,
    ReferenceOrigin referenceOrigin,
    ReferenceBehavior referenceBehavior,
    System.Type parameterType = null)
  {
    System.Type type = parameterType;
    if ((object) type == null)
      type = typeof (Required<>);
    parameterType = type;
    this.Parent = parent;
    this.Child = child;
    this.ReferenceOrigin = referenceOrigin;
    this.ReferenceBehavior = referenceBehavior;
    this.ParentSelect = Reference.BuildParentSelect(parent, child, parameterType);
    this.ChildrenSelect = Reference.BuildChildrenSelect(parent, child, parameterType);
  }

  /// <summary>
  /// Instantiate <see cref="T:PX.Data.ReferentialIntegrity.Reference" /> between parent and child tables with manual selects setting
  /// </summary>
  internal Reference(
    TableWithKeys parent,
    TableWithKeys child,
    ReferenceOrigin referenceOrigin,
    ReferenceBehavior referenceBehavior,
    System.Type parentSelect,
    System.Type childrenSelect,
    System.Type parameterType = null)
  {
    System.Type type1 = parameterType;
    if ((object) type1 == null)
      type1 = typeof (Required<>);
    parameterType = type1;
    this.Parent = parent;
    this.Child = child;
    this.ReferenceOrigin = referenceOrigin;
    this.ReferenceBehavior = referenceBehavior;
    System.Type type2 = parentSelect;
    if ((object) type2 == null)
      type2 = Reference.BuildParentSelect(parent, child, typeof (Required<>));
    this.ParentSelect = type2;
    System.Type type3 = childrenSelect;
    if ((object) type3 == null)
      type3 = Reference.BuildChildrenSelect(parent, child, typeof (Required<>));
    this.ChildrenSelect = type3;
  }

  /// <summary>
  /// Instantiate <see cref="T:PX.Data.ReferentialIntegrity.Reference" /> between parent and child tables with auto inference of both selects
  /// </summary>
  private Reference(
    System.Type originalSelect,
    TableWithKeys parent,
    TableWithKeys child,
    ReferenceOrigin referenceOrigin,
    ReferenceBehavior referenceBehavior,
    System.Type parameterType)
    : this(parent, child, referenceOrigin, referenceBehavior, Reference.BuildParentSelect(parent, child, parameterType), Reference.BuildChildrenSelect(parent, child, parameterType))
  {
    this.OriginalSelect = originalSelect;
  }

  /// <summary>
  /// Parent <see cref="T:PX.Data.IBqlTable" /> with its key-fields
  /// </summary>
  public TableWithKeys Parent { get; private set; }

  /// <summary>
  /// Child <see cref="T:PX.Data.IBqlTable" /> with its key-fields
  /// </summary>
  public TableWithKeys Child { get; private set; }

  /// <summary>
  /// Indicates how referential relationship between DACs is achieved
  /// </summary>
  public ReferenceOrigin ReferenceOrigin { get; private set; }

  /// <summary>
  /// Indicates how referential relationship between DACs is handled
  /// </summary>
  public ReferenceBehavior ReferenceBehavior { get; private set; }

  /// <summary>Select-statement for selecting parent entities</summary>
  public System.Type ParentSelect { get; private set; }

  /// <summary>Select-statement for selecting child entities</summary>
  public System.Type ChildrenSelect { get; private set; }

  /// <summary>
  /// Select-statement from which current reference was inferred
  /// </summary>
  /// <remarks>Meaning only for <see cref="T:PX.Data.ReferentialIntegrity.Reference" /> instances that were obtained by inference from a select-statement</remarks>
  public System.Type OriginalSelect { get; private set; }

  /// <summary>
  /// Represents a mapper from child table fields to parent table fields
  /// </summary>
  public ReadOnlyDictionary<System.Type, System.Type> FieldMap
  {
    get
    {
      TableWithKeys tableWithKeys = this.Child;
      IReadOnlyCollection<System.Type> keyFields1 = tableWithKeys.KeyFields;
      tableWithKeys = this.Parent;
      IReadOnlyCollection<System.Type> keyFields2 = tableWithKeys.KeyFields;
      return EnumerableExtensions.AsReadOnly<System.Type, System.Type>((IDictionary<System.Type, System.Type>) EnumerableExtensions.ToDictionary<System.Type, System.Type>(keyFields1.Zip<System.Type, System.Type, KeyValuePair<System.Type, System.Type>>((IEnumerable<System.Type>) keyFields2, (Func<System.Type, System.Type, KeyValuePair<System.Type, System.Type>>) ((c, p) => new KeyValuePair<System.Type, System.Type>(c, p)))));
    }
  }

  /// <summary>
  /// Indicates whether <see cref="P:PX.Data.ReferentialIntegrity.Reference.Child" /> is referencing to itself by the same set of fields (e.g. BAccount(acctCD) references BAccount(acctCD))
  /// </summary>
  public bool TableReferencesToItself => this.Child == this.Parent;

  /// <summary>
  /// Represents a <see cref="T:PX.Data.IBqlOn" />-clause that describes a rule of joining the child table to the parent table
  /// </summary>
  public System.Type ToOnClause()
  {
    return BqlCommand.Compose(((IEnumerable<System.Type>) BqlCommand.Decompose(this.ChildrenSelect)).Skip<System.Type>(3).Prepend<System.Type>(this.Child.KeyFields.Count == 1 ? typeof (On<,>) : typeof (On<,,>)).Where<System.Type>((Func<System.Type, bool>) (t => !typeof (IBqlParameter).IsAssignableFrom(t))).ToArray<System.Type>());
  }

  public override string ToString()
  {
    return $"Foreign key {this.Child} references {this.Parent}, with {this.ReferenceBehavior}-behavior, achieved by {this.ReferenceOrigin}";
  }

  internal object SelectParentImpl(PXGraph graph, params object[] keys)
  {
    return this.SelectParentImpl(graph, true, keys);
  }

  internal object SelectParentImpl(PXGraph graph, bool IsReadOnly, params object[] keys)
  {
    return graph.TypedViews.GetView(BqlCommand.CreateInstance(this.ParentSelect), (IsReadOnly ? 1 : 0) != 0).SelectSingle(keys);
  }

  public object SelectParent(PXGraph graph, object childRow)
  {
    if (childRow == null)
      return (object) null;
    if (!this.Child.Table.IsAssignableFrom(childRow.GetType()))
      throw new ArgumentException("Type of parameter should be assignable from reference's child type.", nameof (childRow));
    PXCache childCache = graph.Caches[this.Child.Table];
    object[] array = this.Child.KeyFields.Select<System.Type, object>((Func<System.Type, object>) (k => childCache.GetValue(childRow, k.Name))).ToArray<object>();
    return this.SelectParentImpl(graph, array);
  }

  public IEnumerable<object> SelectChildren(PXGraph graph, object parentRow)
  {
    return this.SelectChildren(graph, parentRow, true);
  }

  public IEnumerable<object> SelectChildren(PXGraph graph, object parentRow, bool IsReadOnly)
  {
    if (parentRow == null || !this.Parent.Table.IsAssignableFrom(parentRow.GetType()))
      throw new ArgumentException("Type of parameter should be assignable from reference's parent type.", nameof (parentRow));
    PXCache parentCache = graph.Caches[this.Parent.Table];
    return (IEnumerable<object>) graph.TypedViews.GetView(BqlCommand.CreateInstance(this.ChildrenSelect), (IsReadOnly ? 1 : 0) != 0).SelectMulti(this.Parent.KeyFields.Select<System.Type, object>((Func<System.Type, object>) (k => parentCache.GetValue(parentRow, k.Name))).ToArray<object>());
  }

  public bool Match(PXGraph graph, object parentRow, object childRow)
  {
    if (parentRow == null || !this.Parent.Table.IsAssignableFrom(parentRow.GetType()))
      throw new ArgumentException("Type of parameter should be assignable from reference's parent type.", nameof (parentRow));
    PXCache parentCache = graph.Caches[this.Parent.Table];
    if (childRow == null || !this.Child.Table.IsAssignableFrom(childRow.GetType()))
      throw new ArgumentException("Type of parameter should be assignable from reference's child type.", nameof (childRow));
    PXCache childCache = graph.Caches[this.Child.Table];
    TableWithKeys tableWithKeys = this.Parent;
    IEnumerable<object> first = tableWithKeys.KeyFields.Select<System.Type, object>((Func<System.Type, object>) (k => parentCache.GetValue(parentRow, k.Name)));
    tableWithKeys = this.Child;
    IEnumerable<object> second = tableWithKeys.KeyFields.Select<System.Type, object>((Func<System.Type, object>) (k => childCache.GetValue(childRow, k.Name)));
    return first.SequenceEqual<object>(second);
  }

  public bool Equals(Reference other)
  {
    if ((object) other == null)
      return false;
    if ((object) this == (object) other)
      return true;
    return object.Equals((object) this.Parent, (object) other.Parent) && object.Equals((object) this.Child, (object) other.Child) && this.ReferenceOrigin == other.ReferenceOrigin && this.ReferenceBehavior == other.ReferenceBehavior;
  }

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if ((object) this == obj)
      return true;
    return !(obj.GetType() != this.GetType()) && this.Equals((Reference) obj);
  }

  public override int GetHashCode()
  {
    TableWithKeys tableWithKeys = this.Parent;
    int num = tableWithKeys.GetHashCode() * 397;
    tableWithKeys = this.Child;
    int hashCode = tableWithKeys.GetHashCode();
    return (int) ((ReferenceBehavior) ((int) ((ReferenceOrigin) ((num ^ hashCode) * 397) ^ this.ReferenceOrigin) * 397) ^ this.ReferenceBehavior);
  }

  public static bool operator ==(Reference left, Reference right)
  {
    return object.Equals((object) left, (object) right);
  }

  public static bool operator !=(Reference left, Reference right)
  {
    return !object.Equals((object) left, (object) right);
  }

  [PXLocalizable]
  public static class Messages
  {
    public const string ArrayShouldContainsAtLeastOneElement = "The passed array must contains at least one element.";
    public const string AllFieldsFromChildSideShouldBelongToTheSameType = "All fields from child side must belong to the same table.";
    public const string AllFieldsFromParentSideShouldBelongToTheSameType = "All fields from parent side must belong to the same table.";
    public const string ChildTableShouldBeTheSameAsTargetedClass = "The incorrect child table {1} was provided. The child table must be the same as the target class {0} or one of its descendants.";
  }

  internal class ReferenceKeys
  {
    public System.Type ParentField;
    public System.Type ChildField;
  }
}
