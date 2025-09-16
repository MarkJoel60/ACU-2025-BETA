// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankStatementProtoHelpers.StatementsMatchingProto
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA.BankStatementProtoHelpers;

public static class StatementsMatchingProto
{
  public static string GetStatus(CATran tran)
  {
    if (tran.Posted.GetValueOrDefault())
      return tran.Released.GetValueOrDefault() ? "P" : "U";
    if (tran.Released.GetValueOrDefault() && !tran.Posted.GetValueOrDefault())
      return "R";
    return tran.Hold.GetValueOrDefault() ? "H" : "B";
  }

  public static void SetDocTypeList(PXCache cache, CABankTran Row)
  {
    CABankTran caBankTran = Row;
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    if (caBankTran.OrigModule == "AP")
    {
      if (caBankTran.DocType == "REF")
      {
        PXDefaultAttribute.SetDefault<CABankTranAdjustment.adjdDocType>(cache, (object) "ADR");
        PXStringListAttribute.SetList<CABankTranAdjustment.adjdDocType>(cache, (object) null, new string[2]
        {
          "ADR",
          "PPM"
        }, new string[2]{ "Debit Adj.", "Prepayment" });
      }
      else if (caBankTran.DocType == "PPM")
      {
        PXDefaultAttribute.SetDefault<CABankTranAdjustment.adjdDocType>(cache, (object) "INV");
        PXStringListAttribute.SetList<CABankTranAdjustment.adjdDocType>(cache, (object) null, new string[2]
        {
          "INV",
          "ACR"
        }, new string[2]{ "Bill", "Credit Adj." });
      }
      else if (caBankTran.DocType == "CHK")
      {
        PXDefaultAttribute.SetDefault<CABankTranAdjustment.adjdDocType>(cache, (object) "INV");
        PXStringListAttribute.SetList<CABankTranAdjustment.adjdDocType>(cache, (object) null, new string[5]
        {
          "INV",
          "ADR",
          "ACR",
          "PPM",
          "PPI"
        }, new string[5]
        {
          "Bill",
          "Debit Adj.",
          "Credit Adj.",
          "Prepayment",
          "Prepmt. Invoice"
        });
      }
      else
      {
        PXDefaultAttribute.SetDefault<CABankTranAdjustment.adjdDocType>(cache, (object) "INV");
        PXStringListAttribute.SetList<CABankTranAdjustment.adjdDocType>(cache, (object) null, new string[3]
        {
          "INV",
          "ACR",
          "PPM"
        }, new string[3]
        {
          "Bill",
          "Credit Adj.",
          "Prepayment"
        });
      }
    }
    else
    {
      if (!(caBankTran.OrigModule == "AR"))
        return;
      if (caBankTran.DocType == "REF")
      {
        PXDefaultAttribute.SetDefault<CABankTranAdjustment.adjdDocType>(cache, (object) "CRM");
        PXStringListAttribute.SetList<CABankTranAdjustment.adjdDocType>(cache, (object) null, new string[4]
        {
          "CRM",
          "PMT",
          "PPM",
          "PPI"
        }, new string[4]
        {
          "Credit Memo",
          "Payment",
          "Prepayment",
          "Prepmt. Invoice"
        });
      }
      else if (caBankTran.DocType == "PMT" || caBankTran.DocType == "RPM")
      {
        PXDefaultAttribute.SetDefault<CABankTranAdjustment.adjdDocType>(cache, (object) "INV");
        PXStringListAttribute.SetList<CABankTranAdjustment.adjdDocType>(cache, (object) null, new string[5]
        {
          "INV",
          "DRM",
          "CRM",
          "FCH",
          "PPI"
        }, new string[5]
        {
          "Invoice",
          "Debit Memo",
          "Credit Memo",
          "Overdue Charge",
          "Prepmt. Invoice"
        });
      }
      else
      {
        PXDefaultAttribute.SetDefault<CABankTranAdjustment.adjdDocType>(cache, (object) "INV");
        PXStringListAttribute.SetList<CABankTranAdjustment.adjdDocType>(cache, (object) null, new string[4]
        {
          "INV",
          "DRM",
          "FCH",
          "PPI"
        }, new string[4]
        {
          "Invoice",
          "Debit Memo",
          "Overdue Charge",
          "Prepmt. Invoice"
        });
      }
    }
  }

