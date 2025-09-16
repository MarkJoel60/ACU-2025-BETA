// Decompiled with JetBrains decompiler
// Type: PX.SM.Alias.Note
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM.Alias;

/// <exclude />
[PXHidden]
[Serializable]
public class Note : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private Guid? _NoteID;
  private 
  #nullable disable
  string _EntityType;
  private string _GraphType;

  [PXDBGuid(false, IsKey = true)]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
  public Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBString]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
  public string EntityType
  {
    get => this._EntityType;
    set => this._EntityType = value;
  }

  [PXDBString]
  public string GraphType
  {
    get => this._GraphType;
    set => this._GraphType = value;
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Note.noteID>
  {
  }

  /// <exclude />
  public abstract class entityType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Note.entityType>
  {
  }

  /// <exclude />
  public abstract class graphType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Note.graphType>
  {
  }
}
