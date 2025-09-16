// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.NotDecimalUnitErrorRedirectorScope`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

/// <summary>
/// Redirect an exception with type <see cref="T:PX.Objects.IN.PXNotDecimalUnitException" /> from  detail quantity field to master quantity field
/// </summary>
/// <typeparam name="TDetailQty">detail type quantity field</typeparam>
public class NotDecimalUnitErrorRedirectorScope<TDetailQty> : 
  BaseNotDecimalUnitErrorRedirectorScope<TDetailQty>
  where TDetailQty : IBqlField
{
  private readonly Type _masterQtyField;
  private readonly object _masterRow;

  public NotDecimalUnitErrorRedirectorScope(
    PXCache masterCache,
    object masterRow,
    Type masterQtyField)
    : base(masterCache)
  {
    this._masterQtyField = masterQtyField;
    this._masterRow = masterRow;
  }

  protected override void HandleErrors()
  {
    if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly(this.MasterCache, this._masterRow, this._masterQtyField.Name)))
      return;
    PXCache cach = this.MasterCache.Graph.Caches[this.DetailType];
    foreach (KeyValuePair<object, PXNotDecimalUnitException> redirectedSplit in this.RedirectedSplits)
    {
      if (cach.Cached.OfType<object>().Contains<object>(redirectedSplit.Key) && PXDBQuantityAttribute.VerifyForDecimal<TDetailQty>(cach, redirectedSplit.Key) != null)
      {
        this.MasterCache.RaiseExceptionHandling(this._masterQtyField.Name, this._masterRow, this.MasterCache.GetValue(this._masterRow, this._masterQtyField.Name), (Exception) redirectedSplit.Value);
        break;
      }
    }
  }
}
