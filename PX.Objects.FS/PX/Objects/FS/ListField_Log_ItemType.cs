// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Log_ItemType
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Log_ItemType : IBqlField, IBqlOperand
{
  public const 
  #nullable disable
  string Travel = "TR";
  public const string Service = "SE";
  public const string NonStock = "NS";
  public const string Staff = "SA";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("TR", "Travel"),
        PXStringListAttribute.Pair("SA", "Staff and Service (If Any)"),
        PXStringListAttribute.Pair("SE", "Service"),
        PXStringListAttribute.Pair("NS", "Non-Stock")
      })
    {
    }
  }

  public class travel : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Log_ItemType.travel>
  {
    public travel()
      : base("TR")
    {
    }
  }

  public class service : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Log_ItemType.service>
  {
    public service()
      : base("SE")
    {
    }
  }

  public class nonStock : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Log_ItemType.nonStock>
  {
    public nonStock()
      : base("NS")
    {
    }
  }

  public class staff : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Log_ItemType.staff>
  {
    public staff()
      : base("SA")
    {
    }
  }
}
