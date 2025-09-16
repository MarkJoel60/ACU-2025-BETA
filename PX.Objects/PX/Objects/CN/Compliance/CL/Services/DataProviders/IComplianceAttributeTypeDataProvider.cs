// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Services.DataProviders.IComplianceAttributeTypeDataProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Compliance.CL.DAC;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Services.DataProviders;

public interface IComplianceAttributeTypeDataProvider
{
  ComplianceAttributeType GetComplianceAttributeType(PXGraph graph, string documentType);
}
