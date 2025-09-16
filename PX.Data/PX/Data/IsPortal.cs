// Decompiled with JetBrains decompiler
// Type: PX.Data.IsPortal
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.SP;

#nullable enable
namespace PX.Data;

/// <exclude />
public class IsPortal : BqlType<IBqlBool, bool>.Constant<
#nullable disable
IsPortal>
{
  public IsPortal()
    : base(PortalHelper.IsPortalContext())
  {
  }
}
