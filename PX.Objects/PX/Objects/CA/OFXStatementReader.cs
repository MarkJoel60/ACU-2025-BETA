// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.OFXStatementReader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class OFXStatementReader : OFXReaderBase, IStatementReader
{
  protected static bool skipTime = true;

  public OFXStatementReader() => this.useConverter = true;

  public void ExportToNew<T>(FileInfo aFileInfo, T current, out List<CABankTranHeader> aExported) where T : CABankTransactionsImport, new()
  {
    UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
    aExported = new List<CABankTranHeader>();
    T obj = default (T);
    T graph = (object) current == null ? PXGraph.CreateInstance<T>() : current;
    this.ExportToNew<T>(graph, aExported);
    FileInfo fileInfo = aFileInfo;
    if (!instance.SaveFile(fileInfo, (FileExistsAction) 1))
      return;
    Guid? uid = aFileInfo.UID;
    if (!uid.HasValue)
      return;
    foreach (CABankTranHeader caBankTranHeader in aExported)
    {
      if (((PXSelectBase<CABankTranHeader>) graph.Header).Current != caBankTranHeader)
        ((PXSelectBase<CABankTranHeader>) graph.Header).Current = PXResultset<CABankTranHeader>.op_Implicit(((PXSelectBase<CABankTranHeader>) graph.Header).Search<CABankTranHeader.cashAccountID, CABankTranHeader.refNbr>((object) caBankTranHeader.CashAccountID, (object) caBankTranHeader.RefNbr, Array.Empty<object>()));
      PXCache cache = ((PXSelectBase) graph.Header).Cache;
      CABankTranHeader current1 = ((PXSelectBase<CABankTranHeader>) graph.Header).Current;
      Guid[] guidArray = new Guid[1];
      uid = aFileInfo.UID;
      guidArray[0] = uid.Value;
      PXNoteAttribute.SetFileNotes(cache, (object) current1, guidArray);
      ((PXAction) graph.Save).Press();
    }
  }

  public void ExportToNew<T>(T graph, List<CABankTranHeader> aStatements) where T : CABankTransactionsImport
  {
    bool valueOrDefault1 = ((PXSelectBase<CASetup>) graph.CASetup).Current.ImportToSingleAccount.GetValueOrDefault();
    bool valueOrDefault2 = ((PXSelectBase<CASetup>) graph.CASetup).Current.AllowEmptyFITID.GetValueOrDefault();
    HashSet<string> notExistCashAccounts = new HashSet<string>();
    HashSet<string> notSelectedCashAccounts = new HashSet<string>();
    HashSet<string> linkedToBankFeed = new HashSet<string>();
    foreach (OFXReaderBase.STMTRS stmtr in this.Content.Stmtrs)
    {
      this.ExportNew<T>(graph, stmtr, valueOrDefault1, ref notExistCashAccounts, ref notSelectedCashAccounts, ref linkedToBankFeed, valueOrDefault2);
      aStatements.Add(((PXSelectBase<CABankTranHeader>) graph.Header).Current);
    }
    if (notExistCashAccounts.Any<string>() && notSelectedCashAccounts.Any<string>())
      throw new PXException("Accounts with the following Ext Ref Numbers could not be found in the system: {0}\n\nThe selected file contains information about other accounts: {1}.", new object[2]
      {
        (object) string.Join(string.Empty, (IEnumerable<string>) notExistCashAccounts),
        (object) string.Join(string.Empty, (IEnumerable<string>) notSelectedCashAccounts)
      });
    if (notExistCashAccounts.Any<string>())
      throw new PXException("Accounts with the following Ext Ref Numbers could not be found in the system: {0}", new object[1]
      {
        (object) string.Join(string.Empty, (IEnumerable<string>) notExistCashAccounts)
      });
    if (linkedToBankFeed.Any<string>())
      throw new PXException(string.Join("", (IEnumerable<string>) linkedToBankFeed));
    if (notSelectedCashAccounts.Any<string>())
      throw new PXException("The selected file contains information about other accounts: {0}.", new object[1]
      {
        (object) string.Join(string.Empty, (IEnumerable<string>) notSelectedCashAccounts)
      });
  }

  protected void ExportNew<T>(
    T aGraph,
    OFXReaderBase.STMTRS aAcctStatement,
    bool updateCurrent,
    ref HashSet<string> notExistCashAccounts,
    ref HashSet<string> notSelectedCashAccounts,
    ref HashSet<string> linkedToBankFeed,
    bool allowEmptyFITID = false)
    where T : CABankTransactionsImport
  {
    if (!aAcctStatement.HasAccountInfo())
      throw new PXException("Account information in the file is invalid or has an unsupported format");
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(((PXSelectBase<CashAccount>) aGraph.cashAccountByExtRef).Select(new object[1]
    {
      aAcctStatement.IsBankAccount() ? (object) aAcctStatement.BANKACCTFROM.ACCTID : (object) aAcctStatement.CCACCTFROM.ACCTID
    }));
    if (cashAccount == null)
    {
      notExistCashAccounts.Add("\n" + (aAcctStatement.IsBankAccount() ? aAcctStatement.BANKACCTFROM.ACCTID : aAcctStatement.CCACCTFROM.ACCTID));
    }
    else
    {
      string message;
      if (aGraph.CheckCashAccountIsLinkedToBankFeed(cashAccount, out message))
      {
        linkedToBankFeed.Add("\n" + message);
      }
      else
      {
        if (!((PXSelectBase<CASetup>) aGraph.CASetup).Current.IgnoreCuryCheckOnImport.GetValueOrDefault() && cashAccount.CuryID != aAcctStatement.CURDEF)
          throw new PXException("Account {0} has currency {1} different from one specified in the statement. Statement can not be imported. Please, check correctness of the cash account's Ext Ref Nbr and other settings", new object[3]
          {
            (object) cashAccount.CashAccountCD,
            (object) cashAccount.CuryID,
            (object) aAcctStatement.CURDEF
          });
        CABankTranHeader caBankTranHeader1 = (CABankTranHeader) null;
        CABankTranHeader caBankTranHeader2;
        if (updateCurrent && ((PXSelectBase<CABankTranHeader>) aGraph.Header).Current != null)
        {
          int? cashAccountId1 = ((PXSelectBase<CABankTranHeader>) aGraph.Header).Current.CashAccountID;
          if (cashAccountId1.HasValue)
          {
            caBankTranHeader2 = ((PXSelectBase<CABankTranHeader>) aGraph.Header).Current;
            cashAccountId1 = caBankTranHeader2.CashAccountID;
            if (cashAccountId1.HasValue)
            {
              cashAccountId1 = caBankTranHeader2.CashAccountID;
              int? cashAccountId2 = cashAccount.CashAccountID;
              if (!(cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue))
              {
                notSelectedCashAccounts.Add($"\n{cashAccount.CashAccountCD}-'{cashAccount.Descr}'");
                return;
              }
              goto label_14;
            }
            goto label_14;
          }
        }
        ((PXGraph) (object) aGraph).Clear();
        caBankTranHeader2 = ((PXSelectBase<CABankTranHeader>) aGraph.Header).Insert(new CABankTranHeader()
        {
          CashAccountID = cashAccount.CashAccountID
        });
        ((PXSelectBase<CABankTranHeader>) aGraph.Header).Current = caBankTranHeader2;
label_14:
        foreach (OFXReaderBase.STMTTRN tran in aAcctStatement.BANKTRANLIST.Trans)
        {
          if (!allowEmptyFITID && tran.FITID == null)
            throw new PXException("The file does not comply with the standard format: FITID is empty. You will be able to upload the file if you select the Allow Empty FITID check box on the CA Preferences form.");
          string aRefNbr;
          if (aGraph.IsAlreadyImported(cashAccount.CashAccountID, tran.FITID, out aRefNbr))
            throw new PXException("Transaction with FITID {0} is found in the existing Statement: {1} for the Account: {2}-'{3}'. Most likely, this file has already been imported", new object[4]
            {
              (object) tran.FITID,
              (object) aRefNbr,
              (object) cashAccount.CashAccountCD,
              (object) cashAccount.Descr
            });
        }
        CABankTranHeader copy1 = (CABankTranHeader) ((PXSelectBase) aGraph.Header).Cache.CreateCopy((object) caBankTranHeader2);
        OFXStatementReader.Copy(copy1, aAcctStatement);
        caBankTranHeader1 = ((PXSelectBase<CABankTranHeader>) aGraph.Header).Update(copy1);
        foreach (OFXReaderBase.STMTTRN tran in aAcctStatement.BANKTRANLIST.Trans)
        {
          CABankTran aDest = new CABankTran();
          aDest.CashAccountID = cashAccount.CashAccountID;
          OFXStatementReader.Copy(aDest, tran);
          CABankTran caBankTran = ((PXSelectBase<CABankTran>) aGraph.Details).Insert(aDest);
          CABankTran copy2 = (CABankTran) ((PXSelectBase) aGraph.Details).Cache.CreateCopy((object) caBankTran);
          OFXStatementReader.CopyForUpdate(copy2, tran);
          ((PXSelectBase<CABankTran>) aGraph.Details).Update(copy2);
        }
        ((PXAction) aGraph.Save).Press();
      }
    }
  }

  protected static void Copy(CABankTranHeader aDest, OFXReaderBase.STMTRS aSrc)
  {
    aDest.BankStatementFormat = "OFX";
    if (aSrc.LEDGERBAL.DTASOF.HasValue)
      aDest.DocDate = OFXStatementReader.skipTime ? new DateTime?(aSrc.LEDGERBAL.DTASOF.Value.Date) : aSrc.LEDGERBAL.DTASOF;
    if (aSrc.BANKTRANLIST.DTSTART.HasValue)
      aDest.StartBalanceDate = OFXStatementReader.skipTime ? new DateTime?(aSrc.BANKTRANLIST.DTSTART.Value.Date) : aSrc.BANKTRANLIST.DTSTART;
    if (aSrc.BANKTRANLIST.DTEND.HasValue)
      aDest.EndBalanceDate = OFXStatementReader.skipTime ? new DateTime?(aSrc.BANKTRANLIST.DTEND.Value.Date) : aSrc.BANKTRANLIST.DTEND;
    aDest.CuryEndBalance = new Decimal?(aSrc.LEDGERBAL.BALAMT);
  }

  protected static void Copy(CABankTran aDest, OFXReaderBase.STMTTRN aSrc)
  {
    aDest.TranCode = aSrc.TRNTYPE;
    aDest.TranDate = !aSrc.DTPOSTED.HasValue || !OFXStatementReader.skipTime ? aSrc.DTPOSTED : new DateTime?(aSrc.DTPOSTED.Value.Date);
    aDest.ExtTranID = aSrc.FITID;
    aDest.ExtRefNbr = !string.IsNullOrEmpty(aSrc.CHECKNUM) ? aSrc.CHECKNUM : (string.IsNullOrEmpty(aSrc.REFNUM) || aSrc.REFNUM.Length >= 15 ? (string.IsNullOrEmpty(aSrc.FITID) || aSrc.FITID.Length >= 15 ? (string) null : aSrc.FITID) : aSrc.REFNUM);
    aDest.TranDesc = !string.IsNullOrEmpty(aSrc.NAME) ? aSrc.NAME + " " : string.Empty;
    aDest.TranDesc += aSrc.MEMO;
    aDest.TranDesc = aDest.TranDesc.Trim();
    if (aSrc.HasPayeeInfo())
    {
      aDest.PayeeName = aSrc.PAYEE.NAME;
      aDest.PayeeAddress1 = aSrc.PAYEE.ADDR1;
      aDest.PayeePostalCode = aSrc.PAYEE.POSTALCODE;
      aDest.PayeePhone = aSrc.PAYEE.PHONE;
      aDest.PayeeState = aSrc.PAYEE.STATE;
      aDest.PayeeCity = aSrc.PAYEE.CITY;
    }
    else
      aDest.PayeeName = aSrc.NAME;
  }

  protected static void CopyForUpdate(CABankTran aDest, OFXReaderBase.STMTTRN aSrc)
  {
    if (aSrc.TRNAMT >= 0M)
      aDest.CuryDebitAmt = new Decimal?(aSrc.TRNAMT);
    else
      aDest.CuryCreditAmt = new Decimal?(-aSrc.TRNAMT);
  }
}
