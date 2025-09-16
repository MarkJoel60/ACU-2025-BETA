// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTransactionsMaintSplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

[PXProtectedAccess(null)]
public abstract class CABankTransactionsMaintSplit : PXGraphExtension<CABankTransactionsMaint>
{
  [PXFilterable(new Type[] {})]
  public PXSelect<CABankTran, Where<CABankTran.processed, Equal<False>, And<CABankTran.cashAccountID, Equal<Current<CABankTransactionsMaint.Filter.cashAccountID>>, And<CABankTran.tranType, Equal<Current<CABankTransactionsMaint.Filter.tranType>>>>>, OrderBy<Asc<CABankTran.sortOrder, Desc<CABankTranSplit.splitted, Asc<CABankTran.tranID>>>>> Details;
  public PXAction<CABankTransactionsMaint.Filter> split;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.bankTransactionSplits>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase) this.Base.Details).Cache.AllowUpdate = true;
    ((PXSelectBase) this.Base.Details).AllowUpdate = true;
    ((PXSelectBase) this.Base.Details).Cache.AllowDelete = true;
    ((PXSelectBase) this.Base.Details).AllowDelete = true;
  }

  protected virtual IEnumerable details()
  {
    CABankTransactionsMaintSplit transactionsMaintSplit = this;
    CABankTransactionsMaint.Filter current = ((PXSelectBase<CABankTransactionsMaint.Filter>) transactionsMaintSplit.Base.TranFilter).Current;
    if (current != null && current.CashAccountID.HasValue)
    {
      TimeSpan timeSpan;
      Exception exception;
      PXLongRunStatus status = PXLongOperation.GetStatus(((PXGraph) transactionsMaintSplit.Base).UID, ref timeSpan, ref exception);
      IEnumerable<CABankTran> caBankTrans = (IEnumerable<CABankTran>) null;
      if (status != null)
      {
        object[] source;
        PXLongOperation.GetCustomInfo(((PXGraph) transactionsMaintSplit.Base).UID, ref source);
        if (source != null)
          caBankTrans = source.Cast<CABankTran>();
      }
      foreach (object obj in caBankTrans ?? transactionsMaintSplit.GetUnprocessedTransactions())
        yield return obj;
    }
  }

  [PXOverride]
  public virtual IEnumerable Hide(
    PXAdapter adapter,
    CABankTransactionsMaintSplit.HideDelegate baseMethod)
  {
    CABankTranSplit extension = PXCacheEx.GetExtension<CABankTranSplit>((IBqlTable) ((PXSelectBase<CABankTran>) this.Base.Details).Current);
    if (extension != null && extension.ParentTranID.HasValue || extension.Splitted.GetValueOrDefault())
      throw new PXException("The transaction cannot be hidden because it has been split.");
    return baseMethod(adapter);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Split(PXAdapter adapter)
  {
    CABankTranSplit extension = PXCacheEx.GetExtension<CABankTranSplit>((IBqlTable) ((PXSelectBase<CABankTran>) this.Base.Details).Current);
    if (extension != null)
    {
      CABankTran originalTran;
      if (extension.ParentTranID.HasValue)
        originalTran = ((PXSelectBase<CABankTran>) this.Base.Details).Locate(new CABankTran()
        {
          TranID = extension.ParentTranID
        }) ?? CABankTran.PK.Find((PXGraph) this.Base, extension.ParentTranID, (PKFindOptions) 1);
      else
        originalTran = ((PXSelectBase<CABankTran>) this.Base.Details).Current;
      this.SplitTransaction(((PXSelectBase) this.Base.Details).Cache, originalTran);
      ((PXGraph) this.Base).Views["Details"].RequestRefresh();
    }
    return adapter.Get();
  }

  protected internal CABankTran SplitTransaction(PXCache cache, CABankTran originalTran)
  {
    if (originalTran.DocumentMatched.GetValueOrDefault() || originalTran.Processed.GetValueOrDefault())
      throw new PXException("The transaction cannot be split because it has been matched.");
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      int? tranId = originalTran.TranID;
      CABankTran copy1 = (CABankTran) cache.CreateCopy((object) originalTran);
      copy1.TranID = new int?();
      copy1.CuryTranAmt = new Decimal?(0M);
      copy1.NoteID = new Guid?();
      copy1.Processed = new bool?(false);
      CABankTranSplit extension1 = PXCacheEx.GetExtension<CABankTranSplit>((IBqlTable) copy1);
      extension1.ParentTranID = tranId;
      extension1.Splitted = new bool?(false);
      extension1.CuryOrigTranAmt = new Decimal?(0M);
      extension1.OrigDrCr = (string) null;
      extension1.ChildsCount = new int?();
      extension1.UnmatchedChilds = new int?();
      extension1.UnprocessedChilds = new int?();
      CABankTran caBankTran1 = (CABankTran) cache.Insert((object) copy1);
      CABankTran copy2 = (CABankTran) cache.CreateCopy((object) originalTran);
      CABankTranSplit extension2 = PXCacheEx.GetExtension<CABankTranSplit>((IBqlTable) copy2);
      if (!extension2.Splitted.GetValueOrDefault())
      {
        extension2.CuryOrigTranAmt = originalTran.CuryTranAmt;
        extension2.OrigDrCr = originalTran.DrCr;
        extension2.Splitted = new bool?(true);
      }
      extension2.ChildsCount = new int?(extension2.ChildsCount.GetValueOrDefault() + 1);
      CABankTranSplit caBankTranSplit1 = extension2;
      int? nullable1 = extension2.UnmatchedChilds;
      int? nullable2 = new int?(nullable1.GetValueOrDefault() + 1);
      caBankTranSplit1.UnmatchedChilds = nullable2;
      CABankTranSplit caBankTranSplit2 = extension2;
      nullable1 = extension2.UnprocessedChilds;
      int? nullable3 = new int?(nullable1.GetValueOrDefault() + 1);
      caBankTranSplit2.UnprocessedChilds = nullable3;
      CABankTran caBankTran2 = (CABankTran) cache.Update((object) copy2);
      transactionScope.Complete();
      return caBankTran1;
    }
  }

  [PXOverride]
  public virtual void ValidateBeforeProcessing(CABankTran det)
  {
    CABankTranSplit extension = PXCacheEx.GetExtension<CABankTranSplit>((IBqlTable) det);
    if ((extension == null || !extension.ParentTranID.HasValue) && !extension.Splitted.GetValueOrDefault())
      return;
    int? nullable = extension.Splitted.GetValueOrDefault() ? det.TranID : extension.ParentTranID;
    using (IEnumerator<PXResult<CABankTran>> enumerator = PXSelectBase<CABankTran, PXViewOf<CABankTran>.BasedOn<SelectFromBase<CABankTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CABankTran.documentMatched, Equal<False>>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CABankTranSplit.parentTranID, Equal<P.AsInt>>>>>.Or<BqlOperand<CABankTran.tranID, IBqlInt>.IsEqual<P.AsInt>>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) nullable,
      (object) nullable
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXResult<CABankTran>.op_Implicit(enumerator.Current);
        throw new PXSetPropertyException("The transaction cannot be processed because it has related unmatched transactions.");
      }
    }
  }

  [PXProtectedAccess(null)]
  protected abstract IEnumerable<CABankTran> GetUnprocessedTransactions();

  [PXOverride]
  public virtual IEnumerable<CABankTran> GetUnprocessedTransactions(
    CABankTransactionsMaintSplit.GetUnprocessedTransactionsDelegate baseMethod)
  {
    CABankTransactionsMaintSplit transactionsMaintSplit = this;
    CABankTransactionsMaint.Filter current = ((PXSelectBase<CABankTransactionsMaint.Filter>) transactionsMaintSplit.Base.TranFilter).Current;
    if (current != null && current.CashAccountID.HasValue)
    {
      PXSelect<CABankTran, Where2<Where<CABankTran.processed, Equal<False>, Or<Where<CABankTranSplit.unprocessedChilds, Greater<Zero>, And<CABankTranSplit.unprocessedChilds, IsNotNull>>>>, And<CABankTran.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CABankTran.tranType, Equal<Required<CABankTran.tranType>>>>>, OrderBy<Asc<CABankTran.sortOrder, Desc<CABankTranSplit.splitted, Asc<CABankTran.tranID>>>>> pxSelect = new PXSelect<CABankTran, Where2<Where<CABankTran.processed, Equal<False>, Or<Where<CABankTranSplit.unprocessedChilds, Greater<Zero>, And<CABankTranSplit.unprocessedChilds, IsNotNull>>>>, And<CABankTran.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CABankTran.tranType, Equal<Required<CABankTran.tranType>>>>>, OrderBy<Asc<CABankTran.sortOrder, Desc<CABankTranSplit.splitted, Asc<CABankTran.tranID>>>>>((PXGraph) transactionsMaintSplit.Base);
      object[] objArray = new object[2]
      {
        (object) current.CashAccountID,
        (object) current.TranType
      };
      foreach (PXResult<CABankTran> pxResult in ((PXSelectBase<CABankTran>) pxSelect).SelectWithViewContext(objArray))
        yield return PXResult<CABankTran>.op_Implicit(pxResult);
    }
  }

  public virtual void _(Events.RowSelected<CABankTran> e)
  {
    CABankTran row = e.Row;
    CABankTranSplit extension = row != null ? PXCacheEx.GetExtension<CABankTranSplit>((IBqlTable) row) : (CABankTranSplit) null;
    bool flag1 = extension != null && extension.ParentTranID.HasValue;
    bool? nullable;
    if (flag1)
    {
      bool flag2 = true;
      nullable = row.DocumentMatched;
      if (nullable.GetValueOrDefault())
      {
        flag2 = false;
      }
      else
      {
        CABankTran caBankTran = CABankTran.PK.Find((PXGraph) this.Base, extension.ParentTranID, (PKFindOptions) 1);
        int num;
        if (caBankTran == null)
        {
          num = 0;
        }
        else
        {
          nullable = caBankTran.DocumentMatched;
          num = nullable.GetValueOrDefault() ? 1 : 0;
        }
        if (num != 0)
          flag2 = false;
      }
      PXEntryStatus status = ((PXSelectBase) this.Base.Details).Cache.GetStatus((object) ((PXSelectBase<CABankTran>) this.Base.Details).Current);
      if ((status == 2 ? 1 : (status == 4 ? 1 : 0)) != 0)
      {
        PXUIFieldAttribute.SetEnabled(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankTran>>) e).Cache, (object) row, false);
        ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankTran>>) e).Cache.RaiseExceptionHandling<CABankTranSplit.splittedIcon>((object) row, (object) extension.SplittedIcon, (Exception) new PXSetPropertyException((IBqlTable) row, "The child transaction must be saved before it can be matched.", (PXErrorLevel) 3));
      }
      else
        ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankTran>>) e).Cache.RaiseExceptionHandling<CABankTranSplit.splittedIcon>((object) row, (object) extension.SplittedIcon, (Exception) null);
      PXUIFieldAttribute.SetEnabled<CABankTran.curyCreditAmt>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankTran>>) e).Cache, (object) row, flag1 & flag2);
      PXUIFieldAttribute.SetEnabled<CABankTran.curyDebitAmt>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankTran>>) e).Cache, (object) row, flag1 & flag2);
      PXUIFieldAttribute.SetVisible<CABankTranSplit.splittedIcon>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankTran>>) e).Cache, (object) null, flag1);
      PXUIFieldAttribute.SetVisibility<CABankTranSplit.curyOrigCreditAmt>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankTran>>) e).Cache, (object) null, (PXUIVisibility) 3);
      PXUIFieldAttribute.SetVisibility<CABankTranSplit.curyOrigDebitAmt>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankTran>>) e).Cache, (object) null, (PXUIVisibility) 3);
    }
    if (row == null)
      return;
    nullable = row.Processed;
    if (!nullable.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetEnabled(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CABankTran>>) e).Cache, (object) row, false);
  }

  public virtual void _(Events.RowUpdating<CABankTran> e)
  {
    CABankTran newRow = e.NewRow;
    if (newRow == null || !newRow.CreateDocument.GetValueOrDefault())
      return;
    bool flag = CABankTransactionsMaint.ValidateTranFields(this.Base, ((Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<CABankTran>>) e).Cache, newRow, (PXSelectBase<CABankTranAdjustment>) this.Base.Adjustments);
    ((PXSelectBase) this.Base.Details).Cache.SetValueExt<CABankTran.documentMatched>((object) newRow, (object) flag);
  }

  [PXOverride]
  public virtual void CABankTran_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    CABankTran row = (CABankTran) e.Row;
    CABankTran oldRow = (CABankTran) e.OldRow;
    CABankTranSplit extension = PXCacheEx.GetExtension<CABankTranSplit>((IBqlTable) row);
    int? nullable1;
    int num1;
    if (extension == null)
    {
      num1 = 1;
    }
    else
    {
      nullable1 = extension.ParentTranID;
      num1 = !nullable1.HasValue ? 1 : 0;
    }
    if (num1 != 0)
      return;
    Decimal? curyTranAmt = (Decimal?) oldRow?.CuryTranAmt;
    Decimal? nullable2 = (Decimal?) row?.CuryTranAmt;
    bool? nullable3;
    bool? nullable4;
    if (curyTranAmt.GetValueOrDefault() == nullable2.GetValueOrDefault() & curyTranAmt.HasValue == nullable2.HasValue)
    {
      bool? documentMatched = (bool?) oldRow?.DocumentMatched;
      nullable3 = (bool?) row?.DocumentMatched;
      if (documentMatched.GetValueOrDefault() == nullable3.GetValueOrDefault() & documentMatched.HasValue == nullable3.HasValue)
      {
        nullable3 = (bool?) oldRow?.Processed;
        nullable4 = (bool?) row?.Processed;
        if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
          return;
      }
    }
    CABankTran caBankTran1 = (CABankTran) sender.Locate((object) new CABankTran()
    {
      TranID = extension.ParentTranID
    }) ?? CABankTran.PK.Find((PXGraph) this.Base, extension.ParentTranID, (PKFindOptions) 1);
    CABankTran copy = (CABankTran) sender.CreateCopy((object) caBankTran1);
    CABankTranSplit caBankTranSplit1 = copy != null ? PXCacheEx.GetExtension<CABankTranSplit>((IBqlTable) copy) : throw new PXSetPropertyException("Document Not Found");
    nullable2 = (Decimal?) oldRow?.CuryTranAmt;
    Decimal? nullable5 = (Decimal?) row?.CuryTranAmt;
    if (!(nullable2.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable2.HasValue == nullable5.HasValue))
    {
      nullable2 = copy.CuryTranAmt;
      Decimal valueOrDefault1 = ((Decimal?) oldRow?.CuryTranAmt).GetValueOrDefault();
      nullable5 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
      Decimal? nullable6;
      if (row == null)
      {
        nullable2 = new Decimal?();
        nullable6 = nullable2;
      }
      else
        nullable6 = row.CuryTranAmt;
      nullable2 = nullable6;
      Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
      Decimal? nullable7;
      if (!nullable5.HasValue)
      {
        nullable2 = new Decimal?();
        nullable7 = nullable2;
      }
      else
        nullable7 = new Decimal?(nullable5.GetValueOrDefault() - valueOrDefault2);
      Decimal? nullable8 = nullable7;
      copy = (CABankTran) sender.CreateCopy((object) copy);
      nullable5 = nullable8;
      Decimal num2 = 0M;
      if (nullable5.GetValueOrDefault() > num2 & nullable5.HasValue)
      {
        copy.CuryDebitAmt = nullable8;
      }
      else
      {
        nullable5 = nullable8;
        Decimal num3 = 0M;
        if (nullable5.GetValueOrDefault() < num3 & nullable5.HasValue)
        {
          CABankTran caBankTran2 = copy;
          nullable5 = nullable8;
          Decimal? nullable9;
          if (!nullable5.HasValue)
          {
            nullable2 = new Decimal?();
            nullable9 = nullable2;
          }
          else
            nullable9 = new Decimal?(-nullable5.GetValueOrDefault());
          caBankTran2.CuryCreditAmt = nullable9;
        }
      }
    }
    if (caBankTranSplit1 != null)
    {
      nullable4 = (bool?) oldRow?.DocumentMatched;
      nullable3 = (bool?) row?.DocumentMatched;
      if (!(nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue))
        caBankTranSplit1.UnmatchedChilds = new int?(PXSelectBase<CABankTran, PXViewOf<CABankTran>.BasedOn<SelectFromBase<CABankTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CABankTranSplit.parentTranID, Equal<P.AsInt>>>>>.And<BqlOperand<CABankTran.documentMatched, IBqlBool>.IsEqual<False>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) extension.ParentTranID
        }).Count);
    }
    if (caBankTranSplit1 != null)
    {
      nullable3 = (bool?) oldRow?.Processed;
      nullable4 = (bool?) row?.Processed;
      if (!(nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue))
      {
        CABankTranSplit caBankTranSplit2 = caBankTranSplit1;
        nullable1 = caBankTranSplit1.UnprocessedChilds;
        int num4;
        if (row != null)
        {
          nullable4 = row.Processed;
          if (nullable4.GetValueOrDefault())
          {
            num4 = -1;
            goto label_33;
          }
        }
        num4 = 1;
label_33:
        int num5 = num4;
        int? nullable10 = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + num5) : new int?();
        caBankTranSplit2.UnprocessedChilds = nullable10;
      }
    }
    CABankTran caBankTran3 = (CABankTran) sender.Update((object) copy);
    sender.Current = (object) row;
    ((PXGraph) this.Base).Views["Details"].RequestRefresh();
  }

  public virtual void _(Events.RowPersisted<CABankTran> e)
  {
    if (e.Operation != 2)
      return;
    ((PXGraph) this.Base).Views["Details"].RequestRefresh();
  }

  public virtual void _(Events.RowInserted<CABankTran> e)
  {
    ((PXGraph) this.Base).Views["Details"].RequestRefresh();
  }

  public virtual void _(Events.RowDeleting<CABankTran> e)
  {
    CABankTran row = e.Row;
    CABankTranSplit extension = PXCacheEx.GetExtension<CABankTranSplit>((IBqlTable) row);
    if ((extension != null ? (!extension.ParentTranID.HasValue ? 1 : 0) : 1) != 0)
    {
      e.Cancel = true;
      throw new PXException("Only child transactions can be deleted.");
    }
    if (row != null && row.DocumentMatched.GetValueOrDefault())
    {
      e.Cancel = true;
      throw new PXException("The transaction cannot be deleted because it or its parent transaction has been matched.");
    }
    CABankTran caBankTran = (CABankTran) ((PXGraph) this.Base).Caches[typeof (CABankTran)].Locate((object) new CABankTran()
    {
      TranID = extension.ParentTranID
    }) ?? CABankTran.PK.Find((PXGraph) this.Base, extension.ParentTranID, (PKFindOptions) 1);
    if ((caBankTran != null ? (caBankTran.DocumentMatched.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      e.Cancel = true;
      throw new PXException("The transaction cannot be deleted because it or its parent transaction has been matched.");
    }
  }

  public virtual void _(Events.RowDeleted<CABankTran> e)
  {
    CABankTran row = e.Row;
    PXCache cache = ((Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<CABankTran>>) e).Cache;
    CABankTranSplit extension1 = PXCacheEx.GetExtension<CABankTranSplit>((IBqlTable) row);
    if ((extension1 != null ? (!extension1.ParentTranID.HasValue ? 1 : 0) : 1) != 0)
      return;
    CABankTran caBankTran1 = (CABankTran) cache.Locate((object) new CABankTran()
    {
      TranID = extension1.ParentTranID
    }) ?? CABankTran.PK.Find((PXGraph) this.Base, extension1.ParentTranID, (PKFindOptions) 1);
    Decimal? curyTranAmt = caBankTran1.CuryTranAmt;
    Decimal valueOrDefault = ((Decimal?) row?.CuryTranAmt).GetValueOrDefault();
    Decimal? nullable1 = curyTranAmt.HasValue ? new Decimal?(curyTranAmt.GetValueOrDefault() + valueOrDefault) : new Decimal?();
    CABankTran copy = (CABankTran) cache.CreateCopy((object) caBankTran1);
    Decimal? nullable2 = nullable1;
    Decimal num1 = 0M;
    if (nullable2.GetValueOrDefault() > num1 & nullable2.HasValue)
    {
      copy.CuryDebitAmt = nullable1;
    }
    else
    {
      nullable2 = nullable1;
      Decimal num2 = 0M;
      if (nullable2.GetValueOrDefault() < num2 & nullable2.HasValue)
      {
        CABankTran caBankTran2 = copy;
        nullable2 = nullable1;
        Decimal? nullable3 = nullable2.HasValue ? new Decimal?(-nullable2.GetValueOrDefault()) : new Decimal?();
        caBankTran2.CuryCreditAmt = nullable3;
      }
    }
    bool flag = false;
    using (IEnumerator<PXResult<CABankTran>> enumerator = PXSelectBase<CABankTran, PXViewOf<CABankTran>.BasedOn<SelectFromBase<CABankTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CABankTranSplit.parentTranID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) extension1.ParentTranID
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXResult<CABankTran>.op_Implicit(enumerator.Current);
        flag = true;
      }
    }
    CABankTranSplit extension2 = PXCacheEx.GetExtension<CABankTranSplit>((IBqlTable) copy);
    if (!flag)
    {
      extension2.Splitted = new bool?(false);
      extension2.CuryOrigTranAmt = new Decimal?();
      extension2.OrigDrCr = (string) null;
    }
    extension2.ChildsCount = new int?(extension2.ChildsCount.GetValueOrDefault() - 1);
    extension2.UnmatchedChilds = new int?(extension2.UnmatchedChilds.GetValueOrDefault() - 1);
    extension2.UnprocessedChilds = new int?(extension2.UnprocessedChilds.GetValueOrDefault() - 1);
    CABankTran caBankTran3 = (CABankTran) cache.Update((object) copy);
    ((PXGraph) this.Base).Views["Details"].RequestRefresh();
  }

  public virtual void _(Events.FieldVerifying<CABankTran.curyCreditAmt> e)
  {
    CABankTran row1 = (CABankTran) e.Row;
    PXCache cache = ((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<CABankTran.curyCreditAmt>>) e).Cache;
    CABankTran row2 = row1;
    Decimal? newValue1 = (Decimal?) ((Events.FieldVerifyingBase<Events.FieldVerifying<CABankTran.curyCreditAmt>, object, object>) e).NewValue;
    Decimal? newValue2 = newValue1.HasValue ? new Decimal?(-newValue1.GetValueOrDefault()) : new Decimal?();
    Decimal? curyTranAmt = row1.CuryTranAmt;
    PXSetPropertyException propertyException = this.VerifyAmout(cache, row2, newValue2, curyTranAmt);
    if (propertyException != null)
    {
      ((Events.FieldVerifyingBase<Events.FieldVerifying<CABankTran.curyCreditAmt>, object, object>) e).NewValue = e.OldValue;
      throw propertyException;
    }
  }

  public virtual void _(Events.FieldVerifying<CABankTran.curyDebitAmt> e)
  {
    CABankTran row = (CABankTran) e.Row;
    PXSetPropertyException propertyException = this.VerifyAmout(((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<CABankTran.curyDebitAmt>>) e).Cache, row, (Decimal?) ((Events.FieldVerifyingBase<Events.FieldVerifying<CABankTran.curyDebitAmt>, object, object>) e).NewValue, row.CuryTranAmt);
    if (propertyException != null)
    {
      ((Events.FieldVerifyingBase<Events.FieldVerifying<CABankTran.curyDebitAmt>, object, object>) e).NewValue = e.OldValue;
      throw propertyException;
    }
  }

  [PXInt]
  [PXDBCalced(typeof (Switch<Case<Where<CABankTranSplit.parentTranID, IsNotNull>, CABankTranSplit.parentTranID>, CABankTran.tranID>), typeof (int))]
  protected virtual void _(Events.CacheAttached<CABankTran.sortOrder> e)
  {
  }

  protected virtual PXSetPropertyException VerifyAmout(
    PXCache cache,
    CABankTran row,
    Decimal? newValue,
    Decimal? oldValue)
  {
    CABankTranSplit extension = row != null ? PXCacheEx.GetExtension<CABankTranSplit>((IBqlTable) row) : (CABankTranSplit) null;
    if ((extension != null ? (!extension.ParentTranID.HasValue ? 1 : 0) : 1) != 0)
      return (PXSetPropertyException) null;
    Decimal? curyTranAmt = ((CABankTran) cache.Locate((object) new CABankTran()
    {
      TranID = extension.ParentTranID
    }) ?? CABankTran.PK.Find((PXGraph) this.Base, extension.ParentTranID, (PKFindOptions) 1)).CuryTranAmt;
    Decimal valueOrDefault1 = oldValue.GetValueOrDefault();
    Decimal? nullable1 = curyTranAmt.HasValue ? new Decimal?(curyTranAmt.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
    Decimal valueOrDefault2 = newValue.GetValueOrDefault();
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - valueOrDefault2) : new Decimal?();
    Decimal num = 0M;
    return nullable2.GetValueOrDefault() == num & nullable2.HasValue ? new PXSetPropertyException("Amount of the original transaction cannot be 0. Delete or change the amount of the new transaction.") : (PXSetPropertyException) null;
  }

  [PXOverride]
  public void CATranExt_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CABankTran current = ((PXSelectBase<CABankTran>) this.Base.Details).Current;
    PXEntryStatus status = ((PXSelectBase) this.Base.Details).Cache.GetStatus((object) current);
    if ((status == 2 ? 1 : (status == 4 ? 1 : 0)) == 0 && (current == null || !current.Processed.GetValueOrDefault()))
      return;
    PXUIFieldAttribute.SetEnabled(sender, (string) null, false);
  }

  [PXOverride]
  public void CABankTranInvoiceMatch_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CABankTran current = ((PXSelectBase<CABankTran>) this.Base.Details).Current;
    PXEntryStatus status = ((PXSelectBase) this.Base.Details).Cache.GetStatus((object) current);
    if ((status == 2 ? 1 : (status == 4 ? 1 : 0)) == 0 && (current == null || !current.Processed.GetValueOrDefault()))
      return;
    PXUIFieldAttribute.SetEnabled(sender, (string) null, false);
  }

  public delegate IEnumerable HideDelegate(PXAdapter adapter);

  public delegate IEnumerable<CABankTran> GetUnprocessedTransactionsDelegate();
}
