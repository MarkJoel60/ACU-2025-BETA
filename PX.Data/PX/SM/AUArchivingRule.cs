// Decompiled with JetBrains decompiler
// Type: PX.SM.AUArchivingRule
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

[PXCacheName("Archiving Rule")]
public class AUArchivingRule : AUWorkflowBaseTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault(typeof (AUScreenDefinition.screenID))]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(IsKey = true)]
  public virtual string PrimaryType { get; set; }

  [PXDBString(IsKey = true)]
  public virtual string TableType { get; set; }

  [PXDBInt]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"Parent", "FK", "Select"})]
  public virtual int? ReferStrategy { get; set; }

  [PXDBString]
  public virtual string FKType { get; set; }

  [PXDBString]
  public virtual string SelectType { get; set; }

  [PXDBBool]
  public virtual bool? IsParentToPrimary { get; set; }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUArchivingRule.screenID>
  {
  }

  public abstract class primaryType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUArchivingRule.primaryType>
  {
  }

  public abstract class tableType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUArchivingRule.tableType>
  {
  }

  public abstract class referStrategy : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUArchivingRule.referStrategy>
  {
  }

  public abstract class fkType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUArchivingRule.fkType>
  {
  }

  public abstract class selectType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUArchivingRule.selectType>
  {
  }

  public abstract class isParentToPrimary : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUArchivingRule.isParentToPrimary>
  {
  }
}
