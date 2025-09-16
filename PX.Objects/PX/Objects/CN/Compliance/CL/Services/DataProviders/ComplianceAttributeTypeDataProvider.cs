// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Services.DataProviders.ComplianceAttributeTypeDataProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CN.Compliance.CL.DAC;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Services.DataProviders;

public class ComplianceAttributeTypeDataProvider : IComplianceAttributeTypeDataProvider
{
  public ComplianceAttributeType GetComplianceAttributeType(PXGraph graph, string documentType)
  {
    return PXResultset<ComplianceAttributeType>.op_Implicit(PXSelectBase<ComplianceAttributeType, PXViewOf<ComplianceAttributeType>.BasedOn<SelectFromBase<ComplianceAttributeType, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<ComplianceAttributeType.type, IBqlString>.IsEqual<P.AsString>>>.Config>.Select(graph, new object[1]
    {
      (object) documentType
    }));
  }
}
