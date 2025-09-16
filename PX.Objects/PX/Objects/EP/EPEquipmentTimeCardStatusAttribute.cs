// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEquipmentTimeCardStatusAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

public class EPEquipmentTimeCardStatusAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string OnHold = "H";
  public const string PendingApproval = "O";
  public const string Approved = "A";
  public const string Rejected = "C";
  public const string Released = "R";

  public EPEquipmentTimeCardStatusAttribute()
    : base(new string[5]{ "H", "O", "A", "C", "R" }, new string[5]
    {
      "On Hold",
      "Pending Approval",
      nameof (Approved),
      nameof (Rejected),
      nameof (Released)
    })
  {
  }

  public sealed class onHold : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPEquipmentTimeCardStatusAttribute.onHold>
  {
    public onHold()
      : base("H")
    {
    }
  }

  public sealed class pendingApproval : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPEquipmentTimeCardStatusAttribute.pendingApproval>
  {
    public pendingApproval()
      : base("O")
    {
    }
  }

  public sealed class approved : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPEquipmentTimeCardStatusAttribute.approved>
  {
    public approved()
      : base("A")
    {
    }
  }

  public sealed class rejected : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPEquipmentTimeCardStatusAttribute.rejected>
  {
    public rejected()
      : base("C")
    {
    }
  }

  public sealed class released : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPEquipmentTimeCardStatusAttribute.released>
  {
    public released()
      : base("R")
    {
    }
  }
}
