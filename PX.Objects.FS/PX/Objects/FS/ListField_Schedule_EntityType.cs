// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Schedule_EntityType
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Schedule_EntityType : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Schedule_EntityType().ID_LIST, new ID.Schedule_EntityType().TX_LIST)
    {
    }
  }

  public class Contract : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_Schedule_EntityType.Contract>
  {
    public Contract()
      : base("C")
    {
    }
  }

  public class Employee : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Schedule_EntityType.Employee>
  {
    public Employee()
      : base("E")
    {
    }
  }
}
