// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.PrintARDocuments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.Standalone;
using PX.Objects.CC.GraphExtensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.SM;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class PrintARDocuments : PXGraph<
#nullable disable
PrintARDocuments>
{
  public PXFilter<PrintInvoicesFilter> Filter;
  public PXCancel<PrintInvoicesFilter> Cancel;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessingJoin<PrintARDocuments.ARRegisterPrintDoc, PrintInvoicesFilter, InnerJoinSingleTable<Customer, On<Customer.bAccountID, Equal<ARRegister.customerID>>, LeftJoinSingleTable<ARInvoice, On<ARInvoice.refNbr, Equal<ARRegister.refNbr>, And<ARInvoice.docType, Equal<ARRegister.docType>>>, LeftJoinSingleTable<ARPayment, On<ARPayment.refNbr, Equal<ARRegister.refNbr>, And<ARPayment.docType, Equal<ARRegister.docType>>>>>>> ARDocumentList;
  public PXSetup<ARSetup> arsetup;

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Branch", Visible = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<FeaturesSet.branch>>.Or<FeatureInstalled<FeaturesSet.multiCompany>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<ARRegister.branchID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXStringListAttribute))]
  [ARDocType.PrintARDocTypeList]
  protected virtual void _(PX.Data.Events.CacheAttached<ARInvoice.docType> e)
  {
  }

  public PrintARDocuments()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PrintARDocuments.\u003C\u003Ec__DisplayClass8_0 cDisplayClass80 = new PrintARDocuments.\u003C\u003Ec__DisplayClass8_0();
    ARSetup current = ((PXSelectBase<ARSetup>) this.arsetup).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass80.filter = ((PXSelectBase<PrintInvoicesFilter>) this.Filter).Current;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.ARDocumentList).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<ARRegister.selected>(((PXSelectBase) this.ARDocumentList).Cache, (object) null, true);
    ((PXSelectBase) this.ARDocumentList).Cache.AllowInsert = false;
    ((PXSelectBase) this.ARDocumentList).Cache.AllowDelete = false;
    ((PXProcessingBase<PrintARDocuments.ARRegisterPrintDoc>) this.ARDocumentList).SetSelected<ARRegister.selected>();
    ((PXProcessing<PrintARDocuments.ARRegisterPrintDoc>) this.ARDocumentList).SetProcessCaption("Process");
    ((PXProcessing<PrintARDocuments.ARRegisterPrintDoc>) this.ARDocumentList).SetProcessAllCaption("Process All");
    ((PXProcessingBase<PrintARDocuments.ARRegisterPrintDoc>) this.ARDocumentList).SuppressUpdate = true;
    // ISSUE: method pointer
    ((PXProcessingBase<PrintARDocuments.ARRegisterPrintDoc>) this.ARDocumentList).SetProcessDelegate(new PXProcessingBase<PrintARDocuments.ARRegisterPrintDoc>.ProcessListDelegate((object) cDisplayClass80, __methodptr(\u003C\u002Ector\u003Eb__0)));
  }

  public IEnumerable aRDocumentList()
  {
    PrintARDocuments printArDocuments = this;
    System.Type bqlStatement = printArDocuments.GetBQLStatement();
    PXView pxView = new PXView((PXGraph) printArDocuments, false, BqlCommand.CreateInstance(new System.Type[1]
    {
      bqlStatement
    }));
    int startRow = PXView.StartRow;
    int num = 0;
    object[] searches = PXView.Searches;
    string[] sortColumns = PXView.SortColumns;
    bool[] descendings = PXView.Descendings;
    PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    ref int local1 = ref startRow;
    int maximumRows = PXView.MaximumRows;
    ref int local2 = ref num;
    List<object> objectList = pxView.Select((object[]) null, (object[]) null, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2);
    PXView.StartRow = 0;
    foreach (PXResult<PrintARDocuments.ARRegisterPrintDoc, Customer, ARInvoice, ARPayment> rec in objectList)
      yield return (object) printArDocuments.SetAdditionalFields(rec);
  }

  public static void ProcessDocuments(
    List<PrintARDocuments.ARRegisterPrintDoc> list,
    PrintInvoicesFilter filter)
  {
    ARInvoiceEntry arInvoiceEntry = (ARInvoiceEntry) null;
    ARPaymentEntry arPaymentEntry = (ARPaymentEntry) null;
    ARCashSaleEntry arCashSaleEntry = (ARCashSaleEntry) null;
    string action = filter.Action;
    switch (action)
    {
      case "P":
        PrintARDocuments.PrintInvoicesInBatchMode(list, filter);
        return;
      case "E":
        if (filter.AggregateEmails.GetValueOrDefault())
        {
          PrintARDocuments.EmailInvoicesInBatchMode(list, filter);
          return;
        }
        break;
    }
    for (int index = 0; index < list.Count; ++index)
    {
      PrintARDocuments.ARRegisterPrintDoc registerPrintDoc = list[index];
      try
      {
        PrintARDocuments.ClearGraphsIfNeeded(arInvoiceEntry, arCashSaleEntry, arPaymentEntry, index);
        switch (action)
        {
          case "E":
            if (EnumerableExtensions.IsIn<string>(registerPrintDoc.DocType, "CSL", "RCS"))
            {
              arCashSaleEntry = PrintARDocuments.GetOrCreateCashSaleGraph(arCashSaleEntry);
              ((PXSelectBase<ARCashSale>) arCashSaleEntry.Document).Current = PXResultset<ARCashSale>.op_Implicit(((PXSelectBase<ARCashSale>) arCashSaleEntry.Document).Search<ARCashSale.refNbr>((object) registerPrintDoc.RefNbr, new object[1]
              {
                (object) registerPrintDoc.DocType
              }));
              ((PXAction) arCashSaleEntry.emailInvoice).Press();
              break;
            }
            arInvoiceEntry = PrintARDocuments.GetOrCreateInvoiceGraph(arInvoiceEntry);
            ((PXSelectBase<ARInvoice>) arInvoiceEntry.Document).Current = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) arInvoiceEntry.Document).Search<ARInvoice.refNbr>((object) registerPrintDoc.RefNbr, new object[1]
            {
              (object) registerPrintDoc.DocType
            }));
            ((PXAction) arInvoiceEntry.emailInvoice).Press();
            break;
          case "R":
            if (EnumerableExtensions.IsIn<string>(registerPrintDoc.DocType, "CSL", "RCS"))
            {
              arCashSaleEntry = PrintARDocuments.GetOrCreateCashSaleGraph(arCashSaleEntry);
              ((PXSelectBase<ARCashSale>) arCashSaleEntry.Document).Current = PXResultset<ARCashSale>.op_Implicit(((PXSelectBase<ARCashSale>) arCashSaleEntry.Document).Search<ARCashSale.refNbr>((object) registerPrintDoc.RefNbr, new object[1]
              {
                (object) registerPrintDoc.DocType
              }));
              ((PXAction) arCashSaleEntry.emailInvoice).Press();
              break;
            }
            arPaymentEntry = PrintARDocuments.GetOrCreatePaymentGraph(arPaymentEntry);
            ((PXSelectBase<ARPayment>) arPaymentEntry.Document).Current = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) arPaymentEntry.Document).Search<ARPayment.refNbr>((object) registerPrintDoc.RefNbr, new object[1]
            {
              (object) registerPrintDoc.DocType
            }));
            ((PXAction) ((PXGraph) arPaymentEntry).GetExtension<ARPaymentEntryPaymentReceipt>().emailDocPaymentReceipt).Press();
            break;
          case "M":
            arInvoiceEntry = PrintARDocuments.GetOrCreateInvoiceGraph(arInvoiceEntry);
            ((PXSelectBase<ARInvoice>) arInvoiceEntry.Document).Current = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) arInvoiceEntry.Document).Search<ARInvoice.refNbr>((object) registerPrintDoc.RefNbr, new object[1]
            {
              (object) registerPrintDoc.DocType
            }));
            ((PXGraph) arInvoiceEntry).Actions["Mark as Do not Email"].Press();
            break;
        }
        PXProcessing<PrintARDocuments.ARRegisterPrintDoc>.SetInfo(index, "The record has been processed successfully.");
      }
      catch (Exception ex)
      {
        PXProcessing<PrintARDocuments.ARRegisterPrintDoc>.SetError(index, ex);
      }
    }
  }

  private static void PrintInvoicesInBatchMode(
    List<PrintARDocuments.ARRegisterPrintDoc> list,
    PrintInvoicesFilter filter)
  {
    ARInvoiceEntry invoiceGraph = PrintARDocuments.GetOrCreateInvoiceGraph((ARInvoiceEntry) null);
    List<object> coll = new List<object>();
    foreach (PrintARDocuments.ARRegisterPrintDoc registerPrintDoc in list)
    {
      ARInvoice arInvoice = PXResultset<ARInvoice>.op_Implicit(PXSelectBase<ARInvoice, PXSelect<ARInvoice, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) invoiceGraph, new object[2]
      {
        (object) registerPrintDoc.DocType,
        (object) registerPrintDoc.RefNbr
      }));
      coll.Add((object) arInvoice);
    }
    if (coll.Count() <= 0L)
      return;
    PXAdapter pxAdapter = new PXAdapter((PXView) new PXView.Dummy((PXGraph) invoiceGraph, ((PXSelectBase) invoiceGraph.Document).View.BqlSelect, coll))
    {
      MassProcess = true,
      Arguments = {
        ["PrintWithDeviceHub"] = (object) filter.PrintWithDeviceHub,
        ["DefinePrinterManually"] = (object) filter.DefinePrinterManually,
        ["PrinterID"] = (object) filter.PrinterID,
        ["NumberOfCopies"] = (object) filter.NumberOfCopies
      }
    };
    foreach (object obj in ((PXAction) invoiceGraph.printInvoice).Press(pxAdapter))
      ;
  }

  private static void EmailInvoicesInBatchMode(
    List<PrintARDocuments.ARRegisterPrintDoc> list,
    PrintInvoicesFilter filter)
  {
    ARInvoiceEntry invoiceGraph = PrintARDocuments.GetOrCreateInvoiceGraph((ARInvoiceEntry) null);
    List<object> coll = new List<object>();
    foreach (PrintARDocuments.ARRegisterPrintDoc registerPrintDoc in list)
    {
      ARInvoice arInvoice = ARInvoice.PK.Find((PXGraph) invoiceGraph, registerPrintDoc.DocType, registerPrintDoc.RefNbr);
      coll.Add((object) arInvoice);
    }
    if (coll.Count() <= 0L)
      return;
    PXAdapter pxAdapter = new PXAdapter((PXView) new PXView.Dummy((PXGraph) invoiceGraph, ((PXSelectBase) invoiceGraph.Document).View.BqlSelect, coll))
    {
      MassProcess = true,
      Arguments = {
        ["AggregateEmails"] = (object) filter.AggregateEmails,
        ["AggregateAttachments"] = (object) filter.AggregateAttachments,
        ["AggregatedAttachmentFileName"] = (object) "Invoices and Memos {0}.pdf"
      }
    };
    foreach (object obj in ((PXAction) invoiceGraph.emailInvoice).Press(pxAdapter))
      ;
  }

  private PXResult<PrintARDocuments.ARRegisterPrintDoc, Customer, ARInvoice, ARPayment> SetAdditionalFields(
    PXResult<PrintARDocuments.ARRegisterPrintDoc, Customer, ARInvoice, ARPayment> rec)
  {
    ARInvoice arInvoice = PXResult<PrintARDocuments.ARRegisterPrintDoc, Customer, ARInvoice, ARPayment>.op_Implicit(rec);
    ARPayment arPayment = PXResult<PrintARDocuments.ARRegisterPrintDoc, Customer, ARInvoice, ARPayment>.op_Implicit(rec);
    PrintARDocuments.ARRegisterPrintDoc registerPrintDoc = PXResult<PrintARDocuments.ARRegisterPrintDoc, Customer, ARInvoice, ARPayment>.op_Implicit(rec);
    if (arPayment != null && arPayment.RefNbr != null)
      registerPrintDoc.PaymentMethodID = arPayment.PaymentMethodID;
    else if (arInvoice != null && arInvoice.RefNbr != null)
    {
      registerPrintDoc.InvoiceNbr = arInvoice.InvoiceNbr;
      registerPrintDoc.PaymentMethodID = arInvoice.PaymentMethodID;
    }
    GraphHelper.Hold(((PXSelectBase) this.ARDocumentList).Cache, (object) registerPrintDoc);
    return rec;
  }

  private static ARCashSaleEntry GetOrCreateCashSaleGraph(ARCashSaleEntry graph)
  {
    return graph != null ? graph : PXGraph.CreateInstance<ARCashSaleEntry>();
  }

  private static ARPaymentEntry GetOrCreatePaymentGraph(ARPaymentEntry graph)
  {
    return graph != null ? graph : PXGraph.CreateInstance<ARPaymentEntry>();
  }

  private static ARInvoiceEntry GetOrCreateInvoiceGraph(ARInvoiceEntry graph)
  {
    return graph != null ? graph : PXGraph.CreateInstance<ARInvoiceEntry>();
  }

  private static void ClearGraphsIfNeeded(
    ARInvoiceEntry invoiceGraph,
    ARCashSaleEntry cashSaleGraph,
    ARPaymentEntry paymentGraph,
    int cnt)
  {
    if (cnt % 100 != 0)
      return;
    ((PXGraph) invoiceGraph)?.Clear();
    ((PXGraph) cashSaleGraph)?.Clear();
    ((PXGraph) paymentGraph)?.Clear();
  }

  protected virtual System.Type GetBQLStatement()
  {
    string action = ((PXSelectBase<PrintInvoicesFilter>) this.Filter).Current.Action;
    System.Type type1 = OwnedFilter.ProjectionAttribute.ComposeWhere(typeof (PrintInvoicesFilter), typeof (ARInvoice.workgroupID), typeof (ARInvoice.ownerID));
    System.Type type2 = typeof (Where<ARRegister.hold, Equal<False>, And<ARRegister.scheduled, Equal<False>, And<ARRegister.voided, Equal<False>, And<ARRegister.dontPrint, Equal<False>, And<ARRegister.printed, NotEqual<True>, And<ARInvoice.refNbr, IsNotNull>>>>>>);
    System.Type type3 = typeof (Where<ARRegister.hold, Equal<False>, And<ARRegister.scheduled, Equal<False>, And<ARRegister.voided, Equal<False>, And<ARInvoice.creditHold, Equal<False>, And<ARRegister.dontEmail, Equal<False>, And<ARRegister.emailed, NotEqual<True>, And<ARRegister.docType, In3<ARDocType.invoice, ARDocType.creditMemo, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO>>>>>>>>);
    System.Type type4 = typeof (Where<ARRegister.hold, Equal<False>, And<ARRegister.scheduled, Equal<False>, And<ARRegister.voided, Equal<False>, And<ARInvoice.creditHold, Equal<False>, And<ARRegister.dontEmail, Equal<False>, And<ARRegister.emailed, NotEqual<True>>>>>>>);
    System.Type type5 = typeof (Where<ARRegister.hold, Equal<False>, And<ARRegister.scheduled, Equal<False>, And<ARRegister.voided, Equal<False>, And<ARRegister.dontEmail, Equal<False>, And<ARRegister.emailed, NotEqual<True>, And<ARRegister.released, Equal<True>, And<Where2<Where<ARPayment.docType, In3<ARDocType.payment, ARDocType.prepayment, ARDocType.voidPayment, ARDocType.refund, ARDocType.voidRefund, ARDocType.cashSale, ARDocType.cashReturn>, And<ARPayment.isCCPayment, Equal<False>>>, Or2<Where<ARPayment.docType, In3<ARDocType.payment, ARDocType.prepayment, ARDocType.cashSale>, And<ARPayment.isCCPayment, Equal<True>, And<ARPayment.isCCCaptured, Equal<True>>>>, Or2<Where<ARPayment.docType, In3<ARDocType.voidPayment, ARDocType.cashReturn>, And<ARPayment.isCCPayment, Equal<True>, And<ARRegister.released, Equal<True>, And<ARPayment.isCCCaptured, Equal<False>, Or<ARPayment.isCCRefunded, Equal<True>>>>>>, Or<Where<ARPayment.docType, In3<ARDocType.refund, ARDocType.cashReturn>, And<ARPayment.isCCPayment, Equal<True>, And<ARPayment.isCCRefunded, Equal<True>>>>>>>>>>>>>>>);
    System.Type type6 = typeof (Where<ARRegister.docDate, LessEqual<Current<PrintInvoicesFilter.endDate>>, And<ARRegister.docDate, GreaterEqual<Current<PrintInvoicesFilter.beginDate>>>>);
    System.Type type7 = typeof (Where<Current<PrintInvoicesFilter.customerID>, IsNull, Or<ARRegister.customerID, Equal<Current<PrintInvoicesFilter.customerID>>>>);
    System.Type type8 = typeof (Where<Current<PrintInvoicesFilter.aggregateEmails>, Equal<False>, Or<ARRegister.docType, NotIn3<ARDocType.cashSale, ARDocType.cashReturn>>>);
    System.Type type9;
    if (((PXSelectBase<PrintInvoicesFilter>) this.Filter).Current.ShowAll.GetValueOrDefault())
    {
      type6 = typeof (Where<True, Equal<True>>);
      switch (action)
      {
        case "N":
          type9 = typeof (Where<True, Equal<False>>);
          break;
        case "R":
          type9 = typeof (Where<ARRegister.hold, Equal<False>, And<ARRegister.scheduled, Equal<False>, And<ARRegister.voided, Equal<False>, And<ARRegister.released, Equal<True>, And<Where2<Where<ARPayment.docType, In3<ARDocType.payment, ARDocType.prepayment, ARDocType.voidPayment, ARDocType.refund, ARDocType.voidRefund, ARDocType.cashSale, ARDocType.cashReturn>, And<ARPayment.isCCPayment, Equal<False>>>, Or2<Where<ARPayment.docType, In3<ARDocType.payment, ARDocType.prepayment, ARDocType.cashSale>, And<ARPayment.isCCPayment, Equal<True>, And<ARPayment.isCCCaptured, Equal<True>>>>, Or2<Where<ARPayment.docType, In3<ARDocType.voidPayment, ARDocType.cashReturn>, And<ARPayment.isCCPayment, Equal<True>, And<ARRegister.released, Equal<True>, And<ARPayment.isCCCaptured, Equal<False>, Or<ARPayment.isCCRefunded, Equal<True>>>>>>, Or<Where<ARPayment.docType, In3<ARDocType.refund, ARDocType.cashReturn>, And<ARPayment.isCCPayment, Equal<True>, And<ARPayment.isCCRefunded, Equal<True>>>>>>>>>>>>>);
          break;
        case "M":
          type9 = typeof (Where<ARRegister.hold, Equal<False>, And<ARRegister.scheduled, Equal<False>, And<ARRegister.voided, Equal<False>, And<ARInvoice.creditHold, Equal<False>, And<ARRegister.docType, In3<ARDocType.invoice, ARDocType.creditMemo, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO>>>>>>);
          break;
        default:
          type9 = typeof (Where<ARRegister.hold, Equal<False>, And<ARRegister.scheduled, Equal<False>, And<ARRegister.voided, Equal<False>, And<ARInvoice.creditHold, Equal<False>>>>>);
          break;
      }
    }
    else
    {
      switch (action)
      {
        case "P":
          type9 = type2;
          break;
        case "E":
          type9 = type4;
          break;
        case "R":
          type9 = type5;
          break;
        case "M":
          type9 = type3;
          break;
        default:
          type9 = typeof (Where<True, Equal<False>>);
          break;
      }
    }
    return BqlCommand.Compose(new System.Type[15]
    {
      typeof (Select2<,,>),
      typeof (PrintARDocuments.ARRegisterPrintDoc),
      typeof (InnerJoinSingleTable<Customer, On<Customer.bAccountID, Equal<ARRegister.customerID>>, LeftJoinSingleTable<ARInvoice, On<ARInvoice.refNbr, Equal<ARRegister.refNbr>, And<ARInvoice.docType, Equal<ARRegister.docType>>>, LeftJoinSingleTable<ARPayment, On<ARPayment.refNbr, Equal<ARRegister.refNbr>, And<ARPayment.docType, Equal<ARRegister.docType>>>>>>),
      typeof (Where2<,>),
      typeof (Match<Customer, Current<AccessInfo.userName>>),
      typeof (And2<,>),
      type9,
      typeof (And2<,>),
      type6,
      typeof (And2<,>),
      type1,
      typeof (And2<,>),
      type7,
      typeof (And<>),
      type8
    });
  }

  public virtual bool IsDirty => false;

  protected virtual void PrintInvoicesFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PrintInvoicesFilter row = (PrintInvoicesFilter) e.Row;
    if (row == null || string.IsNullOrEmpty(row.Action))
      return;
    ((PXSelectBase) this.Filter).Cache.ToDictionary(e.Row);
    bool flag1 = this.IsPrintingAllowed(row);
    PXUIFieldAttribute.SetVisible<PrintInvoicesFilter.printWithDeviceHub>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<PrintInvoicesFilter.definePrinterManually>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<PrintInvoicesFilter.printerID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<PrintInvoicesFilter.numberOfCopies>(sender, (object) row, flag1);
    bool flag2 = row.Action == "E";
    bool? aggregateEmails = (bool?) row?.AggregateEmails;
    PXUIFieldAttribute.SetVisible<PrintInvoicesFilter.aggregateEmails>(sender, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<PrintInvoicesFilter.aggregateAttachments>(sender, (object) null, flag2);
    PXUIFieldAttribute.SetEnabled<PrintInvoicesFilter.aggregateAttachments>(sender, (object) null, flag2 && aggregateEmails.GetValueOrDefault());
    if (PXContext.GetSlot<AUSchedule>() != null)
      return;
    PXCache pxCache1 = sender;
    PrintInvoicesFilter printInvoicesFilter1 = row;
    bool? nullable = row.PrintWithDeviceHub;
    int num1 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PrintInvoicesFilter.definePrinterManually>(pxCache1, (object) printInvoicesFilter1, num1 != 0);
    PXCache pxCache2 = sender;
    PrintInvoicesFilter printInvoicesFilter2 = row;
    nullable = row.PrintWithDeviceHub;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PrintInvoicesFilter.numberOfCopies>(pxCache2, (object) printInvoicesFilter2, num2 != 0);
    PXCache pxCache3 = sender;
    PrintInvoicesFilter printInvoicesFilter3 = row;
    nullable = row.PrintWithDeviceHub;
    int num3;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.DefinePrinterManually;
      num3 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetEnabled<PrintInvoicesFilter.printerID>(pxCache3, (object) printInvoicesFilter3, num3 != 0);
  }

  protected virtual bool IsPrintingAllowed(PrintInvoicesFilter filter)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() && filter != null && !string.IsNullOrEmpty(filter.Action) && filter.Action == "P";
  }

  protected virtual void PrintInvoicesFilter_Action_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    foreach (ARRegister arRegister in ((PXSelectBase) this.ARDocumentList).Cache.Updated)
      ((PXSelectBase) this.ARDocumentList).Cache.SetDefaultExt<ARRegister.selected>((object) arRegister);
  }

  public virtual void PrintInvoicesFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<PrintInvoicesFilter.action>(e.Row, e.OldRow) && sender.ObjectsEqual<PrintInvoicesFilter.definePrinterManually>(e.Row, e.OldRow) && sender.ObjectsEqual<PrintInvoicesFilter.printWithDeviceHub>(e.Row, e.OldRow) || ((PXSelectBase<PrintInvoicesFilter>) this.Filter).Current == null || !PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() || !((PXSelectBase<PrintInvoicesFilter>) this.Filter).Current.PrintWithDeviceHub.GetValueOrDefault() || !((PXSelectBase<PrintInvoicesFilter>) this.Filter).Current.DefinePrinterManually.GetValueOrDefault() || PXContext.GetSlot<AUSchedule>() != null && ((PXSelectBase<PrintInvoicesFilter>) this.Filter).Current.PrinterID.HasValue && !((PrintInvoicesFilter) e.OldRow).PrinterID.HasValue)
      return;
    ((PXSelectBase<PrintInvoicesFilter>) this.Filter).Current.PrinterID = new NotificationUtility((PXGraph) this).SearchPrinter("Customer", "AR641000", ((PXGraph) this).Accessinfo.BranchID);
  }

  protected virtual void PrintInvoicesFilter_BeginDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate.Value.AddMonths(-1);
  }

  protected virtual void PrintInvoicesFilter_PrinterName_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    PrintInvoicesFilter row = (PrintInvoicesFilter) e.Row;
    if (row == null || this.IsPrintingAllowed(row))
      return;
    e.NewValue = (object) null;
  }

  public static class PrintARDocumentsAction
  {
    public const string None = "N";
    public const string MarkNotEmail = "M";
    public const string PrintInvoice = "P";
    public const string EmailInvoice = "E";
    public const string EmailPaymentReceipt = "R";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(PrintARDocuments.PrintARDocumentsAction.ListAttribute.ValueLabelPairs())
      {
      }

      public static Tuple<string, string>[] ValueLabelPairs()
      {
        return new Tuple<string, string>[5]
        {
          new Tuple<string, string>("N", "<SELECT>"),
          new Tuple<string, string>("M", "Mark as Do not Email"),
          new Tuple<string, string>("P", "Print Invoices"),
          new Tuple<string, string>("E", "Email Invoices"),
          new Tuple<string, string>("R", "Email Payment Receipts")
        };
      }
    }
  }

  [PXHidden]
  public class ARRegisterPrintDoc : ARRegister
  {
    [PXString(IsUnicode = true)]
    [PXUIField(DisplayName = "Customer Order Nbr.")]
    public virtual string InvoiceNbr { get; set; }

    [PXString(IsUnicode = true)]
    [PXUIField(DisplayName = "Payment Method")]
    public virtual string PaymentMethodID { get; set; }

    public abstract class invoiceNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PrintARDocuments.ARRegisterPrintDoc.invoiceNbr>
    {
    }

    public abstract class paymentMethodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PrintARDocuments.ARRegisterPrintDoc.paymentMethodID>
    {
    }
  }
}
