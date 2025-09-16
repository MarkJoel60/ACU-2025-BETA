// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.SalesPersonAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

/// <summary>
/// Provides UI selector of the Salespersons. <br />
/// Properties of the selector - mask, length of the key, etc.<br />
/// are defined in the Dimension with predefined name "SALESPER".<br />
/// As most Dimention Selector - substitutes SalesPersonID by SalesPersonCD.<br />
/// List of properties - inherited from PXEntityAttribute <br />
/// </summary>
[PXDBInt]
[PXUIField]
[PXRestrictor(typeof (Where<SalesPerson.isActive, Equal<True>>), "The sales person status is 'Inactive'.", new Type[] {})]
public class SalesPersonAttribute : PXEntityAttribute
{
  public const string DimensionName = "SALESPER";

  /// <summary>Default ctor. Shows all the salespersons</summary>
  public SalesPersonAttribute()
    : this(typeof (Where<True, Equal<True>>))
  {
  }

  /// <summary>
  /// Extended ctor. User can provide addtional where clause
  /// </summary>
  /// <param name="WhereType">Must be IBqlWhere type. Additional Where Clause</param>
  public SalesPersonAttribute(Type WhereType)
  {
    Type type1 = BqlCommand.Compose(new Type[3]
    {
      typeof (Search<,>),
      typeof (SalesPerson.salesPersonID),
      WhereType
    });
    PXAggregateAttribute.AggregatedAttributesCollection attributes = ((PXAggregateAttribute) this)._Attributes;
    Type type2 = type1;
    Type type3 = typeof (SalesPerson.salesPersonCD);
    Type[] typeArray = new Type[2]
    {
      typeof (SalesPerson.salesPersonCD),
      typeof (SalesPerson.descr)
    };
    PXDimensionSelectorAttribute selectorAttribute1;
    PXDimensionSelectorAttribute selectorAttribute2 = selectorAttribute1 = new PXDimensionSelectorAttribute("SALESPER", type2, type3, typeArray);
    attributes.Add((PXEventSubscriberAttribute) selectorAttribute1);
    selectorAttribute2.DescriptionField = typeof (SalesPerson.descr);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    selectorAttribute2.CacheGlobal = true;
  }

  /// <summary>
  /// Extended ctor, full form. User can provide addtional Where and Join clause.
  /// </summary>
  /// <param name="WhereType">Must be IBqlWhere type. Additional Where Clause</param>
  /// <param name="JoinType">Must be IBqlJoin type. Defines Join Clause</param>
  public SalesPersonAttribute(Type WhereType, Type JoinType)
  {
    Type type1 = BqlCommand.Compose(new Type[4]
    {
      typeof (Search2<,,>),
      typeof (SalesPerson.salesPersonID),
      JoinType,
      WhereType
    });
    PXAggregateAttribute.AggregatedAttributesCollection attributes = ((PXAggregateAttribute) this)._Attributes;
    Type type2 = type1;
    Type type3 = typeof (SalesPerson.salesPersonCD);
    Type[] typeArray = new Type[2]
    {
      typeof (SalesPerson.salesPersonCD),
      typeof (SalesPerson.descr)
    };
    PXDimensionSelectorAttribute selectorAttribute1;
    PXDimensionSelectorAttribute selectorAttribute2 = selectorAttribute1 = new PXDimensionSelectorAttribute("SALESPER", type2, type3, typeArray);
    attributes.Add((PXEventSubscriberAttribute) selectorAttribute1);
    selectorAttribute2.DescriptionField = typeof (SalesPerson.descr);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    selectorAttribute2.CacheGlobal = true;
  }

  public virtual bool DirtyRead
  {
    get
    {
      return this._SelAttrIndex != -1 && ((PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex]).DirtyRead;
    }
    set
    {
      if (this._SelAttrIndex == -1)
        return;
      ((PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex]).DirtyRead = value;
    }
  }
}
