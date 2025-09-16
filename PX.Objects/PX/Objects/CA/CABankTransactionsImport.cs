// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTransactionsImport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CA.GraphExtensions;
using PX.Objects.Common;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.CA;

public class CABankTransactionsImport : 
  PXGraph<CABankTransactionsImport, CABankTranHeader>,
  PXImportAttribute.IPXPrepareItems,
  ICaptionable
{
  public PXSelect<CABankTranHeader, Where<CABankTranHeader.cashAccountID, Equal<Optional<CABankTranHeader.cashAccountID>>, And<CABankTranHeader.tranType, Equal<Current<CABankTranHeader.tranType>>>>, OrderBy<Asc<CABankTranHeader.cashAccountID, Desc<CABankTranHeader.endBalanceDate>>>> Header;
  [PXImport(typeof (CABankTranHeader))]
  [PXCopyPasteHiddenFields(new Type[] {typeof (CABankTran.processed), typeof (CABankTran.documentMatched), typeof (CABankTran.createDocument), typeof (CABankTran.hidden), typeof (CABankTran.userDesc), typeof (CABankTran.acctName), typeof (CABankTran.payeeBAccountID1), typeof (CABankTran.origModule1), typeof (CABankTran.payeeLocationID1), typeof (CABankTran.paymentMethodID1), typeof (CABankTran.entryTypeID1), typeof (CABankTran.ruleID)})]
  public PXSelect<CABankTran, Where<CABankTran.headerRefNbr, Equal<Current<CABankTranHeader.refNbr>>, And<CABankTran.tranType, Equal<Current<CABankTranHeader.tranType>>>>> Details;
  public PXSelect<CABankTransactionsImport.CABankTran2, Where<CABankTran.headerRefNbr, Equal<Current<CABankTranHeader.refNbr>>, And<CABankTran.tranType, Equal<Current<CABankTranHeader.tranType>>>>> SelectedDetail;
  public PXSelect<CABankTran, Where<CABankTran.headerRefNbr, Equal<Current<CABankTranHeader.refNbr>>, And<CABankTran.tranType, Equal<Current<CABankTranHeader.tranType>>, And<Where<CABankTran.processed, Equal<True>, Or<CABankTran.documentMatched, Equal<True>>>>>>> MatchedOrProcessedDetails;
  public PXSelect<CABankTran, Where<CABankTran.headerRefNbr, Equal<Current<CABankTranHeader.refNbr>>, And<CABankTran.tranType, Equal<Current<CABankTranHeader.tranType>>, And<CABankTran.documentMatched, Equal<True>>>>> MatchedDetails;
  public PXSetup<PX.Objects.CA.CASetup> CASetup;
  public PXSelectReadonly<CashAccount, Where<CashAccount.extRefNbr, Equal<Optional<CashAccount.extRefNbr>>>> cashAccountByExtRef;
  public PXSelect<CABankTranMatch, Where<CABankTranMatch.tranID, Equal<Required<CABankTran.tranID>>>> TranMatch;
  public PXSelect<CABankTranAdjustment, Where<CABankTranAdjustment.tranID, Equal<Required<CABankTran.tranID>>>> TranAdj;
  public PXSelect<CATran, Where<CATran.tranID, Equal<Required<CATran.tranID>>>> CATrans;
  public PXSelect<CABankTranDetail, Where<CABankTranDetail.bankTranID, Equal<Required<CABankTranDetail.bankTranID>>>> CABankTranSplits;
  public PXSelectJoin<CATran, InnerJoin<CABatchDetail, On<CATran.origModule, Equal<CABatchDetail.origModule>, And<CATran.origTranType, Equal<CABatchDetail.origDocType>, And<CATran.origRefNbr, Equal<CABatchDetail.origRefNbr>>>>>, Where<CABatchDetail.batchNbr, Equal<Required<CABatch.batchNbr>>>> CATransInBatch;
  public PXSelect<EPExpenseClaimDetails> ExpenseReceipts;
  public PXSelect<CABankTranHeader> NewRevisionPanel;
  public PXAction<CABankTranHeader> uploadFile;
  public PXAction<CABankTranHeader> processTransactions;
  public PXAction<CABankTranHeader> unhide;
  public PXAction<CABankTranHeader> unmatch;
  public PXAction<CABankTranHeader> unmatchAll;
  public PXAction<CABankTranHeader> viewDoc;

  [PXUIField]
  [PXButton]
  public virtual IEnumerable UploadFile(PXAdapter adapter)
  {
    bool flag = true;
    if (((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.ImportToSingleAccount.GetValueOrDefault())
    {
      CABankTranHeader current = ((PXSelectBase<CABankTranHeader>) this.Header).Current;
      if (current == null || !((PXSelectBase<CABankTranHeader>) this.Header).Current.CashAccountID.HasValue)
        throw new PXException("You need to select a Cash Account, for which a statement will be imported");
      CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) current.CashAccountID
      }));
      string message;
      if (this.CheckCashAccountIsLinkedToBankFeed(cashAccount, out message))
        throw new PXException(message);
      if (cashAccount != null && string.IsNullOrEmpty(cashAccount.StatementImportTypeName))
        throw new PXException("The statement import service has not been specified for the selected cash account. Update the account settings on the Cash Accounts (CA202000) form.");
    }
    else if (string.IsNullOrEmpty(((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.StatementImportTypeName))
      throw new PXException("You have to configure Statement Import Service. Please, check 'Bank Statement Settings' section in the 'Cash Management Preferences'");
    if (((PXSelectBase<CABankTranHeader>) this.Header).Current != null && ((PXGraph) this).IsDirty)
    {
      if (!((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.ImportToSingleAccount.GetValueOrDefault())
      {
        if (((PXSelectBase<CABankTranHeader>) this.Header).Ask("Confirmation", "Unsaved data in this screen will be lost. Continue?", (MessageButtons) 4) != 6)
          flag = false;
      }
      else
        flag = true;
    }
    if (!flag || ((PXSelectBase<CABankTranHeader>) this.NewRevisionPanel).AskExt() != 1)
      return adapter.Get();
    CABankTranHeader current1 = ((PXSelectBase<CABankTranHeader>) this.Header).Current;
    this.ImportStatement(PXContext.SessionTyped<PXSessionStatePXData>().FileInfo.Pop<FileInfo>("ImportStatementProtoFile"), true);
    ((PXAction) this.Save).Press();
    return (IEnumerable) new List<CABankTranHeader>()
    {
      ((PXSelectBase<CABankTranHeader>) this.Header).Current
    };
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ProcessTransactions(PXAdapter adapter)
  {
    CABankTranHeader current = ((PXSelectBase<CABankTranHeader>) this.Header).Current;
    if (current != null && current.CashAccountID.HasValue)
    {
      CABankTransactionsMaint instance = PXGraph.CreateInstance<CABankTransactionsMaint>();
      ((PXSelectBase) instance.TranMatch).Cache.Clear();
      ((PXSelectBase) instance.TaxTrans).Cache.Clear();
      ((PXSelectBase) instance.TranFilter).Cache.SetValueExt<CABankTransactionsMaint.Filter.cashAccountID>((object) ((PXSelectBase<CABankTransactionsMaint.Filter>) instance.TranFilter).Current, (object) current.CashAccountID);
      throw new PXRedirectRequiredException((PXGraph) instance, "Process Transactions");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Unhide(PXAdapter adapter)
  {
    CABankTran current = ((PXSelectBase<CABankTran>) this.Details).Current;
    if (current.Hidden.GetValueOrDefault())
    {
      current.Hidden = new bool?(false);
      current.Processed = new bool?(false);
      current.RuleID = new int?();
      ((PXSelectBase<CABankTran>) this.Details).Update(current);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Unmatch(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankTransactionsImport.\u003C\u003Ec__DisplayClass22_0 cDisplayClass220 = new CABankTransactionsImport.\u003C\u003Ec__DisplayClass22_0();
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass220.detail = ((PXSelectBase<CABankTran>) this.Details).Current;
    // ISSUE: reference to a compiler-generated field
    if (this.AskToUnmatchProcessedBankTransaction(cDisplayClass220.detail))
    {
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass220, __methodptr(\u003CUnmatch\u003Eb__0)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable UnmatchAll(PXAdapter adapter)
  {
    ((PXAction) this.Save).Press();
    if (((PXSelectBase<CABankTran>) this.MatchedDetails).Any<CABankTran>())
    {
      CABankTran detail = GraphHelper.RowCast<CABankTran>((IEnumerable) ((PXSelectBase<CABankTran>) this.MatchedDetails).Select(Array.Empty<object>())).Where<CABankTran>((Func<CABankTran, bool>) (x => x.Processed.GetValueOrDefault())).FirstOrDefault<CABankTran>();
      if (detail != null && !this.AskToUnmatchProcessedBankTransaction(detail, true))
        return adapter.Get();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CUnmatchAll\u003Eb__24_1)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewDoc(PXAdapter adapter)
  {
    CABankTran current = ((PXSelectBase<CABankTran>) this.Details).Current;
    if (current.DocumentMatched.GetValueOrDefault())
    {
      CABankTranMatch match = PXResultset<CABankTranMatch>.op_Implicit(PXSelectBase<CABankTranMatch, PXSelect<CABankTranMatch, Where<CABankTranMatch.tranID, Equal<Required<CABankTran.tranID>>, And<CABankTranMatch.tranType, Equal<Required<CABankTran.tranType>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) current.TranID,
        (object) current.TranType
      }));
      if (match != null)
        CABankTranMatch.Redirect((PXGraph) this, match);
    }
    return adapter.Get();
  }

  public CABankTransactionsImport()
  {
    PXUIFieldAttribute.SetVisible<CABankTran.invoiceInfo>(((PXSelectBase) this.Details).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<CABankTran.extTranID>(((PXSelectBase) this.Details).Cache, (object) null, true);
    PXUIFieldAttribute.SetReadOnly<CABankTran.payeeBAccountID1>(((PXSelectBase) this.Details).Cache, (object) null, true);
    PXUIFieldAttribute.SetReadOnly<CABankTran.acctName>(((PXSelectBase) this.Details).Cache, (object) null, true);
    PXUIFieldAttribute.SetReadOnly<CABankTran.entryTypeID1>(((PXSelectBase) this.Details).Cache, (object) null, true);
    PXUIFieldAttribute.SetReadOnly<CABankTran.origModule1>(((PXSelectBase) this.Details).Cache, (object) null, true);
    PXUIFieldAttribute.SetReadOnly<CABankTran.paymentMethodID1>(((PXSelectBase) this.Details).Cache, (object) null, true);
    PXUIFieldAttribute.SetReadOnly<CABankTran.payeeLocationID1>(((PXSelectBase) this.Details).Cache, (object) null, true);
    PXUIFieldAttribute.SetReadOnly<CABankTran.userDesc>(((PXSelectBase) this.Details).Cache, (object) null, true);
    (((PXSelectBase) this.Details).Attributes.Find((Predicate<Attribute>) (a => a is PXImportAttribute)) as PXImportAttribute).MappingPropertiesInit += new EventHandler<PXImportAttribute.MappingPropertiesInitEventArgs>(this.ImportAttributeMappingPropertiesInit);
  }

  public string Caption()
  {
    CABankTranHeader current = ((PXSelectBase<CABankTranHeader>) this.Header).Current;
    if (current == null)
      return "";
    CashAccount cashAccount = PXSelectorAttribute.Select<CashAccount.cashAccountID>(((PXSelectBase) this.Header).Cache, (object) current) as CashAccount;
    if (((PXSelectBase) this.Header).Cache.GetStatus((object) current) == 2)
      return cashAccount != null ? $"{cashAccount.CashAccountCD} {cashAccount.Descr}" : PXMessages.Localize("New Record");
    if (cashAccount == null)
      return current.RefNbr;
    return $"{current.RefNbr} - {cashAccount.CashAccountCD} {cashAccount.Descr}: {current.DocDate.Value.ToShortDateString()}";
  }

  protected virtual void ImportAttributeMappingPropertiesInit(
    object sender,
    PXImportAttribute.MappingPropertiesInitEventArgs e)
  {
    HashSet<string> stringSet = new HashSet<string>();
    List<string> stringList = new List<string>();
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.payeeBAccountID)));
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.payeeBAccountIDCopy)));
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.processed)));
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.processed)));
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.createDocument)));
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.curyID)));
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.curyTranAmt)));
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.drCr)));
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.headerRefNbr)));
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.payeeLocationID)));
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.payeeLocationID1)));
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.payeeLocationIDCopy)));
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.tranType)));
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.pMInstanceID)));
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.pMInstanceIDCopy)));
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.curyTotalAmt)));
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.paymentMethodID)));
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.paymentMethodID1)));
    stringSet.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.paymentMethodIDCopy)));
    stringSet.Add("CuryRate");
    stringSet.Add("CuryViewState");
    stringList.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.payeeName)));
    stringList.Add(((PXSelectBase) this.Details).Cache.GetField(typeof (CABankTran.tranCode)));
    for (int index = 0; index < e.Names.Count; ++index)
    {
      if (stringSet.Contains(e.Names[index]))
      {
        e.Names.RemoveAt(index);
        e.DisplayNames.RemoveAt(index);
        --index;
      }
    }
    foreach (string str in stringList)
    {
      if (!e.Names.Contains(str))
      {
        e.Names.Add(str);
        if (((PXSelectBase) this.Details).Cache.GetAttributes(str).FirstOrDefault<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is PXUIFieldAttribute)) is PXUIFieldAttribute pxuiFieldAttribute)
          e.DisplayNames.Add(pxuiFieldAttribute.DisplayName);
        else
          e.DisplayNames.Add(str);
      }
    }
  }

  public virtual void ImportStatement(FileInfo aFileInfo, bool doRedirect)
  {
    bool flag = false;
    IStatementReader reader = this.CreateReader();
    if (reader != null && reader.IsValidInput(aFileInfo.BinData))
    {
      reader.Read(aFileInfo.BinData);
      List<CABankTranHeader> aExported;
      reader.ExportToNew<CABankTransactionsImport>(aFileInfo, this, out aExported);
      if (aExported != null & doRedirect)
      {
        CABankTranHeader caBankTranHeader = aExported == null || aExported.Count <= 0 ? (CABankTranHeader) null : aExported[aExported.Count - 1];
        if (((PXSelectBase<CABankTranHeader>) this.Header).Current != null)
        {
          if (caBankTranHeader != null)
          {
            int? cashAccountId1 = ((PXSelectBase<CABankTranHeader>) this.Header).Current.CashAccountID;
            int? cashAccountId2 = caBankTranHeader.CashAccountID;
            if (cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue && !(((PXSelectBase<CABankTranHeader>) this.Header).Current.RefNbr != caBankTranHeader.RefNbr))
              goto label_6;
          }
          else
            goto label_6;
        }
        ((PXSelectBase<CABankTranHeader>) this.Header).Current = PXResultset<CABankTranHeader>.op_Implicit(((PXSelectBase<CABankTranHeader>) this.Header).Search<CABankTranHeader.cashAccountID, CABankTranHeader.refNbr>((object) caBankTranHeader.CashAccountID, (object) caBankTranHeader.RefNbr, Array.Empty<object>()));
        throw new PXRedirectRequiredException((PXGraph) this, "Navigate to the uploaded record");
      }
label_6:
      flag = true;
    }
    if (!flag)
      throw new PXException("This file format is not supported for the bank statement import. You must create an import scenario for this file extention prior uploading it.");
  }

  public virtual bool CheckCashAccountIsLinkedToBankFeed(
    CashAccount cashAccount,
    out string message)
  {
    bool bankFeed = false;
    message = (string) null;
    if (cashAccount != null)
    {
      CABankTransactionsImportBankFeed extension = ((PXGraph) this).GetExtension<CABankTransactionsImportBankFeed>();
      if (extension != null)
      {
        CABankFeedDetail caBankFeedDetail = ((PXSelectBase<CABankFeedDetail>) extension.BankFeedDetail).SelectSingle(new object[1]
        {
          (object) cashAccount.CashAccountID
        });
        if (caBankFeedDetail != null)
        {
          bankFeed = true;
          message = PXMessages.LocalizeFormatNoPrefix("The {0} cash account is already linked to the {1} bank account in the {2} bank feed.", new object[3]
          {
            (object) cashAccount.CashAccountCD,
            (object) caBankFeedDetail.AccountName,
            (object) caBankFeedDetail.BankFeedID
          });
        }
      }
    }
    return bankFeed;
  }

  protected virtual IStatementReader CreateReader()
  {
    IStatementReader reader = (IStatementReader) null;
    int num = ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.ImportToSingleAccount.GetValueOrDefault() ? 1 : 0;
    string statementImportTypeName = ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.StatementImportTypeName;
    if (num != 0)
    {
      CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Optional<CABankTranHeader.cashAccountID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (cashAccount != null)
        statementImportTypeName = cashAccount.StatementImportTypeName;
    }
    if (string.IsNullOrEmpty(statementImportTypeName))
      return reader;
    try
    {
      return (IStatementReader) Activator.CreateInstance(PXBuildManager.GetType(statementImportTypeName, true));
    }
    catch (Exception ex)
    {
      object[] objArray = new object[1]
      {
        (object) statementImportTypeName
      };
      throw new PXException(ex, "A Statement Reader Service of a type {0} is failed to create", objArray);
    }
  }

  public bool IsAlreadyImported(int? aCashAccountID, string aExtTranID, out string aRefNbr)
  {
    aRefNbr = (string) null;
    CABankTran caBankTran = PXResultset<CABankTran>.op_Implicit(PXSelectBase<CABankTran, PXSelectReadonly<CABankTran, Where<CABankTran.tranType, Equal<Current<CABankTranHeader.tranType>>, And<CABankTran.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CABankTran.extTranID, Equal<Required<CABankTran.extTranID>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) aCashAccountID,
      (object) aExtTranID
    }));
    if (caBankTran != null)
      aRefNbr = caBankTran.HeaderRefNbr.ToString();
    return caBankTran != null;
  }

  public virtual void Persist()
  {
    List<CATran> caTranList = new List<CATran>((IEnumerable<CATran>) ((PXSelectBase) this.CATrans).Cache.Cached);
    NonGenericIEnumerableExtensions.Concat_((IEnumerable) caTranList, ((PXSelectBase) this.CATransInBatch).Cache.Cached);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((PXGraph) this).Persist();
      foreach (CATran tran in caTranList)
        CAReconEntry.UpdateClearedOnSourceDoc(tran);
      transactionScope.Complete();
    }
    ((PXSelectBase) this.Header).Cache.RaiseRowSelected((object) ((PXSelectBase<CABankTranHeader>) this.Header).Current);
  }

  [PXMergeAttributes]
  [PXDefault]
  [CashAccount(true, true, null, typeof (Search<CashAccount.cashAccountID, Where<CashAccount.active, Equal<True>, And<Match<Current<AccessInfo.userName>>>>>))]
  protected virtual void CABankTranHeader_CashAccountID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void CABankTranHeader_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CABankTranHeader row))
      return;
    bool flag1 = ((PXSelectBase) this.Header).Cache.GetStatus((object) row) == 2;
    bool flag2 = ((PXSelectBase<CABankTran>) this.MatchedOrProcessedDetails).Any<CABankTran>();
    PXUIFieldAttribute.SetEnabled<CABankTranHeader.docDate>(sender, (object) row, !flag2);
    PXUIFieldAttribute.SetEnabled<CABankTranHeader.startBalanceDate>(sender, (object) row, !flag2);
    PXUIFieldAttribute.SetEnabled<CABankTranHeader.curyBegBalance>(sender, (object) row, !flag2);
    PXAction<CABankTranHeader> uploadFile = this.uploadFile;
    int? cashAccountId;
    int num1;
    if (((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.ImportToSingleAccount.GetValueOrDefault())
    {
      cashAccountId = row.CashAccountID;
      num1 = cashAccountId.HasValue ? 1 : 0;
    }
    else
      num1 = 1;
    ((PXAction) uploadFile).SetEnabled(num1 != 0);
    PXAction<CABankTranHeader> processTransactions = this.processTransactions;
    int num2;
    if (!flag1)
    {
      cashAccountId = row.CashAccountID;
      num2 = cashAccountId.HasValue ? 1 : 0;
    }
    else
      num2 = 0;
    ((PXAction) processTransactions).SetEnabled(num2 != 0);
    if (((PXSelectBase<CABankTransactionsImport.CABankTran2>) this.SelectedDetail).Current == null)
      return;
    ((PXSelectBase) this.Details).Cache.ActiveRow = (IBqlTable) ((PXSelectBase<CABankTransactionsImport.CABankTran2>) this.SelectedDetail).Current;
    ((PXSelectBase<CABankTransactionsImport.CABankTran2>) this.SelectedDetail).Current = (CABankTransactionsImport.CABankTran2) null;
  }

  protected virtual void CABankTran_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is CABankTran) || ((PXSelectBase<CABankTranHeader>) this.Header).Current == null)
      return;
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<CABankTranHeader>) this.Header).Current.CashAccountID
    }));
    if (cashAccount == null)
      return;
    e.NewValue = (object) cashAccount.CuryID;
  }

  protected virtual void CABankTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CABankTran row))
      return;
    PXCache pxCache = sender;
    CABankTran caBankTran = row;
    bool? nullable = row.Processed;
    int num;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.DocumentMatched;
      num = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    PXUIFieldAttribute.SetEnabled(pxCache, (object) caBankTran, num != 0);
    PXUIFieldAttribute.SetEnabled<CABankTran.processed>(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CABankTran.documentMatched>(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CABankTran.hidden>(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CABankTran.ruleID>(sender, (object) row, false);
  }

  protected virtual void CABankTran_TranDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CABankTran row = e.Row as CABankTran;
    CABankTranHeader current = ((PXSelectBase<CABankTranHeader>) this.Header).Current;
    if (row == null || current == null)
      return;
    e.NewValue = (object) (current.TranMaxDate.HasValue ? current.TranMaxDate : current.DocDate);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CABankTran_TranDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CABankTran row))
      return;
    sender.SetDefaultExt<CABankTran.matchingPaymentDate>((object) row);
    sender.SetDefaultExt<CABankTran.matchingfinPeriodID>((object) row);
  }

  [PXMergeAttributes]
  [PXDBLong]
  protected virtual void CABankTran_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void CABankTran_MatchingPaymentDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is CABankTran row))
      return;
    e.NewValue = (object) row.TranDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CABankTran_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    CABankTran row = e.Row as CABankTran;
    CABankTran oldRow = e.OldRow as CABankTran;
    DateTime? nullable1 = row.TranDate;
    DateTime? tranDate = oldRow.TranDate;
    if ((nullable1.HasValue == tranDate.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != tranDate.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      return;
    CABankTranHeader current = ((PXSelectBase<CABankTranHeader>) this.Header).Current;
    if (current == null)
      return;
    DateTime? nullable2 = current.TranMaxDate;
    if (nullable2.HasValue)
    {
      nullable2 = current.TranMaxDate;
      nullable1 = row.TranDate;
      if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      {
        nullable1 = oldRow.TranDate;
        if (!nullable1.HasValue)
          return;
        nullable1 = current.TranMaxDate;
        nullable2 = oldRow.TranDate;
        if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
          return;
        CABankTran caBankTran = PXResultset<CABankTran>.op_Implicit(PXSelectBase<CABankTran, PXSelect<CABankTran, Where<CABankTran.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CABankTran.headerRefNbr, Equal<Required<CABankTran.headerRefNbr>>>>, OrderBy<Desc<CABankTran.tranDate>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) current.CashAccountID,
          (object) current.RefNbr
        }));
        CABankTranHeader caBankTranHeader = current;
        DateTime? nullable3;
        if (caBankTran == null)
        {
          nullable2 = new DateTime?();
          nullable3 = nullable2;
        }
        else
          nullable3 = caBankTran.TranDate;
        caBankTranHeader.TranMaxDate = nullable3;
        ((PXSelectBase<CABankTranHeader>) this.Header).Update(current);
        return;
      }
    }
    current.TranMaxDate = row.TranDate;
    ((PXSelectBase<CABankTranHeader>) this.Header).Update(current);
  }

  protected virtual void CABankTran_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    CABankTran row = e.Row as CABankTran;
    if (row.Processed.GetValueOrDefault() || row.DocumentMatched.GetValueOrDefault())
      throw new PXSetPropertyException("This transaction cannot be deleted because it has already been matched or processed.");
  }

  protected virtual void CABankTran_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    CABankTran row = e.Row as CABankTran;
    CABankTranHeader current = ((PXSelectBase<CABankTranHeader>) this.Header).Current;
    if (current == null || ((PXSelectBase) this.Header).Cache.GetStatus((object) current) == 3)
      return;
    DateTime? nullable1 = current.TranMaxDate;
    if (!nullable1.HasValue)
      return;
    nullable1 = current.TranMaxDate;
    DateTime dateTime = row.TranDate.Value;
    if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() == dateTime ? 1 : 0) : 0) == 0)
      return;
    CABankTran caBankTran = PXResultset<CABankTran>.op_Implicit(PXSelectBase<CABankTran, PXSelect<CABankTran, Where<CABankTran.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CABankTran.headerRefNbr, Equal<Required<CABankTran.headerRefNbr>>>>, OrderBy<Desc<CABankTran.tranDate>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) current.CashAccountID,
      (object) current.RefNbr
    }));
    CABankTranHeader caBankTranHeader = current;
    DateTime? nullable2;
    if (caBankTran == null)
    {
      nullable1 = new DateTime?();
      nullable2 = nullable1;
    }
    else
      nullable2 = caBankTran.TranDate;
    caBankTranHeader.TranMaxDate = nullable2;
    ((PXSelectBase<CABankTranHeader>) this.Header).Update(current);
  }

  protected virtual void CABankTranHeader_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (e.Row is CABankTranHeader && ((PXSelectBase<CABankTran>) this.MatchedOrProcessedDetails).Any<CABankTran>())
      throw new PXSetPropertyException("This statement cannot be deleted because it contains transactions that has already been matched or processed.");
  }

  protected virtual void CABankTranHeader_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is CABankTranHeader row) || !(row.TranType == "S"))
      return;
    PXResultset<CashAccount> source = PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.CashAccountID
    });
    CashAccount cashAccount = PXResult<CashAccount>.op_Implicit(source != null ? ((IQueryable<PXResult<CashAccount>>) source).First<PXResult<CashAccount>>() : (PXResult<CashAccount>) null);
    if (cashAccount != null)
    {
      bool? active = cashAccount.Active;
      bool flag = false;
      if (active.GetValueOrDefault() == flag & active.HasValue)
        sender.RaiseExceptionHandling<CABankTranHeader.cashAccountID>((object) row, (object) row.CashAccountID, (Exception) new PXSetPropertyException("The cash account {0} is deactivated on the Cash Accounts (CA202000) form.", (PXErrorLevel) 5, new object[1]
        {
          (object) cashAccount.CashAccountCD
        }));
    }
    Decimal? nullable1 = row.CuryEndBalance;
    Decimal? nullable2 = row.CuryDetailsEndBalance;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      sender.RaiseExceptionHandling<CABankTranHeader.curyEndBalance>((object) row, (object) row.CuryEndBalance, (Exception) new PXSetPropertyException("Ending balance does not match the calculated balance.", (PXErrorLevel) 2));
    CABankTranHeader caBankTranHeader = PXResultset<CABankTranHeader>.op_Implicit(PXSelectBase<CABankTranHeader, PXSelect<CABankTranHeader, Where<CABankTranHeader.cashAccountID, Equal<Current<CABankTranHeader.cashAccountID>>, And<CABankTranHeader.tranType, Equal<Current<CABankTranHeader.tranType>>, And<CABankTranHeader.endBalanceDate, LessEqual<Current<CABankTranHeader.docDate>>, And<Where<Current<CABankTranHeader.refNbr>, IsNull, Or<CABankTranHeader.refNbr, NotEqual<Current<CABankTranHeader.refNbr>>>>>>>>, OrderBy<Desc<CABankTranHeader.startBalanceDate>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    if (caBankTranHeader == null)
      return;
    nullable2 = caBankTranHeader.CuryEndBalance;
    nullable1 = row.CuryBegBalance;
    if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
      sender.RaiseExceptionHandling<CABankTranHeader.curyBegBalance>((object) row, (object) row.CuryBegBalance, (Exception) new PXSetPropertyException("Beginning balance does not match the ending balance of previous statement ({0:F2}).", (PXErrorLevel) 2, new object[1]
      {
        (object) caBankTranHeader.CuryEndBalance
      }));
    DateTime? endBalanceDate = caBankTranHeader.EndBalanceDate;
    DateTime? startBalanceDate = row.StartBalanceDate;
    if ((endBalanceDate.HasValue == startBalanceDate.HasValue ? (endBalanceDate.HasValue ? (endBalanceDate.GetValueOrDefault() != startBalanceDate.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      return;
    sender.RaiseExceptionHandling<CABankTranHeader.startBalanceDate>((object) row, (object) row.EndBalanceDate, (Exception) new PXSetPropertyException("Start date does not match the end date of the previous statement ({0:d}).", (PXErrorLevel) 2, new object[1]
    {
      (object) caBankTranHeader.EndBalanceDate
    }));
  }

  protected virtual void CABankTranHeader_CashAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is CABankTranHeader row))
      return;
    PXResultset<CashAccount> source = PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    });
    CashAccount cashAccount = PXResult<CashAccount>.op_Implicit(source != null ? ((IQueryable<PXResult<CashAccount>>) source).FirstOrDefault<PXResult<CashAccount>>() : (PXResult<CashAccount>) null);
    if (cashAccount == null)
      return;
    bool? active = cashAccount.Active;
    bool flag = false;
    if (!(active.GetValueOrDefault() == flag & active.HasValue))
      return;
    PXCache pxCache = sender;
    object newValue = e.NewValue;
    PXSetPropertyException propertyException = new PXSetPropertyException("The cash account {0} is deactivated on the Cash Accounts (CA202000) form.", (PXErrorLevel) 5, new object[1]
    {
      (object) cashAccount.CashAccountCD
    });
    pxCache.RaiseExceptionHandling<CABankTranHeader.cashAccountID>((object) row, newValue, (Exception) propertyException);
    ((CancelEventArgs) e).Cancel = true;
  }

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values) => true;

  public void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public bool RowImporting(string viewName, object row) => row == null;

  private void UnmatchBankTran(CABankTran origTran, bool isMassRelease)
  {
    if (!origTran.DocumentMatched.GetValueOrDefault())
      return;
    CABankTran copy = (CABankTran) ((PXSelectBase) this.Details).Cache.CreateCopy((object) origTran);
    copy.Processed = new bool?(false);
    foreach (PXResult<CABankTranMatch> pxResult1 in ((PXSelectBase<CABankTranMatch>) this.TranMatch).Select(new object[1]
    {
      (object) copy.TranID
    }))
    {
      CABankTranMatch match = PXResult<CABankTranMatch>.op_Implicit(pxResult1);
      if (match.DocModule == "AP" && match.DocType == "CBT")
      {
        foreach (PXResult<CATran> pxResult2 in ((PXSelectBase<CATran>) this.CATransInBatch).Select(new object[1]
        {
          (object) match.DocRefNbr
        }))
        {
          CATran caTran = PXResult<CATran>.op_Implicit(pxResult2);
          if (caTran != null && caTran.TranID.HasValue && caTran.ReconNbr == null)
          {
            caTran.ClearDate = new DateTime?();
            caTran.Cleared = new bool?(false);
            ((PXSelectBase<CATran>) this.CATransInBatch).Update(caTran);
          }
        }
      }
      else
      {
        CATran caTran = PXResultset<CATran>.op_Implicit(((PXSelectBase<CATran>) this.CATrans).Select(new object[1]
        {
          (object) match.CATranID
        }));
        if (caTran != null && caTran.TranID.HasValue && caTran.ReconNbr == null)
        {
          caTran.ClearDate = new DateTime?();
          caTran.Cleared = new bool?(false);
          ((PXSelectBase<CATran>) this.CATrans).Update(caTran);
        }
      }
      if (CABankTransactionsHelper.IsMatchedToExpenseReceipt(match))
      {
        EPExpenseClaimDetails expenseClaimDetails = PXResultset<EPExpenseClaimDetails>.op_Implicit(PXSelectBase<EPExpenseClaimDetails, PXSelect<EPExpenseClaimDetails, Where<EPExpenseClaimDetails.claimDetailCD, Equal<Required<EPExpenseClaimDetails.claimDetailCD>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) match.DocRefNbr
        }));
        expenseClaimDetails.BankTranDate = new DateTime?();
        ((PXSelectBase<EPExpenseClaimDetails>) this.ExpenseReceipts).Update(expenseClaimDetails);
      }
      ((PXSelectBase<CABankTranMatch>) this.TranMatch).Delete(match);
    }
    foreach (PXResult<CABankTranAdjustment> pxResult in ((PXSelectBase<CABankTranAdjustment>) this.TranAdj).Select(new object[1]
    {
      (object) copy.TranID
    }))
      ((PXSelectBase<CABankTranAdjustment>) this.TranAdj).Delete(PXResult<CABankTranAdjustment>.op_Implicit(pxResult));
    foreach (PXResult<CABankTranDetail> pxResult in ((PXSelectBase<CABankTranDetail>) this.CABankTranSplits).Select(new object[1]
    {
      (object) copy.TranID
    }))
      ((PXSelectBase<CABankTranDetail>) this.CABankTranSplits).Delete(PXResult<CABankTranDetail>.op_Implicit(pxResult));
    CABankTransactionsMaint.ClearFields(copy);
    CABankTransactionsMaint.ClearChargeFields(copy);
    ((PXSelectBase) this.Details).Cache.SetDefaultExt<CABankTran.curyApplAmt>((object) copy);
    ((PXSelectBase) this.Details).Cache.SetDefaultExt<CABankTran.curyApplAmtCA>((object) copy);
    ((PXSelectBase) this.Details).Cache.SetDefaultExt<CABankTran.curyApplAmtMatch>((object) copy);
    ((PXSelectBase<CABankTran>) this.Details).Update(copy);
    if (isMassRelease)
      return;
    ((PXAction) this.Save).Press();
  }

  private bool AskToUnmatchProcessedBankTransaction(CABankTran detail, bool unmatchAll = false)
  {
    if (detail == null || !detail.DocumentMatched.GetValueOrDefault())
      return false;
    return !detail.Processed.GetValueOrDefault() || ((PXSelectBase<CABankTran>) this.Details).Ask(unmatchAll ? "Unmatch All" : "Unmatch", unmatchAll ? "The system will roll back all changes to the matched bank transactions and make them available for processing on the Process Bank Transactions (CA306000) form. The links to the matched documents will be deleted, but the documents will stay marked as cleared if they are included into a reconciliation statement. If the documents were created to match the transactions, they should be handled manually, for instance, voided or matched. Proceed?" : "The system will roll back all changes to the selected bank transaction and make it available for processing on the Process Bank Transactions (CA306000) form. The link to the matched document will be deleted, but the document will stay marked as cleared if it is included into a reconciliation statement. If the document was created to match the transaction, it should be handled manually, for instance, voided or matched. Proceed?", (MessageButtons) 1) == 1;
  }

  private void UnmatchAllProcess()
  {
    foreach (PXResult<CABankTran> pxResult in ((PXSelectBase<CABankTran>) this.MatchedDetails).Select(Array.Empty<object>()))
      this.UnmatchBankTran(PXResult<CABankTran>.op_Implicit(pxResult), true);
    ((PXAction) this.Save).Press();
  }

  [Serializable]
  public class CABankTran2 : CABankTran
  {
  }
}
