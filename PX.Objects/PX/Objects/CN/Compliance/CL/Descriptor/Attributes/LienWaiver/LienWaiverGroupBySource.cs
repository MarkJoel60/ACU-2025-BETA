// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.LienWaiver.LienWaiverGroupBySource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes.LienWaiver;

public class LienWaiverGroupBySource
{
  public const string CommitmentProjectTask = "CPT";
  public const string CommitmentProject = "CP";
  public const string ProjectTask = "PT";
  public const string Project = "P";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "CPT", "CP", "PT", "P" }, new string[4]
      {
        "Commitment, Project, Project Task",
        "Commitment, Project",
        "Project, Project Task",
        "Project"
      })
    {
    }
  }
}
