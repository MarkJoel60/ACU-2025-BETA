// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.PMActivityValidator.CREmailActivityMaintPMActivityValidator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.CR;

#nullable disable
namespace PX.Objects.PM.GraphExtensions.PMActivityValidator;

public class CREmailActivityMaintPMActivityValidator : PMTimeActivityValidator<CREmailActivityMaint>
{
  public static bool IsActive() => ProjectAttribute.IsPMVisible("TA");
}
