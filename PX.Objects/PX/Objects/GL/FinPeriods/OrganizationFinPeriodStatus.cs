// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriods.OrganizationFinPeriodStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.GL.FinPeriods;

public class OrganizationFinPeriodStatus : OrganizationFinPeriod
{
  public new abstract class organizationID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    OrganizationFinPeriodStatus.organizationID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationFinPeriodStatus.finPeriodID>
  {
  }

  public new abstract class dateLocked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    OrganizationFinPeriodStatus.dateLocked>
  {
  }

  public new abstract class status : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationFinPeriodStatus.status>
  {
  }

  public new abstract class aPClosed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    OrganizationFinPeriodStatus.aPClosed>
  {
  }

  public new abstract class aRClosed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    OrganizationFinPeriodStatus.aRClosed>
  {
  }

  public new abstract class iNClosed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    OrganizationFinPeriodStatus.iNClosed>
  {
  }

  public new abstract class cAClosed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    OrganizationFinPeriodStatus.cAClosed>
  {
  }

  public new abstract class fAClosed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    OrganizationFinPeriodStatus.fAClosed>
  {
  }
}
