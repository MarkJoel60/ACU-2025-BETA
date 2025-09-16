// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectSettingsManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CT;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class ProjectSettingsManager : IProjectSettingsManager
{
  private ProjectSettingsManager.DefaultProjectSettingsDefinition DefaultDefinition
  {
    get
    {
      return PXDatabase.GetSlot<ProjectSettingsManager.DefaultProjectSettingsDefinition>("DefaultProjectSettingsDefinition", ProjectSettingsManager.DefaultProjectSettingsDefinition.DependentTables);
    }
  }

  public int NonProjectID => this.DefaultDefinition.NonProjectID;

  public int EmptyInventoryID => this.DefaultDefinition.EmptyInventoryID;

  public string CostBudgetUpdateMode => this.DefaultDefinition.CostBudgetUpdateMode;

  public string RevenueBudgetUpdateMode => this.DefaultDefinition.RevenueBudgetUpdateMode;

  public bool CalculateProjectSpecificTaxes => this.DefaultDefinition.CalculateProjectSpecificTaxes;

  public bool CostCommitmentTracking => this.DefaultDefinition.CostCommitmentTracking;

  public bool IsPMVisible(string module) => this.DefaultDefinition.VisibleModules.Contains(module);

  private class DefaultProjectSettingsDefinition : IPrefetchable, IPXCompanyDependent
  {
    public const string SLOT_KEY = "DefaultProjectSettingsDefinition";

    public static Type[] DependentTables
    {
      get => new Type[1]{ typeof (PMSetup) };
    }

    public int NonProjectID { get; private set; }

    public int EmptyInventoryID { get; private set; }

    public string CostBudgetUpdateMode { get; private set; }

    public string RevenueBudgetUpdateMode { get; private set; }

    public bool CalculateProjectSpecificTaxes { get; private set; }

    public bool CostCommitmentTracking { get; private set; }

    public HashSet<string> VisibleModules { get; private set; }

    public DefaultProjectSettingsDefinition() => this.VisibleModules = new HashSet<string>();

    public void Prefetch()
    {
      string str = "<N/A>";
      this.CostBudgetUpdateMode = "D";
      this.RevenueBudgetUpdateMode = "S";
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PMSetup>(new PXDataField[15]
      {
        (PXDataField) new PXDataField<PMSetup.visibleInAP>(),
        (PXDataField) new PXDataField<PMSetup.visibleInAR>(),
        (PXDataField) new PXDataField<PMSetup.visibleInCA>(),
        (PXDataField) new PXDataField<PMSetup.visibleInCR>(),
        (PXDataField) new PXDataField<PMSetup.visibleInEA>(),
        (PXDataField) new PXDataField<PMSetup.visibleInGL>(),
        (PXDataField) new PXDataField<PMSetup.visibleInIN>(),
        (PXDataField) new PXDataField<PMSetup.visibleInPO>(),
        (PXDataField) new PXDataField<PMSetup.visibleInSO>(),
        (PXDataField) new PXDataField<PMSetup.visibleInTA>(),
        (PXDataField) new PXDataField<PMSetup.emptyItemCode>(),
        (PXDataField) new PXDataField<PMSetup.costBudgetUpdateMode>(),
        (PXDataField) new PXDataField<PMSetup.revenueBudgetUpdateMode>(),
        (PXDataField) new PXDataField<PMSetup.calculateProjectSpecificTaxes>(),
        (PXDataField) new PXDataField<PMSetup.costCommitmentTracking>()
      }))
      {
        bool? boolean = pxDataRecord.GetBoolean(0);
        if (boolean.GetValueOrDefault())
          this.VisibleModules.Add("AP");
        boolean = pxDataRecord.GetBoolean(1);
        if (boolean.GetValueOrDefault())
          this.VisibleModules.Add("AR");
        boolean = pxDataRecord.GetBoolean(2);
        if (boolean.GetValueOrDefault())
          this.VisibleModules.Add("CA");
        boolean = pxDataRecord.GetBoolean(3);
        if (boolean.GetValueOrDefault())
          this.VisibleModules.Add("CR");
        boolean = pxDataRecord.GetBoolean(4);
        if (boolean.GetValueOrDefault())
          this.VisibleModules.Add("EA");
        boolean = pxDataRecord.GetBoolean(5);
        if (boolean.GetValueOrDefault())
          this.VisibleModules.Add("GL");
        boolean = pxDataRecord.GetBoolean(6);
        if (boolean.GetValueOrDefault())
          this.VisibleModules.Add("IN");
        boolean = pxDataRecord.GetBoolean(7);
        if (boolean.GetValueOrDefault())
          this.VisibleModules.Add("PO");
        boolean = pxDataRecord.GetBoolean(8);
        if (boolean.GetValueOrDefault())
          this.VisibleModules.Add("SO");
        boolean = pxDataRecord.GetBoolean(9);
        if (boolean.GetValueOrDefault())
          this.VisibleModules.Add("TA");
        str = pxDataRecord.GetString(10);
        this.CostBudgetUpdateMode = pxDataRecord.GetString(11);
        this.RevenueBudgetUpdateMode = pxDataRecord.GetString(12);
        boolean = pxDataRecord.GetBoolean(13);
        this.CalculateProjectSpecificTaxes = boolean.GetValueOrDefault();
        boolean = pxDataRecord.GetBoolean(14);
        this.CostCommitmentTracking = boolean.GetValueOrDefault();
      }
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<Contract>(new PXDataField[2]
      {
        (PXDataField) new PXDataField<Contract.contractID>(),
        (PXDataField) new PXDataFieldValue<Contract.nonProject>((object) true, (PXComp) 0)
      }))
        this.NonProjectID = pxDataRecord.GetInt32(0).GetValueOrDefault();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PX.Objects.IN.InventoryItem>(new PXDataField[2]
      {
        (PXDataField) new PXDataField<PX.Objects.IN.InventoryItem.inventoryID>(),
        (PXDataField) new PXDataFieldValue<PX.Objects.IN.InventoryItem.inventoryCD>((object) str, (PXComp) 0)
      }))
        this.EmptyInventoryID = pxDataRecord.GetInt32(0).GetValueOrDefault();
    }
  }
}
