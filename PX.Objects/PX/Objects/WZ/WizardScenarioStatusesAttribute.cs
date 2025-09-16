// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WizardScenarioStatusesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.WZ;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public sealed class WizardScenarioStatusesAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string _PENDING = "PN";
  public const string _ACTIVE = "AC";
  public const string _SUSPEND = "SU";
  public const string _COMPLETED = "CP";

  public WizardScenarioStatusesAttribute()
    : base(new string[4]{ "PN", "AC", "SU", "CP" }, new string[4]
    {
      "Pending",
      "In Progress",
      "Suspended",
      "Completed"
    })
  {
  }

  public sealed class Pending : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WizardScenarioStatusesAttribute.Pending>
  {
    public Pending()
      : base("PN")
    {
    }
  }

  public sealed class Active : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WizardScenarioStatusesAttribute.Active>
  {
    public Active()
      : base("AC")
    {
    }
  }

  public sealed class Suspend : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WizardScenarioStatusesAttribute.Suspend>
  {
    public Suspend()
      : base("SU")
    {
    }
  }

  public sealed class Completed : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WizardScenarioStatusesAttribute.Completed>
  {
    public Completed()
      : base("CP")
    {
    }
  }
}
