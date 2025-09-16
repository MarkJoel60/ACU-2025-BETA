// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARStatementPrint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CR.Standalone;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARStatementPrint : PXGraph<
#nullable disable
ARStatementDetails>
{
  public PXCancel<ARStatementPrint.PrintParameters> Cancel;
  public PXAction<ARStatementPrint.PrintParameters> prevStatementDate;
  public PXAction<ARStatementPrint.PrintParameters> nextStatementDate;
  public PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<ARStatement.customerID>>>> dummyBaccountView;
  public PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<ARStatement.customerID>>>> dummyCustomerView;
  public PXSelect<PX.Objects.AP.Vendor> dummyVendorView;
  public PXSelect<EPEmployee> dummyEmployeeView;
  [PXViewName("AR Statement")]
  public PXSelect<ARStatement> Statement;
  [PXViewName("Customer")]
  public PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<ARStatement.customerID>>>> Customer;
  public PXFilter<ARStatementPrint.PrintParameters> Filter;
  [PXFilterable(new System.Type[] {})]
  [PXVirtualDAC]
  public PXFilteredProcessingOrderBy<ARStatementPrint.DetailsResult, ARStatementPrint.PrintParameters, OrderBy<Asc<ARStatementPrint.DetailsResult.customerID>>> Details;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public PXAction<ARStatementPrint.PrintParameters> InquiriesFolder;
  public PXAction<ARStatementPrint.PrintParameters> ViewDetails;
  [PXViewName("Billing Contact")]
  public PXSelectJoin<ARStatementPrint.ContactR, InnerJoin<PX.Objects.AR.Customer, On<ARStatementPrint.ContactR.contactID, Equal<PX.Objects.AR.Customer.defBillContactID>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<ARStatement.customerID>>>> CustomerDefaultBillingContact;
  [PXViewName("Main Contact")]
  public PXSelect<PX.Objects.CR.Contact> DefaultCompanyContact;

  [PXSuppressActionValidation]
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Customer", IsReadOnly = true)]
  [PXSelector(typeof (Search<PX.Objects.AR.Customer.bAccountID, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<ARStatement.customerID>>>>), SubstituteKey = typeof (PX.Objects.AR.Customer.acctCD), DescriptionField = typeof (PX.Objects.AR.Customer.acctCD), ValidateValue = false)]
  public virtual void ARStatement_CustomerID_CacheAttached()
  {
  }

  public ARStatementPrint()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    ((PXSelectBase) this.Details).Cache.AllowDelete = false;
    ((PXSelectBase) this.Details).Cache.AllowInsert = false;
    ((PXProcessingBase<ARStatementPrint.DetailsResult>) this.Details).SetSelected<ARInvoice.selected>();
    ((PXProcessing<ARStatementPrint.DetailsResult>) this.Details).SetProcessCaption("Process");
    ((PXProcessing<ARStatementPrint.DetailsResult>) this.Details).SetProcessAllCaption("Process All");
    ((PXAction) this.InquiriesFolder).MenuAutoOpen = true;
    ((PXAction) this.InquiriesFolder).AddMenuAction((PXAction) this.ViewDetails);
  }

  [PXUIField]
  [PXPreviousButton]
  public virtual IEnumerable PrevStatementDate(PXAdapter adapter)
  {
    ARStatementPrint.PrintParameters current = ((PXSelectBase<ARStatementPrint.PrintParameters>) this.Filter).Current;
    if (current != null && !string.IsNullOrEmpty(current.StatementCycleId))
    {
      ARStatement arStatement = PXResultset<ARStatement>.op_Implicit(PXSelectBase<ARStatement, PXSelect<ARStatement, Where<ARStatement.statementCycleId, Equal<Required<ARStatement.statementCycleId>>, And<Where<ARStatement.statementDate, Less<Required<ARStatement.statementDate>>, Or<Required<ARStatement.statementDate>, IsNull>>>>, OrderBy<Desc<ARStatement.statementDate>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
      {
        (object) current.StatementCycleId,
        (object) current.StatementDate,
        (object) current.StatementDate
      }));
      if (arStatement != null)
        current.StatementDate = arStatement.StatementDate;
    }
    ((PXSelectBase) this.Details).Cache.Clear();
    return adapter.Get();
  }

  [PXUIField]
  [PXNextButton]
  public virtual IEnumerable NextStatementDate(PXAdapter adapter)
  {
    ARStatementPrint.PrintParameters current = ((PXSelectBase<ARStatementPrint.PrintParameters>) this.Filter).Current;
    if (current != null && !string.IsNullOrEmpty(current.StatementCycleId))
    {
      ARStatement arStatement = PXResultset<ARStatement>.op_Implicit(PXSelectBase<ARStatement, PXSelect<ARStatement, Where<ARStatement.statementCycleId, Equal<Required<ARStatement.statementCycleId>>, And<Where<ARStatement.statementDate, Greater<Required<ARStatement.statementDate>>, Or<Required<ARStatement.statementDate>, IsNull>>>>, OrderBy<Asc<ARStatement.statementDate>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
      {
        (object) current.StatementCycleId,
        (object) current.StatementDate,
        (object) current.StatementDate
      }));
      if (arStatement != null)
        current.StatementDate = arStatement.StatementDate;
    }
    ((PXSelectBase) this.Details).Cache.Clear();
    return adapter.Get();
  }

  protected virtual IEnumerable details()
  {
    ARStatementPrint graph = this;
    PX.Objects.AR.ARSetup current1 = ((PXSelectBase<PX.Objects.AR.ARSetup>) graph.ARSetup).Current;
    ARStatementPrint.PrintParameters current2 = ((PXSelectBase<ARStatementPrint.PrintParameters>) graph.Filter).Current;
    if (current2 != null && ARStatementCycle.PK.Find((PXGraph) graph, current2.StatementCycleId) != null)
    {
      List<ARStatementPrint.DetailsResult> source = new List<ARStatementPrint.DetailsResult>();
      Company company = PXResultset<Company>.op_Implicit(PXSelectBase<Company, PXSelect<Company>.Config>.Select((PXGraph) graph, Array.Empty<object>()));
      foreach (PXResult<ARStatement, PX.Objects.AR.Customer> pxResult in PXSelectBase<ARStatement, PXSelectJoin<ARStatement, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<ARStatement.statementCustomerID>>>, Where<ARStatement.statementDate, Equal<Required<ARStatement.statementDate>>, And<ARStatement.statementCycleId, Equal<Required<ARStatement.statementCycleId>>, And<Match<PX.Objects.AR.Customer, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>, OrderBy<Asc<ARStatement.statementCustomerID, Asc<ARStatement.curyID>>>>.Config>.Select((PXGraph) graph, new object[2]
      {
        (object) current2.StatementDate,
        (object) current2.StatementCycleId
      }))
      {
        ARStatement statement = PXResult<ARStatement, PX.Objects.AR.Customer>.op_Implicit(pxResult);
        PX.Objects.AR.Customer customer = PXResult<ARStatement, PX.Objects.AR.Customer>.op_Implicit(pxResult);
        ARStatementPrint.DetailsResult detailsResult = graph.CreateDetailsResult(statement, customer);
        int? nullable1;
        int? nullable2;
        int num1;
        if (true)
        {
          if (current1.PrepareStatements == "B")
          {
            nullable1 = statement.BranchID;
            nullable2 = current2.BranchID;
            num1 = !(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) ? 1 : 0;
          }
          else
            num1 = 0;
        }
        else
          num1 = 1;
        int num2;
        if (num1 == 0)
        {
          if (current1.PrepareStatements == "C")
          {
            nullable2 = ((PXAccess.Organization) PXAccess.GetBranch(statement.BranchID).Organization).OrganizationID;
            nullable1 = current2.OrganizationID;
            num2 = !(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue) ? 1 : 0;
          }
          else
            num2 = 0;
        }
        else
          num2 = 1;
        bool? nullable3;
        int num3;
        if (num2 == 0)
        {
          nullable1 = ((PXSelectBase<ARStatementPrint.PrintParameters>) graph.Filter).Current.Action;
          int num4 = 0;
          if (nullable1.GetValueOrDefault() == num4 & nullable1.HasValue)
          {
            nullable3 = current2.ShowAll;
            if (!nullable3.GetValueOrDefault())
            {
              nullable3 = statement.DontPrint;
              if (!nullable3.GetValueOrDefault())
              {
                nullable3 = statement.Printed;
                num3 = nullable3.GetValueOrDefault() ? 1 : 0;
                goto label_21;
              }
              num3 = 1;
              goto label_21;
            }
          }
          num3 = 0;
        }
        else
          num3 = 1;
label_21:
        int num5;
        if (num3 == 0)
        {
          nullable1 = ((PXSelectBase<ARStatementPrint.PrintParameters>) graph.Filter).Current.Action;
          if (nullable1.GetValueOrDefault() != 1)
          {
            nullable1 = ((PXSelectBase<ARStatementPrint.PrintParameters>) graph.Filter).Current.Action;
            if (nullable1.GetValueOrDefault() != 2)
              goto label_28;
          }
          nullable3 = current2.ShowAll;
          if (!nullable3.GetValueOrDefault())
          {
            nullable3 = statement.DontEmail;
            if (!nullable3.GetValueOrDefault())
            {
              nullable3 = statement.Emailed;
              num5 = nullable3.GetValueOrDefault() ? 1 : 0;
              goto label_30;
            }
            num5 = 1;
            goto label_30;
          }
label_28:
          num5 = 0;
        }
        else
          num5 = 1;
label_30:
        int num6;
        if (num5 == 0)
        {
          nullable3 = customer.PrintCuryStatements;
          if (nullable3.GetValueOrDefault())
          {
            nullable3 = ((PXSelectBase<ARStatementPrint.PrintParameters>) graph.Filter).Current.CuryStatements;
            num6 = !nullable3.GetValueOrDefault() ? 1 : 0;
          }
          else
            num6 = 0;
        }
        else
          num6 = 1;
        int num7;
        if (num6 == 0)
        {
          nullable3 = customer.PrintCuryStatements;
          if (!nullable3.GetValueOrDefault())
          {
            nullable3 = ((PXSelectBase<ARStatementPrint.PrintParameters>) graph.Filter).Current.CuryStatements;
            num7 = nullable3.GetValueOrDefault() ? 1 : 0;
          }
          else
            num7 = 0;
        }
        else
          num7 = 1;
        if (num7 == 0)
        {
          nullable3 = customer.PrintCuryStatements;
          if (nullable3.GetValueOrDefault())
          {
            ARStatementPrint.DetailsResult destination = source.LastOrDefault<ARStatementPrint.DetailsResult>();
            nullable1 = (int?) destination?.CustomerID;
            nullable2 = detailsResult.CustomerID;
            if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && destination?.CuryID == detailsResult.CuryID)
              graph.AggregateDetailsResult(destination, detailsResult);
            else
              source.Add(detailsResult);
          }
          else
          {
            string baseCurrencyID = string.Empty;
            if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
            {
              if (current1.PrepareStatements == "B")
                baseCurrencyID = PXOrgAccess.GetBaseCuryID(((PXSelectBase<ARStatementPrint.PrintParameters>) graph.Filter).Current.BranchCD);
              else if (current1.PrepareStatements == "C")
                baseCurrencyID = PXOrgAccess.GetBaseCuryID(PXAccess.GetOrganizationCD(((PXSelectBase<ARStatementPrint.PrintParameters>) graph.Filter).Current.OrganizationID));
            }
            else
              baseCurrencyID = company.BaseCuryID;
            graph.ResetDetailsResultToBaseCurrency(detailsResult, baseCurrencyID);
            ARStatementPrint.DetailsResult destination = source.LastOrDefault<ARStatementPrint.DetailsResult>();
            nullable2 = (int?) destination?.CustomerID;
            nullable1 = detailsResult.CustomerID;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            {
              graph.AggregateDetailsResult(destination, detailsResult);
              graph.AggregateDetailsFlagsResult(destination, detailsResult);
            }
            else
              source.Add(detailsResult);
          }
        }
      }
      foreach (ARStatementPrint.DetailsResult detailsResult in source)
      {
        object obj = ((PXSelectBase) graph.Details).Cache.Locate((object) detailsResult);
        if (obj != null)
        {
          yield return obj;
        }
        else
        {
          ((PXSelectBase) graph.Details).Cache.SetStatus((object) detailsResult, (PXEntryStatus) 5);
          yield return (object) detailsResult;
        }
      }
      ((PXSelectBase) graph.Details).Cache.IsDirty = false;
    }
  }

  protected virtual IEnumerable defaultCompanyContact()
  {
    return (IEnumerable) OrganizationMaint.GetDefaultContactForCurrentOrganization((PXGraph) this);
  }

  public virtual bool IsDirty => false;

  protected virtual void PrintParameters_Action_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.SetStatementDateFromCycle((ARStatementPrint.PrintParameters) e.Row);
  }

  protected virtual void PrintParameters_StatementCycleId_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.SetStatementDateFromCycle((ARStatementPrint.PrintParameters) e.Row);
  }

  private void SetStatementDateFromCycle(ARStatementPrint.PrintParameters filter)
  {
    if (string.IsNullOrEmpty(filter.StatementCycleId))
      return;
    ARStatementCycle arStatementCycle = ARStatementCycle.PK.Find((PXGraph) this, filter.StatementCycleId);
    if (arStatementCycle == null)
      return;
    filter.StatementDate = arStatementCycle.LastStmtDate;
  }

  protected virtual void PrintParameters_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARStatementPrint.\u003C\u003Ec__DisplayClass30_0 cDisplayClass300 = new ARStatementPrint.\u003C\u003Ec__DisplayClass30_0();
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    bool flag1 = current.PrepareStatements == "B";
    bool flag2 = current.PrepareStatements == "C";
    ARStatementPrint.PrintParameters row = (ARStatementPrint.PrintParameters) e.Row;
    if (row != null)
    {
      row.BranchCD = (string) null;
      if (!flag1)
        row.BranchID = new int?();
      int? nullable1 = row.BranchID;
      if (nullable1.HasValue)
        row.BranchCD = PXAccess.GetBranchCD(row.BranchID);
      PXUIFieldAttribute.SetVisible<ARStatementPrint.PrintParameters.branchID>(sender, (object) null, flag1);
      PXUIFieldAttribute.SetVisible<ARStatementPrint.PrintParameters.organizationID>(sender, (object) null, flag2);
      PXCache pxCache1 = sender;
      ARStatementPrint.PrintParameters printParameters1 = row;
      nullable1 = row.Action;
      int num1 = nullable1.GetValueOrDefault() != 4 ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<ARStatementPrint.PrintParameters.statementDate>(pxCache1, (object) printParameters1, num1 != 0);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass300.filter = ((PXSelectBase) this.Filter).Cache.CreateCopy((object) row) as ARStatementPrint.PrintParameters;
      nullable1 = row.Action;
      if (nullable1.HasValue)
      {
        switch (nullable1.GetValueOrDefault())
        {
          case 0:
            // ISSUE: reference to a compiler-generated method
            ((PXProcessingBase<ARStatementPrint.DetailsResult>) this.Details).SetAsyncProcessDelegate(new Func<List<ARStatementPrint.DetailsResult>, CancellationToken, System.Threading.Tasks.Task>(cDisplayClass300.\u003CPrintParameters_RowSelected\u003Eb__0));
            break;
          case 1:
            // ISSUE: method pointer
            ((PXProcessingBase<ARStatementPrint.DetailsResult>) this.Details).SetProcessDelegate(new PXProcessingBase<ARStatementPrint.DetailsResult>.ProcessListDelegate((object) cDisplayClass300, __methodptr(\u003CPrintParameters_RowSelected\u003Eb__1)));
            break;
          case 2:
            // ISSUE: method pointer
            ((PXProcessingBase<ARStatementPrint.DetailsResult>) this.Details).SetProcessDelegate(new PXProcessingBase<ARStatementPrint.DetailsResult>.ProcessListDelegate((object) cDisplayClass300, __methodptr(\u003CPrintParameters_RowSelected\u003Eb__2)));
            break;
          case 3:
            // ISSUE: method pointer
            ((PXProcessingBase<ARStatementPrint.DetailsResult>) this.Details).SetProcessDelegate(new PXProcessingBase<ARStatementPrint.DetailsResult>.ProcessListDelegate((object) cDisplayClass300, __methodptr(\u003CPrintParameters_RowSelected\u003Eb__3)));
            break;
          case 4:
            // ISSUE: method pointer
            ((PXProcessingBase<ARStatementPrint.DetailsResult>) this.Details).SetProcessDelegate(new PXProcessingBase<ARStatementPrint.DetailsResult>.ProcessListDelegate((object) cDisplayClass300, __methodptr(\u003CPrintParameters_RowSelected\u003Eb__4)));
            break;
          default:
            goto label_14;
        }
        Guid? nullable2;
        bool? nullable3;
        if (row != null)
        {
          nullable2 = row.PrinterID;
          if (!nullable2.HasValue && PXAccess.FeatureInstalled<FeaturesSet.deviceHub>())
          {
            ARStatementPrint.PrintParameters printParameters2 = row;
            NotificationUtility notificationUtility = new NotificationUtility((PXGraph) this);
            nullable3 = ((PXSelectBase<ARStatementPrint.PrintParameters>) this.Filter).Current.CuryStatements;
            string reportID = nullable3.GetValueOrDefault() ? "AR642000" : "AR641500";
            int? branchId = ((PXGraph) this).Accessinfo.BranchID;
            Guid? nullable4 = notificationUtility.SearchPrinter("Customer", reportID, branchId);
            printParameters2.PrinterID = nullable4;
          }
        }
        bool flag3 = this.IsPrintingAllowed(row);
        PXUIFieldAttribute.SetVisible<ARStatementPrint.PrintParameters.printWithDeviceHub>(sender, (object) row, flag3);
        PXUIFieldAttribute.SetVisible<ARStatementPrint.PrintParameters.definePrinterManually>(sender, (object) row, flag3);
        PXUIFieldAttribute.SetVisible<ARStatementPrint.PrintParameters.printerID>(sender, (object) row, flag3);
        PXUIFieldAttribute.SetVisible<ARStatementPrint.PrintParameters.numberOfCopies>(sender, (object) row, flag3);
        if (PXContext.GetSlot<AUSchedule>() == null)
        {
          PXCache pxCache2 = sender;
          ARStatementPrint.PrintParameters printParameters3 = row;
          nullable3 = row.PrintWithDeviceHub;
          int num2 = nullable3.GetValueOrDefault() ? 1 : 0;
          PXUIFieldAttribute.SetEnabled<ARStatementPrint.PrintParameters.definePrinterManually>(pxCache2, (object) printParameters3, num2 != 0);
          PXCache pxCache3 = sender;
          ARStatementPrint.PrintParameters printParameters4 = row;
          nullable3 = row.PrintWithDeviceHub;
          int num3 = nullable3.GetValueOrDefault() ? 1 : 0;
          PXUIFieldAttribute.SetEnabled<ARStatementPrint.PrintParameters.numberOfCopies>(pxCache3, (object) printParameters4, num3 != 0);
          PXCache pxCache4 = sender;
          ARStatementPrint.PrintParameters printParameters5 = row;
          nullable3 = row.PrintWithDeviceHub;
          int num4;
          if (nullable3.GetValueOrDefault())
          {
            nullable3 = row.DefinePrinterManually;
            num4 = nullable3.GetValueOrDefault() ? 1 : 0;
          }
          else
            num4 = 0;
          PXUIFieldAttribute.SetEnabled<ARStatementPrint.PrintParameters.printerID>(pxCache4, (object) printParameters5, num4 != 0);
        }
        nullable3 = row.PrintWithDeviceHub;
        if (nullable3.GetValueOrDefault())
        {
          nullable3 = row.DefinePrinterManually;
          if (nullable3.GetValueOrDefault())
            return;
        }
        ARStatementPrint.PrintParameters printParameters6 = row;
        nullable2 = new Guid?();
        Guid? nullable5 = nullable2;
        printParameters6.PrinterID = nullable5;
        return;
      }
label_14:
      throw new PXException("Incorrect action has been specified.");
    }
  }

  protected virtual bool IsPrintingAllowed(ARStatementPrint.PrintParameters row)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() || row == null)
      return false;
    int? action = row.Action;
    int num = 0;
    return action.GetValueOrDefault() == num & action.HasValue;
  }

  protected virtual void PrintParameters_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase) this.Details).Cache.Clear();
    if (sender.ObjectsEqual<ARStatementPrint.PrintParameters.action>(e.Row, e.OldRow) && sender.ObjectsEqual<ARStatementPrint.PrintParameters.definePrinterManually>(e.Row, e.OldRow) && sender.ObjectsEqual<ARStatementPrint.PrintParameters.printWithDeviceHub>(e.Row, e.OldRow) || ((PXSelectBase<ARStatementPrint.PrintParameters>) this.Filter).Current == null || !PXAccess.FeatureInstalled<FeaturesSet.deviceHub>())
      return;
    bool? nullable1 = ((PXSelectBase<ARStatementPrint.PrintParameters>) this.Filter).Current.PrintWithDeviceHub;
    if (!nullable1.GetValueOrDefault())
      return;
    nullable1 = ((PXSelectBase<ARStatementPrint.PrintParameters>) this.Filter).Current.DefinePrinterManually;
    if (!nullable1.GetValueOrDefault())
      return;
    if (PXContext.GetSlot<AUSchedule>() != null)
    {
      Guid? printerId = ((PXSelectBase<ARStatementPrint.PrintParameters>) this.Filter).Current.PrinterID;
      if (printerId.HasValue)
      {
        printerId = ((ARStatementPrint.PrintParameters) e.OldRow).PrinterID;
        if (!printerId.HasValue)
          return;
      }
    }
    ARStatementPrint.PrintParameters current = ((PXSelectBase<ARStatementPrint.PrintParameters>) this.Filter).Current;
    NotificationUtility notificationUtility = new NotificationUtility((PXGraph) this);
    nullable1 = ((PXSelectBase<ARStatementPrint.PrintParameters>) this.Filter).Current.CuryStatements;
    string reportID = nullable1.GetValueOrDefault() ? "AR642000" : "AR641500";
    int? branchId = ((PXGraph) this).Accessinfo.BranchID;
    Guid? nullable2 = notificationUtility.SearchPrinter("Customer", reportID, branchId);
    current.PrinterID = nullable2;
  }

  protected virtual void DetailsResult_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PrintParameters_PrinterName_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ARStatementPrint.PrintParameters row = (ARStatementPrint.PrintParameters) e.Row;
    if (row == null || this.IsPrintingAllowed(row))
      return;
    e.NewValue = (object) null;
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable inquiriesFolder(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton(VisibleOnProcessingResults = true)]
  public virtual IEnumerable viewDetails(PXAdapter adapter)
  {
    if (((PXSelectBase<ARStatementPrint.DetailsResult>) this.Details).Current != null && ((PXSelectBase<ARStatementPrint.PrintParameters>) this.Filter).Current != null)
    {
      ARStatementPrint.DetailsResult current1 = ((PXSelectBase<ARStatementPrint.DetailsResult>) this.Details).Current;
      ARStatementForCustomer instance = PXGraph.CreateInstance<ARStatementForCustomer>();
      ARStatementForCustomer.ARStatementForCustomerParameters current2 = ((PXSelectBase<ARStatementForCustomer.ARStatementForCustomerParameters>) instance.Filter).Current;
      current2.CustomerID = current1.CustomerID;
      ((PXSelectBase<ARStatementForCustomer.ARStatementForCustomerParameters>) instance.Filter).Update(current2);
      ((PXSelectBase<ARStatementForCustomer.ARStatementForCustomerParameters>) instance.Filter).Select(Array.Empty<object>());
      throw new PXRedirectRequiredException((PXGraph) instance, "Statement History");
    }
    return (IEnumerable) ((PXSelectBase<ARStatementPrint.PrintParameters>) this.Filter).Select(Array.Empty<object>());
  }

  protected static void Export(
    Dictionary<string, string> aRes,
    ARStatementPrint.PrintParameters aSrc)
  {
    aRes[ARStatementReportParams.Fields.StatementCycleID] = aSrc.StatementCycleId;
    aRes[ARStatementReportParams.Fields.StatementDate] = aSrc.StatementDate.Value.ToString("d", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  protected static void Export(
    Dictionary<string, string> aRes,
    int index,
    ARStatementPrint.PrintParameters aSrc)
  {
    aRes[ARStatementReportParams.Fields.StatementCycleID + index.ToString()] = aSrc.StatementCycleId;
    aRes[ARStatementReportParams.Fields.StatementDate + index.ToString()] = aSrc.StatementDate.Value.ToString("d", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  protected virtual ARStatementPrint.DetailsResult CreateDetailsResult(
    ARStatement statement,
    PX.Objects.AR.Customer customer)
  {
    return new ARStatementPrint.DetailsResult()
    {
      CustomerID = customer.BAccountID,
      UseCurrency = customer.PrintCuryStatements,
      StatementBalance = new Decimal?(statement.EndBalance.GetValueOrDefault()),
      AgeBalance00 = new Decimal?(statement.AgeBalance00.GetValueOrDefault()),
      CuryID = statement.CuryID,
      CuryStatementBalance = new Decimal?(statement.CuryEndBalance.GetValueOrDefault()),
      CuryAgeBalance00 = new Decimal?(statement.CuryAgeBalance00.GetValueOrDefault()),
      DontEmail = statement.DontEmail,
      DontPrint = statement.DontPrint,
      Emailed = statement.Emailed,
      Printed = statement.Printed,
      BranchID = statement.BranchID,
      NoteID = statement.NoteID
    };
  }

  protected virtual void AggregateDetailsResult(
    ARStatementPrint.DetailsResult destination,
    ARStatementPrint.DetailsResult source)
  {
    ARStatementPrint.DetailsResult detailsResult1 = destination;
    Decimal? nullable1 = detailsResult1.StatementBalance;
    Decimal? statementBalance = source.StatementBalance;
    detailsResult1.StatementBalance = nullable1.HasValue & statementBalance.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + statementBalance.GetValueOrDefault()) : new Decimal?();
    ARStatementPrint.DetailsResult detailsResult2 = destination;
    Decimal? nullable2 = detailsResult2.AgeBalance00;
    nullable1 = source.AgeBalance00;
    detailsResult2.AgeBalance00 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    if (destination.CuryID == source.CuryID)
    {
      ARStatementPrint.DetailsResult detailsResult3 = destination;
      nullable1 = detailsResult3.CuryStatementBalance;
      nullable2 = source.CuryStatementBalance;
      detailsResult3.CuryStatementBalance = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      ARStatementPrint.DetailsResult detailsResult4 = destination;
      nullable2 = detailsResult4.CuryAgeBalance00;
      nullable1 = source.CuryAgeBalance00;
      detailsResult4.CuryAgeBalance00 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      destination.CuryStatementBalance = new Decimal?(0M);
      destination.CuryAgeBalance00 = new Decimal?(0M);
    }
  }

  protected virtual void AggregateDetailsFlagsResult(
    ARStatementPrint.DetailsResult destination,
    ARStatementPrint.DetailsResult source)
  {
    ARStatementPrint.DetailsResult detailsResult1 = destination;
    bool? nullable1;
    int num1;
    if (destination.DontEmail.GetValueOrDefault())
    {
      nullable1 = source.DontEmail;
      num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool? nullable2 = new bool?(num1 != 0);
    detailsResult1.DontEmail = nullable2;
    ARStatementPrint.DetailsResult detailsResult2 = destination;
    nullable1 = destination.DontPrint;
    int num2;
    if (nullable1.GetValueOrDefault())
    {
      nullable1 = source.DontPrint;
      num2 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    bool? nullable3 = new bool?(num2 != 0);
    detailsResult2.DontPrint = nullable3;
  }

  protected virtual void ResetDetailsResultToBaseCurrency(
    ARStatementPrint.DetailsResult detailsResult,
    string baseCurrencyID)
  {
    detailsResult.CuryID = baseCurrencyID;
    detailsResult.CuryStatementBalance = detailsResult.StatementBalance;
    detailsResult.CuryAgeBalance00 = detailsResult.AgeBalance00;
  }

  public static void MarkAsDoNotPrint(
    ARStatementPrint.PrintParameters filter,
    IEnumerable<ARStatementPrint.DetailsResult> list)
  {
    if (filter.ShowAll.GetValueOrDefault())
      return;
    PXGraph graph = new PXGraph();
    ARStatementPrint.IStatementsSelector statementSelector = ARStatementPrint.GetStatementSelector(graph, filter);
    foreach (ARStatementPrint.DetailsResult detailsResult in list)
    {
      foreach (ARStatement statement in statementSelector.Select(detailsResult))
      {
        statement.DontPrint = new bool?(true);
        statementSelector.Update(statement);
      }
    }
    graph.Actions.PressSave();
  }

  public static async System.Threading.Tasks.Task PrintStatements(
    ARStatementPrint.PrintParameters filter,
    IEnumerable<ARStatementPrint.DetailsResult> list,
    CancellationToken cancellationToken)
  {
    PXGraph graph = PXGraph.CreateInstance<PXGraph>();
    ARStatementCycle.PK.Find(graph, filter.StatementCycleId);
    PX.Objects.AR.ARSetup arSetup = PXResultset<PX.Objects.AR.ARSetup>.op_Implicit(PXSetup<PX.Objects.AR.ARSetup>.Select(graph, Array.Empty<object>()));
    bool? nullable = filter.CuryStatements;
    string reportID = nullable.GetValueOrDefault() ? "AR642000" : "AR641500";
    PXReportRequiredException reportRequiredException = (PXReportRequiredException) null;
    ARStatementPrint.IStatementsSelector statementsSelector = ARStatementPrint.GetStatementSelector(graph, filter);
    foreach (ARStatementPrint.DetailsResult detailsResult in list)
    {
      Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
      Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
      if (arSetup?.PrepareStatements == "B")
      {
        dictionary1["BranchID"] = PXAccess.GetBranchCD(filter.BranchID);
        dictionary2["BranchID"] = PXAccess.GetBranchCD(filter.BranchID);
      }
      if (arSetup?.PrepareStatements == "C")
      {
        dictionary1["OrganizationID"] = PXAccess.GetOrganizationCD(filter.OrganizationID);
        dictionary2["OrganizationID"] = PXAccess.GetOrganizationCD(filter.OrganizationID);
      }
      dictionary1["StatementMessage"] = filter.StatementMessage;
      dictionary1["StatementCycleId"] = filter.StatementCycleId;
      dictionary1["StatementDate"] = filter.StatementDate.Value.ToString("d", (IFormatProvider) CultureInfo.InvariantCulture);
      dictionary1[ARStatementReportParams.Fields.CustomerID] = detailsResult.CustomerID.ToString();
      dictionary2["StatementMessage"] = filter.StatementMessage;
      dictionary2["StatementCycleId"] = filter.StatementCycleId;
      dictionary2["StatementDate"] = filter.StatementDate.Value.ToString("d", (IFormatProvider) CultureInfo.InvariantCulture);
      PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXViewOf<PX.Objects.AR.Customer>.BasedOn<SelectFromBase<PX.Objects.AR.Customer, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.AR.Customer.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(graph, new object[1]
      {
        (object) detailsResult.CustomerID
      }));
      dictionary2["StatementCustomerId"] = customer.AcctCD;
      nullable = filter.ShowAll;
      if (!nullable.GetValueOrDefault())
      {
        dictionary1[ARStatementReportParams.Fields.PrintStatements] = "true";
        dictionary2[ARStatementReportParams.Fields.PrintStatements] = "true";
      }
      nullable = filter.CuryStatements;
      if (nullable.GetValueOrDefault())
      {
        dictionary1[ARStatementReportParams.Fields.CuryID] = detailsResult.CuryID;
        dictionary2[ARStatementReportParams.Fields.CuryID] = detailsResult.CuryID;
      }
      foreach (ARStatement statement in statementsSelector.Select(detailsResult))
      {
        nullable = statement.Printed;
        if (!nullable.GetValueOrDefault())
        {
          statement.Printed = new bool?(true);
          statementsSelector.Update(statement);
        }
      }
      string customerReportId = ARStatementPrint.GetCustomerReportID(graph, reportID, detailsResult);
      reportRequiredException = PXReportRequiredException.CombineReport(reportRequiredException, customerReportId, dictionary1, (CurrentLocalization) null);
      if (PXAccess.FeatureInstalled<FeaturesSet.deviceHub>())
      {
        int num = await SMPrintJobMaint.CreatePrintJobGroups(SMPrintJobMaint.AssignPrintJobToPrinter(new Dictionary<PrintSettings, PXReportRequiredException>(), dictionary2, (IPrintable) filter, new Func<string, string, int?, Guid?>(new NotificationUtility(graph).SearchPrinter), "Customer", reportID, customerReportId, detailsResult.BranchID, (CurrentLocalization) null), cancellationToken) ? 1 : 0;
      }
    }
    graph.Actions.PressSave();
    if (reportRequiredException != null)
    {
      reportRequiredException.Parameters["StatementCustomerId"] = (string) null;
      throw reportRequiredException;
    }
    graph = (PXGraph) null;
    arSetup = (PX.Objects.AR.ARSetup) null;
    reportID = (string) null;
    reportRequiredException = (PXReportRequiredException) null;
    statementsSelector = (ARStatementPrint.IStatementsSelector) null;
  }

  private static string GetCustomerReportID(
    PXGraph graph,
    string reportID,
    ARStatementPrint.DetailsResult statement)
  {
    return ARStatementPrint.GetCustomerReportID(graph, reportID, statement.CustomerID, statement.BranchID);
  }

  public static string GetCustomerReportID(
    PXGraph graph,
    string reportID,
    int? customerID,
    int? branchID)
  {
    return new NotificationUtility(graph).SearchCustomerReport(reportID, customerID, branchID);
  }

  public static void EmailStatements(
    ARStatementPrint.PrintParameters filter,
    List<ARStatementPrint.DetailsResult> list,
    bool markOnly)
  {
    ARStatementUpdate instance = PXGraph.CreateInstance<ARStatementUpdate>();
    int num1 = 0;
    bool flag = false;
    foreach (ARStatementPrint.DetailsResult detailsResult in list)
    {
      try
      {
        ARStatementUpdate arStatementUpdate = instance;
        int? branchId = filter.BranchID;
        string branchCd = filter.BranchCD;
        int? customerId = detailsResult.CustomerID;
        DateTime? statementDate = filter.StatementDate;
        bool? nullable = filter.CuryStatements;
        string curyId = nullable.GetValueOrDefault() ? detailsResult.CuryID : (string) null;
        string statementMessage = filter.StatementMessage;
        int num2 = markOnly ? 1 : 0;
        nullable = filter.ShowAll;
        int num3 = nullable.GetValueOrDefault() ? 1 : 0;
        int? organizationId = filter.OrganizationID;
        arStatementUpdate.EMailStatement(branchId, branchCd, customerId, statementDate, curyId, statementMessage, num2 != 0, num3 != 0, organizationId);
        if (!markOnly)
          detailsResult.Emailed = new bool?(true);
        PXProcessing<ARStatementPrint.DetailsResult>.SetCurrentItem((object) detailsResult);
        PXProcessing<ARStatementPrint.DetailsResult>.SetProcessed();
      }
      catch (Exception ex)
      {
        PXProcessing<ARStatementPrint.DetailsResult>.SetError(num1, ex);
        flag = true;
      }
      ++num1;
    }
    if (flag)
      throw new PXException("The mail send has failed.");
  }

  private static void RegenerateStatements(
    ARStatementPrint.PrintParameters pars,
    List<ARStatementPrint.DetailsResult> statements)
  {
    StatementCycleProcessBO instance = PXGraph.CreateInstance<StatementCycleProcessBO>();
    ARStatementCycle cycle = ((PXSelectBase<ARStatementCycle>) instance.CyclesList).SelectSingle(new object[1]
    {
      (object) pars.StatementCycleId
    });
    PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<ARStatementPrint.DetailsResult.customerID>>>> pxSelect = new PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<ARStatementPrint.DetailsResult.customerID>>>>((PXGraph) instance);
    int num = 0;
    List<PX.Objects.AR.Customer> source = new List<PX.Objects.AR.Customer>();
    foreach (ARStatementPrint.DetailsResult statement in statements)
    {
      if (ARStatementProcess.CheckForUnprocessedPPD((PXGraph) instance, pars.StatementCycleId, pars.StatementDate, statement.CustomerID))
      {
        PXProcessing<ARStatementPrint.DetailsResult>.SetError(num, (Exception) new PXSetPropertyException("The report cannot be generated. There are documents with unprocessed cash discounts. To proceed, make sure that all documents are processed on the Generate AR Tax Adjustments (AR504500) form and appropriate VAT credit memos are released on the Release AR Documents (AR501000) form.", (PXErrorLevel) 5));
      }
      else
      {
        PX.Objects.AR.Customer customer = ((PXSelectBase<PX.Objects.AR.Customer>) pxSelect).SelectSingle(new object[1]
        {
          (object) statement.CustomerID
        });
        if (customer != null)
        {
          DateTime? statementLastDate = customer.StatementLastDate;
          DateTime? lastStmtDate = cycle.LastStmtDate;
          if ((statementLastDate.HasValue == lastStmtDate.HasValue ? (statementLastDate.HasValue ? (statementLastDate.GetValueOrDefault() != lastStmtDate.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
            PXProcessing<ARStatementPrint.DetailsResult>.SetError(num, (Exception) new PXSetPropertyException("A statement cannot be regenerated for the {0} customer on the {1:d} date because there is a statement prepared for the customer on a later date. Use the Regenerate Last Statement action on the Customers (AR303000) form to regenerate the latest statement.", new object[2]
            {
              (object) customer.AcctCD,
              (object) cycle.LastStmtDate
            }));
          else
            source.Add(customer);
        }
        ++num;
      }
    }
    if (cycle == null || !source.Any<PX.Objects.AR.Customer>())
      return;
    StatementCycleProcessBO.RegenerateStatements(instance, cycle, (IEnumerable<PX.Objects.AR.Customer>) source);
  }

  private static ARStatementPrint.IStatementsSelector GetStatementSelector(
    PXGraph graph,
    ARStatementPrint.PrintParameters filter)
  {
    switch (PXResultset<PX.Objects.AR.ARSetup>.op_Implicit(PXSetup<PX.Objects.AR.ARSetup>.Select(graph, Array.Empty<object>()))?.PrepareStatements)
    {
      case null:
        return (ARStatementPrint.IStatementsSelector) new ARStatementPrint.EmptyStatementsSelector();
      case "B":
        return (ARStatementPrint.IStatementsSelector) new ARStatementPrint.StatementsSelectorEachBranch(graph, filter);
      case "C":
        return (ARStatementPrint.IStatementsSelector) new ARStatementPrint.StatementsSelectorConsolidatedForCompany(graph, filter);
      default:
        return (ARStatementPrint.IStatementsSelector) new ARStatementPrint.StatementsSelector(graph, filter);
    }
  }

  [Serializable]
  public class PrintParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IPrintable
  {
    protected string _BranchCD;
    protected int? _BranchID;
    protected int? _Action;
    protected string _StatementCycleId;
    protected DateTime? _StatementDate;
    protected bool? _CuryStatements;
    protected bool? _ShowAll = new bool?(false);
    protected bool? _PrintWithDeviceHub;
    protected bool? _DefinePrinterManually = new bool?(false);
    protected Guid? _PrinterID;
    protected int? _NumberOfCopies;

    [Organization(true)]
    public int? OrganizationID { get; set; }

    [PXDefault("")]
    public virtual string BranchCD
    {
      get => this._BranchCD;
      set => this._BranchCD = value;
    }

    [Branch(null, null, true, true, true)]
    public virtual int? BranchID
    {
      get => this._BranchID;
      set => this._BranchID = value;
    }

    [PXDBInt]
    [PXDefault(0)]
    [PXUIField(DisplayName = "Actions")]
    [PXIntList(new int[] {0, 1, 2, 3, 4}, new string[] {"Print Statement", "Email Statement", "Mark as Do not Email", "Mark as Do not Print", "Regenerate Statement"})]
    public virtual int? Action
    {
      get => this._Action;
      set => this._Action = value;
    }

    [PXDBString(10, IsUnicode = true)]
    [PXDefault(typeof (ARStatementCycle))]
    [PXUIField]
    [PXSelector(typeof (ARStatementCycle.statementCycleId), DescriptionField = typeof (ARStatementCycle.descr))]
    public virtual string StatementCycleId
    {
      get => this._StatementCycleId;
      set => this._StatementCycleId = value;
    }

    [PXDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? StatementDate
    {
      get => this._StatementDate;
      set => this._StatementDate = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Foreign Currency Statements")]
    public virtual bool? CuryStatements
    {
      get => this._CuryStatements;
      set => this._CuryStatements = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Show All")]
    public virtual bool? ShowAll
    {
      get => this._ShowAll;
      set => this._ShowAll = value;
    }

    [PXString(IsUnicode = true)]
    [PXUIField(DisplayName = "Message")]
    public virtual string StatementMessage { get; set; }

    [PXDBBool]
    [PXDefault(typeof (FeatureInstalled<FeaturesSet.deviceHub>))]
    [PXUIField(DisplayName = "Print with DeviceHub")]
    public virtual bool? PrintWithDeviceHub
    {
      get => this._PrintWithDeviceHub;
      set => this._PrintWithDeviceHub = value;
    }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Define Printer Manually")]
    public virtual bool? DefinePrinterManually
    {
      get => this._DefinePrinterManually;
      set => this._DefinePrinterManually = value;
    }

    [PXPrinterSelector]
    public virtual Guid? PrinterID
    {
      get => this._PrinterID;
      set => this._PrinterID = value;
    }

    [PXDBInt(MinValue = 1)]
    [PXDefault(1)]
    [PXFormula(typeof (Selector<ARStatementPrint.PrintParameters.printerID, SMPrinter.defaultNumberOfCopies>))]
    [PXUIField]
    public virtual int? NumberOfCopies
    {
      get => this._NumberOfCopies;
      set => this._NumberOfCopies = value;
    }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementPrint.PrintParameters.organizationID>
    {
    }

    public abstract class branchCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARStatementPrint.PrintParameters.branchCD>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementPrint.PrintParameters.branchID>
    {
    }

    public abstract class action : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementPrint.PrintParameters.action>
    {
    }

    public abstract class statementCycleId : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARStatementPrint.PrintParameters.statementCycleId>
    {
    }

    public abstract class statementDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARStatementPrint.PrintParameters.statementDate>
    {
    }

    public abstract class curyStatements : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementPrint.PrintParameters.curyStatements>
    {
    }

    public abstract class showAll : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementPrint.PrintParameters.showAll>
    {
    }

    public abstract class statementMessage : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARStatementPrint.PrintParameters.statementMessage>
    {
    }

    public abstract class printWithDeviceHub : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementPrint.PrintParameters.printWithDeviceHub>
    {
    }

    public abstract class definePrinterManually : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementPrint.PrintParameters.definePrinterManually>
    {
    }

    public abstract class printerID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      ARStatementPrint.PrintParameters.printerID>
    {
    }

    public abstract class numberOfCopies : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementPrint.PrintParameters.numberOfCopies>
    {
    }

    public class Actions
    {
      public const int Print = 0;
      public const int Email = 1;
      public const int MarkDontEmail = 2;
      public const int MarkDontPrint = 3;
      public const int Regenerate = 4;
    }
  }

  [Serializable]
  public class DetailsResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected bool? _Selected = new bool?(false);
    protected int? _CustomerID;
    protected string _CuryID;
    protected Decimal? _StatementBalance;
    protected Decimal? _CuryStatementBalance;
    protected bool? _UseCurrency;
    protected Decimal? _AgeBalance00;
    protected Decimal? _CuryAgeBalance00;
    protected bool? _DontPrint;
    protected bool? _Printed;
    protected bool? _DontEmail;
    protected bool? _Emailed;
    protected int? _BranchID;

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected
    {
      get => this._Selected;
      set => this._Selected = value;
    }

    [PX.Objects.AR.Customer(DescriptionField = typeof (PX.Objects.AR.Customer.acctName), IsKey = true, DisplayName = "Customer")]
    public virtual int? CustomerID
    {
      get => this._CustomerID;
      set => this._CustomerID = value;
    }

    [PXDBString(5, IsUnicode = true, IsKey = true)]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
    [PXUIField(DisplayName = "Currency")]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    [PXDBBaseCury(null, null)]
    [PXUIField(DisplayName = "Statement Balance")]
    public virtual Decimal? StatementBalance
    {
      get => this._StatementBalance;
      set => this._StatementBalance = value;
    }

    [PXCury(typeof (ARStatementPrint.DetailsResult.curyID))]
    [PXUIField(DisplayName = "FC Statement Balance")]
    public virtual Decimal? CuryStatementBalance
    {
      get => this._CuryStatementBalance;
      set => this._CuryStatementBalance = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "FC Statement")]
    public virtual bool? UseCurrency
    {
      get => this._UseCurrency;
      set => this._UseCurrency = value;
    }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Age00 Balance")]
    public virtual Decimal? AgeBalance00
    {
      get => this._AgeBalance00;
      set => this._AgeBalance00 = value;
    }

    [PXCury(typeof (ARStatementPrint.DetailsResult.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "FC Age00 Balance")]
    public virtual Decimal? CuryAgeBalance00
    {
      get => this._CuryAgeBalance00;
      set => this._CuryAgeBalance00 = value;
    }

    [PXBaseCury]
    [PXUIField(DisplayName = "Overdue Balance")]
    public virtual Decimal? OverdueBalance
    {
      [PXDependsOnFields(new System.Type[] {typeof (ARStatementPrint.DetailsResult.statementBalance), typeof (ARStatementPrint.DetailsResult.ageBalance00)})] get
      {
        Decimal? statementBalance = this.StatementBalance;
        Decimal? ageBalance00 = this.AgeBalance00;
        return !(statementBalance.HasValue & ageBalance00.HasValue) ? new Decimal?() : new Decimal?(statementBalance.GetValueOrDefault() - ageBalance00.GetValueOrDefault());
      }
    }

    [PXCury(typeof (ARStatementPrint.DetailsResult.curyID))]
    [PXUIField(DisplayName = "FC Overdue Balance")]
    public virtual Decimal? CuryOverdueBalance
    {
      [PXDependsOnFields(new System.Type[] {typeof (ARStatementPrint.DetailsResult.curyStatementBalance), typeof (ARStatementPrint.DetailsResult.curyAgeBalance00)})] get
      {
        Decimal? statementBalance = this._CuryStatementBalance;
        Decimal valueOrDefault = this.CuryAgeBalance00.GetValueOrDefault();
        return !statementBalance.HasValue ? new Decimal?() : new Decimal?(statementBalance.GetValueOrDefault() - valueOrDefault);
      }
    }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Don't Print")]
    public virtual bool? DontPrint
    {
      get => this._DontPrint;
      set => this._DontPrint = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Printed")]
    public virtual bool? Printed
    {
      get => this._Printed;
      set => this._Printed = value;
    }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Don't Email")]
    public virtual bool? DontEmail
    {
      get => this._DontEmail;
      set => this._DontEmail = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Emailed")]
    public virtual bool? Emailed
    {
      get => this._Emailed;
      set => this._Emailed = value;
    }

    [PXDBInt(IsKey = true)]
    [PXDefault]
    [Branch(null, null, true, true, true)]
    [PXUIField(DisplayName = "Branch", Visible = false)]
    public virtual int? BranchID
    {
      get => this._BranchID;
      set => this._BranchID = value;
    }

    [PXNote]
    public virtual Guid? NoteID { get; set; }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementPrint.DetailsResult.selected>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementPrint.DetailsResult.customerID>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARStatementPrint.DetailsResult.curyID>
    {
    }

    public abstract class statementBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementPrint.DetailsResult.statementBalance>
    {
    }

    public abstract class curyStatementBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementPrint.DetailsResult.curyStatementBalance>
    {
    }

    public abstract class useCurrency : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementPrint.DetailsResult.useCurrency>
    {
    }

    public abstract class ageBalance00 : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementPrint.DetailsResult.ageBalance00>
    {
    }

    public abstract class curyAgeBalance00 : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementPrint.DetailsResult.curyAgeBalance00>
    {
    }

    public abstract class overdueBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementPrint.DetailsResult.overdueBalance>
    {
    }

    public abstract class curyOverdueBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementPrint.DetailsResult.curyOverdueBalance>
    {
    }

    public abstract class dontPrint : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementPrint.DetailsResult.dontPrint>
    {
    }

    public abstract class printed : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementPrint.DetailsResult.printed>
    {
    }

    public abstract class dontEmail : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementPrint.DetailsResult.dontEmail>
    {
    }

    public abstract class emailed : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementPrint.DetailsResult.emailed>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementPrint.DetailsResult.branchID>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      ARStatementPrint.DetailsResult.noteID>
    {
    }
  }

  [Serializable]
  public class ContactR : PX.Objects.CR.Contact
  {
    public new abstract class contactID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementPrint.ContactR.contactID>
    {
    }

    public new abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementPrint.ContactR.bAccountID>
    {
    }
  }

  private interface IStatementsSelector
  {
    IEnumerable<ARStatement> Select(ARStatementPrint.DetailsResult detailsResult);

    void Update(ARStatement statement);
  }

  private class EmptyStatementsSelector : ARStatementPrint.IStatementsSelector
  {
    public IEnumerable<ARStatement> Select(ARStatementPrint.DetailsResult detailsResult)
    {
      return Enumerable.Empty<ARStatement>();
    }

    public void Update(ARStatement statement)
    {
    }
  }

  private class StatementsSelector : ARStatementPrint.IStatementsSelector
  {
    private readonly PXView selectStatementsView;
    protected readonly ARStatementPrint.PrintParameters filter;

    public StatementsSelector(PXGraph viewGraph, ARStatementPrint.PrintParameters filter)
    {
      this.filter = filter;
      this.selectStatementsView = new PXView(viewGraph, false, this.AddAdditionalConditions((BqlCommand) new Select<ARStatement, Where<ARStatement.statementCycleId, Equal<Required<ARStatement.statementCycleId>>, And<ARStatement.statementDate, Equal<Required<ARStatement.statementDate>>, And<ARStatement.statementCustomerID, Equal<Required<ARStatement.customerID>>>>>>()));
      viewGraph.Views["_ARStatement_"] = this.selectStatementsView;
      viewGraph.Views.Caches.Add(typeof (ARStatement));
    }

    public IEnumerable<ARStatement> Select(ARStatementPrint.DetailsResult detailsResult)
    {
      return GraphHelper.RowCast<ARStatement>((IEnumerable) this.selectStatementsView.SelectMulti(((IEnumerable<object>) new object[3]
      {
        (object) this.filter.StatementCycleId,
        (object) this.filter.StatementDate,
        (object) detailsResult.CustomerID
      }).Concat<object>(this.GetParametersForAdditionalConditions(detailsResult)).ToArray<object>()));
    }

    protected virtual BqlCommand AddAdditionalConditions(BqlCommand bqlCommand)
    {
      return this.filter.CuryStatements.GetValueOrDefault() ? bqlCommand.WhereAnd<Where<ARStatement.curyID, Equal<Required<ARStatement.curyID>>>>() : bqlCommand;
    }

    protected virtual IEnumerable<object> GetParametersForAdditionalConditions(
      ARStatementPrint.DetailsResult detailsResult)
    {
      if (this.filter.CuryStatements.GetValueOrDefault())
        yield return (object) detailsResult.CuryID;
    }

    public void Update(ARStatement statement)
    {
      this.selectStatementsView.Cache.Update((object) statement);
    }
  }

  private class StatementsSelectorEachBranch(
    PXGraph viewGraph,
    ARStatementPrint.PrintParameters filter) : ARStatementPrint.StatementsSelector(viewGraph, filter)
  {
    protected override BqlCommand AddAdditionalConditions(BqlCommand bqlCommand)
    {
      return base.AddAdditionalConditions(bqlCommand).WhereAnd<Where<ARStatement.branchID, Equal<Required<ARStatement.branchID>>>>();
    }

    protected override IEnumerable<object> GetParametersForAdditionalConditions(
      ARStatementPrint.DetailsResult detailsResult)
    {
      return base.GetParametersForAdditionalConditions(detailsResult).Append<object>((object) this.filter.BranchID);
    }
  }

  private class StatementsSelectorConsolidatedForCompany : ARStatementPrint.StatementsSelector
  {
    private readonly int[] branches;

    public StatementsSelectorConsolidatedForCompany(
      PXGraph viewGraph,
      ARStatementPrint.PrintParameters filter)
      : base(viewGraph, filter)
    {
      this.branches = PXAccess.GetChildBranchIDs(filter.OrganizationID, true);
    }

    protected override BqlCommand AddAdditionalConditions(BqlCommand bqlCommand)
    {
      return base.AddAdditionalConditions(bqlCommand).WhereAnd<Where<ARStatement.branchID, In<Required<ARStatement.branchID>>>>();
    }

    protected override IEnumerable<object> GetParametersForAdditionalConditions(
      ARStatementPrint.DetailsResult detailsResult)
    {
      return base.GetParametersForAdditionalConditions(detailsResult).Append<object>((object) this.branches);
    }
  }
}
