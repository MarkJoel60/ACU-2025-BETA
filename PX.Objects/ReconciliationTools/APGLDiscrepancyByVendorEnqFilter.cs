// Decompiled with JetBrains decompiler
// Type: ReconciliationTools.APGLDiscrepancyByVendorEnqFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.AP;
using System;

#nullable enable
namespace ReconciliationTools;

[Serializable]
public class APGLDiscrepancyByVendorEnqFilter : APGLDiscrepancyEnqFilter
{
  [Vendor(DescriptionField = typeof (Vendor.acctName))]
  public virtual int? VendorID { get; set; }

  public abstract class vendorID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    APGLDiscrepancyByVendorEnqFilter.vendorID>
  {
  }
}
