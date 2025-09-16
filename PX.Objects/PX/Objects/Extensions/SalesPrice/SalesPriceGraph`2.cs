// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.SalesPrice.SalesPriceGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.Extensions.SalesPrice;

/// <summary>A generic graph extension that defines the functionality for working with multiple price lists.</summary>
/// <typeparam name="TGraph">A <see cref="T:PX.Data.PXGraph" /> type.</typeparam>
/// <typeparam name="TPrimary">A DAC (a <see cref="T:PX.Data.IBqlTable" /> type).</typeparam>
public abstract class SalesPriceGraph<TGraph, TPrimary> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  /// <summary>A mapping-based view of the <see cref="T:PX.Objects.Extensions.SalesPrice.Document" /> data.</summary>
  public PXSelectExtension<Document> Documents;
  /// <summary>A mapping-based view of the <see cref="T:PX.Objects.Extensions.SalesPrice.Detail" /> data.</summary>
  public PXSelectExtension<Detail> Details;
  /// <summary>A mapping-based view of the <see cref="F:PX.Objects.Extensions.SalesPrice.SalesPriceGraph`2.PriceClassSource" /> data.</summary>
  public PXSelectExtension<PX.Objects.Extensions.SalesPrice.PriceClassSource> PriceClassSource;
  /// <summary>The current record of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the document</summary>
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<Document.curyInfoID>>>> currencyinfo;

  /// <summary>Returns the mapping of the <see cref="T:PX.Objects.Extensions.SalesPrice.Document" /> mapped cache extension to a DAC. This method must be overridden in the implementation class of the base graph.</summary>
  /// <remarks>In the implementation graph for a particular graph, you can either return the default mapping or override the default mapping in this method.</remarks>
  /// <example><para>The following code shows the method that overrides the GetDocumentMapping() method in the implementation class. The method returns the default mapping of the Document mapped cache extension to the CROpportunity DAC.</para>
  ///   <code title="Example" lang="CS">
  /// protected override DocumentMapping GetDocumentMapping()
  /// {
  ///       return new DocumentMapping(typeof(CROpportunity));
  /// }</code>
  /// </example>
  protected abstract SalesPriceGraph<TGraph, TPrimary>.DocumentMapping GetDocumentMapping();

  /// <summary>Returns the mapping of the <see cref="T:PX.Objects.Extensions.SalesPrice.Detail" /> mapped cache extension to a DAC. This method must be overridden in the implementation class of the base graph.</summary>
  /// <remarks>In the implementation graph for a particular graph, you can either return the default mapping or override the default mapping in this method.</remarks>
  /// <example><para>The following code shows the method that overrides the GetDetailMapping() method in the implementation class. The method overrides the default mapping of the Detail mapped cache extension to a DAC: For the CROpportunityProducts DAC, the mapping of the CuryLineAmount and Descr fields is changed.</para>
  ///   <code title="Example" lang="CS">
  /// protected override DetailMapping GetDetailMapping()
  /// {
  ///     return new DetailMapping(typeof(CROpportunityProducts)) { CuryLineAmount = typeof(CROpportunityProducts.curyAmount), Descr = typeof(CROpportunityProducts.transactionDescription)};
  /// }</code>
  /// </example>
  protected abstract SalesPriceGraph<TGraph, TPrimary>.DetailMapping GetDetailMapping();

  /// <summary>Returns the mapping of the <see cref="F:PX.Objects.Extensions.SalesPrice.SalesPriceGraph`2.PriceClassSource" /> mapped cache extension to a DAC. This method must be overridden in the implementation class of the base graph.</summary>
  /// <remarks>In the implementation graph for a particular graph, you can either return the default mapping or override the default mapping in this method.</remarks>
  /// <example><para>The following code shows the method that overrides the GetPriceClassSourceMapping() method in the implementation class. The method overrides the default mapping of the PriceClassSource mapped cache extension to a DAC: For the Location DAC, the PriceClassID field of the mapped cache extension is mapped to the cPriceClassID field of the DAC; other fields of the extension are mapped by default.</para>
  ///   <code title="Example" lang="CS">
  /// protected override PriceClassSourceMapping GetPriceClassSourceMapping()
  /// {
  ///    return new PriceClassSourceMapping(typeof(Location)) {PriceClassID = typeof(Location.cPriceClassID)};
  /// }</code>
  /// </example>
  protected abstract SalesPriceGraph<TGraph, TPrimary>.PriceClassSourceMapping GetPriceClassSourceMapping();

  /// <summary>Returns the sales price for the specified detail row.</summary>
  /// <param name="row">A detail line.</param>
  /// <param name="sender">A cache object.</param>
  protected virtual Decimal GetPrice(PXCache sender, Detail row)
  {
    string custPriceClass = "BASE";
    PX.Objects.Extensions.SalesPrice.PriceClassSource priceClassSource = (PX.Objects.Extensions.SalesPrice.PriceClassSource) this.PriceClassSource.Select();
    if (!string.IsNullOrEmpty(priceClassSource?.PriceClassID))
      custPriceClass = priceClassSource.PriceClassID;
    Document current = this.Documents.Current;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.SelectCurrencyInfo(current);
    string taxCalcMode = string.IsNullOrEmpty(current.TaxCalcMode) ? "T" : current.TaxCalcMode;
    return this.GetSalesPriceItemAndCalculatedPrice(sender, custPriceClass, current.BAccountID, row.InventoryID, row.SiteID, currencyInfo.GetCM(), row.UOM, row.Quantity, current.DocumentDate ?? sender.Graph.Accessinfo.BusinessDate.Value, row.CuryUnitPrice, taxCalcMode).Item2.GetValueOrDefault();
  }

  protected virtual (ARSalesPriceMaint.SalesPriceItem, Decimal?) GetSalesPriceItemAndCalculatedPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    Decimal? quantity,
    System.DateTime date,
    Decimal? currentUnitPrice,
    string taxCalcMode)
  {
    return ARSalesPriceMaint.SingleARSalesPriceMaint.GetSalesPriceItemAndCalculatedPrice(sender, custPriceClass, customerID, inventoryID, siteID, currencyinfo, UOM, quantity, date, currentUnitPrice, taxCalcMode);
  }

  /// <summary>Returns the current <see cref="T:PX.Objects.CM.CurrencyInfo" /> record associated with the specified document.</summary>
  /// <param name="doc">A document.</param>
  protected virtual PX.Objects.CM.Extensions.CurrencyInfo SelectCurrencyInfo(Document doc)
  {
    if (!doc.CuryInfoID.HasValue)
      return (PX.Objects.CM.Extensions.CurrencyInfo) null;
    if (this.currencyinfo.Cache.Current is PX.Objects.CM.Extensions.CurrencyInfo current)
    {
      long? curyInfoId1 = doc.CuryInfoID;
      long? curyInfoId2 = current.CuryInfoID;
      if (curyInfoId1.GetValueOrDefault() == curyInfoId2.GetValueOrDefault() & curyInfoId1.HasValue == curyInfoId2.HasValue)
        return current;
    }
    return this.currencyinfo.SelectSingle();
  }

  /// <summary>The FieldUpdated2 event handler for the <see cref="P:PX.Objects.Extensions.SalesPrice.Detail.InventoryID" /> field. When the InventoryID field value is changed, <see cref="P:PX.Objects.Extensions.SalesPrice.Detail.Descr" />, <see cref="P:PX.Objects.Extensions.SalesPrice.Detail.UOM" />,
  /// <see cref="P:PX.Objects.Extensions.SalesPrice.Detail.Quantity" /> are assigned the default values.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.FieldUpdated<Detail, Detail.inventoryID> e)
  {
    if (e.ExternalCall)
      e.Cache.SetValue<Detail.curyUnitPrice>((object) e.Row, (object) 0M);
    e.Cache.SetDefaultExt<Detail.descr>((object) e.Row);
    e.Cache.RaiseExceptionHandling<Detail.uOM>((object) e.Row, (object) null, (Exception) null);
    e.Cache.SetDefaultExt<Detail.uOM>((object) e.Row);
    e.Cache.SetDefaultExt<Detail.quantity>((object) e.Row);
  }

  /// <summary>The FieldDefaulting2 event handler for the <see cref="P:PX.Objects.Extensions.SalesPrice.Detail.Descr" /> field.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.FieldDefaulting<Detail, Detail.descr> e)
  {
    PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<Detail.inventoryID>(this.Details.Cache, (object) e.Row);
    e.NewValue = (object) PXDBLocalizableStringAttribute.GetTranslation<PX.Objects.IN.InventoryItem.descr>((PXCache) this.Base.Caches<PX.Objects.IN.InventoryItem>(), (object) inventoryItem);
    object destinationData = e.Row?.Base;
    if (string.IsNullOrEmpty(inventoryItem?.Descr) || destinationData == null)
      return;
    PXDBLocalizableStringAttribute.CopyTranslations<PX.Objects.IN.InventoryItem.descr, Detail.descr>((PXCache) this.Base.Caches<PX.Objects.IN.InventoryItem>(), (object) inventoryItem, this.Base.Caches[destinationData.GetType()], destinationData);
  }

  /// <summary>The FieldDefaulting2 event handler for the <see cref="P:PX.Objects.Extensions.SalesPrice.Detail.CuryUnitPrice" /> field.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(
    PX.Data.Events.FieldDefaulting<Detail, Detail.curyUnitPrice> e)
  {
    Detail row = e.Row;
    if (row == null)
      return;
    e.NewValue = (object) new Decimal?(!this.Base.IsCopyPasteContext ? this.GetPrice(e.Cache, row) : 0M).GetValueOrDefault();
  }

  /// <summary>The FieldUpdated2 event handler for the <see cref="P:PX.Objects.Extensions.SalesPrice.Detail.UOM" /> field. When the UOM field value is changed, <see cref="P:PX.Objects.Extensions.SalesPrice.Detail.CuryUnitPrice" /> is assinged a new value.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.FieldUpdated<Detail, Detail.uOM> e)
  {
    if (e.Row.ManualPrice.GetValueOrDefault() || e.Row.IsFree.GetValueOrDefault() || e.Cache.Graph.IsCopyPasteContext)
      return;
    e.Cache.SetDefaultExt<Detail.curyUnitPrice>((object) e.Row);
  }

  /// <summary>The FieldUpdated2 event handler for the <see cref="P:PX.Objects.Extensions.SalesPrice.Detail.Quantity" /> field. When the Quantity field value is changed, <see cref="P:PX.Objects.Extensions.SalesPrice.Detail.CuryUnitPrice" /> is assinged a new value.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.FieldUpdated<Detail, Detail.quantity> e)
  {
    if (e.Row.ManualPrice.GetValueOrDefault() || e.Row.IsFree.GetValueOrDefault() || e.Cache.Graph.IsCopyPasteContext)
      return;
    e.Cache.SetDefaultExt<Detail.curyUnitPrice>((object) e.Row);
  }

  /// <summary>The FieldUpdated2 event handler for the <see cref="P:PX.Objects.Extensions.SalesPrice.Detail.SiteID" /> field. When the Warehouse field value is changed, <see cref="P:PX.Objects.Extensions.SalesPrice.Detail.CuryUnitPrice" /> is assinged a new value.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.FieldUpdated<Detail, Detail.siteID> e)
  {
    if (e.Row.ManualPrice.GetValueOrDefault() || e.Row.IsFree.GetValueOrDefault() || e.Cache.Graph.IsCopyPasteContext || e.Cache.Graph.IsImportFromExcel)
      return;
    e.Cache.SetDefaultExt<Detail.curyUnitPrice>((object) e.Row);
  }

  /// <summary>The FieldUpdated2 event handler for the <see cref="P:PX.Objects.Extensions.SalesPrice.Detail.IsFree" /> field. When the IsFree field value is changed, <see cref="P:PX.Objects.Extensions.SalesPrice.Detail.CuryUnitPrice" /> is assinged a new value.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.FieldUpdated<Detail, Detail.isFree> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.IsFree.GetValueOrDefault())
    {
      e.Cache.SetValueExt<Detail.curyUnitPrice>((object) e.Row, (object) 0M);
    }
    else
    {
      if (e.Row.ManualPrice.GetValueOrDefault() || e.Cache.Graph.IsCopyPasteContext)
        return;
      e.Cache.SetDefaultExt<Detail.curyUnitPrice>((object) e.Row);
    }
  }

  /// <summary>The RowUpdated event handler for the <see cref="T:PX.Objects.Extensions.SalesPrice.Detail" /> mapped cache extension.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.RowUpdated<Detail> e)
  {
    if ((e.ExternalCall || e.Cache.Graph.IsImport) && e.Cache.ObjectsEqual<Detail.inventoryID>((object) e.Row, (object) e.OldRow) && e.Cache.ObjectsEqual<Detail.uOM>((object) e.Row, (object) e.OldRow) && e.Cache.ObjectsEqual<Detail.quantity>((object) e.Row, (object) e.OldRow) && e.Cache.ObjectsEqual<Detail.branchID>((object) e.Row, (object) e.OldRow) && e.Cache.ObjectsEqual<Detail.siteID>((object) e.Row, (object) e.OldRow) && (!e.Cache.ObjectsEqual<Detail.curyUnitPrice>((object) e.Row, (object) e.OldRow) || !e.Cache.ObjectsEqual<Detail.curyLineAmount>((object) e.Row, (object) e.OldRow)))
      e.Cache.SetValueExt<Detail.manualPrice>((object) e.Row, (object) true);
    if (!e.Row.InventoryID.HasValue)
      return;
    bool? nullable = e.Row.ManualPrice;
    if (nullable.GetValueOrDefault())
      return;
    nullable = e.Row.IsFree;
    if (nullable.GetValueOrDefault() || e.Cache.Graph.IsCopyPasteContext)
      return;
    string custPriceClass = "BASE";
    PX.Objects.Extensions.SalesPrice.PriceClassSource priceClassSource = (PX.Objects.Extensions.SalesPrice.PriceClassSource) this.PriceClassSource.Select();
    if (!string.IsNullOrEmpty(priceClassSource?.PriceClassID))
      custPriceClass = priceClassSource.PriceClassID;
    Document current = this.Documents.Current;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.SelectCurrencyInfo(current);
    string taxCalcMode = string.IsNullOrEmpty(current.TaxCalcMode) ? "T" : current.TaxCalcMode;
    ARSalesPriceMaint arSalesPriceMaint = ARSalesPriceMaint.SingleARSalesPriceMaint;
    bool baseCurrencySetting = arSalesPriceMaint.GetAlwaysFromBaseCurrencySetting(e.Cache);
    try
    {
      ARSalesPriceMaint.SalesPriceItem salesPrice = arSalesPriceMaint.FindSalesPrice(e.Cache, custPriceClass, current.BAccountID, e.Row.InventoryID, e.Row.SiteID, currencyInfo.BaseCuryID, baseCurrencySetting ? currencyInfo.BaseCuryID : currencyInfo.CuryID, new Decimal?(System.Math.Abs(e.Row.Quantity.GetValueOrDefault())), e.Row.UOM, current.DocumentDate ?? e.Cache.Graph.Accessinfo.BusinessDate.Value, taxCalcMode);
      e.Cache.SetValue<Detail.skipLineDiscounts>((object) e.Row, (object) (bool) (salesPrice != null ? (salesPrice.SkipLineDiscounts ? 1 : 0) : 0));
    }
    catch (PXUnitConversionException ex)
    {
    }
  }

  /// <summary>The RowSelected event handler for the <see cref="T:PX.Objects.Extensions.SalesPrice.Detail" /> mapped cache extension.</summary>
  /// <param name="e">Parameters of the event</param>
  protected virtual void _(PX.Data.Events.RowSelected<Detail> e)
  {
    PXCache cache = e.Cache;
    Detail row1 = e.Row;
    Detail row2 = e.Row;
    int num = row2 != null ? (!row2.IsFree.GetValueOrDefault() ? 1 : 0) : 1;
    PXUIFieldAttribute.SetEnabled<Detail.curyUnitPrice>(cache, (object) row1, num != 0);
  }

  /// <summary>A class that defines the default mapping of the <see cref="T:PX.Objects.Extensions.SalesPrice.Document" /> mapped cache extension to a DAC.</summary>
  protected class DocumentMapping : IBqlMapping
  {
    /// <exclude />
    protected System.Type _extension = typeof (Document);
    /// <exclude />
    protected System.Type _table;
    /// <exclude />
    public System.Type BranchID = typeof (Document.branchID);
    /// <exclude />
    public System.Type BAccountID = typeof (Document.bAccountID);
    /// <exclude />
    public System.Type CuryInfoID = typeof (Document.curyInfoID);
    /// <exclude />
    public System.Type DocumentDate = typeof (Document.documentDate);
    /// <exclude />
    public System.Type TaxCalcMode = typeof (Document.taxCalcMode);

    /// <exclude />
    public System.Type Extension => this._extension;

    /// <exclude />
    public System.Type Table => this._table;

    /// <summary>Creates the default mapping of the <see cref="T:PX.Objects.Extensions.SalesPrice.Document" /> mapped cache extension to the specified table.</summary>
    /// <param name="table">A DAC.</param>
    public DocumentMapping(System.Type table) => this._table = table;
  }

  /// <summary>A class that defines the default mapping of the <see cref="T:PX.Objects.Extensions.SalesPrice.Detail" /> mapped cache extension to a DAC.</summary>
  protected class DetailMapping : IBqlMapping
  {
    /// <exclude />
    protected System.Type _table;
    /// <exclude />
    public System.Type BranchID = typeof (Detail.branchID);
    /// <exclude />
    public System.Type InventoryID = typeof (Detail.inventoryID);
    /// <exclude />
    public System.Type Descr = typeof (Detail.descr);
    /// <exclude />
    public System.Type SiteID = typeof (Detail.siteID);
    /// <exclude />
    public System.Type UOM = typeof (Detail.uOM);
    /// <exclude />
    public System.Type Quantity = typeof (Detail.quantity);
    /// <exclude />
    public System.Type CuryUnitPrice = typeof (Detail.curyUnitPrice);
    /// <exclude />
    public System.Type CuryLineAmount = typeof (Detail.curyLineAmount);
    /// <exclude />
    public System.Type ManualPrice = typeof (Detail.manualPrice);
    /// <exclude />
    public System.Type IsFree = typeof (Detail.isFree);
    /// <exclude />
    public System.Type SkipLineDiscounts = typeof (Detail.skipLineDiscounts);

    /// <exclude />
    public System.Type Extension => typeof (Detail);

    /// <exclude />
    public System.Type Table => this._table;

    /// <summary>Creates the default mapping of the <see cref="T:PX.Objects.Extensions.SalesPrice.Detail" /> mapped cache extension to the specified table.</summary>
    /// <param name="table">A DAC.</param>
    public DetailMapping(System.Type table) => this._table = table;
  }

  /// <summary>A class that defines the default mapping of the <see cref="F:PX.Objects.Extensions.SalesPrice.SalesPriceGraph`2.PriceClassSource" /> mapped cache extension to a DAC.</summary>
  protected class PriceClassSourceMapping : IBqlMapping
  {
    /// <exclude />
    protected System.Type _table;
    /// <exclude />
    public System.Type PriceClassID = typeof (PX.Objects.Extensions.SalesPrice.PriceClassSource.priceClassID);

    /// <exclude />
    public System.Type Extension => typeof (PX.Objects.Extensions.SalesPrice.PriceClassSource);

    /// <exclude />
    public System.Type Table => this._table;

    /// <summary>Creates the default mapping of the <see cref="F:PX.Objects.Extensions.SalesPrice.SalesPriceGraph`2.PriceClassSource" /> mapped cache extension to the specified table.</summary>
    /// <param name="table">A DAC.</param>
    public PriceClassSourceMapping(System.Type table) => this._table = table;
  }
}
