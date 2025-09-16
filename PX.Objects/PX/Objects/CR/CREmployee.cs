// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CREmployee
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.EP;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <inheritdoc />
[PXBreakInheritance]
[PXCacheName("Employee")]
[CRCacheIndependentPrimaryGraph(typeof (EmployeeMaint), typeof (Select<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<CREmployee.bAccountID>>>>))]
[Serializable]
public class CREmployee : PX.Objects.CR.Standalone.EPEmployee
{
  /// <inheritdoc />
  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public override 
  #nullable disable
  string AcctName
  {
    get => base.AcctName;
    set => base.AcctName = value;
  }

  /// <inheritdoc />
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Status")]
  [PXDefault("A")]
  [VendorStatus.List]
  public override string VStatus { get; set; }

  /// <inheritdoc />
  [PXUniqueNote(DescriptionField = typeof (PX.Objects.EP.EPEmployee.acctCD), Selector = typeof (PX.Objects.EP.EPEmployee.acctCD))]
  public override Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CREmployee.bAccountID>
  {
  }

  public new abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CREmployee.acctCD>
  {
  }

  public new abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CREmployee.defContactID>
  {
  }

  public new abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CREmployee.defAddressID>
  {
  }

  public new abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CREmployee.acctName>
  {
  }

  public new abstract class acctReferenceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CREmployee.acctReferenceNbr>
  {
  }

  public new abstract class parentBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CREmployee.parentBAccountID>
  {
  }

  public new abstract class departmentID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CREmployee.departmentID>
  {
  }

  public new abstract class defLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CREmployee.defLocationID>
  {
  }

  public new abstract class supervisorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CREmployee.supervisorID>
  {
  }

  public new abstract class vStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CREmployee.vStatus>
  {
  }

  public new abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CREmployee.classID>
  {
  }

  public new abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CREmployee.userID>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CREmployee.noteID>
  {
  }
}
