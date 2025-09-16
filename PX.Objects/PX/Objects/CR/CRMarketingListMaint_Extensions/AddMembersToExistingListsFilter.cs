// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMarketingListMaint_Extensions.AddMembersToExistingListsFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR.CRMarketingListMaint_Extensions;

[PXHidden]
public class AddMembersToExistingListsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  [PXUIField(DisplayName = "Caption")]
  public 
  #nullable disable
  string Caption { get; set; }

  public abstract class caption : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddMembersToExistingListsFilter.caption>
  {
  }
}
