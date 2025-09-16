// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.NonProjectBaseAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Selector Attribute that displays Non-Project only.
/// This Attribute should be used only if Contract Management and Project Accounting features are disabled.
/// </summary>
[PXDBInt]
[PXUIField]
public class NonProjectBaseAttribute : PXEntityAttribute
{
  public NonProjectBaseAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("PROJECT", typeof (Search2<PMProject.contractID, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PMProject.customerID>>>, Where<PMProject.nonProject, Equal<True>>>), typeof (PMProject.contractCD), new Type[6]
    {
      typeof (PMProject.contractCD),
      typeof (PMProject.description),
      typeof (PMProject.status),
      typeof (PMProject.customerID),
      typeof (PX.Objects.AR.Customer.acctName),
      typeof (PMProject.curyID)
    })
    {
      DescriptionField = typeof (PMProject.description),
      ValidComboRequired = true,
      CacheGlobal = true
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.Filterable = true;
  }
}
