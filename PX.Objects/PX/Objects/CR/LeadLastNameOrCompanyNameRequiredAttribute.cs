// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.LeadLastNameOrCompanyNameRequiredAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CR;

public class LeadLastNameOrCompanyNameRequiredAttribute : 
  PXEventSubscriberAttribute,
  IPXRowPersistingSubscriber
{
  public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Row is CRLead row && string.IsNullOrEmpty(row.LastName) && string.IsNullOrEmpty(row.FullName))
      throw new PXSetPropertyException("Last Name or Account Name (or both) should be filled in.");
  }
}
