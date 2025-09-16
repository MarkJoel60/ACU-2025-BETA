// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CT;

/// <summary>Contract Item Selector. Displays all Contract Items.</summary>
[PXDBString(InputMask = "", IsUnicode = true)]
[PXUIField]
public class ContractItemAttribute : PXEntityAttribute
{
  public const string DimensionName = "CONTRACTITEM";

  public ContractItemAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("CONTRACTITEM", typeof (Search<ContractItem.contractItemCD>), typeof (ContractItem.contractItemCD), new Type[3]
    {
      typeof (ContractItem.contractItemCD),
      typeof (ContractItem.descr),
      typeof (ContractItem.baseItemID)
    })
    {
      DescriptionField = typeof (ContractItem.descr)
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public ContractItemAttribute(Type WhereType)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("CONTRACTITEM", BqlCommand.Compose(new Type[4]
    {
      typeof (Search<,,>),
      typeof (ContractItem.contractItemCD),
      WhereType,
      typeof (OrderBy<Desc<ContractItem.contractItemCD>>)
    }), typeof (ContractItem.contractItemCD), new Type[3]
    {
      typeof (ContractItem.contractItemCD),
      typeof (ContractItem.descr),
      typeof (ContractItem.baseItemID)
    })
    {
      DescriptionField = typeof (ContractItem.descr)
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }
}
