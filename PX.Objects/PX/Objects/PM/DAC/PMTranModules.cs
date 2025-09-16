// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.DAC.PMTranModules
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.PM.DAC;

public static class PMTranModules
{
  public const string AllModules = "**";

  public class allModules : BqlType<IBqlString, string>.Constant<PMTranModules.allModules>
  {
    public allModules()
      : base("**")
    {
    }
  }

  public class StringListWithAllAttribute : PXStringListAttribute
  {
    public StringListWithAllAttribute()
      : base(new string[9]
      {
        "**",
        "AP",
        "AR",
        "CA",
        "DR",
        "GL",
        "IN",
        "PM",
        "PR"
      }, new string[9]
      {
        "All Sources",
        "AP",
        "AR",
        "CA",
        "DR",
        "GL",
        "IN",
        "PM",
        "PR"
      })
    {
    }
  }
}
