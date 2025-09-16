// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.RQ;

public class RQType
{
  public const 
  #nullable disable
  string RequestItem = "I";
  public const string Requisition = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("I", "Request"),
        PXStringListAttribute.Pair("R", "Requisition")
      })
    {
    }
  }

  public class requestItem : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQType.requestItem>
  {
    public requestItem()
      : base("I")
    {
    }
  }

  public class requisition : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQType.requisition>
  {
    public requisition()
      : base("R")
    {
    }
  }
}
