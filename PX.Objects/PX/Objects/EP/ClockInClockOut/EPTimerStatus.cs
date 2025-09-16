// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ClockInClockOut.EPTimerStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP.ClockInClockOut;

[PXInternalUseOnly]
public class EPTimerStatus
{
  public const 
  #nullable disable
  string Unavailable = "U";
  public const string Stopped = "S";
  public const string Paused = "P";
  public const string Running = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "U", "S", "P", "R" }, new string[4]
      {
        "U",
        "S",
        "P",
        "R"
      })
    {
    }
  }

  public class unavailable : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPTimerStatus.unavailable>
  {
    public unavailable()
      : base("U")
    {
    }
  }

  public class stopped : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPTimerStatus.stopped>
  {
    public stopped()
      : base("S")
    {
    }
  }

  public class paused : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPTimerStatus.paused>
  {
    public paused()
      : base("P")
    {
    }
  }

  public class running : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPTimerStatus.running>
  {
    public running()
      : base("R")
    {
    }
  }
}
