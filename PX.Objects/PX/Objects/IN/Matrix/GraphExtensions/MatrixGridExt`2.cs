// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.GraphExtensions.MatrixGridExt`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.Attributes;
using PX.Objects.IN.Matrix.DAC.Unbound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.Matrix.GraphExtensions;

public abstract class MatrixGridExt<Graph, MainItemType> : 
  HeaderAndAttributesExt<Graph, MainItemType>
  where Graph : PXGraph
  where MainItemType : class, IBqlTable, new()
{
  protected const string MatrixFieldName = "AttributeValue";
  protected Lazy<int?> _precision;
  public PXSelect<EntryMatrix> Matrix;
  public PXAction<MainItemType> MatrixGridCellChanged;

  public virtual bool AddTotals => false;

  public virtual bool AddPreliminary => false;

  public override void Initialize()
  {
    base.Initialize();
    this._precision = new Lazy<int?>(new Func<int?>(this.GetQtyPrecision));
  }

  public IEnumerable matrix()
  {
    bool preliminaryReturned = false;
    foreach (EntryMatrix entryMatrix in ((PXSelectBase) this.Matrix).Cache.Cached)
    {
      preliminaryReturned |= entryMatrix.IsPreliminary.GetValueOrDefault();
      yield return (object) entryMatrix;
    }
    if (this.AddPreliminary && !preliminaryReturned)
    {
      EntryMatrix preliminaryRow = this.GeneratePreliminaryRow(((PXSelectBase) this.Matrix).Cache.Cached.Cast<EntryMatrix>());
      if (preliminaryRow != null)
      {
        GraphHelper.Hold(((PXSelectBase) this.Matrix).Cache, (object) preliminaryRow);
        yield return (object) preliminaryRow;
      }
    }
    if (this.AddTotals)
    {
      EntryMatrix totalRow = this.GenerateTotalRow(((PXSelectBase) this.Matrix).Cache.Cached.Cast<EntryMatrix>());
      if (totalRow != null)
        yield return (object) totalRow;
    }
  }

  protected virtual EntryMatrix GeneratePreliminaryRow(IEnumerable<EntryMatrix> rows)
  {
    return (EntryMatrix) null;
  }

  protected virtual EntryMatrix GenerateTotalRow(IEnumerable<EntryMatrix> rows)
  {
    return (EntryMatrix) null;
  }

  protected void _(Events.RowPersisting<EntryMatrix> eventArgs) => eventArgs.Cancel = true;

  protected virtual int? GetQtyPrecision() => new int?(CommonSetupDecPl.Qty);

  protected override void AddNeededFields()
  {
    base.AddNeededFields();
    this.AddPreliminaryField();
    EntryMatrix firstMatrixRow = this.GetFirstMatrixRow();
    int attributeNumber = 0;
    while (true)
    {
      int num = attributeNumber;
      int? length = firstMatrixRow?.ColAttributeValueDescrs?.Length;
      int valueOrDefault = length.GetValueOrDefault();
      if (num < valueOrDefault & length.HasValue)
      {
        this.AddField(attributeNumber);
        ++attributeNumber;
      }
      else
        break;
    }
    this.AddTotalField();
  }

  protected virtual void AddField(int attributeNumber)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    MatrixGridExt<Graph, MainItemType>.\u003C\u003Ec__DisplayClass17_0 cDisplayClass170 = new MatrixGridExt<Graph, MainItemType>.\u003C\u003Ec__DisplayClass17_0()
    {
      \u003C\u003E4__this = this,
      attributeNumber = attributeNumber
    };
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass170.fieldName = $"{"AttributeValue"}{cDisplayClass170.attributeNumber}";
    // ISSUE: reference to a compiler-generated field
    if (((PXSelectBase) this.Matrix).Cache.Fields.Contains(cDisplayClass170.fieldName))
      return;
    // ISSUE: reference to a compiler-generated field
    ((PXSelectBase) this.Matrix).Cache.Fields.Add(cDisplayClass170.fieldName);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    this.Base.FieldSelecting.AddHandler(((PXSelectBase) this.Matrix).Cache.GetItemType(), cDisplayClass170.fieldName, new PXFieldSelecting((object) cDisplayClass170, __methodptr(\u003CAddField\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    this.Base.FieldUpdating.AddHandler(((PXSelectBase) this.Matrix).Cache.GetItemType(), cDisplayClass170.fieldName, new PXFieldUpdating((object) cDisplayClass170, __methodptr(\u003CAddField\u003Eb__1)));
  }

  protected abstract void FieldSelectingImpl(
    int attributeNumber,
    PXCache s,
    PXFieldSelectingEventArgs e,
    string fieldName);

  protected abstract void FieldUpdatingImpl(
    int attributeNumber,
    PXCache s,
    PXFieldUpdatingEventArgs e,
    string fieldName);

  protected virtual void AddPreliminaryField()
  {
    if (!this.AddPreliminary || ((PXSelectBase) this.Matrix).Cache.Fields.Contains("Preliminary"))
      return;
    ((PXSelectBase) this.Matrix).Cache.Fields.Add("Preliminary");
    // ISSUE: method pointer
    this.Base.FieldSelecting.AddHandler(((PXSelectBase) this.Matrix).Cache.GetItemType(), "Preliminary", new PXFieldSelecting((object) this, __methodptr(\u003CAddPreliminaryField\u003Eb__20_0)));
    // ISSUE: method pointer
    this.Base.FieldUpdating.AddHandler(((PXSelectBase) this.Matrix).Cache.GetItemType(), "Preliminary", new PXFieldUpdating((object) this, __methodptr(\u003CAddPreliminaryField\u003Eb__20_1)));
  }

  protected virtual void AddTotalField()
  {
    if (!this.AddTotals || ((PXSelectBase) this.Matrix).Cache.Fields.Contains("Extra"))
      return;
    ((PXSelectBase) this.Matrix).Cache.Fields.Add("Extra");
    // ISSUE: method pointer
    this.Base.FieldSelecting.AddHandler(((PXSelectBase) this.Matrix).Cache.GetItemType(), "Extra", new PXFieldSelecting((object) this, __methodptr(\u003CAddTotalField\u003Eb__21_0)));
  }

  protected virtual void PreliminaryFieldSelecting(
    PXCache s,
    PXFieldSelectingEventArgs e,
    string fieldName)
  {
  }

  protected virtual void PreliminaryFieldUpdating(
    PXCache s,
    PXFieldUpdatingEventArgs e,
    string fieldName)
  {
  }

  protected virtual void TotalFieldSelecting(
    PXCache s,
    PXFieldSelectingEventArgs e,
    string fieldName)
  {
  }

  protected virtual void _(
    Events.FieldUpdated<EntryHeader, EntryHeader.siteID> eventArgs)
  {
    this.RecalcMatrixGrid();
  }

  protected override void RecalcAttributesGrid()
  {
    base.RecalcAttributesGrid();
    this.RecalcMatrixGrid();
  }

  protected override void AttributeValueFieldUpdating(
    int attributeNumber,
    PXFieldUpdatingEventArgs e)
  {
    base.AttributeValueFieldUpdating(attributeNumber, e);
    this.RecalcMatrixGrid();
  }

  protected virtual void RecalcMatrixGrid()
  {
    int? selectedColumn = (int?) this.GetFirstMatrixRow()?.SelectedColumn;
    ((PXSelectBase) this.Matrix).Cache.Clear();
    if (((PXSelectBase<EntryHeader>) this.Header).Current.ColAttributeID == null || ((PXSelectBase<EntryHeader>) this.Header).Current.RowAttributeID == null)
      return;
    this.FillInventoryMatrix(this.GetInventoryMap(), selectedColumn);
  }

  /// <summary>
  /// Returns collection to map Inventory Item to row and column attributes values.
  /// Key contains: row attribute value, column attribute value.
  /// Value of dictionary contains: InventoryID or null if for those values inventory doesn't exist.
  /// Method uses MatrixHeader.Current (group id, column attribute, row attribute) to make result.
  /// </summary>
  protected virtual IDictionary<MatrixGridExt<Graph, MainItemType>.InventoryMapKey, MatrixGridExt<Graph, MainItemType>.InventoryMapValue> GetInventoryMap()
  {
    Dictionary<MatrixGridExt<Graph, MainItemType>.InventoryMapKey, MatrixGridExt<Graph, MainItemType>.InventoryMapValue> inventoryMap = new Dictionary<MatrixGridExt<Graph, MainItemType>.InventoryMapKey, MatrixGridExt<Graph, MainItemType>.InventoryMapValue>();
    if (!this.AllAdditionalAttributesArePopulated())
      return (IDictionary<MatrixGridExt<Graph, MainItemType>.InventoryMapKey, MatrixGridExt<Graph, MainItemType>.InventoryMapValue>) inventoryMap;
    int? inventoryID = new int?();
    string rowAttributeValue = (string) null;
    string columnAttributeValue = (string) null;
    string[] source = new string[((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.AttributeIdentifiers.Length];
    foreach (PXResult<CSAnswers, InventoryItem> inventoryWithAttribute in this.SelectInventoryWithAttributes())
    {
      InventoryItem inventoryItem1 = PXResult<CSAnswers, InventoryItem>.op_Implicit(inventoryWithAttribute);
      CSAnswers csAnswers = PXResult<CSAnswers, InventoryItem>.op_Implicit(inventoryWithAttribute);
      int? nullable = inventoryID;
      int? inventoryId = inventoryItem1.InventoryID;
      if (!(nullable.GetValueOrDefault() == inventoryId.GetValueOrDefault() & nullable.HasValue == inventoryId.HasValue))
      {
        inventoryID = inventoryItem1.InventoryID;
        rowAttributeValue = (string) null;
        columnAttributeValue = (string) null;
        for (int index = 0; index < source.Length; ++index)
          source[index] = (string) null;
      }
      if (string.Equals(csAnswers.AttributeID, ((PXSelectBase<EntryHeader>) this.Header).Current.RowAttributeID, StringComparison.OrdinalIgnoreCase))
        rowAttributeValue = csAnswers.Value;
      else if (string.Equals(csAnswers.AttributeID, ((PXSelectBase<EntryHeader>) this.Header).Current.ColAttributeID, StringComparison.OrdinalIgnoreCase))
      {
        columnAttributeValue = csAnswers.Value;
      }
      else
      {
        for (int index = 0; index < source.Length; ++index)
        {
          if (string.Equals(((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.AttributeIdentifiers[index], csAnswers.AttributeID, StringComparison.OrdinalIgnoreCase) && ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.Values[index] == csAnswers.Value)
          {
            source[index] = csAnswers.Value;
            break;
          }
        }
      }
      if (inventoryID.HasValue && rowAttributeValue != null && columnAttributeValue != null && ((IEnumerable<string>) source).All<string>((Func<string, bool>) (v => v != null)))
      {
        MatrixGridExt<Graph, MainItemType>.InventoryMapKey key = new MatrixGridExt<Graph, MainItemType>.InventoryMapKey(columnAttributeValue, rowAttributeValue);
        MatrixGridExt<Graph, MainItemType>.InventoryMapValue inventoryMapValue;
        if (inventoryMap.TryGetValue(key, out inventoryMapValue))
        {
          InventoryItem dirty = InventoryItem.PK.FindDirty((PXGraph) this.Base, this.GetTemplateID());
          InventoryItem inventoryItem2 = InventoryItem.PK.Find((PXGraph) this.Base, inventoryMapValue.InventoryID);
          throw new PXSetPropertyException("The {0} item has the same attribute values as the {1} item. {0} cannot be shown as a matrix item based on the {2} template.", new object[3]
          {
            (object) InventoryItem.PK.Find((PXGraph) this.Base, inventoryID).InventoryCD?.TrimEnd(),
            (object) inventoryItem2.InventoryCD?.TrimEnd(),
            (object) dirty.InventoryCD?.TrimEnd()
          });
        }
        inventoryMap.Add(key, this.CreateInventoryMapValue(inventoryID, inventoryWithAttribute));
        inventoryID = new int?();
      }
    }
    return (IDictionary<MatrixGridExt<Graph, MainItemType>.InventoryMapKey, MatrixGridExt<Graph, MainItemType>.InventoryMapValue>) inventoryMap;
  }

  protected virtual List<PXResult<CSAnswers, InventoryItem>> SelectInventoryWithAttributes()
  {
    PXSelectReadonly2<CSAnswers, InnerJoin<InventoryItem, On<CSAnswers.refNoteID, Equal<InventoryItem.noteID>>, LeftJoin<CSAttributeGroup, On<CSAnswers.attributeID, Equal<CSAttributeGroup.attributeID>, And<CSAttributeGroup.entityClassID, Equal<InventoryItem.itemClassID>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<InventoryItem>>, And<CSAttributeGroup.attributeCategory, Equal<CSAttributeGroup.attributeCategory.variant>>>>>>>, Where<InventoryItem.templateItemID, Equal<Required<InventoryItem.inventoryID>>, And<Where<CSAnswers.attributeID, Equal<MatrixAttributeSelectorAttribute.dummyAttributeName>, Or<CSAttributeGroup.isActive, Equal<True>>>>>, OrderBy<Asc<InventoryItem.inventoryID, Asc<CSAnswers.attributeID>>>> pxSelectReadonly2 = new PXSelectReadonly2<CSAnswers, InnerJoin<InventoryItem, On<CSAnswers.refNoteID, Equal<InventoryItem.noteID>>, LeftJoin<CSAttributeGroup, On<CSAnswers.attributeID, Equal<CSAttributeGroup.attributeID>, And<CSAttributeGroup.entityClassID, Equal<InventoryItem.itemClassID>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<InventoryItem>>, And<CSAttributeGroup.attributeCategory, Equal<CSAttributeGroup.attributeCategory.variant>>>>>>>, Where<InventoryItem.templateItemID, Equal<Required<InventoryItem.inventoryID>>, And<Where<CSAnswers.attributeID, Equal<MatrixAttributeSelectorAttribute.dummyAttributeName>, Or<CSAttributeGroup.isActive, Equal<True>>>>>, OrderBy<Asc<InventoryItem.inventoryID, Asc<CSAnswers.attributeID>>>>((PXGraph) this.Base);
    using (new PXFieldScope(((PXSelectBase) pxSelectReadonly2).View, new Type[3]
    {
      typeof (InventoryItem.inventoryID),
      typeof (CSAnswers.attributeID),
      typeof (CSAnswers.value)
    }))
      return ((IEnumerable<PXResult<CSAnswers>>) ((PXSelectBase<CSAnswers>) pxSelectReadonly2).Select(new object[1]
      {
        (object) this.GetTemplateID()
      })).AsEnumerable<PXResult<CSAnswers>>().Cast<PXResult<CSAnswers, InventoryItem>>().ToList<PXResult<CSAnswers, InventoryItem>>();
  }

  protected virtual int? GetTemplateID()
  {
    return ((PXSelectBase<EntryHeader>) this.Header).Current.TemplateItemID;
  }

  protected virtual MatrixGridExt<Graph, MainItemType>.InventoryMapValue CreateInventoryMapValue(
    int? inventoryID,
    PXResult<CSAnswers, InventoryItem> result)
  {
    return new MatrixGridExt<Graph, MainItemType>.InventoryMapValue()
    {
      InventoryID = inventoryID
    };
  }

  /// <summary>
  /// Inserts matrix rows by inventoryMap and result of GetQty method.
  /// </summary>
  protected virtual void FillInventoryMatrix(
    IDictionary<MatrixGridExt<Graph, MainItemType>.InventoryMapKey, MatrixGridExt<Graph, MainItemType>.InventoryMapValue> inventoryMap,
    int? selectedColumn)
  {
    MatrixGridExt<Graph, MainItemType>.MatrixAttributeValues matrixAttributeValues = this.GetMatrixAttributeValues();
    for (int index1 = 0; index1 < matrixAttributeValues.RowValues.Length; ++index1)
    {
      CSAttributeDetail rowValue = matrixAttributeValues.RowValues[index1];
      EntryMatrix newRow = new EntryMatrix();
      newRow.LineNbr = new int?(index1);
      newRow.RowAttributeValue = rowValue.ValueID;
      newRow.RowAttributeValueDescr = rowValue.Description ?? rowValue.ValueID;
      newRow.ColAttributeValues = new string[matrixAttributeValues.ColumnValues.Length];
      newRow.ColAttributeValueDescrs = new string[matrixAttributeValues.ColumnValues.Length];
      newRow.InventoryIDs = new int?[matrixAttributeValues.ColumnValues.Length];
      newRow.Quantities = new Decimal?[matrixAttributeValues.ColumnValues.Length];
      newRow.UOMs = new string[matrixAttributeValues.ColumnValues.Length];
      newRow.BaseQuantities = new Decimal?[matrixAttributeValues.ColumnValues.Length];
      newRow.Errors = new string[matrixAttributeValues.ColumnValues.Length];
      newRow.Selected = new bool?[matrixAttributeValues.ColumnValues.Length];
      newRow.SelectedColumn = selectedColumn;
      InventoryItem templateItem = this.GetTemplateItem();
      for (int index2 = 0; index2 < matrixAttributeValues.ColumnValues.Length; ++index2)
      {
        CSAttributeDetail columnValue = matrixAttributeValues.ColumnValues[index2];
        newRow.ColAttributeValues[index2] = columnValue.ValueID;
        newRow.ColAttributeValueDescrs[index2] = columnValue.Description;
        newRow.BaseUOM = templateItem.BaseUnit;
        MatrixGridExt<Graph, MainItemType>.InventoryMapValue inventoryValue;
        inventoryMap.TryGetValue(new MatrixGridExt<Graph, MainItemType>.InventoryMapKey(columnValue.ValueID, rowValue.ValueID), out inventoryValue);
        this.FillInventoryMatrixItem(newRow, index2, inventoryValue);
        if (index1 == 0)
          this.AddField(index2);
      }
      ((PXSelectBase) this.Matrix).Cache.SetStatus((object) newRow, (PXEntryStatus) 5);
    }
  }

  protected virtual MatrixGridExt<Graph, MainItemType>.MatrixAttributeValues GetMatrixAttributeValues()
  {
    PXSelect<CSAttributeDetail, Where<CSAttributeDetail.attributeID, Equal<Current<EntryHeader.colAttributeID>>>, OrderBy<Asc<CSAttributeDetail.sortOrder>>> pxSelect1 = new PXSelect<CSAttributeDetail, Where<CSAttributeDetail.attributeID, Equal<Current<EntryHeader.colAttributeID>>>, OrderBy<Asc<CSAttributeDetail.sortOrder>>>((PXGraph) this.Base);
    if (!this.ShowDisabledValue)
      ((PXSelectBase<CSAttributeDetail>) pxSelect1).WhereAnd<Where<CSAttributeDetail.disabled, Equal<False>>>();
    CSAttributeDetail[] csAttributeDetailArray1 = ((PXSelectBase<CSAttributeDetail>) pxSelect1).SelectMain(Array.Empty<object>());
    PXSelect<CSAttributeDetail, Where<CSAttributeDetail.attributeID, Equal<Current<EntryHeader.rowAttributeID>>>, OrderBy<Asc<CSAttributeDetail.sortOrder>>> pxSelect2 = new PXSelect<CSAttributeDetail, Where<CSAttributeDetail.attributeID, Equal<Current<EntryHeader.rowAttributeID>>>, OrderBy<Asc<CSAttributeDetail.sortOrder>>>((PXGraph) this.Base);
    if (!this.ShowDisabledValue)
      ((PXSelectBase<CSAttributeDetail>) pxSelect2).WhereAnd<Where<CSAttributeDetail.disabled, Equal<False>>>();
    CSAttributeDetail[] csAttributeDetailArray2 = ((PXSelectBase<CSAttributeDetail>) pxSelect2).SelectMain(Array.Empty<object>());
    return new MatrixGridExt<Graph, MainItemType>.MatrixAttributeValues()
    {
      ColumnValues = csAttributeDetailArray1,
      RowValues = csAttributeDetailArray2
    };
  }

  protected abstract void FillInventoryMatrixItem(
    EntryMatrix newRow,
    int colAttributeIndex,
    MatrixGridExt<Graph, MainItemType>.InventoryMapValue inventoryValue);

  protected virtual EntryMatrix GetFirstMatrixRow()
  {
    return ((PXSelectBase) this.Matrix).Cache.Cached.Cast<EntryMatrix>().FirstOrDefault<EntryMatrix>((Func<EntryMatrix, bool>) (r => !r.IsPreliminary.GetValueOrDefault() && !r.IsTotal.GetValueOrDefault()));
  }

  protected static TResult GetValueFromArray<TResult>(TResult[] array, int index)
  {
    if (index >= 0)
    {
      int num = index;
      int? length = array?.Length;
      int valueOrDefault = length.GetValueOrDefault();
      if (num < valueOrDefault & length.HasValue)
        return array[index];
    }
    return default (TResult);
  }

  [PXUIField]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable matrixGridCellChanged(PXAdapter adapter)
  {
    string str = adapter.CommandArguments?.TrimEnd();
    int result;
    if (string.IsNullOrEmpty(str) || !str.StartsWith("AttributeValue") || !int.TryParse(str.Substring("AttributeValue".Length), out result) || result < 0)
      result = -1;
    bool flag = false;
    foreach (EntryMatrix entryMatrix in ((PXSelectBase) this.Matrix).Cache.Cached)
    {
      int? selectedColumn = entryMatrix.SelectedColumn;
      int num = result;
      if (!(selectedColumn.GetValueOrDefault() == num & selectedColumn.HasValue))
      {
        flag = true;
        entryMatrix.SelectedColumn = new int?(result);
      }
    }
    if (flag)
      this.OnMatrixGridCellCahnged();
    return adapter.Get();
  }

  protected virtual void OnMatrixGridCellCahnged()
  {
  }

  public class InventoryMapKey(string columnAttributeValue, string rowAttributeValue) : 
    Tuple<string, string>(columnAttributeValue, rowAttributeValue)
  {
    public string ColumnAttributeValue => this.Item1;

    public string RowAttributeValue => this.Item2;
  }

  public class InventoryMapValue
  {
    public int? InventoryID { get; set; }

    public INSiteStatus SiteStatus { get; set; }

    public INLocationStatus LocationStatus { get; set; }
  }

  public class MatrixAttributeValues
  {
    public CSAttributeDetail[] ColumnValues { get; set; }

    public CSAttributeDetail[] RowValues { get; set; }
  }
}
