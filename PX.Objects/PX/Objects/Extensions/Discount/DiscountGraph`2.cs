// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.Discount.DiscountGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Objects.Extensions.SalesTax;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Extensions.Discount;

/// <summary>The generic graph extension that includes the functionality for applying discounts.</summary>
public abstract class DiscountGraph<TGraph, TPrimary> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  /// <summary>A mapping-based view of the <see cref="T:PX.Objects.Extensions.Discount.Document" /> data.</summary>
  public PXSelectExtension<Document> Documents;
  /// <summary>A mapping-based view of the <see cref="T:PX.Objects.Extensions.Discount.Detail" /> data.</summary>
  public PXSelectExtension<Detail> Details;
  /// <summary>A mapping-based view of the <see cref="T:PX.Objects.Extensions.Discount.Discount" /> data.</summary>
  public PXSelectExtension<PX.Objects.Extensions.Discount.Discount> Discounts;
  /// <summary>The filter for the <strong>Recalculate Prices</strong> action (<see cref="F:PX.Objects.Extensions.Discount.DiscountGraph`2.recalculateDiscountsAction" />).</summary>
  public PXFilter<RecalcDiscountsParamFilter> recalcdiscountsfilter;
  /// <summary>The <strong>Recalculate Prices</strong> action.</summary>
  public PXAction<TPrimary> recalculateDiscountsAction;
  private bool SkipTaxesRecalc;

  /// <summary>The CacheAttached event handler for the <see cref="T:PX.Objects.Extensions.Discount.Discount.curyInfoID" /> field of the <see cref="T:PX.Objects.Extensions.Discount.Discount" /> mapped cache extension. You must override this method in
  /// the implementation class of the base graph.</summary>
  /// <param name="sender">The cache object that raised the event.</param>
  /// <example><para>The following code shows sample implementation of the method in the implementation class for the OpportunityMaint graph.</para>
  ///   <code title="Example" lang="CS">
  /// [CurrencyInfo(typeof(CROpportunity.curyInfoID))]
  /// [PXMergeAttributes]
  /// public override void Discount_CuryInfoID_CacheAttached(PXCache sender)
  /// {
  /// }</code>
  /// </example>
  public abstract void Discount_CuryInfoID_CacheAttached(PXCache sender);

  /// <summary>The CacheAttached event handler for the <see cref="T:PX.Objects.Extensions.Discount.Discount.discountID" /> field of the <see cref="T:PX.Objects.Extensions.Discount.Discount" /> mapped cache extension. You must override this method in
  /// the implementation class of the base graph.</summary>
  /// <param name="sender">The cache object that raised the event.</param>
  /// <example><para>The following code shows sample implementation of the method in the implementation class for the OpportunityMaint graph.</para>
  ///   <code title="Example" lang="CS">
  /// [PXSelector(typeof(Search&lt;ARDiscount.discountID,
  ///     Where&lt;ARDiscount.type, NotEqual&lt;DiscountType.LineDiscount&gt;,
  ///       And&lt;ARDiscount.applicableTo, NotEqual&lt;DiscountTarget.warehouse&gt;,
  ///       And&lt;ARDiscount.applicableTo, NotEqual&lt;DiscountTarget.warehouseAndCustomer&gt;,
  ///       And&lt;ARDiscount.applicableTo, NotEqual&lt;DiscountTarget.warehouseAndCustomerPrice&gt;,
  ///       And&lt;ARDiscount.applicableTo, NotEqual&lt;DiscountTarget.warehouseAndInventory&gt;,
  ///       And&lt;ARDiscount.applicableTo, NotEqual&lt;DiscountTarget.warehouseAndInventoryPrice&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;))]
  /// [PXMergeAttributes]
  /// public override void Discount_DiscountID_CacheAttached(PXCache sender)
  /// {
  /// }</code>
  /// </example>
  public abstract void Discount_DiscountID_CacheAttached(PXCache sender);

  /// <summary>Returns the mapping of the <see cref="T:PX.Objects.Extensions.Discount.Document" /> mapped cache extension to a DAC. This method must be overridden in the implementation class of the base graph.</summary>
  /// <remarks>In the implementation graph for a particular graph, you can either return the default mapping or override the default mapping in this method.</remarks>
  /// <example><para>The following code shows the method that overrides the GetDocumentMapping() method in the implementation class. The  method overrides the default mapping of the %Document% mapped cache extension to a DAC: For the CROpportunity DAC, the CuryDiscTot field of the mapped cache extension is mapped to the curyDocDiscTot of the DAC; other fields of the extension are mapped by default.</para>
  ///   <code title="Example" lang="CS">
  /// protected override DocumentMapping GetDocumentMapping()
  /// {
  ///    return new DocumentMapping(typeof(CROpportunity)){CuryDiscTot = typeof(CROpportunity.curyDocDiscTot) };
  /// }</code>
  /// </example>
  protected abstract DiscountGraph<TGraph, TPrimary>.DocumentMapping GetDocumentMapping();

  /// <summary>Returns the mapping of the <see cref="T:PX.Objects.Extensions.Discount.Detail" /> mapped cache extension to a DAC. This method must be overridden in the implementation class of the base graph.</summary>
  /// <remarks>In the implementation graph for a particular graph, you can either return the default mapping or override the default mapping in this method.</remarks>
  /// <example>
  ///   <code title="Example" lang="C#">
  /// //The following code shows the method that overrides the GetDetailMapping() method
  /// //in the implementation class. The method overrides the default mapping
  /// //of the Detail mapped cache extension to a DAC: For the CROpportunityProducts DAC,
  /// //mapping of the two fields of the mapped cache extension is overridden.
  /// protected override DetailMapping GetDetailMapping()
  /// {
  ///    return new DetailMapping(typeof(CROpportunityProducts)) { CuryLineAmount = typeof(CROpportunityProducts.curyAmount), Quantity = typeof(CROpportunityProducts.quantity)};
  /// }</code>
  /// </example>
  protected abstract DiscountGraph<TGraph, TPrimary>.DetailMapping GetDetailMapping();

  /// <summary>Returns the mapping of the <see cref="T:PX.Objects.Extensions.Discount.Discount" /> mapped cache extension to a DAC. This method must be overridden in the implementation class of the base graph.</summary>
  /// <remarks>In the implementation graph for a particular graph, you can either return the default mapping or override the default mapping in this method.</remarks>
  /// <example><para>The following code shows the method that overrides the GetDiscountMapping() method in the implementation class. The method returns the defaul mapping of the %Discount% mapped cache extension to the CROpportunityDiscountDetail DAC.</para>
  ///   <code title="Example" lang="CS">
  /// protected override DiscountMapping GetDiscountMapping()
  /// {
  ///    return new DiscountMapping(typeof(CROpportunityDiscountDetail));
  /// }</code>
  /// </example>
  protected abstract DiscountGraph<TGraph, TPrimary>.DiscountMapping GetDiscountMapping();

  public DiscountEngine<Detail, PX.Objects.Extensions.Discount.Discount> DiscountEngineGraph
  {
    get => DiscountEngineProvider.GetEngineFor<Detail, PX.Objects.Extensions.Discount.Discount>();
  }

  public virtual DiscountEngine.DiscountCalculationOptions DefaultCalculationOptions
  {
    get => DiscountEngine.DiscountCalculationOptions.DisableAPDiscountsCalculation;
  }

  [PXUIField(DisplayName = "Recalculate Prices", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Select, Visible = false)]
  [PXButton]
  public virtual IEnumerable RecalculateDiscountsAction(PXAdapter adapter)
  {
    if (!adapter.ExternalCall || this.recalcdiscountsfilter.AskExt() == WebDialogResult.OK)
    {
      this.DiscountEngineGraph.RecalculatePricesAndDiscounts(this.Details.Cache, (PXSelectBase<Detail>) this.Details, this.Details.Current, (PXSelectBase<PX.Objects.Extensions.Discount.Discount>) this.Discounts, new int?(this.Documents.Current.LocationID.GetValueOrDefault()), this.Documents.Current.DocumentDate, this.recalcdiscountsfilter.Current, this.DefaultCalculationOptions);
      if (!this.Base.IsDirty && this.Details.Cache.IsDirty)
      {
        object main = this.Documents.Cache.GetMain<Document>(this.Documents.Current);
        this.Base.Caches[main.GetType()].Update(main);
      }
    }
    return adapter.Get();
  }

  /// <summary>The method overrides the <tt>Persist</tt> method of the base graph.</summary>
  [PXOverride]
  public virtual void Persist(System.Action del)
  {
    if (this.Documents.Current != null && !this.Discounts.Any<PX.Objects.Extensions.Discount.Discount>() && this.Documents.Current.CuryDiscTot.GetValueOrDefault() != 0M)
      this.AddDiscount(this.Documents.Cache, this.Documents.Current);
    this.DiscountEngineGraph.ValidateDiscountDetails((PXSelectBase<PX.Objects.Extensions.Discount.Discount>) this.Discounts);
    del();
  }

  /// <summary>Sets the default discount account and subaccount for the specified detail line. You must override this method in the implementation class for a particular
  /// graph.</summary>
  /// <param name="discount">The detail line.</param>
  protected abstract void DefaultDiscountAccountAndSubAccount(Detail discount);

  /// <summary>You must override this property in the implementation class for a particular graph.</summary>
  /// <example><para>The following code shows sample implemetation of the method in the implementation class for a particular graph.</para>
  ///   <code title="Example" lang="CS">
  /// protected override bool AddDocumentDiscount =&gt; true;</code>
  /// </example>
  protected abstract bool AddDocumentDiscount { get; }

  /// <summary>Adds the discount to the specified document.</summary>
  public virtual void AddDiscount(PXCache sender, Document row)
  {
    if (this.GetDetailMapping().FreezeManualDisc == typeof (Detail.freezeManualDisc))
      return;
    Detail detail = (Detail) this.Details.Select();
    if (detail == null)
    {
      Detail instance = (Detail) this.Details.Cache.CreateInstance();
      instance.FreezeManualDisc = new bool?(true);
      detail = (Detail) this.Details.Cache.Insert((object) instance);
    }
    Detail copy = (Detail) this.Details.Cache.CreateCopy((object) detail);
    detail.CuryLineAmount = row.CuryDiscTot;
    detail.TaxCategoryID = (string) null;
    this.DefaultDiscountAccountAndSubAccount(detail);
    if (this.Details.Cache.GetStatus((object) detail) == PXEntryStatus.Notchanged)
      this.Details.Cache.SetStatus((object) detail, PXEntryStatus.Updated);
    detail.ManualDisc = new bool?(true);
    this.Details.Cache.RaiseRowUpdated((object) detail, (object) copy);
    Decimal documentDiscount = this.DiscountEngineGraph.GetTotalGroupAndDocumentDiscount<PX.Objects.Extensions.Discount.Discount>((PXSelectBase<PX.Objects.Extensions.Discount.Discount>) this.Discounts);
    Decimal? curyLineAmount = detail.CuryLineAmount;
    Decimal valueOrDefault = curyLineAmount.GetValueOrDefault();
    if (!(documentDiscount == valueOrDefault & curyLineAmount.HasValue))
      return;
    detail.ManualDisc = new bool?(false);
  }

  /// <summary>The FieldUpdated event handler for the <see cref="P:PX.Objects.Extensions.Discount.Detail.DiscountID" /> field. When the DiscountID field value is changed, the discount is recalculated for the current line.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.FieldUpdated<Detail, Detail.discountID> e)
  {
    Document current = this.Documents.Current;
    if (!e.ExternalCall || e.Row == null)
      return;
    this.DiscountEngineGraph.UpdateManualLineDiscount(this.Details.Cache, (PXSelectBase<Detail>) this.Details, e.Row, (PXSelectBase<PX.Objects.Extensions.Discount.Discount>) this.Discounts, current.BranchID, new int?(current.LocationID.GetValueOrDefault()), current.DocumentDate ?? new PXGraph().Accessinfo.BusinessDate, this.DefaultCalculationOptions);
    this.Details.Cache.RaiseFieldUpdated<Detail.curyDiscAmt>((object) e.Row, (object) 0);
  }

  /// <summary>The FieldUpdated event handler for the <see cref="P:PX.Objects.Extensions.Discount.Detail.IsFree" /> field. When the IsFree field value is changed, the price and discount are recalculated for the current line.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.FieldUpdated<Detail, Detail.isFree> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.IsFree.GetValueOrDefault())
    {
      e.Cache.SetValueExt<Detail.curyUnitPrice>((object) e.Row, (object) 0M);
      e.Cache.SetValueExt<Detail.discPct>((object) e.Row, (object) 0M);
      e.Cache.SetValueExt<Detail.curyDiscAmt>((object) e.Row, (object) 0M);
      if (!e.ExternalCall)
        return;
      e.Cache.SetValueExt<Detail.manualDisc>((object) e.Row, (object) true);
    }
    else
      e.Cache.SetDefaultExt<Detail.curyUnitPrice>((object) e.Row);
  }

  /// <summary>The RowSelected event handler for the <see cref="T:PX.Objects.Extensions.Discount.Detail" /> mapped cache extension.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.RowSelected<Detail> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<Detail.manualDisc>(this.Details.Cache, (object) e.Row, !e.Row.IsFree.GetValueOrDefault());
    bool? nullable = e.Row.ManualDisc;
    int num;
    if (!nullable.GetValueOrDefault())
    {
      nullable = e.Row.IsFree;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    bool flag = num != 0;
    if (flag)
    {
      PXUIFieldAttribute.SetEnabled(this.Details.Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<Detail.siteID>(this.Details.Cache, (object) e.Row);
    }
    PXUIFieldAttribute.SetEnabled<Detail.quantity>(this.Details.Cache, (object) e.Row, !flag);
    PXUIFieldAttribute.SetEnabled<Detail.isFree>(this.Details.Cache, (object) e.Row, !flag && e.Row.InventoryID.HasValue);
  }

  /// <summary>The RowUpdated event handler for the <see cref="T:PX.Objects.Extensions.Discount.Detail" /> mapped cache extension. When the value of any field of <see cref="T:PX.Objects.Extensions.Discount.Detail" /> is changed, the discount is recalulated for the current line.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.RowUpdated<Detail> e)
  {
    if ((this.Details.Cache.ObjectsEqual<Detail.branchID>((object) e.Row, (object) e.OldRow) || this.Details.Cache.ObjectsEqual<Detail.inventoryID>((object) e.Row, (object) e.OldRow)) && this.Details.Cache.ObjectsEqual<Detail.siteID>((object) e.Row, (object) e.OldRow) && this.Details.Cache.ObjectsEqual<Detail.quantity>((object) e.Row, (object) e.OldRow) && this.Details.Cache.ObjectsEqual<Detail.curyUnitPrice>((object) e.Row, (object) e.OldRow) && this.Details.Cache.ObjectsEqual<Detail.curyExtPrice>((object) e.Row, (object) e.OldRow) && this.Details.Cache.ObjectsEqual<Detail.curyDiscAmt>((object) e.Row, (object) e.OldRow) && this.Details.Cache.ObjectsEqual<Detail.discPct>((object) e.Row, (object) e.OldRow) && this.Details.Cache.ObjectsEqual<Detail.manualDisc>((object) e.Row, (object) e.OldRow) && this.Details.Cache.ObjectsEqual<Detail.discountID>((object) e.Row, (object) e.OldRow))
      return;
    this.ProcessDiscountsOnDetailRowUpdated(e.Row, e.OldRow, e.Cache);
  }

  protected virtual void ProcessDiscountsOnDetailRowUpdated(
    Detail row,
    Detail oldRow,
    PXCache cache)
  {
    if (!row.ManualDisc.GetValueOrDefault())
    {
      if (oldRow.ManualDisc.GetValueOrDefault() && row.IsFree.GetValueOrDefault())
        this.ResetQtyOnFreeItem(cache.Graph, row);
      if (row.IsFree.GetValueOrDefault())
        this.DiscountEngineGraph.ClearDiscount(cache, row);
    }
    this.RecalculateDiscounts(cache, row, (object) row);
  }

  /// <summary>The RowInserted event handler for the <see cref="T:PX.Objects.Extensions.Discount.Detail" /> mapped cache extension. When a <see cref="T:PX.Objects.Extensions.Discount.Detail" /> line is inserted, the discount is recalulated.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.RowInserted<Detail> e)
  {
    if (e.Row == null || e.Row.ManualDisc.GetValueOrDefault())
      return;
    this.RecalculateDiscounts(e.Cache, e.Row, (object) e.Row);
  }

  /// <summary>The RowDeleted event handler for the <see cref="T:PX.Objects.Extensions.Discount.Detail" /> mapped cache extension. When a <see cref="T:PX.Objects.Extensions.Discount.Detail" /> line is deleted, the discount is recalulated.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.RowDeleted<Detail> e)
  {
    if ((e.Row.ManualDisc.GetValueOrDefault() ? 0 : (e.Row.IsFree.GetValueOrDefault() ? 1 : 0)) != 0)
      return;
    PXCache cache = this.Documents.Cache;
    if (cache.Current == null || cache.GetStatus(cache.Current) == PXEntryStatus.Deleted)
      return;
    Document current = this.Documents.Current;
    this.DiscountEngineGraph.RecalculateGroupAndDocumentDiscounts(this.Details.Cache, (PXSelectBase<Detail>) this.Details, (Detail) null, (PXSelectBase<PX.Objects.Extensions.Discount.Discount>) this.Discounts, current.BranchID, new int?(current.LocationID.GetValueOrDefault()), current.DocumentDate ?? new PXGraph().Accessinfo.BusinessDate, this.DefaultCalculationOptions);
    this.RecalculateTotalDiscount();
    this.RefreshFreeItemLines(this.Details.Cache);
  }

  /// <summary>The RowUpdated event handler for the <see cref="T:PX.Objects.Extensions.Discount.Document" /> mapped cache extension.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.RowUpdated<Document> e)
  {
    if (e.Row == null)
      return;
    System.DateTime? documentDate;
    if (e.ExternalCall && !this.Documents.Cache.ObjectsEqual<Document.documentDate>((object) e.OldRow, (object) e.Row))
    {
      DiscountEngine<Detail, PX.Objects.Extensions.Discount.Discount> discountEngineGraph = this.DiscountEngineGraph;
      PXCache cache = this.Details.Cache;
      PXSelectExtension<Detail> details = this.Details;
      PXSelectExtension<PX.Objects.Extensions.Discount.Discount> discounts = this.Discounts;
      int? locationID = new int?(e.Row.LocationID.GetValueOrDefault());
      documentDate = e.Row.DocumentDate;
      System.DateTime? date = documentDate ?? new PXGraph().Accessinfo.BusinessDate;
      int calculationOptions = (int) this.DefaultCalculationOptions;
      discountEngineGraph.AutoRecalculatePricesAndDiscounts(cache, (PXSelectBase<Detail>) details, (Detail) null, (PXSelectBase<PX.Objects.Extensions.Discount.Discount>) discounts, locationID, date, (DiscountEngine.DiscountCalculationOptions) calculationOptions);
    }
    Decimal? nullable1;
    if (e.ExternalCall && this.Documents.Cache.GetStatus((object) e.Row) != PXEntryStatus.Deleted && !this.Documents.Cache.ObjectsEqual<Document.curyDiscTot>((object) e.OldRow, (object) e.Row))
    {
      Document current = this.Documents.Current;
      DiscountEngine<Detail, PX.Objects.Extensions.Discount.Discount> discountEngineGraph = this.DiscountEngineGraph;
      PXCache cache = this.Details.Cache;
      PXSelectExtension<Detail> details = this.Details;
      PXSelectExtension<PX.Objects.Extensions.Discount.Discount> discounts = this.Discounts;
      nullable1 = current.CuryLineTotal;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = current.CuryDiscTot;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      discountEngineGraph.CalculateDocumentDiscountRate(cache, (PXSelectBase<Detail>) details, (List<Detail>) null, (Detail) null, (PXSelectBase<PX.Objects.Extensions.Discount.Discount>) discounts, valueOrDefault1, valueOrDefault2);
    }
    if (e.Row.CustomerID.HasValue)
    {
      nullable1 = e.Row.CuryDiscTot;
      if (nullable1.HasValue)
      {
        nullable1 = e.Row.CuryDiscTot;
        Decimal num1 = 0M;
        if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
        {
          nullable1 = e.Row.CuryLineTotal;
          if (nullable1.HasValue)
          {
            nullable1 = e.Row.CuryMiscTot;
            if (nullable1.HasValue)
            {
              nullable1 = e.Row.CuryLineTotal;
              Decimal num2 = 0M;
              if (!(nullable1.GetValueOrDefault() > num2 & nullable1.HasValue))
              {
                nullable1 = e.Row.CuryMiscTot;
                Decimal num3 = 0M;
                if (!(nullable1.GetValueOrDefault() > num3 & nullable1.HasValue))
                  goto label_18;
              }
              Decimal discountLimit = this.DiscountEngineGraph.GetDiscountLimit(this.Documents.Cache, e.Row.CustomerID);
              Decimal? curyLineTotal = e.Row.CuryLineTotal;
              Decimal? nullable2 = e.Row.CuryMiscTot;
              Decimal? nullable3 = curyLineTotal.HasValue & nullable2.HasValue ? new Decimal?(curyLineTotal.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
              Decimal num4 = (Decimal) 100;
              Decimal? nullable4;
              if (!nullable3.HasValue)
              {
                nullable2 = new Decimal?();
                nullable4 = nullable2;
              }
              else
                nullable4 = new Decimal?(nullable3.GetValueOrDefault() / num4);
              Decimal? nullable5 = nullable4;
              Decimal num5 = discountLimit;
              nullable1 = nullable5.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * num5) : new Decimal?();
              Decimal? curyDiscTot = e.Row.CuryDiscTot;
              if (nullable1.GetValueOrDefault() < curyDiscTot.GetValueOrDefault() & nullable1.HasValue & curyDiscTot.HasValue)
                PXUIFieldAttribute.SetWarning<Document.curyDiscTot>(this.Documents.Cache, (object) e.Row, $"The total amount of group and document discounts cannot exceed the limit specified for the customer class ({discountLimit:F2}%) on the Customer Classes (AR201000) form.");
            }
          }
        }
      }
    }
label_18:
    int num6 = !this.Documents.Cache.ObjectsEqual<Document.locationID>((object) e.OldRow, (object) e.Row) ? 1 : 0;
    bool flag1 = !this.Documents.Cache.ObjectsEqual<Document.documentDate>((object) e.OldRow, (object) e.Row);
    bool flag2 = !this.Documents.Cache.ObjectsEqual<Document.bAccountID>((object) e.OldRow, (object) e.Row);
    int num7 = flag1 ? 1 : 0;
    if ((num6 | num7 | (flag2 ? 1 : 0)) == 0)
      return;
    RecalcDiscountsParamFilter discountsParamFilter = new RecalcDiscountsParamFilter()
    {
      OverrideManualDiscounts = new bool?(false),
      OverrideManualPrices = new bool?(false),
      RecalcDiscounts = new bool?(true),
      RecalcUnitPrices = new bool?(flag2),
      RecalcTarget = "ALL"
    };
    try
    {
      this.SkipTaxesRecalc = true;
      DiscountEngine<Detail, PX.Objects.Extensions.Discount.Discount> discountEngineGraph = this.DiscountEngineGraph;
      PXCache cache = this.Details.Cache;
      PXSelectExtension<Detail> details = this.Details;
      PXSelectExtension<PX.Objects.Extensions.Discount.Discount> discounts = this.Discounts;
      int? locationID = new int?(e.Row.LocationID.GetValueOrDefault());
      documentDate = e.Row.DocumentDate;
      System.DateTime? date = documentDate ?? new PXGraph().Accessinfo.BusinessDate;
      RecalcDiscountsParamFilter recalcFilter = discountsParamFilter;
      int calculationOptions = (int) this.DefaultCalculationOptions;
      discountEngineGraph.RecalculatePricesAndDiscounts(cache, (PXSelectBase<Detail>) details, (Detail) null, (PXSelectBase<PX.Objects.Extensions.Discount.Discount>) discounts, locationID, date, recalcFilter, (DiscountEngine.DiscountCalculationOptions) calculationOptions);
    }
    finally
    {
      this.SkipTaxesRecalc = false;
    }
    this.RecalcTaxes(this.Details.Cache);
    this.RecalculateTotalDiscount();
  }

  /// <summary>The RowSelected event handler for the <see cref="T:PX.Objects.Extensions.Discount.Discount" /> mapped cache extension.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.Extensions.Discount.Discount> e)
  {
    if (e.Row != null)
    {
      PXUIFieldAttribute.SetEnabled<PX.Objects.Extensions.Discount.Discount.skipDiscount>(this.Discounts.Cache, (object) e.Row, e.Row.DiscountID != null && e.Row.Type != "B");
      PXUIFieldAttribute.SetEnabled<PX.Objects.Extensions.Discount.Discount.discountID>(this.Discounts.Cache, (object) e.Row, e.Row.Type != "B");
      PXUIFieldAttribute.SetEnabled<PX.Objects.Extensions.Discount.Discount.discountSequenceID>(this.Discounts.Cache, (object) e.Row, e.Row.Type != "B");
      PXUIFieldAttribute.SetEnabled<PX.Objects.Extensions.Discount.Discount.curyDiscountAmt>(this.Discounts.Cache, (object) e.Row, e.Row.Type == "B" || e.Row.Type == "D");
      PXUIFieldAttribute.SetEnabled<PX.Objects.Extensions.Discount.Discount.discountPct>(this.Discounts.Cache, (object) e.Row, e.Row.Type == "B" || e.Row.Type == "D");
    }
    PXDefaultAttribute.SetPersistingCheck<PX.Objects.Extensions.Discount.Discount.discountID>(this.Discounts.Cache, (object) e.Row, PXPersistingCheck.Nothing);
  }

  /// <summary>The RowInserted event handler for the <see cref="T:PX.Objects.Extensions.Discount.Discount" /> mapped cache extension.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.Extensions.Discount.Discount> e)
  {
    if (e.Row == null)
      return;
    if (!this.DiscountEngineGraph.IsInternalDiscountEngineCall)
    {
      if (e.Row.DiscountID != null)
      {
        this.DiscountEngineGraph.InsertManualDocGroupDiscount(this.Details.Cache, (PXSelectBase<Detail>) this.Details, (PXSelectBase<PX.Objects.Extensions.Discount.Discount>) this.Discounts, e.Row, e.Row.DiscountID, (string) null, this.Documents.Current.BranchID, new int?(this.Documents.Current.LocationID.GetValueOrDefault()), this.Documents.Current.DocumentDate, this.DefaultCalculationOptions);
        this.RefreshTotalsAndFreeItems(this.Discounts.Cache);
        if (this.Discounts.Cache is PXModelExtension<PX.Objects.Extensions.Discount.Discount> cache)
          cache.UpdateExtensionMapping((object) e.Row);
      }
      if (this.DiscountEngineGraph.SetExternalManualDocDiscount(this.Details.Cache, (PXSelectBase<Detail>) this.Details, (PXSelectBase<PX.Objects.Extensions.Discount.Discount>) this.Discounts, e.Row, (PX.Objects.Extensions.Discount.Discount) null, this.DefaultCalculationOptions))
      {
        this.RefreshTotalsAndFreeItems(this.Discounts.Cache);
        if (this.Discounts.Cache is PXModelExtension<PX.Objects.Extensions.Discount.Discount> cache)
          cache.UpdateExtensionMapping((object) e.Row);
      }
    }
    if (e.Row.DiscountID == null || e.Row.DiscountSequenceID == null || e.Row.Description != null)
      return;
    object newValue = (object) null;
    if (this.Discounts.Cache is PXModelExtension<PX.Objects.Extensions.Discount.Discount> cache1)
      cache1.RaiseFieldDefaulting("description", (object) e.Row, out newValue);
    if (!(this.Discounts.Cache is PXModelExtension<PX.Objects.Extensions.Discount.Discount> cache2))
      return;
    cache2.SetValue<PX.Objects.Extensions.Discount.Discount.description>((object) e.Row, newValue);
  }

  /// <summary>The RowUpdated event handler for the <see cref="T:PX.Objects.Extensions.Discount.Discount" /> mapped cache extension.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.Extensions.Discount.Discount> e)
  {
    if (this.DiscountEngineGraph.IsInternalDiscountEngineCall || e.Row == null)
      return;
    int? locationId;
    if (!this.Discounts.Cache.ObjectsEqual<PX.Objects.Extensions.Discount.Discount.skipDiscount>((object) e.Row, (object) e.OldRow))
    {
      DiscountEngine<Detail, PX.Objects.Extensions.Discount.Discount> discountEngineGraph = this.DiscountEngineGraph;
      PXCache cache1 = this.Details.Cache;
      PXSelectExtension<Detail> details = this.Details;
      PXSelectExtension<PX.Objects.Extensions.Discount.Discount> discounts = this.Discounts;
      int? branchId = this.Documents.Current.BranchID;
      locationId = this.Documents.Current.LocationID;
      int? locationID = new int?(locationId.GetValueOrDefault());
      System.DateTime? documentDate = this.Documents.Current.DocumentDate;
      int num = e.Row.Type != "D" ? 1 : 0;
      int calculationOptions = (int) this.DefaultCalculationOptions;
      discountEngineGraph.UpdateDocumentDiscount(cache1, (PXSelectBase<Detail>) details, (PXSelectBase<PX.Objects.Extensions.Discount.Discount>) discounts, branchId, locationID, documentDate, num != 0, (DiscountEngine.DiscountCalculationOptions) calculationOptions);
      this.RefreshTotalsAndFreeItems(this.Discounts.Cache);
      if (this.Discounts.Cache is PXModelExtension<PX.Objects.Extensions.Discount.Discount> cache2)
        cache2.UpdateExtensionMapping((object) e.Row);
    }
    if (!this.Discounts.Cache.ObjectsEqual<PX.Objects.Extensions.Discount.Discount.discountID>((object) e.Row, (object) e.OldRow) || !this.Discounts.Cache.ObjectsEqual<PX.Objects.Extensions.Discount.Discount.discountSequenceID>((object) e.Row, (object) e.OldRow))
    {
      e.Row.IsManual = new bool?(true);
      DiscountEngine<Detail, PX.Objects.Extensions.Discount.Discount> discountEngineGraph = this.DiscountEngineGraph;
      PXCache cache3 = this.Details.Cache;
      PXSelectExtension<Detail> details = this.Details;
      PXSelectExtension<PX.Objects.Extensions.Discount.Discount> discounts = this.Discounts;
      PX.Objects.Extensions.Discount.Discount row = e.Row;
      string discountId = e.Row.DiscountID;
      string discountSequenceId = this.Discounts.Cache.ObjectsEqual<PX.Objects.Extensions.Discount.Discount.discountID>((object) e.Row, (object) e.OldRow) ? e.Row.DiscountSequenceID : (string) null;
      int? branchId = this.Documents.Current.BranchID;
      locationId = this.Documents.Current.LocationID;
      int? locationID = new int?(locationId.GetValueOrDefault());
      System.DateTime? documentDate = this.Documents.Current.DocumentDate;
      int calculationOptions = (int) this.DefaultCalculationOptions;
      discountEngineGraph.InsertManualDocGroupDiscount(cache3, (PXSelectBase<Detail>) details, (PXSelectBase<PX.Objects.Extensions.Discount.Discount>) discounts, row, discountId, discountSequenceId, branchId, locationID, documentDate, (DiscountEngine.DiscountCalculationOptions) calculationOptions);
      this.RefreshTotalsAndFreeItems(this.Discounts.Cache);
      if (this.Discounts.Cache is PXModelExtension<PX.Objects.Extensions.Discount.Discount> cache4)
        cache4.UpdateExtensionMapping((object) e.Row);
    }
    if (!this.DiscountEngineGraph.SetExternalManualDocDiscount(this.Details.Cache, (PXSelectBase<Detail>) this.Details, (PXSelectBase<PX.Objects.Extensions.Discount.Discount>) this.Discounts, e.Row, e.OldRow, this.DefaultCalculationOptions))
      return;
    this.RefreshTotalsAndFreeItems(this.Discounts.Cache);
    if (!(this.Discounts.Cache is PXModelExtension<PX.Objects.Extensions.Discount.Discount> cache))
      return;
    cache.UpdateExtensionMapping((object) e.Row);
  }

  /// <summary>The RowDeleted event handler for the <see cref="T:PX.Objects.Extensions.Discount.Discount" /> mapped cache extension.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.Extensions.Discount.Discount> e)
  {
    if (!this.DiscountEngineGraph.IsInternalDiscountEngineCall && e.Row != null)
    {
      this.DiscountEngineGraph.UpdateDocumentDiscount(this.Details.Cache, (PXSelectBase<Detail>) this.Details, (PXSelectBase<PX.Objects.Extensions.Discount.Discount>) this.Discounts, this.Documents.Current.BranchID, new int?(this.Documents.Current.LocationID.GetValueOrDefault()), this.Documents.Current.DocumentDate, e.Row.Type != null && e.Row.Type != "D" && e.Row.Type != "B", this.DefaultCalculationOptions);
      if (this.Discounts.Cache is PXModelExtension<PX.Objects.Extensions.Discount.Discount> cache)
        cache.UpdateExtensionMapping((object) e.Row);
    }
    this.RefreshTotalsAndFreeItems(this.Discounts.Cache);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.Extensions.Discount.Discount> e)
  {
    bool flag = e.Row.Type == "B";
    PXDefaultAttribute.SetPersistingCheck<SOOrderDiscountDetail.discountID>(this.Discounts.Cache, (object) e.Row, flag ? PXPersistingCheck.Nothing : PXPersistingCheck.NullOrBlank);
    PXDefaultAttribute.SetPersistingCheck<SOOrderDiscountDetail.discountSequenceID>(this.Discounts.Cache, (object) e.Row, flag ? PXPersistingCheck.Nothing : PXPersistingCheck.NullOrBlank);
  }

  private void RecalculateTotalDiscount()
  {
    if (this.Documents.Current == null)
      return;
    Document copy = (Document) this.Documents.Cache.CreateCopy((object) this.Documents.Current);
    this.Documents.Cache.SetValueExt<Document.curyDiscTot>((object) this.Documents.Current, (object) this.DiscountEngineGraph.GetTotalGroupAndDocumentDiscount<PX.Objects.Extensions.Discount.Discount>((PXSelectBase<PX.Objects.Extensions.Discount.Discount>) this.Discounts));
    this.Documents.Cache.RaiseRowUpdated((object) this.Documents.Current, (object) copy);
  }

  /// <summary>The FieldVerifying2 event handler for the <see cref="P:PX.Objects.Extensions.Discount.Discount.DiscountSequenceID" /> field.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.Extensions.Discount.Discount, PX.Objects.Extensions.Discount.Discount.discountSequenceID> e)
  {
    if (e.ExternalCall)
      return;
    e.Cancel = true;
  }

  /// <summary>The FieldVerifying2 event handler for the <see cref="P:PX.Objects.Extensions.Discount.Discount.DiscountID" /> field.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.Extensions.Discount.Discount, PX.Objects.Extensions.Discount.Discount.discountID> e)
  {
    if (e.ExternalCall)
      return;
    e.Cancel = true;
  }

  private void ResetQtyOnFreeItem(PXGraph graph, Detail line)
  {
    PXView pxView = new PXView(graph, false, this.Discounts.View.BqlSelect.WhereAnd<Where<PX.Objects.Extensions.Discount.Discount.freeItemID, Equal<Required<PX.Objects.Extensions.Discount.Discount.freeItemID>>>>());
    Decimal? nullable1 = new Decimal?(0M);
    object[] objArray = new object[1]
    {
      (object) line.InventoryID
    };
    foreach (IDiscountDetail discountDetail in pxView.SelectMulti(objArray))
    {
      if (!discountDetail.SkipDiscount.GetValueOrDefault() && discountDetail.FreeItemID.HasValue)
      {
        Decimal? nullable2 = discountDetail.FreeItemQty;
        if (nullable2.HasValue)
        {
          nullable2 = discountDetail.FreeItemQty;
          if (nullable2.Value > 0M)
          {
            nullable2 = nullable1;
            Decimal num = discountDetail.FreeItemQty.Value;
            nullable1 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num) : new Decimal?();
          }
        }
      }
    }
    this.Details.Cache.SetValueExt<Detail.quantity>((object) line, (object) nullable1);
  }

  private void RefreshFreeItemLines(PXCache sender)
  {
    if (sender.Graph.IsImport && !sender.Graph.IsMobile)
      return;
    Dictionary<int, Decimal> dictionary1 = new Dictionary<int, Decimal>();
    foreach (PXResult<PX.Objects.Extensions.Discount.Discount> pxResult in this.Discounts.Select())
    {
      PX.Objects.Extensions.Discount.Discount discount = (PX.Objects.Extensions.Discount.Discount) pxResult;
      int? freeItemId = discount.FreeItemID;
      if (freeItemId.HasValue && !discount.SkipDiscount.GetValueOrDefault())
      {
        Dictionary<int, Decimal> dictionary2 = dictionary1;
        freeItemId = discount.FreeItemID;
        int key1 = freeItemId.Value;
        Decimal? freeItemQty;
        if (dictionary2.ContainsKey(key1))
        {
          Dictionary<int, Decimal> dictionary3 = dictionary1;
          freeItemId = discount.FreeItemID;
          int key2 = freeItemId.Value;
          Dictionary<int, Decimal> dictionary4 = dictionary3;
          int key3 = key2;
          Decimal num1 = dictionary3[key2];
          freeItemQty = discount.FreeItemQty;
          Decimal valueOrDefault = freeItemQty.GetValueOrDefault();
          Decimal num2 = num1 + valueOrDefault;
          dictionary4[key3] = num2;
        }
        else
        {
          Dictionary<int, Decimal> dictionary5 = dictionary1;
          freeItemId = discount.FreeItemID;
          int key4 = freeItemId.Value;
          freeItemQty = discount.FreeItemQty;
          Decimal valueOrDefault = freeItemQty.GetValueOrDefault();
          dictionary5.Add(key4, valueOrDefault);
        }
      }
    }
    bool flag1 = false;
    foreach (Detail detail in this.Details.Cache.Cached)
    {
      switch (this.Details.Cache.GetStatus((object) detail))
      {
        case PXEntryStatus.Deleted:
        case PXEntryStatus.InsertedDeleted:
          continue;
        default:
          bool? nullable = detail.IsFree;
          if (nullable.GetValueOrDefault())
          {
            nullable = detail.ManualDisc;
            bool flag2 = false;
            if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
            {
              int? inventoryId = detail.InventoryID;
              if (inventoryId.HasValue)
              {
                Dictionary<int, Decimal> dictionary6 = dictionary1;
                inventoryId = detail.InventoryID;
                int key5 = inventoryId.Value;
                if (dictionary6.ContainsKey(key5))
                {
                  Dictionary<int, Decimal> dictionary7 = dictionary1;
                  inventoryId = detail.InventoryID;
                  int key6 = inventoryId.Value;
                  if (dictionary7[key6] == 0M)
                  {
                    this.Details.Cache.Delete((object) detail);
                    flag1 = true;
                    continue;
                  }
                  continue;
                }
                this.Details.Cache.Delete((object) detail);
                flag1 = true;
                continue;
              }
              continue;
            }
            continue;
          }
          continue;
      }
    }
    foreach (KeyValuePair<int, Decimal> keyValuePair in dictionary1)
    {
      Detail freeLineByItemId = this.GetFreeLineByItemID(new int?(keyValuePair.Key), out object _);
      if (freeLineByItemId == null)
      {
        if (keyValuePair.Value > 0M)
        {
          this.Details.Cache.Insert((object) new Detail()
          {
            InventoryID = new int?(keyValuePair.Key),
            IsFree = new bool?(true),
            Quantity = new Decimal?(keyValuePair.Value)
          });
          flag1 = true;
        }
      }
      else
      {
        Decimal? quantity = freeLineByItemId.Quantity;
        Decimal num = keyValuePair.Value;
        if (!(quantity.GetValueOrDefault() == num & quantity.HasValue))
        {
          Detail copy = (Detail) this.Details.Cache.CreateCopy((object) freeLineByItemId);
          copy.Quantity = new Decimal?(keyValuePair.Value);
          this.Details.Cache.Update((object) copy);
          flag1 = true;
        }
      }
    }
    if (!flag1)
      return;
    this.Details.View.RequestRefresh();
  }

  private Detail GetFreeLineByItemID(int? inventoryID, out object source)
  {
    source = (object) null;
    foreach (Detail freeLineByItemId in this.Details.Cache.Cached)
    {
      switch (this.Details.Cache.GetStatus((object) freeLineByItemId))
      {
        case PXEntryStatus.Deleted:
        case PXEntryStatus.InsertedDeleted:
          continue;
        default:
          bool? nullable1 = freeLineByItemId.IsFree;
          if (nullable1.GetValueOrDefault())
          {
            int? inventoryId = freeLineByItemId.InventoryID;
            int? nullable2 = inventoryID;
            if (inventoryId.GetValueOrDefault() == nullable2.GetValueOrDefault() & inventoryId.HasValue == nullable2.HasValue)
            {
              nullable1 = freeLineByItemId.ManualDisc;
              if (!nullable1.GetValueOrDefault())
              {
                source = (object) freeLineByItemId;
                return freeLineByItemId;
              }
              continue;
            }
            continue;
          }
          continue;
      }
    }
    return (Detail) null;
  }

  /// <summary>Recalculates discounts for the specified detail line.</summary>
  /// <param name="sender">A cache object.</param>
  /// <param name="line">The line for which the discounts should be recalculated.</param>
  protected virtual void RecalculateDiscounts(PXCache sender, Detail line, object source)
  {
    if (!line.Quantity.HasValue || !line.CuryLineAmount.HasValue || line.IsFree.GetValueOrDefault())
      return;
    Document current = this.Documents.Current;
    DiscountEngine<Detail, PX.Objects.Extensions.Discount.Discount> discountEngineGraph = this.DiscountEngineGraph;
    PXCache cache = this.Details.Cache;
    PXSelectExtension<Detail> details = this.Details;
    Detail line1 = line;
    PXSelectExtension<PX.Objects.Extensions.Discount.Discount> discounts = this.Discounts;
    int? nullable = current.BranchID;
    int? branchID = new int?(nullable.GetValueOrDefault());
    nullable = current.LocationID;
    int? locationID = new int?(nullable.GetValueOrDefault());
    string curyId = current.CuryID;
    System.DateTime? date = current.DocumentDate ?? this.Base.Accessinfo.BusinessDate;
    int calculationOptions = (int) this.DefaultCalculationOptions;
    discountEngineGraph.SetDiscounts(cache, (PXSelectBase<Detail>) details, line1, (PXSelectBase<PX.Objects.Extensions.Discount.Discount>) discounts, branchID, locationID, curyId, date, discountCalculationOptions: (DiscountEngine.DiscountCalculationOptions) calculationOptions);
    this.RecalculateTotalDiscount();
    this.RefreshFreeItemLines(sender);
    this.RecalculateDetailsDiscount(this.Details.Cache);
  }

  public virtual void RefreshTotalsAndFreeItems(PXCache sender)
  {
    this.RecalculateTotalDiscount();
    this.RefreshFreeItemLines(sender);
    this.RecalculateDetailsDiscount(this.Details.Cache);
  }

  private static bool IsInstanceOfGenericType(System.Type genericType, object instance)
  {
    for (System.Type type = instance.GetType(); type != (System.Type) null; type = type.BaseType)
    {
      if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
        return true;
    }
    return false;
  }

  private void RecalculateDetailsDiscount(PXCache sender)
  {
    foreach (PXResult<Detail> pxResult in this.Details.Select())
    {
      Detail row = (Detail) pxResult;
      this.Details.Cache.RaiseFieldUpdated<Detail.documentDiscountRate>((object) row, (object) row.DocumentDiscountRate);
    }
    this.RecalcTaxes(sender);
  }

  private void RecalcTaxes(PXCache sender)
  {
    if (this.SkipTaxesRecalc)
      return;
    sender.Graph.FindImplementation<ITaxRecalculator>()?.RecalcTaxes();
  }

  /// <summary>Defines the default mapping of the <see cref="T:PX.Objects.Extensions.Discount.Document" /> mapped cache extension to a DAC.</summary>
  protected class DocumentMapping : IBqlMapping
  {
    /// <exclude />
    protected System.Type _table;
    /// <exclude />
    public System.Type BranchID = typeof (Document.branchID);
    /// <exclude />
    public System.Type CuryID = typeof (Document.curyID);
    /// <exclude />
    public System.Type CuryInfoID = typeof (Document.curyInfoID);
    /// <exclude />
    public System.Type CuryOrigDiscAmt = typeof (Document.curyOrigDiscAmt);
    /// <exclude />
    public System.Type OrigDiscAmt = typeof (Document.origDiscAmt);
    /// <exclude />
    public System.Type CuryDiscTaken = typeof (Document.curyDiscTaken);
    /// <exclude />
    public System.Type DiscTaken = typeof (Document.discTaken);
    /// <exclude />
    public System.Type CuryDiscBal = typeof (Document.curyDiscBal);
    /// <exclude />
    public System.Type DiscBal = typeof (Document.discBal);
    /// <exclude />
    public System.Type DiscTot = typeof (Document.discTot);
    /// <exclude />
    public System.Type CuryDiscTot = typeof (Document.curyDiscTot);
    /// <exclude />
    public System.Type CuryDiscountedDocTotal = typeof (Document.curyDiscountedDocTotal);
    /// <exclude />
    public System.Type DiscountedDocTotal = typeof (Document.discountedDocTotal);
    /// <exclude />
    public System.Type CurydiscountedPrice = typeof (Document.curyDiscountedPrice);
    /// <exclude />
    public System.Type DiscountedPrice = typeof (Document.discountedPrice);
    /// <exclude />
    public System.Type LocationID = typeof (Document.locationID);
    /// <exclude />
    public System.Type DocumentDate = typeof (Document.documentDate);
    /// <exclude />
    public System.Type CuryLinetotal = typeof (Document.curyLineTotal);
    /// <exclude />
    public System.Type LineTotal = typeof (Document.lineTotal);
    /// <exclude />
    public System.Type CuryMiscTot = typeof (Document.curyMiscTot);
    /// <exclude />
    public System.Type CustomerID = typeof (Document.customerID);

    /// <exclude />
    public System.Type Extension => typeof (Document);

    /// <exclude />
    public System.Type Table => this._table;

    /// <summary>Creates the default mapping of the <see cref="T:PX.Objects.Extensions.Discount.Document" /> mapped cache extension to the specified table.</summary>
    /// <param name="table">A DAC.</param>
    public DocumentMapping(System.Type table) => this._table = table;
  }

  /// <summary>Defines the default mapping of the <see cref="T:PX.Objects.Extensions.Discount.Detail" /> mapped cache extension to a DAC.</summary>
  protected class DetailMapping : IBqlMapping
  {
    /// <exclude />
    protected System.Type _table;
    /// <exclude />
    public System.Type BranchID = typeof (Detail.branchID);
    /// <exclude />
    public System.Type InventoryID = typeof (Detail.inventoryID);
    /// <exclude />
    public System.Type SiteID = typeof (Detail.siteID);
    /// <exclude />
    public System.Type CustomerID = typeof (Detail.customerID);
    /// <exclude />
    public System.Type VendorID = typeof (Detail.vendorID);
    /// <exclude />
    public System.Type CuryInfoID = typeof (Detail.curyInfoID);
    /// <exclude />
    public System.Type Quantity = typeof (Detail.quantity);
    /// <exclude />
    public System.Type CuryUnitPrice = typeof (Detail.curyUnitPrice);
    /// <exclude />
    public System.Type CuryExtPrice = typeof (Detail.curyExtPrice);
    /// <exclude />
    public System.Type CuryLineAmount = typeof (Detail.curyLineAmount);
    /// <exclude />
    public System.Type UOM = typeof (Detail.uOM);
    /// <exclude />
    public System.Type GroupDiscountRate = typeof (Detail.groupDiscountRate);
    /// <exclude />
    public System.Type DocumentDiscountRate = typeof (Detail.documentDiscountRate);
    /// <exclude />
    public System.Type CuryDiscAmt = typeof (Detail.curyDiscAmt);
    /// <exclude />
    public System.Type DiscPct = typeof (Detail.discPct);
    /// <exclude />
    public System.Type DiscountID = typeof (Detail.discountID);
    /// <exclude />
    public System.Type DiscountSequenceID = typeof (Detail.discountSequenceID);
    /// <exclude />
    public System.Type IsFree = typeof (Detail.isFree);
    /// <exclude />
    public System.Type ManualDisc = typeof (Detail.manualDisc);
    /// <exclude />
    public System.Type ManualPrice = typeof (Detail.manualPrice);
    /// <exclude />
    public System.Type LineType = typeof (Detail.lineType);
    /// <exclude />
    public System.Type TaxCategoryID = typeof (Detail.taxCategoryID);
    /// <exclude />
    public System.Type FreezeManualDisc = typeof (Detail.freezeManualDisc);

    /// <exclude />
    public System.Type Extension => typeof (Detail);

    /// <exclude />
    public System.Type Table => this._table;

    /// <summary>Creates the default mapping of the <see cref="T:PX.Objects.Extensions.Discount.Detail" /> mapped cache extension to the specified table.</summary>
    /// <param name="table">A DAC.</param>
    public DetailMapping(System.Type table) => this._table = table;
  }

  /// <summary>Defines the default mapping of the <see cref="T:PX.Objects.Extensions.Discount.Discount" /> mapped cache extension to a DAC.</summary>
  protected class DiscountMapping : IBqlMapping
  {
    /// <exclude />
    protected System.Type _table;
    /// <exclude />
    public System.Type RecordID = typeof (PX.Objects.Extensions.Discount.Discount.recordID);
    /// <exclude />
    public System.Type LineNbr = typeof (PX.Objects.Extensions.Discount.Discount.lineNbr);
    /// <exclude />
    public System.Type SkipDiscount = typeof (PX.Objects.Extensions.Discount.Discount.skipDiscount);
    /// <exclude />
    public System.Type DiscountID = typeof (PX.Objects.Extensions.Discount.Discount.discountID);
    /// <exclude />
    public System.Type DiscountSequenceID = typeof (PX.Objects.Extensions.Discount.Discount.discountSequenceID);
    /// <exclude />
    public System.Type Type = typeof (PX.Objects.Extensions.Discount.Discount.type);
    /// <exclude />
    public System.Type DiscountableAmt = typeof (PX.Objects.Extensions.Discount.Discount.discountableAmt);
    /// <exclude />
    public System.Type CuryDiscountableAmt = typeof (PX.Objects.Extensions.Discount.Discount.curyDiscountableAmt);
    /// <exclude />
    public System.Type DiscountableQty = typeof (PX.Objects.Extensions.Discount.Discount.discountableQty);
    /// <exclude />
    public System.Type DiscountAmt = typeof (PX.Objects.Extensions.Discount.Discount.discountAmt);
    /// <exclude />
    public System.Type CuryDiscountAmt = typeof (PX.Objects.Extensions.Discount.Discount.curyDiscountAmt);
    /// <exclude />
    public System.Type DiscountPct = typeof (PX.Objects.Extensions.Discount.Discount.discountPct);
    /// <exclude />
    public System.Type FreeItemID = typeof (PX.Objects.Extensions.Discount.Discount.freeItemID);
    /// <exclude />
    public System.Type FreeItemQty = typeof (PX.Objects.Extensions.Discount.Discount.freeItemQty);
    /// <exclude />
    public System.Type IsManual = typeof (PX.Objects.Extensions.Discount.Discount.isManual);

    /// <exclude />
    public System.Type Extension => typeof (PX.Objects.Extensions.Discount.Discount);

    /// <exclude />
    public System.Type Table => this._table;

    /// <summary>Creates the default mapping of the <see cref="T:PX.Objects.Extensions.Discount.Discount" /> mapped cache extension to the specified table.</summary>
    /// <param name="table">A DAC.</param>
    public DiscountMapping(System.Type table) => this._table = table;
  }
}
