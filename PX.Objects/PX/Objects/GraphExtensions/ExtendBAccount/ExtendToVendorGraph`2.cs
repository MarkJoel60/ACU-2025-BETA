// Decompiled with JetBrains decompiler
// Type: PX.Objects.GraphExtensions.ExtendBAccount.ExtendToVendorGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CA.Light;
using PX.Objects.CR;
using PX.Objects.CS;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.GraphExtensions.ExtendBAccount;

public abstract class ExtendToVendorGraph<TGraph, TPrimary> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  /// <summary>A mapping-based view of the <see cref="T:PX.Objects.GraphExtensions.ExtendBAccount.ExtendToVendorGraph`2.SourceAccount" /> data.</summary>
  public PXSelectExtension<ExtendToVendorGraph<TGraph, TPrimary>.SourceAccount> Accounts;
  public PXAction<TPrimary> extendToVendor;
  public PXDBAction<TPrimary> viewVendor;

  /// <summary>Returns the mapping of the <see cref="T:PX.Objects.GraphExtensions.ExtendBAccount.ExtendToVendorGraph`2.SourceAccount" /> mapped cache extension to a DAC. This method must be overridden in the implementation class of the base graph.</summary>
  /// <remarks>In the implementation graph for a particular graph, you can either return the default mapping or override the default mapping in this method.</remarks>
  /// <example><para>The following code shows the method that overrides the GetSourceAccountMapping() method in the implementation class. The method returns the default mapping of the SourceAccount mapped cache extension to the CROpportunity DAC.</para>
  ///   <code title="Example" lang="CS">
  /// protected override SourceAccountMapping GetSourceAccountMapping()
  /// {
  ///       return new SourceAccountMapping(typeof(CROpportunity));
  /// }</code>
  /// </example>
  protected abstract ExtendToVendorGraph<TGraph, TPrimary>.SourceAccountMapping GetSourceAccountMapping();

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ExtendToVendor(PXAdapter adapter)
  {
    VendorMaint vendorMaint = this.Extend();
    if (vendorMaint != null)
    {
      if (!this.Base.IsContractBasedAPI)
        throw new PXRedirectRequiredException((PXGraph) vendorMaint, "Edit Vendor");
      ((PXAction) vendorMaint.Save).Press();
      this.Base.Actions.PressCancel();
    }
    return adapter.Get();
  }

  public virtual VendorMaint Extend()
  {
    ExtendToVendorGraph<TGraph, TPrimary>.SourceAccount current = ((PXSelectBase<ExtendToVendorGraph<TGraph, TPrimary>.SourceAccount>) this.Accounts).Current;
    PX.Objects.CR.BAccount main = (PX.Objects.CR.BAccount) ((PXSelectBase) this.Accounts).Cache.GetMain<ExtendToVendorGraph<TGraph, TPrimary>.SourceAccount>(current);
    if (current == null || !(current.Type != "VE") || !(current.Type != "VC"))
      return (VendorMaint) null;
    this.Base.Actions["Save"].Press();
    VendorMaint instance = PXGraph.CreateInstance<VendorMaint>();
    ((PXGraph) instance).TimeStamp = this.Base.TimeStamp;
    if (!(main.Type != "CU"))
    {
      int? corgBaccountId = main.COrgBAccountID;
      int num = 0;
      if (!(corgBaccountId.GetValueOrDefault() == num & corgBaccountId.HasValue))
        goto label_4;
    }
    main.BaseCuryID = (string) null;
label_4:
    VendorR vendorR = (VendorR) ((PXSelectBase) instance.BAccount).Cache.Extend<PX.Objects.CR.BAccount>(main);
    ((PXSelectBase) instance.BAccount).Cache.SetDefaultExt<PX.Objects.AP.Vendor.vOrgBAccountID>((object) vendorR);
    ((PXSelectBase) instance.BAccount).Cache.SetDefaultExt<PX.Objects.AP.Vendor.curyID>((object) vendorR);
    ((PXSelectBase) instance.BAccount).Cache.SetDefaultExt<PX.Objects.AP.Vendor.allowOverrideCury>((object) vendorR);
    ((PXSelectBase) instance.BAccount).Cache.SetDefaultExt<PX.Objects.AP.Vendor.curyRateTypeID>((object) vendorR);
    ((PXSelectBase) instance.BAccount).Cache.SetDefaultExt<PX.Objects.AP.Vendor.allowOverrideRate>((object) vendorR);
    ((PXSelectBase<VendorR>) instance.BAccount).Current = vendorR;
    vendorR.NoteID = current.NoteID;
    vendorR.CreatedByID = main.CreatedByID;
    vendorR.BAccountClassID = main.ClassID;
    vendorR.Type = current.Type == "CU" ? "VC" : "VE";
    if (current.LocaleName != null)
      vendorR.LocaleName = current.LocaleName;
    string str = current.Type == "CU" ? "VC" : "VE";
    VendorMaint.DefLocationExt extension1 = ((PXGraph) instance).GetExtension<VendorMaint.DefLocationExt>();
    PX.Objects.CR.Standalone.Location location1 = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension1.DefLocation).Select(Array.Empty<object>()));
    ((PXSelectBase) extension1.DefLocation).Cache.RaiseRowSelected((object) location1);
    bool? isBranch;
    if (location1.VTaxZoneID != null)
    {
      isBranch = main.IsBranch;
      if (!isBranch.GetValueOrDefault())
        goto label_9;
    }
    ((PXSelectBase) extension1.DefLocation).Cache.SetDefaultExt<PX.Objects.CR.Standalone.Location.vTaxZoneID>((object) location1);
