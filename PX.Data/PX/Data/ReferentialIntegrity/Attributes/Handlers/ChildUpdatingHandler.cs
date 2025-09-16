// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.Handlers.ChildUpdatingHandler
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

internal class ChildUpdatingHandler(
  ITableMergedReferencesInspector tableMergedReferencesInspector,
  PXCache cache,
  object childRow) : RestrictedOperationHandlerBase(tableMergedReferencesInspector, cache, childRow)
{
  private object ChildRow => this.Row;

  protected override TableWithKeys ChooseSide(Reference reference) => reference.Child;

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
    return cache.GetItemType().GetInheritanceChain().Where<System.Type>((Func<System.Type, bool>) (t => typeof (IBqlTable).IsAssignableFrom(t))).ToArray<System.Type>();
  }

  private Reference[] GetReferencesToHandle(
    System.Type tableToHandle,
    IEnumerable<System.Type> alreadyHandledTables)
  {
    Reference[] array1 = this.TableMergedReferencesInspector.GetOutgoingReferencesApplicableTo(tableToHandle).ToArray<Reference>();
    if (!((IEnumerable<Reference>) array1).Any<Reference>())
      return new Reference[0];
    Reference[] array2 = EnumerableExtensions.ExceptBy(((IEnumerable<Reference>) array1).Where<Reference>((Func<Reference, bool>) (r => r.ReferenceOrigin == ReferenceOrigin.DeclareReferenceAttribute)), ((IEnumerable<Reference>) array1).Where<Reference>((Func<Reference, bool>) (r => r.ReferenceOrigin == ReferenceOrigin.ParentAttribute)), r => new
    {
      Child = r.Child,
      Parent = r.Parent
    }, null).Where<Reference>((Func<Reference, bool>) (r => !alreadyHandledTables.Contains<System.Type>(r.Parent.Table))).ToArray<Reference>();
    return !((IEnumerable<Reference>) array2).Any<Reference>() ? new Reference[0] : array2;
  }

  private void HandleSingleReference(Reference reference)
  {
    try
    {
      this.HandleSingleReference(reference, this.CreateParentSelectCommand(reference));
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

  private BqlCommand CreateParentSelectCommand(Reference reference)
  {
    BqlCommand instance = BqlCommand.CreateInstance(reference.ParentSelect);
    TableWithKeys tableWithKeys = reference.Child;
    System.Type table1 = tableWithKeys.Table;
    tableWithKeys = reference.Parent;
    System.Type table2 = tableWithKeys.Table;
    return PXExcludeRowsFromReferentialIntegrityCheckAttribute.AppendExcludingCondition(instance, table1, table2);
  }

  protected override void CascadeDeleteChildren(Reference reference, BqlCommand parentSelectCommand)
  {
  }

  protected override void CheckRestriction(Reference reference, BqlCommand parentSelectCommand)
  {
    if (this.ParentExists(reference, parentSelectCommand) || ((IEnumerable<object>) this.ExtractKeyValues(reference)).All<object>((Func<object, bool>) (k => k == null)) && this.ForeignKeyCanBeNull(reference, this.ChildRow))
      return;
    this.ConstraintViolations.Add(this.CreateConstraintViolation(reference, this.ChildRow));
  }

  protected override void SetKeyColumnsOfChildrenToDefault(
    Reference reference,
    BqlCommand parentSelectCommand)
  {
    if (this.ParentExists(reference, parentSelectCommand))
      return;
    if (this.ForeignKeyCanBeDefaulted(reference, this.ChildRow))
    {
      PXCache cach = this.FKGraph.Caches[reference.Child.Table];
      foreach (System.Type keyField in (IEnumerable<System.Type>) reference.Child.KeyFields)
        cach.SetDefaultExt(this.ChildRow, keyField.Name);
      cach.Persisted(false);
    }
    else
      this.ConstraintViolations.Add(this.CreateConstraintViolation(reference, this.ChildRow));
  }

  protected override void SetKeyColumnsOfChildrenToNull(
    Reference reference,
    BqlCommand parentSelectCommand)
  {
    if (this.ParentExists(reference, parentSelectCommand))
      return;
    if (this.ForeignKeyCanBeNull(reference, this.ChildRow))
    {
      PXCache cach = this.FKGraph.Caches[reference.Child.Table];
      foreach (System.Type keyField in (IEnumerable<System.Type>) reference.Child.KeyFields)
        cach.SetValue(this.ChildRow, keyField.Name, (object) null);
      cach.Persisted(false);
    }
    else
      this.ConstraintViolations.Add(this.CreateConstraintViolation(reference, this.ChildRow));
  }

  private bool ParentExists(Reference reference, BqlCommand parentSelectCommand)
  {
    object row = this.CreateViewReadonly(parentSelectCommand).SelectSingle(this.ExtractKeyValues(reference));
    if (row != null)
    {
      object obj = (object) PXResult.Unwrap(row, reference.Parent.Table);
      if (obj != null && this.FKGraph.Caches[reference.Parent.Table].IsKeysFilled(obj))
        return true;
    }
    return false;
  }

  private void RaiseConstaintViolationException()
  {
    if (this.ConstraintViolations.Count == 0)
      throw new InvalidOperationException("ConstraintViolations should contain at least one entry!");
    if (this.ConstraintViolations.Count == 1)
    {
      RestrictedOperationHandlerBase.ConstraintViolation constraintViolation = this.ConstraintViolations.Single<RestrictedOperationHandlerBase.ConstraintViolation>();
      string msg = PXLocalizer.LocalizeFormat("{0} cannot be inserted or updated because the following required parent is absent: {1}.", (object) constraintViolation.ChildRecordInfo, (object) constraintViolation.ParentRecordInfo);
      PXTrace.WithSourceLocation(nameof (RaiseConstaintViolationException), "C:\\build\\code_repo\\NetTools\\PX.Data\\ReferentialIntegrity\\Attributes\\Handlers\\ChildUpdatingHandler.cs", 148).Warning<string>("{Warning}", msg);
      this.ThrowConstraintViolationException(msg);
    }
    else
    {
      RestrictedOperationHandlerBase.ConstraintViolation constraintViolation = this.ConstraintViolations.First<RestrictedOperationHandlerBase.ConstraintViolation>();
      string str = PXLocalizer.LocalizeFormat("{0} cannot be inserted or updated because multiple required parents are absent. For details, see trace.", (object) constraintViolation.ChildRecordInfo);
      PXTrace.WithSourceLocation(nameof (RaiseConstaintViolationException), "C:\\build\\code_repo\\NetTools\\PX.Data\\ReferentialIntegrity\\Attributes\\Handlers\\ChildUpdatingHandler.cs", 156).Warning<string>("{Warning}", str + Environment.NewLine + this.ConstraintViolations.Select<RestrictedOperationHandlerBase.ConstraintViolation, string>((Func<RestrictedOperationHandlerBase.ConstraintViolation, int, string>) ((r, i) => $"\t{i + 1}:\t{r.ParentRecordInfo} - {r.Reference}")).JoinToString<string>(Environment.NewLine));
      this.ThrowConstraintViolationException(PXLocalizer.LocalizeFormat("{0} cannot be inserted or updated because multiple required parents are absent.", (object) constraintViolation.ChildRecordInfo));
    }
  }

  [PXLocalizable]
  public static class Messages
  {
    public const string EntityCannotBeModifiedBecauseOfAbsenceOfParent = "{0} cannot be inserted or updated because the following required parent is absent: {1}.";
    public const string EntityCannotBeModifiedBecauseOfAbsenceOfParents = "{0} cannot be inserted or updated because multiple required parents are absent.";
    public const string EntityCannotBeModifiedBecauseOfAbsenceOfParentsSeeTrace = "{0} cannot be inserted or updated because multiple required parents are absent. For details, see trace.";
  }
}
