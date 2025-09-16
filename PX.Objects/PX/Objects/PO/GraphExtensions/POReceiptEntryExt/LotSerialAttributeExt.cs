// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.LotSerialAttributeExt
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
using PX.Objects.IN;
using PX.Objects.IN.DAC;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.IN.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class LotSerialAttributeExt : 
  DocumentLotSerialAttributesGridExt<
  #nullable disable
  POReceiptEntry, PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine, POReceiptLineSplit, POReceiptItemLotSerialAttributesHeader>
{
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<POReceiptItemLotSerialAttributesHeader, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  POReceiptItemLotSerialAttributesHeader.receiptType, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  POReceiptLineSplit.receiptType, IBqlString>.FromCurrent.NoDefault>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  POReceiptItemLotSerialAttributesHeader.receiptNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  POReceiptLineSplit.receiptNbr, IBqlString>.FromCurrent.NoDefault>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  POReceiptItemLotSerialAttributesHeader.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  POReceiptLineSplit.inventoryID, IBqlInt>.FromCurrent.NoDefault>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  POReceiptItemLotSerialAttributesHeader.lotSerialNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  POReceiptLineSplit.lotSerialNbr, IBqlString>.FromCurrent.NoDefault>>>, 
  #nullable disable
  POReceiptItemLotSerialAttributesHeader>.View lotSerialAttributesHeader;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<INItemLotSerialAttribute, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INItemLotSerialAttribute.inventoryID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  POReceiptLineSplit.inventoryID, IBqlInt>.FromCurrent.NoDefault>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  INItemLotSerialAttribute.isActive, IBqlBool>.IsEqual<
  #nullable disable
  True>>>>.And<BqlOperand<Current2<
  #nullable enable
  POReceiptLineSplit.lotSerialNbr>, IBqlString>.IsNotNull>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  INItemLotSerialAttribute.sortOrder, IBqlShort>.Asc>>, 
  #nullable disable
  INItemLotSerialAttribute>.View lotSerialAttributes;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXStringAttribute), "IsKey", true)]
  [PXDBCalced(typeof (CurrentValue<POReceiptLineSplit.lotSerialNbr>), typeof (string))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<INItemLotSerialAttribute.lotSerialNbr> e)
  {
  }

  public override PXSelectBase<POReceiptItemLotSerialAttributesHeader> GetAttributesHeaderView()
  {
    return (PXSelectBase<POReceiptItemLotSerialAttributesHeader>) this.lotSerialAttributesHeader;
  }

  protected override POReceiptItemLotSerialAttributesHeader GetAttributesHeader(
    int? inventoryID,
    string lotSerialNbr,
    bool insert)
  {
    PX.Objects.PO.POReceipt current = ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.CurrentDocument).Current;
    POReceiptLineSplit split = new POReceiptLineSplit()
    {
      ReceiptType = current?.ReceiptType,
      ReceiptNbr = current?.ReceiptNbr,
      InventoryID = inventoryID,
      LotSerialNbr = lotSerialNbr
    };
    POReceiptItemLotSerialAttributesHeader attributesHeader1 = this.FillAttributesHeaderKeys(split);
    POReceiptItemLotSerialAttributesHeader attributesHeader2 = ((PXSelectBase<POReceiptItemLotSerialAttributesHeader>) this.lotSerialAttributesHeader).Locate(attributesHeader1);
    if (attributesHeader2 != null && EnumerableExtensions.IsNotIn<PXEntryStatus>(((PXSelectBase) this.lotSerialAttributesHeader).Cache.GetStatus((object) attributesHeader2), (PXEntryStatus) 4, (PXEntryStatus) 3))
      return attributesHeader2;
    POReceiptItemLotSerialAttributesHeader attributesHeader3 = (POReceiptItemLotSerialAttributesHeader) ((PXSelectBase) this.lotSerialAttributesHeader).View.SelectSingleBound(new object[1]
    {
      (object) split
    }, Array.Empty<object>());
    if (attributesHeader3 == null & insert)
      attributesHeader3 = ((PXSelectBase<POReceiptItemLotSerialAttributesHeader>) this.lotSerialAttributesHeader).Insert(attributesHeader1);
    return attributesHeader3;
  }

  protected override POReceiptItemLotSerialAttributesHeader FillAttributesHeaderKeys(
    POReceiptLineSplit split)
  {
    POReceiptItemLotSerialAttributesHeader attributesHeader = base.FillAttributesHeaderKeys(split);
    attributesHeader.ReceiptType = split.ReceiptType;
    attributesHeader.ReceiptNbr = split.ReceiptNbr;
    return attributesHeader;
  }

  protected override IEnumerable<POReceiptLineSplit> GetAllCurrentDocumentSplits()
  {
    return GraphHelper.RowCast<POReceiptLineSplit>((IEnumerable) PXSelectBase<POReceiptLineSplit, PXViewOf<POReceiptLineSplit>.BasedOn<SelectFromBase<POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<POReceiptLineSplit.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<POReceiptLineSplit.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, POReceiptLineSplit>, PX.Objects.PO.POReceipt, POReceiptLineSplit>.SameAsCurrent>, And<BqlOperand<POReceiptLineSplit.lotSerialNbr, IBqlString>.IsNotNull>>>.And<BqlOperand<POReceiptLineSplit.lotSerialNbr, IBqlString>.IsNotEqual<EmptyString>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
  }

  protected override IEnumerable<POReceiptItemLotSerialAttributesHeader> GetAllCurrentDocumentLotSerialAttributesHeaders()
  {
    return GraphHelper.RowCast<POReceiptItemLotSerialAttributesHeader>((IEnumerable) PXSelectBase<POReceiptItemLotSerialAttributesHeader, PXViewOf<POReceiptItemLotSerialAttributesHeader>.BasedOn<SelectFromBase<POReceiptItemLotSerialAttributesHeader, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<POReceiptItemLotSerialAttributesHeader.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<POReceiptItemLotSerialAttributesHeader.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, POReceiptItemLotSerialAttributesHeader>, PX.Objects.PO.POReceipt, POReceiptItemLotSerialAttributesHeader>.SameAsCurrent>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
  }

  protected override IEnumerable<POReceiptLineSplit> GetCurrentDocumentLineOrderedSplits()
  {
    return (IEnumerable<POReceiptLineSplit>) GraphHelper.RowCast<POReceiptLineSplit>((IEnumerable) ((PXSelectBase<POReceiptLineSplit>) this.Base.splits).Select(Array.Empty<object>())).OrderBy<POReceiptLineSplit, int?>((Func<POReceiptLineSplit, int?>) (x => x.SplitLineNbr));
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.PO.POReceiptEntry.PopulateINReceiptAttributes(PX.Data.PXGraph,PX.Objects.PO.POReceipt,PX.Objects.IN.INRegister)" />
  /// </summary>
  [PXOverride]
  public virtual void PopulateINReceiptAttributes(
    PXGraph docgraph,
    PX.Objects.PO.POReceipt aDoc,
    PX.Objects.IN.INRegister copy,
    Action<PXGraph, PX.Objects.PO.POReceipt, PX.Objects.IN.INRegister> base_PopulateINReceiptAttributes)
  {
    base_PopulateINReceiptAttributes(docgraph, aDoc, copy);
    HashSet<(int?, string)> valueTupleSet = new HashSet<(int?, string)>();
    PXResultset<POReceiptLineSplit> pxResultset = PXSelectBase<POReceiptLineSplit, PXViewOf<POReceiptLineSplit>.BasedOn<SelectFromBase<POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<POReceiptItemLotSerialAttributesHeader>.On<POReceiptLineSplit.FK.ReceiptItemLotSerialAttributesHeader>>>.Where<KeysRelation<CompositeKey<Field<POReceiptLineSplit.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<POReceiptLineSplit.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, POReceiptLineSplit>, PX.Objects.PO.POReceipt, POReceiptLineSplit>.SameAsCurrent.And<BqlOperand<POReceiptLineSplit.lotSerialNbr, IBqlString>.IsNotNull>>>.ReadOnly.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
    {
      (object) aDoc
    }, Array.Empty<object>());
    ICopyLotSerialAttributesExt<INRegisterItemLotSerialAttributesHeader> implementation = docgraph.FindImplementation<ICopyLotSerialAttributesExt<INRegisterItemLotSerialAttributesHeader>>();
    if (implementation == null)
      throw new PXInvalidOperationException();
    foreach (PXResult<POReceiptLineSplit, POReceiptItemLotSerialAttributesHeader> pxResult in pxResultset)
    {
      POReceiptLineSplit receiptLineSplit = PXResult<POReceiptLineSplit, POReceiptItemLotSerialAttributesHeader>.op_Implicit(pxResult);
      if (valueTupleSet.Add((receiptLineSplit.InventoryID, receiptLineSplit.LotSerialNbr?.ToUpper())))
      {
        POReceiptItemLotSerialAttributesHeader source = PXResult<POReceiptLineSplit, POReceiptItemLotSerialAttributesHeader>.op_Implicit(pxResult);
        if (source.InventoryID.HasValue)
        {
          INRegisterItemLotSerialAttributesHeader attributesHeader1 = new INRegisterItemLotSerialAttributesHeader()
          {
            DocType = copy.DocType,
            RefNbr = copy.RefNbr,
            InventoryID = source.InventoryID,
            LotSerialNbr = source.LotSerialNbr
          };
          PXSelectBase<INRegisterItemLotSerialAttributesHeader> attributesHeaderView = implementation.GetAttributesHeaderView();
          INRegisterItemLotSerialAttributesHeader attributesHeader2 = attributesHeaderView.Locate(attributesHeader1);
          INRegisterItemLotSerialAttributesHeader attributesHeader3 = attributesHeader2 == null || EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) attributesHeaderView).Cache.GetStatus((object) attributesHeader2), (PXEntryStatus) 3, (PXEntryStatus) 4) ? attributesHeaderView.Insert(attributesHeader1) : attributesHeader2;
          attributesHeader3.MfgLotSerialNbr = source.MfgLotSerialNbr;
          INRegisterItemLotSerialAttributesHeader destination = attributesHeaderView.Update(attributesHeader3);
          implementation.CopyAttributes<POReceiptItemLotSerialAttributesHeader, INRegisterItemLotSerialAttributesHeader>(source, destination, GraphHelper.Caches<POReceiptItemLotSerialAttributesHeader>((PXGraph) this.Base));
        }
        else
        {
          IEnumerable<LotSerialGraphExtBase<POReceiptEntry>.AttributeInformation> requiredAttributes = this.GetRequiredAttributes(receiptLineSplit.InventoryID);
          if ((requiredAttributes != null ? requiredAttributes.FirstOrDefault<LotSerialGraphExtBase<POReceiptEntry>.AttributeInformation>() : (LotSerialGraphExtBase<POReceiptEntry>.AttributeInformation) null) != null)
          {
            PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, receiptLineSplit.InventoryID);
            throw new PXException("At least one required attribute has not been specified for the {0} lot or serial number of the {1} item in the Line Details dialog box. Specify all required attributes to save the document.", new object[2]
            {
              (object) receiptLineSplit.LotSerialNbr,
              (object) inventoryItem?.InventoryCD?.Trim()
            });
          }
        }
      }
    }
  }

  protected override bool IsAttributeReadonlyForDocument(PX.Objects.PO.POReceipt document)
  {
    return document != null && document.Released.GetValueOrDefault() || document?.ReceiptType == "RX" || document?.ReceiptType == "RN";
  }

  public override TAttributesHeader SetValue<TAttributesHeader>(
    TAttributesHeader attributesHeader,
    string attributeID,
    object value,
    PXCache<TAttributesHeader> attributesHeaderCache = null)
  {
    if (!Correction.IsActive())
      return base.SetValue<TAttributesHeader>(attributesHeader, attributeID, value, attributesHeaderCache);
    object obj1 = this.GetValue<TAttributesHeader>(attributesHeader, attributeID);
    TAttributesHeader attributesHeader1 = base.SetValue<TAttributesHeader>(attributesHeader, attributeID, value, attributesHeaderCache);
    object obj2 = value;
    if (obj1 != obj2)
      this.CorrectAffectedLines(attributesHeader.InventoryID, attributesHeader.LotSerialNbr);
    return attributesHeader1;
  }

  protected virtual IEnumerable<POReceiptLineSplit> GetAllCurrentDocumentSplits(
    int? inventoryID,
    string lotSerialNbr)
  {
    return GraphHelper.RowCast<POReceiptLineSplit>((IEnumerable) PXSelectBase<POReceiptLineSplit, PXViewOf<POReceiptLineSplit>.BasedOn<SelectFromBase<POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<POReceiptLineSplit.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<POReceiptLineSplit.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, POReceiptLineSplit>, PX.Objects.PO.POReceipt, POReceiptLineSplit>.SameAsCurrent>, And<BqlOperand<POReceiptLineSplit.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<POReceiptLineSplit.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) inventoryID,
      (object) lotSerialNbr
    }));
  }

  protected void _(
    PX.Data.Events.RowUpdated<POReceiptItemLotSerialAttributesHeader> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<POReceiptItemLotSerialAttributesHeader>>) e).Cache.ObjectsEqual<POReceiptItemLotSerialAttributesHeader.mfgLotSerialNbr>((object) e.Row, (object) e.OldRow))
      return;
    this.CorrectAffectedLines(e.Row.InventoryID, e.Row.LotSerialNbr);
  }

  private void CorrectAffectedLines(int? inventoryID, string lotSerNbr)
  {
    if (!Correction.IsActive())
      return;
    IEnumerable<POReceiptLineSplit> currentDocumentSplits = this.GetAllCurrentDocumentSplits(inventoryID, lotSerNbr);
    Correction extension = ((PXGraph) this.Base).GetExtension<Correction>();
    foreach (POReceiptLineSplit row in currentDocumentSplits)
      extension.SetLineCorrectedFromSplit(row);
  }
}
