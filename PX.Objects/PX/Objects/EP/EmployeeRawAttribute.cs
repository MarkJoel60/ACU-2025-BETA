// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EmployeeRawAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.SM;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.EP;

/// <summary>'EMPLOYEE' dimension selector.</summary>
/// <example>[EmployeeRaw]</example>
public class EmployeeRawAttribute : PXEntityAttribute
{
  public const 
  #nullable disable
  string DimensionName = "EMPLOYEE";

  public EmployeeRawAttribute()
  {
    Type type1 = typeof (Search2<EPEmployee.acctCD, LeftJoin<EmployeeRawAttribute.EmployeeLogin, On<EmployeeRawAttribute.EmployeeLogin.pKID, Equal<EPEmployee.userID>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<EPEmployee.parentBAccountID>>, LeftJoin<EPEmployeePosition, On<EPEmployeePosition.employeeID, Equal<EPEmployee.bAccountID>, And<EPEmployeePosition.isActive, Equal<True>>>>>>, Where<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>);
    PXAggregateAttribute.AggregatedAttributesCollection attributes = ((PXAggregateAttribute) this)._Attributes;
    Type type2 = type1;
    Type type3 = typeof (EPEmployee.acctCD);
    Type[] typeArray = new Type[8]
    {
      typeof (EPEmployee.bAccountID),
      typeof (EPEmployee.acctCD),
      typeof (EPEmployee.acctName),
      typeof (EPEmployee.vStatus),
      typeof (EPEmployeePosition.positionID),
      typeof (EPEmployee.departmentID),
      typeof (EPEmployee.defLocationID),
      typeof (EmployeeRawAttribute.EmployeeLogin.username)
    };
    PXDimensionSelectorAttribute selectorAttribute1;
    PXDimensionSelectorAttribute selectorAttribute2 = selectorAttribute1 = new PXDimensionSelectorAttribute("EMPLOYEE", type2, type3, typeArray);
    attributes.Add((PXEventSubscriberAttribute) selectorAttribute1);
    selectorAttribute2.DescriptionField = typeof (EPEmployee.acctName);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.Filterable = true;
  }

  [PXBreakInheritance]
  [PXHidden]
  [Serializable]
  public sealed class EmployeeLogin : Users
  {
    [PXDBString(256 /*0x0100*/, IsKey = true, IsUnicode = true)]
    [PXUIField(Visible = false)]
    public virtual string Username
    {
      get => base.Username;
      set => base.Username = value;
    }

    public abstract class pKID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      EmployeeRawAttribute.EmployeeLogin.pKID>
    {
    }

    public abstract class username : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      EmployeeRawAttribute.EmployeeLogin.username>
    {
    }
  }
}
