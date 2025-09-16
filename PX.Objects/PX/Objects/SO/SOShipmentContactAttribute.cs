// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentContactAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.SO;

public class SOShipmentContactAttribute(System.Type SelectType) : ContactAttribute(typeof (SOShipmentContact.contactID), typeof (SOShipmentContact.isDefaultContact), SelectType)
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    SOShipmentContactAttribute contactAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) contactAttribute, __vmethodptr(contactAttribute, Record_Override_FieldVerifying));
    fieldVerifying.AddHandler<SOShipmentContact.overrideContact>(pxFieldVerifying);
  }

  public override void DefaultRecord(PXCache sender, object DocumentRow, object Row)
  {
    if (DocumentRow is SOShipment soShipment && soShipment.ShipmentType == "T")
    {
      PXResult SourceRow = (PXResult) null;
      using (new PXReadBranchRestrictedScope())
        SourceRow = (PXResult) PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.FK.Contact>, LeftJoin<SOShipmentContact, On<SOShipmentContact.customerID, Equal<PX.Objects.CR.Contact.bAccountID>, And<SOShipmentContact.customerContactID, Equal<PX.Objects.CR.Contact.contactID>, And<SOShipmentContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<SOShipmentContact.isDefaultContact, Equal<True>>>>>>>, Where<PX.Objects.IN.INSite.siteID, Equal<Current<SOShipment.destinationSiteID>>>>.Config>.SelectMultiBound(sender.Graph, new object[1]
        {
          DocumentRow
        }, Array.Empty<object>()));
      ContactAttribute.DefaultContact<SOShipmentContact, SOShipmentContact.contactID>(sender, this.FieldName, DocumentRow, Row, (object) SourceRow);
    }
    else
      this.DefaultContact<SOShipmentContact, SOShipmentContact.contactID>(sender, DocumentRow, Row);
  }

  public override void CopyRecord(
    PXCache sender,
    object DocumentRow,
    object SourceRow,
    bool clone)
  {
    this.CopyContact<SOShipmentContact, SOShipmentContact.contactID>(sender, DocumentRow, SourceRow, clone);
  }

  public override void Record_IsDefault_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }

  public virtual void Record_Override_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    PXFieldVerifyingEventArgs verifyingEventArgs1 = e;
    object obj1;
    if (e.NewValue != null)
    {
      bool? newValue = (bool?) e.NewValue;
      bool flag = false;
      obj1 = (object) (newValue.GetValueOrDefault() == flag & newValue.HasValue);
    }
    else
      obj1 = e.NewValue;
    verifyingEventArgs1.NewValue = obj1;
    try
    {
      this.Contact_IsDefaultContact_FieldVerifying<SOShipmentContact>(sender, e);
    }
    finally
    {
      PXFieldVerifyingEventArgs verifyingEventArgs2 = e;
      object obj2;
      if (e.NewValue != null)
      {
        bool? newValue = (bool?) e.NewValue;
        bool flag = false;
        obj2 = (object) (newValue.GetValueOrDefault() == flag & newValue.HasValue);
      }
      else
        obj2 = e.NewValue;
      verifyingEventArgs2.NewValue = obj2;
    }
  }

  protected override void Record_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    base.Record_RowSelected(sender, e);
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<SOShipmentContact.overrideContact>(sender, e.Row, true);
  }
}
