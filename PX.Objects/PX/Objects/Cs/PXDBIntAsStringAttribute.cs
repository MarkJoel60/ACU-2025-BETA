// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.PXDBIntAsStringAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public class PXDBIntAsStringAttribute : PXDBIntAttribute, IPXFieldSelectingSubscriber
{
  protected int _Length = -1;
  protected string _InputMask = "";
  protected string _DisplayFormat = "";

  public int Length => this._Length;

  public string InputMask
  {
    get => this._InputMask;
    set => this._InputMask = value;
  }

  public string DisplayFormat
  {
    get => this._InputMask;
    set => this._InputMask = value;
  }

  protected bool DoConversion => this._Length > 0;

  public PXDBIntAsStringAttribute(int aLength)
  {
    this._Length = aLength;
    if (string.IsNullOrEmpty(this._InputMask))
      this._InputMask = this._InputMask.PadRight(this._Length, '#');
    if (!string.IsNullOrEmpty(this._DisplayFormat))
      return;
    this._DisplayFormat = "D" + this._Length.ToString();
  }

  public PXDBIntAsStringAttribute()
  {
  }

  void IPXFieldSelectingSubscriber.FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (((PXEventSubscriberAttribute) this)._AttributeLevel == 2 || e.IsAltered)
      e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(this._Length), new bool?(), ((PXEventSubscriberAttribute) this)._FieldName, new bool?(((PXDBFieldAttribute) this)._IsKey), new int?(), this._InputMask, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
    if (e.ReturnValue == null)
      return;
    e.ReturnValue = (object) ((int) e.ReturnValue).ToString(this._DisplayFormat);
  }
}
