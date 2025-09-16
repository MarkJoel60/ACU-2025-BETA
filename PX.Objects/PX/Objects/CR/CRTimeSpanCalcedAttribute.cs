// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRTimeSpanCalcedAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class CRTimeSpanCalcedAttribute(System.Type operand) : PXDBCalcedAttribute(operand, typeof (int))
{
  public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    object obj = e.Record.GetValue(e.Position);
    if (obj != null)
      obj = Convert.ToInt32(obj) > 0 ? obj : (object) 0;
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) Convert.ToInt32(obj));
    ++e.Position;
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
  }
}
