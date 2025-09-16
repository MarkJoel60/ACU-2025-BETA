// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTransactionsEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Utility;
using PX.Objects.CR;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CA;

public class CABankTransactionsEnq : PXGraph<
#nullable disable
CABankTransactionsEnq>
{
  public PXFilter<CABankTransactionsEnq.Filter> TranFilter;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<CABankTransactionsEnq.CABankTranHistory> Result;
  public PXSelect<CATran> Trans;
  public PXSelect<BAccountR, Where<BAccountR.bAccountID, Equal<Required<BAccountR.bAccountID>>>> BAccountRec;
  public PXSelectJoin<CABankTran, LeftJoin<CABankTranMatch, On<CABankTranMatch.tranID, Equal<CABankTran.tranID>, And<CABankTranMatch.tranType, Equal<CABankTran.tranType>>>, LeftJoin<CATran, On<CATran.tranID, Equal<CABankTranMatch.cATranID>>, LeftJoin<CABatch, On<CABatch.batchNbr, Equal<CABankTranMatch.docRefNbr>, And<CABankTranMatch.docType, Equal<CATranType.cABatch>>>, LeftJoin<BAccountR, On<BAccountR.bAccountID, Equal<CATran.referenceID>>>>>>, Where<CABankTran.cashAccountID, Equal<Current<CABankTransactionsEnq.Filter.cashAccountID>>, And<CABankTran.tranDate, GreaterEqual<Current<CABankTransactionsEnq.Filter.startDate>>, And<CABankTran.tranDate, LessEqual<Current<CABankTransactionsEnq.Filter.endDate>>, And<CABankTran.tranType, Equal<Current<CABankTransactionsEnq.Filter.tranType>>, And<CABankTran.processed, Equal<True>, And<Where<CABankTran.headerRefNbr, Equal<Current<CABankTransactionsEnq.Filter.headerRefNbr>>, Or<Current<CABankTransactionsEnq.Filter.headerRefNbr>, IsNull>>>>>>>>> CATran_History;
  public PXAction<CABankTransactionsEnq.Filter> viewDoc;
  public PXAction<CABankTransactionsEnq.Filter> viewStatement;

  public CABankTransactionsEnq()
  {
    ((PXSelectBase) this.Result).AllowDelete = false;
    ((PXSelectBase) this.Result).AllowInsert = false;
    ((PXSelectBase) this.Result).AllowUpdate = false;
    PXUIFieldAttribute.SetVisible<CABankTran.invoiceInfo>(((PXSelectBase) this.Result).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<CABankTran.entryTypeID>(((PXSelectBase) this.Result).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<CABankTran.status>(((PXSelectBase) this.Result).Cache, (object) null, true);
  }

  protected virtual Dictionary<System.Type, System.Type> GetMapperDictionary()
  {
    return new Dictionary<System.Type, System.Type>()
    {
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.tranID),
        typeof (CABankTran.tranID)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.cATranID),
        typeof (CATran.tranID)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.tranType),
        typeof (CABankTran.tranType)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.headerRefNbr),
        typeof (CABankTran.headerRefNbr)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.cashAccountID),
        typeof (CABankTran.cashAccountID)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.tranDate),
        typeof (CABankTran.tranDate)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.extTranID),
        typeof (CABankTran.extTranID)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.extRefNbr),
        typeof (CABankTran.extRefNbr)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.status),
        typeof (CABankTran.status)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.tranDesc),
        typeof (CABankTran.tranDesc)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.tranCode),
        typeof (CABankTran.tranCode)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.curyID),
        typeof (CABankTran.curyID)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.curyCreditAmt),
        typeof (CABankTran.curyCreditAmt)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.curyDebitAmt),
        typeof (CABankTran.curyDebitAmt)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.documentMatched),
        typeof (CABankTran.documentMatched)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.matchedToInvoice),
        typeof (CABankTran.matchedToInvoice)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.histMatchedToInvoice),
        typeof (CABankTran.histMatchedToInvoice)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.matchedToExpenseReceipt),
        typeof (CABankTran.matchedToExpenseReceipt)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.hidden),
        typeof (CABankTran.hidden)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.processed),
        typeof (CABankTran.hidden)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.createDocument),
        typeof (CABankTran.createDocument)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.invoiceInfo),
        typeof (CABankTran.invoiceInfo)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.payeeName),
        typeof (CABankTran.payeeName)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.ruleID),
        typeof (CABankTran.ruleID)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.curyMatchedDebitAmt),
        typeof (CATran.curyDebitAmt)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.curyMatchedCreditAmt),
        typeof (CATran.curyCreditAmt)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.matchedModule),
        typeof (CATran.origModule)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.matchedDocType),
        typeof (CATran.origTranType)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.matchedRefNbr),
        typeof (CATran.origRefNbr)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.matchedReferenceID),
        typeof (CATran.referenceID)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.matchedReferenceCD),
        typeof (BAccountR.acctCD)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.matchedReferenceName),
        typeof (BAccountR.acctName)
      },
      {
        typeof (CABankTransactionsEnq.CABankTranHistory.sortOrder),
        typeof (CABankTran.sortOrder)
      }
    };
  }

  protected virtual IEnumerable result()
  {
    PXResultMapper pxResultMapper = new PXResultMapper((PXGraph) this, this.GetMapperDictionary(), new System.Type[4]
    {
      typeof (CABankTransactionsEnq.CABankTranHistory),
      typeof (CABankTran),
      typeof (CATran),
      typeof (BAccountR)
    });
    PXDelegateResult delegateResult = pxResultMapper.CreateDelegateResult();
    int startRow = PXView.StartRow;
    int num1 = 0;
    foreach (PXResult<CABankTran, CABankTranMatch, CATran, CABatch, BAccountR> source1 in ((PXGraph) this).Views["CATran_History"].Select(PXView.Currents, (object[]) null, pxResultMapper.Searches, pxResultMapper.SortColumns, pxResultMapper.Descendings, PXView.PXFilterRowCollection.op_Implicit(pxResultMapper.Filters), ref startRow, PXView.MaximumRows, ref num1))
    {
      CABankTran tran = PXResult<CABankTran, CABankTranMatch, CATran, CABatch, BAccountR>.op_Implicit(source1);
      CABankTranMatch match = PXResult<CABankTran, CABankTranMatch, CATran, CABatch, BAccountR>.op_Implicit(source1);
      tran.MatchedToInvoice = new bool?(CABankTransactionsHelper.IsMatchedToInvoice(tran, match));
      tran.MatchedToExpenseReceipt = new bool?(CABankTransactionsHelper.IsMatchedToExpenseReceipt(match));
      CATran catran = PXResult<CABankTran, CABankTranMatch, CATran, CABatch, BAccountR>.op_Implicit(source1);
      CABatch caBatch = PXResult<CABankTran, CABankTranMatch, CATran, CABatch, BAccountR>.op_Implicit(source1);
      BAccountR baccountR = PXResult<CABankTran, CABankTranMatch, CATran, CABatch, BAccountR>.op_Implicit(source1);
      bool flag = false;
      if (catran.OrigModule == null)
        catran.OrigModule = match.DocModule;
      if (catran.OrigTranType == null)
        catran.OrigTranType = match.DocType;
      if (catran.OrigRefNbr == null)
        catran.OrigRefNbr = match.DocRefNbr;
      int? nullable1 = catran.ReferenceID;
      if (!nullable1.HasValue)
      {
        CATran caTran = catran;
        nullable1 = match.ReferenceID;
        int? nullable2 = nullable1 ?? tran.BAccountID;
        caTran.ReferenceID = nullable2;
        int num2;
        if (baccountR == null)
        {
          num2 = 1;
        }
        else
        {
          nullable1 = baccountR.BAccountID;
          num2 = !nullable1.HasValue ? 1 : 0;
        }
        if (num2 != 0)
        {
          nullable1 = catran.ReferenceID;
          if (nullable1.HasValue)
          {
            PXSelect<BAccountR, Where<BAccountR.bAccountID, Equal<Required<BAccountR.bAccountID>>>> baccountRec = this.BAccountRec;
            object[] objArray = new object[1];
            nullable1 = catran.ReferenceID;
            objArray[0] = (object) nullable1.Value;
            baccountR = ((PXSelectBase<BAccountR>) baccountRec).SelectSingle(objArray);
            flag = true;
          }
        }
      }
      if (match.DocType == "CBT" && caBatch.CuryDetailTotal.HasValue)
        this.UpdateCATranWithCABatchInfo(catran, caBatch.CuryDetailTotal);
      if (flag)
      {
        PXResult<CABankTran, CABankTranMatch, CATran, CABatch, BAccountR> source2 = new PXResult<CABankTran, CABankTranMatch, CATran, CABatch, BAccountR>(tran, match, catran, caBatch, baccountR);
        ((List<object>) delegateResult).Add(pxResultMapper.CreateResult((PXResult) source2));
      }
      else
        ((List<object>) delegateResult).Add(pxResultMapper.CreateResult((PXResult) source1));
    }
    PXView.StartRow = 0;
    return (IEnumerable) delegateResult;
  }

  protected virtual void UpdateCATranWithCABatchInfo(CATran catran, Decimal? curyDetailTotal)
  {
    catran.DrCr = "C";
    CATran caTran = catran;
    Decimal? nullable1 = curyDetailTotal;
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
    caTran.CuryTranAmt = nullable2;
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewDoc(PXAdapter adapter)
  {
    CABankTransactionsEnq.CABankTranHistory current = ((PXSelectBase<CABankTransactionsEnq.CABankTranHistory>) this.Result).Current;
    RedirectionToOrigDoc.TryRedirect(current.MatchedDocType, current.MatchedRefNbr, current.MatchedModule);
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewStatement(PXAdapter adapter)
  {
    CABankTranHeader caBankTranHeader = PXResultset<CABankTranHeader>.op_Implicit(PXSelectBase<CABankTranHeader, PXSelect<CABankTranHeader, Where<CABankTranHeader.cashAccountID, Equal<Current<CABankTransactionsEnq.Filter.cashAccountID>>, And<CABankTranHeader.refNbr, Equal<Current<CABankTransactionsEnq.CABankTranHistory.headerRefNbr>>, And<CABankTranHeader.tranType, Equal<Current<CABankTransactionsEnq.CABankTranHistory.tranType>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    CABankTransactionsImport instance = PXGraph.CreateInstance<CABankTransactionsImport>();
    ((PXSelectBase<CABankTranHeader>) instance.Header).Current = caBankTranHeader;
    ((PXSelectBase<CABankTransactionsImport.CABankTran2>) instance.SelectedDetail).Current = PXResultset<CABankTransactionsImport.CABankTran2>.op_Implicit(((PXSelectBase<CABankTransactionsImport.CABankTran2>) instance.SelectedDetail).Search<CABankTransactionsEnq.CABankTranHistory.tranID>((object) ((PXSelectBase<CABankTransactionsEnq.CABankTranHistory>) this.Result).Current.TranID, Array.Empty<object>()));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Import");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [Serializable]
  public class Filter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _StartDate;
    protected int? _CashAccountID;
    protected DateTime? _EndDate;
    protected string _HeaderRefNbr;
    protected string _TranType;

    [PXDBDate]
    [PXDefault]
    [PXUIField]
    public virtual DateTime? StartDate
    {
      get => this._StartDate;
      set => this._StartDate = value;
    }

    [CashAccount]
    [PXDefault]
    public virtual int? CashAccountID
    {
      get => this._CashAccountID;
      set => this._CashAccountID = value;
    }

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? EndDate
    {
      get => this._EndDate;
      set => this._EndDate = value;
    }

    [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
    [PXSelector(typeof (Search<CABankTranHeader.refNbr, Where<CABankTranHeader.cashAccountID, Equal<Current<CABankTransactionsEnq.Filter.cashAccountID>>, And<CABankTranHeader.tranType, Equal<Current<CABankTransactionsEnq.Filter.tranType>>>>>), new System.Type[] {typeof (CABankTranHeader.docDate)})]
    [PXUIField(DisplayName = "Statement Nbr.")]
    public virtual string HeaderRefNbr
    {
      get => this._HeaderRefNbr;
      set => this._HeaderRefNbr = value;
    }

    [PXString(1, IsFixed = true)]
    [PXDefault(typeof (CABankTranType.statement))]
    [CABankTranType.List]
    [PXUIField]
    public virtual string TranType
    {
      get => this._TranType;
      set => this._TranType = value;
    }

    public abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CABankTransactionsEnq.Filter.startDate>
    {
    }

    public abstract class cashAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CABankTransactionsEnq.Filter.cashAccountID>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CABankTransactionsEnq.Filter.endDate>
    {
    }

    public abstract class headerRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsEnq.Filter.headerRefNbr>
    {
    }

    public abstract class tranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsEnq.Filter.tranType>
    {
    }
  }

  [PXHidden]
  public class CABankTranHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    /// <summary>
    /// The unique identifier of the CA bank transaction.
    /// This field is the key field.
    /// </summary>
    [PXDBInt(IsKey = true)]
    [PXUIField(DisplayName = "ID", Visible = false)]
    public virtual int? TranID { get; set; }

    [PXDBLong(IsKey = true)]
    [PXUIField(DisplayName = "Document Number")]
    public virtual long? CATranID { get; set; }

    /// <summary>
    /// The cash account specified on the bank statement for which you want to upload bank transactions.
    /// This field is a part of the compound key of the document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.CA.CashAccount.CashAccountID" /> field.
    /// </value>
    [CashAccount]
    public virtual int? CashAccountID { get; set; }

    /// <summary>
    /// The type of the bank tansaction.
    ///  The field is linked to the <see cref="P:PX.Objects.CA.CABankTranHeader.TranType" /> field.
    /// </summary>
    /// <value>
    /// The field can have one of the following values:
    /// <c>"S"</c>: Bank Statement Import,
    /// <c>"I"</c>: Payments Import
    /// </value>
    [PXDBString(1, IsFixed = true)]
    [CABankTranType.List]
    [PXUIField]
    public virtual string TranType { get; set; }

    /// <summary>
    /// The reference number of the imported bank statement (<see cref="T:PX.Objects.CA.CABankTranHeader">CABankTranHeader</see>),
    /// which the system generates automatically in accordance with the numbering sequence assigned to statements on the Cash Management Preferences (CA101000) form.
    /// </summary>
    [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
    [PXUIField(DisplayName = "Statement Nbr.")]
    public virtual string HeaderRefNbr { get; set; }

    /// <summary>The transaction date.</summary>
    [PXDBDate]
    [PXUIField(DisplayName = "Tran. Date")]
    public virtual DateTime? TranDate { get; set; }

    /// <summary>
    /// Specifies (if set to <c>true</c>) that this bank transaction is processed.
    /// </summary>
    [PXDBBool]
    [PXUIField(DisplayName = "Processed")]
    public virtual bool? Processed { get; set; }

    [PXDBString(2, IsFixed = true)]
    [PXUIField(DisplayName = "Module")]
    [BatchModule.FullList]
    public virtual string MatchedModule { get; set; }

    [PXDBString(3, IsFixed = true)]
    [PXUIField(DisplayName = "Type")]
    [CAAPARTranType.ListByModule(typeof (CABankTransactionsEnq.CABankTranHistory.matchedModule))]
    public virtual string MatchedDocType { get; set; }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Reference Nbr.")]
    public virtual string MatchedRefNbr { get; set; }

    [PXDBInt]
    public virtual int? MatchedReferenceID { get; set; }

    [PXDimensionSelector("BIZACCT", typeof (Search<BAccountR.acctCD>))]
    [PXDBString(30, IsUnicode = true, InputMask = "")]
    [PXUIField]
    [PXFieldDescription]
    public virtual string MatchedReferenceCD { get; set; }

    [PXDBString(255 /*0xFF*/, IsUnicode = true)]
    [PXUIField]
    [PXFieldDescription]
    [PXPersonalDataField]
    public virtual string MatchedReferenceName { get; set; }

    [PXDecimal]
    [PXUIField(DisplayName = "Matched Receipt")]
    public virtual Decimal? CuryMatchedDebitAmt { get; set; }

    [PXDecimal]
    [PXUIField(DisplayName = "Matched Disbursement")]
    public virtual Decimal? CuryMatchedCreditAmt { get; set; }

    /// <summary>The external identifier of the transaction.</summary>
    [PXDBString(255 /*0xFF*/, IsUnicode = true)]
    [PXUIField(DisplayName = "Ext. Tran. ID", Visible = false)]
    public virtual string ExtTranID { get; set; }

    /// <summary>The external reference number of the transaction.</summary>
    [PXDBString(40, IsUnicode = true)]
    [PXUIField(DisplayName = "Ext. Ref. Nbr.")]
    public virtual string ExtRefNbr { get; set; }

    /// <summary>
    /// The identifier of the rule that was applied to the bank transaction to create a document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.CA.CABankTranRule.RuleID" /> field.
    /// </value>
    [PXDBInt]
    [PXSelector(typeof (CABankTranRule.ruleID), DescriptionField = typeof (CABankTranRule.description), SubstituteKey = typeof (CABankTranRule.ruleID))]
    [PXUIField(DisplayName = "Applied Rule", Enabled = false)]
    public int? RuleID { get; set; }

    /// <summary>
    /// Specifies (if set to <c>true</c>) that this bank transaction has been hidden from the statement on the Process Bank Transactions (CA306000) form.
    /// </summary>
    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Hidden", Enabled = false)]
    public virtual bool? Hidden { get; set; }

    /// <summary>
    /// Specifies (if set to <c>true</c>) that a new payment will be created for the selected bank transactions.
    /// </summary>
    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Create")]
    public virtual bool? CreateDocument { get; set; }

    /// <summary>
    /// Specifies (if set to <c>true</c>) that this bank transaction is matched to the invoice.
    /// This is a virtual field and it has no representation in the database.
    /// </summary>
    [PXBool]
    [PXUIField(DisplayName = "Matched to Invoice", Visible = false, Enabled = false)]
    public virtual bool? MatchedToInvoice { get; set; }

    /// <summary>
    /// Specifies (if set to <c>true</c>) that this bank transaction is matched to the invoice.
    /// </summary>
    [PXDBBool]
    [PXUIField(DisplayName = "Matched to Invoice", Visible = false, Enabled = false)]
    public virtual bool? HistMatchedToInvoice { get; set; }

    /// <summary>
    /// Specifies (if set to <c>true</c>) that this bank transaction is matched to the Expense Receipt.
    /// This is a virtual field and it has no representation in the database.
    /// </summary>
    [PXBool]
    [PXUIField(DisplayName = "Matched To Expense Receipt", Visible = false, Enabled = false)]
    public virtual bool? MatchedToExpenseReceipt { get; set; }

    /// <summary>
    /// Specifies (if set to <c>true</c>) that this bank transaction is matched to the payment and ready to be processed.
    /// That is, the bank transaction has been matched to an existing transaction in the system, or details of a new document that matches this transaction have been specified.
    /// </summary>
    [PXDBBool]
    [PXUIField(DisplayName = "Matched", Visible = true, Enabled = false)]
    public virtual bool? DocumentMatched { get; set; }

    /// <summary>
    /// The status of the bank transaction.
    /// This is a virtual field and it has no representation in the database.
    /// </summary>
    /// <value>
    /// The field can have one of the following values:
    /// <c>"M"</c>: The bank transaction is matched to the payment and ready to be processed.
    /// <c>"I"</c>: The bank transaction is matched to the invoice.
    /// <c>"C"</c>: The bank transactions will be matched to a new payment.
    /// <c>"H"</c>: The bank transaction is hidden from the statement on the Process Bank Transactions (CA306000) form.
    /// <c>string.Empty</c>: The <see cref="P:PX.Objects.CA.CABankTransactionsEnq.CABankTranHistory.DocumentMatched" />, <see cref="P:PX.Objects.CA.CABankTransactionsEnq.CABankTranHistory.MatchedToInvoice" />, <see cref="P:PX.Objects.CA.CABankTransactionsEnq.CABankTranHistory.CreateDocument" />, and <see cref="P:PX.Objects.CA.CABankTransactionsEnq.CABankTranHistory.Hidden" /> flags are set to <c>false</c>.
    /// </value>
    [PXString(1, IsFixed = true)]
    [CABankTranStatus.List]
    [PXUIField]
    public virtual string Status
    {
      [PXDependsOnFields(new System.Type[] {typeof (CABankTransactionsEnq.CABankTranHistory.hidden), typeof (CABankTransactionsEnq.CABankTranHistory.createDocument), typeof (CABankTransactionsEnq.CABankTranHistory.matchedToInvoice), typeof (CABankTransactionsEnq.CABankTranHistory.documentMatched), typeof (CABankTransactionsEnq.CABankTranHistory.matchedToExpenseReceipt)})] get
      {
        if (this.Hidden.GetValueOrDefault())
          return "H";
        if (this.MatchedToExpenseReceipt.GetValueOrDefault())
          return "R";
        if (this.CreateDocument.GetValueOrDefault())
          return "C";
        if (this.HistMatchedToInvoice.GetValueOrDefault())
          return "I";
        return this.DocumentMatched.GetValueOrDefault() ? "M" : string.Empty;
      }
    }

    /// <summary>The description of the bank transaction.</summary>
    [PXDBString(512 /*0x0200*/, IsUnicode = true)]
    [PXUIField(DisplayName = "Tran. Desc")]
    public virtual string TranDesc { get; set; }

    /// <summary>The external code from the bank.</summary>
    [PXDBString(35, IsUnicode = true)]
    [PXUIField(DisplayName = "Tran. Code", Visible = false)]
    public virtual string TranCode { get; set; }

    /// <summary>The identifier of currency of the bank transaction.</summary>
    [PXDBString(5, IsUnicode = true)]
    [PXUIField(DisplayName = "Currency")]
    public virtual string CuryID { get; set; }

    /// <summary>
    /// The amount of the receipt in the selected currency.
    /// This is a virtual field and it has no representation in the database.
    /// </summary>
    [PXDBCury(typeof (CABankTransactionsEnq.CABankTranHistory.curyID))]
    [PXUIField(DisplayName = "Receipt")]
    public virtual Decimal? CuryDebitAmt { get; set; }

    /// <summary>
    /// The amount of the disbursement in the selected currency.
    /// This is a virtual field and it has no representation in the database.
    /// </summary>
    [PXDBCury(typeof (CABankTransactionsEnq.CABankTranHistory.curyID))]
    [PXUIField(DisplayName = "Disbursement")]
    public virtual Decimal? CuryCreditAmt { get; set; }

    /// <summary>
    /// The reference number of the document (invoice or bill) generated to match a payment.
    /// This field is displayed if the <c>"AP"</c> or <c>"AR"</c> option is selected in the <see cref="!:OrigModule" /> field.
    /// This field is displayed on the Create Payment tab of on the Process Bank Transactions (CA306000) form.
    /// </summary>
    [PXDBString(256 /*0x0100*/, IsUnicode = true)]
    [PXUIField(DisplayName = "Invoice Nbr.", Visible = false)]
    public virtual string InvoiceInfo { get; set; }

    /// <summary>The payee name, if any, specified for a transaction.</summary>
    [PXDBString(256 /*0x0100*/, IsUnicode = true)]
    [PXUIField(DisplayName = "Payee Name", Visible = false)]
    public virtual string PayeeName { get; set; }

    [PXInt]
    public int? SortOrder { get; set; }

    public abstract class tranID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.tranID>
    {
    }

    public abstract class cATranID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.cATranID>
    {
    }

    public abstract class cashAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.cashAccountID>
    {
    }

    public abstract class tranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.tranType>
    {
    }

    public abstract class headerRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.headerRefNbr>
    {
    }

    public abstract class tranDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.tranDate>
    {
    }

    public abstract class processed : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.processed>
    {
    }

    public abstract class matchedModule : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.matchedModule>
    {
    }

    public abstract class matchedDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.matchedDocType>
    {
    }

    public abstract class matchedRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.matchedRefNbr>
    {
    }

    public abstract class matchedReferenceID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.matchedReferenceID>
    {
    }

    public abstract class matchedReferenceCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.matchedReferenceCD>
    {
    }

    public abstract class matchedReferenceName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.matchedReferenceName>
    {
    }

    public abstract class curyMatchedDebitAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.curyMatchedDebitAmt>
    {
    }

    public abstract class curyMatchedCreditAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.curyMatchedCreditAmt>
    {
    }

    public abstract class extTranID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.extTranID>
    {
    }

    public abstract class extRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.extRefNbr>
    {
    }

    public abstract class ruleID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.ruleID>
    {
    }

    public abstract class hidden : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.hidden>
    {
    }

    public abstract class createDocument : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.createDocument>
    {
    }

    public abstract class matchedToInvoice : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.matchedToInvoice>
    {
    }

    public abstract class histMatchedToInvoice : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.histMatchedToInvoice>
    {
    }

    public abstract class matchedToExpenseReceipt : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.matchedToExpenseReceipt>
    {
    }

    public abstract class documentMatched : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.documentMatched>
    {
    }

    public abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.status>
    {
    }

    public abstract class tranDesc : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.tranDesc>
    {
    }

    public abstract class tranCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.tranCode>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.curyID>
    {
    }

    public abstract class curyDebitAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.curyDebitAmt>
    {
    }

    public abstract class curyCreditAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.curyCreditAmt>
    {
    }

    public abstract class invoiceInfo : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.invoiceInfo>
    {
    }

    public abstract class payeeName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.payeeName>
    {
    }

    public abstract class sortOrder : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CABankTransactionsEnq.CABankTranHistory.sortOrder>
    {
    }
  }
}
