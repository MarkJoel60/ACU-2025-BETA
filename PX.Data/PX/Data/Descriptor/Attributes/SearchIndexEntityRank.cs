// Decompiled with JetBrains decompiler
// Type: PX.Data.Descriptor.Attributes.SearchIndexEntityRank
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data.Descriptor.Attributes;

public class SearchIndexEntityRank : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(200, IsUnicode = true)]
  public virtual 
  #nullable disable
  string EntityType { get; set; }

  [PXDBShort]
  public virtual short? EntityRank { get; set; }

  /// <exclude />
  public abstract class entityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SearchIndexEntityRank.entityType>
  {
  }

  /// <exclude />
  public abstract class entityRank : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    SearchIndexEntityRank.entityRank>
  {
  }
}
