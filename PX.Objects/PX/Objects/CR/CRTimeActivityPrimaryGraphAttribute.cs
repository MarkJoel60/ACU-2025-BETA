// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRTimeActivityPrimaryGraphAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP;
using System;

#nullable disable
namespace PX.Objects.CR;

public sealed class CRTimeActivityPrimaryGraphAttribute : CRCacheIndependentPrimaryGraphListAttribute
{
  public CRTimeActivityPrimaryGraphAttribute()
    : base(new System.Type[4]
    {
      typeof (CRActivityMaint),
      typeof (CREmailActivityMaint),
      typeof (CRTaskMaint),
      typeof (EPEventMaint)
    }, new System.Type[4]
    {
      typeof (Select<CRActivity, Where<CRActivity.noteID, Equal<Current<PMTimeActivity.refNoteID>>, And<CRActivity.classID, Equal<CRActivityClass.activity>>>>),
      typeof (Select<CRSMEmail, Where<CRSMEmail.noteID, Equal<Current<PMTimeActivity.refNoteID>>>>),
      typeof (Select<CRPMTimeActivity, Where<CRPMTimeActivity.noteID, Equal<Current<PMTimeActivity.refNoteID>>, And<CRPMTimeActivity.classID, Equal<CRActivityClass.task>>>>),
      typeof (Select<CRActivity, Where<CRActivity.noteID, Equal<Current<PMTimeActivity.refNoteID>>, And<CRActivity.classID, Equal<CRActivityClass.events>>>>)
    })
  {
  }

  protected override void OnAccessDenied(System.Type graphType)
  {
    throw new AccessViolationException(Messages.FormNoAccessRightsMessage(graphType));
  }
}
