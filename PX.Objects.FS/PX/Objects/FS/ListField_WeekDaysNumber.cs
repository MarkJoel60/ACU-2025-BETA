// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_WeekDaysNumber
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_WeekDaysNumber : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXIntListAttribute
  {
    public ListAtrribute()
      : base(new ID.WeekDaysNumber().ID_LIST, new ID.WeekDaysNumber().TX_LIST)
    {
    }
  }

  public class SUNDAY : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  ListField_WeekDaysNumber.SUNDAY>
  {
    public SUNDAY()
      : base(0)
    {
    }
  }

  public class MONDAY : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ListField_WeekDaysNumber.MONDAY>
  {
    public MONDAY()
      : base(1)
    {
    }
  }

  public class TUESDAY : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ListField_WeekDaysNumber.TUESDAY>
  {
    public TUESDAY()
      : base(2)
    {
    }
  }

  public class WEDNESDAY : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ListField_WeekDaysNumber.WEDNESDAY>
  {
    public WEDNESDAY()
      : base(3)
    {
    }
  }

  public class THURSDAY : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ListField_WeekDaysNumber.THURSDAY>
  {
    public THURSDAY()
      : base(4)
    {
    }
  }

  public class FRIDAY : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ListField_WeekDaysNumber.FRIDAY>
  {
    public FRIDAY()
      : base(5)
    {
    }
  }

  public class SATURDAY : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ListField_WeekDaysNumber.SATURDAY>
  {
    public SATURDAY()
      : base(6)
    {
    }
  }
}
