// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.SubItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.IN;

[PXDBInt]
[PXUIField]
public class SubItemAttribute : PXEntityAttribute
{
  private const 
  #nullable disable
  string DefaultSubItemValue = "0";
  public const string DimensionName = "INSUBITEM";
  private bool _Disabled;

  public bool Disabled
  {
    get => this._Disabled;
    set => this._Disabled = value;
  }

  public bool ValidateValueOnFieldUpdating
  {
    get
    {
      return this.SelectorAttribute is SubItemAttribute.INSubItemDimensionSelector selectorAttribute && selectorAttribute.ValidateValueOnFieldUpdating;
    }
    set
    {
      if (!(this.SelectorAttribute is SubItemAttribute.INSubItemDimensionSelector selectorAttribute))
        return;
      selectorAttribute.ValidateValueOnFieldUpdating = value;
    }
  }

  public bool ValidateValueOnPersisting
  {
    get
    {
      return this.SelectorAttribute is SubItemAttribute.INSubItemDimensionSelector selectorAttribute && selectorAttribute.ValidateValueOnPersisting;
    }
    set
    {
      if (!(this.SelectorAttribute is SubItemAttribute.INSubItemDimensionSelector selectorAttribute))
        return;
      selectorAttribute.ValidateValueOnPersisting = value;
    }
  }

