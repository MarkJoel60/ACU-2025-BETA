// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.RelatedEntity
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.EP;

/// <exclude />
[Serializable]
public class RelatedEntity : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  [PXUIField(DisplayName = "Type", Required = true)]
  [PXEntityTypeList]
  [PXDefault]
  public virtual 
  #nullable disable
  string Type { get; set; }

  [PXUIField(DisplayName = "Entity", Required = true)]
  [PXEntityIDSelector(typeof (RelatedEntity.type))]
  [PXDBGuid(false)]
  [PXDefault]
  public virtual Guid? RefNoteID { get; set; }

  public virtual string EntityCD { get; set; }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedEntity.type>
  {
  }

  /// <exclude />
  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RelatedEntity.refNoteID>
  {
  }

  /// <exclude />
  public abstract class entityCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedEntity.entityCD>
  {
  }
}
