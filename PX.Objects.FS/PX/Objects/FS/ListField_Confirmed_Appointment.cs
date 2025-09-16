// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Confirmed_Appointment
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Confirmed_Appointment : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Confirmed_Appointment().ID_LIST, new ID.Confirmed_Appointment().TX_LIST)
    {
    }
  }

  public class All : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_Confirmed_Appointment.All>
  {
    public All()
      : base("A")
    {
    }
  }

  public class Confirmed : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Confirmed_Appointment.Confirmed>
  {
    public Confirmed()
      : base("C")
    {
    }
  }

  public class Not_Confirmed : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Confirmed_Appointment.Not_Confirmed>
  {
    public Not_Confirmed()
      : base("N")
    {
    }
  }
}
