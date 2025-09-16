// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentAddressAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.SO;

public class SOShipmentAddressAttribute(System.Type SelectType) : AddressAttribute(typeof (SOShipmentAddress.addressID), typeof (SOShipmentAddress.isDefaultAddress), SelectType)
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    SOShipmentAddressAttribute addressAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) addressAttribute, __vmethodptr(addressAttribute, Record_Override_FieldVerifying));
    fieldVerifying.AddHandler<SOShipmentAddress.overrideAddress>(pxFieldVerifying);
  }

  public override void DefaultRecord(PXCache sender, object DocumentRow, object Row)
  {
    if (DocumentRow is SOShipment soShipment && soShipment.ShipmentType == "T")
    {
      using (new PXReadBranchRestrictedScope())
      {
        PXResult SourceRow = (PXResult) PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelectJoin<PX.Objects.CR.Address, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.FK.Address>, LeftJoin<SOShipmentAddress, On<SOShipmentAddress.customerID, Equal<PX.Objects.CR.Address.bAccountID>, And<SOShipmentAddress.customerAddressID, Equal<PX.Objects.CR.Address.addressID>, And<SOShipmentAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<SOShipmentAddress.isDefaultAddress, Equal<True>>>>>>>, Where<PX.Objects.IN.INSite.siteID, Equal<Current<SOShipment.destinationSiteID>>>>.Config>.SelectMultiBound(sender.Graph, new object[1]
        {
          DocumentRow
        }, Array.Empty<object>()));
        AddressAttribute.DefaultAddress<SOShipmentAddress, SOShipmentAddress.addressID>(sender, this.FieldName, DocumentRow, Row, (object) SourceRow);
      }
    }
    else
      this.DefaultAddress<SOShipmentAddress, SOShipmentAddress.addressID>(sender, DocumentRow, Row);
  }

  public override void CopyRecord(
    PXCache sender,
    object DocumentRow,
    object SourceRow,
    bool clone)
  {
    this.CopyAddress<SOShipmentAddress, SOShipmentAddress.addressID>(sender, DocumentRow, SourceRow, clone);
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
      this.Address_IsDefaultAddress_FieldVerifying<SOShipmentAddress>(sender, e);
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
    PXUIFieldAttribute.SetEnabled<SOShipmentAddress.overrideAddress>(sender, e.Row, true);
    PXUIFieldAttribute.SetEnabled<SOShipmentAddress.isValidated>(sender, e.Row, false);
  }
}
