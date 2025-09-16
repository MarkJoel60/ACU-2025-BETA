// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Attributes.PXActiveCarrierSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CS.Attributes;

[PXDBString(15, IsUnicode = true)]
[PXRestrictor(typeof (Where<Carrier.isActive, Equal<True>>), null, new Type[] {})]
public class PXActiveCarrierSelectorAttribute : PXEntityAttribute
{
  public PXActiveCarrierSelectorAttribute(Type type)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(type));
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public PXActiveCarrierSelectorAttribute(Type SearchType, params Type[] fields)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(SearchType, fields));
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }
}
