// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ExternalCommitmentEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM.Extensions;
using System;

#nullable disable
namespace PX.Objects.PM;

[Serializable]
public class ExternalCommitmentEntry : PXGraph<ExternalCommitmentEntry, PMCommitment>, ICaptionable
{
  [PXViewName("Commitments")]
  public PXSelect<PMCommitment, Where<PMCommitment.type, Equal<PMCommitmentType.externalType>>> Commitments;

  public string Caption() => string.Empty;

  [PXDefault]
  [PXDBGuid(true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMCommitment.commitmentID> e)
  {
  }

  [PXDBString(1)]
  [PXDefault("E")]
  [PMCommitmentType.List]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMCommitment.type> e)
  {
  }

  [PXDefault]
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXUIField]
  [PXSelector(typeof (Search<PMCommitment.extRefNbr, Where<PMCommitment.type, Equal<PMCommitmentType.externalType>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMCommitment.extRefNbr> e)
  {
  }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.baseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMCommitment.inventoryID>>>>))]
  [PMUnit(typeof (PMCommitment.inventoryID))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMCommitment.uOM> e)
  {
  }

  [PXFormula(typeof (Selector<PMCommitment.projectID, PMProject.curyID>))]
  [PXString(5, IsUnicode = true)]
  [PXSelector(typeof (CurrencyList.curyID), DescriptionField = typeof (CurrencyList.description))]
  [PXUIField(DisplayName = "Project Currency", Enabled = false, FieldClass = "ProjectMultiCurrency")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMCommitment.projectCuryID> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMCommitment, PMCommitment.costCodeID> e)
  {
    if (CostCodeAttribute.UseCostCode())
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMCommitment, PMCommitment.costCodeID>, PMCommitment, object>) e).NewValue = (object) CostCodeAttribute.GetDefaultCostCode();
  }
}
