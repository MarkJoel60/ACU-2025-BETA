// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.RenewContracts
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

#nullable enable
namespace PX.Objects.CT;

[TableAndChartDashboardType]
public class RenewContracts : PXGraph<
#nullable disable
RenewContracts>
{
  public PXCancel<RenewContracts.RenewalContractFilter> Cancel;
  [Obsolete("Will be removed in Acumatica 2019R1")]
  public PXAction<RenewContracts.RenewalContractFilter> EditDetail;
  public PXFilter<RenewContracts.RenewalContractFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<RenewContracts.ContractsList, RenewContracts.RenewalContractFilter> Items;

  public BqlCommand ItemsInitialCommand { get; set; } = PXSelectBase<RenewContracts.ContractsList, PXSelect<RenewContracts.ContractsList, Where<Add<RenewContracts.ContractsList.expireDate, IsNull<Minus<RenewContracts.ContractsList.autoRenewDays>, Zero>>, LessEqual<Current<RenewContracts.RenewalContractFilter.renewalDate>>>, OrderBy<Asc<RenewContracts.ContractsList.contractCD>>>.Config>.GetCommand();

  public RenewContracts()
  {
    ((PXProcessingBase<RenewContracts.ContractsList>) this.Items).SetSelected<RenewContracts.ContractsList.selected>();
  }

  protected virtual IEnumerable items()
  {
    RenewContracts.RenewalContractFilter current = ((PXSelectBase<RenewContracts.RenewalContractFilter>) this.Filter).Current;
    if (!string.IsNullOrEmpty(current.CustomerClassID))
      this.ItemsInitialCommand = this.ItemsInitialCommand.WhereAnd<Where<RenewContracts.ContractsList.customerClassID, Equal<Current<RenewContracts.RenewalContractFilter.customerClassID>>>>();
    if (current.TemplateID.HasValue)
      this.ItemsInitialCommand = this.ItemsInitialCommand.WhereAnd<Where<RenewContracts.ContractsList.templateID, Equal<Current<RenewContracts.RenewalContractFilter.templateID>>>>();
    return GraphHelper.QuickSelect((PXGraph) this, this.ItemsInitialCommand);
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable editDetail(PXAdapter adapter)
  {
    if (((PXSelectBase<RenewContracts.ContractsList>) this.Items).Current != null)
    {
      ContractMaint instance = PXGraph.CreateInstance<ContractMaint>();
      ((PXGraph) instance).Clear();
      ((PXSelectBase<Contract>) instance.Contracts).Current = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract, Where<Contract.contractID, Equal<Current<RenewContracts.ContractsList.contractID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "ViewContract");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  protected virtual void RenewalContractFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase) this.Items).Cache.Clear();
  }

  protected virtual void RenewalContractFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<RenewContracts.ContractsList>) this.Items).SetProcessDelegate<ContractMaint>(new PXProcessingBase<RenewContracts.ContractsList>.ProcessItemDelegate<ContractMaint>((object) new RenewContracts.\u003C\u003Ec__DisplayClass12_0()
    {
      filter = ((PXSelectBase<RenewContracts.RenewalContractFilter>) this.Filter).Current
    }, __methodptr(\u003CRenewalContractFilter_RowSelected\u003Eb__0)));
  }

  [Obsolete]
  [PXMergeAttributes]
  protected virtual void ContractsList_StartDate_CacheAttached(PXCache sender)
  {
  }

  protected virtual void ContractsList_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is RenewContracts.ContractsList row))
      return;
    DateTime? nextDate = row.NextDate;
    DateTime? expireDate = row.ExpireDate;
    if ((nextDate.HasValue & expireDate.HasValue ? (nextDate.GetValueOrDefault() < expireDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    PXUIFieldAttribute.SetEnabled<RenewContracts.ContractsList.selected>(sender, (object) row, false);
    sender.RaiseExceptionHandling<RenewContracts.ContractsList.selected>((object) row, (object) null, (Exception) new PXSetPropertyException("Contract must be billed before renewal.", (PXErrorLevel) 3));
  }

  public static void RenewContract(
    ContractMaint docgraph,
    RenewContracts.ContractsList item,
    RenewContracts.RenewalContractFilter filter)
  {
    ((PXSelectBase<Contract>) docgraph.Contracts).Current = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract, Where<Contract.contractID, Equal<Required<Contract.contractID>>>>.Config>.Select((PXGraph) docgraph, new object[1]
    {
      (object) item.ContractID
    }));
    ((PXSelectBase<ContractBillingSchedule>) docgraph.Billing).Current = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) docgraph.Billing).Select(Array.Empty<object>()));
    docgraph.RenewContract(filter.RenewalDate.Value);
  }

  [Serializable]
  public class RenewalContractFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _CustomerClassID;
    protected int? _TemplateID;

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
    [PXUIField(DisplayName = "Renewal Date")]
    public virtual DateTime? RenewalDate { get; set; }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RenewContracts.RenewalContractFilter.customerClassID>
    {
    }

    public abstract class templateID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RenewContracts.RenewalContractFilter.templateID>
    {
    }

    public abstract class renewalDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      RenewContracts.RenewalContractFilter.renewalDate>
    {
    }
  }

  [PXProjection(typeof (Select2<Contract, InnerJoin<ContractBillingSchedule, On<Contract.contractID, Equal<ContractBillingSchedule.contractID>>, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<Contract.customerID>>>>, Where<Contract.baseType, Equal<CTPRType.contract>, And<Contract.type, NotEqual<Contract.type.unlimited>, And<Contract.autoRenew, Equal<True>, And2<Not<Exists<Select<RenewContracts.ChildContract, Where<RenewContracts.ChildContract.originalContractID, Equal<Contract.contractID>, And<Where<Contract.type, Equal<Contract.type.renewable>, Or<Contract.type, Equal<Contract.type.expiring>>>>>>>>, And<Where<Contract.status, Equal<Contract.status.active>, Or<Contract.status, Equal<Contract.status.expired>>>>>>>>>))]
  [Serializable]
  public class ContractsList : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected bool? _Selected = new bool?(false);
    protected int? _ContractID;
    protected string _ContractCD;
    protected int? _CustomerID;
    protected string _CustomerName;
    protected string _CustomerClassID;
    protected string _Description;
    protected int? _AutoRenewDays;
    protected DateTime? _LastDate;
    protected DateTime? _NextDate;
    protected DateTime? _StartDate;
    protected DateTime? _ExpireDate;
    protected string _Type;
    protected int? _TemplateID;
    protected string _Status;

    [PXBool]
    [PXDefault(false)]
    [PXUIField]
    public bool? Selected
    {
      get => this._Selected;
      set => this._Selected = value;
    }

    [Contract(BqlTable = typeof (Contract))]
    public virtual int? ContractID
    {
      get => this._ContractID;
      set => this._ContractID = value;
    }

    [PXDimension("CONTRACT")]
    [PXDBString(IsUnicode = true, IsKey = true, InputMask = "", BqlTable = typeof (Contract), IsFixed = true)]
    [PXDefault]
    [PXUIField]
    public virtual string ContractCD
    {
      get => this._ContractCD;
      set => this._ContractCD = value;
    }

    [PXDBInt(BqlTable = typeof (Contract))]
    [PXUIField]
    [PXSelector(typeof (PX.Objects.AR.Customer.bAccountID), SubstituteKey = typeof (PX.Objects.AR.Customer.acctCD))]
    public virtual int? CustomerID
    {
      get => this._CustomerID;
      set => this._CustomerID = value;
    }

    [PXDBString(60, IsUnicode = true, BqlField = typeof (PX.Objects.AR.Customer.acctName))]
    [PXUIField]
    public virtual string CustomerName
    {
      get => this._CustomerName;
      set => this._CustomerName = value;
    }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.AR.CustomerClass">customer class</see>
    /// to which the customer belongs.
    /// </summary>
    [PXDBString(10, IsUnicode = true, BqlTable = typeof (PX.Objects.AR.Customer))]
    [PXSelector(typeof (CustomerClass.customerClassID), DescriptionField = typeof (CustomerClass.descr), CacheGlobal = true)]
    [PXUIField(DisplayName = "Customer Class")]
    public virtual string CustomerClassID
    {
      get => this._CustomerClassID;
      set => this._CustomerClassID = value;
    }

    [PXDBLocalizableString(60, IsUnicode = true, BqlTable = typeof (Contract))]
    [PXDefault]
    [PXUIField]
    public virtual string Description
    {
      get => this._Description;
      set => this._Description = value;
    }

    [PXDBInt(MinValue = 0, MaxValue = 365, BqlTable = typeof (Contract))]
    [PXUIField(DisplayName = "Renewal Point")]
    public virtual int? AutoRenewDays
    {
      get => this._AutoRenewDays;
      set => this._AutoRenewDays = value;
    }

    [PXDBDate(BqlTable = typeof (ContractBillingSchedule))]
    [PXUIField]
    public virtual DateTime? LastDate
    {
      get => this._LastDate;
      set => this._LastDate = value;
    }

    [PXDBDate(BqlTable = typeof (ContractBillingSchedule))]
    [PXUIField]
    public virtual DateTime? NextDate
    {
      get => this._NextDate;
      set => this._NextDate = value;
    }

    [PXDBDate(BqlTable = typeof (Contract))]
    [PXUIField]
    public virtual DateTime? StartDate
    {
      get => this._StartDate;
      set => this._StartDate = value;
    }

    [PXDBDate(BqlTable = typeof (Contract))]
    [PXUIField]
    public virtual DateTime? ExpireDate
    {
      get => this._ExpireDate;
      set => this._ExpireDate = value;
    }

    [PXDBString(1, IsFixed = true, BqlTable = typeof (Contract))]
    [PXUIField]
    [Contract.type.List]
    [PXDefault("R")]
    public virtual string Type
    {
      get => this._Type;
      set => this._Type = value;
    }

    [ContractTemplate(BqlTable = typeof (Contract))]
    public virtual int? TemplateID
    {
      get => this._TemplateID;
      set => this._TemplateID = value;
    }

    [PXDBString(1, IsFixed = true, BqlTable = typeof (Contract))]
    [Contract.status.List]
    [PXUIField]
    public virtual string Status
    {
      get => this._Status;
      set => this._Status = value;
    }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      RenewContracts.ContractsList.selected>
    {
    }

    public abstract class contractID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RenewContracts.ContractsList.contractID>
    {
    }

    public abstract class contractCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RenewContracts.ContractsList.contractCD>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RenewContracts.ContractsList.customerID>
    {
    }

    public abstract class customerName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RenewContracts.ContractsList.customerName>
    {
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RenewContracts.ContractsList.customerClassID>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RenewContracts.ContractsList.description>
    {
    }

    public abstract class autoRenewDays : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RenewContracts.ContractsList.autoRenewDays>
    {
    }

    public abstract class lastDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      RenewContracts.ContractsList.lastDate>
    {
    }

    public abstract class nextDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      RenewContracts.ContractsList.nextDate>
    {
    }

    public abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      RenewContracts.ContractsList.startDate>
    {
    }

    public abstract class expireDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      RenewContracts.ContractsList.expireDate>
    {
    }

    public abstract class type : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RenewContracts.ContractsList.type>
    {
    }

    public abstract class templateID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RenewContracts.ContractsList.templateID>
    {
    }

    public abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RenewContracts.ContractsList.status>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class ChildContract : Contract
  {
    public new abstract class contractID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RenewContracts.ChildContract.contractID>
    {
    }

    public new abstract class contractCD : IBqlField, IBqlOperand
    {
    }

    public new abstract class originalContractID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RenewContracts.ChildContract.originalContractID>
    {
    }

    public new abstract class masterContractID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RenewContracts.ChildContract.masterContractID>
    {
    }
  }
}
