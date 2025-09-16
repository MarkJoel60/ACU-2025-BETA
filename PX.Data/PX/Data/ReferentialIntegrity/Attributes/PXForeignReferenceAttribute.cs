// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.PXForeignReferenceAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.ReferentialIntegrity.Inspecting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes;

/// <summary>
/// Actively includes targeted <see cref="T:PX.Data.IBqlTable" /> in referential integrity
/// check in <see cref="T:PX.Data.ReferentialIntegrity.Attributes.ReferentialIntegrityRole" /> of child,
/// and explicitly declares a <see cref="T:PX.Data.ReferentialIntegrity.Reference" /> between two <see cref="T:PX.Data.IBqlTable" />s.<para />
/// <see cref="T:PX.Data.IBqlTable" /> could be passively (e.g. without <see cref="T:PX.Data.ReferentialIntegrity.Reference" /> generating)
/// included in referential integrity check in any <see cref="T:PX.Data.ReferentialIntegrity.Attributes.ReferentialIntegrityRole" />
/// by using <see cref="T:PX.Data.ReferentialIntegrity.Attributes.PXReferentialIntegrityCheckAttribute" />.<para />
/// Certain rows could be excluded from referential integrity check by using <see cref="T:PX.Data.ReferentialIntegrity.Attributes.PXExcludeRowsFromReferentialIntegrityCheckAttribute" />.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
public class PXForeignReferenceAttribute : PXEventSubscriberAttribute
{
  private readonly PXForeignReferenceAttribute.RefHolder _refHolder;
  private readonly bool _canFail;
  private System.Type _originalBqlTable;

  private protected virtual bool ForceNoAction => false;

  [InjectDependencyOnTypeLevel]
  public ITableReferenceCollector TableReferenceCollector { get; set; }

  public Reference Reference { get; private set; }

  public System.Type ChildTable => this.Reference?.Child.Table;

  public IReadOnlyCollection<System.Type> ChildKeyFields => this.Reference?.Child.KeyFields;

  public System.Type ParentTable => this.Reference?.Parent.Table;

  public IReadOnlyCollection<System.Type> ParentKeyFields => this.Reference?.Parent.KeyFields;

  /// <summary>
  /// Explicitly declares a <see cref="T:PX.Data.ReferentialIntegrity.Reference" /> (foreign key constraint)
  /// between two <see cref="T:PX.Data.IBqlTable" />s by a simple or a composite key relation.
  /// </summary>
  /// <param name="fieldsRelationsContainer">
  /// A single <see cref="T:PX.Data.ReferentialIntegrity.Attributes.Field`1.IsRelatedTo`1" /> type specification
  /// or a <see cref="T:PX.Data.ReferentialIntegrity.Attributes.FieldRelationArray" />, such as a <see cref="T:PX.Data.ReferentialIntegrity.Attributes.CompositeKey`2" />,
  /// filled with such type specifications.
  /// </param>
  protected PXForeignReferenceAttribute(
    System.Type fieldsRelationsContainer,
    ReferenceOrigin referenceOrigin,
    ReferenceBehavior referenceBehavior,
    bool canFail)
  {
    this._refHolder = new PXForeignReferenceAttribute.RefHolder(fieldsRelationsContainer, referenceOrigin, referenceBehavior);
    this._canFail = canFail;
  }

  /// <summary>
  /// Explicitly declares a <see cref="T:PX.Data.ReferentialIntegrity.Reference" /> (foreign key constraint)
  /// between two <see cref="T:PX.Data.IBqlTable" />s by a simple or a composite key relation,
  /// with <see cref="F:PX.Data.ReferentialIntegrity.ReferenceOrigin.DeclareReferenceAttribute" />.
  /// </summary>
  /// <param name="fieldsRelationsContainer">
  /// A single <see cref="T:PX.Data.ReferentialIntegrity.Attributes.Field`1.IsRelatedTo`1" /> type specification
  /// or a <see cref="T:PX.Data.ReferentialIntegrity.Attributes.FieldRelationArray" />, such as a <see cref="T:PX.Data.ReferentialIntegrity.Attributes.CompositeKey`2" />,
  /// filled with such type specifications.
  /// </param>
  public PXForeignReferenceAttribute(
    System.Type fieldsRelationsContainer,
    ReferenceBehavior referenceBehavior)
    : this(fieldsRelationsContainer, ReferenceOrigin.DeclareReferenceAttribute, referenceBehavior, false)
  {
  }

