// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.GraphExtensions.CreateMatrixItemsExt`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Collection;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.IN.Matrix.DAC;
using PX.Objects.IN.Matrix.DAC.Unbound;
using PX.Objects.IN.Matrix.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.Matrix.GraphExtensions;

public abstract class CreateMatrixItemsExt<TGraph, TMainItem> : MatrixGridExt<TGraph, TMainItem>
  where TGraph : PXGraph, new()
  where TMainItem : class, IBqlTable, new()
{
  public PXSelect<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem> MatrixItemsForCreation;
  public PXAction<TMainItem> createMatrixItems;
  public PXAction<TMainItem> createUpdate;

  public override bool AddPreliminary => true;

  public override bool ShowDisabledValue => false;

  [PXGuid]
  public virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem.noteID> e)
  {
  }

  [PXUIField]
  [PXButton(IsLockedOnToolbar = true)]
  public virtual IEnumerable CreateMatrixItems(PXAdapter adapter)
  {
    if (this.Base.IsImport || this.Base.IsContractBasedAPI)
    {
      this.PrepareMatrixItemList();
      return this.CreateUpdate(adapter);
    }
    // ISSUE: method pointer
    return ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem>) this.MatrixItemsForCreation).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CCreateMatrixItems\u003Eb__8_0))) == 1 ? this.CreateUpdate(adapter) : adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CreateUpdate(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CreateMatrixItemsExt<TGraph, TMainItem>.\u003C\u003Ec__DisplayClass10_0 cDisplayClass100 = new CreateMatrixItemsExt<TGraph, TMainItem>.\u003C\u003Ec__DisplayClass10_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass100.templateItem = this.GetTemplateItem();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass100.itemsToCreate = GraphHelper.RowCast<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem>(((PXSelectBase) this.MatrixItemsForCreation).Cache.Cached).Where<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem>((Func<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem, bool>) (mi => mi.Selected.GetValueOrDefault())).ToList<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass100.additionalAttributes = ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.Values;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass100.columnAttributeID = ((PXSelectBase<EntryHeader>) this.Header).Current.ColAttributeID;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass100.rowAttributeID = ((PXSelectBase<EntryHeader>) this.Header).Current.RowAttributeID;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass100.templateItem != null && cDisplayClass100.itemsToCreate.Any<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem>())
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass100.isImportOrApi = this.Base.IsImport || this.Base.IsContractBasedAPI;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass100, __methodptr(\u003CCreateUpdate\u003Eb__1)));
    }
    return adapter.Get();
  }

  protected virtual IEnumerable matrixItemsForCreation()
  {
    ((PXSelectBase) this.MatrixItemsForCreation).Cache.IsDirty = false;
    return ((PXSelectBase) this.MatrixItemsForCreation).Cache.Cached;
  }

  public override void Initialize()
  {
    base.Initialize();
    string[] itemsForCreation = this.GetAttributesOfMatrixItemsForCreation();
    int attributeNumber = 0;
    while (true)
    {
      int num = attributeNumber;
      int? length = itemsForCreation?.Length;
      int valueOrDefault = length.GetValueOrDefault();
      if (num < valueOrDefault & length.HasValue)
      {
        this.AddItemAttributeColumn(attributeNumber);
        ++attributeNumber;
      }
      else
        break;
    }
    PXUIFieldAttribute.SetVisible<EntryHeader.showAvailable>(((PXSelectBase) this.Header).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<EntryHeader.siteID>(((PXSelectBase) this.Header).Cache, (object) null, false);
  }

  private string[] GetAttributesOfMatrixItemsForCreation()
  {
    string[] attributeIds = ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem>) this.MatrixItemsForCreation).Current?.AttributeIDs;
    if (attributeIds != null)
      return attributeIds;
    return GraphHelper.RowCast<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem>(((PXSelectBase) this.MatrixItemsForCreation).Cache.Cached).FirstOrDefault<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem>((Func<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem, bool>) (r => ((PXSelectBase) this.MatrixItemsForCreation).Cache.GetStatus((object) r) == 5))?.AttributeIDs;
  }

  protected virtual void AddItemAttributeColumn(int attributeNumber)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CreateMatrixItemsExt<TGraph, TMainItem>.\u003C\u003Ec__DisplayClass14_0 cDisplayClass140 = new CreateMatrixItemsExt<TGraph, TMainItem>.\u003C\u003Ec__DisplayClass14_0()
    {
      \u003C\u003E4__this = this,
      attributeNumber = attributeNumber
    };
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass140.fieldName = $"AttributeValue{cDisplayClass140.attributeNumber}";
    // ISSUE: reference to a compiler-generated field
    if (((PXSelectBase) this.MatrixItemsForCreation).Cache.Fields.Contains(cDisplayClass140.fieldName))
      return;
    // ISSUE: reference to a compiler-generated field
    ((PXSelectBase) this.MatrixItemsForCreation).Cache.Fields.Add(cDisplayClass140.fieldName);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    this.Base.FieldSelecting.AddHandler(((PXSelectBase) this.MatrixItemsForCreation).Cache.GetItemType(), cDisplayClass140.fieldName, new PXFieldSelecting((object) cDisplayClass140, __methodptr(\u003CAddItemAttributeColumn\u003Eb__0)));
  }

  protected virtual void ItemAttributeValueFieldSelecting(
    int attributeNumber,
    PXFieldSelectingEventArgs e,
    string fieldName)
  {
    PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem row = (PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem) e.Row;
    PXStringState instance = (PXStringState) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(true), fieldName, new bool?(false), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
    ((PXFieldState) instance).Enabled = false;
    e.ReturnState = (object) instance;
    string[] itemsForCreation = this.GetAttributesOfMatrixItemsForCreation();
    int num1 = attributeNumber;
    int? length = itemsForCreation?.Length;
    int valueOrDefault1 = length.GetValueOrDefault();
    if (num1 < valueOrDefault1 & length.HasValue && itemsForCreation[attributeNumber] != "~MX~DUMMY~")
    {
      PXFieldSelectingEventArgs selectingEventArgs = e;
      string str;
      if (0 <= attributeNumber)
      {
        int num2 = attributeNumber;
        length = row?.AttributeValues?.Length;
        int valueOrDefault2 = length.GetValueOrDefault();
        if (num2 < valueOrDefault2 & length.HasValue)
        {
          str = row.AttributeValues[attributeNumber];
          goto label_5;
        }
      }
      str = (string) null;
label_5:
      selectingEventArgs.ReturnValue = (object) str;
      ((PXFieldState) instance).Visible = true;
      ((PXFieldState) instance).Visibility = (PXUIVisibility) 3;
      CRAttribute.Attribute attribute;
      if (!((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes).TryGetValue(itemsForCreation[attributeNumber], ref attribute))
        return;
      ((PXFieldState) instance).DisplayName = attribute.Description;
      instance.AllowedValues = attribute.Values.Select<CRAttribute.AttributeValue, string>((Func<CRAttribute.AttributeValue, string>) (v => v.ValueID)).ToArray<string>();
      instance.AllowedLabels = attribute.Values.Select<CRAttribute.AttributeValue, string>((Func<CRAttribute.AttributeValue, string>) (v => v.Description)).ToArray<string>();
    }
    else
    {
      ((PXFieldState) instance).Value = (object) null;
      e.ReturnValue = (object) null;
      ((PXFieldState) instance).DisplayName = (string) null;
      ((PXFieldState) instance).Visible = false;
      ((PXFieldState) instance).Visibility = (PXUIVisibility) 1;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<EntryMatrix> e)
  {
    ((PXAction) this.createMatrixItems).SetEnabled(((PXSelectBase) this.Matrix).Cache.IsDirty);
  }

  protected override void FieldSelectingImpl(
    int attributeNumber,
    PXCache s,
    PXFieldSelectingEventArgs e,
    string fieldName)
  {
    EntryMatrix entryMatrix1 = (EntryMatrix) e.Row;
    object returnState = e.ReturnState;
    System.Type type = typeof (bool);
    string str1 = fieldName;
    bool? nullable1 = new bool?();
    bool? nullable2 = new bool?();
    int? nullable3 = new int?();
    int? nullable4 = new int?();
    int? nullable5 = new int?();
    string str2 = str1;
    bool? nullable6 = new bool?();
    bool? nullable7 = new bool?();
    bool? nullable8 = new bool?();
    PXFieldState instance = PXFieldState.CreateInstance(returnState, type, nullable1, nullable2, nullable3, nullable4, nullable5, (object) null, str2, (string) null, (string) null, (string) null, (PXErrorLevel) 0, nullable6, nullable7, nullable8, (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    int? valueFromArray1 = MatrixGridExt<TGraph, TMainItem>.GetValueFromArray<int?>(entryMatrix1?.InventoryIDs, attributeNumber);
    bool? valueFromArray2 = MatrixGridExt<TGraph, TMainItem>.GetValueFromArray<bool?>(entryMatrix1?.Selected, attributeNumber);
    string valueFromArray3 = MatrixGridExt<TGraph, TMainItem>.GetValueFromArray<string>(entryMatrix1?.Errors, attributeNumber);
    instance.Enabled = !valueFromArray1.HasValue && this.AllAdditionalAttributesArePopulated();
    instance.Error = valueFromArray3;
    instance.ErrorLevel = string.IsNullOrEmpty(valueFromArray3) ? (PXErrorLevel) 0 : (PXErrorLevel) 2;
    if (entryMatrix1 == null)
      entryMatrix1 = this.GetFirstMatrixRow();
    EntryMatrix entryMatrix2 = entryMatrix1;
    int num = attributeNumber;
    int? length = entryMatrix2?.ColAttributeValueDescrs?.Length;
    int valueOrDefault = length.GetValueOrDefault();
    if (num < valueOrDefault & length.HasValue)
    {
      instance.DisplayName = entryMatrix2.ColAttributeValueDescrs[attributeNumber] ?? entryMatrix2.ColAttributeValues[attributeNumber];
      instance.Visibility = (PXUIVisibility) 3;
      instance.Visible = true;
    }
    else
    {
      instance.DisplayName = (string) null;
      instance.Visibility = (PXUIVisibility) 1;
      instance.Visible = false;
    }
    e.ReturnState = (object) instance;
    e.ReturnValue = (object) valueFromArray2;
  }

  protected override void FieldUpdatingImpl(
    int attributeNumber,
    PXCache s,
    PXFieldUpdatingEventArgs e,
    string fieldName)
  {
    EntryMatrix row1 = (EntryMatrix) e.Row;
    if (row1 == null)
      return;
    int num = attributeNumber;
    int? length = row1.Selected?.Length;
    int valueOrDefault = length.GetValueOrDefault();
    if (!(num < valueOrDefault & length.HasValue))
      return;
    bool? newValue = (bool?) e.NewValue;
    row1.Selected[attributeNumber] = newValue;
    if (!row1.IsPreliminary.GetValueOrDefault())
      return;
    foreach (EntryMatrix entryMatrix in ((PXSelectBase) this.Matrix).Cache.Cached.Cast<EntryMatrix>().Where<EntryMatrix>((Func<EntryMatrix, bool>) (row => !row.IsPreliminary.GetValueOrDefault())))
    {
      if (!entryMatrix.InventoryIDs[attributeNumber].HasValue)
        entryMatrix.Selected[attributeNumber] = newValue;
    }
    ((PXSelectBase) this.Matrix).View.RequestRefresh();
  }

  protected override void FillInventoryMatrixItem(
    EntryMatrix newRow,
    int colAttributeIndex,
    MatrixGridExt<TGraph, TMainItem>.InventoryMapValue inventoryValue)
  {
    newRow.InventoryIDs[colAttributeIndex] = (int?) inventoryValue?.InventoryID;
    newRow.Selected[colAttributeIndex] = new bool?(inventoryValue != null && inventoryValue.InventoryID.HasValue);
  }

  protected virtual void PrepareMatrixItemList()
  {
    ((PXSelectBase) this.MatrixItemsForCreation).Cache.Clear();
    InventoryItem templateItem = this.GetTemplateItem();
    int num = 0;
    CreateMatrixItemsHelper helper = this.GetHelper((PXGraph) this.Base);
    List<INMatrixGenerationRule> idGenerationRules;
    List<INMatrixGenerationRule> descrGenerationRules;
    this.GetGenerationRules(helper, out idGenerationRules, out descrGenerationRules);
    foreach (EntryMatrix row in ((PXSelectBase) this.Matrix).Cache.Cached.Cast<EntryMatrix>().Where<EntryMatrix>((Func<EntryMatrix, bool>) (row => !row.IsPreliminary.GetValueOrDefault())))
    {
      for (int attributeNumber1 = 0; attributeNumber1 < row.InventoryIDs.Length; ++attributeNumber1)
      {
        PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem itemFromTemplate = helper.CreateMatrixItemFromTemplate(row, attributeNumber1, templateItem, idGenerationRules, descrGenerationRules);
        if (itemFromTemplate != null)
        {
          bool flag = num == 0;
          if (flag)
          {
            for (int attributeNumber2 = 0; attributeNumber2 < itemFromTemplate.AttributeIDs.Length; ++attributeNumber2)
              this.AddItemAttributeColumn(attributeNumber2);
          }
          itemFromTemplate.InventoryID = new int?(++num);
          GraphHelper.Hold(((PXSelectBase) this.MatrixItemsForCreation).Cache, (object) itemFromTemplate);
          if (flag)
            ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem>) this.MatrixItemsForCreation).Current = itemFromTemplate;
        }
      }
    }
  }

  protected virtual void GetGenerationRules(
    CreateMatrixItemsHelper helper,
    out List<INMatrixGenerationRule> idGenerationRules,
    out List<INMatrixGenerationRule> descrGenerationRules)
  {
    helper.GetGenerationRules(((PXSelectBase<EntryHeader>) this.Header).Current.TemplateItemID, out idGenerationRules, out descrGenerationRules);
  }

  protected virtual CreateMatrixItemsHelper GetHelper(PXGraph graph)
  {
    return new CreateMatrixItemsHelper(graph);
  }

  public virtual AttributeGroupHelper GetAttributeGroupHelper(PXGraph graph)
  {
    return new AttributeGroupHelper(graph);
  }

  protected override EntryMatrix GeneratePreliminaryRow(IEnumerable<EntryMatrix> rows)
  {
    EntryMatrix instance = (EntryMatrix) ((PXSelectBase) this.Matrix).Cache.CreateInstance();
    instance.RowAttributeValueDescr = PXLocalizer.Localize("Select Column");
    instance.IsPreliminary = new bool?(true);
    instance.LineNbr = new int?(-1);
    EntryMatrix entryMatrix = rows.FirstOrDefault<EntryMatrix>();
    instance.Selected = new bool?[entryMatrix != null ? entryMatrix.InventoryIDs.Length : 0];
    instance.InventoryIDs = new int?[entryMatrix != null ? entryMatrix.InventoryIDs.Length : 0];
    return instance;
  }

  protected override void PreliminaryFieldSelecting(
    PXCache s,
    PXFieldSelectingEventArgs e,
    string fieldName)
  {
    EntryMatrix row = (EntryMatrix) e.Row;
    object returnState = e.ReturnState;
    System.Type type = typeof (bool);
    string str1 = fieldName;
    bool? nullable1 = new bool?();
    bool? nullable2 = new bool?();
    int? nullable3 = new int?();
    int? nullable4 = new int?();
    int? nullable5 = new int?();
    string str2 = str1;
    bool? nullable6 = new bool?();
    bool? nullable7 = new bool?();
    bool? nullable8 = new bool?();
    PXFieldState instance = PXFieldState.CreateInstance(returnState, type, nullable1, nullable2, nullable3, nullable4, nullable5, (object) null, str2, (string) null, (string) null, (string) null, (PXErrorLevel) 0, nullable6, nullable7, nullable8, (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    instance.Enabled = this.AllAdditionalAttributesArePopulated();
    instance.DisplayName = PXLocalizer.Localize("Select Row");
    instance.Visibility = (PXUIVisibility) 3;
    instance.Visible = true;
    e.ReturnState = (object) instance;
    e.ReturnValue = (object) (bool?) row?.AllSelected;
  }

  protected override void PreliminaryFieldUpdating(
    PXCache s,
    PXFieldUpdatingEventArgs e,
    string fieldName)
  {
    EntryMatrix row1 = (EntryMatrix) e.Row;
    if (row1 == null)
      return;
    bool? newValue = (bool?) e.NewValue;
    row1.AllSelected = newValue;
    int index = 0;
    while (true)
    {
      int num = index;
      int? length = row1.InventoryIDs?.Length;
      int valueOrDefault = length.GetValueOrDefault();
      if (num < valueOrDefault & length.HasValue)
      {
        if (!row1.InventoryIDs[index].HasValue)
          row1.Selected[index] = newValue;
        ((PXSelectBase) this.Matrix).View.RequestRefresh();
        ++index;
      }
      else
        break;
    }
    if (!row1.IsPreliminary.GetValueOrDefault())
      return;
    foreach (object obj in ((PXSelectBase) this.Matrix).Cache.Cached.Cast<EntryMatrix>().Where<EntryMatrix>((Func<EntryMatrix, bool>) (row => !row.IsPreliminary.GetValueOrDefault())))
      ((PXSelectBase) this.Matrix).Cache.SetValueExt(obj, fieldName, (object) newValue);
  }

  protected virtual void SetFilter(
    int? templateID,
    string columnAttributeID,
    string rowAttributeID,
    string[] additionalAttributes)
  {
    ((PXSelectBase<EntryHeader>) this.Header).Current.TemplateItemID = templateID;
    ((PXSelectBase<EntryHeader>) this.Header).Current.ColAttributeID = columnAttributeID;
    ((PXSelectBase<EntryHeader>) this.Header).Current.RowAttributeID = rowAttributeID;
    ((PXSelectBase<EntryHeader>) this.Header).UpdateCurrent();
    this.RecalcAttributesGrid();
    int index1 = 0;
    while (true)
    {
      int num = index1;
      int? length = ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current?.Values.Length;
      int valueOrDefault = length.GetValueOrDefault();
      if (num < valueOrDefault & length.HasValue)
      {
        string attributeIdentifier = ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.AttributeIdentifiers[index1];
        string value = additionalAttributes[index1];
        ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.Values[index1] = value;
        string[] descriptions = ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.Descriptions;
        int index2 = index1;
        CRAttribute.Attribute attribute = ((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes)[attributeIdentifier];
        string description = attribute != null ? attribute.Values.Where<CRAttribute.AttributeValue>((Func<CRAttribute.AttributeValue, bool>) (v => string.Equals(v.ValueID, value, StringComparison.OrdinalIgnoreCase))).FirstOrDefault<CRAttribute.AttributeValue>()?.Description : (string) null;
        descriptions[index2] = description;
        ++index1;
      }
      else
        break;
    }
    this.RecalcMatrixGrid();
  }

  protected class EndCreateUpdateOperationCustomInfo : IPXCustomInfo
  {
    protected int? _templateID;
    protected string _columnAttributeID;
    protected string _rowAttributeID;
    protected string[] _additionalAttributes;

    public EndCreateUpdateOperationCustomInfo(
      int? templateID,
      string columnAttributeID,
      string rowAttributeID,
      string[] additionalAttributes)
    {
      this._templateID = templateID;
      this._columnAttributeID = columnAttributeID;
      this._rowAttributeID = rowAttributeID;
      this._additionalAttributes = additionalAttributes;
    }

    public void Complete(PXLongRunStatus status, PXGraph graph)
    {
      if (EnumerableExtensions.IsNotIn<PXLongRunStatus>(status, (PXLongRunStatus) 2, (PXLongRunStatus) 3))
        return;
      // ISSUE: method pointer
      graph.RowSelecting.AddHandler<InventoryItem>(new PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<InventoryItem>>.EventDelegate((object) this, __methodptr(SetFilter)));
    }

    private void SetFilter(PX.Data.Events.RowSelecting<InventoryItem> e)
    {
      int? inventoryId = e.Row.InventoryID;
      int? templateId = this._templateID;
      if (!(inventoryId.GetValueOrDefault() == templateId.GetValueOrDefault() & inventoryId.HasValue == templateId.HasValue))
        return;
      PXGraph graph = ((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<InventoryItem>>) e).Cache.Graph;
      graph.FindImplementation<CreateMatrixItemsExt<TGraph, TMainItem>>()?.SetFilter(this._templateID, this._columnAttributeID, this._rowAttributeID, this._additionalAttributes);
      // ISSUE: method pointer
      graph.RowSelecting.RemoveHandler<InventoryItem>(new PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<InventoryItem>>.EventDelegate((object) this, __methodptr(SetFilter)));
    }
  }
}
