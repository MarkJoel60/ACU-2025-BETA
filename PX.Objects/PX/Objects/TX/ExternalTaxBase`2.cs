// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.ExternalTaxBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.TaxProvider;
using System;

#nullable disable
namespace PX.Objects.TX;

public abstract class ExternalTaxBase<TGraph, TPrimary> : ExternalTaxBase<TGraph>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  private const string UseTaxSuffix = "USE";
  private const string StrConnector = " ";
  public bool skipExternalTaxCalcOnSave;
  protected Lazy<SalesTaxMaint> LazySalesTaxMaint = new Lazy<SalesTaxMaint>((Func<SalesTaxMaint>) (() => PXGraph.CreateInstance<SalesTaxMaint>()));

  [PXOverride]
  public virtual bool IsExternalTax(string taxZoneID)
  {
    return ExternalTaxBase<TGraph>.IsExternalTax((PXGraph) this.Base, taxZoneID);
  }

  public virtual bool IsNonTaxable(IAddressBase address)
  {
    State state = ((PXSelectBase<State>) new PXSelect<State, Where<State.countryID, Equal<Required<State.countryID>>, And<State.stateID, Equal<Required<State.stateID>>>>>((PXGraph) this.Base)).SelectSingle(new object[2]
    {
      (object) address.CountryID,
      (object) address.State
    });
    return state != null && state.NonTaxable.GetValueOrDefault();
  }

  public virtual string CompanyCodeFromBranch(string taxZoneID, int? branchID)
  {
    return ExternalTaxBase<TGraph>.CompanyCodeFromBranch((PXGraph) this.Base, taxZoneID, branchID);
  }

  public virtual string GetTaxProviderTaxCalcMode(string taxZoneID)
  {
    return PXResultset<TaxPlugin>.op_Implicit(PXSelectBase<TaxPlugin, PXViewOf<TaxPlugin>.BasedOn<SelectFromBase<TaxPlugin, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<TaxZone>.On<BqlOperand<TaxPlugin.taxPluginID, IBqlString>.IsEqual<TaxZone.taxPluginID>>>>.Where<BqlOperand<TaxZone.taxZoneID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) taxZoneID
    }))?.TaxCalcMode;
  }

  [PXOverride]
  public abstract TPrimary CalculateExternalTax(TPrimary document);

  protected virtual void LogMessages(ResultBase result)
  {
    foreach (string message in result.Messages)
      PXTrace.WriteError(message);
  }

  public abstract void SkipTaxCalcAndSave();

  protected virtual Decimal? GetDocDiscount() => new Decimal?();

  protected virtual string GetExternalTaxProviderLocationCode(TPrimary order) => (string) null;

  protected string GetExternalTaxProviderLocationCode<TLine, TLineDocFK, TLineSiteID>(
    TPrimary document)
    where TLine : class, IBqlTable, new()
    where TLineDocFK : IParameterizedForeignKeyBetween<TLine, TPrimary>, new()
    where TLineSiteID : IBqlField
  {
    TLine line = PXResultset<TLine>.op_Implicit(PXSelectBase<TLine, PXSelect<TLine, Where2<TLineDocFK, And<TLineSiteID, IsNotNull>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new TPrimary[1]
    {
      document
    }, Array.Empty<object>()));
    if ((object) line == null)
      return (string) null;
    return INSite.PK.Find((PXGraph) this.Base, (int?) ((PXCache) GraphHelper.Caches<TLine>((PXGraph) this.Base)).GetValue<TLineSiteID>((object) line))?.SiteCD;
  }

  protected virtual Tax CreateTax(
    TGraph graph,
    TaxZone taxZone,
    PX.Objects.AP.Vendor taxAgency,
    TaxDetail taxDetail,
    string taxID = null)
  {
    Tax tax = (Tax) null;
    taxDetail.TaxType = !(taxDetail.TaxType == "P") || !(taxZone.ExternalAPTaxType == "S") ? taxDetail.TaxType : "S";
    taxID = taxID ?? this.GenerateTaxId(taxDetail);
    if (!string.IsNullOrEmpty(taxID))
    {
      tax = PXResultset<Tax>.op_Implicit(PXSelectBase<Tax, PXSelect<Tax, Where<Tax.taxID, Equal<Required<Tax.taxID>>>>.Config>.Select((PXGraph) graph, new object[1]
      {
        (object) taxID
      }));
      if (tax == null)
      {
        string str = PXMessages.LocalizeFormatNoPrefixNLA("External Tax Provider {0} tax for {1}", new object[2]
        {
          (object) taxDetail.JurisType,
          (object) taxDetail.JurisName
        });
        tax = new Tax()
        {
          TaxID = taxID,
          Descr = str.Length > 100 ? str.Substring(0, 100) : str,
          TaxType = taxDetail.TaxType,
          TaxCalcType = "D",
          TaxCalcLevel = "1",
          TaxApplyTermsDisc = "X",
          SalesTaxAcctID = taxAgency.SalesTaxAcctID,
          SalesTaxSubID = taxAgency.SalesTaxSubID,
          ExpenseAccountID = taxAgency.TaxExpenseAcctID,
          ExpenseSubID = taxAgency.TaxExpenseSubID,
          TaxVendorID = taxZone.TaxVendorID,
          IsExternal = new bool?(true)
        };
        SalesTaxMaint salesTaxMaint = this.LazySalesTaxMaint.Value;
        ((PXGraph) salesTaxMaint).Clear();
        ((PXSelectBase<Tax>) salesTaxMaint.Tax).Insert(tax);
        ((PXAction) salesTaxMaint.Save).Press();
      }
    }
    return tax;
  }

  protected virtual PX.Objects.AP.Vendor GetTaxAgency(
    TGraph graph,
    TaxZone taxZone,
    bool checkSalesTaxAcct = false)
  {
    if (taxZone == null)
      throw new PXException("The Tax Zone is not specified in the document.");
    PX.Objects.AP.Vendor taxAgency = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>.Config>.Select((PXGraph) graph, new object[1]
    {
      (object) taxZone.TaxVendorID
    }));
    if (taxAgency == null)
      throw new PXException("Tax Vendor is required but not found for the External TaxZone.");
    if (checkSalesTaxAcct)
    {
      if (!taxAgency.SalesTaxAcctID.HasValue)
        throw new PXException("The Tax Payable account should be specified for tax agency '{0}'.", new object[1]
        {
          (object) taxAgency.AcctCD
        });
      if (!taxAgency.SalesTaxSubID.HasValue)
        throw new PXException("The Tax Payable account should be specified for tax agency '{0}'.", new object[1]
        {
          (object) taxAgency.AcctCD
        });
    }
    return taxAgency;
  }

  protected virtual string GenerateTaxId(TaxDetail taxDetail)
  {
    string taxId = taxDetail.TaxName;
    if (string.IsNullOrEmpty(taxId))
      taxId = taxDetail.JurisCode;
    if (!string.IsNullOrEmpty(taxId))
    {
      if (taxDetail.TaxType == "P")
      {
        if (taxId.Length > 55)
          taxId = taxId.Substring(0, 55);
        taxId += " USE";
      }
    }
    else
      PXTrace.WriteInformation("Taxes returned by external tax provider has no tax code and tax zone specified. Please check settings configured in external tax provider.");
    return taxId;
  }
}
