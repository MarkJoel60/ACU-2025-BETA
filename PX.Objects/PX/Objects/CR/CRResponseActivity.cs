// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRResponseActivity
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable disable
namespace PX.Objects.CR;

/// <summary>
/// An alias of <see cref="T:PX.Objects.CR.CRActivity" />
/// used to isolate the cache of a selector for the
/// <see cref="P:PX.Objects.CR.CRActivity.ResponseActivityNoteID">CRActivity.ResponseActivityNoteID</see> field
/// from the <see cref="T:PX.Objects.CR.CRActivity" /> class itself.
/// This class is preserved for internal use only.
/// </summary>
[PXHidden]
[PXBreakInheritance]
[PXTable(new System.Type[] {typeof (CRActivity.noteID)})]
public class CRResponseActivity : CRActivity
{
  public new class PK : PrimaryKeyOf<CRResponseActivity>.By<CRActivity.noteID>
  {
    public static CRResponseActivity Find(PXGraph graph, Guid noteID, PKFindOptions options = 0)
    {
      return (CRResponseActivity) PrimaryKeyOf<CRResponseActivity>.By<CRActivity.noteID>.FindBy(graph, (object) noteID, options);
    }
  }
}
