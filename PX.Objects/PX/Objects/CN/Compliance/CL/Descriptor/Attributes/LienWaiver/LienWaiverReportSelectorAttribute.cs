// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.LienWaiver.LienWaiverReportSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CN.Compliance.CL.DAC;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes.LienWaiver;

public sealed class LienWaiverReportSelectorAttribute : PXCustomSelectorAttribute
{
  private static readonly Type[] Fields = new Type[28]
  {
    typeof (ComplianceDocument.complianceDocumentID),
    typeof (ComplianceDocument.creationDate),
    typeof (ComplianceDocument.projectID),
    typeof (ComplianceDocument.vendorID),
    typeof (ComplianceDocument.vendorName),
    typeof (ComplianceDocument.documentTypeValue),
    typeof (ComplianceDocument.status),
    typeof (ComplianceDocument.required),
    typeof (ComplianceDocument.received),
    typeof (ComplianceDocument.isReceivedFromJointVendor),
    typeof (ComplianceDocument.isProcessed),
    typeof (ComplianceDocument.isVoided),
    typeof (ComplianceDocument.isCreatedAutomatically),
    typeof (ComplianceDocument.customerID),
    typeof (ComplianceDocument.customerName),
    typeof (ComplianceDocument.subcontract),
    typeof (ComplianceDocument.billID),
    typeof (ComplianceDocument.billAmount),
    typeof (ComplianceDocument.lienWaiverAmount),
    typeof (ComplianceDocument.lienNoticeAmount),
    typeof (ComplianceDocument.apCheckId),
    typeof (ComplianceDocument.paymentDate),
    typeof (ComplianceDocument.throughDate),
    typeof (ComplianceDocument.jointVendorInternalId),
    typeof (ComplianceDocument.jointVendorExternalName),
    typeof (ComplianceDocument.jointAmount),
    typeof (ComplianceDocument.jointLienWaiverAmount),
    typeof (ComplianceDocument.jointLienNoticeAmount)
  };
  private readonly string LienWaiverType;

  public LienWaiverReportSelectorAttribute(string lienWaiverType)
    : base(typeof (ComplianceDocument.complianceDocumentID), LienWaiverReportSelectorAttribute.Fields)
  {
    ((PXSelectorAttribute) this).DescriptionField = typeof (ComplianceDocument.projectID);
    this.LienWaiverType = lienWaiverType;
  }

  public IEnumerable GetRecords()
  {
    return (IEnumerable) PXSelectBase<ComplianceDocument, PXViewOf<ComplianceDocument>.BasedOn<SelectFromBase<ComplianceDocument, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ComplianceAttribute>.On<BqlOperand<ComplianceDocument.documentTypeValue, IBqlInt>.IsEqual<ComplianceAttribute.attributeId>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ComplianceDocument.vendorID, IsNotNull>>>, And<BqlOperand<ComplianceDocument.projectID, IBqlInt>.IsNotNull>>, And<BqlOperand<ComplianceDocument.documentTypeValue, IBqlInt>.IsNotNull>>>.And<BqlOperand<ComplianceAttribute.value, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select(this._Graph, new object[1]
    {
      (object) this.LienWaiverType
    });
  }
}
