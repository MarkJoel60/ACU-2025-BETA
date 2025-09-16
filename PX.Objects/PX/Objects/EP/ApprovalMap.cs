// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ApprovalMap
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.EP;

public class ApprovalMap
{
  public readonly int ID;
  public readonly int? NotificationID;

  public ApprovalMap(int assignmentMapID, int? notificationID)
  {
    this.ID = assignmentMapID;
    this.NotificationID = notificationID;
  }
}
