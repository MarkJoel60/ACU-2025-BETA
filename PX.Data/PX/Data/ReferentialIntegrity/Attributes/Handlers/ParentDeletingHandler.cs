// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.Handlers.ParentDeletingHandler
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data.ReferentialIntegrity.Merging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes.Handlers;

internal class ParentDeletingHandler(
  ITableMergedReferencesInspector tableMergedReferencesInspector,
  PXCache cache,
  object parentRow) : RestrictedOperationHandlerBase(tableMergedReferencesInspector, cache, parentRow)
{
  private object ParentRow => this.Row;

  protected override TableWithKeys ChooseSide(Reference reference) => reference.Parent;

  protected override void HandleImpl()
  {
    System.Type[] tablesToHandle = this.GetTablesToHandle(this.Cache);
    for (int count = 0; count < tablesToHandle.Length; ++count)
    {
      foreach (Reference reference in ((IEnumerable<Reference>) this.GetReferencesToHandle(tablesToHandle[count], ((IEnumerable<System.Type>) tablesToHandle).Take<System.Type>(count))).Where<Reference>((Func<Reference, bool>) (r => !RestrictedOperationHandlerBase.BrokenReferences.ContainsKey(r))))
        this.HandleSingleReference(reference);
    }
    if (!this.ConstraintViolations.Any<RestrictedOperationHandlerBase.ConstraintViolation>())
      return;
    this.RaiseConstaintViolationException();
  }

  private System.Type[] GetTablesToHandle(PXCache cache)
  {
    PXTableAttribute interceptor = cache.Interceptor as PXTableAttribute;
    return cache.GetItemType().GetInheritanceChain().Where<System.Type>((Func<System.Type, bool>) (t =>
    {
      if (!typeof (IBqlTable).IsAssignableFrom(t))
        return false;
      PXTableAttribute pxTableAttribute = interceptor;
      return pxTableAttribute == null || !pxTableAttribute.IsBypassedOnDelete(t);
    })).ToArray<System.Type>();
  }

  private Reference[] GetReferencesToHandle(
    System.Type tableToHandle,
    IEnumerable<System.Type> alreadyHandledDescendants)
  {
    Reference[] array1 = this.TableMergedReferencesInspector.GetIncomingReferencesApplicableTo(tableToHandle).ToArray<Reference>();
    if (!((IEnumerable<Reference>) array1).Any<Reference>())
      return new Reference[0];
    Reference[] array2 = EnumerableExtensions.ExceptBy(((IEnumerable<Reference>) array1).Where<Reference>((Func<Reference, bool>) (r => r.ReferenceOrigin == ReferenceOrigin.DeclareReferenceAttribute)), ((IEnumerable<Reference>) array1).Where<Reference>((Func<Reference, bool>) (r => r.ReferenceOrigin == ReferenceOrigin.ParentAttribute && r.ReferenceBehavior == ReferenceBehavior.Cascade)), r => new
    {
      Child = r.Child,
      Parent = r.Parent
    }, null).Where<Reference>((Func<Reference, bool>) (r => !alreadyHandledDescendants.Contains<System.Type>(r.Child.Table) || !(r.Parent.KeyFieldsToString == r.Child.KeyFieldsToString))).ToArray<Reference>();
    return !((IEnumerable<Reference>) array2).Any<Reference>() ? new Reference[0] : array2;
  }

  private void HandleSingleReference(Reference reference)
  {
    try
    {
      this.HandleSingleReference(reference, this.CreateChildrenSelectCommand(reference));
    }
    catch (DbException ex)
    {
      RestrictedOperationHandlerBase.BrokenReferences.GetOrAdd(reference, true);
    }
    finally
    {
      this.FKGraph.Caches.Clear();
    }
  }

  private BqlCommand CreateChildrenSelectCommand(Reference reference)
  {
    BqlCommand instance = BqlCommand.CreateInstance(reference.ChildrenSelect);
    TableWithKeys tableWithKeys = reference.Child;
    System.Type table1 = tableWithKeys.Table;
    tableWithKeys = reference.Parent;
    System.Type table2 = tableWithKeys.Table;
    return PXExcludeRowsFromReferentialIntegrityCheckAttribute.AppendExcludingCondition(instance, table1, table2);
  }

  protected override void CascadeDeleteChildren(
    Reference reference,
    BqlCommand childrenSelectCommand)
  {
  }

  protected override void CheckRestriction(Reference reference, BqlCommand childrenSelectCommand)
  {
    object foundProblemChild = this.CreateViewReadonly(childrenSelectCommand).SelectSingle(this.ExtractKeyValues(reference));
    PXCache childCache = this.Cache.Graph.Caches[reference.Child.Table];
    if (foundProblemChild == null || ((IEnumerable<object>) childCache.Deleted.ToArray<object>()).Any<object>((Func<object, bool>) (child => childCache.ObjectsEqual(child, foundProblemChild))))
      return;
    foundProblemChild = (object) PXResult.Unwrap(foundProblemChild, reference.Child.Table);
    this.ConstraintViolations.Add(this.CreateConstraintViolation(reference, foundProblemChild, this.ParentRow));
  }

