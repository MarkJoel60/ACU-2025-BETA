// Decompiled with JetBrains decompiler
// Type: PX.Data.NoteDoc
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data;

/// <exclude />
[Serializable]
public class NoteDoc : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private Guid? _NoteID;
  private Guid? _FileID;
  private 
  #nullable disable
  string _EntityType;
  private string _EntityName;
  private string _EntityRowValues;

  [PXDBGuid(false, IsKey = true)]
  public Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBGuid(false, IsKey = true)]
  public Guid? FileID
  {
    get => this._FileID;
    set => this._FileID = value;
  }

  [PXString]
  public string EntityType
  {
    get => this._EntityType;
    set => this._EntityType = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Entity")]
  public string EntityName
  {
    get => this._EntityName;
    set => this._EntityName = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Row Values")]
  public string EntityRowValues
  {
    get => this._EntityRowValues;
    set => this._EntityRowValues = value;
  }

  /// <exclude />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  NoteDoc.noteID>
  {
  }

  /// <exclude />
  public abstract class fileID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  NoteDoc.fileID>
  {
  }

  /// <exclude />
  public abstract class entityType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NoteDoc.entityType>
  {
  }

  /// <exclude />
  public abstract class entityName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NoteDoc.entityName>
  {
  }

  /// <exclude />
  public abstract class entityRowValues : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NoteDoc.entityRowValues>
  {
  }
}
