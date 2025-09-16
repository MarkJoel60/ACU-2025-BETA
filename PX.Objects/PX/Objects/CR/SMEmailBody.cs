// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.SMEmailBody
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
[PXProjection(typeof (Select<SMEmail>))]
[PXHidden]
public class SMEmailBody : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true, BqlField = typeof (SMEmail.noteID))]
  public virtual Guid? NoteID { get; set; }

  [PXDBGuid(false, BqlField = typeof (SMEmail.refNoteID))]
  [PXDBDefault(null)]
  [PXParent(typeof (Select<CRActivity, Where<CRActivity.noteID, Equal<Current<SMEmailBody.refNoteID>>>>), ParentCreate = true)]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBText(IsUnicode = true, BqlField = typeof (SMEmail.body))]
  [PXUIField(DisplayName = "Activity Details")]
  public virtual 
  #nullable disable
  string Body { get; set; }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMEmailBody.noteID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMEmailBody.refNoteID>
  {
  }

  public abstract class body : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMEmailBody.body>
  {
  }
}
