// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.JointChecks.JointPayeeDisplay
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using System;

#nullable enable
namespace PX.Objects.CN.JointChecks;

[PXHidden]
[PXProjection(typeof (Select2<JointPayee, LeftJoin<Vendor, On<Vendor.bAccountID, Equal<JointPayee.jointPayeeInternalId>>>>), Persistent = false)]
public class JointPayeeDisplay : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (JointPayee.jointPayeeId))]
  public virtual int? JointPayeeId { get; set; }

  [PXDBString(30, BqlField = typeof (JointPayee.jointPayeeExternalName))]
  [PXUIField(DisplayName = "Joint Payee")]
  public virtual 
  #nullable disable
  string JointPayeeExternalName { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (Vendor.acctName))]
  [PXDefault]
  public string JointVendorName { get; set; }

  [PXString(30)]
  [PXUIField(DisplayName = "Joint Payee Name")]
  public virtual string Name
  {
    [PXDependsOnFields(new Type[] {typeof (JointPayeeDisplay.jointPayeeExternalName), typeof (JointPayeeDisplay.jointVendorName)})] get
    {
      return !string.IsNullOrEmpty(this.JointVendorName) ? this.JointVendorName : this.JointPayeeExternalName;
    }
  }

  public abstract class jointPayeeId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  JointPayeeDisplay.jointPayeeId>
  {
  }

  public abstract class jointPayeeExternalName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    JointPayeeDisplay.jointPayeeExternalName>
  {
  }

  public abstract class jointVendorName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    JointPayeeDisplay.jointVendorName>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  JointPayeeDisplay.name>
  {
  }
}
