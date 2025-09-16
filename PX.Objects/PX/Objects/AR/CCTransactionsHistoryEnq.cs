// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCTransactionsHistoryEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.Standalone;
using PX.Objects.CA;
using PX.Objects.CR.Extensions;
using PX.Objects.GL;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class CCTransactionsHistoryEnq : PXGraph<
#nullable disable
CCTransactionsHistoryEnq>
{
  public PXFilter<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter> Filter;
  public PXCancel<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter> Cancel;
  [PXFilterable(new Type[] {})]
  public PXSelectJoin<CCProcTran, InnerJoin<CustomerPaymentMethod, On<CustomerPaymentMethod.pMInstanceID, Equal<CCProcTran.pMInstanceID>>, InnerJoin<Customer, On<CustomerPaymentMethod.bAccountID, Equal<Customer.bAccountID>>, LeftJoin<ARPayment, On<ARPayment.docType, Equal<CCProcTran.docType>, And<ARPayment.refNbr, Equal<CCProcTran.refNbr>>>>>>> CCTrans;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public PXAction<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter> ViewDocument;
  public PXAction<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter> ViewCustomer;
  public PXAction<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter> ViewPaymentMethod;
  public PXAction<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter> ViewExternalTransaction;
  protected string CardNumber_DetailID = string.Empty;
  protected string NameOnCard_DetailID = string.Empty;

  public CCTransactionsHistoryEnq()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    ((PXSelectBase) this.CCTrans).Cache.AllowInsert = false;
    ((PXSelectBase) this.CCTrans).Cache.AllowUpdate = false;
    ((PXSelectBase) this.CCTrans).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetRequired<ARPayment.customerID>(((PXGraph) this).Caches[typeof (ARPayment)], false);
    PXUIFieldAttribute.SetRequired<Customer.acctName>(((PXGraph) this).Caches[typeof (Customer)], false);
    PXUIFieldAttribute.SetRequired<CCProcTran.processingCenterID>(((PXSelectBase) this.CCTrans).Cache, false);
    PXUIFieldAttribute.SetRequired<CCProcTran.startTime>(((PXSelectBase) this.CCTrans).Cache, false);
  }

  public virtual IEnumerable cCTrans()
  {
    CCTransactionsHistoryEnq transactionsHistoryEnq = this;
    List<PXResult<CCProcTran, CustomerPaymentMethod, Customer, ARPayment>> pxResultList = new List<PXResult<CCProcTran, CustomerPaymentMethod, Customer, ARPayment>>();
    CCTransactionsHistoryEnq.CCTransactionsHistoryFilter current = ((PXSelectBase<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter>) transactionsHistoryEnq.Filter).Current;
    if (current != null && !string.IsNullOrEmpty(current.PaymentMethodID) && current.StartDate.HasValue && current.EndDate.HasValue)
    {
      transactionsHistoryEnq.GetInternalData(current.PaymentMethodID);
      PXView view = new PXView((PXGraph) transactionsHistoryEnq, true, ((PXSelectBase) transactionsHistoryEnq.CCTrans).View.BqlSelect);
      view.WhereAnd<Where<CustomerPaymentMethod.paymentMethodID, Equal<Current<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.paymentMethodID>>>>();
      view.WhereAnd<Where<CCProcTran.startTime, GreaterEqual<Current<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.startDate>>>>();
      view.WhereAnd<Where<CCProcTran.startTime, LessEqual<Current<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.endDatePlusOne>>>>();
      if (!string.IsNullOrEmpty(current.ProcessingCenterID))
        view.WhereAnd<Where<CCProcTran.processingCenterID, Equal<Current<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.processingCenterID>>>>();
      if (current.PMInstanceID.HasValue)
        view.WhereAnd<Where<CustomerPaymentMethod.pMInstanceID, Equal<Current<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.pMInstanceID>>>>();
      if (!string.IsNullOrEmpty(current.CardType))
        view.WhereAnd<Where<CustomerPaymentMethod.cardType, Equal<Current<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.cardType>>>>();
      if (!string.IsNullOrEmpty(current.ProcessingCenterID))
        view.WhereAnd<Where<CustomerPaymentMethod.cCProcessingCenterID, Equal<Current<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.processingCenterID>>>>();
      if (current.CustomerID.HasValue)
        view.WhereAnd<Where<Customer.bAccountID, Equal<Current<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.customerID>>>>();
      foreach (PXResult<CCProcTran, CustomerPaymentMethod, Customer, ARPayment> pxResult in view.SelectWithViewContext())
      {
        ARPayment arPayment = PXResult<CCProcTran, CustomerPaymentMethod, Customer, ARPayment>.op_Implicit(pxResult);
        if (EnumerableExtensions.IsIn<string>(PXResult<CCProcTran, CustomerPaymentMethod, Customer, ARPayment>.op_Implicit(pxResult).TranType, "CDT", "VDG", "REJ"))
        {
          if (PXResultset<ARPayment>.op_Implicit(PXSelectBase<ARPayment, PXSelect<ARPayment, Where<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>, And<ARPayment.docType, Equal<ARDocType.voidPayment>>>>.Config>.Select((PXGraph) transactionsHistoryEnq, new object[1]
          {
            (object) arPayment.RefNbr
          })) != null)
          {
            PXResult<CCProcTran, CustomerPaymentMethod, Customer, ARPayment>.op_Implicit(pxResult).DocType = "RPM";
            PXResult<CCProcTran, CustomerPaymentMethod, Customer, ARPayment>.op_Implicit(pxResult).DocType = "RPM";
          }
        }
        yield return (object) pxResult;
      }
    }
  }

  private void GetInternalData(string paymentMethodID)
  {
    PaymentMethodDetail paymentMethodDetail1 = PXResultset<PaymentMethodDetail>.op_Implicit(PXSelectBase<PaymentMethodDetail, PXSelectReadonly<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>, And<PaymentMethodDetail.isIdentifier, Equal<True>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) paymentMethodID
    }));
    if (paymentMethodDetail1 != null)
      this.CardNumber_DetailID = paymentMethodDetail1.DetailID;
    PaymentMethodDetail paymentMethodDetail2 = PXResultset<PaymentMethodDetail>.op_Implicit(PXSelectBase<PaymentMethodDetail, PXSelectReadonly<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>, And<PaymentMethodDetail.isOwnerName, Equal<True>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) paymentMethodID
    }));
    if (paymentMethodDetail2 == null)
      return;
    this.NameOnCard_DetailID = paymentMethodDetail2.DetailID;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewDocument(PXAdapter adapter)
  {
    CCProcTran current = ((PXSelectBase<CCProcTran>) this.CCTrans).Current;
    if (current != null)
    {
      PXGraph sourceDocumentGraph = CCTransactionsHistoryEnq.FindSourceDocumentGraph(current.DocType, current.RefNbr, current.OrigDocType, current.OrigRefNbr);
      if (sourceDocumentGraph != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException(sourceDocumentGraph, true, "View Document");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewCustomer(PXAdapter adapter)
  {
    if (((PXSelectBase<CCProcTran>) this.CCTrans).Current != null)
    {
      CustomerPaymentMethod customerPaymentMethod = PXResultset<CustomerPaymentMethod>.op_Implicit(PXSelectBase<CustomerPaymentMethod, PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.pMInstanceID, Equal<Required<CustomerPaymentMethod.pMInstanceID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<CCProcTran>) this.CCTrans).Current.PMInstanceID
      }));
      if (customerPaymentMethod != null)
      {
        CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
        ((PXSelectBase<Customer>) instance.BAccount).Current = PXResultset<Customer>.op_Implicit(((PXSelectBase<Customer>) instance.BAccount).Search<Customer.bAccountID>((object) customerPaymentMethod.BAccountID, Array.Empty<object>()));
        if (((PXSelectBase<Customer>) instance.BAccount).Current != null)
        {
          PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Customer");
          ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
          throw requiredException;
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewPaymentMethod(PXAdapter adapter)
  {
    if (((PXSelectBase<CCProcTran>) this.CCTrans).Current != null)
    {
      CustomerPaymentMethod customerPaymentMethod = PXResultset<CustomerPaymentMethod>.op_Implicit(PXSelectBase<CustomerPaymentMethod, PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.pMInstanceID, Equal<Required<CustomerPaymentMethod.pMInstanceID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<CCProcTran>) this.CCTrans).Current.PMInstanceID
      }));
      CustomerPaymentMethodMaint instance = PXGraph.CreateInstance<CustomerPaymentMethodMaint>();
      ((PXSelectBase<CustomerPaymentMethod>) instance.CustomerPaymentMethod).Current = customerPaymentMethod;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Payment Method");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  public virtual void CCTransactionsHistoryFilter_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    CCTransactionsHistoryEnq.CCTransactionsHistoryFilter row = (CCTransactionsHistoryEnq.CCTransactionsHistoryFilter) e.Row;
    PXUIFieldAttribute.SetEnabled<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.pMInstanceID>(sender, e.Row, !string.IsNullOrEmpty(row.PaymentMethodID));
    PXUIFieldAttribute.SetEnabled<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.customerID>(sender, e.Row, !string.IsNullOrEmpty(row.PaymentMethodID));
    PXUIFieldAttribute.SetEnabled<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.cardType>(sender, e.Row, !string.IsNullOrEmpty(row.PaymentMethodID));
  }

  public virtual void CCTransactionsHistoryFilter_PaymentMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CCTransactionsHistoryEnq.CCTransactionsHistoryFilter row = (CCTransactionsHistoryEnq.CCTransactionsHistoryFilter) e.Row;
    row.PMInstanceID = new int?();
    row.CardType = (string) null;
  }

  public virtual void CCTransactionsHistoryFilter_CardType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((CCTransactionsHistoryEnq.CCTransactionsHistoryFilter) e.Row).PMInstanceID = new int?();
  }

  public virtual void CCTransactionsHistoryFilter_CustomerID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((CCTransactionsHistoryEnq.CCTransactionsHistoryFilter) e.Row).PMInstanceID = new int?();
  }

  public static PXGraph FindSourceDocumentGraph(
    string docType,
    string refNbr,
    string origDocType,
    string origRefNbr)
  {
    PXGraph sourceDocumentGraph = (PXGraph) null;
    if (docType == "PMT" || docType == "PPM" || docType == "RPM" || docType == "REF")
    {
      ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
      ((PXSelectBase<ARPayment>) instance.Document).Current = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) instance.Document).Search<ARPayment.refNbr>((object) refNbr, new object[1]
      {
        (object) docType
      }));
      if (((PXSelectBase<ARPayment>) instance.Document).Current != null)
        sourceDocumentGraph = (PXGraph) instance;
    }
    if (docType == "CSL")
    {
      ARCashSaleEntry instance = PXGraph.CreateInstance<ARCashSaleEntry>();
      ((PXSelectBase<ARCashSale>) instance.Document).Current = PXResultset<ARCashSale>.op_Implicit(((PXSelectBase<ARCashSale>) instance.Document).Search<ARCashSale.refNbr>((object) refNbr, new object[1]
      {
        (object) docType
      }));
      if (((PXSelectBase<ARCashSale>) instance.Document).Current != null)
        sourceDocumentGraph = (PXGraph) instance;
    }
    if (docType == "INV")
    {
      SOInvoiceEntry instance = PXGraph.CreateInstance<SOInvoiceEntry>();
      ((PXSelectBase<ARInvoice>) instance.Document).Current = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) instance.Document).Search<ARInvoice.refNbr>((object) refNbr, new object[1]
      {
        (object) docType
      }));
      if (((PXSelectBase<ARInvoice>) instance.Document).Current != null)
        sourceDocumentGraph = (PXGraph) instance;
    }
    if (sourceDocumentGraph == null && !string.IsNullOrEmpty(origRefNbr))
    {
      SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
      ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) origRefNbr, new object[1]
      {
        (object) origDocType
      }));
      if (((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current != null)
        sourceDocumentGraph = (PXGraph) instance;
    }
    return sourceDocumentGraph;
  }

  public static class CardSearchOption
  {
    public const string PartialNumber = "P";
    public const string FullNumber = "F";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "P", "F" }, new string[2]
        {
          "Search by Partial Number",
          "Search by Full Number"
        })
      {
      }
    }
  }

  [Serializable]
  public class CCTransactionsHistoryFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _PaymentMethodID;
    protected DateTime? _StartDate;
    protected DateTime? _EndDate;
    protected Decimal? _Amount;
    protected string _ProcessingCenterID;

    [PXDBString(10, IsUnicode = true)]
    [PXDefault]
    [PXUIField(DisplayName = "Payment Method")]
    [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.paymentType, Equal<PaymentMethodType.creditCard>, Or<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<PaymentMethodType.eft>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
    public virtual string PaymentMethodID
    {
      get => this._PaymentMethodID;
      set => this._PaymentMethodID = value;
    }

    [PXDBString(3, IsFixed = true)]
    [PXUIField(DisplayName = "Card/Account Type")]
    [CardType.ListWithBlankItem]
    public virtual string CardType { get; set; }

    /// <summary>
    /// The identifier of a customer for which the thansaction history must be retrieved.
    /// </summary>
    [PXUIField(DisplayName = "Customer")]
    [Customer]
    public virtual int? CustomerID { get; set; }

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Start Date")]
    public virtual DateTime? StartDate
    {
      get => this._StartDate;
      set => this._StartDate = value;
    }

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "End Date")]
    public virtual DateTime? EndDate
    {
      get => this._EndDate;
      set => this._EndDate = value;
    }

    [PXDBDate]
    public virtual DateTime? EndDatePlusOne
    {
      get
      {
        return this._EndDate.HasValue ? new DateTime?(this._EndDate.Value.AddDays(1.0)) : this._EndDate;
      }
      set
      {
      }
    }

    [PXDBDecimal(4)]
    [PXUIField(DisplayName = "Amount")]
    public virtual Decimal? Amount
    {
      get => this._Amount;
      set => this._Amount = value;
    }

    [PXDBString(10, IsUnicode = true)]
    [PXSelector(typeof (Search<CCProcessingCenter.processingCenterID>))]
    [PXUIField(DisplayName = "Proc. Center ID")]
    public virtual string ProcessingCenterID
    {
      get => this._ProcessingCenterID;
      set => this._ProcessingCenterID = value;
    }

    /// <summary>
    /// The identifier of a customer payment method for which the thansaction history must be retrieved.
    /// </summary>
    [PXDBInt]
    [PXSelector(typeof (Search2<CustomerPaymentMethod.pMInstanceID, InnerJoin<Customer, On<Customer.bAccountID, Equal<CustomerPaymentMethod.bAccountID>>>, Where<CustomerPaymentMethod.paymentMethodID, Equal<Current<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.paymentMethodID>>, And<CustomerPaymentMethod.isActive, Equal<True>, And2<Where<CustomerPaymentMethod.cCProcessingCenterID, Equal<Current<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.processingCenterID>>, Or<Current<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.processingCenterID>, IsNull>>, And2<Where<Customer.bAccountID, Equal<Current<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.customerID>>, Or<Current<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.customerID>, IsNull>>, And<Where<CustomerPaymentMethod.cardType, Equal<Current<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.cardType>>, Or<Current<CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.cardType>, IsNull>>>>>>>>), new Type[] {typeof (CustomerPaymentMethod.descr), typeof (Customer.acctCD), typeof (Customer.acctName)})]
    [PXUIField(DisplayName = "Card/Account Nbr.")]
    public virtual int? PMInstanceID { get; set; }

    public abstract class paymentMethodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.paymentMethodID>
    {
    }

    public abstract class cardType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.cardType>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.customerID>
    {
    }

    public abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.startDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.endDate>
    {
    }

    public abstract class endDatePlusOne : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.endDatePlusOne>
    {
    }

    public abstract class amount : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.amount>
    {
    }

    public abstract class processingCenterID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.processingCenterID>
    {
    }

    public abstract class pMInstanceID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CCTransactionsHistoryEnq.CCTransactionsHistoryFilter.pMInstanceID>
    {
    }
  }
}
