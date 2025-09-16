// Decompiled with JetBrains decompiler
// Type: PX.Objects.GraphExtensions.ExtendBAccount.ExtendToCustomerGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.GraphExtensions.ExtendBAccount;

public abstract class ExtendToCustomerGraph<TGraph, TPrimary> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  /// <summary>A mapping-based view of the <see cref="T:PX.Objects.GraphExtensions.ExtendBAccount.ExtendToCustomerGraph`2.SourceAccount" /> data.</summary>
  public PXSelectExtension<ExtendToCustomerGraph<TGraph, TPrimary>.SourceAccount> Accounts;
  public PXAction<TPrimary> extendToCustomer;
  public PXDBAction<TPrimary> viewCustomer;

  /// <summary>Returns the mapping of the <see cref="T:PX.Objects.GraphExtensions.ExtendBAccount.ExtendToCustomerGraph`2.SourceAccount" /> mapped cache extension to a DAC. This method must be overridden in the implementation class of the base graph.</summary>
  /// <remarks>In the implementation graph for a particular graph, you can either return the default mapping or override the default mapping in this method.</remarks>
  /// <example><para>The following code shows the method that overrides the GetSourceAccountMapping() method in the implementation class. The method returns the default mapping of the SourceAccount mapped cache extension to the CROpportunity DAC.</para>
  ///   <code title="Example" lang="CS">
  /// protected override SourceAccountMapping GetSourceAccountMapping()
  /// {
  ///       return new SourceAccountMapping(typeof(CROpportunity));
  /// }</code>
  /// </example>
  protected abstract ExtendToCustomerGraph<TGraph, TPrimary>.SourceAccountMapping GetSourceAccountMapping();

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ExtendToCustomer(PXAdapter adapter)
  {
    CustomerMaint customerMaint = this.Extend();
    if (customerMaint != null)
    {
      if (!this.Base.IsContractBasedAPI)
        throw new PXRedirectRequiredException((PXGraph) customerMaint, "Edit Customer");
      ((PXAction) customerMaint.Save).Press();
      this.Base.Actions.PressCancel();
    }
    return adapter.Get();
  }

  public virtual CustomerMaint Extend()
  {
    ExtendToCustomerGraph<TGraph, TPrimary>.SourceAccount current = ((PXSelectBase<ExtendToCustomerGraph<TGraph, TPrimary>.SourceAccount>) this.Accounts).Current;
    PX.Objects.CR.BAccount main = (PX.Objects.CR.BAccount) ((PXSelectBase) this.Accounts).Cache.GetMain<ExtendToCustomerGraph<TGraph, TPrimary>.SourceAccount>(current);
    if (current == null || !(current.Type != "CU") || !(current.Type != "VC"))
      return (CustomerMaint) null;
    this.Base.Actions["Save"].Press();
    CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
    ((PXGraph) instance).TimeStamp = this.Base.TimeStamp;
    main.Status = "A";
    if (!(main.Type != "VE"))
    {
      int? vorgBaccountId = main.VOrgBAccountID;
      int num = 0;
      if (!(vorgBaccountId.GetValueOrDefault() == num & vorgBaccountId.HasValue))
        goto label_4;
    }
    main.BaseCuryID = (string) null;
label_4:
    PX.Objects.AR.Customer customer = (PX.Objects.AR.Customer) ((PXSelectBase) instance.BAccount).Cache.Extend<PX.Objects.CR.BAccount>(main);
    ((PXSelectBase) instance.BAccount).Cache.SetDefaultExt<PX.Objects.AR.Customer.cOrgBAccountID>((object) customer);
    ((PXSelectBase) instance.BAccount).Cache.SetDefaultExt<PX.Objects.AR.Customer.curyID>((object) customer);
    ((PXSelectBase) instance.BAccount).Cache.SetDefaultExt<PX.Objects.AR.Customer.allowOverrideCury>((object) customer);
    ((PXSelectBase) instance.BAccount).Cache.SetDefaultExt<PX.Objects.AR.Customer.curyRateTypeID>((object) customer);
    ((PXSelectBase) instance.BAccount).Cache.SetDefaultExt<PX.Objects.AR.Customer.allowOverrideRate>((object) customer);
    ((PXSelectBase<PX.Objects.AR.Customer>) instance.BAccount).Current = customer;
    customer.NoteID = current.NoteID;
    customer.CreatedByID = main.CreatedByID;
    customer.BAccountClassID = main.ClassID;
    customer.Type = current.Type == "VE" ? "VC" : "CU";
    string str = current.Type == "VE" ? "VC" : "CU";
    if (current != null && current.LocaleName != null)
      customer.LocaleName = current?.LocaleName;
    CustomerMaint.DefLocationExt extension1 = ((PXGraph) instance).GetExtension<CustomerMaint.DefLocationExt>();
    PX.Objects.CR.Standalone.Location location1 = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension1.DefLocation).Select(Array.Empty<object>()));
    ((PXSelectBase) extension1.DefLocation).Cache.RaiseRowSelected((object) location1);
    bool? isBranch;
    if (location1.CTaxZoneID != null)
    {
      isBranch = main.IsBranch;
      if (!isBranch.GetValueOrDefault())
        goto label_9;
    }
    ((PXSelectBase) extension1.DefLocation).Cache.SetDefaultExt<PX.Objects.CR.Standalone.Location.cTaxZoneID>((object) location1);
