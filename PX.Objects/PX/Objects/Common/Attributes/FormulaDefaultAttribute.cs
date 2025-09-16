// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.FormulaDefaultAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Attributes;

/// <summary>
/// A <see cref="!:PXDefault" />-like attribute supporting arbitrary
/// BQL formulas as the providers of the default value.
/// </summary>
public class FormulaDefaultAttribute : PXEventSubscriberAttribute, IPXFieldDefaultingSubscriber
{
  public virtual IBqlCreator Formula { get; protected set; }

  public FormulaDefaultAttribute(Type formulaType)
  {
    this.Formula = PXFormulaAttribute.InitFormula(formulaType);
  }

  public void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    object row = e.Row;
    if (e.Row == null || e.NewValue != null)
      return;
    bool? nullable = new bool?(false);
    object obj = (object) null;
    BqlFormula.Verify(sender, row, this.Formula, ref nullable, ref obj);
    if (obj == null || obj == PXCache.NotSetValue)
      return;
    e.NewValue = obj;
  }
}
