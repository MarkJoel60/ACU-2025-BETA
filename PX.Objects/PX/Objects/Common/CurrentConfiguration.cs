// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.CurrentConfiguration
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using System;

#nullable disable
namespace PX.Objects.Common;

internal class CurrentConfiguration
{
  /// <summary>
  /// Returns actual values of selected fields from APSetup table.
  /// </summary>
  public static CurrentConfiguration.APSetupCache ActualAPSetup
  {
    get
    {
      CurrentConfiguration.APSetupCache slot = PXContext.GetSlot<CurrentConfiguration.APSetupCache>();
      if (slot != null)
        return slot;
      return PXContext.SetSlot<CurrentConfiguration.APSetupCache>(PXDatabase.GetSlot<CurrentConfiguration.APSetupCache>(nameof (ActualAPSetup), new Type[1]
      {
        typeof (APSetup)
      }));
    }
  }

  /// <summary>
  /// Returns actual values of selected fields from APSetup table.
  /// </summary>
  public static CurrentConfiguration.ARSetupCache ActualARSetup
  {
    get
    {
      CurrentConfiguration.ARSetupCache slot = PXContext.GetSlot<CurrentConfiguration.ARSetupCache>();
      if (slot != null)
        return slot;
      return PXContext.SetSlot<CurrentConfiguration.ARSetupCache>(PXDatabase.GetSlot<CurrentConfiguration.ARSetupCache>(nameof (ActualARSetup), new Type[1]
      {
        typeof (ARSetup)
      }));
    }
  }

  public class APSetupCache : IPrefetchable, IPXCompanyDependent
  {
    public bool isMigrationModeEnabled { get; private set; }

    public string ApplyQuantityDiscountBy { get; private set; }

    public void Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<APSetup>(new PXDataField[2]
      {
        (PXDataField) new PXDataField<APSetup.migrationMode>(),
        (PXDataField) new PXDataField<APSetup.applyQuantityDiscountBy>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this.isMigrationModeEnabled = pxDataRecord.GetBoolean(0).Value;
        this.ApplyQuantityDiscountBy = pxDataRecord.GetString(1);
      }
    }
  }

  public class ARSetupCache : IPrefetchable, IPXCompanyDependent
  {
    public bool isMigrationModeEnabled { get; private set; }

    public string ApplyQuantityDiscountBy { get; private set; }

    public void Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<ARSetup>(new PXDataField[2]
      {
        (PXDataField) new PXDataField<ARSetup.migrationMode>(),
        (PXDataField) new PXDataField<APSetup.applyQuantityDiscountBy>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this.isMigrationModeEnabled = pxDataRecord.GetBoolean(0).Value;
        this.ApplyQuantityDiscountBy = pxDataRecord.GetString(1);
      }
    }
  }
}
