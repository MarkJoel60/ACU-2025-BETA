// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ActivityMajorStatusesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
[Obsolete]
public class ActivityMajorStatusesAttribute : PXIntListAttribute
{
  public const int _JUST_CREATED = -1;
  public const int _OPEN = 0;
  public const int _PREPROCESS = 2;
  public const int _PROCESSING = 3;
  public const int _PROCESSED = 4;
  public const int _FAILED = 5;
  public const int _CANCELED = 6;
  public const int _COMPLETED = 7;
  public const int _DELETED = 8;
  public const int _RELEASED = 9;

  [Obsolete]
  public ActivityMajorStatusesAttribute()
    : base(new int[10]{ -1, 0, 2, 3, 4, 5, 6, 7, 8, 9 }, new string[10]
    {
      "Just Created",
      "Open",
      "Preprocess",
      "Processing",
      "Processed",
      "Failed",
      "Canceled",
      "Completed",
      "Deleted",
      "Released"
    })
  {
  }

  public class open : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  ActivityMajorStatusesAttribute.open>
  {
    public open()
      : base(0)
    {
    }
  }

  public class preProcess : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ActivityMajorStatusesAttribute.preProcess>
  {
    public preProcess()
      : base(2)
    {
    }
  }

  public class processing : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ActivityMajorStatusesAttribute.processing>
  {
    public processing()
      : base(3)
    {
    }
  }

  public class processed : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ActivityMajorStatusesAttribute.processed>
  {
    public processed()
      : base(4)
    {
    }
  }

  public class failed : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ActivityMajorStatusesAttribute.failed>
  {
    public failed()
      : base(5)
    {
    }
  }

  public class canceled : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ActivityMajorStatusesAttribute.canceled>
  {
    public canceled()
      : base(6)
    {
    }
  }

  public class completed : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ActivityMajorStatusesAttribute.completed>
  {
    public completed()
      : base(7)
    {
    }
  }

  public class deleted : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ActivityMajorStatusesAttribute.deleted>
  {
    public deleted()
      : base(8)
    {
    }
  }

  public class released : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ActivityMajorStatusesAttribute.released>
  {
    public released()
      : base(9)
    {
    }
  }
}
