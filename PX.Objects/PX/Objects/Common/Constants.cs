// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Constants
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.Common;

public class Constants
{
  public const int TranDescLength = 256 /*0x0100*/;
  public const int TranDescLength512 = 512 /*0x0200*/;
  public const 
  #nullable disable
  string ProjectsWorkspaceID = "6dbfa68e-79e9-420b-9f64-e1036a28998c";
  public const string ProjectsWorkspaceIcon = "project";
  public const string ConstructionWorkspaceIcon = "cran";
  public const string ProfessionalServicesWorkspaceIcon = "person_with_checkmarks";

  public class DACName<DAC> : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  Constants.DACName<DAC>> where DAC : IBqlTable
  {
    public DACName()
      : base(typeof (DAC).FullName)
    {
    }
  }

  public class sevenInt : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  Constants.sevenInt>
  {
    public sevenInt()
      : base(7)
    {
    }
  }
}
