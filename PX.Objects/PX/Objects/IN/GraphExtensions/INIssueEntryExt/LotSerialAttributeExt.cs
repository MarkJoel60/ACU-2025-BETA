// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INIssueEntryExt.LotSerialAttributeExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN.DAC;
using PX.Objects.IN.Interfaces;

#nullable enable
namespace PX.Objects.IN.GraphExtensions.INIssueEntryExt;

public class LotSerialAttributeExt : 
  LotSerialGraphExtBase<
  #nullable disable
  INIssueEntry>,
  ICopyLotSerialAttributesExt<INRegisterItemLotSerialAttributesHeader>
{
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<INRegisterItemLotSerialAttributesHeader, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INRegisterItemLotSerialAttributesHeader.docType, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  INTranSplit.docType, IBqlString>.FromCurrent.NoDefault>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  INRegisterItemLotSerialAttributesHeader.refNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INTranSplit.refNbr, IBqlString>.FromCurrent.NoDefault>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  INRegisterItemLotSerialAttributesHeader.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INTranSplit.inventoryID, IBqlInt>.FromCurrent.NoDefault>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  INRegisterItemLotSerialAttributesHeader.lotSerialNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INTranSplit.lotSerialNbr, IBqlString>.FromCurrent.NoDefault>>>, 
  #nullable disable
  INRegisterItemLotSerialAttributesHeader>.View lotSerialAttributesHeader;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();

  public PXSelectBase<INRegisterItemLotSerialAttributesHeader> GetAttributesHeaderView()
  {
    return (PXSelectBase<INRegisterItemLotSerialAttributesHeader>) this.lotSerialAttributesHeader;
  }
}
