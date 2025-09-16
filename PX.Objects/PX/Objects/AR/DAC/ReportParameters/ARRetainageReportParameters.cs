// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.DAC.ReportParameters.ARRetainageReportParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AR.DAC.ReportParameters;

public class ARRetainageReportParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [ARRetainageReportParameters.Format]
  [PXDBString(2)]
  [PXUIField]
  public 
  #nullable disable
  string Format { get; set; }

  [ARActiveProject]
  public int? ProjectID { get; set; }

  /// <summary>Any state project id</summary>
  [ARAnyStateProject(typeof (Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.isActive, Equal<True>>>>, And<BqlOperand<PMProject.isCompleted, IBqlBool>.IsNotEqual<True>>>>.Or<BqlOperand<Optional<PMProject.isActive>, IBqlBool>.IsNotEqual<True>>>))]
  public int? AnyStateProjectID { get; set; }

  public class FormatAttribute : PXStringListAttribute
  {
    public FormatAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("S", "Summary"),
        PXStringListAttribute.Pair("D", "Detailed"),
        PXStringListAttribute.Pair("DR", "Detailed with Retainage")
      })
    {
    }
  }

  [Obsolete("This class has been deprecated and will be removed in Acumatica ERP 2025R1.")]
  public abstract class format : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRetainageReportParameters.format>
  {
    public class ListAttribute : PXStringListAttribute
    {
      public const string Summary = "S";
      public const string Details = "D";
      public const string DetailsRetainage = "DR";

      public ListAttribute()
        : base(ARRetainageReportParameters.format.ListAttribute.GetAllowedValues(), ARRetainageReportParameters.format.ListAttribute.GetAllowedLabels())
      {
      }

      public static string[] GetAllowedValues()
      {
        List<string> stringList = new List<string>()
        {
          "S",
          "D"
        };
        if (PXAccess.FeatureInstalled<FeaturesSet.retainage>())
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
        if (PXAccess.FeatureInstalled<FeaturesSet.retainage>())
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
    ARRetainageReportParameters.projectID>
  {
  }

  public abstract class anyStateProjectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRetainageReportParameters.anyStateProjectID>
  {
  }
}
