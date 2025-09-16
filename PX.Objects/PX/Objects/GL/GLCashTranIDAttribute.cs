// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLCashTranIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Specialized for the GLTran version of the <see cref="T:PX.Objects.CA.CashTranIDAttribute" /><br />
/// Since CATran created from the source row, it may be used only the fields <br />
/// of GLTran compatible DAC. <br />
/// The main purpuse of the attribute - to create CATran <br />
/// for the source row and provide CATran and source synchronization on persisting.<br />
/// CATran cache must exists in the calling Graph.<br />
/// </summary>
public class GLCashTranIDAttribute : 
  CashTranIDAttribute,
  IPXRowInsertingSubscriber,
  IPXRowUpdatingSubscriber
{
  protected bool _IsIntegrityCheck;
  protected bool _CATranValidation;
  protected string _LedgerNotActual = "N";
  protected string _LedgerActual = "A";

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    Type itemType = sender.GetItemType();
    if (!sender.Graph.Views.Caches.Contains(itemType))
      sender.Graph.Views.Caches.Add(itemType);
    if (sender.Graph.Views.Caches.Contains(typeof (CATran)))
      return;
    sender.Graph.Views.Caches.Add(typeof (CATran));
  }

  public static CATran DefaultValues<Field>(PXCache sender, object data) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes<Field>(data))
    {
      if (attribute is GLCashTranIDAttribute)
      {
        ((GLCashTranIDAttribute) attribute)._IsIntegrityCheck = true;
        ((GLCashTranIDAttribute) attribute)._CATranValidation = true;
        CATran caTran = ((CashTranIDAttribute) attribute).DefaultValues(sender, new CATran(), data);
        ((GLCashTranIDAttribute) attribute)._CATranValidation = false;
        return caTran;
      }
    }
    return (CATran) null;
  }

  public static CATran DefaultValues(PXCache sender, object data)
  {
    return new GLCashTranIDAttribute()
    {
      _IsIntegrityCheck = true
    }.DefaultValues(sender, new CATran(), data);
  }

  public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (this._IsIntegrityCheck)
      return;
    base.RowPersisting(sender, e);
  }

  public override void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (this._IsIntegrityCheck)
      return;
    base.RowPersisted(sender, e);
  }

  public virtual void RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    GLTran newRow = (GLTran) e.NewRow;
    bool? nullable = GLCashTranIDAttribute.CheckGLTranCashAcc(sender.Graph, newRow, out int? _);
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    Branch branch = (Branch) PXSelectorAttribute.Select<GLTran.branchID>(sender, (object) newRow);
    Account account = (Account) PXSelectorAttribute.Select<GLTran.accountID>(sender, (object) newRow);
    Sub sub = (Sub) PXSelectorAttribute.Select<GLTran.subID>(sender, (object) newRow);
    if (e.ExternalCall)
    {
      if (!PXAccess.FeatureInstalled<FeaturesSet.subAccount>())
      {
        sender.RaiseExceptionHandling<GLTran.accountID>((object) newRow, (object) account.AccountCD, (Exception) new PXSetPropertyException("The cash account does not exist for the following branch and account: {0}, {1}.", new object[2]
        {
          (object) branch.BranchCD,
          (object) account.AccountCD
        }));
        ((CancelEventArgs) e).Cancel = true;
      }
      else
      {
        sender.RaiseExceptionHandling<GLTran.subID>((object) newRow, (object) sub.SubCD, (Exception) new PXSetPropertyException("Cash account doesn't exist for this branch, account and sub account ({0}, {1}, {2})", new object[3]
        {
          (object) branch.BranchCD,
          (object) account.AccountCD,
          (object) sub.SubCD
        }));
        ((CancelEventArgs) e).Cancel = true;
      }
    }
    else
      throw new PXSetPropertyException("Cash account doesn't exist for this branch, account and sub account ({0}, {1}, {2})", new object[3]
      {
        (object) branch.BranchCD,
        (object) account.AccountCD,
        (object) sub.SubCD
      });
  }

  public virtual void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (sender.Graph.IsContractBasedAPI)
      return;
    GLTran row = (GLTran) e.Row;
    bool? nullable = GLCashTranIDAttribute.CheckGLTranCashAcc(sender.Graph, row, out int? _);
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    Branch branch = (Branch) PXSelectorAttribute.Select<GLTran.branchID>(sender, (object) row);
    Account account = (Account) PXSelectorAttribute.Select<GLTran.accountID>(sender, (object) row);
    Sub sub = (Sub) PXSelectorAttribute.Select<GLTran.subID>(sender, (object) row);
    if (e.ExternalCall)
    {
      sender.RaiseExceptionHandling<GLTran.subID>((object) row, (object) sub.SubCD, (Exception) new PXSetPropertyException("Cash account doesn't exist for this branch, account and sub account ({0}, {1}, {2})", new object[3]
      {
        (object) branch.BranchCD,
        (object) account.AccountCD,
        (object) sub.SubCD
      }));
      ((CancelEventArgs) e).Cancel = true;
    }
    else
      throw new PXSetPropertyException("Cash account doesn't exist for this branch, account and sub account ({0}, {1}, {2})", new object[3]
      {
        (object) branch.BranchCD,
        (object) account.AccountCD,
        (object) sub.SubCD
      });
  }

  public static bool? CheckGLTranCashAcc(PXGraph graph, GLTran tran, out int? cashAccountID)
  {
    cashAccountID = new int?();
    Account account = Account.PK.Find(graph, tran.AccountID);
    bool? nullable1;
    if (account != null)
    {
      nullable1 = account.IsCashAccount;
      if (nullable1.GetValueOrDefault())
      {
        int? nullable2 = tran.SubID;
        if (nullable2.HasValue)
        {
          nullable2 = tran.BranchID;
          if (nullable2.HasValue)
          {
            PX.Objects.CA.CashAccount cashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.accountID, Equal<Required<PX.Objects.CA.CashAccount.accountID>>, And<PX.Objects.CA.CashAccount.subID, Equal<Required<PX.Objects.CA.CashAccount.subID>>, And<PX.Objects.CA.CashAccount.branchID, Equal<Required<PX.Objects.CA.CashAccount.branchID>>>>>>.Config>.Select(graph, new object[3]
            {
              (object) tran.AccountID,
              (object) tran.SubID,
              (object) tran.BranchID
            }));
            if (cashAccount == null)
              return new bool?(false);
            cashAccountID = cashAccount.CashAccountID;
            return new bool?(true);
          }
        }
      }
    }
    nullable1 = new bool?();
    return nullable1;
  }

  public static bool CheckDocTypeAmounts(
    string Module,
    string DocType,
    Decimal CuryDocAmount,
    Decimal CuryDebitAmount,
    Decimal CuryCreditAmount)
  {
    switch (Module)
    {
      case "AR":
        switch (DocType)
        {
          case "PMT":
          case "CSL":
          case "PPM":
            return Math.Abs(CuryDocAmount) == CuryDebitAmount;
          case "RPM":
          case "REF":
          case "RCS":
            return Math.Abs(CuryDocAmount) == CuryCreditAmount;
          default:
            return false;
        }
      case "AP":
        switch (DocType)
        {
          case "CHK":
          case "PPM":
          case "QCK":
            return Math.Abs(CuryDocAmount) == CuryCreditAmount;
          case "VCK":
          case "REF":
          case "VQC":
            return Math.Abs(CuryDocAmount) == CuryDebitAmount;
          default:
            return false;
        }
      default:
        return false;
    }
  }

  private static bool CheckChargeAmounts(
    string Module,
    bool isCADebit,
    Decimal CuryDocAmount,
    Decimal CuryDebitAmount,
    Decimal CuryCreditAmount,
    bool isVoidingTran)
  {
    if (isVoidingTran)
      isCADebit = !isCADebit;
    switch (Module)
    {
      case "AR":
        return isCADebit ? Math.Abs(CuryDocAmount) == Math.Abs(CuryDebitAmount) : Math.Abs(CuryDocAmount) == Math.Abs(CuryCreditAmount);
      case "AP":
        return isCADebit ? Math.Abs(CuryDocAmount) == Math.Abs(CuryDebitAmount) : Math.Abs(CuryDocAmount) == Math.Abs(CuryCreditAmount);
      default:
        return false;
    }
  }

  public override CATran DefaultValues(PXCache sender, CATran catran_Row, object orig_Row)
  {
    return this.DefaultValues<Batch, Batch.module, Batch.batchNbr>(sender, catran_Row, orig_Row);
  }

  protected CATran DefaultValues<TBatch, TModule, TBatchNbr>(
    PXCache sender,
    CATran catran_Row,
    object orig_Row)
    where TBatch : Batch, IBqlTable, new()
    where TModule : IBqlField
    where TBatchNbr : IBqlField
  {
    GLTran glTran = (GLTran) orig_Row;
    if (glTran.Module == "CM" || GLCashTranIDAttribute.IsZeroAmount(glTran) && EnumerableExtensions.IsNotIn<string>(glTran.TranType, "CDT", "CVD"))
      return (CATran) null;
    int? cashAccountID;
    bool? nullable1 = GLCashTranIDAttribute.CheckGLTranCashAcc(sender.Graph, glTran, out cashAccountID);
    if (!nullable1.HasValue)
      return (CATran) null;
    if (string.IsNullOrWhiteSpace(glTran.LedgerBalanceType))
    {
      Ledger ledger = Ledger.PK.Find(sender.Graph, glTran.LedgerID);
      if (ledger == null || !(ledger.BalanceType == "A"))
        return (CATran) null;
    }
    else if (glTran.LedgerBalanceType == this._LedgerNotActual)
      return (CATran) null;
    TBatch batch = PXResultset<TBatch>.op_Implicit(PXSelectBase<TBatch, PXSelect<TBatch, Where<TModule, Equal<Required<GLTran.module>>, And<TBatchNbr, Equal<Required<GLTran.batchNbr>>>>>.Config>.Select(sender.Graph, new object[2]
    {
      (object) glTran.Module,
      (object) glTran.BatchNbr
    }));
    if (!batch.Scheduled.GetValueOrDefault())
    {
      bool? nullable2 = batch.Voided;
      if (!nullable2.GetValueOrDefault())
      {
        nullable2 = nullable1;
        bool flag1 = false;
        if (nullable2.GetValueOrDefault() == flag1 & nullable2.HasValue)
        {
          Branch branch = (Branch) PXSelectorAttribute.Select<GLTran.branchID>(sender, (object) glTran);
          Account account = (Account) PXSelectorAttribute.Select<GLTran.accountID>(sender, (object) glTran);
          Sub sub = (Sub) PXSelectorAttribute.Select<GLTran.subID>(sender, (object) glTran);
          sender.RaiseExceptionHandling<GLTran.subID>((object) glTran, (object) sub.SubCD, (Exception) new PXSetPropertyException("Cash account doesn't exist for this branch, account and sub account ({0}, {1}, {2})", new object[3]
          {
            (object) branch.BranchCD,
            (object) account.AccountCD,
            (object) sub.SubCD
          }));
          return (CATran) null;
        }
        long? nullable3;
        if (!(glTran.Module == "GL"))
        {
          nullable3 = catran_Row.TranID;
          if (nullable3.HasValue)
          {
            nullable2 = glTran.Released;
            if (nullable2.GetValueOrDefault() && catran_Row != null)
            {
              nullable3 = catran_Row.TranID;
              if (nullable3.HasValue)
              {
                catran_Row.Released = glTran.Released;
                catran_Row.Posted = glTran.Posted;
                catran_Row.Hold = new bool?(false);
                catran_Row.BatchNbr = glTran.BatchNbr;
                GLCashTranIDAttribute.SetAmounts(catran_Row, glTran);
                CashTranIDAttribute.SetCleared(catran_Row, CashTranIDAttribute.GetCashAccount(catran_Row, sender.Graph));
                nullable3 = catran_Row.VoidedTranID;
                if (nullable3.HasValue)
                {
                  this.SetOriginalTransactionVoided(sender, catran_Row.VoidedTranID, catran_Row);
                  goto label_55;
                }
                goto label_55;
              }
            }
            return (CATran) null;
          }
        }
        string drCr = (string) null;
        nullable3 = catran_Row.TranID;
        if (!nullable3.HasValue)
        {
          string origTranType = "GLE";
          string batchNbr = glTran.BatchNbr;
          int? lineNbr = glTran.LineNbr;
          string refNbr = glTran.RefNbr;
          long? caTranId = new long?();
          bool isChangeTran = false;
          bool? voided = new bool?(false);
          long? voidedCATranID = new long?();
          if (this._CATranValidation)
          {
            GLCashTranIDAttribute.GetDataFromOriginalDocument(sender, glTran, ref origTranType, ref batchNbr, ref lineNbr, ref refNbr, ref isChangeTran, ref caTranId, ref voided, ref voidedCATranID, ref drCr);
            if (caTranId.HasValue)
            {
              CATran caTran = PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.tranID, Equal<Required<CATran.tranID>>>>.Config>.Select(sender.Graph, new object[1]
              {
                (object) caTranId
              }));
              if (caTran != null)
                catran_Row = caTran;
              else
                caTranId = new long?();
            }
            if (!caTranId.HasValue)
            {
              Decimal? curyDebitAmt = glTran.CuryDebitAmt;
              Decimal? nullable4 = glTran.CuryCreditAmt;
              Decimal? nullable5 = curyDebitAmt.HasValue & nullable4.HasValue ? new Decimal?(curyDebitAmt.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
              Decimal num = 0M;
              Decimal? nullable6;
              string str;
              if (nullable5.GetValueOrDefault() >= num & nullable5.HasValue)
              {
                nullable5 = glTran.CuryDebitAmt;
                nullable4 = glTran.CuryCreditAmt;
                nullable6 = nullable5.HasValue & nullable4.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
                str = "D";
              }
              else
              {
                nullable4 = glTran.CuryDebitAmt;
                nullable5 = glTran.CuryCreditAmt;
                nullable6 = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
                str = "C";
              }
              if (!string.IsNullOrEmpty(drCr))
                str = drCr;
              foreach (PXResult<CATran> pxResult in PXSelectBase<CATran, PXSelect<CATran, Where<CATran.origModule, Equal<Required<CATran.origModule>>, And<CATran.origTranType, Equal<Required<CATran.origTranType>>, And<CATran.origRefNbr, Equal<Required<CATran.origRefNbr>>, And<CATran.cashAccountID, Equal<Required<CATran.cashAccountID>>, And<CATran.curyTranAmt, Equal<Required<CATran.curyTranAmt>>, And<CATran.drCr, Equal<Required<CATran.drCr>>>>>>>>>.Config>.Select(sender.Graph, new object[6]
              {
                (object) glTran.Module,
                (object) origTranType,
                (object) batchNbr,
                (object) cashAccountID,
                (object) nullable6,
                (object) str
              }))
              {
                CATran caTran = PXResult<CATran>.op_Implicit(pxResult);
                if (PXResultset<GLTran>.op_Implicit(PXSelectBase<GLTran, PXSelect<GLTran, Where<GLTran.module, Equal<Required<GLTran.module>>, And<GLTran.batchNbr, Equal<Required<GLTran.batchNbr>>, And<GLTran.cATranID, Equal<Required<GLTran.cATranID>>>>>>.Config>.Select(sender.Graph, new object[3]
                {
                  (object) glTran.Module,
                  (object) glTran.BatchNbr,
                  (object) caTran.TranID
                })) == null)
                {
                  catran_Row = caTran;
                  break;
                }
              }
            }
          }
          nullable3 = catran_Row.TranID;
          if (!nullable3.HasValue)
          {
            catran_Row.OrigModule = glTran.Module;
            catran_Row.OrigTranType = origTranType;
            catran_Row.OrigRefNbr = batchNbr;
            catran_Row.OrigLineNbr = lineNbr;
            catran_Row.ExtRefNbr = refNbr;
            catran_Row.IsPaymentChargeTran = new bool?(isChangeTran);
            catran_Row.Voided = voided;
            catran_Row.VoidedTranID = voidedCATranID;
          }
          else
            catran_Row = PXCache<CATran>.CreateCopy(catran_Row);
        }
        else
        {
          nullable3 = catran_Row.TranID;
          long num = 0;
          if (nullable3.GetValueOrDefault() < num & nullable3.HasValue)
          {
            catran_Row.OrigModule = glTran.Module;
            catran_Row.OrigTranType = "GLE";
            catran_Row.OrigRefNbr = glTran.BatchNbr;
            catran_Row.ExtRefNbr = glTran.RefNbr;
            catran_Row.OrigLineNbr = glTran.LineNbr;
          }
        }
        if (this._CATranValidation)
        {
          nullable3 = catran_Row.TranID;
          if (nullable3.HasValue)
          {
            nullable3 = catran_Row.TranID;
            long num = 0;
            if (!(nullable3.GetValueOrDefault() < num & nullable3.HasValue))
              goto label_55;
          }
        }
        GLCashTranIDAttribute.SetAmounts(catran_Row, glTran);
        if (string.IsNullOrEmpty(drCr))
        {
          Decimal? curyTranAmt = catran_Row.CuryTranAmt;
          Decimal num = 0M;
          catran_Row.DrCr = !(curyTranAmt.GetValueOrDefault() >= num & curyTranAmt.HasValue) ? "C" : "D";
        }
        else
          catran_Row.DrCr = drCr;
        PX.Objects.CA.CashAccount cashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Required<PX.Objects.CA.CashAccount.cashAccountID>>>>.Config>.Select(sender.Graph, new object[1]
        {
          (object) cashAccountID
        }));
        string extRefNbr;
        GLCashTranIDAttribute.TryToGetOrigExtRefNbr(sender, glTran, out extRefNbr);
        catran_Row.CashAccountID = cashAccount.CashAccountID;
        catran_Row.CuryInfoID = glTran.CuryInfoID;
        catran_Row.TranDate = glTran.TranDate;
        catran_Row.TranDesc = glTran.TranDesc;
        CashTranIDAttribute.SetPeriodsByMaster(sender, catran_Row, glTran.TranPeriodID);
        catran_Row.ReferenceID = glTran.ReferenceID;
        catran_Row.Released = glTran.Released;
        catran_Row.Posted = glTran.Posted;
        catran_Row.Hold = new bool?(false);
        catran_Row.BatchNbr = glTran.BatchNbr;
        catran_Row.ExtRefNbr = !string.IsNullOrEmpty(extRefNbr) || !(catran_Row.OrigTranType == "GLE") ? extRefNbr : glTran.RefNbr;
        if (cashAccount != null)
        {
          nullable2 = cashAccount.Reconcile;
          bool flag2 = false;
          if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
          {
            catran_Row.Cleared = new bool?(true);
            catran_Row.ClearDate = catran_Row.TranDate;
          }
        }
label_55:
        return catran_Row;
      }
    }
    return (CATran) null;
  }

  private static bool IsZeroAmount(GLTran tran)
  {
    if (tran != null)
    {
      Decimal? curyCreditAmt = tran.CuryCreditAmt;
      Decimal num1 = 0M;
      if (curyCreditAmt.GetValueOrDefault() == num1 & curyCreditAmt.HasValue && tran != null)
      {
        Decimal? curyDebitAmt = tran.CuryDebitAmt;
        Decimal num2 = 0M;
        if (curyDebitAmt.GetValueOrDefault() == num2 & curyDebitAmt.HasValue && tran != null)
        {
          Decimal? creditAmt = tran.CreditAmt;
          Decimal num3 = 0M;
          if (creditAmt.GetValueOrDefault() == num3 & creditAmt.HasValue && tran != null)
          {
            Decimal? debitAmt = tran.DebitAmt;
            Decimal num4 = 0M;
            return debitAmt.GetValueOrDefault() == num4 & debitAmt.HasValue;
          }
        }
      }
    }
    return false;
  }

  private static void TryToGetOrigExtRefNbr(
    PXCache sender,
    GLTran parentTran,
    out string extRefNbr)
  {
    extRefNbr = (string) null;
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    int? origLineNbr = new int?();
    long? caTranId = new long?();
    bool isChangeTran = false;
    bool? voided = new bool?(false);
    long? voidedCATranID = new long?();
    string drCr = (string) null;
    GLCashTranIDAttribute.GetDataFromOriginalDocument(sender, parentTran, ref empty1, ref empty2, ref origLineNbr, ref extRefNbr, ref isChangeTran, ref caTranId, ref voided, ref voidedCATranID, ref drCr);
  }

  private static PX.Objects.CA.CashAccount GetDataFromOriginalDocument(
    PXCache sender,
    GLTran parentTran,
    ref string origTranType,
    ref string origRefNbr,
    ref int? origLineNbr,
    ref string extRefNbr,
    ref bool isChangeTran,
    ref long? caTranId,
    ref bool? voided,
    ref long? voidedCATranID,
    ref string drCr)
  {
    PX.Objects.CA.CashAccount cashAcct = (PX.Objects.CA.CashAccount) null;
    bool flag1 = false;
    switch (parentTran.Module)
    {
      case "AR":
        flag1 = EnumerableExtensions.IsIn<string>(parentTran.TranType, (IEnumerable<string>) ARPaymentType.AllVoidingTypes);
        PX.Objects.AR.ARPayment arPayment = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(PXSelectBase<PX.Objects.AR.ARPayment, PXSelect<PX.Objects.AR.ARPayment, Where<PX.Objects.AR.ARPayment.docType, Equal<Required<PX.Objects.AR.ARPayment.docType>>, And<PX.Objects.AR.ARPayment.refNbr, Equal<Required<PX.Objects.AR.ARPayment.refNbr>>>>>.Config>.Select(sender.Graph, new object[2]
        {
          (object) parentTran.TranType,
          (object) parentTran.RefNbr
        }));
        int? nullable1 = parentTran.TranLineNbr;
        if (nullable1.HasValue)
        {
          ARPaymentChargeTran paymentChargeTran = PXResultset<ARPaymentChargeTran>.op_Implicit(PXSelectBase<ARPaymentChargeTran, PXSelect<ARPaymentChargeTran, Where<ARPaymentChargeTran.docType, Equal<Required<ARPaymentChargeTran.docType>>, And<ARPaymentChargeTran.refNbr, Equal<Required<ARPaymentChargeTran.refNbr>>, And<ARPaymentChargeTran.lineNbr, Equal<Required<ARPaymentChargeTran.lineNbr>>>>>>.Config>.Select(sender.Graph, new object[3]
          {
            (object) parentTran.TranType,
            (object) parentTran.RefNbr,
            (object) parentTran.TranLineNbr
          }));
          cashAcct = PXSelectorAttribute.Select<ARPaymentChargeTran.cashAccountID>(sender.Graph.Caches[typeof (ARPaymentChargeTran)], (object) paymentChargeTran) as PX.Objects.CA.CashAccount;
          if (paymentChargeTran != null && cashAcct != null)
          {
            nullable1 = cashAcct.AccountID;
            int? accountId = parentTran.AccountID;
            if (nullable1.GetValueOrDefault() == accountId.GetValueOrDefault() & nullable1.HasValue == accountId.HasValue)
            {
              int? subId = cashAcct.SubID;
              nullable1 = parentTran.SubID;
              if (subId.GetValueOrDefault() == nullable1.GetValueOrDefault() & subId.HasValue == nullable1.HasValue)
              {
                string module = parentTran.Module;
                int num1 = paymentChargeTran.GetCASign() == 1 ? 1 : 0;
                Decimal CuryDocAmount = paymentChargeTran.CuryTranAmt.Value;
                Decimal? nullable2 = parentTran.CuryDebitAmt;
                Decimal CuryDebitAmount = nullable2.Value;
                nullable2 = parentTran.CuryCreditAmt;
                Decimal CuryCreditAmount = nullable2.Value;
                int num2 = flag1 ? 1 : 0;
                if (GLCashTranIDAttribute.CheckChargeAmounts(module, num1 != 0, CuryDocAmount, CuryDebitAmount, CuryCreditAmount, num2 != 0))
                {
                  origTranType = paymentChargeTran.DocType;
                  origRefNbr = paymentChargeTran.RefNbr;
                  origLineNbr = paymentChargeTran.LineNbr;
                  extRefNbr = paymentChargeTran.ExtRefNbr;
                  caTranId = paymentChargeTran.CashTranID;
                  drCr = paymentChargeTran.DrCr;
                  isChangeTran = true;
                }
              }
            }
          }
        }
        else
        {
          cashAcct = PXSelectorAttribute.Select<PX.Objects.AR.ARPayment.cashAccountID>(sender.Graph.Caches[typeof (PX.Objects.AR.ARPayment)], (object) arPayment) as PX.Objects.CA.CashAccount;
          if (arPayment != null && cashAcct != null)
          {
            nullable1 = cashAcct.AccountID;
            int? accountId = parentTran.AccountID;
            if (nullable1.GetValueOrDefault() == accountId.GetValueOrDefault() & nullable1.HasValue == accountId.HasValue)
            {
              int? subId = cashAcct.SubID;
              nullable1 = parentTran.SubID;
              if (subId.GetValueOrDefault() == nullable1.GetValueOrDefault() & subId.HasValue == nullable1.HasValue && GLCashTranIDAttribute.CheckDocTypeAmounts(parentTran.Module, parentTran.TranType, arPayment.CuryOrigDocAmt.Value, parentTran.CuryDebitAmt.Value, parentTran.CuryCreditAmt.Value))
              {
                origTranType = arPayment.DocType;
                origRefNbr = arPayment.RefNbr;
                origLineNbr = new int?();
                extRefNbr = arPayment.ExtRefNbr;
                caTranId = arPayment.CATranID;
              }
            }
          }
        }
        if (arPayment != null)
        {
          voided = arPayment.Voided;
          break;
        }
        break;
      case "AP":
        flag1 = EnumerableExtensions.IsIn<string>(parentTran.TranType, (IEnumerable<string>) APPaymentType.AllVoidingTypes);
        PX.Objects.AP.APPayment apPayment = PXResultset<PX.Objects.AP.APPayment>.op_Implicit(PXSelectBase<PX.Objects.AP.APPayment, PXSelect<PX.Objects.AP.APPayment, Where<PX.Objects.AP.APPayment.docType, Equal<Required<PX.Objects.AP.APPayment.docType>>, And<PX.Objects.AP.APPayment.refNbr, Equal<Required<PX.Objects.AP.APPayment.refNbr>>>>>.Config>.Select(sender.Graph, new object[2]
        {
          (object) parentTran.TranType,
          (object) parentTran.RefNbr
        }));
        int? nullable3 = parentTran.TranLineNbr;
        if (nullable3.HasValue)
        {
          APPaymentChargeTran paymentChargeTran = PXResultset<APPaymentChargeTran>.op_Implicit(PXSelectBase<APPaymentChargeTran, PXSelect<APPaymentChargeTran, Where<APPaymentChargeTran.docType, Equal<Required<APPaymentChargeTran.docType>>, And<APPaymentChargeTran.refNbr, Equal<Required<APPaymentChargeTran.refNbr>>, And<APPaymentChargeTran.lineNbr, Equal<Required<APPaymentChargeTran.lineNbr>>>>>>.Config>.Select(sender.Graph, new object[3]
          {
            (object) parentTran.TranType,
            (object) parentTran.RefNbr,
            (object) parentTran.TranLineNbr
          }));
          cashAcct = PXSelectorAttribute.Select<APPaymentChargeTran.cashAccountID>(sender.Graph.Caches[typeof (APPaymentChargeTran)], (object) paymentChargeTran) as PX.Objects.CA.CashAccount;
          bool flag2 = paymentChargeTran?.DocType == "RQC" ? paymentChargeTran?.DrCr == "D" : paymentChargeTran != null && paymentChargeTran.GetCASign() == 1;
          if (paymentChargeTran != null && cashAcct != null)
          {
            nullable3 = cashAcct.AccountID;
            int? accountId = parentTran.AccountID;
            if (nullable3.GetValueOrDefault() == accountId.GetValueOrDefault() & nullable3.HasValue == accountId.HasValue)
            {
              int? subId = cashAcct.SubID;
              nullable3 = parentTran.SubID;
              if (subId.GetValueOrDefault() == nullable3.GetValueOrDefault() & subId.HasValue == nullable3.HasValue)
              {
                string module = parentTran.Module;
                int num3 = flag2 ? 1 : 0;
                Decimal? nullable4 = paymentChargeTran.CuryTranAmt;
                Decimal CuryDocAmount = nullable4.Value;
                nullable4 = parentTran.CuryDebitAmt;
                Decimal CuryDebitAmount = nullable4.Value;
                nullable4 = parentTran.CuryCreditAmt;
                Decimal CuryCreditAmount = nullable4.Value;
                int num4 = flag1 ? 1 : 0;
                if (GLCashTranIDAttribute.CheckChargeAmounts(module, num3 != 0, CuryDocAmount, CuryDebitAmount, CuryCreditAmount, num4 != 0))
                {
                  origTranType = paymentChargeTran.DocType;
                  origRefNbr = paymentChargeTran.RefNbr;
                  origLineNbr = paymentChargeTran.LineNbr;
                  extRefNbr = paymentChargeTran.ExtRefNbr;
                  caTranId = paymentChargeTran.CashTranID;
                  drCr = paymentChargeTran.DrCr;
                  isChangeTran = true;
                }
              }
            }
          }
        }
        else
        {
          cashAcct = PXSelectorAttribute.Select<PX.Objects.AP.APPayment.cashAccountID>(sender.Graph.Caches[typeof (PX.Objects.AP.APPayment)], (object) apPayment) as PX.Objects.CA.CashAccount;
          if (apPayment != null && cashAcct != null)
          {
            nullable3 = cashAcct.AccountID;
            int? accountId = parentTran.AccountID;
            if (nullable3.GetValueOrDefault() == accountId.GetValueOrDefault() & nullable3.HasValue == accountId.HasValue)
            {
              int? subId = cashAcct.SubID;
              nullable3 = parentTran.SubID;
              if (subId.GetValueOrDefault() == nullable3.GetValueOrDefault() & subId.HasValue == nullable3.HasValue && GLCashTranIDAttribute.CheckDocTypeAmounts(parentTran.Module, parentTran.TranType, apPayment.CuryOrigDocAmt.Value, parentTran.CuryDebitAmt.Value, parentTran.CuryCreditAmt.Value))
              {
                origTranType = apPayment.DocType;
                origRefNbr = apPayment.RefNbr;
                origLineNbr = new int?();
                extRefNbr = apPayment.ExtRefNbr;
                caTranId = apPayment.CATranID;
              }
            }
          }
        }
        if (apPayment != null)
        {
          voided = apPayment.Voided;
          break;
        }
        break;
      case "CA":
        string tranType = parentTran.TranType;
        if (tranType != null && tranType.Length == 3)
        {
          switch (tranType[2])
          {
            case 'D':
              if (tranType == "CVD")
                break;
              goto label_52;
            case 'E':
              switch (tranType)
              {
                case "CTE":
                  CAExpense caExpense = CAExpense.PK.Find(sender.Graph, parentTran.RefNbr, parentTran.TranLineNbr);
                  cashAcct = PXSelectorAttribute.Select<CAExpense.cashAccountID>(sender.Graph.Caches[typeof (CAExpense)], (object) caExpense) as PX.Objects.CA.CashAccount;
                  if (caExpense != null)
                  {
                    origTranType = "CTE";
                    origRefNbr = caExpense.RefNbr;
                    origLineNbr = caExpense.LineNbr;
                    extRefNbr = caExpense.ExtRefNbr;
                    caTranId = caExpense.CashTranID;
                    drCr = caExpense.DrCr;
                    goto label_52;
                  }
                  goto label_52;
                case "CAE":
                  if (!parentTran.TranLineNbr.HasValue)
                  {
                    CAAdj caAdj = CAAdj.PK.Find(sender.Graph, parentTran.TranType, parentTran.RefNbr);
                    cashAcct = PXSelectorAttribute.Select<CAAdj.cashAccountID>(sender.Graph.Caches[typeof (CAAdj)], (object) caAdj) as PX.Objects.CA.CashAccount;
                    if (caAdj != null)
                    {
                      origTranType = "CAE";
                      origRefNbr = caAdj.AdjRefNbr;
                      origLineNbr = new int?();
                      extRefNbr = caAdj.ExtRefNbr;
                      caTranId = caAdj.TranID;
                      drCr = caAdj.DrCr;
                      goto label_52;
                    }
                    goto label_52;
                  }
                  goto label_52;
                default:
                  goto label_52;
              }
            case 'I':
              if (tranType == "CTI")
              {
                CATransfer caTransfer = PXResultset<CATransfer>.op_Implicit(PXSelectBase<CATransfer, PXSelect<CATransfer, Where<CATransfer.transferNbr, Equal<Required<CATransfer.transferNbr>>>>.Config>.Select(sender.Graph, new object[1]
                {
                  (object) parentTran.RefNbr
                }));
                cashAcct = PXSelectorAttribute.Select<CATransfer.inAccountID>(sender.Graph.Caches[typeof (CATransfer)], (object) caTransfer) as PX.Objects.CA.CashAccount;
                int? accountId = cashAcct.AccountID;
                int? transitAcctId = caTransfer.TransitAcctID;
                if (accountId.GetValueOrDefault() == transitAcctId.GetValueOrDefault() & accountId.HasValue == transitAcctId.HasValue)
                {
                  int? subId = cashAcct.SubID;
                  int? transitSubId = caTransfer.TransitSubID;
                  if (subId.GetValueOrDefault() == transitSubId.GetValueOrDefault() & subId.HasValue == transitSubId.HasValue)
                    goto label_52;
                }
                if (caTransfer != null)
                {
                  origTranType = "CTI";
                  origRefNbr = caTransfer.RefNbr;
                  origLineNbr = new int?();
                  extRefNbr = caTransfer.InExtRefNbr;
                  caTranId = caTransfer.TranIDIn;
                  goto label_52;
                }
                goto label_52;
              }
              goto label_52;
            case 'O':
              if (tranType == "CTO")
              {
                CATransfer caTransfer = PXResultset<CATransfer>.op_Implicit(PXSelectBase<CATransfer, PXSelect<CATransfer, Where<CATransfer.transferNbr, Equal<Required<CATransfer.transferNbr>>>>.Config>.Select(sender.Graph, new object[1]
                {
                  (object) parentTran.RefNbr
                }));
                cashAcct = PXSelectorAttribute.Select<CATransfer.outAccountID>(sender.Graph.Caches[typeof (CATransfer)], (object) caTransfer) as PX.Objects.CA.CashAccount;
                int? accountId = cashAcct.AccountID;
                int? transitAcctId = caTransfer.TransitAcctID;
                if (accountId.GetValueOrDefault() == transitAcctId.GetValueOrDefault() & accountId.HasValue == transitAcctId.HasValue)
                {
                  int? subId = cashAcct.SubID;
                  int? transitSubId = caTransfer.TransitSubID;
                  if (subId.GetValueOrDefault() == transitSubId.GetValueOrDefault() & subId.HasValue == transitSubId.HasValue)
                    goto label_52;
                }
                if (caTransfer != null)
                {
                  origTranType = "CTO";
                  origRefNbr = caTransfer.RefNbr;
                  origLineNbr = new int?();
                  extRefNbr = caTransfer.OutExtRefNbr;
                  caTranId = caTransfer.TranIDOut;
                  goto label_52;
                }
                goto label_52;
              }
              goto label_52;
            case 'T':
              if (tranType == "CDT")
                break;
              goto label_52;
            case 'X':
              if (tranType == "CDX" || tranType == "CVX")
                break;
              goto label_52;
            default:
              goto label_52;
          }
          bool flag3 = parentTran.TranType == "CVD" || parentTran.TranType == "CVX";
          if (parentTran.TranLineNbr.HasValue)
          {
            PXResult<CADepositDetail, CADeposit> pxResult = (PXResult<CADepositDetail, CADeposit>) PXResultset<CADepositDetail>.op_Implicit(PXSelectBase<CADepositDetail, PXSelectJoin<CADepositDetail, InnerJoin<CADeposit, On<CADepositDetail.FK.CashAccountDeposit>>, Where<CADepositDetail.tranType, Equal<Required<CADepositDetail.tranType>>, And<CADepositDetail.refNbr, Equal<Required<CADepositDetail.refNbr>>, And<CADepositDetail.lineNbr, Equal<Required<CADepositDetail.lineNbr>>>>>>.Config>.Select(sender.Graph, new object[3]
            {
              (object) parentTran.TranType,
              (object) parentTran.RefNbr,
              (object) parentTran.TranLineNbr
            }));
            CADepositDetail caDepositDetail = PXResult<CADepositDetail, CADeposit>.op_Implicit(pxResult);
            CADeposit caDeposit = PXResult<CADepositDetail, CADeposit>.op_Implicit(pxResult);
            cashAcct = PXSelectorAttribute.Select<CADepositDetail.cashAccountID>(sender.Graph.Caches[typeof (CADepositDetail)], (object) caDepositDetail) as PX.Objects.CA.CashAccount;
            if (caDepositDetail != null)
            {
              origTranType = caDepositDetail.TranType;
              origRefNbr = caDepositDetail.RefNbr;
              origLineNbr = caDepositDetail.LineNbr;
              extRefNbr = caDeposit.ExtRefNbr;
              caTranId = caDepositDetail.TranID;
              voided = caDeposit.Voided;
            }
          }
          else
          {
            bool flag4 = parentTran.TranType == "CDX" || parentTran.TranType == "CVX";
            string str = flag4 ? (flag3 ? "CVD" : "CDT") : parentTran.TranType;
            CADeposit caDeposit = PXResultset<CADeposit>.op_Implicit(PXSelectBase<CADeposit, PXSelect<CADeposit, Where<CADeposit.tranType, Equal<Required<CADeposit.tranType>>, And<CADeposit.refNbr, Equal<Required<CADeposit.refNbr>>>>>.Config>.Select(sender.Graph, new object[2]
            {
              (object) str,
              (object) parentTran.RefNbr
            }));
            if (caDeposit != null)
            {
              cashAcct = !flag4 ? PXSelectorAttribute.Select<CADeposit.cashAccountID>(sender.Graph.Caches[typeof (CADeposit)], (object) caDeposit) as PX.Objects.CA.CashAccount : PXSelectorAttribute.Select<CADeposit.extraCashAccountID>(sender.Graph.Caches[typeof (CADeposit)], (object) caDeposit) as PX.Objects.CA.CashAccount;
              origTranType = flag4 ? (flag3 ? "CVX" : "CDX") : caDeposit.TranType;
              origRefNbr = caDeposit.RefNbr;
              origLineNbr = new int?();
              extRefNbr = caDeposit.ExtRefNbr;
              caTranId = flag4 ? caDeposit.CashTranID : caDeposit.TranID;
              voided = caDeposit.Voided;
            }
          }
          flag1 = flag3;
          break;
        }
        break;
    }
label_52:
    if (flag1)
    {
      string voidedType1 = (string) null;
      string voidedType2 = (string) null;
      switch (parentTran.Module)
      {
        case "AR":
          string[] voidedArDocType = ARPaymentType.GetVoidedARDocType(parentTran.TranType);
          voidedType1 = voidedArDocType[0];
          if (((IEnumerable<string>) voidedArDocType).Count<string>() > 1)
          {
            voidedType2 = voidedArDocType[1];
            break;
          }
          break;
        case "AP":
          string[] voidedApDocType = APPaymentType.GetVoidedAPDocType(parentTran.TranType);
          voidedType1 = voidedApDocType[0];
          if (((IEnumerable<string>) voidedApDocType).Count<string>() > 1)
          {
            voidedType2 = voidedApDocType[1];
            break;
          }
          break;
        case "CA":
          switch (parentTran.TranType)
          {
            case "CVD":
              voidedType1 = "CDT";
              break;
            case "CVX":
              voidedType1 = "CDX";
              break;
          }
          break;
      }
      if (cashAcct != null)
        voidedCATranID = GLCashTranIDAttribute.GetVoidedCATranID(sender.Graph, cashAcct, parentTran, voidedType1, voidedType2);
    }
    return cashAcct;
  }

  private static long? GetVoidedCATranID(
    PXGraph graph,
    PX.Objects.CA.CashAccount cashAcct,
    GLTran parentTran,
    string voidedType1,
    string voidedType2)
  {
    CATran caTran = GLCashTranIDAttribute.SelectCATran(graph, cashAcct.CashAccountID, parentTran.Module, voidedType1, parentTran.RefNbr, parentTran.TranLineNbr);
    if (caTran == null && !string.IsNullOrEmpty(voidedType2))
      caTran = GLCashTranIDAttribute.SelectCATran(graph, cashAcct.CashAccountID, parentTran.Module, voidedType2, parentTran.RefNbr, parentTran.TranLineNbr);
    return caTran?.TranID;
  }

  private static CATran SelectCATran(
    PXGraph graph,
    int? cashAccountID,
    string module,
    string tranType,
    string refNbr,
    int? lineNbr)
  {
    return lineNbr.HasValue ? PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.cashAccountID, Equal<Required<CATran.cashAccountID>>, And<CATran.origModule, Equal<Required<CATran.origModule>>, And<CATran.origTranType, Equal<Required<CATran.origTranType>>, And<CATran.origRefNbr, Equal<Required<CATran.origRefNbr>>, And<CATran.origLineNbr, Equal<Required<CATran.origLineNbr>>>>>>>>.Config>.Select(graph, new object[5]
    {
      (object) cashAccountID,
      (object) module,
      (object) tranType,
      (object) refNbr,
      (object) lineNbr
    })) : PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.cashAccountID, Equal<Required<CATran.cashAccountID>>, And<CATran.origModule, Equal<Required<CATran.origModule>>, And<CATran.origTranType, Equal<Required<CATran.origTranType>>, And<CATran.origRefNbr, Equal<Required<CATran.origRefNbr>>, And<CATran.origLineNbr, IsNull>>>>>>.Config>.Select(graph, new object[4]
    {
      (object) cashAccountID,
      (object) module,
      (object) tranType,
      (object) refNbr
    }));
  }

  private static void SetAmounts(CATran caTran, GLTran glTran)
  {
    CATran caTran1 = caTran;
    Decimal? nullable1 = glTran.CuryDebitAmt;
    Decimal? curyCreditAmt = glTran.CuryCreditAmt;
    Decimal? nullable2 = nullable1.HasValue & curyCreditAmt.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - curyCreditAmt.GetValueOrDefault()) : new Decimal?();
    caTran1.CuryTranAmt = nullable2;
    CATran caTran2 = caTran;
    Decimal? debitAmt = glTran.DebitAmt;
    nullable1 = glTran.CreditAmt;
    Decimal? nullable3 = debitAmt.HasValue & nullable1.HasValue ? new Decimal?(debitAmt.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    caTran2.TranAmt = nullable3;
  }

  /// <returns>
  /// <c>true</c>, if the <see cref="T:PX.Objects.GL.Batch" /> record, to which the current <see cref="T:PX.Objects.GL.GLTran" />
  /// row belongs, is part of a recurring schedule, and a default value otherwise.
  /// </returns>
  protected override bool NeedPreventCashTransactionCreation(PXCache sender, object row)
  {
    if (!(row is GLTran glTran) || glTran.Module == null || glTran.BatchNbr == null)
      return base.NeedPreventCashTransactionCreation(sender, row);
    PXCache cach = sender.Graph.Caches[typeof (Batch)];
    if ((cach.Current is Batch current ? current.Module : (string) null) == glTran.Module && current?.BatchNbr == glTran.BatchNbr)
      return current != null && current.Scheduled.GetValueOrDefault();
    if (cach.CreateInstance() is Batch instance)
    {
      instance.Module = glTran.Module;
      instance.BatchNbr = glTran.BatchNbr;
      if (cach.Locate((object) instance) is Batch batch)
        return batch.Scheduled.GetValueOrDefault();
    }
    Batch batch1 = PXResultset<Batch>.op_Implicit(PXSelectBase<Batch, PXSelect<Batch, Where<Batch.module, Equal<Required<Batch.module>>, And<Batch.batchNbr, Equal<Required<Batch.batchNbr>>>>>.Config>.Select(sender.Graph, new object[2]
    {
      (object) glTran.Module,
      (object) glTran.BatchNbr
    }));
    return batch1 != null ? batch1.Scheduled.GetValueOrDefault() : base.NeedPreventCashTransactionCreation(sender, row);
  }
}