label_9:
    isBranch = main.IsBranch;
    if (!isBranch.GetValueOrDefault() && location1 != null && location1.CTaxZoneID != null)
      ((PXSelectBase) extension1.DefLocation).Cache.SetValueExt<PX.Objects.CR.Location.vTaxZoneID>((object) location1, (object) location1.CTaxZoneID);
    VendorMaint.DefLocationExt defLocationExt1 = extension1;
    PX.Objects.CR.Standalone.Location location2 = location1;
    string aLocationType1 = str;
    isBranch = main.IsBranch;
    int num1 = isBranch.GetValueOrDefault() ? 1 : 0;
    defLocationExt1.InitLocation((IBqlTable) location2, aLocationType1, num1 != 0);
    isBranch = main.IsBranch;
    if (isBranch.GetValueOrDefault() && location1 != null)
      ((PXSelectBase) extension1.DefLocation).Cache.SetDefaultExt<PX.Objects.CR.Standalone.Location.vBranchID>((object) location1);
    PX.Objects.CR.Standalone.Location location3 = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension1.DefLocation).Update(location1);
    VendorMaint.LocationDetailsExt extension2 = ((PXGraph) instance).GetExtension<VendorMaint.LocationDetailsExt>();
    foreach (PXResult<PX.Objects.CR.Standalone.Location> pxResult in ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension2.Locations).Select(Array.Empty<object>()))
    {
      PX.Objects.CR.Standalone.Location location4 = PXResult<PX.Objects.CR.Standalone.Location>.op_Implicit(pxResult);
      int? locationId1 = location4.LocationID;
      int? locationId2 = location3.LocationID;
      if (!(locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue))
      {
        VendorMaint.DefLocationExt defLocationExt2 = extension1;
        PX.Objects.CR.Standalone.Location location5 = location4;
        string aLocationType2 = str;
        isBranch = main.IsBranch;
        int num2 = isBranch.GetValueOrDefault() ? 1 : 0;
        defLocationExt2.InitLocation((IBqlTable) location5, aLocationType2, num2 != 0);
        ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension2.Locations).Update(location4);
      }
    }
    UDFHelper.CopyAttributes(((PXSelectBase) this.Accounts).Cache, (object) current, ((PXSelectBase) instance.BAccount).Cache, (object) ((PXSelectBase<VendorR>) instance.BAccount).Current, vendorR.ClassID);
    ((PXGraph) instance).Caches[typeof (CSAnswers)].Clear();
    ((PXGraph) instance).Caches[typeof (CSAnswers)].ClearQueryCache();
    object note = (object) PXNoteAttribute.GetNote(((PXSelectBase) instance.BAccount).Cache, (object) vendorR);
    ((PXSelectBase) instance.BAccount).Cache.RaiseFieldUpdating("NoteText", (object) vendorR, ref note);
    return instance;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewVendor(PXAdapter adapter)
  {
    ExtendToVendorGraph<TGraph, TPrimary>.SourceAccount current = ((PXSelectBase<ExtendToVendorGraph<TGraph, TPrimary>.SourceAccount>) this.Accounts).Current;
    if (current != null && (current.Type == "VE" || current.Type == "VC"))
    {
      VendorMaint instance = PXGraph.CreateInstance<VendorMaint>();
      ((PXSelectBase<VendorR>) instance.BAccount).Current = PXResultset<VendorR>.op_Implicit(((PXSelectBase<VendorR>) instance.BAccount).Search<VendorR.acctCD>((object) current.AcctCD, Array.Empty<object>()));
      throw new PXRedirectRequiredException((PXGraph) instance, "Edit Vendor");
    }
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<TPrimary> e)
  {
    PXEntryStatus status = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<TPrimary>>) e).Cache.GetStatus((object) e.Row);
    ExtendToVendorGraph<TGraph, TPrimary>.SourceAccount current = ((PXSelectBase<ExtendToVendorGraph<TGraph, TPrimary>.SourceAccount>) this.Accounts).Current;
    if (current == null)
      return;
    PX.Objects.CR.BAccount main = (PX.Objects.CR.BAccount) ((PXSelectBase) this.Accounts).Cache.GetMain<ExtendToVendorGraph<TGraph, TPrimary>.SourceAccount>(current);
    bool flag = true;
    if (status != 2 && main?.Type == "CU")
      flag = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXViewOf<Customer>.BasedOn<SelectFromBase<Customer, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Customer.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
      {
        (object) main.BAccountID
      })) != null;
    ((PXAction) this.extendToVendor).SetEnabled(((current.Type == "OR" || current.Type == "VE" || current.Type == "VC" ? 0 : (status != 2 ? 1 : 0)) & (flag ? 1 : 0)) != 0);
    ((PXAction) this.viewVendor).SetEnabled((current.Type == "VE" || current.Type == "VC") && status != 2);
  }

  [PXHidden]
  public class SourceAccount : PX.Objects.GraphExtensions.ExtendBAccount.SourceAccount<ExtendToVendorGraph<TGraph, TPrimary>>
  {
  }

  /// <summary>A class that defines the default mapping of the <see cref="T:PX.Objects.GraphExtensions.ExtendBAccount.ExtendToVendorGraph`2.SourceAccount" /> mapped cache extension to a DAC.</summary>
  protected class SourceAccountMapping : IBqlMapping
  {
    /// <exclude />
    protected System.Type _extension = typeof (ExtendToVendorGraph<TGraph, TPrimary>.SourceAccount);
    /// <exclude />
    protected System.Type _table;
    /// <exclude />
    public System.Type AcctCD = typeof (PX.Objects.GraphExtensions.ExtendBAccount.SourceAccount<ExtendToVendorGraph<TGraph, TPrimary>>.acctCD);
    /// <exclude />
    public System.Type Type = typeof (PX.Objects.GraphExtensions.ExtendBAccount.SourceAccount<ExtendToVendorGraph<TGraph, TPrimary>>.type);
    /// <exclude />
    public System.Type LocaleName = typeof (PX.Objects.GraphExtensions.ExtendBAccount.SourceAccount<ExtendToVendorGraph<TGraph, TPrimary>>.localeName);

    /// <exclude />
    public System.Type Extension => this._extension;

    /// <exclude />
    public System.Type Table => this._table;

    /// <summary>Creates the default mapping of the <see cref="T:PX.Objects.GraphExtensions.ExtendBAccount.ExtendToVendorGraph`2.SourceAccount" /> mapped cache extension to the specified table.</summary>
    /// <param name="table">A DAC.</param>
    public SourceAccountMapping(System.Type table) => this._table = table;
  }
}
