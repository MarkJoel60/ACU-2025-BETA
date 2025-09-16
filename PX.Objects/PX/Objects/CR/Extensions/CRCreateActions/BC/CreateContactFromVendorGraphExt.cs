// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.BC.CreateContactFromVendorGraphExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using System.Collections;

#nullable disable
namespace PX.Objects.CR.Extensions.CRCreateActions.BC;

public class CreateContactFromVendorGraphExt : PXGraphExtension<VendorMaint>
{
  public PXAction<Vendor> newContact;

  [PXUIField]
  [PXButton(ImageKey = "DataEntry")]
  public virtual IEnumerable NewContact(PXAdapter adapter)
  {
    VendorMaint.CreateContactFromVendorGraphExt extension = ((PXGraph) this.Base).GetExtension<VendorMaint.CreateContactFromVendorGraphExt>();
    return extension == null ? adapter.Get() : ((PXAction) extension.CreateContact).Press(adapter);
  }
}
