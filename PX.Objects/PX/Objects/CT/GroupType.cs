// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.GroupType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CT;

public static class GroupType
{
  public const 
  #nullable disable
  string Contract = "CONTRACT";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[1]{ "CONTRACT" }, new string[1]
      {
        "Contract"
      })
    {
    }
  }

  public class ContractType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  GroupType.ContractType>
  {
    public ContractType()
      : base("CONTRACT")
    {
    }
  }
}
