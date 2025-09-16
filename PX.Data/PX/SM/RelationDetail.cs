// Decompiled with JetBrains decompiler
// Type: PX.SM.RelationDetail
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.SM;

[PXCacheName("Relation Detail")]
public class RelationDetail : RelationGroup
{
  private bool? _Selected;

  [PXBool]
  [PXUIField(DisplayName = "Included")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  public new class PK : PrimaryKeyOf<
  #nullable disable
  RelationDetail>.By<RelationGroup.groupName>
  {
    public static RelationDetail Find(PXGraph graph, string groupName, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<RelationDetail>.By<RelationGroup.groupName>.FindBy(graph, (object) groupName, options);
    }
  }

  public new static class FK
  {
    public class SpecificType : 
      PrimaryKeyOf<MaskedType>.By<MaskedType.entityTypeName>.ForeignKeyOf<RelationDetail>.By<RelationGroup.specificType>
    {
    }

    public class SpecificModule : 
      PrimaryKeyOf<Graph>.By<Graph.graphName>.ForeignKeyOf<RelationDetail>.By<RelationGroup.specificModule>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RelationDetail.selected>
  {
  }
}
