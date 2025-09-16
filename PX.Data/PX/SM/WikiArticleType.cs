// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiArticleType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class WikiArticleType
{
  public const int Wiki = 0;
  public const int DeletedItems = 1;
  public const int Article = 10;
  public const int Announcement = 11;
  public const int Notification = 12;
  public const int SitePage = 13;
  public const int KBArticle = 14;

  public class ListAttribute : PXIntListAttribute
  {
    public ListAttribute()
      : base(new int[4]{ 10, 11, 13, 14 }, new string[4]
      {
        "Article",
        "Announcement",
        "SitePage",
        "KB Article"
      })
    {
    }
  }

  public class wiki : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  WikiArticleType.wiki>
  {
    public wiki()
      : base(0)
    {
    }
  }

  public class deletedItems : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  WikiArticleType.deletedItems>
  {
    public deletedItems()
      : base(1)
    {
    }
  }

  public class article : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  WikiArticleType.article>
  {
    public article()
      : base(10)
    {
    }
  }

  public class announcement : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  WikiArticleType.announcement>
  {
    public announcement()
      : base(11)
    {
    }
  }

  public class notification : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  WikiArticleType.notification>
  {
    public notification()
      : base(12)
    {
    }
  }

  public class sitePage : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  WikiArticleType.sitePage>
  {
    public sitePage()
      : base(13)
    {
    }
  }

  public class kBArticle : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  WikiArticleType.kBArticle>
  {
    public kBArticle()
      : base(14)
    {
    }
  }
}
