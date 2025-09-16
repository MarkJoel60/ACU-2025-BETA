// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Time_Cycle_Type
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Time_Cycle_Type : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Time_Cycle_Type().ID_LIST, new ID.Time_Cycle_Type().TX_LIST)
    {
    }
  }

  public class Weekday : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_Time_Cycle_Type.Weekday>
  {
    public Weekday()
      : base("WK")
    {
    }
  }

  public class DayOfMonth : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Time_Cycle_Type.DayOfMonth>
  {
    public DayOfMonth()
      : base("MT")
    {
    }
  }
}
