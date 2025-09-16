// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_SlotLevel_TimeSlot
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_SlotLevel_TimeSlot : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXIntListAttribute
  {
    public ListAtrribute()
      : base(new ID.EmployeeTimeSlotLevel().ID_LIST, new ID.EmployeeTimeSlotLevel().TX_LIST)
    {
    }
  }

  public class Base : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  ListField_SlotLevel_TimeSlot.Base>
  {
    public Base()
      : base(0)
    {
    }
  }

  public class Compress : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ListField_SlotLevel_TimeSlot.Compress>
  {
    public Compress()
      : base(1)
    {
    }
  }
}
