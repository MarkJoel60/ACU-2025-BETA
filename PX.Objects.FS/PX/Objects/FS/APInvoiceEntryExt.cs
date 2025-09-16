// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.APInvoiceEntryExt
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CN.ProjectAccounting.AP.GraphExtensions;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.FS;

public class APInvoiceEntryExt : PXGraphExtension<APInvoiceEntryReclassifyingExt, APInvoiceEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXOverride]
  public virtual void Reclassify(System.Action baseHandler)
  {
    foreach (PXResult<APTran> pxResult in ((PXSelectBase<APTran>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).Select(Array.Empty<object>()))
    {
      if (((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).Cache.GetExtension<FSxAPTran>((object) PXResult<APTran>.op_Implicit(pxResult)).RelatedDocNoteID.HasValue)
        throw new PXException("The bill line cannot be reclassified because it is linked to a service document.");
    }
    baseHandler();
  }
}
