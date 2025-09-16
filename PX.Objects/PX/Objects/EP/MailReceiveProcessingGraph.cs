// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.MailReceiveProcessingGraph
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CR.Extensions.CRCaseCommitments;

#nullable enable
namespace PX.Objects.EP;

[PXInternalUseOnly]
public class MailReceiveProcessingGraph : PXGraph<MailReceiveProcessingGraph>
{
  public MailReceiveProcessingGraph() => ((PXGraph) this).Defaults.Remove(typeof (AccessInfo));

  public class CRCaseCommitments : CRCaseCommitmentsExt<MailReceiveProcessingGraph, CRSMEmail>
  {
    public static bool IsActive()
    {
      return CRCaseCommitmentsExt<MailReceiveProcessingGraph>.IsExtensionActive();
    }
  }
}
