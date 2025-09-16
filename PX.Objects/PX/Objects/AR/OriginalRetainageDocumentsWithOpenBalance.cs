// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.OriginalRetainageDocumentsWithOpenBalance
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;

#nullable enable
namespace PX.Objects.AR;

[PXHidden]
[PXProjection(typeof (SelectFromBase<ARRegisterReport, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ARTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegisterReport.docType, Equal<ARTran.tranType>>>>>.And<BqlOperand<ARRegisterReport.refNbr, IBqlString>.IsEqual<ARTran.refNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegisterReport.isRetainageDocument, Equal<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegisterReport.status, NotEqual<ARDocStatus.closed>>>>>.And<BqlOperand<ARTran.origRefNbr, IBqlString>.IsNotNull>>>.Aggregate<To<GroupBy<ARTran.origDocType, GroupBy<ARTran.origRefNbr>>>>))]
public class OriginalRetainageDocumentsWithOpenBalance : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage
{
  [PXDBString(IsKey = true, BqlField = typeof (ARTran.origDocType))]
  public virtual 
  #nullable disable
  string OrigDocType { get; set; }

  [PXDBString(IsKey = true, BqlField = typeof (ARTran.origRefNbr))]
  public virtual string OrigRefNbr { get; set; }

  public abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OriginalRetainageDocumentsWithOpenBalance.origDocType>
  {
  }

  public abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OriginalRetainageDocumentsWithOpenBalance.origRefNbr>
  {
  }
}
