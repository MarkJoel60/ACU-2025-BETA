// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.ExcludedVendorDomainMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.InvoiceRecognition.DAC;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition;

[PXInternalUseOnly]
public class ExcludedVendorDomainMaint : PXGraph<ExcludedVendorDomainMaint>
{
  public FbqlSelect<SelectFromBase<ExcludedVendorDomain, TypeArrayOf<IFbqlJoin>.Empty>, ExcludedVendorDomain>.View Domains;
  public PXSave<ExcludedVendorDomain> Save;
  public PXCancel<ExcludedVendorDomain> Cancel;
}
