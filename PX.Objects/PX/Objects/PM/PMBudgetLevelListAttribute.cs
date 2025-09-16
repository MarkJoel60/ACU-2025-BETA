// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMBudgetLevelListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class PMBudgetLevelListAttribute : PXStringListAttribute
{
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    stringList1.Add("T");
    stringList2.Add(PXMessages.LocalizeNoPrefix("Task"));
    int num = CostCodeAttribute.UseCostCode() ? 1 : 0;
    if (num != 0)
    {
      stringList1.Add("C");
      stringList2.Add(PXMessages.LocalizeNoPrefix("Task and Cost Code"));
    }
    stringList1.Add("I");
    stringList2.Add(PXMessages.LocalizeNoPrefix("Task and Item"));
    if (num != 0)
    {
      stringList1.Add("D");
      stringList2.Add(PXMessages.LocalizeNoPrefix("Task, Item, and Cost Code"));
    }
    this._AllowedValues = stringList1.ToArray();
    this._AllowedLabels = stringList2.ToArray();
    base.FieldSelecting(sender, e);
  }
}
