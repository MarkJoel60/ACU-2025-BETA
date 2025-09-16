// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCustomerCreditHoldProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlTypes;

#nullable enable
namespace PX.Objects.AR;

public class ARCustomerCreditHoldProcess : PXGraph<
#nullable disable
ARCustomerCreditHoldProcess>
{
  public PXCancel<ARCustomerCreditHoldProcess.CreditHoldParameters> Cancel;
  public PXFilter<ARCustomerCreditHoldProcess.CreditHoldParameters> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<ARCustomerCreditHoldProcess.DetailsResult, ARCustomerCreditHoldProcess.CreditHoldParameters> Details;

  public ARCustomerCreditHoldProcess()
  {
    ((PXSelectBase) this.Details).Cache.AllowDelete = false;
    ((PXSelectBase) this.Details).Cache.AllowInsert = false;
    ((PXProcessingBase<ARCustomerCreditHoldProcess.DetailsResult>) this.Details).SetSelected<ARInvoice.selected>();
    ((PXProcessing<ARCustomerCreditHoldProcess.DetailsResult>) this.Details).SetProcessCaption("Process");
    ((PXProcessing<ARCustomerCreditHoldProcess.DetailsResult>) this.Details).SetProcessAllCaption("Process All");
  }

  /// <summary>
  /// Generates a list of documents that meet the filter criteria.
  /// This list is used for display in the processing screen
  /// </summary>
  /// <returns>List of Customers with Dunning Letters</returns>
  protected virtual IEnumerable details()
  {
    ARCustomerCreditHoldProcess creditHoldProcess = this;
    ARCustomerCreditHoldProcess.CreditHoldParameters current = ((PXSelectBase<ARCustomerCreditHoldProcess.CreditHoldParameters>) creditHoldProcess.Filter).Current;
    if (current != null)
    {
      foreach (PXResult<Customer, ARDunningLetter> pxResult in creditHoldProcess.GetCustomersToProcess(current))
      {
        ARDunningLetter aSrc = PXResult<Customer, ARDunningLetter>.op_Implicit(pxResult);
        Customer cust = PXResult<Customer, ARDunningLetter>.op_Implicit(pxResult);
        ARCustomerCreditHoldProcess.DetailsResult detailsResult1 = new ARCustomerCreditHoldProcess.DetailsResult()
        {
          CustomerId = cust.BAccountID
        };
        ARCustomerCreditHoldProcess.DetailsResult detailsResult2 = (ARCustomerCreditHoldProcess.DetailsResult) ((PXSelectBase) creditHoldProcess.Details).Cache.Locate((object) detailsResult1);
        if (detailsResult2 == null)
          ((PXSelectBase) creditHoldProcess.Details).Cache.SetStatus((object) detailsResult1, (PXEntryStatus) 5);
        else
          detailsResult1 = detailsResult2;
        detailsResult1.Copy((PXGraph) creditHoldProcess, aSrc, cust);
        ARBalances customerBalances = CustomerMaint.GetCustomerBalances<PX.Objects.AR.Override.Customer.sharedCreditCustomerID>((PXGraph) creditHoldProcess, cust.BAccountID);
        if (customerBalances != null)
          detailsResult1.InvBal = new Decimal?(customerBalances.CurrentBal ?? 0.0M);
        yield return (object) detailsResult1;
      }
      ((PXSelectBase) creditHoldProcess.Details).Cache.IsDirty = false;
    }
  }

  protected virtual PXResultset<Customer> GetCustomersToProcess(
    ARCustomerCreditHoldProcess.CreditHoldParameters header)
  {
    int? action = header.Action;
    if (action.HasValue)
    {
      switch (action.GetValueOrDefault())
      {
        case 0:
          object[] objArray = new object[2];
          DateTime? nullable = header.BeginDate;
          SqlDateTime sqlDateTime;
          DateTime valueOrDefault1;
          if (!nullable.HasValue)
          {
            sqlDateTime = SqlDateTime.MinValue;
            valueOrDefault1 = sqlDateTime.Value;
          }
          else
            valueOrDefault1 = nullable.GetValueOrDefault();
          objArray[0] = (object) valueOrDefault1;
          nullable = header.EndDate;
          DateTime valueOrDefault2;
          if (!nullable.HasValue)
          {
            sqlDateTime = SqlDateTime.MaxValue;
            valueOrDefault2 = sqlDateTime.Value;
          }
          else
            valueOrDefault2 = nullable.GetValueOrDefault();
          objArray[1] = (object) valueOrDefault2;
          return PXSelectBase<Customer, PXSelectJoin<Customer, InnerJoin<ARDunningLetter, On<Customer.bAccountID, Equal<ARDunningLetter.bAccountID>, And<ARDunningLetter.lastLevel, Equal<True>, And<ARDunningLetter.released, Equal<True>, And<ARDunningLetter.voided, NotEqual<True>>>>>>, Where<ARDunningLetter.dunningLetterDate, Between<Required<ARDunningLetter.dunningLetterDate>, Required<ARDunningLetter.dunningLetterDate>>, And<BqlOperand<Customer.status, IBqlString>.IsIn<CustomerStatus.active, CustomerStatus.oneTime>>>, OrderBy<Asc<ARDunningLetter.bAccountID>>>.Config>.Select((PXGraph) this, objArray);
        case 1:
          PXSelectBase<Customer> pxSelectBase = (PXSelectBase<Customer>) new PXSelectJoin<Customer, LeftJoin<ARDunningLetter, On<Customer.bAccountID, Equal<ARDunningLetter.bAccountID>, And<ARDunningLetter.lastLevel, Equal<True>, And<ARDunningLetter.released, Equal<True>, And<ARDunningLetter.voided, NotEqual<True>>>>>>, Where<BqlOperand<Customer.status, IBqlString>.IsEqual<CustomerStatus.creditHold>>>((PXGraph) this);
          if (PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>())
            pxSelectBase.WhereAnd<Where<Customer.bAccountID, Equal<Customer.sharedCreditCustomerID>>>();
          return pxSelectBase.Select(Array.Empty<object>());
      }
    }
    return new PXResultset<Customer>();
  }

  protected void _(
    Events.FieldUpdated<ARCustomerCreditHoldProcess.CreditHoldParameters, ARCustomerCreditHoldProcess.CreditHoldParameters.action> e)
  {
    int? action = e.Row.Action;
    int num = 0;
    if (!(action.GetValueOrDefault() == num & action.HasValue))
      return;
    e.Row.BeginDate = new DateTime?();
    e.Row.EndDate = new DateTime?();
  }

  protected virtual void CreditHoldParameters_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARCustomerCreditHoldProcess.\u003C\u003Ec__DisplayClass9_0 cDisplayClass90 = new ARCustomerCreditHoldProcess.\u003C\u003Ec__DisplayClass9_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.header = e.Row as ARCustomerCreditHoldProcess.CreditHoldParameters;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass90.header == null)
      return;
    // ISSUE: method pointer
    ((PXProcessingBase<ARCustomerCreditHoldProcess.DetailsResult>) this.Details).SetProcessDelegate<CustomerMaint>(new PXProcessingBase<ARCustomerCreditHoldProcess.DetailsResult>.ProcessItemDelegate<CustomerMaint>((object) cDisplayClass90, __methodptr(\u003CCreditHoldParameters_RowSelected\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    int? action = cDisplayClass90.header.Action;
    int num = 0;
    bool flag = action.GetValueOrDefault() == num & action.HasValue;
    // ISSUE: reference to a compiler-generated field
    PXUIFieldAttribute.SetVisible<ARCustomerCreditHoldProcess.CreditHoldParameters.beginDate>(sender, (object) cDisplayClass90.header, flag);
    // ISSUE: reference to a compiler-generated field
    PXUIFieldAttribute.SetVisible<ARCustomerCreditHoldProcess.CreditHoldParameters.endDate>(sender, (object) cDisplayClass90.header, flag);
  }

  protected virtual void CreditHoldParameters_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase) this.Details).Cache.Clear();
  }

  protected virtual void CreditHoldParameters_RowPersisting(
    PXCache sedner,
    PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  [Serializable]
  public class CreditHoldParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _Action;
    public const int ActionApplyCreditHold = 0;
    public const int ActionReleaseCreditHold = 1;
    protected DateTime? _BeginDate;
    protected DateTime? _EndDate;

    [PXDBInt]
    [PXDefault(0)]
    [PXUIField(DisplayName = "Action")]
    [PXIntList(new int[] {0, 1}, new string[] {"Credit Hold", "Remove Credit Hold"})]
    public virtual int? Action
    {
      get => this._Action;
      set => this._Action = value;
    }

    [PXDate]
    [PXUIField]
    public virtual DateTime? BeginDate
    {
      get => this._BeginDate;
      set => this._BeginDate = value;
    }

    [PXDate]
    [PXUIField]
    public virtual DateTime? EndDate
    {
      get => this._EndDate;
      set => this._EndDate = value;
    }

    public abstract class action : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerCreditHoldProcess.CreditHoldParameters.action>
    {
    }

    public abstract class beginDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARCustomerCreditHoldProcess.CreditHoldParameters.beginDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARCustomerCreditHoldProcess.CreditHoldParameters.endDate>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class DetailsResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected bool? _Selected = new bool?(false);
    protected int? _DunningLetterID;
    protected int? _CustomerId;
    protected DateTime? _DunningLetterDate;
    protected Decimal? _DocBal;
    protected Decimal? _InvBal;
    protected string _Status;

    [PXBool]
    [PXUnboundDefault(false)]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected
    {
      get => this._Selected;
      set => this._Selected = value;
    }

    [PXInt]
    [PXUIField(Enabled = false)]
    public virtual int? DunningLetterID
    {
      get => this._DunningLetterID;
      set => this._DunningLetterID = value;
    }

    [Customer(DescriptionField = typeof (Customer.acctName), IsKey = true)]
    [PXUIField]
    public virtual int? CustomerId
    {
      get => this._CustomerId;
      set => this._CustomerId = value;
    }

    [PXDBDate]
    [PXDefault(TypeCode.DateTime, "01/01/1900")]
    [PXUIField(DisplayName = "Dunning Letter Date")]
    public virtual DateTime? DunningLetterDate
    {
      get => this._DunningLetterDate;
      set => this._DunningLetterDate = value;
    }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Overdue Balance")]
    public virtual Decimal? DocBal
    {
      get => this._DocBal;
      set => this._DocBal = value;
    }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Customer Balance")]
    public virtual Decimal? InvBal
    {
      get => this._InvBal;
      set => this._InvBal = value;
    }

    [PXUIField(DisplayName = "Customer Status")]
    [CustomerStatus.List]
    public virtual string Status
    {
      get => this._Status;
      set => this._Status = value;
    }

    /// <summary>
    /// Copy field values from selection and aggregate details
    /// </summary>
    /// <param name="G">Graph</param>
    /// <param name="aSrc">Selected DunningLetter</param>
    /// <param name="cust">Selected Customer</param>
    public virtual void Copy(PXGraph G, ARDunningLetter aSrc, Customer cust)
    {
      this.CustomerId = cust.BAccountID;
      this.DunningLetterID = aSrc.DunningLetterID;
      this.DunningLetterDate = aSrc.DunningLetterDate;
      this.DocBal = new Decimal?(0M);
      this.InvBal = new Decimal?(0M);
      this.Status = cust.Status;
      foreach (PXResult<ARDunningLetterDetail> pxResult in PXSelectBase<ARDunningLetterDetail, PXSelect<ARDunningLetterDetail, Where<ARDunningLetterDetail.dunningLetterID, Equal<Required<ARDunningLetterDetail.dunningLetterID>>>>.Config>.Select(G, new object[1]
      {
        (object) aSrc.DunningLetterID
      }))
      {
        ARDunningLetterDetail dunningLetterDetail = PXResult<ARDunningLetterDetail>.op_Implicit(pxResult);
        Decimal? docBal = this.DocBal;
        Decimal? overdueBal = dunningLetterDetail.OverdueBal;
        this.DocBal = docBal.HasValue & overdueBal.HasValue ? new Decimal?(docBal.GetValueOrDefault() + overdueBal.GetValueOrDefault()) : new Decimal?();
      }
    }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARCustomerCreditHoldProcess.DetailsResult.selected>
    {
    }

    public abstract class dunningLetterID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerCreditHoldProcess.DetailsResult.dunningLetterID>
    {
    }

    public abstract class customerId : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerCreditHoldProcess.DetailsResult.customerId>
    {
    }

    public abstract class dunningLetterDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARCustomerCreditHoldProcess.DetailsResult.dunningLetterDate>
    {
    }

    public abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerCreditHoldProcess.DetailsResult.docBal>
    {
    }

    public abstract class invBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerCreditHoldProcess.DetailsResult.invBal>
    {
    }

    public abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARCustomerCreditHoldProcess.DetailsResult.status>
    {
    }
  }
}
