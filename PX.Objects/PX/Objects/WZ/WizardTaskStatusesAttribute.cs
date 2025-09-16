// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WizardTaskStatusesAttribute
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
public sealed class WizardTaskStatusesAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string _PENDING = "PN";
  public const string _SKIPPED = "SK";
  public const string _ACTIVE = "AC";
  public const string _DISABLED = "DS";
  public const string _COMPLETED = "CP";
  public const string _OPEN = "OP";

  public WizardTaskStatusesAttribute()
    : base(new string[6]
    {
      "PN",
      "SK",
      "AC",
      "DS",
      "CP",
      "OP"
    }, new string[6]
    {
      "Pending",
      "Skipped",
      "In Progress",
      "Disabled",
      "Completed",
      "Open"
    })
  {
  }

  public sealed class Pending : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WizardTaskStatusesAttribute.Pending>
  {
    public Pending()
      : base("PN")
    {
    }
  }

  public sealed class Skipped : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WizardTaskStatusesAttribute.Skipped>
  {
    public Skipped()
      : base("SK")
    {
    }
  }

  public sealed class Active : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WizardTaskStatusesAttribute.Active>
  {
    public Active()
      : base("AC")
    {
    }
  }

  public sealed class Disabled : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WizardTaskStatusesAttribute.Disabled>
  {
    public Disabled()
      : base("DS")
    {
    }
  }

  public sealed class Completed : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WizardTaskStatusesAttribute.Completed>
  {
    public Completed()
      : base("CP")
    {
    }
  }

  public sealed class Open : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  WizardTaskStatusesAttribute.Open>
  {
    public Open()
      : base("OP")
    {
    }
  }
}
