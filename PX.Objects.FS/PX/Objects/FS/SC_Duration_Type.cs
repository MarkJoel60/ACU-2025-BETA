// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SC_Duration_Type
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

public abstract class SC_Duration_Type : IBqlField, IBqlOperand
{
  public const 
  #nullable disable
  string MONTH = "M";
  public const string QUARTER = "Q";
  public const string YEAR = "Y";
  public const string CUSTOM = "C";

  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("M", "Month"),
        PXStringListAttribute.Pair("Q", "Quarter"),
        PXStringListAttribute.Pair("Y", "Year"),
        PXStringListAttribute.Pair("C", "Custom (days)")
      })
    {
    }
  }

  public class month : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SC_Duration_Type.month>
  {
    public month()
      : base("M")
    {
    }
  }

  public class quarter : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SC_Duration_Type.quarter>
  {
    public quarter()
      : base("Q")
    {
    }
  }

  public class year : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SC_Duration_Type.year>
  {
    public year()
      : base("Y")
    {
    }
  }

  public class custom : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SC_Duration_Type.custom>
  {
    public custom()
      : base("C")
    {
    }
  }
}
