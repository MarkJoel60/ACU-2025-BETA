// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.OwnerSelectionListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data.EP;

/// <exclude />
public class OwnerSelectionListAttribute : PXIntListAttribute
{
  public const int Assigned = 0;
  public const int WorkGroup = 1;
  public const int Escalated = 2;

  public OwnerSelectionListAttribute()
    : base(new int[3]{ 0, 1, 2 }, new string[3]
    {
      "My",
      "My Workgroups",
      nameof (Escalated)
    })
  {
  }

  /// <exclude />
  public class assigned : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  OwnerSelectionListAttribute.assigned>
  {
    public assigned()
      : base(0)
    {
    }
  }

  /// <exclude />
  public class workGroup : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  OwnerSelectionListAttribute.workGroup>
  {
    public workGroup()
      : base(1)
    {
    }
  }

  /// <exclude />
  public class escalated : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  OwnerSelectionListAttribute.escalated>
  {
    public escalated()
      : base(2)
    {
    }
  }
}
