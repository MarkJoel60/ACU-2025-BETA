// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.PXInvitationStatusAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

/// <summary>List of invitation statuses.</summary>
/// <example>[PXInvitationStatus]</example>
public class PXInvitationStatusAttribute : PXIntListAttribute
{
  public const int NOTINVITED = 0;
  public const int INVITED = 1;
  public const int ACCEPTED = 2;
  public const int REJECTED = 3;
  public const int RESCHEDULED = 4;
  public const int CANCELED = 5;

  public PXInvitationStatusAttribute()
    : base(new int[6]{ 0, 1, 2, 3, 4, 5 }, new string[6]
    {
      "Not invited",
      "Invited",
      "Accepted",
      "Declined",
      "Rescheduled",
      "Canceled"
    })
  {
  }

  public class notinvited : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  PXInvitationStatusAttribute.notinvited>
  {
    public notinvited()
      : base(0)
    {
    }
  }

  public class invited : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  PXInvitationStatusAttribute.invited>
  {
    public invited()
      : base(1)
    {
    }
  }

  public class accepted : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  PXInvitationStatusAttribute.accepted>
  {
    public accepted()
      : base(2)
    {
    }
  }

  public class rejected : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  PXInvitationStatusAttribute.rejected>
  {
    public rejected()
      : base(3)
    {
    }
  }

  public class rescheduled : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  PXInvitationStatusAttribute.rescheduled>
  {
    public rescheduled()
      : base(4)
    {
    }
  }

  public class canceled : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  PXInvitationStatusAttribute.canceled>
  {
    public canceled()
      : base(5)
    {
    }
  }
}
