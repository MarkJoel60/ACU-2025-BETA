// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Contract_ExpirationType
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Contract_ExpirationType : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Contract_ExpirationType().ID_LIST, new ID.Contract_ExpirationType().TX_LIST)
    {
    }
  }

  public class Expiring : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_Contract_ExpirationType.Expiring>
  {
    public Expiring()
      : base("E")
    {
    }
  }

  public class Unlimited : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Contract_ExpirationType.Unlimited>
  {
    public Unlimited()
      : base("U")
    {
    }
  }

  public class Renewable : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Contract_ExpirationType.Renewable>
  {
    public Renewable()
      : base("R")
    {
    }
  }
}
