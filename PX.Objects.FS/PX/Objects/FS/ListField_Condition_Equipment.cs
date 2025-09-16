// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Condition_Equipment
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Condition_Equipment : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Condition().ID_LIST, new ID.Condition().TX_LIST)
    {
    }
  }

  public class New : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_Condition_Equipment.New>
  {
    public New()
      : base("N")
    {
    }
  }

  public class Used : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Condition_Equipment.Used>
  {
    public Used()
      : base("U")
    {
    }
  }
}
