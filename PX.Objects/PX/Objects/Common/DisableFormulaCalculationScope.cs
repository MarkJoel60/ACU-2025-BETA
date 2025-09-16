// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DisableFormulaCalculationScope
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common;

/// <summary>
/// Represents a scope used to shut down <see cref="T:PX.Data.PXFormulaAttribute" />
/// calculation (e.g. for performance reasons). For consistency, the
/// field values should be assigned manually within the scope.
/// </summary>
public class DisableFormulaCalculationScope(PXCache cache, params Type[] fields) : 
  OverrideAttributePropertyScope<PXFormulaAttribute, bool>(cache, (IEnumerable<Type>) fields, (Action<PXFormulaAttribute, bool>) ((formula, value) => formula.CancelCalculation = value), (Func<PXFormulaAttribute, bool>) (formula => formula.CancelCalculation), true)
{
  protected override void AssertAttributesCount(
    IEnumerable<PXFormulaAttribute> attributesOfType,
    string fieldName)
  {
  }
}
