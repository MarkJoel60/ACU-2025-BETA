// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CATransferStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA;

/// <summary>Contains statuses of the transfer.</summary>
public class CATransferStatus
{
  /// <summary>The transfer is balanced and can be released.</summary>
  public const 
  #nullable disable
  string Balanced = "B";
  /// <summary>
  /// The transfer is a draft only; the actual transfer of funds has not been initiated.
  /// </summary>
  public const string Hold = "H";
  /// <summary>The transfer has been released.</summary>
  public const string Released = "R";
  /// <summary>The transfer has been rejected.</summary>
  public const string Rejected = "J";
  /// <summary>The transfer is pending approval.</summary>
  public const string Pending = "P";
  public static readonly string[] Values = new string[5]
  {
    "B",
    "H",
    "R",
    "P",
    "J"
  };
  public static readonly string[] Labels = new string[5]
  {
    nameof (Balanced),
    "On Hold",
    nameof (Released),
    "Pending Approval",
    nameof (Rejected)
  };

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(CATransferStatus.Values, CATransferStatus.Labels)
    {
    }
  }

  public class balanced : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CATransferStatus.balanced>
  {
    public balanced()
      : base("B")
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CATransferStatus.hold>
  {
    public hold()
      : base("H")
    {
    }
  }

  public class released : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CATransferStatus.released>
  {
    public released()
      : base("R")
    {
    }
  }

  public class rejected : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CATransferStatus.rejected>
  {
    public rejected()
      : base("J")
    {
    }
  }

  public class pending : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CATransferStatus.pending>
  {
    public pending()
      : base("P")
    {
    }
  }
}
