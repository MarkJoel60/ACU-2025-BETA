// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.AP1099Year
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXCacheName("AP 1099 Year")]
[Serializable]
public class AP1099Year : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _FinYear;
  protected string _StartDate;
  protected string _EndDate;
  protected string _Status;
  protected byte[] _tstamp;

  [PXDefault]
  [Organization(true, IsKey = true, FieldClass = "MULTICOMPANY")]
  public virtual int? OrganizationID { get; set; }

  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "1099 Year", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Search<AP1099Year.finYear, Where<AP1099Year.organizationID, Equal<Current2<AP1099Year.organizationID>>>>))]
  public virtual string FinYear
  {
    get => this._FinYear;
    set => this._FinYear = value;
  }

  [PXDBCalced(typeof (BqlOperand<AP1099Year.finYear, IBqlString>.Concat<AP1099Year.string0101>), typeof (string))]
  public virtual string StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBCalced(typeof (BqlOperand<AP1099Year.finYear, IBqlString>.Concat<AP1099Year.string1231>), typeof (string))]
  public virtual string EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [AP1099Year.status.List]
  [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  private class string0101 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AP1099Year.string0101>
  {
    public string0101()
      : base("0101")
    {
    }
  }

  private class string1231 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AP1099Year.string1231>
  {
    public string1231()
      : base("1231")
    {
    }
  }

  public class PK : PrimaryKeyOf<AP1099Year>.By<AP1099Year.organizationID, AP1099Year.finYear>
  {
    public static AP1099Year Find(
      PXGraph graph,
      int? organizationID,
      string finYear,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<AP1099Year>.By<AP1099Year.organizationID, AP1099Year.finYear>.FindBy(graph, (object) organizationID, (object) finYear, options);
    }
  }

  public static class FK
  {
    public class Organization : 
      PrimaryKeyOf<PX.Objects.GL.DAC.Organization>.By<PX.Objects.GL.DAC.Organization.organizationID>.ForeignKeyOf<AP1099Year>.By<AP1099Year.organizationID>
    {
    }
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AP1099Year.organizationID>
  {
  }

  public abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AP1099Year.finYear>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AP1099Year.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AP1099Year.endDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AP1099Year.status>
  {
    public const string Open = "N";
    public const string Closed = "C";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "N", "C" }, new string[2]
        {
          "Open",
          "Closed"
        })
      {
      }
    }

    public class open : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AP1099Year.status.open>
    {
      public open()
        : base("N")
      {
      }
    }

    public class closed : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AP1099Year.status.closed>
    {
      public closed()
        : base("C")
      {
      }
    }
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AP1099Year.Tstamp>
  {
  }
}
