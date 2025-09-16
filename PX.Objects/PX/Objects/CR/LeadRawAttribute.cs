// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.LeadRawAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

[Obsolete]
public class LeadRawAttribute : PXEntityAttribute
{
  public const string DimensionName = "LEAD";

  [Obsolete]
  public LeadRawAttribute()
  {
    System.Type type = typeof (Search<Contact.contactID, Where<Contact.contactType, Equal<ContactTypesAttribute.lead>>>);
    PXDimensionSelectorAttribute selectorAttribute;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) (selectorAttribute = new PXDimensionSelectorAttribute("LEAD", type, typeof (EPEmployee.acctCD))));
    selectorAttribute.DescriptionField = typeof (Contact.displayName);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.Filterable = true;
  }
}
