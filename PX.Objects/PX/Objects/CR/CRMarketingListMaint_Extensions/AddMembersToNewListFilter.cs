// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMarketingListMaint_Extensions.AddMembersToNewListFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.TM;

#nullable enable
namespace PX.Objects.CR.CRMarketingListMaint_Extensions;

[PXHidden]
public class AddMembersToNewListFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(30, IsUnicode = true)]
  [PXDefault]
  [PXDimension("MLISTCD")]
  [PXUIField]
  public virtual 
  #nullable disable
  string MailListCode { get; set; }

  [PXString(50, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Name { get; set; }

  [Owner]
  public virtual int? OwnerID { get; set; }

  public abstract class mailListCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddMembersToNewListFilter.mailListCode>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddMembersToNewListFilter.name>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddMembersToNewListFilter.ownerID>
  {
  }
}
