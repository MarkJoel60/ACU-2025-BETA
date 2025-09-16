// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Period_Appointment
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Period_Appointment : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.PeriodType().ID_LIST, new ID.PeriodType().TX_LIST)
    {
    }
  }

  public class Day : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_Period_Appointment.Day>
  {
    public Day()
      : base("D")
    {
    }
  }

  public class Week : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Period_Appointment.Week>
  {
    public Week()
      : base("W")
    {
    }
  }

  public class Month : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Period_Appointment.Month>
  {
    public Month()
      : base("M")
    {
    }
  }
}
