// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.FailedCCPaymentEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CA;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
[Serializable]
public class FailedCCPaymentEnq : PXGraph<
#nullable disable
FailedCCPaymentEnq>
{
  public PXFilter<FailedCCPaymentEnq.CCPaymentFilter> Filter;
  public PXCancel<FailedCCPaymentEnq.CCPaymentFilter> Cancel;
  [PXFilterable(new Type[] {})]
  public PXSelectJoinOrderBy<CCProcTran, LeftJoin<ARPayment, On<CCProcTran.docType, Equal<ARPayment.docType>, And<CCProcTran.refNbr, Equal<ARPayment.refNbr>>>, LeftJoin<Customer, On<ARPayment.customerID, Equal<Customer.bAccountID>>, LeftJoin<ExternalTransaction, On<CCProcTran.transactionID, Equal<ExternalTransaction.transactionID>>, LeftJoin<CustomerPaymentMethod, On<CCProcTran.pMInstanceID, Equal<CustomerPaymentMethod.pMInstanceID>>>>>>, OrderBy<Desc<CCProcTran.refNbr>>> PaymentTrans;
  public PXSelectJoin<CCProcTran, InnerJoin<CustomerPaymentMethod, On<CustomerPaymentMethod.pMInstanceID, Equal<CCProcTran.pMInstanceID>>, LeftJoin<ARPayment, On<CCProcTran.refNbr, Equal<ARPayment.refNbr>, And<CCProcTran.docType, Equal<ARPayment.docType>>>, InnerJoin<Customer, On<Customer.bAccountID, Equal<CustomerPaymentMethod.bAccountID>>, LeftJoin<ExternalTransaction, On<ExternalTransaction.transactionID, Equal<CCProcTran.transactionID>>>>>>, Where<CCProcTran.startTime, GreaterEqual<Required<FailedCCPaymentEnq.CCPaymentFilter.beginDate>>, And<CCProcTran.startTime, LessEqual<Required<FailedCCPaymentEnq.CCPaymentFilter.endDate>>>>> CpmExists;
  public PXSelectJoin<CCProcTran, LeftJoin<CustomerPaymentMethod, On<CustomerPaymentMethod.pMInstanceID, Equal<CCProcTran.pMInstanceID>>, InnerJoin<ARPayment, On<CCProcTran.refNbr, Equal<ARPayment.refNbr>, And<CCProcTran.docType, Equal<ARPayment.docType>>>, InnerJoin<Customer, On<Customer.bAccountID, Equal<ARPayment.customerID>>, LeftJoin<ExternalTransaction, On<ExternalTransaction.transactionID, Equal<CCProcTran.transactionID>>>>>>, Where<CCProcTran.startTime, GreaterEqual<Required<FailedCCPaymentEnq.CCPaymentFilter.beginDate>>, And<CCProcTran.startTime, LessEqual<Required<FailedCCPaymentEnq.CCPaymentFilter.endDate>>, And<CCProcTran.pMInstanceID, IsNull>>>> CpmNotExists;
  public PXAction<FailedCCPaymentEnq.CCPaymentFilter> ViewDocument;
  public PXAction<FailedCCPaymentEnq.CCPaymentFilter> ViewCustomer;
  public PXAction<FailedCCPaymentEnq.CCPaymentFilter> ViewPaymentMethod;
  public PXAction<FailedCCPaymentEnq.CCPaymentFilter> ViewExternalTransaction;

  public FailedCCPaymentEnq()
  {
    ((PXSelectBase) this.PaymentTrans).Cache.AllowInsert = false;
    ((PXSelectBase) this.PaymentTrans).Cache.AllowUpdate = false;
    ((PXSelectBase) this.PaymentTrans).Cache.AllowDelete = false;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewDocument(PXAdapter adapter)
  {
    CCProcTran current = ((PXSelectBase<CCProcTran>) this.PaymentTrans).Current;
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
    return (IEnumerable) ((PXSelectBase<FailedCCPaymentEnq.CCPaymentFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewCustomer(PXAdapter adapter)
  {
    if (((PXSelectBase<CCProcTran>) this.PaymentTrans).Current != null)
    {
      CCProcTran current = ((PXSelectBase<CCProcTran>) this.PaymentTrans).Current;
      ARPayment arPayment = PXResultset<ARPayment>.op_Implicit(PXSelectBase<ARPayment, PXSelect<ARPayment, Where<ARPayment.docType, Equal<Required<ARPayment.docType>>, And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) current.DocType,
        (object) current.RefNbr
      }));
      if (arPayment != null)
      {
        CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
        ((PXSelectBase<Customer>) instance.BAccount).Current = PXResultset<Customer>.op_Implicit(((PXSelectBase<Customer>) instance.BAccount).Search<Customer.bAccountID>((object) arPayment.CustomerID, Array.Empty<object>()));
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
    if (((PXSelectBase<CCProcTran>) this.PaymentTrans).Current != null)
    {
      CCProcTran current = ((PXSelectBase<CCProcTran>) this.PaymentTrans).Current;
      CustomerPaymentMethod customerPaymentMethod = PXResultset<CustomerPaymentMethod>.op_Implicit(PXSelectBase<CustomerPaymentMethod, PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.pMInstanceID, Equal<Required<CustomerPaymentMethod.pMInstanceID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) current.PMInstanceID
      }));
      if (customerPaymentMethod != null)
      {
        CustomerPaymentMethodMaint instance = PXGraph.CreateInstance<CustomerPaymentMethodMaint>();
        ((PXSelectBase<CustomerPaymentMethod>) instance.CustomerPaymentMethod).Current = PXResultset<CustomerPaymentMethod>.op_Implicit(((PXSelectBase<CustomerPaymentMethod>) instance.CustomerPaymentMethod).Search<CustomerPaymentMethod.pMInstanceID>((object) customerPaymentMethod.PMInstanceID, new object[1]
        {
          (object) customerPaymentMethod.BAccountID
        }));
        if (((PXSelectBase<CustomerPaymentMethod>) instance.CustomerPaymentMethod).Current != null)
        {
          PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Payment Method");
          ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
          throw requiredException;
        }
      }
      else
      {
        PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) PXResultset<ARPayment>.op_Implicit(PXSelectBase<ARPayment, PXSelect<ARPayment, Where<ARPayment.docType, Equal<Required<ARPayment.docType>>, And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) current.DocType,
            (object) current.RefNbr
          })).PaymentMethodID
        }));
        PaymentMethodMaint instance = PXGraph.CreateInstance<PaymentMethodMaint>();
        ((PXSelectBase<PX.Objects.CA.PaymentMethod>) instance.PaymentMethod).Current = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) instance.PaymentMethod).Search<PX.Objects.CA.PaymentMethod.paymentMethodID>((object) paymentMethod.PaymentMethodID, Array.Empty<object>()));
        if (((PXSelectBase<PX.Objects.CA.PaymentMethod>) instance.PaymentMethod).Current != null)
        {
          PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Payment Method");
          ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
          throw requiredException;
        }
      }
    }
    return (IEnumerable) ((PXSelectBase<FailedCCPaymentEnq.CCPaymentFilter>) this.Filter).Select(Array.Empty<object>());
  }

  public virtual IEnumerable paymentTrans()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultSorted = false
    };
    FailedCCPaymentEnq.CCPaymentFilter current = ((PXSelectBase<FailedCCPaymentEnq.CCPaymentFilter>) this.Filter).Current;
    if (current != null)
    {
      PXView pxView1 = this.ApplyFilters((PXSelectBase<CCProcTran>) this.CpmExists);
      PXView pxView2 = this.ApplyFilters((PXSelectBase<CCProcTran>) this.CpmNotExists);
      if (current.EndDate.HasValue && pxView1 != null && pxView2 != null)
      {
        ((PXSelectBase) this.PaymentTrans).Cache.ClearQueryCache();
        List<object> objectList = pxView1.SelectMulti(new object[2]
        {
          (object) current.BeginDate,
          (object) current.EndDate.Value.AddDays(1.0)
        });
        objectList.AddRange((IEnumerable<object>) pxView2.SelectMulti(new object[2]
        {
          (object) current.BeginDate,
          (object) current.EndDate.Value.AddDays(1.0)
        }));
        foreach (PXResult<CCProcTran, CustomerPaymentMethod, ARPayment, Customer, ExternalTransaction> pxResult in objectList)
          ((List<object>) pxDelegateResult).Add((object) pxResult);
      }
    }
    return (IEnumerable) pxDelegateResult;
  }

  protected virtual void CCPaymentFilter_BeginDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    FailedCCPaymentEnq.CCPaymentFilter row = (FailedCCPaymentEnq.CCPaymentFilter) e.Row;
    if (!row.BeginDate.HasValue)
      return;
    DateTime? nullable = row.EndDate;
    if (!nullable.HasValue)
      return;
    nullable = row.EndDate;
    DateTime dateTime1 = nullable.Value;
    nullable = row.BeginDate;
    DateTime dateTime2 = nullable.Value;
    if (!(dateTime1 < dateTime2))
      return;
    row.EndDate = row.BeginDate;
  }

  protected virtual void CCPaymentFilter_EndDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    FailedCCPaymentEnq.CCPaymentFilter row = (FailedCCPaymentEnq.CCPaymentFilter) e.Row;
    if (!row.BeginDate.HasValue)
      return;
    DateTime? nullable = row.EndDate;
    if (!nullable.HasValue)
      return;
    nullable = row.EndDate;
    DateTime dateTime1 = nullable.Value;
    nullable = row.BeginDate;
    DateTime dateTime2 = nullable.Value;
    if (!(dateTime1 < dateTime2))
      return;
    row.BeginDate = row.EndDate;
  }

  private PXView ApplyFilters(PXSelectBase<CCProcTran> query)
  {
    FailedCCPaymentEnq.CCPaymentFilter current = ((PXSelectBase<FailedCCPaymentEnq.CCPaymentFilter>) this.Filter).Current;
    PXView pxView = (PXView) null;
    BqlCommand bqlCommand = ((PXSelectBase) query).View.BqlSelect;
    if (bqlCommand != null)
    {
      if (!string.IsNullOrEmpty(current.ProcessingCenterID))
        bqlCommand = bqlCommand.WhereAnd<Where<CCProcTran.processingCenterID, Equal<Current<FailedCCPaymentEnq.CCPaymentFilter.processingCenterID>>>>();
      if (!string.IsNullOrEmpty(current.CustomerClassID))
        bqlCommand = bqlCommand.WhereAnd<Where<Customer.customerClassID, Equal<Current<FailedCCPaymentEnq.CCPaymentFilter.customerClassID>>>>();
      if (current.CustomerID.HasValue)
        bqlCommand = bqlCommand.WhereAnd<Where<Customer.bAccountID, Equal<Current<FailedCCPaymentEnq.CCPaymentFilter.customerID>>>>();
      if (current.DisplayType == "FLD")
        bqlCommand = bqlCommand.WhereAnd<Where<CCProcTran.tranStatus, NotEqual<CCTranStatusCode.approved>, Or<CCProcTran.tranStatus, IsNull>>>();
      pxView = new PXView((PXGraph) this, false, bqlCommand);
    }
    return pxView;
  }

  [Serializable]
  public class CCPaymentFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _BeginDate;
    protected DateTime? _EndDate;
    protected string _CustomerClassID;
    protected string _ProcessingCenterID;
    protected int? _CustomerID;
    protected string _DisplayType;

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Start Date")]
    public virtual DateTime? BeginDate
    {
      get => this._BeginDate;
      set => this._BeginDate = value;
    }

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "End Date")]
    public virtual DateTime? EndDate
    {
      get => this._EndDate;
      set => this._EndDate = value;
    }

    [PXDBString(10, IsUnicode = true)]
    [PXSelector(typeof (CustomerClass.customerClassID), DescriptionField = typeof (CustomerClass.descr), CacheGlobal = true)]
    [PXUIField(DisplayName = "Customer Class")]
    public virtual string CustomerClassID
    {
      get => this._CustomerClassID;
      set => this._CustomerClassID = value;
    }

    [PXDBString(10, IsUnicode = true)]
    [PXSelector(typeof (Search<CCProcessingCenter.processingCenterID>), DescriptionField = typeof (CCProcessingCenter.name))]
    [PXUIField]
    public virtual string ProcessingCenterID
    {
      get => this._ProcessingCenterID;
      set => this._ProcessingCenterID = value;
    }

    [Customer(DescriptionField = typeof (Customer.acctName))]
    public virtual int? CustomerID
    {
      get => this._CustomerID;
      set => this._CustomerID = value;
    }

    [PXDBString(3, IsFixed = true, IsUnicode = false)]
    [FailedCCPaymentEnq.DisplayTypes.List]
    [PXDefault("ALL")]
    [PXUIField(DisplayName = "Display Transactions")]
    public virtual string DisplayType
    {
      get => this._DisplayType;
      set => this._DisplayType = value;
    }

    public abstract class beginDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      FailedCCPaymentEnq.CCPaymentFilter.beginDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      FailedCCPaymentEnq.CCPaymentFilter.endDate>
    {
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FailedCCPaymentEnq.CCPaymentFilter.customerClassID>
    {
    }

    public abstract class processingCenterID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FailedCCPaymentEnq.CCPaymentFilter.processingCenterID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FailedCCPaymentEnq.CCPaymentFilter.customerID>
    {
    }

    public abstract class displayType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FailedCCPaymentEnq.CCPaymentFilter.displayType>
    {
    }
  }

  private static class DisplayTypes
  {
    public const string All = "ALL";
    public const string Failed = "FLD";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "ALL", "FLD" }, new string[2]
        {
          "All Transactions",
          "Failed Only"
        })
      {
      }
    }
  }
}
