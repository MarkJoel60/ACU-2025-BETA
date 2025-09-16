// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BudgetDistributeFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL;

[Serializable]
public class BudgetDistributeFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(1, IsFixed = true)]
  [PXDefault("E")]
  [BudgetDistributeFilter.method.List]
  [PXUIField(DisplayName = "Distribution Method")]
  public virtual 
  #nullable disable
  string Method { get; set; }

  [PXUIField(DisplayName = "Apply to All Articles in This Node")]
  [PXBool]
  public virtual bool? ApplyToAll { get; set; }

  [PXUIField(DisplayName = "Apply to Subarticles", Enabled = false)]
  [PXBool]
  public virtual bool? ApplyToSubGroups { get; set; }

  public abstract class method : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BudgetDistributeFilter.method>
  {
    public const string Evenly = "E";
    public const string PreviousYear = "P";
    public const string ComparedValues = "C";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[3]{ "E", "P", "C" }, new string[3]
        {
          "Evenly",
          "Proportionally to the Previous Year",
          "Proportionally to Compared Values"
        })
      {
      }
    }

    public class NonComparedListAttribute : PXStringListAttribute
    {
      public NonComparedListAttribute()
        : base(new string[2]{ "E", "P" }, new string[2]
        {
          "Evenly",
          "Proportionally to the Previous Year"
        })
      {
      }
    }

    public class evenly : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    BudgetDistributeFilter.method.evenly>
    {
      public evenly()
        : base("E")
      {
      }
    }

    public class previousYear : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      BudgetDistributeFilter.method.previousYear>
    {
      public previousYear()
        : base("P")
      {
      }
    }

    public class comparedValues : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      BudgetDistributeFilter.method.comparedValues>
    {
      public comparedValues()
        : base("C")
      {
      }
    }
  }

  public abstract class applyToAll : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BudgetDistributeFilter.applyToAll>
  {
  }

  public abstract class applyToSubGroups : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BudgetDistributeFilter.applyToSubGroups>
  {
  }
}
