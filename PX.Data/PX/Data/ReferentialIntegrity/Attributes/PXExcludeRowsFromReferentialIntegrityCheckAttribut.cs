// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.PXExcludeRowsFromReferentialIntegrityCheckAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.ReferentialIntegrity.Merging;
using PX.Metadata;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes;

/// <summary>
/// Excludes certain rows of child-<see cref="T:PX.Data.IBqlTable" /> from referential
/// integrity check by using the <see cref="P:PX.Data.ReferentialIntegrity.Attributes.PXExcludeRowsFromReferentialIntegrityCheckAttribute.CurrentTableExcludingCondition" />
/// and <see cref="P:PX.Data.ReferentialIntegrity.Attributes.PXExcludeRowsFromReferentialIntegrityCheckAttribute.ForeignTableExcludingConditions" />.<para />
/// Child-<see cref="T:PX.Data.IBqlTable" /> can has many parent-specific conditions,
/// and only one general condition (where <see cref="P:PX.Data.ReferentialIntegrity.Attributes.PXExcludeRowsFromReferentialIntegrityCheckAttribute.SpecificParent" /> is no set).<para />
/// To include <see cref="T:PX.Data.IBqlTable" />s in referential integrity check
/// declare a <see cref="T:PX.Data.ReferentialIntegrity.Reference" /> by using one of the following attributes:
/// <see cref="T:PX.Data.ReferentialIntegrity.Attributes.PXForeignReferenceAttribute" /> or <see cref="T:PX.Data.PXParentAttribute" /> (on child side),
/// or <see cref="T:PX.Data.ReferentialIntegrity.Attributes.PXReferentialIntegrityCheckAttribute" /> (on parent or child side).
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
public class PXExcludeRowsFromReferentialIntegrityCheckAttribute : PXEventSubscriberAttribute
{
  private static readonly ConcurrentDictionary<System.Type, PXExcludeRowsFromReferentialIntegrityCheckAttribute> GeneralExcludingConditionCache = new ConcurrentDictionary<System.Type, PXExcludeRowsFromReferentialIntegrityCheckAttribute>();
  private static readonly ConcurrentDictionary<PXExcludeRowsFromReferentialIntegrityCheckAttribute.ChildAndParent, PXExcludeRowsFromReferentialIntegrityCheckAttribute> SpecificExcludingConditionCache = new ConcurrentDictionary<PXExcludeRowsFromReferentialIntegrityCheckAttribute.ChildAndParent, PXExcludeRowsFromReferentialIntegrityCheckAttribute>();
  private System.Type _currentTableExcludingCondition;
  private System.Type _specificParent;
  private System.Type _externalTableConditions;

  [InjectDependencyOnTypeLevel]
  public ITableMergedReferencesInspector TableMergedReferencesInspector { get; set; }

  public static BqlCommand AppendExcludingCondition(
    BqlCommand command,
    System.Type childTable,
    System.Type parentTable)
  {
    if (command == null)
      throw new ArgumentNullException(nameof (command));
    PXExcludeRowsFromReferentialIntegrityCheckAttribute.ChildAndParent key = new PXExcludeRowsFromReferentialIntegrityCheckAttribute.ChildAndParent(childTable, parentTable);
    if (!PXExcludeRowsFromReferentialIntegrityCheckAttribute.SpecificExcludingConditionCache.ContainsKey(key))
      PXExcludeRowsFromReferentialIntegrityCheckAttribute.VerifyArguments(childTable, parentTable);
    DacMetadata.InitializationCompleted.Wait();
    PXExcludeRowsFromReferentialIntegrityCheckAttribute integrityCheckAttribute1;
    if (PXExcludeRowsFromReferentialIntegrityCheckAttribute.GeneralExcludingConditionCache.TryGetValue(key.Child, out integrityCheckAttribute1))
      command = integrityCheckAttribute1.Apply(command);
    PXExcludeRowsFromReferentialIntegrityCheckAttribute integrityCheckAttribute2;
    if (PXExcludeRowsFromReferentialIntegrityCheckAttribute.SpecificExcludingConditionCache.TryGetValue(key, out integrityCheckAttribute2))
      command = integrityCheckAttribute2.Apply(command);
    return command;
  }

