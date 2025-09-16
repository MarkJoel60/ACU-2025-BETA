// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriodSaveDialog
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
public class FinPeriodSaveDialog : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _MoveDayOfWeek;

  [PXString]
  [PXUIField(Enabled = false)]
  public virtual 
  #nullable disable
  string Message { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("Y")]
  [FinPeriodSaveDialog.method.List]
  [PXUIField(DisplayName = "Update Method")]
  public virtual string Method { get; set; }

  [PXBool]
  [PXUnboundDefault(true)]
  [PXUIField(DisplayName = "Move Start Date of Financial Period", Enabled = true, Required = false, Visible = false)]
  public virtual bool? MoveDayOfWeek
  {
    get => this._MoveDayOfWeek;
    set => this._MoveDayOfWeek = value;
  }

  [PXString]
  [PXDefault("the start date of the next financial year will be moved to {0}")]
  [PXUIField(Enabled = false, Required = false)]
  public virtual string MethodDescription { get; set; }

  public abstract class method : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FinPeriodSaveDialog.method>
  {
    public const string UpdateNextYearStart = "N";
    public const string UpdateFinYearSetup = "Y";
    public const string ExtendLastPeriod = "E";
    public const string ShortenLastPeriod = "S";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[3]{ "N", "Y", "E" }, new string[3]
        {
          "Modify Start Date of Next Year",
          "Modify Financial Year Settings",
          "Extend Last Period"
        })
      {
      }
    }

    public class updateNextYearStart : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FinPeriodSaveDialog.method.updateNextYearStart>
    {
      public updateNextYearStart()
        : base("N")
      {
      }
    }

    public class updateFinYearSetup : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FinPeriodSaveDialog.method.updateFinYearSetup>
    {
      public updateFinYearSetup()
        : base("Y")
      {
      }
    }

    public class extendLastPeriod : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FinPeriodSaveDialog.method.extendLastPeriod>
    {
      public extendLastPeriod()
        : base("E")
      {
      }
    }

    public class shortenLastPeriod : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FinPeriodSaveDialog.method.shortenLastPeriod>
    {
      public shortenLastPeriod()
        : base("S")
      {
      }
    }
  }

  public abstract class moveDayOfWeek : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FinPeriodSaveDialog.moveDayOfWeek>
  {
  }
}
