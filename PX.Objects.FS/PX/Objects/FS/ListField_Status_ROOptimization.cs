// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Status_ROOptimization
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Status_ROOptimization : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Status_ROOptimization().ID_LIST, new ID.Status_ROOptimization().TX_LIST)
    {
    }
  }

  public class NotOptimized : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_Status_ROOptimization.NotOptimized>
  {
    public NotOptimized()
      : base("NO")
    {
    }
  }

  public class Optimized : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Status_ROOptimization.Optimized>
  {
    public Optimized()
      : base("OP")
    {
    }
  }

  public class NotAble : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Status_ROOptimization.NotAble>
  {
    public NotAble()
      : base("NA")
    {
    }
  }

  public class AddressError : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Status_ROOptimization.AddressError>
  {
    public AddressError()
      : base("AE")
    {
    }
  }
}
