// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.VoidFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXHidden]
[PXCacheName("VoidFilter")]
public class VoidFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  [PXUIField(DisplayName = "Void Payments On")]
  [VoidFilter.voidDateOption.List]
  [PXDefault("O")]
  public virtual 
  #nullable disable
  string VoidDateOption { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Void Date", Visible = false, Enabled = false)]
  [PXDefault]
  public virtual DateTime? VoidDate { get; set; }

  public abstract class voidDateOption : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VoidFilter.voidDateOption>
  {
    public const string OriginalPaymentDates = "O";
    public const string SpecificVoidDate = "S";

    public class originalPaymentDates : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      VoidFilter.voidDateOption.originalPaymentDates>
    {
      public originalPaymentDates()
        : base("O")
      {
      }
    }

    public class specificVoidDate : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      VoidFilter.voidDateOption.specificVoidDate>
    {
      public specificVoidDate()
        : base("S")
      {
      }
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "O", "S" }, new string[2]
        {
          "Original Payment Dates",
          "Specific Date"
        })
      {
      }
    }
  }

  public abstract class voidDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  VoidFilter.voidDate>
  {
  }
}
