// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ProcessLienWaiverActionsAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes;

public class ProcessLienWaiverActionsAttribute : PXStringListAttribute
{
  public const string EmailLienWaiver = "Email Lien Waivers";
  public const string PrintLienWaiver = "Print Lien Waivers";
  public const string SetAsFinal = "Set as Final";
  private static readonly string[] ProcessLienWaiverActions = new string[3]
  {
    "Email Lien Waivers",
    "Print Lien Waivers",
    "Set as Final"
  };

  public ProcessLienWaiverActionsAttribute()
    : base(ProcessLienWaiverActionsAttribute.ProcessLienWaiverActions, ProcessLienWaiverActionsAttribute.ProcessLienWaiverActions)
  {
  }
}
