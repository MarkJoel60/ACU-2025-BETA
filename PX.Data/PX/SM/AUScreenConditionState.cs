// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenConditionState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Linq;

#nullable enable
namespace PX.SM;

public class AUScreenConditionState : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IScreenItem
{
  public const 
  #nullable disable
  string SystemReadonly = "System Readonly";
  [NonSerialized]
  public IWorkflowCondition InternalImplementation;

  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  public virtual string ScreenID { get; set; }

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? ConditionID { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
  [PXUIField(DisplayName = "Condition Name")]
  public virtual string ConditionName { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "System Condition", Visible = false)]
  public virtual string ParentCondition { get; set; }

  [PXInt]
  public virtual int? Order { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "IsSystem")]
  public bool? IsSystem { get; set; }

  [PXString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Expression", IsReadOnly = true)]
  public virtual string Expression { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Append System Condition")]
  public bool? AppendSystemCondition { get; set; } = new bool?(false);

  [PXDBString(3, IsUnicode = false, IsFixed = false)]
  [PXDefault("AND")]
  [PXStringList("AND,OR")]
  [PXUIField(DisplayName = "Operator", Required = true, Visible = false)]
  public virtual string JoinMethod { get; set; } = "AND";

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Inverted", IsReadOnly = true)]
  public bool? InvertCondition { get; set; } = new bool?(false);

  [PXUIField(DisplayName = "Status", Enabled = false)]
  public string CalcStatus
  {
    get
    {
      if (!string.IsNullOrEmpty(this.ScreenID) && this.ConditionID.HasValue && PXSystemWorkflows.GetSystemConditionsList(this.ScreenID).FirstOrDefault<AUScreenConditionState>((Func<AUScreenConditionState, bool>) (it =>
      {
        Guid? conditionId1 = it.ConditionID;
        Guid? conditionId2 = this.ConditionID;
        if (conditionId1.HasValue != conditionId2.HasValue)
          return false;
        return !conditionId1.HasValue || conditionId1.GetValueOrDefault() == conditionId2.GetValueOrDefault();
      })) != null)
        return "System Readonly";
      if (this.ParentCondition != null)
      {
        bool? appendSystemCondition = this.AppendSystemCondition;
        bool flag = true;
        if (appendSystemCondition.GetValueOrDefault() == flag & appendSystemCondition.HasValue)
          return "Inherited";
      }
      return "New";
    }
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenConditionState.screenID>
  {
  }

  public abstract class conditionID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUScreenConditionState.conditionID>
  {
  }

  public abstract class conditionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenConditionState.conditionName>
  {
  }

  public abstract class parentCondition : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenConditionState.parentCondition>
  {
  }

  public abstract class order : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScreenConditionState.order>
  {
  }

  public abstract class expression : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenConditionState.expression>
  {
  }

  public abstract class appendSystemCondition : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenConditionState.appendSystemCondition>
  {
  }

  public abstract class joinMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenConditionState.joinMethod>
  {
  }

  public abstract class invertCondition : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenConditionState.invertCondition>
  {
  }
}
