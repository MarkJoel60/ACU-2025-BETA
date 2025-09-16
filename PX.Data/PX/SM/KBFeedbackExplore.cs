// Decompiled with JetBrains decompiler
// Type: PX.SM.KBFeedbackExplore
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

/// <exclude />
public class KBFeedbackExplore : PXGraph<KBFeedbackExplore>
{
  public PXSelectJoin<KBFeedback, InnerJoin<Users, On<Users.pKID, Equal<KBFeedback.userID>>>> Responses;
  public PXAction<KBFeedback> Feedback;

  [PXUIField(DisplayName = "View Feedback", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public virtual void feedback()
  {
    if (this.Responses.Current == null)
      return;
    PXGraph instance = PXGraph.CreateInstance(typeof (KBFeedbackMaint));
    instance.Views["Responses"].Cache.Current = (object) this.Responses.Current;
    instance.Actions["Submit"].SetVisible(false);
    PXRedirectHelper.TryRedirect(instance, (object) this.Responses.Current, PXRedirectHelper.WindowMode.Popup);
  }

  public KBFeedbackExplore()
  {
    PXUIFieldAttribute.SetEnabled<KBFeedback.feedbackID>(this.Responses.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<KBFeedback.isFind>(this.Responses.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<KBFeedback.summary>(this.Responses.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<KBFeedback.satisfaction>(this.Responses.Cache, (object) null, false);
    PXUIFieldAttribute.SetDisplayName<KBFeedback.feedbackID>(this.Responses.Cache, "ID");
    PXUIFieldAttribute.SetDisplayName<KBFeedback.summary>(this.Responses.Cache, "Summary");
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Found Answer")]
  protected virtual void KBFeedback_IsFind_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Score")]
  protected virtual void KBFeedback_Satisfaction_CacheAttached(PXCache sender)
  {
  }
}