  /// <summary>
  /// Explicitly declares a <see cref="T:PX.Data.ReferentialIntegrity.Reference" /> (foreign key constraint)
  /// between two <see cref="T:PX.Data.IBqlTable" />s by a simple or a composite key relation,
  /// with <see cref="F:PX.Data.ReferentialIntegrity.ReferenceOrigin.DeclareReferenceAttribute" /> and <see cref="F:PX.Data.ReferentialIntegrity.ReferenceBehavior.Restrict" />.
  /// </summary>
  /// <param name="fieldsRelationsContainer">
  /// A single <see cref="T:PX.Data.ReferentialIntegrity.Attributes.Field`1.IsRelatedTo`1" /> type specification
  /// or a <see cref="T:PX.Data.ReferentialIntegrity.Attributes.FieldRelationArray" />, such as a <see cref="T:PX.Data.ReferentialIntegrity.Attributes.CompositeKey`2" />,
  /// filled with such type specifications.
  /// </param>
  public PXForeignReferenceAttribute(System.Type fieldsRelationsContainer)
    : this(fieldsRelationsContainer, ReferenceOrigin.DeclareReferenceAttribute, ReferenceBehavior.Restrict, false)
  {
  }

  protected internal override void SetBqlTable(System.Type bqlTable)
  {
    base.SetBqlTable(bqlTable);
    this._originalBqlTable = bqlTable;
    if (this._AttributeLevel != PXAttributeLevel.Type)
      return;
    if (this.TableReferenceCollector == null)
    {
      PXTrace.WriteWarning("Reference collection for {ReferenceOrigins} references is turned off because ITableReferenceCollector is not registered or attribute-level DI is not enabled", (object) new ReferenceOrigin[2]
      {
        ReferenceOrigin.DeclareReferenceAttribute,
        ReferenceOrigin.ParentAttribute
      });
    }
    else
    {
      if (this.TableReferenceCollector.AllReferencesAreCollected.IsCompleted || this._refHolder.SelectOrFieldsRelationsContainer.With<System.Type, bool>((Func<System.Type, bool>) (t => typeof (IBqlSelect).IsAssignableFrom(t) && t.IsGenericType && t.GetGenericTypeDefinition() == typeof (Select<>))))
        return;
      Reference reference = this.CreateReference(this._originalBqlTable, this._originalBqlTable.GetNestedType(this.FieldName, BindingFlags.IgnoreCase | BindingFlags.Public, true));
      if (!(reference != (Reference) null))
        return;
      this.TableReferenceCollector.TryCollectReference(reference);
    }
  }

