// Decompiled with JetBrains decompiler
// Type: PX.SM.FeedBackSatisfactionAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public sealed class FeedBackSatisfactionAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string _1 = "1";
  public const string _2 = "2";
  public const string _3 = "3";
  public const string _4 = "4";
  public const string _5 = "5";
  public const string _6 = "6";
  public const string _7 = "7";
  public const string _8 = "8";
  public const string _9 = "9";
  public const string _10 = "10";

  public FeedBackSatisfactionAttribute()
    : base(new string[10]
    {
      "1",
      "2",
      "3",
      "4",
      "5",
      "6",
      "7",
      "8",
      "9",
      "10"
    }, new string[10]
    {
      "1 - Not at all Satisfied",
      "2",
      "3",
      "4",
      "5 - Neutral",
      "6",
      "7",
      "8",
      "9",
      "10 - Extremely Satisfied"
    })
  {
  }

  /// <exclude />
  public sealed class r1 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FeedBackSatisfactionAttribute.r1>
  {
    public r1()
      : base("1")
    {
    }
  }

  /// <exclude />
  public sealed class r2 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FeedBackSatisfactionAttribute.r2>
  {
    public r2()
      : base("2")
    {
    }
  }

  /// <exclude />
  public sealed class r3 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FeedBackSatisfactionAttribute.r3>
  {
    public r3()
      : base("3")
    {
    }
  }

  /// <exclude />
  public sealed class r4 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FeedBackSatisfactionAttribute.r4>
  {
    public r4()
      : base("4")
    {
    }
  }

  /// <exclude />
  public sealed class r5 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FeedBackSatisfactionAttribute.r5>
  {
    public r5()
      : base("5")
    {
    }
  }

  /// <exclude />
  public sealed class r6 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FeedBackSatisfactionAttribute.r6>
  {
    public r6()
      : base("6")
    {
    }
  }

  /// <exclude />
  public sealed class r7 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FeedBackSatisfactionAttribute.r7>
  {
    public r7()
      : base("7")
    {
    }
  }

  public sealed class r8 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FeedBackSatisfactionAttribute.r8>
  {
    public r8()
      : base("8")
    {
    }
  }

  public sealed class r9 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FeedBackSatisfactionAttribute.r9>
  {
    public r9()
      : base("9")
    {
    }
  }

  /// <exclude />
  public sealed class r10 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FeedBackSatisfactionAttribute.r10>
  {
    public r10()
      : base("10")
    {
    }
  }
}
