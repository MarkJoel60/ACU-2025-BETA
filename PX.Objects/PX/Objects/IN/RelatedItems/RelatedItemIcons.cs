// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.RelatedItemIcons
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN.RelatedItems;

public static class RelatedItemIcons
{
  public const 
  #nullable disable
  string Any = "~/Icons/dollarGreen.svg";

  public class any : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RelatedItemIcons.any>
  {
    public any()
      : base("~/Icons/dollarGreen.svg")
    {
    }
  }

  public static class Required
  {
    public const string CrossSell = "~/Icons/dollarRed.svg";
    public const string Substitution = "~/Icons/switchRed.svg";
    public const string Other = "~/Icons/dollarRed.svg";

    public class crossSell : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      RelatedItemIcons.Required.crossSell>
    {
      public crossSell()
        : base("~/Icons/dollarRed.svg")
      {
      }
    }

    public class substitution : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      RelatedItemIcons.Required.substitution>
    {
      public substitution()
        : base("~/Icons/switchRed.svg")
      {
      }
    }

    public class other : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    RelatedItemIcons.Required.other>
    {
      public other()
        : base("~/Icons/dollarRed.svg")
      {
      }
    }
  }
}
