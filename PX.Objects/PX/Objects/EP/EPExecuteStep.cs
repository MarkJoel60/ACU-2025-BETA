// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPExecuteStep
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.EP;

public static class EPExecuteStep
{
  public const string Always = "A";
  public const string IfNoApproversFoundatPreviousSteps = "O";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "A", "O" }, new string[2]
      {
        "Always",
        "If No Approvers Found at Previous Steps"
      })
    {
    }
  }

  public class always : Constant<string>
  {
    public always()
      : base("A")
    {
    }
  }

  public class ifNoApproversFoundatPreviousSteps : Constant<string>
  {
    public ifNoApproversFoundatPreviousSteps()
      : base("O")
    {
    }
  }
}
