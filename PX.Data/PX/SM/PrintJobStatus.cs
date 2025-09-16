// Decompiled with JetBrains decompiler
// Type: PX.SM.PrintJobStatus
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class PrintJobStatus
{
  public const 
  #nullable disable
  string Processed = "D";
  public const string Pending = "P";
  public const string Failed = "F";
  public const string Unknown = "U";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(PXStringListAttribute.Pair("D", "Processed"), PXStringListAttribute.Pair("P", "Pending"), PXStringListAttribute.Pair("F", "Failed"), PXStringListAttribute.Pair("U", "Unknown"))
    {
    }
  }

  public class processed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PrintJobStatus.processed>
  {
    public processed()
      : base("D")
    {
    }
  }

  public class pending : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PrintJobStatus.pending>
  {
    public pending()
      : base("P")
    {
    }
  }

  public class failed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PrintJobStatus.failed>
  {
    public failed()
      : base("F")
    {
    }
  }

  public class unknown : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PrintJobStatus.unknown>
  {
    public unknown()
      : base("U")
    {
    }
  }
}
