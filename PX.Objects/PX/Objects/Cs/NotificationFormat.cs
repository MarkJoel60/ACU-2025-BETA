// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.NotificationFormat
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CS;

public class NotificationFormat
{
  public const 
  #nullable disable
  string Html = "H";
  public const string Excel = "E";
  public const string PDF = "P";
  public static NotificationFormat.ListAttribute List = new NotificationFormat.ListAttribute();
  public static NotificationFormat.ListAttribute ReportList = (NotificationFormat.ListAttribute) new NotificationFormat.ReportListAttribute();
  public static NotificationFormat.ListAttribute TemplateList = (NotificationFormat.ListAttribute) new NotificationFormat.TemplateListAttribute();

  public class pdf : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  NotificationFormat.pdf>
  {
    public pdf()
      : base("P")
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public string[] AllowedValues => this._AllowedValues;

    public string[] AllowedLabels => this._AllowedLabels;

    public ListAttribute()
      : base(new string[3]{ "H", "E", "P" }, new string[3]
      {
        "Html",
        "Excel",
        "PDF"
      })
    {
    }

    protected ListAttribute(string[] values, string[] labels)
      : base(values, labels)
    {
    }
  }

  public class ReportListAttribute : NotificationFormat.ListAttribute
  {
    public ReportListAttribute()
      : base(new string[3]{ "H", "E", "P" }, new string[3]
      {
        "Html",
        "Excel",
        "PDF"
      })
    {
    }
  }

  public class TemplateListAttribute : NotificationFormat.ListAttribute
  {
    public TemplateListAttribute()
      : base(new string[1]{ "H" }, new string[1]{ "Html" })
    {
    }
  }
}
