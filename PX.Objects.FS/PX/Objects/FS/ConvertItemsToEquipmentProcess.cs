// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ConvertItemsToEquipmentProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using PX.Objects.SO;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.FS;

public class ConvertItemsToEquipmentProcess : PXGraph<
#nullable disable
ConvertItemsToEquipmentProcess>
{
  [PXHidden]
  public PXFilter<ConvertItemsToEquipmentProcess.StockItemsFilter> Filter;
  public PXCancel<ConvertItemsToEquipmentProcess.StockItemsFilter> Cancel;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingJoin<SoldInventoryItem, ConvertItemsToEquipmentProcess.StockItemsFilter, InnerJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<SoldInventoryItem.customerID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>, Where2<Where<CurrentValue<ConvertItemsToEquipmentProcess.StockItemsFilter.itemClassID>, IsNull, Or<SoldInventoryItem.itemClassID, Equal<CurrentValue<ConvertItemsToEquipmentProcess.StockItemsFilter.itemClassID>>>>, And<Where<CurrentValue<ConvertItemsToEquipmentProcess.StockItemsFilter.date>, IsNull, Or<SoldInventoryItem.docDate, GreaterEqual<CurrentValue<ConvertItemsToEquipmentProcess.StockItemsFilter.date>>>>>>, OrderBy<Asc<SoldInventoryItem.inventoryCD, Asc<SoldInventoryItem.invoiceRefNbr, Asc<SoldInventoryItem.invoiceLineNbr>>>>> InventoryItems;
  public PXAction<ConvertItemsToEquipmentProcess.StockItemsFilter> openInvoice;

  public ConvertItemsToEquipmentProcess()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ConvertItemsToEquipmentProcess.\u003C\u003Ec__DisplayClass0_0 cDisplayClass00 = new ConvertItemsToEquipmentProcess.\u003C\u003Ec__DisplayClass0_0();
    // ISSUE: explicit constructor call
    base.\u002Ector();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass00.\u003C\u003E4__this = this;
    // ISSUE: method pointer
    ((PXProcessingBase<SoldInventoryItem>) this.InventoryItems).SetProcessDelegate(new PXProcessingBase<SoldInventoryItem>.ProcessListDelegate((object) cDisplayClass00, __methodptr(\u003C\u002Ector\u003Eb__0)));
  }

  [PXUIField]
  public virtual void OpenInvoice()
  {
    SOInvoiceEntry instance = PXGraph.CreateInstance<SOInvoiceEntry>();
    if (((PXSelectBase<SoldInventoryItem>) this.InventoryItems).Current != null && ((PXSelectBase<SoldInventoryItem>) this.InventoryItems).Current.InvoiceRefNbr != null)
    {
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) ((PXSelectBase<SoldInventoryItem>) this.InventoryItems).Current.InvoiceRefNbr, new object[1]
      {
        (object) ((PXSelectBase<SoldInventoryItem>) this.InventoryItems).Current.DocType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<SoldInventoryItem> e)
  {
    if (e.Row == null)
      return;
    SoldInventoryItem row = e.Row;
    if (PXSelectBase<FSModelComponent, PXSelect<FSModelComponent, Where<FSModelComponent.modelID, Equal<Required<FSModelComponent.modelID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.InventoryID
    }).Count != 0)
      return;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SoldInventoryItem>>) e).Cache.RaiseExceptionHandling<SoldInventoryItem.inventoryCD>((object) row, (object) row.InventoryCD, (Exception) new PXSetPropertyException("Components and warranties are not defined for this item.", (PXErrorLevel) 3));
  }

  public virtual List<SM_ARReleaseProcess.ItemInfo> GetDifferentItemList(
    PXGraph graph,
    PX.Objects.AR.ARTran arTran,
    bool createDifferentEntriesForQtyGreaterThan1)
  {
    return SharedFunctions.GetDifferentItemList(graph, arTran, createDifferentEntriesForQtyGreaterThan1);
  }

  [Serializable]
  public class StockItemsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXInt]
    [PXUIField(DisplayName = "Item Class ID")]
    [PXSelector(typeof (Search<INItemClass.itemClassID, Where<FSxEquipmentModelTemplate.eQEnabled, Equal<True>>>), SubstituteKey = typeof (INItemClass.itemClassCD))]
    public virtual int? ItemClassID { get; set; }

    [PXDBDate]
    [PXUIField(DisplayName = "Sold After")]
    public virtual DateTime? Date { get; set; }

    public abstract class itemClassID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ConvertItemsToEquipmentProcess.StockItemsFilter.itemClassID>
    {
    }

    public abstract class date : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ConvertItemsToEquipmentProcess.StockItemsFilter.date>
    {
    }
  }
}
