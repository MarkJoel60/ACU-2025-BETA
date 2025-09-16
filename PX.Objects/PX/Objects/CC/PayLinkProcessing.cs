// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.PayLinkProcessing
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CC.GraphExtensions;
using PX.Objects.CC.PaymentProcessing.Helpers;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CC;

public class PayLinkProcessing : PXGraph<
#nullable disable
PayLinkProcessing>
{
  public PXCancel<PayLinkProcessing.PayLinkFilter> Cancel;
  public PXFilter<PayLinkProcessing.PayLinkFilter> Filter;
  [PXHidden]
  public PXSelect<CCPayLink> PayLinks;
  [PXFilterable(new Type[] {})]
  [PXVirtualDAC]
  public PXFilteredProcessing<PayLinkProcessing.PayLinkDocument, PayLinkProcessing.PayLinkFilter> DocumentList;
  public PXAction<PayLinkProcessing.PayLinkFilter> viewCustomer;
  public PXAction<PayLinkProcessing.PayLinkFilter> viewDocument;

  public virtual IEnumerable documentList()
  {
    PayLinkProcessing payLinkProcessing = this;
    PayLinkProcessing.PayLinkFilter filter = ((PXSelectBase<PayLinkProcessing.PayLinkFilter>) payLinkProcessing.Filter).Current;
    PXSelectJoin<PX.Objects.AR.ARInvoice, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.AR.ARInvoice.customerID>>, InnerJoin<CustomerClass, On<CustomerClass.customerClassID, Equal<PX.Objects.AR.Customer.customerClassID>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<PX.Objects.AR.ARInvoice.branchID>>, LeftJoin<CCPayLink, On<CCPayLink.payLinkID, Equal<ARInvoicePayLink.payLinkID>>>>>>, Where<PX.Objects.AR.ARRegister.released, Equal<True>, And<ARInvoicePayLink.processingCenterID, IsNotNull, And<CustomerClassPayLink.disablePayLink, Equal<False>, And<PX.Objects.AR.ARRegister.docType, In3<ARInvoicePayLink.aRDocTypePayLink.ARDocTypesPayLinkAllowed>>>>>> pxSelectJoin1 = new PXSelectJoin<PX.Objects.AR.ARInvoice, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.AR.ARInvoice.customerID>>, InnerJoin<CustomerClass, On<CustomerClass.customerClassID, Equal<PX.Objects.AR.Customer.customerClassID>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<PX.Objects.AR.ARInvoice.branchID>>, LeftJoin<CCPayLink, On<CCPayLink.payLinkID, Equal<ARInvoicePayLink.payLinkID>>>>>>, Where<PX.Objects.AR.ARRegister.released, Equal<True>, And<ARInvoicePayLink.processingCenterID, IsNotNull, And<CustomerClassPayLink.disablePayLink, Equal<False>, And<PX.Objects.AR.ARRegister.docType, In3<ARInvoicePayLink.aRDocTypePayLink.ARDocTypesPayLinkAllowed>>>>>>((PXGraph) payLinkProcessing);
    if (filter?.Action == "C")
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) pxSelectJoin1).WhereAnd<Where<PX.Objects.AR.ARRegister.openDoc, Equal<True>, And<Where<CCPayLink.payLinkID, IsNull, Or<CCPayLink.linkStatus, In3<PayLinkStatus.none, PayLinkStatus.closed>>>>>>();
    else
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) pxSelectJoin1).WhereAnd<Where<CCPayLink.linkStatus, Equal<PayLinkStatus.open>>>();
    object[] prms = new object[4];
    PayLinkProcessing.PayLinkFilter payLinkFilter1 = filter;
    int? customerId;
    int num1;
    if (payLinkFilter1 == null)
    {
      num1 = 0;
    }
    else
    {
      customerId = payLinkFilter1.CustomerID;
      num1 = customerId.HasValue ? 1 : 0;
    }
    if (num1 != 0)
    {
      prms[0] = (object) filter.CustomerID;
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) pxSelectJoin1).WhereAnd<Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>();
    }
    foreach (PXResult<PX.Objects.AR.ARInvoice, PX.Objects.AR.Customer, CustomerClass, PX.Objects.GL.Branch, CCPayLink> pxResult in ((PXSelectBase<PX.Objects.AR.ARInvoice>) pxSelectJoin1).Select(prms))
    {
      PX.Objects.AR.ARInvoice arInvoice = PXResult<PX.Objects.AR.ARInvoice, PX.Objects.AR.Customer, CustomerClass, PX.Objects.GL.Branch, CCPayLink>.op_Implicit(pxResult);
      CCPayLink payLink = PXResult<PX.Objects.AR.ARInvoice, PX.Objects.AR.Customer, CustomerClass, PX.Objects.GL.Branch, CCPayLink>.op_Implicit(pxResult);
      PX.Objects.AR.Customer customer = PXResult<PX.Objects.AR.ARInvoice, PX.Objects.AR.Customer, CustomerClass, PX.Objects.GL.Branch, CCPayLink>.op_Implicit(pxResult);
      PX.Objects.GL.Branch branch = PXResult<PX.Objects.AR.ARInvoice, PX.Objects.AR.Customer, CustomerClass, PX.Objects.GL.Branch, CCPayLink>.op_Implicit(pxResult);
      CustomerClass customerClass = PXResult<PX.Objects.AR.ARInvoice, PX.Objects.AR.Customer, CustomerClass, PX.Objects.GL.Branch, CCPayLink>.op_Implicit(pxResult);
      ARInvoicePayLink extension = ((PXGraph) payLinkProcessing).Caches[typeof (PX.Objects.AR.ARInvoice)].GetExtension<ARInvoicePayLink>((object) arInvoice);
      PayLinkProcessing.PayLinkDocument payLinkDoc = new PayLinkProcessing.PayLinkDocument();
      payLinkDoc.DocType = arInvoice.DocType;
      payLinkDoc.DocTypeDisplayName = ARDocType.GetDisplayName(arInvoice.DocType);
      payLinkDoc.RefNbr = arInvoice.RefNbr;
      payLinkDoc.AcctName = customer.AcctName;
      payLinkDoc.BranchID = branch.BranchCD;
      payLinkDoc.CuryDocBal = arInvoice.CuryDocBal;
      payLinkDoc.CuryID = arInvoice.CuryID;
      payLinkDoc.CuryOrigDocAmt = arInvoice.CuryOrigDocAmt;
      payLinkDoc.CustomerClassID = customerClass.CustomerClassID;
      payLinkDoc.CustomerID = customer.BAccountID;
      payLinkDoc.DocDate = arInvoice.DocDate;
      payLinkDoc.DueDate = arInvoice.DueDate;
      payLinkDoc.ProcessingCenterID = extension.ProcessingCenterID;
      payLinkDoc.NoteID = arInvoice.NoteID;
      payLinkProcessing.SetPayLinkDocFields(payLinkDoc, payLink);
      GraphHelper.Hold(((PXSelectBase) payLinkProcessing.DocumentList).Cache, (object) payLinkDoc);
      yield return (object) payLinkDoc;
    }
    if (!(filter.Action == "C"))
    {
      PXSelectJoin<PX.Objects.SO.SOOrder, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.SO.SOOrder.customerID>>, InnerJoin<CustomerClass, On<CustomerClass.customerClassID, Equal<PX.Objects.AR.Customer.customerClassID>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<PX.Objects.SO.SOOrder.branchID>>, InnerJoin<SOOrderType, On<SOOrderType.orderType, Equal<PX.Objects.SO.SOOrder.orderType>>, LeftJoin<CCPayLink, On<CCPayLink.payLinkID, Equal<SOOrderPayLink.payLinkID>>>>>>>, Where<SOOrderType.canHavePayments, Equal<True>, And<SOOrderPayLink.processingCenterID, IsNotNull, And<CustomerClassPayLink.disablePayLink, Equal<False>, And<PX.Objects.SO.SOOrder.behavior, NotIn3<SOBehavior.iN, SOBehavior.mO>, And<Where<CCPayLink.linkStatus, Equal<PayLinkStatus.open>>>>>>>> pxSelectJoin2 = new PXSelectJoin<PX.Objects.SO.SOOrder, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.SO.SOOrder.customerID>>, InnerJoin<CustomerClass, On<CustomerClass.customerClassID, Equal<PX.Objects.AR.Customer.customerClassID>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<PX.Objects.SO.SOOrder.branchID>>, InnerJoin<SOOrderType, On<SOOrderType.orderType, Equal<PX.Objects.SO.SOOrder.orderType>>, LeftJoin<CCPayLink, On<CCPayLink.payLinkID, Equal<SOOrderPayLink.payLinkID>>>>>>>, Where<SOOrderType.canHavePayments, Equal<True>, And<SOOrderPayLink.processingCenterID, IsNotNull, And<CustomerClassPayLink.disablePayLink, Equal<False>, And<PX.Objects.SO.SOOrder.behavior, NotIn3<SOBehavior.iN, SOBehavior.mO>, And<Where<CCPayLink.linkStatus, Equal<PayLinkStatus.open>>>>>>>>((PXGraph) payLinkProcessing);
      PayLinkProcessing.PayLinkFilter payLinkFilter2 = filter;
      int num2;
      if (payLinkFilter2 == null)
      {
        num2 = 0;
      }
      else
      {
        customerId = payLinkFilter2.CustomerID;
        num2 = customerId.HasValue ? 1 : 0;
      }
      if (num2 != 0)
      {
        prms[0] = (object) filter.CustomerID;
        ((PXSelectBase<PX.Objects.SO.SOOrder>) pxSelectJoin2).WhereAnd<Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>();
      }
      foreach (PXResult<PX.Objects.SO.SOOrder, PX.Objects.AR.Customer, CustomerClass, PX.Objects.GL.Branch, SOOrderType, CCPayLink> pxResult in ((PXSelectBase<PX.Objects.SO.SOOrder>) pxSelectJoin2).Select(prms))
      {
        PX.Objects.SO.SOOrder soOrder = PXResult<PX.Objects.SO.SOOrder, PX.Objects.AR.Customer, CustomerClass, PX.Objects.GL.Branch, SOOrderType, CCPayLink>.op_Implicit(pxResult);
        CCPayLink payLink = PXResult<PX.Objects.SO.SOOrder, PX.Objects.AR.Customer, CustomerClass, PX.Objects.GL.Branch, SOOrderType, CCPayLink>.op_Implicit(pxResult);
        PX.Objects.AR.Customer customer = PXResult<PX.Objects.SO.SOOrder, PX.Objects.AR.Customer, CustomerClass, PX.Objects.GL.Branch, SOOrderType, CCPayLink>.op_Implicit(pxResult);
        PX.Objects.GL.Branch branch = PXResult<PX.Objects.SO.SOOrder, PX.Objects.AR.Customer, CustomerClass, PX.Objects.GL.Branch, SOOrderType, CCPayLink>.op_Implicit(pxResult);
        CustomerClass customerClass = PXResult<PX.Objects.SO.SOOrder, PX.Objects.AR.Customer, CustomerClass, PX.Objects.GL.Branch, SOOrderType, CCPayLink>.op_Implicit(pxResult);
        SOOrderPayLink extension = ((PXGraph) payLinkProcessing).Caches[typeof (PX.Objects.SO.SOOrder)].GetExtension<SOOrderPayLink>((object) soOrder);
        PayLinkProcessing.PayLinkDocument payLinkDoc = new PayLinkProcessing.PayLinkDocument();
        payLinkDoc.DocType = soOrder.OrderType;
        payLinkDoc.DocTypeDisplayName = soOrder.OrderType + " - Sales Order";
        payLinkDoc.RefNbr = soOrder.OrderNbr;
        payLinkDoc.AcctName = customer.AcctName;
        payLinkDoc.BranchID = branch.BranchCD;
        payLinkDoc.CuryDocBal = soOrder.CuryUnpaidBalance;
        payLinkDoc.CuryID = soOrder.CuryID;
        payLinkDoc.CuryOrigDocAmt = soOrder.CuryOrderTotal;
        payLinkDoc.CustomerClassID = customerClass.CustomerClassID;
        payLinkDoc.CustomerID = customer.BAccountID;
        payLinkDoc.DocDate = soOrder.OrderDate;
        payLinkDoc.DueDate = soOrder.RequestDate;
        payLinkDoc.ProcessingCenterID = extension.ProcessingCenterID;
        payLinkDoc.NoteID = soOrder.NoteID;
        payLinkProcessing.SetPayLinkDocFields(payLinkDoc, payLink);
        GraphHelper.Hold(((PXSelectBase) payLinkProcessing.DocumentList).Cache, (object) payLinkDoc);
        yield return (object) payLinkDoc;
      }
    }
  }

  [PXUIField(DisplayName = "View Customer")]
  [PXButton]
  public virtual void ViewCustomer()
  {
    PayLinkProcessing.PayLinkDocument current = ((PXSelectBase<PayLinkProcessing.PayLinkDocument>) this.DocumentList).Current;
    if (current != null)
    {
      PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) current.CustomerID
      }));
      CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
      ((PXSelectBase<PX.Objects.AR.Customer>) instance.BAccount).Current = customer;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [PXUIField]
  [PXButton]
  public virtual void ViewDocument()
  {
    PayLinkProcessing.PayLinkDocument current = ((PXSelectBase<PayLinkProcessing.PayLinkDocument>) this.DocumentList).Current;
    if (current == null)
      return;
    if (ARInvoiceEntryPayLink.DocTypePayLinkAllowed(current.DocType))
    {
      PX.Objects.AR.ARInvoice arInvoice = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) current.DocType,
        (object) current.RefNbr
      }));
      ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = arInvoice;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    PX.Objects.SO.SOOrder soOrder = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXSelect<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.orderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) current.DocType,
      (object) current.RefNbr
    }));
    SOOrderEntry instance1 = PXGraph.CreateInstance<SOOrderEntry>();
    ((PXSelectBase<PX.Objects.SO.SOOrder>) instance1.Document).Current = soOrder;
    PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, true, (string) null);
    ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException1;
  }

  public PayLinkProcessing()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<PayLinkProcessing.PayLinkDocument>) this.DocumentList).SetProcessDelegate(new PXProcessingBase<PayLinkProcessing.PayLinkDocument>.ProcessListDelegate((object) new PayLinkProcessing.\u003C\u003Ec__DisplayClass9_0()
    {
      filter = ((PXSelectBase<PayLinkProcessing.PayLinkFilter>) this.Filter).Current
    }, __methodptr(\u003C\u002Ector\u003Eb__0)));
  }

  private void SetPayLinkDocFields(PayLinkProcessing.PayLinkDocument payLinkDoc, CCPayLink payLink)
  {
    if (payLink == null || PayLinkHelper.PayLinkWasProcessed(payLink))
      return;
    payLinkDoc.ExternalID = payLink.ExternalID;
    payLinkDoc.ErrorMessage = payLink.ErrorMessage;
    payLinkDoc.StatusDate = payLink.StatusDate;
    payLinkDoc.NeedSync = payLink.NeedSync;
    payLinkDoc.PayLinkAmt = payLink.Amount;
  }

  private static ARInvoiceEntry GetInvoiceGraph() => PXGraph.CreateInstance<ARInvoiceEntry>();

  private static SOOrderEntry GetOrderGraph() => PXGraph.CreateInstance<SOOrderEntry>();

  private static void Process(
    List<PayLinkProcessing.PayLinkDocument> items,
    PayLinkProcessing.PayLinkFilter filter)
  {
    ARInvoiceEntry graph1 = (ARInvoiceEntry) null;
    SOOrderEntry graph2 = (SOOrderEntry) null;
    for (int index = 0; index < items.Count; ++index)
    {
      try
      {
        PayLinkProcessing.PayLinkDocument payLinkDocument = items[index];
        if (ARInvoiceEntryPayLink.DocTypePayLinkAllowed(payLinkDocument.DocType))
        {
          if (graph1 == null)
            graph1 = PayLinkProcessing.GetInvoiceGraph();
          PayLinkProcessing.ProcessInvoicePayLink(payLinkDocument, filter, graph1);
        }
        else
        {
          if (graph2 == null)
            graph2 = PayLinkProcessing.GetOrderGraph();
          PayLinkProcessing.ProcessSalesOrderPayLink(payLinkDocument, filter, graph2);
        }
        PXProcessing<PayLinkProcessing.PayLinkDocument>.SetInfo(index, "The record has been processed successfully.");
      }
      catch (Exception ex)
      {
        PXProcessing<PayLinkProcessing.PayLinkDocument>.SetError(index, ex);
      }
    }
  }

  private static void ProcessSalesOrderPayLink(
    PayLinkProcessing.PayLinkDocument item,
    PayLinkProcessing.PayLinkFilter filter,
    SOOrderEntry graph)
  {
    ((PXGraph) graph).Clear();
    ((PXSelectBase<PX.Objects.SO.SOOrder>) graph.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) graph.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) item.RefNbr, new object[1]
    {
      (object) item.DocType
    }));
    SOOrderEntryPayLink extension = ((PXGraph) graph).GetExtension<SOOrderEntryPayLink>();
    if (filter.Action == "C")
      ((PXAction) extension.createLink).Press();
    else
      ((PXAction) extension.syncLink).Press();
  }

  private static void ProcessInvoicePayLink(
    PayLinkProcessing.PayLinkDocument item,
    PayLinkProcessing.PayLinkFilter filter,
    ARInvoiceEntry graph)
  {
    ((PXGraph) graph).Clear();
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) graph.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) graph.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) item.RefNbr, new object[1]
    {
      (object) item.DocType
    }));
    ARInvoiceEntryPayLink extension = ((PXGraph) graph).GetExtension<ARInvoiceEntryPayLink>();
    if (filter.Action == "C")
      ((PXAction) extension.createLink).Press();
    else
      ((PXAction) extension.syncLink).Press();
  }

  [PXHidden]
  public class PayLinkFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(IsFixed = true)]
    [PXDefault("C")]
    [PayLinkProcessingAction.List]
    [PXUIField(DisplayName = "Action")]
    public virtual string Action { get; set; }

    [Customer(DescriptionField = typeof (PX.Objects.AR.Customer.acctName))]
    public virtual int? CustomerID { get; set; }

    public abstract class pendingOperation : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkFilter.pendingOperation>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkFilter.customerID>
    {
    }
  }

  /// <summary>
  /// Represents a row on the Payment Link processing screen.
  /// </summary>
  [PXCacheName("Document Payment Link")]
  public class PayLinkDocument : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    /// <exclude />
    [PXBool]
    [PXUnboundDefault(false)]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected { get; set; }

    /// <exclude />
    [PXString(IsKey = true)]
    [PXUIField(DisplayName = "Document Type")]
    public virtual string DocType { get; set; }

    /// <exclude />
    [PXString]
    [PXUIField(DisplayName = "Document Type")]
    public virtual string DocTypeDisplayName { get; set; }

    /// <exclude />
    [PXString(IsKey = true)]
    [PXUIField(DisplayName = "Reference Nbr.")]
    public virtual string RefNbr { get; set; }

    /// <exclude />
    [PXString]
    [PXUIField(DisplayName = "Branch")]
    public virtual string BranchID { get; set; }

    /// <exclude />
    [Customer(typeof (Search<PX.Objects.AR.Customer.bAccountID>), new Type[] {typeof (PX.Objects.AR.Customer.acctCD)}, DisplayName = "Customer")]
    public virtual int? CustomerID { get; set; }

    /// <exclude />
    [PXString]
    [PXUIField(DisplayName = "Customer Name")]
    public virtual string AcctName { get; set; }

    /// <exclude />
    [PXString]
    [PXUIField(DisplayName = "Customer Class")]
    public virtual string CustomerClassID { get; set; }

    /// <exclude />
    [PXDate]
    [PXUIField(DisplayName = "Document Date")]
    public virtual DateTime? DocDate { get; set; }

    /// <exclude />
    [PXDate]
    [PXUIField(DisplayName = "Due Date")]
    public virtual DateTime? DueDate { get; set; }

    /// <exclude />
    [PXDecimal]
    [PXUIField(DisplayName = "Document Total Amount")]
    public virtual Decimal? CuryOrigDocAmt { get; set; }

    /// <exclude />
    [PXDecimal]
    [PXUIField(DisplayName = "Unpaid Balance")]
    public virtual Decimal? CuryDocBal { get; set; }

    /// <summary>Amount to be payed by Payment Link.</summary>
    [PXDecimal]
    [PXUIField(DisplayName = "Payment Link Amount")]
    public virtual Decimal? PayLinkAmt { get; set; }

    /// <exclude />
    [PXString]
    [PXUIField(DisplayName = "Currency")]
    public virtual string CuryID { get; set; }

    /// <exclude />
    [PXString]
    [PXUIField(DisplayName = "Proc. Center ID")]
    public virtual string ProcessingCenterID { get; set; }

    /// <summary>
    /// Date of the last interaction with the Payment Link webportal.
    /// </summary>
    [PXDate]
    [PXUIField(DisplayName = "Status Date")]
    public virtual DateTime? StatusDate { get; set; }

    /// <exclude />
    [PXString]
    [PXUIField(DisplayName = "Error Message")]
    public virtual string ErrorMessage { get; set; }

    /// <summary>Payment Link webportal specific Id.</summary>
    [PXString]
    [PXUIField(DisplayName = "Link External ID", Visible = false)]
    public virtual string ExternalID { get; set; }

    /// <summary>Need update Payment Link after the document update.</summary>
    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Sync Required", Enabled = false)]
    public virtual bool? NeedSync { get; set; }

    /// <exclude />
    [PXNote]
    public virtual Guid? NoteID { get; set; }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.selected>
    {
    }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.docType>
    {
    }

    public abstract class docTypeDisplayName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.docTypeDisplayName>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.refNbr>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.branchID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.customerID>
    {
    }

    public abstract class acctName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.acctName>
    {
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.customerClassID>
    {
    }

    public abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.docDate>
    {
    }

    public abstract class dueDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.dueDate>
    {
    }

    public abstract class curyOrigDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.curyOrigDocAmt>
    {
    }

    public abstract class curyDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.curyDocBal>
    {
    }

    public abstract class payLinkAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.payLinkAmt>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.curyID>
    {
    }

    public abstract class processingCenterID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.processingCenterID>
    {
    }

    public abstract class statusDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.statusDate>
    {
    }

    public abstract class errorMessage : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.errorMessage>
    {
    }

    public abstract class externalID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.externalID>
    {
    }

    public abstract class needSync : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.needSync>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      PayLinkProcessing.PayLinkDocument.noteID>
    {
    }
  }
}
