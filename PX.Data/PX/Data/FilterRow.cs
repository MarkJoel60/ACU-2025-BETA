// Decompiled with JetBrains decompiler
// Type: PX.Data.FilterRow
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Data;

[DebuggerDisplay("{OpenBrackets} {DataField} {((PXCondition)Condition).ToString()} {ValueSt}|{ValueSt2} {CloseBrackets}")]
[PXCacheName("Filter Row")]
public class FilterRow : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _IsUsed;
  protected Guid? _FilterID;
  protected short? _FilterRowNbr;
  protected int? _OpenBrackets;
  protected int? _CloseBrackets;
  protected 
  #nullable disable
  string _DataField;
  protected byte? _Condition;
  protected int? _Operator;

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsUsed
  {
    get => this._IsUsed;
    set => this._IsUsed = value;
  }

  [PXDefault]
  [PXDBGuid(false, IsKey = true)]
  [PXParent(typeof (Select<FilterHeader, Where<FilterHeader.filterID, Equal<Current<FilterRow.filterID>>>>))]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual Guid? FilterID
  {
    get => this._FilterID;
    set => this._FilterID = value;
  }

  [PXDefault]
  [RowNbr]
  [PXDBShort(IsKey = true)]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual short? FilterRowNbr
  {
    get => this._FilterRowNbr;
    set => this._FilterRowNbr = value;
  }

  [PXDefault(0)]
  [PXDBInt]
  [FilterRow.OpenBrackets]
  [PXUIField(DisplayName = "Brackets")]
  public virtual int? OpenBrackets
  {
    get => this._OpenBrackets;
    set => this._OpenBrackets = value;
  }

  [PXDefault(0)]
  [PXDBInt]
  [FilterRow.CloseBrackets]
  [PXUIField(DisplayName = "Brackets")]
  public virtual int? CloseBrackets
  {
    get => this._CloseBrackets;
    set => this._CloseBrackets = value;
  }

  [PXDefault]
  [PXDBString(256 /*0x0100*/)]
  [PXUIField(DisplayName = "Property")]
  public virtual string DataField
  {
    get => this._DataField;
    set => this._DataField = value;
  }

  [PXDefault]
  [RowCondition]
  [PXUIField(DisplayName = "Condition")]
  public virtual byte? Condition
  {
    get => this._Condition;
    set => this._Condition = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Value")]
  public virtual string ValueSt { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Value2")]
  public virtual string ValueSt2 { get; set; }

  [PXDefault(0)]
  [PXDBInt]
  [PXUIField(DisplayName = "Operator")]
  public virtual int? Operator
  {
    get => this._Operator;
    set => this._Operator = value;
  }

  [PXDBByte]
  [PXDefault(0)]
  public virtual byte? FilterType { get; set; }

  public class PK : PrimaryKeyOf<FilterRow>.By<FilterRow.filterID, FilterRow.filterRowNbr>
  {
    public static FilterRow Find(
      PXGraph graph,
      long? filterID,
      short? filterRowNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<FilterRow>.By<FilterRow.filterID, FilterRow.filterRowNbr>.FindBy(graph, (object) filterID, (object) filterRowNbr, options);
    }
  }

  public static class FK
  {
    public class FilterHeader : 
      PrimaryKeyOf<FilterHeader>.By<FilterHeader.filterID>.ForeignKeyOf<FilterRow>.By<FilterRow.filterID>
    {
    }
  }

  public sealed class OpenBracketsAttribute : PXIntListAttribute
  {
    public OpenBracketsAttribute()
      : base(FilterRow.OpenBracketsAttribute.Values, FilterRow.OpenBracketsAttribute.Labels)
    {
    }

    /// <summary>Get.</summary>
    public static string[] Labels
    {
      get
      {
        return new string[6]
        {
          " ",
          "(",
          "((",
          "(((",
          "((((",
          "((((("
        };
      }
    }

    /// <summary>Get.</summary>
    public static int[] Values
    {
      get => new int[6]{ 0, 1, 2, 3, 4, 5 };
    }
  }

  public sealed class CloseBracketsAttribute : PXIntListAttribute
  {
    public CloseBracketsAttribute()
      : base(FilterRow.CloseBracketsAttribute.Values, FilterRow.CloseBracketsAttribute.Labels)
    {
    }

    /// <summary>Get.</summary>
    public static string[] Labels
    {
      get
      {
        return new string[6]
        {
          " ",
          ")",
          "))",
          ")))",
          "))))",
          ")))))"
        };
      }
    }

    /// <summary>Get.</summary>
    public static int[] Values
    {
      get => new int[6]{ 0, 1, 2, 3, 4, 5 };
    }
  }

  public abstract class isUsed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FilterRow.isUsed>
  {
  }

  public abstract class filterID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FilterRow.filterID>
  {
  }

  public abstract class filterRowNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FilterRow.filterRowNbr>
  {
  }

  public abstract class openBrackets : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FilterRow.openBrackets>
  {
  }

  public abstract class closeBrackets : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FilterRow.closeBrackets>
  {
  }

  public abstract class dataField : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FilterRow.dataField>
  {
  }

  public abstract class condition : BqlType<
  #nullable enable
  IBqlByte, byte>.Field<
  #nullable disable
  FilterRow.condition>
  {
  }

  public abstract class valueSt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FilterRow.valueSt>
  {
  }

  public abstract class valueSt2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FilterRow.valueSt2>
  {
  }

  public abstract class @operator : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FilterRow.@operator>
  {
  }

  public abstract class filterType : BqlType<
  #nullable enable
  IBqlByte, byte>.Field<
  #nullable disable
  FilterRow.filterType>
  {
    public class advanced : BqlType<
    #nullable enable
    IBqlByte, byte>.Constant<
    #nullable disable
    FilterRow.filterType.advanced>
    {
      public advanced()
        : base((byte) 0)
      {
      }
    }

    public class quick : BqlType<
    #nullable enable
    IBqlByte, byte>.Constant<
    #nullable disable
    FilterRow.filterType.quick>
    {
      public quick()
        : base((byte) 1)
      {
      }
    }
  }

  public enum FilterTypeEnum
  {
    Advanced,
    Quick,
  }
}
