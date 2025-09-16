// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiPageStatus
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.TM;

#nullable enable
namespace PX.SM;

public class WikiPageStatus
{
  public const int Hold = 0;
  public const int Pending = 1;
  public const int Rejected = 2;
  public const int Published = 3;
  public const int Deleted = 4;

  public class ListAttribute : PXIntListAttribute, IStatusPath
  {
    public ListAttribute()
      : base(new int[5]{ 0, 1, 2, 3, 4 }, new string[5]
      {
        "Hold",
        "Pending",
        "Rejected",
        "Published",
        "Deleted"
      })
    {
    }

    #nullable disable
    object[] IStatusPath.NextAvalable(object currentStatus)
    {
      switch ((int) currentStatus)
      {
        case 1:
          return new object[2]{ (object) 3, (object) 2 };
        case 2:
          return new object[1]{ (object) 0 };
        default:
          return (object[]) null;
      }
    }

    object[] IStatusPath.PrevAvalable(object currentStatus)
    {
      switch ((int) currentStatus)
      {
        case 1:
          return new object[1]{ (object) 0 };
        case 3:
          return new object[1]{ (object) 0 };
        default:
          return (object[]) null;
      }
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  WikiPageStatus.hold>
  {
    public hold()
      : base(0)
    {
    }
  }

  public class pending : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  WikiPageStatus.pending>
  {
    public pending()
      : base(1)
    {
    }
  }

  public class rejected : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  WikiPageStatus.rejected>
  {
    public rejected()
      : base(2)
    {
    }
  }

  public class published : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  WikiPageStatus.published>
  {
    public published()
      : base(3)
    {
    }
  }

  public class deleted : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  WikiPageStatus.deleted>
  {
    public deleted()
      : base(4)
    {
    }
  }
}
