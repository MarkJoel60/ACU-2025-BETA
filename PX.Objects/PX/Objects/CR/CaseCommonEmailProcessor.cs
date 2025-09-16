// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CaseCommonEmailProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CR.Workflows;
using PX.Objects.EP;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.CR;

public class CaseCommonEmailProcessor : BasicEmailProcessor
{
  protected override bool Process(BasicEmailProcessor.Package package)
  {
    EMailAccount account = package.Account;
    if (!account.IncomingProcessing.GetValueOrDefault())
      return false;
    CRSMEmail message = package.Message;
    if (!message.IsIncome.GetValueOrDefault() || !message.RefNoteID.HasValue)
      return false;
    PXGraph graph = package.Graph;
    PXSelectBase<CRCase, PXSelect<CRCase, Where<CRCase.noteID, Equal<Required<CRCase.noteID>>>>.Config>.Clear(graph);
    CRCase @case = PXResultset<CRCase>.op_Implicit(PXSelectBase<CRCase, PXSelect<CRCase, Where<CRCase.noteID, Equal<Required<CRCase.noteID>>>>.Config>.Select(graph, new object[1]
    {
      (object) message.RefNoteID
    }));
    if (@case == null || @case.CaseCD == null)
      return false;
    if (@case != null && !message.OwnerID.HasValue)
    {
      if (account.EmailAccountType == "S")
      {
        try
        {
          message.WorkgroupID = @case.WorkgroupID;
          graph.Caches[typeof (CRSMEmail)].SetValueExt<CRSMEmail.ownerID>((object) message, (object) @case.OwnerID);
        }
        catch (PXSetPropertyException ex)
        {
          message.OwnerID = new int?();
        }
      }
    }
    CRCaseClass caseClass = PXResultset<CRCaseClass>.op_Implicit(((PXSelectBase<CRCaseClass>) new PXSelect<CRCaseClass, Where<CRCaseClass.caseClassID, Equal<Required<CRCaseClass.caseClassID>>>>(graph)).SelectWindowed(0, 1, new object[1]
    {
      (object) @case.CaseClassID
    }));
    if (!RouterEmailProcessor.IsOwnerEqualUser(graph, message, @case.OwnerID) && !@case.Released.GetValueOrDefault())
    {
      if (!@case.IsActive.GetValueOrDefault())
      {
        CRCaseClass crCaseClass = caseClass;
        if ((crCaseClass != null ? (!crCaseClass.ReopenCaseTimeInDays.HasValue ? 1 : 0) : 1) == 0 && !CaseShouldBeReopenedByDate())
          goto label_14;
      }
      CRCaseMaint instance = PXGraph.CreateInstance<CRCaseMaint>();
      ((PXSelectBase<CRCase>) instance.Case).Current = ((PXSelectBase) instance.Case).Cache.CreateCopy((object) @case) as CRCase;
      ((PXAction) ((PXGraph) instance).GetExtension<CaseWorkflow>().openCaseFromProcessing).Press();
    }
label_14:
    return true;

    bool CaseShouldBeReopenedByDate()
    {
      DateTime? resolutionDate = @case.ResolutionDate;
      if (resolutionDate.HasValue)
      {
        DateTime valueOrDefault1 = resolutionDate.GetValueOrDefault();
        int? reopenCaseTimeInDays = (int?) caseClass?.ReopenCaseTimeInDays;
        if (reopenCaseTimeInDays.HasValue)
        {
          int valueOrDefault2 = reopenCaseTimeInDays.GetValueOrDefault();
          if (valueOrDefault2 > 0)
            return PXTimeZoneInfo.Now - valueOrDefault1 < TimeSpan.FromDays((double) valueOrDefault2);
        }
      }
      return false;
    }
  }
}
