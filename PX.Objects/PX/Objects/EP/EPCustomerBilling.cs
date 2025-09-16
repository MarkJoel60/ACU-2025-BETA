// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPCustomerBilling
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.AR.MigrationMode;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.GL;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.EP;

[TableDashboardType]
public class EPCustomerBilling : PXGraph<
#nullable disable
EPCustomerBilling>
{
  public PXCancel<EPCustomerBilling.BillingFilter> Cancel;
  public PXFilter<EPCustomerBilling.BillingFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingOrderBy<CustomersList, EPCustomerBilling.BillingFilter, OrderBy<Asc<CustomersList.customerID, Asc<CustomersList.locationID>>>> Customers;
  public PXSelectJoinGroupBy<EPExpenseClaimDetails, InnerJoin<PX.Objects.AR.Customer, On<EPExpenseClaimDetails.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>, LeftJoin<Contract, On<EPExpenseClaimDetails.contractID, Equal<Contract.contractID>, And<Where<Contract.baseType, Equal<CTPRType.contract>, Or<Contract.nonProject, Equal<True>>>>>>>, Where<EPExpenseClaimDetails.released, Equal<boolTrue>, And<EPExpenseClaimDetails.billable, Equal<boolTrue>, And<EPExpenseClaimDetails.billed, Equal<boolFalse>, And<EPExpenseClaimDetails.expenseDate, LessEqual<Current<EPCustomerBilling.BillingFilter.endDate>>, And<Where<EPExpenseClaimDetails.contractID, Equal<Contract.contractID>, Or<EPExpenseClaimDetails.contractID, IsNull>>>>>>>, Aggregate<GroupBy<EPExpenseClaimDetails.customerID, GroupBy<EPExpenseClaimDetails.customerLocationID>>>, OrderBy<Asc<EPExpenseClaimDetails.customerID, Asc<EPExpenseClaimDetails.customerLocationID>>>> CustomersView;

  public EPCustomerBilling()
  {
    ARSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    ((PXProcessing<CustomersList>) this.Customers).SetProcessCaption("Process");
    ((PXProcessing<CustomersList>) this.Customers).SetProcessAllCaption("Process All");
    ((PXProcessingBase<CustomersList>) this.Customers).SetSelected<CustomersList.selected>();
  }

  protected virtual IEnumerable customers()
  {
    EPCustomerBilling.BillingFilter filter = ((PXSelectBase<EPCustomerBilling.BillingFilter>) this.Filter).Current;
    if (filter != null)
    {
      bool found = false;
      foreach (CustomersList customersList in ((PXSelectBase) this.Customers).Cache.Inserted)
      {
        found = true;
        yield return (object) customersList;
      }
      if (!found)
      {
        if (filter.CustomerClassID != null)
          ((PXSelectBase<EPExpenseClaimDetails>) this.CustomersView).WhereAnd<Where<PX.Objects.AR.Customer.customerClassID, Equal<Current<EPCustomerBilling.BillingFilter.customerClassID>>>>();
        if (filter.CustomerID.HasValue)
          ((PXSelectBase<EPExpenseClaimDetails>) this.CustomersView).WhereAnd<Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<EPCustomerBilling.BillingFilter.customerID>>>>();
        foreach (PXResult<EPExpenseClaimDetails, PX.Objects.AR.Customer, Contract> pxResult in ((PXSelectBase<EPExpenseClaimDetails>) this.CustomersView).Select(Array.Empty<object>()))
        {
          CustomersList customersList = new CustomersList();
          PX.Objects.AR.Customer customer = PXResult<EPExpenseClaimDetails, PX.Objects.AR.Customer, Contract>.op_Implicit(pxResult);
          EPExpenseClaimDetails expenseClaimDetails = PXResult<EPExpenseClaimDetails, PX.Objects.AR.Customer, Contract>.op_Implicit(pxResult);
          customersList.CustomerID = customer.BAccountID;
          customersList.LocationID = expenseClaimDetails.CustomerLocationID;
          customersList.CustomerClassID = customer.CustomerClassID;
          customersList.Selected = new bool?(false);
          yield return (object) ((PXSelectBase<CustomersList>) this.Customers).Insert(customersList);
        }
      }
    }
  }

  public static void Bill(
    EPCustomerBillingProcess docgraph,
    CustomersList customer,
    EPCustomerBilling.BillingFilter filter)
  {
    docgraph.Bill(customer, filter);
  }

  protected virtual void BillingFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase) this.Customers).Cache.Clear();
  }

  protected virtual void BillingFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<CustomersList>) this.Customers).SetProcessDelegate<EPCustomerBillingProcess>(new PXProcessingBase<CustomersList>.ProcessItemDelegate<EPCustomerBillingProcess>((object) new EPCustomerBilling.\u003C\u003Ec__DisplayClass8_0()
    {
      filter = ((PXSelectBase<EPCustomerBilling.BillingFilter>) this.Filter).Current
    }, __methodptr(\u003CBillingFilter_RowSelected\u003Eb__0)));
  }

  [Serializable]
  public class BillingFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _InvoiceDate;
    protected string _InvFinPeriodID;
    protected string _CustomerClassID;
    protected int? _CustomerID;
    protected DateTime? _EndDate;

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? InvoiceDate
    {
      get => this._InvoiceDate;
      set => this._InvoiceDate = value;
    }

    [AROpenPeriod(typeof (EPCustomerBilling.BillingFilter.invoiceDate), null, null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null)]
    [PXUIField]
    public virtual string InvFinPeriodID
    {
      get => this._InvFinPeriodID;
      set => this._InvFinPeriodID = value;
    }

    [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
    [PXSelector(typeof (CustomerClass.customerClassID), DescriptionField = typeof (CustomerClass.descr), CacheGlobal = true)]
    [PXUIField(DisplayName = "Customer Class")]
    public virtual string CustomerClassID
    {
      get => this._CustomerClassID;
      set => this._CustomerClassID = value;
    }

    [Customer(DescriptionField = typeof (PX.Objects.AR.Customer.acctName))]
    public virtual int? CustomerID
    {
      get => this._CustomerID;
      set => this._CustomerID = value;
    }

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? EndDate
    {
      get => this._EndDate;
      set => this._EndDate = value;
    }

    public abstract class invoiceDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      EPCustomerBilling.BillingFilter.invoiceDate>
    {
    }

    public abstract class invFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      EPCustomerBilling.BillingFilter.invFinPeriodID>
    {
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      EPCustomerBilling.BillingFilter.customerClassID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EPCustomerBilling.BillingFilter.customerID>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      EPCustomerBilling.BillingFilter.endDate>
    {
    }
  }
}
