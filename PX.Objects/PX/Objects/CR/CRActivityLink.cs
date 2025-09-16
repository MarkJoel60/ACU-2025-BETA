// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRActivityLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <summary>
/// Lightweight projection of the <see cref="T:PX.Objects.CR.CRActivity" /> class that is used to fetch references to
/// entities and activities (the <see cref="P:PX.Objects.CR.CRActivityLink.NoteID" />, <see cref="P:PX.Objects.CR.CRActivityLink.RefNoteID" />, and <see cref="P:PX.Objects.CR.CRActivityLink.ParentNoteID" /> properties).
/// For instance, it is used to fetch <see cref="T:PX.Objects.CR.CRCase" /> only if the <see cref="P:PX.Objects.CR.CRActivityLink.NoteID" /> value of the linked activity is known.
/// This class is preserved for internal use only.
/// </summary>
[PXProjection(typeof (Select<CRActivity>))]
[PXHidden]
public class CRActivityLink : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="P:PX.Objects.CR.CRActivity.NoteID" />
  [PXDBGuid(false, IsKey = true, BqlField = typeof (CRActivity.noteID))]
  public virtual Guid? NoteID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.CRActivity.ParentNoteID" />
  [PXDBGuid(false, BqlField = typeof (CRActivity.parentNoteID))]
  public virtual Guid? ParentNoteID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.CRActivity.RefNoteID" />
  [PXDBGuid(false, BqlField = typeof (CRActivity.refNoteID))]
  public virtual Guid? RefNoteID { get; set; }

  public abstract class noteID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  CRActivityLink.noteID>
  {
  }

  public abstract class parentNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRActivityLink.parentNoteID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRActivityLink.refNoteID>
  {
  }
}
