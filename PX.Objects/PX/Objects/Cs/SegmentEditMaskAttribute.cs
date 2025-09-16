// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.SegmentEditMaskAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

public class SegmentEditMaskAttribute : PXStringListAttribute
{
  public SegmentEditMaskAttribute()
    : base("?;Alpha,9;Numeric,a;Alphanumeric,C;Unicode")
  {
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    if (!(e.ReturnState is PXStringState) || !(e.Row is Segment row) || row.ParentDimensionID == null)
      return;
    Segment segment = PXResultset<Segment>.op_Implicit(PXSelectBase<Segment, PXSelectReadonly<Segment, Where<Segment.dimensionID, Equal<Current<Segment.parentDimensionID>>, And<Segment.segmentID, Equal<Current<Segment.segmentID>>>>>.Config>.SelectSingleBound(sender.Graph, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (segment == null)
      return;
    PXStringState returnState = e.ReturnState as PXStringState;
    string[] allowedValues = returnState.AllowedValues;
    string[] allowedLabels = returnState.AllowedLabels;
    switch (segment.EditMask)
    {
      case "?":
        e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), ((PXEventSubscriberAttribute) this)._FieldName, new bool?(), new int?(-1), (string) null, new string[1]
        {
          allowedValues[0]
        }, new string[1]{ allowedLabels[0] }, new bool?(), (string) null, (string[]) null);
        break;
      case "9":
        e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), ((PXEventSubscriberAttribute) this)._FieldName, new bool?(), new int?(-1), (string) null, new string[1]
        {
          allowedValues[1]
        }, new string[1]{ allowedLabels[1] }, new bool?(), (string) null, (string[]) null);
        break;
      case "a":
        e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), ((PXEventSubscriberAttribute) this)._FieldName, new bool?(), new int?(-1), (string) null, new string[3]
        {
          allowedValues[0],
          allowedValues[1],
          allowedValues[2]
        }, new string[3]
        {
          allowedLabels[0],
          allowedLabels[1],
          allowedLabels[2]
        }, new bool?(), (string) null, (string[]) null);
        break;
    }
  }
}
