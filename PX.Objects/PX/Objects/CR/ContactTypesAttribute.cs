// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ContactTypesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public class ContactTypesAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string Person = "PN";
  public const string SalesPerson = "SP";
  public const string BAccountProperty = "AP";
  public const string Employee = "EP";
  public const string Lead = "LD";
  public const string Broker = "BR";

  public ContactTypesAttribute()
    : base(new string[6]
    {
      "PN",
      "SP",
      "AP",
      "EP",
      "LD",
      "BR"
    }, new string[6]
    {
      "Contact",
      "Sales Person",
      "Business Account",
      nameof (Employee),
      nameof (Lead),
      nameof (Broker)
    })
  {
  }

  public class person : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ContactTypesAttribute.person>
  {
    public person()
      : base("PN")
    {
    }
  }

  public class bAccountProperty : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ContactTypesAttribute.bAccountProperty>
  {
    public bAccountProperty()
      : base("AP")
    {
    }
  }

  public class salesPerson : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ContactTypesAttribute.salesPerson>
  {
    public salesPerson()
      : base("SP")
    {
    }
  }

  public class employee : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ContactTypesAttribute.employee>
  {
    public employee()
      : base("EP")
    {
    }
  }

  public class lead : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ContactTypesAttribute.lead>
  {
    public lead()
      : base("LD")
    {
    }
  }

  public class broker : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ContactTypesAttribute.broker>
  {
    public broker()
      : base("BR")
    {
    }
  }

  public class Priority
  {
    public class bAccountPriority : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      ContactTypesAttribute.Priority.bAccountPriority>
    {
      public bAccountPriority()
        : base(-10)
      {
      }
    }

    public class salesPersonPriority : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      ContactTypesAttribute.Priority.salesPersonPriority>
    {
      public salesPersonPriority()
        : base(-5)
      {
      }
    }

    public class employeePriority : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      ContactTypesAttribute.Priority.employeePriority>
    {
      public employeePriority()
        : base(-1)
      {
      }
    }

    public class personPriority : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      ContactTypesAttribute.Priority.personPriority>
    {
      public personPriority()
        : base(0)
      {
      }
    }

    public class leadPriority : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      ContactTypesAttribute.Priority.leadPriority>
    {
      public leadPriority()
        : base(10)
      {
      }
    }
  }
}
