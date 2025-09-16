// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.DAC.Reports.PMRegister
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PM.DAC.Reports;

/// <summary>
/// Represents a batch of <see cref="T:PX.Objects.PM.PMTran"> project transactions</see>.
/// Light version of <see cref="T:PX.Objects.PM.PMRegister">PMRegister</see>.
/// Used in Substantiated Billing report (PM650000).
/// </summary>
[PXCacheName("Project Register")]
[ReportNotePrimaryGraph("OrigNoteID")]
public class PMRegister : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The identifier of the functional area, to which the batch belongs.
  /// </summary>
  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Source")]
  public virtual string? Module { get; set; }

  /// <summary>The reference number of the document.</summary>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Ref. Number")]
  public virtual string? RefNbr { get; set; }

  /// <summary>NoteID of the original document.</summary>
  [PXRefNote(LastKeyOnly = true)]
  [PXUIField(DisplayName = "Orig. Doc. Nbr.")]
  public virtual Guid? OrigNoteID { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public abstract class module : BqlType<IBqlString, string>.Field<PMRegister.module>
  {
  }

  public abstract class refNbr : BqlType<IBqlString, string>.Field<PMRegister.refNbr>
  {
  }

  public abstract class origNoteID : BqlType<IBqlGuid, Guid>.Field<PMRegister.origNoteID>
  {
  }

  public abstract class noteID : BqlType<IBqlGuid, Guid>.Field<PMRegister.noteID>
  {
  }
}
