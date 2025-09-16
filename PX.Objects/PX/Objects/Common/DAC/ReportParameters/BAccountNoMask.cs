// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DAC.ReportParameters.BAccountNoMask
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.Common.DAC.ReportParameters;

[PXHidden(ServiceVisible = true)]
[PXProjection(typeof (Select<BAccount>), Persistent = false)]
[Serializable]
public class BAccountNoMask : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (BAccount.bAccountID))]
  public virtual int? BAccountID { get; set; }

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (BAccount.acctCD))]
  public virtual 
  #nullable disable
  string AcctCD { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (BAccount.acctName))]
  public virtual string AcctName { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (BAccount.acctReferenceNbr))]
  public virtual string AcctReferenceNbr { get; set; }

  [ParentBAccount(typeof (BAccount.bAccountID), null, null, null, null, BqlField = typeof (BAccount.parentBAccountID))]
  public virtual int? ParentBAccountID { get; set; }

  [PXDBInt(BqlField = typeof (BAccount.ownerID))]
  public virtual int? OwnerID { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (BAccount.type))]
  public virtual string Type { get; set; }

  [PXDBInt(BqlField = typeof (BAccount.defContactID))]
  public virtual int? DefContactID { get; set; }

  [PXDBInt(BqlField = typeof (BAccount.defLocationID))]
  public virtual int? DefLocationID { get; set; }

  [PXDBInt(BqlField = typeof (BAccount.defAddressID))]
  public virtual int? DefAddressID { get; set; }

  [PXDBBool(BqlField = typeof (BAccount.isBranch))]
  public virtual bool? IsBranch { get; set; }

  [PXDBInt(BqlField = typeof (BAccount.cOrgBAccountID))]
  public virtual int? COrgBAccountID { get; set; }

  [PXDBInt(BqlField = typeof (BAccount.vOrgBAccountID))]
  public virtual int? VOrgBAccountID { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (BAccount.taxRegistrationID))]
  public virtual string TaxRegistrationID { get; set; }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountNoMask.bAccountID>
  {
  }

  public abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountNoMask.acctCD>
  {
  }

  public abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountNoMask.acctName>
  {
  }

  public abstract class acctReferenceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BAccountNoMask.acctReferenceNbr>
  {
  }

  public abstract class parentBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BAccountNoMask.parentBAccountID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountNoMask.ownerID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountNoMask.type>
  {
  }

  public abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountNoMask.defContactID>
  {
  }

  public abstract class defLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountNoMask.defLocationID>
  {
  }

  public abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountNoMask.defAddressID>
  {
  }

  public abstract class isBranch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BAccountNoMask.isBranch>
  {
  }

  public abstract class cOrgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountNoMask.cOrgBAccountID>
  {
  }

  public abstract class vOrgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountNoMask.vOrgBAccountID>
  {
  }

  public abstract class taxRegistrationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BAccountNoMask.taxRegistrationID>
  {
  }
}
