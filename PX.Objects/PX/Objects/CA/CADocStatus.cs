// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CADocStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA;

/// <exclude />
public class CADocStatus
{
  public const 
  #nullable disable
  string Balanced = "B";
  public const string Closed = "C";
  public const string Hold = "H";
  public const string Voided = "V";
  public const string Rejected = "J";
  public const string Pending = "P";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[6]{ "B", "C", "H", "V", "P", "J" }, new string[6]
      {
        "Balanced",
        "Closed",
        "On Hold",
        "Voided",
        "Pending Approval",
        "Rejected"
      })
    {
    }
  }

  public class balanced : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADocStatus.balanced>
  {
    public balanced()
      : base("B")
    {
    }
  }

  public class closed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADocStatus.closed>
  {
    public closed()
      : base("C")
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADocStatus.hold>
  {
    public hold()
      : base("H")
    {
    }
  }

  public class voided : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADocStatus.voided>
  {
    public voided()
      : base("V")
    {
    }
  }

  public class rejected : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADocStatus.rejected>
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
  CADocStatus.pending>
  {
    public pending()
      : base("P")
    {
    }
  }
}
