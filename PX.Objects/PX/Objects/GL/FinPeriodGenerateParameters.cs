// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriodGenerateParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.GL;

[Serializable]
public class FinPeriodGenerateParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXUIField(DisplayName = "Organization", Enabled = false)]
  [PXUIVisible(typeof (Where<FinPeriodGenerateParameters.organizationID, IsNotNull, And<FinPeriodGenerateParameters.organizationID, NotEqual<int0>>>))]
  [Organization(true)]
  public virtual int? OrganizationID { get; set; }

  /// <summary>
  /// The financial year starting from which the periods will be generated in the system.
  /// </summary>
  [PXString(4, IsFixed = true)]
  [PXDefault("")]
  [PXUIField(DisplayName = "From Year")]
  public virtual 
  #nullable disable
  string FromYear { get; set; }

  /// <summary>
  /// The financial year till which the periods will be generated in the system.
  /// </summary>
  [PXString(4, IsFixed = true)]
  [PXDefault("")]
  [PXUIField(DisplayName = "To Year")]
  public virtual string ToYear { get; set; }

  /// <summary>
  /// The financial year starting from which the periods will be generated in the system.
  /// </summary>
  [PXString(4, IsFixed = true)]
  [PXUIField(DisplayName = "First Year", Enabled = false)]
  public virtual string FirstFinYear { get; set; }

  /// <summary>
  /// The financial year till which the periods will be generated in the system.
  /// </summary>
  [PXString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Last Year", Enabled = false)]
  public virtual string LastFinYear { get; set; }

  public abstract class organizationID : IBqlField, IBqlOperand
  {
  }

  public abstract class fromYear : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FinPeriodGenerateParameters.fromYear>
  {
  }

  public abstract class toYear : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FinPeriodGenerateParameters.toYear>
  {
  }

  public abstract class firstFinYear : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FinPeriodGenerateParameters.firstFinYear>
  {
  }

  public abstract class lastFinYear : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FinPeriodGenerateParameters.lastFinYear>
  {
  }
}
