// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BatchStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.GL;

public class BatchStatus
{
  public const 
  #nullable disable
  string Hold = "H";
  public const string Balanced = "B";
  public const string Unposted = "U";
  public const string Posted = "P";
  public const string Completed = "C";
  public const string Voided = "V";
  public const string Released = "R";
  public const string PartiallyReleased = "Q";
  public const string Scheduled = "S";
  public const string PendingApproval = "D";
  public const string Rejected = "J";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[11]
      {
        "H",
        "B",
        "U",
        "P",
        "C",
        "V",
        "R",
        "Q",
        "S",
        "D",
        "J"
      }, new string[11]
      {
        "On Hold",
        "Balanced",
        "Unposted",
        "Posted",
        "Completed",
        "Voided",
        "Released",
        "Partially Released",
        "Scheduled",
        "Pending Approval",
        "Rejected"
      })
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchStatus.hold>
  {
    public hold()
      : base("H")
    {
    }
  }

  public class balanced : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchStatus.balanced>
  {
    public balanced()
      : base("B")
    {
    }
  }

  public class unposted : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchStatus.unposted>
  {
    public unposted()
      : base("U")
    {
    }
  }

  public class posted : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchStatus.posted>
  {
    public posted()
      : base("P")
    {
    }
  }

  public class completed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchStatus.completed>
  {
    public completed()
      : base("C")
    {
    }
  }

  public class voided : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchStatus.voided>
  {
    public voided()
      : base("V")
    {
    }
  }

  public class scheduled : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchStatus.scheduled>
  {
    public scheduled()
      : base("S")
    {
    }
  }

  public class pendingApproval : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchStatus.pendingApproval>
  {
    public pendingApproval()
      : base("D")
    {
    }
  }

  public class rejected : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchStatus.rejected>
  {
    public rejected()
      : base("J")
    {
    }
  }
}
