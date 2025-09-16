// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ManageBudgetDialog
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
public class ManageBudgetDialog : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsUnicode = true)]
  [PXUIField(Enabled = false)]
  public virtual 
  #nullable disable
  string Message { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("R")]
  [ManageBudgetDialog.method.List]
  [PXUIField(DisplayName = "Select Action")]
  public virtual string Method { get; set; }

  public abstract class method : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ManageBudgetDialog.method>
  {
    public const string RollbackBudget = "R";
    public const string ConvertBudget = "C";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "R", "C" }, new string[2]
        {
          "Roll Back to Released Values",
          "Convert Budget Using Current Budget Configuration"
        })
      {
      }
    }

    public class rollbackBudget : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ManageBudgetDialog.method.rollbackBudget>
    {
      public rollbackBudget()
        : base("R")
      {
      }
    }

    public class convertBudget : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ManageBudgetDialog.method.convertBudget>
    {
      public convertBudget()
        : base("C")
      {
      }
    }
  }
}
