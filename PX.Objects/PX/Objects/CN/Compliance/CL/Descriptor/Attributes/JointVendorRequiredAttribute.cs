// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.JointVendorRequiredAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Compliance.CL.DAC;
using System;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes;

public class JointVendorRequiredAttribute : PXUIVerifyAttribute
{
  public JointVendorRequiredAttribute()
    : base(typeof (Where<ComplianceDocument.jointVendorInternalId, IsNotNull, And<ComplianceDocument.jointVendorExternalName, IsNull, Or<ComplianceDocument.jointVendorInternalId, IsNull, And<ComplianceDocument.jointVendorExternalName, IsNotNull, Or<ComplianceDocument.jointVendorInternalId, IsNull, And<ComplianceDocument.jointVendorExternalName, IsNull>>>>>>), (PXErrorLevel) 4, "You cannot specify the Joint Payee (Vendor) and Joint Payee at the same time. Either clear the Joint Payee (Vendor) or Joint Payee.", Array.Empty<Type>())
  {
    this.CheckOnInserted = false;
    this.CheckOnRowSelected = false;
    this.CheckOnVerify = false;
  }
}
