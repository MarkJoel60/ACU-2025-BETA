// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_ScheduleType
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_ScheduleType : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.ScheduleType().ID_LIST, new ID.ScheduleType().TX_LIST)
    {
    }
  }

  public class Availability : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_ScheduleType.Availability>
  {
    public Availability()
      : base("A")
    {
    }
  }

  public class Unavailability : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_ScheduleType.Unavailability>
  {
    public Unavailability()
      : base("U")
    {
    }
  }
}
