// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRSummCalculationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

/// <summary>
/// Dinamicaly calculates field summ of rows returned by given Bql command.
/// </summary>
[Obsolete]
public class CRSummCalculationAttribute : CRCalculationAttribute
{
  private readonly System.Type _summField;

  [Obsolete]
  public CRSummCalculationAttribute(System.Type valueSelect, System.Type summField)
    : base(valueSelect)
  {
    this._summField = summField;
  }

  protected override object CalculateValue(PXView view, object row)
  {
    return view.Cache.GetValue(view.SelectSingle(Array.Empty<object>()), this._summField.Name);
  }
}
