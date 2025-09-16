// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.NonDecimalUnitsNoVerifyOnDropShipExt`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.IN;

#nullable disable
namespace PX.Objects.Extensions;

/// <summary>
/// Disabling of validation for decimal values for drop ship lines
/// </summary>
public abstract class NonDecimalUnitsNoVerifyOnDropShipExt<TGraph, TLine> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TLine : class, IBqlTable, new()
{
  protected abstract bool IsDropShipLine(TLine line);

  protected virtual void _(PX.Data.Events.RowSelected<TLine> e)
  {
    if ((object) e.Row == null)
      return;
    this.SetDecimalVerifyMode(e.Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<TLine> e)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(e.Operation.Command(), PXDBOperation.Insert, PXDBOperation.Update))
      return;
    this.SetDecimalVerifyMode(e.Cache, e.Row);
  }

  public virtual void SetDecimalVerifyMode(PXCache cache, TLine line)
  {
    DecimalVerifyMode decimalVerifyMode = this.GetLineVerifyMode(cache, line);
    cache.Adjust<PXDBQuantityAttribute>((object) line).ForAllFields((System.Action<PXDBQuantityAttribute>) (a => a.SetDecimalVerifyMode((object) line, decimalVerifyMode)));
  }

  protected virtual DecimalVerifyMode GetLineVerifyMode(PXCache cache, TLine line)
  {
    return !this.IsDropShipLine(line) ? DecimalVerifyMode.Error : DecimalVerifyMode.Off;
  }
}
