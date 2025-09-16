// Decompiled with JetBrains decompiler
// Type: PX.SM.RelationHeader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System.Xml.Serialization;

#nullable enable
namespace PX.SM;

[PXCacheName("Relation Header")]
public class RelationHeader : RelationGroup
{
  protected 
  #nullable disable
  string _EntityTypeName;
  protected System.Type _EntityType;

  [PXString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Entity Type", TabOrder = 2)]
  [MaskedTypeSelector]
  public virtual string EntityTypeName
  {
    get => this._EntityTypeName;
    set => this._EntityTypeName = value;
  }

  [SoapIgnore]
  [PXUIField(Visibility = PXUIVisibility.Invisible, TabOrder = 3)]
  public virtual System.Type EntityType
  {
    get => this._EntityType;
    set => this._EntityType = value;
  }

  public new class PK : PrimaryKeyOf<RelationHeader>.By<RelationGroup.groupName>
  {
    public static RelationHeader Find(PXGraph graph, string groupName, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<RelationHeader>.By<RelationGroup.groupName>.FindBy(graph, (object) groupName, options);
    }
  }

  public new static class FK
  {
    public class SpecificType : 
      PrimaryKeyOf<MaskedType>.By<MaskedType.entityTypeName>.ForeignKeyOf<RelationGroup>.By<RelationGroup.specificType>
    {
    }

    public class EntityType : 
      PrimaryKeyOf<MaskedType>.By<MaskedType.entityTypeName>.ForeignKeyOf<RelationGroup>.By<RelationHeader.entityTypeName>
    {
    }

    public class SpecificModule : 
      PrimaryKeyOf<Graph>.By<Graph.graphName>.ForeignKeyOf<RelationGroup>.By<RelationGroup.specificModule>
    {
    }
  }

  public abstract class entityTypeName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RelationHeader.entityTypeName>
  {
  }

  public abstract class entityType : IBqlField, IBqlOperand
  {
  }
}
