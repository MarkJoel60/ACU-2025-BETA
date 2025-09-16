// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.DrCr
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.GL;

public class DrCr
{
  public const 
  #nullable disable
  string Debit = "D";
  public const string Credit = "C";

  public class debit : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DrCr.debit>
  {
    public debit()
      : base("D")
    {
    }
  }

  public class credit : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DrCr.credit>
  {
    public credit()
      : base("C")
    {
    }
  }
}
