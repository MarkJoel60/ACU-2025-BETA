// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_WeekDays
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_WeekDays : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.WeekDays().ID_LIST, new ID.WeekDays().TX_LIST)
    {
    }
  }

  public class ANY : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_WeekDays.ANY>
  {
    public ANY()
      : base("NT")
    {
    }
  }

  public class SUNDAY : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_WeekDays.SUNDAY>
  {
    public SUNDAY()
      : base("SU")
    {
    }
  }

  public class MONDAY : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_WeekDays.MONDAY>
  {
    public MONDAY()
      : base("MO")
    {
    }
  }

  public class TUESDAY : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_WeekDays.TUESDAY>
  {
    public TUESDAY()
      : base("TU")
    {
    }
  }

  public class WEDNESDAY : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_WeekDays.WEDNESDAY>
  {
    public WEDNESDAY()
      : base("WE")
    {
    }
  }

  public class THURSDAY : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_WeekDays.THURSDAY>
  {
    public THURSDAY()
      : base("TH")
    {
    }
  }

  public class FRIDAY : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_WeekDays.FRIDAY>
  {
    public FRIDAY()
      : base("FR")
    {
    }
  }

  public class SATURDAY : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_WeekDays.SATURDAY>
  {
    public SATURDAY()
      : base("SA")
    {
    }
  }
}
