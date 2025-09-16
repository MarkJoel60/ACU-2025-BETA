// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WZTaskSuccessorRelation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.WZ;

[PXHidden]
[Serializable]
public class WZTaskSuccessorRelation : WZTaskRelation
{
  [PXDBGuid(false, IsKey = true)]
  [PXDBDefault(typeof (WZScenario.scenarioID))]
  [PXParent(typeof (Select<WZScenario, Where<WZScenario.scenarioID, Equal<Current<WZTaskSuccessorRelation.scenarioID>>>>))]
  public override Guid? ScenarioID { get; set; }

  [PXDBGuid(false, IsKey = true)]
  [PXUIField]
  [PXDefault]
  [PXSelector(typeof (Search<WZTask.taskID, Where<WZTask.scenarioID, Equal<Current<WZTaskSuccessorRelation.scenarioID>>, And<WZTask.taskID, NotEqual<Current<WZTask.taskID>>>>>), SubstituteKey = typeof (WZTask.name))]
  public override Guid? TaskID { get; set; }

  [PXDBGuid(false, IsKey = true)]
  [PXDBDefault(typeof (WZTask.taskID))]
  public override Guid? PredecessorID { get; set; }

  public new abstract class scenarioID : 
    BqlType<IBqlGuid, Guid>.Field<
    #nullable disable
    WZTaskSuccessorRelation.scenarioID>
  {
  }

  public new abstract class taskID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZTaskSuccessorRelation.taskID>
  {
  }

  public new abstract class predecessorID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    WZTaskSuccessorRelation.predecessorID>
  {
  }
}
