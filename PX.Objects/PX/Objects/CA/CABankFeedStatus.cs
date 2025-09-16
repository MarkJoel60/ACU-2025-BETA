// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA;

public class CABankFeedStatus
{
  public const 
  #nullable disable
  string Active = "A";
  public const string Suspended = "S";
  public const string Disconnected = "D";
  public const string SetupRequired = "R";
  public const string MigrationRequired = "M";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(CABankFeedStatus.ListAttribute.GetStatuses)
    {
    }

    public static (string, string)[] GetStatuses
    {
      get
      {
        return new (string, string)[5]
        {
          ("A", "Active"),
          ("S", "Suspended"),
          ("D", "Disconnected"),
          ("R", "Setup Required"),
          ("M", "Migration Required")
        };
      }
    }
  }

  public class active : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABankFeedStatus.active>
  {
    public active()
      : base("A")
    {
    }
  }

  public class setupRequired : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABankFeedStatus.setupRequired>
  {
    public setupRequired()
      : base("R")
    {
    }
  }

  public class suspended : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABankFeedStatus.suspended>
  {
    public suspended()
      : base("S")
    {
    }
  }

  public class disconnected : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABankFeedStatus.disconnected>
  {
    public disconnected()
      : base("D")
    {
    }
  }
}
