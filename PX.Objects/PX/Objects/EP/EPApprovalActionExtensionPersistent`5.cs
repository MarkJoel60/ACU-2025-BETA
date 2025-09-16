// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPApprovalActionExtensionPersistent`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.EP;
using System;

#nullable disable
namespace PX.Objects.EP;

public class EPApprovalActionExtensionPersistent<SourceAssign, Approved, Rejected, ApprovalMapID, ApprovalNotificationID> : 
  EPApprovalActionExtension<SourceAssign, Approved, Rejected, ApprovalMapID, ApprovalNotificationID>
  where SourceAssign : class, IAssign, IBqlTable, new()
  where Approved : class, IBqlField
  where Rejected : class, IBqlField
  where ApprovalMapID : class, IBqlField
  where ApprovalNotificationID : class, IBqlField
{
  protected override bool Persistent => true;

  public EPApprovalActionExtensionPersistent(PXGraph graph, Delegate @delegate)
    : base(graph, @delegate)
  {
  }

  public EPApprovalActionExtensionPersistent(PXGraph graph)
    : base(graph)
  {
  }
}
