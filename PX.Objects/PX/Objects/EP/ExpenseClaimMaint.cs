// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ExpenseClaimMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.SM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.EP;

public class ExpenseClaimMaint : PXGraph<
#nullable disable
ExpenseClaimMaint>
{
  public PXFilter<ExpenseClaimMaint.ExpenseClaimFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelectJoin<EPExpenseClaim, LeftJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<EPExpenseClaim.employeeID>>>, Where<Current2<ExpenseClaimMaint.ExpenseClaimFilter.employeeID>, IsNotNull, And<EPExpenseClaim.employeeID, Equal<Current2<ExpenseClaimMaint.ExpenseClaimFilter.employeeID>>, Or<Current2<ExpenseClaimMaint.ExpenseClaimFilter.employeeID>, IsNull, And<Where<EPEmployee.defContactID, Equal<Current<AccessInfo.contactID>>, Or<EPExpenseClaim.createdByID, Equal<Current<AccessInfo.userID>>, Or<EPExpenseClaim.employeeID, WingmanUser<Current<AccessInfo.userID>, EPDelegationOf.expenses>, Or<EPEmployee.defContactID, IsSubordinateOfContact<Current<AccessInfo.contactID>>, Or<EPExpenseClaim.noteID, Approver<Current<AccessInfo.contactID>>>>>>>>>>>, OrderBy<Desc<EPExpenseClaim.refNbr>>> Claim;
  public PXSelect<EPExpenseClaimDetails, Where<EPExpenseClaimDetails.refNbr, Equal<Required<EPExpenseClaim.refNbr>>>> Details;
  public PXSave<EPExpenseClaim> Save;
  public PXAction<ExpenseClaimMaint.ExpenseClaimFilter> createNew;
  public PXAction<ExpenseClaimMaint.ExpenseClaimFilter> EditDetail;
  public PXAction<ExpenseClaimMaint.ExpenseClaimFilter> delete;
  public PXAction<ExpenseClaimMaint.ExpenseClaimFilter> submit;

  [PXSelector(typeof (Users.pKID), SubstituteKey = typeof (Users.fullName), DescriptionField = typeof (Users.fullName), CacheGlobal = true)]
  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Created By", Enabled = false)]
  protected virtual void EPExpenseClaim_CreatedByID_CacheAttached(PXCache sender)
  {
  }

  public ExpenseClaimMaint()
  {
    this.Claim.View.IsReadOnly = true;
    PXUIFieldAttribute.SetVisible<EPExpenseClaim.branchID>(this.Claim.Cache, (object) null, false);
  }

  [PXInsertButton]
  [PXUIField(DisplayName = "")]
  [PXEntryScreenRights(typeof (EPExpenseClaim), "Insert")]
  protected virtual void CreateNew()
  {
    using (new PXPreserveScope())
    {
      ExpenseClaimEntry instance = (ExpenseClaimEntry) PXGraph.CreateInstance(typeof (ExpenseClaimEntry));
      instance.Clear(PXClearOption.ClearAll);
      instance.ExpenseClaim.Insert((EPExpenseClaim) instance.ExpenseClaim.Cache.CreateInstance());
      instance.ExpenseClaim.Cache.IsDirty = false;
      PXRedirectHelper.TryRedirect((PXGraph) instance, PXRedirectHelper.WindowMode.InlineWindow);
    }
  }

  [PXEditDetailButton]
  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select)]
  protected virtual void editDetail()
  {
    if (this.Claim.Current == null)
      return;
    PXRedirectHelper.TryRedirect((PXGraph) this, (object) (EPExpenseClaim) PXSelectBase<EPExpenseClaim, PXSelect<EPExpenseClaim, Where<EPExpenseClaim.refNbr, Equal<Required<EPExpenseClaim.refNbr>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) this.Claim.Current.RefNbr), PXRedirectHelper.WindowMode.InlineWindow);
  }

  [PXDeleteButton]
  [PXUIField(DisplayName = "")]
  [PXEntryScreenRights(typeof (EPExpenseClaim))]
  protected void Delete()
  {
    if (this.Claim.Current == null)
      return;
    if (this.Claim.Current.Released.GetValueOrDefault())
      throw new PXException("This document is released and can not be deleted.");
    ExpenseClaimEntry instance = (ExpenseClaimEntry) PXGraph.CreateInstance(typeof (ExpenseClaimEntry));
    instance.Clear(PXClearOption.ClearAll);
    instance.ExpenseClaim.Current = (EPExpenseClaim) instance.ExpenseClaim.Search<EPExpenseClaim.refNbr>((object) this.Claim.Current.RefNbr);
    instance.Delete.Press();
  }

  [PXButton]
  [PXUIField(DisplayName = "Submit")]
  [PXEntryScreenRights(typeof (EPExpenseClaim))]
  protected void Submit()
  {
    if (this.Claim.Current == null)
      return;
    ExpenseClaimEntry instance = (ExpenseClaimEntry) PXGraph.CreateInstance(typeof (ExpenseClaimEntry));
    instance.Clear(PXClearOption.ClearAll);
    instance.ExpenseClaim.Current = (EPExpenseClaim) instance.ExpenseClaim.Search<EPExpenseClaim.refNbr>((object) this.Claim.Current.RefNbr);
    instance.submit.Press();
  }

  protected virtual void EPExpenseClaimDetails_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is EPExpenseClaimDetails row))
      return;
    this.FindImplementation<ExpenseClaimMaint.ExpenseClaimMaintReceiptExt>().RemoveReceipt(this.Details.Cache, row);
    e.Cancel = true;
  }

  public class ExpenseClaimMaintReceiptExt : ExpenseClaimDetailGraphExtBase<ExpenseClaimMaint>
  {
    public override PXSelectBase<EPExpenseClaimDetails> Receipts
    {
      get => (PXSelectBase<EPExpenseClaimDetails>) this.Base.Details;
    }

    public override PXSelectBase<EPExpenseClaim> Claim
    {
      get => (PXSelectBase<EPExpenseClaim>) this.Base.Claim;
    }
  }

  [PXHidden]
  [Serializable]
  public class ExpenseClaimFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    private int? _employeeId;

    [PXDBInt]
    [PXUIField(DisplayName = "Employee")]
    [PXDefault(typeof (Search<EPEmployee.bAccountID, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    [PXSubordinateAndWingmenSelector(typeof (EPDelegationOf.expenses))]
    [PXFieldDescription]
    public virtual int? EmployeeID
    {
      get => this._employeeId;
      set => this._employeeId = value;
    }

    public abstract class employeeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ExpenseClaimMaint.ExpenseClaimFilter.employeeID>
    {
    }
  }
}
