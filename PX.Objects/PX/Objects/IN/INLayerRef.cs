// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLayerRef
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN;

public class INLayerRef
{
  public const 
  #nullable disable
  string ZZZ = "ZZZ";
  public const string Oversold = "OVERSOLD";

  public class zzz : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INLayerRef.zzz>
  {
    public zzz()
      : base("ZZZ")
    {
    }
  }

  public class oversold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INLayerRef.oversold>
  {
    public oversold()
      : base("OVERSOLD")
    {
    }
  }
}