  protected override void SetKeyColumnsOfChildrenToDefault(
    Reference reference,
    BqlCommand childrenSelectCommand)
  {
    PXCache cach = this.Cache.Graph.Caches[reference.Child.Table];
    foreach (object obj1 in this.CreateView(childrenSelectCommand).SelectMulti(this.ExtractKeyValues(reference)))
    {
      object obj2 = cach.Locate(obj1) ?? obj1;
      if (this.ForeignKeyCanBeDefaulted(reference, (object) childrenSelectCommand))
      {
        if (cach.GetStatus(obj2) != PXEntryStatus.Deleted)
        {
          foreach (System.Type keyField in (IEnumerable<System.Type>) reference.Child.KeyFields)
            cach.SetDefaultExt(obj2, keyField.Name);
          cach.Update(obj2);
        }
      }
      else
        this.ConstraintViolations.Add(this.CreateConstraintViolation(reference, obj2, this.ParentRow));
    }
  }

  protected override void SetKeyColumnsOfChildrenToNull(
    Reference reference,
    BqlCommand childrenSelectCommand)
  {
    PXCache cach = this.Cache.Graph.Caches[reference.Child.Table];
    foreach (object obj1 in this.CreateView(childrenSelectCommand).SelectMulti(this.ExtractKeyValues(reference)))
    {
      object obj2 = cach.Locate(obj1) ?? obj1;
      if (this.ForeignKeyCanBeNull(reference, obj2))
      {
        if (cach.GetStatus(obj2) != PXEntryStatus.Deleted)
        {
          foreach (System.Type keyField in (IEnumerable<System.Type>) reference.Child.KeyFields)
            cach.SetValueExt(obj2, keyField.Name, (object) null);
          cach.Update(obj2);
        }
      }
      else
        this.ConstraintViolations.Add(this.CreateConstraintViolation(reference, obj2, this.ParentRow));
    }
  }

  private void RaiseConstaintViolationException()
  {
    if (this.ConstraintViolations.Count == 0)
      throw new InvalidOperationException("ConstraintViolations should contain at least one entry!");
    if (this.ConstraintViolations.Count == 1)
    {
      RestrictedOperationHandlerBase.ConstraintViolation constraintViolation = this.ConstraintViolations.Single<RestrictedOperationHandlerBase.ConstraintViolation>();
      string msg = PXLocalizer.LocalizeFormat("{0} cannot be deleted because it is referenced in the following record: {1}.", (object) constraintViolation.ParentRecordInfo, (object) constraintViolation.ChildRecordInfo);
      PXTrace.WithSourceLocation(nameof (RaiseConstaintViolationException), "C:\\build\\code_repo\\NetTools\\PX.Data\\ReferentialIntegrity\\Attributes\\Handlers\\ParentDeletingHandler.cs", 155).Warning<string>("{Warning}", msg);
      this.ThrowConstraintViolationException(msg);
    }
    else
    {
      RestrictedOperationHandlerBase.ConstraintViolation constraintViolation = this.ConstraintViolations.First<RestrictedOperationHandlerBase.ConstraintViolation>();
      string str = PXLocalizer.LocalizeFormat("{0} cannot be deleted because it is referenced in multiple tables.", (object) constraintViolation.ParentRecordInfo);
      PXTrace.WithSourceLocation(nameof (RaiseConstaintViolationException), "C:\\build\\code_repo\\NetTools\\PX.Data\\ReferentialIntegrity\\Attributes\\Handlers\\ParentDeletingHandler.cs", 163).Warning<string>("{Warning}", str + Environment.NewLine + this.ConstraintViolations.Select<RestrictedOperationHandlerBase.ConstraintViolation, string>((Func<RestrictedOperationHandlerBase.ConstraintViolation, int, string>) ((r, i) => $"\t{i + 1}:\t{r.ChildRecordInfo} - {r.Reference}")).JoinToString<string>(Environment.NewLine));
      this.ThrowConstraintViolationException(PXLocalizer.LocalizeFormat("{0} cannot be deleted because it is referenced in multiple tables. For details, see trace.", (object) constraintViolation.ParentRecordInfo));
    }
  }

  [PXLocalizable]
  public static class Messages
  {
    public const string EntityCannotBeDeletedBecauseOfOneRefRecord = "{0} cannot be deleted because it is referenced in the following record: {1}.";
    public const string EntityCannotBeDeletedBecauseOfManyRefRecords = "{0} cannot be deleted because it is referenced in multiple tables.";
    public const string EntityCannotBeDeletedBecauseOfManyRefRecordsSeeTrace = "{0} cannot be deleted because it is referenced in multiple tables. For details, see trace.";
  }
}
