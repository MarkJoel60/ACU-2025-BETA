// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.LienWaiver.LienWaiverGenerationEvent
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes.LienWaiver;

public class LienWaiverGenerationEvent
{
  public const 
  #nullable disable
  string PayingBill = "Paying AP Bill";
  private static readonly string[] GenerationEvents = new string[1]
  {
    "Paying AP Bill"
  };

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(LienWaiverGenerationEvent.GenerationEvents, LienWaiverGenerationEvent.GenerationEvents)
    {
    }
  }

  public sealed class payingBill : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    LienWaiverGenerationEvent.payingBill>
  {
    public payingBill()
      : base("Paying AP Bill")
    {
    }
  }
}
