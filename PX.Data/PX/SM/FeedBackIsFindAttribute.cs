// Decompiled with JetBrains decompiler
// Type: PX.SM.FeedBackIsFindAttribute
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
public sealed class FeedBackIsFindAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string _YES = "Y";
  public const string _NO = "N";
  public const string _UNSURE = "U";

  public FeedBackIsFindAttribute()
    : base(new string[3]{ "Y", "N", "U" }, new string[3]
    {
      "Yes",
      "No",
      "Unsure"
    })
  {
  }

  /// <exclude />
  public sealed class Yes : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FeedBackIsFindAttribute.Yes>
  {
    public Yes()
      : base("Y")
    {
    }
  }

  /// <exclude />
  public sealed class No : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FeedBackIsFindAttribute.No>
  {
    public No()
      : base("N")
    {
    }
  }

  /// <exclude />
  public sealed class Unsure : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FeedBackIsFindAttribute.Unsure>
  {
    public Unsure()
      : base("U")
    {
    }
  }
}
