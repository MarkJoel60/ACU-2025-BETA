// Decompiled with JetBrains decompiler
// Type: PX.Data.SiteMapRootNodeSreenID
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data;

/// <summary>
/// Represents value of SiteMap root node Screen ID. It is "00000000".
/// </summary>
public sealed class SiteMapRootNodeSreenID : 
  BqlType<IBqlString, string>.Constant<
  #nullable disable
  SiteMapRootNodeSreenID>
{
  /// <exclude />
  public SiteMapRootNodeSreenID()
    : base("00000000")
  {
  }
}