  public SubItemAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("INSUBITEM", typeof (Search<INSubItem.subItemID, Where<Match<Current<AccessInfo.userName>>>>), typeof (INSubItem.subItemCD))
    {
      CacheGlobal = true
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public SubItemAttribute(Type inventoryID)
    : this(inventoryID, (Type) null)
  {
  }

  public SubItemAttribute(Type inventoryID, Type JoinType)
  {
    Type type;
    if (!(JoinType == (Type) null))
      type = BqlCommand.Compose(new Type[6]
      {
        typeof (Search2<,,>),
        typeof (INSubItem.subItemID),
        JoinType,
        typeof (Where<>),
        typeof (Match<>),
        typeof (Current<AccessInfo.userName>)
      });
    else
      type = typeof (Search<INSubItem.subItemID, Where<Match<Current<AccessInfo.userName>>>>);
    Type search = type;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new SubItemAttribute.INSubItemDimensionSelector(inventoryID, search));
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    if (this._Disabled || !PXAccess.FeatureInstalled<FeaturesSet.subItem>())
    {
      ((PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1]).ValidComboRequired = false;
      ((PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1]).CacheGlobal = true;
      ((PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1]).SetSegmentDelegate((Delegate) null);
      PXGraph.FieldDefaultingEvents fieldDefaulting = sender.Graph.FieldDefaulting;
      Type itemType = sender.GetItemType();
      string fieldName = ((PXEventSubscriberAttribute) this)._FieldName;
      SubItemAttribute subItemAttribute = this;
      // ISSUE: virtual method pointer
      PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) subItemAttribute, __vmethodptr(subItemAttribute, FieldDefaulting));
      fieldDefaulting.AddHandler(itemType, fieldName, pxFieldDefaulting);
    }
    ((PXAggregateAttribute) this).CacheAttached(sender);
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (!this.Definitions.DefaultSubItemID.HasValue)
    {
      object obj = (object) "0";
      sender.RaiseFieldUpdating(((PXEventSubscriberAttribute) this)._FieldName, e.Row, ref obj);
      e.NewValue = obj;
    }
    else
      e.NewValue = (object) this.Definitions.DefaultSubItemID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual SubItemAttribute.Definition Definitions => SubItemAttribute.GetDefinition();

  private static SubItemAttribute.Definition GetDefinition()
  {
    SubItemAttribute.Definition definition = PXContext.GetSlot<SubItemAttribute.Definition>();
    if (definition == null)
      definition = PXContext.SetSlot<SubItemAttribute.Definition>(PXDatabase.GetSlot<SubItemAttribute.Definition>("INSubItem.Definition", new Type[1]
      {
        typeof (INSubItem)
      }));
    return definition;
  }

  public class dimensionName : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SubItemAttribute.dimensionName>
  {
    public dimensionName()
      : base("INSUBITEM")
    {
    }
  }

  private class INSubItemDimensionAttribute : PXDimensionAttribute
  {
    private Type _inventoryID;

    public INSubItemDimensionAttribute(Type inventoryID, string dimension)
      : base(dimension)
    {
      this._inventoryID = inventoryID;
    }

    protected virtual bool FindValueBySegmentDelegate(
      PXCache sender,
      object row,
      string segmentDescr,
      short segmentID,
      string val,
      string currentValue)
    {
      bool bySegmentDelegate = base.FindValueBySegmentDelegate(sender, row, segmentDescr, segmentID, val, currentValue);
      if (!bySegmentDelegate)
      {
        Dictionary<string, PXDimensionAttribute.ValueDescr> dictionary = this._Definition.Values[this._Dimension][(int) segmentID];
        if (dictionary.ContainsKey(currentValue))
        {
          int? inventoryID = (int?) sender.GetValue(row, this._inventoryID.Name);
          if (inventoryID.HasValue)
          {
            InventoryItem inventoryItem = InventoryItem.PK.Find(sender.Graph, inventoryID);
            if (val == "0" && inventoryItem != null && !inventoryItem.StkItem.GetValueOrDefault())
              return true;
            throw new PXSetPropertyException("The {0} value of the {1} segment of the {2} inventory item is inactive. Activate the value on the Stock Items (IN202500) form.", new object[3]
            {
              (object) (dictionary[currentValue].Descr ?? currentValue),
              (object) (segmentDescr ?? segmentID.ToString()),
              (object) (inventoryItem?.InventoryCD ?? inventoryID.ToString())
            });
          }
        }
      }
      return bySegmentDelegate;
    }
  }

  private class INSubItemDimensionSelector : PXDimensionSelectorAttribute
  {
    private readonly Type _inventoryID;
    private bool _ValidateValueOnFieldUpdating;

    public bool ValidateValueOnFieldUpdating
    {
      get => this._ValidateValueOnFieldUpdating;
      set
      {
        this.SetSegmentDelegate(value ? (Delegate) new PXSelectDelegate<short?>((object) this, __methodptr(DoSegmentSelect)) : (Delegate) null);
        this._ValidateValueOnFieldUpdating = value;
      }
    }

    public bool ValidateValueOnPersisting { get; set; }

    public INSubItemDimensionSelector(Type inventoryID, Type search)
      : base("INSUBITEM", search, typeof (INSubItem.subItemCD))
    {
      this._inventoryID = inventoryID;
      if (!(this._inventoryID != (Type) null))
        return;
      ((PXAggregateAttribute) this)._Attributes[((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).IndexOf((PXEventSubscriberAttribute) this.DimensionAttribute)] = (PXEventSubscriberAttribute) new SubItemAttribute.INSubItemDimensionAttribute(inventoryID, "INSUBITEM");
      this.CacheGlobal = false;
      this.ValidateValueOnFieldUpdating = true;
    }

    private IEnumerable DoSegmentSelect([PXShort] short? segment)
    {
      PXGraph currentGraph = PXView.CurrentGraph;
      if (!(this._inventoryID == (Type) null))
      {
        int? nullable = new int?();
        if (PXView.Currents != null)
        {
          foreach (object current in PXView.Currents)
          {
            if (current.GetType() == this._inventoryID.DeclaringType)
              nullable = (int?) currentGraph.Caches[this._inventoryID.DeclaringType].GetValue(current, this._inventoryID.Name);
          }
        }
        int startRow = PXView.StartRow;
        int num = 0;
        PXView pxView = new PXView(PXView.CurrentGraph, false, BqlCommand.CreateInstance(new Type[11]
        {
          typeof (Select2<,,>),
          typeof (INSubItemSegmentValue),
          typeof (InnerJoin<SegmentValue, On<SegmentValue.segmentID, Equal<INSubItemSegmentValue.segmentID>, And<SegmentValue.value, Equal<INSubItemSegmentValue.value>, And<SegmentValue.dimensionID, Equal<SubItemAttribute.dimensionName>>>>>),
          typeof (Where<,,>),
          typeof (INSubItemSegmentValue.segmentID),
          typeof (Equal<Required<SegmentValue.segmentID>>),
          typeof (And<,>),
          typeof (INSubItemSegmentValue.inventoryID),
          typeof (Equal<>),
          typeof (Optional<>),
          this._inventoryID
        }));
        object[] currents = PXView.Currents;
        object[] objArray = new object[2]
        {
          (object) segment,
          (object) nullable
        };
        object[] searches = PXView.Searches;
        string[] sortColumns = PXView.SortColumns;
        bool[] descendings = PXView.Descendings;
        PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
        ref int local1 = ref startRow;
        int maximumRows = PXView.MaximumRows;
        ref int local2 = ref num;
        foreach (PXResult<INSubItemSegmentValue, SegmentValue> pxResult in pxView.Select(currents, objArray, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2))
        {
          SegmentValue segmentValue = PXResult<INSubItemSegmentValue, SegmentValue>.op_Implicit(pxResult);
          yield return (object) new PXDimensionAttribute.SegmentValue()
          {
            Value = segmentValue.Value,
            Descr = segmentValue.Descr,
            IsConsolidatedValue = segmentValue.IsConsolidatedValue
          };
        }
        PXView.StartRow = 0;
      }
    }

    public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
    {
      base.RowPersisting(sender, e);
      if (!this.ValidateValueOnPersisting || e.Row == null)
        return;
      if (e.Operation != 2)
        return;
      try
      {
        object obj = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
        sender.RaiseFieldVerifying(((PXEventSubscriberAttribute) this)._FieldName, e.Row, ref obj);
      }
      catch (PXSetPropertyException ex)
      {
        object obj = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
        if (sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, obj, (Exception) ex))
          throw new PXRowPersistingException(((PXEventSubscriberAttribute) this)._FieldName, obj, ((Exception) ex).Message);
      }
    }
  }

  protected class Definition : IPrefetchable, IPXCompanyDependent
  {
    private int? _DefaultSubItemID;

    public int? DefaultSubItemID => this._DefaultSubItemID;

    public void Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<INSubItem>(new PXDataField[2]
      {
        (PXDataField) new PXDataField<INSubItem.subItemID>(),
        (PXDataField) new PXDataFieldOrder<INSubItem.subItemID>()
      }))
      {
        this._DefaultSubItemID = new int?();
        if (pxDataRecord == null)
          return;
        this._DefaultSubItemID = pxDataRecord.GetInt32(0);
      }
    }
  }

  public class defaultSubItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  SubItemAttribute.defaultSubItemID>
  {
    public defaultSubItemID()
      : base(-1)
    {
    }

    public virtual int Value
    {
      get
      {
        return SubItemAttribute.GetDefinition().DefaultSubItemID ?? ((BqlConstant<SubItemAttribute.defaultSubItemID, IBqlInt, int>) this).Value;
      }
    }
  }
}
