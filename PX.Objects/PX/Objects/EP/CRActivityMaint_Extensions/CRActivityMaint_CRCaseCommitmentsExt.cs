// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.CRActivityMaint_Extensions.CRActivityMaint_CRCaseCommitmentsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.CR;
using PX.Objects.CR.Extensions.CRCaseCommitments;

#nullable disable
namespace PX.Objects.EP.CRActivityMaint_Extensions;

public class CRActivityMaint_CRCaseCommitmentsExt : CRCaseCommitmentsExt<CRActivityMaint, CRActivity>
{
  public static bool IsActive() => CRCaseCommitmentsExt<CRActivityMaint>.IsExtensionActive();
}