  private Reference CreateReference(System.Type currentTable, System.Type currentField)
  {
    try
    {
      this.Reference = Reference.FromFieldsRelations(PXForeignReferenceAttribute.ToFieldsRelation(this._refHolder.SelectOrFieldsRelationsContainer, currentField), this._refHolder.ReferenceOrigin, this.ForceNoAction ? ReferenceBehavior.NoAction : this._refHolder.ReferenceBehavior);
      if (!this.ChildTable.IsAssignableFrom(currentTable))
        throw new PXArgumentException(nameof (currentTable), "The incorrect child table {1} was provided. The child table must be the same as the target class {0} or one of its descendants.", new object[2]
        {
          (object) currentTable.Name,
          (object) this.ChildTable.Name
        });
      return this.Reference;
    }
    catch (PXException ex) when (this._canFail)
    {
      LogForeignReferenceException((Exception) ex);
      return (Reference) null;
    }
    catch (PXException ex) when (!this._canFail && ExceptionExtensions.Rethrow<PXException>(ex, new System.Action<PXException>(LogForeignReferenceException)))
    {
      throw;
    }

    void LogForeignReferenceException(Exception ex)
    {
      if (ex is PXInvalidFieldsRelationException relationException)
        PXTrace.WriteError((Exception) relationException, "An invalid set of {Relations} with {ParentTables} and {ChildTables} for foreign reference was passed to {AttributeType} for {DacType} and {BqlField} (for {ReferenceOrigin} reference type). Please make sure that you haven't used BQL fields from the base DAC in the relation inside the derived DAC.", (object) relationException.Relations, (object) relationException.ParentTables, (object) relationException.ChildTables, (object) this.GetType(), (object) currentTable, (object) currentField, (object) relationException.ReferenceOrigin);
      else
        PXTrace.WriteError(ex, "An invalid foreign reference was passed to {AttributeType} for {DacType} and {BqlField} (for {ReferenceOrigin} reference type).", (object) this.GetType(), (object) currentTable, (object) currentField, (object) this._refHolder.ReferenceOrigin);
    }
  }

  private static System.Type ToFieldsRelation(System.Type selectOrFieldOrFieldsRelation, System.Type currentField)
  {
    if (TypeArrayOf<IFieldsRelation>.IsTypeArrayOrElement(selectOrFieldOrFieldsRelation))
      return selectOrFieldOrFieldsRelation;
    if (typeof (IBqlSelect).IsAssignableFrom(selectOrFieldOrFieldsRelation))
    {
      try
      {
        Reference reference = Reference.FromParentSelect(BqlCommand.CreateInstance(selectOrFieldOrFieldsRelation), ReferenceOrigin.ParentAttribute, ReferenceBehavior.Cascade);
        TableWithKeys tableWithKeys = reference.Child;
        IReadOnlyCollection<System.Type> keyFields1 = tableWithKeys.KeyFields;
        tableWithKeys = reference.Parent;
        IReadOnlyCollection<System.Type> keyFields2 = tableWithKeys.KeyFields;
        return TypeArrayOf<IFieldsRelation>.Construct(keyFields1.Zip<System.Type, System.Type, System.Type>((IEnumerable<System.Type>) keyFields2, (Func<System.Type, System.Type, System.Type>) ((cf, pf) => typeof (Field<>.IsRelatedTo<>).MakeGenericType(cf, pf))).ToArray<System.Type>());
      }
      catch (PXArgumentException ex)
      {
        return (System.Type) null;
      }
    }
    else
    {
      if (!typeof (IBqlField).IsAssignableFrom(selectOrFieldOrFieldsRelation))
        throw new PXArgumentException(nameof (selectOrFieldOrFieldsRelation));
      return typeof (Field<>.IsRelatedTo<>).MakeGenericType(currentField, selectOrFieldOrFieldsRelation);
    }
  }

  /// <exclude />
  [PXLocalizable]
  public static class Messages
  {
    public const string ChildTableShouldBeTheSameAsTargetedClass = "The incorrect child table {1} was provided. The child table must be the same as the target class {0} or one of its descendants.";
  }

  /// <exclude />
  private class RefHolder(
    System.Type selectOrFieldsRelationsContainer,
    ReferenceOrigin referenceOrigin,
    ReferenceBehavior referenceBehavior) : Tuple<System.Type, ReferenceOrigin, ReferenceBehavior>(selectOrFieldsRelationsContainer, referenceOrigin, referenceBehavior)
  {
    public System.Type SelectOrFieldsRelationsContainer => this.Item1;

    public ReferenceOrigin ReferenceOrigin => this.Item2;

    public ReferenceBehavior ReferenceBehavior => this.Item3;
  }
}
