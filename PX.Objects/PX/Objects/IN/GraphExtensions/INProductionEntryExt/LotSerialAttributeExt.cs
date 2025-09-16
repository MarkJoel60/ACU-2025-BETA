// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INProductionEntryExt.LotSerialAttributeExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.BQLConstants;
using PX.Objects.CS;
using PX.Objects.IN.DAC;
using PX.Objects.IN.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN.GraphExtensions.INProductionEntryExt;

public class LotSerialAttributeExt : 
  DocumentLotSerialAttributesGridExt<
  #nullable disable
  INProductionEntry, PX.Objects.IN.INRegister, INTran, INTranSplit, INRegisterItemLotSerialAttributesHeader>,
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
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<INItemLotSerialAttribute, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INItemLotSerialAttribute.inventoryID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  INTranSplit.inventoryID, IBqlInt>.FromCurrent.NoDefault>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  INItemLotSerialAttribute.isActive, IBqlBool>.IsEqual<
  #nullable disable
  True>>>>.And<BqlOperand<Current2<
  #nullable enable
  INTranSplit.lotSerialNbr>, IBqlString>.IsNotNull>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  INItemLotSerialAttribute.sortOrder, IBqlShort>.Asc>>, 
  #nullable disable
  INItemLotSerialAttribute>.View lotSerialAttributes;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXStringAttribute), "IsKey", true)]
  [PXDBCalced(typeof (CurrentValue<INTranSplit.lotSerialNbr>), typeof (string))]
  protected virtual void _(
    Events.CacheAttached<INItemLotSerialAttribute.lotSerialNbr> e)
  {
  }

  public override PXSelectBase<INRegisterItemLotSerialAttributesHeader> GetAttributesHeaderView()
  {
    return (PXSelectBase<INRegisterItemLotSerialAttributesHeader>) this.lotSerialAttributesHeader;
  }

  protected override INRegisterItemLotSerialAttributesHeader GetAttributesHeader(
    int? inventoryID,
    string lotSerialNbr,
    bool insert = false)
  {
    PX.Objects.IN.INRegister current = ((PXSelectBase<PX.Objects.IN.INRegister>) this.Base.Document).Current;
    INTranSplit split = new INTranSplit()
    {
      DocType = current?.DocType,
      RefNbr = current?.RefNbr,
      InventoryID = inventoryID,
      LotSerialNbr = lotSerialNbr
    };
    INRegisterItemLotSerialAttributesHeader attributesHeader1 = this.FillAttributesHeaderKeys(split);
    INRegisterItemLotSerialAttributesHeader attributesHeader2 = ((PXSelectBase<INRegisterItemLotSerialAttributesHeader>) this.lotSerialAttributesHeader).Locate(attributesHeader1);
    if (attributesHeader2 != null && EnumerableExtensions.IsNotIn<PXEntryStatus>(((PXSelectBase) this.lotSerialAttributesHeader).Cache.GetStatus((object) attributesHeader2), (PXEntryStatus) 4, (PXEntryStatus) 3))
      return attributesHeader2;
    INRegisterItemLotSerialAttributesHeader attributesHeader3 = (INRegisterItemLotSerialAttributesHeader) ((PXSelectBase) this.lotSerialAttributesHeader).View.SelectSingleBound(new object[1]
    {
      (object) split
    }, Array.Empty<object>());
    if (attributesHeader3 == null & insert)
      attributesHeader3 = ((PXSelectBase<INRegisterItemLotSerialAttributesHeader>) this.lotSerialAttributesHeader).Insert(attributesHeader1);
    return attributesHeader3;
  }

  protected override INRegisterItemLotSerialAttributesHeader FillAttributesHeaderKeys(
    INTranSplit split)
  {
    INRegisterItemLotSerialAttributesHeader attributesHeader = base.FillAttributesHeaderKeys(split);
    attributesHeader.DocType = split.DocType;
    attributesHeader.RefNbr = split.RefNbr;
    return attributesHeader;
  }

  protected override IEnumerable<INTranSplit> GetAllCurrentDocumentSplits()
  {
    return GraphHelper.RowCast<INTranSplit>((IEnumerable) PXSelectBase<INTranSplit, PXViewOf<INTranSplit>.BasedOn<SelectFromBase<INTranSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<INTranSplit.docType>.IsRelatedTo<PX.Objects.IN.INRegister.docType>, Field<INTranSplit.refNbr>.IsRelatedTo<PX.Objects.IN.INRegister.refNbr>>.WithTablesOf<PX.Objects.IN.INRegister, INTranSplit>, PX.Objects.IN.INRegister, INTranSplit>.SameAsCurrent>, And<BqlOperand<INTranSplit.lotSerialNbr, IBqlString>.IsNotNull>>>.And<BqlOperand<INTranSplit.lotSerialNbr, IBqlString>.IsNotEqual<EmptyString>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
  }

  protected override IEnumerable<INRegisterItemLotSerialAttributesHeader> GetAllCurrentDocumentLotSerialAttributesHeaders()
  {
    return GraphHelper.RowCast<INRegisterItemLotSerialAttributesHeader>((IEnumerable) PXSelectBase<INRegisterItemLotSerialAttributesHeader, PXViewOf<INRegisterItemLotSerialAttributesHeader>.BasedOn<SelectFromBase<INRegisterItemLotSerialAttributesHeader, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<INRegisterItemLotSerialAttributesHeader.docType>.IsRelatedTo<PX.Objects.IN.INRegister.docType>, Field<INRegisterItemLotSerialAttributesHeader.refNbr>.IsRelatedTo<PX.Objects.IN.INRegister.refNbr>>.WithTablesOf<PX.Objects.IN.INRegister, INRegisterItemLotSerialAttributesHeader>, PX.Objects.IN.INRegister, INRegisterItemLotSerialAttributesHeader>.SameAsCurrent>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
  }

  protected override IEnumerable<INTranSplit> GetCurrentDocumentLineOrderedSplits()
  {
    return (IEnumerable<INTranSplit>) GraphHelper.RowCast<INTranSplit>((IEnumerable) ((PXSelectBase<INTranSplit>) this.Base.splits).Select(Array.Empty<object>())).OrderBy<INTranSplit, int?>((Func<INTranSplit, int?>) (x => x.SplitLineNbr));
  }

  protected override bool IsAttributeReadonlyForDocument(PX.Objects.IN.INRegister document)
  {
    return document != null && document.Released.GetValueOrDefault();
  }
}
