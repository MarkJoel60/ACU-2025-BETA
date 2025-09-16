// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Quarter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CR;

public class Quarter
{
  public class ListAttribute : PXIntListAttribute
  {
    public ListAttribute()
      : base(new int[4]{ 1, 2, 3, 4 }, new string[4]
      {
        PXMessages.LocalizeFormatNoPrefix("Quarter# {0}", new object[1]
        {
          (object) 1
        }),
        PXMessages.LocalizeFormatNoPrefix("Quarter# {0}", new object[1]
        {
          (object) 2
        }),
        PXMessages.LocalizeFormatNoPrefix("Quarter# {0}", new object[1]
        {
          (object) 3
        }),
        PXMessages.LocalizeFormatNoPrefix("Quarter# {0}", new object[1]
        {
          (object) 4
        })
      })
    {
    }
  }
}
