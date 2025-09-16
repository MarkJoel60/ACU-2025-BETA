// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPGroupTransaction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

public static class EPGroupTransaction
{
  public const 
  #nullable disable
  string DoNotSplit = "N";
  public const string Week = "W";
  public const string Project = "P";
  public const string Employee = "E";
  public const string WeekEmployee = "WE";
  public const string ProjectEmployee = "PE";
  public const string WeekProject = "WP";
  public const string WeekProjectEmployee = "WPE";

  public class ListAttribule : PXStringListAttribute
  {
    public ListAttribule()
      : base(new string[8]
      {
        "N",
        "W",
        "P",
        "E",
        "WE",
        "PE",
        "WP",
        "WPE"
      }, new string[8]
      {
        "Do Not Split",
        "Week",
        "Project",
        "Employee",
        "Week, Employee",
        "Project, Employee",
        "Week, Employee",
        "Week, Project, Employee"
      })
    {
    }
  }

  public class doNotSplit : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPGroupTransaction.doNotSplit>
  {
    public doNotSplit()
      : base("N")
    {
    }
  }

  public class week : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPGroupTransaction.week>
  {
    public week()
      : base("W")
    {
    }
  }

  public class project : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPGroupTransaction.project>
  {
    public project()
      : base("P")
    {
    }
  }

  public class employee : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPGroupTransaction.employee>
  {
    public employee()
      : base("E")
    {
    }
  }

  public class weekEmployee : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPGroupTransaction.weekEmployee>
  {
    public weekEmployee()
      : base("WE")
    {
    }
  }

  public class projectEmployee : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPGroupTransaction.projectEmployee>
  {
    public projectEmployee()
      : base("PE")
    {
    }
  }

  public class weekProject : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPGroupTransaction.weekProject>
  {
    public weekProject()
      : base("WP")
    {
    }
  }

  public class weekProjectEmployee : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPGroupTransaction.weekProjectEmployee>
  {
    public weekProjectEmployee()
      : base("WPE")
    {
    }
  }
}
