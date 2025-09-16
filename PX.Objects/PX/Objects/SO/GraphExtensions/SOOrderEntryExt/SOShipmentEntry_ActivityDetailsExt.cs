// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOShipmentEntry_ActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class SOShipmentEntry_ActivityDetailsExt : 
  ActivityDetailsExt<SOShipmentEntry, SOShipment, SOShipment.noteID>
{
  public override Type GetBAccountIDCommand()
  {
    return typeof (Select<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<SOShipment.customerID>>>>);
  }

  public override Type GetEmailMessageTarget()
  {
    return typeof (Select<SOShipmentContact, Where<SOShipmentContact.contactID, Equal<Current<SOShipment.shipContactID>>>>);
  }
}
