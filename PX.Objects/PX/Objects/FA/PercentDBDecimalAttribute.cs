// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.PercentDBDecimalAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FA;

public class PercentDBDecimalAttribute : PXDBDecimalAttribute
{
  protected Decimal _Factor = 100M;
  protected int? _RoundPrecision;
  protected PercentDBDecimalAttribute.Rounding _RoundType;

  public double Factor
  {
    get => (double) this._Factor;
    set => this._Factor = (Decimal) value;
  }

  public int RoundPrecision
  {
    get => this._RoundPrecision ?? 4;
    set => this._RoundPrecision = new int?(value);
  }

  public PercentDBDecimalAttribute.Rounding RoundType
  {
    get => this._RoundType;
    set => this._RoundType = value;
  }

  public PercentDBDecimalAttribute()
    : base(4)
  {
    this.MinValue = -99999.0;
    this.MaxValue = 99999.0;
  }

  public PercentDBDecimalAttribute(int precision)
    : base(precision)
  {
    this.MinValue = -99999.0;
    this.MaxValue = 99999.0;
  }

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    base.FieldUpdating(sender, e);
    if (e.NewValue == null)
      return;
    switch (this.RoundType)
    {
      case PercentDBDecimalAttribute.Rounding.Round:
        e.NewValue = (object) ((Decimal) e.NewValue / this._Factor);
        if (!this._RoundPrecision.HasValue)
          break;
        e.NewValue = (object) Math.Round((Decimal) e.NewValue, this._RoundPrecision.Value);
        break;
      case PercentDBDecimalAttribute.Rounding.Truncate:
        Decimal num = (Decimal) Math.Pow(10.0, (double) (this._RoundPrecision ?? 4));
        e.NewValue = (object) (Math.Truncate(num * (Decimal) e.NewValue / this._Factor) / num);
        break;
    }
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    if (e.ReturnValue == null || e.ReturnState is PXFieldState && !string.IsNullOrEmpty(((PXFieldState) e.ReturnState).Error))
      return;
    switch (this.RoundType)
    {
      case PercentDBDecimalAttribute.Rounding.Round:
        e.ReturnValue = (object) ((Decimal) e.ReturnValue * this._Factor);
        break;
      case PercentDBDecimalAttribute.Rounding.Truncate:
        Decimal num = (Decimal) Math.Pow(10.0, (double) (this._Precision ?? 4));
        e.ReturnValue = (object) (Math.Ceiling(num * (Decimal) e.ReturnValue * this._Factor) / num);
        break;
    }
  }

  public enum Rounding
  {
    Round,
    Truncate,
  }
}
