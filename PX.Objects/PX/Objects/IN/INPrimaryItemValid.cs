// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPrimaryItemValid
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INPrimaryItemValid
{
  public const 
  #nullable disable
  string PrimaryNothing = "N";
  public const string PrimaryItemError = "I";
  public const string PrimaryItemClassError = "C";
  public const string PrimaryItemWarning = "X";
  public const string PrimaryItemClassWarning = "Y";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[5]
      {
        PXStringListAttribute.Pair("N", "No Validation"),
        PXStringListAttribute.Pair("X", "Primary Item Warning"),
        PXStringListAttribute.Pair("I", "Primary Item Error"),
        PXStringListAttribute.Pair("Y", "Primary Item Class Warning"),
        PXStringListAttribute.Pair("C", "Primary Item Class Error")
      })
    {
    }
  }

  public class primaryNothing : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INPrimaryItemValid.primaryNothing>
  {
    public primaryNothing()
      : base("N")
    {
    }
  }

  public class primaryItem : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPrimaryItemValid.primaryItem>
  {
    public primaryItem()
      : base("I")
    {
    }
  }

  public class primaryItemClass : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INPrimaryItemValid.primaryItemClass>
  {
    public primaryItemClass()
      : base("C")
    {
    }
  }

  public class primaryItemWarn : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INPrimaryItemValid.primaryItemWarn>
  {
    public primaryItemWarn()
      : base("X")
    {
    }
  }

  public class primaryItemClassWarn : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INPrimaryItemValid.primaryItemClassWarn>
  {
    public primaryItemClassWarn()
      : base("Y")
    {
    }
  }
}
