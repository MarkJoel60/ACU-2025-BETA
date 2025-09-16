// Decompiled with JetBrains decompiler
// Type: PX.SM.NotificationStatusAttribute
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
public class NotificationStatusAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string Draft = "N";
  public const string Published = "P";
  public const string Archived = "A";

  public NotificationStatusAttribute()
    : base(new string[3]{ "N", "P", "A" }, new string[3]
    {
      nameof (Draft),
      nameof (Published),
      nameof (Archived)
    })
  {
  }

  /// <exclude />
  public class draft : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  NotificationStatusAttribute.draft>
  {
    public draft()
      : base("N")
    {
    }
  }

  /// <exclude />
  public class published : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    NotificationStatusAttribute.published>
  {
    public published()
      : base("P")
    {
    }
  }

  /// <exclude />
  public class archived : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  NotificationStatusAttribute.archived>
  {
    public archived()
      : base("A")
    {
    }
  }
}
