// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.GraphExtensions.SmartPanelExt`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.Common.Exceptions;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using PX.Objects.IN.Matrix.Attributes;
using PX.Objects.IN.Matrix.DAC;
using PX.Objects.IN.Matrix.DAC.Unbound;
using PX.Objects.IN.Matrix.Interfaces;
using PX.Objects.IN.Matrix.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.Matrix.GraphExtensions;

public abstract class SmartPanelExt<Graph, MainItemType> : MatrixGridExt<Graph, MainItemType>
  where Graph : PXGraph, new()
  where MainItemType : class, IBqlTable, new()
{
  [PXCopyPasteHiddenView]
  public PXSelectOrderBy<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem, OrderBy<Asc<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem.createdDateTime>>> MatrixItems;
  public PXAction<MainItemType> showMatrixPanel;

  public override bool AddTotals => true;

  public override bool ShowDisabledValue => false;

  public virtual IEnumerable matrixItems() => ((PXSelectBase) this.MatrixItems).Cache.Cached;

  public override void Initialize()
  {
    base.Initialize();
    this.Base.Views.Caches.Remove(typeof (PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem));
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ShowMatrixPanel(PXAdapter adapter)
  {
    WebDialogResult answer1 = ((PXSelectBase) this.Header).View.GetAnswer((string) null);
    ((PXSelectBase) this.Header).View.SetAnswer((string) null, (WebDialogResult) 0);
    WebDialogResult answer2 = ((PXSelectBase) this.Matrix).View.GetAnswer((string) null);
    ((PXSelectBase) this.Matrix).View.SetAnswer((string) null, (WebDialogResult) 0);
    if (answer1 == null && answer2 == null)
      this.ShowPanel();
    else if (answer1 == 6 || answer2 == 6)
      this.ShowPanel(true);
    else if (answer1 == 1)
      this.AddItemsToOrder(((PXSelectBase<EntryHeader>) this.Header).Current.SiteID);
    else if (answer2 == 1)
      this.AddMatrixItemsToOrder(((PXSelectBase<EntryHeader>) this.Header).Current.SiteID);
    return adapter.Get();
  }

  protected virtual void ShowPanel(bool changeType = false)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SmartPanelExt<Graph, MainItemType>.\u003C\u003Ec__DisplayClass10_0 cDisplayClass100 = new SmartPanelExt<Graph, MainItemType>.\u003C\u003Ec__DisplayClass10_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass100.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass100.panelType = ((PXSelectBase<EntryHeader>) this.Header).Current.SmartPanelType;
    if (changeType)
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass100.panelType = ((PXSelectBase<EntryHeader>) this.Header).Current.SmartPanelType == "E" ? "L" : "E";
    }
    // ISSUE: method pointer
    PXView.InitializePanel initializePanel = new PXView.InitializePanel((object) cDisplayClass100, __methodptr(\u003CShowPanel\u003Eb__0));
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass100.panelType == "E")
      ((PXSelectBase<EntryHeader>) this.Header).AskExt(initializePanel);
    else
      ((PXSelectBase<EntryMatrix>) this.Matrix).AskExt(initializePanel);
  }

  protected abstract bool IsDocumentOpen();

  protected abstract void UpdateLine(IMatrixItemLine line);

  protected abstract void CreateNewLine(int? siteID, int? inventoryID, Decimal qty);

  protected abstract void CreateNewLine(
    int? siteID,
    int? inventoryID,
    string taxCategoryID,
    Decimal qty,
    string uom);

  protected abstract IEnumerable<IMatrixItemLine> GetLines(int? siteID, int? inventoryID);

  protected abstract IEnumerable<IMatrixItemLine> GetLines(
    int? siteID,
    int? inventoryID,
    string taxCategoryID,
    string uom);

  protected abstract int? GetDefaultBranch();

  protected virtual bool IsItemStatusDisabled(PX.Objects.IN.InventoryItem item)
  {
    return item != null && EnumerableExtensions.IsIn<string>(item.ItemStatus, "IN", "DE");
  }

  protected virtual string GetDefaultUOM(int? inventoryID)
  {
    return PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, inventoryID)?.SalesUnit;
  }

  protected override CSAttribute[] GetAdditionalAttributes()
  {
    if (((PXSelectBase<EntryHeader>) this.Header).Current.SmartPanelType == "L")
      return base.GetAdditionalAttributes();
    PX.Objects.IN.InventoryItem templateItem = this.GetTemplateItem();
    return ((PXSelectBase<CSAttribute>) new PXSelectReadonly2<CSAttribute, InnerJoin<CSAttributeGroup, On<CSAttributeGroup.attributeID, Equal<CSAttribute.attributeID>>>, Where<CSAttributeGroup.isActive, Equal<True>, And<CSAttributeGroup.entityClassID, Equal<Required<PX.Objects.IN.InventoryItem.itemClassID>>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<PX.Objects.IN.InventoryItem>>, And<CSAttributeGroup.attributeCategory, Equal<CSAttributeGroup.attributeCategory.variant>, And<NotExists<Select<CSAnswers, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CSAnswers.isActive, Equal<False>>>>, And<BqlOperand<CSAnswers.attributeID, IBqlString>.IsEqual<CSAttribute.attributeID>>>>.And<BqlOperand<CSAnswers.refNoteID, IBqlGuid>.IsEqual<P.AsGuid>>>>>>>>>>, OrderBy<Asc<CSAttributeGroup.sortOrder>>>((PXGraph) this.Base)).SelectMain(new object[2]
    {
      (object) (int?) templateItem?.ItemClassID,
      (object) (Guid?) templateItem?.NoteID
    });
  }

  protected override void AddFieldToAttributeGrid(PXCache cache, int attributeNumber)
  {
    if (((PXSelectBase<EntryHeader>) this.Header).Current.SmartPanelType == "L")
      base.AddFieldToAttributeGrid(cache, attributeNumber);
    else
      base.AddFieldToAttributeGrid(((PXSelectBase) this.MatrixItems).Cache, attributeNumber);
  }

  protected override string GetAttributeValue(object row, int attributeNumber)
  {
    if (((PXSelectBase<EntryHeader>) this.Header).Current.SmartPanelType == "L")
      return base.GetAttributeValue(row, attributeNumber);
    PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem matrixInventoryItem = row as PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem;
    string str1;
    if (0 <= attributeNumber)
    {
      int num = attributeNumber;
      int? length = matrixInventoryItem?.AttributeValueDescrs?.Length;
      int valueOrDefault = length.GetValueOrDefault();
      if (num < valueOrDefault & length.HasValue)
      {
        str1 = matrixInventoryItem.AttributeValueDescrs[attributeNumber];
        goto label_6;
      }
    }
    str1 = (string) null;
label_6:
    string attributeValue = str1;
    if (string.IsNullOrEmpty(attributeValue))
    {
      string str2;
      if (0 <= attributeNumber)
      {
        int num = attributeNumber;
        int? length = matrixInventoryItem?.AttributeValues?.Length;
        int valueOrDefault = length.GetValueOrDefault();
        if (num < valueOrDefault & length.HasValue)
        {
          str2 = matrixInventoryItem.AttributeValues[attributeNumber];
          goto label_11;
        }
      }
      str2 = (string) null;
label_11:
      attributeValue = str2;
    }
    return attributeValue;
  }

  protected override void AttributeValueFieldUpdating(
    int attributeNumber,
    PXFieldUpdatingEventArgs e)
  {
    if (((PXSelectBase<EntryHeader>) this.Header).Current.SmartPanelType == "L")
    {
      base.AttributeValueFieldUpdating(attributeNumber, e);
    }
    else
    {
      if (!(e.Row is PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem row))
        return;
      string newValue = e.NewValue as string;
      int num = attributeNumber;
      int? length = row.AttributeValueDescrs?.Length;
      int valueOrDefault = length.GetValueOrDefault();
      if (!(num < valueOrDefault & length.HasValue) || !(row.AttributeValueDescrs[attributeNumber] != newValue))
        return;
      PXSelect<CSAttributeDetail, Where<CSAttributeDetail.attributeID, Equal<Required<CSAttributeDetail.attributeID>>, And<CSAttributeDetail.valueID, Equal<Required<CSAttributeDetail.valueID>>>>> pxSelect = new PXSelect<CSAttributeDetail, Where<CSAttributeDetail.attributeID, Equal<Required<CSAttributeDetail.attributeID>>, And<CSAttributeDetail.valueID, Equal<Required<CSAttributeDetail.valueID>>>>>((PXGraph) this.Base);
      if (!this.ShowDisabledValue)
        ((PXSelectBase<CSAttributeDetail>) pxSelect).WhereAnd<Where<CSAttributeDetail.disabled, NotEqual<True>>>();
      CSAttributeDetail csAttributeDetail = PXResultset<CSAttributeDetail>.op_Implicit(((PXSelectBase<CSAttributeDetail>) pxSelect).Select(new object[2]
      {
        (object) ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.AttributeIdentifiers[attributeNumber],
        (object) newValue
      }));
      row.AttributeValues[attributeNumber] = csAttributeDetail != null ? csAttributeDetail.ValueID : throw new RowNotFoundException((PXCache) GraphHelper.Caches<CSAttributeDetail>((PXGraph) this.Base), new object[2]
      {
        (object) ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.AttributeIdentifiers[attributeNumber],
        (object) newValue
      });
      row.AttributeValueDescrs[attributeNumber] = csAttributeDetail.Description;
      if (!this.AllAttributesArePopulated(row))
        return;
      int? inventoryID = !this.FindMatrixInventoryItem(row) ? this.FindInventoryItem(row) : throw new PXException("An attempt was made to add a duplicate entry.");
      if (inventoryID.HasValue)
        this.OnExisingItemSelected(row, inventoryID);
      else
        this.OnNewItemSelected(((PXSelectBase<EntryHeader>) this.Header).Current.TemplateItemID, row);
    }
  }

  protected virtual bool FindMatrixInventoryItem(PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem row)
  {
    return ((PXSelectBase) this.MatrixItems).Cache.Inserted.Cast<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem>().Any<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem>((Func<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem, bool>) (item =>
    {
      if (item == row)
        return false;
      string[] attributeValues = item.AttributeValues;
      return attributeValues != null && ((IEnumerable<string>) attributeValues).SequenceEqual<string>((IEnumerable<string>) row.AttributeValues);
    }));
  }

  protected virtual int? FindInventoryItem(PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem row)
  {
    int? nullable1 = new int?();
    string[] source = new string[row.AttributeIDs.Length];
    foreach (PXResult<CSAnswers, PX.Objects.IN.InventoryItem> inventoryWithAttribute in this.SelectInventoryWithAttributes())
    {
      PX.Objects.IN.InventoryItem inventoryItem1 = PXResult<CSAnswers, PX.Objects.IN.InventoryItem>.op_Implicit(inventoryWithAttribute);
      CSAnswers csAnswers = PXResult<CSAnswers, PX.Objects.IN.InventoryItem>.op_Implicit(inventoryWithAttribute);
      int? nullable2 = nullable1;
      int? inventoryItem2 = inventoryItem1.InventoryID;
      if (!(nullable2.GetValueOrDefault() == inventoryItem2.GetValueOrDefault() & nullable2.HasValue == inventoryItem2.HasValue))
      {
        nullable1 = inventoryItem1.InventoryID;
        for (int index = 0; index < source.Length; ++index)
          source[index] = (string) null;
      }
      for (int index = 0; index < source.Length; ++index)
      {
        if (string.Equals(row.AttributeIDs[index], csAnswers.AttributeID, StringComparison.OrdinalIgnoreCase) && row.AttributeValues[index] == csAnswers.Value)
        {
          source[index] = csAnswers.Value;
          break;
        }
      }
      if (nullable1.HasValue && ((IEnumerable<string>) source).All<string>((Func<string, bool>) (v => v != null)))
      {
        inventoryItem2 = nullable1;
        return inventoryItem2;
      }
    }
    return new int?();
  }

  protected virtual void OnExisingItemSelected(PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem row, int? inventoryID)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, inventoryID);
    row.InventoryCD = inventoryItem != null ? inventoryItem.InventoryCD : throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.IN.InventoryItem>((PXGraph) this.Base), new object[1]
    {
      (object) inventoryID
    });
    row.InventoryID = inventoryItem.InventoryID;
    row.Descr = inventoryItem.Descr;
    row.New = new bool?(false);
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, this.GetDefaultBranch());
    InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this.Base, inventoryID, branch?.BaseCuryID);
    row.BasePrice = (Decimal?) itemCurySettings?.BasePrice;
    row.TaxCategoryID = inventoryItem.TaxCategoryID;
    row.Exists = new bool?(false);
    (string UOM, Decimal? Qty, int Count) qty = this.GetQty(new int?(), inventoryID);
    row.UOM = qty.UOM;
    row.UOMDisabled = new bool?(qty.Count > 0);
    row.Qty = this.IsItemStatusDisabled(inventoryItem) ? new Decimal?() : new Decimal?(0M);
    ((PXSelectBase) this.MatrixItems).Cache.Normalize();
    ((PXSelectBase) this.MatrixItems).View.RequestRefresh();
  }

  protected virtual void OnNewItemSelected(int? templateItemID, PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem row)
  {
    PX.Objects.IN.InventoryItem templateItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, templateItemID);
    if (templateItem == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.IN.InventoryItem>((PXGraph) this.Base), new object[1]
      {
        (object) templateItemID
      });
    CreateMatrixItemsHelper createHelper = this.GetCreateMatrixItemsHelper((PXGraph) this.Base);
    List<INMatrixGenerationRule> idGenerationRules;
    List<INMatrixGenerationRule> descrGenerationRules;
    createHelper.GetGenerationRules(templateItemID, out idGenerationRules, out descrGenerationRules);
    object matrixItemId1 = (object) createHelper.GenerateMatrixItemID(templateItem, idGenerationRules, row);
    ((PXSelectBase) this.MatrixItems).Cache.RaiseFieldUpdating<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem.inventoryCD>((object) row, ref matrixItemId1);
    row.InventoryCD = (string) matrixItemId1;
    row.InventoryCD = ((PXSelectBase) this.MatrixItems).Cache.GetFormatedMaskField<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem.inventoryCD>((IBqlTable) row);
    row.InventoryID = new int?();
    row.TemplateItemID = templateItem.InventoryID;
    if (PXDBLocalizableStringAttribute.IsEnabled)
    {
      GraphHelper.Caches<PX.Objects.IN.InventoryItem>((PXGraph) this.Base);
      DBMatrixLocalizableDescriptionAttribute.SetTranslations<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem.descr>(((PXSelectBase) this.MatrixItems).Cache, (object) row, (Func<string, string>) (locale =>
      {
        object matrixItemId2 = (object) createHelper.GenerateMatrixItemID(templateItem, descrGenerationRules, row, locale: locale);
        ((PXSelectBase) this.MatrixItems).Cache.RaiseFieldUpdating<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem.descr>((object) row, ref matrixItemId2);
        return (string) matrixItemId2;
      }));
    }
    else
      row.Descr = createHelper.GenerateMatrixItemID(templateItem, descrGenerationRules, row);
    row.New = new bool?(true);
    InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this.Base, (int?) ((PXSelectBase<EntryHeader>) this.Header).Current?.TemplateItemID, PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, this.GetDefaultBranch())?.BaseCuryID);
    row.BasePrice = (Decimal?) itemCurySettings?.BasePrice;
    row.TaxCategoryID = templateItem.TaxCategoryID;
    row.Exists = new bool?(PX.Objects.IN.InventoryItem.UK.Find((PXGraph) this.Base, row.InventoryCD) != null);
    row.UOM = this.GetDefaultUOM(templateItemID);
    row.UOMDisabled = new bool?(false);
    row.Qty = new Decimal?(0M);
    ((PXSelectBase) this.MatrixItems).Cache.Normalize();
    ((PXSelectBase) this.MatrixItems).View.RequestRefresh();
  }

  protected virtual CreateMatrixItemsHelper GetCreateMatrixItemsHelper(PXGraph graph)
  {
    return new CreateMatrixItemsHelper(graph);
  }

  protected virtual AttributeGroupHelper GetAttributeGroupHelper(PXGraph graph)
  {
    return new AttributeGroupHelper(graph);
  }

  protected override void RecalcMatrixGrid()
  {
    if (((PXSelectBase<EntryHeader>) this.Header).Current.SmartPanelType == "L")
      base.RecalcMatrixGrid();
    else
      ((PXSelectBase) this.MatrixItems).Cache.Clear();
  }

  [InventoryRaw(DisplayName = "Inventory ID", IsKey = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem.inventoryCD> eventArgs)
  {
  }

  [PXInt]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem.inventoryID> eventArgs)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    eventArgs.Row.AttributeIDs = (string[]) ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.AttributeIdentifiers.Clone();
    eventArgs.Row.AttributeValueDescrs = new string[eventArgs.Row.AttributeIDs.Length];
    eventArgs.Row.AttributeValues = new string[eventArgs.Row.AttributeIDs.Length];
  }

  protected virtual void _(PX.Data.Events.RowSelected<MainItemType> eventArgs)
  {
    ((PXAction) this.showMatrixPanel).SetEnabled(this.IsDocumentOpen());
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem> eventArgs)
  {
    int num;
    if (this.IsDocumentOpen())
    {
      string[] attributeIdentifiers = ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.AttributeIdentifiers;
      num = attributeIdentifiers != null ? (attributeIdentifiers.Length != 0 ? 1 : 0) : 0;
    }
    else
      num = 0;
    bool flag = num != 0;
    ((PXSelectBase) this.MatrixItems).AllowInsert = flag;
    ((PXSelectBase) this.MatrixItems).AllowDelete = flag;
    ((PXSelectBase) this.MatrixItems).AllowUpdate = flag;
    if (eventArgs.Row == null)
      return;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem>>) eventArgs).Cache, (object) eventArgs.Row);
    attributeAdjuster.ForAllFields((Action<PXUIFieldAttribute>) (a => a.Enabled = false));
    bool allowEditRow = this.AllAttributesArePopulated(eventArgs.Row);
    Exception exception = (Exception) null;
    if (allowEditRow)
    {
      PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem row = eventArgs.Row;
      if ((row != null ? (row.InventoryID.HasValue ? 1 : 0) : 0) != 0)
      {
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, eventArgs.Row.InventoryID);
        if (this.IsItemStatusDisabled(inventoryItem))
        {
          allowEditRow = false;
          exception = (Exception) new PXSetPropertyException("The inventory item is {0}.", (PXErrorLevel) 2, new object[1]
          {
            (object) PXStringListAttribute.GetLocalizedLabel<PX.Objects.IN.InventoryItem.itemStatus>((PXCache) GraphHelper.Caches<PX.Objects.IN.InventoryItem>((PXGraph) this.Base), (object) inventoryItem)
          });
        }
      }
    }
    attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem>>) eventArgs).Cache, (object) eventArgs.Row);
    attributeAdjuster.For<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem.qty>((Action<PXUIFieldAttribute>) (a => a.Enabled = allowEditRow)).SameFor<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem.taxCategoryID>();
    attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem>>) eventArgs).Cache, (object) eventArgs.Row);
    attributeAdjuster.For<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem.uOM>((Action<PXUIFieldAttribute>) (a => a.Enabled = allowEditRow && !eventArgs.Row.UOMDisabled.GetValueOrDefault()));
    if (eventArgs.Row.Exists.GetValueOrDefault())
      exception = (Exception) new PXSetPropertyException("The item with the same inventory ID already exists. Change segment settings of the inventory ID.", (PXErrorLevel) 4);
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem>>) eventArgs).Cache.RaiseExceptionHandling<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem.inventoryCD>((object) eventArgs.Row, (object) eventArgs.Row.InventoryCD, exception);
  }

  protected virtual bool AllAttributesArePopulated(PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem row)
  {
    if (row == null)
      return false;
    string[] attributeValues = row.AttributeValues;
    bool? nullable = attributeValues != null ? new bool?(((IEnumerable<string>) attributeValues).Any<string>((Func<string, bool>) (v => string.IsNullOrEmpty(v)))) : new bool?();
    bool flag = false;
    return nullable.GetValueOrDefault() == flag & nullable.HasValue;
  }

  protected override void _(
    PX.Data.Events.FieldUpdated<EntryHeader, EntryHeader.templateItemID> eventArgs)
  {
    base._(eventArgs);
    if (eventArgs.Row == null)
      return;
    eventArgs.Row.Description = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, eventArgs.Row.TemplateItemID)?.Descr;
  }

  protected virtual void IncreaseQty(
    int? siteID,
    int inventoryID,
    string taxCategoryID,
    Decimal addQty,
    string uom)
  {
    IMatrixItemLine line = this.GetLines(siteID, new int?(inventoryID), taxCategoryID, uom).FirstOrDefault<IMatrixItemLine>();
    if (line != null)
    {
      IMatrixItemLine matrixItemLine = line;
      Decimal? qty = matrixItemLine.Qty;
      Decimal num = addQty;
      matrixItemLine.Qty = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() + num) : new Decimal?();
      this.UpdateLine(line);
    }
    else
      this.CreateNewLine(siteID, new int?(inventoryID), taxCategoryID, addQty, uom);
  }

  protected virtual void AddItemsToOrder(int? siteID)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SmartPanelExt<Graph, MainItemType>.\u003C\u003Ec__DisplayClass39_0 cDisplayClass390 = new SmartPanelExt<Graph, MainItemType>.\u003C\u003Ec__DisplayClass39_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass390.siteID = siteID;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass390.templateItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, ((PXSelectBase<EntryHeader>) this.Header).Current.TemplateItemID);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass390.itemsToCreate = GraphHelper.RowCast<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem>(((PXSelectBase) this.MatrixItems).Cache.Cached).Where<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem>((Func<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem, bool>) (mi => mi.New.GetValueOrDefault())).ToList<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem>();
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass390.templateItem == null)
      return;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass390.clone = GraphHelper.Clone<Graph>(this.Base);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass390, __methodptr(\u003CAddItemsToOrder\u003Eb__1)));
  }

  protected virtual IEnumerable<SmartPanelExt<Graph, MainItemType>.InventoryMatrixResult> GetResult()
  {
    foreach (object obj in ((PXSelectBase) this.Matrix).Cache.Cached)
    {
      EntryMatrix matrix = obj as EntryMatrix;
      for (int columnIndex = 0; columnIndex < matrix.Quantities.Length; ++columnIndex)
      {
        int? inventoryId = matrix.InventoryIDs[columnIndex];
        Decimal? quantity = matrix.Quantities[columnIndex];
        if (inventoryId.HasValue)
          yield return new SmartPanelExt<Graph, MainItemType>.InventoryMatrixResult()
          {
            InventoryID = inventoryId.Value,
            Qty = quantity.GetValueOrDefault()
          };
      }
      matrix = (EntryMatrix) null;
    }
  }

  protected virtual void AddMatrixItemsToOrder(int? siteID)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) new SmartPanelExt<Graph, MainItemType>.\u003C\u003Ec__DisplayClass41_0()
    {
      siteID = siteID,
      clone = GraphHelper.Clone<Graph>(this.Base)
    }, __methodptr(\u003CAddMatrixItemsToOrder\u003Eb__0)));
  }

  protected virtual void IncreaseQty(int? siteID, int inventoryID, Decimal addQty)
  {
    IMatrixItemLine line = this.GetLines(siteID, new int?(inventoryID)).FirstOrDefault<IMatrixItemLine>();
    if (line != null)
    {
      IMatrixItemLine matrixItemLine = line;
      Decimal? qty = matrixItemLine.Qty;
      Decimal num = addQty;
      matrixItemLine.Qty = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() + num) : new Decimal?();
      this.UpdateLine(line);
    }
    else
      this.CreateNewLine(siteID, new int?(inventoryID), addQty);
  }

  protected virtual void DecreaseQty(int? siteID, int inventoryID, Decimal addQty)
  {
    Decimal num1 = addQty * -1M;
    foreach (IMatrixItemLine line in this.GetLines(siteID, new int?(inventoryID)))
    {
      Decimal? qty1 = line.Qty;
      Decimal num2 = num1;
      if (qty1.GetValueOrDefault() >= num2 & qty1.HasValue)
      {
        IMatrixItemLine matrixItemLine = line;
        Decimal? qty2 = matrixItemLine.Qty;
        Decimal num3 = num1;
        matrixItemLine.Qty = qty2.HasValue ? new Decimal?(qty2.GetValueOrDefault() - num3) : new Decimal?();
        this.UpdateLine(line);
        break;
      }
      num1 -= line.Qty.GetValueOrDefault();
      line.Qty = new Decimal?(0M);
      this.UpdateLine(line);
    }
  }

  protected override void FillInventoryMatrixItem(
    EntryMatrix newRow,
    int colAttributeIndex,
    MatrixGridExt<Graph, MainItemType>.InventoryMapValue inventoryValue)
  {
    if ((inventoryValue != null ? (!inventoryValue.InventoryID.HasValue ? 1 : 0) : 1) != 0)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, inventoryValue.InventoryID);
    if (!this.IsItemStatusDisabled(inventoryItem))
    {
      newRow.InventoryIDs[colAttributeIndex] = inventoryValue.InventoryID;
      (string UOM, Decimal? Qty, int Count) qty = this.GetQty(((PXSelectBase<EntryHeader>) this.Header).Current.SiteID, inventoryValue.InventoryID);
      if (qty.Qty.HasValue)
      {
        newRow.Quantities[colAttributeIndex] = qty.Qty;
        newRow.UOMs[colAttributeIndex] = qty.UOM;
        if (qty.UOM == inventoryItem.BaseUnit)
          newRow.BaseQuantities[colAttributeIndex] = qty.Qty;
        else
          newRow.BaseQuantities[colAttributeIndex] = new Decimal?(INUnitAttribute.ConvertToBase(((PXSelectBase) this.Matrix).Cache, inventoryValue.InventoryID, qty.UOM, qty.Qty.GetValueOrDefault(), INPrecision.QUANTITY));
      }
      else
      {
        newRow.InventoryIDs[colAttributeIndex] = new int?();
        newRow.Errors[colAttributeIndex] = PXLocalizer.Localize("Specify the same UOM in all lines with the selected inventory item before using inventory matrix.");
      }
    }
    else
    {
      string localizedLabel = PXStringListAttribute.GetLocalizedLabel<PX.Objects.IN.InventoryItem.itemStatus>((PXCache) GraphHelper.Caches<PX.Objects.IN.InventoryItem>((PXGraph) this.Base), (object) inventoryItem);
      newRow.Errors[colAttributeIndex] = PXLocalizer.LocalizeFormat("The inventory item is {0}.", new object[1]
      {
        (object) localizedLabel
      });
    }
  }

  protected virtual (string UOM, Decimal? Qty, int Count) GetQty(int? siteID, int? inventoryID)
  {
    IMatrixItemLine[] array = this.GetLines(siteID, inventoryID).ToArray<IMatrixItemLine>();
    string firstUOM = ((IEnumerable<IMatrixItemLine>) array).FirstOrDefault<IMatrixItemLine>()?.UOM ?? this.GetDefaultUOM(inventoryID);
    return ((IEnumerable<IMatrixItemLine>) array).Any<IMatrixItemLine>((Func<IMatrixItemLine, bool>) (l => l.UOM != firstUOM)) ? (firstUOM, new Decimal?(), array.Length) : (firstUOM, new Decimal?(((IEnumerable<IMatrixItemLine>) array).Sum<IMatrixItemLine>((Func<IMatrixItemLine, Decimal?>) (l => l.Qty)).GetValueOrDefault()), array.Length);
  }

  protected override void FieldSelectingImpl(
    int attributeNumber,
    PXCache s,
    PXFieldSelectingEventArgs e,
    string fieldName)
  {
    int? valueFromArray1 = MatrixGridExt<Graph, MainItemType>.GetValueFromArray<int?>(e.Row is EntryMatrix row ? row.InventoryIDs : (int?[]) null, attributeNumber);
    Decimal? valueFromArray2 = MatrixGridExt<Graph, MainItemType>.GetValueFromArray<Decimal?>(row?.Quantities, attributeNumber);
    string valueFromArray3 = MatrixGridExt<Graph, MainItemType>.GetValueFromArray<string>(row?.Errors, attributeNumber);
    PXFieldState instance = PXDecimalState.CreateInstance(e.ReturnState, this._precision.Value, fieldName, new bool?(false), new int?(0), new Decimal?(0M), new Decimal?());
    instance.Enabled = valueFromArray1.HasValue && this.IsDocumentOpen();
    instance.Error = valueFromArray3;
    instance.ErrorLevel = string.IsNullOrEmpty(valueFromArray3) ? (PXErrorLevel) 0 : (PXErrorLevel) 2;
    e.ReturnState = (object) instance;
    e.ReturnValue = (object) (valueFromArray1.HasValue || row != null && row.IsTotal.GetValueOrDefault() ? valueFromArray2 : new Decimal?());
    EntryMatrix entryMatrix = NonGenericIEnumerableExtensions.FirstOrDefault_(s.Cached) as EntryMatrix;
    int num = attributeNumber;
    int? length = entryMatrix?.ColAttributeValueDescrs?.Length;
    int valueOrDefault = length.GetValueOrDefault();
    if (num < valueOrDefault & length.HasValue)
    {
      instance.DisplayName = entryMatrix.ColAttributeValueDescrs[attributeNumber] ?? entryMatrix.ColAttributeValues[attributeNumber];
      instance.Visibility = (PXUIVisibility) 3;
      instance.Visible = true;
    }
    else
    {
      instance.DisplayName = (string) null;
      instance.Visibility = (PXUIVisibility) 1;
      instance.Visible = false;
    }
  }

  protected override void FieldUpdatingImpl(
    int attributeNumber,
    PXCache s,
    PXFieldUpdatingEventArgs e,
    string fieldName)
  {
    if (!(e.Row is EntryMatrix row))
      return;
    int num1 = attributeNumber;
    int? length = row.Quantities?.Length;
    int valueOrDefault = length.GetValueOrDefault();
    if (!(num1 < valueOrDefault & length.HasValue))
      return;
    Decimal num2 = e.NewValue == null ? 0M : Convert.ToDecimal(e.NewValue);
    string uoM = row.UOMs[attributeNumber];
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, row.InventoryIDs[attributeNumber]);
    row.Quantities[attributeNumber] = new Decimal?(num2);
    row.BaseQuantities[attributeNumber] = new Decimal?(uoM == inventoryItem?.BaseUnit ? num2 : INUnitAttribute.ConvertToBase(((PXSelectBase) this.Matrix).Cache, (int?) inventoryItem?.InventoryID, uoM, num2, INPrecision.QUANTITY));
    ((PXSelectBase) this.Matrix).View.RequestRefresh();
  }

  protected override void TotalFieldSelecting(
    PXCache s,
    PXFieldSelectingEventArgs e,
    string fieldName)
  {
    EntryMatrix row = e.Row as EntryMatrix;
    PXFieldState instance = PXDecimalState.CreateInstance(e.ReturnState, this._precision.Value, fieldName, new bool?(false), new int?(0), new Decimal?(0M), new Decimal?());
    e.ReturnState = (object) instance;
    instance.Enabled = false;
    instance.DisplayName = PXLocalizer.Localize("Total Qty.");
    int? length;
    int num1;
    if (!(NonGenericIEnumerableExtensions.FirstOrDefault_(s.Cached) is EntryMatrix entryMatrix))
    {
      num1 = 0;
    }
    else
    {
      length = entryMatrix.ColAttributeValueDescrs?.Length;
      int num2 = 0;
      num1 = length.GetValueOrDefault() > num2 & length.HasValue ? 1 : 0;
    }
    if (num1 != 0)
    {
      instance.Visibility = (PXUIVisibility) 3;
      instance.Visible = true;
    }
    else
    {
      instance.Visibility = (PXUIVisibility) 1;
      instance.Visible = false;
    }
    Decimal num3 = 0M;
    int index = 0;
    while (true)
    {
      int num4 = index;
      length = row?.Quantities?.Length;
      int valueOrDefault = length.GetValueOrDefault();
      if (num4 < valueOrDefault & length.HasValue)
      {
        num3 += row.IsTotal.GetValueOrDefault() || row.InventoryIDs[index].HasValue ? row.BaseQuantities[index].GetValueOrDefault() : 0M;
        ++index;
      }
      else
        break;
    }
    e.ReturnValue = (object) num3;
  }

  protected override EntryMatrix GenerateTotalRow(IEnumerable<EntryMatrix> rows)
  {
    bool flag = false;
    EntryMatrix instance = (EntryMatrix) ((PXSelectBase) this.Matrix).Cache.CreateInstance();
    foreach (EntryMatrix entryMatrix in ((PXSelectBase) this.Matrix).Cache.Cached)
    {
      flag = true;
      if (instance.Quantities == null)
        instance.Quantities = new Decimal?[entryMatrix.Quantities.Length];
      if (instance.BaseQuantities == null)
        instance.BaseQuantities = new Decimal?[entryMatrix.Quantities.Length];
      if (instance.UOMs == null)
        instance.UOMs = new string[entryMatrix.Quantities.Length];
      if (instance.BaseUOM == null)
        instance.BaseUOM = entryMatrix.BaseUOM;
      for (int index = 0; index < entryMatrix.Quantities.Length; ++index)
      {
        instance.Quantities[index] = new Decimal?(instance.Quantities[index].GetValueOrDefault());
        ref Decimal? local = ref instance.Quantities[index];
        Decimal? nullable1 = local;
        Decimal? nullable2 = entryMatrix.InventoryIDs[index].HasValue ? entryMatrix.BaseQuantities[index] : new Decimal?(0M);
        local = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        instance.BaseQuantities[index] = instance.Quantities[index];
        instance.UOMs[index] = instance.BaseUOM;
      }
    }
    instance.RowAttributeValueDescr = PXLocalizer.Localize("Total Qty.");
    instance.IsTotal = new bool?(true);
    instance.LineNbr = new int?(int.MaxValue);
    return !flag ? (EntryMatrix) null : instance;
  }

  protected virtual string GetAvailability(
    int? siteID,
    int? inventoryID,
    Decimal? qty,
    string uom)
  {
    if (!inventoryID.HasValue)
      return (string) null;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, inventoryID);
    if (inventoryItem == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.IN.InventoryItem>((PXGraph) this.Base), new object[1]
      {
        (object) inventoryID
      });
    if (!inventoryItem.StkItem.GetValueOrDefault())
      return (string) null;
    int? siteID1 = siteID;
    if (!siteID1.HasValue)
    {
      PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, this.GetDefaultBranch());
      siteID1 = (int?) InventoryItemCurySettings.PK.Find((PXGraph) this.Base, inventoryID, branch?.BaseCuryID)?.DfltSiteID;
    }
    SiteStatusByCostCenter row = new SiteStatusByCostCenter();
    row.InventoryID = inventoryID;
    row.SubItemID = inventoryItem.DefaultSubItemID;
    row.SiteID = siteID1;
    row.CostCenterID = new int?(0);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    SiteStatusByCostCenter copy = PXCache<SiteStatusByCostCenter>.CreateCopy(this.InsertWith<SiteStatusByCostCenter>((PXGraph) this.Base, row, SmartPanelExt<Graph, MainItemType>.\u003C\u003Ec.\u003C\u003E9__50_0 ?? (SmartPanelExt<Graph, MainItemType>.\u003C\u003Ec.\u003C\u003E9__50_0 = new PXRowInserted((object) SmartPanelExt<Graph, MainItemType>.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CGetAvailability\u003Eb__50_0)))));
    INSiteStatusByCostCenter statusByCostCenter1 = INSiteStatusByCostCenter.PK.Find((PXGraph) this.Base, inventoryID, inventoryItem.DefaultSubItemID, siteID1, new int?(0));
    if (statusByCostCenter1 != null)
    {
      SiteStatusByCostCenter statusByCostCenter2 = copy;
      Decimal? qtyOnHand = statusByCostCenter2.QtyOnHand;
      Decimal? nullable1 = statusByCostCenter1.QtyOnHand;
      statusByCostCenter2.QtyOnHand = qtyOnHand.HasValue & nullable1.HasValue ? new Decimal?(qtyOnHand.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
      SiteStatusByCostCenter statusByCostCenter3 = copy;
      nullable1 = statusByCostCenter3.QtyAvail;
      Decimal? nullable2 = statusByCostCenter1.QtyAvail;
      statusByCostCenter3.QtyAvail = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      SiteStatusByCostCenter statusByCostCenter4 = copy;
      nullable2 = statusByCostCenter4.QtyHardAvail;
      nullable1 = statusByCostCenter1.QtyHardAvail;
      statusByCostCenter4.QtyHardAvail = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
      SiteStatusByCostCenter statusByCostCenter5 = copy;
      nullable1 = statusByCostCenter5.QtyActual;
      nullable2 = statusByCostCenter1.QtyActual;
      statusByCostCenter5.QtyActual = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      SiteStatusByCostCenter statusByCostCenter6 = copy;
      nullable2 = statusByCostCenter6.QtyPOOrders;
      nullable1 = statusByCostCenter1.QtyPOOrders;
      statusByCostCenter6.QtyPOOrders = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    }
    foreach (IMatrixItemLine line in this.GetLines(siteID1, inventoryID))
      this.DeductAllocated(copy, line);
    if (uom != inventoryItem.BaseUnit)
    {
      Decimal num = INUnitAttribute.ConvertFromBase(((PXSelectBase) this.Matrix).Cache, inventoryID, uom, 1M, INPrecision.NOROUND);
      SiteStatusByCostCenter statusByCostCenter7 = copy;
      Decimal? nullable3 = copy.QtyOnHand;
      Decimal? nullable4 = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(nullable3.GetValueOrDefault() * num)));
      statusByCostCenter7.QtyOnHand = nullable4;
      SiteStatusByCostCenter statusByCostCenter8 = copy;
      nullable3 = copy.QtyAvail;
      Decimal? nullable5 = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(nullable3.GetValueOrDefault() * num)));
      statusByCostCenter8.QtyAvail = nullable5;
      SiteStatusByCostCenter statusByCostCenter9 = copy;
      nullable3 = copy.QtyHardAvail;
      Decimal? nullable6 = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(nullable3.GetValueOrDefault() * num)));
      statusByCostCenter9.QtyHardAvail = nullable6;
      SiteStatusByCostCenter statusByCostCenter10 = copy;
      nullable3 = copy.QtyHardAvail;
      Decimal? nullable7 = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(nullable3.GetValueOrDefault() * num)));
      statusByCostCenter10.QtyHardAvail = nullable7;
      SiteStatusByCostCenter statusByCostCenter11 = copy;
      nullable3 = copy.QtyPOOrders;
      Decimal? nullable8 = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(nullable3.GetValueOrDefault() * num)));
      statusByCostCenter11.QtyPOOrders = nullable8;
    }
    return this.GetAvailabilityMessage(siteID1, inventoryItem, copy, uom);
  }

  protected abstract string GetAvailabilityMessage(
    int? siteID,
    PX.Objects.IN.InventoryItem item,
    SiteStatusByCostCenter allocated,
    string uom);

  protected T InsertWith<T>(PXGraph graph, T row, PXRowInserted handler) where T : class, IBqlTable, new()
  {
    graph.RowInserted.AddHandler<T>(handler);
    try
    {
      return PXCache<T>.Insert(graph, row);
    }
    finally
    {
      graph.RowInserted.RemoveHandler<T>(handler);
    }
  }

  protected virtual string FormatQty(Decimal? value)
  {
    return value.HasValue ? value.Value.ToString("N" + CommonSetupDecPl.Qty.ToString(), (IFormatProvider) NumberFormatInfo.CurrentInfo) : string.Empty;
  }

  protected abstract void DeductAllocated(SiteStatusByCostCenter allocated, IMatrixItemLine line);

  protected virtual void _(
    PX.Data.Events.FieldSelecting<EntryMatrix, EntryMatrix.matrixAvailability> eventArgs)
  {
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<EntryMatrix, EntryMatrix.matrixAvailability>>) eventArgs).ReturnValue = (object) null;
    EntryMatrix row = eventArgs.Row;
    if (row == null || !row.SelectedColumn.HasValue)
      return;
    EntryHeader current = ((PXSelectBase<EntryHeader>) this.Header).Current;
    if ((current != null ? (current.ShowAvailable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    int index = row.SelectedColumn.Value;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<EntryMatrix, EntryMatrix.matrixAvailability>>) eventArgs).ReturnValue = (object) this.GetAvailability(((PXSelectBase<EntryHeader>) this.Header).Current.SiteID, MatrixGridExt<Graph, MainItemType>.GetValueFromArray<int?>(row.InventoryIDs, index), MatrixGridExt<Graph, MainItemType>.GetValueFromArray<Decimal?>(row.Quantities, index), MatrixGridExt<Graph, MainItemType>.GetValueFromArray<string>(row.UOMs, index));
  }

  protected override void OnMatrixGridCellCahnged()
  {
    object obj = (object) null;
    ((PXSelectBase) this.Matrix).Cache.RaiseFieldSelecting<EntryMatrix.matrixAvailability>((object) ((PXSelectBase<EntryMatrix>) this.Matrix).Current, ref obj, true);
  }

  public class InventoryMatrixResult
  {
    public int InventoryID { get; set; }

    public Decimal Qty { get; set; }
  }
}
