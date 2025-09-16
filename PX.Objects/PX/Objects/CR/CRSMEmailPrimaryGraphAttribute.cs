// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRSMEmailPrimaryGraphAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

public sealed class CRSMEmailPrimaryGraphAttribute : PXPrimaryGraphBaseAttribute
{
  private static readonly System.Type[] _possibleGraphTypes = new System.Type[2]
  {
    typeof (CREmailActivityMaint),
    typeof (CRSMEmailMaint)
  };

  public virtual System.Type GetGraphType(
    PXCache cache,
    ref object row,
    bool checkRights,
    System.Type preferedType)
  {
    SMEmail smEmail = (SMEmail) row;
    Guid? noteId = smEmail.NoteID;
    Guid? refNoteId = smEmail.RefNoteID;
    if ((noteId.HasValue == refNoteId.HasValue ? (noteId.HasValue ? (noteId.GetValueOrDefault() != refNoteId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
    {
      CRSMEmail crsmEmail = PXResultset<CRSMEmail>.op_Implicit(PXSelectBase<CRSMEmail, PXSelect<CRSMEmail, Where<CRSMEmail.noteID, Equal<Required<CRSMEmail.noteID>>>>.Config>.Select(new PXGraph(), new object[1]
      {
        (object) smEmail.RefNoteID
      }));
      if (crsmEmail != null)
      {
        row = (object) crsmEmail;
        return typeof (CREmailActivityMaint);
      }
    }
    return typeof (CRSMEmailMaint);
  }

  private void OnAccessDenied(System.Type graphType)
  {
    throw new AccessViolationException(Messages.FormNoAccessRightsMessage(graphType));
  }

  public virtual IEnumerable<System.Type> GetAllGraphTypes()
  {
    return (IEnumerable<System.Type>) CRSMEmailPrimaryGraphAttribute._possibleGraphTypes;
  }
}
