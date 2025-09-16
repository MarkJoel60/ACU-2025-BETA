// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSetupDefinition
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.AR;

public class ARSetupDefinition : IPrefetchable, IPXCompanyDependent
{
  public bool? PrintBeforeRelease { get; private set; }

  public bool? EmailBeforeRelease { get; private set; }

  public bool? IntegratedCCProcessing { get; private set; }

  public bool? HoldEntry { get; private set; }

  public bool? MigrationMode { get; private set; }

  void IPrefetchable.Prefetch()
  {
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<ARSetup>(new PXDataField[5]
    {
      new PXDataField("PrintBeforeRelease"),
      new PXDataField("EmailBeforeRelease"),
      new PXDataField("IntegratedCCProcessing"),
      new PXDataField("HoldEntry"),
      new PXDataField("MigrationMode")
    }))
    {
      this.PrintBeforeRelease = pxDataRecord != null ? pxDataRecord.GetBoolean(0) : new bool?(false);
      this.EmailBeforeRelease = pxDataRecord != null ? pxDataRecord.GetBoolean(1) : new bool?(false);
      this.IntegratedCCProcessing = pxDataRecord != null ? pxDataRecord.GetBoolean(2) : new bool?(false);
      this.HoldEntry = pxDataRecord != null ? pxDataRecord.GetBoolean(3) : new bool?(false);
      this.MigrationMode = pxDataRecord != null ? pxDataRecord.GetBoolean(4) : new bool?(false);
    }
  }

  public static ARSetupDefinition GetSlot()
  {
    return PXDatabase.GetSlot<ARSetupDefinition>(typeof (ARSetup).FullName, new Type[1]
    {
      typeof (ARSetup)
    });
  }
}
