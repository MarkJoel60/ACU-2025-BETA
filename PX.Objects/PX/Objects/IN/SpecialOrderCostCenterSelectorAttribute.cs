// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.SpecialOrderCostCenterSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.IN;

public class SpecialOrderCostCenterSelectorAttribute : 
  PXSelectorAttribute,
  IPXFieldUpdatedSubscriber,
  IPXRowSelectingSubscriber,
  IPXRowSelectedSubscriber
{
  public Type InventoryIDField { get; protected set; }

  public Type SiteIDField { get; protected set; }

  public Type InvtMultField { get; protected set; }

  public Type CostCenterIDField { get; set; }

  public Type SOOrderTypeField { get; set; }

  public Type SOOrderNbrField { get; set; }

  public Type SOOrderLineNbrField { get; set; }

  public Type CostLayerTypeField { get; set; }

  public Type IsSpecialOrderField { get; set; }

  public Type OrigModuleField { get; set; }

  public Type ReleasedField { get; set; }

  public Type SelectorFromField { get; set; }

  public Type ProjectIDField { get; set; }

  public Type TaskIDField { get; set; }

  public Type CostCodeIDField { get; set; }

  public Type InventorySourceField { get; set; }

  public bool AllowEnabled { get; set; } = true;

  public bool CopyValueFromCostCenterID { get; set; }

  protected SpecialOrderCostCenterSelectorAttribute(Type selectType)
    : base(selectType)
  {
    this._FieldList = new string[5]
    {
      "SOOrderType",
      "SOOrderNbr",
      "SOLine__TranDesc",
      "SOLine__CuryUnitCost",
      "SOOrderLineNbr"
    };
    this.SubstituteKey = typeof (INCostCenter.costCenterCD);
  }

  public SpecialOrderCostCenterSelectorAttribute(Type inventoryIDField, Type siteIDField)
    : this(((IBqlTemplate) BqlTemplate.OfCommand<Search2<INCostCenter.costCenterID, InnerJoin<PX.Objects.SO.SOLine, On<INCostCenter.FK.OrderLine>>, Where<PX.Objects.SO.SOLine.inventoryID, Equal<Current2<BqlPlaceholder.A>>, And<INCostCenter.siteID, Equal<Current2<BqlPlaceholder.B>>>>>>.Replace<BqlPlaceholder.A>(inventoryIDField).Replace<BqlPlaceholder.B>(siteIDField)).ToType())
  {
    this.InventoryIDField = inventoryIDField;
    this.SiteIDField = siteIDField;
  }

  public SpecialOrderCostCenterSelectorAttribute(
    Type inventoryIDField,
    Type siteIDField,
    Type invtMultField)
    : this(((IBqlTemplate) BqlTemplate.OfCommand<Search2<INCostCenter.costCenterID, InnerJoin<PX.Objects.SO.SOLine, On<INCostCenter.FK.OrderLine>, InnerJoin<INSiteStatusByCostCenter, On<PX.Objects.SO.SOLine.inventoryID, Equal<INSiteStatusByCostCenter.inventoryID>, And<PX.Objects.SO.SOLine.subItemID, Equal<INSiteStatusByCostCenter.subItemID>, And<INCostCenter.siteID, Equal<INSiteStatusByCostCenter.siteID>, And<INCostCenter.costCenterID, Equal<INSiteStatusByCostCenter.costCenterID>>>>>>>, Where<PX.Objects.SO.SOLine.inventoryID, Equal<Current2<BqlPlaceholder.A>>, And<INCostCenter.siteID, Equal<Current2<BqlPlaceholder.B>>, And<Where<Current2<BqlPlaceholder.C>, GreaterEqual<int0>, Or<INSiteStatusByCostCenter.qtyOnHand, Greater<decimal0>>>>>>>>.Replace<BqlPlaceholder.A>(inventoryIDField).Replace<BqlPlaceholder.B>(siteIDField).Replace<BqlPlaceholder.C>(invtMultField)).ToType())
  {
    this.InventoryIDField = inventoryIDField;
    this.SiteIDField = siteIDField;
    this.InvtMultField = invtMultField;
  }

  public SpecialOrderCostCenterSelectorAttribute(
    Type inventoryIDField,
    Type toSiteIDField,
    Type invtMultField,
    Type selectorFromField)
    : this(((IBqlTemplate) BqlTemplate.OfCommand<Search2<INCostCenter.costCenterID, InnerJoin<PX.Objects.SO.SOLine, On<INCostCenter.FK.OrderLine>>, Where<INCostCenter.costCenterID, Equal<Current2<BqlPlaceholder.A>>>>>.Replace<BqlPlaceholder.A>(selectorFromField)).ToType())
  {
    this.InventoryIDField = inventoryIDField;
    this.SelectorFromField = selectorFromField;
    this.InvtMultField = invtMultField;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (this.AllowEnabled)
    {
      sender.Graph.FieldUpdated.AddHandler(BqlCommand.GetItemType(this.InventoryIDField), this.InventoryIDField.Name, ClearValue(this.InventoryIDField.Name));
      if (this.SiteIDField != (Type) null)
        sender.Graph.FieldUpdated.AddHandler(BqlCommand.GetItemType(this.SiteIDField), this.SiteIDField.Name, ClearValue(this.SiteIDField.Name));
    }
    if (this.SelectorFromField != (Type) null)
    {
      PXGraph.FieldUpdatedEvents fieldUpdated1 = sender.Graph.FieldUpdated;
      Type itemType1 = BqlCommand.GetItemType(this.SelectorFromField);
      string name1 = this.SelectorFromField.Name;
      SpecialOrderCostCenterSelectorAttribute selectorAttribute1 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) selectorAttribute1, __vmethodptr(selectorAttribute1, CopySpecialOrderNumber));
      fieldUpdated1.AddHandler(itemType1, name1, pxFieldUpdated1);
      PXGraph.FieldUpdatedEvents fieldUpdated2 = sender.Graph.FieldUpdated;
      Type itemType2 = BqlCommand.GetItemType(this.CostLayerTypeField);
      string name2 = this.CostLayerTypeField.Name;
      SpecialOrderCostCenterSelectorAttribute selectorAttribute2 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) selectorAttribute2, __vmethodptr(selectorAttribute2, CopySpecialOrderNumber));
      fieldUpdated2.AddHandler(itemType2, name2, pxFieldUpdated2);
    }
    if (!this.CopyValueFromCostCenterID)
      return;
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.AddHandler(BqlCommand.GetItemType(this.CostCenterIDField), this.CostCenterIDField.Name, new PXFieldUpdated((object) this, __methodptr(\u003CCacheAttached\u003Eb__76_1)));

    PXFieldUpdated ClearValue(string fieldName)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      return new PXFieldUpdated((object) new SpecialOrderCostCenterSelectorAttribute.\u003C\u003Ec__DisplayClass76_0()
      {
        \u003C\u003E4__this = this,
        fieldName = fieldName
      }, __methodptr(\u003CCacheAttached\u003Eb__2));
    }
  }

  public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    this.CopyValue(sender, e.Row);
  }

  protected virtual void CopyValue(PXCache sender, object row)
  {
    bool flag = false;
    if (this.CostLayerTypeField != (Type) null)
      flag = sender.GetValue(row, this.CostLayerTypeField.Name) as string == "S";
    else if (this.IsSpecialOrderField != (Type) null)
      flag = (sender.GetValue(row, this.IsSpecialOrderField.Name) as bool?).GetValueOrDefault();
    if (!flag || !(this.CostCenterIDField != (Type) null))
      return;
    object obj = sender.GetValue(row, this.CostCenterIDField.Name);
    sender.SetValue(row, ((PXEventSubscriberAttribute) this).FieldName, obj);
  }

  protected virtual void CopySpecialOrderNumber(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    string str = (string) cache.GetValue(e.Row, this.CostLayerTypeField.Name);
    int? nullable1 = (int?) cache.GetValue(e.Row, this.SelectorFromField.Name);
    int? nullable2 = (int?) cache.GetValue(e.Row, ((PXEventSubscriberAttribute) this).FieldName);
    if (!(str == "S"))
      return;
    int? nullable3 = nullable1;
    int? nullable4 = nullable2;
    if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
      return;
    cache.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this).FieldName, (object) nullable1);
  }

  public virtual void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    int? nullable1 = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this).FieldName) as int?;
    if (object.Equals((object) nullable1, e.OldValue))
      return;
    INCostCenter inCostCenter = INCostCenter.PK.Find(sender.Graph, nullable1);
    if (inCostCenter == null && nullable1.HasValue)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<INCostCenter>(sender.Graph), new object[1]
      {
        (object) nullable1
      });
    if (this.SOOrderTypeField != (Type) null)
      sender.SetValueExt(e.Row, this.SOOrderTypeField.Name, (object) inCostCenter?.SOOrderType);
    if (this.SOOrderNbrField != (Type) null)
      sender.SetValueExt(e.Row, this.SOOrderNbrField.Name, (object) inCostCenter?.SOOrderNbr);
    if (this.SOOrderLineNbrField != (Type) null)
      sender.SetValueExt(e.Row, this.SOOrderLineNbrField.Name, (object) (int?) inCostCenter?.SOOrderLineNbr);
    if (this.CostCenterIDField != (Type) null)
      sender.SetValueExt(e.Row, this.CostCenterIDField.Name, (object) nullable1);
    if (this.CostLayerTypeField != (Type) null)
      sender.SetValueExt(e.Row, this.CostLayerTypeField.Name, (object) inCostCenter?.CostLayerType);
    if (this.IsSpecialOrderField != (Type) null)
      sender.SetValueExt(e.Row, this.IsSpecialOrderField.Name, (object) nullable1.HasValue);
    PX.Objects.SO.SOLine soLine = PX.Objects.SO.SOLine.PK.Find(sender.Graph, inCostCenter?.SOOrderType, inCostCenter?.SOOrderNbr, (int?) inCostCenter?.SOOrderLineNbr);
    if (soLine == null && nullable1.HasValue)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.SO.SOLine>(sender.Graph), new object[3]
      {
        (object) inCostCenter?.SOOrderType,
        (object) inCostCenter?.SOOrderNbr,
        (object) (int?) inCostCenter?.SOOrderLineNbr
      });
    int? nullable2;
    if (this.ProjectIDField != (Type) null)
    {
      PXCache pxCache = sender;
      object row = e.Row;
      string name = this.ProjectIDField.Name;
      nullable2 = (int?) soLine?.ProjectID;
      // ISSUE: variable of a boxed type
      __Boxed<int?> local = (ValueType) (nullable2 ?? ProjectDefaultAttribute.NonProject());
      pxCache.SetValueExt(row, name, (object) local);
    }
    if (this.TaskIDField != (Type) null)
    {
      PXCache pxCache = sender;
      object row = e.Row;
      string name = this.TaskIDField.Name;
      int? nullable3;
      if (soLine == null)
      {
        nullable2 = new int?();
        nullable3 = nullable2;
      }
      else
        nullable3 = soLine.TaskID;
      // ISSUE: variable of a boxed type
      __Boxed<int?> local = (ValueType) nullable3;
      pxCache.SetValueExt(row, name, (object) local);
    }
    if (!(this.CostCodeIDField != (Type) null))
      return;
    PXCache pxCache1 = sender;
    object row1 = e.Row;
    string name1 = this.CostCodeIDField.Name;
    int? nullable4;
    if (soLine == null)
    {
      nullable2 = new int?();
      nullable4 = nullable2;
    }
    else
      nullable4 = soLine.CostCodeID;
    // ISSUE: variable of a boxed type
    __Boxed<int?> local1 = (ValueType) nullable4;
    pxCache1.SetValueExt(row1, name1, (object) local1);
  }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    bool flag = this.AllowEnabled;
    if (flag)
    {
      string str1 = sender.GetValue(e.Row, this.InventorySourceField.Name) as string;
      string str2 = sender.GetValue(e.Row, this.OrigModuleField.Name) as string;
      bool? nullable = sender.GetValue(e.Row, this.ReleasedField.Name) as bool?;
      flag = str1 == "S" && !nullable.GetValueOrDefault() && EnumerableExtensions.IsIn<string>(str2, "IN", "PI");
    }
    PXUIFieldAttribute.SetEnabled(sender, e.Row, ((PXEventSubscriberAttribute) this).FieldName, flag);
  }
}
