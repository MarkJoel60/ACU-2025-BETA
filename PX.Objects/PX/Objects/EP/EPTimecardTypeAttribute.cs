// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPTimecardTypeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.EP;

public class EPTimecardTypeAttribute : 
  PXStringAttribute,
  IPXRowSelectingSubscriber,
  IPXFieldDefaultingSubscriber
{
  public const string Normal = "N";
  public const string Correction = "C";
  public const string NormalCorrected = "D";

  public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    string timecardType;
    using (new PXConnectionScope())
      timecardType = this.CalculateTimecardType(sender, (EPTimeCard) e.Row);
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) timecardType);
  }

  public void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this.CalculateTimecardType(sender, (EPTimeCard) e.Row);
  }

  protected virtual string CalculateTimecardType(PXCache sender, EPTimeCard row)
  {
    string timecardType = "N";
    if (row != null)
    {
      if (!string.IsNullOrEmpty(row.OrigTimeCardCD))
        timecardType = "C";
      if (row.IsReleased.GetValueOrDefault())
      {
        if (PXResultset<EPTimeCard>.op_Implicit(PXSelectBase<EPTimeCard, PXSelect<EPTimeCard, Where<EPTimeCard.origTimeCardCD, Equal<Required<EPTimeCard.origTimeCardCD>>>>.Config>.Select(sender.Graph, new object[1]
        {
          (object) row.TimeCardCD
        })) != null)
          timecardType = "D";
      }
    }
    return timecardType;
  }
}
