// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DAC.APAROverdueBucket
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.Common.DAC;

[PXCacheName("AP AR Overdue Bucket")]
[Serializable]
public class APAROverdueBucket : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  [APAROverdueBucket.overdueBucket.List]
  [PXUIField(DisplayName = "Overdue Bucket")]
  public virtual 
  #nullable disable
  string OverdueBucket { get; set; }

  public abstract class overdueBucket : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAROverdueBucket.overdueBucket>
  {
    public const string Current = "B1-Current";
    public const string Overdue1to30 = "B2-1-30";
    public const string Overdue31to60 = "B3-31-60";
    public const string Overdue61to90 = "B4-61-90";
    public const string OverdueOver90 = "B5->90";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new (string, string)[5]
        {
          ("B1-Current", "Current"),
          ("B2-1-30", "Overdue 1-30 days"),
          ("B3-31-60", "Overdue 31-60 days"),
          ("B4-61-90", "Overdue 61-90 days"),
          ("B5->90", "Overdue over 90 days")
        })
      {
      }
    }
  }
}
