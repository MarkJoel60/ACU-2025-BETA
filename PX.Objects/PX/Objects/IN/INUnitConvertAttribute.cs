// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INUnitConvertAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

[PXDecimal(4)]
[PXUIField]
public class INUnitConvertAttribute : PXAggregateAttribute
{
  protected readonly Type BaseValue;
  protected readonly Type FromUOM;
  protected readonly Type ToUOM;

  protected PXDecimalAttribute DecimalAttribute => this.GetAttribute<PXDecimalAttribute>();

  protected PXUIFieldAttribute UIFieldAttribute => this.GetAttribute<PXUIFieldAttribute>();

  public string DisplayName
  {
    get => this.UIFieldAttribute?.DisplayName;
    set
    {
      if (this.UIFieldAttribute == null)
        return;
      this.UIFieldAttribute.DisplayName = value;
    }
  }

  public bool Enabled
  {
    get
    {
      PXUIFieldAttribute uiFieldAttribute = this.UIFieldAttribute;
      return uiFieldAttribute == null || uiFieldAttribute.Enabled;
    }
    set
    {
      if (this.UIFieldAttribute == null)
        return;
      this.UIFieldAttribute.Enabled = value;
    }
  }

  public INUnitConvertAttribute(Type baseValue, Type fromUOM, Type toUOM)
  {
    this.BaseValue = baseValue;
    this.FromUOM = fromUOM;
    this.ToUOM = toUOM;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.RowSelectingEvents rowSelecting = sender.Graph.RowSelecting;
    Type bqlTable1 = ((PXEventSubscriberAttribute) this).BqlTable;
    INUnitConvertAttribute convertAttribute1 = this;
    // ISSUE: virtual method pointer
    PXRowSelecting pxRowSelecting = new PXRowSelecting((object) convertAttribute1, __vmethodptr(convertAttribute1, RowSelecting));
    rowSelecting.AddHandler(bqlTable1, pxRowSelecting);
    PXGraph.FieldUpdatedEvents fieldUpdated1 = sender.Graph.FieldUpdated;
    Type bqlTable2 = ((PXEventSubscriberAttribute) this).BqlTable;
    string name1 = this.BaseValue.Name;
    INUnitConvertAttribute convertAttribute2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) convertAttribute2, __vmethodptr(convertAttribute2, RelatedValueUpdated));
    fieldUpdated1.AddHandler(bqlTable2, name1, pxFieldUpdated1);
    PXGraph.FieldUpdatedEvents fieldUpdated2 = sender.Graph.FieldUpdated;
    Type bqlTable3 = ((PXEventSubscriberAttribute) this).BqlTable;
    string name2 = this.FromUOM.Name;
    INUnitConvertAttribute convertAttribute3 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) convertAttribute3, __vmethodptr(convertAttribute3, RelatedValueUpdated));
    fieldUpdated2.AddHandler(bqlTable3, name2, pxFieldUpdated2);
    PXGraph.FieldUpdatedEvents fieldUpdated3 = sender.Graph.FieldUpdated;
    Type bqlTable4 = ((PXEventSubscriberAttribute) this).BqlTable;
    string name3 = this.ToUOM.Name;
    INUnitConvertAttribute convertAttribute4 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated3 = new PXFieldUpdated((object) convertAttribute4, __vmethodptr(convertAttribute4, RelatedValueUpdated));
    fieldUpdated3.AddHandler(bqlTable4, name3, pxFieldUpdated3);
  }

  protected virtual void RelatedValueUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.ReclaculateValue(sender, e.Row);
  }

  protected virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    Decimal? nullable = this.ConvertBaseValue(sender, e.Row, (Decimal?) sender.GetValue(e.Row, this.BaseValue.Name));
    if (!nullable.HasValue)
      return;
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this).FieldOrdinal, (object) nullable);
  }

  protected virtual Decimal? ConvertBaseValue(PXCache sender, object row, Decimal? baseValue)
  {
    if (!baseValue.HasValue)
      return new Decimal?();
    Decimal? nullable = baseValue;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return new Decimal?(0M);
    string str1 = (string) sender.GetValue(row, this.FromUOM.Name);
    string str2 = (string) sender.GetValue(row, this.ToUOM.Name);
    if (str1 == null || str2 == null)
    {
      nullable = new Decimal?();
      return nullable;
    }
    if (string.Equals(str1, str2, StringComparison.InvariantCultureIgnoreCase))
      return baseValue;
    bool ViceVersa = false;
    INUnit unit = INUnit.UK.ByGlobal.Find(sender.Graph, str1, str2);
    if (unit == null)
    {
      unit = INUnit.UK.ByGlobal.Find(sender.Graph, str2, str1);
      ViceVersa = true;
    }
    if (unit != null)
      return new Decimal?(INUnitAttribute.Convert(sender, unit, baseValue.GetValueOrDefault(), INPrecision.NOROUND, ViceVersa));
    nullable = new Decimal?();
    return nullable;
  }

  protected virtual void ReclaculateValue(PXCache cache, object row)
  {
    Decimal? objA = (Decimal?) cache.GetValue((object) cache, ((PXEventSubscriberAttribute) this).FieldOrdinal);
    Decimal? objB = this.ConvertBaseValue(cache, row, (Decimal?) cache.GetValue(row, this.BaseValue.Name));
    if (object.Equals((object) objA, (object) objB))
      return;
    cache.SetValueExt(row, ((PXEventSubscriberAttribute) this).FieldName, (object) objB);
  }
}
