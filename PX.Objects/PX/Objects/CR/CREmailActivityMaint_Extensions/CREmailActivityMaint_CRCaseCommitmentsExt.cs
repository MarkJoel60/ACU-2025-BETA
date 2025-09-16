// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CREmailActivityMaint_Extensions.CREmailActivityMaint_CRCaseCommitmentsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.CR.Extensions.CRCaseCommitments;

#nullable disable
namespace PX.Objects.CR.CREmailActivityMaint_Extensions;

public class CREmailActivityMaint_CRCaseCommitmentsExt : 
  CRCaseCommitmentsExt<CREmailActivityMaint, CRSMEmail>
{
  public static bool IsActive() => CRCaseCommitmentsExt<CREmailActivityMaint>.IsExtensionActive();
}
