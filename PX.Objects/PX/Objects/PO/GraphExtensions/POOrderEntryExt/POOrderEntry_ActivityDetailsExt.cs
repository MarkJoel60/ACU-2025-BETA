// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POOrderEntryExt.POOrderEntry_ActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions;
using System;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POOrderEntryExt;

public class POOrderEntry_ActivityDetailsExt : 
  ActivityDetailsExt<POOrderEntry, PX.Objects.PO.POOrder, PX.Objects.PO.POOrder.noteID>
{
  public override Type GetBAccountIDCommand()
  {
    return typeof (Select<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<PX.Objects.PO.POOrder.vendorID>>>>);
  }

  public override Type GetEmailMessageTarget()
  {
    return typeof (Select<PORemitContact, Where<PORemitContact.contactID, Equal<Current<PX.Objects.PO.POOrder.remitContactID>>>>);
  }
}
