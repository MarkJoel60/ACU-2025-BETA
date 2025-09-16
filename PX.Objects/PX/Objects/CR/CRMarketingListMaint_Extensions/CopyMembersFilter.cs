// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMarketingListMaint_Extensions.CopyMembersFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR.CRMarketingListMaint_Extensions;

[PXHidden]
public class CopyMembersFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [CopyMembersFilter.addMembersOption.List]
  [PXUnboundDefault(0)]
  [PXUIField]
  public int? AddMembersOption { get; set; }

  public abstract class addMembersOption : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    CopyMembersFilter.addMembersOption>
  {
    public const int AddToNew = 0;
    public const int AddToExisting = 1;

    public class addToNew : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      CopyMembersFilter.addMembersOption.addToNew>
    {
      public addToNew()
        : base(0)
      {
      }
    }

    public class addToExisting : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      CopyMembersFilter.addMembersOption.addToExisting>
    {
      public addToExisting()
        : base(1)
      {
      }
    }

    public class ListAttribute : PXIntListAttribute
    {
      public ListAttribute()
        : base(new (int, string)[2]
        {
          (0, "Add All Members to a New Static List"),
          (1, "Add All Members to Existing Static Lists")
        })
      {
      }
    }
  }
}
