// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.IActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.EP;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

/// <summary>
/// Represents the graph extension that is used for Activities creation. Interface is used to
/// get the type of the corresponding activities in the Activities grid.
/// </summary>
public interface IActivityDetailsExt
{
  PXView ActivitiesView { get; }

  int? DefaultEmailAccountID { get; set; }

  string DefaultActivityType { get; }

  string DefaultSubject { get; set; }

  void AdjustActivitiesView();

  void AttachEvents();

  IList<System.Type> GetAllActivityTypes();

  IEnumerable newMailActivity(PXAdapter adapter);

  void CreateNewActivityAndRedirect(int classID, string activityType);

  PXGraph CreateNewActivity(int classID, string activityType);

  void CreatePrimaryActivity(PXGraph targetGraph, int classID, string activityType);

  void CreateTimeActivity(PXGraph targetGraph, int classID, string activityType);

  void InitializeActivity(CRActivity row);

  Guid? GetRefNoteID();

  void InitializeEmail(CRSMEmail row);

  string GetMailReply(CRSMEmail message, string currentMailReply);

  string GetMailTo(CRSMEmail message);

  string GetMailCc(CRSMEmail message, Guid? refNoteId);

  string GetSubject(CRSMEmail message);

  string GetBody();

  void SendNotification(
    string sourceType,
    string notifications,
    int? branchID,
    IDictionary<string, string> parameters,
    bool massProcess = false,
    IList<Guid?> attachments = null);

  NotificationGenerator CreateNotificationProvider(
    string sourceType,
    IList<string> notificationCDs,
    int? branchID,
    IDictionary<string, string> parameters,
    IList<Guid?> attachments = null);

  string GetPrimaryRecipientFromContext(
    NotificationUtility utility,
    string type,
    object row,
    NotificationSource source);

  System.Type GetActivityType();
}
