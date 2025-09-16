// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INUnboundUnitAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

public class INUnboundUnitAttribute : INUnitAttribute
{
  public INUnboundUnitAttribute() => this.ReplaceDBField();

  public INUnboundUnitAttribute(Type baseUnitType)
    : base((Type) null, baseUnitType)
  {
    this.ReplaceDBField();
  }

  private void ReplaceDBField()
  {
    PXDBStringAttribute pxdbStringAttribute = ((IEnumerable) ((PXAggregateAttribute) this)._Attributes).OfType<PXDBStringAttribute>().FirstOrDefault<PXDBStringAttribute>();
    int index = pxdbStringAttribute != null ? ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).IndexOf((PXEventSubscriberAttribute) pxdbStringAttribute) : throw new PXArgumentException("dbStringAttribute");
    ((PXAggregateAttribute) this)._Attributes.RemoveAt(index);
    ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Insert(index, (PXEventSubscriberAttribute) new PXStringAttribute(pxdbStringAttribute.Length)
    {
      IsUnicode = pxdbStringAttribute.IsUnicode,
      InputMask = pxdbStringAttribute.InputMask
    });
  }
}
