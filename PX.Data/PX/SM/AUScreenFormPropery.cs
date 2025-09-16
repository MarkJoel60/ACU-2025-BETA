// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenFormPropery
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUScreenFormPropery
{
  public const 
  #nullable disable
  string Customized = "Customized";
  public const string Caption = "Caption";
  public const string Visible = "IsVisible";

  public class customized : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenFormPropery.customized>
  {
    public customized()
      : base("Customized")
    {
    }
  }

  public class caption : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenFormPropery.caption>
  {
    public caption()
      : base("Caption")
    {
    }
  }

  public class visible : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenFormPropery.visible>
  {
    public visible()
      : base("IsVisible")
    {
    }
  }
}
