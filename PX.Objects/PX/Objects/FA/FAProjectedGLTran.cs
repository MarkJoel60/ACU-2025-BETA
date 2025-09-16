// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FAProjectedGLTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXProjection(typeof (SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Cross<APAROrd>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.released, Equal<True>>>>, And<BqlOperand<FATran.tranType, IBqlString>.IsNotEqual<FATran.tranType.reconcilliationPlus>>>>.And<BqlOperand<FATran.tranType, IBqlString>.IsNotEqual<FATran.tranType.reconcilliationMinus>>>), Persistent = false)]
[PXCacheName("FA transactions in GL representation")]
[Serializable]
public class FAProjectedGLTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (FATran.refNbr))]
  [PXUIField]
  [PXSelector(typeof (FARegister.refNbr))]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (FATran.lineNbr))]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(BqlTable = typeof (FATran))]
  [PXSelector(typeof (Search2<FixedAsset.assetID, LeftJoin<FADetails, On<FADetails.assetID, Equal<FixedAsset.assetID>>, LeftJoin<FALocationHistory, On<FALocationHistory.assetID, Equal<FADetails.assetID>, And<FALocationHistory.revisionID, Equal<FADetails.locationRevID>>>, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<FALocationHistory.locationID>>, LeftJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<FALocationHistory.employeeID>>>>>>, Where<FixedAsset.recordType, Equal<FARecordType.assetType>>>), new Type[] {typeof (FixedAsset.assetCD), typeof (FixedAsset.description), typeof (FixedAsset.classID), typeof (FixedAsset.usefulLife), typeof (FixedAsset.assetTypeID), typeof (FADetails.status), typeof (PX.Objects.GL.Branch.branchCD), typeof (EPEmployee.acctName), typeof (FALocationHistory.department)}, Filterable = true, SubstituteKey = typeof (FixedAsset.assetCD), DescriptionField = typeof (FixedAsset.description))]
  [PXUIField]
  public virtual int? AssetID { get; set; }

  [PXDBInt(BqlTable = typeof (FATran))]
  [PXSelector(typeof (Search2<FABook.bookID, InnerJoin<FABookBalance, On<FABookBalance.bookID, Equal<FABook.bookID>>>, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>>>), SubstituteKey = typeof (FABook.bookCode), DescriptionField = typeof (FABook.description))]
  [PXUIField]
  public virtual int? BookID { get; set; }

  [FABookPeriodID(typeof (FATran.bookID), null, true, typeof (FATran.assetID), null, null, null, null, null, BqlTable = typeof (FATran))]
  public virtual string FinPeriodID { get; set; }

  [Account(IsDBField = false)]
  [PXDBCalced(typeof (IIf<Where<BqlOperand<APAROrd.ord, IBqlShort>.IsEqual<short0>>, FATran.creditAccountID, FATran.debitAccountID>), typeof (int))]
  public virtual int? GLAccountID { get; set; }

  [SubAccount(typeof (FAProjectedGLTran.gLAccountID), IsDBField = false)]
  [PXDBCalced(typeof (IIf<Where<BqlOperand<APAROrd.ord, IBqlShort>.IsEqual<short0>>, FATran.creditSubID, FATran.debitSubID>), typeof (int))]
  public virtual int? GLSubID { get; set; }

  [Branch(null, null, true, true, true, IsDBField = false)]
  [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APAROrd.ord, Equal<short1>>>>>.And<BqlOperand<FATran.tranType, IBqlString>.IsEqual<FATran.tranType.transferDepreciation>>>, FATran.srcBranchID, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APAROrd.ord, Equal<short0>>>>>.And<BqlOperand<FATran.tranType, IBqlString>.IsEqual<FATran.tranType.transferPurchasing>>>, FATran.srcBranchID>>, FATran.branchID>), typeof (int))]
  public virtual int? GLBranchID { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APAROrd.ord, Equal<short1>>>>, And<BqlOperand<FATran.tranType, IBqlString>.IsEqual<FATran.tranType.purchasingMinus>>>, And<BqlOperand<FATran.debitAccountID, IBqlInt>.IsEqual<FATran.creditAccountID>>>, And<BqlOperand<FATran.debitSubID, IBqlInt>.IsEqual<FATran.creditSubID>>>, Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APAROrd.ord, Equal<short0>>>>, And<BqlOperand<FATran.tranType, IBqlString>.IsEqual<FATran.tranType.purchasingPlus>>>, And<BqlOperand<FATran.debitAccountID, IBqlInt>.IsEqual<FATran.creditAccountID>>>>.And<BqlOperand<FATran.debitSubID, IBqlInt>.IsEqual<FATran.creditSubID>>>>, Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APAROrd.ord, Equal<short1>>>>, And<BqlOperand<FATran.tranType, IBqlString>.IsEqual<FATran.tranType.depreciationPlus>>>, And<BqlOperand<FATran.debitAccountID, IBqlInt>.IsEqual<FATran.creditAccountID>>>>.And<BqlOperand<FATran.debitSubID, IBqlInt>.IsEqual<FATran.creditSubID>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APAROrd.ord, Equal<short0>>>>, And<BqlOperand<FATran.tranType, IBqlString>.IsEqual<FATran.tranType.depreciationMinus>>>, And<BqlOperand<FATran.debitAccountID, IBqlInt>.IsEqual<FATran.creditAccountID>>>>.And<BqlOperand<FATran.debitSubID, IBqlInt>.IsEqual<FATran.creditSubID>>>>, decimal0, Case<Where<BqlOperand<APAROrd.ord, IBqlShort>.IsEqual<short1>>, FATran.tranAmt, Case<Where<BqlOperand<APAROrd.ord, IBqlShort>.IsEqual<short0>>, Mult<decimal_1, FATran.tranAmt>>>>>), typeof (Decimal))]
  public virtual Decimal? SignedAmt { get; set; }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAProjectedGLTran.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAProjectedGLTran.lineNbr>
  {
  }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAProjectedGLTran.assetID>
  {
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAProjectedGLTran.bookID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAProjectedGLTran.finPeriodID>
  {
  }

  public abstract class gLAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAProjectedGLTran.gLAccountID>
  {
  }

  public abstract class gLSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAProjectedGLTran.gLSubID>
  {
  }

  public abstract class gLBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAProjectedGLTran.gLBranchID>
  {
  }

  public abstract class signedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FAProjectedGLTran.signedAmt>
  {
  }
}