  public static void UpdateSourceDoc(PXGraph graph, CATran aTran, DateTime? clearDate)
  {
    aTran.Cleared = new bool?(true);
    aTran.ClearDate = clearDate ?? aTran.TranDate;
    graph.Caches[typeof (CATran)].Update((object) aTran);
    switch (aTran.OrigModule)
    {
      case "AP":
        if (!(aTran.OrigTranType != "GLE"))
          break;
        PX.Objects.AP.APPayment apPayment = PXResultset<PX.Objects.AP.APPayment>.op_Implicit(PXSelectBase<PX.Objects.AP.APPayment, PXSelect<PX.Objects.AP.APPayment, Where<PX.Objects.AP.APPayment.docType, Equal<Required<PX.Objects.AP.APPayment.docType>>, And<PX.Objects.AP.APPayment.refNbr, Equal<Required<PX.Objects.AP.APPayment.refNbr>>>>>.Config>.Select(graph, new object[2]
        {
          (object) aTran.OrigTranType,
          (object) aTran.OrigRefNbr
        }));
        apPayment.ClearDate = aTran.ClearDate;
        apPayment.Cleared = aTran.Cleared;
        graph.Caches[typeof (PX.Objects.AP.APPayment)].Update((object) apPayment);
        break;
      case "AR":
        if (!(aTran.OrigTranType != "GLE"))
          break;
        PX.Objects.AR.ARPayment arPayment = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(PXSelectBase<PX.Objects.AR.ARPayment, PXSelect<PX.Objects.AR.ARPayment, Where<PX.Objects.AR.ARPayment.docType, Equal<Required<PX.Objects.AR.ARPayment.docType>>, And<PX.Objects.AR.ARPayment.refNbr, Equal<Required<PX.Objects.AR.ARPayment.refNbr>>>>>.Config>.Select(graph, new object[2]
        {
          (object) aTran.OrigTranType,
          (object) aTran.OrigRefNbr
        }));
        arPayment.ClearDate = aTran.ClearDate;
        arPayment.Cleared = aTran.Cleared;
        graph.Caches[typeof (PX.Objects.AR.ARPayment)].Update((object) arPayment);
        break;
      case "CA":
        if (aTran.OrigTranType == "CDT")
        {
          if (PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.origModule, Equal<Required<CATran.origModule>>, And<CATran.origTranType, Equal<Required<CATran.origTranType>>, And<CATran.origRefNbr, Equal<Required<CATran.origRefNbr>>, And<CATran.cleared, Equal<False>, And<CATran.tranID, NotEqual<Required<CATran.tranID>>>>>>>>.Config>.Select(graph, new object[4]
          {
            (object) aTran.OrigModule,
            (object) aTran.OrigTranType,
            (object) aTran.OrigRefNbr,
            (object) aTran.TranID
          })) == null)
          {
            CADeposit caDeposit = PXResultset<CADeposit>.op_Implicit(PXSelectBase<CADeposit, PXSelect<CADeposit, Where<CADeposit.tranType, Equal<Required<CADeposit.tranType>>, And<CADeposit.refNbr, Equal<Required<CADeposit.refNbr>>>>>.Config>.Select(graph, new object[2]
            {
              (object) aTran.OrigTranType,
              (object) aTran.OrigRefNbr
            }));
            caDeposit.ClearDate = aTran.ClearDate;
            caDeposit.Cleared = aTran.Cleared;
            graph.Caches[typeof (CADeposit)].Update((object) caDeposit);
          }
        }
        if (aTran.OrigTranType == "CAE")
        {
          if (PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.origModule, Equal<Required<CATran.origModule>>, And<CATran.origTranType, Equal<Required<CATran.origTranType>>, And<CATran.origRefNbr, Equal<Required<CATran.origRefNbr>>, And<CATran.cleared, Equal<False>, And<CATran.tranID, NotEqual<Required<CATran.tranID>>>>>>>>.Config>.Select(graph, new object[4]
          {
            (object) aTran.OrigModule,
            (object) aTran.OrigTranType,
            (object) aTran.OrigRefNbr,
            (object) aTran.TranID
          })) == null)
          {
            CAAdj caAdj = PXResultset<CAAdj>.op_Implicit(PXSelectBase<CAAdj, PXSelect<CAAdj, Where<CAAdj.adjTranType, Equal<Required<CAAdj.adjTranType>>, And<CAAdj.adjRefNbr, Equal<Required<CAAdj.adjRefNbr>>>>>.Config>.Select(graph, new object[2]
            {
              (object) aTran.OrigTranType,
              (object) aTran.OrigRefNbr
            }));
            caAdj.ClearDate = aTran.ClearDate;
            caAdj.Cleared = aTran.Cleared;
            graph.Caches[typeof (CAAdj)].Update((object) caAdj);
          }
        }
        if (aTran.OrigTranType == "CTI" || aTran.OrigTranType == "CTO")
        {
          CATransfer caTransfer = PXResultset<CATransfer>.op_Implicit(PXSelectBase<CATransfer, PXSelect<CATransfer, Where<CATransfer.transferNbr, Equal<Required<CATransfer.transferNbr>>>>.Config>.Select(graph, new object[1]
          {
            (object) aTran.OrigRefNbr
          }));
          long? nullable = caTransfer.TranIDIn;
          long? tranId = aTran.TranID;
          if (nullable.GetValueOrDefault() == tranId.GetValueOrDefault() & nullable.HasValue == tranId.HasValue)
          {
            caTransfer.ClearDateIn = aTran.ClearDate;
            caTransfer.ClearedIn = aTran.Cleared;
          }
          long? tranIdOut = caTransfer.TranIDOut;
          nullable = aTran.TranID;
          if (tranIdOut.GetValueOrDefault() == nullable.GetValueOrDefault() & tranIdOut.HasValue == nullable.HasValue)
          {
            caTransfer.ClearDateOut = aTran.ClearDate;
            caTransfer.ClearedOut = aTran.Cleared;
          }
          graph.Caches[typeof (CATransfer)].Update((object) caTransfer);
        }
        if (!(aTran.OrigTranType == "CTE"))
          break;
        CAExpense caExpense = PXResultset<CAExpense>.op_Implicit(PXSelectBase<CAExpense, PXSelect<CAExpense, Where<CAExpense.cashTranID, Equal<Required<CATran.tranID>>, And<CAExpense.cleared, NotEqual<True>>>>.Config>.Select(graph, new object[1]
        {
          (object) aTran.TranID
        }));
        caExpense.Cleared = aTran.Cleared;
        caExpense.ClearDate = aTran.ClearDate;
        graph.Caches[typeof (CAExpense)].Update((object) caExpense);
        break;
    }
  }
}
