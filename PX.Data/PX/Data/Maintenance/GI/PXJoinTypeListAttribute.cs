// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.PXJoinTypeListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Maintenance.GI;

public class PXJoinTypeListAttribute : PXStringListAttribute
{
  public static Dictionary<string, string> JoinTypes = new Dictionary<string, string>()
  {
    {
      "L",
      "Left"
    },
    {
      "R",
      "Right"
    },
    {
      "I",
      "Inner"
    },
    {
      "F",
      "Full"
    },
    {
      "C",
      "Cross"
    }
  };

  public PXJoinTypeListAttribute()
    : base(PXJoinTypeListAttribute.JoinTypes.Keys.ToArray<string>(), PXJoinTypeListAttribute.JoinTypes.Values.ToArray<string>())
  {
  }
}
