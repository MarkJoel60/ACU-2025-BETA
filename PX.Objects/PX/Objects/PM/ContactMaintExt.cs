// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ContactMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class ContactMaintExt : PXGraphExtension<ContactMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.CR.Contact> e)
  {
    if (e.Row == null)
      return;
    PXSelectReadonly<PMProjectContact, Where<PMProjectContact.contactID, Equal<Required<PMProjectContact.contactID>>>> pxSelectReadonly = new PXSelectReadonly<PMProjectContact, Where<PMProjectContact.contactID, Equal<Required<PMProjectContact.contactID>>>>((PXGraph) this.Base);
    ((PXSelectBase<PMProjectContact>) pxSelectReadonly).SelectWindowed(0, 10, new object[1]
    {
      (object) e.Row.ContactID
    });
    List<string> items = new List<string>();
    foreach (PXResult<PMProjectContact> pxResult in ((PXSelectBase<PMProjectContact>) pxSelectReadonly).SelectWindowed(0, 10, new object[1]
    {
      (object) e.Row.ContactID
    }))
    {
      PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, PXResult<PMProjectContact>.op_Implicit(pxResult).ProjectID);
      if (pmProject != null)
        items.Add(pmProject.ContractCD.Trim());
    }
    if (items.Count > 0)
      throw new PXSetPropertyException<PX.Objects.CR.Contact.contactID>("The contact cannot be deleted because it is used in the following projects or project templates: {0}.", new object[1]
      {
        (object) items.JoinIntoStringForMessage<string>()
      });
  }
}
