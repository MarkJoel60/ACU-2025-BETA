// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.SC.Graphs.SubcontractSetupApproval
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.CN.Subcontracts.SC.Graphs;

public class SubcontractSetupApproval : IPrefetchable, IPXCompanyDependent
{
  private bool RequestApproval;

  public static bool IsActive
  {
    get
    {
      return PXDatabase.GetSlot<SubcontractSetupApproval>(nameof (SubcontractSetupApproval), new Type[2]
      {
        typeof (POSetup),
        typeof (POSetupApproval)
      }).RequestApproval;
    }
  }

  void IPrefetchable.Prefetch()
  {
    this.RequestApproval = false;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<POSetupApproval>(new PXDataField[2]
    {
      (PXDataField) new PXDataField<POSetupApproval.assignmentMapID>(),
      (PXDataField) new PXDataFieldValue<POSetupApproval.orderType>((object) "RS")
    }))
    {
      if (pxDataRecord == null)
        return;
      this.RequestApproval = pxDataRecord.GetInt32(0).HasValue;
    }
  }
}
