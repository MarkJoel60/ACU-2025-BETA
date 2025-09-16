// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_OnCompleteApptSetInProcessItemsAs
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_OnCompleteApptSetInProcessItemsAs
{
  public const 
  #nullable disable
  string DoNothing = "DN";
  public const string Completed = "CP";
  public const string NotFinished = "NF";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("NF", "Not Finished"),
        PXStringListAttribute.Pair("CP", "Completed"),
        PXStringListAttribute.Pair("DN", "In Process")
      })
    {
    }
  }

  public class doNothing : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_OnCompleteApptSetInProcessItemsAs.doNothing>
  {
    public doNothing()
      : base("DN")
    {
    }
  }

  public class completed : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_OnCompleteApptSetInProcessItemsAs.completed>
  {
    public completed()
      : base("CP")
    {
    }
  }

  public class notFinished : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_OnCompleteApptSetInProcessItemsAs.notFinished>
  {
    public notFinished()
      : base("NF")
    {
    }
  }
}
