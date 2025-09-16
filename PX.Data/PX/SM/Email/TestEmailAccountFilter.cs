// Decompiled with JetBrains decompiler
// Type: PX.SM.Email.TestEmailAccountFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM.Email;

[PXInternalUseOnly]
[PXHidden]
public class TestEmailAccountFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt]
  [PXUIField(DisplayName = "From")]
  [PXDefault(typeof (Current<EMailAccount.emailAccountID>))]
  [PXSelector(typeof (EMailAccount.emailAccountID), new System.Type[] {typeof (EMailAccount.description)}, DescriptionField = typeof (EMailAccount.description))]
  public int? From { get; set; }

  [PXDBEmail]
  [PXUIField(DisplayName = "Email Address")]
  [PXDefault]
  public 
  #nullable disable
  string EmailAddress { get; set; }

  public abstract class from : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TestEmailAccountFilter.from>
  {
  }

  public abstract class emailAddress : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TestEmailAccountFilter.emailAddress>
  {
  }
}
