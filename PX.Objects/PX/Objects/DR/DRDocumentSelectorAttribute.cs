// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRDocumentSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.DR;

/// <summary>
/// Attribute to be put onto a reference number field
/// of various deferred revenue entities (e.g. <see cref="P:PX.Objects.DR.DRSchedule.RefNbr" />
/// that allows selecting relevant AR / AP documents that the DR entity refers to.
/// </summary>
public class DRDocumentSelectorAttribute : PXCustomSelectorAttribute
{
  protected readonly System.Type _moduleField;
  protected readonly System.Type _docTypeField;
  protected readonly System.Type _businessAccountField;

  /// <summary>
  /// Gets or sets a flag indicating whether unreleased
  /// documents should be excluded from the selection.
  /// </summary>
  public bool ExcludeUnreleased { get; set; }

  /// <param name="businessAccountField">
  /// Optional business account field. If not <c>null</c>,
  /// then the choice in the selector will be restricted by documents
  /// that correspond to that business account.
  /// </param>
  public DRDocumentSelectorAttribute(
    System.Type moduleField,
    System.Type docTypeField,
    System.Type businessAccountField = null)
    : base(typeof (DRDocumentRecord.refNbr))
  {
    if (moduleField == (System.Type) null)
      throw new ArgumentNullException(nameof (moduleField));
    if (docTypeField == (System.Type) null)
      throw new ArgumentNullException(nameof (docTypeField));
    if (BqlCommand.GetItemType(moduleField).Name != BqlCommand.GetItemType(docTypeField).Name || businessAccountField != (System.Type) null && BqlCommand.GetItemType(businessAccountField).Name != BqlCommand.GetItemType(moduleField).Name)
      throw new ArgumentException("All fields must belong to the same declaring type.");
    this._moduleField = moduleField;
    this._docTypeField = docTypeField;
    this._businessAccountField = businessAccountField;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (((PXEventSubscriberAttribute) this)._BqlTable.Name != BqlCommand.GetItemType(this._moduleField).Name)
      throw new ArgumentException("All fields must belong to the same declaring type.");
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    string str1 = (string) sender.GetValue(e.Row, this._moduleField.Name);
    string str2 = (string) sender.GetValue(e.Row, this._docTypeField.Name);
    BqlCommand bqlCommand;
    switch (str1)
    {
      case "AR":
        bqlCommand = (BqlCommand) new Select<ARRegister, Where<ARRegister.docType, Equal<Required<ARRegister.docType>>, And<ARRegister.refNbr, Equal<Required<ARRegister.refNbr>>>>>();
        if (this.ExcludeUnreleased)
        {
          bqlCommand = bqlCommand.WhereAnd<Where<ARRegister.released, Equal<True>>>();
          break;
        }
        break;
      case "AP":
        bqlCommand = (BqlCommand) new Select<APRegister, Where<APRegister.docType, Equal<Required<APRegister.docType>>, And<APRegister.refNbr, Equal<Required<APRegister.refNbr>>>>>();
        if (this.ExcludeUnreleased)
        {
          bqlCommand = bqlCommand.WhereAnd<Where<APRegister.released, Equal<True>>>();
          break;
        }
        break;
      default:
        throw new PXException("Unexpected module specified. Only AP and AR are supported.");
    }
    if (new PXView(this._Graph, true, bqlCommand).SelectSingle(new object[2]
    {
      (object) str2,
      e.NewValue
    }) != null)
      return;
    ((PXSelectorAttribute) this).throwNoItem((string[]) null, true, e.NewValue);
  }