label_9:
    isBranch = main.IsBranch;
    if (!isBranch.GetValueOrDefault() && location1 != null && location1.VTaxZoneID != null)
      ((PXSelectBase) extension1.DefLocation).Cache.SetValueExt<PX.Objects.CR.Location.cTaxZoneID>((object) location1, (object) location1.VTaxZoneID);
    CustomerMaint.DefLocationExt defLocationExt1 = extension1;
    PX.Objects.CR.Standalone.Location location2 = location1;
    string aLocationType1 = str;
    isBranch = main.IsBranch;
    int num1 = isBranch.GetValueOrDefault() ? 1 : 0;
    defLocationExt1.InitLocation((IBqlTable) location2, aLocationType1, num1 != 0);
    isBranch = main.IsBranch;
    if (isBranch.GetValueOrDefault() && location1 != null)
      ((PXSelectBase) extension1.DefLocation).Cache.SetDefaultExt<PX.Objects.CR.Standalone.Location.cBranchID>((object) location1);
    PX.Objects.CR.Standalone.Location location3 = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension1.DefLocation).Update(location1);
    CustomerMaint.LocationDetailsExt extension2 = ((PXGraph) instance).GetExtension<CustomerMaint.LocationDetailsExt>();
    foreach (PXResult<PX.Objects.CR.Standalone.Location> pxResult in ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension2.Locations).Select(Array.Empty<object>()))
    {
      PX.Objects.CR.Standalone.Location location4 = PXResult<PX.Objects.CR.Standalone.Location>.op_Implicit(pxResult);
      int? locationId1 = location4.LocationID;
      int? locationId2 = location3.LocationID;
      if (!(locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue))
      {
        CustomerMaint.DefLocationExt defLocationExt2 = extension1;
        PX.Objects.CR.Standalone.Location location5 = location4;
        string aLocationType2 = str;
        isBranch = main.IsBranch;
        int num2 = isBranch.GetValueOrDefault() ? 1 : 0;
        defLocationExt2.InitLocation((IBqlTable) location5, aLocationType2, num2 != 0);
        ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension2.Locations).Update(location4);
      }
    }
    UDFHelper.CopyAttributes(((PXSelectBase) this.Accounts).Cache, (object) current, ((PXSelectBase) instance.BAccount).Cache, (object) ((PXSelectBase<PX.Objects.AR.Customer>) instance.BAccount).Current, customer.ClassID);
    ((PXGraph) instance).Caches[typeof (CSAnswers)].Clear();
    ((PXGraph) instance).Caches[typeof (CSAnswers)].ClearQueryCache();
    object note = (object) PXNoteAttribute.GetNote(((PXSelectBase) instance.BAccount).Cache, (object) customer);
    ((PXSelectBase) instance.BAccount).Cache.RaiseFieldUpdating("NoteText", (object) customer, ref note);
    return instance;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewCustomer(PXAdapter adapter)
  {
    ExtendToCustomerGraph<TGraph, TPrimary>.SourceAccount current = ((PXSelectBase<ExtendToCustomerGraph<TGraph, TPrimary>.SourceAccount>) this.Accounts).Current;
    if (current != null && (current.Type == "CU" || current.Type == "VC"))
    {
      CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
      ((PXSelectBase<PX.Objects.AR.Customer>) instance.BAccount).Current = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) instance.BAccount).Search<PX.Objects.AR.Customer.acctCD>((object) current.AcctCD, Array.Empty<object>()));
      throw new PXRedirectRequiredException((PXGraph) instance, "Edit Customer");
    }
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<TPrimary> e)
  {
    PXEntryStatus status = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<TPrimary>>) e).Cache.GetStatus((object) e.Row);
    ExtendToCustomerGraph<TGraph, TPrimary>.SourceAccount current = ((PXSelectBase<ExtendToCustomerGraph<TGraph, TPrimary>.SourceAccount>) this.Accounts).Current;
    if (current == null)
      return;
    PX.Objects.CR.BAccount main = (PX.Objects.CR.BAccount) ((PXSelectBase) this.Accounts).Cache.GetMain<ExtendToCustomerGraph<TGraph, TPrimary>.SourceAccount>(current);
    bool flag = true;
    if (status != 2 && main?.Type == "VE")
      flag = PXResultset<PX.Objects.CA.Light.Vendor>.op_Implicit(PXSelectBase<PX.Objects.CA.Light.Vendor, PXViewOf<PX.Objects.CA.Light.Vendor>.BasedOn<SelectFromBase<PX.Objects.CA.Light.Vendor, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.CA.Light.Vendor.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
      {
        (object) main.BAccountID
      })) != null;
    ((PXAction) this.extendToCustomer).SetEnabled(((current.Type == "OR" || current.Type == "CU" || current.Type == "VC" ? 0 : (status != 2 ? 1 : 0)) & (flag ? 1 : 0)) != 0);
    ((PXAction) this.viewCustomer).SetEnabled((current.Type == "CU" || current.Type == "VC") && status != 2);
  }

  [PXHidden]
  public class SourceAccount : PX.Objects.GraphExtensions.ExtendBAccount.SourceAccount<ExtendToCustomerGraph<TGraph, TPrimary>>
  {
  }

  /// <summary>A class that defines the default mapping of the <see cref="T:PX.Objects.GraphExtensions.ExtendBAccount.ExtendToCustomerGraph`2.SourceAccount" /> mapped cache extension to a DAC.</summary>
  protected class SourceAccountMapping : IBqlMapping
  {
    /// <exclude />
    protected System.Type _extension = typeof (ExtendToCustomerGraph<TGraph, TPrimary>.SourceAccount);
    /// <exclude />
    protected System.Type _table;
    /// <exclude />
    public System.Type AcctCD = typeof (PX.Objects.GraphExtensions.ExtendBAccount.SourceAccount<ExtendToCustomerGraph<TGraph, TPrimary>>.acctCD);
    /// <exclude />
    public System.Type Type = typeof (PX.Objects.GraphExtensions.ExtendBAccount.SourceAccount<ExtendToCustomerGraph<TGraph, TPrimary>>.type);
    /// <exclude />
    public System.Type LocaleName = typeof (PX.Objects.GraphExtensions.ExtendBAccount.SourceAccount<ExtendToCustomerGraph<TGraph, TPrimary>>.localeName);

    /// <exclude />
    public System.Type Extension => this._extension;

    /// <exclude />
    public System.Type Table => this._table;

    /// <summary>Creates the default mapping of the <see cref="T:PX.Objects.GraphExtensions.ExtendBAccount.ExtendToCustomerGraph`2.SourceAccount" /> mapped cache extension to the specified table.</summary>
    /// <param name="table">A DAC.</param>
    public SourceAccountMapping(System.Type table) => this._table = table;
  }
}
