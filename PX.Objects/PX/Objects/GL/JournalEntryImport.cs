// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.JournalEntryImport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.GL;

public class JournalEntryImport : 
  PXGraph<
  #nullable disable
  JournalEntryImport, GLTrialBalanceImportMap>,
  PXImportAttribute.IPXPrepareItems
{
  private const string _VALIDATE_ACTION = "Validate";
  private const string _MERGE_DUPLICATES_ACTION = "Merge Duplicates";
  private const string _IMPORTTEMPLATE_VIEWNAME = "ImportTemplate";
  private const string _TRAN_REFNUMBER_PREFIX = "";
  private readonly string _mapNumberFieldName;
  private readonly string _importFieldNameAccount;
  [PXHidden]
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  [PXHidden]
  public PXFilter<JournalEntryImport.OperationParam> Operations;
  public PXSelect<GLTrialBalanceImportMap> Map;
  [PXFilterable(new Type[] {})]
  public PXSelect<GLTrialBalanceImportDetails, Where<GLTrialBalanceImportDetails.mapNumber, Equal<Current<GLTrialBalanceImportMap.number>>>> MapDetails;
  [PXImport(typeof (GLTrialBalanceImportMap))]
  public PXSelect<JournalEntryImport.TrialBalanceTemplate, Where<JournalEntryImport.TrialBalanceTemplate.mapNumber, Equal<Current<GLTrialBalanceImportMap.number>>>> ImportTemplate;
  [PXFilterable(new Type[] {})]
  [PXCopyPasteHiddenView]
  public PXSelectOrderBy<JournalEntryImport.GLHistoryEnquiryWithSubResult, OrderBy<Asc<JournalEntryImport.GLHistoryEnquiryWithSubResult.accountID, Asc<JournalEntryImport.GLHistoryEnquiryWithSubResult.subID>>>> Exceptions;
  public PXSetup<PX.Objects.GL.Ledger, Where<PX.Objects.GL.Ledger.ledgerID, Equal<Optional<GLTrialBalanceImportMap.ledgerID>>>> Ledger;
  public PXInitializeState<GLTrialBalanceImportMap> initializeState;
  public PXAction<GLTrialBalanceImportMap> putOnHold;
  public PXAction<GLTrialBalanceImportMap> releaseFromHold;
  public PXAction<GLTrialBalanceImportMap> process;
  public PXAction<GLTrialBalanceImportMap> processAll;
  public PXAction<GLTrialBalanceImportMap> validate;
  public PXAction<GLTrialBalanceImportMap> mergeDuplicates;
  public PXAction<GLTrialBalanceImportMap> release;

  public JournalEntryImport()
  {
    ((PXSelectBase) this.ImportTemplate).GetAttribute<PXImportAttribute>().MappingPropertiesInit += new EventHandler<PXImportAttribute.MappingPropertiesInitEventArgs>(this.MappingPropertiesInit);
    this._mapNumberFieldName = ((PXSelectBase) this.ImportTemplate).Cache.GetField(typeof (JournalEntryImport.TrialBalanceTemplate.mapNumber));
    this._importFieldNameAccount = ((PXSelectBase) this.ImportTemplate).Cache.GetField(typeof (GLTrialBalanceImportDetails.importAccountCD));
    ((PXSelectBase) this.MapDetails).Cache.AllowInsert = true;
    ((PXSelectBase) this.MapDetails).Cache.AllowUpdate = true;
    ((PXSelectBase) this.MapDetails).Cache.AllowDelete = true;
    OpenPeriodAttribute.SetValidatePeriod<GLTrialBalanceImportMap.finPeriodID>(((PXSelectBase) this.Map).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    PXUIFieldAttribute.SetEnabled<GLTrialBalanceImportDetails.selected>(((PXSelectBase) this.MapDetails).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<GLTrialBalanceImportDetails.importBranchCD>(((PXSelectBase) this.MapDetails).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<GLTrialBalanceImportDetails.importAccountCD>(((PXSelectBase) this.MapDetails).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<GLTrialBalanceImportDetails.importSubAccountCD>(((PXSelectBase) this.MapDetails).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<GLTrialBalanceImportDetails.ytdBalance>(((PXSelectBase) this.MapDetails).Cache, (object) null, true);
    PXUIFieldAttribute.SetReadOnly<GLTrialBalanceImportDetails.status>(((PXSelectBase) this.MapDetails).Cache, (object) null);
    PXUIFieldAttribute.SetEnabled<JournalEntryImport.TrialBalanceTemplate.mapNumber>(((PXSelectBase) this.ImportTemplate).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<JournalEntryImport.TrialBalanceTemplate.mapBranchID>(((PXSelectBase) this.ImportTemplate).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<JournalEntryImport.TrialBalanceTemplate.mapAccountID>(((PXSelectBase) this.ImportTemplate).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<JournalEntryImport.TrialBalanceTemplate.mapSubAccountID>(((PXSelectBase) this.ImportTemplate).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<JournalEntryImport.TrialBalanceTemplate.selected>(((PXSelectBase) this.ImportTemplate).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<JournalEntryImport.TrialBalanceTemplate.status>(((PXSelectBase) this.ImportTemplate).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<JournalEntryImport.TrialBalanceTemplate.description>(((PXSelectBase) this.ImportTemplate).Cache, (object) null, false);
    PXUIFieldAttribute.SetDisplayName<JournalEntryImport.TrialBalanceTemplate.importBranchCD>(((PXSelectBase) this.ImportTemplate).Cache, "Branch");
    PXUIFieldAttribute.SetDisplayName<JournalEntryImport.TrialBalanceTemplate.importAccountCD>(((PXSelectBase) this.ImportTemplate).Cache, "Account");
    PXUIFieldAttribute.SetDisplayName<JournalEntryImport.TrialBalanceTemplate.importSubAccountCD>(((PXSelectBase) this.ImportTemplate).Cache, "Subaccount");
    PXUIFieldAttribute.SetReadOnly(((PXSelectBase) this.Exceptions).Cache, (object) null);
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "Process")]
  [PXButton]
  protected virtual IEnumerable Process(PXAdapter adapter)
  {
    if (this.CanEdit)
    {
      Dictionary<int, GLTrialBalanceImportDetails> dict = new Dictionary<int, GLTrialBalanceImportDetails>();
      foreach (PXResult<GLTrialBalanceImportDetails> pxResult in ((PXSelectBase<GLTrialBalanceImportDetails>) this.MapDetails).Select(new object[1]
      {
        (object) ((PXSelectBase<JournalEntryImport.OperationParam>) this.Operations).Current.Action
      }))
      {
        GLTrialBalanceImportDetails balanceImportDetails = PXResult<GLTrialBalanceImportDetails>.op_Implicit(pxResult);
        if (balanceImportDetails.Selected.GetValueOrDefault() && balanceImportDetails.Line.HasValue)
        {
          int key = balanceImportDetails.Line.Value;
          if (!dict.ContainsKey(key))
            dict.Add(key, (GLTrialBalanceImportDetails) ((PXSelectBase) this.MapDetails).Cache.CreateCopy((object) balanceImportDetails));
        }
      }
      if (((PXSelectBase<JournalEntryImport.OperationParam>) this.Operations).Current != null)
        this.ProcessHandler(dict, ((PXSelectBase<JournalEntryImport.OperationParam>) this.Operations).Current.Action, true);
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Process All")]
  [PXButton]
  protected virtual IEnumerable ProcessAll(PXAdapter adapter)
  {
    if (this.CanEdit)
    {
      Dictionary<int, GLTrialBalanceImportDetails> dict = new Dictionary<int, GLTrialBalanceImportDetails>();
      foreach (PXResult<GLTrialBalanceImportDetails> pxResult in ((PXSelectBase<GLTrialBalanceImportDetails>) this.MapDetails).Select(new object[1]
      {
        (object) ((PXSelectBase<JournalEntryImport.OperationParam>) this.Operations).Current.Action
      }))
      {
        GLTrialBalanceImportDetails balanceImportDetails = PXResult<GLTrialBalanceImportDetails>.op_Implicit(pxResult);
        int? line = balanceImportDetails.Line;
        if (line.HasValue)
        {
          line = balanceImportDetails.Line;
          int key = line.Value;
          if (!dict.ContainsKey(key))
          {
            balanceImportDetails.Selected = new bool?(true);
            dict.Add(key, (GLTrialBalanceImportDetails) ((PXSelectBase) this.MapDetails).Cache.CreateCopy((object) balanceImportDetails));
          }
        }
      }
      if (((PXSelectBase<JournalEntryImport.OperationParam>) this.Operations).Current != null)
        this.ProcessHandler(dict, ((PXSelectBase<JournalEntryImport.OperationParam>) this.Operations).Current.Action, true);
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Validate")]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual IEnumerable Validate(PXAdapter adapter)
  {
    if (this.CanEdit)
    {
      PXResultset<GLTrialBalanceImportDetails> pxResultset = ((PXSelectBase<GLTrialBalanceImportDetails>) this.MapDetails).Select(Array.Empty<object>());
      bool flag = !GraphHelper.RowCast<GLTrialBalanceImportDetails>((IEnumerable) pxResultset).Where<GLTrialBalanceImportDetails>((Func<GLTrialBalanceImportDetails, bool>) (row => row.Selected.GetValueOrDefault())).Any<GLTrialBalanceImportDetails>();
      Dictionary<int, GLTrialBalanceImportDetails> dict = new Dictionary<int, GLTrialBalanceImportDetails>();
      if (!flag || ((PXSelectBase<GLTrialBalanceImportDetails>) this.MapDetails).Ask("Validate Records", "All records will be processed, because nothing was selected in the table.", (MessageButtons) 1) == 1)
      {
        foreach (PXResult<GLTrialBalanceImportDetails> pxResult in pxResultset)
        {
          GLTrialBalanceImportDetails balanceImportDetails = PXResult<GLTrialBalanceImportDetails>.op_Implicit(pxResult);
          if (flag || balanceImportDetails.Selected.GetValueOrDefault())
          {
            int? line = balanceImportDetails.Line;
            if (line.HasValue)
            {
              line = balanceImportDetails.Line;
              int key = line.Value;
              if (!dict.ContainsKey(key))
                dict.Add(key, (GLTrialBalanceImportDetails) ((PXSelectBase) this.MapDetails).Cache.CreateCopy((object) balanceImportDetails));
            }
          }
        }
      }
      if (dict.Count > 0)
        this.ProcessHandler(dict, nameof (Validate), true);
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Merge Duplicates")]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual IEnumerable MergeDuplicates(PXAdapter adapter)
  {
    if (this.CanEdit)
    {
      PXResultset<GLTrialBalanceImportDetails> pxResultset = ((PXSelectBase<GLTrialBalanceImportDetails>) this.MapDetails).Select(Array.Empty<object>());
      bool flag = !GraphHelper.RowCast<GLTrialBalanceImportDetails>((IEnumerable) pxResultset).Where<GLTrialBalanceImportDetails>((Func<GLTrialBalanceImportDetails, bool>) (row => row.Selected.GetValueOrDefault())).Any<GLTrialBalanceImportDetails>();
      Dictionary<int, GLTrialBalanceImportDetails> dict = new Dictionary<int, GLTrialBalanceImportDetails>();
      if (!flag || ((PXSelectBase<GLTrialBalanceImportDetails>) this.MapDetails).Ask("Merge Duplicates", "All records will be processed, because nothing was selected in the table.", (MessageButtons) 1) == 1)
      {
        foreach (PXResult<GLTrialBalanceImportDetails> pxResult in pxResultset)
        {
          GLTrialBalanceImportDetails balanceImportDetails = PXResult<GLTrialBalanceImportDetails>.op_Implicit(pxResult);
          if (flag || balanceImportDetails.Selected.GetValueOrDefault())
          {
            int? line = balanceImportDetails.Line;
            if (line.HasValue)
            {
              line = balanceImportDetails.Line;
              int key = line.Value;
              if (!dict.ContainsKey(key))
                dict.Add(key, (GLTrialBalanceImportDetails) ((PXSelectBase) this.MapDetails).Cache.CreateCopy((object) balanceImportDetails));
            }
          }
        }
      }
      if (dict.Count > 0)
        this.ProcessHandler(dict, "Merge Duplicates", true);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Release(PXAdapter adapter)
  {
    JournalEntryImport graph = this;
    GLTrialBalanceImportMap current = ((PXSelectBase<GLTrialBalanceImportMap>) graph.Map).Current;
    if (current != null)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      JournalEntryImport.\u003C\u003Ec__DisplayClass32_0 cDisplayClass320 = new JournalEntryImport.\u003C\u003Ec__DisplayClass32_0();
      Decimal? nullable = !(current.Status != "B") ? current.CreditTotalBalance : throw new PXException("Status invalid for processing.");
      Decimal? totalBalance1 = current.TotalBalance;
      if (!(nullable.GetValueOrDefault() == totalBalance1.GetValueOrDefault() & nullable.HasValue == totalBalance1.HasValue))
        throw new Exception("Document is out of balance, please review.");
      Decimal? debitTotalBalance = current.DebitTotalBalance;
      Decimal? totalBalance2 = current.TotalBalance;
      if (!(debitTotalBalance.GetValueOrDefault() == totalBalance2.GetValueOrDefault() & debitTotalBalance.HasValue == totalBalance2.HasValue))
        throw new Exception("Document is out of balance, please review.");
      if (PXSelectBase<GLTrialBalanceImportMap, PXSelectJoin<GLTrialBalanceImportMap, InnerJoin<Batch, On<Batch.batchNbr, Equal<GLTrialBalanceImportMap.batchNbr>, And<Batch.module, Equal<BatchModule.moduleGL>, And<Batch.posted, Equal<False>>>>>, Where<GLTrialBalanceImportMap.finPeriodID, LessEqual<Current<GLTrialBalanceImportMap.finPeriodID>>>>.Config>.SelectSingleBound((PXGraph) graph, (object[]) null, (object[]) null).Count > 0)
        throw new Exception("The trial balance cannot be released until there are unposted General Ledger batches.");
      ((PXAction) graph.Save).Press();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass320.isUnsignOperations = JournalEntryImport.IsUnsignOperations(graph);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass320.mapNumber = (object) ((PXSelectBase<GLTrialBalanceImportMap>) graph.Map).Current.Number;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) graph, new PXToggleAsyncDelegate((object) cDisplayClass320, __methodptr(\u003CRelease\u003Eb__0)));
    }
    yield return (object) ((PXSelectBase<GLTrialBalanceImportMap>) graph.Map).Current;
  }

  protected virtual void mapDetails([PXString] ref string action)
  {
    if (action == null)
      return;
    ((PXSelectBase<JournalEntryImport.OperationParam>) this.Operations).Current.Action = action;
  }

  protected virtual IEnumerable exceptions()
  {
    JournalEntryImport graph = this;
    if (((PXSelectBase<GLTrialBalanceImportMap>) graph.Map).Current != null)
    {
      foreach (JournalEntryImport.GLHistoryEnquiryWithSubResult balance in JournalEntryImport.GetBalances(graph, ((PXSelectBase<GLTrialBalanceImportMap>) graph.Map).Current.OrgBAccountID, ((PXSelectBase<GLTrialBalanceImportMap>) graph.Map).Current.LedgerID, ((PXSelectBase<GLTrialBalanceImportMap>) graph.Map).Current.FinPeriodID, ((PXSelectBase<GLTrialBalanceImportMap>) graph.Map).Current.BegFinPeriod))
      {
        Decimal? endBalance = balance.EndBalance;
        Decimal num = 0M;
        if (!(endBalance.GetValueOrDefault() == num & endBalance.HasValue))
        {
          if (PXSelectBase<GLTrialBalanceImportDetails, PXSelect<GLTrialBalanceImportDetails, Where<GLTrialBalanceImportDetails.mapNumber, Equal<Required<GLTrialBalanceImportDetails.mapNumber>>, And<GLTrialBalanceImportDetails.mapBranchID, Equal<Required<GLTrialBalanceImportDetails.mapBranchID>>, And<GLTrialBalanceImportDetails.mapAccountID, Equal<Required<GLTrialBalanceImportDetails.mapAccountID>>, And<GLTrialBalanceImportDetails.mapSubAccountID, Equal<Required<GLTrialBalanceImportDetails.mapSubAccountID>>>>>>>.Config>.Select((PXGraph) graph, new object[4]
          {
            (object) ((PXSelectBase<GLTrialBalanceImportMap>) graph.Map).Current.Number,
            (object) balance.BranchID,
            (object) balance.AccountID,
            (object) balance.SubID
          }).Count <= 0)
            yield return (object) balance;
        }
      }
    }
  }

  protected virtual void ProcessHandler(
    Dictionary<int, GLTrialBalanceImportDetails> dict,
    string operation,
    bool update)
  {
    switch (operation)
    {
      case "Validate":
        foreach (GLTrialBalanceImportDetails data in dict.Values)
        {
          bool flag1 = JournalEntryImport.SetValue<GLTrialBalanceImportDetails.importBranchCD, GLTrialBalanceImportDetails.mapBranchID>(((PXSelectBase) this.MapDetails).Cache, data, "The branch cannot be mapped.", "The branch cannot be empty.");
          bool flag2 = true;
          bool flag3 = JournalEntryImport.SetValue<GLTrialBalanceImportDetails.importAccountCD, GLTrialBalanceImportDetails.mapAccountID>(((PXSelectBase) this.MapDetails).Cache, data, "The account cannot be mapped.", "The account cannot be empty.");
          if (!flag3)
          {
            data.MapSubAccountID = new int?();
            PersistErrorAttribute.ClearError<GLTrialBalanceImportDetails.importSubAccountCD>(((PXSelectBase) this.MapDetails).Cache, (object) data);
          }
          else if (PXAccess.FeatureInstalled<FeaturesSet.subAccount>())
            flag2 = JournalEntryImport.SetValue<GLTrialBalanceImportDetails.importSubAccountCD, GLTrialBalanceImportDetails.mapSubAccountID>(((PXSelectBase) this.MapDetails).Cache, data, (string) null, "The subaccount cannot be empty.");
          data.Status = new int?(flag1 & flag3 & flag2 ? 1 : 3);
        }
        foreach (GLTrialBalanceImportDetails balanceImportDetails in dict.Values)
        {
          if (this.SearchDuplicates(balanceImportDetails).Count >= 2 && balanceImportDetails.Status.GetValueOrDefault() != 3)
            balanceImportDetails.Status = new int?(2);
          if (update)
            ((PXSelectBase) this.MapDetails).Cache.Update((object) balanceImportDetails);
        }
        GLTrialBalanceImportMap current1 = (GLTrialBalanceImportMap) ((PXSelectBase) this.Map).Cache.Current;
        PXFormulaAttribute.CalcAggregate<GLTrialBalanceImportDetails.ytdBalance>(((PXSelectBase) this.MapDetails).Cache, (object) current1);
        current1.DebitTotalBalance = (Decimal?) PXFormulaAttribute.Evaluate<GLTrialBalanceImportMap.debitTotalBalance>(((PXSelectBase) this.Map).Cache, (object) current1);
        current1.CreditTotalBalance = (Decimal?) PXFormulaAttribute.Evaluate<GLTrialBalanceImportMap.creditTotalBalance>(((PXSelectBase) this.Map).Cache, (object) current1);
        if (!this.IsRequireControlTotal)
          current1.TotalBalance = current1.CreditTotalBalance;
        ((PXSelectBase) this.Map).Cache.Update((object) current1);
        break;
      case "Merge Duplicates":
        using (Dictionary<int, GLTrialBalanceImportDetails>.ValueCollection.Enumerator enumerator = dict.Values.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            GLTrialBalanceImportDetails current2 = enumerator.Current;
            PXEntryStatus status = ((PXSelectBase) this.MapDetails).Cache.GetStatus((object) current2);
            if (status != 3 && status != 4)
            {
              int? nullable1;
              foreach (PXResult<GLTrialBalanceImportDetails> searchDuplicate in this.SearchDuplicates(current2))
              {
                GLTrialBalanceImportDetails balanceImportDetails1 = PXResult<GLTrialBalanceImportDetails>.op_Implicit(searchDuplicate);
                int? line = balanceImportDetails1.Line;
                if (line.HasValue)
                {
                  line = balanceImportDetails1.Line;
                  nullable1 = current2.Line;
                  if (!(line.GetValueOrDefault() == nullable1.GetValueOrDefault() & line.HasValue == nullable1.HasValue))
                  {
                    Dictionary<int, GLTrialBalanceImportDetails> dictionary = dict;
                    nullable1 = balanceImportDetails1.Line;
                    int key = nullable1.Value;
                    if (dictionary.ContainsKey(key))
                    {
                      GLTrialBalanceImportDetails balanceImportDetails2 = current2;
                      Decimal? nullable2 = balanceImportDetails2.YtdBalance;
                      Decimal? ytdBalance = balanceImportDetails1.YtdBalance;
                      balanceImportDetails2.YtdBalance = nullable2.HasValue & ytdBalance.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + ytdBalance.GetValueOrDefault()) : new Decimal?();
                      GLTrialBalanceImportDetails balanceImportDetails3 = current2;
                      Decimal? curyYtdBalance = balanceImportDetails3.CuryYtdBalance;
                      nullable2 = balanceImportDetails1.CuryYtdBalance;
                      balanceImportDetails3.CuryYtdBalance = curyYtdBalance.HasValue & nullable2.HasValue ? new Decimal?(curyYtdBalance.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
                      ((PXSelectBase) this.MapDetails).Cache.Delete((object) balanceImportDetails1);
                    }
                  }
                }
              }
              nullable1 = current2.Status;
              if (nullable1.GetValueOrDefault() != 3)
              {
                int num1;
                if (!string.IsNullOrEmpty(current2.ImportBranchCD))
                {
                  nullable1 = current2.MapBranchID;
                  num1 = !nullable1.HasValue ? 1 : 0;
                }
                else
                  num1 = 0;
                bool flag4 = num1 != 0;
                int num2;
                if (!string.IsNullOrEmpty(current2.ImportAccountCD))
                {
                  nullable1 = current2.MapAccountID;
                  num2 = !nullable1.HasValue ? 1 : 0;
                }
                else
                  num2 = 0;
                bool flag5 = num2 != 0;
                int num3;
                if (!string.IsNullOrEmpty(current2.ImportSubAccountCD))
                {
                  nullable1 = current2.MapSubAccountID;
                  num3 = !nullable1.HasValue ? 1 : 0;
                }
                else
                  num3 = 0;
                bool flag6 = num3 != 0;
                current2.Status = new int?(flag4 | flag5 | flag6 ? 0 : 1);
              }
              ((PXSelectBase) this.MapDetails).Cache.Update((object) current2);
            }
          }
          break;
        }
    }
  }

  private void MappingPropertiesInit(
    object sender,
    PXImportAttribute.MappingPropertiesInitEventArgs e)
  {
    if (this.IsCuryYtdBalanceFieldUsable())
      return;
    string field = ((PXSelectBase) this.MapDetails).Cache.GetField(typeof (GLTrialBalanceImportDetails.curyYtdBalance));
    e.Names.Remove(field);
    string displayName = PXUIFieldAttribute.GetDisplayName<GLTrialBalanceImportDetails.curyYtdBalance>(((PXSelectBase) this.MapDetails).Cache);
    e.DisplayNames.Remove(displayName);
  }

  protected virtual void GLTrialBalanceImportMap_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    GLTrialBalanceImportMap row = (GLTrialBalanceImportMap) e.Row;
    bool flag1 = JournalEntryImport.IsEditable(row);
    if (flag1)
      this.CheckTotalBalance(sender, row, this.IsRequireControlTotal);
    PXUIFieldAttribute.SetVisible<GLTrialBalanceImportMap.totalBalance>(((PXSelectBase) this.Map).Cache, (object) null, this.IsRequireControlTotal);
    PXUIFieldAttribute.SetEnabled<GLTrialBalanceImportMap.totalBalance>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<GLTrialBalanceImportMap.importDate>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<GLTrialBalanceImportMap.finPeriodID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<GLTrialBalanceImportMap.description>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<GLTrialBalanceImportMap.ledgerID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<GLTrialBalanceImportMap.orgBAccountID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<GLTrialBalanceImportMap.isHold>(sender, (object) row, flag1);
    ((PXSelectBase) this.Map).Cache.AllowDelete = flag1;
    ((PXSelectBase) this.Map).Cache.AllowUpdate = flag1;
    ((PXSelectBase) this.MapDetails).Cache.AllowInsert = flag1;
    ((PXSelectBase) this.MapDetails).Cache.AllowUpdate = flag1;
    ((PXSelectBase) this.MapDetails).Cache.AllowDelete = flag1;
    ((PXGraph) this).Actions["Process"].SetEnabled(flag1);
    ((PXGraph) this).Actions["ProcessAll"].SetEnabled(flag1);
    PXImportAttribute.SetEnabled((PXGraph) this, "ImportTemplate", flag1);
    PXUIFieldAttribute.SetVisible<GLTrialBalanceImportDetails.curyYtdBalance>(((PXSelectBase) this.MapDetails).Cache, (object) null, this.IsCuryYtdBalanceFieldUsable());
    bool flag2 = this.IsBranchesNotBalancing(row.OrgBAccountID);
    PXUIFieldAttribute.SetEnabled<GLTrialBalanceImportDetails.importBranchCD>(((PXSelectBase) this.MapDetails).Cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<GLTrialBalanceImportDetails.importBranchCD>(((PXSelectBase) this.MapDetails).Cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<GLTrialBalanceImportDetails.mapBranchID>(((PXSelectBase) this.MapDetails).Cache, (object) null, flag2);
    PXUIFieldAttribute.SetEnabled<JournalEntryImport.TrialBalanceTemplate.importBranchCD>(((PXSelectBase) this.ImportTemplate).Cache, (object) null, flag2);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<GLTrialBalanceImportMap, GLTrialBalanceImportMap.orgBAccountID> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<GLTrialBalanceImportMap, GLTrialBalanceImportMap.orgBAccountID>, GLTrialBalanceImportMap, object>) e).NewValue == null)
      return;
    int? newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<GLTrialBalanceImportMap, GLTrialBalanceImportMap.orgBAccountID>, GLTrialBalanceImportMap, object>) e).NewValue as int?;
    PX.Objects.GL.DAC.Organization organization = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelect<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.bAccountID, Equal<Required<PX.Objects.GL.DAC.Organization.bAccountID>>, And<Where<PX.Objects.GL.DAC.Organization.bAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>, Or<PX.Objects.GL.DAC.Organization.active, NotEqual<True>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) newValue
    }));
    if (organization != null)
    {
      if (!organization.Active.GetValueOrDefault())
      {
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<GLTrialBalanceImportMap, GLTrialBalanceImportMap.orgBAccountID>, GLTrialBalanceImportMap, object>) e).NewValue = (object) PXOrgAccess.GetCD(newValue);
        throw new PXSetPropertyException("The entity selected in the Company/Branch box is inactive.");
      }
      if (organization.OrganizationType == "Balancing")
      {
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<GLTrialBalanceImportMap, GLTrialBalanceImportMap.orgBAccountID>, GLTrialBalanceImportMap, object>) e).NewValue = (object) PXOrgAccess.GetCD(newValue);
        throw new PXSetPropertyException("A company with the With Branches Requiring Balancing type cannot be selected.");
      }
    }
    else
    {
      Branch branch = PXResultset<Branch>.op_Implicit(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.bAccountID, Equal<Required<Branch.bAccountID>>, And<Where<Branch.bAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>, Or<Branch.active, NotEqual<True>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
      {
        (object) newValue
      }));
      if (branch != null)
      {
        if (!branch.Active.GetValueOrDefault())
        {
          ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<GLTrialBalanceImportMap, GLTrialBalanceImportMap.orgBAccountID>, GLTrialBalanceImportMap, object>) e).NewValue = (object) PXOrgAccess.GetCD(newValue);
          throw new PXSetPropertyException("The entity selected in the Company/Branch box is inactive.");
        }
        PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) this, branch.OrganizationID);
        if (organizationById != null && organizationById.OrganizationType == "NotBalancing")
        {
          ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<GLTrialBalanceImportMap, GLTrialBalanceImportMap.orgBAccountID>, GLTrialBalanceImportMap, object>) e).NewValue = (object) PXOrgAccess.GetCD(newValue);
          throw new PXSetPropertyException("A branch of a company with the With Branches Not Requiring Balancing type cannot be selected.");
        }
      }
      else
      {
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<GLTrialBalanceImportMap, GLTrialBalanceImportMap.orgBAccountID>, GLTrialBalanceImportMap, object>) e).NewValue = (object) PXOrgAccess.GetCD(newValue);
        throw new PXSetPropertyException("The entity specified in the Company/Branch box cannot be found in the system.");
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<GLTrialBalanceImportMap, GLTrialBalanceImportMap.orgBAccountID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<GLTrialBalanceImportMap, GLTrialBalanceImportMap.orgBAccountID>>) e).Cache.SetDefaultExt<GLTrialBalanceImportMap.ledgerID>((object) e.Row);
    foreach (PXResult<GLTrialBalanceImportDetails> pxResult in ((PXSelectBase<GLTrialBalanceImportDetails>) this.MapDetails).Select(Array.Empty<object>()))
    {
      GLTrialBalanceImportDetails copy = (GLTrialBalanceImportDetails) ((PXSelectBase) this.MapDetails).Cache.CreateCopy((object) PXResult<GLTrialBalanceImportDetails>.op_Implicit(pxResult));
      copy.Status = new int?(0);
      copy.ImportBranchCDError = (string) null;
      copy.MapBranchID = new int?();
      copy.ImportAccountCDError = (string) null;
      copy.MapAccountID = new int?();
      copy.ImportSubAccountCDError = (string) null;
      copy.MapSubAccountID = new int?();
      copy.AccountType = (string) null;
      copy.Description = (string) null;
      if (!this.IsBranchesNotBalancing(e.Row.OrgBAccountID))
        ((PXSelectBase) this.MapDetails).Cache.SetDefaultExt<GLTrialBalanceImportDetails.importBranchCD>((object) copy);
      ((PXSelectBase<GLTrialBalanceImportDetails>) this.MapDetails).Update(copy);
    }
    ((PXSelectBase<GLTrialBalanceImportMap>) this.Map).Current.DebitTotalBalance = new Decimal?(0M);
    ((PXSelectBase<GLTrialBalanceImportMap>) this.Map).Current.CreditTotalBalance = new Decimal?(0M);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<JournalEntryImport.TrialBalanceTemplate, JournalEntryImport.TrialBalanceTemplate.importBranchCD> e)
  {
    if (!this.IsBranchesNotBalancing(((PXSelectBase<GLTrialBalanceImportMap>) this.Map).Current.OrgBAccountID))
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<JournalEntryImport.TrialBalanceTemplate, JournalEntryImport.TrialBalanceTemplate.importBranchCD>, JournalEntryImport.TrialBalanceTemplate, object>) e).NewValue = (object) null;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<JournalEntryImport.TrialBalanceTemplate, JournalEntryImport.TrialBalanceTemplate.importBranchCD>>) e).Cancel = true;
  }

  protected virtual void TrialBalanceTemplate_ImportAccountCD_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void TrialBalanceTemplate_ImportSubAccountCD_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void TrialBalanceTemplate_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    ((PXSelectBase<GLTrialBalanceImportDetails>) this.MapDetails).Update((GLTrialBalanceImportDetails) e.Row);
  }

  protected virtual void TrialBalanceTemplate_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase<GLTrialBalanceImportDetails>) this.MapDetails).Update((GLTrialBalanceImportDetails) e.Row);
  }

  protected virtual void TrialBalanceTemplate_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<GLTrialBalanceImportDetails, GLTrialBalanceImportDetails.importBranchCD> e)
  {
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<GLTrialBalanceImportDetails, GLTrialBalanceImportDetails.importBranchCD>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<GLTrialBalanceImportDetails, GLTrialBalanceImportDetails.mapBranchID> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<GLTrialBalanceImportDetails, GLTrialBalanceImportDetails.mapBranchID>, GLTrialBalanceImportDetails, object>) e).NewValue == null)
      return;
    bool flag = false;
    int? organizationId = (int?) ((PXAccess.Organization) PXAccess.GetOrganizationByBAccountID(((PXSelectBase<GLTrialBalanceImportMap>) this.Map).Current.OrgBAccountID))?.OrganizationID;
    if (organizationId.HasValue && OrganizationMaint.FindOrganizationByID((PXGraph) this, organizationId).OrganizationType == "NotBalancing")
      flag = true;
    if (!flag)
      return;
    if (PXResultset<Branch>.op_Implicit(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.branchID, Equal<Required<Branch.branchID>>, And<Branch.organizationID, Equal<Required<Branch.organizationID>>, And<Where<Branch.bAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<GLTrialBalanceImportDetails, GLTrialBalanceImportDetails.mapBranchID>, GLTrialBalanceImportDetails, object>) e).NewValue,
      (object) organizationId
    })) == null)
      throw new PXSetPropertyException("The branch cannot be mapped.");
  }

  protected virtual void GLTrialBalanceImportDetails_ImportAccountCD_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTrialBalanceImportDetails_ImportSubAccountCD_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTrialBalanceImportDetails_MapSubAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is GLTrialBalanceImportDetails row) || !row.MapAccountID.HasValue || e.NewValue == null)
      return;
    if (!PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Required<Account.accountID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) row.MapAccountID
    })).IsCashAccount.GetValueOrDefault())
      return;
    if (PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.accountID, Equal<Required<CashAccount.accountID>>, And<CashAccount.subID, Equal<Required<CashAccount.subID>>>>>.Config>.Select(sender.Graph, new object[2]
    {
      (object) row.MapAccountID,
      (object) (int?) e.NewValue
    })) == null)
      throw new PXSetPropertyException("Specified Subaccount cannot be used with this Cash Account.");
  }

  protected virtual void GLTrialBalanceImportDetails_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    GLTrialBalanceImportDetails row = (GLTrialBalanceImportDetails) e.Row;
    if (row == null)
      return;
    this.CheckMappingAndBalance(sender, ((PXSelectBase<GLTrialBalanceImportMap>) this.Map).Current, row);
    PXUIFieldAttribute.SetEnabled<GLTrialBalanceImportDetails.curyYtdBalance>(sender, (object) row, this.IsCuryYtdBalanceFieldUsable() && !string.IsNullOrEmpty(row.AccountCuryID) && row.AccountCuryID != ((PXSelectBase<PX.Objects.GL.Ledger>) this.Ledger).Current?.BaseCuryID);
  }

  protected virtual void GLTrialBalanceImportDetails_RowUpdated(
    PXCache sender,
    PXRowUpdatedEventArgs e)
  {
    GLTrialBalanceImportDetails oldRow = (GLTrialBalanceImportDetails) e.OldRow;
    GLTrialBalanceImportDetails row = (GLTrialBalanceImportDetails) e.Row;
    if (oldRow == null || row == null)
      return;
    bool flag = false;
    if (row.ImportBranchCD != oldRow.ImportBranchCD)
    {
      flag = true;
      row.ImportBranchCDError = (string) null;
      row.MapBranchID = new int?();
    }
    if (row.ImportAccountCD != oldRow.ImportAccountCD)
    {
      flag = true;
      row.ImportAccountCDError = (string) null;
      row.MapAccountID = new int?();
      row.ImportSubAccountCDError = (string) null;
      row.MapSubAccountID = new int?();
      row.AccountType = (string) null;
      row.Description = (string) null;
    }
    if (row.ImportSubAccountCD != oldRow.ImportSubAccountCD)
    {
      flag = true;
      row.ImportSubAccountCDError = (string) null;
      row.MapSubAccountID = new int?();
    }
    if (flag)
      this.ProcessRow(row);
    if (row.ImportAccountCD == null && ((PXSelectBase<PX.Objects.GL.Ledger>) this.Ledger).Current == null || row.AccountCuryID != null)
      return;
    row.CuryYtdBalance = row.YtdBalance;
  }

  protected virtual void GLTrialBalanceImportDetails_RowInserted(
    PXCache sender,
    PXRowInsertedEventArgs e)
  {
    GLTrialBalanceImportDetails row = (GLTrialBalanceImportDetails) e.Row;
    if (row == null || row.ImportAccountCD == null && ((PXSelectBase<PX.Objects.GL.Ledger>) this.Ledger).Current == null || row.AccountCuryID != null)
      return;
    row.CuryYtdBalance = row.YtdBalance;
  }

  protected virtual void GLTrialBalanceImportDetails_RowDeleted(
    PXCache sender,
    PXRowDeletedEventArgs e)
  {
    GLTrialBalanceImportDetails row = (GLTrialBalanceImportDetails) e.Row;
  }

  protected virtual void GLTrialBalanceImportDetails_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    GLTrialBalanceImportDetails row = (GLTrialBalanceImportDetails) e.Row;
    if (row == null || e.Operation == 3)
      return;
    this.CheckMappingAndBalance(sender, ((PXSelectBase<GLTrialBalanceImportMap>) this.Map).Current, row);
  }

  protected virtual void GLTrialBalanceImportMap_IsHold_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    GLTrialBalanceImportMap row1 = (GLTrialBalanceImportMap) e.Row;
    if (row1 == null || row1.IsHold.GetValueOrDefault())
      return;
    foreach (PXResult<GLTrialBalanceImportDetails> pxResult in ((PXSelectBase<GLTrialBalanceImportDetails>) this.MapDetails).Select(Array.Empty<object>()))
    {
      GLTrialBalanceImportDetails row2 = PXResult<GLTrialBalanceImportDetails>.op_Implicit(pxResult);
      if (!this.CheckMappingAndBalance(((PXSelectBase) this.MapDetails).Cache, row1, row2))
      {
        ((PXSelectBase) this.MapDetails).Cache.Update((object) row2);
        break;
      }
    }
  }

  protected virtual void GLTrialBalanceImportMap_LedgerID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (this.IsCuryYtdBalanceFieldUsable())
      return;
    foreach (GLTrialBalanceImportDetails balanceImportDetails in GraphHelper.RowCast<GLTrialBalanceImportDetails>((IEnumerable) ((PXSelectBase<GLTrialBalanceImportDetails>) this.MapDetails).Select(Array.Empty<object>())))
    {
      balanceImportDetails.CuryYtdBalance = balanceImportDetails.YtdBalance;
      ((PXSelectBase<GLTrialBalanceImportDetails>) this.MapDetails).Update(balanceImportDetails);
    }
  }

  private bool IsCuryYtdBalanceFieldUsable()
  {
    return ((PXSelectBase<PX.Objects.GL.Ledger>) this.Ledger).Current == null || ((PXSelectBase<PX.Objects.GL.Ledger>) this.Ledger).Current.BalanceType != "R";
  }

  private PXResultset<GLTrialBalanceImportDetails> SearchDuplicates(GLTrialBalanceImportDetails item)
  {
    ((PXSelectBase) this.MapDetails).Cache.ClearQueryCacheObsolete();
    return PXAccess.FeatureInstalled<FeaturesSet.subAccount>() ? PXSelectBase<GLTrialBalanceImportDetails, PXSelect<GLTrialBalanceImportDetails, Where<GLTrialBalanceImportDetails.mapNumber, Equal<Required<GLTrialBalanceImportDetails.mapNumber>>, And<GLTrialBalanceImportDetails.importBranchCD, Equal<Required<GLTrialBalanceImportDetails.importBranchCD>>, And<GLTrialBalanceImportDetails.importAccountCD, Equal<Required<GLTrialBalanceImportDetails.importAccountCD>>, And<GLTrialBalanceImportDetails.importSubAccountCD, Equal<Required<GLTrialBalanceImportDetails.importSubAccountCD>>>>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) item.MapNumber,
      (object) item.ImportBranchCD,
      (object) item.ImportAccountCD,
      (object) item.ImportSubAccountCD
    }) : PXSelectBase<GLTrialBalanceImportDetails, PXSelect<GLTrialBalanceImportDetails, Where<GLTrialBalanceImportDetails.mapNumber, Equal<Required<GLTrialBalanceImportDetails.mapNumber>>, And<GLTrialBalanceImportDetails.importBranchCD, Equal<Required<GLTrialBalanceImportDetails.importBranchCD>>, And<GLTrialBalanceImportDetails.importAccountCD, Equal<Required<GLTrialBalanceImportDetails.importAccountCD>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) item.MapNumber,
      (object) item.ImportBranchCD,
      (object) item.ImportAccountCD
    });
  }

  private static bool SetValue<TSourceField, TTargetField>(
    PXCache cache,
    GLTrialBalanceImportDetails item,
    string alternativeError,
    string emptyError)
    where TSourceField : IBqlField
    where TTargetField : IBqlField
  {
    string str = (string) null;
    PXUIFieldAttribute.SetError<TTargetField>(cache, (object) item, (string) null);
    object obj = cache.GetValue<TSourceField>((object) item);
    if (obj == null || obj is string && string.IsNullOrEmpty(obj.ToString()))
    {
      str = emptyError;
    }
    else
    {
      try
      {
        cache.SetValueExt<TTargetField>((object) item, obj);
      }
      catch (PXSetPropertyException ex)
      {
        str = ((Exception) ex).Message;
      }
      finally
      {
        if (str == null)
          str = PXUIFieldAttribute.GetError<TTargetField>(cache, (object) item);
      }
    }
    if (!string.IsNullOrEmpty(str))
    {
      PersistErrorAttribute.SetError<TSourceField>(cache, (object) item, !(str != emptyError) || alternativeError == null ? str : alternativeError);
      return false;
    }
    PersistErrorAttribute.ClearError<TSourceField>(cache, (object) item);
    return true;
  }

  private static bool IsUnsignOperations(JournalEntryImport graph)
  {
    return ((PXSelectBase<PX.Objects.GL.GLSetup>) graph.GLSetup).Current.TrialBalanceSign != "N";
  }

  private static Account GetAccount(PXGraph graph, object accountID)
  {
    PXResultset<Account> pxResultset = PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Required<Account.accountID>>>>.Config>.Select(graph, new object[1]
    {
      accountID
    });
    return pxResultset.Count <= 0 ? (Account) null : PXResult<Account>.op_Implicit(pxResultset[0]);
  }

  private static PX.Objects.GL.Ledger GetLedger(PXGraph graph, int? ledgerID)
  {
    return PX.Objects.GL.Ledger.PK.Find(graph, ledgerID);
  }

  private bool CanEdit
  {
    get
    {
      return ((PXSelectBase<GLTrialBalanceImportMap>) this.Map).Current != null && JournalEntryImport.IsEditable(((PXSelectBase<GLTrialBalanceImportMap>) this.Map).Current);
    }
  }

  private static bool IsEditable(GLTrialBalanceImportMap map) => map.Status != "R";

  private static void FillDebitAndCreditAmt(
    GLTran tran,
    Decimal diff,
    Decimal curyDiff,
    bool isReversedSign,
    string accountType)
  {
    if ((accountType == "A" || accountType == "E") && diff > 0M || (accountType == "L" || accountType == "I") && (isReversedSign && diff > 0M || !isReversedSign && diff < 0M))
    {
      tran.CreditAmt = new Decimal?(0M);
      tran.DebitAmt = new Decimal?(Math.Abs(diff));
    }
    else
    {
      tran.DebitAmt = new Decimal?(0M);
      tran.CreditAmt = new Decimal?(Math.Abs(diff));
    }
    if ((accountType == "A" || accountType == "E") && curyDiff > 0M || (accountType == "L" || accountType == "I") && (isReversedSign && curyDiff > 0M || !isReversedSign && curyDiff < 0M))
      tran.CuryDebitAmt = new Decimal?(Math.Abs(curyDiff));
    else
      tran.CuryCreditAmt = new Decimal?(Math.Abs(curyDiff));
  }

  private static IEnumerable<JournalEntryImport.GLHistoryEnquiryWithSubResult> GetBalances(
    JournalEntryImport graph,
    int? orgBAccountID,
    int? ledgerID,
    string finPeriodID,
    string begFinPeriod)
  {
    return JournalEntryImport.GetBalances((PXGraph) graph, JournalEntryImport.IsUnsignOperations(graph), orgBAccountID, ledgerID, finPeriodID, begFinPeriod);
  }

  private static IEnumerable<JournalEntryImport.GLHistoryEnquiryWithSubResult> GetBalances(
    PXGraph graph,
    bool isReversedSign,
    int? orgBAccountID,
    int? ledgerID,
    string finPeriodID,
    string begFinPeriod)
  {
    if (ledgerID.HasValue && finPeriodID != null)
    {
      PXSelectBase<GLHistoryByPeriod> pxSelectBase = (PXSelectBase<GLHistoryByPeriod>) new PXSelectJoinGroupBy<GLHistoryByPeriod, InnerJoin<Account, On<GLHistoryByPeriod.accountID, Equal<Account.accountID>, And<Match<Account, Current<AccessInfo.userName>>>>, InnerJoin<Sub, On<GLHistoryByPeriod.subID, Equal<Sub.subID>, And<Match<Sub, Current<AccessInfo.userName>>>>, LeftJoin<GLHistory, On<GLHistoryByPeriod.accountID, Equal<GLHistory.accountID>, And<GLHistoryByPeriod.branchID, Equal<GLHistory.branchID>, And<GLHistoryByPeriod.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistoryByPeriod.subID, Equal<GLHistory.subID>, And<GLHistoryByPeriod.finPeriodID, Equal<GLHistory.finPeriodID>>>>>>, LeftJoin<AH, On<GLHistoryByPeriod.ledgerID, Equal<AH.ledgerID>, And<GLHistoryByPeriod.branchID, Equal<AH.branchID>, And<GLHistoryByPeriod.accountID, Equal<AH.accountID>, And<GLHistoryByPeriod.subID, Equal<AH.subID>, And<GLHistoryByPeriod.lastActivityPeriod, Equal<AH.finPeriodID>>>>>>>>>>, Where<GLHistoryByPeriod.ledgerID, Equal<Required<GLHistoryByPeriod.ledgerID>>, And<GLHistoryByPeriod.finPeriodID, Equal<Required<GLHistoryByPeriod.finPeriodID>>, And<GLHistoryByPeriod.branchID, InsideBranchesOf<Required<GLTrialBalanceImportMap.orgBAccountID>>, And2<Where<Current<PX.Objects.GL.GLSetup.ytdNetIncAccountID>, IsNull, Or<Account.accountID, NotEqual<Current<PX.Objects.GL.GLSetup.ytdNetIncAccountID>>>>, And<Where2<Where<Account.type, Equal<AccountType.asset>, Or<Account.type, Equal<AccountType.liability>>>, Or<Where<GLHistoryByPeriod.lastActivityPeriod, GreaterEqual<Required<GLHistoryByPeriod.lastActivityPeriod>>, And<Where<Account.type, Equal<AccountType.expense>, Or<Account.type, Equal<AccountType.income>>>>>>>>>>>>, Aggregate<Sum<AH.finYtdBalance, Sum<AH.curyFinYtdBalance, Sum<GLHistory.finPtdDebit, Sum<GLHistory.finPtdCredit, Sum<GLHistory.finBegBalance, Sum<GLHistory.finYtdBalance, Sum<GLHistory.curyFinBegBalance, Sum<GLHistory.curyFinYtdBalance, Sum<GLHistory.curyFinPtdCredit, Sum<GLHistory.curyFinPtdDebit, GroupBy<GLHistoryByPeriod.branchID, GroupBy<GLHistoryByPeriod.ledgerID, GroupBy<GLHistoryByPeriod.accountID, GroupBy<GLHistoryByPeriod.subID, GroupBy<GLHistoryByPeriod.finPeriodID>>>>>>>>>>>>>>>>>(graph);
      object[] objArray = new object[4]
      {
        (object) ledgerID,
        (object) finPeriodID,
        (object) orgBAccountID,
        (object) begFinPeriod
      };
      foreach (PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH> pxResult in pxSelectBase.Select(objArray))
      {
        GLHistoryByPeriod glHistoryByPeriod = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult);
        Account account = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult);
        GLHistory glHistory = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult);
        AH ah = PXResult<GLHistoryByPeriod, Account, Sub, GLHistory, AH>.op_Implicit(pxResult);
        JournalEntryImport.GLHistoryEnquiryWithSubResult balance = new JournalEntryImport.GLHistoryEnquiryWithSubResult();
        balance.BranchID = glHistoryByPeriod.BranchID;
        balance.LedgerID = glHistoryByPeriod.LedgerID;
        balance.AccountID = glHistoryByPeriod.AccountID;
        balance.AccountCD = account.AccountCD;
        balance.AccountType = account.Type;
        balance.SubID = glHistoryByPeriod.SubID;
        balance.Type = account.Type;
        balance.Description = account.Description;
        balance.CuryID = account.CuryID;
        balance.LastActivityPeriod = glHistoryByPeriod.LastActivityPeriod;
        balance.PtdCreditTotal = glHistory.FinPtdCredit;
        balance.PtdDebitTotal = glHistory.FinPtdDebit;
        balance.CuryPtdCreditTotal = glHistory.CuryFinPtdCredit;
        balance.CuryPtdDebitTotal = glHistory.CuryFinPtdDebit;
        bool flag = isReversedSign && (balance.AccountType == "L" || balance.AccountType == "I");
        JournalEntryImport.GLHistoryEnquiryWithSubResult enquiryWithSubResult1 = balance;
        Decimal? nullable1;
        if (!flag)
        {
          nullable1 = ah.FinYtdBalance;
        }
        else
        {
          Decimal? finYtdBalance = ah.FinYtdBalance;
          nullable1 = finYtdBalance.HasValue ? new Decimal?(-finYtdBalance.GetValueOrDefault()) : new Decimal?();
        }
        enquiryWithSubResult1.EndBalance = nullable1;
        JournalEntryImport.GLHistoryEnquiryWithSubResult enquiryWithSubResult2 = balance;
        Decimal? nullable2;
        if (!flag)
        {
          nullable2 = ah.CuryFinYtdBalance;
        }
        else
        {
          Decimal? curyFinYtdBalance = ah.CuryFinYtdBalance;
          nullable2 = curyFinYtdBalance.HasValue ? new Decimal?(-curyFinYtdBalance.GetValueOrDefault()) : new Decimal?();
        }
        enquiryWithSubResult2.CuryEndBalance = nullable2;
        balance.ConsolAccountCD = account.GLConsolAccountCD;
        JournalEntryImport.GLHistoryEnquiryWithSubResult enquiryWithSubResult3 = balance;
        Decimal? endBalance = balance.EndBalance;
        Decimal? nullable3;
        if (!flag)
        {
          Decimal? ptdSaldo = balance.PtdSaldo;
          nullable3 = ptdSaldo.HasValue ? new Decimal?(-ptdSaldo.GetValueOrDefault()) : new Decimal?();
        }
        else
          nullable3 = balance.PtdSaldo;
        Decimal? nullable4 = nullable3;
        Decimal? nullable5 = endBalance.HasValue & nullable4.HasValue ? new Decimal?(endBalance.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
        enquiryWithSubResult3.BegBalance = nullable5;
        JournalEntryImport.GLHistoryEnquiryWithSubResult enquiryWithSubResult4 = balance;
        nullable4 = balance.CuryEndBalance;
        Decimal? nullable6;
        if (!flag)
        {
          Decimal? curyPtdSaldo = balance.CuryPtdSaldo;
          nullable6 = curyPtdSaldo.HasValue ? new Decimal?(-curyPtdSaldo.GetValueOrDefault()) : new Decimal?();
        }
        else
          nullable6 = balance.CuryPtdSaldo;
        Decimal? nullable7 = nullable6;
        Decimal? nullable8 = nullable4.HasValue & nullable7.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable7.GetValueOrDefault()) : new Decimal?();
        enquiryWithSubResult4.CuryBegBalance = nullable8;
        yield return balance;
      }
    }
  }

  private bool CheckMappingAndBalance(
    PXCache sender,
    GLTrialBalanceImportMap header,
    GLTrialBalanceImportDetails row)
  {
    bool flag = true;
    sender.RaiseExceptionHandling<GLTrialBalanceImportDetails.importBranchCD>((object) row, (object) null, (Exception) null);
    sender.RaiseExceptionHandling<GLTrialBalanceImportDetails.mapBranchID>((object) row, (object) null, (Exception) null);
    sender.RaiseExceptionHandling<GLTrialBalanceImportDetails.importAccountCD>((object) row, (object) null, (Exception) null);
    sender.RaiseExceptionHandling<GLTrialBalanceImportDetails.mapAccountID>((object) row, (object) null, (Exception) null);
    sender.RaiseExceptionHandling<GLTrialBalanceImportDetails.importSubAccountCD>((object) row, (object) null, (Exception) null);
    sender.RaiseExceptionHandling<GLTrialBalanceImportDetails.mapSubAccountID>((object) row, (object) null, (Exception) null);
    sender.RaiseExceptionHandling<GLTrialBalanceImportDetails.ytdBalance>((object) row, (object) null, (Exception) null);
    if (header != null && JournalEntryImport.IsEditable(header) && !header.IsHold.GetValueOrDefault())
    {
      if (row.ImportBranchCD == null)
      {
        flag = false;
        sender.RaiseExceptionHandling<GLTrialBalanceImportDetails.importBranchCD>((object) row, (object) row.ImportBranchCD, (Exception) new PXSetPropertyException("The branch cannot be empty.", (PXErrorLevel) 4));
      }
      int? nullable = row.MapBranchID;
      if (!nullable.HasValue && this.IsBranchesNotBalancing(header.OrgBAccountID))
      {
        flag = false;
        sender.RaiseExceptionHandling<GLTrialBalanceImportDetails.mapBranchID>((object) row, (object) row.MapBranchID, (Exception) new PXSetPropertyException("The branch is not mapped.", (PXErrorLevel) 4));
      }
      if (row.ImportAccountCD == null)
      {
        flag = false;
        sender.RaiseExceptionHandling<GLTrialBalanceImportDetails.importAccountCD>((object) row, (object) row.ImportAccountCD, (Exception) new PXSetPropertyException("The account cannot be empty.", (PXErrorLevel) 4));
      }
      nullable = row.MapAccountID;
      if (!nullable.HasValue)
      {
        flag = false;
        sender.RaiseExceptionHandling<GLTrialBalanceImportDetails.mapAccountID>((object) row, (object) row.MapAccountID, (Exception) new PXSetPropertyException("The account is not mapped.", (PXErrorLevel) 4));
      }
      if (row.ImportSubAccountCD == null && PXAccess.FeatureInstalled<FeaturesSet.subAccount>())
      {
        flag = false;
        sender.RaiseExceptionHandling<GLTrialBalanceImportDetails.importSubAccountCD>((object) row, (object) row.ImportSubAccountCD, (Exception) new PXSetPropertyException("The subaccount cannot be empty.", (PXErrorLevel) 4));
      }
      nullable = row.MapSubAccountID;
      if (!nullable.HasValue)
      {
        flag = false;
        sender.RaiseExceptionHandling<GLTrialBalanceImportDetails.mapSubAccountID>((object) row, (object) row.MapSubAccountID, (Exception) new PXSetPropertyException("The subaccount is not mapped.", (PXErrorLevel) 4));
      }
      if (!row.YtdBalance.HasValue)
      {
        flag = false;
        sender.RaiseExceptionHandling<GLTrialBalanceImportDetails.ytdBalance>((object) row, (object) row.YtdBalance, (Exception) new PXSetPropertyException("Balance is incorrect", (PXErrorLevel) 4));
      }
    }
    return flag;
  }

  private bool IsRequireControlTotal
  {
    get
    {
      return ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current != null && ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current.RequireControlTotal.GetValueOrDefault();
    }
  }

  private void ProcessRow(GLTrialBalanceImportDetails row)
  {
    if (row.Status.GetValueOrDefault() == 1)
    {
      int? line = row.Line;
      if (line.HasValue)
      {
        Dictionary<int, GLTrialBalanceImportDetails> dict = new Dictionary<int, GLTrialBalanceImportDetails>();
        Dictionary<int, GLTrialBalanceImportDetails> dictionary = dict;
        line = row.Line;
        int key = line.Value;
        GLTrialBalanceImportDetails balanceImportDetails = row;
        dictionary.Add(key, balanceImportDetails);
        this.ProcessHandler(dict, "Validate", false);
        return;
      }
    }
    row.Status = new int?(0);
  }

  private Account GetRowAccount(GLTrialBalanceImportDetails row)
  {
    return row == null || !row.YtdBalance.HasValue || !row.MapAccountID.HasValue ? (Account) null : JournalEntryImport.GetAccount((PXGraph) this, (object) row.MapAccountID);
  }

  private void CheckTotalBalance(PXCache sender, GLTrialBalanceImportMap header, bool require)
  {
    sender.RaiseExceptionHandling<GLTrialBalanceImportMap.debitTotalBalance>((object) header, (object) null, (Exception) null);
    sender.RaiseExceptionHandling<GLTrialBalanceImportMap.creditTotalBalance>((object) header, (object) null, (Exception) null);
    if (header.IsHold.GetValueOrDefault())
      return;
    if (require)
    {
      sender.RaiseExceptionHandling<GLTrialBalanceImportMap.totalBalance>((object) header, (object) null, (Exception) null);
      Decimal? nullable = header.DebitTotalBalance;
      Decimal? creditTotalBalance = header.CreditTotalBalance;
      if (!(nullable.GetValueOrDefault() == creditTotalBalance.GetValueOrDefault() & nullable.HasValue == creditTotalBalance.HasValue))
      {
        sender.RaiseExceptionHandling<GLTrialBalanceImportMap.debitTotalBalance>((object) header, (object) header.DebitTotalBalance, (Exception) new PXSetPropertyException("Document is out of balance, please review.", (PXErrorLevel) 4));
      }
      else
      {
        creditTotalBalance = header.CreditTotalBalance;
        nullable = header.TotalBalance;
        if (creditTotalBalance.GetValueOrDefault() == nullable.GetValueOrDefault() & creditTotalBalance.HasValue == nullable.HasValue)
          return;
        sender.RaiseExceptionHandling<GLTrialBalanceImportMap.totalBalance>((object) header, (object) header.TotalBalance, (Exception) new PXSetPropertyException("Document is out of balance, please review.", (PXErrorLevel) 4));
      }
    }
    else
    {
      Decimal? debitTotalBalance = header.DebitTotalBalance;
      Decimal? creditTotalBalance = header.CreditTotalBalance;
      if (debitTotalBalance.GetValueOrDefault() == creditTotalBalance.GetValueOrDefault() & debitTotalBalance.HasValue == creditTotalBalance.HasValue)
        return;
      sender.RaiseExceptionHandling<GLTrialBalanceImportMap.debitTotalBalance>((object) header, (object) header.DebitTotalBalance, (Exception) new PXSetPropertyException("Document is out of balance, please review.", (PXErrorLevel) 4));
    }
  }

  private bool IsBranchesNotBalancing(int? orgBAccountID)
  {
    bool flag = false;
    int? organizationId = (int?) ((PXAccess.Organization) PXAccess.GetOrganizationByBAccountID(orgBAccountID))?.OrganizationID;
    if (organizationId.HasValue && OrganizationMaint.FindOrganizationByID((PXGraph) this, organizationId).OrganizationType == "NotBalancing")
      flag = true;
    return flag;
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (viewName == "ImportTemplate" && ((PXSelectBase<GLTrialBalanceImportMap>) this.Map).Current != null)
    {
      keys[(object) this._mapNumberFieldName] = (object) ((PXSelectBase<GLTrialBalanceImportMap>) this.Map).Current.Number;
      Account account = Account.UK.Find((PXGraph) this, (string) values[(object) this._importFieldNameAccount]);
      if (account != null && (account.CuryID == null || account.CuryID == ((PXSelectBase<PX.Objects.GL.Ledger>) this.Ledger).Current?.BaseCuryID))
      {
        string name = typeof (GLTrialBalanceImportDetails.curyYtdBalance).Name;
        if (values.Contains((object) name))
          values.Remove((object) name);
      }
    }
    return true;
  }

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  [Serializable]
  public class OperationParam : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _Action;

    [PXDefault("Validate")]
    public string Action
    {
      get => this._Action;
      set => this._Action = value;
    }

    public abstract class action : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalEntryImport.OperationParam.action>
    {
    }
  }

  [Serializable]
  public class TrialBalanceTemplate : GLTrialBalanceImportDetails
  {
    public new abstract class mapNumber : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalEntryImport.TrialBalanceTemplate.mapNumber>
    {
    }

    public new abstract class line : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      JournalEntryImport.TrialBalanceTemplate.line>
    {
    }

    public new abstract class importBranchCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalEntryImport.TrialBalanceTemplate.importBranchCD>
    {
    }

    public new abstract class mapBranchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      JournalEntryImport.TrialBalanceTemplate.mapBranchID>
    {
    }

    public new abstract class importAccountCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalEntryImport.TrialBalanceTemplate.importAccountCD>
    {
    }

    public new abstract class mapAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      JournalEntryImport.TrialBalanceTemplate.mapAccountID>
    {
    }

    public new abstract class importSubAccountCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalEntryImport.TrialBalanceTemplate.importSubAccountCD>
    {
    }

    public new abstract class mapSubAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      JournalEntryImport.TrialBalanceTemplate.mapSubAccountID>
    {
    }

    public new abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      JournalEntryImport.TrialBalanceTemplate.selected>
    {
    }

    public new abstract class status : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      JournalEntryImport.TrialBalanceTemplate.status>
    {
    }

    public new abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalEntryImport.TrialBalanceTemplate.description>
    {
    }
  }

  [Serializable]
  public class GLHistoryEnquiryWithSubResult : GLHistoryEnquiryResult
  {
    protected int? _SubID;
    protected string _AccountType;

    /// <summary>
    /// A reference to the <see cref="T:PX.Objects.GL.Branch" /> to which the history belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
    /// </value>
    [Branch(null, null, true, true, true, IsKey = true, FieldClass = "BRANCH")]
    public override int? BranchID
    {
      get => base.BranchID;
      set => base.BranchID = value;
    }

    [SubAccount(typeof (JournalEntryImport.GLHistoryEnquiryWithSubResult.accountID))]
    public virtual int? SubID
    {
      get => this._SubID;
      set => this._SubID = value;
    }

    [PXDBString(1)]
    [PXDefault("A")]
    [AccountType.List]
    [PXUIField(DisplayName = "Account Type")]
    public virtual string AccountType
    {
      get => this._AccountType;
      set => this._AccountType = value;
    }

    public new abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      JournalEntryImport.GLHistoryEnquiryWithSubResult.branchID>
    {
    }

    public new abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      JournalEntryImport.GLHistoryEnquiryWithSubResult.accountID>
    {
    }

    public abstract class subID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      JournalEntryImport.GLHistoryEnquiryWithSubResult.subID>
    {
    }

    public abstract class accountType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalEntryImport.GLHistoryEnquiryWithSubResult.accountType>
    {
    }
  }

  public class JournalEntryImportProcessing : 
    PXGraph<JournalEntryImport.JournalEntryImportProcessing>
  {
    public PXSetup<Company> CompanySetup;
    public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
    public PXSelect<PX.Objects.CM.CurrencyInfo> CurrencyInfoView;
    public PXSelect<Batch> Batch;
    public PXSelect<GLTran, Where<GLTran.batchNbr, Equal<Current<Batch.batchNbr>>>> GLTrans;
    public PXSelect<GLTrialBalanceImportMap> Map;
    public PXSelect<GLTrialBalanceImportDetails, Where<GLTrialBalanceImportDetails.mapNumber, Equal<Current<GLTrialBalanceImportMap.number>>>> MapDetails;
    public PXSetup<PX.Objects.GL.Ledger, Where<PX.Objects.GL.Ledger.ledgerID, Equal<Optional<Batch.ledgerID>>>> LedgerView;
    private Dictionary<string, PX.Objects.CM.CurrencyInfo> _curyInfosByCuryID;

    public JournalEntryImportProcessing()
    {
      this._curyInfosByCuryID = new Dictionary<string, PX.Objects.CM.CurrencyInfo>();
    }

    public static void ReleaseImport(object mapNumber, bool isReversedSign)
    {
      JournalEntryImport.JournalEntryImportProcessing instance1 = (JournalEntryImport.JournalEntryImportProcessing) PXGraph.CreateInstance(typeof (JournalEntryImport.JournalEntryImportProcessing));
      JournalEntry instance2 = (JournalEntry) PXGraph.CreateInstance(typeof (JournalEntry));
      GLTrialBalanceImportMap balanceImportMap = PXResultset<GLTrialBalanceImportMap>.op_Implicit(((PXSelectBase<GLTrialBalanceImportMap>) instance1.Map).Search<GLTrialBalanceImportMap.number>(mapNumber, Array.Empty<object>()));
      if (balanceImportMap == null)
        return;
      Batch row = new Batch();
      ((PXSelectBase<GLTrialBalanceImportMap>) instance1.Map).Current = balanceImportMap;
      using (new PXConnectionScope())
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          List<GLTrialBalanceImportDetails> source = new List<GLTrialBalanceImportDetails>();
          foreach (PXResult<GLTrialBalanceImportDetails> pxResult in ((PXSelectBase<GLTrialBalanceImportDetails>) instance1.MapDetails).Select(Array.Empty<object>()))
          {
            GLTrialBalanceImportDetails balanceImportDetails = PXResult<GLTrialBalanceImportDetails>.op_Implicit(pxResult);
            source.Add(balanceImportDetails);
          }
          string str = mapNumber?.ToString() ?? "";
          PX.Objects.GL.Ledger ledger = JournalEntryImport.GetLedger((PXGraph) instance1, balanceImportMap.LedgerID);
          int? nullable1 = source.Count > 0 ? source.First<GLTrialBalanceImportDetails>().MapBranchID : new int?();
          row.BranchID = nullable1;
          row.Module = "GL";
          row.BatchType = "T";
          row.DateEntered = balanceImportMap.ImportDate;
          row = (Batch) ((PXSelectBase) instance2.BatchModule).Cache.Insert((object) row);
          row.FinPeriodID = balanceImportMap.FinPeriodID;
          FinPeriodIDAttribute.SetMasterPeriodID<Batch.finPeriodID>(((PXSelectBase) instance2.BatchModule).Cache, (object) row);
          row.LedgerID = balanceImportMap.LedgerID;
          row.Description = balanceImportMap.Description;
          row.DebitTotal = new Decimal?(0M);
          row.CreditTotal = new Decimal?(0M);
          PX.Objects.CM.CurrencyInfo curyInfo = instance1.GetOrCreateCuryInfo(ledger.BaseCuryID, ledger.BaseCuryID);
          row.CuryInfoID = curyInfo.CuryInfoID;
          row.CuryID = curyInfo.CuryID;
          PX.Objects.CM.CurrencyInfo currencyInfo = instance2.currencyInfo;
          if (currencyInfo.BaseCuryID != curyInfo.BaseCuryID && currencyInfo.CuryID != curyInfo.CuryID)
            ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) instance2.currencyinfo).Update(curyInfo);
          foreach (JournalEntryImport.GLHistoryEnquiryWithSubResult balance in JournalEntryImport.GetBalances((PXGraph) instance1, isReversedSign, balanceImportMap.OrgBAccountID, balanceImportMap.LedgerID, balanceImportMap.FinPeriodID, balanceImportMap.BegFinPeriod))
          {
            GLTrialBalanceImportDetails balanceImportDetails1 = (GLTrialBalanceImportDetails) null;
            for (int index = 0; index < source.Count; ++index)
            {
              GLTrialBalanceImportDetails balanceImportDetails2 = source[index];
              int? mapBranchId = balanceImportDetails2.MapBranchID;
              int? branchId = balance.BranchID;
              if (mapBranchId.GetValueOrDefault() == branchId.GetValueOrDefault() & mapBranchId.HasValue == branchId.HasValue)
              {
                int? mapAccountId = balanceImportDetails2.MapAccountID;
                int? accountId = balance.AccountID;
                if (mapAccountId.GetValueOrDefault() == accountId.GetValueOrDefault() & mapAccountId.HasValue == accountId.HasValue)
                {
                  int? mapSubAccountId = balanceImportDetails2.MapSubAccountID;
                  int? subId = balance.SubID;
                  if (mapSubAccountId.GetValueOrDefault() == subId.GetValueOrDefault() & mapSubAccountId.HasValue == subId.HasValue)
                  {
                    balanceImportDetails1 = balanceImportDetails2;
                    source.RemoveAt(index);
                    break;
                  }
                }
              }
            }
            Decimal diff = (balanceImportDetails1 == null ? 0M : balanceImportDetails1.YtdBalance.Value) - balance.EndBalance.Value;
            Decimal curyDiff = (balanceImportDetails1 == null ? 0M : balanceImportDetails1.CuryYtdBalance.Value) - balance.CuryEndBalance.Value;
            if (!(diff == 0M) || !(curyDiff == 0M))
            {
              Account account = JournalEntryImport.GetAccount((PXGraph) instance1, (object) balance.AccountID);
              GLTran tran = new GLTran();
              tran.BranchID = balance.BranchID;
              tran.AccountID = account.AccountID;
              tran.SubID = balance.SubID;
              JournalEntryImport.FillDebitAndCreditAmt(tran, diff, curyDiff, isReversedSign, balance.AccountType);
              tran.RefNbr = str;
              tran.CuryInfoID = instance1.GetOrCreateCuryInfoForTran(account, ledger);
              tran.ProjectID = ProjectDefaultAttribute.NonProject();
              GLTran glTran = (GLTran) ((PXSelectBase) instance2.GLTranModuleBatNbr).Cache.Insert((object) tran);
            }
          }
          foreach (GLTrialBalanceImportDetails balanceImportDetails in source)
          {
            Decimal diff = balanceImportDetails.YtdBalance.Value;
            Decimal curyDiff = balanceImportDetails.CuryYtdBalance.Value;
            if (!(diff == 0M))
            {
              Account account = JournalEntryImport.GetAccount((PXGraph) instance1, (object) balanceImportDetails.MapAccountID);
              GLTran tran = new GLTran();
              tran.BranchID = balanceImportDetails.MapBranchID;
              tran.AccountID = account.AccountID;
              tran.SubID = balanceImportDetails.MapSubAccountID;
              JournalEntryImport.FillDebitAndCreditAmt(tran, diff, curyDiff, isReversedSign, account.Type);
              tran.RefNbr = str;
              tran.CuryInfoID = instance1.GetOrCreateCuryInfoForTran(account, ledger);
              tran.ProjectID = ProjectDefaultAttribute.NonProject();
              GLTran glTran = (GLTran) ((PXSelectBase) instance2.GLTranModuleBatNbr).Cache.Insert((object) tran);
            }
          }
          Batch batch = row;
          Decimal? debitTotal = row.DebitTotal;
          Decimal? creditTotal = row.CreditTotal;
          Decimal? nullable2 = debitTotal.GetValueOrDefault() == creditTotal.GetValueOrDefault() & debitTotal.HasValue == creditTotal.HasValue ? row.DebitTotal : new Decimal?(0M);
          batch.ControlTotal = nullable2;
          row.Hold = new bool?(false);
          row = (Batch) ((PXSelectBase) instance2.BatchModule).Cache.Update((object) row);
          ((PXGraph) instance2).Persist();
          transactionScope.Complete();
        }
      }
      using (new PXTimeStampScope((byte[]) null))
      {
        Batch batch = PXResultset<Batch>.op_Implicit(((PXSelectBase<Batch>) instance1.Batch).Search<Batch.batchNbr, Batch.module>((object) row.BatchNbr, (object) row.Module, Array.Empty<object>()));
        if (((PXSelectBase<GLTrialBalanceImportMap>) instance1.Map).Current != null)
        {
          ((PXSelectBase<GLTrialBalanceImportMap>) instance1.Map).Current.BatchNbr = batch.BatchNbr;
          GraphHelper.MarkUpdated(((PXSelectBase) instance1.Map).Cache, (object) ((PXSelectBase<GLTrialBalanceImportMap>) instance1.Map).Current);
          ((PXGraph) instance1).Persist();
        }
        ((PXGraph) instance1).Clear();
        PXRedirectHelper.TryRedirect(((PXSelectBase) instance1.Batch).Cache, (object) batch, "View Batch");
      }
    }

    private long? GetOrCreateCuryInfoForTran(Account account, PX.Objects.GL.Ledger ledger)
    {
      return account == null ? new long?() : this.GetOrCreateCuryInfo(account.CuryID == null || !(ledger.BalanceType != "R") ? ledger.BaseCuryID : account.CuryID, ledger.BaseCuryID).CuryInfoID;
    }

    private PX.Objects.CM.CurrencyInfo GetOrCreateCuryInfo(string curyID, string baseCuryID)
    {
      if (this._curyInfosByCuryID.ContainsKey(curyID))
        return this._curyInfosByCuryID[curyID];
      PX.Objects.CM.CurrencyInfo curyInfo = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.CurrencyInfoView).Insert(new PX.Objects.CM.CurrencyInfo()
      {
        BaseCuryID = baseCuryID,
        CuryID = curyID,
        BaseCalc = new bool?(false),
        CuryRate = new Decimal?((Decimal) 1),
        RecipRate = new Decimal?((Decimal) 1)
      });
      ((PXSelectBase) this.CurrencyInfoView).Cache.PersistInserted((object) curyInfo);
      ((PXSelectBase) this.CurrencyInfoView).Cache.Persisted(false);
      this._curyInfosByCuryID.Add(curyInfo.CuryID, curyInfo);
      return curyInfo;
    }
  }
}
