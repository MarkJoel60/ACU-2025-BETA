// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.AP.Descriptor.LinkLineSelectedModeListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CN.Subcontracts.AP.Descriptor;

public class LinkLineSelectedModeListAttribute : PXStringListAttribute
{
  public LinkLineSelectedModeListAttribute()
    : base(new string[3]{ "O", "R", "L" }, new string[3]
    {
      "Purchase Order / Subcontract",
      "Purchase Receipt",
      "Landed Cost"
    })
  {
  }
}
