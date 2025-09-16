// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_AppointmentValidation
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_AppointmentValidation : IBqlField, IBqlOperand
{
  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new ID.ValidationType().ID_LIST, new ID.ValidationType().TX_LIST)
    {
    }
  }

  public class NOT_VALIDATE : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_AppointmentValidation.NOT_VALIDATE>
  {
    public NOT_VALIDATE()
      : base("N")
    {
    }
  }

  public class WARN : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_AppointmentValidation.WARN>
  {
    public WARN()
      : base("W")
    {
    }
  }

  public class PREVENT : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_AppointmentValidation.PREVENT>
  {
    public PREVENT()
      : base("D")
    {
    }
  }
}
