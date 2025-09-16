// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.AdditionsViewExtensionBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.Common.Utility;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA;

public abstract class AdditionsViewExtensionBase<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  public virtual BqlCommand GetSelectCommand(GLTranFilter filter)
  {
    BqlCommand bqlSelect = ((PXSelectBase) new PXSelectJoin<FAAccrualTran, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<FAAccrualTran.gLTranAccountID>>>>((PXGraph) this.Base)).View.BqlSelect;
    BqlCommand bqlCommand = (!filter.AccountID.HasValue ? bqlSelect.WhereAnd<Where2<Match<PX.Objects.GL.Account, Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And2<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>, And<Where<PX.Objects.GL.Account.curyID, IsNull, Or<PX.Objects.GL.Account.curyID, Equal<Current<Company.baseCuryID>>>>>>>>>() : bqlSelect.WhereAnd<Where<FAAccrualTran.gLTranAccountID, Equal<Current<GLTranFilter.accountID>>>>()).WhereAnd<Where<FAAccrualTran.closedAmt, Less<FAAccrualTran.gLTranAmt>, Or<FAAccrualTran.tranID, IsNull>>>().WhereAnd<Where<Current<GLTranFilter.showReconciled>, Equal<True>, Or<FAAccrualTran.reconciled, NotEqual<True>, Or<FAAccrualTran.reconciled, IsNull>>>>();
    BqlCommand selectCommand = !(filter.ReconType == "+") ? bqlCommand.WhereAnd<Where<FAAccrualTran.gLTranCreditAmt, Greater<decimal0>>>().OrderByNew<OrderBy<Desc<FAAccrualTran.gLTranCreditAmt>>>() : bqlCommand.WhereAnd<Where<FAAccrualTran.gLTranDebitAmt, Greater<decimal0>>>().OrderByNew<OrderBy<Desc<FAAccrualTran.gLTranDebitAmt>>>();
    if (filter.SubID.HasValue)
      selectCommand = selectCommand.WhereAnd<Where<FAAccrualTran.gLTranSubID, Equal<Current<GLTranFilter.subID>>>>();
    return selectCommand;
  }

  public virtual IEnumerable GetFAAccrualTransactions(GLTranFilter filter, PXCache accrualCache)
  {
    AdditionsViewExtensionBase<TGraph> viewExtensionBase = this;
    int startRow = PXView.StartRow;
    int num1 = 0;
    PXView view1 = viewExtensionBase.GetSelectCommand(filter).CreateView((PXGraph) viewExtensionBase.Base, mergeCache: true);
    PXView view2 = view1;
    Dictionary<Type, Type> fieldMap = new Dictionary<Type, Type>();
    fieldMap.Add(typeof (FAAccrualTran.gLTranQty), typeof (FAAccrualTran.gLTranQtyCalc));
    fieldMap.Add(typeof (FAAccrualTran.gLTranAmt), typeof (FAAccrualTran.gLTranAmtCalc));
    fieldMap.Add(typeof (FAAccrualTran.tranID), typeof (FAAccrualTran.gLTranID));
    Type[] typeArray = new Type[1]{ typeof (FAAccrualTran) };
    PXResultMapper pxResultMapper = new PXResultMapper(view2, fieldMap, typeArray);
    foreach (PXResult<FAAccrualTran> pxResult in view1.Select(PXView.Currents, (object[]) null, pxResultMapper.Searches, pxResultMapper.SortColumns, pxResultMapper.Descendings, PXView.PXFilterRowCollection.op_Implicit(pxResultMapper.Filters), ref startRow, PXView.MaximumRows, ref num1))
    {
      FAAccrualTran accrualTransaction = PXResult<FAAccrualTran>.op_Implicit(pxResult);
      if (!accrualTransaction.GLTranAmt.HasValue)
      {
        FAAccrualTran faAccrualTran1 = accrualTransaction;
        Decimal? nullable1;
        Decimal? nullable2;
        if (!accrualTransaction.GLReclassified.GetValueOrDefault())
        {
          Decimal? glTranDebitAmt = accrualTransaction.GLTranDebitAmt;
          nullable1 = accrualTransaction.GLTranCreditAmt;
          nullable2 = glTranDebitAmt.HasValue & nullable1.HasValue ? new Decimal?(glTranDebitAmt.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        }
        else
          nullable2 = accrualTransaction.GLReclassRemainingAmt;
        faAccrualTran1.GLTranAmt = nullable2;
        accrualTransaction.GLTranQty = accrualTransaction.GLTranOrigQty;
        accrualTransaction.SelectedAmt = new Decimal?(0M);
        accrualTransaction.SelectedQty = new Decimal?(0M);
        accrualTransaction.OpenAmt = accrualTransaction.GLTranAmt;
        accrualTransaction.OpenQty = accrualTransaction.GLTranOrigQty;
        accrualTransaction.ClosedAmt = new Decimal?(0M);
        accrualTransaction.ClosedQty = new Decimal?(0M);
        FAAccrualTran faAccrualTran2 = accrualTransaction;
        nullable1 = accrualTransaction.GLTranOrigQty;
        Decimal num2 = 0M;
        Decimal? nullable3;
        if (!(nullable1.GetValueOrDefault() > num2 & nullable1.HasValue))
        {
          nullable3 = accrualTransaction.GLTranAmt;
        }
        else
        {
          nullable1 = accrualTransaction.GLTranAmt;
          Decimal? glTranOrigQty = accrualTransaction.GLTranOrigQty;
          nullable3 = nullable1.HasValue & glTranOrigQty.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / glTranOrigQty.GetValueOrDefault()) : new Decimal?();
        }
        faAccrualTran2.UnitCost = nullable3;
        accrualTransaction.Reconciled = new bool?(false);
        accrualCache.SetStatus((object) accrualTransaction, (PXEntryStatus) 2);
        accrualCache.RaiseRowInserting((object) accrualTransaction);
      }
      else if (accrualTransaction.GLReclassified.GetValueOrDefault())
      {
        Decimal? glTranAmt1 = accrualTransaction.GLTranAmt;
        Decimal? nullable4 = accrualTransaction.GLReclassRemainingAmt;
        if (!(glTranAmt1.GetValueOrDefault() == nullable4.GetValueOrDefault() & glTranAmt1.HasValue == nullable4.HasValue))
        {
          accrualTransaction.GLTranAmt = accrualTransaction.GLReclassRemainingAmt;
          FAAccrualTran faAccrualTran3 = accrualTransaction;
          Decimal? glTranAmt2 = accrualTransaction.GLTranAmt;
          Decimal? nullable5 = accrualTransaction.SelectedAmt;
          nullable4 = glTranAmt2.HasValue & nullable5.HasValue ? new Decimal?(glTranAmt2.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
          Decimal? nullable6 = accrualTransaction.ClosedAmt;
          Decimal? nullable7;
          if (!(nullable4.HasValue & nullable6.HasValue))
          {
            nullable5 = new Decimal?();
            nullable7 = nullable5;
          }
          else
            nullable7 = new Decimal?(nullable4.GetValueOrDefault() + nullable6.GetValueOrDefault());
          faAccrualTran3.OpenAmt = nullable7;
          FAAccrualTran faAccrualTran4 = accrualTransaction;
          nullable6 = accrualTransaction.GLTranOrigQty;
          Decimal num3 = 0M;
          Decimal? nullable8;
          if (!(nullable6.GetValueOrDefault() > num3 & nullable6.HasValue))
          {
            nullable8 = accrualTransaction.GLTranAmt;
          }
          else
          {
            nullable6 = accrualTransaction.GLTranAmt;
            nullable4 = accrualTransaction.GLTranOrigQty;
            if (!(nullable6.HasValue & nullable4.HasValue))
            {
              nullable5 = new Decimal?();
              nullable8 = nullable5;
            }
            else
              nullable8 = new Decimal?(nullable6.GetValueOrDefault() / nullable4.GetValueOrDefault());
          }
          faAccrualTran4.UnitCost = nullable8;
          accrualCache.SetStatus((object) accrualTransaction, (PXEntryStatus) 1);
        }
      }
      yield return (object) accrualTransaction;
    }
    PXView.StartRow = 0;
  }
}
