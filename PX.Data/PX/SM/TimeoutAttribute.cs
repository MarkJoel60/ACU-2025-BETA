// Decompiled with JetBrains decompiler
// Type: PX.SM.TimeoutAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

public class TimeoutAttribute : PXIntListAttribute
{
  public const int _1Minute = 60000;
  public const int _2Minutes = 120000;
  public const int _3Minutes = 180000;
  public const int _4Minutes = 240000;
  public const int _5Minutes = 300000;
  public const int _6Minutes = 360000;
  public const int _7Minutes = 420000;
  public const int _8Minutes = 480000;
  public const int _9Minutes = 540000;
  public const int _10Minutes = 600000;

  public TimeoutAttribute()
    : base(new int[10]
    {
      60000,
      120000,
      180000,
      240000,
      300000,
      360000,
      420000,
      480000,
      540000,
      600000
    }, new string[10]
    {
      "1 Minute",
      "2 Minutes",
      "3 Minutes",
      "4 Minutes",
      "5 Minutes",
      "6 Minutes",
      "7 Minutes",
      "8 Minutes",
      "9 Minutes",
      "10 Minutes"
    })
  {
  }
}
