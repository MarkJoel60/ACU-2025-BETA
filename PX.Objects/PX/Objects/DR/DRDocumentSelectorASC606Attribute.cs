// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRDocumentSelectorASC606Attribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.DR;

public class DRDocumentSelectorASC606Attribute(
  System.Type moduleField,
  System.Type docTypeField,
  System.Type businessAccountField = null) : DRDocumentSelectorAttribute(moduleField, docTypeField, businessAccountField)
{
  protected override IEnumerable GetRecords()
  {
    DRDocumentSelectorASC606Attribute selectorAsC606Attribute = this;
    PXCache cach = selectorAsC606Attribute._Graph.Caches[BqlCommand.GetItemType(selectorAsC606Attribute._moduleField)];
    if (cach.Current != null)
    {
      string str1 = (string) cach.GetValue(cach.Current, selectorAsC606Attribute._docTypeField.Name);
      string str2 = (string) cach.GetValue(cach.Current, selectorAsC606Attribute._moduleField.Name);
      bool excludeUnreleased = cach.GetAttributesReadonly(cach.Current, ((PXEventSubscriberAttribute) selectorAsC606Attribute)._FieldName).OfType<DRDocumentSelectorAttribute>().First<DRDocumentSelectorAttribute>().ExcludeUnreleased;
      int? nullable = new int?();
      if (selectorAsC606Attribute._businessAccountField != (System.Type) null)
        nullable = (int?) cach.GetValue(cach.Current, selectorAsC606Attribute._businessAccountField.Name);
      if (str2 == "AR")
      {
        PXSelectBase<ARInvoice> pxSelectBase = (PXSelectBase<ARInvoice>) new PXSelectJoin<ARInvoice, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<ARInvoice.customerID>>>, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<Where<ARRegister.drSchedCntr, Less<ARRegister.lineCntr>, Or<ARRegister.drSchedCntr, IsNull>>>>>(selectorAsC606Attribute._Graph);
        if (excludeUnreleased)
          pxSelectBase.WhereAnd<Where<ARInvoice.released, Equal<True>>>();
        object[] objArray;
        if (nullable.HasValue)
        {
          pxSelectBase.WhereAnd<Where<ARInvoice.customerID, Equal<Required<ARInvoice.customerID>>>>();
          objArray = new object[2]
          {
            (object) str1,
            (object) nullable
          };
        }
        else
          objArray = new object[1]{ (object) str1 };
        foreach (PXResult<ARInvoice, PX.Objects.CR.BAccount> pxResult in pxSelectBase.Select(objArray))
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
      }
      else
      {
        // ISSUE: reference to a compiler-generated method
        foreach (object record in selectorAsC606Attribute.\u003C\u003En__0())
          yield return record;
      }
    }
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    string str1 = (string) sender.GetValue(e.Row, this._moduleField.Name);
    string str2 = (string) sender.GetValue(e.Row, this._docTypeField.Name);
    if (str1 == "AR")
    {
      BqlCommand bqlCommand = (BqlCommand) new Select<ARRegister, Where<ARRegister.docType, Equal<Required<ARRegister.docType>>, And<ARRegister.refNbr, Equal<Required<ARRegister.refNbr>>, And<Where<ARRegister.drSchedCntr, Less<ARRegister.lineCntr>, Or<ARRegister.drSchedCntr, IsNull>>>>>>();
      if (this.ExcludeUnreleased)
        bqlCommand = bqlCommand.WhereAnd<Where<ARRegister.released, Equal<True>>>();
      if (new PXView(this._Graph, true, bqlCommand).SelectSingle(new object[2]
      {
        (object) str2,
        e.NewValue
      }) != null)
        return;
      ((PXSelectorAttribute) this).throwNoItem((string[]) null, true, e.NewValue);
    }
    else
      base.FieldVerifying(sender, e);
  }
}
