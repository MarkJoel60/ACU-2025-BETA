// Decompiled with JetBrains decompiler
// Type: PX.SM.PXUsersSourceListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

/// <exclude />
public class PXUsersSourceListAttribute : PXIntListAttribute
{
  public const int Application = 0;
  public const int ActiveDirectory = 1;
  public const int Claim = 2;

  public PXUsersSourceListAttribute()
    : base(new int[3]{ 0, 1, 2 }, new string[3]
    {
      "Native",
      "Active Directory",
      nameof (Claim)
    })
  {
  }

  /// <exclude />
  public sealed class application : 
    BqlType<IBqlInt, int>.Constant<
    #nullable disable
    PXUsersSourceListAttribute.application>
  {
    public application()
      : base(0)
    {
    }
  }

  /// <exclude />
  public sealed class activeDirectory : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    PXUsersSourceListAttribute.activeDirectory>
  {
    public activeDirectory()
      : base(1)
    {
    }
  }

  /// <exclude />
  public sealed class claim : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  PXUsersSourceListAttribute.claim>
  {
    public claim()
      : base(2)
    {
    }
  }
}
