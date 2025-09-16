// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.DAC.ReportParameters.APRetainageReportParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AP.DAC.ReportParameters;

public class APRetainageReportParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [APRetainageReportParameters.format.List]
  [PXDBString(2)]
  [PXUIField(DisplayName = "Format", Visibility = PXUIVisibility.SelectorVisible)]
  public 
  #nullable disable
  string Format { get; set; }

  [APActiveProject]
  public int? ProjectID { get; set; }

  public abstract class format : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRetainageReportParameters.format>
  {
    public class ListAttribute : PXStringListAttribute
    {
      public const string Summary = "S";
      public const string Details = "D";
      public const string DetailsRetainage = "DR";

      public ListAttribute()
        : base(APRetainageReportParameters.format.ListAttribute.GetAllowedValues(), APRetainageReportParameters.format.ListAttribute.GetAllowedLabels())
      {
      }

      public static string[] GetAllowedValues()
      {
        List<string> stringList = new List<string>()
        {
          "S",
          "D"
        };
        if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.retainage>())
          stringList.Add("DR");
        return stringList.ToArray();
      }

      public static string[] GetAllowedLabels()
      {
        List<string> stringList = new List<string>()
        {
          "Summary",
          "Detailed"
        };
        if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.retainage>())
          stringList.Add("Detailed with Retainage");
        return stringList.ToArray();
      }
    }
  }

  public abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APRetainageReportParameters.projectID>
  {
  }
}
