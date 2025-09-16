// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractDetailTypeSetupAndRenewal
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CT;

public static class ContractDetailTypeSetupAndRenewal
{
  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "S", "R", "I" }, new string[3]
      {
        "Setup",
        "Renewal",
        "Re-Installment"
      })
    {
    }
  }
}
