// Decompiled with JetBrains decompiler
// Type: PX.SM.AUDataTypeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUDataTypeAttribute : PXIntListAttribute
{
  public const int NVarChar = 0;
  public const int Int = 1;
  public const int Boolean = 2;
  public const int Decimal = 3;
  public const int DateTime = 4;

  public AUDataTypeAttribute()
    : base(new int[5]{ 0, 1, 2, 3, 4 }, new string[5]
    {
      "nvarchar",
      "int",
      "boolean",
      "decimal",
      "datetime"
    })
  {
  }

  public 
  #nullable disable
  string GetSqlTypeName(int i) => this._AllowedLabels[i];

  public class nvarchar_t : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  AUDataTypeAttribute.nvarchar_t>
  {
    public nvarchar_t()
      : base(0)
    {
    }
  }

  public class int_t : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  AUDataTypeAttribute.int_t>
  {
    public int_t()
      : base(1)
    {
    }
  }

  public class boolean_t : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  AUDataTypeAttribute.boolean_t>
  {
    public boolean_t()
      : base(2)
    {
    }
  }

  public class decimal_t : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  AUDataTypeAttribute.decimal_t>
  {
    public decimal_t()
      : base(3)
    {
    }
  }

  public class datetime_t : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  AUDataTypeAttribute.datetime_t>
  {
    public datetime_t()
      : base(4)
    {
    }
  }
}
