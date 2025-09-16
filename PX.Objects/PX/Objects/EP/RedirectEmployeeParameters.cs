// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.RedirectEmployeeParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXHidden]
[Serializable]
public class RedirectEmployeeParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  public virtual int? ParentBAccountID { get; set; }

  [PXBool]
  public virtual bool? RouteEmails { get; set; }

  public abstract class parentBAccountID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    RedirectEmployeeParameters.parentBAccountID>
  {
  }

  public abstract class routeEmails : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RedirectEmployeeParameters.routeEmails>
  {
  }
}