  private static void VerifyArguments(System.Type childTable, System.Type parentTable)
  {
    if (childTable == (System.Type) null)
      throw new PXArgumentException(nameof (childTable), "The argument cannot be null.");
    if (parentTable == (System.Type) null)
      throw new PXArgumentException(nameof (parentTable), "The argument cannot be null.");
    if (!typeof (IBqlTable).IsAssignableFrom(childTable))
      throw new PXArgumentException(nameof (childTable), "The assigned type must implement the {0} interface.", new object[2]
      {
        (object) "IBqlTable",
        (object) nameof (childTable)
      });
    if (!typeof (IBqlTable).IsAssignableFrom(parentTable))
      throw new PXArgumentException(nameof (parentTable), "The assigned type must implement the {0} interface.", new object[2]
      {
        (object) "IBqlTable",
        (object) nameof (parentTable)
      });
  }

  /// <summary>Indicates whether attribute has a valid configuration</summary>
  public bool IsValid
  {
    get
    {
      return this.CurrentTableExcludingCondition != (System.Type) null || ((IEnumerable<System.Type>) TypeArrayOf<IByForeignTableExcludingCondition>.CheckAndExtract(this.ForeignTableExcludingConditions, (string) null)).Any<System.Type>();
    }
  }

  /// <summary>
  /// Condition (<see cref="T:PX.Data.IBqlWhere" />-clause) that determines if a certain row
  /// should be excluded from referential integrity check based on the row itself
  /// </summary>
  public System.Type CurrentTableExcludingCondition
  {
    get => this._currentTableExcludingCondition;
    set
    {
      this._currentTableExcludingCondition = !(value != (System.Type) null) || typeof (IBqlWhere).IsAssignableFrom(value) ? value : throw new PXArgumentException(nameof (value), "The assigned type must implement the {0} interface.", new object[1]
      {
        (object) "IBqlWhere"
      });
    }
  }

  /// <summary>
  /// Represents a single or a set of excluding conditions (<see cref="T:PX.Data.IBqlWhere" />-clauses) targeted on external
  /// <see cref="T:PX.Data.IBqlTable" />, that left-joined to the row by the defined <see cref="T:PX.Data.IBqlOn" />-expression.<para />
  /// If any of conditions is met for a certain row - it would be excluded from referential integrity check.<para />
  /// Should be a single a <see cref="T:PX.Data.ReferentialIntegrity.Attributes.ExcludeWhen`1.Joined`1.Satisfies`1" />
  /// or a <see cref="T:PX.Data.ReferentialIntegrity.Attributes.ExcludeWhen`1.JoinedAsParent.Satisfies`1" /> type specification,
  /// or an <see cref="T:PX.Data.ReferentialIntegrity.Attributes.ExcludingConditionArray" /> filled with such types.
  /// </summary>
  public System.Type ForeignTableExcludingConditions
  {
    get
    {
      System.Type externalTableConditions = this._externalTableConditions;
      return (object) externalTableConditions != null ? externalTableConditions : typeof (TypeArrayOf<IByForeignTableExcludingCondition>.Empty);
    }
    set
    {
      this._externalTableConditions = TypeArrayOf<IByForeignTableExcludingCondition>.EmptyOrSingleOrSelf(value);
    }
  }

  /// <summary>
  /// The only parent-<see cref="T:PX.Data.IBqlTable" /> for which the passed conditions are meaningful to.<para />
  /// If <see cref="P:PX.Data.ReferentialIntegrity.Attributes.PXExcludeRowsFromReferentialIntegrityCheckAttribute.SpecificParent" /> is set, the conditions become specific excluding conditions,
  /// otherwise, if <see cref="P:PX.Data.ReferentialIntegrity.Attributes.PXExcludeRowsFromReferentialIntegrityCheckAttribute.SpecificParent" /> is <code>null</code>, the conditions become
  /// general excluding conditions that are meaningful to any parent-<see cref="T:PX.Data.IBqlTable" /><para />
  /// General and specific conditions (if both) are combined with <see cref="T:PX.Data.Or`1" />-clause on referential integrity check.
  /// </summary>
  public System.Type SpecificParent
  {
    get => this._specificParent;
    set
    {
      this._specificParent = !(value != (System.Type) null) || typeof (IBqlTable).IsAssignableFrom(value) ? value : throw new PXArgumentException(nameof (value), "The assigned type must implement the {0} interface.", new object[1]
      {
        (object) "IBqlTable"
      });
    }
  }

