// Decompiled with JetBrains decompiler
// Type: PX.SM.AUEventSubScriberType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUEventSubScriberType
{
  public const 
  #nullable disable
  string CreateEmail = "E";
  public const string CreateActivity = "A";
  public const string Trigger = "T";
  public const string None = "N";

  public class createEmail : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUEventSubScriberType.createEmail>
  {
    public createEmail()
      : base("E")
    {
    }
  }

  public class createActivity : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AUEventSubScriberType.createActivity>
  {
    public createActivity()
      : base("A")
    {
    }
  }

  public class trigger : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUEventSubScriberType.trigger>
  {
    public trigger()
      : base("T")
    {
    }
  }

  public class none : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUEventSubScriberType.none>
  {
    public none()
      : base("N")
    {
    }
  }
}
