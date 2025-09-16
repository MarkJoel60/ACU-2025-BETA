// Decompiled with JetBrains decompiler
// Type: PX.SM.NotificationMajorStatusesAttribute
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
public class NotificationMajorStatusesAttribute : PXIntListAttribute
{
  public const int _DRAFT = 0;
  public const int _PUBLISHED = 1;

  public NotificationMajorStatusesAttribute()
    : base(new int[2]{ 0, 1 }, new string[2]
    {
      "Draft",
      "Published"
    })
  {
  }

  /// <exclude />
  public class published : 
    BqlType<IBqlInt, int>.Constant<
    #nullable disable
    NotificationMajorStatusesAttribute.published>
  {
    public published()
      : base(1)
    {
    }
  }
}
