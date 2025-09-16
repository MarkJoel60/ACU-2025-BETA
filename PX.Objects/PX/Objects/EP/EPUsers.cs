// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPUsers
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXSubstitute(GraphType = typeof (EPEventMaint), ParentType = typeof (Users))]
[PXSubstitute(GraphType = typeof (EPCalendarSync), ParentType = typeof (Users))]
[PXHidden]
[Serializable]
public class EPUsers : Users
{
  private 
  #nullable disable
  string _employerEmail;
  private bool _employerEmailLoaded;

  public string EmployerEmail
  {
    [PXDependsOnFields(new System.Type[] {typeof (Users.pKID), typeof (Users.email)})] get
    {
      if (!this._employerEmailLoaded)
      {
        PXResultset<PX.Objects.CR.Contact> pxResultset = PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<EPEmployee, On<PX.Objects.CR.Contact.bAccountID, Equal<EPEmployee.parentBAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<EPEmployee.defContactID>>>>, Where<EPEmployee.userID, Equal<Required<EPEmployee.userID>>>>.Config>.Select(new PXGraph(), new object[1]
        {
          (object) this._PKID
        });
        if (pxResultset != null && pxResultset.Count > 0)
          this._employerEmail = PXResult<PX.Objects.CR.Contact>.op_Implicit(pxResultset[0]).EMail;
        this._employerEmailLoaded = true;
      }
      return this._employerEmail;
    }
  }

  public virtual string Email
  {
    [PXDependsOnFields(new System.Type[] {typeof (EPUsers.employerEmail)})] get
    {
      return !string.IsNullOrEmpty(base.Email) ? base.Email : this.EmployerEmail;
    }
    set => base.Email = value;
  }

  public abstract class employerEmail : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPUsers.employerEmail>
  {
  }
}
