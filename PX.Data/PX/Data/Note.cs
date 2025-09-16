// Decompiled with JetBrains decompiler
// Type: PX.Data.Note
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data;

/// <exclude />
[PXCacheName("Note")]
[Serializable]
public class Note : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private Guid? _NoteID;
  private 
  #nullable disable
  string _NoteText;
  private string _EntityType;
  private string _GraphType;
  private string _EntityName;

  [PXDBSequentialGuid(IsKey = true)]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
  public Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBString(IsUnicode = true)]
  [PXNoteText]
  [PXUIField(DisplayName = "NoteText")]
  public string NoteText
  {
    get => this._NoteText;
    set => this._NoteText = value;
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

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(Visible = false)]
  public string ExternalKey { get; set; }

  /// <exclude />
  [PXDBString(IsUnicode = true)]
  [PXNoteText]
  [PXUIField(DisplayName = "NoteText")]
  public string NotePopupText { get; set; }

  [PXString]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
  public string EntityName
  {
    get => this._EntityName;
    set => this._EntityName = value;
  }

  /// <exclude />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Note.noteID>
  {
  }

  /// <exclude />
  public abstract class noteText : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Note.noteText>
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

  /// <exclude />
  public abstract class externalKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Note.externalKey>
  {
  }

  public abstract class notePopupText : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Note.notePopupText>
  {
  }

  /// <exclude />
  public abstract class entityName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Note.entityName>
  {
  }
}
