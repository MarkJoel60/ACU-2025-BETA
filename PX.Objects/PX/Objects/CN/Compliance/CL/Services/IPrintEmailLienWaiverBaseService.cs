// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Services.IPrintEmailLienWaiverBaseService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.CN.Compliance.CL.DAC;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Services;

internal interface IPrintEmailLienWaiverBaseService
{
  Task Process(List<ComplianceDocument> complianceDocuments, CancellationToken cancellationToken);

  bool IsLienWaiverValid(ComplianceDocument complianceDocument);
}
