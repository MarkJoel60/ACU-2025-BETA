// Decompiled with JetBrains decompiler
// Type: PX.CS.RMColumnCodeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.CS;

public class RMColumnCodeAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldDefaultingSubscriber,
  IPXRowDeletedSubscriber
{
  void IPXRowDeletedSubscriber.RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    RMColumn row1 = (RMColumn) e.Row;
    RMColumnSet row2 = PXParentAttribute.SelectParent<RMColumnSet>(sender, (object) row1);
    if (row1 == null || row1.ColumnCode == null || row2 == null || !(row2.LastColumn == row1.ColumnCode))
      return;
    row2.LastColumn = row2.LastColumn.Length != 3 || row2.LastColumn == "  A" ? "  @" : RMColumnCodeAttribute.ShiftCode(row2.LastColumn, false);
    sender.Graph.Caches[typeof (RMColumnSet)].MarkUpdated((object) row2);
  }

  void IPXFieldDefaultingSubscriber.FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    RMColumn row1 = (RMColumn) e.Row;
    RMColumnSet row2 = PXParentAttribute.SelectParent<RMColumnSet>(sender, (object) row1);
    if (row1 == null || row2 == null || row2.LastColumn == null)
      return;
    row2.LastColumn = row2.LastColumn.Length == 0 || row2.LastColumn == "  @" ? "  A" : RMColumnCodeAttribute.ShiftCode(row2.LastColumn, true);
    e.NewValue = (object) row2.LastColumn;
    sender.Graph.Caches[typeof (RMColumnSet)].MarkUpdated((object) row2);
  }

  public static string ShiftCode(string code, bool forward)
  {
    List<char> charList = new List<char>((IEnumerable<char>) (code ?? "").Trim().ToCharArray());
    for (int index = charList.Count - 1; index >= 0; --index)
    {
      if (forward)
      {
        if (charList[index] < 'Z')
        {
          charList[index]++;
          break;
        }
        charList[index] = 'A';
        if (index == 0)
        {
          charList.Insert(0, 'A');
          break;
        }
      }
      else
      {
        if (charList[index] > 'A')
        {
          charList[index]--;
          break;
        }
        charList[index] = 'Z';
        if (index == 0)
        {
          charList.RemoveAt(0);
          break;
        }
      }
    }
    while (charList.Count < 3)
      charList.Insert(0, ' ');
    return new string(charList.ToArray());
  }
}
