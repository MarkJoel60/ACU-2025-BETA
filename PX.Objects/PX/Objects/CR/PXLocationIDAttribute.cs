// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PXLocationIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

[Obsolete]
public class PXLocationIDAttribute : PXAggregateAttribute
{
  private string _DimensionName = "LOCATION";

  [Obsolete]
  public PXLocationIDAttribute(System.Type type, params System.Type[] fieldList)
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionAttribute(this._DimensionName)
    {
      ValidComboRequired = true
    });
    this._Attributes.Add((PXEventSubscriberAttribute) new PXNavigateSelectorAttribute(type, fieldList));
  }

  [Obsolete]
  public PXLocationIDAttribute(System.Type type)
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionAttribute(this._DimensionName)
    {
      ValidComboRequired = true
    });
    this._Attributes.Add((PXEventSubscriberAttribute) new PXNavigateSelectorAttribute(type));
  }
}