  protected virtual IEnumerable GetRecords()
  {
    DRDocumentSelectorAttribute selectorAttribute = this;
    PXCache cach = selectorAttribute._Graph.Caches[BqlCommand.GetItemType(selectorAttribute._moduleField)];
    if (cach.Current != null)
    {
      string str1 = (string) cach.GetValue(cach.Current, selectorAttribute._docTypeField.Name);
      string str2 = (string) cach.GetValue(cach.Current, selectorAttribute._moduleField.Name);
      bool excludeUnreleased = cach.GetAttributesReadonly(cach.Current, ((PXEventSubscriberAttribute) selectorAttribute)._FieldName).OfType<DRDocumentSelectorAttribute>().First<DRDocumentSelectorAttribute>().ExcludeUnreleased;
      int? nullable = new int?();
      if (selectorAttribute._businessAccountField != (System.Type) null)
        nullable = (int?) cach.GetValue(cach.Current, selectorAttribute._businessAccountField.Name);
      switch (str2)
      {
        case "AR":
          PXSelectBase<ARInvoice> pxSelectBase1 = (PXSelectBase<ARInvoice>) new PXSelectJoin<ARInvoice, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<ARInvoice.customerID>>>, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>>>(selectorAttribute._Graph);
          if (excludeUnreleased)
            pxSelectBase1.WhereAnd<Where<ARInvoice.released, Equal<True>>>();
          object[] objArray1;
          if (nullable.HasValue)
          {
            pxSelectBase1.WhereAnd<Where<ARInvoice.customerID, Equal<Required<ARInvoice.customerID>>>>();
            objArray1 = new object[2]
            {
              (object) str1,
              (object) nullable
            };
          }
          else
            objArray1 = new object[1]{ (object) str1 };
          foreach (PXResult<ARInvoice, PX.Objects.CR.BAccount> pxResult in pxSelectBase1.Select(objArray1))
          {
            ARInvoice arInvoice = PXResult<ARInvoice, PX.Objects.CR.BAccount>.op_Implicit(pxResult);
            PX.Objects.CR.BAccount baccount = PXResult<ARInvoice, PX.Objects.CR.BAccount>.op_Implicit(pxResult);
            string str3 = (string) null;
            ARDocStatus.ListAttribute listAttribute = new ARDocStatus.ListAttribute();
            if (listAttribute.ValueLabelDic.ContainsKey(arInvoice.Status))
              str3 = listAttribute.ValueLabelDic[arInvoice.Status];
            yield return (object) new DRDocumentRecord()
            {
              BAccountCD = baccount.AcctCD,
              RefNbr = arInvoice.RefNbr,
              Status = str3,
              FinPeriodID = arInvoice.FinPeriodID,
              DocType = arInvoice.DocType,
              DocDate = arInvoice.DocDate,
              LocationID = arInvoice.CustomerLocationID,
              CuryOrigDocAmt = arInvoice.CuryOrigDocAmt,
              CuryID = arInvoice.CuryID
            };
          }
          break;
        case "AP":
          PXSelectBase<APInvoice> pxSelectBase2 = (PXSelectBase<APInvoice>) new PXSelectJoin<APInvoice, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<APInvoice.vendorID>>>, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>>>(selectorAttribute._Graph);
          if (excludeUnreleased)
            pxSelectBase2.WhereAnd<Where<APInvoice.released, Equal<True>>>();
          object[] objArray2;
          if (nullable.HasValue)
          {
            pxSelectBase2.WhereAnd<Where<APInvoice.vendorID, Equal<Required<APInvoice.vendorID>>>>();
            objArray2 = new object[2]
            {
              (object) str1,
              (object) nullable
            };
          }
          else
            objArray2 = new object[1]{ (object) str1 };
          foreach (PXResult<APInvoice, PX.Objects.CR.BAccount> pxResult in pxSelectBase2.Select(objArray2))
          {
            APInvoice apInvoice = PXResult<APInvoice, PX.Objects.CR.BAccount>.op_Implicit(pxResult);
            PX.Objects.CR.BAccount baccount = PXResult<APInvoice, PX.Objects.CR.BAccount>.op_Implicit(pxResult);
            string str4 = (string) null;
            APDocStatus.ListAttribute listAttribute = new APDocStatus.ListAttribute();
            if (listAttribute.ValueLabelDic.ContainsKey(apInvoice.Status))
              str4 = listAttribute.ValueLabelDic[apInvoice.Status];
            yield return (object) new DRDocumentRecord()
            {
              BAccountCD = baccount.AcctCD,
              RefNbr = apInvoice.RefNbr,
              Status = str4,
              FinPeriodID = apInvoice.FinPeriodID,
              DocType = apInvoice.DocType,
              DocDate = apInvoice.DocDate,
              LocationID = apInvoice.VendorLocationID,
              CuryOrigDocAmt = apInvoice.CuryOrigDocAmt,
              CuryID = apInvoice.CuryID
            };
          }
          break;
        default:
          throw new PXException("Unexpected module specified. Only AP and AR are supported.");
      }
    }
  }
}
