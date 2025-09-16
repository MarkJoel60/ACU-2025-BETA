// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_OnCompleteApptSetNotStartedItemsAs
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_OnCompleteApptSetNotStartedItemsAs
{
  public const 
  #nullable disable
  string NotPerformed = "NP";
  public const string Completed = "CP";
  public const string DoNothing = "DN";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("NP", "Not Performed"),
        PXStringListAttribute.Pair("CP", "Completed"),
        PXStringListAttribute.Pair("DN", "Not Started")
      })
    {
    }
  }

  public class doNothing : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_OnCompleteApptSetNotStartedItemsAs.doNothing>
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
    ListField_OnCompleteApptSetNotStartedItemsAs.completed>
  {
    public completed()
      : base("CP")
    {
    }
  }

  public class notPerformed : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_OnCompleteApptSetNotStartedItemsAs.notPerformed>
  {
    public notPerformed()
      : base("NP")
    {
    }
  }
}
