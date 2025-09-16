// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Grouping
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class Grouping
{
  private IComparer<PMTran> comparer;

  public Grouping(IComparer<PMTran> comparer) => this.comparer = comparer;

  public virtual List<Group> BreakIntoGroups(List<PMTran> list)
  {
    list.Sort(this.comparer);
    List<Group> groupList = new List<Group>();
    for (int index = 0; index < list.Count; ++index)
    {
      if (index > 0)
      {
        if (this.comparer.Compare(list[index], list[index - 1]) == 0)
          groupList[groupList.Count - 1].List.Add(list[index]);
        else
          groupList.Add(new Group()
          {
            List = {
              list[index]
            }
          });
      }
      else
        groupList.Add(new Group() { List = { list[index] } });
    }
    return groupList;
  }
}
