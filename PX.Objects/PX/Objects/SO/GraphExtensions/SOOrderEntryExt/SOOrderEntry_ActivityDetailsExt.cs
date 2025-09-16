// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderEntry_ActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class SOOrderEntry_ActivityDetailsExt : 
  ActivityDetailsExt<SOOrderEntry, SOOrder, SOOrder.noteID>
{
  public override Type GetBAccountIDCommand() => typeof (SOOrder.customerID);

  public override Type GetContactIDCommand() => typeof (SOOrder.contactID);

  public override Type GetEmailMessageTarget()
  {
    return typeof (Select<SOShippingContact, Where<SOShippingContact.contactID, Equal<Current<SOOrder.shipContactID>>>>);
  }
}
