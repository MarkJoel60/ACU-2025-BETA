// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APSetupDefinition
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;

#nullable disable
namespace PX.Objects.AP;

public class APSetupDefinition : IPrefetchable, IPXCompanyDependent
{
  public bool? MigrationMode { get; private set; }

  void IPrefetchable.Prefetch()
  {
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<APSetup>(new PXDataField("migrationMode")))
      this.MigrationMode = pxDataRecord != null ? pxDataRecord.GetBoolean(0) : new bool?(false);
  }

  public static APSetupDefinition GetSlot()
  {
    return PXDatabase.GetSlot<APSetupDefinition>(typeof (APSetup).FullName, typeof (APSetup));
  }
}
