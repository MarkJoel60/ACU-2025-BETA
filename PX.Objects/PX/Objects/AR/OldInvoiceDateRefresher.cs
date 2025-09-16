// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.OldInvoiceDateRefresher
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

/// <summary>
/// Internal class used to update <see cref="P:PX.Objects.AR.ARBalances.OldInvoiceDate" /> for the AR documents affected by the release process after it is finished.
/// </summary>
internal class OldInvoiceDateRefresher
{
  private HashSet<OldInvoiceDateRefresher.Key> _trace = new HashSet<OldInvoiceDateRefresher.Key>();
  private ARInvoiceEarliestDueDateGraph dueDateGraph = PXGraph.CreateInstance<ARInvoiceEarliestDueDateGraph>();

  public void RecordDocument(int? branchID, int? customerID, int? customerLocationID)
  {
    this._trace.Add(new OldInvoiceDateRefresher.Key()
    {
      BranchID = branchID,
      CustomerID = customerID,
      CustomerLocationID = customerLocationID
    });
  }

  public void CommitRefresh(PXGraph graph)
  {
    foreach (OldInvoiceDateRefresher.Key key in this._trace)
    {
      ((PXSelectBase) this.dueDateGraph.EarliestDueDate).View.Clear();
      ARInvoiceEarliestDueDate invoiceEarliestDueDate = PXResultset<ARInvoiceEarliestDueDate>.op_Implicit(((PXSelectBase<ARInvoiceEarliestDueDate>) this.dueDateGraph.EarliestDueDate).Select(new object[3]
      {
        (object) key.CustomerID,
        (object) key.CustomerLocationID,
        (object) key.BranchID
      }));
      PXUpdate<Set<ARBalances.oldInvoiceDate, Required<ARInvoiceEarliestDueDate.dueDate>>, ARBalances, Where<ARBalances.customerID, Equal<Required<ARInvoice.customerID>>, And<ARBalances.customerLocationID, Equal<Required<ARInvoice.customerLocationID>>, And<ARBalances.branchID, Equal<Required<ARInvoice.branchID>>>>>>.Update(graph, new object[4]
      {
        (object) (DateTime?) invoiceEarliestDueDate?.DueDate,
        (object) key.CustomerID,
        (object) key.CustomerLocationID,
        (object) key.BranchID
      });
    }
  }

  private struct Key
  {
    public int? BranchID;
    public int? CustomerID;
    public int? CustomerLocationID;

    public override int GetHashCode()
    {
      return ((17 * 23 + this.BranchID.GetValueOrDefault()) * 23 + this.CustomerID.GetValueOrDefault()) * 23 + this.CustomerLocationID.GetValueOrDefault();
    }
  }
}
