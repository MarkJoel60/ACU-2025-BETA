// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.ManageExemptCustomer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.TX;

public class ManageExemptCustomer : PXGraph<ManageExemptCustomer>
{
  public PXCancel<ExemptCustomerFilter> Cancel;
  public PXAction<ExemptCustomerFilter> viewDocument;
  public PXFilter<ExemptCustomerFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<ExemptCustomer, ExemptCustomerFilter> CustomerList;
  public PXAction<ExemptCustomer> viewCustomer;

  public ManageExemptCustomer()
  {
    ((PXProcessingBase<ExemptCustomer>) this.CustomerList).SetSelected<ExemptCustomer.selected>();
  }

  public virtual IEnumerable customerList()
  {
    IEnumerable enumerable = (IEnumerable) new List<ExemptCustomer>();
    if (!string.IsNullOrEmpty(((PXSelectBase<ExemptCustomerFilter>) this.Filter).Current?.Action) && (((PXSelectBase<ExemptCustomerFilter>) this.Filter).Current?.Action == "Update Customer in ECM Provider" || !string.IsNullOrEmpty(((PXSelectBase<ExemptCustomerFilter>) this.Filter).Current?.CompanyCode)))
      enumerable = this.GetCustomerRecords(((PXSelectBase<ExemptCustomerFilter>) this.Filter).Current);
    return enumerable;
  }

  protected virtual void _(Events.RowSelected<ExemptCustomerFilter> e)
  {
    ExemptCustomerFilter row = e.Row;
    if (row == null)
      return;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ManageExemptCustomer.\u003C\u003Ec__DisplayClass6_0 cDisplayClass60 = new ManageExemptCustomer.\u003C\u003Ec__DisplayClass6_0();
    int recCount;
    this.GetPreferedCompanyCode(out recCount);
    if (recCount == 1)
      PXUIFieldAttribute.SetEnabled<ExemptCustomerFilter.companyCode>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ExemptCustomerFilter>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetVisible<ExemptCustomerFilter.companyCode>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ExemptCustomerFilter>>) e).Cache, (object) e.Row, row.Action != "Update Customer in ECM Provider");
    // ISSUE: reference to a compiler-generated field
    cDisplayClass60.filter = (ExemptCustomerFilter) ((PXSelectBase) this.Filter).Cache.CreateCopy((object) row);
    // ISSUE: method pointer
    ((PXProcessingBase<ExemptCustomer>) this.CustomerList).SetProcessDelegate(new PXProcessingBase<ExemptCustomer>.ProcessListDelegate((object) cDisplayClass60, __methodptr(\u003C_\u003Eb__0)));
  }

  public string GetPreferedCompanyCode(out int recCount)
  {
    string empty = string.Empty;
    PXResultset<TaxPluginMapping> source = PXSelectBase<TaxPluginMapping, PXViewOf<TaxPluginMapping>.BasedOn<SelectFromBase<TaxPluginMapping, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<TaxPlugin>.On<BqlOperand<TaxPluginMapping.taxPluginID, IBqlString>.IsEqual<TaxPlugin.taxPluginID>>>, FbqlJoins.Inner<TXSetup>.On<BqlOperand<TaxPlugin.taxPluginID, IBqlString>.IsEqual<TXSetup.eCMProvider>>>>.Aggregate<To<GroupBy<TaxPluginMapping.companyCode>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    recCount = ((IQueryable<PXResult<TaxPluginMapping>>) source).Count<PXResult<TaxPluginMapping>>();
    return recCount > 0 ? PXResult<TaxPluginMapping>.op_Implicit(source[0]).CompanyCode : empty;
  }

  protected virtual void _(
    Events.FieldDefaulting<ExemptCustomerFilter.companyCode> e)
  {
    int recCount;
    string preferedCompanyCode = this.GetPreferedCompanyCode(out recCount);
    if (recCount != 1)
      return;
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<ExemptCustomerFilter.companyCode>, object, object>) e).NewValue = (object) preferedCompanyCode;
  }

  protected virtual IEnumerable GetCustomerRecords(ExemptCustomerFilter filter)
  {
    ManageExemptCustomer manageExemptCustomer = this;
    if (!string.IsNullOrEmpty(filter.Action))
    {
      PXSelectBase<ExemptCustomer> pxSelectBase = (PXSelectBase<ExemptCustomer>) new PXSelect<ExemptCustomer>((PXGraph) manageExemptCustomer);
      if (filter.Action == "Update Customer in ECM Provider")
        pxSelectBase.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Customer.eCMCompanyCode, IsNotNull>>>>.And<BqlOperand<Customer.isECMValid, IBqlBool>.IsEqual<False>>>>();
      foreach (PXResult<ExemptCustomer> pxResult in pxSelectBase.Select(Array.Empty<object>()))
        yield return (object) PXResult<ExemptCustomer>.op_Implicit(pxResult);
    }
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewCustomer(PXAdapter adapter)
  {
    ExemptCustomer current = ((PXSelectBase<ExemptCustomer>) this.CustomerList).Current;
    if (current != null && !string.IsNullOrEmpty(current.AcctCD) && current.BAccountID.HasValue)
    {
      CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
      ((PXSelectBase<Customer>) instance.BAccount).Current = PXResultset<Customer>.op_Implicit(((PXSelectBase<Customer>) instance.BAccount).Search<Customer.bAccountID>((object) current.BAccountID, Array.Empty<object>()));
      if (((PXSelectBase<Customer>) instance.BAccount).Current != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Document");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  public static void ProcessECMCustomers(ExemptCustomerFilter filter, List<ExemptCustomer> list)
  {
    CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
    CustomerMaintExternalECMExt extension = ((PXGraph) instance).GetExtension<CustomerMaintExternalECMExt>();
    for (int index = 0; index < list.Count; ++index)
    {
      try
      {
        ((PXGraph) instance).Clear();
        Customer customer = PXResultset<Customer>.op_Implicit(((PXSelectBase<Customer>) instance.BAccount).Search<Customer.bAccountID>((object) list[index].BAccountID, Array.Empty<object>()));
        if (customer != null)
        {
          if (filter?.Action == "Create Customer in ECM Provider")
          {
            string warning;
            extension.CreateECMCustomer(customer, filter.CompanyCode, out warning);
            if (!string.IsNullOrEmpty(warning))
              PXProcessing<ExemptCustomer>.SetWarning(index, PXMessages.LocalizeFormatNoPrefixNLA("The customer already exists in the Avalara ECM provider for the following company code or codes: {0}", new object[1]
              {
                (object) warning
              }));
            else
              PXProcessing<ExemptCustomer>.SetInfo(index, "The record has been processed successfully.");
          }
          else if (filter?.Action == "Update Customer in ECM Provider")
          {
            extension.UpdateECMCustomer(customer);
            PXProcessing<ExemptCustomer>.SetInfo(index, "The record has been processed successfully.");
          }
        }
      }
      catch (Exception ex)
      {
        PXProcessing<ExemptCustomer>.SetError(index, ex.Message);
      }
    }
  }
}
