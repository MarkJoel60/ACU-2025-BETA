// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLBranchAcctMapMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.GL;

public class GLBranchAcctMapMaint : PXGraph<GLBranchAcctMapMaint, Branch>
{
  public PXSelect<Branch> Branches;
  public PXSelect<Ledger> Ledgers;
  public PXSelect<BranchAcctMapFrom, Where<BranchAcctMapFrom.branchID, Equal<Current<Branch.branchID>>>> MapFrom;
  public PXSelect<BranchAcctMapTo, Where<BranchAcctMapTo.branchID, Equal<Current<Branch.branchID>>>> MapTo;

  public GLBranchAcctMapMaint()
  {
    ((PXSelectBase) this.Branches).Cache.AllowInsert = false;
    ((PXSelectBase) this.Branches).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled<Branch.ledgerID>(((PXSelectBase) this.Branches).Cache, (object) null, false);
  }

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (PX.Objects.CR.BAccount.acctCD))]
  [PXDimensionSelector("BRANCH", typeof (Search<Branch.branchCD, Where<Match<Current<AccessInfo.userName>>>>), typeof (Branch.branchCD), DescriptionField = typeof (Branch.acctName))]
  [PXUIField]
  [PXRestrictor(typeof (Where<Branch.active, Equal<True>>), "Branch is inactive.", new System.Type[] {})]
  public virtual void Branch_BranchCD_CacheAttached(PXCache sender)
  {
  }

  public virtual IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    if (!(viewName.ToLower() == "mapfrom") && !(viewName.ToLower() == "mapto"))
      return ((PXGraph) this).ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
    using (new PXReadBranchRestrictedScope())
      return ((PXGraph) this).ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  public virtual void Persist()
  {
    ((PXGraph) this).Persist();
    ((PXSelectBase) this.Branches).Cache.RaiseRowSelected((object) ((PXSelectBase<Branch>) this.Branches).Current);
  }

  public virtual void BranchAcctMapFrom_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = !this.ValidateDuplicateMap<BranchAcctMap.toBranchID>(sender, ((PXSelectBase) this.MapFrom).View, e.Row, (object) null);
  }

  public virtual void BranchAcctMapFrom_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (sender.ObjectsEqual<BranchAcctMapFrom.fromAccountCD, BranchAcctMapFrom.toAccountCD>(e.Row, e.NewRow))
      return;
    ((CancelEventArgs) e).Cancel = !this.ValidateDuplicateMap<BranchAcctMap.toBranchID>(sender, ((PXSelectBase) this.MapFrom).View, e.NewRow, e.Row);
  }

  public virtual void BranchAcctMapTo_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = !this.ValidateDuplicateMap<BranchAcctMap.fromBranchID>(sender, ((PXSelectBase) this.MapTo).View, e.Row, (object) null);
  }

  public virtual void BranchAcctMapTo_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (sender.ObjectsEqual<BranchAcctMapTo.fromAccountCD, BranchAcctMapTo.toAccountCD>(e.Row, e.NewRow))
      return;
    ((CancelEventArgs) e).Cancel = !this.ValidateDuplicateMap<BranchAcctMap.fromBranchID>(sender, ((PXSelectBase) this.MapTo).View, e.NewRow, e.Row);
  }

  protected virtual void BranchAcctMapTo_FromAccountCD_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void BranchAcctMapTo_ToAccountCD_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void BranchAcctMapFrom_FromAccountCD_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void BranchAcctMapFrom_ToAccountCD_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  private bool ValidateDuplicateMap<BranchField>(
    PXCache sender,
    PXView view,
    object row,
    object oldrow)
    where BranchField : IBqlField
  {
    int? nullable1 = (int?) sender.GetValue<BranchField>(row);
    string strA1 = (string) sender.GetValue<BranchAcctMap.fromAccountCD>(row);
    string strB1 = (string) sender.GetValue<BranchAcctMap.toAccountCD>(row);
    if (string.IsNullOrEmpty(strA1) || string.IsNullOrEmpty(strB1))
      return false;
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (string.Compare(strA1, strB1, true) > 0)
      propertyException = new PXSetPropertyException("Account From should be less that Account To.");
    if (propertyException == null)
    {
      foreach (object obj in view.SelectMulti(Array.Empty<object>()))
      {
        if (obj != row && obj != oldrow)
        {
          int? nullable2 = (int?) sender.GetValue<BranchField>(obj);
          int? nullable3 = nullable1;
          int? nullable4 = nullable2;
          if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
          {
            string strA2 = (string) sender.GetValue<BranchAcctMap.fromAccountCD>(obj);
            string strB2 = (string) sender.GetValue<BranchAcctMap.toAccountCD>(obj);
            if (string.Compare(strA1, strB2, true) < 0 && string.Compare(strA2, strB1, true) < 0)
            {
              propertyException = new PXSetPropertyException("Entered account range overlaps with another one.");
              break;
            }
          }
        }
      }
    }
    if (propertyException != null)
    {
      if (!sender.ObjectsEqual<BranchAcctMapFrom.toAccountCD>(row, oldrow) || oldrow == null && sender.GetValue<BranchAcctMap.toAccountCD>(row) != null)
        sender.RaiseExceptionHandling<BranchAcctMapFrom.toAccountCD>(row, sender.GetValue<BranchAcctMap.toAccountCD>(row), (Exception) propertyException);
      else
        sender.RaiseExceptionHandling<BranchAcctMapFrom.fromAccountCD>(row, sender.GetValue<BranchAcctMap.fromAccountCD>(row), (Exception) propertyException);
    }
    return propertyException == null;
  }
}
