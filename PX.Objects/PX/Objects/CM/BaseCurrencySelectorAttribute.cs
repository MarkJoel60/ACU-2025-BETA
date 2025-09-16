// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.BaseCurrencySelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CM;

public class BaseCurrencySelectorAttribute : PXSelectorAttribute
{
  public BaseCurrencySelectorAttribute()
    : base(typeof (Search<Currency.curyID, Where<Exists<Select<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.baseCuryID, Equal<Currency.curyID>>>>>>))
  {
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    if (!(e.ReturnState is PXFieldState returnState))
      return;
    returnState.Visible = PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
  }
}
