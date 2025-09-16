// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.CostCodeManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

public class CostCodeManager : ICostCodeManager
{
  private CostCodeManager.DefaultCostCodeDefinition DefaultDefinition
  {
    get
    {
      return PXDatabase.GetSlot<CostCodeManager.DefaultCostCodeDefinition>("DefaultCostCodeDefinition", CostCodeManager.DefaultCostCodeDefinition.DependentTables);
    }
  }

  public int? DefaultCostCodeID => this.DefaultDefinition.DefaultCostCodeID;

  private class DefaultCostCodeDefinition : IPrefetchable, IPXCompanyDependent
  {
    public const string SLOT_KEY = "DefaultCostCodeDefinition";

    public static Type[] DependentTables
    {
      get => new Type[1]{ typeof (PMCostCode) };
    }

    public int? DefaultCostCodeID { get; private set; }

    public void Prefetch()
    {
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PMCostCode>(new PXDataField[2]
      {
        (PXDataField) new PXDataField<PMCostCode.costCodeID>(),
        (PXDataField) new PXDataFieldValue<PMCostCode.isDefault>((object) true, (PXComp) 0)
      }))
        this.DefaultCostCodeID = new int?(pxDataRecord.GetInt32(0).GetValueOrDefault());
    }
  }
}
