// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookBalanceNext
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXProjection(typeof (Select5<PX.Objects.FA.Standalone.FABookBalance, InnerJoin<FixedAsset, On<FixedAsset.assetID, Equal<PX.Objects.FA.Standalone.FABookBalance.assetID>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<FixedAsset.branchID>>, InnerJoin<FABook, On<PX.Objects.FA.Standalone.FABookBalance.bookID, Equal<FABook.bookID>>, InnerJoin<FABookPeriod, On<FABookPeriod.bookID, Equal<PX.Objects.FA.Standalone.FABookBalance.bookID>, And<FABookPeriod.organizationID, Equal<IIf<Where<FABook.updateGL, Equal<True>>, PX.Objects.GL.Branch.organizationID, FinPeriod.organizationID.masterValue>>>>, LeftJoin<OrganizationFinPeriod, On<OrganizationFinPeriod.finPeriodID, Equal<FABookPeriod.finPeriodID>, And<OrganizationFinPeriod.organizationID, Equal<PX.Objects.GL.Branch.organizationID>>>>>>>>, Where<FABookPeriod.finPeriodID, Greater<PX.Objects.FA.Standalone.FABookBalance.lastDeprPeriod>, And2<Where<FABook.updateGL, NotEqual<True>, Or<OrganizationFinPeriod.fAClosed, NotEqual<True>>>, And<PX.Objects.FA.Standalone.FABookBalance.assetID, Equal<CurrentValue<FixedAsset.assetID>>>>>, Aggregate<GroupBy<PX.Objects.FA.Standalone.FABookBalance.assetID, GroupBy<PX.Objects.FA.Standalone.FABookBalance.bookID, Min<FABookPeriod.finPeriodID>>>>>))]
[PXHidden]
[Serializable]
public class FABookBalanceNext : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AssetID;
  protected int? _BookID;
  protected 
  #nullable disable
  string _FinPeriodID;

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.FA.Standalone.FABookBalance.assetID))]
  [PXDefault]
  public virtual int? AssetID
  {
    get => this._AssetID;
    set => this._AssetID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.FA.Standalone.FABookBalance.bookID))]
  [PXDefault]
  public virtual int? BookID
  {
    get => this._BookID;
    set => this._BookID = value;
  }

  [FABookPeriodID(typeof (FABookBalanceNext.bookID), null, true, typeof (FABookBalanceNext.assetID), null, null, null, null, null, BqlField = typeof (FABookPeriod.finPeriodID))]
  [PXDefault]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookBalanceNext.assetID>
  {
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookBalanceNext.bookID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookBalanceNext.finPeriodID>
  {
  }
}
