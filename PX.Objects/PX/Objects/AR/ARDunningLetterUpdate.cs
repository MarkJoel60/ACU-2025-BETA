// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDunningLetterUpdate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

public class ARDunningLetterUpdate : PXGraph<ARDunningLetterUpdate, PX.Objects.AR.Customer>
{
  public const string notificationCD = "DUNNINGLETTER";
  [PXViewName("Dunning Letter")]
  public PXSelect<ARDunningLetter> docs;
  public PXSelect<ARDunningLetter, Where<ARDunningLetter.dunningLetterID, Equal<Required<ARDunningLetter.dunningLetterID>>>> DL;
  public PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Optional<ARDunningLetter.bAccountID>>>> Customer;
  [PXViewName("AR Contact")]
  public PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AR.Customer.defBillContactID>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<ARDunningLetter.bAccountID>>>> contact;

  public virtual void EMailDL(int dunningLetterID, bool markOnly, bool showAll)
  {
    ARDunningLetter arDunningLetter = PXResultset<ARDunningLetter>.op_Implicit(((PXSelectBase<ARDunningLetter>) this.DL).Select(new object[1]
    {
      (object) dunningLetterID
    }));
    bool? released = arDunningLetter.Released;
    bool flag = false;
    if (released.GetValueOrDefault() == flag & released.HasValue || arDunningLetter.Voided.GetValueOrDefault())
      throw new PXException("Document Status is invalid for processing.");
    ((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Current = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Select(new object[1]
    {
      (object) arDunningLetter.BAccountID
    }));
    if (markOnly)
    {
      arDunningLetter.DontEmail = new bool?(true);
    }
    else
    {
      ((PXSelectBase<ARDunningLetter>) this.DL).Current = arDunningLetter;
      ((PXGraph) this).GetExtension<ARDunningLetterUpdate_ActivityDetailsExt>().SendNotification<ARDunningLetter>("Customer", "DUNNINGLETTER", arDunningLetter.BranchID, (IDictionary<string, string>) new Dictionary<string, string>()
      {
        {
          "DunningLetterID",
          dunningLetterID.ToString()
        }
      }, true);
      arDunningLetter.Emailed = new bool?(true);
    }
    ((PXSelectBase) this.DL).Cache.Update((object) arDunningLetter);
    ((PXAction) this.Save).Press();
  }
}
