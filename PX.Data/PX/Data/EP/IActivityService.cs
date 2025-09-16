// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.IActivityService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.EP;

/// <exclude />
public interface IActivityService
{
  IEnumerable Select(object refNoteID, int? filterId = null);

  string GetKeys(object item);

  bool ShowTime(object item);

  System.DateTime? GetStartDate(object item);

  System.DateTime? GetEndDate(object item);

  void ShowAll(object refNoteID);

  void Cancel(string keys);

  void Complete(string keys);

  void Defer(string keys, int minuts);

  void Dismiss(string keys);

  void Open(string keys);

  bool IsViewed(object item);

  string GetImage(object item);

  string GetTitle(object item);

  string GetEmailAddressesForCc(PXGraph graph, Guid? refNoteID);

  int GetCount(object refNoteID);

  IEnumerable<ActivityService.Total> GetCounts();

  IEnumerable<ActivityService.IActivityType> GetActivityTypes();

  int GetActiveReminderCounts();

  void CreateTask(object refNoteID);

  void CreateEvent(object refNoteID);

  void CreateActivity(object refNoteID, string typeCode);

  void CreateEmailActivity(object refNoteID, int EmailAccountID);

  void OpenMailPopup(string link);
}
