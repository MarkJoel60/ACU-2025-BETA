// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.POProjectDefaultAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Project Default Attribute specific for PO Module. Defaulting of ProjectID field in PO depends on the LineType.
/// If Line type is of type Non-Stock, Freight or Service ProjectID is defaulted depending on the setting in PMSetup (same as <see cref="T:PX.Objects.PM.ProjectDefaultAttribute" />).
/// For all other type of lines Project is defaulted with Non-Project.
/// </summary>
public class POProjectDefaultAttribute : ProjectDefaultAttribute
{
  protected readonly Type lineType;

  public POProjectDefaultAttribute(Type lineType)
    : base("PO")
  {
    this.lineType = lineType;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXEventSubscriberAttribute) this).CacheAttached(sender);
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.AddHandler(this.lineType, this.lineType.Name, new PXFieldUpdated((object) this, __methodptr(\u003CCacheAttached\u003Eb__2_0)));
  }

  protected override bool IsDefaultNonProject(PXCache sender, object row)
  {
    string str = (string) sender.GetValue(row, this.lineType.Name);
    return !(str == "NS") && !(str == "FT") && !(str == "SV") || base.IsDefaultNonProject(sender, row);
  }
}
