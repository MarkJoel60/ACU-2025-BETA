// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestClassBudget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.RQ;

public class RQRequestClassBudget
{
  public const int None = 0;
  public const int Warning = 1;
  public const int Error = 2;

  public class ListAttribute : PXIntListAttribute
  {
    public ListAttribute()
      : base(new Tuple<int, string>[3]
      {
        PXIntListAttribute.Pair(0, "None"),
        PXIntListAttribute.Pair(1, "Warning"),
        PXIntListAttribute.Pair(2, "Error")
      })
    {
    }
  }

  public class none : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  RQRequestClassBudget.none>
  {
    public none()
      : base(0)
    {
    }
  }

  public class warning : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  RQRequestClassBudget.warning>
  {
    public warning()
      : base(1)
    {
    }
  }

  public class error : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  RQRequestClassBudget.error>
  {
    public error()
      : base(2)
    {
    }
  }
}
