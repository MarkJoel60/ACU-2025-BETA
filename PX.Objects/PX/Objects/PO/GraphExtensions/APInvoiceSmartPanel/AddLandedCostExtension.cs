// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel.AddLandedCostExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.PO.LandedCosts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel;

/// <summary>
/// This class implements graph extension to use special dialogs called Smart Panel to perform "ADD LC" (Screen AP301000)
/// </summary>
[Serializable]
public class AddLandedCostExtension : PXGraphExtension<APInvoiceEntry>
{
  [PXCopyPasteHiddenView]
  public PXFilter<POLandedCostDetailFilter> landedCostFilter;
  [PXCopyPasteHiddenView]
  public PXSelect<POLandedCostDetailS> LandedCostDetailsAdd;
  [PXCopyPasteHiddenView]
  public PXSelect<POLandedCostDetail> LandedCostDetails;
  public PXAction<PX.Objects.AP.APInvoice> viewLandedCostDetailsAdd;
  public PXAction<PX.Objects.AP.APInvoice> addLandedCost;
  public PXAction<PX.Objects.AP.APInvoice> addLandedCost2;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  public virtual void Initialize() => ((PXGraphExtension) this).Initialize();

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual void ViewLandedCostDetailsAdd()
  {
    POLandedCostDetailS current = ((PXSelectBase<POLandedCostDetailS>) this.LandedCostDetailsAdd).Current;
    if (current == null)
      return;
    POLandedCostDoc poLandedCostDoc = POLandedCostDoc.PK.Find((PXGraph) this.Base, current.DocType, current.RefNbr) ?? throw new PXException("Document {0} {1} cannot be found in the system.", new object[2]
    {
      (object) current.DocType,
      (object) current.RefNbr
    });
    PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<POLandedCostDocEntry>(), (object) poLandedCostDoc, (PXRedirectHelper.WindowMode) 3);
  }

  [PXUIField]
  [PXLookupButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable AddLandedCost(PXAdapter adapter)
  {
    // ISSUE: method pointer
    return ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current != null && !((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.Released.GetValueOrDefault() && ((PXSelectBase<POLandedCostDetailS>) this.LandedCostDetailsAdd).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CAddLandedCost\u003Eb__9_0)), true) == 1 ? this.AddLandedCost2(adapter) : adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable AddLandedCost2(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current != null && EnumerableExtensions.IsIn<string>(((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.DocType, "INV", "ADR"))
    {
      bool? released = ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.Released;
      bool flag1 = false;
      if (released.GetValueOrDefault() == flag1 & released.HasValue)
      {
        bool? prebooked = ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.Prebooked;
        bool flag2 = false;
        if (prebooked.GetValueOrDefault() == flag2 & prebooked.HasValue)
        {
          POLandedCostDetailS[] array = GraphHelper.RowCast<POLandedCostDetailS>(((PXSelectBase) this.LandedCostDetailsAdd).Cache.Updated).Where<POLandedCostDetailS>((Func<POLandedCostDetailS, bool>) (t => t.Selected.GetValueOrDefault())).ToArray<POLandedCostDetailS>();
          this.Base.AddLandedCosts((IEnumerable<POLandedCostDetailS>) array);
          EnumerableExtensions.ForEach<POLandedCostDetailS>((IEnumerable<POLandedCostDetailS>) array, (Action<POLandedCostDetailS>) (t => t.Selected = new bool?(false)));
        }
      }
    }
    return adapter.Get();
  }

  protected virtual void APInvoice_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.AP.APInvoice row))
      return;
    APInvoiceState documentState = this.Base.GetDocumentState(cache, row);
    ((PXAction) this.addLandedCost).SetVisible(documentState.IsDocumentInvoice || documentState.IsDocumentDebitAdjustment);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.LandedCostDetailsAdd).Cache, (string) null, false);
    bool flag = documentState.IsDocumentEditable && documentState.LandedCostEnabled && !documentState.IsDocumentScheduled && !documentState.IsRetainageDebAdj;
    ((PXAction) this.addLandedCost).SetEnabled(flag);
    PXUIFieldAttribute.SetEnabled<POLandedCostDetailS.selected>(((PXSelectBase) this.LandedCostDetailsAdd).Cache, (object) null, flag);
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (PX.Objects.AP.APInvoice.refNbr))]
  [PXParent(typeof (Select<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APInvoice.docType, Equal<Current<POLandedCostDetail.aPDocType>>, And<PX.Objects.AP.APInvoice.refNbr, Equal<Current<POLandedCostDetail.aPRefNbr>>>>>), LeaveChildren = true)]
  protected virtual void POLandedCostDetail_APRefNbr_CacheAttached(PXCache cache)
  {
  }

  protected virtual IEnumerable landedCostDetailsAdd()
  {
    PXSelectJoinGroupBy<POLandedCostDetailS, InnerJoin<POLandedCostDoc, On<POLandedCostDetailS.docType, Equal<POLandedCostDoc.docType>, And<POLandedCostDetailS.refNbr, Equal<POLandedCostDoc.refNbr>>>, LeftJoin<POLandedCostReceiptLine, On<POLandedCostDetailS.docType, Equal<POLandedCostReceiptLine.docType>, And<POLandedCostDetailS.refNbr, Equal<POLandedCostReceiptLine.refNbr>>>, LeftJoin<PX.Objects.PO.POReceiptLine, On<POLandedCostReceiptLine.FK.ReceiptLine>>>>, Where<POLandedCostDetailS.aPRefNbr, IsNull, And2<Where<POLandedCostDoc.vendorID, Equal<Current<PX.Objects.AP.APRegister.vendorID>>, Or<FeatureInstalled<FeaturesSet.vendorRelations>>>, And2<Where<POLandedCostDoc.vendorLocationID, Equal<Current<PX.Objects.AP.APRegister.vendorLocationID>>, Or<FeatureInstalled<FeaturesSet.vendorRelations>>>, And2<Where<POLandedCostDoc.payToVendorID, Equal<Current<PX.Objects.AP.APRegister.vendorID>>, Or<Not<FeatureInstalled<FeaturesSet.vendorRelations>>>>, And<POLandedCostDoc.curyID, Equal<Current<PX.Objects.AP.APRegister.curyID>>, And<POLandedCostDoc.released, Equal<True>, And2<Where<Current<POLandedCostDetailFilter.receiptType>, IsNull, Or<Current<POLandedCostDetailFilter.receiptType>, Equal<POReceiptType.all>, Or<Current<POLandedCostDetailFilter.receiptType>, Equal<POLandedCostReceiptLine.pOReceiptType>>>>, And2<Where<Current<POLandedCostDetailFilter.receiptNbr>, IsNull, Or<Current<POLandedCostDetailFilter.receiptNbr>, Equal<POLandedCostReceiptLine.pOReceiptNbr>>>, And2<Where<Current<POLandedCostDetailFilter.orderNbr>, IsNull, Or<Where<Current<POLandedCostDetailFilter.orderType>, Equal<PX.Objects.PO.POReceiptLine.pOType>, And<Current<POLandedCostDetailFilter.orderNbr>, Equal<PX.Objects.PO.POReceiptLine.pONbr>>>>>, And2<Where<Current<POLandedCostDetailFilter.landedCostDocRefNbr>, IsNull, Or<Current<POLandedCostDetailFilter.landedCostDocRefNbr>, Equal<POLandedCostDetailS.refNbr>>>, And<Where<Current<POLandedCostDetailFilter.landedCostCodeID>, IsNull, Or<Current<POLandedCostDetailFilter.landedCostCodeID>, Equal<POLandedCostDetailS.landedCostCodeID>>>>>>>>>>>>>>, Aggregate<GroupBy<POLandedCostDetailS.docType, GroupBy<POLandedCostDetailS.refNbr, GroupBy<POLandedCostDetailS.lineNbr>>>>> selectJoinGroupBy = new PXSelectJoinGroupBy<POLandedCostDetailS, InnerJoin<POLandedCostDoc, On<POLandedCostDetailS.docType, Equal<POLandedCostDoc.docType>, And<POLandedCostDetailS.refNbr, Equal<POLandedCostDoc.refNbr>>>, LeftJoin<POLandedCostReceiptLine, On<POLandedCostDetailS.docType, Equal<POLandedCostReceiptLine.docType>, And<POLandedCostDetailS.refNbr, Equal<POLandedCostReceiptLine.refNbr>>>, LeftJoin<PX.Objects.PO.POReceiptLine, On<POLandedCostReceiptLine.FK.ReceiptLine>>>>, Where<POLandedCostDetailS.aPRefNbr, IsNull, And2<Where<POLandedCostDoc.vendorID, Equal<Current<PX.Objects.AP.APRegister.vendorID>>, Or<FeatureInstalled<FeaturesSet.vendorRelations>>>, And2<Where<POLandedCostDoc.vendorLocationID, Equal<Current<PX.Objects.AP.APRegister.vendorLocationID>>, Or<FeatureInstalled<FeaturesSet.vendorRelations>>>, And2<Where<POLandedCostDoc.payToVendorID, Equal<Current<PX.Objects.AP.APRegister.vendorID>>, Or<Not<FeatureInstalled<FeaturesSet.vendorRelations>>>>, And<POLandedCostDoc.curyID, Equal<Current<PX.Objects.AP.APRegister.curyID>>, And<POLandedCostDoc.released, Equal<True>, And2<Where<Current<POLandedCostDetailFilter.receiptType>, IsNull, Or<Current<POLandedCostDetailFilter.receiptType>, Equal<POReceiptType.all>, Or<Current<POLandedCostDetailFilter.receiptType>, Equal<POLandedCostReceiptLine.pOReceiptType>>>>, And2<Where<Current<POLandedCostDetailFilter.receiptNbr>, IsNull, Or<Current<POLandedCostDetailFilter.receiptNbr>, Equal<POLandedCostReceiptLine.pOReceiptNbr>>>, And2<Where<Current<POLandedCostDetailFilter.orderNbr>, IsNull, Or<Where<Current<POLandedCostDetailFilter.orderType>, Equal<PX.Objects.PO.POReceiptLine.pOType>, And<Current<POLandedCostDetailFilter.orderNbr>, Equal<PX.Objects.PO.POReceiptLine.pONbr>>>>>, And2<Where<Current<POLandedCostDetailFilter.landedCostDocRefNbr>, IsNull, Or<Current<POLandedCostDetailFilter.landedCostDocRefNbr>, Equal<POLandedCostDetailS.refNbr>>>, And<Where<Current<POLandedCostDetailFilter.landedCostCodeID>, IsNull, Or<Current<POLandedCostDetailFilter.landedCostCodeID>, Equal<POLandedCostDetailS.landedCostCodeID>>>>>>>>>>>>>>, Aggregate<GroupBy<POLandedCostDetailS.docType, GroupBy<POLandedCostDetailS.refNbr, GroupBy<POLandedCostDetailS.lineNbr>>>>>((PXGraph) this.Base);
    int startRow = PXView.StartRow;
    int num = 0;
    List<POLandedCostDetailS> list = GraphHelper.RowCast<POLandedCostDetailS>((IEnumerable) ((PXSelectBase) selectJoinGroupBy).View.Select(PXView.Currents, PXView.Parameters, new object[PXView.SortColumns.Length], PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num)).ToList<POLandedCostDetailS>();
    PXView.StartRow = 0;
    foreach (POLandedCostDetail landedCostDetail in GraphHelper.RowCast<POLandedCostDetail>(((PXCache) GraphHelper.Caches<POLandedCostDetail>((PXGraph) this.Base)).Updated))
    {
      POLandedCostDetail poLandedCostDetail = landedCostDetail;
      POLandedCostDetailS landedCostDetailS = GraphHelper.RowCast<POLandedCostDetailS>((IEnumerable) list).SingleOrDefault<POLandedCostDetailS>((Func<POLandedCostDetailS, bool>) (t =>
      {
        if (!(t.DocType == poLandedCostDetail.DocType) || !(t.RefNbr == poLandedCostDetail.RefNbr))
          return false;
        int? lineNbr1 = t.LineNbr;
        int? lineNbr2 = poLandedCostDetail.LineNbr;
        return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
      }));
      if (landedCostDetailS != null)
      {
        landedCostDetailS.APDocType = poLandedCostDetail.APDocType;
        landedCostDetailS.APRefNbr = poLandedCostDetail.APRefNbr;
      }
    }
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current != null)
      list = list.Where<POLandedCostDetailS>((Func<POLandedCostDetailS, bool>) (t => t.APRefNbr != ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.RefNbr)).ToList<POLandedCostDetailS>();
    return (IEnumerable) list;
  }
}
