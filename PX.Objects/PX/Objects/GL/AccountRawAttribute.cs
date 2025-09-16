// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountRawAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Base Attribute for AccountCD field. Aggregates PXFieldAttribute, PXUIFieldAttribute and PXDimensionAttribute.
/// PXDimensionAttribute selector has no restrictions and returns all records.
/// </summary>
[PXDBString(10, IsUnicode = true, InputMask = "")]
[PXUIField]
public sealed class AccountRawAttribute : PXEntityAttribute
{
  private string _DimensionName = "ACCOUNT";

  public AccountRawAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionAttribute(this._DimensionName)
    {
      ValidComboRequired = false
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }
}
