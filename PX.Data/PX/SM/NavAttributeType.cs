// Decompiled with JetBrains decompiler
// Type: PX.SM.NavAttributeType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class NavAttributeType : BqlType<IBqlString, string>.Constant<
#nullable disable
NavAttributeType>
{
  public NavAttributeType()
    : base("N")
  {
  }
}
