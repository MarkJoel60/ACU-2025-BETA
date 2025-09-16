// Decompiled with JetBrains decompiler
// Type: PX.SM.SenderDisplayNameSourceAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class SenderDisplayNameSourceAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string Account = "A";
  public const string Employee = "E";

  public SenderDisplayNameSourceAttribute()
    : base(new string[2]{ "A", "E" }, new string[2]
    {
      "Email Account Settings",
      "Employee Name"
    })
  {
  }

  public class account : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SenderDisplayNameSourceAttribute.account>
  {
    public account()
      : base("A")
    {
    }
  }

  public class employee : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SenderDisplayNameSourceAttribute.employee>
  {
    public employee()
      : base("E")
    {
    }
  }
}
