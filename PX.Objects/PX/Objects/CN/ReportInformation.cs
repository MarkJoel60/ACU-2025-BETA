// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ReportInformation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.CN;

[PXCacheName("Report Information")]
public class ReportInformation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [AnyInventory(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.IN.InventoryItem.stkItem, Equal<False>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<Where<PX.Objects.IN.InventoryItem.itemClassID, Equal<Optional<FilterItemByClass.itemClassID>>, Or<Optional<FilterItemByClass.itemClassID>, IsNull>>>>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr))]
  public virtual int? InventoryIdNonStock { get; set; }

  [PXInt]
  [PXSelector(typeof (Search<PMProject.contractID, Where<PMProject.status, In3<ProjectStatus.active, ReportInformation.planned>>>), new Type[] {typeof (PMProject.contractCD), typeof (PMProject.description), typeof (PMProject.status), typeof (PMProject.ownerID)}, SubstituteKey = typeof (PMProject.contractCD))]
  public virtual int? ProjectId { get; set; }

  [PXInt]
  [PXSelector(typeof (Search5<EPEmployee.bAccountID, LeftJoin<PMProject, On<PMProject.ownerID, Equal<EPEmployee.defContactID>>>, Where<PMProject.contractID, Equal<Optional<ReportInformation.projectId>>, Or<Optional<ReportInformation.projectId>, IsNull>>, Aggregate<GroupBy<EPEmployee.bAccountID>>>), SubstituteKey = typeof (EPEmployee.acctCD))]
  public virtual int? ProjectManagerId { get; set; }

  [Project(typeof (Where<PMProject.baseType, Equal<CTPRType.project>>))]
  public virtual int? BudgetForecastProjectId { get; set; }

  [PXString]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMForecast, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<PMForecast.projectID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMForecast.projectID, Equal<BqlField<ReportInformation.budgetForecastProjectId, IBqlInt>.AsOptional>>>>>.And<MatchUserFor<PMProject>>>.Order<By<BqlField<PMForecast.revisionID, IBqlString>.Desc>>, PMForecast>.SearchFor<PMForecast.revisionID>))]
  public virtual 
  #nullable disable
  string RevisionId { get; set; }

  public abstract class projectId : IBqlField, IBqlOperand
  {
  }

  public abstract class projectManagerId : IBqlField, IBqlOperand
  {
  }

  public abstract class budgetForecastProjectId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ReportInformation.budgetForecastProjectId>
  {
  }

  public abstract class revisionId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ReportInformation.revisionId>
  {
  }

  public class planned : BqlType<IBqlString, string>.Constant<ReportInformation.planned>
  {
    public planned()
      : base("D")
    {
    }
  }
}
