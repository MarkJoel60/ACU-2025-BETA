// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectInventoryTransferFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CT;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PM;

[PXHidden]
[Serializable]
public class ProjectInventoryTransferFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Project(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>, And<BqlOperand<PMProject.nonProject, IBqlBool>.IsEqual<False>>>, And<BqlOperand<PMProject.isActive, IBqlBool>.IsEqual<True>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.accountingMode, Equal<ProjectAccountingModes.projectSpecific>>>>>.Or<BqlOperand<PMProject.accountingMode, IBqlString>.IsEqual<ProjectAccountingModes.valuated>>>>>))]
  public virtual int? ProjectID { get; set; }

  [ProjectTask(typeof (ProjectInventoryTransferFilter.projectID))]
  public virtual int? ProjectTaskID { get; set; }

  [StockItem]
  public virtual int? InventoryID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DisableProjectSelection { get; set; }

  public abstract class projectID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    ProjectInventoryTransferFilter.projectID>
  {
  }

  public abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ProjectInventoryTransferFilter.projectTaskID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ProjectInventoryTransferFilter.inventoryID>
  {
  }

  public abstract class disableProjectSelection : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ProjectInventoryTransferFilter.disableProjectSelection>
  {
  }
}
