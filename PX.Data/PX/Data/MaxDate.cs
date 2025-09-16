// Decompiled with JetBrains decompiler
// Type: PX.Data.MaxDate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data;

/// <summary>The maximum date: 06/06/2079.</summary>
public sealed class MaxDate : BqlType<IBqlDateTime, System.DateTime>.Constant<
#nullable disable
MaxDate>
{
  /// <exclude />
  public MaxDate()
    : base(new System.DateTime(2079, 6, 6))
  {
  }
}
