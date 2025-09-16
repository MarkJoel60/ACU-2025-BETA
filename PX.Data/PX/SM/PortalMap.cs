// Decompiled with JetBrains decompiler
// Type: PX.SM.PortalMap
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
[PXCacheName("Portal Map")]
[PXTableName]
[Serializable]
public class PortalMap : SiteMap
{
  public new class PK : PrimaryKeyOf<
  #nullable disable
  PortalMap>.By<PortalMap.nodeID>
  {
    public static PortalMap Find(PXGraph graph, Guid? nodeID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<PortalMap>.By<PortalMap.nodeID>.FindBy(graph, (object) nodeID, options);
    }
  }

  public new class UK : PrimaryKeyOf<PortalMap>.By<PortalMap.screenID>
  {
    public static PortalMap Find(PXGraph graph, string screenID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<PortalMap>.By<PortalMap.screenID>.FindBy(graph, (object) screenID, options);
    }
  }

  /// <exclude />
  public new abstract class nodeID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PortalMap.nodeID>
  {
  }

  /// <exclude />
  public new abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PortalMap.screenID>
  {
  }

  /// <exclude />
  public new abstract class parentID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PortalMap.parentID>
  {
  }

  /// <exclude />
  public new abstract class position : BqlType<
  #nullable enable
  IBqlDouble, double>.Field<
  #nullable disable
  PortalMap.position>
  {
  }
}
