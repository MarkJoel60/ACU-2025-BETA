// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.AssetSummary
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA;

[TableAndChartDashboardType]
public class AssetSummary : PXGraph<AssetSummary>
{
  public PXFilter<AssetFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelectJoin<FixedAsset, LeftJoin<FADetails, On<FADetails.assetID, Equal<FixedAsset.assetID>>, LeftJoin<FALocationHistory, On<FALocationHistory.assetID, Equal<FADetails.assetID>, And<FALocationHistory.revisionID, Equal<FADetails.locationRevID>>>>>, Where<FixedAsset.recordType, Equal<FARecordType.assetType>, And<FixedAsset.status, NotEqual<FixedAssetStatus.reversed>>>> assets;
  public PXCancel<AssetFilter> Cancel;
  public PXFilter<DisposeParams> DispParams;
  public PXSetup<Company> company;
  public PXAction<AssetFilter> Dispose;

  public virtual BqlCommand GetSelectCommand(AssetFilter filter)
  {
    BqlCommand selectCommand = ((PXSelectBase) new PXSelectJoin<FixedAsset, InnerJoin<PX.Objects.GL.Branch, On<FixedAsset.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<FADetails, On<FADetails.assetID, Equal<FixedAsset.assetID>>, LeftJoin<FALocationHistory, On<FALocationHistory.assetID, Equal<FADetails.assetID>, And<FALocationHistory.revisionID, Equal<FADetails.locationRevID>>>>>>, Where<FixedAsset.recordType, Equal<FARecordType.assetType>, And<FixedAsset.status, NotEqual<FixedAssetStatus.reversed>>>>((PXGraph) this)).View.BqlSelect;
    if (filter.ClassID.HasValue)
      selectCommand = selectCommand.WhereAnd<Where<FixedAsset.classID, Equal<Current<AssetFilter.classID>>>>();
    if (filter.AssetTypeID != null)
      selectCommand = selectCommand.WhereAnd<Where<FixedAsset.assetTypeID, Equal<Current<AssetFilter.assetTypeID>>>>();
    if (filter.PropertyType != null)
      selectCommand = selectCommand.WhereAnd<Where<FADetails.propertyType, Equal<Current<AssetFilter.propertyType>>>>();
    if (filter.Condition != null)
      selectCommand = selectCommand.WhereAnd<Where<FADetails.condition, Equal<Current<AssetFilter.condition>>>>();
    if (!string.IsNullOrEmpty(filter.ReceiptType) && filter.ReceiptType != "AL")
      selectCommand = selectCommand.WhereAnd<Where<FADetails.receiptType, Equal<Current<AssetFilter.receiptType>>>>();
    if (filter.ReceiptNbr != null)
      selectCommand = selectCommand.WhereAnd<Where<FADetails.receiptNbr, Equal<Current<AssetFilter.receiptNbr>>>>();
    if (filter.PONumber != null)
      selectCommand = selectCommand.WhereAnd<Where<FADetails.pONumber, Equal<Current<AssetFilter.pONumber>>>>();
    if (filter.BillNumber != null)
      selectCommand = selectCommand.WhereAnd<Where<FADetails.billNumber, Equal<Current<AssetFilter.billNumber>>>>();
    if (filter.OrganizationID.HasValue)
      selectCommand = selectCommand.WhereAnd<Where<PX.Objects.GL.Branch.organizationID, Equal<Current2<AssetFilter.organizationID>>, And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>>();
    if (filter.BranchID.HasValue)
      selectCommand = selectCommand.WhereAnd<Where<FALocationHistory.locationID, Equal<Current<AssetFilter.branchID>>>>();
    if (filter.BuildingID.HasValue)
      selectCommand = selectCommand.WhereAnd<Where<FALocationHistory.buildingID, Equal<Current<AssetFilter.buildingID>>>>();
    if (filter.Floor != null)
      selectCommand = selectCommand.WhereAnd<Where<FALocationHistory.floor, Equal<Current<AssetFilter.floor>>>>();
    if (filter.Room != null)
      selectCommand = selectCommand.WhereAnd<Where<FALocationHistory.room, Equal<Current<AssetFilter.room>>>>();
    if (filter.EmployeeID.HasValue)
      selectCommand = selectCommand.WhereAnd<Where<FALocationHistory.employeeID, Equal<Current<AssetFilter.employeeID>>>>();
    if (filter.Department != null)
      selectCommand = selectCommand.WhereAnd<Where<FALocationHistory.department, Equal<Current<AssetFilter.department>>>>();
    if (filter.AcqDateFrom.HasValue)
      selectCommand = selectCommand.WhereAnd<Where<FADetails.depreciateFromDate, GreaterEqual<Current<AssetFilter.acqDateFrom>>>>();
    if (filter.AcqDateTo.HasValue)
      selectCommand = selectCommand.WhereAnd<Where<FADetails.depreciateFromDate, LessEqual<Current<AssetFilter.acqDateTo>>>>();
    return selectCommand;
  }

  protected virtual IEnumerable Assets()
  {
    AssetFilter current = ((PXSelectBase<AssetFilter>) this.Filter).Current;
    ((PXSelectBase) this.assets).Cache.AllowInsert = false;
    ((PXSelectBase) this.assets).Cache.AllowDelete = false;
    ((PXSelectBase) this.assets).Cache.AllowUpdate = false;
    BqlCommand selectCommand = this.GetSelectCommand(current);
    int startRow = PXView.StartRow;
    int num = 0;
    List<object> objectList = selectCommand.CreateView((PXGraph) this, mergeCache: true).Select(PXView.Currents, (object[]) null, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num);
    PXView.StartRow = 0;
    return (IEnumerable) objectList;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable dispose(PXAdapter adapter)
  {
    if (1 == ((PXSelectBase<DisposeParams>) this.DispParams).AskExt())
    {
      DisposeParams current = ((PXSelectBase<DisposeParams>) this.DispParams).Current;
      throw new NotImplementedException();
    }
    return adapter.Get();
  }
}
