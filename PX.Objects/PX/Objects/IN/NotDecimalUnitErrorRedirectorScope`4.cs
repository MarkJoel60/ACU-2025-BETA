// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.NotDecimalUnitErrorRedirectorScope`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.IN;

public class NotDecimalUnitErrorRedirectorScope<TDetail, TDetailQty, TMaster, TMasterQty> : 
  BaseNotDecimalUnitErrorRedirectorScope<TDetailQty>
  where TDetail : class, IBqlTable, new()
  where TDetailQty : IBqlField
  where TMaster : class, IBqlTable, new()
  where TMasterQty : IBqlField
{
  private Func<TDetail, TMaster> GetMasterRow;

  public NotDecimalUnitErrorRedirectorScope(
    PXCache masterCache,
    Func<TDetail, TMaster> getMasterRow)
    : base(masterCache)
  {
    this.GetMasterRow = getMasterRow ?? throw new PXArgumentException(nameof (getMasterRow));
  }

  protected override void SplitExceptionHandling(PXCache sender, PXExceptionHandlingEventArgs args)
  {
    if (!(args.Exception is PXNotDecimalUnitException exception))
      return;
    exception.IsLazyThrow = true;
    this.RedirectedSplits[args.Row] = exception;
    ((CancelEventArgs) args).Cancel = true;
  }

  protected override void HandleErrors()
  {
    PXNotDecimalUnitException decimalUnitException1 = (PXNotDecimalUnitException) null;
    PXNotDecimalUnitException decimalUnitException2 = (PXNotDecimalUnitException) null;
    foreach (KeyValuePair<object, PXNotDecimalUnitException> redirectedSplit in this.RedirectedSplits)
    {
      TDetail key = (TDetail) redirectedSplit.Key;
      PXNotDecimalUnitException decimalUnitException3 = redirectedSplit.Value;
      decimalUnitException1 = decimalUnitException1 ?? (decimalUnitException3.ErrorLevel >= 4 ? decimalUnitException3 : (PXNotDecimalUnitException) null);
      TMaster master = this.GetMasterRow(key);
      if ((object) master != null && string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<TMasterQty>(this.MasterCache, (object) master)))
      {
        decimalUnitException2 = decimalUnitException2 ?? (decimalUnitException3.ErrorLevel >= 4 ? decimalUnitException3 : (PXNotDecimalUnitException) null);
        this.MasterCache.RaiseExceptionHandling<TMasterQty>((object) master, this.MasterCache.GetValue<TMasterQty>((object) master), (Exception) decimalUnitException3);
      }
    }
    if ((decimalUnitException2 ?? decimalUnitException1) != null)
      throw decimalUnitException2 ?? decimalUnitException1;
  }
}
