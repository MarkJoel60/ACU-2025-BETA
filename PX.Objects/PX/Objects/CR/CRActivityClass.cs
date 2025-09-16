// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRActivityClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR;

public class CRActivityClass : PXIntListAttribute
{
  public const int Task = 0;
  public const int Event = 1;
  public const int Activity = 2;
  public const int Email = 4;
  public const int EmailRouting = -2;
  public const int OldEmails = -3;

  public CRActivityClass()
    : base(new int[6]{ 0, 1, 2, 4, -2, -3 }, new string[6]
    {
      nameof (Task),
      nameof (Event),
      nameof (Activity),
      nameof (Email),
      "Email Response",
      nameof (Email)
    })
  {
  }

  public class task : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  CRActivityClass.task>
  {
    public task()
      : base(0)
    {
    }
  }

  public class events : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  CRActivityClass.events>
  {
    public events()
      : base(1)
    {
    }
  }

  public class activity : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  CRActivityClass.activity>
  {
    public activity()
      : base(2)
    {
    }
  }

  public class email : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  CRActivityClass.email>
  {
    public email()
      : base(4)
    {
    }
  }

  public class emailRouting : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  CRActivityClass.emailRouting>
  {
    public emailRouting()
      : base(-2)
    {
    }
  }

  public class oldEmails : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  CRActivityClass.oldEmails>
  {
    public oldEmails()
      : base(-3)
    {
    }
  }
}
