// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.SuspendParams
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable enable
namespace PX.Objects.FA;

[Serializable]
public class SuspendParams : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CurrentPeriodID;

  [PXUIField(DisplayName = "Current Period")]
  [FASuspendPeriodSelector]
  [PXDefault(typeof (Search2<FABookPeriod.finPeriodID, InnerJoin<FABookBalance, On<FABookPeriod.bookID, Equal<FABookBalance.bookID>>, InnerJoin<FABook, On<FABookBalance.bookID, Equal<FABook.bookID>>, LeftJoin<FinPeriod, On<FABook.updateGL, Equal<True>, And<FABookPeriod.finPeriodID, Equal<FinPeriod.finPeriodID>, And<FABookPeriod.organizationID, Equal<FinPeriod.organizationID>>>>, InnerJoin<FixedAsset, On<FABookBalance.assetID, Equal<FixedAsset.assetID>>, InnerJoin<PX.Objects.GL.Branch, On<FixedAsset.branchID, Equal<PX.Objects.GL.Branch.branchID>>>>>>>, Where2<Where<FinPeriod.fAClosed, Equal<False>, Or<FinPeriod.fAClosed, IsNull>>, And<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>, And<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>, And<FABookPeriod.organizationID, Equal<IIf<Where<FABook.updateGL, Equal<True>, And<Not<FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>>>>, PX.Objects.GL.Branch.organizationID, FinPeriod.organizationID.masterValue>>, And<FABookPeriod.finPeriodID, GreaterEqual<IsNull<FABookBalance.currDeprPeriod, IsNull<FABookBalance.lastDeprPeriod, FABookBalance.deprFromPeriod>>>>>>>>, OrderBy<Desc<FABookBalance.updateGL, Asc<FABookPeriod.finPeriodID>>>>))]
  public virtual string CurrentPeriodID
  {
    get => this._CurrentPeriodID;
    set => this._CurrentPeriodID = value;
  }

  public abstract class currentPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SuspendParams.currentPeriodID>
  {
  }
}
