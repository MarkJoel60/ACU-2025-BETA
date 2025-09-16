// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARStatementUpdate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.CustomerStatements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

public class ARStatementUpdate : PXGraph<ARStatementUpdate, PX.Objects.AR.Customer>
{
  public PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Optional<ARStatement.customerID>>>> Customer;
  [PXViewName("AR Contact")]
  public PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AR.Customer.defBillContactID>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<ARStatement.customerID>>>> contact;
  public PXSelect<ARStatement, Where<ARStatement.statementCustomerID, Equal<Optional<PX.Objects.AR.Customer.bAccountID>>, And<ARStatement.statementDate, Equal<Required<ARStatement.statementDate>>, And<Where<ARStatement.curyID, Equal<Required<ARStatement.curyID>>, Or<Required<ARStatement.curyID>, IsNull>>>>>> StatementMC;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;

  public virtual void EMailStatement(
    int? branchID,
    string branchCD,
    int? customerID,
    DateTime? statementDate,
    string currency,
    string statementMessage,
    bool markOnly,
    bool showAll)
  {
    this.EMailStatement(branchID, branchCD, customerID, statementDate, currency, statementMessage, markOnly, showAll, new int?());
  }

  public virtual void EMailStatement(
    int? branchID,
    string branchCD,
    int? customerID,
    DateTime? statementDate,
    string currency,
    string statementMessage,
    bool markOnly,
    bool showAll,
    int? OrganizationID)
  {
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Search<PX.Objects.AR.Customer.bAccountID>((object) customerID, new object[1]
    {
      (object) customerID
    }));
    ((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Current = customer;
    PX.Objects.AR.ARSetup arSetup = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    ARStatementCycle.PK.Find((PXGraph) this, customer.StatementCycleId);
    bool flag = !string.IsNullOrEmpty(currency);
    ICollection<ARStatementKey> arStatementKeys = (ICollection<ARStatementKey>) new HashSet<ARStatementKey>();
    foreach (ARStatement arStatement in GraphHelper.RowCast<ARStatement>((IEnumerable) ((PXSelectBase<ARStatement>) this.StatementMC).Select(new object[4]
    {
      (object) customerID,
      (object) statementDate,
      (object) currency,
      (object) currency
    })).Where<ARStatement>((Func<ARStatement, bool>) (statement =>
    {
      if (!(arSetup?.PrepareStatements == "A"))
      {
        if (arSetup?.PrepareStatements == "B")
        {
          int? branchId = statement.BranchID;
          int? nullable = branchID;
          if (branchId.GetValueOrDefault() == nullable.GetValueOrDefault() & branchId.HasValue == nullable.HasValue)
            goto label_6;
        }
        if (!(arSetup?.PrepareStatements == "C"))
          return false;
        int? organizationId = ((PXAccess.Organization) PXAccess.GetBranch(statement.BranchID).Organization).OrganizationID;
        int? nullable1 = OrganizationID;
        return organizationId.GetValueOrDefault() == nullable1.GetValueOrDefault() & organizationId.HasValue == nullable1.HasValue;
      }
label_6:
      return true;
    })))
    {
      if (markOnly)
      {
        arStatement.DontEmail = new bool?(true);
        ((PXSelectBase) this.StatementMC).Cache.Update((object) arStatement);
      }
      else
      {
        if (arStatement.IsParentCustomerStatement)
        {
          Dictionary<string, string> parameters = new Dictionary<string, string>();
          parameters["BranchID"] = branchCD;
          if (!showAll)
            parameters[ARStatementReportParams.Fields.SendStatementsByEmail] = "true";
          if (flag)
            parameters[ARStatementReportParams.Fields.CuryID] = arStatement.CuryID;
          if (statementMessage != null)
            parameters["StatementMessage"] = statementMessage;
          ((PXSelectBase<ARStatement>) this.StatementMC).Current = arStatement;
          int branchID1 = arSetup?.PrepareStatements == "B" ? arStatement.BranchID.Value : int.MaxValue;
          string currencyID = flag ? arStatement.CuryID : string.Empty;
          int customerID1 = arStatement.StatementCustomerID.Value;
          DateTime? statementDate1 = arStatement.StatementDate;
          DateTime statementDate2 = statementDate1.Value;
          ARStatementKey arStatementKey = new ARStatementKey(branchID1, currencyID, customerID1, statementDate2);
          if (!arStatementKeys.Contains(arStatementKey))
          {
            parameters[ARStatementReportParams.Fields.StatementCycleID] = arStatement.StatementCycleId;
            Dictionary<string, string> dictionary = parameters;
            string statementDate3 = ARStatementReportParams.Fields.StatementDate;
            statementDate1 = arStatement.StatementDate;
            string str = statementDate1.Value.ToString("d", (IFormatProvider) CultureInfo.InvariantCulture);
            dictionary[statementDate3] = str;
            parameters[ARStatementReportParams.Fields.CustomerID] = arStatement.CustomerID.ToString();
            parameters["StatementCustomerId"] = (string) null;
            string notifications = flag ? "STATEMENTMC" : "STATEMENT";
            ((PXGraph) this).GetExtension<ARStatementUpdate_ActivityDetailsExt>().SendNotification<ARStatement>("Customer", notifications, arStatement.BranchID, (IDictionary<string, string>) parameters, true);
            arStatementKeys.Add(arStatementKey);
          }
        }
        arStatement.Emailed = new bool?(true);
        ((PXSelectBase) this.StatementMC).Cache.Update((object) arStatement);
      }
    }
    ((PXAction) this.Save).Press();
  }
}
