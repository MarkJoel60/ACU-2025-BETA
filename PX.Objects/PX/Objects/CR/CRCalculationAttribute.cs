// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCalculationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

/// <summary>
/// Base attribute class for dinamic calculation values of DAC fields.
/// </summary>
[Obsolete]
public abstract class CRCalculationAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldSelectingSubscriber
{
  private readonly BqlCommand _select;

  [Obsolete]
  protected CRCalculationAttribute(System.Type valueSelect)
  {
    this._select = !(valueSelect == (System.Type) null) ? BqlCommand.CreateInstance(new System.Type[1]
    {
      valueSelect
    }) : throw new PXArgumentException(nameof (valueSelect), "The argument cannot be null.");
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    object obj = this.CalculateValue(sender.Graph.TypedViews.GetView(this._select, true), e.Row);
    sender.SetValue(e.Row, this._FieldName, obj);
    PXFieldState instance = PXFieldState.CreateInstance(e.ReturnState, (System.Type) null, new bool?(false), new bool?(), new int?(-1), new int?(), new int?(), obj, this._FieldName, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(false), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    instance.Value = obj;
    e.ReturnState = (object) instance;
  }

  protected abstract object CalculateValue(PXView view, object row);
}
