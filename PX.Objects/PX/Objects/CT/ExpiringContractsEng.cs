// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ExpiringContractsEng
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.GL;
using System;
using System.Collections;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.CT;

[TableAndChartDashboardType]
public class ExpiringContractsEng : PXGraph<
#nullable disable
ExpiringContractsEng>
{
  public PXCancel<ExpiringContractsEng.ExpiringContractFilter> Cancel;
  public PXAction<ExpiringContractsEng.ExpiringContractFilter> viewContract;
  public PXFilter<ExpiringContractsEng.ExpiringContractFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelectJoin<Contract, InnerJoin<ContractBillingSchedule, On<ContractBillingSchedule.contractID, Equal<Contract.contractID>>, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<Contract.customerID>>>>, Where<Contract.baseType, Equal<CTPRType.contract>, And<Contract.type, Equal<Contract.type.expiring>>>> Contracts;
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R2.")]
  public PXSelectJoin<Contract, InnerJoin<ContractBillingSchedule, On<Contract.contractID, Equal<ContractBillingSchedule.contractID>>, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<Contract.customerID>>>>, Where<Contract.baseType, Equal<CTPRType.contract>, And<Contract.expireDate, LessEqual<Current<ExpiringContractsEng.ExpiringContractFilter.endDate>>, And<Contract.expireDate, GreaterEqual<Current<ExpiringContractsEng.ExpiringContractFilter.beginDate>>, And<Contract.status, NotEqual<Contract.status.canceled>>>>>> ContractsView;

  public ExpiringContractsEng()
  {
    ((PXSelectBase) this.Contracts).Cache.AllowDelete = false;
    ((PXSelectBase) this.Contracts).Cache.AllowUpdate = false;
    ((PXSelectBase) this.Contracts).Cache.AllowInsert = false;
  }

  public virtual PXSelectBase<Contract> GetContractsView()
  {
    return (PXSelectBase<Contract>) new PXSelectJoin<Contract, InnerJoin<ContractBillingSchedule, On<Contract.contractID, Equal<ContractBillingSchedule.contractID>>, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<Contract.customerID>>>>, Where<Contract.baseType, Equal<CTPRType.contract>, And<Contract.expireDate, LessEqual<Current<ExpiringContractsEng.ExpiringContractFilter.endDate>>, And<Contract.expireDate, GreaterEqual<Current<ExpiringContractsEng.ExpiringContractFilter.beginDate>>, And<Contract.status, NotEqual<Contract.status.canceled>>>>>>((PXGraph) this);
  }

  public virtual IEnumerable contracts()
  {
    ExpiringContractsEng expiringContractsEng = this;
    PXSelectBase<Contract> contractsView = expiringContractsEng.GetContractsView();
    ExpiringContractsEng.ExpiringContractFilter current = ((PXSelectBase<ExpiringContractsEng.ExpiringContractFilter>) expiringContractsEng.Filter).Current;
    if (current != null)
    {
      if (!current.ShowAutoRenewable.GetValueOrDefault())
        contractsView.WhereAnd<Where<Contract.autoRenew, Equal<False>, Or<Contract.autoRenew, IsNull>>>();
      if (!string.IsNullOrEmpty(current.CustomerClassID))
        contractsView.WhereAnd<Where<PX.Objects.AR.Customer.customerClassID, Equal<Current<ExpiringContractsEng.ExpiringContractFilter.customerClassID>>>>();
      if (current.TemplateID.HasValue)
        contractsView.WhereAnd<Where<Contract.templateID, Equal<Current<ExpiringContractsEng.ExpiringContractFilter.templateID>>>>();
      foreach (PXResult<Contract> pxResult in contractsView.Select(Array.Empty<object>()))
      {
        bool flag = false;
        if (PXResult<Contract>.op_Implicit(pxResult).Type == "E")
          flag = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract, Where<Contract.originalContractID, Equal<Required<Contract.originalContractID>>>>.Config>.Select((PXGraph) expiringContractsEng, new object[1]
          {
            (object) PXResult<Contract>.op_Implicit(pxResult).ContractID
          })) != null;
        if (!flag)
          yield return (object) pxResult;
      }
    }
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable ViewContract(PXAdapter adapter)
  {
    if (((PXSelectBase<Contract>) this.Contracts).Current != null)
    {
      ContractMaint instance = PXGraph.CreateInstance<ContractMaint>();
      ((PXGraph) instance).Clear();
      ((PXSelectBase<Contract>) instance.Contracts).Current = ((PXSelectBase<Contract>) this.Contracts).Current;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, nameof (ViewContract));
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  public virtual void ExpiringContractFilter_BeginDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  [Serializable]
  public class ExpiringContractFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _CustomerClassID;
    protected int? _TemplateID;
    protected DateTime? _BeginDate;
    protected int? _ExpireXDays;
    protected bool? _ShowAutoRenewable;

    [PXDBString(10, IsUnicode = true)]
    [PXSelector(typeof (CustomerClass.customerClassID), DescriptionField = typeof (CustomerClass.descr), CacheGlobal = true)]
    [PXUIField(DisplayName = "Customer Class")]
    public virtual string CustomerClassID
    {
      get => this._CustomerClassID;
      set => this._CustomerClassID = value;
    }

    [ContractTemplate]
    public virtual int? TemplateID
    {
      get => this._TemplateID;
      set => this._TemplateID = value;
    }

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
    public virtual DateTime? EndDate { get; set; }

    [PXDBInt]
    [PXDefault(30)]
    [PXUIField(DisplayName = "Duration")]
    public virtual int? ExpireXDays
    {
      get => this._ExpireXDays;
      set => this._ExpireXDays = value;
    }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Show Contracts Available for Mass Renewal")]
    public virtual bool? ShowAutoRenewable
    {
      get => this._ShowAutoRenewable;
      set => this._ShowAutoRenewable = value;
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ExpiringContractsEng.ExpiringContractFilter.customerClassID>
    {
    }

    public abstract class templateID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ExpiringContractsEng.ExpiringContractFilter.templateID>
    {
    }

    public abstract class beginDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ExpiringContractsEng.ExpiringContractFilter.beginDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ExpiringContractsEng.ExpiringContractFilter.endDate>
    {
    }

    public abstract class expireXDays : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ExpiringContractsEng.ExpiringContractFilter.expireXDays>
    {
    }

    public abstract class showAutoRenewable : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ExpiringContractsEng.ExpiringContractFilter.showAutoRenewable>
    {
    }
  }
}
