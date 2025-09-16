// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPViewStatusAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

public class EPViewStatusAttribute : PXIntListAttribute
{
  public const int NOTVIEWED = 0;
  public const int VIEWED = 1;

  public EPViewStatusAttribute()
    : base(new int[2]{ 0, 1 }, new string[2]
    {
      "Unread",
      "Read"
    })
  {
  }

  public sealed class NotViewed : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  EPViewStatusAttribute.NotViewed>
  {
    public NotViewed()
      : base(0)
    {
    }
  }

  public sealed class Viewed : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  EPViewStatusAttribute.Viewed>
  {
    public Viewed()
      : base(1)
    {
    }
  }
}