  protected internal override void SetBqlTable(System.Type bqlTable)
  {
    base.SetBqlTable(bqlTable);
    if (this._AttributeLevel != PXAttributeLevel.Type)
      return;
    if (this.SpecificParent == (System.Type) null)
      PXExcludeRowsFromReferentialIntegrityCheckAttribute.GeneralExcludingConditionCache.GetOrAdd(bqlTable, this);
    else
      PXExcludeRowsFromReferentialIntegrityCheckAttribute.SpecificExcludingConditionCache.GetOrAdd(new PXExcludeRowsFromReferentialIntegrityCheckAttribute.ChildAndParent(bqlTable, this.SpecificParent), this);
  }

  private IReadOnlyCollection<IByForeignTableExcludingCondition> GetExternalTableConditions()
  {
    return (IReadOnlyCollection<IByForeignTableExcludingCondition>) ((IEnumerable<IByForeignTableExcludingCondition>) TypeArrayOf<IByForeignTableExcludingCondition>.CheckAndExtractInstances(this.ForeignTableExcludingConditions, (string) null)).ToArray<IByForeignTableExcludingCondition>();
  }

  private BqlCommand Apply(BqlCommand command)
  {
    if (!this.IsValid)
      return command;
    IReadOnlyCollection<IByForeignTableExcludingCondition> externalTableConditions = this.GetExternalTableConditions();
    System.Type type = command.GetType();
    command = BqlCommand.CreateInstance(externalTableConditions.Aggregate<IByForeignTableExcludingCondition, System.Type>(type, (Func<System.Type, IByForeignTableExcludingCondition, System.Type>) ((current, condition) => BqlCommand.AppendJoin(current, this.BuildJoin(condition)))));
    if (this.CurrentTableExcludingCondition != (System.Type) null)
      command = command.WhereAnd(BqlCommand.Compose(typeof (Not<>), this.CurrentTableExcludingCondition));
    command = externalTableConditions.Aggregate<IByForeignTableExcludingCondition, BqlCommand>(command, (Func<BqlCommand, IByForeignTableExcludingCondition, BqlCommand>) ((current, externalCondition) => current.WhereAnd(BqlCommand.Compose(typeof (Not<>), externalCondition.Condition))));
    return command;
  }

  private System.Type BuildJoin(IByForeignTableExcludingCondition condition)
  {
    System.Type type;
    if (condition.JoinOn == typeof (PXParentAttribute))
      type = (this.TableMergedReferencesInspector.GetMergedReferencesOf(this.BqlTable).OutgoingMergedReferences.FirstOrDefault<MergedReference>((Func<MergedReference, bool>) (mr => mr.Reference.ReferenceOrigin == ReferenceOrigin.ParentAttribute && mr.Reference.Parent.Table == condition.ForeignTable)) ?? throw new PXException($"There is no PXParentAttribute referencing {condition.ForeignTable.Name} table from {this.BqlTable.Name} table. Mark {this.BqlTable.Name} table with properly configured PXParentAttribute or use Joined instead of using JoinedAsParentwhen defining excluding condition")).Reference.ToOnClause();
    else
      type = condition.JoinOn;
    return BqlCommand.Compose(typeof (LeftJoin<,>), condition.ForeignTable, type);
  }

  [DebuggerDisplay("{Child} -> {Parent}")]
  private struct ChildAndParent(System.Type child, System.Type parent) : 
    IEquatable<PXExcludeRowsFromReferentialIntegrityCheckAttribute.ChildAndParent>
  {
    public System.Type Parent { get; } = parent;

    public System.Type Child { get; } = child;

    public bool Equals(
      PXExcludeRowsFromReferentialIntegrityCheckAttribute.ChildAndParent other)
    {
      return this.Parent == other.Parent && this.Child == other.Child;
    }

    public override bool Equals(object obj)
    {
      return obj != null && obj is PXExcludeRowsFromReferentialIntegrityCheckAttribute.ChildAndParent other && this.Equals(other);
    }

    public override int GetHashCode()
    {
      System.Type parent = this.Parent;
      int num = ((object) parent != null ? parent.GetHashCode() : 0) * 397;
      System.Type child = this.Child;
      int hashCode = (object) child != null ? child.GetHashCode() : 0;
      return num ^ hashCode;
    }
  }
}
