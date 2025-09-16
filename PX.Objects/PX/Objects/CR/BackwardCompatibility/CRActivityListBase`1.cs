// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BackwardCompatibility.CRActivityListBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR.BackwardCompatibility;

/// <exclude />
[Obsolete]
public abstract class CRActivityListBase<TActivity> : PXSelectBase<TActivity> where TActivity : CRPMTimeActivity, new()
{
  protected internal const string _WORKFLOW = "_Workflow";
  public const string _NEWTASK_COMMAND = "NewTask";
  public const string _NEWEVENT_COMMAND = "NewEvent";
  public const string _VIEWACTIVITY_COMMAND = "ViewActivity";
  public const string _VIEWALLACTIVITIES_COMMAND = "ViewAllActivities";
  public const string _NEWACTIVITY_COMMAND = "NewActivity";
  public const string _NEWMAILACTIVITY_COMMAND = "NewMailActivity";
  public const string _REGISTERACTIVITY_COMMAND = "RegisterActivity";
  public const string _OPENACTIVITYOWNER_COMMAND = "OpenActivityOwner";
  public const string _NEWTASK_WORKFLOW_COMMAND = "NewTask_Workflow";
  public const string _NEWEVENT_WORKFLOW_COMMAND = "NewEvent_Workflow";
  public const string _VIEWACTIVITY_WORKFLOW_COMMAND = "ViewActivity_Workflow";
  public const string _VIEWALLACTIVITIES_WORKFLOW_COMMAND = "ViewAllActivities_Workflow";
  public const string _NEWACTIVITY_WORKFLOW_COMMAND = "NewActivity_Workflow";
  public const string _NEWMAILACTIVITY_WORKFLOW_COMMAND = "NewMailActivity_Workflow";
  public const string _REGISTERACTIVITY_WORKFLOW_COMMAND = "RegisterActivity_Workflow";
  public const string _OPENACTIVITYOWNER_WORKFLOW_COMMAND = "OpenActivityOwner_Workflow";
  public const string _NEWACTIVITY_APPOINTMENT_WORKFLOW_COMMAND = "NewActivityE_Workflow";
  public const string _NEWACTIVITY_ESCALATION_WORKFLOW_COMMAND = "NewActivityES_Workflow";
  public const string _NEWACTIVITY_MESSAGE_WORKFLOW_COMMAND = "NewActivityM_Workflow";
  public const string _NEWACTIVITY_NOTE_WORKFLOW_COMMAND = "NewActivityN_Workflow";
  public const string _NEWACTIVITY_PHONECALL_WORKFLOW_COMMAND = "NewActivityP_Workflow";
  public const string _NEWACTIVITY_WORKITEM_WORKFLOW_COMMAND = "NewActivityW_Workflow";

  public static BqlCommand GenerateOriginalCommand()
  {
    return BqlCommand.CreateInstance(new System.Type[15]
    {
      typeof (Select2<,,,>),
      typeof (TActivity),
      typeof (LeftJoin<,>),
      typeof (CRReminder),
      typeof (On<,>),
      typeof (CRReminder.refNoteID),
      typeof (Equal<>),
      typeof (TActivity).GetNestedType(typeof (CRActivity.noteID).Name),
      typeof (Where<,>),
      typeof (TActivity).GetNestedType(typeof (CRActivity.classID).Name),
      typeof (GreaterEqual<>),
      typeof (Zero),
      typeof (OrderBy<>),
      typeof (Desc<>),
      typeof (TActivity).GetNestedType(typeof (CRActivity.createdDateTime).Name)
    });
  }
}
