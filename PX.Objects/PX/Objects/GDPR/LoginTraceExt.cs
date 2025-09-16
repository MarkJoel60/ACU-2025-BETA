// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.LoginTraceExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.GDPR;

[PXPersonalDataTable(typeof (Select<LoginTrace, Where<LoginTrace.username, Equal<Current<Users.username>>>>))]
[Serializable]
public sealed class LoginTraceExt : PXCacheExtension<
#nullable disable
LoginTrace>, IPseudonymizable, INotable
{
  [PXPseudonymizationStatusField]
  public int? PseudonymizationStatus { get; set; }

  [PXDBGuidNotNull]
  public Guid? NoteID { get; set; }

  public abstract class pseudonymizationStatus : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LoginTraceExt.pseudonymizationStatus>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  LoginTraceExt.noteID>
  {
  }
}
