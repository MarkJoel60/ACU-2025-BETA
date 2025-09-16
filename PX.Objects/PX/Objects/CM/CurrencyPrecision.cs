// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CurrencyPrecision
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CM;

public class CurrencyPrecision : PXDefaultAttribute
{
  protected readonly Type sourceCuryID;

  public CurrencyPrecision(short defaultValue, Type curyID)
    : base((object) defaultValue, CurrencyCollection.CombiteSearchPrecision(curyID))
  {
    this.sourceCuryID = curyID;
  }

  public CurrencyPrecision(Type curyID)
    : base(CurrencyCollection.CombiteSearchPrecision(curyID))
  {
    this.sourceCuryID = curyID;
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) (short?) CurrencyCollection.GetCurrency(sender, this.sourceCuryID, e.Row)?.DecimalPlaces;
    if (e.NewValue != null)
      return;
    base.FieldDefaulting(sender, e);
  }
}
