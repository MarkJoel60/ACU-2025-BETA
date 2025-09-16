// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.LienWaiverTypesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes;

public class LienWaiverTypesAttribute : PXStringListAttribute
{
  private static readonly string[] LienWaiverTypes = new string[5]
  {
    "All",
    "Conditional Partial",
    "Conditional Final",
    "Unconditional Partial",
    "Unconditional Final"
  };

  public LienWaiverTypesAttribute()
    : base(LienWaiverTypesAttribute.LienWaiverTypes, LienWaiverTypesAttribute.LienWaiverTypes)
  {
  }
}
