// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.BankFeedCategory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA.BankFeed;

[PXCacheName("BankFeedCategory")]
public class BankFeedCategory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(100, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Category", Enabled = false)]
  public virtual 
  #nullable disable
  string Category { get; set; }

  public abstract class category : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedCategory.category>
  {
  }
}
