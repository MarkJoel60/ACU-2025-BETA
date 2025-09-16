// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.ACHExportMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA;

public class ACHExportMethod
{
  public const 
  #nullable disable
  string ExportScenario = "E";
  public const string PlugIn = "P";
  protected static string[] _allowedValues = new string[2]
  {
    "E",
    "P"
  };
  protected static string[] _allowedLabels = new string[2]
  {
    "Export Scenario",
    "U.S. ACH Plug-In"
  };

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(ACHExportMethod._allowedValues, ACHExportMethod._allowedLabels)
    {
    }
  }

  public class exportScenario : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ACHExportMethod.exportScenario>
  {
    public exportScenario()
      : base("E")
    {
    }
  }

  public class plugIn : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ACHExportMethod.plugIn>
  {
    public plugIn()
      : base("P")
    {
    }
  }
}
