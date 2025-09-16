// Decompiled with JetBrains decompiler
// Type: PX.SM.ScanJobStatus
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class ScanJobStatus
{
  public const 
  #nullable disable
  string Processed = "D";
  public const string Processing = "P";
  public const string Failed = "F";
  public const string Submitted = "S";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(PXStringListAttribute.Pair("D", "Processed"), PXStringListAttribute.Pair("P", "Processing"), PXStringListAttribute.Pair("F", "Failed"), PXStringListAttribute.Pair("S", "Submitted"))
    {
    }
  }

  public class processed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ScanJobStatus.processed>
  {
    public processed()
      : base("D")
    {
    }
  }

  public class processing : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ScanJobStatus.processing>
  {
    public processing()
      : base("P")
    {
    }
  }

  public class failed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ScanJobStatus.failed>
  {
    public failed()
      : base("F")
    {
    }
  }

  public class submitted : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ScanJobStatus.submitted>
  {
    public submitted()
      : base("S")
    {
    }
  }
}
