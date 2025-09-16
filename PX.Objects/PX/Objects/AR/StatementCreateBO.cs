// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.StatementCreateBO
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

[PXHidden]
public class StatementCreateBO : PXGraph<StatementCreateBO>
{
  public PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>> Customer;
  public PXSelect<ARStatement, Where<ARStatement.statementCycleId, Equal<Required<ARStatement.statementCycleId>>, And<ARStatement.statementDate, Equal<Required<ARStatement.statementDate>>>>> Statement;
  public PXSelect<ARStatement, Where<ARStatement.customerID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>, And<ARStatement.statementDate, Equal<Required<ARStatement.statementDate>>>>> CustomerStatement;
  public PXSelect<ARStatement, Where<ARStatement.customerID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>, And<ARStatement.onDemand, Equal<False>, And<ARStatement.statementDate, Equal<Required<ARStatement.statementDate>>>>>> CustomerStatementForDelete;
  public PXSelect<ARStatementDetail, Where<ARStatementDetail.customerID, Equal<Current<ARStatement.customerID>>, And<ARStatementDetail.statementDate, Equal<Current<ARStatement.statementDate>>, And<ARStatementDetail.curyID, Equal<Current<ARStatement.curyID>>>>>> StatementDetail;
  public PXSelect<ARRegister, Where<ARRegister.docType, Equal<Optional<ARStatementDetail.docType>>, And<ARRegister.refNbr, Equal<Optional<ARStatementDetail.refNbr>>>>> Docs;

  public virtual void ARStatement_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is ARStatement row) || row.OnDemand.GetValueOrDefault())
      return;
    PXUpdate<Set<PX.Objects.AR.Override.Customer.statementLastDate, Required<PX.Objects.AR.Override.Customer.statementLastDate>>, PX.Objects.AR.Override.Customer, Where<PX.Objects.AR.Override.Customer.bAccountID, Equal<Required<PX.Objects.AR.Override.Customer.bAccountID>>, And<PX.Objects.AR.Override.Customer.statementLastDate, Equal<Required<PX.Objects.AR.Override.Customer.statementLastDate>>>>>.Update((PXGraph) this, new object[3]
    {
      (object) this.FindLastCstmStatementDate(row.CustomerID, row.StatementDate),
      (object) row.CustomerID,
      (object) row.StatementDate
    });
  }

  public DateTime? FindLastCstmStatementDate(int? aCustomer, DateTime? aBeforeDate)
  {
    return GraphHelper.RowCast<ARStatement>((IEnumerable) PXSelectBase<ARStatement, PXSelect<ARStatement, Where<ARStatement.customerID, Equal<Required<ARStatement.customerID>>, And<ARStatement.statementDate, Less<Required<ARStatement.statementDate>>, And<ARStatement.onDemand, Equal<False>>>>, OrderBy<Desc<ARStatement.statementDate>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
    {
      (object) aCustomer,
      (object) aBeforeDate
    })).FirstOrDefault<ARStatement>()?.StatementDate;
  }

  public virtual void ARStatementDetail_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is ARStatementDetail row) || this.FindParentStatement(row.BranchID, row.CuryID, row.CustomerID, row.StatementDate).OnDemand.GetValueOrDefault())
      return;
    PXUpdate<Set<ARAdjust.statementDate, Null>, ARAdjust, Where<ARAdjust.noteID, Equal<Required<ARStatementDetail.refNoteID>>>>.Update((PXGraph) this, new object[1]
    {
      (object) row.RefNoteID
    });
  }

  private ARStatement FindParentStatement(
    int? branchID,
    string currencyID,
    int? customerID,
    DateTime? statementDate)
  {
    ARStatement current1 = ((PXSelectBase<ARStatement>) this.Statement).Current;
    int num1;
    if (current1 == null)
    {
      num1 = !branchID.HasValue ? 1 : 0;
    }
    else
    {
      int? branchId = current1.BranchID;
      int? nullable = branchID;
      num1 = branchId.GetValueOrDefault() == nullable.GetValueOrDefault() & branchId.HasValue == nullable.HasValue ? 1 : 0;
    }
    if (num1 != 0 && ((PXSelectBase<ARStatement>) this.Statement).Current?.CuryID == currencyID)
    {
      ARStatement current2 = ((PXSelectBase<ARStatement>) this.Statement).Current;
      int num2;
      if (current2 == null)
      {
        num2 = !customerID.HasValue ? 1 : 0;
      }
      else
      {
        int? customerId = current2.CustomerID;
        int? nullable = customerID;
        num2 = customerId.GetValueOrDefault() == nullable.GetValueOrDefault() & customerId.HasValue == nullable.HasValue ? 1 : 0;
      }
      if (num2 != 0)
      {
        ARStatement current3 = ((PXSelectBase<ARStatement>) this.Statement).Current;
        int num3;
        if (current3 == null)
        {
          num3 = !statementDate.HasValue ? 1 : 0;
        }
        else
        {
          DateTime? statementDate1 = current3.StatementDate;
          DateTime? nullable = statementDate;
          num3 = statementDate1.HasValue == nullable.HasValue ? (statementDate1.HasValue ? (statementDate1.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0) : 1) : 0;
        }
        if (num3 != 0)
          return ((PXSelectBase<ARStatement>) this.Statement).Current;
      }
    }
    ARStatement parentStatement = ((PXSelectBase<ARStatement>) this.Statement).Locate(new ARStatement()
    {
      BranchID = branchID,
      CuryID = currencyID,
      CustomerID = customerID,
      StatementDate = statementDate
    });
    if (parentStatement != null)
      return parentStatement;
    return PXResultset<ARStatement>.op_Implicit(PXSelectBase<ARStatement, PXSelect<ARStatement, Where<ARStatement.branchID, Equal<Required<ARStatement.branchID>>, And<ARStatement.curyID, Equal<Required<ARStatement.curyID>>, And<ARStatement.customerID, Equal<Required<ARStatement.customerID>>, And<ARStatement.statementDate, Equal<Required<ARStatement.statementDate>>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[4]
    {
      (object) branchID,
      (object) currencyID,
      (object) customerID,
      (object) statementDate
    }));
  }
}
