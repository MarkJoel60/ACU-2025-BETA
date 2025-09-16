// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPCompanyTreeMemberExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.TM;

#nullable enable
namespace PX.Objects.EP;

public sealed class EPCompanyTreeMemberExt : PXCacheExtension<
#nullable disable
EPCompanyTreeMember>
{
  [PXDBInt(IsKey = true)]
  [PXSelector(typeof (Search<PX.Objects.CR.Contact.contactID, Where<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.employee>>>))]
  [PXUIField]
  public int? ContactID { get; set; }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPCompanyTreeMemberExt.contactID>
  {
  }
}
