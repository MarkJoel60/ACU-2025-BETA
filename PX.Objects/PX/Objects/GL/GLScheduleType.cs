// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLScheduleType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.GL;

public class GLScheduleType
{
  public const 
  #nullable disable
  string Daily = "D";
  public const string Weekly = "W";
  public const string Monthly = "M";
  public const string Periodically = "P";

  public class CustomListAttribute(string[] allowedValues, string[] allowedLabels) : 
    PXStringListAttribute(allowedValues, allowedLabels)
  {
    public string[] AllowedValues => this._AllowedValues;

    public string[] AllowedLabels => this._AllowedLabels;
  }

  public class ListAttribute : GLScheduleType.CustomListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "D", "W", "M", "P" }, new string[4]
      {
        "Daily",
        "Weekly",
        "Monthly",
        "By Financial Period"
      })
    {
    }
  }

  public class daily : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  GLScheduleType.daily>
  {
    public daily()
      : base("D")
    {
    }
  }

  public class weekly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  GLScheduleType.weekly>
  {
    public weekly()
      : base("W")
    {
    }
  }

  public class monthly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  GLScheduleType.monthly>
  {
    public monthly()
      : base("M")
    {
    }
  }

  public class periodically : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  GLScheduleType.periodically>
  {
    public periodically()
      : base("P")
    {
    }
  }
}
