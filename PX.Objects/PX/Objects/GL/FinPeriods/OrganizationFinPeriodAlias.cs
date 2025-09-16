// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriods.OrganizationFinPeriodAlias
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL.FinPeriods;

[Serializable]
public class OrganizationFinPeriodAlias : OrganizationFinPeriod
{
  public new abstract class organizationID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    OrganizationFinPeriodAlias.organizationID>
  {
  }

  public new abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OrganizationFinPeriodAlias.startDate>
  {
  }

  public new abstract class endDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OrganizationFinPeriodAlias.endDate>
  {
  }
}
