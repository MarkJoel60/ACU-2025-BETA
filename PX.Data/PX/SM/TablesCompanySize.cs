// Decompiled with JetBrains decompiler
// Type: PX.SM.TablesCompanySize
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class TablesCompanySize : TableSize
{
  [PXUIField(DisplayName = "Size in DB", Enabled = false)]
  [PXLong]
  public virtual long? Size { get; set; }

  [PXUIField(DisplayName = "Size in DB (MB)", Enabled = false)]
  [PXDecimal(2)]
  public virtual Decimal? SizeMB
  {
    get
    {
      return new Decimal?(this.Size.HasValue ? Decimal.Round(Convert.ToDecimal(this.Size.Value) / 1048576M, 2, MidpointRounding.AwayFromZero) : 0M);
    }
    set
    {
    }
  }

  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Tenant Name", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual 
  #nullable disable
  string CompanyName { get; set; }

  public abstract class size : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  TablesCompanySize.size>
  {
  }

  public abstract class sizeMB : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TablesCompanySize.sizeMB>
  {
  }

  public abstract class companyName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TablesCompanySize.companyName>
  {
  }
}
