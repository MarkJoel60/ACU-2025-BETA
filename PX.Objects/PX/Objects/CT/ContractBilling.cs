// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractBilling
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.GL;
using PX.Objects.PM;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.CT;

[TableAndChartDashboardType]
public class ContractBilling : PXGraph<
#nullable disable
ContractBilling>
{
  public PXCancel<ContractBilling.BillingFilter> Cancel;
  [Obsolete("Will be removed in Acumatica 2019R1")]
  public PXAction<ContractBilling.BillingFilter> EditDetail;
  public PXFilter<ContractBilling.BillingFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingJoin<Contract, ContractBilling.BillingFilter, InnerJoin<ContractBillingSchedule, On<Contract.contractID, Equal<ContractBillingSchedule.contractID>>, LeftJoin<PX.Objects.AR.Customer, On<Contract.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>>, Where2<Where<ContractBillingSchedule.nextDate, LessEqual<Current<ContractBilling.BillingFilter.invoiceDate>>, Or<ContractBillingSchedule.type, Equal<BillingType.BillingOnDemand>>>, And<Contract.baseType, Equal<CTPRType.contract>, And<Contract.isCancelled, Equal<False>, And<Contract.isCompleted, Equal<False>, And<Contract.isActive, Equal<True>, And2<Where<Current<ContractBilling.BillingFilter.templateID>, IsNull, Or<Current<ContractBilling.BillingFilter.templateID>, Equal<Contract.templateID>>>, And2<Where<Current<ContractBilling.BillingFilter.customerClassID>, IsNull, Or<Current<ContractBilling.BillingFilter.customerClassID>, Equal<PX.Objects.AR.Customer.customerClassID>>>, And<Where<Current<ContractBilling.BillingFilter.customerID>, IsNull, Or<Current<ContractBilling.BillingFilter.customerID>, Equal<Contract.customerID>>>>>>>>>>>> Items;

  public ContractBilling()
  {
    ((PXProcessingBase<Contract>) this.Items).SetSelected<Contract.selected>();
  }

  [PXMergeAttributes]
  [PXDBLong]
  protected void _(PX.Data.Events.CacheAttached<PMTran.projectCuryInfoID> e)
  {
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable editDetail(PXAdapter adapter)
  {
    if (((PXSelectBase<Contract>) this.Items).Current != null)
    {
      ContractMaint instance = PXGraph.CreateInstance<ContractMaint>();
      ((PXGraph) instance).Clear();
      ((PXSelectBase<Contract>) instance.Contracts).Current = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract, Where<Contract.contractID, Equal<Required<Contract.contractID>>>>.Config>.Select((PXGraph) instance, new object[1]
      {
        (object) ((PXSelectBase<Contract>) this.Items).Current.ContractID
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "ViewContract");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  protected virtual void BillingFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase) this.Items).Cache.Clear();
  }

  protected virtual void BillingFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<Contract>) this.Items).SetProcessDelegate(new PXProcessingBase<Contract>.ProcessItemDelegate((object) new ContractBilling.\u003C\u003Ec__DisplayClass8_0()
    {
      filter = (ContractBilling.BillingFilter) e.Row
    }, __methodptr(\u003CBillingFilter_RowSelected\u003Eb__0)));
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXUIEnabledAttribute))]
  [PXRemoveBaseAttribute(typeof (PXFormulaAttribute))]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Enabled", false)]
  protected virtual void Contract_ExpireDate_CacheAttached(PXCache sender)
  {
  }

  [Serializable]
  public class BillingFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _InvoiceDate;
    protected string _CustomerClassID;
    protected int? _CustomerID;
    protected int? _TemplateID;

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? InvoiceDate
    {
      get => this._InvoiceDate;
      set => this._InvoiceDate = value;
    }

    [PXDBString(10, IsUnicode = true)]
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

    [ContractTemplate]
    public virtual int? TemplateID
    {
      get => this._TemplateID;
      set => this._TemplateID = value;
    }

    public abstract class invoiceDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ContractBilling.BillingFilter.invoiceDate>
    {
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ContractBilling.BillingFilter.customerClassID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ContractBilling.BillingFilter.customerID>
    {
    }

    public abstract class templateID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ContractBilling.BillingFilter.templateID>
    {
    }
  }
}
